// --------------------------------------------------------------------------------------------
// <copyright file="F35051.cs" company="Congruent">
//        Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//   This file contains methods for the Schedule Line Items.
// </summary>
// ----------------------------------------------------------------------------------------------
// Change History
// **********************************************************************************
// Date               Author           Description
// ----------        ---------         ---------------------------------------------------------
// 16 July 2008    Sadha Shivudu M     Created
// 29 Oct  2009     R.Malliga           Modified for the issue(3342)  
// 08 Dec 2010     Manoj Kumar          Modified for the CO:9841
// 20111107        Manoj Kumar         Modified for the issue (14028) 
// *********************************************************************************/

namespace D20050
{
    #region Namespace

    using System;
    using System.Data;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Shared;
    using Infrastructure.Interface;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;

    #endregion

    /// <summary>
    /// F35051 Schedule line items class file.
    /// </summary>
    [SmartPart]
    public partial class F35051 : BaseSmartPart
    {
        #region Instance Variables

        /// <summary>
        /// instance variable to hold the money max value for validation.
        /// </summary>
        private const long MONEYMAXVALUE = 922337203685477;

        /// <summary>
        /// instance variable to hold the 35051 controller object.
        /// </summary>
        private F35051Controller form35051Control;

        /// <summary>
        /// instance variable to hold the page mode.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// instance variable to hold the slice permission field object.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// instance variable to hold the form keyId value
        /// </summary>
        private int keyId;

        /// <summary>
        /// instance variable to hold the pageLoadStatus
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// instance variable to hold the schedule line items dataset.
        /// </summary>
        private F35051ScheduleLineItemsData scheduleLineItemDataSet = new F35051ScheduleLineItemsData();

        /// <summary>
        /// instance variable to hold the schedule line item table.
        /// </summary>
        private F35051ScheduleLineItemsData.ListSchedlueLineItemDataTable schedlueLineItemTable =
                new F35051ScheduleLineItemsData.ListSchedlueLineItemDataTable();

        /// <summary>
        /// instance variable to hold the schedule line grid active row.
        /// </summary>
        private UltraGridRow activeRow;

        /// <summary>
        /// instance variable to hold the schedule line grid active cell.
        /// </summary>
        private UltraGridCell activeCell;

        /// <summary>
        /// instance variable to hold the category value list name.
        /// </summary>
        private string categoryValueList;

        /// <summary>
        /// instance variable to hold the table value list name.
        /// </summary>
        private string tableValueList;

        /// <summary>
        /// instance variable to hold the schedule roll year value.
        /// </summary>
        private short scheduleRollYear;

        /// <summary>
        /// instance variable to hold the current depr percentage value.
        /// </summary>
        private decimal currentDeprPercent;

        /// <summary>
        /// instance variable to hold the y-axis point.
        /// </summary>
        private int yaxisPoint;

        /// <summary>
        /// instance variable to hold the base panel scrolled status.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// used to store edited cell position
        /// </summary>
        private int celleditposition = 0;

        /// <summary>
        /// Current row index
        /// </summary>
        private int currentRowIndex = 0;

        /// <summary>
        /// Minimum row count from tts_cfg
        /// </summary>
        private int countFromConfig;

        private short headerClick = 0;

        #endregion Instance Variables

        #region Formslice Instance Variables

        /// <summary>
        /// instance variable to hold the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  instance variable to hold the red color value
        /// </summary>
        private int redColor;

        /// <summary>
        ///  instance variable to hold the green color value
        /// </summary>
        private int greenColor;

        /// <summary>
        ///  instance variable to hold the blue color value
        /// </summary>
        private int blueColor;

        /// <summary>
        /// instance variable to hold the master form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// instance variable to hold the form master edit permission value
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Formslice Instance Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="F35051"/> class.
        /// </summary>
        public F35051()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F35051"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form number.</param>
        /// <param name="keyID">The key ID value.</param>
        /// <param name="red">The red color.</param>
        /// <param name="green">The green color.</param>
        /// <param name="blue">The blue color.</param>
        /// <param name="tabText">The tab text value.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35051(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();

            // this.validationFailed = false;
            this.masterFormNo = masterform;
            this.Tag = formNo;

            this.keyId = keyID;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.blueColor = blue;
            this.greenColor = green;
            this.formMasterPermissionEdit = permissionEdit;
            this.ScheduleLinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ScheduleLinePictureBox.Height, this.ScheduleLinePictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Eventpublication

        /// <summary>
        /// Occurs when [form slice_ edit enabled].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Occurs when [form slice_ validation alert].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Occurs when [form slice_ section indicator click].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Occurs when [form slice_ resize].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Occurs when [D9030_ F95005_ fomr master cancel].
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        #endregion Eventpublication

        #region Properties

        /// <summary>
        /// Gets or sets the F35051 control.
        /// </summary>
        /// <value>The F35051 control.</value>
        [CreateNew]
        public F35051Controller F35051Control
        {
            get { return this.form35051Control as F35051Controller; }
            set { this.form35051Control = value; }
        }

        #endregion Properties

        #region EventSubscription

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

                if (this.scheduleLineItemDataSet.GetRollYear.Rows.Count > 0)
                {
                    short validRecordCount;

                    if (short.TryParse(this.scheduleLineItemDataSet.GetRollYear.Rows[0][this.scheduleLineItemDataSet.GetRollYear.RecordCountColumn.ColumnName].ToString(), out validRecordCount))
                    {
                        if (!validRecordCount.Equals(0))
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
                        }
                        else
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
                else
                {
                    // Last Slice does not have a record also it will not return invalid slice
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
               // this.pageLoadStatus = true;
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.LoadScheduleLineItemGrid();
                //this.pageLoadStatus = false;
            }
        }

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
                    if (this.ValidateScheduleLineGridValues(null))
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
                string getEditedScheduelLineItemsXml = this.GetEditedAndNewScheduleLineItemsXml();

                if (!string.IsNullOrEmpty(getEditedScheduelLineItemsXml.Trim()))
                {
                    this.form35051Control.WorkItem.F35051_SaveScheduleLineItem(this.keyId, getEditedScheduelLineItemsXml, TerraScanCommon.UserId);
                }
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
            this.LoadScheduleLineItemGrid();

            // Maintain form master scroll posion 
            //this.SetFormMaster_ScrollPosition();
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.keyId > 0)
            {
                this.form35051Control.WorkItem.F35051_DeleteScheduleLineItem(this.keyId, null, TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;Infrastructure.Interface.AlertSliceOnClose&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                if (this.ValidateScheduleLineGridValues(null))
                {
                    eventArgs.Data.FlagFormClose = true;
                }
                else
                {
                    eventArgs.Data.FlagFormClose = false;
                }
            }
        }

        /// <summary>
        /// Called when [form master_ set scroll position].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormMaster_SetScrollPosition, ThreadOption.UserInterface)]
        public void OnFormMaster_SetScrollPosition(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.masterFormNo)
            {
                // Maintain form master scroll posion 
                this.SetFormMaster_ScrollPosition();
            }
        }

        #endregion EventSubscription

        #region Protected Methods

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

        /// <summary>
        /// Raises the <see cref="E:D9030_F95005_AlertFomrMasterCancel"/> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);

                //// Maintain form master scroll posion 
                //this.SetFormMaster_ScrollPosition();
            }
        }

        #endregion Protected Methods

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F35051 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F35051_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;

                this.LoadScheduleLineItemGrid();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F35051 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F35051_Resize(object sender, EventArgs e)
        {
            try
            {
               // this.ScheduleLineItemsEntirePanel.Height = 45000;
                this.Height = this.ScheduleLineItemsEntirePanel.Height;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.keyId > 0)
                {
                    object[] optionalParameter;
                    optionalParameter = new object[] { this.keyId, this.GetSelectedScheduleLineItemsXml() };

                    Form moveScheduleItemsForm = new Form();
                    moveScheduleItemsForm = this.form35051Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2205, optionalParameter, this.form35051Control.WorkItem);
                    if (moveScheduleItemsForm != null)
                    {
                        if (moveScheduleItemsForm.ShowDialog() == DialogResult.OK)
                        {
                            this.LoadScheduleLineItemGrid();

                            // Maintain form master scroll posion 
                            this.SetFormMaster_ScrollPosition();
                        }
                        else
                        {
                            this.ActiveControl = this.ScheduleLineItemGrid;
                            this.ActiveControl.Focus();
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
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.permissionFields.deletePermission)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteSheduleLineItems"), SharedFunctions.GetResourceString("DeleteSheduleLineItemsItemsHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                    {
                        // call the delete for selected schedule line items
                        this.form35051Control.WorkItem.F35051_DeleteScheduleLineItem(this.keyId, this.GetSelectedScheduleLineItemsXml(), TerraScanCommon.UserId);

                        // reload the schedule line item grid
                        this.LoadScheduleLineItemGrid();

                        // Maintain form master scroll posion 
                        this.SetFormMaster_ScrollPosition();
                    }
                    else
                    {
                        this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ScheduleLinePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLinePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the ScheduleLinePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLinePictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.ScheduleLineItemsToolTip.RemoveAll();
                this.ScheduleLineItemsToolTip.SetToolTip(this.ScheduleLinePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the DisplayLabelToolTip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DisplayLabelToolTip_MouseHover(object sender, EventArgs e)
        {
            try
            {
                Label sourceLabel = (Label)sender;
                string tempValue = string.Empty;
                tempValue = sourceLabel.Text;
                Graphics graphics = this.CreateGraphics();
                SizeF sizeF = graphics.MeasureString(tempValue, this.Font);
                float preferredwidth = sizeF.Width;

                if (preferredwidth > sourceLabel.Width)
                {
                    this.ScheduleLineItemsToolTip.RemoveAll();
                    this.ScheduleLineItemsToolTip.SetToolTip(sourceLabel, tempValue);
                }
                else
                {
                    this.ScheduleLineItemsToolTip.RemoveAll();
                }

                graphics.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Form Events

        #region Schedule Line ItemGrid Events

        /// <summary>
        /// Handles the InitializeLayout event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                this.CustomizeScheduleLineItemGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeRow event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row != null)
                {
                    if (string.IsNullOrEmpty(e.Row.Cells[this.schedlueLineItemTable.LineColumn.ColumnName].Value.ToString()))
                    {
                        e.Row.Cells[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].Activation = Activation.NoEdit;
                    }

                    if (this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count.Equals(0))
                    {
                        this.CellEditStatus(e.Row, true);
                    }
                    else
                    {
                        if (e.Row.Index.Equals(this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count))
                        {
                            // enable the cell edit
                            this.CellEditStatus(e.Row, true);
                        }
                        else
                        {
                            if (this.pageLoadStatus || e.Row.IsAddRow)
                            {
                                // disable the cell edit 
                                this.InitializeCategoryComboValues();
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].ValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists[this.categoryValueList];
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;

                                this.InitializeTableComboValues();

                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].ValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists[this.tableValueList];
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                            }
                        }
                    }

                    if (e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditValueColumn.ColumnName].Value != null)
                    {
                        if (!string.IsNullOrEmpty(e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditValueColumn.ColumnName].Value.ToString()))
                        {
                            if ((bool)e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditValueColumn.ColumnName].Value)
                            {
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ValueColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                ConditionValueAppearance operatorConditionValueAppearance = this.CreateOperatorConditionValueAppearance();
                                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.scheduleLineItemDataSet.ListSchedlueLineItem.ValueColumn.ColumnName].ValueBasedAppearance = operatorConditionValueAppearance;
                              
                               // e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ValueColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }

                    if (e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditPercentColumn.ColumnName].Value != null)
                    {
                        if (!string.IsNullOrEmpty(e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditPercentColumn.ColumnName].Value.ToString()))
                        {
                            if ((bool)e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.IsEditPercentColumn.ColumnName].Value)
                            {
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                            }
                            else
                            {
                                e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.Black;
                            }
                        }
                    }

                    if (e.Row.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.OriginalCostColumn.ColumnName].Value != null)
                    {
                        Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
                        appearance1.ForeColor = System.Drawing.Color.FromArgb(0, 130, 0);
                        Infragistics.Win.ConditionValueAppearance conditionValueAppearance1 = new Infragistics.Win.ConditionValueAppearance(new Infragistics.Win.ICondition[] {
                    ((Infragistics.Win.ICondition)(new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.LessThan     , 0, false, typeof(decimal ))))}, new Infragistics.Win.Appearance[] {
                    appearance1});
                        conditionValueAppearance1.ApplyAllMatchingConditions = false;
                        //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].Format = "#,##0.00##;(#,##0.00##);0.00##";
                        //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].ValueBasedAppearance = conditionValueAppearance1;
                    }
                   
                }
                else
                {
                    if (e.Row.Index == 0)
                    {
                        this.CellEditStatus(e.Row, true);
                    }
                }
                //Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
                //appearance.BackColor = System.Drawing.Color.Green;
                //appearance.BorderColor = System.Drawing.Color.Black;
                ////this.ScheduleLineItemGrid.DisplayLayout.EmptyRowSettings.CellAppearance = appearance;
                //this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Layout.Rows[0].CellAppearance = appearance;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            try
            {
                if (this.headerClick.Equals(0))
                {
                    this.headerClick = 1;
                }

                this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
                this.activeRow = this.ScheduleLineItemGrid.ActiveRow;

                this.InitializeCategoryComboValues();
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].ValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists[this.categoryValueList];
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ItemCategoryColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;

                this.InitializeTableComboValues();
                
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].ValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists[this.tableValueList];
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DeprTableIDColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;

                if (this.activeRow != null && this.activeCell != null)
                {
                    //this.SetSmartPartHeight(e.TemplateAddRow.Height);
                    if (this.ScheduleLineItemGrid.Rows.Count > 930)
                    {
                        this.ScheduleLineItemGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
                        this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
                    }
                    else
                    {
                        this.ScheduleLineItemGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                        this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
                    }

                    this.activeCell.Activate();
                    this.ScheduleLineItemGrid.PerformAction(UltraGridAction.EnterEditMode);

                    try
                    {
                        if (this.activeCell.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && this.activeCell.IsInEditMode == true && this.activeCell.SelText.Length > 0)
                        {
                            this.activeCell.SelStart = this.activeCell.SelText.Length;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
                this.activeRow = this.ScheduleLineItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null)
                {
                    if ((!this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.LineColumn.Caption)) &&
                        (!this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.ScheduleItemCheckColumn.Caption)))
                    {
                        // to bring the master form to edit mode
                        this.SetEditRecord();

                        // reset the number of lines with the new rows included
                        this.TotalLinesValueLabel.Text = this.ScheduleLineItemGrid.Rows.Count.ToString();    

                        // check for the IsEdited cell value for false and set the value as true
                        if (this.activeRow.Cells[this.schedlueLineItemTable.IsEditedColumn.ColumnName].Value.Equals(false))
                        {
                            this.activeRow.Cells[this.schedlueLineItemTable.IsEditedColumn.ColumnName].Value = true;
                        }

                        if (!this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.QntyColumn.Caption))
                        {
                            // check for qnty cell empty if so assign the default value as 1
                            if (string.IsNullOrEmpty(this.activeRow.Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Text))
                            {
                                this.activeRow.Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Value = 1;
                            }
                        }
                    }
                    else
                    {
                        this.activeRow.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.ValidateScheduleLineGridValues(e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowUpdate event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_AfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {
            try
            {
                // Reset the Row color 
                if (e.Row.CellAppearance.BackColor.Equals(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(106)))))))
                {
                    if (e.Row.IsAlternate)
                    {
                        // Reset the alternate row color 
                        e.Row.CellAppearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
                    }
                    else
                    {
                        // Reset the row color 
                        e.Row.CellAppearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterExitEditMode event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
                this.activeRow = this.ScheduleLineItemGrid.ActiveRow;

                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)
                    && this.activeRow != null
                    && this.activeCell != null
                    && this.activeCell.DataChanged)
                {
                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.DescriptionColumn.Caption))
                    {
                        this.activeCell.Value = this.activeCell.Text.Trim();
                        this.activeRow.Update();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.IsExemptColumn.Caption))
                    {
                        this.activeRow.Update();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.ItemCategoryColumn.Caption))
                    {
                        this.OnCategeoryComboItemChanged();
                    }

                    // check the qnty and originalCost values are changed, then re calculate the value
                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.QntyColumn.Caption) ||
                        this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.OriginalCostColumn.Caption))
                    {
                        this.CalculateScheduleLineItemValue();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.DeprPercentColumn.Caption))
                    {
                        // change the color of depr column 
                        this.OnDepreciationPercentageChanged();

                        // calculate the value
                        this.CalculateScheduleLineItemValue();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.YearColumn.Caption))
                    {
                        this.OnYearValueChanged();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.DeprDescriptionColumn.Caption))
                    {
                        this.OnTableComboItemChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeCellActivate event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            try
            {
                this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
                this.activeRow = this.ScheduleLineItemGrid.ActiveRow;

                if (this.activeRow != null)
                {
                    if (this.activeRow.Index > 0 && this.permissionFields.editPermission && this.formMasterPermissionEdit)
                    {
                        if (string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.YearColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.ScheduleLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Text.Trim()))
                        {
                            this.CellEditStatus(this.activeRow, false);
                            this.ScheduleLineItemGrid.Rows[this.activeRow.Index].Selected = true;
                        }
                        else
                        {
                            this.CellEditStatus(this.activeRow, true);
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
        /// Handles the Error event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ErrorEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeHeaderCheckStateChanged event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeHeaderCheckStateChangedEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_BeforeHeaderCheckStateChanged(object sender, BeforeHeaderCheckStateChangedEventArgs e)
        {
            try
            {
                if (e.Column.Key.Equals("IsExempt"))
                {
                    //if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) &&
                    //   this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count > 0)
                    if (!this.pageLoadStatus && this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count > 0)
                    {
                        // to bring the master form to edit mode
                        if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
                        {
                            if (e.NewCheckState.Equals(CheckState.Checked))
                            {
                                foreach (DataRow scheduleItemRow in this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows)
                                {
                                    if (!(bool)scheduleItemRow[this.schedlueLineItemTable.IsExemptColumn.ColumnName])
                                    {
                                        scheduleItemRow[this.schedlueLineItemTable.IsExemptColumn.ColumnName] = true;

                                        if (!(bool)scheduleItemRow[this.schedlueLineItemTable.IsEditedColumn.ColumnName])
                                        {
                                            scheduleItemRow[this.schedlueLineItemTable.IsEditedColumn.ColumnName] = true;
                                        }
                                    }
                                }
                            }
                            else if (e.NewCheckState.Equals(CheckState.Unchecked))
                            {
                                foreach (DataRow scheduleItemRow in this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows)
                                {
                                    if ((bool)scheduleItemRow[this.schedlueLineItemTable.IsExemptColumn.ColumnName])
                                    {
                                        scheduleItemRow[this.schedlueLineItemTable.IsExemptColumn.ColumnName] = false;
                                        if (!(bool)scheduleItemRow[this.schedlueLineItemTable.IsEditedColumn.ColumnName])
                                        {
                                            scheduleItemRow[this.schedlueLineItemTable.IsEditedColumn.ColumnName] = true;
                                        }
                                    }
                                }
                            }
                            
                            if (!this.pageLoadStatus)
                            {
                                if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                                {
                                    if (!this.headerClick.Equals(1))
                                    {
                                        // when page in edit mode, need to unckeck the selected items for the grid and disable the item check column.
                                        this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].SetHeaderCheckedState(this.ScheduleLineItemGrid.Rows, false);
                                        this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].CellActivation = Activation.Disabled;

                                        // disable the move and delte buttons
                                        this.EnableMoveAndDeleteControls(false);
                                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                                    }
                                }
                            }
                        }

                        this.ScheduleLineItemGrid.UpdateData();
                    }

                    this.headerClick = 2;
                }
                else
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) &&
                        this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count > 0)
                    {
                        if (e.NewCheckState.Equals(CheckState.Checked))
                        {
                            foreach (DataRow scheduleItemRow in this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows)
                            {
                                if (!(bool)scheduleItemRow[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName])
                                {
                                    scheduleItemRow[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName] = true;
                                }
                            }

                            // enable the move and delte buttons
                            this.EnableMoveAndDeleteControls(true);
                        }
                        else if (e.NewCheckState.Equals(CheckState.Indeterminate))
                        {
                            // enable the move and delte buttons
                            this.EnableMoveAndDeleteControls(true);
                        }
                        else if (e.NewCheckState.Equals(CheckState.Unchecked))
                        {
                            foreach (DataRow scheduleItemRow in this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows)
                            {
                                if ((bool)scheduleItemRow[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName])
                                {
                                    scheduleItemRow[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName] = false;
                                }
                            }

                            // disable the move and delte buttons
                            this.EnableMoveAndDeleteControls(false);
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }

                //this.headerClick = 2;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.basePanelScrolled)
                {
                    if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    {
                        // this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
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
        /// Handles the Enter event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_Enter(object sender, EventArgs e)
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
        /// Handles the BeforeRowsDeleted event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                e.Cancel = true;
                e.DisplayPromptMsg = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // check the Esc key and bring the form into view mode
                if (e.KeyValue.Equals((char)Keys.Escape) && !this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowActivate event of the ScheduleLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScheduleLineItemGrid_AfterRowActivate(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.GetFormMaster_ScrollPosition();
                    //this.headerClick = 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Schedule Line ItemGrid Events

        #region Private Methods

        /// <summary>
        /// Sets the edit record.c
        /// </summary>
        private void SetEditRecord()
        {
            if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        ////Coding added on 29/10/2009 by malliga for the issue : 3342
                        ////get the edited cell position
                        if (this.activeCell != null && !this.activeCell.Column.Header.Column.Key.Equals(this.schedlueLineItemTable.IsExemptColumn.ColumnName))
                        {
                            celleditposition = this.activeCell.SelStart;
                        }

                        // when page in edit mode, need to unckeck the selected items for the grid and disable the item check column.
                        this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].SetHeaderCheckedState(this.ScheduleLineItemGrid.Rows, false);
                        this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].CellActivation = Activation.Disabled;

                        // disable the move and delte buttons
                        this.EnableMoveAndDeleteControls(false);

                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));

                        if (this.activeRow != null && this.activeCell != null)
                        {
                            this.activeCell.Activate();
                            this.ScheduleLineItemGrid.PerformAction(UltraGridAction.EnterEditMode);

                            try
                            {
                                if (this.activeCell.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && this.activeCell.IsInEditMode == true && this.activeCell.SelText.Length > 0)
                                {
                                    ////this.activeCell.SelStart = this.activeCell.SelText.Length;
                                    ////Coding added on 29/10/2009 by malliga for the issue : 3342
                                    this.activeCell.SelStart = celleditposition;
                                    this.activeCell.SelLength = 0;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Customizes the schedule line item grid.
        /// </summary>
        private void CustomizeScheduleLineItemGrid()
        {
            UltraGridBand currentBand = this.ScheduleLineItemGrid.DisplayLayout.Bands[0];
          
            // set the RowSelectors to true
            currentBand.Override.RowSelectors = DefaultableBoolean.True;

            // set the RowLayoutStyle to ColumnLayout to customize the header of grid
            this.ScheduleLineItemGrid.DisplayLayout.Bands[0].RowLayoutStyle = RowLayoutStyle.ColumnLayout;

            // set the rows selection to none for avoid selecting multy rows at a time
            currentBand.Override.SelectTypeRow = SelectType.None;

            // set the schedule line item grid column display positions
            this.SetScheduleLineItemGridColumnDisplayPositions();

            this.ScheduleLineItemGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;

            // set the column visible position for active columns in grid
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].Header.VisiblePosition = 0;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].Header.VisiblePosition = 1;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Header.VisiblePosition = 2;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.VisiblePosition = 3;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Header.VisiblePosition = 4;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].Header.VisiblePosition = 5;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].Header.VisiblePosition = 6;
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Header.VisiblePosition = 7;
            
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Header.VisiblePosition = 8;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Header.VisiblePosition = 9;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].Header.VisiblePosition = 10;
            //currentBand.Columns["E"].Header.VisiblePosition = 22;

            // set the column visible position for hidded columns in grid
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Header.VisiblePosition = 11;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleIDColumn.ColumnName].Header.VisiblePosition = 12;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Header.VisiblePosition = 13;
            currentBand.Columns[this.schedlueLineItemTable.RollYearColumn.ColumnName].Header.VisiblePosition = 14;
            currentBand.Columns[this.schedlueLineItemTable.RecoveryColumn.ColumnName].Header.VisiblePosition = 15;
            currentBand.Columns[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Header.VisiblePosition = 16;
            currentBand.Columns[this.schedlueLineItemTable.IsEditValueColumn.ColumnName].Header.VisiblePosition = 17;
            currentBand.Columns[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Header.VisiblePosition = 18;
            currentBand.Columns[this.schedlueLineItemTable.DeprColumn.ColumnName].Header.VisiblePosition = 19;
            currentBand.Columns[this.schedlueLineItemTable.IsEditedColumn.ColumnName].Header.VisiblePosition = 20;
            currentBand.Columns[this.schedlueLineItemTable.IdentityColumn.ColumnName].Header.VisiblePosition = 21;
          
            // set the width for visible columns in grid
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].Width = 37;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].Width = 25;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Width = 654;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Width = 140;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Width = 30;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].Width = 50;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].Width = 56;
          
            currentBand.Columns[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Width = 99;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Width = 76;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].Width = 99;
            //currentBand.Columns["E"].Width = 20;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
       
            // set the width for non visible columns in grid
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleIDColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.RollYearColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.RecoveryColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.IsEditValueColumn.ColumnName].Width = 0;
         
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Width = 120;
            currentBand.Columns[this.schedlueLineItemTable.DeprColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.IsEditedColumn.ColumnName].Width = 0;
            currentBand.Columns[this.schedlueLineItemTable.IdentityColumn.ColumnName].Width = 0;
           
            // set the hidden property to true for non visible columns in grid
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.RollYearColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.RecoveryColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.IsEditValueColumn.ColumnName].Hidden = true;
           
            currentBand.Columns[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.DeprColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.IsEditedColumn.ColumnName].Hidden = true;
            currentBand.Columns[this.schedlueLineItemTable.IdentityColumn.ColumnName].Hidden = true;
          
            // set the cell appearance for active columns in grid
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].CellAppearance.TextVAlign = VAlign.Middle;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
        
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            // set the font name for cost, depr and value columns
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;

            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Format = "#,##0";
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].Format = "#,##0";
           
            // set the max length for editable columns in grid
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].MaxLength = 200;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].MaxLength = 150;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].MaxLength = 10;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].MaxLength = 4;
           
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].MaxLength = 50;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].MaxLength = 16;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].MaxLength = 10;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].MaxLength = 19;
         

            //// add summary row for cost and value columns
            currentBand.Summaries.Add(SummaryType.Sum, currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName]);
            currentBand.Summaries.Add(SummaryType.Sum, currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName]);
            currentBand.Summaries[0].SummaryDisplayArea = SummaryDisplayAreas.None;
            currentBand.Summaries[1].SummaryDisplayArea = SummaryDisplayAreas.None;
           
            // set the cell activation for Line and Value column
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].CellActivation = Activation.NoEdit;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].CellActivation = Activation.NoEdit;

            // set the value column tabStop property to false
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].TabStop = false;

            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemIDColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleIDColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryIDColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.RollYearColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.RecoveryColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.IsEditValueColumn.ColumnName].TabStop = false;
           
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].TabStop = true;
            currentBand.Columns[this.schedlueLineItemTable.DeprColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.IsEditedColumn.ColumnName].TabStop = false;
            currentBand.Columns[this.schedlueLineItemTable.IdentityColumn.ColumnName].TabStop = false;
          
            // default add an empty row bottom of the grid
            currentBand.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;


            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            appearance1.ForeColor = System.Drawing.Color.FromArgb(0, 130, 0);
            Infragistics.Win.ConditionValueAppearance conditionValueAppearance1 = new Infragistics.Win.ConditionValueAppearance(new Infragistics.Win.ICondition[] {
                    ((Infragistics.Win.ICondition)(new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.LessThan     , 0, false, typeof(decimal ))))}, new Infragistics.Win.Appearance[] {
                    appearance1});
            conditionValueAppearance1.ApplyAllMatchingConditions = false;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].ValueBasedAppearance = conditionValueAppearance1;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Format = "#,##0;(#,##0);0";

            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].ValueBasedAppearance = conditionValueAppearance1;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].Format = "#,##0;(#,##0);0";

            ConditionValueAppearance operatorConditionValueAppearance = this.CreateOperatorConditionValueAppearance();
            currentBand.Columns[this.scheduleLineItemDataSet.ListSchedlueLineItem.ValueColumn.ColumnName].ValueBasedAppearance = operatorConditionValueAppearance;

            //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].Format = "#,##0.00##;(#,##0.00##);0.00##";
            //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].ValueBasedAppearance = conditionValueAppearance1;
           
        }

        /// <summary>
        /// Sets the schedule line item grid column display positions.
        /// </summary>
        private void SetScheduleLineItemGridColumnDisplayPositions()
        {
            UltraGridBand currentBand = this.ScheduleLineItemGrid.DisplayLayout.Bands[0];

            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Header.Appearance.TextVAlign = VAlign.Middle;

            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].RowLayoutColumnInfo.OriginX = -1;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].RowLayoutColumnInfo.OriginY = 2;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].RowLayoutColumnInfo.SpanX = 2;
            currentBand.Columns[this.schedlueLineItemTable.LineColumn.ColumnName].RowLayoutColumnInfo.SpanY = 4;

            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].RowLayoutColumnInfo.OriginX = 2;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].RowLayoutColumnInfo.OriginY = 2;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].RowLayoutColumnInfo.SpanX = 2;
            currentBand.Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].RowLayoutColumnInfo.SpanY = 4;

            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.None;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].RowLayoutColumnInfo.OriginX = 4;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].RowLayoutColumnInfo.OriginY = 2;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].RowLayoutColumnInfo.SpanX = 18;
            currentBand.Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].RowLayoutColumnInfo.SpanY = 2;

            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Left;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].RowLayoutColumnInfo.OriginX = 4;
            currentBand.Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Header.CheckBoxAlignment = HeaderCheckBoxAlignment.Right;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].RowLayoutColumnInfo.OriginX = 6;
            currentBand.Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].RowLayoutColumnInfo.OriginX = 8;
            currentBand.Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].RowLayoutColumnInfo.OriginX = 10;
            currentBand.Columns[this.schedlueLineItemTable.YearColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Header.Caption = "Table";
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].RowLayoutColumnInfo.OriginX = 12;
            currentBand.Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].RowLayoutColumnInfo.OriginX = 14;
            currentBand.Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].RowLayoutColumnInfo.OriginX = 16;
            currentBand.Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;

            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].RowLayoutColumnInfo.OriginX = 18;
            currentBand.Columns[this.schedlueLineItemTable.ValueColumn.ColumnName].RowLayoutColumnInfo.OriginY = 4;
          
            //currentBand.Columns["E"].RowLayoutColumnInfo.LabelPosition = LabelPosition.LabelOnly;
            //currentBand.Columns["E"].RowLayoutColumnInfo.LabelPosition = LabelPosition.Right;
            //currentBand.Columns["E"].RowLayoutColumnInfo.OriginX = 20;
            //currentBand.Columns["E"].RowLayoutColumnInfo.OriginY = 4;
            //currentBand.Columns["E"].RowLayoutColumnInfo.ActualCellSize = new Size(1, 1);
        }

        /// <summary>
        /// Loads the schedule line item grid.
        /// </summary>
        private void LoadScheduleLineItemGrid()
        {
            this.pageLoadStatus = true;
            this.headerClick = 0;
            this.scheduleLineItemDataSet.ListSchedlueLineItem.Clear();
            this.ScheduleLineItemGrid.DataSource = null;
            this.scheduleLineItemDataSet = this.form35051Control.WorkItem.F35051_GetScheduleLineItemDetails(this.keyId);

            //DataColumn displayCol = new DataColumn("E");
            //this.scheduleLineItemDataSet.ListSchedlueLineItem.Columns.Add(displayCol);

            if (this.scheduleLineItemDataSet.VisibleRows.Rows.Count > 0)
            {
                // Minimum display row count ('AA_PPMinItems') from tts_cfg
                int.TryParse(this.scheduleLineItemDataSet.VisibleRows.Rows[0][this.scheduleLineItemDataSet.VisibleRows.MinPPItemsColumn.ColumnName].ToString(), out this.countFromConfig);
            }
            else
            {
                this.countFromConfig = 5;
            }
            /// Modified for the CO: 9841.
            this.totalCost();
            this.ScheduleLineItemGrid.DataSource = this.scheduleLineItemDataSet.ListSchedlueLineItem;
           
           this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Vertical;
            // set the activation for the Item CheckBox columns to allowEdit in view mode
            this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName].CellActivation = Activation.AllowEdit;

            // get the schedule rollYear
            if (this.scheduleLineItemDataSet.GetRollYear.Rows.Count > 0)
            {
                Int16.TryParse(this.scheduleLineItemDataSet.GetRollYear.Rows[0][this.scheduleLineItemDataSet.GetRollYear.RollYearColumn.ColumnName].ToString(), out this.scheduleRollYear);
            }

           
            // set the smartpart height
            this.SetSmartPartHeight(0);

            //this.CalculatetotalCost(); 
            // display the grid summeries
            this.DisplayScheduleLineGridSummeries();

            
            // set the page mode to view
            this.pageMode = TerraScanCommon.PageModeTypes.View;

            // disable the move and delte buttons
            this.EnableMoveAndDeleteControls(false);

            if (this.keyId > 0)
            {
                this.ScheduleLineItemGrid.Enabled = true;
            }
            else
            {
                this.ScheduleLineItemGrid.Enabled = false;
            }

            if (this.ScheduleLineItemGrid.Rows.Count > 0)
            {
                this.ScheduleLineItemGrid.Rows[0].Cells[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Activate();
            }

            // check the edit permission for formslice and form master and enable / disable schedule grid
            this.EnableControls(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
            this.pageLoadStatus = false;
        }

         /// <summary>
        /// Displays the schedule line grid summeries.
        /// </summary>
        private void DisplayScheduleLineGridSummeries()
        {
          long outTotalValueSum;
            if (this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count > 0)
            {
                this.TotalLinesValueLabel.Text = this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count.ToString();
                
                
                long.TryParse(this.ScheduleLineItemGrid.Rows.SummaryValues[1].SummaryText.Replace("Sum = ", string.Empty).ToString(), out outTotalValueSum);

                if (outTotalValueSum <= MONEYMAXVALUE)//if (outTotalValueSum >= 0 && outTotalValueSum <= MONEYMAXVALUE)
                {
                    if (outTotalValueSum >= 0)
                    {
                        this.TotalValueSumLabel.Text = outTotalValueSum.ToString("#,##0");
                    }
                    else
                    {
                        this.TotalValueSumLabel.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                    return;
                }

            }
            else
            {
                this.TotalLinesValueLabel.Text = string.Empty;
                this.TotalCostValueLabel.Text = string.Empty;
                this.TotalValueSumLabel.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="extraHeight">Height of the extra.</param>
        private void SetSmartPartHeight(int extraHeight)
        {
            this.ScheduleLineItemSummaryPanel.BringToFront();
            int headerHeight = this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Header.Height;

            int gridHeight = 0;
            int formHeight = 0;
            int gridRowCount = 0;

            if (this.ScheduleLineItemGrid.Rows.Count < this.countFromConfig)
            {
                // If total number of rows less than count('AA_PPMinItems') from tts_cfg
                // Set grid visible row count as 'AA_PPMinItems' value
                gridRowCount = this.countFromConfig - 1;
            }
            else
            {
                // If total number of rows greater than count('AA_PPMinItems') from tts_cfg
                // Set grid visible row count as recordset count
                gridRowCount = this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count;
            }

            if (gridRowCount <= 930)
            {
                gridHeight = (this.ScheduleLineItemGrid.Rows.TemplateAddRow.Height * gridRowCount) + headerHeight + this.ScheduleLineItemGrid.Rows.TemplateAddRow.Height + extraHeight;
                this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Scrollbars.None;
                this.ScheduleLineItemGrid.Height = gridHeight - gridRowCount;
                this.ScheduleLineItemGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            }
            else
            {
                gridHeight = (this.ScheduleLineItemGrid.Rows.TemplateAddRow.Height * 930) + headerHeight + this.ScheduleLineItemGrid.Rows.TemplateAddRow.Height + extraHeight;
                this.ScheduleLineItemGrid.DisplayLayout.Scrollbars = Scrollbars.Vertical;
                this.ScheduleLineItemGrid.Height = gridHeight - 930;
                this.ScheduleLineItemGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.None;
            }

            //this.ScheduleLineItemGrid.Height = gridHeight - gridRowCount;

            if (extraHeight.Equals(0))
            {
                formHeight = this.ScheduleLineItemHeaderPanel.Height + this.ScheduleLineItemGrid.Height + this.ScheduleLineItemSummaryPanel.Height - 3;
                this.ScheduleLineItemSummaryPanel.Location = new Point(-1, this.ScheduleLineItemHeaderPanel.Height + this.ScheduleLineItemGrid.Height - 4);
            }
            else
            {
                formHeight = this.ScheduleLineItemHeaderPanel.Height + this.ScheduleLineItemGrid.Height + this.ScheduleLineItemSummaryPanel.Height - 4;
                this.ScheduleLineItemSummaryPanel.Location = new Point(-1, this.ScheduleLineItemHeaderPanel.Height + this.ScheduleLineItemGrid.Height - 5);
            }

            this.ScheduleLineItemsEntirePanel.Height = formHeight;
            this.ScheduleLinePictureBox.Height = formHeight;
            this.Height = formHeight;

            if (this.ScheduleLineItemGrid.Rows.Count < gridRowCount)
            {
                ////to assgin empty row at the end of the gird
                this.ScheduleLineItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                this.ScheduleLineItemGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.Height + 2;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.ScheduleLinePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ScheduleLinePictureBox.Height, this.ScheduleLinePictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        /// <summary>
        /// Initializes the category combo values.
        /// </summary>
        private void InitializeCategoryComboValues()
        {
            this.categoryValueList = System.Guid.NewGuid().ToString();

            if (this.ScheduleLineItemGrid.DisplayLayout.ValueLists.Exists(this.categoryValueList))
            {
                return;
            }

            ValueList objValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists.Add(this.categoryValueList);

            if (this.scheduleLineItemDataSet.ListSchedlueCategory.Rows.Count > 0)
            {
                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.scheduleLineItemDataSet.ListSchedlueCategory.Rows.Count; count++)
                {
                    objValueList.ValueListItems.Add(Convert.ToInt32(this.scheduleLineItemDataSet.ListSchedlueCategory.Rows[count][this.scheduleLineItemDataSet.ListSchedlueCategory.ItemCategoryIDColumn.ColumnName].ToString()), this.scheduleLineItemDataSet.ListSchedlueCategory.Rows[count][this.scheduleLineItemDataSet.ListSchedlueCategory.ItemCategoryColumn.ColumnName].ToString());
                }
            }
        }

        /// <summary>
        /// Initializes the table combo values.
        /// </summary>
        private void InitializeTableComboValues()
        {
            this.tableValueList = System.Guid.NewGuid().ToString();

            if (this.ScheduleLineItemGrid.DisplayLayout.ValueLists.Exists(this.tableValueList))
            {
                return;
            }

            ValueList objValueList = this.ScheduleLineItemGrid.DisplayLayout.ValueLists.Add(this.tableValueList);

            if (this.scheduleLineItemDataSet.ListDeprTable.Rows.Count > 0)
            {
                //if (!this.scheduleLineItemDataSet.ListDeprTable.Rows[0]["DeprName"].ToString().Equals(" "))
                //{
                //    F35051ScheduleLineItemsData.ListDeprTableRow newRow = this.scheduleLineItemDataSet.ListDeprTable.NewListDeprTableRow();
                //    newRow.DeprTableID = 0;
                //    newRow.DeprName = " ";
                //    this.scheduleLineItemDataSet.ListDeprTable.Rows.InsertAt(newRow, 0);
                //}

                // Iterates to Add DisplayMember and DisplayValue from Table
                for (int count = 0; count < this.scheduleLineItemDataSet.ListDeprTable.Rows.Count; count++)
                {
                    objValueList.ValueListItems.Add(Convert.ToInt32(this.scheduleLineItemDataSet.ListDeprTable.Rows[count][this.scheduleLineItemDataSet.ListDeprTable.DeprTableIDColumn.ColumnName].ToString()), this.scheduleLineItemDataSet.ListDeprTable.Rows[count][this.scheduleLineItemDataSet.ListDeprTable.DeprNameColumn.ColumnName].ToString());
                }
            }
        }

        /// <summary>
        /// Cells the edit status.
        /// </summary>
        /// <param name="row">The selcted row.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void CellEditStatus(Infragistics.Win.UltraWinGrid.UltraGridRow row, bool value)
        {
            try
            {
                if (value)
                {
                    // making cell to allow edit
                    row.Cells[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.YearColumn.ColumnName].Activation = Activation.AllowEdit;
                    
                    row.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Activation = Activation.ActivateOnly;
                }
                else
                {
                    // making cell to readonly
                    row.Cells[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.DescriptionColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.IsExemptColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.YearColumn.ColumnName].Activation = Activation.NoEdit;
                   
                    row.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Activation = Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the move and delete controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableMoveAndDeleteControls(bool enable)
        {
            this.DeleteButton.Enabled = enable && this.permissionFields.deletePermission;
            this.MoveButton.Enabled = enable && this.permissionFields.deletePermission;
        }

        /// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            if (!enable)
            {
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.YearColumn.ColumnName].CellActivation = Activation.AllowEdit;
               
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].CellActivation = Activation.AllowEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].CellActivation = Activation.AllowEdit;
            }
            else
            {
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.ItemCategoryColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.IsExemptColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.QntyColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.YearColumn.ColumnName].CellActivation = Activation.NoEdit;
               
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Columns[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].CellActivation = Activation.NoEdit;
            }
        }

        /// <summary>
        /// Validates the schedule line grid values.
        /// </summary>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        /// <returns>The validation status.</returns>
        private bool ValidateScheduleLineGridValues(Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
            this.activeRow = this.ScheduleLineItemGrid.ActiveRow;

            if (this.activeRow != null && this.activeCell != null && this.activeCell.Value != null)
            {
                if (!this.activeCell.Text.Equals(this.activeCell.Value.ToString()) || this.activeCell.DataChanged)
                {
                    #region Qnty cell max limit validation

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.QntyColumn.Caption))
                    {
                        long qntyValue;

                        if (long.TryParse(this.activeCell.Text.Trim(), out qntyValue))
                        {
                            if (qntyValue >= 1 && qntyValue <= int.MaxValue)
                            {
                                this.activeCell.Value = qntyValue;
                            }
                            else if (qntyValue <= 0)
                            {
                                this.activeCell.Value = 1;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = 1;
                                this.CalculateScheduleLineItemValue();

                                if (e != null)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            this.activeCell.Value = 1;
                        }
                    }

                    #endregion Qnty cell max limit validation

                    #region Year cell max limit validation

                    // check for the Year value range to validate
                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.YearColumn.Caption))
                    {
                        short yearValue;

                        if (short.TryParse(this.activeCell.Text.Trim(), out yearValue))
                        {
                            if (yearValue > 1899 && yearValue < 2080)
                            {
                                this.activeCell.Value = yearValue;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = string.Empty;

                                if (e != null)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            this.activeCell.Value = string.Empty;
                        }
                    }

                    #endregion Year cell max limit validation

                    #region OriginalCost cell max limit validation

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.OriginalCostColumn.Caption))
                    {
                        long originalCostValue;
                        decimal decimalCostValue;
                        // used to replace the negative value issue TSBG 14028.
                        decimal.TryParse(this.activeCell.Text.Replace("(","-").Replace(",","").Replace(")","").Trim()  , out decimalCostValue);
                        decimalCostValue = Math.Round(decimalCostValue, 0, MidpointRounding.AwayFromZero);

                        if (long.TryParse(decimalCostValue.ToString(), out originalCostValue))
                        {
                            if (originalCostValue <= MONEYMAXVALUE)//if (originalCostValue >= 0 && originalCostValue <= MONEYMAXVALUE)
                            {
                                this.activeCell.Value = originalCostValue;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = 0;
                                this.activeRow.Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Value = 0;
                                this.CalculateScheduleLineItemValue();

                                if (e != null)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            this.activeCell.Value = 0;
                        }
                    }

                    #endregion OriginalCost cell max limit validation

                    #region Depr percent cell max limit validation

                    if (this.activeCell.Column.Header.Caption.Equals(this.schedlueLineItemTable.DeprPercentColumn.Caption))
                    {
                        decimal deprPercentValue;

                        if (decimal.TryParse(this.activeCell.Text.Replace("%", string.Empty), out deprPercentValue))
                        {
                            if (deprPercentValue >= 0 && deprPercentValue <= 100)
                            {
                                this.activeCell.Value = deprPercentValue.ToString("#,##0.00") + "%";
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("DeprPercentageValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = "100.00%";
                                this.CalculateScheduleLineItemValue();

                                if (e != null)
                                {
                                    e.Cancel = true;
                                }
                            }
                        }
                        else
                        {
                            this.activeCell.Value = "0.00%";
                        }
                    }

                    #endregion Depr percent cell max limit validation
                }
            }

            return true;
        }

        /// <summary>
        /// Called when [categeory combo item changed].
        /// </summary>
        private void OnCategeoryComboItemChanged()
        {
            if (this.scheduleLineItemDataSet.ListSchedlueCategory.Rows.Count > 0 && !string.IsNullOrEmpty(this.activeCell.Text.Trim()))
            {
                DataRow[] categoryRow;
                categoryRow = this.scheduleLineItemDataSet.ListSchedlueCategory.Select(this.scheduleLineItemDataSet.ListSchedlueCategory.ItemCategoryColumn.ColumnName + " = " + "'" + this.activeCell.Text.Trim().Replace("'", string.Empty) + "'");

                if (categoryRow.Length > 0)
                {
                   
                    int categoryDeprTableId;
                    int.TryParse(categoryRow[0][this.scheduleLineItemDataSet.ListSchedlueCategory.DeprTableIDColumn.ColumnName].ToString(), out categoryDeprTableId);

                    if (this.scheduleLineItemDataSet.ListDeprTable.Rows.Count > 0)
                    {
                        DataRow[] deprTableRow = this.scheduleLineItemDataSet.ListDeprTable.Select(this.scheduleLineItemDataSet.ListDeprTable.DeprTableIDColumn.ColumnName + "=" + categoryDeprTableId.ToString());

                        if (deprTableRow.Length > 0)
                        {
                            //this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = categoryDeprTableId;
                            this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = categoryDeprTableId;
                            this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Text;
                        }
                        else
                        {
                            //this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                            this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = 0;
                            this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                            categoryDeprTableId = 0;
                        }
                    }

                    this.activeRow.Cells[this.schedlueLineItemTable.ItemCategoryIDColumn.ColumnName].Value = this.activeCell.Value;
                   // this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = categoryDeprTableId;

                    this.GetDepreciationPercentageValue(categoryDeprTableId);
                    this.CalculateScheduleLineItemValue();
                }
                else
                {
                    this.activeCell.Value = string.Empty;
                    this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = 0;
                    this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
                    this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00%";
                    this.CalculateScheduleLineItemValue();
                }
            }
            else
            {
                this.activeCell.Value = string.Empty;
                this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = 0;
                this.activeRow.Cells[this.schedlueLineItemTable.DeprDescriptionColumn.ColumnName].Value = string.Empty;
            }
        }

        /// <summary>
        /// Called when [year value changed].
        /// </summary>
        private void OnYearValueChanged()
        {
            int deprTableId;

            if (int.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value.ToString(), out deprTableId))
            {
                // Get the Depr Percentage (ScheduleRollYear, DeprTableId, Year)
                this.GetDepreciationPercentageValue(deprTableId);

                // Calculate the ScheduleLineItem Value
                this.CalculateScheduleLineItemValue();
            }
        }

        /// <summary>
        /// Called when [table combo item changed].
        /// </summary>
        private void OnTableComboItemChanged()
        {
            if (this.scheduleLineItemDataSet.ListDeprTable.Rows.Count > 0 && !string.IsNullOrEmpty(this.activeCell.Text.Trim()))
            {
                DataRow[] deprTableRow;
                int deprTableId = 0;

                deprTableRow = this.scheduleLineItemDataSet.ListDeprTable.Select(this.scheduleLineItemDataSet.ListDeprTable.DeprNameColumn.ColumnName + " = " + "'" + this.activeCell.Text.Trim().Replace("'", string.Empty) + "'");

                if (deprTableRow.Length > 0)
                {
                    int.TryParse(deprTableRow[0][this.scheduleLineItemDataSet.ListDeprTable.DeprTableIDColumn.ColumnName].ToString(), out deprTableId);

                    // Get the Depr Percentage
                    this.GetDepreciationPercentageValue(deprTableId);
                }
                else
                {
                    this.activeCell.Value = string.Empty;
                    this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00%";
                }

                this.activeRow.Cells[this.schedlueLineItemTable.DeprTableIDColumn.ColumnName].Value = deprTableId;

                // Calculate the ScheduleLineItem Value
                this.CalculateScheduleLineItemValue();
            }
            else
            {
                this.activeCell.Value = string.Empty;
                this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value = "0.00%";
            }
        }

        /// <summary>
        /// Called when [depreciation percentage changed].
        /// </summary>
        private void OnDepreciationPercentageChanged()
        {
            decimal deprPercentValue;
            decimal.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.DeprColumn.ColumnName].Value.ToString(), out this.currentDeprPercent);

            decimal.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Text.Replace("%", string.Empty).Trim(), out deprPercentValue);
            this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value = deprPercentValue.ToString("#,##0.00") + "%";

            // check the depr percentage value against the original value and change the cell appearance black/red
            if (!this.currentDeprPercent.Equals(deprPercentValue))
            {
                // change the color of depr percentage to RED
                this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.FromArgb(128, 0, 0);
                this.activeRow.Cells[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = true;
            }
            else
            {
                // change the color of depr percentage to BLACK
                this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Appearance.ForeColor = Color.Black;
                this.activeRow.Cells[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = false;
            }
        }

        /// <summary>
        /// Gets the depreciation percentage value.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        private void GetDepreciationPercentageValue(int deprTableId)
        {
            short yearValue;
            short.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.YearColumn.ColumnName].Value.ToString(), out yearValue);
            this.scheduleLineItemDataSet.GetDeprPercent.Clear();
            this.scheduleLineItemDataSet.Merge(this.form35051Control.WorkItem.F35051_GetDeprPercentage(this.scheduleRollYear, deprTableId, yearValue));

            if (this.scheduleLineItemDataSet.GetDeprPercent.Rows.Count > 0)
            {
                decimal.TryParse(this.scheduleLineItemDataSet.GetDeprPercent.Rows[0][this.scheduleLineItemDataSet.GetDeprPercent.Depr1Column.ColumnName].ToString(), out this.currentDeprPercent);
                this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value = this.currentDeprPercent.ToString("#,##0.00") + "%";
                this.activeRow.Cells[this.schedlueLineItemTable.DeprColumn.ColumnName].Value = this.currentDeprPercent;

                // change the cell color to default black
                this.activeCell.Appearance.ForeColor = Color.Black;
                this.activeRow.Cells[this.schedlueLineItemTable.IsEditPercentColumn.ColumnName].Value = false;
            }
        }

        /// <summary>
        /// Calculates the schedule total Cost.
        /// </summary>
        private void totalCost()
        {
          int qntyValue;
          double costValue;
          double totalcost;
          double cost;
          cost = 0;
          int i = this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count;
            //used for calculating the rows.
        this.TotalLinesValueLabel.Text = this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count.ToString();
          if (this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count > 0)
          {
              for (int j = 0; j < i; j++)
              {

                  int.TryParse(this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows[j]["Qnty"].ToString(), out qntyValue);
                  double.TryParse(this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows[j]["OriginalCost"].ToString(), out costValue);
                  totalcost = Convert.ToDouble(qntyValue) * costValue;
                  cost = cost + totalcost;
              }
              if (cost <= MONEYMAXVALUE)//if (cost >= 0 && cost <= MONEYMAXVALUE)
              {
                  if (cost >= 0)
                  {
                      this.TotalCostValueLabel.Text = cost.ToString("#,##0");
                  }
                  else
                  {
                      this.TotalCostValueLabel.Text = "0";
                  }
              }
              else
              {
                  MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  this.activeRow.CancelUpdate();
                  return;
              }
          }
        
        }
        /// <summary>
        /// Calculates the schedule line item value.
        /// </summary>
        private void CalculateScheduleLineItemValue()
        {
            int qntyValue;
            double costValue;
            decimal deprPercentageValue;
            double calculatedScheduleLineItemValue;
            
            if (int.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.QntyColumn.ColumnName].Value.ToString(), out qntyValue))
            {
                double.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.OriginalCostColumn.ColumnName].Value.ToString(), out costValue);
                decimal.TryParse(this.activeRow.Cells[this.schedlueLineItemTable.DeprPercentColumn.ColumnName].Value.ToString().Replace("%", string.Empty), out deprPercentageValue);
                calculatedScheduleLineItemValue = Convert.ToDouble(qntyValue) * costValue * Convert.ToDouble(1 - (deprPercentageValue / 100));

                if (calculatedScheduleLineItemValue <= MONEYMAXVALUE)//if (calculatedScheduleLineItemValue >= 0 && calculatedScheduleLineItemValue <= MONEYMAXVALUE)
                {
                    //if (calculatedScheduleLineItemValue >= 0)
                    //{
                        this.activeRow.Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Value = calculatedScheduleLineItemValue;
                    //}
                    //else
                    //{
                    //    this.activeRow.Cells[this.schedlueLineItemTable.ValueColumn.ColumnName].Value = 0;
                    //}
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                }
                // Modified CO:9841.
                this.totalCost(); 
               
                // display the refreshed grid summeries when any value calculation happen
                this.DisplayScheduleLineGridSummeries();
                this.activeRow.Update();

                // check the original depr percentage with the modified depr percentage and change color.
                this.OnDepreciationPercentageChanged();
            }
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
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
        /// <returns>The validation status.</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            long totalSumValue;
            long totalCostValue;
            this.activeRow = this.ScheduleLineItemGrid.ActiveRow;
            this.activeCell = this.ScheduleLineItemGrid.ActiveCell;

            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            long.TryParse(this.TotalValueSumLabel.Text.Trim().Replace(",", string.Empty), out totalSumValue);
            long.TryParse(this.TotalCostValueLabel.Text.Trim().Replace(",", string.Empty), out totalCostValue);

            // verify the total cost and total value exceeds the max money value
            if (totalSumValue > MONEYMAXVALUE || totalCostValue > MONEYMAXVALUE)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsTotalMaxValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                return sliceValidationFields;
            }

            if (this.activeRow != null)
            {
                if (!this.activeRow.IsUnmodifiedTemplateAddRow)
                {
                    string filterCondtions = "((Description IS NULL or Description= '') or (Qnty IS NULL) or (Year IS NULL or Year= '') or (Year < 1899) or (Year > 2080) or (DeprPercent IS NULL or DeprPercent = '') or (OriginalCost IS NULL) or (Value IS NULL))";
                    DataRow[] drfilterCondtions = this.scheduleLineItemDataSet.ListSchedlueLineItem.Select(filterCondtions);

                    if (drfilterCondtions.Length.Equals(0))
                    {
                        for (int i = 0; i < this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.Count; i++)
                        {
                            this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Layout.Rows[i].Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.ScheduleIDColumn.ColumnName].Value = this.keyId;
                        }

                        this.SetFormMaster_ScrollPosition();
                    }
                    else
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsDiscardSaveMessage2"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                        {
                            this.activeRow.CancelUpdate();

                            // reload the schedule line item grid
                            //this.LoadScheduleLineItemGrid();

                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            //this.SetFormMaster_ScrollPosition();

                            sliceValidationFields.DisableNewMethod = false;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            // test
                            foreach (DataRow filteredRow in drfilterCondtions)
                            {
                                int rowPosition = this.scheduleLineItemDataSet.ListSchedlueLineItem.Rows.IndexOf(filteredRow);
                                //this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Layout.Rows[rowPosition].CellAppearance.BackColor = System.Drawing.Color.PapayaWhip;
                                this.ScheduleLineItemGrid.DisplayLayout.Bands[0].Layout.Rows[rowPosition].CellAppearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(106)))));
                                InitializeRowEventArgs initializeRow = new InitializeRowEventArgs(this.ScheduleLineItemGrid.Rows[rowPosition], true);
                                this.ScheduleLineItemGrid_InitializeRow(this.ScheduleLineItemGrid, initializeRow);
                                //this.currentRowIndex = rowPosition;
                            }

                            this.ScheduleLineItemGrid.Focus();
                            
                            // Maintain form master scroll posion 
                            this.SetFormMaster_ScrollPosition();
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                            this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsDiscardSaveMessage1"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    // reload the schedule line item grid
                    this.LoadScheduleLineItemGrid();

                    this.SetFormMaster_ScrollPosition();

                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Gets the selected schedule line items XML.
        /// </summary>
        /// <returns>The selected schedule line items xml.</returns>
        private string GetSelectedScheduleLineItemsXml()
        {
            string selectedScheduleLineItemsXml = string.Empty;
            F35051ScheduleLineItemsData selectedScheduleLineItemsDataSet = new F35051ScheduleLineItemsData();

            string filterCondition = this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName + "=" + bool.TrueString;
            DataRow[] selectedScheduleLineItemRows = this.scheduleLineItemDataSet.ListSchedlueLineItem.Select(filterCondition);

            if (selectedScheduleLineItemRows.Length > 0)
            {
                selectedScheduleLineItemsDataSet.Merge(selectedScheduleLineItemRows);
                selectedScheduleLineItemsXml = TerraScanCommon.GetXmlString(selectedScheduleLineItemsDataSet.ListSchedlueLineItem);
            }

            return selectedScheduleLineItemsXml;
        }

        /// <summary>
        /// Gets the edited and new schedule line items XML.
        /// </summary>
        /// <returns>The edited and new schedule line items xml.</returns>
        private string GetEditedAndNewScheduleLineItemsXml()
        {
            string editedAndNewScheduleLineItemsXml = string.Empty;
            F35051ScheduleLineItemsData selectedScheduleLineItemsDataSet = new F35051ScheduleLineItemsData();

            string filterCondition = this.schedlueLineItemTable.IsEditedColumn.ColumnName + "=" + bool.TrueString;
            this.scheduleLineItemDataSet.ListSchedlueLineItem.AcceptChanges();
            DataRow[] selectedScheduleLineItemRows = this.scheduleLineItemDataSet.ListSchedlueLineItem.Select(filterCondition);

            if (selectedScheduleLineItemRows.Length > 0)
            {
                selectedScheduleLineItemsDataSet.Merge(selectedScheduleLineItemRows);
                selectedScheduleLineItemsDataSet.ListSchedlueLineItem.Columns.Remove(this.schedlueLineItemTable.IsEditedColumn.ColumnName);
                selectedScheduleLineItemsDataSet.ListSchedlueLineItem.Columns.Remove(this.schedlueLineItemTable.ScheduleItemCheckColumn.ColumnName);
                editedAndNewScheduleLineItemsXml = TerraScanCommon.GetXmlString(selectedScheduleLineItemsDataSet.ListSchedlueLineItem);
            }

            return editedAndNewScheduleLineItemsXml;
        }

        /// <summary>
        /// Gets the form master_ scroll position.
        /// </summary>
        private void GetFormMaster_ScrollPosition()
        {
            try
            {
                if (this.ScheduleLineItemGrid.ActiveRow != null)
                {
                    this.currentRowIndex = this.ScheduleLineItemGrid.ActiveRow.Index;
                }

                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                }
            }
            catch (Exception ex)
            {
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Forms the master_ scroll position.
        /// </summary>
        private void SetFormMaster_ScrollPosition()
        {
            try
            {
                this.pageLoadStatus = true;
                if (this.ScheduleLineItemGrid.Rows.Count < 1)
                {
                    // For empty rows presents in Grid
                    this.ScheduleLineItemGrid.Focus();
                }
                else if (this.currentRowIndex >= this.ScheduleLineItemGrid.Rows.Count)
                {
                    // For last row focus
                    // After removal of some rows from Grid
                    this.ActiveControl = this.ScheduleLineItemGrid;
                    this.currentRowIndex = this.ScheduleLineItemGrid.Rows.Count - 1;
                    this.ScheduleLineItemGrid.Rows[this.currentRowIndex].Activate();
                    this.activeCell = this.ScheduleLineItemGrid.ActiveRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DescriptionColumn.ColumnName];
                    this.ScheduleLineItemGrid.ActiveCell = this.activeCell;
                    this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell, false, false);
                }
                else
                {
                    //this.ScheduleLineItemGrid.Focus();
                    this.ActiveControl = this.ScheduleLineItemGrid;
                    this.ScheduleLineItemGrid.Rows[this.currentRowIndex].Activate();
                    this.activeCell = this.ScheduleLineItemGrid.ActiveRow.Cells[this.scheduleLineItemDataSet.ListSchedlueLineItem.DescriptionColumn.ColumnName];
                    this.ScheduleLineItemGrid.ActiveCell = this.activeCell;
                    this.ScheduleLineItemGrid.ActiveCell.Activate();
                    this.activeCell = this.ScheduleLineItemGrid.ActiveCell;
                    this.activeCell.Activate();
                    this.ScheduleLineItemGrid.PerformAction(UltraGridAction.ActivateCell, false, false);
                    //this.ScheduleLineItemGrid.ActiveCell.Activation = Activation.AllowEdit;
                }

                // SendKeys.Send("+{TAB}");

                // Set form master panel scroll position
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }

                this.pageLoadStatus = false;
            }
            catch (Exception ex)
            {
            }
        }

        #region CreateOperatorConditionValueAppearance
        private ConditionValueAppearance CreateOperatorConditionValueAppearance()
        {
            // This method will create a ConditionValueAppearance using OperatorConditions. OperatorConditions
            // do not rely on UltraCalcManager and they cannot use formulas. They need a ConditionOperator
            // and a value to determine if the cell matches the condition. 

            // Create a new ConditionValueAppearance
            ConditionValueAppearance conditionValueAppearance = new ConditionValueAppearance();

            // Create a OperatorCondition that checks for negative numbers
            OperatorCondition negativeCondition = new OperatorCondition(ConditionOperator.LessThan, 0);

            // Create an appearance that sets the ForeColor to red.
            Infragistics.Win.Appearance negativeAppearance = new Infragistics.Win.Appearance("Negative");
            negativeAppearance.ForeColor = System.Drawing.Color.FromArgb(0, 130, 0);

            // Create a OperatorCondition that checks for positive numbers
            OperatorCondition positiveCondition = new OperatorCondition(ConditionOperator.GreaterThanOrEqualTo, 0);

            // Create an appearance that sets the ForeColor to blue.
            Infragistics.Win.Appearance positiveAppearance = new Infragistics.Win.Appearance("Positive");
            positiveAppearance.ForeColor = Color.Black;

            // Now that we have the conditions and appearances we need, add them to the 
            // conditionValueAppearance. The conditions will be evaluated in order.
            conditionValueAppearance.Add(negativeCondition, negativeAppearance);
            conditionValueAppearance.Add(positiveCondition, positiveAppearance);

            return conditionValueAppearance;
        }
        #endregion CreateOperatorConditionValueAppearance
        #endregion Private Methods
    }
}
