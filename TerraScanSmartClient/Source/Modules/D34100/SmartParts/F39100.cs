//----------------------------------------------------------------------------------
// <copyright file="F39100.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F39100 Form Slice - AGLAND
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
//
//
//***********************************************************************************



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TerraScan.Common;
using TerraScan.Utilities;
using TerraScan.BusinessEntities;
using TerraScan.UI.Controls;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.EventBroker;
using TerraScan.Infrastructure.Interface.Constants;
using System.Web.Services.Protocols;



namespace D34100
{
    public partial class F39100 : BaseSmartPart
    {
        #region MemberVariables

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

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
        /// form27081Controll variable.
        /// </summary>
        private F39100Controller form39100Controll;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool formMasterNew;

        ///<summary>
        ///used to newclick
        ///</summary>
        private bool newclick = false;

        /// <summary>
        /// dataset contains AglandUseData details.
        /// </summary>
        private F34100AglandUseData AglandUseData = new F34100AglandUseData();

        /// <summary>
        ///  dataset contain AglandUSe Neighborhood
        /// </summary>
        private F34100AglandUseData.AglandMethodDataTableDataTable MethodData = new F34100AglandUseData.AglandMethodDataTableDataTable();  

        ///<Summary>
        /// dataset Contain AglandUse Type Table
        /// </Summary>
        private F34100AglandUseData.AglandTypeDataTableDataTable TypeData = new F34100AglandUseData.AglandTypeDataTableDataTable();  

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F27081"/> class.
        /// </summary>
        public F39100()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15005"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F39100(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.picturebox1.Image = ExtendedGraphics.GenerateVerticalImage(this.picturebox1.Height, this.picturebox1.Width, tabText, red, green, blue);
        }
        #endregion

        #region Event Publication

        ///// <summary>
        ///// Declare the event FormSlice_EditEnabled        
        ///// </summary>
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

        ///// <summary>
        ///// Declare the event FormSlice_FormCloseAlert        
        ///// </summary>
        //[EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        //public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        #endregion

        #region Properities

        /// <summary>
        /// Gets or sets the form27081 controll.
        /// </summary>
        /// <value>The form27081 controll.</value>
        [CreateNew]
        public F39100Controller Form39100Controll
        {
            get { return this.form39100Controll as F39100Controller; }
            set { this.form39100Controll = value; }
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
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {


                    //if (this.tifDistrictData.F27081TIFSubfundComboboxDataTable.Rows.Count > 0)
                    //{
                    eventArgs.Data.FlagInvalidSliceKey = false;
                    //}
                    //else
                    //{
                    //    if (eventArgs.Data.FlagInvalidSliceKey)
                    //    {
                    //        eventArgs.Data.FlagInvalidSliceKey = true;
                    //    }
                    //}
                }

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    //if (this.tifDistrictData.F27081TIFSubfundComboboxDataTable.Rows.Count > 0)
                    //{
                    this.LockControls(false);
                    //}
                    //else
                    //{
                    //    this.LockControls(true);
                    //}
                }
                else
                {
                    this.LockControls(true);
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
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission )
            {
               this.form39100Controll.WorkItem.F34100_DeleteAglandDetails(this.keyId, TerraScan.Common.TerraScanCommon.UserId);
            }
        }
        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {

                    this.Cursor = Cursors.WaitCursor;
                    this.pageLoadStatus = true;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.ClearAGLANDText();
                    //this.keyId = -99;
                    this.LockControls(false);
                    this.pageLoadStatus = false;
                    this.Cursor = Cursors.Default;
                    this.newclick = true;
                    this.AglandUseData.AglandUseDataTable.Clear();
                    //this.AglandUseData.AglandTypeDataTable.Clear();
                    //this.AglandUseData.AglandMethodDataTable.Clear();   
                    ///USED TO DISPLAY SUBFUND COMBOBOX TO DISPLAY ITEMS
                    //this.subFundSelectionData = this.Form27081Controll.WorkItem.F1515_GetSubFundSelection(null, null, 0, 0);
                    //if (this.subFundSelectionData != null)
                    //{
                    //    /// used for the display of combobox after leave active control

                    //    this.SubFundComboBox.DataSource = this.subFundSelectionData.GetSubFundSelection;
                    //    this.SubFundComboBox.DisplayMember = this.subFundSelectionData.GetSubFundSelection.SubFundColumn.ColumnName.ToString();
                    //    this.SubFundComboBox.ValueMember = this.subFundSelectionData.GetSubFundSelection.SubFundIDColumn.ColumnName.ToString();
                    //}
                    this.RollYearTextBox.Focus();

                }
                else
                {
                    this.LockControls(true);
                }

            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            this.newclick = false;
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.LockControls(false);
            }
            else
            {
                this.LockControls(true);
            }

            this.PopulateAgLandUseDetails();

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.PermissionFiled.editPermission)
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
        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                 this.SaveAgLand();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;

                }
            }
            else
            {
                this.LockControls(true);
                //// ToDo : FormLoad Events should happen (refresh)
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    this.keyId = eventArgs.Data.SelectedKeyId;

                    this.PopulateAgLandUseDetails(); 

                    //if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    //{
                    //    this.LockControls(false);
                    //}
                    //else
                    //{
                    //    this.LockControls(true);
                    //}


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

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockvalue)
        {

            this.RollYearPanel.Enabled = !lockvalue;
            this.UsePanel.Enabled = !lockvalue;
            this.DescriptionPanel.Enabled = !lockvalue;
            this.MethodPanel.Enabled = !lockvalue;
            this.FactorPanel.Enabled = !lockvalue;
            this.ValuePanel.Enabled = !lockvalue;
            this.MinimumValuePanel.Enabled = !lockvalue;
            this.TypePanel.Enabled = !lockvalue;

        }

        /// <summary>
        /// Handles the MouseHover event of the LandValuePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PictureBox1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.AglandToolTip.SetToolTip(this.picturebox1 , Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Clears the TIF Text.
        /// </summary>
        private void ClearAGLANDText()
        {
            this.FactorTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.TypeComboBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.UseTextBox.Text = string.Empty;
            this.MethodComboBox.Text = string.Empty;
            this.ValueTextBox.Text = string.Empty;
            this.MinimumValueTextBox.Text = string.Empty;
            this.TypeComboBox.SelectedIndex = -1;
            this.MethodComboBox.SelectedIndex = -1;  

        }

        /// <summary>
        /// Check the page mode to enable or disable the save, cancel Buttons for "selected Index  Changed Events In Combo Box"
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableSaveCancelButton(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.EditEnabled();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (!this.pageLoadStatus && this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
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
            if (string.IsNullOrEmpty(this.UseTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.TypeComboBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.MethodComboBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            //else if (this.ValidateRollYear())
            //{
            //    this.RollYearTextBox.Focus();
            //    sliceValidationFields.DisableNewMethod = false;
            //    sliceValidationFields.RequiredFieldMissing = false;
            //    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
            //}
            return sliceValidationFields;

        }

        ///<summary>
        /// Validate Roll Year
        /// </summary>
        private bool ValidateRollYear()
        {
            try
            {
                short tempRollYear;
                Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                if (tempRollYear < 1900 || tempRollYear > 2079)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// To save Agland
        /// </summary>
        private void SaveAgLand()
        {

            
            int returnValue = -1;
            short shortvalue;
            int invalue;
            decimal factorValue;
            int? AglandId = null;
            F34100AglandUseData.AglandUseDataTableRow AglandRow = AglandUseData.AglandUseDataTable.NewAglandUseDataTableRow();
            //F27081TIFDistrictData.F27081TIFDistrictDataTableRow TIFDistrictRow = tifDistrictData.F27081TIFDistrictDataTable.NewF27081TIFDistrictDataTableRow();
            if (this.newclick)
            {
                this.keyId = -99;
                if (this.DescriptionTextBox.Text != string.Empty)
                {
                    AglandRow.Description = this.DescriptionTextBox.Text;
                }
                //else
                //{
                //    AglandRow.Description = string.Empty;  
                //}
                if (this.UseTextBox.Text != string.Empty)
                {
                    AglandRow.Use = this.UseTextBox.Text;
                }
                short.TryParse(this.RollYearTextBox.Text, out shortvalue);
                AglandRow.RollYear = shortvalue;
                if (this.TypeComboBox.SelectedItem != null)
                {
                    int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out invalue);
                    AglandRow.AglandTypeID = invalue;

                }
                else
                {
                    AglandRow.AglandTypeID = -1;

                }
                if (this.MethodComboBox.SelectedItem != null)
                {
                    int.TryParse(this.MethodComboBox.SelectedValue.ToString(), out invalue);
                    AglandRow.AglandMethodID = invalue;

                }
                else
                {
                    AglandRow.AglandMethodID = -1;

                }

                AglandRow.Factor = (this.FactorTextBox.DecimalTextBoxValue / 100);
                if (this.ValueTextBox.Text != string.Empty)
                {
                    string value = this.ValueTextBox.Text.Trim().Replace("$", "");
                    AglandRow.Value = value.Trim();
                }
                else
                {
                    AglandRow.Value = "0.00";
                }
                if (this.MinimumValueTextBox.Text != string.Empty)
                {
                    string MinValue = this.MinimumValueTextBox.Text.Trim().Replace("$", "");
                    AglandRow.MinimumValue = MinValue.Trim();
                }
                else
                {
                    AglandRow.MinimumValue = "0.00";
                }

                AglandUseData.AglandUseDataTable.Rows.Add(AglandRow);
            }
            if (this.keyId != -99)
            {
                AglandUseData.AglandUseDataTable.Rows[0]["AglandID"] = this.keyId;
                AglandId = this.keyId;
                if (this.DescriptionTextBox.Text != string.Empty)
                {
                    AglandUseData.AglandUseDataTable.Rows[0]["Description"] = this.DescriptionTextBox.Text;
                }
                else
                {
                    AglandUseData.AglandUseDataTable.Rows[0]["Description"] = null; 
                }
                if (this.UseTextBox.Text != string.Empty)
                {
                    AglandUseData.AglandUseDataTable.Rows[0]["Use"] = this.UseTextBox.Text;
                }
                short.TryParse(this.RollYearTextBox.Text, out shortvalue);
                AglandUseData.AglandUseDataTable.Rows[0]["RollYear"] = shortvalue;
                if (this.TypeComboBox.SelectedItem != null)
                {
                    int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out invalue);
                    AglandUseData.AglandUseDataTable.Rows[0]["AglandTypeID"] = invalue;

                }
                else
                {
                    AglandRow.AglandTypeID = -1;

                }
                if (this.MethodComboBox.SelectedItem != null)
                {
                    int.TryParse(this.MethodComboBox.SelectedValue.ToString(), out invalue);
                    AglandUseData.AglandUseDataTable.Rows[0]["AglandMethodID"] = invalue;

                }
                else
                {
                    AglandRow.AglandMethodID = -1;

                }

                AglandUseData.AglandUseDataTable.Rows[0]["Factor"] = (this.FactorTextBox.DecimalTextBoxValue / 100);
                if (this.ValueTextBox.Text != string.Empty)
                {
                    string value = this.ValueTextBox.Text.Trim().Replace("$", "");
                    AglandUseData.AglandUseDataTable.Rows[0]["Value"] = value.Trim();
                }
                else
                {
                    AglandUseData.AglandUseDataTable.Rows[0]["Value"] = "0.00";
                }
                if (this.MinimumValueTextBox.Text != string.Empty)
                {
                    string MinValue = this.MinimumValueTextBox.Text.Trim().Replace("$", "");
                    AglandUseData.AglandUseDataTable.Rows[0]["MinimumValue"] = MinValue.Trim();
                }
                else
                {
                    AglandUseData.AglandUseDataTable.Rows[0]["MinimumValue"] = "0.00";
                }
            }
            
            AglandUseData.AglandUseDataTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Clear(); 
            tempDataSet.Tables.Add(AglandUseData.AglandUseDataTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            returnValue = this.form39100Controll.WorkItem.F34100_SaveAglandDetails(AglandId, tempDataSet.GetXml(), TerraScanCommon.UserId);
            this.newclick = false;
            if (returnValue != -1)
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = returnValue;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
            this.pageMode = TerraScanCommon.PageModeTypes.View;


        }

        /// <summary>
        /// Populates the TIF District fund detais.
        /// </summary>
        private void PopulateAgLandUseDetails()
        {
            this.pageLoadStatus = true;
            this.AglandUseData.Clear();
            //this.AglandUseData.AglandMethodDataTable.Clear();
            //this.AglandUseData.AglandTypeDataTable.Clear();
            this.AglandUseData.AglandUseDataTable.Clear();
             
            //this.TIFId = this.keyId;
            this.AglandUseData = this.form39100Controll.WorkItem.F34100_GetAglandDetails(this.keyId);
            int rollYear;
            //this.MethodData.Merge(this.AglandUseData.AglandMethodDataTable);
            //this.TypeData.Merge(this.AglandUseData.AglandTypeDataTable);
            int.TryParse(this.RollYearTextBox.Text, out rollYear);
            /// used for the display of combobox after leave active control
            if (this.AglandUseData.AglandMethodDataTable.Rows.Count > 0)
            {
                this.MethodComboBox.DataSource = this.AglandUseData.AglandMethodDataTable;
                this.MethodComboBox.DisplayMember = this.AglandUseData.AglandMethodDataTable.AglandMethodColumn.ColumnName.ToString();
                this.MethodComboBox.ValueMember = this.AglandUseData.AglandMethodDataTable.AglandMethodIDColumn.ColumnName.ToString();
            }
            else
            {
                this.MethodComboBox.SelectedIndex = -1;
            }

            if (this.AglandUseData.AglandTypeDataTable.Rows.Count > 0)
            {
                /// used for the display of combobox after leave active control

                this.TypeComboBox.DataSource = this.AglandUseData.AglandTypeDataTable;
                this.TypeComboBox.DisplayMember = this.AglandUseData.AglandTypeDataTable.AglandTypeColumn.ColumnName.ToString();
                this.TypeComboBox.ValueMember = this.AglandUseData.AglandTypeDataTable.AglandTypeIDColumn.ColumnName.ToString();
            }
            else
            {
                this.TypeComboBox.SelectedIndex = -1;
            }
            //// load the form f27081 using keyid and userid          
            if (this.AglandUseData.AglandUseDataTable.Rows.Count > 0)
            {
                this.RollYearTextBox.Text = this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.RollYearColumn.ColumnName].ToString();
                this.UseTextBox.Text = this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.UseColumn.ColumnName].ToString();
                this.DescriptionTextBox.Text = this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.DescriptionColumn.ColumnName].ToString();
                decimal factorValue;
                decimal.TryParse(this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.FactorColumn.ColumnName].ToString(), out factorValue);
                factorValue *= 100;
                //this.BaseAdjustmentTextBox.Text = factorAdjValue.ToString();
                this.FactorTextBox.Text = factorValue.ToString();  //this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.FactorColumn.ColumnName].ToString();
                this.ValueTextBox.Text = this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.ValueColumn.ColumnName].ToString();
                this.MinimumValueTextBox.Text = this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.MinimumValueColumn.ColumnName].ToString();
                int aglandTypeID;
                int.TryParse(this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.AglandTypeIDColumn.ColumnName].ToString(), out aglandTypeID);
                this.TypeComboBox.SelectedValue  = aglandTypeID;
                int aglandMethodID;
                int.TryParse(this.AglandUseData.AglandUseDataTable.Rows[0][this.AglandUseData.AglandUseDataTable.AglandMethodIDColumn.ColumnName].ToString(), out aglandMethodID);
                this.MethodComboBox.SelectedValue = aglandMethodID;
                this.LockControls(false);
            }
            else
            {
                this.ClearAGLANDText();
                this.LockControls(true);
            }
          

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;

        }


        private void F39100_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageLoadStatus = true;
                this.PopulateAgLandUseDetails();
                this.pageLoadStatus = false;
                RollYearTextBox.Focus();

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


        private void FactorTextBox_Leave(object sender, EventArgs e)
        {
           
            decimal maxPercentValue;
            //if (this.FactorTextBox.DecimalTextBoxValue<0)
            //{
            //    this.FactorTextBox.Text = "0.00 %";
            //}
            //else
            if (this.FactorTextBox.DecimalTextBoxValue <= 999.99M && this.FactorTextBox.DecimalTextBoxValue >= 0)
            {
                maxPercentValue = this.FactorTextBox.DecimalTextBoxValue;
                maxPercentValue = maxPercentValue / 100;
                this.FactorTextBox.Text = maxPercentValue.ToString(".00 %");
                //this.FactorTextBox.TextCustomFormat = "0.00 %";
            }
            else
            {
               
                this.FactorTextBox.Text = "0.00 %";
                this.FactorPanel.Focus();  

            }
                
            
        }

        private void Money_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((double)((TerraScanTextBox)sender).DecimalTextBoxValue > 922337203685477.5807)
                {
                    ((TerraScanTextBox)sender).Text = "0";
                }

            }
            catch (Exception ex)
            {
                ((TerraScanTextBox)sender).Text = "0";
            }
        }

        private void MinimumValueTextBox_Leave(object sender, EventArgs e)
        {
           decimal  value = 922337203685477.5807M;
           //decimal minValue = 0.00M;
           if (this.MinimumValueTextBox.Text.Contains("-"))
           {
               this.MinimumValueTextBox.Text = "$ 0.00"; 
           }
           if (string.IsNullOrEmpty(this.MinimumValueTextBox.Text))
           {
               this.MinimumValueTextBox.Text = "$ 0.00";
           }
           if (this.MinimumValueTextBox.DecimalTextBoxValue > value )//&& this.MinimumValueTextBox.DecimalTextBoxValue  < minValue)
            {
                this.MinimumValueTextBox.Text ="$ 0.00"; 
            }
                      
        }

        private void ValueTextBox_Leave(object sender, EventArgs e)
        {
            decimal value = 922337203685477.5807M;
           // decimal minValue = 0.00M;
            if (this.ValueTextBox.Text.Contains("-"))
            {
                this.ValueTextBox.Text = "$ 0.00";
            }
            if (string.IsNullOrEmpty(this.ValueTextBox.Text))
            {
                this.ValueTextBox.Text = "$ 0.00";
            }
            if (this.ValueTextBox.DecimalTextBoxValue > value)// && this.ValueTextBox.DecimalTextBoxValue < minValue)
            {
                this.ValueTextBox.Text   = "$ 0.00";
            }
           
        }

        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
                {
                    short tempRollYear;
                    Int16.TryParse(this.RollYearTextBox.Text.Trim(), out tempRollYear);
                    if (tempRollYear < 1900 || tempRollYear > 2079)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F2200Rollyear"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.RollYearTextBox.Focus();
                    }
                    
                }
              

            } 
           
           
        }

        private void TypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void TypeComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
            if ((this.TypeComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.TypeComboBox.Text.Trim()))
            {
                
                  // this.EditEnabled(); 
              
            }
            else
                {
                    this.TypeComboBox.Text = string.Empty;
                   // this.TypeComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            
        }

        private void TypeComboBox_TextChanged(object sender, EventArgs e)
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

        private void MethodComboBox_SelectionChangeCommitted(object sender, EventArgs e)
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

        private void MethodComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
            if ((this.MethodComboBox.SelectedValue != null) && !string.IsNullOrEmpty(this.MethodComboBox.Text.Trim()))
            {
                
                    //this.EditEnabled(); 
              
            }
            else
                {
                    this.MethodComboBox.Text = string.Empty;
                   // this.MethodComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            
        }

        private void MethodComboBox_TextChanged(object sender, EventArgs e)
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
    }
}
