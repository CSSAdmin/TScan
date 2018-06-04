//----------------------------------------------------------------------------------
// <copyright file="F25050.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------
// 07 Feb 08	    Jaya Prakash .K     Created
// 03 April 09      Shanmuga Sundaram.A Modified to implement CO:#5928
// 02 Aug 11        Manoj P             Issue fixed to bugId: 13001.
//*********************************************************************************/

namespace D20050
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Infragistics.Win.UltraWinGrid;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Web.Services.Protocols;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using System.Globalization;
    using D24530;

    /// <summary>
    /// F205050 Class
    /// </summary>
    public partial class F25050 : BaseSmartPart
    {
        #region PrivateMemebers
        /// <summary>
        /// stores scheduleID
        /// </summary>
        private int scheduleID;

        /// <summary>
        /// stores DistrictID
        /// </summary>
        private int districtID;

        /// <summary>
        /// stores OwnerID
        /// </summary>
        private int ownerID;

        /// <summary>
        /// stores ParcelID
        /// </summary>
        private int parcelID;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        private F2200EditScheduleData editScheduleData = new F2200EditScheduleData();

        /// <summary>
        /// controller for the current view
        /// </summary>
        private F25050Controller form25050Control;

        private string stateConfigured = string.Empty;
        private bool isNotCustomState = false;

        /// <summary>
        /// stores the Schedule DataTable
        /// </summary>
        private F2200EditScheduleData.f2200ListScheduleDataTableDataTable getDetails = new F2200EditScheduleData.f2200ListScheduleDataTableDataTable();

        /// <summary>
        ///// Instance for 29531.
        ///// </summary>
        F29531 f29531;
        
        #endregion PrivateMemebers

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        #endregion Form Slice Variables

        public F25050 f25050
        {
            get { return f25050; }
            set { f25050 = value; }
        }
      

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public F25050()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36010"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F25050(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.scheduleID = keyID;
            this.SchduleHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.SchduleHeaderPictureBox.Height, this.SchduleHeaderPictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_KeyIdAlertSlice, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_KeyIdAlertSlice;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;
        
        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadOtherSlice, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadOtherSlice;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Occurs when [D9030_ F9030_ reload after delete].
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterDelete, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterDelete;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F8050Control
        /// </summary>
        [CreateNew]
        public F25050Controller Form25050Control
        {
            get { return this.form25050Control as F25050Controller; }
            set { this.form25050Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Loads the slice details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> e)
        {
            if (e.Data.MasterFormNo == this.masterFormNo)
            {
                this.scheduleID = e.Data.SelectedKeyId;
                this.GetScheduleDetails(this.scheduleID);
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

                    ////if (this.contractorManagementDatas.ListContractorManagement.Rows.Count > 0)
                    //// if (this.scheduleID > 0)
                    if (this.getDetails.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.Enablescheduleheader();
                    }
                    else
                    {
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                        this.Disablescheduleheader();
                        ////this.DisableDetails();
                        ////this.cancelNew = true;
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
        /// Called when [D9030_ F9033_ query engine close].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_KeyIdAlertSlice(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_KeyIdAlertSlice != null)
            {
                this.D9030_F9030_KeyIdAlertSlice(this, eventArgs);
            }
        }

        #region ProtectedMethods

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
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadOtherSlice(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadOtherSlice != null)
            {
                this.D9030_F9030_ReloadOtherSlice(this, eventArgs);
            }
        }

        ///// <summary>
        ///// Raises the <see cref="E:D9030_F9030_ReloadAfterDelete"/> event.
        ///// </summary>
        ///// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        //protected virtual void OnD9030_F9030_ReloadAfterDelete(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        //{
        //    if (this.D9030_F9030_ReloadAfterDelete != null)
        //    {
        //        this.D9030_F9030_ReloadAfterDelete(this, eventArgs);
        //    }
        //}

        #endregion

        #endregion Event Subscription

        #region Events

        /// <summary>
        /// Handles the Load event of the F25050 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void F25050_Load(object sender, EventArgs e)
        {
            try
            {
                ////ScheduleNumberPanel.BackColor = Color.FromArgb(;
                this.FlagSliceForm = true;
                this.GetScheduleDetails(this.scheduleID);
                this.ScheduleNumberLinkLabel.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SchduleHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void SchduleHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the SchduleHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void SchduleHeaderPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ScheduleFormSliceToolTip.SetToolTip(this.SchduleHeaderPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the LinkClicked event of the ScheduleNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ScheduleNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                int openPermission = 0;
                if (this.scheduleID > 0)
                {
                    ////Coding added for the issue 528(open permission has been implemeted)
                    DataTable formPermissionDataTable = TerraScanCommon.FormPermissionsDataSet.Tables[0];
                    DataRow[] permissionDataRow = formPermissionDataTable.Select("Form=2200");
                    DataSet formPermissionDataSet = new DataSet();
                    formPermissionDataSet.Merge(permissionDataRow); 
                    if (permissionDataRow.Length > 0)
                    {
                        int.TryParse(formPermissionDataSet.Tables[0].Rows[0][1].ToString(), out openPermission);
                    }
                    ////Coding ends here
                if(openPermission.Equals(1))
                {
                    object[] optionalParameter;
                    optionalParameter = new object[2];
                    optionalParameter[0] = this.masterFormNo;
                    optionalParameter[1] = this.scheduleID;

                    Form scheduleForm = new Form();
                    scheduleForm = this.form25050Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2200, optionalParameter, this.form25050Control.WorkItem);
                    if (scheduleForm != null)
                    {
                        //scheduleForm.ShowDialog();
                        if (scheduleForm.ShowDialog() == DialogResult.OK)
                        {
                            int formScheduleID;
                            int.TryParse(TerraScanCommon.GetValue(scheduleForm, SharedFunctions.GetResourceString("F25050ScheduleProperty")).ToString(), out formScheduleID);
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = formScheduleID;

                            int isRecordDeleted;
                            int.TryParse(TerraScanCommon.GetValue(scheduleForm, SharedFunctions.GetResourceString("F25050DeleteProperty")).ToString(), out isRecordDeleted);

                            if (isRecordDeleted > 0)
                            {
                                this.D9030_F9030_ReloadAfterDelete(this, new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            }
                            else
                            {
                                this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                                ////to refresh the master form with the return keyid
                                //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                                this.OnD9030_F9030_ReloadOtherSlice(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                                this.GetScheduleDetails(formScheduleID);
                                this.scheduleID = formScheduleID;
                            }

                            //int candelete;
                            //int.TryParse(TerraScanCommon.GetValue(scheduleForm, SharedFunctions.GetResourceString("F25050DeleteProperty")).ToString(), out candelete);

                            //if (candelete > 0)
                            //{
                            //    ////when deleted
                            //    ////SliceFormCloseAlert sliceFormCloseAlert;
                            //    ////sliceFormCloseAlert.FormNo = this.masterFormNo;
                            //    ////sliceFormCloseAlert.FlagFormClose = false;
                            //    ////this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                            //    this.OnD9030_F9030_KeyIdAlertSlice(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.masterFormNo));
                            //}
                            //else
                            //{
                            //    this.GetScheduleDetails(formScheduleID);
                            //    this.scheduleID = formScheduleID;
                            //}
                        }
                }
                }
            }

                ////khaja added code to fix Bug#5185
                this.ActiveControl = this.ScheduleNumberLinkLabel;
                this.ActiveControl.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the EventslinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void EventslinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(24001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.scheduleID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the PrimaryOwnerlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void PrimaryOwnerlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.ownerID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the LinkClicked event of the ParcelReferencelinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ParcelReferencelinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(30000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.parcelID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the LinkClicked event of the DistrictlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void DistrictlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
             
                formInfo = TerraScanCommon.GetFormInfo(11002);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.districtID;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Leave event of the ScheduleNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleNumberLinkLabel_Leave(object sender, EventArgs e)
        {
            ////PrimaryOwnerlinkLabel.Focus();
        }

        /// <summary>
        /// Handles the Leave event of the PrimaryOwnerlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrimaryOwnerlinkLabel_Leave(object sender, EventArgs e)
        {
            ////ParcelReferencelinkLabel.Focus();
        }

        /// <summary>
        /// Handles the Leave event of the ParcelReferencelinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelReferencelinkLabel_Leave(object sender, EventArgs e)
        {
            ////DistrictlinkLabel.Focus();
        }

        #endregion Events

        #region PrivateMethods

        /// <summary>
        /// Disablescheduleheaders this instance.
        /// </summary>
        private void Disablescheduleheader()
        {
            this.ScheduleNumberPanel.Enabled = false;
            ScheduleNumberLinkLabel.Enabled = false;
            RollYearPanel.Enabled = false;
            RollYearTextBox.Enabled = false;
            SourcePanel.Enabled = false;
            this.HeadOfHouseholdpanel.Enabled = false;
            this.HeadofHouseholdTextBox.Enabled = false;
            this.Exemptpanel.Enabled = false;
            this.ExemptTextBox.Enabled = false;
            this.ScheduleDORpanel.Enabled = false;
            this.ScheduleDORTextBox.Enabled = false;
            this.OwnerPhonePanel.Enabled = false;
            this.OwnerPhoneTextBox.Enabled = false;
            this.OwnerMailPanel.Enabled = false;
            this.OwnerMailTextBox.Enabled = false;
            this.FarmPanel.Enabled = false;
            this.FarmTextBox.Enabled = false;
            this.MID1panel.Enabled = false;
            this.MID1TextBox.Enabled = false;
            this.MID2panel.Enabled = false;
            this.MID2TextBox.Enabled = false;
            this.MID3panel.Enabled = false;
            this.MID3TextBox.Enabled = false;
            this.MID4panel.Enabled = false;
            this.MID4TextBox.Enabled = false;
            this.MID5panel.Enabled = false;
            this.MID5TextBox.Enabled = false;
            SourceTextBox.Enabled = false;
            ReviewPanel.Enabled = false;
            ReviewTextBox.Enabled = false;
            PropertyTypePanel.Enabled = false;
            PropertyTypeTextBox.Enabled = false;
            this.AssessmentTypePanel.Enabled = false;
            this.AssessmentTypeTextBox.Enabled = false;
            this.ScheduleTypePanel.Enabled = false;
            this.ScheduleTypeTextBox.Enabled = false;
            EventsPanel.Enabled = false;
            EventslinkLabel.Enabled = false;
            EventslinkLabel.Visible = false;
            DescriptionPanel.Enabled = false;
            DescriptionTextBox.Enabled = false;
            PrimaryOwnerPanel.Enabled = false;
            PrimaryOwnerlinkLabel.Enabled = false;
            StreetAddressPanel.Enabled = false;
            StreetAddressTextBox.Enabled = false;
            ParcelReferencePanel.Enabled = false;
            ParcelReferencelinkLabel.Enabled = false;
            RealPropertyDORPanel.Enabled = false;
            RealPropertyDORTextBox.Enabled = false;
            NAICSPanel.Enabled = false;
            NAICSTextBox.Enabled = false;
            LegalPanel.Enabled = false;
            LegalTextBox.Enabled = false;
            MapNumberPanel.Enabled = false;
            RealPropertyMapNumberTextBox.Enabled = false;
            PenaltyAmountPanel.Enabled = false;
            PenaltyAmountTextBox.Enabled = false;
            PenaltyPercentPanel.Enabled = false;
            PenaltyPercentTextBox.Enabled = false;
            AssessedValuePanel.Enabled = false;
            AssessedValueTextBox.Enabled = false;
            TaxableValuepanel.Enabled = false;
            TaxableValueTextBox.Enabled = false;
            DistrictPanel.Enabled = false;
            DistrictlinkLabel.Enabled = false;
            BusinessNamePanel.Enabled = false;
            BusinessNameTextBox.Enabled = false;
            //SchduleHeaderPictureBox.Enabled = false;
            FilingDatePanel.Enabled = false;
            this.NewConstructionPanel.Enabled = false;
            this.NewConstrctionTextBox.Enabled = false;
        }

        /// <summary>
        /// Enablescheduleheaders this instance.
        /// </summary>
        private void Enablescheduleheader()
        {
            this.ScheduleNumberPanel.Enabled = true;
            ScheduleNumberLinkLabel.Enabled = true;
            RollYearPanel.Enabled = true;
            RollYearTextBox.Enabled = true;
            SourcePanel.Enabled = true;
            SourceTextBox.Enabled = true;
            this.HeadOfHouseholdpanel.Enabled = true;
            this.HeadofHouseholdTextBox.Enabled = true;
            ReviewPanel.Enabled = true;
            ReviewTextBox.Enabled = true;
            this.AssessmentTypePanel.Enabled = true;
            this.Exemptpanel.Enabled = true;
            this.ExemptTextBox.Enabled = true;
            this.ScheduleDORpanel.Enabled = true;
            this.ScheduleDORTextBox.Enabled = true;
            this.OwnerPhonePanel.Enabled = true;
            this.OwnerPhoneTextBox.Enabled = true;
            this.OwnerMailPanel.Enabled = true;
            this.OwnerMailTextBox.Enabled = true;
            this.FarmPanel.Enabled = true;
            this.FarmTextBox.Enabled = true;
            this.MID1panel.Enabled = true;
            this.MID1TextBox.Enabled = true;
            this.MID2panel.Enabled = true;
            this.MID2TextBox.Enabled = true;
            this.MID3panel.Enabled = true;
            this.MID3TextBox.Enabled = true;
            this.MID4panel.Enabled = true;
            this.MID4TextBox.Enabled = true;
            this.MID5panel.Enabled = true;
            this.MID5TextBox.Enabled = true;
            PropertyTypePanel.Enabled = true;
            PropertyTypeTextBox.Enabled = true;
            this.PropertyTypePanel.Enabled = true;
            this.PropertyTypeTextBox.Enabled = true;
            this.ScheduleTypePanel.Enabled = true;
            this.ScheduleTypeTextBox.Enabled = true;
            EventsPanel.Enabled = true;
            EventslinkLabel.Visible = true;
            EventslinkLabel.Enabled = true;
            DescriptionPanel.Enabled = true;
            DescriptionTextBox.Enabled = true;
            PrimaryOwnerPanel.Enabled = true;
            PrimaryOwnerlinkLabel.Enabled = true;
            StreetAddressPanel.Enabled = true;
            StreetAddressTextBox.Enabled = true;
            ParcelReferencePanel.Enabled = true;
            ParcelReferencelinkLabel.Enabled = true;
            RealPropertyDORPanel.Enabled = true;
            RealPropertyDORTextBox.Enabled = true;
            NAICSPanel.Enabled = true;
            NAICSTextBox.Enabled = true;
            LegalPanel.Enabled = true;
            LegalTextBox.Enabled = true;
            MapNumberPanel.Enabled = true;
            RealPropertyMapNumberTextBox.Enabled = true;
            PenaltyAmountPanel.Enabled = true;
            PenaltyAmountTextBox.Enabled = true;
            PenaltyPercentPanel.Enabled = true;
            PenaltyPercentTextBox.Enabled = true;
            AssessedValuePanel.Enabled = true;
            AssessedValueTextBox.Enabled = true;
            TaxableValuepanel.Enabled = true;
            TaxableValueTextBox.Enabled = true;
            DistrictPanel.Enabled = true;
            DistrictlinkLabel.Enabled = true;
            BusinessNamePanel.Enabled = true;
            BusinessNameTextBox.Enabled = true;
            //SchduleHeaderPictureBox.Enabled = true;
            FilingDatePanel.Enabled = true;
            this.NewConstructionPanel.Enabled = true;
            this.NewConstrctionTextBox.Enabled = true;
        }

        /// <summary>
        /// Gets the schedule details.
        /// </summary>
        /// <param name="scheduleValue">The schedule value.</param>
        private void GetScheduleDetails(int scheduleValue)
        {
            F2200EditScheduleData getScheduleDataset = new F2200EditScheduleData();
            getScheduleDataset = this.form25050Control.WorkItem.F2200_ListEditScheduleDetails(scheduleValue);
            if (getScheduleDataset.f25050_pcget_Configuredstate.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(getScheduleDataset.f25050_pcget_Configuredstate.Rows[0][0].ToString()))
                {

                    this.stateConfigured = getScheduleDataset.f25050_pcget_Configuredstate.Rows[0][0].ToString();
                }
            }

            this.getDetails = getScheduleDataset.f2200ListScheduleDataTable;

            int lockSchedule = getDetails[0].LockScheduleBy;

            if (lockSchedule == null || lockSchedule == 0)
            {
                this.GreenpictureBox.Visible = true;
                this.RedPictureBox.Visible = false;
            }
            else
            {
                this.GreenpictureBox.Visible = false;
                this.RedPictureBox.Visible = true;
            }


            if (this.getDetails.Rows.Count > 0)
            {
                this.SetScheduleDetails(this.GetScheduleRow(0));
                this.Enablescheduleheader();
            }
            else
            {
                ////Coding added for the issue 4497  by maliga on 29/3/2009
                this.ScheduleNumberLinkLabel.Text = string.Empty;
                this.RollYearTextBox.Text = string.Empty;
                this.SourceTextBox.Text = string.Empty;
                this.ReviewTextBox.Text = string.Empty;
                this.HeadofHouseholdTextBox.Text = SharedFunctions.GetResourceString("No");
                this.PropertyTypeTextBox.Text = string.Empty;
                this.ScheduleTypeTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
                this.PrimaryOwnerlinkLabel.Text = string.Empty;
                this.StreetAddressTextBox.Text = string.Empty;
                this.ParcelReferencelinkLabel.Text = string.Empty;
                this.RealPropertyDORTextBox.Text = string.Empty;
                this.NAICSTextBox.Text = string.Empty;
                this.FilingDateTextBox.Text = string.Empty;
                this.LegalTextBox.Text = string.Empty;
                this.RealPropertyMapNumberTextBox.Text = string.Empty;
                this.PenaltyAmountTextBox.Text = string.Empty;
                this.PenaltyPercentTextBox.Text = string.Empty;
                this.AssessedValueTextBox.Text = string.Empty;
                this.TaxableValueTextBox.Text = string.Empty;
                this.DistrictlinkLabel.Text = string.Empty;
                this.BusinessNameTextBox.Text = string.Empty;
                this.AssessmentTypeTextBox.Text = string.Empty;
                this.NewConstrctionTextBox.Text = string.Empty;
                this.MID1TextBox.Text = string.Empty;
                this.MID2TextBox.Text = string.Empty;
                this.MID3TextBox.Text = string.Empty;
                this.MID4TextBox.Text = string.Empty;
                this.MID5TextBox.Text = string.Empty;
                this.ScheduleDORTextBox.Text = string.Empty;
                this.OwnerPhoneTextBox.Text = string.Empty;
                this.OwnerMailTextBox.Text = string.Empty;
                this.ExemptTextBox.Text = SharedFunctions.GetResourceString("No");
                this.FarmTextBox.Text = SharedFunctions.GetResourceString("No");
                this.Disablescheduleheader();
                ////End here for 4497
            }
        }

        /// <summary>
        /// Sets the schedule details.
        /// </summary>
        /// <param name="selectedRow">The selected row.</param>
        private void SetScheduleDetails(F2200EditScheduleData.f2200ListScheduleDataTableRow selectedRow)
        {
            try
            {
                if (stateConfigured.ToLower().ToString().Equals("ne"))
                {
                    this.ScheduleLockPanel.Visible = true;
                    this.PersonalPropertyDescriptionPanel.Visible = true;
                    this.PersonalPropertyCodePanel.Visible = true;
                    this.BottomPanel.Location = new System.Drawing.Point(this.BottomPanel.Location.X, 78);
                    this.SchduleHeaderPictureBox.Height = 468;
                    this.Height = 470;

                    this.CustomViewPanel.BringToFront();
                    this.ExemptYearTextBox.Enabled = false;
                    this.ExemptAmountTextBox.Enabled = false;
                    if (selectedRow.IsFarmExempt)
                    {
                        this.FarmExemptTextBox.Text = "Yes";
                        this.FarmExemptTextBox.Text = SharedFunctions.GetResourceString("Yes");
                    }
                    else
                    {
                        this.FarmExemptTextBox.Text = "No";
                    }
                    if (!string.IsNullOrEmpty(selectedRow.FarmExemptYear.ToString()))
                    {
                        this.ExemptYearTextBox.Text = selectedRow.FarmExemptYear.ToString();
                    }
                    else
                    {
                        this.ExemptYearTextBox.Text = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(selectedRow.FarmExemptAmount.ToString()))
                    {
                        var strTemp = selectedRow.FarmExemptAmount.ToString();
                        double strNumb = Convert.ToDouble(strTemp);
                        var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                        this.ExemptAmountTextBox.Text = strResult;
                    }
                    else
                    {
                        this.ExemptAmountTextBox.Text = string.Empty;
                    }


                    if (!selectedRow.IsIs259ExemptNull())
                    {
                        if (selectedRow.Is259Exempt == 0)
                        {
                            this.Ex259Textbox.Text = "No";
                        }
                        else
                        {
                            this.Ex259Textbox.Text = "Yes";
                        }
                    }
                    else 
                    {
                        this.Ex259Textbox.Text = string.Empty;
                    }

                    if (!selectedRow.IsExempt259AmountNull())
                    {
                        var strTemp = selectedRow.Exempt259Amount.ToString();
                        double strNumb = Convert.ToDouble(strTemp);
                        var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                        this.ExAmt259TextBox.Text = strResult;
                    }
                    else
                    {
                        this.ExAmt259TextBox.Text = string.Empty;
                    }
                }

                else
                {
                    this.Exemptpanel.Visible = true;
                    this.HeadOfHouseholdpanel.Visible = true;
                    this.FarmPanel.Visible = true;
                    this.CustomViewPanel.Visible = false;
                    this.ScheduleLockPanel.Visible = true;
                    this.PersonalPropertyDescriptionPanel.Visible = false;
                    this.PersonalPropertyCodePanel.Visible = false;
                    this.AssessmentTypePanel.Visible = true;
                    this.ScheduleTypePanel.Visible = true;
                    this.SourcePanel.Visible = true;
                    this.PropertyTypePanel.Visible = true;
                    this.BottomPanel.BringToFront();
                    this.BottomPanel.Location = new System.Drawing.Point(this.BottomPanel.Location.X, 39);
                    this.SchduleHeaderPictureBox.Height = 429;
                    this.Height = 430;
                    this.CustomViewPanel.SendToBack();
                    this.AssessmentTypePanel.BringToFront();
                    this.ScheduleTypePanel.BringToFront();
                    this.SourcePanel.BringToFront();
                    this.PropertyTypePanel.BringToFront();
                }


                if (!selectedRow.IsParcelIDNull())
                {
                    int.TryParse(selectedRow.ParcelID.ToString(), out this.parcelID);
                }

                if (!selectedRow.IsDistrictIDNull())
                {
                    int.TryParse(selectedRow.DistrictID.ToString(), out this.districtID);
                }

                if (!selectedRow.IsOwnerIDNull())
                {
                    int.TryParse(selectedRow.OwnerID.ToString(), out this.ownerID);
                }

                if (!string.IsNullOrEmpty(selectedRow.ScheduleNumber.ToString()))
                {
                    //Issue fixed to bugId: 13001 Manoj P. 
                    string ScheduleNumbers = string.Empty;
                    ScheduleNumbers = selectedRow.ScheduleNumber;
                    if (ScheduleNumbers.Contains("&"))
                    {
                        ScheduleNumbers = ScheduleNumbers.Replace("&", "&&");
                    }
                    this.ScheduleNumberLinkLabel.Text = ScheduleNumbers;
                }
                else
                {
                    this.ScheduleNumberLinkLabel.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(selectedRow.RollYear.ToString()))
                {
                    this.RollYearTextBox.Text = selectedRow.RollYear.ToString();
                    F29531.rollYearval = this.RollYearTextBox.Text.ToString();
                    F29531.RollYear = Convert.ToInt32(this.RollYearTextBox.Text.ToString());
                }
                else
                {
                    this.RollYearTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsFilingTypeNull())
                {
                    this.SourceTextBox.Text = selectedRow.FilingType;
                }
                else
                {
                    this.SourceTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(selectedRow.IsReview.ToString()))
                {
                    this.ReviewTextBox.Text = selectedRow.IsReview;
                }
                else
                {
                    this.ReviewTextBox.Text = string.Empty;
                }

                if (selectedRow.IsHeadOfHousehold.Equals(1))
                {
                    this.HeadofHouseholdTextBox.Text = SharedFunctions.GetResourceString("Yes");
                }
                else
                {
                    this.HeadofHouseholdTextBox.Text = SharedFunctions.GetResourceString("No");
                }

                //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
                if (selectedRow.IsExempt.Equals(1))
                {
                    this.ExemptTextBox.Text = SharedFunctions.GetResourceString("Yes");
                }
                else
                {
                    this.ExemptTextBox.Text = SharedFunctions.GetResourceString("No");
                }

                if (!string.IsNullOrEmpty(selectedRow.ScheduleNumber.ToString()))
                {
                    //Issue fixed to bugId: 13001 Manoj P.
                    string ScheduleNumbers = string.Empty;
                    ScheduleNumbers = selectedRow.ScheduleNumber;
                    if (ScheduleNumbers.Contains("&"))
                    {
                        ScheduleNumbers = ScheduleNumbers.Replace("&", "&&");
                    }
                    this.ScheduleNumberLinkLabel.Text = ScheduleNumbers;
                    //this.ScheduleNumberLinkLabel.Text = selectedRow.ScheduleNumber;
                }
                else
                {
                    this.ScheduleNumberLinkLabel.Text = string.Empty;
                }

                //// Added for the CO:#5928 by Shanmuga Sundaram.A on April'02,2009.
                if (!selectedRow.IsMIDLabel1Null())
                {
                    this.MID1label.Text = selectedRow.MIDLabel1;
                }
                else
                {
                    this.MID1label.Text = string.Empty;
                }

                if (!selectedRow.IsMID1Null())
                {
                    this.MID1TextBox.Text = selectedRow.MID1;
                }
                else
                {
                    this.MID1TextBox.Text = string.Empty;
                }

                if (!selectedRow.IsMIDLabel2Null())
                {
                    this.MID2label.Text = selectedRow.MIDLabel2;
                }
                else
                {
                    this.MID2label.Text = string.Empty;
                }

                if (!selectedRow.IsMID2Null())
                {
                    this.MID2TextBox.Text = selectedRow.MID2;
                }
                else
                {
                    this.MID2TextBox.Text = string.Empty;
                }

                if (!selectedRow.IsMIDLabel3Null())
                {
                    this.MID3label.Text = selectedRow.MIDLabel3;
                }
                else
                {
                    this.MID3label.Text = string.Empty;
                }

                if (!selectedRow.IsMID3Null())
                {
                    this.MID3TextBox.Text = selectedRow.MID3;
                }
                else
                {
                    this.MID3TextBox.Text = string.Empty;
                }

                if (!selectedRow.IsMIDLabel4Null())
                {
                    this.MID4label.Text = selectedRow.MIDLabel4;
                }
                else
                {
                    this.MID4label.Text = string.Empty;
                }

                if (!selectedRow.IsMID4Null())
                {
                    this.MID4TextBox.Text = selectedRow.MID4;
                }
                else
                {
                    this.MID4TextBox.Text = string.Empty;
                }

                if (!selectedRow.IsMIDLabel5Null())
                {
                    this.MID5label.Text = selectedRow.MIDLabel5;
                }
                else
                {
                    this.MID5label.Text = string.Empty;
                }

                if (!selectedRow.IsMID5Null())
                {
                    this.MID5TextBox.Text = selectedRow.MID5;
                }
                else
                {
                    this.MID5TextBox.Text = string.Empty;
                }

                if (!selectedRow.IsScheduleStateCodeNull())
                {
                    this.ScheduleDORTextBox.Text = selectedRow.ScheduleStateCode;
                }
                else
                {
                    this.ScheduleDORTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPropertyTypeNull())
                {
                    this.PropertyTypeTextBox.Text = selectedRow.PropertyType;
                }
                else
                {
                    this.PropertyTypeTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsScheduleTypeNull())
                {
                    this.ScheduleTypeTextBox.Text = selectedRow.ScheduleType;
                    if (!selectedRow.ScheduleType.Equals("Active"))
                    {
                        this.ScheduleTypeTextBox.ForeColor = Color.Red;
                    }
                    else
                    {
                        this.ScheduleTypeTextBox.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    this.ScheduleTypeTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsDescriptionNull())
                {
                    this.DescriptionTextBox.Text = selectedRow.Description;
                }
                else
                {
                    this.DescriptionTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPrimaryOwnerNull())
                {
                    //Issue fixed to bugId: 13001 Manoj P.
                    string PrimaryOwners = string.Empty;
                    PrimaryOwners = selectedRow.PrimaryOwner;
                    if (PrimaryOwners.Contains("&"))
                    {
                        PrimaryOwners = PrimaryOwners.Replace("&", "&&");
                    }
                    this.PrimaryOwnerlinkLabel.Text = PrimaryOwners;

                }
                else
                {
                    this.PrimaryOwnerlinkLabel.Text = string.Empty;
                }

                if (!selectedRow.IsStreetAddressNull())
                {
                    this.StreetAddressTextBox.Text = selectedRow.StreetAddress;
                }
                else
                {
                    this.StreetAddressTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsParcelNumberNull())
                {
                    //Issue fixed to bugId: 13001 Manoj P.
                    string ParcelNumbers = string.Empty;
                    ParcelNumbers = selectedRow.ParcelNumber;
                    if (ParcelNumbers.Contains("&"))
                    {
                        ParcelNumbers = ParcelNumbers.Replace("&", "&&");
                    }
                    this.ParcelReferencelinkLabel.Text = ParcelNumbers;
                }
                else
                {
                    this.ParcelReferencelinkLabel.Text = string.Empty;
                }

                if (!selectedRow.IsParcelStateCodeNull())
                {
                    this.RealPropertyDORTextBox.Text = selectedRow.ParcelStateCode;
                }
                else
                {
                    this.RealPropertyDORTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsNAICSNull())
                {
                    this.NAICSTextBox.Text = selectedRow.NAICS;
                }
                else
                {
                    this.NAICSTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsFilingDateNull())
                {
                    this.FilingDateTextBox.Text = selectedRow.FilingDate.ToString();
                }
                else
                {
                    this.FilingDateTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsLegalNull())
                {
                    this.LegalTextBox.Text = selectedRow.Legal;
                }
                else
                {
                    this.LegalTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsMapNumberNull())
                {
                    this.RealPropertyMapNumberTextBox.Text = selectedRow.MapNumber;
                }
                else
                {
                    this.RealPropertyMapNumberTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPenaltyAmountNull())
                {
                    this.PenaltyAmountTextBox.Text = selectedRow.PenaltyAmount.ToString();
                }
                else
                {
                    this.PenaltyAmountTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPenaltyPercentNull())
                {
                    #region Coding Added for the issue 2054
                    this.PenaltyPercentTextBox.TextCustomFormat = "";
                    this.PenaltyPercentTextBox.Text = selectedRow.PenaltyPercent.ToString();
                    this.customFormat();
                    this.PenaltyPercentTextBox.Text = this.PenaltyPercentTextBox.DecimalTextBoxValue.ToString();
                    #endregion
                }
                else
                {
                    this.PenaltyPercentTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsO1ValueNull())
                {
                    this.AssessedValueTextBox.Text = selectedRow.O1Value.ToString();
                }
                else
                {
                    this.AssessedValueTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsO2ValueNull())
                {
                    this.TaxableValueTextBox.Text = selectedRow.O2Value.ToString();
                }
                else
                {
                    this.TaxableValueTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsDistrictNull())
                {
                    //Issue fixed to bugId: 13001 Manoj P.
                    string Districts = string.Empty;
                    Districts = selectedRow.District;
                    if (Districts.Contains("&"))
                    {
                        Districts = Districts.Replace("&", "&&");
                    }
                    this.DistrictlinkLabel.Text = Districts;
                }
                else
                {
                    this.DistrictlinkLabel.Text = string.Empty;
                }

                if (!selectedRow.IsBuisnessNameNull())
                {
                    this.BusinessNameTextBox.Text = selectedRow.BuisnessName;
                }
                else
                {
                    this.BusinessNameTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsAssessmentTypeNull())
                {
                    this.AssessmentTypeTextBox.Text = selectedRow.AssessmentType;
                }
                else
                {
                    this.AssessmentTypeTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsNewConstructionNull())
                {
                    this.NewConstrctionTextBox.Text = selectedRow.NewConstruction.ToString();
                }
                else
                {
                    this.NewConstrctionTextBox.Text = string.Empty;
                }

                if (selectedRow.IsFarm.Equals(1))
                {
                    this.FarmTextBox.Text = SharedFunctions.GetResourceString("Yes");
                }
                else
                {
                    this.FarmTextBox.Text = SharedFunctions.GetResourceString("No");
                }

                if (!selectedRow.IsPhoneNull())
                {
                    this.OwnerPhoneTextBox.Text = selectedRow.Phone;
                }
                else
                {
                    this.OwnerPhoneTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsEmailNull())
                {
                    this.OwnerMailTextBox.Text = selectedRow.Email;
                }
                else
                {
                    this.OwnerMailTextBox.Text = string.Empty;
                }


                if (selectedRow.IsFarmExempt)
                {
                    this.FarmExemptTextBox.Text = SharedFunctions.GetResourceString("Yes");
                }
                else
                {
                    this.FarmExemptTextBox.Text = SharedFunctions.GetResourceString("No");
                }

                if (!selectedRow.IsFarmExemptAmountNull())
                {
                    double strNumb = Convert.ToDouble(selectedRow.FarmExemptAmount.ToString());
                    var strResult = strNumb.ToString("#,##0.####", CultureInfo.InvariantCulture);
                    this.ExemptAmountTextBox.Text = strResult;
                }
                else
                {
                    this.ExemptAmountTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsFarmExemptYearNull())
                {
                    this.ExemptYearTextBox.Text = selectedRow.FarmExemptYear.ToString();
                }
                else
                {
                    this.ExemptYearTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPersonalPropertyCodeNull())
                {
                    this.PersonalPropertyCodeTextBox.Text = selectedRow.PersonalPropertyCode.ToString();
                }
                else
                {
                    this.PersonalPropertyCodeTextBox.Text = string.Empty;
                }

                if (!selectedRow.IsPersonalPropertyCodeDescriptionNull())
                {
                    this.PersonalPropertyDescriptionTextBox.Text = selectedRow.PersonalPropertyCodeDescription.ToString();
                }
                else
                {
                    this.PersonalPropertyDescriptionTextBox.Text = string.Empty;
                }
            }
            catch(Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the schedule row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        /// <returns>DataRow</returns>
        private F2200EditScheduleData.f2200ListScheduleDataTableRow GetScheduleRow(int rowIndex)
        {
            return (F2200EditScheduleData.f2200ListScheduleDataTableRow)this.getDetails.Rows[rowIndex];
        }

        #endregion PrivateMethords

        #region Coding Added for the issue 2054
        /// <summary>
        /// Custom Format For Penalty Percent Field
        /// </summary>
        private void customFormat()
        {
            ////this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.000000%";
            string ppercent = this.PenaltyPercentTextBox.DecimalTextBoxValue.ToString();
            int leng = ppercent.Length;
            int decPos = ppercent.IndexOf(".");
            if (decPos != -1)
            {
                if (leng - (decPos + 1) > 0)
                {
                    ppercent = this.PenaltyPercentTextBox.Text.ToString().Substring(decPos + 1, leng - (decPos + 1)).Trim();
                }
            }
            int zerocount = 0;
            int nonzerocount = 0;
            for (int i = ppercent.Length; i >= 1; i--)
            {
                string arrChar = Convert.ToString(ppercent[i - 1]);
                if (arrChar.Equals("0"))
                {
                    if (nonzerocount >= 1)
                    {
                        nonzerocount++;
                    }
                    else
                    {
                        zerocount++;
                    }
                }
                else
                {
                    nonzerocount++;
                }
            }

            if (decPos != -1)
            {
                if (nonzerocount.Equals(0) || nonzerocount.Equals(1))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0%";
                }
                else if (nonzerocount.Equals(2))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.00%";
                }
                else if (nonzerocount.Equals(3))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.000%";
                }
                else if (nonzerocount.Equals(4))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0000%";
                }
                else if (nonzerocount.Equals(5))
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.00000%";
                }
                else
                {
                    this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.000000%";
                }
            }
            else
            {
                this.PenaltyPercentTextBox.TextCustomFormat = "#,##0.0%";
            }
        }

        /// <summary>
        /// Handles the Leave event of the PenaltyPercentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PenaltyPercentTextBox_Leave(object sender, EventArgs e)
        {
            if (this.PenaltyPercentTextBox.DecimalTextBoxValue != null)
            {
                this.customFormat();
            }
        }
        #endregion

        private void GreenpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form scheduleForm = new Form();
                object[] optionalParameter = new object[] { this.scheduleID, "2005" };
                scheduleForm = this.form25050Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2005, optionalParameter, this.form25050Control.WorkItem);
                if (scheduleForm != null)
                {
                    if (scheduleForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.RedPictureBox.Focus();
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

        private void RedPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form scheduleForm = new Form();
                object[] optionalParameter = new object[] { this.scheduleID, "2005" };
                scheduleForm = this.form25050Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2005, optionalParameter, this.form25050Control.WorkItem);
                if (scheduleForm != null)
                {
                    if (scheduleForm.ShowDialog() == DialogResult.OK)
                    {
                        this.LoadLock();
                        this.GreenpictureBox.Focus();
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

        /// <summary>
        /// Reload the Lock while clicking the Lock Buttons
        /// </summary>
        private void LoadLock()
        {
            this.editScheduleData = this.form25050Control.WorkItem.F25050_GetScheduleDetails(this.scheduleID);

            if (this.editScheduleData.f2200ListScheduleDataTable.Rows.Count > 0)
            {
               if(((!string.IsNullOrEmpty(this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.LockScheduleByColumn].ToString()))&& (this.editScheduleData.f2200ListScheduleDataTable.Rows[0][this.editScheduleData.f2200ListScheduleDataTable.LockScheduleByColumn].ToString() != "0")))
               {
                   this.RedPictureBox.Visible = true;
                   this.RedPictureBox.BringToFront();
                   this.GreenpictureBox.Visible = false;
               }
               else
               {
                   this.GreenpictureBox.Visible = true;
                   this.GreenpictureBox.BringToFront();
                   this.RedPictureBox.Visible = false;
               }
            }
        }
   }
}