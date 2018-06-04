//--------------------------------------------------------------------------------------------
// <copyright file="F36061.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36061 FS Depreciation Control Tables.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			  Author		       Description
// ----------	  ---------		       ---------------------------------------------------------
// 11/02/2007     VijayaKumar.M        Created
// 21/04/2009     Shanmuga Sundaram.A  Modified to implement CO:5733
//***********************************************************************************************/

namespace D36060
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
    using Infrastructure.Interface;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;

    /// <summary>
    /// F36061 class file
    /// </summary>
    [SmartPart]
    public partial class F36061 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// used to store the keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// controller F15015
        /// </summary>
        private F36061Controller form36061Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store the depreciationControlData
        /// </summary>
        private F36061DepreciationControlData depreciationControlData = new F36061DepreciationControlData();

        /// <summary>
        /// Used to store the listDeprDataTable(will be used to bind to dropdown of the grid control)
        /// </summary>
        private F36061DepreciationControlData.ListDeprDataTable listDeprDataTable = new F36061DepreciationControlData.ListDeprDataTable();

        /// <summary>
        /// Used to store the listDeprControlItemsDataTable(will be bind to Deprication Grid Control)
        /// </summary>
        private F36061DepreciationControlData.ListDeprControlItemsDataTable listDeprControlItemsDataTable = new F36061DepreciationControlData.ListDeprControlItemsDataTable();

        /// <summary>
        /// To check whether the key id valid are not
        /// </summary>
        private bool iskeyidValid;

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

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

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

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36061"/> class.
        /// </summary>
        public F36061()
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
        public F36061(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.DeprTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprTablePictureBox.Height, this.DeprTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

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

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F36061Control
        /// </summary>
        [CreateNew]
        public F36061Controller Form36061Control
        {
            get { return this.form36061Control as F36061Controller; }
            set { this.form36061Control = value; }
        }

        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertResizableSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertResizableSlice(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true && eventArgs.Data == this.masterFormNo)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.DeprTablePictureBox.Height;
                sliceResize.SliceFormHeight = this.DeprTablePictureBox.Height;
                this.DeprTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprTablePictureBox.Height, this.DeprTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
            if (this != null && this.IsDisposed != true && this.Visible)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.LoadDeprecationControl();
                this.DeprNeighborhoodlabel.Text = string.Empty;
                this.FormSliceResize();
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
            if (this != null && this.IsDisposed != true && this.Visible)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.LoadDeprecationControl();
                this.FormSliceResize();
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
            if (this != null && this.IsDisposed != true && this.Visible)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                int saveDeprValue = this.form36061Control.WorkItem.F36061_SaveDeprControlItems(this.keyId, TerraScanCommon.GetXmlString(this.listDeprControlItemsDataTable.Copy()), TerraScanCommon.UserId);

                if (saveDeprValue > 0)
                {
                    SliceReloadActiveRecord currentSliceInfo;
                    currentSliceInfo.MasterFormNo = this.masterFormNo;
                    currentSliceInfo.SelectedKeyId = saveDeprValue;
                    this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                }
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
            if (this != null && this.IsDisposed != true && this.Visible && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                /////to check whether the key id is valid are not
                if (this.iskeyidValid)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
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
            if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.LoadDeprecationControl();
                this.FormSliceResize();
            }
        }

        /// <summary>
        /// FormSlice_OnSave_SetKeyId
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true && this.Visible && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.LoadDeprecationControl();
                this.FormSliceResize();
            }
        }

        #endregion Event Subscription

        #region Protected Methods

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

        #endregion Protected Methods

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36061 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36061_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.LoadDeprecationControl();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        #endregion Form Load

        #region Methods

        /// <summary>
        /// Loads the deprecation control.
        /// </summary>
        private void LoadDeprecationControl()
        {
            this.listDeprDataTable.Clear();
            this.listDeprControlItemsDataTable.Clear();
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.depreciationControlData = this.form36061Control.WorkItem.F36061_ListDeprControlItems(this.keyId);
                this.listDeprControlItemsDataTable = this.depreciationControlData.ListDeprControlItems;
                if (this.depreciationControlData.GetDeprDescriptionTitle.Rows.Count > 0)
                {
                    this.DeprNeighborhoodlabel.Text = this.depreciationControlData.GetDeprDescriptionTitle.Rows[0][this.depreciationControlData.GetDeprDescriptionTitle.DeprTitleColumn].ToString();
                    this.iskeyidValid = true;
                }

                this.depreciationControlData = this.form36061Control.WorkItem.F36061_ListDepr(this.keyId);
                this.listDeprDataTable = this.depreciationControlData.ListDepr;
            }

            ////height of the form slice is chnaged
            ////height of the grid is chnged based on records
            this.DepreciationControlDataGrid.DataSource = this.listDeprControlItemsDataTable;

            this.SetFormSliceHeight(this.listDeprControlItemsDataTable.Rows.Count);
            if (this.listDeprControlItemsDataTable.Rows.Count > 0)
            {
                this.DepreciationControlDataGrid.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Sets the height of the form slice.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        private void SetFormSliceHeight(int rowCount)
        {
            if (rowCount > 5)
            {
                int setCurrentHeight = (rowCount - 5) * 21;
                this.EntireDepreciationTablePanel.Height = 155 + setCurrentHeight;

                this.DeprTablePictureBox.Height = this.EntireDepreciationTablePanel.Height;
                this.Height = this.DeprTablePictureBox.Height;
                this.DepreciationControlDataGrid.Height = 1000; //// 126 + setCurrentHeight;  

                this.DeprTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprTablePictureBox.Height, this.DeprTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            else
            {
                //// modified the code to implement the CO:5733  
                rowCount = 5;
                int setCurrentHeight = rowCount * 21;
                this.EntireDepreciationTablePanel.Height = 49 + setCurrentHeight;
                this.DeprTablePictureBox.Height = this.EntireDepreciationTablePanel.Height;
                this.Height = this.DeprTablePictureBox.Height;
                this.DepreciationControlDataGrid.Height = 1000; //// 126 + setCurrentHeight;
                this.DeprTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprTablePictureBox.Height, this.DeprTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }

            //// Added this method and removed the below code as its same.
            this.FormSliceResize();
            this.DepreciationControlDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
        }

        /// <summary>
        /// To resize the form slice.
        /// </summary>
        private void FormSliceResize()
        {
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.DeprTablePictureBox.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.DeprTablePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DeprTablePictureBox.Height, this.DeprTablePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the CellListSelect event of the DepreciationControlDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void DepreciationControlDataGrid_CellListSelect(object sender, CellEventArgs e)
        {
            try
            {
                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DoubleClickCell event of the DepreciationControlDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.DoubleClickCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationControlDataGrid_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {
            try
            {
                if ((e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low1Column.ColumnName].Index) || (e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low2Column.ColumnName].Index) || (e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low3Column.ColumnName].Index) || (e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low4Column.ColumnName].Index) || (e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low5Column.ColumnName].Index) || (e.Cell.Column.Index == this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low6Column.ColumnName].Index))
                {
                    ////on double click the drop down will not come
                    ////F36060 form slice is called
                    e.Cell.DroppedDown = false;

                    int deprtableId = 0;

                    if (e.Cell.Value != null)
                    {
                        int.TryParse(e.Cell.Value.ToString(), out deprtableId);
                    }

                    if (deprtableId > 0)
                    {
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(31060);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = deprtableId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the DeprTablePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprTablePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.F36061DeprToolTip.SetToolTip(this.DeprTablePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeprTablePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeprTablePictureBox_Click(object sender, EventArgs e)
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
        /// Handles the InitializeLayout event of the DepreciationControlDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void DepreciationControlDataGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            this.DeprGridDropDown.DataSource = this.listDeprDataTable;
            this.DeprGridDropDown.ValueMember = this.listDeprDataTable.ValueColumn.ColumnName;
            this.DeprGridDropDown.DisplayMember = this.listDeprDataTable.TextColumn.ColumnName;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low1Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low1Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low2Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low2Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low3Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low3Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low4Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low4Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low5Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low5Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low6Column.ColumnName].ValueList = this.DeprGridDropDown;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low6Column.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

            //// display style for combo in Band[0] to OnCellActivate
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low1Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low2Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low3Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low4Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low5Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;
            this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[this.listDeprControlItemsDataTable.Low6Column.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.OnCellActivate;

            ////when the edit permission does not exists the grid is not editable.
            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
            }
            else
            {
                this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
            }
        }

        /// <summary>
        /// Handles the BeforeCellListDropDown event of the DepreciationControlDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void DepreciationControlDataGrid_BeforeCellListDropDown(object sender, CancelableCellEventArgs e)
        {
            ////here columns are resizeble then drop down width is also adjusted
            if (this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[e.Cell.Column.Index].Width > 90)
            {
                this.DeprGridDropDown.DisplayLayout.Bands[0].Columns[this.listDeprDataTable.TextColumn.ColumnName].Width = this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[e.Cell.Column.Index].Width + 30;
                this.DeprGridDropDown.Width = this.DepreciationControlDataGrid.DisplayLayout.Bands[0].Columns[e.Cell.Column.Index].Width + 30;

                /*Added By Ramya for BugId 1969 */

                if (this.listDeprDataTable.Rows.Count > 1)
                {
                    this.DeprGridDropDown.DisplayLayout.Scrollbars = Scrollbars.Both;
                }
                else
                {
                    this.DeprGridDropDown.DisplayLayout.Scrollbars = Scrollbars.None;
                }

                /*Till here*/
            }
            else
            {
                this.DeprGridDropDown.DisplayLayout.Bands[0].Columns[this.listDeprDataTable.TextColumn.ColumnName].Width = 140;
                this.DeprGridDropDown.Width = 140;
            }
        }

        /// <summary>
        /// Handles the Resize event of the F36061 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F36061_Resize(object sender, EventArgs e)
        {
            //// modified the code to implement the CO:5733
            int recorscount = this.DepreciationControlDataGrid.Rows.Count;
            this.SetFormSliceHeight(recorscount);
        }

        #endregion Events
    }
}