//--------------------------------------------------------------------------------------------
// <copyright file="F35050.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F35050.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31 Jan 2008      Ramya.D             Created
// 30 Jan 2009      Sadha Shivudu       Implenemted #4692 -TSCO Delete button functionality
// 06  Feb 2009     Sadha Shivudu       StyleCop issues fixed
// 25  Feb 2009     Karthikeyan V       TFS Issues fixed
// 04  Jul 2009     Biju I.G.           To implement #1209
//*********************************************************************************/
namespace D20050
{
    using System;
    using System.Collections;
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
    using System.Text.RegularExpressions;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win;
    using Infrastructure.Interface;

    /// <summary>
    /// F35100 class file
    /// </summary>
    [SmartPart]
    public partial class F35050 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

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

        /// <summary>
        /// iteamCategoryValue
        /// </summary>
        private int iteamCategoryValue;

        /// <summary>
        /// tableValues
        /// </summary>
        private string tableValues;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// basePanelScrolled variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// editValuecolor
        /// </summary>
        private bool editValuecolor;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Instance of form35050Controller to call the WorkItem
        /// </summary>
        private F35050Controller form35050Controller;

        /// <summary>
        /// scheduleItemDataSetObject
        /// </summary>
        private F35050ScheduleLineItemDataSet scheduleItemDataSet = new F35050ScheduleLineItemDataSet();

        /// <summary>
        /// listTableDetaist
        /// </summary>
        private F35050ScheduleLineItemDataSet listTableDetails = new F35050ScheduleLineItemDataSet();

        /// <summary>
        /// listTableDetaist
        /// </summary>
        private F35050ScheduleLineItemDataSet tablescheduleLineItemDataSet = new F35050ScheduleLineItemDataSet();

        /// <summary>
        /// An object for selectedPipeItemMenuStrip
        /// </summary>
        private ContextMenuStrip selectedPipeItemMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// scheduleId
        /// </summary>
        private int scheduleId;

        /// <summary>
        /// categoryValueList
        /// </summary>
        private string categoryValueList;

        /// <summary>
        /// descriptionValueList
        /// </summary>
        private string descriptionValueList;

        /// <summary>
        /// tableitemList
        /// </summary>
        private string tableitemList;

        /// <summary>
        /// initializeNewRow
        /// </summary>
        private bool initializeNewRow;

        /// <summary>
        /// percentageChanged
        /// </summary>
        private bool percentageChanged;

        /// <summary>
        /// valueChanged
        /// </summary>
        private bool valueChanged;

        /// <summary>
        /// rowColor
        /// </summary>
        private Color rowColor = new Color();

        /// <summary>
        /// scheduleId
        /// </summary>
        private int rowCount;

        /// <summary>
        /// scheduleId
        /// </summary>
        private int roughDeprTableId = 0;

        /// <summary>
        /// scheduleId
        /// </summary>
        private int varydeprId = 0;

        /// <summary>
        /// scheduleItemDataSetObject
        /// </summary>
        private F35050ScheduleLineItemDataSet deprTable = new F35050ScheduleLineItemDataSet();

        /// <summary>
        /// delKey
        /// </summary>
        private bool delKey;

        /// <summary>
        /// scheduleItemDataSetObject
        /// </summary>
        private F35050ScheduleLineItemDataSet accountTable = new F35050ScheduleLineItemDataSet();

        /// <summary>
        /// chkBools
        /// </summary>
        private bool chkBools;

        /// <summary>
        /// yaxisPoint
        /// </summary>
        private int yaxisPoint;

        /// <summary>
        /// multiRecords
        /// </summary>
        private bool checkOne;

        /// <summary>
        /// To store the Exemptionautocomplete On/Off value
        /// </summary>
        private bool exemptionAutoCompleteOnOff;

        /// <summary>
        /// To get the Roll year from General Configuration Value
        /// </summary>
        private CommentsData getRollYearConfigurationValue = new CommentsData();

        /// <summary>
        /// deletesmartpartheight
        /// </summary>
        private bool deleteSmartPartHeight;

        /// <summary>
        /// used to validate during close
        /// </summary>
        private bool validationFailed;

        /// <summary>
        /// used to identify the depr table is loaded or not
        /// </summary>
        private bool checkDeprTableLoaded;

        /// <summary>
        /// used to identify the depr table value changed or not
        /// </summary>
        private bool isDeprValueChanged;
        #endregion Variable

        #region Constructor

        /// <summary>
        /// F49910
        /// </summary>
        public F35050()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F49910"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35050(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.validationFailed = false;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.scheduleId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.blueColor = blue;
            this.greenColor = green;
            this.formMasterPermissionEdit = permissionEdit;
            this.ScheduleLinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ScheduleLinePictureBox.Height, this.ScheduleLinePictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Eventpublication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

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
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F49910Control.
        /// </summary>
        /// <value>The F49910Control.</value>
        [CreateNew]
        public F35050Controller F35050Control
        {
            get { return this.form35050Controller as F35050Controller; }
            set { this.form35050Controller = value; }
        }

        #endregion Properties

        #region EventSubscription

        #region GetSlicePermission

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

                if (this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count > 0)
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

        #endregion GetSlicePermission

        #region SaveSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.newPermission)
                {
                    if (this.validateGirdValues(null))
                    {
                        this.ValidateSliceForm(eventArgs);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (this.slicePermissionField.editPermission)
                {
                    this.ValidateSliceForm(eventArgs);
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
            }
        }

        #endregion SaveSliceInformation

        #region SaveConfirmed

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.form35050Controller.WorkItem.SaveScheduleLineItem(this.scheduleId, TerraScanCommon.GetXmlString(this.scheduleItemDataSet.SchedlueLineItemTable), TerraScanCommon.UserId);
            }
        }

        #endregion SaveConfirmed

        #region CancelSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.FormLoad();
        }

        #endregion CancelSliceInformation

        #region LoadSliceDetails

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
                this.scheduleId = eventArgs.Data.SelectedKeyId;
                this.FormLoad();
            }
        }

        #endregion LoadSliceDetails

        #region DeleteSliceInformation

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.PermissionFiled.deletePermission && this.scheduleId > 0)
            {
                this.form35050Controller.WorkItem.F35050_DeleteSchedule(this.scheduleId, TerraScanCommon.UserId);
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
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                if (this.validateGirdValues(null))
                {
                    eventArgs.Data.FlagFormClose = true;
                }
                else
                {
                    eventArgs.Data.FlagFormClose = false;
                }
            }
        }

        #endregion DeleteSliceInformation

        #endregion EventSubscription

        #region OnFormSlice_Resize

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            try
            {
                if (this.FormSlice_Resize != null)
                {
                    this.FormSlice_Resize(this, eventArgs);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion OnFormSlice_Resize

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F35050 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35050_Load(object sender, EventArgs e)
        {
            try
            {
              
                this.FlagSliceForm = true;
                this.FormLoad();

                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(this.F35050_Scroll);
                ////((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);                     

                //// Assign the Occupancy menus
                this.selectedPipeItemMenuStrip.Items.Add("Delete Line Item");
                this.selectedPipeItemMenuStrip.Items.Add("Exit");
                this.selectedPipeItemMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedPipeItemMenuStrip_ItemClicked);
                this.selectedPipeItemMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedPipeItemMenuStrip_Closed);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ScheduleLinePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLinePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PipeLineToolTip.SetToolTip(this.ScheduleLinePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Scroll Events

        /// <summary>
        /// Handles the Scroll event of the F35050 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void F35050_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > 5)
            {
                this.yaxisPoint = e.NewValue;
            }
            else
            {
                this.yaxisPoint = 0;
            }
        }

        #endregion Scroll Events

        #region ScheduleLineGrid Events

        /// <summary>
        /// Handles the InitializeLayout event of the PipeLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void PipeLineGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row != null)
                {
                    if (this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count == 0)
                    {
                        this.CellEditStatus(e.Row, true);
                    }
                    else
                    {
                        if (e.Row.Index == this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count)
                        {
                            //// Enable the cell edit
                            this.CellEditStatus(e.Row, true);
                        }
                        else
                        {
                            //// Disable the cell edit 
                            this.LoadCategoryItem();
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.categoryValueList];
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                            this.LoadDescriptionItem();
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.descriptionValueList];
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                            //Commented by Biju to implement #1209
                            //this.LoadTableItem();
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.tableitemList];
                            //Commented by Biju to implement #1209
                            //e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                            //Added by Biju to implement #1209
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                            //till here
                            e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                        }
                    }

                    if (e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Value != null)
                    {
                        if (!string.IsNullOrEmpty(e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Value.ToString()))
                        {
                            if ((bool)e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Value)
                            {
                                e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }

                    if (e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value != null)
                    {
                        if (!string.IsNullOrEmpty(e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value.ToString()))
                        {
                            if ((bool)e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value)
                            {
                                e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                e.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }
                }
                else
                {
                    if (e.Row.Index == 0)
                    {
                        this.CellEditStatus(e.Row, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cells the edit status.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void CellEditStatus(Infragistics.Win.UltraWinGrid.UltraGridRow row, bool value)
        {
            try
            {
                if (value)
                {
                    //// Making column readonly false
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;

                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit;
                }
                else
                {
                    //// Making column readonly false
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                    row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            try
            {

                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;

                this.checkOne = false;
                this.LoadCategoryItem();

                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.categoryValueList];
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                this.LoadDescriptionItem();
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.descriptionValueList];
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                ////Added by Biju to implement #1209
                if (!this.checkDeprTableLoaded)
                {
                    this.LoadTableItem();
                }
                //till here
                //Commented by Biju to implement #1209
                //this.LoadTableItem();
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.tableitemList];
                //Commented by Biju to implement #1209
                //e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                //Added by Biju to implement #1209
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                //till here
                e.TemplateAddRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                if (activeCell != null)
                {
                    if (activeRow != null)
                    {
                        if (activeCell != null)
                        {
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                            {
                                if (activeCell.Column.Index != 4)
                                {
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value = "1";
                                }

                                this.chkBools = true;

                                if (this.ScheduleLineGrid.ActiveCell != null)
                                {
                                    if (activeCell.Text != null)
                                    {
                                        if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                                        {
                                            this.EditEnabled();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    this.SetSmartPartHeight(20);

                    if (activeRow != null)
                    {
                        if (activeCell != null)
                        {
                            activeCell.Activate();
                            this.ScheduleLineGrid.PerformAction(UltraGridAction.EnterEditMode);
                            try
                            {
                                if (activeCell.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && activeCell.IsInEditMode == true && activeCell.SelText.Length > 0)
                                {
                                    activeCell.SelStart = activeCell.SelText.Length;
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint + 25);
                    }
                    this.ScheduleLineGrid.PerformAction(UltraGridAction.EnterEditMode);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;
            //added by Biju to implement #1209
            if (activeCell.Column.Index == 7 & this.isDeprValueChanged)
            {
                int scheduleItemId;
                int rollYear;
                int year;
                int deprID;
                string tableVals;
                if (this.ScheduleLineGrid.DisplayLayout.ValueLists.Exists(this.tableitemList))
                {
                    if (this.ScheduleLineGrid.DisplayLayout.ValueLists[this.tableitemList].SelectedItem != null)
                    {
                        this.isDeprValueChanged = false;
                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Text.ToString().Trim(), out scheduleItemId);
                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Text.ToString().Trim(), out rollYear);
                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Text.ToString().Trim(), out year);
                        //this.tablescheduleLineItemDataSet = this.form35050Controller.WorkItem.F35050_GetListOutTableDetails(this.scheduleId);
                        tableVals = this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[7].Text.ToString().Trim();
                        this.ScheduleLineGrid.UpdateData();


                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);


                        this.accountTable = this.form35050Controller.WorkItem.F35050_CalculateAmount(Convert.ToInt32(scheduleItemId), Convert.ToInt32(rollYear), Convert.ToInt32(year), deprID);
                        //decimal.TryParse(this.accountTable.AccountDetails.Rows[0][this.accountTable.AccountDetails.AmountColumn].ToString(), out totalValues);

                        this.CalculateDeprPercentage(activeCell.Row);
                        int calQnty;
                        Double calCost;
                        int calValue;
                        Decimal percentageValue;
                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[4].Text.ToString().Trim(), out calQnty);

                        string costVal = this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[6].Text.ToString().Trim();
                        Double cos1 = 0.0;
                        Double.TryParse(costVal.ToString().Replace(",", ""), out cos1);

                        cos1 = Math.Round(cos1);

                        if (cos1 > 0)
                        {
                            calCost = cos1;
                        }
                        else
                        {
                            calCost = 0;
                        }

                        int.TryParse(this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[9].Text.ToString().Trim(), out calValue);
                        string percentValues = this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[8].Text.ToString().Trim();
                        Decimal.TryParse(percentValues.ToString().Replace("%", ""), out percentageValue);

                        if (percentageValue >= 0 && percentageValue <= 100)
                        {
                            ////percentageValue = percentageValue.ToString("#,##0.00") + " %";
                            ///// activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprValue;
                        }
                        else
                        {
                            percentageValue = 0;
                            MessageBox.Show("Percentage should be through 0 to 100", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            activeCell.Row.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00 %";
                        }

                        Decimal percentpercentageValue;
                        percentpercentageValue = percentageValue / 100;

                        Double calTotalValue1 = 0.0;
                        Double calQnty1 = Convert.ToDouble(calQnty.ToString());
                        Double calCost1 = Convert.ToDouble(calCost.ToString());
                        Double percentpercentageValue1 = Convert.ToDouble(percentpercentageValue.ToString());

                        calTotalValue1 = (calQnty1 * calCost1 * (1 - percentpercentageValue1));
                        calTotalValue1 = Math.Round(calTotalValue1);
                        this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = calTotalValue1.ToString("#,##0");
                    }
                    else
                    {
                        this.ScheduleLineGrid.Rows[activeCell.Row.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = null;
                    }
                    
                }

                
            }
            //till here
            if (this.chkBools == false)
            {
                if (this.valueChanged)
                {
                    this.validateGirdValues(e);
                    this.valueChanged = false;
                }
            }
            else
            {
                this.chkBools = false;
            }
        }

        /// <summary>
        /// Validates the gird values.
        /// </summary>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        /// <returns>bool</returns>
        private bool validateGirdValues(Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                ////if (!this.chkBools)
                ////{
                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.basePanelScrolled = false;
                }
                else
                {
                    this.basePanelScrolled = false;
                }

                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;
                int tempValue = 0;
                Decimal tempValues;
                string iteamCategoryText = string.Empty;

                if (activeCell != null)
                {
                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Value = this.scheduleId;

                    if ((activeCell.Column.Index == 6) || (activeCell.Column.Index == 8))
                    {
                        int calQnty = 0;
                        Double calCost = 0.0;
                        int calValue = 0;
                        string deprValue = string.Empty;
                        Double percentageValuesss = 0.0;

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[4].Text.ToString().Trim(), out calQnty);
                        string costVal = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[6].Text.ToString().Trim();

                        Double cos1 = 0.0;
                        Double.TryParse(costVal.ToString().Replace(",", ""), out cos1);
                        cos1 = Math.Round(cos1);

                        if (cos1 > 0 && cos1 <= 922337203685477)
                        {
                            calCost = cos1;
                            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value = calCost.ToString();
                        }
                        else
                        {
                            calCost = 0;
                            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value = string.Empty;
                        }

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[9].Text.ToString().Trim(), out calValue);
                        string percentValues = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Text.ToString().Trim();

                        Double.TryParse(percentValues.ToString().Replace("%", ""), out percentageValuesss);
                        deprValue = percentageValuesss.ToString("#,##0.00") + " %";

                        Double.TryParse(deprValue.ToString().Replace("%", ""), out percentageValuesss);
                        percentageValuesss = percentageValuesss / 100;

                        Double calTotalValue3 = 0.0;
                        Double calQnty3 = Convert.ToDouble(calQnty.ToString());
                        Double calCost3 = Convert.ToDouble(calCost.ToString());
                        Double percentpercentageValue3 = Convert.ToDouble(percentageValuesss.ToString());

                        calTotalValue3 = (calQnty3 * calCost3 * (1 - percentpercentageValue3));
                        calTotalValue3 = Math.Round(calTotalValue3);

                        if (this.editValuecolor)
                        {
                            if (calTotalValue3 == 0.0)
                            {
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = 0.0;
                            }
                            else
                            {
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = calTotalValue3.ToString("#,##0");
                            }
                        }

                        int calQnty33 = Convert.ToInt32(calQnty3);
                        int percentpercentageValue33 = Convert.ToInt32(percentpercentageValue3);

                        if (calQnty33 == 0 && calCost3 == 0.0 && percentpercentageValue33 == 0)
                        {
                            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                        }
                    }

                    if (activeCell.Column.Index == 3)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }

                            this.initializeNewRow = false;
                        }
                        else
                        {
                            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Value = activeCell.Text.Trim();
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                            {
                                DataRow[] code;
                                code = this.scheduleItemDataSet.SchedlueItemTable.Select("Description = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");

                                if (code.Length > 0)
                                {
                                    int.TryParse(code[0].ItemArray[2].ToString(), out tempValue);

                                    if (tempValue > 0)
                                    {
                                        //// activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Value = tempValue.ToString();
                                    }
                                    else
                                    {
                                        ////activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Value = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                    else if (activeCell.Column.Index == 4)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }

                            this.initializeNewRow = false;
                            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value = string.Empty;
                        }
                        else
                        {
                            int.TryParse(activeCell.Text.Trim().ToString(), out tempValue);

                            if (tempValue > 0 && tempValue <= 2147483647)
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value = tempValue.ToString();
                            }
                            else
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value = string.Empty;
                            }

                            this.CalculateTotal();
                            this.CalculateValue(activeRow);
                        }
                    }
                    else if (activeCell.Column.Index == 9)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }

                            this.initializeNewRow = false;
                        }
                        else
                        {
                            Decimal.TryParse(activeCell.Text.Trim().ToString().Replace(",", ""), out tempValues);

                            if (tempValues >= 0)
                            {
                                tempValues = Math.Round(tempValues);
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = tempValues.ToString("#,##0");
                            }
                            else
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                            }

                            string qnty = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value.ToString();
                            string cost = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value.ToString();
                            string deprPercent = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value.ToString();

                            Double cos1 = 0.0;
                            Double.TryParse(cost.ToString().Replace(",", ""), out cos1);

                            int qnty1;
                            int cost1;
                            int deprPercent1;
                            int.TryParse(qnty, out qnty1);
                            int.TryParse(cost, out cost1);
                            int.TryParse(deprPercent, out deprPercent1);
                            if (qnty1 == 0 && cos1 == 0.0 && deprPercent1 == 0)
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                            }

                            this.CalculateTotal();
                        }
                    }
                    else if (activeCell.Column.Index == 10)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            this.CalculateTotal();
                        }
                    }
                    else if (activeCell.Column.Index == 5)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim()))
                            {
                                int.TryParse(activeCell.Text.Trim().ToString(), out tempValue);

                                if (tempValue > 1899 && tempValue < 2080)
                                {
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value = tempValue.ToString();
                                }
                                else
                                {

                                    MessageBox.Show("Year should be greater than 1899 and lesser than 2080", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value = string.Empty;
                                    if (e != null)
                                    {
                                        e.Cancel = true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }

                                this.CalculateTotal();
                            }
                        }
                    }
                    else if (activeCell.Column.Index == 7)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                            {
                                DataRow[] deprDes;
                                deprDes = this.tablescheduleLineItemDataSet.pclstDeprTable.Select("DeprName = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");

                                if (deprDes.Length == 0)
                                {
                                    MessageBox.Show("Select the TableItem from list.", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (e != null)
                                    {
                                        e.Cancel = true;
                                    }
                                }
                                else
                                {
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = activeCell.Text.Trim().ToString();
                                }
                            }

                            this.CalculateTotal();
                        }
                    }
                    else if (activeCell.Column.Index == 6)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            Double cos1 = 0.0;
                            int calCost;
                            Double.TryParse(activeCell.Text.Trim().ToString().Replace(",", ""), out cos1);

                            cos1 = Math.Round(cos1);

                            if (cos1 > 0)
                            {
                                cos1 = cos1;
                            }
                            else
                            {
                                cos1 = 0;
                            }

                            if (cos1 > 0)
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value = cos1.ToString("#,##0");
                            }
                            else
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value = string.Empty;
                            }

                            this.CalculateTotal();
                            this.CalculateValue(activeRow);
                        }
                    }
                    else if (activeCell.Column.Index == 8)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim()))
                            {
                                double value = 0.0;
                                string deprValue = string.Empty;
                                double.TryParse(activeCell.Text.Trim().ToString().Replace("%", ""), out value);

                                if (value >= 0.0 && value <= 100.0)
                                {
                                    deprValue = value.ToString("#,##0.00") + " %";
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprValue;
                                }
                                else
                                {
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = string.Empty;
                                    MessageBox.Show("Percentage should be through 0 to 100", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    if (e != null)
                                    {
                                        e.Cancel = true;
                                    }
                                }

                                this.CalculateTotal();
                                this.CalculateValue(activeRow);
                            }
                        }
                    }
                    else if (activeCell.Column.Index == 11)
                    {
                        if (this.initializeNewRow)
                        {
                            if (e != null)
                            {
                                e.Cancel = true;
                            }
                            this.initializeNewRow = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(activeCell.Text.Trim()))
                            {
                                DataRow[] category;
                                category = this.scheduleItemDataSet.SchedlueCategoryTable.Select("ItemCategory = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");

                                if (category.Length == 0)
                                {
                                    if (this.scheduleItemDataSet.SchedlueCategoryTable.Rows.Count > 0)
                                    {
                                        for (int count = 0; count < this.scheduleItemDataSet.SchedlueCategoryTable.Rows.Count; count++)
                                        {
                                            iteamCategoryText = this.scheduleItemDataSet.SchedlueCategoryTable.Rows[0][this.scheduleItemDataSet.SchedlueCategoryTable.ItemCategoryColumn.ColumnName].ToString();
                                        }
                                    }

                                    this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value = iteamCategoryText.ToString();
                                    if (e != null)
                                    {
                                        e.Cancel = true;
                                    }
                                }
                                else
                                {
                                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value = activeCell.Text.Trim();
                                }
                            }
                            else
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value = string.Empty;
                            }
                        }
                    }
                }
                ////}

                this.CalculateTotal();
                this.editValuecolor = false;
                this.chkBools = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            return true;
        }

        /// <summary>
        /// Handles the Error event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.ErrorEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                this.valueChanged = true;
                UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
                UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

                this.editValuecolor = false;
                this.LoadCategoryItem();

                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.EditEnabled();
                }

                int scheduleItemId;
                int rollYear = 0;
                int year = 0;
                string deprName;
                Decimal totalValues;
                int deprID = 0;

                if (activeCell != null)
                {
                    int calQnty = 0;
                    Double calCost = 0.0;
                    int calValue = 0;
                    Decimal percentageValue;
                    string tableVals;
                    int age = 0;
                    decimal deprpercent;
                    string costVal = string.Empty;

                    if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString().Trim()))
                    {
                        if (!this.checkOne)
                        {
                            if (string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Text))
                            {
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value = "1";
                            }

                            this.checkOne = false;
                        }

                        if ((activeCell.Column.Index == 6) || (activeCell.Column.Index == 8))
                        {
                            this.editValuecolor = true;
                        }

                        if (activeCell.Column.Index == 3)
                        {
                            this.getRollYearConfigurationValue.GetCommentsConfigDetails.Clear();
                            this.getRollYearConfigurationValue = this.form35050Controller.WorkItem.GetConfigDetails("PP_ItemsOnlyFromList");
                            if (this.getRollYearConfigurationValue.GetCommentsConfigDetails.Rows.Count > 0)
                            {
                                this.exemptionAutoCompleteOnOff = bool.Parse(this.getRollYearConfigurationValue.GetCommentsConfigDetails[0][this.getRollYearConfigurationValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                            }

                            if (this.exemptionAutoCompleteOnOff)
                            {
                                if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString().Trim()))
                                {
                                    if (this.scheduleItemDataSet.SchedlueItemTable.Rows.Count > 0)
                                    {
                                        DataRow[] category;
                                        category = this.scheduleItemDataSet.SchedlueItemTable.Select("Description = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");

                                        if (category.Length != 0)
                                        {
                                            this.ScheduleLineGrid.UpdateData();
                                        }
                                        else
                                        {
                                            string descriptionText = this.scheduleItemDataSet.SchedlueItemTable.Rows[0]["Description"].ToString();
                                            MessageBox.Show("Select the Description from list.", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Value = descriptionText.ToString().Trim();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                ////activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Activation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
                                ///// this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].MaxLength = 200;
                            }
                        }

                        if (activeCell.Column.Index == 4)
                        {
                            this.checkOne = true;
                        }

                        if (activeCell.Column.Index == 11)
                        {

                            if (this.scheduleItemDataSet.SchedlueCategoryTable.Rows.Count > 0)
                            {
                                DataRow[] category;
                                category = this.scheduleItemDataSet.SchedlueCategoryTable.Select("ItemCategory = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");

                                if (category.Length != 0)
                                {
                                    this.ScheduleLineGrid.UpdateData();
                                }
                                else
                                {
                                    string categoryName = this.scheduleItemDataSet.SchedlueCategoryTable.Rows[0]["ItemCategory"].ToString();
                                    MessageBox.Show("Select the category from list.", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value   = categoryName.ToString().Trim();
                                    //this.LoadCategoryItem();

                                    this.ScheduleLineGrid.ActiveCell.ValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists[this.categoryValueList];
                                    this.ScheduleLineGrid.ActiveCell.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;

                                    this.ScheduleLineGrid.ActiveCell.Band.Override.AllowUpdate = DefaultableBoolean.True;
                                }
                            }

                            if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].OriginalValue.ToString()))
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].OriginalValue.ToString(), out this.iteamCategoryValue);
                            }
                            else
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value.ToString(), out this.iteamCategoryValue);
                            }

                            this.tablescheduleLineItemDataSet.Merge(this.form35050Controller.WorkItem.F35050_GetListTableDetails(this.iteamCategoryValue));
                            this.checkDeprTableLoaded = false;

                            if (this.tablescheduleLineItemDataSet.ListTableDetails.Rows.Count > 0)
                            {
                                string deprtableIDs = Convert.ToInt32(this.tablescheduleLineItemDataSet.ListTableDetails.Rows[0][this.tablescheduleLineItemDataSet.ListTableDetails.DeprTableIdColumn.ColumnName]).ToString();
                                this.tableValues = this.tablescheduleLineItemDataSet.ListTableDetails.Rows[0][this.tablescheduleLineItemDataSet.ListTableDetails.DeprNameColumn.ColumnName].ToString();
                                int.TryParse(this.tablescheduleLineItemDataSet.ListTableDetails.Rows[0][this.tablescheduleLineItemDataSet.ListTableDetails.DeprTableIdColumn.ColumnName].ToString(), out this.roughDeprTableId);
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = this.tableValues.ToString();
                                int.TryParse(deprtableIDs, out this.varydeprId);
                            }
                            else
                            {
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = 0;
                                this.varydeprId = 0;
                            }

                            int rollYears = 0;
                            int years = 0;
                            int.TryParse(this.scheduleItemDataSet.RollYearTable.Rows[0][this.scheduleItemDataSet.RollYearTable.RollYearColumn.ColumnName].ToString(), out rollYears);
                            int.TryParse(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value.ToString(), out years);
                            age = rollYears - years;

                            // Accoring To the ChangeOrder We removed Recv Field so we check only for age.
                            //// if (age >= 0 && (!string.IsNullOrEmpty(recv)))
                            if (age >= 0)
                            {
                                this.deprTable = this.form35050Controller.WorkItem.GetDeprPercentage(rollYears, this.varydeprId, years);
                                decimal.TryParse(this.deprTable.ListDeprTable.Rows[0][this.deprTable.ListDeprTable.Depr1Column].ToString(), out deprpercent);
                                activeRow.Cells[this.deprTable.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprpercent.ToString("#,##0.00") + " %";
                            }

                            int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[4].Text.ToString().Trim(), out calQnty);
                            costVal = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[6].Text.ToString().Trim();
                            Double cos1 = 0.0;
                            Double.TryParse(costVal.ToString().Replace(",", ""), out cos1);
                            cos1 = Math.Round(cos1);

                            if (cos1 > 0)
                            {
                                calCost = cos1;
                            }
                            else
                            {
                                calCost = 0;
                            }

                            int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[9].Text.ToString().Trim(), out calValue);

                            string percentValues = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Text.ToString().Trim();

                            Decimal.TryParse(percentValues.ToString().Replace("%", ""), out percentageValue);
                            Decimal percentpercentageValue;
                            percentpercentageValue = percentageValue / 100;

                            Double calTotalValue3 = 0.0;
                            Double calQnty3 = Convert.ToDouble(calQnty.ToString());
                            Double calCost3 = Convert.ToDouble(calCost.ToString());
                            Double percentpercentageValue3 = Convert.ToDouble(percentpercentageValue.ToString());

                            calTotalValue3 = (calQnty3 * calCost3 * (1 - percentpercentageValue3));
                            calTotalValue3 = Math.Round(calTotalValue3);
                            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = calTotalValue3.ToString("#,##0");
                            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = this.varydeprId;
                            this.CalculateTotal();
                            
                        }
                    }

                    if (activeCell.Column.Index == 5)
                    {
                        int cellrollYears = 0;
                        int cellYear = 0;
                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[5].Text.ToString(), out cellYear);

                        if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[7].Text))
                        {
                            if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString()))
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString(), out deprID);

                                if (deprID == 0)
                                {
                                    deprID = this.roughDeprTableId;
                                }
                            }
                            else
                            {
                                if (deprID == 0)
                                {
                                    deprID = this.roughDeprTableId;
                                }
                                else
                                {
                                    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                                }
                            }

                            if (deprID == 0)
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Value.ToString(), out deprID);
                            }
                        }
                        else
                        {
                            deprID = 0;
                        }

                        int.TryParse(this.scheduleItemDataSet.RollYearTable.Rows[0][this.scheduleItemDataSet.RollYearTable.RollYearColumn.ColumnName].ToString(), out cellrollYears);
                        age = cellrollYears - cellYear;

                        // Accoring To the ChangeOrder We removed Recv Field so we check only for age.
                        //// if (age >= 0 && (!string.IsNullOrEmpty(recv)))
                        if (age >= 0)
                        {
                            if (cellYear > 1899 && cellYear < 2080)
                            {
                                this.deprTable = this.form35050Controller.WorkItem.GetDeprPercentage(cellrollYears, deprID, cellYear);
                                decimal.TryParse(this.deprTable.ListDeprTable.Rows[0][this.deprTable.ListDeprTable.Depr1Column].ToString(), out deprpercent);
                                activeRow.Cells[this.deprTable.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprpercent.ToString("#,##0.00") + " %";
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Appearance.ForeColor = Color.Black;
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = false;
                            }
                            else
                            {
                                activeRow.Cells[this.deprTable.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00 %";
                                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Appearance.ForeColor = Color.Black;
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = false;
                            }
                        }

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[4].Text.ToString().Trim(), out calQnty);
                        costVal = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[6].Text.ToString().Trim();
                        Double cos1 = 0.0;
                        Double.TryParse(costVal.ToString().Replace(",", ""), out cos1);
                        cos1 = Math.Round(cos1);

                        if (cos1 > 0)
                        {
                            calCost = cos1;
                        }
                        else
                        {
                            calCost = 0;
                        }

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[9].Text.ToString().Trim(), out calValue);
                        string percentValues = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Text.ToString().Trim();
                        Decimal.TryParse(percentValues.ToString().Replace("%", ""), out percentageValue);
                        Decimal percentpercentageValue;
                        percentpercentageValue = percentageValue / 100;

                        Double calTotalValue2 = 0.0;
                        Double calQnty2 = Convert.ToDouble(calQnty.ToString());
                        Double calCost2 = Convert.ToDouble(calCost.ToString());
                        Double percentpercentageValue2 = Convert.ToDouble(percentpercentageValue.ToString());

                        calTotalValue2 = (calQnty2 * calCost2 * (1 - percentpercentageValue2));
                        calTotalValue2 = Math.Round(calTotalValue2);
                        this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = calTotalValue2.ToString("#,##0");

                        this.CalculateTotal();
                    }

                    if (activeCell.Column.Index == 7)
                    {
                        //Added by Biju to implement #1209
                        this.isDeprValueChanged =true ;
                    //Commented by Biju to implement #1209
                    //    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Text.ToString().Trim(), out scheduleItemId);
                    //    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Text.ToString().Trim(), out rollYear);
                    //    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Text.ToString().Trim(), out year);
                    //    //this.tablescheduleLineItemDataSet = this.form35050Controller.WorkItem.F35050_GetListOutTableDetails(this.scheduleId);
                    //    tableVals = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[7].Text.ToString().Trim();
                    //    this.ScheduleLineGrid.UpdateData();

                    //    if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString()))
                    //    {
                    //        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString(), out deprID);
                    //    }
                    //    else
                    //    {
                    //        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                    //    }

                    //    this.accountTable = this.form35050Controller.WorkItem.F35050_CalculateAmount(Convert.ToInt32(scheduleItemId), Convert.ToInt32(rollYear), Convert.ToInt32(year), deprID);
                    //    decimal.TryParse(this.accountTable.AccountDetails.Rows[0][this.accountTable.AccountDetails.AmountColumn].ToString(), out totalValues);
                    //    this.CalculateDeprPercentage(activeRow);
                    //till here
                    }

                    if (activeCell.Column.Index == 9)
                    {
                        activeCell.Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Value = true;
                    }

                    if (activeCell.Column.Index == 8)
                    {
                        activeCell.Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                        activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = true;
                        this.percentageChanged = true;
                    }

                    if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                    {
                        this.EditEnabled();
                    }
                    //Commented by Biju to implement #1209
                    //if ((activeCell.Column.Index == 4) || (activeCell.Column.Index == 6) || (activeCell.Column.Index == 7) || (activeCell.Column.Index == 8))
                    //Added by Biju to implement #1209
                    if ((activeCell.Column.Index == 4) || (activeCell.Column.Index == 6) ||  (activeCell.Column.Index == 8))
                    {
                        this.ScheduleLineGrid.Rows[activeRow.Index].Cells[9].Appearance.ForeColor = Color.Black;
                        activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Value = false;

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Text.ToString().Trim(), out scheduleItemId);
                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Text.ToString().Trim(), out rollYear);
                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Text.ToString().Trim(), out year);
                        if (this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count > 0)
                        {
                            deprName = this.tablescheduleLineItemDataSet.pclstDeprTable.Rows[0][this.tablescheduleLineItemDataSet.pclstDeprTable.DeprNameColumn.ColumnName].ToString();
                        }

                        if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[7].Text))
                        {
                            if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString()))
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString(), out deprID);

                                if (deprID == 0)
                                {
                                    deprID = this.roughDeprTableId;
                                }
                            }
                            else
                            {
                                if (deprID == 0)
                                {
                                    deprID = this.roughDeprTableId;
                                }
                                else
                                {
                                    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                                }
                            }

                            if (deprID == 0)
                            {
                                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Value.ToString(), out deprID);
                            }
                        }
                        else
                        {
                            deprID = 0;
                        }

                        this.accountTable = this.form35050Controller.WorkItem.F35050_CalculateAmount(Convert.ToInt32(scheduleItemId), Convert.ToInt32(rollYear), Convert.ToInt32(year), deprID);
                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[4].Text.ToString().Trim(), out calQnty);

                        costVal = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[6].Text.ToString().Trim();
                        Double cos1 = 0.0;
                        Double.TryParse(costVal.ToString().Replace(",", ""), out cos1);

                        cos1 = Math.Round(cos1);

                        if (cos1 > 0)
                        {
                            calCost = cos1;
                        }
                        else
                        {
                            calCost = 0;
                        }

                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[9].Text.ToString().Trim(), out calValue);
                        string percentValues = this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Text.ToString().Trim();
                        Decimal.TryParse(percentValues.ToString().Replace("%", ""), out percentageValue);

                        if (percentageValue >= 0 && percentageValue <= 100)
                        {
                            ////percentageValue = percentageValue.ToString("#,##0.00") + " %";
                            ///// activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprValue;
                        }
                        else
                        {
                            percentageValue = 0;
                            MessageBox.Show("Percentage should be through 0 to 100", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00 %";
                        }

                        Decimal percentpercentageValue;
                        percentpercentageValue = percentageValue / 100;

                        Double calTotalValue1 = 0.0;
                        Double calQnty1 = Convert.ToDouble(calQnty.ToString());
                        Double calCost1 = Convert.ToDouble(calCost.ToString());
                        Double percentpercentageValue1 = Convert.ToDouble(percentpercentageValue.ToString());

                        calTotalValue1 = (calQnty1 * calCost1 * (1 - percentpercentageValue1));
                        calTotalValue1 = Math.Round(calTotalValue1);
                        this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = calTotalValue1.ToString("#,##0");
                    }
                }

                this.CalculateTotal();

                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    //((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeRowDeactivate event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

                if (activeRow != null)
                {
                    if (!activeRow.IsUnmodifiedTemplateAddRow)
                    {
                        if (activeRow.IsAddRow)
                        {
                            if (!string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value.ToString()) && !string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value.ToString()))
                            {
                                this.CalculateTotal();
                                this.TotalItemTextBox.Text = Convert.ToString(this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count + 1);
                                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Value = this.scheduleId;
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
        /// Handles the MouseUp event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.slicePermissionField.deletePermission)
                {
                    Infragistics.Win.UIElement elementPoint = this.ScheduleLineGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));

                    if (elementPoint != null)
                    {
                        UltraGridRow activeRow = (UltraGridRow)elementPoint.GetContext(typeof(UltraGridRow));
                        UltraGridCell activeCell = (UltraGridCell)elementPoint.GetContext(typeof(UltraGridCell));

                        if (activeRow != null && activeCell != null)
                        {
                            if (!activeRow.IsAddRow)
                            {
                                if (activeCell != null)
                                {
                                    if (activeRow.Index <= this.ScheduleLineGrid.Rows.Count && activeRow.Index != -1)
                                    {
                                        if (e.Button == MouseButtons.Right)
                                        {
                                            if (this.ScheduleLineGrid.Rows.Count > 0)
                                            {
                                                this.ScheduleLineGrid.Rows[activeRow.Index].Activate();
                                            }

                                            this.rowColor = this.ScheduleLineGrid.ActiveRow.Appearance.BackColor;
                                            this.ChangeBackColorRow(activeRow.Index);
                                            this.selectedPipeItemMenuStrip.Show(this.ScheduleLineGrid, new Point(e.X, e.Y));
                                        }
                                    }
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
        /// Handles the ItemClicked event of the SelectedPipeItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> instance containing the event data.</param>
        private void SelectedPipeItemMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.deleteSmartPartHeight = false;

                if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete Line Item"))
                {
                    this.selectedPipeItemMenuStrip.Visible = false;

                    if (this.ScheduleLineGrid.ActiveRow != null)
                    {
                        this.ScheduleLineGrid.ActiveRow.Appearance.ForeColor = Color.Black;
                    }

                    int recordcount = this.ScheduleLineGrid.Selected.Rows.Count;

                    if (recordcount > 1)
                    {
                        this.DeleteRecordss();
                    }
                    else
                    {
                        this.DeleteRecord();
                    }

                    this.CalculateTotal();
                    this.SetSmartPartHeight(0);

                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        ////this.FormLoad();
                    }
                }

                if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Exit"))
                {
                    ////if (this.ScheduleLineGrid.ActiveRow != null)
                    ////{
                    ////    this.FormLoad();
                    ////}

                    this.selectedPipeItemMenuStrip.Visible = false;
                }

                this.selectedPipeItemMenuStrip.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Closed event of the SelectedPipeItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> instance containing the event data.</param>
        private void SelectedPipeItemMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            try
            {
                if (this.ScheduleLineGrid.ActiveRow != null)
                {
                    this.RestoreBackColorRow(this.ScheduleLineGrid.ActiveRow.Index);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeCellActivate event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                   // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }

                if (activeRow.Index == 0)
                {
                    if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                    {
                        if (activeRow.Index < this.rowCount)
                        {
                            this.CellEditStatus(activeRow, false);
                        }
                        else
                        {
                            this.CellEditStatus(activeRow, true);
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                            this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                        }
                    }
                    else
                    {
                        this.CellEditStatus(activeRow, true);
                    }
                }
                else
                {
                    if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                    {
                        if (activeRow.Index < this.rowCount)
                        {
                            // this.ScheduleLineGrid.ResetDisplayLayout();
                            // activeRow.Activate();
                            this.CellEditStatus(activeRow, false);
                        }
                        else
                        {
                            this.CellEditStatus(activeRow, true);
                            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        }
                    }
                    else
                    {
                        this.CellEditStatus(activeRow, true);
                    }
                }

                if (activeRow.Index > 0)
                {
                    if (string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[3].Text.Trim()) || string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[4].Text.Trim()) || string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[5].Text.Trim()) || string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[6].Text.Trim()))
                    {
                        this.CellEditStatus(activeRow, false);
                        this.ScheduleLineGrid.Rows[activeRow.Index].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforePerformAction event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeUltraGridPerformActionEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforePerformAction(object sender, BeforeUltraGridPerformActionEventArgs e)
        {
            try
            {
                if (e.UltraGridAction == UltraGridAction.CommitRow)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ScheduleLinePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLinePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20050.F35050"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                   // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeEnterEditMode event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeEnterEditMode(object sender, CancelEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;
                this.delKey = true;
                //added by Biju to implement #1209
                if (activeCell.Column.Index.Equals(7))
                {
                    this.isDeprValueChanged = false;
                }
                //till here
                if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                {
                    if (activeRow.Index < this.rowCount)
                    {
                        this.CellEditStatus(activeRow, false);
                    }
                    else
                    {
                        this.CellEditStatus(activeRow, true);
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterCellUpdate event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

            if (activeRow.Index > 0)
            {
                if (string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[3].Text.Trim()) || string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[5].Text.Trim()) || string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index - 1].Cells[6].Text.Trim()))
                {
                    this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                    this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                }
            }
            
        }

        /// <summary>
        /// Handles the TextChanged event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_TextChanged(object sender, EventArgs e)
        {
            if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
              //  ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            string iteamCategoryText = string.Empty;
            UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
            UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

            if (activeCell != null)
            {
                //Added by Biju to disable the keyboard entry in the Category field.
                if (activeCell.Column.Index == 11)
                {
                    e.Handled = true;
                }
                //till here
                //Commented by Biju to implement #1209
                ////if (activeCell.Column.Index == 7)
                ////{
                ////    Decimal totalValues;
                ////    int deprID = 0;
                ////    int scheduleItemId;
                ////    int rollYear = 0;
                ////    int year = 0;

                ////    DataRow[] deprDes;
                ////    deprDes = this.tablescheduleLineItemDataSet.pclstDeprTable.Select("DeprName = " + "'" + activeCell.Text.Trim().Replace("'", "") + "'");
                ////    if (deprDes.Length == 0)
                ////    {
                ////        if (this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count > 0)
                ////        {
                ////            for (int count = 0; count < this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count; count++)
                ////            {
                ////                this.tableValues = this.tablescheduleLineItemDataSet.pclstDeprTable.Rows[0][this.tablescheduleLineItemDataSet.pclstDeprTable.DeprNameColumn.ColumnName].ToString();
                ////            }

                ////            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = this.tableValues.ToString();
                ////            int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Text.ToString().Trim(), out scheduleItemId);
                ////            int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Text.ToString().Trim(), out rollYear);
                ////            int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Text.ToString().Trim(), out year);
                ////            //this.tablescheduleLineItemDataSet = this.form35050Controller.WorkItem.F35050_GetListOutTableDetails(this.scheduleId);
                ////            this.ScheduleLineGrid.UpdateData();

                ////            if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString()))
                ////            {
                ////                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString(), out deprID);
                ////            }
                ////            else
                ////            {
                ////                int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                ////            }

                ////            this.accountTable = this.form35050Controller.WorkItem.F35050_CalculateAmount(Convert.ToInt32(scheduleItemId), Convert.ToInt32(rollYear), Convert.ToInt32(year), deprID);
                ////            decimal.TryParse(this.accountTable.AccountDetails.Rows[0][this.accountTable.AccountDetails.AmountColumn].ToString(), out totalValues);
                ////            this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = totalValues.ToString("#,##0");
                ////        }
                ////    }

                ////    if (this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count <= 0)
                ////    {
                ////        this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                ////    }
                ////}
                ////till here
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_KeyDown(object sender, KeyEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.ScheduleLineGrid.ActiveCell;

            //this.ScheduleLineGrid.AutoScrollOffset = new Point(0, 0);
            int recordcount = this.ScheduleLineGrid.Selected.Rows.Count;

            if (this.ScheduleLineGrid.ActiveCell != null)
            {
                if (activeCell.Column.Index != 15)
                {
                    if (activeCell.Text != null)
                    {
                        if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            if ((e.KeyValue == 88) || (e.KeyValue == 32))
                            {
                                this.EditEnabled();
                            }

                            if (e.KeyValue == 8)
                            {
                                this.EditEnabled();
                            }

                            if (e.KeyValue == 46)
                            {
                                this.EditEnabled();
                            }
                        }
                    }
                }

                ////if (activeCell.Column.Index == 5)
                ////{
                ////    this.valueChanged = true;
                ////}
            }

            if (activeRow != null)
            {
                if (this.slicePermissionField.deletePermission)
                {
                    if ((this.delKey != true) && (recordcount > 0))
                    {
                        if (!activeRow.IsAddRow)
                        {
                            if (e.KeyValue == 46)
                            {
                                bool deleteAllowed = false;
                                if (recordcount > 1)
                                {
                                    deleteAllowed = this.DeleteRecordss();
                                }
                                else
                                {
                                    deleteAllowed = this.DeleteRecord();
                                }

                                this.CalculateTotal();

                                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                                {
                                    ////this.FormLoad();
                                }

                                e.SuppressKeyPress = !deleteAllowed;
                                this.SetSmartPartHeight(0);
                                return;
                            }
                        }
                        else
                        {
                            if (this.ScheduleLineGrid.ActiveRow != null)
                            {
                                //// this.FormLoad();
                            }
                        }
                    }
                }

                this.delKey = false;
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                this.delKey = false;
                if (!this.basePanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
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
        /// Handles the MouseDown event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_MouseDown(object sender, MouseEventArgs e)
        {
            this.delKey = false;
           
        }

        /// <summary>
        /// Handles the BeforeRowsDeleted event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            e.DisplayPromptMsg = false;
        }

        /// <summary>
        /// Handles the BeforeRowActivate event of the ScheduleLineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineGrid_BeforeRowActivate(object sender, RowEventArgs e)
        {
            try
            {
                if (e.Row.Index > 25)
                {
                    UIElement uiElement = e.Row.GetUIElement();

                    if (uiElement != null)
                    {
                        //Commented by Biju on 07-Jul-2009 to disable the auto scrolling.
                        //this.yaxisPoint = uiElement.Rect.Y;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F35050 control.
        /// Event has been added for - Bug #5851: TSBG - 35050 Personal Property Schedule Line Items - Grid does not redraw itself after Minimize
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35050_Resize(object sender, EventArgs e)
        {
            this.SetSmartPartHeight(20);
        }

        #endregion ScheduleLineGrid Events

        #region Methods

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
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            //// Row selector
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;
            //// Column Position
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].Header.VisiblePosition = 0;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.VisiblePosition = 1;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 2;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Header.VisiblePosition = 3;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Header.VisiblePosition = 4;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Header.VisiblePosition = 5;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Header.VisiblePosition = 6;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Header.VisiblePosition = 7;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Header.VisiblePosition = 8;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Header.VisiblePosition = 9;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Header.VisiblePosition = 10;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Header.VisiblePosition = 11;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Header.VisiblePosition = 12;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Header.VisiblePosition = 13;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Header.VisiblePosition = 14;

            //// Column width
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].Width = 40;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Width = 95;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Width = 130;

            ////this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Width = 53;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Width = 45;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Width = 55;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Width = 90;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Width = 85;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Width = 76;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Width = 110;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Width = 0;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Width = 0;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Width = 0;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Width = 0;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Width = 0;

            //// Header caption
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].Header.Caption = "Line";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.Caption = "Category";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Header.Caption = "Description";

            ////this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Header.Caption = "Code";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Header.Caption = "Qnty";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Header.Caption = "Year";

            //// this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Header.Caption = "Recv";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Header.Caption = "Table";

            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Header.Caption = "Cost";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Header.Caption = "Depr";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Header.Caption = "Value";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.RollYearColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditValueColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Hidden = true;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Hidden = true;

            ////  Header position
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Header.Appearance.TextHAlign = Infragistics.Win.HAlign.Left;

            //// Add new template row
            if (this.PermissionFiled.newPermission && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                //// Allows Row Add with in the Band
                this.ScheduleLineGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            }
            //// Add summary row for column
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName]);
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName]);
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName]);
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries[0].SummaryDisplayArea = SummaryDisplayAreas.None;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries[1].SummaryDisplayArea = SummaryDisplayAreas.None;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Summaries[2].SummaryDisplayArea = SummaryDisplayAreas.None;

            //// Font 
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";

            ////  Text Align
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;

            ////this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            ////  Max length
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].MaxLength = 150;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].MaxLength = 200;

            ////this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].MaxLength = 50;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].MaxLength = 10;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].MaxLength = 4;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].MaxLength = 50;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].MaxLength = 20;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].MaxLength = 10;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].MaxLength = 20;
            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].MaxLength = 50;

            this.ScheduleLineGrid.DisplayLayout.Bands[0].Columns[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].CellActivation = Activation.ActivateOnly;
        }

        /// <summary>
        /// Loads the category item.
        /// </summary>
        private void LoadCategoryItem()
        {
            this.categoryValueList = System.Guid.NewGuid().ToString();

            if (this.ScheduleLineGrid.DisplayLayout.ValueLists.Exists(this.categoryValueList))
            {
                return;
            }

            ValueList objValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists.Add(this.categoryValueList);

            if (this.scheduleItemDataSet.SchedlueCategoryTable.Rows.Count > 0)
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.scheduleItemDataSet.SchedlueCategoryTable.Rows.Count; count++)
                {
                    objValueList.ValueListItems.Add(Convert.ToInt32(this.scheduleItemDataSet.SchedlueCategoryTable.Rows[count][this.scheduleItemDataSet.SchedlueCategoryTable.ItemCategoryIDColumn.ColumnName].ToString()), this.scheduleItemDataSet.SchedlueCategoryTable.Rows[count][this.scheduleItemDataSet.SchedlueCategoryTable.ItemCategoryColumn.ColumnName].ToString());
                }
            }
        }

        /// <summary>
        /// Loads the description item.
        /// </summary>
        private void LoadDescriptionItem()
        {
            this.descriptionValueList = System.Guid.NewGuid().ToString();

            if (this.ScheduleLineGrid.DisplayLayout.ValueLists.Exists(this.descriptionValueList))
            {
                return;
            }

            ValueList objValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists.Add(this.descriptionValueList);

            if (this.scheduleItemDataSet.SchedlueItemTable.Rows.Count > 0)
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.scheduleItemDataSet.SchedlueItemTable.Rows.Count; count++)
                {
                    objValueList.ValueListItems.Add(this.scheduleItemDataSet.SchedlueItemTable.Rows[count][this.scheduleItemDataSet.SchedlueItemTable.DescriptionColumn].ToString());
                }
            }
        }

        /// <summary>
        /// Loads the table item.
        /// </summary>
        private void LoadTableItem()
        {
            this.tableitemList = System.Guid.NewGuid().ToString();

            if (this.ScheduleLineGrid.DisplayLayout.ValueLists.Exists(this.tableitemList))
            {
                return;
            }
            
            ValueList objValueList = this.ScheduleLineGrid.DisplayLayout.ValueLists.Add(this.tableitemList);
            if (!this.checkDeprTableLoaded)
            {
                this.tablescheduleLineItemDataSet = this.form35050Controller.WorkItem.F35050_GetListOutTableDetails(this.scheduleId);
                this.checkDeprTableLoaded = true;
            }

            //// F35050_GetListOutTableDetails
            if (this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count > 0)
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.tablescheduleLineItemDataSet.pclstDeprTable.Rows.Count; count++)
                {
                    objValueList.ValueListItems.Add(Convert.ToInt32(this.tablescheduleLineItemDataSet.pclstDeprTable.Rows[count][this.tablescheduleLineItemDataSet.pclstDeprTable.DeprTableIDColumn.ColumnName].ToString()), this.tablescheduleLineItemDataSet.pclstDeprTable.Rows[count][this.tablescheduleLineItemDataSet.pclstDeprTable.DeprNameColumn.ColumnName].ToString());
                }
            }
        }

        /// <summary>
        /// SetSmartPartHeight
        /// </summary>
        /// <param name="extraHeight">extraHeight</param>
        private void SetSmartPartHeight(int extraHeight)
        {
            if (this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count > 0)
            {
                if (this.ScheduleLineGrid.Rows.TemplateAddRow.Height != null)
                {
                    int gridHeight = 0;
                    int formHeight = 0;
                    int deletedRecordCount = this.ScheduleLineGrid.Selected.Rows.Count;
                    int deletedGridHeight = (this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count - deletedRecordCount);

                    if (this.deleteSmartPartHeight)
                    {
                        gridHeight = (20 * deletedGridHeight) + this.ScheduleLineGrid.DisplayLayout.Bands[0].Header.Height + this.ScheduleLineGrid.Rows.TemplateAddRow.Height + extraHeight;
                    }
                    else
                    {
                        gridHeight = (20 * this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count) + this.ScheduleLineGrid.DisplayLayout.Bands[0].Header.Height + this.ScheduleLineGrid.Rows.TemplateAddRow.Height + extraHeight;
                    }

                    this.ScheduleLineGrid.Height = gridHeight - (this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count / 2);

                    formHeight = this.ScheduleLineGrid.Height + this.SummaryPanel.Height + 2;
                    this.MainPanel.Height = formHeight;
                    this.SummaryPanel.Location = new Point(0, this.ScheduleLineGrid.Height);
                    this.ScheduleLinePictureBox.Height = formHeight - 2;
                    this.Height = formHeight;

                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = "D20050.F35050";
                    sliceResize.SliceFormHeight = this.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.ScheduleLinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ScheduleLinePictureBox.Height, this.ScheduleLinePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.ScheduleLineGrid.Focus();
                }
            }
            else
            {
                int formHeight = 0;
                this.ScheduleLineGrid.Height = 50 + extraHeight;
                formHeight = 50 + extraHeight + this.SummaryPanel.Height + 2;
                this.MainPanel.Height = formHeight;
                this.SummaryPanel.Location = new Point(0, this.ScheduleLineGrid.Height);
                this.ScheduleLinePictureBox.Height = formHeight - 2;
                this.Height = formHeight;

                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = "D20050.F35050";
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.ScheduleLinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ScheduleLinePictureBox.Height, this.ScheduleLinePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Calculates the total.
        /// </summary>
        private void CalculateTotal()
        {
            this.TotalQntyTextBox.Text = string.Empty;
            this.TotalItemTextBox.Text = string.Empty;
            this.TotalValueTextBox.Text = string.Empty;

            this.TotalQntyTextBox.Text = this.ScheduleLineGrid.Rows.SummaryValues[0].SummaryText.Replace("Sum = ", "");
            this.TotalValueTextBox.Text = this.ScheduleLineGrid.Rows.SummaryValues[1].SummaryText.Replace("Sum = ", "");
            this.TotalItemTextBox.Text = this.ScheduleLineGrid.Rows.SummaryValues[2].SummaryText.Replace("Sum = ", "");
        }

        /// <summary>
        /// CalculateValue
        /// </summary>
        /// <param name="activeRow">activeRow</param>
        private void CalculateValue(UltraGridRow activeRow)
        {
            Double cost = 0.0;
            int qnty = 0;
            double depr = 0.0;

            double.TryParse(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value.ToString().Replace(",", ""), out cost);
            int.TryParse(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value.ToString().Replace(",", ""), out qnty);
            double.TryParse(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value.ToString().Replace("%", ""), out depr);
            depr = (depr / 100);

            //// if (Math.Round((cost * qnty) * (1 - depr)) != 0)
            if (Math.Round(cost - (cost * depr)) != 0)
            {
                double result = 0;
                ////result = Math.Round((cost * qnty) * (1 - depr));
                result = Math.Round(cost - (cost * depr));

                if (!result.ToString().Contains("-"))
                {
                    this.CalculateTotal();
                }
                else
                {
                    MessageBox.Show("Cost and Quantity exceeds the limit.", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                    activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value = string.Empty;
                }
            }
            else
            {
                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value = string.Empty;
                this.CalculateTotal();
            }
        }

        /// <summary>
        /// CalculateDeprPercentage
        /// </summary>
        /// <param name="activeRow">activeRow</param>
        private void CalculateDeprPercentage(UltraGridRow activeRow)
        {
            int rollYear = 0;
            int year = 0;
            string recv = string.Empty;
            int age = 0;
            decimal deprpercent;
            int deprID = 0;

            int.TryParse(this.scheduleItemDataSet.RollYearTable.Rows[0][this.scheduleItemDataSet.RollYearTable.RollYearColumn.ColumnName].ToString(), out rollYear);
            int.TryParse(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value.ToString(), out year);
            recv = activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString();

            if ((!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[7].Value.ToString()) || (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[7].Text.ToString()))))
            {
                //Commented by Biju to implement #1209
                //if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString()))
                //Added by Biju to implement #1209
                if (!string.IsNullOrEmpty(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString()))
                {
                    //Commented by Biju to implement #1209
                    //int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].OriginalValue.ToString(), out deprID);
                    //Added by by Biju to implement #1209
                    int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                    if (deprID == 0)
                    {
                        deprID = this.roughDeprTableId;
                    }
                }
                else
                {
                    if (deprID == 0)
                    {
                        deprID = this.roughDeprTableId;
                    }
                    else
                    {
                        int.TryParse(this.ScheduleLineGrid.Rows[activeRow.Index].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprID);
                    }
                }
            }
            else
            {
                deprID = 0;
            }

            activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = deprID;
            age = rollYear - year;

            // Accoring To the ChangeOrder We removed Recv Field so we check only for age.
            //// if (age >= 0 && (!string.IsNullOrEmpty(recv)))
            if (age >= 0)
            {
                this.deprTable = this.form35050Controller.WorkItem.GetDeprPercentage(rollYear, deprID, year);
                decimal.TryParse(this.deprTable.ListDeprTable.Rows[0][this.deprTable.ListDeprTable.Depr1Column].ToString(), out deprpercent);
                activeRow.Cells[this.deprTable.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprpercent.ToString("#,##0.00") + " %";

                this.ScheduleLineGrid.Rows[activeRow.Index].Cells[8].Appearance.ForeColor = Color.Black;
                activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = false;
            }
        }

        /// <summary>
        /// Forms the load.
        /// </summary>
        private void FormLoad()
        {
            this.checkDeprTableLoaded = false;
            //Added by Biju to implement #1209
            this.isDeprValueChanged = true;
            //Added by Biju on 07-Jul-2009 to disable the auto scrolling
            this.yaxisPoint = 0;
            
            this.scheduleItemDataSet = this.form35050Controller.WorkItem.GetScheduleLineItemDetails(this.scheduleId);
            this.listTableDetails = this.form35050Controller.WorkItem.F35050_GetListTableDetails(this.scheduleId);
            this.rowCount = this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count;
            this.ScheduleLineGrid.DataSource = null;
            
            this.ScheduleLineGrid.DataSource = this.scheduleItemDataSet.SchedlueLineItemTable;
            
            this.SetSmartPartHeight(0);
            this.CalculateTotal();
            this.checkOne = false;

            //this.listTableDetails = this.form35050Controller.WorkItem.F35050_GetListTableDetails(this.scheduleId);

            this.pageMode = TerraScanCommon.PageModeTypes.View;

            if (this.scheduleId > 0)
            {
                this.ScheduleLineGrid.Enabled = true;
            }
            else
            {
                this.ScheduleLineGrid.Enabled = false;
            }

            if (this.ScheduleLineGrid.Rows.Count > 0)
            {
                this.ScheduleLineGrid.Rows[0].Cells[15].Activate();
            }
            ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(F35050_MouseWheel);
        }

        private void F35050_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
            {
                this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
            }
        }

        /// <summary>
        /// Checks the empty cell.
        /// </summary>
        /// <param name="activeRow">The active row.</param>
        /// <returns>empty cell status</returns>
        private bool CheckEmptyCell(UltraGridRow activeRow)
        {
            if (string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Value.ToString()) && string.IsNullOrEmpty(activeRow.Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Value.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Restores the back color occupancy row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void RestoreBackColorRow(int rowIndex)
        {
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Appearance.BackColor = this.rowColor;

            ////this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.CodeColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.BackColor = this.rowColor;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.BackColor = this.rowColor;

            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.Black;

            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.Black;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.ForeColor = Color.Black;
        }

        /// <summary>
        /// Changes the back color row.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void ChangeBackColorRow(int rowIndex)
        {
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);

            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.BackColor = Color.FromArgb(0, 0, 128);

            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ItemCategoryColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DescriptionColumn.ColumnName].Appearance.ForeColor = Color.White;

            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.QntyColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.YearColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprDescriptionColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.OriginalCostColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.White;
            this.ScheduleLineGrid.Rows[rowIndex].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ValueColumn.ColumnName].Appearance.ForeColor = Color.White;
        }

        /// <summary>
        /// Deletes the recordss.
        /// </summary>
        /// <returns>deleted record status</returns>
        private bool DeleteRecordss()
        {
            if (MessageBox.Show("Are you sure you wish to Delete these Line Items?", "TerraScan – Delete Line Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.ScheduleLineGrid.Selected.Rows.Count > 0)
                {
                    int[] recordIndexes = new int[this.ScheduleLineGrid.Selected.Rows.Count];
                    int k = 0;

                    for (int j = 0; j < this.ScheduleLineGrid.Rows.Count; j++)
                    {
                        if (this.ScheduleLineGrid.Rows[j].Selected)
                        {
                            recordIndexes[k] = j;
                            k++;
                        }
                    }

                    for (int i = 0; i < recordIndexes.Length; i++)
                    {
                        int scheduleItemId = 0;

                        if (this.scheduleItemDataSet.SchedlueLineItemTable.Rows[recordIndexes[i] - i][this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].ToString() == "-99")
                        {
                            this.scheduleItemDataSet.SchedlueLineItemTable.Rows.RemoveAt(recordIndexes[i] - i);
                            this.ScheduleLineGrid.DataSource = this.scheduleItemDataSet.SchedlueLineItemTable;
                            this.CalculateTotal();
                        }
                        else
                        {
                            int.TryParse(this.scheduleItemDataSet.SchedlueLineItemTable.Rows[recordIndexes[i] - i][this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].ToString(), out scheduleItemId);
                            this.form35050Controller.WorkItem.F35050_DeleteScheduleLineItem(scheduleItemId, TerraScanCommon.UserId);
                            this.ScheduleLineGrid.Rows[recordIndexes[i] - i].Delete();
                            this.CalculateTotal();

                            if (this.ScheduleLineGrid.Rows.Count > 0)
                            {
                                this.ScheduleLineGrid.Rows[0].Cells[15].Activate();
                            }
                        }
                    }
                }

                this.ArrangeLineNumber();
                this.deleteSmartPartHeight = true;
                this.SetSmartPartHeight(0);
                return true;
            }
            else
            {
                this.deleteSmartPartHeight = false;
                this.SetSmartPartHeight(0);
                return false;
            }
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <returns>deleted record status</returns>
        private bool DeleteRecord()
        {
            if (MessageBox.Show("Are you sure you wish to Delete this Line Item?", "TerraScan – Delete Line Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (this.ScheduleLineGrid.ActiveRow != null)
                {
                    int scheduleItemId = 0;

                    this.ScheduleLineGrid.UpdateData();
                    if (this.scheduleItemDataSet.SchedlueLineItemTable.Rows[this.ScheduleLineGrid.ActiveRow.Index][this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].ToString() == "-99")
                    {
                        this.scheduleItemDataSet.SchedlueLineItemTable.Rows.RemoveAt(this.ScheduleLineGrid.ActiveRow.Index);
                        this.ScheduleLineGrid.DataSource = this.scheduleItemDataSet.SchedlueLineItemTable;
                        this.CalculateTotal();
                    }
                    else
                    {
                        int.TryParse(this.scheduleItemDataSet.SchedlueLineItemTable.Rows[this.ScheduleLineGrid.ActiveRow.Index][this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].ToString(), out scheduleItemId);
                        this.form35050Controller.WorkItem.F35050_DeleteScheduleLineItem(scheduleItemId, TerraScanCommon.UserId);
                        this.ScheduleLineGrid.ActiveRow.Delete();
                        this.CalculateTotal();
                        if (this.ScheduleLineGrid.Rows.Count > 0)
                        {
                            this.ScheduleLineGrid.Rows[0].Cells[15].Activate();
                        }
                    }
                }

                this.ArrangeLineNumber();
                this.deleteSmartPartHeight = true;
                this.SetSmartPartHeight(0);
                return true;
            }
            else
            {
                this.deleteSmartPartHeight = false;
                this.SetSmartPartHeight(0);
                return false;
            }
        }

        /// <summary>
        /// Arranges the line number.
        /// </summary>
        private void ArrangeLineNumber()
        {
            DataRow[] rows = this.scheduleItemDataSet.SchedlueLineItemTable.Select("ScheduleItemID > 0");
            for (int count = 0; count < rows.Length; count++)
            {
                this.ScheduleLineGrid.DisplayLayout.Bands[0].Layout.Rows[count].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.LineColumn.ColumnName].Value = count + 1;
            }

            this.ScheduleLineGrid.UpdateData();
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>error status</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            bool returnValue = false;

            double value = 0.0;
            double.TryParse(this.TotalValueTextBox.Text.Trim().Replace(",", ""), out value);
            value = Math.Round(value);

            if (value > 922337203685477)
            {
                MessageBox.Show("The Total Value exceeds the maximum limit.", "TerraScan T2 - Line Item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                ////this.initializeNewRow = true;
                this.ScheduleLineGrid.PerformAction(UltraGridAction.ActivateCell);

                return sliceValidationFields;
            }

            UltraGridRow activeRow = this.ScheduleLineGrid.ActiveRow;

            if (activeRow != null)
            {
                if (!activeRow.IsUnmodifiedTemplateAddRow)
                {
                    ////this.ScheduleLineGrid.UpdateData();
                    ////this.scheduleItemDataSet.SchedlueLineItemTable.AcceptChanges();

                    string filterCondtions = "((Description IS NULL or Description= '') or (Year IS NULL or Year= '') or (Year < 1899) or (Year > 2080) or (OriginalCost IS NULL OR OriginalCost='') or (Qnty IS NULL OR Qnty=''))";

                    DataRow[] drfilterCondtions = this.scheduleItemDataSet.SchedlueLineItemTable.Select(filterCondtions);

                    if (drfilterCondtions.Length == 0)
                    {
                        for (int i = 0; i < this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count; i++)
                        {
                            if (string.IsNullOrEmpty(this.scheduleItemDataSet.SchedlueLineItemTable.Rows[i][scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].ToString()))
                            {
                                this.ScheduleLineGrid.DisplayLayout.Bands[0].Layout.Rows[i].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Value = -99;
                            }

                            this.ScheduleLineGrid.DisplayLayout.Bands[0].Layout.Rows[i].Cells[this.scheduleItemDataSet.SchedlueLineItemTable.ScheduleIDColumn.ColumnName].Value = this.scheduleId;
                        }

                        this.CalculateTotal();
                        this.TotalItemTextBox.Text = Convert.ToString(this.scheduleItemDataSet.SchedlueLineItemTable.Rows.Count + 1);
                    }
                    else
                    {
                        if (MessageBox.Show("This Item cannot be saved because it is missing values or the given value range exceeds the limit.  Do you wish to Cancel this Line Item?", "TerraScan – Cancel New Item Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            activeRow.CancelUpdate();
                            this.SetSmartPartHeight(0);
                            this.FormLoad();
                            this.CalculateTotal();

                            sliceValidationFields.DisableNewMethod = false;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            this.ScheduleLineGrid.Focus();
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                            ////this.initializeNewRow = true;
                            this.ScheduleLineGrid.PerformAction(UltraGridAction.ActivateCell);
                        }
                    }
                }
            }

            if (activeRow == null)
            {
                if (MessageBox.Show("This Item cannot be saved because it is missing values.  Do you wish to Cancel this Line Item?", "TerraScan – Cancel New Item Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.SetSmartPartHeight(0);
                    this.FormLoad();
                    this.CalculateTotal();

                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    ////this.initializeNewRow = true;
                    this.ScheduleLineGrid.PerformAction(UltraGridAction.ActivateCell);
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>DataTable</returns>
        private DataTable DataRowToDataTable(DataRow[] tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            DataTable emptyDataTable = new DataTable();

            if (tempDataRow.Length > 0)
            {
                convertedDataSet.Merge(tempDataRow);
                return convertedDataSet.Tables[0];
            }
            else
            {
                return emptyDataTable;
            }
        }

        #endregion

        

    }
}
