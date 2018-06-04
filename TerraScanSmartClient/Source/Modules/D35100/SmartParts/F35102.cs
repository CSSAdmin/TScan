//--------------------------------------------------------------------------------------------
// <copyright file="F35102.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35102. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// o4 April 07		JYOTHI             Created
//*********************************************************************************/
namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common.Reports;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// F35102 class file
    /// </summary>
    [SmartPart]
    public partial class F35102 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// used to store DateFormat
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        /// <summary>
        /// Instance of F35001 Controller to call the WorkItem
        /// </summary>
        private F35102Controller form35102Controller;

        /// <summary>
        /// Object for OpenfileDialog Created
        /// </summary>
        private System.Windows.Forms.OpenFileDialog neighborhoodConfigOpenDialog = new OpenFileDialog();

        /// <summary>
        /// Object for OpenfileDialog Created
        /// </summary>
        private System.Windows.Forms.FolderBrowserDialog neighborhoodConfigFolderDialog = new FolderBrowserDialog();

        /// <summary>
        /// neighborhoodCfg
        /// </summary>
        private int neighborhoodCfgID;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// neighborhoodCfgData
        /// </summary>
        private F35102NeighborhoodCfgData neighborhoodCfgData = new F35102NeighborhoodCfgData();

        /// <summary>
        /// used to store the listNeighborhoodConfigurationTableDataTable
        /// </summary>
        private F35102NeighborhoodCfgData.ListNeighborhoodConfigurationTableDataTable listNeighborhoodConfigurationTableDataTable = new F35102NeighborhoodCfgData.ListNeighborhoodConfigurationTableDataTable();

        /// <summary>
        /// Usde to store the listNeighborhoodChoiceDatatable
        /// </summary>
        private F35102NeighborhoodCfgData.ListNeighborhoodChoiceDatatableDataTable listNeighborhoodChoiceDatatable = new F35102NeighborhoodCfgData.ListNeighborhoodChoiceDatatableDataTable();

        /// <summary>
        /// Usde to Store the listNeighborhoodQueryDatatable
        /// </summary>
        private F35102NeighborhoodCfgData.ListNeighborhoodQueryDatatableDataTable listNeighborhoodQueryDatatable = new F35102NeighborhoodCfgData.ListNeighborhoodQueryDatatableDataTable();

        /// <summary>
        /// Used to store the current row Index of the NeighborhoodConfigGridView
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// NeighborhoodCfg Id to be save
        /// </summary>
        private int currentNeighborhoodCfgId;

        /// <summary>
        /// Used to store the temprowIndex
        /// </summary>
        private int temprowIndex;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// Used to Hold the current row value of NextNumberRecordsGridView
        /// </summary>
        private bool gridSelected;

        /// <summary>
        /// Usde to store the currentNBHDChoiceId
        /// </summary>
        private int currentNBHDChoiceId;

        /// <summary>
        /// isformSliceCollapsed
        /// </summary>
        private bool isformSliceCollapsed;

        /// <summary>
        /// Used to store the record count
        /// </summary>
        private int recordRowCount;

        #region Form Slice Variables

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

        #endregion Form Slice Variables

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35102"/> class.
        /// </summary>
        public F35102()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// F35102
        /// </summary>
        /// <param name="masterform">masterform</param>
        /// <param name="formNo">formNo</param>
        /// <param name="keyID">keyID</param>
        /// <param name="red">red</param>
        /// <param name="green">green</param>
        /// <param name="blue">blue</param>
        /// <param name="tabText">tabText</param>
        /// <param name="permissionEdit">permissionEdit</param>
        public F35102(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.neighborhoodCfgID = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Eventpublication

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FormMasterSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FormMasterSave;

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_ExpandSlice        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ExpandSlice, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_ExpandSlice;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion Eventpublication

        #region enumerator
        ///<summary>
        /// Button Functionality
        /// </summary>
        public enum ConfigType
        {
            /// <summary>
            /// TExt = 0.
            /// </summary>
            Text = 0,

            /// <summary>
            /// Date = 1.
            /// </summary>
            Date = 1,

            /// <summary>
            /// Choice = 2.
            /// </summary>
            Choice = 2,

            /// <summary>
            /// Type is Option
            /// </summary>
            Option = 3,

            /// <summary>
            /// FilePath = 4.
            /// </summary>
            FilePath = 4,

            /// <summary>
            /// Directory Path = 5.
            /// </summary>
            DirectoryPath = 5,

            /// <summary>
            /// For Numeric
            /// </summary>
            Numeric = 6,

            /// <summary>
            /// For SQL Choice
            /// </summary>
            SQLChoice = 7
        }

        #endregion enumerator

        #region Properties

        /// <summary>
        /// Gets or sets the F35102 control.
        /// </summary>
        /// <value>The F35102 control.</value>
        [CreateNew]
        public F35102Controller F35102Control
        {
            get { return this.form35102Controller as F35102Controller; }
            set { this.form35102Controller = value; }
        }

        #endregion Properties

        #region Event Subscription

       /// <summary>
        /// FormSlice_OnSave_SetKeyId
       /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">eventArgs</param>
        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_SetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true && this.Visible)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.neighborhoodCfgID = eventArgs.Data.SelectedKeyId;
                    this.GetNeighborhoodCfgDetails(0);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);

                    if (!this.flagLoadOnProcess)
                    {
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.NeighborhoodConfigPictureBox.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    }
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
            if (this != null && this.IsDisposed != true && this.Visible)
            {
                this.LockControls(false);
                this.ClearNeighborhoodConfigGrid();
                this.ClearNeighborhoodConfigControls();
                this.temprowIndex = 0;
                this.currentRowIndex = 0;
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                ////if (this.recordRowCount <= 0 && !this.isformSliceCollapsed)
                ////{
                    this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
                ////}
            }
        }

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
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                this.Height = this.NeighborhoodConfigPictureBox.Height;
                sliceResize.SliceFormHeight = this.NeighborhoodConfigPictureBox.Height;
                this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));

                if (this.Parent != null)
                {
                    ////when the Form configuration in db is set as isexpand = false or true,form slice will be in expand mode as per configuration
                    this.isformSliceCollapsed = !this.Parent.Visible;
                }

                if (this.recordRowCount <= 0 && !this.isformSliceCollapsed)
                {
                    this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
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

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.Visible)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        if (this.slicePermissionField.editPermission)
                        {
                            this.SaveNeighborhoodConfiguration();
                        }
                    }
                    else
                    {
                        this.LockControls(true);
                        this.ControlLock(false);
                        ////this.NeighborhoodConfigNameTextBox.Focus();
                        this.GetNeighborhoodCfgDetails(this.currentRowIndex);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    this.ControlLock(false);
                }
                else
                {
                    this.ControlLock(true);
                }

                this.LockControls(true);
                this.GetNeighborhoodCfgDetails(0);
                this.NeighborhoodConfigGridView.Rows[this.NeighborhoodConfigGridView.CurrentCell.RowIndex].Selected = true;
                this.NeighborhoodConfigGridView.CurrentCell = this.NeighborhoodConfigGridView[2, Convert.ToInt32(this.NeighborhoodConfigGridView.CurrentCell.RowIndex)];
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                ////this.NeighborhoodConfigGridView.Focus();
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.DisplayNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
                this.NeighborhoodConfigGridView.TabStop = true;

                if (this.recordRowCount <= 0 && !this.isformSliceCollapsed)
                {
                    this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
                }
                else
                {
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.NeighborhoodConfigPictureBox.Height;
                    ////this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.OnFormSlice_ExpandSlice(new DataEventArgs<SliceResize>(sliceResize));
                    this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
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
            try
            {
                ////Commented by Biju on 11/Dec/2009 to fix #5145
                ////if (this != null && this.IsDisposed != true && this.Visible && this.masterFormNo == eventArgs.Data.MasterFormNo)
                ////Added by Biju on 11/Dec/2009 to fix #5145
                if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                    ////if (this.waterValvePropertiesData.GetWaterValveProperties.Rows.Count > 0)
                    ////{
                    ////    eventArgs.Data.FlagInvalidSliceKey = false;
                    ////}
                    ////else
                    ////{
                    ////    eventArgs.Data.FlagInvalidSliceKey = true;
                    ////}
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
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    ////if (this.Visible)
                    ////{
                        if (this.slicePermissionField.newPermission)
                        {
                            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                            {
                                this.neighborhoodCfgID = eventArgs.Data.SelectedKeyId;
                                ////this.NeighborhoodConfigNameTextBox.Focus();
                                this.GetNeighborhoodCfgDetails(0);
                                ////this.AssignValueToControls(this.tempCurrentRowIndex);

                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                                this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                                ////Commented by Biju on 11/Dec/2009 to fix the issue as given below:
                                ////When no records are available in this slice, it has to be collapsed.
                                ////if (this.recordRowCount <= 0 && !this.isformSliceCollapsed)
                                ////Added by Biju on 11/Dec/2009 to fix the issue as given above:
                                if (this.recordRowCount <= 0 )
                                {
                                    this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
                                }
                                else
                                {
                                    if (!this.flagLoadOnProcess)
                                    {
                                        SliceResize sliceResize;
                                        sliceResize.MasterFormNo = this.masterFormNo;
                                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                                        sliceResize.SliceFormHeight = this.NeighborhoodConfigPictureBox.Height;
                                        ////this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                                        this.OnFormSlice_ExpandSlice(new DataEventArgs<SliceResize>(sliceResize));
                                        this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                                    }
                                } 
                            }
                        }
                        else
                        {
                            this.LockControls(false);
                            this.ClearNeighborhoodConfigGrid();
                            this.ClearNeighborhoodConfigControls();
                            this.temprowIndex = 0;
                            this.currentRowIndex = 0;
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    ////}
                    
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

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_ExpandSlice(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_ExpandSlice != null)
            {
                this.FormSlice_ExpandSlice(this, eventArgs);
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
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFormMasterSave(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FormMasterSave != null)
            {
                this.D9030_F95005_FormMasterSave(this, eventArgs);
            }
        }

        #endregion Protected methods

        #region User Defined Method

        /// <summary>
        /// To Disable All the Controls
        /// </summary>
        /// <param name="lockControl">Boolean value</param>
        private void LockControls(bool lockControl)
        {
            this.NeighborhoodPanel.Enabled = lockControl;
            this.ItemPanel.Enabled = lockControl;
            this.CfgDatePanel.Enabled = lockControl;
            this.FilePanel.Enabled = lockControl;
            this.ChoicePanel.Enabled = lockControl;
            this.NumericPanel.Enabled = lockControl;
            this.CfgCombopanel.Enabled = lockControl;
            this.CfgTextPanel.Enabled = lockControl;
        }

        /// <summary> 
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.CfgText.LockKeyPress = controlLook;
            this.CfgDateTextBox.LockKeyPress = controlLook;
            this.DatePictBox.Enabled = !controlLook;
            this.FileOpen.Enabled = !controlLook;
            this.FilePathTextBox.LockKeyPress = controlLook;
            this.ChoiceCombo.Enabled = !controlLook;
            this.CfgNumericTextBox.LockKeyPress = controlLook;
            this.CfgSqlCombo.Enabled = !controlLook;
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// CommentsDataGridView
        /// </summary>
        private void CustomizeDataGrid()
        {
                this.NBHDCfgID.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.NBHDCfgIDColumn.ColumnName;
                this.Type.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName;
                this.DisplayName.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.DisplayNameColumn.ColumnName;
                this.CfgName.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.CfgNameColumn.ColumnName;
                this.CfgValue.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName;
                this.CfgCfgID.DataPropertyName = this.listNeighborhoodConfigurationTableDataTable.CfgIDColumn.ColumnName;

                this.NBHDCfgID.DisplayIndex = 0;
                this.Type.DisplayIndex = 1;
                this.DisplayName.DisplayIndex = 2;
                this.CfgName.DisplayIndex = 3;
                this.CfgValue.DisplayIndex = 4;
                this.CfgCfgID.DisplayIndex = 5;

                this.NeighborhoodConfigGridView.AutoGenerateColumns = false;
                this.NeighborhoodConfigGridView.PrimaryKeyColumnName = this.listNeighborhoodConfigurationTableDataTable.NBHDCfgIDColumn.ColumnName;
          }

        /// <summary>
        /// GetNeighborhoodCfgDetails
        /// </summary>
          /// <param name="tempRowIndex">tempRowIndex</param>
        private void GetNeighborhoodCfgDetails(int tempRowIndex)
        {
                this.flagLoadOnProcess = true;

                ////if (!isfromNavigation)
                ////{
                    ////to ste the page mode to view
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////}

                this.neighborhoodCfgData.Clear();
                this.neighborhoodCfgData = this.form35102Controller.WorkItem.GetNeighborhoodCfgDetails(this.neighborhoodCfgID);

                this.listNeighborhoodConfigurationTableDataTable = this.neighborhoodCfgData.ListNeighborhoodConfigurationTable;

                ////to ste the this.currentNeighborhoodCfgId = 0
                this.currentNeighborhoodCfgId = 0;
                ////to load the both combo Box
                this.LoadCfgSqlComboBox();
                this.LoadChoiceComboBox();

                this.NeighborhoodConfigGridView.ClearSorting();
                this.EnableOptionGridSorting();

                this.recordRowCount = this.listNeighborhoodConfigurationTableDataTable.Rows.Count;
                if (this.listNeighborhoodConfigurationTableDataTable.Rows.Count > 0)
                {
                    ////this.NeighborhoodConfigGridView.CurrentRow.Selected = true;
                    ////this.NeighborhoodConfigGridView.Rows[tempRowIndex].Selected = true;
                    ////this.NeighborhoodConfigGridView.CurrentCell = this.NeighborhoodConfigGridView[2, Convert.ToInt32(tempRowIndex)];

                    this.NeighborhoodConfigGridView.Enabled = true;

                    if (this.listNeighborhoodConfigurationTableDataTable.Rows.Count > 7)
                    {
                        this.NeighborhoodConfigGridView.NumRowsVisible = this.listNeighborhoodConfigurationTableDataTable.Rows.Count;
                    }
                    else
                    {
                        this.NeighborhoodConfigGridView.NumRowsVisible = 7;
                    }

                    this.NeighborhoodConfigGridView.DataSource = this.listNeighborhoodConfigurationTableDataTable;
                    this.AssignValueToControls(tempRowIndex);
                    TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempRowIndex);
                    this.NeighborhoodConfigGridView.Rows[tempRowIndex].Selected = true;
                    this.flagLoadOnProcess = true;
                    this.SetSmartPartHeight(this.listNeighborhoodConfigurationTableDataTable.Rows.Count);
                    this.flagLoadOnProcess = false;
                    this.LockControls(true);
                }
                else
                {
                    this.LockControls(false);
                    this.ClearNeighborhoodConfigControls();
                    this.ClearNeighborhoodConfigGrid();
                }

                this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Used to assing the Value to the text boxes
        /// </summary>
        /// <param name="rowID">rowID</param>
        private void AssignValueToControls(int rowID)
        {
             this.flagLoadOnProcess = true;

            if (rowID >= 0 && this.NeighborhoodConfigGridView.OriginalRowCount > 0 && (this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString())))
            {
                this.NeighborhoodConfigNameTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.DisplayNameColumn.ColumnName].Value.ToString();
                this.NeighborhoodConfigItemTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgNameColumn.ColumnName].Value.ToString();

                if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.Date)
                {
                    this.CfgDatePanel.Visible = true;
                    this.FilePanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.ChoicePanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.CfgCombopanel.Visible = false;

                    this.CfgDateTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.Text)
                {
                    this.CfgDatePanel.Visible = false;
                    this.ChoicePanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.CfgCombopanel.Visible = false;
                    this.FilePanel.Visible = true;

                    this.FilePathTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.FilePath || Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.DirectoryPath)
                {
                    this.CfgDatePanel.Visible = false;
                    this.ChoicePanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.CfgCombopanel.Visible = false;
                    this.FilePanel.Visible = true;

                    ////to populate the file part controls
                    this.FilePathTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.Choice)
                {
                    this.CfgDatePanel.Visible = false;
                    this.FilePanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.CfgTextPanel.Visible = true;
                    this.CfgCombopanel.Visible = false;
                    this.ChoicePanel.Visible = false;

                    this.CfgText.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.SQLChoice)
                {
                    this.CfgDatePanel.Visible = false;
                    this.FilePanel.Visible = false;
                    this.ChoicePanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.CfgCombopanel.Visible = true;

                    this.LoadTypeComboBox(rowID);
                    this.CfgSqlCombo.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.Numeric)
                {
                    this.CfgDatePanel.Visible = false;
                    this.FilePanel.Visible = false;
                    this.ChoicePanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.CfgCombopanel.Visible = false;
                    this.NumericPanel.Visible = true;

                    this.CfgNumericTextBox.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.Option)
                {
                    this.CfgDatePanel.Visible = false;
                    this.FilePanel.Visible = false;
                    this.CfgTextPanel.Visible = false;
                    this.CfgCombopanel.Visible = false;
                    this.NumericPanel.Visible = false;
                    this.ChoicePanel.Visible = true;
                    this.LoadTypeComboBox(rowID);
                    this.ChoiceCombo.Text = this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.ClearNeighborhoodConfigControls();
                }

                int.TryParse(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.NBHDCfgIDColumn.ColumnName].Value.ToString(), out this.currentNeighborhoodCfgId);
                this.currentRowIndex = rowID;
                this.temprowIndex = rowID;
            }
            else
            {
                this.CfgDatePanel.Visible = false;
                this.FilePanel.Visible = false;
                this.CfgTextPanel.Visible = true;
                this.CfgCombopanel.Visible = false;
                this.NumericPanel.Visible = false;
                this.ChoicePanel.Visible = false;
                this.LockControls(false);
            }

             this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Loads the type combo box.
        /// </summary>
        /// <param name="rowID">The row ID.</param>
        private void LoadTypeComboBox(int rowID)
        {
            if (this.NeighborhoodConfigGridView.OriginalRowCount > 0 && (this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString())))
            {
                this.listNeighborhoodChoiceDatatable.Clear();
                int.TryParse(this.NeighborhoodConfigGridView.Rows[rowID].Cells[listNeighborhoodConfigurationTableDataTable.NBHDCfgIDColumn.ColumnName].Value.ToString(), out this.currentNBHDChoiceId);
                this.neighborhoodCfgData = this.form35102Controller.WorkItem.GetNeighborhoodCfgChoice(this.neighborhoodCfgID, this.currentNBHDChoiceId);

                // Code added for CO implementation - Client mail(D35100.F35102 - Need to allow Nulls ) dated (28/09/2010)
                if (this.neighborhoodCfgData.ListNeighborhoodQueryDatatable.Rows.Count > 0)
                {
                    // Row added for Combobox empty selection
                    F35102NeighborhoodCfgData.ListNeighborhoodQueryDatatableRow emptyRow = this.neighborhoodCfgData.ListNeighborhoodQueryDatatable.NewListNeighborhoodQueryDatatableRow();
                    emptyRow.Value = 0;
                    this.neighborhoodCfgData.ListNeighborhoodQueryDatatable.Rows.InsertAt(emptyRow, 0);
                }
                    
                this.listNeighborhoodChoiceDatatable = this.neighborhoodCfgData.ListNeighborhoodChoiceDatatable;
                this.listNeighborhoodQueryDatatable = this.neighborhoodCfgData.ListNeighborhoodQueryDatatable;

                this.ChoiceCombo.DataSource = this.listNeighborhoodChoiceDatatable;
                this.CfgSqlCombo.DataSource = this.listNeighborhoodQueryDatatable;
            }
        }

        /// <summary>
        /// Used to clear the Neighborhood Config Controls
        /// </summary>
        private void ClearNeighborhoodConfigControls()
        {
            this.NeighborhoodConfigNameTextBox.Text = string.Empty;
            this.NeighborhoodConfigItemTextBox.Text = string.Empty;
            this.CfgDateTextBox.Text = string.Empty;
            this.FilePathTextBox.Text = string.Empty;
            this.CfgText.Text = string.Empty;
            this.CfgSqlCombo.SelectedIndex = -1;
            this.CfgNumericTextBox.Text = string.Empty;
            this.ChoiceCombo.SelectedIndex = -1;
        }

        /// <summary>
        /// Clears the neighborhood config grid.
        /// </summary>
        private void ClearNeighborhoodConfigGrid()
        {
            try
            {
                this.listNeighborhoodConfigurationTableDataTable.Clear();
                this.NeighborhoodConfigGridView.NumRowsVisible = 7;
                this.NeighborhoodConfigGridView.DataSource = this.listNeighborhoodConfigurationTableDataTable;
                this.NeighborhoodConfigGridView.Rows[0].Selected = false;
                this.NeighborhoodConfigGridView.Enabled = false;
                this.SetSmartPartHeight(0);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To Load the CFGSqlCombo
        /// </summary>
        private void LoadCfgSqlComboBox()
        {
            this.CfgSqlCombo.DisplayMember = this.listNeighborhoodQueryDatatable.ValueDisplayColumn.ColumnName;
            this.CfgSqlCombo.ValueMember = this.listNeighborhoodQueryDatatable.ValueColumn.ColumnName;
        }

        /// <summary>
        /// To Load the Choice Combo Box
        /// </summary>
        private void LoadChoiceComboBox()
        {
            this.ChoiceCombo.DisplayMember = this.listNeighborhoodChoiceDatatable.ChoiceColumn.ColumnName;
            this.ChoiceCombo.ValueMember = this.listNeighborhoodChoiceDatatable.ChoiceIDColumn.ColumnName;
        }

        /// <summary>
        /// Disables the option grid.
        /// </summary>
        private void DisableOptionGridSorting()
        {
            this.NBHDCfgID.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.Type.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.DisplayName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.CfgName.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.CfgValue.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// To enable the Sorting option for Grid
        /// </summary>
        private void EnableOptionGridSorting()
        {
            this.NBHDCfgID.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.Type.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.DisplayName.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.CfgName.SortMode = DataGridViewColumnSortMode.Programmatic;
            this.CfgValue.SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Shows the attachment calender in particular location.
        /// </summary>
        private void ShowAttachmentCalender()
        {
            this.ConfigMonthCalander.Visible = true;
            this.ConfigMonthCalander.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.ConfigMonthCalander.Left = this.CfgDatePanel.Left + this.DatePictBox.Left + this.DatePictBox.Width;
            this.ConfigMonthCalander.Top = this.CfgDatePanel.Top + this.DatePictBox.Top;
            this.ConfigMonthCalander.Tag = this.DatePictBox.Tag;
            this.ConfigMonthCalander.Focus();
            if (!String.IsNullOrEmpty(this.CfgDateTextBox.Text.Trim()))
            {
                this.ConfigMonthCalander.SetDate(Convert.ToDateTime(this.CfgDateTextBox.Text));
            }

            this.ConfigMonthCalander.BringToFront();
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                ////this.NeighborhoodConfigGridView.Enabled = false;
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.DisplayNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.NeighborhoodConfigGridView.TabStop = false;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            // Validation code has been commented for CO implementation - Client mail(D35100.F35102 - Need to allow Nulls ) dated (28/09/2010)
            /*if (this.currentNeighborhoodCfgId > 0)
            {
                if (this.CfgDatePanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.CfgDateTextBox.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("OwnerShipRequiredFieldValidation");
                        return sliceValidationFields;
                    }
                }
                else if (this.FilePanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                }
                else if (this.ChoicePanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.ChoiceCombo.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                }
                else if (this.CfgTextPanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.CfgText.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                }
                else if (this.CfgCombopanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.CfgSqlCombo.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                }
                else if (this.NumericPanel.Visible)
                {
                    if (string.IsNullOrEmpty(this.CfgNumericTextBox.Text.Trim()))
                    {
                        sliceValidationFields.RequiredFieldMissing = true;
                        sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                        return sliceValidationFields;
                    }
                }
            }
            else*/
            if (this.currentNeighborhoodCfgId <= 0)
            {
                sliceValidationFields.RequiredFieldMissing = true;
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("RequiredField");
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Used to save the Neighborhood Configuration details.
        /// </summary>
        /// <returns>bool</returns>
        private bool SaveNeighborhoodConfiguration()
        {
            this.listNeighborhoodConfigurationTableDataTable.Rows.Clear();
            F35102NeighborhoodCfgData.ListNeighborhoodConfigurationTableRow dr = this.listNeighborhoodConfigurationTableDataTable.NewListNeighborhoodConfigurationTableRow();

            dr.NBHDCfgID = this.currentNeighborhoodCfgId;

            if (this.CfgDatePanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.CfgDateTextBox.Text.Trim()))
                {
                    dr.CfgValue = this.CfgDateTextBox.Text.Trim();
                }
            }
            else if (this.FilePanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                {
                    dr.CfgValue = this.FilePathTextBox.Text.Trim();
                }
            }
            else if (this.ChoicePanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.ChoiceCombo.Text.Trim()))
                {
                    dr.CfgValue = this.ChoiceCombo.Text.Trim();
                }
            }
            else if (this.CfgTextPanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.CfgText.Text.Trim()))
                {
                    dr.CfgValue = this.CfgText.Text.Trim();
                }
            }
            else if (this.CfgCombopanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.CfgSqlCombo.Text.Trim()))
                {
                    dr.CfgValue = this.CfgSqlCombo.Text.Trim();
                }
            }
            else if (this.NumericPanel.Visible)
            {
                if (!string.IsNullOrEmpty(this.CfgNumericTextBox.Text.Trim()))
                {
                    dr.CfgValue = this.CfgNumericTextBox.Text.Trim();
                }
            }

            this.listNeighborhoodConfigurationTableDataTable.Rows.Add(dr);
            this.form35102Controller.WorkItem.F35102_SaveNeighborhoodCfgDetails(this.currentNeighborhoodCfgId, TerraScanCommon.GetXmlString(this.listNeighborhoodConfigurationTableDataTable.Copy()), TerraScan.Common.TerraScanCommon.UserId);

            ////to reload the entire form afther Save is made
            SliceReloadActiveRecord currentSliceInfo;
            currentSliceInfo.MasterFormNo = this.masterFormNo;
            currentSliceInfo.SelectedKeyId = this.neighborhoodCfgID;
            ////this.tempCurrentRowIndex = this.NeighborhoodConfigGridView.CurrentCell.RowIndex;
            ////this.GetNeighborhoodCfgDetails(this.NeighborhoodConfigGridView.CurrentCell.RowIndex);
            ////this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));

            ////this.NeighborhoodConfigGridView.Focus();
            this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.DisplayNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.NeighborhoodConfigGridView.Columns[listNeighborhoodConfigurationTableDataTable.CfgValueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.NeighborhoodConfigGridView.TabStop = true;

            /////this.pageMode = TerraScanCommon.PageModeTypes.View;
            return true;
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 7)
            {
                int increment = ((recordCount - 7) * 22);
                this.NeighborhoodConfigGridView.Height = 173 + increment;
                this.NeighborhoodConfigPictureBox.Height = 275 + increment;
                ////this.NeighborhoodConfigGridView.NumRowsVisible = recordCount;
                this.Height = this.NeighborhoodConfigPictureBox.Height;
            }
            else
            {
                this.NeighborhoodConfigGridView.Height = 173;
                this.NeighborhoodConfigPictureBox.Height = 275;
                ////this.NeighborhoodConfigGridView.NumRowsVisible = 7;
                this.Height = 275;
            }

            this.TitlePanel.Top = this.NeighborhoodConfigGridView.Bottom - 1;

            if (!this.flagLoadOnProcess)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);

                sliceResize.SliceFormHeight = this.NeighborhoodConfigPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.NeighborhoodConfigPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.NeighborhoodConfigPictureBox.Height, this.NeighborhoodConfigPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            } 
        }        

        #endregion User Defined Method

        #region Form Load Event

        /// <summary>
        /// F35102_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void F35102_Load(object sender, EventArgs e)
        {
            try
            {
            this.FlagSliceForm = true;
            ////this.NeighborhoodConfigNameTextBox.Focus();
            this.CustomizeDataGrid();
            this.GetNeighborhoodCfgDetails(0);
            this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load Event

        #region Events

        /// <summary>
        /// Handles the RowEnter event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.flagLoadOnProcess = true;
                    //// passing Row index to fill the textboxes from grid
                    this.AssignValueToControls(e.RowIndex);
                    this.flagLoadOnProcess = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the NeighborhoodConfigPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
            this.NeighborhooedConfigToolTip.SetToolTip(this.NeighborhoodConfigPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NeighborhoodConfigPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the Click event of the FileOpen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FileOpen_Click(object sender, EventArgs e)
        {
            try
            {
            if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[this.currentRowIndex].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.FilePath)
            {
                this.neighborhoodConfigOpenDialog.Filter = "Windows Bitmaps(*.bmp)|*.bmp|JPEG Files(*.jpg)|*.jpg|GIF Files(*.gif)|*.gif|TIFF Files(*.tif)|*.tif|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
                this.neighborhoodConfigOpenDialog.FilterIndex = 6;
                this.neighborhoodConfigOpenDialog.Multiselect = false;

                this.neighborhoodConfigOpenDialog.FileName = string.Empty;
                this.neighborhoodConfigOpenDialog.ShowDialog();

                if (!string.IsNullOrEmpty(this.neighborhoodConfigOpenDialog.FileName))
                {
                    this.FilePathTextBox.Text = this.neighborhoodConfigOpenDialog.FileName;
                    ////this.configChange = true;
                    //// disable option grid
                    this.DisableOptionGridSorting();
                }
            }
            else if (Convert.ToInt32(this.NeighborhoodConfigGridView.Rows[this.currentRowIndex].Cells[listNeighborhoodConfigurationTableDataTable.TypeColumn.ColumnName].Value.ToString()) == (int)ConfigType.DirectoryPath)
            {
                this.neighborhoodConfigFolderDialog.SelectedPath = string.Empty;
                this.neighborhoodConfigFolderDialog.ShowDialog();

                if (!string.IsNullOrEmpty(this.neighborhoodConfigFolderDialog.SelectedPath))
                {
                    if (this.neighborhoodConfigFolderDialog.SelectedPath.Length <= 3)
                    {
                        this.FilePathTextBox.Text = this.neighborhoodConfigFolderDialog.SelectedPath;
                    }
                    else
                    {
                        this.FilePathTextBox.Text = this.neighborhoodConfigFolderDialog.SelectedPath + "\\";
                    }
                    ////this.configChange = true;
                    //// disable option grid
                    this.DisableOptionGridSorting();
                }
                else
                {
                    ////this.configChange = false;
                }
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DatePictBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DatePictBox_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowAttachmentCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the ConfigMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ConfigMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
            // Assign the selected date to the DateTextbox.
            this.CfgDateTextBox.Text = e.Start.ToShortDateString();
            this.DisableOptionGridSorting();
            this.ConfigMonthCalander.Visible = false;
            this.CfgDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ConfigMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ConfigMonthCalander_Leave(object sender, EventArgs e)
        {
            try
            {
            this.ConfigMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Neighborhoods the config text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigTextChanged(object sender, EventArgs e)
        {
            try
            {
            this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            if (!this.flagLoadOnProcess)
            {
                this.EditEnabled();
            }
        }

        /// <summary>
        /// Neighborhoods the config name text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigNameTextChanged(object sender, EventArgs e)
        {
            try
            {
            this.ToEnableEditButtonInMasterForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Config MonthCalander KeyDown Events
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void ConfigMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.ConfigMonthCalander.Tag = string.Empty;
                    this.DatePictBox.Focus();
                    this.CfgDateTextBox.Text = this.ConfigMonthCalander.SelectionStart.ToString(this.dateFormat);
                    this.ConfigMonthCalander.Visible = false;
                }               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
        }

        /// <summary>
        /// Handles the KeyDown event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 40 || e.KeyValue == 38)
                {
                    this.gridSelected = true;
                }

                int tempRowIndex = 0;
                int tempmiscGirdRowIndex = 0;

                tempRowIndex = ((DataGridView)sender).CurrentCell.RowIndex;

                switch (e.KeyCode)
                {
                    case Keys.Down:
                        {
                            if ((tempRowIndex + 1) <= this.NeighborhoodConfigGridView.OriginalRowCount - 1)
                            {
                                tempmiscGirdRowIndex = tempRowIndex + 1;

                                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                                {
                                    this.AssignValueToControls(tempmiscGirdRowIndex);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        e.Handled = true;
                                        this.OnD9030_F95005_AlertFormMasterSave(new DataEventArgs<int>(this.masterFormNo));
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempmiscGirdRowIndex);
                                        this.AssignValueToControls(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        e.Handled = true;
                                        this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempmiscGirdRowIndex);
                                        this.AssignValueToControls(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.Cancel)
                                    {
                                        e.Handled = true;
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempRowIndex);
                                    }
                                }
                            }

                            break;
                        }

                    case Keys.Up:
                        {
                            if ((tempRowIndex - 1) >= 0)
                            {
                                tempmiscGirdRowIndex = tempRowIndex - 1;

                                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                                {
                                    this.AssignValueToControls(tempmiscGirdRowIndex);
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        e.Handled = true;
                                        this.OnD9030_F95005_AlertFormMasterSave(new DataEventArgs<int>(this.masterFormNo));
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempmiscGirdRowIndex);
                                        this.AssignValueToControls(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.No)
                                    {
                                        e.Handled = true;
                                        this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempmiscGirdRowIndex);
                                        this.AssignValueToControls(tempmiscGirdRowIndex);
                                    }
                                    else if (dialogResult == DialogResult.Cancel)
                                    {
                                        e.Handled = true;
                                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, tempRowIndex);
                                    }
                                }
                            }

                            break;
                        }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowValidating event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        /// <summary>
        /// Handles the MouseDown event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
            DataGridView.HitTestInfo hit = this.NeighborhoodConfigGridView.HitTest(e.X, e.Y);

            if (hit.RowIndex > -1 && hit.RowIndex != this.NeighborhoodConfigGridView.CurrentCell.RowIndex)
            {
                this.gridSelected = true;
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the NeighborhoodConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void NeighborhoodConfigGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                DataGridViewCell cell = this.NeighborhoodConfigGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "NBHDCfgID: " + this.NeighborhoodConfigGridView.Rows[e.RowIndex].Cells[0].Value.ToString();
            }

            if (e.ColumnIndex == 2)
            {
                DataGridViewCell cell = this.NeighborhoodConfigGridView.Rows[e.RowIndex].Cells["DisplayName"];
                if (!string.IsNullOrEmpty(this.NeighborhoodConfigGridView.Rows[e.RowIndex].Cells["CfgCfgID"].Value.ToString()))
                {
                    cell.ToolTipText = "CfgID: " + this.NeighborhoodConfigGridView.Rows[e.RowIndex].Cells["CfgCfgID"].Value.ToString();
                }
                else
                {
                    cell.ToolTipText = "CfgID: 0";
                }
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Events

        /// <summary>
        /// NeighborhoodConfigGridView_CellClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void NeighborhoodConfigGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            if (e.RowIndex >= 0)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.AssignValueToControls(e.RowIndex);
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        this.OnD9030_F95005_AlertFormMasterSave(new DataEventArgs<int>(this.masterFormNo));

                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, e.RowIndex);
                        this.AssignValueToControls(e.RowIndex);
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));

                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, e.RowIndex);
                        this.AssignValueToControls(e.RowIndex);
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.NeighborhoodConfigGridView, this.temprowIndex);
                    }
                }
            }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
