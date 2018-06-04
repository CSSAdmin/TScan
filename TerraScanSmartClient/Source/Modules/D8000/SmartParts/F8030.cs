//--------------------------------------------------------------------------------------------
// <copyright file="F8030.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created// 
//*********************************************************************************/

namespace D8000
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
    /// F8030 class file
    /// </summary>
    public partial class F8030 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// eventId
        /// </summary>
        private int eventId;

        /// <summary>
        /// Used to store the gdocEventHeaderDataTableRowCount
        /// </summary>
        private int gdocEventHeaderDataTableRowCount;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// activeWorkOrderId
        /// </summary>
        private int workOrderId = -1;

        /// <summary>
        /// activeWorkOrderId
        /// </summary>
        private int workOrderStatus;

        /// <summary>
        /// noofChildren
        /// </summary>
        private int noofChildren = -1;

        /// <summary>
        /// featureClassId
        /// </summary>
        private int featureClassId;

        /// <summary>
        /// activeWorkOrderId
        /// </summary>
        private int activeWorkOrderId;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Has the value from delete message dialog box
        /// </summary>
        private bool deleteMessageResult;

        /// <summary>
        /// statusId
        /// </summary>
        private int statusId;

        /////// <summary>
        /////// bool status value true when required fields exist
        /////// </summary>
        ////private bool status;        

        /// <summary>
        /// controller F8030
        /// </summary>
        private F8030Controller form8030Control;

        /// <summary>
        ///  used to Convert Color From string
        /// </summary>
        private ColorConverter colorConv = new ColorConverter();

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Instance for GDocEventHeaderData
        /// </summary>
        private GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// isShift
        /// </summary>
        private bool isshift;

        /// <summary>
        /// mock permission field for the mock userid
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Used to holds Master Form Number
        /// </summary>
        private int masterformnumber = -99;

        /// <summary>
        /// Used to store featureId
        /// </summary>
        private int featureId;

        /// <summary>
        /// Used to store Form Master No Datatable
        /// </summary>

        private GDocEventHeaderData.GetMasterFormNoDataTable getFormMasterDataTable = new GDocEventHeaderData.GetMasterFormNoDataTable();

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8030"/> class.
        /// </summary>
        public F8030()
        {
            this.InitializeComponent();
            ////this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8030"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8030(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.eventId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.EventPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.EventPictureBox.Height, this.EventPictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication
        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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

        ////Coding added for the co : 2838 [for 85000 load call method] by malliga
        /// <summary>
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        #endregion

        #region Property

        /// <summary>
        /// For F8030Control
        /// </summary>
        [CreateNew]
        public F8030Controller Form8030Control
        {
            get { return this.form8030Control as F8030Controller; }
            set { this.form8030Control = value; }
        }

        /// <summary>
        /// Gets or sets the active work order id.
        /// </summary>
        /// <value>The active work order id.</value>
        public int WorkOrderId
        {
            get { return this.workOrderId; }
            set { this.workOrderId = value; }
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
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.ClearEventHeader();
                        this.LockControls(false);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        if (this.slicePermissionField.deletePermission)
                        {
                            if (this.gdocEventHeaderDataTableRowCount > 0)
                            {
                                ////message is raised whether to delete or not  
                                DialogResult dialogResult;
                                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("EventHeaderDeleteDialog4")), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("EventHeaderDeleteDialog5"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    this.DeleteGDocEventHeader();
                                }
                                else if (dialogResult == DialogResult.No)
                                {
                                    //// Dont allow to remove keyid from QE Grid
                                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));
                                    return;
                                }

                                //// when delete message result is true delete the event, refresh the form 85000 if active and close the form
                                if (this.deleteMessageResult == true)
                                {
                                    ////call the delete method
                                    this.ClearEventHeader();
                                    this.LockControls(false);

                                    ////Coding added for the co : 2838 by Maliga
                                    this.RefreshEventOrderForm();
                                    ////code ends here

                                    SliceFormCloseAlert sliceFormCloseAlert;
                                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                                    sliceFormCloseAlert.FlagFormClose = true;
                                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                                }
                                else
                                {
                                    //// Dont allow to remove keyid from QE Grid
                                    this.FormSlice_RevertDeleteAlert(this, new DataEventArgs<int>(this.masterFormNo));

                                    this.LockControls(true);
                                    this.CustomizeEventHeader();

                                    SliceFormCloseAlert sliceFormCloseAlert;
                                    sliceFormCloseAlert.FormNo = this.masterFormNo;
                                    sliceFormCloseAlert.FlagFormClose = false;
                                    this.FormSlice_FormCloseAlert(this, new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                                }
                            }
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
            finally
            {
                this.Cursor = Cursors.Default;
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
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        ////if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                        ////{
                        ////    this.ControlLock(false);
                        ////}
                        ////else
                        ////{
                        ////    this.ControlLock(true);
                        ////}
                       
                        this.LockControls(true);
                        this.CustomizeEventHeader();
                        this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
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
                            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                        {
                            if (this.slicePermissionField.editPermission)
                            {
                                this.SaveGDocEventHeader();
                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                ////khaja added code to fix Bug#4211
                                this.EventTextBox.Focus();
                            }
                        }
                        else
                        {
                            this.LockControls(true);
                           //// this.ControlLock(false);
                            this.CustomizeEventHeader();
                            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                        {
                            this.eventId = eventArgs.Data.SelectedKeyId;
                            this.LoadEventHeader();
                            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
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
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.Visible)
                    {
                        if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                        {
                            this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                            this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                            this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                            this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                            if (this.gdocEventHeaderDataTableRowCount > 0)
                            {
                                eventArgs.Data.FlagInvalidSliceKey = false;
                            }
                            else
                            {
                                //if (eventArgs.Data.FlagInvalidSliceKey)
                                //{
                                    eventArgs.Data.FlagInvalidSliceKey = true;
                                //}
                            }

                            /*TO set the focus in first slice-editable field on form load */
                            if (this.ParentForm != null && this.gdocEventHeaderDataTableRowCount > 0)
                            {
                                this.ParentForm.ActiveControl = this.EventTextBox;
                                this.ParentForm.ActiveControl.Focus();
                                //this.EventTextBox.Focus();
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

        #endregion Event Subscription

        #region Methods

        /// <summary>
        /// Saves the GDoc event header.
        /// </summary>        
        public void SaveGDocEventHeader()
        {
            this.gdocEventHeaderData.SaveGDocEventHeader.Rows.Clear();
            GDocEventHeaderData.SaveGDocEventHeaderRow dr = this.gdocEventHeaderData.SaveGDocEventHeader.NewSaveGDocEventHeaderRow();
            dr.EventID = this.eventId;

            ////the event text box is now editable and can be up dated
            dr.Event = this.EventTextBox.Text.Trim();

            dr.EventDate = this.GeneralEventDate.Text.Trim();
            ////Coding added for the co : 2838
            ////Assigning endadate values to datatable
            dr.EndDate = this.EnddateTextBox.Text.Trim();   
            if (!string.IsNullOrEmpty(this.EventStatus.Text.Trim()))
            {
                dr.StatusID = Convert.ToInt32(this.EventStatus.SelectedValue);
            }

            if (!string.IsNullOrEmpty(this.WorkOrderLinkLabel.Text.Trim()))
            {
                dr.WOID = Convert.ToInt32(this.WorkOrderLinkLabel.Text);
            }

            if (this.CompleteCheckBox.Checked == true)
            {
                // Bit Value is 1
                ////dr.Complete = true;
                dr.Complete = this.CompleteCheckBox.Checked;
            }
            else
            {
                // Bit Value is 0
                ////dr.Complete = false;
                dr.Complete = this.CompleteCheckBox.Checked;
            }

            this.gdocEventHeaderData.SaveGDocEventHeader.Rows.Add(dr);

            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(this.gdocEventHeaderData.SaveGDocEventHeader.Copy());
            tempDataSet.Tables[0].TableName = "Table";

            this.gdocEventHeaderData.Merge(this.form8030Control.WorkItem.SaveGDocEventHeader(tempDataSet.GetXml(), TerraScanCommon.UserId));
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.eventId;
            ////to reload the form with the current keyid(this.eventId)                
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));

            ////Coding added for the co : 2838 [For 85000 Load]
            this.RefreshEventOrderForm();
        }

        #region Loadslicedetails[for 85000 load] 

        ////Coding added for the co : 2838
        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.D9030_F9030_LoadSliceDetails(this, eventArgs);
        }

        #endregion

        /// <summary>
        /// Deletes the GDoc event header.
        /// </summary>
        public void DeleteGDocEventHeader()
        {
            DialogResult dialogResult;

            this.gdocEventHeaderData = this.form8030Control.WorkItem.GetGDocEventHeader(this.eventId);

            this.noofChildren = Convert.ToInt32(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.NoOfChildrenColumn]);

            if (this.noofChildren > 0)
            {
                ////dialogResult = MessageBox.Show(string.Concat("Event " + this.eventId + " has " + this.noofChildren + " children(s).\nClick yes to promote the children as Parent Events.\nClick No to Delete the Event and its children.\nClick Cancel to cancel the operation.", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("EventHeaderDeleteDialog1") + this.eventId + SharedFunctions.GetResourceString("EventHeaderDeleteDialog2") + this.noofChildren + SharedFunctions.GetResourceString("EventHeaderDeleteDialog3"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    this.form8030Control.WorkItem.DeleteGDocEventHeader(this.eventId, 1, TerraScan.Common.TerraScanCommon.UserId);
                    this.deleteMessageResult = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.form8030Control.WorkItem.DeleteGDocEventHeader(this.eventId, 0, TerraScan.Common.TerraScanCommon.UserId);
                    this.deleteMessageResult = true;
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    this.deleteMessageResult = false;
                    return;
                }
            }
            else
            {
                this.form8030Control.WorkItem.DeleteGDocEventHeader(this.eventId, 0, TerraScan.Common.TerraScanCommon.UserId);
                this.deleteMessageResult = true;
            }
        }

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

        #endregion Protected methods

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            this.EventHeaderDatePict.Focus();
            this.GeneralEventDate.Text = dateSelected;
        }

        /// <summary>
        /// Customizes the event header.
        /// </summary>
        private void CustomizeEventHeader()
        {
            bool tempCheckValue;
         
            this.gdocEventHeaderData = this.form8030Control.WorkItem.GetGDocEventHeader(this.eventId);
            this.gdocEventHeaderDataTableRowCount = this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count;

            ////Coding added for the CO(2838) by malliga on 31/8/2009
            ////get a formmaster no
            this.getFormMasterDataTable = (GDocEventHeaderData.GetMasterFormNoDataTable)this.gdocEventHeaderData.GetMasterFormNo.Copy();
            ////if (this.gdocEventHeaderData.GetMasterFormNo.Rows.Count > 0)
            ////{
            ////    int.TryParse(this.gdocEventHeaderData.GetMasterFormNo.Rows[0][0].ToString(), out this.masterformnumber);
            ////}

            //// to check whether datatable GetGDocEventHeader is empty
            if (this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0)
            {
                this.EventTextBox.Text = this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.EventColumn].ToString();
                this.GeneralEventDate.Text = this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.EventDateColumn].ToString();

                ////Coding added for the CO(2838) by malliga on 31/8/2009
                ////Assigning Enddate values to the textbox
                if(!string.IsNullOrEmpty(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.EndDateColumn].ToString()))
                {
                  this.EnddateTextBox.Text = this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.EndDateColumn].ToString();
                }
                else
                {
                    this.EnddateTextBox.Text = string.Empty;  
                }

                this.WorkOrderLinkLabel.Text = this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.WorkOrderColumn].ToString();
                if (!string.IsNullOrEmpty(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.StatusIDColumn].ToString()))
                {
                    this.statusId = Convert.ToInt32(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.StatusIDColumn]);
                }

                if (!string.IsNullOrEmpty(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.StatusColorColumn].ToString()))
                {
                    this.StatusColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.StatusColorColumn].ToString());
                }

                tempCheckValue = Convert.ToBoolean(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.CompleteColumn].ToString());
                if (tempCheckValue == true)
                {
                    this.CompleteCheckBox.Checked = true;
                }
                else if (tempCheckValue == false)
                {
                    this.CompleteCheckBox.Checked = false;
                }
                
                ////the feature class id will be sent to the form F8002
                this.featureClassId = Convert.ToInt32(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.FeatureClassIDColumn]);

                //// Added By Ramya(Sprint40 Change Order)


                ////Coding added for the CO(2838) by malliga on 31/8/2009
                ////get a featureId
                this.featureId = Convert.ToInt32(this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.FeatureIDColumn]);

                if (this.gdocEventHeaderData.GetGDocEventHeader.Rows[0][this.gdocEventHeaderData.GetGDocEventHeader.IsWorkOrderColumn].ToString() == "False")
                {
                    this.WorkOrderLinkLablePanel.Visible = false;
                    this.StatusPanel.Size = new System.Drawing.Size(497, 40);
                    this.StatusPanel.Location = new System.Drawing.Point(1, 41);
                    this.EventStatus.Size = new System.Drawing.Size(440, 24);
                    this.EventStatus.Location = new System.Drawing.Point(12, 12);
                    this.StatusColorLabel.Location = new System.Drawing.Point(461, 6);
                    this.StatusColorLabel.Size = new System.Drawing.Size(29, 29);
                    this.EventDatePanel.Size = new System.Drawing.Size(100, 40);
                    this.EventDatePanel.Location = new System.Drawing.Point(497, 41);
                    this.EnddatePanel.Size = new System.Drawing.Size(100, 40);
                    this.EnddatePanel.Location = new System.Drawing.Point(596, 41);
                }
                else
                {
                    this.WorkOrderLinkLablePanel.Visible = true;
                    this.StatusPanel.Size = new System.Drawing.Size(398, 40);
                    this.StatusPanel.Location = new System.Drawing.Point(1, 41);
                    this.EventStatus.Size = new System.Drawing.Size(343, 24);
                    this.EventStatus.Location = new System.Drawing.Point(12, 12);
                    this.StatusColorLabel.Location = new System.Drawing.Point(361, 6);
                    this.StatusColorLabel.Size = new System.Drawing.Size(29, 29);
                    this.EventDatePanel.Size = new System.Drawing.Size(100, 40);
                    this.EventDatePanel.Location = new System.Drawing.Point(398, 41);
                    this.EnddatePanel.Size = new System.Drawing.Size(100, 40);
                    this.EnddatePanel.Location = new System.Drawing.Point(497, 41);
                    this.WorkOrderLinkLablePanel.Size = new System.Drawing.Size(104, 40);
                    this.WorkOrderLinkLablePanel.Location = new System.Drawing.Point(596, 41);
                }

                this.LoadEventstatus();
                this.EventHeaderControls();
            }
            else
            {
                this.ClearEventHeader();
            }
        }

        /// <summary>
        /// Enables the header controls.
        /// </summary>
        private void EventHeaderControls()
        {
            this.WorkOrderLinkLablePanel.Enabled = true; 
            this.WorkOrdertButton.Enabled = true;
            ////this.WorkOrderLinkLabel.Enabled = true;
            this.CompletePanel.Enabled = true;  
            this.CompleteCheckBox.Enabled = true;
            this.EventPanel.Enabled = true; 
            this.EventHeaderDatePict.Enabled = true;
            this.GeneralEventDate.Enabled = true;
            this.StatusPanel.Enabled = true; 
            this.EventStatus.Enabled = true;
            this.EventStatusTextBox.Enabled = true;
            this.EventDateTimePicker.Enabled = true;
            this.EventDatePanel.Enabled = true;
            ////Coding added for the CO(2838) by malliga on 31/8/2009
            this.EnddateTextBox.Enabled = true;
            this.EventDateTimePicker.Enabled = true;
            this.EnddatePicturebox.Enabled = true;
            this.EnddatePanel.Enabled = true;  
        }

        /// <summary>
        /// Clears the event header.
        /// </summary>
        private void ClearEventHeader()
        {
            this.ClearEventHeaderControls();
            this.LockControls(false);
        }

        /// <summary>
        /// Used to Clear the Event Header Controls
        /// </summary>
        private void ClearEventHeaderControls()
        {
            this.EventTextBox.Text = string.Empty;
            this.GeneralEventDate.Text = string.Empty;
            this.WorkOrderLinkLabel.Text = string.Empty;
            this.EventDateTimePicker.Enabled = false;
            this.WorkOrdertButton.Enabled = false;
            this.WorkOrderLinkLabel.Enabled = false;
            this.CompleteCheckBox.Enabled = false;
            this.EventHeaderDatePict.Enabled = false;
            this.GeneralEventDate.Enabled = false;
            this.StatusColorLabel.Visible = false;
            this.EventStatusTextBox.Text = string.Empty;
            ////Coding added for the CO(2838) by malliga on 31/8/2009
            this.EnddateTextBox.Enabled = false;
            this.EventDateTimePicker.Enabled = false;
            this.EnddatePicturebox.Enabled = false;  
            this.ClearEventStatus();
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockControls(bool lockControl)
        {
            this.EventPanel.Enabled = lockControl;
            this.StatusPanel.Enabled = lockControl;
            this.EventDatePanel.Enabled = lockControl;
            this.WorkOrderLinkLablePanel.Enabled = lockControl;
            this.CompletePanel.Enabled = lockControl;
            this.EventDateTimePicker.Enabled = lockControl;
            ////Coding added for the CO(2838) by malliga on 31/8/2009
            this.EnddatePanel.Enabled = lockControl;
            this.EnddatedateTimePicker.Enabled = lockControl;  
        }

        /// <summary>
        /// Loads the eventstatus.
        /// </summary>
        private void LoadEventstatus()
        {
            this.StatusColorLabel.Visible = true;
            this.gdocEventHeaderData = this.form8030Control.WorkItem.ListGDocEventHeaderStatus(this.eventId);

            if (this.gdocEventHeaderData.ListGDocEventHeaderStatus.Rows.Count > 0)
            {
                // load the data combo box.
                this.EventStatus.DataSource = this.gdocEventHeaderData.ListGDocEventHeaderStatus;
                this.EventStatus.DisplayMember = this.gdocEventHeaderData.ListGDocEventHeaderStatus.StatusColumn.ColumnName;
                this.EventStatus.ValueMember = this.gdocEventHeaderData.ListGDocEventHeaderStatus.StatusIDColumn.ColumnName;
                this.EventStatus.SelectedValue = this.statusId;
            }
        }

        /// <summary>
        /// Clears the event status Combo box.
        /// </summary>
        private void ClearEventStatus()
        {
            // load an empty combo box
            this.EventStatus.DataSource = null;
            this.EventStatus.Items.Clear();
            this.EventStatus.Refresh();
            this.EventStatus.Enabled = false;
            this.EventStatusTextBox.Text = string.Empty;
            this.EventStatusTextBox.Enabled = false;
            this.StatusColorLabel.Visible = false;
        }

        /// <summary>
        /// Loads the Active Work Order form.
        /// </summary>
        private void LoadWorkOrderForm()
        {
            Form activeWorkOrderSelect = new Form();
            object[] optionalParameter = new object[] { this.featureClassId };
            activeWorkOrderSelect = this.form8030Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8002, optionalParameter, this.form8030Control.WorkItem);
            if (activeWorkOrderSelect != null)
            {
                DialogResult dialogResult = activeWorkOrderSelect.ShowDialog();
                if (dialogResult != DialogResult.Ignore)
                {
                    this.workOrderId = Convert.ToInt32(TerraScanCommon.GetValue(activeWorkOrderSelect, "ActiveWorkOrderId"));
                }
            }

            if (this.workOrderId != -1)
            {
                this.WorkOrderLinkLabel.Text = this.workOrderId.ToString();
            }
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

            if (string.IsNullOrEmpty(this.eventId.ToString()))
            {
                this.EventTextBox.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            if (string.IsNullOrEmpty(this.EventStatus.Text.Trim()))
            {
                this.EventStatus.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("EventStatusValidation");
                return sliceValidationFields;
            }

            if (string.IsNullOrEmpty(this.GeneralEventDate.Text.Trim()))
            {
                this.GeneralEventDate.Focus();
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("EventHeaderValidation");
                return sliceValidationFields;
            }

            ////Coding added for the CO(2838) by malliga on 31/8/2009
            ////To check the enddate should not be less than eventdate
            if (!string.IsNullOrEmpty(this.EnddateTextBox.Text.Trim()))
            {
                DateTime enddate;
                DateTime eventdate;
                enddate = DateTime.Parse(this.EnddateTextBox.Text.Trim());
                eventdate = DateTime.Parse(this.GeneralEventDate.Text.Trim());
                if (enddate < eventdate)
                {
                    this.EnddateTextBox.Focus();
                    sliceValidationFields.RequiredFieldMissing = true;
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F8030EndDateValidation");
                    return sliceValidationFields;
                }
            }

            return sliceValidationFields;
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
            }
        }

        /// <summary>
        /// Sets the color of the status.
        /// </summary>
        /// <param name="selectedStatus">The selected status.</param>
        private void SetStatusColor(string selectedStatus)
        {
            string statusIdValue = "StatusID =" + selectedStatus;
            DataRow[] statusRow;
            statusRow = this.gdocEventHeaderData.ListGDocEventHeaderStatus.Select(statusIdValue);
            if (statusRow.Length > 0)
            {
                this.StatusColorLabel.BackColor = (System.Drawing.Color)this.colorConv.ConvertFromInvariantString(statusRow[0][2].ToString());
            }
        }

        /// <summary>
        /// Controls the look.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLook)
        {
            this.EventTextBox.LockKeyPress = controlLook;
            this.EventStatus.Enabled = !controlLook;
            this.EventStatusTextBox.LockKeyPress = controlLook;
            
            this.StatusColorLabel.Enabled = !controlLook;
            this.GeneralEventDate.LockKeyPress = controlLook;
            this.EventHeaderDatePict.Enabled = !controlLook;
            this.WorkOrderLinkLabel.Enabled = !controlLook;
            this.WorkOrderLinkTextBox.LockKeyPress = controlLook;
            this.WorkOrdertButton.Enabled = !controlLook;
            this.CompleteCheckBox.Enabled = !controlLook;
            this.EventDateTimePicker.Enabled = !controlLook;
            ////Coding added for the CO(2838) by malliga on 31/8/2009
            this.EnddateTextBox.LockKeyPress = controlLook;
            this.EnddatePicturebox.Enabled = !controlLook;
            this.EnddatedateTimePicker.Enabled = !controlLook; 
        }

        private void ControlEnableDisbale(bool controlenable)
        {
            this.EventPanel.Enabled = controlenable;
            this.EventTextBox.Enabled = controlenable; 
        }

        /// <summary>
        /// To Load the event header Form slices.
        /// </summary>
        private void LoadEventHeader()
        {
            this.FlagSliceForm = true;
            this.flagLoadOnProcess = true;
            this.CustomizeEventHeader();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.flagLoadOnProcess = false;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F8030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8030_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadEventHeader();
                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                if (this.ParentForm != null)
                ////if(this.gdocEventHeaderData.GetGDocEventHeader.Rows.Count > 0 )
                {
                    this.ParentForm.ActiveControl = this.EventTextBox;
                    this.ParentForm.ActiveControl.Focus();
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
        /// Handles the Click event of the WorkOrdertButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrdertButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkOrderForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EventPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventPictureBox_Click(object sender, EventArgs e)
        {
            // this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the KeyPress event of the GeneralEventDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void GeneralEventDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the EventStatusTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EventStatusTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the WorkOrderLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkOrderLinkLabel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the CheckedChanged event of the CompleteCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CompleteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the TextChanged event of the GeneralEventDate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void GeneralEventDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the Click event of the EventHeaderDatePict control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventHeaderDatePict_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(GeneralEventDate.Text.Trim()))
                {
                    EventDateTimePicker.Value = Convert.ToDateTime(GeneralEventDate.Text);
                }
                else
                {
                    EventDateTimePicker.Value = DateTime.Today;
                }

                EventDateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CloseUp event of the EventDateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.GeneralEventDate.Text = EventDateTimePicker.Text;
                this.ParentForm.ActiveControl = GeneralEventDate;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the EventPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.GDocEventHeaderToolTip.SetToolTip(this.EventPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the WorkOrderLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void WorkOrderLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.WorkOrderLinkLabel.Text.Trim()))
                {
                    Form activeWorkOrderSelect = new Form();

                    this.activeWorkOrderId = Convert.ToInt32(this.WorkOrderLinkLabel.Text);

                    object[] optionalParameter = new object[] { this.activeWorkOrderId };

                    activeWorkOrderSelect = this.form8030Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(8010, optionalParameter, this.form8030Control.WorkItem);

                    if (activeWorkOrderSelect != null)
                    {
                        activeWorkOrderSelect.ShowDialog();
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the EventStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void EventStatus_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.EventStatus.Text.Trim().Length > 15)
                {
                    ////this.GDocEventHeaderToolTip.SetToolTip(this.EventStatus, this.EventStatus.Text);
                }
                else
                {
                    this.GDocEventHeaderToolTip.RemoveAll();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the EventStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                if (!string.IsNullOrEmpty(this.EventStatus.Text.Trim()))
                {
                    this.SetStatusColor(this.EventStatus.SelectedValue.ToString());
                    this.statusId = (int)this.EventStatus.SelectedValue; 
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Key press event to hide month calendar control shown from Datetime picker when Tab is pressed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventDateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

                WorkOrderLinkLabel.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EventTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EventTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// EventStatus_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">events</param>
        private void EventStatus_SelectedIndexChanged(object sender, EventArgs e)
         {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// EventDateTimePicker_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void EventDateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.isshift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region End Date Events  [Coding added for the CO(2838) by malliga on 31/8/2009]
        #region End Date Events
        /// <summary>
        /// Handles the CloseUp event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_CloseUp(object sender, EventArgs e)
        {
            try
            {
                this.EnddateTextBox.Text = this.EnddatedateTimePicker.Text;
                this.ParentForm.ActiveControl = EnddateTextBox;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.isshift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the EnddatedateTimePicker control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EnddatedateTimePicker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyChar;
                if (keyCode == 9)
                {
                    SendKeys.Send("{Esc}");
                }

                WorkOrderLinkLabel.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EnddatePicturebox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddatePicturebox_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(EnddateTextBox.Text.Trim()))
                {
                    this.EnddatedateTimePicker.Value = Convert.ToDateTime(EnddateTextBox.Text);
                }
                else
                {
                    this.EnddatedateTimePicker.Value = DateTime.Today;
                }

                this.EnddatedateTimePicker.Focus();
                SendKeys.Send("{F4}");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EnddateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EnddateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
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
        /// Handles the KeyPress event of the EnddateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EnddateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Event Status Event
        /// <summary>
        /// Handles the Validating event of the EventStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void EventStatus_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (!this.statusId.Equals(this.EventStatus.SelectedValue))
                {
                    if (!this.flagLoadOnProcess)
                    {
                        this.EditEnabled();
                    }
                    if (this.EventStatus.SelectedValue == null)
                    {
                        this.EventStatus.Text = string.Empty;
                        this.StatusColorLabel.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    if (!string.IsNullOrEmpty(this.EventStatus.Text.Trim()))
                    {
                        this.SetStatusColor(this.EventStatus.SelectedValue.ToString());
                        this.statusId = (int)this.EventStatus.SelectedValue; 
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region 85000 Refresh Method
        /// <summary>
        /// Refreshes the event order form.
        /// </summary>
        private void RefreshEventOrderForm()
        {
            string filteValue = "(";
            
            DataSet menuDataSet = (DataSet)this.form8030Control.WorkItem.RootWorkItem.State["FormItemsDataSet"];

            // Filter condition
            for (int recordCount = 0; recordCount < this.getFormMasterDataTable.Rows.Count; recordCount++)
            {
                if (!this.getFormMasterDataTable.Rows.Count.Equals(recordCount + 1))
                {
                    filteValue = filteValue + "Form = " + this.getFormMasterDataTable.Rows[recordCount][this.getFormMasterDataTable.MasterFormColumn.ColumnName].ToString() + " OR ";
                }
                else
                {
                    filteValue = filteValue + "Form = " + this.getFormMasterDataTable.Rows[recordCount][this.getFormMasterDataTable.MasterFormColumn.ColumnName].ToString() + ") AND Active = 1";
                }
            }

            DataRow[] selectedForms = null;
            foreach (DataTable table in menuDataSet.Tables)
            {
                selectedForms = table.Select(filteValue);
                
                foreach (DataRow row in selectedForms)
                {
                     SliceReloadActiveRecord sliceReloadActiveRecord;
                     sliceReloadActiveRecord.MasterFormNo = (int)row["Form"];
                     sliceReloadActiveRecord.SelectedKeyId = this.featureId;
                     this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                }
            }
        }
        #endregion

        /// <summary>
        /// Handles the KeyPress event of the EventStatus control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void EventStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
           //// this.EditEnabled(); 
        }

        #endregion

        private void EventStatus_KeyDown(object sender, KeyEventArgs e)
        {
           //// this.EditEnabled(); 
        }

        private void EventStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
    }
}
