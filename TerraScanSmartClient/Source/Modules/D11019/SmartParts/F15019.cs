//--------------------------------------------------------------------------------------------
// <copyright file="F15019.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15109 - Journal Entry.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18 Feb 2007      KUPPUSAMY.B         Created
// 16 Feb 2008      KUPPUSAMY.B         Modified for Bug 3918,4020
// 30 Apr 2009      Shaik Khaja         Modified to fix Bug 3840 (BGcolor of A/c Panels)
// 6 Aug 2010       Manoj Kumar.P       Modified for CO #8567
//*********************************************************************************/

namespace D11019
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using TerraScan.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Common.Reports;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Linq;

    #endregion NameSpace

    /// <summary>
    /// F15019 class
    /// </summary>
    public partial class F15019 : BaseSmartPart
    {
        #region Private Members

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool transferDateChanged;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// Unique keyId for the FromAccountId
        /// </summary>
        private int fromAccountId;

        /// <summary>
        /// Unique keyId for the ToAccountId
        /// </summary>
        private int accountIdToTransfer;

        ///<summary>
        /// Used to Store ToAccountText
        /// </summary>
        private string toAccText;

        /// <summary>
        /// Declaring the transferDate
        /// </summary>
        private DateTime transferDate;

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// Unique keyId for the RollYear
        /// </summary>
        private int rollYearValue;

        /// <summary>
        /// Instance for tempAccountStatus
        /// </summary>
        private bool tempFromAccountStatus;

        /// <summary>
        /// Instance for tempAccountStatus
        /// </summary>
        private bool tempToAccountStatus;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// F15019Controller
        /// </summary>
        private F15019Controller form15019control;

        /// <summary>
        /// F15019JournalEntryData
        /// </summary>
        private F15019JournalEntryData form15019JournalEntryData;

        /// <summary>
        ///  Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();


        /// <summary>
        /// Minimum filter length from tts_cfg table(TR_AccountListLookup) for auto complete functionality
        /// </summary>
        private int minFilterLength = 3;

        /// <summary>
        /// Checked State Changed
        /// </summary>
        private bool ToCheckAcc = false;


        /// <summary>
        /// fromcombo
        /// <summary>
        private string fromCombo;

        /// </summary>
        /// toCombo
        /// </summary>
        private string toCombo;

        /// <summary>
        /// tocomboText
        /// </summary>
        private string toComboText;

        /// <summary>
        /// hold the Copied Account information  
        /// </summary>
        private int copyAccId;

        /// <summary>
        /// bool to hold ToAccount Copy value
        /// </summary>
        private bool toAccCopy = false;


        /// <summary>
        /// bool to hold fromAccount Value
        /// </summary>
        private bool fromAccCopy = false;


        /// <summary>
        /// Combo2 click
        /// </summary>
        private bool combo2Click;

        /// <summary>
        ///  Combo1 Click
        /// </summary>
        private bool combo1Click;

        /// <summary>
        /// Used to hold Roll Year
        /// </summary>
        private int? rollYear;

        /// <summary>
        /// Temp rollYear
        /// </summary>
        private int? tempYear;

        private static bool isChecked;

        F11018MiscReceiptData.AccountListingDataTable TOAccountData = new F11018MiscReceiptData.AccountListingDataTable();

        F11018MiscReceiptData.AccountListingDataTable FromAccountData = new F11018MiscReceiptData.AccountListingDataTable();

        // F15019JournalEntryData.AccountListingDataTable filterdDat1 = new F15019JournalEntryData.AccountListingDataTable(); 
        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15019"/> class.
        /// </summary>
        public F15019()
        {
            InitializeComponent();
            this.CheckedCondition();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15019"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15019(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            if (keyID == -99)
            {
                isChecked = false;
            }
            this.InitializeComponent();
            this.CheckedCondition();
            ////this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.TransfersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TransfersPictureBox.Height, this.TransfersPictureBox.Width, tabText, red, green, blue);
            this.form15019JournalEntryData = new F15019JournalEntryData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15019"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The featureClassID</param>
        public F15019(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            if (keyID == -99)
            {
                isChecked = false;
            }
            this.InitializeComponent();
            this.CheckedCondition();
            ////this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.TransfersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TransfersPictureBox.Height, this.TransfersPictureBox.Width, tabText, red, green, blue);
            this.form15019JournalEntryData = new F15019JournalEntryData();
            this.getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        }
        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

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

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form15019 control.
        /// </summary>
        /// <value>The form15019 control.</value>
        [CreateNew]
        public F15019Controller Form15019Control
        {
            get { return this.form15019control as F15019Controller; }
            set { this.form15019control = value; }
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
            }
        }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearJournalEntryDetails();
                //this.AccountInfoBackColor((Panel)this.FromAccountPanel.Parent, false, this.comboBox1);
                //this.AccountInfoBackColor((Panel)this.ToAccounttpanel.Parent, false, this.comboBox2);
                this.ControlLock(false);
                this.LockControls(true);
                if (!this.slicePermissionField.newPermission)
                {
                    this.StartNewcheckBox.Enabled = false;
                }
                else
                {
                    this.StartNewcheckBox.Enabled = true;
                }
                this.checkBoxPanel.Enabled = true;
                this.TransferAmountTextBox.Text = 0.ToString("$ #,##0.00");
                //this.FromAccountButton.Enabled = true;
                //this.FromAccountTextBox.LockKeyPress = true;
                //this.ToAccountButton.Enabled = true;
                //this.ToAccountTextBox.LockKeyPress = true;
                this.EnteredByTextBox.LockKeyPress = true;
                this.EnteredByTextBox.Text = TerraScanCommon.UserName;
                //CO fixed for TFSCO #8567
                this.TransferDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                if (string.IsNullOrEmpty(this.TransferDateTextBox.Text))
                {
                    this.rollYear = this.TransferDateTextBox.DateTextBoxValue.Year;
                }
                else
                {
                    this.rollYear = null;
                }
                this.ChangeDateBackGround();
                this.checkBoxPanel.Enabled = true;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.comboBox1.Visible = true;
                this.FromTextBox.Visible = false;
                this.comboBox2.Visible = true;
                this.ToTextBox.Visible = false;
                this.fromAccCopy = false;
                this.toAccCopy = false;
                this.ToAccounttpanel.Enabled = true;
                this.FromAccountPanel.Enabled = true;
                this.TransferDateCalenderButton.Enabled = true;
                //this.TransferDateTextBox.LockKeyPress = true;
                this.PostedTextBox.LockKeyPress = true;
                this.ReceiptNumberTextBox.LockKeyPress = true;
                this.StartNewpanel.Focus();
                // this.TransferDescriptionTextBox.Focus();

                // TOAccountData.Clear();
                this.tempFromAccountStatus = false;
                this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);

                /*if (StartNewcheckBox.Checked && TOAccountData.Rows.Count > 0 && this.comboBox2.SelectedIndex >= 0)
                {
                    this.comboBox2.Text = this.toAccText;
                    string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                    int.TryParse(toAcc, out this.accountIdToTransfer);
                    ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                    accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                    if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                        {
                            this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                        }
                        if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                        {
                            Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                            tempToTextBoxControl.Text = this.comboBox2.Text;
                            tempToTextBoxControl.Tag = this.accountIdToTransfer;
                            this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                        }
                    }
                }
                else
                {*/
                    if (StartNewcheckBox.Checked)
                    {
                        try
                        {
                            this.comboBox2.ValueMember = TOAccountData.AccountIDColumn.ColumnName;
                            this.comboBox2.DisplayMember = TOAccountData.AccountNameColumn.ColumnName;
                            this.ConvertRollYear();
                            TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(this.toAccText.Trim().Substring(0, 3), this.rollYear,masterFormNo).AccountListing;
                            this.toAccText = this.toAccText + " (" + this.rollYear + ")";
                            //TerraScan.BusinessEntities.F15019JournalEntryData.AccountListingRow
                            //IEnumerable<int> toAcc = (from r in TOAccountData.AsEnumerable()
                            //             where RemoveSymbol(r.AccountName) == RemoveSymbol(this.toAccText.ToString())
                            //             select r.AccountID);

                            var temp = from r in TOAccountData
                                       where RemoveSymbol(r.AccountName) == RemoveSymbol(toAccText)
                                       select r.AccountID;

                            var val = temp.FirstOrDefault().ToString();                            
                            this.toCombo = this.toAccText;
                            this.comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                            this.comboBox2.DataSource = TOAccountData;
                            this.comboBox2.Text = this.toCombo;
                            this.comboBox2.Select(this.comboBox2.Text.Length, 0);
                            this.comboBox2.Text = this.toAccText;
                            //this.ConvertRollYear();
                            //TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(this.comboBox2.Text.Trim().Substring(0,3), this.rollYear).AccountListing;
                            //var toAcc = from r in TOAccountData
                            //               where r.AccountName == this.toAccText
                            //               select r.AccountID;// TOAccountData.Select(TOAccountData.AccountNameColumn.ColumnName + "='" + this.toAccText + "'").ToString();
                            //this.accountIdToTransfer = (new System.Linq.SystemCore_EnumerableDebugView<int>(toAcc)).Items[0];

                            int.TryParse(val.ToString(), out this.accountIdToTransfer);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                                {
                                    Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                                    tempToTextBoxControl.Text = this.comboBox2.Text;
                                    tempToTextBoxControl.Tag = this.accountIdToTransfer;
                                    this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        this.comboBox2.Text = string.Empty;
                        this.tempToAccountStatus = false;
                        this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox2);
                        TOAccountData.Clear();
                    }
                /*}*/

            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearJournalEntryDetails();
                this.ControlLock(true);
                this.LockControls(true);
                //this.FromAccountButton.Enabled = true;
                //this.ToAccountButton.Enabled = true;
                this.checkBoxPanel.Enabled = true;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.TransferDateCalenderButton.Enabled = true;
                this.EnteredByTextBox.LockKeyPress = true;
                this.EnteredByTextBox.Text = TerraScanCommon.UserName;
                //CO fixed for TFSCO #8567
                this.TransferDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                this.ChangeDateBackGround();
                //this.TransferDateTextBox.LockKeyPress = true;
                this.PostedTextBox.LockKeyPress = true;
                this.ReceiptNumberTextBox.LockKeyPress = true;
            }
        }


        private string RemoveSymbol(string value)
        {
            value = Regex.Replace(value, @"&quot;|[ '""$!(),&?%\.*-]", string.Empty);
            return value;
        }


        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }
            //this.StartNewcheckBox.Checked = false;
            TOAccountData.Clear();

            ////To Fix bug #3917 by khaja
            this.ClearJournalEntryDetails();
            this.LockControls(true);

            if (!PermissionEdit)
            {
                this.StartNewcheckBox.Enabled = false;
            }
            else
            {
                this.StartNewcheckBox.Enabled = true;
            }
            this.GetJournalEntryDetails();
            this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);
            this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox2);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //this.ToAccountButton.Enabled = false;
            //this.FromAccountButton.Enabled = false;
            this.TransferDateCalenderButton.Enabled = false;
            //this.checkBoxPanel.Enabled = false;
            //this.comboBox1.Enabled = true;
            //this.comboBox2.Enabled = true;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////khaja made changes to implement transferamountvalidation.
            bool missingRequiredField = false;
            string errorMessage = string.Empty;
            decimal maxTransferAmount = 922337203685477.5807M;
            decimal minTransferAmount = -922337203685477.5808M;

            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    if ((this.TransferAmountTextBox.DecimalTextBoxValue.Equals(0)) || string.IsNullOrEmpty(this.TransferDescriptionTextBox.Text.Trim()) || string.IsNullOrEmpty(this.comboBox1.Text.Trim()) || string.IsNullOrEmpty(this.comboBox2.Text.Trim()))//|| string.IsNullOrEmpty(this.FromAccountTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ToAccountTextBox.Text.Trim()))
                    {
                        missingRequiredField = true;
                        errorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                        this.TransferDescriptionTextBox.Focus();
                    }
                    //CO fixed for TFSCO #8567
                    if (String.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()))
                    {
                        missingRequiredField = true;
                        errorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                        this.TransferDateTextBox.Focus();
                    }

                    if (this.TransferAmountTextBox.DecimalTextBoxValue > maxTransferAmount)
                    {
                        missingRequiredField = true;
                        errorMessage = SharedFunctions.GetResourceString("15019TransferAmountLimit");
                        this.TransferDescriptionTextBox.Focus();
                    }

                    if (this.TransferAmountTextBox.DecimalTextBoxValue < minTransferAmount)
                    {
                        missingRequiredField = true;
                        errorMessage = "Transfer Amount exceeded minimum limit.";
                        this.TransferDescriptionTextBox.Focus();
                    }

                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    if (sliceValidationFields.ErrorMessage == string.Empty && !sliceValidationFields.RequiredFieldMissing)
                    {
                        int alertNoEdit = this.CheckRollYear();
                        if (alertNoEdit == -1)
                        {
                            string internalAlertErrorMessageEdit = string.Empty;
                            internalAlertErrorMessageEdit = SharedFunctions.GetResourceString("15019AccountRollyearValidation");
                            MessageBox.Show(internalAlertErrorMessageEdit, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                        }
                        else if (alertNoEdit == -2)
                        {
                            string internalAlertErrorMessage2Edit = string.Empty;
                            internalAlertErrorMessage2Edit = SharedFunctions.GetResourceString("15019AccountValidation");
                            MessageBox.Show(internalAlertErrorMessage2Edit, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                        }
                    }

                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
            }
            else
            {
                if ((this.TransferAmountTextBox.DecimalTextBoxValue.Equals(0)) || string.IsNullOrEmpty(this.TransferDescriptionTextBox.Text.Trim()) || string.IsNullOrEmpty(this.comboBox1.Text.Trim()) || string.IsNullOrEmpty(this.comboBox2.Text.Trim()))// || string.IsNullOrEmpty(this.FromAccountTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ToAccountTextBox.Text.Trim()))
                {
                    missingRequiredField = true;
                    errorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                    this.TransferDescriptionTextBox.Focus();
                }
                //CO fixed for TFSCO #8567
                if (String.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()))
                {
                    missingRequiredField = true;
                    errorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                    this.TransferDateTextBox.Focus();
                }

                if (this.TransferAmountTextBox.DecimalTextBoxValue > maxTransferAmount)
                {
                    missingRequiredField = true;
                    errorMessage = SharedFunctions.GetResourceString("15019TransferAmountLimit");
                    this.TransferDescriptionTextBox.Focus();
                }

                if (this.TransferAmountTextBox.DecimalTextBoxValue < minTransferAmount)
                {
                    missingRequiredField = true;
                    errorMessage = "Transfer Amount exceeded minimum limit.";
                    this.TransferDescriptionTextBox.Focus();
                }

                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                sliceValidationFields.RequiredFieldMissing = missingRequiredField;
                sliceValidationFields.ErrorMessage = errorMessage;
                if (sliceValidationFields.ErrorMessage == string.Empty && !sliceValidationFields.RequiredFieldMissing)
                {
                    int alertNo = this.CheckRollYear();
                    if (alertNo == -1)
                    {
                        string internalAlertErrorMessage = string.Empty;
                        internalAlertErrorMessage = SharedFunctions.GetResourceString("15019AccountRollyearValidation");
                        MessageBox.Show(internalAlertErrorMessage, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sliceValidationFields.DisableNewMethod = true;
                        sliceValidationFields.RequiredFieldMissing = false;
                        sliceValidationFields.ErrorMessage = string.Empty;
                    }
                    else if (alertNo == -2)
                    {
                        string internalAlertErrorMessage2 = string.Empty;
                        internalAlertErrorMessage2 = SharedFunctions.GetResourceString("15019AccountValidation");
                        MessageBox.Show(internalAlertErrorMessage2, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sliceValidationFields.DisableNewMethod = true;
                        sliceValidationFields.RequiredFieldMissing = false;
                        sliceValidationFields.ErrorMessage = string.Empty;
                    }
                }

                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                int currentKeyId = this.SaveJournalEntryDetails();
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = currentKeyId;
                if (currentKeyId > 0)
                {
                    //CO fixed for TFSCO #8567  General Receipting - Default Interest/Receipt Dates now global
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        TerraScanCommon.ReceiptDate = this.TransferDateTextBox.DateTextBoxValue;
                    }
                }
                ////to reload the form with the current keyid(this.valveId)
                ////to refresh the master form with the return keyid                
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                this.LockControls(true);
                this.ControlLock(false);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (!this.PermissionEdit)
                {
                    this.StartNewcheckBox.Enabled = false;
                }
                else
                {
                    this.StartNewcheckBox.Enabled = true;
                }
            }


        }

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
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    if (this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows.Count > 0)
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
        /// Event Subscription LoadSliceDetails
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.FlagSliceForm = true;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //this.checkBoxPanel.Enabled = false;
                    //this.FromAccountButton.Enabled = false;
                    //this.ToAccountButton.Enabled = false;
                    //CO fixed for TFSCO #8567
                    this.TransferDateCalenderButton.Enabled = false;
                    //this.checkBoxPanel.Enabled = false;
                    //this.ToAccounttpanel.Enabled = false;
                    //this.FromAccountPanel.Enabled = false;
                    this.ControlLock(true);
                    this.GetJournalEntryDetails();
                    this.comboBox2.Enabled = true;
                    this.comboBox1.Enabled = true;
                    this.comboBox1.Visible = false;
                    this.FromTextBox.Visible = true;
                    this.comboBox2.Visible = false;
                    this.ToTextBox.Visible = true;

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
            if (this.slicePermissionField.deletePermission)
            {
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = true;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
            }
        }

        /// <summary>
        /// Event Subscription D84700_F84722_OnSave_SetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                ////this.hydrantId = eventArgs.Data.SelectedKeyId;
            }
        }
        #endregion Event Subscription

        #region Protected methods
        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

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
        #endregion Protected methods

        #region Event Handlers
        /// <summary>
        /// Handles the Load event of the F15019 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15019_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetJournalEntryDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.GetMinimumFilterLength();
                //this.FromAccountButton.Enabled = false;
                //this.ToAccountButton.Enabled = false;
                //this.StartNewcheckBox.Checked = false;
                //this.checkBoxPanel.Enabled = false;
                //this.ToAccounttpanel.Enabled = false;
                // this.FromAccountPanel.Enabled = false;
                this.comboBox1.Enabled = true;
                this.comboBox2.Enabled = true;
                this.comboBox1.Visible = false;
                this.FromTextBox.Visible = true;
                //CO fixed for TFSCO #8567
                this.TransferDateCalenderButton.Enabled = false;
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                TransferDescriptionTextBox.Focus();
                if (!this.PermissionEdit)
                {
                    this.StartNewcheckBox.Enabled = false;
                }
                else
                {
                    this.StartNewcheckBox.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the TransfersPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TransfersPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D11019.F15019"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the TransfersPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TransfersPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.JournalEntryToolTip.SetToolTip(this.TransfersPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the FromAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FromAccountButton_Click(object sender, EventArgs e)
        {
            Button tempButton = new Button();
            tempButton = (Button)sender;

            this.SetEditRecord();
            if (tempButton != null)
            {
                try
                {
                    ////bool tempAccountStatus;
                    this.fromAccountId = 0;

                    ////to get the roll year
                    ////this.getRollYearConfigurationValue.GetCommentsConfigDetails;                

                    this.getRollYearConfigurationValue = this.form15019control.WorkItem.GetConfigDetails("TR_RollYear");

                    if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                    {
                        this.rollYearValue = int.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                    }

                    object[] optionalParameter = new object[] { this.rollYearValue };

                    Form accountSelectionForm = this.form15019control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form15019control.WorkItem);
                    if (accountSelectionForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                    {
                        if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out this.fromAccountId);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);

                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                //this.FromAccountTextBox.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                //if (!string.IsNullOrEmpty(this.FromAccountButton.Tag.ToString()))
                                //{
                                //    Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.FromAccountButton.Tag.ToString());
                                //    tempFromTextBoxControl.Text = this.FromAccountTextBox.Text;
                                //    tempFromTextBoxControl.Tag = this.fromAccountId;
                                //    this.AccountInfoBackColor((Panel)this.FromAccountButton.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                                //}
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ToAccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ToAccountButton_Click(object sender, EventArgs e)
        {
            Button tempButton = new Button();
            tempButton = (Button)sender;

            this.SetEditRecord();
            if (tempButton != null)
            {
                try
                {
                    ////bool tempAccountStatus;
                    this.accountIdToTransfer = 0;

                    this.getRollYearConfigurationValue = this.form15019control.WorkItem.GetConfigDetails("TR_RollYear");

                    if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                    {
                        this.rollYearValue = int.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                    }

                    object[] optionalParameter = new object[] { this.rollYearValue };

                    Form accountSelectionForm = this.form15019control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form15019control.WorkItem);

                    if (accountSelectionForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                    {
                        if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out this.accountIdToTransfer);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);

                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                //this.ToAccountTextBox.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                //if (!string.IsNullOrEmpty(this.ToAccountButton.Tag.ToString()))
                                //{
                                //    Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.ToAccountButton.Tag.ToString());
                                //    tempToTextBoxControl.Text = this.ToAccountTextBox.Text;
                                //    tempToTextBoxControl.Tag = this.accountIdToTransfer;
                                //    this.ToAccountInfoBackColor((Panel)this.ToAccountButton.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                                //}
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
        }

        #endregion Event Handlers

        #region PrivateMethods

        /// <summary>
        /// Gets the journal entry details.
        /// </summary>
        private void GetJournalEntryDetails()
        {
            this.form15019JournalEntryData = this.form15019control.WorkItem.F15019GetJournalEntryDetails(this.keyId);

            if (this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferAmountColumn].ToString()))
                {
                    this.TransferAmountTextBox.Text = Convert.ToDecimal(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferAmountColumn]).ToString("$ #,##0.00");
                }

                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferDescriptionColumn].ToString()))
                {
                    this.TransferDescriptionTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferDescriptionColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.FromAccountColumn].ToString()))
                {
                    this.comboBox1.Visible = false;
                    this.FromTextBox.Visible = true;
                    this.FromTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.FromAccountColumn].ToString();
                    //this.comboBox1.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.FromAccountColumn].ToString();
                }
                else
                {
                    this.comboBox1.Visible = false;
                    this.FromTextBox.Visible = true;
                    this.FromTextBox.Text = string.Empty;
                    //this.comboBox1.Text = string.Empty;
                }

                ////khaja added code to fix Bug#3840
                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.FromAccountIsPendingColumn].ToString()))
                {
                    this.tempFromAccountStatus = Convert.ToBoolean(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.FromAccountIsPendingColumn].ToString());
                }
                this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);
                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ToAccountColumn].ToString()))
                {
                    this.comboBox2.Visible = false;
                    this.ToTextBox.Visible = true;
                    this.ToTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ToAccountColumn].ToString();
                    this.toAccText = this.ToTextBox.Text;
                    //this.comboBox2.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ToAccountColumn].ToString();
                }
                else
                {
                    this.comboBox2.Visible = false;
                    this.ToTextBox.Visible = true;
                    this.ToTextBox.Text = string.Empty;
                    this.toAccText = string.Empty;
                    // this.comboBox2.Text = string.Empty;
                }

                ////khaja added code to fix Bug#3840
                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ToAccountIsPendingColumn].ToString()))
                {
                    this.tempToAccountStatus = Convert.ToBoolean(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ToAccountIsPendingColumn].ToString());
                }
                this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox2);
                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.PostedIDColumn].ToString()))
                {
                    this.PostedTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.PostedIDColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.EnteredByColumn].ToString()))
                {
                    this.EnteredByTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.EnteredByColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ReceiptNumberColumn].ToString()))
                {
                    this.ReceiptNumberTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.ReceiptNumberColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferDateColumn].ToString()))
                {
                    this.TransferDateTextBox.Text = this.form15019JournalEntryData.F15019GetJournalEntryDetails.Rows[0][this.form15019JournalEntryData.F15019GetJournalEntryDetails.TransferDateColumn].ToString();
                }
                this.LockControls(true);
                if (!PermissionEdit)
                {
                    this.StartNewcheckBox.Enabled = false;
                }
                else
                {
                    this.StartNewcheckBox.Enabled = true;
                }
            }
            else
            {
                ////Code added for bug fixing on TFS issue #3918 - when Is query load = "False", form must load in disable mode. 
                this.tempFromAccountStatus = false;
                this.tempToAccountStatus = false;
                this.LockControls(false);
            }
        }

        //CO fixed for TFSCO #8567
        /// <summary>
        /// Changes the date back ground with today.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        private void ChangeDateBackGround()
        {
            ////change background color to red if date is not today
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(TransferDateTextBox.Text) || TransferDateTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            {
                TransferDateTextBox.Parent.BackColor = Color.White;
                TransferDateTextBox.BackColor = Color.White;
            }
            else
            {
                TransferDateTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                TransferDateTextBox.BackColor = Color.FromArgb(238, 210, 211);
            }
        }
        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && (this.PageStatus.Equals(TerraScanCommon.PageStatus.NormalMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode)))
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }

        /// <summary>
        /// From Accounts the color of the info back.
        /// </summary>
        /// <param name="panelControl">The panel control.</param>
        /// <param name="tempAccountStatus">if set to <c>true</c> [temp account status].</param>
        /// <param name="tempTextBoxControl">The temp text box control.</param>
        private void AccountInfoBackColor(Panel panelControl, bool tempAccountStatus, Control tempTextBoxControl)
        {
            if (this.tempFromAccountStatus == false)
            {
                panelControl.BackColor = Color.White;
                tempTextBoxControl.BackColor = Color.White;
                this.FromTextBox.BackColor = Color.White;
                this.comboBox1.BackColor = Color.White;
                this.FromAccountPanel.BackColor = Color.White;
            }
            else
            {
                this.FromAccountPanel.BackColor = Color.FromArgb(187, 222, 173);
                this.comboBox1.BackColor = Color.FromArgb(187, 222, 173);
                this.FromTextBox.BackColor = Color.FromArgb(187, 222, 173);

            }
        }

        /// <summary>
        /// To Accounts the color of the info back.
        /// </summary>
        /// <param name="panelControl">The panel control.</param>
        /// <param name="tempAccountStatus">if set to <c>true</c> [temp account status].</param>
        /// <param name="tempTextBoxControl">The temp text box control.</param>
        private void ToAccountInfoBackColor(Panel panelControl, bool tempAccountStatus, Control tempTextBoxControl)
        {
            if (this.tempToAccountStatus == false)
            {
                panelControl.BackColor = Color.White;
                tempTextBoxControl.BackColor = Color.White;
                this.ToTextBox.BackColor = Color.White;
                this.comboBox2.BackColor = Color.White;
                this.ToAccounttpanel.BackColor = Color.White;
            }
            else
            {

                this.ToAccounttpanel.BackColor = Color.FromArgb(187, 222, 173);
                this.comboBox2.BackColor = Color.FromArgb(187, 222, 173);
                this.ToTextBox.BackColor = Color.FromArgb(187, 222, 173);
            }
        }

        /// <summary>
        /// set focus to the next/previous input field
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="key">The key.</param>
        /// <returns>sourceControl and key</returns>
        private Control SearchControlWithKey(Control sourceControl, string key)
        {
            Control requiredControl = sourceControl;

            if (sourceControl != null)
            {
                if (sourceControl.Controls.ContainsKey(key))
                {
                    return sourceControl.Controls[key];
                }

                foreach (Control sampControl in sourceControl.Controls)
                {
                    if (sampControl.Controls.Count > 0)
                    {
                        requiredControl = this.SearchControlWithKey(sampControl, key);
                        if (requiredControl.Name.Equals(key))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                requiredControl = new Control();
            }

            return requiredControl;
        }

        /// <summary>
        /// Clears the journal entry details.
        /// </summary>
        private void ClearJournalEntryDetails()
        {
            if (!this.StartNewcheckBox.Checked && string.IsNullOrEmpty(this.comboBox2.Text))
            {

                this.comboBox2.Text = string.Empty;
                this.StartNewcheckBox.Checked = false;
            }
            else
            {
                this.comboBox2.Text = this.toAccText;
                this.ToCheckAcc = true;
            }
            //  TOAccountData.Clear();  
            FromAccountData.Clear();
            this.comboBox1.Text = string.Empty;
            this.FromTextBox.Text = string.Empty;
            this.TransferAmountTextBox.Text = string.Empty;
            this.TransferDescriptionTextBox.Text = string.Empty;

            //this.comboBox2.Text = string.Empty;  
            //this.FromAccountTextBox.Text = string.Empty;
            this.FromAccountPanel.BackColor = System.Drawing.Color.White;
            // this.FromAccountTextBox.BackColor = System.Drawing.Color.White;
            // this.ToAccountTextBox.Text = string.Empty;
            this.ToAccounttpanel.BackColor = System.Drawing.Color.White;
            // this.ToAccountTextBox.BackColor = System.Drawing.Color.White;
            this.PostedTextBox.Text = string.Empty;
            this.EnteredByTextBox.Text = string.Empty;
            this.ReceiptNumberTextBox.Text = string.Empty;
            //CO fixed for TFSCO #8567
            this.TransferDateTextBox.Text = string.Empty;
            // this.StartNewcheckBox.Checked = false;   
            ////change transferdatetext box color
            this.ChangeDateBackGround();
        }

        /// <summary>
        /// Clears the All the combo boxs in the form.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void ClearComboBox(Control currentControl)
        {
            if (currentControl.HasChildren)
            {
                foreach (Control childControl in currentControl.Controls)
                {
                    this.ClearComboBox(childControl);
                }
            }
            else
            {
                if (currentControl.GetType() == typeof(TerraScanComboBox))
                {
                    TerraScanComboBox currentComboBox = (TerraScanComboBox)currentControl;
                    currentComboBox.DataSource = null;
                    currentComboBox.Items.Clear();
                    currentComboBox.Refresh();
                }
            }
        }

        /// <summary>
        /// Clears all journal details.
        /// </summary>
        private void ClearAllJournalDetails()
        {
            this.ClearJournalEntryDetails();
            this.ClearComboBox(this);
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            this.StartNewpanel.Enabled = lockControl;
            this.TransferAmountpanel.Enabled = lockControl;
            this.TransferDescriptionpanel.Enabled = lockControl;
            this.FromAccountPanel.Enabled = lockControl;
            this.ToAccounttpanel.Enabled = lockControl;
            this.Postedpanel.Enabled = lockControl;
            this.EnteredBypanel.Enabled = lockControl;
            this.ReceiptNumberPanel.Enabled = lockControl;
            this.TransferDatepanel.Enabled = lockControl;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>requiredControl1</returns>        
        private Control CheckRequiredFields()
        {
            Control requiredControl1 = null;

            if (string.IsNullOrEmpty(this.TransferAmountTextBox.Text.Replace("$", "")))
            {
                this.TransferAmountTextBox.Text = "0";
            }

            if (string.IsNullOrEmpty(this.TransferAmountTextBox.Text.Trim()))
            {
                requiredControl1 = this.TransferAmountTextBox;
            }

            if (string.IsNullOrEmpty(this.TransferDescriptionTextBox.Text.Trim()))
            {
                requiredControl1 = this.TransferDescriptionTextBox;
            }

            //if (string.IsNullOrEmpty(this.FromAccountTextBox.Text.Trim()))
            //{
            //    requiredControl1 = this.FromAccountTextBox;
            //}

            //if (string.IsNullOrEmpty(this.ToAccountTextBox.Text.Trim()))
            //{
            //    requiredControl1 = this.ToAccountTextBox;
            //}

            if (string.IsNullOrEmpty(this.comboBox1.Text.Trim()))
            {
                requiredControl1 = this.comboBox1;
            }

            if (string.IsNullOrEmpty(this.comboBox2.Text.Trim()))
            {
                requiredControl1 = this.comboBox2;
            }
            if (string.IsNullOrEmpty(this.PostedTextBox.Text.Trim()))
            {
                requiredControl1 = this.PostedTextBox;
            }

            if (string.IsNullOrEmpty(this.EnteredByTextBox.Text.Trim()))
            {
                requiredControl1 = this.EnteredByTextBox;
            }

            if (string.IsNullOrEmpty(this.ReceiptNumberTextBox.Text.Trim()))
            {
                requiredControl1 = this.ReceiptNumberTextBox;
            }

            if (string.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()))
            {
                requiredControl1 = this.TransferDateTextBox;
            }
            ////required field validation
            if (String.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.TransferDateTextBox.Focus();
            }


            return requiredControl1;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            this.TransferAmountTextBox.LockKeyPress = controlLock;
            this.TransferDescriptionTextBox.LockKeyPress = controlLock;
            //this.FromAccountTextBox.LockKeyPress = controlLock;
            //this.ToAccountTextBox.LockKeyPress = controlLock;
            this.PostedTextBox.LockKeyPress = controlLock;
            this.EnteredByTextBox.LockKeyPress = controlLock;
            this.ReceiptNumberTextBox.LockKeyPress = controlLock;
            this.TransferDateTextBox.LockKeyPress = controlLock;


        }

        ///<summary>
        /// Used to clear Account Details change of Address
        /// </summary>
        private void ClearAccountDetails()
        {

            if (!string.IsNullOrEmpty(this.comboBox1.Text))
            {
                FromAccountData.Clear();
                this.comboBox1.DataSource = FromAccountData;
                this.comboBox1.Text = string.Empty;
                this.fromAccountId = 0;
                this.tempFromAccountStatus = false;
                this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);

            }
            if (!string.IsNullOrEmpty(this.comboBox2.Text))
            {
                TOAccountData.Clear();
                this.comboBox2.DataSource = TOAccountData;
                this.comboBox2.Text = string.Empty;
                this.accountIdToTransfer = 0;
                this.tempToAccountStatus = false;
                this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox1);

            }

        }

        ///<summary>
        /// Used to Convert text Box content into RollYear
        /// </summary>
        private void ConvertRollYear()
        {
            if (!string.IsNullOrEmpty(this.TransferDateTextBox.Text))
            {
                DateTime ds = this.TransferDateTextBox.DateTextBoxValue;
                this.rollYear = ds.Year;
            }
            else
            {
                this.rollYear = null;
            }
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private int CheckRollYear()
        {
            int errorId;
            errorId = this.form15019control.WorkItem.F15019_CheckRollYear(0, 1, this.FrameJournalentryDetails());
            return errorId;
        }

        /// <summary>
        /// Frames the journalentry details.
        /// </summary>
        /// <returns>journalEntry</returns>
        private string FrameJournalentryDetails()
        {
            ////Code Modified for bug fixing on TFS issue # 4020 - Kuppu
            ////Dataset created and GetXml() added to get parameter XML
            DataSet inputXMLDs = new DataSet("Root");
            DataTable inputXMLdt = new DataTable();
            inputXMLdt.TableName = "Table";
            inputXMLDs.Tables.Add(inputXMLdt);

            DataColumn transferDate = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("TransferDateColumn"), typeof(string));
            DataColumn userID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("UserIDColumn"), typeof(Int32));
            DataColumn ppaymentID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("PPaymentIDColumn"), typeof(Int32));
            DataColumn postID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("PostIDColumn"), typeof(Int32));
            DataColumn interestDate = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("InterestDateColumn"), typeof(string));
            DataColumn transferAmount = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("TransferAmountColumn"), typeof(string));
            DataColumn feeAmount = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("FeeAmountColumn"), typeof(Int32));
            DataColumn interestAmount = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("InterestAmountColumn"), typeof(Int32));
            DataColumn description = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("DescriptionColumn"), typeof(string));
            DataColumn fromAccountID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("FromAccountIDColumn"), typeof(Int32));
            DataColumn toaccountID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("ToAccountIDColumn"), typeof(Int32));
            DataColumn postTypeID = inputXMLdt.Columns.Add(SharedFunctions.GetResourceString("PostTypeIDColumn"), typeof(Int32));

            if (this.comboBox2.SelectedIndex >= 0 && this.accountIdToTransfer==0)
            {
                string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                int.TryParse(toAcc, out this.accountIdToTransfer);

            }
            else
            {
                //if (!this.t)
                //{
                //    this.comboBox2.Text = string.Empty;
                //}
                //if (this.comboBox1.Text == this.comboBox2.Text)
                //{
                //    if (this.comboBox1.SelectedIndex != -1)
                //    {
                //        string fromAcc = FromAccountData.Rows[this.comboBox1.SelectedIndex]["AccountID"].ToString();
                //        int.TryParse(fromAcc, out this.accountIdToTransfer);
                //    }
                //    else
                //    {

                //    }
                //}
            }
            if (this.comboBox1.SelectedIndex >= 0)
            {
                string fromAcc = FromAccountData.Rows[this.comboBox1.SelectedIndex]["AccountID"].ToString();
                int.TryParse(fromAcc, out this.fromAccountId);
            }
            else
            {
                //if (!this.copyAcc)
                //{
                //    this.comboBox2.Text = string.Empty;
                //}
                //if (this.comboBox1.Text == this.comboBox2.Text)
                //{
                //    string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                //    int.TryParse(toAcc, out this.fromAccountId);
                //}
                //else
                //{
                //    this.fromAccountId = 0;
                //}
            }
            if (StartNewcheckBox.Checked)
            {
                this.toAccText = this.comboBox2.Text;
            }

            DataRow inputXMLdr;
            inputXMLdr = inputXMLdt.NewRow();
            inputXMLdr[SharedFunctions.GetResourceString("TransferDateColumn")] = this.TransferDateTextBox.Text.Trim();
            inputXMLdr[SharedFunctions.GetResourceString("UserIDColumn")] = TerraScanCommon.UserId;
            inputXMLdr[SharedFunctions.GetResourceString("PPaymentIDColumn")] = 0;
            inputXMLdr[SharedFunctions.GetResourceString("PostIDColumn")] = 0;
            inputXMLdr[SharedFunctions.GetResourceString("InterestDateColumn")] = this.TransferDateTextBox.Text;
            inputXMLdr[SharedFunctions.GetResourceString("TransferAmountColumn")] = this.TransferAmountTextBox.Text.Trim();
            inputXMLdr[SharedFunctions.GetResourceString("FeeAmountColumn")] = 0;
            inputXMLdr[SharedFunctions.GetResourceString("InterestAmountColumn")] = 0;
            inputXMLdr[SharedFunctions.GetResourceString("DescriptionColumn")] = this.TransferDescriptionTextBox.Text.Trim();
            inputXMLdr[SharedFunctions.GetResourceString("FromAccountIDColumn")] = this.fromAccountId;
            inputXMLdr[SharedFunctions.GetResourceString("ToAccountIDColumn")] = this.accountIdToTransfer;
            inputXMLdr[SharedFunctions.GetResourceString("PostTypeIDColumn")] = 33;

            inputXMLdt.Rows.Add(inputXMLdr);
            inputXMLDs.AcceptChanges();

            string xmlstring;
            xmlstring = inputXMLDs.GetXml();
            return xmlstring;
        }

        /// <summary>
        /// Saves the journal entry details.
        /// </summary>
        /// <returns>currentKeyID</returns>
        private int SaveJournalEntryDetails()
        {
            int currentKeyID = -99;
            bool saveResult = false;

            if (string.IsNullOrEmpty(this.TransferAmountTextBox.Text.Replace("$", "")))
            {
                this.TransferAmountTextBox.Text = "0";
            }

            currentKeyID = this.form15019control.WorkItem.F15019UpdateJournalEntryDetails(0, 1, this.FrameJournalentryDetails());
            this.FromTextBox.Text = this.comboBox1.Text;
            this.comboBox1.Visible = false;
            this.FromTextBox.Visible = true;
            // todo:
            ////if (currentKeyID == -1)
            ////{
            ////    errorMessage = "This record cannot be saved because the Account fields are having different roll year.";
            ////    MessageBox.Show(errorMessage, "TerraScan – Invalid Records Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    saveResult = false;
            ////}            

            return currentKeyID;
        }

        #endregion PrivateMethods

        ////Co to Fix #8567

        /// <summary>
        /// calender button click.
        /// </summary>
        private void TransferDateCalenderButton_Click(object sender, EventArgs e)
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
        /// Shows the Date Calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptMonthCalender.Visible = true;

            // Set the calendar to move one month at a time when navigating using the arrows.
            this.ReceiptMonthCalender.ScrollChange = 1;

            //// Set the calendar location.
            this.ReceiptMonthCalender.Left = 565;
            this.ReceiptMonthCalender.Top = this.TransferDatepanel.Top + this.TransferDateCalenderButton.Bottom;
            this.ReceiptMonthCalender.Tag = this.TransferDateCalenderButton.Tag;
            this.ReceiptMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.TransferDateTextBox.Text))
            {
                this.ReceiptMonthCalender.SetDate(this.TransferDateTextBox.DateTextBoxValue);
            }
        }


        /// <summary>
        /// Validation for Date Calender text Changed.
        /// </summary>
        private void ReceiptMonthCalender_Leave(object sender, EventArgs e)
        {
            this.ReceiptMonthCalender.Visible = false;
        }

        private void GetMinimumFilterLength()
        {
            CommentsData.GetCommentsConfigDetailsDataTable configDetails = this.form15019control.WorkItem.GetConfigDetails("TR_AccountListLookup").GetCommentsConfigDetails;
            if (configDetails.Rows.Count > 0 && !string.IsNullOrEmpty(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString()))
            {
                int.TryParse(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.minFilterLength);
            }
        }
        /// <summary>
        /// Selection of the date from Calender.
        /// </summary>
        private void ReceiptMonthCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start);
                //////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                //double.TryParse(this.InterestDueTextBox.Text.Trim(), out this.tempInterestDue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the next working day and assign it to the receiptDate variable.
        /// </summary>
        /// <param name="receiptDateTime">The Transfer date time.</param>
        /// <returns>the transferDate string</returns>
        private string GetNextWorkingDay(DateTime receiptDateTime)
        {
            ////get next day if today else update for default date management
            if (receiptDateTime.Equals(DateTime.Today))
            {
                this.transferDate = this.form15019control.WorkItem.F9001_GetNextWorkingDay();
                return this.transferDate.ToShortDateString();
            }
            else if (String.IsNullOrEmpty(this.TransferDateTextBox.Text.Trim()) && receiptDateTime.Equals(DateTime.MinValue))
            {
                ////check for valid date - if not return the empty value assigned in text box else validated value
                this.transferDate = DateTime.Now;
                return String.Empty;
            }

            this.transferDate = receiptDateTime;
            return this.transferDate.ToString(this.dateFormat);
        }

        /// <summary>
        /// Set the Selected Date.
        /// </summary>
        private void SetSeletedDate(DateTime selectedDate)
        {
            if (String.Compare(this.ReceiptMonthCalender.Tag.ToString(), this.TransferDateTextBox.Name, true) == 0)
            {
                ////assign date to the receiptDate and textbox
                this.TransferDateTextBox.Text = this.GetNextWorkingDay(selectedDate);
                ////change receiptdatetext box color
                this.ChangeDateBackGround();
                TransferDateTextBox.Focus();
                ////this.TransferDateCalenderButton.Focus();
                this.ReceiptMonthCalender.Visible = false;

            }
        }


        /// <summary>
        /// Validation for Date Calender text Changed.
        /// </summary>
        private void TransferDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    this.transferDateChanged = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Validation for transferDateTextBox.
        /// </summary>
        private void TransferDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {

                    ////change background color with today
                    this.ChangeDateBackGround();

                    if (this.transferDateChanged)
                    {
                        if (!string.IsNullOrEmpty(this.TransferDateTextBox.Text))
                        {
                            this.tempYear = this.TransferDateTextBox.DateTextBoxValue.Year;
                        }
                        else
                        {
                            this.tempYear = null;
                        }
                        if (this.rollYear != this.tempYear)
                        {
                            this.ClearAccountDetails();
                        }
                        ////change the text box value with today and close time
                        this.TransferDateTextBox.Text = this.GetNextWorkingDay(this.TransferDateTextBox.DateTextBoxValue);
                        this.transferDateChanged = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Receipt MonthCalende KeyDown.
        /// </summary>
        private void ReceiptMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.ReceiptMonthCalender.SelectionStart);
                    //////this.tempInterestDue = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                    //double.TryParse(this.InterestDueTextBox.Text.Trim(), out this.tempInterestDue);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }



        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //{
            //    this.comboBox1.Enabled = false;

            //}
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            //if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //{
            //    this.comboBox2.Enabled = false;

            //}
        }


        private void StartNewcheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    if (StartNewcheckBox.Checked)
                    {
                        this.ToCheckAcc = true;
                    }
                    else
                    {
                        this.ToCheckAcc = false;
                    }
                }
                isChecked = StartNewcheckBox.Checked;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    // this.comboBox1.SelectAll();
                    if (this.fromAccCopy)
                    {
                        string[] combo1 = this.comboBox1.Text.Split(new Char[] { '/' });
                        this.ConvertRollYear();
                        FromAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo1[0], this.rollYear,masterFormNo).AccountListing;
                        string sa = "AccountName LIKE '" + this.comboBox1.Text + "%'";
                        DataRow[] dt = FromAccountData.Select(sa);
                        if (FromAccountData.Rows.Count > 0)
                        {
                            this.comboBox1.DataSource = FromAccountData;
                            this.comboBox1.ValueMember = FromAccountData.AccountIDColumn.ColumnName;
                            this.comboBox1.DisplayMember = FromAccountData.AccountNameColumn.ColumnName;
                        }
                        if (dt.Length > 0)
                        {
                            this.comboBox1.Text = Convert.ToString(dt[0][1]);
                            this.fromAccountId = Convert.ToInt32(dt[0][0]);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                if (!string.IsNullOrEmpty(this.comboBox1.Tag.ToString()))
                                {
                                    Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.comboBox1.Tag.ToString());
                                    tempFromTextBoxControl.Text = this.comboBox1.Text;
                                    tempFromTextBoxControl.Tag = this.fromAccountId;
                                    this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                                }
                            }
                            this.comboBox1.Select(0, 0);
                        }
                        else
                        {
                            this.comboBox1.Text = string.Empty;

                        }
                        this.fromAccCopy = false;

                    }
                    else
                    {
                        if (this.comboBox1.Text != null && this.comboBox1.Text.Trim().Length.Equals(this.minFilterLength))
                        {
                            this.comboBox1.ValueMember = FromAccountData.AccountIDColumn.ColumnName;
                            this.comboBox1.DisplayMember = FromAccountData.AccountNameColumn.ColumnName;
                            string combo1 = this.comboBox1.Text.Trim();
                            this.fromCombo = this.comboBox1.Text;
                            this.ConvertRollYear();
                            FromAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo1, this.rollYear,masterFormNo).AccountListing;
                            this.comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                            this.comboBox1.DataSource = FromAccountData;
                            this.comboBox1.Text = this.fromCombo;
                            this.comboBox1.Select(this.comboBox1.Text.Length, 0);

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    if (this.toAccCopy)
                    {
                        string[] combo2 = this.comboBox2.Text.Split(new Char[] { '/' });
                        this.ConvertRollYear();
                        TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo2[0], this.rollYear,masterFormNo).AccountListing;
                        string sa = "AccountName LIKE '" + this.comboBox2.Text + "%'";
                        DataRow[] toAccRow = TOAccountData.Select(sa);
                        if (TOAccountData.Rows.Count > 0)
                        {
                            this.comboBox2.DataSource = TOAccountData;
                            this.comboBox2.ValueMember = TOAccountData.AccountIDColumn.ColumnName;
                            this.comboBox2.DisplayMember = TOAccountData.AccountNameColumn.ColumnName;
                        }
                        if (toAccRow.Length > 0)
                        {
                            this.comboBox2.Text = Convert.ToString(toAccRow[0][1]);
                            this.accountIdToTransfer = Convert.ToInt32(toAccRow[0][0]);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                //   this.comboBox2.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                                {
                                    Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                                    tempToTextBoxControl.Text = this.comboBox2.Text;
                                    tempToTextBoxControl.Tag = this.accountIdToTransfer;
                                    this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                                }
                            }
                            this.comboBox2.Select(0, 0);

                        }
                        else
                        {
                            this.comboBox2.Text = string.Empty;

                        }
                        this.toAccCopy = false;
                    }
                    else
                    {
                        if (this.comboBox2.Text != null && this.comboBox2.Text.Trim().Length.Equals(this.minFilterLength))
                        {
                            this.comboBox2.ValueMember = TOAccountData.AccountIDColumn.ColumnName;
                            this.comboBox2.DisplayMember = TOAccountData.AccountNameColumn.ColumnName;
                            this.ConvertRollYear();
                            TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(this.comboBox2.Text.Trim(), this.rollYear,masterFormNo).AccountListing;
                            this.toCombo = this.comboBox2.Text;
                            this.comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                            this.comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
                            this.comboBox2.DataSource = TOAccountData;
                            this.comboBox2.Text = this.toCombo;
                            this.comboBox2.Select(this.comboBox2.Text.Length, 0);


                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.comboBox2.Enabled = true;
            }
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    if (FromAccountData != null && FromAccountData.Rows.Count > 0)
                    {
                        if (this.comboBox1.SelectedValue == null)
                        {
                            this.comboBox1.Text = string.Empty;
                            this.tempFromAccountStatus = false;
                            this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);
                        }

                        if (!string.IsNullOrEmpty(this.comboBox1.Text))
                        {
                            string fromAcc = FromAccountData.Rows[this.comboBox1.SelectedIndex]["AccountID"].ToString();
                            int.TryParse(fromAcc, out this.fromAccountId);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                if (!string.IsNullOrEmpty(this.comboBox1.Tag.ToString()))
                                {
                                    Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.comboBox1.Tag.ToString());
                                    tempFromTextBoxControl.Text = this.comboBox1.Text;
                                    tempFromTextBoxControl.Tag = this.fromAccountId;
                                    this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                                }
                            }
                        }
                        this.fromAccCopy = false;

                    }
                    else
                    {
                        this.comboBox1.Text = string.Empty;
                        this.tempFromAccountStatus = false;
                        this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, this.comboBox1);
                    }

                    this.comboBox1.Select(0, 0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void comboBox2_Validated(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    if (TOAccountData != null && TOAccountData.Rows.Count > 0)
                    {
                        if (this.comboBox2.SelectedValue == null)
                        {
                            this.comboBox2.Text = string.Empty;
                            this.tempToAccountStatus = false;
                            this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox2);
                        }
                        this.toAccCopy = false;
                        if (!string.IsNullOrEmpty(this.comboBox2.Text))
                        {
                            var temp = from r in TOAccountData
                                       where RemoveSymbol(r.AccountName) == RemoveSymbol(this.comboBox2.Text)
                                       select r.AccountID;
                            
                            string toAcc = temp.FirstOrDefault().ToString();//TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                            int.TryParse(toAcc, out this.accountIdToTransfer);
                            ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                            accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                            if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                                {
                                    this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                                }
                                //this.comboBox2.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                                if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                                {
                                    Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                                    tempToTextBoxControl.Text = this.comboBox2.Text;
                                    tempToTextBoxControl.Tag = this.accountIdToTransfer;
                                    this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                                }
                            }
                        }
                    }
                    else
                    {
                        this.comboBox2.Text = string.Empty;
                        this.tempToAccountStatus = false;
                        this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, this.comboBox2);
                    }
                    this.comboBox2.Select(0, 0);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)3)
            {
                //string fromAcc = FromAccountData.Rows[this.comboBox1.SelectedIndex]["AccountID"].ToString();
                //int.TryParse(fromAcc, out this.fromAccountId);
                //this.copyAccId = this.fromAccountId;
                //ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                //accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);
                //if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                //{
                //    this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                //    if (!string.IsNullOrEmpty(this.comboBox1.Tag.ToString()))
                //    {
                //        Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.comboBox1.Tag.ToString());
                //        tempFromTextBoxControl.Text = this.comboBox1.Text;
                //        tempFromTextBoxControl.Tag = this.fromAccountId;
                //        this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                //    }
                //}


            }
            if (e.KeyChar == (char)22)
            {
                //Clipboard.SetData(DataFormats.Text, Clipboard.GetData(DataFormats.Text));
                //if (Clipboard.ContainsData(DataFormats.Text))
                //{
                //    object combo = Clipboard.GetData(DataFormats.Text);

                //}
                //if (this.combo1Click)
                //{
                //    this.comboBox1.Text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
                //    string[] combo1 = this.comboBox1.Text.Split(new Char[] { '/' });
                //    FromAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo1[0]).AccountListing;
                //    string sa = "AccountName Like '" + this.comboBox1.Text + "*'";
                //    DataRow[] dt = FromAccountData.Select(sa);
                //    if (FromAccountData.Rows.Count > 0)
                //    {
                //        this.comboBox1.DataSource = FromAccountData;
                //        this.comboBox1.ValueMember = FromAccountData.AccountIDColumn.ColumnName;
                //        this.comboBox1.DisplayMember = FromAccountData.AccountNameColumn.ColumnName;
                //    }
                //    if (dt.Length > 0)
                //    {

                //        //this.comboBox1.Select(this.comboBox1.Text.Length, 0);
                //        this.comboBox1.Text = Convert.ToString(dt[0][1]);
                //        this.fromAccountId = Convert.ToInt32(dt[0][0]);
                //        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                //        accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);
                //        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                //        {
                //            this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                //            if (!string.IsNullOrEmpty(this.comboBox1.Tag.ToString()))
                //            {
                //                Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.comboBox1.Tag.ToString());
                //                tempFromTextBoxControl.Text = this.comboBox1.Text;
                //                tempFromTextBoxControl.Tag = this.fromAccountId;
                //                this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                //            }
                //        }
                this.fromAccCopy = true;
            }



            //if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //{
            //    this.comboBox1.Enabled = false;  

        }


        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)3)
            {
                //this.toAccCopy = false;
                //string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                //int.TryParse(toAcc, out this.accountIdToTransfer);
                //ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                //accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                //if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                //{
                //    this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                //    this.comboBox2.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                //    if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                //    {
                //        Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                //        tempToTextBoxControl.Text = this.comboBox2.Text;
                //        tempToTextBoxControl.Tag = this.accountIdToTransfer;
                //        this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                //    }
                //}
                this.copyAccId = this.accountIdToTransfer;

            }
            if (e.KeyChar == (char)22)
            {
                //this.comboBox2.Text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
                //string[] combo2 = this.comboBox2.Text.Split(new Char[] { '/' });
                //TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo2[0]).AccountListing;
                //string sa = "AccountName LIKE '" + this.comboBox2.Text + "*'";
                //DataRow[] toAccRow = TOAccountData.Select(sa);
                //if (toAccRow.Length > 0)
                //{
                //    this.comboBox2.DataSource = TOAccountData;
                //    this.comboBox2.ValueMember = TOAccountData.AccountIDColumn.ColumnName;
                //    this.comboBox2.DisplayMember = TOAccountData.AccountNameColumn.ColumnName;
                //    this.comboBox2.Text = Convert.ToString(toAccRow[0][1]);
                //    this.accountIdToTransfer = Convert.ToInt32(toAccRow[0][0]);
                //    string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                //    int.TryParse(toAcc, out this.accountIdToTransfer);
                //    ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                //    accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                //    if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                //    {
                //        this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                //        //this.comboBox2.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                //        if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                //        {
                //            Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                //            tempToTextBoxControl.Text = this.comboBox2.Text;
                //            tempToTextBoxControl.Tag = this.accountIdToTransfer;
                //            this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                //        }
                //    }
                //    //this.toAccCopy = true;
                //}
                this.toAccCopy = true;
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {


        }


        private void comboBox2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void comboBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.combo2Click)
            {
                Clipboard.SetData(DataFormats.Text, this.comboBox2.Text);
                this.comboBox2.Text = "";
            }
            if (this.combo1Click)
            {
                Clipboard.SetData(DataFormats.Text, this.comboBox1.Text);
                this.comboBox1.Text = "";
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pageMode != TerraScanCommon.PageModeTypes.View)
            {
                Clipboard.SetData(DataFormats.Text, Clipboard.GetData(DataFormats.Text));
                if (Clipboard.ContainsData(DataFormats.Text))
                {
                    object combo = Clipboard.GetData(DataFormats.Text);

                }
                if (this.combo1Click)
                {
                    this.comboBox1.Text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
                    string[] combo1 = this.comboBox1.Text.Split(new Char[] { '/' });
                    this.ConvertRollYear();
                    FromAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo1[0], this.rollYear,masterFormNo).AccountListing;
                    string sa = "AccountName Like '" + this.comboBox1.Text + "*'";
                    DataRow[] dt = FromAccountData.Select(sa);
                    if (FromAccountData.Rows.Count > 0)
                    {
                        this.comboBox1.DataSource = FromAccountData;
                        this.comboBox1.ValueMember = FromAccountData.AccountIDColumn.ColumnName;
                        this.comboBox1.DisplayMember = FromAccountData.AccountNameColumn.ColumnName;
                    }
                    if (dt.Length > 0)
                    {

                        //this.comboBox1.Select(this.comboBox1.Text.Length, 0);
                        this.comboBox1.Text = Convert.ToString(dt[0][1]);
                        this.fromAccountId = Convert.ToInt32(dt[0][0]);
                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                        accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.fromAccountId);
                        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString()))
                            {
                                this.tempFromAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                            }

                            if (!string.IsNullOrEmpty(this.comboBox1.Tag.ToString()))
                            {
                                Control tempFromTextBoxControl = this.SearchControlWithKey(this.FromAccountPanel, this.comboBox1.Tag.ToString());
                                tempFromTextBoxControl.Text = this.comboBox1.Text;
                                tempFromTextBoxControl.Tag = this.fromAccountId;
                                this.AccountInfoBackColor((Panel)this.comboBox1.Parent, this.tempFromAccountStatus, tempFromTextBoxControl);
                            }
                        }
                    }
                    this.fromAccCopy = true;
                }
                if (this.combo2Click)
                {
                    this.comboBox2.Text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
                    string[] combo2 = this.comboBox2.Text.Split(new Char[] { '/' });

                    TOAccountData = this.form15019control.WorkItem.F15018_ListAccountDetails(combo2[0], this.rollYear,masterFormNo).AccountListing;
                    string sa = "AccountName = '" + this.comboBox2.Text + "'";
                    DataRow[] toAccRow = TOAccountData.Select(sa);
                    if (toAccRow.Length > 0)
                    {
                        this.comboBox2.DataSource = TOAccountData;
                        this.comboBox2.ValueMember = TOAccountData.AccountIDColumn.ColumnName;
                        this.comboBox2.DisplayMember = TOAccountData.AccountNameColumn.ColumnName;
                        this.comboBox2.Text = Convert.ToString(toAccRow[0][1]);
                        this.accountIdToTransfer = Convert.ToInt32(toAccRow[0][0]);
                        var temp = from r in TOAccountData
                                   where RemoveSymbol(r.AccountName) == RemoveSymbol(this.comboBox2.Text)
                                   select r.AccountID;

                        string toAcc = temp.FirstOrDefault().ToString();
                        //Cometed by Purushotham added above logic
                       // string toAcc = TOAccountData.Rows[this.comboBox2.SelectedIndex]["AccountID"].ToString();
                        int.TryParse(toAcc, out this.accountIdToTransfer);
                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                        accountNameDataSet = this.form15019control.WorkItem.GetAccountName(this.accountIdToTransfer);
                        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString().ToUpper().Trim()))
                            {
                                this.tempToAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                            }

                            //this.comboBox2.Text = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                            if (!string.IsNullOrEmpty(this.comboBox2.Tag.ToString()))
                            {
                                Control tempToTextBoxControl = this.SearchControlWithKey(this.ToAccounttpanel, this.comboBox2.Tag.ToString());
                                tempToTextBoxControl.Text = this.comboBox2.Text;
                                tempToTextBoxControl.Tag = this.accountIdToTransfer;
                                this.ToAccountInfoBackColor((Panel)this.comboBox2.Parent, this.tempToAccountStatus, tempToTextBoxControl);
                            }
                        }
                        this.toAccCopy = true;
                    }
                }
            }
        }

        private void ToAccounttpanel_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void comboBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.combo2Click)
            {
                Clipboard.SetData(DataFormats.Text, this.comboBox2.Text);

            }
            if (this.combo1Click)
            {
                Clipboard.SetData(DataFormats.Text, this.comboBox1.Text);

            }

        }

        private void comboBox2_MouseDown(object sender, MouseEventArgs e)
        {
            string text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
            if (string.IsNullOrEmpty(text))
            {
                contextMenu.Items[2].Enabled = false;
            }
            else
            {
                contextMenu.Items[2].Enabled = true;
            }
            //if (string.IsNullOrEmpty(Convert.ToString (Clipboard.GetData(DataFormats.Text))))
            //{
            //    contextMenu.Items[2].Enabled = false;
            //}
            this.combo1Click = false;
            this.combo2Click = true;
        }

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            string text = Convert.ToString(Clipboard.GetData(DataFormats.Text));
            if (string.IsNullOrEmpty(text))
            {
                contextMenu.Items[2].Enabled = false;
            }
            else
            {
                contextMenu.Items[2].Enabled = true;
            }
            this.combo1Click = true;
            this.combo2Click = false;
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            //if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            //{
            //    this.toAccText =this.comboBox2.Text;
            //    int.TryParse(this.comboBox2.SelectedValue.ToString(), out this.accountIdToTransfer); 

            //}
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.comboBox2.Enabled = true;
            }
        }

        private void comboBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.comboBox1.BackColor = Color.FromArgb(187, 222, 173);
        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            this.comboBox2.BackColor = Color.FromArgb(187, 222, 173);
        }

        /// <summary>
        /// Check start data condition
        /// </summary>
        private void CheckedCondition()
        {
            if (isChecked)
            {
                this.StartNewcheckBox.Checked = true;
            }
        }

        private void StartNewcheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }


    }

}