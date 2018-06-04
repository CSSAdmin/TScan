//--------------------------------------------------------------------------------------------
// <copyright file="F8058.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8058.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 22 May 06        KUPPUSAMY.B              Created
//*********************************************************************************/

namespace D8058
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F8058 class file
    /// </summary>
    [SmartPart]
    public partial class F8058 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Set Date Format
        /// </summary>
        private readonly string dateFormat = SharedFunctions.GetResourceString("DefaultAfdvtDateforamt");

        /// <summary>
        /// variable for F8058ResourcesConfigData
        /// </summary>        
        private F8058ResourcesConfigData form8058ResourcesConfigData;

        /// <summary>
        /// controller F9038
        /// </summary>
        private F8058Controller form8058Control;

        /// <summary>
        /// the current page mode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// Edit permission of FormMaster
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int equipmentId;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int applicationId;

        /// <summary>
        /// dateChanged
        /// </summary>
        private bool dateChanged;

        /// <summary>
        /// Local variable.
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Used to store tempGridRowId
        /// </summary>
        private int tempGridRowId;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8058"/> class.
        /// </summary>
        public F8058()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8058"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8058(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.Tag = formNo;
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.ResCfgSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ResCfgSecIndicatorPictureBox.Height, this.ResCfgSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            this.form8058ResourcesConfigData = new F8058ResourcesConfigData();
        }

        #endregion Constructor

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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets the form F8058 control.
        /// </summary>
        /// <value>The form F8058 control.</value>
        [CreateNew]
        public F8058Controller FormF8058Control
        {
            get { return this.form8058Control as F8058Controller; }
            set { this.form8058Control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        public TerraScanCommon.PageModeTypes PageMode
        {
            get { return this.pageMode; }
            set { this.pageMode = value; }
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
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    if (this.ResourceConfigDetailsGridView.OriginalRowCount > 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
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
        /// Event Subscription LoadSliceDetails
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The Event.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.CustomizeResourcesConfigGrid();
                    if (this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Count > 0)
                    {
                        this.flagFormLoad = true;
                        this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails;
                        this.flagFormLoad = false;
                    }

                    this.ResourceConfigDetailsGridView.Rows[0].Selected = true;
                    this.LoadResourcesConfigItems(0);
                    this.ResCfgSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ResCfgSecIndicatorPictureBox.Height, this.ResCfgSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, ThreadOption.UserInterface)]
        public void OnD9030_F9030_AlertDistinguishedSliceOnClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (eventArgs.Data.FormNo == this.masterFormNo && eventArgs.Data.FlagFormClose)
            {
                eventArgs.Data.FlagFormClose = this.CheckPageStatus(!eventArgs.Data.FlagForQueryEngine);
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

        #endregion Protected methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F8058 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8058_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.CustomizeResourcesConfigGrid();
                this.flagFormLoad = true;
                this.EquipmentNameTextBox.Focus();
                this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails;
                this.LoadResourcesConfigItems(0);
                if (this.ResourceConfigDetailsGridView.Rows.Count > 0)
                {
                    this.EquipmentNameTextBox.Focus();
                }
                this.flagFormLoad = false;
                this.applicationId = TerraScanCommon.ApplicationId;
                this.PurchaseDateMonthCalander.Visible = false;
                this.ControlLock(!this.PermissionFiled.editPermission);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the ResourceConfigDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ResourceConfigDetailsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.selectedRow = e.RowIndex;
            try
            {
                if (!this.flagFormLoad)
                {
                    if ((e.RowIndex >= 0) && (!this.flagFormLoad))
                    {
                        this.LoadResourcesConfigItems(e.RowIndex);
                        this.ResourceConfigDetailsGridView.Rows[e.RowIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the ResourceConfigDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ResourceConfigDetailsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.selectedRow = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PurchaseDateImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PurchaseDateImage_Click(object sender, EventArgs e)
        {
            try
            {
                this.PurchaseDateMonthCalander.Visible = true;
                this.PurchaseDateMonthCalander.ScrollChange = 1;

                //// Display the Calender control near the Calender Picture box.
                this.PurchaseDateMonthCalander.Left = this.PurchaseDatepanel.Left + this.PurchaseDateImage.Left + this.PurchaseDateImage.Width;
                this.PurchaseDateMonthCalander.Top = this.PurchaseDatepanel.Top + this.PurchaseDateImage.Top;
                this.PurchaseDateMonthCalander.Tag = this.PurchaseDateImage.Tag;
                this.PurchaseDateMonthCalander.Focus();

                if (!string.IsNullOrEmpty(this.PurchaseDateTextBox.Text.Trim()))
                {
                    this.PurchaseDateMonthCalander.SetDate(Convert.ToDateTime(this.PurchaseDateTextBox.Text.Trim()));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the NewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.ControlLock(!this.PermissionFiled.newPermission);
                this.ClearResourceConfigDetails();
                this.equipmentId = 0;
                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    this.ResourceConfigDetailsGridView.Enabled = false;
                    this.ResourcesGridViewpanel.Enabled = false;
                    this.ResourceConfigDetailsGridView.CurrentCell = null;
                    this.ResourceConfigDetailsGridView.Rows[0].Selected = false;
                    this.ResourceConfigDetailsGridView.Rows[0].Cells[0].Selected = false;
                }
                else
                {
                    this.ResourceConfigDetailsGridView.Enabled = true;
                    this.ResourcesGridViewpanel.Enabled = true;
                }

                this.SetPanelVisibility(true);
                this.NewButton.Enabled = false;
                this.EquipmentNameTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveResourceConfigDetails();
                this.ControlLock(!this.PermissionFiled.editPermission);
                this.ResourceConfigDetailsGridView.Enabled = true;
                this.ResourcesGridViewpanel.Enabled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DeleteResourceConfigDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Clear();
                this.form8058ResourcesConfigData = this.form8058Control.WorkItem.F8058_ListResourcesConfigDetails();
                this.flagFormLoad = true;
                this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
                this.flagFormLoad = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.ResourceConfigGridVscrollBar.Enabled = false;
                ////this.ResourceConfigDetailsGridView.Enabled = true;
                this.LoadResourcesConfigItems(0);
                this.ResourceConfigDetailsGridView.Enabled = true;
                this.ResourcesGridViewpanel.Enabled = true;
                if (this.ResourceConfigDetailsGridView.OriginalRowCount == 0)
                {
                    this.NewButton.Focus();
                }
                else
                {
                    this.ResourceConfigDetailsGridView.Focus();
                }
                this.ControlLock(!this.PermissionFiled.editPermission);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ResCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void ResCfgSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                ////this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D8058.F8058"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the ResCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ResCfgSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.ResourceCfgToolTip.SetToolTip(this.ResCfgSecIndicatorPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EquipmentNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AlltextBoxTexthanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                        this.ResourceConfigDetailsGridView.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the PurchaseDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PurchaseDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (this.dateChanged)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ActiveCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ActiveCheckBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the PurchaseDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void PurchaseDateMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSelectedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the PurchaseDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PurchaseDateMonthCalander_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSelectedDate(this.PurchaseDateMonthCalander.SelectionStart.ToString(this.dateFormat));
                    this.PurchaseDateMonthCalander.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PurchaseDateMonthCalander control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PurchaseDateMonthCalander_Leave(object sender, EventArgs e)
        {
            try
            {
                this.PurchaseDateMonthCalander.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Private methods

        /// <summary>
        /// Customizes the resources config grid.
        /// </summary>
        private void CustomizeResourcesConfigGrid()
        {
            DataGridViewColumnCollection resourceConfigColumns = this.ResourceConfigDetailsGridView.Columns;
            this.ResourceConfigDetailsGridView.AllowUserToResizeColumns = false;
            this.ResourceConfigDetailsGridView.AllowUserToResizeRows = false;
            this.ResourceConfigDetailsGridView.AutoGenerateColumns = false;
            this.ResourceConfigDetailsGridView.StandardTab = true;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentNameColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentNameColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.PurchaseDateColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.PurchaseDateColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.IdentificationNoColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.IdentificationNoColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.IsActiveColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.IsActiveColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.RateColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.RateColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.DescriptionColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.DescriptionColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.CommentColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.CommentColumn.ColumnName;
            this.ResourceConfigDetailsGridView.Columns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentIDColumn.ColumnName].DataPropertyName = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentIDColumn.ColumnName;

            resourceConfigColumns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentNameColumn.ColumnName].DisplayIndex = 0;
            resourceConfigColumns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.IdentificationNoColumn.ColumnName].DisplayIndex = 1;
            resourceConfigColumns[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.DescriptionColumn.ColumnName].DisplayIndex = 2;
            //// write code to access from db and assign values
            this.form8058ResourcesConfigData = this.form8058Control.WorkItem.F8058_ListResourcesConfigDetails();

            if (this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Count > 7)
            {
                this.ResourceConfigGridVscrollBar.Enabled = true;
                this.ResourceConfigGridVscrollBar.Visible = false;
            }
            else
            {
                this.ResourceConfigGridVscrollBar.Enabled = false;
                this.ResourceConfigGridVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Lists the receipt items.
        /// </summary>
        /// <param name="rowcount">The rowcount.</param>
        private void LoadResourcesConfigItems(int rowcount)
        {
            this.flagFormLoad = true;
            ////this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
            if (ResourceConfigDetailsGridView.OriginalRowCount > 0)
            {
                this.SetPanelVisibility(true);
                this.ResourceConfigDetailsGridView.Focus();
                this.ResourceConfigDetailsGridView.Rows[rowcount].Selected = true;
                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["EquipmentName"].Value != null)
                {
                    this.EquipmentNameTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["EquipmentName"].Value.ToString();
                }
                else
                {
                    this.EquipmentNameTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["PurchaseDate"].Value != null)
                {
                    this.PurchaseDateTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["PurchaseDate"].Value.ToString();
                }
                else
                {
                    this.PurchaseDateTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["IsActive"].Value.ToString().ToLower() == "true")
                {
                    this.ActiveCheckBox.Checked = true;
                }
                else
                {
                    this.ActiveCheckBox.Checked = false;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Rate"].Value != null)
                {
                    this.RateTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Rate"].Value.ToString();
                }
                else
                {
                    this.RateTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Description"].Value != null)
                {
                    this.DescriptionTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Description"].Value.ToString();
                }
                else
                {
                    this.DescriptionTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["IdentificationNo"].Value != null)
                {
                    this.IdentificationNumberTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["IdentificationNo"].Value.ToString();
                }
                else
                {
                    this.IdentificationNumberTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Comment"].Value != null)
                {
                    this.CommentTextBox.Text = this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["Comment"].Value.ToString();
                }
                else
                {
                    this.CommentTextBox.Text = string.Empty;
                }

                if (this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["EquipmentID"].Value != null)
                {
                    this.equipmentId = Convert.ToInt32(this.ResourceConfigDetailsGridView.Rows[rowcount].Cells["EquipmentID"].Value);
                }

                if (this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Count > 7)
                {
                    this.ResourceConfigGridVscrollBar.Enabled = true;
                    this.ResourceConfigGridVscrollBar.Visible = false;
                }
                else
                {
                    this.ResourceConfigGridVscrollBar.Enabled = false;
                    this.ResourceConfigGridVscrollBar.Visible = true;
                }
            }
            else
            {
                this.EquipmentNameTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
                this.IdentificationNumberTextBox.Text = string.Empty;
                this.PurchaseDateTextBox.Text = string.Empty;
                this.RateTextBox.Text = string.Empty;
                this.CommentTextBox.Text = string.Empty;
                this.ActiveCheckBox.Checked = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.SetPanelVisibility(false);
                this.ResourceConfigDetailsGridView.Rows[0].Selected = false;
                if (this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Count > 7)
                {
                    this.ResourceConfigGridVscrollBar.Enabled = true;
                    this.ResourceConfigGridVscrollBar.Visible = false;
                }
                else
                {
                    this.ResourceConfigGridVscrollBar.Enabled = false;
                    this.ResourceConfigGridVscrollBar.Visible = true;
                }
            }

            this.flagFormLoad = false;
        }

        /// <summary>
        /// Sets the visibility of control
        /// </summary>
        /// <param name="visibility">Visibile</param>
        private void SetPanelVisibility(bool visibility)
        {
            this.EquipmentNamePanel.Enabled = visibility;
            this.PurchaseDatepanel.Enabled = visibility;
            this.Activepanel.Enabled = visibility;
            this.Ratepanel.Enabled = visibility;
            this.IdentificationNumberpanel.Enabled = visibility;
            this.Descriptionpanel.Enabled = visibility;
            this.Commentpanel.Enabled = visibility;
        }

        /// <summary>
        /// Sets the selected date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSelectedDate(string dateSelected)
        {
            this.PurchaseDateMonthCalander.Tag = string.Empty;
            this.PurchaseDateMonthCalander.Focus();
            this.PurchaseDateMonthCalander.Text = dateSelected;
            this.PurchaseDateTextBox.Text = dateSelected;
            this.PurchaseDateTextBox.Focus();
            this.PurchaseDateMonthCalander.Visible = false;
        }

        /// <summary>
        /// Clears the resource config details.
        /// </summary>
        private void ClearResourceConfigDetails()
        {
            this.EquipmentNameTextBox.Text = string.Empty;
            this.PurchaseDateTextBox.Text = string.Empty;
            this.ActiveCheckBox.Checked = false;
            this.RateTextBox.Text = string.Empty;
            this.IdentificationNumberTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Clears the grid view.
        /// </summary>
        private void ClearGridView()
        {
            this.ResourceConfigDetailsGridView.Enabled = false;
            F8058ResourcesConfigData.F8058ListResourceConfigDetailsDataTable currentSource = (F8058ResourcesConfigData.F8058ListResourceConfigDetailsDataTable)this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
            currentSource.Rows.Clear();
            this.flagFormLoad = true;
            this.ResourceConfigDetailsGridView.DataSource = currentSource;
            this.flagFormLoad = false;
            this.ResourceConfigDetailsGridView.Rows[0].Selected = false;
            this.ResourceConfigGridVscrollBar.Enabled = false;
        }

        /// <summary>
        /// Saves the resource config details.
        /// </summary>
        private bool SaveResourceConfigDetails()
        {
            if (string.IsNullOrEmpty(this.EquipmentNameTextBox.Text.Trim()))
            {
                ////MessageBox.Show(SharedFunctions.GetResourceString("Missing Required Field"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.EquipmentNameTextBox.Focus();
                return false;
            }
            else
            {
                if ((string.IsNullOrEmpty(this.RateTextBox.Text.Trim())) || (this.RateTextBox.Text == "0.00"))
                {
                    ////MessageBox.Show(SharedFunctions.GetResourceString("Missing Required Field"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.RateTextBox.Focus();
                    return false;
                }
                else
                {
                    //// this.Cursor = Cursors.WaitCursor;
                    string resourceConfigxml = string.Empty;
                    F8058ResourcesConfigData resourcesConfigData = new F8058ResourcesConfigData();
                    F8058ResourcesConfigData.F8058ListResourceConfigDetailsRow resourceConfigDatarow = resourcesConfigData.F8058ListResourceConfigDetails.NewF8058ListResourceConfigDetailsRow();
                    resourceConfigDatarow.EquipmentID = this.equipmentId.ToString();
                    resourceConfigDatarow.EquipmentName = this.EquipmentNameTextBox.Text;
                    resourceConfigDatarow.PurchaseDate = this.PurchaseDateTextBox.Text;

                    resourceConfigDatarow.Rate = Convert.ToDecimal(this.RateTextBox.Text.ToString());
                    resourceConfigDatarow.IsActive = this.ActiveCheckBox.Checked;
                    resourceConfigDatarow.IdentificationNo = this.IdentificationNumberTextBox.Text;
                    resourceConfigDatarow.Description = this.DescriptionTextBox.Text;
                    resourceConfigDatarow.Comment = this.CommentTextBox.Text;
                    resourcesConfigData.F8058ListResourceConfigDetails.Rows.Add(resourceConfigDatarow);
                    resourceConfigxml = TerraScanCommon.GetXmlString(resourcesConfigData.F8058ListResourceConfigDetails);
                    int currentKey = this.form8058Control.WorkItem.F8058_InsertReosurcesConfigDetails(this.equipmentId, resourceConfigxml, this.applicationId, TerraScanCommon.UserId);
                    this.equipmentId = currentKey;
                    this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Clear();
                    this.form8058ResourcesConfigData = this.form8058Control.WorkItem.F8058_ListResourcesConfigDetails();
                    this.flagFormLoad = true;
                    this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
                    this.flagFormLoad = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.ResourceConfigGridVscrollBar.Enabled = true;
                    this.ResourceConfigDetailsGridView.Enabled = true;
                    if (this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Count > 7)
                    {
                        this.ResourceConfigGridVscrollBar.Enabled = true;
                        this.ResourceConfigGridVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.ResourceConfigGridVscrollBar.Enabled = false;
                        this.ResourceConfigGridVscrollBar.Visible = true;
                    }

                    if (this.GetRowIndex(currentKey.ToString()) >= 0)
                    {
                        this.LoadResourcesConfigItems(this.tempGridRowId);
                        TerraScanCommon.SetDataGridViewPosition(this.ResourceConfigDetailsGridView, this.tempGridRowId);
                    }
                }
                return true;
            }
            return true;
        }

        /// <summary>
        /// Deletes the resource config details.
        /// </summary>
        private void DeleteResourceConfigDetails()
        {
            string errorMessage = string.Empty;
            this.equipmentId = Convert.ToInt32(this.ResourceConfigDetailsGridView.Rows[this.selectedRow].Cells["EquipmentID"].Value);
            int deletedRowID = this.form8058Control.WorkItem.F8058_DeleteResourcesConfigDetails(this.equipmentId, TerraScanCommon.UserId);

            this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Rows.Clear();
            this.form8058ResourcesConfigData = this.form8058Control.WorkItem.F8058_ListResourcesConfigDetails();
            this.flagFormLoad = true;
            this.ResourceConfigDetailsGridView.DataSource = this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.Copy();
            this.flagFormLoad = false;

            if (deletedRowID == 1)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.LoadResourcesConfigItems(0);
            }
            else
            {
                errorMessage = "Cannot Delete selected record";
                MessageBox.Show(errorMessage, "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            /* Commented by Latha */
            ////this.LoadResourcesConfigItems(0);
            ////this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);            
        }

        /// <summary>
        /// Sets the buttons.
        /// </summary>
        /// <param name="buttonActionMode">The button action mode.</param>
        private void SetButtons(TerraScanCommon.ButtonActionMode buttonActionMode)
        {
            switch (buttonActionMode)
            {
                case TerraScanCommon.ButtonActionMode.CancelMode:
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.NewButton.Enabled = true;
                        }
                        else
                        {
                            this.NewButton.Enabled = false;
                        }

                        this.CancelButton.Enabled = false;
                        if (this.slicePermissionField.deletePermission)
                        {
                            this.DeleteButton.Enabled = true;
                        }
                        else
                        {
                            this.DeleteButton.Enabled = false;
                        }

                        this.SaveButton.Enabled = false;
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NewMode:
                    {
                        this.NewButton.Enabled = false;
                        this.CancelButton.Enabled = true;
                        this.DeleteButton.Enabled = false;
                        this.SaveButton.Enabled = true;
                        break;
                    }

                case TerraScanCommon.ButtonActionMode.EditMode:
                    {
                        if (this.slicePermissionField.editPermission)
                        {
                            this.NewButton.Enabled = false;
                            this.CancelButton.Enabled = true;
                            this.DeleteButton.Enabled = false;
                            this.SaveButton.Enabled = true;
                        }

                        break;
                    }

                case TerraScanCommon.ButtonActionMode.NullRecordMode:
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.NewButton.Enabled = true;
                        }
                        else
                        {
                            this.NewButton.Enabled = false;
                        }

                        this.CancelButton.Enabled = false;
                        this.DeleteButton.Enabled = false;
                        this.SaveButton.Enabled = false;
                        break;
                    }
            }
        }

        /// <summary>
        /// Gets the index of the row.
        /// </summary>
        /// <param name="searchRowId">The search row id.</param>
        /// <returns>searchRowId</returns>
        private int GetRowIndex(string searchRowId)
        {
            this.tempGridRowId = -1;

            for (int i = 0; i < this.ResourceConfigDetailsGridView.Rows.Count; i++)
            {
                if ((this.ResourceConfigDetailsGridView.Rows[i].Cells[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentIDColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.ResourceConfigDetailsGridView.Rows[i].Cells[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentIDColumn.ColumnName].Value.ToString())))
                {
                    if ((this.ResourceConfigDetailsGridView.Rows[i].Cells[this.form8058ResourcesConfigData.F8058ListResourceConfigDetails.EquipmentIDColumn.ColumnName].Value.ToString() == searchRowId))
                    {
                        return this.tempGridRowId = i;
                    }
                }
            }

            return this.tempGridRowId;
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLock)
        {
            this.EquipmentNameTextBox.LockKeyPress = controlLock;
            this.PurchaseDateTextBox.LockKeyPress = controlLock;
            this.PurchaseDateImage.Enabled = !controlLock;
            this.ActiveCheckBox.Enabled = !controlLock;
            this.RateTextBox.LockKeyPress = controlLock;
            this.IdentificationNumberTextBox.LockKeyPress = controlLock;
            this.DescriptionTextBox.LockKeyPress = controlLock;
            this.CommentTextBox.LockKeyPress = controlLock;
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - for continuing/false - leave unsaved changes</returns>
        private bool CheckPageStatus(bool onclose)
        {
            DialogResult dialogResult;
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Resources Configuration ", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SaveResourceConfigDetails())
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
                    ////this.CancelButton_Click();
                    return true;
                }

                return false;
            }

            return true;
        }
        #endregion Private methods
    }
}
