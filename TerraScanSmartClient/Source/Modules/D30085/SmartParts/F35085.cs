namespace D30085
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
    using System.IO;
    using System.Data.OleDb;
    using System.Linq;
    using System.Xml.Linq;

    [SmartPart]
    public partial class F35085 : BaseSmartPart
    {

        #region Variables

        private F35085Controller form35085Control;
        private int masterFormNo;
        private PermissionFields slicePermissionField;
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;
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
        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        private int? ImportID;
        private bool flagLoadOnProcess;
        private F35085CentrallyAssessedImportData.F35085_ListComboImportTypesDataTable ImportComboDataSet = new F35085CentrallyAssessedImportData.F35085_ListComboImportTypesDataTable();
        F35085CentrallyAssessedImportData ImportHeaderDataSet = new F35085CentrallyAssessedImportData();
        #endregion

        public F35085(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.ImportID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.TemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TemplatePictureBox.Height, this.TemplatePictureBox.Width, "Template", this.redColor, this.greenColor, this.blueColor);
            this.TemplatePictureBox.SendToBack();
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void F35085_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadComboBox();
                this.LoadHeaderTemplateDetails();
                this.CustomizeErrorGridView();
                this.TemplatePictureBox.SendToBack();
                this.flagLoadOnProcess = false;
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
        /// For F25000Control
        /// </summary>
        [CreateNew]
        public F35085Controller Form35085Control
        {
            get { return this.form35085Control as F35085Controller; }
            set { this.form35085Control = value; }
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

                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows.Count > 0)
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
            //this.ClearControls();
            this.LoadComboBox();
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
            //Checks the masterform no is same  
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.ImportID = eventArgs.Data.SelectedKeyId;
                this.flagLoadOnProcess = true;
                this.FlagSliceForm = true;
                this.LoadComboBox();
                this.LoadHeaderTemplateDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;

            }

        }
        /// <summary>
        /// Handles the Leave event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {
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
            //}
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
        /// <summary>
        /// Handles the Click Event for FilePathButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FilePathButton_Click(object sender, EventArgs e)
        {
            try
            {
                //this.FilePathOpenFileDialog.Filter = "Text Documents(*.txt)|*.txt|CSV(*.csv)|*.csv|All Files(*.*)|*.*";
                this.FilePathOpenFileDialog.Filter = "DBF Files(*.dbf)|*.dbf|CSV(*.csv)|*.csv";
                //this.FilePathOpenFileDialog.Filter = "DBF and XML Files(*.dbf,*.xml)|*.dbf;*.xml";
                if (!this.FilePathOpenFileDialog.ShowDialog().Equals(DialogResult.Cancel))
                {
                    //this.ChangeMortgageImportStatus();
                    this.FilePathTextBox.Text = this.FilePathOpenFileDialog.FileName.ToString();
                    //this.FocusRequiredInputField(this.FilePathTextBox, true, true);
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
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }
        /// <summary>
        /// Change mortgage import form status
        /// </summary>       
        private void ChangeMortgageImportStatus()
        {
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }
        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            //this.ImportTypeComboBox.Enabled = !controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.FilePathTextBox.LockKeyPress = controlLook;
            this.ErrorGridView.Enabled = controlLook;
        }

        private void ClearControls()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.ImportTypeComboBox.SelectedIndex = -1;
            this.DescriptionTextBox.Text = string.Empty;
            this.FilePathTextBox.Text = string.Empty;
            this.ImportHeaderDataSet.F35085_Get_GridDataTable.Clear();
            this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
            this.ImportFileButton.Enabled = false;
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
            this.RollYearPanel.Enabled = enable;
            this.ImportPanel.Enabled = enable;
            this.DescriptionPanel.Enabled = enable;
            this.FilePathPanel.Enabled = enable;
            this.Errorpanel.Enabled = enable;
            this.ErrorGridPanel.Enabled = enable;
            //this.ImportFileButton.Enabled = enable;
            //this.CheckErrorButton.Enabled = enable;
            //this.CreateReceiptButton.Enabled = enable;
        }

        private void EnableControls(bool enable)
        {
            this.RollYearTextBox.Enabled = enable;
            this.ImportTypeComboBox.Enabled = enable;
            this.DescriptionTextBox.Enabled = enable;
            this.FilePathTextBox.Enabled = enable;
            this.ErrorGridView.Enabled = enable;
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

            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                this.RollYearTextBox.Focus();
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
            if (string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
            {
                this.FilePathTextBox.Focus();
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

                    this.LoadComboBox();
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);

                    this.RollYearTextBox.Focus();
                    this.FilePathButton.Enabled = true;
                    this.ImportTypeComboBox.SelectedIndex = -1;
                    this.ErrorGridView.Enabled = true;

                }
                else
                {
                    this.EnableControls(false);
                    this.EnablePanelControl(false);
                    this.FilePathButton.Enabled = false;
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
                    this.form35085Control.WorkItem.F35085_DeletetemplateDetails(Convert.ToInt32(this.ImportID), TerraScanCommon.UserId);
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
                string filePath = this.FilePathTextBox.Text;
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text))
                {
                    filePath = this.FilePathTextBox.Text;
                }
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    if (this.ImportID == -99)
                    {
                        this.ImportID = null;
                    }
                    F35085CentrallyAssessedImportData updateHeaderTemplateDetails = new F35085CentrallyAssessedImportData();
                    F35085CentrallyAssessedImportData.F35085_SaveHeaderTemplateDataTableRow dr = updateHeaderTemplateDetails.F35085_SaveHeaderTemplateDataTable.NewF35085_SaveHeaderTemplateDataTableRow();

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.ImportID = null;
                    }
                    dr.Rollyear = this.RollYearTextBox.Text;
                    dr.FilePath = this.FilePathTextBox.Text;
                    dr.Description = this.DescriptionTextBox.Text;

                    if (!string.IsNullOrEmpty(this.ImportTypeComboBox.Text))
                    {
                        dr.PropertyClassID = this.ImportTypeComboBox.SelectedValue.ToString();

                    }

                    updateHeaderTemplateDetails.F35085_SaveHeaderTemplateDataTable.Rows.Add(dr);
                    updateHeaderTemplateDetails.F35085_SaveHeaderTemplateDataTable.AcceptChanges();
                    DataSet tempDataSet = new DataSet("root");
                    tempDataSet.Tables.Add(updateHeaderTemplateDetails.F35085_SaveHeaderTemplateDataTable.Copy());
                    tempDataSet.Tables[0].TableName = "Import";
                    int returnValue;
                    //if (centralID > 0)
                    //{

                    DataSet ds = this.form35085Control.WorkItem.F35085_InsertCentralTemplateDetails(ImportID, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                        {
                            returnValue = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                            SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                            sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceUpdateActiveRecord.SelectedKeyId = returnValue; //Convert.ToInt32(this.centralID);
                            this.ImportID = returnValue;
                            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                            SliceReloadActiveRecord sliceReloadActiveRecord;
                            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            sliceReloadActiveRecord.SelectedKeyId = returnValue;//Convert.ToInt32(this.centralID);
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                        else if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0][1].ToString()))
                        {
                            string message = ds.Tables[0].Rows[0][1].ToString();
                            MessageBox.Show(message, "Terrascan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                            //sliceEventArgs.MasterFormNo = this.masterFormNo;
                            //sliceEventArgs.AllowNullRecordMode = false;
                            //sliceEventArgs.WithoutKeyId = false;
                            //this.FormSlice_NullRecordMode(this, new DataEventArgs<int>(this.masterFormNo));
                            //this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));

                            //SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                            //sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                            //sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.ImportID);
                            ////this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                            //this.SetEditRecord();
                            //SliceReloadActiveRecord sliceReloadActiveRecord;
                            //sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                            //sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.ImportID);
                            //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            //this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }

                    }
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

        private void LoadComboBox()
        {
            this.ImportComboDataSet = form35085Control.WorkItem.F35085_ImportTypeCombo().F35085_ListComboImportTypes;
            if (ImportComboDataSet.Rows.Count > 0)
            {
                this.ImportTypeComboBox.DataSource = this.ImportComboDataSet;
                this.ImportTypeComboBox.DisplayMember = this.ImportComboDataSet.PropertyClassColumn.ColumnName;
                this.ImportTypeComboBox.ValueMember = this.ImportComboDataSet.PropertyClassIDColumn.ColumnName;
            }
        }

        private void LoadHeaderTemplateDetails()
        {
            this.ImportHeaderDataSet.Clear();
            this.ImportHeaderDataSet = form35085Control.WorkItem.F35085_CentralAssessedImportDetails(Convert.ToInt32(this.ImportID));
            if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows.Count > 0)
            {
                this.RollYearTextBox.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["RollYear"].ToString();
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows[0]["PropertyClassID"].ToString()))
                {
                    this.ImportTypeComboBox.SelectedValue = this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows[0]["PropertyClassID"].ToString();
                }
                this.DescriptionTextBox.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["Description"].ToString();
                this.FilePathTextBox.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["FilePath"].ToString();
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString().ToLower().Equals("true"))
                    {
                        this.RollYearTextBox.Enabled = false;
                        this.ImportTypeComboBox.Enabled = false;
                        this.DescriptionTextBox.Enabled = false;
                    }
                    else
                    {
                        this.RollYearTextBox.Enabled = true;
                        this.ImportTypeComboBox.Enabled = true;
                        this.DescriptionTextBox.Enabled = true;
                    }
                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                    {
                        this.FilePathTextBox.Enabled = false;
                        this.FilePathButton.Enabled = false;
                    }
                    else
                    {
                        this.FilePathTextBox.Enabled = true;
                        this.FilePathButton.Enabled = true;
                    }
                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsImportEnabled"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsImportEnabled"].ToString().ToLower().Equals("true"))
                    {
                        this.ImportFileButton.Enabled = true;
                        if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportStatus"].ToString()))
                        {
                            ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
                            ImportFileStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportStatus"].ToString();
                            ImportFileStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString()))
                            {
                                this.ImportFileCountStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString() + " file entries were imported";
                            }
                        }
                    }
                    else
                    {
                        this.ImportFileButton.Enabled = false;
                    }
                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                    {
                        this.CheckErrorButton.Enabled = true;
                        if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["CheckErrorStatus"].ToString()))
                        {
                            CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                            CheckErrorStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["CheckErrorStatus"].ToString();
                            CheckErrorStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                            if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString()))
                            {
                                var label = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString() + " out of " + this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString() + " entries were in error";
                                this.CheckErrorCountStatusLabel.Text = label.ToString();
                            }
                        }
                    }
                    else
                    {
                        this.CheckErrorButton.Enabled = false;
                    }
                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString().ToLower().Equals("true"))
                    {
                        if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString()))
                        {
                            if ((Convert.ToInt32(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"])) < (Convert.ToInt32(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"])))
                            {
                                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString()))
                                {
                                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                                    {
                                        this.CreateReceiptButton.Enabled = true;
                                    }
                                }
                                
                            }
                            else
                            {
                                this.CreateReceiptButton.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        this.CreateReceiptButton.Enabled = false;
                    }
                }
                //if (this.ImportHeaderDataSet.F35085_Get_GridDataTable.Rows.Count > 0)
                //{
                //    this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
                //}

                //if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                ////if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                //{
                //    this.EnablePanelControl(true);
                //    this.EnableControls(true);
                //}
                //else
                //{
                //    this.EnablePanelControl(false);
                //    this.EnableControls(false);
                //}
                this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
            }
            else
            {
                this.ClearControls();
                this.EnableControls(false);
                this.EnablePanelControl(false);
            }

        }
        private void CustomizeErrorGridView()
        {
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.ParcelNumberColumn.ColumnName;

            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.DescriptionColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DescriptionColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.PersonalPropertyColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.PersonalPropertyColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.RealPropertyColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.RealPropertyColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.ErrorStatusColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.ErrorStatusColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.LineColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.LineColumn.ColumnName;
            this.ErrorGridView.Columns[this.ImportHeaderDataSet.F35085_Get_GridDataTable.EntryIDColumn.ColumnName].DataPropertyName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.EntryIDColumn.ColumnName;

            
            
            //this.ErrorGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.ErrorGridView.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // this.ErrorGridView.PrimaryKeyColumnName = this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName.ToString();

            this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
        }
       
       

        private void ImportFileButton_Click(object sender, EventArgs e)
        {
            DialogResult dialougResult;
            string importXML = null;
            string filePath = this.FilePathTextBox.Text;
            var fileName = Path.GetFileName(filePath);
            var path = filePath.Replace(fileName, "");
            var fileType = Path.GetExtension(filePath);
            bool isProcess = false;
            DataSet ds = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text))
                {
                    filePath = this.FilePathTextBox.Text;
                }
                if (!File.Exists(filePath))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ImportFileErrorMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.FilePathButton.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (fileType.Equals(".DBF"))
                {
                    DataTable dt = new DataTable();
                    string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"" + path + "\";Extended Properties=dBASE IV";
                    using (OleDbConnection con = new OleDbConnection(constr))
                    {
                        var sql = "select * from " + fileName;
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        con.Open();
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(ds);
                        ds.DataSetName = "root";
                        ds.Tables[0].TableName = "CentrallyAssessedImport";
                        if (ds.Tables.Count > 0)
                        {
                            importXML = ds.GetXml();
                        }
                    }
                }

                if (fileType.Equals(".csv"))
                {
                    string ConStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}" + ";Extended Properties=\"Text;HDR={1};IMEX=1;FMT=Delimited\\\"", path, true ? "Yes" : "No");
                    string SQL = string.Format("SELECT * FROM {0}", fileName);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConStr);
                    adapter.Fill(ds);
                    ds.Tables[0].Rows.Clear();
                    ConStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}" + ";Extended Properties=\"Text;HDR={1};IMEX=1;FMT=Delimited\\\"", path, true ? "No" : "No");
                    SQL = string.Format("SELECT * FROM {0}", fileName);
                    adapter = new OleDbDataAdapter(SQL, ConStr);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows.RemoveAt(0);
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            dt.Columns[i].ColumnName = ds.Tables[0].Columns[i].ColumnName;
                            dt.AcceptChanges();
                        }
                    }
                    ds.Tables.Clear();
                    ds.Tables.Add(dt);
                    ds.DataSetName = "root";
                    ds.Tables[0].TableName = "CentrallyAssessedImport";
                    if (ds.Tables.Count > 0)
                    {
                        importXML = ds.GetXml();
                    }
                }
               
                ImportFileStatusLabel.ForeColor = System.Drawing.Color.Black;
                ImportFileStatusLabel.Text = "Running..";
                ImportFileStatusLabel.BackColor = System.Drawing.Color.Silver;
                string resultSet = form35085Control.WorkItem.F35085_ExecuteImport(Convert.ToInt32(ImportID), importXML, TerraScanCommon.UserId, isProcess);
                if (!string.IsNullOrEmpty(resultSet) && !resultSet.Equals("No Message"))
                {
                    dialougResult = MessageBox.Show(resultSet, "Terrascan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialougResult == DialogResult.Yes)
                    {
                        isProcess = true;
                        resultSet = form35085Control.WorkItem.F35085_ExecuteImport(Convert.ToInt32(ImportID), importXML, TerraScanCommon.UserId, isProcess);
                    }
                    //else
                    //{
                    //    this.LoadHeaderTemplateDetails();
                    //}
                }
                this.LoadHeaderTemplateDetails();
                if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportStatus"].ToString()))
                    {
                        ImportFileStatusLabel.ForeColor = System.Drawing.Color.White;
                        ImportFileStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportStatus"].ToString();
                        ImportFileStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                    }
                    if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString()))
                    {
                        this.ImportFileCountStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString() + " file entries were imported";
                    }

                    if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString()))
                    {
                        if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsCheckForErrors"].ToString().ToLower().Equals("true"))
                        {
                            this.CheckErrorButton.Enabled = true;
                        }
                        else
                        {
                            this.CheckErrorButton.Enabled = false;
                        }
                    }
                }
            }
             
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (System.IO.IOException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already opened"))
                {
                    if (MessageBox.Show("File is being used by another process.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
            finally
            {
               
            }

            //To clear the grid content
            this.ErrorGridView.Enabled = true;
            this.ImportHeaderDataSet.F35085_Get_GridDataTable.Clear();
            this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
        }
        /// <summary>
        /// For handling Sql injection
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        private static Boolean CheckInputParams(string userInput)
        {
            bool isValid = false;
            var sqlCheckList = new HashSet<string> { "';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
                                       "';xp_","';restore","';backup","';--","';sp_executesql","';rename","';kill","';1=1","';" };
            isValid = sqlCheckList.Any(userInput.ToLower().Contains);
            return isValid;
        }
        private void CheckErrorButton_Click(object sender, EventArgs e)
        {
            CheckErrorStatusLabel.ForeColor = System.Drawing.Color.Black;
            CheckErrorStatusLabel.Text = "Running..";
            CheckErrorStatusLabel.BackColor = System.Drawing.Color.Silver;

            //Modifed to clear the grid content 
            this.ImportHeaderDataSet.F35085_Get_GridDataTable.Clear();
            this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;

            form35085Control.WorkItem.F35085_ExecuteCheckForErrors(Convert.ToInt32(ImportID), TerraScanCommon.UserId);
            this.LoadHeaderTemplateDetails();
            if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["CheckErrorStatus"].ToString()))
                {
                    CheckErrorStatusLabel.ForeColor = System.Drawing.Color.White;
                    CheckErrorStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["CheckErrorStatus"].ToString();
                    CheckErrorStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString()))
                {
                    var label = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"].ToString() + " out of " + this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString() + " entries were in error";
                    this.CheckErrorCountStatusLabel.Text = label.ToString();
                }
                if (this.ImportHeaderDataSet.F35085_Get_GridDataTable.Rows.Count > 0)
                {
                    this.CustomizeErrorGridView();
                    this.ErrorGridView.DataSource = this.ImportHeaderDataSet.F35085_Get_GridDataTable.DefaultView;
                    this.ErrorGridView.Refresh();

                }
                if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString()))
                {
                    if (this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["IsRecordsCreated"].ToString().ToLower().Equals("true"))
                    {
                        if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"].ToString()))
                        {
                            if ((Convert.ToInt32(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["EntriesInError"])) < (Convert.ToInt32(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["ImportedEntries"])))
                            {
                                this.CreateReceiptButton.Enabled = true;
                            }
                            else
                            {
                                this.CreateReceiptButton.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        this.CreateReceiptButton.Enabled = false;
                    }
                }
            }

        }

        private void ErrorGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && e.ColumnIndex.Equals(1))
            {
                if (this.ErrorGridView.CurrentColumnIndex.Equals(1))
                {
                    if (!string.IsNullOrEmpty(this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.ParcelNumberColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName].Value.ToString()))
                    {
                        string tempParcelId = this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName].Value.ToString();
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


        private void ErrorGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (this.ErrorGridView.CurrentRowIndex >= 0)
                {
                    if (this.ErrorGridView.CurrentColumnIndex.Equals(1))
                    {
                        if (!string.IsNullOrEmpty(this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.ParcelNumberColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName].Value.ToString()))
                        {
                            string tempParcelId = this.ErrorGridView.Rows[this.ErrorGridView.CurrentRowIndex].Cells[this.ImportHeaderDataSet.F35085_Get_GridDataTable.CentralIDColumn.ColumnName].Value.ToString();
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

        private void CreateReceiptButton_Click(object sender, EventArgs e)
        {
            DialogResult dialougResult;
            string returnString;
            bool isProcess = false;
            CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.Black;
            CreateReceiptStatusLabel.Text = "Running..";
            CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
            returnString = form35085Control.WorkItem.F35085_CreateImportRecords(Convert.ToInt32(ImportID), TerraScanCommon.UserId, isProcess);
            if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["RecordsCreateStatus"].ToString()))
            {
                CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                CreateReceiptStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["RecordsCreateStatus"].ToString();
                CreateReceiptStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }

            if (!string.IsNullOrEmpty(returnString) && !returnString.Equals("No Message"))
            {
                dialougResult = MessageBox.Show(returnString, "Terrascan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialougResult == DialogResult.Yes)
                {
                    isProcess = true;
                    returnString = form35085Control.WorkItem.F35085_CreateImportRecords(Convert.ToInt32(ImportID), TerraScanCommon.UserId, isProcess);
                }
                else
                {
                    CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                    this.CreateReceiptStatusLabel.Text = "Pending";
                    this.CreateReceiptStatusLabel.BackColor = System.Drawing.Color.Silver;
                    //this.LoadHeaderTemplateDetails();
                }
            }

            this.LoadHeaderTemplateDetails();
            if (!string.IsNullOrEmpty(this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["RecordsCreateStatus"].ToString()))
            {
                CreateReceiptStatusLabel.ForeColor = System.Drawing.Color.White;
                CreateReceiptStatusLabel.Text = this.ImportHeaderDataSet.F35085_GetCAImportDataTable[0]["RecordsCreateStatus"].ToString();
                CreateReceiptStatusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
                this.CreateReceiptButton.Enabled = false;
                this.FilePathPanel.Enabled = false;
                this.FilePathTextBox.Enabled = false;
            }

        }

        private void ImportTypeComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {

            //if (e.Index == -1)
            //    return;
            //ComboBox combo = ((ComboBox)sender);
            //using (SolidBrush brush = new SolidBrush(e.ForeColor))
            //{
            //    e.DrawBackground();
            //    e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, brush, e.Bounds, new StringFormat(StringFormatFlags.DirectionRightToLeft));
            //    e.DrawFocusRectangle();

            //}
        }



    }
}

