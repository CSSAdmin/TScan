//--------------------------------------------------------------------------------------------
// <copyright file="F1100.cs" company="Congruent">
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
//                              	    Created// 
//*********************************************************************************/
namespace D1100
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
    /// Form F1100
    /// </summary>
    [SmartPart]
    public partial class F1100 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// f1100Control Variable
        /// </summary>
        private F1100Controller form1100Control;             

        /// <summary>
        /// DataSet Contains Tax Statement Details - statement, IDs and receipt details
        /// </summary>
        private ExciseTaxStatementData exciseTaxStatement = new ExciseTaxStatementData();

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;   

        /// <summary>
        /// formFilterType variable is used to find the type of filter in the form. 
        /// </summary>   
        private TerraScanCommon.FilterType formFilterType;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// currentExciseTaxStatementId variable is used to store ExciseTaxStatement id. 
        /// </summary>       
        private int? currentExciseTaxStatementId = null;       

        /// <summary>
        /// currentExciseTaxReceiptId variable is used to store ExciseTaxReceipt id. 
        /// </summary>       
        private int? currentExciseTaxReceiptId = null;        

        /// <summary>
        /// ppaymentid for the current statement
        /// </summary>       
        private int ppaymentId;
        
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
        /// Created control for clear Filter
        /// </summary>
        private Control clearFilterControl = new Control();

        /// <summary>
        /// Created control for Filtered Status
        /// </summary>
        private TerraScanButton filteredStatusControl = new TerraScanButton();

        /// <summary>
        /// used to pass Report Parameter
        /// </summary>
        private Hashtable reportOptionalParameter = new Hashtable();     

        /// <summary>
        /// Query Control Array
        /// </summary>
        private TerraScanTextBox[] queryControlArray;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// receipt AdditionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart receiptAdditionalOperationSmartPart;

        /// <summary>
        /// toolBoxSmartPart
        /// </summary>
        private ToolBoxSmartPart toolBoxSmartPart = new ToolBoxSmartPart();       

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart = new StatusBarSmartPart();
        
        #endregion   

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1100"/> class.
        /// </summary>
        public F1100()
        {
            this.InitializeComponent();     
            
            ////Customize ExciseTaxReceiptGridView
            this.CustomizeExciseTaxReceiptGridView();
            this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Statement", 174, 150, 94);
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, "Receipt", 28, 81, 128);
            this.SetQueryingFieldName();
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

        /// <summary>
        /// Declare the event ShowForm        
        /// </summary> 
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;   

        #endregion Event Publication

        #region Properties

        /// <summary>
        /// Gets or sets the F1100 control.
        /// </summary>
        /// <value>The F1100 control.</value>
        [CreateNew]
        public F1100Controller F1100Control
        {
            get { return this.form1100Control as F1100Controller; }
            set { this.form1100Control = value; }
        }

        /// <summary>
        /// Gets or sets the current excise tax receipt id.
        /// </summary>
        /// <value>The current excise tax receipt id.</value>
        public int? CurrentExciseTaxReceiptId
        {
            get
            {
                return this.currentExciseTaxReceiptId;
            }

            set
            {
                this.currentExciseTaxReceiptId = value;
                if (this.receiptAdditionalOperationSmartPart != null)
                {
                    this.receiptAdditionalOperationSmartPart.KeyId = this.currentExciseTaxReceiptId ?? -1;
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
        /// Gets or sets the current excise tax statement id.
        /// </summary>
        /// <value>The current excise tax statement id.</value>
        private int? CurrentExciseTaxStatementId
        {
            get
            {
                return this.currentExciseTaxStatementId;
            } 

            set
            {
                this.currentExciseTaxStatementId = value;
                this.CurrentExciseTaxReceiptId = null;
                if (this.additionalOperationSmartPart != null)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentExciseTaxStatementId ?? -1;
                }
            }
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
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get
            {
                return this.pageMode;
            }

            set
            {
                this.pageMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the page status.
        /// </summary>
        /// <value>The page status.</value>
        private TerraScanCommon.PageStatus PageStatus
        {
            get { return this.pageStatus; }
            set { this.pageStatus = value; }
        }

        /// <summary>
        /// Gets or sets the clear filter control.
        /// </summary>
        /// <value>The clear filter control.</value>
        private Control ClearFilterControl
        {
            get { return this.clearFilterControl; }
            set { this.clearFilterControl = value; }
        }

        /// <summary>
        /// Gets or sets the clear filter control.
        /// </summary>
        /// <value>The clear filter control.</value>
        private TerraScanButton FilteredStatusControl
        {
            get { return this.filteredStatusControl; }
            set { this.filteredStatusControl = value; }
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
            if (this.CheckPageStatus(true))
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
            this.CurrentExciseTaxStatementId = this.RetrieveRecordId(recordNavigationEntity.RecordIndex);
            this.FillExciseTaxStatementFormDetails(null, recordNavigationEntity.RecordNavigationFlag);
        }

        /// <summary>
        /// Queries the by from button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_ToolBoxSmartPart_QueryByFromButtonClick, Thread = ThreadOption.UserInterface)]
        public void QueryByFromButtonClick(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus(true))
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
            if (this.CheckPageStatus(true))
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
            if (this.CheckPageStatus(false))
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
            if (this.CheckPageStatus(false))
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
            if (this.CheckPageStatus(false))
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
            this.F1100Control.WorkItem.SaveAutoPrint(int.Parse(this.Tag.ToString()), TerraScanCommon.UserId, e.Data);
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
                this.F1100Control.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
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
                if (this.CheckPageStatus(false))
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

                    this.CurrentExciseTaxStatementId = Convert.ToInt32(optionalParams[0]);

                    this.FillExciseTaxStatementFormDetails(null, false);
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

                /////  this.ShowReport(Report.ReportType.Print, "11001", "ReceiptID", this.ReceiptEngineUserControl.PreviousReceiptId);

                //// Calling the Common Function for Report
                if (this.currentExciseTaxStatementId.HasValue && this.currentExciseTaxReceiptId.HasValue)
                {
                    this.reportOptionalParameter.Clear();
                    this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("KeyValue", this.currentExciseTaxReceiptId.Value);
                    //// changed the parameter type from string to int
                    TerraScanCommon.ShowReport(11001, Report.ReportType.Print, this.reportOptionalParameter);
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
                if (this.currentExciseTaxStatementId.HasValue && this.currentExciseTaxReceiptId.HasValue)
                {
                    ////// calling  Common Function For Report
                    this.reportOptionalParameter.Clear();
                    this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("KeyValue", this.currentExciseTaxReceiptId.Value);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(11001, Report.ReportType.Preview, this.reportOptionalParameter);
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
                if (this.currentExciseTaxStatementId.HasValue && this.currentExciseTaxReceiptId.HasValue)
                {
                    ////this.ShowReport(Report.ReportType.Email, "11001", "ReceiptID", this.ReceiptEngineUserControl.PreviousReceiptId);
                    ////// calling  Common Function For Report
                    this.reportOptionalParameter.Clear();
                    this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                    this.reportOptionalParameter.Add("KeyValue", this.currentExciseTaxReceiptId.Value);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(11001, Report.ReportType.Email, this.reportOptionalParameter);
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
                if (this.currentExciseTaxStatementId.HasValue)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.reportOptionalParameter.Clear();
                    //// Calling the Common Function for Report
                    this.reportOptionalParameter.Add("KeyName", "StatementID");
                    this.reportOptionalParameter.Add("KeyValue", this.currentExciseTaxStatementId.Value);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(11002, Report.ReportType.Preview, this.reportOptionalParameter);
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

        #region Form Load

        /// <summary>
        /// Handles the Load event of the f1100 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1100_Load(object sender, EventArgs e)
        {
            this.LoadWorkSpaces();
            this.SetAutoPrint(this, new DataEventArgs<bool>(Convert.ToBoolean(this.F1100Control.WorkItem.GetAutoPrintStatus(Convert.ToInt32(this.Tag), TerraScanCommon.UserId))));
            this.CurrentExciseTaxStatementId = null;
            this.FillExciseTaxStatementFormDetails(null, false);            
            ////sets QueryRelated Field
            this.queryControlArray = new TerraScanTextBox[] { this.StatementIDTextBox, this.ParcelNumberTextBox, this.SaleDateTextBox, this.PaymentDateTextBox, this.FormDateTextBox, this.MobileHomeTextBox, this.ReceiptNumberTextBox, this.DistrictTextBox, this.SaleAmountTextBox, this.TaxCodeTextBox, this.GrantorTextBox, this.GranteeTextBox };            

            ////AssignShortcut Keys
            this.NewMenu.Click += new EventHandler(this.NewButton_Click);
            this.SaveMenu.Click += new EventHandler(this.SaveButton_Click);
            this.ParentForm.CancelButton = this.CancelReceiptButton;
        }        

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {  
            ////Load FormHeaderSmartPart to FormHeaderDeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            ////sets form header
            this.SetFormHeader(this, new DataEventArgs<string[]>(new string[] { "Excise Tax Statement", string.Empty }));
           
            ////Load ReportActionSmartPart SmartPart to ReportActionDeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.ReportActionDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.ReportActionDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            }

            ////Load RecordNavigatorSmartPart to RecordNavigatorSmartPartdeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            ////Load ToolBoxSmartPart to ToolBoxSmartPartdeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.ToolBoxSmartPart))
            {
                this.ToolBoxDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.ToolBoxSmartPart));
            }
            else
            {
                this.ToolBoxDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<ToolBoxSmartPart>(SmartPartNames.ToolBoxSmartPart));
            }

            ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.StatusBarDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart));
            }
            else
            {
                this.StatusBarDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.F1100Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.ReceiptCommentsDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.ReceiptCommentsDeckWorkspace.Show(this.F1100Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.F1100Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            this.receiptAdditionalOperationSmartPart = (AdditionalOperationSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.receiptAdditionalOperationSmartPart.ParentWorkItem = this.F1100Control.WorkItem;
            this.receiptAdditionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.receiptAdditionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);            
            this.receiptAdditionalOperationSmartPart.VerticalButtons = true;
            this.receiptAdditionalOperationSmartPart.BackColor = Color.Transparent;
            this.receiptAdditionalOperationSmartPart.AttachmentButtonType = TerraScanButton.ButtonType.CommandButton;
            this.receiptAdditionalOperationSmartPart.CommentButtonType = TerraScanButton.ButtonType.CommandButton;         
           
            this.toolBoxSmartPart = (ToolBoxSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.ToolBoxSmartPart];
            this.ClearFilterControl = this.GetSmartPartControl(this.toolBoxSmartPart, "ClearFilterButton");
            this.ClearFilterControl.Enabled = false;

            this.statusBarSmartPart = (StatusBarSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.StatusBarSmartPart];
            this.FilteredStatusControl = (TerraScanButton)this.GetSmartPartControl(this.statusBarSmartPart, "FilteredButton");
            this.GetSmartPartControl(this.statusBarSmartPart, "DelinquentButton").Visible = false;          
        }

        #endregion

        #region Excise Tax Form - Get And Retrieve Method

        /// <summary>
        /// retrieves the current importId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>import id of the current record</returns>
        private int RetrieveRecordId(int index)
        {
            int tempStatementID = 0;

            if (this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count > 0)
            {
                if (index > 0 && index <= this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(this.exciseTaxStatement.ListExciseTaxStatementID.Rows[index - 1][0])))
                    {
                        tempStatementID = Convert.ToInt32(this.exciseTaxStatement.ListExciseTaxStatementID.Rows[index - 1][0]);
                    }
                }
                else
                {
                    tempStatementID = Convert.ToInt32(this.exciseTaxStatement.ListExciseTaxStatementID.Rows[this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count - 1][0]);
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count));
                }
            }

            return tempStatementID;
        }

        /// <summary>
        /// retrieves the current import position index
        /// </summary>
        /// <param name="tempRecordId">tempRecordId</param>
        /// <returns>index of the current record</returns>
        private int RetrieveRecordIndex(int? tempRecordId)
        {
            if (tempRecordId == null)
            {
                tempRecordId = this.currentExciseTaxStatementId;
            }

            int tempIndex = 0;
            DataTable tempDataTable = this.exciseTaxStatement.ListExciseTaxStatementID.Copy();
            tempDataTable.DefaultView.RowFilter = string.Concat(this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName, " = ", tempRecordId);

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
            }

            return tempIndex;
        }

        /// <summary>
        /// Fills the excise tax statement form details.
        /// </summary>
        /// <param name="tempDataTable">The temp data table contains statementIDs.</param>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void FillExciseTaxStatementFormDetails(DataTable tempDataTable, bool fetchNextRecord)
        {
            this.PageMode = TerraScanCommon.PageModeTypes.View;

            ////this.SetButtons(this, new DataEventArgs<Enum>(TerraScanCommon.ButtonActionMode.CancelMode));
            this.Cursor = Cursors.WaitCursor;
            int recordIndex = 0;

            try
            {
                this.LoadExciseTaxStatementId(tempDataTable);

                if (this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count > 0)
                {                   
                    ////get current statement id
                    this.GetCurrentStatementId(fetchNextRecord);

                    this.exciseTaxStatement.GetExciseTaxStatement.Clear();
                    this.exciseTaxStatement.GetExciseTaxReceipt.Clear();
                    this.exciseTaxStatement.Merge(this.form1100Control.WorkItem.GetExciseTaxStatement(this.currentExciseTaxStatementId.Value), true);
                    
                    this.GetExciseTaxStatementDetails();
                    recordIndex = this.RetrieveRecordIndex(null);
                    this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count));
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { recordIndex, this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count }));
                }
                else
                {
                    this.CurrentExciseTaxStatementId = null;
                    this.CurrentExciseTaxReceiptId = null;
                    this.ClearExciseTaxStatement();                    
                }
            }
            catch (Exception ex)
            {
                // todo:// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                if (this.NewButton.Enabled)
                {
                    this.ActiveControl = this.NewButton;
                }
                else
                {
                    RecordNavigatorSmartPart recordNavigatorSmartPart = (RecordNavigatorSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.RecordNavigatorSmartPart];
                    recordNavigatorSmartPart.SetFocus = true;
                }

                if (this.pageStatus.Equals(TerraScanCommon.PageStatus.NormalMode) || this.pageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    this.StatementIDTextBox.BackColor = Color.White;
                }

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Loads the Excise Tax Statement id.
        /// </summary>
        /// <param name="tempDataTable">The temp data table.</param>
        private void LoadExciseTaxStatementId(DataTable tempDataTable)
        {
            ////clears record set
            this.exciseTaxStatement.Clear();
            if (tempDataTable != null)
            {
                this.exciseTaxStatement.ListExciseTaxStatementID.Merge(tempDataTable);
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
                        this.exciseTaxStatement.ListExciseTaxStatementID.Merge(this.form1100Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, tempWhereString, null, this.ParentFormId).ListKeyId);
                    }
                    else
                    {
                        this.exciseTaxStatement.ListExciseTaxStatementID.Merge(this.form1100Control.WorkItem.ExecuteQuery(tempWhereString, null, this.ParentFormId).ListKeyId);
                    }

                    if (this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count > 0)
                    {
                        return;
                    }

                    ////records not found - clear filter mode
                    this.ClearQueryByFormFields();
                }

                this.exciseTaxStatement.ListExciseTaxStatementID.Merge(this.form1100Control.WorkItem.ListExciseTaxStatement());
            }
        }

        /// <summary>
        /// Gets the current statement id.
        /// </summary>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void GetCurrentStatementId(bool fetchNextRecord)
        {
            if (this.currentExciseTaxStatementId.HasValue)
            {
                DataTable tempDataTable = this.exciseTaxStatement.ListExciseTaxStatementID.Copy();
                DataView tempDataView = new DataView(tempDataTable);
                tempDataView.RowFilter = string.Concat(this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName, " = ", this.currentExciseTaxStatementId);
                if (tempDataView.Count > 0)
                {
                    return;
                }

                tempDataView = tempDataTable.DefaultView;
                if (fetchNextRecord)
                {
                    tempDataView.RowFilter = string.Concat(this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName, " > ", this.currentExciseTaxStatementId);
                }
                else
                {
                    tempDataView.RowFilter = string.Concat(this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName, " < ", this.currentExciseTaxStatementId);
                    tempDataView.Sort = string.Concat(this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName, " DESC");
                }

                if (tempDataView.Count > 0)
                {
                    this.CurrentExciseTaxStatementId = (int)tempDataView[0][this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn.ColumnName];
                    return;
                }
            }

            this.CurrentExciseTaxStatementId = (int)this.exciseTaxStatement.ListExciseTaxStatementID.Rows[0][this.exciseTaxStatement.ListExciseTaxStatementID.KeyIDColumn];                       
        }

        /// <summary>
        /// Gets the Import details and fill MortgageImportForm Header accordingly.
        /// </summary>        
        private void GetExciseTaxStatementDetails()
        {
            if (this.exciseTaxStatement.GetExciseTaxStatement.Rows.Count > 0)
            {                
                this.StatementIDTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.StatementIDColumn].ToString();
                this.ParcelNumberTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.ParcelNumberColumn].ToString();
                this.SaleDateTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.SaleDateColumn].ToString();
                this.PaymentDateTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.PaymentDateColumn].ToString();
                this.FormDateTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.FormDateColumn].ToString();
                this.MobileHomeTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.MobileHomeColumn].ToString();
                this.ReceiptNumberLinkLabel.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.ReceiptNumberColumn].ToString();
                this.DistrictLinkLabel.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.DistrictColumn].ToString();
                this.SaleAmountTextBox.Text = Convert.ToDecimal(this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.TaxableSalePriceColumn]).ToString("$ #,##0.00");
                this.TaxCodeTextBox.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.TaxCodeColumn].ToString();
                this.GranteeLinkLabel.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.GrantorColumn].ToString();
                this.GrantorLinkLabel.Text = this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.GranteeColumn].ToString();
                this.ppaymentId = Convert.ToInt32(this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.PPaymentIDColumn]);
                if (this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.ReceiptIDColumn].Equals(System.DBNull.Value))
                {
                    this.CurrentExciseTaxReceiptId = null;                    
                }
                else
                {
                    this.CurrentExciseTaxReceiptId = Convert.ToInt32(this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.ReceiptIDColumn]);                    
                }

                this.ExciseTaxStatementAuditlinkLabel.Text = string.Concat("tTR_Statement [StatementID]", this.StatementIDTextBox.Text);
                this.ExciseTaxStatementAuditlinkLabel.Enabled = true;

                ////refresh tax receipt
                if (this.exciseTaxStatement.GetExciseTaxReceipt.Rows.Count > 0)
                {
                    this.ExciseTaxReceiptGridView.Refresh();
                }
                else
                {
                    this.exciseTaxStatement.InitializeExciseTaxReceipt();
                    this.ExciseTaxReceiptGridView.Refresh();
                }

                ////calculate total due
                this.CalculateTotalDue();

                this.PaymentEngineUserControl.LoadPayment(this.ppaymentId);

                ////sets property with pagemode
                this.SetPageModeProperty();
            }
            else
            {
               this.ClearExciseTaxStatement();
            }
        }

        #endregion

        #region Clear Excise Tax

        /// <summary>
        /// Method will Clear the PaymentEngine DataGrid
        /// </summary>
        private void ClearExciseTaxStatement()
        {
            this.ClearOtherFields();
            this.ClearExciseTaxStatementHeader();
            this.ClearExciseTaxReceipt();
            this.ppaymentId = 0;
            this.PaymentEngineUserControl.LoadPayment();
            this.PaymentEngineUserControl.Locked = true;            
        }

        /// <summary>
        /// Method will Clear the ExciseTaxStatement Header
        /// </summary>       
        private void ClearExciseTaxStatementHeader()
        {
            ////ExciseTaxStatement related fields
            if (!this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) && !this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                this.StatementIDTextBox.Text = string.Empty;
                this.ParcelNumberTextBox.Text = string.Empty;
                this.SaleDateTextBox.Text = string.Empty;
                this.PaymentDateTextBox.Text = string.Empty;
                this.FormDateTextBox.Text = string.Empty;
                this.MobileHomeTextBox.Text = string.Empty;
                this.ReceiptNumberTextBox.Text = string.Empty;
                this.DistrictTextBox.Text = string.Empty;
                this.SaleAmountTextBox.Text = string.Empty;
                this.TaxCodeTextBox.Text = string.Empty;
                this.GranteeTextBox.Text = string.Empty;
                this.GrantorTextBox.Text = string.Empty;
            }

            ////linklabel
            this.ReceiptNumberLinkLabel.Text = string.Empty;
            this.DistrictLinkLabel.Text = string.Empty;
            this.GranteeLinkLabel.Text = string.Empty;
            this.GrantorLinkLabel.Text = string.Empty;            
        }

        /// <summary>
        /// Method will Clear the Excise Tax Receipt Fields
        /// </summary>
        private void ClearExciseTaxReceipt()
        {
            this.exciseTaxStatement.InitializeExciseTaxReceipt();
            this.ExciseTaxReceiptGridView.Refresh();

            this.StatusTotalTextBox.Text = string.Empty;
            this.LocalTotalTextBox.Text = string.Empty;
            this.FeeTatalTextBox.Text = string.Empty;
            this.TotalDueTextBox.Text = string.Empty;
            this.BalanceAmountTextBox.Text = string.Empty;
            this.PaymentsTotalTextBox.Text = string.Empty;
        }        

        /// <summary>
        /// Method will Reset the values in the MortgageImport Form 
        /// </summary>
        private void ClearOtherFields()
        {
            this.CommentsdeckWorkspace.Enabled = false;
            this.ReceiptCommentsDeckWorkspace.Enabled = false;
            this.SetAdditionalOperationCount(false);

            this.ExciseTaxReceiptAuditLinkLabel.Text = "tTR_Rcpt [ReceiptID]";
            this.ExciseTaxReceiptAuditLinkLabel.Enabled = false;
            
            this.ExciseTaxStatementAuditlinkLabel.Text = "tTR_Statement [StatementID]";
            this.ExciseTaxStatementAuditlinkLabel.Enabled = false; 
           
            ////reset navigation buttons
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            if (this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count == 0)
            {
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(new int[] { 0, 0 }));

                this.ReportActionDeckWorkspace.Enabled = false;
                this.ToolBoxDeckWorkspace.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                this.CancelReceiptButton.Enabled = false;
                this.SaveButton.Enabled = false;

                this.TopPanel.Enabled = false;
                this.ReceiptPanel.Enabled = false;
            }
       
            if (!this.currentExciseTaxStatementId.HasValue)
            {
                this.NewButton.Enabled = false;                
            }
        }

        /// <summary>
        /// Clears the query by form fields.
        /// </summary>
        private void ClearQueryByFormFields()
        {
            ////clears stored ids
            this.CurrentExciseTaxStatementId = null;
            this.CurrentExciseTaxReceiptId = null;
            ////Set page status
            this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
            this.FormFilterType = TerraScanCommon.FilterType.None;
            this.FilterTypeId = 0;
            ////set necessary controls property
            this.ClearFilterControl.Enabled = false;
            this.FilteredStatusControl.FilterStatus = false;
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
        private void QueryByFormFunction(object sender, DataEventArgs<Button>e)
        {
            try
            {               
                e.Data.Focus();
                this.CurrentExciseTaxStatementId = null;
                this.CurrentExciseTaxReceiptId = null;                             

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    this.FilterRecordSet();
                }
                else
                {
                    ////clear the values to display
                    this.Cursor = Cursors.WaitCursor;                    
                    this.ClearFilterControl.Enabled = true;                    

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
                    this.ClearExciseTaxStatement();
                    this.FilteredStatusControl.FilterStatus = false;
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
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm))
                {
                    ////clear query related fields
                    this.ClearQueryByFormFields();
                }
                else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
                {
                    ////Set page status
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;                   
                    this.FilteredStatusControl.FilterStatus = true;
                    this.ClearFilterControl.Enabled = true;
                    this.SetControlsProperty();
                }
                
                //// FillStatementDetails function is used to fill the Statement details in excise tax statement                         
                this.FillExciseTaxStatementFormDetails(null, false);     
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
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArray, queryingFields.ExciseTaxStatementQueryingFields, whereClause, userFriendlyWhereCondition, ref invalidQuery);

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
                    if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm) && this.FormFilterType.Equals(TerraScanCommon.FilterType.SnapShot))
                    {
                        queryData = this.F1100Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
                    }
                    else
                    {
                        queryData = this.F1100Control.WorkItem.ExecuteQuery(whereClause.ToString(), null, int.Parse(this.Tag.ToString()));
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
                    this.ClearFilterControl.Enabled = true;

                    ////Set page status
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.FilteredStatusControl.FilterStatus = true;

                    //// FillStatementDetails function is used to fill the Statement details in ExciseTaxStatement                                         
                    this.FillExciseTaxStatementFormDetails(queryData.ListKeyId, false);
                    
                    this.SetControlsProperty();
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
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.F1100Control.WorkItem;
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
                        queryData = this.F1100Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.toolBoxSmartPart.ToolBoxEntity.WhereCondition, null, this.ParentFormId);
                    }
                    else
                    {
                        ////specific for filtered records
                        this.FilterTypeId = this.toolBoxSmartPart.ToolBoxEntity.KeyId;

                        // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                        queryData = this.F1100Control.WorkItem.GetQueryResult(this.FilterTypeId, null);
                    }

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentExciseTaxStatementId = null;
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
                        //// FillStatementDetails function is used to fill the Statement details in Excise Tax Statement 
                        this.FillExciseTaxStatementFormDetails(queryData.ListKeyId, false);
                        this.FilteredStatusControl.FilterStatus = true;
                        this.ClearFilterControl.Enabled = true;
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

                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {                    
                    snapshotIdsXml = Utility.GetXmlString(this.exciseTaxStatement.ListExciseTaxStatementID.Copy());
                    snapshotIdsCount = this.exciseTaxStatement.ListExciseTaxStatementID.Rows.Count;
                }                

                this.Cursor = Cursors.Default;
                ////required parameter for calling form
                this.toolBoxSmartPart.ToolBoxEntity.CurrentFormId = this.ParentFormId;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotIdXmlString = snapshotIdsXml;
                this.toolBoxSmartPart.ToolBoxEntity.SnapshotCount = snapshotIdsCount;
                this.toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.F1100Control.WorkItem;
                ////which shows SnapshotUtility form
                this.toolBoxSmartPart.ShowSnapshotUtilityForm();
                
                this.Cursor = Cursors.Default;

                if (this.toolBoxSmartPart.ToolBoxEntity.CalledFormStatus)
                {
                    this.Cursor = Cursors.WaitCursor;                   

                    // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                    QueryData queryData = this.F1100Control.WorkItem.GetSnapShotResult(this.toolBoxSmartPart.ToolBoxEntity.KeyId, null);

                    if (queryData.ListKeyId.Rows.Count > 0)
                    {
                        this.CurrentExciseTaxStatementId = null;
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

                        ////FillExciseTaxStatementFormDetails function is used to fill the Statement details in Excise Tax Statement 
                        this.FillExciseTaxStatementFormDetails(queryData.ListKeyId, false);
                        this.FilteredStatusControl.FilterStatus = true;
                        this.ClearFilterControl.Enabled = true;

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
                this.statusBarSmartPart.StatusBarEntity.OptionalInputParameter = new object[] { this.WhereCondition, this.CurrentSnapshotName, this.CurrentSnapshotDescription, queryingFields.ExciseTaxStatementQueryingFields };
                this.statusBarSmartPart.StatusBarEntity.ParentWorkItem = this.F1100Control.WorkItem;

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
                                queryData = this.F1100Control.WorkItem.ExecuteSnapshot(this.FilterTypeId, this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }
                            else
                            {
                                queryData = this.F1100Control.WorkItem.ExecuteQuery(this.statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);
                            }

                            if (queryData.ListKeyId.Rows.Count > 0)
                            {
                                this.CurrentExciseTaxStatementId = null;
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], SharedFunctions.GetResourceString("RecordSearchedString"), queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], SharedFunctions.GetResourceString("MatchesFoundString")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                this.WhereCondition = this.statusBarSmartPart.StatusBarEntity.WhereCondition;
                                this.UserDefinedWhereCondition = this.statusBarSmartPart.StatusBarEntity.UserDefinedWhereCondition;
                                this.ClearFilterControl.Enabled = true;

                                ////Set page status
                                this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                                this.FilteredStatusControl.FilterStatus = true;  

                                //// FillStatementDetails function is used to fill the Statement details in ExciseTaxstatement                                
                                this.FillExciseTaxStatementFormDetails(queryData.ListKeyId, false);
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

        #region Private Methods        

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizeExciseTaxReceiptGridView
        /// </summary>
        private void CustomizeExciseTaxReceiptGridView()
        {            
            DataGridViewColumnCollection columns = this.ExciseTaxReceiptGridView.Columns;

            columns["Item"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.ItemColumn.ColumnName;
            columns["StateRate"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.StateRateColumn.ColumnName;
            columns["StateAmount"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.StateAmountColumn.ColumnName;
            columns["LocalRate"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.LocalRateColumn.ColumnName;
            columns["LocalAmount"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.LocalAmountColumn.ColumnName;
            columns["FeeType"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.FeeTypeColumn.ColumnName;
            columns["FeeAmount"].DataPropertyName = this.exciseTaxStatement.GetExciseTaxReceipt.FeeAmountColumn.ColumnName;

            columns["Item"].DisplayIndex = 0;
            columns["StateRate"].DisplayIndex = 1;
            columns["StateAmount"].DisplayIndex = 2;
            columns["LocalRate"].DisplayIndex = 3;
            columns["LocalAmount"].DisplayIndex = 4;
            columns["FeeType"].DisplayIndex = 5;
            columns["FeeAmount"].DisplayIndex = 6;

            this.exciseTaxStatement.InitializeExciseTaxReceipt();
            this.ExciseTaxReceiptGridView.DataSource = this.exciseTaxStatement.GetExciseTaxReceipt.DefaultView;           
        }

        /// <summary>
        /// Handles the CellFormatting event of the ExciseTaxReceiptGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ExciseTaxReceiptGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;

            //// Only paint if desired, formattable column

            if (e.ColumnIndex == this.ExciseTaxReceiptGridView.Columns["StateRate"].Index || e.ColumnIndex == this.ExciseTaxReceiptGridView.Columns["LocalRate"].Index)
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
                        outDecimal = outDecimal / 100;
                        e.Value = outDecimal.ToString("0.00%");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "0.00%";
                    }
                }
                else
                {
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.ExciseTaxReceiptGridView.Columns["StateAmount"].Index || e.ColumnIndex == this.ExciseTaxReceiptGridView.Columns["LocalAmount"].Index || e.ColumnIndex == this.ExciseTaxReceiptGridView.Columns["FeeAmount"].Index)
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
        
        /// <summary>
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <param name="onclose">onclose</param>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus(bool onclose)
        {
            if (!this.PageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    bool status = this.SaveReceipt(onclose);

                    if (status)
                    {
                        this.PageMode = TerraScanCommon.PageModeTypes.View;
                    }

                    return status;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (onclose)
                    {
                        this.PageMode = TerraScanCommon.PageModeTypes.View;
                    }
                    else
                    {
                        this.CancelReceiptButton_Click(this.CancelReceiptButton, EventArgs.Empty);
                    }

                    return true;
                }

                return false;
            }

            return true;            
        }

        /// <summary>
        /// set focus to the next/previous input field  
        /// </summary>
        /// <param name="sourceControl">control to start the search</param>
        /// <param name="key">The Key.</param>
        /// <returns>if true retrieve next control ,else retrieve previous control</returns>
        private Control GetSmartPartControl(Control sourceControl, string key)
        {
            Control requiredControl = sourceControl;

            if (sourceControl != null)
            {
                if (sourceControl.Controls.ContainsKey(key))
                {
                    return sourceControl.Controls[key];
                }

                foreach (Control sampControl in sourceControl.Controls)
                {
                    if (sampControl.Controls.Count > 0)
                    {
                        requiredControl = this.GetSmartPartControl(sampControl, key);
                        if (requiredControl.Name.Equals(key))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                requiredControl = new Control();
            }

            return requiredControl;
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
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, false, null, Color.White);
                this.TopPanel.BackColor = Color.White;

                this.ReceiptNumberTextBox.Visible = false;
                this.GrantorTextBox.Visible = false;
                this.GranteeTextBox.Visible = false;
                this.DistrictTextBox.Visible = false;
                this.ReceiptNumberLinkLabel.Visible = true;
                this.GrantorLinkLabel.Visible = true;
                this.GranteeLinkLabel.Visible = true;
                this.DistrictLinkLabel.Visible = true;
                this.PaymentDateButton.Visible = true;
                this.FormDateButton.Visible = true;
                this.PaymentDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Date;
                this.FormDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Date;

                this.PaymentDateTextBox.MaxLength = 10;
                this.FormDateTextBox.MaxLength = 10;
                this.StatusTotalTextBox.PersistDefaultColor = true;
                this.LocalTotalTextBox.PersistDefaultColor = true;
                this.FeeTatalTextBox.PersistDefaultColor = true;

                this.ReceiptPanel.Enabled = true;                                        
                this.ReportActionDeckWorkspace.Enabled = true;
                this.OtherCommandButtonsPanel.Enabled = true;
                this.StatusBarDeckWorkspace.Enabled = true;
                this.RecordNavigatorDeckWorkspace.Enabled = true;
                this.CommentsdeckWorkspace.Enabled = true;
            }
            else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////set Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, true, this.userDefinedWhereCondition, Color.FromArgb(204, 255, 204));
                this.TopPanel.BackColor = Color.FromArgb(204, 255, 204);

                this.ReceiptNumberTextBox.Visible = true;
                this.GrantorTextBox.Visible = true;
                this.GranteeTextBox.Visible = true;
                this.DistrictTextBox.Visible = true;
                this.ReceiptNumberLinkLabel.Visible = false;
                this.GrantorLinkLabel.Visible = false;
                this.GranteeLinkLabel.Visible = false;
                this.DistrictLinkLabel.Visible = false;
                this.PaymentDateButton.Visible = false;
                this.FormDateButton.Visible = false;
                this.PaymentDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.FormDateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;

                this.PaymentDateTextBox.MaxLength = 32767;
                this.FormDateTextBox.MaxLength = 32767;
                this.StatusTotalTextBox.PersistDefaultColor = false;
                this.LocalTotalTextBox.PersistDefaultColor = false;
                this.FeeTatalTextBox.PersistDefaultColor = false;

                this.ReceiptPanel.Enabled = false;                                      
                this.ReportActionDeckWorkspace.Enabled = false;
                this.OtherCommandButtonsPanel.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                this.RecordNavigatorDeckWorkspace.Enabled = false;
                this.CommentsdeckWorkspace.Enabled = false;
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
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = new CommentsData.GetCommentsCountDataTable();
                    AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                    AdditionalOperationCountEntity receiptAdditionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                    if (onload && this.CurrentExciseTaxStatementId.HasValue)
                    {
                        additionalOperationCountEntity.AttachmentCount = this.F1100Control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), this.CurrentExciseTaxStatementId.Value, TerraScanCommon.UserId);
                        commentsCountDataTable = this.F1100Control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), this.CurrentExciseTaxStatementId.Value, TerraScanCommon.UserId);
                        if (commentsCountDataTable.Rows.Count > 0)
                        {
                            additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                            additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                        }

                        if (this.currentExciseTaxReceiptId.HasValue)
                        {
                            receiptAdditionalOperationCountEntity.AttachmentCount = this.F1100Control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), this.currentExciseTaxReceiptId.Value, TerraScanCommon.UserId);
                            commentsCountDataTable = this.F1100Control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), this.currentExciseTaxReceiptId.Value, TerraScanCommon.UserId);
                            if (commentsCountDataTable.Rows.Count > 0)
                            {
                                receiptAdditionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                                receiptAdditionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                            }
                        }
                    }

                    this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
                    this.receiptAdditionalOperationSmartPart.AdditionalOperationCountEnt = receiptAdditionalOperationCountEntity;
                }
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        

        /// <summary>
        /// Sets the page mode property.
        /// </summary>
        private void SetPageModeProperty()
        {
            switch (this.PageMode)
            {
                case TerraScanCommon.PageModeTypes.New:
                    this.NewButton.Enabled = false;
                    this.CancelReceiptButton.Enabled = true;
                    this.SaveButton.Enabled = true;

                    this.PaymentDateTextBox.LockKeyPress = false;
                    this.FormDateTextBox.LockKeyPress = false;
                    this.PaymentDateButton.Enabled = true;
                    this.FormDateButton.Enabled = true;                    

                    ////this.PaymentDateTextBox.TabStop = true;
                    ////this.FormDateTextBox.TabStop = true;
                    this.PaymentEngineUserControl.TabStop = true;

                    ////enable paymentengine
                    this.PaymentEngineUserControl.Locked = false;
                    this.PaymentEngineUserControl.SetDefaultSelection = true;

                    ////attachment and comments button 
                    this.ReceiptCommentsDeckWorkspace.Enabled = false;

                    ////Audit link
                    this.ExciseTaxReceiptAuditLinkLabel.Text = "tTR_Rcpt [ReceiptID]";
                    this.ExciseTaxReceiptAuditLinkLabel.Enabled = false;                    

                    break;
                default:
                    if (this.TotalDueTextBox.DecimalTextBoxValue <= 0 || this.currentExciseTaxReceiptId.HasValue)
                    {
                        this.NewButton.Enabled = false;
                    }
                    else
                    {
                        this.NewButton.Enabled = true && this.PermissionFiled.newPermission;
                    }

                    this.CancelReceiptButton.Enabled = false;
                    this.SaveButton.Enabled = false;

                    this.PaymentDateTextBox.LockKeyPress = true;
                    this.FormDateTextBox.LockKeyPress = true;
                    this.PaymentDateButton.Enabled = false;
                    this.FormDateButton.Enabled = false; 

                    ////this.PaymentDateTextBox.TabStop = false;
                    ////this.FormDateTextBox.TabStop = false;
                    this.PaymentEngineUserControl.TabStop = false;

                    ////enable paymentengine
                    this.PaymentEngineUserControl.Locked = true;
                    this.PaymentEngineUserControl.SetDefaultSelection = false;                    

                    ////Audit link     
                    if (this.currentExciseTaxReceiptId.HasValue)
                    {
                        this.ExciseTaxReceiptAuditLinkLabel.Text = string.Concat("tTR_Rcpt [ReceiptID] ", this.currentExciseTaxReceiptId);
                        this.ExciseTaxReceiptAuditLinkLabel.Enabled = true;

                        ////attachment and comments button 
                        this.ReceiptCommentsDeckWorkspace.Enabled = true;
                    }
                    else
                    {
                        this.ExciseTaxReceiptAuditLinkLabel.Text = "tTR_Rcpt [ReceiptID]";
                        this.ExciseTaxReceiptAuditLinkLabel.Enabled = false;

                        ////attachment and comments button 
                        this.ReceiptCommentsDeckWorkspace.Enabled = false;
                    }

                    break;
            }

            this.SetAdditionalOperationCount(true);
            this.CommentsdeckWorkspace.Enabled = true;
        }   

        /// <summary>
        /// Sets the tagname with querying field name
        /// </summary>
        private void SetQueryingFieldName()
        {
            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            this.StatementIDTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.STATEMENTIDColumn.ColumnName;
            this.ParcelNumberTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.PARCELNUMBERColumn.ColumnName;
            this.SaleDateTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.SALEDATEColumn.ColumnName;
            this.PaymentDateTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.PAYMENTDATEColumn.ColumnName;
            this.FormDateTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.FORMDATEColumn.ColumnName;
            this.MobileHomeTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.MOBILEHOMEColumn.ColumnName;
            this.ReceiptNumberTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.RECEIPTNUMBERColumn.ColumnName;
            this.DistrictTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.DISTRICTColumn.ColumnName;
            this.SaleAmountTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.SALEAMOUNTColumn.ColumnName;
            this.TaxCodeTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.TAXCODEColumn.ColumnName;
            this.GrantorTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.GRANTORColumn.ColumnName;
            this.GranteeTextBox.QueryingFileldName = queryingFields.ExciseTaxStatementQueryingFields.GRANTEEColumn.ColumnName;
        } 

        #endregion   
        
        #region Comments
       
        /// <summary>
        /// Handles the Click event of the ReceiptCommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptCommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentExciseTaxReceiptId.HasValue)
                {
                    ////1000 for receipt
                    object[] optionalParameter = new object[] { 1000, this.currentExciseTaxReceiptId.Value, Convert.ToInt32(this.Tag) };
                    Form comments = new Form();
                    comments = this.form1100Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.F1100Control.WorkItem);
                    if (comments != null)
                    {                        
                        comments.ShowDialog();
                        ////TerraScanCommon.SetRecordCount(Convert.ToInt32(this.Tag), this.currentExciseTaxReceiptId.Value, this.ReceiptCommentButton, this.ReceiptAttachmentButton, this);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Attachment

        /// <summary>
        /// Handles the Click event of the ReceiptAttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptAttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { Convert.ToInt32(this.Tag.ToString()), Convert.ToInt32(this.currentExciseTaxReceiptId), Convert.ToInt32(this.Tag.ToString()) };
                Form attachment = new Form();
                attachment = this.form1100Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.F1100Control.WorkItem);
                if (attachment != null)
                {
                    attachment.ShowDialog();
                    ////todo: this.SetButtonText(this, new DataEventArgs<AdditionalOperationCountEntity>(F1010WorkItem.AdditionOperationText(Convert.ToInt32(this.Tag.ToString()), this.currentImportId, TerraScanCommon.UserId)));
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

        #region Calculation

        /// <summary>
        /// Calculatings the balance.
        /// </summary>
        private void CalculateBalance()
        {
            decimal balance = 0;           

            ////calculating total balance

            balance = this.TotalDueTextBox.DecimalTextBoxValue - this.PaymentsTotalTextBox.DecimalTextBoxValue;
            this.BalanceAmountTextBox.Text = balance.ToString();

            ////changing colors of the BalanceAmountTextBox background, depending on balance
            if (balance == 0)
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceAmountTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceAmountTextBox.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Calculating the balance.
        /// </summary>
        private void CalculateTotalDue()
        {            
            decimal totalDue = 0;
            
            ////calculating total due

            this.StatusTotalTextBox.Text = this.exciseTaxStatement.GetExciseTaxReceipt.Compute(string.Concat("SUM(", this.exciseTaxStatement.GetExciseTaxReceipt.StateAmountColumn.ColumnName, ")"), "").ToString();
            this.LocalTotalTextBox.Text = this.exciseTaxStatement.GetExciseTaxReceipt.Compute(string.Concat("SUM(", this.exciseTaxStatement.GetExciseTaxReceipt.LocalAmountColumn.ColumnName, ")"), "").ToString();
            this.FeeTatalTextBox.Text = this.exciseTaxStatement.GetExciseTaxReceipt.Compute(string.Concat("SUM(", this.exciseTaxStatement.GetExciseTaxReceipt.FeeAmountColumn.ColumnName, ")"), "").ToString();
            totalDue = this.StatusTotalTextBox.DecimalTextBoxValue + this.LocalTotalTextBox.DecimalTextBoxValue + this.FeeTatalTextBox.DecimalTextBoxValue;
            this.TotalDueTextBox.Text = totalDue.ToString();           
        }

        #endregion

        #region Payment Engine Events

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
        {
            this.PaymentsTotalTextBox.Text = amount.ToString();
            this.CalculateBalance();
        }

        #endregion

        #region New Receipt

        /// <summary>
        /// Handles the Click event of the NewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewButton_Click(object sender, EventArgs e)
        {
            if (this.NewButton.Enabled)
            {
                this.NewButton.Focus();

                this.Cursor = Cursors.WaitCursor;

                this.SaveButton.Enabled = true;
                this.CancelReceiptButton.Enabled = true;

                ////setting the pagemode
                this.PageMode = TerraScanCommon.PageModeTypes.New;                           
                this.CurrentExciseTaxReceiptId = null;

                ////reset attachment and comment     
   
                ////change controls property
                this.SetPageModeProperty();
                this.Cursor = Cursors.Default;
            }
        }

        #endregion

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (this.SaveButton.Enabled)
            {
                this.SaveButton.Focus();
                if (this.SaveReceipt(false))
                {
                    RecordNavigatorSmartPart recordNavigatorSmartPart = (RecordNavigatorSmartPart)this.F1100Control.WorkItem.SmartParts[SmartPartNames.RecordNavigatorSmartPart];
                    recordNavigatorSmartPart.SetFocus = true;
                }
            }
        }

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [on close].</param>
        /// <returns>true - sucessfull save</returns>
        private bool SaveReceipt(bool onclose)
        {            
            DateTime outPaymentDate;
            DateTime outFormDate;

            ////Check For Required Fields
            if (String.IsNullOrEmpty(this.PaymentDateTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.PaymentDateTextBox.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.FormDateTextBox.Text.Trim()))
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.FormDateTextBox.Focus();
                return false;
            }

            this.Cursor = Cursors.WaitCursor;

            ////checks the receiptdate 
            if (!DateTime.TryParse(this.FormDateTextBox.Text.Trim(), out outFormDate))
            {
                outFormDate = Convert.ToDateTime(this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.FormDateColumn]);
            }

            ////checks for valid receipt
            string validResult = this.F1100Control.WorkItem.GetValidReceiptTest(this.currentExciseTaxStatementId.Value, outFormDate);
            if (!String.IsNullOrEmpty(validResult))
            {
                ////TODO title hardcode - needs some clarification
                MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), validResult), string.Concat(ConfigurationWrapper.ApplicationSave, " - ", "Invalid Receipt"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                return false;
            }

            ////checks finding save/batch            

            if (!String.IsNullOrEmpty(this.BalanceAmountTextBox.Text))
            {
                if (this.BalanceAmountTextBox.DecimalTextBoxValue != 0)
                {
                    ////TODO title hardcode - needs some clarification

                    if (MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), string.Concat(ConfigurationWrapper.ApplicationSave, " - ", "Unpaid Receipt(s)"), MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        this.Cursor = Cursors.Default;
                        return false;
                    }
                }
                else  
                {
                    ////create payment
                    this.ppaymentId = this.PaymentEngineUserControl.CreatePayment();
                }
            }            
            
            ////checks valid interestdate
            if (!DateTime.TryParse(this.PaymentDateTextBox.Text.Trim(), out outPaymentDate))
            {
                outPaymentDate = Convert.ToDateTime(this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.PaymentDateColumn]);
            }

            ////save receipt
            if (this.PageMode.Equals(TerraScanCommon.PageModeTypes.New))
            {
                this.exciseTaxStatement.SaveExciseTaxReceipt.Rows.Clear();
                ExciseTaxStatementData.SaveExciseTaxReceiptRow dr = this.exciseTaxStatement.SaveExciseTaxReceipt.NewSaveExciseTaxReceiptRow();

                dr.StatementID = this.currentExciseTaxStatementId.Value;
                dr.ReceiptDate = outFormDate.ToShortDateString();
                dr.InterestDate = outPaymentDate.ToShortDateString();
                dr.UserID = TerraScanCommon.UserId;
                dr.PPaymentID = this.ppaymentId;

                this.exciseTaxStatement.SaveExciseTaxReceipt.Rows.Add(dr);

                DataSet tempDataSet = new DataSet("Root");
                tempDataSet.Tables.Add(this.exciseTaxStatement.SaveExciseTaxReceipt.Copy());
                tempDataSet.Tables[0].TableName = "Table";

                this.exciseTaxStatement.ExciseTaxReceiptResultSet.Clear();
                this.exciseTaxStatement.Merge(this.F1100Control.WorkItem.SaveExciseTaxReceipt(tempDataSet.GetXml(), TerraScanCommon.UserId));

                if (this.exciseTaxStatement.ExciseTaxReceiptResultSet.Rows.Count > 0 && !this.exciseTaxStatement.ExciseTaxReceiptResultSet.Rows[0][this.exciseTaxStatement.ExciseTaxReceiptResultSet.ReceiptIDColumn].Equals(System.DBNull.Value))
                {
                    bool autoPrintStatus = Convert.ToBoolean(this.exciseTaxStatement.ExciseTaxReceiptResultSet.Rows[0][this.exciseTaxStatement.ExciseTaxReceiptResultSet.IsAutoPrintColumn]);
                    this.SetAutoPrint(this, new DataEventArgs<bool>(autoPrintStatus));

                    if (autoPrintStatus)
                    {
                        this.reportOptionalParameter.Clear();
                        this.reportOptionalParameter.Add("KeyName", "ReceiptID");
                        this.reportOptionalParameter.Add("KeyValue", this.exciseTaxStatement.ExciseTaxReceiptResultSet.Rows[0][this.exciseTaxStatement.ExciseTaxReceiptResultSet.ReceiptIDColumn]);
                        ////changed the parameter type from string to int
                        TerraScanCommon.ShowReport(11001, Report.ReportType.PrintDefault, this.reportOptionalParameter);
                    }
                }  

                if (onclose)
                {
                    return true;
                }
            }

            ////populate with the new values        
            this.FillExciseTaxStatementFormDetails(null, false);            
            this.Cursor = Cursors.Default;

            return true;
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Handles the Click event of the CancelReceiptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelReceiptButton_Click(object sender, EventArgs e)
        {
            this.CancelReceiptButton.Focus();
            this.FillExciseTaxStatementFormDetails(null, false);
            this.NewButton.Focus();
        }

        #endregion   
   
        #region Report Funtion

        #endregion

        #region Other Command Buttons Function

        /// <summary>
        /// Handles the Click event of the AffidavitFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffidavitFormButton_Click(object sender, EventArgs e)
        {
            FormInfo formInfo = TerraScanCommon.GetFormInfo(1105);
            if (this.currentExciseTaxStatementId.HasValue)
            {
                formInfo.optionalParameters = new object[] { this.currentExciseTaxStatementId };
            }
            
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));           
        }

        /// <summary>
        /// Handles the Click event of the ExciseRatesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseRatesButton_Click(object sender, EventArgs e)
        {
            FormInfo formInfo = TerraScanCommon.GetFormInfo(1101);
            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        /// <summary>
        /// Handles the Click event of the WorkQueueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WorkQueueButton_Click(object sender, EventArgs e)
        {           
            FormInfo formInfo = TerraScanCommon.GetFormInfo(1107);
            if (this.currentExciseTaxStatementId.HasValue)
            {
                formInfo.optionalParameters = new object[] { this.currentExciseTaxStatementId };
            }

            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        #endregion  

        #region Links

        /// <summary>
        /// Handles the LinkClicked event of the ReceiptNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ReceiptNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized 
            ////Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                ////this.Cursor = Cursors.WaitCursor;
                ////object[] optionalParameter = new object[] { this.currentExciseTaxReceiptId.Value };
                ////Form receiptNumberForm = new Form();
                ////receiptNumberForm = TerraScanCommon.GetForm(1001, optionalParameter, this.form1100Control.WorkItem);
                ////if (receiptNumberForm != null)
                ////{
                ////    receiptNumberForm.ShowDialog();
                ////}

                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.currentExciseTaxReceiptId.Value;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
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
        /// Handles the LinkClicked event of the DistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void DistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized 
            ////Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.DistrictIDColumn] };
                Form districtForm = new Form();
                districtForm = this.form1100Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9500, optionalParameter, this.form1100Control.WorkItem);
                if (districtForm != null)
                {
                    districtForm.ShowDialog();
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
        /// Handles the LinkClicked event of the GrantorLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GrantorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized 
            ////Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.GrantorOwnerIDColumn] };
                Form grantorForm = new Form();
                grantorForm = this.form1100Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9100, optionalParameter, this.form1100Control.WorkItem);
                if (grantorForm != null)
                {
                    grantorForm.ShowDialog();
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
        /// Handles the LinkClicked event of the GranteeLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void GranteeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // TODO : Genralized 
            ////Hashtable reportOptionalParameter = new Hashtable();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.exciseTaxStatement.GetExciseTaxStatement.Rows[0][this.exciseTaxStatement.GetExciseTaxStatement.GranteeOwnerIDColumn] };
                Form granteeForm = new Form();
                granteeForm = this.form1100Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9100, optionalParameter, this.form1100Control.WorkItem);
                if (granteeForm != null)
                {
                    granteeForm.ShowDialog();
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

        #endregion links

        #region Audit

        /// <summary>
        /// Handles the LinkClicked event of the ExciseTaxReceiptAuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExciseTaxReceiptAuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.currentExciseTaxReceiptId.HasValue)
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("ReceiptID", this.currentExciseTaxReceiptId.Value.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ExciseTaxStatementAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data</param>
        private void ExciseTaxStatementAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.currentExciseTaxStatementId.HasValue)
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("StatementID", this.currentExciseTaxStatementId.Value.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        #endregion Audit

        #region Date Related Calender Controls Events  

        /// <summary>
        /// Handles the DateSelected event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ExciseTaxStatementMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.SetSeletedDate(e.Start.ToString("MM/dd/yyyy"));
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            // Assign the selected date to the DateTextbox.
            if (this.ExciseTaxStatementMonthCalendar.Tag.ToString() == this.PaymentDateButton.Tag.ToString())
            {
                this.ExciseTaxStatementMonthCalendar.Tag = string.Empty;
                this.PaymentDateButton.Focus();
                this.PaymentDateTextBox.Text = dateSelected;
            }
            else if (this.ExciseTaxStatementMonthCalendar.Tag.ToString() == this.FormDateButton.Tag.ToString())
            {
                this.ExciseTaxStatementMonthCalendar.Tag = string.Empty;
                this.FormDateButton.Focus();
                this.FormDateTextBox.Text = dateSelected;
            }

            this.ExciseTaxStatementMonthCalendar.Visible = false;
        }             

        /// <summary>
        /// Handles the Click event of the PaymentDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentDateButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            this.ShowPaymentDateCalender();
        }

        /// <summary>
        /// Handles the Click event of the FormDateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormDateButton_Click(object sender, EventArgs e)
        {
            // Calls the method to show the calender control.
            this.ShowFormDateCalender();
        }

        /// <summary>
        /// Shows the InterestDate calender in particular location.
        /// </summary>
        private void ShowFormDateCalender()
        {
            this.ExciseTaxStatementMonthCalendar.Visible = true;
            this.ExciseTaxStatementMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.
            this.ExciseTaxStatementMonthCalendar.Left = this.TopPanel.Left + this.FromDatePanel.Left + this.FormDateButton.Left + this.FormDateButton.Width;
            this.ExciseTaxStatementMonthCalendar.Top = this.TopPanel.Top + this.FromDatePanel.Top + this.FormDateButton.Top;
            this.ExciseTaxStatementMonthCalendar.Tag = this.FormDateButton.Tag;
            this.ExciseTaxStatementMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.FormDateTextBox.Text))
            {
                this.ExciseTaxStatementMonthCalendar.SetDate(Convert.ToDateTime(this.FormDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the RecieptDate calender in particular location.
        /// </summary>
        private void ShowPaymentDateCalender()
        {
            this.ExciseTaxStatementMonthCalendar.Visible = true;
            this.ExciseTaxStatementMonthCalendar.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.
            this.ExciseTaxStatementMonthCalendar.Left = this.TopPanel.Left + this.PaymentDatePanel.Left + this.PaymentDateButton.Left + this.PaymentDateButton.Width;
            this.ExciseTaxStatementMonthCalendar.Top = this.TopPanel.Top + this.SaleDatePanel.Top + this.PaymentDateButton.Top;
            this.ExciseTaxStatementMonthCalendar.Tag = this.PaymentDateButton.Tag;
            this.ExciseTaxStatementMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.PaymentDateTextBox.Text))
            {
                this.ExciseTaxStatementMonthCalendar.SetDate(Convert.ToDateTime(this.PaymentDateTextBox.Text));
            }
        }

        /// <summary>
        /// Handles the Leave event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseTaxStatementMonthCalendar_Leave(object sender, EventArgs e)
        {
            this.ExciseTaxStatementMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ExciseTaxStatementMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SetSeletedDate(this.ExciseTaxStatementMonthCalendar.SelectionStart.ToString("MM/dd/yyyy"));
            }
        }

        /// <summary>
        /// Handles the Validating event of the ExciseTaxStatementMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ExciseTaxStatementMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            ////    this.ExciseTaxStatementMonthCalendar.Visible = false;
            ////    e.Cancel = true;

            ////    if (this.ExciseTaxStatementMonthCalendar.Tag.Equals(this.FormDateButton.Tag))
            ////    {
            ////        this.FocusRequiredInputField(this.FormDateButton, true, true);
            ////    }
            ////    else if (this.MortgageMonthCalendar.Tag.Equals(this.PaymentDateButton.Tag))
            ////    {
            ////        this.FocusRequiredInputField(this.PaymentDateButton, true, true);
            ////    }

            ////    e.Cancel = false;
        }

        #endregion       
    }
}
