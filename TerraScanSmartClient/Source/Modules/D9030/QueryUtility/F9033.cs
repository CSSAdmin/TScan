//--------------------------------------------------------------------------------------------
// <copyright file="F9033.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the QueryEngine.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Dec 06		VINOTH      	    CREATED
// 16 Feb 07        VINOTH              CHANGE ORDERS
// 14 May 07        VINOTH              CHANGE ORDERS
// 09 Aug 07        VINOTH              CO_BILLS
// 25 Mar 08        LathaMaheswari.D    Implemented Infragistics Custom Grid functionlity
// 03 FEB 11        Manoj Kumar         Implemented new EventArgs for Fee Managment CO
// 16 FEB 11        Manoj Kumar         Changes in the keyId Xml to QueryEngine Update join ForeignKEyId xml.   
// 04 NOV 11        Manoj Kumar         Pass the keyID to the Field Query Engine Data.
// 20160622         Priyadharshini      #21718 TSBG - D9030.F9033 Query Engine form - To Excel button bug.
//20160623         priyadharshini      TSBG - A new field added to a T2 query view via Field Management fails to export to Excel properly,Added Infragistics 11 version for fix the issue
//20170111          priyadharshini     TSBG 11005 Subfund Management: Query View not showing enough decimal places for Rate
//**********************************************************************************************/
namespace D9030
{
    #region NameSpace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using Infragistics.Win;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Utilities;
    using TerraScan.BusinessEntities;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using System.Xml;
    using Infragistics.Documents.Excel;
    using Infragistics.Win.UltraWinGrid.ExcelExport;
    using Microsoft.Win32;
    using TerraScan.Helper;
    using System.Runtime.InteropServices;


    #endregion NameSpace

    /// <summary>
    /// F9033 Class
    /// </summary>
    public partial class F9033 : BaseSmartPart
    {
        #region MemberVariable

        /// <summary>
        /// int Stores selected Rows
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// form9033Control Control Name
        /// </summary>
        private F9033Controller form9033Control;

        /// <summary>
        /// Creates the Instance of ListQueryViewTable
        /// </summary>
        private F9033QueryEngineData.ListQueryViewDataTable queryViewTable = new F9033QueryEngineData.ListQueryViewDataTable();

        /// <summary>
        /// Creates the Instance of ListQueryLayoutDataTable
        /// </summary>
        private F9033QueryEngineData.ListQueryLayoutDataTable listQueryLayout = new F9033QueryEngineData.ListQueryLayoutDataTable();

        /// <summary>
        /// Creates the Instance of ListQuerySnapShotDataTable
        /// </summary>
        private F9033QueryEngineData.ListQuerySnapShotDataTable listQuerySnapShotMerge = new F9033QueryEngineData.ListQuerySnapShotDataTable();

        /// <summary>
        /// The instance of the form master smartpart
        /// </summary>
        private F9030 formMasterSmartPart;

        /// <summary>
        /// Stores the Default Layout
        /// </summary>
        private MemoryStream intialLayout = new MemoryStream();

        /// <summary>
        /// stores resetLayout
        /// </summary>
        private MemoryStream resetLayout = new MemoryStream();

        /// <summary>
        /// Stores Layout Details after loading the datas with QueryView
        /// </summary>
        private MemoryStream queryViewLayout = new MemoryStream();

        /// <summary>
        /// Stores QueryEngine Data
        /// </summary>
        private DataSet queryEngineData = new DataSet();

        /// <summary>
        /// Stores systemSnapShot Data
        /// </summary>
        DataSet systemSnapShotData = new DataSet();

        /// <summary>
        /// Integer stores currentRowIndex
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// Integer stores currentKeyId
        /// </summary>
        private int currentRowKeyId;

        /// <summary>
        /// Integer stores totalRecords
        /// </summary>
        private int totalRecords;

        /// <summary>
        /// Holds system snap shot Id
        /// </summary>
        private int? systemSnapShotId;

        /// <summary>
        /// Flag to load layout
        /// </summary>
        private bool loadedLayout = true;

        /// <summary>
        /// String stores KeyIdColumn
        /// </summary>
        private string keyIdColumnName;

        /// <summary>
        /// Integer Stores KeyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Integer stores FormMasterNo
        /// </summary>
        private int formMasterNo;

        /// <summary>
        /// Stores Selected Row Index
        /// </summary>
        private int selectedRowIndex;

        /// <summary>
        /// Stores resetLayout XML String
        /// </summary>
        private string updatedXmlString;

        /// <summary>
        /// stores the Default XML String
        /// </summary>
        private string defaultXmlString;

        /// <summary>
        /// To Store layout Name
        /// </summary>
        private string layoutName;

        /// <summary>
        /// Checks column Name Invalid
        /// </summary>
        private bool columnNameInvalid;

        /// <summary>
        /// Checks for QueryView
        /// </summary>
        private bool queryViewUnavailable;

        /// <summary>
        /// To Store sort status
        /// </summary>
        private bool sortChange;

        /// <summary>
        /// Not From Menu
        /// </summary>
        private bool notFromMenu;

        /// <summary>
        /// loaded XML Name
        /// </summary>
        private string loadXML;

        /// <summary>
        /// Save the viewName
        /// </summary>
        private string viewName;

        /// <summary>
        /// Stores filteredRow Count
        /// </summary>
        private int filterRowCount;

        /// <summary>
        /// string query view Name
        /// </summary>
        private string queryViewName;

        /// <summary>
        /// string Foreign Key Name
        /// </summary>
        private string foreignKeyName;

        /// <summary>
        /// string format
        /// </summary>
        private string textFormat = "#,###";

        /// <summary>
        /// permission fields from the form formmaster
        /// </summary>
        private PermissionFields formMasterPermissionFields;

        /// <summary>
        /// Stores the layout Id
        /// </summary>
        private int layoutId;

        /// <summary>
        /// Column Found
        /// </summary>
        private bool columnFound = true;

        /// <summary>
        /// Store the status of Deleted Record
        /// </summary>
        private bool keyIdDeleted;

        /// <summary>
        /// Integer Key Value
        /// </summary>
        private int keyValue;

        /// <summary>
        /// Stores Temporavary KayValue
        /// </summary>
        private int tempKeyValue;

        /// <summary>
        /// Temp Row Index
        /// </summary>
        private int tempRowIndex;

        /// <summary>
        /// Previous Row Index
        /// </summary>
        private int prevRowIndex;

        /// <summary>
        /// member to show the aggregate function
        /// </summary>
        private SummaryDialog aggregateFunctionDialogForm;

        /// <summary>
        /// QueryLoad Status
        /// </summary>
        private bool queryLoadFlag;

        /// <summary>
        /// Summary Status
        /// </summary>
        private bool summaryFlag;

        /// <summary>
        /// Stores the Default Layout Id
        /// </summary>
        private int defaultLayoutId;

        /// <summary>
        /// Stores the Status, Whether the layoutMangementForm is open or not
        /// </summary>
        private bool layoutMangementFlag;

        /// <summary>
        /// To check whether its from snapShot
        /// </summary>
        private bool snapShotLoaded;

        /// <summary>
        /// To hold SnapShot Count
        /// </summary>
        private int snapShotCount;

        /// <summary>
        /// Default Layout XML
        /// </summary>
        private string f9033defaultLayout;

        /// <summary>
        /// To Save pinned Data
        /// </summary>
        private DataTable pinnedDataTable = new DataTable("Table1");

        /// <summary>
        /// To save unPinned data
        /// </summary>
        private DataTable unpinnedDataTable = new DataTable("Table2");


        ///<SUMMARY>
        /// to sve update pined Data
        /// </SUMMARY>
        private DataTable pinnedDataTable1 = new DataTable("KEYIDS");

        /// <summary>
        /// To save unPinned data
        /// </summary>
        private DataTable unpinnedDataTable1 = new DataTable("FKEYYIDS");

        /// <summary>
        /// To store whether default layout is there or not
        /// </summary>
        private bool defaultLayoutExists;

        /// <summary>
        /// To store Selected Layout XML.
        /// </summary>
        private string selectedLayoutXml;

        /// <summary>
        /// Sets the Selected Layout Index
        /// </summary>
        private int selectedLayoutIndex;

        /// <summary>
        /// Sets SystemSnapShotCount
        /// </summary>
        private int systemSnapShotCount;

        /// <summary>
        /// Sets the Status of systemSnapShot
        /// </summary>
        private bool systemSnapShotLoaded;

        /// <summary>
        /// Used to store the whether F9037 form is Unsaved 
        /// </summary>
        private bool F9037SummaryFormUnsaved;

        /// <summary>
        /// Holds the Modified XML
        /// </summary>
        private string modifiedLayoutXml;

        /// <summary>
        /// Parameter List
        /// </summary>
        private List<string> parameterList;

        /// <summary>
        /// parameter Count
        /// </summary>
        private Int16 parameterCount;

        /// <summary>
        /// Param Count
        /// </summary>
        private Int16 paramCount = 0;

        /// <summary>
        /// Saves the Current Layout
        /// </summary>
        private MemoryStream currentLayout = new MemoryStream();

        /// <summary>
        /// resetLinkVisible
        /// </summary>
        private bool resetLinkVisible = false;

        /// <summary>
        /// Sorting Order
        /// </summary>
        private string sortOrder;

        /// <summary>
        /// Sorted Column Name
        /// </summary>
        private string sortKey;

        /// <summary>
        /// String for sorting
        /// </summary>
        private string sortData;

        /// <summary>
        /// String for filter
        /// </summary>
        private string filterData;

        /// <summary>
        /// Flag for queryview selection changed
        /// </summary>
        private bool hasChangeQueryView;

        /// <summary>
        /// Flag for binded column
        /// </summary>
        private bool columnBind;

        /// <summary>
        /// String for summaty
        /// </summary>
        private string summaryData;

        /// <summary>
        /// Store Summary Data
        /// </summary>
        private DataTable summaryDetails = new DataTable("Table");

        /// <summary>
        /// Store Temporary Summary Details
        /// </summary>
        private DataTable tempSummaryDetails = new DataTable("Table");

        /// <summary>
        /// Store Columns Details
        /// </summary>
        private DataTable calculatedColumnDetails = new DataTable("Table");

        /// <summary>
        /// Store Filter Details
        /// </summary>
        private DataTable filterDetails = new DataTable("Table");

        /// <summary>
        /// Store Sort Details
        /// </summary>
        private DataTable sortDetails = new DataTable("Table");

        /// <summary>
        /// Flag for summary value changes
        /// </summary>
        private bool summaryChanged;

        /// <summary>
        /// Record count
        /// </summary>
        private int overAllRecord;

        /// <summary>
        /// Columns XML
        /// </summary>
        private string calculatedData = null;

        /// <summary>
        /// Variable to store Formula Builder Formula
        /// </summary>
        private string formula = string.Empty;

        /// <summary>
        /// Flag for Form Laod
        /// </summary>
        private bool isFormLoad;

        /// <summary>
        /// Flag for Default Query
        /// </summary>
        private bool isDefaultQueryLoad;

        /// <summary>
        /// Flag for F5 key press
        /// </summary>
        private bool queryRefreshed;

        /// <summary>
        /// Store newly saved records KeyIds
        /// </summary>
        private DataTable keyIdCollection = new DataTable("Table");

        /// <summary>
        /// Store newly saved records KeyIds
        /// </summary>
        private DataTable optionalkeyIdCollection = new DataTable("Table");

        /// <summary>
        /// KeyId passes to master form
        /// </summary>
        private int openWithKeyId;

        /// <summary>
        /// Queryviewid
        /// </summary>
        private int tempQueryViewId = 0;

        /// <summary>
        /// Layout id
        /// </summary>
        private int tempLayoutId = 0;

        /// <summary>
        /// Dynamic query returned from DataBase
        /// </summary>
        private string queryString;

        /// <summary>
        /// DataTable for Column Chooser
        /// </summary>
        private DataTable tempQueryData = new DataTable("QueryViewData");

        /// <summary>
        /// Flag for save
        /// </summary>
        private bool isSaved;

        /// <summary>
        /// Table to store Grid Design
        /// </summary>
        private DataTable designDetailsTable = new DataTable("Table");

        /// <summary>
        /// Flag restrict property changed event on isPinned checkbox selected
        /// </summary>
        private bool isPinned = false;

        #endregion MemberVariable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9033"/> class.
        /// </summary>
        public F9033()
        {
            this.InitializeComponent();
            this.QueryEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.QueryEnginePictureBox.Height, this.QueryEnginePictureBox.Width, "Query Engine", 23, 54, 96);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9033"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="formHeader">The form header.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="formMasterNo">The form master no.</param>
        public F9033(string columnName, string formHeader, int keyId, int formMasterNo)
        {
            this.InitializeComponent();
            this.keyIdColumnName = columnName;
            this.keyId = keyId;
            this.formMasterNo = formMasterNo;
            this.QueryEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.QueryEnginePictureBox.Height, this.QueryEnginePictureBox.Width, "Query Engine", 23, 54, 96);
            this.selectedRowIndex = -1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9033"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="formHeader">The form header.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="formMasterNo">The form master no.</param>
        /// <param name="formMasterPermission">The form master permission.</param>
        public F9033(string columnName, string formHeader, int keyId, int formMasterNo, PermissionFields formMasterPermission)
        {
            // THIS CONSTRUCTOR FIRES FOR ALL THE GENERALFORM
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
            }

            this.formMasterPermissionFields = formMasterPermission;
            this.keyIdColumnName = columnName;
            this.keyId = keyId;
            this.formMasterNo = formMasterNo;
            this.selectedRowIndex = -1;
            this.QueryEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.QueryEnginePictureBox.Height, this.QueryEnginePictureBox.Width, "Query Engine", 23, 54, 96);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9033"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="formHeader">The form header.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="formMasterNo">The form master no.</param>
        /// <param name="formMasterPermission">The form master permission.</param>
        /// <param name="systemSnapshotLoaded">flag for System Snapshot state</param>
        public F9033(string columnName, string formHeader, int keyId, int formMasterNo, PermissionFields formMasterPermission, bool systemSnapshotLoaded)
        {
            // IN THIS CONSTRUCTOR, WE USED SNAPSHOTLOADED BOOLEAN VALUE.
            // THIS WILL BE TRIGGERED ONLY WHEN SYSTEMSNAPSHOT WATS TO LOAD

            this.InitializeComponent();
            this.formMasterPermissionFields = formMasterPermission;
            this.keyIdColumnName = columnName;
            this.keyId = keyId;
            this.formMasterNo = formMasterNo;
            this.systemSnapShotLoaded = systemSnapshotLoaded;
            this.selectedRowIndex = -1;
            this.QueryEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.QueryEnginePictureBox.Height, this.QueryEnginePictureBox.Width, "Query Engine", 23, 54, 96);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9033"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="formHeader">The form header.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="formMasterNo">The form master no.</param>
        /// <param name="formMasterPermission">The form master permission.</param>
        /// <param name="parameterCount">Parameter Count</param>
        public F9033(string columnName, string formHeader, int keyId, int formMasterNo, PermissionFields formMasterPermission, Int16 parameterCount)
        {
            // THIS CONSTRUCTOR FIRES ONLY WHEN THE FORM NEEDS SOME EXTRA PARAMETER
            // TO LOAD THE FORMS EG: EVENTENGINE NEEDS SOME 2 PARAMETERS TO LOAD THE FORM

            this.InitializeComponent();
            this.formMasterPermissionFields = formMasterPermission;
            this.keyIdColumnName = columnName;
            this.keyId = keyId;
            this.formMasterNo = formMasterNo;
            this.paramCount = parameterCount;
            this.selectedRowIndex = -1;
            this.QueryEnginePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.QueryEnginePictureBox.Height, this.QueryEnginePictureBox.Width, "Query Engine", 23, 54, 96);
        }

        #endregion Constructor

        #region EventPublication

        /// <summary>
        /// event publication to intimate form master about the selected keyid doesnot exists
        /// due to change in the filter option
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_OnFilter_KeyIdReset, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> D9030_F9033_OnFilter_KeyIdReset;

        /// <summary>
        /// event to intimate form master about queryengine close
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_QueryEngineClose, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<int>> D9030_F9033_QueryEngineClose;

        /// <summary>
        /// event to intimate form master about systemSnapShotId
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_SystemSnapshotCompleteEvent, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount>> D9030_F9033_SystemSnapshotCompleteEvent;

        ///<summary>
        /// event to intimate form about recordset
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9033_GetRecordsetXML, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<string[]>> D9030_F9033_GetRecordsetXML;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

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


        ///<summary>
        /// Set the Naivagation Position of the record
        /// </summary>
        [EventPublication(EventTopics.F9030_NavigationRecord, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> NavigationPosition;

        #endregion EventPublication

        #region Property

        #region F9033Control

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F9033Controller F9033Control
        {
            get { return this.form9033Control as F9033Controller; }
            set { this.form9033Control = value; }
        }

        #endregion F9033Control

        #region NoDefaultLayout

        /// <summary>
        /// Gets or sets a value indicating whether [no default layout].
        /// </summary>
        /// <value><c>true</c> if [no default layout]; otherwise, <c>false</c>.</value>
        private bool NoDefaultLayout
        {
            get
            {
                return this.defaultLayoutExists;
            }

            set
            {
                this.defaultLayoutExists = value;
            }
        }

        #endregion NoDefaultLayout

        #region PublicProperties

        #region CurrentRowIndex

        /// <summary>
        /// Gets or sets the index of the current row.
        /// </summary>
        /// <value>The index of the current row.</value>
        public int CurrentRowIndex
        {
            get
            {
                return this.currentRowIndex;
            }

            set
            {
                this.ActiveRowDetails();
                this.currentRowIndex = value;
            }
        }

        #endregion CurrentRowIndex

        #region IsKeyIdDeleted

        /// <summary>
        /// Gets or sets a value indicating whether [id key id deleted].
        /// </summary>
        /// <value><c>true</c> if [id key id deleted]; otherwise, <c>false</c>.</value>
        public bool IdKeyIdDeleted
        {
            get
            {
                return this.keyIdDeleted;
            }

            set
            {
                this.keyIdDeleted = value;
            }
        }

        #endregion

        #region CurrentRowKeyId

        /// <summary>
        /// Gets or sets the current row key id.
        /// </summary>
        /// <value>The current row key id.</value>
        public int CurrentRowKeyId
        {
            get
            {
                bool keyFound = this.FindKeyId();
                return this.currentRowKeyId;
            }

            set
            {
                bool keyFound = this.FindKeyId();
                if (keyFound)
                {
                    this.currentRowKeyId = value;
                }
                else
                {
                    this.currentRowKeyId = -99;
                }
            }
        }

        #endregion CurrentRowKeyId

        #region TotalRecord

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        public int TotalRecords
        {
            get
            {
                return this.totalRecords;
            }

            set
            {
                this.totalRecords = value;
            }
        }

        #endregion TotalRecord

        #region ColumnNameInvalid

        /// <summary>
        /// Gets or sets a value indicating whether [column name invalid].
        /// </summary>
        /// <value><c>true</c> if [column name invalid]; otherwise, <c>false</c>.</value>
        public bool ColumnNameInvalid
        {
            get
            {
                return this.columnNameInvalid;
            }

            set
            {
                this.columnNameInvalid = value;
            }
        }

        #endregion ColumnNameInvalid

        #region QueryViewUnavailable

        /// <summary>
        /// Gets or sets a value indicating whether [column name invalid].
        /// </summary>
        /// <value><c>true</c> if [column name invalid]; otherwise, <c>false</c>.</value>
        public bool QueryViewUnavailable
        {
            get
            {
                return this.queryViewUnavailable;
            }

            set
            {
                this.queryViewUnavailable = value;
            }
        }

        #endregion QueryViewUnavailable

        #region SystemSnapShotLoaded

        /// <summary>
        /// Gets or sets a value indicating whether [system snap shot loaded].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [system snap shot loaded]; otherwise, <c>false</c>.
        /// </value>
        public bool SystemSnapShotLoaded
        {
            get
            {
                return this.systemSnapShotLoaded;
            }

            set
            {
                this.systemSnapShotLoaded = value;
            }
        }

        #endregion SystemSnapShotLoaded

        #region QueryEngineWidth

        /// <summary>
        /// Sets the width of the query engine.
        /// </summary>
        /// <value>The width of the query engine.</value>
        public int QueryEngineWidth
        {
            set
            {
                this.Width = value;
                this.QueryEnginePanel.Width = value;
            }
        }

        /// <summary>
        /// Sets the position picture box.
        /// </summary>
        /// <value>The position picture box.</value>
        public int PositionPictureBox
        {
            set
            {
                if (this.Width != this.Parent.Width)
                {
                    this.Width = this.Parent.Width;
                    this.QueryEnginePanel.Width = this.Width;
                    this.panel1.Left = this.Width - this.panel1.Width;
                }
                else
                {
                    this.panel1.Left = value - this.panel1.Width;
                }

                this.QueryEngineGrid.Width = this.QueryEnginePanel.Width - this.panel1.Width - this.QueryEngineGrid.Left;
                this.DefaultLink.Left = this.QueryLayoutCombo.Right + 10;
                this.ClearLink.Left = this.QuerySnapShotCombo.Right + 3;
                this.ResetLink.Left = this.QueryLayoutCombo.Right + 5;
                this.CloseButton.Left = this.QueryEnginePanel.Width - ((this.CloseButton.Width * 2) - (this.CloseButton.Width / 2));
                this.ReplaceButton.Left = this.CloseButton.Left - (this.ReplaceButton.Width + this.ReplaceButton.Width / 10);
                this.HeaderLabel.Left = this.QueryEnginePanel.Width - (this.HeaderLabel.Width + this.HeaderLabel.Width / 3);
                //// this.NoOfRecords.Left = this.QueryEnginePanel.Width - (this.NoOfRecords.Width + this.NoOfRecords.Width / 3);
            }
        }

        #endregion QueryEngineWidth

        #region ParameterList

        /// <summary>
        /// Gets or sets the parameter list.
        /// </summary>
        /// <value>The parameter list.</value>
        public List<string> ParameterList
        {
            get { return parameterList; }
            set { parameterList = value; }
        }

        #endregion ParameterList

        #region ParameterCount

        /// <summary>
        /// Gets or sets the parameter count.
        /// </summary>
        /// <value>The parameter count.</value>
        public Int16 ParameterCount
        {
            get { return parameterCount; }
            set { parameterCount = value; }
        }

        #endregion ParameterCount

        #endregion PublicProperties

        #endregion Property

        #region EventSubscription

        /// <summary>
        /// Called when [D9030_ F9038_ load layout details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9038_LoadLayoutDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9038_LoadLayoutDetails(object sender, TerraScan.Infrastructure.Interface.EventArgs<LoadLayoutDetails> eventArgs)
        {
            try
            {
                //// THIS EVENT TRIGERRED FROM LAYOUTMANAGEMENT FORM TO LOAD THE SELECTED LAYOUT IN 9038
                if (this.formMasterNo == eventArgs.Data.MasterFormNo)
                {
                    int snapShotId = 0;

                    //// SETS THE SNAPSHOTID, ONLY IF THERE IS SOME SNAPSHOT LOADED
                    if (this.QuerySnapShotCombo.SelectedIndex != -1)
                    {
                        if (this.QuerySnapShotCombo.Items.Count > 0)
                        {
                            int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out snapShotId);
                        }
                    }

                    //// SETS ALL THE PARAMETERS VALUES TO CORRESPONDING VARIABLE FROM THE EVENTARGS
                    this.loadXML = eventArgs.Data.LayoutXml;
                    this.viewName = eventArgs.Data.LayoutName;
                    this.layoutId = eventArgs.Data.LayoutID;
                    this.layoutMangementFlag = true;

                    //// THIS PIECE OF CODE, GETS THE LAYOUT AND LOADS THE CORRESPODING 
                    //// LAYOUT TO THE GRID. SINCE THE GRID ACCEPTS ONLY IN THE MEMORY STREAM
                    //// I'M COMVERTING XMLSTRING TO MEMORY STREAM.
                    if (!string.IsNullOrEmpty(this.loadXML))
                    {
                        this.QueryEngineGrid.DataSource = this.TempQueryGrid.DataSource;

                        this.summaryDetails.Rows.Clear();
                        this.tempSummaryDetails.Rows.Clear();

                        System.Text.Encoding enc = System.Text.Encoding.ASCII;

                        //// GETS THE STRING AS BYTE ARRAY
                        Byte[] fileAsByte = enc.GetBytes(this.loadXML);

                        //// CONVERTS BYTE ARRAY TO MEMORYSTREAM
                        MemoryStream ms = new MemoryStream(fileAsByte);

                        this.summaryFlag = false;

                        //// LOADS THE LAYOUT FROM XML DATA
                        this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                        Byte[] fileAsByteTemp = enc.GetBytes(this.loadXML);

                        //// CONVERTS BYTE ARRAY TO MEMORYSTREAM
                        MemoryStream tempStream = new MemoryStream(fileAsByteTemp);

                        this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                        //// IF THE LAYOUT HAVE SOME UNBOUNDED COLUMN(S) IN THE LAYOUT WITH SOME FORMULA THEN
                        //// IT WONT LOADS THE UNBOUNDED VALUES. THE VALUES COMES ONLY WHEN IF WE ADD ONE
                        //// MORE UNBOUNDED COLUMN TO THE GRID. SO JUST TO OVERCOME THIS ISSUE
                        //// I HAVE ADDED A UNBOUNDED COLUMN AND THEN REMOVED THE SAME.
                        #region FormulaIssue Fix

                        this.AddRemoveTemCol();

                        #endregion

                        this.queryLoadFlag = true;
                        this.summaryFlag = true;

                        //// SAVE THE CURRENT LAYOUT AS INTIAL LAYOUT
                        this.defaultXmlString = this.loadXML;

                        //// THIS METHOD GETS THE SELECTED ROW DETAILS AND SETS FOR THE FORM MASTER
                        this.OnFilterRowSelect();

                        //// THIS METHOD RELOADS THE SNAPSHOT COMBO. THE PARAMETER IS TO CHECK
                        //// FOR THE SYSTEMSNAPSHOT LOADED OR NOT.
                        //this.PopulateQuerySnapShotCombo(false);

                        //// THIS METHOD RELOADS THE LAYOUTCOMBO.
                        this.PopulateQueryLayoutCombo();
                        if (this.QueryLayoutCombo.Items.Count > 0)
                        {
                            if (this.layoutId > 0)
                            {
                                //// AFTER LOADING THE SELECTED LAYOUT SELECT THE CORRESPONDING 
                                //// SELECTED IN THE COMBO.
                                this.QueryLayoutCombo.SelectedValue = this.layoutId;
                                this.LayoutRecords.Visible = true;

                                if (this.QuerySnapShotCombo.Items.Count > 0)
                                {
                                    int queryViewId = 0;
                                    this.selectedLayoutIndex = this.QueryLayoutCombo.SelectedIndex;

                                    if (this.listQueryLayout.Rows.Count > 0)
                                    {
                                        //// THIS PIECE OF CODE CHECKS WHETHER SNAPSHOT IS LOADED OR NOT.
                                        //// IF LOADED, APPLIES THE LAYOUT FOR THE CORRESPONDING SELECTED
                                        //// SNAPSHOT IN THE COMBO OR ELSE IT APPLIES
                                        //// IT APPLIES FOR THE QUERYVIEW RESULT
                                        if (this.QuerySnapShotCombo.SelectedIndex != -1)
                                        {
                                            if (this.QuerySnapShotCombo.Items.Count > 0)
                                            {
                                                bool snapShotIdFound = this.FindSnapShotItems(snapShotId);
                                                if (snapShotIdFound)
                                                {
                                                    this.QuerySnapShotCombo.SelectedValue = snapShotId;
                                                    this.SnapShotRecords.Visible = true;
                                                }
                                                else
                                                {
                                                    this.QuerySnapShotCombo.SelectedIndex = -1;
                                                    this.SnapShotRecords.Visible = false;
                                                }
                                            }

                                            if (this.QueryViewCombo.Items.Count > 0)
                                            {
                                                if (this.QueryViewCombo.SelectedIndex != -1)
                                                {
                                                    int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                                                }
                                            }

                                            if (this.QuerySnapShotCombo.SelectedIndex != -1)
                                            {
                                                ////this.ApplyLayout();
                                                this.GetLayoutDetails();
                                                this.LoadSnapShot(snapShotId, queryViewId);
                                                ////this.ApplyLayout();
                                                this.SetLayoutDetails();
                                            }
                                            else
                                            {
                                                //// this.ApplyLayout();
                                                this.GetLayoutDetails();
                                                this.LoadQueryEngine();
                                                //// this.ApplyLayout();
                                                this.SetLayoutDetails();
                                            }
                                        }
                                        else
                                        {
                                            ////this.ApplyLayout();
                                            this.GetLayoutDetails();
                                            this.LoadQueryEngine();
                                            ////this.ApplyLayout();
                                            this.SetLayoutDetails();
                                        }
                                    }
                                }
                                else
                                {
                                    this.GetLayoutDetails();
                                    this.LoadQueryEngine();
                                    this.SetLayoutDetails();
                                }
                            }
                        }

                        // SETS THE FILTERED ROW COUNT FROM THE GRID TO THE TEXTBOX, TOTALRECORDS COUNT
                        ////this.NoOfRecords.Text = this.QueryEngineGrid.Rows.FilteredInRowCount.ToString(this.textFormat) + " Rows";
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.GetNumberOfRecords(false);
                        this.layoutName = this.viewName;
                        this.LayoutFieldLabel.Text = "Layout - " + this.viewName;

                        this.ResetLink.Visible = false;

                        if (this.QueryLayoutCombo.SelectedIndex == -1)
                        {
                            this.LayoutRecords.Visible = false;
                        }
                        else
                        {
                            this.LayoutRecords.Visible = true;
                        }
                    }

                    this.SetCheckBoxPinningLayout();
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
        /// Called when [D9030_ F9033_ load snap shot details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_LoadSnapShotDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9033_LoadSnapShotDetails(object sender, TerraScan.Infrastructure.Interface.EventArgs<LoadSnapShotDetails> eventArgs)
        {
            try
            {
                if (eventArgs.Data.MasterFormNO == this.formMasterNo)
                {
                    // THIS METHOD RELOADS THE SNAPSHOT COMBO. THE PARAMETER IS TO CHECK
                    // FOR THE SYSTEMSNAPSHOT LOADED OR NOT.
                    this.PopulateQuerySnapShotCombo(false);

                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        this.ClearLink.Visible = true;

                        this.SnapShotRecords.Visible = true;
                        this.QuerySnapShotCombo.SelectedValue = eventArgs.Data.SnapShotId;

                        int selectedLayoutValue = 0;

                        //// GETS THE SELECTED VALUE IN THE LAYOUT COMBO
                        if (this.QueryLayoutCombo.SelectedValue != null)
                        {
                            int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                        }

                        if (selectedLayoutValue != 0)
                        {
                            //// FINDS THE SELECTED VALUE IN THE COMBO ITEMS
                            //// <TRUE>APPLIES THE SELECTED LAYOUT</TRUE>
                            ////<FALSE>SETS THE COMBO VALUE TO -1</FALSE>
                            bool findLayoutItem = this.FindLayoutItems(selectedLayoutValue);

                            if (findLayoutItem)
                            {
                                this.QueryLayoutCombo.SelectedValue = selectedLayoutValue;
                                this.ApplyLayout();
                                this.GetGridDesignDetails();
                                this.GetNewColumnValue();
                                this.GetSortValue();
                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);
                            }
                            else
                            {
                                this.QueryLayoutCombo.SelectedIndex = -1;
                                this.LayoutRecords.Visible = false;
                                this.ResetLink.Visible = false;
                            }
                        }

                        //// LOADS SNAPSHOT BY GETTING SNAPSHOTID AND QUERYVIEWID AS A PARAMETER
                        this.LoadSnapShot(eventArgs.Data.SnapShotId, eventArgs.Data.QueryViewId);

                        this.GetNumberOfRecords(false);

                        //// SHOWS MESSAGE BOX WITH GRID-ROW COUNT AND ORIGINAL SNAPSHOT RECORD COUNT IN THE DB
                        ////if (this.snapShotCount != this.QueryEngineGrid.Rows.FilteredInRowCount || this.QueryEngineGrid.Rows.Count <= 0)
                        ////if (this.snapShotCount != this.QueryEngineGrid.Rows.FilteredInRowCount || this.QueryEngineGrid.Rows.Count <= 0)
                        ////{
                        ////MessageBox.Show("Only " + this.snapShotCount + " of " + this.QueryEngineGrid.Rows.FilteredInRowCount + " records were found", ConfigurationWrapper.ApplicationName + " - Snapshot Record Mismatch ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                        {
                            if (eventArgs.Data.SnapShotCount != Convert.ToInt32(this.queryEngineData.Tables[1].Rows[1][0].ToString()))
                            {
                                MessageBox.Show("Only " + this.queryEngineData.Tables[1].Rows[1][0].ToString() + " records were found", ConfigurationWrapper.ApplicationName + " - Snapshot Record Mismatch ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        ////}

                        this.SetUltraData();

                        if (this.filterData != null)
                        {
                            this.SetFilterValue();
                        }
                        else
                        {
                            this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        }

                        if (this.sortData != null)
                        {
                            this.SetSortOrder();
                        }

                        this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        if (this.summaryDetails.Rows.Count > 0)
                        {
                            this.SetSummaryValue();
                        }
                        else
                        {
                            this.SetTempSummaryValue();
                        }

                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

                        if (this.calculatedColumnDetails.Rows.Count > 0)
                        {
                            this.SetNewColumnValue();
                        }

                        this.SetGridDesignDetails();
                    }
                    else
                    {
                        this.ClearLink.Visible = false;
                    }

                    this.SetCheckBoxPinningLayout();
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
        /// Called when [D9030_ F9033_ load system snap shot details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_SetSystemSnapshotEvent, ThreadOption.UserInterface)]
        public void OnD9030_F9033_SetSystemSnapshotEvent(object sender, TerraScan.Infrastructure.Interface.EventArgs<LoadSystemSnapShotDetails> eventArgs)
        {
            try
            {
                //// THIS EVENT IS TO COMPLETE THE SYSTEM SNAPSHOT PROCESS
                if (eventArgs.Data.MasterFormNO == this.formMasterNo)
                {
                    string recordSetXml;

                    //// CHECK FOR THE SYSTEM SNAPSHOT <TRUE>INSERT SYSTEM SNAPSHOT ITEM</TRUE>
                    //// <FALSE>LOADS SYSTEMSNAPHOT IN THE CURRENT FORM</FALSE>
                    if (!eventArgs.Data.IsSystemSnapShotLoaded)
                    {
                        //// GETS THE SNAPSHOTED KEYIDS FROM THE GRID
                        this.GetSnapShotRecords(true);

                        // // GENERATE THE KEYIDS AS AN XML DEPENDING UPON THE RECORDSET TYPE
                        recordSetXml = this.GetRecordSetType(eventArgs.Data.RecordsetType);
                        if (!string.IsNullOrEmpty(recordSetXml))
                        {
                            //// INSERT THE GENERATED XML KEYIDS AS A SYSTEM-SNAPSHOT ITEM
                            this.systemSnapShotId = this.form9033Control.WorkItem.F9033_InsertSnapShotItems(TerraScanCommon.UserId, recordSetXml);
                            if (this.systemSnapShotId != -1)
                            {
                                SetSystemSnapShotIdnCount setSystemShotIdnCount;
                                int outSystemSnapShotId;
                                int.TryParse(this.systemSnapShotId.ToString(), out outSystemSnapShotId);
                                setSystemShotIdnCount.SystemSnapShotId = outSystemSnapShotId;
                                setSystemShotIdnCount.SystemSnapShotCount = this.systemSnapShotCount;

                                //// SENDS THE SYSTEM SNAPSHOTID TO THE PARENT FORM FROM WHERE IT CALLED                                
                                this.D9030_F9033_SystemSnapshotCompleteEvent(this, new TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount>(setSystemShotIdnCount));

                                int systemSnapShotID;
                                int.TryParse(this.systemSnapShotId.ToString(), out systemSnapShotID);
                                this.LoadSystemSnapShot(systemSnapShotID);

                                if (this.systemSnapShotId != null)
                                {
                                    //// RELOADS THE SNAPSHOTCOMBO WITH A SYSTEMSNAPSHOT PARAMETER TRUE
                                    //// AFTER RELOADING, IT SETS THE SNAPSHOT ITEM TO CODE DEFINED ITEM
                                    this.PopulateQuerySnapShotCombo(true);
                                    this.GetNumberOfRecords(true);
                                    if (this.QuerySnapShotCombo.Items.Count >= 0)
                                    {
                                        this.QuerySnapShotCombo.SelectedValue = 0;
                                        this.SnapShotRecords.Visible = true;
                                        ////this.QueryLayoutCombo.SelectedValue = -1;
                                    }
                                    else
                                    {
                                        this.SnapShotRecords.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        //// THE EXTRA PARAMETER IN THE CONSTRUCTOR IsSystemSnapShotLoaded
                        //// PLAYS THE EXACT ROLE HERE. IT INSERT THE RECORDS AND SEND BACK TO
                        //// PARENTFORM FROM WHERE IT CALLED
                        recordSetXml = eventArgs.Data.SnapShotXML;
                        this.systemSnapShotId = this.form9033Control.WorkItem.F9033_InsertSnapShotItems(TerraScanCommon.UserId, recordSetXml);
                        if (this.systemSnapShotId != -1)
                        {
                            SetSystemSnapShotIdnCount setSystemShotIdnCount;
                            int outSystemSnapShotId;
                            int.TryParse(this.systemSnapShotId.ToString(), out outSystemSnapShotId);
                            setSystemShotIdnCount.SystemSnapShotId = outSystemSnapShotId;
                            setSystemShotIdnCount.SystemSnapShotCount = this.systemSnapShotCount;
                            this.D9030_F9033_SystemSnapshotCompleteEvent(this, new TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount>(setSystemShotIdnCount));
                            //// this.form9033Control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"] = true;
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
        /// Called when [D9030_ F9030_ query engine close at form master].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_QueryEngineCloseAtFormMaster, ThreadOption.UserInterface)]
        public void OnD9030_F9030_QueryEngineCloseAtFormMaster(object sender, TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            SliceReloadActiveRecord reloadActiveRecord;
            reloadActiveRecord.SelectedKeyId = this.currentRowKeyId;
            reloadActiveRecord.MasterFormNo = this.formMasterNo;

            this.AddColumnValuesInTheList();
            this.D9030_F9033_OnFilter_KeyIdReset(this, new DataEventArgs<SliceReloadActiveRecord>(reloadActiveRecord));
        }

        /// <summary>
        /// Called when [D9030_ F9033_ optional parameter key id].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_OptionalParameterKeyId, Thread = ThreadOption.UserInterface)]
        public void OnD9030_F9033_OptionalParameterKeyId(object sender, TerraScan.Infrastructure.Interface.EventArgs<int[]> eventArgs)
        {
            if (this.formMasterNo == eventArgs.Data[1])
            {
                this.openWithKeyId = eventArgs.Data[0];
            }
        }
        #endregion

        #region ReloadQueryAfterColumnUpdate

        /// <summary>
        /// Called when [D9030_ F9033_ reload query after column update].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9033_ReloadQueryAfterColumnUpdate, ThreadOption.UserInterface)]
        public void OnD9030_F9033_ReloadQueryAfterColumnUpdate(object sender, TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            try
            {
                if (eventArgs.Data == this.formMasterNo)
                {
                    this.currentRowKeyId = this.LoadQueryEngine(this.CurrentRowKeyId, "Load");

                    this.AddColumnValuesInTheList();

                    this.ResetLink.Visible = false;
                    this.ClearLink.Visible = false;
                    this.SnapShotRecords.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ReloadQueryAfterColumnUpdate

        #region OnD9030_F9033_ApplyFeeEvent

        ///<Summary>
        ///called when [D9030_F9033_SetSnapshotEvent in Apply Fees]
        /// </Summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_f9033_ApplyFeeEvent, ThreadOption.UserInterface)]
        public void onD9030_f9033_ApplyFeeEvent(object sender, DataEventArgs<int[]> eventArgs)
        {
            try
            {
                if (eventArgs.Data[0] == this.formMasterNo)
                {
                    //string recordSetXml;

                    //// GETS THE SNAPSHOTED KEYIDS FROM THE GRID
                    this.GetSnapShotRecords(true);
                    //int[] tempArgs = new int[1];
                    //tempArgs[0] = this.systemSnapShotCount; 
                    // // GENERATE THE KEYIDS AS AN XML DEPENDING UPON THE RECORDSET TYPE
                    string[] tempArgs = new string[2];
                    tempArgs[0] = this.GetRecordSetType(eventArgs.Data[1]);
                    tempArgs[1] = this.systemSnapShotCount.ToString();
                    // recordSetXml = this.GetRecordSetType(eventArgs.Data[1]);
                    //int recordCount=this.systemSnapShotCount;

                    this.D9030_F9033_GetRecordsetXML(this, new TerraScan.Infrastructure.Interface.EventArgs<string[]>(tempArgs));

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

        #region Public Methods

        #region ReloadQueryEngine

        /// <summary>
        /// Loads the query engine.
        /// </summary>
        /// <param name="keyIndex">Index of the key.</param>
        /// <param name="lastEvent">Form Master Event (Save/Delete)</param>
        /// <returns>Integer</returns>
        public int LoadQueryEngine(int keyIndex, string lastEvent)
        {
            try
            {
                int queryViewId;
                this.QueryEngineGrid.Focus();
                if (this.queryViewTable.Rows.Count > 0)
                {
                    //// ADD/DELETE KEYIDs WITHOUT DB CALL
                    if (!string.IsNullOrEmpty(lastEvent))
                    {
                        bool isDuplicateRecord = false;
                        if (this.systemSnapShotLoaded)
                        {
                            this.systemSnapShotLoaded = false;
                            this.PopulateQuerySnapShotCombo(false);
                            if (this.QuerySnapShotCombo.Items.Count > 0)
                            {
                                this.QuerySnapShotCombo.SelectedIndex = -1;
                                this.SnapShotRecords.Visible = false;
                            }
                        }

                        if (lastEvent.Equals("Save")) ////NEWLY ADDED KEYID HAS ADDED INTO THE QE GRID WITHOUT REFRESH
                        {
                            this.isSaved = true;
                            DataRow keyRow = this.keyIdCollection.NewRow();
                            keyRow["KeyId"] = keyIndex.ToString();
                            if (this.keyIdCollection.Rows.Count > 0)
                            {
                                int limit = this.keyIdCollection.Rows.Count;
                                for (int j = 0; j < limit; j++)
                                {
                                    if (this.keyIdCollection.Rows[j]["KeyId"].ToString() == keyRow["KeyId"].ToString())
                                    {
                                        isDuplicateRecord = true;
                                        break;
                                    }
                                }
                            }

                            if (isDuplicateRecord == false)
                            {
                                this.keyIdCollection.Rows.Add(keyRow);
                            }

                            //string selectQuery = this.QueryUltraDataSource.Band.Columns[0].Key + " = " + keyIndex;
                            string selectQuery = this.QueryUltraDataSource.Band.Columns[this.keyIdColumnName].Key + " = " + keyIndex;
                            DataRow[] selectedRows = this.queryEngineData.Tables[0].Select(selectQuery);
                            if (selectedRows.Length <= 0 && keyIndex > 0)
                            {
                                // used to pass the KeyID after empty Column in Query Engine Data
                                DataRow newRow = this.queryEngineData.Tables[0].NewRow();
                                //newRow[0] = 0;
                                //newRow[1] = keyIndex;
                                newRow[this.keyIdColumnName] = keyIndex;
                                //this.queryEngineData.Tables[0].Rows.Add(keyIndex);
                                this.queryEngineData.Tables[0].Rows.Add(newRow);
                            }
                        }
                        else if (lastEvent.Equals("Delete") && keyIndex > 0) ////DELETED RECORD KEYID HAS REMOVED FROM THE QE GRID WITHOUT REFRESH
                        {
                            if (this.keyIdCollection.Rows.Count > 0)
                            {
                                int limit = this.keyIdCollection.Rows.Count;
                                for (int j = 0; j < limit; j++)
                                {
                                    if (this.keyIdCollection.Rows[j]["KeyId"].ToString() == keyIndex.ToString())
                                    {
                                        this.keyIdCollection.Rows[j].Delete();
                                        break;
                                    }
                                }
                            }

                            //string selectQuery = this.QueryUltraDataSource.Band.Columns[0].Key + " = " + keyIndex;
                            string selectQuery = this.QueryUltraDataSource.Band.Columns[1].Key + " = " + keyIndex;
                            DataRow[] selectedRows = this.queryEngineData.Tables[0].Select(selectQuery);
                            foreach (DataRow eachRow in selectedRows)
                            {
                                this.queryEngineData.Tables[0].Rows.Remove(eachRow);
                            }
                        }
                        else if (lastEvent.Equals("InvalidKey"))
                        {
                            this.queryEngineData.Tables[0].Rows.Clear();
                            this.summaryDetails.Rows.Clear();
                        }
                        else
                        {
                            this.FillDataSet(0, false);
                        }
                    }
                    else
                    {
                        if (this.form9033Control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"] != null)
                        {
                            this.LoadSystemSnapShot(keyIndex);
                        }
                        else
                        {
                            if (this.queryEngineData.Tables.Count > 0)
                            {
                                if (this.queryEngineData.Tables[0].Rows.Count > 0)
                                {
                                    this.queryEngineData.Tables[0].Rows.Clear();
                                }
                                // used to pass the KEYID after empty Column QueryEngine Data 
                                DataRow newRow = this.queryEngineData.Tables[0].NewRow();
                                //newRow[0] = false;
                                //newRow[1] = keyIndex;
                                newRow[this.keyIdColumnName] = keyIndex;
                                this.queryEngineData.Tables[0].Rows.Add(newRow);
                                //  this.queryEngineData.Tables[0].Rows.Add(keyIndex);
                            }
                        }
                    }

                    if (this.isSaved)
                    {
                        this.isSaved = false;
                    }

                    ////Record count for overall records in DB
                    //// this.totalRecordCount = this.queryEngineData.Tables[0].Rows.Count;
                    this.TotalRecords = this.queryEngineData.Tables[0].Rows.Count;

                    /////Used to Update the Navigation panel Records
                    //DataRow[] navigatePanel = this.queryEngineData.Tables[0].Select(this.keyIdColumnName + "=" + keyIndex);
                    //if (navigatePanel.Length > 0)
                    //{
                    //    int rowIndex = this.queryEngineData.Tables[0].Rows.IndexOf(navigatePanel[0]);
                    //    this.NavigationPosition(this, new DataEventArgs<int>(rowIndex + 1));

                    //}
                    this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Both;

                    if (this.queryEngineData.Tables[0].Rows.Count > 0)
                    {
                        this.SetUltraData();

                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        if (this.calculatedColumnDetails.Rows.Count > 0)
                        {
                            this.SetNewColumnValue();
                        }

                        this.SetGridDesignDetails();

                        if (this.filterData != null)
                        {
                            this.SetFilterValue();
                        }
                        else
                        {
                            this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        }

                        if (this.sortData != null)
                        {
                            this.SetSortOrder();
                        }


                        this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        if (this.summaryDetails.Rows.Count > 0)
                        {
                            this.SetSummaryValue();
                        }
                        else
                        {
                            this.SetTempSummaryValue();
                        }

                        this.intialLayout.Flush();
                        this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                        this.defaultXmlString = null;
                        this.defaultXmlString = this.ConvertToXML(this.intialLayout);

                        this.RowSelect(keyIndex, this.keyIdColumnName);
                        this.OnFilterRowSelect();
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.GetNumberOfRecords(false);
                        this.NoOfRecords.Visible = true;
                        this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                        //// To check whether snapshot loaded or not
                        this.snapShotLoaded = false;
                    }
                    else
                    {
                        #region QueryRefresh After Deleting all Records

                        ////afther deleting the last record in grid, the grid is made empty
                        this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.Both;
                        this.SetUltraData();

                        #endregion QueryRefresh After Deleting all Records

                        if (this.filterData != null)
                        {
                            this.SetFilterValue();
                        }
                        else
                        {
                            this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        }

                        if (this.sortData != null)
                        {
                            this.SetSortOrder();
                        }

                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        if (this.calculatedColumnDetails.Rows.Count > 0)
                        {
                            this.SetNewColumnValue();
                        }
                        else
                        {
                            // this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        }

                        this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        if (this.summaryDetails.Rows.Count > 0)
                        {
                            this.SetSummaryValue();
                        }
                        else
                        {
                            this.SetTempSummaryValue();
                        }

                        this.GetNumberOfRecords(false);
                        this.NoOfRecords.Visible = true;
                        this.LayoutFieldLabel.Text = this.layoutName;
                        this.currentRowIndex = -1;
                        this.totalRecords = 0;
                        this.currentRowKeyId = -99;

                        //// To check whether snapshot loaded or not
                        this.snapShotLoaded = false;
                        this.ResetLink.Visible = true;
                    }

                    this.ActiveControl = this.QueryEngineGrid;

                    #region ISSUEFIX : VISIBLITY OF RECORD COUNT IN THE QUERYLOAD

                    this.LayoutRecords.Visible = true;

                    #endregion

                    if (this.QueryEngineGrid.Layouts.Exists("CurrentLayout"))
                    {
                        MemoryStream ms = new MemoryStream();
                        this.QueryEngineGrid.DisplayLayout.SaveAsXml(ms, PropertyCategories.All);
                        string layoutString = this.ConvertToXML(ms);

                        if (this.QueryEngineGrid.Layouts["CurrentLayout"].Equals(layoutString))
                        {
                            this.ResetLink.Visible = false;

                            if (this.QueryLayoutCombo.SelectedIndex == -1)
                            {
                                this.LayoutRecords.Visible = false;
                            }
                            else
                            {
                                this.LayoutRecords.Visible = true;
                            }
                        }
                    }

                    if (this.selectedRowIndex == 0)
                    {
                        int keyValues = 0;
                        if (this.QueryEngineGrid.Rows.Count > 0)
                        {
                            ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                            Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
                            if (filteredRow.Length > 0)
                            {
                                Int32.TryParse(filteredRow[0].Cells[this.keyIdColumnName].Value.ToString(), out keyValues);
                            }

                            this.ResetLink.Visible = this.resetLinkVisible;
                            this.SetCheckBoxPinningLayout();
                            return keyValues;
                        }
                    }
                    else if (this.selectedRowIndex < 0)
                    {
                        this.ResetLink.Visible = this.resetLinkVisible;

                        if (this.queryEngineData.Tables[0].Rows.Count.Equals(0))
                        {
                            this.ResetLink.Visible = true;
                        }

                        this.SetCheckBoxPinningLayout();
                        return this.currentRowKeyId;
                    }
                    else
                    {
                        this.ResetLink.Visible = this.resetLinkVisible;
                        this.SetCheckBoxPinningLayout();
                        return keyIndex;
                    }
                }
                else
                {
                    this.columnNameInvalid = true;
                    this.QueryEngineGrid.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                    this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
                    this.ResetLink.Visible = this.resetLinkVisible;

                    return -1;
                }

                if (this.queryEngineData.Tables[0].Rows.Count.Equals(0))
                {
                    this.ResetLink.Visible = true;
                }


                this.QueryEngineGrid.Focus();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

            return keyIndex;
        }

        #endregion ReloadQueryEngine

        #region LoadSystemSnapShot

        /// <summary>
        /// Loads the system snap shot.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <returns>Current Row KeyId</returns>
        public int ReLoadSystemSnapShot(int systemSnapShotId)
        {
            int keyValues = 0;

            if (this.queryViewTable.Rows.Count > 0)
            {
                this.FillDataSet(systemSnapShotId, true);

                if (this.queryEngineData.Tables[0].Rows.Count > 0)
                {
                    this.QueryEngineGrid.DataSource = null;

                    ////RESET THE DATA IN ULTRA DATASOURCE
                    this.SetUltraData();

                    this.selectedRow = 0;
                    this.ClearLink.Visible = true;

                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                    {
                        DataTable sysSnapShotCountTable = new DataTable();
                        sysSnapShotCountTable = this.queryEngineData.Tables[1];
                        int.TryParse(sysSnapShotCountTable.Rows[0][0].ToString(), out this.snapShotCount);
                    }

                    this.LoadDefaultXMLRecordSet();
                    this.intialLayout.Flush();
                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.defaultXmlString = null;
                    this.defaultXmlString = this.ConvertToXML(this.intialLayout);

                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                    this.OnFilterRowSelect();
                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                    {
                        this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                    }

                    this.GetNumberOfRecords(true);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                    //// To check whether snapshot loaded or not
                    this.snapShotLoaded = true;

                    //// Sets the this.systemSnapShotLoaded, to reload the query when QueryButton Clicked
                    this.systemSnapShotLoaded = true;
                }
                else
                {
                    this.GetNumberOfRecords(true);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = this.layoutName;
                    this.currentRowIndex = -1;
                    this.totalRecords = 0;
                    this.currentRowKeyId = -99;

                    //// To check whether snapshot loaded or not
                    this.snapShotLoaded = true;
                    this.ClearLink.Visible = true;

                    //// Sets the this.systemSnapShotLoaded, to reload the query when QueryButton Clicked
                    this.systemSnapShotLoaded = true;
                }

                if (this.selectedRowIndex == 0)
                {
                    if (this.QueryEngineGrid.Rows.Count > 0)
                    {
                        ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                        Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
                        if (filteredRow.Length > 0)
                        {
                            Int32.TryParse(filteredRow[0].Cells[this.keyIdColumnName].Value.ToString(), out keyValues);
                        }

                        return keyValues;
                    }
                }
                else if (this.selectedRowIndex < 0)
                {
                    return this.currentRowKeyId;
                }
            }
            else
            {
                this.columnNameInvalid = true;
                this.QueryEngineGrid.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
                return -1;
            }

            return this.currentRowKeyId;
        }

        #endregion LoadSystemSnapShot

        #region AdvanceRecordPointer

        /// <summary>
        /// Advances the record pointer.
        /// </summary>
        /// <param name="recordIndex">Index of the record.</param>
        /// <returns>Integer</returns>
        public int AdvanceRecordPointer(int recordIndex)
        {
            ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
            Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetAllNonGroupByRows();

            this.tempRowIndex = row[recordIndex].Index;

            this.QueryEngineGrid.Focus();

            if (this.QueryEngineGrid.Rows.Count > 0)
            {
                this.QueryEngineGrid.Rows[tempRowIndex].Selected = true;
                this.QueryEngineGrid.Rows[tempRowIndex].Activate();
                Int32.TryParse(row[recordIndex].Cells[keyIdColumnName].Value.ToString(), out this.keyValue);
            }

            this.tempKeyValue = this.keyValue;
            this.AddColumnValuesInTheList();
            return this.keyValue;
        }

        #endregion AdvanceRecordPointer

        #region Summary Event

        /// <summary>
        /// Handles the Click event of the F9037OKBUTTON control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9037OKBUTTON_Click(object sender, EventArgs e)
        {
            try
            {
                ////For ok button click the unsaved changes can be applied
                this.F9037SummaryFormUnsaved = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Summaries the check box un checked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SummaryCheckBoxUnChecked(object sender, EventArgs e)
        {
            try
            {
                ////When F9037 form check box event is fired set the form to unsaved
                this.F9037SummaryFormUnsaved = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the SummaryDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        public void SummaryDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!this.F9037SummaryFormUnsaved)
                {
                    this.aggregateFunctionDialogForm.DialogResult = DialogResult.OK;
                }
                else
                {
                    ////When unsaved changes exists and F9037 form cancel button is clicked following message is raised
                    if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ////ok will discard changes and close
                        this.aggregateFunctionDialogForm.DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        ////cancel will not close the form
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpMenuStripItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                HelpEngine.Show(ParentForm.Text, "9037");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        public void HelplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                HelpEngine.Show(ParentForm.Text, "9037");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Summary Event

        #endregion PublicMethod

        #region Protected Method

        #region OnFilter_KeyIdReset

        /// <summary>
        /// Called when [D9030_ F9033_ on filter_ key id reset].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_OnFilter_KeyIdReset(DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9033_OnFilter_KeyIdReset != null)
            {
                this.D9030_F9033_OnFilter_KeyIdReset(this, eventArgs);
            }
        }

        #endregion OnFilter_KeyIdReset

        #region QueryEngineClose

        /// <summary>
        /// Called when [D9030_ F9033_ query engine close].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_QueryEngineClose(TerraScan.Infrastructure.Interface.EventArgs<int> eventArgs)
        {
            if (this.D9030_F9033_QueryEngineClose != null)
            {
                this.D9030_F9033_QueryEngineClose(this, eventArgs);
            }
        }

        #endregion QueryEngineClose

        #region OnD9030_F9033_SystemSnapshotCompleteEvent

        /// <summary>
        /// Called when [D9030_ F9033_ get system snap shot id].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_SystemSnapshotCompleteEvent(TerraScan.Infrastructure.Interface.EventArgs<SetSystemSnapShotIdnCount> eventArgs)
        {
            if (this.D9030_F9033_SystemSnapshotCompleteEvent != null)
            {
                this.D9030_F9033_SystemSnapshotCompleteEvent(this, eventArgs);
            }
        }

        #endregion OnD9030_F9033_SystemSnapshotCompleteEvent

        #region onD9030_F9033_get RecordsetXml

        ///<summary>
        ///called when [D9030_F9033_get RecordsetXml].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9033_GetRecordsetXML(TerraScan.Infrastructure.Interface.EventArgs<string[]> eventArgs)
        {
            if (this.D9030_F9033_GetRecordsetXML != null)
            {
                this.D9030_F9033_GetRecordsetXML(this, eventArgs);
            }
        }

        #endregion


        #endregion

        #region Events

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F9033 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9033_Load(object sender, EventArgs e)
        {
            try
            {
                this.summaryDetails.Columns.AddRange(new DataColumn[] { new DataColumn("SummaryColumn"), new DataColumn("SummaryOperator") });
                this.tempSummaryDetails.Columns.AddRange(new DataColumn[] { new DataColumn("SummaryColumn"), new DataColumn("SummaryOperator") });
                this.calculatedColumnDetails.Columns.AddRange(new DataColumn[] { new DataColumn("CalculatedColumn"), new DataColumn("ColumnFormula"), new DataColumn("ColumnDataType"), new DataColumn("ColumnFormat"), new DataColumn("ColumnCellAppearance"), new DataColumn("IsChecked") });
                this.sortDetails.Columns.AddRange(new DataColumn[] { new DataColumn("SortColumn"), new DataColumn("SortOrder") });
                this.filterDetails.Columns.AddRange(new DataColumn[] { new DataColumn("FilterColumn"), new DataColumn("FilterOperator"), new DataColumn("FilterValue"), new DataColumn("LogicalOperator"), new DataColumn("Expression") });
                this.keyIdCollection.Columns.AddRange(new DataColumn[] { new DataColumn("KeyId") });
                this.optionalkeyIdCollection.Columns.AddRange(new DataColumn[] { new DataColumn("KeyId") });
                this.designDetailsTable.Columns.AddRange(new DataColumn[] { new DataColumn("KeyColumnName"), new DataColumn("VisiblePosition", typeof(Int64)), new DataColumn("IsFixed"), new DataColumn("IsHidden") });
                this.columnNameInvalid = false;
                this.currentRowKeyId = this.keyId;
                this.hasChangeQueryView = false;
                this.AddColumnValuesInTheList();
                this.TempQueryGrid.Visible = false;
                this.HeaderLabel.Visible = false;

                if (F9030.flagQueryLoad)
                {
                    this.queryRefreshed = true;
                }
                else
                {
                    this.queryRefreshed = false;
                }
                this.isFormLoad = true;
                this.LoadQueryViewCombo();

                this.isFormLoad = false;

                ////this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                F9033QueryEngineData.GetDefaultLayoutXMLDataTable defaultXmlTable = new F9033QueryEngineData.GetDefaultLayoutXMLDataTable();
                if (!this.queryViewUnavailable)
                {
                    this.PopulateQueryLayoutCombo();
                    int queryViewId;
                    Int32.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                    this.tempQueryViewId = queryViewId;
                    this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);
                    this.SetUltraData();
                    string defaultLayoutString;
                    Encoding enc = Encoding.ASCII;
                    if (this.queryRefreshed)
                    {
                        defaultXmlTable = this.form9033Control.WorkItem.F9033_GetDefaultLayout(queryViewId);
                    }

                    this.tempQueryData = this.queryEngineData.Tables[0].Clone();
                    this.TempQueryGrid.DataSource = null;
                    this.TempQueryGrid.DataSource = this.tempQueryData;

                    this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                    for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                    {
                        if (this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()))
                        {
                            this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                        }
                        //// this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                    }

                    if (defaultXmlTable.Rows.Count > 0)
                    {
                        defaultLayoutString = defaultXmlTable.Rows[0][defaultXmlTable.LayoutXMLColumn.ColumnName].ToString();
                        this.defaultXmlString = string.Empty;
                        this.defaultXmlString = defaultLayoutString;
                        this.GetLayoutName(this.QueryViewCombo.SelectedIndex);
                        this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                        if (!string.IsNullOrEmpty(defaultLayoutString))
                        {
                            this.defaultLayoutExists = true;

                            //// Gets the String as Byte array
                            Byte[] fileAsByte = enc.GetBytes(defaultLayoutString);

                            //// Converts Byte array to MemoryStream
                            MemoryStream ms = new MemoryStream(fileAsByte);

                            //// Loads the Layout from XML Data
                            this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            Byte[] fileAsByteTemp = enc.GetBytes(defaultLayoutString);

                            //// Converts Byte array to MemoryStream
                            MemoryStream tempStream = new MemoryStream(fileAsByteTemp);

                            this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            if (this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count > 0)
                            {
                                UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                                for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                                {
                                    band.Summaries.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                                }
                            }

                            this.queryLoadFlag = true;
                            this.PopulateQueryLayoutCombo();
                            if (this.QueryLayoutCombo.Items.Count > 0)
                            {
                                this.QueryLayoutCombo.SelectedValue = this.defaultLayoutId;
                            }

                            this.tempLayoutId = this.defaultLayoutId;

                            this.GetLayoutDetails();
                            this.loadedLayout = true;
                        }
                        else
                        {
                            this.GetNewColumnValue();
                        }
                    }
                    else
                    {
                        this.GetNewColumnValue();
                    }

                    if (!this.systemSnapShotLoaded)
                    {
                        this.LayoutStatus(this.intialLayout);

                        if (F9030.flagQueryLoad && this.openWithKeyId.Equals(0))
                        {
                            string maxRecordCount = null;
                            if (!string.IsNullOrEmpty(this.MaxRecordCountTextBox.Text.ToString()))
                            {
                                long maxRecord = 0;
                                long.TryParse(this.MaxRecordCountTextBox.Text.ToString().Replace(",", ""), out maxRecord);
                                maxRecordCount = maxRecord.ToString();
                            }
                            else
                            {
                                maxRecordCount = null;
                            }

                            string keyFilter = TerraScanCommon.GetXmlString(this.keyIdCollection);
                            if (keyFilter.Equals("<Root />"))
                            {
                                keyFilter = null;
                            }

                            this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, "false", maxRecordCount);

                            this.SetUltraData();

                            this.SetLayoutDetails();

                            if (this.queryEngineData.Tables[0].Rows.Count > 0)
                            {
                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.queryViewLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                                this.intialLayout.Flush();
                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                                this.defaultXmlString = null;
                                this.defaultXmlString = this.ConvertToXML(this.intialLayout);

                                this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                                this.OnFilterRowSelect();
                                if (this.queryEngineData.Tables.Count >= 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                                {
                                    this.TotalRecords = this.queryEngineData.Tables[0].Rows.Count;
                                }

                                this.GetNumberOfRecords(false);
                                this.NoOfRecords.Visible = true;
                                this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                                //// To check whether snapshot loaded or not
                                this.snapShotLoaded = false;
                            }
                            else
                            {
                                ////this.NoOfRecords.Text = "0 Rows";
                                this.GetNumberOfRecords(false);
                                this.NoOfRecords.Visible = true;
                                this.LayoutFieldLabel.Text = this.layoutName;
                                this.currentRowIndex = -1;
                                this.totalRecords = 0;
                                this.currentRowKeyId = -99;

                                //// To check whether snapshot loaded or not
                                this.snapShotLoaded = false;
                            }
                        }
                        else
                        {
                            if (!this.keyId.Equals(-99))
                            {
                                if (this.queryEngineData.Tables[0].Rows.Count > 0)
                                {
                                    this.queryEngineData.Tables[0].Rows.Clear();
                                }
                                /// used to pass the KeyID after empty Column in Query Engine Data
                                DataRow newRow = this.queryEngineData.Tables[0].NewRow();
                                // newRow[0] = false;
                                newRow[1] = this.keyId;
                                //this.queryEngineData.Tables[0].Rows.Add(keyIndex);
                                this.queryEngineData.Tables[0].Rows.Add(newRow);
                                //this.queryEngineData.Tables[0].Rows.Add(this.keyId);
                                this.SetUltraData();
                            }
                        }

                        //// Loads the SystemSnapShot and passes the SystemSnapShot flage to false
                        this.PopulateQuerySnapShotCombo(false);
                        this.GetNumberOfRecords(false);
                    }
                    else
                    {
                        this.isFormLoad = true;
                        this.LoadSystemSnapShot(this.keyId);
                        this.SetUltraData();

                        this.isFormLoad = false;
                        this.LayoutStatus(this.intialLayout);

                        //// Loads the SystemSnapShot and passes the SystemSnapShot flage to false
                        this.PopulateQuerySnapShotCombo(true);

                        if (this.QuerySnapShotCombo.Items.Count > 0)
                        {
                            this.QuerySnapShotCombo.SelectedIndex = 0;
                            this.ClearLink.Visible = true;
                            this.SnapShotRecords.Visible = true;
                        }

                        this.GetNumberOfRecords(true);
                    }

                    if (this.queryEngineData.Tables.Count > 0)
                    {
                        this.overAllRecord = this.queryEngineData.Tables[0].Rows.Count;
                        this.totalRecords = this.overAllRecord;
                    }
                    else
                    {
                        this.overAllRecord = 0;
                    }

                    this.summaryFlag = true;
                    this.sortChange = true;
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                    this.ResetLink.Visible = false;

                    if (this.QueryLayoutCombo.SelectedIndex.Equals(-1))
                    {
                        this.LayoutRecords.Visible = false;
                    }
                    else
                    {
                        this.LayoutRecords.Visible = true;
                    }

                    this.columnFound = false;

                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        if (this.QuerySnapShotCombo.GetItemText(this.QuerySnapShotCombo.Items[0]) != "System SnapShot Loaded")
                        {
                            this.QuerySnapShotCombo.SelectedIndex = -1;
                            this.ClearLink.Visible = false;
                            this.SnapShotRecords.Visible = false;
                        }
                        else
                        {
                            if (this.QuerySnapShotCombo.Items.Count >= 0)
                            {
                                this.QuerySnapShotCombo.SelectedIndex = 0;
                                this.ClearLink.Visible = true;
                                this.SnapShotRecords.Visible = true;
                                this.systemSnapShotLoaded = false;
                                this.form9033Control.WorkItem.RootWorkItem.State["9033SystemSnapshotLoaded"] = true;
                            }
                        }
                    }
                    else
                    {
                        this.QuerySnapShotCombo.SelectedIndex = -1;
                        this.ClearLink.Visible = false;
                        this.SnapShotRecords.Visible = false;
                    }

                    this.systemSnapShotLoaded = false;

                    this.LayoutRecords.Visible = false;
                    this.ResetLink.Visible = false;
                    if (this.queryEngineData.Tables.Count > 0 && this.queryEngineData.Tables[0].Rows.Count > 0 && QueryEngineGrid.Rows.Count > 0)
                    {
                        this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                    }

                    if (this.queryEngineData.Tables.Count > 2)
                    {
                        this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                    }

                    this.SetButtonVisibility();
                    this.SetCheckBoxPinningLayout();
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

        #endregion FormLoad

        #region QueryGridEvent

        /// <summary>
        /// Handles the BeforeColumnChooserDisplayed event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeColumnChooserDisplayedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_BeforeColumnChooserDisplayed(object sender, Infragistics.Win.UltraWinGrid.BeforeColumnChooserDisplayedEventArgs e)
        {
            e.Cancel = true;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form fieldManagementForm = new Form();
                ////dummy datagrid is sent which is used to sent the current datasource to the Form f9035
                ////this.ColumnChooserGrid.DataSource = null; 
                this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                    {
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);

                        ////when the remove column count will decrease
                        i = i - 1;
                    }
                }

                ////here newly created column are added and all the column are hidden
                for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    ////here the newly created column are added
                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key))
                    {
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key);
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key;
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Format;
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                        ////if (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key))
                        ////{
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                        ////}
                    }
                    else
                    {
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                    }
                }

                try
                {
                    
                    fieldManagementForm = this.form9033Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9034, null, this.form9033Control.WorkItem);

                    while (fieldManagementForm == null)
                    {
                        fieldManagementForm = this.form9033Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9034, null, this.form9033Control.WorkItem);
                    }
                }
                catch (Exception ex)
                {
                    return;
                }

                if (fieldManagementForm != null)
                {
                    TerraScanCommon.SetValue(fieldManagementForm, "Grid", ColumnChooserGrid);

                    if (fieldManagementForm.ShowDialog() == DialogResult.OK)
                    {
                        this.calculatedColumnDetails.Rows.Clear();

                        ////here newly created column are added and all the column are hidden
                        DataTable calculatedColumnValues = new DataTable("Table");
                        calculatedColumnValues.Columns.AddRange(new DataColumn[] { new DataColumn("NewColumn") });
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            ////here the newly created column are added
                            try
                            {
                                if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Exists(this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key)) //// || !this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                                {
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Add(this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key);
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].DataType = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Formula = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Format = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = !this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout;
                                }
                                else
                                {
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = !this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout;

                                    if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden)
                                    {
                                        string expression = "SummaryColumn = '" + this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key + "'";
                                        this.RemoveSummary(expression);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }

                            if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                            {
                                if (this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout)
                                {
                                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key.Equals(" "))
                                    {
                                        DataRow row = this.calculatedColumnDetails.NewRow();
                                        row["CalculatedColumn"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                        row["ColumnFormula"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                                        row["ColumnDataType"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                                        row["ColumnFormat"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format;
                                        row["ColumnCellAppearance"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                                        row["IsChecked"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout;

                                        this.calculatedColumnDetails.Rows.Add(row);
                                    }
                                }
                                else
                                {
                                    string expression = "SummaryColumn = '" + this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key + "'";
                                    this.RemoveSummary(expression);
                                }
                            }

                            if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound && this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout)
                            {
                                if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key.Equals(" "))
                                {
                                    DataRow row = calculatedColumnValues.NewRow();
                                    row["NewColumn"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                    calculatedColumnValues.Rows.Add(row);
                                }
                            }
                        }

                        if (this.calculatedColumnDetails.Rows.Count > 0)
                        {
                            for (int i = 0; i < this.calculatedColumnDetails.Rows.Count; i++)
                            {
                                char[] c = { '(' };
                                int d = this.formula.IndexOfAny(c);
                                ////formula = formula.Replace('"', ' ');
                                ////                                formula = formula + " as " + calculatedColumnDetails.Rows[i].ItemArray[0].ToString();
                                this.formula = this.FormatSummaryQuery(this.calculatedColumnDetails.Rows[i].ItemArray[1].ToString()) + " as " + this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString();
                                DataRow row = calculatedColumnValues.NewRow();
                                row["NewColumn"] = this.formula;
                                calculatedColumnValues.Rows.Add(row);
                            }
                        }

                        this.calculatedData = TerraScanCommon.GetXmlString(calculatedColumnValues);

                        if (this.calculatedData.Equals("<Root />"))
                        {
                            this.calculatedData = null;
                        }

                        this.columnBind = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        #region Build Formula
        /// <summary>
        /// Formats the summary query.
        /// </summary>
        /// <param name="formulaValue">The formula value.</param>
        /// <returns>Formula String</returns>
        private string FormatSummaryQuery(string formulaValue)
        {
            string formula = formulaValue.Trim();
            //// formula = formula.Replace('"', ' ');
            ////Greater than
            if (formula.Contains(">") || formula.Contains("<") || formula.Contains("<>"))
            {
                formula = "CASE WHEN " + formula + "THEN 'True' ELSE 'FALSE' END ";
            }

            #region And()
            ////And Function
            if (formula.Contains("and("))
            {
                int startIndex = formula.IndexOf("and(") + 3;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("and("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j != formulaArray.Length - 1)
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString() + " and ";
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex - 3, endIndex + 4);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion And()

            #region 0r()
            if (formula.Contains("or("))
            {
                int startIndex = formula.IndexOf("or(") + 2;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("or("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j != formulaArray.Length - 1)
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString() + " or ";
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex - 2, endIndex + 3);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion 0r()

            #region & Operator
            ////& Operator
            if (formula.Contains("&"))
            {
                formula = formula.Replace("&", "+");
            }
            #endregion & Operator

            #region % Operator
            ////Percentage Operator
            if (formula.Contains("%"))
            {
                formula = formula.Replace("%", "/100.00");
            }
            #endregion % Operator

            #region Find()
            ////Find Function
            if (formula.Contains("find("))
            {
                formula = formula.Replace("find(", "charindex(");
                int startIndex = formula.IndexOf("charindex(") + 9;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("charindex("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            if (j <= 1)
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                if (!j.Equals(formulaArray.Length - 1))
                                {
                                    tempFormula = tempFormula + " ,";
                                }
                            }
                            else
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula);
            }

            #endregion Find()

            #region DateDiff()
            ////Date Difference Function
            if (formula.Contains("datediff("))
            {
                int startIndex = formula.IndexOf("datediff(") + 8;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("datediff("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);

                    string[] formulaArray;
                    char[] deliMiter = { ',' };
                    formulaArray = tempFormula.Split(deliMiter);
                    if (formulaArray.Length > 0)
                    {
                        tempFormula = string.Empty;
                        for (int j = 0; j < formulaArray.Length; j++)
                        {
                            ////string test;
                            ////formulaArray[i].ToString().Replace('"', ' ');
                            if (j.Equals(0))
                            {
                                string datePartValue = string.Empty;
                                string stringValue = formulaArray[j].ToString().Trim();
                                if (stringValue.Equals("\"d\"") || stringValue.Equals("d") || stringValue.Equals("'d'")
                                    || stringValue.Equals("\"y\"") || stringValue.Equals("y") || stringValue.Equals("'y'"))
                                {
                                    datePartValue = "day";
                                }
                                else if (stringValue.Equals("\"n\"") || stringValue.Equals("n") || stringValue.Equals("'n'"))
                                {
                                    datePartValue = "minute";
                                }
                                else if (stringValue.Equals("\"h\"") || stringValue.Equals("h") || stringValue.Equals("'h'"))
                                {
                                    datePartValue = "hour";
                                }
                                else if (stringValue.Equals("\"q\"") || stringValue.Equals("q") || stringValue.Equals("'q'"))
                                {
                                    datePartValue = "quarter";
                                }
                                else if (stringValue.Equals("\"s\"") || stringValue.Equals("s") || stringValue.Equals("'s'"))
                                {
                                    datePartValue = "second";
                                }
                                else if (stringValue.Equals("\"w\"") || stringValue.Equals("w") || stringValue.Equals("'w'")
                                         || stringValue.Equals("\"ww\"") || stringValue.Equals("ww") || stringValue.Equals("'ww'"))
                                {
                                    datePartValue = "week";
                                }
                                else if (stringValue.Equals("\"m\"") || stringValue.Equals("m") || stringValue.Equals("'m'"))
                                {
                                    datePartValue = "month";
                                }
                                else if (stringValue.Equals("\"yyyy\"") || stringValue.Equals("yyyy") || stringValue.Equals("'yyyy'"))
                                {
                                    datePartValue = "year";
                                }

                                tempFormula = tempFormula + datePartValue + " , ";
                            }
                            else if (j.Equals(1))
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim() + " , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " , ";
                                }
                            }
                            else
                            {
                                if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                                {
                                    tempFormula = tempFormula + formulaArray[j].ToString().Trim();// +" , ";
                                }
                                else
                                {
                                    tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'";
                                }
                            }
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula);
            }
            #endregion DateDiff()

            #region Congatenate

            if (formula.Contains("concatenate("))
            {
                int startIndex = formula.IndexOf("concatenate(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("concatenate(") + 12, endIndex - 12);
                tempFormula = tempFormula.Replace(',', '+');
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }

            #endregion Congatenate

            #region IsBlank()
            ////IsBlank Function
            if (formula.Contains("isblank("))
            {
                int startIndex = formula.IndexOf("isblank(") + 7;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("isblank("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                    if (formula.Length > (tempFormula.Length + 9))
                    {
                        tempFormula = tempFormula + " , 1";
                    }
                    else
                    {
                        tempFormula = tempFormula + " , 0";
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("isblank(", "dbo.isblankField(");
            }
            #endregion IsBlank()

            #region IsNumber()
            ////IsNumber Function
            if (formula.Contains("isnumber("))
            {
                int startIndex = formula.IndexOf("isnumber(") + 8;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (tempFormula.Contains("isnumber("))
                {
                }
                else
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                    if (formula.Length > (tempFormula.Length + 10))
                    {
                        tempFormula = tempFormula + " , 1";
                    }
                    else
                    {
                        tempFormula = tempFormula + " , 0";
                    }
                }

                string oldValue = formula.Substring(startIndex + 1, endIndex - 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("isnumber(", "dbo.isnumberField(");
            }
            #endregion IsNumber()

            #region Value()
            // For val() function
            if (formula.Contains("value("))
            {
                int startIndex = formula.IndexOf("value(") + 6;
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                if (!tempFormula.Contains("value("))
                {
                    endIndex = tempFormula.IndexOf(")");
                    tempFormula = tempFormula.Substring(1, endIndex - 1);
                }
                string oldValue = formula.Substring(startIndex - 6, endIndex + 7);
                //formula = formula.Replace(oldValue, tempFormula.Trim());
                formula = formula.Replace("value(", "dbo.f9033_udf_RemoveCharValues(");
            }

            #endregion Value()

            #region Now()
            ////Current Date Function
            if (formula.ToUpper().Contains("NOW()"))
            {
                formula = formula.Replace("now()", "getdate()");
            }
            #endregion Now()

            #region Trim()
            ////Trim Function
            if (formula.Contains("trim("))
            {
                int startIndex = formula.IndexOf("trim(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = "ltrim( rtrim( " + tempFormula.Substring(tempFormula.IndexOf("trim(") + 5, endIndex - 5) + " ))";
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }
            #endregion Trim()

            #region True()
            ////True Function
            if (formula.Contains("true()"))
            {
                if (formula.Trim().Length > 6)
                {
                    formula = formula.Replace("true()", "dbo.setBoolean('True', 1)");
                }
                else
                {
                    formula = formula.Replace("true()", "dbo.setBoolean('True', 0)");
                }
            }
            #endregion True()

            #region False()
            ////False Function
            if (formula.Contains("false()"))
            {
                if (formula.Trim().Length > 6)
                {
                    formula = formula.Replace("false()", "dbo.setBoolean('False', 1)");
                }
                else
                {
                    formula = formula.Replace("false()", "dbo.setBoolean('False', 0)");
                }
            }
            #endregion False()

            #region null()
            ////Null() Function
            if (formula.Contains("null("))
            {
                formula = formula.Replace("null()", "dbo.setnull()");
            }
            #endregion null()

            #region Mid()
            ////Mid Function
            if (formula.Contains("mid("))
            {
                int startIndex = formula.IndexOf("mid(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("mid(") + 4, endIndex - 4);
                string[] formulaArray;
                char[] deliMiter = { ',' };
                formulaArray = tempFormula.Split(deliMiter);
                if (formulaArray.Length > 0)
                {
                    tempFormula = string.Empty;
                    for (int j = 0; j < formulaArray.Length; j++)
                    {
                        if (j == 0)
                        {
                            if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                            {
                                tempFormula = tempFormula + "cast(" + formulaArray[j].ToString().Trim() + " AS varchar(max)), ";
                            }
                            else
                            {
                                tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " , ";
                            }
                        }
                        else if (j == 1)
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim() + ",";
                        }
                        else
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                        }
                    }
                }

                tempFormula = "substring(" + tempFormula + ")";
                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }
            #endregion Mid()

            #region Mod()
            if (formula.Contains("mod("))
            {
                int startIndex = formula.IndexOf("mod(");
                int endIndex = 0;
                string tempFormula = formula.Substring(startIndex, formula.Length - startIndex);
                endIndex = tempFormula.IndexOf(")");
                tempFormula = formula.Substring(startIndex, endIndex + 1);
                tempFormula = tempFormula.Substring(tempFormula.IndexOf("mod(") + 4, endIndex - 4);
                string[] formulaArray;
                char[] deliMiter = { ',' };
                formulaArray = tempFormula.Split(deliMiter);
                if (formulaArray.Length > 0)
                {
                    tempFormula = string.Empty;
                    for (int j = 0; j < formulaArray.Length; j++)
                    {
                        if (j == 0)
                        {
                            if (formulaArray[j].ToString().Contains("[") && formulaArray[j].ToString().Contains("]"))
                            {
                                tempFormula = tempFormula + formulaArray[j].ToString().Trim() + " % ";
                            }
                            else
                            {
                                tempFormula = tempFormula + "'" + formulaArray[j].ToString().Trim() + "'" + " % ";
                            }
                        }
                        else
                        {
                            tempFormula = tempFormula + formulaArray[j].ToString().Trim();
                        }
                    }
                }

                string oldValue = formula.Substring(startIndex, endIndex + 1);
                formula = formula.Replace(oldValue, tempFormula.Trim());
            }

            #endregion Mod()

            #region Replace()
            if (formula.Contains("replace("))
            {
                formula = formula.Replace("replace(", "STUFF(");
            }
            #endregion Replace()

            #region Fixed()
            if (formula.Contains("fixed("))
            {
                formula = formula.Replace("fixed(", "round(");
                formula = formula.Replace(" \"true\"", "1").Replace(" \"TRUE\"", "1").Replace(" \"True\"", "1");
                formula = formula.Replace(" \"false\"", "0").Replace(" \"FALSE\"", "0").Replace(" \"False\"", "0");
            }
            #endregion Fixed()

            if (formula.Contains("and(") || formula.Contains("or(") || formula.Contains("find(") || formula.Contains("isblank(") || formula.Contains("isnumber(") || formula.Contains("mid(") || formula.Contains("mod("))
            {
                this.FormatSummaryQuery(formula);
            }

            return formula;
        }
        #endregion Build Formula

        /// <summary>
        /// Handles the AfterSortChange event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BandEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterSortChange(object sender, Infragistics.Win.UltraWinGrid.BandEventArgs e)
        {
            try
            {
                if (this.sortChange && !this.isPinned)
                {
                    this.resetLayout = new MemoryStream();
                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.LayoutStatus(this.resetLayout);
                    this.modifiedLayoutXml = this.ConvertToXML(this.resetLayout);
                    //this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;

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
        /// Handles the AfterRowFixedStateChanged event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.AfterRowFixedStateChangedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterRowFixedStateChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFixedStateChangedEventArgs e)
        {
            try
            {
                this.resetLayout = new MemoryStream();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.LayoutStatus(this.resetLayout);
                this.modifiedLayoutXml = this.ConvertToXML(this.resetLayout);
                //this.QueryEngineGrid.DataSource = this.QueryUltraDataSource;
                ////this.QueryUltraDataSource.Rows.Clear();
                //RefreshRow rs = RefreshRow.ReloadData;

                // this.QueryUltraDataSource.Rows.SetCount(this.queryEngineData.Tables[0].Rows.Count);
                ////this.ApplyLayout(this.modifiedLayoutXml);
                ////this.QueryEngineGrid.Rows.Refresh(rs);
                e.Row.Hidden = false;

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
        /// Handles the SummaryValueChanged event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.SummaryValueChangedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_SummaryValueChanged(object sender, Infragistics.Win.UltraWinGrid.SummaryValueChangedEventArgs e)
        {
            try
            {
                if (!this.queryLoadFlag)
                {
                    ////REMOVE THE DEFAULT SETTING
                    if (this.summaryChanged == false)
                    {
                        for (int sumCount = this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Count - 1; sumCount >= 0; sumCount--)
                        {
                            this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.RemoveAt(sumCount);
                        }
                    }
                }
                else
                {
                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
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
        /// Handles the AfterColPosChanged event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.AfterColPosChangedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterColPosChanged(object sender, Infragistics.Win.UltraWinGrid.AfterColPosChangedEventArgs e)
        {
            try
            {
                this.resetLayout = new MemoryStream();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.LayoutStatus(this.resetLayout);
                this.modifiedLayoutXml = this.ConvertToXML(this.resetLayout);

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
        /// Handles the InitializeLayout event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                ////Turn Off Sorting functionality
                e.Layout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
                ////Turn Off Filter functionality
                e.Layout.Override.RowFilterAction = RowFilterAction.AppearancesOnly;

                //// Turn on Copy functionality. 
                e.Layout.Override.AllowMultiCellOperations = AllowMultiCellOperation.Copy;

                //// In order to copy, the user needs to select cells or rows. 
                //// So set CellClickAction so that clicking on a cell selects that cell
                //// instead of going into edit mode.


                //e.Layout.Override.CellClickAction = CellClickAction.CellSelect;

                e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                e.Layout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButtonFixedSize;

                for (int columnCount = 0; columnCount < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; columnCount++)
                {
                    if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[columnCount].DataType.Name == "Decimal")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[columnCount].MaskInput = "nn,nnn,nnn.nn";
                    }

                    if (columnCount.Equals(0))
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellClickAction = CellClickAction.Edit;

                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[columnCount].CellClickAction = CellClickAction.CellSelect;
                    }
                }

                //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellClickAction = CellClickAction.Edit;
                //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.AllowEdit;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowSummaries = AllowRowSummaries.True;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].SortIndicator = SortIndicator.None;
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Width = 34;
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

        private void QueryEngineGrid_InitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e)
        {
            try
            {
                //if (e.Row.Index >= 0 && e.Row.Cells.Count > 0 && e.Row.Cells[0].Value.Equals(true))
                //{
                //    if ((e.Row.Index % 2) == 0)
                //    {
                //        e.Row.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
                //    }
                //    else
                //    {
                //        e.Row.Appearance.BackColor = System.Drawing.Color.LightBlue;
                //    }
                //}
                /*  //// Allow to edit checkbox column
                  ////if (e.Row.Cells[0].Equals(0))
                  ////{
                 if (e.Row != null && e.Row.Cells.Count > 1 && e.Row.Index >= 0)
                  {
                      for (int columnCount = 0; columnCount < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; columnCount++)
                      {
                          if (columnCount.Equals(0))
                          {
                              e.Row.Cells[0].Activation = Activation.AllowEdit;
                          }
                          else
                          {
                              e.Row.Cells[columnCount].Activation = Activation.NoEdit; 
                          }
                      }
                      this.QueryEngineGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.Default;
                      //e.Row.Cells[1].Activation = Activation.NoEdit;
                      //e.Row.Cells[0].Activation = Activation.AllowEdit;

                      //this.QueryEngineGrid.Rows[e.Row.Index].Cells[0].Activate();
                      //this.QueryEngineGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);               
                  }

                  ////    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellClickAction = CellClickAction.Edit;
                  ////    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.AllowEdit;
                  ////}

                  if (e.Row != null && e.Row.Index.Equals(-1))
                  {
                      if (this.QueryEngineGrid.DisplayLayout != null)
                      {
                          //this.QueryEngineGrid.DisplayLayout.Override.FixedRowIndicator = FixedRowIndicator.None;
                          this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
                          this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowSummaries = AllowRowSummaries.False;
                          this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
                          this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
                          //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].SortIndicator = SortIndicator.None;
                          this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.VisiblePosition = 0;
                          this.QueryEngineGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
                          //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellClickAction = CellClickAction.Edit;
                          //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.AllowEdit;
                          //e.Row.Cells[0].Column.CellActivation = Activation.AllowEdit;
                          //e.Row.Cells[0].Column.CellClickAction = CellClickAction.Edit;
                          //e.Row.Cells[0].Activation = Activation.AllowEdit;
                          //e.Row.Activation = Activation.AllowEdit;
                          this.QueryEngineGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.Default;
                      }
                  }*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeRowActivate event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.RowEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_BeforeRowActivate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e)
        {
            try
            {
                if (e.Row.Index >= 0)
                {
                    bool find = false;
                    this.selectedRow = e.Row.Index;
                    if (this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
                    {
                        string key = this.QueryEngineGrid.Rows[this.selectedRow].Cells[this.keyIdColumnName].Value.ToString();
                        ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                        Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetAllNonGroupByRows();
                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow r in row)
                        {
                            if (key == r.Cells[this.keyIdColumnName].Value.ToString())
                            {
                                this.currentRowIndex = r.VisibleIndex - 1;
                                find = true;
                                break;
                            }
                            else
                            {
                                find = false;
                            }

                        }

                        if (e.Row != null && e.Row.Cells.Count > 1 && e.Row.Index >= 0)
                        {
                            for (int columnCount = 0; columnCount < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; columnCount++)
                            {
                                if (columnCount.Equals(0))
                                {
                                    e.Row.Cells[0].Column.CellClickAction = CellClickAction.Edit;
                                    e.Row.Cells[0].Activation = Activation.AllowEdit;
                                }
                                else
                                {
                                    e.Row.Cells[columnCount].Activation = Activation.NoEdit;
                                }
                            }
                            this.QueryEngineGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.Default;
                            //this.QueryEngineGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
                            //e.Row.Cells[1].Activation = Activation.NoEdit;
                            //e.Row.Cells[0].Activation = Activation.AllowEdit;

                            //this.QueryEngineGrid.Rows[e.Row.Index].Cells[0].Activate();
                            //this.QueryEngineGrid.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);               
                        }
                        if (!find)
                        {
                            this.currentRowIndex = 0;
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
        /// Handles the AfterRowActivate event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryEngineGrid.ActiveRow.Index >= 0)
                {
                    if (this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
                    {
                        this.currentRowKeyId = Convert.ToInt32(this.QueryEngineGrid.ActiveRow.Cells[this.keyIdColumnName].Value);

                        this.AddColumnValuesInTheList();
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
        /// Handles the PropertyChanged event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_PropertyChanged(object sender, Infragistics.Win.PropertyChangedEventArgs e)
        {
            try
            {
                if (!this.columnFound)
                {
                    bool flagAllHidde = false;
                    for (int rowIndex = 0; rowIndex < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; rowIndex++)
                    {
                        if (!flagAllHidde)
                        {
                            if (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[rowIndex].Hidden)
                            {
                                flagAllHidde = true;
                            }
                        }
                    }

                    if (!flagAllHidde)
                    {
                        this.LayoutpictureBox.Enabled = false;
                    }
                    else
                    {
                        this.LayoutpictureBox.Enabled = true;
                    }

                    this.columnFound = false;
                }

                if (this.summaryFlag)
                {
                    this.LayoutStatus(this.resetLayout);

                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (OutOfMemoryException)
            {
                this.LayoutStatus(this.resetLayout);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeSummaryDialog event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeSummaryDialogEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_BeforeSummaryDialog(object sender, Infragistics.Win.UltraWinGrid.BeforeSummaryDialogEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                e.Cancel = false;
                this.aggregateFunctionDialogForm = e.SummaryDialog;
                e.SummaryDialog.Text = "Summaries";
                e.SummaryDialog.Width = e.SummaryDialog.Width;
                e.SummaryDialog.Height = e.SummaryDialog.Height + 30;
                #region Summary Dialog Chnages for version 11.1 Infragistics
                e.SummaryDialog.BackColor = Color.White;
                e.SummaryDialog.Left = 100;
                e.SummaryDialog.AllowTransparency = true;
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F9040));
                e.SummaryDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                Label formNo = new Label();
                Panel linePanel = new Panel();
                LinkLabel helpLinkLabel = new LinkLabel();
                TerraScan.UI.Controls.TerraScanButton F9037OKBUTTON = new TerraScan.UI.Controls.TerraScanButton();
                TerraScan.UI.Controls.TerraScanButton F9037CancelBUTTON = new TerraScan.UI.Controls.TerraScanButton();
                MenuStrip F9037HelpMenuStrip = new System.Windows.Forms.MenuStrip();
                ToolStripMenuItem HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

                ////Help link label for F9037 form
                helpLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                helpLinkLabel.AutoSize = true;
                helpLinkLabel.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                helpLinkLabel.Location = new Point(e.SummaryDialog.Width - 150, 185);
                helpLinkLabel.Name = "HelplinkLabel";
                helpLinkLabel.Size = new System.Drawing.Size(32, 15);
                helpLinkLabel.TabIndex = 125;
                helpLinkLabel.TabStop = true;
                helpLinkLabel.Text = "Help";
                helpLinkLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
                helpLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.HelplinkLabel_LinkClicked);

                ////Menu strip F9037HelpMenuStrip
                F9037HelpMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { HelpToolStripMenuItem });
                F9037HelpMenuStrip.Location = new System.Drawing.Point(0, 0);
                F9037HelpMenuStrip.Name = "F9037HelpMenuStrip";
                F9037HelpMenuStrip.Size = new System.Drawing.Size(600, 24);
                F9037HelpMenuStrip.TabIndex = 206;
                F9037HelpMenuStrip.Text = "F9037HelpMenuStrip";
                F9037HelpMenuStrip.Visible = false;

                ////HelpToolStripMenuItem click event
                HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
                HelpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
                HelpToolStripMenuItem.Size = new System.Drawing.Size(133, 20);
                HelpToolStripMenuItem.Text = "HelpToolStripMenuItem";
                HelpToolStripMenuItem.Visible = false;
                HelpToolStripMenuItem.Click += new System.EventHandler(this.HelpMenuStripItem_Clicked);

                ////Ok button for F9037
                F9037OKBUTTON.ActualPermission = true;
                F9037OKBUTTON.ApplyDisableBehaviour = false;

                F9037OKBUTTON.AutoSize = true;
                F9037OKBUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
                F9037OKBUTTON.BorderColor = System.Drawing.Color.Wheat;
                F9037OKBUTTON.CommentPriority = false;
                F9037OKBUTTON.DialogResult = System.Windows.Forms.DialogResult.OK;
                F9037OKBUTTON.EnableAutoPrint = false;
                F9037OKBUTTON.FilterStatus = false;
                F9037OKBUTTON.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                F9037OKBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                F9037OKBUTTON.FocusRectangleEnabled = true;
                F9037OKBUTTON.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                F9037OKBUTTON.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                F9037OKBUTTON.ImageSelected = false;
                F9037OKBUTTON.Location = new System.Drawing.Point(75, 140);
                F9037OKBUTTON.Name = "F9037OKBUTTON";
                F9037OKBUTTON.NewPadding = 5;
                F9037OKBUTTON.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
                F9037OKBUTTON.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
                F9037OKBUTTON.Size = new System.Drawing.Size(80, 30);
                F9037OKBUTTON.StatusIndicator = false;
                F9037OKBUTTON.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                F9037OKBUTTON.StatusOffText = null;
                F9037OKBUTTON.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
                F9037OKBUTTON.StatusOnText = null;
                F9037OKBUTTON.TabIndex = 7;
                F9037OKBUTTON.TabStop = false;
                F9037OKBUTTON.Text = "OK";
                F9037OKBUTTON.UseVisualStyleBackColor = false;
                F9037OKBUTTON.Visible = true;
                F9037OKBUTTON.Click += new System.EventHandler(this.F9037OKBUTTON_Click);

                /////Cancel Button for F9037 form
                F9037CancelBUTTON.ActualPermission = false;
                F9037CancelBUTTON.ApplyDisableBehaviour = false;

                F9037CancelBUTTON.AutoSize = true;
                F9037CancelBUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(81)))), ((int)(((byte)(128)))));
                F9037CancelBUTTON.BorderColor = System.Drawing.Color.Wheat;
                F9037CancelBUTTON.CommentPriority = false;
                F9037CancelBUTTON.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                F9037CancelBUTTON.EnableAutoPrint = false;
                F9037CancelBUTTON.FilterStatus = false;
                F9037CancelBUTTON.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                F9037CancelBUTTON.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                F9037CancelBUTTON.FocusRectangleEnabled = true;
                F9037CancelBUTTON.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                F9037CancelBUTTON.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                F9037CancelBUTTON.ImageSelected = false;
                F9037CancelBUTTON.Location = new System.Drawing.Point(160, 140);
                F9037CancelBUTTON.Name = "FundCancelButton";
                F9037CancelBUTTON.NewPadding = 5;
                F9037CancelBUTTON.SetActionType = TerraScan.UI.Controls.TerraScanButton.ActionType.New;
                F9037CancelBUTTON.SetButtonType = TerraScan.UI.Controls.TerraScanButton.ButtonType.CommandButton;
                F9037CancelBUTTON.Size = new System.Drawing.Size(80, 30);
                F9037CancelBUTTON.StatusIndicator = false;
                F9037CancelBUTTON.StatusOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                F9037CancelBUTTON.StatusOffText = null;
                F9037CancelBUTTON.StatusOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(133)))), ((int)(((byte)(85)))));
                F9037CancelBUTTON.StatusOnText = null;
                F9037CancelBUTTON.TabIndex = 10;
                F9037CancelBUTTON.TabStop = false;
                F9037CancelBUTTON.Text = "Cancel";
                F9037CancelBUTTON.UseVisualStyleBackColor = false;
                F9037CancelBUTTON.Visible = true;

                ////to set the enter key for OK button
                e.SummaryDialog.AcceptButton = F9037OKBUTTON;
                ////to set esc key for cancel button
                e.SummaryDialog.CancelButton = F9037CancelBUTTON;

                ////Line panel in F9037 form
                linePanel.Location = new Point(5, 180);
                linePanel.Size = new Size(238, 2);
                linePanel.BackColor = Color.FromArgb(50, 68, 108);

                ////form no label in F9037 form
                formNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                formNo.AutoSize = true;
                formNo.BackColor = System.Drawing.Color.White;
                formNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                formNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
                formNo.Text = "9037";
                formNo.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                formNo.Location = new Point(5, 185);
                e.SummaryDialog.Controls.Add(formNo);
                e.SummaryDialog.Controls.Add(linePanel);
                e.SummaryDialog.Controls.Add(helpLinkLabel);
                e.SummaryDialog.Controls.Add(F9037OKBUTTON);
                e.SummaryDialog.Controls.Add(F9037CancelBUTTON);
                e.SummaryDialog.Controls.Add(F9037HelpMenuStrip);
                #endregion
                foreach (Control currentControl in e.SummaryDialog.Controls[0].Controls)
                {
                    if (currentControl is CheckBox)
                    {
                        currentControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        currentControl.Click += new System.EventHandler(this.SummaryCheckBoxUnChecked);
                        if (e.Column.DataType.Equals(typeof(System.Boolean)))
                        {
                            if (currentControl.Text.Equals("Maximum") || currentControl.Text.Equals("Minimum"))
                            {
                                currentControl.Visible = true;
                                currentControl.Enabled = false;
                                currentControl.Enabled = false;
                                currentControl.PerformLayout();
                                currentControl.PerformLayout(currentControl, "Enabled");
                            }
                        }
                    }
                }

                e.SummaryDialog.FormClosing += new FormClosingEventHandler(this.SummaryDialog_FormClosing);
                formNo.BringToFront();
                linePanel.BringToFront();
                helpLinkLabel.BringToFront();
                F9037OKBUTTON.BringToFront();
                F9037CancelBUTTON.BringToFront();
                e.SummaryDialog.StartPosition = FormStartPosition.CenterScreen;
                ////e.SummaryDialog.Activate();

                ////if (e.Column.DataType.Equals(typeof(System.Boolean)))
                ////{
                ////    e.SummaryDialog.Controls[5].Controls[2].Enabled = false;
                ////    e.SummaryDialog.Controls[5].Controls[3].Enabled = false;
                ////    e.SummaryDialog.Controls[5].Controls[2].Visible = false;
                ////    e.SummaryDialog.Controls[5].Controls[3].Visible = true;

                ////}
                ////on form lload there will not be any unsaved changes
                this.F9037SummaryFormUnsaved = false;
                this.queryLoadFlag = false;

                if (this.tempSummaryDetails.Rows.Count > 0)
                {
                    DataTable ds = this.tempSummaryDetails;
                    DataRow[] foundRows;
                    string expression = string.Concat("SummaryColumn = '", e.Column.ToString(), "'");
                    foundRows = ds.Select(expression);
                    if (foundRows.Length > 0)
                    {
                        this.SetTempSummaryValue();
                    }
                }

                this.summaryChanged = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the AfterPerformAction event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterUltraGridPerformActionEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterPerformAction(object sender, AfterUltraGridPerformActionEventArgs e)
        {
            try
            {
                this.PerformCopyAction(UltraGridAction.Copy);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the AfterSelectChange event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            try
            {
                this.PerformCopyAction(UltraGridAction.Copy);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeRowFilterDropDownPopulate event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_BeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e)
        {
            try
            {

                int index = e.ValueList.FindString("(Blanks)");
                //if (e.Column.DataType == System.Boolean)
                //{
                string a = (e.Column.DataType).ToString();
                if (a == "System.Boolean")
                {
                    if (index > -1)
                        e.ValueList.ValueListItems.RemoveAt(index);
                }
                //}
                index = e.ValueList.FindString("(NonBlanks)");
                //if (index > -1)
                //    e.ValueList.ValueListItems.RemoveAt(index);

                e.Handled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion QueryGridEvent

        #region ComboEvent

        #region QueryViewCombo

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the QueryViewCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryViewCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.isFormLoad)
                {
                    this.QueryEngineGrid.DataSource = null;
                    int selectedLayoutValue = 0;

                    if (this.QueryLayoutCombo.SelectedValue != null)
                    {
                        int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                    }

                    this.PopulateQueryLayoutCombo();

                    bool findLayoutItems = this.FindLayoutItems(selectedLayoutValue);

                    if (this.defaultLayoutExists)
                    {
                        this.summaryData = null;
                        this.summaryDetails.Rows.Clear();
                        this.tempSummaryDetails.Rows.Clear();

                        if (findLayoutItems)
                        {
                            if (this.QueryLayoutCombo.SelectedIndex < 0)
                            {
                                this.QueryLayoutCombo.SelectedValue = selectedLayoutValue;
                                this.ApplyLayout();

                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);

                                this.LayoutRecords.Visible = true;
                                this.ResetLink.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (this.QueryLayoutCombo.SelectedIndex <= 0)
                        {
                            this.LayoutRecords.Visible = false;
                            this.ResetLink.Visible = false;
                            this.summaryData = null;
                            this.summaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Rows.Clear();
                            this.filterData = null;
                            this.sortData = null;
                            this.calculatedData = null;
                        }
                    }

                    int selectedSnapShotValue;
                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        if (this.QuerySnapShotCombo.SelectedValue != null)
                        {
                            this.QuerySnapShotCombo.SelectedIndex = -1;
                        }

                        this.SnapShotRecords.Visible = false;

                        this.hasChangeQueryView = true;
                        //this.LoadQueryEngine();
                        this.FillDataSet(0, false);
                        this.hasChangeQueryView = false;
                        this.SetUltraData();
                        if (this.QueryLayoutCombo.SelectedValue != null && this.isFormLoad.Equals(false)) ////Have to check
                        {
                            this.ApplyLayout();
                            if (this.queryEngineData.Tables.Count > 0 && this.queryEngineData.Tables[0].Rows.Count > 0)
                            {
                                this.SetSummaryValue();
                            }
                        }

                        this.overAllRecord = this.queryEngineData.Tables[0].Rows.Count;
                        this.GetNumberOfRecords(false);
                        if (this.QueryLayoutCombo.SelectedValue != null)
                        {
                            this.LayoutRecords.Visible = true;
                            this.ResetLink.Visible = true;
                        }
                        else
                        {
                            this.LayoutRecords.Visible = false;
                            this.ResetLink.Visible = false;
                        }
                    }
                    else
                    {
                        this.SnapShotRecords.Visible = false;

                        this.queryViewName = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryViewColumn.ColumnName].ToString();
                        if (this.QueryLayoutCombo.SelectedValue != null)
                        {
                            this.ApplyLayout();
                        }

                        this.hasChangeQueryView = true;

                        //this.LoadQueryEngine();
                        this.FillDataSet(0, false);
                        this.hasChangeQueryView = false;
                        this.SetUltraData();
                        this.overAllRecord = this.queryEngineData.Tables[0].Rows.Count;
                        this.GetNumberOfRecords(false);
                        if (this.QueryLayoutCombo.SelectedValue != null)
                        {
                            this.LayoutRecords.Visible = true;
                            this.ResetLink.Visible = true;
                        }
                        else
                        {
                            this.LayoutRecords.Visible = false;
                            this.ResetLink.Visible = false;
                        }
                    }

                    this.ClearLink.Visible = false;
                    this.ResetLink.Visible = false;

                    if (this.QueryLayoutCombo.SelectedIndex == -1)
                    {
                        this.LayoutRecords.Visible = false;
                    }
                    else
                    {
                        this.LayoutRecords.Visible = true;
                    }

                    if (this.queryEngineData.Tables.Count > 2)
                    {
                        this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                    }

                    this.SetNewColumnValue();
                    if (this.queryEngineData.Tables.Count > 0)
                    {
                        this.tempQueryData = this.queryEngineData.Tables[0].Clone();
                        this.TempQueryGrid.DataSource = null;
                        this.TempQueryGrid.DataSource = this.tempQueryData;

                        this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                        for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                            {
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);

                                ////when the remove column count will decrease
                                i = i - 1;
                            }
                        }

                        ////here newly created column are added and all the column are hidden
                        for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            ////here the newly created column are added
                            if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key))
                            {
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key);
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Format;
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                            }

                            this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                        }

                        this.GetNewColumnValue();
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

        #endregion QueryViewCombo

        #region QueryLayoutCombo

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the QueryLayoutCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryLayoutCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ////CODE COMMENTED TO DISABLE SELECTION CHANGE EVENT

                /*  int snapShotId = 0;
                  int queryViewId = 0;

                  this.selectedLayoutIndex = this.QueryLayoutCombo.SelectedIndex;
                  if (this.listQueryLayout.Rows.Count > 0)
                  {
                      if (this.QuerySnapShotCombo.SelectedIndex != -1)
                      {
                          if (this.QuerySnapShotCombo.Items.Count > 0)
                          {
                              int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out snapShotId);
                          }

                          if (this.QueryViewCombo.Items.Count > 0)
                          {
                              if (this.QueryViewCombo.SelectedIndex != -1)
                              {
                                  int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                              }
                          }

                          ////Added by Latha
                          ////Remove previous summary values
                          this.summaryDetails.Rows.Clear();
                          ////Remove previous user defined column values
                          this.calculatedColumnDetails.Rows.Clear();
                          ////Ends here

                          this.ApplyLayout();
                          this.LoadSnapShot(snapShotId, queryViewId);
                        
                          this.GetNumberOfRecords(false);
                          if (this.calculatedData != null)
                          {
                              this.SetNewColumnValue();
                          }
                          this.SetSummaryValue();
                          //this.SetSummaryValue();


                          this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);
                      }
                      else
                      {
                          ////Added by Latha
                          ////Remove previous summary values
                          this.summaryDetails.Rows.Clear();
                          ////Ends here
                          this.ApplyLayout();
                          this.LoadQueryEngine();
                          //this.ApplyLayout();
                          if (this.calculatedData != null)
                          {
                              this.SetNewColumnValue();
                          }
                          /*-------test ---------*/
                ////this.SetFilterValue();

                /*  this.SetSummaryValue();
                  this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);
              }
              // changed by suganth from false to true // this.PopulateQuerySnapShotCombo(false);                   

              if (this.QueryLayoutCombo.Items.Count > 0)
              {
                  if (this.QueryLayoutCombo.SelectedIndex >= 0)
                  {
                      //this.QuerySnapShotCombo.SelectedIndex = -1; // changed by suganth
                      // this.LoadQueryEngine(); Commetted For Issue Check
                      if (this.QuerySnapShotCombo.SelectedIndex != -1)
                      {
                          this.SnapShotRecords.Visible = true; // changed by suganth
                      }
                      else
                      {
                          this.SnapShotRecords.Visible = false;
                      }

                      this.LayoutRecords.Visible = true;
                  }
              }
          }
         // this.ApplyLayout();
          this.ResetLink.Visible = false;*/
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion QueryLayoutCombo

        #region Query SnapShot Combo

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the QuerySnapShotCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QuerySnapShotCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                ////CODE COMMENTED TO DISABLE SELECTION CHANGE EVENT

                /* int snapShotId;
                 int queryViewId = 0;
                 this.selectedLayoutXml = string.Empty;

                 if (this.QuerySnapShotCombo.Items.Count > 0)
                 {
                     this.ClearLink.Visible = true;
                     int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out snapShotId);
                     if (this.QueryViewCombo.Items.Count > 0)
                     {
                         int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                         // change made by suganth // this.QueryViewCombo.SelectedValue = queryViewId; 
                     }

                     this.PopulateQuerySnapShotCombo(false);
                     bool snapShotItemFound = this.FindSnapShotItems(snapShotId);
                     if (snapShotItemFound)
                     {
                         this.QuerySnapShotCombo.SelectedValue = snapShotId;
                     }
                     else
                     {
                         int selectedLayoutValue = 0;
                         this.QuerySnapShotCombo.SelectedIndex = -1;
                         this.SnapShotRecords.Visible = false;

                         this.QueryEngineGrid.DataSource = null;

                         this.FillDataSet(0, false);
                         ////Added by Latha
                         ////Repopulate the UltraSource
                         //this.QueryEngineGrid.DataSource = this.this.queryEngineData.Tables[0];
                         this.resetUltraData();

                         ////Ends here

                         if (this.QueryLayoutCombo.SelectedValue != null)
                         {
                             int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                         }

                         if (selectedLayoutValue != 0)
                         {
                             bool findLayoutItem = this.FindLayoutItems(selectedLayoutValue);

                             if (findLayoutItem)
                             {
                                 this.QueryLayoutCombo.SelectedValue = selectedLayoutValue;
                                 this.ApplyLayout();

                                 this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);
                             }
                             else
                             {
                                 this.QueryLayoutCombo.SelectedIndex = -1;
                                 this.LayoutRecords.Visible = false;
                                 this.ResetLink.Visible = false;
                             }
                         }

                         this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                         this.OnFilterRowSelect();
                         if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                         {
                             this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                         }
                         //this.totalRecords = Convert.ToInt32(this.QueryEngineGrid.Rows.FilteredInRowCount.ToString());
                         //this.NoOfRecords.Text = this.totalRecords.ToString(this.textFormat) + " Rows";
                         if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 0)
                         {
                             this.NoOfRecords.Text = this.queryEngineData.Tables[1].Rows[3][0].ToString() + " Rows"; ;
                         }
                         this.GetNumberOfRecords(false);
                         this.NoOfRecords.Visible = true;
                         // this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                         // To check whether snapshot loaded or not
                         this.snapShotLoaded = false;
                     }

                     bool resetLinkVisiblity = false;

                     if (snapShotId > 0)
                     {
                         resetLinkVisiblity = this.ResetLink.Visible;
                         this.LoadSnapShot(snapShotId, queryViewId);

                         ////Added by Latha
                         this.SetSummaryValue();
                         ////Ends here

                         this.GetNumberOfRecords(false);
                         //this.LayoutFieldLabel.Text = string.Empty;
                         this.layoutName = this.queryViewTable.Rows[0][this.queryViewTable.LayoutNameColumn.ColumnName].ToString();
                         this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                         this.SnapShotRecords.Visible = true;

                         // Issue : To Remain the Modified Layout
                         // this.ApplyLayout();
                         this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);

                     }

                     this.ResetLink.Visible = resetLinkVisiblity;
                     //this.LayoutRecords.Visible = resetLinkVisiblity;                       

                     if (this.QueryLayoutCombo.SelectedIndex != -1)
                     {
                         this.LayoutRecords.Visible = true;
                     }
                     else
                     {
                         this.LayoutRecords.Visible = resetLinkVisiblity;
                     }
                 }
                 else
                 {
                     this.ClearLink.Visible = false;
                     this.SnapShotRecords.Visible = false;
                 } */
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

        #endregion Query SnapShot Combo

        #endregion ComboEvent

        #region PreviewDialogLoad

        /// <summary>
        /// Handles the Load event of the QueryResultPreviewDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryResultPreviewDialog_Load(object sender, EventArgs e)
        {
            try
            {
                //// Loads the Document Name
                this.QueryResultPreviewDialog.Document.DocumentName = "Preview";
                this.QueryResultPreviewDialog.Text = "Preview";
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion PreviewDialogLoad

        #region ButtonClick Event

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryEngineGrid.Rows.Count > 0)
                {
                    this.QueryResultPreviewDialog.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ExcelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExcelButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryEngineGrid.Rows.Count > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string strMyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    //// Sets the Current Folder path to MyDocuments\TerraScan
                    DirectoryInfo dirInfo = new DirectoryInfo(strMyDocuments + @"\TerraScan");
                    int fileIndex = 1;
                    string extension = ".XLSX";
                    string filename = "TS" + this.formMasterNo + "_" + fileIndex + extension;

                    //// Checks Whether TerraScan Folder Exists in the MyDocument Folder
                    if (!dirInfo.Exists)
                    {
                        dirInfo.Create();
                    }

                    //// Gets the set of files available in the current directory
                    FileInfo[] fileArray = dirInfo.GetFiles();
                    filename = filename.ToUpper();
                    if (fileArray.Length != 0)
                    {
                        ArrayList filenames = new ArrayList();
                        for (int i = 0; i < fileArray.Length; i++)
                        {
                            //// if a file is an xml file, add it to the arraylist
                            if (fileArray[i].Name.EndsWith(extension))
                            {
                                filenames.Add(fileArray[i].Name);
                            }
                        }

                        //// Iterates whether the given filename exists in the current folder
                        for (int j = 0; j < filenames.Count; j++)
                        {
                            if (filenames.Contains(filename))
                            {
                                //// If Exists, change the filename
                                fileIndex++;
                                filename = "TS" + this.formMasterNo + "_" + fileIndex + extension;
                                filename = filename.ToUpper();
                            }
                        }
                    }

                    filename = dirInfo.ToString() + "\\" + filename;

                    #region #21718 TSBG - D9030.F9033 Query Engine form - To Excel button bug
                    // ExportFormulas set false to export data instead of formula
                    this.ExcelExporter.ExportFormulas = false;
                    #endregion

                    RegistryKey regClasses = Registry.ClassesRoot;
                    //// Check whether Microsoft Excel is installed on this computer, 
                    //// by searching the HKEY_CLASSES_ROOT\Excel.Application key. 
                    RegistryKey regExcel = regClasses.OpenSubKey("Excel.Application");
                    if (regExcel != null)
                    {
                        Workbook objExcelWorkBook = new Workbook(); //Create a new Workbook in memory
                        ////Work on the first sheet:
                        objExcelWorkBook.Worksheets.Add("Sheet1"); //Create A Worksheet
                        objExcelWorkBook.ActiveWorksheet = objExcelWorkBook.Worksheets["Sheet1"];  //Then Make It Active
                        objExcelWorkBook.ActiveWorksheet.Columns[0].Hidden = true;



                        objExcelWorkBook.SetCurrentFormat(WorkbookFormat.Excel2007);
                        this.ExcelExporter.Export(this.QueryEngineGrid, objExcelWorkBook);
                        //If you want to have the very first sheet active WHEN you open the excel file, we need
                        //the following line of code:
                        objExcelWorkBook.Save(filename);
                        //// Opens the Grid Details in the SpreadSheet
                        Process excel = new Process();
                        excel.StartInfo.Arguments = "\"" + dirInfo.ToString() + "\" /e";
                        excel.StartInfo.FileName = filename;
                        excel.Start();
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("ExcelNotFound"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (IOException ex)
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
        /// Handles the Click event of the CloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseForm();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Generate the combine recordset for the xml keyID and ForeignKeyID
        /// </summary>
        /// <param name="recordSetType"></param>
        /// <returns></returns>
        private string GetUpdateRecordSetType(int recordSetType)
        {
            string recordSetXML = string.Empty;
            //StringBuilder currentRowItem = new StringBuilder();
            DataSet InsertSnapShotData = new DataSet("Root");


            InsertSnapShotData.Tables.Add(this.pinnedDataTable1);
            InsertSnapShotData.Tables.Add(this.unpinnedDataTable1);
            this.systemSnapShotCount = this.pinnedDataTable1.Rows.Count + this.unpinnedDataTable1.Rows.Count;
            recordSetXML = InsertSnapShotData.GetXml();
            if (InsertSnapShotData.Tables.CanRemove(this.pinnedDataTable1))
            {
                InsertSnapShotData.Tables.Remove(this.pinnedDataTable1.TableName);
            }
            if (InsertSnapShotData.Tables.CanRemove(this.unpinnedDataTable1))
            {
                InsertSnapShotData.Tables.Remove(this.unpinnedDataTable1.TableName);
            }
            recordSetXML = recordSetXML.Replace("KEYIDS", "Table");
            recordSetXML = recordSetXML.Replace("FKEYYIDS", "Table");
            return recordSetXML;

        }
        // Changes in the generate the KeyID XML and FKEYID XML.
        /// <summary>
        /// Handles the Click event of the ReplaceButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form queryUpdateForm = new Form();
                object[] optionalParameter = null;

                MemoryStream updateLayoutStream = new MemoryStream();
                bool resetVisiblity = this.ResetLink.Visible;
                bool layoutRecordVisiblity = this.LayoutRecords.Visible;
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(updateLayoutStream, PropertyCategories.All);
                string updateXmlString = this.ConvertToXML(updateLayoutStream);

                //// Constructing Optional Parameter
                int queryViewId;
                int.TryParse(this.QueryViewCombo.SelectedValue.ToString().Trim(), out queryViewId);

                ////obtain the value for  update keyIds and Foreign keyIds
                this.GetUpdateSnapShotRecords(true);

                //this.GetSnapShotRecords(true);
                string keyIds = this.GetUpdateRecordSetType(1);
                optionalParameter = new object[] { queryViewId, keyIds, this.keyIdColumnName, this.formMasterNo };

                queryUpdateForm = TerraScanCommon.GetForm(9039, optionalParameter, this.form9033Control.WorkItem);
                if (queryUpdateForm is F9039)
                {
                    (queryUpdateForm as F9039).Grid = this.QueryEngineGrid;
                    if (queryUpdateForm != null)
                    {
                        queryUpdateForm.ShowDialog();
                    }
                }

                this.isPinned = true;

                updateXmlString = this.ApplyLayout(updateXmlString);

                if (queryUpdateForm.DialogResult == DialogResult.Cancel)
                {
                    this.ResetLink.Visible = resetVisiblity;
                    this.LayoutRecords.Visible = layoutRecordVisiblity;
                }
                //else
                //{

                this.SetCheckBoxPinningLayout();
                this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Contains(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0]))
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Remove(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0]);
                    this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Add(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0], true, true);
                }
                else
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Add(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0], true, true);
                }

                //this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
                this.QueryEngineGrid.Refresh();

                this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
                this.isPinned = false;
                //}
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the QueryButtonPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryButtonPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                int queryViewId = 0;
                int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                Form queryViewForm = new Form();
                object[] optionalParameter = new object[] { queryViewId };
                queryViewForm = TerraScanCommon.GetForm(9041, optionalParameter, this.form9033Control.WorkItem);
                if (queryViewForm != null)
                {
                    queryViewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the SnapShotPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SnapShotPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                object[] optionalParameter = null;
                int selectedValue = 0;
                bool selectedValueFound;
                int selectedSnapshotValue = 0;
                Form snapShotForm = new Form();

                //// Gets Pinned and Unpinned Records for the Current RecordSet
                this.GetSnapShotRecords(true);

                int queryViewId;
                int.TryParse(this.QueryViewCombo.SelectedValue.ToString().Trim(), out queryViewId);

                //// BugID : 2580 Snapshot saving should use server-side recordset, not grid
                int recordCount = 0;
                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                {
                    if (this.QuerySnapShotCombo.SelectedIndex >= 0)
                    {
                        if (this.queryEngineData.Tables[1].Rows.Count >= 6 && !string.IsNullOrEmpty(this.queryEngineData.Tables[1].Rows[5][0].ToString()))
                        {
                            int.TryParse(this.queryEngineData.Tables[1].Rows[3][0].ToString().Trim(), out recordCount);
                        }
                    }
                    else if (!string.IsNullOrEmpty(this.queryEngineData.Tables[1].Rows[0][0].ToString()))
                    {
                        int.TryParse(this.queryEngineData.Tables[1].Rows[2][0].ToString().Trim(), out recordCount);
                    }
                }

                /*if (this.QueryLayoutCombo.SelectedValue == null)
                {
                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                    {
                        if (!string.IsNullOrEmpty(this.queryEngineData.Tables[1].Rows[0][0].ToString()))
                        {
                            recordCount = Convert.ToInt32(this.queryEngineData.Tables[1].Rows[0][0].ToString());
                        }
                    }
                }
                else
                {
                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                    {
                        if (!string.IsNullOrEmpty(this.queryEngineData.Tables[1].Rows[0][0].ToString()))
                        {
                            recordCount = Convert.ToInt32(this.queryEngineData.Tables[1].Rows[2][0].ToString().Trim().Split('O').GetValue(0).ToString().Trim());
                        }
                    }
                }*/

                if (this.QuerySnapShotCombo.SelectedValue != null)
                {
                    int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out selectedSnapshotValue);
                }

                ////optionalParameter = new object[] { this.pinnedDataTable, this.unpinnedDataTable, queryViewId, this.formMasterNo, this.queryViewName, this.formMasterPermissionFields };
                optionalParameter = new object[] { this.pinnedDataTable, this.unpinnedDataTable, queryViewId, this.formMasterNo, this.queryViewName, this.formMasterPermissionFields, recordCount, this.filterData, selectedSnapshotValue };
                snapShotForm = TerraScanCommon.GetForm(9040, optionalParameter, this.form9033Control.WorkItem);
                if (snapShotForm != null)
                {
                    snapShotForm.ShowDialog();
                    if (snapShotForm.DialogResult == DialogResult.Cancel)
                    {
                        if (this.QuerySnapShotCombo.Items.Count > 0)
                        {
                            if (this.QuerySnapShotCombo.SelectedValue != null)
                            {
                                int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out selectedValue);
                            }
                        }

                        this.PopulateQuerySnapShotCombo(false);

                        if (this.QuerySnapShotCombo.Items.Count > 0)
                        {
                            if (selectedValue > 0)
                            {
                                selectedValueFound = this.FindSnapShotItems(selectedValue);
                                if (selectedValueFound)
                                {
                                    this.QuerySnapShotCombo.SelectedValue = selectedValue;
                                }
                                else
                                {
                                    int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out selectedValue);

                                    this.LoadSnapShot(selectedValue, queryViewId);

                                    this.GetNumberOfRecords(false);
                                    this.SnapShotRecords.Visible = true;
                                }
                            }
                            else
                            {
                                this.QuerySnapShotCombo.SelectedIndex = -1;
                            }
                        }
                        else
                        {
                            this.SnapShotRecords.Visible = false;
                        }
                    }
                    else
                    {
                        //this.SetUltraData();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the LayoutpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LayoutpictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                ////if (queryRefreshed)
                ////{
                object[] optionalParameter = null;
                this.Cursor = Cursors.WaitCursor;
                int previousSelectedValue = 0;
                int selectedLayoutValue = 0;
                bool layoutModified = false;
                bool resetLinkVisible = this.ResetLink.Visible;

                //// This piece of code Saves the Current Layout as an XML String
                MemoryStream saveLayout = new MemoryStream();
                if (this.QueryEngineGrid.Rows.Count > 0)
                {
                    DataView defaultView = this.designDetailsTable.DefaultView;
                    defaultView.Sort = "VisiblePosition ASC";

                    this.designDetailsTable = defaultView.ToTable();
                    for (int i = 0; i < defaultView.Count; i++)
                    {
                        if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Exists(this.designDetailsTable.Rows[i]["KeyColumnName"].ToString()))
                        {
                            this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.VisiblePosition = Convert.ToInt32(this.designDetailsTable.Rows[i]["VisiblePosition"]);
                            //this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.Fixed = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsFixed"]);
                            //this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Hidden = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsHidden"]);
                        }
                    }
                    //this.QueryEngineGrid.DisplayLayout.SaveAsXml(saveLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.TempQueryGrid.DisplayLayout.SaveAsXml(saveLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    string saveXMLString = this.ConvertToXML(saveLayout);

                    //for (int i = 0; i < this.designDetailsTable.Rows.Count; i++)
                    //{
                    //    if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Exists(this.designDetailsTable.Rows[i]["KeyColumnName"].ToString()))
                    //    {
                    //        this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.VisiblePosition = Convert.ToInt32(this.designDetailsTable.Rows[i]["VisiblePosition"]);
                    //        //this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.Fixed = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsFixed"]);
                    //        //this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Hidden = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsHidden"]);
                    //    }
                    //}

                    //saveLayout.Flush();
                    //this.TempQueryGrid.DisplayLayout.SaveAsXml(saveLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    //saveXMLString = this.ConvertToXML(saveLayout);
                    //MemoryStream saveLayout1 = new MemoryStream();
                    //this.QueryEngineGrid.DisplayLayout.SaveAsXml(saveLayout1, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    //saveXMLString = this.ConvertToXML(saveLayout1);

                    Form layoutManagementForm = new Form();
                    string optionalString = string.Empty;
                    if (!string.IsNullOrEmpty(this.LayoutFieldLabel.Text))
                    {
                        optionalString = this.LayoutFieldLabel.Text.Substring(9);
                        if (!string.IsNullOrEmpty(optionalString))
                        {
                            layoutModified = true;
                        }
                        else
                        {
                            layoutModified = false;
                        }
                    }

                    if (this.QueryLayoutCombo.SelectedValue != null)
                    {
                        int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                    }

                    optionalParameter = new object[] { saveXMLString, this.QueryViewCombo.SelectedValue.ToString().Trim(), optionalString, this.formMasterNo, this.queryViewName, this.formMasterPermissionFields, selectedLayoutValue, layoutModified };
                    layoutManagementForm = TerraScanCommon.GetForm(9038, optionalParameter, this.form9033Control.WorkItem);
                    ////TerraScanCommon.SetValue(layoutManagementForm, "Grid", QueryEngineGrid);
                    if (layoutManagementForm != null)
                    {
                        layoutManagementForm.ShowDialog();
                        if (!this.layoutMangementFlag)
                        {
                            if (layoutManagementForm.DialogResult == DialogResult.Cancel)
                            {
                                if (this.QueryLayoutCombo.SelectedValue != null)
                                {
                                    int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out previousSelectedValue);
                                    this.PopulateQueryLayoutCombo();

                                    DataView queryLayoutView = new DataView(this.listQueryLayout);
                                    queryLayoutView.Sort = this.listQueryLayout.QueryLayoutIDColumn.ColumnName;
                                    int selectedValueFound = queryLayoutView.Find(previousSelectedValue);

                                    if (selectedValueFound < 0)
                                    {
                                        if (this.QueryLayoutCombo.Items.Count > 0)
                                        {
                                            ////////this.QueryViewCombo.SelectedIndex = 0;
                                            ////this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                                            ////this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                                            ////this.summaryDetails.Rows.Clear();
                                            ////this.tempSummaryDetails.Rows.Clear();
                                            ////this.designDetailsTable.Rows.Clear();

                                            this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                            this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                            this.filterData = null;
                                            this.sortData = null;
                                            this.summaryData = null;
                                            this.calculatedData = null;
                                            this.summaryDetails.Rows.Clear();
                                            this.tempSummaryDetails.Rows.Clear();
                                            this.calculatedColumnDetails.Rows.Clear();
                                            this.designDetailsTable.Rows.Clear();
                                            ////here newly created column are added and all the column are hidden
                                            for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                            {
                                                this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                            }

                                            this.GetNewColumnValue();

                                            if (this.QuerySnapShotCombo.SelectedIndex != -1)
                                            {
                                            }
                                            else
                                            {
                                                this.LoadQueryEngine();
                                            }

                                            MemoryStream ms = new MemoryStream();

                                            //// To Reset Layout
                                            this.QueryEngineGrid.DisplayLayout.SaveAsXml(ms, PropertyCategories.All);
                                            this.defaultXmlString = this.ConvertToXML(ms);

                                            if (this.QueryEngineGrid.Layouts.Exists("QueryViewLayout"))
                                            {
                                                this.QueryEngineGrid.Layouts["QueryViewLayout"].SaveAsXml(ms, PropertyCategories.All);
                                            }
                                            else
                                            {
                                                this.QueryEngineGrid.Layouts.Add("QueryViewLayout");
                                                this.QueryEngineGrid.Layouts["QueryViewLayout"].SaveAsXml(ms, PropertyCategories.All);
                                            }

                                            this.OnFilterRowSelect();
                                        }
                                        else
                                        {
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
                                            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                            this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                            this.filterData = null;
                                            this.sortData = null;
                                            this.summaryData = null;
                                            this.calculatedData = null;
                                            this.summaryDetails.Rows.Clear();
                                            this.tempSummaryDetails.Rows.Clear();
                                            this.calculatedColumnDetails.Rows.Clear();
                                            this.designDetailsTable.Rows.Clear();
                                            ////here newly created column are added and all the column are hidden
                                            for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                            {
                                                this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                            }

                                            this.GetNewColumnValue();

                                            if (this.QuerySnapShotCombo.SelectedIndex != -1)
                                            {
                                            }
                                            else
                                            {
                                                this.LoadQueryEngine();
                                            }

                                            MemoryStream ms = new MemoryStream();
                                            this.defaultXmlString = this.ConvertToXML(ms);

                                            if (this.QueryEngineGrid.Layouts.Exists("QueryViewLayout"))
                                            {
                                                this.QueryEngineGrid.Layouts["QueryViewLayout"].SaveAsXml(ms, PropertyCategories.All);
                                            }
                                            else
                                            {
                                                this.QueryEngineGrid.Layouts.Add("QueryViewLayout");
                                                this.QueryEngineGrid.Layouts["QueryViewLayout"].SaveAsXml(ms, PropertyCategories.All);
                                            }

                                            this.OnFilterRowSelect();
                                            ////this.LayoutRecords.Visible = false;
                                            this.ResetLink.Visible = resetLinkVisible;

                                            if (this.QueryLayoutCombo.SelectedIndex == -1)
                                            {
                                                this.LayoutRecords.Visible = false;
                                            }
                                            else
                                            {
                                                this.LayoutRecords.Visible = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (this.QueryLayoutCombo.Items.Count > 0)
                                        {
                                            this.QueryLayoutCombo.SelectedValue = previousSelectedValue;
                                            this.LayoutRecords.Visible = true;
                                            this.ResetLink.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    this.PopulateQueryLayoutCombo();
                                    this.ResetLink.Visible = resetLinkVisible;
                                    if (this.QueryLayoutCombo.SelectedValue == null)
                                    {
                                        this.QueryLayoutCombo.SelectedValue = previousSelectedValue;
                                    }

                                    if (this.QueryEngineGrid.Layouts.Count > 0)
                                    {
                                        if (this.QueryEngineGrid.Layouts.Exists("QueryViewLayout"))
                                        {
                                            if (this.QueryEngineGrid.Layouts["QueryViewLayout"].Equals(this.modifiedLayoutXml))
                                            {
                                                this.ResetLink.Visible = resetLinkVisible;

                                                if (this.QueryLayoutCombo.SelectedIndex == -1)
                                                {
                                                    this.LayoutRecords.Visible = false;
                                                }
                                                else
                                                {
                                                    this.LayoutRecords.Visible = true;
                                                }
                                            }
                                            else
                                            {
                                                this.LayoutRecords.Visible = true;
                                                this.ResetLink.Visible = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.QueryLayoutCombo.SelectedIndex == -1)
                            {
                                this.LayoutRecords.Visible = false;
                            }
                            else
                            {
                                this.LayoutRecords.Visible = true;
                                int selectedLayout = 0;
                                if (this.QueryLayoutCombo.SelectedValue != null)
                                {
                                    int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayout);
                                }

                                this.tempLayoutId = selectedLayout;
                            }

                            int snapShotId = 0;
                            int queryViewId = 0;

                            this.selectedLayoutIndex = this.QueryLayoutCombo.SelectedIndex;
                            if (this.listQueryLayout.Rows.Count > 0)
                            {
                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);

                                if (this.QueryLayoutCombo.Items.Count > 0)
                                {
                                    if (this.QueryLayoutCombo.SelectedIndex >= 0)
                                    {
                                        ////this.QuerySnapShotCombo.SelectedIndex = -1; // changed by suganth
                                        //// this.LoadQueryEngine(); Commetted For Issue Check
                                        if (this.QuerySnapShotCombo.SelectedIndex != -1)
                                        {
                                            this.SnapShotRecords.Visible = true; // changed by suganth
                                        }
                                        else
                                        {
                                            this.SnapShotRecords.Visible = false;
                                        }

                                        this.LayoutRecords.Visible = true;
                                    }
                                }
                            }

                            this.ResetLink.Visible = false;
                            this.ResetLink.Visible = false;
                            this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                            this.OnFilterRowSelect();
                        }
                    }

                    this.layoutMangementFlag = false;
                    this.loadedLayout = true;
                }
                else
                {
                    MessageBox.Show("No Layout to save", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion ButtonClick

        #region ResetLinkClick

        /// <summary>
        /// Handles the LinkClicked event of the ResetLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ResetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Encoding enc = Encoding.ASCII;

                if (!string.IsNullOrEmpty(this.defaultXmlString))
                {
                    this.QueryEngineGrid.DataSource = this.TempQueryGrid.DataSource;

                    if (this.QueryLayoutCombo.SelectedIndex >= 0)
                    {
                        this.selectedLayoutXml = this.listQueryLayout.Rows[this.QueryLayoutCombo.SelectedIndex][this.listQueryLayout.LayoutXMLColumn.ColumnName].ToString();

                        DataSet ds = new DataSet();

                        if (!string.IsNullOrEmpty(this.selectedLayoutXml))
                        {
                            this.summaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Rows.Clear();
                            System.Text.Encoding encrip = System.Text.Encoding.ASCII;

                            //// Gets the String as Byte array
                            Byte[] fileAsByte = encrip.GetBytes(this.selectedLayoutXml);

                            //// Converts Byte array to MemoryStream
                            MemoryStream ms = new MemoryStream(fileAsByte);

                            //// Loads the Layout from XML Data
                            this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            //// Gets the String as Byte array
                            Byte[] fileAsByteTemp = encrip.GetBytes(this.selectedLayoutXml);

                            //// Converts Byte array to MemoryStream
                            MemoryStream tempStream = new MemoryStream(fileAsByteTemp);

                            this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;

                            this.defaultXmlString = this.selectedLayoutXml;

                            this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                            ////here newly created column are added and all the column are hidden
                            for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                            {
                                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                            }

                            this.summaryDetails.Rows.Clear();
                            this.GetLayoutDetails();
                        }
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        this.filterData = null;
                        this.sortData = null;
                        this.summaryData = null;
                        this.calculatedData = null;
                        this.summaryDetails.Rows.Clear();
                        this.tempSummaryDetails.Rows.Clear();
                        this.calculatedColumnDetails.Rows.Clear();
                        this.designDetailsTable.Rows.Clear();
                        ////here newly created column are added and all the column are hidden
                        for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                        }

                        this.GetNewColumnValue();
                    }

                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        if (this.QuerySnapShotCombo.SelectedValue != null)
                        {
                            int snapShotId;
                            int queryViewId = 0;
                            int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out snapShotId);
                            int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);
                            this.LoadSnapShot(snapShotId, queryViewId);
                        }
                        else
                        {
                            this.FillDataSet(0, false);
                        }
                    }
                    else
                    {
                        this.FillDataSet(0, false);
                    }

                    this.SetUltraData();

                    this.SetLayoutDetails();
                    ////this.SetNewColumnValue();

                    this.summaryFlag = true;
                    this.updatedXmlString = this.defaultXmlString;

                    if (this.queryViewTable.Rows.Count > 0)
                    {
                        MemoryStream appliedLayout = new MemoryStream();
                        this.QueryEngineGrid.DisplayLayout.SaveAsXml(appliedLayout, PropertyCategories.All);
                        this.updatedXmlString = this.ConvertToXML(appliedLayout);
                        this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                        this.GetNumberOfRecords(false);
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.ResetLink.Visible = false;

                        if (this.QueryLayoutCombo.SelectedIndex == -1)
                        {
                            this.LayoutRecords.Visible = false;
                        }
                        else
                        {
                            this.LayoutRecords.Visible = true;
                        }

                        this.QueryEngineGrid.Focus();
                        ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                        Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
                        if (filteredRow.Length > 0)
                        {
                            this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                        }
                    }

                    if (this.queryEngineData.Tables.Count > 2)
                    {
                        this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                    }

                    this.SetCheckBoxPinningLayout();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion ResetLinkClick

        #region DefaultLinkClicked

        /// <summary>
        /// Handles the LinkClicked event of the DefaultLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DefaultLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();

                #region ISSUEFIX : CLEAR LINK VISIBLITY AFTER DEFAULT LINK CLICK

                bool clearLinkStatus = this.ClearLink.Visible;

                #endregion

                if (this.QueryViewCombo.SelectedValue != null)
                {
                    int queryViewId;
                    Int32.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out queryViewId);

                    this.sortKey = string.Empty;
                    this.sortOrder = string.Empty;
                    this.filterData = null;
                    this.sortData = null;
                    this.summaryData = null;
                    this.calculatedData = null;
                    this.summaryDetails.Rows.Clear();
                    this.tempSummaryDetails.Rows.Clear();
                    this.calculatedColumnDetails.Rows.Clear();
                    this.calculatedData = null;

                    this.queryRefreshed = true;

                    ////this.ApplyLayout();
                    this.LoadDefaultLayout(queryViewId);

                    ////this.SetSummaryValue();

                    ////if (this.calculatedColumnDetails.Rows.Count > 0)
                    ////{
                    ////    this.SetNewColumnValue();
                    ////}
                    ////else
                    ////{
                    ////    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                    ////    this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                    ////}

                    ////this.SetGridDesignDetails();
                    this.calculatedData = null;

                    this.tempLayoutId = this.defaultLayoutId;
                    this.OnFilterRowSelect();
                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                    this.GetNumberOfRecords(false);
                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.QuerySnapShotCombo.SelectedIndex = -1;
                    this.ResetLink.Visible = false;
                    this.ClearLink.Visible = clearLinkStatus;

                    if (this.QueryLayoutCombo.SelectedIndex == -1)
                    {
                        this.LayoutRecords.Visible = false;
                    }
                    else
                    {
                        this.LayoutRecords.Visible = true;
                    }

                    if (this.queryEngineData.Tables.Count > 2)
                    {
                        this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                    }

                    this.ClearLink.Visible = false;
                    this.SnapShotRecords.Visible = false;

                    this.SetCheckBoxPinningLayout();
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

        #region ClearLinkClick

        /// <summary>
        /// Handles the LinkClicked event of the ClearLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ClearLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                MemoryStream clearMemoryStream = new MemoryStream();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(clearMemoryStream, PropertyCategories.All);
                string clearLayoutXml = this.ConvertToXML(clearMemoryStream);
                bool resetLinkVisiblity = this.ResetLink.Visible;

                if (this.QueryEngineGrid.Layouts.Exists("ClearLayout"))
                {
                    this.QueryEngineGrid.Layouts["ClearLayout"].SaveAsXml(this.currentLayout, PropertyCategories.All);
                }
                else
                {
                    this.QueryEngineGrid.Layouts.Add("ClearLayout");
                    this.QueryEngineGrid.Layouts["ClearLayout"].SaveAsXml(this.currentLayout, PropertyCategories.All);
                }

                int selectedLayoutValue = 0;

                if (this.calculatedData == null)
                {
                    DataTable calculatedColumnValues = new DataTable("Table");
                    calculatedColumnValues.Columns.AddRange(new DataColumn[] { new DataColumn("NewColumn") });

                    for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                    {
                        if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound && this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout)
                        {
                            if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key.Equals(" "))
                            {
                                DataRow row = calculatedColumnValues.NewRow();
                                row["NewColumn"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                calculatedColumnValues.Rows.Add(row);
                            }
                        }
                    }

                    this.calculatedData = TerraScanCommon.GetXmlString(calculatedColumnValues);
                    if (this.calculatedData.Equals("<Root />"))
                    {
                        this.calculatedData = null;
                    }
                }

                if (this.systemSnapShotLoaded)
                {
                    this.systemSnapShotLoaded = false;
                    this.PopulateQuerySnapShotCombo(false);
                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        this.QuerySnapShotCombo.SelectedIndex = -1;
                        this.SnapShotRecords.Visible = false;
                    }

                    this.LoadQueryEngine();
                    return;
                }

                if (this.QuerySnapShotCombo.SelectedIndex != -1)
                {
                    if (this.QuerySnapShotCombo.Items.Count > 0)
                    {
                        this.QuerySnapShotCombo.SelectedIndex = -1;
                        this.ClearLink.Visible = false;
                        this.SnapShotRecords.Visible = false;
                    }

                    if (this.QueryLayoutCombo.Items.Count > 0)
                    {
                        if (this.QueryLayoutCombo.SelectedValue != null)
                        {
                            int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                        }
                    }

                    if (!this.NoDefaultLayout)
                    {
                        this.QueryEngineGrid.DataSource = null;

                        this.FillDataSet(0, false);

                        ////Repopulate UltraDataSource
                        this.SetUltraData();

                        if (selectedLayoutValue != 0)
                        {
                            bool findLayoutItem = this.FindLayoutItems(selectedLayoutValue);
                            if (findLayoutItem)
                            {
                                this.QueryLayoutCombo.SelectedValue = selectedLayoutValue;
                                ////this.ApplyLayout();
                                this.SetLayoutDetails();
                            }
                            else
                            {
                                this.QueryLayoutCombo.SelectedIndex = -1;
                                this.LayoutRecords.Visible = false;
                                this.ResetLink.Visible = false;
                            }
                        }
                        else
                        {
                            this.SetLayoutDetails();
                        }

                        this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                        this.OnFilterRowSelect();
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.GetNumberOfRecords(false);
                        this.NoOfRecords.Visible = true;
                        this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                        //// TO CHECK WHETHER SNAPSHOT LOADED OR NOT
                        this.snapShotLoaded = false;
                    }
                    else
                    {
                        #region entered by suganth review needed from vinoth

                        this.QueryEngineGrid.DataSource = null;

                        this.FillDataSet(0, false);

                        this.SetUltraData();

                        if (selectedLayoutValue != 0)
                        {
                            bool findLayoutItem = this.FindLayoutItems(selectedLayoutValue);

                            if (findLayoutItem)
                            {
                                this.SetLayoutDetails();

                                this.ResetLink.Visible = false;

                                if (this.QueryLayoutCombo.SelectedIndex == -1)
                                {
                                    this.LayoutRecords.Visible = false;
                                }
                                else
                                {
                                    this.LayoutRecords.Visible = true;
                                }
                            }
                            else
                            {
                                this.QueryLayoutCombo.SelectedIndex = -1;
                                this.LayoutRecords.Visible = false;
                                this.ResetLink.Visible = false;
                            }
                        }

                        this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                        this.OnFilterRowSelect();
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.GetNumberOfRecords(false);
                        this.NoOfRecords.Visible = true;

                        //// To check whether snapshot loaded or not
                        this.snapShotLoaded = false;

                        #endregion entered by suganth review needed from vinoth
                    }

                    //// Todo : Write it in Correct Place

                    #region Bug #776 - Fix

                    // BUG #776: 
                    // ISSUE DESCRIPTION - LAYOUT COUNT DIAPLAY IS WRONG WHEN CLEARE IS CLICKED.
                    // FIX DESCIPTION - AFTER APPLYING THE LAYOUT GETS THE RECORD COUNT ONCE AGAIN

                    this.GetNumberOfRecords(false);

                    #endregion Bug #776 - Fix

                    this.ResetLink.Visible = resetLinkVisiblity;

                    if (this.QueryLayoutCombo.SelectedIndex != -1)
                    {
                        this.LayoutRecords.Visible = true;
                    }
                    else
                    {
                        this.LayoutRecords.Visible = false;
                    }
                }

                this.ClearLink.Visible = false;
                this.SnapShotRecords.Visible = false;

                if (this.queryEngineData.Tables.Count > 2)
                {
                    this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                }

                this.SetCheckBoxPinningLayout();
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

        #endregion ClearLinkClick

        #region UltraDataSource

        /// <summary>
        /// Handles the CellDataRequested event of the QueryUltraDataSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs"/> instance containing the event data.</param>
        private void QueryUltraDataSource_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
        {
            try
            {
                ////if (QueryUltraDataSource.Band.Columns[0].Key.Equals(queryEngineData.Tables[0].Columns[0].ColumnName) && (queryEngineData.Tables[0].Columns.Count >= QueryUltraDataSource.Band.Columns.Count) && queryEngineData.Tables[0].Rows.Count > 0 && e.Row.Index < queryEngineData.Tables[0].Rows.Count)
                if (this.queryEngineData.Tables[0].Rows.Count > 0 && e.Row.Index < this.queryEngineData.Tables[0].Rows.Count)
                {
                    ////Added this count to populate only bound column values
                    int count = this.queryEngineData.Tables[0].Columns.Count - QueryUltraDataSource.Band.Columns.Count;
                    ////if (count > 0 && e.Column.Index < QueryUltraDataSource.Band.Columns.Count - 1)
                    if (count > 0 && e.Column.Index < (this.queryEngineData.Tables[0].Columns.Count - count))
                    {
                        if (this.queryEngineData.Tables[0].Rows[e.Row.Index].ItemArray[e.Column.Index].ToString() != string.Empty)
                        {
                            ////e.Data = this.queryEngineData.Tables[0].Rows[e.Row.Index].ItemArray[e.Column.Index + count].ToString();
                            e.Data = this.queryEngineData.Tables[0].Rows[e.Row.Index].ItemArray[e.Column.Index].ToString();
                        }
                    }
                    else
                    {
                        if (this.queryEngineData.Tables[0].Rows[e.Row.Index].ItemArray[e.Column.Index].ToString() != string.Empty)
                        {
                            e.Data = this.queryEngineData.Tables[0].Rows[e.Row.Index].ItemArray[e.Column.Index].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion UltraDataSource

        #region Summary Event

        /// <summary>
        /// Handles the AfterSummaryDialog event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.AfterSummaryDialogEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_AfterSummaryDialog(object sender, AfterSummaryDialogEventArgs e)
        {
            try
            {
                if (e.SummariesChanged)
                {
                    DataTable ds = this.tempSummaryDetails;
                    DataRow[] foundRows;
                    string expression = string.Concat("SummaryColumn = '", e.Column.ToString(), "'");
                    foundRows = ds.Select(expression);
                    if (foundRows.Length > 0)
                    {
                        this.tempSummaryDetails.Rows.Remove(foundRows[0]);
                    }
                }
                this.GetTempSummaryValue();
                this.SetSummaryValue();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.LayoutStatus(this.resetLayout);
                this.modifiedLayoutXml = this.ConvertToXML(this.resetLayout);
                this.summaryChanged = true;
                this.loadedLayout = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Summary Event

        #region Grid Event

        /// <summary>
        /// Handles the DoubleClickRow event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (e.Row.Index >= 0)
                {
                    this.CloseForm();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the BeforeColPosChanged event of the QueryEngineGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.BeforeColPosChangedEventArgs"/> instance containing the event data.</param>
        private void QueryEngineGrid_BeforeColPosChanged(object sender, BeforeColPosChangedEventArgs e)
        {
            try
            {
                if (e.PosChanged.Equals(PosChanged.HiddenStateChanged))
                {
                    if (!this.queryEngineData.Tables[0].Columns[0].Caption.Equals(e.ColumnHeaders[0].Caption)
                        && !this.queryEngineData.Tables[0].Columns[this.keyIdColumnName].Caption.Equals(e.ColumnHeaders[0].Caption))
                    {
                        ////To uncheck column in column chooser grid
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns[e.ColumnHeaders[0].Caption].Hidden = true;
                        if (this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(e.ColumnHeaders[0].Caption))
                        {
                            this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[e.ColumnHeaders[0].Caption].Hidden = true;
                        }

                        string expression = "SummaryColumn = '" + e.ColumnHeaders[0].Caption + "'";
                        this.RemoveSummary(expression);
                    }
                    else
                    {
                        ////Action cancelled - To avoid removing keyid column from the Grid
                        e.Cancel = true;
                    }
                }
                else if (e.ColumnHeaders[0].Caption.Equals(" "))
                {
                    if (!e.PosChanged.Equals(PosChanged.Sized))
                    {
                        ////Action cancelled - To avoid moving checkbox column from the Grid
                        e.Cancel = true;
                    }
                    else
                    {
                        if (e.ColumnHeaders[0].SizeResolved.Width != 34)
                        {
                            ////Action cancelled - To avoid moving checkbox column from the Grid
                            e.Cancel = true;
                        }
                    }
                }
                else if (e.PosChanged.Equals(PosChanged.Moved) && e.ColumnHeaders[0].VisiblePosition.Equals(0))
                {
                    // Action cancelled - To avoid moving other columns in front of checkbox column
                    e.Cancel = true;
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Removes the summary.
        /// </summary>
        /// <param name="filterExpression">The filter expression.</param>
        private void RemoveSummary(string filterExpression)
        {
            DataRow[] checkedRow = null;
            ////Get the collection of checked rows
            checkedRow = this.tempSummaryDetails.Select(filterExpression);

            if (checkedRow.Length > 0)
            {
                for (int i = 0; i < checkedRow.Length; i++)
                {
                    this.tempSummaryDetails.Rows.Remove(checkedRow[i]);
                }
            }

            DataRow[] sumcheckedRow = null;

            ////Get the collection of checked rows
            sumcheckedRow = this.summaryDetails.Select(filterExpression);

            if (sumcheckedRow.Length > 0)
            {
                for (int i = 0; i < sumcheckedRow.Length; i++)
                {
                    this.summaryDetails.Rows.Remove(sumcheckedRow[i]);
                }
            }
        }

        #endregion Grid Event

        #region TextBox Leave Event

        /// <summary>
        /// Handles the Leave event of the MaxRecordCountTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MaxRecordCountTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ////Set format
                if (string.IsNullOrEmpty(this.MaxRecordCountTextBox.Text.ToString()))
                {
                    this.MaxRecordCountTextBox.Text = string.Empty;
                    this.MaxRecordCountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.Integer;
                }
                else
                {
                    this.MaxRecordCountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.WholeInteger;
                    this.MaxRecordCountTextBox.ApplyCFGFormat = true;
                    this.MaxRecordCountTextBox.TextCustomFormat = "#,###";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion TextBox Leave Event

        #region Analytical Button Event
        /// <summary>
        /// Handles the Click event of the AnalyticsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AnalyticsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if ((this.queryEngineData.Tables[0].Rows.Count > 0) && (!string.IsNullOrEmpty(this.queryString)))
                {
                    Form queryAnalyticsForm = new Form();
                    object[] optionalParameter = null;

                    //// Constructing Optional Parameter
                    int queryViewId;
                    int.TryParse(this.QueryViewCombo.SelectedValue.ToString().Trim(), out queryViewId);
                    string queryViewName = this.QueryViewCombo.Text;
                    string snapShotName = string.Empty;
                    if (this.QuerySnapShotCombo.SelectedIndex > 0)
                    {
                        snapShotName = this.QuerySnapShotCombo.Text;
                    }

                    optionalParameter = new object[] { queryViewId, queryViewName, snapShotName, this.queryString };

                    queryAnalyticsForm = TerraScanCommon.GetForm(9042, optionalParameter, this.form9033Control.WorkItem);
                    if (queryAnalyticsForm != null)
                    {
                        queryAnalyticsForm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion Analytical Button Event

        #region Help Link
        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        #endregion Help Link

        #region Combo Tooltip Events

        /// <summary>
        /// Handles the MouseEnter event of the QueryViewCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryViewCombo_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryViewCombo.SelectedIndex >= 0 && this.CheckComboTextLength(this.QueryViewCombo.Text))
                {
                    this.ComboToolTip.RemoveAll();
                    this.ComboToolTip.SetToolTip(this.QueryViewCombo, this.QueryViewCombo.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the QuerySnapShotCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QuerySnapShotCombo_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.QuerySnapShotCombo.SelectedIndex >= 0 && this.CheckComboTextLength(this.QuerySnapShotCombo.Text))
                {
                    this.ComboToolTip.RemoveAll();
                    this.ComboToolTip.SetToolTip(this.QuerySnapShotCombo, this.QuerySnapShotCombo.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseEnter event of the QueryLayoutCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryLayoutCombo_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                if (this.QueryLayoutCombo.SelectedIndex >= 0 && this.CheckComboTextLength(this.QueryLayoutCombo.Text))
                {
                    this.ComboToolTip.RemoveAll();
                    this.ComboToolTip.SetToolTip(this.QueryLayoutCombo, this.QueryLayoutCombo.Text);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Combo Tooltip Events

        #region Refresh
        /// <summary>
        /// Handles the Click event of the RefreshButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                    this.Cursor = Cursors.WaitCursor;
                    int addedRowCount = 0;
                    this.RefreshButton.Focus();
                    int queryViewId;
                    queryViewId = Convert.ToInt32(this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryViewIDColumn.ColumnName].ToString());

                    this.GetGridDesignDetails();

                    this.keyIdCollection.Rows.Clear();

                    this.optionalkeyIdCollection.Rows.Clear();
                    DataTable tempSort = new DataTable("Table");
                    tempSort.Columns.AddRange(new DataColumn[] { new DataColumn("SortColumn"), new DataColumn("SortOrder") });
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();

                    ////Load Default Layout
                    int selectedLayoutValue = 0;
                    if (this.QueryLayoutCombo.SelectedValue != null)
                    {
                        int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayoutValue);
                    }

                    if (!this.queryRefreshed)
                    {                                                                
                        ////Get user changed Sort Order 
                        if (this.sortData != null)
                        {
                            tempSort.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(this.sortData));
                        }

                        F9033QueryEngineData.GetDefaultLayoutXMLDataTable defaultXmlTable = new F9033QueryEngineData.GetDefaultLayoutXMLDataTable();
                        Encoding enc = Encoding.ASCII;
                        defaultXmlTable = this.form9033Control.WorkItem.F9033_GetDefaultLayout(queryViewId);
                        this.tempQueryViewId = queryViewId;
                        if (defaultXmlTable.Rows.Count > 0)
                        {
                            string defaultLayoutString = defaultXmlTable.Rows[0][defaultXmlTable.LayoutXMLColumn.ColumnName].ToString();
                            this.defaultXmlString = string.Empty;
                            this.defaultXmlString = defaultLayoutString;
                            this.GetLayoutName(this.QueryViewCombo.SelectedIndex);
                            this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                            if (!string.IsNullOrEmpty(defaultLayoutString))
                            {
                                this.defaultLayoutExists = true;

                                this.ApplyLayout(defaultLayoutString);

                                this.queryLoadFlag = true;
                                //// this.tempLayoutId = this.defaultLayoutId; - Have to check
                            }
                        }

                        //// Have to check
                        /*if (selectedLayoutValue != 0 && this.tempLayoutId != selectedLayoutValue)
                        {
                        
                            this.GetNewColumnValue();
                           this.tempLayoutId = selectedLayoutValue; 
                            this.GetSortValue();
                            // this.GetNewColumnValue();
                            this.tempSummaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Merge(this.summaryDetails);
                            this.summaryData = TerraScanCommon.GetXmlString(tempSummaryDetails);
                            if (this.summaryData.Equals("<Root />"))
                            {
                                this.summaryData = null;
                            }
                        }*/
                        if (selectedLayoutValue != 0 && this.tempLayoutId != selectedLayoutValue)
                        {
                            this.summaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Rows.Clear();
                            this.loadedLayout = true;
                            this.ApplyLayout();
                            this.GetNewColumnValue();
                            this.GetSortValue();
                            this.GetGridDesignDetails();
                        }
                        else
                        {
                            this.GetNewColumnValue();
                            this.GetFilterValue();

                            this.GetSortValue();

                            tempSort.Merge(this.sortDetails);
                            this.sortData = TerraScanCommon.GetXmlString(tempSort);

                            if (this.sortData.Equals("<Root />"))
                            {
                                this.sortData = null;
                            }

                            this.GetSummaryValue();
                            this.GetGridDesignDetails();
                            this.tempSummaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Merge(this.summaryDetails);
                            this.summaryData = TerraScanCommon.GetXmlString(this.tempSummaryDetails);
                            if (this.summaryData.Equals("<Root />"))
                            {
                                this.summaryData = null;
                            }
                        }
                    }
                    else
                    {
                        if (selectedLayoutValue != 0 && this.tempLayoutId != selectedLayoutValue)
                        {
                            this.summaryDetails.Rows.Clear();
                            this.tempSummaryDetails.Rows.Clear();
                            this.loadedLayout = true;
                            this.ApplyLayout();
                            this.GetGridDesignDetails();
                            this.GetNewColumnValue();
                            //// this.GetSortValue();
                        }
                        else if (selectedLayoutValue != 0)
                        {
                            this.GetNewColumnValue();
                            this.GetGridDesignDetails();
                            if (this.summaryChanged == false)
                            {
                                this.tempSummaryDetails.Rows.Clear();
                                this.tempSummaryDetails.Merge(this.summaryDetails);
                                this.summaryData = TerraScanCommon.GetXmlString(this.tempSummaryDetails);
                                if (this.summaryData.Equals("<Root />"))
                                {
                                    this.summaryData = null;
                                }
                            }
                            else
                            {
                                if (!this.loadedLayout)
                                {
                                    this.GetSummaryValue();
                                    this.summaryDetails.Rows.Clear();
                                    this.summaryDetails.Merge(this.tempSummaryDetails);
                                }
                                else
                                {
                                    if (this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count > 0)
                                    {
                                        UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                                        for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                                        {
                                            band.Summaries.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                                        }

                                        this.GetSummaryValue();
                                    }
                                }

                                this.summaryData = TerraScanCommon.GetXmlString(this.summaryDetails);
                                if (this.summaryData.Equals("<Root />"))
                                {
                                    this.summaryData = null;
                                }
                            }
                        }
                        else
                        {
                            this.calculatedColumnDetails.Rows.Clear();
                            this.GetNewColumnValue();
                            this.GetGridDesignDetails();
                            this.summaryDetails.Rows.Clear();
                            this.summaryDetails.Merge(this.tempSummaryDetails);
                            this.summaryData = TerraScanCommon.GetXmlString(this.summaryDetails);
                            if (this.summaryData.Equals("<Root />"))
                            {
                                this.summaryData = null;
                            }
                        }

                        this.GetFilterValue();
                        this.GetSortValue();
                    }

                    bool isNotValidFilter = false;
                    int prevSnapShotId = 0;
                    this.queryRefreshed = true;
                    this.openWithKeyId = 0;

                    if (this.filterData != null)
                    {
                        DataSet filterSet = new DataSet();
                        StringReader stringReader = new StringReader(this.filterData);
                        System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                        filterSet.ReadXml(textReader);

                        if (filterSet.Tables.Count > 0)
                        {
                            if (filterSet.Tables[0].Rows.Count > 0)
                            {
                                for (int rowCount = 0; rowCount < filterSet.Tables[0].Rows.Count; rowCount++)
                                {
                                    if (filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "Equals" || filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "NotEquals" || filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "LessThan" || filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "LessThanorEqualto" || filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "GreaterThan" || filterSet.Tables[0].Rows[rowCount].ItemArray[1].ToString() == "GreaterThanorEqualto")
                                    {
                                        if (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Boolean")
                                            && !this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Int16")
                                            && !this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Int32")
                                            && !this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Int64")
                                            && !this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Byte")
                                            && !this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("Decimal")) // if (!IsInteger(filterSet.Tables[0].Rows[rowCount].ItemArray[2].ToString()))
                                        {
                                            try
                                            {
                                                if (!filterSet.Tables[0].Rows[rowCount].ItemArray[2].ToString().Contains("[")
                                                    && !filterSet.Tables[0].Rows[rowCount].ItemArray[2].ToString().Contains("(NonBlanks)")
                                                    && !filterSet.Tables[0].Rows[rowCount].ItemArray[2].ToString().Contains("(Blanks)"))
                                                {
                                                    Convert.ToDateTime(filterSet.Tables[0].Rows[rowCount].ItemArray[2].ToString());
                                                }
                                            }
                                            catch
                                            {
                                                if (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[filterSet.Tables[0].Rows[rowCount].ItemArray[0].ToString()].DataType.Name.Equals("String"))
                                                {
                                                    isNotValidFilter = true;
                                                }
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!isNotValidFilter)
                    {
                        if (this.QuerySnapShotCombo.Items.Count > 0)
                        {
                            if (this.QuerySnapShotCombo.SelectedValue != null)
                            {
                                int.TryParse(this.QuerySnapShotCombo.SelectedValue.ToString(), out prevSnapShotId);
                            }
                        }

                        if (prevSnapShotId > 0)
                        {
                            if (selectedLayoutValue != 0 && this.tempLayoutId != selectedLayoutValue)
                            {
                                this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                {
                                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                                    {
                                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);
                                        ////when the remove column count will decrease
                                        i = i - 1;
                                    }
                                }

                                ////here newly created column are added and all the column are hidden
                                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                {
                                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                }

                                this.LoadSnapshotRecords(prevSnapShotId, queryViewId);
                                this.SetUltraData();
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                                this.SetLayoutDetails();
                                this.tempLayoutId = selectedLayoutValue;
                            }
                            else
                            {
                                ////this.GetNewColumnValue();

                                this.LoadSnapShot(prevSnapShotId, queryViewId);

                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

                                this.SetSelectedLayout();
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

                                if (this.calculatedColumnDetails.Rows.Count > 0)
                                {
                                    this.SetNewColumnValue();
                                }

                                if (this.sortData != null)
                                {
                                    this.SetSortOrder();
                                }

                                if (this.filterData != null)
                                {
                                    this.SetFilterValue();
                                }
                                else
                                {
                                    this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                                }

                                this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                                if (this.summaryDetails.Rows.Count > 0)
                                {
                                    this.SetSummaryValue();
                                }
                                else
                                {
                                    this.SetTempSummaryValue();
                                }

                                this.SetGridDesignDetails();

                                Encoding enc = Encoding.ASCII;
                                MemoryStream gridLayout = new MemoryStream();
                                this.QueryEngineGrid.DisplayLayout.SaveAsXml(gridLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                                string gridXMLString = this.ConvertToXML(gridLayout);

                                // Gets the String as Byte array
                                Byte[] fileAsByteTemp = enc.GetBytes(gridXMLString);

                                // Converts Byte array to MemoryStream
                                MemoryStream tempMemory = new MemoryStream(fileAsByteTemp);

                                this.TempQueryGrid.DisplayLayout.LoadFromXml(tempMemory, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowSummaries = AllowRowSummaries.False;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.VisiblePosition = 0;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Width = 34;
                                //e.Row.Cells[0].Column.CellActivation = Activation.AllowEdit;
                                //e.Row.Cells[0].Column.CellClickAction = CellClickAction.Edit;
                                //e.Row.Cells[0].Activation = Activation.AllowEdit;
                                //e.Row.Activation = Activation.AllowEdit;
                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.AllowEdit;
                                this.QueryEngineGrid.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.Default;

                                this.SnapShotRecords.Visible = true;
                                this.ClearLink.Visible = true;
                            }
                        }
                        else
                        {
                            if (selectedLayoutValue != 0 && this.tempLayoutId != selectedLayoutValue)
                            {
                                this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                {
                                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                                    {
                                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);

                                        ////when the remove column count will decrease
                                        i = i - 1;
                                    }
                                }

                                ////here newly created column are added and all the column are hidden
                                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                                {
                                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                                }

                                this.FillDataSet(0, false);
                                this.SetUltraData();
                                this.SetLayoutDetails();
                                this.tempLayoutId = selectedLayoutValue;
                                if (this.queryEngineData.Tables[0].Rows.Count > 0)
                                {
                                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                                }
                            }
                            else
                            {
                                this.LoadQueryEngine();

                                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

                                if (this.calculatedColumnDetails.Rows.Count > 0)
                                {
                                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                    this.SetNewColumnValue();
                                }
                                else
                                {
                                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                                }

                                if (this.filterData != null)
                                {
                                    this.SetFilterValue();
                                }
                                else
                                {
                                    this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                                }

                                if (this.sortData != null)
                                {
                                    this.SetSortOrder();
                                }

                                this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                                if (this.summaryDetails.Rows.Count > 0)
                                {
                                    this.SetSummaryValue();
                                }
                                else
                                {
                                    this.SetTempSummaryValue();
                                }

                                this.SetGridDesignDetails();


                            }
                        }

                        ////Filter value
                        this.filterRowCount = this.QueryEngineGrid.Rows.FilteredInRowCount;
                        this.GetNumberOfRecords(false);
                        this.OnFilterRowSelect();
                        if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                        {
                            this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                        }

                        this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                        this.LayoutStatus(this.resetLayout);
                        this.modifiedLayoutXml = this.ConvertToXML(this.resetLayout);

                        if (this.queryEngineData.Tables.Count > 2)
                        {
                            this.queryString = this.queryEngineData.Tables[2].Rows[0]["QueryString"].ToString();
                        }

                        this.TempQueryGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        this.TempQueryGrid.DisplayLayout.Bands[0].SortedColumns.Clear();
                        this.TempQueryGrid.DisplayLayout.Bands[0].ColumnFilters.CopyFrom(this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters);
                        this.TempQueryGrid.DisplayLayout.Bands[0].SortedColumns.CopyFrom(this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns);
                        this.TempQueryGrid.DisplayLayout.Bands[0].Override.RowFilterAction = RowFilterAction.AppearancesOnly;
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Override.RowFilterAction = RowFilterAction.AppearancesOnly;

                        ////this.GetGridDesignDetails();
                        for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = true;
                        }

                        DataView defaultView = this.designDetailsTable.DefaultView;
                        defaultView.Sort = "VisiblePosition ASC";

                        this.designDetailsTable = defaultView.ToTable();
                        for (int i = 0; i < this.designDetailsTable.Rows.Count; i++)
                        {
                            if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Exists(this.designDetailsTable.Rows[i]["KeyColumnName"].ToString()))
                            {
                                this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.VisiblePosition = Convert.ToInt32(this.designDetailsTable.Rows[i]["VisiblePosition"]);
                                this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Header.Fixed = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsFixed"]);
                                this.TempQueryGrid.DisplayLayout.Bands[0].Columns[designDetailsTable.Rows[i]["KeyColumnName"].ToString()].Hidden = Convert.ToBoolean(this.designDetailsTable.Rows[i]["IsHidden"]);
                            }
                        }

                        if (this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Count > 0)
                        {
                            UltraGridBand band = this.TempQueryGrid.DisplayLayout.Bands[0];

                            for (int i = 0; i < this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                            {
                                band.Summaries.Add(this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                            }
                        }

                        this.SetCheckBoxPinningLayout();

                    }
                    else
                    {
                        MessageBox.Show("Please Enter Appropriate Filter Value", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.QueryEngineGrid.UpdateData();
                }
                //if (this.totalRecords > 10000)
                //{
                GC.Collect();
                GC.WaitForPendingFinalizers();
                //}
                /* IntPtr ptr;
                 try
                 {
                     System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();

                     ptr = Marshal.AllocHGlobal(IntPtr.Size);
                     if (IntPtr.Size == 8)
                     {

                         long max = Marshal.ReadInt64(proc.MaxWorkingSet);
                         max = max / 2;
                         if (proc.WorkingSet64 >= max)
                         {
                             GC.Collect();
                             GC.WaitForPendingFinalizers();
                         }
                     }
                     else if (IntPtr.Size == 4)
                     {
                         ptr = Marshal.AllocHGlobal(IntPtr.Size);
                         long max = Marshal.ReadInt32(proc.MaxWorkingSet);
                         max = max / 2;
                         if (proc.WorkingSet >= max)
                         {
                             // call gc
                             GC.Collect();
                             GC.WaitForPendingFinalizers();
                         }
                     }
                     if (ptr != IntPtr.Zero)
                     {
                         Marshal.FreeHGlobal(ptr);
                     }
                 }

                 catch (Exception ex)
                 {
                     MessageBox.Show("9033 Excep");
                 }*/
            }

                //GC.Collect();
            //GC.WaitForPendingFinalizers();

            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The amount of available memory may be low.")
                    || ex.Message == "Exception of type 'System.OutOfMemoryException' was thrown." || ex.Message == "The underlying connection was closed: The connection was closed unexpectedly.")
                {
                    MessageBox.Show("Unable to process the request.Please try again later.","Terrascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.CloseForm();
                    EventLog.WriteEntry("TerraScan Application Logging", ex.Message.ToString(), EventLogEntryType.Error, 100);
                }
                else
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        #endregion Refresh

        #endregion Events

        #region PrivateMethods

        #region LoadQueryViewCombo

        /// <summary>
        /// Loads the query view combo.
        /// </summary>
        private void LoadQueryViewCombo()
        {
            this.QueryViewCombo.ValueMember = "QueryViewID";
            this.QueryViewCombo.DisplayMember = "QueryViewName";
            this.queryViewTable = this.form9033Control.WorkItem.F9033_ListQueryView(this.formMasterNo);
            if (this.queryViewTable.Rows.Count > 0)
            {
                this.QueryViewCombo.DataSource = this.queryViewTable;
                this.queryViewUnavailable = false;
                this.layoutName = this.queryViewTable.Rows[0][this.queryViewTable.LayoutNameColumn.ColumnName].ToString();
                this.queryViewName = this.queryViewTable.Rows[0][this.queryViewTable.QueryViewColumn.ColumnName].ToString();
                this.foreignKeyName = this.queryViewTable.Rows[0][this.queryViewTable.ForeignKeyNameColumn.ColumnName].ToString();

            }
            else
            {
                this.QueryViewCombo.DataSource = null;
                //// this.NoOfRecords.Text = "0 Rows";
                this.GetNumberOfRecords(false);
                this.queryViewUnavailable = true;
            }
        }

        #endregion

        #region LoadQueryEngine

        /// <summary>
        /// Loads the query engine.
        /// </summary>
        private void LoadQueryEngine()
        {
            if (this.queryViewTable.Rows.Count > 0)
            {
                if (this.openWithKeyId > 0)
                {
                    bool isDuplicateRecord = false;
                    DataRow keyRow = this.optionalkeyIdCollection.NewRow();
                    keyRow["KeyId"] = this.openWithKeyId.ToString();
                    if (this.optionalkeyIdCollection.Rows.Count > 0)
                    {
                        int limit = this.optionalkeyIdCollection.Rows.Count;
                        for (int j = 0; j < limit; j++)
                        {
                            if (this.optionalkeyIdCollection.Rows[j]["KeyId"].ToString() == keyRow["KeyId"].ToString())
                            {
                                isDuplicateRecord = true;
                                break;
                            }
                        }
                    }

                    if (isDuplicateRecord == false)
                    {
                        this.optionalkeyIdCollection.Rows.Add(keyRow);
                        this.queryEngineData.Tables[0].Rows.Add(this.openWithKeyId);
                    }

                    this.queryRefreshed = false;
                }
                else
                {
                    this.FillDataSet(0, false);
                }

                if (this.queryEngineData.Tables[0].Rows.Count > 0)
                {
                    ////Repopulate UltraDataSource
                    this.SetUltraData();

                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.queryViewLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.intialLayout.Flush();
                    this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                    this.defaultXmlString = null;
                    this.defaultXmlString = this.ConvertToXML(this.intialLayout);

                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                    this.OnFilterRowSelect();
                    if (this.queryEngineData.Tables.Count >= 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                    {
                        this.TotalRecords = this.queryEngineData.Tables[0].Rows.Count;
                    }

                    this.GetNumberOfRecords(false);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                    //// To check whether snapshot loaded or not
                    this.snapShotLoaded = false;
                }
                else
                {
                    ////this.NoOfRecords.Text = "0 Rows";
                    this.GetNumberOfRecords(false);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = this.layoutName;
                    this.currentRowIndex = -1;
                    this.totalRecords = 0;
                    this.currentRowKeyId = -99;

                    //// To check whether snapshot loaded or not
                    this.snapShotLoaded = false;

                    ////Repopulate UltraDataSource
                    this.SetUltraData();

                    this.SetSummaryValue();
                }
            }
            else
            {
                this.QueryEngineGrid.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.QueryEngineGrid.DisplayLayout.Scrollbars = Infragistics.Win.UltraWinGrid.Scrollbars.None;
            }
        }

        #endregion LoadQueryEngine

        #region LoadSystemSnapShot

        /// <summary>
        /// Loads the system snap shot.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        private void LoadSystemSnapShot(int systemSnapShotId)
        {
            if (this.F9033Control.WorkItem.RootWorkItem.State[this.formMasterNo + "SystemSnapShotId"] != null)
            {
                systemSnapShotId = (int)this.F9033Control.WorkItem.RootWorkItem.State[this.formMasterNo + "SystemSnapShotId"];
            }
            this.FillDataSet(systemSnapShotId, true);

            if (this.queryEngineData.Tables[0].Rows.Count > 0)
            {
                ////Repopulate UltraDataSource
                if (!this.columnBind)
                {
                    this.SetUltraData();
                }

                this.selectedRow = 0;
                this.ClearLink.Visible = true;

                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                {
                    DataTable sysSnapShotCountTable = new DataTable();
                    sysSnapShotCountTable = this.queryEngineData.Tables[1];
                    int.TryParse(sysSnapShotCountTable.Rows[0][0].ToString(), out this.snapShotCount);
                }

                this.LoadDefaultXMLRecordSet();
                this.intialLayout.Flush();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.defaultXmlString = null;
                this.defaultXmlString = this.ConvertToXML(this.intialLayout);

                this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
                this.OnFilterRowSelect();
                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                {
                    this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                }

                this.GetNumberOfRecords(true);
                this.NoOfRecords.Visible = true;
                this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                //// To check whether snapshot loaded or not
                this.snapShotLoaded = true;

                //// Sets the this.systemSnapShotLoaded, to reload the query when QueryButton Clicked
                this.systemSnapShotLoaded = true;
            }
            else
            {
                this.GetNumberOfRecords(true);
                this.NoOfRecords.Visible = true;
                this.LayoutFieldLabel.Text = this.layoutName;
                this.currentRowIndex = -1;
                this.totalRecords = 0;
                this.currentRowKeyId = -99;

                //// To check whether snapshot loaded or not
                this.snapShotLoaded = true;

                //// Sets the this.systemSnapShotLoaded, to reload the query when QueryButton Clicked
                this.systemSnapShotLoaded = true;
            }
        }

        #endregion LoadSystemSnapShot

        #region LoadDefaultLayout

        /// <summary>
        /// Loads the defaultlayout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        private void LoadDefaultLayout(int queryViewId)
        {
            F9033QueryEngineData.GetDefaultLayoutXMLDataTable defaultXmlTable = new F9033QueryEngineData.GetDefaultLayoutXMLDataTable();

            string defaultLayoutString;
            Encoding enc = Encoding.ASCII;
            defaultXmlTable = this.form9033Control.WorkItem.F9033_GetDefaultLayout(queryViewId);
            if (defaultXmlTable.Rows.Count > 0)
            {
                defaultLayoutString = defaultXmlTable.Rows[0][defaultXmlTable.LayoutXMLColumn.ColumnName].ToString();
                this.defaultXmlString = string.Empty;
                this.defaultXmlString = defaultLayoutString;
                this.GetLayoutName(this.QueryViewCombo.SelectedIndex);
                this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                if (!string.IsNullOrEmpty(defaultLayoutString))
                {
                    this.QueryEngineGrid.DataSource = this.TempQueryGrid.DataSource;

                    int selectedLayout = 0;
                    if (this.QueryLayoutCombo.SelectedValue != null)
                    {
                        int.TryParse(this.QueryLayoutCombo.SelectedValue.ToString(), out selectedLayout);
                    }

                    this.tempLayoutId = selectedLayout;

                    this.defaultLayoutExists = true;

                    //// Gets the String as Byte array
                    Byte[] fileAsByte = enc.GetBytes(defaultLayoutString);

                    //// Converts Byte array to MemoryStream
                    MemoryStream ms = new MemoryStream(fileAsByte);

                    //// Loads the Layout from XML Data
                    this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    Byte[] fileAsByteTemp = enc.GetBytes(defaultLayoutString);

                    //// Converts Byte array to MemoryStream
                    MemoryStream tempStream = new MemoryStream(fileAsByteTemp);

                    this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                    this.queryLoadFlag = true;

                    if (this.QueryLayoutCombo.Items.Count > 0)
                    {
                        this.QueryLayoutCombo.SelectedValue = this.defaultLayoutId;
                    }

                    for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                    {
                        if (this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()))
                        {
                            this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
                        }
                        //// this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                    }

                    this.GetLayoutDetails();

                    this.FillDataSet(0, false);

                    ////Repopulate UltraDataSource
                    this.SetUltraData();
                    //this.ResetUltraData();
                    this.SetUltraData();
                    if (this.calculatedColumnDetails.Rows.Count > 0)
                    {
                        this.SetNewColumnValue();
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                    }

                    if (this.filterData != null)
                    {
                        this.SetFilterValue();
                    }

                    if (this.sortData != null)
                    {
                        this.SetSortOrder();
                    }

                    if (this.summaryData != null)
                    {
                        this.SetSummaryValue();
                    }

                    //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();

                    ////if (this.calculatedColumnDetails.Rows.Count > 0)
                    ////{
                    ////    this.SetNewColumnValue();
                    ////}


                    this.SetGridDesignDetails();
                    //RefreshRow rs = RefreshRow.ReloadData;
                    //this.QueryEngineGrid.Rows.Refresh(rs);

                    this.overAllRecord = this.queryEngineData.Tables[0].Rows.Count;
                    this.loadedLayout = true;
                }
            }
            else
            {
                this.defaultLayoutExists = false;

                //this.QueryEngineGrid.DataSource = null;
                this.TempQueryGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.ClearUnbound();
                this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.TempQueryGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Clear();
                this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                    {
                        this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);

                        ////when the remove column count will decrease
                        i = i - 1;
                    }
                }

                ////here newly created column are added and all the column are hidden
                for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                    this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden = false;
                }

                this.GetNewColumnValue();

                this.FillDataSet(0, false);

                ////Repopulate UltraDataSource
                this.SetUltraData();
                //this.ResetUltraData();
                this.GetGridDesignDetails();
                this.overAllRecord = this.queryEngineData.Tables[0].Rows.Count;

                MemoryStream msi = new MemoryStream();
                this.QueryEngineGrid.DisplayLayout.SaveAsXml(msi, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.defaultXmlString = this.ConvertToXML(msi);

                this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, Infragistics.Win.UltraWinGrid.PropertyCategories.All);
                this.defaultXmlString = this.ConvertToXML(this.resetLayout);

                if (this.QueryLayoutCombo.SelectedIndex != -1 || this.QuerySnapShotCombo.SelectedIndex != -1)
                {
                    this.QueryLayoutCombo.SelectedIndex = -1;
                    this.QuerySnapShotCombo.SelectedIndex = -1;

                    this.SnapShotRecords.Visible = false;
                    this.LayoutRecords.Visible = false;
                    this.ResetLink.Visible = false;
                    this.ClearLink.Visible = false;
                }
            }
        }

        #endregion LoadDefaultLayout

        #region ConvertToXML

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="layout">The layout.</param>
        /// <returns>String</returns>
        private string ConvertToXML(MemoryStream layout)
        {
            layout.Position = 0;
            System.Text.Encoding resetEnc = System.Text.Encoding.ASCII;
            Byte[] resetLayoutAsByte = new Byte[layout.Length];
            layout.Read(resetLayoutAsByte, 0, Convert.ToInt32(layout.Length));
            this.updatedXmlString = resetEnc.GetString(resetLayoutAsByte);
            return this.updatedXmlString;

        }

        #endregion ConvertToXML

        #region RowSelect

        /// <summary>
        /// Rows the select.
        /// </summary>
        /// <param name="keyIndex">Index of the key.</param>
        /// <param name="keyFieldColumn">The key field column.</param>
        private void RowSelect(int keyIndex, string keyFieldColumn)
        {
            if (keyIndex != -99)   //// Checks whether QueryEngine is with or without KeyId
            {
                ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();

                if (filteredRow.Length > 0)
                {
                    //// Finds the KeyID in the Filtered Grid
                    for (int rowIndex = 0; rowIndex < filteredRow.Length; rowIndex++)
                    {
                        bool found;

                        //// Checks the KeyId with the current Row
                        if (keyIndex.Equals(Convert.ToInt32(filteredRow[rowIndex].Cells[this.keyIdColumnName].Value.ToString().Trim())))
                        {
                            this.selectedRowIndex = rowIndex;
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            this.selectedRowIndex = -1;
                        }
                    }

                    //// QueryEngine Data : Gets the Row Index with the corresponding keyId
                    if (this.queryEngineData.Tables.Count > 0)
                    {
                        if (this.queryEngineData.Tables[0].Columns.Contains(keyFieldColumn))
                        {
                            this.SetCurrentRowindex(this.currentRowKeyId, filteredRow);
                        }
                        else
                        {
                            this.columnNameInvalid = true;
                        }
                    }

                    //// System SnapShotData : Gets the Row Index with the corresponding keyId
                    if (this.systemSnapShotData.Tables.Count > 0)
                    {
                        if (this.systemSnapShotData.Tables[0].Columns.Contains(this.keyIdColumnName))
                        {
                            this.SetCurrentRowindex(this.keyId, filteredRow);
                        }
                        else
                        {
                            this.columnNameInvalid = true;
                        }
                    }
                }
                else
                {
                    this.selectedRow = 0;
                }
            }
            else
            {
                if (!this.notFromMenu)
                {
                    //// If  QueryEngine without KeyId, Set the currentRowKeyId to 1st Record
                    this.currentRowKeyId = 0;
                    this.currentRowIndex = 0;
                    this.QueryEngineGrid.Focus();

                    if (this.QueryEngineGrid.Rows.Count > 0)
                    {
                        this.QueryEngineGrid.Rows[0].Activate();
                        this.QueryEngineGrid.Rows[0].Selected = true;
                        if (this.QueryEngineGrid.ActiveRow.Index >= 0)
                        {
                            if (this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
                            {
                                this.currentRowKeyId = Convert.ToInt32(this.QueryEngineGrid.ActiveRow.Cells[this.keyIdColumnName].Value);

                                this.AddColumnValuesInTheList();
                            }
                            else
                            {
                                this.currentRowKeyId = Convert.ToInt32(this.QueryEngineGrid.ActiveRow.Cells[0].Value);

                                this.AddColumnValuesInTheList();

                                this.keyIdColumnName = this.QueryEngineGrid.ActiveRow.Cells[0].Column.Key;
                            }
                        }
                    }
                }
                else
                {
                    this.currentRowIndex = -1;
                    this.currentRowKeyId = -99;
                }
            }
        }

        #endregion RowSelect

        #region RowSelect

        /// <summary>
        /// Rows the select.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="rowIndex">Index of the row.</param>
        private void RowSelect(Infragistics.Win.UltraWinGrid.UltraGridRow[] row, int rowIndex)
        {
            this.currentRowIndex = rowIndex;
            this.currentRowKeyId = Convert.ToInt32(row[rowIndex].Cells[this.keyIdColumnName].Value.ToString());

            this.AddColumnValuesInTheList();

            this.QueryEngineGrid.Focus();
            row[rowIndex].Activate();
            row[rowIndex].Selected = true;
            this.keyIdDeleted = false;
            this.columnNameInvalid = false;
        }

        #endregion

        #region ActiveRowDetails

        /// <summary>
        /// Sets the active row.
        /// </summary>
        private void ActiveRowDetails()
        {
            this.CurrentRowKeyId = this.currentRowKeyId;
            this.CurrentRowIndex = this.currentRowIndex;

            this.AddColumnValuesInTheList();
            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
            {
                this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
            }
        }

        #endregion ActiveRowDetails

        #region LayoutName

        /// <summary>
        /// Layouts the status.
        /// </summary>
        /// <param name="ms">The ms.</param>
        private void LayoutStatus(MemoryStream ms)
        {
            this.updatedXmlString = this.ConvertToXML(ms);
            if (!string.IsNullOrEmpty(this.updatedXmlString) && !this.isPinned)
            {
                //if (!string.IsNullOrEmpty(this.defaultXmlString) && !this.updatedXmlString.ToLower().Equals(this.defaultXmlString.ToLower()))
                //if (!string.IsNullOrEmpty(this.defaultXmlString) && !this.updatedXmlString.ToUpper().Equals(this.defaultXmlString.ToUpper()))
                if (!string.IsNullOrEmpty(this.defaultXmlString) && !this.updatedXmlString.Equals(this.defaultXmlString, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName + " (Modified)";
                    this.LayoutFieldLabel.Visible = false;

                    this.ResetLink.Visible = true;
                    this.LayoutRecords.Visible = true;
                    if (!this.queryLoadFlag)
                    {
                        this.queryLoadFlag = true;
                    }
                }
                else
                {
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                    this.ResetLink.Visible = false;
                }
            }
        }

        #endregion LayoutName

        #region LoadDefaultXMLRecordSet

        /// <summary>
        /// Loads the default XML record set.
        /// </summary>
        private void LoadDefaultXMLRecordSet()
        {
            int rowIndex;
            int queryViewId;
            if (this.queryViewTable.Rows.Count > 0)
            {
                DataView findRowIdView = new DataView(this.queryViewTable);
                if (this.queryViewTable.Columns.Contains("QueryViewId"))
                {
                    findRowIdView.Sort = "QueryViewId";    //// Assign the Primary Key Column with Sorted Order
                    this.queryViewTable = this.form9033Control.WorkItem.F9033_ListQueryView(this.formMasterNo);
                    if (this.queryViewTable.Rows.Count > 0)
                    {
                        queryViewId = Convert.ToInt32(this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryViewIDColumn.ColumnName].ToString());
                        int.TryParse(this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryLayoutIDColumn.ColumnName].ToString(), out this.layoutId);
                        rowIndex = findRowIdView.Find(queryViewId);

                        string loadDefaultXML = this.queryViewTable.Rows[rowIndex][this.queryViewTable.LayoutXMLColumn.ColumnName].ToString();

                        if (!string.IsNullOrEmpty(loadDefaultXML))
                        {
                            this.defaultLayoutExists = true;

                            System.Text.Encoding enc = System.Text.Encoding.ASCII;

                            //// Gets the String as Byte array
                            Byte[] fileAsByte = enc.GetBytes(loadDefaultXML);

                            //// Converts Byte array to MemoryStream
                            MemoryStream ms = new MemoryStream(fileAsByte);

                            //// Loads the Layout from XML Data
                            this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            Byte[] fileAsByteTemp = enc.GetBytes(loadDefaultXML);

                            //// Converts Byte array to MemoryStream
                            MemoryStream tempStream = new MemoryStream(fileAsByteTemp);

                            this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                            if (this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count > 0)
                            {
                                UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                                for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                                {
                                    band.Summaries.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                                }
                            }

                            #region Formula Issue Fix

                            this.AddRemoveTemCol();

                            #endregion

                            this.queryLoadFlag = true;

                            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                            {
                                this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                            }

                            this.GetNumberOfRecords(false);
                            this.NoOfRecords.Visible = true;
                            this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                        }
                        else
                        {
                            this.defaultLayoutExists = false;
                        }
                    }
                }
            }
        }

        #endregion

        #region GetLayoutName

        /// <summary>
        /// Gets the name of the layout.
        /// </summary>
        /// <param name="index">The index.</param>
        private void GetLayoutName(int index)
        {
            F9033QueryEngineData.ListQueryViewDataTable queryComboTable = new F9033QueryEngineData.ListQueryViewDataTable();
            queryComboTable = this.form9033Control.WorkItem.F9033_ListQueryView(this.formMasterNo);
            if (queryComboTable.Rows.Count > 0)
            {
                this.layoutName = queryComboTable.Rows[index][this.queryViewTable.LayoutNameColumn.ColumnName].ToString();
                int.TryParse(queryComboTable.Rows[index][this.queryViewTable.QueryLayoutIDColumn.ColumnName].ToString(), out this.defaultLayoutId);
            }
        }

        #endregion

        #region OnFilterRowSelect

        /// <summary>
        /// Called when [filter row select].
        /// </summary>
        private void OnFilterRowSelect()
        {
            bool find = false;
            if (this.queryEngineData.Tables.Count > 0 && this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
            {
                if (QueryEngineGrid.Rows.Count > 0 && QueryEngineGrid.Rows.Count >= this.selectedRow)
                {
                    string firstRowKeyValue = QueryEngineGrid.Rows[this.selectedRow].Cells[this.keyIdColumnName].Value.ToString();

                    ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                    Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetAllNonGroupByRows();

                    if (row != null && row.Length > 0)
                    {
                        string firstFilterValue = row[0].Cells[this.keyIdColumnName].Value.ToString();

                        foreach (Infragistics.Win.UltraWinGrid.UltraGridRow r in row)
                        {
                            if (firstRowKeyValue == r.Cells[this.keyIdColumnName].Value.ToString())
                            {
                                this.currentRowKeyId = Convert.ToInt32(firstRowKeyValue);
                                this.currentRowIndex = r.VisibleIndex - 1;

                                this.AddColumnValuesInTheList();

                                find = true;

                                this.AddColumnValuesInTheList();

                                break;
                            }
                            else
                            {
                                find = false;
                            }
                        }

                        if (!find)
                        {
                            DataTable filteredRowDataTable = new DataTable("FilteredDataRowTable");
                            filteredRowDataTable = this.CreateFilterRowDataTable();

                            DataView filterData = new DataView(filteredRowDataTable);
                            filterData.Sort = this.keyIdColumnName;     //// Assign the Primary Key Column with Sorted Order
                            this.selectedRow = filterData.Find(firstFilterValue);
                            this.currentRowKeyId = Convert.ToInt32(firstFilterValue);
                            this.currentRowIndex = 0;

                            this.AddColumnValuesInTheList();
                            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                            {
                                this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                            }

                            this.AddColumnValuesInTheList();
                        }
                        else
                        {
                            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                            {
                                this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                            }
                        }
                    }
                    else
                    {
                        if (this.keyIdDeleted)
                        {
                            if (this.prevRowIndex <= row.Length && row.Length > 0)
                            {
                                this.currentRowIndex = this.prevRowIndex;
                                this.currentRowKeyId = Convert.ToInt32(row[this.prevRowIndex].Cells[this.keyIdColumnName].Value.ToString());

                                this.AddColumnValuesInTheList();
                                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                                {
                                    this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                                }

                                this.AddColumnValuesInTheList();
                            }
                            else
                            {
                                if (this.selectedRow.Equals(0))
                                {
                                    if (row.Length > 0)
                                    {
                                        this.currentRowIndex = 0;
                                        this.currentRowKeyId = Convert.ToInt32(row[0].Cells[this.keyIdColumnName].Value.ToString());

                                        this.AddColumnValuesInTheList();
                                    }
                                    else
                                    {
                                        this.currentRowIndex = -1;
                                        this.currentRowKeyId = -99;
                                    }

                                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                                    {
                                        this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                                    }
                                }
                            }

                            this.keyIdDeleted = false;
                        }
                        else
                        {
                            this.currentRowIndex = -1;
                            this.currentRowKeyId = -99;
                        }
                    }
                }
            }
        }

        #endregion

        #region FindKeyId

        /// <summary>
        /// Finds the key id.
        /// </summary>
        /// <returns>Boolean</returns>
        private bool FindKeyId()
        {
            bool find = false;
            string key = this.keyId.ToString();
            ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
            Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetAllNonGroupByRows();
            if (row.Length > 0)
            {
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow r in row)
                {
                    if (key == r.Cells[this.keyIdColumnName].Value.ToString())
                    {
                        this.currentRowIndex = r.VisibleIndex - 1;
                        find = true;
                        break;
                    }
                    else
                    {
                        find = false;
                    }
                }
            }

            if (this.QueryEngineGrid.ActiveRow != null && this.QueryEngineGrid.ActiveRow.Index >= 0)
            {
                if (this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
                {
                    this.currentRowKeyId = Convert.ToInt32(this.QueryEngineGrid.ActiveRow.Cells[this.keyIdColumnName].Value);
                }
            }

            return find;
        }

        #endregion

        #region Add & Remove Temp Column

        /// <summary>
        /// Adds the remove tem col.
        /// </summary>
        private void AddRemoveTemCol()
        {
            #region Formula Issue Fix

            if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count > 0)
            {
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.UnboundColumnsCount > 0)
                {
                    string columnName = System.Guid.NewGuid().ToString();
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Add(columnName);
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Layout.Grid.CalcManager = new UltraCalcManager();
                    string formula = this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].FormulaAbsoluteName;
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[columnName].Hidden = true;

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[columnName].Formula = "[" + formula.Substring(24) + "]";
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Remove(columnName);
                }
            }

            #endregion
        }

        #endregion

        #region PopulateQueryLayoutCombo

        /// <summary>
        /// Populates the query layout combo.
        /// </summary>
        private void PopulateQueryLayoutCombo()
        {
            f9033defaultLayout = SharedFunctions.GetResourceString("DefaultLayout");
            F9033QueryEngineData.ListQueryLayoutDataTable listLayout = new F9033QueryEngineData.ListQueryLayoutDataTable();  
            int outQueryViewId = 0;

            if (this.QueryViewCombo.SelectedValue != null)
            {
                int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out outQueryViewId);
            }

            if (outQueryViewId > 0)
            {
                //To add empty row in layout dropdown.
                DataRow customRow = listLayout.NewRow();
                listLayout.Clear();
                customRow[listLayout.LayoutNameColumn.ColumnName] = "-None-";
                customRow[listLayout.UserIDColumn.ColumnName] = TerraScanCommon.UserId;
                customRow[listLayout.QueryLayoutIDColumn.ColumnName] = "99999";
                customRow[listLayout.LayoutXMLColumn.ColumnName] = f9033defaultLayout;
                listLayout.Rows.Add(customRow);

                this.listQueryLayout = this.form9033Control.WorkItem.F9033_ListQueryLayout(outQueryViewId, TerraScanCommon.UserId);
                this.QueryLayoutCombo.DisplayMember = this.listQueryLayout.LayoutNameColumn.ColumnName;
                this.QueryLayoutCombo.ValueMember = this.listQueryLayout.QueryLayoutIDColumn.ColumnName;

                listQueryLayout.Merge(listLayout);

                if (this.listQueryLayout.Rows.Count > 0)
                {
                    DataTable dt = listQueryLayout.OrderBy(t => t["QueryLayoutID"]).CopyToDataTable();
                    dt.DefaultView.Sort = "QueryLayoutID DESC";
                    DataView newDv = dt.DefaultView;
                    dt = newDv.ToTable();
                    listQueryLayout.Clear();
                    listQueryLayout.Merge(dt);

                    this.QueryLayoutCombo.DataSource = listQueryLayout;
                    this.GetLayoutName(this.QueryViewCombo.SelectedIndex);
                    if (this.defaultLayoutId > 0)
                    {
                        this.QueryLayoutCombo.SelectedValue = this.defaultLayoutId;
                    }
                    else
                    {
                        this.QueryLayoutCombo.SelectedValue = 0;
                        this.LayoutRecords.Visible = false;
                        this.ResetLink.Visible = false;
                    }

                    this.GetNumberOfRecords(false);
                }
                else
                {
                    this.QueryLayoutCombo.DataSource = null;
                }
            }
        }

        #endregion PopulateQueryLayoutCombo

        #region PopulateQuerySnapShotCombo

        /// <summary>
        /// Populates the query snap shot combo.
        /// </summary>
        /// <param name="systemSnapShotLoaded">Flag for SystemSnapShot</param>
        private void PopulateQuerySnapShotCombo(bool systemSnapShotLoaded)
        {
            F9033QueryEngineData.ListQuerySnapShotDataTable listQuerySnapShot = new F9033QueryEngineData.ListQuerySnapShotDataTable();
            int outQueryViewId = 0;
            if (this.QueryViewCombo.SelectedValue != null)
            {
                int.TryParse(this.QueryViewCombo.SelectedValue.ToString(), out outQueryViewId);
            }

            if (outQueryViewId > 0)
            {
                //To add empty row in snapshot dropdown.
                DataRow customRow = listQuerySnapShot.NewRow();
                listQuerySnapShot.Clear();
                customRow[listQuerySnapShot.SnapshotNameColumn.ColumnName] = "-None-";
                customRow[listQuerySnapShot.SnapshotIDColumn.ColumnName] = "0";                
                listQuerySnapShot.Rows.Add(customRow);

                this.QuerySnapShotCombo.DisplayMember = listQuerySnapShot.SnapshotNameColumn.ColumnName;
                this.QuerySnapShotCombo.ValueMember = listQuerySnapShot.SnapshotIDColumn.ColumnName;

                if (TerraScanCommon.IsDataBaseAvailable && TerraScanCommon.IsFieldUser)
                    WSHelper.IsOnLineMode = false;
                this.listQuerySnapShotMerge = this.form9033Control.WorkItem.F9033_ListQuerySnapShot(this.formMasterNo);
                this.listQuerySnapShotMerge.Merge(listQuerySnapShot);

                if (systemSnapShotLoaded)
                {
                    DataRow dr = listQuerySnapShot.NewRow();
                    dr[listQuerySnapShot.SnapshotIDColumn.ColumnName] = 0;
                    dr[listQuerySnapShot.SnapshotNameColumn.ColumnName] = "System SnapShot Loaded";
                    dr[listQuerySnapShot.RecordCountColumn.ColumnName] = this.QueryEngineGrid.Rows.FilteredInRowCount;
                    listQuerySnapShot.Rows.Add(dr);

                    if (listQuerySnapShot.Rows.Count > 0)
                    {
                        listQuerySnapShot.Merge(this.listQuerySnapShotMerge);
                        this.QuerySnapShotCombo.DataSource = listQuerySnapShot;
                    }
                }
                else
                {
                    if (this.listQuerySnapShotMerge.Rows.Count > 0)
                    {
                        DataTable dt = listQuerySnapShotMerge.OrderBy(t => t["SnapshotID"]).CopyToDataTable();
                        listQuerySnapShotMerge.Clear();
                        listQuerySnapShotMerge.Merge(dt);
                        this.QuerySnapShotCombo.DataSource = this.listQuerySnapShotMerge;
                    }
                    else
                    {
                        this.QuerySnapShotCombo.DataSource = null;
                    }
                }
            }

            this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.intialLayout, PropertyCategories.All);
            this.defaultXmlString = this.ConvertToXML(this.intialLayout);
        }

        #endregion PopulateQuerySnapShotCombo

        #region SetQueryLayoutToDefault

        /// <summary>
        /// Sets the query layout to default.
        /// </summary>
        private void SetQueryLayoutToDefault()
        {
            if (this.listQueryLayout.Rows.Count > 0)
            {
                F9033QueryEngineData.GetDefaultLayoutXMLDataTable defaultXmlTable = new F9033QueryEngineData.GetDefaultLayoutXMLDataTable();
                string defaultXMLString;
                Encoding enc = Encoding.ASCII;
                defaultXmlTable = this.form9033Control.WorkItem.F9033_GetDefaultLayout(Convert.ToInt32(this.QueryLayoutCombo.SelectedValue.ToString()));
            }
        }

        #endregion SetQueryLayoutToDefault

        #region GetNumberOfRecords

        /// <summary>
        /// Gets the number of records.
        /// </summary>
        /// <param name="systemSnapShotLoaded">Flag for SystemSnapShot</param>
        private void GetNumberOfRecords(bool systemSnapShotLoaded)
        {
            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
            {
                this.QueryViewRecords.AutoSize = true;
                this.SnapShotRecords.AutoSize = true;
                this.LayoutRecords.AutoSize = true;
                this.QueryViewRecords.Text = this.queryEngineData.Tables[1].Rows[0][0].ToString();
                this.SnapShotRecords.Text = this.queryEngineData.Tables[1].Rows[1][0].ToString();
                this.LayoutRecords.Text = this.queryEngineData.Tables[1].Rows[2][0].ToString();

                if (this.MaxRecordCountTextBox.Text.Equals(string.Empty))
                {
                    this.MaxRecordCountTextBox.Text = this.queryEngineData.Tables[1].Rows[4][0].ToString();
                    this.MaxRecordCountTextBox.ValidateType = TerraScan.UI.Controls.TerraScanTextBox.ControlvalidationType.WholeInteger;
                    this.MaxRecordCountTextBox.ApplyCFGFormat = true;
                    this.MaxRecordCountTextBox.TextCustomFormat = "#,###";
                }

                int maxAmongTwo = 0;
                int maxAmongThree = 0;

                if (this.QuerySnapShotCombo.SelectedIndex > 0)
                {
                    maxAmongTwo = Math.Max(this.QueryViewRecords.Width, this.SnapShotRecords.Width);
                    maxAmongThree = Math.Max(maxAmongTwo, this.LayoutRecords.Width);
                }
                else
                {
                    maxAmongThree = Math.Max(this.QueryViewRecords.Width, this.LayoutRecords.Width);
                }

                this.QueryViewRecords.AutoSize = false;
                this.SnapShotRecords.AutoSize = false;
                this.LayoutRecords.AutoSize = false;
                this.QueryViewRecords.Size = new System.Drawing.Size(maxAmongThree, this.QueryViewRecords.Height);
                this.SnapShotRecords.Size = new System.Drawing.Size(maxAmongThree, this.SnapShotRecords.Height);
                this.LayoutRecords.Size = new System.Drawing.Size(maxAmongThree, this.LayoutRecords.Height);
            }
        }

        #endregion GetNumberOfRecords

        #region LoadSnapShot

        /// <summary>
        /// Loads the snap shot.
        /// </summary>
        /// <param name="snapShotId">Snapshot ID</param>
        /// <param name="queryViewId">QueryView ID</param>
        private void LoadSnapShot(int snapShotId, int queryViewId)
        {
            DataSet snapShotData = new DataSet();

            this.LoadSnapshotRecords(snapShotId, queryViewId);

            if (this.queryEngineData.Tables[0].Rows.Count > 0)
            {
                this.SetUltraData();

                this.selectedRow = 0;
                this.OnFilterRowSelect();
                this.RowSelect(this.currentRowIndex, this.keyIdColumnName);

                //// ToDo : Issue Fix To Recreate the Layout after Query Button is clicked
                //// this.QueryEngineGrid.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                this.snapShotLoaded = true;
                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                {
                    DataTable snapShotCountTable = new DataTable();
                    snapShotCountTable = this.queryEngineData.Tables[1];
                    int.TryParse(snapShotCountTable.Rows[1][0].ToString().Trim().Split('O').GetValue(0).ToString().Trim(), out this.snapShotCount);
                }
            }
            else
            {
                this.snapShotLoaded = true;
                this.SetUltraData();
                this.SetSummaryValue();

                if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
                {
                    DataTable snapShotCountTable = new DataTable();
                    snapShotCountTable = this.queryEngineData.Tables[1];
                    int.TryParse(snapShotCountTable.Rows[1][0].ToString().Trim().Split('O').GetValue(0).ToString().Trim(), out this.snapShotCount);
                    //// int.TryParse(snapShotCountTable.Rows[0][0].ToString(), out this.snapShotCount);
                }

                this.QueryEngineGrid.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButtonFixedSize;
            }

            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
            {
                if (Convert.ToInt32(this.queryEngineData.Tables[1].Rows[2][0].ToString().Trim().Split('O').GetValue(0).ToString().Trim()) > Convert.ToInt32(this.queryEngineData.Tables[1].Rows[3][0].ToString()))
                {
                    this.PreviewButton.Enabled = false;
                    this.ExcelButton.Enabled = false;
                }
                else
                {
                    this.PreviewButton.Enabled = true;
                    this.ExcelButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Gets the snapshot records.
        /// </summary>
        /// <param name="snapShotId">Snapshot ID</param>
        /// <param name="queryViewId">QueryView ID</param>
        private void LoadSnapshotRecords(int snapShotId, int queryViewId)
        {
            string keyFilter = TerraScanCommon.GetXmlString(this.keyIdCollection);
            if (keyFilter.Equals("<Root />"))
            {
                keyFilter = null;
            }

            string maxRecordCount = null;
            if (!string.IsNullOrEmpty(this.MaxRecordCountTextBox.Text.ToString()))
            {
                long maxRecord = 0;
                long.TryParse(this.MaxRecordCountTextBox.Text.ToString().Replace(",", ""), out maxRecord);
                maxRecordCount = maxRecord.ToString();
            }
            else
            {
                maxRecordCount = null;
            }

            //// queryEngineData = this.form9033Control.WorkItem.F9033_GetSnapShotRecordSet(snapShotId, queryViewId);
            try
            {
                if (!this.hasChangeQueryView)
                {
                    this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridSnapshot(snapShotId, queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, 1, maxRecordCount);
                }
                else
                {
                    this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion LoadSnapShot

        #region getUpdateSnapShotRecords

        ///<summary>
        ///Get the Update SnapShot records for Genereate the both FKeyId and KeyID
        /// </summary>
        /// <param name="canGetFilterRow">if set to<c>true</c>[can get filter row]</param>
        private void GetUpdateSnapShotRecords(bool canGetFilterRow)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow;
            if (canGetFilterRow)
            {
                ////filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
            }
            else
            {
                filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
            }
            this.unpinnedDataTable1.Clear();
            this.pinnedDataTable1.Clear();
            //DataColumn unpinnedKeyIdColumn;
            DataColumn unpinnedForeignkeyId;
            DataColumn pinnedKeyIdColumn;
            //DataColumn pinnedForeignkeyId;
            if (!this.pinnedDataTable1.Columns.Contains("KeyID"))
            {
                pinnedKeyIdColumn = new DataColumn("KeyID");

                this.pinnedDataTable1.Columns.Add(pinnedKeyIdColumn);
                //this.pinnedDataTable1.Columns.Add(pinnedForeignkeyId);
            }
            if (!string.IsNullOrEmpty(this.foreignKeyName))
            {
                if (!this.unpinnedDataTable1.Columns.Contains("FKeyID"))
                {
                    //    unpinnedKeyIdColumn = new DataColumn("KeyID");
                    unpinnedForeignkeyId = new DataColumn("FKeyID");
                    //    this.unpinnedDataTable1.Columns.Add(unpinnedKeyIdColumn);
                    this.unpinnedDataTable1.Columns.Add(unpinnedForeignkeyId);
                }
            }
            for (int rowIndex = 0; rowIndex < filteredRow.Length; rowIndex++)
            {
                //if (filteredRow[rowIndex].Fixed)
                //{
                DataRow dr = this.pinnedDataTable1.NewRow();
                dr[0] = filteredRow[rowIndex].Cells[this.keyIdColumnName].Value;
                this.pinnedDataTable1.Rows.Add(dr);
                //}

                if (!string.IsNullOrEmpty(this.foreignKeyName))
                {
                    DataRow dr1 = this.unpinnedDataTable1.NewRow();
                    dr1[0] = filteredRow[rowIndex].Cells[this.foreignKeyName].Value;
                    this.unpinnedDataTable1.Rows.Add(dr1);
                }


            }


        }
        #endregion

        #region GetSnapShotRecords

        /// <summary>
        /// Gets the snap shot records.
        /// </summary>
        /// <param name="canGetFilterRow">if set to <c>true</c> [can get filter row].</param>
        private void GetSnapShotRecords(bool canGetFilterRow)
        {
            //Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow;
            //if (canGetFilterRow)
            //{
            //    ////filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
            //    filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
            //}
            //else
            //{
            //    filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
            //}

            try
            {
                //this.queryEngineData.Tables[0].AcceptChanges();
                //string filterCondition = this.queryEngineData.Tables[0].Columns[0].Caption + " = true";
                //DataRow[] filterRow = this.queryEngineData.Tables[0].Select(filterCondition);
                ////DataView filterView = this.queryEngineData.Tables[0].DefaultView;
                ////filterView.RowFilter = this.queryEngineData.Tables[0].Columns[0].Caption + " = 0";
                //DataSet ds = (DataSet)this.QueryEngineGrid.DataSource;
                //Infragistics.Win.UltraWinDataSource.UltraDataRowsCollection rows = ((Infragistics.Win.UltraWinDataSource.UltraDataSource)(this.QueryEngineGrid.DataSource)).Rows;

            }
            catch (Exception ex)
            {
            }

            this.unpinnedDataTable.Clear();
            this.pinnedDataTable.Clear();
            DataColumn unpinnedKeyIdColumn;
            DataColumn pinnedKeyIdColumn;
            if (!this.pinnedDataTable.Columns.Contains("KeyID"))
            {
                pinnedKeyIdColumn = new DataColumn("KeyID");
                this.pinnedDataTable.Columns.Add(pinnedKeyIdColumn);
            }

            if (!this.unpinnedDataTable.Columns.Contains("KeyID"))
            {
                unpinnedKeyIdColumn = new DataColumn("KeyID");
                this.unpinnedDataTable.Columns.Add(unpinnedKeyIdColumn);
            }

            this.QueryEngineGrid.UpdateData();

            //for (int rowIndex = 0; rowIndex < filteredRow.Length; rowIndex++)
            //{
            //    if (filteredRow[rowIndex].Fixed)
            //    {
            //        DataRow dr = this.pinnedDataTable.NewRow();
            //        dr[0] = filteredRow[rowIndex].Cells[this.keyIdColumnName].Value;
            //        this.pinnedDataTable.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        DataRow dr = this.unpinnedDataTable.NewRow();
            //        dr[0] = filteredRow[rowIndex].Cells[this.keyIdColumnName].Value;
            //        this.unpinnedDataTable.Rows.Add(dr);
            //    }
            //}

            for (int rowIndex = 0; rowIndex < this.QueryEngineGrid.Rows.Count; rowIndex++)
            {
                if (this.QueryEngineGrid.Rows[rowIndex].Cells[0].Value.ToString().ToUpper().Equals("TRUE"))
                {
                    DataRow dr = this.pinnedDataTable.NewRow();
                    dr[0] = this.QueryEngineGrid.Rows[rowIndex].Cells[this.keyIdColumnName].Value;
                    this.pinnedDataTable.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = this.unpinnedDataTable.NewRow();
                    dr[0] = this.QueryEngineGrid.Rows[rowIndex].Cells[this.keyIdColumnName].Value;
                    this.unpinnedDataTable.Rows.Add(dr);
                }
            }
        }

        #endregion GetSnapShotRecords

        #region GetRecordSetType

        /// <summary>
        /// Gets the type of the record set.
        /// </summary>
        /// <param name="recordSetType">Type of the record set.</param>
        /// <returns>String</returns>
        private string GetRecordSetType(int recordSetType)
        {
            string recordSetXML = string.Empty;
            StringBuilder currentRowItem = new StringBuilder();
            DataSet InsertSnapShotData = new DataSet("Root");

            switch (recordSetType)
            {
                case 1:
                    InsertSnapShotData.Tables.Add(this.pinnedDataTable);
                    InsertSnapShotData.Tables.Add(this.unpinnedDataTable);
                    this.systemSnapShotCount = this.pinnedDataTable.Rows.Count + this.unpinnedDataTable.Rows.Count;
                    recordSetXML = InsertSnapShotData.GetXml();
                    break;
                case 2:
                    InsertSnapShotData.Tables.Add(this.pinnedDataTable);
                    this.systemSnapShotCount = this.pinnedDataTable.Rows.Count;
                    recordSetXML = InsertSnapShotData.GetXml();
                    break;
                case 3:
                    InsertSnapShotData.Tables.Add(this.unpinnedDataTable);
                    this.systemSnapShotCount = this.unpinnedDataTable.Rows.Count;
                    recordSetXML = InsertSnapShotData.GetXml();
                    break;
                case 4:
                    currentRowItem.Append("<Root>");
                    currentRowItem.Append("<Table>");
                    currentRowItem.Append("<KeyID>");
                    currentRowItem.Append(this.currentRowKeyId);
                    currentRowItem.Append("</KeyID>");
                    currentRowItem.Append("</Table>");
                    currentRowItem.Append("</Root>");
                    this.systemSnapShotCount = 1;
                    break;
            }

            //// Removing Table from dataset
            //// Else it throws exception while adding the table second time to the same dataset
            if (InsertSnapShotData.Tables.CanRemove(this.pinnedDataTable))
            {
                InsertSnapShotData.Tables.Remove(this.pinnedDataTable.TableName);
            }

            if (InsertSnapShotData.Tables.CanRemove(this.unpinnedDataTable))
            {
                InsertSnapShotData.Tables.Remove(this.unpinnedDataTable.TableName);
            }

            if (!string.IsNullOrEmpty(recordSetXML))
            {
                recordSetXML = recordSetXML.Replace("Table2", "Table");
                recordSetXML = recordSetXML.Replace("Table1", "Table");
                return recordSetXML;
            }
            else
            {
                return currentRowItem.ToString();
            }
        }

        #endregion GetRecordSetType

        #region FindSnapShotItems

        /// <summary>
        /// Finds the snap shot items.
        /// </summary>
        /// <param name="selectedSnapShotValue">The selected snap shot value.</param>
        /// <returns>Boolean</returns>
        private bool FindSnapShotItems(int selectedSnapShotValue)
        {
            DataView snapShotData = new DataView(this.listQuerySnapShotMerge);
            snapShotData.Sort = this.listQuerySnapShotMerge.SnapshotIDColumn.ColumnName;
            int selectedValueFound = snapShotData.Find(selectedSnapShotValue);
            if (selectedValueFound >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion FindSnapShotItems

        #region Get Temporary table values

        /// <summary>
        /// Gets the summary value (summary column & summary operator) and store it in a DataTable.
        /// </summary>
        private void GetTempSummaryValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            int count = band.Summaries.Count;
            bool isDuplicateRecord = false;
            ////if (count != this.tempSummaryDetails.Rows.Count)
            ////{
            //    this.tempSummaryDetails.Rows.Clear();
            //}
            if (band.Summaries.Count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    DataRow summaryRow = this.tempSummaryDetails.NewRow();
                    summaryRow["SummaryColumn"] = band.Summaries[i].SourceColumn.Key;
                    summaryRow["SummaryOperator"] = band.Summaries[i].SummaryType.ToString();
                    isDuplicateRecord = false;
                    if (this.tempSummaryDetails.Rows.Count > 0)
                    {
                        int limit = this.tempSummaryDetails.Rows.Count;
                        for (int j = 0; j < limit; j++)
                        {
                            if ((this.tempSummaryDetails.Rows[j].ItemArray[1].ToString() == summaryRow["SummaryOperator"].ToString()) && (this.tempSummaryDetails.Rows[j].ItemArray[0].ToString() == summaryRow["SummaryColumn"].ToString()))
                            {
                                isDuplicateRecord = true;
                            }
                        }
                    }

                    if (isDuplicateRecord == false)
                    {
                        this.tempSummaryDetails.Rows.Add(summaryRow);
                    }

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.RemoveAt(i);

                }
            }
            else
            {
                this.tempSummaryDetails.Rows.Clear();
            }

            this.summaryData = TerraScanCommon.GetXmlString(this.tempSummaryDetails);

            if (this.summaryData.Equals("<Root />"))
            {
                this.summaryData = null;
            }
        }

        /// <summary>
        /// Sets the summary value while open the summary Dialog Window.
        /// Show both refreshed and unrefreshed data
        /// </summary>
        private void SetTempSummaryValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
            if (this.tempSummaryDetails.Rows.Count > 0)
            {
                string sumValue = string.Empty;
                for (int sumCount = 0; sumCount < this.tempSummaryDetails.Rows.Count; sumCount++)
                {
                    string sumKey = this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() + '@' + this.tempSummaryDetails.Rows[sumCount].ItemArray[0].ToString();
                    SummaryType type;
                    if (this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Sum")
                    {
                        type = SummaryType.Sum;
                    }
                    else if (this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Average")
                    {
                        type = SummaryType.Average;
                    }
                    else if (this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Minimum")
                    {
                        type = SummaryType.Minimum;
                    }
                    else if (this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Maximum")
                    {
                        type = SummaryType.Maximum;
                    }
                    else
                    {
                        type = SummaryType.Count;
                    }

                    if (this.queryEngineData.Tables[0].Columns.Contains(this.tempSummaryDetails.Rows[sumCount].ItemArray[0].ToString()))
                    {
                        Infragistics.Win.UltraWinGrid.SummarySettings TotalSummary = band.Summaries.Add(sumKey, type, band.Columns[this.tempSummaryDetails.Rows[sumCount].ItemArray[0].ToString()], SummaryPosition.UseSummaryPositionColumn);
                        if (this.queryEngineData.Tables.Count > 3)
                        {
                            for (int i = 0; i < this.queryEngineData.Tables[3].Columns.Count; i++)
                            {
                                string sum = band.Summaries[sumCount].SummaryType.ToString() + '@' + band.Summaries[sumCount].SourceColumn.ToString();
                                if (this.queryEngineData.Tables[3].Columns[i].ColumnName.ToString() == sum)
                                {
                                    sumValue = this.queryEngineData.Tables[3].Rows[0].ItemArray[i].ToString();
                                }
                            }

                            TotalSummary.DisplayFormat = this.tempSummaryDetails.Rows[sumCount].ItemArray[1].ToString() + " = " + sumValue;
                        }
                    }
                }
            }
        }

        #endregion Get Temporary table values

        #region FindLayoutItems

        /// <summary>
        /// Finds the layout items.
        /// </summary>
        /// <param name="selectedLayoutValue">The selected layout value.</param>
        /// <returns>Boolean</returns>
        private bool FindLayoutItems(int selectedLayoutValue)
        {
            DataView layoutItemsData = new DataView(this.listQueryLayout);
            layoutItemsData.Sort = this.listQueryLayout.QueryLayoutIDColumn.ColumnName;
            int selectedValueFound = layoutItemsData.Find(selectedLayoutValue);
            if (selectedValueFound != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion FindLayoutItems

        #region ApplyLayout

        /// <summary>
        /// Applies layout
        /// </summary>
        private void ApplyLayout()
        {
            if (this.QueryLayoutCombo.SelectedIndex >= 0)
            {
                this.selectedLayoutXml = this.listQueryLayout.Rows[this.QueryLayoutCombo.SelectedIndex][this.listQueryLayout.LayoutXMLColumn.ColumnName].ToString();

                DataSet ds = new DataSet();

                if (!string.IsNullOrEmpty(this.selectedLayoutXml))
                {
                    this.QueryEngineGrid.DataSource = this.TempQueryGrid.DataSource;

                    System.Text.Encoding enc = System.Text.Encoding.ASCII;

                    // Gets the String as Byte array
                    Byte[] fileAsByte = enc.GetBytes(this.selectedLayoutXml);

                    // Converts Byte array to MemoryStream
                    MemoryStream ms = new MemoryStream(fileAsByte);

                    // Loads the Layout from XML Data
                    this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    // Gets the String as Byte array
                    Byte[] fileAsByteTemp = enc.GetBytes(this.selectedLayoutXml);

                    // Converts Byte array to MemoryStream
                    MemoryStream tempMemory = new MemoryStream(fileAsByteTemp);

                    this.TempQueryGrid.DisplayLayout.LoadFromXml(tempMemory, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;

                    if (this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count > 0) ////&& this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count.Equals(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count))
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                        UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                        for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                        {
                            band.Summaries.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                        }
                    }

                    //this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;

                    #region Formula Issue Fix

                    this.AddRemoveTemCol();

                    #endregion
                    this.GetNewColumnValue();
                    this.defaultXmlString = this.selectedLayoutXml;

                    this.queryLoadFlag = true;
                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);

                    this.OnFilterRowSelect();

                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                    {
                        this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                    }

                    this.GetNumberOfRecords(false);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;

                    this.GetFilterValue();
                    this.GetSummaryValue();
                }
            }
            else
            {
                this.GetFilterValue();
                this.GetSummaryValue();
                ////this.GetNewColumnValue();
            }
        }

        #endregion

        #region Get New Column
        /// <summary>
        /// Gets the new column value.
        /// </summary>
        private void GetNewColumnValue()
        {
            this.calculatedColumnDetails.Rows.Clear();
            this.ColumnChooserGrid.DataSource = null;
            this.ColumnChooserGrid.DataSource = this.TempQueryGrid.DataSource;
            for (int i = 0; i < this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsBound)
                {
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Remove(i);

                    ////when the remove column count will decrease
                    i = i - 1;
                }
            }

            ////here newly created column are added and all the column are hidden
            for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                ////here the newly created column are added
                if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key))
                {
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key);
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key;
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Format;
                    this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                }

                this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden;
            }

            DataTable calculatedColumnValues = new DataTable("Table");
            calculatedColumnValues.Columns.AddRange(new DataColumn[] { new DataColumn("NewColumn") });
            try
            {
                for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    if (this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key))
                    {
                        if ((!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound && this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout)) //// || (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key) && this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout))
                        {
                            if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key.Equals(" "))
                            {
                                DataRow row = this.calculatedColumnDetails.NewRow();
                                row["CalculatedColumn"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                row["ColumnFormula"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                                row["ColumnDataType"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                                row["ColumnFormat"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Format;
                                row["ColumnCellAppearance"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                                row["IsChecked"] = this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout;
                                this.calculatedColumnDetails.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound && this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout)
                        {
                            if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key.Equals(" "))
                            {
                                DataRow row = this.calculatedColumnDetails.NewRow();
                                row["CalculatedColumn"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Key;
                                row["ColumnFormula"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Formula;
                                row["ColumnDataType"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].DataType;
                                row["ColumnFormat"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Format;
                                row["ColumnCellAppearance"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].CellAppearance.TextHAlign;
                                row["IsChecked"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout;

                                this.calculatedColumnDetails.Rows.Add(row);
                            }
                        }
                    }

                    if (this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].IsBound && !this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden && this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].IsVisibleInLayout && !this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Hidden)
                    {
                        // Added if condition to prevent to append checkbox column
                        if (i > 0)
                        {
                            DataRow row = calculatedColumnValues.NewRow();
                            row["NewColumn"] = "[" + this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[i].Key + "]";
                            calculatedColumnValues.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (this.calculatedColumnDetails.Rows.Count > 0)
            {
                for (int i = 0; i < this.calculatedColumnDetails.Rows.Count; i++)
                {
                    char[] c = { '(' };
                    int d = this.formula.IndexOfAny(c);
                    formula = this.FormatSummaryQuery(this.calculatedColumnDetails.Rows[i].ItemArray[1].ToString()) + " as " + this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString();
                    DataRow row = calculatedColumnValues.NewRow();
                    row["NewColumn"] = this.formula;
                    calculatedColumnValues.Rows.Add(row);
                }
            }

            this.calculatedData = TerraScanCommon.GetXmlString(calculatedColumnValues);

            if (this.calculatedData.Equals("<Root />"))
            {
                this.calculatedData = null;
            }

            this.columnBind = false;
        }
        #endregion Get New Column

        #region SetCurrentRowindex

        /// <summary>
        /// Sets the current rowindex.
        /// </summary>
        /// <param name="keyIndex">Key</param>
        /// <param name="filteredRow">Filtered Row</param>
        private void SetCurrentRowindex(int keyIndex, Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow)
        {
            if (this.selectedRowIndex >= 0)
            {
                this.currentRowKeyId = keyIndex;
                this.currentRowIndex = this.selectedRowIndex;

                this.AddColumnValuesInTheList();

                this.prevRowIndex = this.selectedRowIndex;
                this.QueryEngineGrid.Focus();
                filteredRow[selectedRowIndex].Activate();
                filteredRow[selectedRowIndex].Selected = true;
                this.columnNameInvalid = false;

                this.AddColumnValuesInTheList();
            }
            else
            {
                ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                Infragistics.Win.UltraWinGrid.UltraGridRow[] row = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();

                if (this.keyIdDeleted)
                {
                    if (!this.currentRowIndex.Equals(row.Length))
                    {
                        if (row.Length > 0)
                        {
                            this.RowSelect(row, this.currentRowIndex);
                        }
                        else
                        {
                            //// If Row Count is zero
                            this.currentRowIndex = 0;
                            this.currentRowKeyId = -99;
                            this.keyIdDeleted = false;
                            this.columnNameInvalid = false;
                        }
                    }
                    else
                    {
                        if (row.Length > 0)
                        {
                            this.RowSelect(row, 0);
                        }
                        else
                        {
                            this.currentRowIndex = -1;
                            this.currentRowKeyId = -99;
                            this.keyIdDeleted = false;
                            this.columnNameInvalid = false;
                        }
                    }
                }
                else
                {
                    this.RowSelect(row, 0);
                }
            }
        }

        #endregion SetCurrentRowindex

        #region FillDataSet

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="systemSnapshotId">The system snapshot id.</param>
        /// <param name="systemSnapShotLoadedParameter">if set to <c>true</c> [system snap shot loaded parameter].</param>
        private void FillDataSet(int systemSnapshotId, bool systemSnapShotLoadedParameter)
        {
            int queryViewId;
            queryViewId = Convert.ToInt32(this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryViewIDColumn.ColumnName].ToString());
            string maxRecordCount = null;
            if (!string.IsNullOrEmpty(this.MaxRecordCountTextBox.Text.ToString()))
            {
                try
                {
                    long maxRecord = 0;
                    long.TryParse(this.MaxRecordCountTextBox.Text.ToString().Replace(",", ""), out maxRecord);
                    maxRecordCount = maxRecord.ToString();
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                maxRecordCount = null;
            }

            string keyFilter = TerraScanCommon.GetXmlString(this.keyIdCollection);
            if (keyFilter.Equals("<Root />"))
            {
                keyFilter = null;
            }

            if (this.queryRefreshed) ////Execute if QueryButton has pressed / IsQueryLoad flag is set as true in tts_FormSandwich
            {
                if (!hasChangeQueryView)
                {
                    if (!systemSnapShotLoadedParameter)
                    {
                        this.layoutName = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.LayoutNameColumn].ToString();
                        this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, "false", maxRecordCount);
                    }
                    else if (systemSnapShotLoadedParameter)
                    {
                        this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, 0);
                    }
                }
                else
                {
                    if (this.isFormLoad)
                    {
                        ////if (!systemSnapShotLoadedParameter)
                        ////{
                        ////    this.layoutName = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.LayoutNameColumn].ToString();
                        ////    this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, "false", maxRecordCount);
                        ////}
                        ////else if (systemSnapShotLoadedParameter)
                        ////{
                        ////    this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, 1);
                        ////}
                        if (!systemSnapShotLoadedParameter)
                        {
                            ////Load only column without records - Commented to test
                            //////if (F9030.flagQueryLoad && this.openWithKeyId.Equals(0))
                            //////{
                            //////    this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, "false", maxRecordCount);
                            //////}
                            //////else
                            //////{
                            this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);

                            if (this.queryEngineData.Tables.Count > 1)
                            {
                                this.queryString = this.queryEngineData.Tables[1].Rows[0]["QueryString"].ToString();
                            }
                            //////}
                        }
                        else if (systemSnapShotLoadedParameter)
                        {
                            this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, 0);
                        }
                    }
                    else
                    {
                        if (!systemSnapShotLoadedParameter)
                        {
                            ////Load only column without records
                            this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);

                            if (this.queryEngineData.Tables.Count > 1)
                            {
                                this.queryString = this.queryEngineData.Tables[1].Rows[0]["QueryString"].ToString();
                            }
                        }
                        else if (systemSnapShotLoadedParameter)
                        {
                            this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, keyFilter, 0);
                        }
                    }
                }
            }
            else
            {
                DataTable filterWithKeyTable = this.keyIdCollection.Clone();

                if (this.keyIdCollection.Rows.Count > 0)
                {
                    filterWithKeyTable.Merge(this.keyIdCollection);
                }

                if (this.optionalkeyIdCollection.Rows.Count > 0)
                {
                    filterWithKeyTable.Merge(this.optionalkeyIdCollection);
                }

                ////Remove Duplicate entry
                for (int i = 0; i < filterWithKeyTable.Rows.Count; i++)
                {
                    for (int j = i + 1; j < filterWithKeyTable.Rows.Count; j++)
                    {
                        if (filterWithKeyTable.Rows[i]["KeyId"].ToString() == filterWithKeyTable.Rows[j]["KeyId"].ToString())
                        {
                            filterWithKeyTable.Rows[j].Delete();
                            break;
                        }
                    }
                }

                string filterwithKey = TerraScanCommon.GetXmlString(filterWithKeyTable);

                if (filterwithKey.Equals("<Root />"))
                {
                    filterwithKey = null;
                }

                if (isFormLoad)
                {
                    if (this.openWithKeyId > 0) ////Executes if form has opened from Support form call / using keyid
                    {
                        if (!systemSnapShotLoadedParameter)
                        {
                            ////Load only column without records
                            this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);

                            if (this.queryEngineData.Tables.Count > 1)
                            {
                                this.queryString = this.queryEngineData.Tables[1].Rows[0]["QueryString"].ToString();
                            }
                        }
                        else
                        {
                            this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, 1);
                        }
                    }
                    else
                    {
                        if (!systemSnapShotLoadedParameter)
                        {
                            ////Load only column without records
                            this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);

                            if (this.queryEngineData.Tables.Count > 1)
                            {
                                this.queryString = this.queryEngineData.Tables[1].Rows[0]["QueryString"].ToString();
                            }
                        }
                        else
                        {
                            this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, 1);
                        }
                    }
                }
                else
                {
                    if (!this.hasChangeQueryView)
                    {
                        if (this.keyIdCollection.Rows.Count > 0)
                        {
                            if (!systemSnapShotLoadedParameter)
                            {
                                this.layoutName = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.LayoutNameColumn].ToString();
                                try
                                {
                                    this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, "True", maxRecordCount);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else if (systemSnapShotLoadedParameter)
                            {
                                this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, 1);
                            }
                        }
                        else
                        {
                            if (!systemSnapShotLoadedParameter)
                            {
                                this.layoutName = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.LayoutNameColumn].ToString();
                                try
                                {
                                    this.queryEngineData = this.form9033Control.WorkItem.ListQueryEngineGridFunction(queryViewId, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, "false", maxRecordCount);
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                            else if (systemSnapShotLoadedParameter)
                            {
                                this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, 1);
                            }
                        }
                    }
                    else
                    {
                        if (!systemSnapShotLoadedParameter)
                        {
                            ////Load only column without records
                            this.queryEngineData = this.form9033Control.WorkItem.ListColumns(queryViewId);

                            if (this.queryEngineData.Tables.Count > 1)
                            {
                                this.queryString = this.queryEngineData.Tables[1].Rows[0]["QueryString"].ToString();
                            }
                        }
                        else if (systemSnapShotLoadedParameter)
                        {
                            this.queryEngineData = this.form9033Control.WorkItem.F9033_GetSystemSnapShotRecordSet(systemSnapshotId, this.formMasterNo, this.filterData, this.sortData, this.summaryData, this.calculatedData, filterwithKey, 1);
                        }
                    }
                }
            }

            ////Enable Preview & Excel Buttons
            if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[1].Rows.Count > 1)
            {
                if (Convert.ToInt32(this.queryEngineData.Tables[1].Rows[2][0].ToString().Trim().Split('O').GetValue(0).ToString().Trim()) > Convert.ToInt32(this.queryEngineData.Tables[1].Rows[3][0].ToString()))
                {
                    this.PreviewButton.Enabled = false;
                    this.ExcelButton.Enabled = false;
                }
                else
                {
                    this.PreviewButton.Enabled = true;
                    this.ExcelButton.Enabled = true;
                }
            }
        }

        #endregion FillDataSet

        #region CreateFilterRowDataTable

        /// <summary>
        /// Creates the filter row data table.
        /// </summary>
        /// <returns>Filtered Row</returns>
        private DataTable CreateFilterRowDataTable()
        {
            ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
            Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = QueryEngineGrid.Rows.GetAllNonGroupByRows();
            DataTable filterRowDataTable = new DataTable("NewTable");
            filterRowDataTable = this.queryEngineData.Tables[0].Clone();
            for (int rowIndex = 0; rowIndex < filteredRow.Length; rowIndex++)
            {
                DataRow dr = filterRowDataTable.NewRow();
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(this.keyIdColumnName))
                {
                    #region BUG #777 : FIX

                    //// BUG #777 : FIX
                    //// ISSUE DESCRIPION - EXCEPTION IS THROWN IF WE SAVE THE CUSTOM COLUMN 
                    //// IN THE LAYOUT AS DEFAULT AND REOPEN THE FORM OR REFILTER.
                    //// FIX DESCRPTION - REMOVED ADDING ALL COLUMN EXCEPT KEYID COLUMN

                    dr[this.keyIdColumnName] = filteredRow[rowIndex].Cells[this.keyIdColumnName].Value;
                    filterRowDataTable.Rows.Add(dr);

                    #endregion
                }
            }

            return filterRowDataTable;
        }

        #endregion CreateFilterRowDataTable

        #region PerformCopyAction

        /// <summary>
        /// Performs the copy action.
        /// </summary>
        /// <param name="action">The action.</param>
        private void PerformCopyAction(UltraGridAction action)
        {
            try
            {
                //// The KeyActionMappings will reveal whether or not the 
                //// action is valid based on the current state of the grid. 
                this.QueryEngineGrid.KeyActionMappings.IsActionAllowed(action, (long)this.QueryEngineGrid.CurrentState);
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
        #endregion PerformCopyAction

        #region AddColumnValuesInTheList

        /// <summary>
        /// Adds the column values in the list.
        /// </summary>
        private void AddColumnValuesInTheList()
        {
            if (this.paramCount > 0)
            {
                if (this.QueryEngineGrid.Rows.Count > 0)
                {
                    ////Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                    Infragistics.Win.UltraWinGrid.UltraGridRow[] row = QueryEngineGrid.Rows.GetAllNonGroupByRows();
                    this.parameterList = new List<string>();

                    for (int rows = 1; rows <= paramCount; rows++)
                    {
                        if (row.Length > 0)
                        {
                            string meParameters = row[this.CurrentRowIndex].Cells[rows].Value.ToString();

                            if (!string.IsNullOrEmpty(meParameters))
                            {
                                this.parameterList.Add(meParameters);
                            }
                            else
                            {
                                string meParameter = row[0].Cells[rows].Value.ToString();
                                this.parameterList.Add(meParameter);
                            }
                        }
                    }
                }
            }
        }

        #endregion AddColumnValuesInTheList

        #region ApplyLayout

        /// <summary>
        /// Applies the layout.
        /// </summary>
        /// <param name="layoutXmlString">The layout XML string.</param>
        /// <returns>Layout XML String</returns>
        private string ApplyLayout(string layoutXmlString)
        {
            //// Converts Byte array to MemoryStream
            Encoding enc = Encoding.ASCII;
            Byte[] fileAsByte = enc.GetBytes(layoutXmlString);
            MemoryStream ms = new MemoryStream(fileAsByte);
            MemoryStream returnStream = new MemoryStream();
            try
            {
                this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, PropertyCategories.All);

                Byte[] fileAsByteTemp = enc.GetBytes(layoutXmlString);

                //// Converts Byte array to MemoryStream
                MemoryStream tempStream = new MemoryStream(fileAsByteTemp);
                this.TempQueryGrid.DisplayLayout.LoadFromXml(tempStream, PropertyCategories.All);

                if (this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count > 0)
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Clear();
                    UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                    
                    for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Summaries.Count; i++)
                    {
                        band.Summaries.Add(this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].Key, this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SummaryType, this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Summaries[i].SourceColumn.Key], SummaryPosition.UseSummaryPositionColumn);
                    }
                }

                this.QueryEngineGrid.DisplayLayout.SaveAsXml(returnStream, PropertyCategories.All);

                
            }
            catch (Exception ex)
            {
            }
            return this.ConvertToXML(ms);
        }

        #endregion ApplyLayout

        #region Get Layout value

        /// <summary>
        /// Gets the sort value (sort order & sorted column) and store it in a DataTable.
        /// </summary>
        private void GetSortValue()
        {
            string sortIndicator;
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];

            this.sortDetails.Rows.Clear();
            if (band.SortedColumns.Count > 0)
            {
                if (band.SortedColumns[0].Key.Equals(" "))
                {
                    if (band.SortedColumns.Count > 1)
                    {
                        this.sortKey = band.SortedColumns[1].Key;
                        sortIndicator = band.SortedColumns[1].SortIndicator.ToString();
                        if (sortIndicator.ToString() == "Ascending")
                        {
                            this.sortOrder = "asc";
                        }
                        else
                        {
                            this.sortOrder = "desc";
                        }

                        DataRow row = this.sortDetails.NewRow();
                        row["SortColumn"] = this.sortKey;
                        row["SortOrder"] = this.sortOrder;
                        this.sortDetails.Rows.Add(row);
                    }
                }
                else
                {
                    this.sortKey = band.SortedColumns[0].Key;
                    sortIndicator = band.SortedColumns[0].SortIndicator.ToString();
                    if (sortIndicator.ToString() == "Ascending")
                    {
                        this.sortOrder = "asc";
                    }
                    else
                    {
                        this.sortOrder = "desc";
                    }

                    DataRow row = this.sortDetails.NewRow();
                    row["SortColumn"] = this.sortKey;
                    row["SortOrder"] = this.sortOrder;
                    this.sortDetails.Rows.Add(row);
                }
            }

            this.sortData = TerraScanCommon.GetXmlString(this.sortDetails);

            if (this.sortData.Equals("<Root />"))
            {
                this.sortData = null;
            }
        }

        /// <summary>
        /// Gets the filter value - (filtered column, filter operator & Compare value) and store it in a DataTable.
        /// </summary>
        private void GetFilterValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            this.filterDetails.Rows.Clear();

            if (band.ColumnFilters.Count > 0)
            {
                for (int i = 0; i < band.ColumnFilters.Count; i++)
                {
                    if (band.ColumnFilters[i].FilterConditions.Count > 0)
                    {
                        for (int j = 0; j < band.ColumnFilters[i].FilterConditions.Count; j++)
                        {
                            ////Get Visible column Filter value 
                            if (!this.ColumnChooserGrid.DisplayLayout.Bands[0].Columns[band.ColumnFilters[i].FilterConditions[0].Column.Key].Hidden)
                            {
                                DataRow row = this.filterDetails.NewRow();
                                row["FilterColumn"] = band.ColumnFilters[i].FilterConditions[0].Column.ToString();
                                row["FilterOperator"] = band.ColumnFilters[i].FilterConditions[j].ComparisionOperator.ToString();

                                //if (band.ColumnFilters[i].FilterConditions[j].CompareValue.ToString().Equals("(Blanks)"))
                                //{
                                //    row["FilterValue"] = string.Empty;
                                //}
                                //else
                                //{
                                //// QueryEngine Data : Gets the Row Index with the corresponding keyId

                                if (band.ColumnFilters[i].FilterConditions[j].CompareValue != null)
                                {
                                    if (this.queryEngineData.Tables[0].Columns.Contains(band.ColumnFilters[i].FilterConditions[j].CompareValue.ToString()))
                                    {
                                        row["FilterValue"] = "[" + band.ColumnFilters[i].FilterConditions[j].CompareValue.ToString() + "]";
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(band.ColumnFilters[i].FilterConditions[j].CompareValue.ToString()))
                                        {
                                            row["FilterValue"] = band.ColumnFilters[i].FilterConditions[j].CompareValue.ToString();
                                        }
                                        else
                                        {
                                            row["FilterValue"] = "(Blanks)";
                                        }
                                    }
                                }
                                //}
                                if (band.ColumnFilters[i].FilterConditions.Count > 1)
                                {
                                    row["LogicalOperator"] = band.ColumnFilters[i].LogicalOperator.ToString();
                                }
                                else
                                {
                                    row["LogicalOperator"] = "and";
                                }

                                if (!string.IsNullOrEmpty(band.ColumnFilters[i].FilterConditions[0].Column.Formula))
                                {
                                    row["Expression"] = FormatSummaryQuery(band.ColumnFilters[i].FilterConditions[0].Column.Formula);
                                }
                                if (!band.ColumnFilters[i].FilterConditions[0].Column.ToString().Contains("[hidden]")
                                    && band.ColumnFilters[i].FilterConditions[j].CompareValue != null)
                                {
                                    this.filterDetails.Rows.Add(row);
                                }
                            }
                        }
                    }
                }
            }

            DataView filterView = new DataView(this.filterDetails);

            // set the output column array of the destination table 
            string[] strColumns = { "FilterColumn", "FilterOperator", "FilterValue", "LogicalOperator", "Expression" };

            // true = yes, to get distinct values. 
            DataTable distinctTable = filterView.ToTable(true, strColumns);
            this.filterData = TerraScanCommon.GetXmlString(distinctTable);

            if (this.filterData.Equals("<Root />"))
            {
                this.filterData = null;
            }
        }

        /// <summary>
        /// Gets the summary value -(summary column & summary operator) and store it in a DataTable.
        /// </summary>
        private void GetSummaryValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            int count = band.Summaries.Count;
            bool isDuplicateRecord = false;
            if (band.Summaries.Count > 0)
            {
                for (int i = count - 1; i >= 0; i--)
                {

                    DataRow summaryRow = this.summaryDetails.NewRow();
                    summaryRow["SummaryColumn"] = band.Summaries[i].SourceColumn.Key;
                    summaryRow["SummaryOperator"] = band.Summaries[i].SummaryType.ToString();
                    isDuplicateRecord = false;
                    if (this.summaryDetails.Rows.Count > 0)
                    {
                        int limit = this.summaryDetails.Rows.Count;
                        for (int j = 0; j < limit; j++)
                        {
                            if ((this.summaryDetails.Rows[j].ItemArray[1].ToString() == summaryRow["SummaryOperator"].ToString()) && (this.summaryDetails.Rows[j].ItemArray[0].ToString() == summaryRow["SummaryColumn"].ToString()))
                            {
                                isDuplicateRecord = true;
                            }
                        }
                    }

                    if (isDuplicateRecord == false && !band.Summaries[i].SourceColumn.ToString().Contains("[hidden]"))
                    {
                        this.summaryDetails.Rows.Add(summaryRow);
                    }

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.RemoveAt(i);

                }
            }
            else
            {
                this.summaryDetails.Rows.Clear();
            }

            this.summaryData = TerraScanCommon.GetXmlString(this.summaryDetails);

            if (this.summaryData.Equals("<Root />"))
            {
                this.summaryData = null;
            }
        }

        /// <summary>
        /// Gets the grid design details.
        /// </summary>
        private void GetGridDesignDetails()
        {
            this.designDetailsTable.Rows.Clear();
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            for (int i = 0; i < this.TempQueryGrid.DisplayLayout.Bands[0].Columns.Count; i++)
            {
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()))
                {
                    DataRow row = this.designDetailsTable.NewRow();
                    row["KeyColumnName"] = band.Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Key;
                    row["VisiblePosition"] = band.Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Header.VisiblePosition;
                    row["IsFixed"] = band.Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Header.Fixed;
                    row["IsHidden"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Hidden;
                    this.designDetailsTable.Rows.Add(row);
                }
                else if (!this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].Hidden)
                {
                    DataRow row = this.designDetailsTable.NewRow();
                    row["KeyColumnName"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Key;
                    row["VisiblePosition"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Header.VisiblePosition;
                    row["IsFixed"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Header.Fixed;
                    row["IsHidden"] = this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.TempQueryGrid.DisplayLayout.Bands[0].Columns[i].ToString()].Hidden;
                    this.designDetailsTable.Rows.Add(row);
                }
            }
        }

        #endregion Get Layout value

        #region Bind UltraDataSource
        /// <summary>
        /// Sets the ultra data.
        /// </summary>
        private void SetUltraData()
        {
            if (this.QueryUltraDataSource.Band.Columns.Count > 0)
            {
                this.QueryUltraDataSource.Band.Columns.Clear();
            }
            foreach (DataRow row in this.queryEngineData.Tables[0].Rows)
            {
                foreach (DataColumn col in this.queryEngineData.Tables[0].Columns)
                {
                    if (col.DataType == typeof(System.Decimal))
                    {
                        //test for null here
                        if (row[col] == DBNull.Value)
                        {
                            row[col] = 0.00;
                        }
                    }
                }
            }

            // this.QueryEngineGrid.DataSource = this.QueryUltraDataSource;
            //this.QueryEngineGrid.DisplayLayout.LoadStyle = LoadStyle.LoadOnDemand;

            for (int i = 0; i < this.queryEngineData.Tables[0].Columns.Count; i++)
            {
                if (this.calculatedData != null)
                {
                    bool unboundColumn = false;
                    for (int j = 0; j < this.calculatedColumnDetails.Rows.Count; j++)
                    {
                        if (this.queryEngineData.Tables[0].Columns[i].ToString() == this.calculatedColumnDetails.Rows[j].ItemArray[0].ToString())
                        {
                            unboundColumn = true;
                        }
                    }

                    if (unboundColumn == false)
                    {
                        this.QueryUltraDataSource.Band.Columns.Add(this.queryEngineData.Tables[0].Columns[i].ColumnName, this.queryEngineData.Tables[0].Columns[i].DataType);
                    }
                }
                else
                {
                    this.QueryUltraDataSource.Band.Columns.Add(this.queryEngineData.Tables[0].Columns[i].ColumnName, this.queryEngineData.Tables[0].Columns[i].DataType);
                }

            }

            this.QueryEngineGrid.DataSource = this.queryEngineData;
            this.QueryEngineGrid.DataSource = this.QueryUltraDataSource;
            //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.Caption = " ";

            for (int colCount = 0; colCount < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; colCount++)
            {
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].DataType.Equals(typeof(System.Decimal)))
                {

                    Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
                    appearance1.ForeColor = System.Drawing.Color.FromArgb(0, 130, 0);
                    Infragistics.Win.ConditionValueAppearance conditionValueAppearance1 = new Infragistics.Win.ConditionValueAppearance(new Infragistics.Win.ICondition[] {
                    ((Infragistics.Win.ICondition)(new Infragistics.Win.OperatorCondition(Infragistics.Win.ConditionOperator.LessThan     , 0, false, typeof(decimal ))))}, new Infragistics.Win.Appearance[] {
                    appearance1});
                    conditionValueAppearance1.ApplyAllMatchingConditions = false;

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].ValueBasedAppearance = conditionValueAppearance1;

                    #region
                    //TSBG 11005 Subfund Management: Query View not showing enough decimal places for Rate
                    string s = this.queryViewTable.Rows[this.QueryViewCombo.SelectedIndex][this.queryViewTable.QueryViewColumn.ColumnName].ToString();

                    if (s == "vFM_11005_SubFund" && Convert.ToString(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount]) == "Rate")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].Format = "#,##0.0000000000##;(#,##0.0000000000##);0.0000000000##";

                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[colCount].Format = "#,##0.00##;(#,##0.00##);0.00##";
                    }
                    #endregion
                }
            }


            this.QueryEngineGrid.DisplayLayout.LoadStyle = LoadStyle.LoadOnDemand;
            this.QueryUltraDataSource.Rows.Clear();
            RefreshRow rs = RefreshRow.ReloadData;
            this.QueryUltraDataSource.Rows.SetCount(this.queryEngineData.Tables[0].Rows.Count);
            this.QueryEngineGrid.Rows.Refresh(rs);
            this.columnBind = true;
        }

        /// <summary>
        /// Resets the ultra data.
        /// </summary>
        private void ResetUltraData()
        {
            this.QueryEngineGrid.DataSource = this.QueryUltraDataSource;
            this.QueryUltraDataSource.Rows.Clear();
            RefreshRow rs = RefreshRow.ReloadData;

            this.QueryUltraDataSource.Rows.SetCount(this.queryEngineData.Tables[0].Rows.Count);

            this.QueryEngineGrid.Rows.Refresh(rs);
        }

        #endregion Bind UltraDataSource

        #region Set Layout Value

        /// <summary>
        /// Sets the new column value.
        /// </summary>
        private void SetNewColumnValue()
        {
            for (int i = 0; i < this.calculatedColumnDetails.Rows.Count; i++)
            {
                if (!this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()))
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Add(this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString());
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].Formula = this.calculatedColumnDetails.Rows[i].ItemArray[1].ToString();

                    if (this.calculatedColumnDetails.Rows[i].ItemArray[2].ToString() == "System.Int32")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].DataType = typeof(System.Decimal);
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].Format = this.calculatedColumnDetails.Rows[i].ItemArray[3].ToString();
                    }
                    else if (this.calculatedColumnDetails.Rows[i].ItemArray[2].ToString() == "System.String")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].DataType = typeof(System.String);
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].DataType = typeof(System.DateTime);
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].Format = this.calculatedColumnDetails.Rows[i].ItemArray[3].ToString();
                    }

                    if (this.calculatedColumnDetails.Rows[i].ItemArray[4].ToString() == "Left")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Left;
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Left;
                    }
                    else if (this.calculatedColumnDetails.Rows[i].ItemArray[4].ToString() == "Center")
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Center;
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Center;
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Right;
                        this.TempQueryGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].CellAppearance.TextHAlign = HAlign.Right;
                    }

                    if (this.calculatedColumnDetails.Rows[i].ItemArray[5].ToString().Equals("True"))
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].Hidden = false;
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[i].ItemArray[0].ToString()].Hidden = true;
                    }
                }
            }

            for (int columnCount = 0; columnCount < this.calculatedColumnDetails.Rows.Count; columnCount++)
            {
                int col = 0;
                for (int colcount = 0; colcount < this.queryEngineData.Tables[0].Columns.Count; colcount++)
                {
                    if (this.calculatedColumnDetails.Rows[columnCount].ItemArray[0].ToString() == this.queryEngineData.Tables[0].Columns[colcount].ToString())
                    {
                        col = colcount;
                    }
                }

                for (int j = 0; j < this.QueryEngineGrid.Rows.Count; j++)
                {
                    try
                    {
                        this.QueryEngineGrid.Rows[j].Cells[this.calculatedColumnDetails.Rows[columnCount].ItemArray[0].ToString()].Value = this.queryEngineData.Tables[0].Rows[j].ItemArray[col].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[this.calculatedColumnDetails.Rows[columnCount].ItemArray[0].ToString()].DataType = typeof(System.String);
                        this.QueryEngineGrid.Rows[j].Cells[this.calculatedColumnDetails.Rows[columnCount].ItemArray[0].ToString()].Value = this.queryEngineData.Tables[0].Rows[j].ItemArray[col].ToString();
                        ////MessageBox.Show("Input for New column is not in a correct format", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the summary value. Show only refreshed Data
        /// </summary>
        private void SetSummaryValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            int count = band.Summaries.Count;

            if (count > 0)
            {
                this.GetSummaryValue();
            }

            if (this.summaryDetails.Rows.Count > 0 && this.QueryEngineGrid.DisplayLayout.Bands[0].Summaries.Count == 0)
            {
                string sumValue = string.Empty;
                int sumPosition = 0;
                for (int sumCount = 0; sumCount < this.summaryDetails.Rows.Count; sumCount++)
                {
                    ////Remove Duplicate entry
                    for (int i = 0; i < this.summaryDetails.Rows.Count; i++)
                    {
                        for (int j = i + 1; j < this.summaryDetails.Rows.Count; j++)
                        {
                            if (this.summaryDetails.Rows[i]["SummaryColumn"].ToString() == this.summaryDetails.Rows[j]["SummaryColumn"].ToString() && this.summaryDetails.Rows[i]["SummaryOperator"].ToString() == this.summaryDetails.Rows[j]["SummaryOperator"].ToString())
                            {
                                this.summaryDetails.Rows[j].Delete();
                                break;
                            }
                        }
                    }

                    string sumKey = this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() + '@' + this.summaryDetails.Rows[sumCount].ItemArray[0].ToString();
                    SummaryType type;
                    ////  SummarySettings summary = band.Summaries.Add(sumKey, SummaryType.Custom, new OrderTotalsSummary(), band.Columns[summaryDetails.Rows[sumCount].ItemArray[0].ToString()], SummaryPosition.UseSummaryPositionColumn, null);
                    if (this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Sum")
                    {
                        type = SummaryType.Sum;
                    }
                    else if (this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Average")
                    {
                        type = SummaryType.Average;
                    }
                    else if (this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Minimum")
                    {
                        type = SummaryType.Minimum;
                    }
                    else if (this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() == "Maximum")
                    {
                        type = SummaryType.Maximum;
                    }
                    else
                    {
                        type = SummaryType.Count;
                    }

                    if (this.queryEngineData.Tables[0].Columns.Contains(this.summaryDetails.Rows[sumCount].ItemArray[0].ToString()))
                    {
                        Infragistics.Win.UltraWinGrid.SummarySettings TotalSummary = band.Summaries.Add(sumKey, type, band.Columns[this.summaryDetails.Rows[sumCount].ItemArray[0].ToString()], SummaryPosition.UseSummaryPositionColumn);
                        if (this.queryEngineData.Tables.Count > 3)
                        {
                            for (int i = 0; i < this.queryEngineData.Tables[3].Columns.Count; i++)
                            {
                                //// int rowIndex = sumPosition
                                string sum = band.Summaries[sumCount].SummaryType.ToString() + '@' + band.Summaries[sumCount].SourceColumn.ToString();
                                if (this.queryEngineData.Tables[3].Columns[i].ColumnName.ToString() == sum)
                                {
                                    sumValue = this.queryEngineData.Tables[3].Rows[0].ItemArray[i].ToString();
                                }
                            }

                            TotalSummary.DisplayFormat = this.summaryDetails.Rows[sumCount].ItemArray[1].ToString() + " = " + sumValue;
                        }
                    }
                    else
                    {
                        sumPosition = sumPosition + 1;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the filter value.
        /// </summary>
        private void SetFilterValue()
        {
            UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
            band.ColumnFilters.ClearAllFilters();
            DataSet filterSet = new DataSet();
            StringReader stringReader = new StringReader(this.filterData);
            System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
            filterSet.ReadXml(textReader);
            for (int i = 0; i < filterSet.Tables[0].Rows.Count; i++)
            {
                if (this.queryEngineData.Tables[0].Columns.Contains(filterSet.Tables[0].Rows[i].ItemArray[0].ToString()))
                {
                    FilterComparisionOperator compOperator = FilterComparisionOperator.StartsWith;
                    switch (filterSet.Tables[0].Rows[i].ItemArray[1].ToString())
                    {
                        case "Equals":
                            compOperator = FilterComparisionOperator.Equals;
                            break;
                        case "NotEquals":
                            compOperator = FilterComparisionOperator.NotEquals;
                            break;
                        case "LessThan":
                            compOperator = FilterComparisionOperator.LessThan;
                            break;
                        case "LessThanOrEqualTo":
                            compOperator = FilterComparisionOperator.LessThanOrEqualTo;
                            break;
                        case "GreaterThan":
                            compOperator = FilterComparisionOperator.GreaterThan;
                            break;
                        case "GreaterThanOrEqualTo":
                            compOperator = FilterComparisionOperator.GreaterThanOrEqualTo;
                            break;
                        case "Like":
                            compOperator = FilterComparisionOperator.Like;
                            break;
                        case "Contains":
                            compOperator = FilterComparisionOperator.Contains;
                            break;
                        case "DoesNotContain":
                            compOperator = FilterComparisionOperator.DoesNotContain;
                            break;
                        case "Match":
                            compOperator = FilterComparisionOperator.Match;
                            break;
                        case "DoesNotMatch":
                            compOperator = FilterComparisionOperator.DoesNotMatch;
                            break;
                        case "StartsWith":
                            compOperator = FilterComparisionOperator.StartsWith;
                            break;
                        case "EndsWith":
                            compOperator = FilterComparisionOperator.EndsWith;
                            break;
                        case "DoesNotStartWith":
                            compOperator = FilterComparisionOperator.DoesNotStartWith;
                            break;
                        case "DoesNotEndWith":
                            compOperator = FilterComparisionOperator.DoesNotEndWith;
                            break;
                        case "NotLike":
                            compOperator = FilterComparisionOperator.NotLike;
                            break;
                    }

                    band.ColumnFilters[filterSet.Tables[0].Rows[i].ItemArray[0].ToString()].FilterConditions.Add(compOperator, filterSet.Tables[0].Rows[i].ItemArray[2].ToString());

                    // Code added for Bug #5781: TSBG - 9033 Query Engine - Multiple Filter Criteria
                    FilterLogicalOperator logicOperator;
                    if (filterSet.Tables[0].Rows[i].ItemArray[3].ToString().Equals("Or"))
                    {
                        logicOperator = FilterLogicalOperator.Or;
                    }
                    else
                    {
                        logicOperator = FilterLogicalOperator.And;
                    }

                    band.ColumnFilters[filterSet.Tables[0].Rows[i].ItemArray[0].ToString()].LogicalOperator = logicOperator;
                    // Ends here
                }
            }
        }

        /// <summary>
        /// Sets the sort order.
        /// </summary>
        private void SetSortOrder()
        {
            if (this.sortData != null)
            {
                UltraGridBand band = this.QueryEngineGrid.DisplayLayout.Bands[0];
                DataSet sortValue = new DataSet();
                StringReader stringReader = new StringReader(this.sortData);
                System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                sortValue.ReadXml(textReader);
                bool sort;
                if (sortValue.Tables[0].Rows[0].ItemArray[1].ToString() == "asc")
                {
                    sort = false;
                }
                else
                {
                    sort = true;
                }

                band.SortedColumns.Add(sortValue.Tables[0].Rows[0].ItemArray[0].ToString(), sort);
            }
        }

        /// <summary>
        /// Sets the grid design details.
        /// </summary>
        private void SetGridDesignDetails()
        {
            if (this.designDetailsTable.Rows.Count > 0)
            {
                for (int i = 0; i < this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Count; i++)
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[i].Hidden = true;
                }
            }

            DataView defaultView = new DataView(this.designDetailsTable);
            defaultView.Sort = "VisiblePosition ASC";
            DataRowView dataRow;
            for (int rowIndex = 0; rowIndex < defaultView.Count; rowIndex++)
            {
                dataRow = defaultView[rowIndex];
                if (this.QueryEngineGrid.DisplayLayout.Bands[0].Columns.Exists(dataRow["KeyColumnName"].ToString()))
                {
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[dataRow["KeyColumnName"].ToString()].Header.VisiblePosition = Convert.ToInt32(dataRow["VisiblePosition"]);
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[dataRow["KeyColumnName"].ToString()].Header.Fixed = Convert.ToBoolean(dataRow["IsFixed"]);
                    this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[dataRow["KeyColumnName"].ToString()].Hidden = Convert.ToBoolean(dataRow["IsHidden"]);
                }
            }
        }

        #endregion Set Layout Value

        #region Regular Expression
        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInteger(string value)
        {
            return IsMatch(value, @"^([0-9]*|[0-9]*(\.[0-9])[0-9]*)$");
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
        #endregion Regular Expression

        #region Selected Layout Details
        /// <summary>
        /// Sets the selected layout.
        /// </summary>
        private void SetSelectedLayout()
        {
            if (this.QueryLayoutCombo.SelectedIndex >= 0)
            {
                this.selectedLayoutXml = this.listQueryLayout.Rows[this.QueryLayoutCombo.SelectedIndex][this.listQueryLayout.LayoutXMLColumn.ColumnName].ToString();

                DataSet ds = new DataSet();

                if (!string.IsNullOrEmpty(this.selectedLayoutXml))
                {
                    System.Text.Encoding enc = System.Text.Encoding.ASCII;

                    //// Gets the String as Byte array
                    Byte[] fileAsByte = enc.GetBytes(this.selectedLayoutXml);

                    //// Converts Byte array to MemoryStream
                    MemoryStream ms = new MemoryStream(fileAsByte);

                    //// Loads the Layout from XML Data
                    this.QueryEngineGrid.DisplayLayout.LoadFromXml(ms, Infragistics.Win.UltraWinGrid.PropertyCategories.All);

                    this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
                    this.AddRemoveTemCol();

                    this.defaultXmlString = this.selectedLayoutXml;

                    this.queryLoadFlag = true;
                    this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);

                    this.OnFilterRowSelect();

                    if (this.queryEngineData.Tables.Count > 1 && this.queryEngineData.Tables[0].Rows.Count > 0)
                    {
                        this.totalRecords = this.queryEngineData.Tables[0].Rows.Count;
                    }

                    this.GetNumberOfRecords(false);
                    this.NoOfRecords.Visible = true;
                    this.LayoutFieldLabel.Text = "Layout - " + this.layoutName;
                }
            }
        }

        /// <summary>
        /// Gets the layout details.
        /// </summary>
        private void GetLayoutDetails()
        {
            this.GetNewColumnValue();
            this.GetFilterValue();
            this.GetSortValue();
            this.GetSummaryValue();
            this.GetGridDesignDetails();
        }

        /// <summary>
        /// Sets the layout details.
        /// </summary>
        private void SetLayoutDetails()
        {
            this.SetNewColumnValue();

            if (this.sortData != null)
            {
                this.SetSortOrder();
            }

            if (this.filterData != null)
            {
                this.SetFilterValue();
            }

            this.SetGridDesignDetails();

            this.QueryEngineGrid.DisplayLayout.Bands[0].Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            this.SetSummaryValue();
            this.QueryEngineGrid.DisplayLayout.SaveAsXml(this.resetLayout, PropertyCategories.All);
        }

        #endregion Selected Layout Details

        #region F5 key Functionality
        /// <summary>
        /// Processes the CMD key.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="keyData">The key data.</param>
        /// <returns>Boolean</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            ////F5 keypress invoke Refresh functionality
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData.Equals(Keys.F5))
                {
                    this.RefreshButton_Click(this.RefreshButton, null);
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion region F5 key Functionality

        #region Form Close

        /// <summary>
        /// Closes the form.
        /// </summary>
        private void CloseForm()
        {
            if (this.QueryEngineGrid.Rows.Count > 0)
            {
                ////Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetFilteredInNonGroupByRows();
                Infragistics.Win.UltraWinGrid.UltraGridRow[] filteredRow = this.QueryEngineGrid.Rows.GetAllNonGroupByRows();
                this.resetLinkVisible = this.ResetLink.Visible;

                if (this.QueryEngineGrid.ActiveRow != null && this.QueryEngineGrid.ActiveRow.Index >= 0)
                {
                    if (this.queryEngineData.Tables[0].Columns.Contains(this.keyIdColumnName))
                    {
                        this.currentRowKeyId = Convert.ToInt32(this.QueryEngineGrid.ActiveRow.Cells[this.keyIdColumnName].Value);
                    }
                }

                if (filteredRow.Length > 0)
                {
                    //// FINDS THE KEYID IN THE FILTERED GRID
                    for (int rowIndex = 0; rowIndex < filteredRow.Length; rowIndex++)
                    {
                        bool found;

                        //// CHECKS THE KEYID WITH THE CURRENT ROW
                        if (this.currentRowKeyId.Equals(Convert.ToInt32(filteredRow[rowIndex].Cells[this.keyIdColumnName].Value.ToString())))
                        {
                            this.selectedRowIndex = rowIndex;
                            found = true;
                            break;
                        }
                        else
                        {
                            found = false;
                            this.selectedRowIndex = -1;
                        }
                    }
                }
            }
            else
            {
                this.selectedRowIndex = -1;
                this.currentRowKeyId = -99;
                this.totalRecords = 0;
            }

            this.currentRowIndex = this.selectedRowIndex;

            if (this.QueryEngineGrid.Layouts.Exists("CurrentLayout"))
            {
                this.QueryEngineGrid.Layouts["CurrentLayout"].SaveAsXml(this.currentLayout, PropertyCategories.All);
            }
            else
            {
                this.QueryEngineGrid.Layouts.Add("CurrentLayout");
                this.QueryEngineGrid.Layouts["CurrentLayout"].SaveAsXml(this.currentLayout, PropertyCategories.All);
            }

            //// EVENTHOUGH ITS NOT FROM THE MENU, THE CURRENTROWKEYID = -99. JUST TO DISTINCT BOTH
            //// I HAVE PLACED A BOOL. 
            //// INSTEAD OF ONFILTERROWSELECT(), HERE I HAVE USED ROWSELECT(), 
            //// TO IMPLEMENT SORTING AND REVERSESORTING.

            this.notFromMenu = true;
            this.RowSelect(this.currentRowKeyId, this.keyIdColumnName);
            this.notFromMenu = false;

            this.D9030_F9033_QueryEngineClose(this, new TerraScan.Infrastructure.Interface.EventArgs<int>(this.formMasterNo));
        }

        #endregion Form Close

        #region Combo Tooltip Method
        /// <summary>
        /// Checks the length of the combo text.
        /// </summary>
        /// <param name="comboString">The combo string.</param>
        /// <returns>Flag for exceeding Combo Text</returns>
        private bool CheckComboTextLength(string comboString)
        {
            Graphics graphics = this.CreateGraphics();
            ////Get the width of the string
            SizeF fontSize = graphics.MeasureString(comboString, this.Font);

            if (fontSize.Width > 179)
            {
                graphics.Dispose();
                return true;
            }

            graphics.Dispose();
            return false;
        }
        #endregion Combo Tooltip Method

        #region Button Visibility

        /// <summary>
        /// Sets the button visibility.
        /// Method added for CO:#1457
        /// </summary>
        private void SetButtonVisibility()
        {
            // Set Button visibility
            this.AnalyticsButton.Visible = F9030.isAnalyticsVisible;
            this.ReplaceButton.Visible = F9030.isUpdateVisible;

            // If Update Button is not visible, Move Analytics Button on that position
            if (this.AnalyticsButton.Visible && !this.ReplaceButton.Visible)
            {
                this.AnalyticsButton.Left = this.CloseButton.Left - (this.CloseButton.Width + 36);
            }

        }

        #endregion Button Visibility

        private void SetCheckBoxPinningLayout()
        {
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].AllowRowSummaries = AllowRowSummaries.False;
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.FixedHeaderIndicator = FixedHeaderIndicator.None;
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.Fixed = true;
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Header.VisiblePosition = 0;
            this.QueryEngineGrid.DisplayLayout.Override.FixedRowIndicator = Infragistics.Win.UltraWinGrid.FixedRowIndicator.None;
            this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].Width = 34;
        }

        private void QueryEngineGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                if (this.QueryEngineGrid.DisplayLayout != null && e.Cell.Row.Index >= 0)
                {
                    UltraGridRow currentRow = e.Cell.Row;
                    bool changeValue = false;
                    if (e.Cell.Text.ToLower().Equals("true"))
                    {
                        changeValue = true;
                    }
                    //bool.TryParse(e.Cell.Value.ToString(), out changeValue);

                    this.isPinned = true;
                    this.QueryEngineGrid.UpdateData();
                    if (e.Cell.Value.Equals(false))
                    {
                        e.Cell.Row.Appearance.BackColor = e.Cell.Appearance.BackColor;
                    }

                    // this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].SortIndicator = SortIndicator.None;
                    this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti;

                    if (this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Contains(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0]))
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Remove(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0]);
                        this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Add(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0], true, true);
                    }
                    else
                    {
                        this.QueryEngineGrid.DisplayLayout.Bands[0].SortedColumns.Add(this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0], true, true);
                    }

                    //this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
                    this.QueryEngineGrid.Refresh();
                    //this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
                    //this.QueryEngineGrid.DisplayLayout.Bands[0].Columns[0].SortIndicator = SortIndicator.None;

                    for (int i = 0; i < this.QueryEngineGrid.Rows.Count; i++)
                    {
                        if (this.QueryEngineGrid.Rows[i].Cells.Count > 0 && this.QueryEngineGrid.Rows[i].Cells[0].Text.ToLower().Equals("true"))
                        {
                            if ((i % 2) == 0)
                            {
                                this.QueryEngineGrid.Rows[i].Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(214)))), ((int)(((byte)(230)))));
                            }
                            else
                            {
                                this.QueryEngineGrid.Rows[i].Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(194)))), ((int)(((byte)(218)))));
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
                this.isPinned = false;
            }
        }

        #endregion PrivateMethods

        private void QueryEngineGrid_BeforeSortChange(object sender, BeforeSortChangeEventArgs e)
        {
            if (!this.isPinned)
            {
                this.QueryEngineGrid.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.ExternalSortSingle;
            }
        }

        private void ExcelExporter_CellExported(object sender, CellExportedEventArgs e)
        {
            if (e.GridColumn.DataType.Equals(typeof(Boolean)))
            {
                if (e.Value.ToString().ToUpper().Equals("FALSE"))
                {
                    e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex].Value = "FALSE";
                }
                else
                {
                    e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex].Value = "TRUE";
                }
            }
        }
    }
}