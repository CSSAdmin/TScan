//--------------------------------------------------------------------------------------------
// <copyright file="F35000.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Apprisal Summury.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07		Shiva M     	    Created
// 05 April 07      Vinoth              UI Functionality
//*********************************************************************************/
namespace D35000
{
    #region  NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using Infragistics.Win;
    using Infragistics.Win.FormattedLinkLabel;
    using Infragistics.Win.UltraWinGrid;
    using TerraScan.Infrastructure.Interface.Services;
    using Infragistics.Win.UltraWinEditors;
    using System.Linq;
    #endregion NameSpace

    /// <summary>
    /// F35000 Apprisal Summary SmartPart
    /// </summary>
    [SmartPart]
    public partial class F35000 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// camaForm
        /// </summary>
        private Form camaForm = null;

        /// <summary>
        /// formOpened
        /// </summary>
        private bool formOpened = false;

        /// <summary>
        /// F35000Controller variable.
        /// </summary>
        private F35000Controller form35000Controll;

        private int IncomeApproachSliceTypeID;

        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int parcelId;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// datatable contains the formDetails.
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// basePanelScrolled variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// appraisalSummaryData dataset
        /// </summary>
        private F35000AppraisalSummaryData appraisalSummaryData = new F35000AppraisalSummaryData();

        /// <summary>
        /// Holds object and slice data
        /// </summary>
        private F35000AppraisalSummaryData listObjectSliceData = new F35000AppraisalSummaryData();

        /// <summary>
        /// Cell Activate status
        /// </summary>
        private bool cellActivate;

        /// <summary>
        /// Holds Active Row details
        /// </summary>
        private Infragistics.Win.UltraWinGrid.UltraGridRow activeRow;

        ///<summary>
        /// Hold the CurrentRowIndex
        /// </summary>
        private int isCurrentRow;

        /// <summary>
        /// Holds Active Cell details
        /// </summary>
        private Infragistics.Win.UltraWinGrid.UltraGridCell activeCell;

        /// <summary>
        /// Stores description Status
        /// </summary>
        private bool descriptionStatus;

        /// <summary>
        /// Parent RowHeight
        /// </summary>
        private int parentRowHeight;

        /// <summary>
        /// Child RowHeight
        /// </summary>
        private int childRowHeight;

        /// <summary>
        /// Parent RowCount
        /// </summary>
        private int parentRowCount;

        /// <summary>
        /// Child RowCount
        /// </summary>
        private int childRowCount;

        /// <summary>
        /// row Count status
        /// </summary>
        private bool rowCount;

        /// <summary>
        /// To hold tabText
        /// </summary>
        private string tabText;

        /// <summary>
        /// userName
        /// </summary>
        private string userName;

        /// <summary>
        /// outPutValue
        /// </summary>
        private int outPutValue;

        /// <summary>
        /// redColorCode
        /// </summary>
        private int redColorCode;

        /// <summary>
        /// greenColorCode
        /// </summary>
        private int greenColorCode;

        /// <summary>
        /// blueColorCode
        /// </summary>
        private int blueColorCode;

        /// <summary>
        /// Update Status <true>If current row is updated</true> 
        /// </summary>
        private bool updateStatus;

        /// <summary>
        /// slice ValueListName
        /// </summary>
        private string sliceValueListName;

        /// <summary>
        /// object ValueListName
        /// </summary>
        private string objectValueListName;

        /// <summary>
        /// Instance for formattedLinkEditor
        /// </summary>
        private FormattedLinkEditor formattedLinkEditor = null;

        /// <summary>
        /// instance variable to hold the y-axis point.
        /// </summary>
        private int yaxisPoint;

        ///<summary>
        /// Used to hold the Edited Status
        /// </summary>
        private bool IsEditStatus = false;

        private bool isRollValueChanged = false;

        int iSliceId = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35000"/> class.
        /// </summary>
        public F35000()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35000"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35000(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.parcelId = keyID;
            this.tabText = tabText;
            this.redColorCode = red;
            this.greenColorCode = green;
            this.blueColorCode = blue;
            this.AppraisalSummaryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AppraisalSummaryPictureBox.Height, this.AppraisalSummaryPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
        }

        #endregion

        #region Event Publication

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
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;

        #endregion Event Publication

        #region Properties

        #region F35000Controller

        /// <summary>
        /// Gets or sets the form35000 controll.
        /// </summary>
        /// <value>The form35000 controll.</value>
        [CreateNew]
        public F35000Controller Form35000Controll
        {
            get { return this.form35000Controll as F35000Controller; }
            set { this.form35000Controll = value; }
        }

        #endregion

        #region FormattedLinkEditor

        /// <summary>
        /// Gets the formatted link editor.
        /// </summary>
        /// <value>The formatted link editor.</value>
        private FormattedLinkEditor FormattedLinkEditor
        {
            get
            {
                if (this.formattedLinkEditor == null)
                {
                    this.formattedLinkEditor = new FormattedLinkEditor(true);
                    this.formattedLinkEditor.TreatValueAs = TreatValueAs.URL;
                    this.formattedLinkEditor.LinkClicked += new
                 Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(this.FormattedLinkEditor_LinkClicked);
                }

                return this.formattedLinkEditor;
            }
        }

        #endregion

        #endregion

        #region Event Subscription

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                ////this.ClearWaterValveControls();
                ////this.SetNewComboIndex();
                ////this.LockControls(true);
                ////this.ControlLock(false);
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                ////this.ClearWaterValve();
                ////this.SetNewComboIndex();
                ////this.LockControls(false);
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
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                ////this.ControlLock(false);
            }
            else
            {
                ////this.ControlLock(true);
            }
            this.LoadSliceObject();
            this.SetSmartpartHeight();
            ////this.LockControls(true);
            ////this.CustomizeWaterValveProperties();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    ////this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                }
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    ////this.SaveWaterValveProperties();
                    string parcelXML = this.GetParcelXML();
                    this.form35000Controll.WorkItem.F35000_SaveAppraisal(this.parcelId, parcelXML, TerraScanCommon.UserId);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                ////this.LockControls(true);
                ////this.ControlLock(false);
                ////this.CustomizeWaterValveProperties();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = eventArgs.Data.DeletePermission;
                    this.slicePermissionField.editPermission = eventArgs.Data.EditPermission;
                    this.slicePermissionField.newPermission = eventArgs.Data.NewPermission;
                    this.slicePermissionField.openPermission = eventArgs.Data.OpenPermission;

                    if (this.appraisalSummaryData.GetParcelValidTable.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.appraisalSummaryData.GetParcelValidTable.Rows[0][this.appraisalSummaryData.GetParcelValidTable.IsOpenColumn.ColumnName].ToString()))
                        {
                            if (Convert.ToBoolean(this.appraisalSummaryData.GetParcelValidTable.Rows[0][this.appraisalSummaryData.GetParcelValidTable.IsOpenColumn.ColumnName].ToString()))
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
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    //Changes in the Load Slice Details for Link Label loaded from Master form 20000 or 30000
                    BaseSmartPart formn = (BaseSmartPart)sender;
                    if ((!((System.Windows.Forms.Control)(formn)).Name.Equals("F25003")) && !((System.Windows.Forms.Control)(formn)).Name.Equals("F25009") && !((System.Windows.Forms.Control)(formn)).Name.Equals("F27006"))
                    {
                        //this.form35000Controll.State; 
                        this.parcelId = eventArgs.Data.SelectedKeyId;
                        //used to hold the Edit Status false
                        
                        //modified for Load Dropdown
                        this.InitializeObjectValueLists();

                        this.LoadSliceObject();
                        this.ColumnPositioning();

                        ////if (this.AppraisalSummaryGrid.Rows.Count > 0)
                        ////{
                        ////    this.ColumnPositioning();
                        ////    ////this.AppraisalSummaryGrid.ActiveRow = this.AppraisalSummaryGrid.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.First);
                        ////}
                        ////else
                        ////{
                        ////    this.CreateObjectSliceDataTable();
                        ////}

                        this.SetSmartpartHeight();
                 
                    }
                    ////this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                sliceResize.SliceFormName = "D35000.F35000";
                sliceResize.SliceFormHeight = this.AppraisalSummaryGrid.Height;
                ////this.Height = this.AppraisalSummaryGrid.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        #endregion Event Subscription

        #region ParcelChangedValueEvent

        /// <summary>
        /// D35000_s the F35000_ parcel changed value.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D35000_F35000_ParcelChangedValue, ThreadOption.UserInterface)]
        public void D35000_F35000_ParcelChangedValue(object sender, DataEventArgs<int[]> eventArgs)
        {
            if (eventArgs.Data[0] == this.parcelId)
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.LoadSliceObject();

                    this.SetSmartpartHeight();
                    ////Code Commented By Shiva to Fix the Issue Id:344  35001 - Value Slice Header - Save changes form focus.
                    ////if (eventArgs.Data[1].Equals(0))
                    ////{
                    ////    if (this.ParentForm != null)
                    ////    {
                    ////        this.ParentForm.BringToFront();
                    ////    }
                    ////}
                }
                else
                {
                    DialogResult result = (MessageBox.Show("Appraisal Summary value is in edit mode. Do you want to save the changes?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information));
                    if (result.Equals(DialogResult.Yes))
                    {
                        string parcelXML = this.GetParcelXML();
                        this.form35000Controll.WorkItem.F35000_SaveAppraisal(this.parcelId, parcelXML, TerraScanCommon.UserId);
                        this.LoadSliceObject();
                        this.SetSmartpartHeight();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                        //this.SetButtons(TerraScanCommon.ButtonActionMode.OpenMode);
                    }
                    else if (result.Equals(DialogResult.No))
                    {
                        this.LoadSliceObject();
                        this.SetSmartpartHeight();
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                        this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                    }
                    else
                    {
                        //Cancel Operation no changes in value 
                    }


                }
            }
        }

        #endregion ParcelChangedValueEvent

        #region FormResize

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

        #endregion

        #region Form Load Events

        /// <summary>
        /// Handles the Load event of the F35000 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35000_Load(object sender, EventArgs e)
        {
            try
            {
                ////this.InitializeSliceValueLists();
                this.InitializeObjectValueLists();
                this.FlagSliceForm = true;
                this.cellActivate = true;
                this.LoadSliceObject();
                this.ColumnPositioning();

                this.keyField = "ObjectID";
                this.formNo = 35000;

                ////if (this.AppraisalSummaryGrid.Rows.Count > 0)
                ////{
                ////    this.ColumnPositioning();
                ////}
                ////else
                ////{
                ////    this.CreateObjectSliceDataTable();
                ////    // this.AppraisalSummaryGrid.UpdateData();
                ////}

                this.SetSmartpartHeight();

                // this.AppraisalSummaryGrid.Focus();
                this.cellActivate = false;
                this.rowCount = false;
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

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Resize event of the F35000 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35000_Resize(object sender, EventArgs e)
        {
            this.Height = this.AppraisalSummaryGrid.Height+25;
        }

        /// <summary>
        /// Handles the Click event of the AppraisalSummaryPictureBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D35000.F35000"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Apprisal Summary Grid Events

        #region InitializeLayout

        /// <summary>
        /// Handles the InitializeLayout event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion InitializeLayout

        #region InitializeRow

        /// <summary>
        /// Handles the InitializeRow event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row.ChildBands != null)
                {
                    // Expand all Parent Rows
                    this.ExpandAllParentRows(e.Row);
                }

                if (e.Row.IsAddRow)
                {
                    // Checks for the TemplateRow Column and Changes the style to Combo
                    ////this.PopulateComboDetails(e.Row);                    

                    // Disables the cell edit
                    this.DisableCellEdit(e.Row);
                }
                else if (!e.Row.IsAddRow)
                {
                    // Deactivates the Hyperlink for the particular Cell
                    this.DeactivateHyperLink(e.Row);

                    this.ValueCellAppearance(e.Row);
                }

                if (e.Row.Band.Index == 1)
                {
                    if (e.Row.HasParent(this.AppraisalSummaryGrid.DisplayLayout.Bands[0]))
                    {
                        this.childRowHeight = e.Row.Height;

                        if (this.rowCount)
                        {
                            this.childRowCount++;
                        }
                    }
                }

                if (e.Row.Band.Index == 0)
                {
                    this.parentRowHeight = e.Row.Height;

                    if (this.rowCount)
                    {
                        this.parentRowCount++;
                        if (e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].StyleResolved == Infragistics.Win.UltraWinGrid.ColumnStyle.URL)
                        {
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.PlainText;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.ForeColor = Color.White;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.Cursor = Cursors.Hand;
                        }

                        if (e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].StyleResolved == Infragistics.Win.UltraWinGrid.ColumnStyle.URL)
                        {
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.PlainText;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.White;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                            e.Row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.Cursor = Cursors.Hand;
                        }
                    }
                }

                this.DrawTypeIdSymbols(e.Row);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion InitializeRow

        #region AfterCellActivate

        /// <summary>
        /// Handles the AfterCellActivate event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_AfterCellActivate(object sender, EventArgs e)
        {
            try
            {
                if (!this.cellActivate)
                {
                    this.activeRow = this.AppraisalSummaryGrid.ActiveRow;
                    this.activeCell = this.AppraisalSummaryGrid.ActiveCell;

                    int activeType;

                    if (this.activeRow.Band.Index.Equals(0))
                    {
                        int.TryParse(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Value.ToString(), out activeType);
                    }
                    else
                    {
                        int.TryParse(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString(), out activeType);
                    }

                    if (!this.activeRow.IsAddRow)
                    {

                        if (this.activeCell.Band.Index == 0)
                        {
                            if (this.activeCell.Column.Index != 4)
                            {
                                this.AppraisalSummaryGrid.ActiveCell.Activated = false;
                            }

                        }
                        else
                        {
                            if (this.activeCell.Column.Index != 7)
                            {
                                this.AppraisalSummaryGrid.ActiveCell.Activated = false;
                            }
                            if (this.activeCell.Column.Key == "SliceType")
                            {
                                ////this.ShowValueSliceSubForm();
                            }
                        }
                    }
                    else
                    {
                        if (this.AppraisalSummaryGrid.ActiveCell != null)
                        {
                            this.AppraisalSummaryGrid.ActiveCell.Activated = true;

                            // Piece of Code to ADD Slice Details
                            if (this.activeCell.StyleResolved == Infragistics.Win.UltraWinGrid.ColumnStyle.URL)
                            {
                                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                                {
                                    this.InsertValueorObject();
                                    
                                }
                                else
                                {

                                    if (activeType > 0)
                                    {
                                        DialogResult result = MessageBox.Show("Appraisal Summary value is in edit mode. Do you want to save the changes?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                                        if (result.Equals(DialogResult.Yes))
                                        {
                                            string parcelXML = this.GetParcelXML();
                                            this.form35000Controll.WorkItem.F35000_SaveAppraisal(this.parcelId, parcelXML, TerraScanCommon.UserId);
                                            this.InsertValueorObject();
                                            this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                                            //this.pageMode = TerraScanCommon.PageModeTypes.View;
                                            //this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));

                                        }
                                        else
                                            if (result.Equals(DialogResult.No))
                                            {
                                                //Reload the Data based on xml.
                                                this.InsertValueorObject();
                                                
                                            }
                                            else
                                            {
                                                if (this.activeRow.Band.Index.Equals(0))
                                                {
                                                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Activate();
                                                }
                                                else
                                                {
                                                    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activate();
                                                }
                                                ////this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor=Color.Violet;
                                            }
                                    }
                                    else
                                    {
                                        if (this.activeRow.Band.Index.Equals(0))
                                        {
                                            this.updateStatus = false;
                                            MessageBox.Show("Please Select any ObjectType", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Activate();
                                        }
                                        else
                                        {
                                            this.updateStatus = false;
                                            MessageBox.Show("Please Select any SliceType", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activate();
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion AfterCellActivate

        #region InsertValueorObject
        private void InsertValueorObject()
        {
            if (this.activeRow.Band.Index == 1)
            {
                this.InsertValueSlice();
                if (!this.updateStatus)
                {
                    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activate();
                    ////this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor=Color.Violet;
                }
            }
            else if (this.activeRow.Band.Index == 0)
            {
                this.InsertObjectValue();
                if (!this.updateStatus)
                {
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Activate();
                }
            }

        }
        #endregion InsertValueOrObject

        #region InitializeTemplateAddRow

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_InitializeTemplateAddRow(object sender, Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs e)
        {
            try
            {
                this.InitializeObjectValueLists();
                //// Checks for the TemplateRow Column and Changes the style to Combo
                //// loads datasource to the dropdownlist
                if (e.TemplateAddRow.Band.Index == 1)
                {
                    int objectId;
                    if (e.TemplateAddRow.ParentRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeIDColumn.ColumnName].Value != null)
                    {
                        //int.TryParse(e.TemplateAddRow.ParentRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeIDColumn.ColumnName].Value.ToString(), out objectId);
                        int.TryParse(e.TemplateAddRow.ParentRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value.ToString(), out objectId);
                        this.InitializeSliceValueLists(objectId);
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists[this.sliceValueListName];
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Value = "Add";
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                        ////e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.TextHAlign = HAlign.Center;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.TextVAlign = VAlign.Middle;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColor = Color.White;

                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.BackColorDisabled = Color.White;
                        e.TemplateAddRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.BackColor = Color.White;

                    }
                }
                else if (e.TemplateAddRow.Band.Index == 0)
                {
                    if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                    }
                    ////e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                    ////e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.False;
                    ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists[this.objectValueListName];
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Value = "Add";
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                    e.TemplateAddRow.CellAppearance.BackColor = Color.FromArgb(31, 65, 103);

                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.PlainText;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.White;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Appearance.FontData.SizeInPoints = 8;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Appearance.Cursor = Cursors.Hand;
                    ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
                    ////e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always; 

                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    e.TemplateAddRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion InitializeTemplateAddRow

        #region BeforeRowUpdate

        /// <summary>
        /// Handles the BeforeRowUpdate event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CancelableRowEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_BeforeRowUpdate(object sender, Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e)
        {
            // May 2007 Change Order

            ////DialogResult alertWhenActiveRowChange = MessageBox.Show("Do you want to Save the changes to Appraisal Summary", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            ////if (alertWhenActiveRowChange.Equals(DialogResult.Yes))
            ////{
            ////    if (this.activeRow.Band.Index == 1)
            ////    {
            ////        this.InsertValueSlice();
            ////        if (!this.updateStatus)
            ////        {
            ////            e.Cancel = true;
            ////        }
            ////    }
            ////    else if (this.activeRow.Band.Index == 0)
            ////    {
            ////        this.InsertObjectValue();
            ////        if (!this.updateStatus)
            ////        {
            ////            e.Cancel = true;
            ////        }
            ////    }
            ////}
            ////else
            ////{
            ////    e.Cancel = true;
            ////}

            ///due to editing of record
            if (this.activeRow.IsAddRow)
            {
                e.Cancel = true;
            }
            //if (this.activeRow.Band.Index == 1)
            //{
            //    if (string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString()))
            //    {
            //        this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value = string.Empty;
            //    }
            //    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.White;
            //    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.Blue;
            //}
            //if (this.activeRow.Band.Index == 0)
            //{
            //    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);
            //    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.White;

            //}
        }

        #endregion BeforeRowUpdate

        #region BeforeEnterEditMode

        /// <summary>
        /// Handles the BeforeEnterEditMode event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.activeRow.Band.Index == 1)
                {
                    if (this.activeCell.Column.Index == 7)
                    {
                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(255, 226, 113);
                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.Black;
                    }
                    if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value.ToString()))
                    {
                        if (string.Equals(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value.ToString(), "Description"))
                        {
                            this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value = string.Empty;
                            //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value = string.Empty;
                        }

                        if (this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString() == string.Empty)
                        {

                        }

                    }
                }
                else if (this.activeRow.Band.Index == 0)
                {
                    //modified for Load Dropdown
                    if (activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Column.Key == "ObjectType" && activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Index == 0)
                    {
                        this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists[this.objectValueListName];
                        this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                    }


                    ////if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Value.ToString()))
                    ////{
                    ////    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Value = string.Empty;
                    ////}
                    if (this.activeCell.Column.Index == 4)
                    {
                        if (!this.activeRow.IsAddRow)
                        {
                            this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                            //this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(255, 226, 113);
                            //this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.Black;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Value.ToString()))
                            {
                                if (string.Equals(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Value.ToString(), "(New Object)"))
                                {
                                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                                }
                            }
                        }
                    }
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Appearance.BackColorDisabled = Color.FromArgb(31, 65, 103);
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion BeforeEnterEditMode

        #region BeforePerformAction

        /// <summary>
        /// Handles the BeforePerformAction event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeUltraGridPerformActionEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_BeforePerformAction(object sender, Infragistics.Win.UltraWinGrid.BeforeUltraGridPerformActionEventArgs e)
        {
            try
            {
                if (this.activeCell != null)
                {
                    // Tab Stops for the Add Column
                    if (this.activeCell.Column.Key.ToString() == this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            ////if (this.AppraisalSummaryGrid.ActiveRow.ParentRow != null)
            ////{
            ////    if (this.AppraisalSummaryGrid.ActiveRow.ParentRow.Index == (this.AppraisalSummaryGrid.Rows.VisibleRowCount - 1) && this.AppraisalSummaryGrid.Rows.VisibleRowCount > 1)
            ////    {
            ////        if (this.AppraisalSummaryGrid.DisplayLayout.Bands.Count > 0)
            ////        {
            ////            if (this.AppraisalSummaryGrid.Rows.TemplateAddRow.Index == this.AppraisalSummaryGrid.ActiveRow.Index)
            ////            {
            ////                this.AppraisalSummaryGrid.Rows[0].Activated = true;
            ////            }
            ////        }
            ////    }
            ////}
        }

        #endregion

        #region LinkClickedEvent

        /// <summary>
        /// Handles the LinkClicked event of the formattedLinkEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs"/> instance containing the event data.</param>
        private void FormattedLinkEditor_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
        {
            try
            {
                this.ShowValueSliceSubForm();
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion LinkClickedEvent

        #region OnMouseOver ToolTip Events

        /// <summary>
        /// Handles the MouseEnterElement event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UIElementEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_MouseEnterElement(object sender, UIElementEventArgs e)
        {
            try
            {
                if (e.Element != null)
                {
                    UltraGridColumn col = e.Element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;
                    UltraGridRow row = e.Element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
                    if (col != null && row != null)
                    {
                        if (this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Key == col.Key && row.Index >= 0)
                        {
                            string toolTipMessage = this.AppraisalSummaryGrid.Rows[row.ParentRow.Index].ChildBands[0].Rows[row.Index].Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value.ToString();
                            this.AppraisalSummaryToolTip.SetToolTip(AppraisalSummaryGrid, toolTipMessage);
                        }
                        else
                        {
                            this.AppraisalSummaryGrid.DisplayLayout.Override.TipStyleCell = TipStyle.Show;
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
        /// Handles the MouseLeaveElement event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UIElementEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_MouseLeaveElement(object sender, UIElementEventArgs e)
        {
            try
            {
                this.AppraisalSummaryToolTip.SetToolTip(AppraisalSummaryGrid, string.Empty);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        /// <summary>
        /// Handles the Enter event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.basePanelScrolled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the AppraisalSummaryGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.basePanelScrolled)
                {
                    if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(e.X, e.Y);
                        this.basePanelScrolled = false;
                    }
                }
                else
                {
                    this.basePanelScrolled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ultraFormattedTextEditor1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs"/> instance containing the event data.</param>
        private void ultraFormattedTextEditor1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
        {
            try
            {
                if (((Infragistics.Win.UltraWinGrid.UltraGridCell)(e.Context)).Row.IsActiveRow)
                {
                    this.ShowValueSliceSubForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ObjectTypeUltraFormattedTextEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs"/> instance containing the event data.</param>
        private void ObjectTypeUltraFormattedTextEditor_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
        {
            try
            {
                if (((Infragistics.Win.UltraWinGrid.UltraGridCell)(e.Context)).Row.IsActiveRow)
                {
                    this.ShowValueSliceSubForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Apprisal Summary Grid Events

        #region PictureBox MouseEnter Event

        /// <summary>
        /// Handles the MouseEnter event of the AppraisalSummaryPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AppraisalSummaryPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.AppraisalSummaryToolTip.SetToolTip(this.AppraisalSummaryPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
            }
        }

        #endregion PictureBox MouseEnter Event

        #region Common Methods

        #region LoadSliceObject

        /// <summary>
        /// Loads the slice object.
        /// </summary>
        private void LoadSliceObject()
        {

            this.pageMode = TerraScanCommon.PageModeTypes.View;
          

            this.appraisalSummaryData.Clear();
            this.appraisalSummaryData = this.form35000Controll.WorkItem.F35000_GetAppraisalSummaryObjects(this.parcelId);

            //// Creates The Relation for the DataSet
            this.appraisalSummaryData.Relations.Add(this.appraisalSummaryData.GetObjectSummaryTable.Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName], this.appraisalSummaryData.GetSliceSummaryTable.Columns[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName]);

            if (this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count > 0)
            {
                this.rowCount = true;
                ////Added By Ramya (Sprint40 Change Request)
                //// this.appraisalSummaryData.
                this.AppraisalSummaryGrid.DataSource = this.appraisalSummaryData;
            }
            else
            {
                this.AppraisalSummaryGrid.DataSource = this.appraisalSummaryData;
            }

            ////Coding added for the issue 4624 by malliga on 9/10/2009
            for (int i = 0; i < this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count; i++)
            {
                if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["RollBit"].ToString().Equals("False"))
                {
                    this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                    this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
                }

                if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ValueBit"].ToString().Equals("False"))
                {
                    this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                    this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
                }

            }
       
        }

        #endregion LoadSliceObject

        #region CustomizeGrid

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            //InitializeObjectValueLists();
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].FormatInfo = CultureInfoConverter.
            this.AppraisalSummaryGrid.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn;
            EditorWithText textEditor = new EditorWithText();

            //// To make Band[1] Column Header Visible False
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].ColHeadersVisible = false;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].ColHeadersVisible = false;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.False;

            //// Change Band[0] Column style to URL
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            // * this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Editor = this.FormattedLinkEditor;

            //// code added by sri for avoiding changing of link color
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            //// this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellAppearance.ForeColor = Color.Blue;
            // * this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellAppearance.FontData.Underline = Infragistics.Win.DefaultableBoolean.True;
            // * this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Editor = textEditor;

            //Make Editable Column Description
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            // this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;


            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;     
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].EditorControl = this.SliceTypeUltraFormattedTextEditor;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].EditorControl = this.DescriptionEditor;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].EditorControl = this.DescriptionEditor; 
            //Make link cell for Value and Roll
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].EditorControl = this.SliceTypeUltraFormattedTextEditor;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].EditorControl = this.SliceTypeUltraFormattedTextEditor;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellActivation = Activation.ActivateOnly;

            // * this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;
            //Make link cell for Value and Roll
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellButtonAppearance.Image = (System.Drawing.Bitmap)Properties.Resource2.ValueDisabled;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.ValueEnabled;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.ValueEnabled;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.ImageBackgroundStyle = Infragistics.Win.ImageBackgroundStyle.Stretched;
            //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 0, 0, 0);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 0, 0, 0);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 0, 0, 0);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.ImageBackgroundStretchMargins = new Infragistics.Win.ImageBackgroundStretchMargins(0, 0, 0, 0);

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.EditCellAppearance.BackColor = Color.FromArgb(255, 255, 121);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.EditCellAppearance.ForeColor = Color.Black;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.EditCellAppearance.ForeColor = Color.Black;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.EditCellAppearance.BackColor = Color.FromArgb(255, 255, 121);


            //// Make the Row Indentation to left Corner
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Indentation = -2;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Indentation = -2;

            this.AppraisalSummaryGrid.DisplayLayout.Appearance.BorderColor = Color.FromArgb(31, 65, 103);

            //// Change the Row selector Apprance for Band[1] and hide the row selector of band[0]
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BorderColor = Color.Black;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowSelectorAppearance.ThemedElementAlpha = Alpha.Default;

            //// Change the Row RowAppearance
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowAppearance.BackColor = Color.FromArgb(31, 65, 103);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowAppearance.BorderColor = Color.FromArgb(31, 65, 103);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowAppearance.ForeColor = Color.White;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.SizeInPoints = 10;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.RowAppearance.FontData.Bold = DefaultableBoolean.True;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowAppearance.BackColor = Color.FromArgb(217, 217, 217);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowAppearance.ForeColor = Color.FromArgb(31, 31, 249);
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].AddNew().Cells[0].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDown;

            if (this.PermissionFiled.newPermission)
            {
                //// Allows Row Add with in the Band
                this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.FixedAddRowOnBottom;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.FixedAddRowOnBottom;
            }

            //// Band[1] Row Alternate apperance
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor = Color.FromArgb(255, 255, 255);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.RowAlternateAppearance.BackColor2 = Color.White;

            //// Column Sizing as per Specification
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Width = this.AppraisalSummaryGrid.Left + 200;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Width = this.AppraisalSummaryGrid.Left + 225;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].MaxWidth = this.AppraisalSummaryGrid.Left + 56;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].MaxWidth = this.AppraisalSummaryGrid.Left + 56;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].MaxWidth = this.AppraisalSummaryGrid.Left + 91;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].MaxWidth = this.AppraisalSummaryGrid.Left + 35;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].MaxWidth = this.AppraisalSummaryGrid.Left + 110;
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Width = this.AppraisalSummaryGrid.Left + 102;

            //// Text Alignment of Band[0] Cells
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            // * this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;

            //// Text Alignment of Band[1] Cells
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;

            //// default values using the InitializeTemplateAddRow event of the UltraGrid.
            //// Applies Default String "Description" to the Object column
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].DefaultCellValue = "Description";
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.BackColor = Color.White;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextVAlign = VAlign.Middle;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextHAlign = HAlign.Center;

            //// default values using the InitializeTemplateAddRow event of the UltraGrid.
            //// Applies Default String "Type" to the SliceType column in the Band[1]
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].DefaultCellValue = "Types";
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.BackColor = Color.White;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextVAlign = VAlign.Middle;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextHAlign = HAlign.Left;

            //// default values using the InitializeTemplateAddRow event of the UltraGrid.
            //// Applies Default String "Type" to the ObjectType column in the Band[0]
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].DefaultCellValue = "(New Object)";
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.BackColor = Color.White;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextVAlign = VAlign.Middle;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.TextHAlign = HAlign.Left;
           //this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Band.Override.TemplateAddRowAppearance.BorderColor = Color.Black;

            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            // this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            this.AppraisalSummaryGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;

            //// Row selected appearance changed to white
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Override.SelectedCellAppearance.ForeColor = Color.White;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Override.SelectedCellAppearance.ForeColor = Color.White;

            //// Issue Fix : #206 - Fixed
            //// Issue Fix Code Starts Here
            if (this.AppraisalSummaryGrid.Rows.Count == 0)
            {
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellDisplayStyle = Infragistics.Win.UltraWinGrid.CellDisplayStyle.FormattedText;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellAppearance.ForeColor = Color.White;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellAppearance.FontData.Underline = DefaultableBoolean.True;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellAppearance.Cursor = Cursors.Hand;

                // * this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
                this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            }

            ////////Coding Addedby biju [For Roll and Value Column Bachground and Forecolor Changes]
            ////for (int i = 0; i < this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count; i++)
            ////{
            ////    this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.RowFilter = "ObjectID = " + this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ObjectID"].ToString();
            ////    int sliceCount = this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.Count;
            ////    if (sliceCount > 0)
            ////    {
            ////        this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.RowFilter = "RollBit=0 and ObjectID = " + this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ObjectID"].ToString();
            ////        if (this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.Count == sliceCount)
            ////        {
            ////            this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
            ////            this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128,128);
            ////        }

            ////        this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.RowFilter = "ValueBit=0 and ObjectID = " + this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ObjectID"].ToString();
            ////        if (this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.Count == sliceCount)
            ////        {
            ////            this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
            ////            this.AppraisalSummaryGrid.Rows[i].Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
            ////        }
            ////    }

            ////    this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.RowFilter = "";
            ////}
            //// Issue Fix Code Ends Here

        }

        #endregion CustomizeGrid

        #region ColumnPositioning

        /// <summary>
        /// Columns the positioning.
        /// </summary>
        private void ColumnPositioning()
        {
            // Visble Position of Band[0]
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Header.VisiblePosition = 0;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Header.VisiblePosition = 2;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Header.VisiblePosition = 3;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Header.VisiblePosition = 4;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Header.VisiblePosition = 5;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Header.VisiblePosition = 6;

            // Visble Position of Band[1]
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Header.VisiblePosition = 0;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Header.VisiblePosition = 2;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Header.VisiblePosition = 3;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Header.VisiblePosition = 4;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Header.VisiblePosition = 5;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Header.VisiblePosition = 6;

            // Hide Band[0] Columns
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ParcelIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ValueBitColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.RollBitColumn.ColumnName].Hidden = true;
            //   this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.VisibilityColumn.ColumnName].Hidden = true;

            // Hide Band[1] Columns
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ParcelIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ValueBitColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.ColumnName].Hidden = true;
            this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Hidden = true;
            ////this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Hidden = true;

            ////////Added By Ramya (Sprint 40 chnge Request)
            ////if (this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count > 0)
            ////{
            ////    if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[0][this.appraisalSummaryData.GetObjectSummaryTable.VisibilityColumn].ToString() == "False")
            ////    {
            ////        this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Hidden= true;
            ////        this.AppraisalSummaryGrid.DisplayLayout.Bands[1].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].MaxWidth = 1000;
            ////        ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Width = this.AppraisalSummaryGrid.Left + 200;
            ////        ////this.AppraisalSummaryGrid.DisplayLayout.Rows[0].Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName]
            ////        ////this.AppraisalSummaryGrid.DisplayLayout.Rows[0].Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].= "";

            ////        ////this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].DefaultCellValue= string.Empty;
            ////    }
            ////    else
            ////    {
            ////        this.AppraisalSummaryGrid.DisplayLayout.Bands[0].Columns[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Hidden = false;
            ////    }
            ////}
        }
        ////Till Here      
        #endregion ColumnPositioning

        #region NewRowStyle

        /// <summary>
        /// News the row style.
        /// </summary>
        private void NewRowStyle()
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow row = this.AppraisalSummaryGrid.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.Last);

            if (this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Exists("Status"))
            {
                return;
            }

            ValueList objValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Add("Status");

            DataTable table2 = new DataTable();
            table2 = this.appraisalSummaryData.GetSliceSummaryTable;

            for (int i = 0; i < table2.Rows.Count; i++)
            {
                objValueList.ValueListItems.Add((int)(table2.Rows[i].ItemArray[0]), table2.Rows[i].ItemArray[3].ToString());
            }

            row.ChildBands.LastRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists["Status"];
            row.ChildBands.LastRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
        }

        #endregion NewRowStyle

        #region ShowForm

        /// <summary>
        /// Shows the value slice sub form.
        /// </summary>
        private void ShowValueSliceSubForm()
        {
            if (this.activeRow != null)
            {
                if (this.activeRow.Band.Index == 1)
                {

                    if (this.activeCell.Column.Index == 4)
                    {
                        ////if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))//(!this.IsEditStatus)
                        ////{
                        FormInfo sliceForm = new FormInfo();
                        int formNo;

                        if (this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.ColumnName].Value != null)
                        {
                            int.TryParse(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.ColumnName].Value.ToString(), out formNo);
                            sliceForm = TerraScanCommon.GetFormInfo(formNo);
                            sliceForm.optionalParameters = new object[1];
                            sliceForm.optionalParameters[0] = this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value;
                            this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
                        }
                        //}
                    }
                    if (this.activeCell.Column.Index == 9)
                    {
                        DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value);
                        if (rows.Length >= 1)
                        {
                            this.isCurrentRow = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);

                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                            {
                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                            }
                            else
                            {
                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                            }

                            this.SetEditRecord();
                        }
                    }

                    if (this.activeCell.Column.Index == 11)
                    {
                        DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value);
                        if (rows.Length >= 1)
                        {
                            this.isCurrentRow = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);

                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"].ToString().Equals("True"))
                            {
                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"] = false;
                            }
                            else
                            {
                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                            }

                            this.SetEditRecord();
                        }

                    }


                }
                else if (this.activeRow.Band.Index == 0)
                {

                    if (this.activeCell.Column.Index == 3)
                    {
                        if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)) //if (!this.IsEditStatus)
                        {
                            ////FormInfo sliceForm = new FormInfo();
                            ////sliceForm = TerraScanCommon.GetFormInfo(3001);
                            ////sliceForm.optionalParameters = new object[1];
                            ////sliceForm.optionalParameters[0] = this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value;
                            ////this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
                            if (this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value != null)
                            {
                                object[] optionalParameter = new object[] { this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value, this.parcelId, 3001, false };
                                IFormEngineService objFormEngineService = this.form35000Controll.WorkItem.Services.Get<IFormEngineService>();
                                Form objectMangement = objFormEngineService.GetForm(3001, optionalParameter, this.form35000Controll.WorkItem);
                                ////open form in view mode - possible to edit
                                if (objectMangement != null)
                                {
                                    if (objectMangement.ShowDialog().Equals(DialogResult.OK))
                                    {
                                        ////this.parcelId = Convert.ToInt32(TerraScanCommon.GetValue(parcelSelectionForm, "ParcelID"));
                                        ////this.GetParcelDetails(this.parcelId);
                                    }
                                }
                            }

                        }
                    }
                    if (this.activeCell.Column.Index == 7)
                    {
                        DataRow[] rows = this.appraisalSummaryData.GetObjectSummaryTable.Select("ObjectID = " + this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value);
                        if (rows.Length >= 1)
                        {
                            this.isCurrentRow = this.appraisalSummaryData.GetObjectSummaryTable.Rows.IndexOf(rows[0]);
                            if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                            {
                                this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                            }
                            else
                            {
                                this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                            }

                            this.SetEditRecord();
                        }

                    }
                    if (this.activeCell.Column.Index == 9)
                    {
                        DataRow[] rows = this.appraisalSummaryData.GetObjectSummaryTable.Select("ObjectID = " + this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value);
                        if (rows.Length >= 1)
                        {
                            this.isCurrentRow = this.appraisalSummaryData.GetObjectSummaryTable.Rows.IndexOf(rows[0]);
                            if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"].ToString().Equals("True"))
                            {
                                this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = false;
                            }
                            else
                            {
                                this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                            }

                            this.SetEditRecord();
                        }
                    }

                    if (this.activeCell.Column.Index == 10)
                    {

                        this.camaForm = new Form();
                        this.camaForm = TerraScanCommon.ShowSketchForm(3200);
                        Form formAlreadyOpened = (Form)TerraScanCommon.GetObject(this.camaForm, "GetOpenedForm");
                        if (formAlreadyOpened != null)
                        {
                            formAlreadyOpened.Close();
                            Boolean formClosed = (Boolean)TerraScanCommon.GetObject(this.camaForm, "FormOpened");
                            this.formOpened = formClosed;
                            if (!formClosed)
                            {
                                this.camaForm = TerraScanCommon.ShowSketchForm(3200);
                            }
                        }


                        int objectID = Convert.ToInt32(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName].Value.ToString());

                        //if (!this.formOpened)
                        //{
                        TerraScanCommon.SetValue(this.camaForm, "SetObjectID", objectID);
                        try
                        {
                            //TerraScanCommon.SetValue(this.camaForm, "SetMDI", ((Form)this.camaForm));
                            TerraScanCommon.SetValue(this.camaForm, "SetMDI", ((Form)((Form)this.ParentForm).ParentForm));
                        }
                        catch (Exception ex)
                        {
                        }
                        //}
                        ////TerraScanCommon.SetValue(camaForm, "SetObjectID", objectID);
                        ////TerraScanCommon.SetValue(camaForm, "SetMDI", ((Form)((Form)this.ParentForm).ParentForm));
                        this.formOpened = (Boolean)TerraScanCommon.GetObject(this.camaForm, "FormOpened");
                    }
                }
            }
        }

        #endregion ShowForm

        #region InsertValueSlice

        /// <summary>
        /// Inserts the value slice.
        /// </summary>
        private void InsertValueSlice()
        {
            string description = this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value.ToString();
            int sliceType;
            int.TryParse(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString(), out sliceType);
            int objectId = Convert.ToInt32(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName].Value.ToString());

            if (sliceType > 0)
            {
                DataSet ds = new DataSet("Root");
                DataTable dt = new DataTable("Table");
                dt.Columns.Add(this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.Caption);
                dt.Columns.Add(this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.Caption);
                dt.Columns.Add(this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.Caption);
                DataRow dr = dt.NewRow();
                dr[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.Caption] = description;
                dr[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.Caption] = sliceType;
                dr[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.Caption] = objectId;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
                string newXML = ds.GetXml();

                /* ////Added By Ramya.D(Sprint40 ChangeOrder) 
                 this.appraisalSummaryData = this.form35000Controll.WorkItem.F35000_CheckAppraisalSummaryUser(0, objectId, TerraScanCommon.UserId);
                 if (this.appraisalSummaryData.f35000_checkAppraisalUserTable.Rows.Count > 0)
                 {
                     F35000AppraisalSummaryData.f35000_checkAppraisalUserTableRow userTableRow = (F35000AppraisalSummaryData.f35000_checkAppraisalUserTableRow)this.appraisalSummaryData.f35000_checkAppraisalUserTable.Rows[0];
                     this.userName = userTableRow.Name_Display;
                     this.outPutValue = Convert.ToInt32(userTableRow.PrimaryKeyID);
                     if (this.outPutValue != -1)
                     {*/
                this.keyField = "ValueSliceID";
                int outInsertValue = this.form35000Controll.WorkItem.F35000_InsertOrUpdateValueSlice(null, newXML, TerraScan.Common.TerraScanCommon.UserId);
                if (!WSHelper.IsOnLineMode)
                    TerraScanCommon.AddFieldUseValues(outInsertValue, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                if (outInsertValue > 0)
                {
                    this.updateStatus = true;
                    this.LoadSliceObject();
                    this.ShowValueSlices(outInsertValue);
                    this.SetSmartpartHeight();
                }

                /*}
                else
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("ParcelLock") + this.userName, SharedFunctions.GetResourceString("ParcelLockHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {

                        this.updateStatus = true;
                        this.LoadSliceObject();
                     //   this.ShowValueSlices(this.parcelId);
                        this.SetSmartpartHeight();

                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value = "Type";
                        //////////this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(166, 166, 166);////.Band.Override.RowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
                        //////this.activeRow.Band.Columns[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(166, 166, 166);
                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value = "Description";
                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(166, 166, 166);////.Band.Override.RowAppearance.ForeColor = Color.FromArgb(166, 166, 166);
                           
                            
                               
                        //////this.AppraisalSummaryGrid.ActiveRow = this.AppraisalSummaryGrid.GetRow(Infragistics.Win.UltraWinGrid.ChildRow.First);
                          
                    }
                }
            }*/
                ////Till Here
                //this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                // this.pageMode = TerraScanCommon.PageModeTypes.Edit;  
            }
            else
            {
                this.updateStatus = false;
                MessageBox.Show("Please Select any SliceType", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion InsertValueSlice

        #region InsertObjectValue

        /// <summary>
        /// Inserts the object value.
        /// </summary>
        private void InsertObjectValue()
        {
            int parcelId = this.parcelId;
            int outInsertObjectvalue;

            Int16 objectTypeId;
            Int16.TryParse(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Value.ToString(), out objectTypeId);
            string description = this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Value.ToString();

            if (parcelId != -99)
            {
                if (objectTypeId > 0)
                {
                    outInsertObjectvalue = this.form35000Controll.WorkItem.F35000_InsertObject(parcelId, objectTypeId, description, TerraScan.Common.TerraScanCommon.UserId);
                    if (!WSHelper.IsOnLineMode)
                        TerraScanCommon.AddFieldUseValues(outInsertObjectvalue, this.keyField, this.formNo, null, TerraScanCommon.UserId);
                    if (outInsertObjectvalue > 0)
                    {
                        this.updateStatus = true;
                        this.LoadSliceObject();
                        this.SetSmartpartHeight();

                        // 20110912 Latha - DBCall to get config value for "SD" to implement CO #13385
                        CommentsData stateDataSet = new CommentsData();
                        stateDataSet = this.form35000Controll.WorkItem.GetConfigDetails("State");
                        if (stateDataSet.Tables.Count > 0 && stateDataSet.Tables[stateDataSet.GetCommentsConfigDetails.TableName].Rows.Count > 0)
                        {
                            string stateName = stateDataSet.GetCommentsConfigDetails.Rows[0][stateDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
                            if (stateName.ToUpper().Trim().Equals("SD"))
                            {
                                object[] optionalParameter = new object[] { outInsertObjectvalue, this.parcelId, 3001, true };
                                IFormEngineService objFormEngineService = this.form35000Controll.WorkItem.Services.Get<IFormEngineService>();
                                Form objectMangement = objFormEngineService.GetForm(3001, optionalParameter, this.form35000Controll.WorkItem);
                                ////open form in view mode - possible to edit
                                if (objectMangement != null)
                                {
                                    objectMangement.ShowDialog();
                                }
                            }
                        }
                    }
                    try
                    {
                        //this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.FormSlice_SetViewMode(this, new DataEventArgs<int>(this.masterFormNo));
                        // this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    this.updateStatus = false;
                    MessageBox.Show("Please Select any ObjectType", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                this.updateStatus = false;
                MessageBox.Show("Cannot be Inserted for the Invalid KeyId", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion InsertObjectValue

        #region PopulateComboDetails

        /// <summary>
        /// Populates the combo details.
        /// </summary>
        /// <param name="row">The row.</param>
        private void PopulateComboDetails(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            string valueListName = System.Guid.NewGuid().ToString();

            if (this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Exists(valueListName))
            {
                return;
            }

            ValueList objValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Add(valueListName);

            // Checks for the Band Index and loads corresponding Table in the Combo
            // ValueList is Just like a datasource in the combo, 
            // which holds DisplayMemeber and DisplayValue                    

            if (row.Band.Index == 1)
            {
                this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListObjectSliceTypes(this.parcelId);

                if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
                {
                    // Iterates to Add DisplayMember and DisplayValue from Table
                    for (int i = 0; i < this.listObjectSliceData.ListSliceTypes.Rows.Count; i++)
                    {
                        objValueList.ValueListItems.Add(Convert.ToInt16(this.listObjectSliceData.ListSliceTypes.Rows[i].ItemArray[0].ToString()), this.listObjectSliceData.ListSliceTypes.Rows[i].ItemArray[2].ToString());
                    }

                    // Load the ValueList to the Grid and Changes the Style of the Cell to DropDown
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists[valueListName];
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                    // Changes the particular cell style to URL
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value = "Add";
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;

                    ////e.Row.Band.Columns[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].DefaultCellValue = "Description";
                }
            }
            else if (row.Band.Index == 0)
            {
                this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListObjectSliceTypes(this.parcelId);

                // * row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].EditorControl = this.ObjectTypeUltraFormattedTextEditor;

                if (this.listObjectSliceData.ListObjectTypes.Rows.Count > 0)
                {
                    // Iterates to Add DisplayMember and DisplayValue from Table
                    for (int i = 0; i < this.listObjectSliceData.ListObjectTypes.Rows.Count; i++)
                    {
                        objValueList.ValueListItems.Add(Convert.ToInt16(this.listObjectSliceData.ListObjectTypes.Rows[i].ItemArray[0].ToString()), this.listObjectSliceData.ListObjectTypes.Rows[i].ItemArray[1].ToString());
                    }

                    // Load the ValueList to the Grid and Changes the Style of the Cell to DropDown
                    row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].ValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists[valueListName];
                    row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;

                    // Changes the particular cell style to URL
                  
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Value = "Add";
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
                   
                }
            }
        }

        #endregion PopulateComboDetails

        #region DeactivateHyperLink

        /// <summary>
        /// Deactivates the hyper link.
        /// </summary>
        /// <param name="row">The row.</param>
        private void DeactivateHyperLink(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            // Checks for the Form value, 
            // If FORM value is NULL, then there will be NO hyperlink and it will be consider as
            // an Formatted Text Column
            if (row.Band.Index == 1)
            {
                if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.ColumnName].Value.ToString() == string.Empty)
                {
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.FormattedText;
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Appearance.FontData.Underline = DefaultableBoolean.True;
                   
                }
            }
        }

        #endregion

        #region ExpandAllParentRows

        /// <summary>
        /// Expands all parent rows.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ExpandAllParentRows(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            this.AppraisalSummaryGrid.ActiveRow = row;
            this.AppraisalSummaryGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.ExpandRow, false, false);
            this.AppraisalSummaryGrid.DisplayLayout.Rows[row.Index].ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            this.AppraisalSummaryGrid.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None;
        }

        #endregion ExpandAllParentRows

        #region ValueRowAppearance

        /// <summary>
        /// Values the roll appearance.
        /// </summary>
        /// <param name="row">The row.</param>
        private void ValueCellAppearance(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            iSliceId = 0;
            if (row.Band.Index == 1)
            {

                if (this.appraisalSummaryData.GetSliceSummaryTable.Rows.Count > 0 && !this.isRollValueChanged)
                {
                    bool isValue = Convert.ToBoolean(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueBitColumn.ColumnName].Value);
                    bool isRoll = Convert.ToBoolean(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName].Value);
                    decimal adjustmentValue;
                    decimal.TryParse(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value.ToString(), out adjustmentValue);


                    //if (!this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Removed Land Value"))
                    if(!row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Removed Land Value"))
                    {
                        // If the bit field value is false, then it Changes the Appearance of valuestring column 
                        if (!isValue)
                        {
                            // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].ButtonAppearance.Image = (System.Drawing.Bitmap)Properties.Resource2.ValueDisabled;      
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Disabled;
                        }
                        else
                        {
                            // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].ButtonAppearance.Image = (System.Drawing.Bitmap)Properties.Resource2.ValueEnabled;      
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.FromArgb(0, 0, 0); 
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.White;
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ForeColor = Color
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Enabled;
                        }
                    }
                    else
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                       // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                       // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Obj_RemovelandDisabled;
                      
                    }


                    if (!row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Removed Land Value"))
                    {
                        // If the bit field value is false, then it Changes the Appearance of RollString column 
                        if (!isRoll)
                        {
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.Red;
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Disabled;
                        }
                        else
                        {
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Enabled;
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.FromArgb(0, 0, 0); 
                            //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.White;

                        }
                    }
                    else
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        //row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                       // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Obj_RemoveLandDisabled;
                    }

                    int objectId = Convert.ToInt32(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName].Value);

                    this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListSliceTypes(objectId);

                    if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
                    {
                        DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select();
                        for (int i = 0; i < objectSlices.Length; i++)
                        {
                            if (objectSlices[i].ItemArray[5].ToString() == "True" && objectSlices[i].ItemArray[2].ToString() == "Income Approach")
                            {
                                iSliceId = Convert.ToInt32(objectSlices[i].ItemArray[0]);
                            }
                        }
                    }

                    if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Income Approach") &&
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.ColumnName].Value.ToString().Equals(Convert.ToString(iSliceId)))
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                       // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                       // row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.Cursor = Cursors.Default;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Obj_RemovelandDisabled;
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Obj_RemoveLandDisabled;
                    }

                    ////if (adjustmentValue < 0)
                    ////{
                    ////    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                    ////    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                    ////    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);

                    ////    if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value.ToString().Contains("-"))
                    ////    {
                    ////        string decimalValue = row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value.ToString();
                    ////        decimalValue = decimalValue.Replace("-", "(");
                    ////        decimalValue = decimalValue + ")";
                    ////        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value = decimalValue;                           
                    ////        if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Value.ToString().Contains("-"))
                    ////        {                                
                    ////            decimalValue = row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Value.ToString().Replace("-", "(");
                    ////            decimalValue = decimalValue + ")";
                    ////            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Value = decimalValue;
                    ////        }
                    ////        else
                    ////        {
                    ////            decimalValue = "(" + row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Value.ToString() + ")";
                    ////            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Value = decimalValue;
                    ////        }
                    ////    }
                    ////}

                    // Font Color Appearance of AdjustmentValue, AdjustmentType and AmountColumn
                    string adjustmentValue1 = row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value.ToString();
                    if (adjustmentValue1.Contains("(") && adjustmentValue1.Contains(")"))
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                    }

                    // Font Appearance of AdjustmentValue, AdjustmentType and AmountColumn
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.FontData.Bold = DefaultableBoolean.True;
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.FontData.Bold = DefaultableBoolean.True;
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.FontData.Bold = DefaultableBoolean.True;
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.FontData.Name = "Courier New";
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.FontData.Name = "Courier New";
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.FontData.Name = "Courier New";
                }
            }

            if (row.Band.Index == 0)
            {
                if (this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count > 0 && !this.isRollValueChanged)
                {
                    bool isValue = Convert.ToBoolean(row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueBitColumn.ColumnName].Value);
                    bool isRoll = Convert.ToBoolean(row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollBitColumn.ColumnName].Value);

                        if (!isValue)
                        {
                            //   row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].ButtonAppearance.Image = (System.Drawing.Bitmap)Properties.Resource2.ValueDisabled;      
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
                            row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Obj_Disabled;
                        }
                        else
                        {
                            // row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].ButtonAppearance.Image = (System.Drawing.Bitmap)Properties.Resource2.ValueEnabled;      
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.FromArgb(0, 0, 0); 
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);

                            row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Value_Obj_Enabled;

                        }


                        // If the bit field value is false, then it Changes the Appearance of RollString column 
                        if (!isRoll)
                        {
                            row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Obj_Disabled;
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(77, 77, 77);
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 128, 128);
                        }
                        else
                        {
                            row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.ImageBackground = (System.Drawing.Bitmap)Properties.Resource2.Roll_Obj_Enabled;
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.FromArgb(0, 0, 0); 
                            //row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31,65,103);
                        }
                }
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Appearance.ForeColor = Color.White;
            }
        }

        #endregion ValueCellAppearance

        #region DisableCellEdit

        /// <summary>
        /// Disables the cell edit.
        /// </summary>
        /// <param name="row">The row.</param>
        private void DisableCellEdit(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row.Band.Index == 1)
            {
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;

                ////if (!string.IsNullOrEmpty(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value.ToString()))
                ////{
                ////    if (!this.descriptionStatus)
                ////    {
                ////        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value = string.Empty;
                ////        this.descriptionStatus = true;
                ////    }
                ////}

                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColor = Color.White;
            }

            if (row.Band.Index == 0)
            {
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.Disabled;

                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.ColumnName].Appearance.BackColor = Color.White;
                row.Cells[this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.BackColor = Color.White;
            }
        }

        #endregion

        #region CreateObjectSliceDataTable

        /// <summary>
        /// Creates the object slice data table.
        /// </summary>
        private void CreateObjectSliceDataTable()
        {
            DataSet emptyObjectSliceValue = new DataSet("emptyObjectSliceValue");
            DataTable objectDataTable = new DataTable("objectDataTable");
            DataColumn[] objectDataColumn = new DataColumn[] {
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.AdjustmentValueColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.Caption),                
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ObjectTypeIDColumn.Caption),                
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.OValueColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ParcelIDColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.RollBitColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.TypeColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ValueBitColumn.Caption),
                new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.Caption),
               // new DataColumn(this.appraisalSummaryData.GetObjectSummaryTable.VisibilityColumn.Caption)
            };
            objectDataTable.Columns.AddRange(objectDataColumn);

            DataTable sliceDataTable = new DataTable("sliceDataTable");
            DataColumn[] sliceDataColumn = new DataColumn[] { new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.Caption),                
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.Caption),                
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ParcelIDColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ValueBitColumn.Caption), 
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.Caption),                
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.Caption),
                new DataColumn(this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.Caption) };
            sliceDataTable.Columns.AddRange(sliceDataColumn);

            emptyObjectSliceValue.Tables.Add(objectDataTable);
            emptyObjectSliceValue.Tables.Add(sliceDataTable);

            emptyObjectSliceValue.Relations.Add(emptyObjectSliceValue.Tables["objectDataTable"].Columns[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName], emptyObjectSliceValue.Tables["sliceDataTable"].Columns[this.appraisalSummaryData.GetSliceSummaryTable.ObjectIDColumn.ColumnName]);

            this.AppraisalSummaryGrid.DataSource = emptyObjectSliceValue;

            this.ColumnPositioning();
        }

        #endregion CreateObjectSliceDataTable

        #region SetSmartpartHeight

        /// <summary>
        /// Grids the resize.
        /// </summary>
        private void SetSmartpartHeight()
        {
            int tempChildHeight;
            int tempParentHeight;



            if (this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count > 0)
            {
                if (this.PermissionFiled.newPermission)
                {
                    this.parentRowCount = this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count;
                    this.childRowCount = this.appraisalSummaryData.GetSliceSummaryTable.Rows.Count + this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count;
                    if (this.childRowHeight != 0)
                    {
                        tempChildHeight = this.childRowCount * this.childRowHeight;
                        tempParentHeight = this.parentRowCount * (this.parentRowHeight);
                    }
                    else
                    {
                        tempChildHeight = this.childRowCount * 20;
                        tempParentHeight = this.parentRowCount * (this.parentRowHeight);
                    }

                    //// To Reduce Extra space in the Bottom
                    int tempCalc = this.childRowCount - this.parentRowCount;
                    int gridHeight = tempParentHeight + tempChildHeight + (this.parentRowHeight);
                    gridHeight = (gridHeight - tempCalc);
                    gridHeight = gridHeight + 8;
                    //Added for Summary bar at bottom 
                    decimal temptotal = 0;
                    listObjectSliceData= this.form35000Controll.WorkItem.F35000_ObjectTotal(parcelId);
                    if (listObjectSliceData.f35000ObjectTotal.Rows.Count > 0)
                    {
                        temptotal = Convert.ToDecimal(listObjectSliceData.f35000ObjectTotal.Rows[0][listObjectSliceData.f35000ObjectTotal.TotalObjectValueColumn.ColumnName].ToString());
                    }
                    

                    //for (int i = 0; i < parentRowCount; i++)
                    //{
                    //    decimal temp;
                    //    string temp1 = this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ovalue"].ToString();
                    //    if (temp1.Contains("("))
                    //    {
                    //        string temp2 = temp1.Replace('(', '-').Replace(')', ' ');
                    //        decimal.TryParse(temp2, out  temp);
                    //    }
                    //    else
                    //    {
                    //        decimal.TryParse(this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ovalue"].ToString(), out temp);
                    //    }
                    //    temptotal = temptotal + temp;
                    //}
                    this.AppraisalGridpanel.Height = gridHeight;
                    this.AppraisalSummaryGrid.Height = this.AppraisalGridpanel.Height;
                    this.AppraisalSummaryPictureBox.Height = this.AppraisalSummaryGrid.Height + 24;
                    this.TotalGridpanel.Location = new System.Drawing.Point(0, this.AppraisalSummaryGrid.Height-1);
                    string totVal = temptotal.ToString();
                    this.TotalLabelValue.Text = totVal;
                    string strColor= this.TotalLabelValue.ForeColor.ToString();
                    if (strColor == "Color [A=255, R=128, G=0, B=0]")
                    {
                       TotalLabelValue.ForeColor = Color.White;
                    }
                    this.Height = this.AppraisalSummaryGrid.Height + 25;
                    this.TotalGridpanel.BringToFront();
                    //this.TotalGridpanel.SendToBack();
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = "D35000.F35000";
                    sliceResize.SliceFormHeight = gridHeight + 25; 
                    ////if (!this.flagFormLoad)
                    ////{
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.AppraisalSummaryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AppraisalSummaryPictureBox.Height, this.AppraisalSummaryPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
                    ////}

                    if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint + 50);
                    }

                }
                else
                {
                    this.parentRowCount = this.appraisalSummaryData.GetObjectSummaryTable.Rows.Count;
                    this.childRowCount = this.appraisalSummaryData.GetSliceSummaryTable.Rows.Count;
                    ////Made changes to reduce panel space (Bug#4251).
                    int groupcount = 0;
                    DataRow[] childDataRow = this.appraisalSummaryData.GetObjectSummaryTable.Select();

                    foreach (DataRow child in childDataRow)
                    {
                        if (!string.IsNullOrEmpty(child.ItemArray[0].ToString()))
                        {
                            DataRow[] childId = this.appraisalSummaryData.GetSliceSummaryTable.Select("ObjectID = " + child.ItemArray[0].ToString());

                            if (childId.Length > 0)
                            {
                                groupcount = groupcount + 1;
                            }
                        }
                    }

                    if (this.childRowHeight != 0)
                    {
                        tempChildHeight = this.childRowCount * this.childRowHeight;
                        tempParentHeight = this.parentRowCount * (this.parentRowHeight);
                    }
                    else
                    {
                        tempChildHeight = this.childRowCount * 20;
                        tempParentHeight = this.parentRowCount * (this.parentRowHeight);
                    }
                    //// To Reduce Extra space in the Bottom
                    int tempCalc = this.childRowCount - this.parentRowCount;
                    ////int gridHeight = tempParentHeight + tempChildHeight;
                    int gridHeight = tempParentHeight + tempChildHeight + (this.parentRowHeight);
                    ////gridHeight = (gridHeight - tempCalc) + 10;
                    if (this.parentRowCount != groupcount)
                    {
                        tempCalc = tempCalc + ((this.parentRowCount - groupcount) * 12);
                    }
                    ////gridHeight = (gridHeight - tempCalc) ;
                    gridHeight = (gridHeight - tempCalc);
                    gridHeight = gridHeight + 8;
                    decimal temptotal = 0;
                    listObjectSliceData = this.form35000Controll.WorkItem.F35000_ObjectTotal(parcelId);
                    if (listObjectSliceData.f35000ObjectTotal.Rows.Count > 0)
                    {
                        temptotal = Convert.ToDecimal(listObjectSliceData.f35000ObjectTotal.Rows[0][listObjectSliceData.f35000ObjectTotal.TotalObjectValueColumn.ColumnName].ToString());
                    }
                    //for (int i = 0; i < parentRowCount; i++)
                    //{
                    //    int temp;
                    //    int.TryParse(this.appraisalSummaryData.GetObjectSummaryTable.Rows[i]["ovalue"].ToString(), out temp);
                    //    temptotal = temptotal + temp;
                    //}
                    this.AppraisalGridpanel.Height = gridHeight;
                    this.AppraisalSummaryGrid.Height = this.AppraisalGridpanel.Height;
                    this.AppraisalSummaryPictureBox.Height = this.AppraisalSummaryGrid.Height + 25;
                    this.TotalGridpanel.Location = new System.Drawing.Point(-1, this.AppraisalSummaryGrid.Height);
                    this.TotalLabelValue.Text = temptotal.ToString();
                    this.Height = this.AppraisalSummaryGrid.Height + 25;
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = "D35000.F35000";
                    sliceResize.SliceFormHeight = gridHeight + 25;
                    ////if (!this.flagFormLoad)
                    ////{
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.AppraisalSummaryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AppraisalSummaryPictureBox.Height, this.AppraisalSummaryPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
                }
            }
            else
            {
                this.AppraisalGridpanel.Height = 32;
                this.AppraisalSummaryGrid.Height = this.AppraisalGridpanel.Height;
                this.AppraisalSummaryPictureBox.Height = this.AppraisalSummaryGrid.Height + 25;
                this.TotalGridpanel.Location = new System.Drawing.Point(-1, this.AppraisalSummaryGrid.Height);
                //this.TotalGridpanel.BringToFront();
                this.TotalLabelValue.Text = "0";
                this.Height = this.AppraisalSummaryGrid.Height + 25;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = "D35000.F35000";
                sliceResize.SliceFormHeight = this.AppraisalSummaryGrid.Height + 25;
                ////if (!this.flagFormLoad)
                ////{
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.AppraisalSummaryPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AppraisalSummaryPictureBox.Height + 40, this.AppraisalSummaryPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
            }
        }

        #endregion SetSmartpartHeight

        #region ValueLists

        #region InitializeSliceValueLists

        /// <summary>
        /// Initializes the slice value lists.
        /// </summary>
        /// <param name="objectId">objectId</param>
        private void InitializeSliceValueLists(int objectId)
        {
            this.sliceValueListName = System.Guid.NewGuid().ToString();

            if (this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Exists(this.sliceValueListName))
            {
                return;
            }

            ValueList objValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Add(this.sliceValueListName);

            // loads corresponding Table in the Combo
            // ValueList is Just like a datasource in the combo, 
            // which holds DisplayMemeber and DisplayValue

            //this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListObjectSliceTypes();
            this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListSliceTypes(objectId);

            if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
            {
                //string filterCondition = "ObjectTypeID = " + objectId;
                // DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select(filterCondition);
                DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select();

                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int i = 0; i < objectSlices.Length; i++)
                {

                    objValueList.ValueListItems.Add(Convert.ToInt16(objectSlices[i].ItemArray[0].ToString()), objectSlices[i].ItemArray[2].ToString());
                    if (objectSlices[i].ItemArray[5].ToString() == "False" && objectSlices[i].ItemArray[2].ToString()=="Income Approach")
                    {
                        this.IncomeApproachSliceTypeID = Convert.ToInt32(objectSlices[i].ItemArray[0]);
                    }
                }
            }
        }

        #endregion

        #region InitializeObjectValueLists

        /// <summary>
        /// Initializes the object value lists.
        /// </summary>
        private void InitializeObjectValueLists()
        {
            this.objectValueListName = System.Guid.NewGuid().ToString();

            if (this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Exists(this.objectValueListName))
            {
                return;
            }

            ValueList objValueList = this.AppraisalSummaryGrid.DisplayLayout.ValueLists.Add(this.objectValueListName);
            this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListObjectSliceTypes(this.parcelId);

            if (this.listObjectSliceData.ListObjectTypes.Rows.Count > 0)
            {
                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int i = 0; i < this.listObjectSliceData.ListObjectTypes.Rows.Count; i++)
                {
                    objValueList.ValueListItems.Add(Convert.ToInt16(this.listObjectSliceData.ListObjectTypes.Rows[i].ItemArray[0].ToString()), this.listObjectSliceData.ListObjectTypes.Rows[i].ItemArray[1].ToString());
                }
            }
        }

        #endregion

        #endregion ValueLists

        #region ShowValueSlices

        /// <summary>
        /// Shows the value slices.
        /// </summary>
        /// <param name="primaryKeyId">The primary key id.</param>
        private void ShowValueSlices(int primaryKeyId)
        {
            FormInfo sliceForm = new FormInfo();
            int tempRowIndex=0;
            int formNo;

            // Gets the Row Index with the corresponding primaryKeyId
            DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + primaryKeyId);
            if (rows.Length > 0)
            {
                tempRowIndex = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);
            }

            if (tempRowIndex >= 0)
            {
                int.TryParse(this.appraisalSummaryData.GetSliceSummaryTable.Rows[tempRowIndex][this.appraisalSummaryData.GetSliceSummaryTable.FormColumn.ColumnName].ToString(), out formNo);
                if (formNo > 0)
                {
                    sliceForm = TerraScanCommon.GetFormInfo(formNo);
                    sliceForm.optionalParameters = new object[1];
                    sliceForm.optionalParameters[0] = primaryKeyId;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(sliceForm));
                }
            }
        }

        #endregion

        #region TypeIdSymbols

        /// <summary>
        /// Draws the type id symbols.
        /// </summary>
        /// <param name="row">The row.</param>
        private void DrawTypeIdSymbols(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row.Band.Index == 1)
            {
                if (string.Equals(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value.ToString(), "5"))
                {
                    if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Value.ToString().Contains("("))
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[1];
                        if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Removed Land Value"))
                        {
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        }
                        if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Income Approach") &&
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.ColumnName].Value.ToString().Equals(Convert.ToString(iSliceId)))
                        {
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        }
                    }
                    else
                    {
                        row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[0];
                        if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Removed Land Value"))
                        {
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                        }
                        if (row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString().Equals("Income Approach") &&
                       row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeIDColumn.ColumnName].Value.ToString().Equals(Convert.ToString(iSliceId)))
                        {
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AdjustmentValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                            row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.AmountColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(77, 77, 77);
                        }
                    }
                }
                else if (string.Equals(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value.ToString(), "6"))
                {
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[5];
                }
                else if (string.Equals(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value.ToString(), "3"))
                {
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[3];
                }
                else if (string.Equals(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value.ToString(), "4"))
                {
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[4];
                }
                else if (string.Equals(row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.TypeColumn.ColumnName].Value.ToString(), "2"))
                {
                    row.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ImageTypeColumn.ColumnName].Appearance.ImageBackground = this.AdjustmentTypesImage.Images[2];
                }

             
            }         
        }

        #endregion

        private void DescriptionEditor_Click(object sender, EventArgs e)
        {

        }

        #endregion Common Methods


        #region protected Methods
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
        #endregion protectedMethods

        private void AppraisalSummaryGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (this.activeRow.Band.Index == 0)
                {
                    if (this.activeCell.Column.Index == 4)
                    {
                        if (!this.activeRow.IsAddRow)
                        {
                            this.SetEditRecord();
                        }
                    }
                }
                if (this.activeRow.Band.Index == 1)
                {

                    if (this.activeCell.Column.Index == 7)
                    {
                        if (!string.IsNullOrEmpty(this.activeCell.Value.ToString()))
                        {
                            if (this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString() != ("Types"))
                            {
                                if (!this.activeRow.IsAddRow)
                                {
                                    this.SetEditRecord();
                                }
                            }
                        }

                    }

                }

                //this.SetEditRecord();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
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

        private string GetParcelXML()
        {
            string parcelXML = string.Empty;
            string[] objectColumnNames = { this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName, this.appraisalSummaryData.GetObjectSummaryTable .DescriptionColumn.ColumnName
                                   ,this.appraisalSummaryData.GetObjectSummaryTable.ValueBitColumn.ColumnName, this.appraisalSummaryData.GetObjectSummaryTable.RollBitColumn.ColumnName};
            DataTable parcelTable = new DataTable("Table");
            parcelTable = this.appraisalSummaryData.GetObjectSummaryTable.DefaultView.ToTable(parcelTable.TableName, false, objectColumnNames);
            if (parcelTable.Rows.Count > 0)
            {
                parcelTable.Columns[0].ColumnName = "KeyID";
                parcelTable.Columns[1].ColumnName = "Description";
                parcelTable.Columns[2].ColumnName = "IsValue";
                parcelTable.Columns[3].ColumnName = "IsRoll";

                DataColumn isObject = new DataColumn("IsObject");
                isObject.DataType = typeof(System.Boolean);
                isObject.DefaultValue = true;
                parcelTable.Columns.Add(isObject);

            }

            string[] sliceColumnNames = { this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName, this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName
                                   ,this.appraisalSummaryData.GetSliceSummaryTable.ValueBitColumn.ColumnName, this.appraisalSummaryData.GetSliceSummaryTable.RollBitColumn.ColumnName};
            DataTable sliceTable = new DataTable("Table");
            sliceTable = this.appraisalSummaryData.GetSliceSummaryTable.DefaultView.ToTable(sliceTable.TableName, false, sliceColumnNames);
            if (sliceTable.Rows.Count > 0)
            {
                sliceTable.Columns[0].ColumnName = "KeyID";
                sliceTable.Columns[1].ColumnName = "Description";
                sliceTable.Columns[2].ColumnName = "IsValue";
                sliceTable.Columns[3].ColumnName = "IsRoll";

                DataColumn isObject = new DataColumn("IsObject");
                isObject.DataType = typeof(System.Boolean);
                isObject.DefaultValue = false;
                sliceTable.Columns.Add(isObject);

                parcelTable.Merge(sliceTable);
            }

            parcelXML = TerraScanCommon.GetXmlString(parcelTable);
            string xmlNameSpace = string.Concat(" xmlns=", '"', "http://tempuri.org/F35000AppraisalSummaryData.xsd", '"');
            parcelXML = parcelXML.Replace(xmlNameSpace, "");
            return parcelXML;
        }

        private void AppraisalSummaryGrid_ClickCell(object sender, ClickCellEventArgs e)
        {
            int SliceTypeID = 0;
            try
            {
                if (!this.cellActivate)
                {
                    if ((this.AppraisalSummaryGrid.ActiveRow != null) && (this.AppraisalSummaryGrid.ActiveRow.Band.Index == 0 && (e.Cell.Column.Index == 7 || e.Cell.Column.Index == 9)) && !e.Cell.Row.Activated)
                    {
                        e.Cell.Row.Activate();
                    }

                    this.activeRow = this.AppraisalSummaryGrid.ActiveRow;

                    if (this.activeRow != null && this.activeRow.Band.Index == 1)
                    {
                        if (!e.Cell.Row.Activated && (e.Cell.Column.Key == "RollString" || e.Cell.Column.Key == "ValueString")) //((e.Cell.Column.Index == 7 || e.Cell.Column.Index == 9 || e.Cell.Column.Index == 11) && !e.Cell.Row.Activated)
                        {
                            e.Cell.Row.Activate();
                            this.activeRow = this.AppraisalSummaryGrid.ActiveRow;
                        }
                    }
                    else
                    {
                        if (e.Cell.Column.Key == "RollString")// || e.Cell.Column.Key == "ValueString")) //((e.Cell.Column.Index == 7 || e.Cell.Column.Index == 9 || e.Cell.Column.Index == 11) && !e.Cell.Row.Activated)
                        {
                            e.Cell.Row.Activate();
                            this.activeRow = this.AppraisalSummaryGrid.ActiveRow;
                        }
                    }

                  
                    this.activeCell = this.AppraisalSummaryGrid.ActiveCell;
                    if (this.activeRow != null)
                    {
                        if (this.activeRow.Band.Index == 1)
                        {
                          
                            //if (( e.Cell.Column.Index==9 || e.Cell.Column.Index==11) && !e.Cell.Row.Activated)
                            //{
                            //    e.Cell.Row.Activate();
                            //}
                            if (e.Cell.Column.Key == "ValueString" && e.Cell.Column.Index == 7)// (e.Cell.Column.Index == 7 && e.Cell.Column.CellClickAction == CellClickAction.Default)
                            {
                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Removed Land Value"))
                                        {
                                        }
                                        else if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Income Approach"))
                                        {
                                            int objectId = Convert.ToInt32(this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ObjectID"]);
                                            this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListSliceTypes(objectId);
                                            if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
                                            {
                                                DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select();
                                                for (int i = 0; i < objectSlices.Length; i++)
                                                {
                                                    if (objectSlices[i].ItemArray[5].ToString() == "True" && objectSlices[i].ItemArray[2].ToString() == "Income Approach")
                                                    {
                                                        SliceTypeID = Convert.ToInt32(objectSlices[i].ItemArray[0]);
                                                    }
                                                }
                                            }
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceTypeID"].ToString().Equals(Convert.ToString(SliceTypeID)))
                                            {

                                            }
                                            else
                                            {
                                                if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Disabled;
                                                }
                                                else
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Enabled;
                                                }
                                                this.SetEditRecord();
                                            }
                                        }
                                        else
                                        {
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Disabled;
                                            }
                                            else
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Enabled;
                                            }
                                            this.SetEditRecord();
                                        }
                                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                                       
                                    }
                                }

                                this.isRollValueChanged = false;
                            }

                            if (e.Cell.Column.Index == 9 && (e.Cell.Column.Key == "ValueString"))//&& e.Cell.Column.CellClickAction == CellClickAction.Default)
                            {
                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Removed Land Value"))
                                        {
                                        }
                                        else if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Income Approach"))
                                        {
                                            int objectId = Convert.ToInt32(this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ObjectID"]);
                                            this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListSliceTypes(objectId);
                                            if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
                                            {
                                                DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select();
                                                for (int i = 0; i < objectSlices.Length; i++)
                                                {
                                                    if (objectSlices[i].ItemArray[5].ToString() == "True" && objectSlices[i].ItemArray[2].ToString() == "Income Approach")
                                                    {
                                                        SliceTypeID = Convert.ToInt32(objectSlices[i].ItemArray[0]);
                                                    }
                                                }
                                            }
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceTypeID"].ToString().Equals(Convert.ToString(SliceTypeID)))
                                            {

                                            }
                                            else
                                            {
                                                if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Disabled;
                                                }
                                                else
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Enabled;
                                                }
                                                this.SetEditRecord();
                                            }
                                        }
                                        else
                                        {
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_DisActive;
                                            }
                                            else
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Active;
                                            }
                                            this.SetEditRecord();
                                        }
                                        //this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                                    }
                                }

                                this.isRollValueChanged = false;
                            }

                            if (e.Cell.Column.Index == 11  && e.Cell.Column.Key == "RollString")//&& e.Cell.Column.CellClickAction == CellClickAction.Default)
                            {
                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetSliceSummaryTable.Select("ValueSliceID = " + this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.ValueSliceIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetSliceSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Removed Land Value"))
                                        {
                                        }
                                        else if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceType"].ToString().Equals("Income Approach"))
                                        {
                                            int objectId = Convert.ToInt32(this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ObjectID"]);
                                            this.listObjectSliceData = this.form35000Controll.WorkItem.F35000_ListSliceTypes(objectId);
                                            if (this.listObjectSliceData.ListSliceTypes.Rows.Count > 0)
                                            {
                                                DataRow[] objectSlices = this.listObjectSliceData.ListSliceTypes.Select();
                                                for (int i = 0; i < objectSlices.Length; i++)
                                                {
                                                    if (objectSlices[i].ItemArray[5].ToString() == "True" && objectSlices[i].ItemArray[2].ToString() == "Income Approach")
                                                    {
                                                        SliceTypeID = Convert.ToInt32(objectSlices[i].ItemArray[0]);
                                                    }
                                                }
                                            }
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["SliceTypeID"].ToString().Equals(Convert.ToString(SliceTypeID)))
                                            {

                                            }
                                            else
                                            {
                                                if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Disabled;
                                                }
                                                else
                                                {
                                                    this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                                    e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Enabled;
                                                }
                                                this.SetEditRecord();
                                            }
                                        }
                                        else
                                        {
                                            if (this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"].ToString().Equals("True"))
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"] = false;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_DisActive;
                                            }
                                            else
                                            {
                                                this.appraisalSummaryData.GetSliceSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                                                e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Active;
                                            }
                                            this.SetEditRecord();
                                        }
                                        // this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                                    }
                                }

                                this.isRollValueChanged = false;
                            }

                        }
                        if (this.activeRow.Band.Index == 0)
                        {

                            if (e.Cell.Column.Key == "ValueString" && e.Cell.Band.Index == 0)//((e.Cell.Column.Index == 7 && e.Cell.Band.Index == 0 ))
                            {

                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetObjectSummaryTable.Select("ObjectID = " + this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetObjectSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"].ToString().Equals("True"))
                                        {
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = false;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Disabled;
                                        }
                                        else
                                        {
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["ValueBit"] = true;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Value_Obj_Active_Enabled;
                                        }

                                        //this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ValueStringColumn.ColumnName].Appearance.BorderColor = Color.Red;
                                        this.SetEditRecord();
                                    }
                                }

                                this.isRollValueChanged = false;

                            }
                         
                            if (e.Cell.Column.Key == "RollString" && e.Cell.Column.Index == 9) // (e.Cell.Column.Index == 9 && e.Cell.Column.CellClickAction == CellClickAction.Default)
                            {
                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetObjectSummaryTable.Select("ObjectID = " + this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetObjectSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"].ToString().Equals("True"))
                                        {
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = false;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Obj_DisActive;
                                        }
                                        else
                                        {
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Obj__ActiveEnabled;
                                        }

                                        this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.Red;
                                        this.SetEditRecord();
                                    }
                                }

                                this.isRollValueChanged = false;
                            }
                            if (e.Cell.Column.Key == "RollString" && e.Cell.Column.Index == 11 && (e.Cell.Band.Index == 0 || (e.Cell.Column.Band.Index == 1 && e.Cell.Band.Index == 1)))//(e.Cell.Column.Index == 11 && e.Cell.Column.CellClickAction == CellClickAction.Default && e.Cell.Band.Index != 0 && e.Cell.Column.Band.Index != 0)
                            {
                                this.isRollValueChanged = true;
                                if (!string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value.ToString()))
                                {
                                    DataRow[] rows = this.appraisalSummaryData.GetObjectSummaryTable.Select("ObjectID = " + this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.ObjectIDColumn.ColumnName].Value);
                                    if (rows.Length >= 1)
                                    {
                                        this.isCurrentRow = this.appraisalSummaryData.GetObjectSummaryTable.Rows.IndexOf(rows[0]);

                                        if (this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"].ToString().Equals("True"))
                                        {
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = false;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Obj_DisActive;
                                        }
                                        else
                                        {
                                            //if (e.Cell.Appearance.ImageBackground != Properties.Resource2.Roll_Active)
                                            //{
                                            //    this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                                            //    e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Obj_DisActive;
                                            //}
                                            //else
                                            //{
                                            this.appraisalSummaryData.GetObjectSummaryTable.Rows[this.isCurrentRow]["RollBit"] = true;
                                            e.Cell.Appearance.ImageBackground = Properties.Resource2.Roll_Active;
                                            // }

                                        }

                                        // this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.RollStringColumn.ColumnName].Appearance.BorderColor = Color.Red; 
                                        this.SetEditRecord();
                                    }
                                }

                                this.isRollValueChanged = false;
                            }
                        }
                    }

                }
            }
            
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                SliceTypeID = 0;
            }
        }

        private void AppraisalSummaryGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.activeRow.Band.Index == 1)
                {
                    if (string.IsNullOrEmpty(this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.SliceTypeColumn.ColumnName].Value.ToString()))
                    {
                        this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Value = string.Empty;
                    }
                    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.White;
                    this.activeRow.Cells[this.appraisalSummaryData.GetSliceSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.Blue;
                }
                if (this.activeRow.Band.Index == 0)
                {
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(31, 65, 103);
                    this.activeRow.Cells[this.appraisalSummaryData.GetObjectSummaryTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.White;

                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
