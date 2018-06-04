//--------------------------------------------------------------------------------------------
// <copyright file="F1105.cs" company="Congruent">
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
// 19 July 06		GUHAN S	           Created
// 04 Jan 2007      JAYANTHI           Modified (Inconsistent Background color)
// 05 Jan 2007      GUHAN S            Modified (CalcDueTaxableSaleTextBox Disable while Clearing Query by form   )
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using D1100;
    using TerraScan.Common;
    using System.Collections;
    using TerraScan.Utilities;
    using TerraScan.UI.Controls;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// f1105
    /// </summary>
    [SmartPart]
    public partial class F1105 : BaseSmartPart
    {
        #region Common

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

        #endregion

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        /// <summary>
        /// affDvtTotal
        /// </summary>
        private bool affDvtTotal;

        /// <summary>
        /// keep track of save status
        /// </summary>
        private int affDvtRecordCount;

        /// <summary>
        /// keep track of save status
        /// </summary>
        private bool affDvtSave;

        /// <summary>
        /// RecordNavigatorSmartPart
        /// </summary>
        private RecordNavigatorSmartPart recordNavigatorSmartPart;

        /// <summary>
        /// ToolBoxSmartPart
        /// </summary>
        private ToolBoxSmartPart toolBoxSmartPart;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// userWhereConditionGeneral
        /// </summary>
        private StringBuilder userWhereConditionGeneral = new StringBuilder(String.Empty);

        /// <summary>
        /// userWhereConditionParties
        /// </summary>
        private StringBuilder userWhereConditionParties = new StringBuilder(String.Empty);

        /// <summary>
        /// userWhereConditionParcels
        /// </summary>
        private StringBuilder userWhereConditionParcels = new StringBuilder(String.Empty);

        /// <summary>
        /// userWhereConditionAffdvt
        /// </summary>
        private StringBuilder userWhereConditionAffdvt = new StringBuilder(String.Empty);

        /// <summary>
        /// userWhereConditionAmtDue
        /// </summary>
        private StringBuilder userWhereConditionAmtDue = new StringBuilder(String.Empty);

        /// <summary>
        /// Created control for Filtered Status
        /// </summary>
        private TerraScanButton filteredStatusControl = new TerraScanButton();

        /// <summary>
        /// Created control for clear Filter
        /// </summary>
        private Control clearFilterControl = new Control();

        /// <summary>
        /// formFilterType variable is used to find the type of filter in the form. 
        /// </summary>   
        private TerraScanCommon.FilterType formFilterType;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// WhereCondition variable is used execute current records. 
        /// </summary>  
        private string whereCondition = String.Empty;

        /// <summary>
        /// userDefinedWhereCondition variable is used to store user defined where condition. 
        /// </summary>
        private string userDefinedWhereCondition = String.Empty;

        /// <summary>
        /// filterTypeId variable is used to find filteredrecordsid depends on the FormFilterType. 
        /// </summary> 
        private int filterTypeId;

        /// <summary>
        /// Created string for current snapshotName
        /// </summary>
        private string currentSnapshotName = string.Empty;

        /// <summary>
        /// Created string for current snapshotDescription
        /// </summary>
        private string currentSnapshotDescription = string.Empty;

        /// <summary>
        /// queryControlArray
        /// </summary>
        private TerraScanTextBox[] queryControlArrayAffdvt;

        /// <summary>
        /// queryControlArray
        /// </summary>
        private TerraScanTextBox[] queryControlArrayGeneral;

        /// <summary>
        /// queryControlArray
        /// </summary>
        private TerraScanTextBox[] queryControlArrayParties;

        /// <summary>
        /// queryControlArray
        /// </summary>
        private TerraScanTextBox[] queryControlArrayParcels;

        /// <summary>
        /// queryControlArray
        /// </summary>
        private TerraScanTextBox[] queryControlArrayAmtDue;

        /// <summary> /// 
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// Store To keep theCahgnes
        /// </summary>
        private bool affDvtKeypressed;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private AffidavitDistrictSelectionData districtSelectionDataSet = new AffidavitDistrictSelectionData();

        /// <summary>
        /// Store To keep statementIdExist or  not
        /// </summary>
        private bool receiptIDExist;

        /// <summary>
        /// used to check Statemetn Id Exist or Not
        /// </summary>
        private bool statementExist;

        /// <summary>
        /// Used To StoreExchage RateId
        /// </summary>
        private string exciseRateId;

        /// <summary>
        /// form1105Control Control Name
        /// </summary>
        private F1105Controller form1105Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// exciseTaxAffidavitDataSet
        /// </summary>
        private TerraScan.BusinessEntities.ExciseTaxAffidavitData exciseTaxAffidavitDataSet = new ExciseTaxAffidavitData();

        /// <summary>
        /// exciseIndividualtype
        /// </summary>
        private TerraScan.BusinessEntities.ExciseIndividualType exciseIndividualtype = new ExciseIndividualType();

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportFileIdHashTable = new Hashtable();

        /// <summary>
        /// validDataSet
        /// </summary>
        private bool validDataSet;

        /// <summary>
        /// Assgin The DistrictID
        /// </summary>
        private int districtId;

        /// <summary>
        /// Used to Hold TaxCode
        /// </summary>
        private string taxCode;

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
        /// Hold the Statement Id
        /// </summary>
        private int statId = -1;

        #endregion

        #region Parties Variable

        /// <summary>
        /// Keep Track Of Removed Parties
        /// </summary>
        private bool partiesRemoved;

        /// <summary>
        ///  Keep A Track Of Chages inv AffDvt Header;
        /// </summary>
        private bool headerChanges;

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
        /// To Check PartyHeader Mandatory Field are Filled or not
        /// </summary>
        private bool partySave;

        #endregion

        #region Parcel

        /// <summary>
        /// Checks For ParcelSave
        /// </summary>
        private bool parcelSave;

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
        /// validIndividualTypeDataSet
        /// </summary>
        private bool validIndividualTypeDataSet;

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
        ///  Used To Store ownerId
        /// </summary>
        private int ownerId;

        #endregion

        #region AmountDue
        /// <summary>
        /// exciseTaxAffDvtAmountDueDataset store 
        /// </summary>
        private ExciseTaxAffidavitAmountDueData exciseTaxAffDvtAmountDueDataset = new ExciseTaxAffidavitAmountDueData();

        /// <summary>
        /// to check amount due dataset is valid  or not
        /// </summary>
        private bool validAmountDueDataset;

        #endregion

        #region Affdvt

        /// <summary>
        /// Store the Current Record
        /// </summary>
        private RecordNavigationEntity recordNavigationEntityAfdvt;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private int affdvtButtonOperation;

        /// <summary>
        ///  Store the Button AffDvt Operation
        /// </summary>
        private bool affdvtRemove;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1105"/> class.
        /// </summary>
        public F1105()
        {
            this.InitializeComponent();
            this.SetQueryingFieldName();
            this.GeneralPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GeneralPictureBox.Height, this.GeneralPictureBox.Width, "General", 28, 81, 128);
            this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, "Parties", 174, 150, 94);
            this.ParcelsPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelsPictureBox.Height, this.ParcelsPictureBox.Width, "Parcels", 28, 81, 128);
            this.AffidavitDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AffidavitDetailPictureBox.Height, this.AffidavitDetailPictureBox.Width, "Affidavit Detail", 174, 150, 94);
            this.AmountDuePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AmountDuePictureBox.Height, this.AmountDuePictureBox.Width, "Amount Due", 28, 81, 128);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Supplement", 174, 150, 94);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:f1105"/> class.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        public F1105(int statementId)
        {
            this.InitializeComponent();
            this.LoadExciseTaxAffidavit(null, statementId);
            this.SetQueryingFieldName();
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Declare the event SetRecordCount        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Declare the event SetActiveRecord        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Declare the event SetActiveRecordButtons        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        ///// <summary>
        ///// Comments Count
        ///// </summary>
        //// [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText, PublicationScope.WorkItem)]
        //// public event EventHandler<DataEventArgs<AdditionalOperationCountEntity>> SetButtonText;

        #endregion

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
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the FilterTypeId
        /// </summary>
        /// <value>The filterTypeId.</value>
        [Description("Display Data based on Filtered TypeId.")]
        public int FilterTypeId
        {
            get
            {
                return this.filterTypeId;
            }

            set
            {
                this.filterTypeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotName
        /// </summary>
        /// <value>The current Snapshot Name.</value>
        [Description("Display Data based on currentRecordSetCount.")]
        public string CurrentSnapshotName
        {
            get { return this.currentSnapshotName; }
            set { this.currentSnapshotName = value; }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotDescription
        /// </summary>
        /// <value>The current Snapshot Description.</value>
        [Description("Display Data based on currentRecordSetCount.")]
        public string CurrentSnapshotDescription
        {
            get { return this.currentSnapshotDescription; }
            set { this.currentSnapshotDescription = value; }
        }

        /// <summary>
        /// Property Hold the Statement ID
        /// </summary>
        public int StatId
        {
            get { return this.statId; }
            set { this.statId = value; }
        }

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F1105Controller F1105Control
        {
            get { return this.form1105Control as F1105Controller; }
            set { this.form1105Control = value; }
        }

        /// <summary>
        /// Gets or sets the FormFilterType
        /// </summary>
        /// <value>The formFilterType.</value>
        [Description("Display Data based on Filtered Type.")]
        public TerraScanCommon.FilterType FormFilterType
        {
            get { return this.formFilterType; }
            set { this.formFilterType = value; }
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

        /// <summary>
        /// Gets or sets the clear filter control.
        /// </summary>
        /// <value>The clear filter control.</value>
        private Control ClearFilterControl
        {
            get { return this.clearFilterControl; }
            set { this.clearFilterControl = value; }
        }

        /// <summary>
        /// Gets or sets the clear filter control.
        /// </summary>
        /// <value>The clear filter control.</value>
        private TerraScanButton FilteredStatusControl
        {
            get { return this.filteredStatusControl; }
            set { this.filteredStatusControl = value; }
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
        /// Gets or sets the whereCondition
        /// </summary>
        /// <value>The whereCondition.</value>
        [Description("Display Data based on WhereCond.")]
        private string WhereCondition
        {
            get { return this.whereCondition; }
            set { this.whereCondition = value.ToUpper(); }
        }
        #endregion

        /// <summary>
        /// Gets or sets the userDefinedWhereCondition
        /// </summary>
        /// <value>The userDefinedWhereCondition.</value>
        [Description("Display Data based on userDefinedWhereCondition.")]
        private string UserDefinedWhereCondition
        {
            get
            {
                return this.userDefinedWhereCondition;
            }

            set
            {
                this.userDefinedWhereCondition = value.ToUpper();
            }
        }
        #region Event Subcription
        /// <summary>
        /// Called when [audit link click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        /// 
        [EventSubscription(EventTopicNames.AuditLinkClick, ThreadOption.UserInterface)]
        public void OnAuditLinkClick(object sender, DataEventArgs<LinkLabel> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count > 0)
                {
                    ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.One);
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
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.F1105Control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
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
                if (this.CheckPageStatus())
                {
                    if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("QueryByFormModeChange"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RecordConflict")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        this.ClearQueryByFormFields();
                    }
                    else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                    {
                        if (this.RetrieveRecordIndex(Convert.ToInt32(optionalParams[0])) < 1)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("QueryByFormModeChange"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RecordConflict")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }

                            this.ClearQueryByFormFields();
                        }
                    }

                    this.currentAffidavitStatementId = Convert.ToInt32(optionalParams[0]);
                    this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                }
            }
        }

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            if (this.CheckUpdateMode())
            {
                this.recordNavigationEntityAfdvt = e.Data;
                this.SetCaluDueButtonsBGColor();
                this.currentAffidavitStatementId = this.RetrieveStatementId(this.recordNavigationEntityAfdvt.RecordIndex);
                this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
            }
            else
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            ////// Updates 
                            /*this.SaveParcelHeader();
                            //this.SavePartiesHeader();
                            //this.SaveAffDvt();
                            //if (this.affDvtKeypressed == false && this.partiesHeaderkeyPressed == false && this.parcelHeaderKeyPressed == false && this.affdvtRemove == false) 
                            //{
                            //    RecordNavigationEntity recordNavigationEntity = e.Data;
                            //    this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            //    this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                            //    this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                            //    this.affdvtRemove = false;
                            //    this.affDvtKeypressed = false;
                            //    this.partiesHeaderkeyPressed = false;
                            //    this.parcelHeaderKeyPressed = false;
                            //    this.currentAffidavitStatementId = this.RetrieveStatementId(recordNavigationEntity.RecordIndex);
                            //    this.LoadExciseTaxAffidavit(null,this.currentAffidavitStatementId);
                            //}*/

                            //// Checks For Any Changes in  Parcel
                            /*   if (this.parcelHeaderKeyPressed == true || this.parcelButtonOperation == (int)ButtonOperation.New)
                               {
                                   this.SaveParcelHeader();
                               }
                               //// if parcel saved goes to newxt
                               if (this.parcelSave)
                               {
                                   if (this.partiesHeaderkeyPressed == true || this.partyHeaderButtonOperation == (int)ButtonOperation.New)
                                   {
                                       this.SavePartiesHeader();
                                   }
                               }
                               //// if parties saved goes to newxt

                               if (this.affDvtKeypressed == true || this.affdvtButtonOperation == (int)ButtonOperation.New || this.headerChanges)
                               {
                                   this.SaveAffDvt();
                               }*/

                            if (this.partiesHeaderkeyPressed == true || this.partyHeaderButtonOperation == (int)ButtonOperation.New)
                            {
                                this.SavePartiesHeader();
                            }
                            else
                            {
                                this.affDvtKeypressed = true;
                                this.partySave = true;
                            }

                            if (this.partySave)
                            {
                                //// Checks For Any Changes in  Parcel
                                if (this.parcelHeaderKeyPressed == true || this.parcelButtonOperation == (int)ButtonOperation.New)
                                {
                                    this.SaveParcelHeader();
                                }
                                else
                                {
                                    this.affDvtKeypressed = true;
                                    this.parcelSave = true;
                                }
                            }

                            if (this.parcelSave && this.partySave)
                            {
                                //// if parties saved goes to newxt
                                if (this.affDvtKeypressed == true || this.affdvtButtonOperation == (int)ButtonOperation.New)
                                {
                                    this.SaveAffDvt();
                                }
                            }
                            else
                            {
                                this.affDvtSave = false;
                            }

                            if (this.affDvtSave)
                            {
                                this.parcelButtonOperation = (int)ButtonOperation.Empty;
                                this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                                this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                                this.affdvtRemove = false;
                                this.affDvtKeypressed = false;
                                this.partiesHeaderkeyPressed = false;
                                this.parcelHeaderKeyPressed = false;
                                this.headerChanges = false;
                                this.SetCaluDueButtonsBGColor();
                                RecordNavigationEntity recordNavigationEntity = e.Data;
                                this.currentAffidavitStatementId = this.RetrieveStatementId(recordNavigationEntity.RecordIndex);
                                this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                            }

                            /*if (pageSatus)
                            //{   
                            //    this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            //    this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                            //    this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                            //    this.affdvtRemove = false;
                            //    this.affDvtKeypressed = false;
                            //    this.partiesHeaderkeyPressed = false;
                            //    this.parcelHeaderKeyPressed = false;
                            //    this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                            //}
                            //else
                            //{
                            //    pageSatus = false;
                            //}*/

                            break;
                        }

                    case DialogResult.No:
                        {
                            //// WithoutSaving Moves Record
                            RecordNavigationEntity recordNavigationEntity = e.Data;
                            this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtRemove = false;
                            this.affDvtKeypressed = false;
                            this.partiesHeaderkeyPressed = false;
                            this.parcelHeaderKeyPressed = false;
                            this.headerChanges = false;
                            this.partiesRemoved = false;
                            this.SetCaluDueButtonsBGColor();
                            this.currentAffidavitStatementId = this.RetrieveStatementId(recordNavigationEntity.RecordIndex);
                            this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            break;
                        }
                } ///// End Case
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            /* if (this.CheckPageStatus(true))
            // {*/
            this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            /* }*/
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryByFromButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.QueryByFormFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Queries the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckUpdateMode())
            {
                this.QueryUtilityFunction(this, new DataEventArgs<Button>(e.Data));
            }
            else
            {
                this.QueryByFormModeChange(e);
            }
        }

        /// <summary>
        /// Snapshots the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void SnapshotUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckUpdateMode())
            {
                this.SnapshotUtilityFunction(this, new DataEventArgs<Button>(e.Data));
            }
            else
            {
                this.QueryByFormModeChange(e);
            }
        }

        /// <summary>
        /// Clears the filter button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick, Thread = ThreadOption.UserInterface)]
        public void ClearFilterButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.ClearFilterFunction(this, new DataEventArgs<Button>(e.Data));
        }

        #endregion

        #region affDvt Static Method
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
        #endregion

        #region Common Static Method

        /// <summary>
        /// Shows the doument calender.
        /// </summary>
        /// <param name="setCalender">The set calender.</param>
        /// <param name="datePanel">The date panel.</param>
        /// <param name="datePic">The date pic.</param>
        /// <param name="mainPanel">The main panel.</param>
        private static void ShowDoumentCalender(MonthCalendar setCalender, Panel datePanel, Button datePic, Panel mainPanel)
        {
            setCalender.Visible = true;
            setCalender.ScrollChange = 1;
            setCalender.BringToFront();
            //// Display the Calender control near the Calender Picture box.
            setCalender.Left = 510;
            setCalender.Top = (mainPanel.Top + datePanel.Top + datePic.Top) + 25;
            setCalender.Focus();

            /*   setCalender.Visible = true;
            setCalender.ScrollChange = 1;

               // Display the Calender control near the Calender Picture box.
            setCalender.Left = datePanel.Left + datePic.Left + datePic.Width;
            setCalender.Top = datePanel.Top + datePic.Top;
            // setCalender.Tag = datePic.Tag;
            setCalender.Focus(); */
        }

        /// <summary>
        /// Shows the payment date calender.
        /// </summary>
        /// <param name="setCalender">The set calender.</param>
        /// <param name="datePanel">The date panel.</param>
        /// <param name="datePic">The date pic.</param>
        /// <param name="mainPanel">The main panel.</param>
        private static void ShowCalender(MonthCalendar setCalender, Panel datePanel, Button datePic, Panel mainPanel)
        {
            setCalender.Visible = true;
            setCalender.ScrollChange = 1;
            setCalender.BringToFront();
            //// Display the Calender control near the Calender Picture box.
            setCalender.Left = (mainPanel.Left + datePanel.Left + datePic.Left + datePic.Width) + 10;
            if (mainPanel.Width < setCalender.Left)
            {
                setCalender.Left = 500;
            }

            setCalender.Top = (mainPanel.Top + datePanel.Top + datePic.Top) + 75;
            setCalender.Focus();

            /*   setCalender.Visible = true;
            setCalender.ScrollChange = 1;

               // Display the Calender control near the Calender Picture box.
            setCalender.Left = datePanel.Left + datePic.Left + datePic.Width;
            setCalender.Top = datePanel.Top + datePic.Top;
            // setCalender.Tag = datePic.Tag;
            setCalender.Focus(); */
        }

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
        #endregion

        #region Common Methods For Query Smartpart

        /// <summary>
        /// Clears the query by form fields.
        /// </summary>
        private void ClearQueryByFormFields()
        {
            ////Set page status
            this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
            this.FormFilterType = TerraScanCommon.FilterType.None;
            this.FilterTypeId = 0;
            ////set necessary controls property
            this.ClearFilterControl.Enabled = false;
            this.FilteredStatusControl.FilterStatus = false;
            this.UserDefinedWhereCondition = String.Empty;
            this.WhereCondition = string.Empty;
            if (this.PermissionFiled.editPermission)
            {
                this.TreasurerStatusButton.Enabled = true;
                this.AssessorStatusButton.Enabled = true;
            }

            this.FilteredButton.FilterStatus = false;
            this.SetControlsProperty();
        }

        /// <summary>
        /// Loads the excise tax affidavit.
        /// </summary>
        /// <param name="tempDataTable">The temp data table.</param>
        /// <param name="loadStatementId">The load statement id.</param>
        private void LoadExciseTaxAffidavit(DataTable tempDataTable, int? loadStatementId)
        {
            if (tempDataTable == null && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Clear();
                    this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Merge(this.form1105Control.WorkItem.GetAffidavitStatementId(1105, null, null));
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
            else if (tempDataTable != null)
            {
                this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Clear();
                this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Merge(tempDataTable);
            }

            if (this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count > 0)
            {
                this.affDvtTotal = true;
                ////  this.currentAffidavitStatementId = Convert.ToInt32(this.affidavitStatementIdDataSet.ListAffidavitStatementId.Rows[0]["StatementID"].ToString());
                try
                {
                    Boolean findRow = false;

                    if (loadStatementId != null)
                    {
                        DataRow[] findRows;
                        findRows = this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Select("KeyID =" + loadStatementId);
                        if (findRows.Length > 0)
                        {
                            findRow = true;
                        }
                    }

                    if (this.affdvtButtonOperation != (int)ButtonOperation.New && findRow != true)
                    {
                        this.GetStatementID();
                    }
                    else
                    {
                        this.currentAffidavitStatementId = Convert.ToInt32(loadStatementId.ToString());
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

                this.SetAttachmentCommentsCount();
                int recordIndex = 0;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.exciseTaxAffidavitDataSet.General.Clear();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Clear();
                    this.exciseTaxAffidavitDataSet.ParcelHeader.Clear();
                    this.exciseTaxAffidavitDataSet.AmountDue.Clear();
                    this.exciseTaxAffidavitDataSet.Affidavit.Clear();
                    this.exciseTaxAffidavitDataSet.Suppliment.Clear();

                    //// this.exciseTaxAffidavitDataSet = this.form1105Control.WorkItem.LoadExciseTaxAffidavit(this.currentAffidavitStatementId);

                    this.exciseTaxAffidavitDataSet.Merge(this.form1105Control.WorkItem.LoadExciseTaxAffidavit(Convert.ToInt32(this.currentAffidavitStatementId)));
                    //// this.exciseTaxAffidavitDataSet = this.form1105Control.WorkItem.LoadExciseTaxAffidavit(Convert.ToInt32(this.currentAffidavitStatementId));

                    //// this.exciseTaxAffidavitDataSet.Merge(this.form1105Control.WorkItem.LoadExciseTaxAffidavit(Convert.ToInt32(this.currentAffidavitStatementId)));
                    this.exciseIndividualtype = this.form1105Control.WorkItem.GetExciseIndividualType;
                    //// this.exciseTaxAffidavitDataSet = this.form1105Control.WorkItem.LoadExciseTaxAffidavit(statementID);
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                //// Check for Vaild DataSET
                this.validDataSet = CheckValidDataSet(this.exciseTaxAffidavitDataSet);
                this.validIndividualTypeDataSet = CheckValidDataSet((DataSet)this.exciseIndividualtype);
                this.PaymentDateCalender.Visible = false;
                this.FormDateCalender.Visible = false;
                this.AffDvtMonthCalendar.Visible = false;
                this.SuppliMonthCalendar.Visible = false;
                //this.ExciseAffidavitAuditLink.Enabled = true;
                InitComboBoxValues(this.PersonlaPropertyComboBox);
                this.SetGeneralComboBox();

                if (this.validIndividualTypeDataSet)
                {
                    this.InitIndividualTypeComboBox();
                }

                this.ReceiptFormButton.Enabled = true;
                this.LoadAffDvtCombo();
                this.LoadSupplimentCombo();
                this.GetExciseAffidavitDetails();
                this.additionalOperationSmartPart.Enabled = true;
                this.toolBoxSmartPart.Enabled = true;
                this.recordNavigatorSmartPart.Enabled = true;
                if (this.PermissionFiled.newPermission || this.PermissionFiled.editPermission)
                {
                    //// Set The Parcel Button
                    if (this.receiptIDExist)
                    {
                        this.SetParcelGridButtons(ButtonOperation.ReceiptidNotExist);
                    }
                    else
                    {
                        //// CanEdit
                        this.SetParcelGridButtons(ButtonOperation.Empty);
                    }
                }
                else
                {
                    this.SetParcelGridButtons(ButtonOperation.NoPermission);
                }

                //// Set The Parties Button
                if (this.PermissionFiled.newPermission || this.PermissionFiled.editPermission)
                {
                    if (this.receiptIDExist)
                    {
                        this.SetPartiesGridButtons(ButtonOperation.ReceiptidNotExist);
                    }
                    else
                    {
                        this.SetPartiesGridButtons(ButtonOperation.Empty);
                    }
                }
                else
                {
                    this.SetPartiesGridButtons(ButtonOperation.NoPermission);
                }

                //// Set The Parties Button
                if (this.receiptIDExist)
                {
                    this.SetAffDvtButton(ButtonOperation.ReceiptidNotExist);
                }
                else
                {
                    this.SetAffDvtButton(ButtonOperation.Empty);
                }

                recordIndex = this.RetrieveIndex();
                this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                this.SetRecordCount(this, new DataEventArgs<int>(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count));
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { recordIndex, this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count }));
                //// this.ExciseAffidavitAuditLink.Text = "tTR_Statement [StatementID] " + this.StatementIDTextBox.Text.Trim();
                this.footerSmartPart.KeyId = this.currentAffidavitStatementId;

                if (this.exciseTaxAffidavitDataSet.Suppliment.Rows.Count < 0)
                {
                    this.TreasurerStatusButton.Text = "Treasurer - " + this.exciseTaxAffidavitDataSet.Suppliment.Rows[0][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn];
                    this.AssessorStatusButton.Text = "Assessor - " + this.exciseTaxAffidavitDataSet.Suppliment.Rows[0][this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn];
                }

                ////if (this.GeneralHeaderPaymentDate.Enabled)
                ////{
                ////       this.GeneralHeaderPaymentDate.BackColor = PaymentDatePanel.BackColor;  
                ////}
                this.NewAffdvtButton.Focus();
            }
            else
            {
                this.exciseIndividualtype = this.form1105Control.WorkItem.GetExciseIndividualType;
                if (this.exciseIndividualtype != null && this.exciseIndividualtype._ExciseIndividualType.Rows.Count > 0)
                {
                    this.InitIndividualTypeComboBox();
                }

                this.affDvtTotal = false;
                this.SetAffDvtControls(false);
                this.ReceiptFormButton.Enabled = false;
                this.LoadSupplimentCombo();
                this.SetEnableSatustParcelHeader(false);
                this.SetEnableStatusforPartiesControls(false);
                this.SetPartiesGrid();
                this.SetParcelGrid();
                this.SetAffDvtButton(ButtonOperation.NoRecordFound);
                this.SetCalenderInvisible();
                this.SetParcelGridButtons(ButtonOperation.NoRecordFound);
                this.SetPartiesGridButtons(ButtonOperation.NoRecordFound);
                InitComboBoxValues(this.PersonlaPropertyComboBox);
                this.SetGeneralComboBox();
                this.LoadAffDvtCombo();
                this.LoadSupplimentCombo();
                this.ClearAllFields();
                this.additionalOperationSmartPart.Enabled = false;
                this.toolBoxSmartPart.Enabled = false;
                this.recordNavigatorSmartPart.Enabled = false;
                //this.ExciseAffidavitAuditLink.Enabled = false;
                this.SetActiveRecord(this, new DataEventArgs<int>(0));
                this.SetRecordCount(this, new DataEventArgs<int>(0));
                this.ClearAmountDueControls();
                this.NewAffdvtButton.Focus();
            }
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (!string.IsNullOrEmpty(this.currentAffidavitStatementId.ToString()))
                {
                    this.additionalOperationSmartPart.KeyId = this.currentAffidavitStatementId;
                    additionalOperationCountEntity.AttachmentCount = this.F1105Control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.currentAffidavitStatementId), TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.F1105Control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.currentAffidavitStatementId), TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Nons the query fields status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void NonQueryFieldsStatus(bool status)
        {
            this.CalcDueSubTotalTextBox.Visible = status;
            this.CalcDueFeesTextBox.Visible = status;
            this.CalcDueTtlAmountTextBox.Visible = status;
            this.SuppliAgentNameTextBox.Visible = status;
            this.SuppliInstTypeTextBox.Visible = status;
            this.SuppliInstDateTextBox.Visible = status;
            this.SuppliFirmNameTextBox.Visible = status;
            this.SuppliReasonHeldTextBox.Visible = status;
            this.SuppliA1Combo.Visible = status;
            this.SuppliA1TtlDbtTextBox.Visible = status;
            this.SuppliA1GrntPaysGranTextBox.Visible = status;
            this.SuppliA2Combo.Visible = status;
            this.SuppliA2TtlDbtTextBox.Visible = status;
            this.SuppliA2GrantPaysGranTextBox.Visible = status;
            this.SuppliA1DbtRateTextBox.Visible = status;
            this.SuppliB1Combo.Visible = status;
            this.SuppliB2Combo.Visible = status;
            this.SuppliB1TtlDbtTextBox.Visible = status;
            this.SuppliB3Combo.Visible = status;
            this.SuppliB2TtlDbtTextBox.Visible = status;
            this.SuppliB4Combo.Visible = status;
            this.SuppliRefiCombo.Visible = status;
            this.SuppliGiftedEquityTextBox.Visible = status;
            this.SuppliGranSignTextBox.Visible = status;
            this.SuppliGranteSignTextBox.Visible = status;
            this.SuppliFNameTextBox.Visible = status;
            this.SuppliGNameTextBox.Visible = status;
            this.SuppliInsDatePict.Visible = status;
        }

        /// <summary>
        /// Clears the non query fields.
        /// </summary>
        private void ClearNonQueryFields()
        {
            this.CalcDueSubTotalTextBox.Text = string.Empty;
            this.CalcDueFeesTextBox.Text = string.Empty;
            this.CalcDueTtlAmountTextBox.Text = string.Empty;
            this.SuppliAgentNameTextBox.Text = string.Empty;
            this.SuppliInstTypeTextBox.Text = string.Empty;
            this.SuppliInstDateTextBox.Text = string.Empty;
            this.SuppliFirmNameTextBox.Text = string.Empty;
            this.SuppliReasonHeldTextBox.Text = string.Empty;
            this.SuppliA1Combo.Text = string.Empty;
            this.SuppliA1TtlDbtTextBox.Text = string.Empty;
            this.SuppliA1GrntPaysGranTextBox.Text = string.Empty;
            this.SuppliA2Combo.Text = string.Empty;
            this.SuppliA2TtlDbtTextBox.Text = string.Empty;
            this.SuppliA2GrantPaysGranTextBox.Text = string.Empty;
            this.SuppliA1DbtRateTextBox.Text = string.Empty;
            this.SuppliB1Combo.Text = string.Empty;
            this.SuppliB2Combo.Text = string.Empty;
            this.SuppliB1TtlDbtTextBox.Text = string.Empty;
            this.SuppliB3Combo.Text = string.Empty;
            this.SuppliB2TtlDbtTextBox.Text = string.Empty;
            this.SuppliB4Combo.Text = string.Empty;
            this.SuppliRefiCombo.Text = string.Empty;
            this.SuppliGiftedEquityTextBox.Text = string.Empty;
            this.SuppliGranSignTextBox.Text = string.Empty;
            this.SuppliGranteSignTextBox.Text = string.Empty;
            this.SuppliFNameTextBox.Text = string.Empty;
            this.SuppliGNameTextBox.Text = string.Empty;
            this.ParcelVScrolBar.Visible = true;
            this.ParcelVScrolBar.Enabled = false;
            this.PartiesDataGridView.DataSource = null;
            this.ParcelHeaderDataGridView.DataSource = null;
            this.PartiesHeaderVscrollBar.Visible = true;
            this.PartiesHeaderVscrollBar.Enabled = false;
        }

        /// <summary>
        /// Filters the record set.
        /// </summary>
        private void FilterRecordSet()
        {
            ////changing cursor type
            this.Cursor = Cursors.WaitCursor;

            ////used to store parsed where condition
            //// string returnValue = String.Empty;
            string whereClause = string.Empty;
            string userWhereClause = string.Empty;

            StringBuilder whereClauseGeneral = new StringBuilder(String.Empty);
            StringBuilder whereClauseParties = new StringBuilder(String.Empty);
            StringBuilder whereClauseParcels = new StringBuilder(String.Empty);
            StringBuilder whereClauseAffdvt = new StringBuilder(String.Empty);
            StringBuilder whereClauseAmtDue = new StringBuilder(String.Empty);

            this.userWhereConditionGeneral = new StringBuilder(string.Empty);
            this.userWhereConditionAffdvt = new StringBuilder(string.Empty);
            this.userWhereConditionParties = new StringBuilder(string.Empty);
            this.userWhereConditionParcels = new StringBuilder(string.Empty);
            this.userWhereConditionAmtDue = new StringBuilder(string.Empty);

            ////true when the query execution succeeded
            bool querySucceeded = false;

            ////true when query is invalid
            bool invalidQuery = false;

            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArrayGeneral, queryingFields.ExciseAffidvtQueryingFields, whereClauseGeneral, this.userWhereConditionGeneral, ref invalidQuery);
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArrayParties, queryingFields.ExciseAffidvtQueryingFields, whereClauseParties, this.userWhereConditionParties, ref invalidQuery);
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArrayParcels, queryingFields.ExciseAffidvtQueryingFields, whereClauseParcels, this.userWhereConditionParcels, ref invalidQuery);
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArrayAffdvt, queryingFields.ExciseAffidvtQueryingFields, whereClauseAffdvt, this.userWhereConditionAffdvt, ref invalidQuery);
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArrayAmtDue, queryingFields.ExciseAffidvtQueryingFields, whereClauseAmtDue, this.userWhereConditionAmtDue, ref invalidQuery);

            if ((!invalidQuery && whereClauseGeneral.Length == 0) && (!invalidQuery && whereClauseParties.Length == 0) && (!invalidQuery && whereClauseParcels.Length == 0) && (!invalidQuery && whereClauseAffdvt.Length == 0) && (!invalidQuery && whereClauseAmtDue.Length == 0))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryEntryMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.StatementIDTextBox.Focus();
                this.Cursor = Cursors.Default;
                return;
            }

            QueryData queryData = new QueryData();

            try
            {
                string wherAffdvt = string.Empty;
                string userWhereAffdvt = string.Empty;

                // Creating where clause
                if ((!invalidQuery && whereClauseGeneral.Length > 0) && (!invalidQuery && whereClauseAffdvt.Length > 0))
                {
                    wherAffdvt = (whereClauseGeneral.ToString() + "AND" + whereClauseAffdvt.ToString());
                }
                else
                {
                    wherAffdvt = (whereClauseGeneral.ToString() + whereClauseAffdvt.ToString());
                }

                whereClause = (wherAffdvt + "|" + whereClauseParties.ToString() + "|" + whereClauseParcels.ToString() + "|" + whereClauseAmtDue.ToString());

                // Creating User Where Clause
                if ((!invalidQuery && this.userWhereConditionGeneral.Length > 0) && (!invalidQuery && this.userWhereConditionAffdvt.Length > 0))
                {
                    userWhereAffdvt = (this.userWhereConditionGeneral.ToString() + "AND" + this.userWhereConditionAffdvt.ToString());
                }
                else
                {
                    userWhereAffdvt = (this.userWhereConditionGeneral.ToString() + this.userWhereConditionAffdvt.ToString());
                }

                userWhereClause = (userWhereAffdvt + "AND" + this.userWhereConditionParties.ToString() + "AND" + this.userWhereConditionParcels.ToString() + "AND" + this.userWhereConditionAmtDue.ToString());

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm) && this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                {
                    queryData = this.F1105Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
                }
                else
                {
                    queryData = this.F1105Control.WorkItem.ExecuteQuery(whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
                }

                if (queryData.ListKeyId.Rows.Count > 0)
                {
                    querySucceeded = true;
                }
                else
                {
                    querySucceeded = false;
                }
            }
            catch (Exception)
            {
                invalidQuery = true;
                querySucceeded = false;
            }
            finally
            {
                if (querySucceeded)
                {
                    if (queryData.SearchedCountResult.Rows.Count > 0)
                    {
                        MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], " records were searched \n", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " Matches the filter criteria"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.WhereCondition = whereClause;
                    this.UserDefinedWhereCondition = userWhereClause;

                    //// this.ClearFilterControl.Enabled = true;
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.FilteredButton.FilterStatus = true;
                    this.LoadExciseTaxAffidavit(queryData.ListKeyId, null);
                    //// this.LoadExciseTaxAffidavit(null,Convert.ToInt32(this.queryByFormDataSet.ListStatementId.Rows[0][this.queryByFormDataSet.ListStatementId.StatementIdColumn].ToString()));
                    this.SetControlsProperty();
                    this.NonQueryFieldsStatus(true);
                }
                else
                {
                    if (invalidQuery)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.StatementIDTextBox.Focus();
                }

                ////sets default cursor
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the name of the querying field.
        /// </summary>
        private void SetQueryingFieldName()
        {
            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            // General
            this.StatementIDTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.StatementIDColumn.ColumnName;
            this.GeneralHeaderPaymentDate.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PaymentDateColumn.ColumnName;
            this.GeneralHeaderFormDate.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.FormDateColumn.ColumnName;
            this.GeneralLinkTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DistrictColumn.ColumnName;
            this.GeneralSubmittedDate.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.SubmittedDateColumn.ColumnName;
            this.GeneralFromWeb.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.FromWEBColumn.ColumnName;
            this.GerneralTotalDebit.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.TotalDebtColumn.ColumnName;
            this.GeneralLocationCode.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.LocationCodeColumn.ColumnName;
            this.GeneralTaxCodeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.TaxCodeColumn.ColumnName;
            this.GeneralMobileHomeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.MobileHomeColumn.ColumnName;
            this.GeneralReceiptNoTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ReceiptNumberColumn.ColumnName;
            this.GeneralNote.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.NoteToDORColumn.ColumnName;

            // Parties
            this.GeneralPartiesNameTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.NameColumn.ColumnName;
            this.PartiesPhoneNoTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PhoneNumberColumn.ColumnName;
            this.PartiesTypeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.IndividualTypeColumn.ColumnName;
            this.PartiesOwnerTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.OwnerColumn.ColumnName;
            this.PartiesAddress1TextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.Address1Column.ColumnName;
            this.PartiesAddress2TextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.Address2Column.ColumnName;
            this.PartiesCityTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.CityColumn.ColumnName;
            this.PartiesStateTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.StateColumn.ColumnName;
            this.PartiesZipCodeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ZipCodeColumn.ColumnName;
            this.PartiesCountryTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.CountryColumn.ColumnName;

            // Parcels
            this.ParcelNumberTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ParcelNumberColumn.ColumnName;
            this.PersonlaPropertyTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PersonalPropColumn.ColumnName;
            this.AssessedValueTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.AssessedValueColumn.ColumnName;
            this.LegalTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.LegalColumn.ColumnName;

            // Affdvt Details
            this.AffidavitPartialSaleTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PartialSaleColumn.ColumnName;
            this.AffidavitSegregatedTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.SegregatedColumn.ColumnName;
            this.AffidavitStreetAddressTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.StreetAddressColumn.ColumnName;
            this.AffidavitStreetLocationTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.LocationOfSaleColumn.ColumnName;
            this.AffDvtLoactionTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.NameOfLocationColumn.ColumnName;
            this.AffdvtUseCodeQueryTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.UseCodeColumn.ColumnName;
            this.AffdvtExemptRegNumberTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ExemptRegNumberColumn.ColumnName;
            this.AffdvtForestTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ForestLandColumn.ColumnName;
            this.AffidvtOpenSpaceTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.OpenSpaceColumn.ColumnName;
            this.AffDvtHistoryTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.HistoricColumn.ColumnName;
            this.AffDvtContinuanceTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ContinuanceColumn.ColumnName;
            this.AffDvtDescTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PersonalPropertyDescriptionColumn.ColumnName;
            this.AffDvtExemptionCodeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ExemptionCodeColumn.ColumnName;
            this.AffDvtExemptionDescrTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ExemptionDescriptionColumn.ColumnName;
            this.AffDvtDocumentTypeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DocumentTypeColumn.ColumnName;
            this.AffDvtDocDateTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DocumentDateColumn.ColumnName;

            // Amount Due
            this.CalcDueSellingPriceTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.GrossSellingPriceColumn.ColumnName;
            this.CalcDuePerPropertTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.PersonalPropertyAmountColumn.ColumnName;
            this.CalDueRealPropTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.RealPropertyExemptAmountColumn.ColumnName;
            this.CalcDueTaxableSaleTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.TaxableSalePriceColumn.ColumnName;
            this.CalcDueExciseTaxTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ExciseTaxStateColumn.ColumnName;
            this.CalcDueExcisTaxLocaltextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.ExciseTaxLocalColumn.ColumnName;
            this.CalcDueDelinqIntStateTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DelinquentInterestStateColumn.ColumnName;
            this.CalcDueDelinqIntLocalTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DelinquentInterestLocalColumn.ColumnName;
            this.CalcDueDelinqPenaltyTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.DelinquentPenaltyColumn.ColumnName;
            this.CalcDueTransFeeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.TechnologyFeeColumn.ColumnName;
            this.CalcDueTechFeeTextBox.QueryingFileldName = queryingFields.ExciseAffidvtQueryingFields.TransactionFeeColumn.ColumnName;
        }

        /// <summary>
        /// Sets the controls property.
        /// </summary>
        private void SetControlsProperty()
        {
            if (this.PageStatus.Equals(TerraScanCommon.PageStatus.NormalMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
            {
                this.StatementIDTextBox.ReadOnly = true;
                this.StatementIDTextBox.Enabled = false;

                this.ReceiptFormButton.Enabled = true;
                this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;

                ////reset Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayGeneral, false, null, Color.White);
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayAffdvt, false, null, Color.White);
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayParties, false, null, Color.White);
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayParcels, false, null, Color.White);
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayAmtDue, false, null, Color.White);
                this.parcelPictureBox.BackColor = Color.White;

                this.GeneralHeaderPanel.BackColor = Color.White;
                this.PartiesPanel.BackColor = Color.White;
                this.ParcelHeaderPanel.BackColor = Color.White;
                this.AffDvtPanel.BackColor = Color.White;
                this.AmountDuePanel.BackColor = Color.White;

                this.GeneralLinkLabel.Visible = true;
                this.GeneralTaxCode.Visible = true;
                this.GeneralMobileHome.Visible = true;
                this.GeneralReceiptNo.Visible = true;
                this.PartiesTypeComboBox.Visible = true;
                this.PersonlaPropertyComboBox.Visible = true;
                this.AffidavitPartialSaleCombo.Visible = true;
                this.AffidavitSegregatedComboBox.Visible = true;
                this.AffidavitStreetLocationCombo.Visible = true;
                this.AffdvtForestCombo.Visible = true;
                this.AffidvtOpenSpaceComb.Visible = true;
                this.AffDvtHistoryCombo.Visible = true;
                this.AffDvtContinuanceCombo.Visible = true;
                this.AffdvtUseCodeTextBox.Visible = true;

                this.GeneralLinkTextBox.Visible = false;
                this.GeneralTaxCodeTextBox.Visible = false;
                this.GeneralMobileHomeTextBox.Visible = false;
                this.GeneralReceiptNoTextBox.Visible = false;
                this.PartiesTypeTextBox.Visible = false;
                this.PersonlaPropertyTextBox.Visible = false;
                this.AffidavitPartialSaleTextBox.Visible = false;
                this.AffidavitSegregatedTextBox.Visible = false;
                this.AffidavitStreetLocationTextBox.Visible = false;
                this.AffdvtForestTextBox.Visible = false;
                this.AffidvtOpenSpaceTextBox.Visible = false;
                this.AffDvtHistoryTextBox.Visible = false;
                this.AffDvtContinuanceTextBox.Visible = false;
                this.AffdvtUseCodeQueryTextBox.Visible = false;
                //// Added By Guhan on 04-Jan-07
                this.CalcDueTaxableSaleTextBox.Enabled = false;
                //// Till Here
                this.GeneralPaymentDatePict.Visible = true;
                this.GeneralFormDatePic.Visible = true;
                this.DistrictPictureBox.Visible = true;
                this.AffDvtDatePicture.Visible = true;
                this.PictureParcel.Visible = true;
                this.parcelPictureBox.Visible = true;

                this.additionalOperationSmartPart.Enabled = true;
                this.RecordNavigatorDeckWorkspace.Enabled = true;

                ////this.GeneralHeaderPaymentDate.ValidateType = TerraScanTextBox.ControlvalidationType.Date;
                ////this.GeneralHeaderFormDate.ValidateType = TerraScanTextBox.ControlvalidationType.Date;
                ////this.AffDvtDocDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Date;
                ////this.GerneralTotalDebit.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueSellingPriceTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDuePerPropertTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalDueRealPropTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueTaxableSaleTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueExciseTaxTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueExcisTaxLocaltextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueDelinqIntStateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueDelinqIntLocalTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueDelinqPenaltyTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueTransFeeTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.CalcDueTechFeeTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                ////this.AssessedValueTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
            }
            else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.SetAffDvtControls(true);
                this.SetEnableStatusforPartiesControls(true);
                this.SetEnableSatustParcelHeader(true);
                this.StatementIDTextBox.Enabled = true;
                this.StatementIDTextBox.ReadOnly = false;
                this.GeneralSubmittedDate.Enabled = true;
                this.GeneralFromWeb.Enabled = true;
                this.CalcDueTaxableSaleTextBox.Enabled = true;
                this.DisableAllControls();

                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayGeneral, true, this.UserDefinedWhereCondition.ToString(), Color.FromArgb(204, 255, 204));
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayAffdvt, true, this.UserDefinedWhereCondition.ToString(), Color.FromArgb(204, 255, 204));
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayParties, true, this.UserDefinedWhereCondition.ToString(), Color.FromArgb(204, 255, 204));
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayParcels, true, this.UserDefinedWhereCondition.ToString(), Color.FromArgb(204, 255, 204));
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArrayAmtDue, true, this.UserDefinedWhereCondition.ToString(), Color.FromArgb(204, 255, 204));

                this.GeneralHeaderPanel.BackColor = Color.FromArgb(204, 255, 204);
                this.PartiesPanel.BackColor = Color.FromArgb(204, 255, 204);
                this.ParcelHeaderPanel.BackColor = Color.FromArgb(204, 255, 204);
                this.AffDvtPanel.BackColor = Color.FromArgb(204, 255, 204);
                this.AmountDuePanel.BackColor = Color.FromArgb(204, 255, 204);

                this.GeneralLinkLabel.Visible = false;
                this.GeneralTaxCode.Visible = false;
                this.GeneralMobileHome.Visible = false;
                this.GeneralReceiptNo.Visible = false;
                this.PartiesTypeComboBox.Visible = false;
                this.PersonlaPropertyComboBox.Visible = false;
                this.AffidavitPartialSaleCombo.Visible = false;
                this.AffidavitSegregatedComboBox.Visible = false;
                this.AffidavitStreetLocationCombo.Visible = false;
                this.AffdvtForestCombo.Visible = false;
                this.AffidvtOpenSpaceComb.Visible = false;
                this.AffDvtHistoryCombo.Visible = false;
                this.AffDvtContinuanceCombo.Visible = false;
                this.AffdvtUseCodeTextBox.Visible = false;

                this.GeneralLinkTextBox.Visible = true;
                this.GeneralTaxCodeTextBox.Visible = true;
                this.GeneralMobileHomeTextBox.Visible = true;
                this.GeneralReceiptNoTextBox.Visible = true;
                this.PartiesTypeTextBox.Visible = true;
                this.PersonlaPropertyTextBox.Visible = true;
                this.AffidavitPartialSaleTextBox.Visible = true;
                this.AffidavitSegregatedTextBox.Visible = true;
                this.AffidavitStreetLocationTextBox.Visible = true;
                this.AffdvtForestTextBox.Visible = true;
                this.AffidvtOpenSpaceTextBox.Visible = true;
                this.AffDvtHistoryTextBox.Visible = true;
                this.AffDvtContinuanceTextBox.Visible = true;
                this.AffdvtUseCodeQueryTextBox.Visible = true;

                this.GeneralPaymentDatePict.Visible = false;
                this.GeneralFormDatePic.Visible = false;
                this.DistrictPictureBox.Visible = false;
                this.AffDvtDatePicture.Visible = false;
                this.PictureParcel.Visible = false;
                this.parcelPictureBox.Visible = false;

                ////this.GeneralHeaderPaymentDate.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.GeneralHeaderFormDate.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.AffDvtDocDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.GerneralTotalDebit.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueSellingPriceTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDuePerPropertTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalDueRealPropTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueTaxableSaleTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueExciseTaxTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueExcisTaxLocaltextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueDelinqIntStateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueDelinqIntLocalTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueDelinqPenaltyTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueTransFeeTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.CalcDueTechFeeTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                ////this.AssessedValueTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
            }

            ////Added by Jayanhti
            this.PaymentDatePanel.BackColor = this.GeneralHeaderPanel.BackColor;
            this.GeneralLinkLablePanel.BackColor = this.GeneralHeaderPanel.BackColor;
            this.AffDvtDatePanle.BackColor = this.AffDvtPanel.BackColor;
            this.TaxableSalePanel.BackColor = this.AmountDuePanel.BackColor;
            ////Till here
        }

        /// <summary>
        /// Disables all controls.
        /// </summary>
        private void DisableAllControls()
        {
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            this.additionalOperationSmartPart.Enabled = false;
            this.RecordNavigatorDeckWorkspace.Enabled = false;
            this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);

            this.NewAffdvtButton.Enabled = false;
            this.SaveAffdvtButton.Enabled = false;
            this.CancelAffdvtButton.Enabled = false;
            this.DeleteAffdvtButton.Enabled = false;
            this.ReceiptFormButton.Enabled = false;
            this.NewPartiesButton.Enabled = false;
            this.UpdateParites.Enabled = false;
            this.RemovePartiesButton.Enabled = false;
            this.CancelPartiesButton.Enabled = false;
            this.PartiesDataGridView.Enabled = false;
            this.TreasurerStatusButton.Enabled = false;
            this.ExciseRatesButton.Enabled = false;
            this.ViewAfdvtButton.Enabled = false;
            this.AssessorStatusButton.Enabled = false;
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
            this.ParcelHeaderDataGridView.Enabled = false;
            this.CalcuDueCommandButton.Enabled = false;
            this.FilteredButton.Enabled = false;
            this.footerSmartPart.KeyId = null; 
            //this.ExciseAffidavitAuditLink.Text = "tTR_Statement[StatementID] ";
            //this.ExciseAffidavitAuditLink.Enabled = false;

            /* this.SetActiveRecord(this, new DataEventArgs<int>(0));
            // this.SetRecordCount(this, new DataEventArgs<int>(0));
            // if (this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count == 0)
            // {
            //     this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { 0, 0 }));
            // }*/
        }

        /// <summary>
        /// Clears all fields.
        /// </summary>
        private void ClearAllFields()
        {
            #region Affidavit

            this.StatementIDTextBox.Text = string.Empty;
            this.GeneralHeaderPaymentDate.Text = string.Empty;
            this.GeneralHeaderFormDate.Text = string.Empty;
            this.GeneralLinkLabel.Text = string.Empty;
            this.GeneralLinkTextBox.Text = string.Empty;
            this.GeneralSubmittedDate.Text = string.Empty;
            this.GeneralFromWeb.Text = string.Empty;
            this.GerneralTotalDebit.Text = string.Empty;
            this.GeneralLocationCode.Text = string.Empty;
            this.GeneralTaxCode.Text = string.Empty;
            this.GeneralTaxCodeTextBox.Text = string.Empty;
            this.GeneralTaxCodeTextBox.Text = string.Empty;
            this.GeneralMobileHome.Text = string.Empty;
            this.GeneralMobileHomeTextBox.Text = string.Empty;
            this.GeneralReceiptNo.Text = string.Empty;
            this.GeneralReceiptNoTextBox.Text = string.Empty;
            this.GeneralNote.Text = string.Empty;

            this.AffidavitPartialSaleCombo.Text = string.Empty;
            this.AffidavitPartialSaleTextBox.Text = string.Empty;
            this.AffidavitSegregatedComboBox.Text = string.Empty;
            this.AffidavitSegregatedTextBox.Text = string.Empty;
            this.AffidavitStreetAddressTextBox.Text = string.Empty;
            this.AffidavitStreetLocationCombo.Text = string.Empty;
            this.AffidavitStreetLocationTextBox.Text = string.Empty;
            this.AffDvtLoactionTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtExemptRegNumberTextBox.Text = string.Empty;
            this.AffdvtForestCombo.Text = string.Empty;
            this.AffdvtForestTextBox.Text = string.Empty;
            this.AffidvtOpenSpaceComb.Text = string.Empty;
            this.AffidvtOpenSpaceTextBox.Text = string.Empty;
            this.AffDvtHistoryCombo.Text = string.Empty;
            this.AffDvtHistoryTextBox.Text = string.Empty;
            this.AffDvtContinuanceCombo.Text = string.Empty;
            this.AffDvtContinuanceTextBox.Text = string.Empty;
            this.AffDvtDescTextBox.Text = string.Empty;
            this.AffDvtExemptionCodeTextBox.Text = string.Empty;
            this.AffDvtExemptionDescrTextBox.Text = string.Empty;
            this.AffDvtDocumentTypeTextBox.Text = string.Empty;
            this.AffDvtDocDateTextBox.Text = string.Empty;
            this.AffdvtUseCodeQueryTextBox.Text = string.Empty;
            this.GerneralTotalDebit.Text = string.Empty;

            #endregion

            #region Parties

            this.GeneralPartiesNameTextBox.Text = string.Empty;
            this.PartiesPhoneNoTextBox.Text = string.Empty;
            this.PartiesTypeComboBox.Text = string.Empty;
            this.PartiesTypeTextBox.Text = string.Empty;
            this.PartiesTypeTextBox.Text = string.Empty;
            this.PartiesOwnerTextBox.Text = string.Empty;
            this.PartiesAddress1TextBox.Text = string.Empty;
            this.PartiesAddress2TextBox.Text = string.Empty;
            this.PartiesCityTextBox.Text = string.Empty;
            this.PartiesStateTextBox.Text = string.Empty;
            this.PartiesZipCodeTextBox.Text = string.Empty;
            this.PartiesCountryTextBox.Text = string.Empty;

            #endregion

            #region Parcels

            this.ParcelNumberTextBox.Text = string.Empty;
            this.PersonlaPropertyComboBox.Text = string.Empty;
            this.PersonlaPropertyTextBox.Text = string.Empty;
            this.AssessedValueTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;

            #endregion

            #region Amount Due

            this.CalcDueSellingPriceTextBox.Text = string.Empty;
            this.CalcDuePerPropertTextBox.Text = string.Empty;
            this.CalDueRealPropTextBox.Text = string.Empty;
            this.CalcDueTaxableSaleTextBox.Text = string.Empty;
            this.CalcDueExciseTaxTextBox.Text = string.Empty;
            this.CalcDueExcisTaxLocaltextBox.Text = string.Empty;
            this.CalcDueDelinqIntStateTextBox.Text = string.Empty;
            this.CalcDueDelinqIntLocalTextBox.Text = string.Empty;
            this.CalcDueDelinqPenaltyTextBox.Text = string.Empty;
            this.CalcDueTransFeeTextBox.Text = string.Empty;
            this.CalcDueTechFeeTextBox.Text = string.Empty;

            #endregion

            #region Suppliemtn
            this.ClearSupplimentControl();
            #endregion
        }

        /// <summary>
        /// set focus to the next/previous input field  
        /// </summary>
        /// <param name="sourceControl">control to start the search</param>
        /// <param name="key">The Key.</param>
        /// <returns>if true retrieve next control ,else retrieve previous control</returns>
        private Control GetSmartPartControl(Control sourceControl, string key)
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
                        requiredControl = this.GetSmartPartControl(sampControl, key);
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
        /// Gets the statement ID.
        /// </summary>
        private void GetStatementID()
        {
            ////if (this.currentAffidavitStatementId != null)
            ////{
            DataTable tempDataTable = this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Copy();
            if (tempDataTable != null)
            {
                this.affDvtRecordCount = tempDataTable.Rows.Count;
                if (tempDataTable.Rows.Count > 0)
                {
                    this.statementExist = true;
                }
                else
                {
                    this.statementExist = false;
                }
            }
            else
            {
                this.affDvtRecordCount = 0;
                this.statementExist = false;
            }

            DataView tempDataView = new DataView(tempDataTable);
            tempDataView.RowFilter = string.Concat(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.KeyIDColumn.ColumnName, " = ", this.currentAffidavitStatementId);
            if (tempDataView.Count > 0)
            {
                return;
            }
            //// }

            this.currentAffidavitStatementId = Convert.ToInt32(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows[0][this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.KeyIDColumn].ToString());
        }

        #endregion

        #region Query

        /// <summary>
        /// Queries the by form function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void QueryByFormFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                e.Data.Focus();
                this.currentAffidavitStatementId = 0;

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    this.FilterRecordSet();
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.ClearFilterControl.Enabled = true;

                    if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                    {
                        this.PageStatus = TerraScanCommon.PageStatus.FilteredQueryByForm;
                    }
                    else
                    {
                        this.FilterTypeId = 0;
                        this.PageStatus = TerraScanCommon.PageStatus.QueryByForm;
                    }

                    this.ClearNonQueryFields();
                    this.NonQueryFieldsStatus(false);
                    this.FilteredButton.FilterStatus = false;
                    this.SetControlsProperty();
                    this.StatementIDTextBox.Focus();
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

        #endregion

        #region ReQuery

        /// <summary>
        /// Handles the Click event of the FilteredButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilteredButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentAffidavitStatementId = 0;
                this.Cursor = Cursors.WaitCursor;
                Form querystringForm = new Form();

                // if (!this.CheckPageStatus(false))
                // {
                //     return;
                // }

                if (!this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                {
                    this.CurrentSnapshotName = "N/A";
                    this.CurrentSnapshotDescription = "N/A";
                }

                F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();
                string requeryWhereCondition = this.WhereCondition;

                object[] optionalParameter = new object[] { requeryWhereCondition, this.CurrentSnapshotName, this.CurrentSnapshotDescription, queryingFields.ExciseAffidvtQueryingFields };
                querystringForm = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9052, optionalParameter, this.F1105Control.WorkItem);
                this.Cursor = Cursors.Default;

                while (true)
                {
                    try
                    {
                        if (querystringForm != null && querystringForm.ShowDialog() == DialogResult.Yes)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            QueryData queryData = new QueryData();
                            bool invalidQuery = false;
                            if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                            {
                                queryData = this.F1105Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, TerraScanCommon.GetValue(querystringForm, "CurrentQueryWhereCondition"), null, int.Parse(this.Tag.ToString()));
                            }
                            else
                            {
                                queryData = this.F1105Control.WorkItem.ExecuteQuery(TerraScanCommon.GetValue(querystringForm, "CurrentQueryWhereCondition"), null, int.Parse(this.Tag.ToString()));
                            }

                            if (queryData.ListKeyId.Rows.Count > 0)
                            {
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], " records were searched \n", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " Matches the filter criteria"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                this.WhereCondition = TerraScanCommon.GetValue(querystringForm, "CurrentQueryWhereCondition");
                                this.UserDefinedWhereCondition = TerraScanCommon.GetValue(querystringForm, "UserDefinedWhereCondition");
                                this.ClearFilterControl.Enabled = true;

                                ////Set page status
                                this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                                this.FilteredButton.FilterStatus = true;

                                //// FillStatementDetails function is used to fill the Statement details in ExciseTaxstatement                                
                                this.LoadExciseTaxAffidavit(queryData.ListKeyId, null);
                                this.SetControlsProperty();
                                break;
                            }
                            else
                            {
                                if (!invalidQuery)
                                {
                                    // MessageForm.ShowMessage(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion

        #region Query Utility

        /// <summary>
        /// Queries the utility function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void QueryUtilityFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.currentAffidavitStatementId = 0;
                this.Cursor = Cursors.WaitCursor;

                // object[] optionalParameter = new object[] { int.Parse(this.Tag.ToString()), this.WhereCondition, this.UserDefinedWhereCondition };

                object[] optionalParameter = null;
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    optionalParameter = new object[] { int.Parse(this.Tag.ToString()), this.WhereCondition, this.UserDefinedWhereCondition };
                }
                else
                {
                    optionalParameter = new object[] { int.Parse(this.Tag.ToString()), string.Empty, string.Empty };
                }

                Form queryUtility = new Form();
                queryUtility = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9050, optionalParameter, this.F1105Control.WorkItem);
                if (queryUtility != null)
                {
                    if (queryUtility.ShowDialog() == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        QueryData queryData = new QueryData();
                        if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                        {
                            ////specific for snapshot - filter
                            this.UserDefinedWhereCondition = TerraScanCommon.GetValue(queryUtility, "UserWhereCondition");
                            this.WhereCondition = TerraScanCommon.GetValue(queryUtility, "WhereCondition");

                            // Load tempDataSet will contain query and where condition in Table[1] and all filtered statementIds in Tables[0]
                            queryData = this.F1105Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.WhereCondition, null, int.Parse(this.Tag.ToString()));
                        }
                        else
                        {
                            ////specific for filtered records
                            this.FilterTypeId = Convert.ToInt32(TerraScanCommon.GetValue(queryUtility, "QueryId"));

                            // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                            queryData = this.F1105Control.WorkItem.GetQueryResult(this.FilterTypeId, null);
                        }

                        if (queryData.ListKeyId.Rows.Count > 0)
                        {
                            ////specific to snapshot filter
                            if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                            {
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], " records were searched \n", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " Matches the filter criteria"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                ////specific for query filtered records
                                this.FormFilterType = TerraScanCommon.FilterType.Query;
                                this.UserDefinedWhereCondition = queryData.GetQueryResult.Rows[0][queryData.GetQueryResult.WhereCondnColumn].ToString();
                                this.WhereCondition = queryData.GetQueryResult.Rows[0][queryData.GetQueryResult.WhereCondnSqlColumn].ToString();
                            }

                            this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                            this.LoadExciseTaxAffidavit(queryData.ListKeyId, null);
                            this.FilteredButton.FilterStatus = true;
                            this.ClearFilterControl.Enabled = true;
                            this.SetControlsProperty();
                        }
                        else
                        {
                            if (!this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                            {
                                ////specific for filtered records 
                                this.FilterTypeId = 0;
                            }

                            MessageBox.Show(SharedFunctions.GetResourceString("NoRecord"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        #endregion

        #region Snapshot utility

        /// <summary>
        /// Snapshots the utility function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SnapshotUtilityFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.currentAffidavitStatementId = 0;
                this.Cursor = Cursors.WaitCursor;
                string snapshotIdsXml = string.Empty;
                int snapshotIdsCount = 0;

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    snapshotIdsXml = tempDataSet.GetXml();
                    snapshotIdsCount = this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count;
                }

                this.Cursor = Cursors.Default;
                object[] optionalParameter = new object[] { int.Parse(this.Tag.ToString()), snapshotIdsXml, snapshotIdsCount };
                Form snapshot = new Form();
                snapshot = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9051, optionalParameter, this.F1105Control.WorkItem);
                this.Cursor = Cursors.Default;
                if (snapshot != null)
                {
                    if (snapshot.ShowDialog() == DialogResult.Yes)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ////specific for filtered records
                        this.FilterTypeId = Convert.ToInt32(TerraScanCommon.GetValue(snapshot, "SnapShotId"));
                        this.CurrentSnapshotName = TerraScanCommon.GetValue(snapshot, "SnapshotName");
                        this.CurrentSnapshotDescription = TerraScanCommon.GetValue(snapshot, "SnapshotDescription");

                        // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                        QueryData queryData = this.F1105Control.WorkItem.GetSnapShotResult(this.FilterTypeId, null);

                        if (queryData.ListKeyId.Rows.Count > 0)
                        {
                            if (queryData.SearchedCountResult.Rows.Count > 0 && !int.Equals(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn]))
                            {
                                MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " of ", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], "\nrecords found"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SnapshotMismatch")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            ////specific for filtered records
                            this.FormFilterType = TerraScanCommon.FilterType.SnapShot;
                            this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;

                            ////FillExciseTaxStatementFormDetails function is used to fill the Statement details in Excise Tax Statement 
                            this.LoadExciseTaxAffidavit(queryData.ListKeyId, null);
                            this.FilteredButton.FilterStatus = true;
                            this.ClearFilterControl.Enabled = true;

                            this.UserDefinedWhereCondition = String.Empty;
                            this.WhereCondition = String.Empty;
                            this.SetControlsProperty();
                        }
                        else
                        {
                            ////specific for filtered records  
                            this.FilterTypeId = 0;
                            MessageBox.Show(SharedFunctions.GetResourceString("NoRecord"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
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

        #endregion

        #region ClearFilter

        /// <summary>
        /// Handles the Click event of the ClearFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearFilterFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////set necessary controls property
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm))
                {
                    this.currentAffidavitStatementId = 0;
                    this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
                    this.FormFilterType = TerraScanCommon.FilterType.None;
                    this.FilterTypeId = 0;
                    if (this.PermissionFiled.editPermission)
                    {
                        this.AssessorStatusButton.Enabled = true;
                        this.TreasurerStatusButton.Enabled = true;
                    }

                    this.ClearFilterControl.Enabled = false;
                    this.FilteredButton.FilterStatus = false;
                    this.UserDefinedWhereCondition = String.Empty;
                    this.WhereCondition = string.Empty;
                }
                else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    //// FillStatementDetails function is used to fill the Statement details in excise tax statement                         

                    ////this.GetStatementId();
                    this.FilteredButton.FilterStatus = true;
                    this.ClearFilterControl.Enabled = true;
                }

                this.SetControlsProperty();
                this.NonQueryFieldsStatus(true);
                this.LoadExciseTaxAffidavit(null, null);
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

        #endregion

        #region ExciseTaxAffidavit

        #region Coding For GeneralHeader

        #region method

        /// <summary>
        /// Fills the general header.
        /// </summary>
        /// <param name="dataSetRowNO">The data set row NO.</param>
        private void FillGeneralHeaderText(int dataSetRowNO)
        {
            try
            {
                if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.StatementIDTextBox.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.StatementIDColumn].ToString();
                    this.GeneralHeaderPaymentDate.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.PaymentDateColumn].ToString();
                    this.GeneralHeaderFormDate.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.FormDateColumn].ToString();
                    this.GeneralLinkLabel.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DistrictColumn].ToString();
                    if (!string.IsNullOrEmpty(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn].ToString()))
                    {
                        this.GeneralSubmittedDate.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn].ToString();
                    }
                    else
                    {
                        this.GeneralSubmittedDate.Text = "N/A";
                    }

                    if (string.Compare(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn].ToString().ToUpperInvariant(), "TRUE") == 0)
                    {
                        this.GeneralFromWeb.Text = SharedFunctions.GetResourceString("YESValue");
                    }
                    else
                    {
                        this.GeneralFromWeb.Text = SharedFunctions.GetResourceString("NOValue");
                    }

                    this.GerneralTotalDebit.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.TotalDebtColumn].ToString();
                    this.GeneralLocationCode.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.LocationCodeColumn].ToString();
                    SetComboboxValue(this.GeneralTaxCode, this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExemptColumn].ToString());
                    if (string.Compare(this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsExemptColumn].ToString().ToUpperInvariant(), "FALSE") == 0)
                    {
                        this.taxCode = SharedFunctions.GetResourceString("TAXABLEValue");
                    }
                    else
                    {
                        this.taxCode = SharedFunctions.GetResourceString("EXEMPTValue");
                    }

                    SetComboboxValue(this.GeneralMobileHome, this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn].ToString());
                    this.GeneralReceiptNo.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ReceiptNumberColumn].ToString();
                    if (!String.IsNullOrEmpty(this.GeneralReceiptNo.Text.Trim()))
                    {
                        this.receiptIDExist = true;
                    }
                    else
                    {
                        this.receiptIDExist = false;
                    }

                    this.GeneralNote.Text = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.DORNoteColumn].ToString();
                    this.exciseRateId = this.exciseTaxAffidavitDataSet.General.Rows[dataSetRowNO][this.exciseTaxAffidavitDataSet.General.ExciseRateIDColumn].ToString();
                }
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the general combo box.
        /// </summary>
        private void SetGeneralComboBox()
        {
            this.GeneralTaxCode.Items.Clear();
            this.GeneralMobileHome.Items.Clear();
            this.GeneralTaxCode.Items.Insert(0, SharedFunctions.GetResourceString("TAXABLEValue"));
            this.GeneralTaxCode.Items.Insert(1, SharedFunctions.GetResourceString("EXEMPTValue"));
            this.GeneralMobileHome.Items.Insert(0, SharedFunctions.GetResourceString("NOValue"));
            this.GeneralMobileHome.Items.Insert(1, SharedFunctions.GetResourceString("YESValue"));
        }

        /// <summary>
        /// Sets the general header field new mode.
        /// </summary>
        private void SetGeneralHeaderFieldNewMode()
        {
            this.StatementIDTextBox.Text = string.Empty;
            this.GeneralHeaderPaymentDate.Text = DateTime.Now.ToString(this.dateFormat);
            this.GeneralHeaderFormDate.Text = DateTime.Now.ToString(this.dateFormat);
            this.GeneralSubmittedDate.Text = "N/A";
            this.GeneralFromWeb.Text = string.Empty;
            this.GerneralTotalDebit.Text = string.Empty;
            this.GeneralLocationCode.Text = string.Empty;
            this.GeneralTaxCode.SelectedIndex = 0;
            this.GeneralMobileHome.SelectedIndex = 0;
            this.GeneralReceiptNo.Text = string.Empty;
            this.GeneralNote.Text = string.Empty;
            this.GeneralLinkLabel.Text = string.Empty;
            SetComboboxValue(this.AffidavitStreetLocationCombo, "COUNTY");
            this.AffdvtForestCombo.SelectedIndex = 0;
            this.PersonlaPropertyComboBox.SelectedIndex = 1;
            this.taxCode = this.GeneralTaxCode.Text.Trim();
            this.exciseRateId = "0";
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the DescriptionPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionPanel_Click(object sender, EventArgs e)
        {
            this.GeneralNote.Focus();
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Handles the LinkClicked event of the GeneralReceiptNo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GeneralReceiptNo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                ////formInfo.optionalParameters = new object[1];
                ////formInfo.optionalParameters[0] = this.GeneralReceiptNo.Text.Trim();
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
        /// Handles the LinkClicked event of the GeneralLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GeneralLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(9500);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.districtId;
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
        /// Handles the Click event of the GeneralPaymentDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralPaymentDatePict_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GeneralHeaderPaymentDate.Text.Trim()))
            {
                try
                {
                    this.PaymentDateCalender.SetDate(Convert.ToDateTime(this.GeneralHeaderPaymentDate.Text.Trim()));
                    ShowCalender(this.PaymentDateCalender, this.PaymentDatePanel, this.GeneralPaymentDatePict, this.GeneralHeaderPanel);
                }
                catch
                {
                    this.PaymentDateCalender.Visible = false;
                    this.GeneralHeaderPaymentDate.Text = DateTime.Now.ToString(this.dateFormat);
                }
            }
            else
            {
                ShowCalender(this.PaymentDateCalender, this.PaymentDatePanel, this.GeneralPaymentDatePict, this.GeneralHeaderPanel);
            }
        }

        /// <summary>
        /// Handles the Click event of the GeneralFormDatePic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralFormDatePic_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GeneralHeaderFormDate.Text.Trim()))
            {
                try
                {
                    this.FormDateCalender.SetDate(Convert.ToDateTime(this.GeneralHeaderFormDate.Text.Trim()));
                    ShowCalender(this.FormDateCalender, this.FromDatePanel, this.GeneralFormDatePic, this.GeneralHeaderPanel);
                }
                catch
                {
                    this.FormDateCalender.Visible = false;
                    this.GeneralHeaderFormDate.Text = DateTime.Now.ToString(this.dateFormat);
                }
            }
            else
            {
                ShowCalender(this.FormDateCalender, this.FromDatePanel, this.GeneralFormDatePic, this.GeneralHeaderPanel);
            }
        }

        #endregion

        #endregion

        #region Coding For Parties

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PartiesTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartiesTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SetPartieHeaderToUpdateMode();
        }

        /// <summary>
        /// Disables the parties header.
        /// </summary>
        /// <param name="statusOfPartiesCntrl">if set to <c>true</c> [status of parties CNTRL].</param>
        private void SetEnableStatusforPartiesControls(bool statusOfPartiesCntrl)
        {
            this.GeneralPartiesNameTextBox.Enabled = statusOfPartiesCntrl;
            this.GeneralPartiesNameTextBox.BackColor = System.Drawing.Color.White;
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
            this.PictureParcel.Enabled = statusOfPartiesCntrl;
        }

        /// <summary>
        /// Customises the user management grid.
        /// </summary>
        private void CustomisePartiesDataGridView()
        {
            try
            {
                DataGridViewColumnCollection columns = this.PartiesDataGridView.Columns;
                columns["PartyName"].DataPropertyName = "Name";
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

                columns["PartyName"].DisplayIndex = 0;
                columns["Address"].DisplayIndex = 1;
                columns["City"].DisplayIndex = 2;
                columns["IndividualType"].DisplayIndex = 3;
                columns["Phone"].DisplayIndex = 4;
                columns["PercentOwner"].DisplayIndex = 5;
                columns["State"].DisplayIndex = 6;
                columns["Zip"].DisplayIndex = 7;
                columns["Country"].DisplayIndex = 8;
                columns["individualID"].DisplayIndex = 9;
                columns["individualTypeId"].DisplayIndex = 10;
                columns["StatementId"].DisplayIndex = 12;
                columns["Address1"].DisplayIndex = 13;
                columns["Address2"].DisplayIndex = 14;
                columns["StatementId"].DisplayIndex = 15;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the parties text.
        /// </summary>
        /// <param name="partiesRowId">The parties row id.</param>
        private void SetPartiesText(int partiesRowId)
        {
            if (this.validDataSet && partiesRowId >= 0)
            {
                /*this.GeneralPartiesNameTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["PartyName"].ToString();
                this.PartiesPhoneNoTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["Phone"].ToString();
                //this.SetComboboxValue(this.PartiesTypeComboBox, this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["IndividualType"].ToString());
                //this.PartiesOwnerTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["PercentOwner"].ToString();
                //this.PartiesAddress1TextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["Address"].ToString();
                //this.PartiesAddress2TextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["Address"].ToString();
                //this.PartiesCityTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["City"].ToString();
                //this.PartiesStateTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["State"].ToString();
                //this.PartiesZipCodeTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["Zip"].ToString();
                //this.PartiesCountryTextBox.Text = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId]["Country"].ToString();  */
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.GeneralPartiesNameTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["PartyName"].Value.ToString();
                    this.PartiesPhoneNoTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["Phone"].Value.ToString();
                    SetComboboxValue(this.PartiesTypeComboBox, this.PartiesDataGridView.Rows[partiesRowId].Cells["IndividualType"].Value.ToString());
                    this.PartiesOwnerTextBox.Text = this.PartiesDataGridView.Rows[partiesRowId].Cells["PercentOwner"].Value.ToString();
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
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                /*
                 //string stringExp;

                 //if (this.partiesRowCount > 0)
                 //{
                 //    DataRow[] userInRows;
                 //    stringExp = "IndividualId =" + partiesKeyId;
                 //    userInRows = this.exciseTaxAffidavitDataSet.PartiesHeader.Select(stringExp);

                 //    this.GeneralPartiesNameTextBox.Text = userInRows[0]["PartyName"].ToString();
                 //    this.PartiesPhoneNoTextBox.Text = userInRows[0]["Phone"].ToString();
                 //    this.SetComboboxValue(this.PartiesTypeComboBox, userInRows[0]["IndividualType"].ToString());
                 //    this.PartiesOwnerTextBox.Text = userInRows[0]["PercentOwner"].ToString();
                 //    this.PartiesAddress1TextBox.Text = userInRows[0]["Address"].ToString();
                 //    this.PartiesAddress2TextBox.Text = userInRows[0]["Address"].ToString();
                 //    this.PartiesCityTextBox.Text = userInRows[0]["City"].ToString();
                 //    this.PartiesStateTextBox.Text = userInRows[0]["State"].ToString();
                 //    this.PartiesZipCodeTextBox.Text = userInRows[0]["Zip"].ToString();
                 //    this.PartiesCountryTextBox.Text = userInRows[0]["Country"].ToString();
                 //}
                 //else
                 //{

                 //    this.GeneralPartiesNameTextBox.Text = string.Empty;
                 //    this.PartiesPhoneNoTextBox.Text = string.Empty;
                 //    this.PartiesTypeComboBox.SelectedIndex = 0;
                 //    //// this.SetComboboxValue(this.PartiesTypeComboBox, userInRows[0]["IndividualType"].ToString());
                 //    this.PartiesOwnerTextBox.Text = string.Empty;
                 //    this.PartiesAddress1TextBox.Text = string.Empty;
                 //    this.PartiesAddress2TextBox.Text = string.Empty;
                 //    this.PartiesCityTextBox.Text = string.Empty;
                 //    this.PartiesStateTextBox.Text = string.Empty;
                 //    this.PartiesZipCodeTextBox.Text = string.Empty;
                 //    this.PartiesCountryTextBox.Text = string.Empty;
                 //}
                 */
            }
        }

        /// <summary>
        /// Inits the individual type combo box.
        /// </summary>
        private void InitIndividualTypeComboBox()
        {
            this.PartiesTypeComboBox.DataSource = this.exciseIndividualtype._ExciseIndividualType;
            this.PartiesTypeComboBox.ValueMember = this.exciseIndividualtype._ExciseIndividualType.IndividualTypeIDColumn.ColumnName; ///// "IndividualTypeID";
            this.PartiesTypeComboBox.DisplayMember = this.exciseIndividualtype._ExciseIndividualType.IndividualTypeColumn.ColumnName;   /////"IndividualType";
        }

        /// <summary>
        /// Updates the local parties data set.
        /// </summary>
        /// <param name="partiesRowId">The parties row id.</param>
        private void UpdateLocalPartiesDataSet(int partiesRowId)
        {
            ///// if its NewOpeation Then Save 

            if (this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.CreateNewPartiesRow();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    this.partySave = true;
                }
                catch (Exception e1)
                {
                    this.partySave = false;
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            else
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.NameColumn] = this.GeneralPartiesNameTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.PhoneColumn] = this.PartiesPhoneNoTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeColumn] = this.PartiesTypeComboBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.IndividualTypeIDColumn] = this.PartiesTypeComboBox.SelectedValue;
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.OwnerIDColumn.ColumnName] = this.ownerId;
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.PercentOwnerColumn] = this.PartiesOwnerTextBox.Text.Trim().Replace("%", "");
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.Address1Column] = this.PartiesAddress1TextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.Address2Column] = this.PartiesAddress2TextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.AddressColumn] = this.PartiesAddress1TextBox.Text.Trim() + " " + this.PartiesAddress2TextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.CityColumn] = this.PartiesCityTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.StateColumn] = this.PartiesStateTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.ZipColumn] = this.PartiesZipCodeTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[partiesRowId][this.exciseTaxAffidavitDataSet.PartiesHeader.CountryColumn] = this.PartiesCountryTextBox.Text.Trim();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    this.partySave = true;
                }
                catch (Exception e1)
                {
                    this.partySave = false;
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }

            this.SetPartiesGrid();
            if (this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                if (this.partiesRowCount > 0)
                {
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
        /// Handles the CellClick event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.selectedPartyGridRowId = e.RowIndex;
                this.partieCoulmnIndex = e.ColumnIndex;
                this.SetCalenderInvisible();
                if (this.partiesHeaderkeyPressed)
                {
                    //// Check if its Same Row Else Do
                    if (this.selectedPartyGridRowId != this.tempRowIdParties)
                    {
                        switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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
                                    //// PartiesDataGridView.Rows[Convert.ToInt32(e.RowIndex)].Selected = true;
                                    ////   PartiesDataGridView.CurrentCell = PartiesDataGridView[e.ColumnIndex, e.RowIndex];
                                    ////  TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, e.RowIndex); 
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
                                    //// if its Cancel Assing DataGrid Postion As its
                                    //// TerraScan.Common.TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.tempRowIdParties);
                                    ////TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.tempRowIdParties);

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
            }
        }

        /// <summary>
        /// Handles the Click event of the UpdateParites control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateParites_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.SavePartiesHeader();
            this.Cursor = Cursors.Default;
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
                this.headerChanges = true;
            }
            else
            {
                this.partySave = false;
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
            }

            return this.partyHeaderValidData;
        }

        /// <summary>
        /// Handles the Click event of the CancelPartiesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelPartiesButton_Click(object sender, EventArgs e)
        {
            ////  this.exciseTaxAffidavitDataSet.PartiesHeader.RejectChanges();
            this.Cursor = Cursors.WaitCursor;
            this.SetPartiesGrid();

            if (this.partiesRowCount > 0)
            {
                this.PartiesDataGridView.Rows[Convert.ToInt32(0)].Selected = true;
                this.PartiesDataGridView.CurrentCell = this.PartiesDataGridView[4, 0];
                this.PartiesDataGridView.Focus();
                this.SetPartiesText(0);
            }
            else
            {
                //// Disable
                this.SetEnableStatusforPartiesControls(false);
                this.PartiesDataGridView.Rows[Convert.ToInt32(0)].Selected = false;
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
                this.NewPartiesButton.Focus();
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Sets the parties grid.
        /// </summary>
        private void SetPartiesGrid()
        {
            ////  this.PartiesDataGridView.DataSource = null;
            this.CustomisePartiesDataGridView();
            this.partiesRowCount = this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Count;
            this.PartiesDataGridView.DataSource = this.exciseTaxAffidavitDataSet.PartiesHeader.Copy().DefaultView;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.partiesRowCount > 0)
                {
                    if (this.receiptIDExist)
                    {
                        ////Disable
                        this.SetEnableStatusforPartiesControls(false);
                        this.PartiesDataGridView.Enabled = true;
                    }
                }
                else
                {
                    this.PartiesDataGridView.Rows[Convert.ToInt32(0)].Selected = false;
                    //// Disable
                    this.SetEnableStatusforPartiesControls(false);
                    this.ClearPartiesHeader();
                    this.PartiesDataGridView.CurrentCell = null;
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
                    this.Cursor = Cursors.WaitCursor;
                    //// turn On because inorder to  avoid rowenter event 
                    this.partiesHeaderkeyPressed = true;
                    this.partiesRemoved = true;
                    /////this.exciseTaxAffidavitDataSet.PartiesHeader.Rows.Remove[
                    this.exciseTaxAffidavitDataSet.PartiesHeader.Rows[this.selectedPartyGridRowId].Delete();
                    this.exciseTaxAffidavitDataSet.PartiesHeader.AcceptChanges();
                    this.SetPartiesGrid();
                    if (this.partiesRowCount > 0)
                    {
                        if (this.selectedPartyGridRowId > 0)
                        {
                            this.selectedPartyGridRowId = this.partiesRowCount - 1;
                        }

                        //// TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, this.selectedPartyGridRowId);
                        //// this.SetPartiesText(this.selectedPartyGridRowId);
                        TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, 0);
                        this.SetPartiesText(0);
                        this.PartiesDataGridView.Focus();
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

                    /*if (this.selectedPartyGridRowId > 0)
                    {
                        this.selectedPartyGridRowId = this.selectedPartyGridRowId - 1;
                    }*/
                    this.SetPartiesGridButtons(ButtonOperation.Remove);
                    this.partyHeaderButtonOperation = (int)ButtonOperation.Remove;
                    this.affdvtRemove = true;
                    this.SetAffDvtButton(ButtonOperation.Remove);
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
            //// this.PartiesDataGridView.DataSource = null;
            //// this.SetPartiesGrid();  
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

                //// this.SetAffDvtButton(ButtonOperation.Update);
                //// this.PartiesDataGridView.CurrentCell = this.PartiesDataGridView[0, Convert.ToInt32(0)];
                this.PartiesDataGridView.Rows[0].Selected = false;
                //// TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, 0); 
                this.partiesHeaderkeyPressed = true;

                //// To be get From Resource String
                this.PartiesOwnerTextBox.Text = "100";
                //// Set Focus to First 
                this.GeneralPartiesNameTextBox.Focus();
                this.GeneralPartiesNameTextBox.BackColor = this.GeneralPartiesNameTextBox.SetFocusColor;
                this.ownerId = 0;
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
        /// Creates the new parties row.
        /// </summary>
        private void CreateNewPartiesRow()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
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

                /*if(this.affdvtButtonOperation == (int)ButtonOperation.New)
                //{
                //    this.exciseTaxAffidavitDataSet.PartiesHeader.Clear(); 
                //}
                //else
                //{
                //}*/
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
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.PercentOwnerColumn.ColumnName] = this.PartiesOwnerTextBox.Text.Trim().Replace("%", "");

                //// partiesTempRow["OwnerId"] = DBNull.Value;

                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.OwnerIDColumn.ColumnName] = this.ownerId;

                //// partiesTempRow["Address"] = this.PartiesAddress1TextBox.Text;
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.Address1Column.ColumnName] = this.PartiesAddress1TextBox.Text.Trim();

                ////partiesTempRow["Address"] = this.PartiesAddress2TextBox.Text;
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.Address2Column.ColumnName] = this.PartiesAddress2TextBox.Text.Trim();

                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.AddressColumn.ColumnName] = this.PartiesAddress1TextBox.Text.Trim() + "" + this.PartiesAddress2TextBox.Text.Trim();

                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.CityColumn.ColumnName] = this.PartiesCityTextBox.Text.Trim();

                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.StateColumn.ColumnName] = this.PartiesStateTextBox.Text.Trim();

                ////partiesTempRow["Zip"] = this.PartiesZipCodeTextBox.Text;
                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.ZipColumn.ColumnName] = this.PartiesZipCodeTextBox.Text.Trim();

                partiesTempRow[this.exciseTaxAffidavitDataSet.PartiesHeader.CountryColumn.ColumnName] = this.PartiesCountryTextBox.Text.Trim();
                ////partiesTempRow["Country"] = this.PartiesCountryTextBox.Text;

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
        /// Handles the KeyDown event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_KeyDown(object sender, KeyEventArgs e)
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

        /// <summary>
        /// Grids the parties cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridPartiesCancel(KeyEventArgs e)
        {
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        this.UpdateLocalPartiesDataSet(this.selectedPartyGridRowId);
                        this.partiesHeaderkeyPressed = false;
                        e.Handled = false;
                        this.SetDataGridCoulmn(this.PartiesDataGridView, this.selectedPartyGridRowId, this.partieCoulmnIndex);
                        this.SetPartiesGridButtons(ButtonOperation.Empty);
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
            if (this.affdvtButtonOperation != (int)ButtonOperation.New)
            {
                if (!this.partiesHeaderkeyPressed && (this.affdvtButtonOperation != (int)ButtonOperation.New))
                {
                    this.selectedPartyGridRowId = e.RowIndex;
                    this.tempRowIdParties = e.RowIndex;
                    this.SetPartiesText(e.RowIndex);
                    this.partieCoulmnIndex = e.ColumnIndex;
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesPhoneNoTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesPhoneNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Sets the partie header to update mode.
        /// </summary>
        private void SetPartieHeaderToUpdateMode()
        {
            if (!(this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                if ((this.PermissionFiled.editPermission || this.PermissionFiled.newPermission) || this.partyHeaderButtonOperation != (int)ButtonOperation.New)
                {
                    if (!this.partiesHeaderkeyPressed)
                    {
                        this.partySave = false;
                        this.partiesHeaderkeyPressed = true;
                        this.partyHeaderButtonOperation = (int)ButtonOperation.Update;
                    }

                    this.SetPartiesGridButtons(ButtonOperation.Update);
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesOwnerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesOwnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesAddress1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesAddress1TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesAddress2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesAddress2TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesCityTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesCityTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesStateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesStateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesZipCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesZipCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the PartiesCountryTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void PartiesCountryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetPartieHeaderToUpdateMode();
            }
        }
        #endregion

        #region Coding For ParcelHeader

        /// <summary>
        /// Handles the Click event of the parcelPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form form1401 = new Form();
                form1401 = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, null, this.form1105Control.WorkItem);
                if (form1401 != null)
                {
                    if (form1401.ShowDialog() != DialogResult.Cancel)
                    {
                        //// TO DO
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
        /// Handles the RowEnter event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.affdvtButtonOperation != (int)ButtonOperation.New)
            {
                if (!this.parcelHeaderKeyPressed && this.parcelButtonOperation != (int)ButtonOperation.New)
                {
                    this.parcelRowId = e.RowIndex;
                    this.tempParcelRowId = e.RowIndex;
                    this.SetParcelHeaderTextBox(this.parcelRowId);
                    this.parcelColumnId = e.ColumnIndex;
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_KeyDown(object sender, KeyEventArgs e)
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

        /// <summary>
        /// Grids the parties cancel.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GridParcelHeaderCancel(KeyEventArgs e)
        {
            ////MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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
        /// Disables the parcel header.
        /// </summary>
        /// <param name="statusParcelControls">if set to <c>true</c> [status parcel controls].</param>
        private void SetEnableSatustParcelHeader(bool statusParcelControls)
        {
            this.ParcelNumberTextBox.Enabled = statusParcelControls;
            this.ParcelNumberTextBox.BackColor = Color.White;
            this.PersonlaPropertyComboBox.Enabled = statusParcelControls;
            this.AssessedValueTextBox.Enabled = statusParcelControls;
            this.AssessedValueTextBox.BackColor = Color.White;
            this.LegalTextBox.Enabled = statusParcelControls;
            this.LegalTextBox.BackColor = Color.White;
            this.parcelPictureBox.Enabled = statusParcelControls;
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
                this.ParcelVScrolBar.BringToFront();
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
                /*this.ParcelNumberTextBox.Text = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[parcelRowIdSelect]["Number"].ToString();
                //this.AssessedValueTextBox.Text = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[parcelRowIdSelect]["AssessedValue"].ToString();
                ////this.LegalTextBox.Text = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[parcelRowIdSelect]["Legal"].ToString();
                ////int correctIndex = 0;
                ////// get the index of the cfgValue
                //// correctIndex = this.PersonlaPropertyComboBox.FindString(this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[parcelRowIdSelect]["IsPersonalProperty"].ToString());
                //// this.PersonlaPropertyComboBox.SelectedIndex = correctIndex;*/
                try
                {
                    this.ParcelNumberTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["ParcelNumber"].Value.ToString();
                    this.AssessedValueTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["AssessedValue"].Value.ToString();
                    this.LegalTextBox.Text = this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["Legal"].Value.ToString();
                    //// get the index of the cfgValue
                    SetComboboxValue(this.PersonlaPropertyComboBox, this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["IsPersonalProp"].Value.ToString());
                    //// correctIndex = this.PersonlaPropertyComboBox.FindString(this.ParcelHeaderDataGridView.Rows[parcelRowIdSelect].Cells["IsPersonalProp"].Value.ToString());
                    //// this.PersonlaPropertyComboBox.SelectedIndex = correctIndex;
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
        /// Handles the Click event of the ParcelNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNewButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.parcelButtonOperation = (int)ButtonOperation.New;
            this.SetParcelGridButtons(ButtonOperation.New);
            this.ClearParcelHeader();
            ////Enable
            this.SetEnableSatustParcelHeader(true);
            this.ParcelNumberTextBox.Focus();
            ////SetPanelPosition(this.ParcelHeaderPanel);
            this.Cursor = Cursors.Default;
            //// TerraScanCommon.SetDataGridViewPosition(this.PartiesDataGridView, 0);
        }

        /// <summary>
        /// Clears the parcel header.
        /// </summary>
        private void ClearParcelHeader()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.AssessedValueTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Parcels the mantadtory field.
        /// </summary>
        /// <returns> Return True if Parcel Field is filled else false</returns>
        private bool ParcelMantadtoryField()
        {
            bool parcelMntField;
            decimal assessedValue;
            if (!string.IsNullOrEmpty(this.AssessedValueTextBox.Text.Trim()))
            {
                assessedValue = Decimal.Parse(this.AssessedValueTextBox.Text.Trim());
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
                parcelMntField = false;
            }

            return parcelMntField;
        }

        /// <summary>
        /// Handles the Click event of the ParcelUpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelUpdateButton_Click(object sender, EventArgs e)
        {
            this.SaveParcelHeader();
        }

        /// <summary>
        /// Saves the parcel header.
        /// </summary>
        private void SaveParcelHeader()
        {
            ///// if its NewOpeation Then Save 
            if (this.ParcelMantadtoryField())
            {
                if (this.parcelButtonOperation == (int)ButtonOperation.New)
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        this.CreateNewParcelRow();
                        this.SetParcelGrid();
                        if (this.parcelRecordCount > 0)
                        {
                            this.parcelRowId = this.parcelRecordCount - 1;
                            TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId);
                            this.SetParcelHeaderTextBox(this.parcelRowId);
                        }

                        ////this.toolBoxSmartPart.Enabled = false; 
                        this.PartiesDataGridView.Focus();
                        if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
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
                else
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.UpdateParcelHeader(this.parcelRowId);
                        //// this.toolBoxSmartPart.Enabled = false;
                        this.ReceiptFormButton.Enabled = false;
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
                            this.SetEnableSatustParcelHeader(false);
                            this.ParcelHeaderDataGridView.Enabled = false;
                        }

                        if (this.affdvtButtonOperation != (int)ButtonOperation.New)
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
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

                this.parcelButtonOperation = (int)ButtonOperation.Empty;
                this.SetAffDvtButton(ButtonOperation.HeaderPartUpdate);
                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.parcelHeaderKeyPressed = false;
                ////   SetPanelPosition(this.ParcelHeaderPanel);
            }
            else
            {
                this.parcelSave = false;
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Updates the parcel header.
        /// </summary>
        /// <param name="rowId">The row id.</param>
        private void UpdateParcelHeader(int rowId)
        {
            try
            {
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["Number"] = this.ParcelNumberTextBox.Text.Trim();
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

                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["AssessedValue"] = this.AssessedValueTextBox.Text.Replace("$", "").Trim();
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[rowId]["Legal"] = this.LegalTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.ParcelHeader.Columns["IsPersonalPropertyValue"].ReadOnly = false;
                this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();

                //// ForNavigation From Other Form
                this.parcelSave = true;
            }
            catch (Exception e1)
            {
                this.parcelSave = false;
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Creates the new parcel row.
        /// </summary>
        private void CreateNewParcelRow()
        {
            try
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

                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    ////  this.exciseTaxAffidavitDataSet.ParcelHeader.Clear(); 
                }

                DataRow partiesTempRow = this.exciseTaxAffidavitDataSet.ParcelHeader.NewRow();
                partiesTempRow["SoldParcelID"] = this.keyParcelId;
                partiesTempRow["StatementID"] = 0;
                partiesTempRow["Number"] = this.ParcelNumberTextBox.Text.Trim();
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

                partiesTempRow["AssessedValue"] = this.AssessedValueTextBox.Text.Replace("$", "").Trim();

                partiesTempRow["Legal"] = this.LegalTextBox.Text.Trim();
                //// if(partiesTempRow["EmptyRecord"
                this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Add(partiesTempRow);
                this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();
                this.parcelRecordCount = this.exciseTaxAffidavitDataSet.ParcelHeader.Rows.Count;
                this.parcelSave = true;
                this.headerChanges = true;
            }
            catch (Exception e1)
            {
                this.parcelSave = false;
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the parcel header to update mode.
        /// </summary>
        private void SetParcelHeaderToUpdateMode()
        {
            if (this.parcelButtonOperation != (int)ButtonOperation.New && !(this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                if (this.PermissionFiled.editPermission || this.PermissionFiled.newPermission)
                {
                    this.parcelHeaderKeyPressed = true;
                    this.SetParcelGridButtons(ButtonOperation.Update);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelRemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelRemoveButton_Click(object sender, EventArgs e)
        {
            if (this.parcelRowId >= 0)
            {
                //// string keyId;
                //// string stmtId;
                //// string stringExp;
                //// DataRow foundRow;

                //// turn On because inorder to  avoid rowenter event 
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.exciseTaxAffidavitDataSet.ParcelHeader.Rows[this.parcelRowId].Delete();
                    this.exciseTaxAffidavitDataSet.ParcelHeader.AcceptChanges();
                    this.parcelHeaderKeyPressed = true;
                    this.SetParcelGrid();

                    if (this.parcelRecordCount > 0)
                    {
                        if (this.parcelRowId > 0)
                        {
                            this.parcelRowId = this.parcelRecordCount - 1;
                            TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, 0);
                            this.SetParcelHeaderTextBox(0);
                        }
                        else if (this.parcelRowId == 0)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.ParcelHeaderDataGridView, this.parcelRowId);
                            this.SetParcelHeaderTextBox(this.parcelRowId);
                        }
                    }
                    else
                    {
                        this.ParcelHeaderDataGridView.Rows[Convert.ToInt32(this.parcelRowId)].Selected = false;
                        ////this.PartiesDataGridView.CurrentCell = this.PartiesDataGridView[4, Convert.ToInt32(this.partiesRowCount)];

                        this.SetParcelHeaderTextBox(this.parcelRowId);
                        this.PartiesDataGridView.CurrentCell = null;
                    }

                    this.parcelHeaderKeyPressed = false;
                }
                catch (Exception e1)
                {
                    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                this.SetParcelGridButtons(ButtonOperation.Empty);
                this.SetAffDvtButton(ButtonOperation.Remove);
                this.ParcelNewButton.Focus();
                this.affdvtRemove = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelCancelButton_Click(object sender, EventArgs e)
        {
            ////this.exciseTaxAffidavitDataSet.ParcelHeader.RejectChanges();
            this.SetParcelGrid();
            if (this.parcelRecordCount > 0)
            {
                this.tempParcelRowId = 0;
                this.ParcelHeaderDataGridView.Rows[Convert.ToInt32(this.tempParcelRowId)].Selected = true;
                this.ParcelHeaderDataGridView.CurrentCell = this.ParcelHeaderDataGridView[2, Convert.ToInt32(this.tempParcelRowId)];
                this.SetParcelHeaderTextBox(this.tempParcelRowId);
            }
            else
            {
                this.ParcelHeaderDataGridView.Enabled = false;
                this.ClearParcelHeader();
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
                this.ParcelNewButton.Focus();
            }
        }

        /// <summary>
        /// Handles the CellClick event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.parcelCoulmnIndex = e.ColumnIndex;

                if (this.parcelHeaderKeyPressed)
                {
                    this.parcelRowId = e.RowIndex;    //// Check if its Same Row Else Do
                    if (this.tempParcelRowId != this.parcelRowId)
                    {
                        switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
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
                    this.tempParcelRowId = e.RowIndex;
                    this.SetParcelHeaderTextBox(e.RowIndex);
                    this.parcelCoulmnIndex = e.ColumnIndex;
                }
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
        /// Parcels the button opr no permission.
        /// </summary>
        private void ParcelButtonOprNoPermission()
        {
            this.ParcelNewButton.Enabled = false;
            this.ParcelUpdateButton.Enabled = false;
            this.ParcelRemoveButton.Enabled = false;
            this.ParcelCancelButton.Enabled = false;
            this.SetEnableSatustParcelHeader(false);
            if (this.receiptIDExist)
            {
                this.ParcelHeaderDataGridView.Enabled = false;
            }
            else
            {
                this.ParcelHeaderDataGridView.Enabled = true;
            }

            this.additionalOperationSmartPart.Enabled = false;
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
            this.SetEnableSatustParcelHeader(false);
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
            this.SetEnableSatustParcelHeader(false);
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
                //// Enable
                this.SetEnableSatustParcelHeader(true);
            }
            else
            {
                //// Diasable
                this.SetEnableSatustParcelHeader(false);
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
                    if (!this.receiptIDExist)   //// So Can Edit
                    {
                        this.ParcelNewButton.Enabled = true;
                        this.ParcelUpdateButton.Enabled = false;
                        this.ParcelRemoveButton.Enabled = true;
                        this.ParcelCancelButton.Enabled = false;
                        //// enable
                        this.SetEnableSatustParcelHeader(true);

                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                    else
                    {
                        //// Diasable
                        this.SetEnableSatustParcelHeader(false);

                        this.ParcelRemoveButton.Enabled = false;
                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                }
                else
                {
                    //// Diasable
                    this.SetEnableSatustParcelHeader(false);
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    if (!this.receiptIDExist)   //// So Can Edit
                    {
                        this.ParcelNewButton.Enabled = true;
                        this.ParcelUpdateButton.Enabled = false;
                        this.ParcelRemoveButton.Enabled = true;
                        this.ParcelCancelButton.Enabled = false;
                        //// enable
                        this.SetEnableSatustParcelHeader(true);

                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                    else
                    {
                        //// Diasable
                        this.SetEnableSatustParcelHeader(false);

                        this.ParcelRemoveButton.Enabled = false;
                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                }
                else
                {
                    //// Diasable
                    this.SetEnableSatustParcelHeader(false);
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = false;
                }
            }
            else if (this.PermissionFiled.newPermission)
            {
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    //// Diasable
                    this.SetEnableSatustParcelHeader(true);
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                }
                else
                {
                    //// If Only NEw Permission Not in new Mode then make it disable
                    this.SetEnableSatustParcelHeader(false);
                    this.ParcelNewButton.Enabled = false;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelHeaderDataGridView.Enabled = true;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                if (this.parcelRecordCount > 0)
                {
                    if (!this.receiptIDExist)   //// So Can Edit
                    {
                        this.ParcelNewButton.Enabled = true;
                        this.ParcelUpdateButton.Enabled = false;
                        this.ParcelRemoveButton.Enabled = true;
                        this.ParcelCancelButton.Enabled = false;
                        //// enable
                        this.SetEnableSatustParcelHeader(true);

                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                    else
                    {
                        //// Diasable
                        this.SetEnableSatustParcelHeader(false);

                        this.ParcelRemoveButton.Enabled = false;
                        this.ParcelHeaderDataGridView.Enabled = true;
                    }
                }
                else
                {
                    this.ParcelNewButton.Enabled = true;
                    this.ParcelUpdateButton.Enabled = false;
                    this.ParcelRemoveButton.Enabled = false;
                    this.ParcelCancelButton.Enabled = false;
                    //// enable
                    this.SetEnableSatustParcelHeader(false);
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

        /// <summary>
        /// Handles the KeyPress event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetParcelHeaderToUpdateMode();
            }
        }
        #endregion

        #region Affidvt

        /// <summary>
        /// Handles the CallUpdate event of the AffdvtTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffdvtTextBox_CallUpdate(object sender, KeyEventArgs e)
        {
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Loads the aff DVT combo.
        /// </summary>
        private void LoadAffDvtCombo()
        {
            InitComboBoxValues(this.AffidavitPartialSaleCombo);
            InitComboBoxValues(this.AffidavitSegregatedComboBox);
            InitComboBoxValues(this.AffdvtForestCombo);
            InitComboBoxValues(this.AffidvtOpenSpaceComb);
            InitComboBoxValues(this.AffDvtHistoryCombo);
            InitComboBoxValues(this.AffDvtContinuanceCombo);
            this.AffidavitStreetLocationCombo.Items.Clear();
            this.AffidavitStreetLocationCombo.Items.Insert(0, "CITY");
            this.AffidavitStreetLocationCombo.Items.Insert(1, "COUNTY");
        }

        /// <summary>
        /// Loads the aff DVT.
        /// </summary>
        /// <param name="affDvtRowNo">The aff DVT row no.</param>
        private void LoadAffDvtValue(int affDvtRowNo)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.AffidavitStreetAddressTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn].ToString();
                this.AffDvtDescTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn].ToString();
                SetComboboxValue(this.AffdvtForestCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsForestLandColumn].ToString());
                SetComboboxValue(this.AffidvtOpenSpaceComb, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn].ToString());
                SetComboboxValue(this.AffDvtHistoryCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn].ToString());
                SetComboboxValue(this.AffDvtContinuanceCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.HasContinuanceColumn].ToString());
                if (string.Compare(this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn].ToString(), "0") == 0)
                {
                    this.AffidavitStreetLocationCombo.SelectedIndex = 0;
                }
                else
                {
                    this.AffidavitStreetLocationCombo.SelectedIndex = 1;
                }

                SetComboboxValue(this.AffidavitPartialSaleCombo, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn].ToString());
                SetComboboxValue(this.AffidavitSegregatedComboBox, this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn].ToString());
                this.AffDvtLoactionTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn].ToString();
                this.AffdvtUseCodeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn].ToString();
                this.AffdvtExemptRegNumberTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn].ToString();
                this.AffDvtExemptionCodeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn].ToString();
                this.AffDvtExemptionDescrTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn].ToString();
                this.AffDvtDocumentTypeTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.DocumentTypeColumn].ToString();
                this.AffDvtDocDateTextBox.Text = this.exciseTaxAffidavitDataSet.Affidavit.Rows[affDvtRowNo][this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn].ToString();
            }
            catch
            {
                //// TODO
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the AffDvtDatePicture control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtDatePicture_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
            {
                try
                {
                    this.AffDvtMonthCalendar.SetDate(Convert.ToDateTime(this.AffDvtDocDateTextBox.Text.Trim()));
                    ShowDoumentCalender(this.AffDvtMonthCalendar, this.AffDvtDatePanle, this.AffDvtDatePicture, this.AffDvtPanel);
                }
                catch
                {
                    this.AffDvtMonthCalendar.Visible = false;
                    this.AffDvtDocDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                }
            }
            else
            {
                ShowDoumentCalender(this.AffDvtMonthCalendar, this.AffDvtDatePanle, this.AffDvtDatePicture, this.AffDvtPanel);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the AffDvtMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void AffDvtMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            //// Assign the selected date to the DateTextbox.
            this.AffDvtDocDateTextBox.Text = e.Start.ToShortDateString();
            this.AffDvtMonthCalendar.Visible = false;
            this.AffDvtDocDateTextBox.Focus();
            ////SetPanelPosition(this.AffDvtPanel);
        }

        /// <summary>
        /// Handles the Leave event of the AffDvtMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffDvtMonthCalendar_Leave(object sender, EventArgs e)
        {
            this.AffDvtMonthCalendar.Visible = false;
        }
        #endregion

        #region CalcDue

        /// <summary>
        /// Handles the Click event of the CalcDuePanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcDuePanel_Click(object sender, EventArgs e)
        {
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Sets the calc due text box.
        /// </summary>
        /// <param name="calDueRowId">The cal due row id.</param>
        private void SetCalcDueTextBox(int calDueRowId)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.CalcDueSellingPriceTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.GrossSalePriceColumn].ToString();
                this.CalcDuePerPropertTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.PersonalPropAmtColumn].ToString();
                this.CalDueRealPropTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.RealPropExemptAmtColumn].ToString();
                this.CalcDueTaxableSaleTextBox.Text = this.exciseTaxAffidavitDataSet.AmountDue.Rows[calDueRowId][this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn].ToString();
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
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Calulates the taxable sale price.
        /// </summary>
        private void CalulateTaxableSalePrice()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                double sellingPrice = 0.00;
                double propertPrice = 0.00;
                if (!string.IsNullOrEmpty(this.CalcDueSellingPriceTextBox.Text.Trim().ToString()))
                {
                    sellingPrice = Double.Parse(this.CalcDueSellingPriceTextBox.Text.Trim().ToString());
                    sellingPrice = Math.Round(sellingPrice, 2);
                }

                if ((!string.IsNullOrEmpty(this.CalcDuePerPropertTextBox.Text.Trim().ToString())) && (!string.IsNullOrEmpty(this.CalDueRealPropTextBox.Text.Trim().ToString())))
                {
                    propertPrice = Math.Round(Double.Parse(this.CalcDuePerPropertTextBox.Text.Trim().ToString()), 2) + Math.Round(Double.Parse(this.CalDueRealPropTextBox.Text.Trim().ToString()), 2);
                }
                //// this.CalcDueTaxableSaleTextBox.Text = ((Convert.ToDecimal(CalcDueSellingPriceTextBox.Text.Trim().ToString()) – (Convert.ToDecimal(this.CalcDuePerPropertTextBox.Text.Trim().ToString()) + (Convert.ToDecimal(this.CalDueRealPropTextBox.Text.Trim().ToString())));
                double taxSalesAmount = sellingPrice - propertPrice;
                this.CalcDueTaxableSaleTextBox.Text = taxSalesAmount.ToString();
            }
            catch (ArithmeticException arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
            try
            {
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
            }
            catch (ArithmeticException arException)
            {
                ExceptionManager.ManageException(arException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Calcs the tax total.  
        /// SUM of the form fields TechnologyFee and Transaction Fee.
        /// </summary>
        private void CalcTaxTotal()
        {
            double tecFee = 0.00;
            double tranFee = 0.00;
            try
            {
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
            }
            catch (ArithmeticException arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Calcs the tax total.  
        /// SUM of the form fields TechnologyFee and Transaction Fee.
        /// </summary>
        private void CalcTotalAmountDue()
        {
            double subTotal = 0.00;
            double fees = 0.00;
            try
            {
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
                this.CalcDueTtlAmountTextBox.Text = amountDue.ToString();
            }
            catch (ArithmeticException arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the CalcDueSellingPriceTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CalcDueSellingPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(this.CalcDueSellingPriceTextBox.Text.Trim()))
            {
                this.CalcDueSellingPriceTextBox.Text = this.currencyFormat;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the Leave event of the CalcDueTechFeeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcTaxTotal_Leave(object sender, EventArgs e)
        {
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.CalcTaxTotal();
            }
        }

        /// <summary>
        /// Handles the Click event of the CalcuDueCommandButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalcuDueCommandButton_Click(object sender, EventArgs e)
        {
            if (string.Compare(this.GeneralTaxCode.SelectedItem.ToString(), SharedFunctions.GetResourceString("TAXABLEValue")) == 0)
            {
                this.selectedTaxCode = 0;
            }
            else
            {
                this.selectedTaxCode = 1;
            }

            //// Checks All  Mandatory fields are filled or not;
            if (this.CheckForAmountDueRequriedField())
            {
                this.exciseTaxAffDvtAmountDueDataset = this.form1105Control.WorkItem.GetExciseTaxAffidavitCalulateAmountDue(Convert.ToDateTime(this.AffDvtDocDateTextBox.Text.Trim()), Convert.ToDateTime(this.GeneralHeaderPaymentDate.Text.Trim()), Convert.ToInt32(this.exciseRateId), this.selectedTaxCode, double.Parse(this.CalcDueTaxableSaleTextBox.Text.Trim()));
                this.validAmountDueDataset = CheckValidDataSet(this.exciseTaxAffDvtAmountDueDataset);
                this.SetAmountDueCalulateValue();
                this.CalulateTaxableSalePrice();
                this.CalcTaxTotal();
                this.CalcSubTotal();
                if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffDvtButton(ButtonOperation.Update);
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
            this.CalcDueTaxableSaleTextBox.Text = this.currencyFormat;
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
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.CalcDueExciseTaxTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.ExciseTaxStateColumn.ColumnName].ToString();
                    this.CalcDueExcisTaxLocaltextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.ExciseTaxLocalColumn.ColumnName].ToString();
                    this.CalcDueDelinqIntStateTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.DelInterestStateColumn.ColumnName].ToString();
                    this.CalcDueDelinqIntLocalTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.DelInterestLocalColumn.ColumnName].ToString();
                    this.CalcDueDelinqPenaltyTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.DelPenaltyColumn.ColumnName].ToString();
                    this.CalcDueTechFeeTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.TechnologyFeeAmountColumn.ColumnName].ToString();
                    this.CalcDueTransFeeTextBox.Text = this.exciseTaxAffDvtAmountDueDataset.AmountDue.Rows[0][this.exciseTaxAffDvtAmountDueDataset.AmountDue.TransactionFeeAmountColumn.ColumnName].ToString();
                }
                catch (Exception arException)
                {
                    ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
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
            if (string.IsNullOrEmpty(this.GeneralHeaderPaymentDate.Text.Trim()))
            {
                this.GeneralHeaderPaymentDate.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.PaymentDatePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
            }
            else
            {
                this.GeneralHeaderPaymentDate.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.PaymentDatePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
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
                if (!string.IsNullOrEmpty(this.CalcDueTaxableSaleTextBox.Text.Trim()))
                {
                    if (Double.Parse(this.CalcDueTaxableSaleTextBox.Text.Trim()) <= 0)
                    {
                        this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                        this.CalcDueTaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                        validField = false;
                    }
                    else
                    {
                        this.CalcDueTaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                        validField = true;
                    }
                }
            }
            else
            {
                this.CalcDueTaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            }

            if (int.Parse(this.exciseRateId) <= 0)
            {
                this.GeneralLinkLabel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.GeneralLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                this.DistrictPictureBox.BackColor = System.Drawing.Color.FromArgb(238, 210, 211);
                validExRate = false;
            }
            else
            {
                this.GeneralLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.GeneralLinkLabel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                this.DistrictPictureBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
                validExRate = true;
            }

            if (validField && validExRate && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralHeaderPaymentDate.Text.Trim()))
            {
                validExRate = true;
            }
            else
            {
                validField = false;
            }

            return validField;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the GeneralTaxCode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralTaxCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.taxCode = this.GeneralTaxCode.SelectedItem.ToString();
            if (string.Compare(this.taxCode, SharedFunctions.GetResourceString("TAXABLEValue")) == 0)
            {
                this.selectedTaxCode = 0;
            }
            else
            {
                this.selectedTaxCode = 1;
            }

            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the Leave event of the CalulateTaxableSalePrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CalulateTaxableSalePrice_Leave(object sender, EventArgs e)
        {
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.CalulateTaxableSalePrice();
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalulateTaxableSalePrice control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalulateTaxableSalePrice_Validating(object sender, CancelEventArgs e)
        {
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.CalulateTaxableSalePrice();
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalcSubTotal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalcSubTotal_Validating(object sender, CancelEventArgs e)
        {
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.CalcSubTotal();
            }
        }

        /// <summary>
        /// Handles the Validating event of the CalcTaxTotal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CalcTaxTotal_Validating(object sender, CancelEventArgs e)
        {
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.CalcTaxTotal();
            }
        }

        #endregion

        #region Suppliment

        /// <summary>
        /// Handles the Click event of the SupplementRHPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SupplementRHPanel_Click(object sender, EventArgs e)
        {
            this.SuppliReasonHeldTextBox.Focus();
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Handles the Click event of the SuppliInsDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuppliInsDatePict_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.SuppliInstDateTextBox.Text.Trim()))
            {
                try
                {
                    this.SuppliMonthCalendar.SetDate(Convert.ToDateTime(this.SuppliInstDateTextBox.Text.Trim()));
                    ShowCalender(this.SuppliMonthCalendar, this.SuppliInDatePanel, this.SuppliInsDatePict, this.SupplimentPanelHeader);
                }
                catch
                {
                    this.SuppliMonthCalendar.Visible = false;
                    this.SuppliInstDateTextBox.Text = DateTime.Now.ToString(this.dateFormat);
                }
            }
            else
            {
                ShowCalender(this.SuppliMonthCalendar, this.SuppliInDatePanel, this.SuppliInsDatePict, this.SupplimentPanelHeader);
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
            this.SuppliA1Combo.SelectedIndex = 0;
            this.SuppliA1TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliA1GrntPaysGranTextBox.Text = string.Empty;
            this.SuppliA2Combo.SelectedIndex = 0;
            this.SuppliA2TtlDbtTextBox.Text = this.currencyFormat;
            this.SuppliA2GrantPaysGranTextBox.Text = string.Empty;
            this.SuppliA1DbtRateTextBox.Text = this.currencyFormat + "%";
            this.SuppliB1Combo.SelectedIndex = 0;
            this.SuppliB2Combo.SelectedIndex = 0;
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
            InitComboBoxValues(this.SuppliA1Combo);
            InitComboBoxValues(this.SuppliA2Combo);
            InitComboBoxValues(this.SuppliB1Combo);
            InitComboBoxValues(this.SuppliB2Combo);
            InitComboBoxValues(this.SuppliB3Combo);
            InitComboBoxValues(this.SuppliB4Combo);
            InitComboBoxValues(this.SuppliRefiCombo);
        }

        /// <summary>
        /// Sets the suppliment text box.
        /// </summary>
        /// <param name="supplimentRowId">The suppliment row id.</param>
        private void SetSupplimentTextBox(int supplimentRowId)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.SuppliAgentNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.AgentNameColumn].ToString();
                this.SuppliInstTypeTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentTypeColumn].ToString();
                this.SuppliInstDateTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentDateColumn].ToString();
                this.SuppliFirmNameTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.FirmnameColumn].ToString();
                this.SuppliReasonHeldTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.ReasonHeldColumn].ToString();
                SetComboboxValue(this.SuppliA1Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A1Column].ToString());
                this.SuppliA1TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A1Column].ToString();
                this.SuppliA1GrntPaysGranTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A1Column].ToString();
                SetComboboxValue(this.SuppliA2Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftConsideration_A2Column].ToString());
                this.SuppliA2TtlDbtTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.TotalDebt_A2Column].ToString();
                this.SuppliA2GrantPaysGranTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteePaysGrantor_A2Column].ToString();
                this.SuppliA1DbtRateTextBox.Text = this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.DebtRate_A2Column].ToString();
                SetComboboxValue(this.SuppliB1Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column].ToString());
                SetComboboxValue(this.SuppliB2Combo, this.exciseTaxAffidavitDataSet.Suppliment.Rows[supplimentRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B2Column].ToString());
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
                this.TreasurerStatusButton.Text = "Treasurer - " + this.treasurerDesc;
                this.AssessorStatusButton.Text = "Assessor - " + this.assessorDesc;
                this.CalcSubTotal();
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion

        #endregion

        #region Common Function
        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            // Header Form
            if (this.form1105Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1105Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1105Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            this.formLabelInfo[0] = "Excise Tax Affidavit";
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

            ////Load RecordNavigatorSmartPart to RecordNavigatorSmartPartdeckWorkspace
            if (this.form1105Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show(this.form1105Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.form1105Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////Load ToolBoxSmartPart to ToolBoxSmartPartdeckWorkspace
            if (this.form1105Control.WorkItem.SmartParts.Contains(SmartPartNames.ToolBoxSmartPart))
            {
                this.ToolBoxDeckWorkspace.Show(this.form1105Control.WorkItem.SmartParts.Get(SmartPartNames.ToolBoxSmartPart));
            }
            else
            {
                this.ToolBoxDeckWorkspace.Show(this.form1105Control.WorkItem.SmartParts.AddNew<ToolBoxSmartPart>(SmartPartNames.ToolBoxSmartPart));
            }
            ////Load ToolBoxSmartPart to ToolBoxSmartPartdeckWorkspace

            if (this.form1105Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.AdditionalOperationWorkSpace.Show(this.form1105Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.AdditionalOperationWorkSpace.Show(this.form1105Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.F1105Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.F1105Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            this.toolBoxSmartPart = (ToolBoxSmartPart)this.F1105Control.WorkItem.SmartParts[SmartPartNames.ToolBoxSmartPart];

            this.ClearFilterControl = this.GetSmartPartControl(this.toolBoxSmartPart, "ClearFilterButton");
            this.ClearFilterControl.Enabled = false;
            this.recordNavigatorSmartPart = (RecordNavigatorSmartPart)this.F1105Control.WorkItem.SmartParts[SmartPartNames.RecordNavigatorSmartPart];
            // To Load FooterSmartPart into FooterWorkspace
            if (this.form1105Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form1105Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form1105Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form1105Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];

            this.footerSmartPart.ParentWorkItem = this.form1105Control.WorkItem;
            if (this.ParentFormId != null)
            {
                this.footerSmartPart.FormId = this.ParentFormId.ToString();
            }

            this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("F1105AuditLink");
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;



        }

        /// <summary>
        /// Handles the Load event of the f1105 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1105_Load(object sender, EventArgs e)
        {
            #region Affidvt

            //// Sets The Back Color To White
            this.DisableTextBox();

            #endregion
            this.AffDvtMenu.Visible = false;
            this.SaveMenu.Visible = false;
            this.NewMenu.Visible = false;
            this.LoadWorkSpace();
            this.queryControlArrayGeneral = new TerraScanTextBox[] { this.StatementIDTextBox, this.GeneralHeaderPaymentDate, this.GeneralHeaderFormDate, this.GeneralLinkTextBox, this.GeneralSubmittedDate, this.GeneralFromWeb, this.GerneralTotalDebit, this.GeneralLocationCode, this.GeneralTaxCodeTextBox, this.GeneralMobileHomeTextBox, this.GeneralReceiptNoTextBox, this.GeneralNote, this.CalcDueSellingPriceTextBox, this.CalcDuePerPropertTextBox, this.CalDueRealPropTextBox, this.CalcDueTaxableSaleTextBox };
            this.queryControlArrayAffdvt = new TerraScanTextBox[] { this.AffidavitPartialSaleTextBox, this.AffidavitSegregatedTextBox, this.AffidavitStreetAddressTextBox, this.AffidavitStreetLocationTextBox, this.AffDvtLoactionTextBox, this.AffdvtUseCodeQueryTextBox, this.AffdvtExemptRegNumberTextBox, this.AffdvtForestTextBox, this.AffidvtOpenSpaceTextBox, this.AffDvtHistoryTextBox, this.AffDvtContinuanceTextBox, this.AffDvtDescTextBox, this.AffDvtExemptionCodeTextBox, this.AffDvtExemptionDescrTextBox, this.AffDvtDocumentTypeTextBox, this.AffDvtDocDateTextBox };
            this.queryControlArrayParties = new TerraScanTextBox[] { this.GeneralPartiesNameTextBox, this.PartiesPhoneNoTextBox, this.PartiesTypeTextBox, this.PartiesOwnerTextBox, this.PartiesAddress1TextBox, this.PartiesAddress2TextBox, this.PartiesCityTextBox, this.PartiesStateTextBox, this.PartiesZipCodeTextBox, this.PartiesCountryTextBox };
            this.queryControlArrayParcels = new TerraScanTextBox[] { this.ParcelNumberTextBox, this.PersonlaPropertyTextBox, this.AssessedValueTextBox, this.LegalTextBox };
            this.queryControlArrayAmtDue = new TerraScanTextBox[] { this.CalcDueExciseTaxTextBox, this.CalcDueExcisTaxLocaltextBox, this.CalcDueDelinqIntStateTextBox, this.CalcDueDelinqIntLocalTextBox, this.CalcDueDelinqPenaltyTextBox, this.CalcDueTransFeeTextBox, this.CalcDueTechFeeTextBox };
            this.CustomisePartiesDataGridView();
            this.CustomiseParcelDataGridView();
            this.SetGeneralComboBox();

            if (this.StatId == -1)
            {
                this.LoadExciseTaxAffidavit(null, null);
            }

            if (this.PermissionFiled.newPermission)
            {
                this.NewMenu.Click += new EventHandler(this.NewAffdvtButton_Click);
                this.SaveMenu.Click += new EventHandler(this.SaveAffdvtButton_Click);
                this.ParentForm.CancelButton = this.CancelAffdvtButton;
            }
            else if (this.PermissionFiled.editPermission)
            {
                this.SaveMenu.Click += new EventHandler(this.SaveAffdvtButton_Click);
                this.ParentForm.CancelButton = this.CancelAffdvtButton;
            }
        }

        /// <summary>
        /// retrieves the current import position index
        /// </summary>
        /// <param name="tempRecordId">tempRecordId</param>
        /// <returns>index of the current record</returns>
        private int RetrieveRecordIndex(int? tempRecordId)
        {
            if (tempRecordId == null)
            {
                tempRecordId = this.currentAffidavitStatementId;
            }

            int tempIndex = 0;
            DataTable tempDataTable = this.exciseTaxAffidavitDataSet.General.Copy();
            tempDataTable.DefaultView.RowFilter = string.Concat(this.exciseTaxAffidavitDataSet.General.StatementIDColumn.ColumnName, " = ", tempRecordId);

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
            }

            return tempIndex;
        }

        /// <summary>
        /// Disables the aff DVT controls.
        /// </summary>
        /// <param name="enableStatus">if set to <c>true</c> [enable status].</param>
        private void SetAffDvtControls(bool enableStatus)
        {
            #region AffdtButton
            if (this.PermissionFiled.deletePermission && this.affdvtButtonOperation != (int)ButtonOperation.New)
            {
                this.DeleteAffdvtButton.Enabled = enableStatus;
            }
            #endregion
            #region GeneralHeader
            this.GeneralFormDatePic.Enabled = enableStatus;
            this.GeneralPaymentDatePict.Enabled = enableStatus;
            this.GeneralHeaderPaymentDate.Enabled = enableStatus;
            /////this.GeneralHeaderPaymentDate.BackColor = Color.White;
            this.GeneralHeaderFormDate.Enabled = enableStatus;
            this.GeneralHeaderFormDate.BackColor = Color.White;
            this.GeneralLinkLabel.Enabled = enableStatus;
            this.DistrictPictureBox.Enabled = enableStatus;
            this.DistrictPictureBox.BackColor = Color.White;
            this.GerneralTotalDebit.Enabled = enableStatus;
            this.GerneralTotalDebit.BackColor = Color.White;
            this.GeneralLocationCode.Enabled = enableStatus;
            this.GeneralLocationCode.BackColor = Color.White;
            this.GeneralTaxCode.Enabled = enableStatus;
            this.GeneralMobileHome.Enabled = enableStatus;
            this.GeneralNote.Enabled = enableStatus;
            this.GeneralNote.BackColor = Color.White;
            this.GeneralHeaderFormDate.BackColor = Color.White;
            this.DistrictPictureBox.Enabled = enableStatus;
            #endregion

            #region Affidavit

            this.AffidavitPartialSaleCombo.Enabled = enableStatus;
            this.AffidavitSegregatedComboBox.Enabled = enableStatus;
            this.AffidavitStreetAddressTextBox.Enabled = enableStatus;
            this.AffidavitStreetAddressTextBox.BackColor = Color.White;
            this.AffidavitStreetLocationCombo.Enabled = enableStatus;
            this.AffDvtLoactionTextBox.Enabled = enableStatus;
            this.AffDvtLoactionTextBox.BackColor = Color.White;
            this.AffdvtUseCodeTextBox.Enabled = enableStatus;
            this.AffdvtUseCodeTextBox.BackColor = Color.White;
            this.AffdvtExemptRegNumberTextBox.Enabled = enableStatus;
            this.AffdvtExemptRegNumberTextBox.BackColor = Color.White;
            this.AffdvtForestCombo.Enabled = enableStatus;
            this.AffdvtUseCodeQueryTextBox.Enabled = enableStatus;
            this.AffdvtUseCodeQueryTextBox.BackColor = Color.White;
            this.AffidvtOpenSpaceComb.Enabled = enableStatus;
            this.AffDvtHistoryCombo.Enabled = enableStatus;
            this.AffDvtContinuanceCombo.Enabled = enableStatus;
            this.AffDvtDescTextBox.Enabled = enableStatus;
            this.AffDvtDescTextBox.BackColor = Color.White;
            this.AffDvtExemptionCodeTextBox.Enabled = enableStatus;
            this.AffDvtExemptionCodeTextBox.BackColor = Color.White;
            this.AffDvtExemptionDescrTextBox.Enabled = enableStatus;
            this.AffDvtDocumentTypeTextBox.Enabled = enableStatus;
            this.AffDvtDocumentTypeTextBox.BackColor = Color.White;
            this.AffDvtDocDateTextBox.Enabled = enableStatus;
            this.AffDvtDocDateTextBox.BackColor = Color.White;
            this.AffdvtUseCodeTextBox.Enabled = enableStatus;
            this.AffDvtDatePicture.Enabled = enableStatus;
            #endregion

            #region calDue
            this.CalcDueSellingPriceTextBox.Enabled = enableStatus;
            this.CalcDueSellingPriceTextBox.BackColor = Color.White;
            this.CalcDuePerPropertTextBox.Enabled = enableStatus;
            this.CalcDuePerPropertTextBox.BackColor = Color.White;
            this.CalDueRealPropTextBox.Enabled = enableStatus;
            this.CalDueRealPropTextBox.BackColor = Color.White;
            ////this.CalcDueTaxableSaleTextBox.Enabled = enableStatus;
            this.CalcDueExciseTaxTextBox.Enabled = enableStatus;
            this.CalcDueExciseTaxTextBox.BackColor = Color.White;
            this.CalcDueExcisTaxLocaltextBox.Enabled = enableStatus;
            this.CalcDueExcisTaxLocaltextBox.BackColor = Color.White;
            this.CalcDueDelinqIntStateTextBox.Enabled = enableStatus;
            this.CalcDueDelinqIntStateTextBox.BackColor = Color.White;
            this.CalcDueDelinqIntLocalTextBox.Enabled = enableStatus;
            this.CalcDueDelinqIntLocalTextBox.BackColor = Color.White;
            this.CalcDueDelinqPenaltyTextBox.Enabled = enableStatus;
            this.CalcDueDelinqPenaltyTextBox.BackColor = Color.White;
            this.CalcDueTechFeeTextBox.Enabled = enableStatus;
            this.CalcDueTechFeeTextBox.BackColor = Color.White;
            this.CalcDueTransFeeTextBox.Enabled = enableStatus;
            this.CalcDueTransFeeTextBox.BackColor = Color.White;
            this.CalcDueFeesTextBox.BackColor = Color.White;
            this.CalcDueSubTotalTextBox.BackColor = Color.White;
            this.CalcDueTtlAmountTextBox.BackColor = Color.White;
            this.CalcDueTaxableSaleTextBox.BackColor = Color.White;
            if (this.receiptIDExist)
            {
                this.CalcuDueCommandButton.Enabled = false;
            }
            else
            {
                this.CalcuDueCommandButton.Enabled = enableStatus;
            }
            #endregion

            #region suppli
            this.SuppliAgentNameTextBox.BackColor = Color.White;
            this.SuppliAgentNameTextBox.Enabled = enableStatus;
            this.SuppliAgentNameTextBox.BackColor = Color.White;
            this.SuppliInstTypeTextBox.Enabled = enableStatus;
            this.SuppliInstTypeTextBox.BackColor = Color.White;
            this.SuppliInstDateTextBox.Enabled = enableStatus;
            this.SuppliInstDateTextBox.BackColor = Color.White;
            this.SuppliFirmNameTextBox.Enabled = enableStatus;
            this.SuppliFirmNameTextBox.BackColor = Color.White;
            this.SuppliReasonHeldTextBox.Enabled = enableStatus;
            this.SuppliReasonHeldTextBox.BackColor = Color.White;
            this.SuppliA1Combo.Enabled = enableStatus;
            this.SuppliA1TtlDbtTextBox.Enabled = enableStatus;
            this.SuppliA1TtlDbtTextBox.BackColor = Color.White;
            this.SuppliA1GrntPaysGranTextBox.Enabled = enableStatus;
            this.SuppliA1GrntPaysGranTextBox.BackColor = Color.White;
            this.SuppliA2Combo.Enabled = enableStatus;
            this.SuppliA2TtlDbtTextBox.Enabled = enableStatus;
            this.SuppliA2TtlDbtTextBox.BackColor = Color.White;
            this.SuppliA2GrantPaysGranTextBox.Enabled = enableStatus;
            this.SuppliA2GrantPaysGranTextBox.BackColor = Color.White;
            this.SuppliA1DbtRateTextBox.Enabled = enableStatus;
            this.SuppliA1DbtRateTextBox.BackColor = Color.White;
            this.SuppliB1Combo.Enabled = enableStatus;
            this.SuppliB2Combo.Enabled = enableStatus;
            this.SuppliB1TtlDbtTextBox.Enabled = enableStatus;
            this.SuppliB1TtlDbtTextBox.BackColor = Color.White;
            this.SuppliB3Combo.Enabled = enableStatus;
            this.SuppliB2TtlDbtTextBox.Enabled = enableStatus;
            this.SuppliB2TtlDbtTextBox.BackColor = Color.White;
            this.SuppliB4Combo.Enabled = enableStatus;
            this.SuppliRefiCombo.Enabled = enableStatus;
            this.SuppliGiftedEquityTextBox.Enabled = enableStatus;
            this.SuppliGiftedEquityTextBox.BackColor = Color.White;
            this.SuppliGranSignTextBox.Enabled = enableStatus;
            this.SuppliGranSignTextBox.BackColor = Color.White;
            this.SuppliGranteSignTextBox.Enabled = enableStatus;
            this.SuppliGranteSignTextBox.BackColor = Color.White;
            this.SuppliFNameTextBox.Enabled = enableStatus;
            this.SuppliFNameTextBox.BackColor = Color.White;
            this.SuppliGNameTextBox.Enabled = enableStatus;
            this.SuppliGNameTextBox.BackColor = Color.White;
            this.SuppliInsDatePict.Enabled = enableStatus;

            #endregion

            #region AddOprtSmartPArt
            // this.additionalOperationSmartPart.Enabled = enableStatus;
            #endregion
            #region   Treasurer / Asssessor

            if (this.affdvtButtonOperation == (int)ButtonOperation.New)
            {
                this.TreasurerStatusButton.Enabled = false;
                this.TreasurerStatusButton.Text = "Treasurer - ";
                //// this.ExciseRatesButton.Enabled = false;
                this.ViewAfdvtButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.AssessorStatusButton.Text = "Assessor - ";
                this.FilteredButton.Enabled = false;
                this.footerSmartPart.KeyId = null; 
                //this.ExciseAffidavitAuditLink.Enabled = false;
                //this.ExciseAffidavitAuditLink.Text = "tTR_Statement [StatementID]";
            }
            else if (this.PermissionFiled.editPermission || this.PermissionFiled.newPermission)
            {
                ////// Because Treasure / Assessor Should Be Enable Always
                /*
                if (string.Compare(this.treasurerStatus, "1") == 0 && this.receiptIDExist)
                {
                    //// this.TreasurerStatusButton.Enabled = false;
                }*/
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

                /*
                if (string.Compare(this.assessorStatus, "1") == 0 && this.receiptIDExist)
                {
                    this.AssessorStatusButton.Enabled = false;
                }
                else if (this.PermissionFiled.editPermission)
                {
                    this.AssessorStatusButton.Enabled = true;
                }
                else
                {
                    this.AssessorStatusButton.Enabled = false;
                }
                */
                this.FilteredButton.Enabled = true;
                //this.ExciseAffidavitAuditLink.Enabled = true;
            }
            else if (!this.PermissionFiled.editPermission)
            {
                this.TreasurerStatusButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.additionalOperationSmartPart.Enabled = false;
            }
            #endregion
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>IF No Update Operation in Progress then True else its False</returns>
        private bool CheckPageStatus()
        {
            bool pageSatus = false;

            if (!this.CheckUpdateMode())
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            if (this.partiesHeaderkeyPressed == true || this.partyHeaderButtonOperation == (int)ButtonOperation.New)
                            {
                                this.SavePartiesHeader();
                            }
                            else
                            {
                                this.affDvtKeypressed = true;
                                this.partySave = true;
                            }

                            if (this.partySave)
                            {
                                //// Checks For Any Changes in  Parcel
                                if (this.parcelHeaderKeyPressed == true || this.parcelButtonOperation == (int)ButtonOperation.New)
                                {
                                    this.SaveParcelHeader();
                                }
                                else
                                {
                                    this.affDvtKeypressed = true;
                                    this.parcelSave = true;
                                }
                            }

                            if (this.parcelSave)
                            {
                                //// if parties saved goes to newxt
                                if (this.affDvtKeypressed == true || this.affdvtButtonOperation == (int)ButtonOperation.New)
                                {
                                    this.SaveAffDvt();
                                }
                            }

                            if (this.affDvtSave)
                            {
                                pageSatus = true;
                            }
                            else
                            {
                                pageSatus = false;
                            }

                            break;
                        }

                    case DialogResult.No:
                        {
                            //// WithoutSaving Moves Record
                            this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtRemove = false;
                            this.affDvtKeypressed = false;
                            this.partiesHeaderkeyPressed = false;
                            this.parcelHeaderKeyPressed = false;
                            //// this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                            pageSatus = true;
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            pageSatus = false;
                            break;
                        }
                } ///// End Case
            }
            else
            {
                pageSatus = true;
            }

            return pageSatus;
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
            try
            {
                if (sourceDataGrid.Rows.Count > 0 && sourceRowId >= 0 && sourceColumnId >= 0)
                {
                    sourceDataGrid.Rows[Convert.ToInt32(sourceRowId)].Selected = false;
                    sourceDataGrid.CurrentCell = sourceDataGrid[sourceColumnId, Convert.ToInt32(sourceRowId)];
                    sourceDataGrid.CurrentCell.Selected = true;
                }
            }
            catch (IndexOutOfRangeException rangeException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), rangeException, ExceptionManager.ActionType.Display, this.ParentForm);
                //// TODO
            }
            catch (SystemException dataGridPositionException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), dataGridPositionException, ExceptionManager.ActionType.Display, this.ParentForm);
                //// TODO
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
                    try
                    {
                        this.GeneralPartiesNameTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.PartiesAddress1TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString();
                        this.PartiesAddress2TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                        this.PartiesCityTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString();
                        this.PartiesStateTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                        this.PartiesZipCodeTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.ZipColumn].ToString();
                    }
                    catch (Exception arException)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the partie header to update mode.
        /// </summary>
        private void SetAffiDvtToUpdateMode()
        {
            if (this.affdvtButtonOperation != (int)ButtonOperation.New)
            {
                if (!this.affDvtKeypressed)
                {
                    this.affDvtKeypressed = true;
                }

                this.SetAffDvtButton(ButtonOperation.Update);
            }
        }

        /// <summary>
        /// retrieves the current importId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>import id of the current record</returns>
        private int RetrieveStatementId(int index)
        {
            int tempImportID = 0;

            if (this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count > 0)
            {
                if (index > 0 && index <= this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows[index - 1][0])))
                    {
                        tempImportID = Convert.ToInt32(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows[index - 1][0]);
                    }
                }
                else
                {
                    tempImportID = Convert.ToInt32(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows[this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count - 1][0]);
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count));
                }
            }

            return tempImportID;
        }

        /// <summary>
        /// Retrieves the index.
        /// </summary>
        /// <returns>Index of the CurrendStatmentId</returns>
        private int RetrieveIndex()
        {
            int tempIndex = this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count;
            DataTable tempDataTable = this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Copy();
            tempDataTable.DefaultView.RowFilter = string.Concat(this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.KeyIDColumn.ColumnName, " = ", this.currentAffidavitStatementId);
            if (this.affdvtButtonOperation != (int)ButtonOperation.New)
            {
                if (tempDataTable.DefaultView.Count > 0)
                {
                    tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
                }
                else
                {
                    tempIndex = 1;
                }
            }

            return tempIndex;
        }

        /// <summary>
        /// Gets the excise affidavit details.
        /// </summary>
        private void GetExciseAffidavitDetails()
        {
            //// if Valid Then Call General Header To Fill
            if (this.validDataSet)
            {
                #region General
                if (this.exciseTaxAffidavitDataSet.General.Rows.Count > 0)
                {
                    this.FillGeneralHeaderText(0);
                }
                #endregion

                #region Parties
                //// Loads The PArties Grid
                this.SetPartiesGrid();
                #endregion

                #region Parcel
                this.SetParcelGrid();
                if (this.parcelRecordCount > 0)
                {
                    this.SetParcelHeaderTextBox(0);
                }
                else
                {
                    this.ParcelHeaderDataGridView.Rows[Convert.ToInt32(0)].Selected = false;
                }
                #endregion

                #region AffDvt

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
                #endregion

                #region Suppliment
                if (this.exciseTaxAffidavitDataSet.Suppliment.Rows.Count > 0)
                {
                    this.SetSupplimentTextBox(0);
                }
                #endregion

                this.CalcDueTaxableSaleTextBox.BackColor = Color.White;
                this.CalcDueFeesTextBox.BackColor = Color.White;
                this.CalcDueSubTotalTextBox.BackColor = Color.White;
                this.CalcDueTtlAmountTextBox.BackColor = Color.White;
            }
        }

        ///// <summary>
        ///// Sets the panel position.
        ///// </summary>
        ///// <param name="sourcePanel">The source panel.</param>
        ////private void SetPanelPosition(Panel sourcePanel)
        ////{
        ////    // this.MainPanel.ScrollControlIntoView(sourcePanel); 
        ////}       

        /// <summary>
        /// Handles the Click event of the SaveAffdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveAffdvtButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.parcelButtonOperation != (int)ButtonOperation.New && this.partyHeaderButtonOperation != (int)ButtonOperation.New && !this.parcelHeaderKeyPressed && !this.partiesHeaderkeyPressed)
                {
                    this.SaveAffDvt();
                    this.SetCaluDueButtonsBGColor();
                    //// this.SetPaymentDateTextBox();
                    ////this.NewAffdvtButton.Focus();  
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("parties/parcelUpdateMode"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception arException)
            {
                this.affDvtSave = false;
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the payment date text box.
        /// </summary>
        private void SetPaymentDateTextBox()
        {
            if (this.GeneralHeaderPaymentDate.Enabled)
            {
                ////this.GeneralHeaderPaymentDate.BackColor = this.GeneralHeaderPaymentDate.SetFocusColor;
                ////this.PaymentDatePanel.BackColor = this.GeneralHeaderPaymentDate.SetFocusColor;
            }
        }

        /// <summary>
        /// Saves the aff DVT.
        /// </summary>
        private void SaveAffDvt()
        {
            if (this.CheckForMandatoryField())
            {
                DataSet tempDataSet = new DataSet("Root");
                DataTable general = new DataTable();
                DataTable amountDue = new DataTable();
                DataTable affDVt = new DataTable();
                DataTable suppliment = new DataTable();
                tempDataSet.Tables.Add(general);
                tempDataSet.Tables.Add(amountDue);
                tempDataSet.Tables.Add(affDVt);
                tempDataSet.Tables.Add(suppliment);
                if (this.affdvtButtonOperation == (int)ButtonOperation.New)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.SaveAffiDvtDetails();
                        int affDvtstId = 0;

                        string artieslAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.PartiesHeader);
                        string parcelAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.ParcelHeader);
                        tempDataSet.Tables[0].Merge(this.exciseTaxAffidavitDataSet.General);
                        tempDataSet.Tables[1].Merge(this.exciseTaxAffidavitDataSet.AmountDue);
                        tempDataSet.Tables[2].Merge(this.exciseTaxAffidavitDataSet.Affidavit);
                        tempDataSet.Tables[3].Merge(this.exciseTaxAffidavitDataSet.Suppliment);
                        affDvtstId = this.form1105Control.WorkItem.SaveAffiDavitDetails(affDvtstId, artieslAddress, parcelAddress, tempDataSet.GetXml(), TerraScanCommon.UserId);
                        this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                        this.LoadExciseTaxAffidavit(null, affDvtstId);
                        this.SetAmountDueSaveStatus();
                        this.affDvtKeypressed = false;
                        this.parcelHeaderKeyPressed = false;
                        this.partiesHeaderkeyPressed = false;
                        this.FilteredButton.Enabled = true;

                        ////this.ExciseRatesButton.Enabled = true;
                        if (this.PermissionFiled.editPermission)
                        {
                            this.TreasurerStatusButton.Enabled = true;
                            this.AssessorStatusButton.Enabled = true;
                        }
                        else
                        {
                            this.TreasurerStatusButton.Enabled = false;
                            this.AssessorStatusButton.Enabled = false;
                        }

                        this.ViewAfdvtButton.Enabled = true;
                        //this.ExciseAffidavitAuditLink.Enabled = true;
                        this.affDvtSave = true;
                        this.headerChanges = false;
                    }
                    catch (Exception arException)
                    {
                        this.affDvtSave = false;
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }
                else  //// for Update
                {
                    //// Set Default Sort
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.exciseTaxAffidavitDataSet.General.DefaultView.Sort = "StatementID";
                        int updateRowId = this.exciseTaxAffidavitDataSet.General.DefaultView.Find(this.StatementIDTextBox.Text.Trim());
                        this.UpdateAffvt(updateRowId);
                        string updatePartieslAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.PartiesHeader);
                        string updateParcelAddress = TerraScanCommon.GetXmlString(this.exciseTaxAffidavitDataSet.ParcelHeader);
                        tempDataSet.Tables[0].Merge(this.exciseTaxAffidavitDataSet.General);
                        tempDataSet.Tables[1].Merge(this.exciseTaxAffidavitDataSet.AmountDue);
                        tempDataSet.Tables[2].Merge(this.exciseTaxAffidavitDataSet.Affidavit);
                        tempDataSet.Tables[3].Merge(this.exciseTaxAffidavitDataSet.Suppliment);
                        int updatestId = Int16.Parse(this.StatementIDTextBox.Text.Trim());
                        updatestId = this.form1105Control.WorkItem.SaveAffiDavitDetails(updatestId, updatePartieslAddress, updateParcelAddress, tempDataSet.GetXml(), TerraScanCommon.UserId);
                        this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                        this.LoadExciseTaxAffidavit(null, updatestId);
                        this.SetAmountDueSaveStatus();
                        this.affDvtKeypressed = false;
                        this.parcelHeaderKeyPressed = false;
                        this.partiesHeaderkeyPressed = false;
                        this.partiesRemoved = false;
                        this.affdvtRemove = false;
                        //this.ExciseAffidavitAuditLink.Enabled = true;
                        this.affDvtSave = true;
                        this.Cursor = Cursors.Default;
                        this.SetAffDvtButton(ButtonOperation.Empty);
                        this.headerChanges = false;
                    }
                    catch (Exception arException)
                    {
                        this.affDvtSave = false;
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }
                }

                this.additionalOperationSmartPart.Enabled = true;
                this.toolBoxSmartPart.Enabled = true;
                this.recordNavigatorSmartPart.Enabled = true;
            }
            else
            {
                this.affDvtSave = false;
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseTaxAffDvtMissing"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sets the amount due save status.
        /// </summary>
        private void SetAmountDueSaveStatus()
        {
            this.GeneralHeaderPaymentDate.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.AffDvtDocDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.CalcDueTaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
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
            try
            {
                //// Create New Record For Suppliemnt
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AgentNameColumn.ColumnName] = this.SuppliAgentNameTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentTypeColumn.ColumnName] = this.SuppliInstTypeTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.InstrumentDateColumn.ColumnName] = this.SuppliInstDateTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.FirmnameColumn.ColumnName] = this.SuppliFirmNameTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.ReasonHeldColumn.ColumnName] = this.SuppliReasonHeldTextBox.Text.Trim();
                //// GiftConsideration_A1

                if (String.Equals(this.SuppliA1Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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

                if (String.Equals(this.SuppliA2Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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
                if (String.Equals(this.SuppliB1Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 0;
                }

                ////GiftNoConsideration_B2
                if (String.Equals(this.SuppliB2Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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

                /////Grantor’s Signature (Name)
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GrantorsSignatureColumn.ColumnName] = this.SuppliGranSignTextBox.Text.Trim();

                ////Grantee’s Signature
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteesSignatureColumn.ColumnName] = this.SuppliGranteSignTextBox.Text.Trim();

                ////Facilitator Name
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.FacilitatorNameColumn.ColumnName] = this.SuppliFNameTextBox.Text.Trim();

                ////Grantee Name
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn.ColumnName] = this.SuppliGNameTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn.ColumnName] = this.treasurerDesc;
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.TreasurerStatusIDColumn.ColumnName] = int.Parse(this.treasurerStatus);
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn.ColumnName] = this.assessorDesc;
                this.exciseTaxAffidavitDataSet.Suppliment.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Suppliment.AssessorStatusIDColumn.ColumnName] = int.Parse(this.assessorStatus);
                this.exciseTaxAffidavitDataSet.Suppliment.AcceptChanges();
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Updates the affdvt header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateAffdvtHeader(int updateRowId)
        {
            try
            {
                //// AffDVt
                this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn.ReadOnly = false;

                if (string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn.ColumnName] = DBNull.Value;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn.ColumnName] = this.AffDvtDocDateTextBox.Text.Trim();
                }

                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.DocumentTypeColumn.ColumnName] = this.AffDvtDocumentTypeTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn.ColumnName] = this.AffDvtExemptionCodeTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn.ColumnName] = this.AffDvtExemptionDescrTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn.ColumnName] = this.AffdvtExemptRegNumberTextBox.Text.Trim();

                if (String.Equals(this.AffDvtContinuanceCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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

                if (String.Equals(this.AffDvtHistoryCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 0;
                }

                if (String.Equals(this.AffidvtOpenSpaceComb.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 0;
                }

                if (String.Equals(this.AffidavitPartialSaleCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 0;
                }

                if (String.Equals(this.AffidavitSegregatedComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 0;
                }

                if (String.Equals(this.AffidavitStreetLocationCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), "COUNTY"))
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 0;
                }

                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn.ColumnName] = this.AffDvtLoactionTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn.ColumnName] = this.AffDvtDescTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn.ColumnName] = this.AffidavitStreetAddressTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.Rows[updateRowId][this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.AffdvtUseCodeTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.Affidavit.AcceptChanges();
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Updates the amount due header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateAmountDueHeader(int updateRowId)
        {
            try
            {
                //// Create New Record From Calc Due
                this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.GrossSalePriceColumn.ColumnName] = this.CalcDueSellingPriceTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.PersonalPropAmtColumn.ColumnName] = this.CalcDuePerPropertTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.RealPropExemptAmtColumn.ColumnName] = this.CalDueRealPropTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.AmountDue.Rows[updateRowId][this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn.ColumnName] = this.CalcDueTaxableSaleTextBox.Text.Trim();
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
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Updates the general header.
        /// </summary>
        /// <param name="updateRowId">The update row id.</param>
        private void UpdateGeneralHeader(int updateRowId)
        {
            try
            {
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.StatementIDColumn.ColumnName] = this.StatementIDTextBox.Text.Trim();
                this.exciseTaxAffidavitDataSet.General.PaymentDateColumn.ReadOnly = false;
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PaymentDateColumn.ColumnName] = this.GeneralHeaderPaymentDate.Text.Trim();
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.FormDateColumn.ColumnName] = this.GeneralHeaderFormDate.Text.Trim();
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DistrictColumn.ColumnName] = this.GeneralLinkLabel.Text.Trim();
                if (string.Compare(this.GeneralSubmittedDate.Text.Trim(), "N/A") == 0)
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn.ColumnName] = DBNull.Value;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.SubmittedDateColumn.ColumnName] = this.GeneralSubmittedDate.Text.Trim();
                }

                if (String.Compare(this.GeneralFromWeb.Text.Trim().ToUpperInvariant(), SharedFunctions.GetResourceString("YESValue")) == 0)
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.PreDatesStmtColumn.ColumnName] = 0;
                }

                if (String.Equals(this.GeneralMobileHome.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn.ColumnName] = 1;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsMobileHomeColumn.ColumnName] = 0;
                }

                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.TotalDebtColumn.ColumnName] = this.GerneralTotalDebit.Text.Replace("$", "").Trim();
                this.exciseTaxAffidavitDataSet.General.LocationCodeColumn.AllowDBNull = true;
                if (string.IsNullOrEmpty(this.GeneralLocationCode.Text.Trim()))
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.LocationCodeColumn.ColumnName] = DBNull.Value;
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.LocationCodeColumn.ColumnName] = this.GeneralLocationCode.Text.Trim();
                }

                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.ReceiptNumberColumn.ColumnName] = this.GeneralReceiptNo.Text.Trim();
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.ExciseRateIDColumn.ColumnName] = this.exciseRateId;
                this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.DORNoteColumn.ColumnName] = this.GeneralNote.Text.Trim();

                if (String.Equals(this.GeneralTaxCode.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("EXEMPTValue")))
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsExemptColumn.ColumnName] = 1;
                    this.taxCode = "1";
                }
                else
                {
                    this.exciseTaxAffidavitDataSet.General.Rows[updateRowId][this.exciseTaxAffidavitDataSet.General.IsExemptColumn.ColumnName] = 0;
                    this.taxCode = "0";
                }

                this.exciseTaxAffidavitDataSet.General.AcceptChanges();
            }
            catch (Exception arException)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), arException, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Checks for mandatory field.
        /// </summary>
        /// <returns> True If All Field are Filled else False</returns>
        private bool CheckForMandatoryField()
        {
            bool validMandatoryField = false;
            if (!string.IsNullOrEmpty(this.GeneralHeaderPaymentDate.Text.Trim()) && !string.IsNullOrEmpty(this.GeneralLinkLabel.Text.Trim()) && this.GeneralTaxCode.SelectedIndex >= 0 && this.GeneralMobileHome.SelectedIndex >= 0 && this.AffidavitStreetLocationCombo.SelectedIndex >= 0 && !string.IsNullOrEmpty(this.AffDvtLoactionTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.AffdvtUseCodeTextBox.Text.Trim()) && this.AffdvtForestCombo.SelectedIndex >= 0 && this.AffidvtOpenSpaceComb.SelectedIndex >= 0 && this.AffDvtHistoryCombo.SelectedIndex >= 0 && !string.IsNullOrEmpty(this.AffDvtDocumentTypeTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()) && this.exciseRateId != "0")
            {
                if (this.GeneralTaxCode.SelectedIndex == 0)
                {
                    if (Decimal.Parse(this.CalcDueSellingPriceTextBox.Text.Trim()) > 0)
                    {
                        validMandatoryField = true;
                    }
                    else
                    {
                        validMandatoryField = false;
                    }
                }
                else
                {
                    validMandatoryField = true;
                }

                Match m = Regex.Match(this.AffdvtUseCodeTextBox.Text.Trim(), this.validUsedCode);
                if (m.Success)
                {
                    validMandatoryField = true;
                }
                else
                {
                    validMandatoryField = false;
                }
            }
            else
            {
                validMandatoryField = false;
            }

            return validMandatoryField;
        }

        /// <summary>
        /// Handles the Click event of the DistrictPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictPictureBox_Click(object sender, EventArgs e)
        {
            Form districtF1102 = new Form();
            districtF1102 = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1102, null, this.form1105Control.WorkItem);
            DialogResult districtDialog;
            if (districtF1102 != null)
            {
                districtDialog = districtF1102.ShowDialog();

                if (districtDialog == DialogResult.Yes)
                {
                    try
                    {
                        this.exciseRateId = TerraScanCommon.GetValue(districtF1102, "ExciseRateDistrictSelectionId");
                        this.districtSelectionDataSet = this.form1105Control.WorkItem.GetDistrictSelection(Convert.ToInt32(this.exciseRateId));
                        this.districtId = int.Parse(this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictIDColumn].ToString());

                        if (this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows.Count > 0)
                        {
                            this.GeneralLinkLabel.Text = this.districtSelectionDataSet.ListAffidavitDistrictSelection.Rows[0][this.districtSelectionDataSet.ListAffidavitDistrictSelection.DistrictColumn].ToString();
                        }

                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
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
                        formInfo = TerraScanCommon.GetFormInfo(1101);
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
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
        /// Handles the SelectionChangeCommitted event of the PersonlaPropertyComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PersonlaPropertyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SetParcelHeaderToUpdateMode();
        }

        /// <summary>
        /// Handles the KeyPress event of the AssessedValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AssessedValueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetParcelHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the LegalTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LegalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                this.SetParcelHeaderToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PictureParcel_Click(object sender, EventArgs e)
        {
            Form parcelF9101 = new Form();
            parcelF9101 = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form1105Control.WorkItem);

            if (parcelF9101 != null)
            {
                if (parcelF9101.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(parcelF9101, "MasterNameOwnerId"));
                        this.ownerDetailDataSet = this.form1105Control.WorkItem.GetOwnerDetails(this.ownerId);
                    }
                    catch (Exception)
                    {
                        //// TODO
                    }

                    if (this.OwnerDetailStatus())
                    {
                        this.AssignOwnerDetail();
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
            }
        }

        /// <summary>
        /// Sets the calender invisible.
        /// </summary>
        private void SetCalenderInvisible()
        {
            this.FormDateCalender.Visible = false;
            this.PaymentDateCalender.Visible = false;
            this.SuppliMonthCalendar.Visible = false;
            this.AffDvtMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Checks the update mode.
        /// </summary>
        /// <returns>If it is Update Mode return Tru or else false</returns>
        private bool CheckUpdateMode()
        {
            if (this.affDvtKeypressed || this.partiesHeaderkeyPressed || this.parcelHeaderKeyPressed || this.affdvtButtonOperation == (int)ButtonOperation.New || this.affdvtButtonOperation == (int)ButtonOperation.Update || this.parcelButtonOperation == (int)ButtonOperation.New || this.partyHeaderButtonOperation == (int)ButtonOperation.New || this.affdvtRemove || this.headerChanges || this.partiesRemoved)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Handles the Click event of the MainPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MainPanel_Click(object sender, EventArgs e)
        {
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Handles the 1 event of the ReceiptFormButton_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptFormButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1100);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.currentAffidavitStatementId;
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

        #endregion

        #region  Affidvt Code

        #region ButtonEvents

        /// <summary>
        /// Clears the aff DVT controls.
        /// </summary>
        private void ClearAffDvtControls()
        {
            this.AffidavitPartialSaleCombo.SelectedIndex = 0;
            this.AffidavitSegregatedComboBox.SelectedIndex = 0;
            this.AffidavitStreetAddressTextBox.Text = string.Empty;
            this.AffidavitStreetLocationCombo.SelectedIndex = 0;
            this.AffDvtLoactionTextBox.Text = string.Empty;
            this.AffdvtUseCodeTextBox.Text = string.Empty;
            this.AffdvtExemptRegNumberTextBox.Text = string.Empty;
            this.AffdvtForestCombo.Text = string.Empty;
            this.AffidvtOpenSpaceComb.SelectedIndex = 0;
            this.AffDvtHistoryCombo.SelectedIndex = 0;
            this.AffDvtContinuanceCombo.SelectedIndex = 0;
            this.AffDvtDescTextBox.Text = string.Empty;
            this.AffDvtExemptionCodeTextBox.Text = string.Empty;
            this.AffDvtExemptionDescrTextBox.Text = string.Empty;
            this.AffDvtDocumentTypeTextBox.Text = string.Empty;
            this.AffDvtDocDateTextBox.Text = string.Empty;
        }

        #endregion

        /// <summary>
        /// Disables the text box.
        /// </summary>
        private void DisableTextBox()
        {
            this.StatementIDTextBox.BackColor = System.Drawing.Color.White;
            this.StatementIDTextBox.Enabled = false;
            this.GeneralSubmittedDate.BackColor = System.Drawing.Color.White;
            this.GeneralSubmittedDate.Enabled = false;
            this.GeneralFromWeb.BackColor = System.Drawing.Color.White;
            this.GeneralFromWeb.Enabled = false;
        }

        /// <summary>
        /// Handles the CallUpdate event of the AffdvtTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AffdvtTextBox_CallUpdate(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 27)
            {
                if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffiDvtToUpdateMode();
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the AffidvtComb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffidvtComb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }
        #endregion

        #region Common Events

        /// <summary>
        /// Handles the Click event of the ReceiptFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptFormButton_Click(object sender, EventArgs e)
        {
            /*FormInfo formInfo;
            //formInfo = TerraScanCommon.GetFormInfo(1100);
            // formInfo.optionalParameters = new object[1];
            // formInfo.optionalParameters[0] = this.currentAffidavitStatementId;
            //this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));*/
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
                formInfo = TerraScanCommon.GetFormInfo(1101);
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
        /// Handles the Click event of the ViewAfdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewAfdvtButton_Click(object sender, EventArgs e)
        {
            try
            {
                string reportAuditId = string.Empty;
                this.Cursor = Cursors.WaitCursor;

                reportAuditId = this.StatementIDTextBox.Text.Trim();

                this.reportFileIdHashTable.Clear();
                this.reportFileIdHashTable.Add("KeyName", "ReportFileID");
                this.reportFileIdHashTable.Add("KeyValue", reportAuditId);

                // Shows the report form.
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(11050, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportFileIdHashTable);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

                ////MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ExciseAffidavitAuditLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExciseAffidavitAuditLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.exciseTaxAffidavitDataSet.ListAffidavitStatementId.Rows.Count > 0)
            {
                ErrorEngine.ShowForm((int)TerraScanCommon.ErrorEngineType.One);
            }
        }

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
                if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
                {
                    this.Cursor = Cursors.WaitCursor;
                    statementId = Convert.ToInt32(this.StatementIDTextBox.Text.Trim());
                    Form form1111 = new Form();
                    object[] optionalParameter = new object[] {"1111", statementId, 1 };
                    form1111 = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1111, optionalParameter, this.form1105Control.WorkItem);

                    if (form1111 != null)
                    {
                        if (form1111.ShowDialog() != DialogResult.Cancel)
                        {
                            this.treasurerStatus = TerraScanCommon.GetValue(form1111, "StatusId");
                            this.treasurerDesc = TerraScanCommon.GetValue(form1111, "Status");
                            this.TreasurerStatusButton.Text = "Treasurer - " + this.treasurerDesc;
                            ////if (this.receiptIDExist && string.Compare(this.treasurerStatus, "1") == 0)
                            ////{
                            ////    this.TreasurerStatusButton.Enabled = false;
                            ////}

                            /*if (!this.receiptIDExist)
                            //{
                            //    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            //    this.SetAffDvtButton(ButtonOperation.Update);
                            //}*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

                ////MessageBox.Show(ex.Message);
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
                if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
                {
                    statementId = Convert.ToInt32(this.StatementIDTextBox.Text.Trim());

                    Form form1111 = new Form();
                    object[] optionalParameter = new object[] {"1111", statementId, 0 };
                    form1111 = this.form1105Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1111, optionalParameter, this.form1105Control.WorkItem);
                    if (form1111 != null)
                    {
                        if (form1111.ShowDialog() != DialogResult.Cancel)
                        {
                            this.assessorStatus = TerraScanCommon.GetValue(form1111, "StatusId");
                            this.assessorDesc = TerraScanCommon.GetValue(form1111, "Status");
                            this.AssessorStatusButton.Text = "Assessor - " + this.assessorDesc;
                            ////if (this.receiptIDExist && string.Compare(this.assessorStatus, "1") == 0)
                            ////{
                            ////    this.AssessorStatusButton.Enabled = false;
                            ////}

                            /* if (!this.receiptIDExist)
                            //{
                            //    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            //    this.SetAffDvtButton(ButtonOperation.Update);
                            //}*/
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);

                ////MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region F1105

        /// <summary>
        /// Handles the Click event of the NewAffdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewAffdvtButton_Click(object sender, EventArgs e)
        {
            ////changing cursor type
            this.ClearAffDvtControls();
            this.Cursor = Cursors.WaitCursor;
            int statusId = 0;
            //// int the Generla header to default values
            this.SetGeneralHeaderFieldNewMode();
            this.ReceiptFormButton.Enabled = false;
            //// Assing to new mode
            this.affdvtButtonOperation = (int)ButtonOperation.New;

            this.currentAffidavitStatementId = 0;
            this.receiptIDExist = false;

            //// Set AffDvt Button
            this.SetAffDvtButton(ButtonOperation.New);

            //// Set All The Parcel and 
            this.SetAffiDvtButtonsOperation();

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

            this.SetEnableSatustParcelHeader(false);
            //// Diasable
            this.SetEnableStatusforPartiesControls(false);

            this.SetAffDvtControls(true);

            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));

            this.CalcuDueCommandButton.Enabled = true;

            this.additionalOperationSmartPart.Enabled = false;

            if (!this.affDvtTotal)
            {
                this.toolBoxSmartPart.Enabled = false;
            }

            this.recordNavigatorSmartPart.Enabled = false;
            this.treasurerStatus = statusId.ToString();
            this.assessorStatus = statusId.ToString();
            this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);

            ////changing cursor type
            this.Cursor = Cursors.Default;
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
            this.generalHeaderRow["PaymentDate"] = this.GeneralHeaderPaymentDate.Text.Trim();
            this.generalHeaderRow["FormDate"] = this.GeneralHeaderFormDate.Text.Trim();
            this.generalHeaderRow["District"] = this.GeneralLinkLabel.Text.Trim();
            if (string.Compare(this.GeneralSubmittedDate.Text.Trim(), "N/A") == 0)
            {
                this.generalHeaderRow["SubmittedDate"] = DBNull.Value;
            }
            else
            {
                this.generalHeaderRow["SubmittedDate"] = this.GeneralSubmittedDate.Text.Trim();
            }

            if (String.Equals(this.GeneralFromWeb.Text.Trim().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("NOValue")))
            {
                this.generalHeaderRow["PreDatesStmt"] = 0;
            }
            else
            {
                this.generalHeaderRow["PreDatesStmt"] = 1;
            }

            if (String.Compare(this.GeneralFromWeb.Text.Trim().ToUpperInvariant(), SharedFunctions.GetResourceString("NOValue")) == 0)
            {
                this.generalHeaderRow["PreDatesStmt"] = 0;
            }
            else
            {
                this.generalHeaderRow["PreDatesStmt"] = 1;
            }

            if (String.Equals(this.GeneralMobileHome.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                this.generalHeaderRow["IsMobileHome"] = 1;
            }
            else
            {
                this.generalHeaderRow["IsMobileHome"] = 0;
            }

            if (String.Equals(this.GeneralTaxCode.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("EXEMPTValue")))
            {
                this.generalHeaderRow["IsExempt"] = 1;
                this.taxCode = "Exempt";
            }
            else
            {
                this.generalHeaderRow["IsExempt"] = 0;
                this.taxCode = SharedFunctions.GetResourceString("TAXABLEValue");
            }

            this.generalHeaderRow["TotalDebt"] = this.GerneralTotalDebit.Text.Trim().Replace("$", "").Trim();
            if (string.IsNullOrEmpty(this.GeneralLocationCode.Text.Trim()))
            {
                this.generalHeaderRow["LocationCode"] = DBNull.Value;
            }
            else
            {
                this.generalHeaderRow["LocationCode"] = this.GeneralLocationCode.Text.Trim();
            }

            this.generalHeaderRow["ReceiptNumber"] = this.GeneralReceiptNo.Text.Trim();
            this.generalHeaderRow["ExciseRateID"] = this.exciseRateId;
            this.generalHeaderRow["DORNote"] = this.GeneralNote.Text.Trim();
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
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TaxableSalePriceColumn.ColumnName] = this.CalcDueTaxableSaleTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TechnologyFeeColumn.ColumnName] = this.CalcDueTechFeeTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TotalAmountDueColumn.ColumnName] = this.CalcDueTtlAmountTextBox.Text.Trim();
            calcDueHeaderRow[this.exciseTaxAffidavitDataSet.AmountDue.TransactionFeeColumn.ColumnName] = this.CalcDueTransFeeTextBox.Text.Trim();
            this.exciseTaxAffidavitDataSet.AmountDue.Rows.Add(calcDueHeaderRow);
            this.exciseTaxAffidavitDataSet.AmountDue.AcceptChanges();

            //// Row Affdivat

            DataRow affdvtRow;
            this.exciseTaxAffidavitDataSet.Affidavit.Rows.Clear();

            affdvtRow = this.exciseTaxAffidavitDataSet.Affidavit.NewRow();
            if (string.IsNullOrEmpty(this.AffDvtDocDateTextBox.Text.Trim()))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn.ColumnName] = DBNull.Value;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.DocumentDateColumn.ColumnName] = this.AffDvtDocDateTextBox.Text.Trim();
            }

            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.DocumentTypeColumn.ColumnName] = this.AffDvtDocumentTypeTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptionCodeColumn.ColumnName] = this.AffDvtExemptionCodeTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptionDescColumn.ColumnName] = this.AffDvtExemptionDescrTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.ExemptRegNumColumn.ColumnName] = this.AffdvtExemptRegNumberTextBox.Text.Trim();

            if (String.Equals(this.AffDvtContinuanceCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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

            if (String.Equals(this.AffDvtHistoryCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsHistoricColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidvtOpenSpaceComb.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsOpenSpaceColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidavitPartialSaleCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsPartialSaleColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidavitSegregatedComboBox.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.IsSegregatedColumn.ColumnName] = 0;
            }

            if (String.Equals(this.AffidavitStreetLocationCombo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 1;
            }
            else
            {
                affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationSaleColumn.ColumnName] = 0;
            }

            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.LocationNameColumn.ColumnName] = this.AffDvtLoactionTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.PersonalPropDescColumn.ColumnName] = this.AffDvtDescTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.StreetAddressColumn.ColumnName] = this.AffidavitStreetAddressTextBox.Text.Trim();
            affdvtRow[this.exciseTaxAffidavitDataSet.Affidavit.UseCodeColumn.ColumnName] = this.AffdvtUseCodeTextBox.Text.Trim();

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
            if (String.Equals(this.SuppliA1Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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
            if (String.Equals(this.SuppliA2Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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
            if (String.Equals(this.SuppliB1Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 1;
            }
            else
            {
                supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GiftNoConsideration_B1Column.ColumnName] = 0;
            }

            ////GiftNoConsideration_B2
            if (String.Equals(this.SuppliB2Combo.SelectedItem.ToString().ToUpperInvariant().Trim(), SharedFunctions.GetResourceString("YESValue")))
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

            /////Grantor’s Signature (Name)
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GrantorsSignatureColumn.ColumnName] = this.SuppliGranSignTextBox.Text.Trim();

            ////Grantee’s Signature
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteesSignatureColumn.ColumnName] = this.SuppliGranteSignTextBox.Text.Trim();

            ////Facilitator Name
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.FacilitatorNameColumn.ColumnName] = this.SuppliFNameTextBox.Text.Trim();
            ////Grantee Name
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.GranteeNameColumn.ColumnName] = this.SuppliGNameTextBox.Text.Trim();
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasurerColumn.ColumnName] = this.treasurerDesc;
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.TreasurerStatusIDColumn.ColumnName] = int.Parse(this.treasurerStatus);
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessorColumn.ColumnName] = this.assessorDesc;
            supplimentHeaderRow[this.exciseTaxAffidavitDataSet.Suppliment.AssessorStatusIDColumn.ColumnName] = int.Parse(this.assessorStatus);
            this.exciseTaxAffidavitDataSet.Suppliment.Rows.Add(supplimentHeaderRow);
            this.exciseTaxAffidavitDataSet.Suppliment.AcceptChanges();
        }

        /// <summary>
        /// SetAffDvtButton
        /// </summary>
        /// <param name="buttonOperation">The button operation.</param>
        private void SetAffDvtButton(ButtonOperation buttonOperation)
        {
            switch (buttonOperation)
            {
                case ButtonOperation.New:
                    {
                        this.AffdvtButtonOprNEw();
                        break;
                    }

                case ButtonOperation.Cancel:
                    {
                        this.AffdvtButtonOprCancel();
                        break;
                    }

                case ButtonOperation.Empty:
                    {
                        this.AffdvtButtonOprEmpty();
                        break;
                    }

                case ButtonOperation.Update:
                    {
                        this.AffdvtButtonOprUpdate();
                        break;
                    }

                case ButtonOperation.ReceiptidNotExist:
                    {
                        this.AffDvtButtonOprReceiptNotExist();
                        break;
                    }

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

                case ButtonOperation.Remove:
                    {
                        this.AffDvtButtonOprRemove();
                        break;
                    }
            }
        }

        /// <summary>
        /// Affs the DVT button opr remove.
        /// </summary>
        private void AffDvtButtonOprRemove()
        {
            this.NewAffdvtButton.Enabled = false;
            this.DeleteAffdvtButton.Enabled = false;
            this.SaveAffdvtButton.Enabled = true;
            this.CancelAffdvtButton.Enabled = true;
        }

        /// <summary>
        /// Affs the DVT button opr no record found.
        /// </summary>
        private void AffDvtButtonOprNoRecordFound()
        {
            ////this.NewAffdvtButton.Enabled = true;
            this.NewAffdvtButton.Enabled = this.PermissionFiled.newPermission;
            this.DeleteAffdvtButton.Enabled = false;
            this.SaveAffdvtButton.Enabled = false;
            this.CancelAffdvtButton.Enabled = false;
            this.CalcuDueCommandButton.Enabled = false;
            this.FilteredButton.Enabled = false;
            this.TreasurerStatusButton.Enabled = false;
            ////this.ExciseRatesButton.Enabled = false;
            this.ViewAfdvtButton.Enabled = false;
            this.AssessorStatusButton.Enabled = false;
        }

        /// <summary>
        /// Affdvts the button opr header part update.
        /// </summary>
        private void AffdvtButtonOprHeaderPartUpdate()
        {
            this.NewAffdvtButton.Enabled = false;
            this.DeleteAffdvtButton.Enabled = false;
            this.SaveAffdvtButton.Enabled = true;
            this.CancelAffdvtButton.Enabled = true;
            this.CalcuDueCommandButton.Enabled = true;
        }

        /// <summary>
        /// Affs the DVT button opr receipt not exist.
        /// </summary>
        private void AffDvtButtonOprReceiptNotExist()
        {
            if (this.statementExist)
            {
                this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
            }
            else
            {
                this.DeleteAffdvtButton.Enabled = false;
            }

            this.SaveAffdvtButton.Enabled = false;
            this.CancelAffdvtButton.Enabled = false;
            ////this.NewAffdvtButton.Enabled = true;
            this.NewAffdvtButton.Enabled = this.PermissionFiled.newPermission;
            this.CalcuDueCommandButton.Enabled = false;
            this.SetAffDvtControls(false);
        }

        /// <summary>
        /// Affdvts the button opr update.
        /// </summary>
        private void AffdvtButtonOprUpdate()
        {
            //// Checks To See If Parties Button or Parcel Buttton in New Mode
            if (this.parcelButtonOperation == (int)ButtonOperation.New || this.partyHeaderButtonOperation == (int)ButtonOperation.New)
            {
                this.NewAffdvtButton.Enabled = false;
                this.SaveAffdvtButton.Enabled = true;
                this.CancelAffdvtButton.Enabled = true;
                this.DeleteAffdvtButton.Enabled = false;
            }
            else if (this.affdvtButtonOperation == (int)ButtonOperation.Update)
            {
                this.NewAffdvtButton.Enabled = false;
                this.SaveAffdvtButton.Enabled = true;
                this.CancelAffdvtButton.Enabled = true;
                this.DeleteAffdvtButton.Enabled = false;
                ////  this.toolBoxSmartPart.Enabled = false;
                this.ReceiptFormButton.Enabled = false;
            }
            else if (this.parcelButtonOperation == (int)ButtonOperation.Update)
            {
                this.NewAffdvtButton.Enabled = false;
                this.SaveAffdvtButton.Enabled = true;
                this.CancelAffdvtButton.Enabled = true;
                this.DeleteAffdvtButton.Enabled = false;
            }
            else
            {
                this.NewAffdvtButton.Enabled = this.PermissionFiled.newPermission;
                ////this.NewAffdvtButton.Enabled = true;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = true;
                if (this.statementExist)
                {
                    this.DeleteAffdvtButton.Enabled = true;
                }
                else
                {
                    this.DeleteAffdvtButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Affdvts the button opr empty.
        /// </summary>
        private void AffdvtButtonOprEmpty()
        {
            if (this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                this.NewAffdvtButton.Enabled = this.PermissionFiled.newPermission;
                ////this.NewAffdvtButton.Enabled = true;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = false;
                this.CalcuDueCommandButton.Enabled = true;
                this.FilteredButton.Enabled = true;
                //// this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;
                if (this.receiptIDExist)
                {
                    this.DeleteAffdvtButton.Enabled = false;
                    this.SetAffDvtControls(false);
                }
                else
                {
                    this.SetAffDvtControls(true);
                    this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
                    this.additionalOperationSmartPart.Enabled = true;
                }
            }
            else if (this.PermissionFiled.newPermission)
            {
                this.NewAffdvtButton.Enabled = true;
                ////this.NewAffdvtButton.Enabled = true;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = false;
                this.SetAffDvtControls(false);
                this.CalcuDueCommandButton.Enabled = true;
                this.FilteredButton.Enabled = true;
                this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;
                if (this.receiptIDExist)
                {
                    this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
                }
                else
                {
                    this.DeleteAffdvtButton.Enabled = false;
                }
            }
            else if (this.PermissionFiled.editPermission)
            {
                this.NewAffdvtButton.Enabled = false;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = false;
                this.SetAffDvtControls(true);
                this.CalcuDueCommandButton.Enabled = true;
                this.FilteredButton.Enabled = true;
                this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;
                if (this.receiptIDExist)
                {
                    this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
                }
                else
                {
                    this.DeleteAffdvtButton.Enabled = false;
                }
            }
            else if (this.PermissionFiled.deletePermission)
            {
                this.NewAffdvtButton.Enabled = false;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = false;
                this.SetAffDvtControls(false);
                this.CalcuDueCommandButton.Enabled = true;
                this.FilteredButton.Enabled = true;
                this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;
                if (this.receiptIDExist)
                {
                    this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
                }
                else
                {
                    this.DeleteAffdvtButton.Enabled = true;
                }
            }
            else
            {
                this.NewAffdvtButton.Enabled = false;
                ////this.NewAffdvtButton.Enabled = true;
                this.SaveAffdvtButton.Enabled = false;
                this.CancelAffdvtButton.Enabled = false;
                this.SetAffDvtControls(false);
                this.CalcuDueCommandButton.Enabled = true;
                this.FilteredButton.Enabled = true;
                this.ExciseRatesButton.Enabled = true;
                this.ViewAfdvtButton.Enabled = true;
                if (this.receiptIDExist)
                {
                    this.DeleteAffdvtButton.Enabled = this.PermissionFiled.deletePermission;
                }
                else
                {
                    this.DeleteAffdvtButton.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Affdvts the button opr cancel.
        /// </summary>
        private void AffdvtButtonOprCancel()
        {
            ////this.NewAffdvtButton.Enabled = true;
            this.NewAffdvtButton.Enabled = this.PermissionFiled.newPermission;
            this.SaveAffdvtButton.Enabled = false;
            this.CancelAffdvtButton.Enabled = false;
            this.DeleteAffdvtButton.Enabled = true;
        }

        /// <summary>
        /// Affdvts the button opr N ew.
        /// </summary>
        private void AffdvtButtonOprNEw()
        {
            this.NewAffdvtButton.Enabled = false;
            this.SaveAffdvtButton.Enabled = true;
            this.CancelAffdvtButton.Enabled = true;
            this.DeleteAffdvtButton.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the CancelAffdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelAffdvtButton_Click(object sender, EventArgs e)
        {
            // this.currentAffidavitStatementId = -1;
            this.Cursor = Cursors.WaitCursor;
            this.affdvtButtonOperation = (int)ButtonOperation.Empty;

            this.SetAffDvtButton(ButtonOperation.Empty);
            this.FillGeneralHeaderText(0);
            this.SetPartiesGridButtons(ButtonOperation.Empty);
            this.SetParcelGridButtons(ButtonOperation.Empty);
            this.parcelButtonOperation = (int)ButtonOperation.Empty;
            this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
            this.LoadExciseTaxAffidavit(null, null);
            this.parcelHeaderKeyPressed = false;
            this.partiesHeaderkeyPressed = false;
            this.partiesRemoved = false;
            this.headerChanges = false;
            this.selectedPartyGridRowId = 0;
            this.SetPartiesText(this.selectedPartyGridRowId);
            this.parcelHeaderKeyPressed = false;
            this.partiesHeaderkeyPressed = false;
            this.affDvtKeypressed = false;
            this.SetCaluDueButtonsBGColor();
            this.affdvtRemove = false;
            //// this.additionalOperationSmartPart.Enabled = true;

            this.SetPaymentDateTextBox();
            if (this.affDvtTotal)
            {
                this.TreasurerStatusButton.Enabled = this.PermissionFiled.editPermission;
                this.AssessorStatusButton.Enabled = this.PermissionFiled.editPermission;
                this.recordNavigatorSmartPart.Enabled = true;
                this.toolBoxSmartPart.Enabled = true;
                this.additionalOperationSmartPart.Enabled = true;
            }
            else
            {
                this.TreasurerStatusButton.Enabled = false;
                this.AssessorStatusButton.Enabled = false;
                this.recordNavigatorSmartPart.Enabled = false;
                this.toolBoxSmartPart.Enabled = false;
                this.additionalOperationSmartPart.Enabled = false;
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Sets the color of the calu due buttons BG.
        /// </summary>
        private void SetCaluDueButtonsBGColor()
        {
            this.GeneralHeaderPaymentDate.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.PaymentDatePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.AffDvtDocDateTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.AffDvtDatePanle.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.CalcDueTaxableSaleTextBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.TaxableSalePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralLinkLablePanel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.GeneralLinkLabel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
        }
        #endregion

        #region  Calender Events

        /// <summary>
        /// Handles the KeyDown event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.GeneralHeaderPaymentDate.Text = this.PaymentDateCalender.SelectionStart.ToShortDateString();
                this.GeneralHeaderPaymentDate.Focus();
                this.affDvtKeypressed = true;
                this.PaymentDateCalender.Visible = false;
                if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffiDvtToUpdateMode();
                }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.GeneralHeaderFormDate.Text = this.FormDateCalender.SelectionStart.ToShortDateString();
                this.affDvtKeypressed = true;
                this.FormDateCalender.Visible = false;
                this.GeneralHeaderFormDate.Focus();
                if (this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffiDvtToUpdateMode();
                }
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            ///// Assign the selected date to the DateTextbox.
            this.SuppliInstDateTextBox.Text = e.Start.ToShortDateString();
            this.SuppliMonthCalendar.Visible = false;
            this.SuppliInstDateTextBox.Focus();
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.SuppliInstDateTextBox.Text = this.SuppliMonthCalendar.SelectionStart.ToShortDateString();
                this.affDvtKeypressed = true;
                this.SuppliMonthCalendar.Visible = false;
                if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffiDvtToUpdateMode();
                }

                this.SuppliInstDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AffDvtMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffDvtMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                this.AffDvtDocDateTextBox.Text = this.AffDvtMonthCalendar.SelectionStart.ToShortDateString();
                this.affDvtKeypressed = true;
                this.AffDvtMonthCalendar.Visible = false;
                if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                {
                    this.affdvtButtonOperation = (int)ButtonOperation.Update;
                    this.SetAffiDvtToUpdateMode();
                }

                this.SetAffiDvtToUpdateMode();
                this.AffDvtDocDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.SetGeneralHeaderPaymentDate(e.Start.ToShortDateString());
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the DateChanged event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_DateChanged(object sender, DateRangeEventArgs e)
        {
            this.SetGeneralHeaderFormDate(e.Start.ToShortDateString());
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Sets the general header payment date.
        /// </summary>
        /// <param name="dateValeu">The date valeu.</param>
        private void SetGeneralHeaderPaymentDate(string dateValeu)
        {
            // //Assign the selected date to the DateTextbox.
            this.GeneralHeaderPaymentDate.Text = dateValeu;
            this.PaymentDateCalender.Visible = false;
            this.GeneralHeaderPaymentDate.Focus();
        }

        /// <summary>
        /// Sets the general header form date.
        /// </summary>
        /// <param name="formHeaderDate">The form header date.</param>
        private void SetGeneralHeaderFormDate(string formHeaderDate)
        {
            //// Assign the selected date to the DateTextbox.
            this.GeneralHeaderFormDate.Text = formHeaderDate;
            this.FormDateCalender.Visible = false;
            this.GeneralHeaderFormDate.Focus();
            ////SetPanelPosition(this.GeneralHeaderPanel);
        }

        /// <summary>
        /// Queries the by form mode change.
        /// </summary>
        /// <param name="e">The e.</param>
        private void QueryByFormModeChange(DataEventArgs<Button> e)
        {
            switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes:
                    {
                        if (this.partiesHeaderkeyPressed == true || this.partyHeaderButtonOperation == (int)ButtonOperation.New)
                        {
                            this.SavePartiesHeader();
                        }
                        else
                        {
                            this.affDvtKeypressed = true;
                            this.partySave = true;
                        }

                        if (this.partySave)
                        {
                            //// Checks For Any Changes in  Parcel
                            if (this.parcelHeaderKeyPressed == true || this.parcelButtonOperation == (int)ButtonOperation.New)
                            {
                                this.SaveParcelHeader();
                            }
                            else
                            {
                                this.affDvtKeypressed = true;
                                this.parcelSave = true;
                            }
                        }

                        if (this.parcelSave && this.partySave)
                        {
                            //// if parties saved goes to newxt
                            if (this.affDvtKeypressed == true || this.affdvtButtonOperation == (int)ButtonOperation.New)
                            {
                                this.SaveAffDvt();
                            }
                        }
                        else
                        {
                            this.affDvtSave = false;
                        }

                        if (this.affDvtSave)
                        {
                            this.parcelButtonOperation = (int)ButtonOperation.Empty;
                            this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                            this.affdvtRemove = false;
                            this.affDvtKeypressed = false;
                            this.partiesHeaderkeyPressed = false;
                            this.parcelHeaderKeyPressed = false;
                            this.headerChanges = false;
                            //// RecordNavigationEntity recordNavigationEntity = DataEventArgs < RecordNavigationEntity > e.Data;
                            /////  this.currentAffidavitStatementId = this.RetrieveStatementId(recordNavigationEntity.RecordIndex);
                            this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                            this.QueryUtilityFunction(this, new DataEventArgs<Button>(e.Data));
                        }

                        break;
                    }

                case DialogResult.No:
                    {
                        this.parcelButtonOperation = (int)ButtonOperation.Empty;
                        this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                        this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                        this.affdvtRemove = false;
                        this.affDvtKeypressed = false;
                        this.partiesHeaderkeyPressed = false;
                        this.parcelHeaderKeyPressed = false;
                        this.headerChanges = false;
                        this.QueryUtilityFunction(this, new DataEventArgs<Button>(e.Data));
                        break;
                    }

                case DialogResult.Cancel:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            ////this.SetGeneralHeaderFormDate(e.Start.ToShortDateString()); 
            this.SetGeneralHeaderFormDate(e.Start.ToShortDateString());
            if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
            {
                this.affdvtButtonOperation = (int)ButtonOperation.Update;
                this.SetAffiDvtToUpdateMode();
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteAffdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteAffdvtButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteAffDvt"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(this.StatementIDTextBox.Text.Trim()))
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        this.form1105Control.WorkItem.DeleteAffidavitDetails(int.Parse(this.StatementIDTextBox.Text.Trim()), TerraScanCommon.UserId);
                        this.parcelHeaderKeyPressed = false;
                        this.partiesHeaderkeyPressed = false;
                        this.affDvtKeypressed = false;
                        this.affdvtButtonOperation = (int)ButtonOperation.Empty;
                        this.partyHeaderButtonOperation = (int)ButtonOperation.Empty;
                        this.parcelButtonOperation = (int)ButtonOperation.Empty;

                        if (this.recordNavigationEntityAfdvt != null && this.recordNavigationEntityAfdvt.RecordIndex > 0)
                        {
                            if (this.affDvtRecordCount > this.recordNavigationEntityAfdvt.RecordIndex)
                            {
                                this.recordNavigationEntityAfdvt.RecordIndex = this.recordNavigationEntityAfdvt.RecordIndex + 1;
                            }
                            else if (this.affDvtRecordCount <= this.recordNavigationEntityAfdvt.RecordIndex && this.affDvtRecordCount > 0)
                            {
                                this.recordNavigationEntityAfdvt.RecordIndex = this.recordNavigationEntityAfdvt.RecordIndex - 1;
                            }

                            this.currentAffidavitStatementId = this.RetrieveStatementId(this.recordNavigationEntityAfdvt.RecordIndex);
                            this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
                        }
                        else
                        {
                            this.LoadExciseTaxAffidavit(null, null);
                        }

                        ////this.GetStatementId();
                        //// this.LoadExciseTaxAffidavit(null, this.currentAffidavitStatementId);
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
            }
        }

        /// <summary>
        /// Handles the Leave event of the PaymentDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentDateCalender_Leave(object sender, EventArgs e)
        {
            this.PaymentDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Leave event of the FormDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormDateCalender_Leave(object sender, EventArgs e)
        {
            this.FormDateCalender.Visible = false;
        }

        /// <summary>
        /// Handles the Leave event of the SuppliMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SuppliMonthCalendar_Leave(object sender, EventArgs e)
        {
            this.SuppliMonthCalendar.Visible = false;
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the F1105 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1105_Click(object sender, EventArgs e)
        {
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Handles the KeyDown event of the GeneralNote control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffDvt_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the UpdateParties control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UpdateParties_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        this.SetPartieHeaderToUpdateMode();
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void UpdateParcel_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    {
                        this.SetParcelHeaderToUpdateMode();
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the GeneralNote control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GeneralNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }

                default:
                    {
                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GeneralNote control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GeneralNote_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        break;
                    }

                default:
                    {
                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the Enter event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_Enter(object sender, EventArgs e)
        {
            if (this.parcelPictureBox.Enabled)
            {
                this.parcelPictureBox.BackColor = this.ParcelNumberTextBox.BackColor;
            }
        }

        /// <summary>
        /// Handles the Leave event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_Leave(object sender, EventArgs e)
        {
            if (this.parcelPictureBox.Enabled)
            {
                this.parcelPictureBox.BackColor = this.ParcelNumberTextBox.BackColor;
            }
        }

        /// <summary>
        /// Handles the Scroll event of the MainPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void MainPanel_Scroll(object sender, ScrollEventArgs e)
        {
            this.SetCalenderInvisible();
        }

        /// <summary>
        /// Handles the MouseMove event of the AffDvtDocumentTypeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void AffDvtDocumentTypeTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.AffDvtDocumentTypeTextBox.Text.Trim().Length > 14)
            {
                this.AffDvtToolTip.SetToolTip(this.AffDvtDocumentTypeTextBox, this.AffDvtDocumentTypeTextBox.Text);
            }
            else
            {
                this.AffDvtToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the AffDvtDescTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AffDvtDescTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        break;
                    }

                default:
                    {
                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the AffDvtDescTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AffDvtDescTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    {
                        e.Handled = true;
                        break;
                    }

                default:
                    {
                        if (this.PermissionFiled.editPermission && this.affdvtButtonOperation != (int)ButtonOperation.New && (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm)))
                        {
                            this.affdvtButtonOperation = (int)ButtonOperation.Update;
                            this.SetAffiDvtToUpdateMode();
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the PartiesDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void PartiesDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.PartiesDataGridView.OriginalRowCount == 0)
            {
                this.PartiesDataGridView.CurrentCell = null;
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ParcelHeaderDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.ParcelHeaderDataGridView.OriginalRowCount == 0)
            {
                this.ParcelHeaderDataGridView.CurrentCell = null;
            }
        }
    }
}
