//--------------------------------------------------------------------------------------------
// <copyright file="F1020.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// Aug-14           Ranjani JG        	    Created// 
//*********************************************************************************/
namespace D1020
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
    using TerraScan.ReceiptEngine;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Form F1020
    /// </summary>
    [SmartPart]
    public partial class F1020 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// controller F1020
        /// </summary>
        private F1020Controller form1020Control;

        /// <summary>
        /// statementIds variable is used to store list of statementIds for RealEstate. 
        /// </summary>       
        private RealEstateData realEstate = new RealEstateData();

        /// <summary>
        /// currentRecordSetCount variable is used to find total number of records for the Current Record. 
        /// </summary>
        private int currentRecordSetCount;        

        /// <summary>
        /// WhereCondition variable is used execute current records. 
        /// </summary>  
        private string whereCondition = String.Empty;

        /// <summary>
        /// userDefinedWhereCondition variable is used to store user defined where condition. 
        /// </summary>
        private string userDefinedWhereCondition = String.Empty;

        /// <summary>
        /// formFilterType variable is used to find the type of filter in the form. 
        /// </summary>   
        private TerraScanCommon.FilterType formFilterType;

        /// <summary>
        /// currentPageStatus variable is used to find the status in the form. 
        /// </summary>   
        private TerraScanCommon.PageStatus currentPageStatus;

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
        /// used to pass Report Parameter
        /// </summary>
        private Hashtable reportOptionalParameter = new Hashtable();

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
        /// currentExciseTaxStatementId variable is used to store ExciseTaxStatement id. 
        /// </summary>       
        private int? currentStatementId = null;

        /// <summary>
        /// Query Control Array
        /// </summary>
        private TerraScanTextBox[] queryControlArray;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1020"/> class.
        /// </summary>
        public F1020()
        {
            this.Load += new EventHandler(this.F1020_Load);
            this.InitializeComponent();
            //// this.FormClosing += new FormClosingEventHandler(this.RealEstateMasterForm_FormClosing);
            ////Savedevent subscription
            this.ReceiptEngineUserControl.ParentFormId = Convert.ToInt32(this.Tag);           
            this.ReceiptEngineUserControl.PermissionDataset = TerraScanCommon.FormPermissionsDataSet;
            this.ReceiptEngineUserControl.SavedEvent += new ReceiptEngineUserControl.SavedEventHandler(this.ReceiptEngineUserControl_SavedEvent);           
            ////PageModeChanged Subcription
            this.ReceiptEngineUserControl.DelinquencyChangedEvent += new ReceiptEngineUserControl.DelinquencyChangedEventHandler(this.ReceiptEngineUserControl_DelinquencyChangedEvent);
            ////sets tags of the control with query field name
            this.SetQueryFieldName();
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Statement", 174, 150, 94);
            this.ReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptPictureBox.Height, this.ReceiptPictureBox.Width, "Receipt", 28, 81, 128);
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
        /// Declare the event SetActiveRecordButtons        
        /// </summary> 
        [EventPublication(EventTopics.D1100_F1100_ExciseTaxSmartPart_SetAutoPrint, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<bool>> SetAutoPrint;

        /// <summary>
        /// Declare the event PageStatusActivated        
        /// </summary> 
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        #endregion Event Publication      

        #region Property

        /// <summary>
        /// For F1020Control
        /// </summary>
        [CreateNew]
        public F1020Controller Form1020Control
        {
            get { return this.form1020Control as F1020Controller; }
            set { this.form1020Control = value; }
        }

        /// <summary>
        /// Gets or sets the current statement id.
        /// </summary>
        /// <value>The current statement id.</value>
        private int? CurrentStatementId
        {
            get 
            {
                return this.currentStatementId;
            }

            set 
            {
                this.currentStatementId = value;
                if (this.additionalOperationSmartPart != null)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentStatementId ?? -1;
                }
            }
        }  

        /// <summary>
        /// Gets or sets the userDefinedWhereCondition
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
        /// Gets or sets the currentRecordSetCount
        /// </summary>
        /// <value>The current RecordSet Count.</value>
        [Description("Display Data based on currentRecordSetCount.")]
        private int CurrentRecordSetCount
        {
            get { return this.currentRecordSetCount; }
            set { this.currentRecordSetCount = value; }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotName
        /// </summary>
        /// <value>The current Snapshot Name.</value>
        [Description("Display Data based on currentRecordSetCount.")]
        private string CurrentSnapshotName
        {
            get { return this.currentSnapshotName; }
            set { this.currentSnapshotName = value; }
        }

        /// <summary>
        /// Gets or sets the CurrentSnapshotDescription
        /// </summary>
        /// <value>The current Snapshot Description.</value>
        [Description("Display Data based on currentRecordSetCount.")]
        private string CurrentSnapshotDescription
        {
            get { return this.currentSnapshotDescription; }
            set { this.currentSnapshotDescription = value; }
        }

        /// <summary>
        /// Gets or sets the currentPageStatus
        /// </summary>
        /// <value>The current Page Status.</value>
        [Description("Display Data based on page status.")]
        private TerraScanCommon.PageStatus CurrentPageStatus
        {
            get { return this.currentPageStatus; }
            set { this.currentPageStatus = value; }
        }

        /// <summary>
        /// Gets or sets the FormFilterType
        /// </summary>
        /// <value>The formFilterType.</value>
        [Description("Display Data based on Filtered Type.")]
        private TerraScanCommon.FilterType FormFilterType
        {
            get { return this.formFilterType; }
            set { this.formFilterType = value; }
        }

        /// <summary>
        /// Gets or sets the FilterTypeId
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
        /// Gets or sets the whereCondition
        /// </summary>
        /// <value>The whereCondition.</value>
        [Description("Display Data based on WhereCond.")]
        private string WhereCondition
        {
            get { return this.whereCondition; }
            set { this.whereCondition = value.ToUpper(); }
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
            if (this.ReceiptEngineUserControl.CheckPageStatus(true))
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
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
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.CurrentStatementId = this.RetrieveStatementId(recordNavigationEntity.RecordIndex);
            this.FillStatementDetails(null, recordNavigationEntity.RecordNavigationFlag);
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryByFromButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.ReceiptEngineUserControl.CheckPageStatus(true))
            {
                this.QueryByFormFunction(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Clears the filter button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_ClearFilterButtonClick, Thread = ThreadOption.UserInterface)]
        public void ClearFilterButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.ReceiptEngineUserControl.CheckPageStatus(true))
            {
                this.ClearFilterFunction(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Queries the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.ReceiptEngineUserControl.CheckPageStatus(false))
            {
                this.QueryUtilityFunction(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Snapshots the utility button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_SnapshotUtilityButtonClick, Thread = ThreadOption.UserInterface)]
        public void SnapshotUtilityButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.ReceiptEngineUserControl.CheckPageStatus(false))
            {
                this.SnapshotUtilityFunction(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick, Thread = ThreadOption.UserInterface)]
        public void FilteredButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.ReceiptEngineUserControl.CheckPageStatus(false))
            {
                this.ReQueryFunction(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Autoes the print on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_AutoPrintOnButtonClick, Thread = ThreadOption.UserInterface)]
        public void AutoPrintOnButtonClick(object sender, DataEventArgs<bool> e)
        {
            this.Form1020Control.WorkItem.SaveAutoPrint(int.Parse(this.Tag.ToString()), TerraScanCommon.UserId, e.Data);
        }

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.Form1020Control.WorkItem.State["FormStatus"] = this.ReceiptEngineUserControl.CheckPageStatus(false);
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
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                if (this.ReceiptEngineUserControl.CheckPageStatus(true))
                {
                    if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("QueryByFormModeChange"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("RecordConflict")), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        this.ClearQueryByFormFields();
                    }
                    else if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
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

                    this.CurrentStatementId = Convert.ToInt32(optionalParams[0]);
                    this.FillStatementDetails(null, false);
                }
            }       
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
            // TODO : Genralized 
            try
            {
                this.Cursor = Cursors.WaitCursor;

                /////  this.ShowReport(Report.ReportType.Print, "11001", "ReceiptID", this.ReceiptEngineUserControl.CurrentReceiptId);

                //// Calling the Common Function for Report
                if (this.currentStatementId.HasValue && this.ReceiptEngineUserControl.CurrentReceiptId.HasValue)
                {
                    this.reportOptionalParameter.Clear();
                    ////this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("ReceiptID", this.ReceiptEngineUserControl.CurrentReceiptId);
                    //changed the parameter type from string to int
                    TerraScanCommon.ShowReport(10202, Report.ReportType.Print, this.reportOptionalParameter);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NoActiveReceipt"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, Thread = ThreadOption.UserInterface)]
        public void PreviewButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.currentStatementId.HasValue && this.ReceiptEngineUserControl.CurrentReceiptId.HasValue)
                {
                    ////// calling  Common Function For Report
                    this.reportOptionalParameter.Clear();
                   //// this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("ReceiptID", this.ReceiptEngineUserControl.CurrentReceiptId);
                    //changed the parameter type from string to int
                    TerraScanCommon.ShowReport(10202, Report.ReportType.Preview, this.reportOptionalParameter);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NoActiveReceipt"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the Click event of the EMailButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, Thread = ThreadOption.UserInterface)]
        public void EmailButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.currentStatementId.HasValue && this.ReceiptEngineUserControl.CurrentReceiptId.HasValue)
                {                    
                    ////// calling  Common Function For Report
                    this.reportOptionalParameter.Clear();
                    ////this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("ReceiptID", this.ReceiptEngineUserControl.CurrentReceiptId);
                    //changed the parameter type from string to int
                    TerraScanCommon.ShowReport(10202, Report.ReportType.Email, this.reportOptionalParameter);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NoActiveReceipt"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Handles the Click event of the DetailsButton control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_DetailsButtonClick, Thread = ThreadOption.UserInterface)]
        public void DetailsButtonClick(object sender, DataEventArgs<Button> e)
        {
            try
            {
                // TODO : Genralized 
                if (this.currentStatementId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.reportOptionalParameter.Clear();
                    //// Calling the Common Function for Report
                    ////this.reportOptionalParameter.Add("KeyName", "StatementID");
                    this.reportOptionalParameter.Add("StatementID", this.currentStatementId.Value);
                    //changed the parameter type from string to int
                    TerraScanCommon.ShowReport(10201, Report.ReportType.Preview, this.reportOptionalParameter);
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("NoActiveStatement"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion reports

        #region Help Engine

        /// <summary>
        /// Autoes the print on button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_HelpRealEstateButtonClick, Thread = ThreadOption.UserInterface)]
        public void HelpRealEstateButtonClick(object sender, DataEventArgs<int> e)
        {
            TerraScan.Common.HelpEngine.Show(ParentForm.Text, "1100");
        }

        #endregion

        #endregion

        #region UserDefinedFunction

        /// <summary>
        /// Clears the real estate details.
        /// </summary>
        public void ClearRealEstateDetails()
        {
            if (!this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.StatementIDTextBox.Text = "";
                this.StatementNumberTextBox.Text = "";
                this.RollYearTextBox.Text = "";
                this.LevyYearTextBox.Text = "";
                this.DistrictTextBox.Text = "";
                this.TotalValueTextBox.Text = "";
                this.OriginalTaxTextBox.Text = "";
                this.SitusTextBox.Text = "";
                this.ExemptionsTextBox.Text = "";
                this.DeductionsTextBox.Text = "";
                this.LegalTextBox.Text = "";
                this.TaxableValueTextBox.Text = "";
                this.TaxBillableTextBox.Text = "";
                this.MapTextBox.Text = "";
                this.OwnerTextBox.Text = "";
                this.ParcelIDTextBox.Text = "";
                this.ParcelNumberTextBox.Text = "";
            }

            this.statusBarSmartPart.DelinquentButtonStatus = false;
            this.ParcelIDlinkLabel.Text = "";
            this.OwnerlinkLabel.Text = "";
            this.StatementAuditlinkLabel.Text = "tTR_Statement [StatementID]";
            this.StatementAuditlinkLabel.Enabled = false;

            this.AdditionalOperationDeckWorkspace.Enabled = false;
            this.SetAdditionalOperationCount();

            ////reset navigation buttons
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            if (this.realEstate.ListRealPropertyStatementID.Rows.Count == 0)
            {
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { 0, 0 }));

                this.ReportActionDeckWorkspace.Enabled = false;
                this.ToolBoxDeckWorkspace.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                this.RecordNavigatorDeckWorkspace.Enabled = false;

                this.ReceiptEngineUserControl.Enabled = false;
                this.TopPanel.Enabled = false;
            }
        }

        /// <summary>
        /// Clears the query by form fields.
        /// </summary>
        private void ClearQueryByFormFields()
        {
            ////clears stored ids
            this.CurrentStatementId = null;            
            ////Set page status
            this.CurrentPageStatus = TerraScanCommon.PageStatus.NormalMode;
            this.FormFilterType = TerraScanCommon.FilterType.None;
            this.FilterTypeId = 0;
            ////set necessary controls property
            this.toolBoxSmartPart.EnableClearFilterButton = false;
            this.SetFilteredControlsProperty(false);
            this.UserDefinedWhereCondition = String.Empty;
            this.WhereCondition = string.Empty;
            this.SetControlsProperty();           
        }

        /// <summary>
        /// Set Property of the form Controls Based on Conditions 
        /// </summary>
        private void SetControlsProperty()
        {
            if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////set sort button visibility
                this.SetSortButtonVisibility(false);
                ////set Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, true, this.userDefinedWhereCondition, Color.FromArgb(204, 255, 204));
                this.TopPanel.BackColor = Color.FromArgb(204, 255, 204);

                this.ParcelIDTextBox.Visible = true;
                this.OwnerTextBox.Visible = true;
                this.ParcelIDlinkLabel.Visible = false;
                this.OwnerlinkLabel.Visible = false;

                this.ReceiptEngineUserControl.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                this.ReportActionDeckWorkspace.Enabled = false;
                this.AdditionalOperationDeckWorkspace.Enabled = false;
                this.RecordNavigatorDeckWorkspace.Enabled = false;
                this.OwnerLabel.Text = "Owner:";
            }
            else
            {
                ////set sort button visibility
                this.SetSortButtonVisibility(true);
                ////reset Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, false, null, Color.White);
                this.TopPanel.BackColor = Color.White;

                this.ParcelIDTextBox.Visible = false;
                this.OwnerTextBox.Visible = false;
                this.ParcelIDlinkLabel.Visible = true;
                this.OwnerlinkLabel.Visible = true;

                this.ReceiptEngineUserControl.Enabled = true;
                this.StatusBarDeckWorkspace.Enabled = true;
                this.ReportActionDeckWorkspace.Enabled = true;
                this.AdditionalOperationDeckWorkspace.Enabled = true;
                this.RecordNavigatorDeckWorkspace.Enabled = true;
            }
        }

        /// <summary>
        /// Sets the sort icon visibility.
        /// </summary>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        private void SetSortButtonVisibility(bool visibility)
        {
            this.StatementIDUpPictureBox.Visible = visibility;
            this.StatementIDDownPictureBox.Visible = visibility;
            this.StatementNumberUpPictureBox.Visible = visibility;
            this.StatementNumberDownPictureBox.Visible = visibility;
            this.RollYearUpPictureBox.Visible = visibility;
            this.RollYearDownPictureBox.Visible = visibility;
            this.LevyYearUpPictureBox.Visible = visibility;
            this.LevyYearDownPictureBox.Visible = visibility;
            this.DistrictUpPictureBox.Visible = visibility;
            this.DistrictDownPictureBox.Visible = visibility;
            this.ParcelIDUpPictureBox.Visible = visibility;
            this.ParcelIDDownPictureBox.Visible = visibility;
            this.ParcelNumberUpPictureBox.Visible = visibility;
            this.ParcelNumberDownPictureBox.Visible = visibility;
            this.MapUpPictureBox.Visible = visibility;
            this.MapDownPictureBox.Visible = visibility;
            this.TotalValueUpPictureBox.Visible = visibility;
            this.TotalValueDownPictureBox.Visible = visibility;
            this.OriginalTaxUpPictureBox.Visible = visibility;
            this.OriginalTaxDownPictureBox.Visible = visibility;
            this.TaxableValueUpPictureBox.Visible = visibility;
            this.TaxableValueDownPictureBox.Visible = visibility;
            this.TaxBillableUpPictureBox.Visible = visibility;
            this.TaxBillableDownPictureBox.Visible = visibility;
            this.SelectedPictureBox.Visible = visibility;
        }

        /// <summary>
        /// Sets the tagname with querying field name
        /// </summary>
        private void SetQueryFieldName()
        {
            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            this.StatementIDTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.STATEMENTIDColumn.ColumnName;
            this.StatementNumberTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.STATEMENTNUMBERColumn.ColumnName;
            this.RollYearTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.ROLLYEARColumn.ColumnName;
            this.LevyYearTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.LEVYYEARColumn.ColumnName;
            this.DistrictTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.DISTRICTColumn.ColumnName;
            this.TotalValueTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.TOTALVALUEColumn.ColumnName;
            this.OriginalTaxTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.ORIGINALTAXColumn.ColumnName;
            this.SitusTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.SITUSColumn.ColumnName;
            this.ExemptionsTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.TOTALEXEMPTIONSColumn.ColumnName;
            this.DeductionsTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.TOTALDEDUCTIONSColumn.ColumnName;
            this.LegalTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.LEGALColumn.ColumnName;
            this.TaxableValueTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.TAXABLEVALUEColumn.ColumnName;
            this.TaxBillableTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.TAXBILLEDColumn.ColumnName;
            this.MapTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.MAPNUMBERColumn.ColumnName;
            this.OwnerTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.OWNERNAMEColumn.ColumnName;
            this.ParcelIDTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.PARCELIDColumn.ColumnName;
            this.ParcelNumberTextBox.QueryingFileldName = queryingFields.RealEstateQueryingFields.PARCELNUMBERColumn.ColumnName;
        }

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the RealEstateMasterForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1020_Load(object sender, EventArgs e)
        {            
            try
            {
                this.CurrentStatementId = null;  
                this.LoadWorkSpaces();            
                this.SetAutoPrint(this, new DataEventArgs<bool>(Convert.ToBoolean(this.Form1020Control.WorkItem.GetAutoPrintStatus(Convert.ToInt32(this.Tag), TerraScanCommon.UserId))));
                this.ReceiptEngineUserControl.ParentWorkItem = this.form1020Control.WorkItem;
                ////PopulateStatement function is used to load the Statement in RealEstateform 
                this.FillStatementDetails(null, false);
                ////sets pagestatus and current record set count
                this.CurrentPageStatus = TerraScanCommon.PageStatus.NormalMode;               
                this.ParentForm.CancelButton = this.ReceiptEngineUserControl.ReceiptEngineCancelButton;
                ////sets QueryRelated Field
                this.queryControlArray = new TerraScanTextBox[] { this.StatementIDTextBox, this.StatementNumberTextBox, this.RollYearTextBox, this.LevyYearTextBox, this.DistrictTextBox, this.TotalValueTextBox, this.OriginalTaxTextBox, this.SitusTextBox, this.ExemptionsTextBox, this.DeductionsTextBox, this.LegalTextBox, this.TaxableValueTextBox, this.TaxBillableTextBox, this.OwnerTextBox, this.MapTextBox, this.ParcelIDTextBox, this.ParcelNumberTextBox };
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }           
            
            ////if (this.NewButton.Enabled)
            ////{
            ////    this.ActiveControl = this.NewButton;
            ////}
            ////else
            ////{
            ////    RecordNavigatorSmartPart recordNavigatorSmartPart = (RecordNavigatorSmartPart)this.F1100Control.WorkItem.SmartParts["RecordNavigatorSmartPart"];
            ////    recordNavigatorSmartPart.SetFocus = true;
            ////}           
        }

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Real Property Statement", string.Empty }));

            ////Load ReportActionSmartPart SmartPart to ReportActionDeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.ReportActionDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.ReportActionDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            }

            ////Load RecordNavigatorSmartPart to RecordNavigatorSmartPartdeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////Load ToolBoxSmartPart to ToolBoxSmartPartdeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.ToolBoxSmartPart))
            {
                this.ToolBoxDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.ToolBoxSmartPart));
            }
            else
            {
                this.ToolBoxDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<ToolBoxSmartPart>(SmartPartNames.ToolBoxSmartPart));
            }

            ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.StatusBarDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart));
            }
            else
            {
                this.StatusBarDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.Form1020Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.AdditionalOperationDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.AdditionalOperationDeckWorkspace.Show(this.Form1020Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1020Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.Form1020Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            this.toolBoxSmartPart = (ToolBoxSmartPart)this.Form1020Control.WorkItem.SmartParts[SmartPartNames.ToolBoxSmartPart];
            this.toolBoxSmartPart.EnableClearFilterButton = false;

            this.statusBarSmartPart = (StatusBarSmartPart)this.Form1020Control.WorkItem.SmartParts[SmartPartNames.StatusBarSmartPart];
            this.statusBarSmartPart.FilteredButtonFilterStatus = false;            
        }

        #endregion       

        #region ReceiptEngineUserControl EventHandling       

        /// <summary>
        /// Receipts the engine user control_ Delinquency Changed Event. Event triggerd after delinquency change
        /// </summary>
        /// <param name="isdelinquent">the delinquent</param>
        private void ReceiptEngineUserControl_DelinquencyChangedEvent(bool isdelinquent)
        {
            ////sets delinquency of the statement
            this.statusBarSmartPart.DelinquentButtonStatus = isdelinquent;
        }

        /// <summary>
        /// Receipts the engine user control_ saved event. Event triggerd after save
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        private void ReceiptEngineUserControl_SavedEvent(int receiptID)
        {
            // TODO : Genralized 
            try
            {
                this.Cursor = Cursors.WaitCursor;

                /////  this.ShowReport(Report.ReportType.Print, "10202", "ReceiptID", this.ReceiptEngineUserControl.CurrentReceiptId);

                //// Calling the Common Function for Report
                if (receiptID > 0)
                {
                    bool autoPrintStatus = Convert.ToBoolean(this.form1020Control.WorkItem.GetAutoPrintStatus(int.Parse(this.Tag.ToString()), TerraScanCommon.UserId));
                    this.SetAutoPrint(this, new DataEventArgs<bool>(autoPrintStatus));
                    if (autoPrintStatus)
                    {
                        this.reportOptionalParameter.Clear();
                        ////this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                        this.reportOptionalParameter.Add("ReceiptID", receiptID);
                        //changed the parameter type from string to int
                        TerraScanCommon.ShowReport(10202, Report.ReportType.PrintDefault, this.reportOptionalParameter);
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

        #region StatementDetails      

        /// <summary>
        /// Gets the statement details and fill realestatemasterform accordingly.
        /// </summary>
        private void GetStatementDetails()
        {
            if (this.realEstate.GetRealPropertyStatementDetails.Rows.Count > 0)
            {
                this.StatementIDTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.StatementIDColumn].ToString();
                this.StatementNumberTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.StatementNumberColumn].ToString();
                this.RollYearTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.RollYearColumn].ToString();
                this.LevyYearTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.LevyYearColumn].ToString();
                this.DistrictTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.DistrictColumn].ToString();
                this.TotalValueTextBox.Text = String.Format("{0:$#,##0}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.TotalValueColumn]);
                this.OriginalTaxTextBox.Text = String.Format("{0:$#,##0.00}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.OriginalTaxColumn]);
                this.SitusTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.SitusColumn].ToString();
                this.ExemptionsTextBox.Text = String.Format("{0:$#,##0}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.TotalExemptionsColumn]);
                this.DeductionsTextBox.Text = String.Format("{0:$#,##0.00}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.TotalDeductionsColumn]);
                this.LegalTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.LegalColumn].ToString();
                this.TaxableValueTextBox.Text = String.Format("{0:$#,##0}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.TaxableValueColumn]);
                this.TaxBillableTextBox.Text = String.Format("{0:$#,##0.00}", this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.TaxBilledColumn]);
                this.ParcelIDlinkLabel.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.ParcelIDColumn].ToString();
                this.ParcelNumberTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.ParcelNumberColumn].ToString();
                this.MapTextBox.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.MapNumberColumn].ToString();
                this.OwnerlinkLabel.Text = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.OwnerNameColumn].ToString();
                this.StatementAuditlinkLabel.Text = "tTR_Statement [StatementID] " + this.StatementIDTextBox.Text;
                this.StatementAuditlinkLabel.Enabled = true;
                if (bool.Parse(this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.OwnerStatusColumn].ToString()))
                {
                    this.OwnerLabel.Text = "Owner (Primary):";
                }
                else
                {
                    this.OwnerLabel.Text = "Owner:";
                }

                //// LoadReceiptEngine method used to load receipt engine based on current statement ID
                this.LoadReceiptEngine(this.currentStatementId);
                ////passing ownername to receipt engine usercontrol
                this.ReceiptEngineUserControl.OwnerName = this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.OwnerNameColumn].ToString();
                this.statusBarSmartPart.DelinquentButtonStatus = (Convert.ToBoolean(this.realEstate.GetRealPropertyStatementDetails.Rows[0][this.realEstate.GetRealPropertyStatementDetails.DelinquentStatusColumn].ToString()));
                this.ChangeDelinquentParcelStatus(this.realEstate.GetRealPropertyStatementDetails.Rows[0][realEstate.GetRealPropertyStatementDetails.DelinquentParcelStatusColumn].ToString());
                ////Used to set record count of attachment and comments.
                this.additionalOperationSmartPart.Enabled = true;
                this.SetAdditionalOperationCount();
            }
            else
            {
                this.ClearRealEstateDetails();
            }
        }

        /// <summary>
        /// Changes the delinquent parcel status.
        /// </summary>
        /// <param name="tempParcelStatus">The temp parcel status.</param>
        private void ChangeDelinquentParcelStatus(string tempParcelStatus)
        {
            bool delinquentParcelStatus;
            bool.TryParse(tempParcelStatus, out delinquentParcelStatus);

            if (delinquentParcelStatus)
            {
                this.ParcelIDlinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                this.ParcelNumberTextBox.ForeColor = Color.FromArgb(255, 0, 0);
            }
            else
            {
                this.ParcelIDlinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                this.ParcelNumberTextBox.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }

        /// <summary>
        /// Fill Statement Details
        /// </summary>
        /// <param name="tempDataTable">The temp data table.</param>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void FillStatementDetails(DataTable tempDataTable, bool fetchNextRecord)
        {    
            this.Cursor = Cursors.WaitCursor;
            int recordIndex = 0;

            try
            {
                ////refresh form record set
                if (tempDataTable != null)
                {
                    this.realEstate.ListRealPropertyStatementID.Clear();
                    this.realEstate.ListRealPropertyStatementID.Merge(tempDataTable);
                }
                else
                {
                    this.ListRealPropertyStatementId(null, null);
                }

                if (this.realEstate.ListRealPropertyStatementID.Rows.Count > 0)
                {
                    ////get current statement id
                    this.GetCurrentStatementId(fetchNextRecord);

                    this.realEstate.GetRealPropertyStatementDetails.Clear();                    
                    this.realEstate.Merge(this.form1020Control.WorkItem.GetRealEstateStatement(this.currentStatementId.Value), true);

                    this.GetStatementDetails();
                    recordIndex = this.RetrieveRecordIndex(null);
                    this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.realEstate.ListRealPropertyStatementID.Rows.Count));
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { recordIndex, this.realEstate.ListRealPropertyStatementID.Rows.Count }));
                }
                else
                {
                    this.CurrentStatementId = null;
                    this.ClearRealEstateDetails();
                }
            }
            catch (Exception ex)
            {
                // todo:
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                ////this.FocusRequiredInputField(this.MortgageImportPanel, true);
                this.Cursor = Cursors.Default;
            }            
        }

        /// <summary>
        /// retrieves the current import position index
        /// </summary>
        /// <param name="tempRecordId">The temp record id.</param>
        /// <returns>index of the current record</returns>
        private int RetrieveRecordIndex(int? tempRecordId)
        {
            if (tempRecordId == null)
            {
                tempRecordId = this.currentStatementId;
            }

            int tempIndex = 0;
            DataTable tempDataTable = this.realEstate.ListRealPropertyStatementID.Copy();
            tempDataTable.DefaultView.RowFilter = string.Concat(this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName, " = ", tempRecordId);

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
            }

            return tempIndex;
        }

        /// <summary>
        /// Retrieves the statement id.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>tempStatementId</returns>
        private int? RetrieveStatementId(int index)
        {
            int? tempStatementId = null;

            if (this.realEstate.ListRealPropertyStatementID.Rows.Count > 0)
            {
                if (index > 0 && index <= this.realEstate.ListRealPropertyStatementID.Rows.Count)
                {
                    tempStatementId = Convert.ToInt32(this.realEstate.ListRealPropertyStatementID.Rows[index - 1][this.realEstate.ListRealPropertyStatementID.KeyIDColumn]);
                }
                else
                {
                    tempStatementId = Convert.ToInt32(this.realEstate.ListRealPropertyStatementID.Rows[0][this.realEstate.ListRealPropertyStatementID.KeyIDColumn]);
                    this.SetActiveRecord(this, new DataEventArgs<int>(1));
                }
            }

            return tempStatementId;
        }

        /// <summary>
        /// Gets the current statement id.
        /// </summary>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void GetCurrentStatementId(bool fetchNextRecord)
        {
            if (this.CurrentStatementId.HasValue)
            {
                DataTable tempDataTable = this.realEstate.ListRealPropertyStatementID.Copy();
                DataView tempDataView = new DataView(tempDataTable);
                tempDataView.RowFilter = string.Concat(this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName, " = ", this.currentStatementId);
                if (tempDataView.Count > 0)
                {
                    return;
                }

                tempDataView = tempDataTable.DefaultView;
                if (fetchNextRecord)
                {
                    tempDataView.RowFilter = string.Concat(this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName, " > ", this.currentStatementId);
                }
                else
                {
                    tempDataView.RowFilter = string.Concat(this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName, " < ", this.currentStatementId);
                    tempDataView.Sort = string.Concat(this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName, " DESC");
                }

                if (tempDataView.Count > 0)
                {
                    this.CurrentStatementId = (int)tempDataView[0][this.realEstate.ListRealPropertyStatementID.KeyIDColumn.ColumnName];
                    return;
                }
            }

            this.CurrentStatementId = (int)this.realEstate.ListRealPropertyStatementID.Rows[0][this.realEstate.ListRealPropertyStatementID.KeyIDColumn];
        }

        /// <summary>
        /// Loads the receipt engine.
        /// </summary>
        /// <param name="statementID">The statement ID.</param>
        private void LoadReceiptEngine(int? statementID)
        {
            this.ReceiptEngineUserControl.StatementId = statementID;
        }

        #endregion       

        #region Attachments and Comments

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            ////// this.Cursor = Cursors.WaitCursor;
            ////// TerraScanCommon.ShowAttachment(int.Parse(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), this);
            ////// this.Cursor = Cursors.Default;

            ////try
            ////{
            ////    object[] optionalParameter = new object[] { Convert.ToInt32(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), Convert.ToInt32(this.Tag.ToString()) };
            ////    Form attachment = new Form();
            ////    attachment = TerraScanCommon.GetForm(9005, optionalParameter, true);
            ////    if (attachment != null)
            ////    {
            ////        attachment.Owner = this;
            ////        attachment.ShowDialog();
            ////        TerraScanCommon.SetRecordCount(Convert.ToInt32(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), this.CommentButton, this.AttachmentButton, this);
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            ////}
        }    

        /// <summary>
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
            ////// this.Cursor = Cursors.WaitCursor;
            ////// TerraScanCommon.ShowComment(int.Parse(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), this);
            ////// this.Cursor = Cursors.Default;

            ////try
            ////{
            ////    object[] optionalParameter = new object[] { Convert.ToInt32(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), Convert.ToInt32(this.Tag.ToString()) };
            ////    Form comments = new Form();
            ////    comments = TerraScanCommon.GetForm(9075, optionalParameter, true);
            ////    if (comments != null)
            ////    {
            ////        comments.Owner = this;
            ////        comments.ShowDialog();
            ////        TerraScanCommon.SetRecordCount(Convert.ToInt32(this.Tag.ToString()), int.Parse(this.StatementIDTextBox.Text), this.CommentButton, this.AttachmentButton, this);
            ////    }

            ////}
            ////catch (Exception ex)
            ////{
            ////   //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            ////}
        }

        /// <summary>
        /// Sets the attachment and comments count.
        /// </summary>
        private void SetAdditionalOperationCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                if (this.additionalOperationSmartPart != null)
                {
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = new CommentsData.GetCommentsCountDataTable();
                    AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);                   
                    if (this.CurrentStatementId.HasValue)
                    {
                        additionalOperationCountEntity.AttachmentCount = this.form1020Control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), this.currentStatementId.Value, TerraScanCommon.UserId);
                        commentsCountDataTable = this.form1020Control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), this.currentStatementId.Value, TerraScanCommon.UserId);
                        if (commentsCountDataTable.Rows.Count > 0)
                        {
                            additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                            additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                        }                       
                    }

                    this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;                    
                }
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        } 

        #endregion

        #region Sort Methods

        /// <summary>
        /// Handles the Click event of the Sorting control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Sorting_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.ReceiptEngineUserControl.CheckPageStatus(false))
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                PictureBox senderPicturebox = ((PictureBox)sender);

                string[] tempFieldandType = senderPicturebox.Tag.ToString().Split('~');

                if (senderPicturebox.Name.ToUpper().Trim() != "SELECTEDPICTUREBOX")
                {
                    this.SelectedPictureBox.Parent = senderPicturebox.Parent;
                    this.SelectedPictureBox.Left = senderPicturebox.Left + senderPicturebox.Width - this.SelectedPictureBox.Width;
                    this.SelectedPictureBox.Top = 0;
                    this.SelectedPictureBox.BringToFront();
                }

                if (tempFieldandType[1] == "Desc")
                {
                    this.SelectedPictureBox.Image = this.SortingImageList.Images[1];
                    this.SelectedPictureBox.Tag = senderPicturebox.Tag.ToString().Replace("Desc", "Asc");
                }
                else
                {
                    this.SelectedPictureBox.Image = this.SortingImageList.Images[0];
                    this.SelectedPictureBox.Tag = senderPicturebox.Tag.ToString().Replace("Asc", "Desc");
                }

                this.ListRealPropertyStatementId(tempFieldandType[1], tempFieldandType[0]);

                ////setrecord Count
                int recordIndex = this.RetrieveRecordIndex(null);
                this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                this.SetRecordCount(this, new DataEventArgs<int>(this.realEstate.ListRealPropertyStatementID.Rows.Count));
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { recordIndex, this.realEstate.ListRealPropertyStatementID.Rows.Count }));
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
        /// Lists the real property statement id.
        /// </summary>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="fieldName">Name of the field.</param>
        private void ListRealPropertyStatementId(string orderByCondition, string fieldName)
        {
            ////contains order by value
            string orderbyField = null;
            ////if null - apply sorting from SelectedPictureBox else apply sorting from parameter
            if (string.IsNullOrEmpty(orderByCondition) && string.IsNullOrEmpty(fieldName))
            {
                if (string.Compare(this.StatementIDDownPictureBox.Tag.ToString().Trim(), this.SelectedPictureBox.Tag.ToString().Trim(), true) != 0)
                {
                    string[] tempFieldandType = this.SelectedPictureBox.Tag.ToString().Split('~');
                    fieldName = tempFieldandType[0];
                    if (tempFieldandType[1] == "Desc")
                    {
                        orderByCondition = "Asc";
                    }
                    else
                    {
                        orderByCondition = "Desc";
                    }

                    orderbyField = string.Concat("ORDER BY ", fieldName, " ", orderByCondition);
                }
            }
            else
            {
                orderbyField = string.Concat("ORDER BY ", fieldName, " ", orderByCondition);
            }

            ////clears form record set
            this.realEstate.ListRealPropertyStatementID.Clear();
            if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
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
                    this.realEstate.ListRealPropertyStatementID.Merge(this.form1020Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, tempWhereString, orderbyField, this.ParentFormId).ListKeyId);
                }
                else
                {
                    this.realEstate.ListRealPropertyStatementID.Merge(this.form1020Control.WorkItem.ExecuteQuery(tempWhereString, orderbyField, this.ParentFormId).ListKeyId);
                }

                if (this.realEstate.ListRealPropertyStatementID.Rows.Count > 0)
                {
                    return;
                }

                ////records not found - clear filter mode
                this.ClearQueryByFormFields();
            }

            ////default load record set
            this.realEstate.ListRealPropertyStatementID.Merge(this.form1020Control.WorkItem.GetRealEstateStatementIds(fieldName, orderByCondition).ListRealPropertyStatementID);              
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
            try
            {
                this.CurrentStatementId = null;
                if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    this.FilterRecordSet();
                }
                else
                {
                    ////clear the values to display
                    this.Cursor = Cursors.WaitCursor;
                    this.LoadReceiptEngine(null);
                    this.toolBoxSmartPart.EnableClearFilterButton = true;

                    if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                    {
                        this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredQueryByForm;
                    }
                    else
                    {                        
                        ////reset FilterTypeId
                        this.FilterTypeId = 0;
                        this.CurrentPageStatus = TerraScanCommon.PageStatus.QueryByForm;
                    }

                    this.ClearRealEstateDetails();
                    this.SetFilteredControlsProperty(false);
                    this.SetControlsProperty();
                    this.StatementIDTextBox.Focus();
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

        /// <summary>
        /// Handles the Click event of the ClearFilterButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearFilterFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////set necessary controls property
                if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode) || this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm))
                {
                    ////clear query related fields
                    this.ClearQueryByFormFields();
                }
                else if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {                   
                    this.SetFilteredControlsProperty(true);
                    this.toolBoxSmartPart.EnableClearFilterButton = true;
                    ////Set page status
                    this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.SetControlsProperty();
                }
                
                //// FillStatementDetails function is used to fill the Statement details in RealEstateform                         
                this.FillStatementDetails(null, false);
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
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
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArray, queryingFields.RealEstateQueryingFields, whereClause, userFriendlyWhereCondition, ref invalidQuery);

            if (!invalidQuery && whereClause.Length == 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryEntryMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.StatementIDTextBox.Focus();
                this.Cursor = Cursors.Default;
                return;
            }

            QueryData queryData = new QueryData();

            try
            {
                ////checks for any criteria
                if (whereClause.Length > 0 && !invalidQuery)
                {
                    if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm) && this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                    {
                        queryData = this.form1020Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
                    }
                    else
                    {
                        queryData = this.form1020Control.WorkItem.ExecuteQuery(whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
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
                    this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.SetFilteredControlsProperty(true);

                    this.SetControlsProperty();
                    //// FillStatementDetails function is used to fill the Statement details in RealEstateform                     
                    this.FillStatementDetails(queryData.ListKeyId, false);            
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

                    this.StatementIDTextBox.Focus();
                }

                ////sets default cursor
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Set property of Filtered Status Related Control
        /// </summary>
        /// <param name="filteredStatus">true - if filtered.</param>        
        private void SetFilteredControlsProperty(bool filteredStatus)
        {
            this.statusBarSmartPart.FilteredButtonFilterStatus = filteredStatus;

            this.SelectedPictureBox.Parent = this.StatementIDUpPictureBox.Parent;
            this.SelectedPictureBox.Left = this.StatementIDUpPictureBox.Left + this.StatementIDUpPictureBox.Width - this.SelectedPictureBox.Width;
            this.SelectedPictureBox.Top = 0;
            this.SelectedPictureBox.Tag = this.StatementIDDownPictureBox.Tag;
            this.SelectedPictureBox.Image = this.SortingImageList.Images[0];
            this.SelectedPictureBox.BringToFront();
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
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.form1020Control.WorkItem;
                if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
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
                        //// Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                        queryData = this.form1020Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.toolBoxSmartPart.ToolBoxEntity.WhereCondition, null, this.ParentFormId);
                    }
                    else
                    {
                        ////specific for filtered records
                        this.FilterTypeId = this.toolBoxSmartPart.ToolBoxEntity.KeyId;

                        // Load queryData will contain query and where condition and all filtered statementIds in ListKeyid
                        queryData = this.form1020Control.WorkItem.GetQueryResult(this.FilterTypeId, null);
                    }

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentStatementId = null;
                        ////specific to snapshot filter
                        if (this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                        {
                            if (queryData.SearchedCountResult.Rows.Count > 0)
                            {
                                MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            }
                        }
                        else
                        {
                            ////specific for query filtered records
                            this.FormFilterType = TerraScanCommon.FilterType.Query;                          
                        }

                        this.UserDefinedWhereCondition = this.toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition;
                        this.WhereCondition = this.toolBoxSmartPart.ToolBoxEntity.WhereCondition;
                        this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredMode;
                        ////FillStatementDetails function is used to fill the Statement details in RealEstateform 
                        this.FillStatementDetails(queryData.ListKeyId, false);
                        this.SetFilteredControlsProperty(true);
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

                if (this.CurrentPageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    snapshotIdsXml = Utility.GetXmlString(this.realEstate.ListRealPropertyStatementID.Copy());
                    snapshotIdsCount = this.realEstate.ListRealPropertyStatementID.Rows.Count;
                }

                ////required parameter for calling form
                this.toolBoxSmartPart.ToolBoxEntity.CurrentFormId = this.ParentFormId;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotIdXmlString = snapshotIdsXml;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotCount = snapshotIdsCount;
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.form1020Control.WorkItem;
                ////which shows SnapshotUtility form
                this.toolBoxSmartPart.ShowSnapshotUtilityForm();         
                this.Cursor = Cursors.Default;
                if (this.toolBoxSmartPart.ToolBoxEntity.CalledFormStatus)
                {
                    this.Cursor = Cursors.WaitCursor;                   

                    // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                    QueryData queryData = this.form1020Control.WorkItem.GetSnapShotResult(this.toolBoxSmartPart.ToolBoxEntity.KeyId, null);

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentStatementId = null;
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
                        this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredMode;

                        // FillStatementDetails function is used to fill the Statement details in RealEstateform 
                        this.FillStatementDetails(queryData.ListKeyId, false);
                        this.SetFilteredControlsProperty(true);
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
                this.statusBarSmartPart.StatusBarEntity.OptionalInputParameter = new object[] { this.WhereCondition, this.CurrentSnapshotName, this.CurrentSnapshotDescription, queryingFields.RealEstateQueryingFields };
                this.statusBarSmartPart.StatusBarEntity.ParentWorkItem = this.Form1020Control.WorkItem;   
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
                                queryData = this.form1020Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }
                            else
                            {
                                queryData = this.form1020Control.WorkItem.ExecuteQuery(this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }

                            if (queryData.ListKeyId.Rows.Count > 0)
                            {
                                this.CurrentStatementId = null;
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);                                    
                                }

                                this.WhereCondition = this.statusBarSmartPart.StatusBarEntity.WhereCondition;
                                this.UserDefinedWhereCondition = this.statusBarSmartPart.StatusBarEntity.UserDefinedWhereCondition;
                                this.toolBoxSmartPart.EnableClearFilterButton = true;    
                                ////Set page status
                                this.CurrentPageStatus = TerraScanCommon.PageStatus.FilteredMode;
                                //// FillStatementDetails function is used to fill the Statement details in RealEstateform 
                                this.FillStatementDetails(queryData.ListKeyId, false);
                                this.SetFilteredControlsProperty(true);                                                            
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
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Audit Link

        /// <summary>
        /// Handles the LinkClicked event of the OwnerlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized             
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.reportOptionalParameter.Clear();
                this.reportOptionalParameter.Add("StatementID", this.StatementIDTextBox.Text);
                //changed the parameter type from string to int
                TerraScanCommon.ShowReport(91001, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportOptionalParameter);
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
        /// Handles the LinkClicked event of the StatementAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void StatementAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            TerraScanCommon.ShowAuditReport("StatementID", this.StatementIDTextBox.Text);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Handles the LinkClicked event of the ParcelIDlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ParcelIDlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized             
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.reportOptionalParameter.Clear();
                this.reportOptionalParameter.Add("ParcelID", this.ParcelIDlinkLabel.Text);
                //changed the parameter type from string to int
                TerraScanCommon.ShowReport(1006, TerraScan.Common.Reports.Report.ReportType.Preview, this.reportOptionalParameter);
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
    }
}