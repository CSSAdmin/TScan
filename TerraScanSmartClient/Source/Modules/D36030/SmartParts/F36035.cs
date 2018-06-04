// --------------------------------------------------------------------------------------------
// <copyright file="F36035.cs" company="Congruent">
//       Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//  This file contains methods F36035 FS Land Codes..
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author            Description
// ----------       ---------          ---------------------------------------------------------
// 26/06/09         Sadha Shivudu      Implemented the TSCO 7395
// 18/09/09         Sadha Shivudu      Implemented the TSCO 3825
// 01/Dec/2010      Biju I.G.          Implemented the TSCO 9328
// 28082013         Purushotham        Implemented the TSC) 19278
//  20170217        Dhinesh            Alert the value slice header on land form Update
// *********************************************************************************/

namespace D36030
{
    #region Namespace

    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.Helper;

    #endregion Namespace

    /// <summary>
    /// F36035 class
    /// </summary>
    [SmartPart]
    public partial class F36035 : BaseSmartPart
    {
        #region Instance Variables

        #region Common Instance Variables

        /// <summary>
        /// Instance variable to hold the flag for load on process
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Instance variable to hold the form value slice id
        /// </summary>
        private int formValueSliceId;

        /// <summary>
        /// Instance variable to hold the current form id
        /// </summary>
        private int currentFormId;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// Instance variable to hold the page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Instance variable to hold the operation smartpart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// Instance variable to hold the form roll year
        /// </summary>
        private int formRollYear;

        /// <summary>
        /// Instance variable to hold the additional operation smartpart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// Instance variable to hold the form36035Controller
        /// </summary>
        private F36035Controller form36035Control;

        /// <summary>
        /// Instance variable to hold the land details dataset.
        /// </summary>
        private F36035LandData landDetailsDataSet;

        /// <summary>
        /// Instance variable to hold the current selectedRow index.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Instance variable to hold the landUniqueID value
        /// </summary>
        private int landUniqueID;

        /// <summary>
        /// Instance variable to hold the label foreColor value
        /// </summary>
        private Color standardLabelForeColor = System.Drawing.Color.FromArgb(51, 51, 153);

        /// <summary>
        /// Instance variable to hold the standard textbox fore color value
        /// </summary>
        private Color standardTextBoxForeColor = Color.Black;

        /// <summary>
        /// Instance variable to hold the standard textbox back color value
        /// </summary>
        private Color standardTextBoxBackColor = Color.White;

        /// <summary>
        /// Instance variable to hold the standard pane back color value
        /// </summary>
        private Color standardPanelBackColor = Color.White;

        /// <summary>
        /// Instance variable to hold the disabled textbox fore and back color value
        /// </summary>
        private Color disabledTextBoxForeAndBackColor = Color.LightGray;

        /// <summary>
        /// Instance variable to hold the disabled label fore color value
        /// </summary>
        private Color disabledLabelForeColor = Color.DarkGray;

        /// <summary>
        /// Instance variable to hold the disabled panel back color value
        /// </summary>
        private Color disabledPanelBackColor = Color.LightGray;

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        /// <summary>
        /// Instance variable to hold the min money field value
        /// </summary>
        private double minMoneyFieldValue = -922337203685477.58;

        /// <summary>
        /// Instance varaible to hold the base market field max value
        /// </summary>
        private double maxBaseMarketFieldValue = 999999999.99;

        /// <summary>
        /// Instance varaible to hold the base market field min value
        /// </summary>
        private double minBaseMarketFieldValue = -999999999.99;

        /// <summary>
        /// Instance variable to hold influenceGridRowIndex;
        /// </summary>
        private int influencerowindex;

        /// <summary>
        /// Instance variable to hold influenceGridColumnIndex;
        /// </summary>
        private int influencecolumnindex;

        #endregion Common Instance Variables

        #region Land Type Header Instance Variables

        /// <summary>
        /// Instance variable to hold the landType1 datatable
        /// </summary>
        private F36035LandData.ListLandType1DataTable listLandType1ComboDataTable = new F36035LandData.ListLandType1DataTable();

        /// <summary>
        /// Instance variable to hold the landType2 datatable
        /// </summary>
        private F36035LandData.ListLandType2DataTable listLandType2ComboDataTable = new F36035LandData.ListLandType2DataTable();

        /// <summary>
        /// Instance variable to hold the landType3 datatable
        /// </summary>
        private F36035LandData.ListLandType3DataTable listLandType3ComboDataTable = new F36035LandData.ListLandType3DataTable();

        /// <summary>
        /// instance variable to hold the landTypeId1 value
        /// </summary>
        private int landTypeId1Value;

        /// <summary>
        /// instance variable to hold the landTypeId2 value
        /// </summary>
        private int landTypeId2Value;

        /// <summary>
        /// instance variable to hold the landTypeId3 value
        /// </summary>
        private int landTypeId3Value;

        /// <summary>
        /// instance variable to hold the unitType value.
        /// </summary>
        private string landUnitType;

        /// <summary>
        /// instance variable to hold the ReportAs value
        /// </summary>
        private string reportAsValue;

        /// <summary>
        /// instance variable to hold the LandValues_UseMultiplier value.
        /// </summary>
        private decimal landCodeUseMultiplierValue;

        /// <summary>
        /// instance variable to hold the LandCodeValues_MrktMultiplier value.
        /// </summary>
        private decimal landCodeMarketMultiplierValue;

        /// <summary>
        /// instance variable to hold the report as label changed value.
        /// </summary>
        private bool flagReportAsLabelChanged;

        private string tempFrontText=string.Empty ;
        private string temp1FrontText = string.Empty;
        //private string tempUseAdjustmentTxt = string.Empty;
        //private string tempUseAdjustmentCombo = string.Empty;
        //private string tempReasonForUseAdjText = string.Empty;  
        /// <summary>
        /// Instance variable to hold the land details dataset.
        /// </summary>
        private F36035LandData listlandCodeTypeDataSet;

        private DataTable LandType1Data = new DataTable();
        private DataTable LandType2Data = new DataTable();
        private DataTable LandType3Data = new DataTable();
        private DataTable LandCodeData = new DataTable();
        private DataTable UseLandCodeData = new DataTable(); 
        #endregion Land Type Header Instance Variables

        #region Base Market Value Instance Variables

        /// <summary>
        /// Instance variable to hold the shapes combo box datatable
        /// </summary>
        private F36035LandData.ListShapesDataTable listShapesComboDataTable = new F36035LandData.ListShapesDataTable();

        /// <summary>
        /// Instance variable to hold the base adjustment combo data table
        /// </summary>
        private F36035LandData.ListLandCodeDataTable listBaseAdjustmentComboDataTable = new F36035LandData.ListLandCodeDataTable();

        /// <summary>
        /// Instance variable to hold the base adjustment types combo data table
        /// </summary>
        private F36035LandData.ListAdjustmentTypesDataTable listBaseAdjustmentTypesComboDataTable = new F36035LandData.ListAdjustmentTypesDataTable();

        /// <summary>
        /// instance variable to hold the land code base values data.
        /// </summary>
        private F36035LandData.Get_LandCodeBaseValueDataTable getLandCodeBaseValueDataTable = new F36035LandData.Get_LandCodeBaseValueDataTable();

        /// <summary>
        /// instance variable to hold the shape.
        /// </summary>
        private F36035LandData.LandShapesTableDataTable listShapeDetailsTable = new F36035LandData.LandShapesTableDataTable();

        /// <summary>
        /// instance variable to hold the land value curve formula
        /// </summary>
        private string landValueCurveFormula;

        /// <summary>
        /// instance variable to hold the LandValues_BaseValue
        /// </summary>
        private decimal landCodeBaseValue;

        /// <summary>
        /// instance variable to hold the alternateCode baseValue
        /// </summary>
        private decimal alternateLandCodeBaseValue;

        /// <summary>
        /// instance variable to hold the break1 value
        /// </summary>
        private decimal break1Value;

        /// <summary>
        /// instance variable to hold the valuePer1 value
        /// </summary>
        private decimal valuePer1Value;

        /// <summary>
        /// instance variable to hold the break2 value
        /// </summary>
        private decimal break2Value;

        /// <summary>
        /// instance variable to hold the valuePer2 value
        /// </summary>
        private decimal valuePer2Value;

        /// <summary>
        /// instance variable to hold the break3 value
        /// </summary>
        private decimal break3Value;

        /// <summary>
        /// instance variable to hold the valuePer3 value
        /// </summary>
        private decimal valuePer3Value;

        /// <summary>
        /// instance variable to hold the break4 value
        /// </summary>
        private decimal break4Value;

        /// <summary>
        /// instance variable to hold the valuePer4 value
        /// </summary>
        private decimal valuePer4Value;

        /// <summary>
        /// instance variable to hold the calculated BaseDollerPerUnit value
        /// </summary>
        private decimal calculatedBaseDollerPerUnitValue;

        /// <summary>
        /// instance variable to hold the calculated baseMarket value.
        /// </summary>
        private decimal calculatedBaseMarketValue;

        #endregion Base Market Value Instance Variables

        #region Market Value Influence Instance Variables

        /// <summary>
        /// Instance variable to hold the InfluenceType1 combo box datatable
        /// </summary>
        private F36035LandData.ListInfluenceTypeDataTable listInfluenceType1ComboDataTable = new F36035LandData.ListInfluenceTypeDataTable();

        /// <summary>
        /// Instance variable to hold the InfluenceType2 combo box datatable
        /// </summary>
        private F36035LandData.ListInfluenceTypeDataTable listInfluenceType2ComboDataTable = new F36035LandData.ListInfluenceTypeDataTable();

        /// <summary>
        /// Instance variable to hold the InfluenceType3 combo box datatable
        /// </summary>
        private F36035LandData.ListInfluenceTypeDataTable listInfluenceType3ComboDataTable = new F36035LandData.ListInfluenceTypeDataTable();

        /// <summary>
        /// instance variable to hold the influenceTypeId1 Value
        /// </summary>
        private int influenceTypeId1Value;

        /// <summary>
        /// instance variable to hold the influence1 value
        /// </summary>
        private decimal influence1Value;

        /// <summary>
        /// instance variable to hold the influenceType1 Value
        /// </summary>
        private byte influenceType1Value;

        /// <summary>
        /// instance variable to hold the influenceDescription1 value.
        /// </summary>
        private string influenceDescription1Value;

        /// <summary>
        /// instance variable to hold the influenceTypeId2 Value
        /// </summary>
        private int influenceTypeId2Value;

        /// <summary>
        /// instance variable to hold the influence2 value
        /// </summary>
        private decimal influence2Value;

        /// <summary>
        /// instance variable to hold the influenceType2 Value
        /// </summary>
        private byte influenceType2Value;

        /// <summary>
        /// instance variable to hold the influenceDescription2 value.
        /// </summary>
        private string influenceDescription2Value;

        /// <summary>
        /// instance variable to hold the influenceTypeId3 Value
        /// </summary>
        private int influenceTypeId3Value;

        /// <summary>
        /// instance variable to hold the influence3 value
        /// </summary>
        private decimal influence3Value;

        /// <summary>
        /// instance variable to hold the influenceType3 Value
        /// </summary>
        private byte influenceType3Value;

        /// <summary>
        /// instance variable to hold the influenceDescription3 value.
        /// </summary>
        private string influenceDescription3Value;

        /// <summary>
        /// Instance variable to hold the flag value of influence type1 changed
        /// </summary>
        private bool influenceType1Chaged;

        /// <summary>
        /// Instance variable to hold the flag value of influence type2 changed
        /// </summary>
        private bool influenceType2Chaged;

        /// <summary>
        /// Instance variable to hold the flag value of influence type3 changed
        /// </summary>
        private bool influenceType3Chaged;

        private bool formDataLoad = false;

        private F36035LandData.ListGridInfluencesDataTable filteredTable = new F36035LandData.ListGridInfluencesDataTable();

        /// <summary>
        /// Instance variable to hold the selected section values.
        /// </summary>
        private DataTable clearDataTable = new DataTable();

        private bool influenceTypeChanged;

        #endregion Market Value Influence Instance Variables

        #region Use Value Instance Variables

        /// <summary>
        /// Instance variable to hold the land program combo data table
        /// </summary>
        private F36035LandData.ListLandProgramDataTable listLandProgramComboDataTable = new F36035LandData.ListLandProgramDataTable();

        /// <summary>
        /// Instance variable to hold the use adjustment combo data table
        /// </summary>
        private F36035LandData.ListLandCodeDataTable listUseAdjustmentComboDataTable = new F36035LandData.ListLandCodeDataTable();

        /// <summary>
        /// Instance variable to hold the use adjustment types combo data table
        /// </summary>
        private F36035LandData.ListAdjustmentTypesDataTable listUseAdjustmentTypesComboDataTable = new F36035LandData.ListAdjustmentTypesDataTable();

        /// <summary>
        /// instance variable to hold the LandValues_UseBaseValue
        /// </summary>
        private decimal landCodeUseBaseValue;

        /// <summary>
        /// instance variable to hold the alternate LandCode UseBaseValue
        /// </summary>
        private decimal alternateLandCodeUseBaseValue;

        /// <summary>
        /// instance variable to hold the calculated UseBaseDollerPerUnit value
        /// </summary>
        private decimal calculatedUseBaseDollerPerUnitValue;

        #endregion Use Value Instance Variables

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
        /// instance variable to hold the form master edit permission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Instance Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F36035"/> class.
        /// </summary>
        public F36035()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F36035"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36035(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.currentFormId = formNo;

            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;

            this.formValueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);

            this.landDetailsDataSet = new F36035LandData();
            this.listlandCodeTypeDataSet = new F36035LandData();
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Occurs when [form slice_ section indicator click].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Occurs when [form slice_ resize].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Occurs when [D35000_ F35002_ sub form save].
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        /// <summary>
        /// Occurs when [form slice_ validation alert].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Enumerator for Adjustment Types
        /// </summary>
        private enum AdjustmentTypes
        {
            /// <summary>
            /// Value for None
            /// </summary>
            None = 0,

            /// <summary>
            /// Value for Alternate Land Code
            /// </summary>
            AlternateLandCode = 1,

            /// <summary>
            /// Value for Factor
            /// </summary>
            Factor = 2,

            /// <summary>
            /// Value for Unit
            /// </summary>
            UnitValue = 3,

            /// <summary>
            /// Value for Production
            /// </summary>
            Production = 4,

            /// <summary>
            /// Value for Additive
            /// </summary>
            Additive = 5,

            ////Added by Biju on 01-Dec-2010 to implement #9328
            /// <summary>
            /// Value for Total Value
            /// </summary>
            TotalValue = 6
        }

        /// <summary>
        /// Enumerator for Shape Types.
        /// </summary>
        private enum ShapeTypes
        {
            /// <summary>
            /// Value for Rectangular
            /// </summary>
            Rectangular = 0,

            /// <summary>
            /// Value for Irregular
            /// </summary>
            Irregular = 1
        }

        #endregion Enum

        #region Property

        /// <summary>
        /// Gets or sets the form36035 control.
        /// </summary>
        /// <value>The form36035 control.</value>
        [CreateNew]
        public F36035Controller Form36035Control
        {
            get { return this.form36035Control as F36035Controller; }
            set { this.form36035Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SlicePermissionReload&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                F36035LandData tempLandDataSet = new F36035LandData();

                if (this.formValueSliceId != eventArgs.Data.KeyId)
                {
                    // To check the invalid key id in set slice event subscription db call is set to F36035_ListLandDetails Method to check invalid key id
                    tempLandDataSet = this.form36035Control.WorkItem.F36035_ListLandDetails(eventArgs.Data.KeyId);
                }

                if (tempLandDataSet.GetValueSliceValidTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(tempLandDataSet.GetValueSliceValidTable.Rows[0][tempLandDataSet.GetValueSliceValidTable.IsOpenColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(tempLandDataSet.GetValueSliceValidTable.Rows[0][tempLandDataSet.GetValueSliceValidTable.IsOpenColumn.ColumnName].ToString()))
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
                        }
                        else
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.String&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            if (this.operationSmartPart == sender)
            {
                switch (e.Data)
                {
                    case "NEW":
                        this.NewButtonClick();
                        break;
                    case "SAVE":
                        this.SaveButtonClick();
                        break;
                    case "CANCEL":
                        this.CancelButtonClick();
                        break;
                    case "DELETE":
                        this.DeleteButtonClick();
                        break;
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission && this.formValueSliceId > 0)
            {
                this.form36035Control.WorkItem.F35001_DeleteValueSlice(this.formValueSliceId, TerraScan.Common.TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            // here form master save is not used but we are using this Event subscription to update the value slice header form slice
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            // here save is only used to update the description of the Value slice header
            // pls check with the this.CheckErrors(eventArgs.Data) method             
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;Infrastructure.Interface.AlertSliceOnClose&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                eventArgs.Data.FlagFormClose = this.CheckPageStatus();
            }
        }

        #endregion Event Subscription

        #region Protected Methods

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

        #endregion Protected Methods

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36035_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.flagLoadOnProcess = true;
                this.InitializeLandTypeComboBoxes();
                this.InitializeShapeComboBox();
                this.InitializeBaseAdjustmentTypeComboBox();
                this.InitializeBaseAdjustmentComboBox();
                this.InitializeUseAdjustmentTypeComboBox();
                this.InitializeUseAdjustmentComboBox();
                this.keyField = "LUID";
                //this.InitializeInfluenceType1ComboBox();
                //this.InitializeInfluenceType2ComboBox();
                //this.InitializeInfluenceType3ComboBox();
                this.InitializeLandProgramComboBox();
                this.CustomizeLandDetailsGridView();
                this.PopulateLandDetailsGridView();
                this.flagLoadOnProcess = false;
                this.formDataLoad = true; ;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the New button click.
        /// </summary>
        private void NewButtonClick()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);

            // Disable Attachment and Comment buttons
            this.CommentsdeckWorkspace.Enabled = false;

            this.flagLoadOnProcess = true;
            this.EnableLandFormPanels(true);
            this.ClearFormFields(true);
            this.LandCodeTextBox.Text = string.Empty; 
            if (this.flagReportAsLabelChanged)
            {
                this.ReportAsTextBox.Text = decimal.Zero.ToString();
                this.ReportAsTextBox.ForeColor = Color.Black;
                this.ReportAsTextBox.LockKeyPress = false;
                this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.ReportAsTextBox.TextAlign = HorizontalAlignment.Right;
            }

            //this.OnShapeComboChangeSetBaseMarketValueFields();
            this.OnBaseAdjustmentTypeChangeSetBaseMarketValueFields();
            this.OnProgramChangeSetUseValueFields();
            this.landDetailsDataSet.ListGridInfluences.Rows.Clear();
            this.filteredTable.Rows.Clear();
            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            this.InfluenceGridView.AllowSorting = false;
            this.InfluenceGridVerticalScroll.Visible = true;
            //this.InfluenceGridView.DataSource = this.landDetailsDataSet.ListGridInfluences.DefaultView;
            this.InitializeLandTypeComboBoxes();
            this.ControlLock(!this.PermissionFiled.newPermission);
            this.LandType1Combo.Focus();
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Handles the Save button click
        /// </summary>
        private void SaveButtonClick()
        {
            if (this.SaveLandSliceDetails())
            {
                this.AlertValueSliceHeader();
                this.temp1FrontText = string.Empty;
                this.tempFrontText = string.Empty;  
            }
            
        }

        /// <summary>
        /// Handles the Cancel button click
        /// </summary>       
        private void CancelButtonClick()
        {
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            if (this.LandDetailsDataGridView.NumRowsVisible >= 15)
            {
                this.SetSmartPartHeight(true);
            }

            this.flagLoadOnProcess = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.LandCodeTextBox.Text = string.Empty;  
            this.PopulateLandDetailsGridView();
            //this.listlandCodeTypeDataSet.ListLandCodeLandType.Clear();
            this.LandType1Combo.Focus();
            this.flagLoadOnProcess = false;
            this.LandDetailsGridViewPanel.Enabled = false;
            this.LandDetailsGridViewPanel.Enabled = true;
            this.LandDetailsDataGridView.Enabled = true;
        }

        /// <summary>
        /// Handles the Delete buttton click
        /// </summary>
        private void DeleteButtonClick()
        {
            if (this.LandDetailsDataGridView.CurrentRowIndex >= 0)
            {
                this.selectedRow = this.LandDetailsDataGridView.CurrentRowIndex;
            }

            if (this.selectedRow >= 0 && this.PermissionFiled.deletePermission)
            {
                if (int.TryParse(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString(), out this.landUniqueID))
                {
                    if (this.landUniqueID > 0)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.form36035Control.WorkItem.F36035_DeleteLandDetails(this.landUniqueID, TerraScan.Common.TerraScanCommon.UserId);
                            this.landUniqueID = 0;
                            this.selectedRow = 0;
                            this.flagLoadOnProcess = true;
                            this.ClearFormFields(false);
                            if (this.LandDetailsDataGridView.NumRowsVisible > 7 && this.LandDetailsDataGridView.NumRowsVisible <= 15)
                            {
                                int tempRowHeigh = 22;
                                this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.NumRowsVisible - 1;
                                this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height - tempRowHeigh;
                                this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height - tempRowHeigh;
                                this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height - tempRowHeigh;
                                this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height - tempRowHeigh;
                                this.LandPictureBox.Height = this.LandPictureBox.Height - tempRowHeigh;
                                this.FooterLeftpanel.Top = this.FooterLeftpanel.Top - tempRowHeigh;
                                this.Footerpanel.Top = this.Footerpanel.Top - tempRowHeigh;
                                this.Height = this.EntireLandFormPanel.Height;

                                // Resize the slice with new height
                                SliceResize sliceResize;
                                sliceResize.MasterFormNo = this.masterFormNo;
                                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                                sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
                                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                                this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                            }
                            else if (this.LandDetailsDataGridView.NumRowsVisible > 15)
                            {
                                this.SetSmartPartHeight(true);
                            }

                            this.LandDetailsDataGridView.RemoveDefaultSelection = true;
                            this.PopulateLandDetailsGridView();
                            this.flagLoadOnProcess = false;
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36035_Resize(object sender, EventArgs e)
        {
            try
            {
                this.Height = this.EntireLandFormPanel.Height;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.LandPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the DisplayLabelToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DisplayLabelToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label sourceLabel = (Label)sender;
                string tempValue = string.Empty;
                tempValue = sourceLabel.Text;
                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                float preferredwidth = sizeF.Width;

                if (preferredwidth > sourceLabel.Width)
                {
                    this.TotalValueToolTip.RemoveAll();
                    this.TotalValueToolTip.SetToolTip(sourceLabel, tempValue);
                }
                else
                {
                    this.TotalValueToolTip.RemoveAll();
                }

                graphics.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Land Types Header Field Events

        /// <summary>
        /// Handles the TextChanged event of the LandType1Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType1Combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId1Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType1Combo.Text.Trim()))
                    {
                        string filterCond = "LandType1 = '" + this.LandType1Combo.Text.Trim().Replace("'", "''") + "'";
                       // DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);
                        DataRow[] choiceRows = this.LandType1Data.Select(filterCond);
                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.LandType1Data.Columns[0]].ToString(), out this.landTypeId1Value);
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the LandType1Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType1Combo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId1Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType1Combo.Text.Trim()) && this.LandType1Combo.SelectedValue != null)
                    {
                        if (!int.Equals(this.LandType1Combo.SelectedValue, 0))
                        {
                            string filterCond = "LandTypeID1 = " + this.LandType1Combo.SelectedValue.ToString();
                            //DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.LandType1Data.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType1Data.Columns[0]].ToString(), out this.landTypeId1Value);
                               // int.TryParse(choiceRows[0][this.listLandType1ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId1Value);
                            }
                        }
                    }
                    this.GetLandType1LandCode();
                    //this.GetLandCode();
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void GetLandType2LandCode()
        {
            try
            {
                DataRow[] GetLandCode = this.listlandCodeTypeDataSet.ListLandCodeLandType.Select("LandTypeID1=" + this.landTypeId1Value + "AND LandTypeID2=" + this.landTypeId2Value);
                if (GetLandCode.Length > 0)
                {
                    this.LandType3Data.Clear();
                    DataTable landType3 = new DataTable();
                    landType3 = this.LandType3Data.Clone();
                    for (int i = 0; i < GetLandCode.Length; i++)
                    {
                        DataRow customLandType3Row = landType3.NewRow();
                        customLandType3Row["LandTypeID3"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandTypeID3"]].ToString();
                        customLandType3Row["LandType3"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandType3"]].ToString();
                        landType3.Rows.Add(customLandType3Row);
                    }

                    DataRow[] NullLandType3Row = landType3.Select("LandTypeID3=0");
                    if (NullLandType3Row.Length.Equals(0))
                    {
                        DataRow customLandTypRow = this.LandType3Data.NewRow();
                        customLandTypRow["LandTypeID3"] = "0";
                        customLandTypRow["LandType3"] = string.Empty;
                        this.LandType3Data.Rows.InsertAt(customLandTypRow, 0);
                    }
                    this.LandType3Data.Merge(landType3.DefaultView.ToTable(true, "LandTypeID3", "LandType3"));
                }
                else
                {
                    if(!string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim()))
                    {
                        this.LandCodeTextBox.Text = string.Empty;
                        this.BaseDollerPerUnitTextBox.Text = string.Empty;
                        this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;   
                    }
                    if (string.IsNullOrEmpty(this.LandType3Combo.SelectedText.Trim()))
                    {
                        this.LandType3Combo.SelectedText = string.Empty;
                        this.LandType3Data.Clear();
                        DataRow customLandType1Row = this.LandType3Data.NewRow();
                        customLandType1Row["LandTypeID3"] = "0";
                        customLandType1Row["LandType3"] = string.Empty;
                        this.LandType3Data.Rows.InsertAt(customLandType1Row, 0);
                        this.LandType3Combo.DataSource = this.LandType3Data;
                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        private void GetLandType1LandCode()
        {
            try
            {
                DataRow[] GetLandCode = this.listlandCodeTypeDataSet.ListLandCodeLandType.Select("LandTypeID1=" + this.landTypeId1Value);
                if (GetLandCode.Length > 0)
                {
                    this.LandType2Data.Clear();  
                    DataTable landType2 = new DataTable();
                    landType2 = this.LandType2Data.Clone();   
                    for(int i=0;i<GetLandCode.Length;i++)
                    {
                        DataRow customLandType2Row = landType2.NewRow();
                        customLandType2Row["LandTypeID2"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandTypeID2"]].ToString();
                        customLandType2Row["LandType2"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandType2"]].ToString();
                        landType2.Rows.Add(customLandType2Row);       
                    }
                    DataRow[] NullLandType2Row = landType2.Select("LandTypeID2=0");
                    if (NullLandType2Row.Length.Equals(0))
                    {
                        DataRow customLandTypeRow = this.LandType2Data.NewRow();
                        customLandTypeRow["LandTypeID2"] = "0";
                        customLandTypeRow["LandType2"] = string.Empty;
                        this.LandType2Data.Rows.InsertAt(customLandTypeRow, 0);
                    }
                    this.LandType2Data.Merge( landType2.DefaultView.ToTable(true, "LandTypeID2", "LandType2"));
                    this.LandType3Data.Clear();
                    DataTable landType3 = new DataTable();
                    landType3 = this.LandType3Data.Clone();
                    for (int i = 0; i < GetLandCode.Length; i++)
                    {
                        DataRow customLandType3Row = landType3.NewRow();
                        customLandType3Row["LandTypeID3"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandTypeID3"]].ToString();
                        customLandType3Row["LandType3"] = GetLandCode[i][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandType3"]].ToString();
                        landType3.Rows.Add(customLandType3Row);
                    }
                    DataRow[] NullLandType3Row = landType3.Select("LandTypeID3=0");
                    if (NullLandType3Row.Length.Equals(0))
                    {
                        DataRow customLandTypRow = this.LandType3Data.NewRow();
                        customLandTypRow["LandTypeID3"] = "0";
                        customLandTypRow["LandType3"] = string.Empty;
                        this.LandType3Data.Rows.InsertAt(customLandTypRow, 0);
                    }
                    this.LandType3Data.Merge(landType3.DefaultView.ToTable(true, "LandTypeID3", "LandType3"));
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim()))
                    {
                        this.LandCodeTextBox.Text = string.Empty;
                        this.BaseDollerPerUnitTextBox.Text = string.Empty;   
                    }
                    if(!string.IsNullOrEmpty(this.LandType2Combo.Text.Trim()  ))
                    {
                        this.LandType2Combo.Text = string.Empty;
                        
                        this.LandType2Data.Clear();
                        DataRow customLandType1Row = this.LandType2Data.NewRow();
                         customLandType1Row["LandTypeID2"] = "0";
                         customLandType1Row["LandType2"] = string.Empty;
                         this.LandType2Data.Rows.InsertAt(customLandType1Row, 0);
                         this.LandType2Combo.DataSource = this.LandType2Data;  
                    }
                    if(!string.IsNullOrEmpty(this.LandType3Combo.Text.Trim()))
                    {
                        this.LandType3Combo.SelectedText = string.Empty;
                        this.LandType3Data.Clear();
                        DataRow customLandType1Row = this.LandType3Data.NewRow();
                        customLandType1Row["LandTypeID3"] = "0";
                        customLandType1Row["LandType3"] = string.Empty;
                        this.LandType3Data.Rows.InsertAt(customLandType1Row, 0);
                        this.LandType3Combo.DataSource = this.LandType3Data;
                        
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles the Leave event of the LandType1Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType1Combo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.landTypeId1Value.Equals(0) && this.LandType1Combo.SelectedValue == null)
                    {
                        if (!string.IsNullOrEmpty(this.LandType1Combo.Text.Trim()))
                        {
                            string filterCond = "LandType1 = '" + this.LandType1Combo.Text.Trim().Replace("'", "''") + "'";
                            //DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.LandType1Data.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType1Data.Columns[0]].ToString(), out this.landTypeId1Value);
                            }
                            else
                            {
                                this.landTypeId1Value = 0;
                                this.LandType1Combo.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.LandType1Combo.SelectedIndex = 0;
                        }

                        //this.GetLandCode();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the LandType2Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType2Combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId2Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType2Combo.Text.Trim()))
                    {
                        string filterCond = "LandType2 = '" + this.LandType2Combo.Text.Trim().Replace("'", "''") + "'";
                       // DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);
                        DataRow[] choiceRows = this.LandType2Data.Select(filterCond);
                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.LandType2Data.Columns[0]].ToString(), out this.landTypeId2Value);
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the LandType2Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType2Combo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId2Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType2Combo.Text.Trim()) && this.LandType2Combo.SelectedValue != null)
                    {
                        if (!int.Equals(this.LandType2Combo.SelectedValue, 0))
                        {
                            //string filterCond = "LandTypeID = " + this.LandType2Combo.SelectedValue.ToString();
                            //DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);
                            string filterCond = "LandTypeID2 = " + this.LandType2Combo.SelectedValue.ToString();
                            DataRow[] choiceRows = this.LandType2Data.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType2Data.Columns[0]].ToString(), out this.landTypeId2Value);
                                //int.TryParse(choiceRows[0][this.listLandType2ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId2Value);
                            }
                        }
                    }

                    this.GetLandType2LandCode(); 
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LandType2Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType2Combo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.landTypeId2Value.Equals(0) && this.LandType2Combo.SelectedValue == null)
                    {
                        if (!string.IsNullOrEmpty(this.LandType2Combo.Text.Trim()))
                        {
                            string filterCond = "LandType2 = '" + this.LandType2Combo.Text.Trim().Replace("'", "''") + "'";
                            //DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.LandType2Data.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType2Data.Columns[0]].ToString(), out this.landTypeId2Value);
                            }
                            else
                            {
                                this.landTypeId2Value = 0;
                                this.LandType2Combo.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.LandType2Combo.SelectedIndex = 0;
                        }
                        this.GetLandType2LandCode();
                        //this.GetLandCode();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the LandType3Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType3Combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId3Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType3Combo.Text.Trim()))
                    {
                        string filterCond = "LandType3 = '" + this.LandType3Combo.Text.Trim().Replace("'", "''") + "'";
                        DataRow[] choiceRows = this.LandType3Data.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.LandType3Data.Columns[0]].ToString(), out this.landTypeId3Value);
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the LandType3Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType3Combo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId3Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType3Combo.Text.Trim()) && this.LandType3Combo.SelectedValue != null)
                    {
                        if (!int.Equals(this.LandType3Combo.SelectedValue, 0))
                        {
                            //string filterCond = "LandTypeID = " + this.LandType3Combo.SelectedValue.ToString();
                            //DataRow[] choiceRows = this.listLandType3ComboDataTable.Select(filterCond);
                            string filterCond = "LandTypeID3 = " + this.LandType3Combo.SelectedValue.ToString();
                            DataRow[] choiceRows = this.LandType3Data.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType3Data.Columns[0]].ToString(), out this.landTypeId3Value);
                                //int.TryParse(choiceRows[0][this.listLandType3ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId3Value);
                            }
                        }
                    }

                    this.GetLandTypeLandCode();
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void GetLandTypeLandCode()
        {
            try
            {
                DataRow[] GetLandCode = this.listlandCodeTypeDataSet.ListLandCodeLandType.Select("LandTypeID1=" + this.landTypeId1Value + "AND LandTypeID2=" + this.landTypeId2Value + "AND LandTypeID3=" + this.landTypeId3Value);
                if (GetLandCode.Length > 0)
                {
                    this.LandCodeTextBox.Text = GetLandCode[0][this.listlandCodeTypeDataSet.ListLandCodeLandType.Columns["LandCode"]].ToString();
                    this.GetLandCode(); 
                }
                else
                {
                    this.LandCodeTextBox.Text = string.Empty;
                    this.BaseDollerPerUnitTextBox.Text = string.Empty; 
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the LandType3Combo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandType3Combo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.landTypeId3Value.Equals(0) && this.LandType3Combo.SelectedValue == null)
                    {
                        if (!string.IsNullOrEmpty(this.LandType3Combo.Text.Trim()))
                        {
                            string filterCond = "LandType3 = '" + this.LandType3Combo.Text.Trim().Replace("'", "''") + "'";
                            DataRow[] choiceRows = this.LandType3Data.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.LandType3Data.Columns[0]].ToString(), out this.landTypeId3Value);
                            }
                            else
                            {
                                this.landTypeId3Value = 0;
                                this.LandType3Combo.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.LandType3Combo.SelectedIndex = 0;
                        }

                        //this.GetLandCode();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the ReportAsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ReportAsTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) && this.flagReportAsLabelChanged)
                {
                    // check for the frontAge text box max value
                    if (this.CheckLandFieldsMaxLimitValidation(this.ReportAsTextBox, this.ReportAsTextBox, false, false))
                    {
                        return;
                    }

                    // check for empty textbox value and assign the default value as zero.
                    if (string.IsNullOrEmpty(this.ReportAsTextBox.Text.Trim()))
                    {
                        this.ReportAsTextBox.Text = decimal.Zero.ToString();
                        this.ReportAsTextBox.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.NegativeValidation(this.ReportAsTextBox);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Land Types Header Field Events

        #region Base Market Value Field Events

        /// <summary>
        /// Handles the SelectedValueChanged event of the ShapeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShapeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    //this.OnShapeComboChangeSetBaseMarketValueFields();
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the BaseAdjusmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseAdjusmentTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    // change the base market value fields on changing the base adjustment type 
                    this.OnBaseAdjustmentTypeChangeSetBaseMarketValueFields();

                    // set the page in edit mode
                    this.EditEnabled();

                    // calculates the base doller per unit textbox
                    this.CalculateBaseDollerPerUnitTextBox();

                    // check for the base doller per unit max value
                    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseDollerPerUnitTextBox, true, true);

                    // calculates the base market value textbox
                    this.CalculateBaseMarketValueTextBox();

                    // check for the base market value max limit
                    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseMarketValueTextBox, true, true);

                    ////// calculate the influence adjustment textbox fields
                    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    this.SetInfluenceAdjustmentValue();
                    this.CalculateFinalMarketValueTextBox(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the BaseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseAdjusmentComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.alternateLandCodeBaseValue.Equals(0) && this.BaseAdjusmentComboBox.SelectedValue == null)
                    {
                        if (!string.IsNullOrEmpty(this.BaseAdjusmentComboBox.Text.Trim()))
                        {
                            string filterCond = "LandCode = '" + this.BaseAdjusmentComboBox.Text.Trim().Replace("'", "''") + "'";
                            //DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.LandCodeData.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                this.getLandCodeBaseValueDataTable.Clear();
                                this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                                //this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listBaseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, null));
                                this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.LandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                                this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                                if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                                {
                                    decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.BaseValueColumn].ToString(), out this.alternateLandCodeBaseValue);
                                }
                            }
                            else
                            {
                                this.alternateLandCodeBaseValue = 0;
                                this.BaseAdjusmentComboBox.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.BaseAdjusmentComboBox.SelectedIndex = -1;
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
        /// Handles the SelectedValueChanged event of the BaseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseAdjusmentComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.alternateLandCodeBaseValue = 0;

                    if (!string.IsNullOrEmpty(this.BaseAdjusmentComboBox.Text.Trim()) && this.BaseAdjusmentComboBox.SelectedValue != null)
                    {
                        if (!string.Equals(this.BaseAdjusmentComboBox.SelectedValue.ToString(), string.Empty))
                        {
                            string filterCond = "LandCode = '" + this.BaseAdjusmentComboBox.SelectedValue.ToString() + "'";
                           // DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.LandCodeData.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                this.getLandCodeBaseValueDataTable.Clear();
                                this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                                this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.LandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                                this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                                if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                                {
                                    decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.BaseValueColumn].ToString(), out this.alternateLandCodeBaseValue);
                                }
                            }
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the BaseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseAdjusmentComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.alternateLandCodeBaseValue = 0;

                    if (!string.IsNullOrEmpty(this.BaseAdjusmentComboBox.Text.Trim()))
                    {
                        string filterCond = "LandCode = '" + this.BaseAdjusmentComboBox.Text.Trim().Replace("'", "''") + "'";
                        DataRow[] choiceRows = this.LandCodeData.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            this.getLandCodeBaseValueDataTable.Clear();
                            this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.LandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                            this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                            if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                            {
                                decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.BaseValueColumn].ToString(), out this.alternateLandCodeBaseValue);
                            }
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the LotWidthAndDepthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LotWidthAndDepthTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    // check for the sender max value
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, (TerraScanTextBox)sender, false, false))
                    {
                        return;
                    }
                    else
                    {
                        this.NegativeValidation((TerraScanTextBox)sender);
                    }

                    this.CalculateBaseUnitsTextBox();

                    // check for the units textbox max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.UnitsTextBox, false, false))
                    {
                        this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();

                        this.FinalUseValueTextBox.Text = decimal.Zero.ToString();

                        ////// calculate the influence adjustment textbox fields
                        ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                        this.CalculateFinalMarketValueTextBox(0);

                        return;
                    }

                    this.CalculateBaseMarketValueTextBox();

                    // check for the base market value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseMarketValueTextBox, true, true))
                    {
                        this.UnitsTextBox.Text = decimal.Zero.ToString();
                    }

                    ////// calculate the influence adjustment textbox fields
                    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    this.SetInfluenceAdjustmentValue();
                    this.CalculateFinalMarketValueTextBox(0);


                    // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                    this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

                    // check for the final use value max limit
                    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalUseValueTextBox, true, true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the FrontageTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void FrontageTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    // check for the frontAge text box max value
                    if (this.CheckLandFieldsMaxLimitValidation(this.FrontageTextBox, this.FrontageTextBox, false, false))
                    {
                        return;
                    }
                    else
                    {
                        this.NegativeValidation(this.FrontageTextBox);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the UnitsTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UnitsTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    // check for the Units text box max value
                    if (this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.UnitsTextBox, false, false))
                    {
                        return;
                    }
                    else
                    {
                        this.NegativeValidation(this.UnitsTextBox);
                    }

                    this.CalculateBaseMarketValueTextBox();

                    // check the max value for base market text box
                    this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.BaseMarketValueTextBox, true, true);

                    ////// calculate the influence adjustment textbox fields
                    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    this.SetInfluenceAdjustmentValue();
                    this.CalculateFinalMarketValueTextBox(0);

                    // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                    this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

                    // check for the final use value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalUseValueTextBox, true, true))
                    {
                        this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();
                        this.FinalMarketValueTextBox.Text = decimal.Zero.ToString();

                        ////// calculate the influence adjustment textbox fields
                        ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                        this.SetInfluenceAdjustmentValue();
                        this.CalculateFinalMarketValueTextBox(0);
                    }


                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the BaseAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseAdjustmentTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                        {
                            if (this.BaseAdjustmentTextBox.DecimalTextBoxValue < 0 || this.BaseAdjustmentTextBox.DecimalTextBoxValue > 999999)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentPercentageFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////Commented by Biju on 24-Sep-2009 to fix issue #4015
                                ////this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                                this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();
                                // this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.BaseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue)
                            || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                        {                            
                            double baseAdjValue;
                            double.TryParse(this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out baseAdjValue);
                            if (baseAdjValue < 0 || baseAdjValue > this.maxBaseMarketFieldValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                                this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();
                                this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.BaseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                        {
                            double baseAdjValue;
                            double.TryParse(this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out baseAdjValue);
                            if (baseAdjValue < this.minBaseMarketFieldValue || baseAdjValue > this.maxBaseMarketFieldValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentNegativeDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.BaseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        this.formDataLoad = false;
                    }
                    this.formDataLoad = false;
                    this.CalculateBaseDollerPerUnitTextBox();

                    // check for the base doller per unit value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseDollerPerUnitTextBox, true, true))
                    {
                        this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();

                        ////// calculate the influence adjustment textbox fields
                        ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                        this.SetInfluenceAdjustmentValue();
                        this.CalculateFinalMarketValueTextBox(0);

                        return;
                    }

                    // calculate the base market value textbox
                    this.CalculateBaseMarketValueTextBox();

                    // check for the base market value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseMarketValueTextBox, true, true))
                    {
                        this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                    }

                    ////// calculate the influence adjustment textbox fields
                    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    this.SetInfluenceAdjustmentValue();
                    this.CalculateFinalMarketValueTextBox(0);

                    ////added by Biju on 25-Sep-2009 to fix forcolor issue for -ve values
                    if (this.BaseAdjustmentTextBox.DecimalTextBoxValue < 0)
                    {
                        this.BaseAdjustmentTextBox.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        this.BaseAdjustmentTextBox.ForeColor = System.Drawing.Color.Black;
                    }
                    ////till here
                    
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Base Market Value Field Events

        #region Market Value Influences Field Events

        /////// <summary>
        /////// Handles the TextChanged event of the InfluenceType1ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType1ComboBox_TextChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId1Value = 0;
        ////            this.influence1Value = 0;
        ////            this.influenceType1Value = 0;
        ////            this.influenceDescription1Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType1ComboBox.Text.Trim()))
        ////            {
        ////                string filterCond = "InfluenceType = '" + this.InfluenceType1ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                DataRow[] choiceRows = this.listInfluenceType1ComboDataTable.Select(filterCond);

        ////                if (choiceRows.Length > 0)
        ////                {
        ////                    int.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId1Value);
        ////                    decimal.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceColumn].ToString(), out this.influence1Value);
        ////                    byte.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.TypeColumn].ToString(), out this.influenceType1Value);
        ////                    this.influenceDescription1Value = choiceRows[0][this.listInfluenceType1ComboDataTable.DescriptionColumn].ToString();
        ////                }
        ////            }

        ////            this.influenceType1Chaged = true;
        ////            this.EditEnabled();
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the SelectedValueChanged event of the InfluenceType1ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType1ComboBox_SelectedValueChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId1Value = 0;
        ////            this.influenceType1Value = 0;
        ////            this.influence1Value = 0;
        ////            this.influenceDescription1Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType1ComboBox.Text.Trim()) && this.InfluenceType1ComboBox.SelectedValue != null)
        ////            {
        ////                if (!int.Equals(this.InfluenceType1ComboBox.SelectedValue, 0))
        ////                {
        ////                    string filterCond = "InfluenceTypeID = " + this.InfluenceType1ComboBox.SelectedValue.ToString();
        ////                    DataRow[] choiceRows = this.listInfluenceType1ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId1Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceColumn].ToString(), out this.influence1Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.TypeColumn].ToString(), out this.influenceType1Value);
        ////                        this.influenceDescription1Value = choiceRows[0][this.listInfluenceType1ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                }

        ////                this.influenceType1Chaged = true;
        ////                this.EditEnabled();
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Leave event of the InfluenceType1ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType1ComboBox_Leave(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            if (this.influenceTypeId1Value.Equals(0) && this.InfluenceType1ComboBox.SelectedValue == null)
        ////            {
        ////                if (!string.IsNullOrEmpty(this.InfluenceType1ComboBox.Text.Trim()))
        ////                {
        ////                    string filterCond = "InfluenceType = '" + this.InfluenceType1ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                    DataRow[] choiceRows = this.listInfluenceType1ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId1Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceColumn].ToString(), out this.influence1Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.TypeColumn].ToString(), out this.influenceType1Value);
        ////                        this.influenceDescription1Value = choiceRows[0][this.listInfluenceType1ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                    else
        ////                    {
        ////                        this.influenceTypeId1Value = 0;
        ////                        this.influence1Value = 0;
        ////                        this.influenceType1Value = 0;
        ////                        this.influenceDescription1Value = string.Empty;
        ////                        this.InfluenceType1ComboBox.Text = string.Empty;
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    this.InfluenceType1ComboBox.SelectedIndex = 0;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Leave event of the InfluenceType2ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType2ComboBox_Leave(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            if (this.influenceTypeId2Value.Equals(0) && this.InfluenceType2ComboBox.SelectedValue == null)
        ////            {
        ////                if (!string.IsNullOrEmpty(this.InfluenceType2ComboBox.Text.Trim()))
        ////                {
        ////                    string filterCond = "InfluenceType = '" + this.InfluenceType2ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                    DataRow[] choiceRows = this.listInfluenceType2ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId2Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceColumn].ToString(), out this.influence2Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.TypeColumn].ToString(), out this.influenceType2Value);
        ////                        this.influenceDescription2Value = choiceRows[0][this.listInfluenceType2ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                    else
        ////                    {
        ////                        this.influenceTypeId2Value = 0;
        ////                        this.influenceType2Value = 0;
        ////                        this.influence2Value = 0;
        ////                        this.influenceDescription2Value = string.Empty;
        ////                        this.InfluenceType2ComboBox.Text = string.Empty;
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    this.InfluenceType2ComboBox.SelectedIndex = 0;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the SelectedValueChanged event of the InfluenceType2ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType2ComboBox_SelectedValueChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId2Value = 0;
        ////            this.influenceType2Value = 0;
        ////            this.influence2Value = 0;
        ////            this.influenceDescription2Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType2ComboBox.Text.Trim()) && this.InfluenceType2ComboBox.SelectedValue != null)
        ////            {
        ////                if (!int.Equals(this.InfluenceType2ComboBox.SelectedValue, 0))
        ////                {
        ////                    string filterCond = "InfluenceTypeID = " + this.InfluenceType2ComboBox.SelectedValue.ToString();
        ////                    DataRow[] choiceRows = this.listInfluenceType2ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId2Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceColumn].ToString(), out this.influence2Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.TypeColumn].ToString(), out this.influenceType2Value);
        ////                        this.influenceDescription2Value = choiceRows[0][this.listInfluenceType2ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                }

        ////                this.influenceType2Chaged = true;
        ////                this.EditEnabled();
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the TextChanged event of the InfluenceType2ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType2ComboBox_TextChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId2Value = 0;
        ////            this.influence2Value = 0;
        ////            this.influenceType2Value = 0;
        ////            this.influenceDescription2Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType2ComboBox.Text.Trim()))
        ////            {
        ////                string filterCond = "InfluenceType = '" + this.InfluenceType2ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                DataRow[] choiceRows = this.listInfluenceType2ComboDataTable.Select(filterCond);

        ////                if (choiceRows.Length > 0)
        ////                {
        ////                    int.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId2Value);
        ////                    decimal.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.InfluenceColumn].ToString(), out this.influence2Value);
        ////                    byte.TryParse(choiceRows[0][this.listInfluenceType2ComboDataTable.TypeColumn].ToString(), out this.influenceType2Value);
        ////                    this.influenceDescription2Value = choiceRows[0][this.listInfluenceType2ComboDataTable.DescriptionColumn].ToString();
        ////                }
        ////            }

        ////            this.influenceType2Chaged = true;
        ////            this.EditEnabled();
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the TextChanged event of the InfluenceType3ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType3ComboBox_TextChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId3Value = 0;
        ////            this.influence3Value = 0;
        ////            this.influenceType3Value = 0;
        ////            this.influenceDescription3Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType3ComboBox.Text.Trim()))
        ////            {
        ////                string filterCond = "InfluenceType = '" + this.InfluenceType3ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                DataRow[] choiceRows = this.listInfluenceType3ComboDataTable.Select(filterCond);

        ////                if (choiceRows.Length > 0)
        ////                {
        ////                    int.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId3Value);
        ////                    decimal.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceColumn].ToString(), out this.influence3Value);
        ////                    byte.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.TypeColumn].ToString(), out this.influenceType3Value);
        ////                    this.influenceDescription3Value = choiceRows[0][this.listInfluenceType3ComboDataTable.DescriptionColumn].ToString();
        ////                }
        ////            }

        ////            this.influenceType3Chaged = true;
        ////            this.EditEnabled();
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the SelectedValueChanged event of the InfluenceType3ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType3ComboBox_SelectedValueChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            this.influenceTypeId3Value = 0;
        ////            this.influence3Value = 0;
        ////            this.influenceType3Value = 0;
        ////            this.influenceDescription3Value = string.Empty;

        ////            if (!string.IsNullOrEmpty(this.InfluenceType3ComboBox.Text.Trim()) && this.InfluenceType3ComboBox.SelectedValue != null)
        ////            {
        ////                if (!int.Equals(this.InfluenceType3ComboBox.SelectedValue, 0))
        ////                {
        ////                    string filterCond = "InfluenceTypeID = " + this.InfluenceType3ComboBox.SelectedValue.ToString();
        ////                    DataRow[] choiceRows = this.listInfluenceType3ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId3Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceColumn].ToString(), out this.influence3Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.TypeColumn].ToString(), out this.influenceType3Value);
        ////                        this.influenceDescription3Value = choiceRows[0][this.listInfluenceType3ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                }

        ////                this.influenceType3Chaged = true;
        ////                this.EditEnabled();
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Leave event of the InfluenceType3ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType3ComboBox_Leave(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess)
        ////        {
        ////            if (this.influenceTypeId3Value.Equals(0) && this.InfluenceType3ComboBox.SelectedValue == null)
        ////            {
        ////                if (!string.IsNullOrEmpty(this.InfluenceType3ComboBox.Text.Trim()))
        ////                {
        ////                    string filterCond = "InfluenceType = '" + this.InfluenceType3ComboBox.Text.Trim().Replace("'", "''") + "'";
        ////                    DataRow[] choiceRows = this.listInfluenceType3ComboDataTable.Select(filterCond);

        ////                    if (choiceRows.Length > 0)
        ////                    {
        ////                        int.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceTypeIDColumn].ToString(), out this.influenceTypeId3Value);
        ////                        decimal.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.InfluenceColumn].ToString(), out this.influence3Value);
        ////                        byte.TryParse(choiceRows[0][this.listInfluenceType3ComboDataTable.TypeColumn].ToString(), out this.influenceType3Value);
        ////                        this.influenceDescription3Value = choiceRows[0][this.listInfluenceType3ComboDataTable.DescriptionColumn].ToString();
        ////                    }
        ////                    else
        ////                    {
        ////                        this.influenceTypeId3Value = 0;
        ////                        this.influence3Value = 0;
        ////                        this.influenceType3Value = 0;
        ////                        this.influenceDescription3Value = string.Empty;
        ////                        this.InfluenceType3ComboBox.Text = string.Empty;
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    this.InfluenceType3ComboBox.SelectedIndex = 0;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Validated event of the InfluenceType1ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType1ComboBox_Validated(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess
        ////            && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)
        ////            && this.influenceType1Chaged)
        ////        {
        ////            this.influenceType1Chaged = false;
        ////            this.FillInfluence1FieldValues();

        ////            // check for the influenct adjustment1 text box max value
        ////            this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment1TextBox, true, true);

        ////            // check for the final market value text box max value
        ////            if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalMarketValueTextBox, true, true))
        ////            {
        ////                return;
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Validated event of the InfluenceType2ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType2ComboBox_Validated(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess
        ////            && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)
        ////             && this.influenceType2Chaged)
        ////        {
        ////            this.influenceType2Chaged = false;
        ////            this.FillInfluence2FieldValues();

        ////            // check for the influenct adjustment2 text box max value
        ////            this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment2TextBox, true, true);

        ////            // check for the final market value text box max value
        ////            if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalMarketValueTextBox, true, true))
        ////            {
        ////                return;
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        /////// <summary>
        /////// Handles the Validated event of the InfluenceType3ComboBox control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        ////private void InfluenceType3ComboBox_Validated(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (!this.flagLoadOnProcess
        ////            && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)
        ////             && this.influenceType3Chaged)
        ////        {
        ////            this.influenceType3Chaged = false;
        ////            this.FillInfluence3FieldValues();

        ////            // check for the influenct adjustment3 text box max value
        ////            this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment3TextBox, true, true);

        ////            // check for the final market value text box max value
        ////            if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalMarketValueTextBox, true, true))
        ////            {
        ////                return;
        ////            }
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}

        #endregion Market Value Influences Field Events

        #region Use Value Field Events

        /// <summary>
        /// Handles the SelectedValueChanged event of the ProgramComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ProgramComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
                this.OnProgramChangeSetUseValueFields();

                byte outSelectedProgramId;
                if (byte.TryParse(this.ProgramComboBox.SelectedValue.ToString(), out outSelectedProgramId))
                {
                    if (!outSelectedProgramId.Equals(0))
                    {
                        // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                        this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

                        // check for the use base doller per unit value max limit
                        if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.UseBaseDollarsPerUnitTextBox, true, false))
                        {
                            this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                            return;
                        }

                        // check for the final use value max limit
                        if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalUseValueTextBox, true, true))
                        {
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the UseAdjusmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    //if (this.UseAdjustmentTextBox.Visible && !string.IsNullOrEmpty(this.UseAdjustmentTextBox.Text))
                    //{
                    //    this.tempUseAdjustmentCombo = string.Empty;
                    //    this.tempUseAdjustmentTxt = this.UseAdjustmentTextBox.Text;
                    //}
                    //if (this.UseAdjusmentComboBox.Visible && !string.IsNullOrEmpty(this.UseAdjusmentComboBox.Text) )
                    //{
                    //    this.tempUseAdjustmentTxt = string.Empty;
                    //    this.tempUseAdjustmentCombo = this.UseAdjusmentComboBox.Text;
                    //}
                    //if (!string.IsNullOrEmpty(this.ReasonForUseAdjTextBox.Text))
                    //{
                    //    this.tempReasonForUseAdjText = this.ReasonForUseAdjTextBox.Text;
                    //}
                    // change the use value fields on changing the use adjustment type
                    this.OnUseAdjustmentTypeChangeSetUseValueFields();

                    // set the page in edti mode
                    this.EditEnabled();

                    // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                    this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

                    // check for the use base doller per unit value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.UseBaseDollarsPerUnitTextBox, true, false))
                    {
                        this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                        return;
                    }

                    // check for the final use value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalUseValueTextBox, true, true))
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the UseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.alternateLandCodeUseBaseValue = 0;

                    if (!string.IsNullOrEmpty(this.UseAdjusmentComboBox.Text.Trim()))
                    {
                        string filterCond = "LandCode = '" + this.UseAdjusmentComboBox.Text.Trim().Replace("'", "''") + "'";
                        //DataRow[] choiceRows = this.listUseAdjustmentComboDataTable.Select(filterCond);
                        DataRow[] choiceRows = this.UseLandCodeData.Select(filterCond);
                        if (choiceRows.Length > 0)
                        {
                            this.getLandCodeBaseValueDataTable.Clear();
                            this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.UseLandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                            //this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listUseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, null));
                            this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                            if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                            {
                                decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.UseBaseValueColumn].ToString(), out this.alternateLandCodeUseBaseValue);
                            }
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the UseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.alternateLandCodeUseBaseValue = 0;

                    if (!string.IsNullOrEmpty(this.UseAdjusmentComboBox.Text.Trim()) && this.UseAdjusmentComboBox.SelectedValue != null)
                    {
                        if (!string.Equals(this.UseAdjusmentComboBox.SelectedValue.ToString(), string.Empty))
                        {
                            string filterCond = "LandCode = '" + this.UseAdjusmentComboBox.SelectedValue.ToString() + "'";
                            DataRow[] choiceRows = this.UseLandCodeData.Select(filterCond);
                            //DataRow[] choiceRows = this.listUseAdjustmentComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                this.getLandCodeBaseValueDataTable.Clear();
                                this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                                this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.UseLandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                                this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                                if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                                {
                                    decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.UseBaseValueColumn].ToString(), out this.alternateLandCodeUseBaseValue);
                                }
                                // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                                this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();
                            }
                        }
                    }

                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the UseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.alternateLandCodeUseBaseValue.Equals(0) && this.UseAdjusmentComboBox.SelectedValue == null)
                    {
                        if (!string.IsNullOrEmpty(this.UseAdjusmentComboBox.Text.Trim()))
                        {
                            string filterCond = "LandCode = '" + this.UseAdjusmentComboBox.Text.Trim().Replace("'", "''") + "'";
                            //DataRow[] choiceRows = this.listUseAdjustmentComboDataTable.Select(filterCond);
                            DataRow[] choiceRows = this.UseLandCodeData.Select(filterCond);
                            if (choiceRows.Length > 0)
                            {
                                this.getLandCodeBaseValueDataTable.Clear();
                                this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                                this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.UseLandCodeData.Columns[0]].ToString(), this.formValueSliceId, null));
                                //this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listUseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, null));
                                this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                                if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                                {
                                    decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.UseBaseValueColumn].ToString(), out this.alternateLandCodeUseBaseValue);
                                }
                            }
                            else
                            {
                                this.alternateLandCodeUseBaseValue = 0;
                                this.UseAdjusmentComboBox.Text = string.Empty;
                            }
                        }
                        else
                        {
                            this.UseAdjusmentComboBox.SelectedIndex = 0;
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
        /// Handles the Validated event of the UseAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjustmentTextBox_Validated(object sender, EventArgs e)
        {
            try
            {

                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                        {
                              
                            if (this.UseAdjustmentTextBox.DecimalTextBoxValue < 0 || this.UseAdjustmentTextBox.DecimalTextBoxValue > 999999)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UseAdjustmentPercentageFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////Commented by Biju on 24-Sep-2009 to fix issue #4015
                                ////this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                                this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                                //this.UseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.UseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue)
                             || this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                        {
                            double useAdjValue;
                            double.TryParse(this.UseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out useAdjValue);
                            if (useAdjValue < 0 || useAdjValue > this.maxBaseMarketFieldValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UseAdjustmentDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                                this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                                this.UseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.UseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                        {
                            double useAdjValue;
                            double.TryParse(this.UseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out useAdjValue);
                            if (useAdjValue < this.minBaseMarketFieldValue || useAdjValue > this.maxBaseMarketFieldValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("UseAdjustmentNegativeDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.UseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.UseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        this.formDataLoad = false; 
                    }
                    this.formDataLoad = false;
                    // calculates the UseBaseDollerPerUnit and finalUseValue textbox
                    this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

                    // check for the use base doller per unit value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.UseBaseDollarsPerUnitTextBox, true, false))
                    {
                        this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                        return;
                    }

                    // check for the final use value max limit
                    if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalUseValueTextBox, true, true))
                    {
                        this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                        return;
                    }
                }
                ////added by Biju on 25-Sep-2009 to fix forcolor issue for -ve values
                if (this.UseAdjustmentTextBox.DecimalTextBoxValue < 0)
                {
                    this.UseAdjustmentTextBox.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    this.UseAdjustmentTextBox.ForeColor = System.Drawing.Color.Black;
                }
                ////till here
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Use Value Field Events

        #region Land Details Grid View Events

        /// <summary>
        /// Handles the CellFormatting event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex.Equals(this.FinalMrktValue.Index) || e.ColumnIndex.Equals(this.FinalUseValue.Index) || e.ColumnIndex.Equals(this.FinalValue.Index))
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0"), ")");
                                    e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0");
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
                            e.Value = string.Empty;
                        }
                    }

                    if (e.ColumnIndex.Equals(this.Units.Index))
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString()))
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
                            e.Value = string.Empty;
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
        /// Handles the CellClick event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !this.flagLoadOnProcess)
                {
                    if (this.selectedRow < 0)
                    {
                        this.selectedRow = e.RowIndex;
                    }

                    if (!this.selectedRow.Equals(e.RowIndex))
                    {
                        this.SetLandFormFieldValues(this.GetSelectedLandDetailsRow(e.RowIndex));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !this.flagLoadOnProcess && !this.NullRecords)
                {
                    this.selectedRow = e.RowIndex;
                    this.flagLoadOnProcess = true;

                    if (this.landDetailsDataSet.ListLandValueSliceDetailsNew.Rows.Count > 0)
                    {
                        this.SetLandFormFieldValues(this.GetSelectedLandDetailsRow(e.RowIndex));
                    }

                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    this.flagLoadOnProcess = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.selectedRow = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                decimal totalUnits;
                decimal totalMrktValue;
                decimal totalUseValue;
                decimal totalFinalValue;

                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    if (this.landDetailsDataSet.ListLandValueSliceDetailsNew.Rows.Count > 0)
                    {
                        decimal.TryParse(this.landDetailsDataSet.ListLandValueSliceDetailsNew.Rows[0][this.landDetailsDataSet.ListLandValueSliceDetailsNew.GridUnitsTotalColumn.ColumnName].ToString(), out totalUnits);
                        this.TotalUnitsLabel.Text = totalUnits.ToString("#,##0.00");
                    }

                    decimal.TryParse(this.landDetailsDataSet.ListLandValueSliceDetailsNew.Compute("SUM (FinalMrktValue)", "1=1").ToString(), out totalMrktValue);
                    decimal.TryParse(this.landDetailsDataSet.ListLandValueSliceDetailsNew.Compute("SUM (FinalUseValue)", "1=1").ToString(), out totalUseValue);
                    decimal.TryParse(this.landDetailsDataSet.ListLandValueSliceDetailsNew.Compute("SUM (GridFinalValue)", "1=1").ToString(), out totalFinalValue);

                    if (totalMrktValue.ToString().Contains("-"))
                    {
                        this.TotalMarketValueLabel.Text = String.Concat("(", Decimal.Negate(totalMrktValue).ToString("#,##0"), ")");
                        this.TotalMarketValueLabel.ForeColor = Color.FromArgb(0, 128, 0);
                    }
                    else
                    {
                        this.TotalMarketValueLabel.Text = totalMrktValue.ToString("#,##0");
                        this.TotalMarketValueLabel.ForeColor = Color.Black;
                    }

                    if (totalUseValue.ToString().Contains("-"))
                    {
                        this.TotalUseValueLabel.Text = String.Concat("(", Decimal.Negate(totalUseValue).ToString("#,##0"), ")");
                        this.TotalUseValueLabel.ForeColor = Color.FromArgb(0, 128, 0);
                    }
                    else
                    {
                        this.TotalUseValueLabel.Text = totalUseValue.ToString("#,##0");
                        this.TotalUseValueLabel.ForeColor = Color.Black;
                    }

                    if (totalFinalValue.ToString().Contains("-"))
                    {
                        this.TotalFinalValueLabel.Text = String.Concat("(", Decimal.Negate(totalFinalValue).ToString("#,##0"), ")");
                        this.TotalFinalValueLabel.ForeColor = Color.FromArgb(0, 128, 0);
                    }
                    else
                    {
                        this.TotalFinalValueLabel.Text = totalFinalValue.ToString("#,##0");
                        this.TotalFinalValueLabel.ForeColor = Color.Black;
                    }

                    //this.TotalMarketValueLabel.Text = totalMrktValue.ToString("#,##0");
                    //this.TotalUseValueLabel.Text = totalUseValue.ToString("#,##0");
                    //this.TotalFinalValueLabel.Text = totalFinalValue.ToString("#,##0");
                }
                else
                {
                    this.TotalUnitsLabel.Text = string.Empty;
                    this.TotalMarketValueLabel.Text = string.Empty;
                    this.TotalUseValueLabel.Text = string.Empty;
                    this.TotalFinalValueLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Land Details Grid View Events

        #region Influence Grid View Events

        /// <summary>
        /// Handles the EditingControlShowing event of the InfluenceGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void InfluenceGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;

                ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(F36035_InfluenceTypeSelectionChangeCommitted);
                ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(F36035_InfluenceTypeSelectionChangeCommitted);

                ((ComboBox)e.Control).Validating -= new System.ComponentModel.CancelEventHandler(F36035_InfluenceTypeValidating);
                ((ComboBox)e.Control).Validating += new System.ComponentModel.CancelEventHandler(F36035_InfluenceTypeValidating);

                ((ComboBox)e.Control).TextUpdate -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                ((ComboBox)e.Control).TextUpdate += new EventHandler(F36035_InfluenceTypeTextUpdate);

                ((ComboBox)e.Control).SelectedValueChanged -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                ((ComboBox)e.Control).SelectedValueChanged += new EventHandler(F36035_InfluenceTypeTextUpdate);
            }
            else if (e.Control is DataGridViewTextBoxEditingControl)
            {
                e.Control.TextChanged -= new EventHandler(Control_TextChanged);
                e.Control.TextChanged += new EventHandler(Control_TextChanged);

                e.Control.Validated -= new EventHandler(this.Control_Validated);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.InfluenceGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InfluenceTypeTextUpdate event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36035_InfluenceTypeTextUpdate(object sender, EventArgs e)
        {
            this.influenceTypeChanged = true;
            this.EditEnabled();
        }

        /// <summary>
        /// Handles the InfluenceTypeValidating event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void F36035_InfluenceTypeValidating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && this.influenceTypeChanged)
                {
                    if ((sender as DataGridViewComboBoxEditingControl).SelectedValue == null)
                    {
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value = 0;
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceType.Name].Value = DBNull.Value;
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.Influence.Name].Value = DBNull.Value;
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceDesc.Name].Value = string.Empty;
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceValue.Name].Value = DBNull.Value;
                    }
                    else
                    {

                        int combovalue = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;

                        if (combovalue > 0)
                        {
                            string filterCond = "InfluenceTypeID = " + combovalue;
                            DataRow[] choiceRows = this.listInfluenceType1ComboDataTable.Select(filterCond);
                            this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value = combovalue;

                            if (choiceRows.Length > 0)
                            {
                                decimal.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceColumn].ToString(), out this.influence1Value);
                                this.influenceDescription1Value = choiceRows[0][this.listInfluenceType1ComboDataTable.DescriptionColumn].ToString();

                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.Influence.Name].Value = this.influence1Value.ToString();
                                byte.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.TypeColumn].ToString(), out this.influenceType1Value);
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceType.Name].Value = this.influenceType1Value;
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceDesc.Name].Value = this.influenceDescription1Value;

                                decimal influence1Adjustment;

                                if (this.influenceType1Value.Equals(1))
                                {
                                    influence1Adjustment = this.BaseMarketValueTextBox.DecimalTextBoxValue * this.influence1Value / 100;

                                    if ((double)influence1Adjustment < Math.Floor(this.minMoneyFieldValue) || (double)influence1Adjustment > Math.Floor(this.maxMoneyFieldValue))
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        influence1Adjustment = 0;
                                    }

                                    this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceValue.Name].Value = influence1Adjustment.ToString();
                                }
                                else if (this.influenceType1Value.Equals(2))
                                {
                                    this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceValue.Name].Value = this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.Influence.Name].Value.ToString();
                                }
                                else
                                {
                                    this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.Influence.Name].Value = string.Empty;
                                    this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceValue.Name].Value = string.Empty;
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value.ToString()))
                            {
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value = 0;
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceType.Name].Value = DBNull.Value;
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.Influence.Name].Value = DBNull.Value;
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceDesc.Name].Value = string.Empty;
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceValue.Name].Value = DBNull.Value;
                            }
                            else
                            {
                                this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value = DBNull.Value;
                            }
                        }


                        //// Calculates the finalMarketValue
                        //this.landDetailsDataSet.ListGridInfluences.AcceptChanges();
                        //this.CalculateFinalMarketValueTextBox();
                        this.filteredTable.AcceptChanges();
                        //this.InfluenceGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }

                    this.CalculateFinalMarketValueTextBox(0);

                    if ((this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value == null
                        || string.IsNullOrEmpty(this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value.ToString()))
                        && !string.IsNullOrEmpty(this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value.ToString()))
                    {
                        this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value = 0;
                    }

                    this.influenceTypeChanged = false;
                }

                if (this.InfluenceGridView.EditingControl != null)
                {
                    ((ComboBox)this.InfluenceGridView.EditingControl).TextUpdate -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                    ((ComboBox)this.InfluenceGridView.EditingControl).SelectedValueChanged -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InfluenceTypeSelectionChangeCommitted event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36035_InfluenceTypeSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if ((sender as DataGridViewComboBoxEditingControl).SelectedValue != null)
                {
                    this.EditEnabled();
                    this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    (sender as DataGridViewComboBoxEditingControl).SelectedValue = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    this.influenceTypeChanged = true;
                    this.formDataLoad = false;
                }

                if (this.InfluenceGridView.EditingControl != null)
                {
                    ((ComboBox)this.InfluenceGridView.EditingControl).TextUpdate -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                    ((ComboBox)this.InfluenceGridView.EditingControl).SelectedValueChanged -= new EventHandler(F36035_InfluenceTypeTextUpdate);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the InfluenceGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InfluenceGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SetReadOnly(e.RowIndex);
                this.influencerowindex = e.RowIndex;
                this.influencecolumnindex = e.ColumnIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the InfluenceGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InfluenceGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex.Equals(this.InfluenceGridView.Rows.Count - 1)
                    && (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit)
                    || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New)))
                {
                    if (this.InfluenceGridView.Rows[e.RowIndex].Cells[this.InfluenceTypeID.Name].Value != null
                        && !string.IsNullOrEmpty(this.InfluenceGridView.Rows[e.RowIndex].Cells[this.InfluenceTypeID.Name].Value.ToString().Trim()))
                    {
                        F36035LandData.ListGridInfluencesRow newRow = this.filteredTable.NewListGridInfluencesRow();
                        newRow["EmptyRecord$"] = "True";
                        //this.landDetailsDataSet.ListGridInfluences.AddListGridInfluencesRow(newRow);
                        this.filteredTable.AddListGridInfluencesRow(newRow);
                        this.InfluenceGridVerticalScroll.Visible = false;
                        //InfluenceGridView.ScrollBars = ScrollBars.Vertical;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the InfluenceGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InfluenceGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex.Equals(this.Influence.Index) || e.ColumnIndex.Equals(this.InfluenceValue.Index))
                    {
                        if (e.Value != null)
                        {
                            string val = e.Value.ToString();
                            if (!string.IsNullOrEmpty(val))
                            {
                                if (Decimal.TryParse(val, out outDecimal))
                                {
                                    if (outDecimal.ToString().Contains("-"))
                                    {
                                        // For negative value display format
                                        if (e.ColumnIndex.Equals(this.Influence.Index))
                                        {
                                            if (InfluenceGridView.Rows[e.RowIndex].Cells[this.InfluenceType.Name].Value.ToString().Equals("1"))
                                            {
                                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString() + " %", ")");
                                            }
                                            else
                                            {
                                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString(), ")");
                                            }

                                        }
                                        else
                                        {
                                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                        }

                                        e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);
                                    }
                                    else
                                    {
                                        if (e.ColumnIndex.Equals(this.Influence.Index))
                                        {
                                            if (InfluenceGridView.Rows[e.RowIndex].Cells[this.InfluenceType.Name].Value.ToString().Equals("1"))
                                            {
                                                e.Value = outDecimal.ToString() + " %";
                                            }
                                            else
                                            {
                                                e.Value = outDecimal.ToString();
                                            }

                                        }
                                        else
                                        {
                                            e.Value = outDecimal.ToString("#,##0.00");
                                        }

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
                                e.Value = DBNull.Value;
                            }
                        }
                        else
                        {
                            e.Value = DBNull.Value;
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
        /// Handles the KeyDown event of the InfluenceGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InfluenceGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    if (this.InfluenceGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                        && this.InfluenceGridView.OriginalRowCount > 0 && this.influencerowindex < this.InfluenceGridView.OriginalRowCount)
                    {
                        if (!string.IsNullOrEmpty(this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value.ToString()))
                        {
                            int influenceItemId = (int)this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceItemId.Name].Value;
                            DataRow[] deletedRow = this.landDetailsDataSet.ListGridInfluences.Select(this.landDetailsDataSet.ListGridInfluences.InfluenceItemIDColumn.ColumnName + " = " + influenceItemId.ToString());
                            if (deletedRow.Length > 0)
                            {
                                this.landDetailsDataSet.ListGridInfluences.Rows.Remove(deletedRow[0]);
                            }
                        }

                        this.InfluenceGridView.Rows.RemoveAt(this.influencerowindex);

                        this.CalculateFinalMarketValueTextBox(0);

                        if (this.InfluenceGridView.Rows.Count <= this.InfluenceGridView.NumRowsVisible)
                        {
                            F36035LandData.ListGridInfluencesRow newRow = this.filteredTable.NewListGridInfluencesRow();
                            newRow["EmptyRecord$"] = "True";
                            this.filteredTable.AddListGridInfluencesRow(newRow);
                            this.InfluenceGridVerticalScroll.Visible = true;
                        }
                        else
                        {
                            this.InfluenceGridVerticalScroll.Visible = false;
                        }

                        this.EditEnabled();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void InfluenceGridView_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (this.InfluenceGridView.EditingControl is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)this.InfluenceGridView.EditingControl).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)this.InfluenceGridView.EditingControl).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)this.InfluenceGridView.EditingControl).AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void InfluenceGridView_CellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
        {
            try
            {
                //if (this.InfluenceGridView.EditingControl is DataGridViewComboBoxEditingControl)
                //{
                //    if ((this.InfluenceGridView.EditingControl as DataGridViewComboBoxEditingControl).SelectedValue == null)
                //    {
                //        (this.InfluenceGridView.EditingControl as DataGridViewComboBoxEditingControl).SelectedValue = DBNull.Value;
                //    }
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Influence Grid View Events

        #region Private Methods

        #region Common Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form36035Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form36035Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form36035Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            // set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form36035Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form36035Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            if (this.form36035Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form36035Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form36035Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
        }

        /// <summary>
        /// Customizes the land details grid view.
        /// </summary>
        private void CustomizeLandDetailsGridView()
        {
            this.LandDetailsDataGridView.AutoGenerateColumns = false;
            this.LandDetailsDataGridView.PrimaryKeyColumnName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName;

            this.LUID.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName;
            this.ValueSliceID.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.ValueSliceIDColumn.ColumnName;
            this.RollYear.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.RollYearColumn.ColumnName;

            this.LandTypeID1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandTypeID1Column.ColumnName;
            this.LandType1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandType1Column.ColumnName;
            this.LandTypeID2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandTypeID2Column.ColumnName;
            this.LandType2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandType2Column.ColumnName;
            this.LandTypeID3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandTypeID3Column.ColumnName;
            this.LandType3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandType3Column.ColumnName;

            this.LandCode.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandCodeColumn.ColumnName;
            this.ReportAS.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.ReportASColumn.ColumnName;

            this.BaseValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.BaseValueColumn.ColumnName;

            this.Break1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Break1Column.ColumnName;
            this.Value1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Value1Column.ColumnName;
            this.Break2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Break2Column.ColumnName;
            this.Value2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Value2Column.ColumnName;
            this.Break3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Break3Column.ColumnName;
            this.Value4.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Value3Column.ColumnName;
            this.Break4.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Break4Column.ColumnName;
            this.Value4.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Value4Column.ColumnName;

            this.AdjustmentType.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.AdjustmentTypeColumn.ColumnName;
            this.AdjTypeDescription.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.AdjTypeDescriptionColumn.ColumnName;

            this.Adjustment.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.AdjustmentColumn.ColumnName;
            this.AdjDescription.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.AdjDescriptionColumn.ColumnName;

            this.UnitType.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UnitTypeColumn.ColumnName;
            this.Units.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UnitsColumn.ColumnName;
            this.FinalMrktValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.FinalMrktValueColumn.ColumnName;
            this.FinalUseValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.FinalUseValueColumn.ColumnName;
            this.UseBaseValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseBaseValueColumn.ColumnName;
            this.UseAdjustmentType.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseAdjustmentTypeColumn.ColumnName;

            this.UseAdjTypeDescription.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseAdjTypeDescriptionColumn.ColumnName;
            this.UseAdjustment.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseAdjustmentColumn.ColumnName;
            this.UseAdjDescription.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseAdjDescriptionColumn.ColumnName;

            this.LotWidth.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LotWidthColumn.ColumnName;
            this.LotDepth.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LotDepthColumn.ColumnName;
            this.LandShape.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LandShapeColumn.ColumnName;
            this.Frontage.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.FrontageColumn.ColumnName;
            this.BaseMrktValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.BaseMrktValueColumn.ColumnName;

            this.InfluenceTypeID1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceTypeID1Column.ColumnName;
            this.InfluenceType1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceType1Column.ColumnName;
            this.Influence1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Influence1Column.ColumnName;
            this.InfluenceDesc1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceDesc1Column.ColumnName;
            this.InfluenceValue1.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceValue1Column.ColumnName;

            this.InfluenceTypeID2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceTypeID2Column.ColumnName;
            this.InfluenceType2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceType2Column.ColumnName;
            this.Influence2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Influence2Column.ColumnName;
            this.InfluenceDesc2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceDesc2Column.ColumnName;
            this.InfluenceValue2.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceValue2Column.ColumnName;

            this.InfluenceTypeID3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceTypeID3Column.ColumnName;
            this.InfluenceType3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceType3Column.ColumnName;
            this.Influence3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Influence3Column.ColumnName;
            this.InfluenceDesc3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceDesc3Column.ColumnName;
            this.InfluenceValue3.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.InfluenceValue3Column.ColumnName;

            this.ProgramID.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.ProgramIDColumn.ColumnName;
            this.ProgramAbv.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.ProgramAbvColumn.ColumnName;
            this.Program.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.ProgramColumn.ColumnName;
            this.VFormula.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.VFormulaColumn.ColumnName;
            this.BaseDollarPerUnit.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.BaseDollarPerUnitColumn.ColumnName;
            this.UseBaseDollarPerUnit.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.UseBaseDollarPerUnitColumn.ColumnName;
            this.FinalValue.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.GridFinalValueColumn.ColumnName;
            this.IsLandConfigured.DataPropertyName = this.landDetailsDataSet.ListLandValueSliceDetailsNew.IsLandConfiguredColumn.ColumnName;
        }

        /// <summary>
        /// Clears the form fields.
        /// </summary>
        /// <param name="clearAll">if set to <c>true</c> [clear all].</param>
        private void ClearFormFields(bool clearAll)
        {
            if (clearAll)
            {
                // Reset the LandType header field values
                this.landTypeId1Value = 0;
                this.landTypeId2Value = 0;
                this.landTypeId3Value = 0;

                this.LandType1Combo.SelectedIndex = 0;
                this.LandType2Combo.SelectedIndex = 0;
                this.LandType3Combo.SelectedIndex = 0;

                this.LandType1Combo.Text = string.Empty;
                this.LandType2Combo.Text = string.Empty;
                this.LandType3Combo.Text = string.Empty;
                ////Added by Biju on 24-Sep-2009 to fix #4015
                this.ReportAsTextBox.Text = string.Empty;
            }

            ////Reset the instance variable values
            this.landUnitType = string.Empty;
            this.reportAsValue = string.Empty;
            this.landValueCurveFormula = string.Empty;
            this.landCodeBaseValue = 0;
            this.landCodeUseBaseValue = 0;

            this.alternateLandCodeBaseValue = 0;
            this.alternateLandCodeUseBaseValue = 0;
            this.landCodeMarketMultiplierValue = 0;
            this.landCodeUseMultiplierValue = 0;
            this.calculatedBaseDollerPerUnitValue = 0;
            this.calculatedBaseMarketValue = 0;
            this.calculatedUseBaseDollerPerUnitValue = 0;

            this.break1Value = 0;
            this.valuePer1Value = 0;
            this.break2Value = 0;
            this.valuePer2Value = 0;
            this.break3Value = 0;
            this.valuePer3Value = 0;
            this.break4Value = 0;
            this.valuePer4Value = 0;

            this.influenceTypeId1Value = 0;
            this.influenceTypeId2Value = 0;
            this.influenceTypeId3Value = 0;

            this.influence1Value = 0;
            this.influence2Value = 0;
            this.influence3Value = 0;

            this.influenceType1Value = 0;
            this.influenceType2Value = 0;
            this.influenceType3Value = 0;

            this.influenceDescription1Value = string.Empty;
            this.influenceDescription2Value = string.Empty;
            this.influenceDescription3Value = string.Empty;

            //Commented for the TSCO #18354
           // this.LandCodeTextBox.Text = string.Empty;
            ////Added by Biju on 24-Sep-2009 to fix #4015 --the IF condition only
            if (!this.flagReportAsLabelChanged)
                this.ReportAsTextBox.Text = string.Empty;
            this.UnitTypeTextBox.Text = string.Empty;

            // Reset the BaseMarketValue field values
            this.ShapeComboBox.SelectedIndex = 0; ////ByDefault Rectangular

            this.LotWidthTextBox.Text = string.Empty;
            this.LotDepthTextBox.Text = string.Empty;
            this.FrontageTextBox.Text = string.Empty;
            this.UnitsLabel.Text = string.Empty;
            this.UnitsTextBox.Text = string.Empty;
            this.BaseDollerPerUnitTextBox.Text = string.Empty;

            this.Break1TextBox.Text = string.Empty;
            this.ValuePer1TextBox.Text = string.Empty;
            this.Break2TextBox.Text = string.Empty;
            this.ValuePer2TextBox.Text = string.Empty;
            this.Break3TextBox.Text = string.Empty;
            this.ValuePer3TextBox.Text = string.Empty;
            this.Break4TextBox.Text = string.Empty;
            this.ValuePer4TextBox.Text = string.Empty;

            this.LandValueCurveFormulaTextBox.Text = string.Empty;

            this.BaseAdjusmentTypeComboBox.SelectedIndex = 0;
             
            //this.BaseAdjusmentComboBox.SelectedIndex = 0;
            //this.UseAdjusmentComboBox.SelectedIndex = 0;
            this.BaseAdjustmentTextBox.Text = string.Empty;
            this.BaseReasonForAdjustmentTextBox.Text = string.Empty;
            this.BaseMarketValueTextBox.Text = string.Empty;

            this.BaseAdjusmentComboBox.Visible = false;
            this.BaseAdjustmentTextBox.Visible = true;

            ////// Reset the MarketValueInfluence field values
            ////this.InfluenceType1ComboBox.SelectedIndex = 0;
            ////this.InfluenceType2ComboBox.SelectedIndex = 0;
            ////this.InfluenceType3ComboBox.SelectedIndex = 0;

            ////this.InfluenceType1ComboBox.Text = string.Empty;
            ////this.InfluenceType2ComboBox.Text = string.Empty;
            ////this.InfluenceType3ComboBox.Text = string.Empty;

            ////this.Influence1TextBox.Text = string.Empty;
            ////this.Influence2TextBox.Text = string.Empty;
            ////this.Influence3TextBox.Text = string.Empty;

            ////this.InfluenceDescription1TextBox.Text = string.Empty;
            ////this.InfluenceDescription2TextBox.Text = string.Empty;
            ////this.InfluenceDescription3TextBox.Text = string.Empty;

            ////this.InfluenceAdjustment1TextBox.Text = string.Empty;
            ////this.InfluenceAdjustment2TextBox.Text = string.Empty;
            ////this.InfluenceAdjustment3TextBox.Text = string.Empty;

            this.FinalMarketValueTextBox.Text = string.Empty;

            // Reset UseValue field values
            this.ProgramComboBox.SelectedIndex = 0;
            this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
            this.UseAdjusmentTypeComboBox.SelectedIndex = 0;
            //this.UseAdjusmentComboBox.SelectedIndex = 0;
            this.UseAdjustmentTextBox.Text = string.Empty;
            this.ReasonForUseAdjTextBox.Text = string.Empty;
            this.FinalUseValueTextBox.Text = string.Empty;
            this.UseAdjusmentComboBox.Visible = false;
            this.UseAdjustmentTextBox.Visible = true;
            ////Added by Biju on 15/Dec/2009 to fix #4691.2
            this.Frontagelabel.Visible = true;
            this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, false, false);
            ////till here
        }

        /// <summary>
        /// Populates the land details grid view.
        /// </summary>
        private void PopulateLandDetailsGridView()
        {
            this.ClearFormFields(true);
            this.landDetailsDataSet.ListLandValueSliceDetailsNew.Clear();
            this.landDetailsDataSet.GetCfgLandTypeLabel.Clear();
            this.landDetailsDataSet.GetValueSliceValidTable.Clear();
            this.landDetailsDataSet.ListGridInfluences.Rows.Clear();
            this.filteredTable.Rows.Clear();
            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_ListLandDetails(this.formValueSliceId));
            this.LandDetailsDataGridView.DataSource = this.landDetailsDataSet.ListLandValueSliceDetailsNew.DefaultView;
           
            ////Populate InflunceDetails
            this.LoadInfluenceTypeCombo();

            // Set the LandType lable values
            this.SetLandTypeLableValues(this.GetSelectedCfgLandTypesRow(0));

            if (this.flagReportAsLabelChanged)
            {
                this.ReportAsTextBox.ForeColor = Color.Black;
                this.ReportAsTextBox.LockKeyPress = false;
                this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.ReportAsTextBox.TextAlign = HorizontalAlignment.Right;
            }
            else
            {
                this.ReportAsTextBox.ForeColor = Color.Gray;
                this.ReportAsTextBox.LockKeyPress = true;
                this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.ReportAsTextBox.TextAlign = HorizontalAlignment.Left;
            }

            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                this.EnableLandFormPanels(true);
                this.SetSmartPartHeight(false);
                this.NullRecords = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.LandDetailsDataGridView.RemoveDefaultSelection = false;

                if (this.landUniqueID > 0)
                {
                    this.selectedRow = this.GetSelectedLandCodeRowIndex(this.landUniqueID);
                }

                this.SetLandFormFieldValues(this.GetSelectedLandDetailsRow(this.selectedRow));
                TerraScanCommon.SetDataGridViewPosition(this.LandDetailsDataGridView, this.selectedRow);

                this.CommentsdeckWorkspace.Enabled = true;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                //for (int i = 0; i < this.LandDetailsDataGridView.OriginalRowCount; i++)
                //{
                //    if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[i].Cells["IsLandConfigured"].Value.ToString()) && (this.LandDetailsDataGridView.Rows[i].Cells["IsLandConfigured"].Value.ToString().Equals("1")))
                //    {
                //        //this.LandDetailsDataGridView.RowsDefaultCellStyle.BackColor = Color.Pink;
                //        this.LandDetailsDataGridView.Rows[i].Cells["IsLandConfigured"].Style.BackColor = Color.Pink;
                //    }
                //    else
                //    {
                //        this.LandDetailsDataGridView.Rows[i].Cells["IsLandConfigured"].Style.BackColor = Color.Gray;
                //    }
                //}
                //foreach (DataGridViewRow row in this.LandDetailsDataGridView.Rows)
                //{
                //    if(!string.IsNullOrEmpty(row.Cells["IsLandConfigured"].Value.ToString()))
                //    {
                //        if (row.Cells["IsLandConfigured"].Value.ToString() == "1")
                //        {
                //            this.LandDetailsDataGridView.DefaultCellStyle.BackColor = Color.Pink;
                //        }
                //        else
                //        {
                //            this.LandDetailsDataGridView.DefaultCellStyle.BackColor = Color.Gray;
                //        }
                //    }
                //    //else
                //    //{
                //    //    this.LandDetailsDataGridView.RowsDefaultCellStyle.BackColor = Color.White;
                //    //}
                //}
            }
            else
            {
                this.LandDetailsDataGridView.RemoveDefaultSelection = true;
                this.SetSmartPartHeight(true);
                this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();
                this.ChangeUseValueFieldsBehaviorOnChageOfProgramType();
                this.EnableLandFormPanels(false);
                this.NullRecords = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.CommentsdeckWorkspace.Enabled = false;
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="defaultHeight">if set to <c>true</c> [default height].</param>
        private void SetSmartPartHeight(bool defaultHeight)
        {
            if (!defaultHeight)
            {
                if (this.LandDetailsDataGridView.OriginalRowCount > this.LandDetailsDataGridView.NumRowsVisible)
                {
                    if (this.LandDetailsDataGridView.OriginalRowCount > 15)
                    {
                        this.LandDetailsVscrollBar.Visible = false;

                        // Enable the gridview scrollbar
                        int tempRowCount = 8;
                        int tempRowHeigh = tempRowCount * 22;
                        this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.OriginalRowCount;
                        this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height + tempRowHeigh;
                        this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height + tempRowHeigh;
                        this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height + tempRowHeigh;
                        this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height + tempRowHeigh;
                        this.LandPictureBox.Height = this.LandPictureBox.Height + tempRowHeigh;
                        this.FooterLeftpanel.Top = this.FooterLeftpanel.Top + tempRowHeigh;
                        this.Footerpanel.Top = this.Footerpanel.Top + tempRowHeigh;
                        this.Height = this.EntireLandFormPanel.Height;

                        // Resize the slice with new height
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    }
                    else
                    {
                        this.LandDetailsVscrollBar.Visible = true;
                        int tempRowCount = this.LandDetailsDataGridView.OriginalRowCount - this.LandDetailsDataGridView.NumRowsVisible;
                        int tempRowHeigh = tempRowCount * 22;
                        this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.OriginalRowCount;
                        this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height + tempRowHeigh;
                        this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height + tempRowHeigh;
                        this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height + tempRowHeigh;
                        this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height + tempRowHeigh;
                        this.LandPictureBox.Height = this.LandPictureBox.Height + tempRowHeigh;
                        this.FooterLeftpanel.Top = this.FooterLeftpanel.Top + tempRowHeigh;
                        this.Footerpanel.Top = this.Footerpanel.Top + tempRowHeigh;
                        this.Height = this.EntireLandFormPanel.Height;

                        // Resize the slice with new height
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    }
                }
                else
                {
                    this.LandDetailsVscrollBar.Visible = true;
                }
            }
            else
            {
                this.LandDetailsVscrollBar.Visible = true;
                this.LandDetailsDataGridView.NumRowsVisible = 7;
                this.LandDetailsDataGridView.Height = 175;
                this.LandDetailsGridViewPanel.Height = 176;
                this.LandDetailsVscrollBar.Height = 173;
                this.EntireLandFormPanel.Height = 837; // 872;
                this.LandPictureBox.Height = 833; // 868;
                this.FooterLeftpanel.Top = 813; // 848;
                this.Footerpanel.Top = 813; // 848;
                this.Height = this.EntireLandFormPanel.Height;

                // Resize the slice with new height
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            // Lock the LandType header editable fields
            this.LandType1Combo.Enabled = !controlLock;
            this.LandType2Combo.Enabled = !controlLock;
            this.LandType3Combo.Enabled = !controlLock;

            // Lock the BaseMarketValue editable fields
            this.ShapeComboBox.Enabled = !controlLock;
            this.LotWidthTextBox.LockKeyPress = controlLock;
            this.LotDepthTextBox.LockKeyPress = controlLock;
            this.FrontageTextBox.LockKeyPress = controlLock;
            this.UnitsTextBox.LockKeyPress = controlLock;
            this.BaseAdjusmentTypeComboBox.Enabled = !controlLock;
            this.BaseAdjusmentComboBox.Enabled = !controlLock;
            this.BaseAdjustmentTextBox.LockKeyPress = controlLock;
            this.BaseReasonForAdjustmentTextBox.LockKeyPress = controlLock;

            ////// Lock the MarketValueInfluence editable fields
            ////this.InfluenceType1ComboBox.Enabled = !controlLock;
            ////this.InfluenceType2ComboBox.Enabled = !controlLock;
            ////this.InfluenceType3ComboBox.Enabled = !controlLock;
            ////this.InfluenceDescription1TextBox.LockKeyPress = controlLock;
            ////this.InfluenceDescription2TextBox.LockKeyPress = controlLock;
            ////this.InfluenceDescription3TextBox.LockKeyPress = controlLock;

            // Lock the UseValue editable fields
            this.ProgramComboBox.Enabled = !controlLock;
            this.UseAdjusmentTypeComboBox.Enabled = !controlLock;
            this.UseAdjusmentComboBox.Enabled = !controlLock;
            this.UseAdjustmentTextBox.LockKeyPress = controlLock;
            this.ReasonForUseAdjTextBox.LockKeyPress = controlLock;
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                // Disable the attachement and comment button when edit mode
                this.CommentsdeckWorkspace.Enabled = false;
                this.LandDetailsDataGridView.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.InfluenceGridView.AllowSorting = false;
            }
        }

        /// <summary>
        /// Enables the land form panels.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableLandFormPanels(bool enable)
        {
            // Enable the LandTypeHeader panels
            this.LandType1Panel.Enabled = enable;
            this.LandType2panel.Enabled = enable;
            this.LantType3panel.Enabled = enable;
            this.LandCodePanel.Enabled = enable;
            this.ReportAspanel.Enabled = enable;
            this.UnitTypePanel.Enabled = enable;

            // Enable the BaseMarketValue Panels
            this.Shapepanel.Enabled = enable;
            this.LotWidthPanel.Enabled = enable;
            this.LotDepthPanel.Enabled = enable;
            this.Frontagepanel.Enabled = enable;
            this.UnitsPanel.Enabled = enable;
            this.BaseDollerPerUnitPanel.Enabled = enable;
            this.Break1panel.Enabled = enable;
            this.ValuePer1panel.Enabled = enable;
            this.Break2panel.Enabled = enable;
            this.ValuePer2panel.Enabled = enable;
            this.Break3panel.Enabled = enable;
            this.ValuePer3panel.Enabled = enable;
            this.Break4panel.Enabled = enable;
            this.ValuePer4panel.Enabled = enable;
            this.LandValueCurveFormulaPanel.Enabled = enable;
            this.BaseAdjusmentTypePanel.Enabled = enable;
            this.BaseAdjustmentPanel.Enabled = enable;
            this.BaseReasonForAdjustmentPanel.Enabled = enable;
            this.BaseMarketValuepanel.Enabled = enable;

            // Enable the MarketValueInfluence Panels
            ////this.InfluenceType1Panel.Enabled = enable;
            ////this.Influence1Panel.Enabled = enable;
            ////this.InfluenceDescription1Panel.Enabled = enable;
            ////this.InfluenceAdjustment1Panel.Enabled = enable;

            ////this.InfluenceType2panel.Enabled = enable;
            ////this.Influence2panel.Enabled = enable;
            ////this.InfluenceDescription2Panel.Enabled = enable;
            ////this.InfluenceAdjustment2Panel.Enabled = enable;

            ////this.InfluenceType3panel.Enabled = enable;
            ////this.Influence3panel.Enabled = enable;
            ////this.InfluenceDescription3Panel.Enabled = enable;
            ////this.InfluenceAdjustment3Panel.Enabled = enable;
            this.FinalMarketValuePanel.Enabled = enable;

            this.InfluencePanel.Enabled = enable;
            this.InfluenceGridView.Enabled = enable;
            // Enable the UseValue panels
            this.Programpanel.Enabled = enable;
            this.UseBaseDollarsPerUnitPanel.Enabled = enable;
            this.UseAdjusmentTypePanel.Enabled = enable;
            this.UseAdjustmentpanel.Enabled = enable;
            this.ReasonForUseAdjpanel.Enabled = enable;
            this.FinalUseValuePanel.Enabled = enable;

            // Enable LandDataGridView Panel
            this.LandDetailsGridViewPanel.Enabled = enable;
            this.LandDetailsDataGridView.Enabled = enable;

            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                this.LandDetailsDataGridView.CurrentCell = null;
                this.LandDetailsDataGridView.Rows[0].Selected = enable;
                this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = enable;
            }

            this.Footerpanel.Enabled = enable;

            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.LandDetailsGridViewPanel.Enabled = false;
                this.LandDetailsDataGridView.Enabled = false;
                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    this.LandDetailsDataGridView.CurrentCell = null;
                    this.LandDetailsDataGridView.Rows[0].Selected = false;
                    this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = false;
                }
            }
            else
            {
                this.LandDetailsDataGridView.Focus();
                this.LandDetailsGridViewPanel.Enabled = true;
                this.LandDetailsDataGridView.Enabled = true;
            }
        }

        /// <summary>
        /// Sets the fields fill to light gray and disable.
        /// </summary>
        /// <param name="currentPanel">The current panel.</param>
        /// <param name="currentLabel">The current label.</param>
        /// <param name="currentTextBox">The current text box.</param>
        /// <param name="currentComboBox">The current combo box.</param>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <param name="editable">if set to <c>true</c> [editable].</param>
        private void SetFieldsFillToLightGrayAndDisable(Panel currentPanel, Label currentLabel, TerraScanTextBox currentTextBox, TerraScanComboBox currentComboBox, bool disable, bool editable)
        {
            currentPanel.Enabled = !disable;

            if (currentTextBox != null && editable)
            {
                currentTextBox.LockKeyPress = disable;
            }

            if (currentComboBox != null && editable)
            {
                currentComboBox.Enabled = !disable;
            }

            if (disable)
            {
                currentPanel.BackColor = this.disabledPanelBackColor;
                currentLabel.ForeColor = this.disabledLabelForeColor;

                if (currentTextBox != null)
                {
                    currentTextBox.Text = string.Empty;
                    currentTextBox.BackColor = this.disabledTextBoxForeAndBackColor;
                    currentTextBox.ForeColor = this.disabledTextBoxForeAndBackColor;
                }

                if (currentComboBox != null)
                {
                    currentComboBox.SelectedIndex = 0;
                    currentComboBox.BackColor = this.disabledTextBoxForeAndBackColor;
                    currentComboBox.ForeColor = this.disabledTextBoxForeAndBackColor;
                }
            }
            else
            {
                currentPanel.BackColor = this.standardPanelBackColor;
                currentLabel.ForeColor = this.standardLabelForeColor;

                if (currentTextBox != null)
                {
                    currentTextBox.BackColor = this.standardTextBoxBackColor;
                    currentTextBox.ForeColor = this.standardTextBoxForeColor;
                }

                if (currentComboBox != null)
                {
                    currentComboBox.BackColor = this.standardTextBoxBackColor;
                    currentComboBox.ForeColor = this.standardTextBoxForeColor;
                }
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>The page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if ((!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)) && (this.LandDetailsDataGridView.OriginalRowCount > 0))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Land ", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult.Equals(DialogResult.Yes))
                {
                    if (this.SaveLandSliceDetails())
                    {
                        // Alert the value slice header on land form Update.
                        this.AlertValueSliceHeader();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (dialogResult.Equals(DialogResult.No))
                {
                    this.CancelButtonClick();

                    return true;
                }

                return false;
            }

            return true;
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

            // when the update button is enabled alert the user with message
            if (this.operationSmartPart.SaveButtonEnable)
            {
                DialogResult currentResult = MessageBox.Show(SharedFunctions.GetResourceString("LandSaveValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (DialogResult.Yes == currentResult)
                {
                    this.SaveButtonClick();

                    // Check save is complete or not 
                    if (this.operationSmartPart.SaveButtonEnable)
                    {
                        sliceValidationFields.DisableNewMethod = true;
                    }
                    else
                    {
                        // when save is completed then the value slice header is alerted automatically.                      
                        sliceValidationFields.DisableNewMethod = false;
                    }

                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                else if (DialogResult.No == currentResult)
                {
                    this.CancelButtonClick();

                    // if no is seleted then alert the value slice header part
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
                // alert the value slice header
                this.AlertValueSliceHeader();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// To check max length of the Break and value per
        /// </summary>
        /// <returns>boolean value</returns>
        private bool CheckBreakValueMaxLength()
        {
            double breakMaxValue = 9999999.99;
            double valuePerMaxValue = 922337203685477.5807;

            double break1;
            double break1ValuePer;
            double break2;
            double break2ValuePer;
            double break3;
            double break3ValuePer;
            double break4;
            double break4ValuePer;

            double.TryParse(this.Break1TextBox.Text.Trim(), out break1);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1ValuePer);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2ValuePer);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3ValuePer);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4ValuePer);

            if (break1 <= breakMaxValue && break2 <= breakMaxValue && break3 <= breakMaxValue
                && break4 <= breakMaxValue && break1ValuePer < valuePerMaxValue && break2ValuePer < valuePerMaxValue
                && break3ValuePer < valuePerMaxValue && break4ValuePer < valuePerMaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the base adjustment required field.
        /// </summary>
        /// <returns>The required field validation status.</returns>
        private bool CheckBaseAdjustmentRequiredField()
        {
            if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                {
                    if (!string.IsNullOrEmpty(this.BaseAdjusmentComboBox.SelectedValue.ToString()))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor)
                    || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue)
                    || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                {
                    // Code modified for #7480 (Land Adjustment fields not allowing 0 as a value)
                    if (!string.IsNullOrEmpty(this.BaseAdjustmentTextBox.Text.Trim()) && this.BaseAdjustmentTextBox.DecimalTextBoxValue >= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                ////Modified by Biju on 01-Dec-2010 to implement #9328
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
                {
                    if (!string.IsNullOrEmpty(this.BaseAdjustmentTextBox.Text.Trim()))
                        return false;
                    else
                        return true;
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Checks the use adjustment required field.
        /// </summary>
        /// <returns>The required field validation status.</returns>
        private bool CheckUseAdjustmentRequiredField()
        {
            if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                {
                    if (!string.IsNullOrEmpty(this.UseAdjusmentComboBox.SelectedValue.ToString()))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor)
                    || this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue)
                    || this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                {
                    // Code modified for #7480 (Land Adjustment fields not allowing 0 as a value)
                    if (!string.IsNullOrEmpty(this.UseAdjustmentTextBox.Text.Trim()) && this.UseAdjustmentTextBox.DecimalTextBoxValue >= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                ////Modified by Biju on 01-Dec-2010 to implement #9328
                else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
                {
                    if (!string.IsNullOrEmpty(this.UseAdjustmentTextBox.Text.Trim()))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// To Save the LandSliceDetails
        /// </summary>
        /// <returns>true or false</returns>
        private bool SaveLandSliceDetails()
        {
            if (this.CheckBreakValueMaxLength())
            {
                if (string.IsNullOrEmpty(this.UnitsTextBox.Text.Trim())
                    || this.UnitsTextBox.DecimalTextBoxValue.Equals(0)
                    || string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim())
                    || string.IsNullOrEmpty(this.ReportAsTextBox.Text.Trim())
                    || (this.flagReportAsLabelChanged && this.ReportAsTextBox.DecimalTextBoxValue < 0)
                    || string.IsNullOrEmpty(this.UnitTypeTextBox.Text.Trim())
                    || (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()) && string.IsNullOrEmpty(this.BaseDollerPerUnitTextBox.Text))
                    || string.IsNullOrEmpty(this.BaseMarketValueTextBox.Text)
                    || string.IsNullOrEmpty(this.FinalMarketValueTextBox.Text)
                    || this.CheckBaseAdjustmentRequiredField()
                    || this.CheckUseAdjustmentRequiredField()
                    || this.CheckInfluenceDetails())
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.LandType1Combo.Focus();
                    return false;
                }
                else
                {
                    F36035LandData saveLandDetailsDataSet = new F36035LandData();
                    F36035LandData.ListLandValueSliceDetailsNewRow landDetailsDataRow = saveLandDetailsDataSet.ListLandValueSliceDetailsNew.NewListLandValueSliceDetailsNewRow();
                    landDetailsDataRow.LandTypeID1 = this.landTypeId1Value;
                    landDetailsDataRow.LandTypeID2 = this.landTypeId2Value;
                    landDetailsDataRow.LandTypeID3 = this.landTypeId3Value;
                    landDetailsDataRow.LandCode = this.LandCodeTextBox.Text.Trim();
                    landDetailsDataRow.SrAcres = this.ReportAsTextBox.DecimalTextBoxValue;
                    landDetailsDataRow.ReportAS = this.reportAsValue.Trim();

                    landDetailsDataRow.UnitType = this.UnitTypeTextBox.Text.Trim();

                    //if (this.ShapeComboBox.SelectedValue != null)
                    //{
                    //    landDetailsDataRow.LandShape = this.ShapeComboBox.Text.Trim();
                    //}
                    //else
                    //{
                    //    landDetailsDataRow.LandShape = "Rectangular";
                    //}

                    /// #19728 Shape Combobox include null value 20130828 Purushotham.
                    if (this.ShapeComboBox.SelectedIndex >= 0)
                    {
                        landDetailsDataRow.LandShape = this.ShapeComboBox.Text.Trim();
                        //if (this.ShapeComboBox.SelectedIndex.Equals(1))
                        //{
                        //    landDetailsDataRow.LandShape = this.ShapeComboBox.Text.Trim();
                        //}
                        //else
                        //{
                        //    landDetailsDataRow.LandShape = "Rectangular";
                        //}
                    }
                    if (!string.IsNullOrEmpty(this.LotWidthTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.LotWidth = this.LotWidthTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LotDepthTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.LotDepth = this.LotDepthTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.FrontageTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.Frontage = this.FrontageTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.UnitsTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.Units = this.UnitsTextBox.DecimalTextBoxValue;
                    }

                    landDetailsDataRow.Break1 = this.Break1TextBox.DecimalTextBoxValue;
                    this.ValuePer1TextBox.TextCustomFormat="#,##0.00##";
                    landDetailsDataRow.Value1 = this.ValuePer1TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break2 = this.Break2TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value2 = this.ValuePer2TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break3 = this.Break3TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value3 = this.ValuePer3TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break4 = this.Break4TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value4 = this.ValuePer4TextBox.DecimalTextBoxValue;

                    if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        landDetailsDataRow.AdjustmentType = (byte)this.BaseAdjusmentTypeComboBox.SelectedValue;

                        if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                        {
                            if (this.BaseAdjusmentComboBox.SelectedValue != null)
                            {
                                landDetailsDataRow.Adjustment = this.BaseAdjusmentComboBox.SelectedValue.ToString();
                            }
                            else
                            {
                                landDetailsDataRow.Adjustment = string.Empty;
                            }
                        }
                        else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                        {
                            landDetailsDataRow.Adjustment = (this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100).ToString();
                        }
                        else
                        {
                            landDetailsDataRow.Adjustment = this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString();
                        }
                    }
                    else
                    {
                        landDetailsDataRow.AdjustmentType = 0;
                    }

                    landDetailsDataRow.AdjDescription = this.BaseReasonForAdjustmentTextBox.Text.Trim();
                    landDetailsDataRow.BaseMrktValue = this.BaseMarketValueTextBox.DecimalTextBoxValue;

                    ////if (this.influenceTypeId1Value > 0)
                    ////{
                    ////    landDetailsDataRow.InfluenceTypeID1 = this.influenceTypeId1Value;
                    ////    landDetailsDataRow.Influence1 = this.influence1Value;
                    ////    landDetailsDataRow.InfluenceDesc1 = this.InfluenceDescription1TextBox.Text.Trim();
                    ////    landDetailsDataRow.InfluenceValue1 = this.InfluenceAdjustment1TextBox.DecimalTextBoxValue;
                    ////}

                    ////if (this.influenceTypeId2Value > 0)
                    ////{
                    ////    landDetailsDataRow.InfluenceTypeID2 = this.influenceTypeId2Value;
                    ////    landDetailsDataRow.Influence2 = this.influence2Value;
                    ////    landDetailsDataRow.InfluenceDesc2 = this.InfluenceDescription2TextBox.Text.Trim();
                    ////    landDetailsDataRow.InfluenceValue2 = this.InfluenceAdjustment2TextBox.DecimalTextBoxValue;
                    ////}

                    ////if (this.influenceTypeId3Value > 0)
                    ////{
                    ////    landDetailsDataRow.InfluenceTypeID3 = this.influenceTypeId3Value;
                    ////    landDetailsDataRow.Influence3 = this.influence3Value;
                    ////    landDetailsDataRow.InfluenceDesc3 = this.InfluenceDescription3TextBox.Text.Trim();
                    ////    landDetailsDataRow.InfluenceValue3 = this.InfluenceAdjustment3TextBox.DecimalTextBoxValue;
                    ////}

                    landDetailsDataRow.FinalMrktValue = this.FinalMarketValueTextBox.DecimalTextBoxValue;

                    if (this.ProgramComboBox.SelectedValue != null)
                    {
                        landDetailsDataRow.ProgramID = (byte)this.ProgramComboBox.SelectedValue;
                    }
                    else
                    {
                        landDetailsDataRow.ProgramID = 0;
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        landDetailsDataRow.UseAdjustmentType = (byte)this.UseAdjusmentTypeComboBox.SelectedValue;

                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                        {
                            if (this.UseAdjusmentComboBox.SelectedValue != null)
                            {
                                landDetailsDataRow.UseAdjustment = this.UseAdjusmentComboBox.SelectedValue.ToString();
                            }
                            else
                            {
                                landDetailsDataRow.UseAdjustment = string.Empty;
                            }
                        }
                        else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                        {
                            landDetailsDataRow.UseAdjustment = (this.UseAdjustmentTextBox.DecimalTextBoxValue / 100).ToString();
                        }
                        else
                        {
                            landDetailsDataRow.UseAdjustment = this.UseAdjustmentTextBox.DecimalTextBoxValue.ToString();
                        }
                    }
                    else
                    {
                        landDetailsDataRow.UseAdjustmentType = 0;
                    }

                    landDetailsDataRow.UseBaseDollarPerUnit = this.UseBaseDollarsPerUnitTextBox.DecimalTextBoxValue;
                    landDetailsDataRow.UseAdjDescription = this.ReasonForUseAdjTextBox.Text.Trim();
                    landDetailsDataRow.FinalUseValue = this.FinalUseValueTextBox.DecimalTextBoxValue;

                    landDetailsDataRow.ValueSliceID = this.formValueSliceId;

                    saveLandDetailsDataSet.ListLandValueSliceDetailsNew.Rows.Add(landDetailsDataRow);
                    string saveLandDetailsXmlString = TerraScanCommon.GetXmlString(saveLandDetailsDataSet.ListLandValueSliceDetailsNew);

                    string saveInfluenceDetails = string.Empty;

                    saveInfluenceDetails = TerraScanCommon.GetXmlString(this.clearDataTable);

                    if (this.selectedRow == -1)
                    {
                        this.selectedRow = 0;
                    }

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.landUniqueID = 0;
                        this.selectedRow = 0;
                        if (this.LandDetailsDataGridView.NumRowsVisible > 7 && this.LandDetailsDataGridView.NumRowsVisible < 15)
                        {
                            int tempRowHeigh = 22;
                            this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.NumRowsVisible + 1;
                            this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height + tempRowHeigh;
                            this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height + tempRowHeigh;
                            this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height + tempRowHeigh;
                            this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height + tempRowHeigh;
                            this.LandPictureBox.Height = this.LandPictureBox.Height + tempRowHeigh;
                            this.FooterLeftpanel.Top = this.FooterLeftpanel.Top + tempRowHeigh;
                            this.Footerpanel.Top = this.Footerpanel.Top + tempRowHeigh;
                            this.Height = this.EntireLandFormPanel.Height;

                            // Resize the slice with new height
                            SliceResize sliceResize;
                            sliceResize.MasterFormNo = this.masterFormNo;
                            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                            sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                        }
                        else if (this.LandDetailsDataGridView.NumRowsVisible >= 15)
                        {
                            this.SetSmartPartHeight(true);
                        }
                    }
                    else if (this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value != null && !string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString()))
                    {
                        int.TryParse(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString(), out this.landUniqueID);

                        if (this.LandDetailsDataGridView.NumRowsVisible >= 15)
                        {
                            this.SetSmartPartHeight(true);
                        }
                    }

                    //DataTable changeSets = this.landDetailsDataSet.ListGridInfluences.Copy();
                    //changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
                    //DataTable changesToSave = changeSets.DefaultView.ToTable();
                    //saveInfluenceDetails = TerraScanCommon.GetXmlString(changesToSave);
                    if (saveInfluenceDetails.Equals("<Root />") || !saveInfluenceDetails.Equals("<Root />"))
                    {
                        //saveInfluenceDetails = TerraScanCommon.GetXmlString(clearDataTable);
                        saveInfluenceDetails = this.GetInfluenceDetails();
                    }

                    //saveInfluenceDetails = this.GetInfluenceDetails();

                    if (saveInfluenceDetails == string.Empty)
                    {
                        saveInfluenceDetails = "<Root />";
                    }

                    this.landUniqueID = this.form36035Control.WorkItem.F36035_InsertLandDetails(this.landUniqueID, saveLandDetailsXmlString, saveInfluenceDetails, TerraScan.Common.TerraScanCommon.UserId);
                    if (this.landUniqueID > 0 && !WSHelper.IsOnLineMode)
                        TerraScanCommon.InsertFieldUseDetails(this.formValueSliceId, "ValueSliceID", TerraScanCommon.UserId);
                    else if (this.landUniqueID <= 0 && !WSHelper.IsOnLineMode)
                        TerraScanCommon.AddFieldUseValues(this.landUniqueID, this.keyField, this.currentFormId, null, TerraScanCommon.UserId);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = true;
                    this.PopulateLandDetailsGridView();
                    this.flagLoadOnProcess = false;

                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.LandDetailsGridViewPanel.Enabled = true;
                    this.LandDetailsDataGridView.Enabled = true;
                    return true;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        /// <summary>
        /// Usde to alert the value slice header
        /// </summary>
        private void AlertValueSliceHeader()
        {
            // Update Appraisal Summary Table
            decimal resultAmount;
            Decimal.TryParse(this.TotalMarketValueLabel.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

            F35002SubFormSaveEventArgs subFormSaveEventArgs;
            subFormSaveEventArgs.type = 5;
            subFormSaveEventArgs.value = resultAmount;
            subFormSaveEventArgs.valueSliceId = this.formValueSliceId;

            subFormSaveEventArgs.amount = resultAmount;
            this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
        }

        /// <summary>
        /// Sets the attachment and comment count.
        /// </summary>
        private void SetAttachmentAndCommentCount()
        {
            this.LandDetailsDataGridView.RowEnter -= new System.Windows.Forms.DataGridViewCellEventHandler(this.LandDetailsDataGridView_RowEnter);

            int.TryParse(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName].Value.ToString(), out this.landUniqueID);
            this.additionalOperationSmartPart.KeyId = this.landUniqueID;

            this.LandDetailsDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandDetailsDataGridView_RowEnter);
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            if (this.additionalOperationSmartPart.KeyId > 0)
            {
                additionalOperationCountEntity.AttachmentCount = this.form36035Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form36035Control.WorkItem.GetCommentsCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        /// <summary>
        /// Checks the land fields max limit validation.
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="validatedTextBox">The validated text box.</param>
        /// <param name="flagMoneyValidation">if set to <c>true</c> [flag money validation].</param>
        /// <param name="flagNegativeMoneyValidation">if set to <c>true</c> [flag negative money validation].</param>
        /// <returns>The validation flag value.</returns>
        private bool CheckLandFieldsMaxLimitValidation(Control sourceControl, TerraScanTextBox validatedTextBox, bool flagMoneyValidation, bool flagNegativeMoneyValidation)
        {
            double validatedTextBoxValue;
            double.TryParse(validatedTextBox.DecimalTextBoxValue.ToString(), out validatedTextBoxValue);
            bool valueExceeded = false;

            if (flagMoneyValidation)
            {
                if (!flagNegativeMoneyValidation)
                {
                    if (validatedTextBoxValue > Math.Floor(this.maxMoneyFieldValue))
                    {
                        valueExceeded = true;
                    }
                }
                else
                {
                    if (validatedTextBoxValue < Math.Floor(this.minMoneyFieldValue) || validatedTextBoxValue > Math.Floor(this.maxMoneyFieldValue))
                    {
                        valueExceeded = true;
                    }
                }
            }
            else
            {
                if (validatedTextBoxValue > this.maxBaseMarketFieldValue)
                {
                    valueExceeded = true;
                }
            }

            if (valueExceeded)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                validatedTextBox.Text = decimal.Zero.ToString();

                if (sourceControl.GetType().Equals(typeof(TerraScanTextBox)))
                {
                    sourceControl.Text = decimal.Zero.ToString();
                }

                sourceControl.Focus();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Negatives the validation.
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        private void NegativeValidation(TerraScanTextBox sourceControl)
        {
            if (!string.IsNullOrEmpty(sourceControl.Text.ToString())
                && sourceControl.DecimalTextBoxValue < 0)
            {
                sourceControl.Text = "0";
            }
        }

        #endregion Common Methods

        #region Land Types Header Field Methods

        /// <summary>
        /// Initializes the land type combo boxes.
        /// </summary>
        private void InitializeLandTypeComboBoxes()
        {
            this.listlandCodeTypeDataSet = new F36035LandData();
            this.listlandCodeTypeDataSet.ListLandCodeLandType.Clear();
            // Fetch the landType combo data for valueSliceId
            this.listlandCodeTypeDataSet.Merge(this.form36035Control.WorkItem.F36035_ListLandTypeDetails(this.formValueSliceId));

            //if (this.landDetailsDataSet.ListLandType1.Rows.Count > 0)
            //{
            //    int.TryParse(this.landDetailsDataSet.ListLandType1.Rows[0][this.landDetailsDataSet.ListLandType1.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            //}
            //else if (this.landDetailsDataSet.ListLandType2.Rows.Count > 0)
            //{
            //    int.TryParse(this.landDetailsDataSet.ListLandType2.Rows[0][this.landDetailsDataSet.ListLandType2.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            //}
            //else if (this.landDetailsDataSet.ListLandType3.Rows.Count > 0)
            //{
            //    int.TryParse(this.landDetailsDataSet.ListLandType3.Rows[0][this.landDetailsDataSet.ListLandType3.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            //}

            // Initialize the LandType1 ComboBox
            //this.listLandType1ComboDataTable.Clear();

            
            // To assign a empty row in the combo box
            //DataRow customLandType1Row = this.listLandType1ComboDataTable.NewRow();
            //customLandType1Row[this.listLandType1ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
            //customLandType1Row[this.listLandType1ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
            if (this.listlandCodeTypeDataSet.ListLandCodeLandType.Rows.Count > 0)
            {
                int.TryParse(this.listlandCodeTypeDataSet.ListLandCodeLandType.Rows[0]["RollYear"].ToString(), out this.formRollYear);
            }
            this.LandType1Data = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandTypeID1", "LandType1");
            this.LandType1Data.DefaultView.Sort = "LandType1 Asc";   
            DataRow customLandType1Row = this.LandType1Data.NewRow();
            customLandType1Row["LandTypeID1"] = "0";
            customLandType1Row["LandType1"] = string.Empty;
            this.LandType1Data.Rows.InsertAt(customLandType1Row, 0);
            if (this.LandType1Data.Rows.Count > 0)
            {
                this.LandType1Combo.DataSource = this.LandType1Data;
                this.LandType1Combo.MaxLength = this.LandType1Data.Columns[1].MaxLength;
                this.LandType1Combo.DisplayMember = "LandType1";
                this.LandType1Combo.ValueMember = "LandTypeID1";
            }
            //this.listLandType1ComboDataTable.Rows.Add(customLandType1Row);
            
            //DataTable LandType2Data = new DataTable();
            this.LandType2Data = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandTypeID2", "LandType2");
            this.LandType2Data.Clear();
            this.LandType2Data.DefaultView.Sort = "LandType2 Asc";
            DataRow[] LandRow2 = this.listlandCodeTypeDataSet.ListLandCodeLandType.Select("LandType1=NULL");
            if(LandRow2.Length >0)
            {
                
                DataRow customLandType2Row = this.LandType2Data.NewRow();
                customLandType2Row["LandTypeID2"] = "0";
                customLandType2Row["LandType2"] = string.Empty;
                this.LandType2Data.Rows.InsertAt(customLandType2Row, 0);
            }
            else
            {
                DataRow customLandType2Row = this.LandType2Data.NewRow();
                customLandType2Row["LandTypeID2"] = "0";
                customLandType2Row["LandType2"] = string.Empty;
                this.LandType2Data.Rows.InsertAt(customLandType2Row, 0);
            }
            if (LandType2Data.Rows.Count > 0)
            {
                this.LandType2Combo.DataSource = this.LandType2Data;
                this.LandType2Combo.MaxLength = this.LandType2Data.Columns[1].MaxLength;
                this.LandType2Combo.DisplayMember = this.LandType2Data.Columns[1].ColumnName;
                this.LandType2Combo.ValueMember = this.LandType2Data.Columns[0].ColumnName;
            }
            //DataTable LandType3Data = new DataTable();

            this.LandType3Data = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandTypeID3", "LandType3");
            this.LandType3Data.Clear();
            this.LandType3Data.DefaultView.Sort = "LandType3 Asc";
            DataRow[] LandRow3 = this.listlandCodeTypeDataSet.ListLandCodeLandType.Select("LandType1=NULL AND LandType2= NULL");
            if (LandRow3.Length > 0)
            {
                
                DataRow customLandType3Row = this.LandType3Data.NewRow();
                customLandType3Row["LandTypeID3"] = "0";
                customLandType3Row["LandType3"] = string.Empty;
                this.LandType3Data.Rows.InsertAt(customLandType3Row, 0);
            }
            else
            {
                DataRow customLandType3Row = this.LandType3Data.NewRow();
                customLandType3Row["LandTypeID3"] = "0";
                customLandType3Row["LandType3"] = string.Empty;
                this.LandType3Data.Rows.InsertAt(customLandType3Row, 0);
            }
            if (this.LandType3Data.Rows.Count > 0)
            {
                this.LandType3Combo.DataSource = this.LandType3Data;
                this.LandType3Combo.MaxLength = this.LandType3Data.Columns[1].MaxLength;
                this.LandType3Combo.DisplayMember = this.LandType3Data.Columns[1].ColumnName;
                this.LandType3Combo.ValueMember = this.LandType3Data.Columns[0].ColumnName;
            }
            //this.listLandType1ComboDataTable.Merge(ds);

            //if (this.listLandType1ComboDataTable.Rows.Count > 0)
            //{
            //    this.LandType1Combo.DataSource = this.listLandType1ComboDataTable;
            //    this.LandType1Combo.MaxLength = this.landDetailsDataSet.ListLandType1.LandTypeColumn.MaxLength;
            //    this.LandType1Combo.DisplayMember = this.landDetailsDataSet.ListLandType1.LandTypeColumn.ColumnName;
            //    this.LandType1Combo.ValueMember = this.landDetailsDataSet.ListLandType1.LandTypeIDColumn.ColumnName;
            //}

            // Initialize the LandType2 ComboBox
            //this.listLandType2ComboDataTable.Clear();

            ////To assign a empty row in the combo box
        //    DataRow customLandType2Row = this.listLandType2ComboDataTable.NewRow();
        //    customLandType2Row[this.listLandType2ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
        //    customLandType2Row[this.listLandType2ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
        //    this.listLandType2ComboDataTable.Rows.Add(customLandType2Row);

        //    this.listLandType2ComboDataTable.Merge(this.landDetailsDataSet.ListLandType2);

        //    if (this.listLandType2ComboDataTable.Rows.Count > 0)
        //    {
        //        this.LandType2Combo.DataSource = this.listLandType2ComboDataTable;
        //        this.LandType2Combo.MaxLength = this.landDetailsDataSet.ListLandType2.LandTypeColumn.MaxLength;
        //        this.LandType2Combo.DisplayMember = this.landDetailsDataSet.ListLandType2.LandTypeColumn.ColumnName;
        //        this.LandType2Combo.ValueMember = this.landDetailsDataSet.ListLandType2.LandTypeIDColumn.ColumnName;
        //    }

        //    // Initialize the LandType3 ComboBox
        //    this.listLandType3ComboDataTable.Clear();

        //    // To assign a empty row in the combo box
        //    DataRow customLandType3Row = this.listLandType3ComboDataTable.NewRow();
        //    customLandType3Row[this.listLandType3ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
        //    customLandType3Row[this.listLandType3ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
        //    this.listLandType3ComboDataTable.Rows.Add(customLandType3Row);

        //    this.listLandType3ComboDataTable.Merge(this.landDetailsDataSet.ListLandType3);

        //    if (this.listLandType3ComboDataTable.Rows.Count > 0)
        //    {
        //        this.LandType3Combo.DataSource = this.listLandType3ComboDataTable;
        //        this.LandType3Combo.MaxLength = this.landDetailsDataSet.ListLandType3.LandTypeColumn.MaxLength;
        //        this.LandType3Combo.DisplayMember = this.landDetailsDataSet.ListLandType3.LandTypeColumn.ColumnName;
        //        this.LandType3Combo.ValueMember = this.landDetailsDataSet.ListLandType3.LandTypeIDColumn.ColumnName;
        //    }
        }

        /// <summary>
        /// Gets the selected CFG land types row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The selected config landtype row</returns>
        private F36035LandData.GetCfgLandTypeLabelRow GetSelectedCfgLandTypesRow(int rowIndex)
        {
            return (F36035LandData.GetCfgLandTypeLabelRow)this.landDetailsDataSet.GetCfgLandTypeLabel.Rows[rowIndex];
        }

        /// <summary>
        /// Sets the land type lable values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandTypeLableValues(F36035LandData.GetCfgLandTypeLabelRow selectedRow)
        {
            if (!selectedRow.IsLandTypeLabel1Null())
            {
                if (string.IsNullOrEmpty(selectedRow.LandTypeLabel1.Trim()))
                {
                    this.LandType1Label.Text = string.Empty;
                }
                else
                {
                    this.LandType1Label.Text = selectedRow.LandTypeLabel1.Replace(":", string.Empty).Trim() + ":";
                }
            }
            else
            {
                this.LandType1Label.Text = string.Empty;
            }

            if (!selectedRow.IsLandTypeLabel2Null())
            {
                if (string.IsNullOrEmpty(selectedRow.LandTypeLabel2.Trim()))
                {
                    this.LandType2label.Text = string.Empty;
                }
                else
                {
                    this.LandType2label.Text = selectedRow.LandTypeLabel2.Replace(":", string.Empty).Trim() + ":";
                }
            }
            else
            {
                this.LandType2label.Text = string.Empty;
            }

            if (!selectedRow.IsLandTypeLabel3Null())
            {
                if (string.IsNullOrEmpty(selectedRow.LandTypeLabel3.Trim()))
                {
                    this.LandType3label.Text = string.Empty;
                }
                else
                {
                    this.LandType3label.Text = selectedRow.LandTypeLabel3.Replace(":", string.Empty).Trim() + ":";
                }
            }
            else
            {
                this.LandType3label.Text = string.Empty;
            }

            // get the config values for the srAcres and change the label and textBox behavior
            if (!selectedRow.IsReportAsLabelNull())
            {
                if (string.IsNullOrEmpty(selectedRow.ReportAsLabel.Trim()))
                {
                    this.ReportAslabel.Text = string.Empty;
                }
                else
                {
                    this.ReportAslabel.Text = selectedRow.ReportAsLabel.Replace(":", string.Empty).Trim() + ":";
                }
            }
            else
            {
                this.ReportAslabel.Text = string.Empty;
            }

            if (!selectedRow.IsIsChangeLabelNull())
            {
                this.flagReportAsLabelChanged = selectedRow.IsChangeLabel;
            }
            else
            {
                this.flagReportAsLabelChanged = false;
            }
        }

        /// <summary>
        /// Gets the land code.
        /// </summary>
        private void GetLandCode()
        {
            // reset the form fileds
           // this.ClearFormFields(false);

            this.landDetailsDataSet.Get_LandCode.Clear();
            DataTable dt= this.form36035Control.WorkItem.F36035_GetLandCode(this.formRollYear, this.landTypeId1Value, this.landTypeId2Value, this.landTypeId3Value, this.formValueSliceId, null).Get_LandCode;
            this.landDetailsDataSet.Get_LandCode.Merge(dt);
            dt.Clear(); 
            if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
            {
                this.SetLandCodeAndBreakValues(this.GetSelectedLandCodeRow(0));
            }

            this.landUnitType = this.UnitTypeTextBox.Text.Trim();
            this.landValueCurveFormula = this.LandValueCurveFormulaTextBox.Text.Trim();

            // verify the reportAsLabel changed and disply the reportAs textbox accordingly
            if (this.flagReportAsLabelChanged)
            {
                this.ReportAslabel.Visible = true;
                this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, false, true);
            }
            else
            {
                this.reportAsValue = this.ReportAsTextBox.Text.Trim();

                // Check for ReportAs value for null and disable/enable field
                if (string.IsNullOrEmpty(this.reportAsValue))
                {
                    this.ReportAslabel.Visible = false;
                    this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, true, false);
                }
                else
                {
                    this.ReportAslabel.Visible = true;
                    this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, false, false);
                    this.ReportAsTextBox.ForeColor = Color.Gray;
                }
            }

            // change the base marke value fields based on the base adjustment type
            this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();

            // calculate the base units textbox
            this.CalculateBaseUnitsTextBox();
            filteredTable.Rows.Clear();
            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            //this.InfluenceGridView.DataSource = this.landDetailsDataSet.ListGridInfluences.DefaultView;
            
            if (this.landUnitType.Equals("Front Foot") || this.landUnitType.Equals("FF")
                || this.landUnitType.Equals("Front Feet"))
            {
                this.Frontagelabel.Visible = false;
                if (!string.IsNullOrEmpty(this.FrontageTextBox.Text))
                {
                    this.tempFrontText = this.FrontageTextBox.Text;
                }
                this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, true, false);
                
            }
            else
            {
                this.Frontagelabel.Visible = true;
                this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, false, true);
                if (!string.IsNullOrEmpty(this.tempFrontText))
                {
                    this.FrontageTextBox.Text = this.tempFrontText;
                }
                this.tempFrontText = string.Empty;   
            }

            // check for the Units text box max value
            if (this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.UnitsTextBox, false, false))
            {
                this.LotWidthTextBox.Text = decimal.Zero.ToString();
                this.LotDepthTextBox.Text = decimal.Zero.ToString();
            }

            // calculates the base doller per unit text box
            this.CalculateBaseDollerPerUnitTextBox();

            // calculate the base market value text box
            this.CalculateBaseMarketValueTextBox();

            // check for the base market value max limit
            this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.BaseMarketValueTextBox, true, true);

            ////// calculate the influence adjustment textbox fields
            ////this.CalculateInfluenceAdjustmentTextBoxFields(this.UnitsTextBox);
            this.CalculateFinalMarketValueTextBox(0);

            // calculates the UseBaseDollerPerUnit and finalUseValue textbox
            this.CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox();

            // check for the final use value max limit
            this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.FinalUseValueTextBox, true, true);
        }

        /// <summary>
        /// Gets the selected land details row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The selected land details row.</returns>
        private F36035LandData.ListLandValueSliceDetailsNewRow GetSelectedLandDetailsRow(int rowIndex)
        {
            return (F36035LandData.ListLandValueSliceDetailsNewRow)this.landDetailsDataSet.ListLandValueSliceDetailsNew.DefaultView[rowIndex].Row;
        }

        /// <summary>
        /// Gets the index of the selected land code row.
        /// </summary>
        /// <param name="landCodeValue">The land code value.</param>
        /// <returns>Index of the specified landCodeValue in the dataset</returns>
        private int GetSelectedLandCodeRowIndex(int landCodeValue)
        {
            int tempIndex = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Rows.Count;
            DataTable tempDataTable = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Copy();
            tempDataTable.DefaultView.RowFilter = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName + " = " + landCodeValue.ToString();

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
            }

            return tempIndex;
        }

        /// <summary>
        /// Sets the land form field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandFormFieldValues(F36035LandData.ListLandValueSliceDetailsNewRow selectedRow)
        {
            // Fill the LandType header field values
            this.SetLandTypesHeaderFieldValues(selectedRow);

            // Fill the BaseMarketValue field values
            this.SetBaseMarketValueFieldValues(selectedRow);

            ////// Fill the MarketValueInfluence field values
            ////this.SetMarketValueInfluenceFieldValues(selectedRow);

            // Fill UseValue field values
            this.SetUseValueFieldValues(selectedRow);

            // Set the Attachment and Comment button count
            this.SetAttachmentAndCommentCount();

            ////// Fill the MarketValueInfluence Grid
            if (!selectedRow.IsLUIDNull())
            {
                this.SetMarketValueInfluenceFieldValues(selectedRow.LUID);
                this.CalculateFinalMarketValueTextBox(selectedRow.LUID);
            }
            
            if (this.landUnitType.Equals("Front Foot") || this.landUnitType.Equals("FF")
              || this.landUnitType.Equals("Front Feet"))
            {
                this.Frontagelabel.Visible = false;
                if (!string.IsNullOrEmpty(this.FrontageTextBox.Text))
                {
                    this.temp1FrontText = this.FrontageTextBox.Text;
                }
                this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, true, false);
               
            }
            else
            {
                this.Frontagelabel.Visible = true;
                this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, false, true);
                if (!string.IsNullOrEmpty(this.temp1FrontText))
                {
                    this.FrontageTextBox.Text = this.temp1FrontText;
                }
                this.temp1FrontText = string.Empty;  
            }

        }

        /// <summary>
        /// Sets the land types header field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandTypesHeaderFieldValues(F36035LandData.ListLandValueSliceDetailsNewRow selectedRow)
        {
            if (!selectedRow.IsRollYearNull())
            {
                this.formRollYear = selectedRow.RollYear;
            }

            if (!selectedRow.IsLandTypeID1Null())
            {
                this.LandType1Combo.SelectedValue = selectedRow.LandTypeID1;
                this.landTypeId1Value = selectedRow.LandTypeID1;
                this.GetLandType1LandCode(); 
            }
            else
            {
                this.LandType1Combo.SelectedIndex = 0;
                this.landTypeId1Value = 0;
                this.GetLandType1LandCode();
            }

            if (!selectedRow.IsLandTypeID2Null())
            {
                this.LandType2Combo.SelectedValue = selectedRow.LandTypeID2;
                this.landTypeId2Value = selectedRow.LandTypeID2;
                this.GetLandType2LandCode(); 
            }
            else
            {
                this.LandType2Combo.SelectedIndex = 0;
                this.landTypeId2Value = 0;
                this.GetLandType2LandCode();
            }

            if (!selectedRow.IsLandTypeID3Null())
            {
                this.LandType3Combo.SelectedValue = selectedRow.LandTypeID3;
                this.landTypeId3Value = selectedRow.LandTypeID3;
                this.GetLandTypeLandCode(); 
            }
            else
            {
                this.LandType3Combo.SelectedIndex = 0;
                this.landTypeId3Value = 0;
                this.GetLandTypeLandCode(); 
            }

            if (!selectedRow.IsLandCodeNull())
            {
                this.LandCodeTextBox.Text = selectedRow.LandCode;
            }
            else
            {
                this.LandCodeTextBox.Text = string.Empty;
            }

            // verify the flag reportAsLabel changed and assign values accordingly
            if (this.flagReportAsLabelChanged)
            {
                if (!selectedRow.IsSrAcresNull())
                {
                    this.ReportAsTextBox.Text = selectedRow.SrAcres.ToString();
                }
                else
                {
                    this.ReportAsTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsReportASNull())
                {
                    this.reportAsValue = selectedRow.ReportAS;
                }
                else
                {
                    this.reportAsValue = string.Empty;
                }
            }
            else
            {
                if (!selectedRow.IsReportASNull())
                {
                    this.ReportAsTextBox.Text = selectedRow.ReportAS;
                    this.reportAsValue = selectedRow.ReportAS;
                }
                else
                {
                    this.ReportAsTextBox.Text = string.Empty;
                    this.reportAsValue = string.Empty;
                }
            }

            if (!selectedRow.IsUnitTypeNull())
            {
                this.UnitTypeTextBox.Text = selectedRow.UnitType;
                this.UnitsLabel.Text = selectedRow.UnitType + ":";
                this.landUnitType = selectedRow.UnitType;
            }
            else
            {
                this.UnitTypeTextBox.Text = string.Empty;
                this.UnitsLabel.Text = string.Empty;
                this.landUnitType = string.Empty;
            }

            // check for flag reportAsLabel changed and do the enable/disable
            if (this.flagReportAsLabelChanged)
            {
                this.ReportAslabel.Visible = true;
                this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, false, true);
            }
            else
            {
                // Check for ReportAs value for null and disable/enable field
                if (string.IsNullOrEmpty(this.reportAsValue))
                {
                    this.ReportAslabel.Visible = false;
                    this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, true, false);
                }
                else
                {
                    this.ReportAslabel.Visible = true;
                    this.SetFieldsFillToLightGrayAndDisable(this.ReportAspanel, this.ReportAslabel, this.ReportAsTextBox, null, false, false);
                    this.ReportAsTextBox.ForeColor = Color.Gray;
                }
            }
        }

        #endregion Land Types Header Field Methods

        #region Base Market Value Methods

        /// <summary>
        /// Initializes the shape combo box.
        /// </summary>
        private void InitializeShapeComboBox()
        {
            this.listShapeDetailsTable.Clear();

            //// Adding shape types in FormLevel
            //DataRow customShapeRow;

            //customShapeRow = this.listShapesComboDataTable.NewRow();
            //customShapeRow[this.listShapesComboDataTable.ShapeIDColumn.ColumnName] = 0;
            //customShapeRow[this.listShapesComboDataTable.ShapeDescriptionColumn.ColumnName] = "Rectangular";
            //this.listShapesComboDataTable.Rows.Add(customShapeRow);

            //customShapeRow = this.listShapesComboDataTable.NewRow();
            //customShapeRow[this.listShapesComboDataTable.ShapeIDColumn.ColumnName] = 1;
            //customShapeRow[this.listShapesComboDataTable.ShapeDescriptionColumn.ColumnName] = "Irregular";
            //this.listShapesComboDataTable.Rows.Add(customShapeRow);
            listShapeDetailsTable=this.form36035Control.WorkItem.F36035_ListShapeDetails().LandShapesTable;
            this.ShapeComboBox.DataSource = this.listShapeDetailsTable.DefaultView;
            this.ShapeComboBox.DisplayMember = this.listShapeDetailsTable.LandShapeColumn.ColumnName;
            this.ShapeComboBox.ValueMember = this.listShapeDetailsTable.LandShapeColumn.ColumnName;
        }

        /// <summary>
        /// Initializes the base adjustment type combo box.
        /// </summary>
        private void InitializeBaseAdjustmentTypeComboBox()
        {
            // Initialize the LandType1 ComboBox
            this.listBaseAdjustmentTypesComboDataTable.Clear();

            // Adding baseAdujstment Types in FormLevel
            DataRow baseAdjustmentTypeRow1;
            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();

            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 0;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "None";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 1;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Alternate Land Code";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 2;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Factor";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 3;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Unit Value";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 4;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Production";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 5;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Additive";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            ////Added by Biju on 01-Dec-2010 to implement #9328
            baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 6;
            baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Total Value";
            this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

            this.BaseAdjusmentTypeComboBox.DataSource = this.listBaseAdjustmentTypesComboDataTable;
            this.BaseAdjusmentTypeComboBox.DisplayMember = this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName;
            this.BaseAdjusmentTypeComboBox.ValueMember = this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName;
        }

        /// <summary>
        /// Initializes the base adjustment combo box.
        /// </summary>
        private void InitializeBaseAdjustmentComboBox()
        {
            // Initialize the BaseAdjustmentComboBox
            //this.listBaseAdjustmentComboDataTable.Clear();

            //DataRow baseAdjustmentRow;
            //baseAdjustmentRow = this.listBaseAdjustmentComboDataTable.NewRow();

            //baseAdjustmentRow[this.listBaseAdjustmentComboDataTable.LandCodeColumn.ColumnName] = string.Empty;
            //baseAdjustmentRow[this.listBaseAdjustmentComboDataTable.RollYearColumn.ColumnName] = this.formRollYear;
            //this.listBaseAdjustmentComboDataTable.Rows.Add(baseAdjustmentRow);
            this.LandCodeData.Clear();
            this.LandCodeData = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandCode", "RollYear");
            this.LandCodeData.DefaultView.Sort = "LandCode Asc";
            //this.LandCodeData.Clear();
            //this.LandCodeData.DefaultView.Sort = "LandCode Asc";
            //this.LandCodeData = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandCode","RollYear");
            DataRow baseAdjustmentRow;
            baseAdjustmentRow = this.LandCodeData.NewRow();
            baseAdjustmentRow[this.LandCodeData.Columns[0]] = string.Empty;
            baseAdjustmentRow[this.LandCodeData.Columns[1]] = this.formRollYear;
            this.LandCodeData.Rows.InsertAt(baseAdjustmentRow,0);
            this.BaseAdjusmentComboBox.MaxLength = this.LandCodeData.Columns[0].MaxLength;
            this.BaseAdjusmentComboBox.DisplayMember = this.LandCodeData.Columns[0].ColumnName;
            this.BaseAdjusmentComboBox.ValueMember = this.LandCodeData.Columns[0].ColumnName;
            this.BaseAdjusmentComboBox.DataSource = this.LandCodeData;
            //this.BaseAdjusmentComboBox.SelectedIndex = 0; 
        }

        /// <summary>
        /// Called when [shape combo change set base market value fields].
        /// </summary>
        private void OnShapeComboChangeSetBaseMarketValueFields()
        {
            this.LotDepthTextBox.Text = string.Empty;
            this.LotWidthTextBox.Text = string.Empty;
            this.FrontageTextBox.Text = string.Empty;

            // change the width and depth fields behavior on chage of shape type
            //this.ChangeWidthAndDepthFieldsBehaviorOnChangeOfShapeType();
        }

        /// <summary>
        /// Changes the type of the width and depth fields behavior on change of shape.
        /// </summary>
        private void ChangeWidthAndDepthFieldsBehaviorOnChangeOfShapeType()
        {
            bool disable = false;

            if (this.ShapeComboBox.SelectedValue != null && this.ShapeComboBox.SelectedValue.Equals((short)ShapeTypes.Rectangular))
            {
                disable = false;
            }
            else if (this.ShapeComboBox.SelectedValue != null && this.ShapeComboBox.SelectedValue.Equals((short)ShapeTypes.Irregular))
            {
                disable = true;
            }

            this.LotWidthLabel.Visible = !disable;
            this.LotDepthLabel.Visible = !disable;
            this.SetFieldsFillToLightGrayAndDisable(this.LotWidthPanel, this.LotWidthLabel, this.LotWidthTextBox, null, disable, true);
            this.SetFieldsFillToLightGrayAndDisable(this.LotDepthPanel, this.LotDepthLabel, this.LotDepthTextBox, null, disable, true);
            
        }

        /// <summary>
        /// Called when [base adjustment type change set base market value fields].
        /// </summary>
        private void OnBaseAdjustmentTypeChangeSetBaseMarketValueFields()
        {
            if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {

                if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
                {
                    this.InitializeBaseAdjustmentComboBox();
                    this.BaseAdjusmentComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.BaseAdjusmentComboBox.Text = string.Empty;
                }
                this.BaseAdjustmentTextBox.Text = string.Empty;
                this.BaseDollerPerUnitTextBox.Text = string.Empty;
                this.calculatedBaseDollerPerUnitValue = 0;

                // change the base market value field behavior on change of base adj combo type
                this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();
            }
        }

        /// <summary>
        /// Changes the type of the base market value fields behavior on change of base adjustment.
        /// </summary>
        private void ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType()
        {
            bool adjDisabled = false;
            bool disabled = false;
            bool altColor = false;
            bool formulaDisabled = false;

            this.BaseAdjustmentTextBox.Visible = true;
            this.BaseAdjusmentComboBox.Visible = false;

            if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None)
                || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor)
                || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
            {
                if (string.IsNullOrEmpty(this.landValueCurveFormula.Trim()))
                {
                    if (this.break1Value > 0)
                    {
                        this.Break1TextBox.Text = this.break1Value.ToString();
                    }

                    if (this.valuePer1Value > 0)
                    {
                        this.ValuePer1TextBox.Text = this.valuePer1Value.ToString();
                    }

                    if (this.break2Value > 0)
                    {
                        this.Break2TextBox.Text = this.break2Value.ToString();
                    }

                    if (this.valuePer2Value > 0)
                    {
                        this.ValuePer2TextBox.Text = this.valuePer2Value.ToString();
                    }

                    if (this.break3Value > 0)
                    {
                        this.Break3TextBox.Text = this.break3Value.ToString();
                    }

                    if (this.valuePer3Value > 0)
                    {
                        this.ValuePer3TextBox.Text = this.valuePer3Value.ToString();
                    }

                    if (this.break4Value > 0)
                    {
                        this.Break4TextBox.Text = this.break4Value.ToString();
                    }

                    if (this.valuePer4Value > 0)
                    {
                        this.ValuePer4TextBox.Text = this.valuePer4Value.ToString();
                    }

                    if (string.IsNullOrEmpty(this.Break1TextBox.Text.Trim()))
                    {
                        disabled = false;
                        adjDisabled = true;
                        formulaDisabled = false;
                    }
                    else
                    {
                        adjDisabled = true;
                        disabled = true;
                        altColor = true;
                    }
                }
                else
                {
                    adjDisabled = true;
                    disabled = false;
                    formulaDisabled = true;
                    this.LandValueCurveFormulaTextBox.Text = this.landValueCurveFormula.Trim();
                }

                if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                {
                    //disabled = false;
                    //formulaDisabled = false;
                    adjDisabled = false;
                    this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                    this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                    this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                    //Modifed to display 5 decimal Precisons
                    this.BaseAdjustmentTextBox.Precision = 5;
                    this.BaseAdjustmentTextBox.TextCustomFormat = "0.00###%";
                }
                ////modified by Biju on 01-Dec-2010 to implement #9328
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                {
                    //disabled = false;
                    //formulaDisabled = false;
                    adjDisabled = false;
                    this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                    this.BaseAdjustmentTextBox.AllowNegativeSign = true;
                    this.BaseAdjustmentTextBox.ApplyNegativeStandard = true;
                    this.BaseAdjustmentTextBox.ApplyNegativeForeColor = Color.Green;
                    //Modifed to display 2 decimal Precisons
                    this.BaseAdjustmentTextBox.Precision = 2;
                    this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
                }
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
            {
                disabled = false;
                formulaDisabled = false;
                this.BaseAdjustmentTextBox.Visible = false;
                this.BaseAdjusmentComboBox.Visible = true;
            }

            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue))
                //Commented by purushotham and added condtion below
                 //|| this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
            {
                disabled = false;
                formulaDisabled = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                //Modifed to display 4 decimal Precisons
                this.BaseAdjustmentTextBox.Precision = 4;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00##";
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
            {
                //disabled = false;
                //formulaDisabled = false;
                adjDisabled = false;
                this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = true;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = true;
                this.BaseAdjustmentTextBox.ApplyNegativeForeColor = Color.Green;
                this.BaseAdjustmentTextBox.Precision = 2;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
                //Added seperate condtion for Production Field
            else if(this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
            {
                disabled = false;
                formulaDisabled = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                //Modifed to display 2 decimal Precisons
                this.BaseAdjustmentTextBox.Precision = 2;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }

            this.BaseDollerPerUnitLabel.Visible = !formulaDisabled;
            this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, formulaDisabled, false);
            this.SetFieldsFillToLightGrayAndDisable(this.BaseAdjustmentPanel, this.BaseAdjustmentLabel, this.BaseAdjustmentTextBox, this.BaseAdjusmentComboBox, adjDisabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.BaseReasonForAdjustmentPanel, this.BaseReasonForAdjusmentLabel, this.BaseReasonForAdjustmentTextBox, null, adjDisabled, true);

            // Disable break fields for AdjType > 0
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break1panel, this.Break1label, this.Break1TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer1panel, this.ValuePer1label, this.ValuePer1TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break2panel, this.Break2label, this.Break2TextBox, !disabled, altColor);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer2panel, this.ValuePer2label, this.ValuePer2TextBox, !disabled, altColor);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break3panel, this.Break3label, this.Break3TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer3panel, this.ValuePer3label, this.ValuePer3TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break4panel, this.Break4label, this.Break4TextBox, !disabled, altColor);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer4panel, this.ValuePer4label, this.ValuePer4TextBox, !disabled, altColor);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.LandValueCurveFormulaPanel, this.LandValueCurveFormulalabel, this.LandValueCurveFormulaTextBox, !formulaDisabled, false);
        }

        /// <summary>
        /// Sets the base market value field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetBaseMarketValueFieldValues(F36035LandData.ListLandValueSliceDetailsNewRow selectedRow)
        {
            if (!selectedRow.IsLandShapeNull() && selectedRow.LandShape.Equals("Rectangular"))
            {
                this.ShapeComboBox.SelectedValue = "Rectangular";
                this.ShapeComboBox.SelectedIndex = 2;
            }
            else if (!selectedRow.IsLandShapeNull() && selectedRow.LandShape.Equals("Irregular"))
            {
                this.ShapeComboBox.SelectedValue = "Irregular";
                this.ShapeComboBox.SelectedIndex = 1;
            }

            // change the width and depth fields behavior on chage of shape type
            //this.ChangeWidthAndDepthFieldsBehaviorOnChangeOfShapeType();
            //
            
            /// #19728 Shape Combobox include null value 201308228 purushotham
            if (this.ShapeComboBox.SelectedIndex >= 0) // && this.ShapeComboBox.SelectedValue.Equals((short)ShapeTypes.Rectangular))// if (this.ShapeComboBox.SelectedValue != null) 
            {
                if (!selectedRow.IsLotWidthNull())
                {
                    this.LotWidthTextBox.Text = selectedRow.LotWidth.ToString();
                }
                else
                {
                    this.LotWidthTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsLotDepthNull())
                {
                    this.LotDepthTextBox.Text = selectedRow.LotDepth.ToString();
                }
                else
                {
                    this.LotDepthTextBox.Text = string.Empty;
                }
            }

            if (!selectedRow.IsFrontageNull())
            {
                this.FrontageTextBox.Text = selectedRow.Frontage.ToString();
            }
            else
            {
                this.FrontageTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsUnitsNull())
            {
                this.UnitsTextBox.Text = selectedRow.Units.ToString();
            }
            else
            {
                this.UnitsTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsBaseDollarPerUnitNull())
            {
                this.BaseDollerPerUnitTextBox.Text = selectedRow.BaseDollarPerUnit.ToString();
                this.calculatedBaseDollerPerUnitValue = selectedRow.BaseDollarPerUnit;
            }
            else
            {
                this.BaseDollerPerUnitTextBox.Text = string.Empty;
                this.calculatedBaseDollerPerUnitValue = 0;
            }

            if (!selectedRow.IsBreak1Null())
            {
                this.Break1TextBox.Text = selectedRow.Break1.ToString();
                this.break1Value = selectedRow.Break1;
            }
            else
            {
                this.Break1TextBox.Text = string.Empty;
                this.break1Value = 0;
            }

            if (!selectedRow.IsValue1Null())
            {
                this.ValuePer1TextBox.Text = selectedRow.Value1.ToString();
                this.valuePer1Value = selectedRow.Value1;
            }
            else
            {
                this.ValuePer1TextBox.Text = string.Empty;
                this.valuePer1Value = 0;
            }

            if (!selectedRow.IsBreak2Null())
            {
                this.Break2TextBox.Text = selectedRow.Break2.ToString();
                this.break2Value = selectedRow.Break2;
            }
            else
            {
                this.Break2TextBox.Text = string.Empty;
                this.break2Value = 0;
            }

            if (!selectedRow.IsValue2Null())
            {
                this.ValuePer2TextBox.Text = selectedRow.Value2.ToString();
                this.valuePer2Value = selectedRow.Value2;
            }
            else
            {
                this.ValuePer2TextBox.Text = string.Empty;
                this.valuePer2Value = 0;
            }

            if (!selectedRow.IsBreak3Null())
            {
                this.Break3TextBox.Text = selectedRow.Break3.ToString();
                this.break3Value = selectedRow.Break3;
            }
            else
            {
                this.Break3TextBox.Text = string.Empty;
                this.break3Value = 0;
            }

            if (!selectedRow.IsValue3Null())
            {
                this.ValuePer3TextBox.Text = selectedRow.Value3.ToString();
                this.valuePer3Value = selectedRow.Value3;
            }
            else
            {
                this.ValuePer3TextBox.Text = string.Empty;
                this.valuePer3Value = 0;
            }

            if (!selectedRow.IsBreak4Null())
            {
                this.Break4TextBox.Text = selectedRow.Break4.ToString();
                this.break4Value = selectedRow.Break4;
            }
            else
            {
                this.Break4TextBox.Text = string.Empty;
                this.break4Value = 0;
            }

            if (!selectedRow.IsValue4Null())
            {
                this.ValuePer4TextBox.Text = selectedRow.Value4.ToString();
                this.valuePer4Value = selectedRow.Value4;
            }
            else
            {
                this.ValuePer4TextBox.Text = string.Empty;
                this.valuePer4Value = 0;
            }

            if (!selectedRow.IsVFormulaNull())
            {
                this.LandValueCurveFormulaTextBox.Text = selectedRow.VFormula;
                this.landValueCurveFormula = selectedRow.VFormula;
            }
            else
            {
                this.LandValueCurveFormulaTextBox.Text = string.Empty;
                this.landValueCurveFormula = string.Empty;
            }

            if (!selectedRow.IsBaseValueNull())
            {
                this.landCodeBaseValue = selectedRow.BaseValue;
            }
            else
            {
                this.landCodeBaseValue = 0;
            }

            if (!selectedRow.IsUseBaseValueNull())
            {
                this.landCodeUseBaseValue = selectedRow.UseBaseValue;
            }
            else
            {
                this.landCodeUseBaseValue = 0;
            }

            if (!selectedRow.IsMrktMultiplierNull())
            {
                this.landCodeMarketMultiplierValue = selectedRow.MrktMultiplier;
            }
            else
            {
                this.landCodeMarketMultiplierValue = 0;
            }

            if (!selectedRow.IsUseMultiplierNull())
            {
                this.landCodeUseMultiplierValue = selectedRow.UseMultiplier;
            }
            else
            {
                this.landCodeUseMultiplierValue = 0;
            }

            if (!selectedRow.IsAdjustmentTypeNull())
            {
                this.BaseAdjusmentTypeComboBox.SelectedValue = selectedRow.AdjustmentType;
            }
            else
            {
                this.BaseAdjusmentTypeComboBox.SelectedValue = 0;
            }

            // change the base market value field behavior on change of base adj combo type
            this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();

            if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None))
            {
                this.BaseAdjustmentTextBox.Text = string.Empty;
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
            {
                if (!selectedRow.IsAdjustmentNull())
                {
                    this.BaseAdjusmentComboBox.SelectedValue = selectedRow.Adjustment;

                    this.getLandCodeBaseValueDataTable.Clear();
                    this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                    this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(selectedRow.Adjustment, this.formValueSliceId, null));
                    this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                    if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                    {
                        decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.BaseValueColumn].ToString(), out this.alternateLandCodeBaseValue);
                    }
                }
                else
                {
                    this.BaseAdjusmentComboBox.SelectedIndex = 0;
                }
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
            {
                if (!selectedRow.IsAdjustmentNull())
                {
                    decimal factorAdjValue;
                    decimal.TryParse(selectedRow.Adjustment, out factorAdjValue);
                    factorAdjValue *= 100;
                    this.BaseAdjustmentTextBox.Text = factorAdjValue.ToString();
                }
                else
                {
                    this.BaseAdjustmentTextBox.Text = string.Empty;
                }
            }
            else
            {
                if (!selectedRow.IsAdjustmentNull())
                {
                    this.BaseAdjustmentTextBox.Text = selectedRow.Adjustment;
                }
                else
                {
                    this.BaseAdjustmentTextBox.Text = string.Empty;
                }
            }

            if (!selectedRow.IsAdjDescriptionNull())
            {
                this.BaseReasonForAdjustmentTextBox.Text = selectedRow.AdjDescription;
            }
            else
            {
                this.BaseReasonForAdjustmentTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsBaseMrktValueNull())
            {
                this.BaseMarketValueTextBox.Text = selectedRow.BaseMrktValue.ToString();
                this.calculatedBaseMarketValue = selectedRow.BaseMrktValue;
            }
            else
            {
                this.BaseMarketValueTextBox.Text = string.Empty;
                this.calculatedBaseMarketValue = 0;
            }

            if (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()))
            {
                this.SetBreakFieldsFillToLightGrayAndDisable(this.LandValueCurveFormulaPanel, this.LandValueCurveFormulalabel, this.LandValueCurveFormulaTextBox, true, false);
                this.BaseDollerPerUnitLabel.Visible = true;
                this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, false, false);
                this.BaseDollerPerUnitTextBox.ForeColor = Color.Gray;

                // calculates the Base$/Unit textbox form level only
                this.CalculateBaseDollerPerUnitTextBox();
            }
            else
            {
                this.BaseDollerPerUnitLabel.Visible = false;
                this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, true, false);
            }
        }

        /// <summary>
        /// Gets the selected land code row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The selected landCode row.</returns>
        private F36035LandData.Get_LandCodeRow GetSelectedLandCodeRow(int rowIndex)
        {
            return (F36035LandData.Get_LandCodeRow)this.landDetailsDataSet.Get_LandCode.Rows[rowIndex];
        }

        /// <summary>
        /// Sets the land code and break values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandCodeAndBreakValues(F36035LandData.Get_LandCodeRow selectedRow)
        {
            if (!selectedRow.IsLandCodeNull())
            {
                this.LandCodeTextBox.Text = selectedRow.LandCode;
            }
            else
            {
                this.LandCodeTextBox.Text = string.Empty;
            }

            // check for flag reportAsLabel change and assign the default value zero
            if (this.flagReportAsLabelChanged)
            {
                ////Commented by Biju on 24-Sep-2009 to fix #4015
                ////this.ReportAsTextBox.Text = decimal.Zero.ToString();

                if (!selectedRow.IsReportASNull())
                {
                    this.reportAsValue = selectedRow.ReportAS;
                }
                else
                {
                    this.reportAsValue = string.Empty;
                }
            }
            else
            {
                if (!selectedRow.IsReportASNull())
                {
                    this.ReportAsTextBox.Text = selectedRow.ReportAS;
                }
                else
                {
                    this.ReportAsTextBox.Text = string.Empty;
                }
            }

            if (!selectedRow.IsBreak1Null())
            {
                this.Break1TextBox.Text = selectedRow.Break1.ToString();
                this.break1Value = selectedRow.Break1;
            }
            else
            {
                this.Break1TextBox.Text = string.Empty;
                this.break1Value = 0;
            }

            if (!selectedRow.IsValue1Null())
            {
                this.ValuePer1TextBox.Text = selectedRow.Value1.ToString();
                this.valuePer1Value = selectedRow.Value1;
            }
            else
            {
                this.ValuePer1TextBox.Text = string.Empty;
                this.valuePer1Value = 0;
            }

            if (!selectedRow.IsBreak2Null())
            {
                this.Break2TextBox.Text = selectedRow.Break2.ToString();
                this.break2Value = selectedRow.Break2;
            }
            else
            {
                this.Break2TextBox.Text = string.Empty;
                this.break2Value = 0;
            }

            if (!selectedRow.IsValue2Null())
            {
                this.ValuePer2TextBox.Text = selectedRow.Value2.ToString();
                this.valuePer2Value = selectedRow.Value2;
            }
            else
            {
                this.ValuePer2TextBox.Text = string.Empty;
                this.valuePer2Value = 0;
            }

            if (!selectedRow.IsBreak3Null())
            {
                this.Break3TextBox.Text = selectedRow.Break3.ToString();
                this.break3Value = selectedRow.Break3;
            }
            else
            {
                this.Break3TextBox.Text = string.Empty;
                this.break3Value = 0;
            }

            if (!selectedRow.IsValue3Null())
            {
                this.ValuePer3TextBox.Text = selectedRow.Value3.ToString();
                this.valuePer3Value = selectedRow.Value3;
            }
            else
            {
                this.ValuePer3TextBox.Text = string.Empty;
                this.valuePer3Value = 0;
            }

            if (!selectedRow.IsBreak4Null())
            {
                this.Break4TextBox.Text = selectedRow.Break4.ToString();
                this.break4Value = selectedRow.Break4;
            }
            else
            {
                this.Break4TextBox.Text = string.Empty;
                this.break4Value = 0;
            }

            if (!selectedRow.IsValue4Null())
            {
                this.ValuePer4TextBox.Text = selectedRow.Value4.ToString();
                this.valuePer4Value = selectedRow.Value4;
            }
            else
            {
                this.ValuePer4TextBox.Text = string.Empty;
                this.valuePer4Value = 0;
            }

            if (!selectedRow.IsUnitTypeNull())
            {
                this.UnitTypeTextBox.Text = selectedRow.UnitType;
                this.UnitsLabel.Text = selectedRow.UnitType + ":";
            }
            else
            {
                this.UnitTypeTextBox.Text = string.Empty;
                this.UnitsLabel.Text = string.Empty;
            }

            if (!selectedRow.IsVFormulaNull())
            {
                this.LandValueCurveFormulaTextBox.Text = selectedRow.VFormula;
            }
            else
            {
                this.LandValueCurveFormulaTextBox.Text = string.Empty;
            }

            if (!selectedRow.IsBaseValueNull())
            {
                this.landCodeBaseValue = selectedRow.BaseValue;
            }
            else
            {
                this.landCodeBaseValue = 0;
            }

            if (!selectedRow.IsUseBaseValueNull())
            {
                this.landCodeUseBaseValue = selectedRow.UseBaseValue;
            }
            else
            {
                this.landCodeUseBaseValue = 0;
            }

            if (!selectedRow.IsMrktMultiplierNull())
            {
                this.landCodeMarketMultiplierValue = selectedRow.MrktMultiplier;
            }
            else
            {
                this.landCodeMarketMultiplierValue = 0;
            }

            if (!selectedRow.IsUseMultiplierNull())
            {
                this.landCodeUseMultiplierValue = selectedRow.UseMultiplier;
            }
            else
            {
                this.landCodeUseMultiplierValue = 0;
            }
        }

        /// <summary>
        /// Sets the break fields fill to light gray and disable.
        /// </summary>
        /// <param name="currentPanel">The current panel.</param>
        /// <param name="currentLabel">The current label.</param>
        /// <param name="currentTextBox">The current text box.</param>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <param name="altColor">if set to <c>true</c> [alt color].</param>
        private void SetBreakFieldsFillToLightGrayAndDisable(Panel currentPanel, Label currentLabel, TerraScanTextBox currentTextBox, bool disable, bool altColor)
        {
            currentPanel.Enabled = !disable;

            if (disable)
            {
                currentPanel.BackColor = this.disabledPanelBackColor;
                currentLabel.ForeColor = this.disabledLabelForeColor;

                if (currentTextBox != null)
                {
                    currentTextBox.Text = string.Empty;
                    currentTextBox.BackColor = this.disabledTextBoxForeAndBackColor;
                    currentTextBox.ForeColor = this.disabledTextBoxForeAndBackColor;
                }
            }
            else
            {
                if (altColor)
                {
                    currentPanel.BackColor = Color.FromArgb(227, 255, 227);
                    currentLabel.ForeColor = this.standardLabelForeColor;

                    if (currentTextBox != null)
                    {
                        currentTextBox.BackColor = Color.FromArgb(227, 255, 227);
                        currentTextBox.ForeColor = this.standardTextBoxForeColor;
                    }
                }
                else
                {
                    if (currentPanel.Name.Equals(this.LandValueCurveFormulaPanel.Name))
                    {
                        currentPanel.BackColor = this.standardPanelBackColor;
                        currentLabel.ForeColor = this.standardLabelForeColor;

                        if (currentTextBox != null)
                        {
                            currentTextBox.BackColor = this.standardTextBoxBackColor;
                            currentTextBox.ForeColor = Color.Gray;
                        }
                    }
                    else
                    {
                        currentPanel.BackColor = Color.Gainsboro;
                        currentLabel.ForeColor = this.standardLabelForeColor;

                        if (currentTextBox != null)
                        {
                            currentTextBox.BackColor = Color.Gainsboro;
                            currentTextBox.ForeColor = this.standardTextBoxForeColor;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates for base units text box.
        /// </summary>
        private void CalculateBaseUnitsTextBox()
        {
            if (this.ShapeComboBox.SelectedIndex >0
                && this.ShapeComboBox.SelectedValue.Equals((short)ShapeTypes.Rectangular)
                && (this.landUnitType.Equals("Square Foot") || this.landUnitType.Equals("SF")
                || this.landUnitType.Equals("Sq Ft") || this.landUnitType.Equals("SqFt")
                || this.landUnitType.Equals("Square Feet"))
                && !string.IsNullOrEmpty(this.LotWidthTextBox.Text.Trim())
                && !string.IsNullOrEmpty(this.LotDepthTextBox.Text.Trim()))
            {
                decimal calculatedUnitsValue;
                calculatedUnitsValue = this.LotWidthTextBox.DecimalTextBoxValue * this.LotDepthTextBox.DecimalTextBoxValue;
                this.UnitsTextBox.Text = calculatedUnitsValue.ToString();
            }
        }

        /// <summary>
        /// Calculates the base doller per unit text box.
        /// </summary>
        private void CalculateBaseDollerPerUnitTextBox()
        {
            if (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()) && this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                {
                    this.calculatedBaseDollerPerUnitValue = this.alternateLandCodeBaseValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue; // *(this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100);
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue))
                {
                    this.calculatedBaseDollerPerUnitValue = this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeMarketMultiplierValue * this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue; // *this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                ////Added by Biju on 01-Dec-2010 to implement #9328
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue;
                }
                //MOdifed by purushotham to display 4 decimal Precisons
                if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue))
                {
                    this.BaseDollerPerUnitTextBox.Precision = 4;
                    this.BaseDollerPerUnitTextBox.TextCustomFormat = "0.00##";
                }
                else
                {
                    this.BaseDollerPerUnitTextBox.Precision = 4;
                    this.BaseDollerPerUnitTextBox.TextCustomFormat = "0.00##";
                }
                this.BaseDollerPerUnitTextBox.Text = this.calculatedBaseDollerPerUnitValue.ToString();
            }
            else
            {
                this.BaseDollerPerUnitTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Calculates the base market value text box.
        /// </summary>
        private void CalculateBaseMarketValueTextBox()
        {
            int configValue = 0;
            configValue = Convert.ToInt32(this.landDetailsDataSet.GetCfgLandTypeLabel.Rows[0][this.landDetailsDataSet.GetCfgLandTypeLabel.SegmentRoundToColumn]);
            decimal finalBaseMarketValueCalc;
            if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None)
                    || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor)
                    || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                {
                    if (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()))
                    {                  
                        this.calculatedBaseMarketValue = this.CalculateBreakValuesForBaseMarketValueTextBox();
                    }
                    else
                    {
                        if (!this.UnitsTextBox.DecimalTextBoxValue.Equals(0))
                        {
                            DataSet resultDataSet = new DataSet();
                            resultDataSet = this.form36035Control.WorkItem.F36035_ExecuteVFormula(this.landValueCurveFormula, this.UnitsTextBox.DecimalTextBoxValue);
                            if (resultDataSet != null && resultDataSet.Tables.Count > 0)
                            {
                                if (resultDataSet.Tables[0].Rows.Count > 0)
                                {
                                    decimal.TryParse(resultDataSet.Tables[0].Rows[0][0].ToString(), out this.calculatedBaseMarketValue);
                                }
                            }
                        }
                        else
                        {
                            this.calculatedBaseMarketValue = 0;
                        }
                    }
                    if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                    {
                        this.calculatedBaseMarketValue = this.calculatedBaseMarketValue * (this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100);
                        // this.calculatedBaseMarketValue = this.UnitsTextBox.DecimalTextBoxValue * this.BaseDollerPerUnitTextBox.DecimalTextBoxValue * (this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100);
                    }
                    else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                    {
                        this.calculatedBaseMarketValue = this.calculatedBaseMarketValue + this.BaseAdjustmentTextBox.DecimalTextBoxValue;;
                        //this.calculatedBaseMarketValue = (this.UnitsTextBox.DecimalTextBoxValue * this.BaseDollerPerUnitTextBox.DecimalTextBoxValue) + this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                    }
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                {
                    this.calculatedBaseMarketValue = this.alternateLandCodeBaseValue * this.UnitsTextBox.DecimalTextBoxValue;;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue))
                {
                    this.calculatedBaseMarketValue = this.UnitsTextBox.DecimalTextBoxValue * this.BaseDollerPerUnitTextBox.DecimalTextBoxValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                {
                    this.calculatedBaseMarketValue = this.landCodeMarketMultiplierValue * this.BaseAdjustmentTextBox.DecimalTextBoxValue * this.UnitsTextBox.DecimalTextBoxValue;
                }
                ////Added by Biju on 01-Dec-2010 to implement #9328
                else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
                {
                    this.calculatedBaseMarketValue = this.BaseAdjustmentTextBox.DecimalTextBoxValue; 
                }
            }
             //Reverted to make whole integer by purushotham 
             // this.BaseMarketValueTextBox.TextCustomFormat = "#,##0.#####";
            //21975 - Added to Round Off Base Market Value based of Land Configuration Value
            var tempadjustFactorValue= Convert.ToDecimal(this.calculatedBaseMarketValue.ToString());  
            if (configValue > 1)
            {
                var baseRoundMarketValue = Math.Round((tempadjustFactorValue / configValue), 0 ,MidpointRounding.AwayFromZero);
                finalBaseMarketValueCalc = baseRoundMarketValue * configValue;                
            }
            else
            {
                finalBaseMarketValueCalc = System.Math.Round(tempadjustFactorValue, 0, MidpointRounding.AwayFromZero);
            }

            this.BaseMarketValueTextBox.Text = finalBaseMarketValueCalc.ToString();        
        }

        /// <summary>
        /// Calculates the break values for base market value text box.
        /// </summary>
        /// <returns>The calculated base market value.</returns>
        private decimal CalculateBreakValuesForBaseMarketValueTextBox()
        {
            decimal availableQuantity = 0;
            decimal valueCalculated = 0;

            decimal.TryParse(this.UnitsTextBox.Text, out availableQuantity);

            if (string.IsNullOrEmpty(this.Break1TextBox.Text.Trim()))
            {
                valueCalculated = availableQuantity * this.landCodeBaseValue;
            }
            else
            {
                decimal break1, value1, break2, value2, break3, value3, break4, value4;

                decimal.TryParse(this.Break1TextBox.Text, out break1);
                decimal.TryParse(this.Break2TextBox.Text, out break2);
                decimal.TryParse(this.Break3TextBox.Text, out break3);
                decimal.TryParse(this.Break4TextBox.Text, out break4);

                decimal.TryParse(this.ValuePer1TextBox.Text, out value1);
                decimal.TryParse(this.ValuePer2TextBox.Text, out value2);
                decimal.TryParse(this.ValuePer3TextBox.Text, out value3);
                decimal.TryParse(this.ValuePer4TextBox.Text, out value4);

                if (break1 > 0)
                {
                    if (availableQuantity >= break1)
                    {
                        valueCalculated += break1 * this.landCodeBaseValue;
                        availableQuantity = availableQuantity - break1;
                    }
                    else
                    {
                        valueCalculated += availableQuantity * this.landCodeBaseValue;
                        availableQuantity = 0;
                    }
                }
                else if (break1.Equals(0))
                {
                    // valueCalculated += break1 * unit;
                    availableQuantity = availableQuantity - break1;
                }

                if (break2 > 0)
                {
                    if (availableQuantity >= (break2 - break1))
                    {
                        valueCalculated += (break2 - break1) * value1;
                        availableQuantity = availableQuantity - (break2 - break1);
                    }
                    else
                    {
                        valueCalculated += availableQuantity * value1;
                        availableQuantity = 0;
                    }
                }
                else if (break2.Equals(0))
                {
                    valueCalculated += availableQuantity * value1;
                    availableQuantity = 0;
                }

                if (break3 > 0)
                {
                    if (availableQuantity >= (break3 - break2))
                    {
                        valueCalculated += (break3 - break2) * value2;
                        availableQuantity = availableQuantity - (break3 - break2);
                    }
                    else
                    {
                        valueCalculated += availableQuantity * value2;
                        availableQuantity = 0;
                    }
                }
                else if (break3.Equals(0))
                {
                    valueCalculated += availableQuantity * value2;
                    availableQuantity = 0;
                }

                if (break4 > 0)
                {
                    if (availableQuantity >= (break4 - break3))
                    {
                        valueCalculated += (break4 - break3) * value3;
                        availableQuantity = availableQuantity - (break4 - break3);
                    }
                    else
                    {
                        valueCalculated += availableQuantity * value3;
                        availableQuantity = 0;
                    }
                }
                else if (break4.Equals(0))
                {
                    valueCalculated += availableQuantity * value3;
                    availableQuantity = 0;
                }

                if (availableQuantity > 0)
                {
                    if (break1 > 0)
                    {
                        valueCalculated += availableQuantity * value4;
                    }
                    else
                    {
                        // valueCalculated += availableQuantity * unit;
                    }
                }
            }

            return valueCalculated;
        }

        #endregion Base Market Value Methods

        #region Market Value Influence Methods

        /////// <summary>
        /////// Initializes the influence type1 combo box.
        /////// </summary>
        ////private void InitializeInfluenceType1ComboBox()
        ////{
        ////    this.landDetailsDataSet.ListInfluenceType.Clear();

        ////    //// Fetch the landType combo data for valueSliceId
        ////    this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_ListInfluenceType(this.formValueSliceId));

        ////    ////Initialize the Influence Type ComboBox
        ////    this.listInfluenceType1ComboDataTable.Clear();

        ////    ////To assign a empty row in the combo box
        ////    DataRow customRow = this.listInfluenceType1ComboDataTable.NewRow();
        ////    customRow[this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn.ColumnName] = 0;
        ////    customRow[this.listInfluenceType1ComboDataTable.InfluenceTypeColumn.ColumnName] = string.Empty;
        ////    this.listInfluenceType1ComboDataTable.Rows.Add(customRow);

        ////    this.listInfluenceType1ComboDataTable.Merge(this.landDetailsDataSet.ListInfluenceType);

        ////    if (this.listInfluenceType1ComboDataTable.Rows.Count > 0)
        ////    {
        ////        this.InfluenceType1ComboBox.DataSource = this.listInfluenceType1ComboDataTable;
        ////        this.InfluenceType1ComboBox.MaxLength = this.listInfluenceType1ComboDataTable.InfluenceTypeColumn.MaxLength;
        ////        this.InfluenceType1ComboBox.DisplayMember = this.listInfluenceType1ComboDataTable.InfluenceTypeColumn.ColumnName;
        ////        this.InfluenceType1ComboBox.ValueMember = this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn.ColumnName;
        ////    }
        ////}

        /////// <summary>
        /////// Initializes the influence type1 combo box.
        /////// </summary>
        ////private void InitializeInfluenceType2ComboBox()
        ////{
        ////    ////Initialize the Influence Type ComboBox
        ////    this.listInfluenceType2ComboDataTable.Clear();

        ////    ////To assign a empty row in the combo box
        ////    DataRow customRow = this.listInfluenceType2ComboDataTable.NewRow();
        ////    customRow[this.listInfluenceType2ComboDataTable.InfluenceTypeIDColumn.ColumnName] = 0;
        ////    customRow[this.listInfluenceType2ComboDataTable.InfluenceTypeColumn.ColumnName] = string.Empty;
        ////    this.listInfluenceType2ComboDataTable.Rows.Add(customRow);

        ////    this.listInfluenceType2ComboDataTable.Merge(this.landDetailsDataSet.ListInfluenceType);

        ////    if (this.listInfluenceType2ComboDataTable.Rows.Count > 0)
        ////    {
        ////        this.InfluenceType2ComboBox.DataSource = this.listInfluenceType2ComboDataTable;
        ////        this.InfluenceType2ComboBox.MaxLength = this.listInfluenceType2ComboDataTable.InfluenceTypeColumn.MaxLength;
        ////        this.InfluenceType2ComboBox.DisplayMember = this.listInfluenceType2ComboDataTable.InfluenceTypeColumn.ColumnName;
        ////        this.InfluenceType2ComboBox.ValueMember = this.listInfluenceType2ComboDataTable.InfluenceTypeIDColumn.ColumnName;
        ////    }
        ////}

        /////// <summary>
        /////// Initializes the influence type1 combo box.
        /////// </summary>
        ////private void InitializeInfluenceType3ComboBox()
        ////{
        ////    ////Initialize the Influence Type ComboBox
        ////    this.listInfluenceType3ComboDataTable.Clear();

        ////    ////To assign a empty row in the combo box
        ////    DataRow customRow = this.listInfluenceType3ComboDataTable.NewRow();
        ////    customRow[this.listInfluenceType3ComboDataTable.InfluenceTypeIDColumn.ColumnName] = 0;
        ////    customRow[this.listInfluenceType3ComboDataTable.InfluenceTypeColumn.ColumnName] = string.Empty;
        ////    this.listInfluenceType3ComboDataTable.Rows.Add(customRow);

        ////    this.listInfluenceType3ComboDataTable.Merge(this.landDetailsDataSet.ListInfluenceType);

        ////    if (this.listInfluenceType3ComboDataTable.Rows.Count > 0)
        ////    {
        ////        this.InfluenceType3ComboBox.DataSource = this.listInfluenceType3ComboDataTable;
        ////        this.InfluenceType3ComboBox.MaxLength = this.listInfluenceType3ComboDataTable.InfluenceTypeColumn.MaxLength;
        ////        this.InfluenceType3ComboBox.DisplayMember = this.listInfluenceType3ComboDataTable.InfluenceTypeColumn.ColumnName;
        ////        this.InfluenceType3ComboBox.ValueMember = this.listInfluenceType3ComboDataTable.InfluenceTypeIDColumn.ColumnName;
        ////    }
        ////}

        /////// <summary>
        /////// Fills the influence1 field values.
        /////// </summary>
        ////private void FillInfluence1FieldValues()
        ////{
        ////    // change the Influence fields behavior on changing the influenceType value
        ////    this.ChangeInfluence1FieldsBehaviorOnInfluenceType();
        ////    this.Influence1TextBox.Text = this.influence1Value.ToString();
        ////    this.InfluenceDescription1TextBox.Text = this.influenceDescription1Value;
        ////    this.CalculateInfluence1AdjustmentTextBox();
        ////}

        /////// <summary>
        /////// Changes the type of the influence1 fields behavior on influence.
        /////// </summary>
        ////private void ChangeInfluence1FieldsBehaviorOnInfluenceType()
        ////{
        ////    if (this.influenceType1Value.Equals(1))
        ////    {
        ////        this.Influence1TextBox.ApplyCFGFormat = false;
        ////        this.Influence1TextBox.TextCustomFormat = "0.00%";
        ////    }
        ////    else if (this.influenceType1Value.Equals(2))
        ////    {
        ////        this.Influence1TextBox.ApplyCFGFormat = true;
        ////        this.Influence1TextBox.TextCustomFormat = "#,##0.00";
        ////    }
        ////}

        /////// <summary>
        /////// Fills the influence2 field values.
        /////// </summary>
        ////private void FillInfluence2FieldValues()
        ////{
        ////    // change the Influence fields behavior on changing the influenceType value
        ////    this.ChangeInfluence2FieldsBehaviorOnInfluenceType();
        ////    this.Influence2TextBox.Text = this.influence2Value.ToString();
        ////    this.InfluenceDescription2TextBox.Text = this.influenceDescription2Value;
        ////    this.CalculateInfluence2AdjustmentTextBox();
        ////}

        /////// <summary>
        /////// Changes the type of the influence2 fields behavior on influence.
        /////// </summary>
        ////private void ChangeInfluence2FieldsBehaviorOnInfluenceType()
        ////{
        ////    if (this.influenceType2Value.Equals(1))
        ////    {
        ////        this.Influence2TextBox.ApplyCFGFormat = false;
        ////        this.Influence2TextBox.TextCustomFormat = "0.00%";
        ////    }
        ////    else if (this.influenceType2Value.Equals(2))
        ////    {
        ////        this.Influence2TextBox.ApplyCFGFormat = false;
        ////        this.Influence2TextBox.TextCustomFormat = "#,##0.00";
        ////    }
        ////}

        /////// <summary>
        /////// Fills the influence3 field values.
        /////// </summary>
        ////private void FillInfluence3FieldValues()
        ////{
        ////    // change the Influence fields behavior on changing the influenceType value
        ////    this.ChangeInfluence3FieldsBehaviorOnInfluenceType();
        ////    this.Influence3TextBox.Text = this.influence3Value.ToString();
        ////    this.InfluenceDescription3TextBox.Text = this.influenceDescription3Value;
        ////    this.CalculateInfluence3AdjustmentTextBox();
        ////}

        /////// <summary>
        /////// Changes the type of the influence3 fields behavior on influence.
        /////// </summary>
        ////private void ChangeInfluence3FieldsBehaviorOnInfluenceType()
        ////{
        ////    if (this.influenceType3Value.Equals(1))
        ////    {
        ////        this.Influence3TextBox.ApplyCFGFormat = false;
        ////        this.Influence3TextBox.TextCustomFormat = "0.00%";
        ////    }
        ////    else if (this.influenceType3Value.Equals(2))
        ////    {
        ////        this.Influence3TextBox.ApplyCFGFormat = true;
        ////        this.Influence3TextBox.TextCustomFormat = "#,##0.00";
        ////    }
        ////}

        /////// <summary>
        /////// Sets the market value influence field values.
        /////// </summary>
        /////// <param name="selectedRow">The selected row.</param>
        ////private void SetMarketValueInfluenceFieldValues(F36035LandData.ListLandValueSliceDetailsNewRow selectedRow)
        ////{
        ////    if (!selectedRow.IsInfluenceTypeID1Null())
        ////    {
        ////        this.InfluenceType1ComboBox.SelectedValue = selectedRow.InfluenceTypeID1;
        ////        this.influenceTypeId1Value = selectedRow.InfluenceTypeID1;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceType1ComboBox.SelectedIndex = 0;
        ////        this.influenceTypeId1Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceTypeID2Null())
        ////    {
        ////        this.InfluenceType2ComboBox.SelectedValue = selectedRow.InfluenceTypeID2;
        ////        this.influenceTypeId2Value = selectedRow.InfluenceTypeID2;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceType2ComboBox.SelectedIndex = 0;
        ////        this.influenceTypeId2Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceTypeID3Null())
        ////    {
        ////        this.InfluenceType3ComboBox.SelectedValue = selectedRow.InfluenceTypeID3;
        ////        this.influenceTypeId3Value = selectedRow.InfluenceTypeID3;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceType3ComboBox.SelectedIndex = 0;
        ////        this.influenceTypeId3Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceType1ValueNull())
        ////    {
        ////        this.influenceType1Value = selectedRow.InfluenceType1Value;
        ////    }
        ////    else
        ////    {
        ////        this.influenceType1Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceType2ValueNull())
        ////    {
        ////        this.influenceType2Value = selectedRow.InfluenceType2Value;
        ////    }
        ////    else
        ////    {
        ////        this.influenceType2Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceType3ValueNull())
        ////    {
        ////        this.influenceType3Value = selectedRow.InfluenceType3Value;
        ////    }
        ////    else
        ////    {
        ////        this.influenceType3Value = 0;
        ////    }

        ////    // change the Influence1 fields behavior on changing the influenceType value
        ////    this.ChangeInfluence1FieldsBehaviorOnInfluenceType();

        ////    // change the Influence2 fields behavior on changing the influenceType value
        ////    this.ChangeInfluence2FieldsBehaviorOnInfluenceType();

        ////    // change the Influence3 fields behavior on changing the influenceType value
        ////    this.ChangeInfluence3FieldsBehaviorOnInfluenceType();

        ////    if (!selectedRow.IsInfluence1Null())
        ////    {
        ////        this.Influence1TextBox.Text = selectedRow.Influence1.ToString();
        ////        this.influence1Value = selectedRow.Influence1;
        ////    }
        ////    else
        ////    {
        ////        this.Influence1TextBox.Text = string.Empty;
        ////        this.influence1Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluence2Null())
        ////    {
        ////        this.Influence2TextBox.Text = selectedRow.Influence2.ToString();
        ////        this.influence2Value = selectedRow.Influence2;
        ////    }
        ////    else
        ////    {
        ////        this.Influence2TextBox.Text = string.Empty;
        ////        this.influence2Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluence3Null())
        ////    {
        ////        this.Influence3TextBox.Text = selectedRow.Influence3.ToString();
        ////        this.influence3Value = selectedRow.Influence3;
        ////    }
        ////    else
        ////    {
        ////        this.Influence3TextBox.Text = string.Empty;
        ////        this.influence3Value = 0;
        ////    }

        ////    if (!selectedRow.IsInfluenceDesc1Null())
        ////    {
        ////        this.InfluenceDescription1TextBox.Text = selectedRow.InfluenceDesc1;
        ////        this.influenceDescription1Value = selectedRow.InfluenceDesc1;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceDescription1TextBox.Text = string.Empty;
        ////        this.influenceDescription1Value = string.Empty;
        ////    }

        ////    if (!selectedRow.IsInfluenceDesc2Null())
        ////    {
        ////        this.InfluenceDescription2TextBox.Text = selectedRow.InfluenceDesc2;
        ////        this.influenceDescription2Value = selectedRow.InfluenceDesc2;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceDescription2TextBox.Text = string.Empty;
        ////        this.influenceDescription2Value = string.Empty;
        ////    }

        ////    if (!selectedRow.IsInfluenceDesc3Null())
        ////    {
        ////        this.InfluenceDescription3TextBox.Text = selectedRow.InfluenceDesc3;
        ////        this.influenceDescription3Value = selectedRow.InfluenceDesc3;
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceDescription3TextBox.Text = string.Empty;
        ////        this.influenceDescription3Value = string.Empty;
        ////    }

        ////    if (!selectedRow.IsInfluenceValue1Null())
        ////    {
        ////        this.InfluenceAdjustment1TextBox.Text = selectedRow.InfluenceValue1.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceAdjustment1TextBox.Text = string.Empty;
        ////    }

        ////    if (!selectedRow.IsInfluenceValue2Null())
        ////    {
        ////        this.InfluenceAdjustment2TextBox.Text = selectedRow.InfluenceValue2.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceAdjustment2TextBox.Text = string.Empty;
        ////    }

        ////    if (!selectedRow.IsInfluenceValue3Null())
        ////    {
        ////        this.InfluenceAdjustment3TextBox.Text = selectedRow.InfluenceValue3.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.InfluenceAdjustment3TextBox.Text = string.Empty;
        ////    }

        ////    if (!selectedRow.IsFinalMrktValueNull())
        ////    {
        ////        this.FinalMarketValueTextBox.Text = selectedRow.FinalMrktValue.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.FinalMarketValueTextBox.Text = string.Empty;
        ////    }

        ////    if (this.InfluenceType1ComboBox.SelectedValue != null && this.InfluenceType1ComboBox.SelectedValue.Equals(0))
        ////    {
        ////        this.Influence1TextBox.Text = string.Empty;
        ////        this.InfluenceDescription1TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment1TextBox.Text = string.Empty;
        ////    }

        ////    if (this.InfluenceType2ComboBox.SelectedValue != null && this.InfluenceType2ComboBox.SelectedValue.Equals(0))
        ////    {
        ////        this.Influence2TextBox.Text = string.Empty;
        ////        this.InfluenceDescription2TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment2TextBox.Text = string.Empty;
        ////    }

        ////    if (this.InfluenceType3ComboBox.SelectedValue != null && this.InfluenceType3ComboBox.SelectedValue.Equals(0))
        ////    {
        ////        this.Influence3TextBox.Text = string.Empty;
        ////        this.InfluenceDescription3TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment3TextBox.Text = string.Empty;
        ////    }
        ////}

        /////// <summary>
        /////// Calculates the influence adjustment text box fields.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        ////private void CalculateInfluenceAdjustmentTextBoxFields(object sender)
        ////{
        ////    this.CalculateInfluence1AdjustmentTextBox();
        ////    this.CalculateInfluence2AdjustmentTextBox();
        ////    this.CalculateInfluence3AdjustmentTextBox();

        ////    // check for the influenct adjustment1 text box max value
        ////    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment1TextBox, true, true);

        ////    // check for the influenct adjustment1 text box max value
        ////    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment2TextBox, true, true);

        ////    // check for the influenct adjustment1 text box max value
        ////    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.InfluenceAdjustment3TextBox, true, true);

        ////    // check for the final market value text box max value
        ////    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.FinalMarketValueTextBox, true, true);
        ////}

        /////// <summary>
        /////// Calculates for influence1 adjustment.
        /////// </summary>
        ////private void CalculateInfluence1AdjustmentTextBox()
        ////{
        ////    decimal influence1Adjustment;

        ////    if (this.influenceType1Value.Equals(1))
        ////    {
        ////        influence1Adjustment = this.BaseMarketValueTextBox.DecimalTextBoxValue * this.influence1Value / 100;
        ////        this.InfluenceAdjustment1TextBox.Text = influence1Adjustment.ToString();
        ////    }
        ////    else if (this.influenceType1Value.Equals(2))
        ////    {
        ////        this.InfluenceAdjustment1TextBox.Text = this.Influence1TextBox.DecimalTextBoxValue.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.Influence1TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment1TextBox.Text = string.Empty;
        ////    }

        ////    //// Calculates the finalMarketValue
        ////    this.CalculateFinalMarketValueTextBox();
        ////}

        /////// <summary>
        /////// Calculates for influence2 adjustment.
        /////// </summary>
        ////private void CalculateInfluence2AdjustmentTextBox()
        ////{
        ////    decimal influence2Adjustment;

        ////    if (this.influenceType2Value.Equals(1))
        ////    {
        ////        influence2Adjustment = this.BaseMarketValueTextBox.DecimalTextBoxValue * this.influence2Value / 100;
        ////        this.InfluenceAdjustment2TextBox.Text = influence2Adjustment.ToString();
        ////    }
        ////    else if (this.influenceType2Value.Equals(2))
        ////    {
        ////        this.InfluenceAdjustment2TextBox.Text = this.Influence2TextBox.DecimalTextBoxValue.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.Influence2TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment2TextBox.Text = string.Empty;
        ////    }

        ////    //// Calculates the finalMarketValue
        ////    this.CalculateFinalMarketValueTextBox();
        ////}

        /////// <summary>
        /////// Calculates for influence3 adjustment.
        /////// </summary>
        ////private void CalculateInfluence3AdjustmentTextBox()
        ////{
        ////    decimal influence3Adjustment;

        ////    if (this.influenceType3Value.Equals(1))
        ////    {
        ////        influence3Adjustment = this.BaseMarketValueTextBox.DecimalTextBoxValue * this.influence3Value / 100;
        ////        this.InfluenceAdjustment3TextBox.Text = influence3Adjustment.ToString();
        ////    }
        ////    else if (this.influenceType3Value.Equals(2))
        ////    {
        ////        this.InfluenceAdjustment3TextBox.Text = this.Influence3TextBox.DecimalTextBoxValue.ToString();
        ////    }
        ////    else
        ////    {
        ////        this.Influence3TextBox.Text = string.Empty;
        ////        this.InfluenceAdjustment3TextBox.Text = string.Empty;
        ////    }

        ////    //// Calculates the finalMarketValue
        ////    this.CalculateFinalMarketValueTextBox();
        ////}

        /////// <summary>
        /////// Calculates the final market value text box.
        /////// </summary>
        ////private void CalculateFinalMarketValueTextBox()
        ////{
        ////    decimal calculatedFinalMarketValue;

        ////    calculatedFinalMarketValue = this.BaseMarketValueTextBox.DecimalTextBoxValue + this.InfluenceAdjustment1TextBox.DecimalTextBoxValue + this.InfluenceAdjustment2TextBox.DecimalTextBoxValue + this.InfluenceAdjustment3TextBox.DecimalTextBoxValue;
        ////    this.FinalMarketValueTextBox.Text = calculatedFinalMarketValue.ToString("#,##,0");
        ////}

        #endregion Market Value Influence Methods

        #region Use Value Methods

        /// <summary>
        /// Initializes the land program combo box.
        /// </summary>
        private void InitializeLandProgramComboBox()
        {
            ////Initialize the Influence Type ComboBox
            this.listLandProgramComboDataTable.Clear();

            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_ListLandProgram());

            ////To assign a empty row in the combo box
            DataRow customRow = this.listLandProgramComboDataTable.NewRow();
            customRow[this.listLandProgramComboDataTable.ProgramIDColumn.ColumnName] = 0;
            customRow[this.listLandProgramComboDataTable.ProgramColumn.ColumnName] = "<< Select Program >>";
            this.listLandProgramComboDataTable.Rows.Add(customRow);

            this.listLandProgramComboDataTable.Merge(this.landDetailsDataSet.ListLandProgram);

            if (this.listLandProgramComboDataTable.Rows.Count > 0)
            {
                this.ProgramComboBox.DataSource = this.listLandProgramComboDataTable;
                this.ProgramComboBox.MaxLength = this.listLandProgramComboDataTable.ProgramColumn.MaxLength;
                this.ProgramComboBox.DisplayMember = this.listLandProgramComboDataTable.ProgramColumn.ColumnName;
                this.ProgramComboBox.ValueMember = this.listLandProgramComboDataTable.ProgramIDColumn.ColumnName;
            }
        }

        /// <summary>
        /// Initializes the use adjustment type combo box.
        /// </summary>
        private void InitializeUseAdjustmentTypeComboBox()
        {
            // Initialize the LandType1 ComboBox
            this.listUseAdjustmentTypesComboDataTable.Clear();

            // Adding baseAdujstment Types in FormLevel
            DataRow useAdjustmentTypeRow1;

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 0;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "None";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 1;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Alternate Land Code";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 2;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Factor";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 3;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Unit Value";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 4;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Production";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 5;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Additive";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            ////Added by Biju on 01-Dec-2010 to implement #9328
            useAdjustmentTypeRow1 = this.listUseAdjustmentTypesComboDataTable.NewRow();
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 6;
            useAdjustmentTypeRow1[this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Total Value";
            this.listUseAdjustmentTypesComboDataTable.Rows.Add(useAdjustmentTypeRow1);

            this.UseAdjusmentTypeComboBox.DataSource = this.listUseAdjustmentTypesComboDataTable;
            this.UseAdjusmentTypeComboBox.DisplayMember = this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName;
            this.UseAdjusmentTypeComboBox.ValueMember = this.listUseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName;
        }

        /// <summary>
        /// Initializes the use adjustment combo box.
        /// </summary>
        private void InitializeUseAdjustmentComboBox()
        {
            // Initialize the UseAdjustmentComboBox
            this.UseLandCodeData.Clear();
            this.UseLandCodeData = this.listlandCodeTypeDataSet.ListLandCodeLandType.DefaultView.ToTable(true, "LandCode", "RollYear");
            this.UseLandCodeData.DefaultView.Sort = "LandCode Asc";
            //this.listUseAdjustmentComboDataTable.Clear();
            //DataRow useAdjustmentRow;
            //useAdjustmentRow = this.listUseAdjustmentComboDataTable.NewRow();

            //useAdjustmentRow[this.listUseAdjustmentComboDataTable.LandCodeColumn.ColumnName] = string.Empty;
            //useAdjustmentRow[this.listUseAdjustmentComboDataTable.RollYearColumn.ColumnName] = this.formRollYear;
            //this.listUseAdjustmentComboDataTable.Rows.Add(useAdjustmentRow);
            DataRow baseAdjustmentRow;
            baseAdjustmentRow = this.UseLandCodeData.NewRow();
            baseAdjustmentRow[this.UseLandCodeData.Columns[0]] = string.Empty;
            baseAdjustmentRow[this.UseLandCodeData.Columns[1]] = this.formRollYear;
            this.UseLandCodeData.Rows.InsertAt(baseAdjustmentRow, 0);
            this.UseAdjusmentComboBox.MaxLength = this.UseLandCodeData.Columns[0].MaxLength;
            this.UseAdjusmentComboBox.DisplayMember = this.UseLandCodeData.Columns[0].ColumnName;
            this.UseAdjusmentComboBox.ValueMember = this.UseLandCodeData.Columns[0].ColumnName;
            this.UseAdjusmentComboBox.DataSource = this.UseLandCodeData;
            //this.listUseAdjustmentComboDataTable.Merge(this.landDetailsDataSet.ListLandCode);
            //this.UseAdjusmentComboBox.MaxLength = this.listUseAdjustmentComboDataTable.LandCodeColumn.MaxLength;
            //this.UseAdjusmentComboBox.DisplayMember = this.listUseAdjustmentComboDataTable.LandCodeColumn.ColumnName;
            //this.UseAdjusmentComboBox.ValueMember = this.listUseAdjustmentComboDataTable.LandCodeColumn.ColumnName;
            //this.UseAdjusmentComboBox.DataSource = this.listUseAdjustmentComboDataTable;
        }

        /// <summary>
        /// Sets the use value field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetUseValueFieldValues(F36035LandData.ListLandValueSliceDetailsNewRow selectedRow)
        {
            if (!selectedRow.IsProgramIDNull())
            {
                this.ProgramComboBox.SelectedValue = selectedRow.ProgramID;
            }
            else
            {
                this.ProgramComboBox.SelectedIndex = 0;
            }

            // change the behavior of use value fields on program type changes
            this.ChangeUseValueFieldsBehaviorOnChageOfProgramType();

            int outProgramId;
            if (int.TryParse(this.ProgramComboBox.SelectedValue.ToString(), out outProgramId))
            {
                if (outProgramId > 0)
                {
                    if (!selectedRow.IsUseBaseDollarPerUnitNull())
                    {
                        if (selectedRow.UseBaseDollarPerUnit < 0)
                        {
                            this.calculatedUseBaseDollerPerUnitValue = decimal.Zero;
                            this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                        }
                        else
                        {
                            this.UseBaseDollarsPerUnitTextBox.Text = selectedRow.UseBaseDollarPerUnit.ToString();
                            this.calculatedUseBaseDollerPerUnitValue = selectedRow.UseBaseDollarPerUnit;
                        }
                    }
                    else
                    {
                        this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
                        this.calculatedUseBaseDollerPerUnitValue = 0;
                    }

                    if (!selectedRow.IsUseAdjustmentTypeNull())
                    {
                        this.UseAdjusmentTypeComboBox.SelectedValue = selectedRow.UseAdjustmentType;
                    }
                    else
                    {
                        this.UseAdjusmentTypeComboBox.SelectedValue = 0;
                    }

                    // change the behavoior of the use value fields on adj type changes
                    this.ChangeUseValueFieldsBehaviorOnChageUseAdjustmentType();

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None))
                    {
                        this.UseAdjustmentTextBox.Text = string.Empty;
                    }
                    else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                    {
                        if (!selectedRow.IsUseAdjustmentNull())
                        {
                            this.UseAdjusmentComboBox.SelectedValue = selectedRow.UseAdjustment;

                            this.getLandCodeBaseValueDataTable.Clear();
                            this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_GetLandCodeBaseValue(selectedRow.UseAdjustment, this.formValueSliceId, null));
                            this.getLandCodeBaseValueDataTable.Merge(this.landDetailsDataSet.Get_LandCodeBaseValue);

                            if (this.getLandCodeBaseValueDataTable.Rows.Count > 0)
                            {
                                decimal.TryParse(this.getLandCodeBaseValueDataTable.Rows[0][this.getLandCodeBaseValueDataTable.UseBaseValueColumn].ToString(), out this.alternateLandCodeUseBaseValue);
                            }
                        }
                        else
                        {
                            this.UseAdjusmentComboBox.SelectedIndex = 0;
                        }
                    }
                    else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                    {
                        if (!selectedRow.IsUseAdjustmentNull())
                        {
                            decimal factorUseAdjValue;
                            decimal.TryParse(selectedRow.UseAdjustment, out factorUseAdjValue);
                            factorUseAdjValue *= 100;
                            this.UseAdjustmentTextBox.Text = factorUseAdjValue.ToString();
                        }
                        else
                        {
                            this.UseAdjustmentTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (!selectedRow.IsUseAdjustmentNull())
                        {
                            this.UseAdjustmentTextBox.Text = selectedRow.UseAdjustment;
                        }
                        else
                        {
                            this.UseAdjustmentTextBox.Text = string.Empty;
                        }
                    }

                    if (!selectedRow.IsUseAdjDescriptionNull())
                    {
                        this.ReasonForUseAdjTextBox.Text = selectedRow.UseAdjDescription;
                    }
                    else
                    {
                        this.ReasonForUseAdjTextBox.Text = string.Empty;
                    }

                    if (!selectedRow.IsFinalUseValueNull())
                    {
                        this.FinalUseValueTextBox.Text = selectedRow.FinalUseValue.ToString();
                    }
                    else
                    {
                        this.FinalUseValueTextBox.Text = string.Empty;
                    }
                    ///for Update the value for Modifying after set Program code = none
                    //if (this.UseAdjustmentTextBox.Visible)
                    //{
                    //    this.tempUseAdjustmentTxt = this.UseAdjustmentTextBox.Text;
                    //}
                    //else if (this.UseAdjusmentComboBox.Visible)
                    //{
                    //    this.tempUseAdjustmentCombo = this.UseAdjusmentComboBox.Text;
                    //}
                    //if (!string.IsNullOrEmpty(this.ReasonForUseAdjTextBox.Text))
                    //{
                    //    this.tempReasonForUseAdjText = this.ReasonForUseAdjTextBox.Text;
                    //}
                }
            }
        }

        /// <summary>
        /// Called when [program change set use value fields].
        /// </summary>
        private void OnProgramChangeSetUseValueFields()
        {
            if (this.ProgramComboBox.SelectedValue != null)
            {
                // change the behavior of use value fields on program type changes
                this.ChangeUseValueFieldsBehaviorOnChageOfProgramType();
            }
        }

        /// <summary>
        /// Called when [use adjustment type change set use value fields].
        /// </summary>
        private void OnUseAdjustmentTypeChangeSetUseValueFields()
        {
     
            if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.UseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
                {
                    this.InitializeUseAdjustmentComboBox();
                    this.UseAdjusmentComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.UseAdjusmentComboBox.Text = string.Empty;
                    this.UseAdjustmentTextBox.Text = string.Empty;
                }
                this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
                this.calculatedUseBaseDollerPerUnitValue = decimal.Zero;

                // change the behavoior of the use value fields on adj type changes
                this.ChangeUseValueFieldsBehaviorOnChageUseAdjustmentType();
            }
        }

        /// <summary>
        /// Changes the type of the use value fields behavior on chage use adjustment.
        /// </summary>
        private void ChangeUseValueFieldsBehaviorOnChageUseAdjustmentType()
        {
            bool disabled = false;
            this.UseAdjustmentTextBox.Visible = true;
            this.UseAdjusmentComboBox.Visible = false;

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None))
            {
                disabled = true;
            }
            else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
            {
                disabled = false;
                this.UseAdjustmentTextBox.Visible = false;
                this.UseAdjusmentComboBox.Visible = true;
            }
            else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
            {
                disabled = false;
                this.UseAdjustmentTextBox.ApplyCFGFormat = false;
                this.UseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.UseAdjustmentTextBox.AllowNegativeSign = false;
                //Modifed to display 5 decimal Precisons
                this.UseAdjustmentTextBox.Precision = 5;
                this.UseAdjustmentTextBox.TextCustomFormat = "0.00###%";
            }
            else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue))
                //Commented and added the condition below
               // || this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
            {
                disabled = false;
                this.UseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.UseAdjustmentTextBox.AllowNegativeSign = false;
                //Modifed to display 4 decimal Precisons
                this.UseAdjustmentTextBox.Precision = 4;
                this.UseAdjustmentTextBox.TextCustomFormat = "#,##0.00##";
            }
            ////modified by Biju on 01-Dec-2010 to implement #9328
            else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive)
                    || this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
            {
                disabled = false;
                this.UseAdjustmentTextBox.ApplyCFGFormat = false;
                this.UseAdjustmentTextBox.ApplyNegativeStandard = true;
                this.UseAdjustmentTextBox.AllowNegativeSign = true;
                this.UseAdjustmentTextBox.ApplyNegativeForeColor = Color.Green;
                this.UseAdjustmentTextBox.Precision = 2;
                this.UseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
                //Seperated the condition for Production field 
            else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
            {
                disabled = false;
                this.UseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.UseAdjustmentTextBox.AllowNegativeSign = false;
                this.UseAdjustmentTextBox.Precision = 2;
                this.UseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
        
            this.SetFieldsFillToLightGrayAndDisable(this.UseAdjustmentpanel, this.UseAdjustmentlabel, this.UseAdjustmentTextBox, this.UseAdjusmentComboBox, disabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.ReasonForUseAdjpanel, this.ReasonForUseAdjusmentlabel, this.ReasonForUseAdjTextBox, null, disabled, true);
            //if (!disabled)
            //{
            //    if (this.UseAdjusmentComboBox.Visible)
            //    {
            //        if (!string.IsNullOrEmpty(this.tempUseAdjustmentCombo))
            //        {
            //            this.UseAdjusmentComboBox.Text = this.tempUseAdjustmentCombo;
            //        }
            //    }
            //    if (this.UseAdjustmentTextBox.Visible)
            //    {
            //        if (!string.IsNullOrEmpty(this.tempUseAdjustmentTxt))
            //        {
            //            this.UseAdjustmentTextBox.Text = this.tempUseAdjustmentTxt;
            //        }
            //    }
            //    if (!string.IsNullOrEmpty(this.tempReasonForUseAdjText))
            //    {
            //        this.ReasonForUseAdjTextBox.Text = this.tempReasonForUseAdjText;  
            //    }
            //}
          
        }

        /// <summary>
        /// Changes the type of the use value fields behavior on chage of program.
        /// </summary>
        private void ChangeUseValueFieldsBehaviorOnChageOfProgramType()
        {
            int outProgramId;
            bool disabled;
            bool adjTypeDisabled;
            int.TryParse(this.ProgramComboBox.SelectedValue.ToString(), out outProgramId);

            if (outProgramId.Equals(0))
            {
                disabled = true;
                adjTypeDisabled = true;
                this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
                this.UseAdjusmentTypeComboBox.SelectedIndex = 0;
                this.UseAdjustmentTextBox.Text = string.Empty;
                this.UseAdjusmentComboBox.SelectedIndex = 0;
                this.UseAdjusmentComboBox.Text = string.Empty;
                this.ReasonForUseAdjTextBox.Text = string.Empty;
                this.FinalUseValueTextBox.Text = string.Empty;
                this.UseAdjusmentComboBox.Visible = false;
                this.UseAdjustmentTextBox.Visible = true;
            }
            else
            {
                disabled = false;
                adjTypeDisabled = false;
                if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                {
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.None))
                    {
                        adjTypeDisabled = true;
                    }
                }
            }

            this.SetFieldsFillToLightGrayAndDisable(this.UseBaseDollarsPerUnitPanel, this.UseBaseDollarsPerUnitLabel, this.UseBaseDollarsPerUnitTextBox, null, disabled, false);
            this.SetFieldsFillToLightGrayAndDisable(this.UseAdjusmentTypePanel, this.UseAdjusmentTypelabel, null, this.UseAdjusmentTypeComboBox, disabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.UseAdjustmentpanel, this.UseAdjustmentlabel, this.UseAdjustmentTextBox, this.UseAdjusmentComboBox, adjTypeDisabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.ReasonForUseAdjpanel, this.ReasonForUseAdjusmentlabel, this.ReasonForUseAdjTextBox, null, adjTypeDisabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.FinalUseValuePanel, this.FinalUseValueLabel, this.FinalUseValueTextBox, null, disabled, false);

            if (!disabled)
            {
                this.FinalUseValuePanel.BackColor = Color.FromArgb(181, 203, 133);
                this.FinalUseValueTextBox.BackColor = Color.FromArgb(181, 203, 133);
                this.FinalUseValueTextBox.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Calculates the use base doller per unit and final use value text box.
        /// </summary>
        private void CalculateUseBaseDollerPerUnitAndFinalUseValueTextBox()
        {
            int selectedProgramId;
            F36035LandData getUsebaseDollerDataSet = new F36035LandData();
            decimal calculatedFinalUseValue;
            decimal finalMarketUseValueCalc;

            if (int.TryParse(this.ProgramComboBox.SelectedValue.ToString(), out selectedProgramId))
            {
                if (selectedProgramId > 0)
                {
                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                        {
                            if (!string.IsNullOrEmpty(this.UseAdjusmentComboBox.Text.Trim()))
                            {
                                //this.tempUseAdjustmentCombo = this.UseAdjusmentComboBox.Text;
                                getUsebaseDollerDataSet.Merge(this.form36035Control.WorkItem.F36035_GetUseBaseDollarPerUnit((byte)this.ProgramComboBox.SelectedValue, (byte)this.UseAdjusmentTypeComboBox.SelectedValue, this.UseAdjusmentComboBox.Text, this.landCodeUseBaseValue, this.formRollYear, this.landCodeUseMultiplierValue, this.UnitsTextBox.DecimalTextBoxValue));
                            }
                            else
                            {
                                this.calculatedUseBaseDollerPerUnitValue = decimal.Zero;
                                this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                                this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                            }
                        }
                        else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                        {
                            getUsebaseDollerDataSet.Merge(this.form36035Control.WorkItem.F36035_GetUseBaseDollarPerUnit((byte)this.ProgramComboBox.SelectedValue, (byte)this.UseAdjusmentTypeComboBox.SelectedValue, (this.UseAdjustmentTextBox.DecimalTextBoxValue / 100).ToString(), this.landCodeUseBaseValue, this.formRollYear, this.landCodeUseMultiplierValue, this.UnitsTextBox.DecimalTextBoxValue));
                        }
                        else
                        {
                            getUsebaseDollerDataSet.Merge(this.form36035Control.WorkItem.F36035_GetUseBaseDollarPerUnit((byte)this.ProgramComboBox.SelectedValue, (byte)this.UseAdjusmentTypeComboBox.SelectedValue, this.UseAdjustmentTextBox.DecimalTextBoxValue.ToString(), this.landCodeUseBaseValue, this.formRollYear, this.landCodeUseMultiplierValue, this.UnitsTextBox.DecimalTextBoxValue));
                        }
                    }

                    if (getUsebaseDollerDataSet.GetUseBaseDollarPerUnit.Rows.Count > 0)
                    {
                        decimal.TryParse(getUsebaseDollerDataSet.GetUseBaseDollarPerUnit.Rows[0][getUsebaseDollerDataSet.GetUseBaseDollarPerUnit.UseBaseValueColumn.ColumnName].ToString(), out this.calculatedUseBaseDollerPerUnitValue);
                        decimal.TryParse(getUsebaseDollerDataSet.GetUseBaseDollarPerUnit.Rows[0][getUsebaseDollerDataSet.GetUseBaseDollarPerUnit.FinalUseValueColumn.ColumnName].ToString(), out calculatedFinalUseValue);

                        //Modified by purushotham to change decimal precision
                        this.UseBaseDollarsPerUnitTextBox.Precision = 4;
                        this.UseBaseDollarsPerUnitTextBox.TextCustomFormat = "#,##0.00##";

                        this.UseBaseDollarsPerUnitTextBox.Text = this.calculatedUseBaseDollerPerUnitValue.ToString();
                        // this.FinalUseValueTextBox.TextCustomFormat = "#,##0.#####";
                        
                        //#21975 - Added to Round Off Base Market Value based of Land Configuration Value
                        int configValue = 0;
                        configValue = Convert.ToInt32(this.landDetailsDataSet.GetCfgLandTypeLabel.Rows[0][this.landDetailsDataSet.GetCfgLandTypeLabel.SegmentRoundToColumn]);
                        decimal tempFinalcalcUseValue = calculatedFinalUseValue;                       
                        if (configValue > 1)
                        {
                            var baseRoundMarketValue = Math.Round((tempFinalcalcUseValue / configValue), 0, MidpointRounding.AwayFromZero);
                            finalMarketUseValueCalc = baseRoundMarketValue * configValue;
                        }
                        else
                        {
                            finalMarketUseValueCalc = System.Math.Round(tempFinalcalcUseValue, 0, MidpointRounding.AwayFromZero);
                        }

                        this.FinalUseValueTextBox.Text = finalMarketUseValueCalc.ToString();
                        //this.FinalUseValueTextBox.Text = calculatedFinalUseValue.ToString();                      
                    }
                    else
                    {
                        if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0 && this.UseAdjusmentTypeComboBox.SelectedValue == null)
                        {
                            decimal.TryParse(this.landDetailsDataSet.Get_LandCode.Rows[0][this.landDetailsDataSet.Get_LandCode.UseBaseValueColumn.ColumnName].ToString(), out this.calculatedUseBaseDollerPerUnitValue);
                            this.UseBaseDollarsPerUnitTextBox.Text = this.calculatedUseBaseDollerPerUnitValue.ToString();
                            this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                        }
                        else
                        {
                            this.calculatedUseBaseDollerPerUnitValue = decimal.Zero;
                            this.UseBaseDollarsPerUnitTextBox.Text = decimal.Zero.ToString();
                            this.FinalUseValueTextBox.Text = decimal.Zero.ToString();
                        }
                    }
                }
                else
                {
                    this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
                    this.FinalUseValueTextBox.Text = string.Empty;
                }
            }
            else
            {
                this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
                this.FinalUseValueTextBox.Text = string.Empty;
            }
        }

        #endregion Use Value Methods

        #region InfluenceType Methods

        /// <summary>
        /// Customizes the influence grid view.
        /// </summary>
        private void CustomizeInfluenceGridView()
        {
            this.InfluenceGridView.AutoGenerateColumns = false;
            this.InfluenceTypeID.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceTypeIDColumn.ColumnName;
            this.Influence.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceColumn.ColumnName;
            this.InfluenceDesc.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceDescColumn.ColumnName;
            this.InfluenceValue.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceValueColumn.ColumnName;
            this.InfluenceItemId.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceItemIDColumn.ColumnName;
            this.InfluenceType.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.InfluenceTypeColumn.ColumnName;
            this.LandCodeId.DataPropertyName = this.landDetailsDataSet.ListGridInfluences.LUIDColumn.ColumnName;
            this.InfluenceGridView.PrimaryKeyColumnName = this.landDetailsDataSet.ListGridInfluences.InfluenceItemIDColumn.ColumnName;
        }

        /// <summary>
        /// Loads the influence type combo.
        /// </summary>
        private void LoadInfluenceTypeCombo()
        {
            this.CustomizeInfluenceGridView();

            //// Fetch the landType combo data for valueSliceId
            this.landDetailsDataSet.ListInfluenceType.Rows.Clear();
            this.landDetailsDataSet.Merge(this.form36035Control.WorkItem.F36035_ListInfluenceType(this.formValueSliceId));

            ////Initialize the Influence Type ComboBox
            this.listInfluenceType1ComboDataTable.Rows.Clear();

            ////To assign a empty row in the combo box
            DataRow customRow = this.listInfluenceType1ComboDataTable.NewRow();
            customRow[this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn.ColumnName] = 0;
            customRow[this.listInfluenceType1ComboDataTable.InfluenceTypeColumn.ColumnName] = string.Empty;
            this.listInfluenceType1ComboDataTable.Rows.Add(customRow);

            this.listInfluenceType1ComboDataTable.Merge(this.landDetailsDataSet.ListInfluenceType);

            if (this.listInfluenceType1ComboDataTable.Rows.Count > 0)
            {
                (this.InfluenceTypeID as DataGridViewComboBoxColumn).DataSource = this.listInfluenceType1ComboDataTable;
                (this.InfluenceTypeID as DataGridViewComboBoxColumn).DisplayMember = this.listInfluenceType1ComboDataTable.InfluenceTypeColumn.ColumnName;
                (this.InfluenceTypeID as DataGridViewComboBoxColumn).ValueMember = this.listInfluenceType1ComboDataTable.InfluenceTypeIDColumn.ColumnName;
            }

            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            //this.InfluenceGridView.DataSource = this.landDetailsDataSet.ListGridInfluences.DefaultView;

            if (this.landDetailsDataSet.ListGridInfluences.Rows.Count > 0)
            {
                this.InfluenceGridView.Enabled = true;
            }
            else
            {
                this.InfluenceGridView.Enabled = false;
            }
            this.landDetailsDataSet.ListGridInfluences.AcceptChanges();
        }

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetReadOnly(int rowIndex)
        {
            if (rowIndex > 0)
            {
                if (string.IsNullOrEmpty(this.InfluenceGridView[this.InfluenceTypeID.Name, rowIndex - 1].Value.ToString().Trim())
                    && (string.IsNullOrEmpty(this.InfluenceGridView[this.InfluenceItemId.Name, rowIndex - 1].Value.ToString().Trim())))
                {
                    if (rowIndex < this.InfluenceGridView.OriginalRowCount)
                    {
                        this.InfluenceGridView.Rows[rowIndex].ReadOnly = false;
                    }
                    else
                    {
                        this.InfluenceGridView.Rows[rowIndex].ReadOnly = true;
                    }
                }
                else
                {
                    //if ((int)this.InfluenceGridView[this.InfluenceItemId.Name, rowIndex].Value > 0)
                    //{
                    this.InfluenceGridView.Rows[rowIndex].ReadOnly = false;
                    //}
                    //else
                    //{
                    //    this.InfluenceGridView.Rows[rowIndex].ReadOnly = true;
                    //}
                }
            }
            else if (rowIndex.Equals(0))
            {
                this.InfluenceGridView.Rows[rowIndex].ReadOnly = false;
            }
        }

        /// <summary>
        /// Calculates the final market value text box.
        /// </summary>
        private void CalculateFinalMarketValueTextBox(int LUID)
        {
            decimal calculatedAdjustmentValue;
            decimal finalMarketUseValue=0M;
            string expression;
            expression = "Sum(" + this.landDetailsDataSet.ListGridInfluences.InfluenceValueColumn.ColumnName + ")";

            DataTable filterData = new DataTable();
            filterData = this.landDetailsDataSet.ListLandValueSliceDetailsNew.Copy();
            filterData.DefaultView.RowFilter = this.landDetailsDataSet.ListLandValueSliceDetailsNew.LUIDColumn.ColumnName + " = '" + LUID + "'";

            // Sum of adjustment in influence grid
            decimal.TryParse(this.filteredTable.Compute(expression, "").ToString(), out calculatedAdjustmentValue);

            // Sum of adjustment in influence grid + Market Value
            //21975 - Added to Round Off Base Market Value based of Land Configuration Value
            int configValue = 0;
            configValue = Convert.ToInt32(this.landDetailsDataSet.GetCfgLandTypeLabel.Rows[0][this.landDetailsDataSet.GetCfgLandTypeLabel.SegmentRoundToColumn]);          
            var tempFinalMarketValue = this.BaseMarketValueTextBox.DecimalTextBoxValue + calculatedAdjustmentValue;

            if (formDataLoad)
            {                
                if (filterData.DefaultView.Count > 0)
                {
                    string val = Convert.ToString(filterData.DefaultView[0]["FinalMrktValue"]);
                    if (!String.IsNullOrEmpty(val))
                    {
                        finalMarketUseValue = Convert.ToDecimal(val);
                    }
                }
            }
            else
            {
                if (configValue > 1)
                {
                    var baseRoundMarketValue = Math.Round((tempFinalMarketValue / configValue), 0, MidpointRounding.AwayFromZero);
                    finalMarketUseValue = baseRoundMarketValue * configValue;                    
                }
                else
                {
                    finalMarketUseValue = System.Math.Round(tempFinalMarketValue, 0, MidpointRounding.AwayFromZero);
                }
            }       
           // this.FinalMarketValueTextBox.TextCustomFormat = "#,##,0.#####";
            this.FinalMarketValueTextBox.Text = finalMarketUseValue.ToString("#,##,0");
        }

        /// <summary>
        /// Sets the market value influence field values.
        /// </summary>
        /// <param name="landCodeId">The land code id.</param>
        private void SetMarketValueInfluenceFieldValues(int landCodeId)
        {
            DataTable filterData = new DataTable();
            filterData = this.landDetailsDataSet.ListGridInfluences.Copy();
            filterData.DefaultView.RowFilter = this.landDetailsDataSet.ListGridInfluences.LUIDColumn.ColumnName + " = '" + landCodeId + "'";
            try
            {
                filterData = filterData.DefaultView.ToTable();
                this.filteredTable.Clear();
                this.filteredTable.Merge(filterData);
            }
            catch (Exception ex)
            {
            }
            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            this.clearDataTable.Clear();
            this.clearDataTable = filterData.DefaultView.ToTable();

            this.SetInfluenceScrollBarVisibility();

            this.InfluenceGridView.AllowSorting = true;
        }

        /// <summary>
        /// Gets the influence details.
        /// </summary>
        /// <returns>XMLString of influence details</returns>
        private string GetInfluenceDetails()
        {
            DataSet originalSets = new DataSet();
            DataTable updateData = ((DataView)this.InfluenceGridView.DataSource).ToTable();
            try
            {
                DataRow[] influenceRows = this.landDetailsDataSet.ListGridInfluences.Select(this.landDetailsDataSet.ListGridInfluences.LUIDColumn.ColumnName + " <> " + this.landUniqueID);
                originalSets.Merge(influenceRows);
                if (updateData.Rows.Count == 0)
                {
                    if (originalSets.Tables.Count > 0)
                    {
                        updateData.Clear();
                        updateData.Merge(originalSets.Tables[0]);
                    }
                }
                DataView changeSetsView = new DataView(updateData);
                changeSetsView.RowFilter = "EmptyRecord$ = false AND InfluenceTypeID <> 0";
                updateData = changeSetsView.ToTable();
                //changeSets.Load(((DataView)this.InfluenceGridView.DataSource).ToTable().CreateDataReader(), LoadOption.OverwriteChanges);
            }
            catch (Exception ex)
            {
            }

            string saveInfluenceDetails = string.Empty;
            if ((updateData.Rows.Count > 0))
            {
                saveInfluenceDetails = TerraScanCommon.GetXmlString(updateData);
            }
            return saveInfluenceDetails;
        }

        /// <summary>
        /// Checks the influence details.
        /// </summary>
        /// <returns>Flag for influence details</returns>
        private bool CheckInfluenceDetails()
        {
            string filterValue = this.landDetailsDataSet.ListGridInfluences.InfluenceItemIDColumn.ColumnName + " <> '0' AND "
                                 + this.landDetailsDataSet.ListGridInfluences.InfluenceTypeIDColumn.ColumnName + " = '0'";

            DataRow[] invalidRows = this.filteredTable.Select(filterValue);

            if (invalidRows.Length > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the influence scroll bar visibility.
        /// </summary>
        private void SetInfluenceScrollBarVisibility()
        {
            if (this.InfluenceGridView.OriginalRowCount > 0
                && this.InfluenceGridView.OriginalRowCount >= this.InfluenceGridView.NumRowsVisible)
            {
                F36035LandData.ListGridInfluencesRow newRow = this.filteredTable.NewListGridInfluencesRow();
                newRow["EmptyRecord$"] = "True";
                this.filteredTable.AddListGridInfluencesRow(newRow);
                this.InfluenceGridVerticalScroll.Visible = false;
            }
            else
            {
                this.InfluenceGridVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// Sets the influence adjustment value.
        /// </summary>
        private void SetInfluenceAdjustmentValue()
        {
            if (this.InfluenceGridView.OriginalRowCount > 0)
            {
                for (int rowCount = 0; rowCount < this.InfluenceGridView.OriginalRowCount; rowCount++)
                {
                    if ((int)this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceItemId.Name].Value >= 0
                        && (int)this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceTypeID.Name].Value > 0)
                    {
                        int influenceTypeID;
                        int.TryParse(this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceTypeID.Name].Value.ToString(), out influenceTypeID);
                        string filterCond = "InfluenceTypeID = " + influenceTypeID;
                        DataRow[] choiceRows = this.listInfluenceType1ComboDataTable.Select(filterCond);

                        decimal.TryParse(choiceRows[0][this.listInfluenceType1ComboDataTable.InfluenceColumn].ToString(), out this.influence1Value);

                        this.InfluenceGridView.Rows[rowCount].Cells[this.Influence.Name].Value = this.influence1Value.ToString();
                        byte.TryParse(this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceType.Name].Value.ToString(), out this.influenceType1Value);

                        decimal influence1Adjustment;

                        if (this.influenceType1Value.Equals(1))
                        {
                            influence1Adjustment = this.BaseMarketValueTextBox.DecimalTextBoxValue * this.influence1Value / 100;

                            if ((double)influence1Adjustment < Math.Floor(this.minMoneyFieldValue) || (double)influence1Adjustment > Math.Floor(this.maxMoneyFieldValue))
                            {
                                //MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                influence1Adjustment = 0;
                            }

                            this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceValue.Name].Value = influence1Adjustment.ToString();
                        }
                        else if (this.influenceType1Value.Equals(2))
                        {
                            this.InfluenceGridView.Rows[rowCount].Cells[this.InfluenceValue.Name].Value = this.influence1Value;
                        }
                    }
                }
            }
        }

        #endregion

        private void UseAdjustmentTextBox_TextChanged(object sender, EventArgs e)
        {

        }



        #endregion Private Methods

        private void UseAdjustmentTextBox_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!this.flagLoadOnProcess)
            //    {
            //        if (!string.IsNullOrEmpty(this.tempUseAdjustmentTxt))
            //        {
            //            this.UseAdjustmentTextBox.Text = this.tempUseAdjustmentTxt;
            //        }
                   
            //    }
            //}
            //catch (Exception e1)
            //{
            //    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
        }

        private void LandType2Combo_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
               // int width = LandType2Combo.Width;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandType1Combo_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandType3Combo_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandDetailsDataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grd = sender as DataGridView;
            if ((grd.Rows[e.RowIndex].Cells["IsLandConfigured"].Value.ToString() != null))
            {
                if (grd.Rows[e.RowIndex].Cells["IsLandConfigured"].Value.ToString().ToLower() == "true")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                if (grd.Rows[e.RowIndex].Cells["IsLandConfigured"].Value.ToString().ToLower() == "false")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                }
                //else
                //{
                //    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                //    //if (grd.Rows[e.RowIndex].Cells["IsLandConfigured"].Value.ToString().ToLower() == "false")
                //    //{
                //    //    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
                //    //}
                //}
                
            }
        }



    }
}
