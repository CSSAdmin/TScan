//--------------------------------------------------------------------------------------------
// <copyright file="F25011.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25011.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/05/2006       M.Vijaya Kumar     Created// 
//*********************************************************************************/

namespace D20003
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

    /// <summary>
    /// F25011 class file
    /// </summary>
    [SmartPart]
    public partial class F25011 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to store the isunSavedMessageraised
        /// </summary>
        private bool isunSavedMessageraised;

        /// <summary>
        /// Used to store the unsavedMessageraisedAndSaved
        /// </summary>
        private bool unsavedMessageraisedAndSaved;

        /// <summary>
        /// Used to store the isnewlysaved
        /// </summary>
        private bool isnewlysaved;

        /// <summary>
        /// Used to store igonreTextChanged
        /// </summary>
        private bool igonreTextChanged;

        /// <summary>
        /// Used to store the isnewOpertion
        /// </summary>
        private bool isnewOpertion;

        /// <summary>
        /// Used to store the saveEventFired
        /// </summary>
        private bool saveEventFired;

        /// <summary>
        /// Used to store the streetNameValue
        /// </summary>
        private string streetNameValue;

        /// <summary>
        /// Used to store the street Name text box on search button click;
        /// </summary>
        private string searchStreetName;

        /// <summary>
        /// Used to store the City text box vale on seacrh condition;
        /// </summary>
        private string searchCityText;

        /// <summary>
        /// Used to store tempStreetListGridRowId
        /// </summary>
        private int tempStreetListGridRowId;

        /// <summary>
        /// Used to store the tempstreetIdvalue
        /// </summary>
        private int tempstreetIdvalue;

        /// <summary>
        /// Used to store the streetId(KeyID)
        /// </summary>
        private int? streetId;

        /// <summary>
        /// used to store the currrentStreetId
        /// </summary>
        private int currrentStreetId;

        /// <summary>
        /// Usde to store the saved StreetId return form database
        /// </summary>
        private int savedStreetId;

        /// <summary>
        /// Used to store validKeyId
        /// </summary>
        private int validKeyId;

        private int currentRowIndexVaalue;

        /// <summary>
        /// Used to store the listStreetMgmtDataTableRowCount
        /// </summary>
        private int listStreetMgmtDataTableRowCount; 

        /// <summary>
        /// controller F25011
        /// </summary>
        private F25011Controller form25011Control;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;
      
        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        private bool isDeletedRecord = false;
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// streetListManagementData
        /// </summary>
        private F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();

        /// <summary>
        /// listStreetManagementDataTable
        /// </summary>
        private F25011StreetListManagementData.ListStreetManagementDataTable listStreetManagementDataTable = new F25011StreetListManagementData.ListStreetManagementDataTable();

        /// <summary>
        /// listCityDataTable
        /// </summary>
        private F25011StreetListManagementData.ListCityDataTable listCityDataTable = new F25011StreetListManagementData.ListCityDataTable();

        /// <summary>
        /// listDirectionalDataTable
        /// </summary>
        private F25011StreetListManagementData.ListDirectionalDataTable listDirectionalDataTable = new F25011StreetListManagementData.ListDirectionalDataTable();

        /// <summary>
        /// listSuffixDataTable
        /// </summary>
        private F25011StreetListManagementData.ListSuffixDataTable listSuffixDataTable = new F25011StreetListManagementData.ListSuffixDataTable();

        /// <summary>
        /// Flag to identify whether the record got deleted
        /// </summary>
        private bool isRecordDeleted = false;
        
        /// <summary>
        /// Flag to identify the load event firing after delete 
        /// </summary>
        private bool isAfterDelte = false;

        DataSet StreetHeaderDetails = new DataSet();

        #region Form Slice Variables

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F25011"/> class.
        /// </summary>
        public F25011()
        {
            InitializeComponent();
        }

          /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F25011(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.streetId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.StreetListingPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StreetListingPictureBox.Height, this.StreetListingPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            /////todothis.AllOwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AllOwnersPictureBox.Height, this.AllOwnersPictureBox.Width, "All Owners", 174, 150, 94);   ////todo remove hard code value                     
        }

        #endregion Constructor     
 
        #region Event Publication

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_CancelEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_CancelEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_CancelEnabled;

        /// <summary>
        /// Occurs when [form slice_ null record mode].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_NullRecordMode;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;

        #endregion Event Publication

   
        #region Property

        /// <summary>
        /// For F25011Control
        /// </summary>
        [CreateNew]
        public F25011Controller Form25011Control
        {
            get { return this.form25011Control as F25011Controller; }
            set { this.form25011Control = value; }
        }

        #endregion Property 

        #region Event Subscription

       
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
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControls();
                if (this.slicePermissionField.newPermission)
                {
                    this. EnableControls();
                    this.LoadStreetListManagement();
                    this.LoadDefaultView();
                    // Set focus on firs editanle field
                    this.FullStreetNameTextBox.Focus();
                }
                else
                {
                    this.DisableControls();
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
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                this.flagLoadOnProcess = true;                
                this.ClearControls();
                this.LoadDefaultView();
                this.LoadHeaderStreetDetails();
                this.flagLoadOnProcess = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            try
            {
                //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
                 if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                 {
                     if (this.streetId == -99)
                     {
                         this.streetId = null;
                     }
                    this.streetListManagementData.ListStreetManagement.Rows.Clear();
                    F25011StreetListManagementData.ListStreetManagementRow dr = this.streetListManagementData.ListStreetManagement.NewListStreetManagementRow();

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.streetId = null;
                    }
                    dr.FullStreetName = this.FullStreetNameTextBox.Text.Trim();

                    if (!string.IsNullOrEmpty(this.CityComboBox.Text.Trim()))
                    {
                        dr.City = this.CityComboBox.Text.Trim();
                    }

                    dr.StreetName = this.StreetNameTextBox.Text.Trim();
                    if (!string.IsNullOrEmpty(this.DirectionalComboBox.Text.Trim()))
                    {
                        dr.Directional = this.DirectionalComboBox.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(this.SuffixComboBox.Text.Trim()))
                    {
                        dr.Suffix = this.SuffixComboBox.Text.Trim();
                    }

                    dr.ZipCode = this.ZipCodeTextBox.Text.Trim();
                    this.streetListManagementData.ListStreetManagement.Rows.Add(dr);

                    this.savedStreetId = this.form25011Control.WorkItem.F25011_SaveStreetListManagement(Convert.ToInt32(this.streetId), (Utility.GetXmlString(this.streetListManagementData.ListStreetManagement.Copy())), TerraScanCommon.UserId);

                    //this.streetId = this.savedStreetId;
                    //SliceReloadActiveRecord currentSliceInfo;
                    //currentSliceInfo.MasterFormNo = this.masterFormNo;
                    //currentSliceInfo.SelectedKeyId = savedStreetId;//this.streetId;
                    //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                    //this.pageMode = TerraScanCommon.PageModeTypes.View;

                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = savedStreetId;
                    //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));

                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = savedStreetId;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;

                 }
                else
                {
                    this.savedStreetId = this.form25011Control.WorkItem.F25011_SaveStreetListManagement(0, (Utility.GetXmlString(this.streetListManagementData.ListStreetManagement.Copy())), TerraScanCommon.UserId);

                    //this.streetId = this.savedStreetId;
                    //SliceReloadActiveRecord currentSliceInfo;
                    //currentSliceInfo.MasterFormNo = this.masterFormNo;
                    //currentSliceInfo.SelectedKeyId = Convert.ToInt32(this.streetId);//this.streetId;
                    //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                    //this.pageMode = TerraScanCommon.PageModeTypes.View;


                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.streetId);
                    //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.streetId);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
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

                    if (this.StreetHeaderDetails.Tables["GetStreetListManagement"].Rows.Count > 0)
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
                    this.streetId = eventArgs.Data.SelectedKeyId;
                    if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                    {
                        this.streetId = eventArgs.Data.SelectedKeyId;
                        this.flagLoadOnProcess = true;
                        this.FlagSliceForm = true;
                        this.LoadStreetListManagement();
                        this.LoadHeaderStreetDetails();
                        this.LoadDefaultView();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.flagLoadOnProcess = false;
                    }
                    else if (this.streetId == eventArgs.Data.SelectedKeyId)  
                    {
                        this.flagLoadOnProcess = true;
                        this.FlagSliceForm = true;
                        this.EnableControls();
                        this.LoadStreetListManagement();
                        this.LoadHeaderStreetDetails();
                        this.LoadHeaderStreetDetails();
                        this.LoadDefaultView();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.flagLoadOnProcess = false;
                        isDeletedRecord = false;
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
            finally
            {
                this.isAfterDelte = false;
                this.isRecordDeleted = false;
            }
        }

        /// <summary>
        /// Loads the default view.
        /// </summary>
        public void LoadDefaultView()
        {
            this.HeaderMaintenancePanel.Enabled = true;
            this.StreetMaintenanceSecPanel.Enabled = true;
            this.FullStreetNameTextBox.Enabled = true;
            this.CityComboBox.Enabled = true;
            this.StreetNameTextBox.Enabled = true;
            this.DirectionalComboBox.Enabled = true;
            this.SuffixComboBox.Enabled = true;
            this.ZipCodeTextBox.Enabled = true;
        }

        /// <summary>
        /// Disables the controls.
        /// </summary>
        public  void DisableControls()
        {
            this.HeaderMaintenancePanel.Enabled = false;
            this.StreetMaintenanceSecPanel.Enabled = false;
            this.FullStreetNamePanel.Enabled = false;
            this.StreetNamePanel.Enabled = false;
            this.CityPanel.Enabled = false;
            this.DirectionalPanel.Enabled = false;
            this.StreetNamePanel.Enabled = false;
            this.SuffixPanel.Enabled = false;
            this.ZipCodePanel.Enabled = false;
            this.FullStreetNameTextBox.Enabled = false;
            this.CityComboBox.Enabled = false;
            this.StreetNameTextBox.Enabled = false;
            this.DirectionalComboBox.Enabled = false;
            this.SuffixComboBox.Enabled = false;
            this.ZipCodeTextBox.Enabled = false;
        }

        private void EnableControls()
        {
            this.FullStreetNamePanel.Enabled = true;
            this.StreetNamePanel.Enabled = true;
            this.CityPanel.Enabled = true;
            this.DirectionalPanel.Enabled = true;
            this.StreetNamePanel.Enabled = true;
            this.ZipCodePanel.Enabled = true;
            this.HeaderMaintenancePanel.Enabled = true;
            this.StreetMaintenanceSecPanel.Enabled = true;
            this.SuffixPanel.Enabled = true;
            this.FullStreetNameTextBox.Enabled = true;
            this.CityComboBox.Enabled = true;
            this.StreetNameTextBox.Enabled = true;
            this.DirectionalComboBox.Enabled = true;
            this.SuffixComboBox.Enabled = true;
            this.ZipCodeTextBox.Enabled = true;
        }
        public void ClearControls()
        {
            this.FullStreetNameTextBox.Text = string.Empty;
            this.CityComboBox.Text = string.Empty;
            this.StreetNameTextBox.Text = string.Empty;
            this.DirectionalComboBox.Text = string.Empty;
            this.SuffixComboBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;
        }
        
        #region DeleteSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                this.isAfterDelte = true;
                if (this.slicePermissionField.deletePermission)
                {
                    int deletedStreetId = this.form25011Control.WorkItem.F25011_DeleteStreetList(Convert.ToInt32(this.streetId), TerraScanCommon.UserId);
                    if (deletedStreetId > 0)
                    {
                        MessageBox.Show("The following street record cannot be deleted because it is currently reference by one or more records in the application.", "Cannot Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                        this.isRecordDeleted = false;
                    }
                    else
                    {
                        this.isDeletedRecord = true;
                        this.isRecordDeleted = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                        sliceEventArgs.MasterFormNo = this.masterFormNo;
                        sliceEventArgs.AllowNullRecordMode = false;
                        sliceEventArgs.WithoutKeyId = false;                        
                        this.Cursor = Cursors.Default;
                        this.FormSlice_NullRecordMode(this, new DataEventArgs<int>(this.masterFormNo));
                        this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                        this.isunSavedMessageraised = false;
                        this.unsavedMessageraisedAndSaved = false;
                        this.ClearControls();
                       //this.DisableControls();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }

                
            }
        }

        #endregion DeleteSliceInformation
        #endregion Event Subscription

        #region Protected methods


        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
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

        #endregion Protected methods

        #region StreetListing Grid Methods


        /// <summary>
        /// Used to clear all Controls in the street list management form slice
        /// </summary>
        private void NewStreetListManagement()
        {
            
        }

        private void StreetNameTextChanged()
        {

            this.streetNameValue = string.Empty;

            if (!string.IsNullOrEmpty(this.DirectionalComboBox.Text.Trim()))
            {
                this.streetNameValue = this.streetNameValue + " " + this.DirectionalComboBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.StreetNameTextBox.Text.Trim()))
            {
                this.streetNameValue = this.streetNameValue + " " + this.StreetNameTextBox.Text.Trim();
            }

            if (!string.IsNullOrEmpty(this.SuffixComboBox.Text.Trim()))
            {
                this.streetNameValue = this.streetNameValue + " " + this.SuffixComboBox.Text.Trim();
            }

            this.FullStreetNameTextBox.Text = this.streetNameValue;
            this.streetNameValue = string.Empty;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.FullStreetNameTextBox.Text.Trim()))
            {
                this.FullStreetNameTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields; 
            }

            if (string.IsNullOrEmpty(this.StreetNameTextBox.Text.Trim()))
            {
                this.StreetNameTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }
          
        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.FullStreetNameTextBox.MaxLength = this.listStreetManagementDataTable.FullStreetNameColumn.MaxLength;
            this.CityComboBox.MaxLength = this.listStreetManagementDataTable.CityColumn.MaxLength;
            this.CityComboxTextBox.MaxLength = this.listStreetManagementDataTable.CityColumn.MaxLength;                
            this.StreetNameTextBox.MaxLength = this.listStreetManagementDataTable.StreetNameColumn.MaxLength;
            this.DirectionalComboBox.MaxLength = this.listStreetManagementDataTable.DirectionalColumn.MaxLength;
           // this.DirectionalComboBoxTextBox.MaxLength = this.listStreetManagementDataTable.DirectionalColumn.MaxLength;
            this.SuffixComboBox.MaxLength = this.listStreetManagementDataTable.SuffixColumn.MaxLength;
            this.SuffixComboBoxTextBox.MaxLength = this.listStreetManagementDataTable.SuffixColumn.MaxLength;
            this.ZipCodeTextBox.MaxLength = this.listStreetManagementDataTable.ZipCodeColumn.MaxLength;

            
        }

        /// <summary>
        /// Clears the street list maintanenec part.
        /// </summary>
        private void ClearStreetListMaintanenecPart()
        {
            this.igonreTextChanged = true;
            this.ClearStreetMaintanenceSection();
            this.igonreTextChanged = false;
        }



        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Clear street Maintanence section
        /// </summary>
        private void ClearStreetMaintanenceSection()
        {
            this.FullStreetNameTextBox.Text = string.Empty;
            this.StreetNameTextBox.Text = string.Empty;
            this.ZipCodeTextBox.Text = string.Empty;
            this.PopulateCityDirSufCombox(this.CityComboBox, string.Empty);
            this.PopulateCityDirSufCombox(this.SuffixComboBox, string.Empty);
            this.PopulateCityDirSufCombox(this.DirectionalComboBox, string.Empty);
        }      

        /// <summary>
        /// To lOad the city Combo Box
        /// </summary>
        private void LoadCityComboBox()
        {
            DataRow customRow = this.listCityDataTable.NewRow();
            this.listCityDataTable.Clear();
            customRow[this.listCityDataTable.CityColumn.ColumnName] = string.Empty;
            customRow[this.listCityDataTable.CityIDColumn.ColumnName] = "0";
            this.listCityDataTable.Rows.Add(customRow);
            this.listCityDataTable.Merge(this.streetListManagementData.ListCity);

            CityComboBox.DataSource = this.listCityDataTable;
            CityComboBox.DisplayMember = this.listCityDataTable.CityColumn.ColumnName;
            CityComboBox.ValueMember = this.listCityDataTable.CityIDColumn.ColumnName;
        }

        /// <summary>
        /// To load Suffix Combo Box
        /// </summary>
        private void LoadSuffixComboBox()
        {
            DataRow customRow = this.listSuffixDataTable.NewRow();
            this.listSuffixDataTable.Clear();
            customRow[this.listSuffixDataTable.SuffixColumn.ColumnName] = string.Empty;
            customRow[this.listSuffixDataTable.SuffixIDColumn.ColumnName] = "0";
            this.listSuffixDataTable.Rows.Add(customRow);
            this.listSuffixDataTable.Merge(this.streetListManagementData.ListSuffix);

            SuffixComboBox.DataSource = this.listSuffixDataTable;
            SuffixComboBox.DisplayMember = this.listSuffixDataTable.SuffixColumn.ColumnName;
            SuffixComboBox.ValueMember = this.listSuffixDataTable.SuffixIDColumn.ColumnName;
        }

        /// <summary>
        /// To load the Directional Combo Box
        /// </summary>
        private void LoadDirectionalComboBox()
        {
            DataRow customRow = this.listDirectionalDataTable.NewRow();
            this.listDirectionalDataTable.Clear();
            customRow[this.listDirectionalDataTable.DirectionalColumn.ColumnName] = string.Empty;
            customRow[this.listDirectionalDataTable.DirectionalIDColumn.ColumnName] = "0";
            this.listDirectionalDataTable.Rows.Add(customRow);
            this.listDirectionalDataTable.Merge(this.streetListManagementData.ListDirectional);

            DirectionalComboBox.DataSource = this.listDirectionalDataTable;
            DirectionalComboBox.DisplayMember = this.listDirectionalDataTable.DirectionalColumn.ColumnName;
            DirectionalComboBox.ValueMember = this.listDirectionalDataTable.DirectionalIDColumn.ColumnName;
        }

        /// <summary>
        /// To populate the city directional and suffix Combo Box
        /// </summary>
        /// <param name="controlName">controlName</param>
        /// <param name="comboDisplayValue">comboDisplayValue</param>
        private void PopulateCityDirSufCombox(TerraScanComboBox controlName, string comboDisplayValue)
        {
            if (!string.IsNullOrEmpty(comboDisplayValue))
            {
                controlName.Text = comboDisplayValue;
            }
            else
            {
                controlName.Text = string.Empty;
                controlName.SelectedIndex = -1;
            }
        }       


        /// <summary>
        /// To Load the street List Management
        /// </summary>
        private void LoadStreetListManagement()
        {
            this.streetListManagementData = this.form25011Control.WorkItem.F25011_ListStreetCityDirectionalSuffixDetails();
            this.LoadCityComboBox();
            this.LoadDirectionalComboBox();
            this.LoadSuffixComboBox();
        }


        private void LoadHeaderStreetDetails()
        {
           // this.StreetHeaderDetails.Tables["GetStreetListManagement"].Clear();
            this.ClearControls();
            StreetHeaderDetails = this.form25011Control.WorkItem.F25011_GetMasterStreetList(Convert.ToInt32(this.streetId));
            if (StreetHeaderDetails.Tables[4].Rows.Count > 0)
            {
                this.FullStreetNameTextBox.Text = StreetHeaderDetails.Tables[4].Rows[0]["FullStreetName"].ToString();
                this.CityComboBox.Text = StreetHeaderDetails.Tables["GetStreetListManagement"].Rows[0]["City"].ToString();
                this.StreetNameTextBox.Text = StreetHeaderDetails.Tables["GetStreetListManagement"].Rows[0]["StreetName"].ToString();
                this.DirectionalComboBox.Text = StreetHeaderDetails.Tables["GetStreetListManagement"].Rows[0]["Directional"].ToString();
                this.ZipCodeTextBox.Text = StreetHeaderDetails.Tables["GetStreetListManagement"].Rows[0]["ZipCode"].ToString();
                this.SuffixComboBox.Text = StreetHeaderDetails.Tables["GetStreetListManagement"].Rows[0]["Suffix"].ToString();
            }
        }
       
        #endregion StreetListing Grid Methods

        #region Street Listing Events

        /// <summary>
        /// Handles the Load event of the F25011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F25011_Load(object sender, EventArgs e)
        {
            try 
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.EnableControls();
                this.LoadStreetListManagement();
                this.LoadDefaultView();
                this. LoadHeaderStreetDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
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
        /// Handles the Click event of the StreetListingPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetListingPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the StreetListingPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StreetListingPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.StreetListMgmtToolTip.SetToolTip(this.StreetListingPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       
        /// <summary>
        /// Handles the TextChanged event of the FullStreetNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FullStreetNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {                
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       

        /// <summary>
        /// Handles the TextChanged event of the ZipCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ZipCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        private void CityComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
            this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void DirectionalComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.StreetNameTextChanged();
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SuffixComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.StreetNameTextChanged();
                this.SetEditRecord();
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }
        #endregion Street Listing Events

        private void DirectionalComboBoxTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
               // this.StreetNameTextChanged();
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void StreetNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
                this.SetEditRecord();
                this.StreetNameTextChanged();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }
}
