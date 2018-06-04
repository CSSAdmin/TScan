//--------------------------------------------------------------------------------------------
// <copyright file="F27080.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27080.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/09/2007      A.Sriparameswari     Created
// 17/03/2017      Dhineshkumar.J       21734 - D22080.F27080 Exemption Definition - Multiple changes
//***********************************************************************************************/


namespace D22080
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
    using Infrastructure.Interface;


    /// <summary>
    ///  F27080
    /// </summary>
    public partial class F27080 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// F27080ExemptionDefinitionData
        /// </summary>
        private F27080ExemptionDefinitionData exemptionData = new F27080ExemptionDefinitionData();

        /// <summary>
        /// form15018Control Controller
        /// </summary>
        private F27080Controller form27080Control;

        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int keyId;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store the Grid rows count
        /// </summary>
        private int exemptionGridRowCount;

        /// <summary>
        /// To store Whether Save is performed 
        /// </summary>
        private bool saveStatus;

        /// <summary>
        /// emptyRowAvailable
        /// </summary>
        private bool emptyRowAvailable = true;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// chkNewVal
        /// </summary>
        private bool chkNewVal = false;

        /// <summary>
        /// changedText
        /// </summary>
        private string changedText;

        /// <summary>
        /// isDeleteRow
        /// </summary>
        private bool isDeleteRow = true;

        /// <summary>
        /// sliceVal
        /// </summary>
        private bool sliceVal;

        /// <summary>
        /// hasValues
        /// </summary>
        private bool hasRowValues;

        /// <summary>
        /// isRowHeaderClick
        /// </summary>
        private bool isRowHeaderClick = false;

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// isrowDeleted
        /// </summary>
        private bool isrowDeleted = false;

        private string stateConfigured = string.Empty;

        /// <summary>
        /// Used to store the saveValidateErrorMessage
        /// </summary>
        private string saveValidateErrorMessage = string.Empty;

        F2550TaxRollCorrectionData.ConfiguredStateDataTable StateDetailsTable = new F2550TaxRollCorrectionData.ConfiguredStateDataTable();

        #endregion

        #region Constructors

        /// <summary>
        /// F27080
        /// </summary>
        public F27080()
        {
            InitializeComponent();
            this.saveStatus = true;
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
        public F27080(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, string.Empty, red, green, blue);
            this.ExemptionGridpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ExemptionGridpictureBox.Height, this.ExemptionGridpictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

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

        #region Properties

        /// <summary>
        /// Gets or sets the F27000 control.
        /// </summary>
        /// <value>The F27000 control.</value>
        [CreateNew]
        public F27080Controller Form27080Control
        {
            get { return this.form27080Control as F27080Controller; }
            set { this.form27080Control = value; }
        }

        #endregion

        #region Event Subscriptions

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
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (!this.IsDisposed)
                    {
                        if ((this.exemptionData.GridLoadExemptionTypeTable.Rows.Count > 0) && (this.exemptionData.GetSeniorExemptionTypeTable.Rows.Count > 0))
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
                            ExcemptionTypeComboBox.SelectedIndex = -1;
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
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (!this.slicePermissionField.newPermission)
            {
                this.ClearControl();
                this.ControlLock(true);
                this.IrrigatedBaseType3Panel.Visible = false;
                this.ExemptionGridpictureBox.Visible = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
            else
            {
                ////setting the pagemode
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                ////this.exemptionData.Clear(); 
                this.ExcemptionTypeComboBox.Enabled = true;
                this.ExcemptionTypePanel.Enabled = true;
                ExcemptionTypePanel.Focus();
                this.ExcemptionTypeComboBox.Focus();
                this.RollYearPanel.Enabled = true;
                this.RollYearTextBox.Enabled = true;
                this.exemptionData.Clear();
                this.ExcemptionTypeComboBox.SelectedValue = 0;

                CommentsData.GetCommentsConfigDetailsDataTable commentsConfigDetailsDataTable = this.form27080Control.WorkItem.GetConfigDetails("AA_RollYear").GetCommentsConfigDetails;
                if (commentsConfigDetailsDataTable.Rows.Count > 0)
                {
                    this.RollYearTextBox.Text = commentsConfigDetailsDataTable.Rows[0][commentsConfigDetailsDataTable.ConfigurationValueColumn.ColumnName].ToString();
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }
                //Added to determine state
                this.LoadConfiguredState();
                this.IrrigatedBaseType3Panel.Visible = false;
                this.ExemptionGridView.Visible = false;
                this.ExemptionGridpictureBox.Visible = false;

                this.chkNewVal = true;
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
            if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
            {
                this.CustomizeNEStateGridView();
            }
            else
            {
                this.CustomizeExemptionGridView();
            }
            //this.CustomizeExemptionGridView();
            this.FlagSliceForm = true;
            this.LoadComboBox();
            this.LoadDataGrid();
            this.flagLoadOnProcess = false;
            this.RollYearTextBox.Focus();
            this.chkNewVal = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.isDeleteRow = true;
            }
            #region BugID 5599

            ////To Remove Focus from DataGridView while pressing ESC key---- Ramya.D
            this.ExemptionGridView.CurrentCell = null;

            #endregion
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
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
            if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
            {
                this.saveStatus = true;
                this.SaveExemptionDefinition();
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
                    if (this.emptyRowAvailable)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        this.flagLoadOnProcess = true;
                        this.LoadComboBox();
                        this.LoadDataGrid();
                        this.flagLoadOnProcess = false;
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
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

        #endregion

        #region CommonMethods

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

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                isDeleteRow = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                if (!this.chkNewVal)
                {
                    this.chkNewVal = false;
                }
            }
            else if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.chkNewVal = true;
            }
        }

        /// <summary>
        /// Checks the excempion code max min value.
        /// </summary>
        /// <param name="validateDataTable">The validate data table.</param>
        /// <returns>bool</returns>
        private bool CheckExcempionCodeMaxMinValue(F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable validateDataTable)
        {
            decimal minValue;
            decimal maxvalue;
            this.saveValidateErrorMessage = string.Empty;
            for (int i = 0; i < validateDataTable.Rows.Count; i++)
            {
                //// Condition is added for checking whether Min and max values are empty or not --- Ramya.D
                if (!string.IsNullOrEmpty(validateDataTable.Rows[i]["ExemptionCode"].ToString().Trim()) && !string.IsNullOrEmpty(validateDataTable.Rows[i]["ExemptionPercent"].ToString().Trim()))
                {
                    string excemptionCode = validateDataTable.Rows[i]["ExemptionCode"].ToString().Trim().Replace("'", "");

                    string excemptionCodeDistinctFilter = string.Empty;
                    if (!string.IsNullOrEmpty(excemptionCode))
                    {
                        excemptionCodeDistinctFilter = "ExemptionCode = '" + excemptionCode + "'";
                    }

                    DataRow[] drfilterCodeCondtion1 = validateDataTable.Select(excemptionCodeDistinctFilter);

                    string excemptionMinMax = "ValueChangeMinimum IS NOT NULL And ValueChangeMaximum IS NULL";

                    DataRow[] drexcemptionMinMax = validateDataTable.Select(excemptionMinMax);

                    if (drfilterCodeCondtion1.Length > 1 || drexcemptionMinMax.Length > 0)
                    {
                        if (drexcemptionMinMax.Length > 0)
                        {
                            this.saveValidateErrorMessage = "ValueChangeMaximum should Greater Than Minimum.";
                        }

                        if (drfilterCodeCondtion1.Length > 1)
                        {
                            if (!string.IsNullOrEmpty(this.saveValidateErrorMessage))
                            {
                                this.saveValidateErrorMessage = this.saveValidateErrorMessage + " and Code should be unique";
                            }
                            else
                            {
                                this.saveValidateErrorMessage = "Identical code cannot exist for the same Exemption in the same roll year";
                            }
                        }

                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(validateDataTable.Rows[i]["ValueChangeMinimum"].ToString().Trim()) || !string.IsNullOrEmpty(validateDataTable.Rows[i]["ValueChangeMaximum"].ToString().Trim()))
                        {
                            decimal.TryParse(validateDataTable.Rows[i]["ValueChangeMinimum"].ToString().Trim(), out minValue);
                            decimal.TryParse(validateDataTable.Rows[i]["ValueChangeMaximum"].ToString().Trim(), out maxvalue);

                            if (minValue != 0)
                            {
                                if (maxvalue <= minValue)
                                {
                                    this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                    return false;
                                }
                            }

                            if (maxvalue != 0)
                            {
                                if (maxvalue <= minValue)
                                {
                                    this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                    return false;
                                }
                            }

                            if (minValue == 0 || minValue == 0)
                            {
                                if (maxvalue > 0 || minValue > 0)
                                {
                                    if (maxvalue <= minValue)
                                    {
                                        this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.saveValidateErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks the excempion code max min value.
        /// </summary>
        /// <param name="validateDataTable">The validate data table.</param>
        /// <returns>bool</returns>
        private bool CheckExcempionCodeIncomeMaxMinValue(F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable validateDataTable)
        {
            decimal IncomeminValue;
            decimal IncomemaxValue;
            this.saveValidateErrorMessage = string.Empty;
            for (int i = 0; i < validateDataTable.Rows.Count; i++)
            {
                //// Condition is added for checking whether Min and max values are empty or not --- Ramya.D
                if (!string.IsNullOrEmpty(validateDataTable.Rows[i]["ExemptionCode"].ToString().Trim()) && !string.IsNullOrEmpty(validateDataTable.Rows[i]["ExemptionPercent"].ToString().Trim()))
                {
                    string excemptionCode = validateDataTable.Rows[i]["ExemptionCode"].ToString().Trim().Replace("'", "");

                    string excemptionCodeDistinctFilter = string.Empty;
                    if (!string.IsNullOrEmpty(excemptionCode))
                    {
                        excemptionCodeDistinctFilter = "ExemptionCode = '" + excemptionCode + "'";
                    }

                    DataRow[] drfilterCodeCondtion1 = validateDataTable.Select(excemptionCodeDistinctFilter);

                    string excemptionMinMax = "IncomeMin IS NOT NULL And IncomeMax IS NULL";

                    DataRow[] drexcemptionMinMax = validateDataTable.Select(excemptionMinMax);

                    if (drfilterCodeCondtion1.Length > 1 || drexcemptionMinMax.Length > 0)
                    {
                        if (drexcemptionMinMax.Length > 0)
                        {
                            this.saveValidateErrorMessage = "IncomeMax should Greater Than Minimum.";
                        }

                        if (drfilterCodeCondtion1.Length > 1)
                        {
                            if (!string.IsNullOrEmpty(this.saveValidateErrorMessage))
                            {
                                this.saveValidateErrorMessage = this.saveValidateErrorMessage + " and Code should be unique";
                            }
                            else
                            {
                                this.saveValidateErrorMessage = "Identical code cannot exist for the same Exemption in the same roll year";
                            }
                        }

                        return false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(validateDataTable.Rows[i]["IncomeMin"].ToString().Trim()) || !string.IsNullOrEmpty(validateDataTable.Rows[i]["IncomeMax"].ToString().Trim()))
                        {
                            decimal.TryParse(validateDataTable.Rows[i]["IncomeMin"].ToString().Trim(), out IncomeminValue);
                            decimal.TryParse(validateDataTable.Rows[i]["IncomeMax"].ToString().Trim(), out IncomemaxValue);

                            if (IncomeminValue != 0)
                            {
                                if (IncomemaxValue <= IncomeminValue)
                                {
                                    this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                    return false;
                                }
                            }

                            if (IncomemaxValue != 0)
                            {
                                if (IncomemaxValue <= IncomeminValue)
                                {
                                    this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                    return false;
                                }
                            }

                            if (IncomeminValue == 0 || IncomeminValue == 0)
                            {
                                if (IncomemaxValue > 0 || IncomeminValue > 0)
                                {
                                    if (IncomemaxValue <= IncomeminValue)
                                    {
                                        this.saveValidateErrorMessage = "Maximum should Greater Than Minimum.";
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.saveValidateErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// CheckErrors
        /// </summary>
        /// <param name="formNo">formNo</param>
        /// <returns>bool</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            bool requiredField;
            try
            {
                string filterCondtions = "((ExemptionCode IS  NULL or ExemptionCode = '') AND  (ExemptionPercent IS NULL or ExemptionPercent = '' ) AND (Description IS  NULL OR Description='')AND (ValueChangeMinimum IS  NULL OR ValueChangeMinimum ='')AND (ValueChangeMaximum IS  NULL OR ValueChangeMaximum ='')AND (IncomeMin IS  NULL OR IncomeMin ='') AND (IncomeMax IS  NULL OR IncomeMax ='') AND (AbstractCode IS NULL OR AbstractCode=''))";
                DataRow[] drfilterCondtions = this.exemptionData.GridLoadExemptionTypeTable.Select(filterCondtions);
                if (drfilterCondtions.Length == 12)
                {
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                    this.RollYearTextBox.Focus();
                    return sliceValidationFields;
                }
            }
            catch (Exception ex)
            {
            }
            if ((this.exemptionData.GridLoadExemptionTypeTable.Rows.Count <= 0) || (ExcemptionTypeComboBox.SelectedIndex == 0))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                this.RollYearTextBox.Focus();
                return sliceValidationFields;
            }


            int yearField;
            int.TryParse(this.RollYearTextBox.Text, out yearField);

            if ((string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim())) || yearField == 0)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                #region BugID 5599(1)

                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));

                #endregion
                this.RollYearTextBox.Focus();
                return sliceValidationFields;
            }
            else
            {
                string filterCondtion1 = "((ExemptionCode IS  NULL or ExemptionCode = '' or ExemptionPercent IS NULL or ExemptionPercent = '') AND (ExemptionCode IS NOT NULL OR Description IS NOT NULL OR ExemptionPercent IS NOT NULL OR ValueChangeMinimum IS NOT NULL OR ValueChangeMaximum IS NOT NULL OR  IncomeMin IS NOT NULL OR IncomeMax IS NOT NULL OR AbstractCode IS NOT NULL ))";
                DataRow[] drfilterCondtion1 = this.exemptionData.GridLoadExemptionTypeTable.Select(filterCondtion1);

                if (drfilterCondtion1.Length > 0)
                {
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                    this.RollYearTextBox.Focus();
                    return sliceValidationFields;
                }
                else
                {
                    string filterCondtion2 = "((ExemptionCode IS NOT NULL OR Description IS NOT NULL OR ExemptionPercent IS NOT NULL OR ValueChangeMinimum IS NOT NULL OR ValueChangeMaximum IS NOT NULL OR IncomeMin IS NOT NULL OR IncomeMax IS NOT NULL OR AbstractCode IS NOT NULL ))";
                    DataRow[] drfilterCondtion2 = this.exemptionData.GridLoadExemptionTypeTable.Select(filterCondtion2);

                    F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable checkGridLoadExemptionTypeTableDataTable = new F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable();
                    foreach (DataRow currentValidateRow in drfilterCondtion2)
                    {
                        checkGridLoadExemptionTypeTableDataTable.ImportRow(currentValidateRow);
                    }

                    if (!this.CheckExcempionCodeMaxMinValue(checkGridLoadExemptionTypeTableDataTable))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = this.saveValidateErrorMessage;
                        this.RollYearTextBox.Focus();
                        return sliceValidationFields;
                    }
                    if (!this.CheckExcempionCodeIncomeMaxMinValue(checkGridLoadExemptionTypeTableDataTable))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = this.saveValidateErrorMessage;
                        this.RollYearTextBox.Focus();
                        return sliceValidationFields;
                    }
                }
            }

            string excemptionType;
            string elementItems = string.Empty;
            int checkError;
            this.ExemptionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.exemptionData.GridLoadExemptionTypeTable.AcceptChanges();
            elementItems = TerraScanCommon.GetXmlString(this.exemptionData.GridLoadExemptionTypeTable);
            excemptionType = "<Root><Table><ExemptionTypeID>" + this.ExcemptionTypeComboBox.SelectedValue.ToString() + "</ExemptionTypeID></Table><Table><RollYear>" + this.RollYearTextBox.Text + "</RollYear></Table></Root>";
            checkError = this.form27080Control.WorkItem.F27080_SaveExemptionType(0, elementItems, excemptionType, 0, TerraScanCommon.UserId);

            if (checkError == -1)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("identicalRecord"));
                ExemptionGridView.DataSource = this.exemptionData.GridLoadExemptionTypeTable;
                return sliceValidationFields;
            }

            return sliceValidationFields;
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
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            this.ExemptionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
        }

        /// <summary>
        /// Handles the TextChanged event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            string textVal = sender.ToString();
            string[] textValChanged = null;
            textValChanged = textVal.Split(',');
            if (textVal.Length > 1)
            {
                string Text = textValChanged[1].Trim();
                textValChanged = Text.Split(':');
                if (Text.Length > 0)
                {
                    changedText = textValChanged[1].Trim();
                }
            }

            if (!string.IsNullOrEmpty(changedText))
            {
                string minValue = this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["Minimum"].Value.ToString();
                string maxValue = this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["Maximum"].Value.ToString();
                string minIncomeValue = this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["IncomeMin"].Value.ToString();
                string maxIncomeValue = this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["IncomeMax"].Value.ToString();

                if (string.IsNullOrEmpty(minValue) && string.IsNullOrEmpty(maxValue) && string.IsNullOrEmpty(minIncomeValue) && string.IsNullOrEmpty(maxIncomeValue))
                {
                    this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["Minimum"].Value = 0;
                    this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["Maximum"].Value = 0;
                    this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["IncomeMin"].Value = 0;
                    this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentCell.RowIndex].Cells["IncomeMax"].Value = 0;
                    this.EditEnabled();
                }
            }

            ////to enable the save cancel button in form master
            if (!isrowDeleted && !isRowHeaderClick)
            {
                this.ToEnableEditButtonInMasterForm();
            }

            if ((this.ExemptionGridView.CurrentCell.RowIndex + 1) == this.ExemptionGridView.Rows.Count && this.ExemptionGridView.CurrentCell.ColumnIndex >= 0)
            {
                this.exemptionData.GridLoadExemptionTypeTable.Rows.InsertAt(this.exemptionData.GridLoadExemptionTypeTable.NewGridLoadExemptionTypeTableRow(), this.ExemptionGridView.Rows.Count);
            }

            if (this.exemptionData.GridLoadExemptionTypeTable.Rows.Count > this.ExemptionGridView.NumRowsVisible)
            {
                this.ExemptionGridVscrollBar.Visible = false;
                //this.ExemptionGridVscrollBar.Enabled = false;
            }
            else
            {
                this.ExemptionGridVscrollBar.Visible = true;
                this.ExemptionGridVscrollBar.Enabled = false;
            }
        }


        private void LoadConfiguredState()
        {
            this.StateDetailsTable = this.form27080Control.WorkItem.F2550_GetConfiguredState().ConfiguredState;
            if (this.StateDetailsTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.StateDetailsTable.Rows[0][this.StateDetailsTable.StateColumn].ToString()))
                {
                    this.stateConfigured = this.StateDetailsTable.Rows[0][this.StateDetailsTable.StateColumn].ToString();
                }
            }

        }

        #endregion

        /// <summary>
        /// F27080_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F27080_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadConfiguredState();
                if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToUpper().Equals("NE"))
                {
                    this.CustomizeNEStateGridView();
                }
                else
                {
                    this.CustomizeExemptionGridView();
                }
                this.exemptionData = this.form27080Control.WorkItem.F27080_FillExemptionTypeGrid(this.keyId);
                if (this.exemptionData.GridLoadExemptionTypeTable.Rows.Count > 0)
                {

                    this.LoadComboBox();
                    this.LoadDataGrid();
                    this.ExcemptionTypeComboBox.SelectedIndex = 1;
                    this.ExcemptionTypePanel.Enabled = false;
                    this.ExcemptionTypeComboBox.Enabled = false;
                    this.RollYearTextBox.Enabled = false;
                    this.flagLoadOnProcess = false;
                    this.RollYearTextBox.Focus();
                }
                else
                {
                    this.LoadComboBox();
                    this.ExcemptionTypePanel.Enabled = false;
                    this.ExcemptionTypeComboBox.Enabled = false;
                    this.RollYearPanel.Enabled = false;
                    this.RollYearTextBox.Enabled = false;
                    this.ExemptionGridView.Visible = false;
                    this.IrrigatedBaseType3Panel.Visible = false;
                    this.ExemptionGridpictureBox.Visible = false;
                }

                if (!this.PermissionFiled.editPermission)
                {
                    this.LoadComboBox();
                    this.ControlLock(true);
                    this.RollYearTextBox.Enabled = false;
                    this.ExcemptionTypeComboBox.Enabled = false;
                    this.ExemptionGridView.ReadOnly = true;
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

        #region Producted Methods

        /// <summary>
        /// LoadWithExemptionTypeComboValue
        /// </summary>
        private void LoadWithExemptionTypeComboValue()
        {
            ExemptionGridView.DataSource = this.exemptionData.GridLoadExemptionTypeTable.DefaultView;
            this.ExemptionGridView.Visible = true;
        }

        /// <summary>
        /// This Method used to load combobox datasource
        /// LoadComboBox
        /// </summary>
        private void LoadComboBox()
        {
            F27080ExemptionDefinitionData exemptionData = new F27080ExemptionDefinitionData();
            exemptionData = this.form27080Control.WorkItem.F27080_ListExemptionTypeCombo(TerraScanCommon.ApplicationId);
            this.ExcemptionTypeComboBox.DataSource = exemptionData.ListExemptionTypeTable.Copy();
            this.ExcemptionTypeComboBox.DisplayMember = exemptionData.ListExemptionTypeTable.ExemptionTypeColumn.ToString();
            this.ExcemptionTypeComboBox.ValueMember = exemptionData.ListExemptionTypeTable.ExemptionTypeIDColumn.ToString();
        }

        /// <summary>
        /// CustomizeExemptionGridView
        /// </summary>
        private void CustomizeExemptionGridView()
        {
            this.ExemptionGridView.AutoGenerateColumns = false;

            F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable exemptionGridTable = this.exemptionData.GridLoadExemptionTypeTable;


            this.ExemptionGridView.Columns[exemptionGridTable.DescriptionColumn.ColumnName].Visible = false;
            this.ExemptionGridView.Columns[exemptionGridTable.AbstractCodeColumn.ColumnName].Visible = true;

            this.ExemptionCode.DataPropertyName = exemptionGridTable.ExemptionCodeColumn.ColumnName;
            this.Description.DataPropertyName = exemptionGridTable.DescriptionColumn.ColumnName;
            this.ExemptionPercent.DataPropertyName = exemptionGridTable.ExemptionPercentColumn.ColumnName;
            this.Minimum.DataPropertyName = exemptionGridTable.ValueChangeMinimumColumn.ColumnName;
            this.Maximum.DataPropertyName = exemptionGridTable.ValueChangeMaximumColumn.ColumnName;
            this.IncomeMin.DataPropertyName = exemptionGridTable.IncomeMinColumn.ColumnName;
            this.IncomeMax.DataPropertyName = exemptionGridTable.IncomeMaxColumn.ColumnName;
            this.AbstractCode.DataPropertyName = exemptionGridTable.AbstractCodeColumn.ColumnName;
            this.ExemptionGridView.DataSource = exemptionGridTable.DefaultView;

            this.ExemptionGridView.Columns[exemptionGridTable.AbstractCodeColumn.ColumnName].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        }


        private void CustomizeNEStateGridView()
        {
            this.ExemptionGridView.AutoGenerateColumns = false;

            F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable exemptionGridTable = this.exemptionData.GridLoadExemptionTypeTable;

            this.ExemptionGridView.Columns[exemptionGridTable.DescriptionColumn.ColumnName].Visible = true;
            this.ExemptionGridView.Columns[exemptionGridTable.AbstractCodeColumn.ColumnName].Visible = false;

            this.ExemptionCode.DataPropertyName = exemptionGridTable.ExemptionCodeColumn.ColumnName;
            this.Description.DataPropertyName = exemptionGridTable.DescriptionColumn.ColumnName;
            this.ExemptionPercent.DataPropertyName = exemptionGridTable.ExemptionPercentColumn.ColumnName;
            this.Minimum.DataPropertyName = exemptionGridTable.ValueChangeMinimumColumn.ColumnName;
            this.Maximum.DataPropertyName = exemptionGridTable.ValueChangeMaximumColumn.ColumnName;
            this.IncomeMin.DataPropertyName = exemptionGridTable.IncomeMinColumn.ColumnName;
            this.IncomeMax.DataPropertyName = exemptionGridTable.IncomeMaxColumn.ColumnName;
            this.AbstractCode.DataPropertyName = exemptionGridTable.AbstractCodeColumn.ColumnName;
            this.ExemptionGridView.DataSource = exemptionGridTable.DefaultView;
            //this.ExemptionGridView.Columns[exemptionGridTable.IncomeMaxColumn.ColumnName].AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        }

        /// <summary>
        /// LoadDataGrid()
        /// </summary>
        private void LoadDataGrid()
        {
            this.ExemptionGridView.Visible = true;
            this.IrrigatedBaseType3Panel.Visible = true;
            this.ExemptionGridpictureBox.Visible = true;
            this.ExemptionGridView.AutoGenerateColumns = false;
            this.exemptionData = this.form27080Control.WorkItem.F27080_FillExemptionTypeGrid(this.keyId);
            this.exemptionGridRowCount = this.exemptionData.GridLoadExemptionTypeTable.Rows.Count;
            if (this.exemptionData.GridLoadExemptionTypeTable.Rows.Count > 0)
            {
                this.exemptionData = this.form27080Control.WorkItem.F27080_FillExemptionTypeGrid(this.keyId);
                this.ExcemptionTypePanel.Enabled = true;
                this.ExcemptionTypeComboBox.Enabled = true;
                this.ExcemptionTypeComboBox.SelectedValue = this.exemptionData.GetSeniorExemptionTypeTable.Rows[0]["ExemptionTypeID"].ToString();
                this.RollYearTextBox.Text = this.exemptionData.GetSeniorExemptionTypeTable.Rows[0]["RollYear"].ToString();
                this.ExcemptionTypeComboBox.Enabled = false;
                this.RollYearTextBox.Enabled = false;
                this.RollYearPanel.Enabled = true;
                this.ExcemptionTypePanel.Enabled = false;
                this.ExemptionGridView.DataSource = this.exemptionData.GridLoadExemptionTypeTable.DefaultView;
                DataRow[] dr = this.exemptionData.GridLoadExemptionTypeTable.Select("EmptyRecord$=False");
                ////if (this.exemptionData.GridLoadExemptionTypeTable.Rows.Count >= 5 )
                if (dr.Length >= 12)
                {
                    this.exemptionData.GridLoadExemptionTypeTable.AddGridLoadExemptionTypeTableRow(this.exemptionData.GridLoadExemptionTypeTable.NewGridLoadExemptionTypeTableRow());
                    this.ExemptionGridVscrollBar.Visible = false;
                    //this.ExemptionGridVscrollBar.Enabled = false;
                }
                else
                {
                    this.ExemptionGridVscrollBar.Visible = true;
                    this.ExemptionGridVscrollBar.Enabled = false;
                }


                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                {
                    this.PermissionControlLock(false);
                }
                else
                {
                    this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
            }
            else
            {
                ////this.LoadComboBox();
                this.ExcemptionTypeComboBox.SelectedValue = -1;
                this.ExcemptionTypePanel.Enabled = false;
                this.ExcemptionTypeComboBox.Enabled = false;
                this.RollYearPanel.Enabled = false;
                this.RollYearTextBox.Text = string.Empty;
                this.RollYearTextBox.Enabled = false;
                this.ExemptionGridView.Visible = false;
                this.IrrigatedBaseType3Panel.Visible = false;
                this.ExemptionGridpictureBox.Visible = false;
            }
        }

        private void PermissionControlLock(bool lockControl)
        {
            this.RollYearTextBox.LockKeyPress = lockControl;
            this.ExemptionGridView.ReadOnly = lockControl;
        }

        /// <summary>
        /// Validates the Function data gird items passes true on validation success.
        /// </summary>
        /// <returns>Boolean Value</returns>
        private bool ValidateKeyItems()
        {
            try
            {
                bool validationResult = true;
                this.ExemptionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.exemptionData.GridLoadExemptionTypeTable.AcceptChanges();
                for (int i = 0; i < this.ExemptionGridView.Rows.Count; i++)
                {
                    if (validationResult)
                    {
                        if (((string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[0].Value.ToString().Trim())) && (!string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[2].Value.ToString().Trim()))))
                        {
                            validationResult = false;
                        }
                    }
                }

                return validationResult;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// ValidateEmptyRows()
        /// </summary>
        /// <returns>bool</returns>
        private bool ValidateEmptyRows()
        {
            try
            {
                this.ExemptionGridView.AllowEmptyRows = false;

                this.ExemptionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.exemptionData.GridLoadExemptionTypeTable.AcceptChanges();

                string val1 = "(ExemptionCode IS  NULL And Description IS  NULL And ExemptionPercent IS  NULL And Minimum IS  NULL And Maximum IS  NULL And AbstractCode IS  NULL )";
                DataRow[] dr1 = this.exemptionData.GridLoadExemptionTypeTable.Select(val1);
                if (dr1.Length >= 5)
                {
                    this.emptyRowAvailable = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
                else
                {
                    string val2 = "ExemptionCode IS NOT  NULL or ExemptionPercent IS NOT  NULL ";
                    DataRow[] dr2 = this.exemptionData.GridLoadExemptionTypeTable.Select(val2);
                    if (dr2.Length > 0)
                    {
                        for (int i = 0; i < dr2.Length; i++)
                        {
                            if ((dr2[i].ItemArray[0].ToString() == "") || (dr2[i].ItemArray[2].ToString() == ""))
                            {
                                this.emptyRowAvailable = false;
                                break;
                            }
                            else if (Convert.ToInt32(dr2[i].ItemArray[3]) > Convert.ToInt32(dr2[i].ItemArray[4]))
                            {
                                this.emptyRowAvailable = false;
                                break;
                            }
                        }
                    }
                }

                return this.emptyRowAvailable;
                ////}          
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// To verfiy empty rows available
        /// </summary>
        private void CheckEmptyRowsAvaliable()
        {
            {
                this.saveStatus = true;
                this.SaveExemptionDefinition();
            }
        }

        /// <summary>
        /// SaveExemptionDefinition
        /// </summary>
        private void SaveExemptionDefinition()
        {
            string excemptionType;
            int saveConfirm;
            int eventKeyID;
            excemptionType = "<Root><Table><ExemptionTypeID>" + this.ExcemptionTypeComboBox.SelectedValue.ToString() + "</ExemptionTypeID></Table><Table><RollYear>" + this.RollYearTextBox.Text + "</RollYear></Table></Root>";
            {
                string elementItems = string.Empty;
                this.ExemptionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.exemptionData.GridLoadExemptionTypeTable.AcceptChanges();
                elementItems = TerraScanCommon.GetXmlString(this.exemptionData.GridLoadExemptionTypeTable);

                if (this.chkNewVal)
                {
                    eventKeyID = 0;
                }
                else
                {
                    eventKeyID = this.keyId;
                }

                saveConfirm = this.form27080Control.WorkItem.F27080_SaveExemptionType(eventKeyID, elementItems, excemptionType, 1, TerraScanCommon.UserId);
                this.chkNewVal = false;
                ////MessageBox.Show("attached");

                if (saveConfirm > 0)
                {
                    this.keyId = saveConfirm;
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = this.keyId;
                    ////to reload the form with the current keyid(this.valveId)
                    ////to refresh the master form with the return keyid
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                    this.RollYearTextBox.Focus();
                    this.sliceVal = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ErrorValidation"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ExemptionGridView.DataSource = this.exemptionData.GridLoadExemptionTypeTable.DefaultView;
                    this.sliceVal = false;
                    ////   this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.isDeleteRow = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// ExcemptionTypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExcemptionTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.exemptionData.Clear();
                this.IrrigatedBaseType3Panel.Visible = false;
                this.ExemptionGridView.Visible = false;
                this.ExemptionGridpictureBox.Visible = false;
                this.LoadWithExemptionTypeComboValue();
                if (ExcemptionTypeComboBox.SelectedIndex != 0)
                {
                    this.IrrigatedBaseType3Panel.Visible = true;
                    this.ExemptionGridView.Visible = true;
                    this.ExemptionGridpictureBox.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        ///  ExemptionGridView_CellFormatting
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.ExemptionGridView.Columns[this.ExemptionPercent.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString().Trim();
                        Decimal outDecimal;
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal <= 100)
                            {
                                string currenTcellvalue = outDecimal.ToString("#,##0.0");

                                e.Value = string.Concat(currenTcellvalue, "  %");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0.0 %";
                            }
                        }
                        else
                        {
                            e.Value = "0.0 %";
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if (e.ColumnIndex == this.ExemptionGridView.Columns[this.Maximum.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        double outDecimal;
                        double.TryParse(val, out outDecimal);
                        // Condition added for the issue 1683
                        if (e.Value.ToString().Contains("."))
                        {
                            if (outDecimal != 0.0)
                            {
                                e.Value = outDecimal.ToString("#,##0.0000");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }

                if (e.ColumnIndex == this.ExemptionGridView.Columns[this.Minimum.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        double outDecimal;
                        double.TryParse(this.ExemptionGridView["Minimum", e.RowIndex].Value.ToString().Trim(), out outDecimal);
                        // Condition added for the issue 1683
                        if (e.Value.ToString().Contains("."))
                        {
                            if (outDecimal != 0.0)
                            {
                                e.Value = outDecimal.ToString("#,##0.0000");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }
                if (e.ColumnIndex == this.ExemptionGridView.Columns[this.IncomeMax.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        double outDecimal;
                        double.TryParse(val, out outDecimal);
                        // Condition added for the issue 1683
                        if (e.Value.ToString().Contains("."))
                        {
                            if (outDecimal != 0.0)
                            {
                                e.Value = outDecimal.ToString("#,##0.0000");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
                        }
                    }
                    else
                    {
                        e.Value = string.Empty;
                    }
                }
                if (e.ColumnIndex == this.ExemptionGridView.Columns[this.IncomeMin.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        double outDecimal;
                        double.TryParse(this.ExemptionGridView["IncomeMin", e.RowIndex].Value.ToString().Trim(), out outDecimal);
                        // Condition added for the issue 1683
                        if (e.Value.ToString().Contains("."))
                        {
                            if (outDecimal != 0.0)
                            {
                                e.Value = outDecimal.ToString("#,##0.0000");
                                e.FormattingApplied = true;
                            }
                            else
                            {
                                e.Value = "0";
                            }
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
        }

        /// <summary>
        /// ExemptionGridView_EditingControlShowing
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ExemptionGridView_RowEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (this.ExemptionGridView.OriginalRowCount >= 0)
                {
                    if (e.RowIndex >= 1)
                    {
                        if ((string.IsNullOrEmpty(this.ExemptionGridView.Rows[(e.RowIndex - 1)].Cells[0].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.ExemptionGridView.Rows[(e.RowIndex - 1)].Cells[2].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < ExemptionGridView.RowCount)
                            {
                                for (int i = e.RowIndex; i < ExemptionGridView.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.ExemptionGridView["ExemptionCode", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Description", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Minimum", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Maximum", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["AbstractCode", e.RowIndex].Value.ToString().Trim())))
                                    {
                                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Description", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                        }
                    }

                    if (e.RowIndex == 0)
                    {
                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                    }

                    this.currentRowIndex = e.RowIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// ExemptionGridView_CellEndEdit
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double decimalVal;
                ////string exemptPercent;
                if (e.ColumnIndex == 2)
                {
                    if ((this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Trim())))
                    {
                        if (this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Trim().Contains("%"))
                        {
                            this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value = this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Replace("%", "");
                        }

                        double.TryParse(this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Trim(), out decimalVal);

                        if (decimalVal <= 0.00 || decimalVal > 100.00)
                        {
                            this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value = "0";
                        }
                        else
                        {
                            this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value = decimalVal;
                        }
                    }
                }

                double percentageMInDecVal;

                if (e.ColumnIndex == 3)
                {
                    if ((this.ExemptionGridView["Minimum", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.ExemptionGridView["Minimum", e.RowIndex].Value.ToString().Trim())))
                    {
                        double.TryParse(this.ExemptionGridView["Minimum", e.RowIndex].Value.ToString().Trim(), out percentageMInDecVal);

                        if (percentageMInDecVal <= 0.0 || percentageMInDecVal > 922337203685477.5807)
                        {
                            this.ExemptionGridView["Minimum", e.RowIndex].Value = "0";
                        }
                        else
                        {
                            this.ExemptionGridView["Minimum", e.RowIndex].Value = percentageMInDecVal;
                        }
                    }
                }

                double percentageMaxVal;
                if (e.ColumnIndex == 4)
                {
                    if ((this.ExemptionGridView["Maximum", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.ExemptionGridView["Maximum", e.RowIndex].Value.ToString().Trim())))
                    {
                        double.TryParse(this.ExemptionGridView["Maximum", e.RowIndex].Value.ToString().Trim(), out percentageMaxVal);

                        if (percentageMaxVal <= 0.0 || percentageMaxVal > 922337203685477.5807)
                        {
                            this.ExemptionGridView["Maximum", e.RowIndex].Value = "0";
                        }
                        else
                        {
                            this.ExemptionGridView["Maximum", e.RowIndex].Value = percentageMaxVal;
                        }
                    }
                }

                int percentageIncomeMaxVal;
                if (e.ColumnIndex == 6)
                {
                    if ((this.ExemptionGridView["IncomeMax", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.ExemptionGridView["IncomeMax", e.RowIndex].Value.ToString().Trim())))
                    {
                        if (this.ExemptionGridView["IncomeMax", e.RowIndex].Value.ToString().Trim().Contains("."))
                        {
                            this.ExemptionGridView["IncomeMax", e.RowIndex].Value = "0";
                        }
                        else
                        {
                            int.TryParse(this.ExemptionGridView["IncomeMax", e.RowIndex].Value.ToString().Trim(), out percentageIncomeMaxVal);

                            if (percentageIncomeMaxVal <= 0 || percentageIncomeMaxVal > 2147483647)
                            {
                                this.ExemptionGridView["IncomeMax", e.RowIndex].Value = "0";
                            }
                            else
                            {
                                this.ExemptionGridView["IncomeMax", e.RowIndex].Value = percentageIncomeMaxVal;
                            }
                        }
                    }
                }
                int percentageIncomeMinVal;
                if (e.ColumnIndex == 5)
                {
                    if ((this.ExemptionGridView["IncomeMin", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.ExemptionGridView["IncomeMin", e.RowIndex].Value.ToString().Trim())))
                    {
                        if (this.ExemptionGridView["IncomeMin", e.RowIndex].Value.ToString().Trim().Contains("."))
                        {
                            this.ExemptionGridView["IncomeMin", e.RowIndex].Value = "0";
                        }
                        else
                        {
                            int.TryParse(this.ExemptionGridView["IncomeMin", e.RowIndex].Value.ToString().Trim(), out percentageIncomeMinVal);

                            if (percentageIncomeMinVal <= 0 || percentageIncomeMinVal > 2147483647)
                            {
                                this.ExemptionGridView["IncomeMin", e.RowIndex].Value = "0";
                            }
                            else
                            {
                                this.ExemptionGridView["IncomeMin", e.RowIndex].Value = percentageIncomeMinVal;
                            }
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
        /// Handles the CellClick event of the ExemptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ExemptionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isRowHeaderClick = false;
            try
            {
                bool hasValues = false;

                if (this.ExemptionGridView.OriginalRowCount >= 0)
                {
                    if (e.RowIndex >= 1)
                    {
                        if ((string.IsNullOrEmpty(this.ExemptionGridView.Rows[(e.RowIndex - 1)].Cells[0].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.ExemptionGridView.Rows[(e.RowIndex - 1)].Cells[2].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < ExemptionGridView.RowCount)
                            {
                                for (int i = e.RowIndex; i < ExemptionGridView.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.ExemptionGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                                    this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.ExemptionGridView["ExemptionCode", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Description", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["ExemptionPercent", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Minimum", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["Maximum", e.RowIndex].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.ExemptionGridView["AbstractCode", e.RowIndex].Value.ToString().Trim())))
                                    {
                                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = true;
                                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                                        this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Description", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = true;
                                this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                            this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                        }
                    }

                    if (e.RowIndex == 0)
                    {
                        this.ExemptionGridView["ExemptionCode", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Description", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["ExemptionPercent", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Minimum", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["Maximum", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["IncomeMin", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["IncomeMax", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView["AbstractCode", e.RowIndex].ReadOnly = false;
                        this.ExemptionGridView.Rows[e.RowIndex].Selected = false;
                    }

                    this.currentRowIndex = e.RowIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// ExemptionGridpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridpictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// ExemptionGridpictureBox_MouseHover
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExemptionGridpictureBox_MouseHover(object sender, EventArgs e)
        {
            this.ExcemptionDefintionToolTip.SetToolTip(this.ExemptionGridpictureBox, Utility.GetFormNameSpace(this.Name));
        }

        /// <summary>
        /// ExcemptionTypeComboBox_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExcemptionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the ExemptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ExemptionGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.ExemptionGridView.CurrentCell.ReadOnly = true;
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the ExemptionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void ExemptionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString()))
            //{
            //    this.ExemptionGridView.Rows[e.RowIndex].Cells["Minimum"].Value = 0;
            //    this.EditEnabled();
            //}

            //if (string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells["Maximum"].Value.ToString()))
            //{
            //    this.ExemptionGridView.Rows[e.RowIndex].Cells["Maximum"].Value = 0;
            //    this.EditEnabled();
            //}
            //if (string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells["IncomeMin"].Value.ToString()))
            //{
            //    this.ExemptionGridView.Rows[e.RowIndex].Cells["IncomeMin"].Value = 0;
            //    this.EditEnabled();
            //}

            //if (string.IsNullOrEmpty(this.ExemptionGridView.Rows[e.RowIndex].Cells["IncomeMax"].Value.ToString()))
            //{
            //    this.ExemptionGridView.Rows[e.RowIndex].Cells["IncomeMax"].Value = 0;
            //    this.EditEnabled();
            //}
        }

        /// <summary>
        /// HeaderPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HeaderPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// HeaderPictureBox_MouseHover
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void HeaderPictureBox_MouseHover(object sender, EventArgs e)
        {
            this.ExcemptionDefintionToolTip.SetToolTip(this.ExemptionGridpictureBox, Utility.GetFormNameSpace(this.Name));
        }

        #region Methods

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Control Lock</param>
        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
        }

        /// <summary>
        /// LockControls
        /// </summary>
        /// <param name="lockControl">lockControl</param>
        private void LockControls(bool lockControl)
        {
            this.IrrigatedBaseType3Panel.Enabled = lockControl;
            this.RollYearPanel.Enabled = lockControl;
            this.ExcemptionTypePanel.Enabled = lockControl;
        }

        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            this.ExcemptionTypeComboBox.DataSource = null;
            this.RollYearTextBox.Text = string.Empty;
        }

        #endregion

        #region Events

        ////
        /// <summary>
        /// RollYearTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        #endregion

        /// <summary>
        /// Click Delete Row Context.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRow_Click(object sender, EventArgs e)
        {
            if (isDeleteRow && hasRowValues)
            {
                DeleteSelectedRow();
            }
        }
        #region Delete

        /// <summary>
        /// Deletes the schedule details.
        /// </summary>
        private void DeleteExcemption(int rowIndex, string exemptionCode)
        {
            try
            {
                if (this.ExemptionGridView.CurrentCell != null && this.slicePermissionField.deletePermission
                    && this.ExemptionGridView.OriginalRowCount > 0 && this.ExemptionGridView.Rows[rowIndex].Selected)
                {
                    this.form27080Control.WorkItem.F27080_DeleteExemption(TerraScanCommon.UserId, this.keyId, exemptionCode);
                    //this.GetSelectedParcels();               
                    this.exemptionData.GridLoadExemptionTypeTable.Rows.RemoveAt(rowIndex);
                    this.isrowDeleted = true;
                    this.exemptionData.GridLoadExemptionTypeTable.AcceptChanges();
                    //this.ExemptionGridView.Focus();
                    if (this.ExemptionGridView.Rows.Count > 0)
                    {
                        if (rowIndex > 0)
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.ExemptionGridView, rowIndex - 1);
                            this.ExemptionGridView.Rows[rowIndex - 1].Selected = true;
                        }
                        else
                        {
                            TerraScanCommon.SetDataGridViewPosition(this.ExemptionGridView, 0);
                            this.ExemptionGridView.Rows[0].Selected = true;
                        }
                        this.LoadDataGrid();
                        //this.flagLoadOnProcess = false;
                        //this.RollYearTextBox.Focus();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                        {
                            this.isDeleteRow = true;
                            this.isrowDeleted = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion

        /// <summary>
        /// Exemption Grid View Keydown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExemptionGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.CellRowValues();
                if (e.KeyCode.Equals(Keys.Delete))
                {
                    if (this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Selected)
                    {
                        if (isDeleteRow && hasRowValues)
                        {
                            DeleteSelectedRow();
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
        /// GridView Mouse Down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExemptionGridView_MouseDown(object sender, MouseEventArgs e)
        {
            this.CellRowValues();
            int rowSelected = this.ExemptionGridView.CurrentRowIndex;
            if (hasRowValues)
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool isSelected = false;
                    if (rowSelected != -1 && this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Selected)
                    {
                        isSelected = true;
                    }
                    if (isSelected)
                    {
                        ContextMenu m = new ContextMenu();
                        m.MenuItems.Add("Delete Row", DeleteRow_Click);
                        int currentMouseOverRow = this.ExemptionGridView.HitTest(e.X, e.Y).RowIndex;

                        if (currentMouseOverRow >= 0 && rowSelected == currentMouseOverRow)
                        {
                            if (isDeleteRow && isRowHeaderClick)
                            {
                                m.Show(ExemptionGridView, new Point(e.X, e.Y));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Delete the Selected Row
        /// </summary>
        private void DeleteSelectedRow()
        {
            string Message = string.Empty;
            string ExemptionCode = this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.exemptionData.GridLoadExemptionTypeTable.ExemptionCodeColumn.ColumnName].Value.ToString();
            if (this.ExemptionGridView.CurrentRowIndex >= 0)
            {
                if (this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.exemptionData.GridLoadExemptionTypeTable.ExemptionCodeColumn.ColumnName].Value != null
                           && this.ExemptionGridView.Rows[this.ExemptionGridView.CurrentRowIndex].Cells[this.exemptionData.GridLoadExemptionTypeTable.ExemptionCodeColumn.ColumnName].Value.ToString() != this.ExemptionCode.ToString())
                {
                    Message = this.form27080Control.WorkItem.F27080_GetExemptionError(this.keyId, ExemptionCode);
                    if (string.IsNullOrEmpty(Message))
                    {
                        if (MessageBox.Show(TerraScan.Utilities.SharedFunctions.GetResourceString("F27080DeleteRecord"), TerraScan.Utilities.SharedFunctions.GetResourceString("DeleteExemption"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.DeleteExcemption(this.ExemptionGridView.CurrentRowIndex, ExemptionCode);
                        }
                    }
                    else
                    {
                        MessageBox.Show(Message, TerraScan.Utilities.SharedFunctions.GetResourceString("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// Mouse Header Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExemptionGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                isRowHeaderClick = true;
                if (e.RowIndex >= 0 && this.ExemptionGridView.CurrentCell != null)
                {
                    this.ExemptionGridView.CurrentCell.ReadOnly = true;
                    this.ExemptionGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To check Row has values.
        /// </summary>
        private void CellRowValues()
        {
            try
            {
                if (this.ExemptionGridView.OriginalRowCount >= 0)
                {
                    if ((string.IsNullOrEmpty(this.ExemptionGridView.Rows[(currentRowIndex)].Cells[2].Value.ToString().Trim())) || (string.IsNullOrEmpty(this.ExemptionGridView.Rows[(currentRowIndex)].Cells[0].Value.ToString().Trim())))
                    {
                        hasRowValues = false;
                    }
                    else
                    {
                        hasRowValues = true;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}

