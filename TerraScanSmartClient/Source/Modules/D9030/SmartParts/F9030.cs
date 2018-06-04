//--------------------------------------------------------------------------------------------
// <copyright file="F9030.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Sep 06        Suganth Mani       Modified for stylecop changes
// 07 Dec 09        Malliga            BackGround Color on 9030 functionality implemented. 
// 20120510         Manoj P            Changes in the form Quick Find Operation Form 3 Parameters added.
// 20160613         Priyadharshini     Added Delete Message Box for Permit Import and Permit Import Template.
//20160725          priyadharshini     TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
//20170630          dhineshkumar       Modified Header detail smartpart for #22104 Form Header.
//*********************************************************************************/

#region NameSpace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Infrastructure.Interface;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.ObjectBuilder;
using TerraScan.BusinessEntities;
using TerraScan.Common;
using TerraScan.Common.Reports;
using TerraScan.Infrastructure.Interface.Constants;
using TerraScan.SmartParts;
using TerraScan.UI.Controls;
using TerraScan.Utilities;
using TerraScan.Helper;


#endregion NameSpace

namespace D9030
{
    /// <summary>
    /// Class file implimaenting the masterform functionalities
    /// </summary>
    [SmartPart]
    public partial class F9030 : BaseSmartPart
    {
        #region PrivateMembers

        /// <summary>
        /// vaiable for gap between slices
        /// </summary>
        private int gapBetweenSlices;

        /// <summary>
        /// LoginUserValidation;
        /// </summary>
        private bool LoginUserValidation;

        /// <summary>
        /// picture box array representing collapsed tab array
        /// </summary>
        private PictureBox[] collapsedTabs;

        /// <summary>
        /// deckworkspaces to load the slice forms
        /// </summary>
        private DeckWorkspace[] collapsedWorkSpaces;

        /// <summary>
        /// count of the slices in the sandwich
        /// </summary>
        private int formSliceCount;

        /// <summary>
        /// Form master entity
        /// </summary>
        private FormMasterData formMasterDataSet;

        /// <summary>
        /// keyid value
        /// </summary>
        private int keyId;

        /// <summary>
        /// pointer to the xcoordinate
        /// </summary>
        private int xcoordinatePosition;

        /// <summary>
        /// pointer to the ycoordinate
        /// </summary>
        private int ycoorPosition;

        /// <summary>
        /// flag to identify wether to close the form after delete operation
        /// </summary>
        private bool flagCloseOnDelete;

        /// <summary>
        /// the no of the form being loaded
        /// </summary>
        private int formNo;

        /// <summary>
        /// The instance of the operation smartpart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// The instance of the reportactionsmartpart
        /// </summary>
        private ReportActionSmartPart reportActionSmartPart;

        /// <summary>
        /// Instance of additional operation smartpart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// F10101 controller
        /// </summary>
        private F9030Controller form9030control;

        /// <summary>
        /// variable to hold validation error messages
        /// </summary>
        private string errorMessage;

        /// <summary>
        /// userid for the current form
        /// </summary>
        private int userId;

        /// <summary>
        /// flag to show required field missing
        /// </summary>
        private bool requiredFieldMissing;

        /// <summary>
        /// flag to identify completion of save operation
        /// </summary>
        private bool flagSaveConfirmed;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// FeatureClassID for the form master
        /// </summary>
        private int featureClassID;

        /// <summary>
        /// flag to identify wether record navigation visible
        /// </summary>
        private bool flagRecordNavigationVisible;

        /// <summary>
        /// flag to identify wether query engine visible
        /// </summary>
        private bool flagQueryEngineVisible;

        /// <summary>
        /// flag to identify wether query engine visible
        /// </summary>
        private bool flagQuickFindVisible;

        /// <summary>
        /// The string which appears in the form header
        /// </summary>
        private string formHeaderString;

        /// <summary>
        /// variable for column name of query view
        /// </summary>
        private string columnNameValue;

        /// <summary>
        /// The instance of the queryenginesmartpart
        /// </summary>
        private F9033 queryEngineSmartPart;

        /// <summary>
        /// The instance of the findSmartPart
        /// </summary>
        private F9610 findSmartPart;

        /// <summary>
        /// Link label text for auditlink label
        /// </summary>
        private string auditLinkLabelText;

        /// <summary>
        /// flag to disable new mode
        /// </summary>
        private bool disableNewMode;

        /// <summary>
        /// flag to specify load using queryengine
        /// </summary>
        private bool flagWithoutKeyId;

        /// <summary>
        /// flag for navigation
        /// </summary>
        private bool flagNavigation;

        /// <summary>
        /// flag to validatekeyid
        /// </summary>
        private bool flagValidateKeyId;

        /// <summary>
        /// flag to identify wether the form already exists
        /// </summary>
        private bool flagFormExists;


        /// <summary>
        /// flag to identiy form resizing of form
        /// </summary>
        private bool flagFormResizing;

        /// <summary>
        /// slice display area height
        /// </summary>
        private int sliceDisplayAreaHeight;

        /// <summary>
        /// previous active keyid
        /// </summary>
        private int previousKeyId;

        /// <summary>
        /// the no of parameters
        /// </summary>
        private Int16 parameterCount;

        /// <summary>
        /// List to maintain the parameters
        /// </summary>
        private List<string> parameterList;

        private Panel tempPanel;

        private Panel tempNavigationPanel;

        /// <summary>
        /// variable to hold Last Content
        /// </summary>
        private string lastcontentxml;

        /// <summary>
        /// variable to hold Last Content
        /// </summary>
        private string lastcontent;


        /// <summary>
        /// variable to hold Quick Find rows Content
        /// </summary>
        private DataTable QuickFindcontent;

        /// <summary>
        /// Query Button Click flag
        /// </summary>
        private bool isQueryLoaded;

        /// <summary>
        /// Load Default Query View
        /// </summary>
        public static bool flagQueryLoad;

        /// <summary>
        /// Load Receipt Date
        /// </summary>
        public static string lastreceiptdate;

        /// <summary>
        /// Load Interest Date
        /// </summary>
        public static string lastinterestdate;

        /// <summary>
        /// Flag for load QuickFind form
        /// </summary>
        private bool quickfindloadflag = false;

        /// <summary>
        /// Flag for load Query Load
        /// </summary>
        private bool isQyeryClosed = false;

        /// <summary>
        /// Flag for deleted record
        /// </summary>
        private bool hasRecordDeleted = true;

        /// <summary>
        /// Flag for set null record mode
        /// </summary>
        private bool canSetNullRecordMode = true;

        /// <summary>
        /// Flag for non query dependent form
        /// </summary>
        private bool queryDependentForm = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool isAnalyticsVisible;

        /// <summary>
        /// 
        /// </summary>
        public static bool isUpdateVisible;

        /// <summary>
        /// F9612 Activity Queue SmartPart
        /// </summary>
        private F9612 activityQueueSmartPart;

        /// <summary>
        /// Flag to track visited record
        /// </summary>
        private bool isValidRecord;

        /// <summary>
        /// Loaded webslices count 
        /// </summary>
        private int loadedWebSlices;

        /// <summary>
        /// Resized webslices count
        /// </summary>
        private int resizedWebSlices;

        private PictureBox progressImage;

        /// <summary>
        /// Flag for visible temppanel
        /// </summary>
        private bool isJerkFree = true;

        private bool isCancelClick = false;

        private bool isClear = false;


        /// <summary>
        /// Instance for F15100
        /// </summary>
        D11001.F15110 f15110;

      #endregion PrivateMembers
      
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9030"/> class.
        /// </summary>
        public F9030()
        {
            this.InitializeComponent();
            this.formMasterDataSet = new FormMasterData();
            this.userId = TerraScanCommon.UserId;
            this.featureClassID = -99;
            this.parameterCount = 0;
            this.parameterList = new List<string>();
            this.parameterList.Clear();
            this.columnNameValue = string.Empty;
            this.formHeaderString = string.Empty;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //this.DoubleBuffered = true;
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true); 

            // White panel to hide jankyness
            tempPanel = new Panel();
            tempPanel.Height = this.Height;
            tempPanel.Width = this.Width;
            tempPanel.BackColor = Color.White;
            tempPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
            this.Controls.Add(tempPanel);
            tempPanel.Visible = true;
            tempPanel.BringToFront();
            tempPanel.UseWaitCursor = true;
            //Cursor.Current = Cursors.WaitCursor;
            Application.UseWaitCursor = true; ;

            tempNavigationPanel = new Panel();
            tempNavigationPanel.Height = this.sliceListPanel.Height;
            tempNavigationPanel.Width = this.sliceListPanel.Width;
            tempNavigationPanel.Location = this.sliceListPanel.Location;
            tempNavigationPanel.BackColor = Color.White;
            this.Controls.Add(tempNavigationPanel);

            this.HelpMenuItem.Click += new EventHandler(this.HelpMenuItem_Click);
        }

        /// <summary>
        /// Handles the Click event of the HelpMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.InvokeHelpEngine();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Constructor

        #region EventPublication

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_AlertResizableSlice, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_AlertResizableSlice;

        /// <summary>
        /// Event publication for save to proceed after validation
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_SaveConfirmed, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_SaveConfirmed;

        /// <summary>
        /// Event publication for cancel triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_CancelSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_CancelSliceInformation;

        /// <summary>
        /// Event publication for save triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_SaveSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_SaveSliceInformation;

        /// <summary>
        /// Event publication for delete triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_DeleteSliceInformation, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_DeleteSliceInformation;

        /// <summary>
        /// Event publication for new triggerring in slice
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_EnableNewMethod, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> D9030_F9030_EnableNewMethod;

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event to set permission in slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_SetSlicePermission, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<SlicePermissionReload>> D9030_F9030_SetSlicePermission;

        /// <summary>
        /// Event for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

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
        /// event to intimate slice to reload the record based in keyid
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9030_LoadSliceDetails;

        /// <summary>
        /// event to intimate slice to reload the record based in keyid and parameters
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_LoadParaMeterizedSliceDetails, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadParaMeterizedActiveRecord>> D9030_F9030_LoadParaMeterizedSliceDetails;

        /// <summary>
        /// Event triggered when smartpart closed
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_AlertDistinguishedSliceOnClose, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose>> D9030_F9030_AlertDistinguishedSliceOnClose;

        /// <summary>
        /// Event to intimate query engine about queryengine close
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_QueryEngineCloseAtFormMaster, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9030_QueryEngineCloseAtFormMaster;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Get current record keyID -- Forms opening from support from call
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_OptionalParameterKeyId, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int[]>> OptionalParameter_GetKeyId;

        #region RecordNavigation

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for SetActiveKeyid
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveKeyId;


        #endregion RecordNavigation

        /// <summary>
        /// Occurs when [form master_ set scroll position].
        /// </summary>
        [EventPublication(EventTopicNames.FormMaster_SetScrollPosition, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<int>> FormMaster_SetScrollPosition;


        ///<summary>
        /// Used to hold the SketchButton Mode
        /// </summary>
        [EventPublication(EventTopicNames.ApexOpenedEvent, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<bool>> ApexOpenedEvent;

        /// <summary>
        /// FormSlice_EditEnabled
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        #endregion EventPublication

        #region FieldEncapsulation

        /// <summary>
        /// Gets or sets the key ID.
        /// </summary>
        /// <value>The key ID.</value>
        public int KeyId
        {
            get { return this.keyId; }
            set { this.keyId = value; }
        }

        /// <summary>
        /// Gets or sets the form9030control.
        /// </summary>
        /// <value>The form9030control.</value>
        [CreateNew]
        public F9030Controller Form9030control
        {
            get { return this.form9030control; }
            set { this.form9030control = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }
        }

        /// <summary>
        /// Gets or sets the content of the last.
        /// </summary>
        /// <value>The content of the last.</value>
        public string CommandResult
        {
            get { return this.lastcontentxml; }
            set { this.lastcontentxml = value; }
        }

        /////// <summary>
        /////// Gets or sets a value indicating whether this instance is analytics visible.
        /////// </summary>
        /////// <value>
        /////// 	<c>true</c> if this instance is analytics visible; otherwise, <c>false</c>.
        /////// </value>
        ////public static bool IsAnalyticsVisible
        ////{
        ////    get { return isAnalyticsVisible; }
        ////    set { isAnalyticsVisible = value; }
        ////}

        /////// <summary>
        /////// Gets or sets a value indicating whether this instance is update visible.
        /////// </summary>
        /////// <value>
        /////// 	<c>true</c> if this instance is update visible; otherwise, <c>false</c>.
        /////// </value>
        ////public static bool IsUpdateVisible
        ////{
        ////    get { return isUpdateVisible; }
        ////    set { isUpdateVisible = value; }
        ////}

        #endregion FieldEncapsulation

        #region EventSubscription


        /// <summary>
        /// Sets the Navigation Position of the record
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.F9030_NavigationRecord, ThreadOption.UserInterface)]
        public void NavigationPosition(object sender, DataEventArgs<int> e)
        {
            this.SetActiveRecord(this, new DataEventArgs<int>(e.Data));
            this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { e.Data, this.queryEngineSmartPart.TotalRecords }));
            //this.SetMasterformGeneralProperties(); 
        }

        /// <summary>
        /// Invokes the Apex Opened Sketch Button operation at form master.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.F9030_ApexOpenEvent, ThreadOption.UserInterface)]
        public void D9030_F9030_ApexOpenEvent(object sender, DataEventArgs<bool> eventArgs)
        {
            if (eventArgs.Data.Equals(true))
            {
                this.ApexOpenedEvent(this, new DataEventArgs<bool>(true));
            }
            else
            {
                this.ApexOpenedEvent(this, new DataEventArgs<bool>(false));
            }
        }

        /// <summary>
        /// Invokes the cancel operation at form master.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F95005_FomrMasterCancel, ThreadOption.UserInterface)]
        public void OnD9030_F95005_AlertFomrMasterCancel(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn].ToString()))
                {
                    this.isJerkFree = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn]);
                }

                this.CancelButton_Click();
            }
        }

        /// <summary>
        /// Invokes the save operation at form master.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F95005_FormMasterSave, ThreadOption.UserInterface)]
        public void OnD9030_F95005_AlertFormMasterSave(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                this.SaveButton_Click();
            }
        }

        /// <summary>
        /// Invoked when the query engine closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_QueryEngineClose, ThreadOption.UserInterface)]
        public void OnD9030_F9033_QueryEngineClose(object sender, TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                // tocheck

                this.tempPanel.Visible = true;
                this.tempPanel.BringToFront();

                int tempKeyId = this.keyId;
                this.previousKeyId = this.keyId;
                this.isQyeryClosed = true;
                
                this.DisplayQueryEngine();
                ////commented by Biju on 30/Dec/2009 to avoid multiple calls of f9030_pcget_FormMaster
                ////this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009
                ////added by Biju on 30/Dec/2009 to avoid multiple calls of f9030_pcget_FormMaster
                this.SetMasterformGeneralPropertiesWithNoDBCall();

                if (this.queryEngineSmartPart.TotalRecords > 0)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }

                if (!this.queryDependentForm)
                {
                    if (this.canSetNullRecordMode)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.AuditlinkLabel.Enabled = true;
                    }
                }

                this.isQyeryClosed = false;

                // Method added to track visited records on after QE close
                this.isValidRecord = true;
                this.TrackRecordHistory();

                if (!tempKeyId.Equals(this.keyId))
                {
                    if (this.keyId > 0)
                    {
                        DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                       "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                       "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                       "IsPermissionOpen = 'True'");
                        this.loadedWebSlices = webSliceRecords.Length * 2;
                    }
                    else
                    {
                        this.loadedWebSlices = 0;
                    }

                    if (this.loadedWebSlices.Equals(0))
                    {
                        tempPanel.Visible = false;
                        tempPanel.SendToBack();
                        Application.UseWaitCursor = false;
                    }
                }
                else
                {
                    tempPanel.Visible = false;
                    tempPanel.SendToBack();
                    Application.UseWaitCursor = false;
                    this.loadedWebSlices = 0;
                }

                this.canSetNullRecordMode = true;
                this.queryDependentForm = true;
                this.isJerkFree = true;
                //if (this.loadedWebSlices.Equals(0))
                //{
                //    tempPanel.Visible = false;
                //    tempPanel.SendToBack();
                //}

                //this.isQueryLoaded = true; 
            }
         }

        /// <summary>
        /// Called when the section indicator in the slice is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormSlice_SectionIndicatorClick, ThreadOption.UserInterface)]
        public void OnFormSlice_SectionIndicatorClick(object sender, EventArgs eventArgs)
        {
            if (eventArgs != null)
            {
                this.flagFormResizing = true;
                this.sliceListPanel.AutoScroll = false;
                string formNumber = eventArgs.ToString();
                // Flag for identify call from operation button click events - specially added for F35102 form slice
                bool callFromOperations = false;

                if (tempPanel.Visible)
                {
                    callFromOperations = true;
                }

                if (!callFromOperations)
                {
                    tempPanel.Visible = true;
                    tempPanel.BringToFront();
                }
                this.CollapseSlices(formNumber);

                if (!callFromOperations)
                {
                    tempPanel.Visible = false;
                    tempPanel.SendToBack();
                    Application.UseWaitCursor = false;
                }

                this.sliceListPanel.AutoScroll = true;
                this.flagFormResizing = false;
            }
        }

        /// <summary>
        /// Whe any button in the operation smart part is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn].ToString()))
            {
                this.isJerkFree = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn]);
            }

            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.Focus();
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
                case "DELETE":
                    this.DeleteButton_Click();
                    break;
            }

            this.isJerkFree = true;
        }

        /// <summary>
        /// Called when [form slice_ validation alert].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_ValidationAlert, ThreadOption.UserInterface)]
        public void OnFormSlice_ValidationAlert(object sender, DataEventArgs<SliceValidationFields> eventArgs)
        {
            this.requiredFieldMissing = false;
            if (this.formNo == eventArgs.Data.FormNo)
            {
                if (string.IsNullOrEmpty(this.errorMessage))
                {
                     this.errorMessage = eventArgs.Data.ErrorMessage;
                     this.requiredFieldMissing = eventArgs.Data.RequiredFieldMissing;
                }

                if (!this.disableNewMode)
                {
                    this.disableNewMode = eventArgs.Data.DisableNewMethod;
                }
            }
        }

        /// <summary>
        /// Called when [form slice_ edit enabled].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_EditEnabled, ThreadOption.UserInterface)]
        public void OnFormSlice_EditEnabled(object sender, DataEventArgs<int> eventArgs)
        {
            if (!this.flagNavigation)
            {
                if (eventArgs.Data == this.formNo)
                {
                    if (this.PermissionEdit)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                    }
                }
            }
        }

        /// <summary>
        /// Called when [form slice_ set view mode].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_SetViewMode, ThreadOption.UserInterface)]
        public void OnFormSlice_SetViewMode(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.formNo)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
            }
        }

        /// <summary>
        /// Handles the VisibleForms event of the FormMaster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Boolean&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormMaster_VisibleForms, ThreadOption.UserInterface)]
        public void OnFormMaster_VisibleForms(object sender, DataEventArgs<EnablePanelEventArgs> eventArgs)
        {
            if (!eventArgs.Data.IsSlice)
            {
                if (!eventArgs.Data.IsVisible)
                {
                    this.tempPanel.Visible = true;
                    this.tempPanel.BringToFront();
                }
                else
                {
                    if (this.loadedWebSlices.Equals(this.resizedWebSlices))
                    {
                        this.tempPanel.Visible = false;
                        //this.tempPanel.SendToBack();
                        Application.UseWaitCursor = false;
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            else
            {
                if (!eventArgs.Data.IsVisible)
                {
                    this.tempNavigationPanel.Visible = true;
                    this.tempNavigationPanel.BringToFront();
                }
                else
                {
                    //if (this.loadedWebSlices.Equals(this.resizedWebSlices))
                    //{
                    this.tempNavigationPanel.Visible = false;
                    this.tempNavigationPanel.SendToBack();
                    //}
                }
            }
        }


        /// <summary>
        /// Called when [form slice_ null record mode after delete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceNullRecordModeEventArgs&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormSlice_NullRecordModeAfterDelete, ThreadOption.UserInterface)]
        public void OnFormSlice_NullRecordModeAfterDelete(object sender, DataEventArgs<SliceNullRecordModeEventArgs> eventArgs)
        {
            if (eventArgs.Data.MasterFormNo == this.formNo)
            {
                this.canSetNullRecordMode = eventArgs.Data.AllowNullRecordMode;
                this.queryDependentForm = eventArgs.Data.WithoutKeyId;
            }
        }

        /// <summary>
        /// Called when [quick find_ key id].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.QuickFind_KeyId, ThreadOption.UserInterface)]
        public void OnQuickFind_KeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.formNo == eventArgs.Data.MasterFormNo)
            {
          
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.previousKeyId = eventArgs.Data.SelectedKeyId;

                //if (this.flagQueryEngineVisible)
                //{
                //    // Code has been added for Bug #5678: Quick Find does not enable Delete button
                //    this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "Save");
                //}
                this.RecordNavigationDeckWorkspace.Visible = false; 
                if (this.collapsedTabs == null && this.collapsedWorkSpaces == null)
                {
                    this.LoadSlices();
                }
                else
                {

                    //19722 Quick find form when null values in query engine load quick find form
                    //Clear the content collapsed tabs and workspace recreate content
                    this.collapsedTabs = null;
                    this.collapsedWorkSpaces = null;
                    this.LoadSlices();
                ////  this.OnD9030_F9030_LoadSliceDetails(eventArgs);
                }

                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                    "IsPermissionOpen = 'True'");
                if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
                {
                    this.loadedWebSlices = webSliceRecords.Length * 2;
                }
            }
        }

        /// <summary>
        /// Called when [form slice_ revert delete alert].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormSlice_RevertDeleteAlert, ThreadOption.UserInterface)]
        public void OnFormSlice_RevertDeleteAlert(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.formNo)
            {
                this.hasRecordDeleted = false;
            }
        }

        /// <summary>
        /// Called when [form slice_ form close alert].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_FormCloseAlert, ThreadOption.UserInterface)]
        public void OnFormSlice_FormCloseAlert(object sender, DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.formNo == eventArgs.Data.FormNo)
            {
                if (!this.flagCloseOnDelete)
                {
                    this.flagCloseOnDelete = eventArgs.Data.FlagFormClose;
                }
            }
        }

        /// <summary>
        /// Handles the SetParcelLockProperties event of the D9030_F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.Parcellockimplimentation&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetParcelLockProperties, ThreadOption.UserInterface)]
        public void D9030_F9030_SetParcelLockProperties(object sender, DataEventArgs<Parcellockimplimentation> eventArgs)
        {
            if (this.formNo == eventArgs.Data.MasterFormNo)
            {
                this.operationSmartPart.SaveButtonEnable = true;
                this.operationSmartPart.SaveButtonText = eventArgs.Data.SaveButtonText;
                if (this.operationSmartPart.SaveButtonText == "Locked")
                {
                    this.operationSmartPart.SaveButtonType = TerraScanButton.ButtonType.None;
                }
                else
                {
                    this.operationSmartPart.SaveButtonType = TerraScanButton.ButtonType.CommandButton;
                }
                this.operationSmartPart.SaveButtonBackColor = eventArgs.Data.SaveButtonBackColor;
                this.operationSmartPart.SaveButtonForeColor = eventArgs.Data.SaveButtonForeColor;
                this.operationSmartPart.SaveButtonTooltip = eventArgs.Data.SaveButtonTooltipText;
                this.operationSmartPart.CancelButtonEnable = eventArgs.Data.CancelButtonEnable;
                this.operationSmartPart.DeleteButtonEnable = eventArgs.Data.DeleteButtonEnable;
                this.operationSmartPart.SaveButtonEnable = eventArgs.Data.SaveButtonEnable;
                //////this.operationSmartPart.saveButtonType = eventArgs.Data.saveButtonType;

            }
        }

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            //tempPanel.Visible = true;
            //tempPanel.BringToFront();

            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.formNo))
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    this.formNo = Convert.ToInt32(optionalParams[optionalParams.Length - 1]);
                    if (optionalParams[0] != null)
                    {
                        if (!int.TryParse(optionalParams[0].ToString(), out this.keyId))
                        {
                            //// check wether the form exists
                            if (this.flagFormExists != true)
                            {
                                this.keyId = -99;
                            }
                            //else
                            //{
                            //    if (this.keyId == 0)
                            //    {
                            //        if (DialogResult.OK == MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                            //        {
                            //            this.keyId = -99;
                            //        }
                            //        else
                            //        {
                            //            this.ParentForm.Close();
                            //            return;
                            //        }
                            //    }
                            //}
                        }

                        this.flagWithoutKeyId = false;
                    }
                    else
                    {
                        //// These code is executed when no parameter is passed.
                        this.flagWithoutKeyId = true;

                        //// check wether the form exists
                        if (this.flagFormExists != true)
                        {
                            this.keyId = -99;
                        }
                        else
                        {
                            if (this.QueryEngineWorkSpace.Visible)
                            {
                                ActivateDisplayQueryEngine();
                                //this.QueryEngineWorkSpace.Visible = false;
                            }
                        }
                    }

                    this.userId = TerraScanCommon.UserId;
                    if (optionalParams.Length > 3)
                    {
                        if (optionalParams[optionalParams.Length - 3] != null && (!string.IsNullOrEmpty(Convert.ToString(optionalParams[optionalParams.Length - 3]))))
                        {
                            this.PermissionFiled = ((PermissionFields)optionalParams[optionalParams.Length - 3]);
                            this.userId = Convert.ToInt32(optionalParams[optionalParams.Length - 2]);
                            this.PermissionEdit = this.PermissionFiled.editPermission;
                            this.SetFormPermissions(this.PermissionFiled);
                            if (this.NullRecords)
                            {
                                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                            }
                            else
                            {
                                this.AuditlinkLabel.Enabled = true;
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                        }
                    }
                    else if (optionalParams.Length == 3) // Will be executing when form opened from other form(event engine)
                    {
                        if (optionalParams[1] != null)
                        {
                            int.TryParse(optionalParams[1].ToString(), out this.featureClassID);
                        }
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (this.flagFormExists.Equals(true) && optionalParams[0] == null)
                    {
                        // this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        if (this.queryEngineSmartPart != null && this.queryEngineSmartPart.TotalRecords <= 0)
                        {
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        }

                        this.LoadAuditLinkLabel();
                    }
                    else
                    {
                        this.GetSandwichAndItsSliceInformation();//suganth
                        if (this.parameterCount > 0)
                        {
                            this.parameterList.Clear();
                            for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                            {
                                if ((optionalParams.Length > (listPointer + 2)) && (optionalParams[listPointer + 1] != null))
                                {
                                    this.parameterList.Add(optionalParams[listPointer + 1].ToString());
                                }
                            }
                        }

                        this.LoadMasterForm();
                    }

                    // to check parentform existance after 
                    // invalid keyid passes
                    if (this.ParentForm != null)
                    {
                        if (this.ParentForm.Size.Width > this.MinimumSize.Width)
                        {
                            this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                            this.formHeaderSmartPartdeckWorkspace.ResumeLayout(true);
                        }
                        else
                        {
                            this.AllignControls();
                        }
                    }
                }
                else
                {
                    if (optionalParams[0] != null)
                    {
                        DialogResult alertWhenEdit = MessageBox.Show(SharedFunctions.GetResourceString("F9030DiscardChanges"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (alertWhenEdit.Equals(DialogResult.Yes))
                        {
                            this.formNo = Convert.ToInt32(optionalParams[optionalParams.Length - 1]);
                            if (optionalParams[0] != null)
                            {
                                if (!int.TryParse(optionalParams[0].ToString(), out this.keyId))
                                {
                                    // check wether the form exists
                                    if (this.flagFormExists != true)
                                    {
                                        this.keyId = -99;
                                    }
                                }

                                this.flagWithoutKeyId = false;
                            }
                            else
                            {
                                this.flagWithoutKeyId = true;

                                // check wether the form exists
                                if (this.flagFormExists != true)
                                {
                                    this.keyId = -99;
                                }
                            }

                            this.userId = TerraScanCommon.UserId;
                            if (optionalParams.Length > 3)
                            {
                                if (optionalParams[optionalParams.Length - 3] != null && (!string.IsNullOrEmpty(Convert.ToString(optionalParams[optionalParams.Length - 3]))))
                                {
                                    this.PermissionFiled = ((PermissionFields)optionalParams[optionalParams.Length - 3]);
                                    this.userId = Convert.ToInt32(optionalParams[optionalParams.Length - 2]);
                                    this.PermissionEdit = this.PermissionFiled.editPermission;
                                    this.SetFormPermissions(this.PermissionFiled);
                                    if (this.NullRecords)
                                    {
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                    }
                                    else
                                    {
                                        this.AuditlinkLabel.Enabled = true;
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                                    }
                                }
                            }
                            else if (optionalParams.Length == 3)
                            {
                                if (optionalParams[1] != null)
                                {
                                    int.TryParse(optionalParams[1].ToString(), out this.featureClassID);
                                }
                            }

                            this.pageMode = TerraScanCommon.PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            this.GetSandwichAndItsSliceInformation();//suganth
                            if (this.parameterCount > 0)
                            {
                                this.parameterList.Clear();
                                for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                {
                                    if ((optionalParams.Length > (listPointer + 2)) && (optionalParams[listPointer + 1] != null))
                                    {
                                        this.parameterList.Add(optionalParams[listPointer + 1].ToString());
                                    }
                                }
                            }

                            this.LoadMasterForm();
                            if (this.ParentForm.Size.Width > this.MinimumSize.Width)
                            {
                                this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                                this.formHeaderSmartPartdeckWorkspace.ResumeLayout(true);
                            }
                            else
                            {
                                this.AllignControls();
                            }
                        }
                    }
                }
            }

            //// disable the recordnaviation as we came through support form call
            if (this.RecordNavigationDeckWorkspace.Visible && this.flagWithoutKeyId)
            {
                if (this.flagRecordNavigationVisible)
                {
                    this.RecordNavigationDeckWorkspace.Visible = true;
                }
            }
            else
            {
                if (this.queryEngineSmartPart == null)
                {
                    this.RecordNavigationDeckWorkspace.Visible = false;
                }
            }

            this.ShowDisabledScrollbar();
            this.flagFormExists = true;
            //tempPanel.Visible = false;
            //tempPanel.SendToBack();
            //if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
            //{
            //    if (this.collapsedWorkSpaces[0].ActiveSmartPart.ToString().Equals("D20003.F25011"))
            //    {
            //        this.pageMode = TerraScanCommon.PageModeTypes.View;
            //        this.LoadSlices();
            //        this.CancelButton_Click();
            //       // this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
            //        //this.LoadSlices();
            //    }
            //}

        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.formNo.ToString())
            {
                this.form9030control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
            }
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus())
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }


        /// <summary>
        /// Loads the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, Thread = ThreadOption.UserInterface)]
        public void LoadCancelButton(object sender, DataEventArgs<Button> e)
        {
            this.SetCancelButton(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Sets the form cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SetFormCancelButton, Thread = ThreadOption.UserInterface)]
        public void SetFormCancelButton(object sender, DataEventArgs<string> e)
        {
            this.GetCancelButton(this, new DataEventArgs<string>(string.Empty));
        }

        #region Reports

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, Thread = ThreadOption.UserInterface)]
        public void PrintButtonClick(object sender, DataEventArgs<Button> e)
        {
            //// Calling the Common Function for Report
            Hashtable ht = new Hashtable();
            ht.Add("KeyID", this.keyId);
            TerraScanCommon.ShowReport(Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.ReportColumn]), Report.ReportType.Print, ht);
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            Hashtable ht = new Hashtable();
            ht.Add("KeyID", this.keyId);
            // ht.Add(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.keyfieldColumn].ToString(), this.keyId);
            TerraScanCommon.ShowReport(Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.ReportColumn]), Report.ReportType.Preview, ht);
        }

        /// <summary>
        /// Handles the Click event of the EMailButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            ////// calling  Common Function For Report
            Hashtable ht = new Hashtable();
            ht.Add("KeyID", this.keyId);
            TerraScanCommon.ShowReport(Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.ReportColumn]), Report.ReportType.Email, ht);
        }

        #endregion reports

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_Resize, ThreadOption.UserInterface)]
        public void OnFormSlice_Resize(object sender, DataEventArgs<SliceResize> eventArgs)
        {
            if (eventArgs.Data.MasterFormNo == this.formNo)
            {
                this.flagFormResizing = true;
                bool formLoading = true;

                if (this.isJerkFree)
                {
                    if (this.loadedWebSlices > 0)
                    {
                        this.resizedWebSlices = this.resizedWebSlices + 1;
                        if (this.loadedWebSlices.Equals(this.resizedWebSlices))
                        {
                            this.resizedWebSlices = 0;
                            this.loadedWebSlices = 0;
                            this.isValidRecord = true;
                            formLoading = false;
                        }
                        else
                        {
                            formLoading = true;
                        }
                    }
                    else
                    {
                        if (this.tempPanel.Visible)
                        {
                            formLoading = true;
                        }
                        else
                        {
                            formLoading = false;
                        }
                    }

                    if (formLoading && !this.tempPanel.Visible)
                    {
                        tempPanel.Visible = true;
                        tempPanel.BringToFront();
                    }
                }

                this.sliceListPanel.AutoScroll = false;
                this.ExpandSlices(eventArgs.Data.SliceFormName, eventArgs.Data.SliceFormHeight);
                this.sliceListPanel.AutoScroll = true;

                if (this.isJerkFree)
                {
                    //if (!formLoading && !this.flagValidateKeyId)
                    if (!formLoading && this.isValidRecord)
                    {
                        tempPanel.Visible = false;
                        // tempPanel.SendToBack();
                        Application.UseWaitCursor = false;
                    }
                }

                this.flagFormResizing = false;
            }
        }

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_ExpandSlice, ThreadOption.UserInterface)]
        public void OnFormSlice_ExpandSlice(object sender, DataEventArgs<SliceResize> eventArgs)
        {
            if (eventArgs.Data.MasterFormNo == this.formNo)
            {
                this.flagFormResizing = true;
                this.sliceListPanel.AutoScroll = false;
                //tempPanel.Visible = true;
                //tempPanel.BringToFront();
                this.ExpandSlices(eventArgs.Data.SliceFormName);
                //tempPanel.Visible = false;
                //tempPanel.SendToBack();
                this.RefreshLoadedSlicePosition();
                this.sliceListPanel.AutoScroll = true;
                this.flagFormResizing = false;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9033_ on filter_ key id reset].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_OnFilter_KeyIdReset, ThreadOption.UserInterface)]
        public void OnD9030_F9033_OnFilter_KeyIdReset(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.formNo == eventArgs.Data.MasterFormNo)
            {
                this.RefreshFormMaster(eventArgs.Data.SelectedKeyId);
                this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009
                this.QueryEngineWorkSpace.BringToFront();
            }
        }

        /// <summary>
        /// Called when [OnD9030_F9033_OnChange_Neighborhood reset].TSBG - 30100 Neighborhood Form - NBHD Button Bug Fixing
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_OnChange_Neighborhood, ThreadOption.UserInterface)]
        public void OnD9030_F9033_OnChange_Neighborhood(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.formNo == eventArgs.Data.MasterFormNo)
            {
                this.SetMasterformGeneralProperties();
                this.RecordNavigationDeckWorkspace.Visible = true;
                this.RecordNavigationDeckWorkspace.BringToFront();
                SliceReloadActiveRecord sliceReloadActiveRecord;
                sliceReloadActiveRecord.MasterFormNo = this.formNo;
                sliceReloadActiveRecord.SelectedKeyId = this.previousKeyId;
                this.keyId = this.previousKeyId;
                this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
               
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ key id alert slice].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_KeyIdAlertSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_KeyIdAlertSlice(object sender, TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.formNo)
            {
                if (this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
                {
                    if (this.keyId <= 0)
                    {
                        this.OnKeyIdAlert();
                    }
                }

                this.flagFormExists = true;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_ReloadAfterSave, ThreadOption.UserInterface)]
        public void OnD9030_F9030_ReloadAfterSave(object sender, TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.formNo == eventArgs.Data.MasterFormNo)
            {
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.previousKeyId = this.keyId;
                int[] currentformno = new int[2];
                currentformno[0] = this.keyId;
                currentformno[1] = this.formNo;
                this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno));
                if (this.queryEngineSmartPart != null)
                {
                    if (!this.quickfindloadflag)
                    {
                        int currentKeyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "Save");
                        if (currentKeyId.Equals(-1))
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F9030WithoutQueryView"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (this.keyId != currentKeyId)
                            {
                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formNo;
                                sliceReloadActiveRecord.SelectedKeyId = currentKeyId;
                                this.keyId = currentKeyId;
                                this.previousKeyId = this.keyId;
                                this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            }

                            this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                            this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                            int[] currentRecordInfo = new int[2];
                            currentRecordInfo[0] = this.queryEngineSmartPart.CurrentRowIndex + 1;
                            currentRecordInfo[1] = this.queryEngineSmartPart.TotalRecords;
                            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                            this.LoadAuditLinkLabel();
                            if (this.FormIDLabel.Text == "9030 / 10031") 
                            {
                                this.ReloadSubTitleDetails(); 
                            }
                            //int[] currentformno = new int[2];
                            //currentformno[0] = this.keyId;
                            //currentformno[1] = this.formNo;
                            //this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno)); 
                        }
                    }
                    else
                    {
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.formNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.AuditlinkLabel.Visible = false;
                        this.RecordNavigationDeckWorkspace.Visible = false;  
                    }
                }
                else
                {
                    ////Commented by Purushotham Refresh Subtitles
                    //this.keyId = eventArgs.Data.SelectedKeyId;
                    //this.previousKeyId = this.keyId;
                    //if (this.FormIDLabel.Text == "9030 / 24500")
                    //{
                    //   // this.ReloadSubTitleDetails(); 
                    //}
                }
                ////Aded by purushotham
                this.keyId = eventArgs.Data.SelectedKeyId;
                this.previousKeyId = this.keyId;
                if (this.FormIDLabel.Text == "9030 / 24500")
                {
                    this.ReloadSubTitleDetails();
                }
                ////Code Commented for the issue 5358 by malliga on 18/1/2010
                //// this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_ReloadOtherSlice, ThreadOption.UserInterface)]
        public void OnD9030_F9030_ReloadOtherSlice(object sender, TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.formNo == eventArgs.Data.MasterFormNo)
                {
                    if (this.isJerkFree)
                    {
                        this.isJerkFree = false;
                        this.FormMaster_FormVisibility(false);
                    }

                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.previousKeyId = this.keyId;
                    int[] currentformno = new int[2];
                    currentformno[0] = this.keyId;
                    currentformno[1] = this.formNo;
                    this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno));
                    if (this.queryEngineSmartPart != null)
                    {
                        if (!this.quickfindloadflag)
                        {
                            int currentKeyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "Save");
                            if (currentKeyId.Equals(-1))
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F9030WithoutQueryView"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formNo;
                                sliceReloadActiveRecord.SelectedKeyId = currentKeyId;
                                this.keyId = currentKeyId;
                                this.previousKeyId = this.keyId;
                                this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                                this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                                this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                                int[] currentRecordInfo = new int[2];
                                currentRecordInfo[0] = this.queryEngineSmartPart.CurrentRowIndex + 1;
                                currentRecordInfo[1] = this.queryEngineSmartPart.TotalRecords;
                                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                                this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
                                this.SetMasterformGeneralProperties();
                            }
                        }
                        else
                        {
                            SliceReloadActiveRecord sliceReloadActiveRecord;
                            sliceReloadActiveRecord.MasterFormNo = this.formNo;
                            sliceReloadActiveRecord.SelectedKeyId = this.keyId ;
                            this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                            this.AuditlinkLabel.Text = string.Empty;
                            this.RecordNavigationDeckWorkspace.Visible = false;  
                        }
                    }
                    else
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        this.previousKeyId = this.keyId;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.isJerkFree)
                {
                    this.FormMaster_FormVisibility(true);
                    this.FormMaster_SetScrollPosition(this, new DataEventArgs<int>(this.formNo));
                }
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ reload after delete]. - Written for F2200 Edit schedule delete
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="TerraScan.Infrastructure.Interface.EventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        /// <returns></returns>
        [EventSubscription(EventTopicNames.D9030_F9030_ReloadAfterDelete, ThreadOption.UserInterface)]
        public void OnD9030_F9030_ReloadAfterDelete(object sender, TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this.formNo == eventArgs.Data.MasterFormNo)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    if (this.queryEngineSmartPart == null)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }

                    if (this.queryEngineSmartPart != null)
                    {
                        string expression = "IsPermissionDelete=True and SubForm<>9032";

                        DataRow[] checkedRow = null;
                        checkedRow = this.formMasterDataSet.FormSliceInformationList.Select(expression);

                        if (checkedRow.Length > 0)
                        {
                            this.queryEngineSmartPart.IdKeyIdDeleted = true;
                            this.keyId = this.queryEngineSmartPart.LoadQueryEngine(eventArgs.Data.SelectedKeyId, "Delete");
                        }

                        if (this.parameterCount > 0)
                        {
                            if (this.parameterList != null)
                            {
                                this.parameterList.Clear();
                                for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                {
                                    if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                    {
                                        this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                    }
                                }
                            }
                        }

                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.formNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.SetRecordNavigationProperties();
                        this.LoadAuditLinkLabel();

                        ////Code has been adeded to refresh Attachment & Comment form
                        this.SetAttachmentCommentsCount();

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        if (this.queryEngineSmartPart.TotalRecords == 0)
                        {
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                            this.AuditlinkLabel.Text = this.auditLinkLabelText;
                            this.AuditlinkLabel.Enabled = false;
                            this.additionalOperationSmartPart.Enabled = false;
                            this.reportActionSmartPart.Enabled = false;
                            this.isValidRecord = false;
                        }
                        else
                        {
                            this.isValidRecord = !this.flagValidateKeyId;
                        }

                        // Method added to track visited records on after Delete
                        this.TrackRecordHistory();

                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }

        }

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            //tempNavigationPanel.Visible = true;
            //tempNavigationPanel.BringToFront();

            tempPanel.Visible = true;
            tempPanel.BringToFront();

            this.flagNavigation = true;

            this.keyId = this.queryEngineSmartPart.AdvanceRecordPointer(e.Data.RecordIndex - 1);

            ////Code has been added for maintain the keyid 
            this.previousKeyId = this.keyId;

            if (this.parameterCount > 0)
            {
                if (this.parameterList != null)
                {
                    this.parameterList.Clear();
                    for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                    {
                        if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                        {
                            this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                        }
                    }
                }
            }
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.formNo;
            sliceReloadActiveRecord.SelectedKeyId = this.keyId;

            if (this.parameterCount > 0)
            {
                SliceReloadParaMeterizedActiveRecord sliceReloadParaMeterizedActiveRecord;
                sliceReloadParaMeterizedActiveRecord.MasterFormNo = this.formNo;
                sliceReloadParaMeterizedActiveRecord.SelectedKeyId = this.keyId;
                sliceReloadParaMeterizedActiveRecord.ParameterList = new List<string>();

                if (this.parameterList != null)
                {
                    for (int listItem = 0; listItem < this.parameterList.Count; listItem++)
                    {
                        if (this.parameterList[listItem] != null)
                        {
                            sliceReloadParaMeterizedActiveRecord.ParameterList.Add(this.parameterList[listItem]);
                        }
                    }
                }

                this.OnD9030_F9030_LoadParaMeterizedSliceDetails(new DataEventArgs<SliceReloadParaMeterizedActiveRecord>(sliceReloadParaMeterizedActiveRecord));
            }
            int[] currentformno = new int[2];
            currentformno[0] = this.keyId;
            currentformno[1] = this.formNo;
            this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno));

            this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

            this.RefreshLoadedSlicePosition();
            this.SetActiveRecord(this, new DataEventArgs<int>(e.Data.RecordIndex));
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { e.Data.RecordIndex, this.queryEngineSmartPart.TotalRecords }));
            this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
            ////this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009

            // Code added for Bug #5783: Jankyness Issue in webslice - Not getting formslice related info on navigation
            this.formMasterDataSet = this.form9030control.WorkItem.GetSandwichAndItsSliceInformation(this.formNo, this.keyId, this.userId);
            this.SetMasterformGeneralPropertiesWithNoDBCall();

            this.AllignControls();
            this.flagNavigation = false;

            // Method added to track visited records on Navigation
            this.isValidRecord = !this.flagValidateKeyId;
            this.TrackRecordHistory();

            if (this.loadedWebSlices.Equals(0))
            {
                tempPanel.Visible = false;
                Application.UseWaitCursor = false;
                tempPanel.SendToBack();
            }
            this.isJerkFree = true;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ raise form master new].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_RaiseFormMasterNew, ThreadOption.UserInterface)]
        public void OnD9030_F9030_RaiseFormMasterNew(object sender, TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                this.NewButton_Click();
            }
        }

        /// <summary>
        /// Called when [form slice_ cancel enabled].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.FormSlice_CancelEnabled, ThreadOption.UserInterface)]
        public void OnFormSlice_CancelEnabled(object sender, DataEventArgs<int> eventArgs)
        {
            if (!this.flagNavigation)
            {
                if (eventArgs.Data == this.formNo)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
            }
        }

        /// <summary>
        /// Called when [form slice_ null record mode].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.FormSlice_NullRecordMode, ThreadOption.UserInterface)]
        public void OnFormSlice_NullRecordMode(object sender, DataEventArgs<int> eventArgs)
        {
            if (eventArgs.Data == this.formNo)
            {
                if (this.pageMode != TerraScanCommon.PageModeTypes.New)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            }
        }

        /// <summary>
        /// Sets the GNRL properties.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_SetMasterGnrlPropertis, Thread = ThreadOption.UserInterface)]
        public void SetGnrlProperties(object sender, DataEventArgs<int[]> e)
        {
            if (e.Data[0] == this.formNo)
            {
                this.formMasterDataSet.FormSubTitle2.Clear();
                this.formMasterDataSet.FormSubTitle1.Clear();
                this.formMasterDataSet.BackgroundColor.Clear();

                this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(e.Data[0], e.Data[1], TerraScanCommon.UserId), false);
                this.keyId = e.Data[1];
                this.LoadSubtitles();
                this.LoadBackGroundColor();
                this.LoadAuditLinkLabel();
                this.SetAttachmentCommentsCount();

                // Code added to track visited records on Quick Find
                if (this.quickfindloadflag)
                {
                    this.isValidRecord = true;
                    this.TrackRecordHistory();
                }
            }
        }

        /// <summary>
        /// Sets the report additional properties.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32[]&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_EnableReportAdditonalProperties, Thread = ThreadOption.UserInterface)]
        public void SetReportAdditionalProperties(object sender, DataEventArgs<int[]> e)
        {
            if (e.Data[0] == this.formNo)
            {
                if (e.Data[2] == 1)
                {
                    this.reportActionSmartPartWorkSpace.Enabled = true;
                    this.additionalOperationSmartPartWorkspace.Enabled = true;
                }
                else
                {
                    this.reportActionSmartPartWorkSpace.Enabled = false;
                    this.additionalOperationSmartPartWorkspace.Enabled = false;
                    this.AuditlinkLabel.Text = this.auditLinkLabelText;
                    this.AuditlinkLabel.Enabled = false;
                }
            }
        }

        #endregion EventSubscription

        #region protected events

        /// <summary>
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_AlertResizableSlice(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_AlertResizableSlice != null)
            {
                this.D9030_F9030_AlertResizableSlice(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_LoadSliceDetails(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_LoadSliceDetails != null)
            {
                tempNavigationPanel.Visible = true;
                tempNavigationPanel.BringToFront();

                this.D9030_F9030_LoadSliceDetails(this, eventArgs);

                tempNavigationPanel.Visible = false;
                tempNavigationPanel.SendToBack();
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_QuickFind(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_LoadSliceDetails != null)
            {
                tempNavigationPanel.Visible = true;
                tempNavigationPanel.BringToFront();

                this.OnQuickFind_KeyId(this, eventArgs);

                tempNavigationPanel.Visible = false;
                tempNavigationPanel.SendToBack();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:D9030_F9030_LoadParaMeterizedSliceDetails"/> event.
        /// </summary>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        protected virtual void OnD9030_F9030_LoadParaMeterizedSliceDetails(DataEventArgs<SliceReloadParaMeterizedActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_LoadParaMeterizedSliceDetails != null)
            {
                this.D9030_F9030_LoadParaMeterizedSliceDetails(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ alert distinguished slice on close].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_AlertDistinguishedSliceOnClose(TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose> eventArgs)
        {
            if (this.D9030_F9030_AlertDistinguishedSliceOnClose != null)
            {
                this.D9030_F9030_AlertDistinguishedSliceOnClose(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ query engine close at form master].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_QueryEngineCloseAtFormMaster(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9030_QueryEngineCloseAtFormMaster != null)
            {
                this.D9030_F9030_QueryEngineCloseAtFormMaster(this, eventArgs);
            }
        }

        #endregion protected events

        #region PrivateMethods

        #region PrivateEvents

        /// <summary>
        /// Handles the LinkClicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.InvokeHelpEngine();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Enter event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9030_Enter(object sender, EventArgs e)
        {
            try
            {
                //tempPanel.Visible = true;
                //tempPanel.BringToFront();
                if (this.ParentForm.Size.Width > this.MinimumSize.Width)
                {
                    this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                    this.statusLabelOneFirstPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                    this.statusLabelOneSecondPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                    this.statusLabelTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                    this.AuditlinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
                    this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                    this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
                    this.QueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                    this.QueryEngineWorkSpace.Width = this.sliceListPanel.Width - 8;
                    this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;

                    //// Bug #785: Query Engine Width messed up
                    if (this.queryEngineSmartPart != null)
                    {
                        this.QueryEngineWorkSpace.Width = this.panel5.Width - 8;
                        this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;
                        this.queryEngineSmartPart.Width = this.panel5.Width;
                        this.queryEngineSmartPart.QueryEngineWidth = this.panel5.Width;
                        this.queryEngineSmartPart.PositionPictureBox = this.queryEngineSmartPart.Width;
                    }


                }
                else
                {
                    this.AllignControls();
                }
                //tempPanel.Visible = false;
                //tempPanel.SendToBack();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        /// <summary>
        /// Handles the Resize event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9030_Resize(object sender, EventArgs e)
        {
            try
            {
                // tempPanel.Visible = true;
                //tempPanel.BringToFront();
                if (this.ParentForm != null)
                {
                    if (this.ParentForm.Size.Width > this.MinimumSize.Width)
                    {
                        this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                        this.statusLabelOneFirstPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                        this.statusLabelOneSecondPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                        this.statusLabelTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                        this.AuditlinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
                        this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                        this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
                        this.QueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                        this.QueryEngineWorkSpace.Width = this.panel5.Width - 8;
                        this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;

                        if (this.queryEngineSmartPart != null)
                        {
                            this.queryEngineSmartPart.Width = this.panel5.Width;
                            this.queryEngineSmartPart.QueryEngineWidth = this.panel5.Width;
                            try
                            {
                                this.queryEngineSmartPart.PositionPictureBox = this.queryEngineSmartPart.Width;
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        this.AllignControls();
                    }

                    RefreshLoadedSlicePosition();
                    this.ShowDisabledScrollbar();
                }
                //tempPanel.Visible = false;
                //tempPanel.SendToBack();

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the QueryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.quickfindloadflag = false;
                //this.isQueryLoaded = true; 
                int tempKeyId = this.keyId;
                if (this.QueryEngineWorkSpace.Visible)
                {
                    tempPanel.Visible = true;
                    tempPanel.BringToFront();
                }

                this.DisplayQueryEngine();
                //this.isQueryLoaded = false;
                // ISSUE #785 FIXED
                // ISSUE DESCRIPTION : QUERY ENGINE WIDTH MESSED UP

                // STARTS HERE
                // FIX DESCRIPTION : QUERYENGINE SMARTPART RESIZED

                this.QueryEngineWorkSpace.Width = this.panel5.Width - 8;
                this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;

                if (this.queryEngineSmartPart != null)
                {
                    this.queryEngineSmartPart.Width = this.panel5.Width;
                    this.queryEngineSmartPart.QueryEngineWidth = this.panel5.Width;
                    this.queryEngineSmartPart.PositionPictureBox = this.queryEngineSmartPart.Width;
                    this.queryEngineSmartPart.Focus();

                    #region Bug #6002: [Regression] Delete button is getting disabled when Ctrl+Q is pressed in query engine.
                    if (this.queryEngineSmartPart.TotalRecords > 0 && this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    #endregion Bug #6002: [Regression] Delete button is getting disabled when Ctrl+Q is pressed in query engine.
                }

                // Code added to track visited records on Ctrl + Q functionality
                if (!this.QueryEngineWorkSpace.Visible)
                {
                    this.isValidRecord = !this.flagValidateKeyId;
                    this.TrackRecordHistory();
                }

                if (tempPanel.Visible)
                {
                    if (!tempKeyId.Equals(this.keyId))
                    {
                        DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                    "IsPermissionOpen = 'True'");
                        this.loadedWebSlices = webSliceRecords.Length * 2;

                        if (this.loadedWebSlices.Equals(0))
                        {
                            tempPanel.Visible = false;
                            Application.UseWaitCursor = false;
                            tempPanel.SendToBack();
                        }
                    }
                    else
                    {
                        tempPanel.Visible = false;
                        Application.UseWaitCursor = false;
                        tempPanel.SendToBack();
                    }
                }
                //else
                //{
                //    tempPanel.Visible = false;
                //    tempPanel.SendToBack();
                //}

                // ISSUE #785 FIX : ENDS HERE
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
        /// Handles the Load event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9030_Load(object sender, EventArgs e)
        {
            try
            {
                //tempPanel.Visible = true;
                //tempPanel.BringToFront();
                this.Cursor = Cursors.WaitCursor;
                this.LoadWorkSpaces();
                this.ParentForm.CancelButton = this.operationSmartPart.RetrieveCancelButton;
                this.formNo = Convert.ToInt32(this.Tag);
                //if (!this.FlagParametirized)
                //{
                //    tempPanel.Visible = false;
                //    tempPanel.SendToBack();
                //}

                ////New Form 9025 Validation
                /* Form newCommentTemplateForm = new Form();
                 Form UserLoginValidation = new Form();
                 newCommentTemplateForm = TerraScanCommon.ShowFormValidation(this.formNo);
                 this.LoginUserValidation = Convert.ToBoolean(newCommentTemplateForm.Text.ToString());
                 if (this.LoginUserValidation.Equals(true))
                 {
                     bool value = TerraScanCommon.AdminUserValidationForm(this.form9030control.WorkItem);
                     //UserLoginValidation = TerraScanCommon.AdminUserValidationForm();
                     //bool value = Convert.ToBoolean(UserLoginValidation.Text.ToString());
                 } */
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
        /// Handles the SmartPartClosing event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs"/> instance containing the event data.</param>
        private void F9030_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            try
            {
                this.form9030control.WorkItem.Items.Remove(e.SmartPart);
                this.tempPanel.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.formNo;
                formInfo.optionalParameters[1] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the MouseEnter event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9030_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                PictureBox sectionIndicator = (PictureBox)sender;
                this.SectionIndicatorToolTip.SetToolTip(sectionIndicator, sectionIndicator.Tag.ToString());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the F9030 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9030_Click(object sender, EventArgs e)
        {
            try
            {
                this.flagFormResizing = true;
                this.sliceListPanel.AutoScroll = false;

                tempPanel.Visible = true;
                tempPanel.BringToFront();

                this.ExpandSlices(((PictureBox)sender).Tag.ToString());
                // this.ExpandSlices(((PictureBox)sender).Tag.ToString(), 500);

                tempPanel.Visible = false;
                tempPanel.SendToBack();
                Application.UseWaitCursor = false;

                this.RefreshLoadedSlicePosition();
                this.sliceListPanel.AutoScroll = true;
                this.flagFormResizing = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the HelplinkLabel.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void HelplinkLabel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                ///code to disable the focus on QueryButton-tabbing issue
                if (e.KeyValue.Equals((int)Keys.Up) || e.KeyValue.Equals((int)Keys.Down) || e.KeyValue.Equals((int)Keys.Left) || e.KeyValue.Equals((int)Keys.Right))
                {
                    this.QueryButton.TabIndex = 999;
                    this.AuditlinkLabel.TabIndex = this.HelplinkLabel.TabIndex + 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        ///code added by kuppu to disable the focus on QueryButton
        /// <summary>
        /// Handles the Enter event of the QueryButton.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryButton_Enter(object sender, EventArgs e)
        {
            try
            {
                ///code to disable the focus on QueryButton-tabbing issue
                this.HelplinkLabel.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion PrivateEvents

        #region Form Master controls Loading Related

        /// <summary>
        /// Loadtitleses this instance.
        /// </summary>
        private void LoadTitles()
        {
            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.MenuNameColumn].ToString();
            formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));
            this.FormIDLabel.Text = string.Empty;
            this.FormIDLabel.Text = "9030 / " + this.formNo.ToString();
            ////this.LoadSubtitles(); 
            ////this.LoadAuditLinkLabel();
        }

        #region coding Added for the CO:5026(BackGround Color on F9030)
        /// <summary>
        /// Loads the color of the back ground.
        /// </summary>
        private void LoadBackGroundColor()
        {
            string backcolor;
            string[] backcolorArr = null;
            int RColor;
            int GColor;
            int BColor;

            ////if Background color table have a value
            if (this.formMasterDataSet.BackgroundColor.Rows.Count > 0 && this.keyId > 0)
            {
                ////assigning backcolor value
                backcolor = this.formMasterDataSet.BackgroundColor.Rows[0][this.formMasterDataSet.BackgroundColor.BackgroundColorColumn].ToString();
                if (!string.IsNullOrEmpty(backcolor))
                {
                    char[] splitchar = { ',' };
                    backcolorArr = backcolor.Split(splitchar);
                    if (backcolorArr.Length.Equals(3))
                    {
                        ////Getting Red Color
                        if (string.IsNullOrEmpty(backcolorArr[0]))
                        {
                            RColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[0], out RColor);
                        }

                        ////Getting Green Color
                        if (string.IsNullOrEmpty(backcolorArr[1]))
                        {
                            GColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[1], out GColor);
                        }

                        ////Getting Blue Color
                        if (string.IsNullOrEmpty(backcolorArr[2]))
                        {
                            BColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[2], out BColor);
                        }

                        ////Assign RGB value to form backcolor
                        if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                        {
                            RColor = 255;
                            GColor = 255;
                            BColor = 255;
                        }
                        this.BackColor = Color.FromArgb(RColor, GColor, BColor);
                        this.tempPanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                        tempNavigationPanel.BackColor = Color.FromArgb(RColor, GColor, BColor);
                    }
                    else
                    {
                        this.setWhiteColor();
                    }
                }
                else
                {
                    this.setWhiteColor();
                }
            }
            else
            {
                this.setWhiteColor();
            }
        }

        /// <summary>
        /// Sets the color of the white.
        /// </summary>
        private void setWhiteColor()
        {
            //Assign white color to form backcolor
            this.BackColor = Color.White;
            this.tempPanel.BackColor = Color.White;
            tempNavigationPanel.BackColor = Color.White;
        }

        #endregion

        /// <summary>
        /// Loads the subtitles.
        /// </summary>
        private void LoadSubtitles()
        {
            string[] titleArray;
            this.statusLabelOneFirstPart.Text = string.Empty;
            this.statusLabelOneSecondPart.Text = string.Empty;
            this.statusLabelOneSecondPart.Text = string.Empty;
            if (this.formMasterDataSet.FormSubTitle1.Rows.Count > 0)
            {
                titleArray = this.formMasterDataSet.FormSubTitle1.Rows[0][this.formMasterDataSet.FormSubTitle1.SubTitle1Column].ToString().Split('/');
                if (titleArray.GetLength(0) <= 1)
                {
                    this.statusLabelOneSecondPart.Text = this.formMasterDataSet.FormSubTitle1.Rows[0][this.formMasterDataSet.FormSubTitle1.SubTitle1Column].ToString();
                    this.statusLabelOneSecondPart.Visible = true;
                    this.statusLabelOneFirstPart.Visible = false;
                    this.statusLabelOneSecondPart.ForeColor = Color.FromArgb(28, 80, 129);
                    // this.statusLabelOneSecondPart.Left = this.sliceListPanel.Left + this.sliceListPanel.Width - this.statusLabelOneSecondPart.Width;
                    this.statusLabelOneSecondPart.Left = this.panel5.Left + this.panel5.Width - this.statusLabelOneSecondPart.Width + 5;
                }
                else
                {
                    this.statusLabelOneFirstPart.Text = titleArray[0];
                    this.statusLabelOneFirstPart.Visible = true;
                    this.statusLabelOneFirstPart.ForeColor = Color.FromArgb(28, 80, 129);
                    this.statusLabelOneSecondPart.ForeColor = Color.FromArgb(217, 104, 13);
                    this.statusLabelOneSecondPart.Visible = true;
                    for (int currentString = 1; currentString < titleArray.GetLength(0); currentString++)
                    {
                        if (currentString != 1)
                        {
                            this.statusLabelOneSecondPart.Text += " /" + titleArray[currentString];
                        }
                        else
                        {
                            this.statusLabelOneFirstPart.Text += " /";
                            this.statusLabelOneSecondPart.Text += titleArray[currentString];
                        }
                    }

                    this.statusLabelOneSecondPart.Left = this.panel5.Left + this.panel5.Width - this.statusLabelOneSecondPart.Width + 5;
                    this.statusLabelOneFirstPart.Left = (this.statusLabelOneSecondPart.Left - this.statusLabelOneFirstPart.Width);

                    ////this.statusLabelOneSecondPart.Left = this.statusLabelOneSecondPart.Left - 5;
                }

                // this.statusLabelOneFirstPart.RightToLeft = RightToLeft.Yes;
                this.statusLabelOneFirstPart.RightToLeft = RightToLeft.No;
                this.statusLabelOneSecondPart.RightToLeft = RightToLeft.No;

                if (this.formMasterDataSet.FormSubTitle2.Rows.Count > 0)
                {
                    this.statusLabelTwo.Text = this.formMasterDataSet.FormSubTitle2.Rows[0][this.formMasterDataSet.FormSubTitle2.SubTitle2Column].ToString();
                    this.statusLabelTwo.Left = this.panel5.Left + this.panel5.Width - this.statusLabelTwo.Width + 5;
                }
            }
        }

        /// <summary>
        /// Loads the audit link label.
        /// </summary>
        private void LoadAuditLinkLabel()
        {
            if (this.formMasterDataSet.FormSandwichDetails.Rows.Count > 0)
            {
                if (Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsAuditVisibleColumn]))
                {
                    this.AuditlinkLabel.Visible = true;
                    this.auditLinkLabelText = this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.AuditLinkLabelColumn].ToString();
                    if (this.keyId != -99)
                    {
                        if (this.quickfindloadflag)
                        {
                            this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
                            this.AuditlinkLabel.Enabled = true;
                        }
                        else
                        {
                            if (this.flagQueryEngineVisible)
                            {
                                if (this.queryEngineSmartPart != null && this.queryEngineSmartPart.TotalRecords > 0)
                                {
                                    this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
                                    this.AuditlinkLabel.Enabled = true;
                                }
                                else
                                {
                                    this.AuditlinkLabel.Text = this.auditLinkLabelText;
                                    this.AuditlinkLabel.Enabled = false;
                                }
                            }
                            else
                            {
                                this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
                                this.AuditlinkLabel.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        this.AuditlinkLabel.Text = this.auditLinkLabelText;
                        this.AuditlinkLabel.Enabled = false;
                    }
                }
                else
                {
                    this.AuditlinkLabel.Visible = false;
                }

                this.AllignAuditLink();
            }
        }

        /// <summary>
        /// Loads the buttons.
        /// </summary>
        private void LoadButtons()
        {
            this.operationSmartPart.NewButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsNewVisibleColumn]);
            this.operationSmartPart.SaveButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsSaveVisibleColumn]);
            this.operationSmartPart.CancelButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsCancelVisibleColumn]);
            this.operationSmartPart.DeleteButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsDeleteVisibleColumn]);
            this.reportActionSmartPart.PrintButtonVisible = false;
            this.reportActionSmartPart.PreviewButtonVisible = false;
            this.reportActionSmartPart.EmailButtonVisible = false;
            this.reportActionSmartPart.DetailsButtonVisible = false;
            if (Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsPrintVisibleColumn]))
            {
                this.reportActionSmartPart.PrintButtonVisible = true;
            }
            else
            {
                this.reportActionSmartPart.PrintButtonVisible = false;
            }

            if (Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsPreviewVisibleColumn]))
            {
                this.reportActionSmartPart.PreviewButtonVisible = true;
            }
            else
            {
                this.reportActionSmartPart.PreviewButtonVisible = false;
            }

            if (Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsEmailVisibleColumn]))
            {
                this.reportActionSmartPart.EmailButtonVisible = true;
            }
            else
            {
                this.reportActionSmartPart.EmailButtonVisible = false;
            }

            this.additionalOperationSmartPart.AttachmentButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsAttachmentsVisibleColumn]);
            this.additionalOperationSmartPart.CommentButtonVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsCommentsVisibleColumn]);
            this.reportActionSmartPartWorkSpace.Width = this.reportActionSmartPart.VisibleButtonLength;
            this.additionalOperationSmartPartWorkspace.Left = this.reportActionSmartPartWorkSpace.Left + this.reportActionSmartPartWorkSpace.Width;
            this.additionalOperationSmartPart.ParentWorkItem = this.form9030control.WorkItem;
            this.additionalOperationSmartPart.IsCommentFormIdDiffers = true;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.AttachmentAsColumn]);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.AttachmentAsColumn]);
            this.additionalOperationSmartPart.CommentFormId = Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.CommentAsColumn]);
            ////Commented by Biju I.G. to on 17/Dec/2009 fix the multiple attachment/Comment DB calls
            ////this.additionalOperationSmartPart.KeyId = this.keyId;
        }

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.form9030control.WorkItem.Items.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPartWorkSpace.Show((OperationSmartPart)this.form9030control.WorkItem.Items.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.operationSmartPart = this.form9030control.WorkItem.Items.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }

            if (this.form9030control.WorkItem.Items.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.reportActionSmartPartWorkSpace.Show((ReportActionSmartPart)this.form9030control.WorkItem.Items.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.reportActionSmartPart = this.form9030control.WorkItem.Items.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart);
                this.reportActionSmartPartWorkSpace.Show(this.reportActionSmartPart);
            }

            if (this.form9030control.WorkItem.Items.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPartWorkspace.Show((AdditionalOperationSmartPart)this.form9030control.WorkItem.Items.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.additionalOperationSmartPart = this.form9030control.WorkItem.Items.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
                this.additionalOperationSmartPartWorkspace.Show(this.additionalOperationSmartPart);
            }

            if (this.form9030control.WorkItem.Items.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show((FormHeaderSmartPart)this.form9030control.WorkItem.Items.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form9030control.WorkItem.Items.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.form9030control.WorkItem.Items.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigationDeckWorkspace.Show((RecordNavigatorSmartPart)this.form9030control.WorkItem.Items.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigationDeckWorkspace.Show(this.form9030control.WorkItem.Items.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            this.operationSmartPartWorkSpace.TabStop = false;

            foreach (UserControl currentSmartPart in this.operationSmartPartWorkSpace.SmartParts)
            {
                if (currentSmartPart != null)
                {
                    currentSmartPart.TabStop = false;
                }
            }

            this.reportActionSmartPartWorkSpace.TabStop = false;

            foreach (UserControl currentSmartPart in this.reportActionSmartPartWorkSpace.SmartParts)
            {
                if (currentSmartPart != null)
                {
                    currentSmartPart.TabStop = false;
                }
            }

            this.additionalOperationSmartPartWorkspace.TabStop = false;

            foreach (UserControl currentSmartPart in this.additionalOperationSmartPartWorkspace.SmartParts)
            {
                if (currentSmartPart != null)
                {
                    currentSmartPart.TabStop = false;
                }
            }

            this.formHeaderSmartPartdeckWorkspace.TabStop = false;

            foreach (UserControl currentSmartPart in this.formHeaderSmartPartdeckWorkspace.SmartParts)
            {
                if (currentSmartPart != null)
                {
                    currentSmartPart.TabStop = false;
                }
            }

            this.RecordNavigationDeckWorkspace.TabStop = false;

            foreach (UserControl currentSmartPart in this.RecordNavigationDeckWorkspace.SmartParts)
            {
                if (currentSmartPart != null)
                {
                    currentSmartPart.TabStop = false;
                }
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form9030control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form9030control.WorkItem;
        }

        #endregion Form Master controls Loading Related

        #region General Form Control Initialization

        /// <summary>
        /// Method to set the form master general properties
        /// such as audit link label, title1, tilte2 and
        /// form header
        /// </summary>
        private void SetMasterformGeneralProperties()////method name spelling mistake corrected by Biju on 30/12/2009
        {
            this.formMasterDataSet.FormSubTitle2.Clear();
            this.formMasterDataSet.FormSubTitle1.Clear();
            this.formMasterDataSet.BackgroundColor.Clear();
            this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), false);
            this.LoadSubtitles();
            this.LoadBackGroundColor();
            this.LoadAuditLinkLabel();
            this.SetAttachmentCommentsCount();
        }
        /// <summary>
        /// Method to re call subtitles 
        /// </summary>
        private void ReloadSubTitleDetails()
        {
            this.formMasterDataSet.FormSubTitle2.Clear();
            this.formMasterDataSet.FormSubTitle1.Clear();
            this.formMasterDataSet.BackgroundColor.Clear();
            this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), false);
            this.LoadSubtitles();
        }

        /// <summary>
        /// Method to replicate SetMasterformGeneralProperties() method
        /// but with no DB call
        /// Created by Biju on 30/Dec/2009
        /// </summary>
        private void SetMasterformGeneralPropertiesWithNoDBCall()
        {
            this.LoadSubtitles();
            this.LoadBackGroundColor();
            this.LoadAuditLinkLabel();
            this.SetAttachmentCommentsCount();
        }
        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                ////Added by Biju on 19/Jan/2010 to fix the clearing of attachment/comment button count
                ////when NEW button is clicked
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                if ((this.keyId == -99) || (this.pageMode == TerraScanCommon.PageModeTypes.New))
                {
                    this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
                    this.additionalOperationSmartPartWorkspace.Enabled = false;
                }
                else
                {
                    this.additionalOperationSmartPartWorkspace.Enabled = true;
                    ////Commented by Biju on 19/Jan/2010 to fix the clearing of attachment/comment button count
                    ////when NEW button is clicked
                    ////AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

                    ////Added by Biju I.G. on 17/Dec/2009 to fix #4672
                    if (!this.additionalOperationSmartPart.KeyId.Equals(this.keyId) || this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                    {
                        ////till here
                        this.additionalOperationSmartPart.KeyId = this.keyId;
                        additionalOperationCountEntity.AttachmentCount = this.form9030control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.AttachmentAsColumn]), this.keyId, TerraScanCommon.UserId);
                        CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form9030control.WorkItem.GetCommentsCount(Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.CommentAsColumn]), this.keyId, TerraScanCommon.UserId);
                        if (commentsCountDataTable.Rows.Count > 0)
                        {
                            additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                            additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                        }

                        this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
                    }////Added by Biju I.G. on 17/Dec/2009 to fix #4672
                }

                if (this.QueryEngineWorkSpace.Visible)
                {
                    this.additionalOperationSmartPart.Enabled = false;
                }
                else
                {
                    if (this.keyId == -99 || this.flagValidateKeyId)          //|| this.keyId==-1
                    {
                        ////Modified by purushotham on 19/Mar/13 to fix #17823
                        this.additionalOperationSmartPart.Enabled = false;
                        if (!(this.pageMode == TerraScanCommon.PageModeTypes.New) && (!(this.isCancelClick == true)) && (this.formMasterDataSet.FormSandwichDetails.Rows[0]["IsQueryVisible"].ToString().ToUpper().Equals("TRUE")))
                        {
                            this.sliceListPanel.Controls.Clear();
                            this.sliceListPanel.BorderStyle = System.Windows.Forms.BorderStyle.None;
                           // this.operationSmartPart.DeleteButtonEnable = false;
                            this.isCancelClick = false;
                            isClear = true;
                        }
                       
                    }
                    else
                    {
                        this.sliceListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        this.additionalOperationSmartPart.Enabled = true;
                    }
                }

                if (this.queryEngineSmartPart != null)
                {
                    if (this.quickfindloadflag)
                    {
                        this.additionalOperationSmartPart.Enabled = true;
                        this.reportActionSmartPart.Enabled = true;
                    }
                    else
                    {
                        if (this.queryEngineSmartPart.TotalRecords == 0)
                        {
                            this.additionalOperationSmartPart.Enabled = false;
                            this.reportActionSmartPart.Enabled = false;
                        }
                        else
                        {
                            this.reportActionSmartPartWorkSpace.Enabled = true;
                            this.additionalOperationSmartPart.Enabled = true;
                            this.reportActionSmartPart.Enabled = true;
                            //this.sliceListPanel.Enabled = true;
                            //this.verticalScrollBar.Enabled = true;
                            //this.sliceListPanel.Visible = true;
                            //this.verticalScrollBar.Visible = true;
                                                    
                        }
                    }
                }
                else ////Code added to enable print/preview buttons for GDOC forms ---- Have to check
                {
                    if (this.keyId == -99 || this.flagValidateKeyId)
                    {
                        this.reportActionSmartPartWorkSpace.Enabled = false;
                    }
                    else
                    {
                        this.reportActionSmartPartWorkSpace.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the record navigation properties.
        /// </summary>
        private void SetRecordNavigationProperties()
        {
            if (this.queryEngineSmartPart != null)
            {
                if (!this.keyId.Equals(-99))
                {
                    this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                    int[] currentRecordInfo = new int[2];

                    if (this.queryEngineSmartPart.TotalRecords > 0)
                    {
                        currentRecordInfo[0] = this.queryEngineSmartPart.CurrentRowIndex + 1;
                        this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                    }
                    else
                    {
                        currentRecordInfo[0] = 0;
                        this.SetActiveRecord(this, new DataEventArgs<int>(0));
                    }

                    currentRecordInfo[1] = this.queryEngineSmartPart.TotalRecords;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                }
                else
                {
                    if (this.queryEngineSmartPart.TotalRecords >= 0)
                    {
                        this.SetRecordCount(this, new DataEventArgs<int>(0));
                        this.SetActiveRecord(this, new DataEventArgs<int>(0));
                        int[] currentRecordInfo = new int[2];
                        currentRecordInfo[0] = 0;
                        currentRecordInfo[1] = 0;
                        this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                    }
                }
            }
        }

        /// <summary>
        /// Invokes the help engine.
        /// </summary>
        private void InvokeHelpEngine()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.formNo.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion General Form Control Initialization

        #region Dynamic positioning related

        /// <summary>
        /// Shows the disabled scrollbar.
        /// </summary>
        private void ShowDisabledScrollbar()
        {
            this.CalculateDisplayAreaHeightAndLocation();
            if (this.ycoorPosition > this.sliceDisplayAreaHeight)
            {
                this.verticalScrollBar.Location = new Point(824, this.verticalScrollBar.Location.Y);
                this.verticalScrollBar.SendToBack();
                this.verticalScrollBar.Visible = false;
                this.sliceListPanel.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.verticalScrollBar.Location = new Point(824, this.verticalScrollBar.Location.Y);
                this.verticalScrollBar.BringToFront();
                this.verticalScrollBar.Visible = false;
                this.sliceListPanel.BorderStyle = BorderStyle.None;
            }
        }

        /// <summary>
        /// Firsts the row visible.
        /// </summary>
        /// <returns>flag for identifying first row visibility</returns>
        private bool FirstRowVisible()
        {
            #region Previous Implimentation

            //// if (this.reportActionSmartPart != null)
            //// {
            ////     if (this.reportActionSmartPartWorkSpace.Visible)
            ////     {
            ////         if ((!this.reportActionSmartPart.PreviewButtonVisible && !this.reportActionSmartPart.PrintButtonVisible && !this.reportActionSmartPart.EmailButtonVisible) && (string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim() + this.statusLabelOneSecondPart.Text.Trim() + this.formHeaderString.Trim())))
            ////         {
            ////             return false;
            ////         }
            ////     }
            ////     else
            ////     {
            ////         if (string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim() + this.statusLabelOneSecondPart.Text.Trim() + this.formHeaderString.Trim()))
            ////         {
            ////             return false;
            ////         }
            ////     }
            ////}
            ////else
            ////{
            ////     if (string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim() + this.statusLabelOneSecondPart.Text.Trim() + this.formHeaderString.Trim()))
            ////     {
            ////         return false;
            ////     }
            //// }
            //// return true;

            #endregion Previous Implimentation

            bool reportVisible, additionalOperationVisible, formHeaders;

            #region check whether reportsmartpart available

            if (this.reportActionSmartPartWorkSpace.Visible == false)
            {
                reportVisible = false;
            }
            else
            {
                if (this.reportActionSmartPart != null)
                {
                    if (!this.reportActionSmartPart.PreviewButtonVisible && !this.reportActionSmartPart.PrintButtonVisible && !this.reportActionSmartPart.EmailButtonVisible)
                    {
                        reportVisible = false;
                    }
                    else
                    {
                        reportVisible = true;
                    }
                }
                else
                {
                    reportVisible = false;
                }
            }

            #endregion check whether reportsmartpart available

            #region check whether additionaloperationsmartpart available

            if (this.additionalOperationSmartPartWorkspace.Visible == false)
            {
                additionalOperationVisible = false;
            }
            else
            {
                if (this.additionalOperationSmartPart != null)
                {
                    if (!this.additionalOperationSmartPart.AttachmentButtonVisible && !this.additionalOperationSmartPart.CommentButtonVisible)
                    {
                        additionalOperationVisible = false;
                    }
                    else
                    {
                        additionalOperationVisible = true;
                    }
                }
                else
                {
                    additionalOperationVisible = false;
                }
            }

            #endregion check whether additionaloperationsmartpart available

            #region check whether formheader available

            if (string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim() + this.statusLabelOneSecondPart.Text.Trim() + this.formHeaderString.Trim()))
            {
                formHeaders = false;
            }
            else
            {
                formHeaders = true;
            }

            #endregion check whether formheader available

            #region check whether first row visible

            if (!reportVisible && !additionalOperationVisible && !formHeaders)
            {
                return false;
            }
            else
            {
                return true;
            }

            #endregion check whether first row visible
        }

        /// <summary>
        /// Seconds the row visible.
        /// </summary>
        /// <returns>flag for identifying second row visibility</returns>
        private bool SecondRowVisible()
        {
            if (this.operationSmartPart != null)
            {
                if (this.operationSmartPartWorkSpace.Visible)
                {
                    if ((!this.operationSmartPart.NewButtonVisible && !this.operationSmartPart.SaveButtonVisible && !this.operationSmartPart.CancelButtonVisible && !this.operationSmartPart.DeleteButtonVisible) && string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()))
                    {
                        return false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Navigations the visible.
        /// </summary>
        /// <returns>flag for identifying navigation buttons visibility</returns>
        private bool NavigationVisible()
        {
            if (!this.RecordNavigationDeckWorkspace.Visible && !this.QueryButton.Visible)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Calculates the height of the display area.
        /// </summary>
        private void CalculateDisplayAreaHeightAndLocation()
        {
            //tempPanel.Visible = true;
            //tempPanel.BringToFront();

            bool firstRowVisible, secondRowVisible, queryEngineVisible;
            int otherLeftOutHeight = 70;

            this.sliceListPanel.Top = 0;

            secondRowVisible = this.SecondRowVisible();
            firstRowVisible = this.FirstRowVisible();
            queryEngineVisible = this.NavigationVisible();

            if (secondRowVisible)
            {
                otherLeftOutHeight = otherLeftOutHeight + 50;
                this.sliceListPanel.Top += 46;

                ////Code commented to enable shortcut keys

                //if (null != this.operationSmartPart)
                //{
                //    this.operationSmartPart.OnOffShortCutKeys = true;
                //}
            }
            //else
            //{
            //    if (null != this.operationSmartPart)
            //    {
            //        this.operationSmartPart.OnOffShortCutKeys = false;
            //    }
            //}

            ////Enable/Disable shortcut keys based on the presents of form master operations buttons
            if (null != this.operationSmartPart)
            {
                if (!this.operationSmartPart.NewButtonVisible && !this.operationSmartPart.SaveButtonVisible && !this.operationSmartPart.CancelButtonVisible && !this.operationSmartPart.DeleteButtonVisible)
                {
                    ////Enable Form level short cut keys
                    this.operationSmartPart.OnOffShortCutKeys = false;
                }
                else
                {
                    //Enable Form Master short cut keys
                    this.operationSmartPart.OnOffShortCutKeys = true;
                }
            }

            if (firstRowVisible)
            {
                otherLeftOutHeight = otherLeftOutHeight + 55;
                this.sliceListPanel.Top += 51;
                this.MoveSecondRowTofirst(false);
            }
            else
            {
                this.MoveSecondRowTofirst(true);
            }

            if (queryEngineVisible)
            {
                otherLeftOutHeight = otherLeftOutHeight + 50;
            }

            this.sliceDisplayAreaHeight = this.Height - otherLeftOutHeight;
            this.sliceListPanel.Height = this.sliceDisplayAreaHeight;
            this.statusLabelOneSecondPart.Left = this.panel5.Left + this.panel5.Width - this.statusLabelOneSecondPart.Width + 5;
            this.statusLabelOneSecondPart.Left = this.panel5.Left + this.panel5.Width - this.statusLabelOneSecondPart.Width + 5;
            this.statusLabelTwo.Left = (this.statusLabelOneSecondPart.Left + this.statusLabelOneSecondPart.Width) - this.statusLabelTwo.Width + 5;

            //tempPanel.Visible = false;
            //tempPanel.SendToBack();
            // this.statusLabelTwo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        }

        /// <summary>
        /// Moves the second row tofirst.
        /// </summary>
        /// <param name="moveToFirst">if set to <c>true</c> [move to first].</param>
        private void MoveSecondRowTofirst(bool moveToFirst)
        {
            // write code to move the buttons up
            if (moveToFirst)
            {
                this.operationSmartPartWorkSpace.Top = 3;
                this.statusLabelTwo.Top = 8;
            }
            else
            {
                this.operationSmartPartWorkSpace.Top = 48;
                this.statusLabelTwo.Top = 53;
            }
        }

        #endregion Dynamic positioning related

        #region Form Master Related

        /// <summary>
        /// Loads the master form.
        /// </summary>
        private void LoadMasterForm()
        {
            // this.GetSandwichAndItsSliceInformation();
            if (this.formMasterDataSet.FormSandwichDetails.Rows.Count > 0)
            {
                // Close Activity Queue form if it is in Active state 
                // Code executes only for External component
                if (TerraScanCommon.activityForm != null)
                {
                    TerraScanCommon.activityForm.Close();
                    TerraScanCommon.activityForm = null;
                }

                this.LoadTitles();
                if (this.flagQueryEngineVisible)
                {
                    this.LoadQueryEngine();
                    ////Modified by Biju on 30/Dec/2009 to avoid multiple calls of f9030_pcget_FormMaster
                    ////the flag checking is added because for IsQueryLoad = True, the sub titles and background 
                    ////color was not setting correctly on form load. Only in this scenario, the DB call will 
                    ////go once again.
                    if (flagQueryLoad)
                    {
                        this.formMasterDataSet.FormSubTitle2.Clear();
                        this.formMasterDataSet.FormSubTitle1.Clear();
                        this.formMasterDataSet.BackgroundColor.Clear();
                        this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), true);
                    }
                    ////till here
                    this.LoadSubtitles();
                    this.LoadBackGroundColor();
                    this.LoadAuditLinkLabel();
                    this.LoadButtons();
                    this.LoadSlices();

                    // code to be performed when invalid keyid detected
                    #region raise message box on invalid keyid

                    if (this.flagValidateKeyId)
                    {
                        if (this.queryEngineSmartPart != null && this.queryEngineSmartPart.TotalRecords <= 0)
                        {
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                            if (this.queryEngineSmartPart.TotalRecords <= 0)
                            {
                                #region show message and proceed accroding to it
                                if (!this.flagWithoutKeyId)//&& !this.flagFormExists)
                                {
                                    if (this.flagFormExists)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("F9030DeafultAlertAfterValidKeyId"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "InvalidKey");
                                        SliceReloadActiveRecord sliceReloadActiveRecord;
                                        sliceReloadActiveRecord.MasterFormNo = this.formNo;
                                        sliceReloadActiveRecord.SelectedKeyId = this.previousKeyId;
                                        this.keyId = this.previousKeyId;
                                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                                        if (this.flagRecordNavigationVisible)
                                        {
                                            this.RecordNavigationDeckWorkspace.Visible = true;
                                        }
                                        else
                                        {
                                            this.RecordNavigationDeckWorkspace.Visible = false;
                                        }

                                        this.formMasterDataSet.FormSubTitle2.Clear();
                                        this.formMasterDataSet.FormSubTitle1.Clear();
                                        this.formMasterDataSet.BackgroundColor.Clear();
                                        this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), true);
                                        this.LoadSubtitles();
                                        this.LoadBackGroundColor();
                                        this.LoadAuditLinkLabel();
                                        this.LoadButtons();
                                        this.LoadSlices();
                                    }
                                    else
                                    {
                                        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                        if (DialogResult.OK == dialogResult)
                                        {
                                            this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "InvalidKey");

                                            this.flagValidateKeyId = false;

                                            this.tempPanel.Visible = true;
                                            this.tempPanel.BringToFront();

                                            this.ActivateQueryEngine();
                                            if (this.queryEngineSmartPart.TotalRecords > 0)
                                            {
                                                this.keyId = this.queryEngineSmartPart.AdvanceRecordPointer(0);
                                                if (this.parameterCount > 0)
                                                {
                                                    if (this.parameterList != null)
                                                    {
                                                        this.parameterList.Clear();
                                                        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                                        {
                                                            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                                            {
                                                                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                                            }
                                                        }
                                                    }
                                                }

                                                this.previousKeyId = this.keyId;
                                            }
                                            else
                                            {
                                                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                                this.keyId = -99;
                                            }

                                            if (this.flagRecordNavigationVisible)
                                            {
                                                this.RecordNavigationDeckWorkspace.Visible = true;
                                            }
                                            else
                                            {
                                                this.RecordNavigationDeckWorkspace.Visible = false;
                                            }

                                            this.formMasterDataSet.FormSubTitle2.Clear();
                                            this.formMasterDataSet.FormSubTitle1.Clear();
                                            this.formMasterDataSet.BackgroundColor.Clear();
                                            this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), true);
                                            this.LoadSubtitles();
                                            this.LoadBackGroundColor();
                                            this.LoadAuditLinkLabel();
                                            this.LoadButtons();
                                            this.LoadSlices();
                                        }
                                        else
                                        {
                                            this.queryEngineSmartPart = null;
                                            this.ParentForm.Close();
                                        }
                                    }
                                #endregion show message and proceed accroding to it
                                }
                            }
                        }
                        else
                        {
                            #region show message and proceed accroding to it
                            if (this.flagFormExists)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F9030DeafultAlertAfterValidKeyId"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                ////To empty QueryEngine
                                this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "InvalidKey");
                                //this.keyId = -99;

                                this.tempPanel.Visible = true;
                                this.tempPanel.BringToFront();

                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formNo;
                                sliceReloadActiveRecord.SelectedKeyId = this.previousKeyId;


                                if (this.keyId != this.previousKeyId)
                                {
                                    this.keyId = this.previousKeyId;
                                    this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "Save");
                                    if (this.queryEngineSmartPart.TotalRecords > 0)
                                    {
                                        this.previousKeyId = this.keyId;
                                    }
                                    else
                                    {
                                        this.keyId = -99;
                                    }
                                }

                                this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));

                                if (this.queryEngineSmartPart.TotalRecords <= 0)
                                {
                                    this.NullRecords = true;
                                }

                                //if (this.flagRecordNavigationVisible)
                                //{
                                //    this.RecordNavigationDeckWorkspace.Visible = true;
                                //}
                                //else
                                //{
                                this.RecordNavigationDeckWorkspace.Visible = false;
                                //}

                                this.formMasterDataSet.FormSubTitle2.Clear();
                                this.formMasterDataSet.FormSubTitle1.Clear();
                                this.formMasterDataSet.BackgroundColor.Clear();
                                this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), true);
                                this.LoadSubtitles();

                                this.LoadAuditLinkLabel();
                                this.LoadButtons();
                                this.LoadSlices();
                                if (this.NullRecords)
                                {
                                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                    this.setWhiteColor();
                                }
                                else
                                {
                                    //this.AuditlinkLabel.Enabled = true;
                                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                                    this.LoadBackGroundColor();
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (DialogResult.OK == dialogResult)
                                {
                                    this.flagValidateKeyId = false;
                                    this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "InvalidKey");

                                    this.tempPanel.Visible = true;
                                    this.tempPanel.BringToFront();

                                    this.ActivateQueryEngine();
                                    if (this.queryEngineSmartPart.TotalRecords > 0)
                                    {
                                        this.keyId = this.queryEngineSmartPart.AdvanceRecordPointer(0);
                                        if (this.parameterCount > 0)
                                        {
                                            if (this.parameterList != null)
                                            {
                                                this.parameterList.Clear();
                                                for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                                {
                                                    if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                                    {
                                                        this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                                    }
                                                }
                                            }
                                        }

                                        this.previousKeyId = this.keyId;
                                    }
                                    else
                                    {
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                        this.keyId = -99;
                                    }

                                    if (this.flagRecordNavigationVisible)
                                    {
                                        this.RecordNavigationDeckWorkspace.Visible = true;
                                    }
                                    else
                                    {
                                        this.RecordNavigationDeckWorkspace.Visible = false;
                                    }

                                    this.formMasterDataSet.FormSubTitle2.Clear();
                                    this.formMasterDataSet.FormSubTitle1.Clear();
                                    this.formMasterDataSet.BackgroundColor.Clear();
                                    this.formMasterDataSet.Merge(this.form9030control.WorkItem.GetSandwichSubTitleInformation(this.formNo, this.keyId, TerraScanCommon.UserId), true);
                                    this.LoadSubtitles();
                                    this.LoadBackGroundColor();
                                    this.LoadAuditLinkLabel();
                                    this.LoadButtons();
                                    this.LoadSlices();

                                    //if (this.keyId.Equals(-99))
                                    //{
                                    //    this.loadedWebSlices = 0;
                                    //}
                                }
                                else
                                {
                                    this.queryEngineSmartPart = null;
                                    this.ParentForm.Close();
                                    Application.UseWaitCursor = false;
                                }
                            }
                            #endregion show message and proceed accroding to it
                        }


                    }
                    else
                    {
                        this.previousKeyId = this.keyId;

                        //// Disable recordnaviation while opening the form using single keyid
                        if (!this.flagWithoutKeyId)
                        {
                            this.RecordNavigationDeckWorkspace.Visible = false;
                        }

                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }

                    #endregion raise message box on invalid keyid

                    this.SetAttachmentCommentsCount();
                }
                else
                {
                    if (this.keyId > 0)
                    {
                        //need to be addressed condition
                        this.LoadSubtitles();
                        this.LoadBackGroundColor();
                        this.LoadAuditLinkLabel();
                        this.LoadButtons();
                        this.LoadSlices();
                        this.SetAttachmentCommentsCount();
                    }
                    else
                    {
                        this.LoadSubtitles();
                        this.LoadBackGroundColor();
                        this.LoadAuditLinkLabel();
                        this.LoadButtons();
                        this.LoadSlices();
                    }

                    this.isValidRecord = true;
                }

                // sets the record navigation properties
                this.SetRecordNavigationProperties();

                // Method added to track visited records on Form Load
                this.isValidRecord = !this.flagValidateKeyId;
                this.TrackRecordHistory();

                //if (!this.isValidRecord)
                //{
                //    this.loadedWebSlices = 0;
                //}
            }

            if (this.queryEngineSmartPart != null)
            {
                if (this.queryEngineSmartPart.QueryViewUnavailable)
                {
                    ErrorEngine.ShowForm(10);
                    this.ParentForm.Close();
                }
            }
        }

        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        private void GetSandwichAndItsSliceInformation()
        {
            try
            {
                if (this.formNo == 21010 || this.formNo == 21020)
                {
                    if (this.keyId == -99)
                    {
                        this.keyId = 999;
                    }
                }
            }
            catch (Exception ex)
            { }
            try
            {

                this.formMasterDataSet = this.form9030control.WorkItem.GetSandwichAndItsSliceInformation(this.formNo, this.keyId, this.userId);
            }
            catch (Exception ex)
            {
            }
            this.formSliceCount = this.formMasterDataSet.FormSliceInformationList.Rows.Count;
            if (this.formMasterDataSet.FormSandwichDetails.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsNavVisibleColumn].ToString()))
                {
                    this.flagRecordNavigationVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsNavVisibleColumn]);
                }

                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsQueryVisibleColumn].ToString()))
                {
                    this.flagQueryEngineVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsQueryVisibleColumn]);

                    if (this.flagQueryEngineVisible)
                    {
                        this.QueryButton.Visible = true;
                    }
                    else
                    {
                        this.QueryButton.Visible = false;
                    }
                }
                //For Load Query Engine
                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsQueryLoadColumn].ToString()))
                {
                    flagQueryLoad = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsQueryLoadColumn]);
                }

                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsAnalyticsColumn].ToString()))
                {
                    isAnalyticsVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsAnalyticsColumn]);
                }

                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsUpdateColumn].ToString()))
                {
                    isUpdateVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsUpdateColumn]);
                }

                //For Quick FInd
                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsFindOnColumn].ToString()))
                {
                    this.flagQuickFindVisible = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsFindOnColumn]);
                }

                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.MenuNameColumn].ToString()))
                {
                    this.formHeaderString = this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.MenuNameColumn].ToString();
                }

                if (this.featureClassID <= 0)
                {
                    if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.FeatureClassIDColumn].ToString()))
                    {
                        this.featureClassID = Convert.ToInt32(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.FeatureClassIDColumn]);
                    }
                }

                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.keyfieldColumn].ToString()))
                {
                    this.columnNameValue = this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.keyfieldColumn].ToString();
                }

                // To check for excess parameters availability
                if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.UseExtraParameterColumn].ToString()))
                {
                    Int16.TryParse(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.UseExtraParameterColumn].ToString(), out this.parameterCount);
                }

                //if (!string.IsNullOrEmpty(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn].ToString()))
                //{
                //    this.isJerkFree = Convert.ToBoolean(this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.IsJerkFreeColumn]);
                //}

                //if (!this.isJerkFree)
                //{
                //    tempPanel.Visible = false;
                //    tempPanel.SendToBack();
                //}
            }
        }

        /// <summary>
        /// Called when [key id alert].
        /// </summary>
        private void OnKeyIdAlert()
        {
            this.keyId = -99;
            this.flagWithoutKeyId = true;
            this.AuditlinkLabel.Enabled = true;
            this.GetSandwichAndItsSliceInformation();//suganth
            //tempPanel.Visible = true;
            //tempPanel.BringToFront();
            this.LoadMasterForm();
            if (this.ParentForm != null)
            {
                this.AllignControls();
            }
            //tempPanel.Visible = false;
            //tempPanel.SendToBack();

        }

        /// <summary>
        /// Refreshes the form master.
        /// </summary>
        /// <param name="currentKeyId">The key id.</param>
        private void RefreshFormMaster(int currentKeyId)
        {
            try
            {
                this.KeyId = currentKeyId;
                if (!WSHelper.IsOnLineMode)
                {
                    DataSet terraScanMenuItems = WSHelper.GetMenuItems(TerraScanCommon.UserId, TerraScanCommon.ApplicationId);
                    DataSet formInfoDataSet = new DataSet();
                    if (terraScanMenuItems.Tables != null && terraScanMenuItems.Tables.Count > 0 && terraScanMenuItems.Tables[0] != null)
                    {
                        formInfoDataSet.Tables.Add(terraScanMenuItems.Tables[0].Clone());
                    }

                    for (int i = 0; i < terraScanMenuItems.Tables.Count; i++)
                    {
                        formInfoDataSet.Tables[0].Merge(terraScanMenuItems.Tables[i], true);
                    }

                    TerraScanCommon.TerraScanCachedData = formInfoDataSet;
                }
                //this.previousKeyId = this.keyId;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.GetSandwichAndItsSliceInformation();
                if (this.formMasterDataSet.FormSandwichDetails.Rows.Count > 0)
                {
                        this.LoadQueryEngine();
                        this.LoadTitles();
                        this.LoadButtons();
                        this.LoadSlices();
                        this.SetAttachmentCommentsCount();
                    

                }
                else
                {
                    this.ShowDisabledScrollbar();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Master Related

        #region Operation Related

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        /// 

        private void DeleteButton_Click()
        {
            try
            {
                    string sam = string.Empty;
                    if (this.formNo.Equals(30075))
                    {
                        sam = "Are you sure you want to delete this State Assessed record and all of its parcels?";
                    }

                    else if (this.formNo.Equals(30080))
                    {
                        sam = "Do you want to delete this entire Centrally Assessed record?";
                    }
                    else if (this.formNo.Equals(30085))
                    {
                        sam = "Do you want to delete this entire Centrally Assessed Import record?";
                    }
                    else if (this.formNo.Equals(11071))
                    {
                        sam = "Do you want to delete this Journal Entry Template record?";
                    }
                    #region Added Delete Message Box for Permit Import and Permit Import Template.
                    else if (this.formNo.Equals(23210))
                    {
                        sam = "Do you want to delete this Permit Import record?";
                    }
                    else if (this.formNo.Equals(23200))
                    {
                        sam = "Do you want to delete this Permit Import Template record?";
                    }
                    #endregion

                    #region Added Delete Message Box for MAD Import and MAD Import Template.
                    else if (this.formNo.Equals(23210))
                    {
                        sam = "Do you want to delete this Permit Import record?";
                    }
                    else if (this.formNo.Equals(23300))
                    {
                        sam = "Do you want to delete this MAD Import Template record?";
                    }
                    else if (this.formNo.Equals(23310))
                    {
                        sam = "Do you want to delete this MAD Import record?";
                    }
                    else if (this.formNo.Equals(31090))
                    {
                        sam = "Do you want to delete this Income Source record?";
                    }
                   
                    #endregion

                    #region Added Delete Message Box for TIF District Form.
                    else if (this.formNo.Equals(22081))
                    {
                        sam = "Do you want to delete this TIF District?";
                    }


                    #endregion

                    #region Added Delete Message Box for Snapshot Import and Snapshot Import Template Form.
                    else if (this.formNo.Equals(23510))
                    {
                        sam = "Do you want to delete this Snapshot Import record?";
                    }

                    else if (this.formNo.Equals(23500))
                    {
                        sam = "Do you want to delete this Snapshot Import Template record?";
                    }


                    #endregion

                    else
                    {
                        sam = SharedFunctions.GetResourceString("DeleteRecord");
                    }

                    if (MessageBox.Show(sam, ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.D9030_F9030_DeleteSliceInformation(this, new DataEventArgs<int>(this.keyId));

                        if (this.isJerkFree)
                        {
                            this.isJerkFree = false;
                            this.FormMaster_FormVisibility(false);
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        if (this.queryEngineSmartPart == null)
                        {
                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        }
                        else
                        {
                            if (formNo.Equals(20011) || formNo.Equals(22081) || formNo.Equals(11071))
                            {
                                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                            }
                            else
                            {
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                        }

                        if (this.flagCloseOnDelete)
                        {
                            this.ParentForm.Close();
                        }
                        else
                        {
                            if (this.queryEngineSmartPart != null || !this.queryDependentForm)
                            {
                                string expression = "IsPermissionDelete=True and SubForm<>9032";

                                DataRow[] checkedRow = null;
                                checkedRow = this.formMasterDataSet.FormSliceInformationList.Select(expression);

                                if (this.queryEngineSmartPart != null && checkedRow.Length > 0 && this.hasRecordDeleted)
                                {
                                    this.queryEngineSmartPart.IdKeyIdDeleted = true;
                                    this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, "Delete");
                                }
                                if (this.parameterCount > 0)
                                {
                                    if (this.parameterList != null)
                                    {
                                        this.parameterList.Clear();
                                        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                        {
                                            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                            {
                                                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                            }
                                        }
                                    }
                                }

                                SliceReloadActiveRecord sliceReloadActiveRecord;
                                sliceReloadActiveRecord.MasterFormNo = this.formNo;
                                sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                                this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                                this.SetRecordNavigationProperties();
                                ////Commented by Biju I.G. on 17/Dec/2009 to fix the reload after delete issue
                                ////this.LoadAuditLinkLabel();

                                ////////Code has been adeded to refresh Attachment & Comment form
                                ////this.SetAttachmentCommentsCount();
                                ////till here
                                ////Added by Biju I.G. on 17/Dec/2009 to fix the reload after delete issue
                                this.SetMasterformGeneralProperties();

                                this.pageMode = TerraScanCommon.PageModeTypes.View;
                                if (this.queryEngineSmartPart != null && this.queryEngineSmartPart.TotalRecords == 0)
                                {
                                    if (this.canSetNullRecordMode)
                                    {
                                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                        this.AuditlinkLabel.Text = this.auditLinkLabelText;
                                        this.AuditlinkLabel.Enabled = false;
                                        this.additionalOperationSmartPart.Enabled = false;
                                        this.reportActionSmartPart.Enabled = false;
                                        this.isValidRecord = false;


                                        ////coding added for the co : background color on 9030
                                        this.BackColor = Color.White;
                                        this.tempPanel.BackColor = Color.White;
                                        tempNavigationPanel.BackColor = Color.White;
                                        ////ends here
                                    }

                                }
                                else
                                {
                                    if (!this.queryDependentForm)
                                    {
                                        if (this.canSetNullRecordMode)
                                        {
                                            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                        }
                                        else
                                        {
                                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                                        }
                                    }

                                    this.isValidRecord = !this.flagValidateKeyId;
                                }

                                // Method added to track visited records on after Delete
                                this.TrackRecordHistory();
                            }
                        }

                        //if (!this.queryDependentForm)
                        //{
                        //    if (this.canSetNullRecordMode)
                        //    {
                        //        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        //    }
                        //    else
                        //    {
                        //        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        //        this.AuditlinkLabel.Enabled = true;
                        //    }
                        //}
                        this.hasRecordDeleted = true;
                        this.canSetNullRecordMode = true;
                        this.queryDependentForm = true;
                    }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.isJerkFree)
                {
                    this.FormMaster_FormVisibility(true);
                }
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.isCancelClick = true;
                if (this.isJerkFree)
                {
                    this.isJerkFree = false;
                    this.FormMaster_FormVisibility(false);
                }

                //this.isValidRecord = !this.flagValidateKeyId;
                //DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("(FormFile = 'D90010.F95010' OR " +
                //"FormFile = 'D90010.F95011' OR " +
                //"FormFile = 'D90010.F95012') AND " +
                //"IsPermissionOpen = 'True'");
                this.loadedWebSlices = 0;// webSliceRecords.Length * 2;
                try
                {
                    this.D9030_F9030_CancelSliceInformation(this, new DataEventArgs<int>(this.keyId));
                }
                catch (Exception ex)
                {
                    if (!ex.Source.ToString().Equals("Microsoft.Practices.CompositeUI"))
                    {
                        ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
                    }
                }
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);

              
                this.EditModeEnabled();
            
                this.LoadAuditLinkLabel();
                this.LoadSubtitles();
                this.LoadBackGroundColor();
                if (this.queryEngineSmartPart != null)
                {
                    if (this.queryEngineSmartPart.TotalRecords == 0)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                        this.AuditlinkLabel.Text = this.auditLinkLabelText;
                        this.AuditlinkLabel.Enabled = false;
                    }
                }
                if (!this.quickfindloadflag)
                {
                    this.SetRecordNavigationProperties();
                }
                this.SetAttachmentCommentsCount();

                //this.isValidRecord = !this.flagValidateKeyId;
                //DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("(FormFile = 'D90010.F95010' OR " +
                //                                                                        "FormFile = 'D90010.F95011' OR " +
                //                                                                        "FormFile = 'D90010.F95012') AND " +
                //                                                                        "IsPermissionOpen = 'True'");
                //this.loadedWebSlices = webSliceRecords.Length * 2;

                /*Modified for BugID#973 - By Kuppusamy.B -From here-*/
                if (this.KeyId == -99 || this.flagValidateKeyId && this.pageMode == TerraScanCommon.PageModeTypes.View && this.queryEngineSmartPart == null)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.AuditlinkLabel.Enabled = false;
                    ////coding added for the co : background color on 9030
                    this.BackColor = Color.White;
                    this.tempPanel.BackColor = Color.White;
                    tempNavigationPanel.BackColor = Color.White;
                    ////ends here
                }
                if (!this.queryDependentForm)
                {
                    if (this.canSetNullRecordMode)
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                    else
                    {
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.AuditlinkLabel.Enabled = true;
                    }
                }


                this.canSetNullRecordMode = true;
                this.queryDependentForm = true;
                /*-here-*/
                /*  Code Commented By Shiva To Fix the Issue Id: 1157 The Focus does NOT return to the 1st Field.
                if (this.operationSmartPart.NewButtonVisible)
                {
                    Control[] slectedcontrols = this.operationSmartPart.Controls.Find("NewButton", true);
                    if (slectedcontrols[0] is TerraScan.UI.Controls.TerraScanButton)
                    {
                        //// (slectedcontrols[0] as TerraScan.UI.Controls.TerraScanButton).Focus();
                        this.ParentForm.ActiveControl = slectedcontrols[0];
                        this.ParentForm.ActiveControl.Focus();
                    }
                }
                else
                {
                    this.HelplinkLabel.Focus();
                }
                 */
                this.Refresh();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.isJerkFree)
                {
                    this.FormMaster_FormVisibility(true);
                    this.FormMaster_SetScrollPosition(this, new DataEventArgs<int>(this.formNo));
                }
            }
        }

        /// <summary>
        /// deActivates the payment button in Master form according to the conditions specified.
        /// </summary>
        private void EditModeEnabled()
        {
            try
            {
                var f15110s = this.ParentForm.Controls.Find("F15110", true);
               
                if (f15110s.Length > 0)
                {
                    //this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                    //this.FormSlice_EditEnabled(this, new DataEventArgs<int>(15110));
                    f15110 = (D11001.F15110)f15110s[0];
                    f15110.EnableManagePaymentButton();
                }
                if (f15110 != null)
                {
                   // f15110.EnableManagePaymentButton();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }     

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            try
            {
                this.errorMessage = string.Empty;
                this.D9030_F9030_SaveSliceInformation(this, new DataEventArgs<int>(this.formNo));

                if (string.IsNullOrEmpty(this.errorMessage))
                {
                    if (!this.disableNewMode)
                    {
                        if (this.isJerkFree)
                        {
                            this.isJerkFree = false;
                            this.FormMaster_FormVisibility(false);
                        }

                        TerraScanCommon.PageModeTypes currentMode = this.pageMode;
                        int currentKeyId = this.keyId;
                        this.D9030_F9030_SaveConfirmed(this, new DataEventArgs<int>(this.formNo));
                        if (!WSHelper.IsOnLineMode)
                        {
                            TerraScanCommon.InsertFieldUseDetails(this.keyId, this.columnNameValue, TerraScanCommon.UserId);
                        }
                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.formNo;
                        sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                        this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.flagValidateKeyId = false;

                        ////Coding Added for the issue 5358 by malliga on 19/1/2010
                        this.SetMasterformGeneralProperties();
                        int tempId = 11020;
                        if (tempId == formNo)
                        {
                        }
                        else
                        {
                            // Code added to track visited records on New record creation
                            if (currentKeyId != this.keyId)// (currentMode.Equals(TerraScanCommon.PageModeTypes.New))
                            {
                                this.isValidRecord = !this.flagValidateKeyId;
                                this.TrackRecordHistory();
                            }
                            else
                            {
                                this.isValidRecord = !this.flagValidateKeyId;
                                DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                        "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                        "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                        "IsPermissionOpen = 'True'");
                                this.loadedWebSlices = webSliceRecords.Length * 2;
                            }
                        }

                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        this.EditModeEnabled();
                        this.SetAttachmentCommentsCount();
                        this.flagSaveConfirmed = true;
                        if (!this.quickfindloadflag)
                        {
                            if (this.flagRecordNavigationVisible)
                            {
                                this.RecordNavigationDeckWorkspace.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        this.disableNewMode = false;
                    }
                }
                else
                {
                    this.flagSaveConfirmed = false;
                    if (this.requiredFieldMissing)
                    {
                        //Modifed to display Custtom Warning message for F24555 Form
                        if (this.errorMessage.Equals("Cannot assign more than one owner as Grantee"))
                        {
                            MessageBox.Show(this.errorMessage, "Terrascan T2 – Invalid Grantee selections", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                        }
                        else
                        {
                            MessageBox.Show(this.errorMessage, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    else
                    {
                        MessageBox.Show(this.errorMessage, SharedFunctions.GetResourceString("F9030RequiredFieldMissing"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.requiredFieldMissing = false;
                }

                // checkpage mode for new mode so that audit label and 
                // record navigation are disabled ISSUE #785 FIXED
                if (this.pageMode != TerraScanCommon.PageModeTypes.New && flagSaveConfirmed == true)
                {
                    this.LoadAuditLinkLabel();

                    this.SetRecordNavigationProperties();
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.isJerkFree)
                {
                    this.FormMaster_FormVisibility(true);
                    this.FormMaster_SetScrollPosition(this, new DataEventArgs<int>(this.formNo));
                }
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                if (this.isJerkFree)
                {
                    this.isJerkFree = false;
                    this.FormMaster_FormVisibility(false);
                }
                //if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
                //{
                //    if (this.collapsedWorkSpaces[0].ActiveSmartPart.ToString().Equals("D20003.F25011"))
                //    {
                //        this.pageMode = TerraScanCommon.PageModeTypes.New;
                //        this.LoadSlices();
                //    }
                //}
                if (this.collapsedTabs == null && this.collapsedWorkSpaces == null)
                {
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    this.LoadSlices();
                    this.isClear = false;
                }
                else
                {
                    if (formSliceCount > 0)
                    {
                        for (int i = 0; i < formSliceCount; i++)
                        {
                            string value = this.collapsedWorkSpaces[i].ActiveSmartPart.ProductName.ToString();
                            if (value == "D90010")
                            {
                                if (this.keyId > 0)
                                {
                                   // this.keyId = -99;
                                }
                            }
                        }
                    }
                }
                //added by purushotham to resolved #19975 item loading problem on new after clearing the slice list panel
                if ((this.isClear))
                {
                    if (this.previousKeyId != this.keyId)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.New;
                        LoadSlices();
                        isClear = false;
                    }
                }
                this.D9030_F9030_EnableNewMethod(this, new DataEventArgs<int>(this.formNo));
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.AuditlinkLabel.Text = this.auditLinkLabelText;
                this.AuditlinkLabel.Enabled = false;
                this.statusLabelOneFirstPart.Text = string.Empty;
                this.statusLabelOneSecondPart.Text = string.Empty;
                this.statusLabelTwo.Text = string.Empty;

                ////coding added for the co : background color on 9030
                this.BackColor = Color.White;
                this.tempPanel.BackColor = Color.White;
                tempNavigationPanel.BackColor = Color.White;
                ////ends here

                this.SetRecordCount(this, new DataEventArgs<int>(0));
                this.SetActiveRecord(this, new DataEventArgs<int>(0));
                int[] currentRecordInfo = new int[2];
                currentRecordInfo[0] = 0;
                currentRecordInfo[1] = 0;
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                this.SetAttachmentCommentsCount();
                ////Disable report smartpart on new mode
                this.reportActionSmartPartWorkSpace.Enabled = false;
                ////Added by Biju on 18/Jan/2010 to fix the issue of attachment/comment count when 
                ////cancel button is clicked immediately after the New button is clicked.
                this.additionalOperationSmartPart.KeyId = 0;
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.isJerkFree)
                {
                    this.FormMaster_FormVisibility(true);
                }
            }
        }

        /// <summary>
        /// Saves the master confirm.
        /// </summary>
        /// <returns>flag to specify confirmation of save</returns>
        private bool SaveMasterConfirm()
        {
            this.flagSaveConfirmed = false;
            this.SaveButton_Click();
            return this.flagSaveConfirmed;
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            AlertSliceOnClose currentSliceCloseValid = new AlertSliceOnClose(this.formNo);
            this.OnD9030_F9030_AlertDistinguishedSliceOnClose(new TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose>(currentSliceCloseValid));
            if (currentSliceCloseValid.FlagFormClose == false)
            {
                return false;
            }

            DialogResult dialogResult;
            if (!this.PageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.MenuNameColumn].ToString(), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveMasterConfirm();
                }
                else if (dialogResult == DialogResult.No)
                {
                    this.CancelButton_Click();
                    return true;
                }

                return false;
            }

            return true;
        }

        #endregion Operation Related

        #region Query Engine Related

        /// <summary>
        /// Loads the query engine.
        /// </summary>
        private void LoadQueryEngine()
        {
            #region QueryEngineImplimentation

            if ((this.keyId > 0) || this.flagWithoutKeyId.Equals(false))
            {
                /* This piece of code will execute when keyid passed to
                 * the master form. For eg navigated from form master or 
                 * external component. */

                this.RecordNavigationDeckWorkspace.Visible = false;

                if (this.queryEngineSmartPart != null)
                {
                    if (this.flagRecordNavigationVisible)
                    {
                        this.RecordNavigationDeckWorkspace.Visible = true;
                    }
                    else
                    {
                        this.RecordNavigationDeckWorkspace.Visible = false;
                    }


                    if (!this.isQyeryClosed)
                    {
                        this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, string.Empty);

                        if (this.parameterCount > 0)
                        {
                            if (this.parameterList != null)
                            {
                                this.parameterList.Clear();
                                for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                {
                                    if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                    {
                                        this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                    }
                                }
                            }
                        }

                        this.RecordNavigationDeckWorkspace.Visible = false;

                        if (this.flagFormExists)
                        {
                            //this.QueryEngineWorkSpace.Visible = true;

                            if (this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotId"] != null)
                            {
                                int systemSnapShotId;
                                int.TryParse(this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotId"].ToString(), out systemSnapShotId);
                                this.keyId = this.queryEngineSmartPart.ReLoadSystemSnapShot(systemSnapShotId);

                                if (this.parameterCount > 0)
                                {
                                    if (this.parameterList != null)
                                    {
                                        this.parameterList.Clear();
                                        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                        {
                                            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                            {
                                                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                            }
                                        }
                                    }
                                }
                            }

                            if (this.QueryEngineWorkSpace.Visible)
                            {
                                this.isQyeryClosed = true;

                                this.DisplayQueryEngine();
                                this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009
                                if (this.queryEngineSmartPart.TotalRecords > 0)
                                {
                                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                                }
                                else
                                {
                                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                                }
                                this.isQyeryClosed = false;
                            }
                            //this.QueryEngineWorkSpace.Visible = false;
                        }
                    }



                }
                else //// Else portion has added to load QueryEngine when keyid passed to the master form.
                {

                    this.ActivateQueryEngine();
                    if (this.queryEngineSmartPart != null && this.keyId != -99)
                    {
                        if (this.form9030control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"] != null)
                        {
                            // Gets the systemSnapshot bool from the state variable <9033SystemSnapshotLoaded>
                            bool systemSnapShotLoaded = Convert.ToBoolean(this.form9030control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"].ToString());
                            if (!systemSnapShotLoaded)
                            {
                                this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, string.Empty);

                                if (this.parameterCount > 0)
                                {
                                    if (this.parameterList != null)
                                    {
                                        this.parameterList.Clear();
                                        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                        {
                                            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                            {
                                                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Gets the systemSnapshot Id from the state variable <91000SystemSnapShotId>

                                if (this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotId"] != null)
                                {
                                    int systemSnapShotId;
                                    int.TryParse(this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotId"].ToString(), out systemSnapShotId);
                                    this.keyId = this.queryEngineSmartPart.ReLoadSystemSnapShot(systemSnapShotId);

                                    if (this.parameterCount > 0)
                                    {
                                        if (this.parameterList != null)
                                        {
                                            this.parameterList.Clear();
                                            for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                            {
                                                if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                                {
                                                    this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }

                    //if (this.parameterCount > 0)
                    //{
                    //    if (this.parameterList != null)
                    //    {
                    //        this.parameterList.Clear();
                    //        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                    //        {
                    //            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                    //            {
                    //                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                    //            }
                    //        }
                    //    }
                    //}

                    if (this.flagRecordNavigationVisible)
                    {
                        this.RecordNavigationDeckWorkspace.Visible = true;
                        this.RecordNavigationDeckWorkspace.Visible = false;
                    }
                    else
                    {
                        this.RecordNavigationDeckWorkspace.Visible = false;
                    }
                }
            }
            else
            {
                /* This piece of code will execute when no keyid passed to
                 * the master form. For eg navigated from Menu.  */

                this.ActivateQueryEngine();

                this.keyId = this.queryEngineSmartPart.CurrentRowKeyId; ////todo: check this code


                if (this.parameterCount > 0)
                {
                    if (this.parameterList != null)
                    {
                        this.parameterList.Clear();
                        for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                        {
                            if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                            {
                                this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                            }
                        }
                    }
                }

                if (this.flagRecordNavigationVisible)
                {
                    this.RecordNavigationDeckWorkspace.Visible = true;
                }
                else
                {
                    this.RecordNavigationDeckWorkspace.Visible = false;
                }
            }

            #endregion QueryEngineImplimentation
        }


        /// <summary>
        /// Enables the query engine.
        /// </summary>
        private void EnableQueryEngine(string doEvent)
        {
            try
            {
                if (!this.QueryEngineWorkSpace.Visible)
                {
                    AlertSliceOnClose currentSliceCloseValid = new AlertSliceOnClose(this.formNo);
                    currentSliceCloseValid.FlagForQueryEngine = true;
                    this.OnD9030_F9030_AlertDistinguishedSliceOnClose(new TerraScan.Infrastructure.Interface.EventArgs<AlertSliceOnClose>(currentSliceCloseValid));
                    if (currentSliceCloseValid.FlagFormClose == false)
                    {
                        return;
                    }

                    // Code Added by Vinoth : ReloadQueryEngine whenever QueryButton is Clicked
                    // Vinoth Code : Starts Here
                    if (this.queryEngineSmartPart != null)
                    {
                        if (this.form9030control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"] != null)
                        {
                            queryEngineSmartPart.SystemSnapShotLoaded = Convert.ToBoolean(this.form9030control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"].ToString());
                        }

                        if (!queryEngineSmartPart.SystemSnapShotLoaded)
                        {
                            //if (!this.isQueryLoaded && this.flagQueryLoad)
                            if (flagQueryLoad)
                            {
                                this.keyId = this.queryEngineSmartPart.LoadQueryEngine(this.keyId, doEvent);
                            }

                            if (this.parameterCount > 0)
                            {
                                if (this.parameterList != null)
                                {
                                    this.parameterList.Clear();
                                    for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                    {
                                        if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                        {
                                            this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                        }
                                    }
                                }
                            }

                            this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                            this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                            int[] currentRecordInfo = new int[2];
                            currentRecordInfo[0] = this.queryEngineSmartPart.CurrentRowIndex + 1;
                            currentRecordInfo[1] = this.queryEngineSmartPart.TotalRecords;
                            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                            int[] currentformno = new int[2];
                            currentformno[0] = this.keyId;
                            currentformno[1] = this.formNo;
                            this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno));
                        }
                    }

                    // Vinoth Code : Ends Here
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }


        /// <summary>
        /// Displays the query engine.
        /// </summary>
        private void DisplayQueryEngine()
        {
            try
            {
                if (this.flagQueryEngineVisible)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.New) || (this.pageMode == TerraScanCommon.PageModeTypes.Edit))
                    {
                        DialogResult dialogResult;
                        dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.formMasterDataSet.FormSandwichDetails.Rows[0][this.formMasterDataSet.FormSandwichDetails.MenuNameColumn].ToString(), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ////this.SaveButton_Click();
                            if (this.SaveMasterConfirm())
                            {
                                // ISSUE #1106 FIX : Navigation buttons disabled
                                this.EnableQueryEngine("Save");
                                // ISSUE #1106 FIX : Navigation buttons disabled
                                this.ActivateDisplayQueryEngine();
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            this.CancelButton_Click();
                            // ISSUE #1106 FIX : Navigation buttons disabled
                            //this.EnableQueryEngine(string.Empty);
                            // ISSUE #1106 FIX : Navigation buttons disabled
                            this.ActivateDisplayQueryEngine();
                        }
                    }
                    else if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        // ISSUE #1106 FIX : Navigation button disabled
                        // this.EnableQueryEngine(string.Empty);
                        // ISSUE #1106 FIX : Navigation button disabled
                        this.ActivateDisplayQueryEngine();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Activates the query engine.
        /// </summary>
        private void ActivateQueryEngine()
        {
            if (this.queryEngineSmartPart == null)
            {
                object[] optionalParameters;
                if (this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotLoaded"] != null)
                {
                    optionalParameters = new object[6];
                    optionalParameters[5] = (bool)this.form9030control.WorkItem.RootWorkItem.State[this.formNo.ToString().Trim() + "SystemSnapShotLoaded"];
                    this.form9030control.WorkItem.RootWorkItem.State.Remove(this.formNo.ToString().Trim() + "SystemSnapShotLoaded");
                }
                else
                {
                    if (this.parameterCount > 0)
                    {
                        optionalParameters = new object[6];
                        optionalParameters[5] = this.parameterCount;
                    }
                    else
                    {
                        optionalParameters = new object[5];
                    }

                }
                optionalParameters[0] = this.columnNameValue;
                optionalParameters[1] = this.formHeaderString;
                optionalParameters[2] = this.keyId;
                optionalParameters[3] = this.formNo;
                optionalParameters[4] = this.PermissionFiled;
                this.queryEngineSmartPart = (F9033)TerraScanCommon.GetSmartPart(9033, optionalParameters, this.form9030control.WorkItem);
                this.QueryEngineWorkSpace.Location = new Point(this.sliceListPanel.Location.X, (this.Location.Y + this.Height) - (this.queryEngineSmartPart.Height + 35));

                this.queryEngineSmartPart.FlagSliceForm = true;
                this.QueryEngineWorkSpace.Height = this.queryEngineSmartPart.Height;
                this.QueryEngineWorkSpace.Width = this.queryEngineSmartPart.Width;
                this.QueryEngineWorkSpace.Show(this.queryEngineSmartPart);
                this.QueryEngineWorkSpace.SendToBack();
                int tempHeight = this.QueryEngineWorkSpace.Height;
                this.QueryEngineWorkSpace.Height = 0;

                if (this.flagWithoutKeyId.Equals(false))
                {
                    int[] currentforminfo = new int[2];
                    currentforminfo[0] = this.keyId;
                    currentforminfo[1] = this.formNo;
                    try
                    {
                        this.OptionalParameter_GetKeyId(this, new TerraScan.Infrastructure.Interface.EventArgs<int[]>(currentforminfo));
                    }
                    catch (Exception ex)
                    {
                    }
                }

                this.QueryEngineWorkSpace.Visible = true;
                this.QueryEngineWorkSpace.Visible = false;
                this.QueryEngineWorkSpace.Height = tempHeight;
                this.QueryEngineWorkSpace.BringToFront();
            }
        }

        /// <summary>
        /// Loads the quick find.
        /// </summary>
        private void LoadQuickFind()
        {
            if(!this.QueryEngineWorkSpace.Visible)  
            {
            ////Checking the Page mode
                if (this.CheckPageStatus())
                {
                    if (this.tempPanel.Visible)
                    {
                        this.tempPanel.Visible = false;
                        this.tempPanel.SendToBack();
                    }
                    this.quickfindloadflag = true;
                    object[] optionalParameter = new object[6];
                    optionalParameter[0] = this.formNo;
                    if (this.findSmartPart != null)
                    {
                        /*20120510         Manoj P             Changes in the form Quick Find Operation*/
                        //this.lastcontent = TerraScanCommon.GetValue(this.findSmartPart, "CommandResult");
                        bool value = false;
                        string identityValue = TerraScanCommon.GetValue(this.findSmartPart, "CommandIdentity");
                        if (identityValue.Equals(this.keyId.ToString()))
                        {
                            value = true;
                        }
                        DataTable dataTable = (DataTable)TerraScanCommon.GetObject(this.findSmartPart, "CommentDataTable");
                        //optionalParameter[1] = this.lastcontent;
                        //optionalParameter[2] = value;
                        optionalParameter[1] = identityValue;
                        string findvalue = TerraScanCommon.GetValue(this.findSmartPart, "CommandString");
                        optionalParameter[2] = findvalue;
                        optionalParameter[3] = dataTable;
                        optionalParameter[4] = value;
                        optionalParameter[5] = this.keyId;
                    }
                    else
                    {
                        this.lastcontent = "No Content";
                    }
                    //this.RecordNavigationDeckWorkspace.Visible = false;

                    if (this.collapsedWorkSpaces != null)
                    {
                        this.collapsedWorkSpaces[0].Focus();
                    }
                    //this.RecordNavigationDeckWorkspace.Visible = false;
                    this.findSmartPart = (F9610)TerraScanCommon.GetForm(9610, optionalParameter, this.form9030control.WorkItem);
                    this.LoadAuditLinkLabel();
                    if (this.findSmartPart != null)
                    {
                        this.findSmartPart.ShowDialog();
                    }
                    //this.LoadAuditLinkLabel();
                    //if (this.SetActiveRecord != null)
                    //{
                    //    this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                    //}
                    //else
                    //{
                    //    this.findSmartPart.Close();  
                    //}
                    //if (this.SetRecordCount != null)
                    //{
                    //    this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                    //}
                    //if (this.SetActiveRecordButtons != null)
                    //{
                    //    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { this.queryEngineSmartPart.CurrentRowIndex + 1, this.queryEngineSmartPart.TotalRecords }));
                    //}
                    //// this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
                    //this.RecordNavigationDeckWorkspace.Visible = false;
                    //this.quickfindloadflag = false;
                }
            }

        }

        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Panel scroll for PageUp and PageDown key press
            if (keyData.Equals(Keys.PageUp) || keyData.Equals(Keys.PageDown))
            {
                int changeAmount;
                changeAmount = this.sliceListPanel.VerticalScroll.LargeChange;
                int currentPosition = this.sliceListPanel.VerticalScroll.Value;

                if (keyData.Equals(Keys.PageUp))
                {
                    // For PageUp key press
                    if ((currentPosition - changeAmount) > this.sliceListPanel.VerticalScroll.Minimum)
                    {
                        // Reduce the scroll value
                        this.sliceListPanel.VerticalScroll.Value -= changeAmount;
                    }
                    else
                    {
                        this.sliceListPanel.VerticalScroll.Value = this.sliceListPanel.VerticalScroll.Minimum;
                    }
                }
                else
                {
                    // For PageDown key press
                    if ((currentPosition + changeAmount) < this.sliceListPanel.VerticalScroll.Maximum)
                    {
                        // Increase the scroll value
                        this.sliceListPanel.VerticalScroll.Value += changeAmount;
                    }
                    else
                    {
                        this.sliceListPanel.VerticalScroll.Value = this.sliceListPanel.VerticalScroll.Maximum;
                    }
                }

                this.sliceListPanel.PerformLayout();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Activates the display query engine.
        /// </summary>
        private void ActivateDisplayQueryEngine()
        {
            #region Activate and display Query Engine
            this.ActivateQueryEngine();
            if (this.queryEngineSmartPart.ColumnNameInvalid)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("F9030WithoutQueryView"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                #region QueryEngine Visible
                if (this.QueryEngineWorkSpace.Visible)
                {
                    this.isQyeryClosed = true;
                    int[] currentRecordInfo = new int[2];
                    this.QueryEngineWorkSpace.Visible = false;
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.queryEngineSmartPart.CurrentRowIndex + 1));
                    if (!this.queryEngineSmartPart.TotalRecords.Equals(0))
                    {
                        currentRecordInfo[1] = this.queryEngineSmartPart.TotalRecords;
                        this.SetRecordCount(this, new DataEventArgs<int>(this.queryEngineSmartPart.TotalRecords));
                    }
                    else
                    {
                        currentRecordInfo[1] = 0;
                        this.SetRecordCount(this, new DataEventArgs<int>(0));
                        this.SetActiveRecord(this, new DataEventArgs<int>(0));
                    }

                    if (!this.queryEngineSmartPart.CurrentRowIndex.Equals(-1))
                    {
                        currentRecordInfo[0] = this.queryEngineSmartPart.CurrentRowIndex + 1;
                    }
                    else
                    {
                        if (this.queryEngineSmartPart.TotalRecords > 0)
                        {
                            this.SetActiveRecord(this, new DataEventArgs<int>(1));
                            currentRecordInfo[0] = 1;
                        }
                        else
                        {
                            currentRecordInfo[0] = 0;
                        }
                    }

                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(currentRecordInfo));
                    //if (this.keyId != this.queryEngineSmartPart.CurrentRowKeyId)
                    //{ 
                    this.keyId = this.queryEngineSmartPart.CurrentRowKeyId;

                    if (this.parameterCount > 0)
                    {
                        if (this.parameterList != null)
                        {
                            this.parameterList.Clear();
                            for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                            {
                                if ((this.queryEngineSmartPart.ParameterList != null) && (this.queryEngineSmartPart.ParameterList.Count > listPointer) && this.queryEngineSmartPart.ParameterList[listPointer] != null)
                                {
                                    this.parameterList.Add(this.queryEngineSmartPart.ParameterList[listPointer]); //suganth
                                }
                            }
                        }
                    }

                    //// If condition has added to restrict the form reload for same keyid 
                    if (this.previousKeyId != this.keyId)//// && this.keyId != -99) //Code commented to test - to disable form for -99 keyid on QE close
                    {
                        // this.previousKeyId = this.keyId;
                        this.RefreshFormMaster(this.keyId);
                    }
                    //}

                    this.AuditlinkLabel.Text = this.auditLinkLabelText + " " + this.keyId;
                    if (this.flagRecordNavigationVisible)
                    {
                        this.RecordNavigationDeckWorkspace.Visible = true;
                        this.RecordNavigationDeckWorkspace.Enabled = true;
                    }
                    this.additionalOperationSmartPartWorkspace.Enabled = true;
                    this.operationSmartPartWorkSpace.Enabled = true;
                    this.AuditlinkLabel.Enabled = true;
                    this.sliceListPanel.Enabled = true;
                    this.reportActionSmartPartWorkSpace.Enabled = true;
                    ////commented by Biju on 30/Dec/2009 to avoid multiple calls of f9030_pcget_FormMaster
                    ////this.SetMasterformGeneralProperties();////method name spelling mistake corrected by Biju on 30/12/2009
                    ////added by Biju on 30/Dec/2009 to avoid multiple calls of f9030_pcget_FormMaster
                    this.SetMasterformGeneralPropertiesWithNoDBCall();
                    this.isQyeryClosed = false;
                    ////this.OnD9030_F9030_QueryEngineCloseAtFormMaster(new TerraScan.Infrastructure.Interface.EventArgs<int>(this.formNo));
                }
                else
                {
                    this.QueryEngineWorkSpace.BringToFront();
                    this.RecordNavigationDeckWorkspace.Enabled = false;
                    this.additionalOperationSmartPartWorkspace.Enabled = false;
                    this.operationSmartPartWorkSpace.Enabled = false;
                    this.AuditlinkLabel.Enabled = false;
                    this.sliceListPanel.Enabled = false;
                    this.reportActionSmartPartWorkSpace.Enabled = false;

                    if (this.keyId != this.queryEngineSmartPart.CurrentRowKeyId)
                    {
                        this.queryEngineSmartPart.CurrentRowKeyId = this.keyId;

                        if (this.parameterCount > 0)
                        {
                            if (this.parameterList != null)
                            {
                                if (this.queryEngineSmartPart.ParameterList == null)
                                {
                                    this.queryEngineSmartPart.ParameterList = new List<string>();
                                }

                                this.queryEngineSmartPart.ParameterList.Clear();
                                for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                {
                                    if (this.parameterList.Count > listPointer && this.parameterList[listPointer] != null)
                                    {
                                        this.queryEngineSmartPart.ParameterList.Add(this.parameterList[listPointer]); //suganth
                                    }
                                }
                            }
                        }
                    }

                    this.QueryEngineWorkSpace.Visible = true;
                    this.QueryEngineWorkSpace.BringToFront();
                    this.RecordNavigationDeckWorkspace.Visible = false;
                    if (this.flagWithoutKeyId)
                    {
                        if (this.flagRecordNavigationVisible)
                        {
                            this.RecordNavigationDeckWorkspace.Visible = true;
                        }
                    }
                }
                #endregion QueryEngine Visible
            }
            #endregion Activate and display Query Engine
        }

        #endregion Query Engine Related

        #region Display slice Related

        /// <summary>
        /// Sets the initial position.
        /// </summary>
        private void SetInitialPosition()
        {
            this.xcoordinatePosition = 5;
            this.ycoorPosition = 5;

        }

        /// <summary>
        /// Locates the slice.
        /// </summary>
        /// <param name="currentSlice">The current slice.</param>
        private void LocateSlice(int currentSlice)
        {
            this.collapsedTabs[currentSlice].Location = new Point(this.xcoordinatePosition, this.ycoorPosition);
            this.collapsedWorkSpaces[currentSlice].Location = new Point(this.xcoordinatePosition, this.ycoorPosition);
        }

        /// <summary>
        /// Collapses the slices.
        /// </summary>
        /// <param name="sliceFormNo">The slice form no.</param>
        private void CollapseSlices(string sliceFormNo)
        {
            ////tempPanel.Visible = true;
            ////tempPanel.BringToFront();

            this.SetInitialPosition();
            if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
            {
                for (int currentSlice = 0; currentSlice < this.formSliceCount; currentSlice++)
                {
                    if (this.collapsedTabs[currentSlice].Visible || this.collapsedWorkSpaces[currentSlice].Visible)
                    {
                        this.LocateSlice(currentSlice);
                        if (sliceFormNo != this.collapsedTabs[currentSlice].Tag.ToString())
                        {
                            if (this.collapsedWorkSpaces[currentSlice].Visible == true)
                            {
                                this.ycoorPosition = this.ycoorPosition + this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                            }
                            else
                            {
                                this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                            }
                        }
                        else
                        {
                            this.collapsedWorkSpaces[currentSlice].Visible = false;
                            this.collapsedTabs[currentSlice].Visible = true;
                            this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                        }
                    }
                }
            }

            this.ShowDisabledScrollbar();

            ////tempPanel.Visible = false;
            ////tempPanel.SendToBack();
        }

        /// <summary>
        /// Expands the slices.
        /// </summary>
        /// <param name="sliceFormNo">The slice form no.</param>
        private void ExpandSlices(string sliceFormNo)
        {
            ////tempPanel.Visible = true;
            ////tempPanel.BringToFront();

            this.SetInitialPosition();
            for (int currentSlice = 0; currentSlice < this.formSliceCount; currentSlice++)
            {
                if (this.collapsedTabs[currentSlice].Visible || this.collapsedWorkSpaces[currentSlice].Visible)
                {
                    this.LocateSlice(currentSlice);
                    if (sliceFormNo != this.collapsedTabs[currentSlice].Tag.ToString())
                    {
                        if (this.collapsedWorkSpaces[currentSlice].Visible == true)
                        {
                            this.ycoorPosition += this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                        }
                        else
                        {
                            this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                        }
                    }
                    else
                    {
                        this.collapsedWorkSpaces[currentSlice].Visible = true;
                        this.collapsedTabs[currentSlice].Visible = false;
                        this.ycoorPosition = this.ycoorPosition + this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                    }
                }
            }

            this.ShowDisabledScrollbar();

            ////tempPanel.Visible = false;
            ////tempPanel.SendToBack();
        }

        /// <summary>
        /// Expands the slices.
        /// </summary>
        /// <param name="sliceFormNo">The slice form no.</param>
        /// <param name="sliceHeight">Height of the slice.</param>
        private void ExpandSlices(string sliceFormNo, int sliceHeight)
        {
            ////tempPanel.Visible = true;
            ////tempPanel.BringToFront();

            this.SetInitialPosition();
            for (int currentSlice = 0; currentSlice < this.formSliceCount; currentSlice++)
            {
                if (this.collapsedTabs[currentSlice] != null)
                {
                    if (this.collapsedTabs[currentSlice].Visible || this.collapsedWorkSpaces[currentSlice].Visible)
                    {
                        this.LocateSlice(currentSlice);
                        if (sliceFormNo == this.collapsedTabs[currentSlice].Tag.ToString())
                        {
                            if (this.collapsedWorkSpaces[currentSlice].Visible == true)
                            {
                                this.collapsedWorkSpaces[currentSlice].Height = sliceHeight;
                                this.collapsedWorkSpaces[currentSlice].SuspendLayout();
                                this.collapsedWorkSpaces[currentSlice].Show();
                                this.ycoorPosition += this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                                //this.collapsedWorkSpaces[currentSlice].Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
                            }
                            else
                            {
                                this.collapsedWorkSpaces[currentSlice].Height = sliceHeight;
                                this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                            }
                        }
                        else
                        {
                            if (this.collapsedWorkSpaces[currentSlice].Visible == true)
                            {
                                this.collapsedWorkSpaces[currentSlice].Visible = true;
                                this.collapsedTabs[currentSlice].Visible = false;
                                this.ycoorPosition = this.ycoorPosition + this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                                // this.collapsedWorkSpaces[currentSlice].Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
                            }
                            else
                            {
                                this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                            }
                        }
                    }
                }
            }

            #region BugID:2618 9030 Form Master pushing to bottom for any record

            ////Set the focus in First Form Slice
            ////Reduce the space between two slices in DeedShifter 
            if (this.collapsedWorkSpaces[0] != null)
            {
                this.collapsedWorkSpaces[0].Focus();
            }

            #endregion BugID:2618 9030 Form Master pushing to bottom for any record

            this.ShowDisabledScrollbar();

            ////tempPanel.Visible = false;
            ////tempPanel.SendToBack();
        }

        /// <summary>
        /// Refreshes the loaded slice position.
        /// </summary>
        private void RefreshLoadedSlicePosition()
        {
            if (!this.flagFormResizing)
            {
                this.sliceListPanel.AutoScroll = false;
                this.SetInitialPosition();
                if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
                {
                    for (int currentSlice = 0; currentSlice < this.formSliceCount; currentSlice++)
                    {
                        if (this.collapsedTabs[currentSlice].Visible || this.collapsedWorkSpaces[currentSlice].Visible)
                        {
                            this.LocateSlice(currentSlice);
                            if (this.collapsedWorkSpaces[currentSlice].Visible == true)
                            {
                                foreach (Control currentSmartPart in this.collapsedWorkSpaces[currentSlice].SmartParts)
                                {
                                    if (currentSmartPart.GetType().FullName == this.formMasterDataSet.FormSliceInformationList.Rows[currentSlice][this.formMasterDataSet.FormSliceInformationList.FormFileColumn].ToString())
                                    {
                                        if (this.collapsedWorkSpaces[currentSlice].Height != currentSmartPart.PreferredSize.Height)
                                        {
                                            int difference = currentSmartPart.PreferredSize.Height - this.collapsedWorkSpaces[currentSlice].Height;
                                            if (difference < 0)
                                            {
                                                difference = difference * -1;
                                            }

                                            if (difference > 8)
                                            {
                                                //Slice Spacing issue fix.
                                                if (difference != 70 && difference != 48 && difference != 26)
                                                {
                                                    this.collapsedWorkSpaces[currentSlice].Height = currentSmartPart.PreferredSize.Height;
                                                    this.collapsedWorkSpaces[currentSlice].SuspendLayout();
                                                    currentSmartPart.Height = this.collapsedWorkSpaces[currentSlice].Height;
                                                    this.collapsedWorkSpaces[currentSlice].Show(currentSmartPart);
                                                }
                                            }
                                        }
                                    }
                                }

                                this.ycoorPosition += this.collapsedWorkSpaces[currentSlice].Height + this.gapBetweenSlices;
                            }
                            else
                            {
                                this.ycoorPosition = this.ycoorPosition + this.collapsedTabs[currentSlice].Height + this.gapBetweenSlices;
                            }
                        }
                        #region BugID:2618 9030 Form Master pushing to bottom for any record

                        if (this.collapsedWorkSpaces[0] != null)
                        {
                            ////Set the focus in First Form Slice
                            this.collapsedWorkSpaces[0].Focus();
                        }

                        #endregion BugID:2618 9030 Form Master pushing to bottom for any record
                    }
                }

                this.ShowDisabledScrollbar();
                this.sliceListPanel.AutoScroll = true;
            }
        }

        /// <summary>
        /// Loads the slices.
        /// </summary>
        private void LoadSlices()
        {
            bool flagSliceUnavailable = false;
            bool flagKeyValidinAllSlice = false;
            string formFile = string.Empty;
            // this.Controls.Add(tempPanel);
            //tempPanel.Visible = true;
            //tempPanel.BringToFront();

            if (this.formMasterDataSet.FormSliceInformationList.Rows.Count > 0 && (this.keyId > 0 || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New)))
            {
                this.collapsedTabs = new PictureBox[this.formSliceCount];
                this.collapsedWorkSpaces = new DeckWorkspace[this.formSliceCount];
                this.SetInitialPosition();
                this.sliceListPanel.AutoScroll = false;
                this.sliceListPanel.Controls.Clear();
                for (int collapsedTab = 0; collapsedTab < this.formMasterDataSet.FormSliceInformationList.Rows.Count; collapsedTab++)
                //for (int collapsedTab = this.formMasterDataSet.FormSliceInformationList.Rows.Count - 1; collapsedTab >= 0; collapsedTab--)
                {
                    this.collapsedTabs[collapsedTab] = new PictureBox();
                    this.collapsedWorkSpaces[collapsedTab] = new DeckWorkspace();
                    this.collapsedWorkSpaces[collapsedTab].SmartPartClosing += new EventHandler<WorkspaceCancelEventArgs>(this.F9030_SmartPartClosing);
                    this.sliceListPanel.Controls.Add(this.collapsedTabs[collapsedTab]);
                    this.sliceListPanel.Controls.Add(this.collapsedWorkSpaces[collapsedTab]);
                    this.collapsedTabs[collapsedTab].Location = new Point(this.xcoordinatePosition, this.ycoorPosition);
                    this.collapsedWorkSpaces[collapsedTab].Location = new Point(this.xcoordinatePosition, this.ycoorPosition);
                    this.collapsedWorkSpaces[collapsedTab].Padding = new Padding(0, 0, 0, 0);
                    this.collapsedWorkSpaces[collapsedTab].Margin = new Padding(0, 0, 0, 0);
                    //this.collapsedWorkSpaces[collapsedTab].Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
                    this.collapsedTabs[collapsedTab].Height = 35;
                    this.collapsedTabs[collapsedTab].Width = 801;
                    this.collapsedTabs[collapsedTab].Visible = true;
                    //this.collapsedTabs[collapsedTab].Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));
                    this.collapsedTabs[collapsedTab].Tag = this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn];
                    this.collapsedTabs[collapsedTab].MouseEnter += new EventHandler(this.F9030_MouseEnter);
                    this.collapsedTabs[collapsedTab].Image = ExtendedGraphics.GenerateHorizontalImage(801, 35, this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.MenuNameColumn].ToString(), Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.RedColumn]), Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.GreenColumn]), Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.BlueColumn]));
                    this.collapsedWorkSpaces[collapsedTab].Visible = false;

                    if (!this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn].ToString().Contains(SharedFunctions.GetResourceString("F9030SliceTestFormNo")))
                    {
                        if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionMenuColumn]))
                        {
                            if (this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn].ToString().Contains(SharedFunctions.GetResourceString("F9030SpacerFormNo")))
                            {
                                try
                                {
                                    F9032 spacer;
                                    if (this.form9030control.WorkItem.Items.Contains(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString() + collapsedTab.ToString()))
                                    {
                                        spacer = (F9032)this.form9030control.WorkItem.Items.Get(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString() + collapsedTab.ToString());
                                    }
                                    else
                                    {
                                        spacer = new F9032();
                                        this.form9030control.WorkItem.Items.Add(spacer, this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString() + collapsedTab.ToString());
                                    }

                                    this.collapsedWorkSpaces[collapsedTab].Height = spacer.Height;
                                    this.collapsedWorkSpaces[collapsedTab].Width = spacer.Width;
                                    this.collapsedWorkSpaces[collapsedTab].Show(spacer);
                                    this.collapsedWorkSpaces[collapsedTab].Visible = true;
                                    this.collapsedTabs[collapsedTab].Visible = false;
                                    this.ycoorPosition += this.collapsedWorkSpaces[collapsedTab].Height + this.gapBetweenSlices;
                                }
                                catch
                                {
                                    this.collapsedTabs[collapsedTab].Image = ExtendedGraphics.GenerateHorizontalImage(801, 35, SharedFunctions.GetResourceString("F9030FormUnderConstruction"), 166, 166, 166);
                                    this.collapsedWorkSpaces[collapsedTab].Height = this.collapsedTabs[collapsedTab].Height;
                                    this.collapsedWorkSpaces[collapsedTab].Width = this.collapsedTabs[collapsedTab].Width;
                                    this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                    this.collapsedTabs[collapsedTab].Visible = true;
                                    this.ycoorPosition += this.collapsedTabs[collapsedTab].Height + this.gapBetweenSlices;
                                }
                            }
                            else
                            {
                                #region before spacer implimented

                                try
                                {
                                    Object[] sliceOptionalParameter;
                                    if (this.featureClassID != -99)
                                    {
                                        if (this.parameterCount > 0)
                                        {
                                            sliceOptionalParameter = new object[9 + this.parameterCount];
                                            sliceOptionalParameter[8] = this.featureClassID;
                                            for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                            {
                                                sliceOptionalParameter[9 + listPointer] = this.parameterList[listPointer];
                                            }
                                        }
                                        else
                                        {
                                            sliceOptionalParameter = new object[9];
                                            sliceOptionalParameter[8] = this.featureClassID;
                                        }
                                    }
                                    else
                                    {
                                        if (this.parameterCount > 0)
                                        {
                                            sliceOptionalParameter = new object[8 + this.parameterCount];
                                            for (int listPointer = 0; listPointer < this.parameterCount; listPointer++)
                                            {
                                                sliceOptionalParameter[8 + listPointer] = this.parameterList[listPointer];
                                            }
                                        }
                                        else
                                        {
                                            sliceOptionalParameter = new object[8];
                                        }
                                    }

                                    sliceOptionalParameter[0] = this.formNo;
                                    sliceOptionalParameter[1] = Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn]);
                                    sliceOptionalParameter[2] = this.keyId;
                                    //if ((this.formNo == 14005) && (Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn]) == 19005) && ((this.keyId ==-99) ||(this.keyId==999)))
                                    //{
                                    //    sliceOptionalParameter[2] = -1;//this.keyId;
                                    //}
                                    //else
                                    //{
                                    //    sliceOptionalParameter[2] = this.keyId;
                                    //}
                                    sliceOptionalParameter[3] = Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.RedColumn]);
                                    sliceOptionalParameter[4] = Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.GreenColumn]);
                                    sliceOptionalParameter[5] = Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.BlueColumn]);
                                    sliceOptionalParameter[6] = Convert.ToString(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.MenuNameColumn]);
                                    sliceOptionalParameter[7] = this.PermissionEdit;
                                    UserControl mycontrol = TerraScanCommon.GetSmartPart(Convert.ToInt32(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn]), sliceOptionalParameter, this.form9030control.WorkItem);
                                    this.collapsedWorkSpaces[collapsedTab].Height = mycontrol.Height;
                                    this.collapsedWorkSpaces[collapsedTab].Width = mycontrol.Width;
                                    //this.collapsedWorkSpaces[collapsedTab].Width = this.sliceListPanel.Width;
                                    this.collapsedWorkSpaces[collapsedTab].Visible = true;
                                    bool hiddenworkSpace = this.collapsedWorkSpaces[collapsedTab].Visible;
                                    if (!hiddenworkSpace)
                                    {
                                        this.collapsedWorkSpaces[collapsedTab].Visible = true;
                                    }

                                    if (this.keyId > 0 || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                                    {
                                        this.collapsedWorkSpaces[collapsedTab].Show(mycontrol);
                                    }

                                    // To set the focus in first slice after the form load (Bug Id : 4227)
                                    if (this.collapsedTabs.Length > 0 && this.collapsedWorkSpaces[0] != null)
                                    {
                                        this.collapsedWorkSpaces[0].Focus();
                                    }
                                    // Ends here 4227
                                    if (!hiddenworkSpace)
                                    {
                                        this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                    }

                                    ////SlicePermissionReload slicePermissionReload = new SlicePermissionReload();
                                    ////slicePermissionReload.MasterFormNo = this.formNo;
                                    ////slicePermissionReload.SelectUserId = this.userId;
                                    ////slicePermissionReload.KeyId = this.keyId;
                                    ////slicePermissionReload.DeletePermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionDeleteColumn]);
                                    ////slicePermissionReload.NewPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionAddColumn]);
                                    ////slicePermissionReload.OpenPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionOpenColumn]);
                                    ////slicePermissionReload.EditPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionEditColumn]);

                                    //// this.D9030_F9030_SetSlicePermission(this, new DataEventArgs<SlicePermissionReload>(slicePermissionReload));

                                    //// code to test wether the keyid is valid at slice level

                                    ////if (!flagKeyValidinAllSlice)
                                    ////{
                                    ////    flagKeyValidinAllSlice = !slicePermissionReload.FlagInvalidSliceKey;
                                    ////}

                                    if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionOpenColumn]))
                                    {
                                        this.collapsedTabs[collapsedTab].Click += new EventHandler(this.F9030_Click);
                                    }

                                    this.ycoorPosition += this.collapsedTabs[collapsedTab].Height + this.gapBetweenSlices;
                                }
                                catch
                                {
                                    flagSliceUnavailable = true;
                                    this.collapsedTabs[collapsedTab].Image = ExtendedGraphics.GenerateHorizontalImage(801, 35, SharedFunctions.GetResourceString("F9030FormUnderConstruction"), 166, 166, 166);
                                    this.collapsedWorkSpaces[collapsedTab].Height = this.collapsedTabs[collapsedTab].Height;
                                    this.collapsedWorkSpaces[collapsedTab].Width = this.collapsedTabs[collapsedTab].Width;
                                    this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                    this.collapsedTabs[collapsedTab].Visible = true;
                                    this.ycoorPosition += this.collapsedTabs[collapsedTab].Height + this.gapBetweenSlices;
                                }

                                #endregion before spacer implimented
                            }
                        }
                        else
                        {
                            this.collapsedTabs[collapsedTab].Visible = false;
                        }

                        #region foruncreated and test slice

                        if (!flagSliceUnavailable)
                        {
                            if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionMenuColumn]))
                            {
                                if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsExpandedColumn]))
                                {
                                    if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionOpenColumn]))
                                    {
                                        this.collapsedTabs[collapsedTab].Visible = false;
                                        this.collapsedWorkSpaces[collapsedTab].Visible = true;
                                        this.ycoorPosition += this.collapsedWorkSpaces[collapsedTab].Height - this.collapsedTabs[collapsedTab].Height - this.gapBetweenSlices;
                                    }
                                    else
                                    {
                                        this.collapsedTabs[collapsedTab].Visible = true;
                                        this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                    }
                                }
                                else
                                {
                                    this.collapsedTabs[collapsedTab].Visible = true;
                                    this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionMenuColumn]))
                        {
                            try
                            {
                                F9031 testSlice;
                                if (this.form9030control.WorkItem.Items.Contains(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString()))
                                {
                                    testSlice = (F9031)this.form9030control.WorkItem.Items.Get(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString());
                                }
                                else
                                {
                                    testSlice = new F9031((FormMasterData.FormSliceInformationListRow)this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab], this.keyId);
                                    this.form9030control.WorkItem.Items.Add(testSlice, this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.SubFormColumn].ToString());
                                }

                                this.collapsedWorkSpaces[collapsedTab].Height = testSlice.Height;
                                this.collapsedWorkSpaces[collapsedTab].Width = testSlice.Width;
                                this.collapsedWorkSpaces[collapsedTab].Show(testSlice);
                                this.collapsedWorkSpaces[collapsedTab].Visible = true;
                                this.collapsedTabs[collapsedTab].Visible = false;
                                this.ycoorPosition += this.collapsedWorkSpaces[collapsedTab].Height + this.gapBetweenSlices;
                            }
                            catch
                            {
                                this.collapsedTabs[collapsedTab].Image = ExtendedGraphics.GenerateHorizontalImage(801, 35, SharedFunctions.GetResourceString("F9030FormUnderConstruction"), 166, 166, 166);
                                this.collapsedWorkSpaces[collapsedTab].Height = this.collapsedTabs[collapsedTab].Height;
                                this.collapsedWorkSpaces[collapsedTab].Width = this.collapsedTabs[collapsedTab].Width;
                                this.collapsedWorkSpaces[collapsedTab].Visible = false;
                                this.collapsedTabs[collapsedTab].Visible = true;
                                this.ycoorPosition += this.collapsedTabs[collapsedTab].Height + this.gapBetweenSlices;
                            }
                        }
                        else
                        {
                            this.collapsedWorkSpaces[collapsedTab].Visible = false;
                            this.collapsedTabs[collapsedTab].Visible = false;
                        }
                    }

                        #endregion foruncreated and test slice

                    this.sliceListPanel.AutoScroll = true;
                    flagSliceUnavailable = false;
                    if (Convert.ToString(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn]).Equals("D20050.F35051"))
                        formFile = Convert.ToString(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn]);
                    else if (Convert.ToString(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn]).Equals("D35000.F35000"))
                        formFile = Convert.ToString(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn]);
                }

                // Added the code for the issue 5194 on 12/5/2009 by Malliga
                if (this.keyId > 0 || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                {
                    for (int collapsedTab = 0; collapsedTab < this.formMasterDataSet.FormSliceInformationList.Rows.Count; collapsedTab++)
                    {
                        if (!this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.FormFileColumn].ToString().Contains(SharedFunctions.GetResourceString("F9030SliceTestFormNo")))
                        {
                            if (Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionMenuColumn]))
                            {
                                SlicePermissionReload slicePermissionReload = new SlicePermissionReload();
                                slicePermissionReload.MasterFormNo = this.formNo;
                                slicePermissionReload.SelectUserId = this.userId;
                                slicePermissionReload.KeyId = this.keyId;
                                // Codind added for the issue 4212
                                slicePermissionReload.FlagInvalidSliceKey = true;
                                // COding ends here
                                slicePermissionReload.DeletePermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionDeleteColumn]);
                                slicePermissionReload.NewPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionAddColumn]);
                                slicePermissionReload.OpenPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionOpenColumn]);
                                slicePermissionReload.EditPermission = Convert.ToBoolean(this.formMasterDataSet.FormSliceInformationList.Rows[collapsedTab][this.formMasterDataSet.FormSliceInformationList.IsPermissionEditColumn]);

                                this.D9030_F9030_SetSlicePermission(this, new DataEventArgs<SlicePermissionReload>(slicePermissionReload));
                                if (!flagKeyValidinAllSlice)
                                {
                                    //Addded to dispalay record when loaded from query engine 
                                    if (this.formNo != 11072 && this.formNo != 11071)
                                    {
                                        flagKeyValidinAllSlice = !slicePermissionReload.FlagInvalidSliceKey;
                                    }
                                    else
                                    {
                                        if (this.formNo != 11072)
                                        {
                                            flagKeyValidinAllSlice = !slicePermissionReload.FlagInvalidSliceKey;
                                        }
                                        else
                                        {
                                            flagKeyValidinAllSlice = slicePermissionReload.FlagInvalidSliceKey;
                                        }
                                        //if (this.KeyId == -99)
                                        //{
                                        //    flagKeyValidinAllSlice = !slicePermissionReload.FlagInvalidSliceKey;
                                        //}
                                        //else
                                        //{
                                        //    flagKeyValidinAllSlice = !slicePermissionReload.FlagInvalidSliceKey;
                                        //}
                                    }

                                }
                            }
                        }
                    }
                }
                // Coding Ends here
            }

            this.flagValidateKeyId = !flagKeyValidinAllSlice;
            //if (this.formNo == 22081 && this.flagValidateKeyId)
            //{
            //   // this.flagValidateKeyId = false;
            //}
            if (this.flagValidateKeyId)
            {
                this.additionalOperationSmartPartWorkspace.Enabled = false;
                this.reportActionSmartPartWorkSpace.Enabled = false;
                ////Coding added for the issue 5194 on 12/5/2009 by malliga
                ////if the keyid is invalid then auditlink should show thw keyid
                this.AuditlinkLabel.Text = this.auditLinkLabelText;
                ////Ends here for 5194
                this.AuditlinkLabel.Enabled = false;
                this.operationSmartPart.DeleteButtonEnable = false;
            }
            else
            {
                if (this.keyId != -99)
                {
                    this.additionalOperationSmartPartWorkspace.Enabled = true;
                    this.reportActionSmartPartWorkSpace.Enabled = true;
                    this.AuditlinkLabel.Enabled = true;
                    //test
                    if (this.previousKeyId != this.keyId && this.keyId != 0)
                    {
                        this.previousKeyId = this.keyId;
                    }
                    else if (!this.previousKeyId.Equals(0) && this.keyId.Equals(0)) ////HAve to check
                    {
                        //this.keyId = this.previousKeyId;
                        this.flagValidateKeyId = true;
                    }

                    ////this.operationSmartPart.DeleteButtonEnable = true;
                }
                else
                {
                    this.additionalOperationSmartPartWorkspace.Enabled = false;
                    this.reportActionSmartPartWorkSpace.Enabled = false;
                    this.AuditlinkLabel.Enabled = false;
                    this.operationSmartPart.DeleteButtonEnable = false;

                    ////Added for Create Subdivision form - Have to check
                    this.flagValidateKeyId = true;
                }
            }
            this.flagFormResizing = true;

            this.CollapseSlices(string.Empty);
            this.flagFormResizing = false;
            this.ShowDisabledScrollbar();

            if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
            {
                this.OnD9030_F9030_AlertResizableSlice(new DataEventArgs<int>(this.formNo));
                try
                {
                    DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                  "IsPermissionOpen = 'True'");

                    this.loadedWebSlices = webSliceRecords.Length * 2;
                }
                catch (Exception ex)
                {
                }


            }

            int[] currentformno = new int[2];
            currentformno[0] = this.keyId;
            currentformno[1] = this.formNo;
            this.SetActiveKeyId(this, new DataEventArgs<int[]>(currentformno));

            //tempPanel.Visible = false;
            //tempPanel.SendToBack();

            ////Added for displaying added panel for apprisal summary
            if (formFile.Equals("D20050.F35051") || formFile.Equals("D35000.F35000"))
                this.RefreshLoadedSlicePosition();

            //if (this.collapsedTabs != null && this.collapsedWorkSpaces != null)
            //{
            //    if (this.collapsedWorkSpaces[0].ActiveSmartPart.ToString().Equals("D20003.F25011"))
            //    {
            //        this.pageMode = TerraScanCommon.PageModeTypes.View;
            //        //this.LoadSlices();
            //    }
            //}
        }
        #endregion Display slice Related

        #region Alligment Related

        /// <summary>
        /// Alligns the audit link.
        /// </summary>
        private void AllignAuditLink()
        {
            this.AuditlinkLabel.Left = (this.panel5.Left + this.panel5.Width) - this.AuditlinkLabel.Width;
        }

        /// <summary>
        /// Alligns the bar.
        /// </summary>
        private void AllignBar()
        {
            if ((this.ParentForm != null) && (this.ParentForm.Size.Width > this.MinimumSize.Width))
            {
                this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
            }
            else
            {
                this.panel5.Width = this.sliceListPanel.Width;
                this.panel5.Left = this.sliceListPanel.Left;
            }
        }

        /// <summary>
        /// Alligns the query button.
        /// </summary>
        private void AllignQueryButton()
        {
            this.QueryButton.Left = this.panel5.Left;
        }

        /// <summary>
        /// Allignforms the label.
        /// </summary>
        private void AllignformLabel()
        {
            this.FormIDLabel.Left = this.panel5.Left;
        }

        /// <summary>
        /// Alligns the form header.
        /// </summary>
        private void AllignFormHeader()
        {
            this.formHeaderSmartPartdeckWorkspace.Location = new System.Drawing.Point(563, 0);
            this.statusLabelOneSecondPart.Left = this.panel5.Left + this.panel5.Width - this.statusLabelOneSecondPart.Width + 5;
            this.statusLabelOneFirstPart.Left = (this.statusLabelOneSecondPart.Left - this.statusLabelOneFirstPart.Width);
            this.statusLabelTwo.Left = this.panel5.Left + this.panel5.Width - this.statusLabelTwo.Width + 5;
        }

        /// <summary>
        /// Alligns the controls.
        /// </summary>
        private void AllignControls()
        {
            if ((this.ParentForm != null) && (this.ParentForm.Size.Width > this.MinimumSize.Width))
            {
                this.formHeaderSmartPartdeckWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                this.statusLabelOneFirstPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                this.statusLabelOneSecondPart.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                this.statusLabelTwo.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
                this.AuditlinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
                this.FormIDLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
                this.QueryButton.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
                this.QueryEngineWorkSpace.Width = this.sliceListPanel.Width - 8;
                this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;

                if (this.queryEngineSmartPart != null)
                {
                    this.QueryEngineWorkSpace.Width = this.panel5.Width - 8;
                    this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;
                    this.queryEngineSmartPart.Width = this.panel5.Width;
                    this.queryEngineSmartPart.QueryEngineWidth = this.panel5.Width;
                    this.queryEngineSmartPart.PositionPictureBox = this.queryEngineSmartPart.Width;
                }
            }
            else
            {
                this.AllignBar();
                this.AllignAuditLink();
                this.AllignFormHeader();
                this.AllignformLabel();
                this.AllignQueryButton();
                this.QueryEngineWorkSpace.Width = this.panel5.Width - 8;

                if (this.queryEngineSmartPart != null)
                {
                    ////this.queryEngineSmartPart.Width = this.panel5.Width;
                    this.queryEngineSmartPart.Width = this.QueryEngineWorkSpace.Width;

                    this.queryEngineSmartPart.SuspendLayout();
                    this.queryEngineSmartPart.QueryEngineWidth = this.QueryEngineWorkSpace.Width;
                    ////this.QueryEngineWorkSpace.Show();
                    this.queryEngineSmartPart.PositionPictureBox = this.queryEngineSmartPart.Width;

                }
                this.QueryEngineWorkSpace.Left = this.sliceListPanel.Left;
            }
        }

        #endregion Alligment Related

        private void QuickFindMenuItem_Click(object sender, EventArgs e)
        {
            if (this.flagQuickFindVisible)
            {
                this.LoadQuickFind();
            }
        }

        /// <summary>
        /// Tracks the visited record history.
        /// </summary>
        private void TrackRecordHistory()
        {
            // Track only valid keyIds
            if (this.keyId > 0 && this.isValidRecord)
            {
                // Add newly visited record details in history table
                F9612ActivityQueueData historyDataSet = (F9612ActivityQueueData)this.form9030control.WorkItem.RootWorkItem.State["RecordHistoryDataSet"];
                F9612ActivityQueueData.VisitedRecordHistoryRow recentlyVisitedRow = historyDataSet.VisitedRecordHistory.NewVisitedRecordHistoryRow();

                // Assign parameter values
                // Master form Number
                recentlyVisitedRow.FormNumber = this.formNo;

                // Key ID of the form slice
                recentlyVisitedRow.Parameter1 = this.keyId;

                // Feature class ID for that form slice
                if (this.featureClassID > 0)
                {
                    recentlyVisitedRow.Parameter2 = this.featureClassID;
                }

                // Current user ID
                recentlyVisitedRow.UserId = TerraScanCommon.UserId;

                // Combination of MenuName, SubTitle1 and SubTitle2
                if (string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()) && string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim()) && string.IsNullOrEmpty(this.statusLabelOneSecondPart.Text.Trim()))
                {
                    recentlyVisitedRow.Task = this.formHeaderString;
                }
                else if (!string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()) && string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim()) && string.IsNullOrEmpty(this.statusLabelOneSecondPart.Text.Trim()))
                {
                    recentlyVisitedRow.Task = string.Concat(this.formHeaderString, "  \u25A0  ", this.statusLabelTwo.Text.Trim());
                }
                else if (!string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()) && (!string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim()) || !string.IsNullOrEmpty(this.statusLabelOneSecondPart.Text.Trim())))
                {
                    recentlyVisitedRow.Task = string.Concat(this.formHeaderString, "  \u25A0  ", this.statusLabelOneFirstPart.Text.Trim(), " ", this.statusLabelOneSecondPart.Text.Trim(), "  \u25A0  ", this.statusLabelTwo.Text.Trim());
                }
                else if (string.IsNullOrEmpty(this.statusLabelTwo.Text.Trim()) && (!string.IsNullOrEmpty(this.statusLabelOneFirstPart.Text.Trim()) || !string.IsNullOrEmpty(this.statusLabelOneSecondPart.Text.Trim())))
                {
                    recentlyVisitedRow.Task = string.Concat(this.formHeaderString, "  \u25A0  ", this.statusLabelOneFirstPart.Text.Trim(), " ", this.statusLabelOneSecondPart.Text.Trim());
                }

                // Current date and time
                recentlyVisitedRow.VisitedTime = System.DateTime.Now;

                historyDataSet.VisitedRecordHistory.Rows.Add(recentlyVisitedRow);

                // Maintain the visited records dataset in session                
                this.form9030control.WorkItem.RootWorkItem.State["RecordHistoryDataSet"] = historyDataSet;

                DataRow[] webSliceRecords = this.formMasterDataSet.FormSliceInformationList.Select("((FormFile = 'D90010.F95010' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95011' AND (WebHeight IS NULL OR WebHeight <= 0)) OR " +
                                                                                                    "(FormFile = 'D90010.F95012' AND (WebHeight IS NULL OR WebHeight <= 0))) AND " +
                                                                                                    "IsPermissionOpen = 'True'");
                this.loadedWebSlices = webSliceRecords.Length * 2;
            }
            else
            {
                //this.loadedWebSlices = 0;
            }
        }


        /// <summary>
        /// Forms the master_ reduce flicker.
        /// </summary>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        private void FormMaster_FormVisibility(bool isVisible)
        {
            EnablePanelEventArgs visibleInfo;
            visibleInfo.IsSlice = false;
            visibleInfo.IsVisible = isVisible;
            this.OnFormMaster_VisibleForms(this, new DataEventArgs<EnablePanelEventArgs>(visibleInfo));
        }

        #endregion PrivateMethods

        #region Coding Added for F5 on 9030
        /// <summary>
        /// Handles the Click event of the RefeshMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RefeshMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ////If query engine workspace is not visible then do the following lines
                if (!this.QueryEngineWorkSpace.Visible)
                {

                    ////if the page isnot in view mode then do the following lines
                    if (!this.PageMode.Equals(TerraScanCommon.PageModeTypes.View))
                    {
                        // Restrict form jankyness issue
                        this.FormMaster_FormVisibility(false);
                        // Check the page Status when the edit/new mode
                        this.CheckPageStatus();
                    }
                    else
                    {
                        // Avoid form load for -99 keyid
                        if (this.keyId > 0)
                        {
                            // Restrict form jankyness issue
                            this.FormMaster_FormVisibility(false);
                            ////Calling LoadSlice Details Information
                            this.SetMasterformGeneralProperties();
                            SliceReloadActiveRecord sliceReloadActiveRecord;
                            sliceReloadActiveRecord.MasterFormNo = this.formNo;
                            sliceReloadActiveRecord.SelectedKeyId = this.keyId;
                            this.OnD9030_F9030_LoadSliceDetails(new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                if (!this.QueryEngineWorkSpace.Visible && (this.keyId > 0 || !this.PageMode.Equals(TerraScanCommon.PageModeTypes.View)))
                {
                    this.FormMaster_FormVisibility(true);
                }
                this.isJerkFree = true;
            }

        }
        /// <summary>
        /// Called when [web slice_ form master refresh].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;System.Int32&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.WebSlice_FormMasterRefresh, Thread = ThreadOption.UserInterface)]
        public void OnWebSlice_FormMasterRefresh(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                EventArgs menuEvent = new EventArgs();
                this.RefeshMenuItem_Click(this.RefeshMenuItem, menuEvent);
            }
        }

        /// <summary>
        /// Show quick find form for WebSlice.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.WebSlice_QuickFind, Thread = ThreadOption.UserInterface)]
        public void OnWebSlice_QuickFind(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.formNo == eventArgs.Data)
            {
                if (this.flagQuickFindVisible)
                {
                    this.LoadQuickFind();
                }
            }
        }
        #endregion
    }
}