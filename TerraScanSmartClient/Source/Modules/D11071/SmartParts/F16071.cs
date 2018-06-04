
namespace D11071
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Resources;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using System.Globalization;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;
    using TerraScan.Common;
    using System.IO;

    [SmartPart]
    public partial class F16071 : BaseSmartPart
    {

        /// <summary>
        /// form15018Control Controller
        /// </summary>
        private F16071Controller form16071Control;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        private bool flagLoadOnProcess;

        private int? templateID;
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the schedule line grid active cell.
        /// </summary>
        private UltraGridCell activeCell;
        private UltraGridRow activeRow;

        /// <summary>
        /// Minimum filter length from tts_cfg table(TR_AccountListLookup) for auto complete functionality
        /// </summary>
        private int minFilterLength = 3;

        private string accountValueList = string.Empty;
        private string toAccountValueList = string.Empty;
        private int cuerrentRollYear;

        private bool canDelte = false;

        F16071JournalEntryTemplateData JournalDataSet = new F16071JournalEntryTemplateData();
        F11018MiscReceiptData.AccountListingDataTable filterdData = new F11018MiscReceiptData.AccountListingDataTable();
        F16071JournalEntryTemplateData.f11024_TempFromAccountTableDataTable FromAccountDataTable = new F16071JournalEntryTemplateData.f11024_TempFromAccountTableDataTable();
        F16071JournalEntryTemplateData.f11024_TempToAccountTableDataTable ToAccountDataTable = new F16071JournalEntryTemplateData.f11024_TempToAccountTableDataTable();


        #region Constructor
        public F16071()
        {
            InitializeComponent();
        }

        public F16071(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.templateID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.JournalEntryTemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.JournalEntryTemplatePictureBox.Height, this.JournalEntryTemplatePictureBox.Width, "", red, green, blue);
            this.JournalEntryTemplatePictureBox.SendToBack();
            this.JournalGridTemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.JournalGridTemplatePictureBox.Height, this.JournalGridTemplatePictureBox.Width, tabText, red, green, blue);
            this.JournalGridTemplatePictureBox.SendToBack();
            this.RollYearTextBox.Focus();
        }
        #endregion

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
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
        public F16071Controller Form16071Control
        {
            get { return this.form16071Control as F16071Controller; }
            set { this.form16071Control = value; }
        }
        #endregion Property

        #region FormLoad
        /// <summary>
        /// Handles the Load event of the F16071 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F16071_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.ClearControls();
                this.LoadDefaultView();
                this.CustomizeGridView();
                this.JournalGridTemplatePictureBox.SendToBack();
                this.JournalEntryTemplatePictureBox.SendToBack();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
                if (this.templateID > 0)
                {
                    this.RollYearTextBox.LockKeyPress = true;
                    this.RollYearTextBox.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.LockKeyPress = false;
                    this.RollYearTextBox.Enabled = true;

                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Event Subscription


        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    // this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.deletePermission = eventArgs.Data.DeletePermission;
                    //this.PermissionFiled.deletePermission;
                    //this.PermissionFiled.editPermission = eventArgs.Data.EditPermission;
                    this.slicePermissionField.editPermission = eventArgs.Data.EditPermission;//this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = eventArgs.Data.NewPermission;//this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = eventArgs.Data.OpenPermission;//this.PermissionFiled.openPermission;

                    if (this.JournalDataSet.f11071_GetHeaderJETempalte.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                        this.F16072LoadTemplateDetails();
                    }
                    else
                    {
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
                this.LoadDefaultView();
                if (this.templateID > 0)
                {
                    this.RollYearTextBox.LockKeyPress = true;
                    this.RollYearTextBox.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.LockKeyPress = false;
                    this.RollYearTextBox.Enabled = true;

                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;            
        }

        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.templateID = eventArgs.Data.SelectedKeyId;

            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.templateID = eventArgs.Data.SelectedKeyId;
                this.flagLoadOnProcess = true;
                this.FlagSliceForm = true;
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
                if (this.templateID > 0)
                {
                    this.RollYearTextBox.LockKeyPress = true;
                    this.RollYearTextBox.Enabled = false;
                }
                else
                {
                    this.RollYearTextBox.LockKeyPress = false;
                    this.RollYearTextBox.Enabled = true;

                }
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
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
        }

        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {

                    if (this.templateID == -99)
                    {
                        this.templateID = null;
                    }
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.templateID = null;
                    }

                    int rollYear;
                    int.TryParse(this.RollYearTextBox.Text, out rollYear);
                    int returnValue;
                    returnValue = this.form16071Control.WorkItem.F16071_SaveHeaderTemplateDetails(templateID, rollYear, this.TransferDescriptionTextBox.Text, TerraScanCommon.UserId);

                    this.templateID = returnValue;
                    string gridDetails = string.Empty;
                    gridDetails = this.ConvertToXML(this.JournalDataSet.f11071_GridJETempalte);
                    if (this.templateID > 0)
                    {
                        this.form16071Control.WorkItem.F16071_SaveGridTemplateDetails(templateID, gridDetails, TerraScanCommon.UserId);

                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.templateID);
                        this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                    }
                }
                else
                {
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.templateID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.templateID);
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.PermissionControlLock(!this.PermissionFiled.editPermission);
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            try
            {

                this.ClearControls();
                this.CustomizeGridView();
                if (this.slicePermissionField.newPermission)
                {
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.RollYearTextBox.Focus();
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
                    this.form16071Control.WorkItem.F16071_DeleteJournalHeaderDetails(Convert.ToInt32(this.templateID), TerraScanCommon.UserId);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.Cursor = Cursors.Default;

                    SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                    sliceEventArgs.MasterFormNo = this.masterFormNo;
                    sliceEventArgs.AllowNullRecordMode = false;
                    sliceEventArgs.WithoutKeyId = false;
                    this.FormSlice_NullRecordMode(this, new DataEventArgs<int>(this.masterFormNo));
                    this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }

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
                    this.templateID = eventArgs.Data.SelectedKeyId;
                    this.LoadDefaultView();
                }
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

        #endregion

        #region PictureBox Events

        /// <summary>
        /// Handles the MouseClick event of the JournalGridTemplatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void JournalGridTemplatePictureBox_MouseClick(object sender, MouseEventArgs e)
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

        /// <summary>
        /// Handles the MouseHover event of the JournalGridTemplatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JournalGridTemplatePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.JournalTemplateToolTip.SetToolTip(this.JournalGridTemplatePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion


        #region Private methods



        private string ConvertToXML(DataTable table)
        {
            DataTable dt = table.Copy();
            dt.Namespace = "";
            dt.TableName = "TemplateItem";
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            xmlstr = xmlstr.Replace("DocumentElement", "root");
            return (xmlstr);
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

            
           
            if (this.JournalDataSet.f11071_GridJETempalte.Rows.Count == 0)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("JournalEntryTemplateMissingField");
                this.JournalEntryGrid.Focus();
                return sliceValidationFields;
            }
            this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();

           // DataView tempDataView = new DataView(this.JournalDataSet.f11072Get_MiscGridDetailsTable, string.Concat(this.JournalDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);


            for (int i = 0; i < this.JournalDataSet.f11071_GridJETempalte.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["FromAccount"].ToString()) || !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["ToAccount"].ToString()) || !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["TransferAmount"].ToString()))
                {
                    if (!string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["FromAccount"].ToString()) && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["ToAccount"].ToString()) && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["TransferAmount"].ToString()))
                    {
                    }
                    else
                    {

                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("JournalEntryTemplateMissingField");
                        this.JournalEntryGrid.Focus();
                        return sliceValidationFields;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["FromAccount"].ToString()) && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["ToAccount"].ToString()) && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[i]["TransferAmount"].ToString()))
                    {
                    }
                    else
                    {
                        if (this.JournalDataSet.f11071_GridJETempalte.Rows.Count > 0)
                        {
                            this.JournalDataSet.f11071_GridJETempalte.Rows[i].Delete();
                            this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();
                        }
                        // var xxx = this.JournalDataSet.f11071_GridJETempalte.AsEnumerable();
                        //var xy = xxx.Where(x => x.ItemArray[0] != null);

                        //IEnumerable<DataRow> row = from v in this.JournalDataSet.f11071_GridJETempalte.AsEnumerable() where v.ItemArray.; 
                        if (JournalDataSet.f11071_GridJETempalte.Rows.Count == 0)
                        {
                            sliceValidationFields.RequiredFieldMissing = true;
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("JournalEntryTemplateMissingField");
                            this.JournalEntryGrid.Focus();
                            return sliceValidationFields;
                        }
                    }
                }
                
            }
            return sliceValidationFields;
        }

        /// <summary>
        /// Permissions the control lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void PermissionControlLock(bool controlLook)
        {
            this.ControlLock(controlLook);
        }

        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.TransferDescriptionTextBox.LockKeyPress = controlLook;
            this.JournalEntryGrid.Enabled = !controlLook;
        }

        private void ClearControls()
        {
            this.RollYearTextBox.Text = string.Empty;
            this.TransferDescriptionTextBox.Text = string.Empty;
            this.JournalDataSet.f11071_GridJETempalte.Clear();
            this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;


        }

        private void EnablePanelControl(bool enable)
        {
            this.RollYearPanel.Enabled = enable;
            this.TransferDescriptionPanel.Enabled = enable;
            this.JournalEntryTemplateGridPanel.Enabled = enable;
            //this.JournalEntryGrid.DisplayLayout.Bands[0].Header.Appearance.ForeColor = Color.White;
            //this.JournalEntryGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.ForeColor = Color.White;
        }

        private void EnableControls(bool enable)
        {
            this.RollYearTextBox.Enabled = enable;
            this.TransferDescriptionTextBox.Enabled = enable;
            this.JournalEntryGrid.Enabled = enable;
            //this.JournalEntryGrid.DisplayLayout.Bands[0].Header.Appearance.ForeColor = Color.White;
            //this.JournalEntryGrid.DisplayLayout.Bands[0].Override.HeaderAppearance.ForeColor = Color.White;

        }

        private void LoadDefaultView()
        {
            this.EnablePanelControl(true);
            this.EnableControls(true);
            this.ControlLock(false);
            this.JournalEntryGrid.Enabled = true;
            this.F16072LoadTemplateDetails();
        }

        private void CustomizeGridView()
        {
            UltraGridBand currentBand = this.JournalEntryGrid.DisplayLayout.Bands[0];
            currentBand.Override.SelectTypeRow = SelectType.Extended;

            currentBand.Override.RowSelectors = DefaultableBoolean.True;
            this.JournalEntryGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
            this.JournalEntryGrid.DisplayLayout.TabNavigation = TabNavigation.NextCell;
            currentBand.Header.Appearance.ForeColor = Color.White;
            currentBand.Override.HeaderAppearance.ForeColor = Color.White;

            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Hidden = true;

            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;


            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].Format = "$#,###0.00";


            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].CellActivation = Activation.AllowEdit;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].CellActivation = Activation.AllowEdit;
            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].CellActivation = Activation.AllowEdit;

            currentBand.Columns[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].MaxLength = 14;



            if (this.JournalEntryGrid.Rows.Count < 12)
            {
                ////to assgin empty row at the end of the gird
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.JournalEntryGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }

            this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
        }

        private void F16072LoadTemplateDetails()
        {
            this.JournalDataSet = this.form16071Control.WorkItem.F16071_GetJournalTeplateDetails(Convert.ToInt32(this.templateID));
            if (JournalDataSet.f11071_GetHeaderJETempalte.Rows.Count > 0)
            {
                this.RollYearTextBox.Text = this.JournalDataSet.f11071_GetHeaderJETempalte.Rows[0]["RollYear"].ToString();
                int.TryParse(this.RollYearTextBox.Text, out cuerrentRollYear);
                this.TransferDescriptionTextBox.Text = this.JournalDataSet.f11071_GetHeaderJETempalte.Rows[0]["TransferDescription"].ToString();

                //if (JournalDataSet.f11071_GridJETempalte.Rows.Count > 0)
                //{
                    this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
               // }

                if (this.formMasterPermissionEdit || (!this.formMasterPermissionEdit))

                // if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                {
                    //if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)


                    if (this.slicePermissionField.editPermission)
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
        #endregion


        #region Grid Events/Methods


        /// <summary>
        /// Handles the InitializeLayout event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGridView();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {

                this.LoadAccountItem(e.Row.Index);
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ValueList = this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList];
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                this.LoadToAccountItem(e.Row.Index);
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ValueList = this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList];
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                    e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                }
                else
                {
                    e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                    e.Row.Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;
                if (this.activeCell != null && this.activeCell.Value != null)
                {
                    this.SetEditRecord();

                    if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                        && activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName))
                    {
                        try
                        {
                            e.Cell.Tag = e.Cell.Text.Trim();
                            this.masterFormNo = 11071;
                            int.TryParse(this.RollYearTextBox.Text, out cuerrentRollYear);
                            filterdData = this.form16071Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), cuerrentRollYear, masterFormNo).AccountListing;
                            DataView fromDataView = filterdData.DefaultView;
                            FromAccountDataTable.Clear();
                            foreach (DataRow sourcerow in filterdData.Rows)
                            {
                                DataRow destRow = FromAccountDataTable.NewRow();
                                destRow["FromAccountID"] = sourcerow["AccountID"];
                                destRow["FromAccount"] = sourcerow["AccountName"];
                                FromAccountDataTable.Rows.Add(destRow);
                            }

                            DataRow[] filteredRow = FromAccountDataTable.Select();

                            if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                            {
                                this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                            }
                            else
                            {
                                this.accountValueList = System.Guid.NewGuid().ToString();
                            }

                            if (filteredRow.Length > 0)
                            {
                                if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                                {
                                    this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.Clear();
                                }

                                ValueList objValueList = (ValueList)e.Cell.ValueList;


                                for (int count = 0; count < filteredRow.Length; count++)
                                {

                                    if (filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName] != null
                                          && !string.IsNullOrEmpty(filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].ToString().Trim()))
                                    {
                                        objValueList.ValueListItems.Add((filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].ToString()), filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ToString());
                                    }
                                }
                            }
                            else
                            {
                                //e.Cell.CancelUpdate();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }



                    if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                         && activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName))
                    {

                        try
                        {
                            e.Cell.Tag = e.Cell.Text.Trim();
                            this.masterFormNo = 11071;
                            int.TryParse(this.RollYearTextBox.Text, out cuerrentRollYear);
                            filterdData = this.form16071Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), cuerrentRollYear, masterFormNo).AccountListing;
                            DataView fromDataView = filterdData.DefaultView;
                            ToAccountDataTable.Clear();
                            foreach (DataRow sourcerow in filterdData.Rows)
                            {
                                DataRow destRow = ToAccountDataTable.NewRow();
                                destRow["ToAccountID"] = sourcerow["AccountID"];
                                destRow["ToAccount"] = sourcerow["AccountName"];
                                ToAccountDataTable.Rows.Add(destRow);
                            }
                            DataRow[] filteredRow = ToAccountDataTable.Select();

                            if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                            {
                                this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                            }
                            else
                            {
                                this.toAccountValueList = System.Guid.NewGuid().ToString();
                            }

                            if (filteredRow.Length > 0)
                            {
                                if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList))
                                {
                                    this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.Clear();
                                }

                                ValueList objValueList = (ValueList)e.Cell.ValueList;


                                for (int count = 0; count < filteredRow.Length; count++)
                                {

                                    if (filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName] != null
                                          && !string.IsNullOrEmpty(filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].ToString().Trim()))
                                    {
                                        objValueList.ValueListItems.Add((filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].ToString()), filteredRow[count][this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ToString());
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
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
        /// Handles the BeforeExitEditMode event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {               
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.accountValueList = System.Guid.NewGuid().ToString();
                            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList)
                                && this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {

                                this.activeCell.CancelUpdate();
                                // this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.AccountNameColumn.ColumnName].Value = DBNull.Value;
                                //Modiifed AccountName to ID
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value = string.Empty;

                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                                {
                                    this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.JournalEntryGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }

                                    //  availablestring = this.JournalEntryGrid.ActiveCell.ValueList.FindStringExact(this.activeCell.Text.Trim());
                                }

                                if (availablestring < 0)
                                {
                                    this.activeCell.CancelUpdate();
                                }
                            }
                        }
                        else
                        {
                            //Modiifed AccountName to ID
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value = string.Empty;
                        }
                    }




                    //ToAccount Details

                    if (activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.toAccountValueList = System.Guid.NewGuid().ToString();
                            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList)
                                && this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {

                                this.activeCell.CancelUpdate();

                                //Modiifed AccountName to ID
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = string.Empty;

                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null)
                                {
                                    this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.JournalEntryGrid.DisplayLayout.ValueLists[this.toAccountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }
                                }

                                if (availablestring < 0)
                                {
                                    this.activeCell.CancelUpdate();
                                }
                            }
                        }
                        else
                        {
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = string.Empty;
                        }
                    }


                    if (activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            decimal convertedValue = 0;
                            decimal.TryParse(this.activeCell.Text.Trim(), out convertedValue);
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].Value = convertedValue;
                        }
                        else
                        {
                            this.JournalEntryGrid.Rows[this.activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].Value = DBNull.Value;
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
        /// Handles the AfterExitEditMode event of the JournalEntryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void JournalEntryGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.JournalEntryGrid.ActiveCell;
                this.activeRow = this.JournalEntryGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName))
                    {
                        if (this.activeCell.DataChanged) //((int)this.activeCell.OriginalValue != (int)this.activeCell.Value)//
                        {
                            if (this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value.ToString()))
                            {
                                int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value.ToString());
                                F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();

                                accountNameDataSet = this.form16071Control.WorkItem.F15013_GetAccountName(accountId);

                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value = dr.AccountID;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value = dr.AccountName + " " + dr.Description;

                                    //this.ReceiptItemGridView[this.AccountId.Name, e.RowIndex].Value = accountId;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.DescriptionColumn.ColumnName].Value = dr.Description;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = dr.AccountName;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = dr.AccountID;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].Value = 0;
                                }
                                else
                                {
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.DescriptionColumn.ColumnName].Value = string.Empty;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value = string.Empty;
                                }
                                this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value;
                                //this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value;

                            }
                            else
                            {
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].Value = string.Empty;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountIDColumn.ColumnName].Value = DBNull.Value;
                            }

                            this.JournalEntryGrid.UpdateData();

                            DataView tempDataView = new DataView(this.JournalDataSet.f11071_GridJETempalte, string.Concat(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                            DataTable valdiateTable = tempDataView.ToTable().Copy();


                        }
                    }


                    if (activeCell.Column.Key.Equals(this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName))
                    {
                        if (this.activeCell.DataChanged)
                        {
                            if (this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value.ToString()))
                            {
                                int accountId = int.Parse(this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value.ToString());
                                F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();

                                accountNameDataSet = this.form16071Control.WorkItem.F15013_GetAccountName(accountId);

                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];

                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = dr.AccountID;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = dr.AccountName + " " + dr.Description;

                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.DescriptionColumn.ColumnName].Value = dr.Description;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName].Value = 0;
                                }
                                else
                                {
                                    // this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.DescriptionColumn.ColumnName].Value = string.Empty;

                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                                    this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = string.Empty;
                                    //this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.AccountStatusColumn.ColumnName].Value = 0;
                                }
                                this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value;
                                //this.activeCell.Value = this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value;

                            }
                            else
                            {
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].Value = string.Empty;
                                this.JournalEntryGrid.Rows[activeRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountIDColumn.ColumnName].Value = DBNull.Value;
                            }
                            this.JournalEntryGrid.UpdateData();

                            DataView tempDataView = new DataView(this.JournalDataSet.f11071_GridJETempalte, string.Concat(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                            DataTable valdiateTable = tempDataView.ToTable().Copy();


                        }
                    }
                    if (this.activeCell.DataChanged)
                    {
                        this.JournalEntryGrid.UpdateData();
                        DataView tempDataView = new DataView(this.JournalDataSet.f11071_GridJETempalte, string.Concat(this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.TransferAmountColumn.ColumnName, " IS NOT NULL OR ", this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



        /// <summary>
        /// Loads to account item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void LoadToAccountItem(int rowIndex)
        {
            //To Account Combobox
            if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index >= 0
           && this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ValueList != null)
            {
                this.toAccountValueList = this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ValueList.ToString();
            }
            else
            {
                this.toAccountValueList = System.Guid.NewGuid().ToString();
            }
            if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.toAccountValueList))
            {
                return;
            }

            if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index.Equals(rowIndex)
                 && this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null && this.JournalEntryGrid.ActiveCell.ValueList.ItemCount > 0)
            {
                // TO DO
                this.toAccountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
            }
            else
            {
                ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.toAccountValueList);

                if (rowIndex < this.JournalDataSet.f11071_GridJETempalte.Rows.Count)// && this.flagLoadOnProcess)
                {
                    // ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                    if (this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName] != null
                        && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ToString().Trim()))
                    {
                        objValueList.ValueListItems.Add((this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.ToAccountColumn.ColumnName].ToString()));

                    }
                }
            }
        }

        /// <summary>
        /// Loads the account item.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void LoadAccountItem(int rowIndex)
        {            
            if (rowIndex >= 0)
            {
                if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index >= 0
                    && this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ValueList != null)
                {
                    this.accountValueList = this.JournalEntryGrid.Rows[this.JournalEntryGrid.ActiveRow.Index].Cells[this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ValueList.ToString();
                }
                else
                {
                    this.accountValueList = System.Guid.NewGuid().ToString();
                }

                //    // If the cell already populated with list item
                if (this.JournalEntryGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                {
                    return;
                }

                if (this.JournalEntryGrid.ActiveRow != null && this.JournalEntryGrid.ActiveRow.Index.Equals(rowIndex)
                       && this.JournalEntryGrid.ActiveCell != null && this.JournalEntryGrid.ActiveCell.ValueList != null && this.JournalEntryGrid.ActiveCell.ValueList.ItemCount > 0)
                {
                    // TO DO
                    this.accountValueList = this.JournalEntryGrid.ActiveCell.ValueList.ToString();
                }
                else
                {
                    ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);

                    if (rowIndex < this.JournalDataSet.f11071_GridJETempalte.Rows.Count)// && this.flagLoadOnProcess)
                    {
                        // ValueList objValueList = this.JournalEntryGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                        if (this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ToString().Trim()))
                        {
                            objValueList.ValueListItems.Add((this.JournalDataSet.f11071_GridJETempalte.Rows[rowIndex][this.JournalDataSet.f11071_GridJETempalte.FromAccountColumn.ColumnName].ToString()));
                        }
                    }
                }

            }
        }
        #endregion

        private void JournalEntryGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                if (this.canDelte)
                {
                    this.canDelte = false;
                    e.Cancel = true;

                }
                //// this.JournalEntryGrid.DeleteSelectedRows(false);
                // if (this.JournalDataSet.f11071_GridJETempalte.Rows.Count == 0)
                // {
                //     e.Cancel = true;
                // }
                // else
                // {
                //     e.DisplayPromptMsg = false;
                //    // this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();
                //   // this.JournalDataSet.f11071_GridJETempalte.
                //     this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;

                // }
                // e.Cancel = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void JournalEntryGrid_AfterRowsDeleted(object sender, EventArgs e)
        {
            this.CustomizeGridView();
            // this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();
            // this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
        }

        private void JournalEntryGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }
        private string RemoveXMLItem(ArrayList parcelIds)
        {
            DataTable dt = new DataTable("TemplateItem");
            dt.Columns.Add("TemplateItemID", typeof(Int32));
            foreach (var r in parcelIds)
            {
                dt.Rows.Add(r);
            }
            dt = dt.DefaultView.ToTable(true, "TemplateItemID");
            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string removexmlstr;
            removexmlstr = sr.ReadToEnd();
            removexmlstr = removexmlstr.Replace("DocumentElement", "root");
            // xmlstr = xmlstr.Replace("ParcelTable", "Table");
            return (removexmlstr);
        }
        private void JournalEntryGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                ArrayList tempList = new ArrayList();
                string templateIds = string.Empty;
                ////Delete key handler
                if (e.KeyValue == 46)
                {
                    if (JournalEntryGrid.Selected.Rows.Count > 0)
                    {
                        if (this.JournalDataSet.f11071_GridJETempalte.Rows.Count > 0)
                        {
                            foreach (UltraGridRow rowSelected in this.JournalEntryGrid.Selected.Rows)
                            {
                                var index = rowSelected.Index;
                                if (index < this.JournalDataSet.f11071_GridJETempalte.Rows.Count)
                                {
                                    if (!string.IsNullOrEmpty(this.JournalDataSet.f11071_GridJETempalte.Rows[index]["TemplateItemID"].ToString()))
                                    {
                                        tempList.Add(this.JournalDataSet.f11071_GridJETempalte.Rows[index]["TemplateItemID"].ToString());
                                    }
                                }
                                else
                                {
                                    this.canDelte = true;
                                }

                            }
                            if (tempList.Count > 0)
                            {
                                templateIds = RemoveXMLItem(tempList);
                                if (MessageBox.Show("Do you want to delete these items?                   ", "TerraScan T2 – Delete Journal Entry Template Items ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    foreach (UltraGridRow rowSelected in this.JournalEntryGrid.Selected.Rows)
                                    {
                                        var index = rowSelected.Index;
                                        this.JournalEntryGrid.DeleteSelectedRows(false);
                                        this.JournalEntryGrid.DeleteSelectedRows();

                                        //for (int i = this.JournalDataSet.f11071_GridJETempalte.Rows.Count - 1; i >= 0; i--)
                                        //{
                                        //    DataRow dr = this.JournalDataSet.f11071_GridJETempalte.Rows[i];
                                        //    if (index==i)
                                        //    {
                                        //        dr.Delete();
                                        //    }
                                        //} 

                                    }
                                    //this.JournalDataSet.f11071_GridJETempalte.Rows[deleteRowIndex].Delete();
                                    if (!templateIds.Equals("<root />"))
                                    {
                                        this.form16071Control.WorkItem.F16071_DeleteJournalGridDetails(Convert.ToInt32(templateID), templateIds, TerraScanCommon.UserId);

                                        this.SetEditRecord();
                                    }
                                    this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();
                                    this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
                                }
                                else
                                {
                                    this.canDelte = true;
                                }
                            }
                            else
                            {
                                if (JournalEntryGrid.Selected.Rows.Count > 0 && this.JournalDataSet.f11071_GridJETempalte.Rows.Count>0)
                                {
                                    foreach (UltraGridRow rowSelected in this.JournalEntryGrid.Selected.Rows)
                                    {
                                        var index = rowSelected.Index;
                                        if (index < this.JournalDataSet.f11071_GridJETempalte.Rows.Count)
                                        {
                                            if (MessageBox.Show("Do you want to delete these items?                   ", "TerraScan T2 – Delete Journal Entry Template Items ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                this.JournalEntryGrid.DeleteSelectedRows(false);
                                            }
                                            else
                                            {
                                                this.canDelte = true;
                                            }
                                        }
                                    }
                                }

                            }

                            this.JournalDataSet.f11071_GridJETempalte.AcceptChanges();
                            this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
                            // this.JournalEntryGrid.DeleteSelectedRows(false);
                        }
                        else
                        {
                            this.canDelte = true;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RollYearTextBox_Leave(object sender, EventArgs e)
        {

            int tempYear;
            int.TryParse(this.RollYearTextBox.Text, out tempYear);
            if (!this.cuerrentRollYear.Equals(tempYear))
            {
                int.TryParse(this.RollYearTextBox.Text, out cuerrentRollYear);
                if (this.templateID > 0)
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.JournalDataSet.f11071_GridJETempalte.Clear();
                        this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
                    }
                }
                else
                {
                    this.JournalDataSet.f11071_GridJETempalte.Clear();
                    this.JournalEntryGrid.DataSource = this.JournalDataSet.f11071_GridJETempalte.DefaultView;
                }
            }

        }

        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text))
            {
                //int.TryParse(this.RollYearTextBox.Text, out cuerrentRollYear);
                this.SetEditRecord();
            }
        }

        private void TransferDescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SetEditRecord();
        }

        private void RollYearTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {


        }

        private void RollYearTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //if (this.templateID > 0)
            //{
            //    this.RollYearTextBox.LockKeyPress = true;
            //    this.RollYearTextBox.Enabled = false;
            //}
            //else
            //{
            //    this.RollYearTextBox.LockKeyPress = false;
            //    this.RollYearTextBox.Enabled = true;

            //}

        }

    }
}
