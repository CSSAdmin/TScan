//----------------------------------------------------------------------------------
// <copyright file="F27081.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F27081 Form Slice - TIF DISTRICT 
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
// 07062016         priyadharshini       TSCO - D22081.F27081 TIF District - Multiple form changes
//***********************************************************************************

namespace D22081
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Globalization; 

    /// <summary>
    /// F27081-TIF DISTRICT FORM
    /// </summary>
    [SmartPart]
    public partial class F27081 : BaseSmartPart
    {
        #region member variables

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// form27081Controll variable.
        /// </summary>
        private F27081Controller form27081Controll;

        /// <summary>
        /// dataset contains subFund management details.
        /// </summary>
        private F27081TIFDistrictData tifDistrictData = new F27081TIFDistrictData();

        /// <summary>
        /// Used to store the listTIFDistrictDataTableDataTable
        /// </summary>
     //   private F27081TIFDistrictData.F27081TIFDistrictDataTableDataTable listTIFDistrictDataTable = new F27081TIFDistrictData.F27081TIFDistrictDataTableDataTable();

        private F27081TIFDistrictData subFundSelectionData = new F27081TIFDistrictData(); 

       

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool formMasterNew;

        ///// <summary>
        ///// TIFId variable is used to store the TIFId value default value is Null.
        ///// </summary>
        private int? TIFID;
        private int includedParcel;
        private string totalVlue;
        private string baseValue;
        private string excessvalue;
        ///<summary>
        ///used to check value changes or not
        ///</summary>
        private bool rolltext=false;

        ///<summary>
        ///used to newclick
        ///</summary>
        private bool newclick = false;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;
        private string sectionIndicatorText;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F27081"/> class.
        /// </summary>
        public F27081()
        {
            this.InitializeComponent();
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
        public F27081(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.masterFormNo = masterform;
            this.sectionIndicatorText = tabText;
            this.TIFID = keyID;
            this.TIFPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TIFPictureBox.Height, this.TIFPictureBox.Width, sectionIndicatorText, red, green, blue);
        }
        #endregion

        #region Event Publication

        ///// <summary>
        ///// Declare the event FormSlice_EditEnabled        
        ///// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>>FormSlice_EditEnabled;

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;
        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;
        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

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
        public F27081Controller Form27081Controll
        {
            get { return this.form27081Controll as F27081Controller; }
            set { this.form27081Controll = value; }
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
                 
                    if (this.tifDistrictData.F27081TIFDistrictDataTable.Rows.Count > 0)
                    {
                       eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else 
                    {
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }

                //if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                //{
                //    if (this.tifDistrictData.F27081TIFSubfundComboboxDataTable.Rows.Count > 0)
                //    {
                //    this.LockControls(false);
                //    }
                //    else
                //    {
                //    //this.LockControls(true);
                //    }
                //}
                //else
                //{
                //    this.LockControls(true);
                //}
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
            // TSCO - D22081.F27081 TIF District - Multiple form changes
            string returnMessage = string.Empty;
            if (this != null && this.IsDisposed != true)
            {
                if (this.slicePermissionField.deletePermission)
                {
                    ////this.Cursor = Cursors.WaitCursor;
                    returnMessage = this.form27081Controll.WorkItem.F27081_DeleteTIFDistrictDetails(Convert.ToInt32(this.TIFID), TerraScanCommon.UserId, false);
                    if (!string.IsNullOrEmpty(returnMessage))
                    {
                        MessageBox.Show(returnMessage, "Terrascan – TIF District Cannot be deleted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                        SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                        sliceEventArgs.MasterFormNo = this.masterFormNo;
                        sliceEventArgs.AllowNullRecordMode = false;
                        sliceEventArgs.WithoutKeyId = false;
                        this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                    }
                    else
                    {
                        returnMessage = this.form27081Controll.WorkItem.F27081_DeleteTIFDistrictDetails(Convert.ToInt32(this.TIFID), TerraScanCommon.UserId, true);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.Cursor = Cursors.Default;
                        SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                        sliceEventArgs.MasterFormNo = this.masterFormNo;
                        sliceEventArgs.AllowNullRecordMode = false;
                        sliceEventArgs.WithoutKeyId = false;
                        this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                    }
                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                    
                    short shortvalue;
                    short beginYear;
                    if (this.TIFID == -99)
                    {
                        this.TIFID = null;
                    }
                    F27081TIFDistrictData TifDistrict = new F27081TIFDistrictData();
                    F27081TIFDistrictData.F27801_TIFSaveDataTableRow TIFDistrictRow = TifDistrict.F27801_TIFSaveDataTable.NewF27801_TIFSaveDataTableRow();
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.TIFID = null;
                    }

                    ////if (this.newclick)
                    ////{
                    ////    this.keyId = -99;
                    ////}
                    //if (this.keyId != -99)
                    //{
                    //    //TIFDistrictRow.TIFID = this.keyId;
                    //    tifIdDistId = this.keyId;
                    //}
                    if (this.TIFCodeTextBox.Text != string.Empty)
                    {
                        TIFDistrictRow.TIFCode = this.TIFCodeTextBox.Text;
                    }
                    if (this.TIFDistrictTextBox.Text != string.Empty)
                    {
                        TIFDistrictRow.TIFDistrict = this.TIFDistrictTextBox.Text;
                    }
                    short.TryParse(this.RollYearTextBox.Text, out shortvalue);
                    TIFDistrictRow.RollYear = shortvalue;

                    if (!string.IsNullOrEmpty(this.SubFundComboBox.Text))
                    {
                        TIFDistrictRow.SubFundID = this.SubFundComboBox.SelectedValue.ToString();

                    }
                    TIFDistrictRow.CityName = this.CityNameTextBox.Text;
                    if (this.BeginYearTextBox.Text != string.Empty)
                    {
                        short.TryParse(this.BeginYearTextBox.Text, out beginYear);
                        TIFDistrictRow.BeginYear = beginYear;
                    }

                    if (this.SchoolNameTextBox.Text != string.Empty)
                    {
                        TIFDistrictRow.SchoolName = this.SchoolNameTextBox.Text;
                    }

                    TifDistrict.F27801_TIFSaveDataTable.Rows.Add(TIFDistrictRow);
                    TifDistrict.F27801_TIFSaveDataTable.AcceptChanges();
                    DataSet tempDataSet = new DataSet("root");
                    tempDataSet.Tables.Add(TifDistrict.F27801_TIFSaveDataTable.Copy());

                    tempDataSet.Tables[0].TableName = "Table";
                    int returnValue;
                    returnValue = this.form27081Controll.WorkItem.F27081_SaveDistrictDetails(TIFID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    this.TIFID = returnValue;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue; //Convert.ToInt32(this.centralID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;//Convert.ToInt32(this.centralID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //if (returnValue != -1)
                    //{
                    //    SliceReloadActiveRecord currentSliceInfo;
                    //    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    //    currentSliceInfo.SelectedKeyId = returnValue;
                    //    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                    //}
                    //this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //this.SaveTIFDistrict(); 
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(Convert.ToInt32(TIFID));
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(TIFID);
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                this.LockControls(true);
                this.EnableControls(false);
                //// ToDo : FormLoad Events should happen (refresh)
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
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
                        this.ClearTIFText();
                        this.LockControls(false);
                        this.EnableControls(true);
                        this.pageLoadStatus = false;
                        this.Cursor = Cursors.Default;
                        this.newclick = true;
                        // TSCO - D22081.F27081 TIF District - Multiple form changes
                        this.LoadSubFundCombox(0);
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.SubFundComboBox.SelectedIndex = -1;
                        this.TIFCodeTextBox.Focus();
                }
                else
                {
                    this.LockControls(true);
                    this.EnableControls(false);
                }
                // TSCO - D22081.F27081 TIF District - Multiple form changes
                this.RollYearPanel.Enabled = true;
                this.RollYearTextBox.Enabled = true;
                
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
                this.EnableControls(true);
            }
            else
            {
                this.LockControls(true);
                this.EnableControls(false);
            }

            this.PopulateTIFDistrictDetails(); 
            //this.FundButton.Focus();
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
                    this.TIFDistrictTextBox.Focus();
                    this.TIFID = eventArgs.Data.SelectedKeyId;
                    this.PopulateTIFDistrictDetails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                    //{
                    //    this.LockControls(false);
                    //    this.EnableControls(true);
                    //}
                    //else
                    //{
                    //    this.LockControls(true);
                        //    this.EnableControls(false);
                    //}
                    //if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    //{
                    //    this.LockControls(false);
                    //    this.EnableControls(true);
                    //}
                    //else
                    //{
                    //    this.LockControls(true);
                    //    this.EnableControls(false);
                    //}
                    this.TIFCodeTextBox.Focus();
                    //this.LockControls(true);

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

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F27081 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F27081_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageLoadStatus = true;
                this.TIFPictureBox.SendToBack();

                this.CustomizeParcelGrid();
                this.PopulateTIFDistrictDetails();
                this.pageLoadStatus = false; 
                this.TIFCodeTextBox.Focus();

                //modified on 20160705 22081_TIFDistrict_20160607_CO_RollYear_SubFund_Delete_Changes 
                this.RollYearTextBox.Enabled = false;
                
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

        #region Methods

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
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
               
            }
           
        }

        /// <summary>
        /// Populates the TIF District fund detais.
        /// </summary>
        private void PopulateTIFDistrictDetails()
        {
            // TSCO - D22081.F27081 TIF District - Multiple form changes
            int rollYear;
            this.pageLoadStatus = true;

                //Loading Combo box
            this.tifDistrictData = this.form27081Controll.WorkItem.F27081_GetTIFDistrictDetails(Convert.ToInt32(TIFID));
            if (this.tifDistrictData.F27081TIFDistrictDataTable.Rows.Count > 0)
            {
                // TSCO - D22081.F27081 TIF District - Multiple form changes
                this.SubFundComboBox.Text = string.Empty;
               // this.EditEnabled();
                this.SubFundComboBox.SelectedIndex = -1;
                rollYear = Convert.ToInt32(this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.RollYearColumn.ColumnName].ToString());
                this.LoadSubFundCombox(rollYear);

                this.TIFCodeTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.TIFCodeColumn.ColumnName].ToString();
                this.TIFDistrictTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.TIFDistrictColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.RollYearColumn.ColumnName].ToString();
                this.BeginYearTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.BeginYearColumn.ColumnName].ToString();
                this.CityNameTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.CityNameColumn.ColumnName].ToString();
                this.SchoolNameTextBox.Text = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.SchoolNameColumn.ColumnName].ToString();
                if (!string.IsNullOrEmpty(this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.SubFundIDColumn.ColumnName].ToString()))
                {
                    this.SubFundComboBox.SelectedValue = this.tifDistrictData.F27081TIFDistrictDataTable.Rows[0][this.tifDistrictData.F27081TIFDistrictDataTable.SubFundIDColumn.ColumnName].ToString();
                }
                this.ParcelDataGridView.DataSource = this.tifDistrictData.F27081_TIFGridDistrictDataTable.DefaultView;
                if (this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count > 0)
                {
                    this.TotalValuelabel.ForeColor = Color.White;
                    this.BaseValuLabel.ForeColor = Color.White;
                    this.Excesslabel.ForeColor = Color.White;
                    if (!string.IsNullOrEmpty(this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedTotalValue"].ToString()))
                    {
                        var strTemp = this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedTotalValue"].ToString();
                        double strNumb = Convert.ToDouble(strTemp);
                        var strResult = strNumb.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                        this.TotalValuelabel.Text = strResult.ToString();
                    }
                    else
                    {
                        this.TotalValuelabel.Text = "0.00";
                    }
                    if (!string.IsNullOrEmpty(this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedBaseValue"].ToString()))
                    {
                        var temp = this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedBaseValue"].ToString();
                        double strNumb1 = Convert.ToDouble(temp);
                        var strResult1 = strNumb1.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                        this.BaseValuLabel.Text = strResult1.ToString();
                    }
                    else
                    {
                        this.BaseValuLabel.Text = "0.00";
                    }
                    if (!string.IsNullOrEmpty(this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedExcessValue"].ToString()))
                    {
                        var temp2 = this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[0]["SummedExcessValue"].ToString();
                        double strNumb2 = Convert.ToDouble(temp2);
                        var strResult2 = strNumb2.ToString("#,##0.00##", CultureInfo.InvariantCulture);
                        this.Excesslabel.Text = strResult2.ToString();
                    }
                    else
                    {
                        this.Excesslabel.Text = "0.00";
                    }
                    //if (this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count > 5)
                    //{
                    //   // this.ParcelGridVScrollBar.Visible = true;
                    //   // this.ParcelGridVScrollBar.Enabled = true;
                    //}
                    //else
                    //{
                    //    //this.ParcelGridVScrollBar.Enabled = false;
                    //   // this.ParcelGridVScrollBar.Visible = false;
                    //}
                }
                this.ParcelRecordCount();
                
                 if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                  //if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.LockControls(false);
                    this.EnableControls(true);
                }
                else
                {
                    this.LockControls(true);
                    this.EnableControls(false);
                }
           }
           else
            {
                this.ClearTIFText();
                this.LockControls(true);
                this.EnableControls(false);
            }
           int.TryParse(this.RollYearTextBox.Text, out rollYear);

           //modified on 20160705 22081_TIFDistrict_20160607_CO_RollYear_SubFund_Delete_Changes
           this.RollYearTextBox.Enabled = false;
           this.pageMode = TerraScanCommon.PageModeTypes.View;
           this.pageLoadStatus = false;

        }
        /// <summary>
        /// Parcels the record count.
        /// </summary>
        private void ParcelRecordCount()
        {
            if (this.ParcelDataGridView.OriginalRowCount > 0)
            {
                if (this.ParcelDataGridView.OriginalRowCount >= 5)
                {
                    this.ParcelHeaderLabel.Text = "Included Parcels (" + (this.ParcelDataGridView.OriginalRowCount) + ")";
                }
                else
                {
                    this.ParcelHeaderLabel.Text = "Included Parcels (" + (this.ParcelDataGridView.OriginalRowCount) + ")";
                }
            }
            else
            {
                this.ParcelHeaderLabel.Text = "Included Parcels";
            }
        }
        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockvalue)
        {
            this.BeginDatepanel.Enabled = !lockvalue;
            this.SubFundPanel.Enabled = !lockvalue;
            this.TIFDistrictPanel.Enabled = !lockvalue;
            this.TIFCodePanel.Enabled = !lockvalue;
            this.RollYearPanel.Enabled = !lockvalue;
            this.BeginDatepanel.Enabled = !lockvalue;
            this.Citypanel.Enabled = !lockvalue;
            this.Contactpanel.Enabled = !lockvalue;
            this.ParcelPanel.Enabled = !lockvalue;
        }
        private void EnableControls(bool value)
        {
            this.ParcelDataGridView.Enabled = value;
        }

        /// <summary>
        /// Load SubFund Combobox
        /// </summary>
        /// <param name="rollYear"></param>
        private void LoadSubFundCombox(int rollYear)
        {
            this.subFundSelectionData = this.Form27081Controll.WorkItem.F27081_GetTIFComboBoxDetails(rollYear);
            if (this.subFundSelectionData != null)
            {
                this.SubFundComboBox.DataSource = this.subFundSelectionData.F27081TIFSubfundComboboxDataTable;
                this.SubFundComboBox.DisplayMember = this.subFundSelectionData.F27081TIFSubfundComboboxDataTable.SubFundColumn.ColumnName.ToString();
                this.SubFundComboBox.ValueMember = this.subFundSelectionData.F27081TIFSubfundComboboxDataTable.SubFundIDColumn.ColumnName.ToString();
            }
        }
        private void CustomizeParcelGrid()
        {
            try
            {
                this.ParcelDataGridView.AutoGenerateColumns = false;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelNumberColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictColumn.ColumnName;
                    this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.TotalValueColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.TotalValueColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.BaseValueColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.BaseValueColumn.ColumnName;
                this.ParcelDataGridView.Columns[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ExcessValueColumn.ColumnName].DataPropertyName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.ExcessValueColumn.ColumnName;
                this.ParcelDataGridView.PrimaryKeyColumnName = this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ToString();
                this.ParcelDataGridView.DataSource = this.tifDistrictData.F27081_TIFGridDistrictDataTable.DefaultView;
            }
            catch (Exception ex)
            {
            }
        }

       
        /// <summary>
        /// Validates the roll year.
        /// </summary>
        /// <returns>Validated Status</returns>
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
        /// Clears the TIF Text.
        /// </summary>
        private void ClearTIFText()
        {
            this.TIFCodeTextBox.Text = string.Empty;
            this.TIFDistrictTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SubFundComboBox.Text  = string.Empty;
            this.BeginYearTextBox.Text = string.Empty;
            this.CityNameTextBox.Text = string.Empty;
            this.SchoolNameTextBox.Text = string.Empty;
            this.tifDistrictData.F27081_TIFGridDistrictDataTable.Clear();
            this.ParcelDataGridView.DataSource = this.tifDistrictData.F27081_TIFGridDistrictDataTable.DefaultView;
            this.ParcelRecordCount();
            this.TotalValuelabel.Text =string.Empty;
            this.BaseValuLabel.Text = string.Empty;
            this.Excesslabel.Text = string.Empty;
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
            if (string.IsNullOrEmpty(this.TIFCodeTextBox.Text.Trim()))
            {
                this.TIFCodeTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
            }
            else if (this.ValidateRollYear())
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
            }
            else if (this.SubFundComboBox.SelectedIndex == -1 || String.IsNullOrEmpty(this.SubFundComboBox.Text))
            {
                this.SubFundComboBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            return sliceValidationFields;
        }


        #endregion


        /// <summary>
        /// Gets the form master new permission.
        /// </summary>
        /// <returns>bool</returns>
        private bool GetFormMasterNewPermission()
        {
            if ((this.Parent != null) && (this.Parent.Parent != null) && (this.Parent.Parent.Parent != null))
            {
                if (this.Parent.Parent.Parent is BaseSmartPart)
                {
                    return ((BaseSmartPart)this.Parent.Parent.Parent).PermissionFiled.newPermission;
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

        /// <summary>
        /// Handles the TextChanged Event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
                //if (!this.pageLoadStatus)
                //{
                //    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                //    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                //   // this.SubFundComboBox.Text = string.Empty;
                //   // this.rolltext = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
      
        /// <summary>
        /// Handles the TextChanged event of the SubFundComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SubFundComboBox_TextChanged(object sender, EventArgs e)
        {
               this.EditEnabled();
            
        }

        /// <summary>
        /// Validates the begin year.
        /// </summary>
        /// <returns></returns>
        private bool ValidateBeginYear()
        {
            try
            {
                short tempRollYear;
                Int16.TryParse(this.BeginYearTextBox.Text.Trim(), out tempRollYear);
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
        /// Handles the KeyPress event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == ' ')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }
        }

        private void ParcelDataGridView_MouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void ParcelDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(1))
            {
                
                if (this.ParcelDataGridView.CurrentColumnIndex.Equals(1))
                {
                    if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                    {
                        this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Style.ForeColor = Color.White;
                        string tempParcelId = this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Value.ToString();
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(30000);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = tempParcelId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
                //if (!string.IsNullOrEmpty(this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[e.RowIndex]["ParcelID"].ToString()))
                //{
                //    if (this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count > 0)
                //    {
                //        if (e.RowIndex < this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count)
                //        {
                //            string tempParcelId = this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[e.RowIndex]["ParcelID"].ToString();
                //            //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                //            FormInfo formInfo;
                //            formInfo = TerraScanCommon.GetFormInfo(30000);
                //            formInfo.optionalParameters = new object[1];
                //            formInfo.optionalParameters[0] = tempParcelId;
                //            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //        }
                //    }
                //}

            }
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(3))
            {
                if (this.ParcelDataGridView.CurrentColumnIndex.Equals(3))
                {
                    if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName].Value.ToString()) && (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictColumn.ColumnName].Value.ToString())))
                    {
                        string tempDistId = this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName].Value.ToString();
                         FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(11009);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = tempDistId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
                //if (!string.IsNullOrEmpty(this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[e.RowIndex]["DistrictID"].ToString()))
                //{
                //    if (this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count > 0)
                //    {
                //        if (e.RowIndex < this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows.Count)
                //        {
                //            string tempDistId = this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[e.RowIndex]["DistrictID"].ToString();
                //            //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                //            FormInfo formInfo;
                //            formInfo = TerraScanCommon.GetFormInfo(11009);
                //            formInfo.optionalParameters = new object[1];
                //            formInfo.optionalParameters[0] = tempDistId;
                //            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                //        }
                //    }
                //}
            }
        }

        private void ParcelDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue== 13)
            {
                if (this.ParcelDataGridView.CurrentRowIndex >= 0)
                {
                    if (this.ParcelDataGridView.CurrentColumnIndex.Equals(1))
                    {
                        if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                        {
                            string tempParcelId = this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Value.ToString();//this.tifDistrictData.F27081_TIFGridDistrictDataTable.Rows[e.RowIndex]["ParcelID"].ToString();
                            //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(30000);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = tempParcelId;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                    }
                    if (this.ParcelDataGridView.CurrentColumnIndex.Equals(3))
                    {
                        if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName].Value.ToString()) && (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictColumn.ColumnName].Value.ToString())))
                        {
                            string tempDistId = this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.DistrictIDColumn.ColumnName].Value.ToString();
                            //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(11009);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = tempDistId;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                    }
                }
            }
        }



        #region TIF Picture Box
        /// <summary>
        /// Handles the Click event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CentralAssessedPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CentralAssessedPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.TIFDistrictToolTip.SetToolTip(this.TIFPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion
        /// <summary>
        /// BeginYearTextBox_Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginYearTextBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.BeginYearTextBox.Text.Trim()))
            {
                int yearval;
                int.TryParse(this.BeginYearTextBox.Text.Trim(), out yearval);
                if (yearval <= 1899 || yearval >= 2080)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F2200Rollyear"), "TerraScan-InValid RollYear", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //this.RollYearTextBox.Text = "0";
                    this.BeginYearTextBox.Focus();
                }
            }
        }
        /// <summary>
        /// ParcelDataGridView_CellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelDataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(1))
            {
                if (this.ParcelDataGridView.CurrentColumnIndex.Equals(1))
                {
                    if (!string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelIDColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(this.ParcelDataGridView.Rows[this.ParcelDataGridView.CurrentRowIndex].Cells[this.tifDistrictData.F27081_TIFGridDistrictDataTable.ParcelNumberColumn.ColumnName].Value.ToString()))
                    {

                    }
                }
            }

        }
        /// <summary>
        /// RollYearTextBox_Leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            // TSCO - D22081.F27081 TIF District - Multiple form changes
            int rollYear=0;

            //Loading Combo box
            this.SubFundComboBox.SelectedIndex = -1;
            if(!String.IsNullOrEmpty(this.RollYearTextBox.Text))
                rollYear = Convert.ToInt32(this.RollYearTextBox.Text);
            this.LoadSubFundCombox(rollYear);
            // TSCO - D22081.F27081 TIF District - Multiple form changes
        }
    }
}