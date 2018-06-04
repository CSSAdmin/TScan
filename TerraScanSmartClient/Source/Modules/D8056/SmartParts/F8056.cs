//--------------------------------------------------------------------------------------------
// <copyright file="F8056.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8056.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Sep 06        JYOTHI              Created
//*********************************************************************************/
namespace D8056
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
    /// F8052 class file
    /// </summary>
    [SmartPart]
    public partial class F8056 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// inspectionDetailsDataSet variable is used to get the details of inspection Event.
        /// </summary>
        private InspectionEventData inspectionDetailsDataSet = new InspectionEventData();

        /// <summary>
        /// Integer
        /// </summary>
        private int formNo;

        /// <summary>
        /// detailTypeDataTable variable is used to get the details of detail type.
        /// </summary>
        private InspectionEventData.ListInspectionComponentDataTable componentTypeDataTable = new InspectionEventData.ListInspectionComponentDataTable();

        /// <summary>
        /// detailTypeDataTable variable is used to get the details of detail type.
        /// </summary>
        private InspectionEventData.ListInspectionConditionDataTable conditionTypeDataTable = new InspectionEventData.ListInspectionConditionDataTable();

        /// <summary>
        /// detailTypeDataTable variable is used to get the details of detail type.
        /// </summary>
        private InspectionEventData.ListInspectionActionDataTable actionTypeDataTable = new InspectionEventData.ListInspectionActionDataTable();

        /// <summary>
        /// F8056Controller Controller
        /// </summary>
        private F8056Controller form8056Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int keyId;

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int redColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int greenColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private int blueColor;

        /// <summary>
        /// keyId Local variable.
        /// </summary>
        private string sectionIndicatorTabText;

        /// <summary>
        /// formMasterPermissionEdit Local variable.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// flag to identify form load
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F8056"/> class.
        /// </summary>
        public F8056()
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
        public F8056(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.formNo = formNo;
            this.AutoSize = true;
            this.flagFormLoad = false;
            this.sectionIndicatorTabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(153, this.DistrictInfoSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
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
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F8056 control.
        /// </summary>
        /// <value>The F8056 control.</value>
        [CreateNew]
        public F8056Controller Form8056Control
        {
            get { return this.form8056Control as F8056Controller; }
            set { this.form8056Control = value; }
        }
        #endregion

        #region Event Subscription

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
                this.Height = this.DistrictInfoSecIndicatorPictureBox.Height; ////this.AddButton.Height + this.InspectionDetailsGridView.Height + this.panel2.Height;
                sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                if (this.slicePermissionField.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
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
            this.ClearInspectionDetails();
            InspectionEventData tempInspectionDetailsDataSet = new InspectionEventData();
            this.InspectionDetailsGridView.DataSource = tempInspectionDetailsDataSet.ListInspectionDetails;
            if (this.InspectionDetailsGridView.CurrentCell != null)
            {
                this.InspectionDetailsGridView.CurrentCell.Selected = false;
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
                if (this.InspectionDetailsGridView.CurrentCell != null)
                {
                    this.InspectionDetailsGridView.CurrentCell.Selected = true;
                }

                this.DisableControls(true);
                this.ClearInspectionDetails();
                this.GetComponentType();
                this.GetConditionType();
                this.GetActionType();
                this.PopulateInspectionDetails();
                ////this.recordEdited = false;
                this.DisableHeaderControls(this.slicePermissionField.newPermission);
                ////if (InspectionDetailsGridView.OriginalRowCount > 0)
                ////{
                ////    this.DisableHeaderControls(this.slicePermissionField.newPermission);
                ////}
                ////else
                ////{
                ////    this.DisableHeaderControls(false);
                ////}
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
                    this.SaveGrid();
                    ////this.recordEdited = false;
                    this.DisableHeaderControls(this.slicePermissionField.newPermission);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.DisableControls(true);
                this.GetComponentType();
                this.GetConditionType();
                this.GetActionType();
                this.PopulateInspectionDetails();
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

                    this.InspectionDetailsGridView.ReadOnly = (!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);
                    ////this.DisableHeaderControls(this.slicePermissionField.newPermission);                   
                    ////Coding added for the issue 3169
                    if (this.InspectionDetailsGridView.OriginalRowCount  > 0)
                    {
                        this.DisableHeaderControls(this.slicePermissionField.newPermission);
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
                this.ClearInspectionDetails();
                this.GetComponentType();
                this.GetConditionType();
                this.GetActionType();
                this.PopulateInspectionDetails();
            }

            ////To resize of the Form slice afther added
            if (!this.flagLoadOnProcess)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F8056 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F8056_Load(object sender, EventArgs e)
        {
            try
            {
            this.FlagSliceForm = true;
            this.GetComponentType();
            this.GetConditionType();
            this.GetActionType();
            this.CustomizeInspectionDetailsGridView();
            this.flagFormLoad = true;
            this.PopulateInspectionDetails();
            this.CommentTextBox.MaxLength = this.inspectionDetailsDataSet.ListInspectionDetails.CommentColumn.MaxLength;
            this.flagFormLoad = false;
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

        #region Private Methods

        /// <summary>
        /// Customizes the inspection details grid view.
        /// </summary>
        private void CustomizeInspectionDetailsGridView()
        {
            this.InspectionDetailsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.InspectionDetailsGridView.Columns;

            columns["Component"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.ComponentIDColumn.ColumnName;
            columns["Condition"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.ConditionIDColumn.ColumnName;
            columns["Action"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.ActionIDColumn.ColumnName;
            columns["Comment"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.CommentColumn.ColumnName;
            columns["InspectionID"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.InspectionIDColumn.ColumnName;
            columns["EventId"].DataPropertyName = this.inspectionDetailsDataSet.ListInspectionDetails.EventIDColumn.ColumnName;

            columns["Component"].DisplayIndex = 0;
            columns["Condition"].DisplayIndex = 1;
            columns["Action"].DisplayIndex = 2;
            columns["Comment"].DisplayIndex = 3;
            columns["InspectionID"].DisplayIndex = 4;
            columns["EventId"].DisplayIndex = 5;

            this.InspectionDetailsGridView.DataSource = this.inspectionDetailsDataSet.ListInspectionDetails;

            (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).DataSource = this.componentTypeDataTable;
            (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).DisplayMember = this.componentTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
            (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).ValueMember = this.componentTypeDataTable.ComponentIDColumn.ColumnName; ////"ComponentID";

            (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).DataSource = this.conditionTypeDataTable;
            (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).DisplayMember = this.conditionTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
            (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).ValueMember = this.conditionTypeDataTable.ConditionIDColumn.ColumnName; ////"ConditionID";

            (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).DataSource = this.actionTypeDataTable;
            (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).DisplayMember = this.actionTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
            (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).ValueMember = this.actionTypeDataTable.ActionIDColumn.ColumnName; ////"ActionID";
        }

        /// <summary>
        /// Populates the inspection details.
        /// </summary>
        private void PopulateInspectionDetails()
        {
                // Issue Fix Code : Starts Here
                int keyIdvalid = this.form8056Control.WorkItem.F8056_CheckEventId(this.masterFormNo, this.keyId);

                if (keyIdvalid == 1)
                {
                    this.DisableControls(true);
                    this.DisableHeaderControls(true); 

                    this.inspectionDetailsDataSet = this.form8056Control.WorkItem.F8056_ListInspectionDetails(this.keyId);

                    (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).DataSource = this.componentTypeDataTable;
                    (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).DisplayMember = this.componentTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
                    (this.InspectionDetailsGridView.Columns["Component"] as DataGridViewComboBoxColumn).ValueMember = this.componentTypeDataTable.ComponentIDColumn.ColumnName; ////"ComponentID";

                    (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).DataSource = this.conditionTypeDataTable;
                    (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).DisplayMember = this.conditionTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
                    (this.InspectionDetailsGridView.Columns["Condition"] as DataGridViewComboBoxColumn).ValueMember = this.conditionTypeDataTable.ConditionIDColumn.ColumnName; ////"ConditionID";

                    (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).DataSource = this.actionTypeDataTable;
                    (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).DisplayMember = this.actionTypeDataTable.DescriptionColumn.ColumnName; ////"Description";
                    (this.InspectionDetailsGridView.Columns["Action"] as DataGridViewComboBoxColumn).ValueMember = this.actionTypeDataTable.ActionIDColumn.ColumnName; ////"ActionID";

                    this.SetSmartPartHeight(this.inspectionDetailsDataSet.ListInspectionDetails.Rows.Count);

                    if (this.inspectionDetailsDataSet.ListInspectionDetails.Rows.Count > 8)
                    {
                        this.InspectionDetailsGridVscrollBar.Visible = false;
                    }
                    else
                    {
                        this.InspectionDetailsGridVscrollBar.Visible = true;
                    }

                    this.InspectionDetailsGridView.DataSource = this.inspectionDetailsDataSet.ListInspectionDetails;
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
                    if (!this.flagFormLoad)
                    {
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
                    }

                    if (this.InspectionDetailsGridView.OriginalRowCount == 0)
                    {
                        this.InspectionDetailsGridView.CurrentCell = null;
                    }
                }
                else
                {
                    this.DisableControls(false);
                    this.DisableHeaderControls(false);
                }

                // Issue Fix Code : Ends Here
        }

        /// <summary>
        /// Sets the height of the smart part.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetSmartPartHeight(int recordCount)
        {
            if (recordCount > 4)
            {
                if (recordCount > 8)
                {
                    recordCount = 8;
                }

                int increment = ((recordCount - 4) * 22);
                this.InspectionDetailsGridView.Height = 111 + increment;
                this.GridPanel.Height = this.InspectionDetailsGridView.Height;
                this.InspectionDetailsGridVscrollBar.Height = 111 + increment - 4;
                this.DistrictInfoSecIndicatorPictureBox.Height = 153 + increment;
                this.panel2.Top = CommentPanel.Height + this.GridPanel.Height - 4;
                this.panel4.Top = this.panel2.Top;
                this.InspectionDetailsGridView.NumRowsVisible = recordCount;
                this.Height = this.DistrictInfoSecIndicatorPictureBox.Height;
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(153, this.DistrictInfoSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            }
            else
            {
                this.InspectionDetailsGridView.Height = 111;
                this.GridPanel.Height = this.InspectionDetailsGridView.Height;
                this.InspectionDetailsGridVscrollBar.Height = this.InspectionDetailsGridView.Height - 4;
                this.panel2.Top = CommentPanel.Height + this.GridPanel.Height - 4;
                this.panel4.Top = this.panel2.Top;
                this.DistrictInfoSecIndicatorPictureBox.Height = 153;
                this.InspectionDetailsGridView.NumRowsVisible = 4;
                this.Height = 153;
                this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(153, this.DistrictInfoSecIndicatorPictureBox.Width, this.sectionIndicatorTabText, this.redColor, this.greenColor, this.blueColor);
            }
        }

        /// <summary>
        /// Gets the type of the action.
        /// </summary>
        private void GetActionType()
        {
            try
            {
                InspectionEventData.ListInspectionActionDataTable tempActionListInspection = new InspectionEventData.ListInspectionActionDataTable();
                this.actionTypeDataTable = this.form8056Control.WorkItem.F8056_ListInspectionDetails(this.keyId).ListInspectionAction;
                tempActionListInspection.Rows.Add(new object[] { 0, "<Select>", });
                tempActionListInspection.Merge(this.actionTypeDataTable);
                this.ActionTypeComboBox.DataSource = tempActionListInspection;
                this.ActionTypeComboBox.ValueMember = tempActionListInspection.ActionIDColumn.ColumnName;
                this.ActionTypeComboBox.DisplayMember = tempActionListInspection.DescriptionColumn.ColumnName;
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
        /// Gets the type of the condition.
        /// </summary>
        private void GetConditionType()
        {
                InspectionEventData.ListInspectionConditionDataTable tempCondListInspection = new InspectionEventData.ListInspectionConditionDataTable();
                this.conditionTypeDataTable = this.form8056Control.WorkItem.F8056_ListInspectionDetails(this.keyId).ListInspectionCondition;
                tempCondListInspection.Rows.Add(new object[] { 0, "<Select>", });
                tempCondListInspection.Merge(this.conditionTypeDataTable);
                this.ConditionTypeComboBox.DataSource = tempCondListInspection;
                this.ConditionTypeComboBox.ValueMember = tempCondListInspection.ConditionIDColumn.ColumnName;
                this.ConditionTypeComboBox.DisplayMember = tempCondListInspection.DescriptionColumn.ColumnName;
           }

        /// <summary>
        /// Gets the type of the component.
        /// </summary>
        private void GetComponentType()
        {
                InspectionEventData.ListInspectionComponentDataTable tempListInspection = new InspectionEventData.ListInspectionComponentDataTable();
                this.componentTypeDataTable = this.form8056Control.WorkItem.F8056_ListInspectionDetails(this.keyId).ListInspectionComponent;
                tempListInspection.Rows.Add(new object[] { 0, "<Select>", });
                tempListInspection.Merge(this.componentTypeDataTable);
                this.ComponentTypeComboBox.DataSource = tempListInspection;
                this.ComponentTypeComboBox.ValueMember = tempListInspection.ComponentIDColumn.ColumnName;
                this.ComponentTypeComboBox.DisplayMember = tempListInspection.DescriptionColumn.ColumnName;
         }

        /// <summary>
        /// Clears the inspection details.
        /// </summary>
        private void ClearInspectionDetails()
        {
            this.CommentTextBox.Text = string.Empty;
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
        /// Saves the grid.
        /// </summary>
        private void SaveGrid()
        {
                this.Cursor = Cursors.WaitCursor;
                if (InspectionDetailsGridView.DataSource != null)
                {
                    DataSet ds = new DataSet("Root");
                    DataTable dt = new DataTable("Table");

                    dt = ((DataView)InspectionDetailsGridView.DataSource).Table;
                    dt.TableName = "Table";
                    ds.Tables.Add(dt.Copy());
                    string inspectionDetails = ds.GetXml();
                    this.form8056Control.WorkItem.F8056_UpdateInspectionDetails(inspectionDetails, TerraScanCommon.UserId);
                }

                this.Cursor = Cursors.Default;
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the AddButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            this.AddRecord();

            ////To resize of the Form slice afther added
            if (!this.flagLoadOnProcess)
            {
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the InspectionDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InspectionDetailsGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 46)
                {
                    if (this.InspectionDetailsGridView.SelectedRows.Count > 0)
                    {
                        int selectedRowIndex = this.InspectionDetailsGridView.SelectedRows[0].Index;

                        if (PermissionFiled.deletePermission)
                        {
                            //Condition added for the issue 106
                            if (!string.IsNullOrEmpty(this.InspectionDetailsGridView.Rows[selectedRowIndex].Cells[0].Value.ToString()))
                            {
                                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteInspectionRecord"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("DeleteTimeLine"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    this.form8056Control.WorkItem.F8056_DeleteInspectionDetails(Convert.ToInt32(this.InspectionDetailsGridView.Rows[selectedRowIndex].Cells[4].Value.ToString()), TerraScanCommon.UserId);
                                    this.PopulateInspectionDetails();

                                    ////To resize of the Form slice afther added
                                    if (!this.flagLoadOnProcess)
                                    {
                                        SliceResize sliceResize;
                                        sliceResize.MasterFormNo = this.masterFormNo;
                                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                                        sliceResize.SliceFormHeight = this.DistrictInfoSecIndicatorPictureBox.Height;
                                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
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
        /// Handles the CellValueChanged event of the InspectionDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void InspectionDetailsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            this.EditEnabled();
            this.DisableHeaderControls(false);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the DistrictInfoSecIndicatorPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictInfoSecIndicatorPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
            this.InspectionDetailsToolTip.SetToolTip(this.DistrictInfoSecIndicatorPictureBox, Utility.GetFormNameSpace(this.Name));
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Headers the controls key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void HeaderControlsKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Enter)
            {
                this.AddRecord();
            }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Adds the record.
        /// </summary>
        private void AddRecord()
        {
                this.Cursor = Cursors.WaitCursor;
                string validationErrors = string.Empty;

                InspectionEventData inspectionEventData = new InspectionEventData();
                InspectionEventData.SaveInspectionDetailsRow dr = inspectionEventData.SaveInspectionDetails.NewSaveInspectionDetailsRow();

                dr.EventID = this.keyId;
                if (Convert.ToInt32(this.ComponentTypeComboBox.SelectedValue.ToString()) == 0)
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8056SelectComponentType"); ////"Please select the Component. \n";
                }
                else
                {
                    dr.ComponentID = Convert.ToInt32(this.ComponentTypeComboBox.SelectedValue.ToString());
                }

                if (Convert.ToInt32(this.ConditionTypeComboBox.SelectedValue.ToString()) == 0)
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8056SelectConditionType"); ////"Please select the Condition. \n";
                }
                else
                {
                    dr.ConditionID = Convert.ToInt32(this.ConditionTypeComboBox.SelectedValue.ToString());
                }

                if (Convert.ToInt32(this.ActionTypeComboBox.SelectedValue.ToString()) == 0)
                {
                    validationErrors = validationErrors + SharedFunctions.GetResourceString("F8056SelectActionType"); ////"Please select the Action.";
                }
                else
                {
                    dr.ActionID = Convert.ToInt32(this.ActionTypeComboBox.SelectedValue.ToString());
                }

                dr.Comment = this.CommentTextBox.Text.Trim();

                if (string.IsNullOrEmpty(validationErrors.Trim()))
                {
                    inspectionEventData.SaveInspectionDetails.Rows.Add(dr);
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(inspectionEventData.SaveInspectionDetails.Copy());
                    tempDataSet.Tables[0].TableName = "Table";

                    this.form8056Control.WorkItem.F8056_SaveInspectionDetails(tempDataSet.GetXml(), TerraScanCommon.UserId);
                    this.ClearInspectionDetails();
                    this.GetComponentType();
                    this.GetConditionType();
                    this.GetActionType();
                    this.PopulateInspectionDetails();
                }
                else
                {
                    MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Comboes the tool tip.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ComboToolTip(object sender, MouseEventArgs e)
        {
            try
            {
            if (this.ComponentTypeComboBox.Text.Trim().Length > 15)
            {
                this.InspectionDetailsToolTip.SetToolTip(this.ComponentTypeComboBox, this.ComponentTypeComboBox.Text);
            }
            else
            {
                this.InspectionDetailsToolTip.RemoveAll();
            }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the ConditionTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ConditionTypeComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
            if (this.ConditionTypeComboBox.Text.Trim().Length > 15)
            {
                this.InspectionDetailsToolTip.SetToolTip(this.ConditionTypeComboBox, this.ConditionTypeComboBox.Text);
            }
            else
            {
                this.InspectionDetailsToolTip.RemoveAll();
            }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseMove event of the ActionTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void ActionTypeComboBox_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
            if (this.ActionTypeComboBox.Text.Trim().Length > 15)
            {
                this.InspectionDetailsToolTip.SetToolTip(this.ActionTypeComboBox, this.ActionTypeComboBox.Text);
            }
            else
            {
                this.InspectionDetailsToolTip.RemoveAll();
            }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the InspectionDetailsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void InspectionDetailsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
            if (this.InspectionDetailsGridView.OriginalRowCount == 0)
            {
                this.InspectionDetailsGridView.CurrentCell = null;
            }
             }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void InspectionDetailsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex < this.InspectionDetailsGridView.OriginalRowCount)
                {
                    this.InspectionDetailsGridView["Component", e.RowIndex].ReadOnly = false;
                    this.InspectionDetailsGridView["Condition", e.RowIndex].ReadOnly = false;
                    this.InspectionDetailsGridView["Action", e.RowIndex].ReadOnly = false;
                    this.InspectionDetailsGridView["Comment", e.RowIndex].ReadOnly = false;
                    this.InspectionDetailsGridView["InspectionID", e.RowIndex].ReadOnly = false;
                    this.InspectionDetailsGridView["EventId", e.RowIndex].ReadOnly = false;
                }
                else
                {
                    this.InspectionDetailsGridView["Component", e.RowIndex].ReadOnly = true;
                    this.InspectionDetailsGridView["Condition", e.RowIndex].ReadOnly = true;
                    this.InspectionDetailsGridView["Action", e.RowIndex].ReadOnly = true;
                    this.InspectionDetailsGridView["Comment", e.RowIndex].ReadOnly = true;
                    this.InspectionDetailsGridView["InspectionID", e.RowIndex].ReadOnly = true;
                    this.InspectionDetailsGridView["EventId", e.RowIndex].ReadOnly = true;
                }
            }
        }
    }
}
