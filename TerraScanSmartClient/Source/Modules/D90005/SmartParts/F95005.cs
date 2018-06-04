//--------------------------------------------------------------------------------------------
// <copyright file="F95005.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Reference Data.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 June 07       M.Vijayakumar       Created
// 
//*********************************************************************************/

namespace D90005
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Infragistics.Win;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using Infragistics.Documents.Excel;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;

    /// <summary>
    /// F95005 Class file
    /// </summary>
    public partial class F95005 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// controller F95005
        /// </summary>
        private F95005Controller form95005Control;

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        private int Gridcount;

        private int Initialize=0;

        /// <summary>
        ///  Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Stores Reference Data from db
        /// </summary>
        private DataSet referenceData = new DataSet();

        /// <summary>
        /// Used to store the No of rows count returned from db
        /// </summary>
        private int referenceDataRowCount;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to store the avoidInitializeLayoutEvent
        /// </summary>
        private bool avoidInitializeLayoutEvent;

        /// <summary>
        /// Used to store the UnsavedChnages exists or not
        /// </summary>
        private bool unsavedChangesExists;

        private bool cancelclick;

        /// <summary>
        /// Used to store the IsIdentity Column Position
        /// </summary>
        private int isidentityColumnPosition;

        /// <summary>
        /// Used to store the IsIdentity Column Name
        /// </summary>
        private string isidentityColumnName;

        /// <summary>
        /// Used to store the Table Name of the reference data Table;
        /// </summary>
        private string tableNameOfReferenceData;

        /// <summary>
        /// Used to store the current Deleted row Index;
        /// </summary>
        private int currentDeletedRowIndex;

        /// <summary>
        /// Used to store the daleted rows of the reference data table
        /// </summary>
        private DataTable deletedRowDatatable;

        private bool isDeleted = false;

        private bool tempTopRowColor = false;

        private Color temporaryRowColor;

        private Color blueGreenColor = Color.FromArgb(107, 232, 219);

        private Color blueTopColor = Color.FromArgb(131, 196, 255);

        private bool ExitMode=false;

        private bool Tabpressed = false;


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
        /// Hidden ColumnExists
        /// </summary>
        private bool hiddenColumnExists;

        /// <summary>
        /// Used to store the Original  Refrence Datatable        
        /// </summary>
        private DataTable uneditedRefreenceDataTable;

        #endregion Form Slice Variables

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F95005"/> class.
        /// </summary>
        public F95005()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F95005(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ReferenceDataPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReferenceDataPictureBox.Height, this.ReferenceDataPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        /// <summary>
        /// event publication to intimate form master about the selected keyid doesnot exists
        /// due to change in the filter option
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_OnChange_Neighborhood, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9033_OnChange_Neighborhood;

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
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F25011Control
        /// </summary>
        [CreateNew]
        public F95005Controller Form95005Control
        {
            get { return this.form95005Control as F95005Controller; }
            set { this.form95005Control = value; }
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
            if (eventArgs.Data == this.masterFormNo)
            {
                ////SliceResize sliceResize;
                ////sliceResize.MasterFormNo = this.masterFormNo;
                ////sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                ////this.Height = this.ReferenceDataPictureBox.Height;
                ////sliceResize.SliceFormHeight = this.ReferenceDataPictureBox.Height;
                ////this.ReferenceDataPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReferenceDataPictureBox.Height, this.ReferenceDataPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                ////this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
            if (this.unsavedChangesExists)
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
            else
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
            if (this.unsavedChangesExists)
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                ////Note since this form doesn't contains the key id master form no is sent as key id
                currentSliceInfo.SelectedKeyId = 0; // this.masterFormNo;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.ReferenceDataGrid.Rows.TemplateAddRow.Appearance.BackColor = blueTopColor;
                this.BeginInvoke(new MethodInvoker(this.SetFocusToTemplateAddRow));
            }
            else
            {
                this.LoadReferenceDataGrid();
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

            this.LoadReferenceDataGrid();
            this.unsavedChangesExists = false;
            // this.SetSmartPartHeight(); 
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.ReferenceDataGrid.Refresh();
            this.BeginInvoke(new MethodInvoker(this.SetFocusToTemplateAddRow));
         
            this.ReferenceDataGrid.Rows.TemplateAddRow.Appearance.BackColor = blueTopColor;
        }

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                eventArgs.Data.FlagInvalidSliceKey = false;
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
                this.LoadReferenceDataGrid();
            }
        }

        #endregion Event Subscription

        #region Protected Methods

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);
            }
        }

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

        #endregion Protected methods

        #region Methods

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.AllowReferenceGridSorting(false);

                ////this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.unsavedChangesExists = true;
            }
        }

        /// <summary>
        /// Allows the reference grid sorting.
        /// </summary>
        /// <param name="allowSort">if set to <c>true</c> [allow sort].</param>
        private void AllowReferenceGridSorting(bool allowSort)
        {
            for (int i = 0; i < this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (allowSort)
                {
                    this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[i].SortIndicator = SortIndicator.None;
                }
                else
                {
                    this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[i].SortIndicator = SortIndicator.Disabled;
                }
            }
        }

        /// <summary>
        /// To Load the Reference data Grid
        /// </summary>
        private void LoadReferenceDataGrid()
        {
            this.avoidInitializeLayoutEvent = true;
            this.referenceData.Tables.Clear();
            this.avoidInitializeLayoutEvent = false;

            this.referenceData = this.form95005Control.WorkItem.F95005_ListReferenceData(this.masterFormNo);

            if (this.referenceData.Tables.Count > 0)
            {
                ////to check wheher the table[0] is empty.
                if (this.referenceData.Tables[0] != null)
                {
                    this.ReferenceDataGrid.DataSource = this.referenceData;

                    ////Used to store the Data of the Refrence Datatable
                    this.uneditedRefreenceDataTable = this.referenceData.Tables[0].Copy();
                    this.deletedRowDatatable = this.referenceData.Tables[0].Clone();

                    ////here there will be no hidden columns                    
                    this.hiddenColumnExists = false;
                    this.unsavedChangesExists = false;
                    this.referenceDataRowCount = this.referenceData.Tables[0].Rows.Count;
                    ////this.totalColumnCount = this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns.Count;

                    if (this.referenceDataRowCount > 0)
                    {
                        this.ReferenceDataGrid.Focus();
                       // this.ReferenceDataGrid.Rows[0].Selected = true;
                    }

                    if (this.referenceData.Tables.Count >= 1)
                    {
                        if (this.referenceData.Tables[2] != null && this.referenceData.Tables[1].Rows.Count > 0)
                        {
                            ////used to get the postion of the IsIdentity Column
                            int.TryParse(this.referenceData.Tables[1].Rows[0][SharedFunctions.GetResourceString("F95005IsIdentity")].ToString(), out this.isidentityColumnPosition);
                            // Coding commented for the issue 1067
                            if (this.isidentityColumnPosition > 0)
                            {
                                this.isidentityColumnPosition = this.isidentityColumnPosition - 1;
                            }
                            // ends here
                            this.isidentityColumnName = this.referenceData.Tables[1].Rows[0][SharedFunctions.GetResourceString("F95005Column_Name")].ToString();
                            this.tableNameOfReferenceData = this.referenceData.Tables[1].Rows[0][SharedFunctions.GetResourceString("F95005TableName")].ToString();
                        }
                        else
                        {
                            this.isidentityColumnPosition = -1;
                            this.isidentityColumnName = string.Empty;
                            this.tableNameOfReferenceData = string.Empty;
                        }
                    }

                    ////To resize the form slice
                    ////this.SetSmartPartHeight(this.referenceDataRowCount);
                    this.SetSmartPartHeight();
                    ReferenceDataGrid.DisplayLayout.Override.AllowAddNew = AllowAddNew.FixedAddRowOnTop;


                    this.ReferenceDataPanel.Enabled = true;

                    this.AllowReferenceGridSorting(true);
                }
                else
                {
                    this.DisableReferenceData();
                }
            }
            else
            {
                this.DisableReferenceData();
            }

            this.pageMode = TerraScanCommon.PageModeTypes.View;
          
        }

        /// <summary>
        /// Used to Disable the referencedata smart Part
        /// </summary>
        private void DisableReferenceData()
        {
            //// this.SetStandardSmartPartHeight();
            this.ReferenceDataPanel.Enabled = false;
        }

        /////// <summary>
        /////// To set the height of the Form Slice when no record exists
        /////// </summary>
        ////private void SetStandardSmartPartHeight()
        ////{
        ////    this.ReferenceDataPanel.Height = this.ReferenceDataGrid.Height - 2;
        ////    this.ReferenceDataGrid.Height = 154 - 1;
        ////    this.ReferenceDataPictureBox.Height = this.ReferenceDataPanel.Height;
        ////    this.ReferenceDataPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReferenceDataPictureBox.Height, this.ReferenceDataPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        ////    this.Height = this.ReferenceDataPictureBox.Height;

        ////    ////to assgin empty row at the end of the gird
        ////    this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
        ////    this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
        ////}

        /////// <summary>
        /////// Sets the height of the smart part.
        /////// </summary>
        /////// <param name="recordCount">The record count.</param>
        ////private void SetSmartPartHeight(int recordCount)
        ////{
        ////    if (recordCount > 4)
        ////    {
        ////        if (recordCount > 9)
        ////        {
        ////            recordCount = 9;
        ////        }

        ////        int increment = ((recordCount - 4) * 19);
        ////        this.ReferenceDataPanel.Height = 153 + increment - 2;
        ////        this.ReferenceDataGrid.Height = 154 + increment - 1;
        ////        this.ReferenceDataPictureBox.Height = this.ReferenceDataPanel.Height;
        ////        this.ReferenceDataPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReferenceDataPictureBox.Height, this.ReferenceDataPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        ////        this.Height = this.ReferenceDataPictureBox.Height;

        ////        ////to assgin empty row at the end of the gird
        ////        ////this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
        ////        ////this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
        ////    }
        ////    else
        ////    {
        ////        this.SetStandardSmartPartHeight();
        ////    }
        ////}

        #region Coding Added for the the issue 1073
        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        private void SetSmartPartHeight()
        {
            if (this.ReferenceDataGrid.Rows.Count <= 4)
            {
                // For 4 records display
                this.ReferenceDataGrid.Height = 153;
                this.ReferenceDataPanel.Height = 153;
                this.ReferenceDataGrid.Height = 153;
                this.ReferenceDataPictureBox.Height = this.ReferenceDataPanel.Height;
                if (this.ReferenceDataGrid.Rows.Count < 4)
                {
                    ////to assgin empty row at the end of the gird
                    this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                    this.ReferenceDataGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
                    
                }
            }
            else if (this.ReferenceDataGrid.Rows.Count == 5)
            {
                // For less than or equalto 9 records display
                this.ReferenceDataGrid.Height = (this.ReferenceDataGrid.Rows.Count * 22) + 64;
                this.ReferenceDataPanel.Height = this.ReferenceDataGrid.Height;
                this.ReferenceDataGrid.Height = this.ReferenceDataPanel.Height;
            }
            else if (this.ReferenceDataGrid.Rows.Count == 6)
            {
                // For less than or equalto 9 records display
                this.ReferenceDataGrid.Height = (this.ReferenceDataGrid.Rows.Count * 22) + 60;
                this.ReferenceDataPanel.Height = this.ReferenceDataGrid.Height;
                this.ReferenceDataGrid.Height = this.ReferenceDataPanel.Height;
            }

            else if (this.ReferenceDataGrid.Rows.Count <= 8)
            {
                // For less than or equalto 9 records display
                this.ReferenceDataGrid.Height = (this.ReferenceDataGrid.Rows.Count * 22) + 56;
                this.ReferenceDataPanel.Height = this.ReferenceDataGrid.Height;
                this.ReferenceDataGrid.Height = this.ReferenceDataPanel.Height;
            }
            else
            {
                // For above 12 records display
                this.ReferenceDataGrid.Height = 249;
                this.ReferenceDataPanel.Height = this.ReferenceDataGrid.Height ;
                this.ReferenceDataGrid.Height = this.ReferenceDataPanel.Height;
            }

            this.ReferenceDataPictureBox.Height = this.ReferenceDataPanel.Height;
            this.Height = this.ReferenceDataPictureBox.Height;

            // Resize SmartPart
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.ReferenceDataPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReferenceDataPictureBox.Height, this.ReferenceDataPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }
        #endregion 


        /// <summary>
        ///  To return whether reference data grid has hidden column
        /// </summary>
        /// <returns>returns boolean value</returns>
        private bool ReferenceDataGridColumnsHidden()
        {
            for (int rowIndex = 0; rowIndex < this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns.Count; rowIndex++)
            {
                if (this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[rowIndex].Hidden)
                {
                    return this.hiddenColumnExists = true;
                }
                else
                {
                    this.hiddenColumnExists = false;
                }
            }

            return this.hiddenColumnExists;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            int errorCode = -1;
            int referenceDataKeycolumnValue = 0;
            int currentRowIndexsaveDt = -1;
            string messageInvalid = "You cannot save this " + this.sectionIndicatorText + " because it is missing required fields.";
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            DataTable tempDataTable;
            tempDataTable = this.referenceData.Tables[0].GetChanges();

            // Coding added for the issue 1458 by malliga on 27/2009
            if (tempDataTable != null && tempDataTable.Rows.Count > 0)
            {
                for (int j = 0; j <= this.referenceData.Tables[2].Rows.Count - 1; j++)
                {
                   //// string filtercondition = this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + " IS NULL or " + "TRIM(CONVERT(" + this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + ",'System.String'))='' or " + this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + "=0";
                    string filtercondition = this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + " IS NULL or " + "TRIM(CONVERT(" + this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + ",'System.String'))=''";
                    ////DataRow[] statementDataRow = tempDataTable.Select(this.referenceData.Tables[2].Rows[j][0].ToString().Trim() + " IS NULL");
                    try
                    {
                        DataRow[] statementDataRow = tempDataTable.Select(filtercondition);

                        if (statementDataRow.Length > 0)
                        {
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString(messageInvalid);
                            return sliceValidationFields;
                        }
                    }
                    catch (Exception ex)
                    {
                        ///This will throw the integer data type convertion exception
                    }
                }

                ////Coding added on 19/10/2009 by malliga for foreign checking
                ////For Foreign Key Checking
                DataTable foreignKeyDataTable = new DataTable();
                DataTable columnCheckingDataTable = new DataTable(); 

                IDataReader dataRead = this.referenceData.Tables[2].CreateDataReader();
                foreignKeyDataTable.Load(dataRead, LoadOption.OverwriteChanges);
                foreignKeyDataTable.DefaultView.RowFilter = "FK=1";
                foreignKeyDataTable = foreignKeyDataTable.DefaultView.ToTable("forgientKeyDataTable");
                if (foreignKeyDataTable != null)
                {
                    if (foreignKeyDataTable.Rows.Count > 0)
                    {
                        for (int FK = 0; FK <= foreignKeyDataTable.Rows.Count - 1; FK++)
                        {
                            string columnName = foreignKeyDataTable.Rows[FK][0].ToString();
                            IDataReader idataRead = this.referenceData.Tables[0].CreateDataReader();
                            columnCheckingDataTable.Load(idataRead, LoadOption.OverwriteChanges);
                            columnCheckingDataTable.DefaultView.RowFilter = columnName + "=0";
                            columnCheckingDataTable = columnCheckingDataTable.DefaultView.ToTable();
                            if (columnCheckingDataTable.Rows.Count > 0)
                            {
                                sliceValidationFields.RequiredFieldMissing = false;
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString(messageInvalid);
                                return sliceValidationFields;
                            }
                        }
                    }
                }
            }
            // Coding Ends here

            if (!this.ReferenceDataGridColumnsHidden())
            {
                if (tempDataTable != null && tempDataTable.Rows.Count > 0)
                {
                    DataColumn dc = new DataColumn("ISNEWCOLUMNVALUE", typeof(int));
                    tempDataTable.Columns.Add(dc);

                    if (this.isidentityColumnPosition >= 0 && !string.IsNullOrEmpty(this.isidentityColumnName))
                    {
                        DataTable saveDt = this.uneditedRefreenceDataTable.Copy();
                        saveDt.DefaultView.Sort = this.isidentityColumnName;
                        
                        for (int i = 0; i < tempDataTable.Rows.Count; i++)
                        {
                            if (tempDataTable.Rows[i].RowState != DataRowState.Deleted)
                            {
                                int.TryParse(tempDataTable.Rows[i][isidentityColumnName].ToString(), out referenceDataKeycolumnValue);
                            }

                            if (referenceDataKeycolumnValue > 0)
                            {
                                ////to check whether the entire is newly inserted
                                currentRowIndexsaveDt = saveDt.DefaultView.Find(referenceDataKeycolumnValue);

                                if (currentRowIndexsaveDt == -1)
                                {
                                    ////if the value is newly inserted then set the falg to true
                                    tempDataTable.Rows[i]["ISNEWCOLUMNVALUE"] = 1;
                                }
                                else
                                {
                                    ////if the value is Updated then set the falg to false
                                    tempDataTable.Rows[i]["ISNEWCOLUMNVALUE"] = 0;
                                }
                            }
                        }

                        tempDataTable.AcceptChanges();
                    }

                    ////when deleted rows are existing sent the deleted rows as xml string else sent empty string(Allowed Null)
                    if (this.deletedRowDatatable != null && this.deletedRowDatatable.Rows.Count > 0)
                    {
                        errorCode = this.form95005Control.WorkItem.F95005_SaveReferenceData(TerraScanCommon.GetXmlString(tempDataTable), TerraScanCommon.GetXmlString(this.deletedRowDatatable), this.tableNameOfReferenceData, this.isidentityColumnName, TerraScanCommon.UserId);
                    }
                    else
                    {

                        errorCode = this.form95005Control.WorkItem.F95005_SaveReferenceData(TerraScanCommon.GetXmlString(tempDataTable), string.Empty, this.tableNameOfReferenceData, this.isidentityColumnName, TerraScanCommon.UserId);
                    }

                    if (errorCode == 0)
                    {
                        return sliceValidationFields;
                    }
                    else
                    {
                        sliceValidationFields.RequiredFieldMissing = false;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString(messageInvalid);
                    }
                }
                else
                {
                    ////when deleted rows are existing sent the deleted rows as xml string else sent empty string(Allowed Null)
                    if (this.deletedRowDatatable != null && this.deletedRowDatatable.Rows.Count > 0)
                    {
                        errorCode = this.form95005Control.WorkItem.F95005_SaveReferenceData(string.Empty, TerraScanCommon.GetXmlString(this.deletedRowDatatable), this.tableNameOfReferenceData, this.isidentityColumnName, TerraScanCommon.UserId);
                    }

                    if (errorCode == 0)
                    {
                        return sliceValidationFields;
                    }
                    else
                    {

                        //sliceValidationFields.RequiredFieldMissing = false;                        
                        //sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString(messageInvalid);                        
                    }
                }

                return sliceValidationFields;
            }
            else
            {
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("Some of the columns are removed from the layout.");
                return sliceValidationFields;
            }
        }

        /// <summary>
        /// Used to Stored the Delted rows into the deletedRowDatatable
        /// </summary>
        /// <param name="rowIndex">Current row index</param>
        private void GetDeletedRows(int rowIndex)
        {
            int keyColumnvalue = 0;
            int currentReferenceDataTableRowIndex = 0;

            if (rowIndex < this.referenceDataRowCount && this.deletedRowDatatable != null && this.isidentityColumnPosition >= 0 && !string.IsNullOrEmpty(this.isidentityColumnName))
            {
                int.TryParse(this.ReferenceDataGrid.Rows[rowIndex].Cells[this.isidentityColumnName].Value.ToString(), out keyColumnvalue);

                if (keyColumnvalue > 0)
                {
                    ////to find the row index of the Current value in the Reference datatable
                    DataTable dt = this.referenceData.Tables[0].Copy();
                    ////to find the row index use the DefaultView of the datatable
                    dt.DefaultView.Sort = this.isidentityColumnName;
                    ////to find the exact loaction of the Current value in the Reference datatable
                    currentReferenceDataTableRowIndex = dt.DefaultView.Find(keyColumnvalue);

                    if (currentReferenceDataTableRowIndex >= 0)
                    {
                        DataTable catchCurrentRowDatatable = this.referenceData.Tables[0].Clone();
                        catchCurrentRowDatatable.Clear();
                        DataRow exactCurrentEntireRow = this.referenceData.Tables[0].Rows[currentReferenceDataTableRowIndex];
                        catchCurrentRowDatatable.ImportRow(exactCurrentEntireRow);
                        this.deletedRowDatatable.Merge(catchCurrentRowDatatable);
                        this.isDeleted = true;
                    }

                    this.currentDeletedRowIndex = currentReferenceDataTableRowIndex;
                }
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F95005 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F95005_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////default value is set
                
               this.isidentityColumnPosition = -1;
                this.isidentityColumnName = string.Empty;
                this.tableNameOfReferenceData = string.Empty;

                this.LoadReferenceDataGrid();
                this.BeginInvoke(new MethodInvoker(this.SetFocusToTemplateAddRow));
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

        private void SetFocusToTemplateAddRow()
        {
            this.ReferenceDataGrid.ActiveCell = this.ReferenceDataGrid.Rows.TemplateAddRow.Cells[0];
            this.ReferenceDataGrid.PerformAction(UltraGridAction.EnterEditMode);
        } 

        /// <summary>
        /// Handles the InitializeLayout event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>l
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
               
                if (!this.avoidInitializeLayoutEvent)
                {
                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                    {
                        this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowAddNew = AllowAddNew.FixedAddRowOnTop;
                        this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
                        if (this.isidentityColumnPosition >= 0)
                        {
                            this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[this.isidentityColumnPosition].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                        }
                        else
                        {
                            this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                        }
                    }
                    else
                    {
                        this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
                    }

                    this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnTop;
                    //this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.TemplateAddRowAppearance.BackColor = blueTopColor;
                    Gridcount = e.Layout.Rows.FilteredInRowCount;
                    this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;               
                    this.ReferenceDataGrid.Update();
                   
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            try
            {
                //this.ReferenceDataGrid.PerformAction(UltraGridAction.EnterEditMode);
                //TSCO - D90005.F95005 Table Reference - New row near top of grid
                this.setColorsInitialize(e);
                this.ReferenceDataGrid.DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell; 
                if (!this.PermissionFiled.newPermission)
                {   
                    e.TemplateAddRow.Activation = Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeColPosChanged event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeColPosChangedEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_BeforeColPosChanged(object sender, BeforeColPosChangedEventArgs e)
        {
            try
            {
                if (this.isidentityColumnPosition >= 0)
                {
                    if (e.ColumnHeaders[this.isidentityColumnPosition].Column.ToString() == this.isidentityColumnName || this.unsavedChangesExists)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeEnterEditMode event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.referenceDataRowCount > 0 && this.ReferenceDataGrid.ActiveRow != null && (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit))
                {
                    if (this.ReferenceDataGrid.ActiveRow.Index > this.referenceDataRowCount - 1)
                    {
                        this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
                    }
                    else
                    {
                        if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                        {
                            this.ReferenceDataGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
                        }
                    }

                    if (this.isidentityColumnPosition >= 0)
                    {
                        this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (this.ReferenceDataGrid.ActiveRow != null && this.ReferenceDataGrid.ActiveRow.Index >= 0)
                {
                    this.EditEnabled();
                   
                    if (this.ReferenceDataGrid.ActiveRow.Index <= this.referenceDataRowCount)
                    {

                        int currentTempHighestKeyColumnValue = 0;
                        int currentMaxValue;

                        if (!string.IsNullOrEmpty(this.isidentityColumnName) && string.IsNullOrEmpty(this.ReferenceDataGrid.ActiveRow.Cells[this.isidentityColumnName].Value.ToString()))
                        {
                            string findVaue = "MAX (" + this.isidentityColumnName + ")";
                            string conDition = this.isidentityColumnName + " > 0";

                            int.TryParse(this.referenceData.Tables[0].Compute(findVaue, conDition).ToString(), out currentTempHighestKeyColumnValue);
                            int.TryParse(this.ReferenceDataGrid.ActiveRow.Cells[this.isidentityColumnName].Value.ToString(), out currentMaxValue);

                            this.ReferenceDataGrid.ActiveRow.Cells[this.isidentityColumnName].Value = currentTempHighestKeyColumnValue + 1;

                            ExitMode = true;
                            this.ReferenceDataGrid.Update();
                            this.ReferenceDataGrid.ActiveRow.Activation = Activation.AllowEdit;
                            this.SetSmartPartHeight();

                            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ReferenceDataGrid.ActiveCell;
                            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ReferenceDataGrid.ActiveRow;

                            if (activeRow != null && activeCell != null)
                            {
                                activeCell.Activate();

                                this.ReferenceDataGrid.PerformAction(UltraGridAction.EnterEditMode);
                                try
                                {
                                    if (activeCell.IsInEditMode == true && activeCell.SelText.Length >= 0)
                                    {
                                        activeCell.SelStart = activeCell.Text.Length;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
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

        /// <summary>
        /// Handles the BeforeRowsDeleted event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                e.DisplayPromptMsg = false;
                this.isDeleted = false;
                if (this.ReferenceDataGrid.ActiveRow != null)
                {
                    if (!this.PermissionFiled.deletePermission)
                    {
                        if (this.ReferenceDataGrid.ActiveRow.Index >= this.referenceDataRowCount)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.GetDeletedRows(this.ReferenceDataGrid.ActiveRow.Index);
                                e.Cancel = false;
                            }
                            else
                            {
                                e.Cancel = true;
                                this.currentDeletedRowIndex = -1;
                            }
                        }
                        else
                        {
                            e.Cancel = true;
                            this.currentDeletedRowIndex = -1;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.ReferenceDataGrid.Rows[this.ReferenceDataGrid.ActiveRow.Index].Cells[this.isidentityColumnName].Value.ToString()))
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.GetDeletedRows(this.ReferenceDataGrid.ActiveRow.Index);
                                e.Cancel = false;
                            }
                            else
                            {
                                e.Cancel = true;
                                this.currentDeletedRowIndex = -1;
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

        /// <summary>
        /// Handles the Click event of the ReferenceDataPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReferenceDataPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the ReferenceDataPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReferenceDataPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ReferenceDataFormSliceToolTip.SetToolTip(this.ReferenceDataPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeCellCancelUpdate event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_BeforeCellCancelUpdate(object sender, CancelableCellEventArgs e)
        {
            try
            {
                this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowsDeleted event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_AfterRowsDeleted(object sender, EventArgs e)
        {
            try
            {
                if (this.isDeleted)
                {
                    if (this.currentDeletedRowIndex >= -1 && this.currentDeletedRowIndex < this.referenceDataRowCount)
                    {
                        if (this.referenceDataRowCount > 0)
                        {
                            this.referenceDataRowCount = this.referenceDataRowCount - 1;
                        }

                        if (this.currentDeletedRowIndex >= 0)
                        {
                            this.referenceData.Tables[0].Rows.RemoveAt(this.currentDeletedRowIndex)                                                                                                                                               ;
                            this.EditEnabled();
                            this.SetSmartPartHeight();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void ReferenceDataGrid_Enter(object sender, EventArgs e)
        {
           //// this.ReferenceDataGrid.Focus();
            //this.ReferenceDataGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.ActivateOnly;
            //// coding aded for the issue 1065
            //if (this.ReferenceDataGrid.Rows.Count > 0)
            //{
            //    this.ReferenceDataGrid.Rows[0].Selected = true;
            //    this.ReferenceDataGrid.Rows[0].Activate();
            //}
            //// Endshere 
        }

        #endregion Events

        /// <summary>
        /// Handles the CellDataError event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
        {
            e.RestoreOriginalValue = true;
            e.StayInEditMode = true;
        }

        /// <summary>
        /// Handles the Error event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ErrorEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_Error(object sender, Infragistics.Win.UltraWinGrid.ErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the ReferenceDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void ReferenceDataGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ReferenceDataGrid.ActiveCell;
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ReferenceDataGrid.ActiveRow;

            if (activeRow != null && activeCell != null)
            {
              
                if (activeCell.Text.Equals(" "))
                {
                    activeCell.Value = DBNull.Value;
                }
            }

        }

        //TSCO - D90005.F95005 Table Reference - New row near top of grid
        private void setColorsInitialize(InitializeTemplateAddRowEventArgs e)
        {
            tempTopRowColor = false;
            if (Initialize == 0)
            {
                if (tempTopRowColor)
                {
                    temporaryRowColor = blueGreenColor;
                    e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                    tempTopRowColor = false;
                }
                else
                {
                    temporaryRowColor = blueTopColor;
                    e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                    tempTopRowColor = true;
                }
                Initialize++;
            }
            else if(Initialize==1)
            {
                if (e.TemplateAddRow.ParentCollection.Count > Gridcount)
                {
                    if (!tempTopRowColor)
                    {
                        temporaryRowColor = blueGreenColor;
                        e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                        tempTopRowColor = false;
                    }
                    else
                    {
                        temporaryRowColor = blueTopColor;
                        e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                        tempTopRowColor = true;
                    }
                    Gridcount = e.TemplateAddRow.ParentCollection.Count;
                    ++Initialize;
                }
                else
                {
                    if (tempTopRowColor)
                    {
                        temporaryRowColor = blueGreenColor;
                        e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                    }
                    else
                    {
                        temporaryRowColor = blueTopColor;
                        e.TemplateAddRow.Appearance.BackColor = temporaryRowColor;
                    }

                }
            }
                else
                {
                    Color rowColor = this.ReferenceDataGrid.Rows[0].Appearance.BackColor;
                    if (rowColor == blueTopColor)
                    {
                        e.TemplateAddRow.Appearance.BackColor = blueGreenColor;
                    }
                    else
                    {
                        e.TemplateAddRow.Appearance.BackColor = blueTopColor;
                    }
                }
            }


        private void ReferenceDataGrid_AfterRowInsert(object sender, RowEventArgs e)
        {

            //this.ReferenceDataGrid.Rows.TemplateAddRow.Selected = true;
            //// this.SetSmartPartHeight();
        }

      

        private void ReferenceDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Tab)
                Tabpressed = true;
            else
                Tabpressed = false;
        }

        private void ReferenceDataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
        {
            if (Tabpressed == true)
            {
                if (ExitMode == true)
                {
                    if (this.ReferenceDataGrid.ActiveRow.Index == 0)
                    {
                        this.BeginInvoke(new MethodInvoker(this.SetFocusToTemplateAddRow));
                    }
                    ExitMode = false;
                }
            }
        }
    }
}
