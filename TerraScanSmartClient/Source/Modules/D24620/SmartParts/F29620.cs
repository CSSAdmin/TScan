//--------------------------------------------------------------------------------------------
// <copyright file="F29620.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29620 Agland Application.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/08/2008        Malliga             Created
//***********************************************************************************************/

namespace D24620
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
    /// 29620
    /// </summary>
    [SmartPart]
    public partial class F29620 : BaseSmartPart
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
        private int eventId;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F36041
        /// </summary>
        private F29620Controller form29620Control;

        private F29620AglandApplicationData AglandApplicationDetailDataSet = new F29620AglandApplicationData();

        private int ownerid;
        #endregion

        #region Constructor
        public F29620()
        {
            InitializeComponent();
        }


         /// <summary>
        /// Initializes a new instance of the <see cref="T:F29620"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29620(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.eventId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.AglandpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AglandpictureBox.Height, this.AglandpictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form29620 control.
        /// </summary>
        /// <value>The form29620 control.</value>
        [CreateNew]
        public F29620Controller Form29620Control
        {
            get { return this.form29620Control as F29620Controller; }
            set { this.form29620Control = value; }
        }

        #endregion Property
        
        #region Event Subscription
        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                }
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
            this.ClearControls();
            this.GetAglandApplicationDetails(this.eventId);
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
                    int returnValue = this.form29620Control.WorkItem.F29620_SaveAglandApplicationDetails(this.eventId, ownerid, TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {

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
                 this.eventId = eventArgs.Data.SelectedKeyId;
                 this.GetAglandApplicationDetails(this.eventId);
                 this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                 this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }
        #endregion

        #region Form Load
        private void F29620_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.GetAglandApplicationDetails(this.eventId);
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ScheduleNumberTextBox.Focus();  
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

        #region Agland PictureBox Events
        private void AglandpictureBox_Click(object sender, EventArgs e)
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

        private void AglandpictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.AglandToolTip.SetToolTip(this.AglandpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Methods

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

        private void ClearControls()
        {
            this.ScheduleNumberTextBox.Text = string.Empty;
            this.ScheduleOwnerTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;   
        }


       private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            if (string.IsNullOrEmpty(this.ScheduleNumberTextBox.Text))
            {
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredFieldMissing");
                sliceValidationFields.FormNo = formNo;
                sliceValidationFields.RequiredFieldMissing = true;
                this.ScheduleNumberTextBox.Focus();
                return  sliceValidationFields;
            }

            return sliceValidationFields;
        }

        private void GetAglandApplicationDetails(int eventId)
        {
            this.AglandApplicationDetailDataSet = this.form29620Control.WorkItem.F29620_GetAglandApplicationDetails(eventId);
            if (this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows.Count > 0)
            {
              this.ScheduleNumberTextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.ScheduleNumberColumn].ToString();
              this.ScheduleOwnerTextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.ScheduleOwnerColumn].ToString();
              this.Address1TextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Address1Column].ToString();
              this.Address2TextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Address2Column].ToString();
              this.CityTextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.CityColumn].ToString();
              this.StateTextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.StateColumn].ToString();
              this.ZipTextBox.Text = this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.ZipColumn].ToString();
              int.TryParse( this.AglandApplicationDetailDataSet.f29620GetAglandapplication.Rows[0][this.AglandApplicationDetailDataSet.f29620GetAglandapplication.OwnerIDColumn].ToString(),out ownerid); 
            }
        }

        #endregion
                
    }
}
