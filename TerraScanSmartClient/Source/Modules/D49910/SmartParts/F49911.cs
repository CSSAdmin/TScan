
//--------------------------------------------------------------------------------------------
// <copyright file="F49911.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49911.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 31-01-2008       KUPPUSAMY.B         Created
//*********************************************************************************/

namespace D49910
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using TerraScan.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.Common;
    using TerraScan.UI.Controls;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Common.Reports;
    using System.Reflection;
    using System.Linq; 
    #endregion NameSpace

    /// <summary>
    /// F49911 class
    /// </summary>
    public partial class F49911 : BaseSmartPart
    {
        #region Private Members


        /// <summary>
        /// formMasterPermissionNew
        /// </summary>
        private bool formMasterPermissionNew;


        /// <summary>
        /// Checks the copy button Clicked
        /// </summary>
        private bool copyButton;

        /// <summary>
        /// grantorId
        /// </summary>
        private int grantorId = 0;

        /// <summary>
        /// granteeId
        /// </summary>
        private int granteeId = 0;

        /// <summary>
        /// grantorSaveDataSet
        /// </summary>
        private DataSet grantorSaveDataSet = new DataSet("Root");

        /// <summary>
        /// granteeSaveDataSet
        /// </summary>
        private DataSet granteeSaveDataSet = new DataSet("Root");

        /// <summary>
        /// Variable for tempId
        /// </summary>
        private int tempId;

        /// <summary>
        /// Variable for RowId
        /// </summary>
        private int rowId;

        /// <summary>
        /// Variable for copyID
        /// </summary>
        private int copyID = 0;

        /// <summary>
        /// saveGrantorDetails
        /// </summary>
        private DataTable saveGrantorDetails = new DataTable("Table");

        /// <summary>
        /// saveGranteeDetails
        /// </summary>
        private DataTable saveGranteeDetails = new DataTable("Table");

        /// <summary>
        /// Instance for formattedGrantorValuesDS
        /// </summary>
        private DataSet formattedGrantorValuesDS = new DataSet("Root");

        /// <summary>
        /// formattedGrantorValueTable
        /// </summary>
        private DataTable formattedGrantorValueTable = new DataTable("Table");

        /// <summary>
        /// Instance for formattedGranteeValuesDS
        /// </summary>
        private DataSet formattedGranteeValuesDS = new DataSet("Root");

        /// <summary>
        /// formattedGrantorValueTable
        /// </summary>
        private DataTable formattedGranteeValueTable = new DataTable("Table");

        /// <summary>
        /// Instance for temporary datatable
        /// </summary>
        private DataTable grantorList = new DataTable();

        /// <summary>
        /// GranteeList
        /// </summary>
        private DataTable GranteeList = new DataTable();

        /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// controller F36032
        /// </summary>        
        private F49911Controller form49911Control;

        /// <summary>
        /// F49910InstrumentHeaderDataSet
        /// </summary>
        private F49910InstrumentHeaderDataSet form49911PartiesFieldData = new F49910InstrumentHeaderDataSet();

        /// <summary>
        /// F49910InstrumentHeaderDataSet
        /// </summary>
        private F49910InstrumentHeaderDataSet form49911InstrumentPartiesFieldData = new F49910InstrumentHeaderDataSet();

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
        /// PageModeTypes
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// PermissionFields
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// variable holds the MissingRequired field Flag.
        /// </summary>
        private bool flagMissingRequiredField = true;

        /// <summary>
        /// maxGridRowCount
        /// </summary>
        private int maxGridRowCount;

        /// <summary>
        /// Bool value for flagFormLoad
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// saveConfirm
        /// </summary>
        private int saveConfirm;

        /// <summary>
        /// variable for grantorEnteredText
        /// </summary>
        private string grantorEnteredText;

        /// <summary>
        /// variable for grantorSelectedTextIndex
        /// </summary>
        private int grantorSelectedTextIndex;

        /// <summary>
        /// variable for grantorRetainedNegativeIndex
        /// </summary>
        private int grantorRetainedNegativeIndex;

        /// <summary>
        /// variable for granteeEnteredText
        /// </summary>
        private string granteeEnteredText;

        /// <summary>
        /// variable for granteeSelectedTextIndex
        /// </summary>
        private int granteeSelectedTextIndex;

        /// <summary>
        /// variable for granteeRetainedNegativeIndex      
        /// </summary>
        private int granteeRetainedNegativeIndex;

        /// <summary>
        /// variable for  editedGridView       
        /// </summary>
        private string editedGridView;

        /// <summary>
        /// variable for  tempNewGrantorID      
        /// </summary>
        private int tempNewGrantorID;

        /// <summary>
        /// variable for  tempNewGranteeID      
        /// </summary>
        private int tempNewGranteeID;

        /// <summary>
        /// granteeFlag
        /// </summary>
        private bool granteeFlag;

        /// <summary>
        /// grantorFlag
        /// </summary>
        private bool grantorFlag;

        /// <summary>
        /// grantorFlag
        /// </summary>
        private bool cancelKeyPress;

        /// <summary>
        /// Stores the new value
        /// </summary>
        private int newValue;

        /// <summary>
        /// Checks  Grantee loaded 
        /// </summary>
        private bool granteeLoad;

        /// <summary>
        /// Checks grantee tabpressed
        /// </summary>
        private bool granteeTabPressed;

        /// <summary>
        /// Stores the grantee edited string
        /// </summary>
        private string granteeEditedString = string.Empty;

        /// <summary>
        /// stores the grantor new value
        /// </summary>
        private int newGrantorValue;

        /// <summary>
        /// Checks grantor tab pressed
        /// </summary>
        private bool grantorTabPressed;

        /// <summary>
        /// Checks grantee new row entered
        /// </summary>
        private bool granteeNewRowEnter;

        /// <summary>
        /// Checks grantor new row entered
        /// </summary>
        private bool grantorNewRowEnter;

        /// <summary>
        /// Checks New row created for grantee
        /// </summary>
        private bool granteeNewRowEntered;

        /// <summary>
        /// Checks New row created for grantor
        /// </summary>
        private bool grantorNewRowEntered;

        /// <summary>
        /// Stores the grantor edited string
        /// </summary>
        private string grantorEditedString = string.Empty;

        /// <summary>
        /// Check Enter Key is Pressed.
        /// </summary>
        private bool granteeEnterKeyPressed = true;

        /// <summary>
        /// Check Enter Key is Pressed.
        /// </summary>
        private bool grantorEnterKeyPressed = true;

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F49911"/> class.
        /// </summary>
        public F49911()
        {
            this.InitializeComponent();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F49911"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F49911(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.sectionIndicatorText = tabText;
            this.formMasterPermissionEdit = permissionEdit;
            this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, red, green, blue);
            this.granteeNewRowEntered = false;
            this.grantorNewRowEntered = false;
        }

        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// FormSlice_ValidationAlert
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// FormSlice_SectionIndicatorClick
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// FormSlice_EditEnabled
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Gets or sets the form49911 control.
        /// </summary>
        /// <value>The form49911 control.</value>
        [CreateNew]
        public F49911Controller Form49911Control
        {
            get { return this.form49911Control as F49911Controller; }
            set { this.form49911Control = value; }
        }
        #endregion Property

        #region Event Subscription

        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                this.formMasterPermissionNew = this.GetFormMasterNewPermission();

                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);


                if (this.keyId != eventArgs.Data.KeyId)
                {
                    ////To check the invalid key id in set slice event subscription db call is set to F36035_ListLandDetails Method to check invalid key id                    
                    this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(eventArgs.Data.KeyId);
                }

                ////To check the invalid key id in set slice event subscription db call is set to GetRecorderDetails Method to check invalid key id
                if (this.form49911PartiesFieldData.f49901RecorderDetailsDataTable.Rows.Count > 0)
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

                // Added And Condition for implementing permission
                if (this.GrantorGridView.OriginalRowCount >= 0 &&  (this.PermissionFiled.editPermission&& this.formMasterPermissionEdit))
                {
                    // Check for the Permission 
                    this.ControlLock(false);

                   
                  //  this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    
                }
                else
                {
                    this.ControlLock(true);
                }

            }
        }



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
                this.Height = this.PartiesPictureBox.Height;
                sliceResize.SliceFormHeight = this.PartiesPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.Height = this.PartiesPictureBox.Height;
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
                if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.FlagSliceForm = true;
                    this.ClearGrantorGrid();
                    this.ClearGranteeGrid();
                    this.flagFormLoad = true;
                    this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);
                    this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();

                    this.CustomizeGranteeGrid();
                    this.CustomizeGrantorGrid();
                    this.grantorFlag = false;
                    this.PopulateRowInGrantorGranteeGrid();

                    if (this.GrantorGridView.OriginalRowCount > 0)
                    {
                        this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count.ToString()) - 1) + ")";
                    }
                    else
                    {
                        ////this.GrantorGridView.Columns[0].HeaderText = string.Empty;
                        this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                    }

                    if (this.GranteeGridView.OriginalRowCount > 0)
                    {
                        this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count.ToString()) - 1) + ")";
                    }
                    else
                    {
                        this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                    }

                    this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                    if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                    {
                        this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                    }

                    this.SetFormHeight(this.maxGridRowCount);

                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                    //// Check that Data is present 
                    //if (this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count == 0 || this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count == 0)
                    //{
                    //    // If no record then disable the controls
                    //    this.ControlLock(true);
                    //}
                    //else
                    //{
                    //    //Else Apply the Permission
                    //    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    //}


                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.flagFormLoad = false;
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                   
                    //if (this.slicePermissionField.newPermission)
                    //{
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();

                        this.ClearGrantorGrid();
                        this.ClearGranteeGrid();
                        this.CustomizeGranteeGrid();
                        this.CustomizeGrantorGrid();
                       
                        /* Condition checked by Ramya*/
                        if (this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count == 0)
                        {
                            F49910InstrumentHeaderDataSet.F49911GrantorDetailsRow dr = this.form49911PartiesFieldData.F49911GrantorDetails.NewF49911GrantorDetailsRow();
                            if (this.form49911PartiesFieldData.F49911GrantorDetails.Columns.Contains(this.GrantorGridView.EmptyRecordColumnName))
                            {
                                dr[this.GrantorGridView.EmptyRecordColumnName] = false;
                            }

                            this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Add(dr);
                        }

                        if (this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count == 0)
                        {
                            F49910InstrumentHeaderDataSet.F49911GranteeDetailsRow drow = this.form49911PartiesFieldData.F49911GranteeDetails.NewF49911GranteeDetailsRow();
                            if (this.form49911PartiesFieldData.F49911GranteeDetails.Columns.Contains(this.GranteeGridView.EmptyRecordColumnName))
                            {
                                drow[this.GranteeGridView.EmptyRecordColumnName] = false;
                            }

                            this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Add(drow);
                        }
                        /*Till here */

                        this.SetHeightForGrantorGrid(0);
                        this.SetHeightForGranteeGrid(0);
                        this.SetFormHeight(0);
                        SliceResize sliceResize;

                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.PartiesPictureBox.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);

                        if (this.GrantorGridView.OriginalRowCount > 0)
                        {
                            this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count.ToString()) - 1) + ")";
                            if (string.IsNullOrEmpty(this.GrantorGridView.Rows[0].Cells[0].Value.ToString().Trim()))
                            {
                                this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                            }
                        }
                        else
                        {
                            this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                        }

                        if (this.GranteeGridView.OriginalRowCount > 0)
                        {
                            this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count.ToString()) - 1) + ")";
                            if (string.IsNullOrEmpty(this.GranteeGridView.Rows[0].Cells[0].Value.ToString().Trim()))
                            {
                                this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                            }
                        }
                        else
                        {
                            this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                        }

                        this.ControlLock(!this.PermissionFiled.newPermission);
                    }
                    else
                    {
                        this.ControlLock(true);
                    }
               // }
            }
            else
            {
                this.ControlLock(true);
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
            if (this != null && this.IsDisposed != true)
            {
                this.flagFormLoad = true;
                this.ClearGrantorGrid();
                this.ClearGranteeGrid();

                this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);
                this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();

                this.CustomizeGranteeGrid();
                this.CustomizeGrantorGrid();
                this.maxGridRowCount = this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count;

                if (this.maxGridRowCount <= this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count)
                {
                    this.maxGridRowCount = this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count;
                }

                this.PopulateRowInGrantorGranteeGrid();

                this.flagFormLoad = false;

                if (this.GrantorGridView.OriginalRowCount > 0)
                {
                    this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count.ToString()) - 1) + ")";
                    if (this.GrantorGridView.Columns[0].HeaderText.ToString() == "Grantor(0)")
                    {
                        this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                    }
                }
                else
                {
                    this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                }

                if (this.GranteeGridView.OriginalRowCount > 0)
                {
                    this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count.ToString()) - 1) + ")";
                    if (this.GranteeGridView.Columns[0].HeaderText.ToString() == "Grantee(0)")
                    {
                        this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                    }
                }
                else
                {
                    this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                }

                if (this.maxGridRowCount < this.GrantorGridView.RowCount)
                {
                    this.maxGridRowCount = this.GrantorGridView.RowCount;
                }

                // Get the Maxium Row Count Between Grantor and Grantee Grid 
                // And Assign the Form Height.
                if (this.GrantorGridView.Rows.Count >= this.GranteeGridView.Rows.Count)
               { 
                    this.SetFormHeight(this.GrantorGridView.Rows.Count);
                }
                else
                {
                    this.SetFormHeight(this.GranteeGridView.Rows.Count);
                }
                
               
               
               
                    this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
               

                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            if (this != null && this.IsDisposed != true)
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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
        /// Event Subscription for save confirmed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.PermissionFiled.deletePermission && this.keyId > 0)
                {
                    this.form49911Control.WorkItem.F49910_DeleteInstrumentHeader(this.keyId, TerraScan.Common.TerraScanCommon.UserId);
                }
            }
        }

        /// <summary>
        /// Event Subscription D84700_F84721_OnSave_GetKeyId.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    //if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit ) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
                    {
                        if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                        {
                            this.keyId = eventArgs.Data.SelectedKeyId;
                            this.SaveGrantorGranteeDetails();
                            
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            (this.GrantorGridView.Columns["Grantor"] as DataGridViewComboBoxColumn).DataPropertyName = "GrantorID";
                        }
                    }
                    else
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
                this.cancelKeyPress = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D49910_F49910_OnCopy_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnCopy_SetKeyId(object sender, DataEventArgs<SliceReloadParaMeterActiveRecord> eventArgs)
        {
            this.copyID = 1;
            string copyXML = eventArgs.Data.ParameterList;
            DataSet copyDataSet = new DataSet();
            this.saveGranteeDetails.Clear();
            this.saveGrantorDetails.Clear();

            copyDataSet.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(copyXML));

            if (copyDataSet.Tables.Count > 0)
            {
                if (copyDataSet.Tables[0].Rows.Count > 0)
                {
                    int.TryParse(copyDataSet.Tables[0].Rows[1]["Grantor"].ToString(), out this.grantorId);
                    int.TryParse(copyDataSet.Tables[0].Rows[2]["Grantee"].ToString(), out this.granteeId);

                    this.copyButton = true;
                    if (this.granteeId == 1)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));

                        /*Added By Ramya */
                        this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);

                        this.CustomizeGranteeGrid();

                        ////this.flagFormLoad = true;
                        this.PopulateRowInGrantorGranteeGrid();

                        this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                        if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                        {
                            this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                        }

                    }
                    else
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.New;
                            this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();
                            this.ClearGranteeGrid();
                            F49910InstrumentHeaderDataSet.F49911GranteeDetailsRow drow = this.form49911PartiesFieldData.F49911GranteeDetails.NewF49911GranteeDetailsRow();
                            if (this.form49911PartiesFieldData.F49911GranteeDetails.Columns.Contains(this.GranteeGridView.EmptyRecordColumnName))
                            {
                                drow[this.GranteeGridView.EmptyRecordColumnName] = false;
                            }

                            this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Add(drow);
                            this.SetHeightForGranteeGrid(0);

                            if (this.GranteeGridView.OriginalRowCount > 0)
                            {
                                this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count.ToString()) - 1) + ")";
                                if (string.IsNullOrEmpty(this.GranteeGridView.Rows[0].Cells[0].Value.ToString().Trim()))
                                {
                                    this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                                }
                            }
                            else
                            {
                                this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                            }

                            
                            this.ControlLock(!this.PermissionFiled.newPermission);
                            this.CustomizeGranteeGrid();
                        }
                        else
                        {
                            this.ControlLock(true);
                        }
                    }

                    if (this.grantorId == 1)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));

                        /*Added By Ramya*/

                        this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);

                        this.CustomizeGrantorGrid();
                        this.CustomizeGranteeGrid();
                        ////this.flagFormLoad = true;
                        this.PopulateRowInGrantorGranteeGrid();

                        this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                        if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                        {
                            this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                        }



                    }
                    else
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.New;
                            this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();
                            this.ClearGrantorGrid();
                            F49910InstrumentHeaderDataSet.F49911GrantorDetailsRow dr = this.form49911PartiesFieldData.F49911GrantorDetails.NewF49911GrantorDetailsRow();
                            if (this.form49911PartiesFieldData.F49911GrantorDetails.Columns.Contains(this.GrantorGridView.EmptyRecordColumnName))
                            {
                                dr[this.GrantorGridView.EmptyRecordColumnName] = false;
                            }

                            this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Add(dr);

                            this.SetHeightForGrantorGrid(0);

                            if (this.GrantorGridView.OriginalRowCount > 0)
                            {
                                this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count.ToString()) - 1) + ")";
                                if (string.IsNullOrEmpty(this.GrantorGridView.Rows[0].Cells[0].Value.ToString().Trim()))
                                {
                                    this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                                }
                            }
                            else
                            {
                                this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                            }

                            this.ControlLock(!this.PermissionFiled.newPermission);

                            this.CustomizeGrantorGrid();
                        }
                        else
                        {
                            this.ControlLock(true);
                        }
                    }


                }


                if (this.grantorId == 0 && this.granteeId == 0)
                {
                    this.SetFormHeight(0);
                }
                else
                {
                    this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                    if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                    {
                        this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                    }
                    else
                    {
                        this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                    }

                    this.SetFormHeight(this.maxGridRowCount);
                }

                SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
                sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            }

            this.copyButton = false;
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
        #endregion Event Subscription

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

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Load event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F49911_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                this.saveGrantorDetails.Columns.AddRange(new DataColumn[] { new DataColumn("GrantorId", System.Type.GetType("System.Int32")), new DataColumn("Grantor", System.Type.GetType("System.String")) });
                this.saveGranteeDetails.Columns.AddRange(new DataColumn[] { new DataColumn("GranteeId", System.Type.GetType("System.Int32")), new DataColumn("Grantee", System.Type.GetType("System.String")) });

                this.formattedGrantorValueTable.Columns.AddRange(new DataColumn[] { new DataColumn("GrantorId", System.Type.GetType("System.Int32")), new DataColumn("Grantor", System.Type.GetType("System.String")) });
                this.formattedGranteeValueTable.Columns.AddRange(new DataColumn[] { new DataColumn("GranteeId", System.Type.GetType("System.Int32")), new DataColumn("Grantee", System.Type.GetType("System.String")) });

                this.form49911PartiesFieldData.F49911GrantorDetails.Clear();
                this.form49911PartiesFieldData.F49911GranteeDetails.Clear();

                this.form49911PartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentHeaderDetails(this.keyId);
                this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();

                this.CustomizeGranteeGrid();
                this.CustomizeGrantorGrid();

                this.flagFormLoad = true;
                this.PopulateRowInGrantorGranteeGrid();
                this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                {
                    this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                }

                this.SetFormHeight(this.maxGridRowCount);

             

                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                this.flagFormLoad = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F49911_SelectedValueChanged(object sender, EventArgs e)
        {

            //(sender as DataGridViewComboBoxEditingControl).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //(sender as DataGridViewComboBoxEditingControl).AutoCompleteSource = AutoCompleteSource.ListItems;  
            // ((sender as DataGridViewComboBoxEditingControl) as ComboBox).DropDownStyle = ComboBoxStyle.DropDown;
        }

        /// <summary>
        /// Handles the MouseEnter event of the PartiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PartiesPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.PartiesToolTip.SetToolTip(this.PartiesPictureBox, Utility.GetFormNameSpace(this.Name));
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
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Control_Validated(object sender, EventArgs e)
        {
            try
            {



                if ((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl) != null)
                {
                    if (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent) != null)
                    {
                        if ((((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent).Parent).Name != string.Empty)
                        {
                            //if (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent as TerraScanDataGridView).Name != null)
                            //{
                            //this.editedGridView = (((sender as DataGridViewComboBoxEditingControl).Parent as Panel).Parent as TerraScanDataGridView).Name;
                            this.editedGridView = (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent).Parent).Name;
                            //}
                        }
                    }
                }
                else
                {
                    ////this.editedGridView = null;
                }

                if (this.editedGridView == "GrantorGridView")
                {
                    ////Modified by Biju on 23/Jul/2010 to fix #7204
                    if (String.IsNullOrEmpty((sender as DataGridViewComboBoxEditingControl).Text))
                        this.grantorEnteredText = this.grantorEditedString;
                    else
                        this.grantorEnteredText = (sender as DataGridViewComboBoxEditingControl).Text;
                    
                    ////Modified by Biju on 23/Jul/2010 to fix #7204
                    this.grantorEnteredText = this.grantorEnteredText.Trim().Replace("'", "'");
                    ////Modified by Biju on 23/Jul/2010 to fix #7204
                    DataRow[] temp = this.grantorList.Select("Grantor ='" + this.grantorEnteredText.Replace("'","''") + "'");

                    if (temp.Length > 0)
                    {
                        int.TryParse(temp[0]["GrantorID"].ToString(), out this.grantorSelectedTextIndex);
                    }
                    else
                    {
                        int existingGrantorID;
                        int.TryParse(this.GrantorGridView.CurrentCell.Value.ToString(), out existingGrantorID);

                        if (existingGrantorID > 0)
                        {
                            DataRow[] existingGrantorRow = this.grantorList.Select("GrantorID ='" + existingGrantorID.ToString() + "'");
                            if (existingGrantorRow.Length > 0)
                            {
                                existingGrantorRow[0][this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["Grantor"].ColumnName] = this.grantorEnteredText;
                            }

                            this.grantorSelectedTextIndex = existingGrantorID;
                        }
                        else
                        {

                            DataRow emptyGrantorRow = this.grantorList.NewRow();
                            emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["Grantor"].ColumnName] = this.grantorEnteredText;
                            emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = this.grantorList.Compute("Max(GrantorID)", "1=1");
                            int tempId;
                            int.TryParse(emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName].ToString(), out this.tempNewGrantorID);
                            this.tempNewGrantorID += 1;
                            emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = this.tempNewGrantorID;
                            this.grantorList.Rows.InsertAt(emptyGrantorRow, this.grantorList.Rows.Count);
                            //  this.grantorList.Rows.Add(emptyGrantorRow);
                            this.grantorSelectedTextIndex = this.grantorRetainedNegativeIndex;
                            this.grantorRetainedNegativeIndex++;
                        }

                        this.grantorList.AcceptChanges();
                        this.GrantorGridView.EditingControl.Text = this.grantorEnteredText;
                    }



                }
                else
                {
                    ////Modified by Biju on 23/Jul/2010 to fix #7204
                    this.granteeEnteredText = (sender as DataGridViewComboBoxEditingControl).Text.Trim().Replace("'", "'");

                    DataRow[] temp = this.GranteeList.Select("Name ='" + this.granteeEnteredText.Replace("'","''") + "'");
                    if (temp.Length > 0)
                    {
                        int.TryParse(temp[0]["GrantID"].ToString(), out this.granteeSelectedTextIndex);
                    }
                    else
                    {
                        int existingGranteeID;
                        int.TryParse(this.GranteeGridView.CurrentCell.Value.ToString(), out existingGranteeID);
                        if (existingGranteeID > 0)
                        {
                            DataRow[] existingGranteeRow = this.GranteeList.Select("GrantId ='" + existingGranteeID.ToString() + "'");
                            if (existingGranteeRow.Length > 0)
                            {
                                existingGranteeRow[0]["Name"] = this.granteeEnteredText;
                            }

                            this.granteeSelectedTextIndex = existingGranteeID;
                        }
                        else
                        {
                            DataRow emptyGranteeRow = this.GranteeList.NewRow();
                            emptyGranteeRow["Name"] = this.granteeEnteredText;
                            emptyGranteeRow["GrantID"] = this.GranteeList.Compute("Max(GrantID)", "1=1");
                            int tempId;
                            int.TryParse(emptyGranteeRow["GrantID"].ToString(), out this.tempNewGranteeID);
                            this.tempNewGranteeID += 1;
                            emptyGranteeRow["GrantID"] = this.tempNewGranteeID;
                            this.GranteeList.Rows.Add(emptyGranteeRow);
                            this.granteeSelectedTextIndex = this.granteeRetainedNegativeIndex;
                            this.granteeRetainedNegativeIndex++;
                        }

                        this.GranteeList.AcceptChanges();
                        this.GranteeGridView.EditingControl.Text = this.granteeEnteredText;
                    }
                }
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
            try
            {
                if ((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl) != null)
                {
                    this.grantorEnteredText = (sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).EditingControlFormattedValue.ToString();

                }
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
        private void Combobox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                //Reset grantor and grantee key pressed.
                this.granteeEnterKeyPressed = true;
                this.grantorEnterKeyPressed = true;

                int rowIndex = (sender as DataGridViewComboBoxEditingControl).EditingControlRowIndex;
                string comboboxText = (sender as DataGridViewComboBoxEditingControl).EditingControlFormattedValue.ToString();

                ////Commented by Biju on 20/nov/2009 to fix #4512
                ////if (!string.IsNullOrEmpty(comboboxText) && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
                ////Added by Biju on 20/nov/2009 to fix #4512
                if (!string.IsNullOrEmpty(comboboxText) && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit && !e.KeyValue.Equals(9) && !e.KeyValue.Equals(16) && !e.KeyValue.Equals(13))
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    if (this.GrantorGridView.CurrentCell != null)
                    {
                        (this.GrantorGridView.CurrentCell as DataGridViewComboBoxCell).AutoComplete = false;
                    }
                }

                /*Added By Ramya.D For BugID2460 */

                if ((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl) != null)
                {
                    if (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent) != null)
                    {
                        if ((((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent).Parent).Name != string.Empty)
                        {
                            //if (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent as TerraScanDataGridView).Name != null)
                            //{
                            //this.editedGridView = (((sender as DataGridViewComboBoxEditingControl).Parent as Panel).Parent as TerraScanDataGridView).Name;
                            this.editedGridView = (((sender as System.Windows.Forms.DataGridViewComboBoxEditingControl).Parent).Parent).Name;
                            //}
                        }
                    }
                }
                else
                {
                    ////this.editedGridView = null;
                }
                if (this.editedGridView == "GrantorGridView")
                {
                    this.grantorTabPressed = true;
                }
                else if (!this.granteeNewRowEnter)
                {
                    this.granteeTabPressed = true;
                }

                if (e.KeyCode == Keys.Enter)
                {

                    if (this.editedGridView == "GrantorGridView")
                    {
                        if (rowIndex >= this.GrantorGridView.Rows.Count - 1)
                        {
                            if (!string.IsNullOrEmpty(comboboxText) && comboboxText != "<<Select>>")
                            {
                                if (!string.IsNullOrEmpty(comboboxText.Trim()))
                                {
                                    F49910InstrumentHeaderDataSet.F49911GrantorDetailsRow dr = this.form49911PartiesFieldData.F49911GrantorDetails.NewF49911GrantorDetailsRow();
                                    if (this.form49911PartiesFieldData.F49911GrantorDetails.Columns.Contains(this.GrantorGridView.EmptyRecordColumnName))
                                    {
                                        dr[this.GrantorGridView.EmptyRecordColumnName] = false;
                                    }

                                    this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Add(dr);


                                    // this.GrantorGridView.Rows[this.GrantorGridView.Rows.Count].Cells["Grantor"].Selected = true;
                                    // Avoid last row Tab 
                                    grantorNewRowEntered = false;
                                    this.SetHeightForGrantorGrid(this.GrantorGridView.Rows.Count);
                                    this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                                    if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                                    {
                                        this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                                    }
                                    this.SetFormHeight(this.maxGridRowCount);
                                    // Set the focus to the next row 
                                    // when the user enters the Enter key
                                    TerraScanCommon.SetDataGridViewCellPosition(this.GrantorGridView, rowIndex + 1, 0);
                                    this.GrantorGridView.CurrentCell.Selected = true;
                                    this.grantorEnterKeyPressed = false;
                                    ////Added to fix #8188
                                    this.GrantorGridView.Focus();
                                }
                            }
                        }


                        if (this.GrantorGridView.OriginalRowCount > 0)
                        {
                            this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count.ToString()) - 1) + ")";
                            if (this.GrantorGridView.Columns[0].HeaderText.ToString() == "Grantor(0)")
                            {
                                this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                            }
                        }
                        else
                        {
                            this.GrantorGridView.Columns[0].HeaderText = "Grantor";
                        }

                        if (this.GrantorGridView.CurrentCell != null)
                        {
                            (this.GrantorGridView.CurrentCell as DataGridViewComboBoxCell).AutoComplete = false;
                        }
                        if (String.IsNullOrEmpty(this.grantorEnteredText))
                        {

                        }
                        this.cancelKeyPress = false;

                    }
                    else if (this.editedGridView == "GranteeGridView" && !this.granteeNewRowEnter)
                    {

                        if (rowIndex >= this.GranteeGridView.Rows.Count - 1)
                        {
                            if (!string.IsNullOrEmpty(comboboxText) && comboboxText != "<<Select>>")
                            {
                                if (!string.IsNullOrEmpty(comboboxText.Trim()))
                                {
                                    F49910InstrumentHeaderDataSet.F49911GranteeDetailsRow dr = this.form49911PartiesFieldData.F49911GranteeDetails.NewF49911GranteeDetailsRow();
                                    if (this.form49911PartiesFieldData.F49911GranteeDetails.Columns.Contains(this.GranteeGridView.EmptyRecordColumnName))
                                    {
                                        dr[this.GranteeGridView.EmptyRecordColumnName] = false;
                                    }

                                    this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Add(dr);

                                   this.granteeNewRowEntered  = false;
                                    
                                    //this.GranteeGridView.Rows[rowIndex + 1].Cells["Grantee"].Value = -1;
                                    this.SetHeightForGranteeGrid(this.GranteeGridView.Rows.Count);
                                    this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                                    if (this.GrantorGridView.Rows.Count > this.maxGridRowCount)
                                    {
                                        this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                                    }
                        
                                    this.SetFormHeight(this.maxGridRowCount);
                                    // Set the focus to the next row 
                                    // when the user enters the Enter key
                                    this.granteeEnterKeyPressed = false;
                                    TerraScanCommon.SetDataGridViewCellPosition(this.GranteeGridView, rowIndex + 1, 0);
                                    this.GranteeGridView.CurrentCell.Selected = true;
                                    ////Added to fix #8188
                                    this.GranteeGridView.Focus();
                                }
                            }
                        }
                 

                        if (this.GranteeGridView.OriginalRowCount > 0)
                        {
                            this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + (Convert.ToInt32(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count.ToString()) - 1) + ")";
                            if (this.GranteeGridView.Columns[0].HeaderText.ToString() == "Grantee(0)")
                            {
                                this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                            }
                        }
                        else
                        {
                            this.GranteeGridView.Columns[0].HeaderText = "Grantee";
                        }

                        this.cancelKeyPress = false;

                    }

                }



            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PartiesPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PartiesPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D49910.F49911"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Event Handlers

        #region GrantorGrid Events

        /// <summary>
        /// Handles the EditingControlShowing event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                ////this.granteeFlag = false;
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.Validated += new EventHandler(this.Control_Validated);

                // Checks the control is combobox
                if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
                {
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)e.Control).MaxLength = 50;
                    ((ComboBox)e.Control).KeyUp += new KeyEventHandler(this.Combobox_KeyDown);
                 
                    ((ComboBox)e.Control).TextUpdate += new EventHandler(F49911_TextUpdate);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(F49911_GrantorSelectionChangeCommitted);
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }


        }


        #endregion GrantorGrid Events

        #region GranteeGrid Events



        /// <summary>
        /// Handles the EditingControlShowing event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                ////this.granteeFlag = false;
                e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);
                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                //e.Control.Validated += new EventHandler(this.Control_Validated);

                // Checks the control is combobox
                if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
                {
                    ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)e.Control).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)e.Control).KeyUp += new KeyEventHandler(this.Combobox_KeyDown);
                    ((ComboBox)e.Control).MaxLength = 50;
                    //((ComboBox)e.Control).KeyPress += new KeyPressEventHandler(F49911_KeyPress);
                    ((ComboBox)e.Control).TextUpdate += new EventHandler(F49911_TextUpdate);
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(F49911_SelectionChangeCommitted);

                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextUpdate event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void F49911_TextUpdate(object sender, EventArgs e)
        {
            try
            {
                this.granteeTabPressed = true;
                this.granteeNewRowEnter = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void F49911_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.granteeTabPressed = true;
                this.granteeNewRowEnter = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the GrantorSelectionChangeCommitted event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void F49911_GrantorSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.grantorTabPressed = true;
                this.grantorNewRowEnter = false;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            } 
        }

        /// <summary>
        /// Handles the KeyPress event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        void F49911_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.granteeNewRowEnter = false;
                this.granteeTabPressed = true;
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            } 

        }

        /// <summary>
        /// Handles the GrantorKeyPress event of the F49911 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        void F49911_GrantorKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.grantorTabPressed = true;
                this.grantorNewRowEnter = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            } 
        }






        #endregion GranteeGrid Events

        #region PrivateMethods




        /// <summary>
        /// Populates the row in grantor grantee grid.
        /// </summary>
        private void PopulateRowInGrantorGranteeGrid()
        {
            if (this.GrantorGridView.OriginalRowCount >= 0)
            {
                this.ControlLock(false);
                this.SetHeightForGrantorGrid(0);
                this.SetFormHeight(0);
            }
            else
            {
                this.ControlLock(true);
                this.SetHeightForGrantorGrid(0);
                this.SetFormHeight(0);
            }

            if (this.GranteeGridView.OriginalRowCount >= 0)
            {
                this.ControlLock(false);
                this.SetHeightForGranteeGrid(0);
                this.SetFormHeight(0);
            }
            else
            {
                this.ControlLock(true);
                this.SetHeightForGranteeGrid(0);
                this.SetFormHeight(0);
            }

            // Checks for the copy button clicked
            if (this.copyButton)
            {
                // Its Copy Mode Then check for the new permission
                this.ControlLock(!this.PermissionFiled.newPermission || !this.formMasterPermissionNew );
            }
            else
            {
                // Not check for the Edit permission.
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }


            if (this.GrantorGridView.OriginalRowCount >= this.GrantorGridView.NumRowsVisible)
            {
                //if ((this.form49911PartiesFieldData.F49911GrantorDetails.Rows[this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count - 1][this.GrantorGridView.EmptyRecordColumnName].ToString))
                //{
                /*Row Count Check for BugID 2458 By Ramya.D */
                if (this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count > 0 && this.form49911PartiesFieldData.F49911GrantorDetails.Columns.Contains(this.GrantorGridView.EmptyRecordColumnName))
                {
                    if (!Convert.ToBoolean(this.form49911PartiesFieldData.F49911GrantorDetails.Rows[this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count - 1][this.GrantorGridView.EmptyRecordColumnName]))
                    {
                        this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Add(this.form49911PartiesFieldData.F49911GrantorDetails.NewRow());
                        this.form49911PartiesFieldData.F49911GrantorDetails.Rows[this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count - 1][this.GrantorGridView.EmptyRecordColumnName] = true;

                        this.SetHeightForGrantorGrid(this.GrantorGridView.Rows.Count);
                        this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + this.GrantorGridView.OriginalRowCount + ")";
                    }
                }
            }

            if (this.GranteeGridView.OriginalRowCount >= this.GranteeGridView.NumRowsVisible)
            {
                if (this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count > 0 && this.form49911PartiesFieldData.F49911GranteeDetails.Columns.Contains(this.GranteeGridView.EmptyRecordColumnName))
                {
                    if (!Convert.ToBoolean(this.form49911PartiesFieldData.F49911GranteeDetails.Rows[this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count - 1][this.GranteeGridView.EmptyRecordColumnName]))
                    {
                        this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Add(this.form49911PartiesFieldData.F49911GranteeDetails.NewRow());
                        this.form49911PartiesFieldData.F49911GranteeDetails.Rows[this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count - 1][this.GranteeGridView.EmptyRecordColumnName] = true;

                        this.SetHeightForGranteeGrid(this.GranteeGridView.Rows.Count);
                        this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + this.GranteeGridView.OriginalRowCount + ")";
                    }
                }
            }
        }

        /// <summary>
        /// Sets the height for grantor grid.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetHeightForGrantorGrid(int recordCount)
        {
            if (recordCount > 1)
            {
                int increment = ((recordCount - 1) * 22);
                this.GrantorGridView.Height = 43 + increment;
                this.GrantorGridpanel.Height = this.GrantorGridView.Height;
                this.GrantorGridView.NumRowsVisible = recordCount;
            }
            else
            {
                this.GridViewpanel.Height = 60;
                this.GrantorGridView.Height = 44;
                this.GrantorGridpanel.Height = this.GrantorGridView.Height - 1;
                this.Height = this.GridViewpanel.Height;
                this.GrantorGridView.NumRowsVisible = 1;
            }
        }

        /// <summary>
        /// Sets the height for grantee grid.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetHeightForGranteeGrid(int recordCount)
        {
            if (recordCount > 1)
            {
                int increment = ((recordCount - 1) * 22);
                this.GranteeGridView.Height = 43 + increment;
                this.GranteeGridpanel.Height = this.GranteeGridView.Height;
                this.GranteeGridView.NumRowsVisible = recordCount;
            }
            else
            {
                this.GridViewpanel.Height = 60;
                this.GranteeGridView.Height = 43;
                this.GranteeGridpanel.Height = this.GranteeGridView.Height; // 43;
                this.Height = this.GridViewpanel.Height;
                this.GranteeGridView.NumRowsVisible = 1;
            }
        }

        /// <summary>
        /// Sets the height of the form.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetFormHeight(int recordCount)
        {
            if (recordCount > 1)
            {
                int increment = ((recordCount - 1) * 22);
                this.GridViewpanel.Height = 60 + increment + 5;
                this.PartiesPictureBox.Height = this.GridViewpanel.Height;
                this.Height = this.PartiesPictureBox.Height;
                this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }
            else
            {
                this.GridViewpanel.Height = 60;
                this.PartiesPictureBox.Height = this.GridViewpanel.Height;
                this.Height = this.PartiesPictureBox.Height;
                this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.PartiesPictureBox.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.PartiesPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PartiesPictureBox.Height, this.PartiesPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }





              /// <summary>
        /// Gets the form master new permission.
        /// </summary>
        /// <returns>bool</returns>
        private bool GetFormMasterNewPermission()
        {
            if ((this.Parent != null) && (this.Parent.Parent != null) && (this.Parent.Parent.Parent != null))
            {
                if (this.Parent.Parent.Parent is BaseSmartPart)
                {
                    return ((BaseSmartPart)this.Parent.Parent.Parent).PermissionFiled.newPermission;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
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
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            this.GridViewpanel.Enabled = !controlLock;
            this.GrantorGridpanel.Enabled = !controlLock;
            this.GrantorGridView.Enabled = !controlLock;
            this.GranteeGridpanel.Enabled = !controlLock;
            this.GranteeGridView.Enabled = !controlLock;
        }

        /// <summary>
        /// Clears the grantor grid.
        /// </summary>
        private void ClearGrantorGrid()
        {
            this.form49911PartiesFieldData.F49911GrantorDetails.Clear();
            this.GrantorGridView.Refresh();
        }

        /// <summary>
        /// Clears the grantee grid.
        /// </summary>
        private void ClearGranteeGrid()
        {
            this.form49911PartiesFieldData.F49911GranteeDetails.Clear();
            this.GranteeGridView.Refresh();
        }

        /// <summary>
        /// Saves the grantor grantee details.
        /// </summary>
        /// <returns>saveResult</returns>
        private bool SaveGrantorGranteeDetails()
        {
            this.grantorFlag = true;
            bool saveResult = false;
            string errorMessage = string.Empty;

            // Temp Stores the dataSet


            this.form49911InstrumentPartiesFieldData = this.form49911Control.WorkItem.F49910_GetInstrumentTypeDetails();

            string s = this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.Compute("MAX(GrantID)", "").ToString();


            this.GrantorGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

            DataRow saveGrantorRow;

            if (this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.copyID == 0)
                {
                    this.saveGrantorDetails.Clear();
                }
            }

            int temp = 0;
            DataRow formattedGrantorValueRow;
            this.formattedGrantorValueTable.Clear();
            for (int i = 0; i <= this.GrantorGridView.Rows.Count - 1; i++)
            {
                /*Added By Ramya.D*/
                if (this.GrantorGridView.Rows[i].Cells[0].FormattedValue.ToString() != "<<Select>>")
                {
                    formattedGrantorValueRow = this.formattedGrantorValueTable.NewRow();
                    //if (string.IsNullOrEmpty(this.GrantorGridView[1, i].Value.ToString()))
                    //{
                    //    formattedGrantorValueRow[0] = 0;
                    //}
                    //else
                    //{

                    ////Added By Ramya For Duplicate check
                    DataView dv;


                    dv = new DataView(this.form49911PartiesFieldData.F49911GrantorDetails, "Grantor='" + this.GrantorGridView.Rows[i].Cells["Grantor"].Value.ToString() + "'", "", DataViewRowState.CurrentRows);

                    if (dv.Count <= 1)
                    {


                        if (!string.IsNullOrEmpty(this.GrantorGridView.Rows[i].Cells[1].Value.ToString()) && (!string.IsNullOrEmpty(s)))
                        {
                            if (Convert.ToInt32(this.GrantorGridView.Rows[i].Cells[1].Value.ToString()) > (Convert.ToInt32(s)))
                            {
                                formattedGrantorValueRow[0] = "0";
                            }
                            else
                            {
                                formattedGrantorValueRow[0] = this.GrantorGridView.Rows[i].Cells[1].Value;
                            }
                        }
                        else
                        {
                            formattedGrantorValueRow[0] = this.GrantorGridView.Rows[i].Cells[1].Value;
                        }

                        formattedGrantorValueRow[1] = this.GrantorGridView.Rows[i].Cells[0].FormattedValue.ToString();
                        this.formattedGrantorValueTable.Rows.Add(formattedGrantorValueRow);
                    }
                }
            }



            this.formattedGrantorValueTable.AcceptChanges();

            this.formattedGrantorValuesDS.Tables.Clear();
            this.formattedGrantorValuesDS.Tables.Add(this.formattedGrantorValueTable);
            string formattedgrantorValueDetails = this.formattedGrantorValuesDS.GetXml();

            DataRow formattedGranteeValueRow;
            this.formattedGranteeValueTable.Clear();
            for (int i = 0; i <= this.GranteeGridView.Rows.Count - 1; i++)
            {
                formattedGranteeValueRow = this.formattedGranteeValueTable.NewRow();
                ////if (string.IsNullOrEmpty(this.GranteeGridView[2, i].Value.ToString()))
                ////{
                ////    formattedGranteeValueRow[0] = 0;
                ////}
                ////else
                ////{
                formattedGranteeValueRow[0] = this.GranteeGridView.Rows[i].Cells[0].Value;
                ////}

                formattedGranteeValueRow[1] = this.GranteeGridView.Rows[i].Cells[0].FormattedValue.ToString();
                this.formattedGranteeValueTable.Rows.Add(formattedGranteeValueRow);
            }

            this.formattedGranteeValueTable.AcceptChanges();
            this.formattedGranteeValuesDS.Tables.Clear();
            this.formattedGranteeValuesDS.Tables.Add(this.formattedGranteeValueTable);
            string formattedgranteeValueDetails = this.formattedGranteeValuesDS.GetXml();

            this.saveConfirm = this.form49911Control.WorkItem.F49911_InsertPartiesFieldDetails(this.keyId, formattedgrantorValueDetails, formattedgranteeValueDetails, TerraScanCommon.UserId, this.copyID);

            this.copyID = 0;
            this.keyId = this.saveConfirm;
            saveResult = true;

            if (this.saveConfirm == -1)
            {
                //MessageBox.Show("Grantor/Grantee should be unique", "Terrascan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return saveResult;
            }

            return saveResult;
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            string messageMissingReqField = string.Empty; ////"You cannot save this record because it is missing required fields in FS Parties.";
            sliceValidationFields.FormNo = formNo;
            sliceValidationFields.ErrorMessage = string.Empty;
            sliceValidationFields.RequiredFieldMissing = false;

            if ((this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count > 0) || (this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count > 0))
            {
                // Remove the null grantor / Grantee id 
                // Inorder to execute LINQ Query
                this.form49911PartiesFieldData.F49911GrantorDetails.Rows.RemoveAt(this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count -1);
                this.form49911PartiesFieldData.F49911GranteeDetails.Rows.RemoveAt(this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count - 1);  

              // Use  Query to get the Unique Id  for the Grantor
                IEnumerable<Int32> grantntorUnique = (from row in this.form49911PartiesFieldData.F49911GrantorDetails.AsEnumerable()
                                                      select row.Field<int>("GrantorID")).Distinct();

              // Use  Query to get the Unique Id  for the Grantee
              IEnumerable<Int32> granteeUnique = (from row in this.form49911PartiesFieldData.F49911GranteeDetails.AsEnumerable()

                                                    select row.Field<int>("GranteeID")).Distinct();
              try
              {
                  // Compare unique  row count of  Grantor / Grantee with orignal grantor / Grantee count 
                  // to find out the Unique Value
                  if (grantntorUnique.Count() != this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count ||
                      granteeUnique.Count() != this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count)
                  {
                      // Add the empty row again.
                      this.form49911PartiesFieldData.F49911GrantorDetails.Rows.InsertAt(this.form49911PartiesFieldData.F49911GrantorDetails.NewRow(), this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count);
                      this.form49911PartiesFieldData.F49911GranteeDetails.Rows.InsertAt(this.form49911PartiesFieldData.F49911GranteeDetails.NewRow(), this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count);

                      // If not Raise the Meesage.
                      MessageBox.Show("Grantor/Grantee should be unique", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);

                      sliceValidationFields.ErrorMessage = string.Empty;
                      sliceValidationFields.RequiredFieldMissing = false;
                      sliceValidationFields.DisableNewMethod = true;

                  }
                  else
                  {
                      // Add the empty row again.
                      this.form49911PartiesFieldData.F49911GrantorDetails.Rows.InsertAt(this.form49911PartiesFieldData.F49911GrantorDetails.NewRow(), this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count);
                      this.form49911PartiesFieldData.F49911GranteeDetails.Rows.InsertAt(this.form49911PartiesFieldData.F49911GranteeDetails.NewRow(), this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count);

                  }
              }
              catch (Exception ex)
              {
              }
              

            /*to validate the missing fields in GRID.*/
            this.GrantorGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            this.GranteeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

           
                //if (string.IsNullOrEmpty(this.GrantorGridView[0, 0].FormattedValue.ToString()))
                //{
                //    sliceValidationFields.ErrorMessage = "You cannot save this record because it is missing required fields in FS Parties.";
                //    sliceValidationFields.RequiredFieldMissing = true;
                //    return sliceValidationFields;
                //}

                //if (string.IsNullOrEmpty(this.GranteeGridView[0, 0].FormattedValue.ToString()))
                //{
                //    sliceValidationFields.ErrorMessage = "You cannot save this record because it is missing required fields in FS Parties.";
                //    sliceValidationFields.RequiredFieldMissing = true;
                //    return sliceValidationFields;
                //}
            }

            return sliceValidationFields;
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void LockControls(bool controlLock)
        {
            this.GridViewpanel.Enabled = !controlLock;
            this.GrantorGridpanel.Enabled = !controlLock;
            this.GrantorGridView.Enabled = !controlLock;
            this.GranteeGridpanel.Enabled = !controlLock;
            this.GranteeGridView.Enabled = !controlLock;
        }
        #endregion PrivateMethods

        /// <summary>
        /// Handles the KeyDown event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    this.grantorNewRowEnter = false;
                    this.grantorTabPressed = true;
                }
                else
                {
                    this.grantorTabPressed = false;
                }

                if (e.KeyCode == Keys.Delete && this.GrantorGridView[this.GrantorGridView.CurrentCell.ColumnIndex, this.GrantorGridView.CurrentCell.RowIndex].Value.ToString() != "")
                {
                    this.GrantorGridView.AllowUserToDeleteRows = true;
                    this.EditEnabled();
                }
                else
                {
                    this.GrantorGridView.AllowUserToDeleteRows = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the UserDeletingRow event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowCancelEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                if (this.GrantorGridView[this.GrantorGridView.CurrentCell.ColumnIndex, this.GrantorGridView.CurrentCell.RowIndex].Value.ToString() != "")
                {
                    e.Cancel = false;
                    SetHeightForGrantorGrid(this.GrantorGridView.Rows.Count - 1);
                    if (this.GranteeGridView.Rows.Count > this.GrantorGridView.Rows.Count - 1)
                    {
                        this.SetFormHeight(this.GranteeGridView.Rows.Count);
                    }
                    else
                    {
                        this.SetFormHeight(this.GrantorGridView.Rows.Count - 1);
                    }

                    this.SetGrantorRowHeaderCountAfterDelete();

                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GrantorGridView.IsCurrentCellDirty)
                {

                    GrantorGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the CellLeave event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
                {
                if (this.GrantorGridView.CurrentRowIndex + 2 == this.GrantorGridView.Rows.Count && grantorNewRowEntered 
                     && this.grantorEnterKeyPressed )
                {
                    //In order avoid cell leave event fire when 
                    // Enter key is pressed.
                    this.grantorEnterKeyPressed = true;

                    //for avoid normal cell leave events
                    this.grantorNewRowEntered = false;

                    //focus to the grid
                    this.GrantorGridView.Focus();

                    //set focus to the next cell
                    SendKeys.Send("{Tab}");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the CellLeave event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.GranteeGridView.CurrentRowIndex + 2 == this.GranteeGridView.Rows.Count && granteeNewRowEntered
                        && this.granteeEnterKeyPressed )
                {
                    //In order avoid cell leave event fire when 
                    // Enter key is pressed.
                     this.granteeEnterKeyPressed = true;

                     //for avoid normal cell leave events
                     this.granteeNewRowEntered = false;

                     //focus to the grid
                    this.GranteeGridView.Focus();

                    //set focus to the next cell
                    SendKeys.Send("{Tab}");
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        #region GrantorGrid

        /// <summary>
        /// Customizes the grantor grantee grid.
        /// </summary>
        private void CustomizeGrantorGrid()
        {
            /*GrantorGrid*/
            this.form49911PartiesFieldData.F49911GrantorDetails.GrantorIDColumn.AllowDBNull = true;
            this.form49911PartiesFieldData.F49911GrantorDetails.GrantorIDColumn.DefaultValue = DBNull.Value;
            this.form49911PartiesFieldData.F49911GrantorDetails.GrantorColumn.AllowDBNull = true;
            this.form49911PartiesFieldData.F49911GrantorDetails.GrantorColumn.DefaultValue = DBNull.Value;
            this.GrantorGridView.AllowUserToResizeColumns = false;
            this.GrantorGridView.AllowUserToResizeRows = false;
            this.GrantorGridView.AutoGenerateColumns = false;
            this.Grantor.DataPropertyName = this.form49911PartiesFieldData.F49911GrantorDetails.GrantorIDColumn.ColumnName;
            this.GrantorID.DataPropertyName = this.form49911PartiesFieldData.F49911GrantorDetails.GrantorIDColumn.ColumnName;
            this.InstID.DataPropertyName = this.form49911PartiesFieldData.F49911GrantorDetails.InstIDColumn.ColumnName;
            this.Count.DataPropertyName = this.form49911PartiesFieldData.F49911GrantorDetails.CountColumn.ColumnName;
            this.GrantornameNew.DataPropertyName = this.form49911PartiesFieldData.F49911GrantorDetails.GrantorColumn.ColumnName;
            this.Grantor.AutoComplete = false;





            this.grantorList = this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.Copy();

            this.grantorList.Columns[0].ColumnName = "GrantorID";
            this.grantorList.Columns[0].AllowDBNull = true;
            this.grantorList.Columns[0].DefaultValue = DBNull.Value;

            this.grantorList.Columns[1].ColumnName = "Grantor";
            this.grantorList.Columns[1].AllowDBNull = true;
            this.grantorList.Columns[1].DefaultValue = DBNull.Value;

            DataRow emptyGrantorRow = this.grantorList.NewRow();
            emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["Grantor"].ColumnName] = DBNull.Value;
            emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = -1;

            this.grantorList.Rows.InsertAt(emptyGrantorRow, 0);


            (this.GrantorGridView.Columns["Grantor"] as DataGridViewComboBoxColumn).DataSource = null;
            (this.GrantorGridView.Columns["Grantor"] as DataGridViewComboBoxColumn).DataSource = this.grantorList; //// this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList;
            (this.GrantorGridView.Columns["Grantor"] as DataGridViewComboBoxColumn).DisplayMember = "Grantor"; //// this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.NameColumn.ColumnName;
            (this.GrantorGridView.Columns["Grantor"] as DataGridViewComboBoxColumn).ValueMember = "GrantorID";


            for (int i = 0; i < this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count; i++)
            {

                
                DataRow[] temp = this.grantorList.Select("Grantor ='" + this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["Grantor"].ToString().Trim().Replace("'", "''") + "'");

                if (temp.Length <= 0)
                {
                    
                    DataRow[] temp1 = this.form49911PartiesFieldData.F49911GrantorDetails.Select("Grantor ='" + this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["Grantor"].ToString().Trim().Replace("'", "''") + "'");
                    DataTable dt = new DataTable("tempTable");
                    dt.Columns.Clear();
                    dt.Columns.AddRange(new DataColumn[] { new DataColumn("GrantorID", System.Type.GetType("System.Int32")), new DataColumn("Grantor", System.Type.GetType("System.String")) });
                    DataRow dr = grantorList.NewRow();
                    string s = this.grantorList.Compute("MAX(GrantorID)", "").ToString();

                    dr[0] = Convert.ToInt32(s) + 1;
                    dr[1] = this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["Grantor"].ToString();

                    grantorList.Rows.InsertAt(dr, grantorList.Rows.Count - 1);
                }

                for (int j = 0; j < this.grantorList.Rows.Count; j++)
                {

                    if (this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["Grantor"].ToString() == this.grantorList.Rows[j]["Grantor"].ToString())
                    {
                        this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["GrantorID"] = this.grantorList.Rows[j]["GrantorID"].ToString();
                        break;
                    }
                }
            }

            if (this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count > 0)
            {
                this.GrantorGridView.NumRowsVisible = 0;
            }
            else
            {
                this.GrantorGridView.NumRowsVisible = 1;

            }


            this.GrantorGridView.DataSource = this.form49911PartiesFieldData.F49911GrantorDetails;

            this.GrantorGridView.NumRowsVisible = this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Count;




        }

        /// <summary>
        /// Sets the grantor row header count after delete.
        /// </summary>
        private void SetGrantorRowHeaderCountAfterDelete()
        {

            int grantorRowCount = Convert.ToInt32(this.GrantorGridView.Rows.Count) - 2;
            if (grantorRowCount >= 1)
            {
                // Count will be less than the empty row count
                this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + grantorRowCount + ")";
            }
            else
            {
                // Assign only the Name
                this.GrantorGridView.Columns[0].HeaderText = "Grantor";
            }
        }

        /// <summary>
        /// Sets the grantor row header count after add.
        /// </summary>
        private void SetGrantorRowHeaderCountAfterAdd()
        {


            int grantorRowCount = Convert.ToInt32(this.GrantorGridView.Rows.Count) - 1;

            if (grantorRowCount >= 1)
            {
                // Count will be less than the empty row count
                this.GrantorGridView.Columns[0].HeaderText = "Grantor(" + grantorRowCount + ")";
            }
            else
            {
                // Assign only the Name
                this.GrantorGridView.Columns[0].HeaderText = "Grantor";
            }
        }


        #endregion




        #region GranteeGrid
        /// <summary>
        /// Customizes the grantor grantee grid.
        /// </summary>
        private void CustomizeGranteeGrid()
        {


            this.granteeLoad = false;

            /*GranteeGrid*/
            this.GranteeGridView.AllowUserToResizeColumns = false;
            this.GranteeGridView.AllowUserToResizeRows = false;
            this.GranteeGridView.AutoGenerateColumns = false;
            //this.GranteeGridView.StandardTab = false;

            this.Grantee.DataPropertyName = this.form49911PartiesFieldData.F49911GranteeDetails.GranteeIDColumn.ColumnName;
            this.GranteeID.DataPropertyName = this.form49911PartiesFieldData.F49911GranteeDetails.GranteeIDColumn.ColumnName;
            this.InsntID.DataPropertyName = this.form49911PartiesFieldData.F49911GranteeDetails.InstIDColumn.ColumnName;
            this.GranteeCount.DataPropertyName = this.form49911PartiesFieldData.F49911GranteeDetails.CountColumn.ColumnName;
            this.GranteenameNew.DataPropertyName = this.form49911PartiesFieldData.F49911GranteeDetails.GranteeColumn.ColumnName;
            this.Grantee.AutoComplete = false;


            // Merge the grantlist to granteelist
            this.GranteeList.Rows.Clear();
            this.GranteeList.Merge(this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList);
            // this.GranteeList = this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList;

            this.GranteeList.Columns[this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.GrantIDColumn.ColumnName].AllowDBNull = true;
            // this.GranteeList.Columns[this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.GrantIDColumn.ColumnName].DefaultValue = -1;

            this.GranteeList.Columns[this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.NameColumn.ColumnName].AllowDBNull = true;
            // this.GranteeList.Columns[1].DefaultValue = string.Empty;
            //Code to insert the Empty row
            //Created new row 
            DataRow emptyGranteeRow = this.GranteeList.NewRow();

            //// Assign empty value to the name
            emptyGranteeRow[this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.GrantIDColumn.ColumnName] = -1;

            //// Assign -1 value to the grandId
            emptyGranteeRow[this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.NameColumn.ColumnName] = "";

            ////Add the Empty Row
            this.GranteeList.Rows.InsertAt(emptyGranteeRow, 0);




            for (int i = 0; i < this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count; i++)
            {
                
                DataRow[] temp = this.GranteeList.Select("Name ='" + this.form49911PartiesFieldData.F49911GranteeDetails.Rows[i]["Grantee"].ToString().Trim().Replace("'", "''") + "'");

                if (temp.Length <= 0)
                {
                    //DataRow[] temp1 = this.form49911PartiesFieldData.F49911GrantorDetails.Select("Grantor ='" + this.form49911PartiesFieldData.F49911GrantorDetails.Rows[i]["Grantor"].ToString().Trim().Replace("'", "''") + "'");
                    //DataTable dt = new DataTable("tempTable");
                    //dt.Columns.Clear();
                    //dt.Columns.AddRange(new DataColumn[] { new DataColumn("GrantorID", System.Type.GetType("System.Int32")), new DataColumn("Grantor", System.Type.GetType("System.String")) });
                    DataRow dr = GranteeList.NewRow();
                    string s = this.GranteeList.Compute("MAX(GrantID)", "").ToString();

                    dr[0] = Convert.ToInt32(s) + 1;
                    dr[1] = this.form49911PartiesFieldData.F49911GranteeDetails.Rows[i]["Grantee"].ToString();
                    GranteeList.Rows.Add(dr);

                    // GranteeList.Rows.InsertAt(dr, GranteeList.Rows.Count - 1);
                }


                for (int j = 0; j < this.GranteeList.Rows.Count; j++)
                {

                    if (this.form49911PartiesFieldData.F49911GranteeDetails.Rows[i]["Grantee"].ToString() == this.GranteeList.Rows[j][this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.NameColumn.ColumnName].ToString())
                    {
                        this.form49911PartiesFieldData.F49911GranteeDetails.Rows[i]["GranteeID"] = this.GranteeList.Rows[j][this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.GrantIDColumn.ColumnName].ToString();
                        break;
                    }

                }
            }

            if (this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count > 0)
            {
                this.GranteeGridView.NumRowsVisible = 0;
            }
            else
            {
                this.GranteeGridView.NumRowsVisible = 1;
            }


            // Apply Sort Criteria to the DataView
            this.GranteeList.DefaultView.Sort = "Name ASC"; ;
            //DataGrid.DataSource = ds.Tables[0].DefaultView

            (this.GranteeGridView.Columns["Grantee"] as DataGridViewComboBoxColumn).DataSource = this.GranteeList.DefaultView; //// this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList;
            (this.GranteeGridView.Columns["Grantee"] as DataGridViewComboBoxColumn).DisplayMember = "Name"; //// this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.NameColumn.ColumnName;
            (this.GranteeGridView.Columns["Grantee"] as DataGridViewComboBoxColumn).ValueMember = "GrantID"; //// this.form49911InstrumentPartiesFieldData.f49910_pclst_GrantList.GrantIDColumn.ColumnName;

            this.form49911PartiesFieldData.F49911GranteeDetails.GranteeIDColumn.AllowDBNull = true;


            this.GranteeGridView.DataSource = this.form49911PartiesFieldData.F49911GranteeDetails;
            this.GranteeGridView.NumRowsVisible = this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Count;

            this.GrantorGridpanel.TabIndex = 0;
            this.GrantorGridView.TabIndex = 1;
            this.GranteeGridpanel.TabIndex = 2;
            this.GranteeGridView.TabIndex = 3;
            this.Grantee.AutoComplete = false;

            this.granteeLoad = true;



        }



        /// <summary>
        /// Handles the CellValidating event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Declare a Row to a in Grantee Table
            DataRow granteeRow;

            if (this.GranteeGridView.CurrentCell.IsInEditMode && this.granteeTabPressed && e.FormattedValue.ToString() != "<<Select>>")
            {
                ////Modified by Biju on 23/Jul/2010 to fix #7204
                this.granteeEditedString = e.FormattedValue.ToString().Trim().Replace("'", "'");


                if (this.GranteeGridView.CurrentCell.GetType() ==
                typeof(DataGridViewComboBoxCell))
                {
                    DataGridViewComboBoxCell cell =
                    (
                    DataGridViewComboBoxCell)GranteeGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (!cell.Items.Contains(e.FormattedValue) && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {


                        // Finds the Enterd value already in GranteeList Table
                        DataRow[] existingGranteeRow = this.GranteeList.Select("Name ='" + this.granteeEditedString.Replace ("'","''") + "'");

                        // Checks already exist or not
                        if (existingGranteeRow.Length == 0)
                        {
                            // Get the Max id
                            string maxGrantId = this.GranteeList.Compute("MAX(GrantID)", "").ToString();

                            newValue = Convert.ToInt32(maxGrantId) + 1;

                            // Create new row
                            granteeRow = this.GranteeList.NewRow();

                            //Compute Maxium Id
                            granteeRow[0] = newValue;

                            // Assing the Newly grantee  name
                            granteeRow[1] = e.FormattedValue;

                            //Adds the row the table.
                            GranteeList.Rows.Add(granteeRow);

                            cell.Value = newValue;


                        }
                        else if (e.FormattedValue.ToString() != "<<Select>>")
                        {
                            if (existingGranteeRow.Length > 0)
                            {
                                newValue = Convert.ToInt32(existingGranteeRow[0][0].ToString());
                                cell.Value = newValue;
                            }
                        }
                    }
                }

            }
            else
            {
                this.granteeEditedString = string.Empty;
            }
        }

        /// <summary>
        /// Handles the CellValidated event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (this.GranteeGridView.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell) && this.granteeTabPressed && this.granteeEditedString != string.Empty)
                {
                    DataGridViewCell cell =this.GranteeGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    ////Added by Biju on 20/nov/2009 to fix #4512
                    if (cell.Value.Equals(newValue) && !this.GranteeGridView.Rows.Count.Equals(e.RowIndex + 1))
                        return;
                    ////till here
                    cell.Value = newValue;

                    // Adds a new Row GranteeTable

                    if (e.RowIndex >= this.GranteeGridView.Rows.Count - 1)
                    {
                        if (!string.IsNullOrEmpty(this.GranteeGridView[0, e.RowIndex].FormattedValue.ToString()))
                        {
                            if (!string.IsNullOrEmpty(this.GranteeGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                            {
                                F49910InstrumentHeaderDataSet.F49911GranteeDetailsRow dr = this.form49911PartiesFieldData.F49911GranteeDetails.NewF49911GranteeDetailsRow();
                                if (this.form49911PartiesFieldData.F49911GranteeDetails.Columns.Contains(this.GranteeGridView.EmptyRecordColumnName))
                                {
                                    dr[this.GranteeGridView.EmptyRecordColumnName] = false;
                                }

                                this.form49911PartiesFieldData.F49911GranteeDetails.Rows.Add(dr);


                                this.SetHeightForGranteeGrid(this.GranteeGridView.OriginalRowCount);

                                this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                                if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                                {
                                    this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                                }

                                this.SetFormHeight(this.maxGridRowCount);
                            }
                        }
                    }

                    // Method call to sets grantee grid row count
                    this.SetGranteeRowHeaderCountAfterAdd();

                    // Method call to sets the form into edit mode
                    this.EditEnabled();

                    // Reset the flag. 
                    this.granteeTabPressed = false;


                    this.GranteeGridView.ClearCurrentCellOnLeave = false;


                    // Sets the New Row 
                    this.granteeNewRowEnter = true;

                    this.granteeNewRowEntered = true;


                    this.GranteeGridView.Focus();




                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    this.granteeNewRowEnter = false;
                    this.granteeTabPressed = true;
                }
                else
                {
                    this.granteeTabPressed = false;
                }

                if (e.KeyCode == Keys.Delete && this.GranteeGridView[this.GranteeGridView.CurrentCell.ColumnIndex, this.GranteeGridView.CurrentCell.RowIndex].Value.ToString() != "")
                {
                    this.GranteeGridView.AllowUserToDeleteRows = true;////this.grantorList.Rows.RemoveAt(1);
                    this.EditEnabled();
                }
                else
                {
                    this.GranteeGridView.AllowUserToDeleteRows = false;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       

        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (!GranteeGridView.IsCurrentCellDirty)
                {
                    GranteeGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        /// <summary>
        /// Handles the UserDeletingRow event of the GranteeGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewRowCancelEventArgs"/> instance containing the event data.</param>
        private void GranteeGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                if (this.GranteeGridView[this.GranteeGridView.CurrentCell.ColumnIndex, this.GranteeGridView.CurrentCell.RowIndex].Value.ToString() != "")
                {
                    e.Cancel = false;
                    this.SetHeightForGranteeGrid(this.GranteeGridView.Rows.Count - 1);
                    if (this.GrantorGridView.Rows.Count > this.GranteeGridView.Rows.Count - 1)
                    {
                        this.SetFormHeight(this.GrantorGridView.Rows.Count);
                    }
                    else
                    {
                        this.SetFormHeight(this.GranteeGridView.Rows.Count - 1);
                    }

                    SetGranteeRowHeaderCountAfterDelete();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the grantee row header count after delete.
        /// </summary>
        private void SetGranteeRowHeaderCountAfterDelete()
        {

            int GranteeRowCount = Convert.ToInt32(GranteeGridView.Rows.Count) - 2;
            if (GranteeRowCount >= 1)
            {
                // Count will be less than the empty row count
                this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + GranteeRowCount + ")";
            }
            else
            {
                // Assign only the Name
                this.GranteeGridView.Columns[0].HeaderText = "Grantee";
            }
        }


        /// <summary>
        /// Sets the grantee row header count after add.
        /// </summary>
        private void SetGranteeRowHeaderCountAfterAdd()
        {


            int GranteeRowCount = Convert.ToInt32(GranteeGridView.Rows.Count) - 1;

            if (GranteeRowCount >= 1)
            {
                // Count will be less than the empty row count
                this.GranteeGridView.Columns[0].HeaderText = "Grantee(" + GranteeRowCount + ")";
            }
            else
            {
                // Assign only the Name
                this.GranteeGridView.Columns[0].HeaderText = "Grantee";
            }
        }
        #endregion

        /// <summary>
        /// Handles the CellValidating event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                // Declare a Row to a in Grantor Table
                DataRow grantorRow;

                if (this.GrantorGridView.CurrentCell.IsInEditMode && this.grantorTabPressed && e.FormattedValue.ToString() != "<<Select>>")
                {
                    ////Modified by Biju on 23/Jul/2010 to fix #7204
                    this.grantorEditedString = e.FormattedValue.ToString().Trim().Replace("'", "'");

                    if (this.GrantorGridView.CurrentCell.GetType() ==
                    typeof(DataGridViewComboBoxCell))
                    {
                        DataGridViewComboBoxCell cell =
                        (
                        DataGridViewComboBoxCell)GrantorGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        if (!cell.Items.Contains(e.FormattedValue) && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                        {



                            // Finds the Enterd value already in GranteeList Table
                            ////Modified by Biju on 23/Jul/2010 to fix #7204
                            DataRow[] existingGrantorRow = this.grantorList.Select("Grantor ='" + grantorEditedString.Replace("'","''") + "'");

                            // Checks already exist or not
                            if (existingGrantorRow.Length == 0)
                            {
                                // Get the Max id
                                string maxGrantId = this.grantorList.Compute("MAX(GrantorID)", "").ToString();

                                newGrantorValue = Convert.ToInt32(maxGrantId) + 1;

                                // Create new row
                                grantorRow = this.grantorList.NewRow();

                                //Compute Maxium Id
                                grantorRow[0] = newGrantorValue;

                                // Assing the Newly grantee  name
                                grantorRow[1] = e.FormattedValue;

                                //Adds the row the table.
                                grantorList.Rows.Add(grantorRow);

                                cell.Value = newGrantorValue;
                                

                            }
                            else if (e.FormattedValue.ToString() != "<<Select>>")
                            {
                                newGrantorValue = Convert.ToInt32(existingGrantorRow[0][0].ToString());
                                cell.Value = newGrantorValue;
                            }


                        }
                    }
                }
                else
                {
                    this.grantorEditedString = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellValidated event of the GrantorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void GrantorGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              
                if (this.GrantorGridView.CurrentCell.GetType() == typeof(DataGridViewComboBoxCell) && this.grantorTabPressed && this.grantorEditedString != string.Empty)
                {
                    DataGridViewCell cell = this.GrantorGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    ////Added by Biju on 20/nov/2009 to fix #4512
                    if (cell.Value.Equals(newGrantorValue) && !this.GrantorGridView.Rows.Count.Equals(e.RowIndex+1))
                        return;
                    ////till here
                    //Assing new value to the cell
                    cell.Value = newGrantorValue;

                    // Adds a new Row GranteeTable
                    if (e.RowIndex >= this.GrantorGridView.Rows.Count - 1)
                    {
                        if (!string.IsNullOrEmpty(this.GrantorGridView[0, e.RowIndex].FormattedValue.ToString()) && this.GrantorGridView[0, e.RowIndex].FormattedValue.ToString() != "<<Select>>")
                        {
                            if (!string.IsNullOrEmpty(this.GrantorGridView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()))
                            {
                                F49910InstrumentHeaderDataSet.F49911GrantorDetailsRow dr = this.form49911PartiesFieldData.F49911GrantorDetails.NewF49911GrantorDetailsRow();
                                if (this.form49911PartiesFieldData.F49911GrantorDetails.Columns.Contains(this.GrantorGridView.EmptyRecordColumnName))
                                {
                                    dr[this.GrantorGridView.EmptyRecordColumnName] = false;
                                }
                                this.form49911PartiesFieldData.F49911GrantorDetails.Rows.Add(dr);
                                this.SetHeightForGrantorGrid(this.GrantorGridView.Rows.Count);
                                this.maxGridRowCount = this.GrantorGridView.Rows.Count;
                                if (this.GranteeGridView.Rows.Count > this.maxGridRowCount)
                                {
                                    this.maxGridRowCount = this.GranteeGridView.Rows.Count;
                                }
                                
                                this.SetFormHeight(this.maxGridRowCount);
                                
                                 
                                
                            }
                        }

                        grantorNewRowEntered = true;
                    }


                    this.SetGrantorRowHeaderCountAfterAdd();

                    this.EditEnabled();

                    this.GrantorGridView.ClearCurrentCellOnLeave = false;

                    this.grantorNewRowEnter = true;

                    this.grantorTabPressed = false;

                    this.GrantorGridView.Focus();

                    // Code commented fo fix TFSID:#12304 - TSBG - 49911 Grantor and Grantee information not saving
                    //cell.Value = this.grantorEditedString;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

       
    }
}