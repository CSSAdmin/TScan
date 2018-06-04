//--------------------------------------------------------------------------------------------
// <copyright file="F15018.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15018.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Feb 07        Ranjani JG        	    Created// 
// 02 Feb 09        Sadha Shivudu      #4776 TSCO - 15018 Misc Receipt - New Item Grid Column  
// 24/08/09         Sadha Shivudu      Implemented TSCO # 2803 - Default Interest/Receipt Dates now global
// 28/06/11         Manoj Kumar         Change default tender Type as 'Check'
// 12 SEP 2011      Manoj Kumar         Removed the tender Type default 'check' TSCO #13410.
//*********************************************************************************

namespace D11018
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
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
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;

    /// <summary>
    /// Form F15018
    /// </summary>
    [SmartPart]
    public partial class F15018 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// form15018Control Controller
        /// </summary>
        private F15018Controller form15018Control;

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
        /// miscReceipt dataset
        /// </summary>
        private F11018MiscReceiptData miscReceipt = new F11018MiscReceiptData();

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber;

        /// <summary>
        /// Declaring the receiptDate
        /// </summary>
        private DateTime receiptDate;

        /// <summary>
        /// miscReceiptFields used to communicate with parent forms
        /// </summary>
        private MiscReceiptFields miscReceiptFields = new MiscReceiptFields();

        /// <summary>
        /// templateValueChanged variable is used to find the template related fields changed or not. 
        /// </summary>   
        private bool templateValueChanged = true;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool receiptDateChanged;

        /// <summary>
        /// checks history grid row validation cancelled or not
        /// </summary>
        private bool processRowEnter;

        /// <summary>
        /// Used to store receiptItemGrid is editable in View mode
        /// </summary>
        private bool receiptItemGridViewIsEditable;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// check to avoid grid RowEnter
        /// </summary>
        private bool avoidRowEnter;

        /// <summary>
        /// To Check whether the Form Load on Process
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used to store the Total Value on form Load
        /// </summary>
        private decimal reciptedTotalValue;

        /// <summary>
        /// Check whether  save process completed
        /// </summary>
        private bool saveCompleted;

        /// <summary>
        /// receivedFrom;
        /// </summary>
        private string receivedFrom;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// iscalleave
        /// </summary>
        private bool iscalleave;

        /// <summary>
        /// iscall
        /// </summary>
        private bool iscall;

        private string accountValueList = string.Empty;

        private UltraGridRow activeRow;

        /// <summary>
        /// instance variable to hold the schedule line grid active cell.
        /// </summary>
        private UltraGridCell activeCell;

        /// <summary>
        /// Minimum filter length from tts_cfg table(TR_AccountListLookup) for auto complete functionality
        /// </summary>
        private int minFilterLength = 3;

        private string isFilteredAccount = string.Empty;

        F11018MiscReceiptData.AccountListingDataTable filterdData = new F11018MiscReceiptData.AccountListingDataTable();

        private string lastChangeValue = string.Empty;

        private bool isAccountEdited = false;

        ///<summary>
        /// Used to hold RollYear Value
        /// </summary>
        private int? rollYear;

        ///<summary>
        /// Used to hold  temp roll Year
        /// </summary>
        private int? tempYear;
        /// <summary>
        /// getAutoPrintOnValue
        /// </summary>
        private CommentsData getAutoPrintOnValue = new CommentsData();

        /// <summary>
        /// autoprintonoff
        /// </summary>
        private bool autoPrintOnOff = false;

        /// <summary>
        /// Misc-receipt Row
        /// </summary>
        F11018MiscReceiptData.GetMiscReceiptRow miscReceiptRow;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15018"/> class.
        /// </summary>
        public F15018()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15018"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15018(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.MiscReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.MiscReceiptPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

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

        #endregion

        #region Enumerator

        /// <summary>
        ///  enumerator for Receipt Type
        /// </summary>
        public enum Receipttype
        {
            /// <summary>
            /// value for Collection
            /// </summary>
            Collection = 1,

            /// <summary>
            /// value for Disbursement
            /// </summary>
            Disbursement = 2
        }

        #endregion Enumerator

        #region Properties

        /// <summary>
        /// Gets or sets the F15018 control.
        /// </summary>
        /// <value>The F15018 control.</value>
        [CreateNew]
        public F15018Controller Form15018Control
        {
            get { return this.form15018Control as F15018Controller; }
            set { this.form15018Control = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////setting the pagemode and prevreceipt id
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                ////display default receipt header - Assigning Default Value.           
                this.ReceiptNumberTextBox.Text = string.Empty;
                ////assign default date to this.ReceiptDateTextBox
                //this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.receiptDate);
                this.isFilteredAccount = string.Empty;
                this.filterdData.Clear();
                // Clear all the binded combo dataset
                this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                this.ReceiptDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
                {
                    this.rollYear = this.ReceiptDateTextBox.DateTextBoxValue.Year;
                }
                else
                {
                    this.rollYear = null; 
                }
                this.ReceivedbyTextBox.Text = TerraScanCommon.UserName;
                this.ReceivedFromTextBox.Text = string.Empty;
                this.miscReceiptFields = new MiscReceiptFields();
                this.ReceiptPanel.Enabled = true;
                this.ReceiptItemPanel.Enabled = true;
                ////clear other fields for display
                this.ClearOtherFields();

                if (this.MiscItemGrid.Rows.Count < 7)
                {
                    ////to assgin empty row at the end of the gird
                    this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                    this.MiscItemGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
                }
                else
                {
                    this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
                }

                ////set default property for new receipt            
                this.SetPageModeProperty();
                this.ClearOtherFields();
                this.TypeComboBox.SelectedIndex = 0;
                this.ReceiptDateTextBox.Focus();
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
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            //this.CustomizeReceiptItemGridView();
            // Clear all the binded combo dataset
            this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
            this.GetMiscReceiptDetails();
            this.avoidRowEnter = false;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.SaveReceipt();
                this.saveCompleted = true;
            }
        }

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
                    this.slicePermissionField.deletePermission = eventArgs.Data.DeletePermission;
                    this.slicePermissionField.editPermission = eventArgs.Data.EditPermission;
                    this.slicePermissionField.newPermission = eventArgs.Data.NewPermission;
                    this.slicePermissionField.openPermission = eventArgs.Data.OpenPermission;
                    // NEED TO IMP - Latha
                   /* ////check for template record
                    if (this.ReceiptItemGridView.OriginalRowCount > 0)
                    {
                        this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
                    }
                    else
                    {
                        this.SaveTemplateButton.Enabled = false;
                    }*/
                    ////check wether the form is populated with records - based on thekeyid                    
                    if (this.miscReceipt.GetMiscReceipt.Rows.Count > 0)
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

                    // Clear all the binded combo dataset
                    this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                    ////Binding Database Columns to ReceiptItem Grid.
                    //this.CustomizeReceiptItemGridView();
                    this.GetMiscReceiptDetails();
                    ////khaja added code to fix Buf#4895
                    this.avoidRowEnter = false;
                    
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F15018 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15018_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                //// Assigning Current Date to ReceiptDate Variable.
                this.receiptDate = DateTime.Today;

                // Populate type combo 
                this.LoadTypeCombo();
                this.PaymentEngineUserControl.ParentWorkItem = this.form15018Control.WorkItem;   
                this.PaymentEngineUserControl.IsAutoPayment = true;

                ////populate form details
                this.GetMiscReceiptDetails();

                // Get minimum filter character length for Auto complete functionality
                this.GetMinimumFilterLength();

                this.iscalleave = true;
                this.iscall = true;

                this.PaymentEngineUserControl.TotalDue = this.TotalDueTextBox.DecimalTextBoxValue;

                if (this.miscReceipt.GetMiscReceipt.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.ReceivedFromTextBox;
                    this.ParentForm.ActiveControl.Focus();
                }
                else
                {
                    Control[] selectedControl = this.ParentForm.Controls.Find("HelpLinkLabel", true);
                    this.ParentForm.ActiveControl = selectedControl[0];
                }

                //this.MiscItemGrid.Rows[0].Activate();
                //this.MiscItemGrid.Rows[0].Selected = true;
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

        #endregion

        #region Private Methods

        #region Load Combo

        /// <summary>
        /// Load typecombo combobox with defualt values.
        /// </summary>
        private void LoadTypeCombo()
        {
            // Populate table with default values
            Hashtable typeTable = new Hashtable();
            typeTable.Add("Collection", (int)Receipttype.Collection);
            typeTable.Add("Disbursement", (int)Receipttype.Disbursement);
            CommonData receiptTypeData = new CommonData();
            receiptTypeData.LoadGeneralComboData(typeTable);

            // Assign default values to combo box
            TypeComboBox.DisplayMember = receiptTypeData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            TypeComboBox.ValueMember = receiptTypeData.ComboBoxDataTable.KeyIdColumn.ColumnName;

            // List the values based on receipttype id
            receiptTypeData.ComboBoxDataTable.DefaultView.Sort = receiptTypeData.ComboBoxDataTable.KeyIdColumn.ColumnName + " ASC";
            TypeComboBox.DataSource = receiptTypeData.ComboBoxDataTable.DefaultView;
        }

        #endregion Load Combo

        #region Receipt Details

        /// <summary>
        /// Gets the Misc Receipt details and fill Receipt header and receipt items.
        /// </summary>
        private void GetMiscReceiptDetails()
        {
            this.miscReceipt.Clear();
            this.miscReceipt = this.form15018Control.WorkItem.F15018_GetMiscReceipt(this.keyId);
            this.miscReceiptFields = new MiscReceiptFields();
            ////change pagemode
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////check whether the form load is on process
            this.flagLoadOnProcess = true;
            ////set autoprint status
            this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15018Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));

            if (this.miscReceipt.GetMiscReceipt.Rows.Count > 0)
            {
                ////enable Receipt Panel
                this.ReceiptPanel.Enabled = true;
                
                ////get receipt header details
                miscReceiptRow = (F11018MiscReceiptData.GetMiscReceiptRow)this.miscReceipt.GetMiscReceipt.Rows[0];
                this.ReceiptNumberTextBox.Text = miscReceiptRow.ReceiptNumber;
                this.ReceivedbyTextBox.Text = miscReceiptRow.ReceivedBy;
                this.ReceiptDateTextBox.Text = miscReceiptRow.ReceiptDate;
                this.ChangeDateBackGround();
                this.ReceivedFromTextBox.Text = miscReceiptRow.ReceivedFrom.Trim();

                /////here the PaymentEngineUserControl's OwnerName is set properly
                this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text.Trim();
                if (!miscReceiptRow.IsOwnerIDNull())
                {
                    this.miscReceiptFields.OwnerId = miscReceiptRow.OwnerID;
                }
                ////get receipt report  
                if (!miscReceiptRow.IsReceiptReportNull())
                {
                    this.reportNumber = miscReceiptRow.ReceiptReport;
                }
                ////by default ppaymentid is zero
                int ppaymentId = 0;
                if (!miscReceiptRow.IsPPaymentIDNull())
                {
                    ppaymentId = miscReceiptRow.PPaymentID;
                }

                if (!miscReceiptRow.IsReceiptTypeIDNull())
                {
                    this.TypeComboBox.SelectedValue = miscReceiptRow.ReceiptTypeID;
                }
                else
                {
                    this.TypeComboBox.SelectedIndex = -1;
                }
                ////check for posted flag - if postid is zero then not posted
                this.NotPostedButton.StatusOffColor = Color.FromArgb(128, 0, 0);

                if (!miscReceiptRow.IsPostIDNull() && miscReceiptRow.PostID > 0)
                {
                    this.receiptItemGridViewIsEditable = false;
                    this.NotPostedButton.StatusIndicator = true;
                    this.ReceivedFromTextBox.IsEditable = false;
                }
                else
                {
                    this.receiptItemGridViewIsEditable = this.PermissionFiled.editPermission;
                    this.NotPostedButton.StatusIndicator = false;
                    this.ReceivedFromTextBox.IsEditable = true;
                }

                ////refresh Receipt Items - load receipt                    
                this.PopulateReceiptItemGrid(false, -999);
                ////calculate total due                    
                this.TotalDueTextBox.Text = this.miscReceipt.ListReceiptItem.Compute(string.Concat("SUM(", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, ")"), "").ToString();

                ////to store the Total Due for the receipt Id
                decimal.TryParse(this.TotalDueTextBox.Text.Trim(), out this.reciptedTotalValue);

                ////load payment
                this.PaymentEngineUserControl.LoadPayment(ppaymentId);

                ////sets property with pagemode
                this.SetPageModeProperty();
                ////this.ReceiptPanel.Focus();
                ////this.ReceiptDateTextBox.Focus();
                this.ReceiptItemPanel.Focus();
                //this.ReceiptItemGridView.Focus();

                ////Khaja added code to fix Bug#4895
                //this.ReceiptItemGridView.Rows[0].Selected = true;

                this.MiscItemGrid.Focus();
                if (this.MiscItemGrid.Rows.Count > 0)
                {
                    this.MiscItemGrid.Rows[0].Activate();
                    this.MiscItemGrid.Rows[0].Selected = true;
                }
                //this.ShowGridButton(0);
            }
            else
            {
                this.ClearMiscReceiptDetails();
            }

            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Clears the Misc Receipt Details
        /// </summary>
        private void ClearMiscReceiptDetails()
        {
            ////clear receipt header header
            this.ReceiptNumberTextBox.Text = string.Empty;
            this.ReceivedbyTextBox.Text = string.Empty;
            this.ReceiptDateTextBox.Text = string.Empty;
            this.ReceivedFromTextBox.Text = string.Empty;
            this.TypeComboBox.SelectedIndex = -1;
            this.ReceiptPanel.Enabled = false;
            this.ClearOtherFields();
            ////reset autoprint
            ////this.AutoPrintOnButton.EnableAutoPrint = false;
        }

        ///<summary>
        /// Used to clear Misc Item Grid change in Roll Year
        /// </summary>
        private void ClearMiscIemGrid()
        {
            
                 if (this.miscReceipt.ListReceiptItem.Rows.Count > 0)
                 {
                     this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                     this.miscReceipt.ListReceiptItem.Clear();
                     this.miscReceipt.AccountListing.Clear();
                     this.PopulateReceiptItemGrid(false, -999);
                     this.PaymentEngineUserControl.LoadPayment();
                     this.MiscItemGrid.DataSource = this.miscReceipt.ListReceiptItem.DefaultView;
                     this.TotalDueTextBox.Text = "0.00";
                     this.PaymentsTotalTextBox.Text = "0.00";
                     this.BalanceAmountTextBox.Text = "0.00";
                     this.BalanceAmountTextBox.BackColor = Color.FromArgb(130, 189, 140);
                     this.BalanceAmountTextBox.ForeColor = Color.Black;
                     
                     
                 }
             
                
        }

        /// <summary>
        /// Clears the other fields.
        /// </summary>
        private void ClearOtherFields()
        {
            ////change receiptdatetext box color
            this.ChangeDateBackGround();
            ////reset post flad
            this.NotPostedButton.StatusOffColor = Color.FromArgb(150, 150, 150);
            this.NotPostedButton.StatusIndicator = false;
            ////clear calculation fields         
            this.TotalDueTextBox.Text = string.Empty;
            this.PaymentEngineUserControl.BalanceAmount = 0;
            this.BalanceAmountTextBox.Text = string.Empty;
            this.PaymentsTotalTextBox.Text = string.Empty;
            ////clear receipt item grid
            this.miscReceipt.ListReceiptItem.Clear();
            this.PopulateReceiptItemGrid(false, -999);
            ////clear payment
            this.PaymentEngineUserControl.LoadPayment();
        }

        #endregion

        #region Save

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        private void SaveReceipt()
        {
            ////default value
            int ppaymentId = 0;
            DataView tempDataView = new DataView();
            ////create payment for balance is zero
            if (this.BalanceAmountTextBox.DecimalTextBoxValue == 0)
            {
                //Commented by purushotham to resolve TFS #21282 to allow negative values

               // this.PaymentEngineUserControl.AllowZeroPayment = false ;

                ppaymentId = this.PaymentEngineUserControl.CreatePayment(this.miscReceiptFields.OwnerId);
            }
            ////clears save receipt
            this.miscReceipt.SaveReceipt.Rows.Clear();
            F11018MiscReceiptData.SaveReceiptRow dr = this.miscReceipt.SaveReceipt.NewSaveReceiptRow();

            ////Current receipt Id is sent When Update
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                dr.ReceiptID = this.keyId;
            }

            ////for misc receipt
            dr.PostTypeID = 30;
            this.receiptDate = this.ReceiptDateTextBox.DateTextBoxValue;
            dr.ReceiptDate = this.receiptDate.ToShortDateString();
            dr.ReceivedFrom = this.ReceivedFromTextBox.Text.Trim();
            dr.UserID = TerraScanCommon.UserId;
            dr.TotalAmount = this.TotalDueTextBox.DecimalTextBoxValue;
            dr.PPaymentID = ppaymentId;
            if (TypeComboBox.SelectedValue != null)
            {
                dr.ReceiptTypeID = (int)TypeComboBox.SelectedValue;
            }

            if (this.miscReceiptFields.MiscTemplateId > 0)
            {
                dr.MiscTemplateID = this.miscReceiptFields.MiscTemplateId;
            }

            this.miscReceipt.SaveReceipt.Rows.Add(dr);
            ////get receipt items
            this.miscReceipt.ListReceiptItem.AcceptChanges();
            
            tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
            DataTable tempDataTable = tempDataView.ToTable();
            //// receipt soureceid - 1 for receipt creation
            int savedReceiptId = 0;
            //try
            //{
                savedReceiptId = this.form15018Control.WorkItem.F1405_SaveMasterReceipt(0, 1, Utility.GetXmlString(new DataTable[] { this.miscReceipt.SaveReceipt, tempDataTable }), null);
                //savedReceiptId = this.form15018Control.WorkItem.F1405_SaveMasterReceipt(0, 1, Utility.GetXmlString(new DataTable[] { this.miscReceipt.SaveReceipt, tempDataTable }));
            //}
            //catch (Exception ex)
            //{
            //}
            ////receipt saved
            if (savedReceiptId > 0)
            {
                // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    TerraScanCommon.ReceiptDate = this.ReceiptDateTextBox.DateTextBoxValue;
                }

                this.keyId = savedReceiptId;
                ////get autoprint status
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form15018Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                if (this.AutoPrintOnButton.EnableAutoPrint)
                {
                    this.getAutoPrintOnValue = this.form15018Control.WorkItem.GetConfigDetails("TR_AutoprintOn");
                    if (this.getAutoPrintOnValue.GetCommentsConfigDetails.Rows.Count > 0)
                    {
                        this.autoPrintOnOff = bool.Parse(this.getAutoPrintOnValue.GetCommentsConfigDetails[0][this.getAutoPrintOnValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                    }
                    Hashtable reportOptionalParameter = new Hashtable();
                    reportOptionalParameter.Add("ReceiptID", savedReceiptId);
                    ////changed the parameter type from string to int
                   // TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);


                    if (this.autoPrintOnOff)
                    {
                        try
                        {
                            TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);
                        }
                        catch (Exception ex) 
                        {
                           // throw ex;
                        }
                    }
                    else
                    {
                    TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.Preview, reportOptionalParameter);
                    }
                }
                

                if (ppaymentId > 0 && this.PaymentEngineUserControl.RefundNow && MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), SharedFunctions.GetResourceString("RefundTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ////refund management form
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(1214);
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }

            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

            ////Binding Database Columns to ReceiptItem Grid.
            //this.CustomizeReceiptItemGridView();
            ////populate misc receipt with the newly saved receipt           
            this.GetMiscReceiptDetails();
            //this.ReceiptItemGridView.Focus();
            this.MiscItemGrid.Focus();
        }

        #endregion

        #region User Defined Methods

      /*  /// <summary>
        /// Displays the account icon.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void DisplayAccountIcon(int rowIndex)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || (this.receiptItemGridViewIsEditable && !this.avoidRowEnter))
            {
                //this.ShowGridButton(rowIndex);
            }
        }

        /// <summary>
        /// Used to show the Button in the Grid
        /// </summary>
        /// <param name="rowIndex">Current row index</param>
        private void ShowGridButton(int rowIndex)
        {
            for (int i = 0; i < this.ReceiptItemGridView.Rows.Count; i++)
            {
                TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.ReceiptItemGridView[this.Account.Name, i];
                if (rowIndex == i)
                {
                    imgCell.Image = Properties.Resources.Abutton;
                }
                else
                {
                    if (rowIndex >= 0)
                    {
                        try
                        {
                            imgCell.Image = new Bitmap(1, 1);
                        }
                        catch
                        {
                        }
                    }
                }
            }

            this.ReceiptItemGridView.Refresh();
        }
*/
        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns SliceValidationFields - validated in master form</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            ////required field validation
            if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptMissingField");
                this.ReceiptDateTextBox.Focus();
                return sliceValidationFields;
            }

            ////khaja added code to fix Bug#5289
            if (String.IsNullOrEmpty(this.ReceivedFromTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptMissingField");
                this.ReceiptDateTextBox.Focus();
                return sliceValidationFields;
            }
            
           // DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
            this.miscReceipt.ListReceiptItem.AcceptChanges();
            DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.TransactionIDColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
            try
            {
                DataTable valdiateTable = tempDataView.ToTable().Copy();
                this.miscReceipt.ListReceiptItem.Clear();
                this.miscReceipt.ListReceiptItem.Merge(valdiateTable, true, MissingSchemaAction.Ignore);
            }
            catch (Exception ex)
            {
            }
            int recordCount = tempDataView.Count;

            try
            {
                tempDataView.RowFilter = string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NULL");
                int accountNumberCount = tempDataView.Count;

                tempDataView.RowFilter = string.Concat(this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " = '0.00' OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NULL");
                int amountNullZeroCount = tempDataView.Count;

                // 1.If Account not selcted from dropdown 
                // 2. No item has any amount (Amount column contains only zero/null value)
                if (accountNumberCount > 0 || amountNullZeroCount.Equals(recordCount))
                {
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptMissingField");
                    //this.ReceiptItemGridView.Focus();
                    this.MiscItemGrid.Focus();
                    return sliceValidationFields;
                }

                // If some of the items having null/zero amount
                if (amountNullZeroCount > 0 && amountNullZeroCount < recordCount)
                {
                    if (MessageBox.Show("This Receipt has some items with no amount specified. These items will be discarded. Do you still wish to continue?", "TerraScan – Discard Zero-Amount Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        sliceValidationFields.DisableNewMethod = true;
                        //this.ReceiptItemGridView.Focus();
                        this.MiscItemGrid.Focus();
                        return sliceValidationFields;
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
           ////check for total due validation
            //if (this.TotalDueTextBox.DecimalTextBoxValue <= 0)
            //{
            //    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidTotalDue");
            //    //this.ReceiptItemGridView.Focus();
            //    return sliceValidationFields;
            //}
            //else
            //{
            decimal tempTotalDueValue;
            /////to check whether the total Due is equal to the total due on Form load
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                decimal.TryParse(this.TotalDueTextBox.Text.Trim(), out tempTotalDueValue);
                if (tempTotalDueValue != this.reciptedTotalValue)
                {
                    MessageBox.Show("Record can not be saved because the receipt total must be equal to " + this.reciptedTotalValue + "", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    sliceValidationFields.DisableNewMethod = true;
                    ////sliceValidationFields.ErrorMessage = "Record can not be saved because the receipt total must be equal to " + this.reciptedTotalValue;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    sliceValidationFields.RequiredFieldMissing = false;
                    //this.ReceiptItemGridView.Focus();
                    return sliceValidationFields;
                }
            }
            //}

            ////check for batch
            if (this.BalanceAmountTextBox.DecimalTextBoxValue != 0)
            {
                if (this.BalanceAmountTextBox.DecimalTextBoxValue > this.TotalDueTextBox.DecimalTextBoxValue)
                {
                    MessageBox.Show("Balance amount is greater than Total Due amount!", string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    sliceValidationFields.DisableNewMethod = true;
                    //this.ReceiptItemGridView.Focus();
                    this.MiscItemGrid.Focus();
                    return sliceValidationFields;
                }
            }
            //Added to implement TFS#21282 
            if (this.BalanceAmountTextBox.DecimalTextBoxValue == 0 && this.PaymentsTotalTextBox.DecimalTextBoxValue ==0)
            {
                if (string.IsNullOrEmpty(PaymentEngineUserControl.PaymentItemsGridView.CurrentRow.Cells["TenderType"].Value.ToString()))
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        sliceValidationFields.DisableNewMethod = true;
                        //this.ReceiptItemGridView.Focus();
                        this.MiscItemGrid.Focus();
                        return sliceValidationFields;
                    }
                }
            }
            ////checks for valid receipt
            string validResult = this.form15018Control.WorkItem.F1009_GetValidReceiptTest(-99, this.ReceiptDateTextBox.DateTextBoxValue);
            if (!String.IsNullOrEmpty(validResult))
            {
                MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("InvalidReceipt")), MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                this.ReceiptDateTextBox.Focus();
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the page mode property.
        /// </summary>
        private void SetPageModeProperty()
        {
            switch (this.pageMode)
            {
                case TerraScanCommon.PageModeTypes.New:

                    ////unlock receipt header
                    this.ReceivedFromTextBox.LockKeyPress = false;
                    this.ReceiptDateTextBox.LockKeyPress = false;
                    this.ReceiptDateCalenderButton.Enabled = true;
                    ////this.ReceivedFromButton.Enabled = true;

                    // Modified to implement TFS#21152 Item
                    //this.SaveTemplateButton.Enabled = false;
                    ////enable paymentengine
                    this.PaymentEngineUserControl.TabStop = true;
                    this.PaymentEngineUserControl.Locked = false;
                    this.PaymentEngineUserControl.ApplyReadonlyColumn = false;
                    this.PaymentEngineUserControl.TotalReceiptAmount = Decimal.Zero;
                    this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text;
                    this.PaymentEngineUserControl.SetDefaultSelection = true;
                    //this.ReceiptItemGridView.TabStop = true;
                    this.MiscItemGrid.Focus();
                    this.HiddenButton.TabStop = false;
                    this.SetReadOnlyToGrid(false);
                    ////template
                    this.templateValueChanged = false;
                    this.FromTemplateButton.Enabled = true;
                    this.FromDistrictButton.Enabled = true;
                    this.TypeComboBox.Enabled = true;
                    break;

                default:

                    ////to avoid the receiptItemGridView row event
                    this.avoidRowEnter = true;

                    if (this.receiptItemGridViewIsEditable)
                    {
                        ////enable paymentengine
                        this.PaymentEngineUserControl.TabStop = true;
                        this.PaymentEngineUserControl.Locked = false;

                        this.PaymentEngineUserControl.ApplyReadonlyColumn = true;
                        string[] obj = new string[] 
                        {
                            "TenderType", "Amount", "PID", "PPID"
                        };

                        this.PaymentEngineUserControl.SetReadOnlycolumn(obj);

                        this.PaymentEngineUserControl.SetDefaultSelection = true;
                       // this.ReceiptItemGridView.TabStop = true;
                        this.SetReadOnlyToGrid(false);
                        ////template                        
                        this.templateValueChanged = true;
                    }
                    else
                    {
                        ////disable paymentengine
                        this.PaymentEngineUserControl.TabStop = false;
                        this.PaymentEngineUserControl.Locked = true;
                        this.PaymentEngineUserControl.ApplyReadonlyColumn = false;
                        this.PaymentEngineUserControl.SetDefaultSelection = false;
                        //this.ReceiptItemGridView.TabStop = false;
                        this.MiscItemGrid.TabStop = false;
                        this.SetReadOnlyToGrid(true);
                    }

                    // Modified to implement TFS#21152 Item
                    //this.SaveTemplateButton.Enabled = true;
                    ////unlock receipt header
                    ////this.ReceivedFromButton.Enabled = false;
                    if (!miscReceiptRow.IsPostIDNull() && miscReceiptRow.PostID > 0)
                    {
                        this.ReceivedFromTextBox.LockKeyPress = true;
                    }
                    else
                    {
                        this.ReceivedFromTextBox.LockKeyPress = false;
                    }
                    this.ReceiptDateTextBox.LockKeyPress = true;
                    this.ReceiptDateCalenderButton.Enabled = false;
                    this.HiddenButton.TabStop = false;
                    ////template                   
                    this.FromTemplateButton.Enabled = false;
                    this.FromDistrictButton.Enabled = false;
                    this.TypeComboBox.Enabled = false;
                    ////check whether the save process completed 
                    if (this.saveCompleted)
                    {
                        this.avoidRowEnter = false;
                        this.saveCompleted = false;
                    }

                    break;
            }
        }

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeReceiptItemGridView
        /// </summary>
        //private void CustomizeReceiptItemGridView()
        //{
        //    this.ReceiptItemGridView.AutoGenerateColumns = false;
        //    this.ReceiptItemGridView.AllowUserToResizeColumns = false;
        //    this.Account.Resizable = DataGridViewTriState.False;
        //    F11018MiscReceiptData.ListReceiptItemDataTable receiptItemDataTable = this.miscReceipt.ListReceiptItem;
        //    this.Account.DataPropertyName = receiptItemDataTable.AccountNameColumn.ColumnName;
        //    this.AccountDescription.DataPropertyName = receiptItemDataTable.DescriptionColumn.ColumnName;
        //    this.Amount.DataPropertyName = receiptItemDataTable.AmountColumn.ColumnName;
        //    this.AccountId.DataPropertyName = receiptItemDataTable.AccountIDColumn.ColumnName;
        //    this.AccountStatus.DataPropertyName = receiptItemDataTable.AccountStatusColumn.ColumnName;
          
        //    //// #4776 TSCO - 15018 Misc Receipt - New Item Grid Column
        //    this.Code.DataPropertyName = receiptItemDataTable.CodeColumn.ColumnName;
        //    F11018MiscReceiptData.AccountListingDataTable accountListTable = new F11018MiscReceiptData.AccountListingDataTable();
        //    (this.ReceiptItemGridView.Columns[this.AccountId.Name] as DataGridViewComboBoxColumn).DataSource = accountListTable.DefaultView;
        //    (this.ReceiptItemGridView.Columns[this.AccountId.Name] as DataGridViewComboBoxColumn).DisplayMember = accountListTable.AccountNameColumn.ColumnName;
        //    (this.ReceiptItemGridView.Columns[this.AccountId.Name] as DataGridViewComboBoxColumn).ValueMember = accountListTable.AccountIDColumn.ColumnName;//// accountListTable.AccountIDColumn.ColumnName;
        //    this.AccountId.DisplayIndex = 0;
        //    //this.Account.DisplayIndex = 0;
        //    this.AccountDescription.DisplayIndex = 1;
        //    this.Code.DisplayIndex = 2;
        //    this.Amount.DisplayIndex = 3;
        //    this.Account.DisplayIndex = 4;
        //    //this.AccountId.DisplayIndex = 4;
        //    this.ReceiptItemGridView.DataSource = receiptItemDataTable.DefaultView;
            
        //}

        /// <summary>
        /// Set readonly to grid
        /// </summary>
        /// <param name="readOnlyGrid">Set Readonly to cells -false to allow edit true to lock.</param>        
        private void SetReadOnlyToGrid(bool readOnlyGrid)
        {
            //this.AccountDescription.ReadOnly = readOnlyGrid;
            //this.Amount.ReadOnly = readOnlyGrid;
            if (!readOnlyGrid)
            {
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellActivation = Activation.AllowEdit;
            }
            else
            {
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.MiscItemGrid.DisplayLayout.Bands[0].Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellActivation = Activation.NoEdit;
            }
        }

        private void CustomizeMiscItemGrid()
        {
            UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];

            // set the RowSelectors to true
            currentBand.Override.RowSelectors = DefaultableBoolean.True;

            // set the rows selection to none for avoid selecting multy rows at a time
            currentBand.Override.SelectTypeRow = SelectType.None;

            this.MiscItemGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;

            // set the column visible position for active columns in grid
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Header.VisiblePosition = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Header.VisiblePosition = 2;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Header.VisiblePosition = 3;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Header.VisiblePosition = 4;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Header.VisiblePosition = 5;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.TransactionIDColumn.ColumnName].Header.VisiblePosition = 6;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerIDColumn.ColumnName].Header.VisiblePosition = 7;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerNameColumn.ColumnName].Header.VisiblePosition = 8;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.ReceivedFromColumn.ColumnName].Header.VisiblePosition = 9;
            
            //currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;

            // set the hidden property to true for non visible columns in grid
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.TransactionIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerNameColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.ReceivedFromColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Hidden = false;

            // set the width for non visible columns in grid
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Width = 300;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].Width = 184;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Width = 100;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Width = 110;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Width = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Width = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.TransactionIDColumn.ColumnName].Width = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerIDColumn.ColumnName].Width = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.OwnerNameColumn.ColumnName].Width = 0;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.ReceivedFromColumn.ColumnName].Width = 0;

            currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Header.Caption = "Account";

            // set the font name for cost, depr and value columns
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8.25F;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            appearance1.ForeColor = System.Drawing.Color.FromArgb(0, 130, 0);
            Infragistics.Win.ConditionValueAppearance conditionValueAppearance1 = new Infragistics.Win.ConditionValueAppearance(new Infragistics.Win.ICondition[] {
                    ((Infragistics.Win.ICondition)(new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.LessThan     , 0, false, typeof(decimal ))))}, new Infragistics.Win.Appearance[] {
                    appearance1});
            conditionValueAppearance1.ApplyAllMatchingConditions = false;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Format = "#,##0.00;(#,##0.00);0.00";
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].ValueBasedAppearance = conditionValueAppearance1;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].MaxLength = 14;
            if (this.MiscItemGrid.Rows.Count < 7)
            {
                ////to assgin empty row at the end of the gird
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }

            this.MiscItemGrid.UpdateMode = UpdateMode.OnUpdate;
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
                this.receiptDate = this.form15018Control.WorkItem.F9001_GetNextWorkingDay();
                return this.receiptDate.ToShortDateString();
            }
            else if (String.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()) && receiptDateTime.Equals(DateTime.MinValue))
            {
                ////check for valid date - if not return the empty value assigned in text box else validated value
                this.receiptDate = DateTime.Now;
                return String.Empty;
            }

            this.receiptDate = receiptDateTime;
            return this.receiptDate.ToShortDateString();
        }

        /// <summary>
        /// Calculatings the balance.
        /// </summary>       
        private void CalculateBalance()
        {
            ////calculating total balance
            decimal receiptBalance = this.TotalDueTextBox.DecimalTextBoxValue - this.PaymentsTotalTextBox.DecimalTextBoxValue;
            this.BalanceAmountTextBox.Text = receiptBalance.ToString();

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
        }

        /// <summary>
        /// Calculatings the receipt total.
        /// </summary>        
        private void CalculateReceiptTotal()
        {
            decimal outDecimalValue;
            decimal receiptTotal = 0;
            int recordCount = 0;

            for (int counter = 0; counter < this.miscReceipt.ListReceiptItem.Rows.Count; counter++)
            {
                if (Decimal.TryParse(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AmountColumn].ToString(), out outDecimalValue))
                {
                    receiptTotal += outDecimalValue;
                    // Code commented for CO
                    //if (outDecimalValue > 0 && !String.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AccountIDColumn].ToString()))
                    //{
                    //    recordCount++;
                    //}
                }

                if ((this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AmountColumn] != null && !string.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AmountColumn].ToString()))
                    || (this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AccountIDColumn] != null && !String.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.AccountIDColumn].ToString()))
                    || (this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.DescriptionColumn] != null && !string.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.DescriptionColumn].ToString()))
                    || (this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.CodeColumn] != null &&!string.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[counter][this.miscReceipt.ListReceiptItem.CodeColumn].ToString())))
                {
                    recordCount++;
                }
            }
            //// modified string format to display leeding zeros.
            this.TotalDueTextBox.Text = receiptTotal.ToString(".00");
            this.PaymentEngineUserControl.TotalReceiptAmount = receiptTotal;
            ////check for template creation

            // Modified to implement TFS#21152 Item
            //if (recordCount > 0)
            //{
            //    this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
            //}
            //else
            //{
            //    this.SaveTemplateButton.Enabled = false;
            //}

            if (recordCount == this.miscReceipt.ListReceiptItem.Rows.Count || this.miscReceipt.ListReceiptItem.Rows.Count < 7)
            {
                // Latha
                //F11018MiscReceiptData.ListReceiptItemRow dr = this.miscReceipt.ListReceiptItem.NewListReceiptItemRow();
                //this.miscReceipt.ListReceiptItem.Rows.Add(dr);
                this.HiddenButton.Visible = true;
                this.HiddenButton.TabStop = true;
                //this.ReceiptItemGridView.Refresh();
                //this.MiscItemGrid.Refresh();
            }
            else
            {
                this.HiddenButton.TabStop = false;
            }
            ////check for scrollbar visibility
            //if (this.miscReceipt.ListReceiptItem.Rows.Count > this.ReceiptItemGridView.NumRowsVisible)
            //{
            //    this.ReceiptItemGridVscrollBar.Visible = false;
            //}
            //else
            //{
            //    this.ReceiptItemGridVscrollBar.Visible = true;
            //}
            ////calculate balance
            this.CalculateBalance();
        }

        ///<summary>
        /// Used to Convert text Box content into RollYear
        /// </summary>
        private void ConvertRollYear()
        {
            if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
            {
                DateTime ds = this.ReceiptDateTextBox.DateTextBoxValue;
                this.rollYear = ds.Year;
            }
            else
            {
                this.rollYear = null;
            }
        }

        /// <summary>
        /// Populates the receipt item grid.
        /// </summary>
        /// <param name="loadReceiptItem">if set to <c>true</c> [load receipt item].</param>
        /// <param name="miscTemplateId">The misc template id.</param>
        private void PopulateReceiptItemGrid(bool loadReceiptItem, int miscTemplateId)
        {
            this.processRowEnter = false;
            if (loadReceiptItem)
            {
                this.miscReceipt.ListReceiptItem.Clear();
                this.miscReceipt.ListReceiptItem.Merge(this.form15018Control.WorkItem.F1021_GetMiscReceiptTemplate(miscTemplateId).ListReceiptItem);
                if (this.miscReceipt.ListReceiptItem.Rows.Count > 0)
                {
                    this.ReceivedFromTextBox.Text = this.miscReceipt.ListReceiptItem.Rows[0][this.miscReceipt.ListReceiptItem.ReceivedFromColumn].ToString();
                    this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text;
                }
            }
            ////check for template record
            if (this.miscReceipt.ListReceiptItem.Rows.Count > 0)
            {
                this.ReceiptItemPanel.Enabled = true;

                // Modified to implement TFS#21152 Item
                //this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
            }
            // Modified to implement TFS#21152 Item
            //else
            //{
            //    this.SaveTemplateButton.Enabled = false;
            //}

            try
            {
                //this.ReceiptItemGridView.DataSource = this.miscReceipt.ListReceiptItem.DefaultView;
                this.MiscItemGrid.DataSource = this.miscReceipt.ListReceiptItem.DefaultView;

                UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
                currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Format = "#,##0.00;(#,##0.00);0.00";
            }
            catch (Exception ex)
            {
                ////no code
            }

            ////Coding added for the issue 3129 on 11/1/2010 by malliga
            this.CalculateReceiptTotal(); 

            this.processRowEnter = true;
        }

        /// <summary>
        /// Changes the date back ground with today.
        /// </summary>
        private void ChangeDateBackGround()
        {
            ////change background color to red if date is not today
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(this.ReceiptDateTextBox.Text) || this.ReceiptDateTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            {
                this.ReceiptDateTextBox.Parent.BackColor = Color.White;
                this.ReceiptDateTextBox.BackColor = Color.White;
            }
            else
            {
                this.ReceiptDateTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                this.ReceiptDateTextBox.BackColor = Color.FromArgb(238, 210, 211);
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Payments the engine user control_ payment item change event.
        /// </summary>
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    ////the below method is used to enable the save and cancel methods
                    this.EditEnabled();
                }

                ////this.PaymentEngineUserControl.OwnerName = this.ReceivedbyTextBox.Text;
                this.PaymentEngineUserControl.TotalDue = this.TotalDueTextBox.DecimalTextBoxValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Events

        private void TypeComboBox_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    ////get default comments detail
                    CommentsData commentsData = this.form15018Control.WorkItem.F9075_GetComment(this.keyId, this.masterFormNo);
                    if (commentsData.GetComments.Rows.Count > 0)
                    {
                        CommentsData.GetCommentsRow dr = (CommentsData.GetCommentsRow)commentsData.GetComments.Rows[0];
                        this.miscReceiptFields.DefaultComment = dr.Comment;
                        this.miscReceiptFields.HighPriority = Convert.ToBoolean(dr.IsHighPriority);
                    }
                    else
                    {
                        this.miscReceiptFields.DefaultComment = string.Empty;
                        this.miscReceiptFields.HighPriority = false;
                    }
                }
                else
                {
                    this.miscReceiptFields.DefaultComment = string.Empty;
                    this.miscReceiptFields.HighPriority = false;
                }

                ////get template items
                //DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " <> '0.00'"), "", DataViewRowState.CurrentRows);
               // DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL AND ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                DataTable tempDataTable = tempDataView.ToTable();
                this.miscReceiptFields.ReceiptItems = Utility.GetXmlString(tempDataTable);
                this.receivedFrom = ReceivedFromTextBox.Text;
                ////Save Tempalte Form - FormID - 1021
                Form saveTemplateForm = this.form15018Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1021, new object[] { this.miscReceiptFields, this.receivedFrom }, this.form15018Control.WorkItem);
                if (saveTemplateForm != null)
                {
                    saveTemplateForm.ShowDialog();
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
        /// Handles the Click event of the FromTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////Select Misc Template Form - FormID - 1532
                Form fromTemplateForm = this.form15018Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1022, new object[] { this.ParentFormId }, this.form15018Control.WorkItem);                
                ////open select template form
                if (fromTemplateForm != null && fromTemplateForm.ShowDialog() == DialogResult.Yes)
                {
                    // Clear all the binded combo dataset
                    this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                    int tempalteId = Convert.ToInt32(TerraScanCommon.GetValue(fromTemplateForm, "MiscTempalteId"));
                    ////refresh the template items grid
                    if (tempalteId > 0 && (!this.templateValueChanged || MessageBox.Show(SharedFunctions.GetResourceString("UndoTemplate"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("ChangesLost")), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    {
                        this.miscReceiptFields.MiscTemplateId = tempalteId;
                        this.templateValueChanged = true;
                        // used to set default payment total
                        this.PaymentsTotalTextBox.DecimalTextBoxValue = 0.0M; 
                        this.PopulateReceiptItemGrid(true, tempalteId);
                        //Removed the tender Type default 'check' TSCO #13410.
                        //this.PaymentEngineUserControl.LoadDefaultValue(this.ReceivedFromTextBox.Text, this.BalanceAmountTextBox.DecimalTextBoxValue);
                        
                        this.CalculateReceiptTotal();
                        
                        this.PaymentEngineUserControl.TotalDue = this.TotalDueTextBox.DecimalTextBoxValue;
                    }
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
        /// Handles the TextChanged event of the ReceivedFromTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceivedFromTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.flagLoadOnProcess)
            {
                this.templateValueChanged = true;
                this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text.Trim();
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceivedFromButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceivedFromButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form parcelForm = this.form15018Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form15018Control.WorkItem);

                if (parcelForm != null && parcelForm.ShowDialog() == DialogResult.Yes)
                {
                    int ownerId;
                    int.TryParse(TerraScanCommon.GetValue(parcelForm, SharedFunctions.GetResourceString("MasterNameOwnerId")), out ownerId);
                    this.miscReceiptFields.OwnerId = ownerId;
                    PartiesOwnerDetailsData partiesOwnerDetails = this.form15018Control.WorkItem.GetOwnerDetails(this.miscReceiptFields.OwnerId);

                    if (partiesOwnerDetails.ListPartiesOwnerDetail.Rows.Count > 0)
                    {
                        this.templateValueChanged = true;
                        this.ReceivedFromTextBox.Text = partiesOwnerDetails.ListPartiesOwnerDetail.Rows[0][partiesOwnerDetails.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.PaymentEngineUserControl.OwnerName = this.ReceivedFromTextBox.Text;
                    }
                    else
                    {
                        this.ReceivedFromTextBox.Text = string.Empty;
                        this.PaymentEngineUserControl.OwnerName = string.Empty;
                    }
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
        /// Handles the TextChanged event of the ReceiptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.receiptDateChanged = true;
            }
        }

        /// <summary>
        /// Handles the Validating event of the ReceiptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    ////change background color with today
                    this.ChangeDateBackGround();

                    if (this.receiptDateChanged)
                    {
                        if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
                        {
                            this.tempYear = this.ReceiptDateTextBox.DateTextBoxValue.Year;
                        }
                        else
                        {
                            this.tempYear = null;
                        }
                        if (this.rollYear != this.tempYear)
                        {
                            this.ClearMiscIemGrid();
                        }
                        ////change the text box value with today and close time
                        this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.ReceiptDateTextBox.DateTextBoxValue);
                        this.receiptDateChanged = false;
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

                this.form15018Control.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
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
/*
        /// <summary>
        /// Handles the Enter event of the HiddenButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HiddenButton_Enter(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptItemGridView.Focus();
                this.ReceiptItemGridView.CurrentCell = this.ReceiptItemGridView[this.Account.Name, this.ReceiptItemGridView.Rows.Count - 1];
                this.HiddenButton.TabStop = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        */
        #endregion

        #region Section Indicator

        /// <summary>
        /// Handles the Click event of the MiscReceiptPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscReceiptPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the MiscReceiptPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscReceiptPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.MiscReceiptToolTip.SetToolTip(this.MiscReceiptPictureBox, Utility.GetFormNameSpace(this.Name));
        }

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Shows the receipt date calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;
            this.isshift = false;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            // Set the calendar location.
            //this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
            this.ReceiptMonthCalender.Left = this.ReceiptDatePanel.Left - 50; // +this.ReceiptDateCalenderButton.Right + this.ReceiptDateCalenderButton.Width;
            this.ReceiptMonthCalender.Top = this.ReceiptDateCalenderButton.Bottom;//this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
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
            ////assign date to the receiptDate and textbox
            this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(selectedDate);
            ////change receiptdatetext box color
            this.ChangeDateBackGround();
            ReceiptDateTextBox.Focus();
            ////this.ReceiptDateCalenderButton.Focus();
            this.ReceiptMonthCalender.Visible = false;
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
        /// Handles the DateSelected event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
                ReceiptDateTextBox.Focus();
                this.iscall = false;
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
                }

                this.isshift = e.Shift;
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
        /// Handles the Leave event of the ReceiptMonthCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptMonthCalender_Leave(object sender, EventArgs e)
        {
            /*CustomTextbox has modified for shift+tab issue.so this code commented*/
            ////ReceiptMonthCalender.Visible = false;
            ////if (ReceiptDateTextBox.Text == string.Empty)
            ////{
            ////    this.ReceiptDateTextBox.Text = this.GetNextWorkingDay(this.receiptDate);
            ////}

            ////if (this.isshift)
            ////{
            ////    ReceiptDateTextBox.Focus();
            ////}
            ////else
            ////{
            ////    ReceivedFromTextBox.Focus();
            ////    this.iscalleave = false;
            ////}
        }

        #endregion

        #region TextBox Events

        /// <summary>
        /// Handles the Leave event of the ReceiptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTextBox_Leave(object sender, EventArgs e)
        {
            /*CustomTextbox has modified for shift+tab issue.so this code commented*/
            ////ReceiptDateTextBox.BackColor = Color.White;
            ////ReceiptDatePanel.BackColor = Color.White;
            ////if (this.isshift)
            ////{
            ////    ReceivedbyTextBox.Focus();
            ////}
            ////else
            ////{
            ////    if (this.iscalleave == false)
            ////    {
            ////        ReceivedFromTextBox.Focus();
            ////        this.iscalleave = true;
            ////    }
            ////    else
            ////    {
            ////        ReceiptDateCalenderButton.Focus();
            ////        this.iscalleave = true;
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the Leave event of the ReceivedFromTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceivedFromTextBox_Leave(object sender, EventArgs e)
        {
            /*CustomTextbox has modified for shift+tab issue.so this code commented*/
            ////ReceivedFromTextBox.BackColor = Color.White;
            ////ReceivedFromPanel.BackColor = Color.White;
            ////if (this.iscall == true)
            ////{
            ////    if (this.isshift)
            ////    {
            ////        ReceiptDateCalenderButton.Focus();
            ////        this.isshift = false;
            ////    }
            ////    else
            ////    {
            ////        ReceiptItemGridView.Focus();
            ////    }
            ////}

            ////this.iscall = true;
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceivedFromTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceivedFromTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*CustomTextbox has modified for shift+tab issue.so this code commented*/
            ////this.isshift = e.Shift;
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceiptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*CustomTextbox has modified for shift+tab issue.so this code commented*/
            ////this.isshift = e.Shift;
        }

        /// <summary>
        /// Handles the Leave event of the ReceivedbyTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceivedbyTextBox_Leave(object sender, EventArgs e)
        {
            ReceivedbyTextBox.BackColor = Color.White;
            ReceivedbyPanel.BackColor = Color.White;

            ////khaja commented this code to fix Bug#3848
            ////if (this.isshift)
            ////{
            ////    ReceiptNumberTextBox.Focus();
            ////    this.isshift = false;
            ////}
            ////else
            ////{
            ////    ReceiptDateTextBox.Focus();
            ////}            
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceivedbyTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceivedbyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ////khaja commented this code to fix Bug#3848
            ////this.isshift = e.Shift;            
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceiptNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ////khaja commented this code to fix Bug#3848
            ////this.isshift = e.Shift;            
        }

        /// <summary>
        /// Handles the Leave event of the ReceiptNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberTextBox_Leave(object sender, EventArgs e)
        {
            ReceiptNumberTextBox.BackColor = Color.White;
            ReceiptNumberPanel.BackColor = Color.White;
            ////khaja commented this code to fix Bug#3848
            ////if (this.isshift)
            ////{
            ////    ExciseReceiptAuditLinkLabel.Focus();
            ////    this.isshift = false;
            ////}
            ////else
            ////{
            ////    ReceivedbyTextBox.Focus();
            ////}
        }

        #endregion

        private void FromDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CommentsData.GetCommentsConfigDetailsDataTable commentsConfigDetailsDataTable = this.form15018Control.WorkItem.GetConfigDetails("TR_RollYear").GetCommentsConfigDetails;

                int rollYear = DateTime.Now.Year;
                if (commentsConfigDetailsDataTable.Rows.Count > 0)
                {
                    rollYear = int.Parse(commentsConfigDetailsDataTable.Rows[0][commentsConfigDetailsDataTable.ConfigurationValueColumn.ColumnName].ToString());
                }
                //object[] optionalParameter = new object[] { rollYear.ToString() };
                Form fromDistrictForm = this.form15018Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1024, new object[] { rollYear.ToString() }, this.form15018Control.WorkItem);
                ////open select template form
                if (fromDistrictForm != null && fromDistrictForm.ShowDialog() == DialogResult.Yes)
                {
                    // Clear all the binded combo dataset
                    this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                    F11018MiscReceiptData.ListReceiptItemDataTable districtDetails = (F11018MiscReceiptData.ListReceiptItemDataTable)TerraScanCommon.GetObject(fromDistrictForm, "SelectedDistrict");
                    
                    string ButtonAction= TerraScanCommon.GetValue(fromDistrictForm, "ButtonAction");


                    if (districtDetails.Rows.Count > 0)
                    {
                        bool hasReplace = true;
                        if (ButtonAction == "Replace")
                        {
                            hasReplace = true;
                        }
                        else
                        {
                            hasReplace = false;
                        }
                        //////refresh the template items grid
                        //Latha
                        //if (this.MiscItemGrid.Rows.Count > 0)
                        //{
                        //    if (MessageBox.Show(SharedFunctions.GetResourceString("UndoTemplate"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("ChangesLost")), MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        //    {
                        //        hasReplace = true;
                        //    }
                        //    else
                        //    {
                        //        hasReplace = false;
                        //    }
                        //}

                        //if (hasReplace)
                        {
                            this.processRowEnter = false;
                            if (hasReplace)
                            {
                                this.miscReceipt.ListReceiptItem.Clear();
                            }
                            this.miscReceipt.ListReceiptItem.Merge(districtDetails);
                            //this.miscReceipt.ListReceiptItem.AcceptChanges();

                            ////check for template record
                            if (this.miscReceipt.ListReceiptItem.Rows.Count > 0)
                            {
                                this.ReceiptItemPanel.Enabled = true;

                                // Modified to implement TFS#21152 Item
                               // this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
                                
                            }
                            else
                            {
                                // Modified to implement TFS#21152 Item
                                //this.SaveTemplateButton.Enabled = false;
                            }
                        
                            //this.ReceiptItemGridView.DataSource = this.miscReceipt.ListReceiptItem.DefaultView;
                            this.MiscItemGrid.DataSource = null;
                            this.MiscItemGrid.DataSource = this.miscReceipt.ListReceiptItem;

                            UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
                            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Format = "#,##0.00;(#,##0.00);0.00";
                            // Latha
                            if (this.miscReceipt.ListReceiptItem.Rows.Count > 0 && this.MiscItemGrid.Rows.Count > 0)
                            {
                                F11018MiscReceiptData.AccountListingDataTable accountListTable = new F11018MiscReceiptData.AccountListingDataTable();
                                //Latha
                                for (int rowCount = 0; rowCount < this.MiscItemGrid.Rows.Count; rowCount++)
                                {
                                    F11018MiscReceiptData.AccountListingRow newAccountList = accountListTable.NewAccountListingRow();
                                    newAccountList.AccountID = int.Parse(this.miscReceipt.ListReceiptItem[rowCount][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString());
                                    newAccountList.AccountName = this.miscReceipt.ListReceiptItem[rowCount][this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].ToString();
                                    accountListTable.Rows.Add(newAccountList);
                                }

                                this.filterdData.Merge(accountListTable);
                            }

                            ////Coding added for the issue 3129 on 11/1/2010 by malliga
                            this.CalculateReceiptTotal();
                            //Removed the tender Type default 'check' TSCO #13410.
                            //this.PaymentEngineUserControl.LoadDefultValue(this.ReceivedFromTextBox.Text, this.BalanceAmountTextBox.DecimalTextBoxValue);
                            this.processRowEnter = true;                       
                        }
                    }
                   
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

        #endregion
        private void GetMinimumFilterLength()
        {
            CommentsData.GetCommentsConfigDetailsDataTable configDetails = this.form15018Control.WorkItem.GetConfigDetails("TR_AccountListLookup").GetCommentsConfigDetails;
            if (configDetails.Rows.Count > 0 && !string.IsNullOrEmpty(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString()))
            {
                int.TryParse(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.minFilterLength);
            }
        }

        private void MiscItemGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeMiscItemGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void MiscItemGrid_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                 this.activeCell = this.MiscItemGrid.ActiveCell;
                 this.activeRow = this.MiscItemGrid.ActiveRow;
                 if (this.activeCell != null && this.activeCell.Value != null)
                 {
                     this.EditEnabled();

                     if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                         && activeCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName))
                     {
                         if (this.tempYear != this.rollYear)
                         {
                             e.Cell.Tag = null;
                         }
                         if (e.Cell.Tag != null && e.Cell.Tag.ToString().ToUpper().Trim().Equals(e.Cell.Text.ToUpper().Trim()))
                         {
                             return;
                         }
                        
                         try
                            {
                             e.Cell.Tag = e.Cell.Text.Trim();
                             this.ConvertRollYear(); 
                             filterdData = this.form15018Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(),this.rollYear,masterFormNo).AccountListing;

                             DataRow[] filteredRow = filterdData.Select();

                             if (this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null)
                             {
                                 this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                             }
                             else
                             {
                                 this.accountValueList = System.Guid.NewGuid().ToString();
                             }
                            
                             if (filteredRow.Length > 0)
                             {
                                 
                                 //this.accountValueList = "Row" + e.Cell.Row. Index;
                                 if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                                 {
                                     this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.Clear();
                                 }

                                 ValueList objValueList = (ValueList)e.Cell.ValueList;
                                 
                                 for (int count = 0; count < filteredRow.Length; count++)
                                 {
                                     if (filteredRow[count][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName] != null
                                          && !string.IsNullOrEmpty(filteredRow[count][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString().Trim()))
                                     {
                                         objValueList.ValueListItems.Add(Convert.ToInt32(filteredRow[count][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString()), filteredRow[count][this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].ToString());
                                     }
                                 }
                             }
                             else
                             {
                                 //e.Cell.CancelUpdate();
                             }
                         }
                         catch (Exception ex)
                         {
                         }
                     }
                  
                 }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void MiscItemGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.MiscItemGrid.ActiveCell;
                this.activeRow = this.MiscItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName))
                    {
                        if (this.activeCell.DataChanged) //((int)this.activeCell.OriginalValue != (int)this.activeCell.Value)//
                        {
                            if (this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value.ToString()))
                            {
                                int accountId = int.Parse(this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value.ToString());
                                F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();
                                accountNameDataSet = this.form15018Control.WorkItem.F15013_GetAccountName(accountId);

                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value = dr.AccountStatus;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Value = dr.AccountName;
                                    //this.ReceiptItemGridView[this.AccountId.Name, e.RowIndex].Value = accountId;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].Value = dr.Description;
                                }
                                else
                                {
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Value = string.Empty;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].Value = string.Empty;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Value = DBNull.Value;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value = 0;
                                }

                                this.activeCell.Value = this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value;

                            }
                            else
                            {
                                this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Value = string.Empty;
                                this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.MiscItemGrid.Rows[activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value = DBNull.Value;
                            }

                            this.CalculateReceiptTotal();
                            this.MiscItemGrid.UpdateData();

                            // To change the 'Account' cell background color based on 'AccountStatus' column value 
                            if (this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value != null 
                                && !string.IsNullOrEmpty(this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value.ToString()))
                            {
                                Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
                                if (Convert.ToBoolean(this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value))
                                {
                                    appearance2.BackColor = System.Drawing.Color.FromArgb(187, 222, 173);
                                }
                                else
                                {
                                    appearance2.BackColor = this.activeRow.Cells[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Appearance.BackColor;
                                }

                                this.activeRow.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Appearance = appearance2;
                            }

                            DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                            DataTable valdiateTable = tempDataView.ToTable().Copy();

                            // Modified to implement TFS#21152 Item
                            //if (tempDataView.Count > 0)
                            //{
                            //    // Modified to implement TFS#21152 Item
                            //    this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
                            //}
                            //else
                            //{
                            //    this.SaveTemplateButton.Enabled = false;
                            //}
                        }
                    }

                    ////checks only if amount or account field
                    if (activeCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName))// || e.ColumnIndex == this.ReceiptItemGridView.Columns[this.Account.Name].Index)
                    {
                        this.MiscItemGrid.UpdateData();
                        ////calculating related values for new values
                        this.CalculateReceiptTotal();
                    }

                    if (this.activeCell.DataChanged)
                    {
                        this.MiscItemGrid.UpdateData();
                        DataView tempDataView = new DataView(this.miscReceipt.ListReceiptItem, string.Concat(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);

                        // Modified to implement TFS#21152 Item
                        //if (tempDataView.Count > 0)
                        //{
                        //    this.SaveTemplateButton.Enabled = this.slicePermissionField.newPermission;
                        //}
                        //else
                        //{
                        //    this.SaveTemplateButton.Enabled = false;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void MiscItemGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                this.activeCell = this.MiscItemGrid.ActiveCell;
                this.activeRow = this.MiscItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.accountValueList = System.Guid.NewGuid().ToString();
                            if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList)
                                && this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {
                                // Invalid data cancel leave process
                                //e.Cancel = true;
                                this.activeCell.CancelUpdate();
                                this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                // Clear all the binded combo dataset
                                //this.MiscItemGrid.DisplayLayout.ValueLists.Clear();
                                //this.LoadAccountItem(this.activeRow.Index);
                                // this.MiscItemGrid.DisplayLayout.ValueLists["Row" + this.activeRow.Index].ValueListItems.ValueList.Key 
                                // this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value = this.activeCell.Value;
                                //this.activeCell.Value
                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null)
                                {
                                    this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }

                                  //  availablestring = this.MiscItemGrid.ActiveCell.ValueList.FindStringExact(this.activeCell.Text.Trim());
                                }

                                if (availablestring < 0)
                                {
                                    this.activeCell.CancelUpdate();
                                }
                            }
                        }
                        else
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Value = DBNull.Value;
                        }
                    }

                    if (activeCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            decimal convertedValue = 0;
                            decimal.TryParse(this.activeCell.Text.Trim(), out convertedValue);
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Value = convertedValue;
                            //this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Text = this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Text;
                        }
                        else
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Value = DBNull.Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void MiscItemGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                
                this.LoadAccountItem(e.Row.Index);
                e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ValueList = this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList];
                
                e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                }
                else
                {
                    e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                }
                

                Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
                
                if (this.MiscItemGrid.Rows[e.Row.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value != null 
                    && !string.IsNullOrEmpty(this.MiscItemGrid.Rows[e.Row.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value.ToString()))
                {
                    if (Convert.ToBoolean(this.MiscItemGrid.Rows[e.Row.Index].Cells[this.miscReceipt.ListReceiptItem.AccountStatusColumn.ColumnName].Value))
                    {
                        appearance2.BackColor = System.Drawing.Color.FromArgb(187, 222, 173);
                    }
                    else
                    {
                        appearance2.BackColor = e.Row.Cells[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Appearance.BackColor;
                    }

                    e.Row.Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Appearance = appearance2;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void MiscItemGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || this.receiptItemGridViewIsEditable)
                {
                    ////delete key
                    if (e.KeyCode.Equals(Keys.Delete) && this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveCell == null)// && this.MiscItemGrid.ActiveRow.Selected)
                    {
                        //this.miscReceipt.ListReceiptItem.Rows.RemoveAt(this.MiscItemGrid.ActiveCell.Row.Index);
                        int deleteIndex = this.MiscItemGrid.ActiveRow.Index;
                        //Modifed to delete empty row
                        if (deleteIndex <this.miscReceipt.ListReceiptItem.Rows.Count)
                        {
                            this.miscReceipt.ListReceiptItem.Rows.RemoveAt(this.MiscItemGrid.ActiveRow.Index);

                            this.miscReceipt.AcceptChanges();
                            this.CalculateReceiptTotal();
                            this.EditEnabled();
                            this.MiscItemGrid.UpdateData();
                            this.MiscItemGrid.DataSource = this.miscReceipt.ListReceiptItem.DefaultView;

                            UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
                            currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Format = "#,##0.00;(#,##0.00);0.00";
                            // Latha
                            if (this.miscReceipt.ListReceiptItem.Rows.Count > 0 && this.MiscItemGrid.Rows.Count > 0)
                            {
                                F11018MiscReceiptData.AccountListingDataTable accountListTable = new F11018MiscReceiptData.AccountListingDataTable();
                                //Latha
                                for (int rowCount = 0; rowCount < this.MiscItemGrid.Rows.Count; rowCount++)
                                {
                                    F11018MiscReceiptData.AccountListingRow newAccountList = accountListTable.NewAccountListingRow();
                                    newAccountList.AccountID = int.Parse(this.miscReceipt.ListReceiptItem[rowCount][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString());
                                    newAccountList.AccountName = this.miscReceipt.ListReceiptItem[rowCount][this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].ToString();
                                    accountListTable.Rows.Add(newAccountList);
                                }

                                this.filterdData.Merge(accountListTable);
                            }

                            this.MiscItemGrid.Focus();
                            if (this.MiscItemGrid.Rows.Count > 0)
                            {
                                if (deleteIndex > 0)
                                {
                                    //this.accountValueList = "Row" + (deleteIndex - 1);
                                    this.MiscItemGrid.Rows[deleteIndex - 1].Activate();
                                    this.MiscItemGrid.Rows[deleteIndex - 1].Selected = true;
                                }
                                else
                                {
                                    //this.accountValueList = "Row0";
                                    this.MiscItemGrid.Rows[0].Activate();
                                    this.MiscItemGrid.Rows[0].Selected = true;
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
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MiscItemGrid_AfterCellActivate(object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (this.MiscItemGrid.ActiveCell.Value != null && this.MiscItemGrid.ActiveCell.Column.Key.Equals(this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName)
            //        && !string.IsNullOrEmpty(this.MiscItemGrid.ActiveCell.Value.ToString()))
            //    {
            //        decimal roundedValue = 0.00M;
            //        decimal.TryParse(this.MiscItemGrid.ActiveCell.Value.ToString(), out roundedValue);
            //        if (roundedValue > 0)
            //        {
            //            string formattedValue = decimal.Round(roundedValue, 2, MidpointRounding.AwayFromZero).ToString("#,##0.00;0.00");
            //            this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveCell.Row.Index].Cells[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Value = decimal.Parse(formattedValue);
            //        }
            //        else
            //        {
            //            this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveCell.Row.Index].Cells[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Value = decimal.Round(roundedValue, 2, MidpointRounding.AwayFromZero);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
        }

        private void LoadAccountItem(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                if (this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveRow.Index >= 0
                    && this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ValueList != null)
                {
                    this.accountValueList = this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveRow.Index].Cells[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ValueList.ToString();
                }
                else
                {
                    this.accountValueList = System.Guid.NewGuid().ToString();
                }
                //this.accountValueList = "Row" + rowIndex;

                // If the cell already populated with list item
                if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                {
                    return;
                }

                if (this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveRow.Index.Equals(rowIndex)
                       && this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null && this.MiscItemGrid.ActiveCell.ValueList.ItemCount > 0)
                {
                    // TO DO
                    this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                }
                else
                {
                    ValueList objValueList = this.MiscItemGrid.DisplayLayout.ValueLists.Add(this.accountValueList);

                    if (rowIndex < this.miscReceipt.ListReceiptItem.Rows.Count)// && this.flagLoadOnProcess)
                    {
                        // ValueList objValueList = this.MiscItemGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                        if (this.miscReceipt.ListReceiptItem.Rows[rowIndex][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.miscReceipt.ListReceiptItem.Rows[rowIndex][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString().Trim()))
                        {
                            objValueList.ValueListItems.Add(Convert.ToInt32(this.miscReceipt.ListReceiptItem.Rows[rowIndex][this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].ToString()), this.miscReceipt.ListReceiptItem.Rows[rowIndex][this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].ToString());
                        }
                    }
                }

            }
        }

    }
}
