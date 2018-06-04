//--------------------------------------------------------------------------------------------
// <copyright file="F8062.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8062.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 May 06        JYOTHI              Created
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
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;
    using Infrastructure.Interface;

    /// <summary>
    /// F8062 class file
    /// </summary>
    [SmartPart]
    public partial class F8062 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// materialDetailsDataSet variable is used to get the details of material Details.
        /// </summary>
        private F8062ComponentsConfigData componentsConfigDataSet = new F8062ComponentsConfigData();

        /// <summary>
        /// partTypeDataTable variable is used to get the details of part type.
        /// </summary>
        private F8062ComponentsConfigData.ListFeatureClassDataTable featureClassDataTable = new F8062ComponentsConfigData.ListFeatureClassDataTable();

        /// <summary>
        /// form8062Control Controller
        /// </summary>
        private F8062Controller form8062Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// Used to store actualMaterialDetailsRowsCount
        /// </summary>
        private int actualMaterialDetailsRowsCount;

        /// <summary>
        /// Form Number of the masterFormNo
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Edit permission of FormMaster
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// Used to store the form8062Controller
        /// </summary>
        private F8062Controller form8062Controller;

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8062"/> class.
        /// </summary>
        public F8062()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8104"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F8062(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.ComponentsCfgSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.ComponentsCfgSecIndicatorPictureBox.Width, tabText, red, green, blue);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// When a record is added in this local grid, the data in F8046 will get refreshed
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_MaterialFooterCountRefresh, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_MaterialFooterCountRefresh;

        /////// <summary>
        /////// event publication for PageStatusActivated
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8062 control.
        /// </summary>
        /// <value>The F8062 control.</value>
        [CreateNew]
        public F8062Controller FormF8062Control
        {
            get { return this.form8062Controller as F8062Controller; }
            set { this.form8062Controller = value; }
        }
        #endregion

        #region Protected Methods        

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

        /// <summary>
        /// Raises the form slice_ material footer count refresh event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnFormSlice_MaterialFooterCountRefresh(EventArgs eventArgs)
        {
            if (this.FormSlice_MaterialFooterCountRefresh != null)
            {
                this.FormSlice_MaterialFooterCountRefresh(this, eventArgs);
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
        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
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

                    this.PopulateComponentsConfigDetails();
                    this.ComponentsConfigGridView.ReadOnly = (!this.slicePermissionField.editPermission);
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
        }

        ////    /// <summary>
        /////// Checks the page status.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The e.</param>
        ////[EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        ////public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        ////{
        ////    if (this.CheckPageStatus(false))
        ////    {
        ////        this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
        ////    }
        ////}

        /////// <summary>
        /////// Gets the form status.
        /////// </summary>
        /////// <param name="sender">The sender.</param>
        /////// <param name="e">The e.</param>
        ////[EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        ////public void GetFormStatus(object sender, DataEventArgs<string> e)
        ////{
        ////    if (e.Data == this.GetType().Name)
        ////    {
        ////        this.form8062Controller.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
        ////    }
        ////}

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Load event of the F8062 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8062_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                ////this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);
                this.CustomizeComponentsCfgGridView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.ComponentsConfigGridView.OriginalRowCount == 0)
                {
                    this.ComponentsConfigGridView.RemoveDefaultSelection = true;
                }
                else
                {
                    this.ComponentsConfigGridView.RemoveDefaultSelection = false;
                }
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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

        /// <summary>
        /// Handles the Click event of the DeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ComponentsConfigGridView.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = this.ComponentsConfigGridView.SelectedRows[0].Index;

                    if (this.slicePermissionField.deletePermission)
                    {
                        if (!string.IsNullOrEmpty(this.ComponentsConfigGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()))
                        {
                            if (this.form8062Controller.WorkItem.F8062_DeleteComponentsConfiguration(Convert.ToInt32(this.ComponentsConfigGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()), TerraScanCommon.UserId) == 0)
                            {
                                this.PopulateComponentsConfigDetails();
                                this.FormSlice_MaterialFooterCountRefresh(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Cannot Delete selected record", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    this.ComponentsConfigGridView.ReadOnly = (!this.slicePermissionField.editPermission);
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
                ////this.CancelButton_Click();
                if (this.ComponentsConfigGridView.CurrentCell != null)
                {
                    this.ComponentsConfigGridView.CurrentCell.Selected = true;
                }
                ////this.EnableConfigGridSorting();
                this.PopulateComponentsConfigDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            ////if (this.ComponentsConfigGridView.CurrentCell != null)
            ////{
            ////    this.ComponentsConfigGridView.CurrentCell.Selected = true;
            ////}

            ////this.PopulateComponentsConfigDetails();
            ////this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            ////this.SaveComponentsConfig(false);
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (ComponentsConfigGridView.DataSource != null)
                {
                    DataRow[] tempRowCollection;

                    this.componentsConfigDataSet.ListComponentsConfiguration.AcceptChanges();

                    tempRowCollection = this.componentsConfigDataSet.ListComponentsConfiguration.Select("((FeatureClassID > 0) AND (Description IS NULL OR Description = '')) OR ((FeatureClassID IS NULL) AND (Description IS NOT NULL))");

                    if (tempRowCollection.Length == 0)
                    {
                        string componentDetails = string.Empty;
                        DataTable dt = ((DataView)this.ComponentsConfigGridView.DataSource).Table;
                        componentDetails = Utility.GetXmlString(dt);
                        this.form8062Controller.WorkItem.F8062_SaveComponentsConfiguration(componentDetails, TerraScanCommon.UserId);
                        this.PopulateComponentsConfigDetails();
                        this.FormSlice_MaterialFooterCountRefresh(sender, e);
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SaveButton.Enabled = false;
                        this.ComponentsCancelButton.Enabled = false;
                        this.ComponentsConfigGridView.TabStop = true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), ConfigurationWrapper.ApplicationSave.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                ////this.EnableConfigGridSorting();
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

        /// <summary>
        /// Handles the Click event of the ComponentsCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentsCfgSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the ComponentsCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentsCfgSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.ComponentCfgToolTip.SetToolTip(this.ComponentsCfgSecIndicatorPictureBox, Utility.GetFormNameSpace(this.Name));
        }

        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the KeyDown event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_KeyDown(object sender, KeyEventArgs e)
        {
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.ComponentsConfig_SelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.ComponentsConfig_SelectionChangeCommitted);
                }

                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.ComponentsConfigGridControl_TextChanged);
                    e.Control.Validating -= new CancelEventHandler(this.ComponentsConfigGridControl_Validating);
                    e.Control.Validating += new CancelEventHandler(this.ComponentsConfigGridControl_Validating);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ComponentsConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentsConfig_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the ComponentsConfigGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridControl_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.ComponentsConfigGridView.EditingControl.TextChanged -= new EventHandler(this.ComponentsConfigGridControl_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the ComponentsConfigGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridControl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////calculating related values for new values
                if (this.slicePermissionField.newPermission)
                {
                    this.CalculateComponentItemCount();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.ComponentsConfigGridView.Rows[(e.RowIndex - 1)].Cells["Description"].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < this.ComponentsConfigGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < this.ComponentsConfigGridView.RowCount; i++)
                            {
                                if (this.ComponentsConfigGridView.Rows[i].Cells["Description"].Value != null && !String.IsNullOrEmpty(this.ComponentsConfigGridView.Rows[i].Cells["Description"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                                this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                                this.ComponentsConfigGridView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                                this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                                this.ComponentsConfigGridView.Rows[e.RowIndex].Selected = true;
                            }
                        }
                        else
                        {
                            this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                            this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                            this.ComponentsConfigGridView.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                        this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                        this.ComponentsConfigGridView.Rows[e.RowIndex].Selected = true;
                    }
                }
                //// Need to Get Confirmation
                if (e.RowIndex == 0)
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView.Rows[e.RowIndex].Selected = true;
                }

                ////this.currentRowIndex = e.RowIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Only paint if desired, formattable column

            ////if (e.ColumnIndex == this.FeatureClass.Index)
            ////{
            ////    if (e.RowIndex < 0)
            ////    {
            ////        return;
            ////    }

            ////    if (this.ComponentsConfigGridView[this.ComponentID.Index, e.RowIndex].Value == null || String.IsNullOrEmpty(this.ComponentsConfigGridView[this.ComponentID.Index, e.RowIndex].Value.ToString()))
            ////    {
            ////        this.Description.ReadOnly = !this.slicePermissionField.newPermission;
            ////        this.FeatureClass.ReadOnly = !this.slicePermissionField.newPermission;
            ////    }
            ////    else
            ////    {
            ////        this.Description.ReadOnly = !this.slicePermissionField.editPermission;
            ////        this.FeatureClass.ReadOnly = !this.slicePermissionField.editPermission;
            ////    }
            ////}
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.ComponentsConfigGridView.OriginalRowCount == 1)
            {
                ////this.ComponentsConfigGridView.Rows[0].Selected = false;
                this.ComponentsConfigGridView.CurrentCell = null;
            }
            else
            {
                ////this.ComponentsConfigGridView.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the ComponentsConfigGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ComponentsConfigGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.ComponentsConfigGridView.OriginalRowCount)
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                }
            }

            if (e.RowIndex >= 0 && !this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.ComponentsConfigGridView.OriginalRowCount)
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                }
            }

            if (e.RowIndex >= 0 && this.PermissionFiled.newPermission && !this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.ComponentsConfigGridView.OriginalRowCount)
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                }
            }

            if (e.RowIndex >= 0 && !this.PermissionFiled.newPermission && !this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.ComponentsConfigGridView.OriginalRowCount)
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = false;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.ComponentsConfigGridView["FeatureClass", e.RowIndex].ReadOnly = true;
                    this.ComponentsConfigGridView["Description", e.RowIndex].ReadOnly = true;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the components config.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>boolean value</returns>
        private bool SaveComponentsConfig(bool onclose)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (ComponentsConfigGridView.DataSource != null)
                {
                    string componentDetails = string.Empty;
                    DataTable dt = ((DataView)this.ComponentsConfigGridView.DataSource).Table;
                    componentDetails = Utility.GetXmlString(dt);
                    this.form8062Controller.WorkItem.F8062_SaveComponentsConfiguration(componentDetails, TerraScanCommon.UserId);
                    this.PopulateComponentsConfigDetails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SaveButton.Enabled = false;
                    this.ComponentsCancelButton.Enabled = false;
                    this.ComponentsConfigGridView.TabStop = true;

                    if (onclose)
                    {
                        return true;
                    }
                }

                return true;
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Customizes the components CFG grid view.
        /// </summary>
        private void CustomizeComponentsCfgGridView()
        {
            this.ComponentsConfigGridView.AutoGenerateColumns = false;
            ////DataGridViewColumnCollection columns = this.ComponentsConfigGridView.Columns;

            ////columns["Description"].DataPropertyName = this.ComponentsConfigDataSet.ListComponentsConfiguration.DescriptionColumn.ColumnName;
            ////columns["FeatureClass"].DataPropertyName = this.ComponentsConfigDataSet.ListComponentsConfiguration.FeatureClassIDColumn.ColumnName;
            ////columns["ComponentID"].DataPropertyName = this.ComponentsConfigDataSet.ListComponentsConfiguration.ComponentIDColumn.ColumnName;

            ////columns["Description"].DisplayIndex = 0;
            ////columns["FeatureClass"].DisplayIndex = 1;
            ////columns["ComponentID"].DisplayIndex = 2;

            this.Description.DataPropertyName = this.componentsConfigDataSet.ListComponentsConfiguration.DescriptionColumn.ColumnName;
            this.FeatureClass.DataPropertyName = this.componentsConfigDataSet.ListComponentsConfiguration.FeatureClassIDColumn.ColumnName;
            this.ComponentID.DataPropertyName = this.componentsConfigDataSet.ListComponentsConfiguration.ComponentIDColumn.ColumnName;
            this.ComponentsConfigGridView.PrimaryKeyColumnName = "ComponentID";

            this.Description.DisplayIndex = 0;
            this.FeatureClass.DisplayIndex = 1;
            this.ComponentID.DisplayIndex = 2;

            this.ComponentsConfigGridView.DataSource = this.componentsConfigDataSet.ListComponentsConfiguration;

            this.featureClassDataTable = this.form8062Controller.WorkItem.F8062_ListFeatureClass(TerraScanCommon.ApplicationId).ListFeatureClass;

            (this.FeatureClass as DataGridViewComboBoxColumn).DisplayMember = this.featureClassDataTable.FeatureClassColumn.ColumnName;
            (this.FeatureClass as DataGridViewComboBoxColumn).ValueMember = this.featureClassDataTable.FeatureClassIDColumn.ColumnName;
            (this.FeatureClass as DataGridViewComboBoxColumn).DataSource = this.featureClassDataTable;

            ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).DataSource = this.featureClassDataTable;
            ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).DisplayMember = this.featureClassDataTable.FeatureClassColumn.ColumnName;
            ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).ValueMember = this.featureClassDataTable.FeatureClassIDColumn.ColumnName;
        }

        /// <summary>
        /// Populates the components config details.
        /// </summary>
        private void PopulateComponentsConfigDetails()
        {
            try
            {
                this.formLoad = true;
                this.componentsConfigDataSet = this.form8062Controller.WorkItem.F8062_ListComponentsConfiguration(TerraScanCommon.ApplicationId);
                this.PopulateEmptyRow();

                ////if (this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Count > 0)
                if (this.ComponentsConfigGridView.OriginalRowCount - 1 > 0)
                {
                    (this.FeatureClass as DataGridViewComboBoxColumn).DisplayMember = this.featureClassDataTable.FeatureClassColumn.ColumnName;
                    (this.FeatureClass as DataGridViewComboBoxColumn).ValueMember = this.featureClassDataTable.FeatureClassIDColumn.ColumnName;
                    (this.FeatureClass as DataGridViewComboBoxColumn).DataSource = this.featureClassDataTable;

                    ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).DataSource = this.featureClassDataTable;
                    ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).DisplayMember = this.featureClassDataTable.FeatureClassColumn.ColumnName;
                    ////(this.ComponentsConfigGridView.Columns["FeatureClass"] as DataGridViewComboBoxColumn).ValueMember = this.featureClassDataTable.FeatureClassIDColumn.ColumnName;                    
                    this.ComponentsConfigGridView.ReadOnly = false;
                    ////this.ComponentsConfigGridView.Rows[0].Selected = true;
                    ////this.ComponentsConfigGridView.TabStop = true;
                    ////if (this.ComponentsConfigGridView.OriginalRowCount > 0)
                    ////{
                    ////    this.ComponentsConfigGridView.Rows[0].Selected = true; ////commented by vijay
                    ////}
                    ////else
                    ////{
                    ////    this.ComponentsConfigGridView.Rows[0].Selected = false; ////commented by vijay
                    ////}

                    if (this.ComponentsConfigGridView.CurrentRowIndex >= 0)
                    {
                        this.ComponentsConfigGridView.Rows[this.ComponentsConfigGridView.CurrentRowIndex].Selected = false;
                        this.ComponentsConfigGridView.Rows[0].Selected = true;
                    }

                    if (this.slicePermissionField.deletePermission)
                    {
                        this.DeleteButton.Enabled = true;
                    }
                    else
                    {
                        this.DeleteButton.Enabled = false;
                    }  
                 
                    this.ComponentsConfigGridView.ReadOnly = false;
                    this.SaveButton.Enabled = false;
                    this.ComponentsCancelButton.Enabled = false;
                    this.formLoad = false;                    
                    ////this.ComponentsConfigGridView.Rows[0].Selected = true;
                }
                else
                {
                    ////Disabled the controls if there are no records
                    this.ComponentsConfigGridView.ReadOnly = false;                    
                    this.formLoad = true;
                    this.ComponentsConfigGridView.DataSource = this.componentsConfigDataSet.ListComponentsConfiguration;
                    this.formLoad = false;
                    if (this.ComponentsConfigGridView.Rows.Count > 0)
                    {
                        this.ComponentsConfigGridView.Rows[0].Selected = false;
                    }

                    this.DeleteButton.Enabled = false;
                    this.SaveButton.Enabled = false;
                    this.ComponentsCancelButton.Enabled = false;
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
        }

        /// <summary>
        /// Populates the empty row.
        /// </summary>
        private void PopulateEmptyRow()
        {
            if (this.slicePermissionField.newPermission)
            {
                F8062ComponentsConfigData.ListComponentsConfigurationRow dr = this.componentsConfigDataSet.ListComponentsConfiguration.NewListComponentsConfigurationRow();
                if (this.componentsConfigDataSet.ListComponentsConfiguration.Columns.Contains(this.ComponentsConfigGridView.EmptyRecordColumnName))
                {
                    dr[this.ComponentsConfigGridView.EmptyRecordColumnName] = true;
                }

                this.componentsConfigDataSet.ListComponentsConfiguration.Rows.Add(dr);
            }

            if (this.componentsConfigDataSet.ListComponentsConfiguration.Rows.Count > 7)
            {
                this.ComponentsConfigGridVscrollBar.Visible = false;
            }
            else
            {
                this.ComponentsConfigGridVscrollBar.Visible = true;
            }

            this.ComponentsConfigGridView.DataSource = this.componentsConfigDataSet.ListComponentsConfiguration;
        }

        /// <summary>
        /// To enable the save and cancel button
        /// </summary>
        private void EditEnabled()
        {
            ////if (this.ComponentsConfigDataSet.ListComponentsConfiguration.Columns.Contains(this.ComponentsConfigGridView.EmptyRecordColumnName))
            ////{
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                ////this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.DeleteButton.Enabled = false;
                this.SaveButton.Enabled = true;
                this.ComponentsCancelButton.Enabled = true;
                ////this.DisableConfigGridSorting();
            }
            ////}
        }

        /// <summary>
        /// Calculates the component item count.
        /// </summary>
        private void CalculateComponentItemCount()
        {
            ////int recordCount = 0;
            ////Decimal outDecimalValue;

            ////for (int counter = 0; counter < this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Count; counter++)
            ////{
            ////    if (!this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows[counter].RowState.Equals(DataRowState.Detached) && !this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows[counter].RowState.Equals(DataRowState.Deleted))
            ////    {
            ////        if (Decimal.TryParse(this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows[counter][this.ComponentsConfigDataSet.ListComponentsConfiguration.ComponentIDColumn].ToString(), out outDecimalValue))
            ////        {
            ////            recordCount++;
            ////        }
            ////    }
            ////}

            ////if (recordCount == this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Count || this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Count < this.ComponentsConfigGridView.NumRowsVisible)
            ////{
            ////    F8062ComponentsConfigData.ListComponentsConfigurationRow dr = this.ComponentsConfigDataSet.ListComponentsConfiguration.NewListComponentsConfigurationRow();
            ////    if (this.ComponentsConfigDataSet.ListComponentsConfiguration.Columns.Contains(this.ComponentsConfigGridView.EmptyRecordColumnName))
            ////    {
            ////        dr[this.ComponentsConfigGridView.EmptyRecordColumnName] = true;
            ////    }

            ////    this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Add(dr);
            ////    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            ////    {
            ////        this.ComponentsConfigGridView.FirstDisplayedScrollingRowIndex = this.ComponentsConfigGridView.Rows.Count - this.ComponentsConfigGridView.NumRowsVisible;
            ////    }

            ////    this.ComponentsConfigGridView.Refresh();
            ////}

            ////////check for scrollbar visibility miscAssessment.ListMADistributionItem
            ////if (this.ComponentsConfigDataSet.ListComponentsConfiguration.Rows.Count > this.ComponentsConfigGridView.NumRowsVisible)
            ////{
            ////    this.ComponentsConfigGridVscrollBar.Visible = false;
            ////}
            ////else
            ////{
            ////    this.ComponentsConfigGridVscrollBar.Visible = true;
            ////}

            int recordCount = 0;
            Decimal outDecimalValue;

            for (int counter = 0; counter < this.ComponentsConfigGridView.OriginalRowCount; counter++)   ////this.partsConfigDataSet.ListPartsConfiguration.Rows.Count
            {
                if (!this.componentsConfigDataSet.ListComponentsConfiguration.Rows[counter].RowState.Equals(DataRowState.Detached) && !this.componentsConfigDataSet.ListComponentsConfiguration.Rows[counter].RowState.Equals(DataRowState.Deleted))
                {
                    if (!string.IsNullOrEmpty(this.componentsConfigDataSet.ListComponentsConfiguration.Rows[counter][this.componentsConfigDataSet.ListComponentsConfiguration.DescriptionColumn].ToString()))
                    {
                        recordCount++;
                    }
                }
            }

            if (recordCount == this.ComponentsConfigGridView.OriginalRowCount)
            ////if (recordCount == this.partsConfigDataSet.ListPartsConfiguration.Rows.Count || this.partsConfigDataSet.ListPartsConfiguration.Rows.Count < this.PartsGridView.NumRowsVisible)
            {
                if (this.ComponentsConfigGridView.OriginalRowCount < this.ComponentsConfigGridView.NumRowsVisible)
                {
                    this.componentsConfigDataSet.ListComponentsConfiguration.Rows[this.ComponentsConfigGridView.OriginalRowCount][this.ComponentsConfigGridView.EmptyRecordColumnName] = false;
                }
                else
                {
                    F8062ComponentsConfigData.ListComponentsConfigurationRow dr = this.componentsConfigDataSet.ListComponentsConfiguration.NewListComponentsConfigurationRow();
                    if (this.componentsConfigDataSet.ListComponentsConfiguration.Columns.Contains(this.ComponentsConfigGridView.EmptyRecordColumnName))
                    {
                        dr[this.ComponentsConfigGridView.EmptyRecordColumnName] = false;
                    }

                    this.componentsConfigDataSet.ListComponentsConfiguration.Rows.Add(dr);
                }

                if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.ComponentsConfigGridView.FirstDisplayedScrollingRowIndex = this.ComponentsConfigGridView.Rows.Count - this.ComponentsConfigGridView.NumRowsVisible;
                }

                ////this.ComponentsConfigDataSet.ListComponentsConfiguration.AcceptChanges();
                ////this.ComponentsConfigGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.ComponentsConfigGridView.RefreshEdit();
            }

            ////check for scrollbar visibility
            if (this.ComponentsConfigGridView.OriginalRowCount > this.ComponentsConfigGridView.NumRowsVisible)
            {
                this.ComponentsConfigGridVscrollBar.Visible = false;
            }
            else
            {
                this.ComponentsConfigGridVscrollBar.Visible = true;
            }
        }

        ////private bool CheckPageStatus(bool onclose)
        ////{
        ////    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
        ////    {
        ////        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        ////        if (dialogResult == DialogResult.Yes)
        ////        {
        ////            bool status = this.SaveComponentsConfig(onclose);

        ////            if (status)
        ////            {
        ////                this.pageMode = TerraScanCommon.PageModeTypes.View;
        ////                this.SaveButton.Enabled = false;
        ////                this.ComponentsCancelButton.Enabled = false;
        ////            }

        ////            return status;
        ////        }
        ////        else if (dialogResult == DialogResult.No)
        ////        {
        ////            if (onclose)
        ////            {
        ////                this.pageMode = TerraScanCommon.PageModeTypes.View;
        ////                this.SaveButton.Enabled = false;
        ////                this.ComponentsCancelButton.Enabled = false;
        ////            }
        ////            else
        ////            {
        ////                this.CancelButton_Click();
        ////            }

        ////            return true;
        ////        }
        ////        else
        ////        {
        ////            return false;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        return true;
        ////    }
        ////}

        /// <summary>
        /// Enables the config grid sorting.
        /// </summary>
        private void EnableConfigGridSorting()
        {
            DataGridViewColumnCollection enableGridSortColumn = this.ComponentsConfigGridView.Columns;
            enableGridSortColumn["Description"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["FeatureClass"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["ComponentID"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        /// Disables the config grid sorting.
        /// </summary>
        private void DisableConfigGridSorting()
        {
            DataGridViewColumnCollection disableGridSortColumn = this.ComponentsConfigGridView.Columns;
            disableGridSortColumn["Description"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["FeatureClass"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["ComponentID"].SortMode = DataGridViewColumnSortMode.NotSortable;
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
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Components Configuration ", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SaveComponentsConfig(true))
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
        #endregion       
    }
}