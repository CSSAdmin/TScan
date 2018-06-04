//--------------------------------------------------------------------------------------------
// <copyright file="F29505.cs" company="Congruent">
//     Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F29505.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date	     	    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 29 Dec 08        R.Malliga           Created
//27 Mar 09         R.Malliga           For the Issue 5668 
//27 Apr  09        R.Malliga           For the Issue 5850    
//*********************************************************************************/

namespace D29505
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using TerraScan.SmartParts;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;

    /// <summary>
    /// F29505 Class File
    /// </summary>
    [SmartPart]
    public partial class F29505 : BaseSmartPart
    {
        #region Variables

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
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        /// <summary>
        /// Usede to store the form slice key id
        /// </summary>
        private int eventId;

        /// <summary>
        /// Usede to store the Owner id
        /// </summary>
        private int ownerId = 0;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// controller F24505
        /// </summary>
        private F29505Controller form29505Control;

        /// <summary>
        /// PartiesOwnerDetailsData
        /// </summary>
        private PartiesOwnerDetailsData ownerDetailDataSet = new PartiesOwnerDetailsData();

        /// <summary>
        /// F29505SubdivisionDataSet
        /// </summary>
        private F29505CreateSubdivisionData subDivisionDataSet = new F29505CreateSubdivisionData();

        private F29505CreateSubdivisionData LandCodeDataSet = new F29505CreateSubdivisionData();

        /// <summary>
        /// Used to store situsManagementData
        /// </summary>
        private F25003SitusManagementData situsManagementData = new F25003SitusManagementData();

        private DataTable AdjustmenttypeDataTable = new DataTable();
        private F2550TaxRollCorrectionData.ConfiguredStateDataTable ConfiguredTable = new F2550TaxRollCorrectionData.ConfiguredStateDataTable();

        private F26000ParcelHeaderFormData.f26000ClassCodeDataTable classCodeDataTable = new F26000ParcelHeaderFormData.f26000ClassCodeDataTable();

        /// <summary>
        /// LandData class
        /// </summary>
        private F29505CreateSubdivisionData formLandCodeData;

        /// <summary>
        /// Usede to store the MakeSubId
        /// </summary>
        private int makeSubIdValue = 0;

        /// <summary>
        /// Usede to store the isProcessed
        /// </summary>
        private int isprocessed;

        /// <summary>
        /// Stores the parcel formula to calculate the next row.
        /// </summary>
        private string parcelFormula;

        /// <summary>
        /// Stores the formula for each of the column.
        /// </summary>
        private Hashtable formulaDatas = new Hashtable();

        /// <summary>
        /// Stores the each column has alphaNumeric or String.
        /// </summary>
        private Hashtable alphaNumericDatas = new Hashtable();

        /// <summary>
        /// Stores the each column values.
        /// </summary>
        private Hashtable alphaNumericString = new Hashtable();

        /// <summary>
        /// Current column is alphanumeric or not.
        /// </summary>
        private bool alphaNumeric;

        /// <summary>
        /// Stores the selected combo box value.
        /// </summary>
        private string comboText = string.Empty;

        /// <summary>
        /// Stores the integer part of the alpha numeric.
        /// </summary>
        private string valueString = string.Empty;

        /// <summary>
        /// noofparcelflag
        /// </summary>
        private bool noofparcelflag = false;

        /// <summary>
        /// tempSubDivision
        /// </summary>
        private int tempSubDivision;

        /// <summary>
        /// tempDOR
        /// </summary>
        private string tempDOR;

        /// <summary>
        /// tempNeighborhood
        /// </summary>
        private int tempNeighborhood;

        /// <summary>
        /// tempDistrict
        /// </summary>
        private int tempDistrict;

        /// <summary>
        /// tempLandType1
        /// </summary>
        private int tempLandType1;

        /// <summary>
        /// tempLandType2
        /// </summary>
        private int tempLandType2;

        /// <summary>
        /// tempLandType3
        /// </summary>
        private int tempLandType3;

        /// <summary>
        /// rowIndex
        /// </summary>
        private int rowIndex;

        /// <summary>
        /// columnIndex
        /// </summary>
        private int columnIndex;

        private int scrollpos = 0;
        private bool scrollmov = false;
        private bool msgBoxShow = false;

        private bool editallow = true;

        private string stateConfigured = string.Empty;
      
        /// <summary>
        /// templength
        /// </summary>
        private int templength = 0;
        private string tempClassCode;
        private int classCodeConfigValue;
        private string classCode;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F24505"/> class.
        /// </summary>
        public F29505()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F24505"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29505(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.eventId = keyID;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.sectionIndicatorText = tabText;
            this.formMasterPermissionEdit = permissionEdit;
            this.CreateSubDivisionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CreateSubDivisionPictureBox.Height, this.CreateSubDivisionPictureBox.Width, tabText, red, green, blue);
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
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for EnableButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationEntity>> EnableButtons;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Set Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for getting the form status
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> GetFormStatus;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form24505 control.
        /// </summary>
        /// <value>The form24505 control.</value>
        [CreateNew]
        public F29505Controller Form29505Control
        {
            get { return this.form29505Control as F29505Controller; }
            set { this.form29505Control = value; }
        }

        #endregion PropertyDefaultOwnerTextBox

        #region Event Subscription

        /// <summary>
        /// Event Subscription for cancel slice information.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
        {
            this.ClearControl();
            this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);
            //Modifed to implement #21431 CO by purushotham
            //if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count > 0)
            //{
            //    int.TryParse(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["AutoCompleteValue"].ToString(), out this.classCodeConfigValue);
            //}
            this.FillAllDetails();
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                //// For Header Details
                F29505CreateSubdivisionData createSubdivisionData = new F29505CreateSubdivisionData();
                createSubdivisionData.F29505_Get_SubdivisionHeaderDetails.Clear();
                F29505CreateSubdivisionData.F29505_Get_SubdivisionHeaderDetailsRow createSubdivisionRow = createSubdivisionData.F29505_Get_SubdivisionHeaderDetails.NewF29505_Get_SubdivisionHeaderDetailsRow();

                createSubdivisionRow.EventID = this.eventId;
                createSubdivisionRow.MakeSubID = this.makeSubIdValue;
                createSubdivisionRow.RollYear = Convert.ToInt16(this.RollYearTextBox.Text);
                createSubdivisionRow.OwnerID = this.ownerId;
                createSubdivisionRow.OwnerName = this.DefaultOwnerTextBox.Text.Trim();
                createSubdivisionRow.StateCode = this.DORCodeComboBox.Text.Trim();
                createSubdivisionRow.DistrictID = Convert.ToInt32(this.DistrictComboBox.SelectedValue);
                createSubdivisionRow.NBHDID = Convert.ToInt32(this.NeighborhoodComboBox.SelectedValue);
                createSubdivisionRow.Neighborhood = this.NeighborhoodComboBox.SelectedText;
                createSubdivisionRow.SubdivisionID = Convert.ToInt32(this.SubdivisionComboBox.SelectedValue);
                createSubdivisionRow.LandTypeID1 = Convert.ToInt32(this.LandType1ComboBox.SelectedValue);
                createSubdivisionRow.LandTypeID2 = Convert.ToInt32(this.LandType2ComboBox.SelectedValue);
                createSubdivisionRow.LandTypeID3 = Convert.ToInt32(this.LandType3ComboBox.SelectedValue);
                createSubdivisionRow.LandCode = this.LandCodeTextBox.Text.Trim();
                createSubdivisionRow.NumParcels = Convert.ToByte(this.NoOfParcelTextBox.Text);
               // createSubdivisionRow.ClassCode = this.ClassCodeComboBox.Text.Trim();
                if (!string.IsNullOrEmpty(this.ClassCodeComboBox.Text.Trim()))
                {
                    string tempVar = this.ClassCodeComboBox.Text.Trim();
                    tempVar = tempVar.Replace(" ", "");
                    createSubdivisionRow.ClassCode = tempVar;
                }
                createSubdivisionRow.IsProcessed = false;
                
              
                createSubdivisionData.F29505_Get_SubdivisionHeaderDetails.Rows.Add(createSubdivisionRow);

                string xmlSubdivisionHeaderDetails = TerraScanCommon.GetXmlString(createSubdivisionData.F29505_Get_SubdivisionHeaderDetails);
                string xmlSubdivisionGridDetails = TerraScanCommon.GetXmlString(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails);

                this.makeSubIdValue = this.form29505Control.WorkItem.F29505_SaveDivisionParcels(this.eventId, xmlSubdivisionHeaderDetails, xmlSubdivisionGridDetails, TerraScanCommon.UserId);
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = this.eventId;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
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
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count > 0)
                {
                    eventArgs.Data.FlagInvalidSliceKey = false;
                }
                else
                {
                    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //// Last Slice does not have a record also it will not return invalid slice
                    if (eventArgs.Data.FlagInvalidSliceKey)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = true;
                    }
                }
                this.DefaultOwnerPictureBox.Focus();
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
                this.eventId = eventArgs.Data.SelectedKeyId;
                this.CreateSubdivisionMainPanel.AutoScroll = false;
                this.ClearControl();
                this.subDivisionDataSet = this.form29505Control.WorkItem.F429505_ListAllComoboxes(this.eventId);
                ////modified to reload the comboboxes by purushotham #18825
                ////To bind DORDetails
                if (this.subDivisionDataSet.F29505_StateCodeComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_StateCodeComboDetailsDataTable listStatecodeComboDatatable = new F29505CreateSubdivisionData.F29505_StateCodeComboDetailsDataTable();
                    DataRow listStateCodeRow = listStatecodeComboDatatable.NewRow();
                    listStateCodeRow[listStatecodeComboDatatable.StateCodeIDColumn.ColumnName] = "0";
                    listStateCodeRow[listStatecodeComboDatatable.StateCodeColumn.ColumnName] = string.Empty;
                    listStatecodeComboDatatable.Rows.Add(listStateCodeRow);
                    listStatecodeComboDatatable.Merge(this.subDivisionDataSet.F29505_StateCodeComboDetails);
                    this.DORCodeComboBox.DataSource = listStatecodeComboDatatable;
                    this.DORCodeComboBox.DisplayMember = this.subDivisionDataSet.F29505_StateCodeComboDetails.StateCodeColumn.ColumnName;
                    this.DORCodeComboBox.ValueMember = this.subDivisionDataSet.F29505_StateCodeComboDetails.StateCodeColumn.ColumnName;
                    this.DORCodeComboBox.SelectedIndex = -1;
                }

                ////To Bind DistrictDetails
                if (this.subDivisionDataSet.F29505_DistrictComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_DistrictComboDetailsDataTable listDistrictComboDataTable = new F29505CreateSubdivisionData.F29505_DistrictComboDetailsDataTable();
                    DataRow listDistrictRow = listDistrictComboDataTable.NewRow();
                    listDistrictRow[listDistrictComboDataTable.DistrictIDColumn.ColumnName] = "0";
                    listDistrictRow[listDistrictComboDataTable.DistrictColumn.ColumnName] = string.Empty;
                    listDistrictComboDataTable.Rows.Add(listDistrictRow);
                    listDistrictComboDataTable.Merge(this.subDivisionDataSet.F29505_DistrictComboDetails);
                    this.DistrictComboBox.DataSource = listDistrictComboDataTable;
                    this.DistrictComboBox.DisplayMember = this.subDivisionDataSet.F29505_DistrictComboDetails.DistrictColumn.ColumnName;
                    this.DistrictComboBox.ValueMember = this.subDivisionDataSet.F29505_DistrictComboDetails.DistrictIDColumn.ColumnName;
                    this.DistrictComboBox.SelectedIndex = -1;
                }

                ////To Bind NeighborhoodDetails
                if (this.subDivisionDataSet.F29505_NeighborhoodComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_NeighborhoodComboDetailsDataTable listNeighborhoodComboDataTable = new F29505CreateSubdivisionData.F29505_NeighborhoodComboDetailsDataTable();
                    DataRow listNeighborhoodRow = listNeighborhoodComboDataTable.NewRow();
                    listNeighborhoodRow[listNeighborhoodComboDataTable.NBHDIDColumn.ColumnName] = "0";
                    listNeighborhoodRow[listNeighborhoodComboDataTable.NeighborhoodColumn.ColumnName] = string.Empty;
                    listNeighborhoodComboDataTable.Rows.Add(listNeighborhoodRow);
                    listNeighborhoodComboDataTable.Merge(this.subDivisionDataSet.F29505_NeighborhoodComboDetails);
                    this.NeighborhoodComboBox.DataSource = listNeighborhoodComboDataTable;
                    this.NeighborhoodComboBox.DisplayMember = this.subDivisionDataSet.F29505_NeighborhoodComboDetails.NeighborhoodColumn.ColumnName;
                    this.NeighborhoodComboBox.ValueMember = this.subDivisionDataSet.F29505_NeighborhoodComboDetails.NBHDIDColumn.ColumnName;
                    this.NeighborhoodComboBox.SelectedIndex = -1;
                }

                ////To Bind SubdivisionDetails
                if (this.subDivisionDataSet.F29505_SubdivisionComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_SubdivisionComboDetailsDataTable listSubDivisionDatatTable = new F29505CreateSubdivisionData.F29505_SubdivisionComboDetailsDataTable();
                    DataRow listSubDivisionRow = listSubDivisionDatatTable.NewRow();
                    listSubDivisionRow[listSubDivisionDatatTable.SubdivisionIDColumn.ColumnName] = "0";
                    listSubDivisionRow[listSubDivisionDatatTable.SubNameColumn.ColumnName] = string.Empty;
                    listSubDivisionDatatTable.Rows.Add(listSubDivisionRow);
                    listSubDivisionDatatTable.Merge(this.subDivisionDataSet.F29505_SubdivisionComboDetails);
                    this.SubdivisionComboBox.DataSource = listSubDivisionDatatTable;
                    this.SubdivisionComboBox.DisplayMember = this.subDivisionDataSet.F29505_SubdivisionComboDetails.SubNameColumn.ColumnName;
                    this.SubdivisionComboBox.ValueMember = this.subDivisionDataSet.F29505_SubdivisionComboDetails.SubdivisionIDColumn.ColumnName;
                    this.SubdivisionComboBox.SelectedIndex = -1;
                }

                ////To Bind LandType1 Details
                if (this.subDivisionDataSet.F29505_LandType1ComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_LandType1ComboDetailsDataTable listLandType1ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType1ComboDetailsDataTable();
                    DataRow listLandType1Row = listLandType1ComboDataTable.NewRow();
                    listLandType1Row[listLandType1ComboDataTable.LandTypeID1Column.ColumnName] = "0";
                    listLandType1Row[listLandType1ComboDataTable.LandType1Column.ColumnName] = string.Empty;
                    listLandType1ComboDataTable.Rows.Add(listLandType1Row);
                    listLandType1ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType1ComboDetails);
                    this.LandType1ComboBox.DataSource = listLandType1ComboDataTable;
                    this.LandType1ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType1ComboDetails.LandType1Column.ColumnName;
                    this.LandType1ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType1ComboDetails.LandTypeID1Column.ColumnName;
                    this.LandType1ComboBox.SelectedIndex = -1;
                }

                ////To Bind LandType2 Details
                if (this.subDivisionDataSet.F29505_LandType1ComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_LandType2ComboDetailsDataTable listLandType2ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType2ComboDetailsDataTable();
                    DataRow listLandType2Row = listLandType2ComboDataTable.NewRow();
                    listLandType2Row[listLandType2ComboDataTable.LandTypeID2Column.ColumnName] = "0";
                    listLandType2Row[listLandType2ComboDataTable.LandType2Column.ColumnName] = string.Empty;
                    listLandType2ComboDataTable.Rows.Add(listLandType2Row);
                    listLandType2ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType2ComboDetails);
                    this.LandType2ComboBox.DataSource = listLandType2ComboDataTable;
                    this.LandType2ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType2ComboDetails.LandType2Column.ColumnName;
                    this.LandType2ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType2ComboDetails.LandTypeID2Column.ColumnName;
                    this.LandType2ComboBox.SelectedIndex = -1;
                }

                ////To Bind LandType3 Details
                if (this.subDivisionDataSet.F29505_LandType3ComboDetails.Rows.Count > 0)
                {
                    ////Coding Added for the issue 5850
                    F29505CreateSubdivisionData.F29505_LandType3ComboDetailsDataTable listLandType3ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType3ComboDetailsDataTable();
                    DataRow listLandType3Row = listLandType3ComboDataTable.NewRow();
                    listLandType3Row[listLandType3ComboDataTable.LandTypeID3Column.ColumnName] = "0";
                    listLandType3Row[listLandType3ComboDataTable.LandType3Column.ColumnName] = string.Empty;
                    listLandType3ComboDataTable.Rows.Add(listLandType3Row);
                    listLandType3ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType3ComboDetails);
                    this.LandType3ComboBox.DataSource = listLandType3ComboDataTable;
                    this.LandType3ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType3ComboDetails.LandType3Column.ColumnName;
                    this.LandType3ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType3ComboDetails.LandTypeID3Column.ColumnName;
                    this.LandType3ComboBox.SelectedIndex = -1;
                }
                this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);

                if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count > 0)
                {
                    int.TryParse(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["AutoCompleteValue"].ToString(), out this.classCodeConfigValue);
                }
                //To Customise Grid Coulmns 
                this.CustomiseGrid();
                this.FillAllGridCombos();
                this.FillGridCombos();
                this.FillAllDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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


        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            //RecordNavigationEntity recordNavigationEntity = e.Data;
            ////this.eventId=e.Data.
            //this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);
            //this.FillAllDetails();
            //this.CurrentImportId = this.RetrieveImportId(recordNavigationEntity.RecordIndex);
            //this.additionalOperationSmartPart.KeyId = this.currentImportId;
            //this.FillImportFormDetails(null, recordNavigationEntity.RecordNavigationFlag);
        }
        #endregion

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

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is integer; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInteger(string value)
        {
            return IsMatch(value, @"^([0-9]*|[0-9]*(\[0-9])[0-9]*)$");
        }

        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsMatch(string value, string pattern)
        {
            System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex(@pattern);
            if (objRegex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Form Load
        /// <summary>
        /// Handles the Load event of the F24505 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F24505_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                if (this.eventId > 0)
                {
                    //this.hScrollBar1.Location = new System.Drawing.Point(this.CreateSubdivisionMainPanel.Location.X + 200, this.CreateSubdivisionMainPanel.Height + 5);
                    //this.hScrollBar1.BringToFront();
                    //this.CreateSubdivisionMainPanel.Controls.Add(this.hScrollBar1);  
                    this.CreateSubdivisionMainPanel.AutoScroll = false;

                    this.subDivisionDataSet = this.form29505Control.WorkItem.F429505_ListAllComoboxes(this.eventId);                    
       
                    this.ConfiguredTable= this.form29505Control.WorkItem.F2550_GetConfiguredState().ConfiguredState;
                    if (this.ConfiguredTable.Rows.Count > 0)
                    {
                        this.stateConfigured = this.ConfiguredTable.Rows[0][0].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(this.stateConfigured) && this.stateConfigured.ToLower().ToString().Equals("ne"))
                    {
                        
                        this.DORCodePanel.Visible = false;
                        this.DORCodePanel.SendToBack();
                        this.ClassCodePanel.Visible = true;
                        this.ClassCodePanel.Enabled = true;                        
                        this.ClassCodePanel.BringToFront();
                    }
                    else
                    {
                       // this.ClassCodePanel.Visible = false;
                        //this.ClassCodePanel.SendToBack();
                        this.DORCodePanel.Visible = true;
                        this.DORCodePanel.BringToFront();
                        this.DORCodePanel.Enabled = true;
                        
                    }
       
                    ////To bind DORDetails
                    if (this.subDivisionDataSet.F29505_StateCodeComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_StateCodeComboDetailsDataTable listStatecodeComboDatatable = new F29505CreateSubdivisionData.F29505_StateCodeComboDetailsDataTable();
                        DataRow listStateCodeRow = listStatecodeComboDatatable.NewRow();
                        listStateCodeRow[listStatecodeComboDatatable.StateCodeIDColumn.ColumnName] = "0";
                        listStateCodeRow[listStatecodeComboDatatable.StateCodeColumn.ColumnName] = string.Empty;
                        listStatecodeComboDatatable.Rows.Add(listStateCodeRow);
                        listStatecodeComboDatatable.Merge(this.subDivisionDataSet.F29505_StateCodeComboDetails);
                        this.DORCodeComboBox.DataSource = listStatecodeComboDatatable;
                        this.DORCodeComboBox.DisplayMember = this.subDivisionDataSet.F29505_StateCodeComboDetails.StateCodeColumn.ColumnName;
                        this.DORCodeComboBox.ValueMember = this.subDivisionDataSet.F29505_StateCodeComboDetails.StateCodeColumn.ColumnName;
                        this.DORCodeComboBox.SelectedIndex = -1;
                    }

                    ////To Bind DistrictDetails
                    if (this.subDivisionDataSet.F29505_DistrictComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_DistrictComboDetailsDataTable listDistrictComboDataTable = new F29505CreateSubdivisionData.F29505_DistrictComboDetailsDataTable();
                        DataRow listDistrictRow = listDistrictComboDataTable.NewRow();
                        listDistrictRow[listDistrictComboDataTable.DistrictIDColumn.ColumnName] = "0";
                        listDistrictRow[listDistrictComboDataTable.DistrictColumn.ColumnName] = string.Empty;
                        listDistrictComboDataTable.Rows.Add(listDistrictRow);
                        listDistrictComboDataTable.Merge(this.subDivisionDataSet.F29505_DistrictComboDetails);
                        this.DistrictComboBox.DataSource = listDistrictComboDataTable;
                        this.DistrictComboBox.DisplayMember = this.subDivisionDataSet.F29505_DistrictComboDetails.DistrictColumn.ColumnName;
                        this.DistrictComboBox.ValueMember = this.subDivisionDataSet.F29505_DistrictComboDetails.DistrictIDColumn.ColumnName;
                        this.DistrictComboBox.SelectedIndex = -1;
                    }

                    ////To Bind NeighborhoodDetails
                    if (this.subDivisionDataSet.F29505_NeighborhoodComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_NeighborhoodComboDetailsDataTable listNeighborhoodComboDataTable = new F29505CreateSubdivisionData.F29505_NeighborhoodComboDetailsDataTable();
                        DataRow listNeighborhoodRow = listNeighborhoodComboDataTable.NewRow();
                        listNeighborhoodRow[listNeighborhoodComboDataTable.NBHDIDColumn.ColumnName] = "0";
                        listNeighborhoodRow[listNeighborhoodComboDataTable.NeighborhoodColumn.ColumnName] = string.Empty;
                        listNeighborhoodComboDataTable.Rows.Add(listNeighborhoodRow);
                        listNeighborhoodComboDataTable.Merge(this.subDivisionDataSet.F29505_NeighborhoodComboDetails);
                        this.NeighborhoodComboBox.DataSource = listNeighborhoodComboDataTable;
                        this.NeighborhoodComboBox.DisplayMember = this.subDivisionDataSet.F29505_NeighborhoodComboDetails.NeighborhoodColumn.ColumnName;
                        this.NeighborhoodComboBox.ValueMember = this.subDivisionDataSet.F29505_NeighborhoodComboDetails.NBHDIDColumn.ColumnName;
                        this.NeighborhoodComboBox.SelectedIndex = -1;
                    }

                    ////To Bind SubdivisionDetails
                    if (this.subDivisionDataSet.F29505_SubdivisionComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_SubdivisionComboDetailsDataTable listSubDivisionDatatTable = new F29505CreateSubdivisionData.F29505_SubdivisionComboDetailsDataTable();
                        DataRow listSubDivisionRow = listSubDivisionDatatTable.NewRow();
                        listSubDivisionRow[listSubDivisionDatatTable.SubdivisionIDColumn.ColumnName] = "0";
                        listSubDivisionRow[listSubDivisionDatatTable.SubNameColumn.ColumnName] = string.Empty;
                        listSubDivisionDatatTable.Rows.Add(listSubDivisionRow);
                        listSubDivisionDatatTable.Merge(this.subDivisionDataSet.F29505_SubdivisionComboDetails);
                        this.SubdivisionComboBox.DataSource = listSubDivisionDatatTable;
                        this.SubdivisionComboBox.DisplayMember = this.subDivisionDataSet.F29505_SubdivisionComboDetails.SubNameColumn.ColumnName;
                        this.SubdivisionComboBox.ValueMember = this.subDivisionDataSet.F29505_SubdivisionComboDetails.SubdivisionIDColumn.ColumnName;
                        this.SubdivisionComboBox.SelectedIndex = -1;
                    }

                    ////To Bind LandType1 Details
                    if (this.subDivisionDataSet.F29505_LandType1ComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_LandType1ComboDetailsDataTable listLandType1ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType1ComboDetailsDataTable();
                        DataRow listLandType1Row = listLandType1ComboDataTable.NewRow();
                        listLandType1Row[listLandType1ComboDataTable.LandTypeID1Column.ColumnName] = "0";
                        listLandType1Row[listLandType1ComboDataTable.LandType1Column.ColumnName] = string.Empty;
                        listLandType1ComboDataTable.Rows.Add(listLandType1Row);
                        listLandType1ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType1ComboDetails);
                        this.LandType1ComboBox.DataSource = listLandType1ComboDataTable;
                        this.LandType1ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType1ComboDetails.LandType1Column.ColumnName;
                        this.LandType1ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType1ComboDetails.LandTypeID1Column.ColumnName;
                        this.LandType1ComboBox.SelectedIndex = -1;
                    }

                    ////To Bind LandType2 Details
                    if (this.subDivisionDataSet.F29505_LandType1ComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_LandType2ComboDetailsDataTable listLandType2ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType2ComboDetailsDataTable();
                        DataRow listLandType2Row = listLandType2ComboDataTable.NewRow();
                        listLandType2Row[listLandType2ComboDataTable.LandTypeID2Column.ColumnName] = "0";
                        listLandType2Row[listLandType2ComboDataTable.LandType2Column.ColumnName] = string.Empty;
                        listLandType2ComboDataTable.Rows.Add(listLandType2Row);
                        listLandType2ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType2ComboDetails);
                        this.LandType2ComboBox.DataSource = listLandType2ComboDataTable;
                        this.LandType2ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType2ComboDetails.LandType2Column.ColumnName;
                        this.LandType2ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType2ComboDetails.LandTypeID2Column.ColumnName;
                        this.LandType2ComboBox.SelectedIndex = -1;
                    }

                    ////To Bind LandType3 Details
                    if (this.subDivisionDataSet.F29505_LandType3ComboDetails.Rows.Count > 0)
                    {
                        ////Coding Added for the issue 5850
                        F29505CreateSubdivisionData.F29505_LandType3ComboDetailsDataTable listLandType3ComboDataTable = new F29505CreateSubdivisionData.F29505_LandType3ComboDetailsDataTable();
                        DataRow listLandType3Row = listLandType3ComboDataTable.NewRow();
                        listLandType3Row[listLandType3ComboDataTable.LandTypeID3Column.ColumnName] = "0";
                        listLandType3Row[listLandType3ComboDataTable.LandType3Column.ColumnName] = string.Empty;
                        listLandType3ComboDataTable.Rows.Add(listLandType3Row);
                        listLandType3ComboDataTable.Merge(this.subDivisionDataSet.F29505_LandType3ComboDetails);
                        this.LandType3ComboBox.DataSource = listLandType3ComboDataTable;
                        this.LandType3ComboBox.DisplayMember = this.subDivisionDataSet.F29505_LandType3ComboDetails.LandType3Column.ColumnName;
                        this.LandType3ComboBox.ValueMember = this.subDivisionDataSet.F29505_LandType3ComboDetails.LandTypeID3Column.ColumnName;
                        this.LandType3ComboBox.SelectedIndex = -1;
                    }

                    ////To Bind CreateSubdivison grid Combo Selection
                    this.FillAllGridCombos();
                    this.FillGridCombos();
                    ////To Customise Grid Coulmns 
                    this.CustomiseGrid();

                    this.FillAllDetails();
                }
                else
                {
                    this.FormSliceLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    this.ProcessButton.BackColor = Color.FromArgb(28, 81, 128);
                    this.ProcessButton.StatusOffColor = Color.FromArgb(28, 81, 128);
                    this.RollYearpanel.Enabled = false;
                    this.DeafaultOwnerPanel.Enabled = false;
                    this.DORCodePanel.Enabled = false;
                    this.DistrictPanel.Enabled = false;
                    this.NeighborhoodPanel.Enabled = false;
                    this.SubdivisionPanel.Enabled = false;
                    this.LandType1Panel.Enabled = false;
                    this.LandType2panel.Enabled = false;
                    this.LantType3panel.Enabled = false;
                    this.LandCodePanel.Enabled = false;
                    this.NoOfParcelPanel.Enabled = false;
                    this.ButtonPanel.Enabled = false;
                    this.GridComboSelectionPanel.Enabled = false;
                    this.GridPanel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Checks the required field.
        /// </summary>
        /// <returns>Returns boolean</returns>
        private bool CheckRequiredField()
        {

            if (!this.stateConfigured.ToLower().ToString().Equals("ne"))
            {
                if (string.IsNullOrEmpty(this.DORCodeComboBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F29505DORcodevalidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505Rollyearvalidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
                
            else if (string.IsNullOrEmpty(this.DistrictComboBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505DistrictValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505NeighborhoodValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else if (string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505CheckNoOfParcels"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int numofparcel = Convert.ToInt32(this.NoOfParcelTextBox.Text.Trim());
            if (numofparcel != this.SubdivisionGridView.OriginalRowCount)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505NoOfParcelsChecking"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (this.makeSubIdValue.Equals(0))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505ProcessChecking"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            ////Coding Added for the Issue 5850
            for (int i = 0; i <= this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count - 1; i++)
            {
                string formula = "ParcelNumber='" + this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["ParcelNumber"] + "'";
                DataRow[] subdivisionrow = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Select(formula);
                if (subdivisionrow.Length > 1)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F29505DupParcelChecking"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            ////Ends here
            return true;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = this.masterFormNo;

            if (string.IsNullOrEmpty(this.RollYearTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.RollYearTextBox.Focus();
                return sliceValidationFields;
            }
            else if (string.IsNullOrEmpty(this.DORCodeComboBox.Text.Trim()))
            {
                if (!this.stateConfigured.ToLower().ToString().Equals("ne"))
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.DORCodeComboBox.Focus();
                    return sliceValidationFields;
                }
            }
            else if (string.IsNullOrEmpty(this.DistrictComboBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.DistrictComboBox.Focus();
                return sliceValidationFields;
            }
            else if (string.IsNullOrEmpty(this.NeighborhoodComboBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.NeighborhoodComboBox.Focus();
                return sliceValidationFields;
            }
            else if (string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                sliceValidationFields.RequiredFieldMissing = true;
                this.NoOfParcelTextBox.Focus();
                return sliceValidationFields;
            }

            int numofparcel = Convert.ToInt32(this.NoOfParcelTextBox.Text.Trim());

            ////Coding Added for the issue 2254 by malliga on 29/9/2009
            if (numofparcel != this.SubdivisionGridView.OriginalRowCount)
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("NoofParcelsChecking"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    ////sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505NoOfParcelsChecking");
                    sliceValidationFields.DisableNewMethod = true;
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.NoOfParcelTextBox.Focus();
                    return sliceValidationFields;
                }
                else
                {
                    this.NoOfParcelTextBox.Text = this.SubdivisionGridView.OriginalRowCount.ToString();
                }
            }

            if (!string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
            {
                numofparcel = Convert.ToInt32(this.NoOfParcelTextBox.Text.Trim());
                if (numofparcel <= 1)
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505CheckMaxParcels");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.NoOfParcelTextBox.Focus();
                    return sliceValidationFields;
                }

                if (numofparcel > 255)
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505CheckMaxParcels");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.NoOfParcelTextBox.Focus();
                    return sliceValidationFields;
                }
            }


            ////Maximum value checking for Block,Lot,Acres Field
            for (int i = 0; i <= this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Acres"].ToString()))
                {
                    //Modifed by purushotham to fix 21447 Bug on 11march2015
                    double nonStringValue;
                    bool isNumber;
                    //Modifed data type to fix 21447 Bug on 11march2015
                    isNumber = double.TryParse(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Acres"].ToString(), out nonStringValue);
                    if (isNumber)
                    {
                        int len = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Acres"].ToString().Length;
                        int pos = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Acres"].ToString().IndexOf(".");
                        if (pos == -1)
                        {
                            if (len > 9)
                            {
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505AcresMaxValueValidation");
                                return sliceValidationFields;
                            }
                        }
                        else
                        {
                            if (len > 12)
                            {
                                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505AcresMaxValueValidation");
                                return sliceValidationFields;
                            }
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Acres"] = "";
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                    }
                }
                ////For width
                if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotWidth"].ToString()))
                {
                    int len = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotWidth"].ToString().Length;
                    int pos = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotWidth"].ToString().IndexOf(".");
                    if (pos == -1)
                    {
                        if (len > 9)
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505WidthMaxValueValidation");
                            return sliceValidationFields;
                        }
                    }
                    else
                    {
                        if (len > 12)
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505WidthMaxValueValidation");
                            return sliceValidationFields;
                        }
                    }
                }

                ////For Depth
                if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotDepth"].ToString()))
                {
                    int len = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotDepth"].ToString().Length;
                    int pos = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["LotDepth"].ToString().IndexOf(".");
                    if (pos == -1)
                    {
                        if (len > 9)
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505DepthMaxValueValidation");
                            return sliceValidationFields;
                        }
                    }
                    else
                    {
                        if (len > 12)
                        {
                            sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("F29505DepthMaxValueValidation");
                            return sliceValidationFields;
                        }
                    }
                }
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["AdjustmentType"].ToString()))
                {
                    sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                    sliceValidationFields.RequiredFieldMissing = true;
                    this.SubdivisionGridView.Rows[i].Cells["AdjustmentType"].Selected = true;
                    return sliceValidationFields;
                }
                /*  if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["Adjustment"].ToString()))
                  {
                      if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["AdjustmentType"].ToString() != ("0"))
                      {
                          sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString("MissingRequiredMsg");
                          sliceValidationFields.RequiredFieldMissing = true;
                          this.SubdivisionGridView.Rows[i].Cells["Adjustment"].Selected = true;
                          return sliceValidationFields;
                      }
                  }*/

            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Fills all details.
        /// </summary>
        private void FillAllDetails()
        {
            try
            {
                ////To Bind Subdivision Header Details
                if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count > 0)
                {
                    string ClassCodeField = string.Empty;
                    if (!string.IsNullOrEmpty(this.stateConfigured) && (this.stateConfigured.ToLower().ToString().Equals("ne")))
                    {
                        if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["ClassCode"].ToString()))
                        {

                            ClassCodeField = this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0][this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.ClassCodeColumn].ToString();

                            StringBuilder sb = new StringBuilder();
                            if (!string.IsNullOrEmpty(ClassCodeField))
                            {
                                List<string> result = new List<string>(Regex.Split(ClassCodeField, @"(?<=\G.{2})", RegexOptions.Singleline));
                                var count = result.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    if (result[i].Length.Equals(2))
                                    {
                                        if (sb.ToString().Length < 17)
                                        {
                                            string temp1 = result[i].Insert(2, " ").ToString();

                                            sb.Append(temp1);
                                        }

                                    }
                                    else
                                    {
                                        if (sb.ToString().Length < 17)
                                        {
                                            sb.Append(result[i].ToString());
                                        }
                                    }

                                }
                                ClassCodeField = sb.ToString();

                            }

                            this.ClassCodeComboBox.Text = ClassCodeField;
                            this.ClassCodeTextBox.Text = ClassCodeField;
                            string temp = (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0][this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.ClassCodeColorRGBRColumn].ToString());
                            string[] array = temp.Split(',');
                            if (array.Length > 0)
                            {
                                int R = 0;
                                int G = 0;
                                int B = 0;
                                if (array[0].Length > 0)
                                {
                                    R = Convert.ToInt32(array[0]);
                                }
                                if (array[1].Length > 0)
                                {
                                    G = Convert.ToInt32(array[1]);
                                }
                                if (array[2].Length > 0)
                                {
                                    B = Convert.ToInt32(array[2]);
                                }
                                Color foreColor = Color.FromArgb(R, G, B);
                                if (foreColor != null)
                                {
                                    this.ClassCodeComboBox.ForeColor = foreColor;
                                }
                            }
                        }
                        else
                        {
                            this.ClassCodeComboBox.Text = ClassCodeField;
                            this.ClassCodeTextBox.Text = ClassCodeField;
                        }
                    } 
                    this.RollYearTextBox.Text = this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["RollYear"].ToString();
                    this.DefaultOwnerTextBox.Text = this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["OwnerName"].ToString();
                    this.DORCodeComboBox.SelectedValue = this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["StateCode"].ToString();
                    this.tempDOR = this.DORCodeComboBox.Text.Trim();
                    this.DistrictComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["DistrictID"].ToString());
                    if (this.DistrictComboBox.SelectedValue != null)
                    {
                        this.tempDistrict = (int)this.DistrictComboBox.SelectedValue;
                    }

                    this.NeighborhoodComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["NBHDID"].ToString());
                    if (this.NeighborhoodComboBox.SelectedValue != null)
                    {
                        this.tempNeighborhood = (int)this.NeighborhoodComboBox.SelectedValue;
                    }

                    this.SubdivisionComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["SubdivisionID"].ToString());
                    if (this.SubdivisionComboBox.SelectedValue != null)
                    {
                        this.tempSubDivision = (int)this.SubdivisionComboBox.SelectedValue;
                    }

                    this.LandType1ComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["LandTypeID1"].ToString());
                    if (this.LandType1ComboBox.SelectedValue != null)
                    {
                        this.tempLandType1 = (int)this.LandType1ComboBox.SelectedValue;
                    }

                    this.LandType2ComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["LandTypeID2"].ToString());
                    if (this.LandType2ComboBox.SelectedValue != null)
                    {
                        this.tempLandType2 = (int)this.LandType2ComboBox.SelectedValue;
                    }

                    this.LandType3ComboBox.SelectedValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["LandTypeID3"].ToString());
                    if (this.LandType3ComboBox.SelectedValue != null)
                    {
                        this.tempLandType3 = (int)this.LandType3ComboBox.SelectedValue;
                    }

                    this.LandCodeTextBox.Text = this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["LandCode"].ToString();
                    this.makeSubIdValue = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["MakeSubID"].ToString());
                    if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["IsProcessed"].ToString() == "True")
                    {
                        this.isprocessed = 1;
                    }
                    else
                    {
                        this.isprocessed = 0;
                    }

                    if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["OwnerID"].ToString()))
                    {
                        this.ownerId = Convert.ToInt32(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["OwnerID"].ToString());
                    }
                    else
                    {
                        this.ownerId = -1;
                    }
                    int rollYear;
                    int.TryParse(this.RollYearTextBox.Text, out rollYear);
                    this.LandCodeDataSet = this.form29505Control.WorkItem.LandCodes(this.tempNeighborhood, rollYear);
                    DataRow landCodeRow = this.LandCodeDataSet.f36035ListLandCodes.NewRow();
                    landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName] = "";
                    landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.RollYearColumn.ColumnName] = rollYear;
                    this.LandCodeDataSet.f36035ListLandCodes.Rows.InsertAt(landCodeRow, 0);
                }
                else
                {
                    this.ClearControl();
                    this.EnableDisableControls(false);
                }

                ////To Bind Subdivision Grid Details
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count > 0)
                {


                    this.SubdivisionGridView.DataSource = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.DefaultView;



                    for (int i = 0; i < subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count; i++)
                    {
                    if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString()))
                    {
                        if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("Yes".ToLower()))
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = true;
                        }
                        else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().Equals("No"))
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = false;
                        }
                        else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("true"))
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = true;
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "Yes";
                        }
                        else if(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("false"))
                        {

                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = false;
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "No";
                        }

                    }

                    }  

                    this.situsManagementData = this.form29505Control.WorkItem.F25003_ListStreet();

                    (this.Street as DataGridViewComboBoxColumn).DataSource = this.situsManagementData.ListStreet;
                    (this.Street as DataGridViewComboBoxColumn).DisplayMember = this.situsManagementData.ListStreet.StreetNameColumn.ColumnName;
                    (this.Street as DataGridViewComboBoxColumn).ValueMember = this.situsManagementData.ListStreet.StreetIDColumn.ColumnName;

                    if (this.AdjustmenttypeDataTable.Columns.Count.Equals(0))
                    {
                        this.AdjustmenttypeDataTable.Columns.Add("AdjustmentTypeId");
                        this.AdjustmenttypeDataTable.Columns.Add("AdjustmentType");
                    }
                    if (this.AdjustmenttypeDataTable.Rows.Count.Equals(0))
                    {
                        this.AdjustmenttypeDataTable.Clear();

                        // Adding baseAdujstment Types in FormLevel
                        DataRow baseAdjustmentTypeRow1;
                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();

                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 0;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "None";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 1;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Alternate Land Code";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 2;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Factor";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 3;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Unit Value";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 4;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Production";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 5;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Additive";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);

                        ////Added by Biju on 01-Dec-2010 to implement #9328
                        baseAdjustmentTypeRow1 = this.AdjustmenttypeDataTable.NewRow();
                        baseAdjustmentTypeRow1["AdjustmentTypeId"] = 6;
                        baseAdjustmentTypeRow1["AdjustmentType"] = "Total Value";
                        this.AdjustmenttypeDataTable.Rows.Add(baseAdjustmentTypeRow1);
                    }
                    (this.AdjustmentType as DataGridViewComboBoxColumn).DataSource = this.AdjustmenttypeDataTable;
                    (this.AdjustmentType as DataGridViewComboBoxColumn).DisplayMember = "AdjustmentType";
                    (this.AdjustmentType as DataGridViewComboBoxColumn).ValueMember = "AdjustmentTypeId";

                    this.SubdivisionGridView.Columns["MID1"].HeaderText = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label1"].ToString();
                    this.SubdivisionGridView.Columns["MID2"].HeaderText = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label2"].ToString();
                    this.SubdivisionGridView.Columns["MID3"].HeaderText = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label3"].ToString();
                    this.SubdivisionGridView.Columns["MID4"].HeaderText = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label4"].ToString();
                    this.SubdivisionGridView.Columns["MID5"].HeaderText = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label5"].ToString();
                }
                else
                {
                    this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);
                    this.SubdivisionGridView.DataSource = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.DefaultView;
                }
                //this.SubdivisionGridView.Rows[0].Cells[0].Selected = true;    
                int noofParcel = this.SubdivisionGridView.OriginalRowCount;
                if (this.SubdivisionGridView.OriginalRowCount.Equals(0))
                {
                    noofParcel = 1;
                }

                this.SetHeight(noofParcel);

                ////Process button Enabled and Disabled
                if (this.makeSubIdValue > 0)
                {
                    int numParcel = this.SubdivisionGridView.OriginalRowCount;
                    this.NoOfParcelTextBox.Text = numParcel.ToString();
                    if (this.isprocessed.Equals(0))
                    {
                        this.EnableDisableControls(true);
                    }
                    else
                    {
                        this.EnableDisableControls(false);
                    }
                }
                else
                {
                    if (this.SubdivisionGridView.OriginalRowCount > 0)
                    {
                        int numParcel = this.SubdivisionGridView.OriginalRowCount;
                        this.NoOfParcelTextBox.Text = numParcel.ToString();
                        this.EnableDisableControls(true);
                    }
                }

                if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count.Equals(0))
                {
                    this.EnableDisableControls(false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Customises the grid.
        /// </summary>
        private void CustomiseGrid()
        {
            try
            {
                this.subDivisionDataSet.EnforceConstraints = false;
                this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);
                if (this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows.Count > 0)
                {
                    int.TryParse(this.subDivisionDataSet.F29505_Get_SubdivisionHeaderDetails.Rows[0]["AutoCompleteValue"].ToString(), out this.classCodeConfigValue);
                }
                ////this.getSubdivisionGridDetailsDataTable = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails;

                this.SubdivisionGridView.AutoGenerateColumns = false;
                this.Row.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.RowColumn.ColumnName;
                this.MakeSubId.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MakeSubIDColumn.ColumnName;
                this.ItemID.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.ItemIDColumn.ColumnName;
                this.Parcel.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.ParcelNumberColumn.ColumnName;
                this.AddNum.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.HouseNumberColumn.ColumnName;
                this.StreetID.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.StreetIDColumn.ColumnName;
                this.Street.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.StreetIDColumn.ColumnName;
                this.Shape.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.LandShapeColumn.ColumnName;
                this.ShapeID.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.StreetIDColumn.ColumnName;
                this.Block.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.BlockColumn.ColumnName;
                this.Lot.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.LotColumn.ColumnName;
                this.Width.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.LotWidthColumn.ColumnName;
                this.Depth.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.LotDepthColumn.ColumnName;
                this.Acres.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcresColumn.ColumnName;
                this.Dor2.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcresColumn.ColumnName;
                this.MID1.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MID1Column.ColumnName;
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label1"].ToString()))
                {
                    if (this.MID1.Visible)
                    {
                        this.MID1.Visible = false;
                        this.GridComboSelectionPanel.Controls.Remove(this.MID1Panel);
                        this.MID2Panel.Location = new System.Drawing.Point(this.MID1Panel.Location.X, -1);
                        this.MID3Panel.Location = new System.Drawing.Point(this.MID2Panel.Location.X + this.MID2Panel.Width, -1);
                        this.MID4Panel.Location = new System.Drawing.Point(this.MID3Panel.Location.X + this.MID3Panel.Width, -1);
                        this.MID5Panel.Location = new System.Drawing.Point(this.MID4Panel.Location.X + this.MID4Panel.Width, -1);
                        this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X + this.MID5Panel.Width, -1);
                        this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width, -1);
                        this.NewConstructionPanel.Location = new System.Drawing.Point(this.AdjustmentPanel.Location.X + this.AdjustmentPanel.Width, -1);
                        this.GridComboSelectionPanel.Width = this.GridComboSelectionPanel.Width - this.MID1.Width;
                        // this.GridPanel.Width = this.GridPanel.Width - this.MID1Panel.Width;
                    }

                }
                this.MID2.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MID2Column.ColumnName;
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label2"].ToString()))
                {
                    if (this.MID2.Visible)
                    {
                        this.MID2.Visible = false;
                        this.GridComboSelectionPanel.Controls.Remove(this.MID2Panel);
                        this.MID3Panel.Location = new System.Drawing.Point(this.MID2Panel.Location.X, -1);
                        this.MID4Panel.Location = new System.Drawing.Point(this.MID3Panel.Location.X + this.MID3Panel.Width, -1);
                        this.MID5Panel.Location = new System.Drawing.Point(this.MID4Panel.Location.X + this.MID4Panel.Width, -1);
                        this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X + this.MID5Panel.Width, -1);
                        this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width, -1);
                        this.NewConstructionPanel.Location = new System.Drawing.Point(this.AdjustmentPanel.Location.X + this.AdjustmentPanel.Width, -1);
                        this.GridComboSelectionPanel.Width = this.GridComboSelectionPanel.Width - this.MID2.Width;
                        // this.GridPanel.Width = this.GridPanel.Width - this.MID2Panel.Width;
                    }
                }
                this.MID3.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MID3Column.ColumnName;
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label3"].ToString()))
                {
                    if (this.MID3.Visible)
                    {
                        this.MID3.Visible = false;
                        this.GridComboSelectionPanel.Controls.Remove(this.MID3Panel);
                        //this.MID2Panel.Location = new System.Drawing.Point(this.MID1Panel.Location.X, -1);
                        // this.MID3Panel.Location = new System.Drawing.Point(this.MID2Panel.Location.X, -1);
                        this.MID4Panel.Location = new System.Drawing.Point(this.MID3Panel.Location.X, -1);
                        this.MID5Panel.Location = new System.Drawing.Point(this.MID4Panel.Location.X + this.MID4Panel.Width, -1);
                        this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X + this.MID5Panel.Width, -1);
                        this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width, -1);
                        this.NewConstructionPanel.Location = new System.Drawing.Point(this.AdjustmentPanel.Location.X + this.AdjustmentPanel.Width, -1);
                        this.GridComboSelectionPanel.Width = this.GridComboSelectionPanel.Width - this.MID3.Width;
                        // this.GridPanel.Width = this.GridPanel.Width - this.MID3Panel.Width;
                    }
                }
                this.MID4.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MID4Column.ColumnName;
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label4"].ToString()))
                {
                    if (this.MID4.Visible)
                    {
                        this.MID4.Visible = false;
                        this.GridComboSelectionPanel.Controls.Remove(this.MID4Panel);
                        this.MID5Panel.Location = new System.Drawing.Point(this.MID4Panel.Location.X, -1);
                        this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X + this.MID5Panel.Width, -1);
                        this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width, -1);
                        this.NewConstructionPanel.Location = new System.Drawing.Point(1852, -1);
                        this.GridComboSelectionPanel.Width = this.GridComboSelectionPanel.Width - this.MID4.Width;
                        //this.GridPanel.Width = this.GridPanel.Width - this.MID4Panel.Width;
                    }
                }
                this.MID5.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.MID5Column.ColumnName;
                if (string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Label5"].ToString()))
                {
                    if (this.MID5.Visible)
                    {
                        this.MID5.Visible = false;
                        this.GridComboSelectionPanel.Controls.Remove(this.MID5Panel);
                        this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X, -1);
                        this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width - 1, -1);
                        this.NewConstructionPanel.Location = new System.Drawing.Point(this.AdjustmentPanel.Location.X + this.AdjustmentPanel.Width - 1, -1);
                        this.GridComboSelectionPanel.Width = this.GridComboSelectionPanel.Width - this.MID5Panel.Width;
                        // this.GridPanel.Width = this.GridPanel.Width - this.MID5.Width;
                    }
                }
                else
                {
                    this.AdjustmentTypePanel.Location = new System.Drawing.Point(this.MID5Panel.Location.X + this.MID5Panel.Width - 2, -1);
                    this.AdjustmentPanel.Location = new System.Drawing.Point(this.AdjustmentTypePanel.Location.X + this.AdjustmentTypePanel.Width - 1, -1);
                    this.NewConstructionPanel.Location = new System.Drawing.Point(this.AdjustmentPanel.Location.X + this.AdjustmentPanel.Width - 1, -1);

                }
                this.Label1.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Label1Column.ColumnName;
                this.Label2.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Label2Column.ColumnName;
                this.Label3.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Label3Column.ColumnName;
                this.Label4.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Label4Column.ColumnName;
                this.Label5.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Label5Column.ColumnName;
                this.AdjustmentTypeId.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AdjustmentTypeIDColumn.ColumnName;
                this.AdjustmentType.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AdjustmentTypeColumn.ColumnName;
                this.Adjustment.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AdjustmentColumn.ColumnName;
                this.NewConstruction.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.NewConstructionColumn.ColumnName;
                this.IsValue.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.IsValueColumn.ColumnName;
                this.IsValueID.DataPropertyName = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.IsValueIDColumn.ColumnName;

                this.MakeSubId.DisplayIndex = 1;
                this.Row.DisplayIndex = 2;
                this.Parcel.DisplayIndex = 3;
                this.AddNum.DisplayIndex = 4;
                //this.StreetID.DisplayIndex = 5;
                this.Street.DisplayIndex = 5;
                this.Block.DisplayIndex = 6;
                this.Lot.DisplayIndex = 7;
                this.Shape.DisplayIndex = 8;
                this.Shape.DisplayIndex = 9;
                this.Width.DisplayIndex = 10;
                this.Depth.DisplayIndex = 11;
                this.Acres.DisplayIndex = 12;
                //this.Dor2.DisplayIndex = 14;
                this.MID1.DisplayIndex = 13;
                this.MID2.DisplayIndex = 14;
                this.MID3.DisplayIndex = 15;
                this.MID4.DisplayIndex = 16;
                this.MID5.DisplayIndex = 17;
                //this.Label1.DisplayIndex = 20;
                //this.Label2.DisplayIndex = 21;
                //this.Label3.DisplayIndex = 22;
                //this.Label4.DisplayIndex = 23;
                //this.Label5.DisplayIndex = 24;
                //this.AdjustmentTypeId.DisplayIndex = 25;
                this.AdjustmentType.DisplayIndex = 18;
                this.Adjustment.DisplayIndex = 19;
                this.NewConstruction.DisplayIndex = 20;

                this.Parcel.MaxInputLength = 50;
                this.AddNum.MaxInputLength = 15;
                this.Block.MaxInputLength = 10;
                this.Lot.MaxInputLength = 10;
                this.Width.MaxInputLength = 15;
                this.Depth.MaxInputLength = 15;
                this.Acres.MaxInputLength = 15;
                this.MID1.MaxInputLength = 50;
                this.MID2.MaxInputLength = 50;
                this.MID3.MaxInputLength = 50;
                this.MID4.MaxInputLength = 50;
                this.MID5.MaxInputLength = 50;
                //Added for Changing Current row header Fore colour to white by purushotham
                this.SubdivisionGridView.RowHeadersDefaultCellStyle.ForeColor = Color.White;
                ////Coding added for the CO : 3843 by malliga on 25/9/2009
                this.Acres.HeaderText = "# of Units";
                this.Street.Resizable = DataGridViewTriState.False;
                this.Shape.Resizable = DataGridViewTriState.False;
                this.AdjustmentType.Resizable = DataGridViewTriState.False;
                
                this.Adjustment.MaxInputLength = 50;
                this.NewConstruction.MaxInputLength = 23;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        /*  /// <summary>
          /// Initializes the base adjustment type combo box.
          /// </summary>
          private void InitializeBaseAdjustmentTypeComboBox()
          {
              // Initialize the LandType1 ComboBox
              this.listBaseAdjustmentTypesComboDataTable.Clear();

              // Adding baseAdujstment Types in FormLevel
              DataRow baseAdjustmentTypeRow1;
              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();

              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 0;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "None";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 1;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Alternate Land Code";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 2;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Factor";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 3;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Unit Value";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 4;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Production";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 5;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Additive";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              ////Added by Biju on 01-Dec-2010 to implement #9328
              baseAdjustmentTypeRow1 = this.listBaseAdjustmentTypesComboDataTable.NewRow();
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName] = 6;
              baseAdjustmentTypeRow1[this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName] = "Total Value";
              this.listBaseAdjustmentTypesComboDataTable.Rows.Add(baseAdjustmentTypeRow1);

              this.BaseAdjusmentTypeComboBox.DataSource = this.listBaseAdjustmentTypesComboDataTable;
              this.BaseAdjusmentTypeComboBox.DisplayMember = this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeDescriptionColumn.ColumnName;
              this.BaseAdjusmentTypeComboBox.ValueMember = this.listBaseAdjustmentTypesComboDataTable.AdjustmentTypeIDColumn.ColumnName;
          }*/

        /// <summary>
        /// Fills grid combos.
        /// </summary>
        private void FillGridCombos()
        {
            try
            {
                //// To Bind Parcel Combo
                DataTable parcelDt = new DataTable();
                DataRow parcelDr;
                parcelDt.Columns.Add(" ");

                parcelDr = parcelDt.NewRow();
                parcelDr[0] = " ";
                parcelDt.Rows.Add(parcelDr);



                parcelDr = parcelDt.NewRow();
                parcelDr[0] = "=";
                parcelDt.Rows.Add(parcelDr);

                this.AdjustmentComboBox.DataSource = parcelDt.Copy();
                this.AdjustmentComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.AdjustmentComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.AdjustmentComboBox.SelectedIndex = 0;

                this.AdjustmentTypeComboBox.DataSource = parcelDt.Copy();
                this.AdjustmentTypeComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.AdjustmentTypeComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.AdjustmentTypeComboBox.SelectedIndex = 0;

                this.NewConstructionComboBox.DataSource = parcelDt.Copy();
                this.NewConstructionComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.NewConstructionComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.NewConstructionComboBox.SelectedIndex = 0;

                this.IsValueComboBox.DataSource = parcelDt.Copy();
                this.IsValueComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.IsValueComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.IsValueComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Fills all grid combos.
        /// </summary>
        private void FillAllGridCombos()
        {
            try
            {
                //// To Bind Parcel Combo
                DataTable parcelDt = new DataTable();
                DataRow parcelDr;
                parcelDt.Columns.Add(" ");

                parcelDr = parcelDt.NewRow();
                parcelDr[0] = " ";
                parcelDt.Rows.Add(parcelDr);

                parcelDr = parcelDt.NewRow();
                parcelDr[0] = "+1";
                parcelDt.Rows.Add(parcelDr);

                parcelDr = parcelDt.NewRow();
                parcelDr[0] = "=";
                parcelDt.Rows.Add(parcelDr);

                this.ParcelCombo.DataSource = parcelDt.Copy();
                this.ParcelCombo.ValueMember = parcelDt.Columns[0].ColumnName;
                this.ParcelCombo.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.ParcelCombo.SelectedIndex = 0;

                ////To Bind AddNum Combo
                this.AddNumComboBox.DataSource = parcelDt.Copy();
                this.AddNumComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.AddNumComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.AddNumComboBox.SelectedIndex = 0;

                ////To Bind Street Combo
                this.StreetComboBox.DataSource = parcelDt.Copy();
                this.StreetComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.StreetComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.StreetComboBox.SelectedIndex = 0;

                ////To Bind Block Combo
                this.BlockComboBox.DataSource = parcelDt.Copy();
                this.BlockComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.BlockComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.BlockComboBox.SelectedIndex = 0;

                ////To Bind Lot Combo
                this.LotComboBox.DataSource = parcelDt.Copy();
                this.LotComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.LotComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.LotComboBox.SelectedIndex = 0;

                ////new field Shape
                ////To Bind Shape Combo
                this.ShapeComboBox.DataSource = parcelDt.Copy();
                this.ShapeComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.ShapeComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.ShapeComboBox.SelectedIndex = 0;

                ////To Bind Width Combo
                this.WidthComboBox.DataSource = parcelDt.Copy();
                this.WidthComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.WidthComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.WidthComboBox.SelectedIndex = 0;

                ////To Bind Depth Combo
                this.DepthComboBox.DataSource = parcelDt.Copy();
                this.DepthComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.DepthComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.DepthComboBox.SelectedIndex = 0;

                ////To Bind Acres Combo
                this.AcresComboBox.DataSource = parcelDt.Copy();
                this.AcresComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.AcresComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.AcresComboBox.SelectedIndex = 0;

                ////To Bind MID1 Combo
                this.MID1ComboBox.DataSource = parcelDt.Copy();
                this.MID1ComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.MID1ComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.MID1ComboBox.SelectedIndex = 0;

                //// To Bind MID2 Combo
                this.MID2ComboBox.DataSource = parcelDt.Copy();
                this.MID2ComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.MID2ComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.MID2ComboBox.SelectedIndex = 0;

                //// To Bind MID3 Combo
                this.MID3ComboBox.DataSource = parcelDt.Copy();
                this.MID3ComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.MID3ComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.MID3ComboBox.SelectedIndex = 0;

                //// To Bind MID4 Combo
                this.MID4ComboBox.DataSource = parcelDt.Copy();
                this.MID4ComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.MID4ComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.MID4ComboBox.SelectedIndex = 0;

                //// To Bind MID5 Combo
                this.MID5ComboBox.DataSource = parcelDt.Copy();
                this.MID5ComboBox.ValueMember = parcelDt.Columns[0].ColumnName;
                this.MID5ComboBox.DisplayMember = parcelDt.Columns[0].ColumnName;
                this.MID5ComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                ////Added by Biju to fix #3922
                ProcessButton.Enabled = false;
            }
        }

        /// <summary>
        /// Checks the max parcels.
        /// </summary>
        /// <param name="setParcels">The set parcels.</param>
        /// <returns>Return boolValue</returns>
        private bool CheckMaxParcels(int setParcels)
        {
            if (setParcels <= 1)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505CheckMaxParcels"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (setParcels > 255)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F29505CheckMaxParcels"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clears the control.
        /// </summary>
        private void ClearControl()
        {
            try
            {
                this.DefaultOwnerTextBox.Text = string.Empty;
                this.DORCodeComboBox.SelectedIndex = -1;
                this.DistrictComboBox.SelectedIndex = -1;
                this.NeighborhoodComboBox.SelectedIndex = -1;
                this.SubdivisionComboBox.SelectedIndex = -1;
                this.LandType1ComboBox.SelectedIndex = -1;
                this.LandType2ComboBox.SelectedIndex = -1;
                this.LandType3ComboBox.SelectedIndex = -1;
                this.LandCodeTextBox.Text = string.Empty;
                if (!this.NoOfParcelTextBox.Text.Equals("0"))
                {
                    this.NoOfParcelTextBox.Text = string.Empty;
                }
                this.ParcelCombo.SelectedIndex = -1;
                this.AddNumComboBox.SelectedIndex = -1;
                this.StreetComboBox.SelectedIndex = -1;
                this.BlockComboBox.SelectedIndex = -1;
                this.LotComboBox.SelectedIndex = -1;
                this.ShapeComboBox.SelectedIndex = -1;
                this.WidthComboBox.SelectedIndex = -1;
                this.DepthComboBox.SelectedIndex = -1;
                this.AcresComboBox.SelectedIndex = -1;
                ////this.Dor2ComboBox.SelectedIndex = -1;
                this.MID1ComboBox.SelectedIndex = -1;
                this.MID2ComboBox.SelectedIndex = -1;
                this.MID3ComboBox.SelectedIndex = -1;
                this.MID4ComboBox.SelectedIndex = -1;
                this.MID5ComboBox.SelectedIndex = -1;
                this.AdjustmentComboBox.SelectedIndex = -1;
                this.AdjustmentTypeComboBox.SelectedIndex = -1;
                this.NewConstructionComboBox.SelectedIndex = -1;
                this.IsValueComboBox.SelectedIndex = -1;
                this.noofparcelflag = false;
                this.ClassCodeComboBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the height.
        /// </summary>
        /// <param name="recordCount">The recordcount.</param>
        private void SetHeight(int recordCount)
        {
            try
            {
                int increment = recordCount * 22;
                this.SubdivisionGridView.Height = 24 + increment + 16;
                this.GridPanel.Height = 24 + increment + 17;

                this.CreateSubdivisionMainPanel.Height = this.GridComboSelectionPanel.Height + this.GridPanel.Height - 2;
                this.hScrollBar1.Visible = false;
                //this.hScrollBar1.Location = new System.Drawing.Point(this.CreateSubdivisionMainPanel.Location.X + 200, this.CreateSubdivisionMainPanel.Height + 5);
                //this.hScrollBar1.BringToFront();
                this.MainPanel.Height = this.HeaderPanel.Height + this.GridComboSelectionPanel.Height + this.GridPanel.Height + 17;
                this.CreateSubDivisionPictureBox.Height = this.HeaderPanel.Height + this.GridComboSelectionPanel.Height + this.GridPanel.Height;
                //this.Height = this.CreateSubDivisionPictureBox.Height + 7;
                this.Height = this.CreateSubDivisionPictureBox.Height;
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.CreateSubDivisionPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CreateSubDivisionPictureBox.Height, this.CreateSubDivisionPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Gets the land code.
        /// </summary>
        private void GetLandCode()
        {
            try
            {
                int landType1;
                int landType2;
                int landType3;
                if (this.LandType1ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.LandType1ComboBox.SelectedValue.ToString(), out landType1);
                }
                else
                {
                    landType1 = 0;
                }

                if (this.LandType2ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.LandType2ComboBox.SelectedValue.ToString(), out landType2);
                }
                else
                {
                    landType2 = 0;
                }

                if (this.LandType3ComboBox.SelectedValue != null)
                {
                    int.TryParse(this.LandType3ComboBox.SelectedValue.ToString(), out landType3);
                }
                else
                {
                    landType3 = 0;
                }

                ////Coding modified for the issue 5668 by Maliga on 27/3/2009
                ////Earlier it referred 36033 pc_get_landcode sp now it referring new sp f29505_pcget_LandCode
                this.formLandCodeData = this.form29505Control.WorkItem.F29505_GetLandCode(landType1, landType2, landType3, Convert.ToInt32(this.NeighborhoodComboBox.SelectedValue), Convert.ToInt32(this.RollYearTextBox.Text));
                if (this.formLandCodeData.Get_LandCodeAllValue.Rows.Count > 0)
                {
                    this.LandCodeTextBox.Text = this.formLandCodeData.Get_LandCodeAllValue.Rows[0][formLandCodeData.Get_LandCodeAllValue.LandCodeColumn].ToString();
                }
                else
                {
                    this.LandCodeTextBox.Text = string.Empty;
                }
                ////Coding Ends here
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Enables the disable controls.
        /// </summary>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        private void EnableDisableControls(bool enabled)
        {
            if (!enabled)
            {
                this.ProcessButton.Text = "Processed";
                this.ProcessButton.BackColor = Color.FromArgb(119, 47, 40);
                this.ProcessLabel.Visible = true;
                this.ProcessLabel.BringToFront();
            }
            else
            {
                this.ProcessButton.Text = "Process";
                this.ProcessButton.BackColor = Color.FromArgb(28, 81, 128);
                this.ProcessLabel.Visible = !enabled;
                this.ProcessLabel.SendToBack();
            }

            this.RollYearpanel.Enabled = enabled;
            this.DeafaultOwnerPanel.Enabled = enabled;
            this.DORCodePanel.Enabled = enabled;
            this.DistrictPanel.Enabled = enabled;
            if (this.stateConfigured.ToLower().ToString().Equals("ne"))
            {
                this.ClassCodePanel.Enabled = enabled;
            }
            this.NeighborhoodPanel.Enabled = enabled;
            this.SubdivisionPanel.Enabled = enabled;
            this.LandType1Panel.Enabled = enabled;
            this.LandType2panel.Enabled = enabled;
            this.LantType3panel.Enabled = enabled;
            this.LandCodePanel.Enabled = enabled;
            this.NoOfParcelPanel.Enabled = enabled;
            this.FillGridButton.Enabled = enabled;

            if (this.makeSubIdValue > 0)
            {
                this.ProcessButton.Enabled = true;
            }
            else
            {
                this.ProcessButton.Enabled = false;
            }

            this.GridComboSelectionPanel.Enabled = enabled;
            this.GridPanel.Enabled = true;
            this.editallow = enabled;
            this.SubdivisionGridView.Enabled = true;
        }

        /// <summary>
        /// Fills the grid plus one fn.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="columnName">Name of the column.</param>
        private void FillGridPlusOneFn(int i, string columnName)
        {
            ////Coding Added for the Issue 5850
            long parcelGridValue;
            string fillGridValue = string.Empty;
            string fillSubGridValue = string.Empty;
            string gridValue = string.Empty;
            string subGridValue = string.Empty;
            bool isintCheckflag;
            int leng = 0;
            if (i.Equals(1))
            {
                fillGridValue = this.SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString();
                fillSubGridValue = this.SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString();
            }
            else
            {
                fillGridValue = this.SubdivisionGridView.Rows[i - 1].Cells[columnName].Value.ToString();
                fillSubGridValue = this.SubdivisionGridView.Rows[i - 1].Cells[columnName].Value.ToString();
            }

            leng = fillGridValue.Length;
            if (!string.IsNullOrEmpty(fillGridValue))
            {
                isintCheckflag = IsInteger(fillGridValue);
                if (isintCheckflag)
                {
                    string addedvalue = Convert.ToString((Convert.ToDecimal(fillGridValue) + 1));
                    addedvalue = addedvalue.PadLeft(leng, '0');
                    int frontLength = addedvalue.Length;
                    if (frontLength <= 50)
                    {
                        this.SubdivisionGridView.Rows[i].Cells[columnName].Value = addedvalue;
                        this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else
                    {
                        this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                    }
                }
                else
                {
                    int intLengthCount = 0;
                    int notIntLengthCount = 0;
                    for (int charLen = fillGridValue.Length; charLen >= 1; charLen--)
                    {
                        string arrChar = Convert.ToString(fillGridValue[charLen - 1]);
                        bool charCheck = IsInteger(arrChar);
                        if (charCheck)
                        {
                            if (notIntLengthCount <= 0)
                            {
                                intLengthCount = intLengthCount + 1;
                            }
                        }
                        else
                        {
                            notIntLengthCount = notIntLengthCount + 1;
                        }
                    }

                    gridValue = fillGridValue.Substring(leng - intLengthCount);
                    subGridValue = fillSubGridValue.Substring(0, (leng - intLengthCount));
                    if (!string.IsNullOrEmpty(gridValue))
                    {
                        parcelGridValue = Convert.ToInt64(gridValue) + 1;
                        string parcelValue = parcelGridValue.ToString();
                        parcelValue = parcelValue.PadLeft(intLengthCount, '0');

                        int frontLength = subGridValue.Length;
                        int backLength = parcelValue.Length;
                        frontLength = frontLength + backLength;
                        if (frontLength <= 50)
                        {
                            this.SubdivisionGridView.Rows[i].Cells[columnName].Value = subGridValue + parcelValue;
                            this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        }
                        else
                        {
                            this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                        }
                    }
                    else
                    {
                        this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                    }
                }

                this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
            }
            else
            {
                this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
            }
        }

        /// <summary>
        /// Fills the grid plus one.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="columnName">Name of the column.</param>
        private void FillGridPlusOne(int i, string columnName)
        {
            int parcelGridValue;
            string fillGridValue = string.Empty;
            string fillSubGridValue = string.Empty;
            string gridValue = string.Empty;
            string subGridValue = string.Empty;
            bool isintCheck;
            bool isintCheckflag;
            int leng = 0;
            if (i.Equals(1))
            {
                fillGridValue = this.SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString();
                fillSubGridValue = this.SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString();
            }
            else
            {
                fillGridValue = this.SubdivisionGridView.Rows[i - 1].Cells[columnName].Value.ToString();
                fillSubGridValue = this.SubdivisionGridView.Rows[i - 1].Cells[columnName].Value.ToString();
            }

            leng = fillGridValue.Length;
            if (leng >= 3)
            {
                if (!string.IsNullOrEmpty(fillGridValue))
                {
                    gridValue = fillGridValue.Substring(leng - 3);
                    subGridValue = fillSubGridValue.Substring(0, (leng - 3));
                    isintCheck = IsInteger(gridValue);
                    isintCheckflag = IsInteger(fillGridValue);
                    if (isintCheckflag)
                    {
                        long parcelValue = Convert.ToInt64(fillGridValue) + 1;
                        this.SubdivisionGridView.Rows[i].Cells[columnName].Value = parcelValue;
                    }
                    else
                    {
                        if (isintCheck)
                        {
                            parcelGridValue = Convert.ToInt32(gridValue) + 1;
                            string padding = parcelGridValue.ToString();
                            leng = padding.Length;
                            if (leng <= 3)
                            {
                                padding = padding.PadLeft(3, '0');
                                int frontLength = subGridValue.Length;
                                int backLength = padding.Length;
                                frontLength = frontLength + backLength;
                                if (frontLength <= 50)
                                {
                                    this.SubdivisionGridView.Rows[i].Cells[columnName].Value = subGridValue + padding;
                                }
                                else
                                {
                                    this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                                }

                                this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                            }
                            else
                            {
                                int intLengthCount = 0;
                                int notIntLengthCount = 0;
                                for (int charLen = fillGridValue.Length; charLen >= 1; charLen--)
                                {
                                    string arrChar = Convert.ToString(fillGridValue[charLen - 1]);
                                    bool charCheck = IsInteger(arrChar);
                                    if (charCheck)
                                    {
                                        if (notIntLengthCount <= 0)
                                        {
                                            intLengthCount = intLengthCount + 1;
                                        }
                                    }
                                    else
                                    {
                                        notIntLengthCount = notIntLengthCount + 1;
                                    }
                                }

                                leng = fillGridValue.Length;
                                gridValue = fillGridValue.Substring(leng - intLengthCount);
                                subGridValue = fillSubGridValue.Substring(0, (leng - intLengthCount));
                                parcelGridValue = Convert.ToInt32(gridValue) + 1;
                                string parcelValue = parcelGridValue.ToString();
                                int frontLength = subGridValue.Length;
                                int backLength = parcelValue.Length;
                                frontLength = frontLength + backLength;
                                if (frontLength <= 50)
                                {
                                    this.SubdivisionGridView.Rows[i].Cells[columnName].Value = subGridValue + parcelValue;
                                }
                                else
                                {
                                    this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                                }
                            }

                            this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                        }
                        else
                        {
                            this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                            this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                        }
                    }
                }
                else
                {
                    this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                    this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                }
            }
            else
            {
                this.SubdivisionGridView.Rows[i].Cells[columnName].Value = string.Empty;
                this.SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
            }
        }

        #endregion

        #region DefaultOwner PictureBox

        /// <summary>
        /// Handles the Click event of the DefaultOwnerPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DefaultOwnerPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                Form ownerIdForm = new Form();
                ownerIdForm = TerraScanCommon.GetForm(9101, null, this.form29505Control.WorkItem);
                if (ownerIdForm != null)
                {
                    if (ownerIdForm.ShowDialog() == DialogResult.Yes)
                    {
                        this.ownerId = Convert.ToInt32(TerraScanCommon.GetValue(ownerIdForm, "MasterNameOwnerId"));
                        this.SetEditRecord();
                        this.ownerDetailDataSet = this.form29505Control.WorkItem.GetOwnerDetails(this.ownerId);
                        this.DefaultOwnerTextBox.Text = this.ownerDetailDataSet.ListPartiesOwnerDetail.Rows[0][this.ownerDetailDataSet.ListPartiesOwnerDetail.NameColumn].ToString();
                        this.ProcessButton.Enabled = false;
                    }
                }
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

        #region Button Events

        /// <summary>
        /// Handles the Click event of the FillGridButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FillGridButton_Click(object sender, EventArgs e)
        {
            try
            {

                // Checks the row is greater than zero.
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count > 0)
                {
                    // Check the parcel text box is not empty.
                    if (string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
                    {
                        // If its empty the show the message Box.
                        MessageBox.Show(SharedFunctions.GetResourceString("F29505CheckNoOfParcels"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else if (!string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
                    {
                        // Get the Maximum Parcel
                        int noofParcel = 0;

                        int.TryParse(this.NoOfParcelTextBox.Text.Trim(), out noofParcel);

                        if (this.CheckMaxParcels(noofParcel))
                        {
                            ///MODIFIED FOR NEW FIELD.
                            if ((!string.IsNullOrEmpty(this.ParcelCombo.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.AddNumComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.StreetComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.BlockComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.LotComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.DepthComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.WidthComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.AcresComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.MID1ComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.MID2ComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.MID3ComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.MID4ComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.MID5ComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.ShapeComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.AdjustmentComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.AdjustmentTypeComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.NewConstructionComboBox.Text.Trim()))
                                || (!string.IsNullOrEmpty(this.IsValueComboBox.Text.Trim()))
                                || this.noofparcelflag)
                            {
                                // Sets the cursor to busy. 
                                this.Cursor = Cursors.WaitCursor;

                                // Call the method to create the record.
                                this.CreateParcelRecord();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                // Sets the cursor to busy. 
                this.Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// Handles the Click event of the ProcessButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission)
                {

                    if (this.CheckRequiredField())
                    {
                        this.Cursor = Cursors.WaitCursor;
                        string returnMessage = this.form29505Control.WorkItem.F29505_CreateParcel(this.makeSubIdValue, TerraScanCommon.UserId);
                        this.Cursor = Cursors.Default;
                        if (returnMessage != null && !string.IsNullOrEmpty(returnMessage.Trim()))
                        {
                            MessageBox.Show(returnMessage, SharedFunctions.GetResourceString("F29505SubDivisiontHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F29505ProcessSuccess"), SharedFunctions.GetResourceString("CreateSubDivisiontHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // this.editallow = false;
                        //this.form29505Control.WorkItem.F29505_CreateParcel(this.makeSubIdValue, TerraScanCommon.UserId);
                        //this.Cursor = Cursors.Default;
                        //MessageBox.Show(SharedFunctions.GetResourceString("F29505ProcessSuccess"), SharedFunctions.GetResourceString("CreateSubDivisiontHeader"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ////this.subDivisionDataSet = this.form29505Control.WorkItem.F29505_GetBaseParcelValue(this.eventId);
                        ////To Bind CreateSubdivison grid Combo Selection
                        this.FillAllGridCombos();
                        this.FillGridCombos();
                        ////To Customise Grid Coulmns 
                        this.CustomiseGrid();
                        this.FillAllDetails();                        
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        #region TextChanged Events
        /// <summary>
        /// Handles the SelectionChangeCommitted event of the DORCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
                if (this.SubdivisionComboBox.SelectedValue != null)
                {
                    this.tempSubDivision = (int)this.SubdivisionComboBox.SelectedValue;
                }

                if (this.DORCodeComboBox.SelectedValue != null)
                {
                    this.tempDOR = this.DORCodeComboBox.SelectedValue.ToString();
                }

                if (this.DistrictComboBox.SelectedValue != null)
                {
                    this.tempDistrict = (int)this.DistrictComboBox.SelectedValue;
                }

                if (this.NeighborhoodComboBox.SelectedValue != null)
                {
                    this.tempNeighborhood = (int)this.NeighborhoodComboBox.SelectedValue;
                    int rollyear;
                    int.TryParse(this.RollYearTextBox.Text, out rollyear);
                    this.LandCodeDataSet = this.form29505Control.WorkItem.LandCodes(this.tempNeighborhood, rollyear);
                    DataRow landCodeRow = this.LandCodeDataSet.f36035ListLandCodes.NewRow();
                    landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName] = "";
                    landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.RollYearColumn.ColumnName] = rollyear;
                    this.LandCodeDataSet.f36035ListLandCodes.Rows.InsertAt(landCodeRow, 0);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region LandType Events
        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LandTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LandTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.SetEditRecord();
            this.GetLandCode();
            this.ProcessButton.Enabled = false;
            if (this.LandType1ComboBox.SelectedValue != null)
            {
                this.tempLandType1 = (int)this.LandType1ComboBox.SelectedValue;
            }

            if (this.LandType2ComboBox.SelectedValue != null)
            {
                this.tempLandType2 = (int)this.LandType2ComboBox.SelectedValue;
            }

            if (this.LandType3ComboBox.SelectedValue != null)
            {
                this.tempLandType3 = (int)this.LandType3ComboBox.SelectedValue;
            }
        }
        #endregion

        #region Auto Scroll Evets
        /// <summary>
        /// Handles the Scroll event of the CreateSubdivisionMainPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void CreateSubdivisionMainPanel_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (!this.scrollmov)
                {
                    this.SubdivisionGridView.Focus();

                    this.scrollpos = e.NewValue;
                    this.SubdivisionGridView.HorizontalScrollingOffset = e.NewValue;
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (e.ScrollOrientation.ToString() != "VerticalScroll")
                {
                    this.CreateSubdivisionMainPanel.AutoScrollPosition = new Point(e.NewValue, 0);

                    int val = e.NewValue - e.OldValue;
                    if (val > 0)
                    {
                        this.GridComboSelectionPanel.SendToBack();
                        this.GridComboSelectionPanel.Location = new Point(this.GridComboSelectionPanel.Location.X - val, -1);
                    }
                    else
                    {
                        this.GridCombofirstPartPanel.BringToFront();
                        this.GridComboSelectionPanel.Location = new Point(this.GridComboSelectionPanel.Location.X + ((-1) * val), -1);
                    }

                    //if (e.NewValue>=200)
                    //{ 
                    //    this.AddNumPanel.Location = new Point(e.NewValue - 200, this.AddNumPanel.Location.Y);   
                    //    //this.GridComboSelectionPanel.Location = new Point(e.NewValue-200 , this.GridComboSelectionPanel.Location.Y);
                    //}
                }
                else
                {

                    // this.SubdivisionGridView.Location = new Point(-1, 2);  

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Picture Box Events
        /// <summary>
        /// Handles the Click event of the CreateSubDivisionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreateSubDivisionPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the CreateSubDivisionPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreateSubDivisionPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.CreateSubdivisionToolTip.SetToolTip(this.CreateSubDivisionPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Grid Events
        /// <summary>
        /// Handles the CellFormatting event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.RowIndex.Equals(0))
                {
                    e.CellStyle.BackColor = Color.FromArgb(202, 218, 169);
                }

                decimal outDecimal;

                ////For Acres Column
                if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.Acres.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Acres.Name].Value.ToString()))
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

                ////For width Column
                if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.Width.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Width.Name].Value.ToString()))
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


                ////if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.IsValue.Name].Index)
                ////{
                ////    for (int i = 0; i < subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count; i++)
                ////    {
                ////        if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString()))
                ////        {
                ////            if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("true"))
                ////            {
                ////                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = true;
                ////                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "Yes";
                ////            }
                ////            else
                ////            {
                ////                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = false;
                ////                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "No";
                ////            }

                ////        }

                ////    }
                ////}

                ////For depth Column
                if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.Depth.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    if (e.Value != null && !String.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Depth.Name].Value.ToString()))
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

                /*        if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.NewConstruction.Name].Index)
                        {
                            if (e.RowIndex < 0)
                            {
                                return;
                            }

                            if (e.Value != null && !String.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.NewConstruction.Name].Value.ToString()))
                            {
                                string val = e.Value.ToString();
                                if (Decimal.TryParse(val, out outDecimal))
                                {
                                    e.Value = outDecimal.ToString("#,##0.00");
                                    e.FormattingApplied = true;
                                }
                                else
                                {
                                    e.Value = 0;
                                }
                            }
                            else
                            {
                                e.Value = "";
                            }
                        }*/


                if (e.ColumnIndex == this.SubdivisionGridView.Columns["NewConstruction"].Index)
                {

                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //// Only paint if text provided, Only paint if desired text is in cell 
                    if (e.Value != null)
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                //e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            ////if (!this.ApplyInstrumentPayment)
                            ////{
                            e.Value = "0.00";
                            ////}
                            ////else
                            ////{
                            ////e.Value = this.ApplyInstrumentBalanceAmount;
                            ////this.instrumentPaymentsDataTable.Rows[e.RowIndex][e.ColumnIndex] = this.ApplyInstrumentBalanceAmount;
                            //////this.instrumentPaymentsDataTable.AcceptChanges();
                            //////this.instrumentPaymentsDataTable.AcceptChanges();
                            ////}
                        }
                    }
                    //else
                    //{
                    //    e.Value = "";
                    //}
                }

                ////For Adjustment column
                if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.Adjustment.Name].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }
                    //if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("1"))
                    //{

                    //    DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                    //    Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                    //    Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                    //    Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                    //    if (Landcode != null)
                    //    {
                    //        this.SubdivisionGridView[27, e.RowIndex] = Landcode; 
                    //    }

                    //}
                    if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("1"))
                    {
                        //  e.Value = "";
                    }
                    if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("2"))
                    {
                        //this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Adjustment.Name].ReadOnly = false;
                        if (e.Value != null)
                        {
                            string val = e.Value.ToString().Replace("%", "");
                            if (Decimal.TryParse(val, out outDecimal))
                            {

                                if (outDecimal.ToString().Contains("-"))
                                {
                                    //if (outDecimal < -100)
                                    //{

                                    outDecimal = 0;
                                    e.Value = "";
                                    e.FormattingApplied = true;
                                    //}
                                    //else
                                    //{
                                    //    decimal a = outDecimal / 100;
                                    //    e.Value = String.Concat("(", Decimal.Negate(a).ToString("0.00%"), ")");
                                    //    e.CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
                                    //}
                                }
                                else
                                {
                                    //if (outDecimal > 100 || outDecimal == 0)
                                    //{
                                    //    // this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex][e.ColumnIndex].ToString().Equals(0);
                                    //    e.Value = "0.00%";
                                    //}
                                    //else
                                    //{
                                    if (outDecimal <= 999999 && outDecimal > 0)
                                    {
                                        decimal a = outDecimal / 100;
                                        e.Value = a.ToString("0.00%");
                                        e.FormattingApplied = true;
                                    }
                                    else if (outDecimal == 0)
                                    {
                                        outDecimal = 0;

                                        e.Value = "0.00%";
                                    }
                                    else
                                    {
                                        e.Value = "";
                                    }
                                    //}

                                    //if (outDecimal == 100)
                                    //{
                                    //    e.Value = "100.00%";
                                    //}

                                    //e.FormattingApplied = true;
                                }

                            }
                            else
                            {
                                //this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex][e.ColumnIndex].Equals(0);
                                e.Value = "";
                            }
                        }
                        else
                        {
                            e.Value = "";
                        }

                    }
                    if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("3")
                        || this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("4")
                        || this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("5")
                        || this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("6"))
                    {
                        this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Adjustment.Name].ReadOnly = false;
                        if (e.Value != null)
                        {
                            string val = e.Value.ToString().Replace("%", "");
                            double outDecimal1;
                            if (double.TryParse(val, out outDecimal1))
                            {
                                if (outDecimal1 > 999999999.99)
                                {
                                    outDecimal1 = 0;
                                    //this.landInfluenceData.ListLandInfluenceTable.Rows[e.RowIndex]["Influence"].Equals(0);
                                    e.Value = "";
                                }
                                else
                                    if (outDecimal1 > 0)
                                    {

                                        e.Value = outDecimal1.ToString("#,##,##0.00");

                                    }
                                    else if (outDecimal1.ToString().Contains("-"))
                                    {
                                        if (outDecimal1 >= -999999999.99)
                                        {
                                            e.Value = String.Concat("(", (outDecimal1).ToString("#,##,##0.00"), ")");
                                        }
                                        else
                                        {
                                            e.Value = "";
                                        }
                                        //this.SubdivisionGridView.Rows[e.RowIndex].Cells[27].CellStyle.ForeColor = Color.FromArgb(128, 0, 0);
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
        /// Handles the RowEnter event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.rowIndex = e.RowIndex;
            // SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.i);
            //if (e.ColumnIndex.Equals(26))
            //{
            /*   if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("1"))
               {

                   DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                   Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                   Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                   Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                   if (Landcode != null)
                   {
                       this.SubdivisionGridView[27, e.RowIndex] = Landcode;
                   }

               }
               else if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString() != "1")
               {
                   DataGridViewTextBoxCell txt = new DataGridViewTextBoxCell();
                   txt.MaxInputLength = 50;
                   if(txt!=null)
                   {
                       this.SubdivisionGridView[27, e.RowIndex] = txt;   
                   }


               }*/




        }

        /// <summary>
        /// Handles the CellEndEdit event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
                {
                if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Acres"].Value.ToString()))
                {
                    string acresvalue = this.SubdivisionGridView.Rows[e.RowIndex].Cells["Acres"].Value.ToString();
                    int acreslength = acresvalue.Length;

                    if (acreslength > 9)
                    {
                        int pos = acresvalue.IndexOf(".");
                        if (pos.Equals(-1))
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells["Acres"].Value = 0.00;
                        }
                        else
                        {
                            if (pos > 9)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.SubdivisionGridView.Rows[e.RowIndex].Cells["Acres"].Value = 0.00;
                            }
                        }
                    }
                    //else if(acreslength <= 9)
                    //{
                    //        if (!string.IsNullOrEmpty(acresvalue))
                    //    {
                    //        int pos = acresvalue.IndexOf(".");
                    //        if (!pos.Equals(-1))
                    //        {
                    //            this.SubdivisionGridView.Rows[e.RowIndex].Cells["Acres"].Value = acresvalue.Remove(acreslength - 1); 
                    //        }
                    //    }
                    //}
                }
                if (e.ColumnIndex.Equals(26))
                {
                    if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString()))
                    {
                        if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.AdjustmentType.Name].Value.ToString().Equals("0"))
                        {
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Adjustment.Name].Value = string.Empty;
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Adjustment.Name].ReadOnly = true;

                        }
                        else
                        {
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells[this.Adjustment.Name].ReadOnly = false;
                        }
                    }
                }

                ////For Width
                if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Width"].Value.ToString()))
                {
                    string acresvalue = this.SubdivisionGridView.Rows[e.RowIndex].Cells["Width"].Value.ToString();
                    int acreslength = acresvalue.Length;

                    if (acreslength > 9)
                    {
                        int pos = acresvalue.IndexOf(".");
                        if (pos.Equals(-1))
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells["Width"].Value = 0.00;
                        }
                        else
                        {
                            if (pos > 9)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.SubdivisionGridView.Rows[e.RowIndex].Cells["Width"].Value = 0.00;
                            }
                        }
                    }
                }

                ////For Depth
                if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Depth"].Value.ToString()))
                {
                    string acresvalue = this.SubdivisionGridView.Rows[e.RowIndex].Cells["Depth"].Value.ToString();
                    int acreslength = acresvalue.Length;

                    if (acreslength > 9)
                    {
                        int pos = acresvalue.IndexOf(".");
                        if (pos.Equals(-1))
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.SubdivisionGridView.Rows[e.RowIndex].Cells["Depth"].Value = 0.00;
                        }
                        else
                        {
                            if (pos > 9)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.SubdivisionGridView.Rows[e.RowIndex].Cells["Depth"].Value = 0.00;
                            }
                        }
                    }
                }

                /////For AdjustmentValue
                if (e.ColumnIndex.Equals(27))
                {
                    if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString()))
                    {
                        string adjustmentValue = this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString();
                        int adjusLength = adjustmentValue.Length;

                        if (this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("2"))
                        {
                            if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString()))
                            {
                                string val = this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString().Replace("%", "");
                                decimal outDecimal;
                                decimal.TryParse(val, out outDecimal);
                                if (outDecimal <= 999999 && outDecimal > 0)
                                {
                                    decimal a = outDecimal / 100;
                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = a.ToString("0.00%");

                                }
                                else
                                {
                                    if (outDecimal > 999999)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentPercentageFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else if (outDecimal < 0)
                                    {
                                        MessageBox.Show("Adjustment value should not be less than 0", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = "0.00%";
                                }
                            }

                        }
                        else if (this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("3") || this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("4"))
                        {
                            if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString()))
                            {
                                double outTaxDecimal;
                                double.TryParse(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString(), out outTaxDecimal);
                                if (outTaxDecimal >= 0 && outTaxDecimal <= 999999999.99)
                                {

                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = outTaxDecimal.ToString("#,##,##0.00");

                                }
                                else
                                {
                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = "0.00";
                                    MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else if (this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("5") || this.SubdivisionGridView.Rows[e.RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("6"))
                        {
                            if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString()))
                            {
                                double outTaxDecimal;
                                double.TryParse(this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value.ToString(), out outTaxDecimal);
                                if (outTaxDecimal >= 0 && outTaxDecimal <= 999999999.99)
                                {

                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = outTaxDecimal.ToString("#,##,##0.00");

                                }
                                else if (outTaxDecimal < 0 && outTaxDecimal >= -999999999.99)
                                {
                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = String.Concat("(", (outTaxDecimal).ToString("#,##,##0.00"), ")");
                                }
                                else
                                {
                                    this.SubdivisionGridView.Rows[e.RowIndex].Cells["Adjustment"].Value = "0.00";
                                    MessageBox.Show(SharedFunctions.GetResourceString("AdjustmentNegativeDecimalFeildValidation"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                    }


                }


                if (e.ColumnIndex.Equals(29))
                {
                    if (e.ColumnIndex == this.SubdivisionGridView.Columns[this.IsValue.Name].Index)
                    {
                        //if(this.IsValueComboBox.SelectedItem
                        for (int i = 0; i < subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count; i++)
                        {

                            if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString()))
                            {

                                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().Equals("Yes"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = true;
                                }
                                else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().Equals("No"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = false;
                                }

                               else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("true"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = true;
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "Yes";
                                }
                               else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"].ToString().ToLower().Equals("false"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValueID"] = false;
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[i]["IsValue"] = "No";
                                }
                               
                            }

                        }
                    }
                }
                if (e.ColumnIndex.Equals(28))
                {

                    decimal outDecimal;
                    decimal.TryParse(this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value.ToString(), out outDecimal);
                    if (outDecimal >= 0 && outDecimal <= 922337203685477.58M)
                    {

                    }
                    else if (outDecimal < 0 && outDecimal >= -922337203685477.58M)
                    {

                    }
                    else
                    {
                        this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value = 0.00;
                    }

                    /* double outDecimal;
                double.TryParse(this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value.ToString(), out outDecimal);
                  if (outDecimal >= -922337203685477.5808 && outDecimal < 0)
                    {
                        string va;
                        va = string.Concat("(", (outDecimal).ToString("#,##,##0.00"), ")");
                        va = va.Replace("-", ""); 
                        this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value = va;
                    }*/

                    /*  if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value.ToString()))
                      {
                          double outDecimal;
                          double.TryParse(this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value.ToString(), out outDecimal);
                          if (outDecimal >= 0 && outDecimal <= 922337203685477.58)
                          {
                              this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value = outDecimal.ToString("#,##,##0.00");
                          }
                          else if (outDecimal >= -922337203685477.5808 && outDecimal < 0)
                          {

                              this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value = string.Concat("(", (outDecimal).ToString("#,##,##0.00"), ")");
                          }
                          else
                          {
                              this.SubdivisionGridView.Rows[e.RowIndex].Cells["NewConstruction"].Value = "0.00";
                              MessageBox.Show("New Construction value should be between -922337203685477.58 to 922337203685477.58", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                          }
                      }*/
                }
                SubdivisionGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                SubdivisionGridView.HorizontalScrollingOffset = SubdivisionGridView.HorizontalScrollingOffset;
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
            }

            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {

                    ((ComboBox)e.Control).TextUpdate += new EventHandler(F29505_GridStreetComboTextUpdate);
                    ((ComboBox)e.Control).Validating += new CancelEventHandler(this.GridStreetCombo_Validating);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F29505_SelectionChangeCommitted);
                    //((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.F29505_SelectionChangeCommitted);
                    //((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F29505_SelectionChangeCommitted);
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;


                }
                else
                {
                    e.Control.LostFocus += new EventHandler(Combobox_LostFocus);
                    e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                    e.Control.PreviewKeyDown += new PreviewKeyDownEventHandler(Control_PreviewKeyDown);
                }


                if (this.columnIndex != 5 && this.columnIndex != 26)
                {
                    e.Control.TextChanged += new EventHandler(Control_TextChanged);
                }
                e.Control.Validated += new EventHandler(Control_Validated);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the GridStreetComboTextUpdate event of the F29505 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F29505_GridStreetComboTextUpdate(object sender, EventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Validated event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {
                this.SubdivisionGridView.EditingControl.TextChanged -= new EventHandler(this.Control_TextChanged);
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
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            {
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(46))
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        this.SetEditRecord();
                        this.ProcessButton.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }



        /// <summary>
        /// Handles the Validating event of the GridStreetCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void GridStreetCombo_Validating(object sender, CancelEventArgs e)
        {
            ////if (this.streetComboValidated)
            ////    return;

            if ((sender as DataGridViewComboBoxEditingControl).SelectedValue == null)
            {

                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetName"].Equals(string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 5)
                {
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetName"] = string.Empty;
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetID"] = 0;
                    (sender as DataGridViewComboBoxEditingControl).SelectedIndex = -1;
                    ////Added by Biju to fix #3936

                    // this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = string.Empty;
                }
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentType"].Equals(string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 26)
                {
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentType"] = string.Empty;
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentType"] = 0;
                    (sender as DataGridViewComboBoxEditingControl).SelectedIndex = -1;
                    ////Added by Biju to fix #3936

                    // this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = string.Empty;
                }

                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["LandShape"].Equals(string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 9)
                {
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["LandShape"] = string.Empty;
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["ShapeID"] = 0;
                    (sender as DataGridViewComboBoxEditingControl).SelectedIndex = -1;
                    ////Added by Biju to fix #3936

                    //this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = string.Empty;

                }

                if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"].ToString()))
                {
                   // if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"].ToString().Equals("Yes"))
                   // {
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValueID"] = true;
                   // }
                   // else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"].ToString().Equals("No"))
                   // {
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValueID"] = false;
                   // }

                   // else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"].ToString().ToLower().Equals("true"))
                   // {
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValueID"] = true;
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"] = "Yes";
                   // }
                   //else if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"].ToString().ToLower().Equals("false"))
                   // {
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValueID"] = false;
                   //     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[rowIndex]["IsValue"] = "No";
                   // }

                }
            }
            else
            {
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetName"] != (string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 5)
                {
                    int combovalue = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    string comboboxText = (sender as DataGridViewComboBoxEditingControl).Text.ToString();
                    //if (!string.IsNullOrEmpty(comboboxText))
                    //{
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetName"] = comboboxText;
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetID"] = combovalue;
                    this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = combovalue;
                    //}
                    //e.Cancel = false;
                }
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentType"] != (string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 26)
                {
                    if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Alternate Land Code"))
                    {
                        this.SubdivisionGridView[26, this.rowIndex].Value = 1;


                        DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                        Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                        Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                        Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                        if (Landcode != null)
                        {
                            //this.SubdivisionGridView.Rows[0].(Landcode);  
                            this.SubdivisionGridView[27, this.rowIndex] = Landcode;
                            this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].Value = "";
                        }

                        //DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                        //Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes;
                        //Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                        //Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;

                        //if (this.LandCodeDataSet.f36035ListLandCodes.Rows.Count > 0)
                        //{
                        //    for(int i=0;i<this.LandCodeDataSet.f36035ListLandCodes.Rows.Count;i++)
                        //    {
                        //        Landcode.Items.Add(this.LandCodeDataSet.f36035ListLandCodes.Rows[i][0].ToString());     
                        //    }
                        //}
                        //this.SubdivisionGridView[this.rowIndex, 27] = (int)Landcode.DataSource;



                    }
                    else if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("None"))
                    {
                        this.SubdivisionGridView[26, this.rowIndex].Value = 0;
                        DataGridViewTextBoxCell TEXT = new DataGridViewTextBoxCell();
                        TEXT.MaxInputLength = 13;
                        if (TEXT != null)
                        {
                            //this.SubdivisionGridView.Rows[0].(Landcode);  
                            this.SubdivisionGridView[27, this.rowIndex] = TEXT;
                        }
                        //this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].Value = "";
                        this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].ReadOnly = true;
                    }
                    else
                    {
                        if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Factor"))
                        {
                            this.SubdivisionGridView[26, this.rowIndex].Value = 2;
                        }
                        if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Unit Value"))
                        {
                            this.SubdivisionGridView[26, this.rowIndex].Value = 3;
                        }
                        if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Production"))
                        {
                            this.SubdivisionGridView[26, this.rowIndex].Value = 4;
                        }
                        if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Additive"))
                        {
                            this.SubdivisionGridView[26, this.rowIndex].Value = 5;
                        }
                        if (((System.Windows.Forms.DataGridViewComboBoxEditingControl)(sender)).EditingControlFormattedValue.Equals("Total Value"))
                        {
                            this.SubdivisionGridView[26, this.rowIndex].Value = 6;
                        }
                        DataGridViewTextBoxCell TEXT = new DataGridViewTextBoxCell();
                        TEXT.MaxInputLength = 13;
                        if (TEXT != null)
                        {
                            // this.SubdivisionGridView.Rows[0].(Landcode);  
                            this.SubdivisionGridView[27, this.rowIndex] = TEXT;

                        }
                        this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].ReadOnly = false;

                    }
                    //int combovalue = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    //string comboboxText = (sender as DataGridViewComboBoxEditingControl).Text.ToString();
                    //this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentType"] = comboboxText;
                    //this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["AdjustmentTypeId"] = combovalue;
                    //this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = combovalue;
                }
                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["LandShape"] != (string.Empty) && this.SubdivisionGridView.CurrentCell.ColumnIndex == 9)
                {
                    int combovalue = (int)(sender as DataGridViewComboBoxEditingControl).SelectedValue;
                    string comboboxText = (sender as DataGridViewComboBoxEditingControl).Text.ToString();
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["LandShape"] = comboboxText;
                    //this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["StreetID"] = combovalue;
                    //this.SubdivisionGridView.Rows[this.rowIndex].Cells[5].Value = comboboxText;
                }

                
            }
            
        }


        /// <summary>
        /// Handles the KeyPress event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            // {
            //     if (this.SubdivisionGridView.Rows[this.SubdivisionGridView.SelectedCells[0].RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("1"))
            //     {
            //         DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
            //         Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
            //         Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
            //         Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

            //         if (Landcode != null)
            //         {
            //             //this.SubdivisionGridView.Rows[0].(Landcode);  
            //             this.SubdivisionGridView[27, this.SubdivisionGridView.SelectedCells[0].RowIndex] = Landcode;
            //         }
            //     }
            //     //e.Cancel = true;            
            // }
            // catch (Exception ex)
            // {
            //     ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            // }
            //try
            //{
            //    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
            //    {
            //        this.SetEditRecord();
            //        this.ProcessButton.Enabled = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            //}
        }

        /// <summary>
        /// Handles the KeyPress event of the Control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void Combobox_LostFocus(object sender, EventArgs e)
        {
            /* try
             {
                 if (this.SubdivisionGridView.Rows[this.SubdivisionGridView.SelectedCells[0].RowIndex].Cells["AdjustmentType"].Value.ToString().Equals("1"))
                 {
                     DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                     Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                     Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                     Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                     if (Landcode != null)
                     {
                         //this.SubdivisionGridView.Rows[0].(Landcode);  
                         this.SubdivisionGridView[27, this.SubdivisionGridView.SelectedCells[0].RowIndex] = Landcode;
                     }
                 }
                 if (this.SubdivisionGridView.Rows[this.SubdivisionGridView.SelectedCells[0].RowIndex].Cells["AdjustmentType"].Value.ToString()!="1")
                 {
                     DataGridViewTextBoxCell TEXT = new DataGridViewTextBoxCell();
                     TEXT.MaxInputLength = 50;
                     if (TEXT != null)
                     {
                         //this.SubdivisionGridView.Rows[0].(Landcode);  
                         this.SubdivisionGridView[27, this.SubdivisionGridView.SelectedCells[0].RowIndex] = TEXT;
                     }
                     this.SubdivisionGridView.Rows[this.SubdivisionGridView.SelectedCells[0].RowIndex].Cells["Adjustment"].ReadOnly = false;

                 }
                 //e.Cancel = true;            
             }
             catch (Exception ex)
             {
                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
             }*/
        }

        #endregion

        #region 29505 Events
        /// <summary>
        /// Handles the Resize event of the F29505 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29505_Resize(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.NoOfParcelTextBox.Text.Trim()))
                {
                    int noofParcel = Convert.ToInt32(this.NoOfParcelTextBox.Text.Trim());
                    this.SetHeight(noofParcel);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the F29505 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29505_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (this.columnIndex.Equals(26))
                {
                    // this.SubdivisionGridView[27, this.rowIndex].Value = ((System.Windows.Forms.ComboBox)(sender)).Text;   
                }
                //  ((ComboBox)e.Control).Validating += new CancelEventHandler(this.GridStreetCombo_Validating);
                /* if (((ComboBox)sender).Text == "Factor")
                 {
                     if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"].ToString()))
                     {
                           decimal outTaxDecimal = 0;
                         string val = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"].ToString();
                         if (Decimal.TryParse(val, out outTaxDecimal))
                         {
                             if (outTaxDecimal.ToString().Contains("-"))
                             {
                                 if (outTaxDecimal < -100)
                                 {
                                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = "0.00";
                                     //this.landInfluenceData.ListLandInfluenceTable.AcceptChanges();
                                     //e.Value = "0.00 ";
                                     //e.ParsingApplied = true;

                                 }

                             }
                             else
                             {
                                 if (outTaxDecimal > 100 || outTaxDecimal == 0)
                                 {
                                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = "0.00";
                                 }
                             }
                            
                            
                            

                         }
                     }
                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = (this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].Value.ToString());
                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();
                 }
                 else if(((ComboBox)sender).Text != "None")
                 {
                     if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"].ToString()))
                     {
                         decimal outTaxDecimal = 0;
                         string val = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"].ToString().Replace('%',' ') ;
                         if (Decimal.TryParse(val, out outTaxDecimal))
                         {


                             if (outTaxDecimal.ToString().Contains("-"))
                             {
                                 if (outTaxDecimal <= -1000000)
                                 {
                                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = "0.00";
                                   
                                 }

                             }
                             else
                             {

                                 if (outTaxDecimal > 1000000 || outTaxDecimal == 1000000)
                                 {

                                     this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = "0.00";
                                   


                                 }


                             }

                             this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[this.rowIndex]["Adjustment"] = (this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].Value.ToString());
                             this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AcceptChanges();


                         }
                     }
                 //}*/
                //DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                //Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                //Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                //Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                //if (Landcode != null)
                //{

                //    this.SubdivisionGridView[27, this.rowIndex] = Landcode;
                //}
                this.SetEditRecord();
                this.ProcessButton.Enabled = false;

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /////// <summary>
        /////// Handles the TextChanged event of the Control control.
        /////// </summary>
        /////// <param name="sender">The source of the event.</param>
        /////// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        ////private void Control_TextChanged(object sender, EventArgs e)
        ////{
        ////    try
        ////    {
        ////        if (this.pageMode == TerraScanCommon.PageModeTypes.View)
        ////        {
        ////            this.SetEditRecord();
        ////            this.ProcessButton.Enabled = false;
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
        ////    }
        ////}
        #endregion

        /// <summary>
        /// Handles the CellValidated event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            GetCustomString(SubdivisionGridView.Rows[0].Cells[e.ColumnIndex].OwningColumn.Name);
        }

        /// <summary>
        /// Gets the custom string.
        /// </summary>
        /// <param name="Name">The name.</param>
        private void GetCustomString(string Name)
        {
            switch (Name)
            {
                case "ParcelNumber":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.ParcelCombo.Text == "+1")
                        {
                            Name = "Parcel";
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.ParcelCombo.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        Name = "ParcelNumber";
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "HouseNumber":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.AddNumComboBox.Text == "+1")
                        {
                            Name = "AddNum";
                            this.parcelFormula = GetFormula(Name);
                            Name = "HouseNumber";
                        }
                        else if (AddNumComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "Block":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.BlockComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.BlockComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "Lot":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.LotComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.LotComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "StreetName":
                    {
                        // Checks selected value +1 or empty Or equal
                        Name = "Street";
                        if (this.StreetComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.LotComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        Name = "StreetName";
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "LotWidth":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.WidthComboBox.Text == "+1")
                        {
                            Name = "Width";
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.WidthComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "LotWidth";
                        break;
                    }
                case "LotDepth":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.DepthComboBox.Text == "+1")
                        {
                            Name = "Depth";
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.DepthComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "LotDepth";
                        break;
                    }
                case "Shape":
                    {
                        // Checks selected value +1 or empty Or equal
                        Name = "Shape";
                        if (this.ShapeComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "Shape";
                        break;
                    }
                case "Acres":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.AcresComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.AcresComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "MID1":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.MID1ComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.MID1ComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "MID2":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.MID2ComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.MID2ComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "MID3":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.MID3ComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.MID3ComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "MID4":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.MID4ComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.MID4ComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "MID5":
                    {
                        // Checks selected value +1 or empty Or equal
                        if (this.MID5ComboBox.Text == "+1")
                        {
                            this.parcelFormula = GetFormula(Name);
                        }
                        else if (this.MID5ComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }

                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);
                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        break;
                    }
                case "AdjustmentType":
                    {
                        // Checks selected value +1 or empty Or equal
                        Name = "AdjustmentType";
                        if (this.AdjustmentTypeComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "AdjustmentType";
                        break;
                    }
                case "Adjustment":
                    {
                        // Checks selected value +1 or empty Or equal
                        Name = "Adjustment";
                        if (this.AdjustmentTypeComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "Adjustment";
                        break;
                    }
                case "NewConstruction":
                    {
                        // Checks selected value +1 or empty Or equal
                        Name = "NewConstruction";
                        if (this.AdjustmentTypeComboBox.Text == "=")
                        {
                            this.parcelFormula = this.comboText;
                        }
                        else
                        {
                            this.parcelFormula = string.Empty;
                        }
                        // Add Alphanumeric string or not
                        this.alphaNumericDatas.Remove(Name);
                        this.alphaNumericDatas.Add(Name, this.alphaNumeric);

                        this.formulaDatas.Remove(Name);
                        this.formulaDatas.Add(Name, this.parcelFormula);
                        Name = "NewConstruction";
                        break;
                    }
            }
        }


        /// <summary>
        /// Gets the formula.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private string GetFormula(string columnName)
        {
            // Variable to store the formula.
            string constFormula = string.Empty;

            // Checks the grid rows count is greater than zero
            if (this.SubdivisionGridView.Rows.Count >= 0)
            {
                // Check entered string is a int
                if (IsInteger(SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString()))
                {
                    // Stores its not alpha numeric
                    this.alphaNumeric = false;

                    // Check columnName
                    if (columnName == "Street")
                    {
                        // Its combobox column so get the formatted value.
                        constFormula = SubdivisionGridView.Rows[0].Cells[columnName].FormattedValue.ToString();
                    }
                    else
                    {
                        // Get the cell value.
                        constFormula = SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString();
                    }

                }
                else
                {
                    // Call to get the int value from alphanumeric string
                    constFormula = this.SeparateInt(this.SubdivisionGridView.Rows[0].Cells[columnName].Value.ToString(), columnName);
                }
            }

            return constFormula;
        }

        /// <summary>
        /// Separates the int.
        /// </summary>
        /// <param name="alphanumericString">The alphanumeric string.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        private string SeparateInt(string alphanumericString, string columnName)
        {
            // Stores the start index.
            int stringIndex = 0;

            // Stores the final string.
            string finalString = string.Empty;

            // Re-Set the alphanumeric flag
            this.alphaNumeric = false;

            // Check string already added
            bool stringAdded = false;

            // Go thorugh each of the string.
            for (int charLen = alphanumericString.Length; charLen >= 1; charLen--)
            {
                // Get each letter from a string
                string arrChar = alphanumericString.Substring((charLen - 1), 1);

                // check its integer
                bool charCheck = IsInteger(arrChar);

                // If its integer
                if (charCheck)
                {
                    this.alphaNumeric = true;
                    finalString = arrChar + finalString;
                    stringIndex++;
                }
                else
                {
                    // Else construt the decimal value.
                    if (arrChar == "." && !stringAdded)
                    {
                        stringAdded = true;
                        finalString = arrChar + finalString;
                    }
                    this.valueString = alphanumericString.Substring(0, (alphanumericString.Length - (stringIndex)));
                    this.alphaNumericString.Remove(columnName);
                    this.alphaNumericString.Add(columnName, this.valueString);
                    break;
                }

            }
            return finalString;
        }

        /// <summary>
        /// Creates the record.
        /// </summary>
        private void CreateParcelRecord()
        {
            this.SetEditRecord();
            // Temp Hold the Datas.
            F29505CreateSubdivisionData tempHoldData = new F29505CreateSubdivisionData();

            // Stores the number of parcel to be created.
            int noOfParcel = 0;

            // Gets the no of parcel.
            Int32.TryParse(this.NoOfParcelTextBox.Text.Trim(), out noOfParcel);

            // Temp hold the datas.
            tempHoldData = (F29505CreateSubdivisionData)this.subDivisionDataSet.Copy();

            // get Default row.
            DataRow dr = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[0];

            // Clear The existing table.
            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Clear();

            // Create the defaultRow.
            DataRow defaultRow;

            // Create the Default row.
            defaultRow = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.NewRow();

            // Index to store to increament
            int indexNo = 0;

            // Adds the default Row.
            for (int defaultRowIndex = 0; defaultRowIndex <= tempHoldData.F29505_Get_SubdivisionGridDetails.Columns.Count - 1; defaultRowIndex++)
            {
                // Check for null value
                if (!string.IsNullOrEmpty(dr[defaultRowIndex].ToString()))
                {
                    if (dr.ItemArray.GetValue(9).ToString().Contains(",") || dr.ItemArray.GetValue(10).ToString().Contains(",") || dr.ItemArray.GetValue(11).ToString().Contains(","))
                    {
                        dr[defaultRowIndex] = dr[defaultRowIndex].ToString().Replace(",", "");
                    }

                    defaultRow[defaultRowIndex] = dr[defaultRowIndex].ToString();
                }
            }

            // Add the default row.
            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Add(defaultRow);

            // Go through each of the column 
            // Construct the formula.
            for (int defaultRowIndex = 0; defaultRowIndex <= tempHoldData.F29505_Get_SubdivisionGridDetails.Columns.Count - 1; defaultRowIndex++)
            {
                this.GetCustomString(tempHoldData.F29505_Get_SubdivisionGridDetails.Columns[defaultRowIndex].ColumnName);
            }

            // Create the rows one by one.
            for (int newRowIndex = 1; noOfParcel > newRowIndex; newRowIndex++)
            {
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.AddF29505_Get_SubdivisionGridDetailsRow(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.NewF29505_Get_SubdivisionGridDetailsRow());
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ItemID"] = 0;
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MakeSubID"] = this.makeSubIdValue;
                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Row"] = newRowIndex + 1;

                Int32.TryParse(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["Row"].ToString(), out indexNo);
                // Checks parcel combo's selected text.
                if (this.ParcelCombo.Text == "+1")
                {
                    ////Call Method to Add Parcel
                    this.AddParcel(newRowIndex);
                }
                else if (ParcelCombo.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["ParcelNumber"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;  
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                    }
                }

                //// AddNum 
                if (AddNumComboBox.Text == "+1")
                {
                    //// Call Method to add HouseNumber.
                    this.AddHouseNumber(newRowIndex);
                }
                else if (AddNumComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["HouseNumber"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                    }
                }

                //// Street
                if (this.StreetComboBox.Text == "=")
                {
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetName"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["StreetName"];
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetID"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["StreetID"];
                }
                else
                {
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetName"] = string.Empty;
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetID"] = 0;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetID"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetID"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetName"] = string.Empty;
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["StreetID"] = 0;
                    }
                }

                //////Shape
                if (this.ShapeComboBox.Text == "=")
                {
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LandShape"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["LandShape"];
                    //this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ShapeID"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["ShapeID"];
                }
                else
                {
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LandShape"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LandShape"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LandShape"] = string.Empty;
                    }
                }

                ////Block
                if (BlockComboBox.Text == "+1")
                {
                    //// Add the  Block
                    this.AddBlock(newRowIndex);
                }
                else if (BlockComboBox.Text == "=")
                {
                    //// Assign the previous row value..
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["Block"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                    }
                }

                ////Lot
                if (LotComboBox.Text == "+1")
                {   //// Add the Lot
                    this.AddLot(newRowIndex);
                }
                else if (LotComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["Lot"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                    }
                }

                ////Depth
                if (DepthComboBox.Text == "+1")
                {
                    //// Add the depth
                    this.AddDepth(newRowIndex);
                }
                else if (DepthComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["LotDepth"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                    }
                }

                //// WidthComboBox
                if (WidthComboBox.Text == "+1")
                {
                    //// Add the Width
                    this.AddWidth(newRowIndex);
                }
                else if (WidthComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["LotWidth"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                    }
                }

                //Acres
                if (AcresComboBox.Text == "+1")
                {
                    //// Add Acres
                    this.AddAcres(newRowIndex);
                }
                else if (AcresComboBox.Text == "=")
                {
                    // Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["Acres"];
                }
                else
                {
                    // Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = 0;
                    }
                }

                ////MID1
                if (MID1ComboBox.Text == "+1")
                {
                    //// Add MID1
                    this.AddMID1(newRowIndex);
                }
                else if (MID1ComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["MID1"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                    }
                }

                ////MID2ComboBox
                if (MID2ComboBox.Text == "+1")
                {
                    //// Add MID2
                    this.AddMID2(newRowIndex);
                }
                else if (MID2ComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["MID2"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                    }
                }

                ////MID3ComboBox
                if (MID3ComboBox.Text == "+1")
                {
                    //// Add MID3
                    this.AddMID3(newRowIndex);
                }
                else if (MID3ComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["MID3"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                    }
                }

                ////MID4ComboBox
                if (MID4ComboBox.Text == "+1")
                {
                    //// Add MID4
                    this.AddMID4(newRowIndex);
                }
                else if (MID4ComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["MID4"];
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                    }
                }

                ////MID5ComboBox
                if (MID5ComboBox.Text == "+1")
                {
                    this.AddMID5(newRowIndex);
                }
                else if (MID5ComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["MID5"];
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = this.formulaDatas["MID5"].ToString();
                }
                else
                {
                    //// Assign empty value.
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                    }
                }

                if (this.AdjustmentTypeComboBox.Text == "=")
                {
                    if (this.AdjustmentComboBox.Text != "=")
                    {
                        //for (int i = 0; i < this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows.Count; i++)
                        //{
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Adjustment"] = "";
                        //}
                    }
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentType"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["AdjustmentType"];
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentTypeID"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[0]["AdjustmentTypeID"];


                }
                else
                {
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentTypeID"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentTypeID"];
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentType"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentType"];
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentType"] = "0";
                        //  this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["AdjustmentTypeID"] = string.Empty;  
                    }
                }

                if (this.AdjustmentComboBox.Text == "=")
                {
                    if (this.AdjustmentTypeComboBox.Text == "=")
                    {
                        //// Assign the previous row value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Adjustment"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["Adjustment"];
                    }
                }
                else
                {
                    if (this.AdjustmentComboBox.Text != "=" && this.AdjustmentTypeComboBox.Text == "=")
                    {
                    }
                    else
                    {
                        if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Adjustment"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Adjustment"];
                        }
                    }
                    //else
                    //{
                    //    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Adjustment"] = string.Empty;
                    //}
                }

                if (this.NewConstructionComboBox.Text == "=")
                {
                    //// Assign the previous row value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["NewConstruction"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["NewConstruction"];
                }
                else
                {
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["NewConstruction"] = tempHoldData.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["NewConstruction"];
                    }
                    //else
                    //{
                    //    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["NewConstruction"] =string.Empty;
                    //}
                }


                if (this.IsValueComboBox.Text == "=")
                {
                    if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["IsValue"].ToString()))
                    {
                        if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["IsValue"].ToString().Equals("Yes"))
                        {
                            if (newRowIndex == 1)
                            {
                                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex-1]["IsValueID"] = true;
                            }
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = true;
                        }
                        else
                        {
                            if (newRowIndex == 1)
                            {
                                this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["IsValueID"] = false;
                            }
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = false;
                        }
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"] = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex - 1]["IsValue"];
                      
                    }
                }
                else
                {
                    if (newRowIndex < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count)
                    {
                        ////for (int i = 0; i < tempHoldData.F29505_Get_SubdivisionGridDetails.Rows.Count; i++)
                        ////{
                            if (!string.IsNullOrEmpty(this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"].ToString()))
                            {
                                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"].ToString().Equals("Yes"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = true;
                                }
                                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"].ToString().Equals("No"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = false;
                                }
                                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"].ToString().ToLower().Equals("true"))
                                {
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = true;
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"] = "Yes";
                                }
                                if (this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"].ToString().ToLower().Equals("false"))
                                {

                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValueID"] = false;
                                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["IsValue"] = "No";
                                }                               

                            }

                        //}                      
                       
                    }

                }
            }

            this.subDivisionDataSet.AcceptChanges();
            this.SubdivisionGridView.DataSource = this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.DefaultView;
            this.SetHeight(this.SubdivisionGridView.Rows.Count);

        }

        /// <summary>
        /// Adds the MI d5.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddMID5(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["MID5"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["MID5"].ToString()))
                {
                    if (this.formulaDatas["MID5"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                        this.formulaDatas.Remove("MID5");
                        this.formulaDatas.Add("MID5", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["MID5"].ToString(), out  tempParcel);

                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["MID5"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.MID5.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = tempParcel + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                        this.formulaDatas.Remove("MID5");
                        this.formulaDatas.Add("MID5", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["MID5"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["MID5"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["MID5"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.MID5.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = this.alphaNumericString["MID5"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                            this.formulaDatas.Remove("MID5");
                            this.formulaDatas.Add("MID5", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                        this.formulaDatas.Remove("MID5");
                        this.formulaDatas.Add("MID5", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["MID5"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["MID5"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = this.alphaNumericString["MID5"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID5"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the MI d4.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddMID4(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["MID4"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["MID4"].ToString()))
                {
                    if (this.formulaDatas["MID4"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                        this.formulaDatas.Remove("MID4");
                        this.formulaDatas.Add("MID4", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["MID4"].ToString(), out  tempParcel);

                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["MID4"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.MID4.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = tempParcel + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                        this.formulaDatas.Remove("MID4");
                        this.formulaDatas.Add("MID4", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["MID4"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["MID4"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["MID4"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.MID4.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = this.alphaNumericString["MID4"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                            this.formulaDatas.Remove("MID4");
                            this.formulaDatas.Add("MID4", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                        this.formulaDatas.Remove("MID4");
                        this.formulaDatas.Add("MID4", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["MID4"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["MID4"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = this.alphaNumericString["ParcelNumber"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID4"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the MI d3.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddMID3(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["MID3"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["MID3"].ToString()))
                {
                    if (this.formulaDatas["MID3"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                        this.formulaDatas.Remove("MID3");
                        this.formulaDatas.Add("MID3", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["MID3"].ToString(), out  tempParcel);

                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["MID3"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.MID3.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = tempParcel + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                        this.formulaDatas.Remove("MID3");
                        this.formulaDatas.Add("MID3", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["MID3"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["MID3"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["MID3"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.MID3.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = this.alphaNumericString["MID3"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                            this.formulaDatas.Remove("MID3");
                            this.formulaDatas.Add("MID3", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                        this.formulaDatas.Remove("MID3");
                        this.formulaDatas.Add("MID3", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["MID3"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["MID3"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = this.alphaNumericString["MID3"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID3"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the MI d2.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddMID2(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["MID2"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["MID2"].ToString()))
                {
                    if (this.formulaDatas["MID2"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                        this.formulaDatas.Remove("MID2");
                        this.formulaDatas.Add("MID2", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["MID2"].ToString(), out  tempParcel);

                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["MID2"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.MID2.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = tempParcel + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                        this.formulaDatas.Remove("MID2");
                        this.formulaDatas.Add("MID2", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["MID2"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["MID2"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["MID2"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.MID2.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = this.alphaNumericString["MID2"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                            this.formulaDatas.Remove("MID2");
                            this.formulaDatas.Add("MID2", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                        this.formulaDatas.Remove("MID2");
                        this.formulaDatas.Add("MID2", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["MID2"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["MID2"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = this.alphaNumericString["MID2"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID2"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the MI d1.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddMID1(int newRowIndex)
        {
            int mID1MaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["MID1"].ToString() != "True")
            {
                // Get the parcel value
                long tempMID1;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["MID1"].ToString()))
                {
                    if (this.formulaDatas["MID1"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                        this.formulaDatas.Remove("MID1");
                        this.formulaDatas.Add("MID1", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["MID1"].ToString(), out  tempMID1);

                // Checks its zero
                if (tempMID1 < 0 || string.IsNullOrEmpty(this.formulaDatas["MID1"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempMID1 + newRowIndex;
                    mID1MaxLen = totalInputLength.ToString().Length;

                    if (this.MID1.MaxInputLength >= mID1MaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = tempMID1 + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                        this.formulaDatas.Remove("MID1");
                        this.formulaDatas.Add("MID1", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["MID1"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["MID1"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["MID1"] + temp.ToString().PadLeft(value.Length, '0');
                        mID1MaxLen = decparcelvalue.Length;
                        if (this.MID1.MaxInputLength >= mID1MaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = this.alphaNumericString["MID1"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                            this.formulaDatas.Remove("MID1");
                            this.formulaDatas.Add("MID1", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                        this.formulaDatas.Remove("MID1");
                        this.formulaDatas.Add("MID1", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["MID1"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["MID1"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = this.alphaNumericString["MID1"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["MID1"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the acres.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddAcres(int newRowIndex)
        {
            // Get the Acres value
            int acresmaxlength;
            if (this.alphaNumericDatas["Acres"].ToString() != "True")
            {
                // Get the Acres value
                long tempAcres;

                bool isAlpha;

                if (!this.IsDecimal(this.formulaDatas["Acres"].ToString()))
                {
                    long.TryParse(this.formulaDatas["Acres"].ToString(), out tempAcres);
                    isAlpha = true;
                }
                else
                {
                    isAlpha = false;
                }

                Int64.TryParse(this.formulaDatas["Acres"].ToString(), out tempAcres);
                // Checks its zero
                if (tempAcres == 0 && isAlpha)
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = tempAcres + newRowIndex;
                    long totalacres = tempAcres + newRowIndex;
                    acresmaxlength = totalacres.ToString().Length;

                    ////Commented by Biju to check max length
                    ////if (this.Acres.MaxInputLength >= acresmaxlength)
                    ////Added by Biju to check max length
                    if (9 >= acresmaxlength)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = tempAcres + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                        this.formulaDatas.Remove("Acres");
                        this.formulaDatas.Add("Acres", string.Empty);
                    }
                }
            }
            else
            {
                ////Added by Biju on 24-Sep-2009
                if (this.IsDecimal(this.alphaNumericString["Acres"].ToString()))
                {
                    decimal temp = Convert.ToDecimal(this.alphaNumericString["Acres"].ToString());
                    temp += 1;
                    this.alphaNumericString.Remove("Acres");
                    if (temp.ToString().Length <= 9)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = temp + Convert.ToDecimal(this.formulaDatas["Acres"].ToString());
                        this.alphaNumericString.Add("Acres", temp);
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                        this.alphaNumericString.Add("Acres", string.Empty);
                    }

                }
                ////till here
                ////Commented by Biju on 24-Sep-2009
                ////// Check is it a decimal
                ////if (this.IsDecimal(this.formulaDatas["Acres"].ToString()))
                ////{
                ////    // Gets the Acres value
                ////    string value = this.formulaDatas["Acres"].ToString();

                ////    // Construt padding
                ////    string padding = newRowIndex.ToString().PadLeft(value.Length - 1, '0');

                ////    //Add the decimal point
                ////    padding = "." + padding;

                ////    // Add Decimal Part
                ////    decimal temp = Convert.ToDecimal(value) + Convert.ToDecimal(padding);

                ////    // Get the values in the cell
                ////    decimal oringal;
                ////    decimal.TryParse(this.alphaNumericString["Acres"].ToString(), out oringal);

                ////    string wholepart = this.alphaNumericString["Acres"].ToString();
                ////    acresmaxlength = wholepart.Length + padding.Length;


                ////    if (this.Acres.MaxInputLength >= acresmaxlength)
                ////    {
                ////        if (wholepart.Length <= 9)
                ////        {
                ////            // Add Existing value and added value.
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = oringal + temp;
                ////        }
                ////        else
                ////        {
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                ////            this.formulaDatas.Remove("Acres");
                ////            this.formulaDatas.Add("Acres", string.Empty);
                ////        }
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                ////        this.formulaDatas.Remove("Acres");
                ////        this.formulaDatas.Add("Acres", string.Empty);
                ////    }

                ////}
                ////else
                ////{
                ////    if (!string.IsNullOrEmpty(this.formulaDatas["Acres"].ToString()))
                ////    {
                ////        // Add one with existing value.
                ////        long temp = Convert.ToInt64(this.formulaDatas["Acres"].ToString()) + newRowIndex;

                ////        // Concat Existing value and added value.
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = this.alphaNumericString["Acres"] + temp.ToString();
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = string.Empty;
                ////    }
                ////}
            }
        }

        private void AddWidth(int newRowIndex)
        {
            // Get the Acres value
            int acresmaxlength;
            if (this.alphaNumericDatas["Width"].ToString() != "True")
            {
                // Get the Acres value
                long tempAcres;

                bool isAlpha;

                if (!this.IsDecimal(this.formulaDatas["Width"].ToString()))
                {
                    long.TryParse(this.formulaDatas["Width"].ToString(), out tempAcres);
                    isAlpha = true;
                }
                else
                {
                    isAlpha = false;
                }

                Int64.TryParse(this.formulaDatas["Width"].ToString(), out tempAcres);
                // Checks its zero
                if (tempAcres == 0 && isAlpha)
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = tempAcres + newRowIndex;
                    long totalacres = tempAcres + newRowIndex;
                    acresmaxlength = totalacres.ToString().Length;

                    ////Commented by Biju to check max length
                    ////if (this.Acres.MaxInputLength >= acresmaxlength)
                    ////Added by Biju to check max length
                    if (9 >= acresmaxlength)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = tempAcres + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                        this.formulaDatas.Remove("Width");
                        this.formulaDatas.Add("Width", string.Empty);
                    }
                }
            }
            else
            {
                ////Added by Biju on 24-Sep-2009
                if (this.IsDecimal(this.alphaNumericString["Width"].ToString()))
                {
                    decimal temp = Convert.ToDecimal(this.alphaNumericString["Width"].ToString());
                    temp += 1;
                    this.alphaNumericString.Remove("Width");
                    if (temp.ToString().Length <= 9)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = temp + Convert.ToDecimal(this.formulaDatas["Width"].ToString());
                        this.alphaNumericString.Add("Width", temp);
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                        this.alphaNumericString.Add("Width", string.Empty);
                    }

                }
                ////till here
                ////Commented by Biju on 24-Sep-2009
                ////// Check is it a decimal
                ////if (this.IsDecimal(this.formulaDatas["Width"].ToString()))
                ////{
                ////    // Gets the Acres value
                ////    string value = this.formulaDatas["Width"].ToString();

                ////    // Construt padding
                ////    string padding = newRowIndex.ToString().PadLeft(value.Length - 1, '0');

                ////    //Add the decimal point
                ////    padding = "." + padding;

                ////    // Add Decimal Part
                ////    decimal temp = Convert.ToDecimal(value) + Convert.ToDecimal(padding);

                ////    // Get the values in the cell
                ////    decimal oringal;
                ////    decimal.TryParse(this.alphaNumericString["Width"].ToString(), out oringal);

                ////    string wholepart = this.alphaNumericString["Width"].ToString();
                ////    acresmaxlength = wholepart.Length + padding.Length;


                ////    if (this.Acres.MaxInputLength >= acresmaxlength)
                ////    {
                ////        if (wholepart.Length <= 9)
                ////        {
                ////            // Add Existing value and added value.
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = oringal + temp;
                ////        }
                ////        else
                ////        {
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                ////            this.formulaDatas.Remove("Width");
                ////            this.formulaDatas.Add("Width", string.Empty);
                ////        }
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                ////        this.formulaDatas.Remove("Width");
                ////        this.formulaDatas.Add("Width", string.Empty);
                ////    }

                ////}
                ////else
                ////{
                ////    if (!string.IsNullOrEmpty(this.formulaDatas["Width"].ToString()))
                ////    {
                ////        // Add one with existing value.
                ////        long temp = Convert.ToInt64(this.formulaDatas["Width"].ToString()) + newRowIndex;

                ////        // Concat Existing value and added value.
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = this.alphaNumericString["Width"] + temp.ToString();
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                ////    }
                ////}
            }
        }

        /// <summary>
        /// Adds the width.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddWidth1(int newRowIndex)
        {
            // Get the width value
            if (this.alphaNumericDatas["Width"].ToString() != "True")
            {

                // Get the width value
                long tempWidth;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["Width"].ToString()))
                {
                    if (this.formulaDatas["Width"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Width"] = string.Empty;
                        this.formulaDatas.Remove("Width");
                        this.formulaDatas.Add("Width", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["Width"].ToString(), out  tempWidth);

                // Checks its zero
                if (tempWidth < 0 || string.IsNullOrEmpty(this.formulaDatas["Width"].ToString()))
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = tempWidth + newRowIndex;
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["Width"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["Width"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    decimal temp = Convert.ToDecimal(value) + Convert.ToDecimal(padding);

                    // Add Existing value and added value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = this.alphaNumericString["Width"] + temp.ToString().PadLeft(value.Length, '0');
                }
                else
                {
                    // Add one with existing value.
                    long temp = Convert.ToInt64(this.formulaDatas["Depth"].ToString()) + Convert.ToInt64(newRowIndex);

                    // Concat Existing value and added value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotWidth"] = this.alphaNumericString["Width"] + temp.ToString();
                }
            }
        }



        private void AddDepth(int newRowIndex)
        {
            // Get the Acres value
            int acresmaxlength;
            if (this.alphaNumericDatas["Depth"].ToString() != "True")
            {
                // Get the Acres value
                long tempAcres;

                bool isAlpha;

                if (!this.IsDecimal(this.formulaDatas["Depth"].ToString()))
                {
                    long.TryParse(this.formulaDatas["Depth"].ToString(), out tempAcres);
                    isAlpha = true;
                }
                else
                {
                    isAlpha = false;
                }

                Int64.TryParse(this.formulaDatas["Depth"].ToString(), out tempAcres);
                // Checks its zero
                if (tempAcres == 0 && isAlpha)
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    ////this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Acres"] = tempAcres + newRowIndex;
                    long totalacres = tempAcres + newRowIndex;
                    acresmaxlength = totalacres.ToString().Length;
                    ////Commented by Biju to check max length
                    ////if (this.Acres.MaxInputLength >= acresmaxlength)
                    ////Added by Biju to check max length
                    if (9 >= acresmaxlength)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = tempAcres + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                        this.formulaDatas.Remove("Depth");
                        this.formulaDatas.Add("Depth", string.Empty);
                    }
                }
            }
            else
            {
                ////Added by Biju on 24-Sep-2009
                if (this.IsDecimal(this.alphaNumericString["Depth"].ToString()))
                {
                    decimal temp = Convert.ToDecimal(this.alphaNumericString["Depth"].ToString());
                    temp += 1;
                    this.alphaNumericString.Remove("Depth");
                    if (temp.ToString().Length <= 9)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = temp + Convert.ToDecimal(this.formulaDatas["Depth"].ToString());
                        this.alphaNumericString.Add("Depth", temp);
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                        this.alphaNumericString.Add("Depth", string.Empty);
                    }


                }
                ////till here
                ////Commented by Biju on 24-Sep-2009
                ////// Check is it a decimal
                ////if (this.IsDecimal(this.formulaDatas["Depth"].ToString()))
                ////{
                ////    // Gets the Acres value
                ////    string value = this.formulaDatas["Depth"].ToString();

                ////    // Construt padding
                ////    string padding = newRowIndex.ToString().PadLeft(value.Length - 1, '0');

                ////    //Add the decimal point
                ////    padding = "." + padding;

                ////    // Add Decimal Part
                ////    decimal temp = Convert.ToDecimal(value) + Convert.ToDecimal(padding);

                ////    // Get the values in the cell
                ////    decimal oringal;
                ////    decimal.TryParse(this.alphaNumericString["Depth"].ToString(), out oringal);

                ////    string wholepart = this.alphaNumericString["Depth"].ToString();
                ////    acresmaxlength = wholepart.Length + padding.Length;


                ////    if (this.Acres.MaxInputLength >= acresmaxlength)
                ////    {
                ////        if (wholepart.Length <= 9)
                ////        {
                ////            // Add Existing value and added value.
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = oringal + temp;
                ////        }
                ////        else
                ////        {
                ////            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                ////            this.formulaDatas.Remove("Depth");
                ////            this.formulaDatas.Add("Depth", string.Empty);
                ////        }
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                ////        this.formulaDatas.Remove("Depth");
                ////        this.formulaDatas.Add("Depth", string.Empty);
                ////    }

                ////}
                ////else
                ////{
                ////    if (!string.IsNullOrEmpty(this.formulaDatas["Depth"].ToString()))
                ////    {
                ////        // Add one with existing value.
                ////        long temp = Convert.ToInt64(this.formulaDatas["Depth"].ToString()) + newRowIndex;

                ////        // Concat Existing value and added value.
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = this.alphaNumericString["Depth"] + temp.ToString();
                ////    }
                ////    else
                ////    {
                ////        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                ////    }
                ////}
            }
        }

        /// <summary>
        /// Adds the depth.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddDepth1(int newRowIndex)
        {
            // checks the Depth grid values is a alpha numeric
            if (this.alphaNumericDatas["Depth"].ToString() != "True")
            {
                // Get the Depth value
                long tempDepth;


                long.TryParse(this.formulaDatas["Depth"].ToString(), out  tempDepth);

                // Checks its zero
                if (tempDepth < 0 || string.IsNullOrEmpty(this.formulaDatas["Depth"].ToString()))
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = tempDepth + newRowIndex;
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["Depth"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["Depth"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    decimal temp = Convert.ToDecimal(value) + Convert.ToDecimal(padding);

                    // Add Existing value and added value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["LotDepth"] = this.alphaNumericString["Depth"] + temp.ToString().PadLeft(value.Length, '0');
                }
                else
                {
                    // Add one with existing value
                    long temp = Convert.ToInt64(this.formulaDatas["Depth"].ToString()) + newRowIndex;

                    // Concat Existing value and added value.
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Depth"] = this.alphaNumericString["Depth"] + temp.ToString();
                }
            }
        }

        /// <summary>
        /// Adds the lot.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddLot(int newRowIndex)
        {
            int bolckmaxlen;
            // checks the Block grid values is a alpha numeric
            if (this.alphaNumericDatas["Lot"].ToString() != "True")
            {
                // Get the Block value
                long tempBlock;
                long totalinputlength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["Lot"].ToString()))
                {
                    if (this.formulaDatas["Lot"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                        this.formulaDatas.Remove("Lot");
                        this.formulaDatas.Add("Lot", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["Lot"].ToString(), out  tempBlock);

                // Checks its zero
                if (tempBlock < 0 || string.IsNullOrEmpty(this.formulaDatas["Lot"].ToString()))
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalinputlength = tempBlock + newRowIndex;

                    bolckmaxlen = totalinputlength.ToString().Length;

                    if (this.Lot.MaxInputLength >= bolckmaxlen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = tempBlock + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                        this.formulaDatas.Remove("Lot");
                        this.formulaDatas.Add("Lot", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["Lot"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["Lot"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["Lot"] + temp.ToString().PadLeft(value.Length, '0');
                        bolckmaxlen = decparcelvalue.Length;
                        if (this.Lot.MaxInputLength >= bolckmaxlen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = this.alphaNumericString["Lot"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                            this.formulaDatas.Remove("Lot");
                            this.formulaDatas.Add("Lot", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                        this.formulaDatas.Remove("Lot");
                        this.formulaDatas.Add("Lot", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["Lot"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["Lot"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = this.alphaNumericString["Block"] + temp.ToString();
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Lot"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the block.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddBlock(int newRowIndex)
        {
            int bolckmaxlen;
            // checks the Block grid values is a alpha numeric
            if (this.alphaNumericDatas["Block"].ToString() != "True")
            {
                // Get the Block value
                long tempBlock;
                long totalinputlength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["Block"].ToString()))
                {
                    if (this.formulaDatas["Block"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                        this.formulaDatas.Remove("Block");
                        this.formulaDatas.Add("Block", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["Block"].ToString(), out  tempBlock);

                // Checks its zero
                if (tempBlock < 0 || string.IsNullOrEmpty(this.formulaDatas["Block"].ToString()))
                {
                    // Zero then assign the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalinputlength = tempBlock + newRowIndex;

                    bolckmaxlen = totalinputlength.ToString().Length;

                    if (this.Block.MaxInputLength >= bolckmaxlen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = tempBlock + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                        this.formulaDatas.Remove(this.Block.Name);
                        this.formulaDatas.Add(this.Block.Name, string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["Block"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["Block"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["Block"] + temp.ToString().PadLeft(value.Length, '0');
                        bolckmaxlen = decparcelvalue.Length;
                        if (this.Block.MaxInputLength >= bolckmaxlen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = this.alphaNumericString["Block"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                            this.formulaDatas.Remove("Block");
                            this.formulaDatas.Add("Block", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                        this.formulaDatas.Remove("Block");
                        this.formulaDatas.Add("Block", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["Block"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["Block"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = this.alphaNumericString["Block"] + temp.ToString();
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["Block"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the house number.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddHouseNumber(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["HouseNumber"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["HouseNumber"].ToString()))
                {
                    if (this.formulaDatas["HouseNumber"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                        this.formulaDatas.Remove("HouseNumber");
                        this.formulaDatas.Add("HouseNumber", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["HouseNumber"].ToString(), out  tempParcel);

                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["HouseNumber"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.AddNum.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = tempParcel + newRowIndex;
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                        this.formulaDatas.Remove("HouseNumber");
                        this.formulaDatas.Add("HouseNumber", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["HouseNumber"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["HouseNumber"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');


                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["AddNum"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.AddNum.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = this.alphaNumericString["AddNum"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                            this.formulaDatas.Remove("HouseNumber");
                            this.formulaDatas.Add("HouseNumber", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                        this.formulaDatas.Remove("HouseNumber");
                        this.formulaDatas.Add("HouseNumber", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["HouseNumber"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["HouseNumber"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = this.alphaNumericString["HouseNumber"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["HouseNumber"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the parcel.
        /// </summary>
        /// <param name="newRowIndex">New index of the row.</param>
        private void AddParcel(int newRowIndex)
        {
            int parcelMaxLen;
            // checks the parcel grid values is a alpha numeric
            if (this.alphaNumericDatas["ParcelNumber"].ToString() != "True")
            {
                // Get the parcel value
                long tempParcel;
                long totalInputLength;

                ////Long Datatype maxlimit validation checking.
                if (!string.IsNullOrEmpty(this.formulaDatas["ParcelNumber"].ToString()))
                {
                    if (this.formulaDatas["ParcelNumber"].ToString().Length >= 19)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                        this.formulaDatas.Remove("ParcelNumber");
                        this.formulaDatas.Add("ParcelNumber", string.Empty);
                    }
                }

                long.TryParse(this.formulaDatas["ParcelNumber"].ToString(), out  tempParcel);
                ////Added by Biju on 02-Dec-2010 to implement #8836
                string trailZeros = "", numberFormat = "";
                if (!this.formulaDatas["ParcelNumber"].ToString().Length.Equals(tempParcel.ToString().Length))
                    trailZeros = this.formulaDatas["ParcelNumber"].ToString().Replace(tempParcel.ToString(), "");
                numberFormat = numberFormat.PadLeft(tempParcel.ToString().Length, '#');
                numberFormat = "{0:" + trailZeros + numberFormat + "}";
                // Checks its zero
                if (tempParcel < 0 || string.IsNullOrEmpty(this.formulaDatas["ParcelNumber"].ToString()))
                {
                    // Zero then assing the empty
                    this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                }
                else
                {
                    // Else add one .
                    totalInputLength = tempParcel + newRowIndex;
                    parcelMaxLen = totalInputLength.ToString().Length;

                    if (this.Parcel.MaxInputLength >= parcelMaxLen)
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = String.Format(numberFormat, tempParcel + newRowIndex);
                    }
                    else
                    {
                        //// MessageBox.Show("Value Exceeds the max limit.");
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                        this.formulaDatas.Remove("ParcelNumber");
                        this.formulaDatas.Add("ParcelNumber", string.Empty);
                    }
                }
            }
            else
            {
                // checks is it a decimal
                if (this.IsDecimal(this.formulaDatas["ParcelNumber"].ToString()))
                {
                    // Remove the dot.
                    string value = this.formulaDatas["ParcelNumber"].ToString().Replace(".", "");

                    // Construt padding.
                    string padding = newRowIndex.ToString().PadLeft(value.Length, '0');

                    // Add the Record
                    long tempValue;
                    decimal temp;
                    string decparcelvalue;

                    if (value.Length <= 19)
                    {
                        long.TryParse(value, out tempValue);
                        temp = tempValue + Convert.ToDecimal(padding);
                        decparcelvalue = this.alphaNumericString["Parcel"] + temp.ToString().PadLeft(value.Length, '0');
                        parcelMaxLen = decparcelvalue.Length;
                        if (this.Parcel.MaxInputLength >= parcelMaxLen)
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = this.alphaNumericString["Parcel"] + temp.ToString().PadLeft(value.Length, '0');
                        }
                        else
                        {
                            this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                            this.formulaDatas.Remove("ParcelNumber");
                            this.formulaDatas.Add("ParcelNumber", string.Empty);
                        }
                    }
                    else
                    {
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                        this.formulaDatas.Remove("ParcelNumber");
                        this.formulaDatas.Add("ParcelNumber", string.Empty);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.formulaDatas["ParcelNumber"].ToString()))
                    {
                        // Add one with existing value
                        long temp = Convert.ToInt64(this.formulaDatas["ParcelNumber"].ToString()) + newRowIndex;

                        // Concat Existing value and added value.
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = this.alphaNumericString["ParcelNumber"] + temp.ToString();
                    }
                    else
                    {
                        // Zero then assing the empty
                        this.subDivisionDataSet.F29505_Get_SubdivisionGridDetails.Rows[newRowIndex]["ParcelNumber"] = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the specified value is decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// >true</c> if the specified value is decimal; otherwise, <c>false</c>.
        /// </returns>
        private bool IsDecimal(string value)
        {
            return IsMatch(value, @"\d+\.?\d*");
        }

        /// <summary>
        /// Handles the KeyPress event of the NoOfParcelTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void NoOfParcelTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (NoOfParcelTextBox.Text != "0")
                {
                    this.SetEditRecord();
                    this.ProcessButton.Enabled = false;
                    this.noofparcelflag = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the NoOfParcelTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void NoOfParcelTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int newparcel;
                // Condition added for QuickFind
                if (this.noofparcelflag || string.IsNullOrEmpty(NoOfParcelTextBox.Text.Trim()))
                {
                    this.SetEditRecord();
                    this.ProcessButton.Enabled = false;
                }
                else
                {
                    int noofParcel = this.SubdivisionGridView.OriginalRowCount;
                    int.TryParse(NoOfParcelTextBox.Text.Trim(), out newparcel);
                    if (newparcel != noofParcel)
                    {
                        this.SetEditRecord();
                        this.ProcessButton.Enabled = false;
                        this.noofparcelflag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox combo = (ComboBox)sender;
                if (!string.IsNullOrEmpty(combo.Text))
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the SubdivisionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ComboBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                ////SubDivision/Section Combo
                if (!this.tempSubDivision.Equals(this.SubdivisionComboBox.SelectedValue))
                {
                    if (this.tempSubDivision > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.SubdivisionComboBox.SelectedValue == null)
                    {
                        this.SubdivisionComboBox.SelectedValue = 0;
                        this.SubdivisionComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempSubDivision = (int)this.SubdivisionComboBox.SelectedValue;
                    }
                }

                ////DOR Combo
                if (!this.tempDOR.Equals(this.DORCodeComboBox.Text.Trim()))
                {
                    if (!string.IsNullOrEmpty(this.tempDOR))
                    {
                        this.SetEditRecord();
                    }

                    if (this.DORCodeComboBox.SelectedValue == null)
                    {
                        this.DORCodeComboBox.SelectedValue = 0;
                        this.DORCodeComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempDOR = this.DORCodeComboBox.Text.Trim();
                    }
                }

                ////Neighborhood COmbo
                if (!this.tempNeighborhood.Equals(this.NeighborhoodComboBox.SelectedValue))
                {
                    if (this.tempNeighborhood > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.NeighborhoodComboBox.SelectedValue == null)
                    {
                        this.NeighborhoodComboBox.SelectedValue = 0;
                        this.NeighborhoodComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempNeighborhood = (int)this.NeighborhoodComboBox.SelectedValue;
                        int rollYear;
                        int.TryParse(this.RollYearTextBox.Text, out rollYear);
                        this.LandCodeDataSet = this.form29505Control.WorkItem.LandCodes(this.tempNeighborhood, rollYear);
                        DataRow landCodeRow = this.LandCodeDataSet.f36035ListLandCodes.NewRow();
                        landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName] = "";
                        landCodeRow[this.LandCodeDataSet.f36035ListLandCodes.RollYearColumn.ColumnName] = rollYear;
                        this.LandCodeDataSet.f36035ListLandCodes.Rows.InsertAt(landCodeRow, 0);
                        //this.LandCodeDataSet.f36035ListLandCodes.Rows.Add(landCodeRow); 
                        this.GetLandCode();
                    }
                    for (int i = 0; i < this.SubdivisionGridView.OriginalRowCount; i++)
                    {
                        if (this.SubdivisionGridView.Rows[i].Cells["AdjustmentType"].Value.Equals("1"))
                        {
                            this.SubdivisionGridView.Rows[i].Cells["Adjustment"].Value = "";
                        }
                    }
                }

                ////District Combo
                if (!this.tempDistrict.Equals(this.DistrictComboBox.SelectedValue))
                {
                    if (this.tempDistrict > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.DistrictComboBox.SelectedValue == null)
                    {
                        this.DistrictComboBox.SelectedValue = 0;
                        this.DistrictComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempDistrict = (int)this.DistrictComboBox.SelectedValue;
                        ////this.DistrictComboBox.SelectedIndex = -1;
                        ////this.DistrictComboBox.SelectedValue = tempDistrict;
                    }
                }

                ////LandType1
                if (!this.tempLandType1.Equals(this.LandType1ComboBox.SelectedValue))
                {
                    if (this.tempLandType1 > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.LandType1ComboBox.SelectedValue == null)
                    {
                        this.LandType1ComboBox.SelectedValue = 0;
                        this.LandType1ComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempLandType1 = (int)this.LandType1ComboBox.SelectedValue;
                        this.GetLandCode();
                    }
                }

                ////LandType2
                if (!this.tempLandType2.Equals(this.LandType2ComboBox.SelectedValue))
                {
                    if (this.tempLandType2 > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.LandType2ComboBox.SelectedValue == null)
                    {
                        this.LandType2ComboBox.SelectedValue = 0;
                        this.LandType2ComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempLandType2 = (int)this.LandType2ComboBox.SelectedValue;
                        this.GetLandCode();
                    }
                }

                ////LandType3
                if (!this.tempLandType3.Equals(this.LandType3ComboBox.SelectedValue))
                {
                    if (this.tempLandType3 > 0)
                    {
                        this.SetEditRecord();
                    }

                    if (this.LandType3ComboBox.SelectedValue == null)
                    {
                        this.LandType3ComboBox.SelectedValue = 0;
                        this.LandType3ComboBox.Text = string.Empty;
                    }
                    else
                    {
                        this.tempLandType3 = (int)this.LandType3ComboBox.SelectedValue;
                        this.GetLandCode();
                    }
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.isprocessed.Equals(0))
            {

                this.SetEditRecord();
                this.ProcessButton.Enabled = false;

            }
        }

        /// <summary>
        /// Handles the CellEnter event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.columnIndex = e.ColumnIndex;
            if (e.ColumnIndex.Equals(27))
            {
                if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[26].Value.ToString().Equals("1"))
                {
                    DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
                    Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                    Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
                    Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

                    if (Landcode != null)
                    {
                        if (!e.RowIndex.Equals(27) && !e.ColumnIndex.Equals(27))
                        {
                            //this.SubdivisionGridView.Rows[0].(Landcode);  
                            this.SubdivisionGridView[27, this.rowIndex] = Landcode;
                        }
                    }

                }
                else if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[26].Value.ToString().Equals("0"))
                {
                    DataGridViewTextBoxCell TEXT = new DataGridViewTextBoxCell();
                    TEXT.MaxInputLength = 13;
                    if (TEXT != null)
                    {
                        ///this.SubdivisionGridView[27, this.rowIndex] = TEXT;
                    }
                    this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].ReadOnly = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.SubdivisionGridView.Rows[e.RowIndex].Cells[27].Value.ToString()))
                    {
                        DataGridViewTextBoxCell TEXT = new DataGridViewTextBoxCell();
                        TEXT.MaxInputLength = 13;
                        if (TEXT != null)
                        {
                            if (!e.RowIndex.Equals(27) && !e.ColumnIndex.Equals(27))
                            {
                                //this.SubdivisionGridView.Rows[0].(Landcode);  
                                this.SubdivisionGridView[27, this.rowIndex] = TEXT;
                            }
                        }
                        this.SubdivisionGridView.Rows[this.rowIndex].Cells["Adjustment"].ReadOnly = false;
                    }
                }
            }
            ////this.streetComboValidated = false;
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the SubdivisionGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void SubdivisionGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //this.SubdivisionGridView.Focus();
            //this.CreateSubdivisionMainPanel.HorizontalScroll.Value = this.scrollpos;  
            // this.SubdivisionGridView.Rows[0].Cells[0].Selected = true;    
            /*
            if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                this.SubdivisionGridView.Rows[0].Cells[0].Selected = false;  
                this.CreateSubdivisionMainPanel.HorizontalScroll.Value = this.scrollpos;
                this.NeighborhoodLabel.Focus();   
                //this.SubdivisionGridView.Rows[0].Cells[0].Selected = true;
                
                //this.SubdivisionGridView.DeselectCurrentCell = false;
                //this.SubdivisionGridView.DeselectSpecifiedRow = 0;   
                //this.NeighborhoodLabel.Focus();   
            }
            if (this.SubdivisionGridView.EditingControl is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)this.SubdivisionGridView.EditingControl).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)this.SubdivisionGridView.EditingControl).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((ComboBox)this.SubdivisionGridView.EditingControl).AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < SubdivisionGridView.Columns.Count; ++i)
                    //SubdivisionGridView.CurrentCell.Frozen = false;
                if (SubdivisionGridView.CurrentCell.ColumnIndex== e.ColumnIndex)
                {
                    //SubdivisionGridView.CurrentCell.ColumnIndex = -1;
                    // GridFormStatusBar.Text = "UnFroze Column " + dGV.Columns[e.ColumnIndex].Name;
                }
                else
                {
                    SubdivisionGridView.Columns[e.ColumnIndex].Frozen = true;
                    // curFrozenCol = e.ColumnIndex;
                    // GridFormStatusBar.Text = "Froze Column " + dGV.Columns[e.ColumnIndex].Name;
                }
            }*/

        }

        private void SubdivisionGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!this.editallow)
            {
                e.Cancel = true;
            }
            this.CreateSubdivisionMainPanel.HorizontalScroll.Value = this.scrollpos;
            this.scrollmov = true;
            //if (e.ColumnIndex.Equals("26"))
            //{

            //    if (this.SubdivisionGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals("1"))
            //    {
            //        DataGridViewComboBoxCell Landcode = new DataGridViewComboBoxCell();
            //        Landcode.DisplayMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
            //        Landcode.ValueMember = this.LandCodeDataSet.f36035ListLandCodes.LandCodeColumn.ColumnName;
            //        Landcode.DataSource = this.LandCodeDataSet.f36035ListLandCodes.DefaultView;

            //        if (Landcode != null)
            //        {
            //            //this.SubdivisionGridView.Rows[0].(Landcode);  
            //            this.SubdivisionGridView[27, this.rowIndex] = Landcode;
            //        }
            //    }
            //}
            /* if (e.ColumnIndex.Equals(this.SubdivisionGridView.Columns["MID1"].Index))
             {
                 this.CreateSubdivisionMainPanel.HorizontalScroll.Value = this.MID1Panel.Location.X+this.MID1Panel.Width; 
             }
             if (e.ColumnIndex.Equals(this.SubdivisionGridView.Columns["MID2"].Index))
             {
                 this.CreateSubdivisionMainPanel.HorizontalScroll.Value = this.MID2Panel.Location.X + this.MID2Panel.Width;
             }*/
        }


        private void CreateSubdivisionMainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SubdivisionGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SubdivisionGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            //if (e.RowIndex.Equals(-1))
            //{
            ////    this.scrollmov = true;  
            //////    this.SubdivisionGridView.Rows[0].Cells[0].Selected = true;     
            //}
        }

        private void SubdivisionGridView_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex.Equals(28))
            {

            }

        }

        private void SubdivisionGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void SubdivisionGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            Decimal outDecimal;


            if (e.ColumnIndex == this.SubdivisionGridView.Columns["NewConstruction"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //// Only paint if text provided, Only paint if desired text is in cell
                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (tempvalue.EndsWith("."))
                    {
                        tempvalue = string.Concat(tempvalue, "0");
                    }

                    if (Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal))
                    {
                        //// check for Over / under based on tender type
                        //// ComboBox combo = (ComboBox)sender;

                        ////change property for combobox change

                        tempvalue = outDecimal.ToString();
                        tempvalue = tempvalue.Replace("-", "");

                        ////Added By ramya
                        ////if (!this.instrumentPayment)
                        ////{
                        ////Commented by purushotham on 06/March/13
                        ////if (!tempvalue.Contains("."))
                        ////{
                        ////    tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                        ////}
                        ////}

                        if (outDecimal.ToString().Contains("-"))
                        {
                            outDecimal = decimal.Parse(tempvalue);
                            outDecimal = decimal.Negate(outDecimal);
                        }
                        else
                        {
                            outDecimal = decimal.Parse(tempvalue);
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = decimal.Parse("0");
                        e.ParsingApplied = true;
                    }
                }
            }
        }


        private void ClassCodeComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == ' ')
            {

            }
            else
            {
                e.Handled = e.KeyChar != (char)Keys.Back;
            }

        }


        private void ClassCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            //dataSetCollection = new List<DataSetCollection>();
            //DataSet ds = new DataSet();
            int cursorPosition = ClassCodeTextBox.SelectionStart;
            this.tempClassCode = ClassCodeTextBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeTextBox.TextChanged -= new EventHandler(this.ClassCodeTextBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeTextBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeTextBox.SelectionStart = cursorPosition;
                }
                //ClassCodeRGB
                //AddDataSetValues("DSName", "", "f26000_udf_GetParcelClassCodeRGB");
                //ds = this.form26000Control.WorkItem.ClassCode_RGB(this.dataSetCollection[0].commandText);
                ClassCodeTextBox.TextChanged += new EventHandler(this.ClassCodeTextBox_TextChanged);
                this.SetEditRecord();
            }

        }

        private void ClassCodeComboBox_TextChanged(object sender, EventArgs e)
        {
            int cursorPosition = ClassCodeComboBox.SelectionStart;
            this.tempClassCode = ClassCodeComboBox.Text;
            this.templength = tempClassCode.Length;
            this.tempClassCode = tempClassCode.Replace(" ", "");
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tempClassCode))
            {
                List<string> result = new List<string>(Regex.Split(tempClassCode, @"(?<=\G.{2})", RegexOptions.Singleline));
                var count = result.Count;
                for (int i = 0; i < count; i++)
                {
                    if (result[i].Length.Equals(2))
                    {
                        if (sb.ToString().Length < 17)
                        {
                            string temp = result[i].Insert(2, " ").ToString();

                            sb.Append(temp);
                        }

                    }
                    else
                    {
                        if (sb.ToString().Length < 17)
                        {
                            sb.Append(result[i].ToString());
                        }
                    }
                }
                //previousCursorindex = cursorPosition;
                var length = sb.ToString().Length;
                ClassCodeComboBox.TextChanged -= new EventHandler(this.ClassCodeComboBox_TextChanged);
                if (length > 17)
                {
                    ClassCodeComboBox.Text = sb.ToString().Remove(17);
                    length = length - 1;
                }
                else
                {
                    ClassCodeComboBox.Text = sb.ToString();
                }
                if (length > templength)
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition + 1;
                }
                else
                {
                    if (cursorPosition != 0)
                    {
                        ClassCodeComboBox.SelectionStart = cursorPosition;
                    }
                }
                if (length.Equals(templength))
                {
                    ClassCodeComboBox.SelectionStart = cursorPosition;
                }
                ClassCodeComboBox.TextChanged += new EventHandler(this.ClassCodeComboBox_TextChanged);
                this.SetEditRecord();
            }
        }
        private void ClassCodeComboBox_TextUpdate(object sender, EventArgs e)
        {
            this.classCode = this.ClassCodeComboBox.Text;
            if ((!string.IsNullOrEmpty(this.ClassCodeComboBox.Text)) && (classCodeConfigValue > 0) && (classCode.ToString().Replace(" ", "").Length == classCodeConfigValue))
            {
                this.classCodeDataTable = this.form29505Control.WorkItem.F26000_ClassCodeDetails(classCode).f26000ClassCode;
                this.ClassCodeComboBox.DisplayMember = classCodeDataTable.ClassCodeColumn.ColumnName;                
                if (classCodeDataTable.Rows.Count > 0)
                {
                    this.ClassCodeComboBox.DataSource = classCodeDataTable.DefaultView;
                    this.classCode = this.ClassCodeComboBox.Text;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    this.ClassCodeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                    this.ClassCodeComboBox.Text = this.classCode;
                    this.ClassCodeComboBox.Select(this.ClassCodeComboBox.Text.Length, 0);
                   
                }                
            }

        }    

    }
}

