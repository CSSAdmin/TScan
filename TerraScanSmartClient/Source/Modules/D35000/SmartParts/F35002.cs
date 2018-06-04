//--------------------------------------------------------------------------------------------
// <copyright file="F35002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GeneralAdjustment Slice.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07		Shiva M     	    Created
//*********************************************************************************/
namespace D35000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    /// <summary>
    /// FormSlice GeneralAdjustment Slice
    /// </summary>
    [SmartPart]
    public partial class F35002 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// F35000Controller variable.
        /// </summary>
        private F35002Controller form35002Controll;

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// Variable to Hold the Current Parcel Value Slice ID.
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// variable holds the value of Adjustment.
        /// </summary>
        private decimal adjustmentValue;

        /// <summary>
        /// variable holds the type of Adjustment.
        /// </summary>
        private byte type;

        /// <summary>
        /// Variable Holds the ValueSlice Header Dataset.
        /// </summary>
        private F35001ValueSliceHeaderData valueSliceHeaderDataSet = new F35001ValueSliceHeaderData();

        /// <summary>
        /// variable holds the willValue bool.
        /// </summary>
        private bool willValue;

        /// <summary>
        /// variable is Used to store the bool value whether comboInitialized or not.
        /// </summary>
        private bool comboInitialized;

        /// <summary>
        /// Used to store the lowestMinValue
        /// </summary>
        private decimal lowestMinValue;

        /// <summary>
        /// Used to store the highestMaxValue
        /// </summary>
        private decimal highestMaxValue;

        private bool keyPressed = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35002"/> class.
        /// </summary>
        public F35002()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35002"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35002"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F35002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        #endregion

        #region Event Publication

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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form35002 controll.
        /// </summary>
        /// <value>The form35002 controll.</value>
        [CreateNew]
        public F35002Controller Form35002Controll
        {
            get
            {
                return this.form35002Controll as F35002Controller;
            }

            set
            {
                this.form35002Controll = value;
            }
        }

        /// <summary>
        /// Gets or sets the value slice id.
        /// </summary>
        /// <value>The value slice id.</value>
        public int ValueSliceId
        {
            get
            {
                return this.valueSliceId;
            }

            set
            {
                this.valueSliceId = value;
            }
        }

        #endregion

        #region Event Subscription

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

                    if (this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows.Count > 0)
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

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.LockControls(false);
                }
                else
                {
                    this.LockControls(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    // TODO : Need To Send the Values to the F35001.
                    decimal resultAmount;
                    F35002SubFormSaveEventArgs subFormSaveEventArgs;
                    subFormSaveEventArgs.type = this.type;
                    subFormSaveEventArgs.value = this.adjustmentValue;
                    subFormSaveEventArgs.valueSliceId = this.keyId;
                    Decimal.TryParse(this.ResultingAmountOfAdjustmentTextBox.Text, System.Globalization.NumberStyles.Currency, null, out resultAmount);
                    subFormSaveEventArgs.amount = resultAmount;
                    this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.Cursor = Cursors.WaitCursor;

            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.PopulateValueSliceHeaderDetails();
            this.ApplyTextBoxValidationType();
            this.FormatAdjustmentTextBoxValue();
            this.FormatResultAdjustmentTextBoxValue();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.Cursor = Cursors.Default;
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
                    this.PopulateValueSliceHeaderDetails();
                    this.ApplyTextBoxValidationType();
                    this.FormatAdjustmentTextBoxValue();
                    this.FormatResultAdjustmentTextBoxValue();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D35000_ F35001_ set will value].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D35000_F35001_SetWillValue, ThreadOption.UserInterface)]
        public void OnD35000_F35001_SetWillValue(object sender, DataEventArgs<bool> eventArgs)
        {
            if (this.slicePermissionField.editPermission)
            {
                this.willValue = eventArgs.Data;
                if (this.willValue)
                {
                    this.CalculateResultingAmountOfAdjustment();
                }
                else
                {
                    this.ResultingAmountOfAdjustmentTextBox.Text = "0";
                    ////commented by Biju on 11/Feb/2010 to fix #5862
                    ////this.ResultingAmountOfAdjustmentTextBox.ForeColor = Color.Black;
                }

                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
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

        #region Form Load Events

        /// <summary>
        /// Handles the Load event of the F35002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35002_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GeneralAdjustmentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.GeneralAdjustmentPictureBox.Height, this.GeneralAdjustmentPictureBox.Width, "", 28, 81, 128);
                this.InitAdjustmentTypeComboBox();
                this.comboInitialized = true;
                this.PopulateValueSliceHeaderDetails();
                this.ApplyTextBoxValidationType();
                this.willValue = Convert.ToBoolean(this.form35002Controll.WorkItem.RootWorkItem.State["WillValue"]);
                this.FormatAdjustmentTextBoxValue();
                this.FormatResultAdjustmentTextBoxValue();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Formats the adjustment text box value.
        /// </summary>
        private void FormatAdjustmentTextBoxValue()
        {
            decimal tempAdjValue;
            decimal.TryParse(this.ValueOfAdjustmentTextBox.Text, System.Globalization.NumberStyles.Number, null, out tempAdjValue);
            if (this.type.Equals(2) || this.type.Equals(3) || this.type.Equals(4) || this.type.Equals(5))
            {
                this.ValueOfAdjustmentTextBox.Text = tempAdjValue.ToString("#,##0");

                string actualValue = this.ValueOfAdjustmentTextBox.Text.Trim();
                if (actualValue.Contains("-"))
                {
                    actualValue = actualValue.Replace("-", "");
                    actualValue = "(" + actualValue + ")";
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed  ;
                }
                else
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;
                }

                this.ValueOfAdjustmentTextBox.Text = actualValue;
            }
            else if (this.type.Equals(6))
            {
                this.ValueOfAdjustmentTextBox.Text = tempAdjValue.ToString("0.0");
                string actualValue = this.ValueOfAdjustmentTextBox.Text.Trim();
                if (actualValue.Contains("-"))
                {
                    actualValue = actualValue.Replace("-", "");
                    actualValue = "(" + actualValue + ")";
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
                }
                else
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;
                }

                this.ValueOfAdjustmentTextBox.Text = actualValue + " %";
            }
        }

        /// <summary>
        /// Formats the result adjustment text box value.
        /// </summary>
        private void FormatResultAdjustmentTextBoxValue()
        {
            decimal tempResultAdjValue;
            decimal.TryParse(this.ResultingAmountOfAdjustmentTextBox.Text, System.Globalization.NumberStyles.Number, null, out tempResultAdjValue);
            if (this.type.Equals(2) || this.type.Equals(3) || this.type.Equals(4) || this.type.Equals(5))
            {
                this.ResultingAmountOfAdjustmentTextBox.Text = tempResultAdjValue.ToString("#,##0");

                string actualValue = this.ResultingAmountOfAdjustmentTextBox.Text.Trim();
                if (actualValue.Contains("-"))
                {
                    actualValue = actualValue.Replace("-", "");
                    actualValue = "(" + actualValue + ")";
                    ////commented by Biju on 11/Feb/2010 to fix #5862
                    ////this.ResultingAmountOfAdjustmentTextBox.ForeColor = Color.DarkRed;
                }
                else
                {
                    ////commented by Biju on 11/Feb/2010 to fix #5862
                    ////this.ResultingAmountOfAdjustmentTextBox.ForeColor = Color.Black;
                }

                this.ResultingAmountOfAdjustmentTextBox.Text = actualValue;
            }
            else if (this.type.Equals(6))
            {
                this.ResultingAmountOfAdjustmentTextBox.Text = tempResultAdjValue.ToString("#,##0.00");
                string actualValue = this.ResultingAmountOfAdjustmentTextBox.Text.Trim();
                if (actualValue.Contains("-"))
                {
                    actualValue = actualValue.Replace("-", "");
                    actualValue = "(" + actualValue + ")";
                    ////commented by Biju on 11/Feb/2010 to fix #5862
                    ////this.ResultingAmountOfAdjustmentTextBox.ForeColor = Color.DarkRed;
                }
                else
                {
                    ////commented by Biju on 11/Feb/2010 to fix #5862
                    ////this.ResultingAmountOfAdjustmentTextBox.ForeColor = Color.Black;
                }

                this.ResultingAmountOfAdjustmentTextBox.Text = actualValue;
            }
        }

        /// <summary>
        /// Populates the value slice header details.
        /// </summary>
        private void PopulateValueSliceHeaderDetails()
        {
            this.pageLoadStatus = true;
            this.ClearValueSliceHeader();
            this.valueSliceHeaderDataSet = this.Form35002Controll.WorkItem.F35001_GetValueSliceHeader(this.keyId);

            if (this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows.Count > 0)
            {
                decimal tempAdjValue;
                this.TypeOfAdjustmentCombo.SelectedValue = Convert.ToInt32(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.TypeColumn.ColumnName]);
                this.ResultingAmountOfAdjustmentTextBox.Text = this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.AmountColumn.ColumnName].ToString();
                this.type = Convert.ToByte(this.TypeOfAdjustmentCombo.SelectedValue);
                decimal.TryParse(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.ValueColumn.ColumnName].ToString(), System.Globalization.NumberStyles.Number, null, out tempAdjValue);

                if (this.type.Equals(6))
                {
                    tempAdjValue *= 100;
                }

                this.ValueOfAdjustmentTextBox.Text = tempAdjValue.ToString();

                ////to get the highest and lowest values
                decimal.TryParse(this.valueSliceHeaderDataSet.GetValueSliceHeader[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.HighestMaxValueColumn.ColumnName].ToString(), out this.highestMaxValue);
                decimal.TryParse(this.valueSliceHeaderDataSet.GetValueSliceHeader[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.LowestMinValueColumn.ColumnName].ToString(), out this.lowestMinValue);
            }
            else
            {
                this.ClearValueSliceHeader();
                this.LockControls(true);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Enter event of the ValueOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValueOfAdjustmentTextBox_Enter(object sender, EventArgs e)
        {
            try
            {
                string actualValue = this.ValueOfAdjustmentTextBox.Text.Trim();
                actualValue = actualValue.Replace(",", "");
                actualValue = actualValue.Replace("%", "");
                if (actualValue.Contains("("))
                {
                    actualValue = actualValue.Replace("(", "");
                    actualValue = actualValue.Replace(")", "");
                    actualValue = actualValue.Insert(0, "-");
                }

                this.ValueOfAdjustmentTextBox.Text = actualValue;

                if (this.ValueOfAdjustmentTextBox.Text.Contains("-") || this.ValueOfAdjustmentTextBox.Text.Contains("("))
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculates the resulting amount of adjustment.
        /// </summary>
        /// <returns>status of the resulting amount.</returns>
        private bool CalculateResultingAmountOfAdjustment()
        {
            if (!this.pageLoadStatus)
            {
                string tempValueOfAdjustment = this.ValueOfAdjustmentTextBox.Text.Replace("%", "");
                decimal tempValue;
                Decimal.TryParse(tempValueOfAdjustment, System.Globalization.NumberStyles.Currency, null, out this.adjustmentValue);
                tempValue = this.adjustmentValue;

                if (this.type.Equals(6))
                {
                    if (this.adjustmentValue > 100 || this.adjustmentValue < -100)
                    {
                        MessageBox.Show("Adjustment value should be within -100% and 100%", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.adjustmentValue = 0;
                        tempValue = 0;
                        this.ValueOfAdjustmentTextBox.Text = "0";
                        this.ValueOfAdjustmentTextBox.Focus();
                        return true;
                    }
                    else
                    {
                        this.adjustmentValue = Math.Round(this.adjustmentValue, 1);
                        tempValue = Math.Round(tempValue, 1);
                        this.adjustmentValue /= 100;
                        tempValue /= 100;
                    }
                }
                else if (this.type.Equals(5))
                {
                    if (this.adjustmentValue > 922337203685477 || this.adjustmentValue < -922337203685477)
                    {
                        MessageBox.Show("Adjustment value should be between -922337203685477 and 922337203685477", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.adjustmentValue = 0;
                        tempValue = 0;
                        this.ValueOfAdjustmentTextBox.Text = "0";
                        this.ValueOfAdjustmentTextBox.Focus();
                        return true;
                    }
                }
                else if (this.type.Equals(3))
                {
                    if (!string.IsNullOrEmpty(this.valueSliceHeaderDataSet.GetValueSliceHeader[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.LowestMinValueColumn.ColumnName].ToString()))
                    {
                        if (this.adjustmentValue < this.lowestMinValue)
                        {
                            string messageText = "Adjustment value should be greater than minimum Value " + this.lowestMinValue.ToString("#,##.00") + " in the current parcel";

                            MessageBox.Show(SharedFunctions.GetResourceString(messageText), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            //this.ValueOfAdjustmentTextBox.Focus();
                            this.TypeOfAdjustmentCombo.Focus();
                            return true;
                        }
                        else if (this.adjustmentValue > 922337203685477 || this.adjustmentValue < -922337203685477)
                        {
                            MessageBox.Show("Adjustment value should be between -922337203685477 and 922337203685477", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            this.ValueOfAdjustmentTextBox.Focus();
                            return true;
                        }
                    }
                    else
                    {
                        if (this.adjustmentValue > 922337203685477 || this.adjustmentValue < -922337203685477)
                        {
                            MessageBox.Show("Adjustment value should be between -922337203685477 and 922337203685477", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            this.ValueOfAdjustmentTextBox.Focus();
                            return true;
                        }
                    }
                }
                else if (this.type.Equals(4))
                {
                    if (!string.IsNullOrEmpty(this.valueSliceHeaderDataSet.GetValueSliceHeader[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.HighestMaxValueColumn.ColumnName].ToString()))
                    {
                        if (this.adjustmentValue > this.highestMaxValue)
                        {
                            string messageText = "Adjustment value should be lesser than maximum Value " + this.highestMaxValue.ToString("#,##.00") + " in the current parcel";

                            MessageBox.Show(SharedFunctions.GetResourceString(messageText), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            this.ValueOfAdjustmentTextBox.Focus();
                            return true;
                        }
                        else if (this.adjustmentValue > 922337203685477 || this.adjustmentValue < -922337203685477)
                        {
                            MessageBox.Show("Adjustment value should be between -922337203685477 and 922337203685477", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            this.ValueOfAdjustmentTextBox.Focus();
                            return true;
                        }
                    }
                    else
                    {
                        if (this.adjustmentValue > 922337203685477 || this.adjustmentValue < -922337203685477)
                        {
                            MessageBox.Show("Adjustment value should be between -922337203685477 and 922337203685477", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.adjustmentValue = 0;
                            tempValue = 0;
                            this.ValueOfAdjustmentTextBox.Text = "0";
                            this.ValueOfAdjustmentTextBox.Focus();
                            return true;
                        }
                    }
                }


                ////else if (this.type.Equals(4))
                ////{
                ////    if (this.value > Convert.ToDecimal(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.HighestMaxValueColumn.ColumnName]))
                ////    {
                ////        MessageBox.Show("Min value should not greater than Max value" + this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.HighestMaxValueColumn.ColumnName].ToString() , "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////        this.value = 0;
                ////        tempValue = 0;
                ////        this.ValueOfAdjustmentTextBox.Text = "0";
                ////        this.ValueOfAdjustmentTextBox.Focus();
                ////    }

                ////}

                if (this.adjustmentValue.ToString().Contains("-"))
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
                    tempValue = decimal.Negate(tempValue);
                    if (this.type.Equals(6))
                    {
                        tempValue *= 100;
                        this.ValueOfAdjustmentTextBox.Text = "(" + tempValue.ToString("#,##0.0") + ") %";
                    }
                    else
                    {
                        this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
                        this.ValueOfAdjustmentTextBox.Text = "(" + tempValue.ToString(this.ValueOfAdjustmentTextBox.TextCustomFormat) + ")";
                    }
                }
                else
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;
                    this.ValueOfAdjustmentTextBox.Text = this.adjustmentValue.ToString(this.ValueOfAdjustmentTextBox.TextCustomFormat);
                }

                if (this.adjustmentValue.Equals(0) || this.willValue.Equals(false))
                {
                    this.ResultingAmountOfAdjustmentTextBox.Text = "0";
                }
                else
                {
                    if (this.type.Equals(5) || this.type.Equals(6) || this.type.Equals(2) || this.type.Equals(3) || this.type.Equals(4))
                    {
                        string resultingAmount = this.Form35002Controll.WorkItem.F35001_GetAdjustmentSliceValue(this.keyId, this.type, this.willValue, this.adjustmentValue);
                        this.ResultingAmountOfAdjustmentTextBox.Text = resultingAmount;
                    }
                    else
                    {
                        string resultingAmount = string.Empty;
                        ////string resultingAmount = this.Form35002Controll.WorkItem.F35001_GetAdjustmentSliceValue(this.keyId, this.type, this.willValue, this.value);
                        this.ResultingAmountOfAdjustmentTextBox.Text = resultingAmount;
                    }
                }

                //if (this.ValueOfAdjustmentTextBox.Text.Contains("-"))
                //{
                //    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed;
                //}
                //else
                //{
                //    this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;
                //}

                this.FormatResultAdjustmentTextBoxValue();
                return false;
            }
            return false;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the TypeOfAdjustmentCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeOfAdjustmentCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (this.comboInitialized)
                    {
                        this.adjustmentValue = 0;
                        this.ValueOfAdjustmentTextBox.Text = "0";
                        this.ResultingAmountOfAdjustmentTextBox.Text = "0";
                        this.ApplyTextBoxValidationType();
                    }
                }
                this.keyPressed = true;
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the GeneralAdjustmentPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralAdjustmentPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueSliceHeadeToolTip.SetToolTip(this.GeneralAdjustmentPictureBox, "D35000.F35002");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Handles the Leave event of the ValueOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValueOfAdjustmentTextBox_Leave(object sender, EventArgs e)
        {
            if (this.ValueOfAdjustmentTextBox.Text.Contains("-") || this.ValueOfAdjustmentTextBox.Text.Contains("("))
            {
                this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
            }
            else
            {
                this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;
            }

            //this.FormatResultAdjustmentTextBoxValue();
        }

        /// <summary>
        /// Handles the KeyDown event of the ValueOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ValueOfAdjustmentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            ////Modified by Biju on 20/Jan/2010 to fix #5793. Added keyvalue checking against 189 (-)
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.KeyValue.Equals(189) || e.KeyValue.Equals(109))
            {
                this.keyPressed = true;
                this.SetEditRecord();
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ValueOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ValueOfAdjustmentTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ( e.KeyChar >= 48 && e.KeyChar <= 57)
            {
                this.keyPressed = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ResultingAmountOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ResultingAmountOfAdjustmentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                this.keyPressed = true;
                this.SetEditRecord();

            }
        }

        /// <summary>
        /// Handles the Validated event of the ValueOfAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValueOfAdjustmentTextBox_Validated(object sender, EventArgs e)
        {
            try
            {
                this.CalculateResultingAmountOfAdjustment();
                if (this.ValueOfAdjustmentTextBox.Text.Contains("-") || this.ValueOfAdjustmentTextBox.Text.Contains("("))
                {
                    this.ValueOfAdjustmentTextBox.ForeColor = Color.DarkRed ;
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.GeneralAdjustmentPanel.Enabled = !lockValue;
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.TypeOfAdjustmentCombo.Text.Trim()))
            {
                this.TypeOfAdjustmentCombo.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.ValueOfAdjustmentTextBox.Text.Trim()))
            {
                this.ValueOfAdjustmentTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.ResultingAmountOfAdjustmentTextBox.Text.Trim()))
            {
                this.ValueOfAdjustmentTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F15003RequiredFieldMissing");
            }
            else if (this.CalculateResultingAmountOfAdjustment())
            {
                this.TypeOfAdjustmentCombo.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = "The amount entered is invalid because it is conflicting with the other value slice amount.";
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Clears the value slice header.
        /// </summary>
        private void ClearValueSliceHeader()
        {
            this.TypeOfAdjustmentCombo.SelectedValue = 5;
            this.ValueOfAdjustmentTextBox.Text = string.Empty;
            this.ResultingAmountOfAdjustmentTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.keyPressed)
            {
                if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                {
                    if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                    {
                        if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                        }
                    }
                }
                this.keyPressed = false;
            }
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
                this.SetEditRecord();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Applies the type of the text box validation.
        /// </summary>
        private void ApplyTextBoxValidationType()
        {
            decimal tempvalue;
            string tempValueOfAdjustment = this.ValueOfAdjustmentTextBox.Text.Replace("%", "");

            Decimal.TryParse(tempValueOfAdjustment, System.Globalization.NumberStyles.Currency, null, out tempvalue);
            this.ValueOfAdjustmentTextBox.ForeColor = Color.Black;

            if (Convert.ToInt32(this.TypeOfAdjustmentCombo.SelectedValue).Equals(5))
            {
                this.type = 5;

                this.ValueOfAdjustmentTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.WholeInteger;
                this.ValueOfAdjustmentTextBox.AllowNegativeSign = true;
                this.ValueOfAdjustmentTextBox.WholeInteger = true;
                this.ValueOfAdjustmentTextBox.TextCustomFormat = "#,##0";
                this.ValueOfAdjustmentTextBox.Text = tempvalue.ToString("0");
            }
            else if (Convert.ToInt32(this.TypeOfAdjustmentCombo.SelectedValue).Equals(6))
            {
                this.type = 6;
                this.ValueOfAdjustmentTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.ValueOfAdjustmentTextBox.AllowNegativeSign = true;
                this.ValueOfAdjustmentTextBox.ApplyNegativeStandard = false;
                this.ValueOfAdjustmentTextBox.TextCustomFormat = "#,##0.0 %";
                this.ValueOfAdjustmentTextBox.Text = tempvalue.ToString("0.0");
            }
            else
            {
                this.type = Convert.ToByte(this.TypeOfAdjustmentCombo.SelectedValue);
                this.ValueOfAdjustmentTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.WholeInteger;
                this.ValueOfAdjustmentTextBox.AllowNegativeSign = true;
                this.ValueOfAdjustmentTextBox.WholeInteger = true;
                this.ValueOfAdjustmentTextBox.TextCustomFormat = "#,##0";
                this.ValueOfAdjustmentTextBox.Text = tempvalue.ToString("0");
            }
        }

        /// <summary>
        /// Inits the adjustment type combo box.
        /// </summary>
        private void InitAdjustmentTypeComboBox()
        {
            this.pageLoadStatus = true;
            F35001ValueSliceHeaderData.ListAdjustmentTypeDataTable listAdjustmentType = new F35001ValueSliceHeaderData.ListAdjustmentTypeDataTable();
            listAdjustmentType.Merge(this.form35002Controll.WorkItem.F35002_ListAdjustmentType(this.masterFormNo));
            this.TypeOfAdjustmentCombo.ValueMember = this.valueSliceHeaderDataSet.ListAdjustmentType.AdjustmentTypeIDColumn.ColumnName;
            this.TypeOfAdjustmentCombo.DisplayMember = this.valueSliceHeaderDataSet.ListAdjustmentType.AdjustmentTypeColumn.ColumnName;
            this.TypeOfAdjustmentCombo.DataSource = listAdjustmentType;
            if (this.TypeOfAdjustmentCombo.Items.Count > 0)
            {
                this.TypeOfAdjustmentCombo.SelectedValue = 5;
            }

            this.pageLoadStatus = false;
        }

        #endregion
    }
}
