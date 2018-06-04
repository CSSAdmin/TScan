//--------------------------------------------------------------------------------------------
// <copyright file="F1031.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 06        JYOTHI              Created
//*********************************************************************************/
namespace D1030
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
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;
    using System.Web.Services.Protocols;
    using System.Collections;
    using TerraScan.Common.Reports;

    /// <summary>
    /// F1031 Special District Assessment class file
    /// </summary>
    [SmartPart]
    public partial class F1031 : BaseSmartPart
    {
        #region Member Variables

        /// <summary>
        /// Special District Assessment form1031Control Controller
        /// </summary>
        private F1031Controller form1031Control;

        /// <summary>
        /// formLabel Info
        /// </summary>
        private string[] formLabelInfo;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// current Record
        /// </summary>
        private int currentRecord;

        /// <summary>
        /// recordPointerArray variable
        /// </summary>
        private int[] recordPointerArray = new int[2];

        /// <summary>
        /// pageLoadStatus Local variable.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// acresEmpty Local variable.
        /// </summary>
        private bool acresEmpty;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScan.Common.TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// ratesDetailsDataTable variable is used to get the details of rate listing Details.
        /// </summary>
        private F1031SpecialDistrictAssessmentData.ListDistrictAssessmentRatesDataTable ratesDetailsDataTable = new F1031SpecialDistrictAssessmentData.ListDistrictAssessmentRatesDataTable();

        /// <summary>
        /// districtAssessmentIds variable is used to store list of districtAssessmentIds for Special District Assessment. 
        /// </summary>       
        private F1031SpecialDistrictAssessmentData districtAssessmentIds = new F1031SpecialDistrictAssessmentData();

        /// <summary>
        /// totalDistrictAssessmentIdsCount variable is used to find total number of districtAssessmentIds for Special District Assessment. 
        /// </summary>
        private int totalDistrictAssessmentIdsCount;

        /// <summary>
        /// used to maintain loadstate
        /// </summary>
        private bool formLoad = true;

        /// <summary>
        /// used to maintain ratesGrid edit status
        /// </summary>
        private bool ratesGridEdited = false;

        /// <summary>
        /// propertyInfoDataSet variable is used to get the details of District Assessment Details.
        /// </summary>
        private F1031SpecialDistrictAssessmentData propertyInfoDataSet = new F1031SpecialDistrictAssessmentData();

        /// <summary>
        /// currentDistrictAssessmentStatementId variable is used to store District Assessment Statement id. 
        /// </summary>       
        private int currentStatementId = 0;

        /// <summary>
        ///  variable is used to store District Assessment Statement id. 
        /// </summary>       
        private bool ispaid;

        /// <summary>
        ///  variable is used to store Special District id. 
        /// </summary>
        private int sadistrictId = 0;

        /// <summary>
        ///  specialDistrictDetailsDataSet is used to store Special District details. 
        /// </summary>
        private F1031SpecialDistrictAssessmentData specialDistrictDetailsDataSet = new F1031SpecialDistrictAssessmentData();

        /// <summary>
        /// additionalOperationSmartPart
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart = new AdditionalOperationSmartPart();

        /// <summary>
        /// statusBarSmartPart
        /// </summary>
        private StatusBarSmartPart statusBarSmartPart = new StatusBarSmartPart();

        /// <summary>
        /// amountValue
        /// </summary>
        private decimal amountValue;

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1031"/> class.
        /// </summary>
        public F1031()
        {
            InitializeComponent();
            this.formLabelInfo = new string[2];
            this.CustomizeRatesListingGridView();
            this.PropertyInfoPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PropertyInfoPictureBox.Height, this.PropertyInfoPictureBox.Width, "Property Info", 28, 81, 128);
            this.RatesListingSecIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.RatesListingSecIndicatorPictureBox.Height, this.RatesListingSecIndicatorPictureBox.Width, "Rates Listing", 0, 51, 0);
        }
        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// event publication for SetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_SetCancelButton, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<Button>> SetCancelButton;

        /// <summary>
        /// event publication for GetCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton; 

        /// <summary>
        /// event publication for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// event publication for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// event publication for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event Publication for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F1031 control.
        /// </summary>
        /// <value>The F1031 control.</value>
        [CreateNew]
        public F1031Controller Form1031Control
        {
            get { return this.form1031Control as F1031Controller; }
            set { this.form1031Control = value; }
        }

        /// <summary>
        /// Gets or sets the current statement id.
        /// </summary>
        /// <value>The current statement id.</value>
        public int CurrentStatementId
        {
            get
            {
                return this.currentStatementId;
            }

            set
            {
                this.currentStatementId = value;
                this.additionalOperationSmartPart.KeyId = this.currentStatementId;
            }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private TerraScanCommon.PageModeTypes PageMode
        {
            get { return this.pageMode; }
            set { this.pageMode = value; }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.FillDistrictAssessmentDetails(recordNavigationEntity.RecordIndex);
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
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            if (e.Data == this.GetType().Name)
            {
                this.form1031Control.WorkItem.State["FormStatus"] = this.CheckPageStatus(false);
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

        #endregion

        #region Form Load

        /// <summary>
        /// Handles the Load event of the F1031 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1031_Load(object sender, EventArgs e)
        {
            this.LoadWorkSpaces();
            if (!this.PermissionEdit)
            {
                this.DetailsButton.Enabled = false;
                this.additionalOperationSmartPart.Enabled = false;
            }
            else
            {
                this.DetailsButton.Enabled = true;
                this.additionalOperationSmartPart.Enabled = true;
            }

            this.FillDistrictAssessmentDetails(0);
            this.LockTextBoxControls(this.ispaid);
            ////this.EnableButtonControls(!this.ispaid);
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }

        #endregion

        #region Private Methods

        #region LoadWorkSpaces
        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            try
            {
                // Load RecordNavigatorSmartPart into RecordNavigatorSmartPartdeckWorkspace
                if (this.form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
                {
                    this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1031Control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
                }
                else
                {
                    this.RecordNavigatorSmartPartdeckWorkspace.Show(this.form1031Control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
                }

                ////Load StatusBarSmartPart to StatusBarSmartPartdeckWorkspace
                if (this.Form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.StatusBarSmartPart))
                {
                    this.statusBarSmartPart = (StatusBarSmartPart)this.Form1031Control.WorkItem.SmartParts.Get(SmartPartNames.StatusBarSmartPart);
                }
                else
                {
                    this.statusBarSmartPart = this.Form1031Control.WorkItem.SmartParts.AddNew<StatusBarSmartPart>(SmartPartNames.StatusBarSmartPart);
                }

                this.StatusBarDeckWorkspace.Show(this.statusBarSmartPart);
                this.statusBarSmartPart.VisibleDelinquentButton = false;
                this.statusBarSmartPart.VisibleAutoPrintButton = false;

                // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace

                if (this.Form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
                {
                    this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.Form1031Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
                }
                else
                {
                    this.additionalOperationSmartPart = this.Form1031Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart);
                }

                this.CommentsdeckWorkspace.Show(this.additionalOperationSmartPart);

                if (this.form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form1031Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form1031Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                if (this.form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
                {
                    this.operationSmartPart = (OperationSmartPart)this.form1031Control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                    this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
                }
                else
                {
                    this.operationSmartPart = (OperationSmartPart)this.form1031Control.WorkItem.SmartParts.AddNew<OperationSmartPart>(this.Name + SmartPartNames.OperationSmartPart);
                    this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
                }

                ////set required variable - attachment and comment
                this.additionalOperationSmartPart.ParentWorkItem = this.Form1031Control.WorkItem;
                this.additionalOperationSmartPart.ParentFormId = 1020;
                this.additionalOperationSmartPart.CurrntFormId = 1020;

                this.formLabelInfo[0] = SharedFunctions.GetResourceString("F1031FormHeader");
                this.formLabelInfo[1] = string.Empty;
                ////sets form header
                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

                // To Load FooterSmartPart into FooterWorkspace
                if (this.form1031Control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
                {
                    this.FooterWorkspace.Show(this.form1031Control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
                }
                else
                {
                    this.FooterWorkspace.Show(this.form1031Control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
                }

                this.footerSmartPart = (FooterSmartPart)this.form1031Control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
                this.footerSmartPart.ParentWorkItem = this.form1031Control.WorkItem;
                this.footerSmartPart.FormId = "1031";
                this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("F1031AuditLink");
                this.footerSmartPart.VisibleHelpButton = false;
                this.footerSmartPart.VisibleHelpLinkButton = true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region CustomizeRatesListingGridView
        /// <summary>
        /// Customizes the materials grid view.
        /// </summary>
        private void CustomizeRatesListingGridView()
        {
            this.RatesListingGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.RatesListingGridView.Columns;
            columns["RateDescription"].DataPropertyName = this.ratesDetailsDataTable.RateDescriptionColumn.ColumnName;
            columns["RateType"].DataPropertyName = this.ratesDetailsDataTable.RateTypeColumn.ColumnName;
            columns["Minimum"].DataPropertyName = this.ratesDetailsDataTable.MinimumColumn.ColumnName;
            columns["Amount"].DataPropertyName = this.ratesDetailsDataTable.AmountColumn.ColumnName;
            columns["Acres"].DataPropertyName = this.ratesDetailsDataTable.AcresColumn.ColumnName;
            columns["Total"].DataPropertyName = this.ratesDetailsDataTable.TotalColumn.ColumnName;
            columns["RateAcresID"].DataPropertyName = this.ratesDetailsDataTable.RateAcresIDColumn.ColumnName;
            columns["SARateItemID"].DataPropertyName = this.ratesDetailsDataTable.SARateItemIDColumn.ColumnName;

            columns["RateDescription"].DisplayIndex = 0;
            columns["RateType"].DisplayIndex = 1;
            columns["Minimum"].DisplayIndex = 2;
            columns["Amount"].DisplayIndex = 3;
            columns["Acres"].DisplayIndex = 4;
            columns["Total"].DisplayIndex = 5;
            columns["RateAcresID"].DisplayIndex = 6;
            columns["SARateItemID"].DisplayIndex = 7;

            this.RatesListingGridView.DataSource = this.ratesDetailsDataTable;
        }
        #endregion CustomizeRatesListingGridView

        #region GetDistrictAssessmentDetails
        /// <summary>
        /// Gets the District Assessment details. int statementId
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        private void GetDistrictAssessmentDetails(int statementId)
        {
            this.formLoad = true;
            this.propertyInfoDataSet = this.form1031Control.WorkItem.F1031_ListDistrictAssessmentDetails(statementId);

            if (this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows.Count > 0 && this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > 0)
            {
                //// Fill Property Info
                this.currentStatementId = Convert.ToInt32(this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.StatementIDColumn.ColumnName].ToString());
                this.ParcelNumberTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.ParcelNumberColumn.ColumnName].ToString();
                this.ParcelIDLinkLabel.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.ParcelIDColumn.ColumnName].ToString();
                this.ispaid = Convert.ToBoolean(this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.IsPaidColumn.ColumnName].ToString());
                this.TypeTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.PostNameColumn.ColumnName].ToString();
                this.TypeTextBox.Tag = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.PostTypeIDColumn.ColumnName].ToString();
                if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                {
                    this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber");
                }
                else
                {
                    this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber");
                }

                this.IrrigableAcresTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.IrrgAcresColumn.ColumnName].ToString();
                this.RPAcresCountTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.AcresColumn.ColumnName].ToString();
                this.TurnoutTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.TurnoutsColumn.ColumnName].ToString();
                this.Address1TextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.Address1Column.ColumnName].ToString() + Environment.NewLine + this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.Address2Column.ColumnName].ToString() + Environment.NewLine + this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.CityColumn.ColumnName].ToString();
                this.OwnerNameLinkLabel.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.Owner_NameColumn.ColumnName].ToString();
                this.MinDistFeeTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.MinimumDistrictFeeColumn.ColumnName].ToString();
                this.RollYearTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.RollYearColumn.ColumnName].ToString();
                this.SpecialDistrictLinkLabel.Tag = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.SADistrictIDColumn.ColumnName].ToString();
                this.SpecialDistrictLinkLabel.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.DistrictNameColumn.ColumnName].ToString();
                this.StatementNumberLinkLabel.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.StatementNumberColumn.ColumnName].ToString();
                this.LoanTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.LoanNumberColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Tag = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.MortgageIDColumn.ColumnName].ToString();
                this.MortgageCoTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.MortgageNameColumn.ColumnName].ToString();
                this.SitusTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.SitusColumn.ColumnName].ToString();
                this.MapTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.MapNumberColumn.ColumnName].ToString();
                this.LegalTextBox.Text = this.propertyInfoDataSet.ListDistrictAssessmentProperty.Rows[0][this.propertyInfoDataSet.ListDistrictAssessmentProperty.LegalColumn.ColumnName].ToString();
                ////audit link
                ////this.SpecialDistrictAuditlinkLabel.Text = SharedFunctions.GetResourceString("F1031AuditLink") + this.currentStatementId;
                ////this.SpecialDistrictAuditlinkLabel.Enabled = true;
                this.footerSmartPart.KeyId = this.currentStatementId;

                ////VscrollBar is enabled or disabled based on NumRowsVisible in GridView
                if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count > this.RatesListingGridView.NumRowsVisible)
                {
                    this.RatesListingGridVscrollBar.Visible = false;
                }
                else
                {
                    this.RatesListingGridVscrollBar.Visible = true;
                }

                this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates;
                decimal.TryParse(this.propertyInfoDataSet.ListDistrictAssessmentRates.Compute("SUM(Total)", "").ToString(), out this.amountValue);
                this.TotalAssessmentValueTextBox.Text = this.amountValue.ToString();
                ////attachment and comments
                this.CommentsdeckWorkspace.Enabled = true;
                this.RecordNavigatorSmartPartdeckWorkspace.Enabled = true;
                this.DetailsButton.Enabled = true;
                this.EnableFormControls(true);
                this.OwnerNameButton.Enabled = true;
                this.StatusBarDeckWorkspace.Enabled = true;
                this.SetAttachmentCommentsCount();
                this.LockTextBoxControls(!this.PermissionEdit);
                this.EnableButtonControls(!this.ispaid);
            }
            else
            {
                this.ClearDistrictAssessmentDetails();
                this.PropertyInfoPanel.Enabled = false;
                this.RatesListingGridView.CurrentCell = null;
                this.GridPanel.Enabled = false;
            }
                
            this.formLoad = false;
        }
        #endregion GetDistrictAssessmentDetails

        #region FillDistrictAssessmentDetails
        /// <summary>
        /// Fills the mortgage template details.
        /// </summary>
        /// <param name="currentRowIndex">Index of the current row.</param>
        private void FillDistrictAssessmentDetails(int currentRowIndex)
        {
            try
            {
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.districtAssessmentIds = this.form1031Control.WorkItem.F1031_ListDistrictAssessmentIDs();
                this.totalDistrictAssessmentIdsCount = this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count;

                if (this.totalDistrictAssessmentIdsCount > 0)
                {
                    if (currentRowIndex > this.totalDistrictAssessmentIdsCount)
                    {
                        currentRowIndex = this.totalDistrictAssessmentIdsCount;
                    }

                    if (currentRowIndex == 0)
                    {
                        this.currentRecord = 1;
                    }
                    else
                    {
                        this.currentRecord = currentRowIndex;
                    }
                    
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.currentRecord));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalDistrictAssessmentIdsCount));
                    this.recordPointerArray[0] = this.currentRecord;
                    this.recordPointerArray[1] = this.totalDistrictAssessmentIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.GetDistrictAssessmentDetails(this.RetrieveDistrictAssessmentId(this.currentRecord));
                    this.DisplayTotal();
                }
                else
                {
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.currentRecord));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalDistrictAssessmentIdsCount));
                    this.recordPointerArray[0] = this.currentRecord;
                    this.recordPointerArray[1] = this.totalDistrictAssessmentIdsCount;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    this.SetDefaultState();
                    this.RatesListingGridView.CurrentCell = null;
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
        #endregion FillDistrictAssessmentDetails
        
        #region RetrieveDistrictAssessmentId
        /// <summary>
        /// retrieves the current exciseId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>integer</returns>
        private int RetrieveDistrictAssessmentId(int index)
        {
            int tempDistrictAssessmentID = 0;
            if (this.districtAssessmentIds.Tables.Count > 0)
            {
                if (index > 0)
                {
                    if (index <= this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count)
                    {
                        if (this.districtAssessmentIds.ListDistrictAssessmentID.Rows[index - 1][0].ToString() != null)
                        {
                            tempDistrictAssessmentID = int.Parse(this.districtAssessmentIds.ListDistrictAssessmentID.Rows[index - 1][0].ToString());
                        }
                    }
                }
                else
                {
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count));
                    this.SetRecordCount(this, new DataEventArgs<int>(this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count));
                    this.recordPointerArray[0] = this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count;
                    this.recordPointerArray[1] = this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count;
                    this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                    tempDistrictAssessmentID = int.Parse(this.districtAssessmentIds.ListDistrictAssessmentID.Rows[this.districtAssessmentIds.ListDistrictAssessmentID.Rows.Count - 1][0].ToString()); ////toverify
                }
            }

            return tempDistrictAssessmentID;
        }
        #endregion RetrieveDistrictAssessmentId

        /// <summary>
        /// Enables the form controls.
        /// </summary>
        /// <param name="enableValue">if set to <c>true</c> [enable value].</param>
        private void EnableFormControls(bool enableValue)
        {
            this.PropertyInfoPanel.Enabled = enableValue;
            this.GridPanel.Enabled = enableValue;
        }

        /// <summary>
        /// Sets the state of the default.
        /// </summary>
        private void SetDefaultState()
        {
            this.ClearDistrictAssessmentDetails();
            this.NullRecords = true;
            this.DetailsButton.Enabled = false;
            this.EnableFormControls(false);
            this.StatusBarDeckWorkspace.Enabled = false;
            this.CommentsdeckWorkspace.Enabled = false;
            this.RecordNavigatorSmartPartdeckWorkspace.Enabled = false;
            this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
        }

        /// <summary>
        /// Locks the text box controls.
        /// </summary>
        /// <param name="lockControl">if set to <c>true</c> [lock control].</param>
        private void LockTextBoxControls(bool lockControl)
        {
            this.ParcelNumberTextBox.LockKeyPress = lockControl;
            this.IrrigableAcresTextBox.LockKeyPress = lockControl;
            this.RPAcresCountTextBox.LockKeyPress = lockControl;
            this.TurnoutTextBox.LockKeyPress = lockControl;
            this.SitusTextBox.LockKeyPress = lockControl;
            this.MapTextBox.LockKeyPress = lockControl;
            this.LegalTextBox.LockKeyPress = lockControl;
            this.RatesListingGridView.Columns[4].ReadOnly = lockControl;
        }

        /// <summary>
        /// Enables the button controls.
        /// </summary>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        private void EnableButtonControls(bool enable)
        {
            this.ParcelNumButton.Enabled = enable;
            this.OwnerNameButton.Enabled = enable;
            ////this.SpecialDistrictButton.Enabled = enable;
        }

        /// <summary>
        /// Clears the excise details.
        /// </summary>
        private void ClearDistrictAssessmentDetails()
        {
            this.ParcelNumberTextBox.Text = string.Empty;
            this.ParcelIDLinkLabel.Text = string.Empty;
            this.TypeTextBox.Text = string.Empty;
            this.IrrigableAcresTextBox.Text = string.Empty;
            this.RPAcresCountTextBox.Text = string.Empty;
            this.TurnoutTextBox.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.OwnerNameLinkLabel.Text = string.Empty;
            this.MinDistFeeTextBox.Text = string.Empty;
            this.RollYearTextBox.Text = string.Empty;
            this.SpecialDistrictLinkLabel.Text = string.Empty;
            this.StatementNumberLinkLabel.Text = string.Empty;
            this.LoanTextBox.Text = string.Empty;
            this.MortgageCoTextBox.Text = string.Empty;
            this.SitusTextBox.Text = string.Empty;
            this.MapTextBox.Text = string.Empty;
            this.LegalTextBox.Text = string.Empty;
            this.TotalAssessmentValueTextBox.Text = string.Empty;
            this.propertyInfoDataSet.ListDistrictAssessmentRates.Clear();
            this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates;
            this.currentRecord = 0;
            this.footerSmartPart.KeyId = null;
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
        /// News the button_ click.
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.ClearDistrictAssessmentDetails();
                this.currentStatementId = 0;
                this.SpecialDistrictLinkLabel.Tag = 0;
                this.SetActiveRecord(this, new DataEventArgs<int>(0));
                this.SetRecordCount(this, new DataEventArgs<int>(0));
                this.recordPointerArray[0] = 0;
                this.recordPointerArray[1] = 0;
                this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(this.recordPointerArray));
                this.CommentsdeckWorkspace.Enabled = false;
                this.DetailsButton.Enabled = false;
                this.StatusBarDeckWorkspace.Enabled = false;
                this.EnableFormControls(this.PermissionFiled.newPermission);
                this.ParcelNumberTextBox.Focus();
                this.OwnerNameButton.Enabled = false;
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.SpecialDistrictButton.Enabled = true;
                this.additionalOperationSmartPart.AdditionalOperationCountEnt = new AdditionalOperationCountEntity(0, 0, false);
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string validationErrors = string.Empty;
                string ratesListingDetails = string.Empty;
                F1031SpecialDistrictAssessmentData saveSpecialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
                F1031SpecialDistrictAssessmentData.ListDistrictAssessmentPropertyRow dr = saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.NewListDistrictAssessmentPropertyRow();

                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()) && Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString()) > 0 && !this.acresEmpty)
                {
                    dr.ParcelNumber = this.ParcelNumberTextBox.Text;
                    dr.SADistrictID = Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString());
                    dr.MortgageID = Convert.ToInt32(this.MortgageCoTextBox.Tag.ToString());
                    dr.StatementNumber = this.StatementNumberLinkLabel.Text;
                    dr.ParcelID = Convert.ToInt32(this.ParcelIDLinkLabel.Text);
                    dr.PostTypeID = Convert.ToByte(this.TypeTextBox.Tag.ToString());
                    dr.RollYear = Convert.ToInt16(this.RollYearTextBox.Text);

                    byte maxStructValue = byte.MaxValue;
                    int tempIrrgAcres, tempAcres;
                    int.TryParse(this.IrrigableAcresTextBox.Text.Trim(), out tempIrrgAcres);
                    int.TryParse(this.RPAcresCountTextBox.Text.Trim(), out tempAcres);

                    if (!string.IsNullOrEmpty(this.IrrigableAcresTextBox.Text.Trim()))
                    {
                        if ((Convert.ToInt32(tempIrrgAcres) > maxStructValue))
                        {
                            validationErrors = validationErrors + SharedFunctions.GetResourceString("1031IrrigableAcresMaxvalue"); ////"Irrigable Acres value should not exceed 255. \n";
                            this.IrrigableAcresTextBox.Text = "0";
                        }
                        else
                        {
                            dr.IrrgAcres = Convert.ToInt16(this.IrrigableAcresTextBox.Text.Trim());
                        }
                    }

                    if (!string.IsNullOrEmpty(this.RPAcresCountTextBox.Text.Trim()))
                    {
                        if (Convert.ToInt32(tempAcres) > maxStructValue)
                        {
                            validationErrors = validationErrors + SharedFunctions.GetResourceString("1031RPAcresCountMaxvalue"); //// "RPAcresCount value should not exceed 255.";
                            this.RPAcresCountTextBox.Text = "0";
                        }
                        else
                        {
                            dr.Acres = Convert.ToInt16(this.RPAcresCountTextBox.Text.Trim());
                        }
                    }

                    if (!string.IsNullOrEmpty(this.TurnoutTextBox.Text.Trim()))
                    {
                        dr.Turnouts =this.TurnoutTextBox.Text;
                    }

                    dr.Situs = this.SitusTextBox.Text.Trim();
                    dr.MapNumber = this.MapTextBox.Text.Trim();
                    dr.Legal = this.LegalTextBox.Text.Trim();

                    if (this.RatesListingGridView.DataSource != null)
                    {
                        DataTable dt = ((DataView)this.RatesListingGridView.DataSource).Table;
                        ratesListingDetails = Utility.GetXmlString(dt);
                    }

                    if (this.currentStatementId > 0)
                    {
                        dr.StatementID = this.currentStatementId;
                    }
                    else
                    {
                        dr.StatementID = 0;
                    }

                    this.acresEmpty = false;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("1031MissingRequiredFields"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.acresEmpty = false;
                    return false;
                }

                if (string.IsNullOrEmpty(validationErrors.Trim()))
                {
                    int saveResult;
                    saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.Rows.Add(dr);
                    DataSet tempDataSet = new DataSet("Root");
                    tempDataSet.Tables.Add(saveSpecialDistrictAssessmentData.ListDistrictAssessmentProperty.Copy());
                    tempDataSet.Tables[0].TableName = "Table";
                    string propertyDetails = tempDataSet.GetXml();
                    saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, false, false, TerraScanCommon.UserId);

                    if (saveResult == -2)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentPartI") + this.TypeTextBox.Text + SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentPartII"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("1031DuplicateParcelAssesssmentTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ////saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, true, false);

                            if (MessageBox.Show(SharedFunctions.GetResourceString("1031AssociateOwnerToStatement"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("CopyOwnersFromParcel"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, true, false, TerraScanCommon.UserId);
                            }
                            else
                            {
                                saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, true, true, TerraScanCommon.UserId);
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (saveResult == -3)
                    {
                        if (MessageBox.Show(SharedFunctions.GetResourceString("1031AssociateOwnerToStatement"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("CopyOwnersFromParcel"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, true, false, TerraScanCommon.UserId);
                        }
                        else
                        {
                            saveResult = this.form1031Control.WorkItem.F1031_SaveDistrictAssessmentDetails(propertyDetails, ratesListingDetails, true, true, TerraScanCommon.UserId);
                        }
                    }
                    else if (saveResult == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(validationErrors, ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                this.SetButtons(TerraScanCommon.ButtonActionMode.SaveMode);
                this.SpecialDistrictButton.Enabled = false;
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.RatesListingGridView.AllowSorting = true;
                this.RatesListingGridView.TabStop = true;

                if (onclose)
                {
                    return true;
                }

                if (this.currentRecord > 0)
                {
                    this.FillDistrictAssessmentDetails(this.currentRecord);
                }
                else
                {
                    this.FillDistrictAssessmentDetails(-1);
                }

                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Cancels the button_ click.
        /// </summary>
        private void CancelButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.FillDistrictAssessmentDetails(this.currentRecord);
                if (this.currentRecord <= 0)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }

                this.SpecialDistrictButton.Enabled = false;
                this.acresEmpty = false;
                this.RatesListingGridView.AllowSorting = true;
                this.RatesListingGridView.TabStop = true;
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
        /// Deletes the button_ click.
        /// </summary>
        private void DeleteButton_Click()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.currentStatementId > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteRecord"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.form1031Control.WorkItem.F1031_DeleteDistrictAssessment(this.currentStatementId, TerraScanCommon.UserId);
                    }
                }

                this.districtAssessmentIds = this.form1031Control.WorkItem.F1031_ListDistrictAssessmentIDs();
                this.FillDistrictAssessmentDetails(this.currentRecord);
                this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.ispaid == false && this.pageMode == TerraScanCommon.PageModeTypes.View && this.PermissionEdit)
            {
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.RatesListingGridView.AllowSorting = false;
                this.RatesListingGridView.TabStop = false;
            }
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            try
            {
                AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);
                if (this.currentStatementId > 0)
                {
                    this.additionalOperationSmartPart.KeyId = this.currentStatementId;
                    additionalOperationCountEntity.AttachmentCount = this.form1031Control.WorkItem.GetAttachmentCount(1020, this.CurrentStatementId, TerraScanCommon.UserId);
                    CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.form1031Control.WorkItem.GetCommentsCount(1020, this.CurrentStatementId, TerraScanCommon.UserId);
                    if (commentsCountDataTable.Rows.Count > 0)
                    {
                        additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                        additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                    }
                }

                this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
            }
            catch (Exception ex)
            {
                ////TODO : Need to find specific exception and handle it.
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Enables the edit record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditRecord(object sender, EventArgs e)
        {
            if (!this.formLoad)
            {
                this.SetEditRecord();
            }
        }

        /// <summary>
        /// Handles the Click event of the DetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Hashtable reportOptionalParameter = new Hashtable();
                reportOptionalParameter.Clear();
                //// Calling the Common Function for Report
                reportOptionalParameter.Add("StatementID", this.currentStatementId);
                TerraScanCommon.ShowReport(103110, Report.ReportType.Preview, reportOptionalParameter);
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
        /// Handles the Validating event of the ParcelNumberTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string tempParcelNumber = string.Empty;
                int tempParcelId = -999;
           
                if (!string.IsNullOrEmpty(this.ParcelNumberTextBox.Text.Trim()))
                {
                    tempParcelNumber = this.ParcelNumberTextBox.Text.Trim();
                    
                    F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable parcelDetailsDataTable = new F1031SpecialDistrictAssessmentData.GetDistrictAssessmentParcelIDDataTable();
                    parcelDetailsDataTable = this.form1031Control.WorkItem.F1031_GetDistrictAssessmentParcelID(tempParcelNumber, tempParcelId,null).GetDistrictAssessmentParcelID;

                    if (parcelDetailsDataTable.Rows.Count > 0)
                    {
                        //// Fill Property Info
                        this.ParcelNumberTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelNumberColumn.ColumnName].ToString();
                        this.ParcelIDLinkLabel.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelIDColumn.ColumnName].ToString();
                        this.TypeTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.PostTypeIDColumn.ColumnName].ToString();
                        if (Convert.ToInt32(this.TypeTextBox.Tag.ToString()) == 14)
                        {
                            this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031CustomerNumber");
                            this.SpecialDistrictButton.Enabled = false;
                        }
                        else
                        {
                            this.ParcelNumberLabel.Text = SharedFunctions.GetResourceString("1031ParcelNumber");
                            this.SpecialDistrictButton.Enabled = true;
                        }

                        this.RollYearTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.ParcelRollYearColumn.ColumnName].ToString();
                        this.LoanTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LoanNumberColumn.ColumnName].ToString();
                        this.MortgageCoTextBox.Tag = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageIDColumn.ColumnName].ToString();
                        this.MortgageCoTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MortgageNameColumn.ColumnName].ToString();
                        this.SitusTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.SitusColumn.ColumnName].ToString();
                        this.MapTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.MapNumberColumn.ColumnName].ToString();
                        this.LegalTextBox.Text = parcelDetailsDataTable.Rows[0][parcelDetailsDataTable.LegalNotesColumn.ColumnName].ToString();
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F1031ParcelNumber"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ParcelNumberTextBox.Text = string.Empty; 
                        this.ParcelNumberTextBox.Focus();
                    }
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
        }

        /// <summary>
        /// Handles the LinkClicked event of the SpecialDistrictAuditlinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictAuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (this.currentStatementId > 0)
                {
                    ////// calling  Common Function For Report
                    Hashtable reportOptionalParameter = new Hashtable();
                    reportOptionalParameter.Add("StatementID", this.currentStatementId);
                    ////changed the parameter type from string to int
                    TerraScanCommon.ShowReport(103190, Report.ReportType.Preview, reportOptionalParameter);
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
        /// Handles the LinkClicked event of the ParcelIDLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ParcelIDLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (!string.IsNullOrEmpty(this.ParcelIDLinkLabel.Text))
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Parcel Detail Form - FormID - 1006
                    Form parcelDetailForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1006, null, this.form1031Control.WorkItem);
                    ////open form in view mode - possible to edit
                    if (parcelDetailForm != null)
                    {
                        parcelDetailForm.ShowDialog();
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
        /// Handles the LinkClicked event of the OwnerNameLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerNameLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (!string.IsNullOrEmpty(this.OwnerNameLinkLabel.Text))
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Master Name Address Form - FormID - 9100
                    Form masterNameAddressForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9100, null, this.form1031Control.WorkItem);
                    ////open form in view mode - possible to edit
                    if (masterNameAddressForm != null)
                    {
                        masterNameAddressForm.ShowDialog();
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
        /// Handles the Click event of the OwnerNameButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OwnerNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.currentStatementId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Statement Owner Management Form - FormID - 9110
                    Form statementOwnerManagementForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9110, null, this.form1031Control.WorkItem);
                    ////open form in view mode - possible to edit
                    if (statementOwnerManagementForm != null)
                    {
                        statementOwnerManagementForm.ShowDialog();
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
        /// Handles the LinkClicked event of the SpecialDistrictLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormInfo formInfo = TerraScanCommon.GetFormInfo(1030);

            if (!string.IsNullOrEmpty(this.SpecialDistrictLinkLabel.Tag.ToString()))
            {
                formInfo.optionalParameters = new object[] { this.SpecialDistrictLinkLabel.Tag.ToString() };
            }

            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
        }

        /// <summary>
        /// Handles the Click event of the SpecialDistrictButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SpecialDistrictButton_Click(object sender, EventArgs e)
        {
            try
            {
                Form specialDistrictSelectionForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1033, null, this.form1031Control.WorkItem);

                if (specialDistrictSelectionForm != null && specialDistrictSelectionForm.ShowDialog() == DialogResult.OK)
                {
                    int.TryParse(TerraScanCommon.GetValue(specialDistrictSelectionForm, "SpecialDistrictId").ToString(), out this.sadistrictId);
                    if (Convert.ToInt32(this.SpecialDistrictLinkLabel.Tag.ToString()) != this.sadistrictId)
                    {
                        if (this.ratesGridEdited.Equals(true) && this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit))
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("1031existingAcresValue"), ConfigurationWrapper.ApplicationName + " - " + SharedFunctions.GetResourceString("1031AcresValues"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.LoadDefaultDistrictValues();
                            }
                        }
                        else
                        {
                            this.LoadDefaultDistrictValues();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Loads the default district values.
        /// </summary>
        private void LoadDefaultDistrictValues()
        {
            ////F1031SpecialDistrictAssessmentData specialDistrictDetailsDataSet = new F1031SpecialDistrictAssessmentData();
            ////this.specialDistrictDetailsDataSet = this.form1031Control.WorkItem.F1031_ListDistrictAssessment(this.sadistrictId);
            this.propertyInfoDataSet.ListDistrictAssessmentRates.Clear();
            this.propertyInfoDataSet = this.form1031Control.WorkItem.F1031_ListDistrictAssessment(this.sadistrictId);
            
            if (this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows.Count > 0)
            {
                this.SpecialDistrictLinkLabel.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.DistrictNameColumn].ToString();
                this.SpecialDistrictLinkLabel.Tag = this.sadistrictId;
                this.TypeTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.PostNameColumn].ToString();
                this.TypeTextBox.Tag = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.TypeColumn].ToString();
                this.MinDistFeeTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.MinimumDistrictFeeColumn].ToString();
                this.RollYearTextBox.Text = this.propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.Rows[0][propertyInfoDataSet.ListSpecialDistrictAssessmentProperty.DistrictRollYearColumn].ToString();
                this.RatesListingGridView.DataSource = this.propertyInfoDataSet.ListDistrictAssessmentRates;
            }
            else
            {
                this.SpecialDistrictLinkLabel.Text = string.Empty;
                this.SpecialDistrictLinkLabel.Tag = string.Empty;
                this.TypeTextBox.Text = string.Empty;
                this.MinDistFeeTextBox.Text = string.Empty;
                this.RollYearTextBox.Text = string.Empty;
            }

            ////VscrollBar is enabled or disabled based on NumRowsVisible in GridView
            if (this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows.Count  > this.RatesListingGridView.NumRowsVisible)
            {
                this.RatesListingGridVscrollBar.Visible = false;
            }
            else
            {
                this.RatesListingGridVscrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the ParcelNumButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ParcelNumButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int postTypeId;
                int.TryParse(this.TypeTextBox.Tag.ToString().Trim(), out postTypeId);
                switch (postTypeId)
                {
                    case 14:
                        {
                            ////Parcel Detail Form - FormID - 1402
                            Form customerSelectionForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1402, null, this.form1031Control.WorkItem);
                            ////open form in view mode - possible to edit
                            if (customerSelectionForm != null)
                            {
                                customerSelectionForm.ShowDialog();
                            }

                            break;
                        }

                    default:
                        {
                            ////Parcel Detail Form - FormID - 1401
                            Form parcelSelectionForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1401, null, this.form1031Control.WorkItem);
                            ////open form in view mode - possible to edit
                            if (parcelSelectionForm != null)
                            {
                                parcelSelectionForm.ShowDialog();
                            }

                            break;
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
        /// Handles the CellFormatting event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if ((e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.AmountColumn.ColumnName.ToString()].Index) || (e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.AcresColumn.ColumnName.ToString()].Index) || (e.ColumnIndex == this.RatesListingGridView.Columns[this.propertyInfoDataSet.ListDistrictAssessmentRates.TotalColumn.ColumnName.ToString()].Index))
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && (!String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.AmountColumn.ColumnName.ToString()].Value.ToString().Trim()) || !String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.AcresColumn.ColumnName.ToString()].Value.ToString().Trim()) || !String.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells[this.propertyInfoDataSet.ListDistrictAssessmentRates.TotalColumn.ColumnName.ToString()].Value.ToString().Trim())))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                ////e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                ////e.CellStyle.ForeColor = Color.Green;                                
                                e.Value = "0.00";
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                                e.FormattingApplied = true;
                            }
                        }
                        else
                        {
                            e.Value = "0.00 ";
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            ////Only paint if desired column

            if (e.ColumnIndex == this.RatesListingGridView.Columns["Acres"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                this.ratesGridEdited = true;

                // Only paint if text provided, Only paint if desired text is in cell
                if (!string.IsNullOrEmpty(e.Value.ToString().Trim()))
                {
                    string tempvalue = e.Value.ToString().Trim();
                    Decimal outDecimal = 0;

                    if (Decimal.TryParse(tempvalue, out outDecimal))
                    {
                        if (outDecimal > Convert.ToDecimal(999999.99))
                        {
                            outDecimal = 0;
                            MessageBox.Show(SharedFunctions.GetResourceString("1031AcresValidationError"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.SetTotal(e, 0, 0);
                            e.Value = outDecimal;
                            ////string test=this.ratesDetailsDataTable.Rows[e.RowIndex][e.ColumnIndex].ToString();
                        }
                        else
                        {
                            e.Value = outDecimal.ToString();
                            //// If zero validate
                            decimal acresValue = 0;
                            decimal tempAcres, tempAmount;
                            decimal.TryParse(e.Value.ToString(), out tempAcres);
                            decimal.TryParse(this.RatesListingGridView.Rows[e.RowIndex].Cells["Amount"].Value.ToString(), out tempAmount);
                            ////bool tempHasMinimum;
                            ////tempHasMinimum = this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString();
                            this.RatesListingGridView.RefreshEdit();
                            decimal.TryParse(e.Value.ToString(), out acresValue);

                            if (this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("RATE") && acresValue < 1 && this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString().Equals("Yes"))
                            {
                                this.SetTotal(e, 1, tempAmount);
                            }
                            else
                            {
                                this.SetTotal(e, tempAcres, tempAmount);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("1031InvalidAcresValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        outDecimal = 0;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Rows[e.RowIndex]["Total"] = 0;
                        this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }

                    e.ParsingApplied = true;
                    this.RatesListingGridView.RefreshEdit();
                }
                else if (!this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("FEE"))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("1031EmptyAcresValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Sets the total.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        /// <param name="tempAcres">The temp acres.</param>
        /// <param name="tempAmount">The temp amount.</param>
        private void SetTotal(DataGridViewCellParsingEventArgs e, decimal tempAcres, decimal tempAmount)
        {
            if (this.RatesListingGridView.Rows[e.RowIndex].Cells["Minimum"].Value.ToString().ToUpper().Equals("TRUE") && tempAcres < 1)
            {
                tempAcres = 1;
            }

            if (this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().ToUpper().Equals("FEE"))
            {
                this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                this.RatesListingGridView.Rows[e.RowIndex].Cells["Total"].Value = tempAmount;
                this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
            }
            else
            {
                this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = false;
                this.RatesListingGridView.Rows[e.RowIndex].Cells["Total"].Value = tempAmount * tempAcres;
                this.propertyInfoDataSet.ListDistrictAssessmentRates.Columns["Total"].ReadOnly = true;
            }

            this.DisplayTotal();
        }

        /// <summary>
        /// Displays the total.
        /// </summary>
        private void DisplayTotal()
        {
            decimal minDistFee;
            decimal.TryParse(this.propertyInfoDataSet.ListDistrictAssessmentRates.Compute("SUM(Total)", "").ToString(), out this.amountValue);
            decimal.TryParse(this.MinDistFeeTextBox.Text.Replace("$", "").Trim(), out minDistFee);
            if (this.amountValue > minDistFee)
            {
                this.TotalAssessmentValueTextBox.Text = this.amountValue.ToString();
            }
            else
            {
                this.TotalAssessmentValueTextBox.Text = this.MinDistFeeTextBox.Text;
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.formLoad)
            {
                this.SetEditRecord();
                if (string.IsNullOrEmpty(this.RatesListingGridView.Rows[e.RowIndex].Cells["Acres"].Value.ToString()) && this.RatesListingGridView.Rows[e.RowIndex].Cells["RateType"].Value.ToString().Equals("RATE"))
                {
                    this.acresEmpty = true;
                }
                else
                {
                    this.acresEmpty = false;
                }
            }
        }

        /// <summary>
        /// Handles the DataError event of the RatesListingGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void RatesListingGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the LinkClicked event of the StatementNumberLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void StatementNumberLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ////check for valid value
                if (this.currentStatementId > 0)
                {
                    this.Cursor = Cursors.WaitCursor;

                    ////Statement Management Form - FormID - 1080
                    Form statementManagementForm = this.form1031Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1080, null, this.form1031Control.WorkItem);
                    ////open form in view mode - possible to edit
                    if (statementManagementForm != null)
                    {
                        statementManagementForm.ShowDialog();
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
    }
}
