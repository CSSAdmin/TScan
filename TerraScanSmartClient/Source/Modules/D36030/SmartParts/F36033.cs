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
// 14/09/2007       M.Vijayakumar      Created
// 05/05/2009       A.ShanmugaSundaram Modified to implement the CO:#7106
// 02/07/2009       M Sadha Shivudu    Implemented the TSCO # 1518 Land Values - Add formula, etc
// 18/09/2009       M Sadha Shivudu    Implemented the TSCO # 3825
// ***********************************************************************************************/

namespace D36030
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infragistics.Win.UltraWinCalcManager;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    #endregion Namespace

    /// <summary>
    /// F36033 Class file
    /// </summary>
    public partial class F36033 : BaseSmartPart
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
        /// used to store the savedLandCodeRollYear
        /// </summary>
        private string savedLandCodeRollYear = string.Empty;

        /// <summary>
        /// Used to store the landCodesValuesData details
        /// </summary>
        private F36033LandCodesValuesData landCodesValuesData = new F36033LandCodesValuesData();

        /// <summary>
        /// used to store the landCodeValuesDetailsGridDataTable
        /// </summary>
        private F36033LandCodesValuesData.ListLandCodeValueDetailsDataTable landCodeValuesDetailsGridDataTable = new F36033LandCodesValuesData.ListLandCodeValueDetailsDataTable();

        /// <summary>
        /// Used to store the listNeighborhoodTypeDataTable
        /// </summary>
        private F36033LandCodesValuesData.ListNeighborhoodTypeDataTable listNeighborhoodTypeDataTable = new F36033LandCodesValuesData.ListNeighborhoodTypeDataTable();

        /// <summary>
        /// Used to store the listLandCodeDataTable
        /// </summary>
        private F36033LandCodesValuesData.ListLandCodeDataTable listLandCodeDataTable = new F36033LandCodesValuesData.ListLandCodeDataTable();

        /// <summary>
        /// Usde to store the listUnitTypeDataTable
        /// </summary>
        private F36033LandCodesValuesData.ListUnitTypeDataTable listUnitTypeDataTable = new F36033LandCodesValuesData.ListUnitTypeDataTable();

        /// <summary>
        /// Used to store the listLandCodeComboBoxDataTableOnLoad
        /// </summary>
        private DataTable listLandCodeComboBoxDataTableOnLoad = new DataTable();

        /// <summary>
        /// Usde to store the listUnitTypeComboBoxDataTableOnLoad
        /// </summary>
        private DataTable listUnitTypeComboBoxDataTableOnLoad = new DataTable();

        /// <summary>
        /// controller F36033
        /// </summary>
        private F36033Controller form36033Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// additional Operation SmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// Used store the setButtonModeOnformLoad
        /// </summary>
        private bool setButtonModeOnformLoad;

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool avoidEditMode;

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool rollYearTextChanged;

        /// <summary>
        /// Used to store the currentRollYesr
        /// </summary>
        private string currentRollYear = string.Empty;

        /// <summary>
        /// used to store thelandCodeValuesGridSource
        /// </summary>
        private BindingSource landCodeValuesGridSource = new BindingSource();

        /// <summary>
        /// Flag for click on grid
        /// </summary>
        private bool gridclickflag = false;

        /// <summary>
        /// Instance variable to hold the max money field value
        /// </summary>
        private double maxMoneyFieldValue = 922337203685477.58;

        /// <summary>
        /// Instance variable to hold the min money field value
        /// </summary>
        private double minMoneyFieldValue = -922337203685477.58;

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
        /// flag LoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMaster PermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36033"/> class.
        /// </summary>
        public F36033()
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
        public F36033(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.LandCodeValuePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandCodeValuePictureBox.Height, this.LandCodeValuePictureBox.Width, tabText, red, green, blue);
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
        public F36033Controller Form36033Control
        {
            get { return this.form36033Control as F36033Controller; }
            set { this.form36033Control = value; }
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
                    this.LandCodedetailsDataGrid.Enabled = true;
                    this.LoadLandCodeDetailsGrid();
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
            if (this.form36033Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form36033Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form36033Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form36033Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form36033Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form36033Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form36033Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form36033Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            if (this.landUniqueId != -999)
            {
                this.additionalOperationSmartPart.KeyId = this.landUniqueId;
                additionalOperationCountEntity.AttachmentCount = this.form36033Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.landUniqueId, TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form36033Control.WorkItem.GetCommentsCount(this.ParentFormId, this.landUniqueId, TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
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

        /// <summary>
        /// To check max length of the Break and value per
        /// </summary>
        /// <returns>boolean value</returns>
        private bool CheckBreakMaxLength()
        {
            double breakMaxValue = 9999999.99;
            double valuePerMaxValue = 922337203685477.5807;

            double baseValue;
            double usebasevalue;
            double break1;
            double break1ValuePer;
            double break2;
            double break2ValuePer;
            double break3;
            double break3ValuePer;
            double break4;
            double break4ValuePer;

            double.TryParse(this.BaseValuePerTextBox.Text.Trim(), out baseValue);
            double.TryParse(this.UseValuePerTextBox.Text.Trim(), out usebasevalue);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1);
            double.TryParse(this.Break1ValuePerTextBox.Text.Trim(), out break1ValuePer);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2);
            double.TryParse(this.Break2ValuePerTextBox.Text.Trim(), out break2ValuePer);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3);
            double.TryParse(this.Break3ValuePerTextBox.Text.Trim(), out break3ValuePer);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4);
            double.TryParse(this.Break4ValuePerTextBox.Text.Trim(), out break4ValuePer);

            if (baseValue < valuePerMaxValue && usebasevalue < valuePerMaxValue && break1 <= breakMaxValue && break2 <= breakMaxValue && break3 <= breakMaxValue && break4 <= breakMaxValue && break1ValuePer < valuePerMaxValue && break2ValuePer < valuePerMaxValue && break3ValuePer < valuePerMaxValue && break4ValuePer < valuePerMaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                && !string.IsNullOrEmpty(this.BaseValuePerTextBox.Text.Trim())
                && !string.IsNullOrEmpty(this.UseValuePerTextBox.Text.Trim()))
            {
                ////This method is used to check the max length for decimal value text box
                if (this.CheckBreakMaxLength())
                {
                    // check for the max limit validation for the MrktMultiplier textBox
                    if (this.CheckMaxFieldValidation(this.MrktMultiplierTextBox))
                    {
                        return false;
                    }

                    // check for the max limit validation for the UseMultiplier textBox
                    if (this.CheckMaxFieldValidation(this.UseMultiplierTextBox))
                    {
                        return false;
                    }

                    this.landCodesValuesData.SaveLandCodeValueDetails.Rows.Clear();
                    F36033LandCodesValuesData.SaveLandCodeValueDetailsRow dr = this.landCodesValuesData.SaveLandCodeValueDetails.NewSaveLandCodeValueDetailsRow();
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

                    if (!string.IsNullOrEmpty(this.MrktMultiplierTextBox.Text.Trim()))
                    {
                        dr.MrktMultiplier = this.MrktMultiplierTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.UseMultiplierTextBox.Text.Trim()))
                    {
                        dr.UseMultiplier = this.UseMultiplierTextBox.DecimalTextBoxValue;
                    }

                    if (!string.IsNullOrEmpty(this.LandValueCurveFormulaTextBox.Text.Trim()))
                    {
                        dr.VFormula = this.LandValueCurveFormulaTextBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.BaseValuePerTextBox.Text.Trim()))
                    {
                        dr.BaseValue = this.BaseValuePerTextBox.DecimalTextBoxValue;
                    }

                    ////For UseValuePer

                    if (!string.IsNullOrEmpty(this.UseValuePerTextBox.Text.Trim()))
                    {
                        dr.UseBaseValue = this.UseValuePerTextBox.DecimalTextBoxValue;
                    }

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

                    this.landCodesValuesData.SaveLandCodeValueDetails.Rows.Add(dr);

                    if (this.ValidateBreakValue())
                    {
                        this.savedLandCodeRollYear = this.RollYearTextBox.Text.Trim();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.savedlandUniqueId = this.form36033Control.WorkItem.F36033_SaveLandCodeValue(null, TerraScanCommon.GetXmlString(this.landCodesValuesData.SaveLandCodeValueDetails.Copy()), TerraScanCommon.UserId);
                        }
                        else
                        {
                            this.savedlandUniqueId = this.form36033Control.WorkItem.F36033_SaveLandCodeValue(this.landUniqueId, TerraScanCommon.GetXmlString(this.landCodesValuesData.SaveLandCodeValueDetails.Copy()), TerraScanCommon.UserId);
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
                    MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// To Validate the Break values
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool ValidateBreakValue()
        {
            decimal currentBreakValue;
            decimal referBreakvalue;
            int currentColumnIndex;

            F36033LandCodesValuesData.CheckBreakValuesDataTable checkBreakValuesDataTable = new F36033LandCodesValuesData.CheckBreakValuesDataTable();

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
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit && !this.flagLoadOnProcess && !this.avoidEditMode && !this.gridclickflag)
            {
                this.LandCodedetailsDataGrid.Enabled = false;
                this.additionalOperationSmartPart.Enabled = false;
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
            this.BaseValuePerTextBox.LockKeyPress = controlLook;
            this.UseValuePerTextBox.LockKeyPress = controlLook;
            this.MrktMultiplierTextBox.LockKeyPress = controlLook;
            this.UseMultiplierTextBox.LockKeyPress = controlLook;
            this.LandValueCurveFormulaTextBox.LockKeyPress = controlLook;
            this.Break1TextBox.LockKeyPress = controlLook;
            this.Break1ValuePerTextBox.LockKeyPress = controlLook;
            this.Break2TextBox.LockKeyPress = controlLook;
            this.Break2ValuePerTextBox.LockKeyPress = controlLook;
            this.Break3TextBox.LockKeyPress = controlLook;
            this.Break3ValuePerTextBox.LockKeyPress = controlLook;
            this.Break4TextBox.LockKeyPress = controlLook;
            this.Break4ValuePerTextBox.LockKeyPress = controlLook;

            this.NeighborhoodComboBox.Enabled = !controlLook;
            this.LandCodeComboBox.Enabled = !controlLook;
            this.UnitTypeComboBox.Enabled = !controlLook;

            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.additionalOperationSmartPart.Enabled = false;
            }
            else
            {
                if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0)
                {
                    this.additionalOperationSmartPart.Enabled = !controlLook;
                }
                else
                {
                    this.additionalOperationSmartPart.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Used to Populate the Land Code header part controls
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void PopulateLandCodeHeaderPartControls(int rowIndex)
        {
            if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0 && rowIndex >= 0 && this.LandCodedetailsDataGrid.Rows.VisibleRowCount > 1 && rowIndex <= this.landCodeValuesDetailsGridDataTable.Rows.Count - 1)
            {
                this.avoidEditMode = true;

                int.TryParse(this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName].Value.ToString(), out this.landUniqueId);
                this.additionalOperationSmartPart.KeyId = this.landUniqueId;
                this.RollYearTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].Value.ToString();
                this.LoadComBoBoxBasedOnRollYear();
                ////this.currentRollYear = this.landCodeValuesDetailsGridDataTable.Rows[rowIndex][this.landCodeValuesDetailsGridDataTable.RollYearColumn].ToString();
                this.PopulateAllLandCodeComboBoxValues(this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UnitTypeColumn.ColumnName].Value.ToString(), this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.LandCodeColumn.ColumnName].Value.ToString(), this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.NBHDIDColumn.ColumnName].Value.ToString());
                this.MrktMultiplierTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.MrktMultiplierColumn.ColumnName].Value.ToString();
                this.UseMultiplierTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UseMultiplierColumn.ColumnName].Value.ToString();
                this.LandValueCurveFormulaTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.VFormulaColumn.ColumnName].Value.ToString();
                this.BaseValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.BaseValueColumn.ColumnName].Value.ToString();
                this.Break1TextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break1Column.ColumnName].Value.ToString();
                this.Break1ValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value1Column.ColumnName].Value.ToString();
                this.Break2TextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break2Column.ColumnName].Value.ToString();
                this.Break2ValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value2Column.ColumnName].Value.ToString();
                this.Break3TextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break3Column.ColumnName].Value.ToString();
                this.Break3ValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value3Column.ColumnName].Value.ToString();
                this.Break4TextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Break4Column.ColumnName].Value.ToString();
                this.Break4ValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.Value4Column.ColumnName].Value.ToString();

                ////For New Construction
                this.UseValuePerTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.landCodeValuesDetailsGridDataTable.UseBaseValueColumn.ColumnName].Value.ToString();

                this.LandCodeHeaderPanel.Enabled = true;
                this.AttachmentCommentpanel.Enabled = true;

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.avoidEditMode = false;
            }
            else
            {
                this.ClearLandCodeHeaderControls();
                this.landUniqueId = -999;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.AttachmentCommentpanel.Enabled = false;
                this.LandCodeHeaderPanel.Enabled = false;
            }

            this.SetAttachmentCommentsCount();
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
            this.MrktMultiplierTextBox.Text = string.Empty;
            this.UseMultiplierTextBox.Text = string.Empty;
            this.LandValueCurveFormulaTextBox.Text = string.Empty;
            this.BaseValuePerTextBox.Text = string.Empty;
            this.Break1TextBox.Text = string.Empty;
            this.Break1ValuePerTextBox.Text = string.Empty;
            this.Break2TextBox.Text = string.Empty;
            this.Break2ValuePerTextBox.Text = string.Empty;
            this.Break3TextBox.Text = string.Empty;
            this.Break3ValuePerTextBox.Text = string.Empty;
            this.Break4TextBox.Text = string.Empty;
            this.Break4ValuePerTextBox.Text = string.Empty;
            this.UseValuePerTextBox.Text = string.Empty;
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
        /// To Load the land code details grid.
        /// </summary>
        private void LoadLandCodeDetailsGrid()
        {
            this.flagLoadOnProcess = true;

            this.landCodesValuesData.Clear();
            this.listNeighborhoodTypeDataTable.Clear();
            this.listUnitTypeDataTable.Clear();
            this.listLandCodeDataTable.Clear();
            this.listUnitTypeComboBoxDataTableOnLoad.Clear();
            this.listLandCodeComboBoxDataTableOnLoad.Clear();

            this.CustimizeAllLandCodeComboBoxs();

            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].Columns[this.landCodesValuesData.ListLandCodeValueDetails.BaseValueColumn.ColumnName].MaskInput = "nnnnnnnnnnnnnnn.nn";

            ////to load the Land code individual values
            this.landCodesValuesData = this.form36033Control.WorkItem.F36033_ListIndividualLandCodeValuesItems();
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
            this.landCodesValuesData = this.form36033Control.WorkItem.F36033_ListLandCodeValues();
            this.landCodeValuesDetailsGridDataTable = this.landCodesValuesData.ListLandCodeValueDetails;
            this.LandCodedetailsDataGrid.DataSource = this.landCodeValuesDetailsGridDataTable;

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
                    this.LandCodedetailsDataGrid.Rows[0].Selected = true;

                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                    {
                        this.additionalOperationSmartPart.Enabled = this.PermissionFiled.editPermission;
                    }
                    else
                    {
                        this.additionalOperationSmartPart.Enabled = false;
                    }
                }
            }
            else
            {
                this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
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
            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters[this.landCodeValuesDetailsGridDataTable.RollYearColumn.ColumnName].FilterConditions.Add(FilterComparisionOperator.StartsWith, filterByRollYear);
            UltraGridRow[] filteredRow = this.LandCodedetailsDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                filteredRow[0].Activated = true;
                filteredRow[0].Selected = true;

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.additionalOperationSmartPart.Enabled = this.PermissionFiled.editPermission;
                }
                else
                {
                    this.additionalOperationSmartPart.Enabled = false;
                }
            }
            else
            {
                this.ClearLandCodeGriddetails();
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
            this.LandCodeHeaderPanel.Enabled = false;
            this.additionalOperationSmartPart.Enabled = false;
            this.additionalOperationSmartPart.Controls[0].Text = "Attachment";
            this.additionalOperationSmartPart.Controls[1].Text = "Comment";
        }

        /// <summary>
        /// Loads the All Combo Box based on roll year.
        /// </summary>
        private void LoadComBoBoxBasedOnRollYear()
        {
            int temprollyear;
            ////for current roll year text is used to select DISTINCT  value for perticular roll year
            F36033LandCodesValuesData.ListLandCodeDataTable tempListlandCodeDataTable = new F36033LandCodesValuesData.ListLandCodeDataTable();

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
            this.landCodesValuesData = this.form36033Control.WorkItem.F36033_ListNeighborhoodType(temprollyear);

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
        /// Checks the max field validation.
        /// </summary>
        /// <param name="validatedTextBox">The validated text box.</param>
        /// <returns>The validation status</returns>
        private bool CheckMaxFieldValidation(TerraScanTextBox validatedTextBox)
        {
            double maxFildValue = 999.99;
            double validatedTextBoxValue;
            double.TryParse(validatedTextBox.DecimalTextBoxValue.ToString(), out validatedTextBoxValue);
            bool valueExceeded = false;

            if (validatedTextBoxValue > maxFildValue)
            {
                valueExceeded = true;
            }

            if (valueExceeded)
            {
                MessageBox.Show("Value exceeded the max limit.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                validatedTextBox.Text = decimal.Zero.ToString();
                validatedTextBox.Focus();
                return true;
            }

            return false;
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

        #endregion Methods

        #region Events

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36033 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36033_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.LoadLandCodeDetailsGrid();
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

        /// <summary>
        /// Handles the MouseHover event of the LandCodeValuePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodeValuePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandCodeValuesFormSliceToolTip.SetToolTip(this.LandCodeValuePictureBox, Utility.GetFormNameSpace(this.Name));
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
        private void LandCodeValuePictureBox_Click(object sender, EventArgs e)
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
        /// Handles the BeforeRowActivate event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void LandCodedetailsDataGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.PopulateLandCodeHeaderPartControls(e.Row.Index);
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
                        int deletedReturnedValue = this.form36033Control.WorkItem.F36033_DeleteLandCodevalue(this.landUniqueId, TerraScanCommon.UserId);

                        ////when the land code is deleted then reload the form
                        if (deletedReturnedValue > 0)
                        {
                            this.LoadLandCodeDetailsGrid();
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
        /// Saves the button_ click.
        /// </summary>
        private void SaveButtonClick()
        {
            try
            {
                int currentRowIndexValue;
                if (this.SaveLandCodeValues())
                {
                    this.LandCodedetailsDataGrid.Enabled = true;
                    this.LoadLandCodeDetailsGrid();
                    this.landCodeValuesGridSource.DataSource = this.landCodeValuesDetailsGridDataTable.DefaultView;
                    ////Added by Biju on 19/Nov/2009 to fix issue #4779
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (this.landCodeValuesDetailsGridDataTable.Rows.Count > 0)
                    {
                        this.FilterLandCodeGridRows(this.savedLandCodeRollYear);

                        currentRowIndexValue = this.landCodeValuesGridSource.Find(this.landCodeValuesDetailsGridDataTable.LuVIDColumn.ColumnName, this.savedlandUniqueId);
                        if (currentRowIndexValue >= 0)
                        {
                            this.PopulateLandCodeHeaderPartControls(currentRowIndexValue);
                            this.LandCodedetailsDataGrid.Rows[currentRowIndexValue].Selected = true;
                            this.LandCodedetailsDataGrid.Rows[currentRowIndexValue].Activated = true;
                            this.LandCodedetailsDataGrid.Focus();
                        }
                    }
                    ////Commented by Biju on 19/Nov/2009 to fix issue #4779
                    ////this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButtonClick()
        {
            try
            {
                this.LandCodedetailsDataGrid.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.LoadLandCodeDetailsGrid();
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
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.RollYearTextBox.Text = string.Empty;
                ////combo box value are loaded based on the Roll year
                this.LoadComBoBoxBasedOnRollYear();
                this.ControlLock(!this.PermissionFiled.newPermission);
                this.LandCodeHeaderPanel.Enabled = true;
                this.ClearLandCodeHeaderControls();
                this.BaseValuePerTextBox.Text = decimal.Zero.ToString();
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.SetAttachmentCommentsCount(); 
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
                    this.LoadComBoBoxBasedOnRollYear();
                    this.rollYearTextChanged = false;
                }
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

        /// <summary>
        /// Handles the Click event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodedetailsDataGrid_Click(object sender, EventArgs e)
        {
            this.gridclickflag = true;
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
        /// Handles the Validated event of the BaseValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseValuePerTextBox_Validated(object sender, EventArgs e)
        {
            if (this.BaseValuePerTextBox.DecimalTextBoxValue < 0)
            {
                this.BaseValuePerTextBox.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                this.BaseValuePerTextBox.ForeColor = System.Drawing.Color.Black;
            }

            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                if (this.CheckLandFieldsMaxLimitValidation(this.BaseValuePerTextBox, this.BaseValuePerTextBox, true, true))
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Handles the Validated event of the UseValuePerTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseValuePerTextBox_Validated(object sender, EventArgs e)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                if (this.CheckLandFieldsMaxLimitValidation(this.UseValuePerTextBox, this.UseValuePerTextBox, true, false))
                {
                    return;
                }
            }
        }

        #endregion Events

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
    }
}
