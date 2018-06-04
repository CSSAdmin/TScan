//--------------------------------------------------------------------------------------------
// <copyright file="F36041.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 FS Land Codes.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02/11/2007       Malliga             Created
// 20/4/2009        Malliga             For bug Id 5670    
// 22/4/2009        Malliga             For bug Id 4333     
//***********************************************************************************************/

namespace D36040
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
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infrastructure.Interface;

    /// <summary>
    /// F36041 Class File
    /// </summary>
    public partial class F36041 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// F36041CropData
        /// </summary>
        private F36041CropData cropData = new F36041CropData();

        /// <summary>
        /// Used to Store Crop Data Table
        /// </summary>
        private F36041CropData.GetCropDetailsDataTable getCropdetailsDataTable = new F36041CropData.GetCropDetailsDataTable();

        /// <summary>
        /// Used to Store CropCode Data Table
        /// </summary>
        private F36041CropData.GetCropCodeDetailsDataTable getCropCodedetailsDataTable = new F36041CropData.GetCropCodeDetailsDataTable();

        /// <summary>
        /// currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// keyField
        /// </summary>
        private string keyField;

        /// <summary>
        /// formNo
        /// </summary>
        private int formNo;

        /// <summary>
        /// To Store CropID
        /// </summary>
        private int cropIdValue;


        # region Added for TSCO - D36040.F36041 Crop Form - New "Remove' button
        /// <summary>
        /// variable holds the selectedCropsIds.
        /// </summary>
        private List<int> selectedCropsIds = new List<int>();

        /// <summary>
        /// variable holds the selected Crops ids xml string.
        /// </summary>
        private string selectedCropsIdsXml = string.Empty;

        /// <summary>
        /// variable holds the selected Crops ids xml string.
        /// </summary>
        private string selectedCropsValueIdXml = string.Empty;
        
        /// <summary>
        /// cropgridRowCount
        /// </summary>
        private int cropgridRowCount;
        #endregion

        /// <summary>
        /// Used to store the currentColumnIndex
        /// </summary>
        private int currentColumnIndex;

        /// <summary>
        /// Used to store the RowBackColor
        /// </summary>
        private Color rowBackColor;

        /// <summary>
        /// controller F36041
        /// </summary>
        private F36041Controller form36041Control;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// An object for ContextMenuStrip
        /// </summary>
        // private ContextMenuStrip selectedComponentMenuStrip = new ContextMenuStrip(); //Commented for TSCO - D36040.F36041 Crop Form - New "Remove' button

        #region Form Slice Variables

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        #endregion Form Slice Variables

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int valueSliceId;

        /// <summary>
        /// allowDelete
        /// </summary>
        private bool allowDelete;

        /// <summary>
        /// deleteValidation
        /// </summary>
        private bool deleteValidation;

        /// <summary>
        /// selectionchangeflag
        /// </summary>
        private bool selectionchangeflag = false;

        /// <summary>
        /// Used to store Temp Crop Id
        /// </summary>
        private string tempCropId;

        /// <summary>
        /// redColorCode
        /// </summary>
        private int redColorCode;

        /// <summary>
        /// greenColorCode
        /// </summary>
        private int greenColorCode;

        /// <summary>
        /// blueColorCode
        /// </summary>
        private int blueColorCode;

        /// <summary>
        /// To hold tabText
        /// </summary>
        private string tabText;

        /// <summary>
        /// To hold parcel roll year
        /// </summary>
        private int rollYear;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F36041"/> class.
        /// </summary>
        public F36041()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F84721"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F36041(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.valueSliceId = keyID;
            this.tabText = tabText;
            this.redColorCode = red;
            this.greenColorCode = green;
            this.blueColorCode = blue;
            this.formMasterPermissionEdit = permissionEdit;
            this.CropGridpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CropGridpictureBox.Height, this.CropGridpictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declared the event SubFormSave of D35000_F35002
        /// </summary>
        [EventPublication(EventTopicNames.D35000_F35002_SubFormSave, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<F35002SubFormSaveEventArgs>> D35000_F35002_SubFormSave;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form36032 control.
        /// </summary>
        /// <value>The form36032 control.</value>
        [CreateNew]
        public F36041Controller Form36041Control
        {
            get { return this.form36041Control as F36041Controller; }
            set { this.form36041Control = value; }
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
                this.CustomizeCropCodeGridView();
                this.LoadCropCodeDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.New;
            }
            else
            {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearControl();
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
            this.ClearControl();
            this.CustomizeCropCodeGridView();
            this.LoadCropCodeDetails();
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
            if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
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
            this.SaveCropDetails();
            decimal resultAmount;
            Decimal.TryParse(this.ValueTotalOverrideTextBox.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

            F35002SubFormSaveEventArgs subFormSaveEventArgs;
            subFormSaveEventArgs.type = 5;
            subFormSaveEventArgs.value = resultAmount;
            subFormSaveEventArgs.valueSliceId = this.valueSliceId;

            subFormSaveEventArgs.amount = resultAmount;
            this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

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
            #region Added For TSCO - D36040.F36041 Crop Form - New "Remove' button
            this.selectedCropsValueIdXml = string.Empty;
            DataTable tempXMLValuedataTable = new DataTable();
            foreach (DataColumn column in this.getCropdetailsDataTable.Columns)
            {
                if (column.ColumnName == this.getCropdetailsDataTable.CropIDColumn.ColumnName)
                {
                    tempXMLValuedataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }
            DataRow tempXMLDataRow = tempXMLValuedataTable.NewRow();
            tempXMLDataRow[this.getCropdetailsDataTable.CropIDColumn.ColumnName] = this.valueSliceId.ToString();
            tempXMLValuedataTable.Rows.Add(tempXMLDataRow);
            #endregion
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                selectedCropsValueIdXml = TerraScanCommon.GetXmlString(tempXMLValuedataTable);//Added for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.form36041Control.WorkItem.F36041_DeleteCropIds(selectedCropsValueIdXml, TerraScanCommon.UserId);//Modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                SliceFormCloseAlert sliceFormCloseAlert;
                sliceFormCloseAlert.FormNo = this.masterFormNo;
                sliceFormCloseAlert.FlagFormClose = false;
                this.OnFormSlice_FormCloseAlert(new DataEventArgs<SliceFormCloseAlert>(sliceFormCloseAlert));
                this.Cursor = Cursors.Default;
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
                this.CustomizeCropCodeGridView();
                this.LoadCropCodeDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        #endregion Event Subscription

        #region Methods


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
        /// Customizing CropGrid 
        /// </summary>
        private void CustomizeCropCodeGridView()
        {
            //Added for TSCO - D36040.F36041 Crop Form - New "Remove' button
            this.RemoveButton.Enabled = false;
            this.SelectAllCheckBox.Checked = false;
            selectedCropsIds = new List<int>();
            //end
            this.cropData = this.form36041Control.WorkItem.F36041_ListCropCodeDetails(this.valueSliceId);

            this.getCropCodedetailsDataTable = this.cropData.GetCropCodeDetails;
            this.CropGridView.AutoGenerateColumns = false;
            //this.CropGridView.Columns[SharedFunctions.GetResourceString("ValidStatus")].DataPropertyName = SharedFunctions.GetResourceString("ValidStatus");
            this.CropID.DataPropertyName = this.getCropdetailsDataTable.CropIDColumn.ColumnName;
            this.CropValueSliceID.DataPropertyName = this.getCropdetailsDataTable.ValueSliceIDColumn.ColumnName;
            this.CropCode.DataPropertyName = this.getCropdetailsDataTable.CropCodeColumn.ColumnName; ////Crop Code
            this.Description.DataPropertyName = this.getCropdetailsDataTable.DescriptionColumn.ColumnName;
            this.Fruit.DataPropertyName = this.getCropdetailsDataTable.IsFruitTreeColumn.ColumnName;
            this.Planted.DataPropertyName = this.getCropdetailsDataTable.PlantedColumn.ColumnName;
            this.Age.DataPropertyName = this.getCropdetailsDataTable.AgeColumn.ColumnName;
            this.Adjust.DataPropertyName = this.getCropdetailsDataTable.AdjustColumn.ColumnName;
            this.Acre.DataPropertyName = this.getCropdetailsDataTable.ValuePerColumn.ColumnName;
            this.Acres.DataPropertyName = this.getCropdetailsDataTable.AcresColumn.ColumnName;
            this.Value.DataPropertyName = this.getCropdetailsDataTable.ValueColumn.ColumnName;
            this.IsCropConfigured.DataPropertyName = this.getCropCodedetailsDataTable.IsCropConfiguredColumn.ColumnName;
            //Added for TSCO - D36040.F36041 Crop Form - New "Remove' button
            this.CropGridView.Columns[SharedFunctions.GetResourceString("ValidStatus")].ReadOnly = true;

            this.ValidStatus.DisplayIndex = 0;
            this.CropID.DisplayIndex = 1;
            this.CropValueSliceID.DisplayIndex = 2;
            this.CropCode.DisplayIndex = 3;
            this.Description.DisplayIndex = 4;
            this.Fruit.DisplayIndex = 5;
            this.Planted.DisplayIndex = 6;
            this.Age.DisplayIndex = 7;
            this.Adjust.DisplayIndex = 8;
            this.Acre.DisplayIndex = 9;
            this.Acres.DisplayIndex = 10;
            this.Value.DisplayIndex = 11;

        }

        /// <summary>
        /// Loading CropGrid 
        /// </summary>
        private void LoadCropCodeDetails()
        {

            this.cropData = this.form36041Control.WorkItem.F36041_ListCropDetails(this.valueSliceId);
            this.getCropdetailsDataTable = this.cropData.GetCropDetails;
            cropgridRowCount = this.cropData.GetCropDetails.Rows.Count;
            ////to custmize combo box in grid
            (this.CropCode as DataGridViewComboBoxColumn).DataSource = this.getCropCodedetailsDataTable;
            (this.CropCode as DataGridViewComboBoxColumn).DisplayMember = this.getCropdetailsDataTable.CropCodeColumn.ColumnName;
            (this.CropCode as DataGridViewComboBoxColumn).ValueMember = this.getCropdetailsDataTable.CropCodeColumn.ColumnName;



            // Get parcel roll year for Age calculation
            if (this.cropData.RollYear.Rows.Count > 0)
            {
                F36041CropData.RollYearRow rollYearDetail = (F36041CropData.RollYearRow)this.cropData.RollYear.Rows[0];
                if (!rollYearDetail.IsRollYearNull())
                {
                    rollYear = rollYearDetail.RollYear;
                }
                else
                {
                    rollYear = 0;
                }
            }

            int emptyRows;
            if (cropgridRowCount > 0)
            {
                ////if the no of visiable rows(grid) is greater than the actual rows from the Datatable ---
                //// --- then a temp datatable with empty rows are merged with getCropdetailsDataTable datatable                    
                if (this.CropGridView.NumRowsVisible > cropgridRowCount)
                {
                    emptyRows = this.CropGridView.NumRowsVisible - cropgridRowCount;


                    for (int i = 0; i < emptyRows; i++)
                    {
                        this.getCropdetailsDataTable.AddGetCropDetailsRow(this.getCropdetailsDataTable.NewGetCropDetailsRow());
                    }
                }
                else
                {
                    this.getCropdetailsDataTable.AddGetCropDetailsRow(this.getCropdetailsDataTable.NewGetCropDetailsRow());
                }

                this.CropGridView.DataSource = this.getCropdetailsDataTable.DefaultView;

                this.CropGridView.Rows[0].Selected = true;

                this.CropGridviewPanel.Enabled = this.PermissionFiled.editPermission;
                // For Enabled top most check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Enabled = true;

               

            }
            else
            {
                for (int i = 0; i < this.CropGridView.NumRowsVisible; i++)
                {
                    this.getCropdetailsDataTable.AddGetCropDetailsRow(this.getCropdetailsDataTable.NewGetCropDetailsRow());
                }

                this.CropGridView.DataSource = this.getCropdetailsDataTable.DefaultView;
                this.CropGridView.Rows[0].Selected = false;
                // For Disabled top most check box  modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Enabled = false;
            }

            this.CropGridView.Enabled = true;
            this.CropGridView.AutoGenerateColumns = false;

            /////to enable or disable the vertical scroll bar
            if (this.getCropdetailsDataTable.Rows.Count > 12)
            {
                this.CropGridVerticalScroll.Visible = false;
            }
            else
            {
                this.CropGridVerticalScroll.Visible = true;
            }

            this.SetSmartPartHeight();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        /// <summary>
        /// Clearing Controls
        /// </summary>
        private void ClearControl()
        {
            this.CropGridView.DataSource = null;
            this.TotalAcresLabel.Text = string.Empty;
            this.TotalValueLabel.Text = string.Empty;
            this.AcresTotalOverrideTextBox.Text = string.Empty;
            this.ValueTotalOverrideTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Checking Errors
        /// </summary>
        /// <param name="formNo">Form Number.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            if (this.CropGridView.RowCount > 1)
            {
                string filterCondtions = "(((CropCode IS NULL or CropCode = '') OR (IsFruitTree IS NULL or IsFruitTree = '') OR (Planted IS NULL or Planted = '') OR (Age IS NULL or Age < 0) OR (ValuePer IS NULL or ValuePer < 0) OR (Acres IS NULL Or Acres = '' OR (Value IS NULL or Value < 0) )))"; //OR (Value IS NULL or Value < 0)))"; // (Value IS NULL or Value < 0)(Acres IS NULL Or Acres = '' Or Acres <= 0.0 
                DataRow[] drfilterCondtions = this.getCropdetailsDataTable.Select(filterCondtions);

                if (this.getCropdetailsDataTable.Rows.Count > 4)
                {
                    if (drfilterCondtions.Length > 1)
                    {
                        sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = string.Empty;
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                }
                else
                {
                    bool saveflag = false;
                    double decValue;
                    for (int i = 0; i < CropGridView.RowCount - 1; i++)
                    {
                        double.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out decValue);

                        if (!string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.CropCode.Name].Value.ToString().Trim()))
                        {
                            if (string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.CropCode.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Fruit.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Planted.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Age.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Acre.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Acres.Name].Value.ToString().Trim()) || string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim()) || decValue < 0.0)
                            {
                                saveflag = false;
                                break;
                            }
                            else
                            {
                                saveflag = true;
                            }
                        }
                        else
                        {

                            if (string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.CropCode.Name].Value.ToString().Trim()))
                            {
                                if (!string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Fruit.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Planted.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Age.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Acre.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Acres.Name].Value.ToString().Trim()) || !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[this.Acres.Name].Value.ToString().Trim()))
                                {
                                    saveflag = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (saveflag == false)
                    {
                        sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("RequiredField"));
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                    else
                    {
                        sliceValidationFields.ErrorMessage = string.Empty;
                        sliceValidationFields.FormNo = formNo;
                        sliceValidationFields.RequiredFieldMissing = false;
                    }
                }
                ////Coding Added for the issue bug id : 4333 by malliga on 22/4/2009
                decimal tmptotvalue = 0;
                decimal totalvalue = 0;
                for (int i = 0; i < CropGridView.RowCount - 1; i++)
                {
                    decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out totalvalue);
                    tmptotvalue = tmptotvalue + totalvalue;
                }

                double maxmoney = 922337203685477.5807;
                if (Convert.ToDouble(tmptotvalue) > maxmoney)
                {
                    sliceValidationFields.ErrorMessage = (SharedFunctions.GetResourceString("Entered values exceeds max limit."));
                    sliceValidationFields.FormNo = formNo;
                    sliceValidationFields.RequiredFieldMissing = false;
                }
                ////Ends here
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Saving CropCode Details
        /// </summary>
        /// 
        private void SaveCropDetails()
        {
            this.Cursor = Cursors.WaitCursor;
            string cropItems = string.Empty;
            cropItems = TerraScanCommon.GetXmlString(this.getCropdetailsDataTable);
            int returnValue = this.form36041Control.WorkItem.F36041_SaveCropCodeDetails(this.valueSliceId, cropItems, TerraScanCommon.UserId);

            if (returnValue < 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Cursor = Cursors.Default;
            }

            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Changing CropGrid RowBackColor
        /// </summary>
        /// <param name="row">Row.</param>
        private void ChangeRowBackColor(int row)
        {
            this.rowBackColor = this.CropGridView.Rows[row].DefaultCellStyle.BackColor;
        }

        /// <summary>
        /// Calculating Value Column
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void CalculateValueColumn(DataGridViewCellEventArgs e)
        {
            ////Value Calculation
            decimal adjust;
            decimal acre;
            decimal acres;
            ////Modified by Biju on 08/Jan/2010 to fix #4246. Changed "this.Value.Name" to "this.Acre.Name"
            if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim()) && (e.ColumnIndex.Equals(this.CropGridView.Columns[this.Acres.Name].Index) || e.ColumnIndex.Equals(this.CropGridView.Columns[this.Planted.Name].Index) || e.ColumnIndex.Equals(this.CropGridView.Columns[this.Adjust.Name ].Index)))
            {
                if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim()))
                {
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim(), out adjust);
                    if (adjust <= 0)
                    {
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value = 0;
                        adjust = 0;
                    }

                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim(), out acre);
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim(), out acres);
                    if (acres <= 0)
                    {
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value = 0;
                        acres = 0;
                    }

                    this.CropGridView.Rows[e.RowIndex].Cells[this.Value.Name].Value = adjust * acre * acres;
                    
                }
                else if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim()))
                {
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim(), out acre);
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim(), out acres);
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Value.Name].Value = acre * acres;
                    
                }
                else
                {
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Value.Name].Value = 0.0;
                }
            }
        }

        /// <summary>
        /// Calculating the Acre Column.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void CalculateAcreColumn(DataGridViewCellEventArgs e)
        {
            ////Coding added for the issue 5670 on 7/4/2009 by malliga
            if ((e.ColumnIndex == this.CropGridView.Columns[this.Acres.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Planted.Name].Index) && (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim())))
            {
                decimal currentAcresValue;
                decimal currentAcrePerValue;

                F36041CropData.CheckBreakValuesDataTable tempFilterBreakValue = new F36041CropData.CheckBreakValuesDataTable();
                F36041CropData.CheckBreakValuesDataTable filterBreakValue = new F36041CropData.CheckBreakValuesDataTable();

                string fillterCondtion = "CropCode = '" + this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim() + "'";

                DataRow[] tempCurrentRowValues = this.getCropCodedetailsDataTable.Select(fillterCondtion);

                if (tempCurrentRowValues.Length > 0)
                {
                    foreach (DataRow currentRow in tempCurrentRowValues)
                    {
                        tempFilterBreakValue.ImportRow(currentRow);
                    }

                    filterBreakValue.Merge(tempFilterBreakValue);

                    ////Coding changed for the issue 5670 on 7/4/2009 by malliga
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Age.Name].Value.ToString().Trim(), out currentAcresValue);

                    if (filterBreakValue.Rows.Count > 0 && currentAcresValue > 0)
                    {
                        if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break1"].ToString()))
                        {
                            decimal break1Value;
                            decimal.TryParse(filterBreakValue.Rows[0]["Break1"].ToString().Trim(), out break1Value);

                            if (currentAcresValue < break1Value)
                            {
                                decimal.TryParse(filterBreakValue.Rows[0]["BaseValue"].ToString().Trim(), out currentAcrePerValue);
                                // Coding Added for the issue 1154
                                currentAcrePerValue = Decimal.Round(currentAcrePerValue,MidpointRounding.AwayFromZero);   
                                // Ends here
                                this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                            }
                            else if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break2"].ToString()))
                            {
                                decimal break2Value;
                                decimal.TryParse(filterBreakValue.Rows[0]["Break2"].ToString().Trim(), out break2Value);

                                if (currentAcresValue >= break1Value && currentAcresValue < break2Value)
                                {
                                    decimal.TryParse(filterBreakValue.Rows[0]["Value1"].ToString().Trim(), out currentAcrePerValue);
                                    currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);    
                                    this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                }
                                else if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break3"].ToString()))
                                {
                                    decimal break3Value;
                                    decimal.TryParse(filterBreakValue.Rows[0]["Break2"].ToString().Trim(), out break2Value);
                                    decimal.TryParse(filterBreakValue.Rows[0]["Break3"].ToString().Trim(), out break3Value);

                                    if (currentAcresValue >= break2Value && currentAcresValue < break3Value)
                                    {
                                        decimal.TryParse(filterBreakValue.Rows[0]["Value2"].ToString().Trim(), out currentAcrePerValue);
                                        currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                                        this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                    }
                                    else if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break4"].ToString()))
                                    {
                                        decimal break4Value;
                                        decimal.TryParse(filterBreakValue.Rows[0]["Break3"].ToString().Trim(), out break3Value);
                                        decimal.TryParse(filterBreakValue.Rows[0]["Break4"].ToString().Trim(), out break4Value);

                                        if (currentAcresValue >= break3Value && currentAcresValue < break4Value)
                                        {
                                            decimal.TryParse(filterBreakValue.Rows[0]["Value3"].ToString().Trim(), out currentAcrePerValue);
                                            currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);    
                                            this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                        }
                                        else if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break5"].ToString()))
                                        {
                                            decimal break5Value;
                                            decimal.TryParse(filterBreakValue.Rows[0]["Break4"].ToString().Trim(), out break4Value);
                                            decimal.TryParse(filterBreakValue.Rows[0]["Break5"].ToString().Trim(), out break5Value);

                                            if (currentAcresValue >= break4Value && currentAcresValue < break5Value)
                                            {
                                                decimal.TryParse(filterBreakValue.Rows[0]["Value4"].ToString().Trim(), out currentAcrePerValue);
                                                currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);   
                                                this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                            }
                                            else if (!string.IsNullOrEmpty(filterBreakValue.Rows[0]["Break5"].ToString()))
                                            {
                                                decimal.TryParse(filterBreakValue.Rows[0]["Break5"].ToString().Trim(), out break5Value);

                                                if (currentAcresValue >= break5Value)
                                                {
                                                    decimal.TryParse(filterBreakValue.Rows[0]["Value5"].ToString().Trim(), out currentAcrePerValue);
                                                    currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                                                    this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                                }
                                            }
                                            else
                                            {
                                                decimal.TryParse(filterBreakValue.Rows[0]["Value4"].ToString().Trim(), out currentAcrePerValue);
                                                currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);   
                                                this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                                //// this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                                            }
                                        }
                                        else
                                        {
                                            decimal.TryParse(filterBreakValue.Rows[0]["Value4"].ToString().Trim(), out currentAcrePerValue);
                                            currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                                            this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                            ////this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                                        }
                                    }
                                    else
                                    {
                                        decimal.TryParse(filterBreakValue.Rows[0]["Value3"].ToString().Trim(), out currentAcrePerValue);
                                        currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                                        this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();

                                        //// this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                                    }
                                }
                                else
                                {
                                    decimal.TryParse(filterBreakValue.Rows[0]["Value2"].ToString().Trim(), out currentAcrePerValue);
                                    currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);    
                                    this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                    ////this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                                }
                            }
                            else
                            {
                                decimal.TryParse(filterBreakValue.Rows[0]["Value1"].ToString().Trim(), out currentAcrePerValue);
                                currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                                this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                                ////this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                            }
                        }
                        else
                        {
                            decimal.TryParse(filterBreakValue.Rows[0]["BaseValue"].ToString().Trim(), out currentAcrePerValue);
                            currentAcrePerValue = Decimal.Round(currentAcrePerValue,MidpointRounding.AwayFromZero);  
                            this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                        }
                    }
                    else
                    {
                        decimal.TryParse(filterBreakValue.Rows[0]["BaseValue"].ToString().Trim(), out currentAcrePerValue);
                        currentAcrePerValue = Decimal.Round(currentAcrePerValue, MidpointRounding.AwayFromZero);  
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value = currentAcrePerValue.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Calculating the Age and MaxLimit check.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void AgeAndMaxLimitCheck(DataGridViewCellEventArgs e)
        {
            decimal currentAdjustVale;
            double currentAcres;

            if (e.ColumnIndex == this.CropGridView.Columns[this.Adjust.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Acres.Name].Index)
            {
                if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim()))
                {
                    int pos;
                    string adjustvalue;
                    int adjustlen;
                    adjustvalue = this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim();
                    adjustlen = adjustvalue.Length;
                    if (adjustlen > 7)
                    {
                        pos = adjustvalue.IndexOf(".");
                        if (pos == -1)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value = string.Empty;
                        }
                        else
                        {
                            decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim(), out currentAdjustVale);

                            this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value = currentAdjustVale;
                        }
                    }

                    decimal tmpadjustvalue;
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim(), out tmpadjustvalue);
                    if (tmpadjustvalue <= 0)
                    {
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value = 0;
                        tmpadjustvalue = 0;
                    }
                }

                if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim()))
                {
                    int pos;
                    string acresvalue;
                    int acreslen;
                    acresvalue = this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim();
                    acreslen = acresvalue.Length;
                    if (acreslen > 9)
                    {
                        pos = acresvalue.IndexOf(".");
                        if (pos == -1)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value = string.Empty;
                        }
                        else
                        {
                            double.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim(), out currentAcres);

                            this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value = currentAcres;
                        }
                    }

                    //if (string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim()))
                    //{
                    //    MessageBox.Show("Required Field Missing.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }

                decimal tmpacresvalue;
                if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim()))
                {
                    decimal.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString().Trim(), out tmpacresvalue);
                    if (tmpacresvalue <= 0)
                    {
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value = 0;
                        tmpacresvalue = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Validating the Planted Year.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void ValidatePlantedYear(DataGridViewCellEventArgs e)
        {
            int plantedYear;

            if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim()))
            {
                int.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim(), out plantedYear);
                // Current Year has been replaced by Parcel RollYear to fix issue #11807 - Latha
                if (plantedYear > rollYear) //if (plantedYear > System.DateTime.Now.Year)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("Wrong PlantedYear"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value = DBNull.Value;
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Age.Name].Value = DBNull.Value;
                }

                if (plantedYear < 1900)
                {
                    MessageBox.Show("Planted Year must be greater than 1899 and lesser than 2079.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value = DBNull.Value;
                    this.CropGridView.Rows[e.RowIndex].Cells[this.Age.Name].Value = DBNull.Value;
                }
            }
        }

        /// <summary>
        /// Populating the Fruit Column.
        /// </summary>
        /// <param name="e">DataGridViewCellEventArgs</param>
        private void PopulateFruitValue(DataGridViewCellEventArgs e)
        {
            ////Commented by Biju on 08/Jan/2010 to fix an already fixed issue of not updating fruit value when
            ////crop code is selected using arrow keys
            ////if (this.currentColumnIndex == this.CropGridView.Columns[this.CropCode.Name].Index && (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim())))
            ////Added by Biju on 08/Jan/2010 to fix an already fixed issue of not updating fruit value when
            ////crop code is selected using arrow keys
            if (e.ColumnIndex == this.CropGridView.Columns[this.CropCode.Name].Index && (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim())))
            {
                string fillterCondtion = "CropCode = '" + this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim() + "'";

                DataRow[] tempCurrentRowValues = this.getCropCodedetailsDataTable.Select(fillterCondtion);

                if (tempCurrentRowValues.Length > 0)
                {
                    if (this.selectionchangeflag)
                    {
                        this.CropGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = tempCurrentRowValues[0]["Description"].ToString();
                        this.CropGridView.Rows[this.currentRowIndex].Cells[this.Fruit.Name].Value = tempCurrentRowValues[0]["IsFruitTree"].ToString();
                    }
                }
            }
        }

        private void SetSmartPartHeight()
        {
            // For Gridview design modified based on check box column added TSCO - D36040.F36041 Crop Form - New "Remove' button 
            if (this.CropGridView.Rows.Count.Equals(this.CropGridView.NumRowsVisible))
            {
               
                // For 4 records display
                this.CropGridView.Height = 111;
                this.CropGridviewPanel.Height = 173;
                this.CropGridpictureBox.Height = this.CropGridviewPanel.Height;
            }
            else if (this.CropGridView.Rows.Count <= 12)
            {
                // For less than or equalto 12 records display
                this.CropGridView.Height = (this.CropGridView.Rows.Count * 22) + 23;
                this.CropGridviewPanel.Height = this.CropGridView.Height + this.Footerpanel.Height - 1 + this.panel3.Height;
            }
            else
            {
                // For above 12 records display
                this.CropGridView.Height = 287;
                this.CropGridView.Width = 766;
                this.CropGridviewPanel.Height = this.CropGridView.Height + this.Footerpanel.Height - 1 +this.panel3.Height;
                this.CropGridVerticalScroll.Visible = false;
            }

            this.CropGridVerticalScroll.Height = this.CropGridView.Height - 1;
            this.CropGridpictureBox.Height = this.CropGridviewPanel.Height;
            this.Height = this.CropGridpictureBox.Height;

            // Resize SmartPart
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = 350;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.CropGridpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CropGridpictureBox.Height, this.CropGridpictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);

            // Footer Panel Position
            this.Footerpanel.Top = this.CropGridView.Bottom - 1;
            this.panel2.Top = this.CropGridView.Bottom - 1;
        }

        /// <summary>
        /// Calculates the sum.
        /// </summary>
        private void CalculateSum()
        {
            if (this.CropGridView.OriginalRowCount > 0)
            {
                decimal tmptotacresvalue = 0;
                decimal totalacresCalcValue = 0;
                decimal tmptotvalue = 0;
                decimal totalvalue = 0;

                for (int i = 0; i < CropGridView.RowCount - 1; i++)
                {
                    decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Acres.Name].Value.ToString().Trim(), out totalacresCalcValue);
                    tmptotacresvalue = tmptotacresvalue + totalacresCalcValue;
                    decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out totalvalue);
                    tmptotvalue = tmptotvalue + totalvalue;
                }

                this.AcresTotalOverrideTextBox.Text = tmptotacresvalue.ToString();
                this.ValueTotalOverrideTextBox.Text = tmptotvalue.ToString();

                this.TotalAcresLabel.Text = this.AcresTotalOverrideTextBox.Text.Trim();
                this.TotalValueLabel.Text = this.ValueTotalOverrideTextBox.Text.Trim();
                ////double maxmoney = 922337203685477.5807;
                ////if (Convert.ToDouble(tmptotvalue) > maxmoney)
                ////{
                ////    MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////}
            }
            else
            {
                this.TotalAcresLabel.Text = string.Empty;
                this.TotalValueLabel.Text = string.Empty;
            }
        }

        #endregion Methods

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F36041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        /// 
        private void F36041_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.CustomizeCropCodeGridView();
                this.LoadCropCodeDetails();
                this.keyField = "CropID";
                this.formNo = 36041;
                // Assiging menu value for Contextmenustrip
                //this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuDelete"));
                //this.selectedComponentMenuStrip.Items.Add(SharedFunctions.GetResourceString("MenuCancel"));
                //this.selectedComponentMenuStrip.ItemClicked += new ToolStripItemClickedEventHandler(this.SelectedComponentMenuStrip_ItemClicked);
                //this.selectedComponentMenuStrip.Closed += new ToolStripDropDownClosedEventHandler(this.SelectedComponentMenuStrip_Closed);
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region PictureBox Events
        /// <summary>
        /// Handles the MouseHover event of the CropGridpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CropGridpictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.CropValueSliceToolTip.SetToolTip(this.CropGridpictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CropGridpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CropGridpictureBox_Click(object sender, EventArgs e)
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

        #endregion PictureBox Events

        #region Comment Menustrip for right click uncomment if necessary
        // For Comment menu strip  TSCO - D36040.F36041 Crop Form - New "Remove' button
        //#region Menu Strip Events
        ///// <summary>
        ///// Handles the Closed event of the SelectedComponentMenuStrip control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> instance containing the event data.</param>
        ///// 
        //private void SelectedComponentMenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        //{
        //    try
        //    {
        //        if (this.currentRowIndex > 0)
        //        {
        //            this.CropGridView.Rows[this.currentRowIndex].DefaultCellStyle.BackColor = this.rowBackColor;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //    }
        //}

        ///// <summary>
        ///// Selecteds the component menu strip_ item clicked.
        ///// </summary>
        ///// <param name="sender">The sender.</param>
        ///// <param name="e">The e.</param>
        //private void SelectedComponentMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //{
        //    if (this.currentRowIndex >= 0)
        //    {
        //        if (e.ClickedItem.Text == SharedFunctions.GetResourceString("Delete"))
        //        {
        //            //this.selectedComponentMenuStrip.Visible = false;
        //            if (!string.IsNullOrEmpty(this.CropGridView[this.CropID.Name, this.currentRowIndex].Value.ToString().Trim()) && this.pageMode == TerraScanCommon.PageModeTypes.View)
        //            {
        //                this.form36041Control.WorkItem.F36041_DeleteCrop(this.cropIdValue, TerraScanCommon.UserId);

        //                this.CustomizeCropCodeGridView();
        //                this.allowDelete = true;
        //                this.deleteValidation = true;
        //                this.LoadCropCodeDetails();
        //                if (this.CropGridView.OriginalRowCount > 0)
        //                {
        //                    decimal totalvalue = 0;
        //                    decimal tmptotvalue = 0;
        //                    for (int i = 0; i < CropGridView.RowCount - 1; i++)
        //                    {
        //                        decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out totalvalue);
        //                        tmptotvalue = tmptotvalue + totalvalue;
        //                    }

        //                    this.ValueTotalOverrideTextBox.Text = tmptotvalue.ToString();
        //                }
        //                ////Added On 29/2/2008
        //                decimal resultAmount;
        //                Decimal.TryParse(this.ValueTotalOverrideTextBox.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

        //                F35002SubFormSaveEventArgs subFormSaveEventArgs;
        //                subFormSaveEventArgs.type = 5;
        //                subFormSaveEventArgs.value = resultAmount;
        //                subFormSaveEventArgs.valueSliceId = this.valueSliceId;

        //                subFormSaveEventArgs.amount = resultAmount;
        //                this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
        //                //// this.allowDelete = false;
        //                this.pageMode = TerraScanCommon.PageModeTypes.View;
        //            }
        //        }
        //    }
        //}

        //#endregion
        #endregion

        #region Grid Events

        /// <summary>
        /// Handles the CellClick event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CropGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////Code Added for avoiding Emptyrow
                if (e.ColumnIndex == -1)
                {
                    this.deleteValidation = true;
                }
                else
                {
                    this.deleteValidation = false;
                }

                if (e.RowIndex >= 0)
                {
                    if (!string.IsNullOrEmpty(this.CropGridView[this.CropID.Name, e.RowIndex].Value.ToString().Trim()))
                    {
                        int.TryParse(this.CropGridView[this.CropID.Name, e.RowIndex].Value.ToString().Trim(), out this.cropIdValue);
                    }
                  
                    if (e.RowIndex == 0)
                    {
                        this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView.Rows[e.RowIndex].Selected = false;
                    }

                    bool hasValues = false;
                    if (e.RowIndex >= 1)
                    {
                        if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                        {
                            if (e.RowIndex + 1 < CropGridView.RowCount)
                            {
                                for (int i = e.RowIndex; i < CropGridView.RowCount; i++)
                                {
                                    if (!string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                    {
                                        hasValues = true;
                                        break;
                                    }
                                }

                                if (hasValues)
                                {
                                    this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView.Rows[e.RowIndex].Selected = false;
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                                    {
                                        this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = true;
                                        this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                        this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = true;
                                        this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = true;
                                        this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = true;
                                    }
                                    else
                                    {
                                        this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                                        this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                        this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                                        this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                                        this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                                        this.CropGridView.Rows[e.RowIndex].Selected = false;
                                    }
                                }
                            }
                            else
                            {
                                this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = true;
                                this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = true;
                                this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = true;
                                this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = true;
                            }
                        }
                        else
                        {
                            this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                            this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                            this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                            this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                            this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                            this.CropGridView.Rows[e.RowIndex].Selected = false;
                        }
                    }

                    this.currentRowIndex = e.RowIndex;
                    this.currentColumnIndex = e.ColumnIndex;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// 
        private void CropGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.RowIndex + 1) == this.CropGridView.Rows.Count)
                {
                    if ((!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.CropCode.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Description.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Adjust.Name].Value.ToString().Trim())) || (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString().Trim())))
                    {
                        this.getCropdetailsDataTable.AddGetCropDetailsRow(this.getCropdetailsDataTable.NewGetCropDetailsRow());
                        //this.CropGridVerticalScroll.Visible = false;
                        this.SetSmartPartHeight();
                        ////Added by Biju on 08/Jan/2010 to fix #5000
                        if (this.CropGridView.CurrentCell == null)
                        {
                            this.CropGridView.CurrentCell = this.CropGridView.Rows[e.RowIndex].Cells[this.Description.Name];
                            this.CropGridView.Focus();
                        }
                        ////till here
                    }
                }

                //// Populating the Fruit Value Column
                this.PopulateFruitValue(e);

                ////Coding added for the issue 5670 on 7/4/2009 by malliga
                ////Age Caculation

                if (e.ColumnIndex == this.CropGridView.Columns[this.Planted.Name].Index)
                {
                    if (!string.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim()))
                    {
                        int calAge;
                        int.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.Planted.Name].Value.ToString().Trim(), out calAge);
                        
                        // Current Year has been replaced by Parcel RollYear to fix issue #11807 - Latha
                        //this.CropGridView.Rows[e.RowIndex].Cells[this.Age.Name].Value = System.DateTime.Now.Year - calAge;
                        this.CropGridView.Rows[e.RowIndex].Cells[this.Age.Name].Value = rollYear - calAge;
                    }
                    else
                    {
                        this.CropGridView.Rows[this.currentRowIndex].Cells[this.Age.Name].Value = DBNull.Value;
                    }
                }

                //// Calculating the Value Column
                this.CalculateAcreColumn(e);
    
                //// Validating the Planted Year Column
                this.ValidatePlantedYear(e);

                //// Checking the MaxLimit and Age Calculation
                this.AgeAndMaxLimitCheck(e);

                //// Calculating the Value Column.
                this.CalculateValueColumn(e);

                //// Calculate total value of Acres and Values
                this.CalculateSum();

                this.currentRowIndex = e.RowIndex;
                this.currentColumnIndex = e.ColumnIndex;
                this.selectionchangeflag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Cell Formatting event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// 
        private void CropGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                decimal outDecimal;

                if (e.ColumnIndex == this.CropGridView.Columns[this.Acre.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Value.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.CropGridView.Rows[e.RowIndex].Cells[this.Acre.Name].Value.ToString()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                        else
                        {
                            e.Value = "";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the CropGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:
        /// System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        /// 
        private void CropGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.CropGridView.Columns[this.CropID.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.CropValueSliceID.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Description.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Planted.Name].Index || e.ColumnIndex == this.CropGridView.Columns[this.Adjust.Name].Index)
                {
                    this.CropGridView.CurrentCell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DataBindingComplete event of the CropGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> instance containing the event data.</param>
        private void CropGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                this.CalculateSum();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the CropGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void CropGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                //this.selectionchangeflag = false;
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged -= new EventHandler(this.Control_TextChanged);
               
                ////Coding added for the issue 5670 on 7/4/2009 by malliga
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    if (string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString()))
                    {
                        this.tempCropId = string.Empty;
                    }
                    else
                    {
                        this.tempCropId = this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString();
                    }

                    if (this.CropGridView.CurrentColumnIndex.Equals(2) && !this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].ReadOnly
                        && string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString()))
                    {
                        //if (this.CropGridView.EditingControl != null)
                        //{
                        //    this.CropGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                        //}

                        ((ComboBox)this.CropGridView.EditingControl).SelectedIndex = -1;
                    }
                    ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.F36041_CropCodeSelectionChangeCommitted);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F36041_CropCodeSelectionChangeCommitted);
                }

                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.MouseClick += new MouseEventHandler(this.Control_MouseClick);
                e.Control.Validated += new EventHandler(this.Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CropCodeSelectionChangeCommitted event of the F36041 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F36041_CropCodeSelectionChangeCommitted(object sender, EventArgs e)
        {
            ////Coding added for the issue 5670 on 7/4/2009 by malliga
            try
            {
                ComboBox combo = (ComboBox)sender;
                this.tempCropId = combo.Text;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                ////called textchanged event to fix Bug#5187
                if (this.tempCropId != null && this.tempCropId != "")
                {
                    combo.SendToBack();
                    combo.SelectAll();
                    this.selectionchangeflag = true;
                    this.Control_TextChanged(null, null);
                    this.ToEnableEditButtonInMasterForm();
                    ((ComboBox)this.CropGridView.EditingControl).SelectionChangeCommitted -= new EventHandler(this.F36041_CropCodeSelectionChangeCommitted);
                    combo.BringToFront();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the MouseUp event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void CropGridView_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && this.slicePermissionField.deletePermission)
                {
                    if (this.currentRowIndex >= 0)
                    {
                        if (this.CropGridView.Rows[this.currentRowIndex].Selected)
                        {
                            if (!string.IsNullOrEmpty(this.CropGridView[this.CropID.Name, this.currentRowIndex].Value.ToString().Trim()) && this.pageMode == TerraScanCommon.PageModeTypes.View)
                            {
                                this.ChangeRowBackColor(this.currentRowIndex);
                                //Commented for TSCO - D36040.F36041 Crop Form - New "Remove' button
                               // this.selectedComponentMenuStrip.Show(this.CropGridView, new Point(e.X, e.Y));
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
        /// Handles the RowEnter event of the CropDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        /// 
        private void CropGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.CropGridView[this.CropID.Name, e.RowIndex].Value.ToString().Trim()))
                {
                    int.TryParse(this.CropGridView[this.CropID.Name, e.RowIndex].Value.ToString().Trim(), out this.cropIdValue);
                }

                if (e.RowIndex == 0)
                {
                    this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                    this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                    this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                    this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                    this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                    this.CropGridView.Rows[e.RowIndex].Selected = false;
                }

                bool hasValues = false;
                if (e.RowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (e.RowIndex + 1 < CropGridView.RowCount)
                        {
                            for (int i = e.RowIndex; i < CropGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                                this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                                this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                                this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                                this.CropGridView.Rows[e.RowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (e.RowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (e.RowIndex - 1)].Value.ToString().Trim())))
                                {
                                    this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = true;
                                    this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                                    this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = true;
                                    this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = true;
                                    this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                                    this.CropGridView.Rows[e.RowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = true;
                            this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = true;
                            this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = true;
                            this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = true;
                            this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.CropGridView[this.CropCode.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Description.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Planted.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Adjust.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView[this.Acres.Name, e.RowIndex].ReadOnly = false;
                        this.CropGridView.Rows[e.RowIndex].Selected = false;
                    }

                }
              

                this.currentRowIndex = e.RowIndex;
                this.currentColumnIndex = e.ColumnIndex;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the CropGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void CropGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && this.CropGridView.CurrentCell != null)
                {
                    this.CropGridView.CurrentCell.ReadOnly = true;
                    this.CropGridView.Rows[e.RowIndex].Selected = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Control_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.currentRowIndex == 0)
                {
                    this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = false;
                    this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                    this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = false;
                    this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = false;
                    this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = false;
                    this.CropGridView.Rows[this.currentRowIndex].Selected = false;
                }

                bool hasValues = false;
                if (this.currentRowIndex >= 1)
                {
                    if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())))
                    {
                        if (this.currentRowIndex + 1 < CropGridView.RowCount)
                        {
                            for (int i = this.currentRowIndex; i < CropGridView.RowCount; i++)
                            {
                                if (!string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrEmpty(this.CropGridView.Rows[i].Cells[2].Value.ToString().Trim()))
                                {
                                    hasValues = true;
                                    break;
                                }
                            }

                            if (hasValues)
                            {
                                this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = false;
                                this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                                this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = false;
                                this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = false;
                                this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = false;
                                this.CropGridView.Rows[this.currentRowIndex].Selected = false;
                            }
                            else
                            {
                                if ((string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Description.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Planted.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Adjust.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())) && (string.IsNullOrEmpty(this.CropGridView[this.Acres.Name, (this.currentRowIndex - 1)].Value.ToString().Trim())))
                                {
                                    this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = true;
                                    this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = true;
                                    this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = true;
                                    this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = true;
                                    this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = true;
                                }
                                else
                                {
                                    this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = false;
                                    this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                                    this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = false;
                                    this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = false;
                                    this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = false;
                                    this.CropGridView.Rows[this.currentRowIndex].Selected = false;
                                }
                            }
                        }
                        else
                        {
                            this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = true;
                            this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = true;
                            this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = true;
                            this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = true;
                            this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        this.CropGridView[this.CropCode.Name, this.currentRowIndex].ReadOnly = false;
                        this.CropGridView[this.Description.Name, this.currentRowIndex].ReadOnly = false;
                        this.CropGridView[this.Planted.Name, this.currentRowIndex].ReadOnly = false;
                        this.CropGridView[this.Adjust.Name, this.currentRowIndex].ReadOnly = false;
                        this.CropGridView[this.Acres.Name, this.currentRowIndex].ReadOnly = false;
                        this.CropGridView.Rows[this.currentRowIndex].Selected = false;
                    }
                }
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
                if (this.CropGridView.EditingControl is DataGridViewComboBoxEditingControl)
                {
                    if (string.IsNullOrEmpty(this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString()))
                    {
                        this.tempCropId = string.Empty;
                    }
                    else
                    {
                        this.tempCropId = this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString();
                    }
                }

                this.CropGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if (this.CropGridView.EditingControl != null)
                {
                    this.CropGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
                    //if (this.CropGridView.EditingControl is DataGridViewComboBoxEditingControl && sender != null)
                    //{
                    //    return;
                    //}
                }

                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    ////if (!this.allowDelete)
                    ////{
                    ////    this.ToEnableEditButtonInMasterForm();
                    ////}

                    //if (!string.IsNullOrEmpty(this.CropGridView.Rows[this.currentRowIndex].Cells[this.CropCode.Name].Value.ToString()))
                    //{
                    //    this.tempCropId = this.CropGridView.Rows[this.currentRowIndex].Cells[this.CropCode.Name].Value.ToString();
                    //}
                    /////Code Added For Avoiding emptyrow(Sriparameswari)
                    if (!this.deleteValidation)// && this.tempCropId != null && this.tempCropId != "")
                    {
                        this.ToEnableEditButtonInMasterForm();
                        if (this.currentColumnIndex == this.CropGridView.Columns[this.CropCode.Name].Index)
                        {
                            //string fillterCondtion = "CropCode = '" + this.tempCropId + "'";

                            string fillterCondtion = "CropCode = '" + this.CropGridView[this.CropCode.Name, this.CropGridView.CurrentRowIndex].Value.ToString() + "'";

                            DataRow[] tempCurrentRowValues = this.getCropCodedetailsDataTable.Select(fillterCondtion);

                            if (tempCurrentRowValues.Length > 0)
                            {
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.CropCode.Name].Value = this.tempCropId;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = tempCurrentRowValues[0]["Description"].ToString();
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Fruit.Name].Value = tempCurrentRowValues[0]["IsFruitTree"].ToString();
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Planted.Name].Value = DBNull.Value;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Age.Name].Value = DBNull.Value;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Adjust.Name].Value = DBNull.Value;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Acres.Name].Value = DBNull.Value;
                                this.CropGridView.Rows[this.currentRowIndex].Cells[this.Value.Name].Value = DBNull.Value;
                            }
                        }
                    }
                }

                if (!this.allowDelete)
                {
                    if (this.CropGridView.CurrentColumnIndex == this.CropGridView.Columns[this.CropCode.Name].Index && (!string.IsNullOrEmpty(this.CropGridView.Rows[this.currentRowIndex].Cells[this.CropCode.Name].Value.ToString().Trim())))
                    {
                        string fillterCondtion = "CropCode = '" + this.CropGridView.Rows[this.currentRowIndex].Cells[this.CropCode.Name].Value.ToString().Trim() + "'";

                        DataRow[] tempCurrentRowValues = this.getCropCodedetailsDataTable.Select(fillterCondtion);

                        if (tempCurrentRowValues.Length > 0)
                        {
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Fruit.Name].Value = tempCurrentRowValues[0]["IsFruitTree"].ToString();
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Description.Name].Value = tempCurrentRowValues[0]["Description"].ToString();
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Planted.Name].Value = DBNull.Value;
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Age.Name].Value = DBNull.Value;
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Adjust.Name].Value = DBNull.Value;
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Acre.Name].Value = DBNull.Value;
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Acres.Name].Value = DBNull.Value;
                            this.CropGridView.Rows[this.currentRowIndex].Cells[this.Value.Name].Value = DBNull.Value;
                        }

                        if (this.CropGridView.OriginalRowCount > 0)
                        {
                            decimal tmptotacresvalue = 0;
                            decimal totalacresCalcValue = 0;
                            decimal tmptotvalue = 0;
                            decimal totalvalue = 0;

                            for (int i = 0; i < CropGridView.RowCount - 1; i++)
                            {
                                decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Acres.Name].Value.ToString().Trim(), out totalacresCalcValue);
                                tmptotacresvalue = tmptotacresvalue + totalacresCalcValue;
                                decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out totalvalue);
                                tmptotvalue = tmptotvalue + totalvalue;
                            }

                            this.AcresTotalOverrideTextBox.Text = tmptotacresvalue.ToString();
                            this.ValueTotalOverrideTextBox.Text = tmptotvalue.ToString();

                            this.TotalAcresLabel.Text = this.AcresTotalOverrideTextBox.Text.Trim();
                            this.TotalValueLabel.Text = this.ValueTotalOverrideTextBox.Text.Trim();
                            ////double maxmoney = 922337203685477.5807;
                            ////if (Convert.ToDouble(tmptotvalue) > maxmoney)
                            ////{
                            ////    MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ////}
                        }
                        else
                        {
                            this.TotalAcresLabel.Text = string.Empty;
                            this.TotalValueLabel.Text = string.Empty;
                        }
                    }
                }
            }
            catch (StackOverflowException est)
            {
                ExceptionManager.ManageException(est, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void CropGridView_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void CropGridView_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// To the enable edit buttons in master form.
        /// </summary>
        private void ToEnableEditButtonInMasterForm()
        {
            this.EditEnabled();
        }

        /// <summary>
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.RemoveButton.Enabled = false;
                //For Select and unselect topmost check box and grid check box modified for TSCO - D36040.F36041 Crop Form - New "Remove' button
                this.SelectAllCheckBox.Checked = false;
                this.SelectAllCheckBox.Enabled = false;
                this.selectedCropsIds = new List<int>();
                this.ReadOnlyAll("false");
                //end
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        #endregion Grid Events

        #region Total Acres and Total Value Lables
        /// <summary>
        /// Handles the Mouse Hover event of the TotalAcres Label.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TotalAcresLabel_MouseHover"/> instance containing the event data.</param>
        /// 
        private void TotalAcresLabel_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.AcresTotalToolTip.SetToolTip(this.TotalAcresLabel, this.TotalAcresLabel.Text.Trim());
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Mouse Hover event of the TotalAcres Label.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TotalAcresLabel_MouseHover"/> instance containing the event data.</param>
        /// 
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

        #endregion

        private void CropGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView grd = sender as DataGridView;
            if ((grd.Rows[e.RowIndex].Cells["IsCropConfigured"].Value.ToString() != null))
            {
                if (grd.Rows[e.RowIndex].Cells["IsCropConfigured"].Value.ToString().ToLower() == "true")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
                if (grd.Rows[e.RowIndex].Cells["IsCropConfigured"].Value.ToString().ToLower() == "false")
                {
                    grd.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Pink;
                }
            }
        }
        /// <summary>
        /// Calculates the selected Crops for TSCO - D36040.F36041 Crop Form - New "Remove' button .
        /// </summary>
        private void CalculateSelectAllCrops(bool isChecked)
        {
            try
            {
                this.CropGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.cropgridRowCount; count++)
                {
                    if (isChecked == true)
                    {
                        this.selectedCropsIds.Add(Convert.ToInt32(this.CropGridView[this.getCropdetailsDataTable.CropIDColumn.ColumnName, count].Value));
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /// <summary>
        /// Calculates the Unselected receipts Crops for TSCO - D36040.F36041 Crop Form - New "Remove' button.
        /// </summary>
        private void CalculateUnSelectCrops(bool isChecked)
        {
            try
            {
                this.CropGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                for (int count = 0; count < this.cropgridRowCount; count++)
                {
                    if (isChecked == false)
                    {
                        this.selectedCropsIds.Remove(Convert.ToInt32(this.CropGridView[this.getCropdetailsDataTable.CropIDColumn.ColumnName, count].Value));
                    }

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the selected Crops ids to XML  for 163 sprint.
        /// </summary>
        private void GetSelectedCropIdsXml()
        {
            this.selectedCropsIdsXml = string.Empty;
            DataTable tempXMLdataTable = new DataTable();
            foreach (DataColumn column in this.getCropdetailsDataTable.Columns)
            {
                if (column.ColumnName == this.getCropdetailsDataTable.CropIDColumn.ColumnName)
                {
                    tempXMLdataTable.Columns.Add(new DataColumn(column.ColumnName));
                }
            }

            for (int item = 0; item < this.selectedCropsIds.Count; item++)
            {
                DataRow tempXMLDataRow = tempXMLdataTable.NewRow();
                tempXMLDataRow[this.getCropdetailsDataTable.CropIDColumn.ColumnName] = this.selectedCropsIds[item].ToString();
                tempXMLdataTable.Rows.Add(tempXMLDataRow);
            }

            this.selectedCropsIdsXml = TerraScanCommon.GetXmlString(tempXMLdataTable);
        }

        /// <summary>
        /// Selects the un select all and Unselect all for 163 sprint.
        /// </summary>
        /// <param name="status">The status.</param>
        private void SelectUnSelectAll(string status)
        {
            if (this.cropgridRowCount > 0)
            {
                for (int count = 0; count < this.cropgridRowCount; count++)
                {
                    this.CropGridView.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }

        }

        /// <summary>
        /// Set the Readonly all check box column.
        /// </summary>
        /// <param name="status">The status.</param>
        private void ReadOnlyAll(string status)
        {
            if (this.cropgridRowCount > 0)
            {
                for (int count = 0; count < this.cropgridRowCount; count++)
                {
                    this.CropGridView.Rows[count].Cells[SharedFunctions.GetResourceString("ValidStatus")].Value = status;
                }
            }

        }


        /// <summary>
        /// Cell Content Click for the cropgrid view.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.TotalAcresLabel_MouseHover"/> instance containing the event data.</param>
        private void CropGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                // Add and remove the selected items for remove to  selectedCropsIds in the sprint 163

                if (e.RowIndex >= 0 && e.RowIndex < this.CropGridView.OriginalRowCount)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (e.ColumnIndex.Equals(this.CropGridView.Columns["ValidStatus"].Index))
                        {
                            int cropId;
                            int.TryParse(this.CropGridView.Rows[e.RowIndex].Cells[this.getCropdetailsDataTable.CropIDColumn.ColumnName].Value.ToString(), out cropId);
                            if (cropId > 0)
                            {
                                if (Convert.ToBoolean(this.CropGridView.Rows[e.RowIndex].Cells[this.ValidStatus.Name].EditedFormattedValue) == true)
                                {
                                    this.CropGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                                    if (this.selectedCropsIds.Contains(cropId))
                                    {
                                        this.selectedCropsIds.Remove(Convert.ToInt32(this.CropGridView[this.getCropdetailsDataTable.CropIDColumn.ColumnName, e.RowIndex].Value));
                                        if (this.selectedCropsIds.Count == 0)
                                        {
                                            this.RemoveButton.Enabled = false;
                                        }
                                    }
                                    if (this.cropgridRowCount == 0 && this.selectedCropsIds.Count == 0)
                                    {
                                        this.RemoveButton.Enabled = false;
                                    }
                                    if (this.cropgridRowCount > this.selectedCropsIds.Count)
                                    {
                                        this.SelectAllCheckBox.Checked = false;
                                    }
                                }
                                else
                                {
                                    this.CropGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                                    if (!this.selectedCropsIds.Contains(cropId))
                                    {
                                        this.selectedCropsIds.Add(Convert.ToInt32(this.CropGridView[this.getCropdetailsDataTable.CropIDColumn.ColumnName, e.RowIndex].Value));
                                    }
                                    this.RemoveButton.Enabled = true;
                                    if (this.cropgridRowCount == this.selectedCropsIds.Count)
                                    {
                                        this.SelectAllCheckBox.Checked = true;
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
        /// To Remove the selected Crops in the remove button functionality "RemoveButton_Click".
        /// </summary>
        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.deletePermission)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("RemoveRecords"), SharedFunctions.GetResourceString("RemoveRecordHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        GetSelectedCropIdsXml();
                        this.form36041Control.WorkItem.F36041_DeleteCropIds(this.selectedCropsIdsXml, TerraScanCommon.UserId);
                        this.CustomizeCropCodeGridView();
                        this.LoadCropCodeDetails();
                        this.CropGridView.RefreshEdit();
                        this.RemoveButton.Enabled = false;
                        this.SelectAllCheckBox.Checked = false;


                        this.CustomizeCropCodeGridView();
                        this.allowDelete = true;
                        this.LoadCropCodeDetails();
                        ////Added On 29/2/2008
                        if (this.CropGridView.OriginalRowCount > 0)
                        {
                            decimal totalvalue = 0;
                            decimal tmptotvalue = 0;
                            for (int i = 0; i < CropGridView.RowCount - 1; i++)
                            {
                                decimal.TryParse(this.CropGridView.Rows[i].Cells[this.Value.Name].Value.ToString().Trim(), out totalvalue);
                                tmptotvalue = tmptotvalue + totalvalue;
                            }

                            this.ValueTotalOverrideTextBox.Text = tmptotvalue.ToString();
                        }

                        decimal resultAmount;
                        Decimal.TryParse(this.ValueTotalOverrideTextBox.Text.Trim(), System.Globalization.NumberStyles.Currency, null, out resultAmount);

                        F35002SubFormSaveEventArgs subFormSaveEventArgs;
                        subFormSaveEventArgs.type = 5;
                        subFormSaveEventArgs.value = resultAmount;
                        subFormSaveEventArgs.valueSliceId = this.valueSliceId;

                        subFormSaveEventArgs.amount = resultAmount;
                        this.D35000_F35002_SubFormSave(this, new DataEventArgs<F35002SubFormSaveEventArgs>(subFormSaveEventArgs));
                        ////Commented by Biju on 20/Jan/2010 to fix #4246
                        ////TerraScanCommon.SetDataGridViewPosition(this.CropGridView, 0);
                        ////this.allowDelete = false;
                        ////till here

                        this.pageMode = TerraScanCommon.PageModeTypes.View;

                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Top most check box change event.
        /// </summary>
        private void SelectAllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (SelectAllCheckBox.Checked == true)
                {
                    selectedCropsIds = new List<int>();
                    if (this.cropgridRowCount > 0)
                    {
                        this.SelectUnSelectAll("True");
                        this.RemoveButton.Enabled = true;
                    }
                    this.CalculateSelectAllCrops(SelectAllCheckBox.Checked);

                }
                else if (SelectAllCheckBox.Checked == false)
                {
                    if (this.cropgridRowCount > 0 && this.cropgridRowCount <= this.selectedCropsIds.Count)
                    {
                        this.SelectUnSelectAll("False");
                        this.RemoveButton.Enabled = false;
                        this.CalculateUnSelectCrops(SelectAllCheckBox.Checked);
                    }

                }



            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
        }
        /// <summary>
        /// For thickness of border panel
        /// </summary>
        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            if (panel5.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(0,
                                                              0,
                                                              panel5.ClientSize.Width - thickness,
                                                              panel5.ClientSize.Height - thickness));
                }
            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// For thickness of border to TotalAcresLabel
        /// </summary>
        private void TotalAcresLabel_Paint(object sender, PaintEventArgs e)
        {
            if (TotalAcresLabel.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              TotalAcresLabel.ClientSize.Width - thickness,
                                                              TotalAcresLabel.ClientSize.Height - thickness));
                }
            }
        }
        /// <summary>
        /// For thickness of border to TotalValueLabel
        /// </summary>
        private void TotalValueLabel_Paint(object sender, PaintEventArgs e)
        {
            if (TotalValueLabel.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              TotalValueLabel.ClientSize.Width - thickness,
                                                              TotalValueLabel.ClientSize.Height - thickness));
                }
            }
        }

        private void CropGridVerticalScroll_Scroll(object sender, ScrollEventArgs e)
        {

        }

      
    }
}
