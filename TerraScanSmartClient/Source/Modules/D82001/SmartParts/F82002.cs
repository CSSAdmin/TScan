//--------------------------------------------------------------------------------------------
// <copyright file="F82002.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82002 FS Contractor management
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07/12/2007      SriParameswari.A        Created
//***********************************************************************************************/

namespace D82001
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
    /// class file F82002
    /// </summary>
    public partial class F82002 : BaseSmartPart
    {
        #region variable

        /// <summary>
        /// setSlicecount
        /// </summary>
        private int setSlicecount;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool newSave;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool newCancel;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool shiftTab;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool cancelNew;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool cancelSave;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool cancelsearch;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool clearcheck;

        /// <summary>
        /// searchInfo
        /// </summary>
        private bool searchInfo;

        /// <summary>
        /// delInfo
        /// </summary>
        private bool delInfo;

        /// <summary>
        /// dateVal
        /// </summary>
        private bool dateVal;

        /// <summary>
        /// isshift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// saveConfirm
        /// </summary>
        private bool saveConfirm;

        /// <summary>
        /// setCancelFlag
        /// </summary>
        private bool setCancelFlag;

        /// <summary>
        /// Variable for ContractorId
        /// </summary>
        private int uniqueContractorId;

        /// <summary>
        /// Variable for keyDown
        /// </summary>
        private bool keyDown;

        /// <summary>
        /// deleteFlag
        /// </summary>
        private bool deleteFlag;

        /// <summary>
        /// deleteFlag
        /// </summary>
        private bool newSaveFlag;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// Used to store the searchNameTxt
        /// </summary>
        private string searchNameTxt = string.Empty;

        /// <summary>
        /// Used to store the searchNameTxt
        /// </summary>
        private string searchCityTxt = string.Empty;

        /// <summary>
        /// Used to store the searchNameTxt
        /// </summary>
        private string searchLicenseTxt = string.Empty;

        /// <summary>
        /// Used to store the searchNameTxt
        /// </summary>
        private string searchBondTxt = string.Empty;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int previousIndex;

        /// <summary>
        ///  Local variable for keyId
        /// </summary>
        private int keyId;

        /// <summary>
        ///  Local variable for clearKeyID
        /// </summary>
        private int clearKeyID;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store the statementId(keyid)
        /// </summary>
        private int contractorId;

        /// <summary>
        /// contractorupdateDetails
        /// </summary>
        private int contractorupdateDetails;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// form82002Controler
        /// </summary>
        private F82002Controler form82002Controler;

        /// <summary>
        /// contractorManagementData
        /// </summary>
        private F82002ContractorManagementData contractorManagementData = new F82002ContractorManagementData();

        /// <summary>
        /// contractorManagementDatas
        /// </summary>
        private F82002ContractorManagementData contractorManagementDatas = new F82002ContractorManagementData();

        /// <summary>
        /// listContractorManagementDataTable
        /// </summary>
        private F82002ContractorManagementData.ListContractorManagementDataTable listContractorManagementDataTable = new F82002ContractorManagementData.ListContractorManagementDataTable();

        /// <summary>
        /// keypress flag
        /// </summary>
        private bool keypress = false;

        /// <summary>
        /// upArrowContrctorId
        /// </summary>
        private int upArrowContrctorId = 0;

        private int upArrowIndex = -1;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F82002"/> class.
        /// </summary>
        public F82002()
        {
            InitializeComponent();
        }

        /// <summary>
        /// F82002
        /// </summary>
        /// <param name="masterform">masterform</param>
        /// <param name="formNo">formNo</param>
        /// <param name="keyID">keyID</param>
        /// <param name="red">red</param>
        /// <param name="green">green</param>
        /// <param name="blue">blue</param>
        /// <param name="tabText">tabText</param>
        /// <param name="permissionEdit">permissionEdit</param>
        public F82002(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.contractorId = keyID;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ContractormanagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractormanagementPictureBox.Height, this.ContractormanagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FormMasterSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FormMasterSave;

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_RevertDeleteAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8040 control.
        /// </summary>
        /// <value>The F8040 control.</value>
        [CreateNew]
        public F82002Controler Form82002Controler
        {
            get { return this.form82002Controler as F82002Controler; }
            set { this.form82002Controler = value; }
        }
        #endregion

        #region Event Subscription

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
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.PermissionFiled.newPermission)
                {
                    this.EnableDetails();
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.deleteFlag = false;
                    this.newSaveFlag = true;
                    this.ClearDetails();
                    this.LockControls(false);
                    this.ControlStatus(false);
                    this.contractorupdateDetails = 0;
                    this.ContractorNameTextBox.Focus();
                    this.newCancel = true;
                }
                else
                {
                    this.LockControls(true);
                    this.ControlStatus(true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                this.searchInfo = false;
                //// keypress = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.contractorId = this.keyId;
                if (!this.cancelsearch)
                {

                    ////this.contractorId = this.clearKeyID;
                    //// this.contractorId = this.upArrowContrctorId; 
                    this.LoadFieldDeatails();

                }
                //// else if((this.newCancel == true) && (this.cancelSave == false))
                else if (this.newCancel == true)
                {
                    this.ClearDetails();
                    //if (this.upArrowContrctorId != 0)
                    //{
                    //    this.contractorId = this.upArrowContrctorId;
                    //}
                    //else
                    //{
                    //    this.contractorId = this.keyId; 
                    //}
                    this.LoadFieldDeatails();
                    this.SearchDeatails();
                    this.newCancel = false;
                }
                else if (this.cancelNew && this.cancelSave == false && this.newSave == false)
                {
                    this.ClearDetails();
                    this.cancelNew = false;
                }
                else if ((!this.deleteFlag) && (!this.newSaveFlag))
                {
                    this.LoadFieldDeatails();
                    this.SearchDeatails();
                }
                else if (this.newSaveFlag)
                {
                    this.LoadFieldDeatails();
                    this.SearchDeatails();
                }
                else if (this.deleteFlag)
                {
                    ////  addIds = this.f82002Controler.WorkItem.F82002_InsertBuildingPermitDetails(this.uniqueContractorId, Utility.GetXmlString(contractorManagementData.ListContractorManagement.Copy()), TerraScan.Common.TerraScanCommon.UserId);
                    try
                    {
                        this.LoadFieldDeatails();
                        this.SearchDeatails();
                        this.clearcheck = true;
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                    }
                }

                this.ContractorManagementsortMode();
                if (this.listContractorManagementDataTable.Rows.Count > 0)
                {
                    TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, this.upArrowIndex);
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
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                this.saveConfirm = false;
                this.InsertOrUpdateContractDetails();
                this.ContractorManagementsortMode();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.newSaveFlag = false;
            }
        }

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

                    if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 0)
                    ////if (this.setSlicecount > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.EnableDetails();
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                        this.DisableDetails();
                        this.cancelNew = true;
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
                    this.FlagSliceForm = true;
                    //// this.CustomizeConstractorManagementGridView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.contractorId = eventArgs.Data.SelectedKeyId;
                    ////this.PageLoadDetails();
                    this.searchInfo = false;
                    this.LoadFieldDeatails();
                    if (this.cancelsearch)
                    {
                        this.SearchDeatails();
                    }

                    this.ContractorManagementsortMode();
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
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                this.searchInfo = false;
                this.DeleteButton_Click();

                if (this.delInfo == true)
                {
                    this.LoadUpArrowFieldDeatails();
                    this.delInfo = false;
                    this.SearchDeatails();
                }
                else
                {
                    this.ClearDetails();
                    this.LoadFieldDeatails();
                    this.SearchDeatails();
                }
            }
        }

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

        #endregion

        #region Protected methods

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
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFormMasterSave(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FormMasterSave != null)
            {
                this.D9030_F95005_FormMasterSave(this, eventArgs);
            }
        }

        #endregion

        #region Methods

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
            if (string.IsNullOrEmpty(ContractorNameTextBox.Text.Trim()))
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                this.ContractorNameTextBox.Focus();
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 3)
            {
                if (recordCount > 15)
                {
                    recordCount = 15;
                }

                int increment = ((recordCount - 3) * 22);
                this.ContractorManagementGridView.Height = 88 + increment;
                this.ConstractorGridPanel.Height = this.ContractorManagementGridView.Height;
                //// this.AllOwnersDetailsGridVerticalScroll.Height = 88 + increment +23-4;   
                this.AllOwnersDetailsGridVerticalScroll.Height = this.ContractorManagementGridView.Height - 22;
                this.ContractorManagementGridView.NumRowsVisible = recordCount;
                ////int heightPanel = (ConstractorNamePanel.Height) + (AddressPanel.Height) + (CityLicenseNumberPanel.Height) + (panel8.Height) + (ContractorPanel.Height) + (ConstractorGridPanel.Height);
                this.ContractormanagementPictureBox.Height = 294 + increment + 23;
                ////this.ContractormanagementPictureBox.Height = heightPanel;
                ////this.ContractormanagementPictureBox.Height = 293 + increment;
                ////this.Height = this.ContractorManagementGridView.Height + heightPanel;
                this.ContractormanagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractormanagementPictureBox.Height, this.ContractormanagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.Height = ConstractorNamePanel.Height + AddressPanel.Height + CityLicenseNumberPanel.Height + panel8.Height + ContractorPanel.Height + ConstractorGridPanel.Height + this.TitlePanel.Height;
            }
            else
            {
                this.ContractorManagementGridView.Height = 88;
                this.ConstractorGridPanel.Height = this.ContractorManagementGridView.Height;
                this.AllOwnersDetailsGridVerticalScroll.Height = this.ContractorManagementGridView.Height - 21;
                ////int heightPanel = (ConstractorNamePanel.Height) + (AddressPanel.Height) + (CityLicenseNumberPanel.Height) + (panel8.Height) + (ContractorPanel.Height) + (ConstractorGridPanel.Height);
                this.ContractormanagementPictureBox.Height = 294 + 23;
                this.ContractorManagementGridView.NumRowsVisible = 3;
                this.Height = 294 + 23;
                this.ContractormanagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractormanagementPictureBox.Height, this.ContractormanagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }

            this.TitlePanel.Top = this.ConstractorGridPanel.Bottom - 1;
        }

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
            if ((this.uniqueContractorId == this.contractorId) || (this.uniqueContractorId == 0) || (this.uniqueContractorId == this.previousIndex))
            {
                int allOwnerGirdRowIndexValues;
                allOwnerGirdRowIndexValues = ContractorManagementGridView.CurrentRow.Index;
                TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, allOwnerGirdRowIndexValues);
                this.delInfo = false;
                //// if((ContractorNameTextBox.Enabled)||(!string.IsNullOrEmpty(ContractorNameTextBox.Text)))
                if (!string.IsNullOrEmpty(ContractorNameTextBox.Text))
                {
                    MessageBox.Show("Record Cannot be deleted", "TerraScan-Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Dont allow to remove keyid from QE Grid
                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
            else
            {
                if (this.uniqueContractorId > 0)
                {
                    this.delInfo = false;
                    this.form82002Controler.WorkItem.F82002_DeleteContractorManagement(this.uniqueContractorId, TerraScan.Common.TerraScanCommon.UserId);
                    this.ClearDetails();
                    this.deleteFlag = false;
                    this.newSaveFlag = true;
                    this.SearchDeatails();
                }
            }
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.ContractorManagementNotsortMode();
            }
        }

        /// <summary>
        /// Customizes the constractor management grid view.
        /// </summary>
        private void CustomizeConstractorManagementGridView()
        {
            this.ContractorManagementGridView.AutoGenerateColumns = false;

            F82002ContractorManagementData.ListContractorManagementDataTable contractorManagementTable = this.contractorManagementData.ListContractorManagement;
            //// F27080ExemptionDefinitionData.GridLoadExemptionTypeTableDataTable exemptionGridTable = this.exemptionData.GridLoadExemptionTypeTable;
            //// this.ExemptionCode.DataPropertyName = exemptionGridTable.ExemptionCodeColumn.ColumnName;

            // Coding added for the issue 1026 for sorting focus
            this.ContractorManagementGridView.PrimaryKeyColumnName = contractorManagementTable.ContractorIDColumn.ColumnName;
            // Ends here for 1026
            this.ContractorIDs.DataPropertyName = contractorManagementTable.ContractorIDColumn.ColumnName;
            this.ContractorName.DataPropertyName = contractorManagementTable.ContractorNameColumn.ColumnName;
            this.ContractorPhone.DataPropertyName = contractorManagementTable.ContractorPhoneColumn.ColumnName;
            this.ContractorAddress.DataPropertyName = contractorManagementTable.ContractorAddressColumn.ColumnName;
            this.ContractorCity.DataPropertyName = contractorManagementTable.ContractorCityColumn.ColumnName;
            this.ContractorState.DataPropertyName = contractorManagementTable.ContractorStateColumn.ColumnName;
            this.ContractorZip.DataPropertyName = contractorManagementTable.ContractorZipColumn.ColumnName;
            this.CityLicenseNumber.DataPropertyName = contractorManagementTable.CityLicenseNumberColumn.ColumnName;
            this.LicenseExprDate.DataPropertyName = contractorManagementTable.LicenseExprDateColumn.ColumnName;
            this.BondExprDate.DataPropertyName = contractorManagementTable.BondExprDateColumn.ColumnName;
            this.SetSmartPartHeight(this.contractorManagementDatas.ListContractorManagement.Rows.Count);
            if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
            {
                panel3.Visible = false;
                this.AllOwnersDetailsGridVerticalScroll.Visible = false;
            }
            else
            {
                panel3.Visible = true;
                this.AllOwnersDetailsGridVerticalScroll.Visible = true;
            }
        }

        /// <summary>
        /// Shows the license expiration date.
        /// </summary>
        private void ShowLicenseExpirationDate()
        {
            TimeMonthCalender.Enabled = true;
            this.TimeMonthCalender.Visible = true;
            this.isshift = false;
            this.shiftTab = false;
            this.TimeMonthCalender.ScrollChange = 1;
            //// Set the calendar location.
            this.TimeMonthCalender.Left = this.LicenseExpirationDatepanel.Left + this.LicenseExpirationDateButton.Left + this.LicenseExpirationDateButton.Width;
            this.TimeMonthCalender.Top = this.LicenseExpirationDatepanel.Top + this.LicenseExpirationDateButton.Top;
            this.TimeMonthCalender.Tag = this.LicenseExpirationDateButton.Tag;
            this.TimeMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.LicenseExpirationDateTextBox.Text))
            {
                this.TimeMonthCalender.SetDate(Convert.ToDateTime(this.LicenseExpirationDateTextBox.Text));
            }
            else
            {
                this.TimeMonthCalender.SetDate(DateTime.Today);
            }
        }

        /// <summary>
        /// Shows the bond expiration date.
        /// </summary>
        private void ShowBondExpirationDate()
        {
            TimeMonthCalender.Enabled = true;
            this.TimeMonthCalender.Visible = true;
            this.TimeMonthCalender.ScrollChange = 1;
            this.isshift = false;
            //// Set the calendar location.
            this.TimeMonthCalender.Left = this.LicenseExpirationDatepanel.Left + this.LicenseExpirationDateButton.Left + this.LicenseExpirationDateButton.Width;
            this.TimeMonthCalender.Top = this.LicenseExpirationDatepanel.Top + this.LicenseExpirationDateButton.Top;
            this.TimeMonthCalender.Tag = this.BondExpirationDatebutton.Tag;
            this.TimeMonthCalender.Focus();
            if (!string.IsNullOrEmpty(this.BondExpirationDateTextBox.Text))
            {
                this.TimeMonthCalender.SetDate(Convert.ToDateTime(this.BondExpirationDateTextBox.Text));
                ////this.LicenseExpirationDateTextBox.Focus();
            }
            else
            {
                this.TimeMonthCalender.SetDate(DateTime.Today);
                //// this.LicenseExpirationDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Shows the search license expiration date.
        /// </summary>
        private void ShowSearchLicenseExpirationDate()
        {
            TimeMonthCalender.Enabled = true;
            this.TimeMonthCalender.Visible = true;
            this.isshift = false;

            // Set the calendar to move one month at a time when navigating using the arrows.

            this.TimeMonthCalender.ScrollChange = 1;

            // Set the calendar location.

            this.TimeMonthCalender.Left = this.LicenseExpirationDatepanel.Left + this.LicenseExpirationDateButton.Left + this.LicenseExpirationDateButton.Width;
            this.TimeMonthCalender.Top = this.LicenseExpirationDatepanel.Top + this.LicenseExpirationDateButton.Top;
            this.TimeMonthCalender.Tag = this.LicenseExpirationDatesbutton.Tag;
            this.TimeMonthCalender.Focus();

            if (!string.IsNullOrEmpty(this.LicenseExpirationDatesTextBox.Text))
            {
                this.TimeMonthCalender.SetDate(Convert.ToDateTime(this.LicenseExpirationDatesTextBox.Text));
                ////this.LicenseExpirationDateTextBox.Focus();
            }
            else
            {
                this.TimeMonthCalender.SetDate(DateTime.Today);
                //// this.LicenseExpirationDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Shows the bond expiration dates text box.
        /// </summary>
        private void ShowBondExpirationDatesTextBox()
        {
            TimeMonthCalender.Enabled = true;
            this.TimeMonthCalender.Visible = true;
            //// Set the calendar to move one month at a time when navigating using the arrows.
            this.TimeMonthCalender.ScrollChange = 1;
            this.isshift = false;
            this.TimeMonthCalender.Left = this.LicenseExpirationDatepanel.Left + this.LicenseExpirationDateButton.Left + this.LicenseExpirationDateButton.Width;
            this.TimeMonthCalender.Top = this.LicenseExpirationDatepanel.Top + this.LicenseExpirationDateButton.Top;
            this.TimeMonthCalender.Tag = this.BondExpirationDatesbutton.Tag;
            this.TimeMonthCalender.Focus();

            if (!string.IsNullOrEmpty(this.BondExpirationDatesTextBox.Text))
            {
                this.TimeMonthCalender.SetDate(Convert.ToDateTime(this.BondExpirationDatesTextBox.Text));
            }
            else
            {
                this.TimeMonthCalender.SetDate(DateTime.Today);
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void SetSeletedDate(string selectedDate)
        {
            if (this.TimeMonthCalender.Tag.ToString() == "LicenseExpirationDateTextBox")
            {
                this.LicenseExpirationDateTextBox.Text = selectedDate;
                this.shiftTab = true;
                LicenseExpirationDateTextBox.Focus();
                this.dateVal = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.ContractorManagementNotsortMode();
                this.TimeMonthCalender.Visible = false;
            }

            if (this.TimeMonthCalender.Tag.ToString() == "BondExpirationDateTextBox")
            {
                this.BondExpirationDateTextBox.Text = selectedDate;
                this.shiftTab = true;
                BondExpirationDateTextBox.Focus();
                this.dateVal = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.ContractorManagementNotsortMode();
                this.TimeMonthCalender.Visible = false;
            }

            if (this.TimeMonthCalender.Tag.ToString() == "LicenseExpirationDatesTextBox")
            {
                this.shiftTab = true;
                this.LicenseExpirationDatesTextBox.Text = selectedDate;
                LicenseExpirationDatesTextBox.Focus();
                this.TimeMonthCalender.Visible = false;
            }

            if (this.TimeMonthCalender.Tag.ToString() == "BondExpirationDatesTextBox")
            {
                this.shiftTab = true;
                this.BondExpirationDatesTextBox.Text = selectedDate;
                BondExpirationDatesTextBox.Focus();
                this.TimeMonthCalender.Visible = false;
            }
        }

        /// <summary>
        /// EnableDetails()
        /// </summary>
        private void EnableDetails()
        {
            ConstractorNamePanel.Enabled = true;
            ContractorNameTextBox.Enabled = true;
            DistrictPanel.Enabled = true;
            PhoneNumberTextBox.Enabled = true;
            AddressPanel.Enabled = true;
            AddressTextBox.Enabled = true;
            CityPanel.Enabled = true;
            CityTextBox.Enabled = true;
            StatePanel.Enabled = true;
            StateTextBox.Enabled = true;
            ZipPanel.Enabled = true;
            ZipTextBox.Enabled = true;
            BondExpirationDatePanel.Enabled = true;
            BondExpirationDatebutton.Enabled = true;
            BondExpirationDateTextBox.Enabled = true;
            LicenseExpirationDateTextBox.Enabled = true;
            LicenseExpirationDateButton.Enabled = true;
            LicenseExpirationDatepanel.Enabled = true;
            CityLicenseNumberPanel.Enabled = true;
            CityLicenseNumberTextBox.Enabled = true;
        }

        /// <summary>
        /// DisableDetails()
        /// </summary>
        private void DisableDetails()
        {
            ConstractorNamePanel.Enabled = false;
            ContractorNameTextBox.Enabled = false;
            DistrictPanel.Enabled = false;
            PhoneNumberTextBox.Enabled = false;
            AddressPanel.Enabled = false;
            AddressTextBox.Enabled = false;
            CityPanel.Enabled = false;
            CityTextBox.Enabled = false;
            StatePanel.Enabled = false;
            StateTextBox.Enabled = false;
            ZipPanel.Enabled = false;
            ZipTextBox.Enabled = false;
            BondExpirationDatePanel.Enabled = false;
            BondExpirationDatebutton.Enabled = false;
            BondExpirationDateTextBox.Enabled = false;
            LicenseExpirationDateTextBox.Enabled = false;
            LicenseExpirationDateButton.Enabled = false;
            LicenseExpirationDatepanel.Enabled = false;
            CityLicenseNumberPanel.Enabled = false;
            CityLicenseNumberTextBox.Enabled = false;
        }

        /// <summary>
        /// Clears the details.
        /// </summary>
        private void ClearDetails()
        {
            this.ContractorNameTextBox.Text = string.Empty;
            this.PhoneNumberTextBox.Text = string.Empty;
            this.AddressTextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.CityLicenseNumberTextBox.Text = string.Empty;
            this.LicenseExpirationDateTextBox.Text = string.Empty;
            this.BondExpirationDateTextBox.Text = string.Empty;
            this.uniqueContractorId = 0;
            ////keypress = false;
        }

        /// <summary>
        /// Contractors the managementsort mode.
        /// </summary>
        private void ContractorManagementsortMode()
        {
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.ContractorNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.ContractorCityColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.LicenseExprDateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.BondExprDateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Contractors the management notsort mode.
        /// </summary>
        private void ContractorManagementNotsortMode()
        {
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.ContractorNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.ContractorCityColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.LicenseExprDateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.ContractorManagementGridView.Columns[this.listContractorManagementDataTable.BondExprDateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Loads the up arrow field deatails.
        /// </summary>
        private void LoadUpArrowFieldDeatails()
        {
            ////int allOwnerGirdRowIndexValue;
            if (this.clearcheck != false)
            {
                this.newSave = true;
                if (!string.IsNullOrEmpty(this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorIDs"].Value.ToString()))
                {
                    ////allOwnerGirdRowIndexValue = ContractorManagementGridView.CurrentRow.Index;
                    this.uniqueContractorId = Convert.ToInt32(this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorIDs"].Value.ToString());
                    this.contractorupdateDetails = Convert.ToInt32(this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorIDs"].Value.ToString());
                    ContractorNameTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorName"].Value.ToString();
                    PhoneNumberTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorPhone"].Value.ToString();
                    AddressTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorAddress"].Value.ToString();
                    CityTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorCity"].Value.ToString();
                    StateTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorState"].Value.ToString();
                    ZipTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["ContractorZip"].Value.ToString();
                    CityLicenseNumberTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["CityLicenseNumber"].Value.ToString();
                    LicenseExpirationDateTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["LicenseExprDate"].Value.ToString();
                    BondExpirationDateTextBox.Text = this.ContractorManagementGridView.Rows[this.previousIndex].Cells["BondExprDate"].Value.ToString();
                    this.deleteFlag = true;
                    this.clearcheck = true;

                    ////made changes to fix Bug#5995
                    ////this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //// this.EditEnabled();
                }
            }
        }

        /// <summary>
        /// Loads the field deatails.
        /// </summary>
        private void LoadFieldDeatails()
        {
            this.contractorManagementDatas = this.form82002Controler.WorkItem.F82002_ListContractorManagementData(this.contractorId, null);
            if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 0)
            {
                this.setSlicecount = this.contractorManagementDatas.ListContractorManagement.Rows.Count;
                this.contractorupdateDetails = Convert.ToInt32(this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorIDColumn.ColumnName].ToString());
                ////int contractorId = Convert.ToInt32(this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorIDColumn.ColumnName].ToString());
                this.ContractorNameTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorNameColumn.ColumnName].ToString();
                this.PhoneNumberTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorPhoneColumn.ColumnName].ToString();
                this.AddressTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorAddressColumn.ColumnName].ToString();
                this.CityTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorCityColumn.ColumnName].ToString();
                this.StateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorStateColumn.ColumnName].ToString();
                this.ZipTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorZipColumn.ColumnName].ToString();
                this.CityLicenseNumberTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.CityLicenseNumberColumn.ColumnName].ToString();
                this.LicenseExpirationDateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.LicenseExprDateColumn.ColumnName].ToString();
                this.BondExpirationDateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.BondExprDateColumn.ColumnName].ToString();
            }
            else
            {
                ////made changes to lock controls in new mode.
                this.deleteFlag = false;
                this.ClearDetails();
                this.LockControls(true);
                this.ControlStatus(true);
            }

            this.contractorManagementDatas.Clear();
            this.ContractorManagementGridView.DataSource = this.contractorManagementDatas;
            this.ContractorManagementGridView.Enabled = false;
            this.MoveUpButton.Enabled = false;

            ////this.SetSmartPartHeight(this.contractorManagementDatas.ListContractorManagement.Rows.Count);

            if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
            {
                panel3.Visible = false;
                this.AllOwnersDetailsGridVerticalScroll.Visible = false;
            }
            else
            {
                panel3.Visible = true;
                this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
            }
        }

        /// <summary>
        /// Inserts the or update contract details.
        /// </summary>
        private void InsertOrUpdateContractDetails()
        {
            this.searchInfo = false;
            this.contractorManagementData.ListContractorManagement.Clear();
            F82002ContractorManagementData.ListContractorManagementRow contractDetailsRow = this.contractorManagementData.ListContractorManagement.NewListContractorManagementRow();

            if (!string.IsNullOrEmpty(this.ContractorNameTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorName = ContractorNameTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorName = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.PhoneNumberTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorPhone = this.PhoneNumberTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorPhone = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.AddressTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorAddress = this.AddressTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorAddress = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.CityTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorCity = this.CityTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorCity = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.StateTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorState = this.StateTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorState = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.ZipTextBox.Text.Trim()))
            {
                contractDetailsRow.ContractorZip = this.ZipTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.ContractorZip = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.CityLicenseNumberTextBox.Text.Trim()))
            {
                contractDetailsRow.CityLicenseNumber = this.CityLicenseNumberTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.CityLicenseNumber = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.LicenseExpirationDateTextBox.Text.Trim()))
            {
                contractDetailsRow.LicenseExprDate = this.LicenseExpirationDateTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.LicenseExprDate = string.Empty;
            }

            if (!string.IsNullOrEmpty(this.BondExpirationDateTextBox.Text.Trim()))
            {
                contractDetailsRow.BondExprDate = this.BondExpirationDateTextBox.Text.Trim();
            }
            else
            {
                contractDetailsRow.BondExprDate = string.Empty;
            }

            this.contractorManagementData.ListContractorManagement.Rows.Add(contractDetailsRow);
            int addIds;
            if (this.contractorupdateDetails == 0)
            {
                addIds = this.form82002Controler.WorkItem.F82002_InsertBuildingPermitDetails(null, TerraScanCommon.GetXmlString(this.contractorManagementData.ListContractorManagement.Copy()), TerraScan.Common.TerraScanCommon.UserId);
            }
            else
            {
                addIds = this.form82002Controler.WorkItem.F82002_InsertBuildingPermitDetails(this.contractorupdateDetails, TerraScanCommon.GetXmlString(this.contractorManagementData.ListContractorManagement.Copy()), TerraScan.Common.TerraScanCommon.UserId);
            }

            this.cancelSave = true;

            if (addIds > 0)
            {
                if (string.IsNullOrEmpty(ContractorTextBox.Text) && string.IsNullOrEmpty(CitysTextBox.Text) && string.IsNullOrEmpty(LicenseExpirationDatesTextBox.Text) && string.IsNullOrEmpty(BondExpirationDatesTextBox.Text) && this.cancelsearch)
                {
                    this.SearchDeatails();
                }
                else if (this.cancelsearch)
                {
                    this.SearchDeatails();
                }
                else
                {
                }

                ////this.SetSmartPartHeight(this.contractorManagementDatas.ListContractorManagement.Rows.Count);

                if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
                {
                    panel3.Visible = false;
                    this.AllOwnersDetailsGridVerticalScroll.Visible = false;
                }
                else
                {
                    panel3.Visible = true;
                    this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                    this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
                }

                ////Added By Ramya

                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = addIds;
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                ////to refresh the master form with the return keyid
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
        }

        /// <summary>
        /// Pages the load details.
        /// </summary>
        private void PageLoadDetails()
        {
            this.contractorManagementDatas = this.form82002Controler.WorkItem.F82002_ListContractorManagementData(54, null);
            ////int contractorId = Convert.ToInt32(this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorIDColumn.ColumnName].ToString());
            ContractorNameTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorNameColumn.ColumnName].ToString();
            PhoneNumberTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorPhoneColumn.ColumnName].ToString();
            AddressTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorAddressColumn.ColumnName].ToString();
            CityTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorCityColumn.ColumnName].ToString();
            StateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorStateColumn.ColumnName].ToString();
            ZipTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.ContractorZipColumn.ColumnName].ToString();
            CityLicenseNumberTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.CityLicenseNumberColumn.ColumnName].ToString();
            LicenseExpirationDateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.LicenseExprDateColumn.ColumnName].ToString();
            BondExpirationDateTextBox.Text = this.contractorManagementDatas.ListContractorManagement.Rows[0][this.contractorManagementDatas.ListContractorManagement.BondExprDateColumn.ColumnName].ToString();
            //// ContractorManagementGridView.DataSource = this.contractorManagementDatas.Clear();
            this.contractorManagementDatas.Clear();
            ContractorManagementGridView.DataSource = this.contractorManagementDatas;
            MoveUpButton.Enabled = false;

            this.SetSmartPartHeight(this.contractorManagementDatas.ListContractorManagement.Rows.Count);

            if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
            {
                panel3.Visible = false;
                this.AllOwnersDetailsGridVerticalScroll.Visible = false;
            }
            else
            {
                panel3.Visible = true;
                this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
            }
            ////Permission
            this.LockControls(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.ControlStatus(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
        }

        /// <summary>
        /// Searches the deatails.
        /// </summary>
        private void SearchDeatails()
        {
            try
            {
                this.setCancelFlag = true;
                ////if ((string.IsNullOrEmpty(this.ContractorTextBox.Text.Trim().Replace("'", ""))) && (string.IsNullOrEmpty(this.CitysTextBox.Text.Trim().Replace("'", ""))) && (string.IsNullOrEmpty(this.LicenseExpirationDatesTextBox.Text.Trim().Replace("'", ""))) && (string.IsNullOrEmpty(this.BondExpirationDatesTextBox.Text.Trim().Replace("'", ""))))

                if ((string.IsNullOrEmpty(this.searchNameTxt.Replace("'", ""))) && (string.IsNullOrEmpty(this.searchCityTxt.Replace("'", ""))) && (string.IsNullOrEmpty(this.searchLicenseTxt.Replace("'", ""))) && (string.IsNullOrEmpty(this.searchBondTxt.Replace("'", ""))))
                {
                    this.contractorManagementDatas.ListContractorManagement.Rows.Clear();
                    this.ContractorManagementGridView.DataSource = this.contractorManagementDatas;
                    this.ContractorManagementGridView.NumRowsVisible = 3;
                    MoveUpButton.Enabled = false;
                    this.ContractorManagementGridView.Enabled = false;
                    this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                    this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
                    this.SetSmartPartHeight(3);
                }
                else
                {
                    if ((!string.IsNullOrEmpty(this.searchNameTxt)) || (!string.IsNullOrEmpty(this.searchCityTxt)) || (!string.IsNullOrEmpty(this.searchLicenseTxt)) || (!string.IsNullOrEmpty(this.searchBondTxt)))
                    {
                        this.contractorManagementData.ListContractorManagement.Rows.Clear();
                        F82002ContractorManagementData.ListContractorManagementRow dr = this.contractorManagementData.ListContractorManagement.NewListContractorManagementRow();

                        if (this.searchInfo == false)
                        {
                            if (!string.IsNullOrEmpty(this.searchNameTxt))
                            {
                                dr.ContractorName = this.searchNameTxt.Trim().Replace("'", "");
                            }

                            ////if (!string.IsNullOrEmpty(this.CitysTextBox.Text.Trim()))
                            if (!string.IsNullOrEmpty(this.searchCityTxt))
                            {
                                dr.ContractorCity = this.searchCityTxt.Trim().Replace("'", "");
                            }

                            if (!string.IsNullOrEmpty(this.searchLicenseTxt))
                            {
                                dr.LicenseExprDate = this.searchLicenseTxt.Trim().Replace("'", "");
                            }

                            if (!string.IsNullOrEmpty(this.searchBondTxt))
                            {
                                dr.BondExprDate = this.searchBondTxt.Trim().Replace("'", "");
                            }
                            ////  dr.ContractorID = 2;
                            this.contractorManagementData.ListContractorManagement.Rows.Add(dr);
                            this.searchInfo = true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.ContractorTextBox.Text))
                            {
                                dr.ContractorName = this.ContractorTextBox.Text.Trim().Replace("'", "");
                            }

                            ////if (!string.IsNullOrEmpty(this.CitysTextBox.Text.Trim()))
                            if (!string.IsNullOrEmpty(this.CitysTextBox.Text))
                            {
                                dr.ContractorCity = this.CitysTextBox.Text.Trim().Replace("'", "");
                            }

                            if (!string.IsNullOrEmpty(this.LicenseExpirationDatesTextBox.Text))
                            {
                                dr.LicenseExprDate = this.LicenseExpirationDatesTextBox.Text.Trim().Replace("'", "");
                            }

                            if (!string.IsNullOrEmpty(this.BondExpirationDatesTextBox.Text))
                            {
                                dr.BondExprDate = this.BondExpirationDatesTextBox.Text.Trim().Replace("'", "");
                            }

                            this.contractorManagementData.ListContractorManagement.Rows.Add(dr);
                        }

                        int a = this.contractorManagementData.ListContractorManagement.Rows.Count;
                        try
                        {
                            this.ContractorManagementGridView.Height = 88;
                            this.contractorManagementDatas.Clear();
                            this.ContractorManagementGridView.NumRowsVisible = 3;
                        }
                        catch (Exception ex)
                        {
                            ////
                        }

                        this.contractorManagementDatas = this.form82002Controler.WorkItem.F82002_ListContractorManagementData(null, Utility.GetXmlString(this.contractorManagementData.ListContractorManagement.Copy()));
                        this.ContractorManagementGridView.DataSource = this.contractorManagementDatas;

                        if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 0)
                        {
                            MoveUpButton.Enabled = true;
                            this.ContractorManagementGridView.Enabled = true;
                        }
                        else
                        {
                            MoveUpButton.Enabled = false;
                            this.ContractorManagementGridView.Enabled = false;
                        }

                        if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
                        {
                            panel3.Visible = false;
                            this.AllOwnersDetailsGridVerticalScroll.Visible = false;
                        }
                        else
                        {
                            panel3.Visible = true;
                            this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                            this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
                        }

                        this.SetSmartPartHeight(this.contractorManagementDatas.ListContractorManagement.Rows.Count);
                    }

                    #region Modified MS

                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    //// this.Height = this.ContractormanagementPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                    sliceResize.SliceFormHeight = this.Height;
                    //// this.ContractormanagementPictureBox.Height = this.Height;
                    this.ContractormanagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractormanagementPictureBox.Height, this.ContractormanagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    #endregion Modified MS
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Permission

        /// <summary>
        /// LockControls
        /// </summary>
        /// <param name="keyPress">keyPress</param>
        private void LockControls(bool keyPress)
        {
            ContractorNameTextBox.LockKeyPress = keyPress;
            PhoneNumberTextBox.LockKeyPress = keyPress;
            AddressTextBox.LockKeyPress = keyPress;
            CityTextBox.LockKeyPress = keyPress;
            StateTextBox.LockKeyPress = keyPress;
            ZipTextBox.LockKeyPress = keyPress;
            CityLicenseNumberTextBox.LockKeyPress = keyPress;
            LicenseExpirationDateTextBox.LockKeyPress = keyPress;
            BondExpirationDateTextBox.LockKeyPress = keyPress;
        }

        /// <summary>
        /// ControlStatus
        /// </summary>
        /// <param name="show">show</param>
        private void ControlStatus(bool show)
        {
            this.ContractorNameTextBox.Enabled = !show;
            this.PhoneNumberTextBox.Enabled = !show;
            this.AddressTextBox.Enabled = !show;
            this.CityTextBox.Enabled = !show;
            this.StateTextBox.Enabled = !show;
            this.ZipTextBox.Enabled = !show;
            this.CityLicenseNumberTextBox.Enabled = !show;
            this.LicenseExpirationDateTextBox.Enabled = !show;
            this.BondExpirationDateTextBox.Enabled = !show;
        }
        #endregion Permission

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the F82002 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F82002_Load(object sender, EventArgs e)
        {
            try
            {
                this.newSave = false;
                this.newCancel = false;
                this.cancelSave = false;
                this.shiftTab = false;
                this.saveConfirm = true;
                this.FlagSliceForm = true;
                this.CustomizeConstractorManagementGridView();
                this.ContractorManagementGridView.DataSource = this.contractorManagementData.ListContractorManagement;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////this.PageLoadDetails();
                this.LoadFieldDeatails();
                ////this.cancelOnProcess = true;
                this.ContractorManagementsortMode();
                this.deleteFlag = false;
                this.newSaveFlag = false;
                this.MoveUpButton.Enabled = false;
                this.setCancelFlag = false;
                this.dateVal = true;
                this.ContractorTextBox.Focus();
                this.delInfo = false;
                this.searchInfo = true;
                this.clearcheck = true;
                this.cancelsearch = true;
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
        /// Handles the Click event of the LicenseExpirationDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LicenseExpirationDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowLicenseExpirationDate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_DateSelected_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_DateSelected_1(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToShortDateString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// BondExpirationDatebutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BondExpirationDatebutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowBondExpirationDate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// LicenseExpirationDatesbutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LicenseExpirationDatesbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowSearchLicenseExpirationDate();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// BondExpirationDatesbutton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BondExpirationDatesbutton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowBondExpirationDatesTextBox();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// SearchButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SearchDeatails();
                this.ContractorTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ClearButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////   this.clearcheck = false;
                this.cancelsearch = false;
                ////this.deleteFlag = false;
                ContractorTextBox.Text = string.Empty;
                ////Clears the global variables to fix Bug#5994
                this.searchNameTxt = string.Empty;
                this.searchCityTxt = string.Empty;
                this.searchLicenseTxt = string.Empty;
                this.searchBondTxt = string.Empty;

                CitysTextBox.Text = string.Empty;
                LicenseExpirationDatesTextBox.Text = string.Empty;
                BondExpirationDatesTextBox.Text = string.Empty;

                int.TryParse(this.ContractorManagementGridView.Rows[this.ContractorManagementGridView.CurrentRow.Index].Cells["ContractorIDs"].Value.ToString(), out this.clearKeyID);

                this.contractorManagementDatas = this.form82002Controler.WorkItem.F82002_ListContractorManagementData(2, Utility.GetXmlString(this.contractorManagementData.ListContractorManagement.Copy()));
                this.contractorManagementDatas.Clear();
                this.ContractorManagementGridView.NumRowsVisible = 3;
                ContractorManagementGridView.DataSource = this.contractorManagementDatas;
                ////this.SetSmartPartHeight(this.contractorManagementData.ListContractorManagement.Rows.Count);
                this.SetSmartPartHeight(3);

                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                //// this.Height = this.ContractormanagementPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                sliceResize.SliceFormHeight = this.Height;
                //// this.ContractormanagementPictureBox.Height = this.Height;
                this.ContractormanagementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ContractormanagementPictureBox.Height, this.ContractormanagementPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));


                MoveUpButton.Enabled = false;
                this.ContractorTextBox.Focus();
                ContractorManagementGridView.ReadOnly = true;
                ////this.ContractormanagementPictureBox.Height = this.Height;
                if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 15)
                {
                    panel3.Visible = false;
                    this.AllOwnersDetailsGridVerticalScroll.Visible = false;
                    this.AllOwnersDetailsGridVerticalScroll.Enabled = true;
                }
                else
                {
                    panel3.Visible = true;
                    this.AllOwnersDetailsGridVerticalScroll.Visible = true;
                    this.AllOwnersDetailsGridVerticalScroll.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// MoveUpButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            this.EnableDetails();
            this.LockControls(false);
            this.deleteFlag = true;
            this.newSaveFlag = false;
            int allOwnerGirdRowIndexValues;
            if (ContractorManagementGridView.CurrentRow != null)
            {
                allOwnerGirdRowIndexValues = ContractorManagementGridView.CurrentRow.Index;
                this.previousIndex = allOwnerGirdRowIndexValues;

                ////allOwnerGirdRowIndexValue = ContractorManagementGridView.CurrentRow.Index;
                ////his.uniqueContractorId = Convert.ToInt32(this.ContractorManagementGridView.Rows[allOwnerGirdRowIndexValues].Cells["ContractorIDs"].Value.ToString());

                try
                {
                    if (allOwnerGirdRowIndexValues >= 0)
                    {
                        if ((!string.IsNullOrEmpty(ContractorManagementGridView.Rows[allOwnerGirdRowIndexValues].Cells["ContractorName"].Value.ToString()) && this.uniqueContractorId.ToString() != ContractorManagementGridView.Rows[allOwnerGirdRowIndexValues].Cells["ContractorIDs"].Value.ToString()) || (!string.IsNullOrEmpty(ContractorNameTextBox.Text)))
                        {
                            if (this.pageMode == TerraScanCommon.PageModeTypes.View || (string.IsNullOrEmpty(ContractorNameTextBox.Text)))
                            {
                                this.LoadUpArrowFieldDeatails();
                                this.ContractorManagementsortMode();
                            }
                            else
                            {
                                this.ContractorManagementNotsortMode();
                                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                if (dialogResult == DialogResult.Yes)
                                {
                                    this.OnD9030_F95005_AlertFormMasterSave(new DataEventArgs<int>(this.masterFormNo));

                                    TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, allOwnerGirdRowIndexValues);
                                    this.LoadUpArrowFieldDeatails();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));

                                    TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, allOwnerGirdRowIndexValues);
                                    this.LoadUpArrowFieldDeatails();
                                }
                                else if (dialogResult == DialogResult.Cancel)
                                {
                                    this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                                    TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, allOwnerGirdRowIndexValues);
                                    this.LoadUpArrowFieldDeatails();
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
        }

        /// <summary>
        /// ContractorManagementGridView_CellDoubleClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ContractorManagementGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.EnableDetails();
                this.LockControls(false);
                if (e.RowIndex >= 0)
                //// && !string.IsNullOrEmpty(ContractorManagementGridView.Rows[e.RowIndex].Cells["ContractorName"].Value.ToString()))
                {
                    ////allOwnerGirdRowIndexValue = ContractorManagementGridView.CurrentRow.Index;
                    ////this.uniqueContractorId = Convert.ToInt32(this.ContractorManagementGridView.Rows[allOwnerGirdRowIndexValue].Cells["ContractorIDs"].Value.ToString());
                    ////this.uniqueContractorId = Convert.ToInt32(this.ContractorManagementGridView.Rows[e.RowIndex].Cells["ContractorIDs"].Value.ToString());
                    this.upArrowContrctorId = Convert.ToInt32(this.ContractorManagementGridView.Rows[e.RowIndex].Cells["ContractorIDs"].Value.ToString());
                    this.upArrowIndex = e.RowIndex;

                    if (!string.IsNullOrEmpty(ContractorManagementGridView.Rows[e.RowIndex].Cells["ContractorName"].Value.ToString()) && this.uniqueContractorId.ToString() != ContractorManagementGridView.Rows[e.RowIndex].Cells["ContractorIDs"].Value.ToString())
                    {
                        this.deleteFlag = true;
                        this.newSaveFlag = false;
                        this.previousIndex = e.RowIndex;
                        if ((this.pageMode == TerraScanCommon.PageModeTypes.View) || (string.IsNullOrEmpty(ContractorNameTextBox.Text)))
                        {
                            this.LoadUpArrowFieldDeatails();
                            this.ContractorManagementsortMode();
                        }
                        else
                        {
                            this.ContractorManagementNotsortMode();
                            DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                            if (dialogResult == DialogResult.Yes)
                            {
                                this.OnD9030_F95005_AlertFormMasterSave(new DataEventArgs<int>(this.masterFormNo));

                                TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, e.RowIndex);
                                this.LoadUpArrowFieldDeatails();
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));

                                TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, e.RowIndex);
                                this.LoadUpArrowFieldDeatails();
                            }
                            else if (dialogResult == DialogResult.Cancel)
                            {
                                this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                                TerraScanCommon.SetDataGridViewPosition(this.ContractorManagementGridView, e.RowIndex);
                                this.LoadUpArrowFieldDeatails();
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
        /// ContractormanagementPictureBox_Click
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">e</param>
        private void ContractormanagementPictureBox_Click(object sender, EventArgs e)
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
        /// ContractormanagementPictureBox_MouseHover
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ContractormanagementPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ContractorManagementToolTip.SetToolTip(this.ContractormanagementPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the AllTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AllTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ////keypress = true;

                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    int a = e.KeyChar;
                    if (this.keyDown == true)
                    {
                        if ((e.KeyChar == 'v') || (e.KeyChar == 24))
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                            this.ContractorManagementNotsortMode();
                        }
                        else
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                    else
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                        this.ContractorManagementNotsortMode();
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
        /// Handles the KeyDown event of the AllTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void AllTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int a = e.KeyValue;
                if (e.KeyValue == 46)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    this.ContractorManagementNotsortMode();
                }
                else if (e.KeyValue == 86)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    this.ContractorManagementNotsortMode();
                }

                this.keyDown = e.Control;
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
        /// TimeMonthCalender_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.TimeMonthCalender.SelectionStart.ToString(dateFormat));
                }

                this.isshift = e.Shift;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_Leave_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_Leave_1(object sender, EventArgs e)
        {
            TimeMonthCalender.Visible = false;
            if (this.TimeMonthCalender.Tag.ToString() == "LicenseExpirationDateTextBox")
            {
                if (this.isshift || this.shiftTab)
                {
                    this.LicenseExpirationDateTextBox.Focus();
                }
                else
                {
                    this.BondExpirationDateTextBox.Focus();
                }
            }

            if (this.TimeMonthCalender.Tag.ToString() == "BondExpirationDateTextBox")
            {
                if (this.isshift || this.shiftTab)
                {
                    this.BondExpirationDateTextBox.Focus();
                }
                else
                {
                    this.ContractorTextBox.Focus();
                }
            }

            if (this.TimeMonthCalender.Tag.ToString() == "LicenseExpirationDatesTextBox")
            {
                if (this.isshift || this.shiftTab)
                {
                    this.LicenseExpirationDatesTextBox.Focus();
                }
                else
                {
                    this.BondExpirationDatesTextBox.Focus();
                }
            }

            if (this.TimeMonthCalender.Tag.ToString() == "BondExpirationDatesTextBox")
            {
                if (this.isshift || this.shiftTab)
                {
                    this.BondExpirationDatesTextBox.Focus();
                }
                else
                {
                    this.ContractorManagementGridView.Focus();
                }
            }
        }

        /// <summary>
        /// LicenseExpirationDateTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LicenseExpirationDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                string a = LicenseExpirationDateTextBox.Text;
                LicenseExpirationDatepanel.BackColor = Color.White;
                LicenseExpirationDateTextBox.BackColor = Color.White;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// BondExpirationDateTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BondExpirationDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                BondExpirationDatePanel.BackColor = Color.White;
                BondExpirationDateTextBox.BackColor = Color.White;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LicenseExpirationDatesTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LicenseExpirationDatesTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                LicenseExpirationDatesPanel.BackColor = Color.White;
                LicenseExpirationDatesTextBox.BackColor = Color.White;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// BondExpirationDatesTextBox
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BondExpirationDatesTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                BondExpirationDatesPanel.BackColor = Color.White;
                BondExpirationDatesTextBox.BackColor = Color.White;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ContractorTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ContractorTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ContractorTextBox.BackColor = Color.White;
                ContractorPanel.BackColor = Color.White;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ContractorTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ContractorTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.ContractorTextBox.Text.Trim()))
                {
                    this.searchNameTxt = this.ContractorTextBox.Text.Trim();
                    this.cancelsearch = true;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CitysTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CitysTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.CitysTextBox.Text.Trim()))
                {
                    this.searchCityTxt = this.CitysTextBox.Text.Trim();
                    this.cancelsearch = true;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LicenseExpirationDatesTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LicenseExpirationDatesTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.LicenseExpirationDateTextBox.Text.Trim()))
            {
                this.searchLicenseTxt = this.LicenseExpirationDateTextBox.Text.Trim();
                this.cancelsearch = true;
            }
        }

        /// <summary>
        /// BondExpirationDatesTextBox_TextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BondExpirationDatesTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.BondExpirationDatesTextBox.Text.Trim()))
                {
                    this.searchBondTxt = this.BondExpirationDatesTextBox.Text.Trim();
                    this.cancelsearch = true;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// TimeMonthCalender_KeyPress
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void TimeMonthCalender_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Alltexts the box text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AlltextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                //this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        /// <summary>
        /// Handles the Leave event of the BondExpirationDatesbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BondExpirationDatesbutton_Leave(object sender, EventArgs e)
        {
            if (this.ContractorManagementGridView.OriginalRowCount > 1)
            {
                this.ContractorManagementGridView.Focus();
            }
        }
    }
}
