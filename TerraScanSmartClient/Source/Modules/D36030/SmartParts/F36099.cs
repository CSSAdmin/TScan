//--------------------------------------------------------------------------------------------
// <copyright file="F36035.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods F36035 FS Land Codes..
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/09/2007       Kuppu               Created
// 24/02/2009       khaja               Made changes to fix Bugs #4303 to 4316,4328 & 5094 
//
//*********************************************************************************/
namespace D36030
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
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F36099 Class File.
    /// </summary>
    [SmartPart]
    public partial class F36099 : BaseSmartPart
    {
        #region Private Members

        /// <summary>
        /// LandData class
        /// </summary>
        private F36035LandData form36035LandData;

        /// <summary>
        /// LandData class
        /// </summary>
        private F36035LandData formLandCodeData;

        /// <summary>
        /// AdjustmentTypeDataSet
        /// </summary>
        private DataSet adjustmentTypeDataSet;

        /// <summary>
        /// UseAdjustmentTypeDataSet
        /// </summary>
        private DataSet useadjustmentTypeDataSet;

        /// <summary>
        /// UseLandCodeDataTable
        /// </summary>
        private DataTable useLandCodeDataTable;

        /// <summary> 
        /// controller F36032
        /// </summary>        
        private F36099Controller form36099Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// OperationSmartPart Variable
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// Used to store the landUniqueID (here it is unique id and key id)
        /// </summary>
        private int landUniqueID;

        /// <summary>
        /// Local variable.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Local variable.
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// Unique keyId from the Form Master
        /// </summary>
        private int keyId;

        /// <summary>
        /// Unique valueSliceID from the Form Master
        /// </summary>
        private int valueSliceID;

        /// <summary>
        /// Unique ID from the landCode
        /// </summary>
        private string landCode;

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
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// current rollyear
        /// </summary>
        private int rollYear;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// Value MaxValue
        /// </summary>
        private double maxvalue;

        /// <summary>
        /// UseValue MaxValue
        /// </summary>
        private double maxusevalue;

        /// <summary>
        /// Money MaxValue
        /// </summary>
        private double moneyMaxValue = 922337203685477.5807;

        ////khaja created these 2 members and used to fix Bug#5093

        /// <summary>
        /// unit base value
        /// </summary>
        private decimal unitBaseValue;

        /// <summary>
        /// use unit base value
        /// </summary>
        private decimal useunitBaseValue;

        ////khaja added these private members to fix Bug#4307

        /// <summary>
        /// AcresValueTextbox Changeevent
        /// </summary>
        private bool acresValueChanged;

        /// <summary>
        /// Multiplier Changeevent
        /// </summary>
        private bool multiplierValueChanged;

        /// <summary>
        /// AdjustmentType PreviewKeydown Event
        /// </summary>
        private bool adjTypeValueChanged;

        /// <summary>
        /// UseAdjustmentType PreviewKeydown Event
        /// </summary>
        private bool useadjTypeValueChanged;

        ////end

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36035"/> class.
        /// </summary>
        public F36099()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36033"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36099(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.valueSliceID = this.keyId;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.form36035LandData = new F36035LandData();
            this.formLandCodeData = new F36035LandData();
            this.adjustmentTypeDataSet = new DataSet();
            DataTable adjustmentTypeDataTable = new DataTable();
            adjustmentTypeDataTable.Columns.Add(new DataColumn("adjustmentTypeId", typeof(int)));
            adjustmentTypeDataTable.Columns.Add(new DataColumn("adjustmentTypeDescription", typeof(string)));
            DataRow dr1 = adjustmentTypeDataTable.NewRow();
            dr1["adjustmentTypeId"] = 0;
            dr1["adjustmentTypeDescription"] = "None";
            adjustmentTypeDataTable.Rows.Add(dr1);
            DataRow dr2 = adjustmentTypeDataTable.NewRow();
            dr2["adjustmentTypeId"] = 1;
            dr2["adjustmentTypeDescription"] = "Land Code";
            adjustmentTypeDataTable.Rows.Add(dr2);
            DataRow dr3 = adjustmentTypeDataTable.NewRow();
            dr3["adjustmentTypeId"] = 2;
            dr3["adjustmentTypeDescription"] = "Factor";
            adjustmentTypeDataTable.Rows.Add(dr3);
            DataRow dr4 = adjustmentTypeDataTable.NewRow();
            dr4["adjustmentTypeId"] = 3;
            dr4["adjustmentTypeDescription"] = "Value";
            adjustmentTypeDataTable.Rows.Add(dr4);
            this.adjustmentTypeDataSet.Tables.Clear();
            this.adjustmentTypeDataSet.Tables.Add(adjustmentTypeDataTable);

            ////use AdjustmentType
            this.useadjustmentTypeDataSet = new DataSet();
            DataTable useadjustmentTypeDataTable = new DataTable();
            useadjustmentTypeDataTable.Columns.Add(new DataColumn("adjustmentTypeId", typeof(int)));
            useadjustmentTypeDataTable.Columns.Add(new DataColumn("adjustmentTypeDescription", typeof(string)));
            DataRow usedr1 = useadjustmentTypeDataTable.NewRow();
            usedr1["adjustmentTypeId"] = 0;
            usedr1["adjustmentTypeDescription"] = "None";
            useadjustmentTypeDataTable.Rows.Add(usedr1);
            DataRow usedr2 = useadjustmentTypeDataTable.NewRow();
            usedr2["adjustmentTypeId"] = 1;
            usedr2["adjustmentTypeDescription"] = "Land Code";
            useadjustmentTypeDataTable.Rows.Add(usedr2);
            DataRow usedr3 = useadjustmentTypeDataTable.NewRow();
            usedr3["adjustmentTypeId"] = 2;
            usedr3["adjustmentTypeDescription"] = "Factor";
            useadjustmentTypeDataTable.Rows.Add(usedr3);
            DataRow usedr4 = useadjustmentTypeDataTable.NewRow();
            usedr4["adjustmentTypeId"] = 3;
            usedr4["adjustmentTypeDescription"] = "Value";
            useadjustmentTypeDataTable.Rows.Add(usedr4);
            this.useadjustmentTypeDataSet.Tables.Clear();
            this.useadjustmentTypeDataSet.Tables.Add(useadjustmentTypeDataTable);

            this.LandPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LandPictureBox.Height, this.LandPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /////// <summary>
        /////// event publication for panel link label click
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /////// <summary>
        /////// event publication for SetCancelButton
        /////// </summary>
        ////[EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /////// <summary>
        /////// Declare the event FormSlice_EditEnabled        
        /////// </summary>
        ////[EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        ////public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        #endregion Event Publication

        #region Enum

        /// <summary>
        /// Enumerator ComboBox Items
        /// </summary>
        public enum ComboBoxItems
        {
            /// <summary>
            /// Value for None
            /// </summary>
            None = 0,

            /// <summary>
            /// Value for LandCode
            /// </summary>
            LandCode = 1,

            /// <summary>
            /// Value for Factor
            /// </summary>
            Factor = 2,

            /// <summary>
            /// Value for Value
            /// </summary>
            Value = 3
        }

        #endregion Enum

        #region Property

        /// <summary>
        /// Gets or sets the form36032 control.
        /// </summary>
        /// <value>The form36032 control.</value>
        [CreateNew]
        public F36099Controller Form36099Control
        {
            get { return this.form36099Control as F36099Controller; }
            set { this.form36099Control = value; }
        }
        #endregion Property

        #region Event Subscription

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

                if (this.keyId != eventArgs.Data.KeyId)
                {
                    ////To check the invalid key id in set slice event subscription db call is set to F36035_ListLandDetails Method to check invalid key id
                    this.form36035LandData = this.form36099Control.WorkItem.F36035_ListLandDetails(eventArgs.Data.KeyId);
                }

                if (this.form36035LandData.GetValueSliceValidTable.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(this.form36035LandData.GetValueSliceValidTable.Rows[0][this.form36035LandData.GetValueSliceValidTable.IsOpenColumn.ColumnName].ToString()))
                    {
                        if (Convert.ToBoolean(this.form36035LandData.GetValueSliceValidTable.Rows[0][this.form36035LandData.GetValueSliceValidTable.IsOpenColumn.ColumnName].ToString()))
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
                        }
                        else
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Button Click Event For the WorkSpace
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            if (this.operationSmartPart == sender)
            {
                switch (e.Data)
                {
                    case "NEW":
                        this.NewButtonClick();
                        break;
                    case "SAVE":
                        this.SaveButtonClick();
                        break;
                    case "CANCEL":
                        this.CancelButtonClick();
                        break;
                    case "DELETE":
                        this.DeleteButtonClick();
                        break;
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
            if (this.PermissionFiled.deletePermission && this.valueSliceID > 0)
            {
                this.form36099Control.WorkItem.F35001_DeleteValueSlice(this.valueSliceID, TerraScan.Common.TerraScanCommon.UserId);
            }
        }

        /// <summary>
        /// Event Subscription for save slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            ////here form master save is not used but we are using this Event subscription to update the value slice header form slice
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

        /// <summary>
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            ////here save is only used to update the description of the Value slice header
            ////pls check with the this.CheckErrors(eventArgs.Data) method             
            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
                eventArgs.Data.FlagFormClose = this.CheckPageStatus();
            }
        }
        #endregion Event Subscriptionwew

        #region Protected methods

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

        #endregion Protected methods

        #region Events

        /// <summary>
        /// Handles the Load event of the F36035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36099_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.LoadWorkSpaces();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                this.flagLoadOnProcess = true;
                this.CustomizeAllComboBoxes();
                this.CustomizeLandDetailsGrid();
                this.LoadLandFrom();
                this.flagLoadOnProcess = true;
                this.ClearTextBoxValues();
                this.DisablePanels();
                this.flagLoadOnProcess = false;
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
                {
                    this.CommentsdeckWorkspace.Enabled = true;
                }
                else
                {
                    this.CommentsdeckWorkspace.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButtonClick()
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
            this.flagFormLoad = true;
            this.flagLoadOnProcess = true;
            this.ControlLock(!this.PermissionFiled.newPermission);
            this.ShowPanel(true);
            this.ClearLandHeaderDetails();
            this.LandDetailsDataGridView.DataSource = this.form36035LandData.ListLandValueSliceDetails;

            if ((this.AdjusmentTypeComboBox.SelectedValue.ToString()).Equals(((int)ComboBoxItems.None).ToString()))
            {
                this.SetForNone();
            }
            else
            {
                this.SetForFactor();
            }

            ////khaja added these lines to fix Bug #4308
            #region
            this.UseAdjusmentTypeComboBox.SelectedValue = 0;
            if ((this.UseAdjusmentTypeComboBox.SelectedValue.ToString()).Equals(((int)ComboBoxItems.None).ToString()))
            {
                this.SetForUseNone();
            }
            else
            {
                this.SetForUseFactor();
            }
            #endregion

            this.LandDetailsDataGridView.Rows[0].Selected = false;
            this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = false;

            this.flagFormLoad = false;
            this.flagLoadOnProcess = false;
            this.LandType1Combo.Focus();
        }

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButtonClick()
        {
            if (this.selectedRow >= 0 && (this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value != null) && (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value.ToString())))
            {
                int.TryParse(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value.ToString(), out this.landUniqueID);
            }

            if (this.landUniqueID > 0)
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.form36099Control.WorkItem.F36035_DeleteLandDetails(this.landUniqueID, TerraScan.Common.TerraScanCommon.UserId);
                    this.landUniqueID = 0;
                    this.flagLoadOnProcess = true;
                    this.ClearLandHeaderDetails();
                    this.LoadLandFrom();
                    this.flagLoadOnProcess = false;
                    this.flagLoadOnProcess = true;
                    this.TotalGridValues();
                    this.ClearTextBoxValues();
                    this.DisablePanels();
                    this.flagLoadOnProcess = false;
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }

                    if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
                    {
                        this.CommentsdeckWorkspace.Enabled = true;
                    }
                    else
                    {
                        this.CommentsdeckWorkspace.Enabled = false;
                    }
                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// ClickEvent for the SaveButton
        /// </summary>
        private void SaveButtonClick()
        {
            this.SaveLandSliceDetails();
            this.AlertValueSliceHeader();
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.CommentsdeckWorkspace.Enabled = true;
            }
            else
            {
                this.CommentsdeckWorkspace.Enabled = false;
            }
        }

        /// <summary>
        /// ClickEvent for the CancelButton
        /// </summary>       
        private void CancelButtonClick()
        {
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.flagLoadOnProcess = true;
            this.CustomizeAllComboBoxes();
            this.PopulateLandDetailsGrid();
            this.LoadLandFrom();
            this.TotalGridValues();
            this.flagLoadOnProcess = true;
            this.ClearTextBoxValues();
            this.ClearTextBoxValuesForUse();
            this.DisablePanels();
            this.LandType1Combo.Focus();
            this.flagLoadOnProcess = false;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.LandDetailsGridViewPanel.Enabled = true;
            this.LandDetailsDataGridView.Enabled = true;

            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                this.LandDetailsDataGridView.Rows[0].Selected = true;
                this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = true;
            }

            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.CommentsdeckWorkspace.Enabled = true;
            }
            else
            {
                this.CommentsdeckWorkspace.Enabled = false;
            }
            ////this.AcresTextBox.Text = string.Empty;
            ////this.MultiplierTextBox.Text = string.Empty;   
        }

        /// <summary>
        /// To store the Land form values
        /// </summary>
        private void LoadLandFrom()
        {
            try
            {
                ////this.DisablePanels();
                this.LoadAllComboBoxes();
                this.AdjusmentTypeComboBox.SelectedValue = 0;
                this.UseAdjusmentTypeComboBox.SelectedValue = 0;

                this.form36035LandData = this.form36099Control.WorkItem.F36035_ListLandDetails(this.valueSliceID);

                // this.LandDetailsDataGridView.DataSource = this.form36035LandData.ListLandValueSliceDetails.Copy();
                this.LandDetailsDataGridView.DataSource = this.form36035LandData.ListLandValueSliceDetails.Copy();

                if (this.landUniqueID > 0)
                {
                    for (int row = 0; row < this.LandDetailsDataGridView.RowCount; row++)
                    {
                        if (this.landUniqueID.ToString() == this.LandDetailsDataGridView[27, row].Value.ToString())
                        {
                            this.LoadLandHeaderDetails(row);
                        }
                    }
                }
                else
                {
                    this.LoadLandHeaderDetails(0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the EquipmentNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AlltextBoxTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    if ((sender is TerraScanComboBox) && (sender as TerraScanComboBox).Name.StartsWith("LandType", StringComparison.OrdinalIgnoreCase))
                    {
                        this.GetLandCode();
                    }

                    ////khaja added code to fix bug#4307
                    if ((sender is TerraScanTextBox))
                    {
                        if (((TerraScanTextBox)sender).Name.Equals(this.AcresTextBox.Name))
                        {
                            this.acresValueChanged = true;
                        }
                        else
                        {
                            this.acresValueChanged = false;
                        }

                        //if (((TerraScanTextBox)sender).Name.Equals(this.MultiplierTextBox.Name))
                        //{
                        //    this.multiplierValueChanged = true;
                        //}
                        //else
                        //{
                        //    this.multiplierValueChanged = false;
                        //}
                    }

                    if ((sender is TerraScanComboBox))
                    {
                        if (((TerraScanComboBox)sender).Name.Equals(this.AdjusmentTypeComboBox.Name))
                        {
                            this.adjTypeValueChanged = true;
                        }
                        else
                        {
                            this.adjTypeValueChanged = false;
                        }

                        if (((TerraScanComboBox)sender).Name.Equals(this.UseAdjusmentComboBox.Name))
                        {
                            this.useadjTypeValueChanged = true;
                        }
                        else
                        {
                            this.useadjTypeValueChanged = false;
                        }
                    }

                    ////end
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// SelectionChangeCommitted event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdjusmentTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal units, adjustment;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                #region None
                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                {
                    this.AdjustmentTextBox.Text = string.Empty;
                    this.ReasonForAdjTextBox.Text = string.Empty;
                    //this.AdjUnitTextBox.Text = string.Empty;

                    if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                    {
                        this.GetLandCode();
                    }

                    this.SetForNone();
                    this.SetForNoneLandCodeValues();
                    this.CalculationForNone();
                }
                #endregion None

                #region LandCode
                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                {
                    ////this.SetForFactor();
                    this.AdjustmentTextBox.Text = string.Empty;
                    this.ReasonForAdjTextBox.Text = string.Empty;
                    //this.AdjUnitTextBox.Text = string.Empty;

                    this.SetForLandCode();
                    if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                    {
                        this.AdjusmentComboBox.SelectedValue = -1;
                        this.ReasonForAdjTextBox.Text = string.Empty;
                        //this.AdjUnitTextBox.Text = string.Empty;
                    }
                    else
                    {
                        this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                        this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                    }

                    if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                    {
                        //this.AdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.BaseValueColumn].ToString();
                    }

                    this.CalculationForLandCode1();
                }
                #endregion LandCode

                #region Factor
                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                {
                    this.AdjustmentTextBox.Text = string.Empty;
                    this.ReasonForAdjTextBox.Text = string.Empty;
                    //this.AdjUnitTextBox.Text = string.Empty;

                    //decimal multiplier;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);

                    //decimal.TryParse(this.UnitTextBox.Text, out units);
                    decimal.TryParse(this.AdjustmentTextBox.Text, out adjustment);
                    this.SetForFactor();
                    ////this.AdjUnitTextBox.Text = Convert.ToString((multiplier * units) * adjustment);
                    //this.AdjUnitTextBox.Text = Convert.ToString(units * adjustment);
                    this.CalculationForFactor2();
                }
                #endregion Factor

                #region Value
                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                {
                    this.AdjustmentTextBox.Text = string.Empty;
                    this.ReasonForAdjTextBox.Text = string.Empty;
                    //this.AdjUnitTextBox.Text = string.Empty;

                    this.SetForFactor();
                    //decimal multiplier;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    decimal.TryParse(this.AdjustmentTextBox.Text, out adjustment);

                    //this.AdjUnitTextBox.Text = Convert.ToString(multiplier * adjustment);
                    this.AdjusmentComboBox.Visible = false;
                    this.AdjustmentTextBox.Visible = true;
                    this.CalculationForValue3();
                }
                #endregion Value

                this.ClearTextBoxValues();
                this.ClearValueTextBox();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// AdjusmentComboBox_SelectionChangeCommitted
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdjusmentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal acres, landCodeValue;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                {
                    //this.AdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.BaseValueColumn].ToString();
                }
                else
                {
                    //this.AdjUnitTextBox.Text = string.Empty;
                }

                //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                //{
                //    multiplier = 1;
                //}

                //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
                //decimal.TryParse(this.AdjUnitTextBox.Text, out landCodeValue);
                //this.AdjUnitTextBox.Text = Convert.ToString(multiplier * landCodeValue);
                decimal.TryParse(this.AcresTextBox.Text, out acres);
                //this.ValueTextBox.Text = Convert.ToString((multiplier * landCodeValue) * acres);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// LandDetailsDataGridView_CellContentClick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LandDetailsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        /// CellClick Event of LandDetailsDataGridView
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LandDetailsDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    if ((e.RowIndex >= 0) && (!this.flagFormLoad))
                    {
                        this.LoadLandHeaderDetails(e.RowIndex);
                    }
                }

                this.flagLoadOnProcess = true;
                this.ClearTextBoxValues();
                this.flagLoadOnProcess = false;
                ////this.selectedRow = e.RowIndex; 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Method to set AdjustmentTypeProperty
        /// </summary>
        private void SetAdjustmentTypeProperty()
        {
            this.AdjustmentTextBox.Visible = true;
            this.AdjusmentComboBox.Visible = false;
            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                this.SetForNone();
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
            {
                this.SetForFactor();
                this.AdjustmentTextBox.SendToBack();
                this.AdjusmentComboBox.BringToFront();
                this.AdjusmentComboBox.Visible = true;
                this.Adjustmentpanel.Enabled = true;
                ////this.Adjustmentlabel.ForeColor = System.Drawing.Color.DarkBlue;
                this.Adjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.Adjustmentpanel.BackColor = System.Drawing.Color.White;
                this.AdjustmentTextBox.BackColor = System.Drawing.Color.White;

                this.ReasonForAdjpanel.Enabled = true;
                ////this.ReasonForAdjusmentlabel.ForeColor = System.Drawing.Color.DarkBlue;
                this.ReasonForAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.ReasonForAdjpanel.BackColor = System.Drawing.Color.White;
                this.ReasonForAdjTextBox.BackColor = System.Drawing.Color.White;
                //this.AdjUnitpanel.Enabled = true;
                //this.AdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
                //this.AdjUnitpanel.BackColor = System.Drawing.Color.White;
                //this.AdjUnitTextBox.BackColor = System.Drawing.Color.White;

                this.AdjustmentTextBox.Visible = false;
                this.AdjusmentComboBox.Visible = true;
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
            {
                this.SetForFactor();
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
            {
                this.SetForFactor();
            }

            this.UseAdjustmentTextBox.Visible = true;
            this.UseAdjusmentComboBox.Visible = false;
            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                ////this.SetForUseNone();
                this.SetPanelColorForUseNone();
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
            {
                ////this.SetForUseFactor();
                ////this.SetPanelColorForUseFactor();

                this.UseAdjustmentTextBox.SendToBack();
                this.UseAdjusmentComboBox.BringToFront();
                this.UseAdjusmentComboBox.Visible = true;
                ////khaja made changes to fix Bug #4312 and changed the back colors to fix Bug#4306
                this.UseAdjusmentComboBox.Enabled = true;
                ////
                this.UseAdjustmentpanel.Enabled = true;
                ////this.Adjustmentlabel.ForeColor = System.Drawing.Color.DarkBlue;
                this.UseAdjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.UseAdjustmentpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
                this.UseAdjustmentTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;

                this.ReasonForUseAdjpanel.Enabled = true;
                ////this.ReasonForAdjusmentlabel.ForeColor = System.Drawing.Color.DarkBlue;
                this.ReasonForUseAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
                this.ReasonForUseAdjpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
                this.ReasonForUseAdjTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
                //this.UseAdjUnitpanel.Enabled = true;
                //this.UseAdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
                //this.UseAdjUnitpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
                //this.UseAdjUnitTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;

                this.UseAdjustmentTextBox.Visible = false;
                this.UseAdjusmentComboBox.Visible = true;
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
            {
                ////this.SetForUseFactor();
                this.SetPanelColorForUseFactor();
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
            {
                ////this.SetForUseFactor();
                this.SetPanelColorForUseFactor();
            }
        }

        #region LandPictureBox Events

        /// <summary>
        /// Handles the Click event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseEnter event of the LandPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.LandFormSliceToolTip.SetToolTip(this.LandPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        /// <summary>
        /// LandDetailsDataGridView_RowEnter
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LandDetailsDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.selectedRow = e.RowIndex;
            try
            {
                if (!this.flagFormLoad)
                {
                    if ((e.RowIndex >= 0) && (!this.flagFormLoad))
                    {
                        this.flagLoadOnProcess = true;
                        this.flagFormLoad = true;
                        this.LoadLandHeaderDetails(e.RowIndex);
                        this.flagFormLoad = false;
                        this.flagLoadOnProcess = false;
                    }
                }

                this.flagLoadOnProcess = true;
                this.ClearTextBoxValues();
                this.ClearTextBoxValuesForUse();
                this.flagLoadOnProcess = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// AdjustmentTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AdjustmentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.AdjusmentTypeComboBox.SelectedValue.ToString() == "0")
                {
                    this.CalculationForNone();
                    this.ClearValueTextBox();
                }

                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                {
                    if (this.AdjusmentComboBox.SelectedValue != null)
                    {
                        this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                        this.CalculationForLandCode1();
                        this.ClearValueTextBox();
                    }
                }

                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                {
                    decimal adjustmentUnitValue, acreUnits, baseValue, adjustedValue;
                    //decimal multiplier;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
                    //decimal.TryParse(this.UnitTextBox.Text, out baseValue);
                    decimal.TryParse(this.AdjustmentTextBox.Text, out adjustedValue);
                    decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                    //////this.AdjUnitTextBox.Text = Convert.ToString(multiplier * baseValue * adjustedValue);
                    //this.AdjUnitTextBox.Text = Convert.ToString(baseValue * adjustedValue);
                    //decimal.TryParse(this.AdjUnitTextBox.Text, out adjustmentUnitValue);
                    //this.ValueTextBox.Text = Convert.ToString(adjustmentUnitValue * acreUnits);
                    ////decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
                    ////this.UseAdjUnitTextBox.Text = Convert.ToString(adjustedValue * useunit);
                    ////tempcalc = useunit * adjustedValue;
                    ////this.UseValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
                    this.ClearValueTextBox();
                }

                if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                {
                    this.CalculationForValue3();
                    this.ClearValueTextBox();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Value was either too large or too small for a Decimal.")
                {
                    this.AdjustmentTextBox.Text = string.Empty;
                    MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            ////this.ClearValueTextBox();
        }

        /// <summary>
        /// AcresTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AcresTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ////khaja added this IF Condition to fix Bug#4307
                if (this.acresValueChanged)
                {
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;

                        decimal adjustmentValue, acreUnits, adjUnit, useunit;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
                        //decimal.TryParse(this.UnitTextBox.Text, out useunit);
                        decimal.TryParse(this.AdjustmentTextBox.Text, out adjustmentValue);
                        decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                        ////this.AdjUnitTextBox.Text = Convert.ToString(multiplier * useunit * adjustmentValue);
                        //this.AdjUnitTextBox.Text = Convert.ToString(useunit * adjustmentValue);
                        //decimal.TryParse(this.AdjUnitTextBox.Text, out adjUnit);
                        //this.ValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
                        ////tempcalc = useunit * adjustmentValue;
                        ////this.UseValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;
                        this.CalculationForValue3();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;
                        this.CalculationForNone();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.AdjusmentComboBox.SelectedValue != null)
                        {
                            //this.AdjUnitTextBox.Text = string.Empty;
                            //this.ValueTextBox.Text = string.Empty;
                            this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                            if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                            {
                                //this.AdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.BaseValueColumn].ToString();
                            }

                            this.CalculationForLandCode1();
                        }
                    }

                    this.ClearValueTextBox();

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        decimal adjustmentValue, acreUnits, adjUnit, useunit;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                        //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
                        decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustmentValue);
                        decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                        //this.UseAdjUnitTextBox.Text = Convert.ToString(useunit * adjustmentValue);
                        //decimal.TryParse(this.UseAdjUnitTextBox.Text, out adjUnit);
                        //this.UseValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
                        ////tempcalc = useunit * adjustmentValue;
                        ////this.UseValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
                    }

                    ////this.UseAdjUnitTextBox.Text = string.Empty;
                    ////this.UseValueTextBox.Text = string.Empty;    
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        this.CalculationForUseValue3();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        this.CalculationForNone();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.UseAdjusmentComboBox.SelectedValue != null)
                        {
                            //this.UseAdjUnitTextBox.Text = string.Empty;
                            //this.UseValueTextBox.Text = string.Empty;
                            this.landCode = this.UseAdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                            if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                            {
                                //this.UseAdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.UseBaseValueColumn].ToString();
                            }

                            this.CalculationForUseLandCode1();
                        }
                    }

                    this.ClearValueTextBoxForUse();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Value was either too large or too small for a Decimal.")
                {
                    this.AcresTextBox.Text = string.Empty;
                    MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Event for cell formatting
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LandDetailsDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.ColumnIndex == this.LandDetailsDataGridView.Columns["Value"].Index || e.ColumnIndex == this.LandDetailsDataGridView.Columns["BaseValue"].Index || e.ColumnIndex == this.LandDetailsDataGridView.Columns["UseValue"].Index || e.ColumnIndex == this.LandDetailsDataGridView.Columns["UsePerUnit"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells["LUID"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == this.LandDetailsDataGridView.Columns["Units"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells["LUID"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "0.00";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                if (e.ColumnIndex == this.LandDetailsDataGridView.Columns["Multiplier"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[e.RowIndex].Cells["LUID"].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        ////else
                        ////{
                        ////    e.Value = "0.00";
                        ////}
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the TotalValueLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TotalValueLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.TotalValueToolTip.SetToolTip(this.TotalValueLabel, this.TotalValueLabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the UnitTotallabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UnitTotallabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.UnitTotalToolTip.SetToolTip(this.UnitTotallabel, this.UnitTotallabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the LandDetailsDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void LandDetailsDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    this.LandDetailsDataGridView.RemoveDefaultSelection = false;
                    this.LandDetailsDataGridView.Rows[0].Selected = true;
                    this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = true;
                }
                else
                {
                    this.LandDetailsDataGridView.Rows[0].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the AdjusmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void AdjusmentTypeComboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                ////khaja added this IF Condition to fix Bug#4307
                if (this.adjTypeValueChanged)
                {
                    decimal units, adjustment;
                    if (!this.flagLoadOnProcess && this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        this.EditEnabled();
                    }

                    #region None
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                        {
                            this.GetLandCode();
                        }

                        this.SetForNone();
                        this.SetForNoneLandCodeValues();
                        this.CalculationForNone();
                    }
                    #endregion None

                    #region LandCode
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        ////this.SetForFactor();
                        this.SetForLandCode();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.AdjusmentComboBox.SelectedValue = -1;
                            this.ReasonForAdjTextBox.Text = string.Empty;
                            //this.AdjUnitTextBox.Text = string.Empty;
                        }
                        else
                        {
                            this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                        }

                        if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                        {
                            //this.AdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.BaseValueColumn].ToString();
                        }

                        this.CalculationForLandCode1();
                    }
                    #endregion LandCode

                    #region Factor
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //decimal multiplier;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);

                        //decimal.TryParse(this.UnitTextBox.Text, out units);
                        decimal.TryParse(this.AdjustmentTextBox.Text, out adjustment);
                        this.SetForFactor();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                        {
                            //this.AdjUnitTextBox.Text = Convert.ToString(units * adjustment);
                        }

                        this.CalculationForFactor2();
                    }
                    #endregion Factor

                    #region Value
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        this.SetForFactor();
                        //this.AdjUnitTextBox.Text = this.AdjustmentTextBox.Text;
                        this.AdjusmentComboBox.Visible = false;
                        this.AdjustmentTextBox.Visible = true;
                        this.CalculationForValue3();
                    }
                    #endregion Value

                    this.ClearTextBoxValues();
                    this.ClearValueTextBox();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <returns>Page Status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if ((!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)) && (this.LandDetailsDataGridView.OriginalRowCount > 0))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " Land ", "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    if (this.SaveLandSliceDetails())
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
                    this.CancelButtonClick();

                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Usde to alert the value slice header
        /// </summary>
        private void AlertValueSliceHeader()
        {
            // Update Appraisal Summary Table
            decimal resultAmount;
            Decimal.TryParse(this.TotalValueLabel.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

            F35002SubFormSaveEventArgs subFormSaveEventArgs;
            subFormSaveEventArgs.type = 5;
            subFormSaveEventArgs.value = resultAmount;
            subFormSaveEventArgs.valueSliceId = this.valueSliceID;

            subFormSaveEventArgs.amount = resultAmount;
            this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
        }

        /// <summary>
        /// Checks the Main valve Id exists.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;

            ////when the update button is enabled alert the user with message
            if (this.operationSmartPart.SaveButtonEnable)
            {
                DialogResult currentResult = MessageBox.Show("Unable to save because Land Details records are in New/Update mode, Do you want to save the changes to Land Details.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (DialogResult.Yes == currentResult)
                {
                    this.SaveButtonClick();

                    ////Check save is complete or not 
                    if (this.operationSmartPart.SaveButtonEnable)
                    {
                        sliceValidationFields.DisableNewMethod = true;
                    }
                    else
                    {
                        ////when save is completed then the value slice header is alerted automatically.                      
                        sliceValidationFields.DisableNewMethod = false;
                    }

                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                else if (DialogResult.No == currentResult)
                {
                    this.CancelButtonClick();
                    ////if no is seleted then alert the value slice header part
                    this.AlertValueSliceHeader();

                    sliceValidationFields.DisableNewMethod = false;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
                else if (DialogResult.Cancel == currentResult)
                {
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.ErrorMessage = string.Empty;
                    return sliceValidationFields;
                }
            }
            else
            {
                ////alert the value slice header
                this.AlertValueSliceHeader();
                sliceValidationFields.DisableNewMethod = false;
                sliceValidationFields.RequiredFieldMissing = false;
                sliceValidationFields.ErrorMessage = string.Empty;
                return sliceValidationFields;
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            #region moved this part of code from F36035_Load()
            //// To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form36099Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form36099Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form36099Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            ////set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form36099Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form36099Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);
            #endregion

            if (this.form36099Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.form36099Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.form36099Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
            }

            this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
        }

        /// <summary>
        /// To Customize All the LandComboBoxes
        /// </summary>
        private void CustomizeAllComboBoxes()
        {
            this.LandType1Combo.MaxLength = this.form36035LandData.ListLandType1.LandTypeColumn.MaxLength;
            this.LandType1Combo.DisplayMember = this.form36035LandData.ListLandType1.LandTypeColumn.ColumnName;
            this.LandType1Combo.ValueMember = this.form36035LandData.ListLandType1.LandTypeIDColumn.ColumnName;

            this.LandType2Combo.MaxLength = this.form36035LandData.ListLandType2.LandTypeColumn.MaxLength;
            this.LandType2Combo.DisplayMember = this.form36035LandData.ListLandType2.LandTypeColumn.ColumnName;
            this.LandType2Combo.ValueMember = this.form36035LandData.ListLandType2.LandTypeIDColumn.ColumnName;

            this.LandType3Combo.MaxLength = this.form36035LandData.ListLandType3.LandTypeColumn.MaxLength;
            this.LandType3Combo.DisplayMember = this.form36035LandData.ListLandType3.LandTypeColumn.ColumnName;
            this.LandType3Combo.ValueMember = this.form36035LandData.ListLandType3.LandTypeIDColumn.ColumnName;

            this.AdjusmentComboBox.MaxLength = this.form36035LandData.ListLandCode.LandCodeColumn.MaxLength;
            this.AdjusmentComboBox.DisplayMember = this.form36035LandData.ListLandCode.LandCodeColumn.ColumnName;
            this.AdjusmentComboBox.ValueMember = this.form36035LandData.ListLandCode.LandCodeColumn.ColumnName;

            this.AdjusmentTypeComboBox.DisplayMember = "adjustmentTypeDescription";
            this.AdjusmentTypeComboBox.ValueMember = "adjustmentTypeId";
            this.AdjusmentTypeComboBox.DataSource = this.adjustmentTypeDataSet.Tables[0];

            ////Added on 23/9/2008 by Malliga

            this.UseAdjusmentTypeComboBox.DisplayMember = "adjustmentTypeDescription";
            this.UseAdjusmentTypeComboBox.ValueMember = "adjustmentTypeId";
            this.UseAdjusmentTypeComboBox.DataSource = this.useadjustmentTypeDataSet.Tables[0];

            this.UseAdjusmentComboBox.DisplayMember = "LandCode";
            this.UseAdjusmentComboBox.ValueMember = "LandCode";
        }

        /// <summary>
        /// To Load All the ComboBoxes
        /// </summary>
        private void LoadAllComboBoxes()
        {
            DataRow dr;
            this.form36035LandData = this.form36099Control.WorkItem.F36035_ListLandTypeDetails(this.valueSliceID);

            ////code to add an empty column to the combo.
            if (this.form36035LandData.ListLandType1.Rows.Count > 0)
            {
                F36035LandData.ListLandType1DataTable listLandType1ComboxDatatable = new F36035LandData.ListLandType1DataTable();
                DataRow listLandType1Row = listLandType1ComboxDatatable.NewRow();
                listLandType1Row[listLandType1ComboxDatatable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType1Row[listLandType1ComboxDatatable.LandTypeIDColumn.ColumnName] = "0";
                listLandType1ComboxDatatable.Rows.Add(listLandType1Row);
                listLandType1ComboxDatatable.Merge(this.form36035LandData.ListLandType1);
                this.flagLoadOnProcess = true;
                this.LandType1Combo.DataSource = listLandType1ComboxDatatable;
                this.flagLoadOnProcess = false;
            }
            else
            {
                this.LandType1Combo.DataSource = this.form36035LandData.ListLandType1;
            }

            if (this.form36035LandData.ListLandType2.Rows.Count > 0)
            {
                F36035LandData.ListLandType2DataTable listLandType2ComboxDatatable = new F36035LandData.ListLandType2DataTable();
                DataRow listLandType2Row = listLandType2ComboxDatatable.NewRow();
                listLandType2Row[listLandType2ComboxDatatable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType2Row[listLandType2ComboxDatatable.LandTypeIDColumn.ColumnName] = "0";
                listLandType2ComboxDatatable.Rows.Add(listLandType2Row);
                listLandType2ComboxDatatable.Merge(this.form36035LandData.ListLandType2);
                this.flagLoadOnProcess = true;
                this.LandType2Combo.DataSource = listLandType2ComboxDatatable;
                this.flagLoadOnProcess = false;
            }
            else
            {
                this.LandType2Combo.DataSource = this.form36035LandData.ListLandType2;
            }

            if (this.form36035LandData.ListLandType3.Rows.Count > 0)
            {
                F36035LandData.ListLandType3DataTable listLandType3ComboxDatatable = new F36035LandData.ListLandType3DataTable();
                DataRow listLandType3Row = listLandType3ComboxDatatable.NewRow();
                listLandType3Row[listLandType3ComboxDatatable.LandTypeColumn.ColumnName] = string.Empty;
                listLandType3Row[listLandType3ComboxDatatable.LandTypeIDColumn.ColumnName] = "0";
                listLandType3ComboxDatatable.Rows.Add(listLandType3Row);
                listLandType3ComboxDatatable.Merge(this.form36035LandData.ListLandType3);
                this.flagLoadOnProcess = true;
                this.LandType3Combo.DataSource = listLandType3ComboxDatatable;
                this.flagLoadOnProcess = false;
            }
            else
            {
                this.LandType3Combo.DataSource = this.form36035LandData.ListLandType3;
            }

            if (this.form36035LandData.ListLandType1.Rows.Count > 0)
            {
                int.TryParse(this.form36035LandData.ListLandType1.Rows[0][this.form36035LandData.ListLandType1.RollYearColumn].ToString(), out this.rollYear);
            }

            this.flagLoadOnProcess = true;
            this.AdjusmentComboBox.DataSource = this.form36035LandData.ListLandCode;

            this.useLandCodeDataTable = new DataTable();

            if (this.useLandCodeDataTable.Columns.Count == 0)
            {
                this.useLandCodeDataTable.Columns.Add("LandCode");
            }

            for (int i = 0; i <= this.form36035LandData.ListLandCode.Rows.Count - 1; i++)
            {
                dr = this.useLandCodeDataTable.NewRow();
                dr["LandCode"] = this.form36035LandData.ListLandCode.Rows[i]["LandCode"];
                this.useLandCodeDataTable.Rows.Add(dr);
            }

            this.UseAdjusmentComboBox.DataSource = this.useLandCodeDataTable;
            this.flagLoadOnProcess = false;
            //// this.AdjusmentTypeComboBox.Text = string.Empty;            
        }

        /// <summary>
        /// Customizes the LandDetailsGrid
        /// </summary>
        private void CustomizeLandDetailsGrid()
        {
            //// write code to access from db and assign values
            // this.form36035LandData.ListLandValueSliceDetails.Clear();
            this.form36035LandData = this.form36099Control.WorkItem.F36035_ListLandDetails(this.keyId);
            this.LandDetailsDataGridView.AllowUserToResizeColumns = false;
            this.LandDetailsDataGridView.AllowUserToResizeRows = false;
            this.LandDetailsDataGridView.AutoGenerateColumns = false;
            this.Value.Width = 80;

            ////Adding New Invisible Column For LandTypeID1
            this.LandDetailsDataGridView.StandardTab = true;
            this.LandDetailsDataGridView.Columns.Add("LandTypeID1", "LandTypeID1");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID1Column.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID1Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandTypeID1Column.ColumnName;

            ////Adding New Invisible Column For LandTypeID2
            this.LandDetailsDataGridView.Columns.Add("LandTypeID2", "LandTypeID2");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID2Column.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID2Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandTypeID2Column.ColumnName;

            ////Adding New Invisible Column For LandTypeID3
            this.LandDetailsDataGridView.Columns.Add("LandTypeID3", "LandTypeID3");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID3Column.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandTypeID3Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandTypeID3Column.ColumnName;

            this.LandDetailsDataGridView.Columns.Add("UnitType", "UnitType");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UnitTypeColumn.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UnitTypeColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UnitTypeColumn.ColumnName;

            this.LandDetailsDataGridView.Columns.Add("AdjTypeDescription", "AdjTypeDescription");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.AdjTypeDescriptionColumn.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.AdjTypeDescriptionColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.AdjustmentTypeColumn.ColumnName;

            this.LandDetailsDataGridView.Columns.Add("UseAdjTypeDescription", "UseAdjTypeDescription");
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UseAdjTypeDescriptionColumn.ColumnName].Visible = false;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UseAdjTypeDescriptionColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentTypeColumn.ColumnName;

            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandType1Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandType1Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandType2Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandType2Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandType3Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandType3Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LandCodeColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LandCodeColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.ReportASColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.ReportASColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Break1Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Break1Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Break2Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Break2Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Break3Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Break3Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Break4Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Break4Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Break5Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Break5Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Value1Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Value1Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Value2Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Value2Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Value3Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Value3Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Value4Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Value4Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.Value5Column.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.Value5Column.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.AdjustmentTypeColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.AdjTypeDescriptionColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentTypeColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UseAdjTypeDescriptionColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.AdjustmentColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.AdjustmentColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.AdjDescriptionColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.AdjDescriptionColumn.ColumnName;
            ////khaja made changes to fix Bug#4304
            ////this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName;
            ////this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UnitsColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UnitsColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.ValueColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.ValueColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.ValueSliceIDColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.ValueSliceIDColumn.ColumnName;

            ////Added for Co on 14/7/2008
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UseBaseValueColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UseBaseValueColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UsePerUnitColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UsePerUnitColumn.ColumnName;
            this.LandDetailsDataGridView.Columns[this.form36035LandData.ListLandValueSliceDetails.UseValueColumn.ColumnName].DataPropertyName = this.form36035LandData.ListLandValueSliceDetails.UseValueColumn.ColumnName;

            ////To add the units and value columns in the grid.
            this.TotalGridValues();
        }

        /// <summary>
        /// To Populate the LandDetailsGrid
        /// </summary>
        private void PopulateLandDetailsGrid()
        {
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.LandDetailsDataGridView.DataSource = this.form36035LandData.ListLandValueSliceDetails;
            }
            else
            {
                this.ClearLandHeaderDetails();
                this.ClearLandGridDetails();
            }

            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > this.LandDetailsDataGridView.NumRowsVisible)
            {
                ////this.FooterRightpanel.Width = 18;
                ////this.FooterRightpanel.Location = new System.Drawing.Point(748, 504);
                ////this.TotalValueLabel.Width = 84;
                ////Added by khaja to fix bug#4314
                this.TotalUseValueLabel.Width = 73;
                this.TotalUseValueLabel.Location = new System.Drawing.Point(656, -2);
                this.TotalValueLabel.Width = 66;
                this.TotalValueLabel.Location = new System.Drawing.Point(520, -1);
                this.Value.Width = 65;
                this.LandDetailsVscrollBar.Visible = false;
            }
            else
            {
                ////this.FooterRightpanel.Width = 21;
                ////this.FooterRightpanel.Location = new System.Drawing.Point(746, 504);
                ////this.TotalValueLabel.Width = 82;
                this.TotalUseValueLabel.Width = 76;
                this.TotalUseValueLabel.Location = new System.Drawing.Point(671, -2);
                this.TotalValueLabel.Width = 82;
                this.TotalValueLabel.Location = new System.Drawing.Point(519, -1);
                this.Value.Width = 80;
                this.LandDetailsVscrollBar.Visible = true;
            }

            this.TotalGridValues();
        }

        //// code added to fix Bug#4316

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>       
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                int landUid = 0;

                this.LandDetailsDataGridView.RowEnter -= new System.Windows.Forms.DataGridViewCellEventHandler(this.LandDetailsDataGridView_RowEnter);

                ////code added to fix Bug#4316                
                int.TryParse(this.LandDetailsDataGridView.Rows[selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value.ToString(), out landUid);
                this.additionalOperationSmartPart.KeyId = landUid;

                this.LandDetailsDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.LandDetailsDataGridView_RowEnter);

                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                if (this.additionalOperationSmartPart.KeyId > 0)
                {
                    additionalOperationCountEntity.AttachmentCount = this.form36099Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form36099Control.WorkItem.GetCommentsCount(this.ParentFormId, this.additionalOperationSmartPart.KeyId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }
                else
                {
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To LoadLandHeaderDetails
        /// </summary>
        /// <param name="rowCount">rowCount</param>
        private void LoadLandHeaderDetails(int rowCount)
        {
            ////try
            ////{
            this.Cursor = Cursors.WaitCursor;

            this.flagLoadOnProcess = true;

            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                if (!this.flagFormLoad)
                {
                    ////TerraScanCommon.SetDataGridViewPosition(this.LandDetailsDataGridView, rowCount);
                    this.LandDetailsDataGridView.Focus();
                    this.LandDetailsDataGridView.Rows[rowCount].Selected = true;
                    this.selectedRow = rowCount;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID1Column.ColumnName].Value.ToString()))
                {
                    this.LandType1Combo.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID1Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.LandType1Combo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID2Column.ColumnName].Value.ToString()))
                {
                    this.LandType2Combo.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID2Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.LandType2Combo.SelectedValue = -1;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID3Column.ColumnName].Value.ToString()))
                {
                    this.LandType3Combo.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandTypeID3Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.LandType3Combo.SelectedValue = -1;
                }

                if (this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandCodeColumn.ColumnName].Value != null)
                {
                    this.LandCodeTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LandCodeColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.LandCodeTextBox.Text = string.Empty;
                }

                if (this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.ReportASColumn.ColumnName].Value != null)
                {
                    this.ReportAsTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.ReportASColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.ReportAsTextBox.Text = string.Empty;
                }

                ////khaja made changes to fix Bug#4304
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName].Value.ToString()))
                {
                    //this.UnitTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn.ColumnName].Value.ToString();
                    //decimal unitvalue;
                    //decimal.TryParse(this.UnitTextBox.Text, out unitvalue);
                    //this.unitBaseValue = unitvalue;
                }
                else
                {
                    //this.UnitTextBox.Text = string.Empty;
                }
                ////Added for CO on 14/7/2008
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseBaseValueColumn.ColumnName].Value.ToString()))
                {
                    //this.UseUnitTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseBaseValueColumn.ColumnName].Value.ToString();
                    //decimal useunitvalue;
                    //decimal.TryParse(this.UseUnitTextBox.Text, out useunitvalue);
                    //this.useunitBaseValue = useunitvalue;
                }
                else
                {
                    //this.UseUnitTextBox.Text = string.Empty;
                }

                ////Adding on 26/9/2008 by Malliga
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotWidthColumn.ColumnName].Value.ToString()))
                {
                    this.LotWidthTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotWidthColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.LotWidthTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotDepthColumn.ColumnName].Value.ToString()))
                {
                    this.LotDepthTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotDepthColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.LotDepthTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotDepthColumn.ColumnName].Value.ToString()))
                {
                    this.LotDepthTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.LotDepthColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.LotDepthTextBox.Text = string.Empty;
                }

                //if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.MultiplierColumn.ColumnName].Value.ToString()))
                //{
                //    this.MultiplierTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.MultiplierColumn.ColumnName].Value.ToString();
                //}
                //else
                //{
                //    this.MultiplierTextBox.Text = string.Empty;
                //}

                ////~~~~~End
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break1Column.ColumnName].Value.ToString()))
                {
                    this.Break1TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break1Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.Break1TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break2Column.ColumnName].Value.ToString()))
                {
                    this.Break2TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break2Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.Break2TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break3Column.ColumnName].Value.ToString()))
                {
                    this.Break3TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break3Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.Break3TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break4Column.ColumnName].Value.ToString()))
                {
                    this.Break4TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break4Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.Break4TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break5Column.ColumnName].Value.ToString()))
                {
                    this.Break5TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Break5Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.Break5TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value1Column.ColumnName].Value.ToString()))
                {
                    this.ValuePer1TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value1Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.ValuePer1TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value2Column.ColumnName].Value.ToString()))
                {
                    this.ValuePer2TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value2Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.ValuePer2TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value3Column.ColumnName].Value.ToString()))
                {
                    this.ValuePer3TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value3Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.ValuePer3TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value4Column.ColumnName].Value.ToString()))
                {
                    this.ValuePer4TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value4Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.ValuePer4TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value5Column.ColumnName].Value.ToString()))
                {
                    this.ValuePer5TextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.Value5Column.ColumnName].Value.ToString();
                }
                else
                {
                    this.ValuePer5TextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjustmentTypeColumn.ColumnName].Value.ToString()))
                {
                    this.AdjusmentTypeComboBox.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjTypeDescriptionColumn.ColumnName].Value.ToString();
                    if (this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjustmentTypeColumn.ColumnName].Value.ToString() == "Land Code")
                    {
                        this.AdjustmentTextBox.Visible = false;
                        this.AdjustmentTextBox.Enabled = false;
                        this.AdjusmentComboBox.Enabled = true;
                        this.AdjusmentComboBox.Visible = true;
                        this.AdjusmentComboBox.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjustmentColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        this.AdjustmentTextBox.Enabled = true;
                        this.AdjustmentTextBox.Visible = true;
                        this.AdjusmentComboBox.Enabled = false;
                        this.AdjusmentComboBox.Visible = false;
                        this.AdjustmentTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjustmentColumn.ColumnName].Value.ToString();
                        if (this.AdjusmentComboBox.Items.Count > 0)
                        {
                            this.AdjusmentComboBox.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    this.AdjusmentTypeComboBox.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjDescriptionColumn.ColumnName].Value.ToString()))
                {
                    this.ReasonForAdjTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.AdjDescriptionColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.ReasonForAdjTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjDescriptionColumn.ColumnName].Value.ToString()))
                {
                    this.ReasonForUseAdjTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjDescriptionColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.ReasonForUseAdjTextBox.Text = string.Empty;
                }

                ////khaja made changes to fix Bug#4304
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName].Value.ToString()))
                {
                    //this.AdjUnitTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn.ColumnName].Value.ToString();
                }
                else
                {
                    //this.AdjUnitTextBox.Text = string.Empty;
                }

                ////Added for CO on 14/7/2008
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UsePerUnitColumn.ColumnName].Value.ToString()))
                {
                    //this.UseAdjUnitTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UsePerUnitColumn.ColumnName].Value.ToString();
                }
                else
                {
                    //this.UseAdjUnitTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UnitsColumn.ColumnName].Value.ToString()))
                {
                    this.AcresTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UnitsColumn.ColumnName].Value.ToString();
                    this.Acreslabel.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UnitTypeColumn.ColumnName].Value.ToString();
                }
                else
                {
                    this.AcresTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.ValueColumn.ColumnName].Value.ToString()))
                {
                    //this.ValueTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.ValueColumn.ColumnName].Value.ToString();
                }
                else
                {
                    //this.ValueTextBox.Text = string.Empty;
                }

                ////Added for CO on 14/7/2008
                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseValueColumn.ColumnName].Value.ToString()))
                {
                    //this.UseValueTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseValueColumn.ColumnName].Value.ToString();
                }
                else
                {
                    //this.UseValueTextBox.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                {
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        this.CalculationForNone();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        ////khaja commented this code to fix Bug#4305
                        ////this.CalculationForLandCode1();
                        //decimal multiplier;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        this.CalculationForFactor2();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        this.CalculationForValue3();
                    }
                }

                if (!string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentTypeColumn.ColumnName].Value.ToString()))
                {
                    ////this.UseAdjusmentTypeComboBox.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentTypeColumn.ColumnName].Value.ToString();
                    this.UseAdjusmentTypeComboBox.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjTypeDescriptionColumn.ColumnName].Value.ToString();
                    if (this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentTypeColumn.ColumnName].Value.ToString() == "Land Code")
                    {
                        this.UseAdjustmentTextBox.Visible = false;
                        this.UseAdjustmentTextBox.Enabled = false;
                        this.UseAdjusmentComboBox.Enabled = true;
                        this.UseAdjusmentComboBox.Visible = true;
                        this.UseAdjusmentComboBox.SelectedValue = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentColumn.ColumnName].Value.ToString();
                    }
                    else
                    {
                        this.UseAdjustmentTextBox.Enabled = true;
                        this.UseAdjustmentTextBox.Visible = true;
                        this.UseAdjusmentComboBox.Enabled = false;
                        this.UseAdjusmentComboBox.Visible = false;
                        this.UseAdjustmentTextBox.Text = this.LandDetailsDataGridView.Rows[rowCount].Cells[this.form36035LandData.ListLandValueSliceDetails.UseAdjustmentColumn.ColumnName].Value.ToString();
                        if (this.UseAdjusmentComboBox.Items.Count > 0)
                        {
                            this.UseAdjusmentComboBox.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    this.UseAdjusmentTypeComboBox.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                {
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        this.CalculationForNone();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        ////khaja commented this code to fix Bug#4305
                        ////this.CalculationForUseLandCode1();
                        //decimal multiplier;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        this.CalculationForUseFactor2();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        this.CalculationForUseValue3();
                    }
                }

                if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > this.LandDetailsDataGridView.NumRowsVisible)
                {
                    ////this.FooterRightpanel.Width = 18;
                    ////this.FooterRightpanel.Location = new System.Drawing.Point(748, 504);                        
                    ////this.TotalValueLabel.Width = 84;
                    ////Added by khaja to fix bug#4314
                    this.TotalUseValueLabel.Width = 73;
                    this.TotalUseValueLabel.Location = new System.Drawing.Point(656, -2);
                    this.TotalValueLabel.Width = 66;
                    this.TotalValueLabel.Location = new System.Drawing.Point(520, -1);
                    this.Value.Width = 65;
                    this.LandDetailsVscrollBar.Visible = false;
                }
                else
                {
                    ////this.FooterRightpanel.Width = 21;
                    ////this.FooterRightpanel.Location = new System.Drawing.Point(746, 504); 
                    ////this.TotalValueLabel.Width = 82;
                    this.TotalUseValueLabel.Width = 76;
                    this.TotalUseValueLabel.Location = new System.Drawing.Point(671, -2);
                    this.TotalValueLabel.Width = 82;
                    this.TotalValueLabel.Location = new System.Drawing.Point(519, -1);
                    this.Value.Width = 80;
                    this.LandDetailsVscrollBar.Visible = true;
                }

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                ////code added to fix Bug#4316
                this.SetAttachmentCommentsCount();
            }
            else
            {
                #region tochange

                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                this.LandCodeTextBox.Text = string.Empty;

                this.ReportAsTextBox.Text = string.Empty;

                //this.UnitTextBox.Text = string.Empty;

                //this.UseUnitTextBox.Text = string.Empty;

                this.Break1TextBox.Text = string.Empty;

                this.Break2TextBox.Text = string.Empty;

                this.Break3TextBox.Text = string.Empty;

                this.Break4TextBox.Text = string.Empty;

                this.Break5TextBox.Text = string.Empty;

                this.ValuePer1TextBox.Text = string.Empty;

                this.ValuePer2TextBox.Text = string.Empty;

                this.ValuePer3TextBox.Text = string.Empty;

                this.ValuePer4TextBox.Text = string.Empty;

                this.ValuePer5TextBox.Text = string.Empty;
                this.AcresTextBox.Text = string.Empty;
                //this.MultiplierTextBox.Text = string.Empty;
                this.LotDepthTextBox.Text = string.Empty;
                this.LotWidthTextBox.Text = string.Empty;

                #endregion tochange
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                this.SetForNone();
            }
            else
            {
                this.SetForFactor();
            }

            this.SetAdjustmentTypeProperty();
            this.flagLoadOnProcess = false;
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            ////}
            ////finally
            ////{

            this.Cursor = Cursors.Default;
            ////}

            ////khaja added code to enable Attachment & Comment Buttons
            if (this.LandDetailsDataGridView.OriginalRowCount > 0)
            {
                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
                {
                    this.additionalOperationSmartPart.Enabled = this.PermissionFiled.editPermission;
                }
                else
                {
                    this.additionalOperationSmartPart.Enabled = false;
                }
            }
        }

        /// <summary>
        /// To Delete the selected record
        /// </summary>
        private void DeleteLandDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            ////string errorMessage = string.Empty;
            this.landUniqueID = Convert.ToInt32(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells["LUID"].Value);
            this.form36099Control.WorkItem.F36035_DeleteLandDetails(this.landUniqueID, TerraScan.Common.TerraScanCommon.UserId);
            this.form36035LandData.ListLandValueSliceDetails.Rows.Clear();
            this.form36035LandData = this.form36099Control.WorkItem.F36035_ListLandDetails(this.landUniqueID);
            this.LandDetailsDataGridView.DataSource = this.form36035LandData.ListLandValueSliceDetails.Copy();
            this.LoadLandHeaderDetails(0);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.TotalGridValues();
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// To check max length of the Break and value per
        /// </summary>
        /// <returns>boolean value</returns>
        private bool CheckBreakMaxLength()
        {
            double breakMaxValue = 9999999.99;
            double valuePerMaxValue = 922337203685477.5807;

            double baseValue;
            double usebasevalue;
            double break1;
            double break1ValuePer;
            double break2;
            double break2ValuePer;
            double break3;
            double break3ValuePer;
            double break4;
            double break4ValuePer;
            double break5;
            double break5ValuePer;

            //double.TryParse(this.ValueTextBox.Text.Trim(), out baseValue);
            //double.TryParse(this.UseValueTextBox.Text.Trim(), out usebasevalue);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1);
            double.TryParse(this.Break1TextBox.Text.Trim(), out break1ValuePer);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2);
            double.TryParse(this.Break2TextBox.Text.Trim(), out break2ValuePer);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3);
            double.TryParse(this.Break3TextBox.Text.Trim(), out break3ValuePer);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4);
            double.TryParse(this.Break4TextBox.Text.Trim(), out break4ValuePer);
            double.TryParse(this.Break5TextBox.Text.Trim(), out break5);
            double.TryParse(this.Break5TextBox.Text.Trim(), out break5ValuePer);

            if (break1 <= breakMaxValue && break2 <= breakMaxValue && break3 <= breakMaxValue && break4 <= breakMaxValue && break5 <= breakMaxValue && break1ValuePer < valuePerMaxValue && break2ValuePer < valuePerMaxValue && break3ValuePer < valuePerMaxValue && break4ValuePer < valuePerMaxValue && break5ValuePer < valuePerMaxValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// To Save the LandSliceDetails
        /// </summary>
        /// <returns>true or false</returns>
        private bool SaveLandSliceDetails()
        {
            if (this.CheckBreakMaxLength())
            {
                if (string.IsNullOrEmpty(this.LandCodeTextBox.Text.Trim()) || string.IsNullOrEmpty(this.ReportAsTextBox.Text.Trim()) || string.IsNullOrEmpty(this.AcresTextBox.Text.Trim()) || this.AcresTextBox.Text == "0.0")
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ////MessageBox.Show(SharedFunctions.GetResourceString("Missing Required Field"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.LandType1Combo.Focus();
                    return false;
                }
                else
                {
                    ////Added by khaja to fix Bug#4313
                    #region  Land Code

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.AdjusmentComboBox.SelectedIndex < 0)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.AdjusmentComboBox.Focus();
                            return false;
                        }
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.UseAdjusmentComboBox.SelectedIndex < 0)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.UseAdjusmentComboBox.Focus();
                            return false;
                        }
                    }

                    #endregion

                    #region Factor

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        decimal adjustedValue;
                        decimal.TryParse(this.AdjustmentTextBox.Text, out adjustedValue);
                        if (adjustedValue <= 0) ////(!((adjustedValue <= 1) && (adjustedValue > 0)))
                        {
                            MessageBox.Show("The adjustment value for adjustment type factor \n should be greater than zero", "TerraScan T2 - Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.AdjustmentTextBox.Focus();
                            return false;
                        }
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        decimal useadjustedValue;
                        decimal.TryParse(this.UseAdjustmentTextBox.Text, out useadjustedValue);
                        if (useadjustedValue <= 0) ////(!((adjustedValue <= 1) && (adjustedValue > 0)))
                        {
                            MessageBox.Show("The use adjustment value for adjustment type factor \n should be greater than zero", "TerraScan T2 - Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.UseAdjustmentTextBox.Focus();
                            return false;
                        }
                    }

                    #endregion

                    #region Value
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        decimal adjustedValue;
                        decimal.TryParse(this.AdjustmentTextBox.Text, out adjustedValue);
                        if (adjustedValue <= 0)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.AdjustmentTextBox.Focus();
                            return false;
                        }
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        decimal useadjustedValue;
                        decimal.TryParse(this.UseAdjustmentTextBox.Text, out useadjustedValue);
                        if (useadjustedValue <= 0) ////(!((adjustedValue <= 1) && (adjustedValue > 0)))
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.UseAdjustmentTextBox.Focus();
                            return false;
                        }
                    }
                    #endregion

                    decimal baseValue, usebasevalue, break1, break2, break3, break4, break5;
                    decimal value1, value2, value3, value4, value5;
                    decimal perUnit, useperunit, value, usevalue, acres;
                    this.Cursor = Cursors.WaitCursor;
                    string landDetailsxml = string.Empty;

                    ////F8058ResourcesConfigData resourcesConfigData = new F8058ResourcesConfigData();
                    ////F8058ResourcesConfigData.F8058ListResourceConfigDetailsRow resourceConfigDatarow = resourcesConfigData.F8058ListResourceConfigDetails.NewF8058ListResourceConfigDetailsRow();

                    F36035LandData landDetailsData = new F36035LandData();
                    F36035LandData.ListLandValueSliceDetailsRow landDetailsDatarow = landDetailsData.ListLandValueSliceDetails.NewListLandValueSliceDetailsRow();
                    landDetailsDatarow.LandTypeID1 = Convert.ToInt16(this.LandType1Combo.SelectedValue);
                    landDetailsDatarow.LandTypeID2 = Convert.ToInt16(this.LandType2Combo.SelectedValue);
                    landDetailsDatarow.LandTypeID3 = Convert.ToInt16(this.LandType3Combo.SelectedValue);
                    landDetailsDatarow.LandCode = this.LandCodeTextBox.Text;
                    //decimal.TryParse(this.UnitTextBox.Text, out baseValue);
                    //landDetailsDatarow.BaseValue = baseValue;
                    //decimal.TryParse(this.UseUnitTextBox.Text, out usebasevalue);
                    //landDetailsDatarow.UseBaseValue = usebasevalue;
                    landDetailsDatarow.ReportAS = this.ReportAsTextBox.Text;
                    //// Added on 26/9/2008
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    //if (!string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    landDetailsDatarow.Multiplier = multiplier;
                    //}

                    landDetailsDatarow.LotWidth = this.LotWidthTextBox.Text;
                    landDetailsDatarow.LotDepth = this.LotDepthTextBox.Text;

                    decimal.TryParse(this.Break1TextBox.Text, out break1);
                    landDetailsDatarow.Break1 = break1;

                    decimal.TryParse(this.ValuePer1TextBox.Text, out value1);
                    landDetailsDatarow.Value1 = value1;

                    decimal.TryParse(this.Break2TextBox.Text, out break2);
                    landDetailsDatarow.Break2 = break2;

                    decimal.TryParse(this.ValuePer2TextBox.Text, out value2);
                    landDetailsDatarow.Value2 = value2;

                    decimal.TryParse(this.Break3TextBox.Text, out break3);
                    landDetailsDatarow.Break3 = break3;

                    decimal.TryParse(this.ValuePer3TextBox.Text, out value3);
                    landDetailsDatarow.Value3 = value3;

                    decimal.TryParse(this.Break4TextBox.Text, out break4);
                    landDetailsDatarow.Break4 = break4;

                    decimal.TryParse(this.ValuePer4TextBox.Text, out value4);
                    landDetailsDatarow.Value4 = value4;

                    decimal.TryParse(this.Break5TextBox.Text, out break5);
                    landDetailsDatarow.Break5 = break5;

                    decimal.TryParse(this.ValuePer5TextBox.Text, out value5);
                    landDetailsDatarow.Value5 = value5;

                    if (this.AdjusmentTypeComboBox.SelectedValue != null)
                    {
                        landDetailsDatarow.AdjustmentType = Convert.ToByte(this.AdjusmentTypeComboBox.SelectedValue);
                    }
                    else
                    {
                        landDetailsDatarow.AdjustmentType = 0;
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        landDetailsDatarow.UseAdjustmentType = Convert.ToByte(this.UseAdjusmentTypeComboBox.SelectedValue);
                    }
                    else
                    {
                        landDetailsDatarow.UseAdjustmentType = 0;
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue != null)
                    {
                        if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                        {
                            landDetailsDatarow.Adjustment = string.Empty;
                        }

                        if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                        {
                            landDetailsDatarow.Adjustment = this.AdjusmentComboBox.SelectedValue.ToString();
                        }

                        if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                        {
                            landDetailsDatarow.Adjustment = this.AdjustmentTextBox.Text;
                        }

                        if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                        {
                            landDetailsDatarow.Adjustment = this.AdjustmentTextBox.Text;
                        }
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                        {
                            landDetailsDatarow.UseAdjustment = string.Empty;
                        }

                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                        {
                            ////khaja made changes to fix the Bug#4313                           
                            ////landDetailsDatarow.UseAdjustment = this.AdjusmentComboBox.SelectedValue.ToString();
                            landDetailsDatarow.UseAdjustment = this.UseAdjusmentComboBox.SelectedValue.ToString();
                        }

                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                        {
                            landDetailsDatarow.UseAdjustment = this.UseAdjustmentTextBox.Text;
                        }

                        if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                        {
                            landDetailsDatarow.UseAdjustment = this.UseAdjustmentTextBox.Text;
                        }
                    }

                    landDetailsDatarow.AdjDescription = this.ReasonForAdjTextBox.Text;

                    landDetailsDatarow.UseAdjDescription = this.ReasonForUseAdjTextBox.Text;

                    //decimal.TryParse(this.AdjUnitTextBox.Text, out perUnit);
                    //landDetailsDatarow.PerUnit = perUnit;

                    //decimal.TryParse(this.UseAdjUnitTextBox.Text, out useperunit);
                    //landDetailsDatarow.UsePerUnit = useperunit;

                    decimal.TryParse(this.AcresTextBox.Text, out acres);
                    landDetailsDatarow.Units = acres;

                    landDetailsDatarow.UnitType = this.Acreslabel.Text;

                    //decimal.TryParse(this.ValueTextBox.Text, out value);
                    //landDetailsDatarow.Value = value;

                    //decimal.TryParse(this.UseValueTextBox.Text, out usevalue);
                    //landDetailsDatarow.UseValue = usevalue;

                    landDetailsDatarow.ValueSliceID = this.valueSliceID;

                    landDetailsData.ListLandValueSliceDetails.Rows.Add(landDetailsDatarow);
                    landDetailsxml = TerraScanCommon.GetXmlString(landDetailsData.ListLandValueSliceDetails);

                    if (this.selectedRow == -1)
                    {
                        this.selectedRow = 0;
                    }

                    if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.landUniqueID = 0;
                    }
                    else if (this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value != null && !string.IsNullOrEmpty(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value.ToString()))
                    {
                        this.landUniqueID = Convert.ToInt32(this.LandDetailsDataGridView.Rows[this.selectedRow].Cells[this.form36035LandData.ListLandValueSliceDetails.LUIDColumn.ColumnName].Value);
                    }

                    string influenceItems = "<Root />";
                    this.landUniqueID = this.form36099Control.WorkItem.F36035_InsertLandDetails(this.landUniqueID, landDetailsxml, influenceItems, TerraScan.Common.TerraScanCommon.UserId);

                    ////this.CustomizeLandDetailsGrid();
                    this.PopulateLandDetailsGrid();
                    this.LoadLandFrom();
                    this.TotalGridValues();
                    this.flagLoadOnProcess = true;
                    this.ClearTextBoxValues();
                    this.ClearTextBoxValuesForUse();

                    this.flagLoadOnProcess = false;
                    ////to make the focus after save.
                    this.LandType1Combo.Focus();

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.LandDetailsGridViewPanel.Enabled = true;
                    this.LandDetailsDataGridView.Enabled = true;

                    if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > this.LandDetailsDataGridView.NumRowsVisible)
                    {
                        ////this.FooterRightpanel.Width = 18;
                        ////this.FooterRightpanel.Location = new System.Drawing.Point(748, 504);
                        //// this.TotalValueLabel.Width = 84;
                        ////Added by khaja to fix bug#4314
                        this.TotalUseValueLabel.Width = 73;
                        this.TotalUseValueLabel.Location = new System.Drawing.Point(656, -2);
                        this.TotalValueLabel.Width = 66;
                        this.TotalValueLabel.Location = new System.Drawing.Point(520, -1);
                        this.Value.Width = 65;
                        this.LandDetailsVscrollBar.Visible = false;
                    }
                    else
                    {
                        ////this.FooterRightpanel.Width = 21;
                        ////this.FooterRightpanel.Location = new System.Drawing.Point(746, 504);
                        ////this.TotalValueLabel.Width = 82;
                        this.TotalUseValueLabel.Width = 76;
                        this.TotalUseValueLabel.Location = new System.Drawing.Point(671, -2);
                        this.TotalValueLabel.Width = 82;
                        this.TotalValueLabel.Location = new System.Drawing.Point(519, -1);
                        this.Value.Width = 80;
                        this.LandDetailsVscrollBar.Visible = true;
                    }

                    if (!this.flagFormLoad)
                    {
                        TerraScanCommon.SetDataGridViewPosition(this.LandDetailsDataGridView, this.selectedRow);
                    }

                    return true;
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            ////return true;
        }

        /// <summary>
        /// To ClearLandGrid Details
        /// </summary>
        private void ClearLandGridDetails()
        {
            this.ClearLandHeaderDetails();
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            ////made changes to fix Bug#4316 
            ////this.HeaderPanel.Enabled = false;
        }

        /// <summary>
        /// To ClearLandHeaderDetails
        /// </summary>
        private void ClearLandHeaderDetails()
        {
            ////this.landUniqueID = -99;
            this.LandType1Combo.SelectedValue = -1;
            this.LandType2Combo.SelectedValue = -1;
            this.LandType3Combo.SelectedValue = -1;
            this.AdjusmentTypeComboBox.SelectedValue = 0;
            this.LandCodeTextBox.Text = string.Empty;
            this.ReportAsTextBox.Text = string.Empty;
            //this.UnitTextBox.Text = string.Empty;
            this.Break1TextBox.Text = string.Empty;
            this.ValuePer1TextBox.Text = string.Empty;
            this.Break2TextBox.Text = string.Empty;
            this.ValuePer2TextBox.Text = string.Empty;
            this.Break3TextBox.Text = string.Empty;
            this.ValuePer3TextBox.Text = string.Empty;
            this.Break4TextBox.Text = string.Empty;
            this.ValuePer4TextBox.Text = string.Empty;
            this.Break5TextBox.Text = string.Empty;
            this.ValuePer5TextBox.Text = string.Empty;
            this.AdjustmentTextBox.Text = string.Empty;
            this.ReasonForAdjTextBox.Text = string.Empty;

            this.UseAdjustmentTextBox.Text = string.Empty;
            this.ReasonForUseAdjTextBox.Text = string.Empty;
            //this.AdjUnitTextBox.Text = string.Empty;
            this.AcresTextBox.Text = string.Empty;
            //this.ValueTextBox.Text = string.Empty;
            this.AdjusmentComboBox.Visible = false;
            this.AdjusmentComboBox.SelectedValue = -1;
            //this.UseUnitTextBox.Text = string.Empty;
            //this.UseAdjUnitTextBox.Text = string.Empty;
            //this.UseValueTextBox.Text = string.Empty;
            this.LotWidthTextBox.Text = string.Empty;
            this.LotDepthTextBox.Text = string.Empty;
            //this.MultiplierTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.LandDetailsDataGridView.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                //// Code is commented to avoid Form master mode EditEnable 
                //// this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// To Lock All the Controls 
        /// </summary>
        /// <param name="controlLook">Boolean value</param>
        private void ControlLock(bool controlLook)
        {
            this.AdjustmentTextBox.LockKeyPress = controlLook;
            this.UseAdjustmentTextBox.LockKeyPress = controlLook;
            this.ReasonForAdjTextBox.LockKeyPress = controlLook;
            this.ReasonForUseAdjTextBox.LockKeyPress = controlLook;
            this.AcresTextBox.LockKeyPress = controlLook;
            this.LandType1Combo.Enabled = !controlLook;
            this.LandType2Combo.Enabled = !controlLook;
            this.LandType3Combo.Enabled = !controlLook;
            this.AdjusmentTypeComboBox.Enabled = !controlLook;
            ////this.LotWidthTextBox.LockKeyPress = !controlLook;
            ////this.LotDepthTextBox.LockKeyPress = !controlLook;  

            ////khaja added code to disable Attachment & Comment Buttons
            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                this.additionalOperationSmartPart.Enabled = false;
            }
            else
            {
                if (this.LandDetailsDataGridView.OriginalRowCount > 0)
                {
                    this.additionalOperationSmartPart.Enabled = !controlLook;
                }
                else
                {
                    this.additionalOperationSmartPart.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Sets the panel enable.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void SetPanelEnable(bool enable)
        {
            this.Break1panel.Enabled = enable;
            this.ValuePer1panel.Enabled = enable;
            this.Break2panel.Enabled = enable;
            this.ValuePer2panel.Enabled = enable;
            this.Break3panel.Enabled = enable;
            this.ValuePer3panel.Enabled = enable;
            this.Break4panel.Enabled = enable;
            this.ValuePer4panel.Enabled = enable;
            this.Break5panel.Enabled = enable;
            this.ValuePer5panel.Enabled = enable;
        }

        /// <summary>
        /// Sets the panel colors.
        /// </summary>
        private void SetPanelColorsForFactor()
        {
            this.Break1panel.BackColor = System.Drawing.Color.LightGray;
            this.Break1label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break1TextBox.ForeColor = System.Drawing.Color.LightGray;
            this.Break1TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer1label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer1TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer1panel.BackColor = System.Drawing.Color.LightGray;

            this.Break2panel.BackColor = System.Drawing.Color.LightGray;
            this.Break2label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break2TextBox.ForeColor = System.Drawing.Color.LightGray;
            this.Break2TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer2label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer2TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer2panel.BackColor = System.Drawing.Color.LightGray;

            this.Break3panel.BackColor = System.Drawing.Color.LightGray;
            this.Break3label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break3TextBox.ForeColor = System.Drawing.Color.LightGray;
            this.Break3TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer3label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer3TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer3panel.BackColor = System.Drawing.Color.LightGray;

            this.Break4panel.BackColor = System.Drawing.Color.LightGray;
            this.Break4label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break4TextBox.ForeColor = System.Drawing.Color.LightGray;
            this.Break4TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer4label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer4TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer4panel.BackColor = System.Drawing.Color.LightGray;

            this.Break5panel.BackColor = System.Drawing.Color.LightGray;
            this.Break5label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break5TextBox.ForeColor = System.Drawing.Color.LightGray;
            this.Break5TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer5label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer5TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer5panel.BackColor = System.Drawing.Color.LightGray;
        }

        /// <summary>
        /// Sets the panel colors for none.
        /// </summary>
        private void SetPanelColorsForNone()
        {
            this.Break1panel.BackColor = System.Drawing.Color.LightGray;
            this.Break1label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break1TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Break1TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer1label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer1TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer1TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer1panel.BackColor = System.Drawing.Color.LightGray;

            this.Break2panel.BackColor = System.Drawing.Color.LightGray;
            this.Break2label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break2TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Break2TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer2label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer2TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer2TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer2panel.BackColor = System.Drawing.Color.LightGray;

            this.Break3panel.BackColor = System.Drawing.Color.LightGray;
            this.Break3label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break3TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Break3TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer3label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer3TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer3TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer3panel.BackColor = System.Drawing.Color.LightGray;

            this.Break4panel.BackColor = System.Drawing.Color.LightGray;
            this.Break4label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break4TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Break4TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer4label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer4TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer4TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer4panel.BackColor = System.Drawing.Color.LightGray;

            this.Break5panel.BackColor = System.Drawing.Color.LightGray;
            this.Break5label.ForeColor = System.Drawing.Color.DarkGray;
            this.Break5TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.Break5TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer5label.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer5TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer5TextBox.ForeColor = System.Drawing.Color.DarkGray;
            this.ValuePer5panel.BackColor = System.Drawing.Color.LightGray;
        }

        /// <summary>
        /// Panels the color for none.
        /// </summary>
        private void PanelColorForNone()
        {
            this.Break1panel.BackColor = System.Drawing.Color.LightGray;
            this.Break1label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.Break1TextBox.ForeColor = System.Drawing.Color.Black;
            this.Break1TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer1label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.ValuePer1TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer1TextBox.ForeColor = System.Drawing.Color.Black;
            this.ValuePer1panel.BackColor = System.Drawing.Color.LightGray;

            this.Break2panel.BackColor = Color.FromArgb(227, 255, 227);
            this.Break2label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.Break2TextBox.ForeColor = System.Drawing.Color.Black;
            this.Break2TextBox.BackColor = Color.FromArgb(227, 255, 227);

            this.ValuePer2panel.BackColor = Color.FromArgb(227, 255, 227);
            this.ValuePer2label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.ValuePer2TextBox.ForeColor = System.Drawing.Color.Black;
            this.ValuePer2TextBox.BackColor = Color.FromArgb(227, 255, 227);

            this.Break3panel.BackColor = System.Drawing.Color.LightGray;
            this.Break3label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.Break3TextBox.ForeColor = System.Drawing.Color.Black;
            this.Break3TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer3label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.ValuePer3TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer3TextBox.ForeColor = System.Drawing.Color.Black;
            this.ValuePer3panel.BackColor = System.Drawing.Color.LightGray;

            this.Break4panel.BackColor = Color.FromArgb(227, 255, 227);
            this.Break4label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.Break4TextBox.ForeColor = System.Drawing.Color.Black;
            this.Break4TextBox.BackColor = Color.FromArgb(227, 255, 227);

            this.ValuePer4label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.ValuePer4TextBox.BackColor = Color.FromArgb(227, 255, 227);
            this.ValuePer4TextBox.ForeColor = System.Drawing.Color.Black;
            this.ValuePer4panel.BackColor = Color.FromArgb(227, 255, 227);

            this.Break5panel.BackColor = System.Drawing.Color.LightGray;
            this.Break5label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.Break5TextBox.ForeColor = System.Drawing.Color.Black;
            this.Break5TextBox.BackColor = System.Drawing.Color.LightGray;

            this.ValuePer5label.ForeColor = System.Drawing.Color.FromArgb(51, 51, 153);
            this.ValuePer5TextBox.BackColor = System.Drawing.Color.LightGray;
            this.ValuePer5TextBox.ForeColor = System.Drawing.Color.Black;
            this.ValuePer5panel.BackColor = System.Drawing.Color.LightGray;
        }

        /// <summary>
        /// Sets the panel color for use none.
        /// </summary>
        private void SetPanelColorForUseNone()
        {
            this.UseAdjusmentComboBox.Visible = false;

            this.UseAdjustmentpanel.Enabled = false;
            this.UseAdjustmentlabel.ForeColor = System.Drawing.Color.DarkGray;
            this.UseAdjustmentpanel.BackColor = System.Drawing.Color.LightGray;
            this.UseAdjustmentTextBox.BackColor = System.Drawing.Color.LightGray;

            this.ReasonForUseAdjpanel.Enabled = false;
            this.ReasonForUseAdjusmentlabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ReasonForUseAdjpanel.BackColor = System.Drawing.Color.LightGray;
            this.ReasonForUseAdjTextBox.BackColor = System.Drawing.Color.LightGray;

            //this.UseAdjUnitpanel.Enabled = false;
            //this.UseAdjUnitlabel.ForeColor = System.Drawing.Color.DarkGray;
            //this.UseAdjUnitpanel.BackColor = System.Drawing.Color.LightGray;
            //this.UseAdjUnitTextBox.BackColor = System.Drawing.Color.LightGray;
        }

        /// <summary>
        /// Sets the panel color for use factor.
        /// </summary>
        private void SetPanelColorForUseFactor()
        {
            this.UseAdjustmentpanel.Enabled = true;
            this.UseAdjustmentTextBox.Enabled = true;
            this.UseAdjustmentTextBox.Visible = true;
            ////this.AdjusmentComboBox.Enabled = false;
            this.UseAdjusmentComboBox.Visible = false;

            this.UseAdjustmentTextBox.Enabled = true;
            this.UseAdjustmentpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
            this.UseAdjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.UseAdjustmentTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;

            //this.UseAdjUnitpanel.Enabled = true;
            //this.UseAdjUnitpanel.Enabled = true;
            //this.UseAdjUnitpanel.BackColor = Color.FromArgb(253, 239, 227);  ////System.Drawing.Color.White;
            //this.UseAdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
            //this.UseAdjUnitTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;

            this.ReasonForUseAdjpanel.Enabled = true;
            this.ReasonForUseAdjpanel.Enabled = true;
            this.ReasonForUseAdjpanel.Enabled = true;
            this.ReasonForUseAdjpanel.BackColor = Color.FromArgb(253, 239, 227);  ////System.Drawing.Color.White;
            this.ReasonForUseAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.ReasonForUseAdjTextBox.BackColor = Color.FromArgb(253, 239, 227);  ////System.Drawing.Color.White;
        }

        /// <summary>
        /// Method to set the controls
        /// </summary>
        private void SetForFactor()
        {
            this.AdjustmentTextBox.Enabled = true;
            this.AdjustmentTextBox.Visible = true;
            ////this.AdjusmentComboBox.Enabled = false;
            this.AdjusmentComboBox.Visible = false;

            this.Adjustmentpanel.Enabled = true;
            this.Adjustmentpanel.BackColor = System.Drawing.Color.White;
            this.Adjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.AdjustmentTextBox.BackColor = System.Drawing.Color.White;

            //this.AdjUnitpanel.Enabled = true;
            //this.AdjUnitpanel.Enabled = true;
            //this.AdjUnitpanel.BackColor = System.Drawing.Color.White;
            //this.AdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
            //this.AdjUnitTextBox.BackColor = System.Drawing.Color.White;

            this.ReasonForAdjpanel.Enabled = true;
            this.ReasonForAdjpanel.Enabled = true;
            this.ReasonForAdjpanel.Enabled = true;
            this.ReasonForAdjpanel.BackColor = System.Drawing.Color.White;
            this.ReasonForAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.ReasonForAdjTextBox.BackColor = System.Drawing.Color.White;

            //decimal multiplier;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);

            if (!this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                this.SetPanelColorsForFactor();
                this.SetPanelEnable(false);
            }
        }

        /// <summary>
        /// Sets for use factor.
        /// </summary>
        private void SetForUseFactor()
        {
            this.SetPanelColorForUseFactor();

            //decimal multiplier;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);

            if (!this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                this.SetPanelColorsForFactor();
                this.SetPanelEnable(false);
            }

            if (!this.UseAdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                ////this.SetPanelColorsForFactor();
                this.SetPanelEnable(false);
            }
        }

        /// <summary>
        /// Set for None
        /// </summary>
        private void SetForNone()
        {
            this.AdjusmentComboBox.Visible = false;

            this.Adjustmentpanel.Enabled = false;
            this.Adjustmentlabel.ForeColor = System.Drawing.Color.DarkGray;
            this.Adjustmentpanel.BackColor = System.Drawing.Color.LightGray;
            this.AdjustmentTextBox.BackColor = System.Drawing.Color.LightGray;

            this.ReasonForAdjpanel.Enabled = false;
            this.ReasonForAdjusmentlabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ReasonForAdjpanel.BackColor = System.Drawing.Color.LightGray;
            this.ReasonForAdjTextBox.BackColor = System.Drawing.Color.LightGray;

            //this.AdjUnitpanel.Enabled = false;
            //this.AdjUnitlabel.ForeColor = System.Drawing.Color.DarkGray;
            //this.AdjUnitpanel.BackColor = System.Drawing.Color.LightGray;
            //this.AdjUnitTextBox.BackColor = System.Drawing.Color.LightGray;

            //decimal multiplier;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                this.SetPanelColorsForNone();
                ////this.PanelColorForNone();
                this.SetPanelEnable(true);
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                ////this.SetPanelColorsForNone();
                this.PanelColorForNone();
                this.SetPanelEnable(true);
            }
        }

        /// <summary>
        /// Sets for use none.
        /// </summary>
        private void SetForUseNone()
        {
            this.SetPanelColorForUseNone();

            //decimal multiplier;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                ////this.SetPanelColorsForNone();
                ////this.PanelColorForNone();
                this.SetPanelEnable(true);
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                this.PanelColorForNone();
                this.SetPanelEnable(true);
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals(0))
            {
                ////this.PanelColorForNone();
                this.UseAdjustmentTextBox.Text = string.Empty;
                //this.UseAdjUnitTextBox.Text = string.Empty;
                this.ReasonForUseAdjTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// SetForLandCode
        /// </summary>
        private void SetForLandCode()
        {
            this.AdjusmentComboBox.BringToFront();
            this.AdjusmentComboBox.Enabled = true;
            this.AdjusmentComboBox.Visible = true;
            this.AdjustmentTextBox.Enabled = false;
            this.Adjustmentpanel.Enabled = true;
            this.Adjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.Adjustmentpanel.BackColor = System.Drawing.Color.White;
            this.AdjustmentTextBox.BackColor = System.Drawing.Color.White;

            this.ReasonForAdjpanel.Enabled = true;
            this.ReasonForAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.ReasonForAdjpanel.BackColor = System.Drawing.Color.White;
            this.ReasonForAdjTextBox.BackColor = System.Drawing.Color.White;
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.ReasonForAdjTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.AdjDescriptionColumn].ToString();
            }
            else
            {
                this.ReasonForAdjTextBox.Text = string.Empty;
            }

            //this.AdjUnitpanel.Enabled = true;
            //this.AdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
            //this.AdjUnitpanel.BackColor = System.Drawing.Color.White;
            //this.AdjUnitTextBox.BackColor = System.Drawing.Color.White;
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                ////khaja made changes to fix Bug#4304
                ////this.AdjUnitTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn].ToString();
                //this.AdjUnitTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.BaseValueColumn].ToString();
            }
            else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                //this.AdjUnitTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets for use land code.
        /// </summary>
        private void SetForUseLandCode()
        {
            this.UseAdjusmentComboBox.BringToFront();
            this.UseAdjusmentComboBox.Enabled = true;
            this.UseAdjusmentComboBox.Visible = true;
            this.UseAdjustmentTextBox.Enabled = false;
            this.UseAdjustmentpanel.Enabled = true;
            this.UseAdjustmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.UseAdjustmentpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
            this.UseAdjustmentTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;

            this.ReasonForUseAdjpanel.Enabled = true;
            this.ReasonForUseAdjusmentlabel.ForeColor = Color.FromArgb(51, 51, 153);
            this.ReasonForUseAdjpanel.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
            this.ReasonForUseAdjTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.ReasonForUseAdjTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.UseAdjDescriptionColumn].ToString();
            }
            else
            {
                this.ReasonForUseAdjTextBox.Text = string.Empty;
            }

            //this.UseAdjUnitpanel.Enabled = true;
            //this.UseAdjUnitlabel.ForeColor = Color.FromArgb(51, 51, 153);
            //this.UseAdjUnitpanel.BackColor = Color.FromArgb(253, 239, 227);  ////System.Drawing.Color.White;
            //this.UseAdjUnitTextBox.BackColor = Color.FromArgb(253, 239, 227); ////System.Drawing.Color.White;
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                ////khaja made changes to fix Bug#4304
                ////this.UseAdjUnitTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.PerUnitColumn].ToString();                
                //this.UseAdjUnitTextBox.Text = this.form36035LandData.ListLandValueSliceDetails.Rows[0][this.form36035LandData.ListLandValueSliceDetails.UseBaseValueColumn].ToString();
            }
            else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
                //this.UseAdjUnitTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// To get the LandCode,ReportAs,Unit.
        /// </summary>
        private void GetLandCode()
        {
            this.formLandCodeData.Get_LandCode.Clear();
            int landType1, landType2, landType3;
            if (this.LandType1Combo.SelectedValue != null)
            {
                int.TryParse(this.LandType1Combo.SelectedValue.ToString(), out landType1);
            }
            else
            {
                landType1 = 0;
            }

            if (this.LandType2Combo.SelectedValue != null)
            {
                int.TryParse(this.LandType2Combo.SelectedValue.ToString(), out landType2);
            }
            else
            {
                landType2 = 0;
            }

            if (this.LandType3Combo.SelectedValue != null)
            {
                int.TryParse(this.LandType3Combo.SelectedValue.ToString(), out landType3);
            }
            else
            {
                landType3 = 0;
            }

            this.formLandCodeData = this.form36099Control.WorkItem.F36035_GetLandCode(this.rollYear, landType1, landType2, landType3, this.valueSliceID,null);
            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.LandCodeTextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.LandCodeColumn].ToString();
                this.ReportAsTextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.ReportASColumn].ToString();
                //this.UnitTextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.BaseValueColumn].ToString();
                //this.UseUnitTextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.UseBaseValueColumn].ToString();
                this.Break1TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Break1Column].ToString();
                this.Break2TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Break2Column].ToString();
                this.Break3TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Break3Column].ToString();
                this.Break4TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Break4Column].ToString();
                //this.Break5TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Break5Column].ToString();
                this.ValuePer1TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Value1Column].ToString();
                this.ValuePer2TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Value2Column].ToString();
                this.ValuePer3TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Value3Column].ToString();
                this.ValuePer4TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Value4Column].ToString();
                //this.ValuePer5TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.Value5Column].ToString();
                this.Acreslabel.Text = this.formLandCodeData.Get_LandCode.Rows[0][formLandCodeData.Get_LandCode.UnitTypeColumn].ToString();

                //decimal multiplier, unit, useunit;
                //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                //decimal.TryParse(this.UnitTextBox.Text, out unit);
                //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
                //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                //{
                //    multiplier = 1;
                //}

                //this.unitBaseValue = unit;
                //this.useunitBaseValue = useunit;
                //this.UnitTextBox.Text = Convert.ToString(unit * multiplier);
                //this.UseUnitTextBox.Text = Convert.ToString(useunit * multiplier);
            }
            else
            {
                this.LandCodeTextBox.Text = string.Empty;
                this.ReportAsTextBox.Text = string.Empty;
                //this.UnitTextBox.Text = string.Empty;
                //this.UseUnitTextBox.Text = string.Empty;
                this.Break1TextBox.Text = string.Empty;
                this.Break2TextBox.Text = string.Empty;
                this.Break3TextBox.Text = string.Empty;
                this.Break4TextBox.Text = string.Empty;
                this.Break5TextBox.Text = string.Empty;
                this.ValuePer1TextBox.Text = string.Empty;
                this.ValuePer2TextBox.Text = string.Empty;
                this.ValuePer3TextBox.Text = string.Empty;
                this.ValuePer4TextBox.Text = string.Empty;
                this.ValuePer5TextBox.Text = string.Empty;
                this.AcresTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// CalculationForNone()
        /// </summary>
        private void CalculationForNone()
        {
            decimal availableQuantity = 0;
            decimal valueCalculated = 0;
            decimal break1, value1, break2, value2, break3, value3;
            decimal break4, value4, break5, value5;
            decimal unit, useunit;

            decimal.TryParse(this.Break1TextBox.Text, out break1);
            decimal.TryParse(this.Break2TextBox.Text, out break2);
            decimal.TryParse(this.Break3TextBox.Text, out break3);
            decimal.TryParse(this.Break4TextBox.Text, out break4);
            decimal.TryParse(this.Break5TextBox.Text, out break5);
            decimal.TryParse(this.ValuePer1TextBox.Text, out value1);
            decimal.TryParse(this.ValuePer2TextBox.Text, out value2);
            decimal.TryParse(this.ValuePer3TextBox.Text, out value3);
            decimal.TryParse(this.ValuePer4TextBox.Text, out value4);
            decimal.TryParse(this.ValuePer5TextBox.Text, out value5);
            //decimal.TryParse(this.UnitTextBox.Text, out unit);
            //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
            decimal.TryParse(this.AcresTextBox.Text, out availableQuantity);
            if (break1 > 0)
            {
                if (availableQuantity >= break1)
                {
                    //valueCalculated += break1 * unit;
                    availableQuantity = availableQuantity - break1;
                }
                else
                {
                    //valueCalculated += availableQuantity * unit;
                    availableQuantity = 0;
                }
            }
            else if (break1.Equals(0))
            {
                //valueCalculated += break1 * unit;
                availableQuantity = availableQuantity - break1;
            }

            if (break2 > 0)
            {
                if (availableQuantity >= break2)
                {
                    valueCalculated += (break2 - break1) * value1;
                    availableQuantity = availableQuantity - (break2 - break1);
                }
                else
                {
                    valueCalculated += availableQuantity * value1;
                    availableQuantity = 0;
                }
            }
            else if (break2.Equals(0))
            {
                valueCalculated += availableQuantity * value1;
                availableQuantity = 0;
            }

            if (break3 > 0)
            {
                if (availableQuantity >= break3)
                {
                    valueCalculated += (break3 - break2) * value2;
                    availableQuantity = availableQuantity - (break3 - break2);
                }
                else
                {
                    valueCalculated += availableQuantity * value2;
                    availableQuantity = 0;
                }
            }
            else if (break3.Equals(0))
            {
                valueCalculated += availableQuantity * value2;
                availableQuantity = 0;
            }

            if (break4 > 0)
            {
                if (availableQuantity >= break4)
                {
                    valueCalculated += (break4 - break3) * value3;
                    availableQuantity = availableQuantity - (break4 - break3);
                }
                else
                {
                    valueCalculated += availableQuantity * value3;
                    availableQuantity = 0;
                }
            }
            else if (break4.Equals(0))
            {
                valueCalculated += availableQuantity * value3;
                availableQuantity = 0;
            }

            if (break5 > 0)
            {
                if (availableQuantity >= break5)
                {
                    valueCalculated += (break5 - break4) * value4;
                    availableQuantity = availableQuantity - (break5 - break4);
                }
                else
                {
                    valueCalculated += availableQuantity * value4;
                    availableQuantity = 0;
                }
            }
            else if (break5.Equals(0))
            {
                valueCalculated += availableQuantity * value4;
                availableQuantity = 0;
            }

            if (availableQuantity > 0)
            {
                if (break1 > 0)
                {
                    valueCalculated += availableQuantity * value5;
                }
                else
                {
                    //valueCalculated += availableQuantity * unit;
                }
            }

            ////this.ValueTextBox.Text = valueCalculated.ToString();
            ////this.UseValueTextBox.Text = valueCalculated.ToString();

            decimal acres;
            ////if (string.IsNullOrEmpty(this.Break1TextBox.Text))
            ////{
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
            //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
            //decimal.TryParse(this.UnitTextBox.Text, out unit);
            //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);

            decimal.TryParse(this.AcresTextBox.Text, out acres);

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                ////this.maxvalue = Convert.ToDouble((multiplier * unit) * acres);
                //this.maxvalue = Convert.ToDouble(unit * acres);
                if (this.maxvalue < this.moneyMaxValue)
                {
                    ////this.ValueTextBox.Text = Convert.ToString((multiplier * unit) * acres);
                    //this.ValueTextBox.Text = Convert.ToString(unit * acres);
                }
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                ////this.maxusevalue = Convert.ToDouble((multiplier * useunit) * acres);
                //this.maxusevalue = Convert.ToDouble(useunit * acres);
                if (this.maxusevalue < this.moneyMaxValue)
                {
                    ////this.UseValueTextBox.Text = Convert.ToString((multiplier * useunit) * acres);
                    //this.UseValueTextBox.Text = Convert.ToString(useunit * acres);
                }
            }
            //// }
            ////this.UseAdjUnitTextBox.Text = useunit.ToString();
            ////decimal acresvalue;
            ////decimal.TryParse(this.AcresTextBox.Text,out acresvalue);
            ////this.UseValueTextBox.Text = Convert.ToString(acresvalue* useunit);
        }

        /// <summary>
        /// CalculationForLandCode1()
        /// </summary>
        private void CalculationForLandCode1()
        {
            decimal adjUnit, acreUnits, unit;
            //decimal.TryParse(this.UnitTextBox.Text, out unit);
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);

            //decimal.TryParse(this.AdjUnitTextBox.Text, out adjUnit);
            //this.AdjUnitTextBox.Text = Convert.ToString(multiplier * adjUnit);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
            //this.ValueTextBox.Text = Convert.ToString(multiplier * adjUnit * acreUnits);
            //// this.UseValueTextBox.Text = Convert.ToString(acreUnits * unit);
        }

        /// <summary>
        /// Calculations for use land code1.
        /// </summary>
        private void CalculationForUseLandCode1()
        {
            decimal adjUnit, acreUnits, useunit;
            //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
            //decimal.TryParse(this.UseAdjUnitTextBox.Text, out adjUnit);
            //this.UseAdjUnitTextBox.Text = Convert.ToString(multiplier * adjUnit);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
            ////this.ValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
            ////decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
            //this.UseValueTextBox.Text = Convert.ToString(multiplier * adjUnit * acreUnits);
        }

        /// <summary>`
        /// CalculationForFactor2()
        /// </summary>
        private void CalculationForFactor2()
        {
            decimal adjustmentValue, acreUnits, unit, tempcalc;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
            //decimal.TryParse(this.UnitTextBox.Text, out unit);
            decimal.TryParse(this.AdjustmentTextBox.Text, out adjustmentValue);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
            ////decimal.TryParse(this.AdjUnitTextBox.Text, out adjUnit);
            ////this.ValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
            ////this.AdjUnitTextBox.Text = Convert.ToString((multiplier * unit) * adjustmentValue);
            //this.AdjUnitTextBox.Text = Convert.ToString(unit * adjustmentValue);
            //tempcalc = (unit * adjustmentValue);
            ////tempcalc = (multiplier * unit) * adjustmentValue;
            //this.ValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
        }

        /// <summary>
        /// Calculations for use factor2.
        /// </summary>
        private void CalculationForUseFactor2()
        {
            decimal adjustmentValue, acreUnits, unit, tempcalc, tmpcalc;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);

            //decimal.TryParse(this.UseUnitTextBox.Text, out unit);
            decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustmentValue);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);

            ////decimal.TryParse(this.AdjUnitTextBox.Text, out adjUnit);
            ////this.ValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
            ////decimal.TryParse(this.UnitTextBox.Text, out unit);
            ////this.UseAdjUnitTextBox.Text = Convert.ToString((multiplier * unit) * adjustmentValue);
            //this.UseAdjUnitTextBox.Text = Convert.ToString(unit * adjustmentValue);

            ////tempcalc = multiplier * unit;
            //tempcalc = unit;
            tmpcalc = adjustmentValue * acreUnits;
            //this.UseValueTextBox.Text = Convert.ToString(tempcalc * tmpcalc);
        }

        /// <summary>
        /// CalculationForValue3()
        /// </summary>
        private void CalculationForValue3()
        {
            decimal adjustmentValue, acreUnits;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
            decimal.TryParse(this.AdjustmentTextBox.Text, out adjustmentValue);
            //// decimal.TryParse(this.AdjUnitTextBox.Text, out adjunitvalue);

            //this.AdjUnitTextBox.Text = Convert.ToString(multiplier * adjustmentValue);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
            //this.ValueTextBox.Text = Convert.ToString(multiplier * adjustmentValue * acreUnits);
            ////this.UseAdjUnitTextBox.Text = adjustmentValue.ToString();
            ////this.UseValueTextBox.Text = Convert.ToString(acreUnits * adjustmentValue);
        }

        /// <summary>
        /// Calculations for use value3.
        /// </summary>
        private void CalculationForUseValue3()
        {
            decimal adjustmentValue, acreUnits;
            //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
            //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
            //{
            //    multiplier = 1;
            //}

            //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
            decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustmentValue);
            decimal.TryParse(this.AcresTextBox.Text, out acreUnits);

            ////decimal.TryParse(this.UseAdjUnitTextBox.Text, out adjunitvalue);
            //// this.AdjUnitTextBox.Text = Convert.ToString(adjustmentValue);
            ////this.ValueTextBox.Text = Convert.ToString(adjustmentValue * acreUnits);
            ////this.UseAdjUnitTextBox.Text = adjustmentValue.ToString();

            //this.UseAdjUnitTextBox.Text = Convert.ToString(multiplier * adjustmentValue);
            //this.UseValueTextBox.Text = Convert.ToString((multiplier * adjustmentValue) * acreUnits);
        }

        /// <summary>
        /// Method to set None-Landcode values
        /// </summary>
        private void SetForNoneLandCodeValues()
        {
            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.Break1TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Break1Column].ToString();
            }
            else
            {
                this.Break1TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.ValuePer1TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Value1Column].ToString();
            }
            else
            {
                this.ValuePer1TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.Break2TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Break2Column].ToString();
            }
            else
            {
                this.Break2TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.ValuePer2TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Value2Column].ToString();
            }
            else
            {
                this.ValuePer2TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.Break3TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Break3Column].ToString();
            }
            else
            {
                this.Break3TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.ValuePer3TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Value3Column].ToString();
            }
            else
            {
                this.ValuePer3TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.Break4TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Break4Column].ToString();
            }
            else
            {
                this.Break4TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                this.ValuePer4TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Value4Column].ToString();
            }
            else
            {
                this.ValuePer4TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                //this.Break5TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Break5Column].ToString();
            }
            else
            {
                this.Break5TextBox.Text = string.Empty;
            }

            if (this.formLandCodeData.Get_LandCode.Rows.Count > 0)
            {
                //this.ValuePer5TextBox.Text = this.formLandCodeData.Get_LandCode.Rows[0][this.formLandCodeData.Get_LandCode.Value5Column].ToString();
            }
            else
            {
                this.ValuePer5TextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// To make the total of gridview values.
        /// </summary>
        private void TotalGridValues()
        {
            if (this.form36035LandData != null && this.form36035LandData.ListLandValueSliceDetails != null)
            {
                if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
                {
                    object totalUnits;
                    object totalValue;
                    object totalUseValue;

                    totalUnits = Convert.ToString(this.form36035LandData.ListLandValueSliceDetails.Compute("SUM(Units)", "1=1"));
                    totalValue = Convert.ToString(this.form36035LandData.ListLandValueSliceDetails.Compute("SUM(Value)", "1=1"));
                    totalUseValue = Convert.ToString(this.form36035LandData.ListLandValueSliceDetails.Compute("SUM(UseValue)", "1=1"));

                    this.UnitsTotalOverrideTextBox.Text = totalUnits.ToString();
                    this.ValueTotalOverrideTextBox.Text = totalValue.ToString();
                    this.UseValueTotalOverrideTextBox.Text = totalUseValue.ToString();

                    this.UnitTotallabel.Text = this.UnitsTotalOverrideTextBox.Text.Trim();
                    this.TotalValueLabel.Text = this.ValueTotalOverrideTextBox.Text.Trim();
                    this.TotalUseValueLabel.Text = this.UseValueTotalOverrideTextBox.Text.Trim();
                }
                else
                {
                    this.LandDetailsDataGridView.DataSource = null;
                    this.UnitTotallabel.Text = string.Empty;
                    this.TotalValueLabel.Text = string.Empty;
                    this.TotalUseValueLabel.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// To show or hide the controls
        /// </summary>
        /// <param name="show">show</param>
        private void ShowPanel(bool show)
        {
            this.LandType1Panel.Enabled = show;
            this.LandType2panel.Enabled = show;
            this.LantType3panel.Enabled = show;
            this.LandCodePanel.Enabled = show;
            this.ReportAspanel.Enabled = show;
            this.BaseValuepanel.Enabled = show;
            this.LotDepthPanel.Enabled = show;
            this.LotWidthPanel.Enabled = show;
            this.UnitTypePanel.Enabled = show;
            this.Break1panel.Enabled = show;
            this.Break1label.Enabled = show;
            this.Break1TextBox.Enabled = show;
            this.Break2panel.Enabled = show;
            this.Break2label.Enabled = show;
            this.Break2TextBox.Enabled = show;
            this.Break3panel.Enabled = show;
            this.Break3label.Enabled = show;
            this.Break3TextBox.Enabled = show;
            this.Break4panel.Enabled = show;
            this.Break4label.Enabled = show;
            this.Break4TextBox.Enabled = show;
            this.Break5panel.Enabled = show;
            this.Break5label.Enabled = show;
            this.Break5TextBox.Enabled = show;
            this.ValuePer1panel.Enabled = show;
            this.ValuePer1label.Enabled = show;
            this.ValuePer1TextBox.Enabled = show;
            this.ValuePer2panel.Enabled = show;
            this.ValuePer2label.Enabled = show;
            this.ValuePer2TextBox.Enabled = show;
            this.ValuePer3panel.Enabled = show;
            this.ValuePer3label.Enabled = show;
            this.ValuePer3TextBox.Enabled = show;
            this.ValuePer4panel.Enabled = show;
            this.ValuePer4label.Enabled = show;
            this.ValuePer4TextBox.Enabled = show;
            this.ValuePer5panel.Enabled = show;
            this.ValuePer5label.Enabled = show;
            this.ValuePer5TextBox.Enabled = show;
            this.Adjustmentpanel.Enabled = show;
            this.AdjusmentTypePanel.Enabled = show;
            //this.AdjUnitpanel.Enabled = show;
            this.Acrespanel.Enabled = show;
            this.BaseMarketValuepanel.Enabled = show;
            ////Adding for Co on 14/7/2008
            this.UseBaseDollarsPerAcrepanel.Enabled = show;
            //this.UseAdjUnitpanel.Enabled = show;
            this.FinalValuepanel.Enabled = show;

            ////Adding for Co on 24/9/2008
            this.UseAdjusmentTypePanel.Enabled = show;
            this.UseAdjustmentpanel.Enabled = show;
            this.ReasonForUseAdjpanel.Enabled = show;
            this.LandDetailsGridViewPanel.Enabled = show;
            this.LandDetailsDataGridView.Enabled = show;
            this.LandDetailsDataGridView.CurrentCell = null;
            this.LandDetailsDataGridView.Rows[0].Selected = show;
            this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = show;
            this.Footerpanel.Enabled = show;
            if ((this.pageMode == TerraScanCommon.PageModeTypes.New))
            {
                this.LandDetailsGridViewPanel.Enabled = false;
                this.LandDetailsDataGridView.Enabled = false;
                this.LandDetailsDataGridView.CurrentCell = null;
                this.LandDetailsDataGridView.Rows[0].Selected = false;
                this.LandDetailsDataGridView.Rows[0].Cells[0].Selected = false;
            }
            else
            {
                this.LandDetailsGridViewPanel.Focus();
                this.LandDetailsDataGridView.Focus();
                this.LandDetailsGridViewPanel.Enabled = true;
                this.LandDetailsDataGridView.Enabled = true;
            }
        }

        /// <summary>
        /// Clears the text box values.
        /// </summary>
        private void ClearTextBoxValues()
        {
            if (!this.Break1panel.Enabled)
            {
                this.Break1TextBox.Text = string.Empty;
                this.ValuePer1TextBox.Text = string.Empty;
                this.Break2TextBox.Text = string.Empty;
                this.ValuePer2TextBox.Text = string.Empty;
                this.Break3TextBox.Text = string.Empty;
                this.ValuePer3TextBox.Text = string.Empty;
                this.Break4TextBox.Text = string.Empty;
                this.ValuePer4TextBox.Text = string.Empty;
                this.Break5TextBox.Text = string.Empty;
                this.ValuePer5TextBox.Text = string.Empty;
            }
            else
            {
                ////this.AdjustmentTextBox.Text = string.Empty;
                ////this.AdjUnitTextBox.Text = string.Empty;
                ////this.ReasonForAdjTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Clears the text box values for use.
        /// </summary>
        private void ClearTextBoxValuesForUse()
        {
            if (!this.Break1panel.Enabled)
            {
                this.Break1TextBox.Text = string.Empty;
                this.ValuePer1TextBox.Text = string.Empty;
                this.Break2TextBox.Text = string.Empty;
                this.ValuePer2TextBox.Text = string.Empty;
                this.Break3TextBox.Text = string.Empty;
                this.ValuePer3TextBox.Text = string.Empty;
                this.Break4TextBox.Text = string.Empty;
                this.ValuePer4TextBox.Text = string.Empty;
                this.Break5TextBox.Text = string.Empty;
                this.ValuePer5TextBox.Text = string.Empty;
            }
            else
            {
                ////this.UseAdjustmentTextBox.Text = string.Empty;
                ////this.UseAdjUnitTextBox.Text = string.Empty;
                ////this.ReasonForUseAdjTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Disables the panels.
        /// </summary>
        private void DisablePanels()
        {
            if (this.form36035LandData.ListLandValueSliceDetails.Rows.Count > 0)
            {
                this.ShowPanel(true);
            }
            else
            {
                this.ShowPanel(false);
            }
        }

        /// <summary>
        /// Clears the value text box.
        /// </summary>
        private void ClearValueTextBox()
        {
            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                if (string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.ValueTextBox.Text = string.Empty;
                }
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
            {
                if (string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.ValueTextBox.Text = string.Empty;
                }
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
            {
                if (string.IsNullOrEmpty(this.AdjustmentTextBox.Text) && string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.AdjUnitTextBox.Text = string.Empty;
                    //this.ValueTextBox.Text = string.Empty;
                }
            }

            if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
            {
                if (string.IsNullOrEmpty(this.AdjustmentTextBox.Text) && string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.AdjUnitTextBox.Text = string.Empty;
                    //this.ValueTextBox.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Clears the value text box for use.
        /// </summary>
        private void ClearValueTextBoxForUse()
        {
            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
            {
                if (string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.UseValueTextBox.Text = string.Empty;
                }
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
            {
                if (string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.UseValueTextBox.Text = string.Empty;
                }
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
            {
                if (string.IsNullOrEmpty(this.UseAdjustmentTextBox.Text) && string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.UseAdjUnitTextBox.Text = string.Empty;
                    //this.UseValueTextBox.Text = string.Empty;
                }
            }

            if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
            {
                if (string.IsNullOrEmpty(this.UseAdjustmentTextBox.Text) && string.IsNullOrEmpty(this.AcresTextBox.Text))
                {
                    //this.UseAdjUnitTextBox.Text = string.Empty;
                    //this.UseValueTextBox.Text = string.Empty;
                }
            }
        }

        #endregion Methods

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the UseAdjusmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal units, adjustment;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                #region None
                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                {
                    this.UseAdjustmentTextBox.Text = string.Empty;
                    this.ReasonForUseAdjTextBox.Text = string.Empty;
                    //this.UseAdjUnitTextBox.Text = string.Empty;

                    if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                    {
                        this.GetLandCode();
                    }

                    this.SetForUseNone();
                    this.SetForNoneLandCodeValues();
                    this.CalculationForNone();
                }
                #endregion None

                #region LandCode
                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                {
                    this.UseAdjustmentTextBox.Text = string.Empty;
                    this.ReasonForUseAdjTextBox.Text = string.Empty;
                    //this.UseAdjUnitTextBox.Text = string.Empty;

                    ////this.SetForUseFactor();
                    this.SetForUseLandCode();
                    if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                    {
                        this.UseAdjusmentComboBox.SelectedValue = -1;
                        this.ReasonForUseAdjTextBox.Text = string.Empty;
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                    }
                    else
                    {
                        if (this.UseAdjusmentComboBox.SelectedValue != null)
                        {
                            this.landCode = this.UseAdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                        }
                    }

                    if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                    {
                        //this.UseAdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.UseBaseValueColumn].ToString();
                    }

                    this.CalculationForUseLandCode1();
                }
                #endregion LandCode

                #region Factor
                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                {
                    this.UseAdjustmentTextBox.Text = string.Empty;
                    this.ReasonForUseAdjTextBox.Text = string.Empty;
                    //this.UseAdjUnitTextBox.Text = string.Empty;                   

                    //decimal multiplier;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //////if (multiplier.Equals(0))
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                    //decimal.TryParse(this.UseUnitTextBox.Text, out units);
                    decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustment);
                    this.SetForUseFactor();
                    //this.UseAdjUnitTextBox.Text = Convert.ToString(units * adjustment);
                    this.CalculationForUseFactor2();
                }
                #endregion Factor

                #region Value
                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                {
                    this.UseAdjustmentTextBox.Text = string.Empty;
                    this.ReasonForUseAdjTextBox.Text = string.Empty;
                    //this.UseAdjUnitTextBox.Text = string.Empty;

                    this.SetForUseFactor();
                    decimal adjstmentvalue;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjstmentvalue);
                    //this.UseAdjUnitTextBox.Text = Convert.ToString(multiplier * adjstmentvalue);
                    this.UseAdjusmentComboBox.Visible = false;
                    this.UseAdjustmentTextBox.Visible = true;

                    this.CalculationForUseValue3();
                    ////this.UseAdjustmentTextBox.BringToFront(); 
                    ////this.UseAdjustmentpanel.Enabled = true;
                    ////this.UseAdjustmentTextBox.Enabled = true;
                }

                #endregion Value

                this.ClearTextBoxValuesForUse();
                this.ClearValueTextBoxForUse();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the UseAdjusmentTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentTypeComboBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                ////khaja added this IF Condition to fix Bug#4307
                if (this.useadjTypeValueChanged)
                {
                    decimal units, adjustment;
                    if (!this.flagLoadOnProcess && this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                    {
                        this.EditEnabled();
                    }

                    #region None
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        if (!string.IsNullOrEmpty(this.LandCodeTextBox.Text))
                        {
                            this.GetLandCode();
                        }

                        this.SetForUseNone();
                        this.SetForNoneLandCodeValues();
                        this.CalculationForNone();
                    }
                    #endregion None

                    #region LandCode
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        ////this.SetForUseFactor();
                        this.SetForUseLandCode();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            this.UseAdjusmentComboBox.SelectedValue = -1;
                            this.ReasonForUseAdjTextBox.Text = string.Empty;
                            //this.UseAdjUnitTextBox.Text = string.Empty;
                        }
                        else
                        {
                            if (this.UseAdjusmentComboBox.SelectedValue != null)
                            {
                                this.landCode = this.UseAdjusmentComboBox.SelectedValue.ToString();
                                this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                            }
                        }

                        if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                        {
                            //this.UseAdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.UseBaseValueColumn].ToString();
                        }

                        this.CalculationForUseLandCode1();
                    }
                    #endregion LandCode

                    #region Factor
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //decimal multiplier;
                        //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        //{
                        //    multiplier = 1;
                        //}

                        //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);

                        //decimal.TryParse(this.UseUnitTextBox.Text, out units);
                        decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustment);
                        this.SetForUseFactor();
                        if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                        {
                            //this.UseAdjUnitTextBox.Text = Convert.ToString(units * adjustment);
                        }

                        this.CalculationForUseFactor2();
                    }
                    #endregion Factor

                    #region Value
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        this.SetForUseFactor();
                        //this.UseAdjUnitTextBox.Text = this.UseAdjustmentTextBox.Text;
                        this.UseAdjusmentComboBox.Visible = false;
                        this.UseAdjustmentTextBox.Visible = true;
                        this.CalculationForUseValue3();
                    }
                    #endregion Value

                    this.ClearValueTextBox();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the UseAdjusmentComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjusmentComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                decimal acres, landCodeValue;
                if (!this.flagLoadOnProcess)
                {
                    this.EditEnabled();
                }

                this.landCode = this.UseAdjusmentComboBox.SelectedValue.ToString();
                this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID,null);
                if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                {
                    //this.UseAdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.UseBaseValueColumn].ToString();
                }
                else
                {
                    //this.UseAdjUnitTextBox.Text = string.Empty;
                }

                //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                //{
                //    multiplier = 1;
                //}

                //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                //decimal.TryParse(this.UseAdjUnitTextBox.Text, out landCodeValue);
                //this.UseAdjUnitTextBox.Text = Convert.ToString(multiplier * landCodeValue);
                decimal.TryParse(this.AcresTextBox.Text, out acres);
                //this.UseValueTextBox.Text = Convert.ToString((multiplier * landCodeValue) * acres);

                ////decimal.TryParse(this.AcresTextBox.Text, out acres);
                ////decimal.TryParse(this.UseAdjUnitTextBox.Text, out landCodeValue);
                ////this.ValueTextBox.Text = Convert.ToString(acres * landCodeValue);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the UseAdjustmentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UseAdjustmentTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.UseAdjusmentTypeComboBox.SelectedValue.ToString() == "0")
                {
                    this.CalculationForNone();
                    this.ClearValueTextBoxForUse();
                }

                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                {
                    if (this.UseAdjusmentTypeComboBox.SelectedValue != null)
                    {
                        this.landCode = this.UseAdjusmentTypeComboBox.SelectedValue.ToString();
                        this.CalculationForUseLandCode1();
                        this.ClearValueTextBoxForUse();
                    }
                }

                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                {
                    decimal acreUnits, baseValue, adjustedValue, useunit, tempcalc, tmpcalc;
                    //decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                    //if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                    //{
                    //    multiplier = 1;
                    //}

                    //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                    decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustedValue);
                    //decimal.TryParse(this.UseUnitTextBox.Text, out baseValue);
                    decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                    //this.UseAdjUnitTextBox.Text = Convert.ToString(baseValue * adjustedValue);
                    ////decimal.TryParse(this.AdjUnitTextBox.Text, out adjustmentUnitValue);
                    ////this.ValueTextBox.Text = Convert.ToString(adjustmentUnitValue * acreUnits);
                    //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
                    //// this.UseAdjUnitTextBox.Text = Convert.ToString(adjustedValue * useunit);
                    ////tempcalc = multiplier * useunit;
                    //tempcalc = useunit;
                    tmpcalc = adjustedValue * acreUnits;
                    //this.UseValueTextBox.Text = Convert.ToString(tempcalc * tmpcalc);
                    this.ClearValueTextBoxForUse();
                }

                if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                {
                    this.CalculationForUseValue3();
                    this.ClearValueTextBoxForUse();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Value was either too large or too small for a Decimal.")
                {
                    this.UseAdjustmentTextBox.Text = string.Empty;
                    MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MultiplierTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        //private void MultiplierTextBox_Leave(object sender, EventArgs e)
        /*{
            try
            {
                ////khaja added this IF Condition to fix Bug#4307
                if (this.multiplierValueChanged)
                {                    
                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;

                        decimal adjustmentValue, acreUnits, adjUnit, useunit, multiplier;
                        decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        {
                            multiplier = 1;
                        }

                        //this.UnitTextBox.Text = Convert.ToString(this.unitBaseValue * multiplier);
                        //decimal.TryParse(this.UnitTextBox.Text, out useunit);
                        decimal.TryParse(this.AdjustmentTextBox.Text, out adjustmentValue);
                        decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                        ////this.AdjUnitTextBox.Text = Convert.ToString(multiplier * useunit * adjustmentValue);
                        //this.AdjUnitTextBox.Text = Convert.ToString(useunit * adjustmentValue);
                        //decimal.TryParse(this.AdjUnitTextBox.Text, out adjUnit);
                        //this.ValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
                        ////tempcalc = useunit * adjustmentValue;
                        ////this.UseValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;
                        this.CalculationForValue3();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        //this.AdjUnitTextBox.Text = string.Empty;
                        //this.ValueTextBox.Text = string.Empty;
                        this.CalculationForNone();
                    }

                    if (this.AdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.AdjusmentComboBox.SelectedValue != null)
                        {
                            //this.AdjUnitTextBox.Text = string.Empty;
                            //this.ValueTextBox.Text = string.Empty;
                            this.landCode = this.AdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID);
                            if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                            {
                                //this.AdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.BaseValueColumn].ToString();
                            }

                            this.CalculationForLandCode1();
                        }
                    }

                    this.ClearValueTextBox();

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Factor))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        decimal adjustmentValue, acreUnits, adjUnit, useunit, multiplier;
                        decimal.TryParse(this.MultiplierTextBox.Text, out multiplier);
                        if (string.IsNullOrEmpty(this.MultiplierTextBox.Text))
                        {
                            multiplier = 1;
                        }

                        //this.UseUnitTextBox.Text = Convert.ToString(this.useunitBaseValue * multiplier);
                        //decimal.TryParse(this.UseUnitTextBox.Text, out useunit);
                        decimal.TryParse(this.UseAdjustmentTextBox.Text, out adjustmentValue);
                        decimal.TryParse(this.AcresTextBox.Text, out acreUnits);
                        ////this.UseAdjUnitTextBox.Text = Convert.ToString(multiplier * useunit * adjustmentValue);
                        //this.UseAdjUnitTextBox.Text = Convert.ToString(useunit * adjustmentValue);
                        //decimal.TryParse(this.UseAdjUnitTextBox.Text, out adjUnit);
                        //this.UseValueTextBox.Text = Convert.ToString(adjUnit * acreUnits);
                        ////tempcalc = useunit * adjustmentValue;
                        ////this.UseValueTextBox.Text = Convert.ToString(tempcalc * acreUnits);
                    }

                    ////this.UseAdjUnitTextBox.Text = string.Empty;
                    ////this.UseValueTextBox.Text = string.Empty;    
                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.Value))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        this.CalculationForUseValue3();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.None))
                    {
                        //this.UseAdjUnitTextBox.Text = string.Empty;
                        //this.UseValueTextBox.Text = string.Empty;
                        this.CalculationForNone();
                    }

                    if (this.UseAdjusmentTypeComboBox.SelectedValue.Equals((int)ComboBoxItems.LandCode))
                    {
                        if (this.UseAdjusmentComboBox.SelectedValue != null)
                        {
                            //this.UseAdjUnitTextBox.Text = string.Empty;
                            //this.UseValueTextBox.Text = string.Empty;
                            this.landCode = this.UseAdjusmentComboBox.SelectedValue.ToString();
                            this.form36035LandData = this.form36099Control.WorkItem.F36035_GetLandCodeBaseValue(this.landCode, this.valueSliceID);
                            if (this.form36035LandData.Get_LandCodeBaseValue.Rows.Count > 0)
                            {
                                //this.UseAdjUnitTextBox.Text = this.form36035LandData.Get_LandCodeBaseValue.Rows[0][this.form36035LandData.Get_LandCodeBaseValue.UseBaseValueColumn].ToString();
                            }

                            this.CalculationForUseLandCode1();
                        }
                    }

                    this.ClearValueTextBoxForUse();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Value was either too large or too small for a Decimal.")
                {
                    this.MultiplierTextBox.Text = string.Empty;
                    MessageBox.Show(SharedFunctions.GetResourceString("F36033_ValidationMessage3"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                ////  ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }*/

        /// <summary>
        /// Handles the MouseHover event of the TotalUseValueLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TotalUseValueLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.UseTotalValueToolTip.SetToolTip(this.TotalUseValueLabel, this.TotalUseValueLabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void F36099_Resize(object sender, EventArgs e)
        {
            this.Height = this.panel2.Height;
        }
    }
}
