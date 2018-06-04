//--------------------------------------------------------------------------------------------
// <copyright file="F49912.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49912-LegalListing .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13/02/2008       KUPPUSAMY.B         Created
//***********************************************************************************************/


namespace D49910
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
    using Infragistics.Win.UltraWinGrid;
    using Infrastructure.Interface;

    /// <summary>
    /// F49912 Class File
    /// </summary>
    public partial class F49912 : BaseSmartPart
    {
        #region Private Members

        /// <summary>
        /// Checks the copy button Clicked
        /// </summary>
        private bool copyButton;

        /// <summary>
        /// formMasterPermissionNew
        /// </summary>
        private bool formMasterPermissionNew;

        /// <summary>
        /// valveId or keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Variable for copyID
        /// </summary>
        private int copyID = 0;

        /// <summary>
        /// F49912Controller
        /// </summary>
        private F49912Controller form49912Controller;

        /////// <summary>
        /////// F49910InstrumentHeaderDataSet
        /////// </summary>
        ////private F49910InstrumentHeaderDataSet form49912LegalData = new F49910InstrumentHeaderDataSet();

        /// <summary>
        /// form49912LegalData
        /// </summary>
        private F49912LegalData form49912LegalData = new F49912LegalData();

        /// <summary>
        /// F49912LegalData
        /// </summary>
        private F49912LegalData formLegalComboData = new F49912LegalData();

        /// <summary>
        /// F49910InstrumentHeaderDataSet
        /// </summary>
        private F49910InstrumentHeaderDataSet form49912InstrumentLegalComboData = new F49910InstrumentHeaderDataSet();

        /////// <summary>
        /////// F49912LegalFieldListingDataTable
        /////// </summary>
        ////private F49910InstrumentHeaderDataSet.F49912LegalFieldListingDataTable f49912legalfieldDatatable = new F49910InstrumentHeaderDataSet.F49912LegalFieldListingDataTable();

        /// <summary>
        /// PageModeTypes
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Bool value for flagFormLoad
        /// </summary>
        private bool flagFormLoad;

        /// <summary>
        /// recordCount
        /// </summary>
        private int recordCount;

        /// <summary>
        /// saveConfirm
        /// </summary>
        private int saveConfirm;

        /// <summary>
        /// Used to store commentsReturnText
        /// </summary>
        private string commentsReturnText;

        private int usercontrolRecordCount;

        private bool secgridedit;

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

        /// <summary>
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

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

        #endregion Form Slice Variables

        #endregion Private Members

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F49912"/> class.
        /// </summary>
        public F49912()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// F49911s the specified masterform.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F49912(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();

            this.LegalGridUserControl.SectionGridEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.EditEventHandler(this.LegalGridUserControl_SectionGridEdit);
            this.LegalGridUserControl.SectionGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridEditEndEventHandler(LegalGridUserControl_SectionGridEndEdit);
            ////this.LegalGridUserControl.NWGridBeginEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.EditEventHandler1(this.LegalGridUserControl_NWGridBeginEdit);
            ////this.LegalGridUserControl.NEGridBeginEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.EditEventHandler2(this.LegalGridUserControl_NEGridBeginEdit);
            ////this.LegalGridUserControl.SWGridBeginEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.EditEventHandler3(this.LegalGridUserControl_SWGridBeginEdit);
            ////this.LegalGridUserControl.SEGridBeginEdit +=new TerraScan.FSLegalGrid.FSLegalGridUserControl.EditEventHandler4(LegalGridUserControl_SEGridBeginEdit);
            //Added by Biju on 16-Nov-2010 to implement the keypress event for NE,SE,NW,SW grids
            this.LegalGridUserControl.NEKeyPressEvent +=new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridKeyPressEventHandler(LegalGridUserControl_NEKeyPressEvent);
            this.LegalGridUserControl.NWKeyPressEvent +=new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridKeyPressEventHandler(LegalGridUserControl_NWKeyPressEvent);
            this.LegalGridUserControl.SEKeyPressEvent +=new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridKeyPressEventHandler(LegalGridUserControl_SEKeyPressEvent);
            this.LegalGridUserControl.SWKeyPressEvent +=new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridKeyPressEventHandler(LegalGridUserControl_SWKeyPressEvent);
            //till here
            this.LegalGridUserControl.SectionGridRowDeleteEvent += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridRowCancelEventHandler(this.LegalGridUserControl_SectionGridRowDeleteEvent);

            ////this.LegalGridUserControl.SectionGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SectionGridEditEndEventHandler(this.FsLegalGridUserControl_SectionGridEndEdit);
            this.LegalGridUserControl.NEGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NEGridEditEndEventHandler(this.FsLegalGridUserControl_NEGridEndEdit);
            this.LegalGridUserControl.NWGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.NWGridEditEndEventHandler(this.FsLegalGridUserControl_NWGridEndEdit);
            this.LegalGridUserControl.SWGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SWGridEditEndEventHandler(this.FsLegalGridUserControl_SWGridEndEdit);
            this.LegalGridUserControl.SEGridEndEdit += new TerraScan.FSLegalGrid.FSLegalGridUserControl.SEGridEditEndEventHandler(this.FsLegalGridUserControl_SEGridEndEdit);


            this.LegalGridUserControl.MultirowSetting = true;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.sectionIndicatorText = tabText;
            this.formMasterPermissionEdit = permissionEdit;
            this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, red, green, blue);

            this.LegalGridUserControl.LoadCommentsImage();
        }



        #endregion Constructor

        #region Eventpublication

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
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

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

        /// <summary>
        /// Declare the event D84700_F84722_OnSave_SetKeyId
        /// </summary>
        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        #endregion Eventpublication

        #region Property

        /// <summary>
        /// Gets or sets the F49912 control.
        /// </summary>
        /// <value>The F49912 control.</value>
        [CreateNew]
        public F49912Controller F49912Control
        {
            get { return this.form49912Controller as F49912Controller; }
            set { this.form49912Controller = value; }
        }

        #endregion Property

        #region EventSubscription

        #region GetSlicePermission
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
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                        this.formMasterPermissionNew = this.GetFormMasterNewPermission();

                        ////if (this.form49912LegalData.SubDivisionTable.Rows[0][this.form49912LegalData.SubDivisionTable.SubIDColumn].Count > 0)
                        ////{
                        ////    eventArgs.Data.FlagInvalidSliceKey = false;
                        ////}
                        ////else
                        ////{
                        ////    eventArgs.Data.FlagInvalidSliceKey = true;
                        ////}
                    }

                    /*To load the Comments image on Load */
                    DataGridView commentsGridView = (this.LegalGridUserControl.Controls[0].Controls[9].Controls[1] as DataGridView);

                    for (int i = 0; i < commentsGridView.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(commentsGridView[1, i].Value.ToString()))
                        {
                            commentsGridView[0, i].Value = Properties.Resources.GREEN_copy;
                        }
                        else
                        {
                            commentsGridView[0, i].Value = Properties.Resources.RED_copy; ////@"C:\Documents and Settings\kuppusamyb\Desktop\ciamge\C.jpg";
                        }
                    }

                    if (this.form49912LegalData.SubDivisionTable.Rows.Count >= 1)
                    {
                        this.ControlLock(false);
                    }
                    // Checks for the copy button clicked
                    if (this.copyButton)
                    {
                        // Its Copy Mode Then check for the new permission
                        this.ControlLock(!this.PermissionFiled.newPermission || !this.formMasterPermissionNew);
                    }
                    else
                    {
                        // Not check for the Edit permission.
                        this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    }
                   // this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion GetSlicePermission

        #region EnableNewMethod

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                {
                    if (this.slicePermissionField.newPermission)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(-99);
                        this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

                        /* Added By Ramya For adding empty row */
                        DataTable masterTable = new DataTable();
                        masterTable.Clear();
                        masterTable.Columns.Clear();
                        masterTable.Columns.AddRange(new DataColumn[] { new DataColumn("SubID", System.Type.GetType("System.Int32")), new DataColumn("SubName", System.Type.GetType("System.String")) });
                        DataRow emptyGrantorRow = masterTable.NewRow();
                        emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["Subname"].ColumnName] = "";
                        emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["SubID"].ColumnName] = -1;
                        //// emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = -99;
                        masterTable.Rows.Add(emptyGrantorRow);
                        this.formLegalComboData.F49912SubDivisionComboTable.Merge(masterTable);
                        /*Till here */

                        this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable);
                        this.usercontrolRecordCount = this.LegalGridUserControl.RecordCountIndicator;
                        this.SetHeightForForm(0);
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                        this.ControlLock(!this.PermissionFiled.newPermission);
                        this.form49912LegalData.CommentsDetailsTable.Rows.Clear();
                    }
                    else
                    {
                        this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(-99);
                        this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();
                        this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable);

                        this.ControlLock(true);
                    }
                }
            }
            else
            {
                this.ControlLock(true);
            }
        }

        #endregion EnableNewMethod

        #region SaveSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
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
                    ////this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
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

        #endregion SaveSliceInformation

        #region SaveConfirmed

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this != null && this.IsDisposed != true)
            {
                ////this.SaveLegalGridDetails();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }
        #endregion SaveConfirmed

        #region FormSlice_OnSave_GetKeyId
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
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit ) || (this.pageMode == TerraScanCommon.PageModeTypes.New ))
                    {
                        if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                        {
                            this.keyId = eventArgs.Data.SelectedKeyId;
                            this.SaveLegalGridDetails();
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                        }
                    }
                    else
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion FormSlice_OnSave_GetKeyId

        #region FormSlice_OnCopy_SetKeyId
        /// <summary>
        /// Handles the SetKeyId event of the FormSlice_OnCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadParaMeterActiveRecord&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D49910_F49910_OnCopy_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnCopy_SetKeyId(object sender, DataEventArgs<SliceReloadParaMeterActiveRecord> eventArgs)
        {
            this.copyID = 1;
            string copyXML = eventArgs.Data.ParameterList;
            DataSet copyDataSet = new DataSet();
            copyDataSet.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(copyXML));
            if (copyDataSet.Tables.Count > 0)
            {
                if (copyDataSet.Tables[0].Rows.Count > 0)
                {
                    int grantorId = 0;
                    int granteeId = 0;
                    int legalId = 0;

                    this.copyButton = true;

                    int.TryParse(copyDataSet.Tables[0].Rows[3]["Legal"].ToString(), out legalId);

                    //// For Legal
                    if (legalId == 1)
                    {
                        this.LegalGridUserControl.Enabled = true;
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));

                        this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(this.keyId);
                        this.AddEmptyRowOnLoad();
                        this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

                        this.CustomizeLegalGridUserControl();
                        if (this.form49912LegalData.SubDivisionTable.Rows.Count == 0)
                        {
                            this.InsertEmptyRow();
                        }

                        this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable, this.form49912LegalData);
                        this.usercontrolRecordCount = this.LegalGridUserControl.RecordCountIndicator;
                        this.recordCount = this.form49912LegalData.SubDivisionTable.Rows.Count;
                        this.SetHeightForForm(this.recordCount);
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                        this.LegalGridUserControl.LoadCommentsImage();
                        // Checks for the copy button clicked
                        if (this.copyButton)
                        {
                            // Its Copy Mode Then check for the new permission
                            this.ControlLock(!this.PermissionFiled.newPermission || !this.formMasterPermissionNew);
                        }
                        else
                        {
                            // Not check for the Edit permission.
                            this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                        }
                       // this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);


                    }
                    else
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.pageMode = TerraScanCommon.PageModeTypes.New;

                            this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(this.keyId);
                            this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

                            this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable);
                            this.AddEmptyRowOnLoad();
                            this.SetHeightForForm(0);
                            SliceResize sliceResize;
                            sliceResize.MasterFormNo = this.masterFormNo;
                            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                            sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                            this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                            this.ControlLock(!this.PermissionFiled.newPermission);
                        }
                        else
                        {
                            this.ControlLock(true);
                        }
                    }


                }
            }
            this.copyButton = false;
        }

        #endregion FormSlice_OnCopy_SetKeyId

        #region CancelSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.flagFormLoad = true;
            /// this.extraspaceflag = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
            {
                this.ControlLock(false);
            }
            else
            {
                this.ControlLock(true);
            }

            this.ClearDataTables();

            this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(this.keyId);
            this.AddEmptyRowOnLoad();
            this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

            /* Added By Ramya For adding empty row */

            DataTable masterTable = new DataTable();
            masterTable.Clear();
            masterTable.Columns.Clear();
            masterTable.Columns.AddRange(new DataColumn[] { new DataColumn("SubID", System.Type.GetType("System.Int32")), new DataColumn("SubName", System.Type.GetType("System.String")) });
            DataRow emptyGrantorRow = masterTable.NewRow();
            emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["Subname"].ColumnName] = "";
            emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["SubID"].ColumnName] = -1;
            //// emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = -99;
            masterTable.Rows.Add(emptyGrantorRow);
            this.formLegalComboData.F49912SubDivisionComboTable.Merge(masterTable);

            /*Till Here*/

            this.CustomizeLegalGridUserControl();
            if (this.form49912LegalData.SubDivisionTable.Rows.Count == 0)
            {
                this.InsertEmptyRow();
            }

            this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable, this.form49912LegalData);
            this.SetHeightForForm(this.form49912LegalData.SubDivisionTable.Rows.Count);
            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            this.flagFormLoad = false;
        }

        #endregion CancelSliceInformation

        #region DeleteSliceInformation

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this.PermissionFiled.deletePermission && this.keyId > 0)
                {
                    this.form49912Controller.WorkItem.F49910_DeleteInstrumentHeader(this.keyId, TerraScan.Common.TerraScanCommon.UserId);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #endregion DeleteSliceInformation

        #region LoadSliceDetails

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.flagFormLoad = true;
                // this.extraspaceflag = true;
                this.ClearDataTables();
                this.ControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);

                ////this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(this.keyId);
                this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

                /* Added By Ramya For adding empty row */
                DataTable masterTable = new DataTable();
                masterTable.Clear();
                masterTable.Columns.Clear();
                masterTable.Columns.AddRange(new DataColumn[] { new DataColumn("SubID", System.Type.GetType("System.Int32")), new DataColumn("SubName", System.Type.GetType("System.String")) });
                DataRow emptyGrantorRow = masterTable.NewRow();
                emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["Subname"].ColumnName] = "";
                emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["SubID"].ColumnName] = -1;
                //// emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = -99;
                masterTable.Rows.Add(emptyGrantorRow);
                this.formLegalComboData.F49912SubDivisionComboTable.Merge(masterTable);
                /*Till here */


                this.CustomizeLegalGridUserControl();

                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.AddEmptyRowOnLoad();
                this.LegalGridUserControl.LoadCommentsImage();
                this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable, this.form49912LegalData);

                this.SetHeightForForm(this.form49912LegalData.SubDivisionTable.Rows.Count);
                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.flagFormLoad = false;
            }
        }

        #endregion LoadSliceDetails

        #region ReloadAfterSave

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

        #endregion ReloadAfterSave

        #endregion EventSubscription

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

        #region Event Handlers

        /// <summary>
        /// Handles the Load event of the F49912 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F49912_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageMode = TerraScanCommon.PageModeTypes.View;

                this.form49912LegalData = this.form49912Controller.WorkItem.F49912_ListLegalField(this.keyId);
                this.AddEmptyRowOnLoad();
                this.formLegalComboData = this.form49912Controller.WorkItem.F49912_ListSubDivisionCombo();

                /* Added By Ramya For adding empty row */
                DataTable masterTable = new DataTable();
                masterTable.Clear();
                masterTable.Columns.Clear();
                masterTable.Columns.AddRange(new DataColumn[] { new DataColumn("SubID", System.Type.GetType("System.Int32")), new DataColumn("SubName", System.Type.GetType("System.String")) });
                DataRow emptyGrantorRow = masterTable.NewRow();
                emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["Subname"].ColumnName] = "";
                emptyGrantorRow[this.formLegalComboData.F49912SubDivisionComboTable.Columns["SubID"].ColumnName] = -1;
                //// emptyGrantorRow[this.form49911PartiesFieldData.Tables["F49911GrantorDetails"].Columns["GrantorID"].ColumnName] = -99;
                masterTable.Rows.Add(emptyGrantorRow);
                this.formLegalComboData.F49912SubDivisionComboTable.Merge(masterTable);
                /*Till Here */
                this.CustomizeLegalGridUserControl();
                if (this.form49912LegalData.SubDivisionTable.Rows.Count == 0)
                {
                    this.InsertEmptyRow();
                }

                this.LegalGridUserControl.fillData(this.formLegalComboData.F49912SubDivisionComboTable, this.form49912LegalData);
                this.usercontrolRecordCount = this.LegalGridUserControl.RecordCountIndicator;
                this.recordCount = this.form49912LegalData.SubDivisionTable.Rows.Count;
                this.SetHeightForForm(this.recordCount);

                SliceResize sliceResize;
                sliceResize.MasterFormNo = this.masterFormNo;
                sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                this.LegalGridUserControl.LoadCommentsImage();

                this.ControlLock(!this.slicePermissionField.editPermission || !this.formMasterPermissionEdit);

                this.LegalGridUserControl.InvokeScrollEvent();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LegalPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LegalPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D49910.F49912"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the LegalPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LegalPictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                this.LegalToolTip.SetToolTip(this.LegalPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CommandImageCellClick event of the LegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void LegalGridUserControl_CommandImageCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView commentsGridView = (this.LegalGridUserControl.Controls[0].Controls[9].Controls[1] as DataGridView);
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    Form commentsForm = new Form();

                    object[] optionalParameter = new object[] { };
                    commentsForm = this.form49912Controller.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(4992, null, this.form49912Controller.WorkItem);

                    if (this.form49912LegalData.CommentsDetailsTable.Rows.Count > e.RowIndex)
                    {
                        if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                        {
                            if (e.RowIndex > 1)
                            {
                                TerraScanCommon.SetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"), this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0].ToString());
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(commentsGridView[1, e.RowIndex].Value.ToString()))
                                {
                                    TerraScanCommon.SetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"), this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0].ToString());
                                }
                            }
                        }
                        else
                        {
                            //if (commentsGridView.Rows.Count - 1 >= this.form49912LegalData.CommentsDetailsTable.Rows.Count - 1)
                            if (this.form49912LegalData.CommentsDetailsTable.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0].ToString()))
                                {
                                    TerraScanCommon.SetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"), this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0].ToString());
                                }
                            }
                        }
                    }

                    if (commentsForm.ShowDialog() == DialogResult.OK)
                    {
                        // Coding Added for the issue 4678 on 21/5/2009 by malliga
                        // if you click C button save  and cancel should get enabled.after clikcing the ok button only it should get enabled.

                        this.EditEnabled();
                        this.commentsReturnText = TerraScanCommon.GetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"));
                        ////this.form49912LegalData.CommentsDetailsTable.Merge((commentsGridView.DataSource as DataSet).Tables[0]);    
                        if (e.RowIndex >= this.form49912LegalData.CommentsDetailsTable.Rows.Count - 1)
                        {
                            commentsGridView[1, e.RowIndex].Value = this.commentsReturnText;
                            DataRow commentSRow = this.form49912LegalData.CommentsDetailsTable.NewRow();
                            this.form49912LegalData.CommentsDetailsTable.Rows.Add(commentSRow);

                            //commentSRow[0] = this.commentsReturnText;
                            //this.form49912LegalData.CommentsDetailsTable.Rows.Add(commentSRow);
                            //if (this.form49912LegalData.CommentsDetailsTable.Rows.Count == 0)
                            //{
                            //    this.form49912LegalData.CommentsDetailsTable.Rows.Add(commentSRow); 
                            //}
                            this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0] = this.commentsReturnText;

                            if ((string.IsNullOrEmpty(this.form49912LegalData.CommentsDetailsTable.LglIDColumn.ColumnName.ToString())))
                            {
                                if (commentsGridView.Rows.Count - 1 >= this.form49912LegalData.CommentsDetailsTable.Rows.Count - 1)
                                {
                                    this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0] = this.commentsReturnText;
                                    TerraScanCommon.SetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"), this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0].ToString());
                                    commentsGridView[0, e.RowIndex].Value = Properties.Resources.RED_copy;
                                }
                            }
                            else
                            {
                                //// Added for 4505 by Malliga
                                // if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                                if (!string.IsNullOrEmpty(this.commentsReturnText))
                                {
                                    commentsGridView[0, e.RowIndex].Value = Properties.Resources.RED_copy;
                                }
                                else
                                {
                                    commentsGridView[0, e.RowIndex].Value = Properties.Resources.GREEN_copy;
                                }
                                ////End Here
                            }
                        }
                        else
                        {
                            commentsGridView[1, e.RowIndex].Value = this.commentsReturnText;
                            this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0] = this.commentsReturnText;
                            TerraScanCommon.SetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"), this.commentsReturnText);
                            if (this.commentsReturnText == string.Empty)
                            {
                                commentsGridView[0, e.RowIndex].Value = Properties.Resources.GREEN_copy;
                            }
                            else
                            {
                                commentsGridView[0, e.RowIndex].Value = Properties.Resources.RED_copy;
                            }
                        }
                    }
                    else
                    {
                        //// DataGridView commentsGridView = (this.LegalGridUserControl.Controls[0].Controls[9].Controls[1] as DataGridView);
                        //// this.commentsReturnText = TerraScanCommon.GetValue(commentsForm, SharedFunctions.GetResourceString("ReturnTextValue"));

                        //// if (string.IsNullOrEmpty(this.commentsReturnText))
                        //// {
                        ////     commentsGridView[1, e.RowIndex].Value = string.Empty;

                        ////     DataRow commentSRow = this.form49912LegalData.CommentsDetailsTable.NewRow();
                        ////     commentSRow[0] = this.commentsReturnText;
                        ////     this.form49912LegalData.CommentsDetailsTable.Rows.Add(commentSRow);
                        ////     this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0] = this.commentsReturnText;
                        ////     commentsGridView[0, e.RowIndex].Value = Properties.Resources.CImage;
                        //// }
                        //// else
                        //// {
                        ////     commentsGridView[1, e.RowIndex].Value = this.commentsReturnText;
                        ////     this.form49912LegalData.CommentsDetailsTable.Rows[e.RowIndex][0] = commentsGridView[1, e.RowIndex].Value.ToString();
                        ////     commentsGridView[0, e.RowIndex].Value = Properties.Resources.New_C_Red;
                        //// }
                    }

                    //this.SetHeightForForm(this.form49912LegalData.CommentsDetailsTable.Rows.Count);
                    //SliceResize sliceResize;
                    //sliceResize.MasterFormNo = this.masterFormNo;
                    //sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    //sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                    //this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    //this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SectionGridEdit event of the LegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void LegalGridUserControl_SectionGridEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

            //try
            //{
            //    if (!this.flagFormLoad)
            //    {
            //        this.EditEnabled();                    
            //        /////this.recordCount = this.form49912LegalData.SubDivisionTable.Rows.Count;
            //        this.SetHeightForForm(this.recordCount + 1);
            //        SliceResize sliceResize;
            //        sliceResize.MasterFormNo = this.masterFormNo;
            //        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
            //        sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
            //        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            //        this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            //     }                
            //}
            //catch (Exception ex)
            //{
            //    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        private void LegalGridUserControl_SectionGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (this.LegalGridUserControl.secgridedit)
                {
                    this.EditEnabled();

                    Control[] userControl = this.LegalGridUserControl.Controls.Find("SectiondataGridView", true);
                    DataTable userControlDataTable = new DataTable();
                    if (userControl.Length > 0)
                    {
                        DataGridView userControlGridView = (DataGridView)userControl[0];
                        userControlDataTable = (DataTable)userControlGridView.DataSource;
                        this.recordCount = userControlDataTable.Rows.Count;
                    }
                     
                    /////this.recordCount = this.form49912LegalData.SubDivisionTable.Rows.Count;
                    this.SetHeightForForm(this.recordCount);// + 1);
                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
                    this.LegalGridUserControl.secgridedit = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the NWGridBeginEdit event of the LegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void LegalGridUserControl_NWGridBeginEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

       
        /// <summary>
        /// Handles the SWGridBeginEdit event of the LegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void LegalGridUserControl_SWGridBeginEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SEGridBeginEdit event of the LegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void LegalGridUserControl_SEGridBeginEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ////this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }



        //private void FsLegalGridUserControl_SectionGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    this.EditEnabled();
        //}

        /// <summary>
        /// Handles the NEGridEndEdit event of the FsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_NEGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles the NWGridEndEdit event of the FsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_NWGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles the SWGridEndEdit event of the FsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_SWGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles the SEGridEndEdit event of the FsLegalGridUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void FsLegalGridUserControl_SEGridEndEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Event Handlers

        #region PrivateMethods



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
        /// Customizes the legal grid user control.
        /// </summary>
        private void CustomizeLegalGridUserControl()
        {
            this.form49912LegalData.SubDivisionTable.SubNameColumn.ColumnName = "SubName";
            this.form49912LegalData.SubDivisionTable.LotColumn.ColumnName = "Lot";
            this.form49912LegalData.SubDivisionTable.PLotColumn.ColumnName = "LotP";
            this.form49912LegalData.SubDivisionTable.BlockColumn.ColumnName = "Block";
            this.form49912LegalData.SubDivisionTable.PBlockColumn.ColumnName = "BlockP";

            this.form49912LegalData.NEDetailsTable.NENEColumn.ColumnName = "NEFirst";
            this.form49912LegalData.NEDetailsTable.NENWColumn.ColumnName = "NWFirst";
            this.form49912LegalData.NEDetailsTable.NESWColumn.ColumnName = "SWFirst";
            this.form49912LegalData.NEDetailsTable.NESEColumn.ColumnName = "SEFirst";

            this.form49912LegalData.NWDetailsTable.NWNEColumn.ColumnName = "NESEC";
            this.form49912LegalData.NWDetailsTable.NWNWColumn.ColumnName = "NWSEC";
            this.form49912LegalData.NWDetailsTable.NWSWColumn.ColumnName = "SWSEC";
            this.form49912LegalData.NWDetailsTable.NWSEColumn.ColumnName = "SESEC";

            this.form49912LegalData.SWDetailsTable.SWNEColumn.ColumnName = "ThirdNE";
            this.form49912LegalData.SWDetailsTable.SWNWColumn.ColumnName = "ThirdNW";
            this.form49912LegalData.SWDetailsTable.SWSWColumn.ColumnName = "ThirdSW";
            this.form49912LegalData.SWDetailsTable.SWSEColumn.ColumnName = "ThirdSE";

            this.form49912LegalData.SEDetailsTable.SENEColumn.ColumnName = "FourNE";
            this.form49912LegalData.SEDetailsTable.SENWColumn.ColumnName = "FourNW";
            this.form49912LegalData.SEDetailsTable.SESWColumn.ColumnName = "FourSW";
            this.form49912LegalData.SEDetailsTable.SESEColumn.ColumnName = "FourSE";

            this.form49912LegalData.CommentsDetailsTable.CommentsColumn.ColumnName = "Comments";
        }

        /// <summary>
        /// Loads the legal grid user control.
        /// </summary>
        private void LoadLegalGridUserControl()
        {
            this.form49912InstrumentLegalComboData.f49910_pclst_GrantList.Clear();
            this.form49912InstrumentLegalComboData = this.form49912Controller.WorkItem.F49910_GetInstrumentTypeDetails();
            this.form49912InstrumentLegalComboData.f49910_pclst_GrantList.NameColumn.ColumnName = "SubName";
            this.form49912InstrumentLegalComboData.f49910_pclst_GrantList.GrantIDColumn.ColumnName = "SubID";
            ////this.LegalGridUserControl.fillData(this.form49912InstrumentLegalComboData.f49910_pclst_GrantList);               
        }

        /// <summary>
        /// Sets the height for form.
        /// </summary>
        /// <param name="recordCount">The record count.</param>
        private void SetHeightForForm(int recordCount)
        {
            if (recordCount > 1)
            //{
            //    ////int increment = ((recordCount - 1) * 22);
            //    ////this.LegalGridUserControl.Height = this.LegalGridUserControl.Height;
            //    this.UserControlGridViewpanel.Height = this.LegalGridUserControl.Height + 22;
            //    this.LegalPictureBox.Height = UserControlGridViewpanel.Height;
            //    this.Height = this.LegalPictureBox.Height;
            ///if (recordCount == 2)
            {
                this.UserControlGridViewpanel.Height = this.LegalGridUserControl.Height;
                this.LegalPictureBox.Height = UserControlGridViewpanel.Height;
                this.Height = this.LegalPictureBox.Height;
            }

            else
            {
                this.LegalGridUserControl.Height = 96;
                this.UserControlGridViewpanel.Height = this.LegalGridUserControl.Height - 1;
                this.LegalPictureBox.Height = this.UserControlGridViewpanel.Height;
                this.Height = this.LegalPictureBox.Height;
            }
        }

        /// <summary>
        /// Saves the legal grid details.
        /// </summary>
        /// <returns>saveResult</returns>
        private bool SaveLegalGridDetails()
        {
            bool saveResult = false;
            string legalDetails = string.Empty;

            DataSet dslegalData = new DataSet("Root");
            dslegalData = this.LegalGridUserControl.GetDatas();
            dslegalData.Namespace = string.Empty;
            dslegalData.DataSetName = "Root";

            legalDetails = dslegalData.GetXml();
            this.saveConfirm = this.form49912Controller.WorkItem.F49912_InsertLegalFieldDetails(this.keyId, legalDetails, TerraScanCommon.UserId, this.copyID);
            this.copyID = 0;
            this.keyId = this.saveConfirm;

            saveResult = true;
            return saveResult;
        }

        /// <summary>
        /// Edits the enabled.
        /// </summary>
        private void EditEnabled()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.LegalGridUserControl.Enabled = true;
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }

        /// <summary>
        /// Clears the data tables.
        /// </summary>
        private void ClearDataTables()
        {
            this.form49912LegalData.SubDivisionTable.Clear();
            this.form49912LegalData.NEDetailsTable.Clear();
            this.form49912LegalData.NWDetailsTable.Clear();
            this.form49912LegalData.SEDetailsTable.Clear();
            this.form49912LegalData.SWDetailsTable.Clear();
            this.form49912LegalData.CommentsDetailsTable.Clear();
            this.form49912InstrumentLegalComboData.f49910_pclst_GrantList.Clear();
        }

        /// <summary>
        /// Adds the empty row on load.
        /// </summary>
        private void InsertEmptyRow()
        {
            if (this.form49912LegalData.SubDivisionTable.Rows.Count == 0)
            {
                this.form49912LegalData.SubDivisionTable.Rows.InsertAt(this.form49912LegalData.SubDivisionTable.NewRow(), this.form49912LegalData.SubDivisionTable.Rows.Count);
            }

            if (this.form49912LegalData.NWDetailsTable.Rows.Count == 0)
            {
                this.form49912LegalData.NWDetailsTable.Rows.InsertAt(this.form49912LegalData.NWDetailsTable.NewRow(), this.form49912LegalData.NWDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.NEDetailsTable.Rows.Count == 0)
            {
                this.form49912LegalData.NEDetailsTable.Rows.InsertAt(this.form49912LegalData.NEDetailsTable.NewRow(), this.form49912LegalData.NEDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.SWDetailsTable.Rows.Count == 0)
            {
                this.form49912LegalData.SWDetailsTable.Rows.InsertAt(this.form49912LegalData.SWDetailsTable.NewRow(), this.form49912LegalData.SWDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.SEDetailsTable.Rows.Count == 0)
            {
                this.form49912LegalData.SEDetailsTable.Rows.InsertAt(this.form49912LegalData.SEDetailsTable.NewRow(), this.form49912LegalData.SEDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.CommentsDetailsTable.Rows.Count == 0)
            {
                this.form49912LegalData.CommentsDetailsTable.Rows.InsertAt(this.form49912LegalData.CommentsDetailsTable.NewRow(), this.form49912LegalData.CommentsDetailsTable.Rows.Count);
            }
        }

        /// <summary>
        /// Adds the empty row on load.
        /// </summary>
        private void AddEmptyRowOnLoad()
        {
            if (this.form49912LegalData.SubDivisionTable.Rows.Count >= 0)
            {
                this.form49912LegalData.SubDivisionTable.Rows.InsertAt(this.form49912LegalData.SubDivisionTable.NewRow(), this.form49912LegalData.SubDivisionTable.Rows.Count);
            }

            if (this.form49912LegalData.NWDetailsTable.Rows.Count >= 0)
            {
                this.form49912LegalData.NWDetailsTable.Rows.InsertAt(this.form49912LegalData.NWDetailsTable.NewRow(), this.form49912LegalData.NWDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.NEDetailsTable.Rows.Count >= 0)
            {
                this.form49912LegalData.NEDetailsTable.Rows.InsertAt(this.form49912LegalData.NEDetailsTable.NewRow(), this.form49912LegalData.NEDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.SWDetailsTable.Rows.Count >= 0)
            {
                this.form49912LegalData.SWDetailsTable.Rows.InsertAt(this.form49912LegalData.SWDetailsTable.NewRow(), this.form49912LegalData.SWDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.SEDetailsTable.Rows.Count >= 0)
            {
                this.form49912LegalData.SEDetailsTable.Rows.InsertAt(this.form49912LegalData.SEDetailsTable.NewRow(), this.form49912LegalData.SEDetailsTable.Rows.Count);
            }

            if (this.form49912LegalData.CommentsDetailsTable.Rows.Count >= 0)
            {
                this.form49912LegalData.CommentsDetailsTable.Rows.InsertAt(this.form49912LegalData.CommentsDetailsTable.NewRow(), this.form49912LegalData.CommentsDetailsTable.Rows.Count);
            }
        }

        /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>sliceValidationFields</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            string messageMissingReqField = string.Empty;
            sliceValidationFields.FormNo = formNo;
            sliceValidationFields.ErrorMessage = string.Empty;
            sliceValidationFields.RequiredFieldMissing = false;

            DataGridView sectionGridView = (this.LegalGridUserControl.Controls[0].Controls[10].Controls[0] as DataGridView);
            int tempVal;
            int.TryParse(sectionGridView[0, 0].Value.ToString(), out tempVal);

            /*to validate the missing fields in GRID.*/
            if (tempVal == 0)
            {
                sliceValidationFields.ErrorMessage = "You cannot save this record because it is missing required fields in FSLegal.";
                sliceValidationFields.RequiredFieldMissing = true;
                return sliceValidationFields;
            }
            return sliceValidationFields;
        }

        /// <summary>
        /// Controls the lock.
        /// </summary>
        /// <param name="controlLock">if set to <c>true</c> [control lock].</param>
        private void ControlLock(bool controlLock)
        {
            this.UserControlGridViewpanel.Enabled = !controlLock;
            this.LegalGridUserControl.Enabled = !controlLock;
        }
        #endregion PrivateMethods

        /* Added By Ramya.D for BugId #2479 */
        /// <summary>
        /// LegalGridUserControl_SectionGridRowDeleteEvent
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LegalGridUserControl_SectionGridRowDeleteEvent(object sender, DataGridViewRowCancelEventArgs e)
        {
            int rowcount = this.form49912LegalData.SubDivisionTable.Rows.Count;
            if (e.Row.Index >= 0)
            {
                Control[] userControl = this.LegalGridUserControl.Controls.Find("SectiondataGridView", true);
                DataTable userControlDataTable = new DataTable();
                if (userControl.Length > 0)
                {
                    DataGridView userControlGridView = (DataGridView)userControl[0];
                    userControlDataTable = (DataTable)userControlGridView.DataSource;
                }

                this.form49912LegalData.SubDivisionTable.Clear();
                this.form49912LegalData.SubDivisionTable.Merge(userControlDataTable.Copy());
                
                //if (!string.IsNullOrEmpty(this.form49912LegalData.SubDivisionTable.Rows[e.Row.Index][this.form49912LegalData.SubDivisionTable.SubNameColumn].ToString()))
                if (!string.IsNullOrEmpty(this.form49912LegalData.SubDivisionTable.Rows[e.Row.Index][this.form49912LegalData.SubDivisionTable.SubIDColumn].ToString()))
                {

                    rowcount = rowcount - 1;
                    this.EditEnabled();
                    LegalGridUserControl.deleteRecord(this.formLegalComboData.F49912SubDivisionComboTable, e.Row.Index);

                    this.usercontrolRecordCount = this.LegalGridUserControl.RecordCountIndicator;
                    this.recordCount = rowcount;
                    this.SetHeightForForm(this.usercontrolRecordCount);

                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo;
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.LegalPictureBox.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.LegalPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.LegalPictureBox.Height, this.LegalPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);

                }
            }

        }

        ////To change the mode while selecting the combo 
        /// <summary>
        /// LegalGridUserControl_SectionGridSelectionChangeEvent
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LegalGridUserControl_SectionGridSelectionChangeEvent(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EditEnabled();
                    this.form49912LegalData.SubDivisionTable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// LegalGridUserControl_SectionTextChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LegalGridUserControl_SectionTextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    //this.EditEnabled();
                    //this.form49912LegalData.SubDivisionTable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// LegalGridUserControl_SectionKeyPressEvent
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void LegalGridUserControl_SectionKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EditEnabled();
                    //this.form49912LegalData.SubDivisionTable.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /// <summary>
        /// Handles NE grid key press. Added by Biju on 16-Nov-2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalGridUserControl_NEKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles SE grid key press. Added by Biju on 16-Nov-2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalGridUserControl_SEKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles NW grid key press. Added by Biju on 16-Nov-2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalGridUserControl_NWKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
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
        /// Handles SW grid key press. Added by Biju on 16-Nov-2010
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LegalGridUserControl_SWKeyPressEvent(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!this.flagFormLoad)
                {
                    this.EditEnabled();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        /* Till here */
    }
}
