namespace D11072
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
    using TerraScan.Common;
    using TerraScan.Common.Reports;
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
    using System.IO;

    /// <summary>
    /// 
    /// </summary>
    [SmartPart]
    public partial class F16072 : BaseSmartPart
    {
        #region Variables

        private F16072Controller form16072Control;
        /// <summary>
        /// Unique keyId from the Form Master - statement id
        /// </summary>
        private int? MiscTemplateID;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        private PermissionFields slicePermissionField;
        
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        private bool formMasterPermissionEdit;

        private UltraGridRow activeRow;

        /// <summary>
        /// instance variable to hold the schedule line grid active cell.
        /// </summary>
        private UltraGridCell activeCell;

        /// <summary>
        /// Minimum filter length from tts_cfg table(TR_AccountListLookup) for auto complete functionality
        /// </summary>
        private int minFilterLength = 3;

        private string isFilteredAccount = string.Empty;

        ///<summary>
        /// Used to hold RollYear Value
        /// </summary>
        private int? rollYear;

        ///<summary>
        /// Used to hold  temp roll Year
        /// </summary>
        private int? tempYear;

        private string accountValueList = string.Empty;

        public static int TempMiscID;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;
        private F11018MiscReceiptData miscReceipt = new F11018MiscReceiptData();
        
        F16072MiscReceiptTemplate.f11072_Get_MiscReceiptTemplateDataTable MiscReceipttemplateDataTable = new F16072MiscReceiptTemplate.f11072_Get_MiscReceiptTemplateDataTable();
        F16072MiscReceiptTemplate MiscGridDataSet = new F16072MiscReceiptTemplate();
        F16072MiscReceiptTemplate.f16072_FilterDataDataTable filterdDataTable = new F16072MiscReceiptTemplate.f16072_FilterDataDataTable();
        F11018MiscReceiptData.AccountListingDataTable filterdData = new F11018MiscReceiptData.AccountListingDataTable();


        ArrayList miscId = new ArrayList();
        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F16072"/> class.
        /// </summary>
        public F16072()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F16072"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F16072(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.MiscTemplateID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.MiscReceiptTemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.MiscReceiptTemplatePictureBox.Height, this.MiscReceiptTemplatePictureBox.Width, tabText, red, green, blue);
            this.MiscReceiptTemplatePictureBox.SendToBack();            
            TempMiscID = keyID;
            
        }
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form16072 control.
        /// </summary>
        /// <value>The form16072 control.</value>
        [CreateNew]
        public F16072Controller Form16072Control
        {
            get { return this.form16072Control as F16072Controller; }
            set { this.form16072Control = value; }
        }
        #endregion
             

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


        /// <summary>
        /// Handles the Load event of the F16072 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        public void F16072_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = false;
                this.flagLoadOnProcess = true;
                this.ClearControls();
                this.MiscItemGrid.Enabled = true;
                this.LoadDefaultView();
                this.CustomizeGrid();
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

                    if (this.MiscReceipttemplateDataTable.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
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
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }


        /// <summary>
        /// OnD9030_F9030_LoadSliceDetails
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            this.MiscTemplateID = eventArgs.Data.SelectedKeyId;

            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.MiscTemplateID = eventArgs.Data.SelectedKeyId;
                TempMiscID =Convert.ToInt32(this.MiscTemplateID);
                this.flagLoadOnProcess = true;
                this.FlagSliceForm = true;
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
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
                    if (TempMiscID > 0)
                    {
                        if (MiscTemplateID != TempMiscID)
                        {
                            MiscTemplateID = TempMiscID;
                            TempMiscID = 0;
                        }
                    }
                    if (this.MiscTemplateID == -99)
                    {
                        this.MiscTemplateID = null;
                    }
                    F16072MiscReceiptTemplate updateMiscTemplateDetails = new F16072MiscReceiptTemplate();
                    F16072MiscReceiptTemplate.f16072_SaveMiscHeaderDetailsRow dr = updateMiscTemplateDetails.f16072_SaveMiscHeaderDetails.Newf16072_SaveMiscHeaderDetailsRow();
                    
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.MiscTemplateID = null;
                    }

                    dr.Description = this.DescriptionTextBox.Text;
                    dr.ReceivedFrom = this.ReceivedFromTextBox.Text;
                    dr.DefaultComment = this.DefaultCommentTextBox.Text;

                    updateMiscTemplateDetails.f16072_SaveMiscHeaderDetails.Rows.Add(dr);
                    updateMiscTemplateDetails.f16072_SaveMiscHeaderDetails.AcceptChanges();
                    DataSet MiscHeaderDataSet = new DataSet("Root");
                    MiscHeaderDataSet.Tables.Add(updateMiscTemplateDetails.f16072_SaveMiscHeaderDetails.Copy());
                    MiscHeaderDataSet.Tables[0].TableName = "Table";

                    this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AcceptChanges();
                    DataSet MiscAccountDataSet = new DataSet("Root");
                    MiscAccountDataSet.Tables.Add(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Copy());
                    MiscAccountDataSet.Tables[0].TableName = "Table";                 

                    int returnValue;

                    returnValue = this.form16072Control.WorkItem.F16072_SaveMiscReceiptTemplate(MiscTemplateID, MiscHeaderDataSet.GetXml(),MiscAccountDataSet.GetXml(), TerraScanCommon.UserId);
                    SliceReloadActiveRecord sliceUpdateActiveRecord = new SliceReloadActiveRecord();
                    sliceUpdateActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceUpdateActiveRecord.SelectedKeyId = returnValue;
                    TempMiscID = returnValue;
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
                    sliceUpdateActiveRecord.SelectedKeyId = Convert.ToInt32(this.MiscTemplateID);
                    this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(sliceUpdateActiveRecord));
                    SliceReloadActiveRecord sliceReloadActiveRecord;
                    sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                    sliceReloadActiveRecord.SelectedKeyId = Convert.ToInt32(this.MiscTemplateID);
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
                    this.form16072Control.WorkItem.F16072_DeleteMisctemplateDetails(Convert.ToInt32(this.MiscTemplateID), TerraScanCommon.UserId);
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
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                TempMiscID = 0;
                this.ClearControls();
                this.CustomizeGrid();
                if (this.slicePermissionField.newPermission)
                {
                    this.EnableControls(true);
                    this.EnablePanelControl(true);
                    this.ControlLock(false);
                    this.DescriptionTextBox.Focus();
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
                    this.MiscTemplateID = eventArgs.Data.SelectedKeyId;
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

        #region Grid Events

        private void MiscItemGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Gets the minimum length of the filter.
        /// </summary>
        private void GetMinimumFilterLength()
        {
            CommentsData.GetCommentsConfigDetailsDataTable configDetails = this.form16072Control.WorkItem.GetConfigDetails("TR_AccountListLookup").GetCommentsConfigDetails;
            if (configDetails.Rows.Count > 0 && !string.IsNullOrEmpty(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString()))
            {
                int.TryParse(configDetails.Rows[0][configDetails.ConfigurationValueColumn.ColumnName].ToString(), out this.minFilterLength);
            }
        }

        private void LoadAccountItem(int rowIndex)
        {
            if (rowIndex >= 0)
            {
                if (this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveRow.Index >= 0
                    && this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ValueList != null)
                {
                    this.accountValueList = this.MiscItemGrid.Rows[this.MiscItemGrid.ActiveRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ValueList.ToString();
                }
                else
                {
                    this.accountValueList = System.Guid.NewGuid().ToString();
                }
                //this.accountValueList = "Row" + rowIndex;

                // If the cell already populated with list item
                if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                {
                    return;
                }

                if (this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveRow.Index.Equals(rowIndex)
                       && this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null && this.MiscItemGrid.ActiveCell.ValueList.ItemCount > 0)
                {
                    // TO DO
                    this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                }
                else
                {
                    ValueList objValueList = this.MiscItemGrid.DisplayLayout.ValueLists.Add(this.accountValueList);

                    if (rowIndex < this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.Count)// && this.flagLoadOnProcess)
                    {
                        // ValueList objValueList = this.MiscItemGrid.DisplayLayout.ValueLists.Add(this.accountValueList);
                        if (this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[rowIndex][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName] != null
                            && !string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[rowIndex][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ToString().Trim()))
                        {
                            objValueList.ValueListItems.Add((this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[rowIndex][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ToString()));
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Handles the CellChange event of the MiscItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void MiscItemGrid_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
        {
            try
            {
                this.activeCell = this.MiscItemGrid.ActiveCell;
                this.activeRow = this.MiscItemGrid.ActiveRow;
                if (this.activeCell != null && this.activeCell.Value != null)
                {
                    this.SetEditRecord();

                    if (e.Cell.Text != null && e.Cell.Text.Trim().Length.Equals(this.minFilterLength)
                        && activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName))
                    {
                        if (this.tempYear != this.rollYear)
                        {
                            e.Cell.Tag = null;
                        }
                        if (e.Cell.Tag != null && e.Cell.Tag.ToString().ToUpper().Trim().Equals(e.Cell.Text.ToUpper().Trim()))
                        {
                            return;
                        }

                        try
                        {
                            e.Cell.Tag = e.Cell.Text.Trim();
                            filterdData = this.form16072Control.WorkItem.F15018_ListAccountDetails(e.Cell.Text.Trim(), this.rollYear, masterFormNo).AccountListing;

                            DataRow[] filteredRow = filterdData.Select();

                            if (this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null)
                            {
                                this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                            }
                            else
                            {
                                this.accountValueList = System.Guid.NewGuid().ToString();
                            }

                            if (filteredRow.Length > 0)
                            {


                                //this.accountValueList = "Row" + e.Cell.Row. Index;
                                if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList))
                                {
                                    this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.Clear();
                                }

                                ValueList objValueList = (ValueList)e.Cell.ValueList;


                                for (int count = 0; count < filteredRow.Length; count++)
                                {

                                    if (filteredRow[count][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName] != null
                                          && !string.IsNullOrEmpty(filteredRow[count][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].ToString().Trim()))
                                    {
                                        objValueList.ValueListItems.Add((filteredRow[count][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].ToString()), filteredRow[count][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ToString());
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

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the AfterExitEditMode event of the MiscItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MiscItemGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.MiscItemGrid.ActiveCell;
                this.activeRow = this.MiscItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName))
                    {
                        if (this.activeCell.DataChanged) //((int)this.activeCell.OriginalValue != (int)this.activeCell.Value)//
                        {
                            if (this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value != null
                                && !string.IsNullOrEmpty(this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value.ToString()))
                            {
                                int accountId=0;
                                try
                                {
                                     accountId = int.Parse(this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value.ToString());
                                }
                                catch (Exception ex)
                                {

                                }
                                F15013ExciseTaxRateData accountNameDataSet = new F15013ExciseTaxRateData();

                                accountNameDataSet = this.form16072Control.WorkItem.F15013_GetAccountName(accountId);

                                if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                                {
                                    F15013ExciseTaxRateData.GetAccountNameRow dr = (F15013ExciseTaxRateData.GetAccountNameRow)accountNameDataSet.GetAccountName.Rows[0];
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = dr.AccountID;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = dr.AccountName;
                                    //this.ReceiptItemGridView[this.AccountId.Name, e.RowIndex].Value = accountId;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Value = dr.Description;
                                }
                                else
                                {
                                    // this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = string.Empty;
                                    // this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Value = string.Empty;
                                    this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                    //this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountStatusColumn.ColumnName].Value = 0;
                                }
                                this.activeCell.Value = this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value;
                                this.activeCell.Value = this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value;

                            }
                            else
                            {
                                this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = string.Empty;
                                this.MiscItemGrid.Rows[activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = DBNull.Value;
                            }

                            //this.CalculateReceiptTotal();
                            this.MiscItemGrid.UpdateData();

                            //Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();                            
                            //appearance2.BackColor = System.Drawing.Color.FromArgb(187, 222, 173);
                            //this.activeRow.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Appearance = appearance2;

                            DataView tempDataView = new DataView(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable, string.Concat(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
                            DataTable valdiateTable = tempDataView.ToTable().Copy();


                        }
                    }

                    ////checks only if amount or account field
                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName))// || e.ColumnIndex == this.ReceiptItemGridView.Columns[this.Account.Name].Index)
                    {
                        //UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
                        //currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].CellActivation = Activation.NoEdit;
                        //currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].CellActivation = Activation.AllowEdit;
                        // this.MiscItemGrid.UpdateData();
                        ////calculating related values for new values
                        // this.CalculateReceiptTotal();
                    }

                    if (this.activeCell.DataChanged)
                    {
                        this.MiscItemGrid.UpdateData();
                        DataView tempDataView = new DataView(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable, string.Concat(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the BeforeExitEditMode event of the MiscItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void MiscItemGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                this.activeCell = this.MiscItemGrid.ActiveCell;
                this.activeRow = this.MiscItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
                {
                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.accountValueList = System.Guid.NewGuid().ToString();
                            if (this.MiscItemGrid.DisplayLayout.ValueLists.Exists(this.accountValueList)
                                && this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.SelectedItem == null)
                            {

                                this.activeCell.CancelUpdate();
                                // this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = DBNull.Value;
                                //Modiifed AccountName to ID
                                this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = DBNull.Value;
                                this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = string.Empty;

                            }
                            else
                            {
                                int availablestring = -1;
                                if (this.MiscItemGrid.ActiveCell != null && this.MiscItemGrid.ActiveCell.ValueList != null)
                                {
                                    this.accountValueList = this.MiscItemGrid.ActiveCell.ValueList.ToString();
                                    //modified by purushotham to avoid cancel update when description field returns null value
                                    if (this.activeCell.Text.Trim().Length > 3)
                                    {
                                        availablestring = this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList].ValueListItems.ValueList.FindString(this.activeCell.Text.Trim());
                                    }

                                    //  availablestring = this.MiscItemGrid.ActiveCell.ValueList.FindStringExact(this.activeCell.Text.Trim());
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
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Value = DBNull.Value;
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Value = string.Empty;
                        }
                    }

                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            decimal convertedValue = 0;
                            decimal.TryParse(this.activeCell.Text.Trim(), out convertedValue);
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Value = convertedValue;
                            //this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Text = this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Text;
                        }
                        else
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Value = DBNull.Value;
                        }
                    }
                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Value = this.activeCell.Text.Trim();
                            //this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Text = this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Text;
                        }
                        else
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Value = string.Empty;
                        }
                    }


                    if (activeCell.Column.Key.Equals(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName) && this.activeCell.DataChanged)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].Value = this.activeCell.Text.Trim();
                        }
                        else
                        {
                            this.MiscItemGrid.Rows[this.activeRow.Index].Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].Value = string.Empty;
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
        /// Handles the InitializeRow event of the MiscItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void MiscItemGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {

                this.LoadAccountItem(e.Row.Index);
                e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ValueList = this.MiscItemGrid.DisplayLayout.ValueLists[this.accountValueList];

                e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                }
                else
                {
                    e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest;
                }

                //Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
                //e.Row.Cells[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Appearance = appearance2;


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the KeyDown event of the MiscItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MiscItemGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    
                   
                    ////delete key
                    if (e.KeyCode.Equals(Keys.Delete) && this.MiscItemGrid.ActiveRow != null && this.MiscItemGrid.ActiveCell == null)// && this.MiscItemGrid.ActiveRow.Selected)
                    {
                        ////this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.RemoveAt(this.MiscItemGrid.ActiveCell.Row.Index);
                        //int deleteIndex = this.MiscItemGrid.ActiveRow.Index;
                        //this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.RemoveAt(this.MiscItemGrid.ActiveRow.Index);
                        //this.MiscGridDataSet.AcceptChanges();
                        ////this.CalculateReceiptTotal();
                        //this.SetEditRecord();
                        //this.MiscItemGrid.UpdateData();
                        //this.MiscItemGrid.DataSource = this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DefaultView;

                        //UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
                        //currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Format = "#,##0.00;(#,##0.00);0.00";
                        //// Latha
                        //if (this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.Count > 0 && this.MiscItemGrid.Rows.Count > 0)
                        //{
                        //    F11018MiscReceiptData.AccountListingDataTable accountListTable = new F11018MiscReceiptData.AccountListingDataTable();
                        //    //Latha
                        //    for (int rowCount = 0; rowCount < this.MiscItemGrid.Rows.Count; rowCount++)
                        //    {
                        //        F11018MiscReceiptData.AccountListingRow newAccountList = accountListTable.NewAccountListingRow();
                        //        newAccountList.AccountID = int.Parse(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable[rowCount][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].ToString());
                        //       // newAccountList.AccountName = this.MiscGridDataSet.f11072Get_MiscGridDetailsTable[rowCount][this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].ToString();
                        //        accountListTable.Rows.Add(newAccountList);
                        //    }

                        //    this.filterdData.Merge(accountListTable);
                        //}

                        //this.MiscItemGrid.Focus();
                        //if (this.MiscItemGrid.Rows.Count > 0)
                        //{
                        //    if (deleteIndex > 0)
                        //    {
                        //        //this.accountValueList = "Row" + (deleteIndex - 1);
                        //        this.MiscItemGrid.Rows[deleteIndex - 1].Activate();
                        //        this.MiscItemGrid.Rows[deleteIndex - 1].Selected = true;
                        //    }
                        //    else
                        //    {
                        //        //this.accountValueList = "Row0";
                        //        this.MiscItemGrid.Rows[0].Activate();
                        //        this.MiscItemGrid.Rows[0].Selected = true;
                        //    }
                        //}

                    }
                }
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

        private void CustomizeGrid()
        {
            UltraGridBand currentBand = this.MiscItemGrid.DisplayLayout.Bands[0];
            currentBand.Override.SelectTypeRow = SelectType.Extended;

            currentBand.Override.RowSelectors = DefaultableBoolean.True;

            // set the rows selection to none for avoid selecting multy rows at a time
            // currentBand.Override.SelectTypeRow = SelectType.None;

            this.MiscItemGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;
            this.MiscItemGrid.DisplayLayout.TabNavigation = TabNavigation.NextCell;

            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Width = 300;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Width = 200;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].Width = 110;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Width = 110;


            // currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.MiscItemIDColumn.ColumnName].Hidden = true;



            //currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountNameColumn.ColumnName].Header.VisiblePosition = 0;
            //currentBand.Columns[this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            //currentBand.Columns[this.miscReceipt.ListReceiptItem.CodeColumn.ColumnName].Header.VisiblePosition = 2;
            //currentBand.Columns[this.miscReceipt.ListReceiptItem.AmountColumn.ColumnName].Header.VisiblePosition = 3;
            //currentBand.Columns[this.miscReceipt.ListReceiptItem.AccountIDColumn.ColumnName].Header.VisiblePosition = 4;
            //currentBand.Columns[this.miscReceipt.ListReceiptItem..ColumnName].Header.VisiblePosition = 4;



            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Header.Caption = "Account";
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Header.Caption = "Description";
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].Header.Caption = "Code";
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Header.Caption = "Amount";


            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;



            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;



            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].Format = "$#,###0.00";


            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName].CellActivation = Activation.AllowEdit;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].CellActivation = Activation.AllowEdit;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].CellActivation = Activation.AllowEdit;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].CellActivation = Activation.AllowEdit;




            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName].MaxLength = 14;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName].MaxLength = 50;
            currentBand.Columns[this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DescriptionColumn.ColumnName].MaxLength = 100;


            if (this.MiscItemGrid.Rows.Count < 12)
            {
                ////to assgin empty row at the end of the gird
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }
            else
            {
                this.MiscItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = false;
            }

            this.MiscItemGrid.UpdateMode = UpdateMode.OnUpdate;
            this.MiscItemGrid.DataSource = this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DefaultView;
        } 
        #endregion

        #region General Methods
        /// <summary>
        /// Clears the controls.
        /// </summary>
        private void ClearControls()
        {
            this.DescriptionTextBox.Text = string.Empty;
            this.ReceivedFromTextBox.Text = string.Empty;
            this.DefaultCommentTextBox.Text = string.Empty;
            this.MiscGridDataSet.Tables["f11072Get_MiscGridDetailsTable"].Clear();
            this.MiscItemGrid.DataSource = this.MiscGridDataSet.Tables["f11072Get_MiscGridDetailsTable"].DefaultView;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void EnableControls(bool show)
        {
            this.DescriptionTextBox.Enabled = show;
            this.ReceivedFromTextBox.Enabled = show;
            this.DefaultCommentTextBox.Enabled = show;
            this.MiscItemGrid.Enabled = show;
        }


        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void ControlLock(bool controlLook)
        {
            this.DescriptionTextBox.LockKeyPress = controlLook;
            this.ReceivedFromTextBox.LockKeyPress = controlLook;
            this.DefaultCommentTextBox.LockKeyPress = controlLook;
            //this.MiscItemGrid.Enabled = controlLook;
        }
        /// <summary>
        /// Enables the panel control.
        /// </summary>
        /// <param name="view">if set to <c>true</c> [view].</param>
        private void EnablePanelControl(bool view)
        {
            this.DescriptionPanel.Enabled = view;
            this.ReceviedFromPanel.Enabled = view;
            this.DefaultCommentPanel.Enabled = view;
            //this.ReceiptItemPanel.Enabled = view;
        }
        private void LoadDefaultView()
        {
            this.EnableControls(true);
            this.EnablePanelControl(true);
            this.ControlLock(false);
            this.MiscItemGrid.Enabled = true;
            this.F16072LoadFormDetails();
        }

        /// <summary>
        /// Permissions the control lock.
        /// </summary>
        /// <param name="controlLook">if set to <c>true</c> [control look].</param>
        private void PermissionControlLock(bool controlLook)
        {
            this.ControlLock(controlLook);
        }
        private void F16072LoadFormDetails()
        {
            this.MiscGridDataSet.Clear();
            this.MiscGridDataSet = form16072Control.WorkItem.F16072_GetMiscteplateDetails(Convert.ToInt32(MiscTemplateID));
            if (this.MiscGridDataSet.Tables["f11072_Get_MiscReceiptTemplate"].Rows.Count > 0)
            {
                this.DescriptionTextBox.Text = this.MiscGridDataSet.Tables["f11072_Get_MiscReceiptTemplate"].Rows[0]["Description"].ToString();
                this.ReceivedFromTextBox.Text = this.MiscGridDataSet.Tables["f11072_Get_MiscReceiptTemplate"].Rows[0]["ReceivedFrom"].ToString();
                this.DefaultCommentTextBox.Text = this.MiscGridDataSet.Tables["f11072_Get_MiscReceiptTemplate"].Rows[0]["DefaultComment"].ToString();



                //Binding DataSource to MiscItem Grid
                this.MiscItemGrid.UpdateMode = UpdateMode.OnUpdate;

                this.MiscItemGrid.DataSource = this.MiscGridDataSet.Tables["f11072Get_MiscGridDetailsTable"].DefaultView;
                this.MiscItemGrid.DataBind();
                this.MiscItemGrid.UpdateData();

                ////Form Master Permissions
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
        
        #endregion       
             

        #region Check For Errors
        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns></returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            if (string.IsNullOrEmpty(this.DescriptionTextBox.Text.Trim()))
            {
                this.DescriptionTextBox.Focus();
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AcceptChanges();
            foreach (DataRow row in this.MiscGridDataSet.f11072Get_MiscGridDetailsTable)
            {
                //if(row.ItemArray.
            }
            DataTable dt = this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Copy();
            if (this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.Count == 0)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptTemplateMissingField");
                this.MiscItemGrid.Focus();
                return sliceValidationFields;
            }          
          
            DataView tempDataView = new DataView(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable, string.Concat(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountIDColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AccountNameColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.CodeColumn.ColumnName, " IS NOT NULL OR ", this.miscReceipt.ListReceiptItem.DescriptionColumn.ColumnName, " IS NOT NULL OR ", this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.AmountColumn.ColumnName, " IS NOT NULL"), "", DataViewRowState.CurrentRows);
            for (int i = 0; i < this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows.Count; i++)
            {
                
                if (string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["AccountName"].ToString()))
                {
                    if (! string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Description"].ToString()) || !string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Code"].ToString()) || !string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Amount"].ToString()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptTemplateMissingField");
                        this.MiscItemGrid.Focus();
                        return sliceValidationFields;
                    }
                    else if (string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Description"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Code"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["AccountID"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["AccountName"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["MiscItemID"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["Amount"].ToString()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptTemplateMissingField");
                        this.MiscItemGrid.Focus();
                        return sliceValidationFields;
                    }
                    else if(string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["AccountName"].ToString()) && string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["AccountID"].ToString()) && (!string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[i]["MiscItemID"].ToString())))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MiscReceiptTemplateMissingField");
                        this.MiscItemGrid.Focus();
                        return sliceValidationFields;
                    }
                }
            }            
            return sliceValidationFields;
        } 
        #endregion

        #region Edit Events
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
        /// Handles the TextChanged event of the DefaultCommentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DefaultCommentTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
            }
            catch
            {

            }
        } 
        #endregion

        #region Picture Box Events

        /// <summary>
        /// Handles the MouseClick event of the MiscReceiptTemplatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void MiscReceiptTemplatePictureBox_MouseClick(object sender, MouseEventArgs e)
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
        /// Handles the MouseHover event of the MiscReceiptTemplatePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MiscReceiptTemplatePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.MiscReceiptTemplateToolTip.SetToolTip(this.MiscReceiptTemplatePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        } 
        #endregion

        private void MiscItemGrid_AfterRowsDeleted(object sender, EventArgs e)
        {
            if (miscId.Count > 0)
            {
                string returnXML = RemoveXMLItem(miscId);
                if (!returnXML.Equals("<Root />"))
                {
                    this.form16072Control.WorkItem.F16072_DeleteMiscGridtemplate(Convert.ToInt32(this.MiscTemplateID), returnXML, TerraScanCommon.UserId);
                    this.miscId.Clear();
                    this.SetEditRecord();
                }
            }
             this.CustomizeGrid();
             //this.MiscItemGrid.DataSource = this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.DefaultView;
             //this.MiscItemGrid.UpdateData();                 

            //this.MiscItemGrid.Selected.Rows.AddRange((UltraGridRow[])this.MiscItemGrid.Rows.All);
            //this.MiscItemGrid.DeleteSelectedRows(false);
            
        }

        private void MiscItemGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                int recordcount = this.MiscItemGrid.Selected.Rows.Count;
                if (recordcount > 0)
                {
                    foreach (UltraGridRow rowSelected in this.MiscItemGrid.Selected.Rows)
                    {
                        var index = rowSelected.Index;

                        if (index < this.MiscItemGrid.Rows.Count)
                        {

                            if (!string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["MiscItemID"].ToString()))
                            {
                                miscId.Add(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["MiscItemID"].ToString());
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void MiscItemGrid_KeyUp(object sender, KeyEventArgs e)
        {
            //int recordcount = this.MiscItemGrid.Selected.Rows.Count;
            //ArrayList indexList = new ArrayList();
            //this.miscId.Clear();
            //if (recordcount > 0)
            //{
            //    foreach (UltraGridRow rowSelected in this.MiscItemGrid.Selected.Rows)
            //    {

            //        var index = rowSelected.Index;

            //        if (!string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["AccountName"].ToString()))
            //        {
            //            indexList.Add(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["AccountName"].ToString());
            //        }
            //        if (!string.IsNullOrEmpty(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["MiscItemID"].ToString()))
            //        {
            //            miscId.Add(this.MiscGridDataSet.f11072Get_MiscGridDetailsTable.Rows[index]["MiscItemID"].ToString());
            //        }
            //    }
                
            //}
        }


        private string RemoveXMLItem(ArrayList miscIds)
        {
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("MiscItemID", typeof(Int32));
            foreach (var r in miscIds)
            {
                dt.Rows.Add(r);
            }
            dt = dt.DefaultView.ToTable(true, "MiscItemID");        

            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string removexmlstr;
            removexmlstr = sr.ReadToEnd();
            removexmlstr = removexmlstr.Replace("DocumentElement", "Root");
            // xmlstr = xmlstr.Replace("ParcelTable", "Table");
            return (removexmlstr);
        }
    }
}
