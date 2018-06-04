//--------------------------------------------------------------------------------------------
// <copyright file="F36033.cs" company="Congruent">
//       Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the F36033.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************************
// Date               Author            Description
// ----------        ---------         ---------------------------------------------------------
// 05 SEP 2011       Manoj Kumar        Modified for the TSCO #13041
// 12 SEP 2011       Manoj Kumar        Modified for the TSCO #13316 
// ***********************************************************************************************/
namespace D34133
{

    #region NameSpace

    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;   
    using Microsoft.Practices.ObjectBuilder;  
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using Infrastructure.Interface;
    using Infragistics.Win; 
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    #endregion NameSpace

    [SmartPart] 
    public partial class F39133 : BaseSmartPart
    {
        #region Variables
       
        /// <summary>
        /// Used to store the landUniqueId (here it is unique id and key id)
        /// </summary>
        private int landUniqueId;

        /// <summary>
        /// Used to save land uniques id
        /// </summary>
        private int savedlandUniqueId;

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

        ///<summary>
        /// bool for edit mode in Noncropvalue;
        /// </summary>
        private bool editnonCropValue = false;

        ///<summary>
        /// bool for edit mode in Cropvalue
        /// </summary>
        private bool editcropValue = false;
        
        /// <summary>
        /// Used to store the currentRollYesr
        /// </summary>
        private string currentRollYear = string.Empty;


        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// Used store the setButtonModeOnformLoad
        /// </summary>
        private bool setButtonModeOnformLoad;

        /// <summary>
        /// controller F36033
        /// </summary>
        private F39133Controller form39133Control;

        /// <summary>
        /// used to store thelandCodeValuesGridSource
        /// </summary>
        private BindingSource landCodeValuesGridSource = new BindingSource();

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool avoidEditMode;

        /// <summary>
        /// Flag for click on grid
        /// </summary>
        private bool gridclickflag = false;

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool rollYearTextChanged;

        /// <summary>
        /// used to store the savedLandCodeRollYear
        /// </summary>
        private string savedLandCodeRollYear = string.Empty;

        ///<summary>
        /// used to hold the Crop Value text box
        /// </summary>
        private string isCropValue = string.Empty;

        ///<summary>
        /// used to hold the NonCrop Rate text box
        /// </summary>
        private string isNonCropRate = string.Empty;

        ///<summary>
        /// used to hold the Crop Rate text box
        /// </summary>
        private string isCropRate = string.Empty;
        
        ///<summary>
        /// used to hold the Crop Value text box
        /// </summary>
        private string isBreak1 = string.Empty;

        ///<summary>
        /// used to hold theisBreak2 text box
        /// </summary>
        private string isBreak2 = string.Empty;

        ///<summary>
        /// used to hold theisBreak3 text box
        /// </summary>
        private string isBreak3 = string.Empty;

        ///<summary>
        /// used to hold the isBreak4 text box
        /// </summary>
        private string isBreak4 = string.Empty;

        ///<summary>
        /// used to hold the isBreak1Value text box
        /// </summary>
        private string isBreak1Value = string.Empty;

        ///<summary>
        /// used to hold the isBreak1Value text box
        /// </summary>
        private string isBreak2Value = string.Empty;

        ///<summary>
        /// used to hold the isBreak1Value text box
        /// </summary>
        private string isBreak3Value = string.Empty;

        ///<summary>
        /// used to hold the isBreak1Value text box
        /// </summary>
        private string isBreak4Value = string.Empty;

        ///<summary>
        /// used to hold the Land Curve Formula
        /// </summary>
        private string isLandFormula = string.Empty;


        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        ///<summary>
        /// hold the rowIndex of Populate Land Code 
        /// </summary>
        private int rowIndex;
     
        
        /// <summary>
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        ///<summary>
        /// iscroprate
        /// </summary>
        private bool iscroprate = false;

        ///<summary>
        /// isnoncroprate
        /// </summary>
        private bool isnoncroprate = false;

        /// <summary>
        /// formMaster PermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Used to store the landCodesValuesData details
        /// </summary>
        private F39133LandCodeValueData landCodesValuesData = new F39133LandCodeValueData();

        ///<summary>
        /// Used to CalculateNonCropValues
        /// </summary>
        private F39133LandCodeValueData.CalculateNonCropValueDataTable calculateNonCrop = new F39133LandCodeValueData.CalculateNonCropValueDataTable(); 

        /// <summary>
        /// used to store the landCodeValuesDetailsGridDataTable
        /// </summary>
        private F39133LandCodeValueData.ListLandCodeValueDetailsDataTable landCodeValuesDetailsGridDataTable = new F39133LandCodeValueData.ListLandCodeValueDetailsDataTable();


        /// <summary>
        /// Used to store the listNeighborhoodTypeDataTable
        /// </summary>
        private F39133LandCodeValueData.ListNeighborhoodTypeDataTable listNeighborhoodTypeDataTable = new F39133LandCodeValueData.ListNeighborhoodTypeDataTable();

        /// <summary>
        /// Used to store the listLandCodeDataTable
        /// </summary>
        private F39133LandCodeValueData.ListLandCodeDataTable listLandCodeDataTable = new F39133LandCodeValueData.ListLandCodeDataTable();

        /// <summary>
        /// Usde to store the listUnitTypeDataTable
        /// </summary>
        private F39133LandCodeValueData.ListUnitTypeDataTable listUnitTypeDataTable = new F39133LandCodeValueData.ListUnitTypeDataTable();


        /// <summary>
        /// Used to store the listLandCodeComboBoxDataTableOnLoad
        /// </summary>
        private DataTable listLandCodeComboBoxDataTableOnLoad = new DataTable();

        /// <summary>
        /// Usde to store the listUnitTypeComboBoxDataTableOnLoad
        /// </summary>
        private DataTable listUnitTypeComboBoxDataTableOnLoad = new DataTable();

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        /// <summary>
        /// Instance variable to hold the min money field value
        /// </summary>
        private double minMoneyFieldValue = -922337203685477.58;

        #endregion Variables

        #region Constructor


        /// <summary>
        /// Initializes a new instance of the <see cref="T:F39133"/> class.
        /// </summary>
        public F39133()
        {
            InitializeComponent();
        }
       
        /// <summary>
        /// Initializes a new instance of the <see cref="F36033"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F39133(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.LandValuePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandValuePictureBox.Height, this.LandValuePictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructors
        
        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form36033 control.
        /// </summary>
        /// <value>The form36033 control.</value>
        [CreateNew]
        public F39133Controller Form39133Control
        {
            get { return this.form39133Control as F39133Controller; }
            set { this.form39133Control = value; }
        }

        #endregion Property

        #region Event Subscription

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
                    eventArgs.Data.FlagInvalidSliceKey = false;

                    if (this.setButtonModeOnformLoad)
                    {
                        this.setButtonModeOnformLoad = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
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

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            try
            {
                if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
                {
                    eventArgs.Data.FlagFormClose = this.CheckPageStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                ////here the current form slice F36033 is reloaded when F36032 formlice save and delete funcationality
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.LandValuedetailsDataGrid.Enabled = true;
                    this.LoadLandValueDetailsGrid();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form39133Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form39133Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form39133Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }
            this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);

        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>
        /// true - for continuing/false - leave unsaved changes
        /// </returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("F36033_LandCodeValueFormName"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SaveLandCodeValues())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelButtonClick();

                    return true;
                }

                return false;
            }

            return true;
        }

        //Modified for the TSCO #13041.
        ///<summary>
        /// Use to Calculate Crop Value and Non Crop Value
        /// </summary>
        /// <param>RollYear</param>
        /// <Param>CropRate</Param>
        /// <param>NonCropRate</param>
        public void CalculateCropValues(int rollYear, decimal?  CropRate, decimal? NonCropRate)
        {
            this.calculateNonCrop = this.form39133Control.WorkItem.F39133_CalculateNonCropValue(rollYear, CropRate, NonCropRate).CalculateNonCropValue;
            if (this.calculateNonCrop.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.calculateNonCrop.Rows[0][0].ToString()))
                {
                    this.CropValueTextBox.Text = this.calculateNonCrop.Rows[0][0].ToString();
                }
                else
                {
                    this.CropValueTextBox.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.calculateNonCrop.Rows[0][1].ToString()))
                {
                    this.NonCropValueTextBox.Text = this.calculateNonCrop.Rows[0][1].ToString();
                }
                else
                {
                    this.NonCropValueTextBox.Text = string.Empty;
                }
            }
            else
            {
                this.CropValueTextBox.Text = string.Empty ;
                this.NonCropValueTextBox.Text = string.Empty;
            }
        }




        /// <summary>
        /// To check max length of the Break and value per
        /// </summary>
        /// <returns>boolean value</returns>
        private bool CheckBreakMaxLength()
        {
            double breakMaxValue = 9999999.99;
            decimal valuePerMaxValue = 922337203685477.5807M;
            decimal ValueperMinValue = -922337203685477.5808M;
            decimal baseValue;
            decimal  usebasevalue;
            double break1;
            decimal break1ValuePer;
            double break2;
            decimal break2ValuePer;
            double break3;
            decimal break3ValuePer;
            double break4;
            decimal break4ValuePer;

            decimal.TryParse(this.NonCropValueTextBox.DecimalTextBoxValue.ToString(), out baseValue);
            decimal.TryParse(this.CropValueTextBox.DecimalTextBoxValue.ToString(), out usebasevalue);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1);
            decimal.TryParse(this.Break1ValuePerTextBox.DecimalTextBoxValue.ToString(), out break1ValuePer);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2);
            decimal.TryParse(this.Break2ValuePerTextBox.DecimalTextBoxValue.ToString(), out break2ValuePer);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3);
            decimal.TryParse(this.Break3ValuePerTextBox.DecimalTextBoxValue.ToString(), out break3ValuePer);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4);
            decimal.TryParse(this.Break4ValuePerTextBox.DecimalTextBoxValue.ToString(), out break4ValuePer);

            if (break1ValuePer >= ValueperMinValue && break3ValuePer >= ValueperMinValue && break4ValuePer >= ValueperMinValue && break2ValuePer >= ValueperMinValue && baseValue <= valuePerMaxValue && usebasevalue <= valuePerMaxValue && break1 <= breakMaxValue && break2 <= breakMaxValue && break3 <= breakMaxValue && break4 <= breakMaxValue && break1ValuePer <= valuePerMaxValue && break2ValuePer <= valuePerMaxValue && break3ValuePer <= valuePerMaxValue && break4ValuePer <= valuePerMaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit && !this.flagLoadOnProcess && !this.avoidEditMode && !this.gridclickflag)
            {
                this.LandValuedetailsDataGrid.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.SubClassTextBox.LockKeyPress = controlLook;
            this.CropValueTextBox.LockKeyPress = controlLook;
            this.CropRateTextBox.LockKeyPress = controlLook;
            this.NonCropRateTextBox.LockKeyPress = controlLook;
            this.LandValueCurveFormulaTextBox.LockKeyPress = controlLook;
            this.Break1TextBox.LockKeyPress = controlLook;
            this.Break1ValuePerTextBox.LockKeyPress = controlLook;
            this.Break2TextBox.LockKeyPress = controlLook;
            this.Break2ValuePerTextBox.LockKeyPress = controlLook;
            this.Break3TextBox.LockKeyPress = controlLook;
            this.Break3ValuePerTextBox.LockKeyPress = controlLook;
            this.Break4TextBox.LockKeyPress = controlLook;
            this.Break4ValuePerTextBox.LockKeyPress = controlLook;
            this.CropValueTextBox.LockKeyPress = controlLook;
            this.NonCropRateTextBox.LockKeyPress = controlLook;  

            this.NeighborhoodComboBox.Enabled = !controlLook;
            this.LandCodeComboBox.Enabled = !controlLook;
            this.UnitTypeComboBox.Enabled = !controlLook;

         
        }

        /// <summary>
        /// To Save the land code values.
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool SaveLandCodeValues()
        {
            int saveRollyear;

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())
                && !string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim())
                && (this.NeighborhoodComboBox.SelectedValue != null)
                && !string.IsNullOrEmpty(this.LandCodeComboBox.Text.Trim())
                && (this.LandCodeComboBox.SelectedValue != null)
                && (this.UnitTypeComboBox.SelectedValue != null)
                && !string.IsNullOrEmpty(this.UnitTypeComboBox.Text.Trim()))
            {
                ////This method is used to check the max length for decimal value text box
                if (this.CheckBreakMaxLength())
                {
                    // check for the max limit validation for the MrktMultiplier textBox
                    //if (this.CheckMaxFieldValidation(this.MrktMultiplierTextBox))
                    //{
                    //    return false;
                    //}

                    //// check for the max limit validation for the UseMultiplier textBox
                    //if (this.CheckMaxFieldValidation(this.UseMultiplierTextBox))
                    //{
                    //    return false;
                    //}

                    this.landCodesValuesData.SaveLandCodeValueDetails.Rows.Clear();
                    F39133LandCodeValueData.SaveLandCodeValueDetailsRow dr = this.landCodesValuesData.SaveLandCodeValueDetails.NewSaveLandCodeValueDetailsRow();
                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        int.TryParse(this.RollYearTextBox.Text.Trim(), out saveRollyear);
                        dr.RollYear = saveRollyear;
                    }

                    if (!string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim()))
                    {
                        dr.NBHDID = Convert.ToInt32(this.NeighborhoodComboBox.SelectedValue);
                    }

                    dr.LandCode = this.LandCodeComboBox.Text.Trim();
                    dr.UnitType = this.UnitTypeComboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(this.CropValueTextBox.Text.Trim()))
                    {
                        dr.UseBaseValue = this.CropValueTextBox.DecimalTextBoxValue; 
                    }
                    if (!string.IsNullOrEmpty(this.NonCropRateTextBox.Text.Trim()))
                    {
                        dr.NonCropRate  = this.NonCropRateTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.CropRateTextBox.Text.Trim()))
                    {
                        dr.CropRate  = this.CropRateTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()))
                    {
                        dr.VFormula = this.LandValueCurveFormulaTextBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.NonCropValueTextBox.Text.Trim()))
                    {
                        dr.BaseValue = this.NonCropValueTextBox.DecimalTextBoxValue;
                    }
                    

                    ////For UseValuePer


                    if (!string.IsNullOrEmpty(this.Break1TextBox.Text.Trim()))
                    {
                        dr.Break1 = this.Break1TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break1ValuePerTextBox.Text.Trim()))
                    {
                        dr.Value1 = this.Break1ValuePerTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break2TextBox.Text.Trim()))
                    {
                        dr.Break2 = this.Break2TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break2ValuePerTextBox.Text.Trim()))
                    {
                        dr.Value2 = this.Break2ValuePerTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break3TextBox.Text.Trim()))
                    {
                        dr.Break3 = this.Break3TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break3ValuePerTextBox.Text.Trim()))
                    {
                        dr.Value3 = this.Break3ValuePerTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break4TextBox.Text.Trim()))
                    {
                        dr.Break4 = this.Break4TextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.Break4ValuePerTextBox.Text.Trim()))
                    {
                        dr.Value4 = this.Break4ValuePerTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.SubClassTextBox.Text.Trim()))
                    {
                        dr.SubClass = this.SubClassTextBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.IsAglandComboBox.Text.Trim()))
                    {
                        dr.IsAgland  = Convert.ToByte(this.IsAglandComboBox.SelectedIndex);
                        //Modified for the TSCO #13041.
                        if (this.IsAglandComboBox.SelectedIndex.Equals(0))
                        {
                            dr.SetCropRateNull();
                            dr.SetNonCropRateNull();
                            dr.SetUseBaseValueNull();
                            this.isCropRate = string.Empty;
                            this.isNonCropRate  = string.Empty;
                            this.isCropValue  = string.Empty;
                            this.NonCropValueLabel.Text = "Base Value:";
                           
  
                        }
                        else
                        {
                            dr.SetBreak1Null();
                            dr.SetBreak2Null();
                            dr.SetBreak3Null();
                            dr.SetBreak4Null();
                            dr.SetValue1Null();
                            dr.SetValue2Null();
                            dr.SetValue3Null();
                            dr.SetValue4Null();
                            dr.SetVFormulaNull();
                            this.NonCropValueLabel.Text = "Non-Crop Value:";
                            //dr.VFormula = string.Empty;   
                            //this.isBreak1  = string.Empty;
                            //this.isBreak2  = string.Empty;
                            //this.isBreak3  = string.Empty;
                            //this.isBreak4 = string.Empty;
                            //this.isBreak1Value  = string.Empty;
                            //this.isBreak2Value  = string.Empty;
                            //this.isBreak3Value  = string.Empty;
                            //this.isBreak4Value  = string.Empty;
                            //this.isLandFormula  = string.Empty;   
                             
                        }
                        
                    }

                    this.landCodesValuesData.SaveLandCodeValueDetails.Rows.Add(dr);

                    if (this.ValidateBreakValue())
                    {
                        this.savedLandCodeRollYear = this.RollYearTextBox.Text.Trim();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.savedlandUniqueId = this.form39133Control.WorkItem.F39133_SaveLandCodeValue(null, TerraScanCommon.GetXmlString(this.landCodesValuesData.SaveLandCodeValueDetails.Copy()), TerraScanCommon.UserId);
                        }
                        else
                        {
                            this.savedlandUniqueId = this.form39133Control.WorkItem.F39133_SaveLandCodeValue(this.landUniqueId, TerraScanCommon.GetXmlString(this.landCodesValuesData.SaveLandCodeValueDetails.Copy()), TerraScanCommon.UserId);
                        }

                        ////when the savedlandUniqueid value are not Unique then -2 will be returned                    
                        if (this.savedlandUniqueId > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage1"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage2"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F39133_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        /// <summary>
        /// Usde to store the Grid and header paer
        /// </summary>
        private void ClearLandCodeGriddetails()
        {
            this.landUniqueId = -999;
            this.setButtonModeOnformLoad = true;
            this.ClearLandCodeHeaderControls();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            this.LandValueHeaderPanel.Enabled = false;
            
        }



        /// <summary>
        /// Used to clear the Land code form slice Header Controls
        /// </summary>
        private void ClearLandCodeHeaderControls()
        {
            this.landUniqueId = -999;
            this.avoidEditMode = true;
            this.RollYearTextBox.Text = string.Empty;
            ////this.listUnitTypeDataTable.Clear();
            this.listLandCodeDataTable.Clear();
            this.listNeighborhoodTypeDataTable.Clear();
            this.PopulateAllLandCodeComboBoxValues(string.Empty, string.Empty, string.Empty);
            this.UnitTypeComboBox.SelectedText = string.Empty;
            this.IsAglandComboBox.SelectedText = string.Empty;   
            this.SubClassTextBox.Text = string.Empty;
            /// used to identify the edit in crop value
            this.editcropValue = true;
            this.CropRateTextBox.Text = string.Empty;
            this.editcropValue = false;
            this.CropValueTextBox.Text = string.Empty;
            /// used to identify the edit in noncrop value
            this.editnonCropValue = true;
            this.NonCropRateTextBox.Text =string.Empty;
            this.editnonCropValue = false; 
            this.NonCropValueTextBox.Text = string.Empty;
            this.LandValueCurveFormulaTextBox.Text = string.Empty;
            this.Break1TextBox.Text = string.Empty;
            this.Break1ValuePerTextBox.Text = string.Empty;
            this.Break2TextBox.Text = string.Empty;
            this.Break2ValuePerTextBox.Text = string.Empty;
            this.Break3TextBox.Text = string.Empty;
            this.Break3ValuePerTextBox.Text = string.Empty;
            this.Break4TextBox.Text = string.Empty;
            this.Break4ValuePerTextBox.Text = string.Empty;
            this.CropValueTextBox.Text = string.Empty;
            this.avoidEditMode = false;
            this.gridclickflag = false;
        }

        /// <summary>
        /// Used to custimize the All combo box in the Load code Value Form
        /// </summary>
        private void CustimizeAllLandCodeComboBoxs()
        {
            this.NeighborhoodComboBox.MaxLength = this.landCodeValuesDetailsGridDataTable.NBHDListColumn.MaxLength;
            this.LandCodeComboBox.MaxLength = this.landCodeValuesDetailsGridDataTable.LandCodeColumn.MaxLength;
            this.UnitTypeComboBox.MaxLength = this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.MaxLength;

            this.NeighborhoodComboBox.DisplayMember = this.listNeighborhoodTypeDataTable.NBHDListColumn.ColumnName;
            this.NeighborhoodComboBox.ValueMember = this.listNeighborhoodTypeDataTable.NBHDIDColumn.ColumnName;

            this.LandCodeComboBox.DisplayMember = this.listLandCodeDataTable.LandCodeColumn.ColumnName;
            this.LandCodeComboBox.ValueMember = this.listLandCodeDataTable.LandCodeColumn.ColumnName;

            this.UnitTypeComboBox.DisplayMember = this.listUnitTypeDataTable.UnitTypeColumn.ColumnName;
            this.UnitTypeComboBox.ValueMember = this.listUnitTypeDataTable.UnitTypeColumn.ColumnName;
        }

        /// <summary>
        /// Used to load all the Combo box in the Land cod evalue form slice
        /// </summary>
        private void LoadAllLandCodeComboBoxs()
        {
            this.UnitTypeComboBox.DataSource = this.listUnitTypeDataTable;
            this.NeighborhoodComboBox.DataSource = this.listNeighborhoodTypeDataTable;
            this.LandCodeComboBox.DataSource = this.listLandCodeDataTable;
        }

        /// <summary>
        /// To Load the Unit type Combo Box
        /// </summary>
        /// <param name="unitTypeComboBoxSelectedValue">The unit type combo box selected value.</param>
        /// <param name="landCodeSelectedValue">The land code selected value.</param>
        /// <param name="neighborhoodTypeComboBoxSelectedValue">The neighborhood type combo box selected value.</param>
        private void PopulateAllLandCodeComboBoxValues(string unitTypeComboBoxSelectedValue, string landCodeSelectedValue, string neighborhoodTypeComboBoxSelectedValue)
        {
            if (!string.IsNullOrEmpty(neighborhoodTypeComboBoxSelectedValue))
            {
                this.NeighborhoodComboBox.SelectedValue = neighborhoodTypeComboBoxSelectedValue;
            }
            else
            {
                this.NeighborhoodComboBox.SelectedValue = -1;
            }

            this.LandCodeComboBox.SelectedValue = landCodeSelectedValue;
            this.UnitTypeComboBox.SelectedValue = unitTypeComboBoxSelectedValue;
        }
        /// <summary>
        /// Sets the width of the drop down.
        /// </summary>
        private void SetDropDownWidth()
        {
            if (this.landCodesValuesData.ListNeighborhoodType.Count > 0)
            {
                int oneTimeValueSet = 0;
                string tempNBHDListLenght = string.Empty;
                string tempNBHDListmaxLenght = string.Empty;
                for (int i = 0; i < this.landCodesValuesData.ListNeighborhoodType.Count; i++)
                {
                    tempNBHDListLenght = this.landCodesValuesData.ListNeighborhoodType.Rows[i][this.landCodesValuesData.ListNeighborhoodType.NBHDListColumn.ColumnName].ToString();
                    //// First time value has to be set to tempNBHDListmaxLenght for comparing with tempNBHDListLenght values in the dataset
                    if (oneTimeValueSet.Equals(0))
                    {
                        tempNBHDListmaxLenght = tempNBHDListLenght;
                        oneTimeValueSet++;
                    }

                    //// comparing tempNBHDListmaxLenght with tempNBHDListLenght values in the dataset and setting the tempNBHDListmaxLenght value depending on the condition
                    if (tempNBHDListmaxLenght.Length < tempNBHDListLenght.Length)
                    {
                        //// If tempNBHDListmaxLenght smaller than the tempNBHDListLenght then tempNBHDListLenght value is assigned to tempNBHDListmaxLenght
                        tempNBHDListmaxLenght = tempNBHDListLenght;
                    }
                }

                //// Calculating the Width of the NeighborhoodComboBox
                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempNBHDListmaxLenght, this.NeighborhoodComboBox.Font);
                float preferredWidth = sizeF.Width;
                //// Setting the NeighborhoodComboBox's DropDownWidth
                if (preferredWidth > this.NeighborhoodComboBox.Width)
                {
                    //// Checking the Number of rows in the ListNeighborhoodType table with the Combo MaxDropDownItems
                    if (this.landCodesValuesData.ListNeighborhoodType.Count > this.NeighborhoodComboBox.MaxDropDownItems)
                    {
                        //// If ListNeighborhoodType table has greater than the MaxDropDownItems in NeighborhoodComboBox then "15" increased to show the scroll bar
                        this.NeighborhoodComboBox.DropDownWidth = Convert.ToInt32(preferredWidth) + 15;
                    }
                    else
                    {
                        //// If ListNeighborhoodType table has smaller than the MaxDropDownItems in NeighborhoodComboBox then the preferredWith is assigned
                        this.NeighborhoodComboBox.DropDownWidth = Convert.ToInt32(preferredWidth);
                    }
                }
                else
                {
                    //// If preferredWidth is smaller than NeighborhoodComboBox Width then default width is assigned
                    this.NeighborhoodComboBox.DropDownWidth = this.NeighborhoodComboBox.Width;
                }
            }
            else
            {
                //// If ListNeighborhoodType count is empty then default width is assigned
                this.NeighborhoodComboBox.DropDownWidth = this.NeighborhoodComboBox.Width;
            }
        }

        /// <summary>
        /// Loads the All Combo Box based on roll year.
        /// </summary>
        private void LoadComboBoxBasedOnRollYear()
        {
            int temprollyear;
            ////for current roll year text is used to select DISTINCT  value for perticular roll year
            F39133LandCodeValueData.ListLandCodeDataTable tempListlandCodeDataTable = new F39133LandCodeValueData.ListLandCodeDataTable();

            string filterLandCodeCondition;

            int.TryParse(this.RollYearTextBox.Text.Trim(), out temprollyear);

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                filterLandCodeCondition = SharedFunctions.GetResourceString("F36033_LandCodeComboBoxFilterCondition1") + temprollyear;
            }
            else
            {
                filterLandCodeCondition = SharedFunctions.GetResourceString("F36033_LandCodeComboBoxFilterCondition2");
            }

            // Populate Neighborhood combobox based on the roll year
            this.landCodesValuesData = this.form39133Control.WorkItem.F39133_ListNeighborhoodType(temprollyear);

            // Set the Width of the Neiborhood Combo when lenght increases than its width defined.
            this.SetDropDownWidth();

            this.listNeighborhoodTypeDataTable = this.landCodesValuesData.ListNeighborhoodType;
            this.NeighborhoodComboBox.DataSource = this.listNeighborhoodTypeDataTable;

            DataRow[] landCodedatatableRowCollection = this.listLandCodeComboBoxDataTableOnLoad.Select(filterLandCodeCondition);

            foreach (DataRow currentlandCodeRow in landCodedatatableRowCollection)
            {
                tempListlandCodeDataTable.ImportRow(currentlandCodeRow);
            }

            this.listLandCodeDataTable.Rows.Clear();
            this.listLandCodeDataTable = tempListlandCodeDataTable;
            this.LandCodeComboBox.DataSource = this.listLandCodeDataTable;
            this.LandCodeComboBox.SelectedValue = string.Empty;
            this.NeighborhoodComboBox.SelectedIndex = -1;
            this.NeighborhoodComboBox.Text = string.Empty;
        }

        #endregion Methods

        /// <summary>
        /// To Load the land code details grid.
        /// </summary>
        private void LoadLandValueDetailsGrid()
        {
            this.flagLoadOnProcess = true;
            this.RollYearTextBox.Text = string.Empty;   
            this.landCodesValuesData.Clear();
            this.listNeighborhoodTypeDataTable.Clear();
            this.listUnitTypeDataTable.Clear();
            this.listLandCodeDataTable.Clear();
            this.listUnitTypeComboBoxDataTableOnLoad.Clear();
            this.listLandCodeComboBoxDataTableOnLoad.Clear();

            this.CustimizeAllLandCodeComboBoxs();

            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].Columns[this.landCodesValuesData.ListLandCodeValueDetails.BaseValueColumn.ColumnName].MaskInput = "nnnnnnnnnnnnnnn.nn";

            ////to load the Land code individual values
            this.landCodesValuesData = this.form39133Control.WorkItem.F39133_ListIndividualLandCodeValuesItems();
            this.listNeighborhoodTypeDataTable = this.landCodesValuesData.ListNeighborhoodType;
            this.listUnitTypeDataTable = this.landCodesValuesData.ListUnitType;
            this.listLandCodeDataTable = this.landCodesValuesData.ListLandCode;
            this.listUnitTypeComboBoxDataTableOnLoad = this.listUnitTypeDataTable.Copy();
            this.listLandCodeComboBoxDataTableOnLoad = this.listLandCodeDataTable.Copy();

            ////to get theapplication roll year
            if (this.landCodesValuesData.GetAppRollYear.Rows.Count > 0)
            {
                this.currentRollYear = this.landCodesValuesData.GetAppRollYear.Rows[0][this.landCodesValuesData.GetAppRollYear.AssessmentRollYearColumn].ToString();
            }

            ////bind the combo box with datatable
            this.LoadAllLandCodeComboBoxs();

            ////to load the land code grid
            this.landCodesValuesData = this.form39133Control.WorkItem.F39133_ListLandCodeValues();
            this.landCodeValuesDetailsGridDataTable = this.landCodesValuesData.ListLandCodeValueDetails;
            this.LandValuedetailsDataGrid.DataSource = this.landCodeValuesDetailsGridDataTable;

            if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0)
            {
                this.LandValueGridPanel.Enabled = true;
                ////this.LandCodedetailsDataGrid.Focus();

                ////to filter the datatset containing the roll year
                if (!string.IsNullOrEmpty(this.currentRollYear))
                {
                    this.FilterLandCodeGridRows(this.currentRollYear);
                }
                else
                {
                    this.LandValuedetailsDataGrid.Rows[0].Selected = true;

                }
            }
            else
            {
                this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.ClearLandCodeGriddetails();
                this.LandValueGridPanel.Enabled = false;
            }

            this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// used to filter the land code grid row filter
        /// </summary>
        /// <param name="filterByRollYear">The filter by roll year.</param>
        private void FilterLandCodeGridRows(string filterByRollYear)
        {
            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].FilterConditions.Add(FilterComparisionOperator.StartsWith, filterByRollYear);
            UltraGridRow[] filteredRow = this.LandValuedetailsDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                filteredRow[0].Activated = true;
                filteredRow[0].Selected = true;

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

             }
            else
            {
                this.ClearLandCodeGriddetails();
            }
        }


        #region FormLoad
        private void F39133_Load(object sender, EventArgs e)
        {

            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.LoadLandValueDetailsGrid();
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
        #endregion FormLoad


        /// <summary>
        /// Handles the MouseHover event of the LandValuePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandValuePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandValuesFormSliceToolTip.SetToolTip(this.LandValuePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LandCodeValuePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandValuePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.RollYearTextBox.Text = string.Empty;
                ////combo box value are loaded based on the Roll year
                this.LoadComboBoxBasedOnRollYear();
                this.LandValuedetailsDataGrid.Enabled = false;
                this.ControlLock(!this.PermissionFiled.newPermission);
                this.LandValueHeaderPanel.Enabled = true;
                this.ClearLandCodeHeaderControls();
               // this.NonCropValueTextBox.Text = decimal.Zero.ToString();
                //Modified for the TSCO #13041.
                this.IsAglandComboBox.SelectedIndex = 0;
                this.NonCropValueLabel.Text = "Base Value:";
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.RollYearTextBox.Focus();
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
        /// Saves the button_ click.
        /// </summary>
        private void SaveButtonClick()
        {
            try
            {
                int currentRowIndexValue;
                if (this.SaveLandCodeValues())
                {
                    this.LandValuedetailsDataGrid.Enabled = true;
                    this.LoadLandValueDetailsGrid();
                    this.landCodeValuesGridSource.DataSource = this.landCodeValuesDetailsGridDataTable.DefaultView;
   
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0)
                    {
                        this.FilterLandCodeGridRows(this.savedLandCodeRollYear);

                        currentRowIndexValue = this.landCodeValuesGridSource.Find(this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName, this.savedlandUniqueId);
                        if (currentRowIndexValue >= 0)
                        {
                            this.PopulateLandCodeHeaderPartControls(currentRowIndexValue);
                            this.LandValuedetailsDataGrid.Rows[currentRowIndexValue].Selected = true;
                            this.LandValuedetailsDataGrid.Rows[currentRowIndexValue].Activated = true;
                            this.LandValuedetailsDataGrid.Focus();
                        }
                    }

                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
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
        /// Handles the BeforeRowActivate event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void LandValuedetailsDataGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.PopulateLandCodeHeaderPartControls(e.Row.Index);
                    this.rowIndex = e.Row.Index;
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
        /// Handles the Click event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandValuedetailsDataGrid_Click(object sender, EventArgs e)
        {
            this.gridclickflag = true;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the UnitTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnitTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the SelectionChangeCommitted event of the NeighborhoodComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

         /// <summary>
        /// Used to Populate the Land Code header part controls
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateLandCodeHeaderPartControls(int rowIndex)
        {
            if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0 && rowIndex >= 0 && this.LandValuedetailsDataGrid.Rows.VisibleRowCount > 1 && rowIndex <= this.landCodeValuesDetailsGridDataTable.Rows.Count - 1)
            {
                this.avoidEditMode = true;

                int.TryParse(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName].Value.ToString(), out this.landUniqueId);
              
                this.RollYearTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].Value.ToString();
                this.LoadComboBoxBasedOnRollYear();
                ////this.currentRollYear = this.landCodeValuesDetailsGridDataTable.Rows[rowIndex][this.landCodeValuesDetailsGridDataTable.RollYearColumn].ToString();
                this.PopulateAllLandCodeComboBoxValues(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].Value.ToString(), this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].Value.ToString(), this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.NBHDIDColumn.ColumnName].Value.ToString());
                this.CropValueTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Value.ToString();
                this.Break1TextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Value.ToString();
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak1 = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Value.ToString();
               // }
               //if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak2 = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak3 = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak4 = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak1Value = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak2Value = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak3Value = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Value.ToString();
               // }
               // if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Value.ToString()))
               // {
               //     this.isBreak4Value = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Value.ToString();
               // }
                        
                this.Break1ValuePerTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Value.ToString();
                this.Break2TextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Value.ToString();
                this.Break2ValuePerTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Value.ToString();
                this.Break3TextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Value.ToString();
                this.Break3ValuePerTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Value.ToString();
                this.Break4TextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Value.ToString();
                this.Break4ValuePerTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Value.ToString();
                //if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Value.ToString()))
                //{
                //    this.isLandFormula = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Value.ToString();
                //}
                this.LandValueCurveFormulaTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Value.ToString();

                //if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Value.ToString()))
                //{
                //    this.isCropValue = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Value.ToString();
                //}
                this.NonCropRateTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Value.ToString();
                this.CropRateTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Value.ToString();
                //if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Value.ToString()))
                //{
                //    this.isNonCropRate = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Value.ToString();  
                //}
                //if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Value.ToString()))
                //{
                //    this.isCropRate  =this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Value.ToString();
                //}

                if (!string.IsNullOrEmpty(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.IsAglandColumn.ColumnName].Value.ToString()))
                {
                    this.IsAglandComboBox.SelectedIndex  = Convert.ToInt32(this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.IsAglandColumn.ColumnName].Value.ToString());
                    //Modified for the TSCO #13041.
                    if (this.IsAglandComboBox.SelectedIndex.Equals(0))
                    {
                        this.CropValueTextBox.Text = string.Empty;
                        this.CropRateTextBox.Text = string.Empty;
                        this.NonCropRateTextBox.Text = string.Empty;
                        this.NonCropValueLabel.Text = "Base Value:";
                    }
                    else
                    {
                        this.NonCropValueLabel.Text = "Non-Crop Value:";
                        this.LandValueCurveFormulaTextBox.Text = string.Empty;
                        this.Break1TextBox.Text = string.Empty; 
                        this.Break1ValuePerTextBox.Text = string.Empty;  
                        this.Break2TextBox.Text = string.Empty;  
                        this.Break2ValuePerTextBox.Text = string.Empty;  
                        this.Break3TextBox.Text = string.Empty;  
                        this.Break3ValuePerTextBox.Text = string.Empty;  
                        this.Break4TextBox.Text = string.Empty;  
                        this.Break4ValuePerTextBox.Text = string.Empty;  
                    }
                }
                else
                {
                    this.IsAglandComboBox.SelectedIndex = 0;
                    this.CropValueTextBox.Text = string.Empty;
                    this.CropRateTextBox.Text = string.Empty;
                    this.NonCropRateTextBox.Text = string.Empty;
                    this.NonCropValueLabel.Text = "Base Value:";
                    //this.LandValueCurveFormulaTextBox.Text = string.Empty;
                    //this.Break1TextBox.Text = string.Empty;
                    //this.Break1ValuePerTextBox.Text = string.Empty;
                    //this.Break2TextBox.Text = string.Empty;
                    //this.Break2ValuePerTextBox.Text = string.Empty;
                    //this.Break3TextBox.Text = string.Empty;
                    //this.Break3ValuePerTextBox.Text = string.Empty;
                    //this.Break4TextBox.Text = string.Empty;
                    //this.Break4ValuePerTextBox.Text = string.Empty;  
                }
               
                this.NonCropValueTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Value.ToString();
               
                this.SubClassTextBox.Text = this.LandValuedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].Value.ToString();

                ////For New Construction
               

                this.LandValueHeaderPanel.Enabled = true;
                
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.avoidEditMode = false;
            }
            else
            {
                this.ClearLandCodeHeaderControls();
                this.landUniqueId = -999;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.LandValueHeaderPanel.Enabled = false;
            }
            this.ChangeIsAglandCombobox();
        }
        /// <summary>
        /// Changes the type of the IsAgland Combobox.
        /// </summary>
        private void ChangeIsAglandCombobox()
        {
           
            bool disabled = true;
            bool adjdisabled = true;
           
            if (this.IsAglandComboBox.SelectedIndex.Equals(0))
            {
                adjdisabled = false;
            }
            else
            {
                disabled = false;
            }
            this.SetFieldsFillToLightGrayAndDisable(this.NonCropRatePanel, this.NonCropRateLabel, this.NonCropRateTextBox, !adjdisabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.CropRatePanel, this.CropRateLabel, this.CropRateTextBox, !adjdisabled, true);
            // TSCO 13041 make Crop Value and Non Crop Value changes uneditable during IsAgland Yes Operation.
            this.SetFieldsFillToLightGrayAndDisable(this.CropValuePanel, this.CropValueLabel, this.CropValueTextBox, true, true);
            this.SetFieldsFillToLightGrayAndDisable(this.NonCropValuePanel, this.NonCropValueLabel, this.NonCropValueTextBox, !disabled, true);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break1Panel, this.Break1Label, this.Break1TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break1ValuePerPanel, this.Break1ValuePerLabel, this.Break1ValuePerTextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break2Panel, this.Break2Label, this.Break2TextBox, !disabled, true);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break2ValuePerPanel, this.Break2ValuePerLabel, this.Break2ValuePerTextBox, !disabled, true);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break3Panel, this.Break3Label, this.Break3TextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break3ValuePerPanel, this.Break3ValuePerLabel, this.Break3ValuePerTextBox, !disabled, false);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break4Panel, this.Break4Label, this.Break4TextBox, !disabled, true);
            this.SetBreakFieldsFillToLightGrayAndDisable(this.Break4ValuePerPanel, this.Break4ValuePerLabel, this.Break4ValuePerTextBox, !disabled, true);
            this.SetFieldsFillToLightGrayAndDisable(this.LandValueCurveFormulaPanel, this.LandValueCurveFormulalabel, this.LandValueCurveFormulaTextBox, !disabled, true);
               
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
        /// Sets the break fields fill to light gray and disable.
        /// </summary>
        /// <param name="currentPanel">The current panel.</param>
        /// <param name="currentLabel">The current label.</param>
        /// <param name="currentTextBox">The current text box.</param>
        /// <param name="disable">if set to <c>true</c> [disable].</param>
        /// <param name="altColor">if set to <c>true</c> [alt color].</param>
        private void SetFieldsFillToLightGrayAndDisable(Panel currentPanel, Label currentLabel, TerraScanTextBox currentTextBox, bool disable, bool altColor)
        {
            currentPanel.Enabled = !disable;

            if (disable)
            {
                currentPanel.BackColor = this.disabledPanelBackColor;
                currentLabel.ForeColor = this.disabledLabelForeColor;

                if (currentTextBox != null)
                {
                    currentTextBox.BackColor = this.disabledTextBoxForeAndBackColor;
                    currentTextBox.ForeColor = this.disabledTextBoxForeAndBackColor;
                }
            }
            else
            {
                if (altColor)
                {
                    currentPanel.BackColor = Color.White; 
                    currentLabel.ForeColor = this.standardLabelForeColor;

                    if (currentTextBox != null)
                    {
                        currentTextBox.BackColor = Color.White; 
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
                        currentPanel.BackColor = Color.White;
                        currentLabel.ForeColor = this.standardLabelForeColor;

                        if (currentTextBox != null)
                        {
                            currentTextBox.BackColor = Color.White;
                            currentTextBox.ForeColor = this.standardTextBoxForeColor;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButtonClick()
        {
            try
            {
                this.LandValuedetailsDataGrid.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
              //  this.LoadLandValueDetailsGrid();
                this.PopulateLandCodeHeaderPartControls(this.rowIndex); 
                 
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
        /// To Validate the Break values
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool ValidateBreakValue()
        {
            decimal currentBreakValue;
            decimal referBreakvalue;
            int currentColumnIndex;

            F39133LandCodeValueData.CheckBreakValuesDataTable checkBreakValuesDataTable = new F39133LandCodeValueData.CheckBreakValuesDataTable();

            ////Here break Value are validated such that the Break value are in ascending order
            string filtercondtion = SharedFunctions.GetResourceString("F36033_RollYearNotNullFilterCondition");

            DataRow[] validatateRow = this.landCodesValuesData.SaveLandCodeValueDetails.Select(filtercondtion);

            foreach (DataRow currentValidateRow in validatateRow)
            {
                checkBreakValuesDataTable.ImportRow(currentValidateRow);
            }

            if (checkBreakValuesDataTable.Rows.Count > 0)
            {
                ////here for loop is used to check whether the break values are increasing order and other condition like break can be given when all its desecending break are given
                for (int i = checkBreakValuesDataTable.Columns.Count; i > 0; i--)
                {
                    currentColumnIndex = i - 1;

                    if (!string.IsNullOrEmpty(checkBreakValuesDataTable.Rows[0][currentColumnIndex].ToString()))
                    {
                        decimal.TryParse(checkBreakValuesDataTable.Rows[0][currentColumnIndex].ToString(), out currentBreakValue);

                        if (currentBreakValue > 0)
                        {
                            for (int j = currentColumnIndex - 1; j >= 0; j--)
                            {
                                if (!string.IsNullOrEmpty(checkBreakValuesDataTable.Rows[0][j].ToString()))
                                {
                                    decimal.TryParse(checkBreakValuesDataTable.Rows[0][j].ToString(), out referBreakvalue);

                                    if (currentBreakValue <= referBreakvalue)
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButtonClick()
        {
            try
            {
                if (this.landUniqueId > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int deletedReturnedValue = this.form39133Control.WorkItem.F39133_DeleteLandCodevalue(this.landUniqueId, TerraScanCommon.UserId);

                        ////when the land code is deleted then reload the form
                        if (deletedReturnedValue > 0)
                        {
                            this.LoadLandValueDetailsGrid();
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F36033_DeleteValidateMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                    else
                    {
                        return;
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
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();

                if (!this.flagLoadOnProcess && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.rollYearTextChanged = true;
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
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess && !this.avoidEditMode && this.rollYearTextChanged)
                {
                    this.LoadComboBoxBasedOnRollYear();
                    //Modified for the TSCO #13041.
                    if (this.IsAglandComboBox.SelectedIndex.Equals(1))
                    {
                         int rollyear;
                         int.TryParse(this.RollYearTextBox.Text.Trim(), out rollyear);
                         decimal? cropRate, noncropRate;
                         if (!string.IsNullOrEmpty(this.CropRateTextBox.Text))
                         {
                             cropRate = this.CropRateTextBox.DecimalTextBoxValue; 
                         }
                         else
                         {
                             cropRate = null; 
                         }
                         if (!string.IsNullOrEmpty(this.NonCropRateTextBox.Text))
                         {
                             noncropRate = this.NonCropRateTextBox.DecimalTextBoxValue;
                         }
                         else
                         {
                             noncropRate = null;
                         }
                         this.CalculateCropValues(rollyear, cropRate, noncropRate);       
                    }
                    this.rollYearTextChanged = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ComboBox_Validating(object sender, CancelEventArgs e)
        {
            TerraScanComboBox currentComboBox = (TerraScanComboBox)sender;
            if (currentComboBox.SelectedValue == null)
            {
                currentComboBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Handles the Leave event of the LandCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LandCodeComboBox_Leave(object sender, EventArgs e)
        {
            this.LandCodeComboBox.BackColor = Color.White;
            this.LandCodeComboBoxPanel.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LandCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Controls_TextChangedEvent(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void IsAglandComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(!this.flagLoadOnProcess)
                {
                    this.gridclickflag = false;
                    this.ChangeIsAglandCombobox();
                    this.EditEnabled();
                    //Modified for the TSCO #13041.
                    if(this.IsAglandComboBox.SelectedIndex.Equals(0))
                    {
                        this.CropValueTextBox.Text = string.Empty;
                        this.NonCropRateTextBox.Text = string.Empty;
                        this.CropRateTextBox.Text = string.Empty;
                        // this.NonCropValueTextBox.Text = "0.00";
                        this.NonCropValueTextBox.Text = string.Empty;
                        this.NonCropValueLabel.Text = "Base Value:";
                    }
                    else
                    {
                    this.NonCropValueLabel.Text = "Non-Crop Value:";
                   // this.NonCropValueTextBox.Text = "0.00";
                    this.editnonCropValue = true; 
                    this.NonCropValueTextBox.Text = string.Empty;
                    this.editnonCropValue = false;  
                    this.Break1TextBox.Text = string.Empty;
                    this.Break2TextBox.Text = string.Empty;
                    this.Break3TextBox.Text = string.Empty;
                    this.Break4TextBox.Text = string.Empty;
                    this.Break1ValuePerTextBox.Text = string.Empty;
                    this.Break2ValuePerTextBox.Text = string.Empty;
                    this.Break3ValuePerTextBox.Text = string.Empty;
                    this.Break4ValuePerTextBox.Text = string.Empty;
                    this.LandValueCurveFormulaTextBox.Text = string.Empty;
                    }
                   
                     
                       
                
                }
           
             
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void Break1TextBox_Leave(object sender, EventArgs e)
        {
          // this.isBreak1 = this.Break1TextBox.DecimalTextBoxValue;  
        }

        private void Break1ValuePerTextBox_Leave(object sender, EventArgs e)
        {
           // this.isBreak1Value = this.Break1ValuePerTextBox.DecimalTextBoxValue;  
        }

        private void Break2TextBox_Leave(object sender, EventArgs e)
        {
           // this.isBreak2 = this.Break2TextBox.DecimalTextBoxValue;  
        }

        private void Break2ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            //this.isBreak2Value = this.Break2ValuePerTextBox.DecimalTextBoxValue;  
        }

        private void Break3TextBox_Leave(object sender, EventArgs e)
        {
            //this.isBreak3 = this.Break3TextBox.DecimalTextBoxValue; 
        }

        private void Break3ValuePerTextBox_Leave(object sender, EventArgs e)
        {
            //this.isBreak3Value = this.Break3ValuePerTextBox.DecimalTextBoxValue;   
        }

        private void Break4TextBox_Leave(object sender, EventArgs e)
        {
           // this.isBreak4  = this.Break4TextBox.DecimalTextBoxValue;  
        }

        private void Break4ValuePerTextBox_Leave(object sender, EventArgs e)
        {
          //  this.isBreak4Value  = this.Break4ValuePerTextBox.DecimalTextBoxValue;  
        }

        private void LandValueCurveFormulaTextBox_Leave(object sender, EventArgs e)
        {
            //this.isLandFormula = this.LandValueCurveFormulaTextBox.Text;  
        }

        private void CropRateTextBox_Leave(object sender, EventArgs e)
        {
            double value = 9.9999;
            double minValue;
            double.TryParse(this.CropRateTextBox.Text.Trim(), out  minValue);
            if (minValue <= value)
            {
            }
            else
            {
                this.CropRateTextBox.Text = "0.0000";
                this.CropRateTextBox.DecimalTextBoxValue = 0.0000M; 
            }
            if (this.CropRateTextBox.DecimalTextBoxValue < 0)
            {
                this.CropRateTextBox.Text = "0.00";
            }
            //Modified for the TSCO #13041.
            if (this.IsAglandComboBox.SelectedIndex.Equals(1) && this.iscroprate)
            {
                int rollyear;
                int.TryParse(this.RollYearTextBox.Text.Trim(), out rollyear);
                decimal? cropRate, noncropRate;
                if (!string.IsNullOrEmpty(this.CropRateTextBox.Text))
                {
                    cropRate = this.CropRateTextBox.DecimalTextBoxValue;
                }
                else
                {
                    cropRate = null;
                }
                if (!string.IsNullOrEmpty(this.NonCropRateTextBox.Text))
                {
                    noncropRate = this.NonCropRateTextBox.DecimalTextBoxValue;
                }
                else
                {
                    noncropRate = null;
                }
                this.CalculateCropValues(rollyear, cropRate, noncropRate);
                 
            }
            this.iscroprate = false; 
        }

        private void CropValueTextBox_Leave(object sender, EventArgs e)
        {
            if (this.CropValueTextBox.DecimalTextBoxValue < 0)
            {
                this.CropValueTextBox.Text = "0.00";
            }
        }

        private void NonCropRateTextBox_Leave(object sender, EventArgs e)
        {
            double value = 9.9999;
            double minValue;
            double.TryParse(this.NonCropRateTextBox.Text.Trim(), out  minValue);
            if (minValue <= value)
            {
            }
            else
            {
                this.NonCropRateTextBox.Text  = "0.0000"; 
            }
            if (this.NonCropRateTextBox.DecimalTextBoxValue < 0)
            {
                this.NonCropRateTextBox.Text = "0.00";
            }
            if (this.IsAglandComboBox.SelectedIndex.Equals(1)&& this.isnoncroprate )
            {
                int rollyear;
                int.TryParse(this.RollYearTextBox.Text.Trim(), out rollyear);
                decimal? cropRate, noncropRate;
                if (!string.IsNullOrEmpty(this.CropRateTextBox.Text))
                {
                    cropRate = this.CropRateTextBox.DecimalTextBoxValue;
                }
                else
                {
                    cropRate = null;
                }
                if (!string.IsNullOrEmpty(this.NonCropRateTextBox.Text))
                {
                    noncropRate = this.NonCropRateTextBox.DecimalTextBoxValue;
                }
                else
                {
                    noncropRate = null;
                }
                this.CalculateCropValues(rollyear, cropRate, noncropRate);
            }
            this.isnoncroprate = false; 
        }

        /// <summary>
        /// Handles the InitializeLayout event of the Land Value Details Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void LandValuedetailsDataGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeLandValueDetailsGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Customizes the schedule line item grid.
        /// </summary>
        private void CustomizeLandValueDetailsGrid()
        {
            UltraGridBand currentBand = this.LandValuedetailsDataGrid.DisplayLayout.Bands[0];
            // set the RowSelectors to true
            currentBand.Override.RowSelectors = DefaultableBoolean.True;

            // set the RowLayoutStyle to ColumnLayout to customize the header of grid
            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.ColumnLayout;

            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].Override.MinRowHeight = 20;
            this.LandValuedetailsDataGrid.DisplayLayout.Bands[0].Override.RowSelectorHeaderAppearance.BorderAlpha = Alpha.Transparent;
            // set the rows selection to none for avoid selecting multy rows at a time
            currentBand.Override.SelectTypeRow = SelectType.Single;

            //// set the schedule line item grid column display positions
            //this.SetLandValueGridColumnDisplayPositions();

            this.LandValuedetailsDataGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;

            // set the column visible position for active columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].Header.VisiblePosition = 1;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].Header.VisiblePosition = 2;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].Header.VisiblePosition = 3;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].Header.VisiblePosition = 4;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Header.VisiblePosition = 5;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Header.VisiblePosition = 6;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].Header.VisiblePosition = 7;

            //// set the column visible position for hidded columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Header.VisiblePosition = 8;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Header.VisiblePosition = 9;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Header.VisiblePosition = 10;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Header.VisiblePosition = 11;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Header.VisiblePosition = 12;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Header.VisiblePosition = 13;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Header.VisiblePosition = 14;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Header.VisiblePosition = 15;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Header.VisiblePosition = 16;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Header.VisiblePosition = 17;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.MrktMultiplierColumn.ColumnName].Header.VisiblePosition = 18;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseMultiplierColumn.ColumnName].Header.VisiblePosition = 19;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Header.VisiblePosition = 20;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.IsAglandColumn.ColumnName].Header.VisiblePosition = 21;

            // set the width for visible columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].Width = 91; //75
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].Width = 200; //200
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].Width = 85;  //95
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].Width = 85;  //95
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Width = 85; //95
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Width = 85;   //95
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].Width = 84; //95

            // set the width for non visible columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.MrktMultiplierColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseMultiplierColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.IsAglandColumn.ColumnName].Width = 0;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName].Width = 0;

            // set the hidden property to true for non visible columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.CropRateColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NonCropRateColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.MrktMultiplierColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseMultiplierColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.IsAglandColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName].Hidden = true;
            // set the cell appearance for active columns in grid
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;


            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Format = "#,##0.00";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Format = "#,##0.00";

            // set the cell activation for Line and Value column
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].CellActivation = Activation.NoEdit;

            // set the value column tabStop property to false
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.NBHDListColumn.ColumnName].Header.Caption = "Neighborhood";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].Header.Caption = "Land Code";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].Header.Caption = "Unit Type";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Header.Caption = "CR $/Unit";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Header.Caption = "NC $/Unit";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.SubClassColumn.ColumnName].Header.Caption = "Subclass";
            currentBand.Columns[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].Header.Caption = "Roll Year";


        }

        /// <summary>
        /// Sets the schedule line item grid column display positions.
        /// </summary>
        private void SetLandValueGridColumnDisplayPositions()
        {
            UltraGridBand currentBand = this.LandValuedetailsDataGrid.DisplayLayout.Bands[0];


        }

        private void CropValueTextBox_Validated(object sender, EventArgs e)
        {
            if (this.CropValueTextBox.DecimalTextBoxValue < 0)
            {
                this.CropValueTextBox.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                this.CropValueTextBox.ForeColor = System.Drawing.Color.Black;
            }

            //if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            //{
            //    if (this.CheckLandFieldsMaxLimitValidation(this.CropValueTextBox, this.CropValueTextBox, true, false))
            //    {
            //        return;
            //    }
            //}
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

        private void NonCropValueTextBox_Validated(object sender, EventArgs e)
        {

            //decimal noncropValue;
            //if (this.NonCropValueTextBox.DecimalTextBoxValue < 0)
            //{
            //    this.NonCropValueTextBox.Text = "0.00";
            //}
            //if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            //{
            //    if (this.CheckLandFieldsMaxLimitValidation(this.NonCropRateTextBox , this.NonCropRateTextBox , true, false))
            //    {
            //        return;
            //    }
            //}
        }

        private void NonCropValueTextBox_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.NonCropValueTextBox.Text))
            //{
            //    this.NonCropValueTextBox.Text = "0.00";
            //}
            if (this.NonCropValueTextBox.DecimalTextBoxValue < 0)
            {
                this.NonCropValueTextBox.Text = "0.00";
            }
        }

        private void CropRateTextBox_TextChanged(object sender, EventArgs e)
            {
            try
            {
                this.gridclickflag = false;
                if (!this.editcropValue)
                {
                    this.iscroprate = true;
                }
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void NonCropRateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                if (!this.editnonCropValue)
                {
                    this.isnoncroprate = true;
                }
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



    }
}
