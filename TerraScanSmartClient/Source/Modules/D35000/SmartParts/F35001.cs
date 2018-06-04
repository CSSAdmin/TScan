//--------------------------------------------------------------------------------------------
// <copyright file="F35001.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Value Slice Header.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07		Sadha Shivudu M    Created
// 21 April 09		Sadha Shivudu M    Implemented the TSCO# 5950
// 14 May 09        Shanmuga Sundaram A Implemented the TSCO# 7071
//*********************************************************************************/

namespace D35000
{
    #region Namespace

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
    using TerraScan.Helper;

    #endregion Namespace

    /// <summary>
    /// Form Slice F35001 ValueSlice Header
    /// </summary>
    [SmartPart]
    public partial class F35001 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// F35000Controller variable.
        /// </summary>
        private F35001Controller form35001Controll;

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
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// Variable Holds the ValueSlice Header Dataset.
        /// </summary>
        private F35001ValueSliceHeaderData valueSliceHeaderDataSet = new F35001ValueSliceHeaderData();

        /// <summary>
        /// variable holds the willValue Bool.
        /// </summary>
        private bool willValue;

        /// <summary>
        /// variable is Used to store the bool value whether comboInitialized or not.
        /// </summary>
        private bool comboInitialized;

        /// <summary>
        /// variable is Used to store New Construction TextBox Value.
        /// </summary>
        private string tempNewConstructionValue;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35001"/> class.
        /// </summary>
        public F35001()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35001"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35001"/> class.
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
        public F35001(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D35000_F35001_SetWillValue      
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35001_SetWillValue, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<bool>> D35000_F35001_SetWillValue;

        /// <summary>
        /// Declare the event D35000_F35000_ParcelChangedValue
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35000_ParcelChangedValue, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> D35000_F35000_ParcelChangedValue;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the form35001 controll.
        /// </summary>
        /// <value>The form35001 controll.</value>
        [CreateNew]
        public F35001Controller Form35001Controll
        {
            get { return this.form35001Controll as F35001Controller; }
            set { this.form35001Controll = value; }
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

        #region Event SubScription

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
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.LockControls(false);
                    this.DescriptionTextBox.Focus();
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
                    // TODO : Need To Fire Global Event SaveSubFormEvent
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

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.PopulateValueSliceHeaderDetails();
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                int parcelId = -1;

                if (this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows.Count > 0)
                {
                    parcelId = Convert.ToInt32(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.ParcelIDColumn.ColumnName]);
                }
                ////Added By Ramya(Sprint40 CO)
                /* this.appraisalSummaryData = this.form35001Controll.WorkItem.F35000_CheckAppraisalSummaryUser(this.keyId, 0, TerraScan.Common.TerraScanCommon.UserId);
                 if (this.appraisalSummaryData.f35000_checkAppraisalUserTable.Rows.Count > 0)
                 {
                     F35000AppraisalSummaryData.f35000_checkAppraisalUserTableRow userTableRow = (F35000AppraisalSummaryData.f35000_checkAppraisalUserTableRow)this.appraisalSummaryData.f35000_checkAppraisalUserTable.Rows[0];
                     this.userName = userTableRow.Name_Display;
                     this.outPutValue = userTableRow.PrimaryKeyID;
                     if (this.outPutValue != 1)
                     {*/
                this.Form35001Controll.WorkItem.F35001_DeleteValueSlice(this.keyId, TerraScanCommon.UserId);

                if (parcelId != -1)
                {
                    int[] tempArgs = new int[2];
                    tempArgs[0] = parcelId;
                    tempArgs[1] = 1;
                    this.D35000_F35000_ParcelChangedValue(this, new DataEventArgs<int[]>(tempArgs));
                }

                /*  }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ParcelLock") + this.userName, SharedFunctions.GetResourceString("ParcelLockHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }*/
                ////Till Here
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = true;
                this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
            }
        }

        /// <summary>
        /// D35000_s the F35002_ sub form save.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D35000_F35002_SubFormSave, ThreadOption.UserInterface)]
        public void D35000_F35002_SubFormSave(object sender, DataEventArgs<F35002SubFormSaveEventArgs> eventArgs)
        {
            if (this.keyId == eventArgs.Data.valueSliceId)
            {
                int returnValue = -1;
                F35001ValueSliceHeaderData.GetValueSliceHeaderDataTable tempValueSliceHeaderTable = new F35001ValueSliceHeaderData.GetValueSliceHeaderDataTable();
                DataRow updateValueSliceHeaderRow = tempValueSliceHeaderTable.NewGetValueSliceHeaderRow();
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.DescriptionColumn.ColumnName] = this.DescriptionTextBox.Text.Trim();
                decimal newconstruction;
                ////Commented by Biju on 11/Feb/2010 to fix #5877
                ////decimal.TryParse(this.NewConstValueTextBox.Text, out newconstruction);
                ////Added by Biju on 11/Feb/2010 to fix #5877
                newconstruction = this.NewConstValueTextBox.DecimalTextBoxValue;

                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.NewConstructionColumn.ColumnName] = newconstruction;
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.IsValueColumn.ColumnName] = Convert.ToInt16(this.WillValueCombo.SelectedValue);
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.IsRollColumn.ColumnName] = Convert.ToInt16(this.WillRollCombo.SelectedValue);
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.TypeColumn.ColumnName] = eventArgs.Data.type;
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.ValueColumn.ColumnName] = eventArgs.Data.value;
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.AmountColumn.ColumnName] = eventArgs.Data.amount;
                //// Added new column to valid the New Construction field by sending default value for 'UpdateNewConstColumn' as '1'.
                updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.UpdateNewConstColumn.ColumnName] = 1;
                tempValueSliceHeaderTable.Rows.Add(updateValueSliceHeaderRow);
                string valueSliceHeaderItems = TerraScanCommon.GetXmlString(tempValueSliceHeaderTable);

                //// returns -2 as to show the message box screen 
                returnValue = this.form35001Controll.WorkItem.F35000_InsertOrUpdateValueSlice(this.keyId, valueSliceHeaderItems, TerraScan.Common.TerraScanCommon.UserId);

                //// this condition is to show/notshow the message box screen
                if (returnValue.Equals(-2))
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("NewConstruction"), SharedFunctions.GetResourceString("NewConstructionTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        //// Added for updating the New Construction field by sending default value for 'UpdateNewConstColumn' as '2'.
                        updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.UpdateNewConstColumn.ColumnName] = 2;
                    }
                    else
                    {
                        //// Added for not updating the New Construction field by sending default value for 'UpdateNewConstColumn' as '3'.
                        updateValueSliceHeaderRow[this.valueSliceHeaderDataSet.GetValueSliceHeader.UpdateNewConstColumn.ColumnName] = 3;
                    }

                    valueSliceHeaderItems = TerraScanCommon.GetXmlString(tempValueSliceHeaderTable);
                    //// returns the primary keyID with / without updation of the New Construction value
                    returnValue = this.form35001Controll.WorkItem.F35000_InsertOrUpdateValueSlice(this.keyId, valueSliceHeaderItems, TerraScan.Common.TerraScanCommon.UserId);
                }

                if (returnValue != -1)
                {
                    this.keyId = returnValue;
                }
                if (!WSHelper.IsOnLineMode)
                    TerraScanCommon.AddFieldUseValues(this.keyId, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = returnValue;
                ////to refresh the master form with the return keyid
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                this.PopulateValueSliceHeaderDetails();
                if (this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows.Count > 0)
                {
                    int[] tempArgs = new int[2];
                    int parcelId = Convert.ToInt32(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.ParcelIDColumn.ColumnName]);
                    tempArgs[0] = parcelId;
                    tempArgs[1] = 0;
                    this.D35000_F35000_ParcelChangedValue(this, new DataEventArgs<int[]>(tempArgs));
                }

                this.DescriptionTextBox.Focus();
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
        /// Handles the Load event of the F35001 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35001_Load(object sender, EventArgs e)
        {
            try
            {
                this.keyField = "ValueSliceID";
                this.formNo = 35001;
                this.FlagSliceForm = true;
                this.ValueSliceHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ValueSliceHeaderPictureBox.Height, this.ValueSliceHeaderPictureBox.Width, "", 28, 81, 128);
                this.InitWillValueCombo();
                this.InitWillRollCombo();
                this.comboInitialized = true;
                this.PopulateValueSliceHeaderDetails();
                this.willValue = Convert.ToBoolean(this.WillValueCombo.SelectedValue);
                this.form35001Controll.WorkItem.RootWorkItem.State["WillValue"] = this.willValue;
                if (this.ParentForm != null)
                {
                    this.ParentForm.ActiveControl = this.DescriptionTextBox;
                    this.DescriptionTextBox.Focus();
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

        /// <summary>
        /// Populates the value slice header details.
        /// </summary>
        private void PopulateValueSliceHeaderDetails()
        {
            this.pageLoadStatus = true;
            this.valueSliceHeaderDataSet = this.Form35001Controll.WorkItem.F35001_GetValueSliceHeader(this.keyId);

            if (this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows.Count > 0)
            {
                decimal outNewConstructionValue;
                this.SliceTypeTextBox.Text = this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.SliceTypeColumn.ColumnName].ToString();
                this.DescriptionTextBox.Text = this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.DescriptionColumn.ColumnName].ToString();
                decimal.TryParse(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.NewConstructionColumn.ColumnName].ToString(), out outNewConstructionValue);
                this.NewConstValueTextBox.Text = outNewConstructionValue.ToString("#,##0.00");
                this.tempNewConstructionValue = outNewConstructionValue.ToString("0.00");
                this.WillValueCombo.SelectedValue = Convert.ToInt32(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.IsValueColumn.ColumnName]);
                this.WillRollCombo.SelectedValue = Convert.ToInt32(this.valueSliceHeaderDataSet.GetValueSliceHeader.Rows[0][this.valueSliceHeaderDataSet.GetValueSliceHeader.IsRollColumn.ColumnName]);
            }
            else
            {
                this.ClearValueSliceHeader();
                this.LockControls(true);
            }

            // Added by Latha
            //if (this.NewConstValueTextBox.Text.Equals("0.00"))
            //{
            //    this.NewConstValueTextBox.ForeColor = System.Drawing.Color.Black;
            //}
            //else if (this.ConvertStringtoDec(this.AdjOtherTextBox.Text.Trim()) > 0)
            //{
            //    this.NewConstValueTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0);
            //}
            if (this.NewConstValueTextBox.Text.Contains("("))
            {
                this.NewConstValueTextBox.ForeColor = System.Drawing.Color.FromArgb(128, 0, 0);
                ////Added by Biju on 08/Feb/2010 to fix #5877
                //decimal tempNewConstValue;
                //decimal.TryParse(this.NewConstValueTextBox.Text, out tempNewConstValue);
                ////till here
                ////Modified by Biju on 08/Feb/2010 to fix #5877
                //this.NewConstValueTextBox.Text = "(" + Decimal.Negate(tempNewConstValue ) + ")";
            }
            else
            {
                this.NewConstValueTextBox.ForeColor = System.Drawing.Color.Black;
            }
            // Ends here

            this.DescriptionTextBox.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the SelectedIndexChanged event of the WillValueCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WillValueCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                if (this.comboInitialized)
                {
                    this.willValue = Convert.ToBoolean(this.WillValueCombo.SelectedValue);
                    this.D35000_F35001_SetWillValue(this, new DataEventArgs<bool>(this.willValue));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ValueSliceHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValueSliceHeaderPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ValueSliceHeadeToolTip.SetToolTip(this.ValueSliceHeaderPictureBox, "D35000.F35001");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the WillRollCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WillRollCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the KeyDown event of the NewConstValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NewConstValueTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Inits the will value combo.
        /// </summary>
        private void InitWillValueCombo()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.WillValueCombo.DataSource = commonData.ComboBoxDataTable;
            this.WillValueCombo.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.WillValueCombo.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Inits the will roll combo.
        /// </summary>
        private void InitWillRollCombo()
        {
            CommonData commonData = new CommonData();
            ////which loads yes, no value to the ComboBoxDataTable
            commonData.LoadYesNoValue();
            this.WillRollCombo.DataSource = commonData.ComboBoxDataTable;
            this.WillRollCombo.ValueMember = commonData.ComboBoxDataTable.KeyIdColumn.ToString();
            this.WillRollCombo.DisplayMember = commonData.ComboBoxDataTable.KeyNameColumn.ToString();
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            this.ValueSliceHeaderPanel.Enabled = !lockValue;
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
            return sliceValidationFields;
        }

        /// <summary>
        /// Clears the value slice header.
        /// </summary>
        private void ClearValueSliceHeader()
        {
            this.SliceTypeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.NewConstValueTextBox.Text = string.Empty;
            this.WillValueCombo.SelectedValue = 1;
            this.WillRollCombo.SelectedValue = 1;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)   //// && this.pageMode == TerraScanCommon.PageModeTypes.View
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.D35000_F35001_SetWillValue(this, new DataEventArgs<bool>(this.willValue));
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
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
        /// Handles the TextChanged event of the NewConstValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NewConstValueTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!NewConstValueTextBox.Text.Replace(",", "").Trim().Equals(this.tempNewConstructionValue))
                //{
                this.SetEditRecord();
                // }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void NewConstValueTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                //if (!NewConstValueTextBox.Text.Replace(",", "").Trim().Equals(this.tempNewConstructionValue))
                //{
                this.SetEditRecord();
                // }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
