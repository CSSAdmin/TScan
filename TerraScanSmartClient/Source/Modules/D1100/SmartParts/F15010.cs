//--------------------------------------------------------------------------------------------
// <copyright file="F15010.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15010 - Excise Affidavit.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  M.Vijayakumar      Created// 
// 30 apr 09        Khaja               Modified to fix Bug#3876
// 18 May 09        Malliga             Coding Added for the issue 3881
// 24/08/09         Sadha Shivudu       Implemented TSCO # 2803 - Default Interest/Receipt Dates now global
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using System.Xml;
    using System.Net;
    using System.Web.Services.Description;
    using System.IO;
    using System.Collections;
    using System.Text;
    using System.Xml.Serialization;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Reflection;


    /// <summary>
    /// F15010 Class file
    /// </summary>
    public partial class F15010 : BaseSmartPart
    {
        #region Variables

        #region ReadOnly

        /// <summary>
        /// used to store useeCode Format
        /// </summary>
        private readonly string validUsedCode = "^[0-9]{2}[-][0-9]{2}[-][0-9]{2}$";

        /// <summary>
        /// used to store DateFormat
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        /// <summary>
        /// used to store DateFormat
        /// </summary>
        private readonly string currencyFormat = SharedFunctions.GetResourceString("DefaultAfdvtAmntforamt");

        /// <summary>
        /// used to store receiptButtonClick
        /// </summary>
        private int receiptButtonClick;

        /// <summary>
        /// used to store flag
        /// </summary>
        private bool flag = false;

        #endregion

        /// <summary>
        /// controller F15010
        /// </summary>
        private F15010Controller form15010Control;

        /// <summary>
        /// To store formLoaded
        /// </summary>
        private bool formLoaded;

        /// <summary>
        /// To store partiesGridClick
        /// </summary>
        private bool partiesGridClick;

        /// <summary>
        /// to store parcelGridClick
        /// </summary>
        private bool parcelGridClick;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// temp owner Id.
        /// </summary>
        private int tempOwnerId;

        /// <summary>
        /// To store the submittedBy value
        /// </summary>
        private bool submittedBy;

        ///<summary>
        /// To store GeneralHeaderPanel enabled
        /// </summary>
        private bool isGeneralHead = false;


        /// <summary>
        /// Keep Track of No sumbit
        /// </summary>
        private int submitCount;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// form height
        /// </summary>
        private int formHeight;

        /// <summary>
        /// form height
        /// </summary>
        private int receiptNo;

        /// <summary>
        /// To store the Main panel height
        /// </summary>
        private int mainPanelHeight;

        /// <summary>
        /// Used to store the ReceiptID
        /// </summary>
        private int receiptId;

        /// <summary>
        /// to store parcelGridClick
        /// </summary>
        private bool flagSave;

        /// <summary>
        /// Used to store the Address1
        /// </summary>
        private string address1 = string.Empty;

        /// <summary>
        /// Used to store the Address2
        /// </summary>
        private string address2 = string.Empty;

        /// <summary>
        /// Used to store the Address1
        /// </summary>
        private DataTable address1dt;

        /// <summary>
        /// Used to store the Address2
        /// </summary>
        private DataTable address2dt;

        private byte isDistrictEditable = 0;


        //changes for DOR Amend
        /// <summary>
        /// xmlFilePath
        /// </summary>
        private string xmlFilePath = string.Empty;

        /// <summary>
        /// Created instance for Typed dataset AffidavitWorkQueue
        /// </summary>
        private SubmittalQueueData affidavitWorkQueueDataSet = new SubmittalQueueData();

        /// <summary>
        /// REETA
        /// </summary>
        private REETA submitDataset = new REETA();

        /// <summary>
        /// Keep Track of no affidavit 
        /// </summary>
        private int affidavitCount;

        /// <summary>
        /// Keep Track of No sumbit success Affidavit  
        /// </summary>
        private int selectedCount;

        /// <summary>
        /// reetSubmitValue
        /// </summary>
        private string reetSubmitValue = string.Empty;

        /// <summary>
        /// Status of Sumbit
        /// </summary>
        private Boolean submitPass;

        /// <summary>
        /// DataTable
        /// </summary>
        private DataTable configurationDatatable = new DataTable();

        DataTable dtParcel = new DataTable();

        Hashtable ht = new Hashtable();

        private int affidavitYear;


        ArrayList arrayList;
        #endregion Variables

        #region Common

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData ownerDetailDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private F15010ExciseAffidavitData districtSelectionDataSet = new F15010ExciseAffidavitData();


        private F15010ExciseAffidavitData getOwnerDataset = new F15010ExciseAffidavitData();

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// Store To keep statementIdExist or  not
        /// </summary>
        private bool receiptIDExist;

        ///<summary>
        /// used to identify the lock controls 
        /// </summary>
        private bool Amend;

        /// <summary>
        /// Used To StoreExchage RateId
        /// </summary>
        private string exciseRateId;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// exciseTaxAffidavitDataSet
        /// </summary>
        private TerraScan.BusinessEntities.F15010ExciseAffidavitData exciseTaxAffidavitDataSet = new F15010ExciseAffidavitData();

        /// <summary>
        /// exciseIndividualtype
        /// </summary>
        private TerraScan.BusinessEntities.F15010ExciseAffidavitData exciseIndividualtype = new F15010ExciseAffidavitData();

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private int districtId;

        /// <summary>
        /// Used to Hold TaxCode
        /// </summary>
        private string taxCode;

        /// <summary>
        /// to store the ExciseTaxStatement id and used in the cancel and new button operation of master form
        /// </summary>
        private int currentTempStatementKeyId;

        /// <summary>
        /// currentExciseTaxStatementId variable is used to store ExciseTaxStatement id. 
        /// </summary>       
        private int currentAffidavitStatementId;

        /// <summary>
        /// hold the genralrow
        /// </summary>
        private DataRow generalHeaderRow;

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private string assessorStatus = string.Empty;

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private string treasurerStatus = string.Empty;

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private string assessorDesc = string.Empty;

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private string treasurerDesc = string.Empty;

        /// <summary>
        /// Assgin the Selected TaxCode
        /// </summary>
        private int selectedTaxCode;

        /// <summary>
        /// Assign the partyHeaderCount
        /// </summary>
        private int partyHeaderCount;

        /// <summary>
        /// Used to store the roll year
        /// </summary>
        private int rollYear;

        /// <summary>
        /// Used to unsavedTreasurerStatus
        /// </summary>
        private string unsavedTreasurerStatus;

        /// <summary>
        /// Used to store unsavedTreasurerDesc
        /// </summary>
        private string unsavedTreasurerDesc;

        /// <summary>
        /// Used to store unsavedAssessorStatus
        /// </summary>
        private string unsavedAssessorStatus;

        /// <summary>
        /// Used to store unsavedAssessorDesc
        /// </summary>
        private string unsavedAssessorDesc;

        /// <summary>
        /// Used to store the assessorUserName
        /// </summary>
        private string assessorUserName;

        /// <summary>
        /// used to store the assessorUserId
        /// </summary>
        private int assessorUserId;

        /// <summary>
        /// Used to store the assessorUpdatedTime
        /// </summary>
        private string assessorUpdatedTime;

        /// <summary>
        /// Used to store the assessorUserName
        /// </summary>
        private string treasurerUserName;

        /// <summary>
        /// used to store the assessorUserId
        /// </summary>
        private int treasurerUserId;

        /// <summary>
        /// Used to store the assessorUpdatedTime
        /// </summary>
        private string treasurerUpdatedTime;

        /// <summary>
        /// Used to store the treasurerStatusModified
        /// </summary>
        private bool treasurerStatusModified;

        /// <summary>
        /// Used to store the assessorStatusModified
        /// </summary>
        private bool assessorStatusModified;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool interestDateChanged;

        /// <summary>
        /// Boolean Value
        /// </summary>        
        private bool receiptDateChanged;

        /// <summary>
        /// To store the Exemptionautocomplete On/Off value
        /// </summary>
        private bool exemptionautocompleteonoff;

        /// <summary>
        /// To store the Exemptionautocomplete On/Off value
        /// </summary>
        private bool exemptionValidateonoff;

        /// <summary>
        /// To store the Receiptdate
        /// </summary>
        private string receiptdate;

        /// <summary>
        /// To store the Interestdate
        /// </summary>
        private string interestdate;

        /// <summary>
        /// Store selected Deed type id
        /// </summary>
        private string tempDeedTypeId = string.Empty;

        /// <summary>
        /// Store selected source id
        /// </summary>
        private int tempSourceId;



        /// <summary>
        /// Flag to identify district selection
        /// </summary>
        private bool isDistrictChanged = false;
        #endregion

        #region Parties Variable

        /// <summary>
        ///  Keep A Track Of Selected PArtyGrid Column Position
        /// </summary>
        private int partieCoulmnIndex;

        /// <summary>
        ///  Keep A Track Of Selected PArtyGrid Position
        /// </summary>
        private int selectedPartyGridRowId;

        /// <summary>
        /// partyHeaderButtonOperation
        /// </summary>
        private int partyHeaderButtonOperation;

        /// <summary>
        ///  Keep A Track Of UpdateStatus
        /// </summary>
        private bool partiesHeaderkeyPressed;

        /// <summary>
        /// Used To Store Temp sore RowID
        /// </summary>
        private int tempRowIdParties;

        /// <summary>
        /// Used To Store partiesRowCount
        /// </summary>
        private int partiesRowCount;

        /// <summary>
        /// Used To Stroe Individual ID Value
        /// </summary>
        private int keyPartiesId;

        /// <summary>
        /// To Check PartyHeader Mandatory Field are Filled or not
        /// </summary>
        private bool partyHeaderValidData;

        /// <summary>
        /// Used to store the whther paties new button is clicked are not
        /// </summary>
        private bool isnewButtonClicked;

        /// <summary>
        /// To state whether the Parties Type Combo Selection Changed evnet should be avoided
        /// </summary>
        private bool avoidPartiesTypeComboSelectionChangedEvent;

        #endregion

        #region Parcel

        /// <summary>
        ///  Keep A Track Of Selected Parcel Column Position
        /// </summary>
        private int parcelCoulmnIndex;

        /// <summary>
        /// parcelButton 
        /// </summary>
        private int parcelButtonOperation;

        /// <summary>
        /// used to keep track the parcel grid row
        /// </summary>
        private int parcelRowId;

        /// <summary>
        /// tempParcelRowId
        /// </summary>
        private int tempParcelRowId;

        /// <summary>
        /// parcelHeaderKeyPressed
        /// </summary>
        private bool parcelHeaderKeyPressed;

        /// <summary>
        /// keyParcelId
        /// </summary>
        private int keyParcelId;

        /// <summary>
        /// keyParcelId
        /// </summary>
        private int parcelRecordCount;

        /// <summary>
        ///  Used To Store Parcel Grid Column
        /// </summary>
        private int parcelColumnId;

        /// <summary>
        /// currentParcelRowId
        /// </summary>
        private int currentParcelRowId;

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private int ownerId;

        /// <summary>
        /// Flag to identify parcel number field changes
        /// </summary>
        private bool isParcelEdited = false;
        #endregion

        #region AmountDue
        /// <summary>
        /// exciseTaxAffDvtAmountDueDataset store 
        /// </summary>
        private F15010ExciseAffidavitData exciseTaxAffDvtAmountDueDataset = new F15010ExciseAffidavitData();

        /// <summary>
        /// to check amount due dataset is valid  or not
        /// </summary>
        private bool validAmountDueDataset;

        #endregion

        #region Affdvt

        /// <summary>
        /// Used to store the Use Code value for the save method
        /// </summary>
        private string useCode;

        /// <summary>
        /// Used to store the affDvitUseCode
        /// </summary>
        private string affDvitUseCode;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private int affdvtButtonOperation;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private bool affdvtRemove;

        #endregion

        #region Mobile Home

        /// <summary>
        /// Used to store the the row count of the mobile home grid
        /// </summary>
        private int currentMobileHomeGridRowCout;

        /// <summary>
        /// Used to store the current row index of Mobile home grid
        /// </summary>
        private int currentMobileHomeGridRowIndex;

        /// <summary>
        /// currentMobileHomeRollYear
        /// </summary>
        private int currentMobileHomeRollYear;

        /// <summary>
        /// Used to store the avoidMobileHomeGridRowEnter
        /// </summary>
        private bool avoidMobileHomeGridRowEnter;

        #endregion Mobile Home

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15010"/> class.
        /// </summary>
        public F15010()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F15010(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.currentAffidavitStatementId = keyID;
            ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
            this.currentTempStatementKeyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;

            this.formHeight = this.Height;
            this.mainPanelHeight = MainPanel.Height;
            this.GeneralPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GeneralPictureBox.Height, this.GeneralPictureBox.Width, "General", 77, 97, 133);
            this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, "Parties", 174, 150, 94);
            this.ParcelsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelsPictureBox.Height, this.ParcelsPictureBox.Width, "Parcels", 77, 97, 133);
            this.AffidavitDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AffidavitDetailPictureBox.Height, this.AffidavitDetailPictureBox.Width, "Affidavit Details", 174, 150, 94);
            this.AmountDuePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AmountDuePictureBox.Height, this.AmountDuePictureBox.Width, "Amount Due", 77, 97, 133);
            this.SupplementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SupplementPictureBox.Height, this.SupplementPictureBox.Width, "Supplement", 174, 150, 94);
            this.MobileHomePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MobileHomePictureBox.Height, this.MobileHomePictureBox.Width, "Mobile Info", 77, 97, 133);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassId">ifeatureClassId</param>
        public F15010(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassId)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.currentAffidavitStatementId = keyID;
            ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
            this.currentTempStatementKeyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;

            this.formHeight = this.Height;
            this.mainPanelHeight = MainPanel.Height;
            this.GeneralPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GeneralPictureBox.Height, this.GeneralPictureBox.Width, "General", 77, 97, 133);
            this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, "Parties", 174, 150, 94);
            this.ParcelsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelsPictureBox.Height, this.ParcelsPictureBox.Width, "Parcels", 77, 97, 133);
            this.AffidavitDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AffidavitDetailPictureBox.Height, this.AffidavitDetailPictureBox.Width, "Affidavit Details", 174, 150, 94);
            this.AmountDuePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AmountDuePictureBox.Height, this.AmountDuePictureBox.Width, "Amount Due", 77, 97, 133);
            this.SupplementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SupplementPictureBox.Height, this.SupplementPictureBox.Width, "Supplement", 174, 150, 94);
            this.MobileHomePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MobileHomePictureBox.Height, this.MobileHomePictureBox.Width, "Mobile Info", 77, 97, 133);
            ////int.TryParse(featureClassId,out this.receiptNo);
            this.receiptNo = featureClassId;

        }

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion Event Publication

        #region enumeratorButtonOperation For Parties

        /// <summary>
        /// Button Functionality
        /// </summary>
        public enum ButtonOperation
        {
            /// <summary>
            /// Empty = 0.
            /// </summary>
            Empty = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New = 1,

            /// <summary>
            /// Save = 2.
            /// </summary>
            Update = 2,

            /// <summary>
            /// Delete = 3.
            /// </summary>
            Remove = 3,

            /// <summary>
            /// Cancel = 4.
            /// </summary>
            Cancel = 4,

            /// <summary>
            /// For HeaderPartNew
            /// </summary>
            HeaderPartNew = 5,

            /// <summary>
            /// For HeaderPartUpdate
            /// </summary>
            HeaderPartUpdate = 6,

            /// <summary>
            /// For ReceiptidNotExist
            /// </summary>
            ReceiptidNotExist = 9,

            /// <summary>
            /// For NoRecordFound
            /// </summary>
            NoRecordFound = 10,

            /// <summary>
            /// For Permission
            /// </summary>
            NoPermission = 11
        }
        #endregion enumeratorButtonOperation For Parties

        #region Property

        /// <summary>
        /// For F15010Control
        /// </summary>
        [CreateNew]
        public F15010Controller Form15010Control
        {
            get { return this.form15010Control as F15010Controller; }
            set { this.form15010Control = value; }
        }

        #endregion Property

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
                this.receiptNo = 0;
                this.receiptId = 0;
                if (this.slicePermissionField.newPermission)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.isDistrictChanged = false;
                    this.NewExciseAffDvt();
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.ToDisableCalAmountDuePartOnReceiptID(true);
                    ////this.GeneralHeaderPaymentDateTextBox.Focus();
                    ////to disable on new operation 
                    this.GeneralDistrictLinkLablePanel.Enabled = true;
                    this.GeneralHeaderPaymentDateTextBox.Focus();
                    this.TreasurerStatusButton.Enabled = true;
                    this.ReceiptFormButton.Enabled = false;
                    this.AssessorStatusButton.Enabled = true;
                    this.DORAmendButton.Enabled = false;
                }
                else
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    ////When permission does not exists this grid is not editable
                    this.MobileHomeGridView.IsEditableGrid = false;
                    this.DORAmendButton.Enabled = false;
                    this.NewExciseAffDvt();
                    this.LockControls(false);
                }
                ////Added  by Malliga on 3/4/2008
                ////if (!string.IsNullOrEmpty(this.receiptdate))
                ////{
                ////    this.GeneralHeaderPaymentDateTextBox.Text = this.receiptdate;
                ////}
                ////else
                ////{
                ////    this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
                ////}

                ////if (!string.IsNullOrEmpty(this.interestdate))
                ////{
                ////    this.GeneralHeaderFormDateTextBox.Text = this.interestdate;
                ////}
                ////else
                ////{
                ////    this.GeneralHeaderFormDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
                ////}
                ////For Exemption TextBox Color
                ////if (string.IsNullOrEmpty(this.AffDvtExemptionCodeTextBox.Text))
                ////{
                ////    this.AffDvtExemptionCodeTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                ////}
                ////else
                ////{
                ////    this.AffDvtExemptionCodeTextBox.BackColor = Color.White;
                ////}
                ////~~~~~~~~~~~~~~~~~~~~~~~~~~
                ////Address field auto complete functionality
                if (this.PartiesAddress1TextBox.Text == "")
                {
                    this.SetAutoCompleteForAddress1();
                }
                else
                {
                    this.PartiesAddress1TextBox.AutoCompleteCustomSource = null;
                }

                if (this.PartiesAddress2TextBox.Text == "")
                {
                    this.SetAutoCompleteForAddress2();
                }
                else
                {
                    this.PartiesAddress2TextBox.AutoCompleteCustomSource = null;
                }

                if (this.exemptionValidateonoff && this.AffDvtExemptionCodeTextBox.Text != "")
                {
                    TerraScanTextBox sourceTextBox = this.AffDvtExemptionCodeTextBox as TerraScanTextBox;
                    this.ChangeExemptionBackGround(sourceTextBox);
                }
                else
                {
                    this.AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                this.PanelOne.Focus();
                ////to Clear the all the controls
                this.ClearAffidavitPartControls();
                this.SetGeneralHeaderFieldNewMode();
                this.ClearAmountDueControls();
                this.ClearSupplimentControl();
                ////this.partiesHeaderkeyPressed = false;
                ////this.parcelHeaderKeyPressed = false;

                this.DisablePartiesHeaderPanels(true);
                this.DisableParcelHeaderPanels(true);
                this.partiesHeaderkeyPressed = false;
                this.parcelHeaderKeyPressed = false;
                this.LockControls(true);
                ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
                this.currentAffidavitStatementId = this.currentTempStatementKeyId;

                this.LoadEntireAffidavit();
                ////this.partiesHeaderkeyPressed = false;
                ////this.parcelHeaderKeyPressed = false;

                this.pageMode = TerraScanCommon.PageModeTypes.View;

                this.ForTreasurerAndAssessorStatusButton();
                this.partiesHeaderkeyPressed = false;
                this.parcelHeaderKeyPressed = false;
                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                ////Added  by Malliga on 3/4/2008
                ////if (!string.IsNullOrEmpty(this.receiptdate))
                ////{
                ////    this.GeneralHeaderPaymentDateTextBox.Text = this.receiptdate;
                ////}
                ////else
                ////{
                ////    this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
                ////}

                ////if (!string.IsNullOrEmpty(this.interestdate))
                ////{
                ////    this.GeneralHeaderFormDateTextBox.Text = this.interestdate;
                ////}
                ////else
                ////{
                ////    this.GeneralHeaderFormDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
                ////}

                this.SetFocusFirstEditableControls();
                if (this.exemptionValidateonoff && this.AffDvtExemptionCodeTextBox.Text != "")
                {
                    TerraScanTextBox sourceTextBox = this.AffDvtExemptionCodeTextBox as TerraScanTextBox;
                    this.ChangeExemptionBackGround(sourceTextBox);
                }
                else
                {
                    this.AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                }

                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                {
                    if (this.isDistrictEditable > 0)
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }
                    else
                    {
                        if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !this.submittedBy)
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = true;
                        }
                        else
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = false;
                        }
                    }
                }
                else
                {
                    this.GeneralDistrictLinkLablePanel.Enabled = false;
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
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
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
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
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.slicePermissionField.editPermission)
                    {
                        this.SaveAffDvt();
                        this.flagSave = true;
                        ////this.SetCaluDueButtonsBGColor();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                else
                {
                    this.LoadEntireAffidavit();
                    this.LockControls(true);
                    this.ControlLock(false);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }

                //this.receiptdate = this.GeneralHeaderPaymentDateTextBox.Text;
                //this.interestdate = this.GeneralHeaderFormDateTextBox.Text;
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.ForTreasurerAndAssessorStatusButton();

                    if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
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
                    // Coding Added for the issue 3881
                    this.SetFocusFirstEditableControls();
                    // Ends here 3881


                    if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                    {
                        if (this.isDistrictEditable > 0)
                        {

                            this.GeneralDistrictLinkLablePanel.Enabled = false;
                        }
                        else
                        {
                            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !this.submittedBy)
                            {
                                this.GeneralDistrictLinkLablePanel.Enabled = true;
                            }
                            else
                            {
                                this.GeneralDistrictLinkLablePanel.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }


                }
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.slicePermissionField.deletePermission)
                {
                    if (this.DeleteExciseAffdvt())
                    {
                        SliceFormCloseAlert sliceFormCloseAlert;
                        sliceFormCloseAlert.FormNo = this.masterFormNo;
                        sliceFormCloseAlert.FlagFormClose = false;
                        this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                    }
                    else
                    {
                        // Dont allow to remove keyid from QE Grid
                        this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                    this.formLoaded = false;
                    this.parcelHeaderKeyPressed = false;
                    this.currentAffidavitStatementId = eventArgs.Data.SelectedKeyId;

                    ////to clear statment number for quickfind invalid ID Bug#4497
                    if (this.currentAffidavitStatementId < 0)
                    {
                        this.flagLoadOnProcess = true;
                        this.StatementNumberTextBox.Text = string.Empty;
                        this.StatementNumberTextBox.BackColor = Color.FromArgb(255, 255, 255);
                        this.PanelStatementNumber.BackColor = Color.FromArgb(255, 255, 255);
                        this.flagLoadOnProcess = false;
                    }

                    ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
                    this.currentTempStatementKeyId = eventArgs.Data.SelectedKeyId;
                    this.LoadEntireAffidavit();

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.ForTreasurerAndAssessorStatusButton();
                    this.formLoaded = true;

                    if (this.isDistrictEditable > 0)
                    {

                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }
                    else
                    {
                        if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !this.submittedBy)
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = true;
                        }
                        else
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = false;
                        }
                    }

                    this.SetFocusFirstEditableControls();
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// D35000_s the F35000_ parcel changed value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D11011_F15011_ReceiptId, ThreadOption.UserInterface)]
        public void D11011_F15011_ReceiptId(object sender, DataEventArgs<int[]> eventArgs)
        {
            if (eventArgs.Data[0] == this.currentTempStatementKeyId)
            {
                this.LoadEntireAffidavit();

                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.ForTreasurerAndAssessorStatusButton();
                this.formLoaded = true;

                if (this.isDistrictEditable > 0)
                {

                    this.GeneralDistrictLinkLablePanel.Enabled = false;
                }
                else
                {
                    if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !this.submittedBy)
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = true;
                    }
                    else
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }
                }

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

        #region Static Methods

        #region Affidavit Methods

        /// <summary>
        /// Inits the combo box values.
        /// </summary>
        /// <param name="initComboBox">The init combo box.</param>
        private static void InitComboBoxValues(TerraScan.UI.Controls.TerraScanComboBox initComboBox)
        {
            initComboBox.Items.Clear();
            initComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            initComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
        }

        /// <summary>
        /// Inits the combo box values.
        /// </summary>
        /// <param name="initComboBox">The init combo box.</param>
        private static void InitContinuanceSpaceComboBoxValues(TerraScan.UI.Controls.TerraScanComboBox initComboBox)
        {
            initComboBox.Items.Clear();
            initComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            initComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
        }

        #endregion Affidavit Methods

        #region Amount Due Methods

        /// <summary>
        /// Checks the valid data set.
        /// </summary>
        /// <param name="checkDataSet">The check data set.</param>
        /// <returns>If its Valid dataset return true else false</returns>
        private static bool CheckValidDataSet(DataSet checkDataSet)
        {
            if (checkDataSet != null)
            {
                if (checkDataSet.Tables.Count > 0)
                {
                    return true;
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

        #endregion Amount Due Methods

        /// <summary>
        /// Used to Set the correct value to the ComboBox
        /// </summary>
        /// <param name="setComboBox">The set combo box.</param>
        /// <param name="comboxString">The combox string.</param>
        private static void SetComboboxValue(TerraScan.UI.Controls.TerraScanComboBox setComboBox, string comboxString)
        {
            int correctIndex = 0;
            comboxString = comboxString.ToUpperInvariant();
            if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue")) == 0 || String.Compare(comboxString, SharedFunctions.GetResourceString("TRUEValue")) == 0)
            {
                if (String.Compare(comboxString, SharedFunctions.GetResourceString("FALSEValue")) == 0)
                {
                    correctIndex = 0;
                }
                else
                {
                    correctIndex = 1;
                }
            }
            else
            {
                correctIndex = setComboBox.FindString(comboxString);
            }

            setComboBox.SelectedIndex = correctIndex;
        }

        #endregion Static Methods

        #region Coding For Parties

        #region Parties Methods

        /// <summary>
        /// To clear Parties Part Controls
        /// </summary>
        private void ClearPartiesPartControls()
        {
            this.GeneralPartiesNameTextBox.Text = string.Empty;
            this.PartiesPhoneNoTextBox.Text = string.Empty;
            this.PartiesOwnerTextBox.Text = string.Empty;
            this.PartiesAddress1TextBox.Text = string.Empty;
            this.PartiesAddress2TextBox.Text = string.Empty;
            this.PartiesCityTextBox.Text = string.Empty;
            this.PartiesStateTextBox.Text = string.Empty;
            this.PartiesZipCodeTextBox.Text = string.Empty;
            this.PartiesCountryTextBox.Text = string.Empty;

            ////to empty the combo boxs
            this.PartiesTypeTextBox.Text = string.Empty;
            this.PartiesTypeComboBox.Text = string.Empty;
        }

        /// <summary>
        /// Disables the parties header.
        /// </summary>
        /// <param name="statusOfPartiesCntrl">if set to <c>true</c> [status of parties CNTRL].</param>
        private void SetEnableStatusforPartiesControls(bool statusOfPartiesCntrl)
        {
            this.GeneralPartiesNameTextBox.Enabled = statusOfPartiesCntrl;
            this.StatusLinkLabel.Enabled = statusOfPartiesCntrl;
            //this.GeneralPartiesNameTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesPhoneNoTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesPhoneNoTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesTypeComboBox.Enabled = statusOfPartiesCntrl;
            this.PartiesOwnerTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesOwnerTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesAddress1TextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesAddress1TextBox.BackColor = System.Drawing.Color.White;
            this.PartiesAddress2TextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesAddress2TextBox.BackColor = System.Drawing.Color.White;
            this.PartiesCityTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesCityTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesStateTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesStateTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesZipCodeTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesZipCodeTextBox.BackColor = System.Drawing.Color.White;
            this.PartiesCountryTextBox.Enabled = statusOfPartiesCntrl;
            this.PartiesCountryTextBox.BackColor = System.Drawing.Color.White;
            // this.ParcelPictureBox.Enabled = statusOfPartiesCntrl;
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomisePartiesDataGridView()
        {
            DataGridViewColumnCollection columns = this.PartiesDataGridView.Columns;
            columns["PartyName"].DataPropertyName = "Name";
            columns["StatusVal"].DataPropertyName = "Status";
            columns["Address"].DataPropertyName = "Address";
            columns["City"].DataPropertyName = "City";
            columns["IndividualType"].DataPropertyName = "IndividualType";
            columns["Phone"].DataPropertyName = "Phone";
            columns["PercentOwner"].DataPropertyName = "PercentOwner";
            columns["State"].DataPropertyName = "State";
            columns["PercentOwner"].DataPropertyName = "PercentOwner";
            columns["Zip"].DataPropertyName = "Zip";
            columns["Country"].DataPropertyName = "Country";
            columns["individualID"].DataPropertyName = "individualID";
            columns["individualTypeId"].DataPropertyName = "individualTypeId";
            columns["OwnerId1"].DataPropertyName = "OwnerId";
            columns["StatementId"].DataPropertyName = "StatementId";
            columns["Address1"].DataPropertyName = "Address1";
            columns["Address2"].DataPropertyName = "Address2";
            columns["IndividualAutoID"].DataPropertyName = "IndividualAutoID";

            columns["PartyName"].DisplayIndex = 0;
            columns["StatusVal"].DisplayIndex = 1;
            columns["Address"].DisplayIndex = 2;
            columns["City"].DisplayIndex = 3;
            columns["IndividualType"].DisplayIndex = 4;
            columns["Phone"].DisplayIndex = 5;
            columns["PercentOwner"].DisplayIndex = 6;
            columns["State"].DisplayIndex = 7;
            columns["Zip"].DisplayIndex = 8;
            columns["Country"].DisplayIndex = 9;
            columns["individualID"].DisplayIndex = 10;
            columns["individualTypeId"].DisplayIndex = 12;
            columns["StatementId"].DisplayIndex = 13;
            columns["Address1"].DisplayIndex = 14;
            columns["Address2"].DisplayIndex = 15;
            columns["StatementId"].DisplayIndex = 16;
            columns["IndividualAutoID"].DisplayIndex = 17;
        }

        /// <summary>
        /// Sets the parties text.
        /// </summary>
        /// <param name="partiesRowId">The parties row id.</param>
        private void SetPartiesText(int partiesRowId)
        {
            this.StatusLinkLabel.Visible = false;
            PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
            if (partiesRowId >= 0)
            {
                this.GeneralPartiesNameTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["PartyName"].Value.ToString();
                this.PartiesPhoneNoTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Phone"].Value.ToString();
                SetComboboxValue(this.PartiesTypeComboBox, this.PartiesDataGridView.Rows[partiesRowId].Cells["IndividualType"].Value.ToString());

                this.PartiesOwnerTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["PercentOwner"].Value.ToString();
                if (this.PartiesOwnerTextBox.Text == "0")
                {
                    this.PartiesOwnerTextBox.Text = "";
                }

                this.PartiesAddress1TextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Address1"].Value.ToString();
                this.PartiesAddress2TextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Address2"].Value.ToString();
                this.PartiesCityTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["City"].Value.ToString();
                this.PartiesStateTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["State"].Value.ToString();
                this.PartiesZipCodeTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Zip"].Value.ToString();
                this.PartiesCountryTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Country"].Value.ToString();
                if (!string.IsNullOrEmpty(this.PartiesDataGridView.Rows[partiesRowId].Cells["OwnerID1"].Value.ToString()))
                {
                    this.ownerId = int.Parse(this.PartiesDataGridView.Rows[partiesRowId].Cells["OwnerID1"].Value.ToString());
                }
                else
                {
                    this.ownerId = 0;
                }

                int ownerLowVal = 0;
                int ownerHighVal = 0;

                this.getOwnerDataset.Clear();
                getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(this.ownerId);

                try
                {
                    int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                    int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);
                    if (ownerLowVal > 0)
                    {
                        StatusLinkLabel.Visible = true;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                    }
                    if (ownerHighVal > 0)
                    {
                        StatusLinkLabel.Visible = true;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                    }

                    if (((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                    {
                        StatusLinkLabel.Visible = false;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    }
                }
                catch (Exception exp)
                {
                }
            }
        }

        /// <summary>
        /// To Set the Parties Header Part on New button click and when parties type combo box is selected
        /// </summary>
        private void SetPartiesHeaderPartOnNew()
        {
            int parcelTypeAutoMaxId = 0;
            string findPartiesType;
            string findNewPartiesType;

            ////here the Parties header part is load with last added/modified parties type when parties type combo box is selected
            if (this.isnewButtonClicked && this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                if (this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count > 0 && !string.IsNullOrEmpty(this.PartiesTypeComboBox.Text.Trim()))
                {
                    findPartiesType = "IndividualTypeID = " + this.PartiesTypeComboBox.SelectedValue.ToString();

                    ////this is used to get the Max value for IndividualAutoID for the particular Parties Type
                    int.TryParse(this.exciseTaxAffidavitDataSet.PartiesHeader.Compute("MAX (IndividualAutoID)", findPartiesType).ToString(), out parcelTypeAutoMaxId);

                    ////this condiiton is used to check whether the parties datatable is modified
                    if (parcelTypeAutoMaxId > 0)
                    {
                        findNewPartiesType = findPartiesType + " AND IndividualAutoID = " + parcelTypeAutoMaxId;
                        DataRow[] dr = this.exciseTaxAffidavitDataSet.PartiesHeader.Select(findNewPartiesType);

                        if (dr.Length > 0)
                        {
                            this.avoidPartiesTypeComboSelectionChangedEvent = true;

                            this.GeneralPartiesNameTextBox.Text = string.Empty;
                            /////this.GeneralPartiesNameTextBox.Text = dr[0]["Name"].ToString();
                            this.PartiesPhoneNoTextBox.Text = dr[0]["Phone"].ToString();
                            SetComboboxValue(this.PartiesTypeComboBox, dr[0]["IndividualType"].ToString());
                            this.PartiesOwnerTextBox.Text = "100";
                            ////this.PartiesOwnerTextBox.Text = dr[0]["PercentOwner"].ToString();
                            this.PartiesAddress1TextBox.Text = dr[0]["Address1"].ToString();
                            this.PartiesAddress2TextBox.Text = dr[0]["Address2"].ToString();
                            this.PartiesCityTextBox.Text = dr[0]["City"].ToString();
                            this.PartiesStateTextBox.Text = dr[0]["State"].ToString();
                            this.PartiesZipCodeTextBox.Text = dr[0]["Zip"].ToString();
                            this.PartiesCountryTextBox.Text = dr[0]["Country"].ToString();
                            this.avoidPartiesTypeComboSelectionChangedEvent = false;
                        }
                    }
                    else
                    {
                        this.avoidPartiesTypeComboSelectionChangedEvent = true;

                        this.GeneralPartiesNameTextBox.Text = string.Empty;
                        this.PartiesPhoneNoTextBox.Text = string.Empty;
                        this.PartiesOwnerTextBox.Text = "100";
                        this.PartiesAddress1TextBox.Text = string.Empty;
                        this.PartiesAddress2TextBox.Text = string.Empty;
                        this.PartiesCityTextBox.Text = string.Empty;
                        this.PartiesStateTextBox.Text = string.Empty;
                        this.PartiesZipCodeTextBox.Text = string.Empty;
                        this.PartiesCountryTextBox.Text = string.Empty;

                        this.avoidPartiesTypeComboSelectionChangedEvent = false;
                    }
                }
            }
        }

        /// <summary>
        /// Inits the individual type combo box.
        /// </summary>
        private void InitIndividualTypeComboBox()
        {
            this.PartiesTypeComboBox.DataSource = this.exciseIndividualtype.ExciseIndividualType;
            this.PartiesTypeComboBox.ValueMember = this.exciseIndividualtype.ExciseIndividualType.IndividualTypeIDColumn.ColumnName; ///// "IndividualTypeID";
            this.PartiesTypeComboBox.DisplayMember = this.exciseIndividualtype.ExciseIndividualType.IndividualTypeColumn.ColumnName;   /////"IndividualType";
        }

        /// <summary>
        /// Updates the local parties data set.
        /// </summary>
        /// <param name="partiesRowId">The parties row id.</param>
        private void UpdateLocalPartiesDataSet(int partiesRowId)
        {
            int updatePartiesTypeAutoID = 0;
            int ownerLowVal = 0;
            int ownerHighVal = 0;
            ///// if its NewOpeation Then Save 

            if (this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                this.CreateNewPartiesRow();
                this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
            }
            else
            {
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.NameColumn] = this.GeneralPartiesNameTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.PhoneColumn] = this.PartiesPhoneNoTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeColumn] = this.PartiesTypeComboBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeIDColumn] = this.PartiesTypeComboBox.SelectedValue;
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.OwnerIDColumn.ColumnName] = this.ownerId;

                int newIdvalue = this.ownerId;
                this.getOwnerDataset.Clear();
                getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(newIdvalue);

                try
                {
                    int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                    int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);

                    if (ownerLowVal > 0)
                    {
                        StatusLinkLabel.Visible = true;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                    }
                    if (ownerHighVal > 0)
                    {
                        StatusLinkLabel.Visible = true;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                    }
                    if (((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                    {
                        StatusLinkLabel.Visible = false;
                        StatusLinkLabel.Text = "Status";
                        StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                        PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                        GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    }
                }
                catch (Exception exp)
                {
                }

                if (ownerLowVal > 0 || ownerHighVal > 0)
                {
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.StatusColumn.ColumnName] = "Status";
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.StatusColumn.ColumnName] = "";
                }

                //// if (Convert.ToInt32((this.PartiesTypeComboBox.SelectedValue.ToString()))  <= 1)
                if (this.PartiesOwnerTextBox.Text != "")
                {
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.PercentOwnerColumn] = this.PartiesOwnerTextBox.Text.Trim().Replace("%", "");
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.PercentOwnerColumn] = DBNull.Value;
                }

                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.Address1Column] = this.PartiesAddress1TextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.Address2Column] = this.PartiesAddress2TextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.AddressColumn] = this.PartiesAddress1TextBox.Text.Trim() + " " + this.PartiesAddress2TextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.CityColumn] = this.PartiesCityTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.StateColumn] = this.PartiesStateTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.ZipColumn] = this.PartiesZipCodeTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.CountryColumn] = this.PartiesCountryTextBox.Text.Trim();

                ////to incremment the IndividualAutoID Column
                int.TryParse(this.exciseTaxAffidavitDataSet.PartiesHeader.Compute("MAX (IndividualAutoID)", "IndividualID > 0").ToString(), out updatePartiesTypeAutoID);

                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualAutoIDColumn.ColumnName] = (updatePartiesTypeAutoID + 1);

                this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
            }

            this.SetPartiesGrid();
            if (this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                if (this.partiesRowCount > 0)
                {
                    this.PartiesDataGridView.Enabled = true;
                    this.SetPartiesText(this.partiesRowCount - 1);
                    TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.partiesRowCount - 1);
                    this.tempRowIdParties = this.partiesRowCount - 1;
                }

                this.PartiesDataGridView.Focus();
            }
            else
            {
                if (this.selectedPartyGridRowId >= 0)
                {
                    TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.selectedPartyGridRowId);
                    this.PartiesDataGridView.Focus();
                    this.tempRowIdParties = this.selectedPartyGridRowId;
                    if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                    {
                        this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    }
                }
            }

            this.SetPartiesGridButtons(ButtonOperation.Empty);
            //// MAkes The Affidavit Button To UpdateMode
            this.SetAffDvtButton(ButtonOperation.Update);
            this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
            this.partiesHeaderkeyPressed = false;
        }

        /// <summary>
        /// Saves the parties header.
        /// </summary>
        private void SavePartiesHeader()
        {
            if (this.CheckMandatoryFieldsParty())
            {
                this.UpdateLocalPartiesDataSet(this.selectedPartyGridRowId);
                this.SetPartiesGridButtons(ButtonOperation.Empty);
                this.SetAffDvtButton(ButtonOperation.HeaderPartUpdate);
                this.partiesHeaderkeyPressed = false;
                this.PartiesDataGridView.Focus();
                ////to enable the save and cancel button in the master form
                this.ToEnableEditButtonInMasterForm();
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks the mandatory fields party.
        /// </summary>
        /// <returns>True if All Field are filled else false</returns>
        private bool CheckMandatoryFieldsParty()
        {
            if (!string.IsNullOrEmpty(this.GeneralPartiesNameTextBox.Text.Trim()) && this.PartiesTypeComboBox.SelectedIndex >= 0)
            {
                this.partyHeaderValidData = true;
            }
            else
            {
                this.partyHeaderValidData = false;
                return this.partyHeaderValidData;
            }

            if (this.PartiesTypeComboBox.SelectedIndex <= 1)
            {
                if (this.PartiesOwnerTextBox.Text == "")
                {
                    this.partyHeaderValidData = false;
                    return this.partyHeaderValidData;
                }
                else
                {
                    this.partyHeaderValidData = true;
                }
            }

            return this.partyHeaderValidData;
        }

        /// <summary>
        /// Sets the parties grid.
        /// </summary>
        private void SetPartiesGrid()
        {
            int partiesTypeUpdateId = 0;

            int ownerLowVal = 0;
            int ownerHighVal = 0;

            ////this.PartiesDataGridView.DataSource = null;
            this.CustomisePartiesDataGridView();
            this.partiesRowCount = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count;
            //PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
            if (partiesRowCount > 0)
            {
                for (int i = 0; i < partiesRowCount; i++)
                {
                    int ownerCurrentId = Convert.ToInt32(this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[i]["OwnerID"].ToString());
                    this.getOwnerDataset.Clear();
                    getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(ownerCurrentId);
                    var ownerStatusRow = exciseTaxAffidavitDataSet.PartiesHeader[i];

                    //foreach (var ownerStatusRow in exciseTaxAffidavitDataSet.PartiesHeader)
                    //{
                    try
                    {
                        int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                        int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);
                        if (ownerLowVal > 0)
                        {
                            //StatusLinkLabel.Visible = true;
                            //StatusLinkLabel.Text = "Status";
                            //StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                            //PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                            //GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                        }
                        if (ownerHighVal > 0)
                        {
                            //StatusLinkLabel.Visible = true;
                            //StatusLinkLabel.Text = "Status";
                            //StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                            //PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                            //GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                        }
                        if (((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                        {
                            //StatusLinkLabel.Visible = false;
                            //StatusLinkLabel.Text = "Status";
                            //StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                            //PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                            //GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                        }
                    }
                    catch (Exception exp)
                    {
                    }



                    if (ownerLowVal > 0 || ownerHighVal > 0)
                    {
                        ownerStatusRow.Status = "Status";
                    }
                    else
                    {
                        ownerStatusRow.Status = "";
                    }
                    //}
                }

            }
            this.partiesHeaderkeyPressed = false;

            this.PartiesDataGridView.DataSource = this.exciseTaxAffidavitDataSet.PartiesHeader.Copy().DefaultView;

            if (this.partiesRowCount > 0)
            {
                int.TryParse(this.exciseTaxAffidavitDataSet.PartiesHeader.Compute("MAX (IndividualAutoID)", "IndividualID > 0").ToString(), out partiesTypeUpdateId);

                if (partiesTypeUpdateId == 0)
                {
                    for (int i = 0; i < this.partiesRowCount; i++)
                    {
                        ////to incremment the IndividualAutoID Column
                        this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[i][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualAutoIDColumn.ColumnName] = (i + 1);
                        this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    }
                }

                if (this.receiptIDExist)
                {
                    this.PartiesDataGridView.Enabled = true;
                }

                this.SetEnableStatusforPartiesControls(true);
            }
            else
            {
                this.PartiesDataGridView.Rows[Convert.ToInt32(0)].Selected = false;
                //// Disable
                this.SetEnableStatusforPartiesControls(false);
                this.ClearPartiesHeader();
                this.DisablePartiesHeaderPanels(false);
                this.PartiesDataGridView.CurrentCell = null;
            }

            if (this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count > this.PartiesDataGridView.NumRowsVisible)
            {
                this.PartiesHeaderVscrollBar.Enabled = true;
                this.PartiesHeaderVscrollBar.Visible = false;
                this.PartiesHeaderVscrollBar.BringToFront();
            }
            else
            {
                this.PartiesHeaderVscrollBar.Enabled = false;
                this.PartiesHeaderVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Creates the new parties row.
        /// </summary>
        private void CreateNewPartiesRow()
        {
            int toincrementparceltyeId = 0;
            int ownerLowVal = 0;
            int ownerHighVal = 0;

            String tempKeyId = string.Empty;
            DataView maxView = new DataView(this.exciseTaxAffidavitDataSet.PartiesHeader);
            if (maxView.Count > 0)
            {
                //// dv.RowFilter = "PostalCode = Max(PostalCode)"; 
                maxView.RowFilter = "IndividualID = Max(IndividualID)";
                if (maxView.Count > 0)
                {
                    tempKeyId = maxView[0]["IndividualID"].ToString();
                }

                if (!string.IsNullOrEmpty(tempKeyId))
                {
                    this.keyPartiesId = int.Parse(tempKeyId) + 1;
                }
                else
                {
                    this.keyPartiesId = 0;
                }
            }
            else
            {
                this.keyPartiesId = 1;
            }

            DataRow partiesTempRow = this.exciseTaxAffidavitDataSet.PartiesHeader.NewRow();
            this.exciseTaxAffidavitDataSet.PartiesHeader.Columns[this.exciseTaxAffidavitDataSet.PartiesHeader.StatementIDColumn.ColumnName].AllowDBNull = true;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.StatementIDColumn.ColumnName] = 0;
            //// partiesTempRow["IndividualID"] = this.keyPartiesId;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualIDColumn.ColumnName] = this.keyPartiesId;

            //// partiesTempRow["Name"] = this.GeneralPartiesNameTextBox.Text;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.NameColumn.ColumnName] = this.GeneralPartiesNameTextBox.Text.Trim();

            //// partiesTempRow["Phone"] = this.PartiesPhoneNoTextBox.Text;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.PhoneColumn.ColumnName] = this.PartiesPhoneNoTextBox.Text.Trim();

            //// partiesTempRow["IndividualType"] = this.PartiesTypeComboBox.Text ;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeColumn.ColumnName] = this.PartiesTypeComboBox.Text.Trim();

            //// partiesTempRow["IndividualTypeID"] = this.PartiesTypeComboBox.SelectedValue;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeIDColumn.ColumnName] = this.PartiesTypeComboBox.SelectedValue;

            //// partiesTempRow["PercentOwner"] = this.PartiesOwnerTextBox.Text.Trim().Replace("%", "");
            ////if (this.PartiesTypeComboBox.SelectedIndex <= 1)
            ////{
            ////    if (this.PartiesOwnerTextBox.Text == "")
            ////    {
            ////        MessageBox.Show("Percentage should not be empty");
            ////        return;
            ////    }
            ////}

            if (this.PartiesOwnerTextBox.Text != "")
            {
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.PercentOwnerColumn.ColumnName] = this.PartiesOwnerTextBox.Text.Trim().Replace("%", "");
            }

            //// partiesTempRow["OwnerId"] = DBNull.Value;

            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.OwnerIDColumn.ColumnName] = this.ownerId;

            //// partiesTempRow["Address"] = this.PartiesAddress1TextBox.Text;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.Address1Column.ColumnName] = this.PartiesAddress1TextBox.Text.Trim();

            ////partiesTempRow["Address"] = this.PartiesAddress2TextBox.Text;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.Address2Column.ColumnName] = this.PartiesAddress2TextBox.Text.Trim();

            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.AddressColumn.ColumnName] = this.PartiesAddress1TextBox.Text.Trim() + " " + this.PartiesAddress2TextBox.Text.Trim();

            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.CityColumn.ColumnName] = this.PartiesCityTextBox.Text.Trim();

            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.StateColumn.ColumnName] = this.PartiesStateTextBox.Text.Trim();

            ////partiesTempRow["Zip"] = this.PartiesZipCodeTextBox.Text;
            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.ZipColumn.ColumnName] = this.PartiesZipCodeTextBox.Text.Trim();

            partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.CountryColumn.ColumnName] = this.PartiesCountryTextBox.Text.Trim();
            ////partiesTempRow["Country"] = this.PartiesCountryTextBox.Text;

            int.TryParse(this.exciseTaxAffidavitDataSet.PartiesHeader.Compute("MAX (IndividualAutoID)", "IndividualID > 0").ToString(), out toincrementparceltyeId);

            if (toincrementparceltyeId == 0)
            {
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualAutoIDColumn.ColumnName] = (toincrementparceltyeId + 1);
            }
            else
            {
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualAutoIDColumn.ColumnName] = (toincrementparceltyeId + 1);
            }

            int tempownerIdval = this.ownerId;
            this.getOwnerDataset.Clear();
            getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(tempownerIdval);

            try
            {
                int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);
                if (ownerLowVal > 0)
                {
                    StatusLinkLabel.Visible = true;
                    StatusLinkLabel.Text = "Status";
                    StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                }
                if (ownerHighVal > 0)
                {
                    StatusLinkLabel.Visible = true;
                    StatusLinkLabel.Text = "Status";
                    StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                }
                if (((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                {
                    StatusLinkLabel.Visible = false;
                    StatusLinkLabel.Text = "Status";
                    StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            catch (Exception exp)
            {
            }

            if (ownerLowVal > 0 || ownerHighVal > 0)
            {
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.StatusColumn.ColumnName] = "Status";
            }
            else
            {
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.StatusColumn.ColumnName] = "";
            }

            this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Add(partiesTempRow);
            this.exciseTaxAffidavitDataSet.AcceptChanges();
            this.partyHeaderCount = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count;
            if (this.partyHeaderCount > 0)
            {
                this.selectedPartyGridRowId = this.partyHeaderCount - 1;
            }
            else
            {
                this.selectedPartyGridRowId = 0;
            }
        }

        /// <summary>
        /// Clears the parties header.
        /// </summary>
        private void ClearPartiesHeader()
        {
            this.GeneralPartiesNameTextBox.Text = string.Empty;
            this.PartiesPhoneNoTextBox.Text = string.Empty;
            this.PartiesAddress1TextBox.Text = string.Empty;
            this.PartiesAddress2TextBox.Text = string.Empty;
            this.PartiesCityTextBox.Text = string.Empty;
            this.PartiesStateTextBox.Text = string.Empty;
            this.PartiesZipCodeTextBox.Text = string.Empty;
            this.PartiesCountryTextBox.Text = string.Empty;
            this.PartiesOwnerTextBox.Text = string.Empty;
            this.StatusLinkLabel.Visible = false;
            this.PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
            this.GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Partieses the button opr no permission.
        /// </summary>
        private void PartiesButtonOprNoPermission()
        {
            this.NewPartiesButton.Enabled = false;
            this.UpdateParites.Enabled = false;
            this.RemovePartiesButton.Enabled = false;
            this.CancelPartiesButton.Enabled = false;
            this.PartiesDataGridView.Enabled = false;
            ////Disable
            this.SetEnableStatusforPartiesControls(false);
            if (this.receiptIDExist)
            {
                this.PartiesDataGridView.Enabled = false;
            }
            else
            {
                this.PartiesDataGridView.Enabled = true;
            }
        }

        /// <summary>
        /// Partieses the button opr no record found.
        /// </summary>
        private void PartiesButtonOprNoRecordFound()
        {
            this.NewPartiesButton.Enabled = false;
            this.UpdateParites.Enabled = false;
            this.RemovePartiesButton.Enabled = false;
            this.CancelPartiesButton.Enabled = false;
            this.PartiesDataGridView.Enabled = false;
            this.SetEnableStatusforPartiesControls(false);
            this.PartiesDataGridView.Enabled = false;
        }

        /// <summary>
        /// Partieses the button opr receipt not exist.
        /// </summary>
        private void PartiesButtonOprReceiptNotExist()
        {
            this.NewPartiesButton.Enabled = false;
            this.UpdateParites.Enabled = false;
            this.RemovePartiesButton.Enabled = false;
            this.CancelPartiesButton.Enabled = false;
            this.PartiesDataGridView.Enabled = false;
            ////Disable
            this.SetEnableStatusforPartiesControls(false);
            this.PartiesDataGridView.Enabled = true;
        }

        /// <summary>
        /// Partieses the button opr update.
        /// </summary>
        private void PartiesButtonOprUpdate()
        {
            this.NewPartiesButton.Enabled = false;
            this.UpdateParites.Enabled = true;
            this.RemovePartiesButton.Enabled = false;
            this.CancelPartiesButton.Enabled = true;
            this.PartiesDataGridView.Enabled = true;
        }

        /// <summary>
        /// Partieses the button opr cancel.
        /// </summary>
        private void PartiesButtonOprCancel()
        {
            this.NewPartiesButton.Enabled = true;
            this.UpdateParites.Enabled = false;
            this.RemovePartiesButton.Enabled = true;
            this.CancelPartiesButton.Enabled = false;
            this.PartiesDataGridView.Enabled = true;
        }

        /// <summary>
        /// Partieses the button opr new.
        /// </summary>
        private void PartiesButtonOprNew()
        {
            if (this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                this.NewPartiesButton.Enabled = false;
                this.UpdateParites.Enabled = true;
                this.RemovePartiesButton.Enabled = false;
                this.CancelPartiesButton.Enabled = true;
                this.PartiesDataGridView.Enabled = false;
            }
            else
            {
                this.NewPartiesButton.Enabled = true;
                this.UpdateParites.Enabled = false;
                this.RemovePartiesButton.Enabled = false;
                this.CancelPartiesButton.Enabled = false;
                this.PartiesDataGridView.Enabled = false;
            }
        }

        /// <summary>
        /// Partieses the button opr remove.
        /// </summary>
        private void PartiesButtonOprRemove()
        {
            if (this.partiesRowCount > 0)
            {
                this.RemovePartiesButton.Enabled = true;

                this.PartiesDataGridView.Enabled = true;
                this.SetEnableStatusforPartiesControls(true);
            }
            else
            {
                ////Disable
                this.SetEnableStatusforPartiesControls(false);
                this.CancelPartiesButton.Enabled = false;
                this.RemovePartiesButton.Enabled = false;
                this.PartiesDataGridView.Enabled = false;
            }

            this.NewPartiesButton.Enabled = true;
            this.UpdateParites.Enabled = false;
            this.CancelPartiesButton.Enabled = false;
        }

        /// <summary>
        /// Parties the button opr empty.
        /// </summary>
        private void PartyButtonOprEmpty()
        {
            if (this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (this.partiesRowCount > 0)
                {
                    this.RemovePartiesButton.Enabled = true;
                    this.PartiesDataGridView.Enabled = true;
                    if (!this.receiptIDExist)
                    {
                        this.SetEnableStatusforPartiesControls(true);
                    }
                }
                else
                {
                    ////Disable
                    this.SetEnableStatusforPartiesControls(false);
                    this.RemovePartiesButton.Enabled = false;
                    this.PartiesDataGridView.Enabled = false;
                }

                this.NewPartiesButton.Enabled = true;
                this.UpdateParites.Enabled = false;
                this.CancelPartiesButton.Enabled = false;
            }
            else if (this.PermissionFiled.newPermission)
            {
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    if (this.partiesRowCount > 0)
                    {
                        this.RemovePartiesButton.Enabled = true;
                        this.PartiesDataGridView.Enabled = true;
                        if (!this.receiptIDExist)
                        {
                            this.SetEnableStatusforPartiesControls(true);
                        }
                    }
                    else
                    {
                        ////Disable
                        this.SetEnableStatusforPartiesControls(false);
                        this.RemovePartiesButton.Enabled = false;
                        this.PartiesDataGridView.Enabled = false;
                    }

                    this.NewPartiesButton.Enabled = true;
                    this.UpdateParites.Enabled = false;
                    this.CancelPartiesButton.Enabled = false;
                }
                else
                {
                    ////Disable
                    this.RemovePartiesButton.Enabled = false;
                    this.PartiesDataGridView.Enabled = false;
                    this.SetEnableStatusforPartiesControls(false);
                    this.NewPartiesButton.Enabled = false;
                    this.UpdateParites.Enabled = false;
                    this.CancelPartiesButton.Enabled = false;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.partiesRowCount > 0)
                {
                    this.RemovePartiesButton.Enabled = true;
                    this.PartiesDataGridView.Enabled = true;

                    if (!this.receiptIDExist)
                    {
                        this.SetEnableStatusforPartiesControls(true);
                    }
                }
                else
                {
                    ////Disable
                    this.SetEnableStatusforPartiesControls(false);
                    this.RemovePartiesButton.Enabled = false;
                    this.PartiesDataGridView.Enabled = false;
                }

                this.NewPartiesButton.Enabled = true;
                this.UpdateParites.Enabled = false;
                this.CancelPartiesButton.Enabled = false;
            }
            else
            {
                this.NewPartiesButton.Enabled = false;
                this.UpdateParites.Enabled = false;
                this.CancelPartiesButton.Enabled = false;
                this.SetEnableStatusforPartiesControls(false);

                if (this.receiptIDExist)
                {
                    this.PartiesDataGridView.Enabled = false;
                }
                else
                {
                    this.PartiesDataGridView.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Sets the partie header to update mode.
        /// </summary>
        private void SetPartieHeaderToUpdateMode()
        {
            if ((this.PermissionFiled.editPermission || this.PermissionFiled.newPermission) || this.partyHeaderButtonOperation != (int)ButtonOperation.New)
            {
                if (!this.partiesHeaderkeyPressed && !this.partiesGridClick)
                {
                    this.partiesHeaderkeyPressed = true;
                    ////this.partiesGridClick = true;
                    this.partyHeaderButtonOperation = (int)ButtonOperation.Update;
                }

                this.SetPartiesGridButtons(ButtonOperation.Update);
            }
        }

        /// <summary>
        /// Owners the detail status.
        /// </summary>
        /// <returns>Checks OwnerDetails</returns>
        private bool OwnerDetailStatus()
        {
            if (!string.IsNullOrEmpty(this.GeneralPartiesNameTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PartiesAddress1TextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PartiesAddress2TextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PartiesCityTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PartiesStateTextBox.Text.Trim()) || !string.IsNullOrEmpty(this.PartiesZipCodeTextBox.Text.Trim()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Assigns the owner detail.
        /// </summary>
        private void AssignOwnerDetail()
        {
            if (this.ownerDetailDataSet != null)
            {
                if (this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows.Count > 0)
                {
                    this.GeneralPartiesNameTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                    this.PartiesPhoneNoTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.PhoneColumn].ToString();
                    this.PartiesAddress1TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString();
                    this.PartiesAddress2TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                    this.PartiesCityTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString();
                    this.PartiesStateTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                    this.PartiesZipCodeTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.ZipColumn].ToString();
                }
            }
        }

        #endregion Parties Methods

        #region Parties Events

        /// <summary>
        /// Handles the Click event of the GeneralPartiesNamePictureParcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPartiesNamePictureParcel_Click(object sender, EventArgs e)
        {
            try
            {
                //this.GeneralPartiesNameTextBox.BackColor = System.Drawing.Color.White;
                Form parcelF9101 = new Form();
                parcelF9101 = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form15010Control.WorkItem);

                if (parcelF9101 != null)
                {
                    if (parcelF9101.ShowDialog() == DialogResult.Yes)
                    {
                        int ownerLowVal = 0;
                        int ownerHighVal = 0;
                        ////to avoid the Parties Type Combo Selection Changed Event
                        this.avoidPartiesTypeComboSelectionChangedEvent = true;

                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));

                        getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(this.ownerId);

                        this.ownerDetailDataSet = this.form15010Control.WorkItem.F15010_GetOwnerDetails(this.ownerId); ;

                        if (this.OwnerDetailStatus())
                        {
                            this.AssignOwnerDetail();
                            try
                            {
                                int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                                int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);
                                if (ownerLowVal > 0)
                                {
                                    StatusLinkLabel.Visible = true;
                                    StatusLinkLabel.Text = "Status";
                                    StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                                }

                                else if (ownerHighVal > 0)
                                {
                                    StatusLinkLabel.Visible = true;
                                    StatusLinkLabel.Text = "Status";
                                    StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                                }

                                else //(((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                                {
                                    StatusLinkLabel.Visible = false;
                                    StatusLinkLabel.Text = "Status";
                                    StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                                    PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                                    GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);

                                }
                            }
                            catch (Exception exp)
                            {
                            }
                        }
                        else
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("AffidavitOwner"), SharedFunctions.GetResourceString("AffidavitOwnerHearder"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        if (this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                                        {
                                            this.partyHeaderButtonOperation = (int)ButtonOperation.Update;
                                        }

                                        this.partiesHeaderkeyPressed = true;
                                        this.SetPartiesGridButtons(ButtonOperation.Update);
                                        this.AssignOwnerDetail();
                                        try
                                        {

                                            int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                                            int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);
                                            if (ownerLowVal > 0)
                                            {
                                                StatusLinkLabel.Visible = true;
                                                StatusLinkLabel.Text = "Status";
                                                StatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                                                PartiesHeaderNamePanle.BackColor = Color.FromArgb(200, 214, 230);
                                                GeneralPartiesNameTextBox.BackColor = Color.FromArgb(200, 214, 230);
                                            }

                                            else if (ownerHighVal > 0)
                                            {
                                                StatusLinkLabel.Visible = true;
                                                StatusLinkLabel.Text = "Status";
                                                StatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                                                PartiesHeaderNamePanle.BackColor = Color.FromArgb(237, 205, 203);
                                                GeneralPartiesNameTextBox.BackColor = Color.FromArgb(237, 205, 203);
                                            }

                                            else //(((ownerLowVal == 0) && (ownerHighVal == 0)) || ((ownerLowVal > 0) && (ownerHighVal > 0)))
                                            {
                                                StatusLinkLabel.Visible = false;
                                                StatusLinkLabel.Text = "Status";
                                                StatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                                                PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                                                GeneralPartiesNameTextBox.BackColor = Color.FromArgb(255, 255, 255);
                                            }
                                        }
                                        catch (Exception exp)
                                        {
                                        }
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        if (this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                                        {
                                            this.partyHeaderButtonOperation = (int)ButtonOperation.Update;
                                        }

                                        this.partiesHeaderkeyPressed = true;
                                        this.SetPartiesGridButtons(ButtonOperation.Update);
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.ownerId = int.Parse(this.PartiesDataGridView.Rows[this.selectedPartyGridRowId].Cells["OwnerId1"].Value.ToString());
                                        break;
                                    }
                            }
                        }
                    }

                    ////to avoid the Parties Type Combo Selection Changed Event
                    this.avoidPartiesTypeComboSelectionChangedEvent = false;
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the TextChanged event of the GeneralPartiesTextBoxs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPartiesTextBoxs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.partiesGridClick && this.formLoaded && this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PartiesTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.partiesGridClick && this.formLoaded && this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                {
                    this.SetPartieHeaderToUpdateMode();
                }

                if (string.IsNullOrEmpty(this.GeneralPartiesNameTextBox.Text.Trim()) && !this.avoidPartiesTypeComboSelectionChangedEvent)
                {
                    ////used to load the parties header part based on the Parties type selection commited
                    this.SetPartiesHeaderPartOnNew();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.partiesGridClick = true;
                if (e.RowIndex >= 0)
                {
                    this.selectedPartyGridRowId = e.RowIndex;
                    this.partieCoulmnIndex = e.ColumnIndex;

                    ////this.SetCalenderInvisible();
                    if (this.partiesHeaderkeyPressed)
                    {
                        //// Check if its Same Row Else Do
                        if (this.selectedPartyGridRowId != this.tempRowIdParties)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParties"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        //// Save the Details
                                        this.UpdateLocalPartiesDataSet(this.tempRowIdParties);
                                        //// reset the flag

                                        this.selectedPartyGridRowId = e.RowIndex;
                                        this.tempRowIdParties = e.RowIndex;
                                        //// this.SetPartiesGrid();
                                        this.SetPartiesText(e.RowIndex);
                                        //// reset the textBox
                                        this.partiesHeaderkeyPressed = false;
                                        this.SetDataGridCoulmn(this.PartiesDataGridView, e.RowIndex, e.ColumnIndex);
                                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                                        ////to enable the save and cancel button in the master form
                                        this.ToEnableEditButtonInMasterForm();
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        //// Assing New ID; 
                                        this.selectedPartyGridRowId = e.RowIndex;
                                        this.tempRowIdParties = e.RowIndex;
                                        this.SetPartiesGrid();
                                        this.SetPartiesText(this.selectedPartyGridRowId);
                                        this.SetDataGridCoulmn(this.PartiesDataGridView, this.selectedPartyGridRowId, e.ColumnIndex);
                                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                                        this.partiesHeaderkeyPressed = false;
                                        this.PartiesDataGridView.Focus();
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        this.SetDataGridCoulmn(this.PartiesDataGridView, this.tempRowIdParties, e.ColumnIndex);
                                        break;
                                    }
                            } ///// End Case
                        } //// End if
                    }
                    else
                    {
                        this.selectedPartyGridRowId = e.RowIndex;
                        this.tempRowIdParties = e.RowIndex;
                        this.SetPartiesText(this.selectedPartyGridRowId);
                    }
                    if (this.partieCoulmnIndex == 17)
                    {
                        try
                        {
                            if (e.ColumnIndex == this.PartiesDataGridView.Columns["StatusVal"].Index)
                            {
                                int i = e.RowIndex;

                                if (i >= 0 && this.PartiesDataGridView.Rows[i].Cells["StatusVal"].Value.ToString() == "Status")
                                {
                                    int formno = 9102;
                                    Form formInfo = new Form();
                                    object[] optionalParameter = new object[] { 5, this.ownerId };
                                    formInfo = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, optionalParameter, this.form15010Control.WorkItem);
                                    if (formInfo != null)
                                    {
                                        if (formInfo.ShowDialog() == DialogResult.OK)
                                        {
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

                }

                this.partiesGridClick = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the UpdateParites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateParites_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                ////used to store whether new operion is on
                this.isnewButtonClicked = false;

                this.SavePartiesHeader();
                this.DisablePartiesHeaderPanels(true);
                this.Cursor = Cursors.Default;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelPartiesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelPartiesButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////used to store whether new operion is on
                this.isnewButtonClicked = false;

                ////  this.exciseTaxAffidavitDataSet.PartiesHeader.RejectChanges();
                int tempCurrentPartiesRowId = this.selectedPartyGridRowId;
                this.Cursor = Cursors.WaitCursor;
                this.SetPartiesGrid();

                if (this.partiesRowCount > 0)
                {
                    this.PartiesDataGridView.Rows[tempCurrentPartiesRowId].Selected = true;
                    this.PartiesDataGridView.CurrentCell = this.PartiesDataGridView[4, tempCurrentPartiesRowId];
                    this.PartiesDataGridView.Focus();
                    this.SetPartiesText(tempCurrentPartiesRowId);
                }
                else
                {
                    //// Disable
                    this.SetEnableStatusforPartiesControls(false);
                    this.PartiesDataGridView.Rows[tempCurrentPartiesRowId].Selected = false;
                }

                this.SetPartiesGridButtons(ButtonOperation.Empty);

                this.partiesHeaderkeyPressed = false;
                //// If only Removed and no changes
                if (this.affdvtRemove && this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                {
                    this.SetAffDvtButton(ButtonOperation.Empty);
                }

                this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                if (this.partiesRowCount > 0)
                {
                    this.GeneralPartiesNameTextBox.Focus();
                }
                else
                {
                    ////this.NewPartiesButton.Focus();
                    if (this.ParcelNumberPanel.Enabled)
                    {
                        this.ParcelPictureBox.Enabled = true;
                        this.ParcelNumberTextBox.Focus();
                    }
                    else
                    {
                        this.PartialSaleCombo.Focus();
                    }
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the RemoveParties control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveParties_Click(object sender, EventArgs e)
        {
            if (this.selectedPartyGridRowId >= 0)
            {
                try
                {
                    this.GeneralPartiesNameTextBox.BackColor = System.Drawing.Color.White;
                    int currentPartyGridRow = this.PartiesDataGridView.CurrentRowIndex;
                    this.Cursor = Cursors.WaitCursor;
                    //// turn On because inorder to  avoid rowenter event 
                    this.partiesHeaderkeyPressed = true;
                    /////this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Remove[
                    //this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[this.selectedPartyGridRowId].Delete();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[currentPartyGridRow].Delete();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    this.partiesHeaderkeyPressed = false;
                    this.SetPartiesGrid();
                    if (this.partiesRowCount > 0)
                    {
                        if (this.selectedPartyGridRowId > 0)
                        {
                            this.selectedPartyGridRowId = this.partiesRowCount - 1;
                        }

                        if (currentPartyGridRow == this.partiesRowCount)
                        {
                            currentPartyGridRow = 0;
                        }

                        TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, currentPartyGridRow);
                        this.SetPartiesText(currentPartyGridRow);

                        this.selectedPartyGridRowId = currentPartyGridRow;
                        //TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.selectedPartyGridRowId);
                        //this.SetPartiesText(this.selectedPartyGridRowId);

                        ////this.PartiesDataGridView.Focus();

                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                    }
                    else
                    {
                        this.PartiesDataGridView.Rows[Convert.ToInt32(this.partiesRowCount)].Selected = false;
                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                        this.ClearPartiesHeader();
                        this.PartiesDataGridView.Enabled = false;
                        this.selectedPartyGridRowId = 0;
                        this.NewPartiesButton.Focus();
                        this.PartiesDataGridView.Focus();
                        this.PartiesDataGridView.CurrentCell = null;
                    }

                    this.SetPartiesGridButtons(ButtonOperation.Remove);
                    this.partyHeaderButtonOperation = (int)ButtonOperation.Remove;
                    this.affdvtRemove = true;
                    ////to enable the save and cancel button in the master form
                    this.ToEnableEditButtonInMasterForm();
                    this.Cursor = Cursors.Default;
                    this.partiesHeaderkeyPressed = false;
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the NewParties control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewParties_Click(object sender, EventArgs e)
        {
            //// When NEw Button is click set Parties Header
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.partyHeaderButtonOperation = (int)ButtonOperation.New;
                //// SetButton
                this.SetPartiesGridButtons(ButtonOperation.New);
                this.ClearPartiesHeader();

                //// enable Parites header
                this.SetEnableStatusforPartiesControls(true);
                this.PartiesDataGridView.Rows[0].Selected = false;
                this.partiesHeaderkeyPressed = true;

                ////as per client request the country text box value for new mode is set to U.S.
                //this.PartiesCountryTextBox.Text = "U.S.";
                this.PartiesCountryTextBox.Text = "US";
                //// To be get From Resource String
                this.PartiesOwnerTextBox.Text = "100";

                this.ownerId = 0;

                this.DisablePartiesHeaderPanels(true);
                //// Set Focus to First 
                this.GeneralPartiesNameTextBox.Focus();

                this.PartiesTypeComboBox.SelectedIndex = -1;

                ////when new button is clicked 
                this.isnewButtonClicked = true;

                ////Address Auto Complete Functionality
                if (this.StreetAddressTextBox.Text == "")
                {
                    this.address1dt = new DataTable();
                    if (this.address1dt.Columns.Count == 0)
                    {
                        this.address1dt.Columns.Add("Address1");
                    }

                    DataRow dr1;
                    dr1 = this.address1dt.NewRow();
                    dr1["Address1"] = this.PartiesAddress1TextBox.Text;
                    this.address1dt.Rows.Add(dr1);
                    this.address1 = TerraScanCommon.GetXmlString(this.address1dt);
                    this.SetAutoCompleteForAddress1();
                }

                if (this.StreetAddressTextBox.Text == "")
                {
                    this.address2dt = new DataTable();
                    if (this.address2dt.Columns.Count == 0)
                    {
                        this.address2dt.Columns.Add("Address2");
                    }

                    DataRow dr2;
                    dr2 = this.address2dt.NewRow();
                    dr2["Address2"] = this.PartiesAddress2TextBox.Text;
                    this.address2dt.Rows.Add(dr2);
                    this.address2 = TerraScanCommon.GetXmlString(this.address2dt);
                    this.SetAutoCompleteForAddress2();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the parties grid buttons.
        /// </summary>
        /// <param name="buttonOperation">The button operation.</param>
        private void SetPartiesGridButtons(ButtonOperation buttonOperation)
        {
            switch (buttonOperation)
            {
                case ButtonOperation.New:
                    {
                        this.PartiesButtonOprNew();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.PartiesButtonOprCancel();
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.PartyButtonOprEmpty();
                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.PartiesButtonOprUpdate();
                        break;
                    }

                case ButtonOperation.ReceiptidNotExist:
                    {
                        this.PartiesButtonOprReceiptNotExist();
                        break;
                    }

                case ButtonOperation.NoRecordFound:
                    {
                        this.PartiesButtonOprNoRecordFound();
                        break;
                    }

                case ButtonOperation.NoPermission:
                    {
                        this.PartiesButtonOprNoPermission();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.partieCoulmnIndex = ((DataGridView)sender).CurrentCell.ColumnIndex;
                if (this.partiesHeaderkeyPressed)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                this.GridPartiesCancel(e);
                                break;
                            }

                        case Keys.Up:
                            {
                                this.GridPartiesCancel(e);
                                break;
                            }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Grids the parties cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridPartiesCancel(KeyEventArgs e)
        {
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParties"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.UpdateLocalPartiesDataSet(this.selectedPartyGridRowId);
                        this.partiesHeaderkeyPressed = false;
                        e.Handled = false;
                        this.SetDataGridCoulmn(this.PartiesDataGridView, this.selectedPartyGridRowId, this.partieCoulmnIndex);
                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                        ////to enable the save and cancel button in the master form
                        this.ToEnableEditButtonInMasterForm();
                        break;
                    }

                case DialogResult.No:
                    {
                        this.exciseTaxAffidavitDataSet.PartiesHeader.RejectChanges();
                        this.SetDataGridCoulmn(this.PartiesDataGridView, this.selectedPartyGridRowId, this.partieCoulmnIndex);
                        this.partiesHeaderkeyPressed = false;
                        e.Handled = false;
                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.partiesGridClick = true;
                if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                {
                    //this.StatusVal.LinkColor = Color.White;
                    if (!this.partiesHeaderkeyPressed && (this.affdvtButtonOperation != (int)ButtonOperation.New))
                    {
                        this.selectedPartyGridRowId = e.RowIndex;
                        this.tempRowIdParties = e.RowIndex;
                        this.SetPartiesText(e.RowIndex);
                        this.partieCoulmnIndex = e.ColumnIndex;
                    }
                }

                this.partiesGridClick = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesPhoneNoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesPhoneNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesOwnerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesOwnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesAddress1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesAddress1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesAddress2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesAddress2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesCityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesCityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesStateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesStateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesZipCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesZipCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesCountryTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesCountryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetPartieHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Parties events

        #endregion Coding For Parties

        #region Coding For ParcelHeader

        #region Parcels Methods

        /// <summary>
        /// To Clear Parcels Part Controls 
        /// </summary>
        private void ClearParcelsPartControls()
        {
            this.ParcelNumberTextBox.Text = string.Empty;

            this.ParcelAssessedValueTextBox.TextCustomFormat = "#,##";
            this.ParcelAssessedValueTextBox.Text = string.Empty;
            this.ParcelAssessedValueTextBox.TextCustomFormat = "#,##0";

            this.ParcelLegalTextBox.Text = string.Empty;
            this.PersonlaPropertyTextBox.Text = string.Empty;
            this.PersonlaPropertyComboBox.Text = string.Empty;
            ////to empty the combo boxs           
        }

        /// <summary>
        /// Saves the parcel header.
        /// </summary>
        private void SaveParcelHeader()
        {
            if (arrayList != null)
            {
                this.arrayList.Clear();
            }
            ///// if its NewOpeation Then Save 
            if (this.ParcelMantadtoryField())
            {
                if (this.parcelButtonOperation == (int)ButtonOperation.New)
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.CreateNewParcelRow();
                    this.SetParcelGrid();
                    if (this.parcelRecordCount > 0)
                    {
                        this.parcelRowId = this.parcelRecordCount - 1;
                        TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId);
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.tempParcelRowId = this.parcelRowId;
                    }
                    //Added by purushotham to implement 20689
                    if (this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString()) && (!this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString().Equals("0")) && this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["IsPersonalProperty"].ToString().ToLower().Equals("false"))
                            {
                                parcelList(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString());
                            }
                        }
                    }
                    ////this.toolBoxSmartPart.Enabled = false; 
                    this.ParcelHeaderDataGridView.Focus();

                    if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                    {
                        this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    }

                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.UpdateParcelHeader(this.parcelRowId);
                    //// this.toolBoxSmartPart.Enabled = false;
                    ////this.ReceiptFormButton.Enabled = false;
                    this.SetParcelGrid();
                    if (this.parcelRecordCount > 0)
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId);
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.ParcelHeaderDataGridView.Enabled = true;
                        this.ParcelHeaderDataGridView.Focus();
                    }
                    else
                    {
                        //// Diasable
                        ////this.SetEnableSatustParcelHeader(false);
                        this.ParcelHeaderDataGridView.Enabled = false;
                    }

                    if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                    {
                        this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    }
                    //Added by purushotham to implement 20689
                    if (this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString()) && (!this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString().Equals("0")) && this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["IsPersonalProperty"].ToString().ToLower().Equals("false"))
                            {
                                parcelList(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString());
                            }
                        }
                    }
                    this.Cursor = Cursors.Default;
                }

                this.parcelButtonOperation = (int)ButtonOperation.Empty;
                this.SetAffDvtButton(ButtonOperation.HeaderPartUpdate);
                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.parcelHeaderKeyPressed = false;
                ////   SetPanelPosition(this.ParcelHeaderPanel);

                ////to enable the save and cancel button in the master form
                this.ToEnableEditButtonInMasterForm();
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomiseParcelDataGridView()
        {
            DataGridViewColumnCollection columns = this.ParcelHeaderDataGridView.Columns;
            columns["ParcelNumber"].DataPropertyName = "Number";
            columns["IsPersonalPropertyValue"].DataPropertyName = "IsPersonalPropertyValue";
            columns["IsPersonalProp"].DataPropertyName = "IsPersonalProperty";
            columns["AssessedValue"].DataPropertyName = "AssessedValue";
            columns["Legal"].DataPropertyName = "Legal";
            columns["SoldParcelID"].DataPropertyName = "SoldParcelID";
            columns["StatementID1"].DataPropertyName = "StatementID";
            columns["ParcelNumber"].DisplayIndex = 0;
            columns["IsPersonalPropertyValue"].DisplayIndex = 1;
            columns["AssessedValue"].DisplayIndex = 2;
            columns["Legal"].DisplayIndex = 3;
            columns["SoldParcelID"].DisplayIndex = 4;
            columns["StatementID1"].DisplayIndex = 5;
            columns["IsPersonalProp"].DisplayIndex = 6;
            columns["SoldParcelID"].Visible = false;
            columns["StatementID1"].Visible = false;
        }

        /// <summary>
        /// Loads the parcel grid.
        /// </summary>
        private void SetParcelGrid()
        {
            this.parcelRecordCount = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count;

            this.ParcelHeaderDataGridView.DataSource = this.exciseTaxAffidavitDataSet.ParcelHeader.Copy().DefaultView;

            if (this.parcelRecordCount > this.ParcelHeaderDataGridView.NumRowsVisible)
            {
                this.ParcelVScrolBar.Enabled = true;
                this.ParcelVScrolBar.Visible = false;
                ////this.ParcelVScrolBar.BringToFront();
            }
            else
            {
                this.ParcelVScrolBar.Enabled = false;
                this.ParcelVScrolBar.Visible = true;
            }
        }

        /// <summary>
        /// Sets the parcel header text box.
        /// </summary>
        /// <param name="parcelRowIdSelect">The parcel row id select.</param>
        private void SetParcelHeaderTextBox(int parcelRowIdSelect)
        {
            if (parcelRowIdSelect >= 0)
            {
                this.ParcelNumberTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["ParcelNumber"].Value.ToString();
                this.ParcelAssessedValueTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["AssessedValue"].Value.ToString();
                this.ParcelLegalTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["Legal"].Value.ToString();
                //// get the index of the cfgValue
                SetComboboxValue(this.PersonlaPropertyComboBox, this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["IsPersonalProp"].Value.ToString());
            }
        }

        /// <summary>
        /// Clears the parcel header.
        /// </summary>
        private void ClearParcelHeader()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.PersonlaPropertyComboBox.SelectedIndex = 0;
            this.ParcelAssessedValueTextBox.TextCustomFormat = "#,##";
            this.ParcelAssessedValueTextBox.Text = string.Empty;
            this.ParcelAssessedValueTextBox.TextCustomFormat = "#,##0";
            this.ParcelLegalTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Parcels the mantadtory field.
        /// </summary>
        /// <returns> Return True if Parcel Field is filled else false</returns>
        private bool ParcelMantadtoryField()
        {
            bool parcelMntField;
            decimal assessedValue;
            if (!string.IsNullOrEmpty(this.ParcelAssessedValueTextBox.Text.Trim().Replace("$", "")))
            {
                assessedValue = Decimal.Parse(this.ParcelAssessedValueTextBox.Text.Trim().Replace("$", ""));
            }
            else
            {
                assessedValue = 0;
            }

            if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()) && assessedValue > 0)
            {
                parcelMntField = true;
            }
            else
            {
                parcelMntField = true;
            }

            return parcelMntField;
        }

        /// <summary>
        /// Updates the parcel header.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void UpdateParcelHeader(int rowId)
        {
            this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["Number"] = this.ParcelNumberTextBox.Text.Trim();
            //Added by purushotham to implement 20689
            if (this.ParcelNumberTextBox.Tag != null && (int)this.ParcelNumberTextBox.Tag > 0)
            {
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["ParcelID"] = this.ParcelNumberTextBox.Tag.ToString();
            }
            else
            {
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["ParcelID"] = 0;
            }
            this.exciseTaxAffidavitDataSet.ParcelHeader.Columns["IsPersonalProperty"].ReadOnly = false;
            this.exciseTaxAffidavitDataSet.ParcelHeader.Columns["IsPersonalPropertyValue"].ReadOnly = false;
            if (String.Equals(this.PersonlaPropertyComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["IsPersonalProperty"] = 1;
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["IsPersonalPropertyValue"] = SharedFunctions.GetResourceString("YESValue");
            }
            else
            {
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["IsPersonalProperty"] = 0;
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["IsPersonalPropertyValue"] = SharedFunctions.GetResourceString("NOValue");
            }

            this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["AssessedValue"] = this.ParcelAssessedValueTextBox.Text.Replace("$", "").Trim();
            this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["Legal"] = this.ParcelLegalTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.ParcelHeader.Columns["IsPersonalPropertyValue"].ReadOnly = false;
            this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();

            //purushotham to udate grid row
            //if (dtParcel.Rows.Count > rowId)
            //{
            //    this.dtParcel.Rows.RemoveAt(rowId);
            //}
        }

        /// <summary>
        /// Creates the new parcel row.
        /// </summary>
        private void CreateNewParcelRow()
        {
            this.Cursor = Cursors.WaitCursor;
            String tempKeyId;
            DataView maxView = new DataView(this.exciseTaxAffidavitDataSet.ParcelHeader);
            //// dv.RowFilter = "PostalCode = Max(PostalCode)"; 
            maxView.RowFilter = "SoldParcelID = Max(SoldParcelID)";
            if (maxView.Count > 0)
            {
                tempKeyId = maxView[0]["SoldParcelID"].ToString();

                if (!string.IsNullOrEmpty(tempKeyId))
                {
                    this.keyParcelId = int.Parse(tempKeyId) + 1;
                }
                else
                {
                    this.keyParcelId = 0;
                }
            }
            else
            {
                this.keyParcelId = 0;
            }

            DataRow partiesTempRow = this.exciseTaxAffidavitDataSet.ParcelHeader.NewRow();
            partiesTempRow["SoldParcelID"] = this.keyParcelId;
            partiesTempRow["StatementID"] = 0;
            partiesTempRow["Number"] = this.ParcelNumberTextBox.Text.Trim();
            //Added by purushotham to implement 20689
            if (this.ParcelNumberTextBox.Tag != null && (int)this.ParcelNumberTextBox.Tag > 0)
            {
                partiesTempRow["ParcelID"] = this.ParcelNumberTextBox.Tag;
            }
            else
            {
                partiesTempRow["ParcelID"] = 0;
            }
            if (String.Equals(this.PersonlaPropertyComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                partiesTempRow["IsPersonalProperty"] = 1;
                partiesTempRow["IsPersonalPropertyValue"] = SharedFunctions.GetResourceString("YESValue");
            }
            else
            {
                partiesTempRow["IsPersonalProperty"] = 0;
                partiesTempRow["IsPersonalPropertyValue"] = SharedFunctions.GetResourceString("NOValue");
            }

            if (!string.IsNullOrEmpty(this.ParcelAssessedValueTextBox.Text))
            {
                partiesTempRow["AssessedValue"] = this.ParcelAssessedValueTextBox.Text.Replace("$", "").Trim();
            }
            else
            {
                partiesTempRow["AssessedValue"] = "0";
            }

            partiesTempRow["Legal"] = this.ParcelLegalTextBox.Text.Trim();
            //// if(partiesTempRow["EmptyRecord"
            this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Add(partiesTempRow);
            this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();
            this.parcelRecordCount = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count;

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Sets the parcel header to update mode.
        /// </summary>
        private void SetParcelHeaderToUpdateMode()
        {
            if (this.PermissionFiled.editPermission || this.PermissionFiled.newPermission)
            {
                this.parcelHeaderKeyPressed = true;
                this.SetParcelGridButtons(ButtonOperation.Update);
            }
        }

        /// <summary>
        /// Parcels the button opr no permission.
        /// </summary>
        private void ParcelButtonOprNoPermission()
        {
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
            ////this.SetEnableSatustParcelHeader(false);
            if (this.receiptIDExist)
            {
                this.ParcelHeaderDataGridView.Enabled = false;
            }
            else
            {
                this.ParcelHeaderDataGridView.Enabled = true;
            }
        }

        /// <summary>
        /// Parcels the button opr no record found.
        /// </summary>
        private void ParcelButtonOprNoRecordFound()
        {
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
            this.ParcelHeaderDataGridView.Enabled = false;
            ////this.SetEnableSatustParcelHeader(false);
        }

        /// <summary>
        /// Parcels the button opr recpt id not exist.
        /// </summary>
        private void ParcelButtonOprRecptIdNotExist()
        {
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
            //// Diasable
            ////this.SetEnableSatustParcelHeader(false);
            this.ParcelHeaderDataGridView.Enabled = true;
        }

        /// <summary>
        /// Parcels the button opr remove.
        /// </summary>
        private void ParcelButtonOprRemove()
        {
            if (this.partiesRowCount > 0)
            {
                this.ParcelRemoveButton.Enabled = true;
                this.ParcelHeaderDataGridView.Enabled = true;
            }
            else
            {
                this.ParcelRemoveButton.Enabled = false;
                this.ParcelHeaderDataGridView.Enabled = false;
            }

            this.ParcelNewButton.Enabled = true;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
        }

        /// <summary>
        /// Parcels the button opr update.
        /// </summary>
        private void ParcelButtonOprUpdate()
        {
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = true;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = true;
            this.ParcelHeaderDataGridView.Enabled = true;
        }

        /// <summary>
        /// Parcels the button opr empty.
        /// </summary>
        private void ParcelButtonOprEmpty()
        {
            if (this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    ////if (!this.receiptIDExist) 
                    ////{
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = true;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelNewButton.Enabled = true;
                    ////    this.ParcelUpdate Button.Enabled = false;
                    ////    this.ParcelRemoveButton.Enabled = true;
                    ////    this.ParcelCancelButton.Enabled = false;                        
                    ////    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                }
                else
                {
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    ////if (!this.receiptIDExist)
                    ////{
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = true;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelNewButton.Enabled = true;
                    ////    this.ParcelUpdateButton.Enabled = false;
                    ////    this.ParcelRemoveButton.Enabled = true;
                    ////    this.ParcelCancelButton.Enabled = false;                        
                    ////    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                }
                else
                {
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.newPermission)
            {
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                }
                else
                {
                    //// If Only NEw Permission Not in new Mode then make it disable
                    ////this.SetEnableSatustParcelHeader(false);
                    this.ParcelNewButton.Enabled = false;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    ////if (!this.receiptIDExist)
                    ////{
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = true;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                    ////else
                    ////{
                    ////    this.ParcelNewButton.Enabled = true;
                    ////    this.ParcelUpdateButton.Enabled = false;
                    ////    this.ParcelRemoveButton.Enabled = true;
                    ////    this.ParcelCancelButton.Enabled = false;                        
                    ////    this.ParcelHeaderDataGridView.Enabled = true;
                    ////}
                }
                else
                {
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Parcels the button opr cancel.
        /// </summary>
        private void ParcelButtonOprCancel()
        {
            this.ParcelNewButton.Enabled = true;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = true;
            this.ParcelCancelButton.Enabled = false;
            this.ParcelHeaderDataGridView.Enabled = true;
        }

        /// <summary>
        /// Parcels the button opr new.
        /// </summary>
        private void ParcelButtonOprNew()
        {
            if (this.parcelButtonOperation == (int)ButtonOperation.New)
            {
                this.ParcelNewButton.Enabled = false;
                this.ParcelUpdateButton.Enabled = true;
                this.ParcelRemoveButton.Enabled = false;
                this.ParcelCancelButton.Enabled = true;
                this.ParcelHeaderDataGridView.Enabled = false;
            }
            else
            {
                this.ParcelNewButton.Enabled = true;
                this.ParcelUpdateButton.Enabled = false;
                this.ParcelRemoveButton.Enabled = false;
                this.ParcelCancelButton.Enabled = false;
                this.ParcelHeaderDataGridView.Enabled = false;
            }
        }

        #endregion Parcels Methods

        #region Parcels Events

        /// <summary>
        /// Handles the TextChanged event of the ParcelTextBoxs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelTextBoxs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)// && this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void ParcelNumberTextBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                this.SetParcelDistrictValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.isParcelEdited = false;
            }
        }

        private void SetParcelDistrictValues()
        {
            if (this.isParcelEdited)
            {
                int ownerLowVal = 0;
                int ownerHighVal = 0;
                this.formLoaded = false;
                string parcelNumber = null;
                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
                {
                    parcelNumber = this.ParcelNumberTextBox.Text.Trim();
                }

                int? parcelId = null;
                if (this.ParcelNumberTextBox.Tag != null && (int)this.ParcelNumberTextBox.Tag > 0)
                {
                    parcelId = (int)this.ParcelNumberTextBox.Tag;
                }

                this.ownerDetailDataSet.ListParcelDetailTable.Clear();
                this.ownerDetailDataSet.PartiesOwnerDetail.Clear();
                this.ownerDetailDataSet.ExciseUseCode.Clear();
                F15010ExciseAffidavitData parcelData = this.form15010Control.WorkItem.GetParcelDetail(parcelId, parcelNumber);
                //if(!string.IsNullOrEmpty(parcelNumber) &&(parcelId==null))
                //{
                //    dtParcel.Rows.Add(parcelId, parcelNumber);
                //}
                //this.ownerDetailDataSet.ListParcelDetailTable.Merge(this.form15010Control.WorkItem.GetParcelDetail(parcelIdValue, parcelNumber));
                this.ownerDetailDataSet.ListParcelDetailTable.Merge(parcelData.ListParcelDetailTable);
                this.ownerDetailDataSet.PartiesOwnerDetail.Merge(parcelData.PartiesOwnerDetail);
                this.ownerDetailDataSet.ExciseUseCode.Merge(parcelData.ExciseUseCode);
                //this.ownerDetailDataSet.AcceptChanges();

                if (this.ownerDetailDataSet.ExciseUseCode.Rows.Count > 0)
                {
                    F15010ExciseAffidavitData.ExciseUseCodeRow useCodeRow = (F15010ExciseAffidavitData.ExciseUseCodeRow)this.ownerDetailDataSet.ExciseUseCode.Rows[0];
                    if (string.Equals(this.AffdvtUseCodeTextBox.Text.Trim(), "-  -"))
                    {
                        // Update UseCode textbox value
                        string useCode = string.Empty;

                        if (!useCodeRow.IsUseCode1Null())
                        {
                            useCode = useCodeRow.UseCode1.ToString() + "-";
                        }

                        if (!useCodeRow.IsUseCode2Null())
                        {
                            useCode = useCode + useCodeRow.UseCode2.ToString() + "-";
                        }

                        if (!useCodeRow.IsUseCode3Null())
                        {
                            useCode = useCode + useCodeRow.UseCode3.ToString();
                        }

                        this.AffdvtUseCodeTextBox.Text = useCode;

                    }

                    //if (string.IsNullOrEmpty(this.GeneralDistrictLinkLabel.Text.Trim()))
                    if (!this.isDistrictChanged)//this.currentAffidavitStatementId <= 0)//!this.isDistrictSelected)
                    {
                        // If District details doesn't exists for excise 
                        // use the ExciseRateID to populate district related fields
                        if (!useCodeRow.IsExciseRateIDNull())
                        {
                            this.exciseRateId = useCodeRow.ExciseRateID.ToString();
                            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_GetDistrictSelection(int.Parse(this.exciseRateId));

                            if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                            {
                                F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow selectedRow = (F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow)this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0];
                                this.GeneralDistrictLinkLabel.Text = selectedRow.District.ToString();
                                this.GeneralLocationCodeTextBox.Text = selectedRow.LocationCode.ToString();
                                this.NameOfLocationTextBox.Text = selectedRow.LocationName.ToString();

                                if (string.Compare(selectedRow.IsCounty.ToString(), "0").Equals(0))
                                {
                                    this.LocationofSaleComboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.LocationofSaleComboBox.SelectedIndex = 1;
                                }
                            }
                        }
                    }
                }

                if (this.ownerDetailDataSet.ListParcelDetailTable.Rows.Count > 0)
                {
                    //this.ParcelNumberTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.ParcelNumberColumn.ColumnName].ToString();
                    //this.ParcelAssessedValueTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.AssessedValueColumn.ColumnName].ToString();
                    //this.ParcelLegalTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.LegalColumn.ColumnName].ToString();
                    //SetComboboxValue(this.PersonlaPropertyComboBox, this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.IsPersonalPropertyColumn.ColumnName].ToString());
                    // Bind parcel related fields

                    //this.parcelHeaderKeyPressed = true;
                    F15010ExciseAffidavitData.ListParcelDetailTableRow parcelRow = (F15010ExciseAffidavitData.ListParcelDetailTableRow)this.ownerDetailDataSet.ListParcelDetailTable.Rows[0];

                    if (!parcelRow.IsParcelNumberNull())
                    {
                        this.ParcelNumberTextBox.Text = parcelRow.ParcelNumber.ToString();
                    }
                    else
                    {
                        this.ParcelNumberTextBox.Text = string.Empty;
                    }

                    if (!parcelRow.IsAssessedValueNull())
                    {
                        this.ParcelAssessedValueTextBox.Text = parcelRow.AssessedValue.ToString();
                    }
                    else
                    {
                        this.ParcelAssessedValueTextBox.Text = string.Empty;
                    }

                    if (!parcelRow.IsLegalNull())
                    {
                        this.ParcelLegalTextBox.Text = parcelRow.Legal.ToString();
                        //this.ParcelLegalTextBox.ForeColor = Color.Black;
                        //this.ParcelLegalTextBox.Multiline = true;
                    }
                    else
                    {
                        this.ParcelLegalTextBox.Text = string.Empty;
                    }

                    SetComboboxValue(this.PersonlaPropertyComboBox, parcelRow.IsPersonalProperty.ToString());
                }
                else
                {
                    //this.ParcelNumberTextBox.Text = string.Empty;
                    this.ParcelAssessedValueTextBox.Text = string.Empty;
                    this.ParcelLegalTextBox.Text = string.Empty;
                    SetComboboxValue(this.PersonlaPropertyComboBox, "False");
                }

                if (this.ownerDetailDataSet.PartiesOwnerDetail.Rows.Count > 0)
                {
                    // this.exciseTaxAffidavitDataSet.PartiesHeader.Clear();
                    // F15010ExciseAffidavitData.PartiesHeaderDataTable ownerTable = this.exciseTaxAffidavitDataSet.PartiesHeader;
                    foreach (F15010ExciseAffidavitData.PartiesOwnerDetailRow dataRow in this.ownerDetailDataSet.PartiesOwnerDetail)
                    {
                        F15010ExciseAffidavitData.PartiesHeaderRow ownerRow = this.exciseTaxAffidavitDataSet.PartiesHeader.NewPartiesHeaderRow();
                        if (!dataRow.IsNameNull())
                        {
                            ownerRow.Name = dataRow.Name;
                        }
                        else
                        {
                            ownerRow.Name = string.Empty;
                        }

                        if (!dataRow.IsAddress1Null())
                        {
                            ownerRow.Address1 = dataRow.Address1;
                        }
                        else
                        {
                            ownerRow.Address1 = string.Empty;
                        }

                        if (!dataRow.IsAddress2Null())
                        {
                            ownerRow.Address2 = dataRow.Address2;
                        }
                        else
                        {
                            ownerRow.Address2 = string.Empty;
                        }


                        ownerRow.Address = ownerRow.Address1.Trim() + ownerRow.Address2.Trim();

                        if (!dataRow.IsCityNull())
                        {
                            ownerRow.City = dataRow.City;
                        }
                        else
                        {
                            ownerRow.City = string.Empty;
                        }

                        if (dataRow.IndividualTypeID != null)
                        {
                            ownerRow.IndividualTypeID = (byte)dataRow.IndividualTypeID;
                        }

                        if (!dataRow.IsIndividualTypeNull())
                        {
                            ownerRow.IndividualType = dataRow.IndividualType;
                        }
                        else
                        {
                            ownerRow.IndividualType = string.Empty;
                        }

                        if (!dataRow.IsPhoneNull())
                        {
                            ownerRow.Phone = dataRow.Phone;
                        }
                        else
                        {
                            ownerRow.Phone = string.Empty;
                        }

                        if (!dataRow.IsPercentOwnerNull())
                        {
                            ownerRow.PercentOwner = dataRow.PercentOwner;
                        }

                        if (!dataRow.IsStateNull())
                        {
                            ownerRow.State = dataRow.State;
                        }
                        else
                        {
                            ownerRow.State = string.Empty;
                        }

                        if (!dataRow.IsZipNull())
                        {
                            ownerRow.Zip = dataRow.Zip;
                        }
                        else
                        {
                            ownerRow.Zip = string.Empty;
                        }

                        if (!dataRow.IsCountryNull())
                        {
                            ownerRow.Country = dataRow.Country;
                        }
                        else
                        {
                            ownerRow.Country = string.Empty;
                        }

                        if (!dataRow.IsOwnerIDNull())
                        {
                            ownerRow.OwnerID = dataRow.OwnerID;
                        }

                        int ownerTempId = ownerRow.OwnerID;
                        this.getOwnerDataset.Clear();
                        getOwnerDataset = this.form15010Control.WorkItem.F15010_GetOwnerStatus(ownerTempId);

                        try
                        {
                            int.TryParse(getOwnerDataset.OwnerStatusLow.Rows[0]["Low"].ToString(), out ownerLowVal);
                            int.TryParse(getOwnerDataset.OwnerStatusHigh.Rows[0]["High"].ToString(), out ownerHighVal);


                            if (ownerLowVal > 0 || ownerHighVal > 0)
                            {
                                ownerRow.Status = "Status";
                            }
                            else
                            {
                                ownerRow.Status = "";
                            }
                        }
                        catch (Exception exp)
                        {
                        }


                        bool alreadyExists = false;
                        for (int i = 0; i < this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count; i++)
                        {
                            try
                            {
                                if (this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[i]["OwnerID"].Equals(ownerRow.OwnerID)
                                    && this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[i]["IndividualTypeID"].Equals(ownerRow.IndividualTypeID))
                                {
                                    alreadyExists = true;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        if (!alreadyExists)
                        {
                            this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Add(ownerRow);
                        }
                    }

                    this.EditEnabled();

                    this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    this.partiesRowCount = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count;
                    this.PartiesDataGridView.DataSource = this.exciseTaxAffidavitDataSet.PartiesHeader.Copy().DefaultView;

                    if (this.partiesRowCount > 0)
                    {
                        int partiesTypeUpdateId = 0;
                        int.TryParse(this.exciseTaxAffidavitDataSet.PartiesHeader.Compute("MAX (IndividualAutoID)", "IndividualID > 0").ToString(), out partiesTypeUpdateId);

                        if (partiesTypeUpdateId == 0)
                        {
                            for (int i = 0; i < this.partiesRowCount; i++)
                            {
                                ////to incremment the IndividualAutoID Column
                                this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[i][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualAutoIDColumn.ColumnName] = (i + 1);
                                this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                            }
                        }

                        if (!this.UpdateParites.Enabled)
                        {
                            this.SetPartiesText(0);
                            this.SetPartiesGridButtons(ButtonOperation.Cancel);
                            this.PartiesDataGridView.Enabled = true;
                            this.SetEnableStatusforPartiesControls(true);
                            this.PartiesDataGridView.Rows[0].Selected = false;
                            this.DisablePartiesHeaderPanels(true);
                            this.partiesHeaderkeyPressed = false;
                        }
                    }


                    if (this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count > this.PartiesDataGridView.NumRowsVisible)
                    {
                        this.PartiesHeaderVscrollBar.Enabled = true;
                        this.PartiesHeaderVscrollBar.Visible = false;
                        this.PartiesHeaderVscrollBar.BringToFront();
                    }
                    else
                    {
                        this.PartiesHeaderVscrollBar.Enabled = false;
                        this.PartiesHeaderVscrollBar.Visible = true;
                    }

                    // F15010ExciseAffidavitData.PartiesOwnerDetailRow dataRow = (F15010ExciseAffidavitData.PartiesOwnerDetailRow)this.ownerDetailDataSet.PartiesOwnerDetail.Rows[0];
                    //this.avoidPartiesTypeComboSelectionChangedEvent = true;
                    //this.GeneralPartiesNameTextBox.Text = dataRow.Name;
                    //this.PartiesPhoneNoTextBox.Text = dataRow.Phone;
                    //SetComboboxValue(this.PartiesTypeComboBox, dataRow.IndividualType.ToString());
                    //this.PartiesOwnerTextBox.Text = dataRow.PercentOwner.ToString();
                    //this.PartiesAddress1TextBox.Text = dataRow.Address1;
                    //this.PartiesAddress2TextBox.Text = dataRow.Address2;
                    //this.PartiesCityTextBox.Text = dataRow.City;
                    //this.PartiesStateTextBox.Text = dataRow.State;
                    //this.PartiesZipCodeTextBox.Text = dataRow.Zip;
                    //this.PartiesCountryTextBox.Text = dataRow.Country;
                    //this.avoidPartiesTypeComboSelectionChangedEvent = false;
                    //this.PartiesDataGridView.DataSource 
                }
                else
                {
                    //this.ClearPartiesHeader();
                    //this.PartiesTypeComboBox.SelectedIndex = -1;
                }

                this.formLoaded = true;
            }
        }

        private void ParcelNumberTextBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.parcelGridClick && this.formLoaded)
                {
                    this.isParcelEdited = true;
                    this.ParcelNumberTextBox.Tag = null; ;
                    if (this.parcelButtonOperation != (int)ButtonOperation.New)
                    {
                        this.SetParcelHeaderToUpdateMode();
                    }
                }
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
        /// Handles the Click event of the parcelPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                //if (dtParcel.Columns.Count == 0)
                //{
                //    dtParcel.Columns.Add("ParcelID", typeof(Int32));
                //    dtParcel.Columns.Add("ParcelNumber", typeof(String));
                //}
                int parcelIdValue = 0;
                Form form1401 = new Form();
                //Modifed to implement #21450 CO by purushotham 
                if (!string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
                {
                    var result = DateTime.Parse(this.AffDvtDocDateTextBox.Text.Trim()).Year;
                    int.TryParse(result.ToString(), out this.affidavitYear);
                }
                else
                {

                    //Modifed to implement #21450 CO by purushotham
                    if (this.exciseIndividualtype.ConfiguredRollYear.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.exciseIndividualtype.ConfiguredRollYear.Rows[0][0].ToString().Trim()))
                        {
                            int.TryParse(this.exciseIndividualtype.ConfiguredRollYear.Rows[0][0].ToString().Trim(), out this.affidavitYear);
                        }
                    }
                }

                object[] optionalParameter = new object[] { this.affidavitYear };
                form1401 = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, optionalParameter, this.form15010Control.WorkItem);
                if (form1401 != null)
                {
                    //if (form1401.ShowDialog() != DialogResult.Cancel)
                    if (form1401.ShowDialog() == DialogResult.OK)
                    {
                        this.isParcelEdited = true;
                        //// TO DO
                        int.TryParse(TerraScanCommon.GetValue(form1401, "ParcelID"), out parcelIdValue);
                        string parcelNumber = TerraScanCommon.GetValue(form1401, "CommandValue");

                        // Returned value has been set on textbox to use on textbox leave event
                        this.ParcelNumberTextBox.Text = parcelNumber;
                        this.ParcelNumberTextBox.Tag = parcelIdValue;
                        //if (parcelIdValue > 0)
                        //{ 
                        //    if (dtParcel.Columns.Count > 0)
                        //    {
                        //        dtParcel.Rows.Add(parcelIdValue, parcelNumber);
                        //    }
                        //  //  parcelList(parcelIdValue,parcelNumber);
                        //    //ht.Add("parcelNumber", parcelIdValue);
                        //}
                        //this.ownerDetailDataSet.ListParcelDetailTable.Clear();
                        //this.ownerDetailDataSet.PartiesOwnerDetail.Clear();
                        //this.ownerDetailDataSet.ExciseUseCode.Clear();
                        //F15010ExciseAffidavitData parcelData = this.form15010Control.WorkItem.GetParcelDetail(parcelIdValue, parcelNumber);
                        ////this.ownerDetailDataSet.ListParcelDetailTable.Merge(this.form15010Control.WorkItem.GetParcelDetail(parcelIdValue, parcelNumber));
                        //this.ownerDetailDataSet.ListParcelDetailTable.Merge(parcelData.ListParcelDetailTable);
                        //this.ownerDetailDataSet.PartiesOwnerDetail.Merge(parcelData.PartiesOwnerDetail);
                        //this.ownerDetailDataSet.ExciseUseCode.Merge(parcelData.ExciseUseCode);
                        ////this.ownerDetailDataSet.AcceptChanges();

                        //if (this.ownerDetailDataSet.ExciseUseCode.Rows.Count > 0)
                        //{
                        //    F15010ExciseAffidavitData.ExciseUseCodeRow useCodeRow = (F15010ExciseAffidavitData.ExciseUseCodeRow)this.ownerDetailDataSet.ExciseUseCode.Rows[0];
                        //    if (string.Equals(this.AffdvtUseCodeTextBox.Text.Trim(), "-  -"))
                        //    {
                        //        // Update UseCode textbox value
                        //        string useCode = string.Empty;

                        //        if (!useCodeRow.IsUseCode1Null())
                        //        {
                        //            useCode = useCodeRow.UseCode1.ToString() + "-";
                        //        }

                        //        if (!useCodeRow.IsUseCode2Null())
                        //        {
                        //            useCode = useCode + useCodeRow.UseCode2.ToString() + "-";
                        //        }

                        //        if (!useCodeRow.IsUseCode3Null())
                        //        {
                        //            useCode = useCode + useCodeRow.UseCode3.ToString();
                        //        }

                        //        this.AffdvtUseCodeTextBox.Text = useCode;

                        //    }

                        //    if (string.IsNullOrEmpty(this.GeneralDistrictLinkLabel.Text.Trim()))
                        //    {
                        //        // If District details doesn't exists for excise 
                        //        // use the ExciseRateID to populate district related fields
                        //        if (!useCodeRow.IsExciseRateIDNull())
                        //        {
                        //            this.exciseRateId = useCodeRow.ExciseRateID.ToString();
                        //            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_GetDistrictSelection(int.Parse(this.exciseRateId));

                        //            if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                        //            {
                        //                F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow selectedRow = (F15010ExciseAffidavitData.ListAffidavitDistrictSelectionRow)this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0];
                        //                this.GeneralDistrictLinkLabel.Text = selectedRow.District.ToString();
                        //                this.GeneralLocationCodeTextBox.Text = selectedRow.LocationCode.ToString();
                        //                this.NameOfLocationTextBox.Text = selectedRow.LocationName.ToString();

                        //                if (string.Compare(selectedRow.IsCounty.ToString(), "0").Equals(0))
                        //                {
                        //                    this.LocationofSaleComboBox.SelectedIndex = 0;
                        //                }
                        //                else
                        //                {
                        //                    this.LocationofSaleComboBox.SelectedIndex = 1;
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        //if (this.ownerDetailDataSet.ListParcelDetailTable.Rows.Count > 0)
                        //{
                        //    //this.ParcelNumberTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.ParcelNumberColumn.ColumnName].ToString();
                        //    //this.ParcelAssessedValueTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.AssessedValueColumn.ColumnName].ToString();
                        //    //this.ParcelLegalTextBox.Text = this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.LegalColumn.ColumnName].ToString();
                        //    //SetComboboxValue(this.PersonlaPropertyComboBox, this.ownerDetailDataSet.ListParcelDetailTable.Rows[0][this.ownerDetailDataSet.ListParcelDetailTable.IsPersonalPropertyColumn.ColumnName].ToString());
                        //    // Bind parcel related fields
                        //    F15010ExciseAffidavitData.ListParcelDetailTableRow parcelRow = (F15010ExciseAffidavitData.ListParcelDetailTableRow)this.ownerDetailDataSet.ListParcelDetailTable.Rows[0];

                        //    if (!parcelRow.IsParcelNumberNull())
                        //    {
                        //        this.ParcelNumberTextBox.Text = parcelRow.ParcelNumber.ToString();
                        //    }
                        //    else
                        //    {
                        //        this.ParcelNumberTextBox.Text = string.Empty;
                        //    }

                        //    if (!parcelRow.IsAssessedValueNull())
                        //    {
                        //        this.ParcelAssessedValueTextBox.Text = parcelRow.AssessedValue.ToString();
                        //    }
                        //    else
                        //    {
                        //        this.ParcelAssessedValueTextBox.Text = string.Empty;
                        //    }

                        //    if (!parcelRow.IsLegalNull())
                        //    {
                        //        this.ParcelLegalTextBox.Text = parcelRow.Legal.ToString();
                        //    }
                        //    else
                        //    {
                        //        this.ParcelLegalTextBox.Text = string.Empty;
                        //    }

                        //    SetComboboxValue(this.PersonlaPropertyComboBox, parcelRow.IsPersonalProperty.ToString());
                        //}
                    }
                }
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


        private void ParcelPictureBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                this.SetParcelDistrictValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                this.isParcelEdited = false;
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelGridClick = true;
                if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                {
                    if (!this.parcelHeaderKeyPressed && this.parcelButtonOperation != (int)ButtonOperation.New && this.parcelGridClick)
                    {
                        this.parcelRowId = e.RowIndex;
                        this.tempParcelRowId = e.RowIndex;
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.parcelColumnId = e.ColumnIndex;
                    }
                }

                this.currentParcelRowId = e.RowIndex;
                this.parcelGridClick = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.parcelColumnId = ((DataGridView)sender).CurrentCell.ColumnIndex;
                if (this.parcelHeaderKeyPressed)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Down:
                            {
                                this.GridParcelHeaderCancel(e);
                                break;
                            }

                        case Keys.Up:
                            {
                                this.GridParcelHeaderCancel(e);
                                break;
                            }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Grids the parties cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridParcelHeaderCancel(KeyEventArgs e)
        {
            ////MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.UpdateParcelHeader(this.parcelRowId);
                        this.SetParcelGrid();
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        //// TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId); this.parcelHeaderKeyPressed = false;
                        this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.parcelRowId, this.parcelColumnId);
                        e.Handled = false;
                        this.parcelHeaderKeyPressed = false;
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        ////to enable the save and cancel button in the master form
                        this.ToEnableEditButtonInMasterForm();
                        break;
                    }

                case DialogResult.No:
                    {
                        //// this.exciseTaxAffidavitDataSet.ParcelHeader.RejectChanges();

                        this.SetParcelGrid();
                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        //// this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.parcelColumnId, this.parcelRowId);
                        //// TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId);
                        this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.parcelRowId, this.parcelColumnId);
                        this.parcelHeaderKeyPressed = false;
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                        e.Handled = false;
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        e.Handled = true;
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.parcelButtonOperation = (int)ButtonOperation.New;
                this.SetParcelGridButtons(ButtonOperation.New);
                this.formLoaded = false;
                this.ClearParcelHeader();
                this.formLoaded = true;
                ////Enable
                ////this.SetEnableSatustParcelHeader(true);          
                ////SetPanelPosition(this.ParcelHeaderPanel);
                this.DisableParcelHeaderPanels(true);
                this.ParcelNumberTextBox.Focus();
                this.Cursor = Cursors.Default;
                //// TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, 0);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelUpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveParcelHeader();
                this.DisableParcelHeaderPanels(true);
                //this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count;

                if ((arrayList != null) && arrayList.Count > 0)
                {
                    this.LoadOpenSpaceField(arrayList);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelRemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelRemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.parcelRowId >= 0)
                {
                    if (arrayList != null)
                    {
                        this.arrayList.Clear();
                    }
                    //// turn On because inorder to  avoid rowenter event 

                    int currentParcelRowid = this.ParcelHeaderDataGridView.CurrentRowIndex;
                    this.Cursor = Cursors.WaitCursor;
                    this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[currentParcelRowid].Delete();
                    this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();
                    //Added by purushotham to implement 20689
                    if (this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count > 0)
                    {
                        for (int i = 0; i < this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString()) && (!this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString().Equals("0")) && this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["IsPersonalProperty"].ToString().ToLower().Equals("false"))
                            {
                                parcelList(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[i]["ParcelID"].ToString());
                            }
                        }
                    }
                    this.parcelHeaderKeyPressed = true;

                    this.SetParcelGrid();

                    if (this.parcelRecordCount > 0)
                    {
                        if (currentParcelRowid == this.parcelRecordCount)
                        {
                            currentParcelRowid = 0;
                        }

                        TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, currentParcelRowid);

                        this.formLoaded = false;
                        this.SetParcelHeaderTextBox(currentParcelRowid);
                        this.formLoaded = true;

                        this.parcelRowId = currentParcelRowid;
                    }
                    else
                    {
                        this.ClearParcelHeader();
                        this.ParcelHeaderDataGridView.Rows[currentParcelRowid].Selected = false;
                        ////this.PartiesDataGridView.CurrentCell = this.PartiesDataGridView[4, Convert.ToInt32(this.partiesRowCount)];
                        this.DisableParcelHeaderPanels(false);
                        this.SetParcelHeaderTextBox(currentParcelRowid);
                        //this.PartiesDataGridView.CurrentCell = null;
                    }

                    ////to enable the save and cancel button in the master form
                    this.ToEnableEditButtonInMasterForm();

                    this.parcelHeaderKeyPressed = false;

                    this.Cursor = Cursors.Default;

                    this.SetParcelGridButtons(ButtonOperation.Empty);
                    this.SetAffDvtButton(ButtonOperation.Remove);
                    this.ParcelNewButton.Focus();
                    this.affdvtRemove = true;
                    if (arrayList != null && arrayList.Count > 0)
                    {
                        LoadOpenSpaceField(arrayList);
                    }
                    else
                    {
                        this.AffidvtOpenSpaceComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelCancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.exciseTaxAffidavitDataSet.ParcelHeader.RejectChanges();
                int tempCurrentParcelRowId = this.currentParcelRowId;

                this.SetParcelGrid();

                if (this.parcelRecordCount > 0)
                {
                    ////this.tempParcelRowId = 0;
                    this.ParcelHeaderDataGridView.Rows[tempCurrentParcelRowId].Selected = true;
                    this.ParcelHeaderDataGridView.CurrentCell = this.ParcelHeaderDataGridView[2, tempCurrentParcelRowId];
                    this.formLoaded = false;
                    this.SetParcelHeaderTextBox(tempCurrentParcelRowId);
                    this.formLoaded = true;
                }
                else
                {
                    this.ParcelHeaderDataGridView.Enabled = false;
                    this.ParcelHeaderDataGridView.Rows[tempCurrentParcelRowId].Selected = false;
                    this.ClearParcelHeader();
                    this.DisableParcelHeaderPanels(false);
                }

                this.parcelButtonOperation = (int)ButtonOperation.Empty;
                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.parcelHeaderKeyPressed = false;
                if (this.parcelRecordCount > 0)
                {
                    this.ParcelNumberTextBox.Focus();
                }
                else
                {
                    //this.PartiesOwnerTextBox.Focus();
                    //this.PartialSaleCombo.Focus();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.parcelGridClick = true;
                if (e.RowIndex >= 0)
                {
                    this.parcelCoulmnIndex = e.ColumnIndex;

                    if (this.parcelHeaderKeyPressed)
                    {
                        this.parcelRowId = e.RowIndex;    //// Check if its Same Row Else Do
                        if (this.tempParcelRowId != this.parcelRowId)
                        {
                            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelParcel"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                case DialogResult.Yes:
                                    {
                                        //// Save the Details
                                        this.UpdateParcelHeader(this.tempParcelRowId);
                                        //// reset the flag
                                        this.parcelRowId = e.RowIndex;
                                        this.tempParcelRowId = e.RowIndex;

                                        //// reset the textBox
                                        this.SetParcelGrid();
                                        this.SetParcelHeaderTextBox(this.tempParcelRowId);
                                        ////TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.tempParcelRowId);

                                        this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                        this.SetParcelGridButtons(ButtonOperation.Empty);
                                        ////to enable the save and cancel button in the master form
                                        this.ToEnableEditButtonInMasterForm();
                                        this.parcelHeaderKeyPressed = false;
                                        break;
                                    }

                                case DialogResult.No:
                                    {
                                        ///// Assing New ID; 
                                        this.parcelRowId = e.RowIndex;
                                        this.tempParcelRowId = e.RowIndex;
                                        //// this.exciseTaxAffidavitDataSet.ParcelHeader.RejectChanges();
                                        this.SetParcelGrid();
                                        this.SetParcelHeaderTextBox(this.tempParcelRowId);
                                        //// this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, e.ColumnIndex, this.tempParcelRowId);
                                        //// TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.tempParcelRowId);
                                        this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                        this.SetParcelGridButtons(ButtonOperation.Empty);
                                        this.parcelHeaderKeyPressed = false;
                                        break;
                                    }

                                case DialogResult.Cancel:
                                    {
                                        //// TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.tempParcelRowId);
                                        this.SetDataGridCoulmn(this.ParcelHeaderDataGridView, this.tempParcelRowId, this.parcelCoulmnIndex);
                                        ////  TerraScan.Common.TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.tempParcelRowId);
                                        //// this.CustomiseParcelDataGridView();
                                        break;
                                    }
                            } //// End Case
                        } ///// End if
                    }
                    else
                    {
                        this.parcelRowId = e.RowIndex;
                        this.tempParcelRowId = e.RowIndex;
                        this.SetParcelHeaderTextBox(e.RowIndex);
                        this.parcelCoulmnIndex = e.ColumnIndex;
                    }
                }

                this.parcelGridClick = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the parties grid buttons.
        /// </summary>
        /// <param name="buttonOperation">The button operation.</param>
        private void SetParcelGridButtons(ButtonOperation buttonOperation)
        {
            switch (buttonOperation)
            {
                case ButtonOperation.New:
                    {
                        this.ParcelButtonOprNew();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.ParcelButtonOprCancel();
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.ParcelButtonOprEmpty();
                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.ParcelButtonOprUpdate();
                        break;
                    }

                case ButtonOperation.Remove:
                    {
                        this.ParcelButtonOprRemove();
                        break;
                    }

                case ButtonOperation.ReceiptidNotExist:
                    {
                        this.ParcelButtonOprRecptIdNotExist();
                        break;
                    }

                case ButtonOperation.NoRecordFound:
                    {
                        this.ParcelButtonOprNoRecordFound();
                        break;
                    }

                case ButtonOperation.NoPermission:
                    {
                        this.ParcelButtonOprNoPermission();
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != 27)
                {
                    this.SetParcelHeaderToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired, formattable column

                if (e.ColumnIndex == this.ParcelHeaderDataGridView.Columns["AssessedValue"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell 

                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0");
                            e.FormattingApplied = true;
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }
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

        #endregion Parcels Events

        #endregion Coding For ParcelHeader

        #region Coding For Affidvt

        #region Affidavit Methods

        /// <summary>
        /// Clears the aff DVT controls.
        /// </summary>
        private void ClearAffDvtControls()
        {
            this.PartialSaleCombo.SelectedIndex = 0;
            this.SegregatedComboBox.SelectedIndex = 0;
            this.StreetAddressTextBox.Text = string.Empty;
            this.LocationofSaleComboBox.SelectedIndex = 0;
            this.NameOfLocationTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtExemptRegNumberTextBox.Text = string.Empty;
            this.AffdvtForestCombo.Text = string.Empty;
            this.AffidvtOpenSpaceComboBox.SelectedIndex = 0;
            this.AffDvtHistoryComboBox.SelectedIndex = 0;
            this.AffDvtContinuanceComboBox.SelectedIndex = 0;
            this.AffDvtDescTextBox.Text = string.Empty;
            this.AffDvtExemptionCodeTextBox.Text = string.Empty;
            this.AffDvtExemptionDescrTextBox.Text = string.Empty;
            this.GeneralLocationCodeTextBox.Text = string.Empty;

            this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##";
            this.GerneralTotalDebitTextBox.Text = string.Empty;
            this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##0";
        }

        /// <summary>
        /// To Clear Affidavit Part Controls
        /// </summary>
        private void ClearAffidavitPartControls()
        {
            this.StreetAddressTextBox.Text = string.Empty;
            this.NameOfLocationTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtExemptRegNumberTextBox.Text = string.Empty;
            this.AffDvtDescTextBox.Text = string.Empty;
            this.AffDvtExemptionCodeTextBox.Text = string.Empty;
            this.AffDvtExemptionDescrTextBox.Text = string.Empty;

            ////to empty the combo boxs
            this.PartialSaleComboTextBox.Text = string.Empty;
            this.PartialSaleCombo.Text = string.Empty;
            this.SegregatedComboTextBox.Text = string.Empty;
            this.SegregatedComboBox.Text = string.Empty;
            this.LocationofSaleComboTextBox.Text = string.Empty;
            this.LocationofSaleComboBox.Text = string.Empty;
            this.AffdvtForestTextBox.Text = string.Empty;
            this.AffdvtForestCombo.Text = string.Empty;
            this.AffidvtOpenSpaceTextBox.Text = string.Empty;
            this.AffidvtOpenSpaceComboBox.Text = string.Empty;
            this.AffDvtHistoryTextBox.Text = string.Empty;
            this.AffDvtHistoryComboBox.Text = string.Empty;
            this.AffDvtContinuanceTextBox.Text = string.Empty;
            this.AffDvtContinuanceComboBox.Text = string.Empty;
            this.GeneralLocationCodeTextBox.Text = string.Empty;

            this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##";
            this.GerneralTotalDebitTextBox.Text = string.Empty;
            this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##0";
        }

        /// <summary>
        /// Loads the aff DVT combo.
        /// </summary>
        private void LoadAffDvtCombo()
        {
            InitComboBoxValues(this.SuppliA1ComboBox);
            InitComboBoxValues(this.SuppliA2ComboBox);
            InitComboBoxValues(this.PartialSaleCombo);
            InitComboBoxValues(this.SegregatedComboBox);
            InitComboBoxValues(this.AffdvtForestCombo);
            InitContinuanceSpaceComboBoxValues(this.AffidvtOpenSpaceComboBox);
            InitComboBoxValues(this.AffDvtHistoryComboBox);
            InitContinuanceSpaceComboBoxValues(this.AffDvtContinuanceComboBox);
            ////InitComboBoxValues(this.PersonlaPropertyComboBox);

            this.LocationofSaleComboBox.Items.Clear();
            this.LocationofSaleComboBox.Items.Insert(0, "CITY");
            this.LocationofSaleComboBox.Items.Insert(1, "COUNTY");
        }

        /// <summary>
        /// Loads the aff DVT.
        /// </summary>
        /// <param name="affDvtRowNo">The aff DVT row no.</param>
        private void LoadAffDvtValue(int affDvtRowNo)
        {
            this.Cursor = Cursors.WaitCursor;
            this.StreetAddressTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn].ToString();
            this.AffDvtDescTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn].ToString();
            SetComboboxValue(this.AffdvtForestCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn].ToString());
            SetComboboxValue(this.AffidvtOpenSpaceComboBox, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn].ToString());
            SetComboboxValue(this.AffDvtHistoryComboBox, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn].ToString());
            SetComboboxValue(this.AffDvtContinuanceComboBox, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn].ToString());
            if (string.Compare(this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn].ToString(), "0") == 0)
            {
                this.LocationofSaleComboBox.SelectedIndex = 0;
            }
            else
            {
                this.LocationofSaleComboBox.SelectedIndex = 1;
            }

            SetComboboxValue(this.PartialSaleCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn].ToString());
            SetComboboxValue(this.SegregatedComboBox, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn].ToString());
            this.NameOfLocationTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn].ToString();
            this.AffdvtUseCodeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn].ToString().Replace("*", " ");
            this.AffdvtExemptRegNumberTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn].ToString();
            this.AffDvtExemptionCodeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn].ToString();
            this.AffDvtExemptionDescrTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn].ToString();
            this.GeneralLocationCodeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn].ToString();

            if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.TotalDebtColumn].ToString()))
            {
                this.GerneralTotalDebitTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.TotalDebtColumn].ToString();
            }
            else
            {
                this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##";
                this.GerneralTotalDebitTextBox.Text = string.Empty;
                this.GerneralTotalDebitTextBox.TextCustomFormat = "#,##0";
            }

            this.Cursor = Cursors.Default;
        }

        #endregion Affidavit Methods

        #region Affidavit Events

        /// <summary>
        /// Handles the CallUpdate event of the AffdvtTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffdvtTextBox_CallUpdate(object sender, KeyEventArgs e)
        {
            try
            {
                ////if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                ////{
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                ////this.SetAffiDvtToUpdateMode();
                ////}
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Affidavit Events

        #endregion Coding For Affidvt

        #region Coding For CalcDue

        #region Amount Due Methods

        /// <summary>
        /// Sets the color of the calu due buttons BG.
        /// </summary>
        private void SetCaluDueButtonsBGColor()
        {
            this.GeneralHeaderPaymentDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralHeaderFormDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.PaymentDatePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.FromDatePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.AffDvtDocDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.AffDvtDatePanle.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.CalcDueSellingPricePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.CalcDueSellingPriceTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.TaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralDistrictLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralDistrictLinkLabel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralDistrictPictureBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// To Clear Amount Due Part Conrols
        /// </summary>
        private void ClearAmountDuePartConrols()
        {
            this.CalcDueSellingPriceTextBox.Text = string.Empty;
            this.CalcDuePerPropertTextBox.Text = string.Empty;
            this.CalDueRealPropTextBox.Text = string.Empty;
            this.TaxableSaleTextBox.Text = string.Empty;
            this.CalcDueExciseTaxTextBox.Text = string.Empty;
            this.CalcDueExcisTaxLocaltextBox.Text = string.Empty;
            this.CalcDueDelinqIntStateTextBox.Text = string.Empty;
            this.CalcDueDelinqIntLocalTextBox.Text = string.Empty;
            this.CalcDueDelinqPenaltyTextBox.Text = string.Empty;
            this.CalcDueTechFeeTextBox.Text = string.Empty;
            this.CalcDueTransFeeTextBox.Text = string.Empty;
            this.CalcDueSubTotalTextBox.Text = string.Empty;
            this.CalcDueFeesTextBox.Text = string.Empty;
            this.CalcDueTtlAmountTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Sets the calc due text box.
        /// </summary>
        /// <param name="calDueRowId">The cal due row id.</param>
        private void SetCalcDueTextBox(int calDueRowId)
        {
            this.Cursor = Cursors.WaitCursor;
            this.CalcDueSellingPriceTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.GrossSalePriceColumn].ToString();
            this.CalcDuePerPropertTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.PersonalPropAmtColumn].ToString();
            this.CalDueRealPropTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.RealPropExemptAmtColumn].ToString();
            this.TaxableSaleTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn].ToString();
            ////to make the back color to white
            ////this.TaxableSaleTextBox.BackColor = Color.White;
            ////this.TaxableSalePanel.BackColor = Color.White;
            this.CalcDueExciseTaxTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxStateColumn].ToString();
            this.CalcDueExcisTaxLocaltextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxLocalColumn].ToString();
            this.CalcDueDelinqIntStateTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestStateColumn].ToString();
            this.CalcDueDelinqIntLocalTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestLocalColumn].ToString();
            this.CalcDueDelinqPenaltyTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentPenaltyColumn].ToString();
            this.CalcDueTechFeeTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.TechnologyFeeColumn].ToString();
            this.CalcDueTransFeeTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.TransactionFeeColumn].ToString();
            this.CalcDueSubTotalTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.SubTotalColumn].ToString();
            this.CalcDueFeesTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.FeesColumn].ToString();
            this.CalcDueTtlAmountTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.TotalAmountDueColumn].ToString();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Calulates the taxable sale price.
        /// </summary>
        private void CalulateTaxableSalePrice()
        {
            this.Cursor = Cursors.WaitCursor;
            double sellingPrice = 0.00;
            double propertPrice = 0.00;
            if (!string.IsNullOrEmpty(this.CalcDueSellingPriceTextBox.Text.Trim().ToString()))
            {
                sellingPrice = Double.Parse(this.CalcDueSellingPriceTextBox.Text.Trim().ToString());
                sellingPrice = Math.Round(sellingPrice, 2);
                this.CalcDueSellingPricePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.CalcDueSellingPriceTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }

            if ((!string.IsNullOrEmpty(this.CalcDuePerPropertTextBox.Text.Trim().ToString())) && (!string.IsNullOrEmpty(this.CalDueRealPropTextBox.Text.Trim().ToString())))
            {
                propertPrice = Math.Round(Double.Parse(this.CalcDuePerPropertTextBox.Text.Trim().ToString()), 2) + Math.Round(Double.Parse(this.CalDueRealPropTextBox.Text.Trim().ToString()), 2);
            }
            //// this.CalcDueTaxableSaleTextBox.Text = ((Convert.ToDecimal(CalcDueSellingPriceTextBox.Text.Trim().ToString()) ֠(Convert.ToDecimal(this.CalcDuePerPropertTextBox.Text.Trim().ToString()) + (Convert.ToDecimal(this.CalDueRealPropTextBox.Text.Trim().ToString())));
            double taxSalesAmount = sellingPrice - propertPrice;
            this.TaxableSaleTextBox.Text = taxSalesAmount.ToString();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Calcs the sub total.
        /// </summary>
        private void CalcSubTotal()
        {
            double exciseTaxState = 0.00;
            double exciseTaxLocal = 0.00;
            double delqInterestState = 0.00;
            double delqInterestLocal = 0.00;
            double delqPenalty = 0.00;

            this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.CalcDueExciseTaxTextBox.Text.Trim().ToString()))
            {
                exciseTaxState = Double.Parse(this.CalcDueExciseTaxTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueExcisTaxLocaltextBox.Text.Trim().ToString()))
            {
                exciseTaxLocal = Double.Parse(this.CalcDueExcisTaxLocaltextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqIntStateTextBox.Text.Trim().ToString()))
            {
                delqInterestState = Double.Parse(this.CalcDueDelinqIntStateTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqIntLocalTextBox.Text.Trim().ToString()))
            {
                delqInterestLocal = Double.Parse(this.CalcDueDelinqIntLocalTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqPenaltyTextBox.Text.Trim().ToString()))
            {
                delqPenalty = Double.Parse(this.CalcDueDelinqPenaltyTextBox.Text.Trim().ToString());
            }

            double subTotal = exciseTaxState + exciseTaxLocal + delqInterestState + delqInterestLocal + delqPenalty;
            this.CalcDueSubTotalTextBox.Text = subTotal.ToString();
            this.CalcTotalAmountDue();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Calcs the tax total.  
        /// SUM of the form fields TechnologyFee and Transaction Fee.
        /// </summary>
        private void CalcTaxTotal()
        {
            double tecFee = 0.00;
            double tranFee = 0.00;

            this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.CalcDueTechFeeTextBox.Text.Trim().ToString()))
            {
                tecFee = Double.Parse(this.CalcDueTechFeeTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueTransFeeTextBox.Text.Trim().ToString()))
            {
                tranFee = Double.Parse(this.CalcDueTransFeeTextBox.Text.Trim().ToString());
            }

            double feeTotal = tecFee + tranFee;
            this.CalcDueFeesTextBox.Text = feeTotal.ToString();
            this.CalcTotalAmountDue();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Calcs the tax total.  
        /// SUM of the form fields TechnologyFee and Transaction Fee.
        /// </summary>
        private void CalcTotalAmountDue()
        {
            double subTotal = 0.00;
            double fees = 0.00;

            this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.CalcDueSubTotalTextBox.Text.Trim().ToString()))
            {
                subTotal = Double.Parse(this.CalcDueSubTotalTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueFeesTextBox.Text.Trim().ToString()))
            {
                fees = Double.Parse(this.CalcDueFeesTextBox.Text.Trim().ToString());
            }

            double amountDue = subTotal + fees;

            ////if the TaxCode is Exempt
            if (this.selectedTaxCode == 1)
            {
                this.CalcDueTtlAmountTextBox.Text = amountDue.ToString();
            }
            else
            {
                this.CalCulateTransFee();
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Calc the Trans fee when tax Code is Taxable
        /// </summary>
        private void CalCulateTransFee()
        {
            double taxableTransFee = 0.00;
            double taxableTechFee = 0.00;
            double taxableTotalAmout = 0.00;
            double taxableFee = 0.00;
            double taxableSubtotal = 0.00;

            double taxableFinaltotalAmount = 0.00;

            double taxableexciseTaxState = 0.00;
            double taxableexciseTaxLocal = 0.00;
            double taxabledelqInterestState = 0.00;
            double taxabledelqInterestLocal = 0.00;
            double taxabledelqPenalty = 0.00;

            if (!string.IsNullOrEmpty(this.CalcDueExciseTaxTextBox.Text.Trim().ToString()))
            {
                taxableexciseTaxState = Double.Parse(this.CalcDueExciseTaxTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueExcisTaxLocaltextBox.Text.Trim().ToString()))
            {
                taxableexciseTaxLocal = Double.Parse(this.CalcDueExcisTaxLocaltextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqIntStateTextBox.Text.Trim().ToString()))
            {
                taxabledelqInterestState = Double.Parse(this.CalcDueDelinqIntStateTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqIntLocalTextBox.Text.Trim().ToString()))
            {
                taxabledelqInterestLocal = Double.Parse(this.CalcDueDelinqIntLocalTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueDelinqPenaltyTextBox.Text.Trim().ToString()))
            {
                taxabledelqPenalty = Double.Parse(this.CalcDueDelinqPenaltyTextBox.Text.Trim().ToString());
            }

            taxableSubtotal = taxableexciseTaxState + taxableexciseTaxLocal + taxabledelqInterestState + taxabledelqInterestLocal + taxabledelqPenalty;
            this.CalcDueSubTotalTextBox.Text = taxableSubtotal.ToString();

            if (!string.IsNullOrEmpty(this.CalcDueTechFeeTextBox.Text.Trim().ToString()))
            {
                taxableTechFee = Double.Parse(this.CalcDueTechFeeTextBox.Text.Trim().ToString());
            }

            if (!string.IsNullOrEmpty(this.CalcDueTransFeeTextBox.Text.Trim().ToString()))
            {
                taxableTransFee = Double.Parse(this.CalcDueTransFeeTextBox.Text.Trim().ToString());
            }

            ////calculate the total taxable Sub total
            taxableTotalAmout = taxableSubtotal + taxableTechFee + taxableTransFee;

            if (taxableTotalAmout > 10)
            {
                this.CalcDueSubTotalTextBox.Text = taxableSubtotal.ToString();

                taxableTransFee = 10 - (taxableSubtotal + taxableTechFee);

                if (taxableTransFee > 0)
                {
                    this.CalcDueTransFeeTextBox.Text = taxableTransFee.ToString();
                }
                else
                {
                    this.CalcDueTransFeeTextBox.Text = string.Empty;
                    taxableTransFee = 0.00;
                }

                taxableFee = taxableTechFee + taxableTransFee;
                this.CalcDueFeesTextBox.Text = taxableFee.ToString();

                taxableFinaltotalAmount = taxableSubtotal + taxableFee;

                this.CalcDueTtlAmountTextBox.Text = taxableFinaltotalAmount.ToString();
            }
            else
            {
                if (taxableTotalAmout == 10)
                {
                    this.CalcDueSubTotalTextBox.Text = taxableSubtotal.ToString();
                    this.CalcDueTransFeeTextBox.Text = taxableTransFee.ToString();
                    taxableFee = taxableTechFee + taxableTransFee;
                    this.CalcDueFeesTextBox.Text = taxableFee.ToString();

                    taxableFinaltotalAmount = taxableSubtotal + taxableFee;

                    this.CalcDueTtlAmountTextBox.Text = taxableTotalAmout.ToString();
                }
                else if (taxableTotalAmout < 10)
                {
                    this.CalcDueSubTotalTextBox.Text = taxableSubtotal.ToString();
                    taxableTransFee = 10 - (taxableSubtotal + taxableTechFee);

                    this.CalcDueTransFeeTextBox.Text = taxableTransFee.ToString();

                    taxableFee = taxableTechFee + taxableTransFee;
                    this.CalcDueFeesTextBox.Text = taxableFee.ToString();
                    taxableFinaltotalAmount = taxableSubtotal + taxableFee;
                    this.CalcDueTtlAmountTextBox.Text = taxableFinaltotalAmount.ToString();
                }
            }
        }

        /// <summary>
        /// Clears the amount due controls.
        /// Int all text box with defult value of this.currencyFormat
        /// </summary>
        private void ClearAmountDueControls()
        {
            this.CalcDueSellingPriceTextBox.Text = this.currencyFormat;
            this.CalcDuePerPropertTextBox.Text = this.currencyFormat;
            this.CalDueRealPropTextBox.Text = this.currencyFormat;
            this.TaxableSaleTextBox.Text = this.currencyFormat;
            this.CalcDueExciseTaxTextBox.Text = this.currencyFormat;
            this.CalcDueExcisTaxLocaltextBox.Text = this.currencyFormat;
            this.CalcDueDelinqIntStateTextBox.Text = this.currencyFormat;
            this.CalcDueDelinqIntLocalTextBox.Text = this.currencyFormat;
            this.CalcDueDelinqPenaltyTextBox.Text = this.currencyFormat;
            this.CalcDueTechFeeTextBox.Text = this.currencyFormat;
            this.CalcDueTransFeeTextBox.Text = this.currencyFormat;
            this.CalcDueSubTotalTextBox.Text = this.currencyFormat;
            this.CalcDueFeesTextBox.Text = this.currencyFormat;
            this.CalcDueTtlAmountTextBox.Text = this.currencyFormat;
        }

        /// <summary>
        /// Sets the amount due calulate value.
        /// </summary>
        private void SetAmountDueCalulateValue()
        {
            //// Checks For Valid DataSet
            if (this.validAmountDueDataset)
            {
                this.Cursor = Cursors.WaitCursor;
                this.CalcDueExciseTaxTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.ExciseTaxStateColumn.ColumnName].ToString();
                this.CalcDueExcisTaxLocaltextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.ExciseTaxLocalColumn.ColumnName].ToString();
                this.CalcDueDelinqIntStateTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.DelinquentInterestStateColumn.ColumnName].ToString();
                this.CalcDueDelinqIntLocalTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.DelinquentInterestLocalColumn.ColumnName].ToString();
                this.CalcDueDelinqPenaltyTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.DelinquentPenaltyColumn.ColumnName].ToString();
                this.CalcDueTechFeeTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.TechnologyFeeColumn.ColumnName].ToString();
                this.CalcDueTransFeeTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.CalAmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.TransactionFeeColumn.ColumnName].ToString();
                this.Cursor = Cursors.Default;
            }
            else
            {
                this.CalcDueDelinqIntStateTextBox.Text = this.currencyFormat;
                this.CalcDueExcisTaxLocaltextBox.Text = this.currencyFormat;
                this.CalcDueDelinqIntStateTextBox.Text = this.currencyFormat;
                this.CalcDueDelinqIntLocalTextBox.Text = this.currencyFormat;
                this.CalcDueDelinqPenaltyTextBox.Text = this.currencyFormat;
                this.CalcDueTechFeeTextBox.Text = this.currencyFormat;
                this.CalcDueTransFeeTextBox.Text = this.currencyFormat;
                this.CalcDueTtlAmountTextBox.Text = this.currencyFormat;
            }
        }

        /// <summary>
        /// Checks for amount due requried field.
        /// </summary>
        /// <returns>All Require filed are filled then it return true else false</returns>
        private bool CheckForAmountDueRequriedField()
        {
            bool validField = true;
            bool validExRate = true;

            if (string.IsNullOrEmpty(this.GeneralHeaderFormDateTextBox.Text.Trim()))
            {
                this.GeneralHeaderFormDateTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.FromDatePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
            }
            else
            {
                this.GeneralHeaderFormDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.FromDatePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }

            if (string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
            {
                this.AffDvtDocDateTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.AffDvtDatePanle.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
            }
            else
            {
                this.AffDvtDocDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.AffDvtDatePanle.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }

            if (string.Compare(this.taxCode, SharedFunctions.GetResourceString("TAXABLEValue")) == 0)
            {
                if (!string.IsNullOrEmpty(this.TaxableSaleTextBox.Text.Trim()))
                {
                    if (Double.Parse(this.TaxableSaleTextBox.Text.Trim()) <= 0)
                    {
                        this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                        this.TaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                        validField = false;
                    }
                    else
                    {
                        this.TaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        validField = true;
                    }
                }
            }
            else
            {
                this.TaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }

            if (int.Parse(this.exciseRateId) <= 0)
            {
                this.GeneralDistrictLinkLabel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.GeneralDistrictLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.GeneralDistrictPictureBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                validExRate = false;
            }
            else
            {
                this.GeneralDistrictLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.GeneralDistrictLinkLabel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.GeneralDistrictPictureBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                validExRate = true;
            }

            ////form date is passed instead of PaymentDate
            ////if (validField && validExRate && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralHeaderPaymentDateTextBox.Text.Trim()))
            if (validField && validExRate && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralHeaderFormDateTextBox.Text.Trim()))
            {
                validExRate = true;
            }
            else
            {
                validField = false;
            }

            return validField;
        }

        #endregion Amount Due Methods

        #region Amount Due Events

        /// <summary>
        /// Handles the KeyPress event of the CalcDueSellingPriceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CalcDueSellingPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.CalcDueSellingPriceTextBox.Text.Trim()))
                {
                    this.CalcDueSellingPriceTextBox.Text = this.currencyFormat;
                    ////this.SetAffiDvtToUpdateMode();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the CalcDueTechFeeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcTaxTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                ////if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                ////{
                this.CalcTaxTotal();
                ////}
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the CalcuDueCommandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcuDueCommandButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.Compare(this.GeneralTaxCodeConboBox.SelectedItem.ToString(), SharedFunctions.GetResourceString("TAXABLEValue")) == 0)
                {
                    this.selectedTaxCode = 0;
                    this.taxCode = SharedFunctions.GetResourceString("TAXABLEValue");
                }
                else
                {
                    this.selectedTaxCode = 1;
                    this.taxCode = SharedFunctions.GetResourceString("EXEMPTValue");
                }

                //// Checks All  Mandatory fields are filled or not;
                if (this.CheckForAmountDueRequriedField())
                {
                    ////form date is passed instead of PaymentDate
                    ////this.exciseTaxAffDvtAmountDueDataset = this.form15010Control.WorkItem.F15010_GetExciseTaxAffidavitCalulateAmountDue(Convert.ToDateTime(this.AffDvtDocDateTextBox.Text.Trim()), Convert.ToDateTime(this.GeneralHeaderPaymentDateTextBox.Text.Trim()), Convert.ToInt32(this.exciseRateId), this.selectedTaxCode, double.Parse(this.TaxableSaleTextBox.Text.Trim()));
                    this.exciseTaxAffDvtAmountDueDataset = this.form15010Control.WorkItem.F15010_GetExciseTaxAffidavitCalulateAmountDue(Convert.ToDateTime(this.AffDvtDocDateTextBox.Text.Trim()), Convert.ToDateTime(this.GeneralHeaderFormDateTextBox.Text.Trim()), Convert.ToInt32(this.exciseRateId), this.selectedTaxCode, double.Parse(this.TaxableSaleTextBox.Text.Trim()));
                    this.validAmountDueDataset = CheckValidDataSet(this.exciseTaxAffDvtAmountDueDataset);
                    this.SetAmountDueCalulateValue();
                    this.CalulateTaxableSalePrice();
                    this.CalcTaxTotal();
                    this.CalcSubTotal();
                    if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                    {
                        this.affdvtButtonOperation = (int)ButtonOperation.Update;
                        this.SetAffDvtButton(ButtonOperation.Update);
                    }
                }
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the SelectionChangeCommitted event of the GeneralTaxCode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralTaxCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.taxCode = this.GeneralTaxCodeConboBox.SelectedItem.ToString();
                if (string.Compare(this.taxCode, SharedFunctions.GetResourceString("TAXABLEValue")) == 0)
                {
                    this.selectedTaxCode = 0;
                }
                else
                {
                    this.selectedTaxCode = 1;
                }

                this.ToEnableEditButtonInMasterForm();
                ////this.affdvtButtonOperation = (int)ButtonOperation.Update;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the CalulateTaxableSalePrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalulateTaxableSalePrice_Leave(object sender, EventArgs e)
        {
            try
            {
                this.CalulateTaxableSalePrice();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalulateTaxableSalePrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalulateTaxableSalePrice_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.CalulateTaxableSalePrice();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalcSubTotal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalcSubTotal_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.CalcSubTotal();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalcTaxTotal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalcTaxTotal_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.CalcTaxTotal();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Amount Due Events

        #endregion Coding For CalcDue

        #region Coding For Suppliment

        #region Calendra Operations

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSuppliMonthCalendarDateSeletedDate(string dateSelected)
        {
            ////this.SuppliMonthCalendar.Tag = string.Empty;
            this.SuppliInsDatePict.Focus();
            this.SuppliInstDateTextBox.Text = dateSelected;
            ////this.SuppliMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the SuppliInsDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuppliInsDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.saveChanged = true;
                this.TimerImage_Click(this.SuppliInstDateTextBox, this.InstrumentDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            ////try
            ////{
            ////    this.SuppliMonthCalendar.Visible = true;
            ////    this.SuppliMonthCalendar.ScrollChange = 1;

            ////    ////// Display the Calender control near the Calender Picture box.
            ////    this.SuppliMonthCalendar.Left = this.SuppliInDatePanel.Left + this.SuppliInsDatePict.Left + this.SuppliInsDatePict.Width;
            ////    this.SuppliMonthCalendar.Top = this.SuppliInDatePanel.Top + this.SuppliInsDatePict.Top;
            ////    this.SuppliMonthCalendar.Tag = this.SuppliInsDatePict.Tag;
            ////    this.SuppliMonthCalendar.Focus();

            ////    if (!string.IsNullOrEmpty(this.SuppliInstDateTextBox.Text))
            ////    {
            ////        this.SuppliMonthCalendar.SetDate(Convert.ToDateTime(this.SuppliInstDateTextBox.Text));
            ////    }
            ////}
            ////catch (Exception e1)
            ////{
            ////    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_Leave(object sender, EventArgs e)
        {
            try
            {
                //// this.SuppliMonthCalendar.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    /////this.SetSuppliMonthCalendarDateSeletedDate(this.SuppliMonthCalendar.SelectionStart.ToString(this.dateFormat));
                    ////this.SuppliMonthCalendar.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSuppliMonthCalendarDateSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Calendra Operations

        /// <summary>
        /// Handles the Click event of the SupplementRHPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SupplementRHPanel_Click(object sender, EventArgs e)
        {
            try
            {
                this.SuppliReasonHeldTextBox.Focus();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Clears the suppliment control.
        /// </summary>
        private void ClearSupplimentControl()
        {
            this.SuppliAgentNameTextBox.Text = string.Empty;
            this.SuppliInstTypeTextBox.Text = string.Empty;
            this.SuppliInstDateTextBox.Text = string.Empty;
            this.SuppliFirmNameTextBox.Text = string.Empty;
            this.SuppliReasonHeldTextBox.Text = string.Empty;

            this.SuppliA1ComboBox.SelectedIndex = 0;
            this.SuppliA1TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliA1GrntPaysGranTextBox.Text = string.Empty;
            this.SuppliA2ComboBox.SelectedIndex = 0;
            this.SuppliA2TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliA2GrantPaysGranTextBox.Text = string.Empty;
            this.SuppliA1DbtRateTextBox.Text = this.currencyFormat + "%";
            this.SuppliB1ComboBox.SelectedIndex = 0;
            this.SuppliB2ComboBox.SelectedIndex = 0;
            this.SuppliB1TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliB3Combo.SelectedIndex = 0;
            this.SuppliB2TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliB4Combo.SelectedIndex = 0;
            this.SuppliRefiCombo.SelectedIndex = 0;
            this.SuppliGiftedEquityTextBox.Text = this.currencyFormat;
            this.SuppliGranSignTextBox.Text = string.Empty;
            this.SuppliGranteSignTextBox.Text = string.Empty;
            this.SuppliFNameTextBox.Text = string.Empty;
            this.SuppliGNameTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Loads the suppliment combo.
        /// </summary>
        private void LoadSupplimentCombo()
        {
            InitComboBoxValues(this.SuppliA1ComboBox);
            InitComboBoxValues(this.SuppliA2ComboBox);
            InitComboBoxValues(this.SuppliB1ComboBox);
            InitComboBoxValues(this.SuppliB2ComboBox);
            InitComboBoxValues(this.SuppliB3Combo);
            InitComboBoxValues(this.SuppliB4Combo);
            InitComboBoxValues(this.SuppliRefiCombo);
            InitComboBoxValues(this.SuppliRefiCombo);
        }

        /// <summary>
        /// Sets the suppliment text box.
        /// </summary>
        /// <param name="supplimentRowId">The suppliment row id.</param>
        private void SetSupplimentTextBox(int supplimentRowId)
        {
            this.Cursor = Cursors.WaitCursor;
            this.SuppliAgentNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AgentNameColumn].ToString();
            this.SuppliInstTypeTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentTypeColumn].ToString();
            this.SuppliInstDateTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentDateColumn].ToString();
            this.SuppliFirmNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.FirmnameColumn].ToString();
            this.SuppliReasonHeldTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.ReasonHeldColumn].ToString();
            SetComboboxValue(this.SuppliA1ComboBox, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column].ToString());
            this.SuppliA1TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A1Column].ToString();
            this.SuppliA1GrntPaysGranTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A1Column].ToString();
            SetComboboxValue(this.SuppliA2ComboBox, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column].ToString());
            this.SuppliA2TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A2Column].ToString();
            this.SuppliA2GrantPaysGranTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A2Column].ToString();
            this.SuppliA1DbtRateTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.DebtRate_A2Column].ToString();
            SetComboboxValue(this.SuppliB1ComboBox, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column].ToString());
            SetComboboxValue(this.SuppliB2ComboBox, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column].ToString());
            this.SuppliB1TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B2Column].ToString();
            this.SuppliB2TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B3Column].ToString();
            SetComboboxValue(this.SuppliB3Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B3Column].ToString());
            SetComboboxValue(this.SuppliB4Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column].ToString());
            SetComboboxValue(this.SuppliRefiCombo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn].ToString());
            this.SuppliGiftedEquityTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftedEquityColumn].ToString();
            this.SuppliGranSignTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GrantorsSignatureColumn].ToString();
            this.SuppliGranteSignTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteesSignatureColumn].ToString();
            this.SuppliGNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn].ToString();
            this.SuppliFNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.FacilitatorNameColumn].ToString();
            this.assessorDesc = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn].ToString();
            this.treasurerDesc = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn].ToString();
            this.assessorStatus = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorStatusIDColumn].ToString();
            this.treasurerStatus = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerStatusIDColumn].ToString();
            ////To assgin the Treasure status and desc in the db on Load
            /////when modified unsavedTreasurerStatus and unsavedTreasurerDesc is used to save the unsaved status and desc in the db
            this.unsavedTreasurerStatus = this.treasurerStatus;
            this.unsavedTreasurerDesc = this.treasurerDesc;
            ////To assgin the Assessor status and desc in the db on Load
            /////when modified unsavedAssessorStatus and unsavedAssessorDesc is used to save the unsaved status and desc in the db

            this.assessorStatusModified = false;
            this.treasurerStatusModified = false;

            this.treasurerUserName = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedBy_NameColumn].ToString();
            int.TryParse(this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedByColumn].ToString(), out this.treasurerUserId);

            this.treasurerUpdatedTime = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedDateColumn].ToString();

            DateTime tempTreasurerDateTime;
            if (DateTime.TryParse(this.treasurerUpdatedTime, out tempTreasurerDateTime))
            {
                this.treasurerUpdatedTime = String.Concat(tempTreasurerDateTime.ToShortDateString(), " ", tempTreasurerDateTime.ToShortTimeString().ToLower().Replace(" ", ""));
            }
            else
            {
                this.treasurerUpdatedTime = String.Empty;
            }

            this.assessorUserName = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedBy_NameColumn].ToString();
            int.TryParse(this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedByColumn].ToString(), out this.assessorUserId);
            this.assessorUpdatedTime = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedDateColumn].ToString();

            DateTime tempAssessorDateTime;
            if (DateTime.TryParse(this.assessorUpdatedTime, out tempAssessorDateTime))
            {
                this.assessorUpdatedTime = String.Concat(tempAssessorDateTime.ToShortDateString(), " ", tempAssessorDateTime.ToShortTimeString().ToLower().Replace(" ", ""));
            }
            else
            {
                this.assessorUpdatedTime = String.Empty;
            }

            this.unsavedAssessorStatus = this.assessorStatus;
            this.unsavedAssessorDesc = this.assessorDesc;
            this.TreasurerStatusButton.Text = "Treasurer - " + this.treasurerDesc;
            this.AssessorStatusButton.Text = "Assessor - " + this.assessorDesc;
            if (this.assessorDesc == "Approved")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.assessorDesc == "Rejected")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.assessorDesc == "Unverified")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }

            if (this.treasurerDesc == "Approved")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.treasurerDesc == "Rejected")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.treasurerDesc == "Unverified")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }

            this.CalcSubTotal();

            this.Cursor = Cursors.Default;
        }

        #endregion Coding For Suppliment

        #region Coding for General

        #region GeneralPart methods

        /// <summary>
        /// Sets the general header field new mode.
        /// </summary>
        private void SetGeneralHeaderFieldNewMode()
        {
            this.StatementIDTextBox.Text = string.Empty;
            this.StatementNumberTextBox.Text = string.Empty;
            ////this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
            //// this.ChangeDateBackGround(this.GeneralHeaderPaymentDateTextBox);
            ////this.GeneralHeaderFormDateTextBox.Text = this.GetNexteceiptWorkingDay(DateTime.Today);
            ////this.ChangeDateBackGround(this.GeneralHeaderFormDateTextBox);

            // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
            this.GeneralHeaderPaymentDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
            this.GeneralHeaderFormDateTextBox.Text = TerraScanCommon.InterestDate.ToShortDateString();

            this.GeneralSubmittedDateTextBox.Text = "N/A";
            this.CreatedBYTextBox.Text = string.Empty;
            //this.GeneralFromWebTextBox.Text = string.Empty;

            this.GeneralTaxCodeConboBox.SelectedIndex = 0;
            this.GeneralMobileHomeComboBox.SelectedIndex = 0;
            this.GeneralReceiptNoLinkLabel.Text = string.Empty;
            this.GeneralNoteTextBox.Text = string.Empty;
            this.GeneralDistrictLinkLabel.Text = string.Empty;
            SetComboboxValue(this.LocationofSaleComboBox, "COUNTY");
            this.AffdvtForestCombo.SelectedIndex = 0;
            this.PersonlaPropertyComboBox.SelectedIndex = 0;
            this.taxCode = this.GeneralTaxCodeTextBox.Text.Trim();
            this.exciseRateId = "0";
            //this.AffDvtDocumentTypeTextBox.Text = string.Empty;
            this.AffDvtDocDateTextBox.Text = string.Empty;

            this.DeedTypeComboBox.SelectedIndex = 0;
            this.SourceComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets the general combo box.
        /// </summary>
        private void SetGeneralComboBox()
        {
            this.GeneralTaxCodeConboBox.Items.Clear();
            this.GeneralMobileHomeComboBox.Items.Clear();
            this.GeneralTaxCodeConboBox.Items.Insert(0, SharedFunctions.GetResourceString("TAXABLEValue"));
            this.GeneralTaxCodeConboBox.Items.Insert(1, SharedFunctions.GetResourceString("EXEMPTValue"));
            this.GeneralMobileHomeComboBox.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.GeneralMobileHomeComboBox.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));

            // Binf DeedType Combobox
            try
            {
                this.DeedTypeComboBox.DataSource = this.exciseIndividualtype.ExciseDeedType;
                this.DeedTypeComboBox.ValueMember = this.exciseIndividualtype.ExciseDeedType.DeedTypeIDColumn.ColumnName;
                this.DeedTypeComboBox.DisplayMember = this.exciseIndividualtype.ExciseDeedType.DeedTypeColumn.ColumnName;
            }
            catch (Exception ex)
            {
            }

            // Bind Source combobox
            this.SourceComboBox.DataSource = this.exciseIndividualtype.ExciseSource.DefaultView;
            this.SourceComboBox.ValueMember = this.exciseIndividualtype.ExciseSource.SourceIDColumn.ColumnName;
            this.SourceComboBox.DisplayMember = this.exciseIndividualtype.ExciseSource.SourceColumn.ColumnName;
        }

        /// <summary>
        /// Fills the general header.
        /// </summary>
        /// <param name="dataSetRowNO">The data set row NO.</param>
        private void FillGeneralHeaderText(int dataSetRowNO)
        {
            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                this.Cursor = Cursors.WaitCursor;

                this.StatementIDTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.StatementIDColumn].ToString();
                this.StatementNumberTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.StatementNumberColumn].ToString();

                this.GeneralHeaderPaymentDateTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.PaymentDateColumn].ToString();

                ////to modify the back color 
                ////this.GetNexteceiptWorkingDay(this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue);
                this.ChangeDateBackGround(this.GeneralHeaderPaymentDateTextBox);
                this.GeneralHeaderFormDateTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.FormDateColumn].ToString();
                ////to modify the back color 
                ////this.GetNexteceiptWorkingDay(this.GeneralHeaderFormDateTextBox.DateTextBoxValue);
                this.ChangeDateBackGround(this.GeneralHeaderFormDateTextBox);

                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExciseRateDisableColumn].ToString()))
                {
                    //this.isDistrictEditable;
                    byte.TryParse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExciseRateDisableColumn].ToString(), out this.isDistrictEditable);
                    if (this.isDistrictEditable > 0)
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }
                    else
                    {
                        if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = true;
                        }
                        else
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = false;
                        }
                    }
                }
                else
                {
                    this.GeneralDistrictLinkLablePanel.Enabled = true;
                }

                this.GeneralDistrictLinkLabel.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DistrictColumn].ToString();

                if (!string.IsNullOrEmpty(this.GeneralDistrictLinkLabel.Text.Trim()))
                {
                    this.isDistrictChanged = true;
                }
                else
                {
                    this.isDistrictChanged = false;
                }

                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn].ToString()))
                {
                    this.GeneralSubmittedDateTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn].ToString();
                }
                else
                {
                    this.GeneralSubmittedDateTextBox.Text = "N/A";
                }

                //if (string.Compare(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn].ToString().ToUpperInvariant(), "TRUE") == 0)
                //{
                //    this.GeneralFromWebTextBox.Text = SharedFunctions.GetResourceString("YESValue");
                //}
                //else
                //{
                //    this.GeneralFromWebTextBox.Text = SharedFunctions.GetResourceString("NOValue");
                //}

                SetComboboxValue(this.GeneralTaxCodeConboBox, this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExemptColumn].ToString());
                if (string.Compare(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExemptColumn].ToString().ToUpperInvariant(), "FALSE") == 0)
                {
                    this.taxCode = SharedFunctions.GetResourceString("TAXABLEValue");
                    this.selectedTaxCode = 0;
                }
                else
                {
                    this.taxCode = SharedFunctions.GetResourceString("EXEMPTValue");
                    this.selectedTaxCode = 1;
                }

                SetComboboxValue(this.GeneralMobileHomeComboBox, this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn].ToString());

                /////To get the Receipt no 
                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ReceiptIDColumn].ToString()))
                {
                    this.receiptId = int.Parse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ReceiptIDColumn].ToString());
                }
                else
                {
                    this.receiptId = 0;
                }

                this.GeneralReceiptNoLinkLabel.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ReceiptNumberColumn].ToString();
                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.AmendByColumn].ToString()))
                {
                    this.Amend = true;
                }
                else
                {
                    this.Amend = false;
                }
                if (!String.IsNullOrEmpty(this.GeneralReceiptNoLinkLabel.Text.Trim()))
                {
                    this.receiptIDExist = true;
                }
                else
                {
                    this.receiptIDExist = false;
                }

                this.GeneralNoteTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DORNoteColumn].ToString();
                this.exciseRateId = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ExciseRateIDColumn].ToString();

                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedByColumn].ToString()))
                {

                    ////to get the Submited By id
                    ////this.submittedBy = int.Parse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedByColumn].ToString());
                    this.submittedBy = true;
                }
                else
                {
                    ////this.submittedBy = -1;
                    this.submittedBy = false;
                }

                ////used to get the roll year 
                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.RollYearColumn].ToString()))
                {
                    this.rollYear = int.Parse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.RollYearColumn].ToString());
                }

                this.CreatedBYTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.CreatedByColumn].ToString();
                //this.AffDvtDocumentTypeTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString();
                this.AffDvtDocDateTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentDateColumn].ToString();

                byte isVoid = 0;
                if (this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsVoidColumn] != null)
                {
                    byte.TryParse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsVoidColumn].ToString(), out isVoid);
                }

                if (isVoid > 0)
                {
                    this.VoidButton.BackColor = Color.FromArgb(128, 0, 0);
                    this.VoidButton.Tag = 1;
                }
                else
                {
                    this.VoidButton.BackColor = Color.FromArgb(128, 128, 128);
                    this.VoidButton.Tag = 0;
                }

                if (this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn] != null)
                {
                    int comboIndex = this.DeedTypeComboBox.FindStringExact(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString());
                    //this.DeedTypeComboBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString();
                    if (comboIndex >= 0)
                    {
                        this.DeedTypeComboBox.SelectedIndex = comboIndex;
                    }
                    else
                    {
                        //this.DeedTypeComboBox.SelectedIndex = 0;

                        if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString()))
                        {
                            F15010ExciseAffidavitData.ExciseDeedTypeRow existingDeedRow = this.exciseIndividualtype.ExciseDeedType.NewExciseDeedTypeRow();
                            existingDeedRow.DeedTypeID = int.MaxValue;
                            existingDeedRow.DeedType = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString();
                            this.exciseIndividualtype.ExciseDeedType.Rows.Add(existingDeedRow);
                            //this.exciseIndividualtype.ExciseDeedType
                            // this.DeedTypeComboBox.Items.Add(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString());
                            // this.DeedTypeComboBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn].ToString();
                            //this.DeedTypeComboBox.Items.Add("Test");
                            //this.DeedTypeComboBox.Text = "Text";
                            this.DeedTypeComboBox.SelectedValue = int.MaxValue;
                        }
                    }
                }

                if (this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SourceIDColumn] != null)
                {
                    int tempId;
                    int.TryParse(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SourceIDColumn].ToString(), out tempId);
                    this.SourceComboBox.SelectedValue = tempId;
                }

                if ((this.DeedTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.DeedTypeComboBox.Text.Trim()))
                {
                    this.tempDeedTypeId = this.DeedTypeComboBox.SelectedValue.ToString();
                }
                else
                {
                    this.tempDeedTypeId = string.Empty;
                }

                if ((this.SourceComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.SourceComboBox.Text.Trim()))
                {
                    this.tempSourceId = (int)this.SourceComboBox.SelectedValue;
                }
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// To Clear General Part Controls 
        /// </summary>
        private void ClearGeneralPartControls()
        {
            this.StatementIDTextBox.Text = string.Empty;
            this.StatementNumberTextBox.Text = string.Empty;
            this.GeneralHeaderPaymentDateTextBox.Text = string.Empty;
            this.GeneralHeaderFormDateTextBox.Text = string.Empty;
            this.GeneralDistrictLinkLabel.Text = string.Empty;
            this.GeneralSubmittedDateTextBox.Text = string.Empty;
            this.CreatedBYTextBox.Text = string.Empty;
            //this.GeneralFromWebTextBox.Text = string.Empty;
            this.GeneralReceiptNoLinkLabel.Text = string.Empty;
            this.GeneralReceiptNoTextBox.Text = string.Empty;
            this.GeneralNoteTextBox.Text = string.Empty;
            ////To empty the combo boxs
            this.GeneralTaxCodeTextBox.Text = string.Empty;
            this.GeneralTaxCodeConboBox.Text = string.Empty;
            this.GeneralMobileHomeTextBox.Text = string.Empty;
            this.GeneralMobileHomeComboBox.Text = string.Empty;
            //this.AffDvtDocumentTypeTextBox.Text = string.Empty;
            this.AffDvtDocDateTextBox.Text = string.Empty;

            this.SourceComboBox.Text = string.Empty;
            this.DeedTypeComboBox.Text = string.Empty;
        }

        /// <summary>
        /// Changes the date back ground with today.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        private void ChangeDateBackGround(TerraScanTextBox sourceTextBox)
        {
            DateTime currentWorkingDateTime;
            DateTime.TryParse(this.GetNexteceiptWorkingDay(sourceTextBox.DateTextBoxValue), out currentWorkingDateTime);
            ////change background color to red if date is not today
            ////if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) || string.IsNullOrEmpty(sourceTextBox.Text) || sourceTextBox.DateTextBoxValue.Equals(System.DateTime.Today))
            if (string.IsNullOrEmpty(sourceTextBox.Text) || sourceTextBox.DateTextBoxValue.Equals(currentWorkingDateTime))
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
        /// Gets the next working day and assign it to the receiptDate variable.
        /// </summary>
        /// <param name="receiptDateTime">The receipt date time.</param>
        /// <returns>the receiptdate string</returns>
        private string GetNexteceiptWorkingDay(DateTime receiptDateTime)
        {
            DateTime tempreceiptDateTime;

            ////get next day if today else update for default date management
            ////if (receiptDateTime.Equals(DateTime.Today) && this.affdvtButtonOperation == (int)ButtonOperation.New)

            if (String.IsNullOrEmpty(this.GeneralHeaderPaymentDateTextBox.Text.Trim()) && receiptDateTime.Equals(DateTime.MinValue))
            {
                ////check for valid date - if not return the empty value assigned in text box else validated value
                tempreceiptDateTime = DateTime.Now;
                return String.Empty;
            }
            else
            {
                tempreceiptDateTime = this.form15010Control.WorkItem.F9001_GetNextWorkingDay();
                return tempreceiptDateTime.ToShortDateString();
            }

            tempreceiptDateTime = receiptDateTime;
            return tempreceiptDateTime.ToString(this.dateFormat);
        }

        #endregion GeneralPart methods

        #region GeneralPart Events

        #region Deed Date Calendra Operations

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetAffDvtDocDateSeletedDate(string dateSelected)
        {
            this.AffDvtDocDateCalender.Tag = string.Empty;
            this.AffDvtDatePicture.Focus();
            this.AffDvtDocDateTextBox.Text = dateSelected;
            this.AffDvtDocDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Click event of the AffDvtDatePicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtDatePicture_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.saveChanged = true;
                this.TimerImage_Click(this.AffDvtDocDateTextBox, this.DeedTypedateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            ////try
            ////{
            ////    this.AffDvtDocDateCalender.Visible = true;
            ////    this.AffDvtDocDateCalender.ScrollChange = 1;

            ////    ////// Display the Calender control near the Calender Picture box.
            ////    ////this.PaymentDateCalender.Left = this.PaymentDatePanel.Left + this.GeneralPaymentDatePict.Left + this.GeneralPaymentDatePict.Width;
            ////    this.AffDvtDocDateCalender.Left = this.AffDvtDocumentTypePanel.Left + this.AffDvtDatePicture.Left + this.AffDvtDatePicture.Width - 43;
            ////    ////this.AffDvtDocDateCalender.Left = this.AffDvtDocumentTypePanel.Top + this.AffDvtDatePicture.Top;
            ////    this.AffDvtDocDateCalender.Top = this.AffDvtDocumentTypePanel.Top + this.AffDvtDocumentTypePanel.Top + 5;
            ////    this.AffDvtDocDateCalender.Tag = this.AffDvtDatePicture.Tag;
            ////    this.AffDvtDocDateCalender.Focus();

            ////    if (!string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text))
            ////    {
            ////        this.SuppliMonthCalendar.SetDate(Convert.ToDateTime(this.AffDvtDocDateTextBox.Text));
            ////    }
            ////}
            ////catch (Exception e1)
            ////{
            ////    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            ////}
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the AffDvtDocDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void AffDvtDocDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetAffDvtDocDateSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Leave event of the AffDvtDocDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtDocDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.AffDvtDocDateCalender.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AffDvtDocDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffDvtDocDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetAffDvtDocDateSeletedDate(this.AffDvtDocDateCalender.SelectionStart.ToString(this.dateFormat));
                    this.AffDvtDocDateCalender.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Deed Date Calendra Operations

        #region Calendra Operations

        #region PaymentDate Calendra Operations

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetGeneralPaymentSeletedDate(string dateSelected)
        {
            this.PaymentDateCalender.Tag = string.Empty;
            this.GeneralPaymentDatePict.Focus();
            this.GeneralHeaderPaymentDateTextBox.Text = dateSelected;

            TerraScanTextBox sourceTextBox = this.GeneralHeaderPaymentDateTextBox as TerraScanTextBox;
            ////this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue);                
            ////change background color with today
            this.ChangeDateBackGround(sourceTextBox);
            this.PaymentDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Leave event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.PaymentDateCalender.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetGeneralPaymentSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetGeneralPaymentSeletedDate(this.PaymentDateCalender.SelectionStart.ToString(this.dateFormat));
                    this.PaymentDateCalender.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the GeneralPaymentDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPaymentDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.saveChanged = true;
                this.TimerImage_Click(this.GeneralHeaderPaymentDateTextBox, this.ReceiptDateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            ////this.PaymentDateCalender.Visible = true;
            ////this.PaymentDateCalender.ScrollChange = 1;

            //////// Display the Calender control near the Calender Picture box.
            ////this.PaymentDateCalender.Left = this.PaymentDatePanel.Left + this.GeneralPaymentDatePict.Left + this.GeneralPaymentDatePict.Width;
            ////this.PaymentDateCalender.Top = this.PaymentDatePanel.Top + this.GeneralPaymentDatePict.Top;
            ////this.PaymentDateCalender.Tag = this.GeneralPaymentDatePict.Tag;
            ////this.PaymentDateCalender.Focus();

            ////if (!string.IsNullOrEmpty(this.GeneralHeaderPaymentDateTextBox.Text))
            ////{
            ////    this.PaymentDateCalender.SetDate(Convert.ToDateTime(this.GeneralHeaderPaymentDateTextBox.Text));
            ////}
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

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

        #endregion PaymentDate Calendra Operations

        #region FormDate Calendra Operations

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetGeneralFormDateSeletedDate(string dateSelected)
        {
            this.FormDateCalender.Tag = string.Empty;
            this.GeneralFormDatePic.Focus();
            this.GeneralHeaderFormDateTextBox.Text = dateSelected;
            TerraScanTextBox sourceTextBox = this.GeneralHeaderFormDateTextBox as TerraScanTextBox;
            ////this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue);                
            ////change background color with today
            this.ChangeDateBackGround(sourceTextBox);
            this.FormDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Leave event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.FormDateCalender.Visible = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetGeneralFormDateSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetGeneralFormDateSeletedDate(this.FormDateCalender.SelectionStart.ToString(this.dateFormat));
                    this.FormDateCalender.Visible = false;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the GeneralFormDatePic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralFormDatePic_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.saveChanged = true;
                this.TimerImage_Click(this.GeneralHeaderFormDateTextBox, this.InterestdateTimePicker);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            ////this.FormDateCalender.Visible = true;
            ////this.FormDateCalender.ScrollChange = 1;

            ////// Display the Calender control near the Calender Picture box.
            ////this.FormDateCalender.Left = this.FromDatePanel.Left + this.GeneralFormDatePic.Left + this.GeneralFormDatePic.Width;
            ////this.FormDateCalender.Top = this.FromDatePanel.Top + this.GeneralFormDatePic.Top;
            ////this.FormDateCalender.Tag = this.GeneralFormDatePic.Tag;
            ////this.FormDateCalender.Focus();

            ////if (!string.IsNullOrEmpty(this.GeneralHeaderFormDateTextBox.Text))
            ////{
            ////    this.FormDateCalender.SetDate(Convert.ToDateTime(this.GeneralHeaderFormDateTextBox.Text));

            ////    TerraScanTextBox sourceTextBox = this.GeneralHeaderFormDateTextBox as TerraScanTextBox;
            ////    ////this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue);                
            ////    ////change background color with today
            ////    this.ChangeDateBackGround(sourceTextBox);                
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion FormDate Calendra Operations

        #endregion Calendra Operations

        /// <summary>
        /// Handles the Click event of the TreasurerStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TreasurerStatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                int statementId = 0;
                int tempTreasurerStatus = 0;

                if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
                {
                    this.Cursor = Cursors.WaitCursor;
                    statementId = Convert.ToInt32(this.StatementIDTextBox.Text.Trim());
                }
                else
                {
                    statementId = 0;
                }

                int.TryParse(this.unsavedTreasurerStatus, out tempTreasurerStatus);
                Form form1111 = new Form();
                object[] optionalParameter;
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    optionalParameter = new object[] { "1111", statementId, 1, tempTreasurerStatus, this.unsavedTreasurerDesc, string.Empty, string.Empty };
                }
                else
                {
                    optionalParameter = new object[] { "1111", statementId, 1, tempTreasurerStatus, this.unsavedTreasurerDesc, this.treasurerUpdatedTime, this.treasurerUserName };
                }

                form1111 = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1111, optionalParameter, this.form15010Control.WorkItem);

                if (form1111 != null)
                {
                    if (form1111.ShowDialog() != DialogResult.Cancel)
                    {
                        this.unsavedTreasurerStatus = TerraScanCommon.GetValue(form1111, "StatusId");
                        this.unsavedTreasurerDesc = TerraScanCommon.GetValue(form1111, "Status");
                        int.TryParse(TerraScanCommon.GetValue(form1111, "StatusValidationUserID"), out this.treasurerUserId);
                        this.treasurerUserName = TerraScanCommon.GetValue(form1111, "StatusValidationUserName");
                        ////this.treasurerUpdatedTime = TerraScanCommon.GetValue(form1111, "StatusValidationUpdatedTime");
                        this.TreasurerStatusButton.Text = "Treasurer - " + this.unsavedTreasurerDesc;
                        if (this.unsavedTreasurerDesc == "Approved")
                        {
                            this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                        }
                        else if (this.unsavedTreasurerDesc == "Rejected")
                        {
                            this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        }
                        else if (this.unsavedTreasurerDesc == "Unverified")
                        {
                            this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                        }

                        this.treasurerStatusModified = true;
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
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
        /// Handles the Click event of the AssessorStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessorStatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int statementId = 0;
                int tempAssessorStatus = 0;

                if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
                {
                    statementId = Convert.ToInt32(this.StatementIDTextBox.Text.Trim());
                }
                else
                {
                    statementId = 0;
                }

                int.TryParse(this.unsavedAssessorStatus, out tempAssessorStatus);
                Form form1111 = new Form();
                object[] optionalParameter;
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    optionalParameter = new object[] { "1112", statementId, 0, tempAssessorStatus, this.unsavedAssessorDesc, string.Empty, string.Empty };
                }
                else
                {
                    optionalParameter = new object[] { "1112", statementId, 0, tempAssessorStatus, this.unsavedAssessorDesc, this.assessorUpdatedTime, this.assessorUserName };
                }

                form1111 = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1112, optionalParameter, this.form15010Control.WorkItem);
                if (form1111 != null)
                {
                    if (form1111.ShowDialog() != DialogResult.Cancel)
                    {
                        this.unsavedAssessorStatus = TerraScanCommon.GetValue(form1111, "StatusId");
                        this.unsavedAssessorDesc = TerraScanCommon.GetValue(form1111, "Status");
                        int.TryParse(TerraScanCommon.GetValue(form1111, "StatusValidationUserID"), out this.assessorUserId);
                        this.assessorUserName = TerraScanCommon.GetValue(form1111, "StatusValidationUserName");
                        ////this.assessorUpdatedTime = TerraScanCommon.GetValue(form1111, "StatusValidationUpdatedTime");
                        this.AssessorStatusButton.Text = "Assessor - " + this.unsavedAssessorDesc;
                        if (this.unsavedAssessorDesc == "Approved")
                        {
                            this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                        }
                        else if (this.unsavedAssessorDesc == "Rejected")
                        {
                            this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                        }
                        else if (this.unsavedAssessorDesc == "Unverified")
                        {
                            this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                        }

                        this.assessorStatusModified = true;
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
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
        /// Handles the LinkClicked event of the GeneralDistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GeneralDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11013);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.exciseRateId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the GeneralReceiptNoLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GeneralReceiptNoLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.receiptId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the GeneralDistrictPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralDistrictPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form districtF1102 = new Form();
                object[] optionalParameter = new object[] { this.rollYear };
                districtF1102 = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, optionalParameter, this.form15010Control.WorkItem);
                DialogResult districtDialog;
                if (districtF1102 != null)
                {
                    districtDialog = districtF1102.ShowDialog();

                    if (districtDialog == DialogResult.Yes)
                    {
                        try
                        {
                            this.exciseRateId = TerraScanCommon.GetValue(districtF1102, "ExciseRateDistrictSelectionId");
                            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_GetDistrictSelection(Convert.ToInt32(this.exciseRateId));
                            //// districtIdis not used as per client May change order
                            ////this.districtId = int.Parse(this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictIDColumn].ToString());

                            if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                            {
                                this.GeneralDistrictLinkLabel.Text = this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictColumn].ToString();
                                this.GeneralLocationCodeTextBox.Text = this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.LocationCodeColumn].ToString();
                                this.NameOfLocationTextBox.Text = this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.LocationNameColumn].ToString();
                                if (string.Compare(this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.IsCountyColumn].ToString(), "0") == 0)
                                {
                                    this.LocationofSaleComboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.LocationofSaleComboBox.SelectedIndex = 1;
                                }

                                this.isDistrictChanged = true;

                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                    }
                    else if (districtDialog == DialogResult.Ignore)
                    {
                        try
                        {
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(11013);
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the GeneralHeaderPaymentDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralHeaderPaymentDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;
                ////this.GeneralHeaderPaymentDateTextBox.Text = this.GetNexteceiptWorkingDay(this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue);                
                ////change background color with today
                this.ChangeDateBackGround(sourceTextBox);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the GeneralHeaderFormDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralHeaderFormDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                TerraScanTextBox sourceTextBox = sender as TerraScanTextBox;
                ////this.GeneralHeaderFormDateTextBox.Text = this.GetNexteceiptWorkingDay(this.GeneralHeaderFormDateTextBox.DateTextBoxValue);
                ////change background color with today
                this.ChangeDateBackGround(sourceTextBox);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the CreatedBYTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreatedBYTextBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (this.CreatedBYTextBox.Text.Trim().Length > 12)
                {
                    this.ExciseAffidavitToolTip.RemoveAll();
                    this.ExciseAffidavitToolTip.SetToolTip(this.CreatedBYTextBox, this.CreatedBYTextBox.Text.Trim());
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the GeneralHeaderPaymentDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralHeaderPaymentDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ChangeDateBackGround(this.GeneralHeaderPaymentDateTextBox);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the GeneralHeaderFormDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralHeaderFormDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ChangeDateBackGround(this.GeneralHeaderFormDateTextBox);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion GeneralPart Events

        #endregion Coding for General

        #region Coding for Mobile Home

        #region Moblie Home Methods

        /// <summary>
        /// Custimizes the mobile home grid.
        /// </summary>
        private void CustimizeMobileHomeGrid()
        {
            this.MobileHomeGridView.AutoGenerateColumns = false;

            this.MobileHomeMake.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.MakeColumn.ColumnName;
            this.MobileHomeYear.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.YearColumn.ColumnName;
            this.MobileHomeModel.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.ModelColumn.ColumnName;
            this.MobileHomeSize.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.SizeColumn.ColumnName;
            this.MobileHomeSerial.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.SerialColumn.ColumnName;
            this.MobileHomeTaxCode.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.TaxCodeColumn.ColumnName;
            this.MobileHomeStatementID.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.StatementIDColumn.ColumnName;
            this.MobileHomeID.DataPropertyName = this.exciseTaxAffidavitDataSet.MobileHome.MobileHomeIDColumn.ColumnName;

            this.MobileHomeMake.DisplayIndex = 0;
            this.MobileHomeYear.DisplayIndex = 1;
            this.MobileHomeModel.DisplayIndex = 2;
            this.MobileHomeSize.DisplayIndex = 3;
            this.MobileHomeSerial.DisplayIndex = 4;
            this.MobileHomeTaxCode.DisplayIndex = 5;
            this.MobileHomeStatementID.DisplayIndex = 6;
            this.MobileHomeID.DisplayIndex = 7;
        }

        /// <summary>
        /// Populates the mobile home grid.
        /// </summary>
        private void PopulateMobileHomeGrid()
        {
            ////to get the roll year
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            /// changes in the County Configuration.
            this.getRollYearConfigurationValue = this.form15010Control.WorkItem.GetConfigDetails("TR_ExciseRollYear");
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                this.currentMobileHomeRollYear = int.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
            }

            int emptyRows;

            this.currentMobileHomeGridRowCout = this.exciseTaxAffidavitDataSet.MobileHome.Rows.Count;

            if (this.currentMobileHomeGridRowCout > 0)
            {
                ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                //// --- then a temp datatable with empty rows are merged with MobileHome datatable                    
                if (this.MobileHomeGridView.NumRowsVisible > this.currentMobileHomeGridRowCout)
                {
                    emptyRows = this.MobileHomeGridView.NumRowsVisible - this.currentMobileHomeGridRowCout;

                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.exciseTaxAffidavitDataSet.MobileHome.AddMobileHomeRow(this.exciseTaxAffidavitDataSet.MobileHome.NewMobileHomeRow());
                    }
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.MobileHome.AddMobileHomeRow(this.exciseTaxAffidavitDataSet.MobileHome.NewMobileHomeRow());
                }

                this.MobileHomeGridView.DataSource = this.exciseTaxAffidavitDataSet.MobileHome.DefaultView;
                TerraScanCommon.SetDataGridViewPosition(this.MobileHomeGridView, 0);
                this.MobileHomeGridView.Rows[0].Selected = true;
            }
            else
            {
                for (int i = 0; i < this.MobileHomeGridView.NumRowsVisible; i++)
                {
                    this.exciseTaxAffidavitDataSet.MobileHome.AddMobileHomeRow(this.exciseTaxAffidavitDataSet.MobileHome.NewMobileHomeRow());
                }

                this.MobileHomeGridView.DataSource = this.exciseTaxAffidavitDataSet.MobileHome.DefaultView;
                this.MobileHomeGridView.Rows[0].Selected = false;
            }

            this.MobileHomeGridView.Enabled = true;
            this.MobileHomeGridView.AutoGenerateColumns = false;

            ////to enable or disable the vertical scroll bar
            if (this.exciseTaxAffidavitDataSet.MobileHome.Rows.Count > this.MobileHomeGridView.NumRowsVisible)
            {
                this.MoblieGridViewVerticalScroll.Visible = false;
            }
            else
            {
                this.MoblieGridViewVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// Loads the mobile home part.
        /// </summary>
        private void LoadMobileHomePart()
        {
            this.PopulateMobileHomeGrid();
        }

        /// <summary>
        /// Used to clear the Mobile home grid
        /// </summary>
        private void ClearMoblieHomeGrid()
        {
            this.exciseTaxAffidavitDataSet.MobileHome.Clear();
            this.PopulateMobileHomeGrid();
        }

        #endregion Moblie Home Methods

        #region Moblie Home Events

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.MobileHomeGridView.CurrentCell != null)
                {
                    this.MobileHomeGridView.CurrentCell.ReadOnly = true;
                    this.MobileHomeGridView.Rows[e.RowIndex].Selected = true;
                }
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
        /// Handles the ColumnHeaderMouseClick event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    this.MobileHomeGridView.CurrentCell.ReadOnly = true;
                }
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
        /// Handles the CellClick event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.currentMobileHomeGridRowIndex = e.RowIndex;

                ////When permission does not exists this grid is not editable
                if (this.MobileHomeGridView.IsEditableGrid)
                {
                    if (e.RowIndex == 0)
                    {
                        this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                        this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                    }

                    bool hasValues = false;
                    if (e.RowIndex >= 1)
                    {
                        if ((string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeMake", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeYear", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeModel", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSize", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSerial", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeTaxCode", (e.RowIndex - 1)].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < MobileHomeGridView.RowCount)
                            {
                                for (int i = e.RowIndex; i < MobileHomeGridView.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[1].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[2].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[3].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[4].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[5].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                                    this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeMake", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeModel", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSize", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].Value.ToString().Trim())))
                                    {
                                        this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                                        this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                                        this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                                        this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                                        this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                                        this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                                this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                                this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                                this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                                this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                                this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                        }
                    }
                }
                else
                {
                    this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                    this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                    this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                    this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                    this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                    this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
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
        /// Handles the CellEndEdit event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int tempRollYear = 0;

                if (!this.avoidMobileHomeGridRowEnter)
                {
                    ////to set the default year when invalid date is given in the year column
                    if (e.ColumnIndex == 1)
                    {
                        if ((this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value.ToString().Trim())))
                        {
                            int.TryParse(this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value.ToString().Trim(), out tempRollYear);

                            if ((tempRollYear <= 0) || (tempRollYear < 1900) || (tempRollYear > 2079))
                            {
                                if (this.currentMobileHomeRollYear > 0)
                                {
                                    this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value = this.currentMobileHomeRollYear;
                                }
                                else
                                {
                                    this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value = DBNull.Value;
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

        /// <summary>
        /// Handles the RowEnter event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.currentMobileHomeGridRowIndex = e.RowIndex;

            try
            {
                if (!this.avoidMobileHomeGridRowEnter)
                {
                    ////When permission does not exists this grid is not editable
                    if (this.MobileHomeGridView.IsEditableGrid)
                    {
                        bool hasValues = false;
                        if (e.RowIndex >= 1)
                        {
                            if ((string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeMake", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeYear", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeModel", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSize", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSerial", (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeTaxCode", (e.RowIndex - 1)].Value.ToString().Trim())))
                            {
                                if (e.RowIndex + 1 < MobileHomeGridView.RowCount)
                                {
                                    for (int i = e.RowIndex; i < MobileHomeGridView.RowCount; i++)
                                    {
                                        if (!string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[1].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[2].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[3].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[4].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[i].Cells[5].Value.ToString().Trim()))
                                        {
                                            hasValues = true;
                                            break;
                                        }
                                    }

                                    if (hasValues)
                                    {
                                        this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                                        this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                                    }
                                    else
                                    {
                                        if ((string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeMake", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeYear", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeModel", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSize", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].Value.ToString().Trim())))
                                        {
                                            this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                                            this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                                            this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                                            this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                                            this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                                            this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
                                        }
                                        else
                                        {
                                            this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                                            this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                                        }
                                    }
                                }
                                else
                                {
                                    this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                                    this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                                    this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                                    this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                                    this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                                    this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
                                }
                            }
                            else
                            {
                                this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                                this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                            }
                        }

                        if (e.RowIndex == 0)
                        {
                            this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = false;
                            this.MobileHomeGridView.Rows[e.RowIndex].Selected = false;
                        }
                    }
                    else
                    {
                        this.MobileHomeGridView["MobileHomeMake", e.RowIndex].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeYear", e.RowIndex].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeModel", e.RowIndex].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeSize", e.RowIndex].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeSerial", e.RowIndex].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeTaxCode", e.RowIndex].ReadOnly = true;
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
        /// Handles the EditingControlShowing event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
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
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.MobileHomeGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
            try
            {
                ////to enable the save cancel button in form master
                this.ToEnableEditButtonInMasterForm();

                ////to add the empty row when last row is edited
                if (((this.MobileHomeGridView.CurrentCell.RowIndex + 1) == this.MobileHomeGridView.Rows.Count) && (this.MobileHomeGridView.CurrentCell.ColumnIndex >= 0))
                {
                    this.exciseTaxAffidavitDataSet.MobileHome.Rows.InsertAt(this.exciseTaxAffidavitDataSet.MobileHome.NewMobileHomeRow(), this.MobileHomeGridView.Rows.Count);
                    this.MoblieGridViewVerticalScroll.Visible = false;
                }
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
        /// Handles the KeyDown event of the MobileHomeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MobileHomeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46)
                {
                    DataRow[] tempRowCollection;
                    F15010ExciseAffidavitData.MobileHomeDataTable tempbileHomeDataTable = new F15010ExciseAffidavitData.MobileHomeDataTable();
                    this.exciseTaxAffidavitDataSet.MobileHome.AcceptChanges();

                    if (!string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[0].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[1].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[2].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[3].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[4].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.MobileHomeGridView.Rows[this.currentMobileHomeGridRowIndex].Cells[5].Value.ToString().Trim()))
                    {
                        this.avoidMobileHomeGridRowEnter = true;
                        this.exciseTaxAffidavitDataSet.MobileHome.Rows.RemoveAt(this.currentMobileHomeGridRowIndex);
                        ////OR StatementID IS NOT NULL AND StatementID <> ''OR MobileHomeID IS NOT NULL AND MobileHomeID <> ''
                        tempRowCollection = this.exciseTaxAffidavitDataSet.MobileHome.Select("(Make IS NOT NULL AND Make <> '') OR (Year IS NOT NULL AND Year <> '') OR (Model IS NOT NULL AND Model <> '') OR (Size IS NOT NULL AND Size <> '') OR (Serial IS NOT NULL AND Serial <> '') OR (TaxCode IS NOT NULL AND TaxCode <> '')", "MobileHomeID ASC");
                        DataRow tempRow;

                        foreach (DataRow dataRow in tempRowCollection)
                        {
                            tempRow = tempbileHomeDataTable.NewRow();
                            tempRow[0] = dataRow[0];
                            tempRow[1] = dataRow[1];
                            tempRow[2] = dataRow[2];
                            tempRow[3] = dataRow[3];
                            tempRow[4] = dataRow[4];
                            tempRow[5] = dataRow[5];
                            tempRow[6] = dataRow[6];
                            tempRow[7] = dataRow[7];
                            tempbileHomeDataTable.Rows.Add(tempRow);
                        }

                        this.exciseTaxAffidavitDataSet.MobileHome.AcceptChanges();
                        this.exciseTaxAffidavitDataSet.MobileHome.Clear();
                        this.exciseTaxAffidavitDataSet.MobileHome.Merge(tempbileHomeDataTable);
                        this.exciseTaxAffidavitDataSet.MobileHome.AcceptChanges();

                        this.avoidMobileHomeGridRowEnter = false;
                        this.PopulateMobileHomeGrid();
                        this.MobileHomeGridView["MobileHomeMake", 0].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeYear", 0].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeModel", 0].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeSize", 0].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeSerial", 0].ReadOnly = true;
                        this.MobileHomeGridView["MobileHomeTaxCode", 0].ReadOnly = true;

                        ////to enable the save cancel button in form master
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
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

        #endregion Moblie Home Events

        #endregion Coding for Mobile Home

        #region Common Methods

        /// <summary>
        /// Sets the focus to first editable controls.
        /// </summary>
        private void SetFocusFirstEditableControls()
        {
            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                if (TerraScanCommon.Administrator)
                {
                    this.ActiveControl = this.StatementNumberTextBox;
                    this.ActiveControl.Select();
                    this.ActiveControl.Focus();
                }
                else
                {
                    this.ActiveControl = this.GeneralHeaderPaymentDateTextBox;
                    this.ActiveControl.Select();
                    this.ActiveControl.Focus();
                }
            }
            else
            {
                this.GeneralHeaderPanel.Focus();
            }
        }

        /// <summary>
        /// To Load the entire Excise affDivt
        /// </summary>
        private void LoadEntireAffidavit()
        {
            ////set default back color
            this.SetCaluDueButtonsBGColor();
            this.affdvtButtonOperation = (int)ButtonOperation.Empty;
            this.SetAffDvtButton(ButtonOperation.Empty);
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.PanelOne.Focus();
            this.LoadAffDvtCombo();
            this.LoadExciseTaxAffidavit();
            //used for excise DOR Amend Button enable
            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                if ((this.AssessorStatusButton.Text == "Assessor - Approved") && (this.TreasurerStatusButton.Text == "Treasurer - Approved") && this.exciseTaxAffidavitDataSet.General.Rows[0]["ReceiptID"].ToString() != string.Empty
                  && this.exciseTaxAffidavitDataSet.General.Rows[0]["SubmittedBy"].ToString() != string.Empty)
                {
                    this.DORAmendButton.Enabled = true;
                    //if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[0][this.exciseTaxAffidavitDataSet.General.AmendByColumn].ToString()))
                    //{
                    this.isGeneralHead = true;
                    //}
                    //else
                    //{
                    //    this.isGeneralHead = false;
                    //}
                    this.LockControls(true);
                    this.Amend = false;
                }
                else
                {
                    this.DORAmendButton.Enabled = false;
                    this.isGeneralHead = false;
                }
                if ((this.exciseTaxAffidavitDataSet.General.Rows[0][this.exciseTaxAffidavitDataSet.General.AmendByColumn].ToString() == string.Empty) && (this.exciseTaxAffidavitDataSet.General.Rows[0]["AmendDate"].ToString() == string.Empty))
                {
                    this.Amend = false;
                }
                else
                {
                    this.Amend = true;
                }
            }

            ////when records are not in the afffidivt disable all controls
            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                this.LockControls(true);
                ////when the rec are greather than zero the check for submitted by is exsits are not
                this.ToDisableExciseAffiDvit();

                if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[0]["IsEditableDate"].ToString()) && Convert.ToInt32(this.exciseTaxAffidavitDataSet.General.Rows[0]["IsEditableDate"].ToString()) > 0)
                {
                    this.PaymentDatePanel.Enabled = true;
                    this.FromDatePanel.Enabled = true;
                }
                else
                {
                    this.PaymentDatePanel.Enabled = false;
                    this.FromDatePanel.Enabled = false;
                }
            }
            else
            {
                this.LockControls(false);
                this.TreasurerStatusButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.ReceiptFormButton.Enabled = false;
            }
            //this.DORAmendButton.Enabled = false;

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
            this.formLoaded = true;

            ////if (this.receiptNo != 0)
            ////{
            ////    this.FromDatePanel.Enabled = false;
            ////    this.PaymentDatePanel.Enabled = false;
            ////    this.GeneralDistrictLinkLablePanel.Enabled = false;
            ////    this.AffDvtDatePanle.Enabled = false;

            ////}
            ////else
            ////{
            ////    this.FromDatePanel.Enabled = true;
            ////    this.PaymentDatePanel.Enabled = true;
            ////    this.GeneralDistrictLinkLablePanel.Enabled = true;
            ////    this.AffDvtDatePanle.Enabled = true;
            ////}


        }

        /// <summary>
        /// To Enable and Disable Treasurer and Assessor status button.
        /// </summary>
        private void ForTreasurerAndAssessorStatusButton()
        {
            if (!this.submittedBy && this.slicePermissionField.editPermission && this.formMasterPermissionEdit && this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                this.TreasurerStatusButton.Enabled = true;
                this.AssessorStatusButton.Enabled = true;
            }
            else
            {
                this.TreasurerStatusButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;

            }
        }

        /// <summary>
        /// To disable the excise affidivt form when Submitted Date and Submitted By are null
        /// </summary>
        private void ToDisableExciseAffiDvit()
        {
            ////used for lock control based on after Dor Amend Button mode.
            ////if (!this.submittedDateExists && this.submittedBy > 0)
            if (!this.submittedBy)
            {
                this.LockControls(true);

                ////check whether the receiptIDExist exists are not 
                if (this.receiptIDExist)
                {
                    this.ToDisableCalAmountDuePartOnReceiptID(false);
                }
                else
                {
                    this.ToDisableCalAmountDuePartOnReceiptID(true);
                }
            }
            else
            {
                this.LockControls(false);
            }
        }

        /// <summary>
        /// to disable the controls when the receipt id exist
        /// </summary>
        /// <param name="tooEnable">boolean value</param>
        private void ToDisableCalAmountDuePartOnReceiptID(bool tooEnable)
        {
            if (TerraScanCommon.Administrator && !this.submittedBy)
            {
                ////when user id is admin and is not submitted then the following controls are editable
                this.PaymentDatePanel.Enabled = true;
                this.FromDatePanel.Enabled = true;

                // Code commented for CO #7208
                //this.GeneralDistrictLinkLablePanel.Enabled = true;

                this.GeneralTaxCodePanel.Enabled = true;
                this.AffDvtDatePanle.Enabled = true;
            }
            else
            {
                this.PaymentDatePanel.Enabled = tooEnable;
                this.FromDatePanel.Enabled = tooEnable;

                // Code commented for CO #7208
                // this.GeneralDistrictLinkLablePanel.Enabled = tooEnable;

                this.GeneralTaxCodePanel.Enabled = tooEnable;
                this.AffDvtDatePanle.Enabled = tooEnable;
            }

            ////when user is admin StatementNumberTextBox is editable
            if (this.affdvtButtonOperation != 1 && TerraScanCommon.Administrator && this.PermissionFiled.editPermission)
            {
                ////StatementNumberTextBox Text box is editable
                this.StatementNumberTextBox.LockKeyPress = false;
            }
            else
            {
                ////On New Opertion the StatementNumberTextBox Textbox is not editable                
                this.StatementNumberTextBox.LockKeyPress = true;
            }

            this.PanelFive.Enabled = tooEnable;
        }

        /// <summary>
        /// To Set Current Form Height
        /// </summary>
        private void ToSetCurrentFormHeight()
        {
            this.Height = this.formHeight;
            this.MainPanel.Height = this.mainPanelHeight;
            this.MainPanel.AutoScroll = false;
        }

        /// <summary>
        /// To delete Excise Affdvt
        /// </summary>
        /// <returns>the boolean value</returns>
        private bool DeleteExciseAffdvt()
        {
            if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
            {
                this.Cursor = Cursors.WaitCursor;
                if (!this.receiptIDExist)
                {
                    this.form15010Control.WorkItem.F15010_DeleteAffidavitDetails(int.Parse(this.StatementIDTextBox.Text.Trim()), TerraScanCommon.UserId);
                    ////to Clear the all the controls afther Delete
                    this.ClearAffidavitPartControls();
                    this.SetGeneralHeaderFieldNewMode();
                    this.ClearAmountDueControls();
                    this.ClearSupplimentControl();

                    return true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Affidavit can not be deleted after Receipt creation."), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                this.Cursor = Cursors.Default;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// For New ExciseAffDVT.
        /// </summary>        
        private void NewExciseAffDvt()
        {
            this.ClearAffDvtControls();
            this.Cursor = Cursors.WaitCursor;
            int statusId = 0;
            //// int the Generla header to default values
            this.SetGeneralHeaderFieldNewMode();
            ////this.ReceiptFormButton.Enabled = false;
            //// Assing to new mode
            this.affdvtButtonOperation = (int)ButtonOperation.New;

            DataRow[] invalidRow = this.exciseIndividualtype.ExciseDeedType.Select(this.exciseIndividualtype.ExciseDeedType.DeedTypeIDColumn + " = " + int.MaxValue.ToString());
            if (invalidRow != null && invalidRow.Length > 0)
            {
                this.exciseIndividualtype.ExciseDeedType.Rows.Remove(invalidRow[0]);
            }

            ////This flag is set for the parcel and parties grid part on load mode 
            this.partiesHeaderkeyPressed = false;
            this.parcelHeaderKeyPressed = false;
            this.parcelButtonOperation = 0;
            this.partyHeaderButtonOperation = 0;

            this.currentAffidavitStatementId = 0;
            this.receiptIDExist = false;

            //// Set AffDvt Button
            this.SetAffDvtButton(ButtonOperation.New);

            ////to get the roll year
            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
            ////this.getRollYearConfigurationValue.Merge(this.form15010Control.WorkItem.GetConfigDetails("TR_RollYear"));
            //Changes in the County Configuration.
            this.getRollYearConfigurationValue = this.form15010Control.WorkItem.GetConfigDetails("TR_ExciseRollYear");
            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
            {
                this.rollYear = int.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
            }

            //// Set All The Parcel and 
            ////this.SetAffiDvtButtonsOperation();

            //// Set Parties Header
            this.ClearPartiesHeader();

            //// Set Parcel Header
            this.ClearParcelHeader();

            this.ClearAmountDueControls();
            this.ClearSupplimentControl();
            this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Clear();
            this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Clear();

            this.ParcelHeaderDataGridView.DataSource = this.exciseTaxAffidavitDataSet.ParcelHeader.Copy().DefaultView;
            this.PartiesDataGridView.DataSource = this.exciseTaxAffidavitDataSet.PartiesHeader.Copy().DefaultView;

            this.PartiesHeaderVscrollBar.Enabled = false;
            this.PartiesHeaderVscrollBar.Visible = true;
            this.PartiesHeaderVscrollBar.BringToFront();

            this.ParcelVScrolBar.Enabled = false;
            this.ParcelVScrolBar.Visible = true;
            this.ParcelVScrolBar.BringToFront();

            ////to clear the Mobile home grid
            this.ClearMoblieHomeGrid();
            this.SetAffDvtControls(true);
            this.CalcuDueCommandButton.Enabled = true;

            /////when modified unsavedStatus and unsavedDesc is used to save status and desc in the db
            this.unsavedAssessorStatus = statusId.ToString();
            this.unsavedTreasurerStatus = statusId.ToString();
            this.unsavedTreasurerDesc = SharedFunctions.GetResourceString("Unverified");
            this.unsavedAssessorDesc = SharedFunctions.GetResourceString("Unverified");

            ////Added Byy Malliga on 10/2/2007
            if (this.unsavedTreasurerDesc == "Approved")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedTreasurerDesc == "Rejected")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedTreasurerDesc == "Unverified")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }

            if (this.unsavedAssessorDesc == "Approved")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedAssessorDesc == "Rejected")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedAssessorDesc == "Unverified")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }

            this.VoidButton.Tag = 0;
            this.SetVoidButtonBackColor();

            this.treasurerUserName = TerraScanCommon.UserName;
            this.treasurerUserId = TerraScanCommon.UserId;
            ////this.treasurerUpdatedTime = DateTime.Now.ToLocalTime().ToString();
            this.assessorUserName = TerraScanCommon.UserName;
            this.assessorUserId = TerraScanCommon.UserId;
            ////this.assessorUpdatedTime = DateTime.Now.ToLocalTime().ToString();

            this.treasurerStatus = statusId.ToString();
            this.assessorStatus = statusId.ToString();
            ////this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);

            this.SetAffiDvtButtonsOperation();

            this.DisablePartiesHeaderPanels(false);
            this.DisableParcelHeaderPanels(false);

            ////changing cursor type
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// To Disable the parcel header panels.
        /// </summary>
        /// <param name="controlDisable">BOOLEAN VALUE</param>
        private void DisableParcelHeaderPanels(bool controlDisable)
        {
            this.ParcelNumberPanel.Enabled = controlDisable;
            this.PersonlaPropertyPanel.Enabled = controlDisable;
            this.AssessedValuePanel.Enabled = controlDisable;
            this.LegalPanel.Enabled = controlDisable;
        }

        /// <summary>
        /// To Disable the parties header panels
        /// </summary>
        /// <param name="show">BOOLEAN VALUE</param>
        private void DisablePartiesHeaderPanels(bool show)
        {
            this.PartiesHeaderNamePanle.Enabled = show;
            this.ParitesPhoneNumberPanel.Enabled = show;
            this.PartiesTypePanel.Enabled = show;
            this.PartiesOwnerPanel.Enabled = show;
            this.PartiesAddress1Panel.Enabled = show;
            this.PartiesAddress2Panel.Enabled = show;
            this.PartiesCityPanel.Enabled = show;
            this.PartiesStatePanel.Enabled = show;
            this.PartiesZipCodePanel.Enabled = show;
            this.PartiesCountryPanel.Enabled = show;
        }

        /// <summary>
        /// Disables the aff DVT controls.
        /// </summary>
        /// <param name="enableStatus">if set to <c>true</c> [enable status].</param>
        private void SetAffDvtControls(bool enableStatus)
        {
            #region calDue

            if (this.receiptIDExist)
            {
                this.CalcuDueCommandButton.Enabled = false;
            }
            else
            {
                this.CalcuDueCommandButton.Enabled = enableStatus;
            }

            #endregion

            #region   Treasurer / Asssessor

            if (this.affdvtButtonOperation == (int)ButtonOperation.New)
            {
                this.TreasurerStatusButton.Enabled = false;
                this.TreasurerStatusButton.Text = "Treasurer - ";
                //// this.ExciseRatesButton.Enabled = false;
                ////this.ViewAfdvtButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.AssessorStatusButton.Text = "Assessor - ";
            }
            else if (this.PermissionFiled.editPermission || this.PermissionFiled.newPermission)
            {
                ////// Because Treasure / Assessor Should Be Enable Always              
                if (this.PermissionFiled.editPermission)
                {
                    this.TreasurerStatusButton.Enabled = true;
                    this.AssessorStatusButton.Enabled = true;
                }
                else
                {
                    this.AssessorStatusButton.Enabled = false;
                    this.TreasurerStatusButton.Enabled = false;
                }
            }
            else if (!this.PermissionFiled.editPermission)
            {
                this.TreasurerStatusButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                //// this.additionalOperationSmartPart.Enabled = false;
            }

            #endregion
        }

        /// <summary>
        /// Saves the aff DVT.
        /// </summary>
        private void SaveAffDvt()
        {
            ////if (this.CheckForMandatoryField())
            ////{
            DataSet tempDataSet = new DataSet("Root");
            DataTable general = new DataTable();
            DataTable amountDue = new DataTable();
            DataTable affDVt = new DataTable();
            DataTable suppliment = new DataTable();
            DataTable mobilHome = new DataTable();
            tempDataSet.Tables.Add(general);
            tempDataSet.Tables.Add(amountDue);
            tempDataSet.Tables.Add(affDVt);
            tempDataSet.Tables.Add(suppliment);
            tempDataSet.Tables.Add(mobilHome);

            if (this.affdvtButtonOperation == (int)ButtonOperation.New)
            {
                this.Cursor = Cursors.WaitCursor;
                this.SaveAffiDvtDetails();
                int affDvtstId = 0;

                string artieslAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.PartiesHeader);
                string parcelAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.ParcelHeader);
                string newMobileHomeValues = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.MobileHome);
                tempDataSet.Tables[0].Merge(this.exciseTaxAffidavitDataSet.General);
                tempDataSet.Tables[1].Merge(this.exciseTaxAffidavitDataSet.AmountDue);
                tempDataSet.Tables[2].Merge(this.exciseTaxAffidavitDataSet.Affidavit);
                tempDataSet.Tables[3].Merge(this.exciseTaxAffidavitDataSet.Suppliment);

                try
                {
                    affDvtstId = this.form15010Control.WorkItem.F15010_SaveAffiDavitDetails(affDvtstId, artieslAddress, parcelAddress, tempDataSet.GetXml(), newMobileHomeValues, TerraScanCommon.UserId);
                }
                catch (Exception ex)
                {
                }

                // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                TerraScanCommon.InterestDate = this.GeneralHeaderFormDateTextBox.DateTextBoxValue; //DateTime.Parse(this.InterestDateTextBox.Text.Trim());
                TerraScanCommon.ReceiptDate = this.GeneralHeaderPaymentDateTextBox.DateTextBoxValue; //DateTime.Parse(this.RecieptDateTextBox.Text.Trim());

                ////to load the affidivt form afther new affdivit is saved
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = affDvtstId;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));

                this.Cursor = Cursors.Default;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                this.exciseTaxAffidavitDataSet.General.DefaultView.Sort = "StatementID";
                int updateRowId = this.exciseTaxAffidavitDataSet.General.DefaultView.Find(this.StatementIDTextBox.Text.Trim());
                this.UpdateAffvt(updateRowId);
                string updatePartieslAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.PartiesHeader);
                string updateParcelAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.ParcelHeader);
                ////to accept the change made the mobile home Datagrid
                this.exciseTaxAffidavitDataSet.MobileHome.AcceptChanges();
                string updateMobileHome = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.MobileHome);
                tempDataSet.Tables[0].Merge(this.exciseTaxAffidavitDataSet.General);
                tempDataSet.Tables[1].Merge(this.exciseTaxAffidavitDataSet.AmountDue);
                tempDataSet.Tables[2].Merge(this.exciseTaxAffidavitDataSet.Affidavit);
                tempDataSet.Tables[3].Merge(this.exciseTaxAffidavitDataSet.Suppliment);
                int updatestId = Int32.Parse(this.StatementIDTextBox.Text.Trim());
                updatestId = this.form15010Control.WorkItem.F15010_SaveAffiDavitDetails(updatestId, updatePartieslAddress, updateParcelAddress, tempDataSet.GetXml(), updateMobileHome, TerraScanCommon.UserId);
                this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                ////this.LoadExciseTaxAffidavit();
                ////this.SetAmountDueSaveStatus();                        
                this.parcelHeaderKeyPressed = false;
                this.partiesHeaderkeyPressed = false;

                ////if (updatestId.Equals(0))
                ////{
                ////    this.PaymentDatePanel.Enabled = false;
                ////    this.FromDatePanel.Enabled = false;
                ////}
                ////else
                ////{
                ////    this.PaymentDatePanel.Enabled = false;
                ////    this.FromDatePanel.Enabled = false;
                ////}

                this.affdvtRemove = false;
                ////this.ExciseAffidavitAuditLink.Enabled = true;                        
                this.Cursor = Cursors.Default;
                this.SetAffDvtButton(ButtonOperation.Empty);

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the affi DVT button. 
        /// it will set all parcel and parties header button
        /// </summary>
        private void SetAffiDvtButtonsOperation()
        {
            this.SetParcelGridButtons(ButtonOperation.New);
            this.SetPartiesGridButtons(ButtonOperation.New);
        }

        /// <summary>
        /// Checks for mandatory field.
        /// </summary>
        /// <returns> True If All Field are Filled else False</returns>
        private bool CheckForMandatoryField()
        {
            bool validMandatoryField = false;
            bool validSalesPrice = false;

            ////form date(interest date) is passed instead of payment date(receipt date)
            ////if (!string.IsNullOrEmpty(this.GeneralDistrictLinkLabel.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralHeaderPaymentDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && (Decimal.Parse(this.TaxableSaleTextBox.Text.Trim()) >= 0) && this.exciseRateId != "0")
            if (!string.IsNullOrEmpty(this.GeneralDistrictLinkLabel.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralHeaderFormDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && (Decimal.Parse(this.TaxableSaleTextBox.Text.Trim()) >= 0) && this.exciseRateId != "0")
            {
                if (this.selectedTaxCode == 0)
                {
                    if (Decimal.Parse(this.CalcDueSellingPriceTextBox.Text.Trim()) > 0 && Decimal.Parse(this.TaxableSaleTextBox.Text.Trim()) >= 0)
                    {
                        validMandatoryField = true;
                        validSalesPrice = true;
                    }
                    else
                    {
                        this.CalcDueSellingPricePanel.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                        this.CalcDueSellingPriceTextBox.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                        validMandatoryField = false;
                        validSalesPrice = false;
                    }
                }
                else
                {
                    validMandatoryField = true;
                    validSalesPrice = true;
                }

                string useCodeValue = this.AffdvtUseCodeTextBox.Text;

                string[] validateUseCode = useCodeValue.Split('-');
                if (validSalesPrice)
                {
                    validMandatoryField = true;
                }
                else
                {
                    validMandatoryField = false;
                }

                ////Based on the length Empty space are applied
                if (validateUseCode[2].Length == 1)
                {
                    this.affDvitUseCode = validateUseCode[0] + "-" + validateUseCode[1] + "-" + validateUseCode[2] + " ";
                }
                else if (validateUseCode[2].Length == 0)
                {
                    this.affDvitUseCode = validateUseCode[0] + "-" + validateUseCode[1] + "-" + " " + " ";
                }
                else if (validateUseCode[2].Length == 2)
                {
                    this.affDvitUseCode = validateUseCode[0] + "-" + validateUseCode[1] + "-" + validateUseCode[2];
                }

                ////To Replace the Empty space with * symbol
                this.useCode = this.affDvitUseCode.Replace(" ", "*");
            }
            else
            {
                if (this.selectedTaxCode == 0)
                {
                    if (Decimal.Parse(this.CalcDueSellingPriceTextBox.Text.Trim()) > 0) ////&& Decimal.Parse(this.TaxableSaleTextBox.Text.Trim()) >= 0)
                    {
                        this.CalcDueSellingPricePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.CalcDueSellingPriceTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        validSalesPrice = true;
                    }
                    else
                    {
                        this.CalcDueSellingPricePanel.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                        this.CalcDueSellingPriceTextBox.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
                        validSalesPrice = false;
                    }
                }

                validMandatoryField = false;
            }

            return validMandatoryField;
        }

        /// <summary>
        /// Saves the affi DVT details.
        /// </summary>
        private void SaveAffiDvtDetails()
        {
            ///// Create New Record For GeneralHeader
            this.exciseTaxAffidavitDataSet.General.Rows.Clear();
            this.generalHeaderRow = this.exciseTaxAffidavitDataSet.General.NewRow();
            this.generalHeaderRow["StatementID"] = 0;
            this.generalHeaderRow["PaymentDate"] = this.GeneralHeaderPaymentDateTextBox.Text.Trim();
            this.generalHeaderRow["FormDate"] = this.GeneralHeaderFormDateTextBox.Text.Trim();
            this.generalHeaderRow["District"] = this.GeneralDistrictLinkLabel.Text.Trim();
            if (string.Compare(this.GeneralSubmittedDateTextBox.Text.Trim(), "N/A") == 0)
            {
                this.generalHeaderRow["SubmittedDate"] = DBNull.Value;
            }
            else
            {
                this.generalHeaderRow["SubmittedDate"] = this.GeneralSubmittedDateTextBox.Text.Trim();
            }

            //if (String.Equals(this.GeneralFromWebTextBox.Text.Trim().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("NOValue")))
            //{
            //    this.generalHeaderRow["PreDatesStmt"] = 1;
            //}
            //else
            //{
            //    this.generalHeaderRow["PreDatesStmt"] = 0;
            //}

            //if (String.Compare(this.GeneralFromWebTextBox.Text.Trim().ToUpperInvariant(), SharedFunctions.GetResourceString("NOValue")) == 0)
            //{
            //    this.generalHeaderRow["PreDatesStmt"] = 1;
            //}
            //else
            //{
            //    this.generalHeaderRow["PreDatesStmt"] = 0;
            //}

            if (String.Equals(this.GeneralMobileHomeComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.generalHeaderRow["IsMobileHome"] = 1;
            }
            else
            {
                this.generalHeaderRow["IsMobileHome"] = 0;
            }

            if (String.Equals(this.GeneralTaxCodeConboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("EXEMPTValue")))
            {
                this.generalHeaderRow["IsExempt"] = 1;
                this.taxCode = "Exempt";
            }
            else
            {
                this.generalHeaderRow["IsExempt"] = 0;
                this.taxCode = SharedFunctions.GetResourceString("TAXABLEValue");
            }

            this.generalHeaderRow["ReceiptNumber"] = this.GeneralReceiptNoLinkLabel.Text.Trim();
            this.generalHeaderRow["ExciseRateID"] = this.exciseRateId;
            this.generalHeaderRow["DORNote"] = this.GeneralNoteTextBox.Text.Trim();

            //this.generalHeaderRow["DocumentType"] = this.AffDvtDocumentTypeTextBox.Text.Trim();

            if (string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
            {
                this.generalHeaderRow["DocumentDate"] = DBNull.Value;
            }
            else
            {
                this.generalHeaderRow["DocumentDate"] = this.AffDvtDocDateTextBox.Text.Trim();
            }

            ////user id is passed to the database
            this.generalHeaderRow["UserID"] = TerraScanCommon.UserId;

            // CO
            if (this.DeedTypeComboBox.SelectedIndex > 0)
            {
                this.generalHeaderRow["DocumentType"] = this.DeedTypeComboBox.Text;
            }
            else
            {
                this.generalHeaderRow["DocumentType"] = string.Empty;
            }

            if (this.SourceComboBox.SelectedIndex >= 0)
            {
                this.generalHeaderRow["SourceID"] = (int)this.SourceComboBox.SelectedValue;
                this.generalHeaderRow["Source"] = this.SourceComboBox.Text;
            }
            else
            {
                this.generalHeaderRow["SourceID"] = DBNull.Value;
                this.generalHeaderRow["Source"] = string.Empty;
            }

            try
            {
                this.generalHeaderRow["IsVoid"] = byte.Parse(this.VoidButton.Tag.ToString());
            }
            catch (Exception ex)
            {
            }
            this.exciseTaxAffidavitDataSet.General.Rows.Add(this.generalHeaderRow);
            this.exciseTaxAffidavitDataSet.General.AcceptChanges();

            //// Create New Record From Calc Due
            DataRow calcDueHeaderRow;
            this.exciseTaxAffidavitDataSet.AmountDue.Rows.Clear();
            calcDueHeaderRow = this.exciseTaxAffidavitDataSet.AmountDue.NewRow();

            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestLocalColumn.ColumnName] = this.CalcDueDelinqIntLocalTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestStateColumn.ColumnName] = this.CalcDueDelinqIntStateTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.DelinquentPenaltyColumn.ColumnName] = this.CalcDueDelinqPenaltyTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxLocalColumn.ColumnName] = this.CalcDueExcisTaxLocaltextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxStateColumn.ColumnName] = this.CalcDueExciseTaxTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.FeesColumn.ColumnName] = this.CalcDueFeesTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.GrossSalePriceColumn.ColumnName] = this.CalcDueSellingPriceTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.PersonalPropAmtColumn.ColumnName] = this.CalcDuePerPropertTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.RealPropExemptAmtColumn.ColumnName] = this.CalDueRealPropTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.SubTotalColumn.ColumnName] = this.CalcDueSubTotalTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn.ColumnName] = this.TaxableSaleTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TechnologyFeeColumn.ColumnName] = this.CalcDueTechFeeTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TotalAmountDueColumn.ColumnName] = this.CalcDueTtlAmountTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TransactionFeeColumn.ColumnName] = this.CalcDueTransFeeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows.Add(calcDueHeaderRow);
            this.exciseTaxAffidavitDataSet.AmountDue.AcceptChanges();

            //// Row Affdivat

            DataRow affdvtRow;
            this.exciseTaxAffidavitDataSet.Affidavit.Rows.Clear();

            affdvtRow = this.exciseTaxAffidavitDataSet.Affidavit.NewRow();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn.ColumnName] = this.AffDvtExemptionCodeTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn.ColumnName] = this.AffDvtExemptionDescrTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn.ColumnName] = this.AffdvtExemptRegNumberTextBox.Text.Trim();

            if (String.Equals(this.AffDvtContinuanceComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffdvtForestCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffDvtHistoryComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidvtOpenSpaceComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 0;
            }

            if (String.Equals(this.PartialSaleCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 0;
            }

            if (String.Equals(this.SegregatedComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 0;
            }

            if (String.Equals(this.LocationofSaleComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 0;
            }

            if (string.IsNullOrEmpty(this.GeneralLocationCodeTextBox.Text.Trim()))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn.ColumnName] = DBNull.Value;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn.ColumnName] = this.GeneralLocationCodeTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.GerneralTotalDebitTextBox.Text.Trim().Replace("$", "").Trim()))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.TotalDebtColumn.ColumnName] = this.GerneralTotalDebitTextBox.Text.Trim().Replace("$", "").Trim();
            }

            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn.ColumnName] = this.NameOfLocationTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn.ColumnName] = this.AffDvtDescTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn.ColumnName] = this.StreetAddressTextBox.Text.Trim();
            ////affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.AffdvtUseCodeTextBox.Text.Trim();
            ////To assgin the empty space with * for Use code
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.useCode;

            this.exciseTaxAffidavitDataSet.Affidavit.Rows.Add(affdvtRow);
            this.exciseTaxAffidavitDataSet.AmountDue.AcceptChanges();

            //// Create New Record For Suppliemnt

            DataRow supplimentHeaderRow;

            this.exciseTaxAffidavitDataSet.Suppliment.Rows.Clear();
            supplimentHeaderRow = this.exciseTaxAffidavitDataSet.Suppliment.NewRow();

            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AgentNameColumn.ColumnName] = this.SuppliAgentNameTextBox.Text.Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.InstrumentTypeColumn.ColumnName] = this.SuppliInstTypeTextBox.Text.Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.InstrumentDateColumn.ColumnName] = this.SuppliInstDateTextBox.Text.Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.FirmnameColumn.ColumnName] = this.SuppliFirmNameTextBox.Text.Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.ReasonHeldColumn.ColumnName] = this.SuppliReasonHeldTextBox.Text.Trim();

            //// GiftConsideration_A1
            if (String.Equals(this.SuppliA1ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column.ColumnName] = 0;
            }

            //// TotalDebt_A1 
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A1Column.ColumnName] = this.SuppliA1TtlDbtTextBox.Text.Replace("$", "").Trim();

            //// GranteePaysGrantor_A1
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A1Column.ColumnName] = this.SuppliA1GrntPaysGranTextBox.Text.Replace("$", "").Trim();

            /////GiftConsideration_A2
            if (String.Equals(this.SuppliA2ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column.ColumnName] = 0;
            }

            ////TotalDebt_A2
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A2Column.ColumnName] = this.SuppliA2TtlDbtTextBox.Text.Replace("$", "").Trim();

            //// GranteePaysGrantor_A2
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A2Column.ColumnName] = this.SuppliA2GrantPaysGranTextBox.Text.Replace("$", "").Trim();

            //// DebtRate_A2
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.DebtRate_A2Column.ColumnName] = this.SuppliA1DbtRateTextBox.Text.Replace("%", "").Trim();

            ////GiftNoConsideration_B1
            if (String.Equals(this.SuppliB1ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 0;
            }

            ////GiftNoConsideration_B2
            if (String.Equals(this.SuppliB2ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column.ColumnName] = 0;
            }

            /////TotalDebt_B2
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B2Column.ColumnName] = this.SuppliB1TtlDbtTextBox.Text.Replace("$", "").Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B3Column.ColumnName] = this.SuppliB2TtlDbtTextBox.Text.Replace("$", "").Trim();
            ////GiftNoConsideration_B3

            if (String.Equals(this.SuppliB3Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B3Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B3Column.ColumnName] = 0;
            }

            //// TotalDebt_B3
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B3Column.ColumnName] = this.SuppliB2TtlDbtTextBox.Text.Replace("$", "").Trim();

            ////GiftNoConsideration_B4

            if (String.Equals(this.SuppliB4Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column.ColumnName] = 0;
            }

            ////IsRefinance
            //// supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = this.SuppliRefiCombo.SelectedValue;   

            if (String.Equals(this.SuppliRefiCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = 0;
            }

            ////GiftedEquity
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn.ColumnName] = this.SuppliGiftedEquityTextBox.Text.Trim();

            /////Grantorӳ Signature (Name)
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GrantorsSignatureColumn.ColumnName] = this.SuppliGranSignTextBox.Text.Trim();

            ////Granteeӳ Signature
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteesSignatureColumn.ColumnName] = this.SuppliGranteSignTextBox.Text.Trim();

            ////Facilitator Name
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.FacilitatorNameColumn.ColumnName] = this.SuppliFNameTextBox.Text.Trim();
            ////Grantee Name
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn.ColumnName] = this.SuppliGNameTextBox.Text.Trim();

            /////when modified unsavedStatus and unsavedDesc is used to save status and desc in the db
            this.treasurerDesc = this.unsavedTreasurerDesc;
            this.treasurerStatus = this.unsavedTreasurerStatus;
            this.assessorDesc = this.unsavedAssessorDesc;
            this.assessorStatus = this.unsavedAssessorStatus;

            ////vijaytodo
            ////this.treasurerUserName = TerraScanCommon.UserName;
            ////this.treasurerUserId = TerraScanCommon.UserId;
            ////this.treasurerUpdatedTime = DateTime.Now.ToLocalTime().ToString();
            ////this.assessorUserName = TerraScanCommon.UserName;
            ////this.assessorUserId = TerraScanCommon.UserId;
            ////this.assessorUpdatedTime = DateTime.Now.ToLocalTime().ToString();

            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedByColumn.ColumnName] = this.assessorUserId;
            ////supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedDateColumn.ColumnName] = this.assessorUpdatedTime;
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedByColumn.ColumnName] = this.treasurerUserId;
            ////supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedDateColumn.ColumnName] = this.treasurerUpdatedTime;

            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn.ColumnName] = this.treasurerDesc;
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasurerStatusIDColumn.ColumnName] = int.Parse(this.treasurerStatus);
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn.ColumnName] = this.assessorDesc;
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessorStatusIDColumn.ColumnName] = int.Parse(this.assessorStatus);
            this.exciseTaxAffidavitDataSet.Suppliment.Rows.Add(supplimentHeaderRow);
            this.exciseTaxAffidavitDataSet.Suppliment.AcceptChanges();

            ////Accept changes for Moblie Home Part
            this.exciseTaxAffidavitDataSet.MobileHome.AcceptChanges();
        }

        /// <summary>
        /// Updates the affvt.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateAffvt(int updateRowId)
        {
            this.UpdateGeneralHeader(updateRowId);
            this.UpdateAmountDueHeader(updateRowId);
            this.UpdateAffdvtHeader(updateRowId);
            this.UpdateSupplimentHeader(updateRowId);
        }

        /// <summary>
        /// Updates the suppliment header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateSupplimentHeader(int updateRowId)
        {
            //// Create New Record For Suppliemnt
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AgentNameColumn.ColumnName] = this.SuppliAgentNameTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentTypeColumn.ColumnName] = this.SuppliInstTypeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentDateColumn.ColumnName] = this.SuppliInstDateTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.FirmnameColumn.ColumnName] = this.SuppliFirmNameTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.ReasonHeldColumn.ColumnName] = this.SuppliReasonHeldTextBox.Text.Trim();
            //// GiftConsideration_A1

            if (String.Equals(this.SuppliA1ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column.ColumnName] = 0;
            }

            //// TotalDebt_A1 
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A1Column.ColumnName] = this.SuppliA1TtlDbtTextBox.Text.Replace("$", "").Trim();

            //// GranteePaysGrantor_A1
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A1Column.ColumnName] = this.SuppliA1GrntPaysGranTextBox.Text.Replace("$", "").Trim();

            //// GiftConsideration_A2

            if (String.Equals(this.SuppliA2ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column.ColumnName] = 0;
            }
            ////TotalDebt_A2
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A2Column.ColumnName] = this.SuppliA2TtlDbtTextBox.Text.Replace("$", "").Trim();

            //// GranteePaysGrantor_A2
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A2Column.ColumnName] = this.SuppliA2GrantPaysGranTextBox.Text.Replace("$", "").Trim();

            //// DebtRate_A2
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.DebtRate_A2Column.ColumnName] = this.SuppliA1DbtRateTextBox.Text.Replace("%", "").Trim();

            ////GiftNoConsideration_B1
            if (String.Equals(this.SuppliB1ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 0;
            }

            ////GiftNoConsideration_B2
            if (String.Equals(this.SuppliB2ComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column.ColumnName] = 0;
            }

            /////TotalDebt_B2
            //// supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B2Column.ColumnName] = this.SuppliB2TtlDbtTextBox.Text.Trim();

            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B2Column.ColumnName] = this.SuppliB2TtlDbtTextBox.Text.Replace("$", "").Trim();
            ////GiftNoConsideration_B3

            if (String.Equals(this.SuppliB3Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B3Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B3Column.ColumnName] = 0;
            }

            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B2Column.ColumnName] = this.SuppliB1TtlDbtTextBox.Text.Replace("$", "").Trim();
            //// TotalDebt_B3
            //// supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B3Column.ColumnName]  = this.SuppliB2TtlDbtTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_B3Column.ColumnName] = this.SuppliB2TtlDbtTextBox.Text.Replace("$", "").Trim();
            ////GiftNoConsideration_B4
            //// supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column.ColumnName] = this.SuppliB4Combo.SelectedValue;   

            if (String.Equals(this.SuppliB4Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B4Column.ColumnName] = 0;
            }

            //// IsRefinance
            //// supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = this.SuppliRefiCombo.SelectedValue;   
            if (String.Equals(this.SuppliRefiCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.IsRefinanceColumn.ColumnName] = 0;
            }

            ////GiftedEquity
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftedEquityColumn.ColumnName] = this.SuppliGiftedEquityTextBox.Text.Replace("$", "").Trim();

            /////Grantorӳ Signature (Name)
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GrantorsSignatureColumn.ColumnName] = this.SuppliGranSignTextBox.Text.Trim();

            ////Granteeӳ Signature
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteesSignatureColumn.ColumnName] = this.SuppliGranteSignTextBox.Text.Trim();

            ////Facilitator Name
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.FacilitatorNameColumn.ColumnName] = this.SuppliFNameTextBox.Text.Trim();

            ////Grantee Name
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn.ColumnName] = this.SuppliGNameTextBox.Text.Trim();

            /////when modified unsavedStatus and unsavedDesc is used to save status and desc in the db
            this.treasurerDesc = this.unsavedTreasurerDesc;
            this.treasurerStatus = this.unsavedTreasurerStatus;
            this.assessorDesc = this.unsavedAssessorDesc;
            this.assessorStatus = this.unsavedAssessorStatus;

            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedByColumn.ColumnName] = this.treasurerUserId;

            ////treasurer status not modifed sent the db value             
            if (!this.treasurerStatusModified)
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedDateColumn.ColumnName] = this.treasurerUpdatedTime;
            }
            else
            {
                ////if treasurer status is modified then sent null value to the db
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasStatusModifiedDateColumn.ColumnName] = string.Empty;
            }

            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedByColumn.ColumnName] = this.assessorUserId;

            ////assessor status not modifed sent the db value           
            if (!this.assessorStatusModified)
            {
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedDateColumn.ColumnName] = this.assessorUpdatedTime;
            }
            else
            {
                ////if assessor status is modified then sent null value to the db
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessStatusModifiedDateColumn.ColumnName] = string.Empty;
            }

            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn.ColumnName] = this.treasurerDesc;
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerStatusIDColumn.ColumnName] = int.Parse(this.treasurerStatus);
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn.ColumnName] = this.assessorDesc;
            this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorStatusIDColumn.ColumnName] = int.Parse(this.assessorStatus);
            this.exciseTaxAffidavitDataSet.Suppliment.AcceptChanges();

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Updates the affdvt header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateAffdvtHeader(int updateRowId)
        {
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn.ColumnName] = this.AffDvtExemptionCodeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn.ColumnName] = this.AffDvtExemptionDescrTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn.ColumnName] = this.AffdvtExemptRegNumberTextBox.Text.Trim();

            if (String.Equals(this.AffDvtContinuanceComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffdvtForestCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffDvtHistoryComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidvtOpenSpaceComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 0;
            }

            if (String.Equals(this.PartialSaleCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 0;
            }

            if (String.Equals(this.SegregatedComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 0;
            }

            if (String.Equals(this.LocationofSaleComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), "COUNTY"))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 0;
            }

            this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn.AllowDBNull = true;
            if (string.IsNullOrEmpty(this.GeneralLocationCodeTextBox.Text.Trim()))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn.ColumnName] = DBNull.Value;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationCodeColumn.ColumnName] = this.GeneralLocationCodeTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.GerneralTotalDebitTextBox.Text.Replace("$", "").Trim()))
            {
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.TotalDebtColumn.ColumnName] = this.GerneralTotalDebitTextBox.Text.Replace("$", "").Trim();
            }

            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn.ColumnName] = this.NameOfLocationTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn.ColumnName] = this.AffDvtDescTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn.ColumnName] = this.StreetAddressTextBox.Text.Trim();
            ////this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.AffdvtUseCodeTextBox.Text.Trim();
            ////To assgin the empty space with * for Use code
            this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.useCode;
            this.exciseTaxAffidavitDataSet.Affidavit.AcceptChanges();
        }

        /// <summary>
        /// Updates the amount due header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateAmountDueHeader(int updateRowId)
        {
            //// Create New Record From Calc Due
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.GrossSalePriceColumn.ColumnName] = this.CalcDueSellingPriceTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.PersonalPropAmtColumn.ColumnName] = this.CalcDuePerPropertTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.RealPropExemptAmtColumn.ColumnName] = this.CalDueRealPropTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn.ColumnName] = this.TaxableSaleTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxStateColumn.ColumnName] = this.CalcDueExciseTaxTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.ExciseTaxLocalColumn.ColumnName] = this.CalcDueExcisTaxLocaltextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestLocalColumn.ColumnName] = this.CalcDueDelinqIntLocalTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentInterestStateColumn.ColumnName] = this.CalcDueDelinqIntStateTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.DelinquentPenaltyColumn.ColumnName] = this.CalcDueDelinqPenaltyTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.TechnologyFeeColumn.ColumnName] = this.CalcDueTechFeeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.TransactionFeeColumn.ColumnName] = this.CalcDueTransFeeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.SubTotalColumn.ColumnName] = this.CalcDueSubTotalTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.FeesColumn.ColumnName] = this.CalcDueFeesTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.TotalAmountDueColumn.ColumnName] = this.CalcDueTtlAmountTextBox.Text.Trim();

            this.exciseTaxAffidavitDataSet.AmountDue.AcceptChanges();
        }

        /// <summary>
        /// Updates the general header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateGeneralHeader(int updateRowId)
        {
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.StatementIDColumn.ColumnName] = this.StatementIDTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.StatementNumberColumn.ColumnName] = this.StatementNumberTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.General.PaymentDateColumn.ReadOnly = false;
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PaymentDateColumn.ColumnName] = this.GeneralHeaderPaymentDateTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.FormDateColumn.ColumnName] = this.GeneralHeaderFormDateTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DistrictColumn.ColumnName] = this.GeneralDistrictLinkLabel.Text.Trim();
            if (string.Compare(this.GeneralSubmittedDateTextBox.Text.Trim(), "N/A") == 0)
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn.ColumnName] = DBNull.Value;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn.ColumnName] = this.GeneralSubmittedDateTextBox.Text.Trim();
            }

            //if (String.Compare(this.GeneralFromWebTextBox.Text.Trim().ToUpperInvariant(), SharedFunctions.GetResourceString("YESValue")) == 0)
            //{
            //    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn.ColumnName] = 1;
            //}
            //else
            //{
            //    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn.ColumnName] = 0;
            //}

            if (String.Equals(this.GeneralMobileHomeComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn.ColumnName] = 1;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn.ColumnName] = 0;
            }

            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.ReceiptNumberColumn.ColumnName] = this.GeneralReceiptNoLinkLabel.Text.Trim();
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.ExciseRateIDColumn.ColumnName] = this.exciseRateId;
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DORNoteColumn.ColumnName] = this.GeneralNoteTextBox.Text.Trim();

            if (String.Equals(this.GeneralTaxCodeConboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("EXEMPTValue")))
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsExemptColumn.ColumnName] = 1;
                this.taxCode = "1";
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsExemptColumn.ColumnName] = 0;
                this.taxCode = "0";
            }

            //this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn.ColumnName] = this.AffDvtDocumentTypeTextBox.Text.Trim();

            //// AffDVt
            this.exciseTaxAffidavitDataSet.General.DocumentDateColumn.ReadOnly = false;

            if (string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DocumentDateColumn.ColumnName] = DBNull.Value;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DocumentDateColumn.ColumnName] = this.AffDvtDocDateTextBox.Text.Trim();
            }

            ////to added the User id to teh database
            this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.UserIDColumn.ColumnName] = TerraScanCommon.UserId;

            if (this.DeedTypeComboBox.SelectedIndex > 0)
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn.ColumnName] = this.DeedTypeComboBox.Text.Trim();
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DocumentTypeColumn.ColumnName] = string.Empty;
            }

            if (this.SourceComboBox.SelectedIndex >= 0)
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SourceIDColumn.ColumnName] = (int)this.SourceComboBox.SelectedValue;
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SourceColumn.ColumnName] = this.SourceComboBox.Text;
            }
            else
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SourceIDColumn.ColumnName] = DBNull.Value;
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SourceColumn.ColumnName] = string.Empty;
            }

            try
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsVoidColumn.ColumnName] = byte.Parse(this.VoidButton.Tag.ToString());
            }
            catch (Exception ex)
            {
            }

            this.exciseTaxAffidavitDataSet.General.AcceptChanges();
        }

        /// <summary>
        /// Loads the excise tax affidavit.
        /// </summary>
        private void LoadExciseTaxAffidavit()
        {
            this.CustomiseParcelDataGridView();
            this.CustimizeMobileHomeGrid();
            ////this.SetAttachmentCommentsCount();            

            this.Cursor = Cursors.WaitCursor;
            this.exciseTaxAffidavitDataSet.General.Clear();
            this.exciseTaxAffidavitDataSet.PartiesHeader.Clear();
            this.exciseTaxAffidavitDataSet.ParcelHeader.Clear();
            this.exciseTaxAffidavitDataSet.AmountDue.Clear();
            this.exciseTaxAffidavitDataSet.Affidavit.Clear();
            this.exciseTaxAffidavitDataSet.Suppliment.Clear();
            this.exciseTaxAffidavitDataSet.MobileHome.Clear();

            this.exciseTaxAffidavitDataSet = this.form15010Control.WorkItem.F15010_LoadExciseTaxAffidavit(this.currentAffidavitStatementId);

            this.exciseIndividualtype = this.form15010Control.WorkItem.F15010_GetExciseIndividualType();

            if (this.exciseTaxAffidavitDataSet.Suppliment.Rows.Count < 0)
            {
                this.TreasurerStatusButton.Text = "Treasurer - " + this.exciseTaxAffidavitDataSet.Suppliment.Rows[0][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn];
                this.AssessorStatusButton.Text = "Assessor - " + this.exciseTaxAffidavitDataSet.Suppliment.Rows[0][this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn];
            }

            //Modifed to implement #21450 CO by purushotham
            if (this.exciseIndividualtype.ConfiguredRollYear.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.exciseIndividualtype.ConfiguredRollYear.Rows[0][0].ToString().Trim()))
                {
                    int.TryParse(this.exciseIndividualtype.ConfiguredRollYear.Rows[0][0].ToString().Trim(), out this.affidavitYear);
                }
            }
            this.Cursor = Cursors.Default;

            ////to enable the ReceiptFormButton
            InitComboBoxValues(this.PersonlaPropertyComboBox);
            this.ReceiptFormButton.Enabled = true;
            this.TreasurerStatusButton.Enabled = true;
            this.AssessorStatusButton.Enabled = true;
            this.GetExciseAffidavitDetails();
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

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "Text Changed Events In Text Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                ////this.SetAutoCompleteForExemptionCode();
                bool changeflag = false;
                if (this.exemptionValidateonoff && this.AffDvtExemptionCodeTextBox.Text != "")
                {
                    this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseWAC();

                    if (this.districtSelectionDataSet.ListExciseWAC.Rows.Count > 0)
                    {
                        for (int i = 0; i <= this.districtSelectionDataSet.ListExciseWAC.Rows.Count - 1; i++)
                        {
                            if (this.AffDvtExemptionCodeTextBox.Text == this.districtSelectionDataSet.ListExciseWAC.Rows[i][this.districtSelectionDataSet.ListExciseWAC.WACColumn.ColumnName].ToString())
                            {
                                changeflag = true;
                            }
                        }
                    }

                    ////TerraScanTextBox sourceTextBox = this.AffDvtExemptionCodeTextBox as TerraScanTextBox;
                    ////ChangeExemptionBackGround(sourceTextBox);
                    if (changeflag)
                    {
                        AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                        ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(238, 210, 211);
                        ExcemptionCodePanel.BackColor = Color.FromArgb(238, 210, 211);
                    }
                }
                else
                {
                    AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
                ////to disable the ReceiptFormButton when form is in edit mode
                this.ReceiptFormButton.Enabled = false;
            }
        }

        //// This Should Be moved to TerraScan.Common

        /// <summary>
        /// Sets the data grid coulmn.
        /// </summary>
        /// <param name="sourceDataGrid">The source data grid.</param>
        /// <param name="sourceRowId">The source row id.</param>
        /// <param name="sourceColumnId">The source column id.</param>
        private void SetDataGridCoulmn(TerraScan.UI.Controls.TerraScanDataGridView sourceDataGrid, int sourceRowId, int sourceColumnId)
        {
            if (sourceDataGrid.Rows.Count > 0 && sourceRowId >= 0 && sourceColumnId >= 0)
            {
                sourceDataGrid.Rows[Convert.ToInt32(sourceRowId)].Selected = false;
                sourceDataGrid.CurrentCell = sourceDataGrid[sourceColumnId, Convert.ToInt32(sourceRowId)];
                sourceDataGrid.CurrentCell.Selected = true;
            }
        }

        /// <summary>
        /// Gets the excise affidavit details.
        /// </summary>
        private void GetExciseAffidavitDetails()
        {
            #region General
            this.SetGeneralComboBox();

            if (this.receiptNo != 0)
            {
                this.FromDatePanel.Enabled = false;
                this.PaymentDatePanel.Enabled = false;
                // Code commented for CO #7208
                // this.GeneralDistrictLinkLablePanel.Enabled = false;
                this.AffDvtDatePanle.Enabled = false;
            }
            else
            {
                this.FromDatePanel.Enabled = true;
                this.PaymentDatePanel.Enabled = true;
                // Code commented for CO #7208
                // this.GeneralDistrictLinkLablePanel.Enabled = true;
                this.AffDvtDatePanle.Enabled = true;
            }

            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                this.FillGeneralHeaderText(0);
            }
            else
            {
                this.GeneralDistrictLinkLablePanel.Enabled = true;
                this.GeneralDistrictLinkLabel.Text = string.Empty;
                this.GeneralReceiptNoLinkLabel.Text = string.Empty;

                this.GeneralHeaderPaymentDateTextBox.Text = string.Empty;
                this.GeneralHeaderFormDateTextBox.Text = string.Empty;
                this.AffDvtDocDateTextBox.Text = string.Empty;
                this.FromDatePanel.Enabled = false;
                this.PaymentDatePanel.Enabled = false;
                this.AffDvtDatePanle.Enabled = false;
            }

            #endregion

            #region Parties
            //// Loads The PArties Grid
            this.SetPartiesGrid();
            ////to load combo box
            this.InitIndividualTypeComboBox();

            if (this.partiesRowCount > 0)
            {
                this.SetPartiesText(0);
                this.DisablePartiesHeaderPanels(true);
            }
            else
            {
                this.PartiesDataGridView.Rows[Convert.ToInt32(0)].Selected = false; ////vijay
                this.DisablePartiesHeaderPanels(false);
            }

            this.SetPartiesGridButtons(ButtonOperation.Empty);

            #endregion

            #region Parcel

            this.SetParcelGrid();
            ////to load PersonlaPropertyComboBox 
            if (this.parcelRecordCount > 0)
            {
                this.SetParcelHeaderTextBox(0);
                this.DisableParcelHeaderPanels(true);
            }
            else
            {
                this.ParcelHeaderDataGridView.Rows[Convert.ToInt32(0)].Selected = false; ////vijay   
                SetComboboxValue(this.PersonlaPropertyComboBox, this.ParcelHeaderDataGridView.Rows[Convert.ToInt32(0)].Cells["IsPersonalProp"].Value.ToString());
                this.ClearParcelHeader();
                this.DisableParcelHeaderPanels(false);
            }

            this.SetParcelGridButtons(ButtonOperation.Empty);

            #endregion Parcel

            #region AffDvt

            this.LoadAffDvtCombo();

            //// newToolStripMenuItem The AffDvtValue
            if (this.exciseTaxAffidavitDataSet.Affidavit.Rows.Count > 0)
            {
                this.LoadAffDvtValue(0);
            }

            #endregion

            #region CalcDue

            if (this.exciseTaxAffidavitDataSet.AmountDue.Rows.Count > 0)
            {
                this.SetCalcDueTextBox(0);
            }

            #endregion CalcDue

            #region Suppliment

            this.LoadSupplimentCombo();
            if (this.exciseTaxAffidavitDataSet.Suppliment.Rows.Count > 0)
            {
                this.SetSupplimentTextBox(0);
            }

            #endregion Suppliment

            #region Mobile Home

            this.LoadMobileHomePart();

            #endregion Mobile Home
        }

        /// <summary>
        /// To Disable All the Panels int the form
        /// </summary>
        /// <param name="lockControl">boolean Value</param>
        private void LockControls(bool lockControl)
        {
            ////to disable the genereal part controls
            this.DisableGeneralPartControl(lockControl);
            this.PartiesPartHeaderPanel.Enabled = lockControl;
            this.ParcelsPartHeaderPanel.Enabled = lockControl;
            this.PanelFour.Enabled = lockControl;
            this.PanelFive.Enabled = lockControl;
            this.PanelSix.Enabled = lockControl;
            this.PanelSeven.Enabled = lockControl;
        }

        /// <summary>
        /// To diaable the genereal part controls
        /// </summary>
        /// <param name="allowEdit">boolen value</param>
        private void DisableGeneralPartControl(bool allowEdit)
        {
            this.TreasurerStatusButton.Enabled = allowEdit;
            this.ReceiptFormButton.Enabled = allowEdit;
            this.AssessorStatusButton.Enabled = allowEdit;
            this.PanelStatmentID.Enabled = allowEdit;
            this.PanelStatementNumber.Enabled = allowEdit;
            this.PaymentDatePanel.Enabled = allowEdit;
            this.FromDatePanel.Enabled = allowEdit;
            this.GeneralDistrictLinkLablePanel.Enabled = allowEdit;
            this.GeneralSubmittedDatePanel.Enabled = allowEdit;
            this.CreatedBYPanel.Enabled = allowEdit;
            this.GeneralFromWebPanel.Enabled = allowEdit;
            this.GerneralTotalDebitPanel.Enabled = allowEdit;
            this.GeneralLocationCodePanel.Enabled = allowEdit;
            this.GeneralTaxCodePanel.Enabled = allowEdit;
            this.GeneralMobileHomePanel.Enabled = allowEdit;
            this.GeneralReceiptNoPanel.Enabled = allowEdit;
            this.GeneralDescriptionPanel.Enabled = allowEdit;
            this.AffDvtDocumentTypePanel.Enabled = allowEdit;
            this.AffDvtDatePanle.Enabled = allowEdit;

            //// Code added to fix #8048 issue - Button is in enable state when loading form without record
            if (!this.isGeneralHead)
            {
                this.GeneralHeaderPanel.Enabled = allowEdit;
            }
            else
            {
                this.GeneralHeaderPanel.Enabled = true;
                this.DORAmendButton.Enabled = true;
                this.VoidButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.TreasurerStatusButton.Enabled = false;
                this.ReceiptFormButton.Enabled = false;
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            #region GeneralPart

            ////this.StatementIDTextBox.Text = string.Empty;                
            this.GeneralHeaderPaymentDateTextBox.LockKeyPress = controlLook;
            this.GeneralPaymentDatePict.Enabled = !controlLook;
            this.GeneralHeaderFormDateTextBox.LockKeyPress = controlLook;
            this.GeneralFormDatePic.Enabled = !controlLook;
            this.GeneralDistrictLinkLabel.Enabled = !controlLook;
            this.GeneralDistrictPictureBox.Enabled = !controlLook;
            ////no need for this below code since it is always locked
            ////this.GeneralSubmittedDateTextBox.LockKeyPress = controlLook;
            ////this.GeneralFromWebTextBox.LockKeyPress = controlLook;

            this.GeneralReceiptNoLinkLabel.Enabled = !controlLook;
            this.GeneralReceiptNoTextBox.LockKeyPress = controlLook;
            this.GeneralNoteTextBox.LockKeyPress = controlLook;
            ////To empty the combo boxs            
            this.GeneralTaxCodeConboBox.Enabled = !controlLook;
            this.GeneralMobileHomeComboBox.Enabled = !controlLook;
            this.DeedTypeComboBox.Enabled = !controlLook;
            this.SourceComboBox.Enabled = !controlLook;
            //this.AffDvtDocumentTypeTextBox.LockKeyPress = controlLook;
            this.AffDvtDocDateTextBox.LockKeyPress = controlLook;

            #endregion GeneralPart

            #region Parties

            this.GeneralPartiesNamePictureParcel.Enabled = !controlLook;
            this.GeneralPartiesNameTextBox.LockKeyPress = controlLook;
            ////this.GeneralHeaderFormDateTextBox.LockKeyPress = controlLook;
            ////this.GeneralHeaderPaymentDateTextBox.LockKeyPress = controlLook;
            this.PartiesPhoneNoTextBox.LockKeyPress = controlLook;
            this.PartiesOwnerTextBox.LockKeyPress = controlLook;
            this.PartiesAddress1TextBox.LockKeyPress = controlLook;
            this.PartiesAddress2TextBox.LockKeyPress = controlLook;
            this.PartiesCityTextBox.LockKeyPress = controlLook;
            this.PartiesStateTextBox.LockKeyPress = controlLook;
            this.PartiesZipCodeTextBox.LockKeyPress = controlLook;
            this.PartiesCountryTextBox.LockKeyPress = controlLook;

            ////to empty the combo boxs
            this.PartiesTypeComboBox.Enabled = !controlLook;
            ////todo: to clear the parties grid view ////vijay

            #endregion Parties

            #region Parcels

            this.ParcelPictureBox.Enabled = !controlLook;
            this.ParcelNumberTextBox.LockKeyPress = controlLook;
            this.ParcelAssessedValueTextBox.LockKeyPress = controlLook;
            this.ParcelLegalTextBox.LockKeyPress = controlLook;
            this.PersonlaPropertyComboBox.Enabled = !controlLook;
            ////to empty the combo boxs
            ////todo: clear the Parcels grid ////vijay

            #endregion Parcels

            #region Affidavit

            this.StreetAddressTextBox.LockKeyPress = controlLook;
            ////this.NameOfLocationTextBox.LockKeyPress = controlLook;
            this.AffdvtUseCodeTextBox.Enabled = !controlLook;
            this.AffdvtExemptRegNumberTextBox.LockKeyPress = controlLook;
            this.AffDvtDescTextBox.LockKeyPress = controlLook;
            this.AffDvtExemptionCodeTextBox.LockKeyPress = controlLook;
            this.AffDvtExemptionDescrTextBox.LockKeyPress = controlLook;
            this.AffDvtDatePicture.Enabled = !controlLook;

            ////to empty the combo boxs            
            this.PartialSaleCombo.Enabled = !controlLook;
            this.SegregatedComboBox.Enabled = !controlLook;
            ////this.LocationofSaleComboBox.Enabled = !controlLook;
            this.AffdvtForestCombo.Enabled = !controlLook;
            this.AffidvtOpenSpaceComboBox.Enabled = !controlLook;
            this.AffDvtHistoryComboBox.Enabled = !controlLook;
            this.AffDvtContinuanceComboBox.Enabled = !controlLook;
            ////this.GeneralLocationCodeTextBox.LockKeyPress = controlLook;
            this.GerneralTotalDebitTextBox.LockKeyPress = controlLook;

            #endregion Affidavit

            #region Amount Due

            this.CalcDueSellingPriceTextBox.LockKeyPress = controlLook;
            this.CalcDuePerPropertTextBox.LockKeyPress = controlLook;
            this.CalDueRealPropTextBox.LockKeyPress = controlLook;
            ////no need for this below code since it is always locked
            ////this.TaxableSaleTextBox.LockKeyPress = controlLook;
            this.CalcDueExciseTaxTextBox.LockKeyPress = controlLook;
            this.CalcDueExcisTaxLocaltextBox.LockKeyPress = controlLook;
            this.CalcDueDelinqIntStateTextBox.LockKeyPress = controlLook;
            this.CalcDueDelinqIntLocalTextBox.LockKeyPress = controlLook;
            this.CalcDueDelinqPenaltyTextBox.LockKeyPress = controlLook;
            this.CalcDueTechFeeTextBox.LockKeyPress = controlLook;
            this.CalcDueTransFeeTextBox.LockKeyPress = controlLook;

            #endregion Amount Due

            #region Suppliement

            this.SuppliAgentNameTextBox.LockKeyPress = controlLook;
            this.SuppliInstTypeTextBox.LockKeyPress = controlLook;
            this.SuppliInsDatePict.Enabled = !controlLook;
            this.SuppliInstDateTextBox.LockKeyPress = controlLook;
            this.SuppliFirmNameTextBox.LockKeyPress = controlLook;
            this.SuppliReasonHeldTextBox.LockKeyPress = controlLook;

            this.SuppliA1ComboBox.Enabled = !controlLook;
            this.SuppliA1TtlDbtTextBox.LockKeyPress = controlLook;
            this.SuppliA1GrntPaysGranTextBox.LockKeyPress = controlLook;
            this.SuppliA2ComboBox.Enabled = !controlLook;
            this.SuppliA2TtlDbtTextBox.LockKeyPress = controlLook;
            this.SuppliA2GrantPaysGranTextBox.LockKeyPress = controlLook;
            this.SuppliA1DbtRateTextBox.LockKeyPress = controlLook;
            this.SuppliB1ComboBox.Enabled = !controlLook;
            this.SuppliB2ComboBox.Enabled = !controlLook;
            this.SuppliB1TtlDbtTextBox.LockKeyPress = controlLook;
            this.SuppliB3Combo.Enabled = !controlLook;
            this.SuppliB2TtlDbtTextBox.LockKeyPress = controlLook;
            this.SuppliB4Combo.Enabled = !controlLook;
            this.SuppliRefiCombo.Enabled = !controlLook;
            this.SuppliGiftedEquityTextBox.LockKeyPress = controlLook;
            this.SuppliGranSignTextBox.LockKeyPress = controlLook;
            this.SuppliGranteSignTextBox.LockKeyPress = controlLook;
            this.SuppliFNameTextBox.LockKeyPress = controlLook;
            this.SuppliGNameTextBox.LockKeyPress = controlLook;

            #endregion Suppliement

            #region Mobile Home

            ////When permission does not exists this grid is not editable
            this.MobileHomeGridView.IsEditableGrid = !controlLook;

            #endregion Mobile Home

            #region Common controls

            this.TreasurerStatusButton.Enabled = !controlLook;
            if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
            {
                //used for the Receipt button disbaled based on isgeneralHEad
                if (!this.isGeneralHead)
                {
                    this.ReceiptFormButton.Enabled = !controlLook;
                }
                else
                {
                    this.ReceiptFormButton.Enabled = false;
                }
            }
            else
            {
                this.ReceiptFormButton.Enabled = false;
            }

            this.AssessorStatusButton.Enabled = !controlLook;
            this.PartiesPartPemissionPanel.Enabled = !controlLook;
            this.ParcelsPartPermissionPanel.Enabled = !controlLook;
            this.CalcuDueCommandButton.Enabled = !controlLook;

            #endregion Common controls
        }

        /// <summary>
        /// Clears all fields.
        /// </summary>
        private void ClearAllFields()
        {
            #region GeneralPart

            this.ClearGeneralPartControls();

            #endregion GeneralPart

            #region Parties

            this.ClearPartiesPartControls();

            #endregion Parties

            #region Parcels

            this.ClearParcelsPartControls();

            #endregion Parcels

            #region Affidavit

            this.ClearAffidavitPartControls();

            #endregion Affidavit

            #region Amount Due

            this.ClearAmountDuePartConrols();

            #endregion Amount Due

            #region Suppliement

            this.ClearSupplimentControl();

            #endregion Suppliement
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.PartiesDataGridView.OriginalRowCount == 0)
            {
                this.PartiesDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.PartiesDataGridView.RemoveDefaultSelection = false;
            }

            if (this.ParcelHeaderDataGridView.OriginalRowCount == 0)
            {
                this.ParcelHeaderDataGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.ParcelHeaderDataGridView.RemoveDefaultSelection = false;
            }
        }

        #endregion Common Methods

        #region Common Events

        /// <summary>
        /// Excises the affidavit multi text box key press.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ExciseAffidavitMultiTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                switch (e.KeyChar)
                {
                    case (char)13:
                        {
                            e.Handled = true;
                            break;
                        }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceiptFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptFormButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////below value used for new and cancel operation fo the master form(In new operation keyid is set to zero, then during cancel operation(reload the entire form) this value is used 
                this.currentAffidavitStatementId = this.currentTempStatementKeyId;
                if (this.currentAffidavitStatementId > 0)
                {
                    ////this.receiptButtonClick = true;
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(11011);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.currentAffidavitStatementId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ExciseRatesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseRatesButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11013);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Resize event of the F15010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15010_Resize(object sender, EventArgs e)
        {
            try
            {
                this.ToSetCurrentFormHeight();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Load event of the F15010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F15010_Load(object sender, EventArgs e)
        {
            try
            {
                this.StatusLinkLabel.Visible = false;
                PartiesHeaderNamePanle.BackColor = Color.FromArgb(255, 255, 255);
                this.LoadEntireAffidavit();
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                this.ForTreasurerAndAssessorStatusButton();
                this.ToSetCurrentFormHeight();

                if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                {
                    this.ActiveControl = StatementNumberTextBox;
                    this.StatementNumberTextBox.Focus();
                }
                else
                {
                    this.GeneralHeaderPanel.Focus();
                }

                //// this.SetFocusFirstEditableControls();

                this.RemoveDefaultSelection();
                if (this.TreasurerStatusButton.Text == "Treasurer - Approved")
                {
                    this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                }
                else if (this.TreasurerStatusButton.Text == "Treasurer - Rejected")
                {
                    this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                }
                else if (this.TreasurerStatusButton.Text == "Treasurer - Unverified")
                {
                    this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                }
                ////Assessor Button Color Change
                if (this.AssessorStatusButton.Text == "Assessor - Approved")
                {
                    this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                }
                else if (this.AssessorStatusButton.Text == "Assessor - Rejected")
                {
                    this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
                }
                else if (this.AssessorStatusButton.Text == "Assessor - Unverified")
                {
                    this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
                }
                //this.DORAmendButton.Enabled = false;
                if ((this.AssessorStatusButton.Text == "Assessor - Approved") && (this.TreasurerStatusButton.Text == "Treasurer - Approved") && !string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[0]["ReceiptID"].ToString())
                     && !string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[0]["SubmittedBy"].ToString()))
                {
                    this.DORAmendButton.Enabled = true;
                    //if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[0]["AmendBy"].ToString()))
                    //{
                    this.isGeneralHead = true;
                    //}
                    //else
                    //{
                    //    this.isGeneralHead = false;
                    //}
                }
                else
                {
                    this.DORAmendButton.Enabled = false;
                    this.isGeneralHead = false;
                }


                ////tTs_cfg

                ////to get the ExemptionautocompleteConfiguration details
                this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
                this.getRollYearConfigurationValue = this.form15010Control.WorkItem.GetConfigDetails("TR_ExciseExemptionAutoCompleteOn");
                if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    this.exemptionautocompleteonoff = bool.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                }

                if (this.exemptionautocompleteonoff)
                {
                    this.SetAutoCompleteForExemptionCode();
                }
                else
                {
                    this.AffDvtExemptionCodeTextBox.AutoCompleteCustomSource = null;
                }

                ////to get the ExemptionValidityConfiguration details
                this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
                this.getRollYearConfigurationValue = this.form15010Control.WorkItem.GetConfigDetails("TR_ExciseExemptionValidityOn");
                if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    this.exemptionValidateonoff = bool.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                }

                if (this.exemptionValidateonoff && this.AffDvtExemptionCodeTextBox.Text != "")
                {
                    bool flag = false;
                    this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseWAC();

                    if (this.districtSelectionDataSet.ListExciseWAC.Rows.Count > 0)
                    {
                        for (int i = 0; i <= this.districtSelectionDataSet.ListExciseWAC.Rows.Count - 1; i++)
                        {
                            if (this.AffDvtExemptionCodeTextBox.Text == this.districtSelectionDataSet.ListExciseWAC.Rows[i][this.districtSelectionDataSet.ListExciseWAC.WACColumn.ColumnName].ToString())
                            {
                                flag = true;
                            }
                        }
                    }

                    if (!flag)
                    {
                        this.AffDvtExemptionCodeTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                        this.ExcemptionCodePanel.BackColor = Color.FromArgb(238, 210, 211);
                    }
                    else
                    {
                        this.AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                        this.ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                    }
                }
                else
                {
                    this.AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                    this.ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
                }

                ////Address Auto Complete Functionality
                if (this.StreetAddressTextBox.Text == "")
                {
                    this.address1dt = new DataTable();
                    if (this.address1dt.Columns.Count == 0)
                    {
                        this.address1dt.Columns.Add("Address1");
                    }

                    DataRow dr1;
                    dr1 = this.address1dt.NewRow();
                    dr1["Address1"] = this.PartiesAddress1TextBox.Text;
                    this.address1dt.Rows.Add(dr1);
                    this.address1 = TerraScanCommon.GetXmlString(this.address1dt);
                    this.SetAutoCompleteForAddress1();
                }

                if (this.StreetAddressTextBox.Text == "")
                {
                    this.address2dt = new DataTable();
                    if (this.address2dt.Columns.Count == 0)
                    {
                        this.address2dt.Columns.Add("Address2");
                    }

                    DataRow dr2;
                    dr2 = this.address2dt.NewRow();
                    dr2["Address2"] = this.PartiesAddress2TextBox.Text;
                    this.address2dt.Rows.Add(dr2);
                    this.address2 = TerraScanCommon.GetXmlString(this.address2dt);
                    this.SetAutoCompleteForAddress2();
                }

                if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                {
                    if (this.isDistrictEditable > 0)
                    {
                        this.GeneralDistrictLinkLablePanel.Enabled = false;
                    }
                    else
                    {
                        if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !this.submittedBy)
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = true;
                        }
                        else
                        {
                            this.GeneralDistrictLinkLablePanel.Enabled = false;
                        }
                    }
                }
                else
                {
                    this.GeneralDistrictLinkLablePanel.Enabled = false;
                }

                // Coding Added for the issue 3881
                this.SetFocusFirstEditableControls();
                // Ends here 3881
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// SetAffDvtButton
        /// </summary>
        /// <param name="buttonOperation">The button operation.</param>
        private void SetAffDvtButton(ButtonOperation buttonOperation)
        {
            switch (buttonOperation)
            {
                case ButtonOperation.HeaderPartUpdate:
                    {
                        this.AffdvtButtonOprHeaderPartUpdate();
                        break;
                    }

                case ButtonOperation.NoRecordFound:
                    {
                        this.AffDvtButtonOprNoRecordFound();
                        break;
                    }
            }
        }

        /// <summary>
        /// Affs the DVT button opr no record found.
        /// </summary>
        private void AffDvtButtonOprNoRecordFound()
        {
            this.AssessorStatusButton.Enabled = false;
        }

        /// <summary>
        /// Affdvts the button opr header part update.
        /// </summary>
        private void AffdvtButtonOprHeaderPartUpdate()
        {
            this.CalcuDueCommandButton.Enabled = true;
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (!this.CheckForMandatoryField())
            {
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing");
            }
            else
            {
                if (this.UpdateParites.Enabled || this.ParcelUpdateButton.Enabled)
                {
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("parties/parcelUpdateMode");
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Handles the MouseHover event of the TreasurerStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TreasurerStatusButton_MouseHover(object sender, EventArgs e)
        {
            if (this.unsavedTreasurerDesc == "Approved")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedTreasurerDesc == "Rejected")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedTreasurerDesc == "Unverified")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the AssessorStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessorStatusButton_MouseHover(object sender, EventArgs e)
        {
            if (this.unsavedAssessorDesc == "Approved")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedAssessorDesc == "Rejected")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedAssessorDesc == "Unverified")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the TreasurerStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TreasurerStatusButton_MouseLeave(object sender, EventArgs e)
        {
            if (this.unsavedTreasurerDesc == "Approved")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedTreasurerDesc == "Rejected")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedTreasurerDesc == "Unverified")
            {
                this.TreasurerStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the AssessorStatusButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssessorStatusButton_MouseLeave(object sender, EventArgs e)
        {
            if (this.unsavedAssessorDesc == "Approved")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            else if (this.unsavedAssessorDesc == "Rejected")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 0, 0);
            }
            else if (this.unsavedAssessorDesc == "Unverified")
            {
                this.AssessorStatusButton.BackColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }
        }

        /// <summary>
        /// Handles the Enter event of the PartiesPhoneNoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesPhoneNoTextBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.GeneralPartiesNameTextBox.Text))
            {
                this.CancelPartiesButton_Click(sender, e);
                this.ParitesPhoneNumberPanel.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Handles the Enter event of the PersonlaPropertyComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PersonlaPropertyComboBox_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ParcelNumberTextBox.Text))
            {
                this.ParcelCancelButton_Click(sender, e);
                this.PersonlaPropertyComboBox.BackColor = Color.White;
                this.PersonlaPropertyPanel.BackColor = Color.White;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the PartiesTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////if (this.PartiesTypeComboBox.SelectedIndex > 1)
            ////{
            ////    this.PartiesOwnerTextBox.Text = string.Empty;
            ////} 
            ////else
            ////{
            ////    this.PartiesOwnerTextBox.Text = "100";
            ////}
        }

        /// <summary>
        /// Sets the auto complete for exemption code.
        /// </summary>
        private void SetAutoCompleteForExemptionCode()
        {
            AutoCompleteStringCollection sourceExemptionCode = new AutoCompleteStringCollection();
            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseWAC();

            if (this.districtSelectionDataSet.ListExciseWAC.Rows.Count > 0)
            {
                this.AssignAutoCompletSource(this.districtSelectionDataSet.ListExciseWAC.Rows, this.districtSelectionDataSet.ListExciseWAC.WACColumn.ColumnName, this.AffDvtExemptionCodeTextBox, sourceExemptionCode);
            }
            else
            {
                this.AffDvtExemptionCodeTextBox.AutoCompleteCustomSource = null;
            }
        }

        /// <summary>
        /// Assigns the auto complet souce.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="textBox">The text box.</param>
        /// <param name="sourceCollection">The source collection.</param>
        private void AssignAutoCompletSource(DataRowCollection dataRow, string columnName, TerraScanTextBox textBox, AutoCompleteStringCollection sourceCollection)
        {
            for (int count = 0; count < dataRow.Count; count++)
            {
                sourceCollection.Add(dataRow[count][columnName].ToString());
            }

            textBox.AutoCompleteCustomSource = sourceCollection;
        }

        /// <summary>
        /// Sets the auto complete for address1.
        /// </summary>
        private void SetAutoCompleteForAddress1()
        {
            AutoCompleteStringCollection sourceAddress1 = new AutoCompleteStringCollection();
            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseIndividual(this.address1);

            if (this.districtSelectionDataSet.ListExciseIndividual.Rows.Count > 0)
            {
                this.AssignAutoCompletSource(this.districtSelectionDataSet.ListExciseIndividual.Rows, this.districtSelectionDataSet.ListExciseIndividual.Address1Column.ColumnName, this.PartiesAddress1TextBox, sourceAddress1);
            }
            else
            {
                this.PartiesAddress1TextBox.AutoCompleteCustomSource = null;
            }
        }

        /// <summary>
        /// Sets the auto complete for address2.
        /// </summary>
        private void SetAutoCompleteForAddress2()
        {
            AutoCompleteStringCollection sourceAddress2 = new AutoCompleteStringCollection();
            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseIndividual(this.address2);

            if (this.districtSelectionDataSet.ListExciseIndividual.Rows.Count > 0)
            {
                this.AssignAutoCompletSource(this.districtSelectionDataSet.ListExciseIndividual.Rows, this.districtSelectionDataSet.ListExciseIndividual.Address2Column.ColumnName, this.PartiesAddress2TextBox, sourceAddress2);
            }
            else
            {
                this.PartiesAddress2TextBox.AutoCompleteCustomSource = null;
            }
        }

        /// <summary>
        /// Handles the Leave event of the PartiesAddress1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesAddress1TextBox_Leave(object sender, EventArgs e)
        {
            if (this.StreetAddressTextBox.Text == "" && this.PartiesAddress1TextBox.Text != "")
            {
                this.address1dt = new DataTable();
                if (this.address1dt.Columns.Count == 0)
                {
                    this.address1dt.Columns.Add("Address1");
                }

                DataRow dr1;
                dr1 = this.address1dt.NewRow();
                dr1["Address1"] = this.PartiesAddress1TextBox.Text;
                this.address1dt.Rows.Add(dr1);
                this.address1 = TerraScanCommon.GetXmlString(this.address1dt);
                this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseIndividual(this.address1);
                if (this.districtSelectionDataSet.ListExciseIndividual.Rows.Count > 0)
                {
                    this.PartiesCityTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.CityColumn.ColumnName].ToString();
                    this.PartiesStateTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.StateColumn.ColumnName].ToString();
                    this.PartiesZipCodeTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.ZipColumn.ColumnName].ToString();
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the PartiesAddress2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesAddress2TextBox_Leave(object sender, EventArgs e)
        {
            if (this.StreetAddressTextBox.Text == "" && this.PartiesAddress2TextBox.Text != "")
            {
                this.address2dt = new DataTable();
                if (this.address2dt.Columns.Count == 0)
                {
                    this.address2dt.Columns.Add("Address2");
                }

                DataRow dr2;
                dr2 = this.address2dt.NewRow();
                dr2["Address2"] = this.PartiesAddress2TextBox.Text;
                this.address2dt.Rows.Add(dr2);
                this.address2 = TerraScanCommon.GetXmlString(this.address2dt);
                this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseIndividual(this.address2);
                if (this.districtSelectionDataSet.ListExciseIndividual.Rows.Count > 0)
                {
                    this.PartiesCityTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.CityColumn.ColumnName].ToString();
                    this.PartiesStateTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.StateColumn.ColumnName].ToString();
                    this.PartiesZipCodeTextBox.Text = this.districtSelectionDataSet.ListExciseIndividual.Rows[0][this.districtSelectionDataSet.ListExciseIndividual.ZipColumn.ColumnName].ToString();
                }
            }
        }

        /// <summary>
        /// Handles the Leave event of the AffDvtExemptionCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtExemptionCodeTextBox_Leave(object sender, EventArgs e)
        {
            if (this.exemptionValidateonoff && this.AffDvtExemptionCodeTextBox.Text != "")
            {
                TerraScanTextBox sourceTextBox = this.AffDvtExemptionCodeTextBox as TerraScanTextBox;
                this.ChangeExemptionBackGround(sourceTextBox);
            }
            else
            {
                this.AffDvtExemptionCodeTextBox.BackColor = Color.FromArgb(255, 255, 255);
                ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        /// <summary>
        /// Changes the exemption back ground.
        /// </summary>
        /// <param name="sourceTextBox">The source text box.</param>
        private void ChangeExemptionBackGround(TerraScanTextBox sourceTextBox)
        {
            bool leaveflag = false;
            this.districtSelectionDataSet = this.form15010Control.WorkItem.F15010_ListExciseWAC();

            if (this.districtSelectionDataSet.ListExciseWAC.Rows.Count > 0)
            {
                for (int i = 0; i <= this.districtSelectionDataSet.ListExciseWAC.Rows.Count - 1; i++)
                {
                    if (this.AffDvtExemptionCodeTextBox.Text == this.districtSelectionDataSet.ListExciseWAC.Rows[i][this.districtSelectionDataSet.ListExciseWAC.WACColumn.ColumnName].ToString())
                    {
                        leaveflag = true;
                    }
                }
            }

            if (leaveflag)
            {
                // sourceTextBox.Parent.BackColor = Color.FromArgb(255, 255, 255);
                sourceTextBox.BackColor = Color.FromArgb(255, 255, 255);
                ExcemptionCodePanel.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                sourceTextBox.ApplyFocusColor = false;
                //// sourceTextBox.Parent.BackColor = Color.FromArgb(238, 210, 211);
                sourceTextBox.BackColor = Color.FromArgb(238, 210, 211);
                ExcemptionCodePanel.BackColor = Color.FromArgb(238, 210, 211);
            }
        }

        /// <summary>
        /// Handles the Enter event of the AffDvtExemptionCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtExemptionCodeTextBox_Enter(object sender, EventArgs e)
        {
            ////this.AffDvtExemptionCodeTextBox.ApplyFocusColor = true;
            ////this.AffDvtExemptionCodeTextBox.SetFocusColor = Color.FromArgb(255,255,211); 
        }

        /// <summary>
        /// Handles the KeyPress event of the ReceiptDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Handles the KeyPress event of the InterestdateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void InterestdateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Handles the KeyPress event of the DeedTypedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DeedTypedateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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

        /// <summary>
        /// Handles the CloseUp event of the ReceiptDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.GeneralHeaderPaymentDateTextBox.Text = this.ReceiptDateTimePicker.Text;
                this.GeneralHeaderPaymentDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the InterestdateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InterestdateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.GeneralHeaderFormDateTextBox.Text = this.InterestdateTimePicker.Text;
                this.GeneralHeaderFormDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the DeedTypedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeedTypedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.AffDvtDocDateTextBox.Text = this.DeedTypedateTimePicker.Text;
                this.AffDvtDocDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the InstrumentDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void InstrumentDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.SuppliInstDateTextBox.Text = this.InstrumentDateTimePicker.Text;
                this.SuppliInstDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the InstrumentDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void InstrumentDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
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


        #endregion Common Events

        #region Coding Added for the issue 3881
        /// <summary>
        /// Handles the Leave event of the StatementNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatementNumberTextBox_Leave(object sender, EventArgs e)
        {
            ////if (this.TreasurerStatusButton.Text == "Treasurer - Approved")
            ////{
            ////    this.ActiveControl = this.AffDvtDocumentTypeTextBox;
            ////    this.ActiveControl.Select();
            ////    this.ActiveControl.Focus();
            ////}
            ////else
            ////{
            ////    this.ActiveControl = this.GeneralHeaderPaymentDateTextBox;
            ////    this.ActiveControl.Select();
            ////    this.ActiveControl.Focus();
            ////}
        }
        #endregion

        #region Void Button Event

        /// <summary>
        /// Handles the Click event of the VoidButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VoidButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.receiptId > 0 || this.receiptIDExist) && (int)this.VoidButton.Tag != 1)
                {
                    DialogResult isVoid = MessageBox.Show("The current Excise Affidavit has a Receipt associated with it, but voiding this Affidavit \r\nwill not affect this receipt directly. \r\nAre you sure you want to Void this Excise Affidavit?", "TerraScan ֠Void Excise Affidavit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (isVoid.Equals(DialogResult.Yes))
                    {
                        this.VoidButton.BackColor = Color.FromArgb(128, 0, 0);
                        this.VoidButton.Tag = 1;
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
                else if ((int)this.VoidButton.Tag != 1)
                {
                    this.VoidButton.BackColor = Color.FromArgb(128, 0, 0);
                    this.VoidButton.Tag = 1;
                    this.ToEnableEditButtonInMasterForm();
                }
                else if (((int)this.VoidButton.Tag).Equals(1))
                {
                    this.VoidButton.Tag = 0;
                    // Enable form master save, cancel button
                    this.ToEnableEditButtonInMasterForm();
                }

                // Chagne void button back color based on certain constrain
                this.SetVoidButtonBackColor();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseLeave event of the VoidButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VoidButton_MouseLeave(object sender, System.EventArgs e)
        {
            try
            {
                // Chagne void button back color based on certain constrain
                this.SetVoidButtonBackColor();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the VoidButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void VoidButton_MouseHover(object sender, System.EventArgs e)
        {
            try
            {
                // Chagne void button back color based on certain constrain
                this.SetVoidButtonBackColor();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the color of the void button back.
        /// </summary>
        private void SetVoidButtonBackColor()
        {
            if (this.VoidButton.Tag != null)
            {
                if ((int)this.VoidButton.Tag > 0)
                {
                    // If isVoid = 1
                    this.VoidButton.BackColor = Color.FromArgb(128, 0, 0);
                }
                else
                {
                    // If isVoid = 0
                    this.VoidButton.BackColor = Color.FromArgb(128, 128, 128);
                }
            }
        }

        #endregion Void Button Event

        #region Deed Type Combo Events

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DeedTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeedTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.DeedTypeComboBox.SelectedValue != null)
                    {
                        this.tempDeedTypeId = this.DeedTypeComboBox.SelectedValue.ToString();
                    }
                    else
                    {
                        this.tempDeedTypeId = string.Empty;
                    }

                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the DeedTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void DeedTypeComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if ((this.DeedTypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.DeedTypeComboBox.Text.Trim()))
                {
                    if (this.tempDeedTypeId != null && !this.tempDeedTypeId.Equals(this.DeedTypeComboBox.SelectedValue.ToString()))
                    {
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
                else
                {
                    this.DeedTypeComboBox.Text = string.Empty;
                    this.DeedTypeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DeedTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeedTypeComboBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.DeedTypeComboBox.SelectedValue != null && !string.IsNullOrEmpty(this.DeedTypeComboBox.Text.Trim()))
                    {
                        if (!this.tempDeedTypeId.Equals(this.DeedTypeComboBox.SelectedValue.ToString()))
                        {
                            this.ToEnableEditButtonInMasterForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Deed Type Combo Events

        #region Source Combo Events

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SourceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SourceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.SourceComboBox.SelectedValue != null)
                    {
                        this.tempSourceId = (int)this.SourceComboBox.SelectedValue;
                    }
                    else
                    {
                        this.tempSourceId = 0;
                    }

                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the SourceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SourceComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if ((this.SourceComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.SourceComboBox.Text.Trim()))
                {
                    if (!this.tempSourceId.Equals((int)this.SourceComboBox.SelectedValue))
                    {
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
                else
                {
                    this.SourceComboBox.Text = string.Empty;
                    this.SourceComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the SourceComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SourceComboBox_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if ((this.SourceComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.SourceComboBox.Text.Trim()))
                    {
                        if (!this.tempSourceId.Equals((int)this.SourceComboBox.SelectedValue))
                        {
                            this.ToEnableEditButtonInMasterForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Source Combo Events

        #region DOR Amend Button

        private void DORAmendButton_Click(object sender, EventArgs e)
        {

            try
            {
                this.Cursor = Cursors.WaitCursor;
                string asmxUrl = string.Empty;
                bool wrongReceiptNo = false;
                string errorStatus = string.Empty;
                string webMethod = string.Empty;
                string userName = string.Empty;
                string password = string.Empty;
                bool amendFlag = true;
                this.submitCount = 0;
                this.affidavitWorkQueueDataSet = this.form15010Control.WorkItem.GetWorkQueueSearchResult(null, null, null, null, null, null, this.StatementNumberTextBox.Text.Trim());
                this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Columns.Add(SharedFunctions.GetResourceString("ValidStatus"), System.Type.GetType("System.Boolean"));

                this.DeleteXmlFile();
                string processedData = string.Empty;
                string replyXmlValue = string.Empty;

                processedData = this.ProcessReetXml();

                ////StreamReader sr = new StreamReader(@"C:\Documents and Settings\karthikeyanv\Desktop\Sam.xml");
                ////processedData = sr.ReadToEnd();

                // To be removed
                /* DirectoryInfo dir = new DirectoryInfo("C:\\TempDOR");
                if (!dir.Exists)
                {
                    Directory.CreateDirectory("C:\\TempDOR");
                }
                string filepath = "C:\\TempDOR\\OneDOR" + DateTime.Now.Ticks + ".xml";
                StreamWriter sw1 = new StreamWriter(filepath);
                sw1.Write(processedData);
                sw1.Close(); */
                this.configurationDatatable = this.form15010Control.WorkItem.ListConfigurationDetail;
                if (this.configurationDatatable != null)
                {
                    if (this.configurationDatatable.Rows.Count > 0)
                    {
                        asmxUrl = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.WebServiceURLColumn.ColumnName].ToString().Trim();
                        webMethod = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.MethodNameColumn.ColumnName].ToString().Trim();
                        userName = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.UserNameColumn.ColumnName].ToString().Trim();
                        password = this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.PasswordColumn.ColumnName].ToString().Trim();
                        amendFlag = true;//Convert.ToBoolean(this.configurationDatatable.Rows[0][this.affidavitWorkQueueDataSet.ListConfigurationDetail.AmendColumn.ColumnName]);
                    }
                }

                this.submitCount = this.submitCount + 1;
                ////StreamReader s = new StreamReader(@"c:\reet.xml");
                ////processedData = s.ReadToEnd(); 
                replyXmlValue = this.ReetServiceCall(asmxUrl, webMethod, userName, password, processedData, amendFlag);
                DataSet replyDataSet = new DataSet();
                if (replyXmlValue != null)
                {
                    replyDataSet.ReadXml(SharedFunctions.XmlParser(replyXmlValue));
                    replyDataSet.Namespace = string.Empty;
                    replyXmlValue = replyDataSet.GetXml();

                    //// To be removed
                    /* string filepath11 = "C:\\TempDOR\\OneDORReply" + DateTime.Now.Ticks + ".xml";
                    StreamWriter sw11 = new StreamWriter(filepath11);
                    sw11.Write(replyXml);
                    sw11.Close();  */

                    if (!string.IsNullOrEmpty(replyXmlValue))
                    {
                        this.submitDataset = this.form15010Control.WorkItem.F1108_GetSubmitAffidavitReply(replyXmlValue, TerraScanCommon.UserId);

                        if (this.submitDataset.ErrorStatusDataTable.Rows.Count > 0)
                        {
                            errorStatus = this.submitDataset.ErrorStatusDataTable.Rows[0][this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName].ToString();
                        }

                        this.submitDataset.Tables.Remove(this.submitDataset.ErrorStatusDataTable.TableName);

                        ////if(this.submitDataset.Tables[this.submitDataset.ErrorStatusDataTable.TableName].[this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName]
                    }
                    else
                    {
                        return;
                    }

                    if (replyDataSet != null && replyDataSet.Tables["AffiDavit"].Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in replyDataSet.Tables["AffiDavit"].Rows)
                        {
                            if (String.Equals(dr1["RECEIPT_NUM"], "N/A"))
                            {
                                wrongReceiptNo = true;
                                break;
                            }
                            else
                            {
                                wrongReceiptNo = false;
                            }
                        }
                    }

                    //// IF ROW COUNT ZERO THEN THERE IS  NO ERROR
                    if (!string.IsNullOrEmpty(errorStatus))
                    {
                        if (errorStatus == "False")
                        {
                            if (this.submitDataset.AFFIDAVIT.Rows.Count == 0)
                            {
                                this.submitPass = true;
                                this.selectedCount = this.affidavitCount;
                                this.submitCount = 0;
                            }
                        }
                        else
                        {
                            if (this.submitDataset.AFFIDAVIT.Rows.Count > 0)
                            {
                                processedData = this.ProcessReetXml();

                                // To be removed
                                /* string filepath2 = "C:\\TempDOR\\TwoDOR" + DateTime.Now.Ticks + ".xml";
                                StreamWriter sw2 = new StreamWriter(filepath2);
                                sw2.Write(processedData);
                                sw2.Close(); */

                                this.submitCount = this.submitCount + 1;

                                replyXmlValue = this.ReetServiceCall(asmxUrl, webMethod, userName, password, processedData, true);

                                DataSet ds1 = new DataSet();
                                ds1.ReadXml(SharedFunctions.XmlParser(replyXmlValue));
                                ds1.Namespace = string.Empty;
                                replyXmlValue = ds1.GetXml();

                                // ds.ReadXml(;
                                // To be removed
                                /* string filepath12 = "C:\\TempDOR\\TwoDORReply" + DateTime.Now.Ticks + ".xml";
                                StreamWriter sw12 = new StreamWriter(filepath12);
                                sw12.Write(replyXml);
                                sw12.Close(); */

                                if (!string.IsNullOrEmpty(replyXmlValue))
                                {
                                    this.submitDataset = this.form15010Control.WorkItem.F1108_GetSubmitAffidavitReply(replyXmlValue, TerraScanCommon.UserId);

                                    if (this.submitDataset.ErrorStatusDataTable.Rows.Count > 0)
                                    {
                                        errorStatus = this.submitDataset.ErrorStatusDataTable.Rows[0][this.submitDataset.ErrorStatusDataTable.ErrorStatusColumn.ColumnName].ToString();
                                    }
                                }
                                else
                                {
                                    return;
                                }

                                this.submitPass = true;
                                this.submitCount = 0;
                            }
                            else
                            {
                                ////MessageBox.Show("The submission process failed.", "TerraScan - SubmitDOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////return;

                                if (MessageBox.Show(SharedFunctions.GetResourceString("FailSumbit") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("FailExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    Hashtable dorSumbitDetails = new Hashtable();
                                    dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                                    dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                                    TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                                }

                                return;
                            }
                        }
                    }

                    //// IF NO ERROR OCCURED IN ( II )  PROCESS THEN
                    if (wrongReceiptNo)
                    {
                        MessageBox.Show("Invalid ReceiptNo Return !!", "TerraScan T2 - SubmitDOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (errorStatus == "False" && this.submitDataset.AFFIDAVIT.Rows.Count == 0 && this.submitPass)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("SuccessfullSumbit") + this.selectedCount.ToString() + SharedFunctions.GetResourceString("TotalDOR") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SuccessfullExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Hashtable dorSumbitDetails = new Hashtable();
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                            TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                        }
                        //this.LockControls(false);
                        bool isEnabled = false;
                        this.LockControls(isEnabled);
                        //   this.LoadAfiidavitListGrid();
                    }
                    else
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("FailSumbit") + " " + this.affidavitCount.ToString() + SharedFunctions.GetResourceString("ViewDORSumbit"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("FailExcise")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Hashtable dorSumbitDetails = new Hashtable();
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("UserID"), TerraScanCommon.UserId);
                            dorSumbitDetails.Add(SharedFunctions.GetResourceString("Update"), DateTime.Now);
                            TerraScanCommon.ShowReport(110815, TerraScan.Common.Reports.Report.ReportType.Preview, dorSumbitDetails);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Reets the service call.
        /// </summary>
        /// <param name="webServiceAsmxUrl">The web service asmx URL.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="amend">if set to <c>true</c> [amend].</param>
        /// <returns>Returns reply Xml</returns>
        private string ReetServiceCall(string webServiceAsmxUrl, string methodName, string userName, string password, string reetXml, bool amend)
        {
            try
            {
                string replyXml = string.Empty;
                replyXml = this.ReetService(webServiceAsmxUrl, methodName, userName, password, reetXml, amend);


                if (!string.IsNullOrEmpty(replyXml))
                {
                    int keyvalue = this.form15010Control.WorkItem.F1108_SaveReplyReetXml(reetXml, replyXml, TerraScanCommon.UserId);
                    return replyXml;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ReplyXml"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (UriFormatException)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrlFormat"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            catch (WebException webexp)
            {
                if (webexp.Message.Contains("404"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Contains("403"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Equals(SharedFunctions.GetResourceString("TimeOut")))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("TimeOut"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (webexp.Message.Contains("500"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("InvalidUrl"), SharedFunctions.GetResourceString("SubmitDOR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return null;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return null;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                return null;
            }
        }


        /// <summary>
        /// Removes the URL extension.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>return Url</returns>
        private string RemoveUrlExtension(string url)
        {
            string tempUrl = string.Empty;
            int length = 0;
            length = url.LastIndexOf(".");
            tempUrl = url.Substring(0, length);
            return tempUrl;
        }

        /// <summary>
        /// Reets the service.
        /// </summary>
        /// <param name="webServiceAsmxUrl">The web service asmx URL.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="amend">if set to <c>true</c> [amend].</param>
        /// <returns>Returns replyXml</returns>
        private string ReetService(string webServiceAsmxUrl, string methodName, string userName, string password, string reetXml, bool amend)
        {
            try
            {
                string serviceName = string.Empty;
                object submitValue = null;
                string clientUrl = this.RemoveUrlExtension(webServiceAsmxUrl);

                WebClient client = new WebClient();

                // -Connect To the web service
                //// System.IO.Stream stream = client.OpenRead(webServiceAsmxUrl + "?wsdl");
                Stream stream = client.OpenRead(clientUrl + ".asmx?wsdl");

                // --Now read the WSDL file describing a // service.
                ServiceDescription description = ServiceDescription.Read(stream);
                serviceName = description.Services[0].Name;

                // --Initialize a service description importer.
                ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
                importer.ProtocolName = "Soap12"; // Use SOAP 1.2.
                ////importer.AddServiceDescription(description, webServiceAsmxUrl, wsdlUrl);
                importer.AddServiceDescription(description, null, null);

                // --Generate a proxy client. 
                importer.Style = ServiceDescriptionImportStyle.Client;

                // --Generate properties to represent pri // mitive values.
                importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

                // --Initialize a Code-DOM tree into which we will import the service.
                CodeNamespace codeNameSpace = new CodeNamespace();
                CodeCompileUnit compileUnit = new CodeCompileUnit();
                compileUnit.Namespaces.Add(codeNameSpace);

                // --Import the service into the Code-DOM tree. 

                // --This creates proxy code that uses the service.
                ServiceDescriptionImportWarnings warning = importer.Import(codeNameSpace, compileUnit);
                if (warning == 0) // --If zero then we are good to go
                {
                    // --Generate the proxy code 
                    CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                    // --Compile the assembly proxy with the appropriate references
                    string[] assemblyReferences = new string[3] { "System.Web.Services.dll", "System.Xml.dll", "System.Configuration.dll" };
                    CompilerParameters parms = new CompilerParameters(assemblyReferences);
                    CompilerResults results = provider1.CompileAssemblyFromDom(parms, compileUnit);

                    ////foreach (CompilerError oops in results.Errors)
                    ////{
                    ////    MessageBox.Show(oops.ErrorText);
                    ////}

                    ////Invoke the web service method
                    object reetsubmitActual = results.CompiledAssembly.CreateInstance(serviceName);
                    Type reetsubmitType = results.CompiledAssembly.GetType(serviceName);

                    ////string actualWebserviceUrl = "https://test-fortress.wa.gov/dor/demo/content/DoingBusiness/MyAccount/Reeta/ReetSubmit.asmx";
                    ////string actualWebserviceUrl = this.webserviceUrl;

                    //// Acquiring the PropertyInfo of Url
                    PropertyInfo propertyInfo = reetsubmitType.GetProperty("Url");
                    Type urltype = propertyInfo.GetType();

                    //// If exists, set its Url property to new Url
                    if (propertyInfo != null)
                    {
                        //// MessageBox.Show(propertyInfo.Name);                
                        object webserviceUrlData = clientUrl + ".asmx";
                        propertyInfo.SetValue(reetsubmitActual, webserviceUrlData, null);
                    }

                    object[] args = new object[] { userName, password, reetXml, amend };
                    MethodInfo methodInfo = reetsubmitType.GetMethod(methodName);

                    if (methodInfo != null)
                    {
                        //// object res = methodInfo.Invoke(o, args1);
                        ////MessageBox.Show(Convert.ToString(methodInfo.Invoke(reetsubmitActual, args)));
                        submitValue = methodInfo.Invoke(reetsubmitActual, args);
                    }

                    return submitValue.ToString();

                    //// --Finally, Invoke the web service method 
                    ////object wsvcClass = results.CompiledAssembly.CreateInstance(serviceName);
                    ////MethodInfo mi = wsvcClass.GetType().GetMethod(methodName);

                    ////////wsvcClass.GetType().Namespace  
                    ////((WebClientProtocol)wsvcClass).Url = webServiceAsmxUrl;
                    ////submitValue = mi.Invoke(wsvcClass, args);
                    ////return submitValue.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }




        /// <summary>
        /// Deletes the XML file.
        /// </summary>
        private void DeleteXmlFile()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR"));

            if (dirInfo.Exists)
            {
                FileInfo[] fileList = dirInfo.GetFiles();

                foreach (FileInfo file in fileList)
                {
                    if (file.Name != SharedFunctions.GetResourceString("ThumbsdbValue"))
                    {
                        System.IO.File.Delete(file.FullName);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR"));
            }
        }



        /// <summary>
        /// Processes the reet XML.
        /// </summary>
        /// <returns>Returns Xml</returns>
        private string ProcessReetXml()
        {
            this.xmlFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + SharedFunctions.GetResourceString("TempDOR") + "\\" + "DOR" + DateTime.Now.Ticks + ".xml";
            string statementIdValue = string.Empty;

            if (this.submitCount == 0)
            {
                DataSet submitDataSet = new DataSet();
                DataRow[] submitDataRow;

                this.affidavitWorkQueueDataSet.AcceptChanges();
                //string ra="Treasurer = Approved";
                //submitDataRow = this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.Select(ra);
                //submitDataSet.Merge(submitDataRow);

                ////Create a temp datatable which contain statementid column
                DataRow row;
                DataTable dataTable = new DataTable();
                dataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.affidavitWorkQueueDataSet.SubmittalQueueDatatable.StatementIDColumn.ColumnName) });

                //for (int i = 0; i < 1; i++)
                //{
                row = dataTable.NewRow();
                row[0] = this.currentAffidavitStatementId;
                dataTable.Rows.Add(row);
                //}

                statementIdValue = TerraScanCommon.GetXmlString(dataTable);
                this.submitDataset = this.form15010Control.WorkItem.GetSubmitAffidavit(statementIdValue);
                this.affidavitCount = this.submitDataset.AFFIDAVIT.Rows.Count;
            }
            else
            {
                this.selectedCount = this.submitDataset.AFFIDAVIT.Rows.Count;
            }

            this.reetSubmitValue = this.submitDataset.GetXml();
            this.CreateXmlFile();

            StringBuilder finalData = new StringBuilder();
            StreamReader xmlData = new StreamReader(this.xmlFilePath);
            xmlData.BaseStream.Seek(0, SeekOrigin.Begin);
            while (xmlData.Peek() > -1)
            {
                finalData.Append(xmlData.ReadLine());
            }

            xmlData.Close();
            return finalData.ToString();
        }
        #endregion


        /// <summary>
        /// Creates the XML file.
        /// </summary>
        private void CreateXmlFile()
        {
            StreamWriter sw = new StreamWriter(this.xmlFilePath);
            sw.Write(this.reetSubmitValue);
            sw.Close();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.xmlFilePath);
            XmlNodeList firstNode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("REETA"));

            foreach (XmlNode nodeList in firstNode)
            {
                foreach (XmlNode nodeList1 in nodeList)
                {
                    foreach (XmlNode nodeList2 in nodeList1)
                    {
                        if (nodeList2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                        {
                            nodeList1.RemoveChild(nodeList2);
                        }
                    }
                }
            }

            XmlNodeList individualNodeList = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("INDIVIDUAL"));

            XmlNodeList xmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("INDIVIDUAL"));
            foreach (XmlElement node1 in xmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("IndividualType"))
                    {
                        string test = node2.InnerText;
                        node1.SetAttribute(SharedFunctions.GetResourceString("type"), test);
                    }

                    if (node2.Name == SharedFunctions.GetResourceString("IndividualType"))
                    {
                        node1.RemoveChild(node2);
                    }
                }

                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList parcelxmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("PARCEL"));
            string keyId = string.Empty;
            foreach (XmlElement node1 in parcelxmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        keyId = node2.InnerText;
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList useCodeNode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("USE_CODES"));
            XmlNode newEmployee;

            DataRow[] usecodeRow;
            string findExpression;
            string nextSibling = string.Empty;
            findExpression = "keyId";

            XmlNodeList useCodeNodes = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("USE_CODES"));

            foreach (XmlElement node1 in useCodeNodes)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == "StatementID")
                    {
                        keyId = node2.InnerText;

                        if (node2.NextSibling != null)
                        {
                            nextSibling = node2.NextSibling.Name;
                        }
                    }
                    else if (node2.Name == nextSibling)
                    {
                        usecodeRow = this.submitDataset.USE_CODES.Select(this.submitDataset.USE_CODES.StatementIDColumn.ColumnName + "=" + keyId);
                        node1.RemoveAll();
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][1].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][1].ToString().Trim()) && usecodeRow[0][1].ToString().Trim() != "0" && usecodeRow[0][1].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][1].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }

                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][2].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][2].ToString().Trim()) && usecodeRow[0][2].ToString().Trim() != "0" && usecodeRow[0][2].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][2].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }

                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                        newEmployee = xmlDoc.CreateElement("USE_CODE", null); //// CreateNode(XmlNodeType.Element, "USE_CODE",string.Empty);
                        ////newEmployee.InnerText = usecodeRow[0][3].ToString();

                        if (!string.IsNullOrEmpty(usecodeRow[0][3].ToString().Trim()) && usecodeRow[0][2].ToString().Trim() != "0" && usecodeRow[0][3].ToString().Trim() != "00")
                        {
                            newEmployee.InnerText = usecodeRow[0][3].ToString();
                        }
                        else
                        {
                            newEmployee.InnerText = string.Empty;
                        }

                        ////XmlNode x = xmlDoc.GetElementsByTagName("USE_CODE")[0].ChildNodes[0] ;
                        node1.AppendChild(newEmployee);
                    }
                }
            }

            foreach (XmlElement node1 in useCodeNode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            XmlNodeList supplementalxmlnode = xmlDoc.GetElementsByTagName(SharedFunctions.GetResourceString("SUPPLEMENTAL"));
            foreach (XmlElement node1 in supplementalxmlnode)
            {
                foreach (XmlElement node2 in node1)
                {
                    if (node2.Name == SharedFunctions.GetResourceString("StatementIDValue"))
                    {
                        node1.RemoveChild(node2);
                    }
                }
            }

            #region Removing Optional Tag

            XmlNodeList affidavitChildNodeList = null;
            XmlNodeList individualChildNodeList = null;
            XmlNodeList parcelChildNodeList = null;
            XmlNodeList useCodeChildNodeList = null;
            XmlNodeList supplimentalChildNodeList = null;
            XmlNodeList nodeNewList = xmlDoc.GetElementsByTagName("AFFIDAVIT");
            ArrayList affidavitArrayList = new ArrayList();
            affidavitArrayList.Add("COUNTY_NAME");
            affidavitArrayList.Add("EXEMPT_PROPERTY");
            affidavitArrayList.Add("FORESTLAND");
            affidavitArrayList.Add("OPEN_SPACE");
            affidavitArrayList.Add("HISTORIC");
            affidavitArrayList.Add("DOC_TYPE");
            affidavitArrayList.Add("DOC_DATE");
            affidavitArrayList.Add("GROSS_SELL_PRICE");
            affidavitArrayList.Add("TAXABLE_SELL_PRICE");
            affidavitArrayList.Add("EXCISE_TAX_STATE");
            affidavitArrayList.Add("EXCISE_TAX_LOCAL");
            affidavitArrayList.Add("STATE_TECH_FEE");
            affidavitArrayList.Add("TOTAL_DUE");
            affidavitArrayList.Add("RECEIPT_DATE");
            affidavitArrayList.Add("RECEIPT_NUM");

            ArrayList individualArrayList = new ArrayList();
            individualArrayList.Add("NAME");
            ArrayList parcelArrayList = new ArrayList();
            parcelArrayList.Add("NUMBER");
            parcelArrayList.Add("ASSESSED_VALUE");

            foreach (XmlNode node in nodeNewList)
            {
                if (node.HasChildNodes)
                {
                    affidavitChildNodeList = node.ChildNodes;
                }

                if (affidavitChildNodeList != null)
                {
                    for (int count = 0; count < affidavitChildNodeList.Count; count++)
                    {
                        if (affidavitChildNodeList[count].InnerText == "" && !affidavitChildNodeList[count].HasChildNodes)
                        {
                            // if (affidavitArrayList.BinarySearch(affidavitChildNodeList[count].Name) != 0)
                            if (!affidavitArrayList.Contains(affidavitChildNodeList[count].Name))
                            {
                                string nodeName = affidavitChildNodeList[count].Name;
                                node.RemoveChild(affidavitChildNodeList[count]);
                                count--;
                            }
                        }

                        if (affidavitChildNodeList[count].Name == "INDIVIDUAL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                individualChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count1 = 0; count1 < individualChildNodeList.Count; count1++)
                            {
                                if (individualChildNodeList[count1].InnerText.Trim() == "" && !individualChildNodeList[count1].HasChildNodes)
                                {
                                    // if (individualArrayList.BinarySearch(individualChildNodeList[count1].Name) != 0)
                                    if (!individualArrayList.Contains(individualChildNodeList[count1].Name))
                                    {
                                        string nodeName = individualChildNodeList[count1].Name;
                                        affidavitChildNodeList[count].RemoveChild(individualChildNodeList[count1]);
                                        count1--;
                                    }
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "PARCEL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                parcelChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count2 = 0; count2 < parcelChildNodeList.Count; count2++)
                            {
                                if (parcelChildNodeList[count2].InnerText.Trim() == "" && !parcelChildNodeList[count2].HasChildNodes)
                                {
                                    if (!parcelArrayList.Contains(parcelChildNodeList[count2].Name))
                                    {
                                        string nodeName = parcelChildNodeList[count2].Name;
                                        affidavitChildNodeList[count].RemoveChild(parcelChildNodeList[count2]);
                                        count2--;
                                    }
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "USE_CODES")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                useCodeChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count3 = 0; count3 < useCodeChildNodeList.Count; count3++)
                            {
                                if (useCodeChildNodeList[count3].InnerText.Trim() == "")
                                {
                                    affidavitChildNodeList[count].RemoveChild(useCodeChildNodeList[count3]);
                                    count3--;
                                }
                            }
                        }
                        else if (affidavitChildNodeList[count].Name == "SUPPLEMENTAL")
                        {
                            if (affidavitChildNodeList[count].HasChildNodes)
                            {
                                supplimentalChildNodeList = affidavitChildNodeList[count].ChildNodes;
                            }

                            for (int count4 = 0; count4 < supplimentalChildNodeList.Count; count4++)
                            {
                                if (supplimentalChildNodeList[count4].InnerText.Trim() == "" && !supplimentalChildNodeList[count4].HasChildNodes)
                                {
                                    string nodeName = supplimentalChildNodeList[count4].Name;
                                    affidavitChildNodeList[count].RemoveChild(supplimentalChildNodeList[count4]);
                                    count4--;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            xmlDoc.Save(this.xmlFilePath);
        }
        private string ConvertToXML(ArrayList parcelIds)
        {
            DataTable dt = new DataTable("ParcelTable");
            dt.Columns.Add("ParcelID", typeof(Int32));
            foreach (var r in parcelIds)
            {
                dt.Rows.Add(r);
            }
            dt = dt.DefaultView.ToTable(true, "ParcelID");
            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            xmlstr = xmlstr.Replace("DocumentElement", "Root");
            xmlstr = xmlstr.Replace("ParcelTable", "Table");
            return (xmlstr);
        }

        private void LoadOpenSpaceField(ArrayList parcelIds)
        {
            var str = ConvertToXML(parcelIds);
            F15010ExciseAffidavitData parcelData = this.form15010Control.WorkItem.F15010_ListOpenSpaceField(str);
            if (parcelData.Tables["OpenSpaceData"].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(parcelData.Tables["OpenSpaceData"].Rows[0][0].ToString()))
                {
                    this.AffidvtOpenSpaceComboBox.SelectedIndex = 1;
                    this.AffDvtContinuanceComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.AffidvtOpenSpaceComboBox.SelectedIndex = 0;
                    this.AffDvtContinuanceComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                this.AffidvtOpenSpaceComboBox.SelectedIndex = 0;
                this.AffDvtContinuanceComboBox.SelectedIndex = 0;
            }
        }

        private void parcelList(string parcelID)
        {
            if (arrayList != null && arrayList.Count > 0)
            {
                arrayList.Add(parcelID);
            }
            else
            {
                // Hashtable ht = new Hashtable();
                arrayList = new ArrayList();
                arrayList.Add(parcelID);
            }
        }

        private void RemoveParcelList(int selectedindex, string parcelNum)
        {
            if ((arrayList != null) && arrayList.Count > selectedindex)
            {
                arrayList.RemoveAt(selectedindex);
            }
        }

        private void StatusLinkLabel_Click(object sender, EventArgs e)
        {
            int formno = 9102;
            Form formInfo = new Form();
            object[] optionalParameter = new object[] { 5, this.ownerId };
            formInfo = this.form15010Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9102, optionalParameter, this.form15010Control.WorkItem);
            if (formInfo != null)
            {
                if (formInfo.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
    }
}