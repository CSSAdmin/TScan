//--------------------------------------------------------------------------------------------
// <copyright file="F36090.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36090.cs.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20160824       Priyadharshini.R     Created
// 20170111       Priyadharshini      TSBG - D31090.F36090 Income Source form - base rent dividing by 100 on tab out
//*********************************************************************************/

namespace D31090
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
    /// F36090 class file
    /// </summary>
    [SmartPart]
    public partial class F36090 : BaseSmartPart
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
        private string rollYearValue;

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
        /// Used to store the IncomeSourceID(KeyID)
        /// </summary>
        private int? IncomeSourceID;

        /// <summary>
        /// used to store the currrentStreetId
        /// </summary>
        private int currrentStreetId;

        /// <summary>
        /// Usde to store the saved StreetId return form database
        /// </summary>
        private int savedIncomeSourceId;

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
        private F36090Controller form36090Control;

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
        /// IncomeSourceData
        /// </summary>
        private F36090IncomeSourceData IncomeSourceData = new F36090IncomeSourceData();

        /// <summary>
        /// listUnitTermsDataTable
        /// </summary>
        private F36090IncomeSourceData.ListUnitTermsDataTable listUnitTermsDataTable = new F36090IncomeSourceData.ListUnitTermsDataTable();

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
        /// 
        public F36090()
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
        public F36090(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.IncomeSourceID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.IncomeSourcePictureBox.Image = ExtendedGraphics.GenerateVerticalImageWithSmallFont(this.IncomeSourcePictureBox.Height, this.IncomeSourcePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
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
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

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
        public F36090Controller Form36090Control
        {
            get { return this.form36090Control as F36090Controller; }
            set { this.form36090Control = value; }
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
                this.ClearControls();
                if (this.slicePermissionField.newPermission)
                {
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.UnitTermsComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                }

                this.RollYearTextBox.Focus();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
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
                 if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                 {
                     if (this.IncomeSourceID == -99)
                     {
                         this.IncomeSourceID = null;
                     }
                     if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                     {
                         this.IncomeSourceID = null;
                     }
                     F36090IncomeSourceData insertincomesourceDetails = new F36090IncomeSourceData();
                     F36090IncomeSourceData.IncomeSourceItemsRow dr = insertincomesourceDetails.IncomeSourceItems.NewIncomeSourceItemsRow();
                     if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                     {
                        dr.RollYear = Convert.ToInt32(this.RollYearTextBox.Text.Trim());
                     }
                     if (!string.IsNullOrEmpty(this.SourceCodeTextBox.Text.Trim()))
                     {
                         dr.SourceCode = Convert.ToInt32(this.SourceCodeTextBox.Text.Trim());
                     }
                    dr.Description = this.DescriptionTextBox.Text.Trim();
                    dr.LeaseType = this.LeaseTypeTextBox.Text.Trim();
                    dr.UnitType = this.UnitTypeTextBox.Text.Trim();
                    if (!string.IsNullOrEmpty(this.UnitTermsComboBox.Text.Trim()))
                    {
                        dr.UnitTermID = Convert.ToInt32(this.UnitTermsComboBox.SelectedValue);
                    }
                    if (!string.IsNullOrEmpty(this.BaseRentTextBox.Text.Trim()))
                    {
                        dr.BaseRent = Convert.ToDecimal(this.BaseRentTextBox.Text.Trim());
                    }
                    insertincomesourceDetails.IncomeSourceItems.Rows.Add(dr);
                    insertincomesourceDetails.IncomeSourceItems.AcceptChanges();
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Clear();
                    tempDataSet.Tables.Add(insertincomesourceDetails.IncomeSourceItems.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    this.savedIncomeSourceId = this.form36090Control.WorkItem.SaveIncomeSourceDetails(this.IncomeSourceID, tempDataSet.GetXml(), TerraScanCommon.UserId);

                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = this.savedIncomeSourceId;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = this.savedIncomeSourceId;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                 }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.IncomeSourceID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.IncomeSourceID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                 this.PermissionControlLock(!this.PermissionFiled.editPermission);
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

        private void PermissionControlLock(bool controlLook)
        {
            this.ControlLock(controlLook);
        }

        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.SourceCodeTextBox.LockKeyPress = controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.LeaseTypeTextBox.LockKeyPress = controlLook;
            this.UnitTermsComboBox.Enabled = !controlLook;
            this.UnitTypeTextBox.LockKeyPress = controlLook;
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

                    if (this.IncomeSourceData.Tables["GetIncomeSource"].Rows.Count > 0)
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
                // Checks the masterform no is same  
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.IncomeSourceID = eventArgs.Data.SelectedKeyId;

                    if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                    {
                        this.IncomeSourceID = eventArgs.Data.SelectedKeyId;
                        this.FlagSliceForm = true;
                        this.LoadDefaultView();
                        this.GetIncomeSourceDetails(Convert.ToInt32(this.IncomeSourceID));

                    }
                    else if (this.IncomeSourceID == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                    {
                        this.FlagSliceForm = true;
                        this.LoadDefaultView();
                        this.GetIncomeSourceDetails(Convert.ToInt32(this.IncomeSourceID));
                    }
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
            finally
            {
                this.isAfterDelte = false;
                this.isRecordDeleted = false;
            }
        }
        /// <summary>
        /// Gets the income source details.
        /// </summary>
        /// <param name="templateId">The IncomeSourceID id.</param>
        private void GetIncomeSourceDetails(int IncomeSourceID)
        {
            try
            {
                if (this.IncomeSourceData.GetIncomeSource != null)
                {
                    if (this.IncomeSourceData.GetIncomeSource.Rows.Count == 0)
                    {
                        this.IncomeSourceData = this.form36090Control.WorkItem.GetIncomeSourceDetail(Convert.ToInt32(this.IncomeSourceID));
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            try
            {
                if (this.IncomeSourceData.GetIncomeSource != null)
                {
                    if (this.IncomeSourceData.GetIncomeSource.Rows.Count > 0)
                    {
                        this.RollYearTextBox.Text = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.RollYearColumn].ToString();
                        this.SourceCodeTextBox.Text = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.SourceCodeColumn].ToString();
                        this.DescriptionTextBox.Text = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.DescriptionColumn].ToString();
                        this.LeaseTypeTextBox.Text = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.LeaseTypeColumn].ToString();
                        this.UnitTypeTextBox.Text = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.UnitTypeColumn].ToString();
                       // this.UnitTermsComboBox.SelectedValue = this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.UnitTermColumn].ToString();
                        this.UnitTermsComboBox.SelectedIndex = this.UnitTermsComboBox.FindString(this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.UnitTermColumn].ToString());
                    }
                    else
                    {
                        this.IncomeSourceID = -1;
                        this.NullRecords = true;
                        this.ClearControls();
                        this.LoadDefaultView();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                    }
                }
            }
            catch (Exception ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        private void LoadDefaultView()
        {
            this.EnableControls(true);
            this.EnablePanelControl(true);
            this.LoadUnitTermsComboBox();
            this.F36090LoadFormDetails();
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// 
        private void EnableControls(bool show)
        {
            this.RollYearTextBox.Enabled = show;
            this.SourceCodeTextBox.Enabled = show;
            this.DescriptionTextBox.Enabled = show;
            this.LeaseTypeTextBox.Enabled = show;
            this.UnitTypeTextBox.Enabled = show;
            this.UnitTermsComboBox.Enabled = show;
            this.BaseRentTextBox.Enabled = show;
            this.RollYearPanel.Enabled = show;
            this.SourceCodePanel.Enabled = show;
            this.DescriptionPanel.Enabled = show;
            this.LeaseTypePanel.Enabled = show;
            this.UnitTypePanel.Enabled = show;
            this.UnitTermsPanel.Enabled = show;
            this.baseRentPanel.Enabled = show;
        }

        /// <summary>
        /// Enables the panel controls.
        /// </summary>
        /// 
        private void EnablePanelControl(bool view)
        {
            this.IncomeSourceEntirePanel.Enabled = view;
            this.RollYearPanel.Enabled = view;
            this.SourceCodePanel.Enabled = view;
            this.DescriptionPanel.Enabled = view;
            this.LeaseTypePanel.Enabled = view;
            this.UnitTypePanel.Enabled = view;
            this.UnitTermsPanel.Enabled = view;
            this.baseRentPanel.Enabled = view;
        }

        /// <summary>
        /// Enables the Clear controls.
        /// </summary>
        /// 
        public void ClearControls()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.SourceCodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.LeaseTypeTextBox.Text = string.Empty;
            this.UnitTypeTextBox.Text = string.Empty;
            this.UnitTermsComboBox.Text = string.Empty;
            this.BaseRentTextBox.Text = string.Empty;
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
                if (this.slicePermissionField.deletePermission)
                {
                    string ReturnMessage = this.form36090Control.WorkItem.DeleteIncomeSource(Convert.ToInt32(this.IncomeSourceID), TerraScanCommon.UserId);
                    if (!string.IsNullOrEmpty(ReturnMessage))
                    {
                        MessageBox.Show(ReturnMessage, "TerraScan – Income Source in use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));

                    }
                    else
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                        sliceEventArgs.MasterFormNo = this.masterFormNo;
                        sliceEventArgs.AllowNullRecordMode = false;
                        sliceEventArgs.WithoutKeyId = false;
                        this.Cursor = Cursors.Default;
                        this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                        this.ClearControls();
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
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) || this.RollYearTextBox.Text=="0")
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields; 
            }

            if (string.IsNullOrEmpty(this.SourceCodeTextBox.Text.Trim()) ||  Convert.ToInt32(SourceCodeTextBox.Text) > 2147483647)
            {
                this.SourceCodeTextBox.Text = string.Empty;
                this.SourceCodeTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (Convert.ToInt32(SourceCodeTextBox.Text) > 2147483647)
            {
                this.SourceCodeTextBox.Text = string.Empty;
                this.SourceCodeTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.DescriptionTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.LeaseTypeTextBox.Text.Trim()))
            {
                this.LeaseTypeTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.BaseRentTextBox.Text.Trim()) || Convert.ToDouble(BaseRentTextBox.Text) > 922337203685477.58)
            {
                this.BaseRentTextBox.Text = string.Empty;
                this.BaseRentTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            if (this.UnitTermsComboBox.SelectedIndex == -1)
            {
                this.UnitTermsComboBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            return sliceValidationFields;
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
        /// To load the Unit Terms Combo Box
        /// </summary>
        private void LoadUnitTermsComboBox()
        {
            try
            {
                if (this.IncomeSourceData.ListUnitTerms != null)
                {
                    if (this.IncomeSourceData.ListUnitTerms.Rows.Count == 0)
                    {
                        IncomeSourceData = this.form36090Control.WorkItem.ListUnitTerms();
                    }
                }
                
                this.UnitTermsComboBox.ValueMember = "UnitTermID";
                this.UnitTermsComboBox.DisplayMember = "UnitTerm";
                this.UnitTermsComboBox.DataSource = IncomeSourceData.ListUnitTerms;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

      
        #endregion StreetListing Grid Methods

        private void F36090LoadFormDetails()
        {
            try
            {
                if (this.IncomeSourceData.GetIncomeSource != null)
                {
                    if (this.IncomeSourceData.GetIncomeSource.Rows.Count == 0)
                    {
                        this.IncomeSourceData = this.form36090Control.WorkItem.GetIncomeSourceDetail(Convert.ToInt32(this.IncomeSourceID));
                    }
                }
                if (IncomeSourceData.Tables["GetIncomeSource"].Rows.Count > 0)
                {
                    this.RollYearTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["RollYear"].ToString();
                    this.SourceCodeTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["SourceCode"].ToString();
                    this.DescriptionTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["Description"].ToString();
                    this.LeaseTypeTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["LeaseType"].ToString();
                    this.UnitTypeTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["UnitType"].ToString();
                    this.BaseRentTextBox.Text = this.IncomeSourceData.Tables["GetIncomeSource"].Rows[0]["BaseRent"].ToString();
                    this.UnitTermsComboBox.SelectedIndex = this.UnitTermsComboBox.FindString(this.IncomeSourceData.GetIncomeSource.Rows[0][this.IncomeSourceData.GetIncomeSource.UnitTermColumn].ToString());
                    this.BaseRentTextBox.Text = AmountFormat(this.BaseRentTextBox.Text);
                   // BaseRentFormat(BaseRentTextBox.Text);
                }
                else
                {
                    this.ClearControls();
                    this.EnableControls(false);
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void F36090_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.EnableControls(true);
                this.LoadUnitTermsComboBox();
                this.LoadDefaultView();
                //this. LoadHeaderStreetDetails();
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

        private void SourceCodeTextBox_TextChanged(object sender, EventArgs e)
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

        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
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

        private void LeaseTypeTextBox_TextChanged(object sender, EventArgs e)
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

        private void UnitTypeTextBox_TextChanged(object sender, EventArgs e)
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

        private void BaseRentTextBox_TextChanged(object sender, EventArgs e)
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

        private void IncomeSourcePictureBox_Click(object sender, EventArgs e)
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

        private void IncomeSourcePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.StreetListMgmtToolTip.SetToolTip(this.IncomeSourcePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
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

        private void UnitTermsComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void BaseRentTextBox_Leave(object sender, EventArgs e)
        {
            this.BaseRentTextBox.Text = AmountFormat(this.BaseRentTextBox.Text);
        }

        //TSBG - D31090.F36090 Income Source form - base rent dividing by 100 on tab out
        public static string AmountFormat(string strval)
        {
            string retval = "";
            decimal outDecimalContractperunit;
            if (!String.IsNullOrEmpty(strval))
            {
                if (Decimal.TryParse(strval, out outDecimalContractperunit))
                {
                    retval = outDecimalContractperunit.ToString("#,##0.00");
                }
                else
                {
                    retval = "";
                }
            }
            else
            {
                retval = "";
            }
            return retval;
        }
       
    }
}
