//----------------------------------------------------------------------------------
// <copyright file="F39110.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F39110 Form Slice - TOP DOLLARS
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
//
//
//***********************************************************************************


namespace D34110
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Web.Services.Protocols;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;   


    /// <summary>
    /// F39110-TOP DOLLAR FORM
    /// </summary>
    [SmartPart]

    public partial class F39110 : BaseSmartPart
    {
        #region Membervariables

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;
        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        ///<summary>
        /// Used to store TopDollarId
        ///</summary>
        private int TopDollarId;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool formMasterNew;

        /// <summary>
        /// form27081Controll variable.
        /// </summary>
        private F39110Controller form39110Controll;

        ///<summary>
        ///used to newclick
        ///</summary>
        private bool newclick = false;

        ///<summary>
        /// Used to identify isEditable
        /// </summary>
        private bool isEdit = false;
        /// <summary>
        /// dataset contains AglandUseData details.
        /// </summary>
        private F34110TopDollarData TopDollarData = new F34110TopDollarData();

        private F34110TopDollarData.TopDollarDataTableDataTable topDollarTable = new F34110TopDollarData.TopDollarDataTableDataTable();  

        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F39110"/> class.
        /// </summary>
        public F39110()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F34110"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F39110(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.PictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PictureBox.Height, this.PictureBox.Width, tabText, red, green, blue);
        }
        #endregion constructors

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
        public F39110Controller  Form39110Controll
        {
            get { return this.form39110Controll as F39110Controller; }
            set { this.form39110Controll = value; }
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
                    //this.formMasterNew = this.GetFormMasterNewPermission();

                    //if (this.tifDistrictData.F27081TIFSubfundComboboxDataTable.Rows.Count > 0)
                    //{
                    //    eventArgs.Data.FlagInvalidSliceKey = false;
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

                    this.LockControls(false);

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
                    this.ClearTopDollar();
                    //this.keyId = -99;
                    this.LockControls(false);
                    this.pageLoadStatus = false;
                    this.Cursor = Cursors.Default;
                    this.newclick = true;
                    ///USED TO DISPLAY SUBFUND COMBOBOX TO DISPLAY ITEMS

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

            this.PopulateTopDollarDetails();
            //this.FundButton.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.Cursor = Cursors.Default;
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
                    this.SaveTopDollar();
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
                    //this.TIFDistrictTextBox.Focus();  
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.PopulateTopDollarDetails();
                    //if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    //{
                    //    this.LockControls(false);
                    //}
                    //else
                    //{
                    //    this.LockControls(true);
                    //}
                    //this.TIFDistrictTextBox.Focus();

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
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission)
            {
                this.form39110Controll.WorkItem.F34110_DeleteTopDollarDetails(this.keyId, TerraScan.Common.TerraScanCommon.UserId);
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




        #region Methods

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            return sliceValidationFields;

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
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockvalue)
        {

            this.AgEqualPanel.Enabled = !lockvalue;
            this.NonAgEqualPanel.Enabled = !lockvalue;
            this.CropPanel.Enabled = !lockvalue;
            this.NonCropPanel.Enabled = !lockvalue;
            this.CountyPanel.Enabled = !lockvalue;
            this.RollYearPanel.Enabled = !lockvalue;

        }

        /// <summary>
        /// Clears the TopDollarText.
        /// </summary>
        private void ClearTopDollar()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.AgEqualTextBox.Text = string.Empty;
            this.NonAgEqualTextBox.Text = string.Empty;
            this.CropTextBox.Text = "$ 0.00";
            this.NonCropTextBox.Text = "$ 0.00";
            this.CountyTextBox.Text = string.Empty;


        }

        /// <summary>
        /// Populates the TIF District fund detais.
        /// </summary>
        private void PopulateTopDollarDetails()
        {
            this.pageLoadStatus = true;
            this.TopDollarId  = this.keyId;
            this.TopDollarData  = this.form39110Controll.WorkItem.F34110_GetTopDollarDetails(this.keyId);  //   .WorkItem.F27081_GetTIFDistrictDetails(this.keyId, TerraScanCommon.UserId);
            int rollYear;

            //// load the form f27081 using keyid and userid          
            if (this.TopDollarData.TopDollarDataTable.Rows.Count > 0)
            {
                this.AgEqualTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.AgEqualRateColumn.ColumnName].ToString();
                this.NonAgEqualTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.NonAgEqualRateColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.RollYearColumn.ColumnName].ToString();
                this.CropTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.CropTopDollarColumn.ColumnName].ToString();
                this.NonCropTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.NonCropTopDollarColumn.ColumnName].ToString();
                this.CountyTextBox.Text = this.TopDollarData.TopDollarDataTable.Rows[0][this.TopDollarData.TopDollarDataTable.CountyFactorColumn.ColumnName].ToString();

            }
            else
            {
                this.ClearTopDollar();
                this.LockControls(true);
            }
            int.TryParse(this.RollYearTextBox.Text, out rollYear);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.pageLoadStatus = false;

        }

        ///<summary>
        /// Used to Calculate NonCrop Dollar
        /// </summary>
        private void CalculateNonCropDollar(bool isChange,decimal crop, decimal  county)
        {
            decimal  nonCrop=0;
            
            //county = this.CountyTextBox.DecimalTextBoxValue;
            //if ((!string.IsNullOrEmpty(this.CropTextBox.Text)) && (!string.IsNullOrEmpty(this.CountyTextBox.Text)))
            //{
               
                    this.TopDollarData = this.form39110Controll.WorkItem.F34110_CropTopDollar(crop, county);
                    if (this.TopDollarData.NonCropDollarDataTable.Rows.Count > 0)
                    {
                        decimal.TryParse(TopDollarData.NonCropDollarDataTable.Rows[0][0].ToString(), out nonCrop);
                    }
                    decimal maxValue = 922337203685477.5807M;
                    if (nonCrop > maxValue)
                    {
                        if (isChange)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("NonCropDollarExceeds"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.CountyTextBox.Text = "0.00000";
                            this.CountyTextBox.Focus();  
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("NonCropDollarExceeds"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.CropTextBox.Text = "$ 0.00";
                            this.CropTextBox.Focus();  
                        }
                        this.NonCropTextBox.Text = "$ 0.00";
                    }
                    else
                    {
                        this.NonCropTextBox.Text = Convert.ToString(nonCrop);
                    }

                
            //}
        }

        /// <summary>
        /// To save TopDollar
        /// </summary>
        private void SaveTopDollar()
        {
            int returnValue = -1;
            //int? TopDollarID = null;
            F34110TopDollarData.TopDollarDataTableDataTable topDollarTable = new F34110TopDollarData.TopDollarDataTableDataTable();
            F34110TopDollarData.TopDollarDataTableRow topDollarRow = topDollarTable.NewTopDollarDataTableRow();

            if (this.newclick)
            {
                this.keyId = -99;
                topDollarRow.TopDollarID = this.keyId;
                if (this.RollYearTextBox.Text != string.Empty)
                {
                    int roll;
                    int.TryParse(this.RollYearTextBox.Text, out roll);
                    topDollarRow.RollYear = roll;

                }
                if (this.AgEqualTextBox.Text != string.Empty)
                {
                    topDollarRow.AgEqualRate = this.AgEqualTextBox.DecimalTextBoxValue;
                }
                if (this.NonAgEqualTextBox.Text != string.Empty)
                {
                    topDollarRow.NonAgEqualRate = this.NonAgEqualTextBox.DecimalTextBoxValue;
                }
                if (this.CropTextBox.Text != string.Empty)
                {
                    decimal Crop;
                    string value = this.CropTextBox.Text.Replace("$", "");
                    decimal.TryParse(value, out Crop);
                    topDollarRow.CropTopDollar = Crop;
                }
                if (this.NonCropTextBox.Text != string.Empty)
                {
                    decimal Noncrop;
                    string value = this.NonCropTextBox.Text.Replace("$", "");
                    decimal.TryParse(value, out Noncrop);
                    topDollarRow.NonCropTopDollar = Noncrop;
                }
                if (this.CountyTextBox.Text != string.Empty)
                {
                    topDollarRow.CountyFactor = this.CountyTextBox.DecimalTextBoxValue;
                }
                topDollarTable.Rows.Add(topDollarRow);
                

            }
            else
            {
                topDollarTable.Clear(); 
                F34110TopDollarData.TopDollarDataTableRow updateRow = topDollarTable.NewTopDollarDataTableRow();
                updateRow.TopDollarID = this.keyId;

                if (this.RollYearTextBox.Text != string.Empty)
                {
                    int roll;
                    int.TryParse(this.RollYearTextBox.Text, out roll);
                    updateRow.RollYear = roll;

                }
                if (this.AgEqualTextBox.Text != string.Empty)
                {
                    updateRow.AgEqualRate = this.AgEqualTextBox.DecimalTextBoxValue;
                }
                if (this.NonAgEqualTextBox.Text != string.Empty)
                {
                    updateRow.NonAgEqualRate = this.NonAgEqualTextBox.DecimalTextBoxValue;
                }
                if (this.CropTextBox.Text != string.Empty)
                {
                    decimal Crop;
                    string value = this.CropTextBox.Text.Replace("$", "");
                    decimal.TryParse(value, out Crop);
                    updateRow.CropTopDollar = Crop;
                }
                if (this.NonCropTextBox.Text != string.Empty)
                {
                    decimal Noncrop;
                    string value = this.NonCropTextBox.Text.Replace("$", "");
                    decimal.TryParse(value, out Noncrop);
                    updateRow.NonCropTopDollar = Noncrop;
                }
                if (this.CountyTextBox.Text != string.Empty)
                {
                    updateRow.CountyFactor = this.CountyTextBox.DecimalTextBoxValue;
                }
                topDollarTable.Rows.Add(updateRow);

            }
            //TopDollarData.TopDollarDataTable.AcceptChanges();   
            //DataSet tempDataSet = new DataSet("Root");
            //tempDataSet.Tables.Add(TopDollarData.TopDollarDataTable.Copy());
            //tempDataSet.Tables[0].TableName = "Table";
            this.newclick = false;
             returnValue = this.form39110Controll.WorkItem.F34110_SaveTopDollarDetails(this.keyId, TerraScanCommon.GetXmlString(topDollarTable), TerraScanCommon.UserId);
            if (returnValue != -1)
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = returnValue;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
            this.pageMode = TerraScanCommon.PageModeTypes.View;


        }


        #endregion


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

        private void F39110_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageLoadStatus = true;
                this.PopulateTopDollarDetails();
                this.pageLoadStatus = false;
                this.RollYearTextBox.Focus();
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

        private void PictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.topDollarToolTip.SetToolTip(this.PictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        private void CountyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                this.isEdit = true;
                this.EditEnabled();
            }
        }

        private void CropTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.pageLoadStatus)
            {
                this.isEdit = true;
                this.EditEnabled();
            }
        }

        private void CropTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.CropTextBox.Text))
                {
                    this.CropTextBox.Text = "$ 0.00";
                }
                else if (this.CropTextBox.Text.Contains("("))
                {
                    this.CropTextBox.Text = "0.00000";
                }
                else
                {
                    decimal CropVal;
                    decimal MaxValue = 922337203685477.5807M;
                    string value = this.CropTextBox.Text.Replace("$", "");
                    decimal.TryParse(value, out CropVal);
                   // decimal.TryParse(this.CropTextBox.DecimalTextBoxValue.ToString(), out CropVal);
                    if (CropVal > MaxValue)
                    {
                        this.CropTextBox.Text = "$ 0.00";
                    }
                    
                }
                if (this.isEdit)
                {
               
                    if (!string.IsNullOrEmpty(this.CountyTextBox.Text.Trim()))
                    {
                        decimal crop, county;
                        string value = this.CropTextBox.Text.Replace("$", "");
                        decimal.TryParse(value, out crop);
                        decimal.TryParse(this.CountyTextBox.Text, out county);
                        this.CalculateNonCropDollar(false,crop,county );
                    }
                    this.EditEnabled();
                }
                this.isEdit = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CountyTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    if (this.isEdit)
                    {
                        if (this.CountyTextBox.Text.Contains("-"))
                        {
                            this.CountyTextBox.Text = string.Empty;
                        }
                        if (string.IsNullOrEmpty(this.CountyTextBox.Text))
                        {
                            this.CountyTextBox.Text = string.Empty;
                        }
                        
                        if (!string.IsNullOrEmpty(this.CropTextBox.Text.Trim()))
                        {
                            decimal crop, county;
                            string value = this.CropTextBox.Text.Replace("$", "");
                            decimal.TryParse(value, out crop);
                            decimal.TryParse(this.CountyTextBox.Text, out county);
                            this.CalculateNonCropDollar(true,crop,county );
                        }
                        this.EditEnabled();
                    }
                }
                this.isEdit = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void AgEqualTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                
                    if (this.AgEqualTextBox.Text.Contains("-"))
                    {
                        this.AgEqualTextBox.Text = "0.0000";
                    }
                    //this.EditEnabled();
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void NonAgEqualTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                   if (this.NonAgEqualTextBox.Text.Contains("-"))
                    {
                        this.NonAgEqualTextBox.Text = "0.0000";
                    }
                    //this.EditEnabled();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


    }
}
