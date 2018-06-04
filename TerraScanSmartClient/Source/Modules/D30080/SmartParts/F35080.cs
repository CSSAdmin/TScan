namespace D30080
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;


    [SmartPart]
    public partial class F35080 : BaseSmartPart
    {

        #region Variables

        private int masterFormNo;
        private int? centralID;

        private bool formMasterPermissionEdit;
        private bool navigationFlag;
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

        private int ownerId;
        private int centailId;
        private string rollYear;

        private string linkLabelName;
        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        private F35080Controller form35080Control;

        private PermissionFields slicePermissionField;

        private F35080CentralAssessedOwner centralOwnerDetailsLoad = new F35080CentralAssessedOwner();

        private F35080CentralAssessedOwner.f35080PropertyClassDataTable ComboBoxLoad = new F35080CentralAssessedOwner.f35080PropertyClassDataTable();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;
        #endregion

        public F35080()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F35080"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35080(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.centralID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.OwnersPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnersPictureBox.Height, this.OwnersPictureBox.Width, sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;
        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// Occurs when [form slice_ null record mode].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_NullRecordMode;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion Event Publication

        #region Property
        /// <summary>
        /// For F25000Control
        /// </summary>
        [CreateNew]
        public F35080Controller Form35080Control
        {
            get { return this.form35080Control as F35080Controller; }
            set { this.form35080Control = value; }
        }
        #endregion Property


        #region Event Subscription

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

                    if (this.centralOwnerDetailsLoad.Tables[1].Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                    //this.OwnerPicture.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.ClearControls();
            this.LoadDefaultView();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// OnD9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            // Checks the masterform no is same  
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.centralID = eventArgs.Data.SelectedKeyId;

                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    this.centralID = eventArgs.Data.SelectedKeyId;
                    this.flagLoadOnProcess = true;
                    //this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadPropertyComboBox();
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
                else if (this.centralID == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                {
                    this.flagLoadOnProcess = true;
                    // this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadPropertyComboBox();
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }

            }

        }

        private void ControlLock(bool controlLook)
        {
            this.ParcelNumberTextBox.LockKeyPress = controlLook;
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.CompanyNameTextBox.LockKeyPress = controlLook;
            this.CompanyNumberTextBox.LockKeyPress = controlLook;
            this.BranchLineTextBox.LockKeyPress = controlLook;
            this.PropertyClassComboBox.Enabled = !controlLook;
            this.OwnerLinkLabel.Enabled = !controlLook;
            this.OwnerPicture.Enabled = !controlLook;
        }
        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo )
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
            {
                this.ParcelNumberTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (this.PropertyClassComboBox.SelectedIndex == -1)
            {
                this.PropertyClassComboBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if(string.IsNullOrEmpty(this.OwnerLinkLabel.Text.Trim())&& ownerId>0)
            {
                this.OwnerLinkLabel.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            return sliceValidationFields;
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
                if (this.slicePermissionField.newPermission)
                {                   
                    this.ClearControls();
                    this.LoadPropertyComboBox();
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    //this.centralID = -99;                    
                    this.PropertyClassComboBox.SelectedIndex = -1;
                    this.OwnerPicture.Enabled = true;
                    this.ParcelNumberTextBox.Focus();

                }
                else
                {
                    // Clear all the controls and make it disable
                   
                    this.ClearControls();
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                    this.OwnerPicture.Enabled = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
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
            if (this != null && this.IsDisposed != true)
            {

                if (this.slicePermissionField.deletePermission)
                { 
                    this.Cursor = Cursors.WaitCursor;
                    this.form35080Control.WorkItem.F35080_DeleteOwnerDetails(Convert.ToInt32(this.centralID), TerraScanCommon.UserId);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;
                   
                    SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                    sliceEventArgs.MasterFormNo = this.masterFormNo;
                    sliceEventArgs.AllowNullRecordMode = false;
                    sliceEventArgs.WithoutKeyId = false;
                    this.FormSlice_NullRecordMode(this, new DataEventArgs<int>(this.masterFormNo));
                    this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                }
                
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
            //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            //{                
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    if (this.centralID == -99)
                    {
                        this.centralID = null;
                    }
                    F35080CentralAssessedOwner updateparcelDetails = new F35080CentralAssessedOwner();
                    F35080CentralAssessedOwner.f35080_updateCentralOwnerDetailsRow dr = updateparcelDetails.f35080_updateCentralOwnerDetails.Newf35080_updateCentralOwnerDetailsRow();

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.centralID = null;
                    }

                    dr.ParcelNumber = this.ParcelNumberTextBox.Text;
                    dr.RollYear = this.RollYearTextBox.Text;
                    dr.CompanyName = this.CompanyNameTextBox.Text;
                    dr.CompanyNumber = this.CompanyNumberTextBox.Text;
                    dr.BranchLine = this.BranchLineTextBox.Text;
                    if (!string.IsNullOrEmpty(this.PropertyClassComboBox.Text))
                    {
                        dr.PropertyClassID = this.PropertyClassComboBox.SelectedValue.ToString();

                    }
                    if (!string.IsNullOrEmpty(this.OwnerLinkLabel.Text))
                    {
                        dr.OwnerID = ownerId.ToString();
                    }
                    dr.Address1 = this.Address1TextBox.Text;
                    dr.Address2 = this.Address2TextBox.Text;
                    dr.City = this.CityTextBox.Text;
                    dr.Zip = this.ZipTextBox.Text;
                    dr.State = this.StateTextBox.Text;
                    dr.OwnershipCode = this.OwnerShipTextBox.Text;
                    updateparcelDetails.f35080_updateCentralOwnerDetails.Rows.Add(dr);
                    updateparcelDetails.f35080_updateCentralOwnerDetails.AcceptChanges();
                    DataSet tempDataSet = new DataSet("root");
                    tempDataSet.Tables.Add(updateparcelDetails.f35080_updateCentralOwnerDetails.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    int returnValue;
                    //if (centralID > 0)
                    //{
                    returnValue = this.form35080Control.WorkItem.F35080_InsertOwnerCentralDetails(centralID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue; //Convert.ToInt32(this.centralID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;//Convert.ToInt32(this.centralID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    //}
                    //else
                    //{
                    //    returnValue = this.form35080Control.WorkItem.F35080_InsertOwnerCentralDetails(null, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    //    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    //    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    //    sliceUpdateActiveRecord.SelectedKeyId = returnValue;
                    //    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    //    SliceReloadActiveRecord currentKeyIdInfo;
                    //    currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                    //    currentKeyIdInfo.SelectedKeyId = returnValue;
                    //    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                    //    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    //    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    //    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    //    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    //}

                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.PermissionControlLock(!this.PermissionFiled.editPermission);
            }
            catch (Exception Ex)
            {
            }
                
        }
        private void PermissionControlLock(bool controlLook)
        {
            this.ControlLock(controlLook);
        }
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
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84721_OnSave_GetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.centralID = eventArgs.Data.SelectedKeyId;
                    this.F35080LoadFormDetails();
                }
            }
        }
        #endregion

        /// <summary>
        /// Loaads the data
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">EventArgs</param>
        public void F35080_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadPropertyComboBox();
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.CompanyNameTextBox.Text = string.Empty;
            this.CompanyNumberTextBox.Text = string.Empty;
            this.BranchLineTextBox.Text = string.Empty;
            this.PropertyClassComboBox.SelectedIndex = -1;
            this.OwnerLinkLabel.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.OwnerShipTextBox.Text = string.Empty;
        }

        private void LoadPropertyComboBox()
        {

            this.ComboBoxLoad = form35080Control.WorkItem.F35080_PropertyClassCombo().f35080PropertyClass;
            if (ComboBoxLoad.Rows.Count > 0)
            {
                this.PropertyClassComboBox.DataSource = ComboBoxLoad;
                this.PropertyClassComboBox.DisplayMember = this.ComboBoxLoad.PropertyClassColumn.ColumnName;
                this.PropertyClassComboBox.ValueMember = this.ComboBoxLoad.PropertyClassIDColumn.ColumnName;
            }
        }
        private void EnableControls(bool show)
        {
            this.ParcelNumberTextBox.Enabled = show;
            this.RollYearTextBox.Enabled = show;
            this.CompanyNameTextBox.Enabled = show;
            this.CompanyNumberTextBox.Enabled = show;
            this.BranchLineTextBox.Enabled = show;
            this.PropertyClassComboBox.Enabled = show;
            this.OwnerLinkLabel.Enabled = show;
            this.OwnerPicture.Enabled = show;
            //this.Address1TextBox.Enabled=show;
            //this.Address2TextBox.Enabled=show;
            //this.CityTextBox.Enabled=show;
            //this.StateTextBox.Enabled=show;
            //this.ZipTextBox.Enabled=show;
            //this.OwnerShipTextBox.Enabled = show;
        }
        private void EnablePanelControl(bool view)
        {
            this.panel1.Enabled = view;
            this.panel2.Enabled = view;
            this.panel3.Enabled = view;
            this.panel4.Enabled = view;
            this.panel5.Enabled = view;
            this.panel6.Enabled = view;
            this.panel7.Enabled = view;
            this.panel8.Enabled = view;
            this.panel9.Enabled = view;
            this.ParcelNumberPanel.Enabled = view;
            this.CompanyNumberPanel.Enabled = view;
            this.OwnerPanel.Enabled = view;
            this.CityPanel.Enabled = view;
        }

        private void LoadDefaultView()
        {
            this.EnableControls(true);
            this.EnablePanelControl(true);
            this.F35080LoadFormDetails();
        }

        private void SaveButtonClick()
        {
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                F35080CentralAssessedOwner updateparcelDetails = new F35080CentralAssessedOwner();
                F35080CentralAssessedOwner.f35080_updateCentralOwnerDetailsRow dr = updateparcelDetails.f35080_updateCentralOwnerDetails.Newf35080_updateCentralOwnerDetailsRow();

                dr.ParcelNumber = this.ParcelNumberTextBox.Text;
                dr.RollYear = this.RollYearTextBox.Text;
                dr.CompanyName = this.CompanyNameTextBox.Text;
                dr.CompanyNumber = this.CompanyNumberTextBox.Text;
                dr.BranchLine = this.BranchLineTextBox.Text;
                if (!string.IsNullOrEmpty(this.PropertyClassComboBox.Text))
                {
                    dr.PropertyClassID = this.PropertyClassComboBox.SelectedValue.ToString();

                }
                if (!string.IsNullOrEmpty(this.OwnerLinkLabel.Text))
                {
                    dr.OwnerID = ownerId.ToString();
                }
                dr.Address1 = this.Address1TextBox.Text;
                dr.Address2 = this.Address2TextBox.Text;
                dr.City = this.CityTextBox.Text;
                dr.Zip = this.ZipTextBox.Text;
                dr.State = this.StateTextBox.Text;
                dr.OwnershipCode = this.OwnerShipTextBox.Text;
                updateparcelDetails.f35080_updateCentralOwnerDetails.Rows.Add(dr);
                updateparcelDetails.f35080_updateCentralOwnerDetails.AcceptChanges();
                DataSet tempDataSet = new DataSet("root");
                tempDataSet.Tables.Add(updateparcelDetails.f35080_updateCentralOwnerDetails.Copy());
                tempDataSet.Tables[0].TableName = "Table";
                int returnValue;
                if (centralID > 0)
                {
                    returnValue = this.form35080Control.WorkItem.F35080_InsertOwnerCentralDetails(centralID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                else
                {
                    returnValue = this.form35080Control.WorkItem.F35080_InsertOwnerCentralDetails(null, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord currentKeyIdInfo;
                    currentKeyIdInfo.MasterFormNo = this.masterFormNo;
                    currentKeyIdInfo.SelectedKeyId = returnValue;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
                    SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                }
                
            }
            else
            {
                SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.centralID);
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        private void F35080LoadFormDetails()
        {

            try
            {
                this.centralOwnerDetailsLoad = form35080Control.WorkItem.F35080_CentralAssessedOwnerDetails(Convert.ToInt32(this.centralID));
                if (centralOwnerDetailsLoad.Tables[1].Rows.Count > 0)
                {
                    this.ParcelNumberTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["ParcelNumber"].ToString();
                    this.RollYearTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["RollYear"].ToString();
                    this.CompanyNameTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["CompanyName"].ToString();
                    this.CompanyNumberTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["CompanyNumber"].ToString();
                    this.BranchLineTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["BranchLine"].ToString();
                    if (!string.IsNullOrEmpty(this.centralOwnerDetailsLoad.Tables[1].Rows[0]["PropertyClassID"].ToString()))
                    {
                        this.PropertyClassComboBox.SelectedValue = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["PropertyClassID"].ToString();
                        this.form35080Control.WorkItem.RootWorkItem.State["PropertyClassID"] = this.PropertyClassComboBox.SelectedValue.ToString();
                    }
                    this.OwnerLinkLabel.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["Owner"].ToString();
                    this.ownerId = Convert.ToInt32(this.centralOwnerDetailsLoad.Tables[1].Rows[0]["OwnerID"].ToString());
                    this.Address1TextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["Address1"].ToString();
                    this.Address2TextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["Address2"].ToString();
                    this.CityTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["City"].ToString();
                    this.StateTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["State"].ToString();
                    this.ZipTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["Zip"].ToString();
                    this.OwnerShipTextBox.Text = this.centralOwnerDetailsLoad.Tables[1].Rows[0]["OwnerCode"].ToString();

                    //To Pass values to 35081 form
                    this.form35080Control.WorkItem.RootWorkItem.State["RollYear"] = this.RollYearTextBox.Text;
                    this.form35080Control.WorkItem.RootWorkItem.State["ParcelNumber"] = this.ParcelNumberTextBox.Text;
                    this.form35080Control.WorkItem.RootWorkItem.State["CompanyName"] = this.CompanyNameTextBox.Text;
                    this.form35080Control.WorkItem.RootWorkItem.State["CompanyNumber"] = this.CompanyNumberTextBox.Text;
                    this.form35080Control.WorkItem.RootWorkItem.State["BranchLine"] = this.BranchLineTextBox.Text;
                    this.form35080Control.WorkItem.RootWorkItem.State["OwnerID"] = this.ownerId.ToString();

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
                    this.ClearControls();
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                }

            }
            catch (Exception ex)
            {

            }
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
            //if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            //{
            //    if (!this.flagLoadOnProcess)
            //    {
            //        if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
            //        {
            //            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            //            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            //        }
            //    }
            //}
        }

        private void OwnersPictureBox_Click(object sender, EventArgs e)
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
        #region Edit button event
        ///<summary>
        /// Enables the edit button in master form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditButtonInMasterForm(object sender, EventArgs e)
        {
            try
            {
                // Enable Form Master Save/Cancel button
                this.EditEnabled();
                this.form35080Control.WorkItem.RootWorkItem.State["ParcelNumber"] = this.ParcelNumberTextBox.Text;
                this.form35080Control.WorkItem.RootWorkItem.State["CompanyName"] = this.CompanyNameTextBox.Text;
                this.form35080Control.WorkItem.RootWorkItem.State["CompanyNumber"] = this.CompanyNumberTextBox.Text;
                this.form35080Control.WorkItem.RootWorkItem.State["BranchLine"] = this.BranchLineTextBox.Text;
                try
                {
                    if (this.PropertyClassComboBox.SelectedIndex >=0)
                    {
                        this.form35080Control.WorkItem.RootWorkItem.State["PropertyClassID"] = this.PropertyClassComboBox.SelectedValue.ToString();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        #endregion
        /// <summary>
        /// Handles the Click event of the ParcelHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderPictureBox_Click(object sender, EventArgs e)
        { 
            try
            {
                this.OwnerCentralToolTip.SetToolTip(this.OwnersPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Owner Button Click
        /// <summary>
        /// Handles the Click event of the OwnerPictureButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerPictureButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form ownerIdForm = new Form();
                ownerIdForm = this.form35080Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form35080Control.WorkItem);
                if (ownerIdForm != null)
                {
                    DataSet ownerDetailDataSet = new DataSet();
                    if (ownerIdForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.SetEditRecord();
                        this.centailId = Convert.ToInt32(TerraScanCommon.GetValue(ownerIdForm, "MasterNameOwnerId"));
                        //this.linkLabelName= TerraScanCommon.GetValue(ownerIdForm, "CommandValue");
                        //this.OwnerLinkLabel.Text = this.linkLabelName;
                        ownerDetailDataSet = this.form35080Control.WorkItem.GetOwnerDetails(this.centailId);
                        if (ownerDetailDataSet.Tables[0].Rows.Count > 0)
                        {
                            this.ownerId = Convert.ToInt32(ownerDetailDataSet.Tables[0].Rows[0]["OwnerID"].ToString());
                            this.OwnerLinkLabel.Text = ownerDetailDataSet.Tables[0].Rows[0]["Name"].ToString();
                            this.Address1TextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["Address1"].ToString();
                            this.Address2TextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["Address2"].ToString();
                            this.CityTextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["City"].ToString();
                            this.ZipTextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["Zip"].ToString();
                            this.StateTextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["State"].ToString();
                            this.OwnerShipTextBox.Text = ownerDetailDataSet.Tables[0].Rows[0]["OwnerCode"].ToString();
                        }
                        this.form35080Control.WorkItem.RootWorkItem.State["OwnerID"] = this.ownerId;
                    }
                }
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
        #endregion

        #region Owner Link Click
        /// <summary>
        /// Handles the LinkClicked event of the OwnerLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInfo formInfo;
            formInfo = TerraScanCommon.GetFormInfo(91000);
            formInfo.optionalParameters = new object[1];
            formInfo.optionalParameters[0] = this.ownerId;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }
        #endregion

        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["RollYear"] = this.RollYearTextBox.Text;
            //if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            //{
            //    int yearval;
            //    int.TryParse(this.RollYearTextBox.Text.Trim(), out yearval);
            //    if (yearval <= 1899 || yearval >= 2080)
            //    {
            //        MessageBox.Show(SharedFunctions.GetResourceString("F2200Rollyear"), "TerraScan-InValid RollYear", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //this.RollYearTextBox.Text = "0";
            //        this.RollYearTextBox.Focus();
            //    }
            //    else
            //    {
            //        this.rollYear = this.RollYearTextBox.Text;
            //    }
            //}
        }

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

        private void ParcelNumberTextBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["ParcelNumber"] = this.ParcelNumberTextBox.Text;
        }

        private void CompanyNameTextBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["CompanyName"] = this.CompanyNameTextBox.Text;
        }

        private void CompanyNumberTextBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["CompanyNumber"] = this.CompanyNumberTextBox.Text;
        }

        private void BranchLineTextBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["BranchLine"] = this.BranchLineTextBox.Text;
        }

        private void PropertyClassComboBox_Leave(object sender, EventArgs e)
        {
            this.form35080Control.WorkItem.RootWorkItem.State["PropertyClassID"] = this.PropertyClassComboBox.SelectedValue.ToString();
        }

        private void OwnerLinkLabel_Leave(object sender, EventArgs e)
        {
           // this.form35080Control.WorkItem.RootWorkItem.State["OwnerID"] = this.ownerId;
        }
    }
}
