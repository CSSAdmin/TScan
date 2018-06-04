//// -------------------------------------------------------------------------------
//// <copyright file="F35055.cs" company="Congruent">
//// Copyright (c) Congruent Info-Tech.  All rights reserved.
//// </copyright>
//// <summary>
//// This file contains methods for the Personal Property Line Item.
//// </summary>
//// --------------------------------------------------------------------------------
//// Change History
//// ********************************************************************************
//// Date              Author            Description
//// ----------       ---------         ---------------------------------------------
//// 23/7/2009        R.Malliga           Created  
////29/10/2009        R.Malliga           Modified for the issue(3342)  
//// *********************************************************************************

namespace D25055
{
    #region Namespace
    using System;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
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
    /// F35055 Class
    /// </summary>
    [SmartPart]
    public partial class F35055 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// Used to holds the MoneyMaxValue 
        /// </summary>
        private const long MONEYMAXVALUE = 922337203685477;

        /// <summary>
        /// Used to store the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        ///  Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        ///  Local variable for Red
        /// </summary>
        private int redColor;

        /// <summary>
        /// Local variable for Green.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// Local variable for Blue.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Used to store the formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// variable holds of form35055Controller to call the WorkItem
        /// </summary>
        private F35055Controller form35055Controller;

        /// <summary>
        /// Used to store the scheduleId
        /// </summary>
        private int scheduleId;

        /// <summary>
        ///  Used to store the PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Used to holds the F35055PPLineItemData dataset
        /// </summary>
        private F35055PPLineItemData pipeLineItemDataSet = new F35055PPLineItemData();

        /// <summary>
        /// Used to holds the common dataset
        /// </summary>
        private CommonData commonData = new CommonData();

        /// <summary>
        /// used to holds the tableitemList
        /// </summary>
        private string tableItemList;

        /// <summary>
        /// used to holds the fuelitemList
        /// </summary>
        private string fuelItemList;

        /// <summary>
        /// used to holds the codeitemList
        /// </summary>
        private string codeItemList;

        /// <summary>
        /// Used to holds the YesNoList
        /// </summary>
        private string yesNoList;

        /// <summary>
        /// holds the grid active row.
        /// </summary>
        private UltraGridRow activeRow;

        /// <summary>
        /// holds the grid active cell.
        /// </summary>
        private UltraGridCell activeCell;

        /// <summary>
        /// holds the PageLoadStatus value.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// variable to hold the y-axis point.
        /// </summary>
        private int yaxisPoint;

        /// <summary>
        /// variable to hold the basePanelScrolled is used to store the flag value for PageLoad.
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F35055"/> class.
        /// </summary>
        public F35055()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F35055"/> class.
        /// </summary>
        /// <param name="masterForm">The master form.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="redColor">Color of the red.</param>
        /// <param name="greenColor">Color of the green.</param>
        /// <param name="blueColor">Color of the blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35055(int masterForm, int formNo, int keyId, int redColor, int greenColor, int blueColor, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterForm;
            this.Tag = formNo;
            this.scheduleId = keyId;
            this.sectionIndicatorText = tabText;
            this.redColor = redColor;
            this.blueColor = blueColor;
            this.greenColor = greenColor;
            this.formMasterPermissionEdit = permissionEdit;
            this.PPLineItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PPLineItemPictureBox.Height, this.PPLineItemPictureBox.Width, tabText, redColor, greenColor, blueColor);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F35055Control.
        /// </summary>
        /// <value>The F35055Control.</value>
        [CreateNew]
        public F35055Controller F35055Control
        {
            get { return this.form35055Controller as F35055Controller; }
            set { this.form35055Controller = value; }
        }

        #endregion Properties

        #region Event Subscription

        /// <summary>
        /// Event Subscription SetSlicePermission
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            ////Checking the form is null and disposed
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo.Equals(eventArgs.Data.MasterFormNo))
                {
                    ////set the slice permission
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    ////Checking Invalid Keyid or not
                    if (this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
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
        }

        /// <summary>
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            ////Check the delete permission 
            if (this.scheduleId > 0)
            {
                ////DBCall for delete
                this.form35055Controller.WorkItem.F35055_DeleteScheduleLineItem(this.scheduleId, null, TerraScanCommon.UserId);
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
            ////Loading the grid values
            this.LoadPPLineItemGrid();
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////Here Checking all the required fields are have a value or not thru Checkerrors method
            if (this != null && this.IsDisposed != true)
            {
                if (this.PermissionFiled.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////get the changes datarow
                    string getEditedPPLineItemsXml = this.GetEditedAndNewScheduleLineItemsXml();
                    ////DB Call for Save
                    this.form35055Controller.WorkItem.F35055_SaveScheduleLineItem(this.scheduleId, getEditedPPLineItemsXml, TerraScanCommon.UserId);
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
                ////get the ppid
                this.scheduleId = eventArgs.Data.SelectedKeyId;
                this.pageLoadStatus = true;
                ////load the method to fill the grid
                this.LoadPPLineItemGrid();
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
            ////Validate the cell values
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                if (this.ValidatePPLineGridValues(null))
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

        #endregion

        #region Protected Methods
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

        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F35055 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F35055_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////Load the Grid 
                this.LoadPPLineItemGrid();
                //// Register the events for Master Form scroll
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(this.Scroll_Click);
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Resize event of the F35055 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F35055_Resize(object sender, EventArgs e)
        {
            ////set the size the form.
            this.Height = this.TopPanel.Height + this.GridLineItemPanel.Height + 7;
        }

        #endregion

        #region PP Line Item Picture Box Events
        /// <summary>
        /// Handles the Click event of the ParcelHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                ////For Section Indicator Click ,form get collapse/Expaned
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the PPLineItemPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PPLineItemPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ////To ToolTip display
                this.PPLineItemToolTip.SetToolTip(this.PPLineItemPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the TotalQntyLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TotalQntyLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ////For Grid Summaries ToolTip
                Label label = (Label)sender;
                this.PPLineItemToolTip.RemoveAll();
                this.PPLineItemToolTip.SetToolTip(label, label.Text.Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Loads the PP line item grid.
        /// </summary>
        private void LoadPPLineItemGrid()
        {
            try
            {
                this.pageLoadStatus = true;
                ////DB Call for Load the grid
                this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Clear();
                this.PPLineItemGrid.DataSource = null;
                this.pipeLineItemDataSet = this.form35055Controller.WorkItem.F35055_GetPPLineItemsDetails(this.scheduleId);

                if (this.pipeLineItemDataSet.F35055_VisibleRows.Rows.Count > 0)
                {
                    int.TryParse(this.pipeLineItemDataSet.F35055_VisibleRows.Rows[0][this.pipeLineItemDataSet.F35055_VisibleRows.MinPPItemsColumn.ColumnName].ToString(), out this.countFromConfig);
                }
                else
                {
                    this.countFromConfig = 5;
                }

                this.PPLineItemGrid.DataSource = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem;
                this.GridLineItemPanel.AutoScroll = true;
                this.GridLineItemPanel.AutoScrollPosition = new Point();

                //// set the activation for the Item CheckBox columns to allowEdit in view mode
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].CellActivation = Activation.AllowEdit;

                ////set the height of the form
                this.SetSmartPartHeight();

                ////Based on the grid count set the enable and disable the controls
                if (this.scheduleId > 0)
                {
                    this.PPLineItemGrid.Enabled = true;
                }
                else
                {
                    this.PPLineItemGrid.Enabled = false;
                }

                ////Calcualte grid summary values
                this.PPLineGridSummeries();

                // disable the move and delte buttons
                this.EnableMoveAndDeleteControls(false);

                ////check the edit permission for formslice and form master and enable / disable schedule grid
                this.EnableDisbaleCells(false || !this.permissionFields.editPermission || !this.formMasterPermissionEdit);
                this.pageLoadStatus = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.Cursor = Cursors.Default;
                this.PPLineItemGrid.UpdateData();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [table combo item changed].
        /// </summary>
        private void OnTableComboItemChanged()
        {
            try
            {
                ////this method is used to check the enter value is exist or not.
                if (this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Rows.Count > 0 && !string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                {
                    DataRow[] deprTableRow;

                    deprTableRow = this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Select(this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.PPDeprNameColumn.ColumnName + " = " + "'" + this.activeCell.Text.Trim().Replace("'", string.Empty) + "'");

                    ////if the value is not exist in the table then it will set the cell to empty
                    if (deprTableRow.Length.Equals(0))
                    {
                        this.activeCell.Value = string.Empty;
                    }
                }
                else
                {
                    this.activeCell.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [Code combo item changed].
        /// </summary>
        private void OnCodeComboItemChanged()
        {
            try
            {
                ////this method is used to check the enter value is exist or not.
                if (this.pipeLineItemDataSet.F35055_ScheduleItemCode.Rows.Count > 0 && !string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                {
                    DataRow[] deprTableRow;

                    deprTableRow = this.pipeLineItemDataSet.F35055_ScheduleItemCode.Select(this.pipeLineItemDataSet.F35055_ScheduleItemCode.ItemCodeColumn.ColumnName + " = " + "'" + this.activeCell.Text.Trim().Replace("'", string.Empty) + "'");

                    ////if the value is not exist in the table then it will set the cell to empty
                    if (deprTableRow.Length.Equals(0))
                    {
                        this.activeCell.Value = string.Empty;
                    }
                }
                else
                {
                    this.activeCell.Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        ///  Called when [Fuel combo item changed].
        /// </summary>
        private void OnFuelComboItemChanged()
        {
            try
            {
                ////this method is used to check the enter value is exist or not.
                if (this.pipeLineItemDataSet.F35055_Fuel.Rows.Count > 0 && !string.IsNullOrEmpty(this.activeCell.Text.Trim()))
                {
                    DataRow[] deprTableRow;

                    deprTableRow = this.pipeLineItemDataSet.F35055_Fuel.Select(this.pipeLineItemDataSet.F35055_Fuel.FuelColumn.ColumnName + " = " + "'" + this.activeCell.Text.Trim().Replace("'", string.Empty) + "'");

                    ////if the value is not exist in the table then it will set the cell to empty
                    if (deprTableRow.Length.Equals(0))
                    {
                        this.activeCell.Value = string.Empty;
                    }
                }
                else
                {
                    this.activeCell.Value = string.Empty;
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
        /// <param name="row">The selcted row.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void CellEditStatus(Infragistics.Win.UltraWinGrid.UltraGridRow row, bool value)
        {
            try
            {
                if (value)
                {
                    ///// making cell to allow edit
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Activation = Activation.AllowEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Activation = Activation.AllowEdit;
                }
                else
                {
                    ///// making cell to readonly
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Activation = Activation.NoEdit;
                    row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Activation = Activation.NoEdit;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the disbale cells.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        private void EnableDisbaleCells(bool value)
        {
            try
            {
                if (!value)
                {
                    //// making cell to allow edit
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].CellActivation = Activation.AllowEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].CellActivation = Activation.AllowEdit;
                }
                else
                {
                    //// making cell to readonly
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].CellActivation = Activation.NoEdit;
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].CellActivation = Activation.NoEdit;
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
            ////Based on the permission enable/disable the controls
            this.DeleteButton.Enabled = enable && this.permissionFields.deletePermission;
            this.MoveButton.Enabled = enable && this.permissionFields.deletePermission;
        }

        /// <summary>
        /// PPs the line grid summeries.
        /// </summary>
        private void PPLineGridSummeries()
        {
            try
            {
                ////used to store total Quantity Column 
                long totalQty = 0;
                ////used to store total cost Column 
                long totalCost = 0;
                ////used to store total value Column 
                long totalValue = 0;
                ////used to store total factored value Column 
                decimal totalFactoredValue = 0;

                if (this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count > 0)
                {
                    ////If the Dataset have a rows then it will store the summary value to the corresponding local variable.
                    long.TryParse(this.PPLineItemGrid.Rows.SummaryValues[0].SummaryText.Replace("Sum = ", string.Empty).ToString(), out totalQty);
                    long.TryParse(this.PPLineItemGrid.Rows.SummaryValues[1].SummaryText.Replace("Sum = ", string.Empty).ToString(), out totalCost);
                    long.TryParse(this.PPLineItemGrid.Rows.SummaryValues[2].SummaryText.Replace("Sum = ", string.Empty).ToString(), out totalValue);
                    decimal.TryParse(this.PPLineItemGrid.Rows.SummaryValues[3].SummaryText.Replace("Sum = ", string.Empty).ToString(), out totalFactoredValue);
                }
                else
                {
                    ////if the dataset dosnot have a value then it will clear all textbox value.
                    this.TotalQntyLabel.Text = string.Empty;
                    this.TotalCostLabel.Text = string.Empty;
                    this.TotalValuecolumnLabel.Text = string.Empty;
                    this.TotalFactoredLabel.Text = string.Empty;
                }

                ////To check whether the total quantity value is greater than money value or not
                if (totalQty >= 0 && totalQty <= int.MaxValue)
                {
                    this.TotalQntyLabel.Text = totalQty.ToString("#,##0");
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                    return;
                }

                ////To check whether the total cost value is greater than money value or not
                if (totalCost >= 0 && totalCost <= MONEYMAXVALUE)
                {
                    this.TotalCostLabel.Text = totalCost.ToString("#,##0");
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                    return;
                }

                ////To check whether the total "value" value is greater than money value or not
                if (totalValue >= 0 && totalValue <= MONEYMAXVALUE)
                {
                    this.TotalValuecolumnLabel.Text = totalValue.ToString("#,##0");
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                    return;
                }

                ////To check whether the total "factored value" value is greater than money value or not
                if (totalFactoredValue >= 0 && totalFactoredValue <= MONEYMAXVALUE)
                {
                    this.TotalFactoredLabel.Text = totalFactoredValue.ToString("#,##0");
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.activeRow.CancelUpdate();
                    return;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Validates the PP line grid values.
        /// </summary>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        /// <returns>returns boolean value</returns>
        private bool ValidatePPLineGridValues(Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                ////to get the active cell of the grid
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                ////to get the active cell of the row
                this.activeRow = this.PPLineItemGrid.ActiveRow;

                ////Check active row and active cell value
                if (this.activeRow != null && this.activeCell != null)
                {
                    //// check for the quantity value range to validate
                    if (this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName))
                    {
                        long qntyValue;
                        if (long.TryParse(this.activeCell.Text.Trim(), out qntyValue))
                        {
                            //// check the max value with integer datatype
                            if (qntyValue >= 1 && qntyValue <= int.MaxValue)
                            {
                                this.activeCell.Value = qntyValue;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = 1;

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

                    //// check for the Year value range to validate
                    if (this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName))
                    {
                        short yearValue;

                        if (short.TryParse(this.activeCell.Text.Trim(), out yearValue))
                        {
                            //// check the year range 
                            if (yearValue > 1899 && yearValue < 2080)
                            {
                                this.activeCell.Value = yearValue;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = string.Empty; ;

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

                    //// check for the original cost value range to validate
                    if (this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName))
                    {
                        long originalCostValue;
                        decimal decimalCostValue;

                        decimal.TryParse(this.activeCell.Text.Trim(), out decimalCostValue);
                        decimalCostValue = Math.Round(decimalCostValue, 0, MidpointRounding.AwayFromZero);

                        if (long.TryParse(decimalCostValue.ToString(), out originalCostValue))
                        {
                            ////check the originalcost value with money value
                            if (originalCostValue >= 0 && originalCostValue <= MONEYMAXVALUE)
                            {
                                this.activeCell.Value = originalCostValue;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("MaxLimitValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.activeCell.Value = 0;

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
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            return true;
        }

        /// <summary>
        /// Calculates the valueand factored value.
        /// </summary>
        private void CalculateValueandFactoredValue()
        {
            try
            {
                ////to get the active cell of the grid
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                ////to get the active cell of the row
                this.activeRow = this.PPLineItemGrid.ActiveRow;

                ////Check active row and active cell value
                if (this.activeRow != null && this.activeCell != null)
                {
                    decimal decimalCostValue;
                    long originalCostValue = 0;
                    short yearValue = 0;
                    short rollYearValue = 0;
                    int pdeprTableId = 0;
                    int trend = 0;

                    ////Get the Depriciation[Table]column value
                    if (!string.IsNullOrEmpty(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Text.Trim()))
                    {
                        int.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprTableIDColumn.ColumnName].Value.ToString(), out pdeprTableId);
                    }

                    ////Get the Original column value
                    if (!string.IsNullOrEmpty(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Text.Trim()))
                    {
                        decimal.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Value.ToString(), out decimalCostValue);
                        decimalCostValue = Math.Round(decimalCostValue, 0, MidpointRounding.AwayFromZero);
                        long.TryParse(decimalCostValue.ToString(), out originalCostValue);
                    }

                    ////Get Trend column value
                    if (!string.IsNullOrEmpty(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Text.Trim()))
                    {
                        int.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value.ToString(), out trend);
                    }

                    ////Get Year column value
                    if (!string.IsNullOrEmpty(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Text.Trim()))
                    {
                        short.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Value.ToString(), out yearValue);
                    }

                    ////Get Roll year column value
                    if (this.pipeLineItemDataSet.F35055_YearSchedlueLineItem.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.pipeLineItemDataSet.F35055_YearSchedlueLineItem.Rows[0][this.pipeLineItemDataSet.F35055_YearSchedlueLineItem.RollYearColumn.ColumnName].ToString()))
                        {
                            short.TryParse(this.pipeLineItemDataSet.F35055_YearSchedlueLineItem.Rows[0][this.pipeLineItemDataSet.F35055_YearSchedlueLineItem.RollYearColumn.ColumnName].ToString(), out rollYearValue);
                        }
                    }

                    ////DB Call for Value Calculation
                    this.pipeLineItemDataSet.F35055_ValueCalculation.Rows.Clear();
                    this.pipeLineItemDataSet.Merge(this.form35055Controller.WorkItem.F35055_GetValueCalculation(this.scheduleId, pdeprTableId, originalCostValue, trend, yearValue, rollYearValue));
                    if (this.pipeLineItemDataSet.F35055_ValueCalculation.Rows.Count > 0)
                    {
                        this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].Value = this.pipeLineItemDataSet.F35055_ValueCalculation.Rows[0][this.pipeLineItemDataSet.F35055_ValueCalculation.ValueColumn.ColumnName].ToString();
                        this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Value = this.pipeLineItemDataSet.F35055_ValueCalculation.Rows[0][this.pipeLineItemDataSet.F35055_ValueCalculation.FactoredValueColumn.ColumnName].ToString();
                    }

                    this.activeRow.Update();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Arithmetic overflow error converting expression to data type money."))
                {
                    MessageBox.Show("Arithmetic overflow error converting expression to data type money.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.activeCell.Value = 0;
                    return;
                }
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>formNo validation value</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            long totalCostValue;
            long totalValuevalue;
            long totalfactoredvalue;

            this.activeRow = this.PPLineItemGrid.ActiveRow;
            this.activeCell = this.PPLineItemGrid.ActiveCell;

            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            long.TryParse(this.TotalCostLabel.Text.Trim().Replace(",", string.Empty), out totalCostValue);
            long.TryParse(this.TotalValuecolumnLabel.Text.Trim().Replace(",", string.Empty), out totalValuevalue);
            long.TryParse(this.TotalFactoredLabel.Text.Trim().Replace(",", string.Empty), out totalfactoredvalue);

            // verify the total cost and total value exceeds the max money value
            if (totalCostValue > MONEYMAXVALUE || totalValuevalue > MONEYMAXVALUE || totalfactoredvalue > MONEYMAXVALUE)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsTotalMaxValidationMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                sliceValidationFields.DisableNewMethod = true;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                this.PPLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                return sliceValidationFields;
            }

            if (this.activeRow != null)
            {
                if (!this.activeRow.IsUnmodifiedTemplateAddRow)
                {
                    ////Checking Required field is null or not
                    string filterCondtions = "((Description IS NULL or Description = '') or (Qnty IS NULL OR Qnty < 0) or (Year IS NULL or Year < 1899 or Year > 2080) or (OriginalCost IS NULL) or (IsTrend IS NULL))";

                    DataRow[] drfilterCondtions = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Select(filterCondtions);
                    if (drfilterCondtions.Length.Equals(0))
                    {
                        for (int i = 0; i < this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count; i++)
                        {
                            this.PPLineItemGrid.DisplayLayout.Bands[0].Layout.Rows[i].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ScheduleIDColumn.ColumnName].Value = this.scheduleId;
                        }
                    }
                    else
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsDiscardSaveMessage2"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                        {
                            this.activeRow.CancelUpdate();

                            // reload the pp line item grid
                            //this.LoadPPLineItemGrid();
                            
                            this.pageMode = TerraScanCommon.PageModeTypes.View;

                            sliceValidationFields.DisableNewMethod = false;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                        }
                        else
                        {
                            this.PPLineItemGrid.Focus();
                            sliceValidationFields.DisableNewMethod = true;
                            sliceValidationFields.RequiredFieldMissing = false;
                            sliceValidationFields.ErrorMessage = string.Empty;
                            this.PPLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                        }
                    }
                }
            }
            else
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("ScheduleLineItemsDiscardSaveMessage1"), SharedFunctions.GetResourceString("ExciseTaxAffDvtMissingHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                {
                    // reload the pp line item grid
                    this.LoadPPLineItemGrid();

                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                }
                else
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    this.PPLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                }
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                {
                    ////get the edited cell position
                    ////Coding added on 29/10/2009 by malliga for the issue : 3342
                    celleditposition = this.activeCell.SelStart;
                    //// when page in edit mode, need to unckeck the selected items for the grid and disable the item check column.
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].SetHeaderCheckedState(this.PPLineItemGrid.Rows, false);
                    this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].CellActivation = Activation.Disabled;
                    //// disable the move and delte buttons
                    this.EnableMoveAndDeleteControls(false);
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    ////set the focus to the edited column
                    if (this.activeRow != null && this.activeCell != null)
                    {
                        this.activeCell.Activate();
                        this.PPLineItemGrid.PerformAction(UltraGridAction.EnterEditMode);

                        try
                        {
                            if (this.activeCell.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && this.activeCell.IsInEditMode == true && this.activeCell.SelText.Length > 0)
                            {
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
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            try
            {
                this.PPLineItemGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;

                this.PPLineItemGrid.DisplayLayout.BorderStyle = UIElementBorderStyle.None;

                ////To set Visibile position for all columns
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName].Header.VisiblePosition = 0;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Header.VisiblePosition = 1;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Header.VisiblePosition = 2;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Header.VisiblePosition = 3;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].Header.VisiblePosition = 4;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Header.VisiblePosition = 5;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Header.VisiblePosition = 6;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Header.VisiblePosition = 7;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Header.VisiblePosition = 8;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Header.VisiblePosition = 9;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].Header.VisiblePosition = 10;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Header.VisiblePosition = 11;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Header.VisiblePosition = 12;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Header.VisiblePosition = 13;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Header.VisiblePosition = 14;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Header.VisiblePosition = 15;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Header.VisiblePosition = 16;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName].Header.VisiblePosition = 17;

                ////To set some columns are hidden
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ScheduleIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ScheduleItemIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCategoryIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCategoryColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.RollYearColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.RecoveryColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprPercentColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditPercentColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditValueColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprTableIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelIDColumn.ColumnName].Hidden = true;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName].Hidden = true;

                ////To set header caption 
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Header.Caption = "Code";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Header.Caption = string.Empty;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Header.Caption = "Table";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Header.Caption = "Cost";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Header.Caption = "Trend";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Header.Caption = "Factored Value";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Header.Caption = "Horse Power";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Header.Caption = "Leased X Ref";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Header.Caption = "Tractor Type";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Header.Caption = "Model Number";

                ////To set Header CheckBox Visibility
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Header.CheckBoxAlignment = HeaderCheckBoxAlignment.Center;

                ////To set cell appearence text alignment
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

                ////Header text alignment
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Left;

                ////Cell Activation set to NoEdit
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].CellActivation = Activation.NoEdit;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].CellActivation = Activation.NoEdit;

                //this.PPLineItemGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;

                //// Add summary row for column
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName]);
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName]);
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName]);
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries.Add(SummaryType.Sum, this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName]);
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries[0].SummaryDisplayArea = SummaryDisplayAreas.None;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries[1].SummaryDisplayArea = SummaryDisplayAreas.None;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries[2].SummaryDisplayArea = SummaryDisplayAreas.None;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Summaries[3].SummaryDisplayArea = SummaryDisplayAreas.None;

                ////To set maxlength of the column
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].MaxLength = 30;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].MaxLength = 20;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].MaxLength = 200;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].MaxLength = 40;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].MaxLength = 10;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].MaxLength = 4;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].MaxLength = 30;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].MaxLength = 18;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].MaxLength = 19;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].MaxLength = 19;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].MaxLength = 30;

                //// Font 
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellAppearance.FontData.Name = "Arial";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].CellAppearance.FontData.SizeInPoints = 8;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].CellAppearance.FontData.Name = "Courier New";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].CellAppearance.FontData.Bold = DefaultableBoolean.False;

                ////set the tabstop
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].TabStop = false;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].TabStop = false;

                ////To set the width of the column
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName].Width = 38;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Width = 38;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Width = 65;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Width = 170;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName].Width = 145;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Width = 65;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Width = 50;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Width = 70;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Width = 101;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].Width = 110;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Width = 110;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName].Width = 130;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName].Width = 115;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName].Width = 136;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Width = 145;
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName].Width = 146;

                ////Cell formatting
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Format = "#,##0";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ValueColumn.ColumnName].Format = "#,##0";
                this.PPLineItemGrid.DisplayLayout.Bands[0].Columns[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FactoredValueColumn.ColumnName].Format = "#,##0";

                this.PPLineItemGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        private void SetSmartPartHeight()
        {
            try
            {
                int childRowCount;
                int tempChildHeight;
                int gridHeight;

                if (this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count < this.countFromConfig)
                {
                    childRowCount = this.countFromConfig;
                }
                else
                {
                    childRowCount = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count + 1;
                }

                tempChildHeight = childRowCount * 22;
                gridHeight = tempChildHeight + (this.PPLineItemGrid.DisplayLayout.Bands[0].Header.Height + 18);
                this.PPLineItemGrid.Height = gridHeight - (18 + (childRowCount * 2) + (childRowCount - 2));
                this.GridLineItemPanel.Height = this.PPLineItemGrid.Height + this.SumariesPanel.Height + 17;
                this.SumariesPanel.Top = this.PPLineItemGrid.Bottom - 2;
                this.PPLineItemPictureBox.Height = this.TopPanel.Height + this.GridLineItemPanel.Height - 1;
                this.Height = this.PPLineItemPictureBox.Height + 7;

                if (this.PPLineItemGrid.Rows.Count < childRowCount)
                {
                    ////to assgin empty row at the end of the gird
                    this.PPLineItemGrid.DisplayLayout.EmptyRowSettings.ShowEmptyRows = true;
                    this.PPLineItemGrid.DisplayLayout.EmptyRowSettings.Style = EmptyRowStyle.AlignWithDataRows;
                }

                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = "D25055.F35055";
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.Height = sliceResize.SliceFormHeight;
                this.PPLineItemPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PPLineItemPictureBox.Height, this.PPLineItemPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the table item.
        /// </summary>
        private void LoadTableItem()
        {
            try
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                this.tableItemList = System.Guid.NewGuid().ToString();

                ValueList objValueList = this.PPLineItemGrid.DisplayLayout.ValueLists.Add(this.tableItemList);

                if (this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Rows.Count > 0)
                {
                    for (int count = 0; count < this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Rows.Count; count++)
                    {
                        objValueList.ValueListItems.Add(Convert.ToInt32(this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Rows[count][this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.PPDeprTableIDColumn.ColumnName].ToString()), this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.Rows[count][this.pipeLineItemDataSet.F35055_DeprSchedlueLineItem.PPDeprNameColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the fuel.
        /// </summary>
        private void LoadFuel()
        {
            try
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                this.fuelItemList = System.Guid.NewGuid().ToString();

                ValueList objValueList = this.PPLineItemGrid.DisplayLayout.ValueLists.Add(this.fuelItemList);

                if (this.pipeLineItemDataSet.F35055_Fuel.Rows.Count > 0)
                {
                    for (int count = 0; count < this.pipeLineItemDataSet.F35055_Fuel.Rows.Count; count++)
                    {
                        objValueList.ValueListItems.Add(this.pipeLineItemDataSet.F35055_Fuel.Rows[count][this.pipeLineItemDataSet.F35055_Fuel.FuelIDColumn.ColumnName].ToString(), this.pipeLineItemDataSet.F35055_Fuel.Rows[count][this.pipeLineItemDataSet.F35055_Fuel.FuelColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the code.
        /// </summary>
        private void LoadCode()
        {
            try
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                this.codeItemList = System.Guid.NewGuid().ToString();

                if (this.PPLineItemGrid.DisplayLayout.ValueLists.Exists(this.codeItemList))
                {
                    return;
                }

                ValueList objValueList = this.PPLineItemGrid.DisplayLayout.ValueLists.Add(this.codeItemList);

                if (this.pipeLineItemDataSet.F35055_ScheduleItemCode.Rows.Count > 0)
                {
                    for (int count = 0; count < this.pipeLineItemDataSet.F35055_ScheduleItemCode.Rows.Count; count++)
                    {
                        objValueList.ValueListItems.Add(this.pipeLineItemDataSet.F35055_ScheduleItemCode.Rows[count][this.pipeLineItemDataSet.F35055_ScheduleItemCode.ItemCodeIDColumn.ColumnName].ToString(), this.pipeLineItemDataSet.F35055_ScheduleItemCode.Rows[count][this.pipeLineItemDataSet.F35055_ScheduleItemCode.ItemCodeColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the code.
        /// </summary>
        private void LoadYesNo()
        {
            try
            {
                //// Iterates to Add DisplayMember and DisplayValue from Table
                this.yesNoList = System.Guid.NewGuid().ToString();

                ValueList objValueList = this.PPLineItemGrid.DisplayLayout.ValueLists.Add(this.yesNoList);

                if (this.commonData.ComboBoxDataTable.Rows.Count > 0)
                {
                    for (int count = 0; count < this.commonData.ComboBoxDataTable.Rows.Count; count++)
                    {
                        objValueList.ValueListItems.Add(this.commonData.ComboBoxDataTable.Rows[count][this.commonData.ComboBoxDataTable.KeyIdColumn.ColumnName].ToString(), this.commonData.ComboBoxDataTable.Rows[count][this.commonData.ComboBoxDataTable.KeyNameColumn.ColumnName].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the selected schedule line items XML.
        /// </summary>
        /// <returns>The selected schedule line items xml.</returns>
        private string GetSelectedPPLineItemsXml()
        {
            ////To Get SelectedScheduleLineItem
            string selectedScheduleLineItemsXml = string.Empty;
            F35055PPLineItemData selectedPPLineItemsDataSet = new F35055PPLineItemData();
            this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AcceptChanges();

            string filterCondition = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName + " = " + bool.TrueString;
            DataRow[] selectedScheduleLineItemRows = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Select(filterCondition);

            ////if item the is selected then it will convert 
            if (selectedScheduleLineItemRows.Length > 0)
            {
                selectedPPLineItemsDataSet.Merge(selectedScheduleLineItemRows);
                selectedScheduleLineItemsXml = TerraScanCommon.GetXmlString(selectedPPLineItemsDataSet.F35055_GetSchedlueLineItem);
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
            F35055PPLineItemData selectedPPLineItemsDataSet = new F35055PPLineItemData();

            string filterCondition = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName + "=" + bool.TrueString;
            this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AcceptChanges();
            DataRow[] selectedScheduleLineItemRows = this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Select(filterCondition);

            if (selectedScheduleLineItemRows.Length > 0)
            {
                selectedPPLineItemsDataSet.Merge(selectedScheduleLineItemRows);
                selectedPPLineItemsDataSet.F35055_GetSchedlueLineItem.Columns.Remove(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName);
                selectedPPLineItemsDataSet.F35055_GetSchedlueLineItem.Columns.Remove(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName);
                editedAndNewScheduleLineItemsXml = TerraScanCommon.GetXmlString(selectedPPLineItemsDataSet.F35055_GetSchedlueLineItem);
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
                if (this.PPLineItemGrid.ActiveRow != null)
                {
                    this.currentRowIndex = this.PPLineItemGrid.ActiveRow.Index;
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

                if (this.PPLineItemGrid.Rows.Count < 1)
                {
                    // For empty rows presents in Grid
                    this.PPLineItemGrid.Focus();
                }
                else if (this.currentRowIndex >= this.PPLineItemGrid.Rows.Count)
                {
                    // For last row focus
                    // After removal of some rows from Grid
                    //this.PPLineItemGrid.Focus();
                    this.ActiveControl = this.PPLineItemGrid;
                    this.currentRowIndex = this.PPLineItemGrid.Rows.Count - 1;
                    this.PPLineItemGrid.Rows[this.currentRowIndex].Activate();
                    this.activeCell = this.PPLineItemGrid.ActiveRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName];
                    this.PPLineItemGrid.ActiveCell = this.activeCell;
                    this.PPLineItemGrid.ActiveCell.Activate();
                }
                else
                {
                    //this.PPLineItemGrid.Focus();
                    this.ActiveControl = this.PPLineItemGrid;
                    this.PPLineItemGrid.Rows[this.currentRowIndex].Activate();
                    this.activeCell = this.PPLineItemGrid.ActiveRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName];
                    this.PPLineItemGrid.ActiveCell = this.activeCell;
                    this.PPLineItemGrid.ActiveCell.Activate();
                    //this.PPLineItemGrid.PerformAction(UltraGridAction.EnterEditMode);
                    //this.PPLineItemGrid.Rows[this.currentRowIndex].Selected = true;
                }

                // Set form master panel scroll position
                if (this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }

                this.pageLoadStatus = false;
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Scroll events

        /// <summary>
        /// Handles the Click event of the Scroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void Scroll_Click(object sender, ScrollEventArgs e)
        {
            try
            {
                // this.yaxisPoint = e.NewValue;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the Smartpart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Smartpart_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                ////set y-axis point position
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    // this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion scroll events

        #region Grid Events

        /// <summary>
        /// Handles the InitializeLayout event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                ////Customise the grid columns
                this.CustomizeGrid();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the InitializeTemplateAddRow event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            try
            {
                ////Get the active cell,active row.
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                this.activeRow = this.PPLineItemGrid.ActiveRow;

                ////Load the schedule item code column
                this.LoadCode();
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.codeItemList];
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                ////Load the table column
                this.LoadTableItem();
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.tableItemList];
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                //////Load the trend column
                this.commonData.LoadYesNoValue();
                this.LoadYesNo();
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.yesNoList];
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                ////Load the fuel column
                this.LoadFuel();
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.fuelItemList];
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                e.TemplateAddRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                ////set the cursor to the edited column
                if (this.activeRow != null && this.activeCell != null)
                {
                    ////sets the height of the form
                    //this.SetSmartPartHeight();

                    this.activeCell.Activate();
                    this.activeRow.Update();
                    this.PPLineItemGrid.PerformAction(UltraGridAction.EnterEditMode);
                    try
                    {
                        if (this.activeCell.Style != Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList && this.activeCell.IsInEditMode == true && this.activeCell.SelText.Length > 0)
                        {
                            this.activeCell.SelStart = this.activeCell.SelText.Length;
                        }
                    }
                    catch (Exception)
                    {
                        ////this will catch and throw the default exception 
                    }
                }

                ////Set scroll position
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    //this.yaxisPoint = this.yaxisPoint + 25;
                    // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellChange event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                ////Get the active cell and active row
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                this.activeRow = this.PPLineItemGrid.ActiveRow;

                if (this.activeRow != null && this.activeCell != null)
                {
                    if ((!this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName))
                       && (!this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName)))
                    {
                        ////set the edit mode
                        this.SetEditRecord();

                        // check for the IsEdited cell value for false and set the value as true
                        if (this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName].Value.Equals(false))
                        {
                            this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsEditedColumn.ColumnName].Value = true;
                        }

                        //// check for qnty cell empty if so assign the default value as 1
                        if (!this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName))
                        {
                            if (string.IsNullOrEmpty(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Text))
                            {
                                this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Value = 1;
                            }
                        }
                        ////Call the method to calculate the grid summaries
                        this.PPLineGridSummeries();
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
        /// Handles the Error event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.ErrorEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_Error(object sender, ErrorEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// Handles the CellDataError event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CellDataErrorEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_CellDataError(object sender, CellDataErrorEventArgs e)
        {
            e.RestoreOriginalValue = true;
            e.StayInEditMode = true;
        }

        /// <summary>
        /// Handles the InitializeRow event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.InitializeRowEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            try
            {
                if (e.Row != null)
                {
                    ////for new row if click the headercheck column then it should not create a new line.because of the here diabling the cell activation
                    if (string.IsNullOrEmpty(e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LineColumn.ColumnName].Value.ToString()))
                    {
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName].Activation = Activation.NoEdit;
                    }

                    if (this.pageLoadStatus || e.Row.IsAddRow)
                    {
                        if (this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count >= 0)
                        {
                            this.CellEditStatus(e.Row, true);
                        }
                        ////Load Item Combo
                        this.LoadCode();
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.codeItemList];
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                        ////Load Table Combo
                        this.LoadTableItem();
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.tableItemList];
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                        ////Load Fuel Combo
                        this.LoadFuel();
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.fuelItemList];
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Column.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                        ////which loads yes, no value to the ComboBoxDataTable
                        this.commonData.LoadYesNoValue();
                        this.LoadYesNo();
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].ValueList = this.PPLineItemGrid.DisplayLayout.ValueLists[this.yesNoList];
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
                        e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Band.Override.AllowUpdate = DefaultableBoolean.True;

                        /////Assigning Trend value to 0 and 1
                        if (e.Row.Index > 0)
                        {
                            if (!string.IsNullOrEmpty(this.PPLineItemGrid.Rows[e.Row.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Text.Trim())
                                   && !string.IsNullOrEmpty(this.PPLineItemGrid.Rows[e.Row.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Text.Trim())
                                    && !string.IsNullOrEmpty(this.PPLineItemGrid.Rows[e.Row.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Text.Trim())
                                    && !string.IsNullOrEmpty(this.PPLineItemGrid.Rows[e.Row.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Text.Trim())
                                    && !string.IsNullOrEmpty(this.PPLineItemGrid.Rows[e.Row.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Text.Trim()))
                            {
                                if (e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value.Equals("Yes"))
                                {
                                    e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 1;
                                }
                                else
                                {
                                    e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 0;
                                }

                                // update the current row after changing the trend column value
                                // e.Row.Update();
                            }
                        }
                        else if (e.Row.Index.Equals(0))
                        {
                            if (e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value.Equals("Yes"))
                            {
                                e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 1;
                            }
                            else
                            {
                                e.Row.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 0;
                            }

                            // update the current row after changing the trend column value
                            // e.Row.Update();
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
        /// Handles the AfterExitEditMode event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_AfterExitEditMode(object sender, EventArgs e)
        {
            try
            {
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                this.activeRow = this.PPLineItemGrid.ActiveRow;
                int itemid;
                int fuelid;
                int deprtableid;
                if (this.activeRow != null && this.activeCell != null && this.activeCell.DataChanged)
                {
                    ////update the current cell value
                    if (this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.AuditIDColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HorsePowerColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.LeasedReferenceColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ModelNumberColumn.ColumnName)
                        || this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.TractorTypeColumn.ColumnName))
                    {
                        this.activeCell.Value = this.activeCell.Text.Trim();
                        this.activeRow.Update();
                    }

                    ////For Item Code Combo
                    if (this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName))
                    {
                        ////call the method to check the entered value is exist or not
                        this.OnCodeComboItemChanged();
                        ////set the itemcodeid value
                        int.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeColumn.ColumnName].Value.ToString(), out itemid);
                        this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.ItemCodeIDColumn.ColumnName].Value = itemid.ToString();
                    }

                    ////For Depriciation table combo
                    if (this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName))
                    {
                        ////call the method to check the entered value is exist or not
                        this.OnTableComboItemChanged();
                        ////set the Depriciation table value
                        int.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprDescriptionColumn.ColumnName].Value.ToString(), out deprtableid);
                        this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DeprTableIDColumn.ColumnName].Value = deprtableid.ToString();
                        ////Calculate Value and factored value column
                        this.CalculateValueandFactoredValue();
                    }

                    ////Assigning IsTrend Column value
                    if (this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName))
                    {
                        ////set the istrend values as 0
                        if (this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Text.Equals("Yes"))
                        {
                            this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 1;
                        }
                        else
                        {
                            this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Value = 0;
                        }
                        ////Calculate Value and factored value column
                        this.CalculateValueandFactoredValue();
                    }

                    ////For Fuel combo
                    if (this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName))
                    {
                        ////call the method to check the entered value is exist or not
                        this.OnFuelComboItemChanged();
                        ////set the Fuel value
                        int.TryParse(this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelColumn.ColumnName].Value.ToString(), out fuelid);
                        this.activeRow.Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.FuelIDColumn.ColumnName].Value = fuelid.ToString();
                    }

                    if (this.activeCell.Column.Header.Caption.Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName)
                        || this.activeCell.Column.ToString().Equals(this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName))
                    {
                        ////Calculate Value and factored value column
                        this.CalculateValueandFactoredValue();
                    }

                    ////Call the method to calculate the grid summaries
                    this.PPLineGridSummeries();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeExitEditMode event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
        {
            try
            {
                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    ////validate all cells thru this method
                    this.ValidatePPLineGridValues(e);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                ////set the basePanelScrolled to false;
                this.basePanelScrolled = true;

                //((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                ////Get the y-axis point
                //if (this.basePanelScrolled)
                //{
                //    if (this.ParentForm != null &&  this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                //    {
                //        this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                //        this.basePanelScrolled = false;
                //    }
                //}
                //else
                //{
                this.basePanelScrolled = false;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeCellActivate event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.CancelableCellEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_BeforeCellActivate(object sender, CancelableCellEventArgs e)
        {
            try
            {
                ////get the active cell and active row
                this.activeCell = this.PPLineItemGrid.ActiveCell;
                this.activeRow = this.PPLineItemGrid.ActiveRow;

                if (this.activeRow != null)
                {
                    if (this.activeRow.Index > 0 && this.permissionFields.editPermission && this.formMasterPermissionEdit)
                    {
                        ////set the cell activation equal to no edit if all values or not filled
                        if (string.IsNullOrEmpty(this.PPLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.DescriptionColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.PPLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.QntyColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.PPLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.YearColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.PPLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.OriginalCostColumn.ColumnName].Text.Trim())
                            || string.IsNullOrEmpty(this.PPLineItemGrid.Rows[this.activeRow.Index - 1].Cells[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.IsTrendColumn.ColumnName].Text.Trim()))
                        {
                            this.CellEditStatus(this.activeRow, false);
                            this.PPLineItemGrid.Rows[this.activeRow.Index].Selected = true;
                        }
                        else
                        {
                            ////set the cell activation equal to edit if all values or filled
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
        /// Handles the BeforeHeaderCheckStateChanged event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeHeaderCheckStateChangedEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_BeforeHeaderCheckStateChanged(object sender, BeforeHeaderCheckStateChangedEventArgs e)
        {
            try
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View) &&
                    this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows.Count > 0)
                {
                    if (e.NewCheckState.Equals(CheckState.Checked))
                    {
                        foreach (DataRow scheduleItemRow in this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows)
                        {
                            if (!(bool)scheduleItemRow[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName])
                            {
                                scheduleItemRow[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName] = true;
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
                        foreach (DataRow scheduleItemRow in this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.Rows)
                        {
                            if ((bool)scheduleItemRow[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName])
                            {
                                scheduleItemRow[this.pipeLineItemDataSet.F35055_GetSchedlueLineItem.HeaderCheckColumn.ColumnName] = false;
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    ////To fire the esc functionality
                    if (e.KeyValue.Equals(27))
                    {
                        this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterRowActivate event of the PPLineItemGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PPLineItemGrid_AfterRowActivate(object sender, System.EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus)
                {
                    this.GetFormMaster_ScrollPosition();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Move and Delete Button Events
        /// <summary>
        /// Handles the Click event of the MoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scheduleId > 0)
                {
                    object[] optionalParameter;
                    optionalParameter = new object[] { this.scheduleId, this.GetSelectedPPLineItemsXml() };

                    ////Calling move form 2205
                    Form moveScheduleItemsForm = new Form();
                    moveScheduleItemsForm = this.form35055Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(2205, optionalParameter, this.form35055Controller.WorkItem);
                    if (moveScheduleItemsForm != null)
                    {
                        if (moveScheduleItemsForm.ShowDialog() == DialogResult.OK)
                        {
                            ////fill the grid
                            this.LoadPPLineItemGrid();

                            // Maintain form master scroll posion 
                            this.SetFormMaster_ScrollPosition();
                        }
                        else
                        {
                            this.ActiveControl = this.PPLineItemGrid;
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
                    if (!string.IsNullOrEmpty(this.GetSelectedPPLineItemsXml()))
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteSheduleLineItems"), SharedFunctions.GetResourceString("DeleteSheduleLineItemsItemsHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals(DialogResult.Yes))
                        {
                            // call the delete for selected schedule line items
                            this.form35055Controller.WorkItem.F35055_DeleteScheduleLineItem(this.scheduleId, this.GetSelectedPPLineItemsXml(), TerraScanCommon.UserId);

                            // fill the grid
                            this.LoadPPLineItemGrid();

                            // Maintain form master scroll posion 
                            this.SetFormMaster_ScrollPosition();
                        }
                        else
                        {
                            this.PPLineItemGrid.PerformAction(UltraGridAction.ActivateCell);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion
    }
}
