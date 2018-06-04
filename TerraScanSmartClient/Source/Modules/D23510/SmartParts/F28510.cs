//--------------------------------------------------------------------------------------------
// <copyright file="F28510.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Snapshot Import.
// </summary>
//----------------------------------------------------------------------------------------------
// History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//20180109          Priyadharshini     Author  
//*********************************************************************************/
namespace D23510
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
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

    [SmartPart]
    public partial class F28510 : BaseSmartPart
    {

        #region Variables

        private F28510Controller form28510Control;
        private int masterFormNo;
        private PermissionFields slicePermissionField;
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;


        private bool formMasterPermissionEdit;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        /// Enumerator Status Action
        /// </summary>
        public enum GridSortOrder
        {
            /// <summary>
            /// ASC = 0
            /// </summary>
            Asc = 0,

            /// <summary>
            /// DESC = 1
            /// </summary>
            Desc = 1
        }

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;
        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;
        /// <summary>
        /// previousImportId variable is used to store import id. 
        /// </summary>       
        private int currentImportId = -1;

        /// <summary>
        /// IDictionary contains position and width details of fields to be extracted
        /// </summary>
        private IDictionary importFileDetailsDictionary = new ListDictionary();

        /// <summary>
        /// import file contains permit import entry datatable
        /// </summary>
        private SnapshotImportFile importFileDataSet = new SnapshotImportFile();


        /// <summary>
        /// DataSet Contains Snapshot Import Details - importIds and importdetails
        /// </summary>
        private F23510SnapshotImport snapshotImportDataSet = new F23510SnapshotImport();

        /// <summary>
        /// DataTable Contains Error Record Details for an ImportId
        /// </summary>
        private F23510SnapshotImport.GetSnapshotImportDetailsDataTable errorCheckDataTable = new F23510SnapshotImport.GetSnapshotImportDetailsDataTable();
        /// <summary>
        /// variable contains mortgage import field structure instance
        /// </summary>
        private SnapshotImportFields SnapshotImportFieldsInstance = new SnapshotImportFields();
        /// <summary>
        /// import file sourcetype enum instance created
        /// </summary>
        private TerraScanCommon.ImportFileSourceType importFileSourceType;

        /// <summary>
        /// variable contains error grid sort order
        /// </summary>
        private GridSortOrder errorGridSortOrder;

        /// <summary>
        /// variable contains error grid sorted column
        /// </summary>
        private DataGridViewColumn errorGridSortedColumn;

        /// <summary>
        /// Gets or sets the error Grid Sort Order
        /// </summary>
        /// <value>The Error Grid Sort Order.</value>
        private GridSortOrder ErrorGridSortOrder
        {
            get { return this.errorGridSortOrder; }
            set { this.errorGridSortOrder = value; }
        }

        /// <summary>
        /// Gets or sets the error Grid Sorted Column
        /// </summary>
        /// <value>The Error Grid Sorted Column.</value>
        private DataGridViewColumn ErrorGridSortedColumn
        {
            get { return this.errorGridSortedColumn; }
            set { this.errorGridSortedColumn = value; }
        }



        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScan.Common.TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        /// <summary>
        /// gets or sets the Previous import id
        /// </summary>
        /// <value>The previous Import Id.</value>
        private int CurrentImportId
        {
            get { return this.currentImportId; }
            set { this.currentImportId = value; }
        }


        /// <summary>
        /// Gets or sets the SnapshotImportTemplateId
        /// </summary>
        /// <value>The SnapshotImportTemplateId.</value>
        public int SnapshotImportTemplateId;
        private int? ImportID;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SnapshotImportTemplateForm"/> class.
        /// </summary>
        /// 

        #region Constructor
        public F28510()
        {
            this.InitializeComponent();
        }

        public F28510(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.ImportID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.Tag = formNo;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.TemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TemplatePictureBox.Height, this.TemplatePictureBox.Width, "Files", this.redColor, this.greenColor, this.blueColor);
            this.TemplatePictureBox.SendToBack();
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion constructor

        private void F28510_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeErrorGridView();
                this.LoadHeaderTemplateDetails();
                this.TemplatePictureBox.SendToBack();
                this.EnableControls(true);
                this.EnablePanelControl(true);
                this.ErrorGridView.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ErrorGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this.ActiveControl = this.btnTemplateFile;
                this.btnTemplateFile.Focus();
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

        #region Event Publication

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for EnableButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationEntity>> EnableButtons;

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
        /// Occurs when [form slice_ null record mode].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_NullRecordMode;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;
        /// <summary>

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Set Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;


        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;
        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        #endregion Event Publication

        #region Property
        /// <summary>
        /// For F28210Control
        /// </summary>
        [CreateNew]
        public F28510Controller Form28510Control
        {
            get { return this.form28510Control as F28510Controller; }
            set { this.form28510Control = value; }
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

                    if (this.snapshotImportDataSet.Tables[0].Rows.Count > 0)
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
            this.LoadHeaderTemplateDetails();
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
                this.ImportID = eventArgs.Data.SelectedKeyId;

                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    this.ImportID = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.LoadHeaderTemplateDetails();

                }
                else if (this.ImportID == eventArgs.Data.SelectedKeyId)
                {
                    this.FlagSliceForm = true;
                    this.LoadHeaderTemplateDetails();
                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        #region File path selection
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

        #endregion
        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ImportFileButton.Enabled = false;
                this.CheckErrorButton.Enabled = false;
                this.CreateReceiptButton.Enabled = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Handles the Click Event for FilePathButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FilePathButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.FilePathOpenFileDialog.Filter = "Text Documents(*.txt)|*.txt|CSV(*.csv)|*.csv";
                if (!this.FilePathOpenFileDialog.ShowDialog().Equals(DialogResult.Cancel))
                {
                    this.FilePathTextBox.Text = this.FilePathOpenFileDialog.FileName.ToString();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion


        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.ImportFileButton.Enabled = false;
                this.CheckErrorButton.Enabled = false;
                this.CreateReceiptButton.Enabled = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        private void ControlLock(bool controlLook)
        {
            this.FilePathTextBox.LockKeyPress = controlLook;
        }

        private void ClearControls()
        {
            this.txtTemplateNameFile.Text = string.Empty;
            this.SourceTypeTextBox.Text = string.Empty;
            this.FilePathTextBox.Text = string.Empty;
            this.snapshotImportDataSet.GetSnapshotImportDetails.Clear();
            this.ErrorGridView.DataSource = this.snapshotImportDataSet.GetSnapshotImportDetails.DefaultView;
            this.ImportFileButton.Enabled = false;
            // this.FilePathButton.Enabled = false;
            this.ImportFileStatusLabel.Text = "Pending";
            this.ImportFileStatusLabel.ForeColor = Color.White;
            this.ImportFileStatusLabel.BackColor = System.Drawing.Color.Silver;
            this.ImportFileCountStatusLabel.Text = string.Empty;
            this.CheckErrorButton.Enabled = false;
            this.CheckErrorStatusLabel.Text = "Pending";
            this.CheckErrorStatusLabel.ForeColor = Color.White;
            this.CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;
            this.CheckErrorCountStatusLabel.Text = string.Empty;
            this.CreateReceiptButton.Enabled = false;
            this.CreateReceiptStatusLabel.Text = "Pending";
            this.CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
        }

        private void EnablePanelControl(bool enable)
        {
            this.DescriptionPanel.Enabled = enable;
            this.FilePathPanel.Enabled = enable;
            this.Errorpanel.Enabled = enable;
            this.panel1.Enabled = enable;
        }

        private void EnableControls(bool enable)
        {
            this.txtTemplateNameFile.Enabled = enable;
            this.FilePathButton.Enabled = enable;
            this.FilePathTextBox.Enabled = enable;
            this.btnTemplateFile.Enabled = enable;
            if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Count > 0)
            {
                if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                {
                    this.btnTemplateFile.Enabled = false;
                }
                else
                {
                    this.btnTemplateFile.Enabled = true;
                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString()))
                {

                    this.CreateReceiptButton.Enabled = false;
                    this.FilePathTextBox.Enabled = false;
                    this.ImportFileButton.Enabled = false;
                    this.CheckErrorButton.Enabled = false;
                    this.btnTemplateFile.Enabled = false;
                    this.FilePathButton.Enabled = false;
                }
            }





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

            if (string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
            {
                this.FilePathTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.txtTemplateNameFile.Text.Trim()))
            {
                this.txtTemplateNameFile.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            if (string.IsNullOrEmpty(this.SourceTypeTextBox.Text.Trim()))
            {
                this.SourceTypeTextBox.Focus();
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
                this.ClearControls();
                if (this.slicePermissionField.newPermission)
                {
                    this.snapshotImportDataSet = new F23510SnapshotImport();
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.btnTemplateFile.Focus();
                    this.FilePathButton.Enabled = true;
                    this.ErrorGridVscrollBar.Visible = true;
                }
                else
                {
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                }

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
                    this.Cursor = Cursors.WaitCursor;
                    this.form28510Control.WorkItem.F28510_DeleteSnapshotImportDetails(Convert.ToInt32(this.ImportID), TerraScanCommon.UserId);
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
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    if (this.ImportID == -99)
                    {
                        this.ImportID = null;
                    }
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.ImportID = null;
                    }
                    F28210PermitImport updateHeaderTemplateDetails = new F28210PermitImport();
                    F28210PermitImport.F28210_SaveHeaderTemplateDataTableRow newRow = updateHeaderTemplateDetails.F28210_SaveHeaderTemplateDataTable.NewF28210_SaveHeaderTemplateDataTableRow();

                    newRow.TemplateID = this.SnapshotImportFieldsInstance.TemplateId;
                    newRow.FilePath = this.FilePathTextBox.Text;

                    updateHeaderTemplateDetails.F28210_SaveHeaderTemplateDataTable.Rows.Add(newRow);
                    updateHeaderTemplateDetails.F28210_SaveHeaderTemplateDataTable.AcceptChanges();
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(updateHeaderTemplateDetails.F28210_SaveHeaderTemplateDataTable.Copy());
                    tempDataSet.Tables[0].TableName = "SnapshotImport";
                    this.CurrentImportId = this.form28510Control.WorkItem.F28510_InsertImportSnapshotDetails(ImportID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    if (this.CurrentImportId > 0)
                    {
                        //this.FillImportFormDetails(null, false);
                    }
                    else
                    {
                        MessageBox.Show("Error in save", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (this.CurrentImportId > 0)
                    {

                        SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                        sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceUpdateActiveRecord.SelectedKeyId = this.CurrentImportId; //Convert.ToInt32(this.centralID);
                        this.ImportID = this.CurrentImportId;
                        this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.CurrentImportId;//Convert.ToInt32(this.centralID);
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                    }
                    else
                    {
                        SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                        sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.ImportID);
                        //this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.ImportID);
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                    this.PermissionControlLock(!this.PermissionFiled.editPermission);
                }
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
                    this.ImportID = eventArgs.Data.SelectedKeyId;
                    this.LoadHeaderTemplateDetails();
                }
            }
        }
        #endregion

        private void ImportFileButton_Click(object sender, EventArgs e)
        {
            DialogResult dialougResult;
            TextReader textReader = null;
            string filePath = this.FilePathTextBox.Text;
            var fileName = Path.GetFileName(filePath);
            var path = filePath.Replace(fileName, "");
            var fileType = Path.GetExtension(filePath);
            bool isProcess = false;
            if (this.SnapshotImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                if (!(MessageBox.Show(SharedFunctions.GetResourceString("ImportFileMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    return;
                }
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text))
                {
                    filePath = this.FilePathTextBox.Text;
                }
                string tempValue = string.Empty;
                int lineIndex = 1;

                if (File.Exists(filePath))
                {
                    textReader = new StreamReader(File.OpenRead(filePath));
                }
                else
                {

                    MessageBox.Show(SharedFunctions.GetResourceString("ImportFileErrorMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.FilePathButton.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }

                ////fill importFileDetailsDictionary 
                this.GetImportFileInputDetails();
                ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.ProcessStatus, null, null);
                this.importFileDataSet.Clear();
                while (true)
                {
                    tempValue = textReader.ReadLine();

                    if (tempValue == null)
                    {
                        break;
                    }

                    if (!String.IsNullOrEmpty(tempValue.Trim()))
                    {
                        this.GenerateSnapshotImportEntryRecord(tempValue, lineIndex);
                    }

                    lineIndex++;
                }

                textReader.Close();

                string errorMessage = string.Empty;

                if (this.importFileDataSet.SnapshotImportEntry.Rows.Count > 0)
                {
                    this.importFileDataSet.SnapshotImportEntry.AcceptChanges();
                    DataSet ImportDetail = new DataSet("Imports");
                    ImportDetail.Tables.Add(this.importFileDataSet.SnapshotImportEntry.Copy());
                    ImportDetail.Tables[0].TableName = "Import";

                    string XML = ImportDetail.GetXml();
                    XML = "<Root>" + XML + "</Root>";
                    //XDoment doc = new XDocument(XML);
                    string resultSet = F28510WorkItem.F28510_ExecuteImport(this.SnapshotImportFieldsInstance.ImportId, XML, TerraScanCommon.UserId, isProcess);
                    if (!string.IsNullOrEmpty(resultSet) && !resultSet.Equals("No Message"))
                    {
                        dialougResult = MessageBox.Show(resultSet, "TerraScan – Snapshot Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialougResult == DialogResult.Yes)
                        {
                            isProcess = true;
                            resultSet = F28510WorkItem.F28510_ExecuteImport(this.SnapshotImportFieldsInstance.ImportId, XML, TerraScanCommon.UserId, isProcess);

                        }
                        else
                        {
                            this.LoadHeaderTemplateDetails();
                            return;
                        }

                    }
                    this.LoadHeaderTemplateDetails();
                }
                else
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("ImportFileEmpty"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        this.LoadHeaderTemplateDetails();
                        return;
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (System.IO.IOException ex)
            {
                if (MessageBox.Show("File is being used by another process.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            this.snapshotImportDataSet.GetSnapshotImportDetails.Clear();
            this.ErrorGridView.DataSource = this.snapshotImportDataSet.GetSnapshotImportDetails.DefaultView;
        }

        /// <summary>
        /// Method will Change the Buttons Property in the PermitImport Form with mode change
        /// </summary>
        private void ChangeStatusRelatedFields()
        {
            if (this.SnapshotImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                this.ImportFileCountStatusLabel.Text = String.Concat(this.SnapshotImportFieldsInstance.ImportedEntries, SharedFunctions.GetResourceString("ImportFileCountDisplay"));
                this.CheckErrorCountStatusLabel.Text = string.Concat(this.SnapshotImportFieldsInstance.ErrorEntries, " out of ", this.SnapshotImportFieldsInstance.ImportedEntries, " entries were in error");
            }
            else
            {
                if (this.SnapshotImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.ImportFileCountStatusLabel.Text = string.Empty;
                    this.FilePathTextBox.LockKeyPress = false || !this.PermissionEdit;
                    this.FilePathButton.Enabled = true && this.PermissionEdit;
                    this.btnTemplateFile.Enabled = true && this.PermissionEdit;
                    this.SnapshotImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    this.CheckErrorButton.Enabled = false;
                }
                else if (this.SnapshotImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    this.ImportFileCountStatusLabel.Text = String.Concat(this.SnapshotImportFieldsInstance.ImportedEntries, SharedFunctions.GetResourceString("ImportFileCountDisplay"));
                    this.FilePathTextBox.LockKeyPress = true;
                    this.FilePathButton.Enabled = false;
                    this.btnTemplateFile.Enabled = false;
                    this.CheckErrorButton.Enabled = true && this.PermissionEdit;
                }

                if (this.SnapshotImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.CheckErrorCountStatusLabel.Text = string.Empty;
                    this.SnapshotImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    this.CreateReceiptButton.Enabled = false;
                }
                else if (this.SnapshotImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    this.CheckErrorCountStatusLabel.Text = string.Concat(this.SnapshotImportFieldsInstance.ErrorEntries, " out of ", this.SnapshotImportFieldsInstance.ImportedEntries, " entries were in error");
                }

                if (this.SnapshotImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.SnapshotImportFieldsInstance.PrintReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                }
            }
        }

        /// <summary>
        /// Method will Change the Status Text,Background and foreground Color
        /// </summary>
        /// <param name="statusLabel">StatusLabel to Cahnge</param>
        /// <param name="actionMode">actionMode</param>
        /// <param name="runDt">The runDt contains process ended date and time</param>
        /// <param name="runBy">The runby contains the username</param>
        private static void ChangeStatusLabel(Label statusLabel, Enum actionMode, string runDt, string runBy)
        {
            if (actionMode.Equals(TerraScanCommon.StatusAction.BeforeStatus))
            {
                statusLabel.ForeColor = System.Drawing.Color.White;
                statusLabel.Text = "Pending";
                statusLabel.BackColor = System.Drawing.Color.Silver;
            }
            else if (actionMode.Equals(TerraScanCommon.StatusAction.ProcessStatus))
            {
                statusLabel.ForeColor = System.Drawing.Color.Black;
                statusLabel.Text = "Running..";
                statusLabel.BackColor = System.Drawing.Color.Silver;
            }
            else if (actionMode.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                DateTime tempDateTime;
                if (DateTime.TryParse(runDt.ToString(), out tempDateTime))
                {
                    runDt = String.Concat(tempDateTime.ToShortDateString(), " ", tempDateTime.ToShortTimeString().ToLower().Replace(" ", ""));
                }
                else
                {
                    runDt = String.Empty;
                }

                statusLabel.ForeColor = System.Drawing.Color.White;
                statusLabel.Text = String.Concat(runDt, " ", runBy);
                statusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            statusLabel.Refresh();
        }


        /// <summary>
        /// Fill Import Header Details
        /// </summary>
        /// <param name="tempDataSet">The temp data set.</param>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void FillImportFormDetails(F23510SnapshotImport tempDataSet, bool fetchNextRecord)
        {
            this.PageMode = TerraScan.Common.TerraScanCommon.PageModeTypes.View;
            this.Cursor = Cursors.WaitCursor;
            int recordIndex = 0;

            try
            {
                if (tempDataSet == null)
                {
                    this.snapshotImportDataSet = form28510Control.WorkItem.F28510_SnapshotImportDetails(Convert.ToInt32(this.currentImportId));
                }
                else
                {
                    this.snapshotImportDataSet = tempDataSet;
                }

                if (this.snapshotImportDataSet.Tables.Count > 0)
                {
                    //this.SetRecordCount(this, new DataEventArgs<int>(this.totalImportCount));
                    if (this.snapshotImportDataSet.Tables.Count > 1 && this.snapshotImportDataSet.GetSnapshotImportDetails.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Rows[0][this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.ImportIDColumn].ToString()))
                        {
                            this.CurrentImportId = Convert.ToInt32(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Rows[0][this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.ImportIDColumn]);
                        }
                        this.GetImportDetails();
                        int[] recordPointerArray = new int[2];
                        recordPointerArray[0] = recordIndex;
                        //recordPointerArray[1] = this.totalImportCount;
                        this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(recordPointerArray));
                    }
                    else
                    {
                        this.CurrentImportId = -1;
                        this.NullRecords = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
                else
                {
                    this.CurrentImportId = -1;
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                   
                }
            }
            catch (Exception)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Gets the Import details and fill PermitImportForm Header accordingly.
        /// </summary>        
        private void GetImportDetails()
        {
            F23510SnapshotImport snapshotImportDetailsDataSet = (F23510SnapshotImport)this.snapshotImportDataSet.Copy();
            string tempValue = String.Empty;
            this.SnapshotImportFieldsInstance = new SnapshotImportFields();
            this.ImportFileButton.Enabled = true && this.PermissionEdit;
            if (snapshotImportDetailsDataSet.Tables.Count > 0 && snapshotImportDetailsDataSet.GetSnapshotImportDetails.Rows.Count > 0)
            {
                this.SnapshotImportFieldsInstance.ImportId = Convert.ToInt32(snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.Rows[0][snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.ImportIDColumn]);
                this.SnapshotImportFieldsInstance.TemplateId = Convert.ToInt32(snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.Rows[0][snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.TemplateIDColumn]);
                this.txtTemplateNameFile.Text = snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.Rows[0][snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.TemplateNameColumn].ToString();
                this.SourceTypeTextBox.Text = snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.Rows[0][snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.TypeNameColumn].ToString();
                this.FilePathTextBox.Text = snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.Rows[0][snapshotImportDetailsDataSet.GetSnapshotImportHeaderDetails.FilePathColumn].ToString();
            }
        }

        /// <summary>
        /// Generate the permit import empty record for each line in the file
        /// </summary>
        /// <param name="fileLine">The line in the imported file</param>
        /// <param name="lineIndex">The index of the file line</param>
        private void GenerateSnapshotImportEntryRecord(string fileLine, int lineIndex)
        {
            try
            {
                //// used to store import file record details
                IDictionary snapshotImportDictionary = new ListDictionary();
                //// used to navigate through the dictionary
                IDictionaryEnumerator fileDetailsEnumerator = this.importFileDetailsDictionary.GetEnumerator();
                ////to store temporary values
                string tempValue = string.Empty;
                ////used to store the values in mortgage import entry table fields
                object fieldValue = null;
                ////temporary value used to store column name of the mortgage import entry table fields
                string columnName = string.Empty;
                ////temporary array
                string[] commaDelimitedArray = null;
                string[] arrValue = new string[2];

                int fieldWidth = -1;
                int position = -1;
                int stringLength = fileLine.Length;

                ////gets the position value for comma delimited files
                if (this.importFileSourceType.Equals(TerraScanCommon.ImportFileSourceType.CommaDelimited))
                {
                    string[] tempArr = fileLine.Split(new char[] { ',' });
                    Array.Resize(ref commaDelimitedArray, tempArr.Length);
                    tempArr.CopyTo(commaDelimitedArray, 0);
                }

                ////navigate to find the position and width of the mortgage import entry table fields
                while (fileDetailsEnumerator.MoveNext())
                {
                    ////clear temp values
                    tempValue = string.Empty;
                    fieldWidth = -1;
                    position = -1;

                    ////checks for null value 
                    if (!Object.Equals(fileDetailsEnumerator.Value, null))
                    {
                        columnName = fileDetailsEnumerator.Key.ToString();

                        ////split value - contains both position and width
                        arrValue = fileDetailsEnumerator.Value.ToString().Split(new char[] { '~' });
                        //arrValue = fileLine.Split(new char[] { ',' });
                        if (!String.IsNullOrEmpty(arrValue[0]))
                        {
                            position = Convert.ToInt16(arrValue[0]);
                        }

                        ////extract field value according to the source type
                        switch (this.importFileSourceType)
                        {
                            case TerraScanCommon.ImportFileSourceType.FixedWidth:
                                position = position - 1;
                                if (position > -1 && position < stringLength)
                                {
                                    if (arrValue.Length > 1)
                                    {
                                        if (!String.IsNullOrEmpty(arrValue[1]))
                                        {
                                            fieldWidth = Convert.ToInt16(arrValue[1]);
                                        }

                                        if (fieldWidth > -1)
                                        {
                                            if ((fieldWidth + position) > stringLength)
                                            {
                                                fieldWidth = stringLength - position;
                                            }

                                            tempValue = fileLine.Substring(position, fieldWidth);
                                        }
                                    }
                                }

                                break;
                            case TerraScanCommon.ImportFileSourceType.CommaDelimited:
                                if (position > 0 && position <= commaDelimitedArray.Length)
                                {
                                    tempValue = commaDelimitedArray.GetValue(position - 1).ToString();
                                }

                                break;
                        }

                        ////validate the values for datatype
                        fieldValue = ValidateFieldValue(tempValue.Trim(), this.importFileDataSet.SnapshotImportEntry.Columns[columnName].DataType);
                        ////adds columnname and corresponding values to temporary collection

                        snapshotImportDictionary[columnName] = fieldValue;

                    }
                }

                ////insert mortgage import entry record to the importfiledataset
                this.importFileDataSet.InsertSnapshotImportEntry(snapshotImportDictionary, lineIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Data given");
            }
        }
        /// <summary>
        /// Validate field value with datatype
        /// </summary>
        /// <param name="fieldValue">The field value to be validated</param>
        /// <param name="fieldDataType">The datatype to be used</param>
        /// <returns>returns validate value</returns>
        private static object ValidateFieldValue(string fieldValue, Type fieldDataType)
        {
            ////store validated value
            Object validatedFieldValue = null;
            int tempIntegerValue = 0;
            decimal tempDecimalValue = 0;
            DateTime tempDate = System.DateTime.Now;

            ////checks for integer datatype
            if (Type.Equals(fieldDataType, typeof(int)))
            {
                if (String.IsNullOrEmpty(fieldValue))
                {
                    validatedFieldValue = 0;
                }
                else
                {
                    int.TryParse(fieldValue, NumberStyles.Currency, null, out tempIntegerValue);
                    validatedFieldValue = tempIntegerValue;
                }
            }
            else if (Type.Equals(fieldDataType, typeof(decimal)))
            {
                ////checks for decimal datatype
                if (!decimal.TryParse(fieldValue, out tempDecimalValue))
                {
                    //For Fixing the 21918 : TSBG - D23210.F28210 Permit Import form - percent complete bug
                    validatedFieldValue = string.Empty;
                }
                else
                {
                    decimal.TryParse(fieldValue, NumberStyles.Currency, null, out tempDecimalValue);
                    validatedFieldValue = tempDecimalValue;
                }
            }
            else if (Type.Equals(fieldDataType, typeof(DateTime)))
            {
                ////checks for decimal datatype
                if (!DateTime.TryParse(fieldValue, out tempDate))
                {
                    validatedFieldValue = null;
                }
                else
                {
                    DateTime.TryParse(fieldValue, out tempDate);
                    validatedFieldValue = tempDate.ToLocalTime();
                }
            }
            else
            {
                validatedFieldValue = fieldValue;
            }

            return validatedFieldValue;
        }

        /// <summary>
        /// validate Permit import fields for saving the import record
        /// </summary>        
        private void ValidatePermitImportFields()
        {
            try
            {
                if (this.PageMode.Equals(TerraScan.Common.TerraScanCommon.PageModeTypes.New))
                {
                    this.SnapshotImportFieldsInstance.ImportId = -1;
                }
                else
                {
                    this.SnapshotImportFieldsInstance.ImportId = this.CurrentImportId;
                }
                this.SnapshotImportFieldsInstance.TemplateName = this.txtTemplateNameFile.Text.Trim();

                switch (this.SnapshotImportFieldsInstance.SourceTypeId)
                {
                    case 1:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.FixedWidth;
                        break;
                    case 2:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.CommaDelimited;
                        break;
                }

                ////string value checks for existing
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                {
                    this.SnapshotImportFieldsInstance.FilePath = this.FilePathTextBox.Text.Trim();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the input file details of the importing record
        /// </summary>
        private void GetImportFileInputDetails()
        {
            DataTable snapshotImportTemplateDataTable = F28510WorkItem.GetSnapshotImportTemplate(this.SnapshotImportFieldsInstance.TemplateId).GetSnapshotImportTemplate;

            if (snapshotImportTemplateDataTable.Rows.Count > 0)
            {
                this.SourceTypeTextBox.Text = snapshotImportTemplateDataTable.Rows[0]["TypeName"].ToString();
                if (!string.IsNullOrEmpty(snapshotImportTemplateDataTable.Rows[0]["TypeID"].ToString()))
                {
                    this.SnapshotImportFieldsInstance.SourceTypeId = Convert.ToInt16(snapshotImportTemplateDataTable.Rows[0]["TypeID"]);
                }

                switch (this.SnapshotImportFieldsInstance.SourceTypeId)
                {
                    case 1:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.FixedWidth;
                        break;
                    case 2:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.CommaDelimited;
                        break;
                }

                if (this.importFileSourceType.Equals(TerraScanCommon.ImportFileSourceType.FixedWidth))
                {
                    this.importFileDetailsDictionary[this.importFileDataSet.SnapshotImportEntry.ParcelNumberColumn] = String.Concat(snapshotImportTemplateDataTable.Rows[0]["ParcelNumber_Pos"], "~", snapshotImportTemplateDataTable.Rows[0]["ParcelNumber_Wid"]);

                }
                else
                {
                    this.importFileDetailsDictionary[this.importFileDataSet.SnapshotImportEntry.ParcelNumberColumn] = String.Concat(snapshotImportTemplateDataTable.Rows[0]["ParcelNumber_Pos"]);
                }  
            }
        }

        /// <summary>
        /// LoadHeaderTemplateDetails
        /// </summary>
        private void LoadHeaderTemplateDetails()
        {
            this.snapshotImportDataSet.Clear();
            this.snapshotImportDataSet = form28510Control.WorkItem.F28510_SnapshotImportDetails(Convert.ToInt32(this.ImportID));
            if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Rows.Count > 0)
            {
                this.txtTemplateNameFile.Visible = true;
                this.txtTemplateNameFile.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["TemplateName"].ToString(); //this.permitImportDataSet.GetPermitImportHeaderDetails[0]["TemplateName"].ToString();
                this.FilePathTextBox.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["FilePath"].ToString();
                this.SnapshotImportFieldsInstance.ImportId = Convert.ToInt32(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportID"]);
                this.SnapshotImportFieldsInstance.TemplateId = Convert.ToInt32(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["TemplateID"]);
                this.SourceTypeTextBox.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["TypeName"].ToString();
                if (this.snapshotImportDataSet.GetSnapshotImportDetails.Rows.Count > 0)
                {
                    this.CustomizeErrorGridView();
                    this.ErrorGridView.DataSource = this.snapshotImportDataSet.GetSnapshotImportDetails.DefaultView;
                    this.ErrorGridView.Refresh();

                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsImportEnabled"].ToString()))
                {
                    if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsImportEnabled"].ToString().ToLower().Equals("true"))
                    {
                        this.ImportFileButton.Enabled = true;
                        if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportStatus"].ToString()))
                        {

                            ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
                            ImportFileStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportStatus"].ToString();
                            ImportFileStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString()))
                            {
                                this.ImportFileCountStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString() + " file entries were imported";
                            }
                        }
                        else
                        {
                            ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
                            ImportFileStatusLabel.Text = "Pending";
                            ImportFileStatusLabel.BackColor = System.Drawing.Color.Silver;
                            this.ImportFileCountStatusLabel.Text = string.Empty;
                        }
                    }

                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsCheckForErrors"].ToString()))
                {
                    if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                    {
                        this.btnTemplateFile.Enabled = false;
                    }
                    else
                    {
                        this.btnTemplateFile.Enabled = true;
                    }
                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsCheckForErrors"].ToString()))
                {
                    if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                    {
                        this.CheckErrorButton.Enabled = true;

                        if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["CheckErrorStatus"].ToString()))
                        {
                            CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                            CheckErrorStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["CheckErrorStatus"].ToString();
                            CheckErrorStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"].ToString()))
                            {
                                var label = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"].ToString() + " out of " + this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString() + " entries were in error";
                                this.CheckErrorCountStatusLabel.Text = label.ToString();
                            }
                        }
                        else
                        {
                            CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                            CheckErrorStatusLabel.Text = "Pending";
                            CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;
                            this.CheckErrorCountStatusLabel.Text = string.Empty;
                        }
                    }
                    else
                    {
                        CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                        CheckErrorStatusLabel.Text = "Pending";
                        CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;
                        this.CheckErrorCountStatusLabel.Text = string.Empty;
                    }
                }
                else
                {
                    CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                    CheckErrorStatusLabel.Text = "Pending";
                    CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;
                    this.CheckErrorCountStatusLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsRecordsCreated"].ToString()))
                {
                    if (this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["IsRecordsCreated"].ToString().ToLower().Equals("true"))
                    {
                        if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["CheckErrorStatus"].ToString()))
                            {
                                CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                                CheckErrorStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["CheckErrorStatus"].ToString();
                                CheckErrorStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"].ToString()))
                                {
                                    var label = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"].ToString() + " out of " + this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString() + " entries were in error";
                                    this.CheckErrorCountStatusLabel.Text = label.ToString();
                                }
                            }
                            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportStatus"].ToString()))
                            {
                                ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
                                ImportFileStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportStatus"].ToString();
                                ImportFileStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString()))
                                {
                                    this.ImportFileCountStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"].ToString() + " file entries were imported";
                                }
                            }

                            CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                            CreateReceiptStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString();
                            CreateReceiptStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                        }
                        else
                        {
                            this.CreateReceiptButton.Enabled = false;
                            CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                            CreateReceiptStatusLabel.Text = "Pending";
                            CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                        }
                        if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"].ToString()))
                        {
                            if ((Convert.ToInt32(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["EntriesInError"])) < (Convert.ToInt32(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["ImportedEntries"])))
                            {

                                this.CreateReceiptButton.Enabled = true;
                            }
                            else
                            {
                                this.CreateReceiptButton.Enabled = false;
                            }
                        }
                        else
                        {
                            this.CreateReceiptButton.Enabled = false;
                            CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                            CreateReceiptStatusLabel.Text = "Pending";
                            CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                        }

                    }
                    else
                    {
                        this.CreateReceiptButton.Enabled = false;
                        CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                        CreateReceiptStatusLabel.Text = "Pending";
                        CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                    }
                }
                else
                {
                    this.CreateReceiptButton.Enabled = false;
                    CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                    CreateReceiptStatusLabel.Text = "Pending";
                    CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                }

                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Rows[0]["TemplateID"].ToString()))
                {
                    this.SnapshotImportFieldsInstance.TemplateId = Convert.ToInt16(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails.Rows[0]["TemplateID"]);
                }
                if (snapshotImportDataSet.Tables.Count > 1 && snapshotImportDataSet.GetSnapshotImportDetails.Rows.Count > 0)
                {
                    this.FillCheckErrorDataTable(snapshotImportDataSet.GetSnapshotImportDetails);
                }
                else
                {
                    this.FillCheckErrorDataTable(null);
                }
                if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString()))
                {

                    this.CreateReceiptButton.Enabled = false;
                    this.FilePathTextBox.Enabled = false;
                    this.ImportFileButton.Enabled = false;
                    this.CheckErrorButton.Enabled = false;
                    this.btnTemplateFile.Enabled = false;
                    this.FilePathButton.Enabled = false;
                }
                else
                {
                    this.FilePathTextBox.Enabled = true;
                    this.FilePathButton.Enabled = true;
                }
            }
            else
            {
                this.ClearControls();
                this.EnableControls(false);
                this.EnablePanelControl(false);
            }
        }

        /// <summary>
        /// Filldts the Check Errors.
        /// </summary>
        /// <param name="tempErrorCheckDataTable">The dt check error grid.</param>
        private void FillCheckErrorDataTable(F23510SnapshotImport.GetSnapshotImportDetailsDataTable tempErrorCheckDataTable)
        {
            try
            {
                ////reset error grid related values
                this.ErrorGridView.CurrentCell = null;
                this.ErrorGridSortedColumn = null;
                this.ErrorGridSortOrder = GridSortOrder.Asc;
                this.ErrorGridView.ClearSorting();

                if (tempErrorCheckDataTable != null && tempErrorCheckDataTable.Rows.Count > 0)
                {
                    this.errorCheckDataTable.Clear();
                    for (int counter = 0; counter < tempErrorCheckDataTable.Rows.Count; counter++)
                    {
                        F23510SnapshotImport.GetSnapshotImportDetailsRow newRow = (F23510SnapshotImport.GetSnapshotImportDetailsRow)this.errorCheckDataTable.NewRow();
                        newRow.ParcelNumber = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.ParcelNumberColumn].ToString().Trim();
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.RollYearColumn].ToString()))
                        {
                            newRow.RollYear = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.RollYearColumn]);
                        }
                        newRow.PermitNumber = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.PermitNumberColumn].ToString().Trim();
                        newRow.ErrorStatus = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.ErrorStatusColumn].ToString().Trim();
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.LineColumn].ToString()))
                        {
                            newRow.Line = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.LineColumn]);
                        }
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.EntryIDColumn].ToString()))
                        {
                            newRow.EntryID = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.EntryIDColumn]);
                        }
                        this.errorCheckDataTable.Rows.Add(newRow);
                    }
                }
                else
                {
                    DataView tempDataView = new DataView(this.errorCheckDataTable.DefaultView.ToTable());
                    tempDataView.RowFilter = string.Concat(this.errorCheckDataTable.LineColumn.ColumnName, "> 0");
                    if (tempDataView.Count > 0)
                    {
                        this.errorCheckDataTable.Clear();
                    }
                }

                this.ErrorGridView.CreateEmptyRows();
                this.ErrorGridView.CurrentCell = null;

                if (this.errorCheckDataTable.Rows.Count > 5)
                {
                    this.ErrorGridVscrollBar.Visible = false;
                }
                else
                {
                    this.ErrorGridVscrollBar.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Template button click.
        /// </summary>
        /// <param name="btnTemplateFile_Click">Template button click.</param>
        private void btnTemplateFile_Click(object sender, EventArgs e)
        {
            try
            {
                this.SnapshotImportTemplateId = 0;
                Form snapshotImportTemplateSelect = new Form();
                snapshotImportTemplateSelect = this.form28510Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2351, null, this.form28510Control.WorkItem);
                if (snapshotImportTemplateSelect != null)
                {
                    if (snapshotImportTemplateSelect.ShowDialog() == DialogResult.Ignore)
                    {
                        try
                        {
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(2351);
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                    }
                    this.SnapshotImportTemplateId = Convert.ToInt32(TerraScanCommon.GetValue(snapshotImportTemplateSelect, "SnapshotImportTemplateId"));
                    if (this.SnapshotImportTemplateId > 0)
                    {
                        this.EditEnabled();
                        this.FillTemplateDetails(this.SnapshotImportTemplateId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Get Template Details and fill it to the corresponding fields
        /// </summary>
        /// <param name="templateId">the templateid</param>
        private void FillTemplateDetails(int templateId)
        {
            F23500SnapshotTemplate snapshotImportTemplateDataSet = F28510WorkItem.GetSnapshotImportTemplate(templateId);
            if (snapshotImportTemplateDataSet.Tables.Count > 0 && snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows.Count > 0)
            {
                this.txtTemplateNameFile.Text = snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TemplateName"].ToString();
                this.SourceTypeTextBox.Text = snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TypeName"].ToString();
                this.FilePathTextBox.Text = snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["FilePath"].ToString();
                if (!string.IsNullOrEmpty(snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TypeID"].ToString()))
                {
                    this.SnapshotImportFieldsInstance.SourceTypeId = Convert.ToInt16(snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TypeID"]);
                }
                if (!string.IsNullOrEmpty(snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TemplateID"].ToString()))
                {
                    this.SnapshotImportFieldsInstance.TemplateId = Convert.ToInt16(snapshotImportTemplateDataSet.GetSnapshotImportTemplate.Rows[0]["TemplateID"]);
                }
            }
        }

        /// <summary>
        /// CheckErrorButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CheckErrorButton_Click(object sender, EventArgs e)
        {
            try
            {
                MortageImportData tempDataSet = new MortageImportData();
                this.Cursor = Cursors.WaitCursor;
                ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.ProcessStatus, null, null);
                this.snapshotImportDataSet.GetSnapshotImportDetails.Clear();
                this.ErrorGridView.DataSource = this.snapshotImportDataSet.GetSnapshotImportDetails.DefaultView;
                F28510WorkItem.F28510_ExecuteCheckForErrors(this.SnapshotImportFieldsInstance.ImportId, TerraScanCommon.UserId);
                this.LoadHeaderTemplateDetails();
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
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizeErrorGridView()
        {
            this.ErrorGridView.AllowUserToResizeColumns = false;
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.AllowUserToResizeRows = false;
            this.ErrorGridView.StandardTab = true;
            this.ErrorGridView.EnableHeadersVisualStyles = false;
            DataGridViewColumnCollection columns = this.ErrorGridView.Columns;

            columns["ParcelNumber"].DataPropertyName = "ParcelNumber";
            columns["ErrorStatus"].DataPropertyName = "ErrorStatus";
            columns["Line"].DataPropertyName = "Line";
            columns["EntryId"].DataPropertyName = "EntryId";


            columns["ParcelNumber"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns["ErrorStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns["Line"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns["EntryId"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            columns["ParcelNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns["ErrorStatus"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columns["Line"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columns["EntryId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ErrorGridView.DataSource = this.errorCheckDataTable.DefaultView;
        }

        /// <summary>
        /// CreateReceiptButton_Click
        /// </summary>
        private void CreateReceiptButton_Click(object sender, EventArgs e)
        {
            DialogResult dialougResult;
            string returnString;
            bool isProcess = false;
            CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.Black;
            CreateReceiptStatusLabel.Text = "Running..";
            CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
            returnString = F28510WorkItem.F28510_CreateImportRecords(Convert.ToInt32(ImportID), TerraScanCommon.UserId, isProcess);
            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString()))
            {
                CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                CreateReceiptStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString();
                CreateReceiptStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }
            if (!string.IsNullOrEmpty(returnString) && !returnString.Equals("No Message"))
            {
                dialougResult = MessageBox.Show(returnString, "TerraScan – Snapshot Import Create Records", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialougResult == DialogResult.Yes)
                {
                    isProcess = true;
                    returnString = F28510WorkItem.F28510_CreateImportRecords(Convert.ToInt32(ImportID), TerraScanCommon.UserId, isProcess);
                }
                else
                {
                    CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                    this.CreateReceiptStatusLabel.Text = "Pending";
                    this.CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                }
            }

            this.LoadHeaderTemplateDetails();
            if (!string.IsNullOrEmpty(this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString()))
            {
                CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                CreateReceiptStatusLabel.Text = this.snapshotImportDataSet.GetSnapshotImportHeaderDetails[0]["RecordsCreateStatus"].ToString();
                CreateReceiptStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                this.CreateReceiptButton.Enabled = false;
                this.FilePathButton.Enabled = false;
                this.FilePathTextBox.Enabled = false;
                this.ImportFileButton.Enabled = false;
                this.CheckErrorButton.Enabled = false;
            }
        }

        /// <summary>
        /// txtTemplateNameFile_LinkClicked
        /// </summary>
        private void txtTemplateNameFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(23500);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = Convert.ToInt64(this.SnapshotImportFieldsInstance.TemplateId);
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
        /// ErrorGridView_KeyDown
        /// </summary>
        private void ErrorGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (this.ErrorGridView.CurrentRowIndex >= 0)
                {
                    if (this.ErrorGridView.CurrentColumnIndex.Equals(1))
                    {
                        if (!string.IsNullOrEmpty(this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.snapshotImportDataSet.GetSnapshotImportDetails.ParcelNumberColumn.ColumnName].Value.ToString()))
                        {
                            string tempParcelId = this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.snapshotImportDataSet.GetSnapshotImportDetails.ParcelNumberColumn.ColumnName].Value.ToString();
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(30080);
                            formInfo.optionalParameters = new object[1];
                            formInfo.optionalParameters[0] = tempParcelId;
                            tempParcelId = "74";
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ErrorGridView_CellMouseClick
        /// </summary>
        private void ErrorGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(1))
            {
                if (this.ErrorGridView.CurrentColumnIndex.Equals(1))
                {
                }
            }
        }

    }
}

