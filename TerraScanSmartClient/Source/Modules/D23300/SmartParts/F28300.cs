//--------------------------------------------------------------------------------------------
// <copyright file="F28300.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the MAD Import Template.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//20160701          Priyadharshini        
//*********************************************************************************/

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.ObjectBuilder;
using TerraScan.BusinessEntities;
using TerraScan.Common;
using TerraScan.Infrastructure.Interface.Constants;
using TerraScan.Utilities;

namespace D23300
{
    /// <summary>
    /// Form F28300 class
    /// </summary>
 
    [SmartPart]
    public partial class F28300 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// controller F28300
        /// </summary>
        private F28300Controller form28300Control;

        /// <summary>
        /// masterFormNo
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

        /// <summary>
        /// TemplateID
        /// </summary>
        private int? TemplateID;

        /// <summary>
        /// MADTemplateDataSet variable is used to store  for MAD Import Template. 
        /// </summary>       
        private F23300MADImportTemplate MADTemplateDataSet;

        /// <summary>
        /// InputFileDataTable variable is used to store list of InputFile details for MAD Import Template. 
        /// </summary> 
        private DataTable inputFileDataTable;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScan.Common.TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// saveComplete
        /// </summary>
        private bool saveComplete;

        /// <summary>
        /// flagLoadOnProcess Local variable.
        /// </summary>
        private bool GridLoad;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MADImportTemplateForm"/> class.
        /// </summary>
        public F28300()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MADImportTemplateForm"/> class.
        /// </summary>
        public F28300(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.TemplateID = keyID;
            this.MADTemplateDataSet = new F23300MADImportTemplate();
            this.inputFileDataTable = new DataTable();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Template Header", 28, 81, 128);
            this.InputFileDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(112, 36, "Input Detail", 174, 150, 94);
            this.StatementPictureBox.SendToBack();
        }

        #endregion

        /// <summary>
        /// Handles the Load event of the MADImportTemplateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F28300_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.CreateInputFile(null);
            this.LoadDefaultView();
            this.InputFileDetailPictureBox.SendToBack();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.ImportTypeComboBox.SelectedValue != null)
            {
                if (this.InputFileGridView.Columns.Count >= 2)
                {
                    if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                    {
                        this.InputFileGridView.Columns[2].Visible = true;
                        this.InputFileGridView.Width = 378;
                        this.MortgageTemplateControlPanel.Width = 480;
                        this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                    }
                    else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                    {
                        this.InputFileGridView.Columns[2].Visible = false;
                        this.MortgageTemplateControlPanel.Width = 315;
                        this.InputFileGridView.Width = 276;
                        this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                        this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 172);
                    }
                }
            }
            this.TemplateNameTextBox.Focus();
        }
        
        #region Event Publication

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_RevertDeleteAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_RevertDeleteAlert;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

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

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// For F28300Control
        /// </summary>
        [CreateNew]
        public F28300Controller Form28300Control
        {
            get { return this.form28300Control as F28300Controller; }
            set { this.form28300Control = value; }
        }

        #endregion Properties

        #region Event Subscription
        /// <summary>
        /// OnD9030_F9030_SetSlicePermission
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
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

                    if (this.MADTemplateDataSet.GetMADImportTemplate.Rows.Count > 0)
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
            this.GetMADImportTemplateDetails(Convert.ToInt32(this.TemplateID));
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
                this.TemplateID = eventArgs.Data.SelectedKeyId;

                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    this.TemplateID = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.GetMADImportTemplateDetails(Convert.ToInt32(this.TemplateID));

                }
                else if (this.TemplateID == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                {
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.GetMADImportTemplateDetails(Convert.ToInt32(this.TemplateID));
                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                this.SetEditRecord();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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

        #endregion

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
                    this.CreateInputFile(null);
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.ImportTypeComboBox.SelectedIndex = 1;
                }
                else
                {
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                }

                this.TemplateNameTextBox.Focus();
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

        ///// <summary>
        ///// Called when [D9030_ F9030_ delete slice information].
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {

            if (this != null && this.IsDisposed != true)
            {
                if (this.slicePermissionField.deletePermission)
                {
                    string ReturnMessage = this.form28300Control.WorkItem.DeleteMADTemplate(Convert.ToInt32(this.TemplateID), TerraScanCommon.UserId);
                    if (!string.IsNullOrEmpty(ReturnMessage))
                    {
                        MessageBox.Show(ReturnMessage, "TerraScan – MAD Import Template in use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        //this.FormSlice_NullRecordMode(this, new DataEventArgs<int>(this.masterFormNo));
                        this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                        this.ClearControls();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
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
                    if (this.TemplateID == -99)
                    {
                        this.TemplateID = null;
                    }

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.TemplateID = null;
                    }

                    this.SaveMADImportTemplate();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.TemplateID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.TemplateID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.PermissionControlLock(!this.PermissionFiled.editPermission);
            }
            catch (Exception Ex)
            {
            }
        }

        /// <summary>
        /// PermissionControlLock
        /// </summary>
        /// <param name="controlLook">controlLook</param>
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
                    this.TemplateID = eventArgs.Data.SelectedKeyId;
                    this.GetMADImportTemplateDetails(Convert.ToInt32(this.TemplateID));
                }
            }
        }

        /// <summary>
        /// Lock the controls.
        /// </summary>
        /// <param name="controlLook">The controlLook.</param>
        /// <returns></returns>
        private void ControlLock(bool controlLook)
        {
            this.TemplateNameTextBox.LockKeyPress = controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.FilePathTextBox.LockKeyPress = controlLook;
            this.ImportTypeComboBox.Enabled = !controlLook;
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

            if (string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
            {
                this.TemplateNameTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }

            if (string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
            {
                this.FilePathTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (this.ImportTypeComboBox.SelectedIndex == -1)
            {
                this.ImportTypeComboBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            string fileError = string.Empty;
            fileError = this.CheckFileExist();
            if (!string.IsNullOrEmpty(fileError.Trim()))
            {
                this.FilePathTextBox.Focus();
                sliceValidationFields.ErrorMessage = fileError;
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["position"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["position"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[2]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[2]["position"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[3]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[3]["position"].ToString().Trim(), "0"))
            {

                if (this.ImportTypeComboBox.SelectedValue.ToString().Trim() == "1")
                {
                    if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["width"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["width"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[2]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[2]["width"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[3]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[3]["width"].ToString().Trim(), "0"))
                    {

                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        sliceValidationFields.RequiredFieldMissing = true;
                        return sliceValidationFields;
                    }
                }
            }
            else
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            return sliceValidationFields;
        }

        #endregion EventsSubscription

        #region TemplateDetails

        /// <summary>
        /// Gets the LoadDefaultView details.
        /// </summary>
        /// 
        private void LoadDefaultView()
        {
            this.EnableControls(true);
            this.EnablePanelControl(true);
            this.GetPermitImportFileType();
            this.F28300LoadFormDetails();
        }

        /// <summary>
        /// Gets the EnableControls details.
        /// </summary>
        /// <param name="show">show.</param>
        /// 
        private void EnableControls(bool show)
        {
            this.TemplateNameTextBox.Enabled = show;
            this.DescriptionTextBox.Enabled = show;
            this.FilePathTextBox.Enabled = show;
            this.FilePathButton.Enabled = show;
            this.MortgageTemplateControlPanel.Enabled = show;
            this.TopPanel.Enabled = show;
        }
        /// <summary>
        /// Gets the EnablePanelControl details.
        /// </summary>
        /// <param name="view">view.</param>
        /// 
        private void EnablePanelControl(bool view)
        {
            this.TopPanel.Enabled = view;
            this.MortgageTemplateControlPanel.Enabled = view;
        }

        /// <summary>
        /// Gets the F28300LoadFormDetails details.
        /// </summary>
        /// 
        private void F28300LoadFormDetails()
        {
            try
            {
                this.MADTemplateDataSet = form28300Control.WorkItem.GetMADImportTemplate(Convert.ToInt32(this.TemplateID));
                if (MADTemplateDataSet.Tables[1].Rows.Count > 0)
                {
                    this.DescriptionTextBox.Text = this.MADTemplateDataSet.Tables[1].Rows[0]["Description"].ToString();
                    this.TemplateNameTextBox.Text = this.MADTemplateDataSet.Tables[1].Rows[0]["TemplateName"].ToString();
                    this.FilePathTextBox.Text = this.MADTemplateDataSet.Tables[1].Rows[0]["FilePath"].ToString();
                    if (!string.IsNullOrEmpty(this.MADTemplateDataSet.Tables[1].Rows[0]["TypeID"].ToString()))
                    {
                        this.ImportTypeComboBox.SelectedValue = this.MADTemplateDataSet.Tables[1].Rows[0]["TypeID"].ToString();
                        this.form28300Control.WorkItem.RootWorkItem.State["TypeID"] = this.ImportTypeComboBox.SelectedValue.ToString();
                    }
                    this.DisplayInputFileDetails(this.MADTemplateDataSet.GetMADImportTemplate);
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

        /// <summary>
        /// Gets the MAD import template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        private void GetMADImportTemplateDetails(int templateId)
        {
            try
            {
                if (this.MADTemplateDataSet.GetMADImportTemplate != null)
                {
                    if (this.MADTemplateDataSet.GetMADImportTemplate.Rows.Count == 0)
                    {
                        this.MADTemplateDataSet = form28300Control.WorkItem.GetMADImportTemplate(Convert.ToInt32(templateId));
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            try
            {
                if (this.MADTemplateDataSet.GetMADImportTemplate != null)
                {
                    if (this.MADTemplateDataSet.GetMADImportTemplate.Rows.Count > 0)
                    {

                        this.DescriptionTextBox.Text = this.MADTemplateDataSet.GetMADImportTemplate.Rows[0][this.MADTemplateDataSet.GetMADImportTemplate.DescriptionColumn].ToString();
                        this.FilePathTextBox.Text = this.MADTemplateDataSet.GetMADImportTemplate.Rows[0][this.MADTemplateDataSet.GetMADImportTemplate.FilePathColumn].ToString();
                        this.TemplateNameTextBox.Text = this.MADTemplateDataSet.GetMADImportTemplate.Rows[0][this.MADTemplateDataSet.GetMADImportTemplate.TemplateNameColumn].ToString();
                        this.ImportTypeComboBox.SelectedValue = this.MADTemplateDataSet.GetMADImportTemplate.Rows[0][this.MADTemplateDataSet.GetMADImportTemplate.TypeIDColumn].ToString();
                        this.DisplayInputFileDetails(this.MADTemplateDataSet.GetMADImportTemplate);
                    }
                    else
                    {
                        this.TemplateID = -1;
                        this.NullRecords = true;
                        this.ClearControls();
                        this.CreateInputFile(null);
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

        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.FilePathTextBox.Text = string.Empty;
            this.TemplateNameTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;

        }

        /// <summary>
        /// Method Will Enable/Disable the Text Boxes
        /// </summary>
        /// <param name="enable">Enable/Disabled</param>
        private void TextboxEnabled(bool enable)
        {
            this.DescriptionTextBox.Enabled = enable;
            this.FilePathTextBox.Enabled = enable;
            this.TemplateNameTextBox.Enabled = enable;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void LockControls(bool enableValue)
        {
            this.ImportTypeComboBox.Enabled = enableValue;
            this.FilePathButton.Enabled = enableValue;
            this.TopPanel.Enabled = enableValue;
            this.MortgageTemplateControlPanel.Enabled = enableValue;
        }

        /// <summary>
        /// Locks the text box controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockTextBoxControls(bool lockControl)
        {
            this.DescriptionTextBox.LockKeyPress = lockControl;
            this.FilePathTextBox.LockKeyPress = lockControl;
            this.TemplateNameTextBox.LockKeyPress = lockControl;
        }

        /// <summary>
        /// Gets the type of the MAD import file.
        /// </summary>
        private void GetPermitImportFileType()
        {
            try
            {
                F23300MADImportTemplate MADImportFileTypeDataSet = this.form28300Control.WorkItem.ListMADImportFileType();
                this.ImportTypeComboBox.ValueMember = "TypeID";
                this.ImportTypeComboBox.DisplayMember = "TypeName";
                this.ImportTypeComboBox.DataSource = MADImportFileTypeDataSet.ListMADImportFileType;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Input File Details

        /// <summary>
        /// Creates the input file.
        /// </summary>
        /// <param name="madImportDatatable">The madImport datatable.</param>
        private void CreateInputFile(DataTable madDatatable)
        {
            try
            {
                for (int colCount = this.inputFileDataTable.Columns.Count - 1; colCount >= 0; colCount--)
                {
                    this.inputFileDataTable.Columns.RemoveAt(colCount);
                }

                this.inputFileDataTable.Clear();

                this.inputFileDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("FieldName"), new DataColumn("Position"), new DataColumn("Width") });
                if (madDatatable != null)
                {
                    if (madDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileParcelNumber"), madDatatable.Rows[0]["ParcelNumber_Pos"].ToString(), madDatatable.Rows[0]["ParcelNumber_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileRollyear"), madDatatable.Rows[0]["RollYear_Pos"].ToString(), madDatatable.Rows[0]["RollYear_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDistrictNumber"), madDatatable.Rows[0]["DistrictNumber_Pos"].ToString(), madDatatable.Rows[0]["DistrictNumber_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileOverrideAmount"), madDatatable.Rows[0]["OverrideAmount_Pos"].ToString(), madDatatable.Rows[0]["OverrideAmount_Wid"].ToString() });
                    }
                    else
                    {
                        this.CreateEmptyStructure();
                    }
                }
                else
                {
                    this.CreateEmptyStructure();
                }
                this.InputFileGridView.DataSource = this.inputFileDataTable;
                this.SetDefaultProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Creates the empty structure.
        /// </summary>
        private void CreateEmptyStructure()
        {
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileParcelNumber"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileRollyear"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDistrictNumber"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileOverrideAmount"), string.Empty, string.Empty });
       

        }

        /// <summary>
        /// Displays the input file details.
        /// </summary>
        /// <param name="mortgageDatatable">The mortgage datatable.</param>
        private void DisplayInputFileDetails(F23300MADImportTemplate.GetMADImportTemplateDataTable madImportDatatable)
        {
            try
            {
                if (madImportDatatable != null)
                {
                    if (madImportDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows[0][1] = madImportDatatable.Rows[0][madImportDatatable.ParcelNumber_PosColumn].ToString();
                        this.inputFileDataTable.Rows[0][2] = madImportDatatable.Rows[0][madImportDatatable.ParcelNumber_WidColumn].ToString();
                        this.inputFileDataTable.Rows[1][1] = madImportDatatable.Rows[0][madImportDatatable.RollYear_PosColumn].ToString();
                        this.inputFileDataTable.Rows[1][2] = madImportDatatable.Rows[0][madImportDatatable.RollYear_WidColumn].ToString();

                        ////District Number,Override amount
                        this.inputFileDataTable.Rows[2][1] = madImportDatatable.Rows[0][madImportDatatable.DistrictNumber_PosColumn].ToString();
                        this.inputFileDataTable.Rows[2][2] = madImportDatatable.Rows[0][madImportDatatable.DistrictNumber_WidColumn].ToString();
                        this.inputFileDataTable.Rows[3][1] = madImportDatatable.Rows[0][madImportDatatable.OverrideAmount_PosColumn].ToString();
                        this.inputFileDataTable.Rows[3][2] = madImportDatatable.Rows[0][madImportDatatable.OverrideAmount_WidColumn].ToString();

                    }
                    else
                    {
                        this.PopulateEmptyFile();
                    }
                }
                else
                {
                    this.PopulateEmptyFile();
                }

                this.InputFileGridView.DataSource = this.inputFileDataTable;
                this.SetDefaultProperty();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the default property.
        /// </summary>
        private void SetDefaultProperty()
        {
            this.InputFileGridView.Columns[0].DefaultCellStyle.ForeColor = Color.FromArgb(51, 51, 153);
            this.InputFileGridView.Columns[0].ReadOnly = true;
            this.InputFileGridView.Columns[0].HeaderText = "Field Name";
            this.InputFileGridView.Columns[0].Width = 176;
            this.InputFileGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.InputFileGridView.Columns[0].DefaultCellStyle.SelectionForeColor = Color.FromArgb(51, 51, 153);
            this.InputFileGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[1].Frozen = true;
            this.InputFileGridView.Columns[2].Frozen = true;
            this.InputFileGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.InputFileGridView.Columns[1].Resizable = DataGridViewTriState.False;
            this.InputFileGridView.Columns[2].Resizable = DataGridViewTriState.False;
        }

        /// <summary>
        /// Populates the empty file.
        /// </summary>
        private void PopulateEmptyFile()
        {
            this.inputFileDataTable.Rows[0][1] = string.Empty;
            this.inputFileDataTable.Rows[0][2] = string.Empty;
            this.inputFileDataTable.Rows[1][1] = string.Empty;
            this.inputFileDataTable.Rows[1][2] = string.Empty;
            this.inputFileDataTable.Rows[2][1] = string.Empty;
            this.inputFileDataTable.Rows[2][2] = string.Empty;
            this.inputFileDataTable.Rows[3][1] = string.Empty;
            this.inputFileDataTable.Rows[3][2] = string.Empty;
        }
        #endregion

        /// <summary>
        /// Saves the MAD import.
        /// </summary>
        /// <returns>SaveMADImportTemplate</returns>
        /// 
        private bool SaveMADImportTemplate()
        {
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    this.Cursor = Cursors.WaitCursor;
                    string validationErrors = string.Empty;
                    int tempImportType = 0;
                    int tempparcelNumberPos = 0;
                    int tempparcelNumberWid = 0;
                    int temprollYearPos = 0;
                    int temprollYearWid = 0;
                    int tempdistrictNumberPos = 0;
                    int tempdistrictNumberWid = 0;
                    int tempoverridePos = 0;
                    int tempoverrideWid = 0;
                   
                    if (!string.IsNullOrEmpty(this.ImportTypeComboBox.SelectedValue.ToString()))
                    {
                        tempImportType = Convert.ToInt32(this.ImportTypeComboBox.SelectedValue.ToString());
                    }
                    int.TryParse(this.inputFileDataTable.Rows[0]["position"].ToString(), out tempparcelNumberPos);
                    int.TryParse(this.inputFileDataTable.Rows[0]["width"].ToString(), out tempparcelNumberWid);
                    int.TryParse(this.inputFileDataTable.Rows[1]["position"].ToString(), out temprollYearPos);
                    int.TryParse(this.inputFileDataTable.Rows[1]["width"].ToString(), out temprollYearWid);
                    int.TryParse(this.inputFileDataTable.Rows[2]["position"].ToString(), out tempdistrictNumberPos);
                    int.TryParse(this.inputFileDataTable.Rows[2]["width"].ToString(), out tempdistrictNumberWid);
                    int.TryParse(this.inputFileDataTable.Rows[3]["position"].ToString(), out tempoverridePos);
                    int.TryParse(this.inputFileDataTable.Rows[3]["width"].ToString(), out tempoverrideWid);
                   
                    F23300MADImportTemplate insertMADimportDetails = new F23300MADImportTemplate();
                    F23300MADImportTemplate.SaveMADImportTemplateRow dr = insertMADimportDetails.SaveMADImportTemplate.NewSaveMADImportTemplateRow();
                    dr.TemplateName = this.TemplateNameTextBox.Text;
                    dr.TypeID = tempImportType;
                    dr.Description = this.DescriptionTextBox.Text;
                    dr.FilePath = this.FilePathTextBox.Text;

                    if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                    {
                        dr.ParcelNumber_Pos = tempparcelNumberPos;
                        dr.ParcelNumber_Wid = 0;
                        dr.RollYear_Pos = temprollYearPos;
                        dr.RollYear_Wid = 0;
                        dr.DistrictNumber_Pos = tempdistrictNumberPos;
                        dr.DistrictNumber_Wid = 0;
                        dr.OverrideAmount_Pos = tempoverridePos;
                        dr.OverrideAmount_Wid = 0;
                    }
                    else
                    {
                        dr.ParcelNumber_Pos = tempparcelNumberPos;
                        dr.ParcelNumber_Wid = tempparcelNumberWid;
                        dr.RollYear_Pos = temprollYearPos;
                        dr.RollYear_Wid = temprollYearWid;
                        dr.DistrictNumber_Pos = tempdistrictNumberPos;
                        dr.DistrictNumber_Wid = tempdistrictNumberWid;
                        dr.OverrideAmount_Pos = tempoverridePos;
                        dr.OverrideAmount_Wid = tempoverrideWid;
                    }

                    insertMADimportDetails.SaveMADImportTemplate.Rows.Add(dr);
                    insertMADimportDetails.SaveMADImportTemplate.AcceptChanges();
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(insertMADimportDetails.SaveMADImportTemplate.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    int returnValue;
                    returnValue = this.form28300Control.WorkItem.SaveMADImportTemplate(TemplateID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue;
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = returnValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.TemplateID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.TemplateID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.saveComplete = true;
               
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Imports the type combo box_ selected index changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ImportTypeComboBox.SelectedValue != null)
                {
                    if (this.InputFileGridView.Columns.Count >= 2)
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            this.InputFileGridView.Columns[2].Visible = true;
                            this.InputFileGridView.Width = 378;
                            this.MortgageTemplateControlPanel.Width = 480;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                            this.SetEditRecord();
                        }
                        else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                        {
                            this.InputFileGridView.Columns[2].Visible = false;
                            this.MortgageTemplateControlPanel.Width = 315;
                            this.InputFileGridView.Width = 276;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                            this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
                            this.SetEditRecord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the file exist.
        /// </summary>
        /// <returns>returns bool</returns>
        private string CheckFileExist()
        {
            string filePath = string.Empty;
            filePath = this.FilePathTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(filePath))
            {
                if (!File.Exists(filePath))
                {
                    this.FilePathTextBox.Text = string.Empty;
                    this.FilePathTextBox.Focus();
                    return SharedFunctions.GetResourceString("RequiredField");
                }
                return string.Empty;
            }
            else
            {
                this.FilePathTextBox.Focus();
                return "File path should be entered. \n";
            }
        }

        /// <summary>
        /// Handles the Click event of the FilePathButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilePathButton_Click(object sender, EventArgs e)
        {
            this.FilePathOpenFileDialog.Filter = "Text Documents(*.txt)|*.txt|CSV(*.csv)|*.csv";
            if (this.FilePathOpenFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                this.SetEditRecord();
                this.FilePathTextBox.Text = this.FilePathOpenFileDialog.FileName;
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        /// 
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            // TODO : Genralized 
            Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                reportOptionalParameter.Clear();
                reportOptionalParameter.Add("KeyName", "TemplateID");
                TerraScanCommon.ShowReport(10111, TerraScan.Common.Reports.Report.ReportType.Preview, reportOptionalParameter);
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
        /// Handles the MouseEnter event of the FilePathTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilePathTextBox_MouseEnter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.FilePathTextBox.Text))
            {
                if (this.FilePathTextBox.Text.Length > 60)
                {
                    this.TemplateToolTip.RemoveAll();
                    this.TemplateToolTip.SetToolTip(this.FilePathTextBox, this.FilePathTextBox.Text);
                }
                else
                {
                    this.TemplateToolTip.RemoveAll();
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ImportTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.InputFileDetailPictureBox.SendToBack();
                if (this.ImportTypeComboBox.SelectedValue != null)
                {
                    if (this.InputFileGridView.Columns.Count >= 2)
                    {
                        if (this.ImportTypeComboBox.SelectedValue.ToString() == "1")
                        {
                            this.InputFileGridView.Columns[2].Visible = true;
                            this.InputFileGridView.Width = 378;
                            this.MortgageTemplateControlPanel.Width = 480;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(370, 0);
                            this.SetEditRecord();
                        }
                        else if (this.ImportTypeComboBox.SelectedValue.ToString() == "2")
                        {
                            this.InputFileGridView.Columns[2].Visible = false;
                            this.MortgageTemplateControlPanel.Width = 315;
                            this.InputFileGridView.Width = 276;
                            this.InputFileDetailPictureBox.Location = new System.Drawing.Point(268, 0);
                            this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
                            this.SetEditRecord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the F28300 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F28300_Enter(object sender, EventArgs e)
        {
            this.ParentForm.Activate();
        }

        /// <summary>
        /// Handles the Click event of the ImportViewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(23310);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Grid

        /// <summary>
        /// Handles the CellValueChanged event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.EditEnabled();
        }

        /// <summary>
        /// Handles the CellParsing event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            int temp;
            if (e.Value.ToString().IndexOf('-') > -1)
            {
                e.Value = string.Empty;
                e.ParsingApplied = true;
            }
            else if (!int.TryParse(e.Value.ToString(), out temp))
            {
                e.Value = string.Empty;
                e.ParsingApplied = true;
            }
        }

        /// <summary>
        /// Handles the DataError event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the TextChanged event of theInputFileGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.saveComplete)
                {
                    this.EditEnabled();
                }
                else
                {
                    this.saveComplete = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
            e.Control.TextChanged += new EventHandler(this.InputFileGridView_TextChanged);
            e.Control.Validated += new EventHandler(this.InputFileGridView_Validated);
        }
        /// <summary>
        /// Handles the Validated event of the InputFileGridViewControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Validated(object sender, EventArgs e)
        {
            this.InputFileGridView.EditingControl.TextChanged -= new EventHandler(this.InputFileGridView_TextChanged);
        }

        /// <summary>
        /// Handles the CellEndEdit event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    this.InputFileGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    int currentValue = 0;
                    int.TryParse(this.InputFileGridView.CurrentCell.Value.ToString(), out currentValue);
                    if (currentValue > 0 && currentValue <= 32000)
                    {
                        // this.InputFileGridView.CurrentCell = null;
                    }
                    else
                    {
                        this.InputFileGridView.CurrentCell.Value = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int outInt;

            // Only paint if desired, formattable column

            if (e.ColumnIndex == this.InputFileGridView.Columns["Position"].Index || e.ColumnIndex == this.InputFileGridView.Columns["Width"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //// Only paint if text provided, Only paint if desired text is in cell 
                ////if (e.Value != null && !String.IsNullOrEmpty(this.InputFileGridView.Rows[e.RowIndex].Cells["Position"].Value.ToString()))
                if (!string.IsNullOrEmpty(e.Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (e.Value.ToString().Trim() == "M")
                    {
                    }

                    if (!int.TryParse(val, out outInt))
                    {
                        e.Value = string.Empty;
                        e.FormattingApplied = false;
                    }
                    else if (e.Value.ToString() == "0")
                    {
                        e.Value = string.Empty;
                    }
                }
                else
                {
                    e.Value = string.Empty;
                }
            }
        }

        /// <summary>
        /// Handles the CellEnter event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.GridLoad)
            {
                if (e.ColumnIndex == 0)
                {
                    SendKeys.Send("{TAB}");
                }
            }

        }

        /// <summary>
        /// Handles the Enter event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Enter(object sender, EventArgs e)
        {
            this.InputFileGridView.CurrentCell = this.InputFileGridView[1, 0];
            this.GridLoad = true;
        }

        /// <summary>
        /// Handles the Leave event of the InputFileGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void InputFileGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                this.InputFileGridView.CurrentCell = null;
                this.GridLoad = false;
            }
            catch (System.InvalidOperationException ex)
            {
                ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion grid
    }
}