//--------------------------------------------------------------------------------------------
// <copyright file="F36011.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36011.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		         Description
// ----------		---------		     ---------------------------------------------------------
// 28/6/07          M.Vijayakumar        Created
// 26/03/09         M.Sadha Shivudu      Implemented TSCO# 5176
// 21/04/09         M.Sadha Shivudu      Implemented TSCO# 6281 
// 14/05/2009       A.Shanmuga Sundaram  Implemented TSCO# 7264
// 21/06/2011       P.Manoj Kumar        Implemented TSCO# 11442 
// 5/5/2017         Dhineshkumar.J       Implemented TSCO# 21837    
//***********************************************************************************************/

namespace D36010
{
    using System;
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
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infrastructure.Interface;
    using TerraScan.Helper;

    /// <summary>
    /// F36011 Class file 
    /// </summary>
    public partial class F36011 : BaseSmartPart
    {
        #region variables

        /// <summary>
        /// saveComplete
        /// </summary>
        private bool saveUncomplete;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool unsavedChangeExists;

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;



        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// Used to store the currentMiscMid
        /// </summary>
        private int currentMiscMid;

        /// <summary>
        /// Used to store the currentMICodeId
        /// </summary>
        private int currentMICodeId;

        /// <summary>
        /// Usde to check whether the key is is vaild are not
        /// </summary>
        private int validkeyId;

        /// <summary>
        /// controller F36011
        /// </summary>
        private F36011Controller form36011Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Usde to store the listMIcodeDatatableRowsCount
        /// </summary>
        private int listMIcodeDatatableRowsCount;

        /// <summary>
        /// used to store the current form number
        /// </summary>
        private int currentFormId;

        /// <summary>
        /// Usde to store the miscGridRowId
        /// </summary>
        private int miscGridRowId;

        /// <summary>
        /// used to keep trakc of user
        /// </summary>
        private BindingSource miscImprovementsGridSource = new BindingSource();

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// Used to store the current application roll year
        /// </summary>
        private int currentApplicationRollyear;

        /// <summary>
        /// Used to store the currentAge
        /// </summary>
        private int currentAge;

        private bool isCheckedPrimary=false;

        /// <summary>
        /// Used to store the miscImprovementOverviewData
        /// </summary>
        private F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();

        /// <summary>
        /// Used to store the listDeprTable Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListDeprTableDataTable listDeprTable = new F36011MiscImprovementOverviewData.ListDeprTableDataTable();

        /// <summary>
        /// Used to store the listMIcodeDatatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMICodeNewDataTable listMIcodeDatatable = new F36011MiscImprovementOverviewData.ListMICodeNewDataTable();

        /// <summary>
        /// Used to store the Datatable for Code Combo Box
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMICodeComboboxDatatableDataTable listMICatalogCodeComboboxDatatable = new F36011MiscImprovementOverviewData.ListMICodeComboboxDatatableDataTable();

        ///// <summary>
        ///// Used to store the listMiscImprovementsDataTable
        ///// </summary>
        ////private F36011MiscImprovementOverviewData.ListMiscImprovementsDataTable listMiscImprovementsDataTable = new F36011MiscImprovementOverviewData.ListMiscImprovementsDataTable();

        /// <summary>
        /// Used to store the listMiscImprovementsDataTable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscImprovementsNewDataTable listMiscImprovementsDataTable = new F36011MiscImprovementOverviewData.ListMiscImprovementsNewDataTable();

        #region Depr ComboBox Tables

        /// <summary>
        /// Used to store the Datatable for Depr Quality Res Combo Box
        /// </summary>
        private F36011MiscImprovementOverviewData.ListQualityResDataTable listDeprQualityResComboboxDatatable = new F36011MiscImprovementOverviewData.ListQualityResDataTable();

        /// <summary>
        /// Used to store the Datatable for Depr Functional Defined Category Combo Box
        /// </summary>
        private F36011MiscImprovementOverviewData.ListDeprFuncCategoryDataTable listDeprFuncDefinedComboboxDatatable = new F36011MiscImprovementOverviewData.ListDeprFuncCategoryDataTable();

        /// <summary>
        /// Used to store the Datatable for Depr Condition Combo Box
        /// </summary>
        private F36011MiscImprovementOverviewData.ListConditionDataTable listDeprConditionComboboxDatatable = new F36011MiscImprovementOverviewData.ListConditionDataTable();

        #endregion Depr ComboBox Tables

        #region MI Labels Fields Formula

        /// <summary>
        /// used to store the miscfieldsFormula1
        /// </summary>
        private string miscfieldsFormula1 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula2
        /// </summary>
        private string miscfieldsFormula2 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula3
        /// </summary>
        private string miscfieldsFormula3 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula4
        /// </summary>
        private string miscfieldsFormula4 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula5
        /// </summary>
        private string miscfieldsFormula5 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula6
        /// </summary>
        private string miscfieldsFormula6 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula7
        /// </summary>
        private string miscfieldsFormula7 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula8
        /// </summary>
        private string miscfieldsFormula8 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula9
        /// </summary>
        private string miscfieldsFormula9 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula10
        /// </summary>
        private string miscfieldsFormula10 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula10
        /// </summary>
        private string miscfieldsFormula11 = string.Empty;

        /// <summary>
        /// used to store the miscfieldsFormula10
        /// </summary>
        private string miscfieldsFormula12 = string.Empty;

        /// <summary>
        /// Store Quality value
        /// </summary>
        private string miscfieldsFormulaQuality = string.Empty;

        /// <summary>
        /// Store condition value
        /// </summary>
        private string miscfieldsFormula1Condition = string.Empty;

        /// <summary>
        /// Store Description Column Value
        /// </summary>
        private string strDesecription = string.Empty;

        /// <summary>
        /// Used to store the vformula
        /// </summary>
        private string vformula = string.Empty;

        #endregion MI Labels Fields Formula

        #region MI Fileds Decimal Places

        /// <summary>
        /// mifields1DecimalPlaces
        /// </summary>
        private int mifields1DecimalPlaces;

        /// <summary>
        /// mifields2DecimalPlaces
        /// </summary>
        private int mifields2DecimalPlaces;

        /// <summary>
        /// mifields3DecimalPlaces
        /// </summary>
        private int mifields3DecimalPlaces;

        /// <summary>
        /// mifields4DecimalPlaces
        /// </summary>
        private int mifields4DecimalPlaces;

        /// <summary>
        /// mifields5DecimalPlaces
        /// </summary>
        private int mifields5DecimalPlaces;

        /// <summary>
        /// mifields6DecimalPlaces
        /// </summary>
        private int mifields6DecimalPlaces;

        /// <summary>
        /// mifields7DecimalPlaces
        /// </summary>
        private int mifields7DecimalPlaces;

        /// <summary>
        /// mifields8DecimalPlaces
        /// </summary>
        private int mifields8DecimalPlaces;

        /// <summary>
        /// mifields9DecimalPlaces
        /// </summary>
        private int mifields9DecimalPlaces;

        /// <summary>
        /// mifields10DecimalPlaces
        /// </summary>
        private int mifields10DecimalPlaces;

        /// <summary>
        /// mifields10DecimalPlaces
        /// </summary>
        private int mifields11DecimalPlaces;

        /// <summary>
        /// mifields10DecimalPlaces
        /// </summary>
        private int mifields12DecimalPlaces;

        #endregion MI Fileds Decimal Places

        #region MI Fields Combo Values

        /// <summary>
        /// variable to hold the C01 value
        /// </summary>
        private short mifieldsC01Value;

        /// <summary>
        /// variable to hold the C02 value
        /// </summary>
        private short mifieldsC02Value;

        /// <summary>
        /// variable to hold the C03 value
        /// </summary>
        private short mifieldsC03Value;

        /// <summary>
        /// variable to hold the C04 value
        /// </summary>
        private short mifieldsC04Value;

        /// <summary>
        /// variable to hold the C05 value
        /// </summary>
        private short mifieldsC05Value;

        /// <summary>
        /// variable to hold the C06 value
        /// </summary>
        private short mifieldsC06Value;

        /// <summary>
        /// variable to hold the C07 value
        /// </summary>
        private short mifieldsC07Value;

        /// <summary>
        /// variable to hold the C08 value
        /// </summary>
        private short mifieldsC08Value;

        /// <summary>
        /// variable to hold the C09 value
        /// </summary>
        private short mifieldsC09Value;

        /// <summary>
        /// variable to hold the C10 value
        /// </summary>
        private short mifieldsC10Value;

        /// <summary>
        /// variable to hold the C11 value
        /// </summary>
        private short mifieldsC11Value;

        /// <summary>
        /// variable to hold the C12 value
        /// </summary>
        private short mifieldsC12Value;

        #endregion MI Fields Combo Values

        #region MI Fields Combo DataTables

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 1 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable1 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 2 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable2 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 3 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable3 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 4 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable4 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 5 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable5 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 6 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable6 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 7 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable7 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 8 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable8 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 9 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable9 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 10 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable10 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 11 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable11 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        /// <summary>
        /// Used to store the listMiscCatalogChoice Combo 12 Datatable
        /// </summary>
        private F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable listMiscCatalogChoiceComboTable12 = new F36011MiscImprovementOverviewData.ListMiscCatalogChoiceDataTable();

        #endregion MI Fields Combo DataTables

        #region MI Catolog Choice ItemValues

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue01
        /// </summary>
        private decimal miscCatalogChoiceItemValue01;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue02
        /// </summary>
        private decimal miscCatalogChoiceItemValue02;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue03
        /// </summary>
        private decimal miscCatalogChoiceItemValue03;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue04
        /// </summary>
        private decimal miscCatalogChoiceItemValue04;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue05
        /// </summary>
        private decimal miscCatalogChoiceItemValue05;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue06
        /// </summary>
        private decimal miscCatalogChoiceItemValue06;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue07
        /// </summary>
        private decimal miscCatalogChoiceItemValue07;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue08
        /// </summary>
        private decimal miscCatalogChoiceItemValue08;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue09
        /// </summary>
        private decimal miscCatalogChoiceItemValue09;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue10
        /// </summary>
        private decimal miscCatalogChoiceItemValue10;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue11
        /// </summary>
        private decimal miscCatalogChoiceItemValue11;

        /// <summary>
        /// used to store the miscCatalogChoiceItemValue12
        /// </summary>
        private decimal miscCatalogChoiceItemValue12;

        #endregion MI Catolog Choice ItemValues

        /// <summary>
        /// To store MiscCode
        /// </summary>
        private int tempMiscCodeId;

        ///<summary>
        /// Used to flag Text Changed event
        /// </summary>
        private bool isTextChange;

        ///<summary>
        /// Used to hold the PolygonId
        /// </summary>
        private int PolygonID;
        ///<summary>
        /// used to flag MI text Changed
        /// </summary>
        private bool isMiTextChange;

        ///<summary>
        /// used to flage Override
        /// </summary>
        private bool isOverride;

        ///<summary>
        /// used to flag text change
        /// </summary>
        private bool isComboTextChange = false;

        #endregion variables

        #region Form Slice Variables

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #region Constructor

        /// <summary>
        /// F36011
        /// </summary>
        public F36011()
        {
            InitializeComponent();
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
        public F36011(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.currentFormId = formNo;

            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.valueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.MiscImprovementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscImprovementPictureBox.Height, this.MiscImprovementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

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




        ///// <summary>
        ///// Published event for FormClose
        ///// </summary>
        //[EventPublication(EventTopics.D9001_BaseSmartPart_formClose, PublicationScope.Global)]
        //public event EventHandler<DataEventArgs<string>> FormClose;



        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F36011Control
        /// </summary>
        [CreateNew]
        public F36011Controller Form36011Control
        {
            get { return this.form36011Control as F36011Controller; }
            set { this.form36011Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.MiscImprovementPictureBox.Height;
                sliceResize.SliceFormHeight = this.MiscImprovementPictureBox.Height + 2;
                this.MiscImprovementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscImprovementPictureBox.Height, this.MiscImprovementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.validkeyId > 0)
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

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.valueSliceId = eventArgs.Data.SelectedKeyId;
                this.LockControls(true);
                this.ControlLock(false);
                this.LoadEntireMiscImprovement();
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                if (!this.flagLoadOnProcess)
                {
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.MiscImprovementPictureBox.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.MiscImprovementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscImprovementPictureBox.Height, this.MiscImprovementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                }
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
                eventArgs.Data.FlagFormClose = this.CheckPageStatus();
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
            if (this.PermissionFiled.deletePermission && this.valueSliceId > 0)
            {
                this.form36011Control.WorkItem.F35001_DeleteValueSlice(this.valueSliceId, TerraScan.Common.TerraScanCommon.UserId);
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
            ////here form master save is not used but we are using this Event subscription to update the value slice header form slice
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            ////here save is only used to update the description of the Value slice header
            ////pls check with the this.CheckErrors(eventArgs.Data) method 
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.PermissionEdit)
            {
                this.btnCopyOrMove.Enabled = true;
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
            this.CancelMiscImprovementsClick();
        }

        ///// <summary>
        ///// Forms the close.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //[EventSubscription(EventTopics.D9001_BaseSmartPart_formClose, Thread = ThreadOption.UserInterface)]
        //public void FormClosed(object sender, FormClosingEventArgs e)
        //{

        //    this.FormClose(this, new DataEventArgs<string>("UserClosing"));
        //     e.Cancel = true;


        //}

        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }


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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            ////when the update button is enabled alert the user with message
            if (this.UpdateButton.Enabled)
            {
                DialogResult currentResult = MessageBox.Show(SharedFunctions.GetResourceString("F36011validationMessage1"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (DialogResult.Yes == currentResult)
                {
                    this.saveUncomplete = false;
                    this.SaveButtonClick();

                    ////Check save is complete or not 
                    if (this.saveUncomplete)
                    {
                        sliceValidationFields.DisableNewMethod = true;
                    }
                    else
                    {
                        ////when save is completed then the value slice header is alerted automatically.                      
                        sliceValidationFields.DisableNewMethod = false;
                    }

                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                else if (DialogResult.No == currentResult)
                {
                    this.CancelMiscImprovementsClick();
                    ////if no is seleted then alert the value slice header part
                    this.AlertValueSliceHeader();

                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                else if (DialogResult.Cancel == currentResult)
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
            }
            else
            {
                ////alert the value slice header
                this.AlertValueSliceHeader();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>        
        /// <returns>true - for continuing/false - leave unsaved changes</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            ////used to check whether save is complete
            this.saveUncomplete = false;

            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("F36011FormName"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.SaveButtonClick();
                    ////when save not complete
                    return !this.saveUncomplete;
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelMiscImprovementsClick();

                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads the entire misc improvement.
        /// </summary>
        private void LoadEntireMiscImprovement()
        {
            this.flagLoadOnProcess = true;
            this.isOverride = true;
            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LoadDeprQualityResComboBox();
            this.LoadDeprConditionComboBox();
            this.LoadDeprFunctionalCategoryComboBox();
            this.LoadMiscImprovementsGridView();
            this.flagLoadOnProcess = false;

            if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
            {
                int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
            }
        }

        /// <summary>
        /// To Set the Max Length
        /// </summary>
        private void SetMaxLength()
        {
            this.CodeComboBox.MaxLength = this.listMICatalogCodeComboboxDatatable.MICodeColumn.MaxLength;
            this.DescriptionTextBox.MaxLength = this.listMiscImprovementsDataTable.DescriptionColumn.MaxLength;
        }

        /// <summary>
        /// Custimizes the misc improvements grid view.
        /// </summary>
        private void CustimizeMiscImprovementsGridView()
        {
            this.MiscImprovementsGridView.AutoGenerateColumns = false;

            this.MiscCode.DataPropertyName = this.listMIcodeDatatable.MICodeColumn.ColumnName;
            this.MiscDescription.DataPropertyName = this.listMIcodeDatatable.DescriptionColumn.ColumnName;
            this.MiscCalcValue.DataPropertyName = this.listMIcodeDatatable.ValueBaseColumn.ColumnName;
            this.MiscDepr.DataPropertyName = this.listMIcodeDatatable.PhysDeprColumn.ColumnName;
            this.MiscOverride.DataPropertyName = this.listMIcodeDatatable.ValueOverideColumn.ColumnName;
            this.MiscValue.DataPropertyName = this.listMIcodeDatatable.TotalValueColumn.ColumnName;
            this.MiscMID.DataPropertyName = this.listMIcodeDatatable.MIDColumn.ColumnName;
            this.MiscMICode.DataPropertyName = this.listMIcodeDatatable.MICodeColumn.ColumnName;
            this.MICodeID.DataPropertyName = this.listMIcodeDatatable.MICodeIDColumn.ColumnName;
            this.IsMiscConfigured.DataPropertyName = this.listMIcodeDatatable.IsMiscConfiguredColumn.ColumnName;
            this.MiscImprovementsGridView.EnableBinding = false;

            this.MiscCode.DisplayIndex = 0;
            this.MiscDescription.DisplayIndex = 1;
            this.MiscCalcValue.DisplayIndex = 2;
            this.MiscDepr.DisplayIndex = 3;
            this.MiscOverride.DisplayIndex = 4;
            this.MiscValue.DisplayIndex = 5;
            this.MiscMID.DisplayIndex = 6;
            this.MiscMICode.DisplayIndex = 7;
            this.MICodeID.DisplayIndex = 8;

            this.MiscImprovementsGridView.PrimaryKeyColumnName = this.listMIcodeDatatable.MIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads the misc improvements grid view.
        /// </summary>
        private void LoadMiscImprovementsGridView()
        {
            ////datatable used to bind the code combo box
            this.listMICatalogCodeComboboxDatatable.Clear();
            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListCatalogCode(this.valueSliceId);
            this.listMICatalogCodeComboboxDatatable = this.miscImprovementOverviewData.ListMICodeComboboxDatatable;
            ////to bind to the Code combo box
            this.CodeComboBox.DataSource = this.listMICatalogCodeComboboxDatatable;

            this.MiscImprovementsGridView.ClearSorting();
            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListMiscCode(this.valueSliceId);

            ////datatable used to bind the Grid                
            this.listMIcodeDatatable.Clear();
            this.listMIcodeDatatable = this.miscImprovementOverviewData.ListMICodeNew;
            int.TryParse(this.miscImprovementOverviewData.ListValidValueSliceID.Rows[0][this.miscImprovementOverviewData.ListValidValueSliceID.ValueSliceIDColumn.ColumnName].ToString(), out this.validkeyId);
            this.listMIcodeDatatableRowsCount = this.miscImprovementOverviewData.ListMICodeNew.Rows.Count;

            if (this.listMIcodeDatatableRowsCount > 0)
            {
                this.MiscImprovementsGridView.Enabled = true;

                if (this.listMIcodeDatatable.Rows.Count > 4)
                {
                    this.MiscImprovementsGridView.NumRowsVisible = this.listMIcodeDatatable.Rows.Count;
                }
                else
                {
                    this.MiscImprovementsGridView.NumRowsVisible = 4;
                }

                this.MiscImprovementsGridView.DataSource = this.listMIcodeDatatable.DefaultView;

                this.miscImprovementsGridSource.DataSource = this.listMIcodeDatatable.DefaultView;
                TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, 0);
                this.AssignMiscellaneousCommponents(0);
                ////lock the attachmenntComment panel controls
                this.AttachmentandCommentPanel.Enabled = true;
            }
            else
            {
                ////when there is no record set the grid num rows visable to 4
                this.MiscImprovementsGridView.NumRowsVisible = 4;
                this.ClearMiscImprovementsGridView();
                this.ClearMiscImprovementsControls();
                this.LockControls(false);
                ////lock the attachmenntComment panel controls
                this.AttachmentandCommentPanel.Enabled = false;
            }

            this.SetSmartPartHeight(this.MiscImprovementsGridView.OriginalRowCount);
        }

        /// <summary>
        /// Clears the misc improvements grid view.
        /// </summary>
        private void ClearMiscImprovementsGridView()
        {
            this.listMIcodeDatatable.Clear();
            this.MiscImprovementsGridView.DataSource = this.listMIcodeDatatable.DefaultView;
            this.listMIcodeDatatableRowsCount = this.miscImprovementOverviewData.ListMICodeNew.Rows.Count;
            this.MiscImprovementsGridView.Rows[0].Selected = false;
            this.MiscImprovementsGridView.Enabled = false;
        }

        /// <summary>
        /// Custimizes the code combo box.
        /// </summary>
        private void CustimizeCodeComboBox()
        {
            this.CodeComboBox.DisplayMember = this.listMICatalogCodeComboboxDatatable.MICodeColumn.ColumnName;
            this.CodeComboBox.ValueMember = this.listMICatalogCodeComboboxDatatable.MICodeIDColumn.ColumnName;
        }

        /// <summary>
        /// Alls the misc improvement value changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AllMiscImprovementValueChanged(object sender, EventArgs e)
        {
            this.ToEnableEditButtonInMasterForm();
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// To Assoicate the Misc Improvements Details for New mode 
        /// Here Misc Code id is sent to get the Misc Improvements Details for New mode 
        /// </summary>
        /// <param name="currentnewmiscCodeidvalue">currentnewmiscCodeidvalue</param>
        /// <param name="currentPermissionvalue">currentPermissionvalue</param>
        /// <param name="currentMasterPermissionFlagvalue">currentMasterPermissionFlagvalue</param>
        private void AssociateMiscImproForNewMode(int currentnewmiscCodeidvalue, bool currentPermissionvalue, bool currentMasterPermissionFlagvalue)
        {
            this.flagLoadOnProcess = true;

            F36010MiscImprovementCatalog miscImprovementCatalogData = new F36010MiscImprovementCatalog();

            ////on new mode the misc code id is sent to get the misc improvement deatails
            miscImprovementCatalogData = this.form36011Control.WorkItem.F36010_GetMiscImprovementCatalog(currentnewmiscCodeidvalue);
            //this.strDesecription = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.DescriptionColumn].ToString();
            this.DescriptionTextBox.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.DescriptionColumn].ToString();

            this.miscfieldsFormula1 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F01Column].ToString();
            this.miscfieldsFormula2 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F02Column].ToString();
            this.miscfieldsFormula3 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F03Column].ToString();
            this.miscfieldsFormula4 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F04Column].ToString();
            this.miscfieldsFormula5 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F05Column].ToString();
            this.miscfieldsFormula6 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F06Column].ToString();
            this.miscfieldsFormula7 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F07Column].ToString();
            this.miscfieldsFormula8 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F08Column].ToString();
            this.miscfieldsFormula9 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F09Column].ToString();
            this.miscfieldsFormula10 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F10Column].ToString();
            this.miscfieldsFormula11 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F11Column].ToString();
            this.miscfieldsFormula12 = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F12Column].ToString();
            this.vformula = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.VFormulaColumn].ToString();

            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D01Column].ToString(), out this.mifields1DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D02Column].ToString(), out this.mifields2DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D03Column].ToString(), out this.mifields3DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D04Column].ToString(), out this.mifields4DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D05Column].ToString(), out this.mifields5DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D06Column].ToString(), out this.mifields6DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D07Column].ToString(), out this.mifields7DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D08Column].ToString(), out this.mifields8DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D09Column].ToString(), out this.mifields9DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D10Column].ToString(), out this.mifields10DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D11Column].ToString(), out this.mifields11DecimalPlaces);
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.D12Column].ToString(), out this.mifields12DecimalPlaces);

            this.ToSetDecimalPlaces(this.MILabel1TextBox, this.mifields1DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel2TextBox, this.mifields2DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel3TextBox, this.mifields3DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel4TextBox, this.mifields4DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel5TextBox, this.mifields5DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel6TextBox, this.mifields6DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel7TextBox, this.mifields7DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel8TextBox, this.mifields8DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel9TextBox, this.mifields9DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel10TextBox, this.mifields10DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel11TextBox, this.mifields11DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel12TextBox, this.mifields12DecimalPlaces);

            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C01Column].ToString(), out this.mifieldsC01Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C02Column].ToString(), out this.mifieldsC02Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C03Column].ToString(), out this.mifieldsC03Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C04Column].ToString(), out this.mifieldsC04Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C05Column].ToString(), out this.mifieldsC05Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C06Column].ToString(), out this.mifieldsC06Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C07Column].ToString(), out this.mifieldsC07Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C08Column].ToString(), out this.mifieldsC08Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C09Column].ToString(), out this.mifieldsC09Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C10Column].ToString(), out this.mifieldsC10Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C11Column].ToString(), out this.mifieldsC11Value);
            short.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C12Column].ToString(), out this.mifieldsC12Value);

            if (this.mifieldsC01Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox1(currentnewmiscCodeidvalue, 1);
            }

            if (this.mifieldsC02Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox2(currentnewmiscCodeidvalue, 2);
            }

            if (this.mifieldsC03Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox3(currentnewmiscCodeidvalue, 3);
            }

            if (this.mifieldsC04Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox4(currentnewmiscCodeidvalue, 4);
            }

            if (this.mifieldsC05Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox5(currentnewmiscCodeidvalue, 5);
            }

            if (this.mifieldsC06Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox6(currentnewmiscCodeidvalue, 6);
            }

            if (this.mifieldsC07Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox7(currentnewmiscCodeidvalue, 7);
            }

            if (this.mifieldsC08Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox8(currentnewmiscCodeidvalue, 8);
            }

            if (this.mifieldsC09Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox9(currentnewmiscCodeidvalue, 9);
            }

            if (this.mifieldsC10Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox10(currentnewmiscCodeidvalue, 10);
            }

            if (this.mifieldsC11Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox11(currentnewmiscCodeidvalue, 11);
            }

            if (this.mifieldsC12Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox12(currentnewmiscCodeidvalue, 12);
            }

            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C01Column].ToString(), this.MILabel1TextBox, this.MILabel1ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C02Column].ToString(), this.MILabel2TextBox, this.MILabel2ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C03Column].ToString(), this.MILabel3TextBox, this.MILabel3ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C04Column].ToString(), this.MILabel4TextBox, this.MILabel4ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C05Column].ToString(), this.MILabel5TextBox, this.MILabel5ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C06Column].ToString(), this.MILabel6TextBox, this.MILabel6ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C07Column].ToString(), this.MILabel7TextBox, this.MILabel7ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C08Column].ToString(), this.MILabel8TextBox, this.MILabel8ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C09Column].ToString(), this.MILabel9TextBox, this.MILabel9ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C10Column].ToString(), this.MILabel10TextBox, this.MILabel10ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C11Column].ToString(), this.MILabel11TextBox, this.MILabel11ComboBox);
            this.MiscFieldsDisplayBehavior(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.C12Column].ToString(), this.MILabel12TextBox, this.MILabel12ComboBox);

            this.MILabel1Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L01Column].ToString();
            this.MILabel2Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L02Column].ToString();
            this.MILabel3Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L03Column].ToString();
            this.MILabel4Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L04Column].ToString();
            this.MILabel5Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L05Column].ToString();
            this.MILabel6Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L06Column].ToString();
            this.MILabel7Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L07Column].ToString();
            this.MILabel8Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L08Column].ToString();
            this.MILabel9Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L09Column].ToString();
            this.MILabel10Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L10Column].ToString();
            this.MILabel11Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L11Column].ToString();
            this.MILabel12Label.Text = miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.L12Column].ToString();

            ////when the corresponding sketch value is true / Formula feed the label forecolor is changed to red color(192,0,0)
            this.MiscImprovementLabelColor(this.MILabel1Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S01Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F01Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel2Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S02Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F02Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel3Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S03Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F03Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel4Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S04Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F04Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel5Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S05Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F05Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel6Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S06Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F06Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel7Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S07Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F07Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel8Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S08Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F08Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel9Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S09Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F09Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel10Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S10Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F10Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel11Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S11Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F11Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel12Label, miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.S12Column].ToString(), miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.F12Column].ToString());

            this.MILabel1TextBox.Text = string.Empty;
            this.MILabel2TextBox.Text = string.Empty;
            this.MILabel3TextBox.Text = string.Empty;
            this.MILabel4TextBox.Text = string.Empty;
            this.MILabel5TextBox.Text = string.Empty;
            this.MILabel6TextBox.Text = string.Empty;
            this.MILabel7TextBox.Text = string.Empty;
            this.MILabel8TextBox.Text = string.Empty;
            this.MILabel9TextBox.Text = string.Empty;
            this.MILabel10TextBox.Text = string.Empty;
            this.MILabel11TextBox.Text = string.Empty;
            this.MILabel12TextBox.Text = string.Empty;

            this.AssignMiscFieldWithComboBox1(0);
            this.AssignMiscFieldWithComboBox2(0);
            this.AssignMiscFieldWithComboBox3(0);
            this.AssignMiscFieldWithComboBox4(0);
            this.AssignMiscFieldWithComboBox5(0);
            this.AssignMiscFieldWithComboBox6(0);
            this.AssignMiscFieldWithComboBox7(0);
            this.AssignMiscFieldWithComboBox8(0);
            this.AssignMiscFieldWithComboBox9(0);
            this.AssignMiscFieldWithComboBox10(0);
            this.AssignMiscFieldWithComboBox11(0);
            this.AssignMiscFieldWithComboBox12(0);

            this.DeprWithPrimaryCheckBox.Checked = false;
            this.DeprYearTextBox.Text = string.Empty;
            this.DeprEconLifeTextBox.Text = string.Empty;
            this.DeprEffectiveAgeTextBox.Text = string.Empty;
            this.DeprPhysPercentTextBox.Text = string.Empty;
            this.DeprFuncPercentTextBox.Text = string.Empty;
            this.DeprBaseCostTextBox.Text = string.Empty;
            this.DeprPhysTextBox.Text = string.Empty;
            this.DeprFuncTextBox.Text = string.Empty;
            this.DeprOverrideTextBox.Text = string.Empty;
            this.DeprFinalValueTextBox.Text = string.Empty;

            int tempMIcodeid;
            int.TryParse(miscImprovementCatalogData.GetMICatalog.Rows[0][miscImprovementCatalogData.GetMICatalog.MICodeIDColumn].ToString(), out tempMIcodeid);

            this.AssignCodeComboBoxValue(tempMIcodeid);
            this.AssignDeprQualityResComboBox(0);
            this.AssignDeprConditionComboBox(0);
            this.AssignDeprFunctionalCategoryComboBox(0);

            this.ValidateMIfieldsTextBoxs(this.MILabel1TextBox, this.miscfieldsFormula1, this.MILabel1Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel2TextBox, this.miscfieldsFormula2, this.MILabel2Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel3TextBox, this.miscfieldsFormula3, this.MILabel3Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel4TextBox, this.miscfieldsFormula4, this.MILabel4Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel5TextBox, this.miscfieldsFormula5, this.MILabel5Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel6TextBox, this.miscfieldsFormula6, this.MILabel6Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel7TextBox, this.miscfieldsFormula7, this.MILabel7Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel8TextBox, this.miscfieldsFormula8, this.MILabel8Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel9TextBox, this.miscfieldsFormula9, this.MILabel9Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel10TextBox, this.miscfieldsFormula10, this.MILabel10Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel11TextBox, this.miscfieldsFormula11, this.MILabel11Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsTextBoxs(this.MILabel12TextBox, this.miscfieldsFormula12, this.MILabel12Label.Text.Trim(), currentPermissionvalue, currentMasterPermissionFlagvalue);

            this.ValidateMIfieldsComboBoxs(this.MILabel1ComboBox, this.MILabel1Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel2ComboBox, this.MILabel2Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel3ComboBox, this.MILabel3Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel4ComboBox, this.MILabel4Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel5ComboBox, this.MILabel5Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel6ComboBox, this.MILabel6Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel7ComboBox, this.MILabel7Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel8ComboBox, this.MILabel8Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel9ComboBox, this.MILabel9Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel10ComboBox, this.MILabel10Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel11ComboBox, this.MILabel11Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);
            this.ValidateMIfieldsComboBoxs(this.MILabel12ComboBox, this.MILabel12Label.Text, currentPermissionvalue, currentMasterPermissionFlagvalue);

            ////value text box
            this.AssignValuetextBox(string.Empty);
            this.LoadMiscCalucaltioGrid(true);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Associates the misc improvements for view mode.
        /// </summary>
        /// <param name="currentmidvalue">The currentmidvalue.</param>
        /// <param name="currentEditablePermission">if set to <c>true</c> [current editable permission].</param>
        /// <param name="currentFormMasterPermissionFlag">if set to <c>true</c> [current form master permission flag].</param>
        private void AssociateMiscImprovementsForViewMode(int currentmidvalue, bool currentEditablePermission, bool currentFormMasterPermissionFlag)
        {
            int miscCodeIdValue;
            this.flagLoadOnProcess = true;

            this.listMiscImprovementsDataTable.Clear();
            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListMiscImprovements(currentmidvalue);
            ////this.listMiscImprovementsDataTable = this.miscImprovementOverviewData.ListMiscImprovements;                
            this.listMiscImprovementsDataTable = this.miscImprovementOverviewData.ListMiscImprovementsNew;

            this.DescriptionTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DescriptionColumn].ToString();
            this.miscfieldsFormula1 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F01Column].ToString();
            this.miscfieldsFormula2 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F02Column].ToString();
            this.miscfieldsFormula3 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F03Column].ToString();
            this.miscfieldsFormula4 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F04Column].ToString();
            this.miscfieldsFormula5 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F05Column].ToString();
            this.miscfieldsFormula6 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F06Column].ToString();
            this.miscfieldsFormula7 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F07Column].ToString();
            this.miscfieldsFormula8 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F08Column].ToString();
            this.miscfieldsFormula9 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F09Column].ToString();
            this.miscfieldsFormula10 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F10Column].ToString();
            this.miscfieldsFormula11 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F11Column].ToString();
            this.miscfieldsFormula12 = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F12Column].ToString();

            this.vformula = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.VFormulaColumn].ToString();

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D01Column].ToString(), out this.mifields1DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D02Column].ToString(), out this.mifields2DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D03Column].ToString(), out this.mifields3DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D04Column].ToString(), out this.mifields4DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D05Column].ToString(), out this.mifields5DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D06Column].ToString(), out this.mifields6DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D07Column].ToString(), out this.mifields7DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D08Column].ToString(), out this.mifields8DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D09Column].ToString(), out this.mifields9DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D10Column].ToString(), out this.mifields10DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D11Column].ToString(), out this.mifields11DecimalPlaces);
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.D12Column].ToString(), out this.mifields12DecimalPlaces);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MICodeIDColumn].ToString(), out miscCodeIdValue);

            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C01Column].ToString(), out this.mifieldsC01Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C02Column].ToString(), out this.mifieldsC02Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C03Column].ToString(), out this.mifieldsC03Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C04Column].ToString(), out this.mifieldsC04Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C05Column].ToString(), out this.mifieldsC05Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C06Column].ToString(), out this.mifieldsC06Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C07Column].ToString(), out this.mifieldsC07Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C08Column].ToString(), out this.mifieldsC08Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C09Column].ToString(), out this.mifieldsC09Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C10Column].ToString(), out this.mifieldsC10Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C11Column].ToString(), out this.mifieldsC11Value);
            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C12Column].ToString(), out this.mifieldsC12Value);

            if (this.mifieldsC01Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox1(miscCodeIdValue, 1);
            }

            if (this.mifieldsC02Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox2(miscCodeIdValue, 2);
            }

            if (this.mifieldsC03Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox3(miscCodeIdValue, 3);
            }

            if (this.mifieldsC04Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox4(miscCodeIdValue, 4);
            }

            if (this.mifieldsC05Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox5(miscCodeIdValue, 5);
            }

            if (this.mifieldsC06Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox6(miscCodeIdValue, 6);
            }

            if (this.mifieldsC07Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox7(miscCodeIdValue, 7);
            }

            if (this.mifieldsC08Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox8(miscCodeIdValue, 8);
            }

            if (this.mifieldsC09Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox9(miscCodeIdValue, 9);
            }

            if (this.mifieldsC10Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox10(miscCodeIdValue, 10);
            }

            if (this.mifieldsC11Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox11(miscCodeIdValue, 11);
            }

            if (this.mifieldsC12Value.Equals(1))
            {
                this.LoadMiscFieldWithComboBox12(miscCodeIdValue, 12);
            }

            this.MILabel1Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L01Column].ToString();
            this.MILabel2Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L02Column].ToString();
            this.MILabel3Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L03Column].ToString();
            this.MILabel4Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L04Column].ToString();
            this.MILabel5Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L05Column].ToString();
            this.MILabel6Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L06Column].ToString();
            this.MILabel7Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L07Column].ToString();
            this.MILabel8Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L08Column].ToString();
            this.MILabel9Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L09Column].ToString();
            this.MILabel10Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L10Column].ToString();
            this.MILabel11Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L11Column].ToString();
            this.MILabel12Label.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.L12Column].ToString();

            ////when the corresponding sketch value is true the label forecolor is changed to red color(192,0,0)
            this.MiscImprovementLabelColor(this.MILabel1Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S01Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F01Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel2Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S02Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F02Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel3Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S03Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F03Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel4Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S04Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F04Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel5Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S05Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F05Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel6Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S06Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F06Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel7Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S07Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F07Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel8Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S08Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F08Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel9Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S09Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F09Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel10Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S10Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F10Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel11Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S11Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F11Column].ToString());
            this.MiscImprovementLabelColor(this.MILabel12Label, this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S12Column].ToString(), this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.F12Column].ToString());

            this.ToSetDecimalPlaces(this.MILabel1TextBox, this.mifields1DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel2TextBox, this.mifields2DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel3TextBox, this.mifields3DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel4TextBox, this.mifields4DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel5TextBox, this.mifields5DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel6TextBox, this.mifields6DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel7TextBox, this.mifields7DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel8TextBox, this.mifields8DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel9TextBox, this.mifields9DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel10TextBox, this.mifields10DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel11TextBox, this.mifields11DecimalPlaces);
            this.ToSetDecimalPlaces(this.MILabel12TextBox, this.mifields12DecimalPlaces);

            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C01Column].ToString(), this.MILabel1TextBox, this.MILabel1ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C02Column].ToString(), this.MILabel2TextBox, this.MILabel2ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C03Column].ToString(), this.MILabel3TextBox, this.MILabel3ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C04Column].ToString(), this.MILabel4TextBox, this.MILabel4ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C05Column].ToString(), this.MILabel5TextBox, this.MILabel5ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C06Column].ToString(), this.MILabel6TextBox, this.MILabel6ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C07Column].ToString(), this.MILabel7TextBox, this.MILabel7ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C08Column].ToString(), this.MILabel8TextBox, this.MILabel8ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C09Column].ToString(), this.MILabel9TextBox, this.MILabel9ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C10Column].ToString(), this.MILabel10TextBox, this.MILabel10ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C11Column].ToString(), this.MILabel11TextBox, this.MILabel11ComboBox);
            this.MiscFieldsDisplayBehavior(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.C12Column].ToString(), this.MILabel12TextBox, this.MILabel12ComboBox);

            this.MILabel1TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V01Column].ToString();
            this.MILabel2TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V02Column].ToString();
            this.MILabel3TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V03Column].ToString();
            this.MILabel4TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V04Column].ToString();
            this.MILabel5TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V05Column].ToString();
            this.MILabel6TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V06Column].ToString();
            this.MILabel7TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V07Column].ToString();
            this.MILabel8TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V08Column].ToString();
            this.MILabel9TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V09Column].ToString();
            this.MILabel10TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V10Column].ToString();
            this.MILabel11TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V11Column].ToString();
            this.MILabel12TextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V12Column].ToString();

            int currentChoiceIdValue;
            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID01Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox1(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID02Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox2(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID03Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox3(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID04Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox4(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID05Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox5(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID06Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox6(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID07Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox7(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID08Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox8(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID09Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox9(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID10Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox10(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID11Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox11(currentChoiceIdValue);

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MIChoiceID12Column].ToString(), out currentChoiceIdValue);
            this.AssignMiscFieldWithComboBox12(currentChoiceIdValue);

            ////fill all depr control values
            short deprWithPrimaryChecked;

            short.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprWithPrimaryColumn].ToString(), out deprWithPrimaryChecked);
            if (deprWithPrimaryChecked > 0)
            {
                this.DeprWithPrimaryCheckBox.Checked = true;
            }
            else
            {
                this.DeprWithPrimaryCheckBox.Checked = false;
            }

            this.DeprYearTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprYearinColumn].ToString();
            this.DeprEconLifeTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprEconLifeColumn].ToString();
            this.DeprEffectiveAgeTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprEAgeColumn].ToString();
            this.DeprPhysPercentTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprPercentColumn].ToString();
            this.DeprFuncPercentTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprFuncPercentColumn].ToString();
            this.DeprBaseCostTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.ValueBaseColumn].ToString();
            this.DeprPhysTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.PhysDeprColumn].ToString();
            this.DeprFuncTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.FuncDeprColumn].ToString();
            this.DeprOverrideTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.ValueOverideColumn].ToString();
            this.DeprFinalValueTextBox.Text = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.ValueFinalColumn].ToString();

            int tempMIcodeid;
            byte tempDeprFuncId;
            decimal tempDeprQuality;
            decimal tempDeprCondition;

            int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.MICodeIDColumn].ToString(), out tempMIcodeid);
            decimal.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprQualityColumn].ToString(), out tempDeprQuality);
            decimal.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprConditionColumn].ToString(), out tempDeprCondition);
            byte.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.DeprFuncIDColumn].ToString(), out tempDeprFuncId);

            this.AssignCodeComboBoxValue(tempMIcodeid);
            this.AssignDeprQualityResComboBox(tempDeprQuality);
            this.AssignDeprConditionComboBox(tempDeprCondition);
            this.AssignDeprFunctionalCategoryComboBox(tempDeprFuncId);

            this.ValidateMIfieldsTextBoxs(this.MILabel1TextBox, this.miscfieldsFormula1, this.MILabel1Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel2TextBox, this.miscfieldsFormula2, this.MILabel2Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel3TextBox, this.miscfieldsFormula3, this.MILabel3Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel4TextBox, this.miscfieldsFormula4, this.MILabel4Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel5TextBox, this.miscfieldsFormula5, this.MILabel5Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel6TextBox, this.miscfieldsFormula6, this.MILabel6Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel7TextBox, this.miscfieldsFormula7, this.MILabel7Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel8TextBox, this.miscfieldsFormula8, this.MILabel8Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel9TextBox, this.miscfieldsFormula9, this.MILabel9Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel10TextBox, this.miscfieldsFormula10, this.MILabel10Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel11TextBox, this.miscfieldsFormula11, this.MILabel11Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsTextBoxs(this.MILabel12TextBox, this.miscfieldsFormula12, this.MILabel12Label.Text.Trim(), currentEditablePermission, currentFormMasterPermissionFlag);

            this.ValidateMIfieldsComboBoxs(this.MILabel1ComboBox, this.MILabel1Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel2ComboBox, this.MILabel2Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel3ComboBox, this.MILabel3Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel4ComboBox, this.MILabel4Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel5ComboBox, this.MILabel5Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel6ComboBox, this.MILabel6Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel7ComboBox, this.MILabel7Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel8ComboBox, this.MILabel8Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel9ComboBox, this.MILabel9Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel10ComboBox, this.MILabel10Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel11ComboBox, this.MILabel11Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);
            this.ValidateMIfieldsComboBoxs(this.MILabel12ComboBox, this.MILabel12Label.Text, currentEditablePermission, currentFormMasterPermissionFlag);


            if (this.listMiscImprovementsDataTable.PolygonIDColumn != null)
            {
                int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.PolygonIDColumn.ColumnName].ToString(), out this.PolygonID);
                if (this.PolygonID > 0)
                {
                    int s01;
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S01Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel1Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel1Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S02Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel2Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel2Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S03Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel3Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel3Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S04Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel4Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel4Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S05Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel5Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel5Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S06Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel6Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel6Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S07Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel7Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel7Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S08Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel8Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel8Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S09Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel9Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel9Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S10Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel10Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel10Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S11Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel11Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel11Panel.Enabled = true;
                    }
                    int.TryParse(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.S12Column].ToString(), out s01);
                    if (s01.Equals(1))
                    {
                        this.MILabel12Panel.Enabled = false;
                    }
                    else
                    {
                        this.MILabel12Panel.Enabled = true;
                    }
                }
                else
                {
                    this.LockCharacteristicsPanel(true);
                }


            }
            else
            {
                this.PolygonID = 0;
                this.LockCharacteristicsPanel(true);
            }
            //value text box
            this.AssignValuetextBox(this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.ValueOverideColumn].ToString());
            this.LoadMiscCalucaltioGrid(false);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Used to fill the value text box controls
        /// </summary>
        /// <param name="currentValueOverride">The current value override.</param>
        private void AssignValuetextBox(string currentValueOverride)
        {
            long deprPhysValue;
            long deprFuncValue;
            long valueBase;
            long finalValue;

            if (!string.IsNullOrEmpty(currentValueOverride) && this.isOverride)
            {
                this.DeprFinalValueTextBox.Text = currentValueOverride;
            }
            else
            {
                long.TryParse(this.DeprPhysTextBox.DecimalTextBoxValue.ToString(), out deprPhysValue);
                long.TryParse(this.DeprFuncTextBox.DecimalTextBoxValue.ToString(), out deprFuncValue);
                long.TryParse(this.DeprBaseCostTextBox.DecimalTextBoxValue.ToString(), out valueBase);

                if (deprPhysValue > int.MaxValue)
                {
                    deprPhysValue = 0;
                }

                if (deprFuncValue > int.MaxValue)
                {
                    deprFuncValue = 0;
                }

                if (valueBase > int.MaxValue)
                {
                    valueBase = 0;
                }

                finalValue = valueBase + deprPhysValue + deprFuncValue;

                if (finalValue > int.MaxValue)
                {
                    finalValue = 0;
                }
                //int a = valueBase + deprPhysValue;// +deprFuncValue;
                //this.DeprFinalValueTextBox.Text = Convert.ToString(valueBase + deprPhysValue + deprFuncValue);
                this.DeprFinalValueTextBox.Text = finalValue.ToString("#,##0");
            }
        }

        /// <summary>
        /// Miscs the color of the improvement label.
        /// </summary>
        /// <param name="labelControls">The label controls.</param>
        /// <param name="issketched">The issketched.</param>
        /// <param name="isformula">The isformula.</param>
        private void MiscImprovementLabelColor(Label labelControls, string issketched, string isformula)
        {
            ////when the corresponding sketch value is true or formula is feed 
            ////then the label forecolor is changed to red color(192,0,0)
            if (issketched.Equals("1") || !string.IsNullOrEmpty(isformula.Trim()))
            {
                labelControls.ForeColor = Color.FromArgb(192, 0, 0);
            }
            else
            {
                labelControls.ForeColor = Color.FromArgb(51, 51, 153);
            }
        }

        /// <summary>
        /// Miscs the fields display behavior.
        /// </summary>
        /// <param name="iscomboChecked">The iscombo checked.</param>
        /// <param name="miscFieldsTextBox">The misc fields text box.</param>
        /// <param name="miscFieldsComboBox">The misc fields combo box.</param>
        private void MiscFieldsDisplayBehavior(string iscomboChecked, TerraScanTextBox miscFieldsTextBox, TerraScanComboBox miscFieldsComboBox)
        {
            if (iscomboChecked.Equals("1"))
            {
                miscFieldsComboBox.Visible = true;
                miscFieldsTextBox.Visible = false;
            }
            else
            {
                miscFieldsComboBox.Visible = false;
                miscFieldsTextBox.Visible = true;
            }
        }

        /// <summary>
        /// To get the Misc Improvements Over view details
        /// </summary>
        /// <param name="currentidvalue">The currentidvalue.</param>
        /// <param name="currentPermission">if set to <c>true</c> [current permission].</param>
        /// <param name="currentMasterPermissionFlag">if set to <c>true</c> [current master permission flag].</param>
        /// <param name="isnewMode">if set to <c>true</c> [isnew mode].</param>
        private void AssignValueForMiscImprovements(int currentidvalue, bool currentPermission, bool currentMasterPermissionFlag, bool isnewMode)
        {
            if (!isnewMode)
            {
                this.AssociateMiscImprovementsForViewMode(currentidvalue, currentPermission, currentMasterPermissionFlag);
            }
            else
            {
                this.AssociateMiscImproForNewMode(currentidvalue, currentPermission, currentMasterPermissionFlag);
            }
        }

        /// <summary>
        /// To assing the Misc components based on the current miscId
        /// </summary>
        /// <param name="currentRowIndex">the current row index of the Grid</param>
        private void AssignMiscellaneousCommponents(int currentRowIndex)
        {
            if ((this.MiscImprovementsGridView.Rows[currentRowIndex].Cells[this.MiscMID.Name].Value != null) && (!string.IsNullOrEmpty(this.MiscImprovementsGridView.Rows[currentRowIndex].Cells[this.MiscMID.Name].Value.ToString())))
            {
                this.flagLoadOnProcess = true;
                this.miscGridRowId = currentRowIndex;
                int.TryParse(this.MiscImprovementsGridView.Rows[currentRowIndex].Cells[this.listMIcodeDatatable.MICodeIDColumn.ColumnName].Value.ToString(), out this.currentMICodeId);
                int.TryParse(this.MiscImprovementsGridView.Rows[currentRowIndex].Cells[this.MiscMID.Name].Value.ToString(), out this.currentMiscMid);

                ////this condtiion is called to on load where we have check the editable permission and form master permission
                this.AssignValueForMiscImprovements(this.currentMiscMid, this.PermissionFiled.editPermission, this.formMasterPermissionEdit, false);
                //if (this.PolygonID > 0)
                //{
                //    this.LockCharacteristicsPanel(false);
                //}
                //else
                //{
                //    this.LockCharacteristicsPanel(true);
                //}
                ////for attachment and comment 
                this.SetAdditionalOperationCount(this.currentMiscMid);

                this.flagLoadOnProcess = false;
            }
        }

        /// <summary>
        /// Validates the M ifields text boxs.
        /// </summary>
        /// <param name="miscFieldstextBoxControls">The misc fieldstext box controls.</param>
        /// <param name="miscFieldsFormula">The misc fields formula.</param>
        /// <param name="miscLabelText">The decimal places value.</param>
        /// <param name="userPermission">if set to <c>true</c> [edit permission].</param>
        /// <param name="masterFormPermission">masterFormPermission</param>
        private void ValidateMIfieldsTextBoxs(Control miscFieldstextBoxControls, string miscFieldsFormula, string miscLabelText, bool userPermission, bool masterFormPermission)
        {
            if (miscFieldstextBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)miscFieldstextBoxControls;

                ////here based on the permission and formula value we have to edit the textbox controls
                if (userPermission && masterFormPermission)
                {
                    if (!string.IsNullOrEmpty(miscFieldsFormula))
                    {
                        currentTextBox.LockKeyPress = true;
                        currentTextBox.ForeColor = System.Drawing.Color.DarkGray;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(miscLabelText))
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
                    if (!string.IsNullOrEmpty(miscFieldsFormula))
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

        /// <summary>
        /// Validates the M ifields combo boxs.
        /// </summary>
        /// <param name="miscFieldsComboBoxControls">The misc fields combo box controls.</param>
        /// <param name="miscLabelText">The misc label text.</param>
        /// <param name="userPermission">if set to <c>true</c> [user permission].</param>
        /// <param name="masterFormPermission">if set to <c>true</c> [master form permission].</param>
        private void ValidateMIfieldsComboBoxs(Control miscFieldsComboBoxControls, string miscLabelText, bool userPermission, bool masterFormPermission)
        {
            if (miscFieldsComboBoxControls.GetType() == typeof(TerraScanComboBox))
            {
                TerraScanComboBox currentComboBox = (TerraScanComboBox)miscFieldsComboBoxControls;

                ////here based on the permission and label value we have to edit the combobox controls
                if (userPermission && masterFormPermission)
                {
                    if (string.IsNullOrEmpty(miscLabelText))
                    {
                        currentComboBox.Enabled = false;
                    }
                    else
                    {
                        currentComboBox.Enabled = true;
                    }
                }
                else
                {
                    currentComboBox.Enabled = false;
                }
            }
        }

        /// <summary>
        /// To set the decimal places for the TextBox controls
        /// </summary>
        /// <param name="textBoxControls">textBoxControls</param>
        /// <param name="decimalPlacesValue">decimalPlacesValue</param>
        private void ToSetDecimalPlaces(Control textBoxControls, int decimalPlacesValue)
        {
            if (textBoxControls.GetType() == typeof(TerraScanTextBox))
            {
                TerraScanTextBox currentTextBox = (TerraScanTextBox)textBoxControls;

                switch (decimalPlacesValue)
                {
                    case 1:
                        currentTextBox.TextCustomFormat = "#,##0.0";
                        currentTextBox.Precision = 1;
                        break;
                    case 2:
                        currentTextBox.TextCustomFormat = "#,##0.00";
                        currentTextBox.Precision = 2;
                        break;
                    case 3:
                        currentTextBox.TextCustomFormat = "#,##0.000";
                        currentTextBox.Precision = 3;
                        break;
                    case 4:
                        currentTextBox.TextCustomFormat = "#,##0.0000";
                        currentTextBox.Precision = 4;
                        break;
                    default:
                        currentTextBox.TextCustomFormat = "#,##";
                        currentTextBox.Precision = 0;
                        break;
                }
            }
        }

        /// <summary>
        /// To assgin the code combobox value
        /// </summary>
        /// <param name="currentMIcodeValue">current MI code id</param>
        private void AssignCodeComboBoxValue(int currentMIcodeValue)
        {
            if (this.listMICatalogCodeComboboxDatatable.Rows.Count > 0)
            {
                if (currentMIcodeValue > 0)
                {
                    this.CodeComboBox.SelectedValue = currentMIcodeValue;
                }
                else
                {
                    this.CodeComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                ////Disable the attachement and comment button when edit mode
                this.AttachmentButton.Enabled = false;
                this.CommentButton.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">
        /// true - to Set the control as not editable
        /// false - to Set the control as editable
        /// </param>
        private void ControlLock(bool controlLook)
        {
            ////lock the Code and Description controls
            this.CodeComboBox.Enabled = !controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.CodeCatalogButton.Enabled = !controlLook;

            ////lock the Depreciation controls
            this.DeprWithPrimaryCheckBox.Enabled = !controlLook;
            this.DeprYearTextBox.LockKeyPress = controlLook;
            this.DeprQualityComboBox.Enabled = !controlLook;
            this.DeprConditionComboBox.Enabled = !controlLook;
            this.DeprEconLifeTextBox.LockKeyPress = controlLook;
            this.DeprEffectiveAgeTextBox.LockKeyPress = controlLook;
            this.DeprPhysPercentTextBox.LockKeyPress = controlLook;
            this.DeprFuncDefCategoryComboBox.Enabled = !controlLook;
            this.DeprFuncPercentTextBox.LockKeyPress = controlLook;
            //this.DeprBaseCostTextBox.LockKeyPress = controlLook;
            this.DeprPhysTextBox.LockKeyPress = controlLook;
            this.DeprFuncTextBox.LockKeyPress = controlLook;
            this.DeprOverrideTextBox.LockKeyPress = controlLook;

            ////lock the attachmenntComment panel controls
            this.AttachmentandCommentPanel.Enabled = controlLook;
        }

        /// <summary>
        /// Clears the misc improvements controls.
        /// </summary>
        private void ClearMiscImprovementsControls()
        {
            this.flagLoadOnProcess = true;

            ////reset the code combo and clear description textbox
            this.CodeComboBox.SelectedIndex = -1;
            //this.strDesecription = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            ////clear the MILabel caption controls
            this.MILabel1Label.Text = string.Empty;
            this.MILabel2Label.Text = string.Empty;
            this.MILabel3Label.Text = string.Empty;
            this.MILabel4Label.Text = string.Empty;
            this.MILabel5Label.Text = string.Empty;
            this.MILabel6Label.Text = string.Empty;
            this.MILabel7Label.Text = string.Empty;
            this.MILabel8Label.Text = string.Empty;
            this.MILabel9Label.Text = string.Empty;
            this.MILabel10Label.Text = string.Empty;
            this.MILabel11Label.Text = string.Empty;
            this.MILabel12Label.Text = string.Empty;

            ////clear the MILabel textbox controls
            this.MILabel1TextBox.Text = string.Empty;
            this.MILabel2TextBox.Text = string.Empty;
            this.MILabel3TextBox.Text = string.Empty;
            this.MILabel4TextBox.Text = string.Empty;
            this.MILabel5TextBox.Text = string.Empty;
            this.MILabel6TextBox.Text = string.Empty;
            this.MILabel7TextBox.Text = string.Empty;
            this.MILabel8TextBox.Text = string.Empty;
            this.MILabel9TextBox.Text = string.Empty;
            this.MILabel10TextBox.Text = string.Empty;
            this.MILabel11TextBox.Text = string.Empty;
            this.MILabel12TextBox.Text = string.Empty;

            ////reset the MILabel combobox controls
            this.MILabel1ComboBox.SelectedIndex = -1;
            this.MILabel2ComboBox.SelectedIndex = -1;
            this.MILabel3ComboBox.SelectedIndex = -1;
            this.MILabel4ComboBox.SelectedIndex = -1;
            this.MILabel5ComboBox.SelectedIndex = -1;
            this.MILabel6ComboBox.SelectedIndex = -1;
            this.MILabel7ComboBox.SelectedIndex = -1;
            this.MILabel8ComboBox.SelectedIndex = -1;
            this.MILabel9ComboBox.SelectedIndex = -1;
            this.MILabel10ComboBox.SelectedIndex = -1;
            this.MILabel11ComboBox.SelectedIndex = -1;
            this.MILabel12ComboBox.SelectedIndex = -1;

            ////make visible the MILabel textbox controls
            this.MILabel1TextBox.Visible = true;
            this.MILabel2TextBox.Visible = true;
            this.MILabel3TextBox.Visible = true;
            this.MILabel4TextBox.Visible = true;
            this.MILabel5TextBox.Visible = true;
            this.MILabel6TextBox.Visible = true;
            this.MILabel7TextBox.Visible = true;
            this.MILabel8TextBox.Visible = true;
            this.MILabel9TextBox.Visible = true;
            this.MILabel10TextBox.Visible = true;
            this.MILabel11TextBox.Visible = true;
            this.MILabel12TextBox.Visible = true;

            ////donot make visible the MILabel combobox controls
            this.MILabel1ComboBox.Visible = false;
            this.MILabel2ComboBox.Visible = false;
            this.MILabel3ComboBox.Visible = false;
            this.MILabel4ComboBox.Visible = false;
            this.MILabel5ComboBox.Visible = false;
            this.MILabel6ComboBox.Visible = false;
            this.MILabel7ComboBox.Visible = false;
            this.MILabel8ComboBox.Visible = false;
            this.MILabel9ComboBox.Visible = false;
            this.MILabel10ComboBox.Visible = false;
            this.MILabel11ComboBox.Visible = false;
            this.MILabel12ComboBox.Visible = false;

            ////clear the MIFields formula
            this.miscfieldsFormula1 = string.Empty;
            this.miscfieldsFormula2 = string.Empty;
            this.miscfieldsFormula3 = string.Empty;
            this.miscfieldsFormula4 = string.Empty;
            this.miscfieldsFormula5 = string.Empty;
            this.miscfieldsFormula6 = string.Empty;
            this.miscfieldsFormula7 = string.Empty;
            this.miscfieldsFormula8 = string.Empty;
            this.miscfieldsFormula9 = string.Empty;
            this.miscfieldsFormula10 = string.Empty;
            this.miscfieldsFormula11 = string.Empty;
            this.miscfieldsFormula12 = string.Empty;
            this.vformula = string.Empty;

            ////reset MIFields decimal places
            this.mifields1DecimalPlaces = 0;
            this.mifields2DecimalPlaces = 0;
            this.mifields3DecimalPlaces = 0;
            this.mifields4DecimalPlaces = 0;
            this.mifields5DecimalPlaces = 0;
            this.mifields6DecimalPlaces = 0;
            this.mifields7DecimalPlaces = 0;
            this.mifields8DecimalPlaces = 0;
            this.mifields9DecimalPlaces = 0;
            this.mifields10DecimalPlaces = 0;
            this.mifields11DecimalPlaces = 0;
            this.mifields12DecimalPlaces = 0;

            ////clear the Depreciation panel contorls
            this.DeprWithPrimaryCheckBox.Checked = false;
            this.DeprYearTextBox.Text = string.Empty;
            this.DeprQualityComboBox.SelectedIndex = -1;
            this.DeprConditionComboBox.SelectedIndex = -1;
            this.DeprEconLifeTextBox.Text = string.Empty;
            this.DeprEffectiveAgeTextBox.Text = string.Empty;
            this.DeprPhysPercentTextBox.Text = string.Empty;
            this.DeprFuncDefCategoryComboBox.SelectedIndex = -1;
            this.DeprFuncPercentTextBox.Text = string.Empty;
            this.DeprBaseCostTextBox.Text = string.Empty;
            this.DeprPhysTextBox.Text = string.Empty;
            this.DeprFuncTextBox.Text = string.Empty;
            this.DeprOverrideTextBox.Text = string.Empty;
            this.DeprFinalValueTextBox.Text = string.Empty;

            ////reset the Attachment and Comment count
            this.SetAdditionalOperationCount(0);

            this.flagLoadOnProcess = false;
        }


        /// <summary>
        /// To eanble or disable the Characteristics panels.
        /// </summary>
        /// <param name="isenable">if set to <c>true</c> [isenable].</param>
        private void LockCharacteristicsPanel(bool isenable)
        {
            this.MILabel1Panel.Enabled = isenable;
            this.MILabel2Panel.Enabled = isenable;
            this.MILabel3Panel.Enabled = isenable;
            this.MILabel4Panel.Enabled = isenable;
            this.MILabel5Panel.Enabled = isenable;
            this.MILabel6Panel.Enabled = isenable;
            this.MILabel7Panel.Enabled = isenable;
            this.MILabel8Panel.Enabled = isenable;
            this.MILabel9Panel.Enabled = isenable;
            this.MILabel10Panel.Enabled = isenable;
            this.MILabel11Panel.Enabled = isenable;
            this.MILabel12Panel.Enabled = isenable;
        }

        /// <summary>
        /// To eanble or disable the header part panels.
        /// </summary>
        /// <param name="isenable">if set to <c>true</c> [isenable].</param>
        private void LockHeaderPartPanels(bool isenable)
        {
            this.DescriptionPanel.Enabled = isenable;
            this.MILabel1Panel.Enabled = isenable;
            this.MILabel2Panel.Enabled = isenable;
            this.MILabel3Panel.Enabled = isenable;
            this.MILabel4Panel.Enabled = isenable;
            this.MILabel5Panel.Enabled = isenable;
            this.MILabel6Panel.Enabled = isenable;
            this.MILabel7Panel.Enabled = isenable;
            this.MILabel8Panel.Enabled = isenable;
            this.MILabel9Panel.Enabled = isenable;
            this.MILabel10Panel.Enabled = isenable;
            this.MILabel11Panel.Enabled = isenable;
            this.MILabel12Panel.Enabled = isenable;

            ////this.DeprEffectiveAgePanel.Enabled = isenable;
            ////this.DeprEconLifePanel.Enabled = isenable;
            ////this.DeprPhysPercentPanel.Enabled = isenable;
            ////this.DeprFuncPanel.Enabled = isenable;
            ////this.DeprFinalValuePanel.Enabled = isenable;

            ////this.DeprBaseCostPanel.Enabled = isenable;
            ////this.DeprOverridePanel.Enabled = isenable;
            ////this.DeprYearPanel.Enabled = isenable;
            //////this.DeprTablePanel.Enabled = isenable;
            ////this.DeprWithPrimaryPanel.Enabled = isenable;

            this.AttachmentButton.Enabled = isenable;
            this.CommentButton.Enabled = isenable;
        }

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.MIscImpHeaderPanel.Enabled = lockControl;
        }

        /// <summary>
        /// To delete the Misc Id
        /// </summary>
        /// <returns>boolean value</returns>
        private bool DeleteMiscid()
        {
            if (this.SelectMiscId())
            {
                this.form36011Control.WorkItem.F36011_DeleteMICode(this.currentMiscMid, TerraScan.Common.TerraScanCommon.UserId);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Selects the midID from the Misc Improvements OverView grid.
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool SelectMiscId()
        {
            int rowId = 0;

            try
            {
                ////To get the Row index for Fund Selection DataGridView
                rowId = this.GetRowIndex();

                if (this.MiscImprovementsGridView.OriginalRowCount > 0 && rowId >= 0)
                {
                    if (!string.IsNullOrEmpty(this.MiscImprovementsGridView.Rows[rowId].Cells[this.MiscMID.Name].Value.ToString()))
                    {
                        int.TryParse(this.MiscImprovementsGridView.Rows[rowId].Cells[this.MiscMID.Name].Value.ToString(), out this.currentMiscMid);

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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <returns>integer value of grid row index</returns>
        private int GetRowIndex()
        {
            try
            {
                if (this.MiscImprovementsGridView.OriginalRowCount > 0)
                {
                    if (this.MiscImprovementsGridView.CurrentCell != null)
                    {
                        return this.MiscImprovementsGridView.CurrentCell.RowIndex;
                    }

                    return -1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 4)
            {
                int increment = ((recordCount - 4) * 22);
                this.MiscImprovementsGridView.Height = 111 + increment;
                this.MiscImprovementsGridpanel.Height = 111 + increment;
                this.MiscImprovementPictureBox.Height = 487 + increment;
                this.EntireMiscImproPanel.Height = 447 + increment;
                this.Height = this.MiscImprovementPictureBox.Height;
            }
            else
            {
                this.MiscImprovementsGridView.Height = 111;
                this.MiscImprovementsGridpanel.Height = 111;
                this.MiscImprovementPictureBox.Height = 487;
                this.EntireMiscImproPanel.Height = 447;
                this.Height = 487;
            }

            this.SummaryBarPanel.Top = this.MiscImprovementsGridpanel.Bottom - 1;
        }

        /// <summary>
        /// Use to Validate the required fields in misc Improvements Overview.
        /// </summary>
        /// <returns>
        /// true - when Req Fields Exists
        /// false - When Req Fields Not Exists
        /// </returns>
        private bool CheckForRequiredFields()
        {
            try
            {
                if (string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()) || string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the misc improvement.
        /// </summary>
        /// <returns>boolean value</returns>
        private bool ValidateMiscImprovement()
        {
            int tempDeprYear = 0;
            byte tempEconLifeValue;
            byte tempEffectiveAgeValue;
            decimal maxDeprPercentValue = 9999M;

            if (!string.IsNullOrEmpty(this.DeprYearTextBox.Text.Trim()))
            {
                int.TryParse(this.DeprYearTextBox.Text.Trim(), out tempDeprYear);

                if (tempDeprYear > this.currentApplicationRollyear)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F36011DeprYearValidate") + this.currentApplicationRollyear, SharedFunctions.GetResourceString("F36011InvalidHeaderDeprYear"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (tempDeprYear == 0)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Year should be greater than 1899 and lesser than 2080"), SharedFunctions.GetResourceString("F36011InvalidHeaderDeprYear"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(this.DeprEconLifeTextBox.Text.Trim()))
            {
                if (byte.TryParse(this.DeprEconLifeTextBox.Text.Trim(), out tempEconLifeValue))
                {
                    this.DeprEconLifeTextBox.Text = tempEconLifeValue.ToString();
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Economic life value must be between 0 and 255."), SharedFunctions.GetResourceString("TerraScan T2 - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DeprEconLifeTextBox.Text = string.Empty;
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(this.DeprEffectiveAgeTextBox.Text.Trim()))
            {
                if (byte.TryParse(this.DeprEffectiveAgeTextBox.Text.Trim(), out tempEffectiveAgeValue))
                {
                    this.DeprEffectiveAgeTextBox.Text = tempEffectiveAgeValue.ToString();
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Effective age value must be between 0 and 255."), SharedFunctions.GetResourceString("TerraScan T2 - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DeprEffectiveAgeTextBox.Text = string.Empty;
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(this.DeprFuncPercentTextBox.Text.Trim()))
            {
                if (this.DeprFuncPercentTextBox.DecimalTextBoxValue > maxDeprPercentValue)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Func depr percent exceeded the maximum limit."), SharedFunctions.GetResourceString("TerraScan T2 - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(this.DeprPhysPercentTextBox.Text.Trim()))
            {
                if (this.DeprPhysPercentTextBox.DecimalTextBoxValue > maxDeprPercentValue)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Phys depr percent exceeded the maximum limit."), SharedFunctions.GetResourceString("TerraScan T2 - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            ////todo: need to confirm with client for validation as per new CO
            ////decimal currentConditionvalue;
            ////if (!string.IsNullOrEmpty(this.DeprEconLifeTextBox.Text.Trim()))
            ////{
            ////    decimal.TryParse(this.DeprEconLifeTextBox.Text.Trim(), out currentConditionvalue);
            ////    if (currentConditionvalue < 1 || currentConditionvalue > 6)
            ////    {
            ////        MessageBox.Show(SharedFunctions.GetResourceString("F36011Conditionvalidate"), SharedFunctions.GetResourceString("F36011InvalidHeaderCondition"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////        return false;
            ////    }
            ////}

            ////if (this.DeprEffectiveAgeTextBox.Text == "0")
            ////{
            ////    MessageBox.Show(SharedFunctions.GetResourceString("Age should not be 0."), SharedFunctions.GetResourceString("TerraScan - Invalid Data"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            ////    return false;
            ////}

            return true;
        }

        /// <summary>
        /// Used to Save Misc Improvements Overview
        /// </summary>
        /// <returns>integer value</returns>
        private int SaveMiscImprovements()
        {
            try
            {
                decimal saveMifields1Value = 0;
                decimal saveMifields2Value = 0;
                decimal saveMifields3Value = 0;
                decimal saveMifields4Value = 0;
                decimal saveMifields5Value = 0;
                decimal saveMifields6Value = 0;
                decimal saveMifields7Value = 0;
                decimal saveMifields8Value = 0;
                decimal saveMifields9Value = 0;
                decimal saveMifields10Value = 0;
                decimal saveMifields11Value = 0;
                decimal saveMifields12Value = 0;

                int saveMiscChoiceId01Value;
                int saveMiscChoiceId02Value;
                int saveMiscChoiceId03Value;
                int saveMiscChoiceId04Value;
                int saveMiscChoiceId05Value;
                int saveMiscChoiceId06Value;
                int saveMiscChoiceId07Value;
                int saveMiscChoiceId08Value;
                int saveMiscChoiceId09Value;
                int saveMiscChoiceId10Value;
                int saveMiscChoiceId11Value;
                int saveMiscChoiceId12Value;

                int savedMid;

                this.miscImprovementOverviewData.ListMiscImprovementsNew.Rows.Clear();

                ////F36011MiscImprovementOverviewData.ListMiscImprovementsRow dr = this.miscImprovementOverviewData.ListMiscImprovements.NewListMiscImprovementsRow();
                F36011MiscImprovementOverviewData.ListMiscImprovementsNewRow dr = this.miscImprovementOverviewData.ListMiscImprovementsNew.NewListMiscImprovementsNewRow();

                if (!string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()) && this.CodeComboBox.SelectedValue != null)
                {
                    int saveMiscCodeId;
                    int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out saveMiscCodeId);
                    dr.MICodeID = saveMiscCodeId;
                }

                ////dr.MICode = this.CodeComboBox.Text.Trim();
                //dr.Description = strDesecription.Trim();
                dr.Description = this.DescriptionTextBox.Text.Trim();

                ////Assign MILabelTextBox/MILabelComboBox Values 01 > 12 
                if (!string.IsNullOrEmpty(this.MILabel1TextBox.Text.Trim()) && this.MILabel1TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel1TextBox.Text.Trim(), out saveMifields1Value);
                    dr.V01 = saveMifields1Value;
                }
                else if (this.MILabel1ComboBox.Visible && this.MILabel1ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel1ComboBox.SelectedValue.ToString(), out saveMiscChoiceId01Value);
                    dr.V01 = this.miscCatalogChoiceItemValue01;
                    dr.MIChoiceID01 = saveMiscChoiceId01Value;
                    dr.MIChoice01Name = this.MILabel1ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel2TextBox.Text.Trim()) && this.MILabel2TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel2TextBox.Text.Trim(), out saveMifields2Value);
                    dr.V02 = saveMifields2Value;
                }
                else if (this.MILabel2ComboBox.Visible && this.MILabel2ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel2ComboBox.SelectedValue.ToString(), out saveMiscChoiceId02Value);
                    dr.V02 = this.miscCatalogChoiceItemValue02;
                    dr.MIChoiceID02 = saveMiscChoiceId02Value;
                    dr.MIChoice02Name = this.MILabel2ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel3TextBox.Text.Trim()) && this.MILabel3TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel3TextBox.Text.Trim(), out saveMifields3Value);
                    dr.V03 = saveMifields3Value;
                }
                else if (this.MILabel3ComboBox.Visible && this.MILabel3ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel3ComboBox.SelectedValue.ToString(), out saveMiscChoiceId03Value);
                    dr.V03 = this.miscCatalogChoiceItemValue03;
                    dr.MIChoiceID03 = saveMiscChoiceId03Value;
                    dr.MIChoice03Name = this.MILabel3ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel4TextBox.Text.Trim()) && this.MILabel4TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel4TextBox.Text.Trim(), out saveMifields4Value);
                    dr.V04 = saveMifields4Value;
                }
                else if (this.MILabel4ComboBox.Visible && this.MILabel4ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel4ComboBox.SelectedValue.ToString(), out saveMiscChoiceId04Value);
                    dr.V04 = this.miscCatalogChoiceItemValue04;
                    dr.MIChoiceID04 = saveMiscChoiceId04Value;
                    dr.MIChoice04Name = this.MILabel4ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel5TextBox.Text.Trim()) && this.MILabel5TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel5TextBox.Text.Trim(), out saveMifields5Value);
                    dr.V05 = saveMifields5Value;
                }
                else if (this.MILabel5ComboBox.Visible && this.MILabel5ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel5ComboBox.SelectedValue.ToString(), out saveMiscChoiceId05Value);
                    dr.V05 = this.miscCatalogChoiceItemValue05;
                    dr.MIChoiceID05 = saveMiscChoiceId05Value;
                    dr.MIChoice05Name = this.MILabel5ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel6TextBox.Text.Trim()) && this.MILabel6TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel6TextBox.Text.Trim(), out saveMifields6Value);
                    dr.V06 = saveMifields6Value;
                }
                else if (this.MILabel6ComboBox.Visible && this.MILabel6ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel6ComboBox.SelectedValue.ToString(), out saveMiscChoiceId06Value);
                    dr.V06 = this.miscCatalogChoiceItemValue06;
                    dr.MIChoiceID06 = saveMiscChoiceId06Value;
                    dr.MIChoice06Name = this.MILabel6ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel7TextBox.Text.Trim()) && this.MILabel7TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel7TextBox.Text.Trim(), out saveMifields7Value);
                    dr.V07 = saveMifields7Value;
                }
                else if (this.MILabel7ComboBox.Visible && this.MILabel7ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel7ComboBox.SelectedValue.ToString(), out saveMiscChoiceId07Value);
                    dr.V07 = this.miscCatalogChoiceItemValue07;
                    dr.MIChoiceID07 = saveMiscChoiceId07Value;
                    dr.MIChoice07Name = this.MILabel7ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel8TextBox.Text.Trim()) && this.MILabel8TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel8TextBox.Text.Trim(), out saveMifields8Value);
                    dr.V08 = saveMifields8Value;
                }
                else if (this.MILabel8ComboBox.Visible && this.MILabel8ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel8ComboBox.SelectedValue.ToString(), out saveMiscChoiceId08Value);
                    dr.V08 = this.miscCatalogChoiceItemValue08;
                    dr.MIChoiceID08 = saveMiscChoiceId08Value;
                    dr.MIChoice08Name = this.MILabel8ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel9TextBox.Text.Trim()) && this.MILabel9TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel9TextBox.Text.Trim(), out saveMifields9Value);
                    dr.V09 = saveMifields9Value;
                }
                else if (this.MILabel9ComboBox.Visible && this.MILabel9ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel9ComboBox.SelectedValue.ToString(), out saveMiscChoiceId09Value);
                    dr.V09 = this.miscCatalogChoiceItemValue09;
                    dr.MIChoiceID09 = saveMiscChoiceId09Value;
                    dr.MIChoice09Name = this.MILabel9ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel10TextBox.Text.Trim()) && this.MILabel10TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel10TextBox.Text.Trim(), out saveMifields10Value);
                    dr.V10 = saveMifields10Value;
                }
                else if (this.MILabel10ComboBox.Visible && this.MILabel10ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel10ComboBox.SelectedValue.ToString(), out saveMiscChoiceId10Value);
                    dr.V10 = this.miscCatalogChoiceItemValue10;
                    dr.MIChoiceID10 = saveMiscChoiceId10Value;
                    dr.MIChoice10Name = this.MILabel10ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel11TextBox.Text.Trim()) && this.MILabel11TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel11TextBox.Text.Trim(), out saveMifields11Value);
                    dr.V11 = saveMifields11Value;
                }
                else if (this.MILabel11ComboBox.Visible && this.MILabel11ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel11ComboBox.SelectedValue.ToString(), out saveMiscChoiceId11Value);
                    dr.V11 = this.miscCatalogChoiceItemValue11;
                    dr.MIChoiceID11 = saveMiscChoiceId11Value;
                    dr.MIChoice11Name = this.MILabel11ComboBox.Text.Trim();
                }

                if (!string.IsNullOrEmpty(this.MILabel12TextBox.Text.Trim()) && this.MILabel12TextBox.Visible)
                {
                    decimal.TryParse(this.MILabel12TextBox.Text.Trim(), out saveMifields12Value);
                    dr.V12 = saveMifields12Value;
                }
                else if (this.MILabel12ComboBox.Visible && this.MILabel12ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.MILabel12ComboBox.SelectedValue.ToString(), out saveMiscChoiceId12Value);
                    dr.V12 = this.miscCatalogChoiceItemValue12;
                    dr.MIChoiceID12 = saveMiscChoiceId12Value;
                    dr.MIChoice12Name = this.MILabel12ComboBox.Text.Trim();
                }

                //// Get depr control values and assign to dr
                dr.DeprWithPrimary = Convert.ToInt16(this.DeprWithPrimaryCheckBox.Checked);

                if (!string.IsNullOrEmpty(this.DeprYearTextBox.Text.Trim()))
                {
                    dr.DeprYearin = Convert.ToInt16(this.DeprYearTextBox.Text.Trim());
                }

                if (this.DeprQualityComboBox.SelectedValue != null)
                {
                    decimal tempDeprQuality;
                    decimal.TryParse(this.DeprQualityComboBox.SelectedValue.ToString(), out tempDeprQuality);
                    dr.DeprQuality = tempDeprQuality;
                }

                if (this.DeprConditionComboBox.SelectedValue != null)
                {
                    decimal tempDeprCondition;
                    decimal.TryParse(this.DeprConditionComboBox.SelectedValue.ToString(), out tempDeprCondition);
                    dr.DeprCondition = tempDeprCondition;
                }

                if (!string.IsNullOrEmpty(this.DeprEconLifeTextBox.Text.Trim()))
                {
                    byte tempDeprEconLife;
                    byte.TryParse(this.DeprEconLifeTextBox.Text.Trim(), out tempDeprEconLife);
                    dr.DeprEconLife = tempDeprEconLife;
                }

                if (!string.IsNullOrEmpty(this.DeprEffectiveAgeTextBox.Text.Trim()))
                {
                    byte tempDeprEffectiveAge;
                    byte.TryParse(this.DeprEffectiveAgeTextBox.Text.Trim(), out tempDeprEffectiveAge);
                    dr.DeprEAge = tempDeprEffectiveAge;
                }

                if (!string.IsNullOrEmpty(this.DeprPhysPercentTextBox.Text.Trim()))
                {
                    dr.DeprPercent = this.DeprPhysPercentTextBox.DecimalTextBoxValue;
                }

                if (this.DeprFuncDefCategoryComboBox.SelectedValue != null)
                {
                    byte tempDeprFuncCategory;
                    byte.TryParse(this.DeprFuncDefCategoryComboBox.SelectedValue.ToString(), out tempDeprFuncCategory);
                    dr.DeprFuncID = tempDeprFuncCategory;
                }

                if (!string.IsNullOrEmpty(this.DeprFuncPercentTextBox.Text.Trim()))
                {
                    dr.DeprFuncPercent = this.DeprFuncPercentTextBox.DecimalTextBoxValue;
                }

                if (!string.IsNullOrEmpty(this.DeprBaseCostTextBox.Text.Trim()))
                {
                    int tempBaseValue;
                    int.TryParse(this.DeprBaseCostTextBox.DecimalTextBoxValue.ToString(), out tempBaseValue);
                    dr.ValueBase = tempBaseValue;
                }

                if (!string.IsNullOrEmpty(this.DeprPhysTextBox.Text.Trim()))
                {
                    int tempDeprPhys;
                    int.TryParse(this.DeprPhysTextBox.DecimalTextBoxValue.ToString().Trim(), out tempDeprPhys);
                    dr.PhysDepr = tempDeprPhys;
                }

                if (!string.IsNullOrEmpty(this.DeprFuncTextBox.Text.Trim()))
                {
                    int tempDeprFunc;
                    int.TryParse(this.DeprFuncTextBox.DecimalTextBoxValue.ToString(), out tempDeprFunc);
                    dr.FuncDepr = tempDeprFunc;
                }

                if (!string.IsNullOrEmpty(this.DeprOverrideTextBox.Text.Trim()))
                {
                    int tempValueOverride;
                    int.TryParse(this.DeprOverrideTextBox.DecimalTextBoxValue.ToString(), out tempValueOverride);
                    dr.ValueOveride = tempValueOverride;
                }

                if (!string.IsNullOrEmpty(this.DeprFinalValueTextBox.Text.Trim()))
                {
                    int tempValueFinal;
                    int.TryParse(this.DeprFinalValueTextBox.DecimalTextBoxValue.ToString(), out tempValueFinal);
                    dr.ValueFinal = tempValueFinal;
                }

                dr.ValueSliceID = this.valueSliceId;

                this.miscImprovementOverviewData.ListMiscImprovementsNew.Rows.Add(dr);

                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    savedMid = this.form36011Control.WorkItem.F36011_SaveMiscImprovement(-999, TerraScanCommon.GetXmlString(this.miscImprovementOverviewData.ListMiscImprovementsNew.Copy()), TerraScan.Common.TerraScanCommon.UserId);
                }
                else
                {
                    savedMid = this.form36011Control.WorkItem.F36011_SaveMiscImprovement(this.currentMiscMid, TerraScanCommon.GetXmlString(this.miscImprovementOverviewData.ListMiscImprovementsNew.Copy()), TerraScan.Common.TerraScanCommon.UserId);
                }

                ////when the returned value is -1 then the Value exists the max limit
                if (savedMid == -1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F36011validationMessage"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return savedMid;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Usde to Load the Misc calucaltion Grid
        /// </summary>
        /// <param name="newModeValue">newModeValue</param>
        private void LoadMiscCalucaltioGrid(bool newModeValue)
        {
            DataSet tempdataset = new DataSet();
            DataTable currentlistMiscImprovementsDataTable = new DataTable("ListMiscImprovements");
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V01", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V02", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V03", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V04", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V05", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V06", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V07", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V08", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V09", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V10", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V11", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("V12", typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("FormulaQulatity"), typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn(SharedFunctions.GetResourceString("FormulaCondition"), typeof(decimal)));
            currentlistMiscImprovementsDataTable.Columns.Add(new DataColumn("VFormula", typeof(decimal)));

            DataRow dr = currentlistMiscImprovementsDataTable.NewRow();

            currentlistMiscImprovementsDataTable.Rows.Add(dr);

            tempdataset.Tables.Add(currentlistMiscImprovementsDataTable);

            this.MiscCalucationGrid.DataSource = tempdataset;

            if (!newModeValue)
            {
                this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V01Column];
                this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V02Column];
                this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V03Column];
                this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V04Column];
                this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V05Column];
                this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V06Column];
                this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V07Column];
                this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V08Column];
                this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V09Column];
                this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V10Column];
                this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V11Column];
                this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V12Column];
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V12Column];
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.V12Column];
                this.MiscCalucationGrid.Rows[0].Cells["VFormula"].Value = this.listMiscImprovementsDataTable.Rows[0][this.listMiscImprovementsDataTable.ValueBaseColumn];
            }
            else
            {
                this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = DBNull.Value;
                this.MiscCalucationGrid.Rows[0].Cells["VFormula"].Value = DBNull.Value;
            }
        }

        /// <summary>
        /// To set the from to grid
        /// </summary>
        private void SetFormula()
        {
            this.unsavedChangeExists = true;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V01"].Formula = this.miscfieldsFormula1;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V02"].Formula = this.miscfieldsFormula2;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V03"].Formula = this.miscfieldsFormula3;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V04"].Formula = this.miscfieldsFormula4;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V05"].Formula = this.miscfieldsFormula5;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V06"].Formula = this.miscfieldsFormula6;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V07"].Formula = this.miscfieldsFormula7;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V08"].Formula = this.miscfieldsFormula8;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V09"].Formula = this.miscfieldsFormula9;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V10"].Formula = this.miscfieldsFormula10;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V11"].Formula = this.miscfieldsFormula11;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["V12"].Formula = this.miscfieldsFormula12;
            this.MiscCalucationGrid.DisplayLayout.Bands[0].Columns["VFormula"].Formula = this.vformula;

            if (DeprQualityComboBox.SelectedValue != null)
            {
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = Convert.ToDouble(DeprQualityComboBox.SelectedValue.ToString());
            }
            else
            {
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = DBNull.Value;
            }

            if (DeprConditionComboBox.SelectedValue != null)
            {
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = Convert.ToDouble(DeprConditionComboBox.SelectedValue.ToString());
            }
            else
            {
                this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = DBNull.Value;
            }

        }

        /// <summary>
        /// To check req fielde are asin formula.
        /// </summary>
        /// <param name="currentFormula">The current formula.</param>
        /// <returns>returns a boolean value</returns>
        private bool ToCheckReqFieldeinFormula(string currentFormula)
        {
            bool isvalidValue = false;
            int isvalidText = 1;

            if (((currentFormula.IndexOf("[V01]") >= 0) || (currentFormula.IndexOf("[V02]") >= 0) ||
                (currentFormula.IndexOf("[V03]") >= 0) || (currentFormula.IndexOf("[V04]") >= 0) ||
                (currentFormula.IndexOf("[V05]") >= 0) || (currentFormula.IndexOf("[V06]") >= 0) ||
                (currentFormula.IndexOf("[V07]") >= 0) || (currentFormula.IndexOf("[V08]") >= 0) ||
                (currentFormula.IndexOf("[V09]") >= 0) || (currentFormula.IndexOf("[V10]") >= 0) ||
                (currentFormula.IndexOf("[V11]") >= 0) || (currentFormula.IndexOf("[V12]") >= 0) ||
                (currentFormula.IndexOf("[Condition]") >= 0) || (currentFormula.IndexOf("[Quality]") >= 0)))
            {
                if (currentFormula.IndexOf("[V01]") >= 0)
                {
                    if (this.MILabel1TextBox.Visible && !string.IsNullOrEmpty(this.MILabel1TextBox.Text.Trim()) || this.MILabel1ComboBox.Visible && this.MILabel1ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V02]") >= 0)
                {
                    if (this.MILabel2TextBox.Visible && !string.IsNullOrEmpty(this.MILabel2TextBox.Text.Trim()) || this.MILabel2ComboBox.Visible && this.MILabel2ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V03]") >= 0)
                {
                    if (this.MILabel3TextBox.Visible && !string.IsNullOrEmpty(this.MILabel3TextBox.Text.Trim()) || this.MILabel3ComboBox.Visible && this.MILabel3ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V04]") >= 0)
                {
                    if (this.MILabel4TextBox.Visible && !string.IsNullOrEmpty(this.MILabel4TextBox.Text.Trim()) || this.MILabel4ComboBox.Visible && this.MILabel4ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V05]") >= 0)
                {
                    if (this.MILabel5TextBox.Visible && !string.IsNullOrEmpty(this.MILabel5TextBox.Text.Trim()) || this.MILabel5ComboBox.Visible && this.MILabel5ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V06]") >= 0)
                {
                    if (this.MILabel6TextBox.Visible && !string.IsNullOrEmpty(this.MILabel6TextBox.Text.Trim()) || this.MILabel6ComboBox.Visible && this.MILabel6ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V07]") >= 0)
                {
                    if (this.MILabel7TextBox.Visible && !string.IsNullOrEmpty(this.MILabel7TextBox.Text.Trim()) || this.MILabel7ComboBox.Visible && this.MILabel7ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V08]") >= 0)
                {
                    if (this.MILabel8TextBox.Visible && !string.IsNullOrEmpty(this.MILabel8TextBox.Text.Trim()) || this.MILabel8ComboBox.Visible && this.MILabel8ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V09]") >= 0)
                {
                    if (this.MILabel9TextBox.Visible && !string.IsNullOrEmpty(this.MILabel9TextBox.Text.Trim()) || this.MILabel9ComboBox.Visible && this.MILabel9ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V10]") >= 0)
                {
                    if (this.MILabel10TextBox.Visible && !string.IsNullOrEmpty(this.MILabel10TextBox.Text.Trim()) || this.MILabel10ComboBox.Visible && this.MILabel10ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V11]") >= 0)
                {
                    if (this.MILabel11TextBox.Visible && !string.IsNullOrEmpty(this.MILabel11TextBox.Text.Trim()) || this.MILabel11ComboBox.Visible && this.MILabel11ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[V12]") >= 0)
                {
                    if (this.MILabel12TextBox.Visible && !string.IsNullOrEmpty(this.MILabel12TextBox.Text.Trim()) || this.MILabel12ComboBox.Visible && this.MILabel12ComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[Condition]") >= 0)
                {
                    if (this.DeprConditionComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                if (currentFormula.IndexOf("[Quality]") >= 0)
                {
                    if (this.DeprQualityComboBox.SelectedValue != null)
                    {
                        if (isvalidText > 0)
                        {
                            isvalidValue = true;
                        }
                        else
                        {
                            isvalidText = -1;
                            return false;
                        }
                    }
                }

                return isvalidValue;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// To Put New the misc improvements Values.
        /// </summary>
        private void NewMiscImprovements()
        {
            if (this.PermissionFiled.newPermission)
            {
                this.LockControls(true);
                // this.LockHeaderPartPanels(false);
                this.ControlLock(false);
                ////focus is set on new method
                this.CodeComboBox.Focus();
            }
            else
            {
                this.LockControls(false);
                this.ControlLock(true);
            }

            this.ClearMiscImprovementsControls();

            if (this.MiscImprovementsGridView.OriginalRowCount > 0)
            {
                TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, 0);
            }

            this.currentMiscMid = 0;
            this.currentMICodeId = 0;
            this.miscGridRowId = 0;
            this.MiscImprovementsGridView.Enabled = false;

            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
        }

        /// <summary>
        /// Cancels the misc improvements click.
        /// </summary>
        private void CancelMiscImprovementsClick()
        {
            this.LockControls(true);
            this.LockHeaderPartPanels(true);

            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.LoadEntireMiscImprovement();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetMiscImprovButton();
        }

        /// <summary>
        /// Reload the Misc Improvements form afther save or delete
        /// </summary>
        private void ReLoadMiscImprovements()
        {
            ////on edit mode attachment and comment button is disable and afther save these button are enabled
            this.AttachmentButton.Enabled = this.PermissionFiled.editPermission;
            this.CommentButton.Enabled = this.PermissionFiled.editPermission;

            this.LockControls(true);
            this.ControlLock(false);
            this.LoadEntireMiscImprovement();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetMiscImprovButton();

            if (!this.flagLoadOnProcess)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.MiscImprovementPictureBox.Height + 2;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.MiscImprovementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscImprovementPictureBox.Height, this.MiscImprovementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }

            #region SaveValueSlice Event

            this.AlertValueSliceHeader();

            #endregion SaveValueSlice Event
        }

        /// <summary>
        /// Usde to alert the value slice header
        /// </summary>
        private void AlertValueSliceHeader()
        {
            decimal resultAmount;
            Decimal.TryParse(this.TotalValueColumnLabel.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

            F35002SubFormSaveEventArgs subFormSaveEventArgs;
            subFormSaveEventArgs.type = 5;
            subFormSaveEventArgs.value = resultAmount;
            subFormSaveEventArgs.valueSliceId = this.valueSliceId;

            subFormSaveEventArgs.amount = resultAmount;
            this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));

            if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
            {
                int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
            }
        }

        /// <summary>
        /// Sets the misc improv button mode.
        /// </summary>
        private void SetMiscImprovButton()
        {
            if (this.listMIcodeDatatableRowsCount > 0)
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            }
            else
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            }
        }

        /// <summary>
        /// Saves button click method.
        /// </summary>
        private void SaveButtonClick()
        {
            int savedMIDValue;
            int currentRowIndex;

            ////to save the Misc Improvement records
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.PermissionFiled.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.PermissionFiled.newPermission))
            {
                if (!this.CheckForRequiredFields())
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.saveUncomplete = true;
                }
                else
                {
                    if (this.ValidateMiscImprovement())
                    {
                        savedMIDValue = this.SaveMiscImprovements();
                        if (savedMIDValue > 0 && !WSHelper.IsOnLineMode)
                            TerraScanCommon.AddFieldUseValues(savedMIDValue, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                        if (savedMIDValue >= 0)
                        {
                            this.ReLoadMiscImprovements();

                            ////get the current row index of the saved value 
                            currentRowIndex = this.miscImprovementsGridSource.Find(this.listMIcodeDatatable.MIDColumn.ColumnName, savedMIDValue);
                            if (currentRowIndex >= 0)
                            {
                                TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, currentRowIndex);
                                this.AssignMiscellaneousCommponents(currentRowIndex);
                            }

                            this.saveUncomplete = false;
                        }
                        else
                        {
                            this.saveUncomplete = true;
                        }

                        if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
                        {
                            int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
                        }
                    }
                    else
                    {
                        this.saveUncomplete = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the buttons.
        /// </summary>
        /// <param name="buttonActionMode">The button action mode.</param>
        private void SetButtons(TerraScanCommon.ButtonActionMode buttonActionMode)
        {
            switch (buttonActionMode)
            {
                case TerraScanCommon.ButtonActionMode.CancelMode:
                    {
                        this.AddButton.Enabled = this.PermissionFiled.newPermission;
                        this.UndoButton.Enabled = false;
                        this.RemoveButton.Enabled = this.PermissionFiled.deletePermission;
                        this.UpdateButton.Enabled = false;
                        this.btnCopyOrMove.Enabled = this.PermissionFiled.newPermission;
                        this.CatalogButton.Enabled = true;
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NewMode:
                    {
                        this.AddButton.Enabled = false;
                        this.UndoButton.Enabled = true;
                        this.RemoveButton.Enabled = false;
                        this.UpdateButton.Enabled = true;
                        this.btnCopyOrMove.Enabled = false;
                        this.CatalogButton.Enabled = false;
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.EditMode:
                    {
                        if (this.PermissionFiled.editPermission)
                        {
                            this.AddButton.Enabled = false;
                            this.UndoButton.Enabled = true;
                            this.RemoveButton.Enabled = false;
                            this.UpdateButton.Enabled = true;
                            this.btnCopyOrMove.Enabled = false;
                            this.CatalogButton.Enabled = true;
                        }

                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NullRecordMode:
                    {
                        this.AddButton.Enabled = this.PermissionFiled.newPermission;
                        this.AddButton.Focus();
                        this.UndoButton.Enabled = false;
                        this.RemoveButton.Enabled = false;
                        this.UpdateButton.Enabled = false;
                        this.btnCopyOrMove.Enabled = this.PermissionFiled.newPermission;
                        this.CatalogButton.Enabled = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// Applies the neagative sign.
        /// </summary>
        /// <param name="currentTextvalue">The current textvalue.</param>
        /// <param name="currentText">The current text.</param>
        /// <returns>string value</returns>
        private string ApplyNeagativeSign(string currentTextvalue, TerraScanTextBox currentText)
        {
            string actualstring = string.Empty;

            if (!string.IsNullOrEmpty(currentTextvalue))
            {
                if (currentTextvalue.Contains("-"))
                {
                    actualstring = "(" + currentTextvalue.Replace("-", "") + ")";
                    currentText.ForeColor = Color.Red;
                    return actualstring;
                }
                else
                {
                    currentText.ForeColor = Color.Black;
                    return currentTextvalue;
                }
            }
            else
            {
                currentText.ForeColor = Color.Black;
                return currentTextvalue;
            }
        }

        /// <summary>
        /// Assigns the calculated value.
        /// </summary>
        private void AssignCalculatedValue()
        {
            if (!string.IsNullOrEmpty(this.miscfieldsFormula1))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula1))
                {
                    this.MILabel1TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V01"].Value.ToString();
                }
                else
                {
                    this.MILabel1TextBox.Text = string.Empty;
                }

                this.MILabel1TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel1TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V01"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula2))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula2))
                {
                    this.MILabel2TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V02"].Value.ToString();
                }
                else
                {
                    this.MILabel2TextBox.Text = string.Empty;
                }

                this.MILabel2TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel2TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V02"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula3))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula3))
                {
                    this.MILabel3TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V03"].Value.ToString();
                }
                else
                {
                    this.MILabel3TextBox.Text = string.Empty;
                }

                this.MILabel3TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel3TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V03"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula4))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula4))
                {
                    this.MILabel4TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V04"].Value.ToString();
                }
                else
                {
                    this.MILabel4TextBox.Text = string.Empty;
                }

                this.MILabel4TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel4TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V04"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula5))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula5))
                {
                    this.MILabel5TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V05"].Value.ToString();
                }
                else
                {
                    this.MILabel5TextBox.Text = string.Empty;
                }

                this.MILabel5TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel5TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V05"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula6))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula6))
                {
                    this.MILabel6TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V06"].Value.ToString();
                }
                else
                {
                    this.MILabel6TextBox.Text = string.Empty;
                }

                this.MILabel6TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel6TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V06"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula7))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula7))
                {
                    this.MILabel7TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V07"].Value.ToString();
                }
                else
                {
                    this.MILabel7TextBox.Text = string.Empty;
                }

                this.MILabel7TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel7TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V07"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula8))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula8))
                {
                    this.MILabel8TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V08"].Value.ToString();
                }
                else
                {
                    this.MILabel8TextBox.Text = string.Empty;
                }

                this.MILabel8TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel8TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V08"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula9))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula9))
                {
                    this.MILabel9TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V09"].Value.ToString();
                }
                else
                {
                    this.MILabel9TextBox.Text = string.Empty;
                }

                this.MILabel9TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel9TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V09"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula10))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula10))
                {
                    this.MILabel10TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V10"].Value.ToString();
                }
                else
                {
                    this.MILabel10TextBox.Text = string.Empty;
                }

                this.MILabel10TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel10TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V10"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula11))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula11))
                {
                    this.MILabel11TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V11"].Value.ToString();
                }
                else
                {
                    this.MILabel11TextBox.Text = string.Empty;
                }

                this.MILabel11TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel11TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V11"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.miscfieldsFormula12))
            {
                if (this.ToCheckReqFieldeinFormula(this.miscfieldsFormula12))
                {
                    this.MILabel12TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V12"].Value.ToString();
                }
                else
                {
                    this.MILabel12TextBox.Text = string.Empty;
                }

                this.MILabel12TextBox.ForeColor = System.Drawing.Color.DarkGray;
            }
            else
            {
                this.MILabel12TextBox.Text = this.MiscCalucationGrid.Rows[0].Cells["V12"].Value.ToString();
            }

            if (!string.IsNullOrEmpty(this.vformula))
            {
                if (this.ToCheckReqFieldeinFormula(this.vformula))
                {
                    decimal tempBaseCostValue;
                    decimal.TryParse(this.MiscCalucationGrid.Rows[0].Cells["VFormula"].Value.ToString(), out tempBaseCostValue);
                    tempBaseCostValue = decimal.Round(tempBaseCostValue, 0, MidpointRounding.AwayFromZero);
                    ////tempBaseCostValue = Math.Round(tempBaseCostValue, 0, MidpointRounding.AwayFromZero);
                    this.DeprBaseCostTextBox.Text = tempBaseCostValue.ToString();
                    this.AssignValuetextBox(this.DeprOverrideTextBox.Text.Trim());
                }
                else
                {
                    this.DeprBaseCostTextBox.Text = string.Empty;
                    this.AssignValuetextBox(this.DeprOverrideTextBox.Text.Trim());
                }
            }

            this.unsavedChangeExists = false;
        }

        /// <summary>
        /// Populates the misc details.
        /// </summary>
        private void PopulateMiscDetails()
        {
            if (!string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()) && (this.CodeComboBox.SelectedValue != null))
            {
                int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);

                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    ////this condtiion is called to on new mode  where we have check only the new permisssion for the user
                    ////Here in new mode Micode is sent the get the header part
                    this.AssignValueForMiscImprovements(this.tempMiscCodeId, this.PermissionFiled.newPermission, true, true);

                    this.SetFormula();
                    this.AssignCalculatedValue();
                }
                else
                {
                    if (!this.flagLoadOnProcess)
                    {
                        ////this condtiion is called to on View mode  where we have get the new value for the particular mi code id
                        ////Here in View mode Micode is sent the get the header part with formula and decimal values (to newly fill the header part)
                        this.AssignValueForMiscImprovements(this.tempMiscCodeId, this.PermissionFiled.editPermission, true, true);

                        this.SetFormula();
                        this.AssignCalculatedValue();
                        this.ToEnableEditButtonInMasterForm();
                    }
                }
            }
        }

        #region Load Misc Fields With ComboBox Methods

        /// <summary>
        /// Loads the misc field with combo box1.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox1(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable1.NewRow();
            this.listMiscCatalogChoiceComboTable1.Clear();
            customRow[this.listMiscCatalogChoiceComboTable1.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable1.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable1.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable1.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable1.Rows.Count > 0)
            {
                this.MILabel1ComboBox.DataSource = this.listMiscCatalogChoiceComboTable1;
                this.MILabel1ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable1.ItemNameColumn.ColumnName;
                this.MILabel1ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable1.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box2.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox2(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable2.NewRow();
            this.listMiscCatalogChoiceComboTable2.Clear();
            customRow[this.listMiscCatalogChoiceComboTable2.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable2.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable2.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable2.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable2.Rows.Count > 0)
            {
                this.MILabel2ComboBox.DataSource = this.listMiscCatalogChoiceComboTable2;
                this.MILabel2ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable2.ItemNameColumn.ColumnName;
                this.MILabel2ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable2.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box3.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox3(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable3.NewRow();
            this.listMiscCatalogChoiceComboTable3.Clear();
            customRow[this.listMiscCatalogChoiceComboTable3.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable3.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable3.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable3.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable3.Rows.Count > 0)
            {
                this.MILabel3ComboBox.DataSource = this.listMiscCatalogChoiceComboTable3;
                this.MILabel3ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable3.ItemNameColumn.ColumnName;
                this.MILabel3ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable3.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box4.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox4(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable4.NewRow();
            this.listMiscCatalogChoiceComboTable4.Clear();
            customRow[this.listMiscCatalogChoiceComboTable4.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable4.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable4.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable4.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable4.Rows.Count > 0)
            {
                this.MILabel4ComboBox.DataSource = this.listMiscCatalogChoiceComboTable4;
                this.MILabel4ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable4.ItemNameColumn.ColumnName;
                this.MILabel4ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable4.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box5.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox5(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable5.NewRow();
            this.listMiscCatalogChoiceComboTable5.Clear();
            customRow[this.listMiscCatalogChoiceComboTable5.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable5.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable5.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable5.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable5.Rows.Count > 0)
            {
                this.MILabel5ComboBox.DataSource = this.listMiscCatalogChoiceComboTable5;
                this.MILabel5ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable5.ItemNameColumn.ColumnName;
                this.MILabel5ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable5.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box6.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox6(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable6.NewRow();
            this.listMiscCatalogChoiceComboTable6.Clear();
            customRow[this.listMiscCatalogChoiceComboTable6.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable6.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable6.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable6.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable6.Rows.Count > 0)
            {
                this.MILabel6ComboBox.DataSource = this.listMiscCatalogChoiceComboTable6;
                this.MILabel6ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable6.ItemNameColumn.ColumnName;
                this.MILabel6ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable6.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box7.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox7(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable7.NewRow();
            this.listMiscCatalogChoiceComboTable7.Clear();
            customRow[this.listMiscCatalogChoiceComboTable7.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable7.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable7.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable7.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable7.Rows.Count > 0)
            {
                this.MILabel7ComboBox.DataSource = this.listMiscCatalogChoiceComboTable7;
                this.MILabel7ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable7.ItemNameColumn.ColumnName;
                this.MILabel7ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable7.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box8.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox8(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable8.NewRow();
            this.listMiscCatalogChoiceComboTable8.Clear();
            customRow[this.listMiscCatalogChoiceComboTable8.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable8.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable8.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable8.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable8.Rows.Count > 0)
            {
                this.MILabel8ComboBox.DataSource = this.listMiscCatalogChoiceComboTable8;
                this.MILabel8ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable8.ItemNameColumn.ColumnName;
                this.MILabel8ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable8.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box9.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox9(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable9.NewRow();
            this.listMiscCatalogChoiceComboTable9.Clear();
            customRow[this.listMiscCatalogChoiceComboTable9.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable9.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable9.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable9.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable9.Rows.Count > 0)
            {
                this.MILabel9ComboBox.DataSource = this.listMiscCatalogChoiceComboTable9;
                this.MILabel9ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable9.ItemNameColumn.ColumnName;
                this.MILabel9ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable9.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box10.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox10(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable10.NewRow();
            this.listMiscCatalogChoiceComboTable10.Clear();
            customRow[this.listMiscCatalogChoiceComboTable10.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable10.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable10.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable10.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable10.Rows.Count > 0)
            {
                this.MILabel10ComboBox.DataSource = this.listMiscCatalogChoiceComboTable10;
                this.MILabel10ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable10.ItemNameColumn.ColumnName;
                this.MILabel10ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable10.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box11.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox11(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable11.NewRow();
            this.listMiscCatalogChoiceComboTable11.Clear();
            customRow[this.listMiscCatalogChoiceComboTable11.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable11.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable11.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable11.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable11.Rows.Count > 0)
            {
                this.MILabel11ComboBox.DataSource = this.listMiscCatalogChoiceComboTable11;
                this.MILabel11ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable11.ItemNameColumn.ColumnName;
                this.MILabel11ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable11.MIChoiceIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the misc field with combo box12.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        private void LoadMiscFieldWithComboBox12(int miscCodeId, int fieldNum)
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listMiscCatalogChoiceComboTable12.NewRow();
            this.listMiscCatalogChoiceComboTable12.Clear();
            customRow[this.listMiscCatalogChoiceComboTable12.MIChoiceIDColumn.ColumnName] = "0";
            customRow[this.listMiscCatalogChoiceComboTable12.ItemNameColumn.ColumnName] = string.Empty;
            this.listMiscCatalogChoiceComboTable12.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36012_ListMiscCatalogChoice(miscCodeId, fieldNum);
            this.listMiscCatalogChoiceComboTable12.Merge(this.miscImprovementOverviewData.ListMiscCatalogChoice);

            if (this.listMiscCatalogChoiceComboTable12.Rows.Count > 0)
            {
                this.MILabel12ComboBox.DataSource = this.listMiscCatalogChoiceComboTable12;
                this.MILabel12ComboBox.DisplayMember = this.listMiscCatalogChoiceComboTable12.ItemNameColumn.ColumnName;
                this.MILabel12ComboBox.ValueMember = this.listMiscCatalogChoiceComboTable12.MIChoiceIDColumn.ColumnName;
            }
        }

        #endregion Load Misc Fields With ComboBox Methods

        #region Assign Misc Fields With ComboBox Methods

        /// <summary>
        /// Assigns the misc field with combo box1.
        /// </summary>
        /// <param name="currentChoiceId1">The current choice id1.</param>
        private void AssignMiscFieldWithComboBox1(int currentChoiceId1)
        {
            if (this.listMiscCatalogChoiceComboTable1.Rows.Count > 0)
            {
                if (currentChoiceId1 >= 0)
                {
                    this.MILabel1ComboBox.SelectedValue = currentChoiceId1;
                }
                else
                {
                    this.MILabel1ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box2.
        /// </summary>
        /// <param name="currentChoiceId2">The current choice id2.</param>
        private void AssignMiscFieldWithComboBox2(int currentChoiceId2)
        {
            if (this.listMiscCatalogChoiceComboTable2.Rows.Count > 0)
            {
                if (currentChoiceId2 >= 0)
                {
                    this.MILabel2ComboBox.SelectedValue = currentChoiceId2;
                }
                else
                {
                    this.MILabel2ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box3.
        /// </summary>
        /// <param name="currentChoiceId3">The current choice id3.</param>
        private void AssignMiscFieldWithComboBox3(int currentChoiceId3)
        {
            if (this.listMiscCatalogChoiceComboTable3.Rows.Count > 0)
            {
                if (currentChoiceId3 >= 0)
                {
                    this.MILabel3ComboBox.SelectedValue = currentChoiceId3;
                }
                else
                {
                    this.MILabel3ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box4.
        /// </summary>
        /// <param name="currentChoiceId4">The current choice id4.</param>
        private void AssignMiscFieldWithComboBox4(int currentChoiceId4)
        {
            if (this.listMiscCatalogChoiceComboTable4.Rows.Count > 0)
            {
                if (currentChoiceId4 >= 0)
                {
                    this.MILabel4ComboBox.SelectedValue = currentChoiceId4;
                }
                else
                {
                    this.MILabel4ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box5.
        /// </summary>
        /// <param name="currentChoiceId5">The current choice id5.</param>
        private void AssignMiscFieldWithComboBox5(int currentChoiceId5)
        {
            if (this.listMiscCatalogChoiceComboTable5.Rows.Count > 0)
            {
                if (currentChoiceId5 >= 0)
                {
                    this.MILabel5ComboBox.SelectedValue = currentChoiceId5;
                }
                else
                {
                    this.MILabel5ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box6.
        /// </summary>
        /// <param name="currentChoiceId6">The current choice id6.</param>
        private void AssignMiscFieldWithComboBox6(int currentChoiceId6)
        {
            if (this.listMiscCatalogChoiceComboTable6.Rows.Count > 0)
            {
                if (currentChoiceId6 >= 0)
                {
                    this.MILabel6ComboBox.SelectedValue = currentChoiceId6;
                }
                else
                {
                    this.MILabel6ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box7.
        /// </summary>
        /// <param name="currentChoiceId7">The current choice id7.</param>
        private void AssignMiscFieldWithComboBox7(int currentChoiceId7)
        {
            if (this.listMiscCatalogChoiceComboTable7.Rows.Count > 0)
            {
                if (currentChoiceId7 >= 0)
                {
                    this.MILabel7ComboBox.SelectedValue = currentChoiceId7;
                }
                else
                {
                    this.MILabel7ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box8.
        /// </summary>
        /// <param name="currentChoiceId8">The current choice id8.</param>
        private void AssignMiscFieldWithComboBox8(int currentChoiceId8)
        {
            if (this.listMiscCatalogChoiceComboTable8.Rows.Count > 0)
            {
                if (currentChoiceId8 >= 0)
                {
                    this.MILabel8ComboBox.SelectedValue = currentChoiceId8;
                }
                else
                {
                    this.MILabel8ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box9.
        /// </summary>
        /// <param name="currentChoiceId9">The current choice id9.</param>
        private void AssignMiscFieldWithComboBox9(int currentChoiceId9)
        {
            if (this.listMiscCatalogChoiceComboTable9.Rows.Count > 0)
            {
                if (currentChoiceId9 >= 0)
                {
                    this.MILabel9ComboBox.SelectedValue = currentChoiceId9;
                }
                else
                {
                    this.MILabel9ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box10.
        /// </summary>
        /// <param name="currentChoiceId10">The current choice id10.</param>
        private void AssignMiscFieldWithComboBox10(int currentChoiceId10)
        {
            if (this.listMiscCatalogChoiceComboTable10.Rows.Count > 0)
            {
                if (currentChoiceId10 >= 0)
                {
                    this.MILabel10ComboBox.SelectedValue = currentChoiceId10;
                }
                else
                {
                    this.MILabel10ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box11.
        /// </summary>
        /// <param name="currentChoiceId11">The current choice id11.</param>
        private void AssignMiscFieldWithComboBox11(int currentChoiceId11)
        {
            if (this.listMiscCatalogChoiceComboTable11.Rows.Count > 0)
            {
                if (currentChoiceId11 >= 0)
                {
                    this.MILabel11ComboBox.SelectedValue = currentChoiceId11;
                }
                else
                {
                    this.MILabel11ComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the misc field with combo box12.
        /// </summary>
        /// <param name="currentChoiceId12">The current choice id12.</param>
        private void AssignMiscFieldWithComboBox12(int currentChoiceId12)
        {
            if (this.listMiscCatalogChoiceComboTable12.Rows.Count > 0)
            {
                if (currentChoiceId12 >= 0)
                {
                    this.MILabel12ComboBox.SelectedValue = currentChoiceId12;
                }
                else
                {
                    this.MILabel12ComboBox.SelectedIndex = -1;
                }
            }
        }

        #endregion Assign Misc Fields With ComboBox Methods

        #region Load Depr ComboBox Methods

        /// <summary>
        /// Loads the depr quality res combo box.
        /// </summary>
        private void LoadDeprQualityResComboBox()
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listDeprQualityResComboboxDatatable.NewRow();
            this.listDeprQualityResComboboxDatatable.Clear();
            customRow[this.listDeprQualityResComboboxDatatable.QualityColumn.ColumnName] = "0";
            customRow[this.listDeprQualityResComboboxDatatable.DescriptionColumn.ColumnName] = string.Empty;
            this.listDeprQualityResComboboxDatatable.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListQualityRes();
            this.listDeprQualityResComboboxDatatable.Merge(this.miscImprovementOverviewData.ListQualityRes);

            if (this.listDeprQualityResComboboxDatatable.Rows.Count > 0)
            {
                this.DeprQualityComboBox.DataSource = this.listDeprQualityResComboboxDatatable;
                this.DeprQualityComboBox.DisplayMember = this.listDeprQualityResComboboxDatatable.DescriptionColumn.ColumnName;
                this.DeprQualityComboBox.ValueMember = this.listDeprQualityResComboboxDatatable.QualityColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the depr condition combo box.
        /// </summary>
        private void LoadDeprConditionComboBox()
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listDeprConditionComboboxDatatable.NewRow();
            this.listDeprConditionComboboxDatatable.Clear();
            customRow[this.listDeprConditionComboboxDatatable.ConditionColumn.ColumnName] = "0";
            customRow[this.listDeprConditionComboboxDatatable.DescriptionColumn.ColumnName] = string.Empty;
            this.listDeprConditionComboboxDatatable.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListCondition();
            this.listDeprConditionComboboxDatatable.Merge(this.miscImprovementOverviewData.ListCondition);

            if (this.listDeprConditionComboboxDatatable.Rows.Count > 0)
            {
                this.DeprConditionComboBox.DataSource = this.listDeprConditionComboboxDatatable;
                this.DeprConditionComboBox.DisplayMember = this.listDeprConditionComboboxDatatable.DescriptionColumn.ColumnName;
                this.DeprConditionComboBox.ValueMember = this.listDeprConditionComboboxDatatable.ConditionColumn.ColumnName;
            }
        }

        /// <summary>
        /// Loads the depr functional category combo box.
        /// </summary>
        private void LoadDeprFunctionalCategoryComboBox()
        {
            ////To assign a empty row in the combo box
            DataRow customRow = this.listDeprFuncDefinedComboboxDatatable.NewRow();
            this.listDeprFuncDefinedComboboxDatatable.Clear();
            customRow[this.listDeprFuncDefinedComboboxDatatable.DeprFuncIDColumn.ColumnName] = "0";
            customRow[this.listDeprFuncDefinedComboboxDatatable.DescriptionColumn.ColumnName] = string.Empty;
            this.listDeprFuncDefinedComboboxDatatable.Rows.Add(customRow);

            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_ListDeprFuncCategory();
            this.listDeprFuncDefinedComboboxDatatable.Merge(this.miscImprovementOverviewData.ListDeprFuncCategory);

            if (this.listDeprFuncDefinedComboboxDatatable.Rows.Count > 0)
            {
                this.DeprFuncDefCategoryComboBox.DataSource = this.listDeprFuncDefinedComboboxDatatable;
                this.DeprFuncDefCategoryComboBox.DisplayMember = this.listDeprFuncDefinedComboboxDatatable.DescriptionColumn.ColumnName;
                this.DeprFuncDefCategoryComboBox.ValueMember = this.listDeprFuncDefinedComboboxDatatable.DeprFuncIDColumn.ColumnName;
            }
        }

        #endregion Load Depr ComboBox Methods

        #region Assign Depr ComboBox Methods

        /// <summary>
        /// Assigns the depr quality res combo box.
        /// </summary>
        /// <param name="currentQuality">The current quality.</param>
        private void AssignDeprQualityResComboBox(decimal currentQuality)
        {
            if (this.listDeprQualityResComboboxDatatable.Rows.Count > 0)
            {
                if (currentQuality >= 0)
                {
                    this.DeprQualityComboBox.SelectedValue = currentQuality;
                }
                else
                {
                    this.DeprQualityComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the depr condition combo box.
        /// </summary>
        /// <param name="currentCondition">The current condition.</param>
        private void AssignDeprConditionComboBox(decimal currentCondition)
        {
            if (this.listDeprConditionComboboxDatatable.Rows.Count > 0)
            {
                if (currentCondition >= 0)
                {
                    this.DeprConditionComboBox.SelectedValue = currentCondition;
                }
                else
                {
                    this.DeprConditionComboBox.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Assigns the depr functional category combo box.
        /// </summary>
        /// <param name="currentDeprFuncId">The current depr func id.</param>
        private void AssignDeprFunctionalCategoryComboBox(byte currentDeprFuncId)
        {
            if (this.listDeprFuncDefinedComboboxDatatable.Rows.Count > 0)
            {
                if (currentDeprFuncId >= 0)
                {
                    this.DeprFuncDefCategoryComboBox.SelectedValue = currentDeprFuncId;
                }
                else
                {
                    this.DeprFuncDefCategoryComboBox.SelectedIndex = -1;
                }
            }
        }

        #endregion Assign Depr ComboBox Methods

        /// <summary>
        /// Caluclate the value based on formula
        /// </summary>
        private void SetCalculatedValue()
        {
            if (!this.flagLoadOnProcess)
            {
                ////Set value in MiscCalucationGrid
                ////if (DeprQualityComboBox.SelectedValue != null)
                ////{
                ////    this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = Convert.ToDouble(DeprQualityComboBox.SelectedValue.ToString());
                ////}
                ////else
                ////{
                ////    this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = DBNull.Value;
                ////}

                ////if (DeprConditionComboBox.SelectedValue != null)
                ////{
                ////    this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = Convert.ToDouble(DeprConditionComboBox.SelectedValue.ToString());
                ////}
                ////else
                ////{
                ////    this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = DBNull.Value;
                ////}

                ////Assign formula for each columns in MiscCalucationGrid
                this.SetFormula();

                ////Recalculate value using UltraCalcManager
                this.UltraMIfieldsCalcManager.ReCalc(-1);

                ////Set calculated value on respective(Textbox/Combo) fields
                this.AssignCalculatedValue();

                ////Enable Form master save, cancel buttons
                this.ToEnableEditButtonInMasterForm();
            }

        }
        #endregion Methods

        #region Attachment And Comment Part

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        /// <param name="keyId">keyId</param>
        private void SetAdditionalOperationCount(int keyId)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       

            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
            ////check for valid registerid
            if (keyId > 0)
            {
                additionalOperationCountEntity.AttachmentCount = this.form36011Control.WorkItem.GetAttachmentCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form36011Control.WorkItem.GetCommentsCount(this.currentFormId, keyId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }
            else
            {
                additionalOperationCountEntity.AttachmentCount = 0;
                additionalOperationCountEntity.CommentCount = 0;
                additionalOperationCountEntity.HighPriority = false;
            }

            this.SetText(additionalOperationCountEntity);
        }

        /// <summary>
        /// Sets the attachment and comments count text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentButton.Text = SharedFunctions.GetResourceString("Attachment");
                }
                else
                {
                    this.AttachmentButton.Text = string.Concat(SharedFunctions.GetResourceString("Attachment"), "(", additionalOperationCountEntity.AttachmentCount, ")");
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentButton.Text = SharedFunctions.GetResourceString("Comment");
                }
                else
                {
                    this.CommentButton.Text = this.CommentButton.Text = string.Concat(SharedFunctions.GetResourceString("Comment"), "(", additionalOperationCountEntity.CommentCount, ")");
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    ////red color for high priority 
                    this.CommentButton.BackColor = Color.FromArgb(255, 0, 0);
                    this.CommentButton.CommentPriority = true;
                }
                else
                {
                    ////default brown color
                    this.CommentButton.BackColor = Color.FromArgb(174, 150, 94);
                    this.CommentButton.CommentPriority = false;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.currentFormId, this.currentMiscMid, this.currentFormId };

                Form attachmentForm = new Form();

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                {
                    attachmentForm = this.form36011Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.form36011Control.WorkItem);
                    attachmentForm.Tag = this.Tag;
                    if (attachmentForm != null)
                    {
                        attachmentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                        this.SetText(additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                object[] optionalParameter;

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                {
                    optionalParameter = new object[] { this.currentFormId, this.currentMiscMid, this.masterFormNo };

                    Form commentForm = new Form();
                    commentForm = this.form36011Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.form36011Control.WorkItem);
                    commentForm.Tag = this.currentFormId;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion Attachment And Comment Part

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36011_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.SetMaxLength();
                this.CustimizeMiscImprovementsGridView();
                this.CustimizeCodeComboBox();
                this.formNo = 36011;
                this.keyField = "MID";
                ////to get the roll year
                this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
                this.getRollYearConfigurationValue = this.form36011Control.WorkItem.GetConfigDetails("AP_RollYear");
                if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    this.currentApplicationRollyear = int.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                }

                this.LoadEntireMiscImprovement();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetMiscImprovButton();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load

        #region Events

        /// <summary>
        /// Handles the DataBindingComplete event of the MiscImprovementsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void MiscImprovementsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.MiscImprovementsGridView.OriginalRowCount > 0)
                {
                    int ttlcalcValue;
                    int ttlvalue;

                    int.TryParse(this.listMIcodeDatatable.Compute("SUM (ValueBase)", "MICodeID > 0").ToString(), out ttlcalcValue);
                    int.TryParse(this.listMIcodeDatatable.Compute("SUM (TotalValue)", "MICodeID > 0").ToString(), out ttlvalue);

                    this.EntireTotalCalcValueTextBox.Text = ttlcalcValue.ToString("#,##0");
                    this.EntireValueColumnTotalTextBox.Text = ttlvalue.ToString("#,##0");

                    this.TotalCalcValueColumnlabel.Text = this.EntireTotalCalcValueTextBox.Text.Trim();
                    this.TotalValueColumnLabel.Text = this.EntireValueColumnTotalTextBox.Text.Trim();
                }
                else
                {
                    this.TotalCalcValueColumnlabel.Text = string.Empty;
                    this.TotalValueColumnLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MiscImprovementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscImprovementPictureBox_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Handles the MouseHover event of the MiscImprovementPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MiscImprovementPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscImprovementsoverViewToolTip.SetToolTip(this.MiscImprovementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CodeCatalogButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CodeCatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if (!string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()) && this.CodeComboBox.SelectedValue != null)
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(31010);
                    formInfo.optionalParameters = new object[1];
                    formInfo.optionalParameters[0] = this.CodeComboBox.SelectedValue;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
                else
                {
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(31010);
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
        /// Handles the CellClick event of the MiscImprovementsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MiscImprovementsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.miscGridRowId != e.RowIndex)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        this.AssignMiscellaneousCommponents(e.RowIndex);
                        if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
                        {
                            int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
                        }
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + SharedFunctions.GetResourceString("F36011FormName") + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (dialogResult == DialogResult.Yes)
                        {
                            this.SaveButtonClick();

                            TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, e.RowIndex);
                            this.AssignMiscellaneousCommponents(e.RowIndex);
                            if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
                            {
                                int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.CancelMiscImprovementsClick();

                            TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, e.RowIndex);
                            this.AssignMiscellaneousCommponents(e.RowIndex);
                            if ((this.CodeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.CodeComboBox.Text.Trim()))
                            {
                                int.TryParse(this.CodeComboBox.SelectedValue.ToString(), out this.tempMiscCodeId);
                            }
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, this.miscGridRowId);
                        }
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
        }

        /// <summary>
        /// Handles the KeyDown event of the MiscImprovementsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MiscImprovementsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int tempRowIndex = 0;
                int tempmiscGirdRowIndex = 0;

                tempRowIndex = ((DataGridView)sender).CurrentCell.RowIndex;

                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            if ((tempRowIndex + 1) <= this.MiscImprovementsGridView.OriginalRowCount - 1)
                            {
                                tempmiscGirdRowIndex = tempRowIndex + 1;

                                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                                {
                                    this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + SharedFunctions.GetResourceString("F36011FormName") + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        e.Handled = true;
                                        this.SaveButtonClick();
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempmiscGirdRowIndex);
                                        this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        e.Handled = true;
                                        this.CancelMiscImprovementsClick();
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempmiscGirdRowIndex);
                                        this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.Cancel)
                                    {
                                        e.Handled = true;
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempRowIndex);
                                    }
                                }
                            }

                            break;
                        }

                    case Keys.Up:
                        {
                            if ((tempRowIndex - 1) >= 0)
                            {
                                tempmiscGirdRowIndex = tempRowIndex - 1;

                                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                                {
                                    this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + SharedFunctions.GetResourceString("F36011FormName") + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        e.Handled = true;
                                        this.SaveButtonClick();
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempmiscGirdRowIndex);
                                        this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        e.Handled = true;
                                        this.CancelMiscImprovementsClick();
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempmiscGirdRowIndex);
                                        this.AssignMiscellaneousCommponents(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.Cancel)
                                    {
                                        e.Handled = true;
                                        TerraScanCommon.SetDataGridViewPosition(this.MiscImprovementsGridView, tempRowIndex);
                                    }
                                }
                            }

                            break;
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
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the CodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.tempMiscCodeId = -1;
                this.PopulateMiscDetails();
               this.LockCharacteristicsPanel(true);
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Misc fields textbox events

        /// <summary>
        /// Handles the TextChanged event of the MILabel1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel1TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel1TextBox.Text.Trim()) && this.MILabel1TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = this.MILabel1TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel2TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel2TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel2TextBox.Text.Trim()) && this.MILabel2TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = this.MILabel2TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel3TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel3TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel3TextBox.Text.Trim()) && this.MILabel3TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = this.MILabel3TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel4TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel4TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel4TextBox.Text.Trim()) && this.MILabel4TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = this.MILabel4TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel5TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel5TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel5TextBox.Text.Trim()) && this.MILabel5TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = this.MILabel5TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel6TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel6TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel6TextBox.Text.Trim()) && this.MILabel6TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = this.MILabel6TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel7TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel7TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel7TextBox.Text.Trim()) && this.MILabel7TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = this.MILabel7TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel8TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel8TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel8TextBox.Text.Trim()) && this.MILabel8TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = this.MILabel8TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel9TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel9TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel9TextBox.Text.Trim()) && this.MILabel9TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = this.MILabel9TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel10TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel10TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel10TextBox.Text.Trim()) && this.MILabel10TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = this.MILabel10TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel11TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel11TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel11TextBox.Text.Trim()) && this.MILabel11TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = this.MILabel11TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel12TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel12TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.IsNullOrEmpty(this.MILabel12TextBox.Text.Trim()) && this.MILabel12TextBox.Text.Trim() != "-")
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = this.MILabel12TextBox.Text.Trim();
                    }
                    else
                    {
                        this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = DBNull.Value;
                    }
                    this.isMiTextChange = true;
                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Misc fields textbox events

        #region Misc fields combo validating event

        /// <summary>
        /// Handles the Validating event of the MILabelComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void MILabelComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;

                if (this.unsavedChangeExists)
                {
                    this.AssignCalculatedValue();
                }

                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Misc fields combo validating event

        #region Misc fields combo text changed events

        /// <summary>
        /// Handles the TextChanged event of the MILabel1ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel1ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue01 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel1ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel1ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable1.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable1.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue01);
                            this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = this.miscCatalogChoiceItemValue01;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel2ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel2ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue02 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel2ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel2ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable2.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable2.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue02);
                            this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = this.miscCatalogChoiceItemValue02;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel3ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel3ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue03 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel3ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel3ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable3.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable3.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue03);
                            this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = this.miscCatalogChoiceItemValue03;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel4ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel4ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue04 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel4ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel4ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable4.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable4.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue04);
                            this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = this.miscCatalogChoiceItemValue04;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel5ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel5ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue05 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel5ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel5ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable5.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable5.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue05);
                            this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = this.miscCatalogChoiceItemValue05;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel6ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel6ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue06 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel6ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel6ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable6.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable6.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue06);
                            this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = this.miscCatalogChoiceItemValue06;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel7ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel7ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue07 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel7ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel7ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable7.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable7.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue07);
                            this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = this.miscCatalogChoiceItemValue07;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel8ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel8ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue08 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel8ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel8ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable8.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable8.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue08);
                            this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = this.miscCatalogChoiceItemValue08;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel9ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel9ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue09 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel9ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel9ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable9.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable9.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue09);
                            this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = this.miscCatalogChoiceItemValue09;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel10ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel10ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue10 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel10ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel10ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable10.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable10.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue10);
                            this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = this.miscCatalogChoiceItemValue10;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel11ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel11ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue11 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel11ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel11ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable11.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable11.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue11);
                            this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = this.miscCatalogChoiceItemValue11;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the MILabel12ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel12ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue12 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel12ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel12ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable12.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable12.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue12);
                            this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = this.miscCatalogChoiceItemValue12;
                        }
                    }

                    this.SetFormula();
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Misc fields combo text changed events

        #region Misc fields combo selected value changed events

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel1ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel1ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue01 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel1ComboBox.Text.Trim()) && this.MILabel1ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel1ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel1ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable1.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable1.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue01);
                                this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = this.miscCatalogChoiceItemValue01;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel2ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel2ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue02 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel2ComboBox.Text.Trim()) && this.MILabel2ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel2ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel2ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable2.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable2.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue02);
                                this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = this.miscCatalogChoiceItemValue02;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel3ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel3ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue03 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel3ComboBox.Text.Trim()) && this.MILabel3ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel3ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel3ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable3.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable3.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue03);
                                this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = this.miscCatalogChoiceItemValue03;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel4ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel4ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue04 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel4ComboBox.Text.Trim()) && this.MILabel4ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel4ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel4ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable4.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable4.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue04);
                                this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = this.miscCatalogChoiceItemValue04;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel5ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel5ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue05 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel5ComboBox.Text.Trim()) && this.MILabel5ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel5ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel5ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable5.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable5.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue05);
                                this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = this.miscCatalogChoiceItemValue05;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel6ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel6ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue06 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel6ComboBox.Text.Trim()) && this.MILabel6ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel6ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel6ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable6.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable6.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue06);
                                this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = this.miscCatalogChoiceItemValue06;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel7ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel7ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue07 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel7ComboBox.Text.Trim()) && this.MILabel7ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel7ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel7ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable7.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable7.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue07);
                                this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = this.miscCatalogChoiceItemValue07;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel8ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel8ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue08 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel8ComboBox.Text.Trim()) && this.MILabel8ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel8ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel8ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable8.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable8.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue08);
                                this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = this.miscCatalogChoiceItemValue08;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel9ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel9ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue09 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel9ComboBox.Text.Trim()) && this.MILabel9ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel9ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel9ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable9.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable9.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue09);
                                this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = this.miscCatalogChoiceItemValue09;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel10ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel10ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue10 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel10ComboBox.Text.Trim()) && this.MILabel10ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel10ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel10ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable10.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable10.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue10);
                                this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = this.miscCatalogChoiceItemValue10;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel11ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel11ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue11 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel11ComboBox.Text.Trim()) && this.MILabel11ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel11ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel11ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable11.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable11.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue11);
                                this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = this.miscCatalogChoiceItemValue11;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the MILabel12ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel12ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.miscCatalogChoiceItemValue12 = 0;
                    this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = DBNull.Value;

                    if (!string.IsNullOrEmpty(this.MILabel12ComboBox.Text.Trim()) && this.MILabel12ComboBox.SelectedValue != null)
                    {
                        if (!int.Equals(this.MILabel12ComboBox.SelectedValue, 0))
                        {
                            string filterCond = "MIChoiceID = " + this.MILabel12ComboBox.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable12.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable12.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue12);
                                this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = this.miscCatalogChoiceItemValue12;
                            }
                        }
                    }
                    if (this.isMiTextChange)
                    {
                        this.RecalcMiscImprovement();
                    }
                    this.isMiTextChange = false;
                    this.SetFormula();
                    this.UltraMIfieldsCalcManager.ReCalc(-1);
                    this.ToEnableEditButtonInMasterForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Misc fields combo selected value changed events

        #region Misc fields combo leave events

        /// <summary>
        /// Handles the Leave event of the MILabel1ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel1ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue01 <= 0 && this.MILabel1ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel1ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel1ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable1.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable1.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue01);
                            this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = this.miscCatalogChoiceItemValue01;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue01 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V01"].Value = DBNull.Value;
                            this.MILabel1ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel1ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel2ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel2ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue02 <= 0 && this.MILabel2ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel2ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel2ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable2.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable2.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue02);
                            this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = this.miscCatalogChoiceItemValue02;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue02 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V02"].Value = DBNull.Value;
                            this.MILabel2ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel2ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel3ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel3ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue03 <= 0 && this.MILabel3ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel3ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel3ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable3.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable3.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue03);
                            this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = this.miscCatalogChoiceItemValue03;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue03 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V03"].Value = DBNull.Value;
                            this.MILabel3ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel3ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel4ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel4ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue04 <= 0 && this.MILabel4ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel4ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel4ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable4.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable4.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue04);
                            this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = this.miscCatalogChoiceItemValue04;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue04 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V04"].Value = DBNull.Value;
                            this.MILabel4ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel4ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel5ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel5ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue05 <= 0 && this.MILabel5ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel5ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel5ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable5.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable5.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue05);
                            this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = this.miscCatalogChoiceItemValue05;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue05 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V05"].Value = DBNull.Value;
                            this.MILabel5ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel5ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel6ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel6ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue06 <= 0 && this.MILabel6ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel6ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel6ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable6.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable6.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue06);
                            this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = this.miscCatalogChoiceItemValue06;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue06 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V06"].Value = DBNull.Value;
                            this.MILabel6ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel6ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel7ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel7ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue07 <= 0 && this.MILabel7ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel7ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel7ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable7.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable7.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue07);
                            this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = this.miscCatalogChoiceItemValue07;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue07 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V07"].Value = DBNull.Value;
                            this.MILabel7ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel7ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel8ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel8ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue08 <= 0 && this.MILabel8ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel8ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel8ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable8.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable8.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue08);
                            this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = this.miscCatalogChoiceItemValue08;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue08 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V08"].Value = DBNull.Value;
                            this.MILabel8ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel8ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel9ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel9ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue09 <= 0 && this.MILabel9ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel9ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel9ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable9.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable9.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue09);
                            this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = this.miscCatalogChoiceItemValue09;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue09 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V09"].Value = DBNull.Value;
                            this.MILabel9ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel9ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel10ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel10ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue10 <= 0 && this.MILabel10ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel10ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel10ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable10.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable10.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue10);
                            this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = this.miscCatalogChoiceItemValue10;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue10 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V10"].Value = DBNull.Value;
                            this.MILabel10ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel10ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel11ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel11ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue11 <= 0 && this.MILabel11ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel11ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel11ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable11.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable11.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue11);
                            this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = this.miscCatalogChoiceItemValue11;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue11 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V11"].Value = DBNull.Value;
                            this.MILabel11ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel11ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MILabel12ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabel12ComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.miscCatalogChoiceItemValue12 <= 0 && this.MILabel12ComboBox.SelectedValue == null)
                {
                    if (!string.IsNullOrEmpty(this.MILabel12ComboBox.Text.Trim()))
                    {
                        string filterCond = "ItemName = '" + this.MILabel12ComboBox.Text.Trim() + "'";
                        DataRow[] choiceRows = this.listMiscCatalogChoiceComboTable12.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            decimal.TryParse(choiceRows[0][this.listMiscCatalogChoiceComboTable12.ItemValueColumn].ToString(), out this.miscCatalogChoiceItemValue12);
                            this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = this.miscCatalogChoiceItemValue12;
                        }
                        else
                        {
                            this.miscCatalogChoiceItemValue12 = 0;
                            this.MiscCalucationGrid.Rows[0].Cells["V12"].Value = DBNull.Value;
                            this.MILabel12ComboBox.Text = string.Empty;
                        }
                        ///used for Recalc Misc Improvement.
                        this.RecalcMiscImprovement();

                        this.SetFormula();
                    }
                    else
                    {
                        this.MILabel12ComboBox.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Misc fields combo leave events

        /// <summary>
        /// MIs the label text box leave.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabelTextBoxLeave(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;

                if (this.unsavedChangeExists)
                {
                    this.AssignCalculatedValue();
                }
                if (this.isMiTextChange)
                {
                    this.RecalcMiscImprovement();
                }
                this.isMiTextChange = false;
                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the SelectionChangeCommitted event of the MILABELCOMBOBOX control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MILabelCombobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.isMiTextChange = true;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the CellFormatting event of the MiscImprovementsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void MiscImprovementsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                int outIntValue;

                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == this.MiscImprovementsGridView.Columns["MiscCalcValue"].Index || e.ColumnIndex == this.MiscImprovementsGridView.Columns["MiscValue"].Index || e.ColumnIndex == this.MiscImprovementsGridView.Columns["MiscDepr"].Index || e.ColumnIndex == this.MiscImprovementsGridView.Columns["MiscOverride"].Index)
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.MiscImprovementsGridView.Rows[e.RowIndex].Cells["MiscMID"].Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (int.TryParse(val, out outIntValue))
                            {
                                if (outIntValue.ToString().Contains("-"))
                                {
                                    e.Value = String.Concat("(", Decimal.Negate(outIntValue).ToString("#,##0"), ")");
                                    e.CellStyle.ForeColor = Color.Green;
                                }
                                else
                                {
                                    e.Value = outIntValue.ToString("#,##0");
                                    e.FormattingApplied = true;
                                }
                            }
                            else
                            {
                                e.Value = "0";
                            }
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }

                    if (e.ColumnIndex.Equals(this.MiscCode.Index))
                    {
                        DataGridViewCell cell = this.MiscImprovementsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        cell.ToolTipText = this.MiscImprovementsGridView.Rows[e.RowIndex].Cells[this.MICodeID.Name].Value.ToString();
                    }

                    if (e.ColumnIndex.Equals(this.MiscValue.Index))
                    {
                        DataGridViewCell cell = this.MiscImprovementsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        cell.ToolTipText = this.MiscImprovementsGridView.Rows[e.RowIndex].Cells[this.MiscMID.Name].Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F36011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36011_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.MiscImprovementPictureBox.Height;
                this.MiscImprovementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscImprovementPictureBox.Height, this.MiscImprovementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the TotalCalcValueColumnlabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TotalCalcValueColumnlabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscImprovementsoverViewToolTip.SetToolTip(this.TotalCalcValueColumnlabel, this.TotalCalcValueColumnlabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the TotalValueColumnLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TotalValueColumnLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscImprovementsoverViewToolTip.SetToolTip(this.TotalValueColumnLabel, this.TotalValueColumnLabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormulaSyntaxError event of the UltraMIfieldsCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaSyntaxErrorEventArgs"/> instance containing the event data.</param>
        private void UltraMIfieldsCalcManager_FormulaSyntaxError(object sender, FormulaSyntaxErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormulaCircularityError event of the UltraMIfieldsCalcManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaCircularityErrorEventArgs"/> instance containing the event data.</param>
        private void UltraMIfieldsCalcManager_FormulaCircularityError(object sender, FormulaCircularityErrorEventArgs e)
        {
            try
            {
                e.DisplayErrorMessage = false;
                MessageBox.Show(e.ErrorDisplayText, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        private void DeleteButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.PermissionFiled.deletePermission)
                {
                    if (this.DeleteMiscid())
                    {
                        this.ReLoadMiscImprovements();
                        this.CodeComboBox.Focus();
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
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.NewMiscImprovements();
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
        /// Handles the Click event of the UpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.SaveButtonClick();
                this.CodeComboBox.Focus();
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
        /// Handles the Click event of the UndoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UndoButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.CancelMiscImprovementsClick();
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
        /// Handles the Click event of the RemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.DeleteButton_Click();
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
        /// Handles the Click event of the AddStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.AddButton.Enabled)
                {
                    this.NewMiscImprovements();
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
        /// Handles the Click event of the UpdateToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.UpdateButton.Enabled)
                {
                    this.CodeComboBox.Focus();
                    this.SaveButtonClick();
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
        /// Handles the Click event of the UndoStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UndoStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.UndoButton.Enabled)
                {
                    this.CancelMiscImprovementsClick();
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
        /// Handles the Click event of the RemoveStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.RemoveButton.Enabled)
                {
                    this.DeleteButton_Click();
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

        #region Depr ComboBox Events



        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DeprFuncDefCategoryComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprFuncDefCategoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {

                // REMOVE the DeprFuncDefCategoryComboBox Function.

                //if (!this.flagLoadOnProcess)
                //{
                //    if (this.DeprFuncDefCategoryComboBox.SelectedValue != null)
                //    {
                //        string filterCond = "DeprFuncID = " + this.DeprFuncDefCategoryComboBox.SelectedValue.ToString();
                //        DataRow[] deprFuncDefRows = this.listDeprFuncDefinedComboboxDatatable.Select(filterCond);

                //        if (deprFuncDefRows.Length > 0)
                //        {
                //            this.DeprFuncPercentTextBox.Text = deprFuncDefRows[0][this.listDeprFuncDefinedComboboxDatatable.FuncPercentColumn].ToString();

                //        }
                //        else
                //        {
                //            this.DeprFuncPercentTextBox.Text = string.Empty;
                //        }

                //        this.ToEnableEditButtonInMasterForm();
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DeprQualityComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprQualityComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //if (!this.flagLoadOnProcess)
                //{
                //    ////Set value in MiscCalucationGrid
                //    if (DeprQualityComboBox.SelectedValue != null)
                //    {  
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = Convert.ToDouble(DeprQualityComboBox.SelectedValue.ToString());
                //    }
                //    else
                //    {
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaQulatity")].Value = DBNull.Value;
                //    }

                //    if (DeprConditionComboBox.SelectedValue != null)
                //    {
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = Convert.ToDouble(DeprConditionComboBox.SelectedValue.ToString());
                //    }
                //    else
                //    {
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = DBNull.Value;
                //    }
                if (this.CodeComboBox.SelectedIndex > -1)
                {
                    ////Calculate based on DeprQualityComboBox selected value
                    this.SetCalculatedValue();
                }

                //    ////Enable Form master save, cancel buttons
                //    this.ToEnableEditButtonInMasterForm();
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DeprConditionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprConditionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //if (!this.flagLoadOnProcess)
                //{
                //    ////Set value in MiscCalucationGrid
                //    if (DeprConditionComboBox.SelectedValue != null)
                //    {
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = Convert.ToDouble(DeprConditionComboBox.SelectedValue.ToString());
                //    }
                //    else
                //    {
                //        this.MiscCalucationGrid.Rows[0].Cells[SharedFunctions.GetResourceString("FormulaCondition")].Value = DBNull.Value;
                //    }

                if (this.CodeComboBox.SelectedIndex > -1)
                {
                    ////Calculate based on DeprQualityComboBox selected value
                    this.SetCalculatedValue();

                }
                if (!this.flagLoadOnProcess && !this.isComboTextChange)
                {
                    this.RecalcMiscImprovement();
                }

                //    ////Enable Form master save, cancel buttons
                //    this.ToEnableEditButtonInMasterForm();
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Depr ComboBox Events

        /// <summary>
        /// Handles the CheckedChanged event of the DeprWithPrimaryCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprWithPrimaryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.ToEnableEditButtonInMasterForm();
                if (!this.flagLoadOnProcess)
                {
                    this.isCheckedPrimary = true;
                    this.RecalcMiscImprovement();
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        //for the TSCO:#11442
        /// <summary>
        /// Used for RECALC MiscImprovement
        /// </summary>
        private void RecalcMiscImprovement()
        {
            bool withprimary;
            int? yearIn;
            string condition;
            int? economicLife;
            int? effectiveAge;
            decimal? physDeprPerc;
            decimal? funcDeprPerc;
            decimal? baseCost;
            decimal? physDepr;
            decimal? funcDepr;
            this.isOverride = false;
            if (this.DeprWithPrimaryCheckBox.Checked)
            {
                withprimary = true;
            }
            else
            {
                withprimary = false;
            }
            if (string.IsNullOrEmpty(this.DeprYearTextBox.Text))
            {
                yearIn = null;
            }
            else
            {
                int year;
                int.TryParse(this.DeprYearTextBox.Text.ToString(), out   year);
                yearIn = year;

            }
            if (this.DeprConditionComboBox.SelectedIndex == -1)
            {
                condition = null;
            }
            else
            {

                string val = this.DeprConditionComboBox.SelectedValue.ToString();
                condition = val;
            }
            if (string.IsNullOrEmpty(this.DeprEconLifeTextBox.Text))
            {
                economicLife = null;
            }
            else
            {
                int economy;
                int.TryParse(this.DeprEconLifeTextBox.Text.ToString(), out economy);
                economicLife = economy;
            }
            if (string.IsNullOrEmpty(this.DeprEffectiveAgeTextBox.Text))
            {
                effectiveAge = null;
            }
            else
            {
                int effectage;
                int.TryParse(this.DeprEffectiveAgeTextBox.Text.ToString(), out effectage);
                effectiveAge = effectage;
            }
            if (string.IsNullOrEmpty(this.DeprPhysPercentTextBox.Text))
            {
                physDeprPerc = null;

            }
            else
            {
                decimal physDeprPerce;
                decimal.TryParse(this.DeprPhysPercentTextBox.Text, out physDeprPerce);
                physDeprPerc = this.DeprPhysPercentTextBox.DecimalTextBoxValue;//physDeprPerce;
            }
            if (string.IsNullOrEmpty(this.DeprFuncPercentTextBox.Text))
            {
                funcDeprPerc = null;
            }
            else
            {
                decimal funcDeprPerce;
                decimal.TryParse(this.DeprFuncPercentTextBox.Text, out funcDeprPerce);
                funcDeprPerc = this.DeprFuncPercentTextBox.DecimalTextBoxValue;//funcDeprPerce;  
            }
            if (string.IsNullOrEmpty(this.DeprBaseCostTextBox.Text))
            {
                baseCost = null;
            }
            else
            {
                decimal baseCos;
                decimal.TryParse(this.DeprBaseCostTextBox.Text, out baseCos);
                baseCost = this.DeprBaseCostTextBox.DecimalTextBoxValue;//baseCos;  
            }
            if (string.IsNullOrEmpty(this.DeprPhysTextBox.Text))
            {
                physDepr = null;
            }
            else
            {
                decimal deprPhys;
                decimal.TryParse(this.DeprPhysTextBox.Text, out deprPhys);
                physDepr = this.DeprPhysTextBox.DecimalTextBoxValue;//deprPhys;
            }
            if (string.IsNullOrEmpty(this.DeprFuncTextBox.Text))
            {
                funcDepr = null;
            }
            else
            {
                decimal deprFunc;
                decimal.TryParse(this.DeprFuncTextBox.Text, out deprFunc);
                funcDepr = this.DeprFuncTextBox.DecimalTextBoxValue;//deprFunc;
            }
            this.miscImprovementOverviewData = this.form36011Control.WorkItem.F36011_RecalcMiscImprovement(withprimary, yearIn, condition, economicLife, effectiveAge, physDeprPerc, funcDeprPerc, baseCost, physDepr, funcDepr, this.valueSliceId, this.tempMiscCodeId);
            
            if (this.miscImprovementOverviewData.RecalcMiscImprovement.Rows.Count > 0)
            {
                this.DeprPhysPercentTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["PhysDeprPercent"].ToString();
                //Comment to avoid retaining its own value in funDep% field with not respective to checked field(with primary check box)
                if (isCheckedPrimary)
                {
                    //this.DeprFuncPercentTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["FuncDeprPercent"].ToString();
                }
                else
                {
                    this.DeprFuncPercentTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["FuncDeprPercent"].ToString();

                }
                this.DeprPhysTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["PhysDepr"].ToString();
                this.DeprFuncTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["FuncDepr"].ToString();
                this.DeprFinalValueTextBox.Text = this.miscImprovementOverviewData.RecalcMiscImprovement.Rows[0]["FinalValue"].ToString();
            }
            this.isCheckedPrimary = false;
        }



        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    TerraScanTextBox currentTextBox = (TerraScanTextBox)sender;
                    if (currentTextBox.Name.Equals(this.DeprOverrideTextBox.Name))
                    {
                        ////this.ApplyNeagativeSign(currentTextBox.Text.Trim(), currentTextBox);
                        this.AssignValuetextBox(this.DeprOverrideTextBox.Text.Trim());
                    }

                    if (currentTextBox.Name.Equals(this.DeprBaseCostTextBox.Name) || currentTextBox.Name.Equals(this.DeprPhysTextBox.Name)
                        || currentTextBox.Name.Equals(this.DeprFuncTextBox.Name) || currentTextBox.Name.Equals(this.DeprYearTextBox.Name)
                        || currentTextBox.Name.Equals(this.DeprEconLifeTextBox.Name) || currentTextBox.Name.Equals(this.DeprEffectiveAgeTextBox.Name)
                        || currentTextBox.Name.Equals(this.DeprPhysPercentTextBox.Name) || currentTextBox.Name.Equals(this.DeprFuncTextBox.Name)
                        || currentTextBox.Name.Equals(this.DeprFuncPercentTextBox.Name))
                    {
                        this.isTextChange = true;

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
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextLeave(object sender, EventArgs e)
        {
            try
            {
                if (this.isTextChange)
                {
                    this.RecalcMiscImprovement();
                }
                this.isTextChange = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the KeyPress event of the DeprTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void DeprTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ////check for (.) symbol
                if (e.KeyChar == 46)
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the CodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void CodeComboBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.CodeComboBox.SelectedValue != null && !this.tempMiscCodeId.Equals(this.CodeComboBox.SelectedValue))
            {
                this.PopulateMiscDetails();
            }
        }

        #endregion Events

        private void DeprConditionComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (this.CodeComboBox.SelectedIndex > -1)
                {
                    ////Calculate based on DeprQualityComboBox selected value
                    this.SetCalculatedValue();

                }
                if (!this.flagLoadOnProcess)
                {
                    this.isComboTextChange = true;
                    this.RecalcMiscImprovement();
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        private void CatalogButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ParentForm.Close();
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(31000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.valueSliceId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));

            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        private void MiscImprovementsGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grd = sender as DataGridView;
            if ((grd.Rows[e.RowIndex].Cells["IsMiscConfigured"].Value.ToString() != null))
            {
                if (grd.Rows[e.RowIndex].Cells["IsMiscConfigured"].Value.ToString().ToLower() == "true")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                if (grd.Rows[e.RowIndex].Cells["IsMiscConfigured"].Value.ToString().ToLower() == "false")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                }
            }
        }

        /// <summary>
        /// To copy or move MISC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyOrMove_Click(object sender, EventArgs e)
        {
            try
            {
                Form scheduleForm = new Form();
                object[] optionalParameter = new object[] {  this.valueSliceId,"3602" };
                scheduleForm = this.form36011Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3602, optionalParameter, this.form36011Control.WorkItem);
                if (scheduleForm != null)
                {
                    if (scheduleForm.ShowDialog() == DialogResult.OK)
                    {
                        //3602 Misc Improvement Form closed.
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
    }
}