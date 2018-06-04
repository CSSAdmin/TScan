//--------------------------------------------------------------------------------------------
// <copyright file="F36032.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 FS Land Codes.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/09/2007       VijayaKumar.M              Created
//***********************************************************************************************/
namespace D36030
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
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infrastructure.Interface;

    /// <summary>
    /// F36032 Class File.
    /// </summary>
    [SmartPart]
    public partial class F36032 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;      

        /// <summary>
        /// Used to store the landCodesData
        /// </summary>
        private F36032LandCodesData landCodesData = new F36032LandCodesData();

        /// <summary>
        /// Used to store the istLandCodeDataTable
        /// </summary>
        private F36032LandCodesData.ListLandCodeDataTable listReportAsDataTable = new F36032LandCodesData.ListLandCodeDataTable();

        /// <summary>
        /// Used to store the listLandCodeDetailsDatatable
        /// </summary>
        private F36032LandCodesData.ListLandCodeDetailsDataTable listLandCodeDetailsDatatable = new F36032LandCodesData.ListLandCodeDetailsDataTable();

        /// <summary>
        /// Used to store the listLandType1
        /// </summary>
        private F36032LandCodesData.ListLandType1DataTable listLandType1 = new F36032LandCodesData.ListLandType1DataTable();

        /// <summary>
        /// Used to store the listLandType2
        /// </summary>
        private F36032LandCodesData.ListLandType2DataTable listLandType2 = new F36032LandCodesData.ListLandType2DataTable();

        /// <summary>
        /// Used to store the listLandType3
        /// </summary>
        private F36032LandCodesData.ListLandType3DataTable listLandType3 = new F36032LandCodesData.ListLandType3DataTable();

        /// <summary>
        /// Used to store the listReportAsDataTableOnFormLoad
        /// </summary>
        private DataTable listReportAsDataTableOnFormLoad = new DataTable();

        /// <summary>
        /// Used to store the listLandType1OnFormLoad
        /// </summary>
        private DataTable listLandType1OnFormLoad = new DataTable();

        /// <summary>
        /// Used to store the listLandType2OnFormLoad
        /// </summary>
        private DataTable listLandType2OnFormLoad = new DataTable();

        /// <summary>
        /// Used to store the listLandType3OnFormLoad
        /// </summary>
        private DataTable listLandType3OnFormLoad = new DataTable();

        /// <summary>
        /// Used to store the valueSliceId (Dummy variable will not be used, get from form master)
        /// </summary>
        private int valueSliceId;       

        /// <summary>
        /// controller F36032
        /// </summary>
        private F36032Controller form36032Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Usde to store the currentRollYear
        /// </summary>
        private string currentRollYear = string.Empty;

        /// <summary>
        /// Used store the setButtonModeOnformLoad
        /// </summary>
        private bool setButtonModeOnformLoad;

        /// <summary>
        /// Used to store the avoidEditMode
        /// </summary>
        private bool avoidEditMode;       

        /// <summary>
        /// Used to store the landCodeId
        /// </summary>
        private int landCodeId;

        /// <summary>
        /// Used to store the savedLandCodeId
        /// </summary>
        private int savedLandCodeId;

        /// <summary>
        /// used to store the savedLandCodeRollYear
        /// </summary>
        private string savedLandCodeRollYear = string.Empty;

        /// <summary>
        /// used to store landCodeGridBindingSource
        /// </summary>
        private BindingSource landCodeGridBindingSource = new BindingSource();

        /// <summary>
        /// Used to track Rollyear is edited or not
        /// Added by Biju on 19/Nov/2009 to fix #4780
        /// </summary>
        private bool isRollYearChanged;  

        private bool gridclickflag = false; 

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
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36032"/> class.
        /// </summary>
        public F36032()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36033"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36032(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.valueSliceId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.LandCodePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandCodePictureBox.Height, this.LandCodePictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructors

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;   

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event D84700_F84721_OnSave_GetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84721_OnSave_GetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_GetKeyId;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form36032 control.
        /// </summary>
        /// <value>The form36032 control.</value>
        [CreateNew]
        public F36032Controller Form36032Control
        {
            get { return this.form36032Control as F36032Controller; }
            set { this.form36032Control = value; }
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    eventArgs.Data.FlagInvalidSliceKey = false;

                    if (this.setButtonModeOnformLoad)
                    {
                        this.setButtonModeOnformLoad = false;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.valueSliceId = eventArgs.Data.SelectedKeyId;
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButtonClick();
                    break;
                case "SAVE":
                    this.SaveButtonClick();
                    break;
                case "CANCEL":
                    this.CancelButtonClick();
                    break;
                case "DELETE":
                    this.DeleteButtonClick();
                    break;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            try
            {
                if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
                {
                    eventArgs.Data.FlagFormClose = this.CheckPageStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event Subscription

        #region Protected methods

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }

        #endregion Protected methods

        #region Form Events

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36032 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36032_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.LoadLandCodedetailsGrid();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
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

        #region LandCodePictureBox Events

        /// <summary>
        /// Handles the Click event of the LandCodePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodePictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the LandCodePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandCodeFormSliceToolTip.SetToolTip(this.LandCodePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion LandCodePictureBox Events

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;                
                this.RollYearTextBox.Text = string.Empty;
                ////combo box value are loaded based on the Roll year
                this.LoadComBoBoxBasedOnRollYear();
                
                this.ControlLock(!this.PermissionFiled.newPermission);
                this.LandCodeHeaderPanel.Enabled = true;
                
                this.ClearLandCodeHeaderControls();               
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.RollYearTextBox.Focus();
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
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButtonClick()
        {
            try
            {
                if (this.landCodeId > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int deletedReturnedValue = this.form36032Control.WorkItem.F36032_DeleteLandCode(this.landCodeId, TerraScan.Common.TerraScanCommon.UserId);

                        ////when the land code is deleted then reload the form
                        if (deletedReturnedValue > 0)
                        {
                            this.LoadLandCodedetailsGrid();

                            ////to reload the f36033 form slice afther delete in f36032 form slice
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = this.valueSliceId;
                            this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F36032_DeleteValidateMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        return;
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
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButtonClick()
        {
            try
            {
                this.LandCodedetailsDataGrid.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.LoadLandCodedetailsGrid();
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
        /// Saves the button_ click.
        /// </summary>
        private void SaveButtonClick()
        {
            try
            {
                int currentRowIndexValue;
                if (this.SaveLandCodeDetails())
                {
                    this.LandCodedetailsDataGrid.Enabled = true;
                    this.LoadLandCodedetailsGrid();
                    this.landCodeGridBindingSource.DataSource = this.listLandCodeDetailsDatatable.DefaultView;

                    if (this.listLandCodeDetailsDatatable.Rows.Count > 0 && !string.IsNullOrEmpty(this.savedLandCodeRollYear))
                    {
                        this.FilterLandCodeGrid(this.savedLandCodeRollYear);
                    }

                    currentRowIndexValue = this.landCodeGridBindingSource.Find(this.listLandCodeDetailsDatatable.LandCodeIDColumn.ColumnName, this.savedLandCodeId);
                    if (currentRowIndexValue >= 0)
                    {
                        this.PopulateLandCodeHeaderPartControls(currentRowIndexValue);
                        this.LandCodedetailsDataGrid.Rows[currentRowIndexValue].Selected = true;
                        this.LandCodedetailsDataGrid.Rows[currentRowIndexValue].Activated = true;
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                    ////to reload the f36033 form slice afther save in f36032 form slice
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = this.valueSliceId;
                    this.FormSlice_OnSave_GetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentSliceInfo));
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
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Controls_TextChangedEvent(object sender, EventArgs e)
        {
            //try
            //{
            //    this.EditEnabled();
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the BeforeRowActivate event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void LandCodedetailsDataGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.PopulateLandCodeHeaderPartControls(e.Row.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
                ////Added by Biju on 19/Nov/2009 to fix #4780
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New ))
                    this.isRollYearChanged = true;
                ////till here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Private Methods

        /// <summary>
        /// Used to custimize the All combo box in the Load code Value Form
        /// </summary>
        private void CustimizeAllLandCodeComboBoxs()
        {
            this.LandType1ComboBox.MaxLength = this.listLandCodeDetailsDatatable.LandType1Column.MaxLength;

            this.LandType2ComboBox.MaxLength = this.listLandCodeDetailsDatatable.LandType2Column.MaxLength;

            this.LandType3ComboBox.MaxLength = this.listLandCodeDetailsDatatable.LandType3Column.MaxLength;

            this.LandCodeTextBox.MaxLength = this.listLandCodeDetailsDatatable.LandCodeColumn.MaxLength;

            this.ReportASComboBox.MaxLength = this.listLandCodeDetailsDatatable.ReportASColumn.MaxLength;

            this.DescriptionTextBox.MaxLength = this.listLandCodeDetailsDatatable.DescriptionColumn.MaxLength;

            this.LandType1ComboBox.DisplayMember = this.listLandType1.LandTypeColumn.ColumnName;
            this.LandType1ComboBox.ValueMember = this.listLandType1.LandTypeIDColumn.ColumnName;

            this.LandType2ComboBox.DisplayMember = this.listLandType2.LandTypeColumn.ColumnName;
            this.LandType2ComboBox.ValueMember = this.listLandType2.LandTypeIDColumn.ColumnName;

            this.LandType3ComboBox.DisplayMember = this.listLandType3.LandTypeColumn.ColumnName;
            this.LandType3ComboBox.ValueMember = this.listLandType3.LandTypeIDColumn.ColumnName;

            //this.ReportASComboBox.DisplayMember = this.listReportAsDataTable.LandCodeColumn.ColumnName;
            //this.ReportASComboBox.ValueMember = this.listReportAsDataTable.LandCodeColumn.ColumnName;
          
        
        }

        /// <summary>
        /// Used to load all the Combo box in the Land cod evalue form slice
        /// </summary>
        private void LoadAllLandCodeComboBoxs()
        {
            if (this.listReportAsDataTable.Rows.Count > 0)
            {
                F36032LandCodesData.ListLandCodeDataTable listLandType1ComboBoxDatatable = new F36032LandCodesData.ListLandCodeDataTable();
                DataRow customRow = listLandType1ComboBoxDatatable.NewRow();
                customRow[this.listLandCodeDetailsDatatable.LandCodeColumn.ColumnName] = string.Empty;

                listLandType1ComboBoxDatatable.Rows.Add(customRow);
                listLandType1ComboBoxDatatable.Merge(this.listReportAsDataTable);

                if (listLandType1ComboBoxDatatable.Rows.Count > 0)
                {
                    this.ReportASComboBox.DisplayMember = this.listReportAsDataTable.LandCodeColumn.ColumnName;
                    this.ReportASComboBox.ValueMember = this.listReportAsDataTable.LandCodeColumn.ColumnName;

                    this.ReportASComboBox.DataSource = listLandType1ComboBoxDatatable;
                }
                else
                {
                    listLandType1ComboBoxDatatable.Clear();
                    this.ReportASComboBox.DataBindings.Clear();
                    this.ReportASComboBox.DataSource = null;
                    this.ReportASComboBox.DisplayMember = this.listLandCodeDetailsDatatable.ReportASColumn.ColumnName;
                    this.ReportASComboBox.ValueMember = this.listLandCodeDetailsDatatable.ReportASColumn.ColumnName;
                    this.ReportASComboBox.DataSource = this.listLandCodeDetailsDatatable;
                }
            }
            else
            {
                this.ReportASComboBox.DataSource = this.listReportAsDataTable;
            }

            if (this.listLandType1.Rows.Count > 0)
            {
                F36032LandCodesData.ListLandType1DataTable listLandType1ComboxDatatable = new F36032LandCodesData.ListLandType1DataTable();
                DataRow listLandType1Row = listLandType1ComboxDatatable.NewRow();
                listLandType1Row[listLandType1ComboxDatatable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType1Row[listLandType1ComboxDatatable.LandTypeIDColumn.ColumnName] = "0";
                listLandType1ComboxDatatable.Rows.Add(listLandType1Row);
                listLandType1ComboxDatatable.Merge(this.listLandType1);
                this.LandType1ComboBox.DataSource = listLandType1ComboxDatatable;
            }
            else
            {
                this.LandType1ComboBox.DataSource = this.listLandType1;
            }

            if (this.listLandType2.Rows.Count > 0)
            {
                F36032LandCodesData.ListLandType2DataTable listLandType2ComboBoxDataTable = new F36032LandCodesData.ListLandType2DataTable();
                DataRow listLandType2Row = listLandType2ComboBoxDataTable.NewRow();
                listLandType2Row[listLandType2ComboBoxDataTable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType2Row[listLandType2ComboBoxDataTable.LandTypeIDColumn.ColumnName] = "0";
                listLandType2ComboBoxDataTable.Rows.Add(listLandType2Row);
                listLandType2ComboBoxDataTable.Merge(this.listLandType2);
                this.LandType2ComboBox.DataSource = listLandType2ComboBoxDataTable;
            }
            else
            {
                this.LandType2ComboBox.DataSource = this.listLandType2;
            }

            if (this.listLandType3.Rows.Count > 0)
            {
                F36032LandCodesData.ListLandType3DataTable listLandType3ComboBoxDataTable = new F36032LandCodesData.ListLandType3DataTable();
                DataRow listLandType3Row = listLandType3ComboBoxDataTable.NewRow();
                listLandType3Row[listLandType3ComboBoxDataTable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType3Row[listLandType3ComboBoxDataTable.LandTypeIDColumn.ColumnName] = "0";
                listLandType3ComboBoxDataTable.Rows.Add(listLandType3Row);
                listLandType3ComboBoxDataTable.Merge(this.listLandType3);
                this.LandType3ComboBox.DataSource = listLandType3ComboBoxDataTable;
            }
            else
            {
                this.LandType3ComboBox.DataSource = this.listLandType3;
            }
        }

        /// <summary>
        /// Usde to store the Grid and header paer
        /// </summary>
        private void ClearLandCodeGriddetails()
        {
            this.landCodeId = -999;
            this.setButtonModeOnformLoad = true;
            this.ClearLandCodeHeaderControls();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            this.LandCodeHeaderPanel.Enabled = false;
            ////this.additionalOperationSmartPart.Enabled = false;
        }

        /// <summary>
        /// landtype2ComboBoxSelectedValue
        /// </summary>
        /// <param name="landtype1ComboBoxSelectedValue">landtype1ComboBoxSelectedValue</param>
        /// <param name="landtype2ComboBoxSelectedValue">landtype2ComboBoxSelectedValue</param>
        /// <param name="landtype3ComboBoxSelectedValue">landtype3ComboBoxSelectedValue</param>
        /// <param name="reportAsComboBoxSelectedValue">landtype4ComboBoxSelectedValue</param>
        private void PopulateAllLandCodeComboBoxValues(string landtype1ComboBoxSelectedValue, string landtype2ComboBoxSelectedValue, string landtype3ComboBoxSelectedValue, string reportAsComboBoxSelectedValue)
        {
            if (!string.IsNullOrEmpty(landtype1ComboBoxSelectedValue))
            {
                this.LandType1ComboBox.SelectedValue = landtype1ComboBoxSelectedValue;
            }
            else
            {
                this.LandType1ComboBox.SelectedValue = -1;
                this.LandType1ComboBox.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(landtype2ComboBoxSelectedValue))
            {
                this.LandType2ComboBox.SelectedValue = landtype2ComboBoxSelectedValue;
            }
            else
            {
                this.LandType2ComboBox.SelectedValue = -1;
                this.LandType2ComboBox.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(landtype3ComboBoxSelectedValue))
            {
                this.LandType3ComboBox.SelectedValue = landtype3ComboBoxSelectedValue;
            }
            else
            {
                this.LandType3ComboBox.SelectedValue = -1;
                this.LandType3ComboBox.Text = string.Empty;
            }

            if (!string.IsNullOrEmpty(reportAsComboBoxSelectedValue))
            {
                this.ReportASComboBox.SelectedValue = reportAsComboBoxSelectedValue;
            }
            else
            {
                this.ReportASComboBox.SelectedIndex = -1;
                this.ReportASComboBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Used to clear the Land code form slice Header Controls
        /// </summary>
        private void ClearLandCodeHeaderControls()
        {
            this.landCodeId = -999;

            this.avoidEditMode = true;

            this.RollYearTextBox.Text = string.Empty;
            this.listLandType1.Clear();
            this.listLandType2.Clear();
            this.listLandType3.Clear();
            this.listReportAsDataTable.Clear();

            this.PopulateAllLandCodeComboBoxValues(string.Empty, string.Empty, string.Empty, string.Empty);
            this.LandCodeTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.avoidEditMode = false;
            this.gridclickflag = false;
        }

        /// <summary>
        /// To load the land code grid and other deatils
        /// </summary>
        private void LoadLandCodedetailsGrid()
        {
            this.flagLoadOnProcess = true;

            this.landCodesData.Clear();
            this.listLandType1.Clear();
            this.listLandType2.Clear();
            this.listLandType3.Clear();
            this.listReportAsDataTable.Clear();

            this.CustimizeAllLandCodeComboBoxs();

            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].SortedColumns.Clear();

            this.landCodesData = this.form36032Control.WorkItem.F36032_ListLandItems(null);
            this.listLandType1 = this.landCodesData.ListLandType1;
            this.listLandType2 = this.landCodesData.ListLandType2;
            this.listLandType3 = this.landCodesData.ListLandType3;
            this.listReportAsDataTable = this.landCodesData.ListLandCode;

            ////used to populate the combo box based on the roll year
            this.listReportAsDataTableOnFormLoad = this.listReportAsDataTable.Copy();
            this.listLandType1OnFormLoad = this.listLandType1.Copy();
            this.listLandType2OnFormLoad = this.listLandType2.Copy();
            this.listLandType3OnFormLoad = this.listLandType3.Copy();

            ////to get theapplication roll year
            if (this.landCodesData.GetConfigRollYear.Rows.Count > 0)
            {
                this.currentRollYear = this.landCodesData.GetConfigRollYear.Rows[0][this.landCodesData.GetConfigRollYear.AssessmentRollYearColumn].ToString();
            }

            this.LoadAllLandCodeComboBoxs();

            ////to load the land code grid 
            this.landCodesData = this.form36032Control.WorkItem.F36032_ListLandCodeDetails();
            this.listLandCodeDetailsDatatable = this.landCodesData.ListLandCodeDetails;

            this.LandCodedetailsDataGrid.DataSource = this.listLandCodeDetailsDatatable;

            if (this.listLandCodeDetailsDatatable.Rows.Count > 0)
            {
                this.LandCodeGridPanel.Enabled = true;
                this.LandCodedetailsDataGrid.Focus();

                ////to filter the datatset containing the roll year
                if (!string.IsNullOrEmpty(this.currentRollYear))
                {
                    this.FilterLandCodeGrid(this.currentRollYear);
                }
                else
                {
                    this.LandCodedetailsDataGrid.Rows[0].Selected = true;                  
                }
            }
            else
            {   
                this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.ClearLandCodeGriddetails();
                this.LandCodeGridPanel.Enabled = false;
            }

            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Used to filter the land code
        /// </summary>
        /// <param name="filterRollYear">filterRollYear</param>
        private void FilterLandCodeGrid(string filterRollYear)
        {            
            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
            this.LandCodedetailsDataGrid.DisplayLayout.Bands[0].ColumnFilters[this.listLandCodeDetailsDatatable.RollYearColumn.ColumnName].FilterConditions.Add(FilterComparisionOperator.StartsWith, filterRollYear);
            UltraGridRow[] filteredRow = this.LandCodedetailsDataGrid.Rows.GetFilteredInNonGroupByRows();

            if (filteredRow.Length > 0)
            {
                filteredRow[0].Selected = true;
                filteredRow[0].Activated = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);               
            }
            else
            {
                this.ClearLandCodeGriddetails();
            }            
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form36032Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form36032Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form36032Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.OperationSmartpartDeckWorkSpace.Show(this.operationSmartPart);
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>
        /// true - for continuing/false - leave unsaved changes
        /// </returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("F36032_LandCodeFormName"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SaveLandCodeDetails())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelButtonClick();

                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// To Save the land code values.
        /// </summary>
        /// <returns>Boolean</returns>
        private bool SaveLandCodeDetails()
        {
            int saveRollyear;
            int.TryParse(this.RollYearTextBox.Text.Trim(), out saveRollyear);
            if ((!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()) && saveRollyear > 0) && !string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim()))
            {
                this.landCodesData.SaveLandCodeDetails.Rows.Clear();
                F36032LandCodesData.SaveLandCodeDetailsRow dr = this.landCodesData.SaveLandCodeDetails.NewSaveLandCodeDetailsRow();
                if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
                {
                    dr.RollYear = saveRollyear;
                }

                if (!string.IsNullOrEmpty(this.LandType1ComboBox.Text.Trim()))
                {
                    dr.LandTypeID1 = Convert.ToInt32(this.LandType1ComboBox.SelectedValue);
                }

                if (!string.IsNullOrEmpty(this.LandType2ComboBox.Text.Trim()))
                {
                    dr.LandTypeID2 = Convert.ToInt32(this.LandType2ComboBox.SelectedValue);
                }

                if (!string.IsNullOrEmpty(this.LandType3ComboBox.Text.Trim()))
                {
                    dr.LandTypeID3 = Convert.ToInt32(this.LandType3ComboBox.SelectedValue);
                }

                dr.LandCode = this.LandCodeTextBox.Text.Trim();
                dr.ReportAS = this.ReportASComboBox.Text.Trim();
                dr.Description = this.DescriptionTextBox.Text.Trim();
                this.savedLandCodeRollYear = this.RollYearTextBox.Text.Trim();
                this.landCodesData.SaveLandCodeDetails.Rows.Add(dr);
                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    this.savedLandCodeId = this.form36032Control.WorkItem.F36032_SaveLandCodeDetails(null, TerraScanCommon.GetXmlString(this.landCodesData.SaveLandCodeDetails.Copy()), TerraScanCommon.UserId);
                }
                else
                {   
                    this.savedLandCodeId = this.form36032Control.WorkItem.F36032_SaveLandCodeDetails(this.landCodeId, TerraScanCommon.GetXmlString(this.landCodesData.SaveLandCodeDetails.Copy()), TerraScanCommon.UserId);
                }

                ////when the savedlandcodeid value are not Unique then -2 will be returned                    
                if (this.savedLandCodeId > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F36032_ValidationMessage1"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Used to Populate the Land Code header part controls
        /// </summary>
        /// <param name="rowIndex">rowIndex</param>
        private void PopulateLandCodeHeaderPartControls(int rowIndex)
        {
            if (this.listLandCodeDetailsDatatable.Rows.Count > 0 && rowIndex >= 0 && this.LandCodedetailsDataGrid.Rows.VisibleRowCount > 1)
            {
                this.avoidEditMode = true;

                int.TryParse(this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.LandCodeIDColumn.ColumnName].Value.ToString(), out this.landCodeId);
                this.RollYearTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.RollYearColumn.ColumnName].Value.ToString();

                this.LoadComBoBoxBasedOnRollYear();

                this.PopulateAllLandCodeComboBoxValues(this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.LandTypeID1Column.ColumnName].Value.ToString(), this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.LandTypeID2Column.ColumnName].Value.ToString(), this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.LandTypeID3Column.ColumnName].Value.ToString(), this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.ReportASColumn.ColumnName].Value.ToString());
                this.LandCodeTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.LandCodeColumn.ColumnName].Value.ToString();
                this.DescriptionTextBox.Text = this.LandCodedetailsDataGrid.Rows[rowIndex].Cells[this.listLandCodeDetailsDatatable.DescriptionColumn.ColumnName].Value.ToString();

                this.LandCodeHeaderPanel.Enabled = true;                

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

                this.avoidEditMode = false;
            }
            else
            {
                this.ClearLandCodeHeaderControls();
                this.landCodeId = -999;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);                
                this.LandCodeHeaderPanel.Enabled = false;
            }
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit && !this.flagLoadOnProcess && !this.avoidEditMode && !this.gridclickflag)
            {
                this.LandCodedetailsDataGrid.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.RollYearTextBox.LockKeyPress = controlLook;
            this.LandCodeTextBox.LockKeyPress = controlLook;
            this.DescriptionTextBox.LockKeyPress = controlLook;

            this.LandType1ComboBox.Enabled = !controlLook;
            this.LandType2ComboBox.Enabled = !controlLook;
            this.LandType3ComboBox.Enabled = !controlLook;
            this.ReportASComboBox.Enabled = !controlLook;         
        }

        /// <summary>
        /// Loads the COMbo box based on roll year.
        /// </summary>
        private void LoadComBoBoxBasedOnRollYear()
        {
            int temprollyear;
            ////for current roll year text is used to select DISTINCT  value for perticular roll year
            F36032LandCodesData.ListLandCodeDetailsDataTable tempListReportAsDataTable = new F36032LandCodesData.ListLandCodeDetailsDataTable();
            F36032LandCodesData.ListLandType1DataTable tempListLandType1DataTable = new F36032LandCodesData.ListLandType1DataTable();
            F36032LandCodesData.ListLandType2DataTable tempListLandType2DataTable = new F36032LandCodesData.ListLandType2DataTable();
            F36032LandCodesData.ListLandType3DataTable tempListLandType3DataTable = new F36032LandCodesData.ListLandType3DataTable();

            string filtereCondition;
            int.TryParse(this.RollYearTextBox.Text.Trim(), out temprollyear);

            if (!string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                filtereCondition = SharedFunctions.GetResourceString("F36032_RollYearFilter") + temprollyear;
            }
            else
            {
                filtereCondition = SharedFunctions.GetResourceString("F36032_RollYearNotNullValidation");
            }

            DataRow[] listReportAsdatatableRowCollection = this.listReportAsDataTableOnFormLoad.Select(filtereCondition);
            DataRow[] listLandType1DataTableRowCollection = this.listLandType1OnFormLoad.Select(filtereCondition);
            DataRow[] listLandType2datatableRowCollection = this.listLandType2OnFormLoad.Select(filtereCondition);
            DataRow[] listLandType3DataTableRowCollection = this.listLandType3OnFormLoad.Select(filtereCondition);

            foreach (DataRow currentreportAsRow in listReportAsdatatableRowCollection)
            {
                tempListReportAsDataTable.ImportRow(currentreportAsRow);
            }

            foreach (DataRow currentlistLandType1Row in listLandType1DataTableRowCollection)
            {
                tempListLandType1DataTable.ImportRow(currentlistLandType1Row);
            }

            foreach (DataRow currentlistLandType2eRow in listLandType2datatableRowCollection)
            {
                tempListLandType2DataTable.ImportRow(currentlistLandType2eRow);
            }

            foreach (DataRow currentlistLandType3Row in listLandType3DataTableRowCollection)
            {
                tempListLandType3DataTable.ImportRow(currentlistLandType3Row);
            }

            this.listLandType1.Clear();
            this.listLandType2.Clear();
            this.listLandType3.Clear();
            this.listReportAsDataTable.Clear();

            this.listReportAsDataTable.Merge(tempListReportAsDataTable);
            this.listLandType1.Merge(tempListLandType1DataTable);
            this.listLandType2.Merge(tempListLandType2DataTable);
            this.listLandType3.Merge(tempListLandType3DataTable);
            this.LoadAllLandCodeComboBoxs();
            this.PopulateAllLandCodeComboBoxValues(string.Empty, string.Empty, string.Empty, string.Empty);
        }

        #endregion Private Methods

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LandType1ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandType1ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LandCodedetailsDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodedetailsDataGrid_Click(object sender, EventArgs e)
        {
            this.gridclickflag = true;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LandType2ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandType2ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LandType3ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandType3ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the LandCodeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandCodeTextBox_TextChanged(object sender, EventArgs e)
        {

            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ReportASComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportASComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {              
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the DescriptionTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DescriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.gridclickflag = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the RollYearTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void RollYearTextBox_Validating(object sender, CancelEventArgs e)
        {
            ////Commented by Biju on 19/Nov/2009 to fix #4780
            ////if (!this.flagLoadOnProcess && !this.avoidEditMode)
            ////Added by Biju on 19/Nov/2009 to fix #4780
            if (!this.flagLoadOnProcess && !this.avoidEditMode && this.isRollYearChanged)
            {
                this.LoadComBoBoxBasedOnRollYear();
                ////Added by Biju on 19/Nov/2009 to fix #4780
                this.isRollYearChanged = false;
            }
        }

        /// <summary>
        /// Handles the Validating event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ComboBox_Validating(object sender, CancelEventArgs e)
        {
            TerraScanComboBox currentComboBox = (TerraScanComboBox)sender;
            if (currentComboBox.SelectedValue == null)
            {
                currentComboBox.Text = string.Empty;
            }
        }

        private void LandType1ComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandType2ComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void LandType3ComboBox_DropDown(object sender, EventArgs e)
        {
            try
            {
                ComboBox senderComboBox = (ComboBox)sender;
                int width = senderComboBox.DropDownWidth;
                Graphics g = senderComboBox.CreateGraphics();
                Font font = senderComboBox.Font;
                int vertScrollBarWidth =
                    (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth;
                foreach (DataRowView s in ((ComboBox)sender).Items)
                {
                    newWidth = (int)g.MeasureString(s.ToString(), font).Width + vertScrollBarWidth;
                    if (width < newWidth)
                    {
                        width = newWidth;
                    }
                }
                senderComboBox.DropDownWidth = width;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

    }
}
