//--------------------------------------------------------------------------------------------
// <copyright file="F1101.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1101.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 July 06       JYOTHI              Created
// 26 July 06       JYOTHI              PopulateSearchResults added
// 27 July 06       JYOTHI              Added PopulateRecord
//*********************************************************************************/

namespace D1100
{   
using System;
using System.Collections.Generic;
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
using TerraScan.BusinessEntities;
using TerraScan.Utilities;
using System.Configuration;
using TerraScan.UI.Controls;
using TerraScan.Common.Reports;
using System.Web.Services.Protocols;
using TerraScan.Infrastructure.Interface.Constants;
using System.Collections;

    /// <summary>
    /// F1101 class file
    /// </summary>
    [SmartPart]
    public partial class F1101 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// exciseRateIds variable is used to store list of exciseRateIds for Excise Tax Rate. 
        /// </summary>       
        private ExciseTaxRateData exciseRateIds = new ExciseTaxRateData();

        /// <summary>
        /// totalExciseIdsCount variable is used to find total number of exciseRateIds for Excise Tax Rate. 
        /// </summary>
        private int totalExciseIdsCount;

        /// <summary>
        /// currentRecord Local variable
        /// </summary>
        private int currentRecord;

        /// <summary>
        /// form1101Control Controller
        /// </summary>
        private F1101Controller form1101Control;

        /// <summary>
        /// TerraScanCommon.PageStatus Enum - used to find normal or filtered mose
        /// </summary>
        private TerraScanCommon.PageStatus pageStatus;

        /// <summary>
        /// exciseTaxRateDataSet variable is used to get the details of Excise Tax Rate.
        /// </summary>
        private ExciseTaxRateData exciseTaxRateDataSet = new ExciseTaxRateData();

        /// <summary>
        /// recordPointerArray variable
        /// </summary>
        private int[] recordPointerArray = new int[2];

        /// <summary>
        /// makeButtonEnable variable
        /// </summary>
        private string[] makeButtonEnable = new string[2];

        /// <summary>
        /// formPermission variable
        /// </summary>
        private DataSet formPermission = new DataSet();

        /// <summary>
        /// permissionForButton variable
        /// </summary>
        private int[] permissionForButton = new int[4];
        
        /// <summary>
        /// Variable accountId 
        /// </summary>
        private int accountId;

        /// <summary>
        /// Variable accountValue 
        /// </summary>
        private string selectedAccountName;        

        /// <summary>
        /// Created control for clear Filter
        /// </summary>
        private Control clearFilterControl = new Control();

        /// <summary>
        /// Created control for Filtered Status
        /// </summary>
        private TerraScanButton filteredStatusControl = new TerraScanButton();

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
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        /// terrascantextbox array used to iterate the controls for querying process
        /// </summary>
        private TerraScanTextBox[] queryControlArray;

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;

        /// <summary>
        /// operationSmartPart
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1101"/> class.
        /// </summary>
        public F1101()
        {
            this.InitializeComponent();
            this.SetQueryingFieldName();
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "District Info", 174, 150, 94);
            this.DistrictInfoSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.DistrictInfoSecIndicatorPictureBox.Height, this.DistrictInfoSecIndicatorPictureBox.Width, "Account Info", 28, 81, 128);
        }
        #endregion  
        
        #region Event Publication

        /// <summary>
        /// Event Publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// Event Publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event Publication for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event Publication for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        /// <summary>
        /// Event Publication for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion       

        #region Properties

        /// <summary>
        /// Gets or sets the F1101 control.
        /// </summary>
        /// <value>The F1101 control.</value>
        [CreateNew]
        public F1101Controller Form1101Control
        {
            get { return this.Form1101Control as F1101Controller; }
            set { this.form1101Control = value; }
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

        ///// <summary>
        ///// Gets or sets the AccountId
        ///// </summary>
        ///// <value>The AccountId.</value>
        ////public int AccountId
        ////{
        ////    set { this.accountId = value; }
        ////    get { return this.accountId; }
        ////}

        ///// <summary>
        ///// Gets or sets the AccountValue
        ///// </summary>
        ///// <value>The accountValue.</value>
        ////public int AccountValue
        ////{
        ////    set { this.accountValue = value; }
        ////    get { return this.accountValue; }
        ////}

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

                //// Calling the Common Function for Report
                // changed the parameter type from string to int
                TerraScanCommon.ShowReport(11011, Report.ReportType.Print);
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
                //// changed the parameter type from string to int
                TerraScanCommon.ShowReport(11011, Report.ReportType.Preview);
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
                ////// calling  Common Function For Report
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(11011, Report.ReportType.Email);
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

        #endregion         

        #region EventSubcription

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.FillExciseRateDetails(null, recordNavigationEntity.RecordIndex);
        }

        /// <summary>
        /// Checks the page status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_CheckPageStatus, Thread = ThreadOption.UserInterface)]
        public void CheckPageStatus(object sender, DataEventArgs<Button> e)
        {
            if (this.CheckPageStatus(false))
            {
                this.PageStatusActivated(this, new DataEventArgs<Button>(e.Data));
            }
        }

        /// <summary>
        /// Operations the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, Thread = ThreadOption.UserInterface)]
        public void OperationButtonClick(object sender, DataEventArgs<string> e)
        {
            switch (e.Data)
            {
                case "NEW":
                    this.NewButton_Click();
                    break;
                case "SAVE":
                    this.SaveButton_Click();
                    break;
                case "CANCEL":
                    this.CancelButton_Click();
                    break;
                case "DELETE":
                    this.DeleteButton_Click();
                    break;
            }
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
        ///Filtered Button Click.
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
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form1101Control.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
            }
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
        /// Called when [audit link click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.AuditLinkClick, ThreadOption.UserInterface)]
        public void OnAuditLinkClick(object sender, DataEventArgs<LinkLabel> eventArgs)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////// calling  Common Function For Report
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Add("ExciseRateID", this.RateDistrictIDTextBox.Text);
                TerraScanCommon.ShowReport(110109, Report.ReportType.Preview, reportOptionalParameter);
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

        /// <summary>
        /// Handles the Load event of the F1101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1101_Load(object sender, EventArgs e)
        {
            try
            {
            this.LoadWorkSpaces();
            this.GetLocalType();
            this.FillExciseRateDetails(null, -1);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            ////used to iterate the controls for querying proce
            this.queryControlArray = new TerraScanTextBox[] { this.RateDistrictIDTextBox, this.AdminFeesTextBox, this.TransactionFeesTextBox, this.TechFeesTextBox, this.YearTextBox, this.LocalRateTextBox, this.LocalIsTextBox, this.TotalTaxRateTextBox, this.TaxDistrictTextBox, this.DescriptionTextBox };
            }
            catch (SoapException soapEx)
            {
                ExceptionManager.ManageException(soapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void LockControls(bool enableValue)
        {
            //// District Info LockControls
            this.RateDistrictIDTextBox.Enabled = enableValue;
            this.AdminFeesTextBox.Enabled = enableValue;
            this.TransactionFeesTextBox.Enabled = enableValue;
            this.TechFeesTextBox.Enabled = enableValue;
            this.YearTextBox.Enabled = enableValue;
            this.LocalRateTextBox.Enabled = enableValue;
            this.LocalIsComboBox.Enabled = enableValue;
            this.TotalTaxRateTextBox.Enabled = enableValue;
            this.TaxDistrictTextBox.Enabled = enableValue;
            this.DescriptionTextBox.Enabled = enableValue;
            this.TaxDistrictButton.Enabled = enableValue;

            //// Account Info LockControls
            this.AccountAdminFeeTextBox.Enabled = enableValue;
            this.AccountTransactionFeeTextBox.Enabled = enableValue;
            this.AccountTechnologyFeeTextBox.Enabled = enableValue;
            this.AccountLocalTaxTextBox.Enabled = enableValue;
            this.AccountLocalInterestTextBox.Enabled = enableValue;
            this.AccountLocalPenaltyTextBox.Enabled = enableValue;
            this.AccountStateTaxTextBox.Enabled = enableValue;
            this.AccountStateInterestTextBox.Enabled = enableValue;
            this.AccountStatePenaltyTextBox.Enabled = enableValue;
        }

        /// <summary>
        /// Clears the excise details.
        /// </summary>
        private void ClearExciseDetails()
        {
            //// District Info LockControls
            if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm) && !string.IsNullOrEmpty(this.UserDefinedWhereCondition.Trim()))
            {
                this.RateDistrictIDTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.RateDistrictIDTextBox.QueryingFileldName);
                this.AdminFeesTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.AdminFeesTextBox.QueryingFileldName);
                this.TransactionFeesTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.TransactionFeesTextBox.QueryingFileldName);
                this.TechFeesTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.TechFeesTextBox.QueryingFileldName);
                this.YearTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.YearTextBox.QueryingFileldName);
                this.LocalRateTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.LocalRateTextBox.QueryingFileldName);
                this.LocalIsTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.LocalIsTextBox.QueryingFileldName);
                this.TotalTaxRateTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.TotalTaxRateTextBox.QueryingFileldName);
                this.TaxDistrictTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.TaxDistrictTextBox.QueryingFileldName);
                this.DescriptionTextBox.Text = SharedFunctions.FindFilterCriteria(this.userDefinedWhereCondition, this.DescriptionTextBox.QueryingFileldName);
            }
            else
            {
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm))
                {
                    this.AdminFeesTextBox.Text = string.Empty;
                    this.TransactionFeesTextBox.Text = string.Empty;
                    this.TechFeesTextBox.Text = string.Empty;
                    this.LocalRateTextBox.Text = string.Empty;
                }
                else
                {
                    this.AdminFeesTextBox.Text = "0";                    
                    this.LocalRateTextBox.Text = "0";
                    this.LocalIsComboBox.SelectedValue = 1;
                    if (this.exciseRateIds.ListExciseTaxRate.Rows.Count > 0 || this.pageMode.Equals(TerraScanCommon.PageModeTypes.New))
                    {
                        this.TransactionFeesTextBox.Text = "5";
                        this.TechFeesTextBox.Text = "5";
                    }
                    else
                    {
                        this.TransactionFeesTextBox.Text = "0";
                        this.TechFeesTextBox.Text = "0";                       
                    }
                }

                this.RateDistrictIDTextBox.Text = string.Empty;                
                this.YearTextBox.Text = string.Empty;
                this.LocalIsComboBox.Text = string.Empty;
                this.LocalIsTextBox.Text = string.Empty;
                this.TotalTaxRateTextBox.Text = string.Empty;
                this.TaxDistrictTextBox.Text = string.Empty;
                this.DescriptionTextBox.Text = string.Empty;
            }

            //// Account Info LockControls            
            this.AccountAdminFeeTextBox.Text = string.Empty;
            this.AccountTransactionFeeTextBox.Text = string.Empty;
            this.AccountTechnologyFeeTextBox.Text = string.Empty;
            this.AccountLocalTaxTextBox.Text = string.Empty;
            this.AccountLocalInterestTextBox.Text = string.Empty;
            this.AccountLocalPenaltyTextBox.Text = string.Empty;
            this.AccountStateTaxTextBox.Text = string.Empty;
            this.AccountStateInterestTextBox.Text = string.Empty;
            this.AccountStatePenaltyTextBox.Text = string.Empty;

            this.AccountAdminFeeTextBox.BackColor = Color.White;
            this.AccountTransactionFeeTextBox.BackColor = Color.White;
            this.AccountTechnologyFeeTextBox.BackColor = Color.White;
            this.AccountLocalTaxTextBox.BackColor = Color.White;
            this.AccountLocalInterestTextBox.BackColor = Color.White;
            this.AccountLocalPenaltyTextBox.BackColor = Color.White;
            this.AccountStateTaxTextBox.BackColor = Color.White;
            this.AccountStateInterestTextBox.BackColor = Color.White;
            this.AccountStatePenaltyTextBox.BackColor = Color.White;

            this.AccountAdminFeePanel.BackColor = Color.White;
            this.AccountTransactionFeePanel.BackColor = Color.White;
            this.AccountTechnologyFeePanel.BackColor = Color.White;
            this.AccountTotalTaxPanel.BackColor = Color.White;
            this.AccountLocalInterestPanel.BackColor = Color.White;
            this.AccountLocalPenaltyPanel.BackColor = Color.White;
            this.AccountStateTaxPanel.BackColor = Color.White;
            this.AccountStateInterestPanel.BackColor = Color.White;
            this.AccountStatePenaltyPanel.BackColor = Color.White;

            ////this.ExciseRateAuditlinkLabel.Text = "tTR_ExciseRate [ExciseRateID]";
            ////this.ExciseRateAuditlinkLabel.Enabled = false;            

            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));
            this.RecordNavigatorSmartPartdeckWorkspace.Enabled = false;

            this.CommentsdeckWorkspace.Enabled = false;
            this.additionalOperationSmartPart.KeyId = -1;
            this.footerSmartPart.KeyId = null;
            this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);
            this.currentRecord = -1;
        }

        /// <summary>
        /// Clears the query by form fields.
        /// </summary>
        private void ClearQueryByFormFields()
        {
            ////Set page status
            this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
            this.FilterTypeId = 0;
            ////set necessary controls property
            this.ClearFilterControl.Enabled = false;
            this.FilteredStatusControl.FilterStatus = false;
            this.UserDefinedWhereCondition = String.Empty;
            this.WhereCondition = string.Empty;
            ////this.ExciseRateAuditlinkLabel.Enabled = true;
            this.SetControlsProperty();
        }

        /// <summary>
        /// Gets the type of the Local.
        /// </summary>
        private void GetLocalType()
        {
            ExciseTaxRateData.LocalTypeDataTable localTypeDataTable = new ExciseTaxRateData.LocalTypeDataTable();
            ////ExciseTaxRateData.LocalTypeRow drCity = localTypeDataTable.NewLocalTypeRow();
            ////ExciseTaxRateData.LocalTypeRow drCounty = localTypeDataTable.NewLocalTypeRow();
            ////drCity.LocalID = "1";
            ////drCity.LocalName = "COUNTY";
            ////drCounty.LocalID = "0";
            ////drCounty.LocalName = "CITY";
            localTypeDataTable.Rows.Add(new object[] { 0, "CITY", });
            localTypeDataTable.Rows.Add(new object[] { 1, "COUNTY" });            
            this.LocalIsComboBox.DataSource = localTypeDataTable.Copy();
            this.LocalIsComboBox.ValueMember = localTypeDataTable.LocalIDColumn.ColumnName;
            this.LocalIsComboBox.DisplayMember = localTypeDataTable.LocalNameColumn.ColumnName;
            this.LocalIsComboBox.SelectedValue = 1;
        }

        /// <summary>
        /// Gets the year.
        /// </summary>
        private void GetYear()
        {
                CommentsData getYearDataSet = new CommentsData();
                getYearDataSet = this.form1101Control.WorkItem.GetConfigDetails("TR_RollYear");
                this.YearTextBox.Text = getYearDataSet.GetCommentsConfigDetails.Rows[0][getYearDataSet.GetCommentsConfigDetails.ConfigurationValueColumn].ToString();
        }

        /// <summary>
        /// Gets the excise rate details. int exciseRateId
        /// </summary>
        /// <param name="exciseId">The excise id.</param>
        private void GetExciseRateDetails(int exciseId)
        {
            this.formLoad = true;
            this.exciseTaxRateDataSet = this.form1101Control.WorkItem.GetExciseTaxRate(exciseId);
            if (this.exciseTaxRateDataSet != null)
            {
                if (this.exciseTaxRateDataSet.Tables.Count > 0)
                {
                    if (this.exciseTaxRateDataSet.GetExciseTaxRate.Rows.Count > 0 && this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows.Count > 0)
                    {
                        //// Fill District Info
                        this.RateDistrictIDTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["ExciseRateID"].ToString();
                        this.additionalOperationSmartPart.KeyId = Convert.ToInt32(this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["ExciseRateID"]);
                        this.footerSmartPart.KeyId = this.additionalOperationSmartPart.KeyId;
                        this.AdminFeesTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["AdminFee"].ToString();
                        this.TransactionFeesTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["TransFee"].ToString();
                        this.TechFeesTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["TechFee"].ToString();
                        this.YearTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["Year"].ToString();
                        this.LocalRateTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["LocalTaxRate"].ToString();
                        this.LocalIsComboBox.SelectedValue = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["IsCounty"].ToString();
                        this.TotalTaxRateTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["TotalTaxRate"].ToString();
                        this.TaxDistrictTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["District"].ToString();
                        this.TaxDistrictTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["DistrictID"].ToString();
                        this.DescriptionTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxRate.Rows[0]["Description"].ToString();
                        ////audit link
                        ////this.ExciseRateAuditlinkLabel.Text = "tTR_ExciseRate [ExciseRateID] " + this.RateDistrictIDTextBox.Text;
                        ////this.ExciseRateAuditlinkLabel.Enabled = true;
                        ////attachment and comments
                        this.CommentsdeckWorkspace.Enabled = true;
                        this.AccountInfoPanel.Enabled = true;
                        this.ToolBoxSmartPartdeckWorkspace.Enabled = true;
                        this.SetAttachmentCommentsCount();

                        //// Account Info
                        this.AccountAdminFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["AdminAcct"].ToString();
                        this.AccountTransactionFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["TransFeeAcct"].ToString();
                        this.AccountTechnologyFeeTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["TechFeeAcct"].ToString();
                        this.AccountLocalTaxTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalTaxAcct"].ToString();
                        this.AccountLocalInterestTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalIntAcct"].ToString();
                        this.AccountLocalPenaltyTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalPenAcct"].ToString();
                        this.AccountStateTaxTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StateTaxAcct"].ToString();
                        this.AccountStateInterestTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StateIntAcct"].ToString();
                        this.AccountStatePenaltyTextBox.Text = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StatePenAcct"].ToString();

                        this.AccountAdminFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["AdminAcctID"].ToString();
                        this.AccountTransactionFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["TransFeeAcctID"].ToString();
                        this.AccountTechnologyFeeTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["TechFeeAcctID"].ToString();
                        this.AccountLocalTaxTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalTaxAcctID"].ToString();
                        this.AccountLocalInterestTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalIntAcctID"].ToString();
                        this.AccountLocalPenaltyTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["LocalPenAcctID"].ToString();
                        this.AccountStateTaxTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StateTaxAcctID"].ToString();
                        this.AccountStateInterestTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StateIntAcctID"].ToString();
                        this.AccountStatePenaltyTextBox.Tag = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0]["StatePenAcctId"].ToString();

                        this.AccountAdminFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.AdminAcctPendingColumn].ToString();
                        this.AccountTransactionFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeePendingColumn].ToString();
                        this.AccountTechnologyFeeTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePendingColumn].ToString();
                        this.AccountLocalTaxTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPendingColumn].ToString();
                        this.AccountLocalInterestTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntPendingColumn].ToString();
                        this.AccountLocalPenaltyTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenPendingColumn].ToString();
                        this.AccountStateTaxTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxPendingColumn].ToString();
                        this.AccountStateInterestTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntPendingColumn].ToString();
                        this.AccountStatePenaltyTextBox.AccessibleName = this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenPendingColumn].ToString();

                        bool tempAdminFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.AdminAcctPendingColumn].ToString());
                        bool tempTransFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TransFeePendingColumn].ToString());
                        bool tempTechFeePending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.TechFeePendingColumn].ToString());
                        bool tempLocalTaxPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalTaxPendingColumn].ToString());
                        bool tempLocalIntPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalIntPendingColumn].ToString());
                        bool tempLocalPenPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.LocalPenPendingColumn].ToString());
                        bool tempStateTaxPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateTaxPendingColumn].ToString());
                        bool tempStateIntPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StateIntPendingColumn].ToString());
                        bool tempStatePenPending = Convert.ToBoolean(this.exciseTaxRateDataSet.GetExciseTaxAccountInfo.Rows[0][exciseTaxRateDataSet.GetExciseTaxAccountInfo.StatePenPendingColumn].ToString());

                        this.AccountInfoBackColor(this.AccountAdminFeePanel, tempAdminFeePending, this.AccountAdminFeeTextBox);
                        this.AccountInfoBackColor(this.AccountTransactionFeePanel, tempTransFeePending, this.AccountTransactionFeeTextBox);
                        this.AccountInfoBackColor(this.AccountTechnologyFeePanel, tempTechFeePending, this.AccountTechnologyFeeTextBox);
                        this.AccountInfoBackColor(this.AccountTotalTaxPanel, tempLocalTaxPending, this.AccountLocalTaxTextBox);
                        this.AccountInfoBackColor(this.AccountLocalInterestPanel, tempLocalIntPending, this.AccountLocalInterestTextBox);
                        this.AccountInfoBackColor(this.AccountLocalPenaltyPanel, tempLocalPenPending, this.AccountLocalPenaltyTextBox);
                        this.AccountInfoBackColor(this.AccountStateTaxPanel, tempStateTaxPending, this.AccountStateTaxTextBox);
                        this.AccountInfoBackColor(this.AccountStateInterestPanel, tempStateIntPending, this.AccountStateInterestTextBox);
                        this.AccountInfoBackColor(this.AccountStatePenaltyPanel, tempStatePenPending, this.AccountStatePenaltyTextBox);
                    }
                }
            }

            this.formLoad = false;
        }

        /// <summary>
        /// Fills the excise rate details.
        /// </summary>
        /// <param name="tempDataTable">The temp data table.</param>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void FillExciseRateDetails(DataTable tempDataTable, int currentRowIndex)
        {
            ////refresh form record set
            this.LoadExciseRateId(tempDataTable);

            if (currentRowIndex > 0)
            {
                this.currentRecord = currentRowIndex;
            }
            else
            {
                this.currentRecord = this.exciseRateIds.ListExciseTaxRate.Rows.Count;
            }
           
            if (this.exciseRateIds.Tables.Count > 0)
            {
                this.totalExciseIdsCount = this.exciseRateIds.Tables[0].Rows.Count;
                if (currentRowIndex > this.totalExciseIdsCount)
                {
                    currentRowIndex = this.totalExciseIdsCount;
                }

                if (this.totalExciseIdsCount > 0)
                {
                    this.LockControls(true);
                    /////Reports control enabled 
                    this.ReportActionSmartPartdeckWorkspace.Enabled = true;
                    this.RecordNavigatorSmartPartdeckWorkspace.Enabled = true;
                    this.SetActiveRecord(this, new DataEventArgs<int>(currentRowIndex));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalExciseIdsCount));
                    this.recordPointerArray[0] = currentRowIndex;
                    this.recordPointerArray[1] = this.totalExciseIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.GetExciseRateDetails(this.RetrieveExciseId(currentRowIndex));
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
                else
                {
                    this.ClearExciseDetails();
                    this.LockControls(false);
                    this.ReportActionSmartPartdeckWorkspace.Enabled = false;
                    ////this.ExciseRateAuditlinkLabel.Enabled = false;
                    this.StatusBarSmartPartDeckWorkspace.Enabled = false;
                    this.AccountInfoPanel.Enabled = false;
                    this.ToolBoxSmartPartdeckWorkspace.Enabled = false;
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }
            }
            else
            {
                this.ClearExciseDetails();
                this.LockControls(false);
                this.ReportActionSmartPartdeckWorkspace.Enabled = false;
                this.AccountInfoPanel.Enabled = false;
                ////this.ExciseRateAuditlinkLabel.Enabled = false;
                this.StatusBarSmartPartDeckWorkspace.Enabled = false;
                this.ToolBoxSmartPartdeckWorkspace.Enabled = false;
                this.NullRecords = true;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
            }
        }

        /// <summary>
        /// Loads the Excise Rate id.
        /// </summary>
        /// <param name="tempDataTable">The temp data table - contains record set.</param>
        private void LoadExciseRateId(DataTable tempDataTable)
        {
            ////clears record set
            this.exciseRateIds.Clear();
            if (tempDataTable != null)
            {
                this.exciseRateIds.ListExciseTaxRate.Merge(tempDataTable);
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

                    ////execute loaded query
                    this.exciseRateIds.ListExciseTaxRate.Merge(this.form1101Control.WorkItem.ExecuteQuery(tempWhereString, null, this.ParentFormId).ListKeyId);

                    if (this.exciseRateIds.ListExciseTaxRate.Rows.Count > 0)
                    {
                        return;
                    }

                    ////records not found - clear filter mode
                    this.ClearQueryByFormFields();
                }

                ////load form record set
                this.exciseRateIds = this.form1101Control.WorkItem.ListExciseTaxRate();
            }
        }

        /// <summary>
        /// retrieves the current exciseId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>integer</returns>
        private int RetrieveExciseId(int index)
        {
            int tempExciseID = 0;
            if (this.exciseRateIds.Tables.Count > 0)
            {
                if (index > 0)
                {
                    if (index <= this.exciseRateIds.ListExciseTaxRate.Rows.Count)
                    {
                        if (this.exciseRateIds.ListExciseTaxRate.Rows[index - 1][0].ToString() != null)
                        {
                            tempExciseID = int.Parse(this.exciseRateIds.ListExciseTaxRate.Rows[index - 1][0].ToString());
                        }
                    }
                }
                else
                {
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.exciseRateIds.ListExciseTaxRate.Rows.Count));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.exciseRateIds.ListExciseTaxRate.Rows.Count));
                    this.recordPointerArray[0] = this.exciseRateIds.ListExciseTaxRate.Rows.Count;
                    this.recordPointerArray[1] = this.exciseRateIds.ListExciseTaxRate.Rows.Count;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    tempExciseID = int.Parse(this.exciseRateIds.ListExciseTaxRate.Rows[this.exciseRateIds.ListExciseTaxRate.Rows.Count - 1][0].ToString()); ////toverify
                }
            }

            return tempExciseID;
        }

        /// <summary>
        /// Check the page Status when the New Reciept is Cancelled
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>boolean Value</returns>
        private bool CheckPageStatus(bool onclose)
        {
            if (!this.pageMode.Equals(TerraScanCommon.PageModeTypes.View))
            {
                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.AccessibleName + "?", ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    bool status = this.SaveExciseRateRecord(onclose);

                    if (status)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    
                    return status;
                }
                else if (dialogResult == DialogResult.No)
                {
                    if (onclose)
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.View;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    }
                    else
                    {
                        this.CancelButton_Click();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
                if (!string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text.Trim()))
                {
                    if (this.form1101Control.WorkItem.DeleteExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), TerraScanCommon.UserId) < 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("DeleteValidation"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.form1101Control.WorkItem.DeleteExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), TerraScanCommon.UserId);
                        }
                    }
                }

                this.exciseRateIds = this.form1101Control.WorkItem.ListExciseTaxRate();
                this.FillExciseRateDetails(null, this.currentRecord);
                ////this.SetButtons(TerraScanCommon.ButtonActionMode.DeleteMode);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                if (this.currentRecord == 0)
                {
                    ////this.DeleteButton.Enabled = false;
                    //// todo: this.DeleteButton.Enabled = false;
                    ////makeButtonEnable[0] = "DELETE";
                    ////makeButtonEnable[1] = "false";
                    ////this.MakeButtonEnable(this, new DataEventArgs<string[]>(makeButtonEnable));
                }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.FillExciseRateDetails(null, this.currentRecord);
                ////if (this.exciseRateIds.ListExciseTaxRate.Rows.Count > 0)
                ////{
                ////    this.ExciseRateAuditlinkLabel.Enabled = true;
                ////}
                ////else
                ////{
                ////    this.ExciseRateAuditlinkLabel.Enabled = false;
                ////}
                ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private int ConvertStringToInt(string textboxValue)
        {
            int outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                int.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }

        /// <summary>
        /// Converts the string to decimal.
        /// </summary>
        /// <param name="textboxValue">The textbox value.</param>
        /// <returns>textboxValue</returns>
        private decimal ConvertStringToDecimal(string textboxValue)
        {
            decimal outValue = 0;
            if (!string.IsNullOrEmpty(textboxValue.Trim()))
            {
                decimal.TryParse(textboxValue.Trim(), out outValue);
            }

            return outValue;
        }

        /// <summary>
        /// Checks the required fields.
        /// </summary>
        /// <returns>the Reqried Fields String</returns>
        private Control CheckRequiredFields()
        {
            Control requiredControll = null;
            if (string.IsNullOrEmpty(this.AdminFeesTextBox.Text.Trim()))
            {
                requiredControll = this.AdminFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.TransactionFeesTextBox.Text.Trim()))
            {
                requiredControll = this.TransactionFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.TechFeesTextBox.Text.Trim()))
            {
                requiredControll = this.TechFeesTextBox;
            }
            else if (string.IsNullOrEmpty(this.YearTextBox.Text.Trim()))
            {
                requiredControll = this.YearTextBox;
            }
            else if (string.IsNullOrEmpty(this.LocalRateTextBox.Text.Trim()))
            {
                requiredControll = this.LocalRateTextBox;
            }
            else if (string.IsNullOrEmpty(this.LocalIsComboBox.SelectedValue.ToString()))
            {
                requiredControll = this.LocalIsComboBox;
            }
            else if (string.IsNullOrEmpty(this.TaxDistrictTextBox.Text.Trim()))
            {
                requiredControll = this.TaxDistrictTextBox;
            }
            else if (string.IsNullOrEmpty(this.AccountAdminFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctAdminFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountTransactionFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTransFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountTechnologyFeeTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTechFeeButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalTaxTextBox.Text.Trim()))
            {
                requiredControll = this.AcctTotalTaxButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalInterestTextBox.Text.Trim()))
            {
                requiredControll = this.AcctLocalIntButton;
            }
            else if (string.IsNullOrEmpty(this.AccountLocalPenaltyTextBox.Text.Trim()))
            {
                requiredControll = this.AcctLocalPenButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStateTaxTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStateTaxButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStateInterestTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStateIntButton;
            }
            else if (string.IsNullOrEmpty(this.AccountStatePenaltyTextBox.Text.Trim()))
            {
                requiredControll = this.AcctStatePenButton;
            }

            return requiredControll;
        }

        /// <summary>
        /// Saves the button_ click.
        /// </summary>
        private void SaveButton_Click()
        {
            this.SaveExciseRateRecord(false);
        }

        /// <summary>
        /// Saves the excise rate record.
        /// </summary>
        /// <param name="onclose">if set to <c>true</c> [onclose].</param>
        /// <returns>bool value true or false </returns>
        private bool SaveExciseRateRecord(bool onclose)
        {
            Control requiredControl;

            requiredControl = this.CheckRequiredFields();

            if (requiredControl != null)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("ExciseRateSaveMissingField"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("RequiredFieldMissingTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                requiredControl.Focus();
                return false;
            }
            else
            {
                    this.Cursor = Cursors.WaitCursor;
                    string validationErrors = string.Empty;
                    bool yearValidationErrors = false;
                    int tempExciseRateID;
                    ExciseTaxRateData saveExciseTaxRateData = new ExciseTaxRateData();
                    ExciseTaxRateData.SaveExciseTaxRateRow dr = saveExciseTaxRateData.SaveExciseTaxRate.NewSaveExciseTaxRateRow();
                    tempExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
                    if (tempExciseRateID > 0)
                    {
                        dr.ExciseRateID = this.ConvertStringToInt(this.RateDistrictIDTextBox.Text);
                    }

                    dr.AdminFee = this.ConvertStringToDecimal(this.AdminFeesTextBox.Text.Replace("%", string.Empty));
                    dr.TransFee = this.ConvertStringToDecimal(this.TransactionFeesTextBox.Text.Replace("$", string.Empty));
                    dr.TechFee = this.ConvertStringToDecimal(this.TechFeesTextBox.Text.Replace("$", string.Empty));
                    int tempYear;
                    int.TryParse(this.YearTextBox.Text, out tempYear);
                    if (tempYear == 0)
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidFieldYear") + " \n", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        dr.Year = tempYear;
                    }
                    
                    dr.LocalTaxRate = this.ConvertStringToDecimal(this.LocalRateTextBox.Text.Replace("%", string.Empty));
                    dr.IsCounty = this.ConvertStringToInt(this.LocalIsComboBox.SelectedValue.ToString());
                    dr.DistrictID = this.ConvertStringToInt(this.TaxDistrictTextBox.Tag.ToString());
                    dr.Description = this.DescriptionTextBox.Text;
                    dr.AdminAcct = this.ConvertStringToInt(this.AccountAdminFeeTextBox.Tag.ToString());
                    dr.TransFeeAcct = this.ConvertStringToInt(this.AccountTransactionFeeTextBox.Tag.ToString());
                    dr.TechFeeAcct = this.ConvertStringToInt(this.AccountTechnologyFeeTextBox.Tag.ToString());
                    dr.LocalTaxAcct = this.ConvertStringToInt(this.AccountLocalTaxTextBox.Tag.ToString());
                    dr.LocalIntAcct = this.ConvertStringToInt(this.AccountLocalInterestTextBox.Tag.ToString());
                    dr.LocalPenAcct = this.ConvertStringToInt(this.AccountLocalPenaltyTextBox.Tag.ToString());
                    dr.StateTaxAcct = this.ConvertStringToInt(this.AccountStateTaxTextBox.Tag.ToString());
                    dr.StateIntAcct = this.ConvertStringToInt(this.AccountStateInterestTextBox.Tag.ToString());
                    dr.StatePenAcct = this.ConvertStringToInt(this.AccountStatePenaltyTextBox.Tag.ToString());

                    saveExciseTaxRateData.SaveExciseTaxRate.Rows.Add(dr);
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(saveExciseTaxRateData.SaveExciseTaxRate.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    if (string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text.Trim()))
                    {
                        this.form1101Control.WorkItem.SaveExciseTaxRate(0, tempDataSet.GetXml(), TerraScanCommon.UserId);
                    }
                    else
                    {
                        this.form1101Control.WorkItem.SaveExciseTaxRate(Convert.ToInt32(this.RateDistrictIDTextBox.Text), tempDataSet.GetXml(), TerraScanCommon.UserId);
                    }

                    this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                    this.pageMode = TerraScanCommon.PageModeTypes.View;

                    if (onclose)
                    {
                        return true;
                    }

                    if (this.currentRecord > 0)
                    {
                        this.FillExciseRateDetails(null, this.currentRecord);
                    }
                    else
                    {
                        this.FillExciseRateDetails(null, -1);
                    }

                    return true;
                    this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                if (this.pageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    ////Set page status
                    this.PageStatus = TerraScanCommon.PageStatus.NormalMode;
                    this.FilterTypeId = 0;
                    ////set necessary controls property
                    this.ClearFilterControl.Enabled = false;
                    this.FilteredStatusControl.FilterStatus = false;
                    this.UserDefinedWhereCondition = String.Empty;
                    this.WhereCondition = string.Empty;
                }

                this.LockControls(true);
                this.AccountInfoPanel.Enabled = true;
                this.ToolBoxSmartPartdeckWorkspace.Enabled = true;
                this.StatusBarSmartPartDeckWorkspace.Enabled = true;
                ////this.StatusBarSmartPartDeckWorkspace.Enabled = false;
                this.ClearExciseDetails();
                this.GetYear();
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.AdminFeesTextBox.Focus();
        }

        #region Load All SmartParts

        /// <summary>
        /// Method to Load All SmartParts in UI WorkSpaces
        /// </summary>
        private void LoadWorkSpaces()
        {
            // Load ExciseTaxActionButtons SmartPart into ExciseTaxActionButtonsdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart));
            }
            else
            {
                this.TestOperationSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart));
            }

            this.operationSmartPart = (OperationSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.OperationSmartPart];

            // Load RecordNavigatorSmartPart into RecordNavigatorSmartPartdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            // Load ToolBoxSmartPart into ToolBoxSmartPartdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.ToolBoxSmartPart))
            {
                this.ToolBoxSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.ToolBoxSmartPart));
            }
            else
            {
                this.ToolBoxSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<ToolBoxSmartPart>(SmartPartNames.ToolBoxSmartPart));
            }

            // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            // To Load ReportActionSmartPart into ReportActionSmartPartdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.ReportActionSmartPart))
            {
                this.ReportActionSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.ReportActionSmartPart));
            }
            else
            {
                this.ReportActionSmartPartdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<ReportActionSmartPart>(SmartPartNames.ReportActionSmartPart));
            }

            // To Load StatusBarSmartPart into StatusBarSmartPartdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
            {
                this.StatusBarSmartPartDeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart));
            }
            else
            {
                this.StatusBarSmartPartDeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart));
            }

            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentsdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentsdeckWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            // To Load FooterSmartPart into FooterWorkspace
            if (this.form1101Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.form1101Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.form1101Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.form1101Control.WorkItem;
            this.footerSmartPart.FormId = "1101";
            this.footerSmartPart.AuditLinkText = "tTR_ExciseRate [ExciseRateID] ";
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;
            ////this.footerSmartPart.CurrntFormId = Convert.ToInt16(this.Tag);

            string[] formLabelInfo = new string[2];
            formLabelInfo[0] = "Excise Rates"; ////Properties.Resources.FormName;
            formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(formLabelInfo));

            ToolBoxSmartPart toolBoxSmartPart = (ToolBoxSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.ToolBoxSmartPart];
            this.ClearFilterControl = this.SearchControlWithKey(toolBoxSmartPart, "ClearFilterButton");
            this.ClearFilterControl.Enabled = false;
            this.SearchControlWithKey(toolBoxSmartPart, "SnapshotUtilityButton").Visible = false;
            this.SearchControlWithKey(toolBoxSmartPart, "CalculatorButton").Visible = false;

            StatusBarSmartPart statusBarSmartPart = (StatusBarSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.StatusBarSmartPart];
            this.FilteredStatusControl = (TerraScanButton)this.SearchControlWithKey(statusBarSmartPart, "FilteredButton");
            this.SearchControlWithKey(statusBarSmartPart, "DelinquentButton").Visible = false;
            this.SearchControlWithKey(statusBarSmartPart, "AutoPrintOnButton").Visible = false;

            ReportActionSmartPart reportActionSmartPart = (ReportActionSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.ReportActionSmartPart];
            this.SearchControlWithKey(reportActionSmartPart, "DetailsButton").Visible = false;

            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1101Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the TaxDistrictButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SetEditRecord();
                int districtId = 0;
                object[] optionalParameter = new object[] { this.YearTextBox.Text.Trim(),this.ParentFormId };
                Form districtSelectionForm = this.form1101Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1512, optionalParameter, this.form1101Control.WorkItem);
                if (districtSelectionForm != null)
                {
                    if (districtSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        int.TryParse(TerraScanCommon.GetValue(districtSelectionForm, "DistrictId").ToString(), out districtId);
                        ExciseTaxRateData districtNameDataSet = new ExciseTaxRateData();
                        districtNameDataSet = this.form1101Control.WorkItem.GetDistrictName(districtId);

                        if (districtNameDataSet.GetDistrictName.Rows.Count > 0)
                        {
                            this.TaxDistrictTextBox.Text = districtNameDataSet.GetDistrictName.Rows[0][districtNameDataSet.GetDistrictName.DistrictNameColumn].ToString();
                            this.TaxDistrictTextBox.Tag = districtId;
                        }
                        else
                        {
                            this.TaxDistrictTextBox.Text = string.Empty;
                            this.TaxDistrictTextBox.Tag = string.Empty;
                        }
                    }
                }
            }
            catch (SoapException soapEx)
            {
                ExceptionManager.ManageException(soapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DistrictCopyButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DistrictCopyButton_Click(object sender, EventArgs e)
        {
            try
            {
            object[] optionalParameter = new object[] { this.RateDistrictIDTextBox.Text };
            Form createExciseRateDistrictForm = new Form();
            createExciseRateDistrictForm = this.form1101Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1104, optionalParameter, this.form1101Control.WorkItem);
            if (createExciseRateDistrictForm != null)
            {
                createExciseRateDistrictForm.ShowDialog();
            }
            }
            catch (SoapException soapEx)
            {
                ExceptionManager.ManageException(soapEx, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the AccountManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void AccountManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11007);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            ////Form accountManagementForm = new Form();
            ////accountManagementForm = this.form1030control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(11007, null, this.form1101Control.WorkItem);
            ////if (accountManagementForm != null)
            ////{
            ////    accountManagementForm.ShowDialog();
            ////}
        }

        /// <summary>
        /// Handles the Click event of the AccountButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AccountButton_Click(object sender, EventArgs e)
        {
            try
            {
                Button tempButton = new Button();
                tempButton = (Button)sender;

                this.SetEditRecord();
                if (tempButton != null)
                {
                    this.GetAccountDetails(tempButton);
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Edits the record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditRecord(object sender, EventArgs e)
        {
            try
            {
                if (!this.formLoad)
                {
                    this.SetEditRecord();
                }
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.View && (this.PageStatus.Equals(TerraScanCommon.PageStatus.NormalMode) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode)))
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            }
        }

        /// <summary>
        /// Gets the account details.
        /// </summary>
        /// <param name="buttonControl">Name of the control.</param>
        private void GetAccountDetails(Button buttonControl)
        {
                bool tempAccountStatus;
                int rollYearValue = 0;
                this.accountId = 0;
                this.selectedAccountName = null;
                int.TryParse(this.YearTextBox.Text.Trim(), out rollYearValue);
                object[] optionalParameter = new object[] { rollYearValue };
                Form accountSelectionForm = this.form1101Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1345, optionalParameter, this.form1101Control.WorkItem);
                if (accountSelectionForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                {
                    if (accountSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        int.TryParse(TerraScanCommon.GetValue(accountSelectionForm, "AccountId").ToString(), out this.accountId);
                        ExciseTaxRateData accountNameDataSet = new ExciseTaxRateData();
                        accountNameDataSet = this.form1101Control.WorkItem.GetAccountName(this.accountId);

                        if (accountNameDataSet.GetAccountName.Rows.Count > 0)
                        {
                            tempAccountStatus = Convert.ToBoolean(accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctPendingColumn].ToString());
                            this.selectedAccountName = accountNameDataSet.GetAccountName.Rows[0][accountNameDataSet.GetAccountName.AdminAcctColumn].ToString();
                            if (!string.IsNullOrEmpty(buttonControl.Tag.ToString()))
                            {
                                Control tempTextBoxControl = this.SearchControlWithKey(this.AccountInfoPanel, buttonControl.Tag.ToString());
                                tempTextBoxControl.Text = this.selectedAccountName;
                                tempTextBoxControl.Tag = this.accountId;
                                this.AccountInfoBackColor((Panel)buttonControl.Parent, tempAccountStatus, tempTextBoxControl);
                            }
                        }
                    }
                }
        }

        /// <summary>
        /// Accounts the color of the info back.
        /// </summary>
        /// <param name="panelControl">The panel control.</param>
        /// <param name="tempAccountStatus">if set to <c>true</c> [temp account status].</param>
        /// <param name="tempTextBoxControl">The temp text box control.</param>
        private void AccountInfoBackColor(Panel panelControl, bool tempAccountStatus, Control tempTextBoxControl)
        {
            if (tempAccountStatus == false)
            {
                panelControl.BackColor = Color.White;
                tempTextBoxControl.BackColor = Color.White;
            }
            else
            {
                tempTextBoxControl.BackColor = Color.FromArgb(187, 222, 173);
                panelControl.BackColor = Color.FromArgb(187, 222, 173);
            }
        }

        /// <summary>
        /// set focus to the next/previous input field
        /// </summary>
        /// <param name="sourceControl">The source control.</param>
        /// <param name="key">The key.</param>
        /// <returns>sourceControl and key</returns>
        private Control SearchControlWithKey(Control sourceControl, string key)
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
                        requiredControl = this.SearchControlWithKey(sampControl, key);
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
                this.DistrictInfoPanel.BackColor = Color.White;

                this.LocalIsTextBox.Visible = false;
                this.LocalIsComboBox.Visible = true;
                this.TaxDistrictButton.Visible = true;
                this.AdminFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.TransactionFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.TechFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.LocalRateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.TotalTaxRateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Decimal;
                this.YearTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Numeric;
                this.RateDistrictIDTextBox.ForeColor = Color.Gray;
                this.TotalTaxRateTextBox.ForeColor = Color.Gray;

                this.AdminFeesTextBox.MaxLength = 8;
                this.TransactionFeesTextBox.MaxLength = 20;
                this.TechFeesTextBox.MaxLength = 20;
                this.LocalRateTextBox.MaxLength = 8;
                this.YearTextBox.MaxLength = 4;
                
                this.AccountManagementLinkLabel.Enabled = true;
                this.AccountInfoPanel.Enabled = true;                 
                this.TestOperationSmartPartdeckWorkspace.Enabled = true;                              
                this.ReportActionSmartPartdeckWorkspace.Enabled = true;
                this.StatusBarSmartPartDeckWorkspace.Enabled = true;               
                this.RecordNavigatorSmartPartdeckWorkspace.Enabled = true;
                this.CommentsdeckWorkspace.Enabled = true;
                this.DistrictCopyButton.Enabled = true;
            }
            else if (this.PageStatus.Equals(TerraScanCommon.PageStatus.QueryByForm) || this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredQueryByForm))
            {
                ////set Query control property
                SharedFunctions.SetQueryRelatedProperty(this.queryControlArray, true, this.userDefinedWhereCondition, Color.FromArgb(204, 255, 204));
                this.DistrictInfoPanel.BackColor = Color.FromArgb(204, 255, 204);

                this.LocalIsTextBox.Visible = true;
                this.LocalIsComboBox.Visible = false;
                this.TaxDistrictButton.Visible = false;
                this.AdminFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.TransactionFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.TechFeesTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.LocalRateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.TotalTaxRateTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.YearTextBox.ValidateType = TerraScanTextBox.ControlvalidationType.Text;
                this.RateDistrictIDTextBox.ForeColor = Color.Black;
                this.TotalTaxRateTextBox.ForeColor = Color.Black;

                this.AdminFeesTextBox.MaxLength = 32767;
                this.TransactionFeesTextBox.MaxLength = 32767;
                this.TechFeesTextBox.MaxLength = 32767;
                this.LocalRateTextBox.MaxLength = 32767;
                this.YearTextBox.MaxLength = 32767;

                this.AccountManagementLinkLabel.Enabled = false;
                this.AccountInfoPanel.Enabled = false;
                this.TestOperationSmartPartdeckWorkspace.Enabled = false;
                this.ReportActionSmartPartdeckWorkspace.Enabled = false;
                this.StatusBarSmartPartDeckWorkspace.Enabled = false;                
                this.RecordNavigatorSmartPartdeckWorkspace.Enabled = false;
                this.CommentsdeckWorkspace.Enabled = false;
                this.DistrictCopyButton.Enabled = false;
            }
        }    

        /// <summary>
        /// Sets the tagname with querying field name
        /// </summary>
        private void SetQueryingFieldName()
        {
            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            this.RateDistrictIDTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.RATEDISTRICTIDColumn.ColumnName;
            this.AdminFeesTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.ADMINFEESColumn.ColumnName;
            this.TransactionFeesTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.TRANSACTIONFEESColumn.ColumnName;
            this.TechFeesTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.TECHFEESColumn.ColumnName;
            this.YearTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.YEARColumn.ColumnName;
            this.LocalRateTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.LOCALRATEColumn.ColumnName;
            this.LocalIsTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.LOCALISColumn.ColumnName;
            this.TotalTaxRateTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.TOTALTAXRATEColumn.ColumnName;
            this.TaxDistrictTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.TAXDISTRICTColumn.ColumnName;
            this.DescriptionTextBox.QueryingFileldName = queryingFields.ExciseTaxRateQueryingFields.DESCRIPTIONColumn.ColumnName;
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
                         
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                if (!string.IsNullOrEmpty(this.RateDistrictIDTextBox.Text))
                {
                    additionalOperationCountEntity.AttachmentCount = this.form1101Control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.RateDistrictIDTextBox.Text), TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1101Control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.RateDistrictIDTextBox.Text), TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        #endregion

        #region Query

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
                e.Data.Focus();               

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
                    this.FilteredStatusControl.FilterStatus = false;
                    this.SetControlsProperty();
                    this.ClearExciseDetails();
                    this.RateDistrictIDTextBox.Focus();
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
                
                //// FillDistrictRateDetails function is used to fill the Statement details in excise tax rates                         
                this.FillExciseRateDetails(null, -1);                
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
            string returnValue = String.Empty;
            StringBuilder whereClause = new StringBuilder(String.Empty);
            ////used for requery
            StringBuilder userFriendlyWhereCondition = new StringBuilder(String.Empty);            
            ////true when the query execution succeeded
            bool querySucceeded = false;
            ////true when query is invalid
            bool invalidQuery = false;

            F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();

            ////getformatted sql condition
            SharedFunctions.GetFormattedSqlCondition(this.queryControlArray, queryingFields.ExciseTaxRateQueryingFields, whereClause, userFriendlyWhereCondition, ref invalidQuery);

            if (!invalidQuery && whereClause.Length == 0)
            {
                MessageBox.Show(SharedFunctions.GetResourceString("NoRecord") + "\n" + SharedFunctions.GetResourceString("QueryEntryMessage"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.RateDistrictIDTextBox.Focus();
                this.Cursor = Cursors.Default;
                return;
            }

            QueryData queryData = new QueryData();

            try
            {
                ////checks for any criteria
                if (whereClause.Length > 0 && !invalidQuery)
                {
                    queryData = this.form1101Control.WorkItem.ExecuteQuery(whereClause.ToString(), null, int.Parse(this.Tag.ToString()));

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
                        MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], " records were searched \n", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " Matches the filter criteria"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.WhereCondition = whereClause.ToString();
                    this.UserDefinedWhereCondition = userFriendlyWhereCondition.ToString();
                    this.ClearFilterControl.Enabled = true;

                    ////Set page status
                    this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                    this.FilteredStatusControl.FilterStatus = true;

                    this.SetControlsProperty();
                    //// FillStatementDetails function is used to fill the Statement details in ExciseTaxStatement                                         
                    this.FillExciseRateDetails(queryData.ListKeyId, queryData.ListKeyId.Rows.Count);                    
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

                    this.RateDistrictIDTextBox.Focus();
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
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void QueryUtilityFunction(object sender, DataEventArgs<Button> e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////get loaded status bar smartpart 
                ToolBoxSmartPart toolBoxSmartPart = (ToolBoxSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.ToolBoxSmartPart];
                ////required parameter for calling form
                toolBoxSmartPart.ToolBoxEntity.CurrentFormId = this.ParentFormId;
                toolBoxSmartPart.ToolBoxEntity.ParentWorkItem = this.form1101Control.WorkItem;
                if (this.PageStatus.Equals(TerraScanCommon.PageStatus.FilteredMode))
                {
                    toolBoxSmartPart.ToolBoxEntity.WhereCondition = this.WhereCondition;
                    toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition = this.UserDefinedWhereCondition;
                }
                else
                {
                    toolBoxSmartPart.ToolBoxEntity.WhereCondition = string.Empty;
                    toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition = string.Empty;
                }

                ////which shows QueryUtility form and set required parameter
                toolBoxSmartPart.ShowQueryUtilityForm();

                this.Cursor = Cursors.Default;               
               ////suceessful queryutility load
                if (toolBoxSmartPart.ToolBoxEntity.CalledFormStatus)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ////specific for filtered records
                    this.FilterTypeId = toolBoxSmartPart.ToolBoxEntity.KeyId;

                    // Load tempDataSet will contain query and where condition in Table[0] and all filtered statementIds in Tables[1]
                    QueryData queryData = this.form1101Control.WorkItem.GetQueryResult(this.FilterTypeId, null);

                    if (queryData.ListKeyId.Rows.Count > 0 && queryData.GetQueryResult.Rows.Count > 0)
                    {
                        ////specific to filter                           
                        this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;

                        this.UserDefinedWhereCondition = toolBoxSmartPart.ToolBoxEntity.UserDefinedWhereCondition;
                        this.WhereCondition = toolBoxSmartPart.ToolBoxEntity.WhereCondition;                        
                        //// FilldistrictrateDetails function is used to fill the rate details in Excise Tax rates 
                        this.FillExciseRateDetails(queryData.ListKeyId, queryData.ListKeyId.Rows.Count);
                        this.FilteredStatusControl.FilterStatus = true;
                        this.ClearFilterControl.Enabled = true;
                        this.SetControlsProperty();
                    }
                    else
                    {
                        ////specific for filtered records                        
                        this.FilterTypeId = 0;
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
                Form querystringForm = new Form();

                F9001QueryingFieldsData queryingFields = new F9001QueryingFieldsData();
                ////get loaded status bar smartpart 
                StatusBarSmartPart statusBarSmartPart = (StatusBarSmartPart)this.form1101Control.WorkItem.SmartParts[SmartPartNames.StatusBarSmartPart];
                statusBarSmartPart.StatusBarEntity.OptionalInputParameter = new object[] { this.WhereCondition, "N/A", "N/A", queryingFields.ExciseTaxRateQueryingFields };
                statusBarSmartPart.StatusBarEntity.ParentWorkItem = this.form1101Control.WorkItem;
                this.Cursor = Cursors.Default;

                while (true)
                {
                    try
                    {
                        ////which show requery form
                        statusBarSmartPart.ShowRequeryForm();
                        if (statusBarSmartPart.StatusBarEntity.CalledFormStatus)
                        {
                            QueryData queryData = new QueryData();   
                            queryData = this.form1101Control.WorkItem.ExecuteQuery(statusBarSmartPart.StatusBarEntity.WhereCondition, null, this.ParentFormId);

                            if (queryData.ListKeyId.Rows.Count > 0)
                            {
                                if (queryData.SearchedCountResult.Rows.Count > 0)
                                {
                                    MessageBox.Show(String.Concat(queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.RecordSearchedColumn], " records were searched \n", queryData.SearchedCountResult.Rows[0][queryData.SearchedCountResult.MatchesFoundColumn], " Matches the filter criteria"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("QueryByForm")), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                this.WhereCondition = statusBarSmartPart.StatusBarEntity.WhereCondition;
                                this.UserDefinedWhereCondition = statusBarSmartPart.StatusBarEntity.UserDefinedWhereCondition;
                                this.ClearFilterControl.Enabled = true;

                                ////Set page status
                                this.PageStatus = TerraScanCommon.PageStatus.FilteredMode;
                                this.FilteredStatusControl.FilterStatus = true;

                                this.SetControlsProperty();
                                //// FillStatementDetails function is used to fill the Statement details in ExciseTaxstatement                                
                                this.FillExciseRateDetails(queryData.ListKeyId, queryData.ListKeyId.Rows.Count);                                
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

        #endregion       

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LocalIsComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LocalIsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
            this.SetEditRecord();
            }
            catch (Exception Ex)
            {
                ExceptionManager.ManageException(Ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the ExciseRateAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ExciseRateAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
        }

        /// <summary>
        /// Handles the Enter event of the StatusBarSmartPartDeckWorkspace control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StatusBarSmartPartDeckWorkspace_Enter(object sender, EventArgs e)
        {
            ////this.ExciseRateAuditlinkLabel.Focus();
        }

        /// <summary>
        /// Handles the Enter event of the TestOperationSmartPartdeckWorkspace control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TestOperationSmartPartdeckWorkspace_Enter(object sender, EventArgs e)
        {
        }
    }
}
