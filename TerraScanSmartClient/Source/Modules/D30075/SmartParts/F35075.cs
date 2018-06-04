//--------------------------------------------------------------------------------------------
// <copyright file="F35075.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35075
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2/9/2008        Malliga             Created
//***********************************************************************************************/

namespace D30075
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

    /// <summary>
    /// 35075
    /// </summary>
    [SmartPart]
    public partial class F35075 : BaseSmartPart
    {
        #region Variables

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int? stateId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F36041
        /// </summary>
        private F35075Controller form35075Control;

        /// <summary>
        /// Unique ownerID 
        /// </summary>
        private int ownerId;

        ///<summary>
        ///PartiesOwnerDetailsData
        ///</summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        ///<summary>
        ///StateAssessesOwnerDetalDataSetData
        ///</summary>
        private F35075StateAssessedData StateAssessesOwnerDataSet = new F35075StateAssessedData();

        private int ownerid;
        private int rollyear;

        private bool flag = false;

        private bool delflag = false;
        #endregion

        #region Constructor

        public F35075()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35075"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35075(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.stateId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.OwnerpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnerpictureBox.Height, this.OwnerpictureBox.Width, tabText, red, green, blue);

        }

        #endregion

        #region Event Publication

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;
        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form29610 control.
        /// </summary>
        /// <value>The form29610 control.</value>
        [CreateNew]
        public F35075Controller Form35075Control
        {
            get { return this.form35075Control as F35075Controller; }
            set { this.form35075Control = value; }
        }

        #endregion Property

        #region Event Subscription
        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.ClearControl();
            this.GetStateAssessedOwnerDetails(this.stateId);
            ////this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows.Count > 0)
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
                    this.OwnerPictureButton.Select();
                    this.OwnerPictureButton.Focus();  
                }
            }
        }


        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.ClearControl();
                this.EnableControls(true);
                this.ControlLock(false);
                this.OwnerPictureButton.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                //this.OwnerpictureBox.Select();
                //this.OwnerpictureBox.Focus();
                this.ownerid = -1;
                this.RollYearTextBox.Visible = true;
                this.RolllinkLabel.Visible = false;
                this.form35075Control.WorkItem.RootWorkItem.State["RollYear"] = string.Empty;
                ////this.Focus();
                //if (this.OwnerPictureButton.CanFocus)
                //{
                //    this.OwnerPictureButton.Focus();
                //}
                ////this.CompanyNumberTextBox.Focus();
                //this.ActiveControl = this.OwnerPictureButton;
                //this.ActiveControl.Focus();
                this.OwnerPictureButton.Select();
                this.OwnerPictureButton.Focus();  
            }
            else
            {
                this.ClearControl();
                this.EnableControls(false);
                this.OwnerPictureButton.Enabled = false;
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.stateId = eventArgs.Data.SelectedKeyId;
                this.GetStateAssessedOwnerDetails(this.stateId);
                ////this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.RollYearTextBox.Visible = false;
                this.RolllinkLabel.Visible = true;
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
                    if (this.stateId == -99)
                    {
                        this.stateId = null;
                    }

                    string StateAssessedOwner = string.Empty;
                    F35075StateAssessedData.GetStateAssessedOwnerDataTable currentTable = new F35075StateAssessedData.GetStateAssessedOwnerDataTable();
                    F35075StateAssessedData.GetStateAssessedOwnerRow currenRow = currentTable.NewGetStateAssessedOwnerRow();
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.stateId = null;
                        currenRow.StateID = Convert.ToInt32(this.stateId);
                    }
                    else
                    {
                        currenRow.StateID = Convert.ToInt32(this.stateId);
                    }

                    currenRow.OwnerID = this.ownerid;
                    currenRow.OwnerName = this.OwnerLinkLabel.Text;
                    if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                    {
                        currenRow.RollYear = this.RollYearTextBox.Text;
                    }
                    else
                    {
                        currenRow.RollYear = string.Empty;
                    }
                    currenRow.Address1 = this.Address1TextBox.Text;
                    currenRow.Address2 = this.Address2TextBox.Text;
                    currenRow.city = this.CityTextBox.Text;
                    currenRow.State = this.StateTextBox.Text;
                    currenRow.Zip = this.ZipTextBox.Text;
                    currenRow.CompanyNumber = this.CompanyNumberTextBox.Text.Trim();
                    currentTable.Rows.Add(currenRow);
                    StateAssessedOwner = TerraScanCommon.GetXmlString(currentTable);

                    int returnValue = this.form35075Control.WorkItem.F35075_SaveStateAssessedOwner(this.stateId, StateAssessedOwner, TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));

                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.stateId);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.stateId);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.PermissionControlLock(!this.PermissionFiled.editPermission);
            }
            catch (Exception ex)
            {

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
                this.Cursor = Cursors.WaitCursor;
                int isdeleted = 0;
                isdeleted = this.form35075Control.WorkItem.F35075_DeleteStateAssessed(Convert.ToInt32(this.stateId), TerraScanCommon.UserId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                //SliceFormCloseAlert sliceFormCloseAlert;
                //sliceFormCloseAlert.FormNo = this.masterFormNo;
                //sliceFormCloseAlert.FlagFormClose = false;
                //this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;

            }
            flag = false;
            delflag = true;
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
                    this.stateId = eventArgs.Data.SelectedKeyId;
                    this.GetStateAssessedOwnerDetails(this.stateId);
                }
            }
        }

        #endregion

        #region Picture Box Events
        private void OwnerpictureBox_Click(object sender, EventArgs e)
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

        private void OwnerpictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.StateAssessedOwnerToolTip.SetToolTip(this.OwnerpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

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
                ownerIdForm = this.form35075Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9101, null, this.form35075Control.WorkItem);
                if (ownerIdForm != null)
                {
                    if (ownerIdForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(ownerIdForm, "MasterNameOwnerId"));
                        this.SetEditRecord();
                        this.ownerDetailDataSet = this.form35075Control.WorkItem.GetOwnerDetails(this.ownerId);
                        this.ownerid = Convert.ToInt32(this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.OwnerIDColumn].ToString());
                        this.OwnerLinkLabel.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.Address1TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address1Column].ToString();
                        this.Address2TextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.Address2Column].ToString();
                        this.CityTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.CityColumn].ToString();
                        this.StateTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.StateColumn].ToString();
                        this.ZipTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.ZipColumn].ToString();
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

        #region Methods
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
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.OwnerPanel.Enabled = enable;
            this.RollyearPanel.Enabled = enable;
            this.Address1Panel.Enabled = enable;
            this.Address2Panel.Enabled = enable;
            this.CityPanel.Enabled = enable;
            this.StatePanel.Enabled = enable;
            this.ZipPanel.Enabled = enable;
            this.CompanyNumberPanel.Enabled = enable;

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
        /// Clears the control.
        /// </summary>
        private void ClearControl()
        {
            this.OwnerLinkLabel.Text = string.Empty;
            this.RolllinkLabel.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            this.CompanyNumberTextBox.Text = string.Empty;
        }


        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            Control requiredControl;
            requiredControl = this.CheckRequiredFields();
            if (requiredControl != null)
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                requiredControl.Focus();
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns></returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.CompanyNumberTextBox.Text.Trim()))
            {
                requiredControll = this.CompanyNumberTextBox;
            }
            else if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                requiredControll = this.RollYearTextBox;
            }
            else if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                if (this.RollYearTextBox.Text.Trim().Length < 4)
                {
                    requiredControll = this.RollYearTextBox;
                }
            }
            return requiredControll;
        }

        /// <summary>
        /// Gets the state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        private void GetStateAssessedOwnerDetails(int? stateId)
        {

            this.StateAssessesOwnerDataSet = this.form35075Control.WorkItem.F35075_GetStateAssessedOwnerDetails(Convert.ToInt32(stateId));

            if (this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows.Count > 0)
            {
                this.ownerid = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.OwnerIDColumn].ToString());
                this.OwnerLinkLabel.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.OwnerNameColumn].ToString();
                if (!string.IsNullOrEmpty(this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.RollYearColumn].ToString()))
                {
                    this.rollyear = Convert.ToInt32(this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.RollYearColumn].ToString());
                    this.RolllinkLabel.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.RollYearColumn].ToString();
                    flag = false;
                    this.RollYearTextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.RollYearColumn].ToString();
                    TerraScanCommon.GetRollYear = this.RollYearTextBox.Text;
                }
                else
                {
                    this.RolllinkLabel.Text = string.Empty;
                }


                this.form35075Control.WorkItem.RootWorkItem.State["RollYear"] = this.RolllinkLabel.Text.Trim().ToString();
                this.Address1TextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Address1Column].ToString();
                this.Address2TextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Address2Column].ToString();
                this.CityTextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.cityColumn].ToString();
                this.StateTextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.StateColumn].ToString();
                this.ZipTextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.ZipColumn].ToString();
                this.CompanyNumberTextBox.Text = this.StateAssessesOwnerDataSet.GetStateAssessedOwner.Rows[0][this.StateAssessesOwnerDataSet.GetStateAssessedOwner.CompanyNumberColumn].ToString();

                flag = true;
                delflag = false;
                this.EnableControls(true);
                //flag = false;
                this.RollYearTextBox.Visible = false;
                this.RolllinkLabel.Visible = true;

                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                {
                    this.PermissionControlLock(false);
                }
                else
                {
                    this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
                //   delflag = true;
                //  flag = false;
            }
            else
            {
                delflag = true;
                flag = false;
                this.ClearControl();

                this.EnableControls(false);
            }
            //this.ActiveControl = this.OwnerpictureBox;
            //this.ActiveControl.Focus();
            ////this.OwnerpictureBox.Focus();
        }


        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F35075 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35075_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetStateAssessedOwnerDetails(this.stateId);
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
        #endregion

        #region TextBox Changed Events
        /// <summary>
        /// Handles the TextChanged event of the Address1TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Address1TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!delflag && flag)
            {
               this.SetEditRecord();
            }

            //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && !string.IsNullOrEmpty(this.CompanyNumberTextBox.Text))
            //{
            //    this.SetEditRecord();
            //}


            //if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            //{
            //    this.SetEditRecord();
            //}
        }

        /// <summary>
        /// Handles the KeyPress event of the CompanyNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void CompanyNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            delflag = false;
            flag = true;
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
            formInfo.optionalParameters[0] = this.ownerid;
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }
        #endregion

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!delflag && flag)
            {
              this.SetEditRecord();
            }
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
            /////this.EnableControls(controlLook);
            /////this.OwnerPictureButton.Enabled = !controlLook;
        }

        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            ////this.Address1TextBox.LockKeyPress = controlLook;
            ////this.Address2TextBox.LockKeyPress = controlLook;
            ////this.CityTextBox.LockKeyPress = controlLook;
            ////this.StateTextBox.LockKeyPress = controlLook;
            ////this.ZipTextBox.LockKeyPress = controlLook;
            this.CompanyNumberTextBox.LockKeyPress = controlLook;

            /// this.RollYearTextBox.Enabled = !controlLook;
            /// this.CompanyNumberTextBox.Enabled=!controlLook;
            this.OwnerPictureButton.Enabled = !controlLook;
        }

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
            this.form35075Control.WorkItem.RootWorkItem.State["RollYear"] = this.RollYearTextBox.Text;
        }

        ////Coding added for the issue 674
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompanyNumberTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            delflag = false;
            flag = true;
        }

        ////ends here
    }
}
