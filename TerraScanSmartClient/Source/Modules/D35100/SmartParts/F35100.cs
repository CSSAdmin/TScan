//--------------------------------------------------------------------------------------------
// <copyright file="F35100.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 may 2007      Ramya.D             Created
// 28 Apr 2009      Khaja               made changes to fix Bug 4029 
// 07252016         priyadharshini      TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections;
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
    using D35100;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    /// <summary>
    /// F35100 class file
    /// </summary>
    [SmartPart]
    public partial class F35100 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// comboChange
        /// </summary>
        private bool comboChange;

        /// <summary>
        /// comboChange
        /// </summary>
        private bool rollYearChange;

        /// <summary>
        /// cancelChange
        /// </summary>
        private bool cancelChange;

        /// <summary>
        /// lockChange
        /// </summary>
        private bool lockChange;

        /// <summary>
        /// deleteFlag
        /// </summary>
        private bool deleteFlag;

        /// <summary>
        /// saveCancelflag
        /// </summary>
        private bool saveCancelflag;

        /// <summary>
        /// parentChange
        /// </summary>
        private bool parentChange;

        /// <summary>
        /// parentChangeflag
        /// </summary>
        private bool parentChangeflag;

        /// <summary>
        /// previousGRPText
        /// </summary>
        private string previousGRPText;

        /// <summary>
        /// previousNbhText
        /// </summary>
        private string previousNbhText;

        /// <summary>
        /// previouGPSelectedValue
        /// </summary>
        private int previouGPSelectedValue = -1;

        /// <summary>
        /// previouParentSelectedValue
        /// </summary>
        private int previouParentSelectedValue = -1;

        /// <summary>
        /// returnValue
        /// </summary>
        private int returnValue = 1;

        /// <summary>
        /// parentNBHDID
        /// </summary>
        private int parentNBHDID;

        /// <summary>
        /// lockNbhdid
        /// </summary>
        private int lockNbhdid;

        /// <summary>
        /// tempTypeSelectedValue
        /// </summary>
        private int tempTypeSelectedValue;

        /// <summary>
        /// lockNbhdid
        /// </summary>
        private int parentneighborFlag;

        /// <summary>
        /// grandParentID
        /// </summary>
        private int grandParentID;

        /// <summary>
        /// previouParentSelectedValue
        /// </summary>
        private int parentSelectedValue = -1;

        /// <summary>
        /// saveButtonFlag
        /// </summary>
        private bool cancelButtonFlag;

        /// <summary>
        /// typeComboData
        /// </summary>
        private CommonData typeComboData = new CommonData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// rollyear
        /// </summary>
        private int rollyear;

        /// <summary>
        /// rollyear
        /// </summary>
        private int grantParent;

        /// <summary>
        /// rollyear
        /// </summary>
        private int saveFlag;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int parentGrandparentbuttonflag;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        ///grandParentButtonFlag
        /// </summary>
        private bool grandParentButtonFlag;

        /// <summary>
        /// previousGRPText
        /// </summary>
        private bool typeComboSelected;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Instance of F35001 Controller to call the WorkItem
        /// </summary>
        private F35100Controller form35100Controller;

        /// <summary>
        /// getNeighborhoodHeaderData
        /// </summary>
        private F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow getNeighborhoodHeaderData;

        /// <summary>
        /// form35100NeighborhoodHeaderData
        /// </summary>
        private F35100NeighborhoodHeaderData form35100NeighborhoodHeaderData = new F35100NeighborhoodHeaderData();

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15013"/> class.
        /// </summary>
        public F35100()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F15013"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35100(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.NeighborhoodPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodPictureBox.Height, this.NeighborhoodPictureBox.Width, tabText, red, green, blue);
            ////this.AllOwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AllOwnersPictureBox.Height, this.AllOwnersPictureBox.Width, "All Owners", 174, 150, 94);   ////todo remove hard code value                     
        }
        #endregion

        #region Eventpublication

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;


        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for SetActiveKeyid
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveKeyId;

        
        /// <summary>
        /// event publication to intimate form master about the selected keyid doesnot exists
        /// due to change in the filter option
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_OnChange_Neighborhood, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9033_OnChange_Neighborhood;

        

        /// <summary>
        /// Called when [D9030_ F9033_ on filter_ key id reset].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_OnChange_Neighborhood(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9033_OnChange_Neighborhood != null)
            {
                this.D9030_F9033_OnChange_Neighborhood(this, eventArgs);
            }
        }

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F35100Controller F35100Control
        {
            get { return this.form35100Controller as F35100Controller; }
            set { this.form35100Controller = value; }
        }


        public bool NBHDBtn
        {
            get { return this.NeighborhoodCopyButton.Enabled; }
            set { this.NeighborhoodCopyButton.Enabled = value; }
        }
        #endregion Properties

        #region EventSubcription

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.EnableControls(true);
            this.ShowPanels(true);
            if (!this.saveCancelflag)
            {
                this.CancelButton_Click();
            }
            ////this.saveCancelflag = true;
            this.parentGrandparentbuttonflag = 2;
            this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

            ////this.PermissionControlLock(!this.PermissionFiled.editPermission);
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = false;
                    this.TypeComboBox.Enabled = false;
                }
            }

            this.saveFlag = 1;
        }

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;


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

                    if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
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
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if (!this.saveCancelflag)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        if (!this.deleteFlag)
                        {
                            this.ShowControls(true);
                            this.ShowPanels(true);
                        }

                        this.saveFlag = 1;
                        this.GetNeighborhoodHeaderDetails();
                        this.AppraisalGreenpictureBox.Visible = true;
                        this.AssessmentGreenpictureBox.Visible = true;
                        this.AdminGreenpictureBox.Visible = true;
                        this.parentGrandparentbuttonflag = 4;
                        this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
                        if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                        {
                            this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                            if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                            {
                                this.RollyearTextBox.Visible = true;
                                this.RollyearTextBox.Enabled = false;
                                this.TypeComboBox.Enabled = false;
                            }
                        }
                    }
                }

                ////this.saveFlag = 1;
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
            if (this.slicePermissionField.newPermission)
            {
                this.NewButton_Click();
                this.ControlLock(false);
                this.parentGrandparentbuttonflag = 4;
            }
            else
            {
                ////this.NewButton_Click();
                this.EnableControls(false);
                this.ShowPanels(false);
                this.AppraisalGreenpictureBox.Visible = true;
                this.AppraisalGreenpictureBox.Enabled = false;
                this.AssessmentGreenpictureBox.Visible = true;
                this.AssessmentGreenpictureBox.Enabled = false;
                this.AdminGreenpictureBox.Visible = true;
                this.AdminGreenpictureBox.Enabled = false;
                this.ClearNeighborhoodHeaderDetails();
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
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                this.parentGrandparentbuttonflag = 4;
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
            //// this.Cursor = Cursors.WaitCursor;
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                if (!this.saveCancelflag)
                {
                    this.SaveButton_Click();
                }
                ////29this.saveCancelflag = true;
                this.parentGrandparentbuttonflag = 1;
                this.PermissionControlLock(!this.PermissionFiled.editPermission);
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
                if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                {
                    this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                    if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                    {
                        this.RollyearTextBox.Visible = true;
                        this.RollyearTextBox.Enabled = false;
                        this.TypeComboBox.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.DeleteButton_Click();
            }
        }

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
        /// Called when [form slice_ edit enabled].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_EditEnabled, ThreadOption.UserInterface)]
        public void OnFormSlice_EditEnabled(object sender, DataEventArgs<int> eventArgs)
        {

            if (eventArgs.Data == this.masterFormNo)
            {
                this.NeighborhoodCopyButton.Enabled = false;
            }

        }


        #endregion EventSubcription

        /// <summary>
        /// F35100_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F35100_Load(object sender, EventArgs e)
        {

            try
            {
                this.saveCancelflag = true;
                this.FlagSliceForm = true;
                this.NeighborhoodTextBox.Focus();
                this.GetNeighborhoodHeaderDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.saveCancelflag = false;
                this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
                if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                {
                    this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                    if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                    {
                        this.RollyearTextBox.Visible = true;
                        this.RollyearTextBox.Enabled = false;
                        this.TypeComboBox.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Reload after copy of neighbourhood
        /// </summary>
        private void CopyMethod()
        {

            this.saveCancelflag = true;
            this.FlagSliceForm = true;
            this.NeighborhoodTextBox.Focus();
            this.GetNeighborhoodHeaderDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.saveCancelflag = false;
            this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = false;
                    this.TypeComboBox.Enabled = false;
                }
            }
        }

        /// <summary>
        /// FillTypeCombo
        /// </summary>
        private void FillTypeCombo()
        {
            Hashtable datas = new Hashtable();
            datas.Add("Neighborhood", 1);
            datas.Add("Parent", 2);
            datas.Add("Grand Parent", 3);
            this.typeComboData.LoadGeneralComboData(datas);
            this.TypeComboBox.DisplayMember = this.typeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.TypeComboBox.ValueMember = this.typeComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.TypeComboBox.DataSource = this.typeComboData.ComboBoxDataTable;
            this.typeComboData.ComboBoxDataTable.DefaultView.Sort = "KeyId";
            this.TypeComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// GetNeighborhoodHeaderDetails
        /// </summary>
        private void GetNeighborhoodHeaderDetails()
        {
            this.formLoad = true;
            this.form35100NeighborhoodHeaderData.EnforceConstraints = false;
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.keyId);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.cancelChange = true;
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                this.FillTypeCombo();
                this.TypeComboBox.SelectedValue = this.getNeighborhoodHeaderData.NBHDType;
                this.tempTypeSelectedValue = this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString());

                ////Ramya
                this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.Neighborhood.ToString();
                this.RollyearTextBox.Text = this.getNeighborhoodHeaderData.RollYear.ToString();
                ////Ramya
                this.FillGrantParentNeighborhoodCombo(-1);
                this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.PNBHDID;
                this.parentNBHDID = this.getNeighborhoodHeaderData.PNBHDID;
                this.previouParentSelectedValue = this.getNeighborhoodHeaderData.PNBHDID;
                this.GrandparentNeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.GrandParentName;
                this.previouGPSelectedValue = this.getNeighborhoodHeaderData.GrandParent;
                //// Added By Guhan 
                if (this.TypeComboBox.SelectedIndex == 1)
                {
                    this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.NBHDID;
                }
                ////Ramya
                //// this.RollyearTextBox.Text = this.getNeighborhoodHeaderData.RollYear.ToString();
                ////Ramya

                this.Statusofcontrols(this.getNeighborhoodHeaderData.NBHDType);
                this.NeighborhoodIDTextBox.Text = this.getNeighborhoodHeaderData.NBHDID.ToString();
                this.ParentNeighborhoodIDTextBox.Text = this.getNeighborhoodHeaderData.PNBHDID.ToString();
                this.NeighborhoodDescriptionTextBox.Text = this.getNeighborhoodHeaderData.Description;
                if (this.getNeighborhoodHeaderData.MarketReview == "(Empty)")
                {
                    this.MarketReviewTextBox.Text = string.Empty;
                }
                else
                {
                    this.MarketReviewTextBox.Text = this.getNeighborhoodHeaderData.MarketReview;
                }

                this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.PNBHDID;
                if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = false;
                    this.TypeComboBox.Enabled = false;
                }
                else
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = true;
                    this.TypeComboBox.Enabled = true;
                }

                if (this.getNeighborhoodHeaderData.LockAppraisalBy != 0)
                {
                    this.AppraisalRedRedpictureBox.Visible = true;
                    this.AppraisalRedRedpictureBox.Enabled = true;
                    this.AppraisalRedRedpictureBox.BringToFront();
                    this.AppraisalGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AppraisalGreenpictureBox.Visible = true;
                    this.AppraisalGreenpictureBox.Enabled = true;
                    this.AppraisalGreenpictureBox.BringToFront();
                    this.AppraisalRedRedpictureBox.Visible = false;
                }

                if (this.getNeighborhoodHeaderData.LockAssessmentBy != 0)
                {
                    this.AssessmentRedpictureBox.Visible = true;
                    this.AssessmentRedpictureBox.Enabled = true;
                    this.AssessmentRedpictureBox.BringToFront();
                    this.AssessmentGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AssessmentGreenpictureBox.Visible = true;
                    this.AssessmentGreenpictureBox.Enabled = true;
                    this.AssessmentGreenpictureBox.BringToFront();
                    this.AssessmentRedpictureBox.Visible = false;
                }

                if (this.getNeighborhoodHeaderData.LockAdminBy != 0)
                {
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminRedpictureBox.Enabled = true;
                    this.AdminRedpictureBox.BringToFront();
                    this.AdminGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AdminGreenpictureBox.Visible = true;
                    this.AdminGreenpictureBox.Enabled = true;
                    this.AdminGreenpictureBox.BringToFront();
                    this.AdminRedpictureBox.Visible = false;
                }

                this.NeighborhoodCopyButton.Enabled = true;
            }
            else
            {
                this.AppraisalGreenpictureBox.Visible = true;
                this.AppraisalGreenpictureBox.BringToFront();
                this.AssessmentGreenpictureBox.Visible = true;
                this.AssessmentGreenpictureBox.BringToFront();
                this.AdminGreenpictureBox.Visible = true;
                this.AdminGreenpictureBox.BringToFront();
                this.ShowControls(false);
                this.ShowPanels(false);
                this.ClearControls();
            }

            this.formLoad = false;
            if (this.lockChange)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }

            this.NeighborhoodTextBox.Select();
            this.NeighborhoodTextBox.Focus();
            ////   this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// FillGrantParentNeighborhoodCombo
        /// </summary>
        /// <param name="parentNeighborhoodIndex">parentNeighborhoodIndex</param>
        private void FillGrantParentNeighborhoodCombo(int parentNeighborhoodIndex)
        {
            int type = -1;
            int parentIndex;

            if (this.TypeComboBox.SelectedValue != null)
            {
                int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out type);
            }

            int.TryParse(this.RollyearTextBox.Text.Trim(), out this.rollyear);

            if (this.rollyear != 0)
            {
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, type, parentNeighborhoodIndex);

                if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                {
                    this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                    this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                    this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                    this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;

                    this.ParentNeighborhoodComboBox.SelectedValue = this.parentNBHDID;
                    if (this.ParentNeighborhoodComboBox.SelectedValue == null)
                    {
                        //// int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out this.parentNBHDID);
                        this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                    }

                    this.comboChange = false;
                }
                else
                {
                    this.ParentNeighborhoodComboBox.DataSource = null;
                    this.ParentNeighborhoodComboBox.Enabled = false;
                    this.ParentNeighborhoodpictureBox.Enabled = false;
                }

                if (this.form35100NeighborhoodHeaderData.f35100GrpNeighborhood.Rows.Count > 0)
                {
                    this.grantParent = Convert.ToInt32(this.form35100NeighborhoodHeaderData.f35100GrpNeighborhood.Rows[0][this.form35100NeighborhoodHeaderData.f35100GrpNeighborhood.NBHDIDColumn].ToString());
                }

                if (this.TypeComboBox.SelectedIndex == 1)
                {
                    if (this.tempTypeSelectedValue > this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()))
                    {
                        this.FillParentComboGrandToParent();
                    }
                    else
                    {
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, 2, parentNeighborhoodIndex);
                        if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                        {
                            this.form35100NeighborhoodHeaderData.f35100GrpNeighborhood.DefaultView.Sort = "Neighborhood ASC";
                            this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                            this.ParentNeighborhoodComboBox.SelectedValue = this.parentNBHDID;
                            if (this.ParentNeighborhoodComboBox.SelectedValue == null)
                            {
                                this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                            }
                        }
                        else if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                        {
                            this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                            this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                            this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.GrandParent;
                            if (this.ParentNeighborhoodComboBox.SelectedValue == null)
                            {
                                this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                            }

                            int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out this.parentSelectedValue);
                        }
                    }

                    if (this.comboChange)
                    {
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, 2, parentNeighborhoodIndex);
                        if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                        {
                            this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                            this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                            this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                            this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                        }
                    }

                    this.ParentNeighborhoodComboBox.Enabled = true;
                    this.ParentNeighborhoodpictureBox.Visible = true;
                    this.ParentNeighborhoodpictureBox.Enabled = true;
                    if (this.ParentNeighborhoodComboBox.DataSource == null)
                    {
                        this.ParentNeighborhoodComboBox.Enabled = false;
                        this.ParentNeighborhoodpictureBox.Enabled = false;
                    }
                }
                else if (this.TypeComboBox.SelectedIndex == 0 && (!string.IsNullOrEmpty(this.ParentNeighborhoodComboBox.Text)))
                {
                    this.ParentNeighborhoodComboBox.Enabled = true;
                    this.GrandparentNeighborhoodTextBox.Enabled = true;
                    this.ParentNeighborhoodpictureBox.Enabled = true;
                    this.GrantParentNeighborhoodpictureBox.Enabled = true;
                    this.ParentNeighborhoodpictureBox.Visible = true;
                    this.ParentNeighborhoodpictureBox.Enabled = true;
                    this.GrantParentNeighborhoodpictureBox.Visible = true;
                    this.GrantParentNeighborhoodpictureBox.Enabled = true;
                    if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                    {
                        this.EnableLock(false);
                    }
                    else
                    {
                        this.EnableLock(true);
                    }

                    int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out parentIndex);
                    int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out this.parentSelectedValue);
                    this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, parentIndex);

                    if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                    {
                        this.GrandparentNeighborhoodTextBox.Text = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn].ToString();
                        this.grandParentID = Convert.ToInt32(this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString());
                    }
                    ////this.priviousSelectedGrandText = this.GrandparentNeighborhoodTextBox.Text.Trim();
                    ////this.grandParentID = Convert.ToInt32(this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString());
                    if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
                    {
                        if (this.tempTypeSelectedValue >= this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()))
                        {
                            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()), this.ConvertStringToInt(this.NeighborhoodIDTextBox.Text.Trim()));
                            {
                                if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                                {
                                    this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                                    this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                                    this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                                    this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                                    ////this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                                    this.ParentNeighborhoodComboBox.SelectedValue = this.parentNBHDID;
                                    if (this.ParentNeighborhoodComboBox.SelectedValue == null)
                                    {
                                        this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                                    }
                                }
                                else
                                {
                                    this.ParentNeighborhoodComboBox.DataSource = null;
                                    this.ParentNeighborhoodComboBox.Enabled = false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.GrandparentNeighborhoodTextBox.Text = string.Empty;
                    //// Added on 4th
                    this.GrandparentNeighborhoodTextBox.Enabled = false;
                    this.GrantParentNeighborhoodpictureBox.Enabled = false;
                    if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                    {
                        this.AppraisalGreenpictureBox.Enabled = false;
                        this.AppraisalRedRedpictureBox.Enabled = false;
                        this.AssessmentGreenpictureBox.Enabled = false;
                        this.AssessmentRedpictureBox.Enabled = false;
                        this.AdminGreenpictureBox.Enabled = false;
                        this.AdminRedpictureBox.Enabled = false;
                    }
                    else
                    {
                        this.AppraisalGreenpictureBox.Enabled = true;
                        this.AppraisalRedRedpictureBox.Enabled = true;
                        this.AssessmentGreenpictureBox.Enabled = true;
                        this.AssessmentRedpictureBox.Enabled = true;
                        this.AdminGreenpictureBox.Enabled = true;
                        this.AdminRedpictureBox.Enabled = true;
                    }
                }
            }
            else
            {
                this.GrandparentNeighborhoodTextBox.Text = string.Empty;
                this.ParentNeighborhoodComboBox.DataSource = null;
                this.ParentNeighborhoodComboBox.Enabled = false;
                this.ParentNeighborhoodpictureBox.Enabled = false;
                this.GrandparentNeighborhoodTextBox.Enabled = false;
            }
        }

        /// <summary>
        /// ParentNeighborhoodComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParentNeighborhoodComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.parentChangeflag = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.NeighborhoodCopyButton.Enabled = false;
                int rollyearText;
                int typeIndex;
                int parentIndex;
                int.TryParse(this.RollyearTextBox.Text.Trim(), out rollyearText);
                int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out typeIndex);
                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out parentIndex);
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(rollyearText, -1, parentIndex);
                if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                {
                    this.GrandparentNeighborhoodTextBox.Text = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn].ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Statusofcontrols
        /// </summary>
        /// <param name="typeIndex">typeIndex</param>
        private void Statusofcontrols(int typeIndex)
        {
            if (typeIndex == 1)
            {
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, -1);
                if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                {
                    this.previousNbhText = this.getNeighborhoodHeaderData.Neighborhood;
                    this.GrandparentNeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.GrandParentName;
                    this.previousGRPText = this.getNeighborhoodHeaderData.GrandParentName;
                    ///// this.previousGRPValue = this.getNeighborhoodHeaderData.GrandParent;
                    this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                    this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                    this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                    this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                    if (this.comboChange && this.parentChange || this.rollYearChange)
                    {
                        this.previouParentSelectedValue = this.getNeighborhoodHeaderData.PNBHDID;
                        this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.PNBHDID;
                    }
                    else
                    {
                        this.previouParentSelectedValue = this.getNeighborhoodHeaderData.NBHDID;
                        this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.NBHDID;
                    }
                }
                else
                {
                    this.ParentNeighborhoodComboBox.DataSource = null;
                    this.ParentNeighborhoodComboBox.Enabled = false;
                    this.ParentNeighborhoodpictureBox.Enabled = false;
                }

                this.NeighborhoodLabel.Visible = true;
                this.NeighborhoodTextBox.Visible = true;
                this.Parentneighborhoodlabel.Visible = true;
                this.ParentNeighborhoodComboBox.Visible = true;
                this.GrandparentNeighborhoodTextBox.Visible = true;
                this.ParentNeighborhoodComboBox.Enabled = true;
                this.GrandparentNeighborhoodTextBox.Enabled = true;
                this.GrandParentNeighborhoodlabel.Visible = true;
                this.ParentNeighborhoodpictureBox.Visible = true;
                this.GrantParentNeighborhoodpictureBox.Visible = true;
                this.GrantParentNeighborhoodpictureBox.Enabled = true;
                this.NeighborhoodLabel.Text = "Neighborhood:";
                this.Parentneighborhoodlabel.Text = "Parent Neighborhood:";
                this.GrandParentNeighborhoodlabel.Text = "Grand Parent Neighborhood:";
            }
            else if (typeIndex == 2)
            {
                this.ParentNeighborhoodComboBox.Enabled = true;
                this.ParentNeighborhoodComboBox.Visible = true;
                this.Parentneighborhoodlabel.Visible = true;
                this.GrandParentNeighborhoodlabel.Visible = false;
                this.ParentNeighborhoodpictureBox.Visible = true;
                this.GrandparentNeighborhoodTextBox.Visible = false;
                this.GrandparentNeighborhoodTextBox.Enabled = false;
                this.ParentNeighborhoodComboBox.Enabled = true;
                this.NeighborhoodLabel.Visible = true;
                this.NeighborhoodTextBox.Visible = true;
                this.NeighborhoodLabel.Text = "Parent Neighborhood:";
                this.Parentneighborhoodlabel.Text = "Grand Parent Neighborhood:";
                this.GrantParentNeighborhoodpictureBox.Visible = false;
                this.ParentNeighborhoodpictureBox.Enabled = true;
                int.TryParse(this.RollyearTextBox.Text.Trim(), out this.rollyear);
                if (this.rollyear == 0)
                {
                    this.ParentNeighborhoodComboBox.Enabled = false;
                }
                ////Modified on 25/07/07

                if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text) && !string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text) && this.saveFlag != 1)
                {
                    int tempPrarentSeletedValue;
                    int.TryParse(this.ParentNeighborhoodIDTextBox.Text.ToString().Trim(), out tempPrarentSeletedValue);
                    if (this.tempTypeSelectedValue >= this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()))
                    {
                        this.BindParentComboBox(typeIndex);
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(tempPrarentSeletedValue);
                        if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                        {
                            this.cancelChange = true;
                            this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                            this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.Neighborhood;
                            this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.GrandParent;
                        }
                    }
                    else
                    {
                        this.FillParentComboGrandToParent();
                    }
                }
                ////Modified on 25/07/07
                else
                {
                    if (this.typeComboSelected)
                    {
                        this.BindParentComboBox(typeIndex);
                        if (this.tempTypeSelectedValue > this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()))
                        {
                            this.FillParentComboGrandToParent();
                        }
                    }
                }

                this.parentChange = true;
            }
            else if (typeIndex == 3)
            {
                this.NeighborhoodLabel.Text = "Grand Parent Neighborhood:";
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.GrandParentName;
                }
                else
                {
                }

                if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.GrandparentNeighborhoodTextBox.Text.Trim()) && !this.typeComboSelected)
                    {
                        this.NeighborhoodTextBox.Text = this.GrandparentNeighborhoodTextBox.Text.ToString();
                    }
                }

                this.Parentneighborhoodlabel.Visible = false;
                this.ParentNeighborhoodComboBox.Visible = false;
                this.GrandParentNeighborhoodlabel.Visible = false;
                this.GrandparentNeighborhoodTextBox.Visible = false;
                this.ParentNeighborhoodpictureBox.Visible = false;
                this.ParentneighborhoodTextBox.Visible = false;
                this.GrantParentNeighborhoodpictureBox.Visible = false;
            }
        }

        /// <summary>
        /// BindParentComboBox
        /// </summary>
        /// <param name="typeIndex">typeIndex</param>
        private void BindParentComboBox(int typeIndex)
        {
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, typeIndex, -1);
            if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
            {
                ////this.previouParentText = this.ParentNeighborhoodComboBox.Text;
                this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out this.grandParentID);
                this.ParentNeighborhoodComboBox.SelectedIndex = 0;
            }
            else
            {
                this.ParentNeighborhoodComboBox.DataSource = null;
                this.ParentNeighborhoodComboBox.Enabled = false;
                this.ParentNeighborhoodpictureBox.Enabled = false;
            }
        }

        /// <summary>
        /// FillParentComboGrandToParent
        /// </summary>
        private void FillParentComboGrandToParent()
        {
            int tempPrarentSeletedValue;
            int.TryParse(this.ParentNeighborhoodIDTextBox.Text.ToString().Trim(), out tempPrarentSeletedValue);
            if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(tempPrarentSeletedValue);
                if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                {
                    this.cancelChange = true;
                    this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                    this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.Neighborhood;
                    this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()), this.getNeighborhoodHeaderData.GrandParent);
                    if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                    {
                        this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.DefaultView.Sort = "Neighborhood ASC";
                        this.ParentNeighborhoodComboBox.DisplayMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NeighborhoodColumn.ColumnName;
                        this.ParentNeighborhoodComboBox.ValueMember = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn.ColumnName;
                        this.ParentNeighborhoodComboBox.DataSource = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable;
                        this.ParentNeighborhoodComboBox.SelectedIndex = 0;
                    }
                    else
                    {
                        this.ParentNeighborhoodComboBox.DataSource = null;
                        this.ParentNeighborhoodComboBox.Enabled = false;
                        this.ParentNeighborhoodpictureBox.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// used to Assign Panles Combo Box Values
        /// </summary>
        private void FillParentPanelcomboBoxes()
        {
            if (this.TypeComboBox.SelectedIndex == 0 && string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View || this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.comboChange)
                    {
                        ////this.NeighborhoodTextBox.Text = string.Empty;
                    }
                    else
                    {
                        this.NeighborhoodTextBox.Text = this.previousNbhText;
                    }

                    this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                    this.NeighborhoodIDTextBox.Text = string.Empty;
                    this.NeighborhoodDescriptionTextBox.Text = string.Empty;
                    this.MarketReviewTextBox.Text = string.Empty;
                }

                this.GrandparentNeighborhoodTextBox.Visible = true;
                this.ParentNeighborhoodComboBox.SelectedValue = this.previouParentSelectedValue;
                if (this.previousGRPText != null)
                {
                    this.GrandparentNeighborhoodTextBox.Text = this.previousGRPText;
                }
                else
                {
                    this.GrandparentNeighborhoodTextBox.Text = string.Empty;
                }

                this.GrantParentNeighborhoodpictureBox.Enabled = false;
                if (this.rollyear == 0)
                {
                    this.GrandparentNeighborhoodTextBox.Text = string.Empty;
                }
            }
            else if (this.TypeComboBox.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
                {
                    this.GrandparentNeighborhoodTextBox.Visible = false;
                    if (this.rollyear == 0 || this.pageMode == TerraScanCommon.PageModeTypes.New)
                    {
                        this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                        this.NeighborhoodIDTextBox.Text = string.Empty;
                    }
                    //// Ramya
                    if (this.comboChange)
                    {
                        this.ParentNeighborhoodComboBox.SelectedValue = this.grandParentID;
                        if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                        {
                            this.NeighborhoodIDTextBox.Text = string.Empty;
                            this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                            this.EnableLock(false);
                        }
                    }
                    else
                    {
                        this.ParentNeighborhoodComboBox.SelectedValue = this.previouGPSelectedValue;
                    }
                }
                else
                {
                    if (this.tempTypeSelectedValue < this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()))
                    {
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(this.ConvertStringToInt(this.ParentNeighborhoodIDTextBox.Text.Trim()));
                        if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
                        {
                            this.cancelChange = true;
                            this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                            if (!this.typeComboSelected)
                            {
                                this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.Neighborhood;
                            }

                            this.ParentNeighborhoodComboBox.SelectedValue = this.getNeighborhoodHeaderData.GrandParent;
                        }
                    }
                }
            }
            ////Ramya
            else if (this.TypeComboBox.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
                {
                    int.TryParse(this.RollyearTextBox.Text.Trim(), out this.rollyear);
                    if (this.rollyear == 0 || this.pageMode != TerraScanCommon.PageModeTypes.New)
                    {
                        this.NeighborhoodIDTextBox.Text = string.Empty;
                        this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                    }
                    else if (!this.comboChange)
                    {
                        this.NeighborhoodTextBox.Text = this.previousGRPText;
                    }
                }
            }
        }

        /// <summary>
        /// TypeComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.typeComboSelected = true;
                int typeindex;
                int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out typeindex);
                this.SetEditRecord();
                this.comboChange = true;
                this.Statusofcontrols(typeindex);
                this.FillParentPanelcomboBoxes();
                if (typeindex == 1)
                {
                    this.rollYearChange = true;
                    int type;
                    int.TryParse(this.TypeComboBox.SelectedValue.ToString(), out type);
                    this.FillGrantParentNeighborhoodCombo(-1);
                    this.SetEditRecord();
                }

                this.typeComboSelected = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.EnableControls(true);
            this.ShowPanels(true);
            this.RollyearTextBox.Text = "";
            this.ParentNeighborhoodComboBox.Enabled = false;
            this.GrantParentNeighborhoodpictureBox.Visible = true;
            this.GrandparentNeighborhoodTextBox.Enabled = false;
            this.ParentNeighborhoodpictureBox.Visible = true;
            this.ParentNeighborhoodpictureBox.Enabled = false;
            this.GrantParentNeighborhoodpictureBox.Enabled = false;
            this.AppraisalGreenpictureBox.Enabled = false;
            this.AdminRedpictureBox.Enabled = false;
            this.AdminGreenpictureBox.Enabled = false;
            this.AppraisalRedRedpictureBox.Enabled = false;
            this.AssessmentGreenpictureBox.Enabled = false;
            this.AssessmentRedpictureBox.Enabled = false;
            this.FillTypeCombo();
            this.RollyearTextBox.Enabled = true;
            this.NeighborhoodTextBox.Enabled = true;
            this.MarketReviewTextBox.Enabled = true;
            this.NeighborhoodDescriptionTextBox.Enabled = true;
            this.ClearNeighborhoodHeaderDetails();
            this.NeighborhoodTextBox.Focus();
            this.pageMode = TerraScanCommon.PageModeTypes.New;
        }

        /// <summary>
        /// EnableControls
        /// </summary>
        /// <param name="show">show</param>
        private void EnableControls(bool show)
        {
            this.Parentneighborhoodlabel.Visible = true;
            this.GrandParentNeighborhoodlabel.Visible = true;
            this.AppraisalGreenpictureBox.Visible = show;
            this.AssessmentGreenpictureBox.Visible = show;
            this.AdminGreenpictureBox.Visible = show;
            this.TypeComboBox.Enabled = show;
            this.RollyearTextBox.Enabled = show;
            this.TypeComboBox.Enabled = show;
            this.AdminRedpictureBox.Visible = false;
            this.AppraisalRedRedpictureBox.Visible = false;
            this.AssessmentRedpictureBox.Visible = false;
            this.NeighborhoodTextBox.Visible = show;
            this.NeighborhoodLabel.Text = "Neighborhood:";
            this.ParentNeighborhoodComboBox.Visible = true;
            this.Parentneighborhoodlabel.Text = "Parent Neighborhood:";
            this.GrandparentNeighborhoodTextBox.Visible = show;
            this.GrandParentNeighborhoodlabel.Text = "Grand Parent Neighborhood:";
            this.Parentneighborhoodlabel.Enabled = show;
            this.GrandParentNeighborhoodlabel.Enabled = show;
            this.AppraisalGreenpictureBox.Enabled = show;
            this.AssessmentGreenpictureBox.Enabled = show;
            this.AdminGreenpictureBox.Enabled = show;
            this.TypeComboBox.Enabled = show;
            this.RollyearTextBox.Enabled = show;
            this.TypeComboBox.Enabled = show;
            this.AdminRedpictureBox.Enabled = false;
            this.AppraisalRedRedpictureBox.Enabled = false;
            this.AssessmentRedpictureBox.Enabled = false;
            this.NeighborhoodTextBox.Enabled = show;
            this.NeighborhoodCopyButton.Enabled = !show;
            this.NeighborhoodLabel.Text = "Neighborhood:";
            this.ParentNeighborhoodComboBox.Enabled = show;
            this.Parentneighborhoodlabel.Text = "Parent Neighborhood:";
            this.GrandparentNeighborhoodTextBox.Enabled = show;
            this.GrandParentNeighborhoodlabel.Text = "Grand Parent Neighborhood:";
        }

        /// <summary>
        /// ClearNeighborhoodHeaderDetails
        /// </summary>
        private void ClearNeighborhoodHeaderDetails()
        {
            this.NeighborhoodTextBox.Text = string.Empty;
            this.RollyearTextBox.Text = string.Empty;
            this.ParentNeighborhoodComboBox.SelectedValue = 0;
            this.GrandparentNeighborhoodTextBox.Text = string.Empty;
            this.NeighborhoodIDTextBox.Text = string.Empty;
            this.ParentNeighborhoodIDTextBox.Text = string.Empty;
            this.NeighborhoodDescriptionTextBox.Text = string.Empty;
            this.MarketReviewTextBox.Text = string.Empty;
            this.TypeComboBox.SelectedIndex = 0;
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
            ////khaja made changes to fix Bug#4029

            Control requiredControl;
            requiredControl = this.CheckRequiredFields();
            if (requiredControl != null)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredFields");
                requiredControl.Focus();
                this.parentGrandparentbuttonflag = 4;
                return sliceValidationFields;
            }

            int.TryParse(this.RollyearTextBox.Text, out this.rollyear);
            if (this.rollyear == 0)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("InvalidFieldYear");
                this.RollyearTextBox.Focus();
                return sliceValidationFields;
            }
            ////Added on 30th
            else if (!this.CheckDuplicateneighborhood())
            {
                DialogResult result = MessageBox.Show("This record cannot be saved because a Neighborhood of that Type already exists with the given Name and Roll Year.", ConfigurationWrapper.ApplicationName + " - Duplicate Neighborhood", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    this.NeighborhoodTextBox.Focus();
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
            }
            ////Added on 30th

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>the Reqried Fields String</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.TypeComboBox.Text.Trim()))
            {
                requiredControll = this.TypeComboBox;
            }
            else if (this.TypeComboBox.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim()))
                {
                    requiredControll = this.NeighborhoodTextBox;
                }
                else if (string.IsNullOrEmpty(this.ParentNeighborhoodComboBox.Text.Trim()))
                {
                    requiredControll = this.ParentNeighborhoodComboBox;
                }
                else if (string.IsNullOrEmpty(this.GrandparentNeighborhoodTextBox.Text.Trim()))
                {
                    requiredControll = this.GrandparentNeighborhoodTextBox;
                }
            }
            else if (this.TypeComboBox.SelectedIndex == 1)
            {
                if (string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim()))
                {
                    requiredControll = this.NeighborhoodTextBox;
                }
                else if (string.IsNullOrEmpty(this.ParentNeighborhoodComboBox.Text.Trim()))
                {
                    requiredControll = this.ParentNeighborhoodComboBox;
                }
                else if (this.ParentneighborhoodTextBox.Enabled == true)
                {
                    ////if (string.IsNullOrEmpty(this.ParentneighborhoodTextBox.Text))
                    ////{
                    ////    requiredControll = this.ParentneighborhoodTextBox;
                    ////}
                }
            }
            else if (this.TypeComboBox.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(this.NeighborhoodTextBox.Text.Trim()))
                {
                    requiredControll = this.NeighborhoodTextBox;
                }
            }

            return requiredControll;
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveNeighborhoodheaderRecord(false);
        }

        /// <summary>
        /// SaveNeighborhoodheaderRecord
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns>bool</returns>
        private bool SaveNeighborhoodheaderRecord(bool onclose)
        {
            this.saveFlag = 1;
            ////this.Cursor = Cursors.WaitCursor;
            int tempNBHID;
            F35100NeighborhoodHeaderData saveNeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            F35100NeighborhoodHeaderData.saveNeighborhoodHeaderDetailsRow dr = saveNeighborhoodHeaderData.saveNeighborhoodHeaderDetails.NewsaveNeighborhoodHeaderDetailsRow();
            int.TryParse(this.NeighborhoodIDTextBox.Text, out tempNBHID);
            if (tempNBHID > 0)
            {
                dr.NBHID = tempNBHID;
            }

            dr.RollYear = this.ConvertStringToInt(this.RollyearTextBox.Text);

            dr.PNBHID = this.ConvertStringToInt(this.ParentNeighborhoodIDTextBox.Text.Trim());
            dr.Type = this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString());
            if (this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()) == 1)
            {
                dr.Neighborhood = this.NeighborhoodTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text))
                {
                    dr.ParentNeighborhood = this.ConvertStringToInt(this.NeighborhoodIDTextBox.Text.ToString());
                    dr.GrantParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodComboBox.SelectedValue.ToString());
                }
                else
                {
                    if (tempNBHID > 0 && this.parentChangeflag)
                    {
                        dr.ParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodIDTextBox.Text.Trim());
                    }
                    else
                    {
                        dr.ParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodComboBox.SelectedValue.ToString());
                    }

                    dr.GrantParentNeighborhood = this.grantParent;
                }
            }
            else if (this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()) == 2)
            {
                dr.Neighborhood = this.NeighborhoodTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.ToString()))
                {
                    dr.ParentNeighborhood = this.ConvertStringToInt(this.NeighborhoodIDTextBox.Text.Trim());
                    dr.GrantParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodComboBox.SelectedValue.ToString());
                }
                else
                {
                    if (tempNBHID > 0 && this.parentChangeflag)
                    {
                        dr.ParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodIDTextBox.Text.Trim());
                    }
                    else
                    {
                        dr.ParentNeighborhood = this.ConvertStringToInt(this.ParentNeighborhoodComboBox.SelectedValue.ToString());
                    }

                    dr.GrantParentNeighborhood = 0;
                }
            }
            else if (this.ConvertStringToInt(this.TypeComboBox.SelectedValue.ToString()) == 3)
            {
                dr.Neighborhood = this.NeighborhoodTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
                {
                    dr.ParentNeighborhood = this.ConvertStringToInt(this.NeighborhoodIDTextBox.Text.Trim());
                    dr.GrantParentNeighborhood = this.ConvertStringToInt(this.NeighborhoodIDTextBox.Text.Trim());
                }
                else
                {
                    dr.ParentNeighborhood = 0;
                    dr.GrantParentNeighborhood = 0;
                }
            }

            dr.Description = this.NeighborhoodDescriptionTextBox.Text.Trim();
            dr.MarketReview = this.MarketReviewTextBox.Text.Trim();
            saveNeighborhoodHeaderData.saveNeighborhoodHeaderDetails.Rows.Add(dr);
            saveNeighborhoodHeaderData.saveNeighborhoodHeaderDetails.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(saveNeighborhoodHeaderData.saveNeighborhoodHeaderDetails.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                this.returnValue = this.form35100Controller.WorkItem.SaveNeighborhoodHeaderDetails(0, tempDataSet.GetXml(), TerraScan.Common.TerraScanCommon.UserId);
                if (this.returnValue == -1)
                {
                    switch (MessageBox.Show("The record cannot be saved because a Neighborhood already associated with the Roll Year ", ConfigurationWrapper.ApplicationName + " - Dupliacte Neighborhood", MessageBoxButtons.OK, MessageBoxIcon.Error))
                    {
                        case DialogResult.OK:
                            {
                                break;
                            }
                    }
                }
            }
            else
            {
                this.returnValue = this.form35100Controller.WorkItem.SaveNeighborhoodHeaderDetails(Convert.ToInt32(this.NeighborhoodIDTextBox.Text.Trim()), tempDataSet.GetXml(), TerraScan.Common.TerraScanCommon.UserId);
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = this.returnValue;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            if (onclose)
            {
                return true;
            }

            this.parentChangeflag = false;
            return true;
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private int ConvertStringToInt(string textboxValue)
        {
            int outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                int.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditRecord(object sender, EventArgs e)
        {
            if (!this.formLoad)
            {
                this.SetEditRecord();

            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode != TerraScanCommon.PageModeTypes.New)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit || !this.saveCancelflag)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    this.NeighborhoodCopyButton.Enabled = false;
                }
            }
        }

        #region Delete

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
            this.deleteFlag = true;
            ////this.Cursor = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                if (this.form35100Controller.WorkItem.DeleteNeighborhoodGroupHeader(Convert.ToInt32(this.NeighborhoodIDTextBox.Text.Trim()), TerraScan.Common.TerraScanCommon.UserId) == 0)
                {
                    SliceFormCloseAlert sliceFormCloseAlert;
                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                    sliceFormCloseAlert.FlagFormClose = false;
                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                }
                else
                {
                    MessageBox.Show(" This Neighborhood cannot be deleted. It has associated Parcels or it has child Neighborhoods.", "TerrasScan-Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    // Dont allow to remove keyid from QE Grid
                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
        }

        #endregion

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            this.formLoad = true;
            this.ClearNeighborhoodHeaderDetails();
            this.formLoad = false;
            this.NeighborhoodTextBox.Focus();
            if (this.cancelButtonFlag && !string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                Int32.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.keyId);
            }

            this.GetNeighborhoodHeaderDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.parentChangeflag = false;
            if (!this.cancelChange)
            {
                this.VisibleContolrs();
                this.ShowControls(false);
                this.ShowPanels(false);
                this.ClearControls();
            }

            this.cancelButtonFlag = false;
            this.NeighborhoodTextBox.Focus();
            this.NeighborhoodCopyButton.Enabled = true;

            //this.NeighborhoodCopyButton.Enabled = true;
        }
       
        /// <summary>
        /// ShowControls
        /// </summary>
        /// <param name="show">bool variable</param>
        private void ShowControls(bool show)
        {
            this.NeighborhoodTextBox.Enabled = show;
            this.RollyearTextBox.Enabled = show;
            this.TypeComboBox.Enabled = show;
            this.ParentNeighborhoodComboBox.Enabled = show;
            this.NeighborhoodDescriptionTextBox.Enabled = show;
            this.MarketReviewTextBox.Enabled = show;
            this.GrandparentNeighborhoodTextBox.Enabled = show;
            this.AppraisalGreenpictureBox.Enabled = show;
            this.AssessmentGreenpictureBox.Enabled = show;
            this.AdminGreenpictureBox.Enabled = show;
        }

        /// <summary>
        /// VisibleContolrs
        /// </summary>
        private void VisibleContolrs()
        {
            this.NeighborhoodLabel.Visible = true;
            this.NeighborhoodTextBox.Visible = true;
            this.Parentneighborhoodlabel.Visible = true;
            this.ParentNeighborhoodComboBox.Visible = true;
            this.GrandparentNeighborhoodTextBox.Visible = true;
            this.ParentNeighborhoodComboBox.Enabled = true;
            this.GrandparentNeighborhoodTextBox.Enabled = true;
            this.GrandparentNeighborhoodTextBox.Visible = false;
            this.GrandParentNeighborhoodlabel.Visible = true;
            this.ParentNeighborhoodpictureBox.Visible = true;
            this.GrantParentNeighborhoodpictureBox.Visible = true;
            this.NeighborhoodLabel.Text = "Neighborhood:";
            this.Parentneighborhoodlabel.Text = "Parent Neighborhood:";
            this.GrandParentNeighborhoodlabel.Text = "Grand Parent Neighborhood:";
        }

        /// <summary>
        /// ShowPanels
        /// </summary>
        /// <param name="show">bool Variable</param>
        private void ShowPanels(bool show)
        {
            this.NeighborhoodPanel.Enabled = show;
            this.TypePanel.Enabled = show;
            this.RollYearpanel.Enabled = show;
            this.ParentNeiborhoodpanel.Enabled = show;
            this.GrantParentNeighborhoodpanel.Enabled = show;
            this.Descriptionpanel.Enabled = show;
            this.Marketreviewpanel.Enabled = show;
            this.Lockspanel.Enabled = show;
        }

        /// <summary>
        /// ClearControls
        /// </summary>
        private void ClearControls()
        {
            this.NeighborhoodTextBox.Text = string.Empty;
            this.TypeComboBox.DataSource = null;
            this.RollyearTextBox.Text = string.Empty;
            this.ParentNeighborhoodComboBox.DataSource = null;
            this.GrandparentNeighborhoodTextBox.Text = string.Empty;
            this.GrandparentNeighborhoodTextBox.Text = string.Empty;
            this.NeighborhoodDescriptionTextBox.Text = string.Empty;
            this.MarketReviewTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Parent and Grand Button click
        /// </summary>
        /// <param name="nbhdID">nbhdID</param>
        private void ParentGrantButton(int nbhdID)
        {
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(nbhdID);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                this.NeighborhoodIDTextBox.Text = this.getNeighborhoodHeaderData.NBHDID.ToString();
                this.ParentNeighborhoodIDTextBox.Text = this.getNeighborhoodHeaderData.PNBHDID.ToString();
                this.NeighborhoodDescriptionTextBox.Text = this.getNeighborhoodHeaderData.Description;
                this.GrandparentNeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.GrandParentName;
                if (this.parentneighborFlag == 2)
                {
                    this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.GrandParentName;
                }
                else if (this.parentneighborFlag == 1)
                {
                    this.NeighborhoodTextBox.Text = this.getNeighborhoodHeaderData.Neighborhood;
                }

                if (this.getNeighborhoodHeaderData.RollYearDisabled == 1)
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = false;
                    this.TypeComboBox.Enabled = false;
                }
                else
                {
                    this.RollyearTextBox.Visible = true;
                    this.RollyearTextBox.Enabled = true;
                    this.TypeComboBox.Enabled = true;
                }

                if (this.getNeighborhoodHeaderData.MarketReview == "(Empty)")
                {
                    this.MarketReviewTextBox.Text = string.Empty;
                }
                else
                {
                    this.MarketReviewTextBox.Text = this.getNeighborhoodHeaderData.MarketReview;
                }

                if (this.getNeighborhoodHeaderData.LockAppraisalBy != 0)
                {
                    this.AppraisalRedRedpictureBox.Enabled = true;
                    this.AppraisalRedRedpictureBox.Visible = true;
                    this.AppraisalRedRedpictureBox.BringToFront();
                    this.AppraisalGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AppraisalGreenpictureBox.Enabled = true;
                    this.AppraisalGreenpictureBox.Visible = true;
                    this.AppraisalGreenpictureBox.BringToFront();
                    this.AppraisalRedRedpictureBox.Visible = false;
                }

                if (this.getNeighborhoodHeaderData.LockAssessmentBy != 0)
                {
                    this.AssessmentRedpictureBox.Enabled = true;
                    this.AssessmentRedpictureBox.Visible = true;
                    this.AssessmentRedpictureBox.BringToFront();
                    this.AssessmentGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AssessmentGreenpictureBox.Enabled = true;
                    this.AssessmentGreenpictureBox.Visible = true;
                    this.AssessmentGreenpictureBox.BringToFront();
                    this.AssessmentRedpictureBox.Visible = false;
                }

                if (this.getNeighborhoodHeaderData.LockAdminBy != 0)
                {
                    this.AdminRedpictureBox.Enabled = true;
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminRedpictureBox.BringToFront();
                    this.AdminGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AdminGreenpictureBox.Enabled = true;
                    this.AdminGreenpictureBox.Visible = true;
                    this.AdminGreenpictureBox.BringToFront();
                    this.AdminRedpictureBox.Visible = false;
                }
            }
        }

        /// <summary>
        /// ParentNeighborhoodpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ParentNeighborhoodpictureBox_Click(object sender, EventArgs e)
        {
          
            int tempType = 0;
            try
            {

                this.saveCancelflag = true;
                this.formLoad = true;
                this.cancelButtonFlag = true;
                if (this.Parentneighborhoodlabel.Text == "Parent Neighborhood:")
                {
                    this.parentneighborFlag = 1;
                    this.grandParentButtonFlag = true;
                   
                    if (this.ParentNeighborhoodComboBox.SelectedValue != null)
                    {
                        int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                    }

                    if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                    {
                        if (this.grandParentButtonFlag)
                        {
                            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                            if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()) && !this.parentChangeflag)
                            {
                                Int32.TryParse(this.ParentNeighborhoodIDTextBox.Text.Trim(), out tempType);
                                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                                {
                                    this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                                }
                            }
                        }

                        this.GetFormStatus(this, new DataEventArgs<string>(this.masterFormNo.ToString()));
                        if (this.parentGrandparentbuttonflag == 1)
                        {
                            if (!this.parentChangeflag)
                            {
                                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                            }

                            this.formLoad = true;
                            this.parentChangeflag = false;
                            this.SaveButton_Click();
                            SliceReloadActiveRecord currentKeyIdInfo;
                            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                            currentKeyIdInfo.SelectedKeyId = tempType;
                            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceReloadActiveRecord.SelectedKeyId = tempType;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.ParentButton();
                            this.parentGrandparentbuttonflag = 4;
                        }
                        else if (this.parentGrandparentbuttonflag == 2)
                        {
                            if (!this.parentChangeflag)
                            {
                                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                            }

                            this.formLoad = true;
                            SliceReloadActiveRecord currentKeyIdInfo;
                            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                            currentKeyIdInfo.SelectedKeyId = tempType;
                            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceReloadActiveRecord.SelectedKeyId = tempType;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.ParentButton();
                            this.parentGrandparentbuttonflag = 4;
                        }
                        else
                        {
                            this.parentGrandparentbuttonflag = 4;
                        }
                    }
                    else
                    {
                        #region TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
                        //SliceReloadActiveRecord currentKeyIdInfo;
                        //currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                        //currentKeyIdInfo.SelectedKeyId = tempType;
                        //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = tempType;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.D9030_F9033_OnChange_Neighborhood(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        #endregion

                        this.ParentButton();
                        this.parentGrandparentbuttonflag = 4;
                    }

                    this.parentneighborFlag = 0;
                    this.parentChangeflag = true;
                }
                else
                {
                    this.parentneighborFlag = 2;
                    this.formLoad = true;
                    this.saveCancelflag = true;
                    this.grandParentButtonFlag = true;
                    //int tempType;
                    int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                    if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                    {
                        if (this.grandParentButtonFlag)
                        {
                            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                            if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                            {
                                Int32.TryParse(this.ParentNeighborhoodIDTextBox.Text.Trim(), out tempType);
                                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                                {
                                    this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                                }
                            }
                        }

                        this.GetFormStatus(this, new DataEventArgs<string>(this.masterFormNo.ToString()));

                        if (this.parentGrandparentbuttonflag == 1)
                        {
                            if (!this.ParentNeighborhoodComboBox.Visible)
                            {
                                this.parentChangeflag = true;
                            }
                            else
                            {
                                this.parentChangeflag = false;
                            }

                            if (!this.parentChangeflag)
                            {
                                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                            }

                            this.SaveButton_Click();
                            SliceReloadActiveRecord currentKeyIdInfo;
                            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                            currentKeyIdInfo.SelectedKeyId = tempType;
                            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceReloadActiveRecord.SelectedKeyId = tempType;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.GrandParentButton();
                            this.parentGrandparentbuttonflag = 4;
                            this.parentChangeflag = true;
                        }
                        else if (this.parentGrandparentbuttonflag == 2)
                        {
                            if (!this.parentChangeflag)
                            {
                                int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
                            }

                            SliceReloadActiveRecord currentKeyIdInfo;
                            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                            currentKeyIdInfo.SelectedKeyId = tempType;
                            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceReloadActiveRecord.SelectedKeyId = tempType;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.GrandParentButton();
                            this.parentGrandparentbuttonflag = 4;
                        }
                        else
                        {
                            this.parentGrandparentbuttonflag = 4;
                        }
                    }
                    else
                    {
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                        if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                        {
                            Int32.TryParse(this.ParentNeighborhoodIDTextBox.Text.Trim(), out tempType);
                        }

                        #region TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
                        //SliceReloadActiveRecord currentKeyIdInfo;
                        //currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                        //currentKeyIdInfo.SelectedKeyId = tempType;
                        //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = tempType;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.D9030_F9033_OnChange_Neighborhood(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        #endregion

                        this.GrandParentButton();
                        this.parentGrandparentbuttonflag = 4;
                    }
                    
                    this.saveCancelflag = false;
                    this.grandParentButtonFlag = false;
                    this.formLoad = false;
                    this.parentneighborFlag = 0;
                }

                this.saveCancelflag = false;
                this.formLoad = false;
                this.NeighborhoodTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GrantParentNeighborhoodpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void GrantParentNeighborhoodpictureBox_Click(object sender, EventArgs e)
        {
            this.cancelButtonFlag = true;
            this.saveCancelflag = true;
            this.grandParentButtonFlag = true;
            this.formLoad = true;
            int tempType;
            int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
            if (this.pageMode != TerraScanCommon.PageModeTypes.View)
            {
                try
                {
                    if (this.grandParentButtonFlag)
                    {
                        this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                        if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                        {
                            Int32.TryParse(this.ParentNeighborhoodIDTextBox.Text.Trim(), out tempType);
                            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                            {
                                this.ParentNeighborhoodIDTextBox.Text = string.Empty;
                            }
                        }

                        if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                        {
                            tempType = Convert.ToInt32(this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }

                this.GetFormStatus(this, new DataEventArgs<string>(this.masterFormNo.ToString()));
                if (this.parentGrandparentbuttonflag == 1)
                {
                    try
                    {
                        this.parentChangeflag = true;
                        this.SaveButton_Click();
                        SliceReloadActiveRecord currentKeyIdInfo;
                        currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                        currentKeyIdInfo.SelectedKeyId = tempType;
                        this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = tempType;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.GrandParentButton();
                        this.parentGrandparentbuttonflag = 4;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    }
                }
                else if (this.parentGrandparentbuttonflag == 2)
                {
                    try
                    {
                        ////refresh the F35102 form 
                        SliceReloadActiveRecord currentKeyIdInfo;
                        currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                        currentKeyIdInfo.SelectedKeyId = tempType;
                        this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = tempType;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.GrandParentButton();
                        this.parentGrandparentbuttonflag = 4;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    }
                }
                else
                {
                    this.parentGrandparentbuttonflag = 4;
                }
            }
            else
            {
                try
                {
                    this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                    if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                    {
                        Int32.TryParse(this.ParentNeighborhoodIDTextBox.Text.Trim(), out tempType);
                    }

                    tempType = Convert.ToInt32(this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString());

                    #region TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
                    ////refresh the F35102 form 
                    //SliceReloadActiveRecord currentKeyIdInfo;
                    //currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                    //currentKeyIdInfo.SelectedKeyId = tempType;
                    //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                    //SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    //sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    //sliceReloadActiveRecord.SelectedKeyId = tempType;

                    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = tempType;
                    

                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.D9030_F9033_OnChange_Neighborhood(this, new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    #endregion 
                    this.GrandParentButton();
                    this.parentGrandparentbuttonflag = 4;
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
            this.saveCancelflag = false;
            this.grandParentButtonFlag = false;
            this.formLoad = false;
            this.NeighborhoodTextBox.Focus();

        }

        /// <summary>
        /// NeighborhoodPictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void NeighborhoodPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D35100.F35100"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// NeighborhoodPictureBox_MouseEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void NeighborhoodPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.NeighborhoodHeaderToolTip.SetToolTip(this.NeighborhoodPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AppraisalRedRedpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppraisalRedRedpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.Cursor = Cursors.WaitCursor;  
                this.saveCancelflag = true;
                this.lockChange = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.lockNbhdid);
                object[] optionalParameter = new object[] { this.lockNbhdid, "3501" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AppraisalLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// AssessmentRedpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssessmentRedpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveCancelflag = true;
                this.lockChange = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text, out this.lockNbhdid);
                this.keyId = this.lockNbhdid;
                object[] optionalParameter = new object[] { this.lockNbhdid, "3502" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AssessmentLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// AssessmentGreenpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AssessmentGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveCancelflag = true;
                this.lockChange = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.lockNbhdid);
                object[] optionalParameter = new object[] { this.lockNbhdid, "3502" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AssessmentLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// AdminRedpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminRedpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveCancelflag = true;
                this.lockChange = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.lockNbhdid);
                this.keyId = this.lockNbhdid;
                object[] optionalParameter = new object[] { this.lockNbhdid, "3503" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AdminLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// AdminGreenpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdminGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveCancelflag = true;
                this.lockChange = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.lockNbhdid);
                this.keyId = this.lockNbhdid;
                object[] optionalParameter = new object[] { this.lockNbhdid, "3503" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AdminLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// RollyearTextBox_Leave_2
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RollyearTextBox_Leave_2(object sender, EventArgs e)
        {
            try
            {
                this.rollYearChange = true;
                this.FillGrantParentNeighborhoodCombo(-1);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AppraisalGreenpictureBox_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AppraisalGreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.lockChange = true;
                this.saveCancelflag = true;
                Form subfundForm = new Form();
                int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out this.lockNbhdid);
                object[] optionalParameter = new object[] { this.lockNbhdid, "3501" };
                subfundForm = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3501, optionalParameter, this.form35100Controller.WorkItem);
                if (subfundForm != null)
                {
                    if (subfundForm.ShowDialog() == DialogResult.OK)
                    {
                        this.AppraisalLock(this.lockNbhdid);
                    }
                }

                this.saveCancelflag = false;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ParentButton
        /// </summary>
        private void ParentButton()
        {
            this.ParentneighborhoodTextBox.Visible = true;
            this.NeighborhoodTextBox.Text = this.ParentNeighborhoodComboBox.Text.Trim();
            this.ParentneighborhoodTextBox.Text = this.GrandparentNeighborhoodTextBox.Text.Trim();
            this.NeighborhoodLabel.Text = "Parent Neighborhood:";
            this.Parentneighborhoodlabel.Text = "Grand Parent Neighborhood:";
            this.GrandParentNeighborhoodlabel.Visible = false;
            this.GrandparentNeighborhoodTextBox.Visible = false;
            this.ParentNeighborhoodComboBox.Visible = false;
            this.GrantParentNeighborhoodpictureBox.Visible = false;
            if (!this.parentChangeflag)
            {
                this.ParentGrantButton(Convert.ToInt32(this.ParentNeighborhoodComboBox.SelectedValue.ToString().Trim()));
            }
            else
            {
                this.ParentGrantButton(this.parentNBHDID);
            }

            this.TypeComboBox.SelectedValue = 2;
        }

        /// <summary>
        /// GrandParentButton
        /// </summary>
        private void GrandParentButton()
        {
            this.NeighborhoodLabel.Text = "Grand Parent Neighborhood:";
            this.NeighborhoodTextBox.Text = this.GrandparentNeighborhoodTextBox.Text.Trim();
            this.ParentNeighborhoodComboBox.Visible = false;
            this.ParentneighborhoodTextBox.Visible = false;
            this.ParentNeighborhoodpictureBox.Visible = false;
            this.GrantParentNeighborhoodpictureBox.Visible = false;
            this.GrandParentNeighborhoodlabel.Visible = false;
            this.GrandparentNeighborhoodTextBox.Visible = false;
            this.GrantParentNeighborhoodpictureBox.Visible = false;
            this.Parentneighborhoodlabel.Visible = false;
            ////Added on 4th
            int tempType;
            int.TryParse(this.ParentNeighborhoodComboBox.SelectedValue.ToString(), out tempType);
            if (this.grandParentButtonFlag)
            {
                this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetParentNeighborhoodHeaderDetails(this.rollyear, -1, tempType);
                if (this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows.Count > 0)
                {
                    this.NeighborhoodIDTextBox.Text = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString();
                    this.ParentNeighborhoodIDTextBox.Text = this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.Rows[0][this.form35100NeighborhoodHeaderData.f35100pclstNeighborhoodDataTable.NBHDIDColumn].ToString();
                }

                if (!string.IsNullOrEmpty(this.ParentNeighborhoodIDTextBox.Text.Trim()))
                {
                    this.ParentGrantButton((Convert.ToInt32(this.ParentNeighborhoodIDTextBox.Text.Trim())));
                }
            }
            else
            {
                this.NeighborhoodIDTextBox.Text = tempType.ToString();
                this.ParentNeighborhoodIDTextBox.Text = tempType.ToString();
                this.ParentGrantButton(tempType);
            }

            this.TypeComboBox.SelectedValue = 3;
        }

        /// <summary>
        /// Checks the duplicate district.
        /// </summary>
        /// <returns>duplicate record status</returns>
        private bool CheckDuplicateneighborhood()
        {
            F35100NeighborhoodHeaderData duplicateNeighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            F35100NeighborhoodHeaderData.CheckDuplicateNeighborhoodTableRow dr = duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.NewCheckDuplicateNeighborhoodTableRow();
            int errorId = -1;
            int tempNBHDID = 0;
            int.TryParse(this.NeighborhoodIDTextBox.Text.Trim(), out tempNBHDID);
            if (tempNBHDID > 0)
            {
                dr.NBHDID = this.NeighborhoodIDTextBox.Text.Trim();
            }

            dr.Neighborhood = this.NeighborhoodTextBox.Text.Trim();
            dr.RollYear = this.RollyearTextBox.Text;
            dr.Type = this.TypeComboBox.SelectedValue.ToString();
            duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.Rows.Add(dr);
            duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.AcceptChanges();
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(duplicateNeighborhoodHeaderData.CheckDuplicateNeighborhoodTable.Copy());
            tempDataSet.Tables[0].TableName = "Table";
            if (string.IsNullOrEmpty(this.NeighborhoodIDTextBox.Text.Trim()))
            {
                errorId = this.form35100Controller.WorkItem.DuplicateNeighborhoodHeaderCheck(0, tempDataSet.GetXml());
            }
            else
            {
                errorId = this.form35100Controller.WorkItem.DuplicateNeighborhoodHeaderCheck(tempNBHDID, tempDataSet.GetXml());
            }

            if (errorId != -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// AppraisalLock
        /// </summary>
        /// <param name="nbhdID"> NeighborhoodID</param>
        private void AppraisalLock(int nbhdID)
        {
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(nbhdID);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                if (this.getNeighborhoodHeaderData.LockAppraisalBy != 0)
                {
                    this.AppraisalRedRedpictureBox.Enabled = true;
                    this.AppraisalRedRedpictureBox.Visible = true;
                    this.AppraisalRedRedpictureBox.BringToFront();
                    this.AppraisalGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AppraisalGreenpictureBox.Enabled = true;
                    this.AppraisalGreenpictureBox.Visible = true;
                    this.AppraisalGreenpictureBox.BringToFront();
                    this.AppraisalRedRedpictureBox.Visible = false;
                }
            }
        }

        /// <summary>
        /// AssessmentLock
        /// </summary>
        /// <param name="nbhdID">nbhdID</param>
        private void AssessmentLock(int nbhdID)
        {
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(nbhdID);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                ////this.cancelChange = true;
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                if (this.getNeighborhoodHeaderData.LockAssessmentBy != 0)
                {
                    this.AssessmentRedpictureBox.Enabled = true;
                    this.AssessmentRedpictureBox.Visible = true;
                    this.AssessmentRedpictureBox.BringToFront();
                    this.AssessmentGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AssessmentGreenpictureBox.Enabled = true;
                    this.AssessmentGreenpictureBox.Visible = true;
                    this.AssessmentGreenpictureBox.BringToFront();
                    this.AssessmentRedpictureBox.Visible = false;
                }
            }
        }

        /// <summary>
        /// AdminLock
        /// </summary>
        /// <param name="nbhdID">nbhdID</param>
        private void AdminLock(int nbhdID)
        {
            this.form35100NeighborhoodHeaderData = this.form35100Controller.WorkItem.GetNeighborhoodHeaderDetails(nbhdID);
            if (this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows.Count > 0)
            {
                this.getNeighborhoodHeaderData = (F35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTableRow)this.form35100NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTable.Rows[0];
                if (this.getNeighborhoodHeaderData.LockAdminBy != 0)
                {
                    this.AdminRedpictureBox.Enabled = true;
                    this.AdminRedpictureBox.Visible = true;
                    this.AdminRedpictureBox.BringToFront();
                    this.AdminGreenpictureBox.Visible = false;
                }
                else
                {
                    this.AdminGreenpictureBox.Enabled = true;
                    this.AdminGreenpictureBox.Visible = true;
                    this.AdminGreenpictureBox.BringToFront();
                    this.AdminRedpictureBox.Visible = false;
                }
            }
        }

        /// <summary>
        /// ControlLock
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void ControlLock(bool lockKey)
        {
            this.NeighborhoodTextBox.LockKeyPress = lockKey;
            this.RollyearTextBox.LockKeyPress = lockKey;
            this.NeighborhoodDescriptionTextBox.LockKeyPress = lockKey;
            this.MarketReviewTextBox.LockKeyPress = lockKey;
        }

        /// <summary>
        /// EnableLock
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void EnableLock(bool lockKey)
        {
            this.AppraisalGreenpictureBox.Enabled = lockKey;
            this.AppraisalRedRedpictureBox.Enabled = lockKey;
            this.AssessmentGreenpictureBox.Enabled = lockKey;
            this.AssessmentRedpictureBox.Enabled = lockKey;
            this.AdminGreenpictureBox.Enabled = lockKey;
            this.AdminRedpictureBox.Enabled = lockKey;
        }

        /// <summary>
        /// DisableControls
        /// </summary>
        /// <param name="lockKey">lockKey</param>
        private void DisableControls(bool lockKey)
        {
            this.ParentNeighborhoodComboBox.Enabled = lockKey;
            this.TypeComboBox.Enabled = lockKey;
            this.ParentNeighborhoodpictureBox.Enabled = lockKey;
            this.GrantParentNeighborhoodpictureBox.Enabled = lockKey;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">
        /// true - to Set the control as not editable
        /// false - to Set the control as editable
        /// </param>
        private void PermissionControlLock(bool controlLook)
        {
            this.ControlLock(controlLook);
            this.EnableLock(!controlLook);
            this.DisableControls(!controlLook);
            this.ParentNeighborhoodpictureBox.Enabled = !controlLook;
            this.GrantParentNeighborhoodpictureBox.Enabled = !controlLook;
        }

        private void NeighborhoodCopyButton_Click(object sender, EventArgs e)
        {
            SupportFormData.GetFormDetailsDataTable getForm3511DetailsData = new SupportFormData.GetFormDetailsDataTable();
            bool form3511OpenPermission = false;
            getForm3511DetailsData = form35100Controller.WorkItem.GetFormDetails(Convert.ToInt32(3511), TerraScanCommon.UserId);
            if (getForm3511DetailsData.Rows.Count > 0)
            {
                form3511OpenPermission = Convert.ToBoolean(getForm3511DetailsData.Rows[0][getForm3511DetailsData.IsPermissionOpenColumn.ColumnName].ToString());
            }

            Form CopyNeighbourhood = new Form();
            object[] optionalParameter = new object[] { this.NeighborhoodIDTextBox.Text, this.rollyear, this.TypeComboBox.SelectedValue };
            CopyNeighbourhood = this.form35100Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(3511, optionalParameter, this.F35100Control.WorkItem);
            if (CopyNeighbourhood != null && form3511OpenPermission == true)
            {
                if (CopyNeighbourhood.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string strNBHID = TerraScanCommon.GetValue(CopyNeighbourhood, "CommandResult");
                        this.formLoad = true;
                        this.ClearNeighborhoodHeaderDetails();
                        this.formLoad = false;
                        this.keyId = Convert.ToInt32(strNBHID);

                        SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        // this.SetActiveKeyId(this, new DataEventArgs<int[]>(this.keyId));
                        this.SetActiveRecord(this, new DataEventArgs<int>(this.keyId));

                        ////this.SetActiveKeyId(this, new DataEventArgs<int[]>(this.keyId));  
                        this.CopyMethod();
                        //this.SetRecordCount(this, new DataEventArgs<int>(this.keyId));  
                        //FormInfo formInfo;
                        //formInfo = TerraScanCommon.GetFormInfo(1011);                        
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
                else
                {
                    if (this.NeighborhoodTextBox.Enabled && this.NeighborhoodTextBox.Visible)
                    {
                        this.NeighborhoodTextBox.Focus();
                    }
                }
                //// Gets The TemplateID From Modal Dialog ,By Passing Form name and Property
                //this.MortgageImportTemplateId = Convert.ToInt32(TerraScanCommon.GetValue(CopyNeighbourhood, "MortgageImportTemplateId"));

                //if (this.MortgageImportTemplateId > 0)
                //{
                //    this.ChangeMortgageImportStatus();
                //    this.FillTemplateDetails(this.MortgageImportTemplateId);
                //}
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("FormOpenPermission") + "Copy Neighbourhood Form", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
