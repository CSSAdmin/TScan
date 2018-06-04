

namespace D34135
{
    
    #region NameSpace
   
    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Infrastructure.Interface; 
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using TerraScan.SmartParts; 
    using TerraScan.Infrastructure.Interface.Constants;   
    using TerraScan.BusinessEntities; 
    #endregion
    
    [SmartPart]
    public partial class F39135 : BaseSmartPart
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
        /// Instance variable to hold the form39135Controller
        /// </summary>
        private F39135Controller form39135Control;

        /// <summary>
        /// Instance variable to hold the land details dataset.
        /// </summary>
        private F36035LandData landDetailsDataSet = new F36035LandData();


        ///<summary>
        /// Instance variable to hold the land details
        /// </summary>
        private  F39135LandData  landDetailsData;
        ///<summary>
        /// Instance variable to hold the land details
        /// </summary>
        private F39135LandData.ListGridInfluencesDataTable  landInfluenceDetails= new F39135LandData.ListGridInfluencesDataTable();


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

        ///<summary>
        /// Instance Variable for holding the Parcel Roll Year
        /// </summary>
        private int ParcelRollYear;

        ///<summary>
        /// Instance use to hold the AglandID
        /// </summary>
        private int? AglandID;

        ///<summary>
        /// Instance used to allow Sp Call
        /// </summary>
        private bool isAllowSp=true;

        ///<summary>
        /// Instance for adjustmentChange
        /// </summary>
        private bool isAdjustmentChange=false;

        #endregion Common Instance Variables

        #region Land Type Header Instance Variables

        /// <summary>
        /// Instance variable to hold the landType1 datatable
        /// </summary>
        private F39135LandData.ListLandTypes1DataTable   listLandType1ComboDataTable = new F39135LandData.ListLandTypes1DataTable();

        /// <summary>
        /// Instance variable to hold the landType2 datatable
        /// </summary>
        private F39135LandData.ListLandTypes2DataTable listLandType2ComboDataTable = new F39135LandData.ListLandTypes2DataTable();

        /// <summary>
        /// Instance variable to hold the landType3 datatable
        /// </summary>
        private F39135LandData.ListLandTypes3DataTable listLandType3ComboDataTable = new F39135LandData.ListLandTypes3DataTable();

        ///<summary>
        /// Instance Variable to hold the Land Use Datatable
        /// </summary>
        private F39135LandData.GetLandUseTypes_DataTable listLandUseComboDataTable= new F39135LandData.GetLandUseTypes_DataTable();  

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


        ///<summary>
        ///  used to identify the unittextchange
        /// </summary>
        private bool isUnitTxtchange=false;

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
        private F39135LandData.ListGridInfluencesDataTable filteredTable = new F39135LandData.ListGridInfluencesDataTable();
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

        //private F36035LandData.ListGridInfluencesDataTable filteredTable = new F36035LandData.ListGridInfluencesDataTable();

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
        public F39135()
        {
            InitializeComponent();
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
        public F39135(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
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

            //this.landDetailsDataSet = new F39135LandData();
        }

        #endregion

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

          }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form36035 control.
        /// </summary>
        /// <value>The form36035 control.</value>
        [CreateNew]
        public F39135Controller Form39135Control
        {
            get { return this.form39135Control as F39135Controller; }
            set { this.form39135Control = value; }
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

                F39135LandData tempLandDataSet = new F39135LandData();

                //if (this.formValueSliceId != eventArgs.Data.KeyId)
                //{
                //    // To check the invalid key id in set slice event subscription db call is set to F36035_ListLandDetails Method to check invalid key id
                //    tempLandDataSet = this.form39135Control.WorkItem.F39135_LandDetails(eventArgs.Data.KeyId);
                //}

                if (this.landDetailsData.GetValueSliceValid.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.landDetailsData.GetValueSliceValid.Rows[0][this.landDetailsData.GetValueSliceValid.IsOpenColumn.ColumnName].ToString()))
                    {
                        int isValid = 0;
                        int.TryParse(this.landDetailsData.GetValueSliceValid.Rows[0][this.landDetailsData.GetValueSliceValid.IsOpenColumn.ColumnName].ToString(), out isValid);
                        if (isValid>0)
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
                //this.form39135Control.WorkItem.F35001_DeleteValueSlice(this.formValueSliceId, TerraScan.Common.TerraScanCommon.UserId);
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
            //this.AlertValueSliceHeader(); 
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
                //eventArgs.Data.FlagFormClose = this.CheckPageStatus();
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
        private void F39135_Load(object sender, EventArgs e)
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
                this.InitialiseLandUse(); 
                this.InitializeBaseAdjustmentTypeComboBox();
                this.InitializeBaseAdjustmentComboBox();
                //this.InitializeInfluenceType1ComboBox();
                //this.InitializeInfluenceType2ComboBox();
                ////this.InitializeInfluenceType3ComboBox();
                this.CustomizeLandDetailsGridView();
                this.PopulateLandDetailsGridView();
                //this.InitializeBaseDollarValue(); 
                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    this.CalculateBaseDollerPerUnitTextBox();
                }
          
               this.ClearFormFields(true);
                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
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
            //if (this.flagReportAsLabelChanged)
            //{
            //    this.ReportAsTextBox.Text = decimal.Zero.ToString();
            //    this.ReportAsTextBox.ForeColor = Color.Black;
            //    this.ReportAsTextBox.LockKeyPress = false;
            //    this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
            //    this.ReportAsTextBox.TextAlign = HorizontalAlignment.Right;
            //}

            //this.OnShapeComboChangeSetBaseMarketValueFields();
            this.OnBaseAdjustmentTypeChangeSetBaseMarketValueFields();
            //this.OnProgramChangeSetUseValueFields();
         //   this.landDetailsDataSet.ListGridInfluences.Rows.Clear(); 
            this.filteredTable.Rows.Clear();
            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            this.InfluenceGridView.AllowSorting = false;
            this.InfluenceGridVerticalScroll.Visible = true;
            //this.InfluenceGridView.DataSource = this.landDetailsDataSet.ListGridInfluences.DefaultView;
            this.BaseDollerPerUnitTextBox.Text = "0.00";
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
               // this.InitializeBaseDollarValue();
                this.CalculateBaseDollerPerUnitTextBox();
                this.AlertValueSliceHeader();
            }
        }

        /// <summary>
        /// Handles the Cancel button click
        /// </summary>       
        private void CancelButtonClick()
        {
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            if (this.LandDetailsDataGridView.NumRowsVisible >= 14)
            {
                this.SetSmartPartHeight(true);
            }

            this.flagLoadOnProcess = true;
            
            this.PopulateLandDetailsGridView();
            this.LandType1Combo.Focus();
            this.flagLoadOnProcess = false;

            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                this.CalculateBaseDollerPerUnitTextBox();
            }

            this.LandDetailsGridViewPanel.Enabled = false;
            this.LandDetailsGridViewPanel.Enabled = true;
            this.LandDetailsDataGridView.Enabled = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                            this.form39135Control.WorkItem.F36035_DeleteLandDetails(this.landUniqueID, TerraScan.Common.TerraScanCommon.UserId);
                            this.landUniqueID = 0;
                            this.selectedRow = 0;
                            this.flagLoadOnProcess = true;
                            this.ClearFormFields(false);
                            if (this.LandDetailsDataGridView.NumRowsVisible > 14 && this.LandDetailsDataGridView.NumRowsVisible <= 14)
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
                            else if (this.LandDetailsDataGridView.NumRowsVisible > 14)
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
        private void F39135_Resize(object sender, EventArgs e)
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


        #endregion

        #region Influence Grid


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
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditEnabled_TextChanged(object sender, EventArgs e)
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

                    this.CalculateFinalMarketValueTextBox();

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
                    (sender as DataGridViewComboBoxEditingControl).SelectedValue = (int)this.InfluenceGridView.Rows[this.influencerowindex].Cells[this.InfluenceTypeID.Name].Value;//(int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    this.influenceTypeChanged = true;
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
                        F39135LandData.ListGridInfluencesRow    newRow = this.filteredTable.NewListGridInfluencesRow();
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

                        this.CalculateFinalMarketValueTextBox();

                        if (this.InfluenceGridView.Rows.Count <= this.InfluenceGridView.NumRowsVisible)
                        {
                            F39135LandData.ListGridInfluencesRow  newRow = this.filteredTable.NewListGridInfluencesRow();
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

        /// <summary>
        /// Calculates the final market value text box.
        /// </summary>
        private void CalculateFinalMarketValueTextBox()
        {
            decimal calculatedAdjustmentValue;
            string expression;
            expression = "Sum(" + this.landDetailsData.ListGridInfluences.InfluenceValueColumn.ColumnName + ")";

            // Sum of adjustment in influence grid
            decimal.TryParse(this.filteredTable.Compute(expression, "").ToString(), out calculatedAdjustmentValue);

            // Sum of adjustment in influence grid + Market Value
            decimal finalMarketValue = this.BaseMarketValueTextBox.DecimalTextBoxValue + calculatedAdjustmentValue;
            this.FinalMarketValueTextBox.Text = finalMarketValue.ToString("#,##,0");
        }


        /// <summary>
        /// Customizes the influence grid view.
        /// </summary>
        private void CustomizeInfluenceGridView()
        {
            this.InfluenceGridView.AutoGenerateColumns = false;
            this.InfluenceTypeID.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceTypeIDColumn.ColumnName;
            this.Influence.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceColumn.ColumnName;
            this.InfluenceDesc.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceDescColumn.ColumnName;
            this.InfluenceValue.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceValueColumn.ColumnName;
            this.InfluenceItemId.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceItemIDColumn.ColumnName;
            this.InfluenceType.DataPropertyName = this.landDetailsData.ListGridInfluences.InfluenceTypeColumn.ColumnName;
            this.LandCodeId.DataPropertyName = this.landDetailsData.ListGridInfluences.LUIDColumn.ColumnName;
            this.InfluenceGridView.PrimaryKeyColumnName = this.landDetailsData.ListGridInfluences.InfluenceItemIDColumn.ColumnName;
        }


        /// <summary>
        /// Loads the influence type combo.
        /// </summary>
        private void LoadInfluenceTypeCombo()
        {
            this.CustomizeInfluenceGridView();

            //// Fetch the landType combo data for valueSliceId

            //this.landDetailsData.ListInfluenceTypes.Rows.Clear();
            //this.landDetailsData.Merge (this.form39135Control.WorkItem.F36035_ListInfluenceType(this.formValueSliceId));
            this.landDetailsDataSet.ListInfluenceType.Rows.Clear();
            this.landDetailsDataSet.Merge(this.form39135Control.WorkItem.F36035_ListInfluenceType(this.formValueSliceId));

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

            if (this.landDetailsData.ListInfluenceTypes.Rows.Count > 0)
            {
                this.InfluenceGridView.Enabled = true;
            }
            else
            {
                this.InfluenceGridView.Enabled = false;
            }
            this.landDetailsData.ListGridInfluences.AcceptChanges();   
            //this.landDetailsDataSet.ListGridInfluences.AcceptChanges();
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
                DataRow[] influenceRows = this.landDetailsData.ListGridInfluences.Select(this.landDetailsData.ListGridInfluences.LUIDColumn.ColumnName + " <> " + this.landUniqueID);   
                //DataRow[] influenceRows = this.landDetailsDataSet.ListGridInfluences.Select(this.landDetailsDataSet.ListGridInfluences.LUIDColumn.ColumnName + " <> " + this.landUniqueID);
                originalSets.Merge(influenceRows);
                if (originalSets.Tables.Count > 0)
                {
                    updateData.Merge(originalSets.Tables[0]);
                }
                DataView changeSetsView = new DataView(updateData);
                changeSetsView.RowFilter = "EmptyRecord$ = false AND InfluenceTypeID <> 0";
                updateData = changeSetsView.ToTable();
                //changeSets.Load(((DataView)this.InfluenceGridView.DataSource).ToTable().CreateDataReader(), LoadOption.OverwriteChanges);
            }
            catch (Exception ex)
            {
            }

            string saveInfluenceDetails = TerraScanCommon.GetXmlString(updateData);
            return saveInfluenceDetails;
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

        #endregion Influence Grid View Events

        #region Common Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form39135Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form39135Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form39135Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            // set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form39135Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form39135Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            if (this.form39135Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form39135Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form39135Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
        }





        #endregion

        #region Land DetailsGridView



        /// <summary>
        /// Populates the land details grid view.
        /// </summary>
        private void PopulateLandDetailsGridView()
        {
            this.ClearFormFields(true);
            this.landDetailsData.GetLandValuesSliceDetails.Clear();   
            //this.landDetailsDataSet.GetCfgLandTypeLabel.Clear();
            //this.landDetailsDataSet.ListGridInfluences.Rows.Clear();
            this.landDetailsData.ListGridInfluences.Clear();    
            this.filteredTable.Rows.Clear();
            this.landDetailsData.Merge(this.form39135Control.WorkItem.F39135_LandDetails(this.formValueSliceId));
            this.landInfluenceDetails.Merge(this.landDetailsData.ListGridInfluences);    
          /*  if (this.landDetailsData.GetLandValuesSliceDetails.Rows.Count > 0)
            {
                for (int i = 0; i < this.landDetailsData.GetLandValuesSliceDetails.Rows.Count; i++)
                {
                    F39135LandData.GetLandValuesSliceDetailsRow currentRow = (F39135LandData.GetLandValuesSliceDetailsRow)this.landDetailsData.GetLandValuesSliceDetails.Rows[i];
                    if (currentRow != null && !currentRow.IsAdjustmentTypeNull())
                    {
                        if (currentRow.AdjustmentType.ToString().Equals("0"))
                        {
                            currentRow.BaseDollarPerUnit = currentRow.BaseValue;
                        }
                        else if (currentRow.AdjustmentType.ToString().Equals("1"))
                        {
                            currentRow.BaseDollarPerUnit = currentRow.BaseValue;
                        }
                        else if (currentRow.AdjustmentType.ToString().Equals("2"))
                        {
                            currentRow.BaseDollarPerUnit = currentRow.BaseValue;
                        }
                        else if (currentRow.AdjustmentType.ToString().Equals("3"))
                        {
                            if (!currentRow.IsAdjustmentNull())
                            {
                                decimal adjustment = 0;
                                decimal.TryParse(currentRow.Adjustment.ToString(), out adjustment);
                                currentRow.BaseDollarPerUnit = adjustment;
                            }
                        }
                        else if (currentRow.AdjustmentType.ToString().Equals("4"))
                        {
                            decimal adjustment = 0;
                            if (!currentRow.IsAdjustmentNull())
                            {
                                decimal.TryParse(currentRow.Adjustment.ToString(), out adjustment);


                                decimal multiplier = 0;
                                if (!currentRow.IsMrktMultiplierNull())
                                {
                                    decimal.TryParse(currentRow.MrktMultiplier.ToString(), out multiplier);
                                }
                                currentRow.BaseDollarPerUnit = multiplier * adjustment;
                            }
                            else if (currentRow.AdjustmentType.ToString().Equals("5"))
                            {
                                currentRow.BaseDollarPerUnit = currentRow.BaseValue;
                            }
                        }
                    }
                }
                this.landDetailsData.GetLandValuesSliceDetails.AcceptChanges();
            }*/

            this.LandDetailsDataGridView.DataSource = this.landDetailsData.GetLandValuesSliceDetails.DefaultView;

            ////Populate InflunceDetails
            this.LoadInfluenceTypeCombo();

            // Set the LandType lable values
            //  this.SetLandTypeLableValues(this.GetSelectedCfgLandTypesRow(0));

            //if (this.flagReportAsLabelChanged)
            //{
            //    this.ReportAsTextBox.ForeColor = Color.Black;
            //    this.ReportAsTextBox.LockKeyPress = false;
            //    this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
            //    this.ReportAsTextBox.TextAlign = HorizontalAlignment.Right;
            //}
            //else
            //{
            //    this.ReportAsTextBox.ForeColor = Color.Gray;
            //    this.ReportAsTextBox.LockKeyPress = true;
            //    this.ReportAsTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
            //    this.ReportAsTextBox.TextAlign = HorizontalAlignment.Left;
            //}

            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                this.EnableLandFormPanels(true);
                //modify to identify the smartpartheight
                this.SetSmartPartHeight(true);
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
            }
            else
            {
                this.LandDetailsDataGridView.RemoveDefaultSelection = true;
                this.SetSmartPartHeight(true);
                this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();
                // this.ChangeUseValueFieldsBehaviorOnChageOfProgramType();
                this.EnableLandFormPanels(false);
                this.NullRecords = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.CommentsdeckWorkspace.Enabled = false;
            }
        }


        #endregion

        #region Land Types Header Field Methods

        /// <summary>
        /// Initializes the land type combo boxes.
        /// </summary>
        private void InitializeLandTypeComboBoxes()
        {
            // Fetch the landType combo data for valueSliceId
           // this.landDetailsData.Merge(this.form39135Control.WorkItem.F39135_LandDetails(this.formValueSliceId));
            this.landDetailsData = this.form39135Control.WorkItem.F39135_LandDetails(this.formValueSliceId);
    int.TryParse(this.landDetailsData.GetRollYear.Rows[0][0].ToString(), out this.ParcelRollYear);
    this.landDetailsData = this.form39135Control.WorkItem.F39135_Landtypes(this.formValueSliceId, this.ParcelRollYear);
        if (this.landDetailsData.ListLandTypes1.Rows.Count > 0)
            {
                int.TryParse(this.landDetailsData.ListLandTypes1.Rows[0][this.landDetailsData.ListLandTypes1.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            }
            else if (this.landDetailsData.ListLandTypes2.Rows.Count > 0)
            {
                int.TryParse(this.landDetailsData.ListLandTypes2.Rows[0][this.landDetailsData.ListLandTypes2.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            }
            else if (this.landDetailsData.ListLandTypes3.Rows.Count > 0)
            {
                int.TryParse(this.landDetailsData.ListLandTypes3.Rows[0][this.landDetailsData.ListLandTypes3.RollYearColumn.ColumnName].ToString(), out this.formRollYear);
            }

            // Initialize the LandType1 ComboBox
            this.listLandType1ComboDataTable.Clear();

            // To assign a empty row in the combo box
            DataRow customLandType1Row = this.listLandType1ComboDataTable.NewRow();
            customLandType1Row[this.listLandType1ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
            customLandType1Row[this.listLandType1ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
            customLandType1Row[this.listLandType1ComboDataTable.RollYearColumn.ColumnName] ="0";
            customLandType1Row[this.listLandType1ComboDataTable.DescriptionColumn.ColumnName] = string.Empty;
            this.listLandType1ComboDataTable.Rows.Add(customLandType1Row);

            this.listLandType1ComboDataTable.Merge(this.landDetailsData.ListLandTypes1);

            if (this.listLandType1ComboDataTable.Rows.Count > 0)
            {
                this.LandType1Combo.DataSource = this.listLandType1ComboDataTable;
                this.LandType1Combo.MaxLength = this.landDetailsData.ListLandTypes1.LandTypeColumn.MaxLength;
                this.LandType1Combo.DisplayMember = this.landDetailsData.ListLandTypes1.LandTypeColumn.ColumnName;
                this.LandType1Combo.ValueMember = this.landDetailsData.ListLandTypes1.LandTypeIDColumn.ColumnName;
            }

            // Initialize the LandType2 ComboBox
            this.listLandType2ComboDataTable.Clear();

            ////To assign a empty row in the combo box
            DataRow customLandType2Row = this.listLandType2ComboDataTable.NewRow();
            customLandType2Row[this.listLandType2ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
            customLandType2Row[this.listLandType2ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
            customLandType2Row[this.listLandType2ComboDataTable.DescriptionColumn.ColumnName] = string.Empty;
            customLandType2Row[this.listLandType2ComboDataTable.RollYearColumn.ColumnName] = "0";
            this.listLandType2ComboDataTable.Rows.Add(customLandType2Row);

            this.listLandType2ComboDataTable.Merge(this.landDetailsData.ListLandTypes2);

            if (this.listLandType2ComboDataTable.Rows.Count > 0)
            {
                this.LandType2Combo.DataSource = this.listLandType2ComboDataTable;
                this.LandType2Combo.MaxLength = this.landDetailsData.ListLandTypes2.LandTypeColumn.MaxLength;
                this.LandType2Combo.DisplayMember = this.landDetailsData.ListLandTypes2.LandTypeColumn.ColumnName;
                this.LandType2Combo.ValueMember = this.landDetailsData.ListLandTypes2.LandTypeIDColumn.ColumnName;
            }

            // Initialize the LandType3 ComboBox
            this.listLandType3ComboDataTable.Clear();

            // To assign a empty row in the combo box
            DataRow customLandType3Row = this.listLandType3ComboDataTable.NewRow();
            customLandType3Row[this.listLandType3ComboDataTable.LandTypeIDColumn.ColumnName] = "0";
            customLandType3Row[this.listLandType3ComboDataTable.LandTypeColumn.ColumnName] = string.Empty;
            customLandType3Row[this.listLandType3ComboDataTable.DescriptionColumn.ColumnName] = string.Empty;
            customLandType3Row[this.listLandType3ComboDataTable.RollYearColumn.ColumnName] = "0";
            this.listLandType3ComboDataTable.Rows.Add(customLandType3Row);

            this.listLandType3ComboDataTable.Merge(this.landDetailsData.ListLandTypes3);

            if (this.listLandType3ComboDataTable.Rows.Count > 0)
            {
                this.LandType3Combo.DataSource = this.listLandType3ComboDataTable;
                this.LandType3Combo.MaxLength = this.landDetailsData.ListLandTypes3.LandTypeColumn.MaxLength;
                this.LandType3Combo.DisplayMember = this.landDetailsData.ListLandTypes3.LandTypeColumn.ColumnName;
                this.LandType3Combo.ValueMember = this.landDetailsData.ListLandTypes3.LandTypeIDColumn.ColumnName;
            }
        }


        ///<summary>
        /// Initialise the land use value
        /// </summary>
        private void InitialiseLandUse()
        {
            // Fetch the landType combo data for valueSliceId
            this.landDetailsData = this.form39135Control.WorkItem.F39135_LandUseTypes(this.formValueSliceId);
            
            this.listLandUseComboDataTable.Clear();
            DataRow landUseRow = this.listLandUseComboDataTable.NewRow();
            landUseRow[this.listLandUseComboDataTable.AglandIDColumn.ColumnName] = "0";
            landUseRow[this.listLandUseComboDataTable.UseColumn.ColumnName] = string.Empty;
            this.listLandUseComboDataTable.Rows.Add(landUseRow);
            this.listLandUseComboDataTable.Merge(this.landDetailsData.GetLandUseTypes_);
            if (this.listLandUseComboDataTable.Rows.Count > 0)
            {
                this.LandUseComboBox.DataSource = this.listLandUseComboDataTable;
                this.LandUseComboBox.MaxLength = this.landDetailsData.GetLandUseTypes_.UseColumn.MaxLength;
                this.LandUseComboBox.DisplayMember = this.landDetailsData.GetLandUseTypes_.UseColumn.ColumnName;
                this.LandUseComboBox.ValueMember = this.landDetailsData.GetLandUseTypes_.AglandIDColumn.ColumnName;     
            }

        }

        // <summary>
        /// Sets the land form field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandFormFieldValues(F39135LandData.GetLandValuesSliceDetailsRow selectedRow)
        {
            //// Fill the LandType header field values
            this.SetLandTypesHeaderFieldValues(selectedRow);

            //// Fill the BaseMarketValue field values
            this.SetBaseMarketValueFieldValues(selectedRow);

            //////// Fill the MarketValueInfluence field values
            //////this.SetMarketValueInfluenceFieldValues(selectedRow);

            //// Fill UseValue field values
            //this.SetUseValueFieldValues(selectedRow);

            //// Set the Attachment and Comment button count
            this.SetAttachmentAndCommentCount();

            //////// Fill the MarketValueInfluence Grid
            if (!selectedRow.IsLUIDNull())
            {
                this.SetMarketValueInfluenceFieldValues(selectedRow.LUID);
                this.CalculateFinalMarketValueTextBox();
            }

            //if (this.landUnitType.Equals("Front Foot") || this.landUnitType.Equals("FF")
            //  || this.landUnitType.Equals("Front Feet"))
            //{
            //    this.Frontagelabel.Visible = false;
            //    this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, true, false);
            //}
            //else
            //{
            //    this.Frontagelabel.Visible = true;
            //    this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, false, true);
            //}

        }

        /// <summary>
        /// Gets the selected land details row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The selected land details row.</returns>
        //private F36035LandData.ListLandValueSliceDetailsNewRow GetSelectedLandDetailsRow(int rowIndex)
        //{
        //    return (F36035LandData.ListLandValueSliceDetailsNewRow)this.landDetailsDataSet.ListLandValueSliceDetailsNew.DefaultView[rowIndex].Row;

        //}

        private F39135LandData.GetLandValuesSliceDetailsRow GetSelectedLandDetailsRow(int rowIndex)
        {
            return (F39135LandData.GetLandValuesSliceDetailsRow)this.landDetailsData.GetLandValuesSliceDetails.DefaultView[rowIndex].Row;
        }

        #endregion

        #region Base Market Value
        /// <summary>
        /// Called when [base adjustment type change set base market value fields].
        /// </summary>
        private void OnBaseAdjustmentTypeChangeSetBaseMarketValueFields()
        {
            if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {
                this.BaseAdjustmentTextBox.Text = string.Empty;
                this.BaseAdjusmentComboBox.SelectedIndex = 0;
                this.BaseAdjusmentComboBox.Text = string.Empty;
                //this.BaseDollerPerUnitTextBox.Text = string.Empty;
                //this.calculatedBaseDollerPerUnitValue = 0;

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

            if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0))
            {
                //if (string.IsNullOrEmpty(this.landValueCurveFormula.Trim()))
                //{
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
                
                }
                else
                {
                    adjDisabled = true;
                    disabled = true;
                    altColor = true;
                }
                if (string.IsNullOrEmpty(this.landValueCurveFormula.Trim()))
                {
                    formulaDisabled = false;
                }
                else
                {
                    formulaDisabled = true;
                }

                this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
            else
            if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
            {
                disabled = false;
                this.BaseAdjustmentTextBox.Visible = false;
                this.BaseAdjusmentComboBox.Visible = true;
                this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(2))
            {
                adjDisabled = false;
                this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.BaseAdjustmentTextBox.TextCustomFormat = "0.00%";
            }
            else
            {
                adjDisabled = false;
                disabled = false;
                formulaDisabled = false;
                this.LandValueCurveFormulaTextBox.Text = this.landValueCurveFormula.Trim();
                this.BaseAdjustmentTextBox.ApplyCFGFormat = false;
                this.BaseAdjustmentTextBox.AllowNegativeSign = false;
                this.BaseAdjustmentTextBox.ApplyNegativeStandard = false;
                this.BaseAdjustmentTextBox.TextCustomFormat = "#,##0.00";
            }
            if (string.IsNullOrEmpty(this.landValueCurveFormula.Trim()))
            {
                formulaDisabled = false;
            }
            else
            {
                formulaDisabled = true;
            }
            this.BaseDollerPerUnitLabel.Visible = true;
            this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, false, false);
            this.SetFieldsFillToLightGrayAndDisable(this.BaseAdjustmentPanel, this.BaseAdjustmentLabel, this.BaseAdjustmentTextBox, this.BaseAdjusmentComboBox, adjDisabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.BaseReasonForAdjustmentPanel, this.BaseReasonForAdjusmentLabel, this.BaseReasonForAdjustmentTextBox, null, adjDisabled, true);
                // Disable break fields for AdjType > 0
            this.SetBreakFieldsFillToLightGrayAndDisable(this.LandValueCurveFormulaPanel, this.LandValueCurveFormulalabel, this.LandValueCurveFormulaTextBox, !formulaDisabled, false);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.Break1panel, this.Break1label, this.Break1TextBox, !disabled, false);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer1panel, this.ValuePer1label, this.ValuePer1TextBox, !disabled, false);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.Break2panel, this.Break2label, this.Break2TextBox, !disabled, altColor);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer2panel, this.ValuePer2label, this.ValuePer2TextBox, !disabled, altColor);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.Break3panel, this.Break3label, this.Break3TextBox, !disabled, false);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer3panel, this.ValuePer3label, this.ValuePer3TextBox, !disabled, false);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.Break4panel, this.Break4label, this.Break4TextBox, !disabled, altColor);
                this.SetBreakFieldsFillToLightGrayAndDisable(this.ValuePer4panel, this.ValuePer4label, this.ValuePer4TextBox, !disabled, altColor);
            
            
        }

        #endregion Base Market Value
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
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            // Lock the LandType header editable fields
            this.LandType1Combo.Enabled = !controlLock;
            this.LandType2Combo.Enabled = !controlLock;
            this.LandType3Combo.Enabled = !controlLock;
            this.LandUseComboBox.Enabled = !controlLock;  
            // Lock the BaseMarketValue editable fields
            this.ShapeComboBox.Enabled = !controlLock;
            this.LotWidthTextBox.LockKeyPress = controlLock;
            this.LotDepthTextBox.LockKeyPress = controlLock;
           //this.FrontageTextBox.LockKeyPress = controlLock;
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
            this.LandCodePanel.Enabled = enable;
            this.LandUsepanel.Enabled = enable; 
            //this.ReportAspanel.Enabled = enable;
            this.UnitTypePanel.Enabled = enable;

            // Enable the BaseMarketValue Panels
            this.Shapepanel.Enabled = enable;
            this.LotWidthPanel.Enabled = enable;
            this.LotDepthPanel.Enabled = enable;
            this.WtRatingPanel.Enabled = enable;
            this.Acrespanel.Enabled = enable;
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
                //this.ReportAsTextBox.Text = string.Empty;
                this.LandUseComboBox.SelectedIndex  = 0;
                 
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

            this.LandCodeTextBox.Text = string.Empty;
           
            ////Added by Biju on 24-Sep-2009 to fix #4015 --the IF condition only
            if (!this.flagReportAsLabelChanged)
                //this.ReportAsTextBox.Text = string.Empty;
                //this.UnitTypeTextBox.Text = string.Empty;

                /// used to set value for the Units Type field.
                
              

            // Reset the BaseMarketValue field values
            this.ShapeComboBox.SelectedIndex = 0; ////ByDefault Rectangular

            this.LotWidthTextBox.Text = string.Empty;
            this.LotDepthTextBox.Text = string.Empty;
            //this.FrontageTextBox.Text = string.Empty;
            this.WtRatingTextBox.Text = string.Empty;   
            //this.UnitsLabel.Text = string.Empty;
            this.Acreslabel.Text = string.Empty; 
            //this.UnitsTextBox.Text = string.Empty;
            this.BaseDollerPerUnitTextBox.Text = string.Empty;
            //if (!this.isUnitTxtchange)
            //{
               
                this.UnitsTextBox.Text = string.Empty;

            //}
            //if (string.IsNullOrEmpty(this.Acreslabel.Text))
            //{
                this.UnitTypeTextBox.Text = string.Empty;
            //}
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

            //this.InfluenceDescription1TextBox.Text = string.Empty;
            ////this.InfluenceDescription2TextBox.Text = string.Empty;
            ////this.InfluenceDescription3TextBox.Text = string.Empty;

            ////this.InfluenceAdjustment1TextBox.Text = string.Empty;
            ////this.InfluenceAdjustment2TextBox.Text = string.Empty;
            ////this.InfluenceAdjustment3TextBox.Text = string.Empty;

            this.FinalMarketValueTextBox.Text = string.Empty;

            // Reset UseValue field values
            //this.ProgramComboBox.SelectedIndex = 0;
            //this.UseBaseDollarsPerUnitTextBox.Text = string.Empty;
            //this.UseAdjusmentTypeComboBox.SelectedIndex = 0;
            //this.UseAdjusmentComboBox.SelectedIndex = 0;
            //this.UseAdjustmentTextBox.Text = string.Empty;
            this.ReasonForUseAdjTextBox.Text = string.Empty;
            this.FinalUseValueTextBox.Text = string.Empty;
            //this.UseAdjusmentComboBox.Visible = false;
            //this.UseAdjustmentTextBox.Visible = true;
            ////Added by Biju on 15/Dec/2009 to fix #4691.2
           // this.Frontagelabel.Visible = true;
           //this.SetFieldsFillToLightGrayAndDisable(this.Frontagepanel, this.Frontagelabel, this.FrontageTextBox, null, false, false);
            ////till here
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
                    || this.ShapeComboBox.SelectedIndex<0
                    || string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim())
                    || this.CheckBaseAdjustmentRequiredField())
                    //|| (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()) && string.IsNullOrEmpty(this.BaseDollerPerUnitTextBox.Text))
                    //|| string.IsNullOrEmpty(this.BaseMarketValueTextBox.Text)
                    //|| string.IsNullOrEmpty(this.FinalMarketValueTextBox.Text)
                    //|| this.CheckBaseAdjustmentRequiredField()
                    //|| this.CheckInfluenceDetails())
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.LandType1Combo.Focus();
                    return false;
                }
                else
                {
                    F39135LandData saveLandDetailsDataSet = new F39135LandData();
                    F39135LandData.GetLandValuesSliceDetailsRow landDetailsDataRow = saveLandDetailsDataSet.GetLandValuesSliceDetails.NewGetLandValuesSliceDetailsRow();   
                    //F36035LandData saveLandDetailsDataSet = new F36035LandData();
                    //F36035LandData.ListLandValueSliceDetailsNewRow landDetailsDataRow = saveLandDetailsDataSet.ListLandValueSliceDetailsNew.NewListLandValueSliceDetailsNewRow();
                    landDetailsDataRow.LandTypeID1 = this.landTypeId1Value;
                    landDetailsDataRow.LandTypeID2 = this.landTypeId2Value;
                    landDetailsDataRow.LandTypeID3 = this.landTypeId3Value;
                    landDetailsDataRow.LandCode = this.LandCodeTextBox.Text.Trim();
                    //landDetailsDataRow.SrAcres = this.ReportAsTextBox.DecimalTextBoxValue;
                   //landDetailsDataRow.ReportAS = this.reportAsValue.Trim();

                    landDetailsDataRow.UnitType = this.UnitTypeTextBox.Text.Trim();

                    if (this.ShapeComboBox.SelectedValue != null)
                    {
                        landDetailsDataRow.LandShape = this.ShapeComboBox.Text.Trim();
                    }
                    else
                    {
                        landDetailsDataRow.LandShape = "Rectangular";
                    }

                    if (!string.IsNullOrEmpty(this.LotWidthTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.LotWidth = this.LotWidthTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LotDepthTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.LotDepth = this.LotDepthTextBox.DecimalTextBoxValue;
                    }

                    //if (!string.IsNullOrEmpty(this.FrontageTextBox.Text.Trim()))
                    //{
                    //    landDetailsDataRow.Frontage = this.FrontageTextBox.DecimalTextBoxValue;
                    //}

                    if (!string.IsNullOrEmpty(this.UnitsTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.Units = this.UnitsTextBox.DecimalTextBoxValue;
                    }

                    landDetailsDataRow.Break1 = this.Break1TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value1 = this.ValuePer1TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break2 = this.Break2TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value2 = this.ValuePer2TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break3 = this.Break3TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value3 = this.ValuePer3TextBox.DecimalTextBoxValue;

                    landDetailsDataRow.Break4 = this.Break4TextBox.DecimalTextBoxValue;
                    landDetailsDataRow.Value4 = this.ValuePer4TextBox.DecimalTextBoxValue;

                    if (this.BaseAdjusmentTypeComboBox.SelectedIndex >=0 )
                    {
                        if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
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
                        else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(2))
                        {
                            landDetailsDataRow.Adjustment = (this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100).ToString();
                        }
                       else
                        {
                            landDetailsDataRow.Adjustment = this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString();
                        }
                        landDetailsDataRow.AdjustmentType = int.Parse(this.BaseAdjusmentTypeComboBox.SelectedValue.ToString());
                        landDetailsDataRow.AdjTypeDescription = this.BaseAdjusmentTypeComboBox.Text.ToString();    
                    }
                    else
                    {
                        landDetailsDataRow.AdjustmentType = 0;
                    }
                    landDetailsDataRow.BaseDollarPerUnit = this.BaseDollerPerUnitTextBox.DecimalTextBoxValue;  
                    landDetailsDataRow.AdjDescription = this.BaseReasonForAdjustmentTextBox.Text.Trim();
                    landDetailsDataRow.BaseMrktValue = this.BaseMarketValueTextBox.DecimalTextBoxValue;
                    landDetailsDataRow.WeightedRating = this.WtRatingTextBox.DecimalTextBoxValue;
                    if (this.LandUseComboBox.SelectedIndex > 0)
                    {
                        landDetailsDataRow.AglandID = int.Parse(this.LandUseComboBox.SelectedValue.ToString());
                    }

                    //if (this.influenceTypeId1Value > 0)
                    //{
                    //    landDetailsDataRow.InfluenceTypeID1 = this.influenceTypeId1Value;
                    //    landDetailsDataRow.Influence1 = this.influence1Value;
                    //    landDetailsDataRow.InfluenceDesc1 = this.InfluenceDescription1TextBox.Text.Trim();
                    //    landDetailsDataRow.InfluenceValue1 = this.InfluenceAdjustment1TextBox.DecimalTextBoxValue;
                    //}

                    //if (this.influenceTypeId2Value > 0)
                    //{
                    //    landDetailsDataRow.InfluenceTypeID2 = this.influenceTypeId2Value;
                    //    landDetailsDataRow.Influence2 = this.influence2Value;
                    //    landDetailsDataRow.InfluenceDesc2 = this.InfluenceDescription2TextBox.Text.Trim();
                    //    landDetailsDataRow.InfluenceValue2 = this.InfluenceAdjustment2TextBox.DecimalTextBoxValue;
                    //}

                    //if (this.influenceTypeId3Value > 0)
                    //{
                    //    landDetailsDataRow.InfluenceTypeID3 = this.influenceTypeId3Value;
                    //    landDetailsDataRow.Influence3 = this.influence3Value;
                    //    landDetailsDataRow.InfluenceDesc3 = this.InfluenceDescription3TextBox.Text.Trim();
                    //    landDetailsDataRow.InfluenceValue3 = this.InfluenceAdjustment3TextBox.DecimalTextBoxValue;
                    //}

                    landDetailsDataRow.FinalMrktValue = this.FinalMarketValueTextBox.DecimalTextBoxValue;
                     
                    //if (this.ProgramComboBox.SelectedValue != null)
                    //{
                    //    landDetailsDataRow.ProgramID = (byte)this.ProgramComboBox.SelectedValue;
                    //}
                    //else
                    //{
                    //    landDetailsDataRow.ProgramID = 0;
                    //}

                    //if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    //{
                    //    landDetailsDataRow.UseAdjustmentType = (byte)this.UseAdjusmentTypeComboBox.SelectedValue;

                    //    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.AlternateLandCode))
                    //    {
                    //        if (this.UseAdjusmentComboBox.SelectedValue != null)
                    //        {
                    //            landDetailsDataRow.UseAdjustment = this.UseAdjusmentComboBox.SelectedValue.ToString();
                    //        }
                    //        else
                    //        {
                    //            landDetailsDataRow.UseAdjustment = string.Empty;
                    //        }
                    //    }
                    //    else if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Factor))
                    //    {
                    //        landDetailsDataRow.UseAdjustment = (this.UseAdjustmentTextBox.DecimalTextBoxValue / 100).ToString();
                    //    }
                    //    else
                    //    {
                    //        landDetailsDataRow.UseAdjustment = this.UseAdjustmentTextBox.DecimalTextBoxValue.ToString();
                    //    }
                    //}
                    //else
                    //{
                    //    landDetailsDataRow.UseAdjustmentType = 0;
                    //}

                    //landDetailsDataRow.UseBaseDollarPerUnit = this.UseBaseDollarsPerUnitTextBox.DecimalTextBoxValue;
                    //landDetailsDataRow.UseAdjDescription = this.ReasonForUseAdjTextBox.Text.Trim();
                    //landDetailsDataRow.FinalUseValue = this.FinalUseValueTextBox.DecimalTextBoxValue;

                    landDetailsDataRow.ValueSliceID = this.formValueSliceId;

                    if (!string.IsNullOrEmpty(this.BaseDollerPerUnitTextBox.Text.Trim()))
                    {
                        landDetailsDataRow.BaseDollarPerUnit = this.BaseDollerPerUnitTextBox.DecimalTextBoxValue;
                    }

                    saveLandDetailsDataSet.GetLandValuesSliceDetails.Rows.Add(landDetailsDataRow);
                    string saveLandDetailsXmlString = TerraScanCommon.GetXmlString(saveLandDetailsDataSet.GetLandValuesSliceDetails);
                    string saveInfluenceDetails = this.GetInfluenceDetails();
                    if (this.selectedRow == -1)
                    {
                        this.selectedRow = 0;
                    }

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.landUniqueID = 0;
                        this.selectedRow = 0;
                        if (this.LandDetailsDataGridView.NumRowsVisible > 14 && this.LandDetailsDataGridView.NumRowsVisible < 15)
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
                        else if (this.LandDetailsDataGridView.NumRowsVisible >= 14)
                        {
                            this.SetSmartPartHeight(true);
                        }
                    }
                    else if (this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName].Value != null && !string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName].Value.ToString()))
                    {
                        int.TryParse(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName].Value.ToString(), out this.landUniqueID);

                        if (this.LandDetailsDataGridView.NumRowsVisible >= 14)
                        {
                            this.SetSmartPartHeight(true);
                        }
                    }

                    //DataTable changeSets = this.landDetailsDataSet.ListGridInfluences.Copy();
                    //changeSets.DefaultView.RowFilter = "EmptyRecord$ = False";
                    //DataTable changesToSave = changeSets.DefaultView.ToTable();
                    //saveInfluenceDetails = TerraScanCommon.GetXmlString(changesToSave);
                    saveInfluenceDetails = this.GetInfluenceDetails();

                    this.landUniqueID = this.form39135Control.WorkItem.F39135_InsertLandDetails(this.landUniqueID, saveLandDetailsXmlString, saveInfluenceDetails, TerraScan.Common.TerraScanCommon.UserId);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = true;
                    this.PopulateLandDetailsGridView();
                    this.flagLoadOnProcess = false;

                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.LandDetailsGridViewPanel.Enabled = true;
                    this.LandDetailsDataGridView.Enabled = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="defaultHeight">if set to <c>true</c> [default height].</param>
        private void SetSmartPartHeight(bool defaultHeight)
        {
            if (this.LandDetailsDataGridView.OriginalRowCount > this.LandDetailsDataGridView.NumRowsVisible)
            {
                if (this.LandDetailsDataGridView.OriginalRowCount > 14)
                {
                    this.LandDetailsVscrollBar.Visible = false;
                }
                else
                {
                    this.LandDetailsVscrollBar.Visible = true;
                }

            }
            else
            {
                this.LandDetailsVscrollBar.Visible = true;
            }
            //if (!defaultHeight)
            //{
            //    if (this.LandDetailsDataGridView.OriginalRowCount > this.LandDetailsDataGridView.NumRowsVisible)
            //    {
            //        if (this.LandDetailsDataGridView.OriginalRowCount > 14)
            //        {
                        

            //            // Enable the gridview scrollbar
            //            int tempRowCount = 8;
            //            int tempRowHeigh = tempRowCount * 22;
            //            this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.OriginalRowCount;
            //            this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height + tempRowHeigh;
            //            this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height + tempRowHeigh;
            //            this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height + tempRowHeigh;
            //            this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height + tempRowHeigh;
            //            this.LandPictureBox.Height = this.LandPictureBox.Height + tempRowHeigh;
            //            this.FooterLeftpanel.Top = this.FooterLeftpanel.Top + tempRowHeigh;
            //            this.Footerpanel.Top = this.Footerpanel.Top + tempRowHeigh;
            //            this.Height = this.EntireLandFormPanel.Height - 110;

            //            // Resize the slice with new height
            //            SliceResize sliceResize;
            //            sliceResize.MasterFormNo = this.masterFormNo;
            //            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //            sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
            //            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            //            this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            //        }
            //        else
            //        {
            //            this.LandDetailsVscrollBar.Visible = true;
            //            int tempRowCount = this.LandDetailsDataGridView.OriginalRowCount - this.LandDetailsDataGridView.NumRowsVisible;
            //            int tempRowHeigh = tempRowCount * 22;
            //            this.LandDetailsDataGridView.NumRowsVisible = this.LandDetailsDataGridView.OriginalRowCount;
            //            this.LandDetailsDataGridView.Height = this.LandDetailsDataGridView.Height + tempRowHeigh;
            //            this.LandDetailsGridViewPanel.Height = this.LandDetailsGridViewPanel.Height + tempRowHeigh;
            //            this.LandDetailsVscrollBar.Height = this.LandDetailsVscrollBar.Height + tempRowHeigh;
            //            this.EntireLandFormPanel.Height = this.EntireLandFormPanel.Height + tempRowHeigh;
            //            this.LandPictureBox.Height = this.LandPictureBox.Height + tempRowHeigh;
            //            this.FooterLeftpanel.Top = this.FooterLeftpanel.Top + tempRowHeigh;
            //            this.Footerpanel.Top = this.Footerpanel.Top + tempRowHeigh;
            //            this.Height = this.EntireLandFormPanel.Height;

            //            // Resize the slice with new height
            //            SliceResize sliceResize;
            //            sliceResize.MasterFormNo = this.masterFormNo;
            //            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //            sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
            //            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            //            this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            //        }
            //    }
            //    else
            //    {
            //        this.LandDetailsVscrollBar.Visible = true;
            //    }
            //}
            //else
            //{
            //    this.LandDetailsVscrollBar.Visible = true;
            //    this.LandDetailsDataGridView.NumRowsVisible = 14;
            //    this.LandDetailsDataGridView.Height = 330;
            //    this.LandDetailsGridViewPanel.Height = 330;
            //    this.LandDetailsVscrollBar.Height = 330;
            //    this.EntireLandFormPanel.Height = 870; // 872;
            //    this.LandPictureBox.Height = 863; // 868;
            //    this.FooterLeftpanel.Top = 845; // 848;
            //    this.Footerpanel.Top = 845; // 848;
            //    this.Height = this.EntireLandFormPanel.Height;

            //    // Resize the slice with new height
            //    SliceResize sliceResize;
            //    sliceResize.MasterFormNo = this.masterFormNo;
            //    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //    sliceResize.SliceFormHeight = this.EntireLandFormPanel.Height + 2;
            //    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            //    this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            //}
        }

     
   
        #region Base Market Value Methods

        /// <summary>
        /// Initializes the shape combo box.
        /// </summary>
        private void InitializeShapeComboBox()
        {
            this.listShapesComboDataTable.Clear();

            // Adding shape types in FormLevel
            DataRow customShapeRow;

            customShapeRow = this.listShapesComboDataTable.NewRow();
            customShapeRow[this.listShapesComboDataTable.ShapeIDColumn.ColumnName] = 0;
            customShapeRow[this.listShapesComboDataTable.ShapeDescriptionColumn.ColumnName] = "Rectangular";
            this.listShapesComboDataTable.Rows.Add(customShapeRow);

            customShapeRow = this.listShapesComboDataTable.NewRow();
            customShapeRow[this.listShapesComboDataTable.ShapeIDColumn.ColumnName] = 1;
            customShapeRow[this.listShapesComboDataTable.ShapeDescriptionColumn.ColumnName] = "Irregular";
            this.listShapesComboDataTable.Rows.Add(customShapeRow);

            this.ShapeComboBox.DataSource = this.listShapesComboDataTable;
            this.ShapeComboBox.DisplayMember = this.listShapesComboDataTable.ShapeDescriptionColumn.ColumnName;
            this.ShapeComboBox.ValueMember = this.listShapesComboDataTable.ShapeIDColumn.ColumnName;
        }

        /// <summary>
        /// Initializes the base adjustment type combo box.
        /// </summary>
        private void InitializeBaseAdjustmentTypeComboBox()
        {
            this.landDetailsData = this.form39135Control.WorkItem.F39135_adjustmentTypes();
            this.BaseAdjusmentTypeComboBox.DataSource = this.landDetailsData.ListAdjustmentType;
            this.BaseAdjusmentTypeComboBox.DisplayMember = this.landDetailsData.ListAdjustmentType.AdjustmentTypeColumn.ColumnName;
            this.BaseAdjusmentTypeComboBox.ValueMember = this.landDetailsData.ListAdjustmentType.AdjustmentTypeIDColumn.ColumnName;     
        }

          /// <summary>
        /// Initializes the base adjustment combo box.
        /// </summary>
        private void InitializeBaseDollarValue()
        {
            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0) || this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(2)
                    || this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(5))
                {
                    if (this.LandUseComboBox.SelectedIndex > 0)
                    {
                        int isAgland;
                        int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAgland);
                        this.AglandID = isAgland;
                    }
                    else
                    {
                        this.AglandID = null;
                    }
                    this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCode(this.ParcelRollYear, this.landTypeId1Value, this.landTypeId2Value, this.landTypeId3Value, this.formValueSliceId, this.AglandID);
                    if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
                    {
                        this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCode.Rows[0]["BaseValue"].ToString();     
                    }
                }
                else
               if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
                {
                   // string landcode= this.LandCodeTextBox.Text;
                    string filterCond = "LandCode = '" + this.BaseAdjusmentComboBox.SelectedValue.ToString() + "'";
                    DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);

                    if (choiceRows.Length > 0)
                    {
                        if (this.LandUseComboBox.SelectedIndex > 0)
                        {
                            int isAgland;
                            int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAgland);
                            this.AglandID =isAgland;
                        }
                        else
                        {
                            this.AglandID = null;
                        }
                        this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listBaseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, this.AglandID);
                        if (this.landDetailsDataSet.Get_LandCodeBaseValue.Rows.Count > 0)
                        {
                            this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString();
                        }
                    }
                }
               else
                   if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(3))
                   {
                       this.BaseDollerPerUnitTextBox.Text = this.BaseAdjustmentTextBox.Text;  
                   }
                   else
                       if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(4))
                       {
                           decimal  adjustment;
                           decimal mrktmultiplier;
                           //int baseDollar;
                           if (this.LandUseComboBox.SelectedIndex > 0)
                           {
                               int isAgland;
                               int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAgland);
                               this.AglandID = isAgland;
                           }
                           else
                           {
                               this.AglandID = null;
                           }
                           this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCode(this.ParcelRollYear, this.landTypeId1Value, this.landTypeId2Value, this.landTypeId3Value, this.formValueSliceId, this.AglandID);
                           if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
                           {
                                decimal.TryParse(this.landDetailsDataSet.Get_LandCode.Rows[0]["MrktMultiplier"].ToString(), out mrktmultiplier);
                                decimal.TryParse(this.landDetailsDataSet.Get_LandCode.Rows[0]["UseAdjustment"].ToString(), out adjustment);
                                this.BaseDollerPerUnitTextBox.Text = (adjustment * mrktmultiplier).ToString();  

                               //this.BaseAdjustmentTextBox.Text = mrktmultiplier * adjustment;  
                           
                           }


                       }


            }

        }




        /// <summary>
        /// Initializes the base adjustment combo box.
        /// </summary>
        private void InitializeBaseAdjustmentComboBox()
        {
            this.landDetailsData.Merge(this.Form39135Control.WorkItem.F36035_ListLandTypeDetails(this.formValueSliceId));
            // Initialize the BaseAdjustmentComboBox
            this.listBaseAdjustmentComboDataTable.Clear();
            
            DataRow baseAdjustmentRow;
            baseAdjustmentRow = this.listBaseAdjustmentComboDataTable.NewRow();

            baseAdjustmentRow[this.listBaseAdjustmentComboDataTable.LandCodeColumn.ColumnName] = string.Empty;
            baseAdjustmentRow[this.listBaseAdjustmentComboDataTable.RollYearColumn.ColumnName] = this.formRollYear;
            this.listBaseAdjustmentComboDataTable.Rows.Add(baseAdjustmentRow);

            this.listBaseAdjustmentComboDataTable.Merge(this.landDetailsData.ListLandCode);
            this.BaseAdjusmentComboBox.MaxLength = this.listBaseAdjustmentComboDataTable.LandCodeColumn.MaxLength;
            this.BaseAdjusmentComboBox.DisplayMember = this.listBaseAdjustmentComboDataTable.LandCodeColumn.ColumnName;
            this.BaseAdjusmentComboBox.ValueMember = this.listBaseAdjustmentComboDataTable.LandCodeColumn.ColumnName;
            this.BaseAdjusmentComboBox.DataSource = this.listBaseAdjustmentComboDataTable;
        }

        #endregion

        /// <summary>
        /// Customizes the land details grid view.
        /// </summary>
        private void CustomizeLandDetailsGridView()
        {
            this.LandDetailsDataGridView.AutoGenerateColumns = false;
            this.LandDetailsDataGridView.PrimaryKeyColumnName = this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName;

            this.LUID.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName;
            this.ValueSliceID.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.ValueSliceIDColumn.ColumnName;
            this.RollYear.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.RollYearColumn.ColumnName;

            this.LandTypeID1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandTypeID1Column.ColumnName;
            this.LandType1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandType1Column.ColumnName;
            this.LandTypeID2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandTypeID2Column.ColumnName;
            this.LandType2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandType2Column.ColumnName;
            this.LandTypeID3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandTypeID3Column.ColumnName;
            this.LandType3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandType3Column.ColumnName;

            this.LandCode.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandCodeColumn.ColumnName;
            this.BaseValue.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.BaseValueColumn.ColumnName;

            this.Break1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Break1Column.ColumnName;
            this.Value1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Value1Column.ColumnName;
            this.Break2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Break2Column.ColumnName;
            this.Value2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Value2Column.ColumnName;
            this.Break3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Break3Column.ColumnName;
            this.Value4.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Value3Column.ColumnName;
            this.Break4.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Break4Column.ColumnName;
            this.Value4.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Value4Column.ColumnName;

            this.AdjustmentType.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.AdjustmentTypeColumn.ColumnName;
            this.AdjTypeDescription.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.AdjTypeDescriptionColumn.ColumnName;

            this.Adjustment.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.AdjustmentColumn.ColumnName;
            this.AdjDescription.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.AdjDescriptionColumn.ColumnName;

            this.UnitType.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.UnitTypeColumn.ColumnName;
            this.Units.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.UnitsColumn.ColumnName;
            //this.FinalMrktValue.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.FinalMrktValueColumn.ColumnName;
            this.BaseDollarPerUnit.Name = this.landDetailsData.GetLandValuesSliceDetails.BaseDollarPerUnitColumn.ColumnName;   
            this.LotWidth.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LotWidthColumn.ColumnName;
            this.LotDepth.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LotDepthColumn.ColumnName;
            this.LandShape.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandShapeColumn.ColumnName;
            //this.BaseMrktValue.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.BaseMrktValueColumn.ColumnName;
            
            this.InfluenceTypeID1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceTypeID1Column.ColumnName;
            this.InfluenceType1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceType1Column.ColumnName;
            this.Influence1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Influence1Column.ColumnName;
            this.InfluenceDesc1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceDesc1Column.ColumnName;
            //this.InfluenceValue1.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceValue1Column.ColumnName;

            this.InfluenceTypeID2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceTypeID2Column.ColumnName;
            this.InfluenceType2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceType2Column.ColumnName;
            this.Influence2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Influence2Column.ColumnName;
            this.InfluenceDesc2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceDesc2Column.ColumnName;
            //this.InfluenceValue2.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceValue2Column.ColumnName;

            this.InfluenceTypeID3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceTypeID3Column.ColumnName;
            this.InfluenceType3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceType3Column.ColumnName;
            this.Influence3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.Influence3Column.ColumnName;
            this.InfluenceDesc3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceDesc3Column.ColumnName;
            //this.InfluenceValue3.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.InfluenceValue3Column.ColumnName;
            this.ProgramAbv.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.LandUseColumn.ColumnName;
            this.weightedRating.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.WeightedRatingColumn.ColumnName;    
            this.VFormula.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.VFormulaColumn.ColumnName;
            this.BaseDollarPerUnit.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.BaseDollarPerUnitColumn.ColumnName;
            this.FinalValue.DataPropertyName = this.landDetailsData.GetLandValuesSliceDetails.GridFinalValueColumn.ColumnName;
        }

        /// <summary>
        /// Gets the index of the selected land code row.
        /// </summary>
        /// <param name="landCodeValue">The land code value.</param>
        /// <returns>Index of the specified landCodeValue in the dataset</returns>
        private int GetSelectedLandCodeRowIndex(int landCodeValue)
        {
            int tempIndex = this.landDetailsData.GetLandValuesSliceDetails.Rows.Count;
            DataTable tempDataTable = this.landDetailsData.GetLandValuesSliceDetails.Copy();
            tempDataTable.DefaultView.RowFilter = this.landDetailsData.GetLandValuesSliceDetails.LUIDColumn.ColumnName + " = " + landCodeValue.ToString();

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row);
            }

            return tempIndex;
        }

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


                    if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(3))
                    {
                        this.BaseDollerPerUnitTextBox.Text = this.BaseAdjustmentTextBox.Text;   
                    }
                   // // calculates the base doller per unit textbox
                   this.CalculateBaseDollerPerUnitTextBox();

                    // check for the base doller per unit max value
                    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseDollerPerUnitTextBox, true, true);

                    // calculates the base market value textbox
                    this.CalculateBaseMarketValueTextBox();

                    // check for the base market value max limit
                    this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseMarketValueTextBox, true, true);

                    ////// calculate the influence adjustment textbox fields
                    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    //this.SetInfluenceAdjustmentValue();
                    this.CalculateFinalMarketValueTextBox();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                            string filterCond = "LandTypeID = " + this.LandType1Combo.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType1ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId1Value);
                            }
                        }
                    }

                    
                    this.CalculateBaseDollerPerUnitTextBox();
                    this.GetLandCode();
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandType1Combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId1Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType1Combo.Text.Trim()))
                    {
                        string filterCond = "LandType = '" + this.LandType1Combo.Text.Trim().Replace("'", "''") + "'";
                        DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.listLandType1ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId1Value);
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
                            string filterCond = "LandType = '" + this.LandType1Combo.Text.Trim().Replace("'", "''") + "'";
                            DataRow[] choiceRows = this.listLandType1ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType1ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId1Value);
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

                        this.CalculateBaseDollerPerUnitTextBox();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }
        /// <summary>
        /// Gets the land code.
        /// </summary>
        private void GetLandCode()
        {
            /// used to remove base value and weighted RatingSP Call
            this.isAllowSp = false; 
            // reset the form fileds
            this.ClearFormFields(false);
            this.isAllowSp = true;
            this.landDetailsData.Get_LandCode.Clear();
          //  this.landDetailsDataSet.Get_LandCode.Clear();
            if (this.LandUseComboBox.SelectedIndex > 0)
            {
                int isAglangID;
                int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglangID);
                this.AglandID  = isAglangID;

            }
            else
            {
                this.AglandID = null;
            }
            this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCode(this.formRollYear, this.landTypeId1Value, this.landTypeId2Value, this.landTypeId3Value, this.formValueSliceId, this.AglandID);

            if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
            {
                this.SetLandCodeAndBreakValues(this.GetSelectedLandCodeRow(0));
            }

            this.landUnitType = this.UnitTypeTextBox.Text.Trim();
            this.landValueCurveFormula = this.LandValueCurveFormulaTextBox.Text.Trim();

           // change the base marke value fields based on the base adjustment type
            this.ChangeBaseMarketValueFieldsBehaviorOnChangeOfBaseAdjustmentType();

            // calculate the base units textbox
            //this.CalculateBaseUnitsTextBox();
            //filteredTable.Rows.Clear();
            filteredTable.Rows.Clear();
            this.InfluenceGridView.DataSource = filteredTable.DefaultView;
            //this.InfluenceGridView.DataSource = this.landDetailsDataSet.ListGridInfluences.DefaultView;

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
            this.CalculateFinalMarketValueTextBox();

            // check for the final use value max limit
            this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.FinalUseValueTextBox, true, true);
        }
        /// <summary>
        /// Gets the selected land code row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>The selected landCode row.</returns>
        //private F39135LandData.Get_LandCodeRow GetSelectedLandCodeRow(int rowIndex)
        //{
        //    return (F39135LandData.Get_LandCodeRow)this.landDetailsData .Get_LandCode.Rows[rowIndex];
        //}
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
                this.Acreslabel.Text = selectedRow.UnitType + ":";
            }
            else
            {
                this.UnitTypeTextBox.Text = string.Empty;
                this.Acreslabel.Text = string.Empty;
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

        ///// <summary>
        ///// Calculates for base units text box.
        ///// </summary>
        //private void CalculateBaseUnitsTextBox()
        //{
        //    if (this.ShapeComboBox.SelectedValue != null
        //        && this.ShapeComboBox.SelectedValue.Equals("Rectangular")
        //        && (this.landUnitType.Equals("Square Foot") || this.landUnitType.Equals("SF")
        //        || this.landUnitType.Equals("Sq Ft") || this.landUnitType.Equals("SqFt")
        //        || this.landUnitType.Equals("Square Feet"))
        //        && !string.IsNullOrEmpty(this.LotWidthTextBox.Text.Trim())
        //        && !string.IsNullOrEmpty(this.LotDepthTextBox.Text.Trim()))
        //    {
        //        decimal calculatedUnitsValue;
        //        calculatedUnitsValue = this.LotWidthTextBox.DecimalTextBoxValue * this.LotDepthTextBox.DecimalTextBoxValue;
        //        this.UnitsTextBox.Text = calculatedUnitsValue.ToString();
        //    }
        //}

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
                            string filterCond = "LandTypeID = " + this.LandType2Combo.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType2ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId2Value);
                            }
                        }
                    }
                    this.CalculateBaseDollerPerUnitTextBox();
                    this.GetLandCode();
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                            string filterCond = "LandType = '" + this.LandType2Combo.Text.Trim().Replace("'", "''") + "'";
                            DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType2ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId2Value);
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

                        this.CalculateBaseDollerPerUnitTextBox();

                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandType2Combo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId2Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType2Combo.Text.Trim()))
                    {
                        string filterCond = "LandType = '" + this.LandType2Combo.Text.Trim().Replace("'", "''") + "'";
                        DataRow[] choiceRows = this.listLandType2ComboDataTable.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.listLandType2ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId2Value);
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

        private void LandType3Combo_TextChanged(object sender, EventArgs e)
        {
             try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.landTypeId3Value = 0;

                    if (!string.IsNullOrEmpty(this.LandType3Combo.Text.Trim()))
                    {
                        string filterCond = "LandType = '" + this.LandType3Combo.Text.Trim().Replace("'", "''") + "'";
                        DataRow[] choiceRows = this.listLandType3ComboDataTable.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            int.TryParse(choiceRows[0][this.listLandType3ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId3Value);
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
                            string filterCond = "LandTypeID = " + this.LandType3Combo.SelectedValue.ToString();
                            DataRow[] choiceRows = this.listLandType3ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType3ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId3Value);
                            }
                        }
                    }

                    this.CalculateBaseDollerPerUnitTextBox();
                    this.GetLandCode(); 
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                            string filterCond = "LandType = '" + this.LandType3Combo.Text.Trim().Replace("'", "''") + "'";
                            DataRow[] choiceRows = this.listLandType3ComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                int.TryParse(choiceRows[0][this.listLandType3ComboDataTable.LandTypeIDColumn].ToString(), out this.landTypeId3Value);
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

                        this.CalculateBaseDollerPerUnitTextBox();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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

        private void LandUseComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.LandUseComboBox.SelectedIndex > 0  )
                    {
                        int isAglandID;
                        int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                        this.AglandID = isAglandID;

                    }
                    this.CalculateBaseDollerPerUnitTextBox();
                    /// used to Change in the Land USE GET LAND CODE.
                    this.GetLandCode(); 
                    this.CalculateBaseMarketValueTextBox();
                    this.EditEnabled(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Sets the land types header field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetLandTypesHeaderFieldValues(F39135LandData.GetLandValuesSliceDetailsRow selectedRow)
        {
            if (!selectedRow.IsRollYearNull())
            {
                this.formRollYear = selectedRow.RollYear;
            }

            if (!selectedRow.IsLandTypeID1Null())
            {
                this.LandType1Combo.SelectedValue = selectedRow.LandTypeID1;
                this.landTypeId1Value = selectedRow.LandTypeID1;
            }
            else
            {
                this.LandType1Combo.SelectedIndex = 0;
                this.landTypeId1Value = 0;
            }

            if (!selectedRow.IsLandTypeID2Null())
            {
                this.LandType2Combo.SelectedValue = selectedRow.LandTypeID2;
                this.landTypeId2Value = selectedRow.LandTypeID2;
            }
            else
            {
                this.LandType2Combo.SelectedIndex = 0;
                this.landTypeId2Value = 0;
            }

            if (!selectedRow.IsLandTypeID3Null())
            {
                this.LandType3Combo.SelectedValue = selectedRow.LandTypeID3;
                this.landTypeId3Value = selectedRow.LandTypeID3;
            }
            else
            {
                this.LandType3Combo.SelectedIndex = 0;
                this.landTypeId3Value = 0;
            }

            if (!selectedRow.IsLandCodeNull())
            {
                this.LandCodeTextBox.Text = selectedRow.LandCode;
            }
            else
            {
                this.LandCodeTextBox.Text = string.Empty;
            }


            if (!selectedRow.IsAglandIDNull())
            {
                this.LandUseComboBox.SelectedValue = selectedRow.AglandID;
            }
            else
            {
                this.LandUseComboBox.SelectedIndex = 0;
            }

            if (!selectedRow.IsUnitTypeNull())
            {
                this.UnitTypeTextBox.Text = selectedRow.UnitType;
                this.Acreslabel.Text = selectedRow.UnitType + ":";
                this.landUnitType = selectedRow.UnitType;
            }
            else
            {
                this.UnitTypeTextBox.Text = string.Empty;
                this.Acreslabel.Text = string.Empty;
                this.landUnitType = string.Empty;
            }

          
        }
        /// <summary>
        /// Sets the base market value field values.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetBaseMarketValueFieldValues(F39135LandData.GetLandValuesSliceDetailsRow  selectedRow)
        {
            if (!selectedRow.IsLandShapeNull() && selectedRow.LandShape.Equals("Rectangular"))
            {
                this.ShapeComboBox.SelectedValue = 0;
            }
            else if (!selectedRow.IsLandShapeNull() && selectedRow.LandShape.Equals("Irregular"))
            {
                this.ShapeComboBox.SelectedValue = 1;
            }

            // change the width and depth fields behavior on chage of shape type
            //this.ChangeWidthAndDepthFieldsBehaviorOnChangeOfShapeType();

            if (this.ShapeComboBox.SelectedValue != null) // && this.ShapeComboBox.SelectedValue.Equals((short)ShapeTypes.Rectangular))
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

            if (!selectedRow.IsWeightedRatingNull())
            {
                this.WtRatingTextBox.Text   = selectedRow.WeightedRating.ToString() ;
            }
            else
            {
                this.WtRatingTextBox.Text = "0.0000";
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

            if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0))
            {
                this.BaseAdjustmentTextBox.Text = string.Empty;
            }
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals(1))
            {
                if (!selectedRow.IsAdjustmentNull())
                {
                    this.BaseAdjusmentComboBox.SelectedValue = selectedRow.Adjustment;

                    this.getLandCodeBaseValueDataTable.Clear();
                    this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();

                    if (this.LandUseComboBox.SelectedIndex > 0)
                    {
                        int isAglandID;
                        int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                        this.AglandID = isAglandID;
                    }
                    else
                    {
                        this.AglandID = null;
                    }
                    this.landDetailsDataSet.Merge(this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(selectedRow.Adjustment, this.formValueSliceId, this.AglandID));
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
            else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals(2))
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

            //if (string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()))
            //{
            //    //this.SetBreakFieldsFillToLightGrayAndDisable(this.LandValueCurveFormulaPanel, this.LandValueCurveFormulalabel, this.LandValueCurveFormulaTextBox, true, false);
            //    this.BaseDollerPerUnitLabel.Visible = true;
            //    //this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, false, false);
            //    this.BaseDollerPerUnitTextBox.ForeColor = Color.Gray;

            //    // calculates the Base$/Unit textbox form level only
            //    this.CalculateBaseDollerPerUnitTextBox();
            //}
            //else
            //{
            //    this.BaseDollerPerUnitLabel.Visible = false;
            //    //this.SetFieldsFillToLightGrayAndDisable(this.BaseDollerPerUnitPanel, this.BaseDollerPerUnitLabel, this.BaseDollerPerUnitTextBox, null, true, false);
            //}
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

        /// <summary>
        /// Calculates the base doller per unit text box.
        /// </summary>
        private void CalculateBaseDollerPerUnitTextBox()
        {
            //if (this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            //{
            //    if( (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0))||(this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(2))||(this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(5)))
            //    {
            //        string landcode = this.LandCodeTextBox.Text;
            //        this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(landcode, this.formValueSliceId, this.AglandID);
            //        if (this.landDetailsDataSet.Get_LandCodeBaseValue.Rows.Count > 0)
            //        {
            //            this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString();
            //        }
            //        //this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue;
            //    }
            //    else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
            //    {
            //        string landcode = this.LandCodeTextBox.Text;
            //        this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(landcode, this.formValueSliceId, this.AglandID);
            //        if (this.landDetailsDataSet.Get_LandCodeBaseValue.Rows.Count > 0)
            //        {
            //            this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString();
            //        }
            //    }
            //    else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(3))
            //    {
            //        this.BaseDollerPerUnitTextBox.Text = this.BaseAdjustmentTextBox.Text;
            //    }
            //    else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(4))
            //    {
            //        string landcode = this.LandCodeTextBox.Text;
            //        this.landDetailsDataSet = this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(landcode, this.formValueSliceId, this.AglandID);
            //        if (this.landDetailsDataSet.Get_LandCodeBaseValue.Rows.Count > 0)
            //        {
            //            this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString();
            //        }
            //    }
            //           // this.BaseDollerPerUnitTextBox.Text = this.calculatedBaseDollerPerUnitValue.ToString();


            //}
            //else
            //{
            //    this.BaseDollerPerUnitTextBox.Text = string.Empty;
            //}


            if ( this.BaseAdjusmentTypeComboBox.SelectedValue != null)
            {
                if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
                {
                    this.calculatedBaseDollerPerUnitValue = this.alternateLandCodeBaseValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(2))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue; // *(this.BaseAdjustmentTextBox.DecimalTextBoxValue / 100);
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(3))
                {
                    this.calculatedBaseDollerPerUnitValue = this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(4))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeMarketMultiplierValue * this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                else if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(5))
                {
                    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue; // *this.BaseAdjustmentTextBox.DecimalTextBoxValue;
                }
                ////Added by Biju on 01-Dec-2010 to implement #9328
                //else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.TotalValue))
                //{
                //    this.calculatedBaseDollerPerUnitValue = this.landCodeBaseValue;
                //}

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
            string landcode;
            if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
            {
                landcode = this.LandCodeTextBox.Text;
            }
            else
            {
                landcode = string.Empty;  
            }
            int adjustmentTypeID=0;
            if (this.BaseAdjusmentTypeComboBox.SelectedIndex >= 0)
            {
                adjustmentTypeID = this.BaseAdjusmentTypeComboBox.SelectedIndex;
            }
            else
            {
                adjustmentTypeID = 0;
            }
            decimal adjustment = 0;
                //if (this.BaseAdjusmentComboBox.Visible)
                //{
                //    int.TryParse(this.BaseAdjusmentComboBox.SelectedValue.ToString() , out adjustment);    
                //}
                //else
                //{
            if (!this.BaseAdjusmentComboBox.Visible)
            {
                decimal.TryParse(this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out adjustment);
            }
                //}

                decimal basecostUnit = 0;
            //Math.Round(this.BaseDollerPerUnitTextBox.DecimalTextBoxValue,
            decimal.TryParse(this.BaseDollerPerUnitTextBox.DecimalTextBoxValue.ToString(), out basecostUnit);
            decimal unit = 0;
            if (this.LandUseComboBox.SelectedIndex > 0)
            {
                int isAglandID;
                int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                this.AglandID = isAglandID;
            }
            else
            {
                this.AglandID = null;
            }
            decimal.TryParse(this.UnitsTextBox.DecimalTextBoxValue.ToString(), out unit);
            this.landDetailsData = this.form39135Control.WorkItem.F39135_CalculatedBaseValue(landcode, adjustmentTypeID, unit, basecostUnit, adjustment, this.AglandID, this.formValueSliceId);
            if (this.landDetailsData.GetCalculateBaseValue.Rows.Count > 0)
            {
                this.BaseMarketValueTextBox.Text = this.landDetailsData.GetCalculateBaseValue.Rows[0][0].ToString();
            }
            this.SetInfluenceAdjustmentValue(); 
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
                additionalOperationCountEntity.AttachmentCount = this.form39135Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form39135Control.WorkItem.GetCommentsCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        /// <summary>
        /// Sets the market value influence field values.
        /// </summary>
        /// <param name="landCodeId">The land code id.</param>
        private void SetMarketValueInfluenceFieldValues(int landCodeId)
        {
            DataTable filterData = new DataTable();
            filterData = this.landDetailsData.ListGridInfluences.Copy();
            filterData.DefaultView.RowFilter = this.landDetailsData.ListGridInfluences.LUIDColumn.ColumnName + " = '" + landCodeId + "'";
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

            this.SetInfluenceScrollBarVisibility();

            this.InfluenceGridView.AllowSorting = true;
        }
        /// <summary>
        /// Sets the influence scroll bar visibility.
        /// </summary>
        private void SetInfluenceScrollBarVisibility()
        {
            if (this.InfluenceGridView.OriginalRowCount > 0
                && this.InfluenceGridView.OriginalRowCount >= this.InfluenceGridView.NumRowsVisible)
            {
                F39135LandData.ListGridInfluencesRow newRow = this.filteredTable.NewListGridInfluencesRow();
                newRow["EmptyRecord$"] = "True";
                this.filteredTable.AddListGridInfluencesRow(newRow);
                this.InfluenceGridVerticalScroll.Visible = false;
            }
            else
            {
                this.InfluenceGridVerticalScroll.Visible = true;
            }
        }

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

        private void LandDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && !this.flagLoadOnProcess && !this.NullRecords)
                {
                    this.selectedRow = e.RowIndex;
                    this.flagLoadOnProcess = true;
                    if (this.landDetailsData.GetLandValuesSliceDetails.Rows.Count <= 0)
                    {
                        DataView landView = (DataView)this.LandDetailsDataGridView.DataSource;
                        if (landView.Count > 0)
                        {
                            this.landDetailsData.GetLandValuesSliceDetails.Merge((F39135LandData.GetLandValuesSliceDetailsDataTable)landView.Table);
                        }
                    }

                    if (this.landDetailsData.ListGridInfluences.Rows.Count <= 0)
                    {
                        
                        if (this.landInfluenceDetails.Rows.Count > 0)
                        {
                            this.landDetailsData.ListGridInfluences.Merge(this.landInfluenceDetails);
                        }
                    }

                    if (this.landDetailsData.GetLandValuesSliceDetails.Rows.Count > 0)
                    {
                        this.SetLandFormFieldValues(this.GetSelectedLandDetailsRow(e.RowIndex));
                        this.CalculateBaseDollerPerUnitTextBox();
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

                    if (this.landDetailsData.GetLandValuesSliceDetails.Rows.Count <= 0)
                    {
                        DataView landView = (DataView)this.LandDetailsDataGridView.DataSource;
                        if (landView.Count > 0)
                        {
                            this.landDetailsData.GetLandValuesSliceDetails.Merge((F39135LandData.GetLandValuesSliceDetailsDataTable)landView.Table);
                        }
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
        /// Handles the DataBindingComplete event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                decimal totalUnits = 0.0M;
                decimal totalMrktValue = 0.00M;
                //decimal totalUseValue;
                decimal totalFinalValue = 0.0000M;

                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    if (this.landDetailsData.GetTotalUnits.Rows.Count > 0)
                    {
                        if (this.landDetailsData.GetTotalUnits.Rows[0][this.landDetailsData.GetTotalUnits.TotalUnitsColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.landDetailsData.GetTotalUnits.Rows[0][this.landDetailsData.GetTotalUnits.TotalUnitsColumn.ColumnName].ToString()))
                        {
                            decimal.TryParse(this.landDetailsData.GetTotalUnits.Rows[0][this.landDetailsData.GetTotalUnits.TotalUnitsColumn.ColumnName].ToString(), out totalUnits);
                        }
                        //used for Total Units Label Decimal (17,8)
                        this.TotalUnitsLabel.Text = totalUnits.ToString("#,##0.########");
                    }
                    else
                    {
                        this.TotalUnitsLabel.Text = totalUnits.ToString("#,##0.########");
                    }

                    if (this.landDetailsData.GetTotalValue.Rows.Count > 0)
                    {
                        if (this.landDetailsData.GetTotalValue.Rows[0][this.landDetailsData.GetTotalValue.TotalValueColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.landDetailsData.GetTotalValue.Rows[0][this.landDetailsData.GetTotalValue.TotalValueColumn.ColumnName].ToString()))
                        {
                            decimal.TryParse(this.landDetailsData.GetTotalValue.Rows[0][this.landDetailsData.GetTotalValue.TotalValueColumn.ColumnName].ToString(), out totalMrktValue);
                        }

                        if (totalMrktValue.ToString().Contains("-"))
                        {
                            this.TotalMarketValueLabel.Text = String.Concat("(", Decimal.Negate(totalMrktValue).ToString("#,##0.00"), ")");
                            this.TotalMarketValueLabel.ForeColor = Color.FromArgb(0, 128, 0);
                        }
                        else
                        {
                            this.TotalMarketValueLabel.Text = totalMrktValue.ToString("#,##0.00");
                            this.TotalMarketValueLabel.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        this.TotalMarketValueLabel.Text = totalMrktValue.ToString("#,##0.00");
                        this.TotalMarketValueLabel.ForeColor = Color.Black;
                    }

                    if (this.landDetailsData.GetTotalRating.Rows.Count > 0)
                    {
                        if (this.landDetailsData.GetTotalRating.Rows[0][this.landDetailsData.GetTotalRating.TotalRatingColumn.ColumnName] != null
                           && !string.IsNullOrEmpty(this.landDetailsData.GetTotalRating.Rows[0][this.landDetailsData.GetTotalRating.TotalRatingColumn.ColumnName].ToString()))
                        {
                            decimal.TryParse(this.landDetailsData.GetTotalRating.Rows[0][this.landDetailsData.GetTotalRating.TotalRatingColumn.ColumnName].ToString(), out totalFinalValue);
                        }

                        if (totalFinalValue.ToString().Contains("-"))
                        {
                            this.TotalFinalValueLabel.Text = String.Concat("(", Decimal.Negate(totalFinalValue).ToString("#,##0.0000"), ")");
                            this.TotalFinalValueLabel.ForeColor = Color.FromArgb(0, 128, 0);
                        }
                        else
                        {
                            this.TotalFinalValueLabel.Text = totalFinalValue.ToString("#,##0.0000");
                            this.TotalFinalValueLabel.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        this.TotalFinalValueLabel.Text = totalFinalValue.ToString("#,##0.0000");
                        this.TotalFinalValueLabel.ForeColor = Color.Black;
                    }
                }
                else
                {
                    this.TotalUnitsLabel.Text = string.Empty;
                    this.TotalMarketValueLabel.Text = string.Empty;
                    this.TotalFinalValueLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                    if (e.ColumnIndex.Equals(this.weightedRating.Index))
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells[this.landDetailsData.GetLandValuesSliceDetails.WeightedRatingColumn.ColumnName].Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                if (outDecimal.ToString().Contains("-"))
                                {
                                    e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.0000"), ")");
                                    e.CellStyle.ForeColor = Color.FromArgb(0, 128, 0);
                                }
                                else
                                {
                                    e.Value = outDecimal.ToString("#,##0.0000");
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

                    if (e.ColumnIndex.Equals(this.FinalValue.Index) || e.ColumnIndex.Equals(this.BaseDollarPerUnit.Index))
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
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

                    if (e.ColumnIndex.Equals(this.Units.Index))
                    {
                        if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        {
                            string val = e.Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                e.Value = outDecimal.ToString("#,##0.########");
                                e.FormattingApplied = true;
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
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void BaseAdjusmentComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.alternateLandCodeBaseValue.Equals(0) )
                    {
                        if (!string.IsNullOrEmpty(this.BaseAdjusmentComboBox.Text.Trim()))
                        {
                            string filterCond = "LandCode = '" + this.BaseAdjusmentComboBox.Text.Trim().Replace("'", "''") + "'";
                            DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                //this.getLandCodeBaseValueDataTable.Clear();
                                //this.landDetailsDataSet.Get_LandCodeBaseValue.Clear();
                                try
                                {
                                    if (this.LandUseComboBox.SelectedIndex > 0)
                                    {
                                        int isAglandID;
                                        int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                                        this.AglandID=isAglandID;
                                    }
                                    else
                                    {
                                        this.AglandID = null;
                                    }
                                    this.landDetailsDataSet=this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listBaseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, this.AglandID);
                                }
                                catch (Exception ex)
                                {

                                }
                                //this.getLandCodeBaseValueDataTable.Merge(this.landDetailsData.Get_LandCodeBaseValue);

                                if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
                                {
                                   decimal.TryParse(this.landDetailsDataSet.Get_LandCode.Rows[0]["BaseValue"].ToString(), out this.alternateLandCodeBaseValue);
                                    this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCode.Rows[0]["BaseValue"].ToString().Trim();
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
                            this.BaseAdjusmentComboBox.SelectedIndex = 0;
                            this.BaseDollerPerUnitTextBox.Text = "0.00";
                        }
                    }
                    this.CalculateFinalMarketValueTextBox(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                            DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);

                            if (choiceRows.Length > 0)
                            {
                                this.getLandCodeBaseValueDataTable.Clear();
                               // //this.landDetailsData.Get_LandCodeBaseValue.Clear();
                                try
                                {
                                    if (this.LandUseComboBox.SelectedIndex > 0)
                                    {
                                        int isAglandID;
                                        int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                                        this.AglandID=isAglandID;
                                    }
                                    else
                                    {
                                        this.AglandID = null;
                                    }
                                    this.landDetailsDataSet=this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listBaseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, this.AglandID);
                                    //this.getLandCodeBaseValueDataTable.Merge(this.landDetailsData.Get_LandCodeBaseValue);
                                }
                                catch (Exception ex)
                                {
                                }
                                if (this.landDetailsDataSet.Get_LandCodeBaseValue.Rows.Count > 0)
                                {
                                    decimal.TryParse(this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString(), out this.alternateLandCodeBaseValue);
                                    this.BaseDollerPerUnitTextBox.Text = this.landDetailsDataSet.Get_LandCodeBaseValue.Rows[0]["BaseValue"].ToString().Trim();  
                                }
                            }
                        }
                    }
                    else
                    {
                        this.BaseAdjusmentComboBox.SelectedIndex = 0;
                        this.BaseDollerPerUnitTextBox.Text = "0.00";
                    }
                    this.CalculateBaseMarketValueTextBox();
                    this.CalculateFinalMarketValueTextBox(); 
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

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
                        DataRow[] choiceRows = this.listBaseAdjustmentComboDataTable.Select(filterCond);

                        if (choiceRows.Length > 0)
                        {
                            //this.getLandCodeBaseValueDataTable.Clear();
                            //this.landDetailsData.Get_LandCodeBaseValue.Clear();
                          
                            
                          try
                          {
                              if (this.LandUseComboBox.SelectedIndex > 0)
                              {
                                  int isAglandID;
                                  int.TryParse(this.LandUseComboBox.SelectedValue.ToString(), out isAglandID);
                                  this.AglandID=isAglandID;
                              }
                              else
                              {
                                  this.AglandID = null;
                              }
                          
                            this.landDetailsDataSet =this.form39135Control.WorkItem.F36035_GetLandCodeBaseValue(choiceRows[0][this.listBaseAdjustmentComboDataTable.LandCodeColumn].ToString(), this.formValueSliceId, this.AglandID);
                            //this.getLandCodeBaseValueDataTable.Merge(this.landDetailsData.Get_LandCodeBaseValue);
                            }
                            catch(Exception ex)
                            {
                            }
                            if (this.landDetailsDataSet.Get_LandCode.Rows.Count > 0)
                            {
                               decimal.TryParse(this.landDetailsDataSet.Get_LandCode.Rows[0]["BaseValue"].ToString(), out this.alternateLandCodeBaseValue);
                                this.BaseDollerPerUnitTextBox.Text  = this.landDetailsDataSet.Get_LandCode.Rows[0]["BaseValue"].ToString().Trim();
                            }
                        
                        }
                    }
                    else
                    {
                        this.BaseAdjusmentComboBox.SelectedIndex = 0;
                        this.BaseDollerPerUnitTextBox.Text = "0.00";
                    }
                    this.CalculateBaseMarketValueTextBox(); 
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void UnitsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.isUnitTxtchange = true;
                    //this.CalculateBaseMarketValueTextBox();
                    //this.CalculatedWeightedRating();
                    this.EditEnabled(); 
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.CalculateBaseMarketValueTextBox();
                    this.CalculatedWeightedRating();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void LandUseComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.CalculatedWeightedRating();
                }
            }
            catch(Exception ex)
            {
                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }


        private void CalculatedWeightedRating()
        {
            if (!this.flagLoadOnProcess)
            {
                decimal units = 0.00M;

                if (!string.IsNullOrEmpty(this.UnitsTextBox.Text.Trim()))
                {
                    decimal.TryParse(this.UnitsTextBox.DecimalTextBoxValue.ToString(), out units);
                }
                string landcode = this.LandCodeTextBox.Text;  
                int? landuse;
                if (this.LandUseComboBox.SelectedValue  != null)
                {
                    int isAglandID;
                     int.TryParse(this.LandUseComboBox.SelectedValue.ToString() , out isAglandID);
                     landuse= isAglandID;
                }
                else
                {
                    landuse = null;
                }
                this.landDetailsData = this.form39135Control.WorkItem.F39135_WeightedRating(landcode, units, landuse, this.formValueSliceId);
                if (this.landDetailsData.WeightedRating_.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.landDetailsData.WeightedRating_.Rows[0][0].ToString()))
                    {
                        this.WtRatingTextBox.Text = this.landDetailsData.WeightedRating_.Rows[0][0].ToString();
                    }
                    else
                    {
                        this.WtRatingTextBox.Text = "0.0000";
                    }
                }
                else
                {
                    this.WtRatingTextBox.Text = "0.0000";
                }
            }
        }

        private void LandUseComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    //this.CalculateBaseMarketValueTextBox(); 
                    //this.CalculatedWeightedRating();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void BaseDollerPerUnitTextBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (!this.flagLoadOnProcess)
                {
                    /// used to remove base value and weighted RatingSP Call
                    if (this.isAllowSp)
                    {
                        this.CalculateBaseMarketValueTextBox();
                        this.CalculatedWeightedRating();
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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
                if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(1))
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
                else 
                if (this.BaseAdjusmentTypeComboBox.SelectedIndex.Equals(0))
                {
                    return false;
                }
                else 
                {
                   if (!string.IsNullOrEmpty(this.BaseAdjustmentTextBox.Text.Trim()) && this.BaseAdjustmentTextBox.DecimalTextBoxValue >= 0)
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

        private void BaseAdjustmentTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                    this.isAdjustmentChange = true;
                //    this.CalculateBaseMarketValueTextBox();
                //    this.SetInfluenceAdjustmentValue(); 
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void UnitsTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.UnitsTextBox, false, false))
                    {
                        return;
                    }

                    this.CalculateBaseDollerPerUnitTextBox();
                    this.CalculateBaseMarketValueTextBox();
                    this.CalculateFinalMarketValueTextBox();
                    if (this.isUnitTxtchange)
                    {
                       
                        /// used to modify while Change in Units field.
                        //this.GetLandCode(); 
                        this.CalculatedWeightedRating();
                    }
                    // check the max value for base market text box
                    if (this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.BaseMarketValueTextBox, true, true)
                        || this.CheckLandFieldsMaxLimitValidation(this.UnitsTextBox, this.BaseDollerPerUnitTextBox, true, true))
                    {
                        this.CalculateBaseMarketValueTextBox();
                        this.CalculateFinalMarketValueTextBox();
                        if (this.isUnitTxtchange)
                        {
                            this.CalculatedWeightedRating();
                        }
                    }

                    this.isUnitTxtchange = false;
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
                        if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals(2))
                        {
                            if (this.BaseAdjustmentTextBox.DecimalTextBoxValue < 0 || this.BaseAdjustmentTextBox.DecimalTextBoxValue > 999999)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentPercentageFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                ////Commented by Biju on 24-Sep-2009 to fix issue #4015
                                ////this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                              //  this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();
                                // this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                                this.BaseAdjustmentTextBox.Focus();
                                return;
                            }
                        }
                        //else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.UnitValue)
                        //    || this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Production))
                        //{
                        //    double baseAdjValue;
                        //    double.TryParse(this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out baseAdjValue);
                        //    if (baseAdjValue < 0 || baseAdjValue > this.maxBaseMarketFieldValue)
                        //    {
                        //        MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                        //        this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();
                        //        this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                        //        this.BaseAdjustmentTextBox.Focus();
                        //        return;
                        //    }
                        //}
                        //else if (this.BaseAdjusmentTypeComboBox.SelectedValue.Equals((byte)AdjustmentTypes.Additive))
                        //{
                        //    double baseAdjValue;
                        //    double.TryParse(this.BaseAdjustmentTextBox.DecimalTextBoxValue.ToString(), out baseAdjValue);
                        //    if (baseAdjValue < this.minBaseMarketFieldValue || baseAdjValue > this.maxBaseMarketFieldValue)
                        //    {
                        //        MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentNegativeDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        this.BaseAdjustmentTextBox.Text = decimal.Zero.ToString();
                        //        this.BaseAdjustmentTextBox.Focus();
                        //        return;
                        //    }
                        //}
                    }

                    //this.CalculateBaseDollerPerUnitTextBox();

                    //// check for the base doller per unit value max limit
                    //if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseDollerPerUnitTextBox, true, true))
                    //{
                    //    this.BaseMarketValueTextBox.Text = decimal.Zero.ToString();

                    //    ////// calculate the influence adjustment textbox fields
                    //    ////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    //    this.SetInfluenceAdjustmentValue();
                    //    this.CalculateFinalMarketValueTextBox();

                    //    return;
                    //}

                    //// calculate the base market value textbox
                    //this.CalculateBaseMarketValueTextBox();

                    //// check for the base market value max limit
                    //if (this.CheckLandFieldsMaxLimitValidation((Control)sender, this.BaseMarketValueTextBox, true, true))
                    //{
                    //    this.BaseDollerPerUnitTextBox.Text = decimal.Zero.ToString();
                    //}

                    //////// calculate the influence adjustment textbox fields
                    //////this.CalculateInfluenceAdjustmentTextBoxFields(sender);
                    //this.SetInfluenceAdjustmentValue();
                    //this.CalculateFinalMarketValueTextBox();

                    //////added by Biju on 25-Sep-2009 to fix forcolor issue for -ve values
                    //if (this.BaseAdjustmentTextBox.DecimalTextBoxValue < 0)
                    //{
                    //    this.BaseAdjustmentTextBox.ForeColor = System.Drawing.Color.Green;
                    //}
                    //else
                    //{
                    //    this.BaseAdjustmentTextBox.ForeColor = System.Drawing.Color.Black;
                    //}
                    //////till here
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void BaseAdjustmentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if (this.isAdjustmentChange)
                    {

                        if (this.CheckLandFieldsMaxLimitValidation(this.BaseAdjustmentTextBox, this.BaseAdjustmentTextBox, false, false))
                        {
                            return;
                        }

                        this.CalculateBaseMarketValueTextBox();
                        this.SetInfluenceAdjustmentValue();
                        this.CalculateBaseDollerPerUnitTextBox();
                        this.CalculateFinalMarketValueTextBox();

                        // check the max value for base market text box
                        if (this.CheckLandFieldsMaxLimitValidation(this.BaseAdjustmentTextBox, this.BaseMarketValueTextBox, true, true)
                            || this.CheckLandFieldsMaxLimitValidation(this.BaseAdjustmentTextBox, this.BaseDollerPerUnitTextBox, true, true))
                        {
                            this.CalculateBaseMarketValueTextBox();
                            this.SetInfluenceAdjustmentValue();
                            this.CalculateBaseDollerPerUnitTextBox();
                            this.CalculateFinalMarketValueTextBox();
                        }
                    }
                    this.isAdjustmentChange = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void TotalUnitsLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.TotalUnitsLabel, this.TotalUnitsLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void TotalMarketValueLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.TotalMarketValueLabel, this.TotalMarketValueLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void TotalFinalValueLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.TotalFinalValueLabel, this.TotalFinalValueLabel.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


       }
}
