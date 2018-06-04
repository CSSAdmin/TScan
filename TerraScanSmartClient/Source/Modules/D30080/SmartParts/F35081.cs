namespace D30080
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
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.Infrastructure.Interface.Constants;
    using Infrastructure.Interface;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using System.IO;
    using System.Globalization;

    [SmartPart]
    public partial class F35081 : BaseSmartPart, IUIElementDrawFilter
    {
       
        #region Variables

        private int masterFormNo;
        private bool formMasterPermissionEdit;
        /// <summary>
        /// navigationFlag
        /// </summary>
        private bool navigationFlag;
        /// <summary>
        /// CentralID
        /// </summary>
        private int? CentralID;
        /// <summary>
        /// centralXMLItem
        /// </summary>
        private string centralXMLItem;
        /// <summary>
        /// removeXMLItem
        /// </summary>
        private string removeXMLItem;
        /// <summary>
        /// personalProperty
        /// </summary>
        private decimal personalProperty;
        /// <summary>
        /// realProperty
        /// </summary>
        private decimal realProperty;
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
        /// Used to store the sectionIndicatorText
        /// </summary>
        private string sectionIndicatorText;

        private int yaxisPoint;
        /// <summary>
        /// basePanelScrolled variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool basePanelScrolled;

        /// <summary>
        /// ColumnName
        /// </summary>
        private string columnName = string.Empty;

        /// <summary>
        /// columnIndex
        /// </summary>
        private int columnindex;

        private bool flagLoadForm = false;
        private bool QueryLoadForm = false;

        /// <summary>
        /// stategridheight
        /// </summary>
        private int stategridheight = 40;

        /// <summary>
        /// staterowcount
        /// </summary>
        private int staterowcount = 0;

        /// <summary>
        /// rollYear
        /// </summary>
        private string rollYear;
        private string parcelNumber;
        private string companyName;
        private string companyNumber;
        private string baseLine;
        private string ownerID;
        private string propertyClassID;
        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;
        private string subFundID;

        private string fundDescription = string.Empty;
        private bool isDeleted = false;

        private F35081Controller form35081Control;
        private string CentralXML = string.Empty;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        F35081CentralAssessedGridData CentralGridDataSet = new F35081CentralAssessedGridData();
        F35081CentralAssessedGridData.f35081_GetCentrallyAssessedItemRateDataTable CentralGridRateDataTable = new F35081CentralAssessedGridData.f35081_GetCentrallyAssessedItemRateDataTable();
        F35080CentralAssessedOwner OwnerDataSet = new F35080CentralAssessedOwner();

        F35081CentralAssessedGridData.CentralAssessmentListDataTable CustomParameterTable = new F35081CentralAssessedGridData.CentralAssessmentListDataTable();

        DataSet CentralXMLDataSet = new DataSet("root");

        DataTable GridDatatTable = new DataTable();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="F35081"/> class.
        /// </summary>
        public F35081()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F35081"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F35081(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.CentralID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.Tag = formNo;
            this.sectionIndicatorText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.CentralAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CentralAssessedPictureBox.Height, this.CentralAssessedPictureBox.Width, sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
            
        }

        #region Event Publication

        /// <summary>
        /// Declare the event FormSlice_SectionIndicatorClick        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;


        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;
        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        /// <summary>
        /// Event publication to alert the resizable slices
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F95005_FomrMasterCancel, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> D9030_F95005_FomrMasterCancel;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_NullRecordModeAfterDelete, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceNullRecordModeEventArgs>> FormSlice_NullRecordModeAfterDelete;

        /// <summary> 
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        /// <summary>
        /// Declare the event FormSlice_Resize        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

        /// <summary>
        /// Declare the event FormSlice_FormCloseAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Declare the event FormSlice_EditEnabled        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SetViewMode, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_SetViewMode;


        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the form29610 control.
        /// </summary>
        /// <value>The form29610 control.</value>
        [CreateNew]
        public F35081Controller Form35081Control
        {
            get { return this.form35081Control as F35081Controller; }
            set { this.form35081Control = value; }
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
            if (this != null && this.IsDisposed != true)
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;

                    ////this.StateAssessesOwnerDataSet = new F35075StateAssessedData();
                    if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
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
        }

        /// <summary>
        /// Event Subscription for enable new method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this.slicePermissionField.newPermission)
                {
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Clear();
                    if (this.CentralXMLDataSet.Tables.Count > 0)
                    {
                        for (int i = 0; i < this.CentralXMLDataSet.Tables.Count; i++)
                        {
                            this.CentralXMLDataSet.Tables[i].Clear();
                            this.CentralXMLDataSet.Tables.Remove(this.CentralXMLDataSet.Tables[i]);
                            i = i - 1;
                            //this.CentralXMLDataSet.AcceptChanges();
                        }
                    }
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                    this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
                    if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Count == 0)
                    {
                        DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);

                        //Modified
                        if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                        {
                            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                        }
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                        this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                    }
                    this.SetSmartPartHeight();
                    this.EnableControls(true);
                    this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    // this.CentralID = -99;
                    // this.rollYear = 0;
                }

                else
                {
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Clear();
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DefaultView;
                    this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
                    this.SetSmartPartHeight();
                    this.pageMode = TerraScanCommon.PageModeTypes.New;
                    /// this.EnableControls(false);
                }
                this.ClearBottomPanelDetails();
            }
            catch(Exception ex)
            {

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
            // this.StateAssessedGrid.DisplayLayout.Bands[0].Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free;
            // this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.ParcelNumberColumn.ColumnName].Width = 150;
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {                
                this.FlagSliceForm = true;
                this.CentralID = eventArgs.Data.SelectedKeyId;
                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Clear();
                this.CentralGridDataSet = this.form35081Control.WorkItem.F35081_CentralAssessedGridDetails(Convert.ToInt32(this.CentralID));
                this.LoadBottomPanelDetails();
                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                {
                    this.EnableControls(true);                   
                    for (int i = 0; i < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count; i++)
                    {
                        //Modifed
                        if (string.IsNullOrEmpty(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[i][1].ToString()))
                        {
                            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[i][1] = "<<< >>>";
                        }
                    }
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                    this.SetSmartPartHeight();
                    this.CentralAssessedGrid.Focus();
                }
                else
                {
                    
                    this.FlagSliceForm = true;
                    if (isDeleted == false)
                    {
                        if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count == 0)
                        {
                            DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                            //Modifed
                            if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                            {
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                            }
                            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                            this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                        }
                        this.SetSmartPartHeight();
                    }
                    else
                    {
                        this.EnableControls(false);
                        this.MainPanel.Enabled = false;
                    }
                }
                if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                {
                    if (isDeleted)
                    {
                        this.MainPanel.Enabled = false;
                        this.CentralAssessedGrid.Enabled = false;
                        isDeleted = false;
                    }
                    this.PermissionControlLock(false);
                }
                else
                {
                    this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                }
                //if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                //{
                //    if (isDeleted)
                //    {
                //        this.MainPanel.Enabled = false;
                //        this.CentralAssessedGrid.Enabled = false;
                //        this.PermissionControlLock(true);
                //        isDeleted = false;
                //    }
                //    else
                //    {
                //        this.PermissionControlLock(false);
                //    }
                //}
                //else
                //{
                //    this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                //}
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }

        }
        private Color ParseColorfromString(string rgbValue)
        {
             string  colour=null;
             string[] array = rgbValue.Split(',');
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
                 return foreColor;                 
             }
             return Color.Black;
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
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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

        [EventSubscription(EventTopicNames.D84700_F84722_OnSave_SetKeyId, ThreadOption.UserInterface)]
        public void FormSlice_OnSave_GetKeyId(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true && this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                        {
                            this.CentralID = eventArgs.Data.SelectedKeyId;
                            if (this.CentralID == -99)
                            {
                                this.CentralID = null;
                            }
                           
                                DataView view = new DataView(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem);
                                ArrayList rowList = new ArrayList();
                                DataTable dt = view.ToTable(false, new string[] { "CentralItemID", "FundID", "PersonalProperty", "RealProperty", "Total", "Description" });
                                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                                {
                                    if (dt.Rows[i][1].ToString().Contains("<<< >>>") || (string.IsNullOrEmpty(dt.Rows[i][1].ToString())) && string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && (string.IsNullOrEmpty(dt.Rows[i][3].ToString())))
                                    {
                                        dt.Rows[i].Delete();
                                    }
                                }
                                dt.AcceptChanges();
                                
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {                                   
                                    if (dt.Rows[i][1].ToString().Contains("<<< >>>"))
                                    {
                                        dt.Rows[i][1] = "";
                                        dt.AcceptChanges();
                                    }
                                }
                                
                                string xmlStr = ConvertToXML(dt);
                                dt.Clear();                                
                                if (!string.IsNullOrEmpty(xmlStr) && !xmlStr.Equals("<root />"))
                                {
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Clear();
                                    this.form35081Control.WorkItem.F35081_InsertOwnerAssessedGrid(xmlStr, Convert.ToInt32(this.CentralID), TerraScanCommon.UserId);
                                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                                }
                            
                            
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

        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            this.isDeleted = true;
            if (this.slicePermissionField.deletePermission)
            {
                this.Cursor = Cursors.WaitCursor;
                 this.form35081Control.WorkItem.F35080_DeleteOwnerDetails(Convert.ToInt32(this.CentralID), TerraScanCommon.UserId);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.Cursor = Cursors.Default;
                SliceNullRecordModeEventArgs sliceEventArgs = new SliceNullRecordModeEventArgs();
                sliceEventArgs.MasterFormNo = this.masterFormNo;
                sliceEventArgs.AllowNullRecordMode = false;
                sliceEventArgs.WithoutKeyId = false;
                this.FormSlice_NullRecordModeAfterDelete(this, new DataEventArgs<SliceNullRecordModeEventArgs>(sliceEventArgs));
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
            this.performCancel();
           
            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
            {
              this.EnableControls(true);
              //this.ClearBottomPanelDetails();
            }
            else
            {
                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Count == 0)
                {
                    DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                    //Modifed
                    if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                    {
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                    }
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                }
                this.SetSmartPartHeight();
                this.EnableControls(true);
                //this.EnableControls(false);
                //this.MainPanel.Enabled = false;
                //this.ClearBottomPanelDetails();

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
        /// Called when [D9030_ F9030_ alert resizable slice].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F95005_AlertFomrMasterCancel(DataEventArgs<int> eventArgs)
        {
            if (this.D9030_F95005_FomrMasterCancel != null)
            {
                this.D9030_F95005_FomrMasterCancel(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            try
            {
                if (this.FormSlice_Resize != null)
                {
                    this.FormSlice_Resize(this, eventArgs);
                }
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion
        private string ConvertToXML(DataTable table)
        {
            DataTable dt = table.Copy();
            dt.Namespace = "";
            dt.TableName = "CentrallyAssessedItems";
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            xmlstr = xmlstr.Replace("DocumentElement", "root");
            return (xmlstr);
        }

        private void LoadBottomPanelDetails()
        {
            try
            {
                if (this.CentralID > 0)
                {
                    this.OwnerDataSet = this.form35081Control.WorkItem.F35080_CentralAssessedOwnerDetails(Convert.ToInt32(this.CentralID));
                    if (this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows.Count > 0)
                    {
                        this.AddCentralOwnerRowDetails();
                       // this.LoadCentralOwnerRowDetails();
                        //short.TryParse(this.OwnerDataSet.f35080CentrallyAssessedOwner[0]["RollYear"].ToString(), out rollYear);

                        if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CountyGeneralTotalColumn].ToString()))
                        {
                            var strTemp = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CountyGeneralTotalColumn].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("$#,##0.##", CultureInfo.InvariantCulture);
                            this.CountyGeneralTotalTextBox.Text = strResult;
                            if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CountyGeneralTotalRGBColumn].ToString()))
                            {
                                string rgbColour = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CountyGeneralTotalRGBColumn].ToString();
                                this.CountyGeneralTotalTextBox.ForeColor = ParseColorfromString(rgbColour);
                            }
                        }
                        else
                        {
                            this.CountyGeneralTotalTextBox.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolGeneralTotalColumn].ToString()))
                        {
                            var strTemp = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolGeneralTotalColumn].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("$#,##0.##", CultureInfo.InvariantCulture);
                            this.SchoolGeneralTotalTextBox.Text = strResult;
                            if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolGeneralTotalRGBColumn].ToString()))
                            {
                                string rgbColour = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolGeneralTotalRGBColumn].ToString();

                                this.SchoolGeneralTotalTextBox.ForeColor = ParseColorfromString(rgbColour);

                            }
                        }
                        else
                        {
                            this.SchoolGeneralTotalTextBox.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolBondTotalColumn].ToString()))
                        {
                            var strTemp = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolBondTotalColumn].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("$#,##0.##", CultureInfo.InvariantCulture);
                            this.SchoolBondTotalTextBox.Text = strResult;
                            if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolBondTotalRGBColumn].ToString()))
                            {
                                string rgbColour = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolBondTotalRGBColumn].ToString();
                                this.SchoolBondTotalTextBox.ForeColor = ParseColorfromString(rgbColour);
                            }
                        }
                        else
                        {
                            this.SchoolBondTotalTextBox.Text = string.Empty;
                        }
                        if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolsWithBondsTotalColumn].ToString()))
                        {
                            var strTemp = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolsWithBondsTotalColumn].ToString();
                            double strNumb = Convert.ToDouble(strTemp);
                            var strResult = strNumb.ToString("$#,##0.##", CultureInfo.InvariantCulture);
                            this.terraScanTextBox3.Text = strResult;

                            if (!string.IsNullOrEmpty(this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolsWithBondsTotalRGBColumn].ToString()))
                            {
                                string rgbColour = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.SchoolsWithBondsTotalRGBColumn].ToString();

                                this.terraScanTextBox3.ForeColor = ParseColorfromString(rgbColour);

                            }
                        }
                        else
                        {
                            this.terraScanTextBox3.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private string RemoveXMLItem(ArrayList parcelIds)
        {
            DataTable dt = new DataTable("CentrallyAssessedItems");
            dt.Columns.Add("CentralItemID", typeof(Int32));
            foreach (var r in parcelIds)
            {
                dt.Rows.Add(r);
            }
            dt = dt.DefaultView.ToTable(true, "CentralItemID");
            dt.AcceptChanges();
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string removexmlstr;
            removexmlstr = sr.ReadToEnd();
            removexmlstr = removexmlstr.Replace("DocumentElement", "root");
           // xmlstr = xmlstr.Replace("ParcelTable", "Table");
            return (removexmlstr);
        }


        private void ClearBottomPanelDetails()
        {
            this.CountyGeneralTotalTextBox.Text = string.Empty;
            this.SchoolGeneralTotalTextBox.Text = string.Empty;
            this.SchoolBondTotalTextBox.Text = string.Empty;
            this.terraScanTextBox3.Text = string.Empty;
        }

        private void SaveAssessedGrid()
        {
            try
            {
                if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                {
                    DataView view = new DataView(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem);
                    DataTable dt = view.ToTable(false, new string[] { "FundID", "PersonalProperty", "RealProperty", "Total", "Description"});
                    string xmlStr= ConvertToXML(dt);
                    if (this.CentralID > 0)
                    {
                        this.form35081Control.WorkItem.F35081_InsertOwnerAssessedGrid(xmlStr, Convert.ToInt32(this.CentralID), TerraScanCommon.UserId);

                        SliceReloadActiveRecord sliceReloadActiveRecord;
                        sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
                        //sliceReloadActiveRecord.SelectedKeyId = returnValue;
                        //this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.Cursor = Cursors.Default;
                        
                    }
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region Central Assessed Picture Box
        /// <summary>
        /// Handles the Click event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CentralAssessedPictureBox_Click(object sender, EventArgs e)
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
        /// Handles the MouseHover event of the StateAssessedPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CentralAssessedPictureBox_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.CentralAssessedOwnerToolTip.SetToolTip(this.CentralAssessedPictureBox, Utility.GetFormNameSpace(this.Name));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion

        /// <summary>
        /// Handles the Load event of the F35081 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F35081_Load(object sender, EventArgs e)
        {
            try
            {

                this.flagLoadForm = false;
                this.CentralAssessedGrid.Enabled = true;
                ////Events for Master Form scroll
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(Scroll_Click);
                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);
                this.FlagSliceForm = true;
                this.CentralGridDataSet = this.form35081Control.WorkItem.F35081_CentralAssessedGridDetails(Convert.ToInt32(this.CentralID));
                this.LoadBottomPanelDetails();
                this.CentralAssessedGrid.DrawFilter = this;
                this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DefaultView;
                if (this.CentralAssessedGrid.Rows.Count > 0)
                {
                    this.MainPanel.Enabled = true;

                    this.SetSmartPartHeight();
                    if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
                    {
                        this.PermissionControlLock(false);
                    }
                    else
                    {
                        this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
                    }
                    this.CentralAssessedGrid.Focus();
                }
                else
                {
                    this.SetSmartPartHeight();
                }
                this.flagLoadForm = true;
                this.QueryLoadForm = false;
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

        #region Common Methods

        private void SetSmartPartHeight()
        {
            int childRowCount;
            int tempChildHeight;
            int gridHeight;

            if (this.CentralAssessedGrid.Rows.Count > 0)
            {
                childRowCount = this.CentralAssessedGrid.Rows.Count + 2;
                tempChildHeight = childRowCount * 22;
                gridHeight = tempChildHeight + (this.CentralAssessedGrid.DisplayLayout.Bands[0].Header.Height + 18);
                this.MainPanel.Height = gridHeight + 5 - ((childRowCount * 2) + (childRowCount - 2));
                this.MainPanel.Height = this.MainPanel.Height + 37;
                this.CentralAssessedGrid.Height = gridHeight - (18 + (childRowCount * 2) + (childRowCount - 2));
                
                this.BottomPanel.Location = new Point(0, this.CentralAssessedGrid.Height - 6);
              
                this.LowerPanel.Location = new Point(0,(this.CentralAssessedGrid.Height - 6)+29);
                this.CentralAssessedPictureBox.Height = gridHeight + 5 - ((childRowCount * 2) + (childRowCount - 2));
                this.CentralAssessedPictureBox.Height = this.CentralAssessedPictureBox.Height + 37;
                this.Height = this.MainPanel.Height;
            }
            else
            {
                gridHeight = 88+45;
                this.MainPanel.Height = gridHeight;
                this.CentralAssessedGrid.Height = gridHeight - (25+37);
                this.BottomPanel.Location = new Point(0, this.CentralAssessedGrid.Height - 5);
                this.LowerPanel.Location = new Point(0, (this.CentralAssessedGrid.Height - 5)+29);
                this.CentralAssessedPictureBox.Height = gridHeight;
                this.Height = this.MainPanel.Height;
            }

            SliceResize sliceResize;
            sliceResize.MasterFormNo = this.masterFormNo;
            sliceResize.SliceFormName = "D30080.F35081";
            sliceResize.SliceFormHeight = this.Height;
            this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
            this.Height = sliceResize.SliceFormHeight;
            this.CentralAssessedPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CentralAssessedPictureBox.Height, this.CentralAssessedPictureBox.Width, this.sectionIndicatorText, this.redColor, this.greenColor, this.blueColor);
        }

        private void performCancel()
        {
            this.stategridheight = 88;
            this.staterowcount = 0;
            this.LoadBottomPanelDetails();
            ////this.PermissionControlLock(this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            this.CentralGridDataSet = this.form35081Control.WorkItem.F35081_CentralAssessedGridDetails(Convert.ToInt32(this.CentralID));
            this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DefaultView;
            
            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
            {
                this.MainPanel.Enabled = true;
            }
            //else
            //{
            //    EnableControls(false);
            //    this.MainPanel.Enabled = false;
            //}

            this.pageMode = TerraScanCommon.PageModeTypes.View;
            
            this.SetSmartPartHeight();
           
            if (this.PermissionFiled.editPermission && this.formMasterPermissionEdit || (this.PermissionFiled.editPermission && !this.formMasterPermissionEdit))
            {
                this.PermissionControlLock(false);
            }
            else
            {
                this.PermissionControlLock(!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit);
            }

        }

        private void PermissionControlLock(bool controlLook)
        {
            
            this.MainPanel.Enabled = true;
            this.CentralAssessedGrid.Enabled = true;
            if (!controlLook)
            {
                this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
            }
            else
            {
                // this.MainPanel.Enabled = true;
                //this.StateAssessedGrid.Enabled = true;
                this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.False;
            }

        }
		// <summary>
        /// Enables the controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableControls(bool enable)
        {
            this.MainPanel.Enabled = enable;
            this.CentralAssessedGrid.Enabled = enable;
        }
        
        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {

            if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
            //if (this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            //{
            //    this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            //    this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            //}
        }



	#endregion


        #region scroll events

        /// <summary>
        /// Handles the Click event of the Scroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.ScrollEventArgs"/> instance containing the event data.</param>
        private void Scroll_Click(object sender, ScrollEventArgs e)
        {
            try
            {
                if (e.NewValue > 5)
                {
                    this.yaxisPoint = e.NewValue;
                }
                else
                {
                    this.yaxisPoint = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Scroll event of the Smartpart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void Smartpart_Scroll(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                    this.yaxisPoint = ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).VerticalScroll.Value;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        private void F35081_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > 5)
            {
                this.yaxisPoint = e.NewValue;
            }
            else
            {
                this.yaxisPoint = 0;
            }
        }
        #endregion scroll events

        private void CentralAssessedGrid_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
            if (this.CentralAssessedGrid.Rows.Count > 0)
            {                
                if (activeRow != null && activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Value == null && string.IsNullOrEmpty(activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Value.ToString()))
                {
                    //activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Value = "<<< >>>";
                    activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Value = "<<< >>>";
                }

                ////Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.StateAssessedGrid.ActiveRow;

                if (activeRow != null)
                {
                    this.SetSmartPartHeight();

                    ////Set the scroll position while adding new row
                    //if (this.ParentForm != null && this.ParentForm.Controls[0] != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                    //{
                    //    this.yaxisPoint = this.yaxisPoint + 25;
                    //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                    //}
                    ////Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.StateAssessedGrid.ActiveCell;

                    if (activeCell != null)
                    {
                        activeCell.Activate();
                        this.CentralAssessedGrid.PerformAction(UltraGridAction.EnterEditMode);
                        try
                        {
                            if (activeCell.IsInEditMode == true && activeCell.SelText.Length > 0)
                            {
                                activeCell.SelStart = activeCell.SelText.Length;
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

            }
            else
            {               
                ////Set scroll position
                if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                {
                   // this.yaxisPoint = this.yaxisPoint + 25;
                   // ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                    if (activeRow == null && activeCell == null)
                    {
                        DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                        //Modified
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                        this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                    }
                }
                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count == 0)
                {
                    DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                    if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                    {
                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                    }
                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                }
                
                this.SetSmartPartHeight();
            }
        }


        private void CentralAssessedGrid_KeyUp(object sender, KeyEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;
            //this.id1 = this.StateAssessedGrid.ActiveRow.Index;
            //activeRow.Activate();
            //activeRow.Selected = true;
            if (this.CentralAssessedGrid.ActiveCell != null)
            {
                if (activeCell.Text != null)
                {
                    if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                    {
                        if (activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName.ToString())  //&& activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName.ToString()
                        {

                            if ((e.KeyValue == 88) || (e.KeyValue == 32))//// && (activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DistrictIDColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName.ToString()))
                            {
                                this.SetEditRecord();
                            }

                            if (e.KeyValue == 8)////&& (activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DistrictIDColumn.ColumnName.ToString() && activeCell.Column.Key.ToString() != this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName.ToString()))
                            {
                                this.SetEditRecord();
                            }
                        }
                    }
                }
            }


            if (activeRow != null && e.KeyValue == 46 && this.slicePermissionField.deletePermission && this.CentralAssessedGrid.ActiveCell == null)
            {
                if (this.CentralAssessedGrid.ActiveRow != null)
                {
                    ArrayList indexList = new ArrayList();
                    if (CentralAssessedGrid.Selected.Rows.Count > 0)
                    { 
                        foreach (UltraGridRow rowSelected in this.CentralAssessedGrid.Selected.Rows)
                        {
                            
                            var index = rowSelected.Index;
                            if (!string.IsNullOrEmpty(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[index]["CentralItemID"].ToString()))
                            {
                                indexList.Add(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[index]["CentralItemID"].ToString());
                            }
                            
                        }
                    }                     
                      this.removeXMLItem = RemoveXMLItem(indexList);
                      if (MessageBox.Show("Do you want to delete these items?", "TerraScan – Delete Centrally Assessed Items", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                      {
                          if (!removeXMLItem.Equals("<root />"))
                          {
                              this.form35081Control.WorkItem.F35081_DeleteOwnerGridDetails(removeXMLItem, Convert.ToInt32(this.CentralID), TerraScanCommon.UserId);
                          }
                          //this.CentralAssessedGrid.Selected.Rows.Clear();
                          //this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Clear();
                      }
                      this.CentralGridDataSet = this.form35081Control.WorkItem.F35081_CentralAssessedGridDetails(Convert.ToInt32(this.CentralID));
                      if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count == 0)
                      {
                          DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                          this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                          if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                          {
                              this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                          }
                          this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                          //this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                      }
                      else
                      {
                          //Added to avoid unnecessary rows after deleting a row
                          if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                          {
                              for (int i = 0; i < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count; i++)
                              {
                                  if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[i][1].ToString()))
                                  {
                                      this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[i][1] = "<<< >>>";
                                  }
                              }
                              this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                          }                          
                      }
                      this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                      this.SetSmartPartHeight();
                      // this.CustomizeGrid();
                }
            }
        }

      
        private void CentralAssessedGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;            
            if (activeRow != null && (activeRow.Index != -1))
            {
                if (activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Value != null && activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Value != null)
                {

                    if (!string.IsNullOrEmpty(activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Value.ToString()))
                    {
                        int rppos;
                        string rpvalue;
                        int rplen;

                        rpvalue = activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Value.ToString();
                        rplen = rpvalue.Replace(",", "").Replace(".", "").Length;
                        rppos = rpvalue.IndexOf(".");
                        if (rplen > 15)
                        {
                            if (rppos > 15)
                            {
                                activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Value = 0;
                            }

                            if (rppos == -1)
                            {
                                activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Value = 0;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Value.ToString()))
                    {
                        string ppvalue;
                        int pppos;
                        int pplen;


                        ppvalue = activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Value.ToString();
                        pplen = ppvalue.Length;
                        pppos = ppvalue.IndexOf(".");
                        if (pplen > 15)
                        {
                            if (pppos > 15)
                            {
                                activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Value = 0;
                            }

                            if (pppos == -1)
                            {
                                activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Value = 0;
                            }
                        }
                    }

                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                }
            }

            ////this.StateAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Format = "$#,###0.00";
        }

        /// <summary>
        /// Handles the KeyDown event of the CentralAssessedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void CentralAssessedGrid_KeyDown(object sender, KeyEventArgs e)
            {
            try
            {   
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;

                
               if (e.KeyValue.Equals(13))
               {
                if (activeRow != null)
                {
                    if (activeCell != null)
                    {
                        if (activeCell.Value.ToString() != null)
                        {
                            //Modified
                            if (activeCell.Column.Index.Equals(1))
                            {
                                string subFundId;
                                if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
                                {
                                    //activeCell.Activated = true;
                                    Form subfundForm = new Form();
                                    // short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                                    object[] optionalParameter;
                                    // short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                                    if (this.form35081Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                                    {
                                        optionalParameter = new object[] { this.form35081Control.WorkItem.RootWorkItem.State["RollYear"].ToString() }; // activeCell.Text.ToString() };
                                    }
                                    else
                                    {
                                        optionalParameter = new object[] { };
                                    }
                                    //object[] optionalParameter = new object[] { this.rollYear };
                                    subfundForm = this.form35081Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1515, optionalParameter, this.form35081Control.WorkItem);
                                    if (subfundForm != null)
                                    {
                                        // activeCell.TabStop = DefaultableBoolean.True;
                                        if (subfundForm.ShowDialog() == DialogResult.OK)
                                        {
                                            // this.CentralAssessedGrid.Rows[activeRow.Index].Activated = true;
                                            activeCell.TabStop = DefaultableBoolean.True;
                                            activeCell.Activated = false;
                                            subFundId = TerraScanCommon.GetValue(subfundForm, "FundId").ToString();
                                            string subFund = TerraScanCommon.GetValue(subfundForm, "FundItem").ToString();
                                            var description = TerraScanCommon.GetValue(subfundForm, "FundDescription").ToString();
                                            this.AddCentralOwnerRowDetails();
                                            activeCell.Selected = true;
                                            if (!string.IsNullOrEmpty(subFundId))
                                            {
                                                if (activeRow.Index >= 0)
                                                {
                                                    activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Value = subFundId;
                                                    activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Value = description.ToString();
                                                    //activeCell.Value = subFundId;
                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["FundID"] = subFundId.ToString();
                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["Fund"] = subFund.ToString();
                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["Description"] = description.ToString();

                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                                     this.AddGridRowToXML();
                                                    if (activeRow.Index < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                                    {
                                                        decimal.TryParse(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["RealProperty"].ToString(), out realProperty);
                                                        decimal.TryParse(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["PersonalProperty"].ToString(), out personalProperty);
                                                    }
                                                    if (!string.IsNullOrEmpty(subFundId) && !subFundId.ToString().Equals("<<< >>>"))
                                                    {
                                                        this.CentralGridRateDataTable = form35081Control.WorkItem.F35081_CentralAssessedRateDetails(Convert.ToInt32(subFundId), personalProperty, realProperty,description,this.centralXMLItem).f35081_GetCentrallyAssessedItemRate;

                                                        if (this.CentralGridRateDataTable.Rows.Count > 0)
                                                        {
                                                            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                                                            {
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Description"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Description"] = this.CentralGridRateDataTable.Rows[0]["Description"].ToString();
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Total"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Total"] = this.CentralGridRateDataTable.Rows[0]["Total"].ToString();
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Rate"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Rate"] = this.CentralGridRateDataTable.Rows[0]["Rate"].ToString();
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["GrossRealTax"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["GrossRealTax"] = this.CentralGridRateDataTable.Rows[0]["GrossRealTax"];
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TaxCredit"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TaxCredit"] = this.CentralGridRateDataTable.Rows[0]["TaxCredit"];
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["NERETax"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["NERETax"] = this.CentralGridRateDataTable.Rows[0]["NERETax"];
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["PPTax"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["PPTax"] = this.CentralGridRateDataTable.Rows[0]["PPTax"];
                                                                }
                                                                if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TotalTax"].ToString()))
                                                                {
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TotalTax"] = this.CentralGridRateDataTable.Rows[0]["TotalTax"];
                                                                }
                                                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                                            }
                                                        }
                                                    }
                                                }
                                                this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                                            }
                                        }
                                        activeCell.Selected = true;
                                        this.SetEditRecord();

                                        // activeCell.Activated = false;
                                    }
                                }
                            }
                            if (activeCell.Column.Index.Equals(14))
                            {
                                if (activeCell.Column.Index.Equals(14) && (this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                                {

                                    if (activeCell.Value.ToString() != null)
                                    {
                                        if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
                                        {
                                            if (activeRow.Index < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                            {
                                                if (!string.IsNullOrEmpty(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["StatementID"].ToString()))
                                                {
                                                    string tempStatementId = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["StatementID"].ToString();
                                                    //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                                                    FormInfo formInfo;
                                                    formInfo = TerraScanCommon.GetFormInfo(11020);
                                                    formInfo.optionalParameters = new object[1];
                                                    formInfo.optionalParameters[0] = tempStatementId;
                                                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                                }
                                            }
                                        }
                                    }
                                    activeCell.Activated = false;
                                }
                            }
                        }
                    }
                }
            }
                if (e.KeyValue.Equals(27))
                {
                    this.OnD9030_F95005_AlertFomrMasterCancel(new DataEventArgs<int>(this.masterFormNo));
                }
                if (e.KeyValue == 9)
                {
                    if (activeCell == null)
                    {
                        var index = activeRow.Index;
                        //Modifed
                        this.CentralAssessedGrid.Rows[index].Cells[1].Activate();
                        this.CentralAssessedGrid.Rows[index].Cells[1].Selected = true;
                        //this.CentralAssessedGrid.Rows[index].Cells[0].Activate();
                        //this.CentralAssessedGrid.Rows[index].Cells[0].Selected = true;
                    }
                  //  
                   // this.CentralAssessedGrid.
                    //this.CentralAssessedGrid.ActiveCell.Activated = true;
                   // activeCell.Activated = true;
                }
                if (activeCell != null)
                {
                    
                    if (activeCell.Text != null)
                    {
                        if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                        {
                            if (e.KeyValue == 46 || e.KeyValue == 8)
                            {
                                this.SetEditRecord();
                            }
                        }
                    }
                }

                

            }
                
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
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

        private void CentralAssessedGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            this.CustomizeGrid();
        }
        /// <summary>
        /// Customizes the grid.
        /// </summary>
        private void CustomizeGrid()
        {
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.RowSelectors = DefaultableBoolean.True;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.CellSelect;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.SelectTypeRow = SelectType.Extended;
            this.CentralAssessedGrid.DisplayLayout.TabNavigation = TabNavigation.NextCell;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementIDColumn.ColumnName].Hidden = true;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.CentralItemIDColumn.ColumnName].Hidden = true;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Hidden = true;
                        
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Header.VisiblePosition = 0;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Header.VisiblePosition = 1;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Header.VisiblePosition = 2;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Header.VisiblePosition = 3;

            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].Header.VisiblePosition = 4;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].Header.VisiblePosition = 5;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].Header.VisiblePosition = 6;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].Header.VisiblePosition = 7;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].Header.VisiblePosition = 8;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].Header.VisiblePosition = 9;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].Header.VisiblePosition = 10;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementIDColumn.ColumnName].Header.VisiblePosition = 11;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].Header.VisiblePosition = 12;


            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Width = 106;
           // this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Width = 60;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Width = 200;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Width = 145;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Width = 145;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].Width = 150;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].Width = 125;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].Width = 150;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].Width = 150;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].Width = 150;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].Width = 150;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].Width = 150;
           // this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Width = 100;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].Width = 250;


            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Header.Caption = "Fund #";
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Header.Caption = "Subfund #";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Header.Caption = "Description";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Header.Caption = "Real Property";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Header.Caption = "Personal Property";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].Header.Caption = "Total";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].Header.Caption = "Rate";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].Header.Caption = "Gross Real Tax";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].Header.Caption = "Tax Credit";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].Header.Caption = "NE RE Tax";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].Header.Caption = "PP Tax";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].Header.Caption = "Total Tax";
           // this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Header.Caption = "Sub FundId";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].Header.Caption = "Statement Number";

            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.URL;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DistrictIDColumn.ColumnName].CellAppearance.ForeColor = Color.FromArgb(31, 65, 103);

            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].Header.Appearance.TextHAlign = HAlign.Center;



            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;

            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Left;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Right;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].CellAppearance.TextHAlign = HAlign.Center;


            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].Format = "$#,###0";//"$#,###0.00";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Format = "$#,###0"; // "$#,###0.00";

            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[thi.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Integer;

            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].Format = "$#,###0"; // "$#,###0.00";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].Format = "#,###0.##############";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].Format = "$#,###0.##";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].Format = "$#,###0.##";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].Format = "$#,###0.##";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].Format = "$#,###0.##";
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].Format = "$#,###0.##";

            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].CellActivation = Activation.NoEdit;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].CellActivation = Activation.AllowEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RateColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.GrossRealTaxColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TaxCreditColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NERETaxColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PPTaxColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.TotalTaxColumn.ColumnName].CellActivation = Activation.NoEdit;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.StatementNumberColumn.ColumnName].CellActivation = Activation.NoEdit;


            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].MaxLength = 20;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.RealPropertyColumn.ColumnName].MaxLength = 20;
            this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.PersonalPropertyColumn.ColumnName].MaxLength = 20;
            //this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].MaxLength = 50;
                        

            if (this.PermissionFiled.newPermission)
            {
                this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
                this.CentralAssessedGrid.DisplayLayout.Bands[0].Columns[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Width = 110;
            }
        }


        private void CentralAssessedGrid_CellDataError(object sender, CellDataErrorEventArgs e)
        {
            e.RestoreOriginalValue = true;
            e.StayInEditMode = true;
        }

        //private void CentralAssessedGrid_Error(object sender, ErrorEventArgs e)
        //{
        //   // e.Cancel = true;
        //}
        private void CentralAssessedGrid_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;

                ////activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DistrictIDColumn.ColumnName].Value = "<<< >>>";

                if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                {                    
                    this.SetEditRecord();
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        private void CentralAssessedGrid_Enter(object sender, EventArgs e)
        {
            try
            {
                this.basePanelScrolled = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CentralAssessedGrid_TextChanged(object sender, EventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;
            if (activeCell.Text != null)
            {
                if (activeCell != null && activeCell.Activation == Activation.AllowEdit && !string.IsNullOrEmpty(activeCell.Text.Trim().ToString()))
                {
                    if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                    {
                        if (activeCell.Column.Index.Equals(1) || activeCell.Column.Index.Equals(4) || activeCell.Column.Index.Equals(5))
                       // if (activeCell.Column.Index.Equals(0) || activeCell.Column.Index.Equals(3) || activeCell.Column.Index.Equals(4))
                        {
                            if (!string.IsNullOrEmpty(activeRow.Cells[1].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[4].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[5].Value.ToString()))
                            //if (!string.IsNullOrEmpty(activeRow.Cells[0].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[3].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[4].Value.ToString()))
                            {
                                this.subFundID = activeRow.Cells[1].Value.ToString();
                                this.fundDescription = activeRow.Cells[3].Value.ToString();
                                decimal.TryParse(activeRow.Cells[4].Value.ToString(), out personalProperty);
                                decimal.TryParse(activeRow.Cells[5].Value.ToString(), out realProperty);
                            }
                            if (activeCell.Column.Index.Equals(0))
                            {
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][0] = activeCell.Value.ToString();
                                this.subFundID = activeCell.Value.ToString();
                            }
                            if (activeCell.Column.Index.Equals(4))
                            {

                                decimal.TryParse(activeCell.Value.ToString(), out personalProperty);
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][4] = this.personalProperty;

                            }
                            if (activeCell.Column.Index.Equals(5))
                            {

                                decimal.TryParse(activeCell.Value.ToString(), out realProperty);
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][5] = this.realProperty;
                            }
                            if (!string.IsNullOrEmpty(subFundID))
                            {
                                this.CentralGridRateDataTable = form35081Control.WorkItem.F35081_CentralAssessedRateDetails(Convert.ToInt32(subFundID), personalProperty, realProperty,fundDescription,this.centralXMLItem).f35081_GetCentrallyAssessedItemRate;

                                if (this.CentralGridRateDataTable.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Description"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Description"] = this.CentralGridRateDataTable.Rows[0]["Description"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Total"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Total"] = this.CentralGridRateDataTable.Rows[0]["Total"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Rate"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Rate"] = this.CentralGridRateDataTable.Rows[0]["Rate"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["GrossRealTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["GrossRealTax"] = this.CentralGridRateDataTable.Rows[0]["GrossRealTax"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TaxCredit"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TaxCredit"] = this.CentralGridRateDataTable.Rows[0]["TaxCredit"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["NERETax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["NERETax"] = this.CentralGridRateDataTable.Rows[0]["NERETax"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["PPTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["PPTax"] = this.CentralGridRateDataTable.Rows[0]["PPTax"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TotalTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TotalTax"] = this.CentralGridRateDataTable.Rows[0]["TotalTax"].ToString();
                                    }
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                }
                            }

                            this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                        }
                        this.SetEditRecord();
                    }

                    //this.SetEditRecord();
                }
            }
        }

        private void CentralAssessedGrid_MouseDown(object sender, MouseEventArgs e)
        {
            Infragistics.Win.UIElement aUIElement = this.CentralAssessedGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
            UltraGridColumn bCol;
            bCol = (UltraGridColumn)aUIElement.GetContext(typeof(UltraGridColumn));
            if (bCol != null)
            {
                this.columnindex = bCol.Index;
                this.columnName = bCol.Key;
            }

        }
        private void CentralAssessedGrid_BeforeRowsDeleted(object sender, BeforeRowsDeletedEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CentralAssessedGrid_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                UltraGrid grid = sender as UltraGrid;
                UIElement controlElement = grid.DisplayLayout.UIElement;
                UIElement elementAtPoint = controlElement != null ? controlElement.ElementFromPoint(e.Location) : null;
                UltraGridColumn column = null;
                while (elementAtPoint != null)
                {
                    HeaderUIElement headerElement = elementAtPoint as HeaderUIElement;
                    if (headerElement != null &&
                         headerElement.Header is Infragistics.Win.UltraWinGrid.ColumnHeader)
                    {
                        column = headerElement.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;


                        break;
                    }

                    elementAtPoint = elementAtPoint.Parent;
                }
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;
                
                string subFundId;
                string isNotValid;

                if (activeRow != null)
                {
                    if (activeCell != null)
                     {
                        //activeCell.Activate();
                        //this.SetEditRecord();
                         if (activeCell.Column.Index != 1 || activeCell.Column.Index != 4 || activeCell.Column.Index != 5 || activeCell.Column.Index != 14)
                       // if (activeCell.Column.Index != 0 || activeCell.Column.Index != 3 || activeCell.Column.Index != 4 || activeCell.Column.Index != 13)
                        {                            
                            activeCell.Activated = false;
                            //activeCell.TabStop = DefaultableBoolean.True;
                        }
                        //Modified by purushotham
                        if (activeCell.Column.Index.Equals(1) && (this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        //if (activeCell.Column.Index.Equals(0) && (this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        {
                            if (activeCell.Value.ToString() != null)
                            {
                                if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
                                    {
                                        //activeCell.Activated = true;
                                       Form subfundForm = new Form();
                                       object[] optionalParameter;
                                    // short.TryParse(this.RollYearTextBox.Text.ToString(), out this.rollYear);
                                       if (this.form35081Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                                       {
                                           optionalParameter = new object[] { this.form35081Control.WorkItem.RootWorkItem.State["RollYear"].ToString() }; // activeCell.Text.ToString() };
                                       }
                                       else
                                       {
                                           string roll = string.Empty;
                                           DateTime dt = DateTime.Now;
                                           roll=dt.Year.ToString();
                                           optionalParameter = new object[] { roll};
                                       }                                   
                                  
                                   // object[] optionalParameter = new object[] { this.rollYear };
                                    subfundForm = this.form35081Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1513, optionalParameter, this.form35081Control.WorkItem);
                                    if (subfundForm != null)
                                    {
                                        // activeCell.TabStop = DefaultableBoolean.True;
                                        if (subfundForm.ShowDialog() == DialogResult.OK)
                                        {
                                           // this.CentralAssessedGrid.Rows[activeRow.Index].Activated = true;
                                            activeCell.TabStop = DefaultableBoolean.True;
                                            activeCell.Activated = false;
                                            isNotValid = TerraScanCommon.GetValue(subfundForm, "ISNotValid").ToString();
                                            if (isNotValid.ToString().ToLower().Equals("false"))
                                            {
                                                subFundId = TerraScanCommon.GetValue(subfundForm, "FundId").ToString();
                                                var subFund = TerraScanCommon.GetValue(subfundForm, "FundItem").ToString();
                                                var description = TerraScanCommon.GetValue(subfundForm, "FundDescription").ToString();
                                                this.AddCentralOwnerRowDetails();
                                                activeCell.Selected = true;
                                                if (!string.IsNullOrEmpty(subFundId))
                                                {
                                                    if (activeRow.Index >= 0)
                                                    {
                                                        activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].Value = subFundId;
                                                        activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundColumn.ColumnName].Value = subFund;
                                                        activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DescriptionColumn.ColumnName].Value = description;
                                                        //activeCell.Value = subFundId;
                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["FundID"] = subFundId.ToString();
                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["Fund"] = subFund.ToString();
                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["Description"] = description.ToString();

                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                                        if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                                                        {
                                                            this.AddGridRowToXML();
                                                        }

                                                        if (activeRow.Index < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                                        {
                                                            decimal.TryParse(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["RealProperty"].ToString(), out realProperty);
                                                            decimal.TryParse(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["PersonalProperty"].ToString(), out personalProperty);
                                                        }
                                                        if (!string.IsNullOrEmpty(subFundId) && !subFundId.ToString().Equals("<<< >>>"))
                                                        {
                                                            this.CentralGridRateDataTable = form35081Control.WorkItem.F35081_CentralAssessedRateDetails(Convert.ToInt32(subFundId), personalProperty, realProperty,description,this.centralXMLItem).f35081_GetCentrallyAssessedItemRate;

                                                            if (this.CentralGridRateDataTable.Rows.Count > 0)
                                                            {
                                                                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                                                                {
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Description"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Description"] = this.CentralGridRateDataTable.Rows[0]["Description"].ToString();
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Total"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Total"] = this.CentralGridRateDataTable.Rows[0]["Total"].ToString();
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Rate"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Rate"] = this.CentralGridRateDataTable.Rows[0]["Rate"].ToString();
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["GrossRealTax"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["GrossRealTax"] = this.CentralGridRateDataTable.Rows[0]["GrossRealTax"];
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TaxCredit"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TaxCredit"] = this.CentralGridRateDataTable.Rows[0]["TaxCredit"];
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["NERETax"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["NERETax"] = this.CentralGridRateDataTable.Rows[0]["NERETax"];
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["PPTax"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["PPTax"] = this.CentralGridRateDataTable.Rows[0]["PPTax"];
                                                                    }
                                                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TotalTax"].ToString()))
                                                                    {
                                                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TotalTax"] = this.CentralGridRateDataTable.Rows[0]["TotalTax"];
                                                                    }
                                                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                                                }
                                                            }
                                                        }
                                                    }
                                                    this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                                                }
                                                this.SetEditRecord();
                                                activeCell.Selected = true;
                                            }
                                            else if (isNotValid.ToString().ToLower().Equals("true"))
                                            {
                                                MessageBox.Show("The rollyear of the Fund selected does not match the current Centrally Assessed record rollyear.", "TerraScan T2 – Invalid Fund Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Modifed by Purushotham
                        if (activeCell.Column.Index.Equals(14) && (this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        //if (activeCell.Column.Index.Equals(13) && (this.CentralAssessedGrid.DisplayLayout.Bands[0].Override.AllowUpdate != DefaultableBoolean.False))
                        {
                            
                            if (activeCell.Value.ToString() != null)
                            {
                                if (!string.IsNullOrEmpty(activeCell.Value.ToString()))
                                {
                                    if (activeRow.Index < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                    {
                                        if (!string.IsNullOrEmpty(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["StatementID"].ToString()))
                                        {
                                            string tempStatementId = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[activeRow.Index]["StatementID"].ToString();
                                            //int.TryParse(activeCell.Value.ToString(), out tempStatementId);
                                            FormInfo formInfo;
                                            formInfo = TerraScanCommon.GetFormInfo(11020);
                                            formInfo.optionalParameters = new object[1];
                                            formInfo.optionalParameters[0] = tempStatementId;
                                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                                        }
                                    }
                                }
                            }
                            activeCell.Activated = false;
                        }
                        //Modifed by Purushotham
                        if (activeCell.Column.Index.Equals(1) && string.IsNullOrEmpty(activeCell.Value.ToString()))
                       // if (activeCell.Column.Index.Equals(0) && string.IsNullOrEmpty(activeCell.Value.ToString()))
                        {
                            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count == 0)
                            {
                                DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                                //Modified
                                if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[0][1].ToString()))
                                {
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][1] = "<<< >>>";
                                    //this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[0][0] = "<<< >>>";
                                }
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                               // this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                            }
                            else
                            {
                                var index = activeRow.Index;
                                DataRow newRow = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.NewRow();
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Add(newRow);
                                if (index < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                {
                                    //Modified
                                    if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[index][1].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[index][1] = "<<< >>>";
                                       // this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[index][0] = "<<< >>>";
                                    }
                                }
                                else
                                {
                                    //Modifed
                                    if (string.IsNullOrEmpty(CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count - 1][1].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count - 1][1] = "<<< >>>";
                                    }
                                }
                                this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                               
                            }
                            
                           // this.SetEditRecord();
                            this.SetSmartPartHeight();
                            if (this.ParentForm != null && this.ParentForm.Controls[0] != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                            {
                                this.yaxisPoint = this.yaxisPoint + 25;
                                ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                            }
                        }
                        //if (this.ParentForm != null && this.ParentForm.Controls[0].Controls["sliceListPanel"] != null)
                        //{
                        //    ((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).AutoScrollPosition = new Point(0, this.yaxisPoint);
                        //}
                        //((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).Scroll += new ScrollEventHandler(Scroll_Click);
                        //((System.Windows.Forms.Panel)this.ParentForm.Controls[0].Controls["sliceListPanel"]).MouseWheel += new MouseEventHandler(this.Smartpart_Scroll);
                       // this.SetSmartPartHeight();                        
                        
                   
                       // this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                        this.CentralAssessedGrid.Focus();

                        //activeCell.Activated = true;
                        //activeCell.Selected = true;
                        //activeCell.TabStop = DefaultableBoolean.True;
                        
                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }


        #region GridFormat Design
        public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
        {
            UltraGridRow row = drawParams.Element.GetContext(typeof(UltraGridRow)) as UltraGridRow;
            UltraGridColumn col = drawParams.Element.GetContext(typeof(UltraGridColumn)) as UltraGridColumn;

            if (row != null && row.Selected)
            {
                drawParams = CustomRowAppearance(drawParams);
            }
            if (row != null && row.Activated)
            {
                drawParams = CustomRowAppearance(drawParams);
            }
            return false;
        }


        private static UIElementDrawParams CustomRowAppearance(UIElementDrawParams drawParams)
        {
            drawParams.AppearanceData.ForeColor = Color.White;

            drawParams.AppearanceData.BackColor = Color.Black;
            return drawParams;
        }

        public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) 
       
        {
            if (drawParams.Element is Infragistics.Win.FormattedLinkLabel.TextSectionUIElement)
            {
                return DrawPhase.BeforeDrawForeground;
            }

            return DrawPhase.None;

        }
        #endregion

        private void CentralAssessedGrid_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
                Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;

                ////activeRow.Cells[this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.DistrictIDColumn.ColumnName].Value = "<<< >>>";

                if (activeCell != null && !string.IsNullOrEmpty(activeCell.Text.ToString()))
                {
                    if (activeCell.Column.Index.Equals(1) || activeCell.Column.Index.Equals(4) || activeCell.Column.Index.Equals(5))
                   // if (activeCell.Column.Index.Equals(0) || activeCell.Column.Index.Equals(3) || activeCell.Column.Index.Equals(4))
                    {
                        this.AddCentralOwnerRowDetails();
                        var activeIndex=activeRow.Index;
                        if (!string.IsNullOrEmpty(activeRow.Cells[1].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[4].Value.ToString()) || !string.IsNullOrEmpty(activeRow.Cells[5].Value.ToString()))
                        {
                            this.subFundID = activeRow.Cells[0].Value.ToString();
                            this.fundDescription = activeRow.Cells[3].Value.ToString();
                            //this.subFund=activeRow.Cells[1].Value.ToString();
                            if (activeRow.Cells[4].Value.ToString().Length < 20)
                            {
                                if (!activeRow.Cells[4].Value.ToString().Contains(".") && activeRow.Cells[4].Value.ToString().Length > 15)
                                {
                                    activeRow.Cells[4].Value = 0;
                                }                               
                                decimal.TryParse(activeRow.Cells[4].Value.ToString(), out personalProperty);
                            }
                            else
                            {
                                activeRow.Cells[4].Value = 0;
                                decimal.TryParse(activeRow.Cells[4].Value.ToString(), out personalProperty);
                            }
                            if (activeRow.Cells[5].Value.ToString().Length < 20)
                            {
                                if (!activeRow.Cells[5].Value.ToString().Contains(".") && activeRow.Cells[5].Value.ToString().Length > 15)
                                {
                                    activeRow.Cells[5].Value = 0;
                                }
                                decimal.TryParse(activeRow.Cells[5].Value.ToString(), out realProperty);
                            }
                            else
                            {
                                activeRow.Cells[5].Value = 0;
                                decimal.TryParse(activeRow.Cells[5].Value.ToString(), out realProperty);
                            }
                        }
                        //Modifed
                        if (activeCell.Column.Index.Equals(1))
                        {
                            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][1] = activeCell.Value;
                            this.subFundID = activeCell.Value.ToString();
                            //this.subFund=activeCell.Value.ToString();
                        }
                        if (activeCell.Column.Index.Equals(4))
                        {
                           
                            decimal.TryParse(activeCell.Value.ToString(), out personalProperty);
                            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                            {
                                if (activeIndex < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                {
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][4] = this.personalProperty;
                                }
                            }

                        }
                        if (activeCell.Column.Index.Equals(5))
                        {

                            decimal.TryParse(activeCell.Value.ToString(), out realProperty);
                            if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                            {
                                if (activeIndex < this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count)
                                {
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index][5] = this.realProperty;
                                }
                            }
                        }
                        if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                        {
                            this.AddGridRowToXML();
                            ////this.CentralGridRateDataTable=this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Copy();
                            //this.CentralXMLDataSet.Tables.Add(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Copy());
                        }
                        if (!string.IsNullOrEmpty(subFundID) && !subFundID.ToString().Equals("<<< >>>"))  //Check subFund also
                        {
                            this.CentralGridRateDataTable = form35081Control.WorkItem.F35081_CentralAssessedRateDetails(Convert.ToInt32(subFundID), personalProperty, realProperty,fundDescription,this.centralXMLItem).f35081_GetCentrallyAssessedItemRate;

                            if (this.CentralGridRateDataTable.Rows.Count > 0)
                            {
                                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Description"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Description"] = this.CentralGridRateDataTable.Rows[0]["Description"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Total"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Total"] = this.CentralGridRateDataTable.Rows[0]["Total"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["Rate"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["Rate"] = this.CentralGridRateDataTable.Rows[0]["Rate"].ToString();
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["GrossRealTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["GrossRealTax"] = this.CentralGridRateDataTable.Rows[0]["GrossRealTax"];
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TaxCredit"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TaxCredit"] = this.CentralGridRateDataTable.Rows[0]["TaxCredit"];
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["NERETax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["NERETax"] = this.CentralGridRateDataTable.Rows[0]["NERETax"];
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["PPTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["PPTax"] = this.CentralGridRateDataTable.Rows[0]["PPTax"];
                                    }
                                    if (!string.IsNullOrEmpty(this.CentralGridRateDataTable.Rows[0]["TotalTax"].ToString()))
                                    {
                                        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem[activeRow.Index]["TotalTax"] = this.CentralGridRateDataTable.Rows[0]["TotalTax"];
                                    }
                                    this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.AcceptChanges();
                                }
                            }
                        }

                        this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;
                    }
                    this.SetEditRecord();
                }
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void CentralAssessedGrid_AfterSelectChange(object sender, AfterSelectChangeEventArgs e)
        {
            Infragistics.Win.UltraWinGrid.UltraGridRow activeRow = this.CentralAssessedGrid.ActiveRow;
            Infragistics.Win.UltraWinGrid.UltraGridCell activeCell = this.CentralAssessedGrid.ActiveCell;                
                //if (this.CentralAssessedGrid.ActiveRow != null)
                //{
                //    this.CentralAssessedGrid.Selected.Rows.AddRange((UltraGridRow[])this.CentralAssessedGrid.Rows.GetAllNonGroupByRows());
                //    if (this.CentralAssessedGrid.Selected.Rows.Count > 0)
                //    {

                //        for (int i = 0; i < this.CentralAssessedGrid.Selected.Rows.Count; i++)
                //        {

                //        }
                //        foreach (UltraGridRow rowSelected in this.CentralAssessedGrid.Selected.Rows)
                //        {
                            
                //        }

                //    }
                   // this.removeXMLItem = ConvertToXML(removeTable);
                   // this.form35081Control.WorkItem.F35081_DeleteOwnerGridDetails(removeXMLItem, this.CentralID, TerraScanCommon.UserId);
                    //if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > this.CentralAssessedGrid.ActiveRow.Index)
                    //{
                    //    if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[this.CentralAssessedGrid.ActiveRow.Index][this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].ToString() == "<<< >>>")
                    //    {
                    //        this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.RemoveAt(this.CentralAssessedGrid.ActiveRow.Index);
                    //    }
                    //    else
                    //    {
                    //        if (!string.IsNullOrEmpty(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[this.CentralAssessedGrid.ActiveRow.Index][this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].ToString()))
                    //        {
                    //            int.TryParse(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows[this.CentralAssessedGrid.ActiveRow.Index][this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.FundIDColumn.ColumnName].ToString(), out stateItemvalue);
                    //            removeXMLItem = ConvertToXML(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem);
                    //            this.form35081Control.WorkItem.F35081_DeleteOwnerGridDetails(removeXMLItem, this.CentralID, TerraScanCommon.UserId);
                    //            this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.RemoveAt(this.CentralAssessedGrid.ActiveRow.Index);
                    //        }
                    //    }
                    //}
               // }

                //this.CentralGridDataSet = this.form35081Control.WorkItem.F35081_CentralAssessedGridDetails(CentralID);
                //this.CentralAssessedGrid.DataSource = this.CentralGridDataSet.f35081_GetCentrallyAssessedItem;

                //this.SetSmartPartHeight();

                //this.CustomizeGrid();
        }

        private void CentralAssessedGrid_Error(object sender, Infragistics.Win.UltraWinGrid.ErrorEventArgs e)
        {          
            e.Cancel = true;        
        }

        private void CentralAssessedGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
           if( e.KeyChar == Char.Parse("."))
            {
                e.Handled = true;
            }
        }


        private void AddCentralOwnerRowDetails()
        {
            try
            {
                if (this.CentralXMLDataSet.Tables.Count ==0)
                {  
                    F35081CentralAssessedGridData updateparcelDetails = new F35081CentralAssessedGridData();
                    F35081CentralAssessedGridData.CentralAssessmentListRow dr = updateparcelDetails.CentralAssessmentList.NewCentralAssessmentListRow();

                    dr.CentralID = this.CentralID.ToString();
                    if (this.form35081Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                    {
                        this.rollYear = this.form35081Control.WorkItem.RootWorkItem.State["RollYear"].ToString();
                        dr.RollYear = rollYear.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"] != null)
                    {
                        this.parcelNumber = this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"].ToString();
                        dr.ParcelNumber = this.parcelNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"] != null)
                    {
                        this.companyName = this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"].ToString();
                        dr.CompanyName = this.companyName.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"] != null)
                    {
                        this.companyNumber = this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"].ToString();
                        dr.CompanyNumber = this.companyNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"] != null)
                    {
                        this.baseLine = this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"].ToString();
                        dr.BranchLine = this.baseLine.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"] != null)
                    {
                        this.propertyClassID = this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"].ToString();
                        dr.PropertyClassID = this.propertyClassID.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"] != null)
                    {
                        this.ownerID = this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"].ToString();
                        dr.OwnerID = this.ownerID.ToString();
                    }
                    updateparcelDetails.CentralAssessmentList.Rows.Add(dr);
                    updateparcelDetails.CentralAssessmentList.AcceptChanges();
                    CentralXMLDataSet.Tables.Add(updateparcelDetails.CentralAssessmentList.Copy());
                    CentralXMLDataSet.Tables[0].TableName = "Table";
                }
                if (CentralXMLDataSet.Tables[0].Rows.Count > 0)
                {  
                    if (this.form35081Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                    {
                        this.rollYear = this.form35081Control.WorkItem.RootWorkItem.State["RollYear"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["RollYear"] = rollYear.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"] != null)
                    {
                        this.parcelNumber = this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["ParcelNumber"] = this.parcelNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"] != null)
                    {
                        this.companyName = this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["CompanyName"] = this.companyName.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"] != null)
                    {
                        this.companyNumber = this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["CompanyNumber"] = this.companyNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"] != null)
                    {
                        this.baseLine = this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["BranchLine"] = this.baseLine.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"] != null)
                    {
                        this.propertyClassID = this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["PropertyClassID"] = this.propertyClassID.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"] != null)
                    {
                        this.ownerID = this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"].ToString();
                        this.CentralXMLDataSet.Tables[0].Rows[0]["OwnerID"] = this.ownerID.ToString();
                    }
                    this.CentralXMLDataSet.Tables[0].AcceptChanges();
                }
                else
                {
                    F35081CentralAssessedGridData updateparcelDetails = new F35081CentralAssessedGridData();
                    F35081CentralAssessedGridData.CentralAssessmentListRow dr = updateparcelDetails.CentralAssessmentList.NewCentralAssessmentListRow();

                    dr.CentralID = this.CentralID.ToString();
                    if (this.form35081Control.WorkItem.RootWorkItem.State["RollYear"] != null)
                    {
                        this.rollYear = this.form35081Control.WorkItem.RootWorkItem.State["RollYear"].ToString();
                        dr.RollYear = rollYear.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"] != null)
                    {
                        this.parcelNumber = this.form35081Control.WorkItem.RootWorkItem.State["ParcelNumber"].ToString();
                        dr.ParcelNumber = this.parcelNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"] != null)
                    {
                        this.companyName = this.form35081Control.WorkItem.RootWorkItem.State["CompanyName"].ToString();
                        dr.CompanyName = this.companyName.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"] != null)
                    {
                        this.companyNumber = this.form35081Control.WorkItem.RootWorkItem.State["CompanyNumber"].ToString();
                        dr.CompanyNumber = this.companyNumber.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"] != null)
                    {
                        this.baseLine = this.form35081Control.WorkItem.RootWorkItem.State["BranchLine"].ToString();
                        dr.BranchLine = this.baseLine.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"] != null)
                    {
                        this.propertyClassID = this.form35081Control.WorkItem.RootWorkItem.State["PropertyClassID"].ToString();
                        dr.PropertyClassID = this.propertyClassID.ToString();
                    }
                    if (this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"] != null)
                    {
                        this.ownerID = this.form35081Control.WorkItem.RootWorkItem.State["OwnerID"].ToString();
                        dr.OwnerID = this.ownerID.ToString();
                    }
                    updateparcelDetails.CentralAssessmentList.Rows.Add(dr);
                    updateparcelDetails.CentralAssessmentList.AcceptChanges();
                    CentralXMLDataSet.Tables.Add(updateparcelDetails.CentralAssessmentList.Copy());
                    CentralXMLDataSet.Tables[0].TableName = "Table";
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// AddGridRowToXML
        /// </summary>
        private void AddGridRowToXML()
        {
            try
            {
                if (this.CentralXMLDataSet.Tables.Count > 1)
                {
                    try
                    {
                        this.CentralXMLDataSet.Tables[1].Clear();
                        this.CentralXMLDataSet.Tables.Remove(this.CentralXMLDataSet.Tables[1]);
                        //this.CentralXMLDataSet.Tables.Remove("CentralItem");
                    }
                    catch (Exception ex)
                    {

                    }
                    //this.CentralXMLDataSet.Tables[1].Clear();
                }
                if (this.CentralGridDataSet.f35081_GetCentrallyAssessedItem.Rows.Count > 0)
                {
                    this.centralXMLItem = string.Empty;
                    DataView view = new DataView(this.CentralGridDataSet.f35081_GetCentrallyAssessedItem);
                    ArrayList rowList = new ArrayList();
                    DataTable dt = view.ToTable(false, new string[] { "CentralItemID", "FundID", "PersonalProperty", "RealProperty", "Total", "Description" });
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dt.Rows[i][1].ToString().Contains("<<< >>>") || (string.IsNullOrEmpty(dt.Rows[i][1].ToString())) && string.IsNullOrEmpty(dt.Rows[i][2].ToString()) && (string.IsNullOrEmpty(dt.Rows[i][3].ToString())))
                        {
                            dt.Rows[i].Delete();
                        }
                    }
                    dt.AcceptChanges();
                    if (dt.Columns.Count == 6 && dt.Rows.Count>0)
                    {
                        DataColumn extraColumn = new DataColumn("CentralID", typeof(string));
                        dt.Columns.Add(extraColumn);
                        dt.AcceptChanges();
                        extraColumn.SetOrdinal(0);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[i]["CentralID"].ToString()))
                        {
                            dt.Rows[i]["CentralID"] = this.CentralID.ToString();
                        }
                        if (dt.Rows[i][1].ToString().Contains("<<< >>>"))
                        {
                            dt.Rows[i][1] = "";
                            dt.AcceptChanges();
                        }
                    }
                    this.CentralXMLDataSet.Tables.Add(dt.Copy());
                    this.CentralXMLDataSet.Tables[1].Namespace = "";
                    this.CentralXMLDataSet.Tables[1].TableName = "CentralItem";
                    this.centralXMLItem = this.CentralXMLDataSet.GetXml();
                    this.centralXMLItem = this.centralXMLItem.Replace("Table", "CentralAssessment");
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void LoadCentralOwnerRowDetails()
        {
            try
            {
                if (this.CentralXMLDataSet.Tables.Count >= 1)
                {

                    this.CentralXMLDataSet.Tables[0].Clear();
                    this.CentralXMLDataSet.Tables.Remove(this.CentralXMLDataSet.Tables[0]);
                }
                //this.CentralXMLDataSet.Tables.Remove("CentralItem");
                   
                F35081CentralAssessedGridData updateparcelDetails = new F35081CentralAssessedGridData();
                F35081CentralAssessedGridData.CentralAssessmentListRow dr = updateparcelDetails.CentralAssessmentList.NewCentralAssessmentListRow();
                if (this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows.Count > 0)
                {
                    dr.CentralID = this.CentralID.ToString();
                    dr.RollYear = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.RollYearColumn].ToString();
                    dr.ParcelNumber = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.ParcelNumberColumn].ToString();
                    dr.CompanyName = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CompanyNameColumn].ToString();
                    dr.CompanyNumber = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.CompanyNumberColumn].ToString();
                    dr.BranchLine = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.BranchLineColumn].ToString();
                    dr.PropertyClassID = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.PropertyClassIDColumn].ToString();
                    dr.OwnerID = this.OwnerDataSet.f35080CentrallyAssessedOwner.Rows[0][this.OwnerDataSet.f35080CentrallyAssessedOwner.OwnerIDColumn].ToString();
                }               
                updateparcelDetails.CentralAssessmentList.Rows.Add(dr);
                updateparcelDetails.CentralAssessmentList.AcceptChanges();
                CentralXMLDataSet.Tables.Add(updateparcelDetails.CentralAssessmentList.Copy());
                CentralXMLDataSet.Tables[0].TableName = "Table";

            }
            catch (Exception ex)
            {

            }
        }
    }
}
