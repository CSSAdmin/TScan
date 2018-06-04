//--------------------------------------------------------------------------------------------
// <copyright file="F28200.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Permit Import Template.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//20160606          Priyadharshini        
//*********************************************************************************/using System;

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
using TerraScan.SmartParts;
using TerraScan.Utilities;
namespace D23200
{
    /// <summary>
    /// Form F28200 class
    /// </summary>
    [SmartPart]
    public partial class F28200 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// controller F1011
        /// </summary>
        private F28200Controller form28200Control;

        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;
        private bool formMasterPermissionEdit;

        private int? TemplateID;

        /// <summary>
        /// permitTemplateDataSet variable is used to store  for permit Import Template. 
        /// </summary>       
        private F23200PermitImportTemplate permitTemplateDataSet;

        /// <summary>
        /// InputFileDataTable variable is used to store list of InputFile details for Permit Import Template. 
        /// </summary> 
        private DataTable inputFileDataTable;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScan.Common.TerraScanCommon.PageModeTypes pageMode;

        private bool saveComplete;


        /// <summary>
        /// flagLoadOnProcess Local variable.
        /// </summary>
        private bool GridLoad;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PermitImportTemplateForm"/> class.
        /// </summary>
        public F28200()
        {
            this.InitializeComponent();
        }

        public F28200(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.TemplateID = keyID;
            this.permitTemplateDataSet = new F23200PermitImportTemplate();
            this.inputFileDataTable = new DataTable();
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Template Header", 28, 81, 128);
            this.InputFileDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(265, 36, "Input File Detail", 174, 150, 94);
            this.StatementPictureBox.SendToBack();
        }
        #endregion
        /// <summary>
        /// Handles the Load event of the PermitImportTemplateForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F28200_Load(object sender, EventArgs e)
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
                        this.InputFileDetailPictureBox.Size = new System.Drawing.Size(36, 265);
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
        /// For F1011Control
        /// </summary>
        [CreateNew]
        public F28200Controller Form28200Control
        {
            get { return this.form28200Control as F28200Controller; }
            set { this.form28200Control = value; }
        }

        #endregion Properties

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

                    if (this.permitTemplateDataSet.GetPermitImportTemplate.Rows.Count > 0)
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
            this.GetPermitImportTemplateDetails(Convert.ToInt32(this.TemplateID));
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
                    this.GetPermitImportTemplateDetails(Convert.ToInt32(this.TemplateID));

                }
                else if (this.TemplateID == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                {
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.GetPermitImportTemplateDetails(Convert.ToInt32(this.TemplateID));
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
                    string ReturnMessage = this.form28200Control.WorkItem.DeletePermiTemplate(Convert.ToInt32(this.TemplateID), TerraScanCommon.UserId);
                    if (!string.IsNullOrEmpty(ReturnMessage))
                    {
                        MessageBox.Show(ReturnMessage, "TerraScan – Permit Import Template in use", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    this.SavePermitImportTemplate();
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
                    this.GetPermitImportTemplateDetails(Convert.ToInt32(this.TemplateID));
                }
            }
        }

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

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[2]["position"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[2]["position"].ToString().Trim(), "0"))
            {

                if (this.ImportTypeComboBox.SelectedValue.ToString().Trim() == "1")
                {
                    if (!string.IsNullOrEmpty(this.inputFileDataTable.Rows[0]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[0]["width"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[1]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[1]["width"].ToString().Trim(), "0")

                 && !string.IsNullOrEmpty(this.inputFileDataTable.Rows[2]["width"].ToString().Trim()) && !string.Equals(this.inputFileDataTable.Rows[2]["width"].ToString().Trim(), "0"))
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

        private void LoadDefaultView()
        {
            this.EnableControls(true);
            this.EnablePanelControl(true);
            this.GetPermitImportFileType();
            this.F28200LoadFormDetails();
        }

        private void EnableControls(bool show)
        {
            this.TemplateNameTextBox.Enabled = show;
            this.DescriptionTextBox.Enabled = show;
            this.FilePathTextBox.Enabled = show;
            this.FilePathButton.Enabled = show;
            this.MortgageTemplateControlPanel.Enabled = show;
            this.TopPanel.Enabled = show;
        }

        private void EnablePanelControl(bool view)
        {
            this.TopPanel.Enabled = view;
            this.MortgageTemplateControlPanel.Enabled = view;
        }

        private void F28200LoadFormDetails()
        {
            try
            {
                this.permitTemplateDataSet = form28200Control.WorkItem.GetPermitImportTemplate(Convert.ToInt32(this.TemplateID));
                if (permitTemplateDataSet.Tables[1].Rows.Count > 0)
                {
                    this.DescriptionTextBox.Text = this.permitTemplateDataSet.Tables[1].Rows[0]["Description"].ToString();
                    this.TemplateNameTextBox.Text = this.permitTemplateDataSet.Tables[1].Rows[0]["TemplateName"].ToString();
                    this.FilePathTextBox.Text = this.permitTemplateDataSet.Tables[1].Rows[0]["FilePath"].ToString();

                    if (!string.IsNullOrEmpty(this.permitTemplateDataSet.Tables[1].Rows[0]["TypeID"].ToString()))
                    {
                        this.ImportTypeComboBox.SelectedValue = this.permitTemplateDataSet.Tables[1].Rows[0]["TypeID"].ToString();
                        this.form28200Control.WorkItem.RootWorkItem.State["TypeID"] = this.ImportTypeComboBox.SelectedValue.ToString();
                    }
                    this.DisplayInputFileDetails(this.permitTemplateDataSet.GetPermitImportTemplate);
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
        /// Gets the permit import template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        private void GetPermitImportTemplateDetails(int templateId)
        {
            try
            {
                this.permitTemplateDataSet = form28200Control.WorkItem.GetPermitImportTemplate(Convert.ToInt32(templateId));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            try
            {
                if (this.permitTemplateDataSet.GetPermitImportTemplate != null)
                {
                    if (this.permitTemplateDataSet.GetPermitImportTemplate.Rows.Count > 0)
                    {

                        this.DescriptionTextBox.Text = this.permitTemplateDataSet.GetPermitImportTemplate.Rows[0][this.permitTemplateDataSet.GetPermitImportTemplate.DescriptionColumn].ToString();
                        this.FilePathTextBox.Text = this.permitTemplateDataSet.GetPermitImportTemplate.Rows[0][this.permitTemplateDataSet.GetPermitImportTemplate.FilePathColumn].ToString();
                        this.TemplateNameTextBox.Text = this.permitTemplateDataSet.GetPermitImportTemplate.Rows[0][this.permitTemplateDataSet.GetPermitImportTemplate.TemplateNameColumn].ToString();
                        this.ImportTypeComboBox.SelectedValue = this.permitTemplateDataSet.GetPermitImportTemplate.Rows[0][this.permitTemplateDataSet.GetPermitImportTemplate.TypeIDColumn].ToString();
                        this.DisplayInputFileDetails(this.permitTemplateDataSet.GetPermitImportTemplate);
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
        /// Gets the type of the Permit import file.
        /// </summary>
        private void GetPermitImportFileType()
        {
            try
            {
                F23200PermitImportTemplate permitImportFileTypeDataSet = this.form28200Control.WorkItem.ListPermitImportFileType();
                this.ImportTypeComboBox.ValueMember = "TypeID";
                this.ImportTypeComboBox.DisplayMember = "TypeName";
                this.ImportTypeComboBox.DataSource = permitImportFileTypeDataSet.ListPermitImportFileType;
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
        /// <param name="mortgageDatatable">The mortgage datatable.</param>
        private void CreateInputFile(DataTable mortgageDatatable)
        {
            try
            {
                for (int colCount = this.inputFileDataTable.Columns.Count - 1; colCount >= 0; colCount--)
                {
                    this.inputFileDataTable.Columns.RemoveAt(colCount);
                }

                this.inputFileDataTable.Clear();

                this.inputFileDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("FieldName"), new DataColumn("Position"), new DataColumn("Width") });
                if (mortgageDatatable != null)
                {
                    if (mortgageDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileParcelNumber"), mortgageDatatable.Rows[0]["ParcelNumber_Pos"].ToString(), mortgageDatatable.Rows[0]["ParcelNumber_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileRollyear"), mortgageDatatable.Rows[0]["RollYear_Pos"].ToString(), mortgageDatatable.Rows[0]["RollYear_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFilePermitNumber"), mortgageDatatable.Rows[0]["PermitNumber_Pos"].ToString(), mortgageDatatable.Rows[0]["PermitNumber_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDateOpened"), mortgageDatatable.Rows[0]["DateOpened_Pos"].ToString(), mortgageDatatable.Rows[0]["DateOpened_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileLastVisit"), mortgageDatatable.Rows[0]["DateLastVisit_Pos"].ToString(), mortgageDatatable.Rows[0]["DateLastVisit_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDateClosed"), mortgageDatatable.Rows[0]["DateClosed_Pos"].ToString(), mortgageDatatable.Rows[0]["DateClosed_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileEstValue"), mortgageDatatable.Rows[0]["EstValue_Pos"].ToString(), mortgageDatatable.Rows[0]["EstValue_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDescription"), mortgageDatatable.Rows[0]["PermitDescription_Pos"].ToString(), mortgageDatatable.Rows[0]["PermitDescription_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputAssignedAppraiser"), mortgageDatatable.Rows[0]["AssignedAppraiserUserName_Pos"].ToString(), mortgageDatatable.Rows[0]["AssignedAppraiserUserName_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputType"), mortgageDatatable.Rows[0]["PermitType_Pos"].ToString(), mortgageDatatable.Rows[0]["PermitType_Wid"].ToString() });
                        this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputPercentComplete"), mortgageDatatable.Rows[0]["PercentComplete_Pos"].ToString(), mortgageDatatable.Rows[0]["PercentComplete_Wid"].ToString() });
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
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFilePermitNumber"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDateOpened"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileLastVisit"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDateClosed"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileEstValue"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputFileDescription"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputAssignedAppraiser"), string.Empty, string.Empty });
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputType"), string.Empty, string.Empty });
            //For implementing CartId
            this.inputFileDataTable.Rows.Add(new object[] { SharedFunctions.GetResourceString("PIInputPercentComplete"), string.Empty, string.Empty });

        }

        /// <summary>
        /// Displays the input file details.
        /// </summary>
        /// <param name="mortgageDatatable">The mortgage datatable.</param>
        private void DisplayInputFileDetails(F23200PermitImportTemplate.GetPermitImportTemplateDataTable permitImportDatatable)
        {
            try
            {
                if (permitImportDatatable != null)
                {
                    if (permitImportDatatable.Rows.Count > 0)
                    {
                        this.inputFileDataTable.Rows[0][1] = permitImportDatatable.Rows[0][permitImportDatatable.ParcelNumber_PosColumn].ToString();
                        this.inputFileDataTable.Rows[0][2] = permitImportDatatable.Rows[0][permitImportDatatable.ParcelNumber_WidColumn].ToString();
                        this.inputFileDataTable.Rows[1][1] = permitImportDatatable.Rows[0][permitImportDatatable.RollYear_PosColumn].ToString();
                        this.inputFileDataTable.Rows[1][2] = permitImportDatatable.Rows[0][permitImportDatatable.RollYear_WidColumn].ToString();

                        ////Parcel Number,PostType
                        this.inputFileDataTable.Rows[2][1] = permitImportDatatable.Rows[0][permitImportDatatable.PermitNumber_PosColumn].ToString();
                        this.inputFileDataTable.Rows[2][2] = permitImportDatatable.Rows[0][permitImportDatatable.PermitNumber_WidColumn].ToString();
                        this.inputFileDataTable.Rows[3][1] = permitImportDatatable.Rows[0][permitImportDatatable.DateOpened_PosColumn].ToString();
                        this.inputFileDataTable.Rows[3][2] = permitImportDatatable.Rows[0][permitImportDatatable.DateOpened_WidColumn].ToString();

                        this.inputFileDataTable.Rows[4][1] = permitImportDatatable.Rows[0][permitImportDatatable.DateLastVisit_PosColumn].ToString();
                        this.inputFileDataTable.Rows[4][2] = permitImportDatatable.Rows[0][permitImportDatatable.DateLastVisit_WidColumn].ToString();
                        this.inputFileDataTable.Rows[5][1] = permitImportDatatable.Rows[0][permitImportDatatable.DateClosed_PosColumn].ToString();
                        this.inputFileDataTable.Rows[5][2] = permitImportDatatable.Rows[0][permitImportDatatable.DateClosed_WidColumn].ToString();
                        this.inputFileDataTable.Rows[6][1] = permitImportDatatable.Rows[0][permitImportDatatable.EstValue_PosColumn].ToString();
                        this.inputFileDataTable.Rows[6][2] = permitImportDatatable.Rows[0][permitImportDatatable.EstValue_WidColumn].ToString();
                        this.inputFileDataTable.Rows[7][1] = permitImportDatatable.Rows[0][permitImportDatatable.PermitDescription_PosColumn].ToString();
                        this.inputFileDataTable.Rows[7][2] = permitImportDatatable.Rows[0][permitImportDatatable.PermitDescription_WidColumn].ToString();
                        this.inputFileDataTable.Rows[8][1] = permitImportDatatable.Rows[0][permitImportDatatable.AssignedAppraiserUserName_PosColumn].ToString();
                        this.inputFileDataTable.Rows[8][2] = permitImportDatatable.Rows[0][permitImportDatatable.AssignedAppraiserUserName_WidColumn].ToString();
                        this.inputFileDataTable.Rows[9][1] = permitImportDatatable.Rows[0][permitImportDatatable.PermitType_PosColumn].ToString();
                        this.inputFileDataTable.Rows[9][2] = permitImportDatatable.Rows[0][permitImportDatatable.PermitType_WidColumn].ToString();
                        this.inputFileDataTable.Rows[10][1] = permitImportDatatable.Rows[0][permitImportDatatable.PercentComplete_PosColumn].ToString();
                        this.inputFileDataTable.Rows[10][2] = permitImportDatatable.Rows[0][permitImportDatatable.PercentComplete_WidColumn].ToString();
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
            this.inputFileDataTable.Rows[4][1] = string.Empty;
            this.inputFileDataTable.Rows[4][2] = string.Empty;
            this.inputFileDataTable.Rows[5][1] = string.Empty;
            this.inputFileDataTable.Rows[5][2] = string.Empty;
            this.inputFileDataTable.Rows[6][1] = string.Empty;
            this.inputFileDataTable.Rows[6][2] = string.Empty;
            this.inputFileDataTable.Rows[7][1] = string.Empty;
            this.inputFileDataTable.Rows[7][2] = string.Empty;
            this.inputFileDataTable.Rows[8][1] = string.Empty;
            this.inputFileDataTable.Rows[8][2] = string.Empty;
            this.inputFileDataTable.Rows[9][1] = string.Empty;
            this.inputFileDataTable.Rows[9][2] = string.Empty;
            this.inputFileDataTable.Rows[10][1] = string.Empty;
            this.inputFileDataTable.Rows[10][2] = string.Empty;

        }
        #endregion

        /// <summary>
        /// Saves the mortage import.
        /// </summary>
        /// <returns>SaveMortageImport</returns>
        private bool SavePermitImportTemplate()
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
                    int temppermitNumberPos = 0;
                    int temppermitNumberWid = 0;
                    int tempdateOpenedPos = 0;
                    int tempdateOpenedWid = 0;
                    int tempdateLastVisitPos = 0;
                    int tempdateLastVisitWid = 0;
                    int tempdateClosedPos = 0;
                    int tempdateClosedWid = 0;
                    int tempestValuePos = 0;
                    int tempestValueWid = 0;
                    int tempassignedAppraiserUserNamePos = 0;
                    int tempassignedAppraiserUserNameWid = 0;
                    int temppermitTypePos = 0;
                    int temppermitTypeWid = 0;
                    int temppercentCompletePos = 0;
                    int temppercentCompleteWid = 0;
                    int temppermitDescriptionPos = 0;
                    int temppermitDescriptionWid = 0;
                    if (!string.IsNullOrEmpty(this.ImportTypeComboBox.SelectedValue.ToString()))
                    {
                        tempImportType = Convert.ToInt32(this.ImportTypeComboBox.SelectedValue.ToString());
                    }
                    int.TryParse(this.inputFileDataTable.Rows[0]["position"].ToString(), out tempparcelNumberPos);
                    int.TryParse(this.inputFileDataTable.Rows[0]["width"].ToString(), out tempparcelNumberWid);
                    int.TryParse(this.inputFileDataTable.Rows[1]["position"].ToString(), out temprollYearPos);
                    int.TryParse(this.inputFileDataTable.Rows[1]["width"].ToString(), out temprollYearWid);

                    int.TryParse(this.inputFileDataTable.Rows[2]["position"].ToString(), out temppermitNumberPos);
                    int.TryParse(this.inputFileDataTable.Rows[2]["width"].ToString(), out temppermitNumberWid);
                    int.TryParse(this.inputFileDataTable.Rows[3]["position"].ToString(), out tempdateOpenedPos);
                    int.TryParse(this.inputFileDataTable.Rows[3]["width"].ToString(), out tempdateOpenedWid);

                    int.TryParse(this.inputFileDataTable.Rows[4]["position"].ToString(), out tempdateLastVisitPos);
                    int.TryParse(this.inputFileDataTable.Rows[4]["width"].ToString(), out tempdateLastVisitWid);
                    int.TryParse(this.inputFileDataTable.Rows[5]["position"].ToString(), out tempdateClosedPos);
                    int.TryParse(this.inputFileDataTable.Rows[5]["width"].ToString(), out tempdateClosedWid);
                    int.TryParse(this.inputFileDataTable.Rows[6]["position"].ToString(), out tempestValuePos);
                    int.TryParse(this.inputFileDataTable.Rows[6]["width"].ToString(), out tempestValueWid);
                    int.TryParse(this.inputFileDataTable.Rows[7]["position"].ToString(), out temppermitDescriptionPos);
                    int.TryParse(this.inputFileDataTable.Rows[7]["width"].ToString(), out temppermitDescriptionWid);
                    int.TryParse(this.inputFileDataTable.Rows[8]["position"].ToString(), out tempassignedAppraiserUserNamePos);
                    int.TryParse(this.inputFileDataTable.Rows[8]["width"].ToString(), out tempassignedAppraiserUserNameWid);
                    int.TryParse(this.inputFileDataTable.Rows[9]["position"].ToString(), out temppermitTypePos);
                    int.TryParse(this.inputFileDataTable.Rows[9]["width"].ToString(), out temppermitTypeWid);
                    int.TryParse(this.inputFileDataTable.Rows[10]["position"].ToString(), out temppercentCompletePos);
                    int.TryParse(this.inputFileDataTable.Rows[10]["width"].ToString(), out temppercentCompleteWid);
                    F23200PermitImportTemplate insertpermitimportDetails = new F23200PermitImportTemplate();
                    F23200PermitImportTemplate.SavePermitImportTemplateRow dr = insertpermitimportDetails.SavePermitImportTemplate.NewSavePermitImportTemplateRow();
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
                        dr.PermitNumber_Pos = temppermitNumberPos;
                        dr.PermitNumber_Wid = 0;
                        dr.DateOpened_Pos = tempdateOpenedPos;
                        dr.DateOpened_Wid = 0;
                        dr.DateLastVisit_Pos = tempdateLastVisitPos;
                        dr.DateLastVisit_Wid = 0;
                        dr.DateClosed_Pos = tempdateClosedPos;
                        dr.DateClosed_Wid = 0;
                        dr.EstValue_Pos = tempestValuePos;
                        dr.EstValue_Wid = 0;
                        dr.AssignedAppraiserUserName_Pos = tempassignedAppraiserUserNamePos;
                        dr.AssignedAppraiserUserName_Wid = 0;
                        dr.PermitType_Pos = temppermitTypePos;
                        dr.PermitType_Wid = 0;
                        dr.PercentComplete_Pos = temppercentCompletePos;
                        dr.PercentComplete_Wid = 0;
                        dr.PermitDescription_Pos = temppermitDescriptionPos;
                        dr.PermitDescription_Wid = 0;
                    }
                    else
                    {
                        dr.ParcelNumber_Pos = tempparcelNumberPos;
                        dr.ParcelNumber_Wid = tempparcelNumberWid;
                        dr.RollYear_Pos = temprollYearPos;
                        dr.RollYear_Wid = temprollYearWid;
                        dr.PermitNumber_Pos = temppermitNumberPos;
                        dr.PermitNumber_Wid = temppermitNumberWid;
                        dr.DateOpened_Pos = tempdateOpenedPos;
                        dr.DateOpened_Wid = tempdateOpenedWid;
                        dr.DateLastVisit_Pos = tempdateLastVisitPos;
                        dr.DateLastVisit_Wid = tempdateLastVisitWid;
                        dr.DateClosed_Pos = tempdateClosedPos;
                        dr.DateClosed_Wid = tempdateClosedWid;
                        dr.EstValue_Pos = tempestValuePos;
                        dr.EstValue_Wid = tempestValueWid;
                        dr.AssignedAppraiserUserName_Pos = tempassignedAppraiserUserNamePos;
                        dr.AssignedAppraiserUserName_Wid = tempassignedAppraiserUserNameWid;
                        dr.PermitType_Pos = temppermitTypePos;
                        dr.PermitType_Wid = temppermitTypeWid;
                        dr.PercentComplete_Pos = temppercentCompletePos;
                        dr.PercentComplete_Wid = temppercentCompleteWid;
                        dr.PermitDescription_Pos = temppermitDescriptionPos;
                        dr.PermitDescription_Wid = temppermitDescriptionWid;
                    }

                    insertpermitimportDetails.SavePermitImportTemplate.Rows.Add(dr);
                    insertpermitimportDetails.SavePermitImportTemplate.AcceptChanges();
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(insertpermitimportDetails.SavePermitImportTemplate.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    int returnValue;
                    returnValue = this.form28200Control.WorkItem.SavePermitImportTemplate(TemplateID, tempDataSet.GetXml(), TerraScanCommon.UserId);
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
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus()
        {
            if (String.Compare(this.pageMode.ToString(), TerraScanCommon.PageModeTypes.View.ToString(), true) != 0)
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + " " + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SavePermitImportTemplate();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
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
        /// Handles the Enter event of the F1011 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F28200_Enter(object sender, EventArgs e)
        {
            this.ParentForm.Activate();
        }


        private void ImportViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(23210);
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
        private void InputFileGridView_Enter(object sender, EventArgs e)
        {
            this.InputFileGridView.CurrentCell = this.InputFileGridView[1, 0];
            this.GridLoad = true;
        }
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