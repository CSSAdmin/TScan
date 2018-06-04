//--------------------------------------------------------------------------------------------
// <copyright file="F1226.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1226.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/10/2006       Ranjani            Created// 
// 10/24/2006       Ranjani            UI design completed
// 10/19/2006       Ranjani            Functionality completed 
// 10/27/2006       Ranjani            query related hardcoded values changed
// 10/27/2006       Ranjani            required comments added
// 11/06/2006       Ranjani            snapshot utilty function modified for retaining the snapshot
// 11/22/2006       Guhan              Removed Delete Button 
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
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
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Form F1226
    /// </summary>
    [SmartPart]
    public partial class F1226 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// F1226Controller Variable
        /// </summary>
        private F1226Controller form1226Control;

        /// <summary>
        /// DataSet Contains CL Details - IDs and CL details
        /// </summary>
        private CheckDetailData checkDetail = new CheckDetailData();

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// formFilterType variable is used to find the type of filter in the form. 
        /// </summary>   
        private TerraScanCommon.FilterType formFilterType;

        /// <summary>
        /// currentclId variable is used to store Cash Ledger id. - default value - null
        /// </summary>       
        private int? currentclId = null;      

        /// <summary>
        /// WhereCondition variable is used execute current records. 
        /// </summary>  
        private string whereCondition = String.Empty;

        /// <summary>
        /// userDefinedWhereCondition variable is used to store user defined where condition. 
        /// </summary>
        private string userDefinedWhereCondition = String.Empty;

        /// <summary>
        /// filterTypeId variable is used to find filteredrecordsid depends on the FormFilterType. 
        /// </summary>       
        private int filterTypeId;

        /// <summary>
        /// Created string for current snapshotName
        /// </summary>
        private string currentSnapshotName = string.Empty;

        /// <summary>
        /// Created string for current snapshotDescription
        /// </summary>
        private string currentSnapshotDescription = string.Empty;

        /// <summary>
        /// toolBoxSmartPart
        /// </summary>
        private ToolBoxSmartPart toolBoxSmartPart = new ToolBoxSmartPart();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart = new StatusBarSmartPart();

        /// <summary>
        /// Query Control Array
        /// </summary>
        private TerraScanTextBox[] queryControlArray;

        #endregion   

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1226"/> class.
        /// </summary>
        public F1226()
        {
            this.InitializeComponent();  
            this.Load += new EventHandler(this.F1226_Load);
            ////Customize ItemListingGridView
            this.CustomizeItemListingGridView();
            ////sets tags of the control with query field name
            this.SetQueryingFieldName();
            this.CheckPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CheckPictureBox.Height, this.CheckPictureBox.Width, "Check", 28, 81, 128);
            this.CLDetailPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.CLDetailPictureBox.Height, this.CLDetailPictureBox.Width, "Detail", 174, 150, 94);
            this.ItemListingPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ItemListingPictureBox.Height, this.ItemListingPictureBox.Width, "Item Listing", 0, 51, 0);
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Declare the event setFormHeader        
        /// </summary>   
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Declare the event SetRecordCount        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Declare the event SetActiveRecord        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Declare the event SetActiveRecordButtons        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;       

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets the F1226 control.
        /// </summary>
        /// <value>The F1226 control.</value>
        [CreateNew]
        public F1226Controller Form1226Control
        {
            get { return this.form1226Control as F1226Controller; }
            set { this.form1226Control = value; }
        }

        /// <summary>
        /// Gets or sets the currentcl(cashledger) id - null if not valid.
        /// </summary>
        /// <value>The currentcl id.</value>
        public int? CurrentclId
        {
            get
            {
                return this.currentclId;
            }

            set
            {
                this.currentclId = value;
                ////sets additionalOperationSmartPart keyid - required for attachment and comment
                if (this.additionalOperationSmartPart != null)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentclId ?? -1;
                }
            }
        }    

        /// <summary>
        /// Gets or sets the userDefinedWhereCondition - user entered
        /// </summary>
        /// <value>The userDefinedWhereCondition.</value>
        [Description("Display Data based on userDefinedWhereCondition.")]
        private string UserDefinedWhereCondition
        {
            get
            {
                return this.userDefinedWhereCondition;
            }

            set
            {
                this.userDefinedWhereCondition = value.ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the FilterTypeId - (snapshot or query)
        /// </summary>
        /// <value>The filterTypeId.</value>
        [Description("Display Data based on Filtered TypeId.")]
        private int FilterTypeId
        {
            get
            {
                return this.filterTypeId;
            }

            set
            {
                this.filterTypeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the whereCondition - sql executable
        /// </summary>
        /// <value>The whereCondition.</value>
        [Description("Display Data based on WhereCond.")]
        private string WhereCondition
        {
            get { return this.whereCondition; }
            set { this.whereCondition = value.ToUpper(); }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotName - loaded snapshot name
        /// </summary>
        /// <value>The current Snapshot Name.</value>
        [Description("Display Data based on currentSnapshot.")]
        private string CurrentSnapshotName
        {
            get { return this.currentSnapshotName; }
            set { this.currentSnapshotName = value; }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotDescription - loaded snapshot description
        /// </summary>
        /// <value>The current Snapshot Description.</value>
        [Description("Display Data based on currentSnapshot.")]
        private string CurrentSnapshotDescription
        {
            get { return this.currentSnapshotDescription; }
            set { this.currentSnapshotDescription = value; }
        }

        /// <summary>
        /// Gets or sets the FormFilterType - query or snapshot
        /// </summary>
        /// <value>The formFilterType.</value>
        [Description("Display Data based on Filtered Type.")]
        private TerraScanCommon.FilterType FormFilterType
        {
            get { return this.formFilterType; }
            set { this.formFilterType = value; }
        }

        /// <summary>
        /// Gets or sets the page status - diff normal and filter mode.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }

        #endregion

        #region EventSubcription

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.CurrentclId = this.RetrieveRecordId(recordNavigationEntity.RecordIndex);
            this.FillCheckDetails(null, recordNavigationEntity.RecordNavigationFlag);
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryByFromButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.QueryByFormFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Clears the filter button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick, Thread = ThreadOption.UserInterface)]
        public void ClearFilterButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.ClearFilterFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Queries the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.QueryUtilityFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Snapshots the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void SnapshotUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.SnapshotUtilityFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick, Thread = ThreadOption.UserInterface)]
        public void FilteredButtonClick(object sender, DataEventArgs<Button> e)
        {
            this.ReQueryFunction(this, new DataEventArgs<Button>(e.Data));
        }

        /// <summary>
        /// Sends the optional parameters.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_SendOptionalParameters, Thread = ThreadOption.UserInterface)]
        public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        {
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("QueryByFormModeChange"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RecordConflict")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    this.ClearQueryByFormFields();
                }
                else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    if (this.RetrieveRecordIndex(Convert.ToInt32(optionalParams[0])) < 1)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("QueryByFormModeChange"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RecordConflict")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        this.ClearQueryByFormFields();
                    }
                }

                this.CurrentclId = Convert.ToInt32(optionalParams[0]);

                this.FillCheckDetails(null, false);
            }
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the f1100 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1226_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadWorkSpaces();
                ////by default null
                this.CurrentclId = null;
                this.FillCheckDetails(null, false);
                ////sets QueryRelated Field
                this.queryControlArray = new TerraScanTextBox[] { this.PayableToTextBox, this.EntryDateTextBox, this.CheckNumberTextBox, this.AmountTextBox, this.MemoTextBox, this.FromAccountTextBox, this.AgencyTextBox, this.CLTypeTextBox, this.UserTextBox, this.ClearedByTextBox, this.ClearedDateTextBox, this.PrintedByTextBox, this.PrintedDateTextBox, this.MailedByTextBox, this.MailedDateTextBox, this.VoidByTextBox, this.VoidDateTextBox };
                this.ActiveControl = ItemListingGridView;
                ItemListingGridView.Focus();
                ItemListingPanel.Focus();
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
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.Form1226Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1226Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1226Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Check Detail", string.Empty }));            

            ////Load RecordNavigatorSmartPart to RecordNavigatorSmartPartdeckWorkspace
            if (this.Form1226Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form1226Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form1226Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////Load ToolBoxSmartPart to ToolBoxSmartPartdeckWorkspace
            if (this.Form1226Control.WorkItem.SmartParts.Contains(SmartPartNames.ToolBoxSmartPart))
            {
                this.toolBoxSmartPart = (ToolBoxSmartPart)this.Form1226Control.WorkItem.SmartParts.Get(SmartPartNames.ToolBoxSmartPart);                
            }
            else
            {
                this.toolBoxSmartPart = (ToolBoxSmartPart)this.Form1226Control.WorkItem.SmartParts.AddNew<ToolBoxSmartPart>(SmartPartNames.ToolBoxSmartPart);                
            }

            this.ToolBoxDeckWorkspace.Show(this.toolBoxSmartPart);

            ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
            if (this.Form1226Control.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.statusBarSmartPart = (StatusBarSmartPart)this.Form1226Control.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart);
            }
            else
            {
                this.statusBarSmartPart = this.Form1226Control.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart);
            }

            this.StatusBarDeckWorkspace.Show(this.statusBarSmartPart);

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.Form1226Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.Form1226Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            }
            else
            {
                this.additionalOperationSmartPart = this.Form1226Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
            }
           
            this.AddtionalOperationDeckWorkspace.Show(this.additionalOperationSmartPart);    
            ////set required variable - attachment and comment
            this.additionalOperationSmartPart.ParentWorkItem = this.Form1226Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = this.ParentFormId;
            this.additionalOperationSmartPart.CurrntFormId = this.ParentFormId;
            ////set enable/visible property to the controls
            this.toolBoxSmartPart.EnableClearFilterButton = false;            
            this.statusBarSmartPart.VisibleDelinquentButton = false;
            this.statusBarSmartPart.VisibleAutoPrintButton = false;           
        }

        #endregion

        #region Private Methods

        #region Cash Ledger - Get And Retrieve Method

        /// <summary>
        /// retrieves the current RecordId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>id of the current record</returns>
        private int RetrieveRecordId(int index)
        {
            int tempCLId = 0;

            if (this.checkDetail.ListCashLedgerID.Rows.Count > 0)
            {
                if (index > 0 && index <= this.checkDetail.ListCashLedgerID.Rows.Count)
                {
                    ////gets current record id
                    if (!String.IsNullOrEmpty(Convert.ToString(this.checkDetail.ListCashLedgerID.Rows[index - 1][this.checkDetail.ListCashLedgerID.KeyIDColumn])))
                    {
                        tempCLId = Convert.ToInt32(this.checkDetail.ListCashLedgerID.Rows[index - 1][this.checkDetail.ListCashLedgerID.KeyIDColumn]);
                    }
                }
                else
                {
                    ////last record id
                    tempCLId = Convert.ToInt32(this.checkDetail.ListCashLedgerID.Rows[this.checkDetail.ListCashLedgerID.Rows.Count - 1][this.checkDetail.ListCashLedgerID.KeyIDColumn]);
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.checkDetail.ListCashLedgerID.Rows.Count));
                }
            }

            return tempCLId;
        }

        /// <summary>
        /// retrieves the current record position index
        /// </summary>
        /// <param name="tempRecordId">tempRecordId</param>
        /// <returns>index of the current record</returns>
        private int RetrieveRecordIndex(int? tempRecordId)
        {
            if (tempRecordId == null)
            {
                tempRecordId = this.currentclId;
            }

            int tempIndex = 0;
            DataTable tempDataTable = this.checkDetail.ListCashLedgerID.Copy();
            tempDataTable.DefaultView.RowFilter = string.Concat(this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName, " = ", tempRecordId);

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
            }

            return tempIndex;
        }

        /// <summary>
        /// Fills the Check detail form details.
        /// </summary>
        /// <param name="tempDataTable">The temp data table contains statementIDs.</param>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void FillCheckDetails(DataTable tempDataTable, bool fetchNextRecord)
        {              
            this.Cursor = Cursors.WaitCursor;
            int recordIndex = 0;

            try
            {
                ////refresh form record set
                this.LoadCashLedgerId(tempDataTable);                

                if (this.checkDetail.ListCashLedgerID.Rows.Count > 0)
                {
                    ////get current cl id - assign clid -> currentclid
                    this.GetCurrentCLId(fetchNextRecord);

                    this.checkDetail.GetCheckDetail.Clear();
                    this.checkDetail.ListSubFundItem.Clear();
                    this.checkDetail.Merge(this.form1226Control.WorkItem.F1226_GetCashLedger(this.currentclId.Value), true);
                    ////fill check related fields
                    this.GetCheckDetails();
                    recordIndex = this.RetrieveRecordIndex(null);
                    this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.checkDetail.ListCashLedgerID.Rows.Count));
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { recordIndex, this.checkDetail.ListCashLedgerID.Rows.Count }));

                    ////Enable Delete button
                    ////this.DeleteCLButton.Enabled = true && this.PermissionFiled.deletePermission;
                }
                else
                {
                    this.CurrentclId = null;                    
                    this.ClearCheckDetail();
                }
            }
            catch (SoapException ex)
            {
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {               
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                ////sets focus
                if (this.EditCLButton.Enabled)
                {
                   //// this.ActiveControl = this.EditCLButton;
                }
                else
                {
                    RecordNavigatorSmartPart recordNavigatorSmartPart = (RecordNavigatorSmartPart)this.form1226Control.WorkItem.SmartParts[SmartPartNames.RecordNavigatorSmartPart];
                    recordNavigatorSmartPart.SetFocus = true;
                }               

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Loads the cash ledger id.
        /// </summary>
        /// <param name="tempDataTable">The temp data table.</param>
        private void LoadCashLedgerId(DataTable tempDataTable)
        {
            ////clears record set
            this.checkDetail.Clear();
            if (tempDataTable != null)
            {
                this.checkDetail.ListCashLedgerID.Merge(tempDataTable);
            }
            else
            {
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    ////check for where condition
                    string tempWhereString = null;
                    if (!string.IsNullOrEmpty(this.whereCondition))
                    {
                        tempWhereString = this.whereCondition;
                    }

                    ////check for filter type if snapshot execute snapshot else execute query
                    if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot) && this.FilterTypeId != 0)
                    {
                        this.checkDetail.ListCashLedgerID.Merge(this.form1226Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, tempWhereString, null, this.ParentFormId).ListKeyId);
                    }
                    else
                    {
                        this.checkDetail.ListCashLedgerID.Merge(this.form1226Control.WorkItem.ExecuteQuery(tempWhereString, null, this.ParentFormId).ListKeyId);
                    }

                    if (this.checkDetail.ListCashLedgerID.Rows.Count > 0)
                    {
                        return;
                    }

                    ////records not found - clear filter mode
                    this.ClearQueryByFormFields();
                }

                ////default load record set
                this.checkDetail.ListCashLedgerID.Merge(this.form1226Control.WorkItem.F1226_ListCashLedger());
            }
        }

        /// <summary>
        /// Gets the current CL id.
        /// </summary>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void GetCurrentCLId(bool fetchNextRecord)
        {            
            if (this.currentclId.HasValue)
            {
                DataTable tempDataTable = this.checkDetail.ListCashLedgerID.Copy();
                DataView tempDataView = new DataView(tempDataTable);
                tempDataView.RowFilter = string.Concat(this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName, " = ", this.currentclId);
                ////checks fetching record exists
                if (tempDataView.Count > 0)
                {
                    return;
                }

                ////fetch next available records
                tempDataView = tempDataTable.DefaultView;
                if (fetchNextRecord)
                {
                    tempDataView.RowFilter = string.Concat(this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName, " > ", this.currentclId);
                }
                else
                {
                    tempDataView.RowFilter = string.Concat(this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName, " < ", this.currentclId);
                    tempDataView.Sort = string.Concat(this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName, " DESC");
                }

                if (tempDataView.Count > 0)
                {
                    this.CurrentclId = (int)tempDataView[0][this.checkDetail.ListCashLedgerID.KeyIDColumn.ColumnName];
                    return;
                }
            }

            this.CurrentclId = (int)this.checkDetail.ListCashLedgerID.Rows[0][this.checkDetail.ListCashLedgerID.KeyIDColumn];
        }

        /// <summary>
        /// Gets the Cl details and fill Subfung item accordingly.
        /// </summary>        
        private void GetCheckDetails()
        {
            ////gets the check detail and fill check ,detail and subfund item
            if (this.checkDetail.GetCheckDetail.Rows.Count > 0)
            {
                ////Enable edit button
                this.EditCLButton.Enabled = true && this.PermissionEdit;
                ////Fill check  
                this.PayableToValueLabel.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PayableToColumn].ToString();                
                this.AmountValueLabel.Text = string.Format("{0:#,##0.00}", this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AmountColumn]);
                this.TextAmountTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.TextAmountColumn].ToString();
                this.AddressNameTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.NameColumn].ToString();
                this.Address1TextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.Address1Column].ToString();
                this.Address2TextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.Address2Column].ToString();
                this.CityStateTextBox.Text = string.Concat(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.CityColumn], " ", this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.StateColumn]);                
                this.ZipTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.ZipColumn].ToString();
                this.MemoValueLabel.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MemoColumn].ToString();
                if (string.IsNullOrEmpty(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.EntryDateColumn].ToString()))
                {
                    this.EntryDateLabel.Text = string.Empty;
                }
                else
                {
                    this.EntryDateLabel.Text = Convert.ToDateTime(this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.EntryDateColumn]).ToString("M/d/yyyy");
                }

                this.CheckNumberLabel.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.CheckNumberColumn].ToString();                
                this.AuthorizedSignatureValue1Label.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AuthorizedSignatureColumn].ToString();
                this.AuthorizedSignatureValue2Label.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AuthorizedSignatureColumn].ToString();
                this.OfficeName1Label.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.OfficeNameColumn].ToString();
                this.OfficeName2Label.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.OfficeNameColumn].ToString();
                this.OfficeLocationLabel.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.OfficeLocationColumn].ToString();
                ////Detail Section
                this.FromAccountTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AccountNameColumn].ToString();
                this.AgencyTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.AgencyNameColumn].ToString();
                this.CLTypeTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.CLTypeColumn].ToString();
                this.UserTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.UserNameColumn].ToString();
                this.ClearedByTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.ClearedByColumn].ToString();
                this.ClearedDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.ClearedDateColumn].ToString();                
                this.PrintedByTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PrintedByColumn].ToString();
                this.PrintedDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.PrintedDateColumn].ToString();
                this.MailedByTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedByColumn].ToString();
                this.MailedDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.MailedDateColumn].ToString();
                this.VoidByTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.VoidByColumn].ToString();
                this.VoidDateTextBox.Text = this.checkDetail.GetCheckDetail.Rows[0][this.checkDetail.GetCheckDetail.VoidDateColumn].ToString();      
                ////enable audit link label
                this.CashLedgerAuditlinkLabel.Text = string.Concat(SharedFunctions.GetResourceString("CheckDetailAuditText"), this.currentclId);
                this.CashLedgerAuditlinkLabel.Enabled = true;
                ////refresh subfund items
                this.ItemListingPanel.Enabled = true;
                this.ItemListingGridView.DataSource = this.checkDetail.ListSubFundItem.DefaultView;         
                if (this.checkDetail.ListSubFundItem.Rows.Count > this.ItemListingGridView.NumRowsVisible)
                {
                    this.ItemListingVscrollBar.Visible = false;
                }
                else
                {
                    this.ItemListingVscrollBar.Visible = true;
                }  
              
                ////Check cleared status
                if (!String.IsNullOrEmpty(this.ClearedByTextBox.Text) && !String.IsNullOrEmpty(this.ClearedDateTextBox.Text))
                {
                    this.ClearedButton.StatusIndicator = true;
                }
                else
                {
                    this.ClearedButton.StatusIndicator = false;
                }

                ////Check printed status
                if (!String.IsNullOrEmpty(this.PrintedByTextBox.Text) && !String.IsNullOrEmpty(this.PrintedDateTextBox.Text))
                {
                    this.PrintedButton.StatusIndicator = true;
                }
                else
                {
                    this.PrintedButton.StatusIndicator = false;
                }

                ////Check mailed status
                if (!String.IsNullOrEmpty(this.MailedByTextBox.Text) && !String.IsNullOrEmpty(this.MailedDateTextBox.Text))
                {
                    this.MailedButton.StatusIndicator = true;
                }
                else
                {
                    this.MailedButton.StatusIndicator = false;
                }

                ////Check void status
                if (!String.IsNullOrEmpty(this.VoidByTextBox.Text) && !String.IsNullOrEmpty(this.VoidDateTextBox.Text))
                {
                    this.VoidButton.StatusIndicator = true;
                }
                else
                {
                    this.VoidButton.StatusIndicator = false;
                }

                ////Set status button property
                this.StatusButtonsPanel.Enabled = true && this.PermissionEdit;
                ////Used to set record count of attachment and comments.
                this.AddtionalOperationDeckWorkspace.Enabled = true;
                this.SetAdditionalOperationCount(true);
            }
            else
            {
                this.ClearCheckDetail();
            }
        }

        #endregion

        #region Clear Check Detail

        /// <summary>
        /// Method will Clear the PaymentEngine DataGrid
        /// </summary>
        private void ClearCheckDetail()
        {
            ////Clears check and detail section
            this.ClearCheckDetailSection();           
            ////clears other fields
            this.ClearOtherFields();
        }

        /// <summary>
        /// Clears the check detail section.
        /// </summary>
        private void ClearCheckDetailSection()
        {
            ////Detail Section
            this.FromAccountTextBox.Text = String.Empty;
            this.AgencyTextBox.Text = String.Empty;
            this.CLTypeTextBox.Text = String.Empty;
            this.UserTextBox.Text = String.Empty;
            this.ClearedByTextBox.Text = String.Empty;
            this.ClearedDateTextBox.Text = String.Empty;
            this.PrintedByTextBox.Text = String.Empty;
            this.PrintedDateTextBox.Text = String.Empty;
            this.MailedByTextBox.Text = String.Empty;
            this.MailedDateTextBox.Text = String.Empty;
            this.VoidByTextBox.Text = String.Empty;
            this.VoidDateTextBox.Text = String.Empty;
            ////Check Section
            this.PayableToValueLabel.Text = String.Empty;
            this.AmountValueLabel.Text = String.Empty;
            this.MemoValueLabel.Text = String.Empty;
            this.EntryDateLabel.Text = String.Empty;
            this.CheckNumberLabel.Text = String.Empty;
            this.TextAmountTextBox.Text = String.Empty;
            this.AddressNameTextBox.Text = String.Empty;
            this.Address1TextBox.Text = String.Empty;
            this.Address2TextBox.Text = String.Empty;
            this.CityStateTextBox.Text = String.Empty;
            this.ZipTextBox.Text = String.Empty;
            this.AuthorizedSignatureValue1Label.Text = String.Empty;
            this.AuthorizedSignatureValue2Label.Text = String.Empty;
            this.OfficeName1Label.Text = String.Empty;
            this.OfficeName2Label.Text = String.Empty;
            this.OfficeLocationLabel.Text = String.Empty;
            ////Clear Item Listing
            this.checkDetail.ListSubFundItem.Clear();
            this.ItemListingGridView.DataSource = this.checkDetail.ListSubFundItem;
            this.ItemListingPanel.Enabled = false;
        }

        /// <summary>
        /// Method will Reset the values in the Check Detail Form 
        /// </summary>
        private void ClearOtherFields()
        {
            ////reset attachment and comments
            this.AddtionalOperationDeckWorkspace.Enabled = false;            
            this.SetAdditionalOperationCount(false);
            ////clear audit link
            this.CashLedgerAuditlinkLabel.Text = SharedFunctions.GetResourceString("CheckDetailAuditText");
            this.CashLedgerAuditlinkLabel.Enabled = false;
            ////Clear Status buttons
            this.ClearedButton.StatusIndicator = false;            
            this.PrintedButton.StatusIndicator = false;            
            this.MailedButton.StatusIndicator = false;            
            this.VoidButton.StatusIndicator = false;
            this.StatusButtonsPanel.Enabled = false;            
           
            ////reset navigation buttons
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            if (this.checkDetail.ListCashLedgerID.Rows.Count == 0)
            {
                ////disable form if record not exists
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { 0, 0 }));

                this.RecordNavigatorDeckWorkspace.Enabled = false;
                this.CLDetailPanel.Enabled = false;
                this.ToolBoxDeckWorkspace.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                ////this.DeleteCLButton.Enabled = false;                
            }

            ////if value not exists
            if (!this.currentclId.HasValue)
            {
                this.EditCLButton.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the query by form fields.
        /// </summary>
        private void ClearQueryByFormFields()
        {
            ////reset clid
            this.CurrentclId = null;
            ////Set page status
            this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
            this.FormFilterType = TerraScanCommon.FilterType.None;
            this.FilterTypeId = 0;
            ////set necessary controls property            
            this.statusBarSmartPart.FilteredButtonFilterStatus = false;
            this.toolBoxSmartPart.EnableClearFilterButton = false;
            this.UserDefinedWhereCondition = String.Empty;
            this.WhereCondition = string.Empty;
            this.SetControlsProperty();
        }

        #endregion

        #region Filter

        /// <summary>
        /// Handles the Click event of the QueryByFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QueryByFormFunction(object sender, DataEventArgs<Button> e)
        {
            ////reset clid
            this.CurrentclId = null;

            if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////which filters record set with quering fields
                this.FilterRecordSet();
            }
            else
            {
                ////clear the values to display
                this.toolBoxSmartPart.EnableClearFilterButton = true;

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredQueryByForm;
                }
                else
                {
                    ////reset FilterTypeId
                    this.FilterTypeId = 0;
                    this.PageStatus = TerraScanCommon.PageStatus.QueryByForm;
                }

                ////clear the values to display
                this.ClearCheckDetail();
                this.statusBarSmartPart.FilteredButtonFilterStatus = false;
                this.SetControlsProperty();
                this.EntryDateTextBox.Focus();
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearFilterFunction(object sender, DataEventArgs<Button> e)
        {
            ////set necessary controls property
            if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm))
            {
                ////clear query related fields
                this.ClearQueryByFormFields();
            }
            else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////Set page status
                this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                this.statusBarSmartPart.FilteredButtonFilterStatus = true;
                this.toolBoxSmartPart.EnableClearFilterButton = true;
                this.SetControlsProperty();
            }

            //// FillCheckDetails function is used to fill the check details                         
            this.FillCheckDetails(null, false);
        }

        /// <summary>
        /// Filter the current record set with criteria entered
        /// </summary>        
        private void FilterRecordSet()
        {
            ////changing cursor type
            this.Cursor = Cursors.WaitCursor;

            ////used to store parsed where condition           
            StringBuilder whereClause = new StringBuilder(String.Empty);
            ////used for requery
            StringBuilder userFriendlyWhereCondition = new StringBuilder(String.Empty);
            ////true when the query execution succeeded
            bool querySucceeded = false;
            ////true when query is invalid
            bool invalidQuery = false;

            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            ////getformatted sql condition
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArray, queryingFields.CheckDetailQueryingFields, whereClause, userFriendlyWhereCondition, ref invalidQuery);

            ////if record not found
            if (!invalidQuery && whereClause.Length == 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryEntryMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////focus for query by form
                this.EntryDateTextBox.Focus();
                this.Cursor = Cursors.Default;
                return;
            }

            QueryData queryData = new QueryData();

            try
            {
                ////checks for any criteria
                if (whereClause.Length > 0 && !invalidQuery)
                {
                    if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm) && this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                    {
                        queryData = this.form1226Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, whereClause.ToString(), null, this.ParentFormId);
                    }
                    else
                    {
                        queryData = this.form1226Control.WorkItem.ExecuteQuery(whereClause.ToString(), null, this.ParentFormId);
                    }

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        querySucceeded = true;
                    }
                    else
                    {
                        querySucceeded = false;
                    }
                }
            }
            catch
            {
                ////TODO : Need to find specific exception and handle it.                
                invalidQuery = true;
                querySucceeded = false;
            }
            finally
            {
                if (querySucceeded)
                {
                    if (queryData.SearchedCountResult.Rows.Count > 0)
                    {
                        MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }

                    this.WhereCondition = whereClause.ToString();
                    this.UserDefinedWhereCondition = userFriendlyWhereCondition.ToString();
                    this.toolBoxSmartPart.EnableClearFilterButton = true;

                    ////Set page status
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.statusBarSmartPart.FilteredButtonFilterStatus = true;
                    this.SetControlsProperty();

                    //// FillStatementDetails function is used to fill the Statement details in ExciseTaxStatement                                         
                    this.FillCheckDetails(queryData.ListKeyId, false);                    
                }
                else
                {
                    if (invalidQuery)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    ////focus for query by form
                    this.EntryDateTextBox.Focus();
                }

                ////sets default cursor
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Query

        /// <summary>
        /// opens queryutility form and performs necessary action
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private void QueryUtilityFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////required parameter for calling form
                this.toolBoxSmartPart.ToolBoxEntity.CurrentFormId = this.ParentFormId;
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.Form1226Control.WorkItem;
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    this.toolBoxSmartPart.ToolBoxEntity.WhereCondition = this.WhereCondition;
                    this.toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition = this.UserDefinedWhereCondition;                    
                }
                else
                {
                    this.toolBoxSmartPart.ToolBoxEntity.WhereCondition = string.Empty;
                    this.toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition = string.Empty;                   
                }

                ////which shows QueryUtility form and set required parameter
                this.toolBoxSmartPart.ShowQueryUtilityForm();
               
                this.Cursor = Cursors.Default;
                ////suceessful queryutility load
                if (this.toolBoxSmartPart.ToolBoxEntity.CalledFormStatus)
                {
                    this.Cursor = Cursors.WaitCursor;
                    QueryData queryData = new QueryData();
                    if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                    {
                        ////specific for snapshot - filter                            
                        //// Load tempDataSet will contain query and where condition in Table[1] and all filtered statementIds in Tables[0]
                        queryData = this.form1226Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.toolBoxSmartPart.ToolBoxEntity.WhereCondition, null, this.ParentFormId);
                    }
                    else
                    {
                        ////specific for filtered records
                        this.FilterTypeId = this.toolBoxSmartPart.ToolBoxEntity.KeyId;

                        // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                        queryData = this.form1226Control.WorkItem.GetQueryResult(this.FilterTypeId, null);
                    }

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentclId = null;
                        ////specific to snapshot filter
                        if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                        {
                            MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        }
                        else
                        {
                            ////specific for query filtered records
                            this.FormFilterType = TerraScanCommon.FilterType.Query;
                        }

                        this.UserDefinedWhereCondition = this.toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition;
                        this.WhereCondition = this.toolBoxSmartPart.ToolBoxEntity.WhereCondition;
                        this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                        //// FillCheckDetails function is used to fill the check details in check 
                        this.FillCheckDetails(queryData.ListKeyId, false);
                        this.statusBarSmartPart.FilteredButtonFilterStatus = true;
                        this.toolBoxSmartPart.EnableClearFilterButton = true;
                        this.SetControlsProperty();
                    }
                    else
                    {
                        if (!this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                        {
                            ////specific for filtered records                        
                            this.FilterTypeId = 0;
                        }

                        MessageBox.Show(SharedFunctions.GetResourceString("NoRecord"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        #endregion

        #region SanpShot

        /// <summary>
        /// Handles the Click event of the SnapshotUtilityButton control.opens SnapshotUtility form and performs necessary action
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>       
        private void SnapshotUtilityFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string snapshotIdsXml = string.Empty;
                int snapshotIdsCount = 0;

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {                   
                    snapshotIdsXml = Utility.GetXmlString(this.checkDetail.ListCashLedgerID.Copy());
                    snapshotIdsCount = this.checkDetail.ListCashLedgerID.Rows.Count;
                }

                this.Cursor = Cursors.Default;                
                ////required parameter for calling form
                this.toolBoxSmartPart.ToolBoxEntity.CurrentFormId = this.ParentFormId;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotIdXmlString = snapshotIdsXml;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotCount = snapshotIdsCount;
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.form1226Control.WorkItem;
                ////which shows SnapshotUtility form
                this.toolBoxSmartPart.ShowSnapshotUtilityForm();

                this.Cursor = Cursors.Default;

                if (this.toolBoxSmartPart.ToolBoxEntity.CalledFormStatus)
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                    QueryData queryData = this.form1226Control.WorkItem.GetSnapShotResult(this.toolBoxSmartPart.ToolBoxEntity.KeyId, null);

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentclId = null;
                        if (queryData.SearchedCountResult.Rows.Count > 0 && !int.Equals(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn]))
                        {
                            MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("OFString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordsFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("SnapshotMismatch")), MessageBoxButtons.OK, MessageBoxIcon.Information);                            
                        }

                        ////Assign required values specific for filtered records
                        this.FilterTypeId = this.toolBoxSmartPart.ToolBoxEntity.KeyId;
                        this.CurrentSnapshotName = this.toolBoxSmartPart.ToolBoxEntity.SnapshotName;
                        this.CurrentSnapshotDescription = this.toolBoxSmartPart.ToolBoxEntity.SnapshotDescription;
                        ////specific for filtered records
                        this.FormFilterType = TerraScanCommon.FilterType.SnapShot;
                        this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;

                        ////FillCheckDetails function is used to fill the Check details in Check 
                        this.FillCheckDetails(queryData.ListKeyId, false);
                        this.statusBarSmartPart.FilteredButtonFilterStatus = true;
                        this.toolBoxSmartPart.EnableClearFilterButton = true;

                        this.UserDefinedWhereCondition = String.Empty;
                        this.WhereCondition = String.Empty;
                        this.SetControlsProperty();
                    }
                    else
                    {
                        if (!this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                        {
                            ////specific for filtered records                        
                            this.FilterTypeId = 0;
                        }

                        MessageBox.Show(SharedFunctions.GetResourceString("NoRecord"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        #endregion

        #region Requery

        /// <summary>
        /// Handles the Click event of the FilteredButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReQueryFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;                

                if (!this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                {
                    this.CurrentSnapshotName = "N/A";
                    this.CurrentSnapshotDescription = "N/A";
                }

                F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();                             
                ////required parameter for calling form
                this.statusBarSmartPart.StatusBarEntity.OptionalInputParameter = new object[] { this.WhereCondition, this.CurrentSnapshotName, this.CurrentSnapshotDescription, queryingFields.CheckDetailQueryingFields };
                this.statusBarSmartPart.StatusBarEntity.ParentWorkItem = this.form1226Control.WorkItem;                
                
                this.Cursor = Cursors.Default;

                while (true)
                {
                    try
                    {
                        ////which show requery form
                        this.statusBarSmartPart.ShowRequeryForm();
                        if (this.statusBarSmartPart.StatusBarEntity.CalledFormStatus)
                        {
                            QueryData queryData = new QueryData();                            

                            if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                            {
                                queryData = this.form1226Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }
                            else
                            {
                                queryData = this.form1226Control.WorkItem.ExecuteQuery(this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }

                            if (queryData.ListKeyId.Rows.Count > 0)
                            {
                                this.CurrentclId = null;
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                this.WhereCondition = this.statusBarSmartPart.StatusBarEntity.WhereCondition;
                                this.UserDefinedWhereCondition = this.statusBarSmartPart.StatusBarEntity.UserDefinedWhereCondition;
                                this.toolBoxSmartPart.EnableClearFilterButton = true;
                                ////Set page status
                                this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                                this.statusBarSmartPart.FilteredButtonFilterStatus = true;
                                //// FillCheckDetails function is used to fill the check details in check                                
                                this.FillCheckDetails(queryData.ListKeyId, false);
                                this.SetControlsProperty();
                                break;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                continue;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidQuery") + "\n" + SharedFunctions.GetResourceString("QueryModificationMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                //ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeItemListingGridView
        /// </summary>
        private void CustomizeItemListingGridView()
        {
            DataGridViewColumnCollection columns = this.ItemListingGridView.Columns;

            columns["SubFund"].DataPropertyName = this.checkDetail.ListSubFundItem.SubFundColumn.ColumnName;
            columns["Description"].DataPropertyName = this.checkDetail.ListSubFundItem.DescriptionColumn.ColumnName;
            columns["Amount"].DataPropertyName = this.checkDetail.ListSubFundItem.AmountColumn.ColumnName;
            columns["RegisterItemId"].DataPropertyName = this.checkDetail.ListSubFundItem.RegisterItemIDColumn.ColumnName;

            columns["SubFund"].DisplayIndex = 0;
            columns["Description"].DisplayIndex = 1;
            columns["Amount"].DisplayIndex = 2;
            columns["RegisterItemId"].DisplayIndex = 3;            
        }

        /// <summary>
        /// Handles the CellFormatting event of the ItemListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ItemListingGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Decimal outDecimal;
                //// Only paint if desired, formattable column
                if (e.ColumnIndex == this.ItemListingGridView.Columns["Amount"].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value))
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
            }
            catch (Exception)
            { 
            }
        }
      
        /// <summary>
        /// enbling/disabling buttons
        /// </summary>
        private void SetControlsProperty()
        {
            ////setting controls property depending on pagestatus

            if (this.PageStatus.Equals(TerraScanCommon.PageStatus.NormalMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
            {
                ////reset Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, false, null, Color.Empty);
                this.CLDetailPanel.BackColor = Color.White;
                this.CheckNumberPanel.BackColor = Color.FromArgb(191, 203, 208);
                ////set querying field visibility - Check Section   
                this.EntryDateLabel.Visible = true;
                this.CheckNumberLabel.Visible = true;
                this.PayableToValueLabel.Visible = true;
                this.AmountValueLabel.Visible = true;
                this.MemoValueLabel.Visible = true;                
                this.EntryDateTextBox.Visible = false;
                this.CheckNumberTextBox.Visible = false;
                this.PayableToTextBox.Visible = false;
                this.AmountTextBox.Visible = false;
                this.MemoTextBox.Visible = false; 

                this.ActionButtonsPanel.Enabled = true;
                this.RecordNavigatorDeckWorkspace.Enabled = true;
                this.StatusBarDeckWorkspace.Enabled = true;                
            }
            else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////set Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, true, this.userDefinedWhereCondition, Color.FromArgb(204, 255, 204));
                this.CLDetailPanel.BackColor = Color.FromArgb(204, 255, 204);
                this.CheckNumberPanel.BackColor = Color.FromArgb(204, 255, 204);
                ////enable requried control
                this.EntryDateTextBox.Visible = true;
                this.CheckNumberTextBox.Visible = true;
                this.PayableToTextBox.Visible = true;
                this.AmountTextBox.Visible = true;
                this.MemoTextBox.Visible = true;
                this.EntryDateLabel.Visible = false;
                this.CheckNumberLabel.Visible = false;
                this.PayableToValueLabel.Visible = false;
                this.AmountValueLabel.Visible = false;
                this.MemoValueLabel.Visible = false;

                this.ActionButtonsPanel.Enabled = false;
                this.RecordNavigatorDeckWorkspace.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;                             
            }
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        /// <param name="onload">if set to <c>true</c> [onload].</param>
        private void SetAdditionalOperationCount(bool onload)
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                if (this.additionalOperationSmartPart != null)
                {                    
                    AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);                    
                    if (onload && this.currentclId.HasValue)
                    {                        
                        additionalOperationCountEntity.AttachmentCount = this.form1226Control.WorkItem.GetAttachmentCount(this.ParentFormId, this.currentclId.Value, TerraScanCommon.UserId);
                        CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1226Control.WorkItem.GetCommentsCount(this.ParentFormId, this.currentclId.Value, TerraScanCommon.UserId);
                        if (commentsCountDataTable.Rows.Count > 0)
                        {
                            additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                            additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                        }                     
                    }

                    this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;                   
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }       

        /// <summary>
        /// Sets the textbox  QueryingFileldName
        /// </summary>
        private void SetQueryingFieldName()
        {
            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            this.PayableToTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.PAYABLETOColumn.ColumnName;
            this.CheckNumberTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.CHECKNUMBERColumn.ColumnName;
            this.EntryDateTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.CHECKDATEColumn.ColumnName;            
            this.AmountTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.AMOUNTColumn.ColumnName;
            this.MemoTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.MEMOColumn.ColumnName;
            this.FromAccountTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.FROMACCOUNTColumn.ColumnName;
            this.AgencyTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.AGENCYColumn.ColumnName;
            this.CLTypeTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.TYPEColumn.ColumnName;
            this.UserTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.USERNAMEColumn.ColumnName;
            this.ClearedByTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.CLEAREDBYColumn.ColumnName;
            this.ClearedDateTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.CLEAREDDATEColumn.ColumnName;
            this.PrintedByTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.PRINTEDBYColumn.ColumnName;
            this.PrintedDateTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.PRINTEDDATEColumn.ColumnName;
            this.MailedByTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.MAILEDBYColumn.ColumnName;
            this.MailedDateTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.MAILEDDATEColumn.ColumnName;
            this.VoidByTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.VOIDBYColumn.ColumnName;
            this.VoidDateTextBox.QueryingFileldName = queryingFields.CheckDetailQueryingFields.VOIDDATEColumn.ColumnName;
        }

        #endregion    

        #region Edit

        /// <summary>
        /// Handles the Click event of the EditCLButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditCLButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentclId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Edit Check Form - FormID - 1221
                    Form editCheckForm = this.form1226Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1221, new object[] { this.currentclId.Value }, this.form1226Control.WorkItem);
                    if (editCheckForm != null)
                    {
                        ////check form updated - dialogresult yes
                        if (editCheckForm.ShowDialog() == DialogResult.Yes)
                        {
                            this.FillCheckDetails(null, false);
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
                this.Cursor = Cursors.Default;
            } 
        }

        #endregion   
     
        //#region Delete

        ///// <summary>
        ///// Handles the Click event of the DeleteCLButton control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        //private void DeleteCLButton_Click(object sender, EventArgs e)
        //{
        //    ////confirmation for delete
        //    if (this.currentclId.HasValue && MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        try
        //        {
        //            this.Cursor = Cursors.WaitCursor;
        //            ////deelte current record
        //            this.form1226Control.WorkItem.F1226_DeleteCashLedger(this.currentclId.Value);
        //            ////refresh record set                    
        //            this.FillCheckDetails(null, true);
        //        }
        //        catch (SoapException ex)
        //        {
        //            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //        }
        //        catch (Exception ex)
        //        {
        //            ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //        }
        //        finally
        //        {
        //            this.Cursor = Cursors.Default;
        //        }
        //    } 
           
        //    ////else retain focus
        //}

        //#endregion

        #region Status Buttons

        /// <summary>
        /// Handles the Click event of the ClearedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearedButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentclId.HasValue)
                {
                    this.Cursor = Cursors.Default;
                    int userId = TerraScanCommon.UserId;
                    ////check status
                    if (this.ClearedButton.StatusIndicator)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("CheckDetailClearedStatus"), SharedFunctions.GetResourceString("TerraScan T2 - Unclear Check"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        ////invalid userid
                        userId = -999;
                    }

                    ////update cleared status to the db - cleared status validation checked
                    this.form1226Control.WorkItem.F1226_UpdateCashLedgerStatus(this.currentclId.Value, userId, DateTime.Now, (int)TerraScanCommon.CheckStatusOrder.Cleared, TerraScanCommon.UserId);
                    ////refresh the form record set
                    this.FillCheckDetails(null, false);
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
        /// Handles the Click event of the PrintedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintedButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentclId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////used to generate report parameter xml
                    this.checkDetail.ReportParameters.Rows.Clear();
                    CheckDetailData.ReportParametersRow reportParametersDataRow = this.checkDetail.ReportParameters.NewReportParametersRow();
                    reportParametersDataRow.CLID = this.currentclId.Value;
                    this.checkDetail.ReportParameters.Rows.Add(reportParametersDataRow);
                    Hashtable reportOptionalParameter = new Hashtable();
                    ////// calling  Common Function For Report              
                    reportOptionalParameter.Add("CLIDs", Utility.GetXmlString(this.checkDetail.ReportParameters.Copy()));
                    //changed the parameter type from string to int
                    TerraScanCommon.ShowReport(122400, Report.ReportType.Print, reportOptionalParameter);
                    ////update printed status to the db - cleared status validation checked
                    this.form1226Control.WorkItem.F1226_UpdateCashLedgerStatus(this.currentclId.Value, TerraScanCommon.UserId, DateTime.Now, (int)TerraScanCommon.CheckStatusOrder.Printed, TerraScanCommon.UserId);
                    ////refresh the form record set
                    this.FillCheckDetails(null, false);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
        /// Handles the Click event of the MailedButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MailedButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentclId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Mailed Check Form - FormID - 1222
                    Form mailedCheckForm = this.form1226Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1222, new object[] { this.currentclId.Value }, this.form1226Control.WorkItem);
                    if (mailedCheckForm != null && mailedCheckForm.ShowDialog() == DialogResult.Yes)
                    {
                        ////refresh the form record set
                        this.FillCheckDetails(null, false);
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

        /// <summary>
        /// Handles the Click event of the VoidButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void VoidButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentclId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Void Check Form - FormID - 1223
                    Form voidCheckForm = this.form1226Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1223, new object[] { this.currentclId.Value }, this.form1226Control.WorkItem);
                    if (voidCheckForm != null && voidCheckForm.ShowDialog() == DialogResult.Yes)
                    {
                        ////refresh the form record set
                        this.FillCheckDetails(null, false);
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

        #endregion 

        #region Links

        /// <summary>
        /// Handles the LinkClicked event of the CashLedgerAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void CashLedgerAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (this.currentclId.Value > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    FormInfo formInfo;
                    formInfo = TerraScanCommon.GetFormInfo(90101);
                    formInfo.optionalParameters = new object[2];
                    formInfo.optionalParameters[0] = this.Tag;
                    formInfo.optionalParameters[1] = this.currentclId.Value;
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }

                ////this.Cursor = Cursors.WaitCursor;
                ////if (this.currentclId.HasValue)
                ////{
                ////    ////// calling  Common Function For Report
                ////    Hashtable reportOptionalParameter = new Hashtable();
                ////    reportOptionalParameter.Add("KeyName", "CLID");
                ////    reportOptionalParameter.Add("KeyValue", this.currentclId.Value);
                ////    //changed the parameter type from string to int
                ////    TerraScanCommon.ShowReport(122090, Report.ReportType.Preview, reportOptionalParameter);
                ////}                
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

        #endregion
        
        #endregion        
    }
}
