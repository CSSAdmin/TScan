namespace D35060
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;

    /// <summary>
    /// F35060 Class
    /// </summary>
    [SmartPart]
    public partial class F35060 : BaseSmartPart
    {

        #region Variable

        /// <summary>
        /// Master Form number
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Current Key Id[EventId] of the form
        /// </summary>
        private int keyId;

        /// <summary>
        /// Slice Permission for the form
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Form master edit permission
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Page Mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// F35060 Controller
        /// </summary>
        private F35060Controller form35060Controller;

        /// <summary>
        /// DataTable
        /// </summary>
        private F35060ScheduleItemCodeData scheduleItemCodeTable = new F35060ScheduleItemCodeData();

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        private ContextMenuStrip selectedItemMenuStrip = new ContextMenuStrip();

        /// <summary>
        /// Grid current Row index
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// used to keep trakc of SelectedRateItem
        /// </summary>
        private BindingSource itemCodeSource = new BindingSource();

        #endregion Variable

        #region Constructor

        public F35060()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F35060"/>
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35060(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.ItemCodePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ItemCodePictureBox.Height, this.ItemCodePictureBox.Width, tabText, red, green, blue);
        }

        #endregion Constructor

        #region Event Publication

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

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// For F35060Control
        /// </summary>
        [CreateNew]
        public F35060Controller Form35060Controller
        {
            get { return this.form35060Controller as F35060Controller; }
            set { this.form35060Controller = value; }
        }

        #endregion Property

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
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                //this.ClearControls();
                //this.LockControls(false);
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                //this.ClearControls();
                //this.LockControls(true);
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
                // this.LockControls(false);
            }
            else
            {
                // this.LockControls(true);
            }

            // this.ClearControls();
            this.LoadItemCodeDetails();
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
                 || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
                || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
            {
                this.SaveItemCodeDetails();
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
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    // this.LockControls(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    if (this.scheduleItemCodeTable.GetScheduleItemCode.Rows.Count > 0)
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
        /// Event Subscription for delete slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.deletePermission)
            {
                //this.Cursor = Cursors.WaitCursor;
                ////this.form29650Controller.WorkItem.DeleteExemptionDetails(this.keyId, this.exemptionEventId, TerraScanCommon.UserId);
                //SliceFormCloseAlert sliceFormCloseAlert;
                //sliceFormCloseAlert.FormNo = this.masterFormNo;
                //sliceFormCloseAlert.FlagFormClose = false;
                //this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                //this.Cursor = Cursors.Default;
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
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    // this.flagLoadOnProcess = true;
                    //this.LockControls(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    this.LoadItemCodeDetails();
                    //this.flagLoadOnProcess = false;
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

        #region Form load

        /// <summary>
        /// Handles the Load event of the F35060 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F35060_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeGrid();
                this.ItemCodePictureBox.SendToBack();
                this.LoadItemCodeDetails();

                // Assiging menu value for Contextmenustrip
                this.selectedItemMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
                this.selectedItemMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
                this.selectedItemMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedItemMenuStrip_ItemClicked);
                this.selectedItemMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedItemMenuStrip_Closed);
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        #endregion Form Load

        #region Events

        #region Enable Edit

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion Enable Edit

        #region PictureBox Events

        /// <summary>
        /// Handles the Click event of the ItemCodePictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ItemCodePictureBox_Click(object sender, EventArgs e)
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

        private void ItemCodePictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ItemCodeToolTip.SetToolTip(this.ItemCodePictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion PictureBox Events

        #region Grid Events

        /// <summary>
        /// Handles the CellEndEdit event of the ItemCodeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ItemCodeGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Add New Row
                if (e.RowIndex.Equals(this.ItemCodeGridView.Rows.Count - 1))
                {
                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit)
                        && (!string.IsNullOrEmpty(this.ItemCodeGridView.Rows[e.RowIndex].Cells[this.ItemCode.Name].Value.ToString().Trim())
                        || !string.IsNullOrEmpty(this.ItemCodeGridView.Rows[e.RowIndex].Cells[this.Abstract.Name].Value.ToString().Trim())
                        || !string.IsNullOrEmpty(this.ItemCodeGridView.Rows[e.RowIndex].Cells[this.Description.Name].Value.ToString().Trim())))
                    {
                        this.scheduleItemCodeTable.GetScheduleItemCode.AddGetScheduleItemCodeRow(
                                             this.scheduleItemCodeTable.GetScheduleItemCode.NewGetScheduleItemCodeRow());

                        this.ItemCodeScroll.Visible = false;
                    }
                }

                this.ItemCodeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                if (string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCodeID.Name, e.RowIndex].Value.ToString())
                      || this.ItemCodeGridView[this.ItemCodeID.Name, e.RowIndex].Value.ToString().Equals("0"))
                {
                    if (string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCode.Name, e.RowIndex].Value.ToString()))
                    {
                        this.ItemCodeGridView[this.ItemCodeID.Name, e.RowIndex].Value = DBNull.Value;
                    }
                    else
                    {
                        this.ItemCodeGridView[this.ItemCodeID.Name, e.RowIndex].Value = 0;
                    }
                }

                //if (!string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCode.Name, e.RowIndex].Value.ToString()))
                //{

                //    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                //    {
                //        this.ItemCodeGridView.AllowSorting = false;
                //        int itemFound = this.itemCodeSource.Find("ItemCode", selectedRateItem);
                //        TerraScanCommon.SetDataGridViewPosition(this.ItemCodeGridView, itemFound);
                //        //TerraScanCommon.SetDataGridViewPosition(this.ItemCodeGridView, this.ItemCodeGridView.CurrentRowIndex);
                //    }
                //}

                // this.ItemCodeGridView.Columns[this.Description.Name].Width = 393;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ItemCodeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ItemCodeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46 && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.deletePermission)
                {
                    // Delete
                    this.form35060Controller.WorkItem.DeleteScheduleItemCodes(this.CreateItemCodeTable(), TerraScanCommon.UserId);
                    this.LoadItemCodeDetails();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the ItemCodeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> 
        ///                             instance containing the event data.</param>
        private void ItemCodeGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && this.slicePermissionField.deletePermission)
                {
                    if (this.ItemCodeGridView.CurrentRow.Index >= 0)// && this.ItemCodeGridView.Rows[this.ItemCodeGridView.CurrentRow.Index].Selected)
                    {
                        if (this.ItemCodeGridView.CurrentRow.Selected)
                        {
                            if (!string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCodeID.Name, this.ItemCodeGridView.CurrentRow.Index].Value.ToString().Trim())
                                                        && this.pageMode == TerraScanCommon.PageModeTypes.View)
                            {
                                this.selectedItemMenuStrip.Show(this.ItemCodeGridView, new Point(e.X, e.Y));
                            }
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
        /// Handles the EditingControlShowing event of the ItemCodeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> 
        ///                                                         instance containing the event data.</param>
        private void ItemCodeGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.ItemCodeGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
                this.ItemCodeGridView.AllowSorting = false;
                //TerraScanCommon.SetDataGridViewPosition(this.ItemCodeGridView, this.ItemCodeGridView.CurrentRowIndex);
                // this.ItemCodeGridView.ClearSorting();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Grid Events

        #region Menu Strip Events

        /// <summary>
        /// Handles the Closed event of the SelectedItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/>
        ///                                               instance containing the event data.</param>
        private void SelectedItemMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            try
            {
                if (this.currentRowIndex > 0)
                {
                    // this.ItemCodeGridView.Rows[this.currentRowIndex].DefaultCellStyle.BackColor = this.rowBackColor;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ItemClicked event of the SelectedItemMenuStrip control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs"/> 
        ///                                            instance containing the event data.</param>
        private void SelectedItemMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (this.currentRowIndex >= 0)
                {
                    if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
                    {
                        this.selectedItemMenuStrip.Visible = false;
                        if (!string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCodeID.Name, this.currentRowIndex].Value.ToString().Trim())
                            && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                        {
                            this.form35060Controller.WorkItem.DeleteScheduleItemCodes(this.CreateItemCodeTable(), TerraScanCommon.UserId);
                            this.LoadItemCodeDetails();
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        private void ItemCodeGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.SetReadOnly(e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        private void ItemCodeGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (this.ItemCodeGridView.Rows[e.RowIndex].Selected)
                //{
                //    this.ItemCodeGridView.Rows[e.RowIndex].Selected = false;
                //}
                //else
                //{
                //    this.ItemCodeGridView.Rows[e.RowIndex].Selected = true;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #endregion Events

        #region Methods

        #region CustomizeGridView

        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.ItemCodeGridView.AutoGenerateColumns = false;
            this.ItemCodeGridView.PrimaryKeyColumnName = this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName;

            // Set DataProperty Name
            DataGridViewColumnCollection columns = this.ItemCodeGridView.Columns;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName].DataPropertyName
                                             = this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeColumn.ColumnName].DataPropertyName
                                             = this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeColumn.ColumnName;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.AbstractColumn.ColumnName].DataPropertyName
                                             = this.scheduleItemCodeTable.GetScheduleItemCode.AbstractColumn.ColumnName;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.DescriptionColumn.ColumnName].DataPropertyName
                                             = this.scheduleItemCodeTable.GetScheduleItemCode.DescriptionColumn.ColumnName;

            // Set Visible Position
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName].DisplayIndex = 0;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeColumn.ColumnName].DisplayIndex = 1;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.AbstractColumn.ColumnName].DisplayIndex = 2;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.DescriptionColumn.ColumnName].DisplayIndex = 3;
            this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.DefaultValue = 0;
            columns[this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName].DefaultCellStyle.NullValue = 0;
        }

        #endregion CustomizeGridView

        #region Load ItemCode

        /// <summary>
        /// Loads the item code details.
        /// </summary>
        private void LoadItemCodeDetails()
        {
            this.scheduleItemCodeTable = this.form35060Controller.WorkItem.GetScheduleItemCodes();

            try
            {
                this.ItemCodeGridView.DataSource = this.scheduleItemCodeTable.GetScheduleItemCode.DefaultView;
                this.itemCodeSource.DataSource = this.scheduleItemCodeTable.GetScheduleItemCode;
            }
            catch (Exception ex)
            {
            }

            // Enable/Disable ScrollBar
            if (this.ItemCodeGridView.OriginalRowCount > this.ItemCodeGridView.NumRowsVisible)
            {
                this.ItemCodeScroll.Visible = false;
            }
            else
            {
                this.ItemCodeScroll.Visible = true;
            }
            this.ItemCodeGridView.AllowSorting = true;
        }

        #endregion Load ItemCode

        #region Validation

        private SliceValidationFields CheckErrors(int formNo)
        {
            this.scheduleItemCodeTable.GetScheduleItemCode.AcceptChanges();
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            // Filter for unentered item code colimn

            string formulaString = " (" + this.scheduleItemCodeTable.GetScheduleItemCode.AbstractColumn.ColumnName + " <> '' or "
                                   + this.scheduleItemCodeTable.GetScheduleItemCode.DescriptionColumn.ColumnName + " <> '' or  "
                                   + this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeIDColumn.ColumnName + " > 0) and "
                                   + this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeColumn.ColumnName + " IS NULL or "
                                   + this.scheduleItemCodeTable.GetScheduleItemCode.ItemCodeColumn.ColumnName + "= ''";
            try
            {
                DataRow[] invalidRows = this.scheduleItemCodeTable.GetScheduleItemCode.Select(formulaString);

                if (invalidRows.Length > 0)
                {
                    sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                    sliceValidationFields.RequiredFieldMissing = true;
                    return sliceValidationFields;
                }
                else
                {
                    sliceValidationFields.ErrorMessage = string.Empty;
                    sliceValidationFields.RequiredFieldMissing = false;
                }
            }
            catch (Exception ex)
            {
            }

            return sliceValidationFields;
        }

        #endregion Validation

        #region Save ItemCode

        /// <summary>
        /// Saves the item code details.
        /// </summary>
        private void SaveItemCodeDetails()
        {
            form35060Controller.WorkItem.SaveScheduleItemCodes(TerraScanCommon.GetXmlString(this.scheduleItemCodeTable.GetScheduleItemCode),
                                                               TerraScanCommon.UserId);
        }

        #endregion Save ItemCode

        #region Read Only

        /// <summary>
        /// Sets the read only.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetReadOnly(int rowIndex)
        {
            if (rowIndex > 0)
            {
                if (string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCode.Name, rowIndex - 1].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ItemCodeGridView[this.ItemCode.Name, rowIndex].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ItemCodeGridView[this.Abstract.Name, rowIndex].Value.ToString().Trim())
                    && string.IsNullOrEmpty(this.ItemCodeGridView[this.Description.Name, rowIndex].Value.ToString().Trim()))
                {
                    this.ItemCodeGridView.Rows[rowIndex].ReadOnly = true;
                    this.ItemCodeGridView.Rows[rowIndex].Selected = true;
                    this.ItemCodeGridView[this.ItemCode.Name, rowIndex].Value = null;
                }
                else
                {
                    this.ItemCodeGridView.Rows[rowIndex].ReadOnly = false;
                }
            }
        }

        #endregion Read Only

        #region ItemCode Table

        private string CreateItemCodeTable()
        {
            DataTable itemTable = new DataTable();
            DataGridViewRow[] selectedRows = new DataGridViewRow[this.ItemCodeGridView.SelectedRows.Count];
            this.ItemCodeGridView.SelectedRows.CopyTo(selectedRows, 0);

            itemTable.Columns.Add("ItemCodeID", typeof(System.String));

            foreach (DataGridViewRow dataRow in selectedRows)
            {
                DataRow newRow = itemTable.NewRow();
                newRow["ItemcodeID"] = dataRow.Cells["ItemCodeID"].Value.ToString();
                itemTable.Rows.Add(newRow);
            }

            if (itemTable.Rows.Count > 0)
            {
                return TerraScanCommon.GetXmlString(itemTable);
            }

            return null;
        }

        #endregion ItemCode Table

        private void ItemCodeGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion Methods

    }
}
