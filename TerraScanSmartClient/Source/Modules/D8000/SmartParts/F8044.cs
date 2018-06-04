//--------------------------------------------------------------------------------------------
// <copyright file="F8044.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8044.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06        JYOTHI              Created
// 31 Oct 2006      JAYANTHI            To publish an event, for 8046 to get refreshed when record added in this local grid
//*********************************************************************************/
namespace D8000
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
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;

    /// <summary>
    /// F8044 class file
    /// </summary>
    [SmartPart]
    public partial class F8044 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// materialDetailsDataSet variable is used to get the details of material Details.
        /// </summary>
        private F8044MaterialsData materialDetailsDataSet = new F8044MaterialsData();

        /// <summary>
        /// Integer FormNo
        /// </summary>
        private int formNO;

        /// <summary>
        /// partTypeDataTable variable is used to get the details of part type.
        /// </summary>
        private F8044MaterialsData.ListMaterialsResourceDataTable partTypeDataTable = new F8044MaterialsData.ListMaterialsResourceDataTable();

        /// <summary>
        /// userTypeDataTable variable is used to get the details of user type.
        /// </summary>
        private F8040TimeData.ListTimeResourceDataTable userTypeDataTable = new F8040TimeData.ListTimeResourceDataTable();

        /// <summary>
        /// gridUserTypeList variable is used to get the details of user type.
        /// </summary>
        private F8040TimeData.ListTimeResourceDataTable gridUserTypeList = new F8040TimeData.ListTimeResourceDataTable();

        /// <summary>
        /// gridPartTypeDataTableAll variable is used to get the details of part type.
        /// </summary>
        private F8044MaterialsData.ListMaterialsResourceDataTable gridPartTypeDataTableAll = new F8044MaterialsData.ListMaterialsResourceDataTable();

        /// <summary>
        /// form8104Control Controller
        /// </summary>
        private F8044Controller form8044Control;

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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8044"/> class.
        /// </summary>
        public F8044()
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
        public F8044(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formNO = formNO;
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.Height, this.DistrictInfoSecIndicatorPictureBox.Width, tabText, red, green, blue);
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
        /// When a record is added in this local grid, the data in F8046 will get refreshed
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_MaterialFooterCountRefresh, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_MaterialFooterCountRefresh;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8044 control.
        /// </summary>
        /// <value>The F8044 control.</value>
        [CreateNew]
        public F8044Controller Form8044Control
        {
            get { return this.form8044Control as F8044Controller; }
            set { this.form8044Control = value; }
        }
        #endregion

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
                    sliceValidationFields.ErrorMessage = this.ValidateGridEditedRecords();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.ClearTimeDetails();
            F8044MaterialsData tempMaterialsDetailsDataSet = new F8044MaterialsData();
            this.MaterialsGridView.DataSource = tempMaterialsDetailsDataSet.ListMaterials;
            if (this.MaterialsGridView.CurrentCell != null)
            {
                this.MaterialsGridView.CurrentCell = null;
            }

            this.DisableControls(false);
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////this.MaterialsGridView.CurrentCell.Selected = true;
            this.DisableControls(true);
            this.ClearTimeDetails();
            this.UserComboBox.SelectedIndex = 0;
            this.PartComboBox.SelectedIndex = 0;
            this.PopulateMaterialDetails();
            this.DisableHeaderControls(this.slicePermissionField.newPermission);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    this.SaveGrid(sender, eventArgs);
                    this.DisableHeaderControls(this.slicePermissionField.newPermission);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.DisableControls(true);
                this.UserComboBox.SelectedIndex = 0;
                this.PartComboBox.SelectedIndex = 0;
                this.PopulateMaterialDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

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

                    this.MaterialsGridView.ReadOnly = (!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    //this.MaterialsGridView.TabStop = !(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    //this.DisableHeaderControls(this.slicePermissionField.newPermission);


                    if (this.actualMaterialDetailsRowsCount > 0)
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

        #region Protected Methods

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

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F8044 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8044_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.GetUserType();
            this.GetPartType();
            this.CustomizeMaterialsGridView();
            this.PopulateMaterialDetails();
            this.CommentTextBox.MaxLength = this.materialDetailsDataSet.ListMaterials.CommentColumn.MaxLength;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Customizes the materials grid view.
        /// </summary>
        private void CustomizeMaterialsGridView()
        {
            this.MaterialsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.MaterialsGridView.Columns;
            this.gridUserTypeList = this.form8044Control.WorkItem.F8040_ListTimeResourceInformation(4).ListTimeResource;
            this.gridPartTypeDataTableAll = this.form8044Control.WorkItem.F8044_ListMaterialsResourceType(2, this.keyId).ListMaterialsResource;
            columns["User"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.UserIDColumn.ColumnName;
            columns["Part"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.PartIDColumn.ColumnName;
            columns["Qnty"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.QuantityColumn.ColumnName;
            columns["Comment"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.CommentColumn.ColumnName;
            columns["MaterialId"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.MaterialIDColumn.ColumnName;
            columns["EventID"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.EventIDColumn.ColumnName;
            columns["WOID"].DataPropertyName = this.materialDetailsDataSet.ListMaterials.WOIDColumn.ColumnName;

            columns["User"].DisplayIndex = 0;
            columns["Part"].DisplayIndex = 1;
            columns["Qnty"].DisplayIndex = 2;
            columns["Comment"].DisplayIndex = 3;
            columns["MaterialId"].DisplayIndex = 4;
            columns["EventId"].DisplayIndex = 5;
            columns["WOID"].DisplayIndex = 6;

            this.MaterialsGridView.DataSource = this.materialDetailsDataSet.ListMaterials;

            (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).DataSource = this.gridUserTypeList;
            (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).DisplayMember = this.gridUserTypeList.ResourceNameColumn.ColumnName; ////"ResourceName";
            (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).ValueMember = this.gridUserTypeList.ResourceIDColumn.ColumnName; ////"ResourceID";

            (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).DataSource = this.gridPartTypeDataTableAll;
            (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).DisplayMember = this.gridPartTypeDataTableAll.PartDescriptionColumn.ColumnName; ////"PartDescription";
            (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).ValueMember = this.gridPartTypeDataTableAll.PartIDColumn.ColumnName; ////"PartID";
        }

        /// <summary>
        /// Populates the material details.
        /// </summary>
        private void PopulateMaterialDetails()
        {
            try
            {
                // Issue Fix : Code Starts Here
                int keyIdValid = this.form8044Control.WorkItem.F8044_CheckEventId(this.formNO, this.keyId);

                if (keyIdValid == 1)
                {
                    this.DisableHeaderControls(true);
                    this.DisableControls(true);
                    this.materialDetailsDataSet = this.form8044Control.WorkItem.F8044_ListMaterialDetails(this.masterFormNo, this.keyId);
                    this.actualMaterialDetailsRowsCount = this.materialDetailsDataSet.ListMaterials.Rows.Count;
                    if (this.materialDetailsDataSet.ListMaterials.Rows.Count > 0)
                    {
                        (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).DataSource = this.gridUserTypeList;
                        (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).DisplayMember = this.gridUserTypeList.ResourceNameColumn.ColumnName; ////"ResourceName";
                        (this.MaterialsGridView.Columns["User"] as DataGridViewComboBoxColumn).ValueMember = this.gridUserTypeList.ResourceIDColumn.ColumnName; ////"ResourceID";

                        (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).DataSource = this.gridPartTypeDataTableAll;
                        (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).DisplayMember = this.gridPartTypeDataTableAll.PartDescriptionColumn.ColumnName; ////"PartDescription";
                        (this.MaterialsGridView.Columns["Part"] as DataGridViewComboBoxColumn).ValueMember = this.gridPartTypeDataTableAll.PartIDColumn.ColumnName; ////"PartID";

                        this.MaterialsGridView.ReadOnly = false;
                        this.MaterialsGridView.TabStop = true;
                        ////VscrollBar is enabled or disabled based on NumRowsVisible in GridView
                        if (this.materialDetailsDataSet.ListMaterials.Rows.Count > this.MaterialsGridView.NumRowsVisible)
                        {
                            this.InspectionDetailsGridVscrollBar.Visible = false;
                        }
                        else
                        {
                            this.InspectionDetailsGridVscrollBar.Visible = true;
                        }
                        ////Populates the material details
                        this.MaterialsGridView.DataSource = this.materialDetailsDataSet.ListMaterials;
                    }
                    else
                    {
                        ////Disabled the controls if there are no records
                        this.MaterialsGridView.ReadOnly = true;
                        this.MaterialsGridView.TabStop = false;
                        this.MaterialsGridView.DataSource = this.materialDetailsDataSet.ListMaterials;
                    }
                }
                else
                {
                    this.DisableControls(false);
                }

                // Issue Fix : Code Ends Here
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
        /// Gets the type of the part.
        /// </summary>
        private void GetPartType()
        {
            try
            {
                F8044MaterialsData.ListMaterialsResourceDataTable tempPartList = new F8044MaterialsData.ListMaterialsResourceDataTable();
                ////Fills DataTable with parts that are currently active.
                ////IsActive(1) - Gets only active parts
                this.partTypeDataTable = this.form8044Control.WorkItem.F8044_ListMaterialsResourceType(1, this.keyId).ListMaterialsResource;
                F8044MaterialsData.ListMaterialsResourceRow dr;
                dr = tempPartList.NewListMaterialsResourceRow();
                ////Sets the default DisplayMember for PartComboBox as <Select>
                dr.PartDescription = "<Select>";
                tempPartList.Rows.Add(dr);
                tempPartList.Merge(this.partTypeDataTable);
                ////Populates the parts that are currently active.
                this.PartComboBox.DataSource = tempPartList;
                this.PartComboBox.ValueMember = tempPartList.PartIDColumn.ColumnName;
                this.PartComboBox.DisplayMember = tempPartList.PartDescriptionColumn.ColumnName;
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
        /// Gets the type of the user.
        /// </summary>
        private void GetUserType()
        {
            try
            {
                F8040TimeData.ListTimeResourceDataTable tempUserTypeList = new F8040TimeData.ListTimeResourceDataTable();
                ////Fills DataTable with users that are currently active.
                ////IsActive(3) - Gets only active parts
                this.userTypeDataTable = this.form8044Control.WorkItem.F8040_ListTimeResourceInformation(3).ListTimeResource;
                F8040TimeData.ListTimeResourceRow dr;
                dr = tempUserTypeList.NewListTimeResourceRow();
                dr.ResourceID = 0;
                ////Sets the default DisplayMember for UserComboBox as <Select>
                dr.ResourceName = "<Select>";
                dr.IsUser = "0";
                tempUserTypeList.Rows.Add(dr);
                tempUserTypeList.Merge(this.userTypeDataTable);
                ////Populates the users that are currently active.
                this.UserComboBox.DataSource = tempUserTypeList;
                this.UserComboBox.ValueMember = tempUserTypeList.ResourceIDColumn.ColumnName;
                this.UserComboBox.DisplayMember = tempUserTypeList.ResourceNameColumn.ColumnName;
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
        /// Disables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
            this.GridPanel.Enabled = enable;
        }

        /// <summary>
        /// Disables the header controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void DisableHeaderControls(bool enable)
        {
            this.HeaderPanel.Enabled = enable;
        }

        /// <summary>
        /// Clears the time details.
        /// </summary>
        private void ClearTimeDetails()
        {
            this.QntyTextBox.Text = string.Empty;
            this.CommentTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Edits the enabled.
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
        /// Validates the grid edited records.
        /// </summary>
        /// <returns>validation Error Message</returns>
        private string ValidateGridEditedRecords()
        {
            foreach (DataGridViewRow currentRow in this.MaterialsGridView.Rows)
            {
                if (!string.IsNullOrEmpty(currentRow.Cells["User"].Value.ToString()))
                {
                    if (currentRow.Cells["Qnty"].Value != null && !string.IsNullOrEmpty(currentRow.Cells["Qnty"].Value.ToString()))
                    {
                        int addedQuantity;
                        if (int.TryParse(currentRow.Cells["Qnty"].Value.ToString(), out addedQuantity))
                        {
                            if (addedQuantity <= 0 || addedQuantity > byte.MaxValue)
                            {
                                return SharedFunctions.GetResourceString("F8044CheckQntyMaxValue"); ////("Please give value between 0 and 255 for Quantity. \n");                               
                            }
                        }
                        else
                        {
                            return SharedFunctions.GetResourceString("F8044CheckQntyNumeric"); ////("Please give a numeric value for Quantity. \n");
                        }
                    }
                    else
                    {
                        return SharedFunctions.GetResourceString("F8044QntyEmpty"); ////("Quantity can't be empty. \n");                        
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Saves the grid.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveGrid(object sender, EventArgs e)
        {
            try
            {
                if (this.MaterialsGridView.DataSource != null)
                {
                    DataTable dt = ((DataView)this.MaterialsGridView.DataSource).Table;
                    for (int currentRow = 0; currentRow < MaterialsGridView.OriginalRowCount; currentRow++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[currentRow]["MaterialId"].ToString()))
                        {
                            dt.Rows.Remove(dt.Rows[currentRow]);
                        }

                        if (string.IsNullOrEmpty(dt.Rows[currentRow]["WOID"].ToString()))
                        {
                            dt.Rows[currentRow]["WOID"] = DBNull.Value;
                        }
                    }

                    string materialsDetails = Utility.GetXmlString(dt);
                    this.form8044Control.WorkItem.F8044_UpdateMaterialDetails(materialsDetails, TerraScan.Common.TerraScanCommon.UserId);
                }

                this.ClearTimeDetails();
                this.UserComboBox.SelectedIndex = 0;
                this.PartComboBox.SelectedIndex = 0;
                ////this.GetUserType();
                ////this.GetPartType();
                this.PopulateMaterialDetails();
                this.FormSlice_MaterialFooterCountRefresh(sender, e);
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

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            this.AddRecord(sender, e);
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddRecord(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string validationErrors = string.Empty;

                F8044MaterialsData materialsData = new F8044MaterialsData();
                F8044MaterialsData.SaveMaterialsRow dr = materialsData.SaveMaterials.NewSaveMaterialsRow();

                dr.KeyID = this.keyId;
                ////dr.WOID = null;
                if (Convert.ToInt32(this.UserComboBox.SelectedValue.ToString()) == 0)
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8044SelectUserName"); //// "Please select the User. \n";
                }
                else
                {
                    dr.UserID = Convert.ToInt32(this.UserComboBox.SelectedValue.ToString());
                }

                if (Convert.ToInt32(this.PartComboBox.SelectedValue.ToString()) == 0)
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8044SelectPartName"); ////"Please select the Part. \n";
                }
                else
                {
                    dr.PartID = Convert.ToInt32(this.PartComboBox.SelectedValue.ToString());
                }

                if (string.IsNullOrEmpty(this.QntyTextBox.Text.Trim()))
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8044CheckQntyEmpty"); ////"Quantity can't be empty. \n";
                }
                else
                {
                    try
                    {
                        int addedQuantity = Convert.ToInt16(this.QntyTextBox.Text.Trim());
                        if ((addedQuantity > 0) && (addedQuantity < 256))
                        {
                            dr.Quantity = addedQuantity;
                        }
                        else
                        {
                            validationErrors = validationErrors + SharedFunctions.GetResourceString("F8044CheckQntyMaxValue"); ////"Please give value between 0 and 255 for Quantity. \n";
                        }
                    }
                    catch
                    {
                        validationErrors = validationErrors + SharedFunctions.GetResourceString("F8044CheckQntyNumeric"); ////"Please give a numeric value for Quantity. \n";
                    }
                }

                dr.Comment = this.CommentTextBox.Text.Trim();

                dr.MasterFormID = this.masterFormNo;

                if (string.IsNullOrEmpty(validationErrors.Trim()))
                {
                    materialsData.SaveMaterials.Rows.Add(dr);
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(materialsData.SaveMaterials.Copy());
                    tempDataSet.Tables[0].TableName = "Table";

                    this.form8044Control.WorkItem.F8044_SaveMaterialDetails(tempDataSet.GetXml(), TerraScan.Common.TerraScanCommon.UserId);
                    this.ClearTimeDetails();
                    this.UserComboBox.SelectedIndex = 0;
                    this.PartComboBox.SelectedIndex = 0;
                    this.PopulateMaterialDetails();
                    this.FormSlice_MaterialFooterCountRefresh(sender, e);
                }
                else
                {
                    MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// Headers the controls key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void HeaderControlsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AddRecord(sender, e);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.InspectionDetailsToolTip.SetToolTip(this.DistrictInfoSecIndicatorPictureBox, Utility.GetFormNameSpace(this.Name));
        }

        /// <summary>
        /// Handles the Click event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));
        }

        /// <summary>
        /// Handles the MouseMove event of the UserComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void UserComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            ComboBox senderCombo = sender as ComboBox;
            if (senderCombo.Text.Trim().Length > 15)
            {
                this.InspectionDetailsToolTip.SetToolTip(senderCombo, senderCombo.Text);
            }
            else
            {
                this.InspectionDetailsToolTip.RemoveAll();
            }
        }
        #endregion Events

        #region GridEvents

        /// <summary>
        /// Handles the KeyDown event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    //// For delete button KeyValue is 46
                    if (e.KeyValue == 46)
                    {
                        if (this.MaterialsGridView.SelectedRows.Count > 0)
                        {
                            int selectedRowIndex = this.MaterialsGridView.SelectedRows[0].Index;

                            if (PermissionFiled.deletePermission)
                            {
                                // Coding added for the issue 106[for Empty row deletion here checking the contion
                                if (!string.IsNullOrEmpty(this.MaterialsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()))
                                {
                                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMaterialItem"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("DeleteMaterialTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                    {
                                        this.form8044Control.WorkItem.F8044_DeleteMaterialItem(Convert.ToInt32(this.MaterialsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()), TerraScan.Common.TerraScanCommon.UserId);
                                        this.PopulateMaterialDetails();
                                        this.FormSlice_MaterialFooterCountRefresh(sender, e);
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
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.EditEnabled();
            this.DisableHeaderControls(false);
        }

        /// <summary>
        /// Handles the CellParsing event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e.ColumnIndex == this.MaterialsGridView.Columns["Qnty"].Index)
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
                    Int16 outInt16;

                    if (!Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal) || outDecimal > (decimal)999999.99)
                    {
                        MessageBox.Show("Invalid Quantity", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //Int16.TryParse(e.Value.ToString(), out outInt16);
                    //if (outInt16 <= 0 || outInt16 >= 256)
                    //{
                    //    MessageBox.Show("Please give value between 0 and 256 for Quantity", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    this.MaterialsGridView[e.ColumnIndex, e.RowIndex].Selected = true;
                    //    this.MaterialsGridView.CurrentCell.Selected = true;
                    //    //TerraScanCommon.SetDataGridViewCellPosition(this.MaterialsGridView, e.RowIndex, e.ColumnIndex);
                    //    //this.MaterialsGridView.EditingControl.Focus ();
                    //    //this.MaterialsGridView.EditingControl.Select();   
                    //    //this.MaterialsGridView.Columns["Qnty"].Selected = true;                        
                    //}
                    e.Value = outDecimal.ToString();
                    e.ParsingApplied = true;
                    this.MaterialsGridView.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Handles the DataError event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (this.MaterialsGridView.OriginalRowCount == 0)
            {
                this.MaterialsGridView.CurrentCell = null;
            }
        }

        /// <summary>
        /// Handles the CellLeave event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            //{
            //    int selectedRowIndex = e.RowIndex;
            //    int selectedColumnIndex = e.ColumnIndex;

            //    if ((Convert.ToInt32(this.MaterialsGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()) == 0) || (Convert.ToInt32(this.MaterialsGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()) >= 256))
            //    {
            //        MessageBox.Show("Please give value between 0 and 265 for Quantity", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //this.MaterialsGridView[e.ColumnIndex, e.RowIndex].Selected = true;
            //        //this.MaterialsGridView[0, 0].KeyEntersEditMode(new KeyEventArgs (Keys.Select));
            //        //((DataGridViewTextBoxEditingControl)this.MaterialsGridView.EditingControl).Select(); 
            //        // TerraScanCommon.SetDataGridViewCellPosition(this.MaterialsGridView, selectedRowIndex, selectedColumnIndex);
            //        //this.MaterialsGridView.EditingControl.Focus ();
            //        //this.MaterialsGridView.EditingControl.Select();

            //    }
            //}
        }

        /// <summary>
        /// Handles the CellEndEdit event of the MaterialsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void MaterialsGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex == 2)
            //{
            //    int selectedRowIndex = e.RowIndex;
            //    int selectedColumnIndex = e.ColumnIndex;

            //    if ((Convert.ToInt32(this.MaterialsGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()) == 0) || (Convert.ToInt32(this.MaterialsGridView.Rows[selectedRowIndex].Cells[2].Value.ToString()) >= 256))
            //    {
            //        MessageBox.Show("Please give value between 0 and 256 for Quantity", ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        //this.MaterialsGridView[e.ColumnIndex, e.RowIndex].Selected = true;
            //        //this.MaterialsGridView[0, 0].KeyEntersEditMode(new KeyEventArgs (Keys.Select));
            //        //((DataGridViewTextBoxEditingControl)this.MaterialsGridView.EditingControl).Select(); 
            //        // TerraScanCommon.SetDataGridViewCellPosition(this.MaterialsGridView, selectedRowIndex, selectedColumnIndex);
            //        //this.MaterialsGridView.EditingControl.Focus ();
            //        //this.MaterialsGridView.EditingControl.Select();

            //        //this.ParentForm.ActiveControl = this.MaterialsGridView;
            //        //this.ParentForm.ActiveControl.Focus();
            //        //this.ParentForm.ActiveControl.Select();

            //    }
            //}
        }
        #endregion GridEvents

        private void MaterialsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < this.MaterialsGridView.OriginalRowCount)
                {
                    this.MaterialsGridView["User", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["Part", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["Qnty", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["Comment", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["MaterialID", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["EventID", e.RowIndex].ReadOnly = false;
                    this.MaterialsGridView["WOID", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.MaterialsGridView["User", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["Part", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["Qnty", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["Comment", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["MaterialID", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["EventID", e.RowIndex].ReadOnly = true;
                    this.MaterialsGridView["WOID", e.RowIndex].ReadOnly = true;
                }
            }

        }
    }
}
