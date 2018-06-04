//--------------------------------------------------------------------------------------------
// <copyright file="F8060.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8060.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 May 06        JYOTHI              Created
// 14 May 09        Malliga             Modified for 4228  
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
    public partial class F8060 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// partsConfigDataSet variable is used to get the details of Parts.
        /// </summary>
        private F8060PartsConfigData partsConfigDataSet = new F8060PartsConfigData();

        /// <summary>
        /// partTypeDataTable variable is used to get the details of part type.
        /// </summary>
        private F8060PartsConfigData.ListComponentsDataTable componentsDataTable = new F8060PartsConfigData.ListComponentsDataTable();

        /// <summary>
        /// form8104Control Controller
        /// </summary>
        private F8060Controller form8060Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

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
        /// Used to store the form8060Controller
        /// </summary>
        private F8060Controller form8060Controller;

        /// <summary>
        /// Edit permission of FormMaster
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        /// Used to store the componentId
        /// </summary>
        private int componentIdentity;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8060"/> class.
        /// </summary>
        public F8060()
        {
            this.InitializeComponent();
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
        public F8060(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.PartsCfgSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.PartsCfgSecIndicatorPictureBox.Width, tabText, red, green, blue);
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8060 control.
        /// </summary>
        /// <value>The F8060 control.</value>
        [CreateNew]
        public F8060Controller FormF8060Control
        {
            get { return this.form8060Controller as F8060Controller; }
            set { this.form8060Controller = value; }
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

                    this.PopulatePartsConfigDetails();
                    this.PartsGridView.ReadOnly = (!this.slicePermissionField.editPermission);
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
        /// When a record is added in F8044, the count here and the other data has to get refreshed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="eventArgs">e</param>
        [EventSubscription(EventTopicNames.FormSlice_MaterialFooterCountRefresh, ThreadOption.UserInterface)]
        public void OnFormSlice_MaterialFooterCountRefresh(object sender, EventArgs eventArgs)
        {
            try
            {
                this.PopulateComponentType();
                this.PopulatePartsConfigDetails();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        #endregion

        #region Form Events

        /// <summary>
        /// Handles the Click event of the PartsCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartsCfgSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseEnter event of the PartsCfgSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartsCfgSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.PartsToolTip.SetToolTip(this.PartsCfgSecIndicatorPictureBox, Utility.GetFormNameSpace(this.Name));
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
                if (this.PartsGridView.SelectedRows.Count > 0)
                {
                    int selectedRowIndex = this.PartsGridView.SelectedRows[0].Index;

                    if (this.slicePermissionField.deletePermission)
                    {
                        if (!string.IsNullOrEmpty(this.PartsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()))
                        {
                            if (this.form8060Controller.WorkItem.F8060_DeletePartsConfiguration(Convert.ToInt32(this.PartsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()), TerraScanCommon.UserId) == 0)
                            {
                                this.PopulatePartsConfigDetails();
                                if (this.PartsGridView.OriginalRowCount == 0)
                                {
                                    this.PartsGridView.ClearSelection();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Cannot Delete selected record", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                    this.PartsGridView.ReadOnly = (!this.slicePermissionField.editPermission);
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
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Save_Click();
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
        /// Save_s the click.
        /// </summary>
        private void Save_Click()
        {
            this.SavePartsConfigDetails();
            ////if (this.PartsGridView.DataSource != null)
            ////{
            ////    DataRow[] tempRowCollection;

            ////    this.partsConfigDataSet.ListPartsConfiguration.AcceptChanges();

            ////    tempRowCollection = this.partsConfigDataSet.ListPartsConfiguration.Select("(Rate > 999999.99)");

            ////    if (tempRowCollection.Length == 0)
            ////    {
            ////        int partId = 0;
            ////        string partDetails = string.Empty;
            ////        DataTable dt = ((DataView)this.PartsGridView.DataSource).Table;
            ////        partDetails = Utility.GetXmlString(dt);
            ////        this.form8060Controller.WorkItem.F8060_SavePartsConfiguration(partId, partDetails, TerraScanCommon.UserId);
            ////        //this.EnablePartsGridSorting();
            ////        this.PopulatePartsConfigDetails();
            ////        this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////        this.SaveButton.Enabled = false;
            ////        this.CancelButton.Enabled = false;
            ////    }
            ////    else
            ////    {
            ////        MessageBox.Show("Cost should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            ////    }
            ////}
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
                this.Cancel_Click();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Cancel_s the click.
        /// </summary>
        private void Cancel_Click()
        {
            if (this.PartsGridView.CurrentCell != null)
            {
                ////this.PartsGridView.Rows[0].Selected = true;
                this.PartsGridView.CurrentCell.Selected = true;
            }
            ////this.EnablePartsGridSorting();
            this.PopulatePartsConfigDetails();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Handles the Load event of the F8060 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8060_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;    
                this.CustomizePartsCfgGridView();
                this.PopulateComponentType();
                this.PopulatePartsConfigDetails();
                this.flagLoadOnProcess = false;    
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Handles the SelectionChangeCommitted event of the ComponentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Save_Click();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        this.Cancel_Click();
                    }
                    else if (dialogResult == DialogResult.Cancel)
                    {
                        this.ComponentComboBox.SelectedValue = this.componentIdentity;
                        return;
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                else
                {
                    this.PopulatePartsConfigDetails();
                }

                this.flagLoadOnProcess = false;
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
        /// Handles the SelectedIndexChanged event of the ComponentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.flagLoadOnProcess = true;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the EditingControlShowing event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewCheckBoxCell)
                {
                    ((CheckBox)e.Control).CheckedChanged -= new EventHandler(this.PartsConfig_CheckedChanged);
                    ((CheckBox)e.Control).CheckedChanged += new EventHandler(this.PartsConfig_CheckedChanged);
                }

                if (e.Control is DataGridViewTextBoxEditingControl)
                {
                    e.Control.TextChanged += new EventHandler(this.PartsConfigGridControl_TextChanged);
                    ////e.Control.Validating -= new CancelEventHandler(this.PartsConfigGridControl_Validating);
                    ////e.Control.Validating += new CancelEventHandler(this.PartsConfigGridControl_Validating);
                    e.Control.Validated += new EventHandler(this.PartsConfigGridControl_Validating);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the PartsConfig control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartsConfig_CheckedChanged(object sender, EventArgs e)
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
        /// Handles the Validating event of the PartsConfigGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void PartsConfigGridControl_Validating(object sender, EventArgs e)
        {
            try
            {
                this.PartsGridView.EditingControl.TextChanged -= new EventHandler(this.PartsConfigGridControl_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the PartsConfigGridControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PartsConfigGridControl_TextChanged(object sender, EventArgs e)
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
        /// Handles the DataBindingComplete event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.PartsGridView.OriginalRowCount == 1)
                {
                    ////this.PartsGridView.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // Only paint if desired column
            try
            {
                if (e.ColumnIndex == this.PartsGridView.Columns["Cost"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    // Only paint if text provided, Only paint if desired text is in cell

                    if (e.Value != null)
                    {
                        string tempvalue = e.Value.ToString().Trim();
                        Decimal outDecimal = 0;

                        ////int temp;
                        ////if (e.Value.ToString().IndexOf('-') > -1)
                        ////{
                        ////    e.Value = string.Empty;
                        ////    e.ParsingApplied = true;
                        ////}
                        ////else if (!int.TryParse(e.Value.ToString(), out temp))
                        ////{
                        ////    e.Value = string.Empty;
                        ////    e.ParsingApplied = true;
                        ////}

                        if (Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal) && outDecimal <= (decimal)999999.99)
                        {
                            e.Value = outDecimal.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Cost should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ////e.Value = string.Empty;
                            e.Value = PartsGridView.Rows[e.RowIndex].Cells["Cost"].Value;

                            ////e.Value = string.Empty;
                            e.ParsingApplied = true;
                        }

                        ////e.ParsingApplied = true;
                        ////this.partsConfigDataSet.ListPartsConfiguration.AcceptChanges();
                        this.PartsGridView.RefreshEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if (string.IsNullOrEmpty(this.PartsGridView.Rows[e.RowIndex].Cells["ComponentId"].Value.ToString().Trim()))
                    {
                        if (e.RowIndex + 1 < this.PartsGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < this.PartsGridView.RowCount; i++)
                            {
                                if (this.PartsGridView.Rows[i].Cells[e.ColumnIndex].Value != null && !String.IsNullOrEmpty(this.PartsGridView.Rows[i].Cells["ComponentId"].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                                this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                                this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                                this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                                this.PartsGridView.Rows[e.RowIndex].Selected = true;
                            }
                            else
                            {
                                this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                                this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                                this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                                this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                                this.PartsGridView.Rows[e.RowIndex].Selected = true;
                            }
                        }
                        else
                        {
                            this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                            this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                            this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                            this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                            this.PartsGridView.Rows[e.RowIndex].Selected = true;
                        }
                    }
                    else
                    {
                        this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                        this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                        this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                        this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                        this.PartsGridView.Rows[e.RowIndex].Selected = true;
                    }
                }
                //// Need to Get Confirmation
                if (e.RowIndex == 0)
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                    this.PartsGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex.Equals(2) && e.RowIndex < this.PartsGridView.OriginalRowCount)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataError event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                TerraScanCommon.SetDataGridViewPosition(this.PartsGridView, e.RowIndex);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////decimal tempRateValue;

                ////calculating related values for new values
                if (this.slicePermissionField.newPermission)
                {
                    this.CalculatePartItemCount(e.ColumnIndex);
                }

                ////if (e.ColumnIndex == this.PartsGridView.Columns["Cost"].Index)
                ////{
                ////    if ((this.PartsGridView["Cost", e.RowIndex].Value != null) && (!string.IsNullOrEmpty(this.PartsGridView["Cost", e.RowIndex].Value.ToString().Trim())))
                ////    {
                ////        decimal.TryParse(this.PartsGridView["Cost", e.RowIndex].Value.ToString().Trim(), out tempRateValue);
                ////        this.PartsGridView["Cost", e.RowIndex].Value = tempRateValue;
                ////    }
                ////}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the PartsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PartsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.PartsGridView.OriginalRowCount)
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                }
            }

            if (e.RowIndex >= 0 && !this.PermissionFiled.newPermission && this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.PartsGridView.OriginalRowCount)
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                }
            }

            if (e.RowIndex >= 0 && this.PermissionFiled.newPermission && !this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.PartsGridView.OriginalRowCount)
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                }
                else
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                }
            }

            if (e.RowIndex >= 0 && !this.PermissionFiled.newPermission && !this.PermissionFiled.editPermission)
            {
                if (e.RowIndex < this.PartsGridView.OriginalRowCount)
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = false;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.PartsGridView["PartName", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["PartNumber", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Active", e.RowIndex].ReadOnly = true;
                    this.PartsGridView["Cost", e.RowIndex].ReadOnly = true;
                }
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Customizes the parts CFG grid view.
        /// </summary>
        private void CustomizePartsCfgGridView()
        {
            this.PartsGridView.AutoGenerateColumns = false;

            this.PartName.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.PartNameColumn.ColumnName;
            this.PartNumber.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.PartNumberColumn.ColumnName;
            this.Active.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.IsActiveColumn.ColumnName;
            this.Cost.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.RateColumn.ColumnName;
            this.PartID.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.PartIDColumn.ColumnName;
            this.ComponentID.DataPropertyName = this.partsConfigDataSet.ListPartsConfiguration.ComponentIDColumn.ColumnName;
            this.PartsGridView.PrimaryKeyColumnName = "PartID";

            this.PartName.DisplayIndex = 0;
            this.PartNumber.DisplayIndex = 1;
            this.Active.DisplayIndex = 2;
            this.Cost.DisplayIndex = 3;
            this.PartID.DisplayIndex = 4;
            this.ComponentID.DisplayIndex = 5;
        }

        /// <summary>
        /// Populates the parts config details.
        /// </summary>
        private void PopulatePartsConfigDetails()
        {
            this.partsConfigDataSet.ListPartsConfiguration.Clear();
            this.componentIdentity = Convert.ToInt32(this.ComponentComboBox.SelectedValue);
            this.partsConfigDataSet = this.form8060Controller.WorkItem.F8060_ListPartsConfig(this.componentIdentity);
            this.PartsGridView.DataSource = this.partsConfigDataSet.ListPartsConfiguration;

            //// Adding New Row Based on Permissions
            this.PopulateEmptyRow();

            if (this.PartsGridView.OriginalRowCount > this.PartsGridView.NumRowsVisible)
            {
                this.PartsDetailsGridVscrollBar.Visible = false;
            }
            else
            {
                this.PartsDetailsGridVscrollBar.Visible = true;
            }

            if (this.PartsGridView.OriginalRowCount > 0 && !this.slicePermissionField.editPermission)
            {
                this.DeleteButton.Enabled = true && this.slicePermissionField.deletePermission;
                ////this.PartsGridView.ReadOnly = false;
                ////this.SaveButton.Enabled = false;
                ////this.CancelButton.Enabled = false;
                ////this.PartsGridView.Rows[0].Selected = true;
            }
            else
            {
                ////this.PartsGridView.ReadOnly = true;
                this.DeleteButton.Enabled = false;
                ////this.DeleteButton.Enabled = true && this.slicePermissionField.deletePermission;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                ////this.PartsGridView.Rows[0].Selected = false;
            }

            if (this.PartsGridView.CurrentRowIndex >= 0)
            {
                ////this.PartsGridView.Rows[this.PartsGridView.CurrentRowIndex].Selected = false;
                this.PartsGridView.Rows[0].Selected = true;
            }

            int val;
            int val1;

            int.TryParse(this.partsConfigDataSet.ListPartsConfiguration.Rows[0][this.partsConfigDataSet.ListPartsConfiguration.PartIDColumn.ColumnName].ToString(), out val);
            int.TryParse(this.partsConfigDataSet.ListPartsConfiguration.Rows[1][this.partsConfigDataSet.ListPartsConfiguration.PartIDColumn.ColumnName].ToString(), out val1);

            ////Delete permission checking added for the issue 4228 on 14/5/2009 by malliga
            ////without chceking the delete permission delete button always enabled.now it will check the delete permission
            if ((this.slicePermissionField.deletePermission && this.partsConfigDataSet.ListPartsConfiguration.Rows.Count > 0 )&& (val != 0 || val1 != 0))
            {
                this.DeleteButton.Enabled = true;
            }
            else
            {
                this.DeleteButton.Enabled = false;
            }             

            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Populates the empty row.
        /// </summary>
        private void PopulateEmptyRow()
        {
            if (this.slicePermissionField.newPermission && this.componentIdentity != 0)
            {
                if (this.PartsGridView.OriginalRowCount >= this.PartsGridView.NumRowsVisible)
                {
                    F8060PartsConfigData.ListPartsConfigurationRow dr = this.partsConfigDataSet.ListPartsConfiguration.NewListPartsConfigurationRow();
                    dr[this.partsConfigDataSet.ListPartsConfiguration.ComponentIDColumn.ColumnName] = this.componentIdentity;
                    this.partsConfigDataSet.ListPartsConfiguration.Rows.Add(dr);
                    this.partsConfigDataSet.ListPartsConfiguration.AcceptChanges();
                }
                else
                {
                    this.partsConfigDataSet.ListPartsConfiguration.Rows[this.PartsGridView.OriginalRowCount][this.partsConfigDataSet.ListPartsConfiguration.ComponentIDColumn.ColumnName] = this.componentIdentity;
                    this.partsConfigDataSet.ListPartsConfiguration.Rows[this.PartsGridView.OriginalRowCount][this.PartsGridView.EmptyRecordColumnName] = false;
                }
            }
        }

        /// <summary>
        /// Populates the type of the component.
        /// </summary>
        private void PopulateComponentType()
        {
            F8060PartsConfigData.ListComponentsDataTable tempPartsConfiguration = new F8060PartsConfigData.ListComponentsDataTable();
            this.componentsDataTable.Clear();
            this.componentsDataTable = this.form8060Controller.WorkItem.F8060_ListComponents().ListComponents;
            DataRow dr = tempPartsConfiguration.NewRow();
            dr[this.componentsDataTable.DescriptionColumn.ColumnName] = "<Select>";
            dr[this.componentsDataTable.ComponentIDColumn.ColumnName] = "0";
            tempPartsConfiguration.Rows.Add(dr);
            tempPartsConfiguration.Merge(this.componentsDataTable);
            this.componentsDataTable.Clear();
            this.componentsDataTable.Merge(tempPartsConfiguration);

            this.ComponentComboBox.DataSource = this.componentsDataTable;
            this.ComponentComboBox.ValueMember = this.componentsDataTable.ComponentIDColumn.ColumnName;
            this.ComponentComboBox.DisplayMember = this.componentsDataTable.DescriptionColumn.ColumnName;
        }

        /// <summary>
        /// To enable the save and cancel button
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                ////this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                this.DeleteButton.Enabled = false;
                this.SaveButton.Enabled = true;
                this.CancelButton.Enabled = true;
                ////this.DisablePartsGridSorting();
            }
        }

        /// <summary>
        /// Calculates the part item count.
        /// </summary>
        /// <param name="columnIndex">Index of the column.</param>
        private void CalculatePartItemCount(int columnIndex)
        {
            int recordCount = 0;
            Decimal outDecimalValue;

            if (columnIndex.Equals(this.PartsGridView.Columns["PartName"].Index) && this.componentIdentity != 0)
            {
                for (int counter = 0; counter < this.PartsGridView.OriginalRowCount; counter++)   ////this.partsConfigDataSet.ListPartsConfiguration.Rows.Count
                {
                    if (!this.partsConfigDataSet.ListPartsConfiguration.Rows[counter].RowState.Equals(DataRowState.Detached) && !this.partsConfigDataSet.ListPartsConfiguration.Rows[counter].RowState.Equals(DataRowState.Deleted))
                    {
                        if (!string.IsNullOrEmpty(this.partsConfigDataSet.ListPartsConfiguration.Rows[counter][this.partsConfigDataSet.ListPartsConfiguration.PartNameColumn].ToString()))
                        {
                            recordCount++;
                        }
                    }
                }

                if (recordCount == this.PartsGridView.OriginalRowCount)
                {
                    if (this.PartsGridView.OriginalRowCount < this.PartsGridView.NumRowsVisible)
                    {
                        this.partsConfigDataSet.ListPartsConfiguration.Rows[this.PartsGridView.OriginalRowCount][this.partsConfigDataSet.ListPartsConfiguration.ComponentIDColumn.ColumnName] = this.componentIdentity;
                        this.partsConfigDataSet.ListPartsConfiguration.Rows[this.PartsGridView.OriginalRowCount][this.PartsGridView.EmptyRecordColumnName] = false;
                    }
                    else
                    {
                        F8060PartsConfigData.ListPartsConfigurationRow dr = this.partsConfigDataSet.ListPartsConfiguration.NewListPartsConfigurationRow();
                        if (this.partsConfigDataSet.ListPartsConfiguration.Columns.Contains(this.PartsGridView.EmptyRecordColumnName))
                        {
                            dr[this.PartsGridView.EmptyRecordColumnName] = false;
                        }

                        dr[this.partsConfigDataSet.ListPartsConfiguration.ComponentIDColumn.ColumnName] = this.componentIdentity;
                        this.partsConfigDataSet.ListPartsConfiguration.Rows.Add(dr);
                    }

                    ////this.partsConfigDataSet.ListPartsConfiguration.AcceptChanges();
                    ////this.PartsGridView.RefreshEdit();
                    ////this.PartsGridView.DataSource = this.partsConfigDataSet.ListPartsConfiguration;

                    if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        this.PartsGridView.FirstDisplayedScrollingRowIndex = this.PartsGridView.Rows.Count - this.PartsGridView.NumRowsVisible;
                    }

                    ////this.PartsGridView.Refresh();
                    ////this.partsConfigDataSet.AcceptChanges();
                    this.PartsGridView.RefreshEdit();
                }

                ////check for scrollbar visibility
                if (this.PartsGridView.OriginalRowCount > this.PartsGridView.NumRowsVisible)
                {
                    this.PartsDetailsGridVscrollBar.Visible = false;
                }
                else
                {
                    this.PartsDetailsGridVscrollBar.Visible = true;
                }
            }
        }

        /// <summary>
        ///  Method to Enable the PartsGridSorting
        /// </summary>
        private void EnablePartsGridSorting()
        {
            DataGridViewColumnCollection enableGridSortColumn = this.PartsGridView.Columns;
            enableGridSortColumn["PartName"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["PartNumber"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["Cost"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["PartID"].SortMode = DataGridViewColumnSortMode.Programmatic;
            enableGridSortColumn["ComponentID"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

        /// <summary>
        ///  Method to Disable the PartsGridSorting
        /// </summary>
        private void DisablePartsGridSorting()
        {
            DataGridViewColumnCollection disableGridSortColumn = this.PartsGridView.Columns;
            disableGridSortColumn["PartName"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["PartNumber"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["Cost"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["PartID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["ComponentID"].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Saves the parts config details.
        /// </summary>
        /// <returns>bool</returns>
        private bool SavePartsConfigDetails()
        {
            if (this.PartsGridView.DataSource != null)
            {
                DataRow[] tempRowCollection;

                this.partsConfigDataSet.ListPartsConfiguration.AcceptChanges();

                tempRowCollection = this.partsConfigDataSet.ListPartsConfiguration.Select("(Rate > 999999.99)");

                if (tempRowCollection.Length == 0)
                {
                    int partId = 0;
                    string partDetails = string.Empty;
                    DataTable dt = ((DataView)this.PartsGridView.DataSource).Table;
                    partDetails = Utility.GetXmlString(dt);
                    this.form8060Controller.WorkItem.F8060_SavePartsConfiguration(partId, partDetails, TerraScanCommon.UserId);
                    ////this.EnablePartsGridSorting();
                    this.PopulatePartsConfigDetails();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SaveButton.Enabled = false;
                    this.CancelButton.Enabled = false;
                    return true;
                }
                else
                {
                    MessageBox.Show("Cost should not be greater than 999999.99", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
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
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Parts Configuration ", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SavePartsConfigDetails())
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

