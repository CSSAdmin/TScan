//--------------------------------------------------------------------------------------------
// <copyright file="F1010.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24/08/09         Sadha Shivudu      Implemented TSCO # 2803 - Default Interest/Receipt Dates now global
// 28/06/11          Manoj Kumar       Set Default tender Type as 'Check'
// 12 SEP 2011       Manoj Kumar       Removed the tender Type default 'check' TSCO #13410. 
// 15 Jun 2016      Priyadharshini     Implemented TSCO # 21806 - Enable create receipt button after check for error  
// 22 Oct 2017      Priyadharshini     Implemented TSCO # 21927 - Add Comment All Button   
//*********************************************************************************/

namespace D1010
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Common;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.IO;
    using System.Collections;
    using System.Collections.Specialized;
    ////using TerraScan.UI.BusinessEntities;
    using TerraScan.Common.Reports;
    using System.Globalization;
    using System.Configuration;
    using System.Reflection;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    /// <summary>
    /// Usercontrol for F1010
    /// </summary>
    [SmartPart]
    public partial class F1010 : BaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// constant value to differentiate input controls from others
        /// </summary>
        private const string InputValueField = "VALUEFIELD";

        /// <summary>
        /// importIds variable is used to store list of importIds for MortgageImport. 
        /// </summary>       
        private MortageImportData.GetMortgageImportIdsDataTable importIds = new MortageImportData.GetMortgageImportIdsDataTable();

        /// <summary>
        /// previousImportId variable is used to store import id. 
        /// </summary>       
        private int currentImportId = -1;

        /// <summary>
        /// TotalImportCount variable is used to find total number of ImportIds for MortgageImport. 
        /// </summary>
        private int totalImportCount;

        ///<summary>
        ///Used to identify the form Load
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// DataSet Contains Mortgage Import Details - importIds and importdetails
        /// </summary>
        private MortageImportData mortgageImportDataSet = new MortageImportData();

        /// <summary>
        /// DataTable Contains Error Record Details for an ImportId
        /// </summary>
        private MortageImportData.GetMortgageImportErrorDataTable errorCheckDataTable = new MortageImportData.GetMortgageImportErrorDataTable();

        /// <summary>
        /// IDictionary contains position and width details of fields to be extracted
        /// </summary>
        private IDictionary importFileDetailsDictionary = new ListDictionary();

        /// <summary>
        /// array contains pay type value[
        /// </summary>
        private object[] payCodeTypeArray = new object[] { "Full", "1st Half", "2nd Half" };

        /// <summary>
        /// import file contains mortgage import entry datatable
        /// </summary>
        private ImportFile importFileDataSet = new ImportFile();

        /// <summary>
        /// import file sourcetype enum instance created
        /// </summary>
        private TerraScanCommon.ImportFileSourceType importFileSourceType;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private PageModeTypes pageMode;

        /// <summary>
        /// variable contains mortgage import field structure instance
        /// </summary>
        private MortgageImportFields mortgageImportFieldsInstance = new MortgageImportFields();

        /// <summary>
        /// variable contains error grid sort order
        /// </summary>
        private GridSortOrder errorGridSortOrder;

        /// <summary>
        /// variable contains error grid sorted column
        /// </summary>
        private DataGridViewColumn errorGridSortedColumn;

        /// <summary>
        /// variable contains current active Control
        /// </summary>
        private Control currentActiveControl;

        /// <summary>
        /// variable contains previous active Control
        /// </summary>
        private Control previousActiveControl;
        public static bool flagmsg = false;

        /// <summary>
        /// reportOptionalParameter
        /// </summary>
        private Hashtable reportOptionalParameter = new Hashtable();

        /// <summary>
        /// minimum date
        /// </summary>
        private DateTime minimunDate = new DateTime(1900, 1, 1);

        /// <summary>
        /// maximum date
        /// </summary>
        private DateTime maximumDate = new DateTime(2079, 6, 6);

        /// <summary>
        /// It contains Selecte MortgageImportTemplateId from mortage import selection
        /// </summary>
        private int mortgageImportTemplateId;

        /// <summary>
        /// formLabelInfo
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// F10101 controller
        /// </summary>
        private F1010Controller form1010control;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private OperationSmartPart operationSmartPart;

        /// <summary>
        /// SmartPart instance for accessing the properties
        /// </summary>
        private AdditionalOperationSmartPart additionalOperationSmartPart;


        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 9999
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCountEnt = new AdditionalOperationCountEntity(-99999, -99999, false);

        /// <summary>
        /// Used to Hold the arguments to update Attachments and comments for 2550
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCount = new AdditionalOperationCountEntity(-99999, -99999, false);

              /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultCommentButtonBackColor = Color.FromArgb(174, 150, 94);
          /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color highPriorityCommentColor = Color.FromArgb(255, 0, 0);

        /// <summary>
        /// Set Date Format
        /// </summary>
        private string dateFormat = "M/d/yyyy";

        /// <summary>
        /// footerSmartPart
        /// </summary>
        private FooterSmartPart footerSmartPart;

        /// <summary>
        /// isShift
        /// </summary>
        private bool issShift;

        private bool isLoaded = false;

        /// <summary>
        /// First Half Pay Code.
        /// </summary>
        private int firstHalfPayCode;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MortgageImportForm class.
        /// </summary>
        public F1010()
        {
            this.InitializeComponent();
            ////   this.PaymentEngineUserControl.UserId = TerraScanCommon.UserId;
            this.PaymentEngineUserControl.AllowOverUnder = false;
            this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
            this.PaymentEngineUserControl.PaymentItemChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentItemChangeEventHandler(this.PaymentEngineUserControl_PaymentItemChangeEvent);
            this.ValidEntriesTotalTextBox.TextChanged += new EventHandler(this.ValidEntriesTotalTextBox_TextChanged);
            this.OverUnderAmountTextBox.TextChanged += new EventHandler(this.ValidEntriesTotalTextBox_TextChanged);
            this.Click += new EventHandler(this.MortgageImportForm_Click);
            ////sets tag value to the input field
            ////todo: this.NewButton.Tag = InputValueField;
            this.TaxRollReportButton.Tag = InputValueField;
            this.TemplateNameButton.Tag = InputValueField;
            this.RecieptDateTextBox.Tag = InputValueField;
            this.InterestDateTextBox.Tag = InputValueField;
            this.PartialPaymentCheckBox.Tag = InputValueField;
            this.FirstHalfChkBox.Tag = InputValueField;
            this.FilePathTextBox.Tag = InputValueField;
            this.ImportFileButton.Tag = InputValueField;
            this.CheckErrorButton.Tag = InputValueField;
            this.CreateReceiptButton.Tag = InputValueField;
            // this.PrintReceiptButton.Tag = InputValueField;
            //////load pay type list using payCodeTypeArray
            //this.PartialPaymentCheckBox.Items.AddRange(this.payCodeTypeArray);

            this.TemplatePictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.TemplatePictureBox.Height, this.TemplatePictureBox.Width, "Template", 28, 81, 128);
            this.ErrorListingPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ErrorListingPictureBox.Height, this.ErrorListingPictureBox.Width, "Error Listing", 174, 150, 98);
            this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.PaymentPictureBox.Height, this.PaymentPictureBox.Width, "Payment", 174, 150, 98);

            this.CustomizeErrorGridView();
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// Event for SetActiveRecord
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecord, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetActiveRecord;

        /// <summary>
        /// Event for EnableButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationEntity>> EnableButtons;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Event for SetActiveRecordButtons
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetActiveRecordButtons, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int[]>> SetActiveRecordButtons;

        /// <summary>
        /// Event for PageStatusActivated
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_PageStatusActivated, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PageStatusActivated;

        /// <summary>
        /// Event for SetFormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

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
        /// EventPublication for SetFormPermissions
        /// </summary>
        ////[EventPublication("topic://Terrascan.UI.CAB/Modules/SetFormPermissions", PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<int[]>> SetFormPermissions;

        /// <summary>
        /// EventPublication for SetButtons
        /// </summary>
        ////[EventPublication("topic://Terrascan.UI.CAB/Modules/SetButtons", PublicationScope.WorkItem)]
        ////public event EventHandler<DataEventArgs<Enum>> SetButtons;

        /// <summary>
        /// Event for SetRecordCount
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_SetRecordCount, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<int>> SetRecordCount;

        #endregion Event Publication

        #region Enum
        /// <summary>
        /// Enumerator Status Action
        /// </summary>
        public enum GridSortOrder
        {
            /// <summary>
            /// ASC = 0
            /// </summary>
            Asc = 0,

            /// <summary>
            /// DESC = 1
            /// </summary>
            Desc = 1
        }

        /// <summary>
        /// Enumerator PageMode
        /// </summary>
        public enum PageModeTypes
        {
            /// <summary>
            /// View = 0.
            /// </summary>
            View = 0,

            /// <summary>
            /// New = 1.
            /// </summary>
            New,

            /// <summary>
            /// Edit = 2.
            /// </summary>
            Edit
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the f1010control.
        /// </summary>
        /// <value>The f1010control.</value>
        [CreateNew]
        public F1010Controller F1010control
        {
            get { return this.form1010control as F1010Controller; }
            set { this.form1010control = value; }
        }

        /// <summary>
        /// Gets or sets the MortgageImportTemplateId
        /// </summary>
        /// <value>The MortgageImportTemplateId.</value>
        public int MortgageImportTemplateId
        {
            get { return this.mortgageImportTemplateId; }
            set { this.mortgageImportTemplateId = value; }
        }

        /// <summary>
        /// gets or sets the Previous import id
        /// </summary>
        /// <value>The previous Import Id.</value>
        private int CurrentImportId
        {
            get { return this.currentImportId; }
            set { this.currentImportId = value; }
        }

        /// <summary>
        /// gets or sets the current Active Control
        /// </summary>
        /// <value>The current Active Control.</value>
        private Control CurrentActiveControl
        {
            get { return this.currentActiveControl; }
            set { this.currentActiveControl = value; }
        }

        /// <summary>
        /// gets or sets the previous Active Control
        /// </summary>
        /// <value>The previous Active Control.</value>
        private Control PreviousActiveControl
        {
            get { return this.previousActiveControl; }
            set { this.previousActiveControl = value; }
        }

        /// <summary>
        /// Gets or sets the page mode.
        /// </summary>
        /// <value>The page mode.</value>
        private PageModeTypes PageMode
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
        /// Gets or sets the error Grid Sort Order
        /// </summary>
        /// <value>The Error Grid Sort Order.</value>
        private GridSortOrder ErrorGridSortOrder
        {
            get { return this.errorGridSortOrder; }
            set { this.errorGridSortOrder = value; }
        }

        /// <summary>
        /// Gets or sets the error Grid Sorted Column
        /// </summary>
        /// <value>The Error Grid Sorted Column.</value>
        private DataGridViewColumn ErrorGridSortedColumn
        {
            get { return this.errorGridSortedColumn; }
            set { this.errorGridSortedColumn = value; }
        }

        #endregion

        #region Event Subscription

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
        /// Displays the navigated record.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_RecordNavigatorSmartPart_DisplayNavigatedRecord, Thread = ThreadOption.UserInterface)]
        public void DisplayNavigatedRecord(object sender, DataEventArgs<RecordNavigationEntity> e)
        {
            RecordNavigationEntity recordNavigationEntity = e.Data;
            this.CurrentImportId = this.RetrieveImportId(recordNavigationEntity.RecordIndex);
            this.additionalOperationSmartPart.KeyId = this.currentImportId;
            this.FillImportFormDetails(null, recordNavigationEntity.RecordNavigationFlag);
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
                this.F1010control.WorkItem.State["FormStatus"] = this.CheckPageStatus();
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
                    this.CancelMortageButton_Click();
                    break;
                case "DELETE":
                    this.DeleteMortageButton_Click();
                    break;
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

        ////[EventSubscription("topic://TerraScan.UI.CAB/MainForm/SendOptionalParameters", Thread = ThreadOption.UserInterface)]
        ////public void SendOptionalParameters(object sender, DataEventArgs<object[]> e)
        ////{
        ////    object[] optionalParams = e.Data;
        ////    int statementId = Convert.ToInt32(optionalParams[0]);
        ////    string statementText = optionalParams[1];
        ////    MessageBox.Show(statementId.ToString());
        ////}

        #endregion Events Subscription

        #region Protected Methods

        /// <summary>
        /// override wnd proc method
        /// </summary>
        /// <param name="m">The m.</param>
        protected override void WndProc(ref Message m)
        {
            if (this.ActiveControl != null)
            {
                if (!this.ActiveControl.Equals(this.CurrentActiveControl))
                {
                    this.PreviousActiveControl = this.CurrentActiveControl;
                    this.CurrentActiveControl = this.ActiveControl;
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Method will Change the Status Text,Background and foreground Color
        /// </summary>
        /// <param name="statusLabel">StatusLabel to Cahnge</param>
        /// <param name="actionMode">actionMode</param>
        /// <param name="runDt">The runDt contains process ended date and time</param>
        /// <param name="runBy">The runby contains the username</param>
        private static void ChangeStatusLabel(Label statusLabel, Enum actionMode, string runDt, string runBy)
        {
            if (actionMode.Equals(TerraScanCommon.StatusAction.BeforeStatus))
            {
                statusLabel.ForeColor = System.Drawing.Color.White;
                statusLabel.Text = "Pending";
                statusLabel.BackColor = System.Drawing.Color.Silver;
            }
            else if (actionMode.Equals(TerraScanCommon.StatusAction.ProcessStatus))
            {
                statusLabel.ForeColor = System.Drawing.Color.Black;
                statusLabel.Text = "Running..";
                statusLabel.BackColor = System.Drawing.Color.Silver;
            }
            else if (actionMode.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                DateTime tempDateTime;
                if (DateTime.TryParse(runDt.ToString(), out tempDateTime))
                {
                    runDt = String.Concat(tempDateTime.ToShortDateString(), " ", tempDateTime.ToShortTimeString().ToLower().Replace(" ", ""));
                }
                else
                {
                    runDt = String.Empty;
                }

                statusLabel.ForeColor = System.Drawing.Color.White;
                statusLabel.Text = String.Concat(runDt, " ", runBy);
                statusLabel.BackColor = System.Drawing.Color.FromArgb(71, 133, 85);
            }

            statusLabel.Refresh();
        }

        /// <summary>
        /// Validate field value with datatype
        /// </summary>
        /// <param name="fieldValue">The field value to be validated</param>
        /// <param name="fieldDataType">The datatype to be used</param>
        /// <returns>returns validate value</returns>
        private static object ValidateFieldValue(string fieldValue, Type fieldDataType)
        {
            ////store validated value
            Object validatedFieldValue = null;
            int tempIntegerValue = 0;
            decimal tempDecimalValue = 0;

            ////checks for integer datatype
            if (Type.Equals(fieldDataType, typeof(int)))
            {
                if (String.IsNullOrEmpty(fieldValue))
                {
                    validatedFieldValue = 0;
                }
                else
                {
                    int.TryParse(fieldValue, NumberStyles.Currency, null, out tempIntegerValue);
                    validatedFieldValue = tempIntegerValue;
                }
            }
            else if (Type.Equals(fieldDataType, typeof(decimal)))
            {
                ////checks for decimal datatype
                if (String.IsNullOrEmpty(fieldValue))
                {
                    validatedFieldValue = 0;
                }
                else
                {
                    decimal.TryParse(fieldValue, NumberStyles.Currency, null, out tempDecimalValue);
                    validatedFieldValue = tempDecimalValue;
                }
            }
            else
            {
                validatedFieldValue = fieldValue;
            }

            return validatedFieldValue;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determines whether  DataTable is Exist or not in  [the specified CMNT data set].
        /// </summary>
        /// <param name="tempDataSet">The MIErrorCheck data set.</param>
        /// <returns>
        /// 	<c>true</c> if [is data table] [the specified MIErrorCheck data set]; otherwise, <c>false</c>.
        /// </returns>
        private static bool CheckValidDataSet(DataSet tempDataSet)
        {
            if (tempDataSet.Tables.Count > 0 && tempDataSet != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (!this.PageMode.Equals(PageModeTypes.View))
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", this.AccessibleName, "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveMortgageImport(true);
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        #region Mortgage Import Form - Import Get And Retrieve Method

        /// <summary>
        /// retrieves the current importId
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>import id of the current record</returns>
        private int RetrieveImportId(int index)
        {
            int tempImportID = 0;

            if (this.importIds.Rows.Count > 0)
            {
                if (index > 0 && index <= this.importIds.Rows.Count)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(this.importIds.Rows[index - 1][0])))
                    {
                        tempImportID = Convert.ToInt32(this.importIds.Rows[index - 1][0]);
                    }
                }
                else
                {
                    tempImportID = Convert.ToInt32(this.importIds.Rows[this.importIds.Rows.Count - 1][0]);
                    this.SetActiveRecord(this, new DataEventArgs<int>(this.importIds.Rows.Count));
                }
            }

            return tempImportID;
        }

        /// <summary>
        /// retrieves the current import position index
        /// </summary>        
        /// <returns>index of the current record</returns>
        private int RetrieveImportIndex()
        {
            int tempIndex = this.importIds.Rows.Count;
            DataTable tempDataTable = this.importIds.Copy();
            tempDataTable.DefaultView.RowFilter = "ImportID = " + this.CurrentImportId.ToString();

            if (tempDataTable.DefaultView.Count > 0)
            {
                tempIndex = tempDataTable.Rows.IndexOf(tempDataTable.DefaultView[0].Row) + 1;
            }

            return tempIndex;
        }

        /// <summary>
        /// Gets the Import details and fill MortgageImportForm Header accordingly.
        /// </summary>        
        private void GetImportDetails()
        {
            MortageImportData mortgageImportDetailsDataSet = (MortageImportData)this.mortgageImportDataSet.Copy();
            string tempValue = String.Empty;
            //// mortgageImportDetailsDataSet.Tables.RemoveAt(0);
            this.IncludeNecessaryEventHandler(false);
            this.mortgageImportFieldsInstance = new MortgageImportFields();

            if (mortgageImportDetailsDataSet.Tables.Count > 0 && mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows.Count > 0)
            {
                ////fill Import header                   
                this.ImportIdTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ImportIDColumn].ToString();
                this.TemplateIdTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.TemplateIDColumn].ToString();
                this.TemplateNameTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.TemplateNameColumn].ToString();
                this.SourceTypeTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.TypeNameColumn].ToString();
                this.FilePathTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.FilePathColumn].ToString();
                this.RecieptDateTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ReceiptDateColumn].ToString();
                this.InterestDateTextBox.Text = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.InterestDateColumn].ToString();

                if (mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.AllowPartialPmtsColumn].ToString().ToLower().Equals("true"))
                {
                    this.PartialPaymentCheckBox.Checked = true;
                }
                else
                {
                    this.PartialPaymentCheckBox.Checked = false;
                }

                //First Half CheckBox - #21976.
                int paycode = 0;
                int.TryParse(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0]["PayCode"].ToString(), out paycode);

                if(paycode==1)
                {
                    this.FirstHalfChkBox.Checked = true;
                }
                else
                {
                    this.FirstHalfChkBox.Checked = false;
                }                
                
                if (!mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.TypeIDColumn].Equals(System.DBNull.Value))
                {
                    this.mortgageImportFieldsInstance.SourceTypeId = Convert.ToInt16(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.TypeIDColumn]);
                }

                tempValue = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.EntriesTotalColumn].ToString();
                if (this.CheckInputValueValidity(ref tempValue, TypeCode.Decimal))
                {
                    this.EntriesTotalTextBox.Text = tempValue;
                }

                ////fill auditlink
                this.footerSmartPart.KeyId = this.currentImportId;
                ////this.ImportAuditLinkLabel.Text = SharedFunctions.GetResourceString("ImportAuditLink") + this.ImportIdTextBox.Text;
                ////this.ImportAuditLinkLabel.Enabled = true;
                ////fill error listing details                
                tempValue = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ErrorTotalColumn].ToString();
                if (this.CheckInputValueValidity(ref tempValue, TypeCode.Decimal))
                {
                    this.ErrorAmountTotalTextBox.Text = tempValue;
                }

                tempValue = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ValidTotalColumn].ToString();
                if (this.CheckInputValueValidity(ref tempValue, TypeCode.Decimal))
                {
                    this.ValidEntriesTotalTextBox.Text = tempValue;
                }

                ////sets valid value to the mortgageimportfields structure
                this.ValidateMortgageImportFields();
                ////sets imported and entries count
                this.mortgageImportFieldsInstance.ImportedEntries = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ImportedEntriesColumn].ToString();
                this.mortgageImportFieldsInstance.ErrorEntries = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ErrorEntriesColumn].ToString();

                ////load payment engine
                if (!string.IsNullOrEmpty(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.PPaymentIDColumn].ToString()))
                {
                    this.mortgageImportFieldsInstance.PpaymentId = Convert.ToInt32(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.PPaymentIDColumn]);
                }
                else
                {
                    this.mortgageImportFieldsInstance.PpaymentId = 0;
                }

                ////over/Under
                if (this.mortgageImportFieldsInstance.PpaymentId == 0)
                {
                    tempValue = mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.OverUnderAmountColumn].ToString();
                    this.CheckInputValueValidity(ref tempValue, TypeCode.Decimal);
                    this.OverUnderAmountTextBox.Text = tempValue;
                }

                ////checks for status
                ////import file process                  
                if (!string.IsNullOrEmpty(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ImportRunByColumn].ToString()))
                {
                    this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.AfterStatus;
                    ChangeStatusLabel(this.ImportFileStatusLabel, this.mortgageImportFieldsInstance.ImportFileStatus, mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ImportRunDtColumn].ToString(), mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ImportRunByColumn].ToString());

                    ////checks for error process                    
                    if (!string.IsNullOrEmpty(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ErrorRunByColumn].ToString()))
                    {
                        this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.AfterStatus;
                        ChangeStatusLabel(this.CheckErrorStatusLabel, this.mortgageImportFieldsInstance.CheckErrorStatus, mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ErrorRunDtColumn].ToString(), mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.ErrorRunByColumn].ToString());
                        if (mortgageImportDetailsDataSet.Tables.Count > 1 && mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows.Count > 0)
                        {
                            this.FillCheckErrorDataTable(mortgageImportDetailsDataSet.GetMortgageImportError);
                        }
                        else
                        {
                            this.ClearErrorListingFields();
                        }

                        ////create receipts button will be enabled depending on the balance

                        ////checks for create receipts process
                        if (!string.IsNullOrEmpty(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptCreatedByColumn].ToString()))
                        {
                            this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.AfterStatus;
                            ChangeStatusLabel(this.CreateReceiptStatusLabel, this.mortgageImportFieldsInstance.CreateReceiptStatus, mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptCreatedDtColumn].ToString(), mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptCreatedByColumn].ToString());

                            ////checks for print receipts process
                            if (!string.IsNullOrEmpty(mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptPrintByColumn].ToString()))
                            {
                                this.mortgageImportFieldsInstance.PrintReceiptStatus = TerraScanCommon.StatusAction.AfterStatus;
                                ChangeStatusLabel(this.CreateReceiptStatusLabel, this.mortgageImportFieldsInstance.PrintReceiptStatus, mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptPrintDtColumn].ToString(), mortgageImportDetailsDataSet.GetMortgageImportDetails.Rows[0][mortgageImportDetailsDataSet.GetMortgageImportDetails.RcptPrintByColumn].ToString());
                            }
                            else
                            {
                                this.mortgageImportFieldsInstance.PrintReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                            }
                        }
                        else
                        {
                            this.LockMortgageImportFields(false);
                            this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                        }
                    }
                    else
                    {
                        this.LockMortgageImportFields(false);
                        this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    }
                }
                else
                {
                    this.LockMortgageImportFields(false);
                    this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.BeforeStatus;
                }

                if (!this.mortgageImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    this.ImportFileButton.Enabled = true && this.PermissionEdit;
                }

                this.ChangeStatusRelatedFields();
                this.EnableButtons(this, new DataEventArgs<AdditionalOperationEntity>(new AdditionalOperationEntity(1, 1)));
                this.SetAttachmentCommentsCount();
                ////todo: this.SetButtonText(this, new DataEventArgs<AdditionalOperationCountEntity>(F1010WorkItem.AdditionOperationText(Convert.ToInt32(this.Tag.ToString()), this.currentImportId, TerraScanCommon.UserId)));
                ////this.additionalOperationSmartPart.AdditionalOperationCountEnt = F1010WorkItem.AdditionOperationText(Convert.ToInt32(this.Tag.ToString()), this.currentImportId, TerraScanCommon.UserId);
                this.IncludeNecessaryEventHandler(true);
            }
            else
            {
                this.ClearMortageImportDetails();
            }
        }

        #endregion

        /// <summary>
        /// Populates the Import Header Section.
        /// </summary>
        private void PopulateImportHeader()
        {
            this.PageMode = PageModeTypes.View;
            this.Cursor = Cursors.WaitCursor;
            this.mortgageImportDataSet = F1010WorkItem.GetMortgageImportStatement(this.CurrentImportId, false);
            //// this.importIds = F1010WorkItem.GetMortgageImportStatementIds().Tables[0];
            this.FillImportFormDetails(null, false);
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Popups the tool tip on mouse enter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PopupToolTip(object sender, EventArgs e)
        {
            Control sourceControl = sender as Control;
            int textLength = 0;

            if (sender.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanTextBox)))
            {
                textLength = 61;
                sourceControl = sender as TextBox;
            }
            else if (sender.GetType().Equals(typeof(Label)))
            {
                textLength = 27;
                sourceControl = sender as Label;
            }

            if (sourceControl != null && sourceControl.Text.Length > textLength)
            {
                this.ImportToolTip.SetToolTip(sourceControl, sourceControl.Text);
            }
            else
            {
                this.ImportToolTip.RemoveAll();
            }
        }

        /// <summary>
        /// Fill Import Header Details
        /// </summary>
        /// <param name="tempDataSet">The temp data set.</param>
        /// <param name="fetchNextRecord">if set to <c>true</c> [fetch next record].</param>
        private void FillImportFormDetails(MortageImportData tempDataSet, bool fetchNextRecord)
        {
            this.PageMode = PageModeTypes.View;
            ////this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            this.Cursor = Cursors.WaitCursor;
            int recordIndex = 0;

            try
            {
                if (tempDataSet == null)
                {
                    this.mortgageImportDataSet = F1010WorkItem.GetMortgageImportStatement(this.CurrentImportId, fetchNextRecord);
                }
                else
                {
                    this.mortgageImportDataSet = tempDataSet;
                }
                if (this.formLoad)
                {
                    int i = this.mortgageImportDataSet.PayListTable.Rows.Count;
                    //Commented by purushotham 
                    //for (int j = 0; j < i; j++)
                    //{
                    //  //  this.PartialPaymentCheckBox.Items.Add(this.mortgageImportDataSet.PayListTable.Rows[j]["PaycodeText"].ToString());
                    //}
                }
                if (this.mortgageImportDataSet.Tables.Count > 0 && this.mortgageImportDataSet.GetMortgageImportIds.Rows.Count > 0)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                    this.importIds = this.mortgageImportDataSet.GetMortgageImportIds;
                    this.totalImportCount = this.importIds.Rows.Count;

                    this.SetRecordCount(this, new DataEventArgs<int>(this.totalImportCount));
                    if (this.mortgageImportDataSet.Tables.Count > 1 && this.mortgageImportDataSet.GetMortgageImportDetails.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(this.mortgageImportDataSet.GetMortgageImportDetails.Rows[0][this.mortgageImportDataSet.GetMortgageImportDetails.ImportIDColumn].ToString()))
                        {
                            this.CurrentImportId = Convert.ToInt32(this.mortgageImportDataSet.GetMortgageImportDetails.Rows[0][this.mortgageImportDataSet.GetMortgageImportDetails.ImportIDColumn]);
                        }
                        if (!(this.mortgageImportFieldsInstance.ImportId > 0 && this.mortgageImportFieldsInstance.ImportId == this.CurrentImportId))
                        {
                            this.PaymentEngineUserControl.AllowOverUnder = false;
                            this.PaymentEngineUserControl.LoadPayment();
                        }

                        this.GetImportDetails();
                        recordIndex = this.RetrieveImportIndex();
                        this.SetActiveRecord(this, new DataEventArgs<int>(recordIndex));
                        int[] recordPointerArray = new int[2];
                        recordPointerArray[0] = recordIndex;
                        recordPointerArray[1] = this.totalImportCount;
                        this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(recordPointerArray));
                    }
                    else
                    {
                        this.CurrentImportId = -1;
                        this.ClearMortageImportDetails();
                        this.NullRecords = true;
                        this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    }
                }
                else
                {
                    this.CurrentImportId = -1;
                    this.NullRecords = true;
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                    this.ClearMortageImportDetails();
                    //// this.operationSmartPart.DeleteButtonEnable = false;
                }
            }
            catch (Exception ex)
            {
                ////ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.FocusRequiredInputField(this.MortgageImportPanel, true, false);
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Get Template Details and fill it to the corresponding fields
        /// </summary>
        /// <param name="templateId">the templateid</param>
        private void FillTemplateDetails(int templateId)
        {
            MortgageImpotTemplateData mortgageImportTemplateDataSet = F1010WorkItem.GetMortgageTemplate(templateId);

            if (mortgageImportTemplateDataSet.Tables.Count > 0 && mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows.Count > 0)
            {
                this.TemplateIdTextBox.Text = mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["TemplateID"].ToString();
                this.TemplateNameTextBox.Text = mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["TemplateName"].ToString();
                this.FilePathTextBox.Text = mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["FilePath"].ToString();
                this.SourceTypeTextBox.Text = mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["TypeName"].ToString();
                if (!string.IsNullOrEmpty(mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["TypeID"].ToString()))
                {
                    this.mortgageImportFieldsInstance.SourceTypeId = Convert.ToInt16(mortgageImportTemplateDataSet.GetMortgageImportTemplate.Rows[0]["TypeID"]);
                }
            }
        }

        /// <summary>
        /// This Method used to  Allign The Header width , Column width , And Font Size for 
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizeErrorGridView()
        {
            //// TODO All styles Should be take from config file
            this.ErrorGridView.AllowUserToResizeColumns = false;
            this.ErrorGridView.AutoGenerateColumns = false;
            this.ErrorGridView.AllowUserToResizeRows = false;
            this.ErrorGridView.StandardTab = true;
            this.ErrorGridView.EnableHeadersVisualStyles = false;
            DataGridViewColumnCollection columns = this.ErrorGridView.Columns;

            columns["StatementID"].DataPropertyName = "StatementID";
            columns["StatementNumber"].DataPropertyName = "StatementNumber";
            ////columns["BankCode"].DataPropertyName = "BankCode";
            columns["ParcelNumber"].DataPropertyName = "ParcelNumber";
            columns["ErrorStatus"].DataPropertyName = "ErrorStatus";
            columns["PayAmt"].DataPropertyName = "Amount";
            columns["FileLine"].DataPropertyName = "FileLine";
            columns["EntryID"].DataPropertyName = "EntryID";
            columns["DueAmount"].DataPropertyName = "DueAmount";
            columns["StatusID"].DataPropertyName = "StatusID";
            columns["PostType"].DataPropertyName = "PostType";

            columns["StatementID"].DisplayIndex = 0;
            columns["StatementNumber"].DisplayIndex = 1;
            //// columns["BankCode"].DisplayIndex = 2;
            columns["ParcelNumber"].DisplayIndex = 2;
            columns["ErrorStatus"].DisplayIndex = 3;
            columns["PayAmt"].DisplayIndex = 4;
            columns["FileLine"].DisplayIndex = 5;
            columns["EntryID"].DisplayIndex = 6;
            columns["DueAmount"].DisplayIndex = 7;
            columns["StatusID"].DisplayIndex = 8;
            columns["PostType"].DisplayIndex = 9;

            this.ErrorGridView.DataSource = this.errorCheckDataTable.DefaultView;

            ////columns["StatementID"].Width= 100;
            ////columns["StatementNumber"].Width = 100;
            ////columns["BankCode"].Width = 100;
            ////columns["ErrorStatus"].Width = 220;
            ////columns["PayAmt"].Width = 100;
            ////columns["FileLine"].Width = 50;
            ////columns["EntryID"].Width = 60;
        }

        /// <summary>
        /// Shows the RecieptDate calender in particular location.
        /// </summary>
        private void ShowRecieptDateCalender()
        {
            this.MortgageMonthCalendar.Visible = true;
            this.issShift = false;
            this.MortgageMonthCalendar.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.MortgageMonthCalendar.Left = this.TemplateHeaderPanel.Left + this.RecieptDatePanel.Left + this.ReceiptDateButton.Left + this.ReceiptDateButton.Width;
            this.MortgageMonthCalendar.Top = this.TemplateHeaderPanel.Top + this.RecieptDatePanel.Top + this.ReceiptDateButton.Top;
            this.MortgageMonthCalendar.Tag = this.ReceiptDateButton.Tag;
            this.MortgageMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.RecieptDateTextBox.Text))
            {
                this.MortgageMonthCalendar.SetDate(Convert.ToDateTime(this.RecieptDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the InterestDate calender in particular location.
        /// </summary>
        private void ShowInterestDateCalender()
        {
            this.MortgageMonthCalendar.Visible = true;
            this.MortgageMonthCalendar.ScrollChange = 1;

            //// Display the Calender control near the Calender Picture box.
            this.MortgageMonthCalendar.Left = this.TemplateHeaderPanel.Left + this.InterestDatePanel.Left + this.InterestDateButton.Left + this.InterestDateButton.Width;
            this.MortgageMonthCalendar.Top = this.TemplateHeaderPanel.Top + this.InterestDatePanel.Top + this.InterestDateButton.Top;
            this.MortgageMonthCalendar.Tag = this.InterestDateButton.Tag;
            this.MortgageMonthCalendar.Focus();
            if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
            {
                this.MortgageMonthCalendar.SetDate(Convert.ToDateTime(this.InterestDateTextBox.Text));
            }
        }

        /// <summary>
        /// Sets the attachment comments count.
        /// </summary>
        private void SetAttachmentCommentsCount()
        {
            ////Display Comments Count in the Comments Buttons  and attachment count in the attachment Buttons       
            AdditionalOperationCountEntity additionalOperationCountEntity = new AdditionalOperationCountEntity(0, 0, false);

            if (!string.IsNullOrEmpty(this.CurrentImportId.ToString()))
            {
                this.additionalOperationSmartPart.KeyId = this.CurrentImportId;
                additionalOperationCountEntity.AttachmentCount = this.F1010control.WorkItem.GetAttachmentCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.CurrentImportId), TerraScanCommon.UserId);
                CommentsData.GetCommentsCountDataTable commentsCountDataTable = this.F1010control.WorkItem.GetCommentsCount(Convert.ToInt32(this.Tag), Convert.ToInt32(this.CurrentImportId), TerraScanCommon.UserId);
                if (commentsCountDataTable.Rows.Count > 0)
                {
                    additionalOperationCountEntity.CommentCount = Convert.ToInt32(commentsCountDataTable.Rows[0][commentsCountDataTable.CommentCountColumn]);
                    additionalOperationCountEntity.HighPriority = Convert.ToBoolean(commentsCountDataTable.Rows[0][commentsCountDataTable.PriorityFlagColumn]);
                }
            }

            this.additionalOperationSmartPart.AdditionalOperationCountEnt = additionalOperationCountEntity;
        }

        #region Clear Mortgage Import

        /// <summary>
        /// Method will Clear the PaymentEngine DataGrid
        /// </summary>
        private void ClearMortageImportDetails()
        {
            this.ClearOtherFields();
            this.ClearHeaderTemplate(false);
            this.ResetStatusButtons();
            this.PaymentEngineUserControl.AllowOverUnder = false;
            this.PaymentEngineUserControl.LoadPayment();
            if (!this.PageMode.Equals(PageModeTypes.New))
            {
                this.TemplateHeaderPanel.Enabled = false;
                this.ErrorAmountTotalTextBox.Enabled = false;
                this.ValidEntriesTotalTextBox.Enabled = false;
                this.OverUnderAmountTextBox.Enabled = false;
                this.BalanceAmountTextBox.Enabled = false;
                this.PaymentsTotalTextBox.Enabled = false;
                this.PaymentEngineUserControl.Enabled = false;
                this.PaymentEngineUserControl.Locked = true;
                this.ValidEntriesTotalLabel.Enabled = false;
                this.PaymentsTotalLabel.Enabled = false;
                this.OverUnderLabel.Enabled = false;
                this.BalanceLabel.Enabled = false;
            }
            else
            {
                this.TemplateHeaderPanel.Enabled = true;
                this.ErrorAmountTotalTextBox.Enabled = true;
                this.ValidEntriesTotalTextBox.Enabled = true;
                this.OverUnderAmountTextBox.Enabled = true;
                this.BalanceAmountTextBox.Enabled = true;
                this.PaymentsTotalTextBox.Enabled = true;
                this.PaymentEngineUserControl.Enabled = true;
                this.PaymentEngineUserControl.Locked = false || !this.PermissionEdit;
                this.ValidEntriesTotalLabel.Enabled = true;
                this.PaymentsTotalLabel.Enabled = true;
                this.OverUnderLabel.Enabled = true;
                this.BalanceLabel.Enabled = true;
            }
        }

        /// <summary>
        /// Method will Clear the MortgageImport Header Template
        /// </summary>
        /// <param name="clearTemplateFieldsOnly">if set to <c>true</c> [clear template fields only].</param>
        private void ClearHeaderTemplate(bool clearTemplateFieldsOnly)
        {
            ////template related fields
            this.TemplateNameTextBox.Text = string.Empty;
            this.TemplateIdTextBox.Text = string.Empty;
            this.ImportIdTextBox.Text = string.Empty;
            this.SourceTypeTextBox.Text = string.Empty;
            this.FilePathTextBox.Text = string.Empty;

            if (!clearTemplateFieldsOnly)
            {
                this.EntriesTotalTextBox.Text = string.Empty;
                ////assign default value on creating new record
                if (this.PageMode.Equals(PageModeTypes.New))
                {
                    // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                    this.RecieptDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                    this.InterestDateTextBox.Text = TerraScanCommon.InterestDate.ToShortDateString();
                }
                else
                {
                    this.RecieptDateTextBox.Text = string.Empty;
                    this.InterestDateTextBox.Text = string.Empty;
                }

                this.PartialPaymentCheckBox.Checked = false;
                this.FirstHalfChkBox.Checked = false;
                this.ErrorAmountTotalTextBox.Text = string.Empty;
            }
        }

        /// <summary>
        /// Method will Clear the MortgageImport Error Listing Fields
        /// </summary>
        private void ClearErrorListingFields()
        {
            this.FillCheckErrorDataTable(null);
            this.ErrorAmountTotalTextBox.Text = "$0.00";
            this.ValidEntriesTotalTextBox.Text = "$0.00";
            this.OverUnderAmountTextBox.Text = "$0.00";
            this.PaymentEngineUserControl.BalanceAmount = Decimal.Zero;
        }

        /// <summary>
        /// Method will Reset the Buttons in the MortgageImport Form
        /// </summary>
        private void ResetStatusButtons()
        {
            this.LockMortgageImportFields(false);
            this.CheckErrorCountStatusLabel.Text = String.Empty;
            this.ImportFileCountStatusLabel.Text = String.Empty;
            ////reset Import file status
            this.ImportFileButton.Enabled = false;
            ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
            this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.BeforeStatus;
            ////reset Import file status
            this.CheckErrorButton.Enabled = false;
            ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
            this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
            ////reset Import file status
            this.CreateReceiptButton.Enabled = false;
            ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
            this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
            ////reset Import file status
            // this.PrintReceiptButton.Enabled = false;
            //ChangeStatusLabel(this.PrintReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
            this.mortgageImportFieldsInstance.PrintReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
            this.ChangeStatusRelatedFields();
            ////todo: this.AttachmentButton.Text = SharedFunctions.GetResourceString("AttachmentText");
            ////todo: this.CommentButton.Text = SharedFunctions.GetResourceString("CommentText");
            ////todo:  newthis.SetButtonText (this,new DataEventArgs<AdditionalOperationCountEntity> (F1010WorkItem.AdditionOperationText (
        }

        /// <summary>
        /// Method will Reset the values in the MortgageImport Form 
        /// </summary>
        private void ClearOtherFields()
        {
            this.EnableButtons(this, new DataEventArgs<AdditionalOperationEntity>(new AdditionalOperationEntity(0, 0)));
            ////todo: this.AttachmentButton.Text = SharedFunctions.GetResourceString("AttachmentText");

            ////todo: this.CommentButton.Text = SharedFunctions.GetResourceString("CommentText");

            this.footerSmartPart.KeyId = null;
            ////this.ImportAuditLinkLabel.Text = SharedFunctions.GetResourceString("ImportAuditLink");
            ////this.ImportAuditLinkLabel.Enabled = false;
            this.SetActiveRecord(this, new DataEventArgs<int>(0));
            this.SetRecordCount(this, new DataEventArgs<int>(0));

            ////reset navigation buttons
            int[] recordPointerArray = new int[2];
            recordPointerArray[0] = 0;
            recordPointerArray[1] = this.totalImportCount;
            this.SetActiveRecordButtons(this, new DataEventArgs<int[]>(recordPointerArray));
            ////todo: dbt this.SetNavigationButtonsProperty(0);

            ////disable required fields
            this.DeleteFileEntriesButton.Enabled = false;
            this.ReportButton.Enabled = false;
            this.IncludeNecessaryEventHandler(true);
            this.mortgageImportFieldsInstance = new MortgageImportFields();
        }

        #endregion

        #region Process Status Releted Events

        /// <summary>
        /// Method will Change the Buttons Property in the MortgageImport Form with mode change
        /// </summary>
        private void ChangeStatusRelatedFields()
        {
            if (this.mortgageImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                this.OverUnderAmountTextBox.Text = "0";
                this.PaymentEngineUserControl.AllowOverUnder = true;
                this.PaymentEngineUserControl.LoadPayment(this.mortgageImportFieldsInstance.PpaymentId);
                this.LockMortgageImportFields(true);
                this.ImportFileCountStatusLabel.Text = String.Concat(this.mortgageImportFieldsInstance.ImportedEntries, SharedFunctions.GetResourceString("ImportFileCountDisplay"));
                this.CheckErrorCountStatusLabel.Text = string.Concat(this.mortgageImportFieldsInstance.ErrorEntries, " out of ", this.mortgageImportFieldsInstance.ImportedEntries, " entries were in error");
                // this.PrintReceiptButton.Enabled = true && this.PermissionEdit;
                this.ViewReceiptsButton.Enabled = true;
            }
            else
            {
                if (this.mortgageImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.EntriesTotalTextBox.Text = string.Empty;
                    this.ImportFileCountStatusLabel.Text = string.Empty;
                    this.DeleteFileEntriesButton.Enabled = false;
                    this.FilePathTextBox.LockKeyPress = false || !this.PermissionEdit;
                    this.FilePathButton.Enabled = true && this.PermissionEdit;
                    this.TemplateNameButton.Enabled = true && this.PermissionEdit;
                    this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    this.CheckErrorButton.Enabled = false;
                }
                else if (this.mortgageImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    this.ImportFileCountStatusLabel.Text = String.Concat(this.mortgageImportFieldsInstance.ImportedEntries, SharedFunctions.GetResourceString("ImportFileCountDisplay"));
                    this.DeleteFileEntriesButton.Enabled = true && this.PermissionFiled.deletePermission;
                    this.FilePathTextBox.LockKeyPress = true;
                    this.FilePathButton.Enabled = false;
                    this.TemplateNameButton.Enabled = false;
                    this.CheckErrorButton.Enabled = true && this.PermissionEdit;
                }

                if (this.mortgageImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ////this.PaymentEngineUserControl.Locked = true;
                    ////this.PaymentEngineUserControl.LoadPayment(mortgageImportFieldsInstance.PpaymentId);
                    ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.CheckErrorCountStatusLabel.Text = string.Empty;
                    this.ClearErrorListingFields();
                    this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    this.CreateReceiptButton.Enabled = false;
                    this.ReportButton.Enabled = false;
                }
                else if (this.mortgageImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    this.CheckErrorCountStatusLabel.Text = string.Concat(this.mortgageImportFieldsInstance.ErrorEntries, " out of ", this.mortgageImportFieldsInstance.ImportedEntries, " entries were in error");
                    this.ReportButton.Enabled = true;
                }

                if (this.mortgageImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.mortgageImportFieldsInstance.PrintReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    // this.PrintReceiptButton.Enabled = false;
                    this.ViewReceiptsButton.Enabled = false;
                }

                /*if (this.mortgageImportFieldsInstance.PrintReceiptStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                {
                    ChangeStatusLabel(this.PrintReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                }*/
            }
        }

        /// <summary>
        /// Locks the mortgage import after creatinf receipt
        /// </summary>
        /// <param name="lockFields">bool to lock or release</param>
        private void LockMortgageImportFields(bool lockFields)
        {
            if (lockFields)
            {
                ////disable status buttons
                this.ImportFileButton.Enabled = false;
                this.CreateReceiptButton.Enabled = false;
                this.CheckErrorButton.Enabled = false;

                this.PaymentEngineUserControl.Locked = true;
                this.DeleteFileEntriesButton.Enabled = false;
                this.TemplateNameButton.Enabled = false;
                this.FilePathTextBox.LockKeyPress = true;
                this.FilePathButton.Enabled = false;
                this.ReportButton.Enabled = true;
                this.ReceiptDateButton.Enabled = false;
                this.InterestDateButton.Enabled = false;
                this.PartialPaymentCheckBox.Enabled = false;
                this.FirstHalfChkBox.Enabled = false;
                this.RecieptDateTextBox.LockKeyPress = true;
                this.InterestDateTextBox.LockKeyPress = true;
            }
            else
            {
                this.PaymentEngineUserControl.Locked = false || !this.PermissionEdit;
                this.ReceiptDateButton.Enabled = true && this.PermissionEdit;
                this.InterestDateButton.Enabled = true && this.PermissionEdit;
                this.PartialPaymentCheckBox.Enabled = true && this.PermissionEdit;
                this.FirstHalfChkBox.Enabled = true && this.PermissionEdit;
                this.RecieptDateTextBox.LockKeyPress = false || !this.PermissionEdit;
                this.InterestDateTextBox.LockKeyPress = false || !this.PermissionEdit;
            }
        }

        #endregion

        #region Balance Calculation

        /// <summary>
        /// Handles the Text Changed event of the ValidEntriesTotalTextBox control - calculate balance
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ValidEntriesTotalTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Calculatings the balance.
        /// </summary>
        private void CalculateBalance()
        {
            decimal balance = 0;
            decimal validEntries = 0;
            decimal paymentTotal = 0;
            decimal overUnderAmount = 0;

            ////calculating total balance

            if (Decimal.TryParse(this.ValidEntriesTotalTextBox.Text, NumberStyles.Currency, null, out validEntries) && Decimal.TryParse(this.PaymentsTotalTextBox.Text, NumberStyles.Currency, null, out paymentTotal) && Decimal.TryParse(this.OverUnderAmountTextBox.Text, NumberStyles.Currency, null, out overUnderAmount))
            {
                balance = validEntries - (overUnderAmount + paymentTotal);
                this.BalanceAmountTextBox.Text = balance.ToString();
                this.PaymentEngineUserControl.BalanceAmount = this.BalanceAmountTextBox.DecimalTextBoxValue;
            }

            ////changing colors of the BalanceAmountTextBox background, depending on balance
            #region TSCO # 21806 - Enable create receipt button after check for error
            if (balance == 0)
            {

                if (validEntries > 0 && this.mortgageImportFieldsInstance.CreateReceiptStatus.Equals(TerraScanCommon.StatusAction.BeforeStatus))
                //if (validEntries > 0 )
                {
                    this.CreateReceiptButton.Enabled = true && this.PermissionEdit;
                }
                else
                {
                    this.CreateReceiptButton.Enabled = false;
                }

                this.BalanceAmountTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceAmountTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.CreateReceiptButton.Enabled = false;
                this.BalanceAmountTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceAmountTextBox.ForeColor = Color.White;
            }
            if (paymentTotal == 0 && validEntries > 0)
            {
                this.CreateReceiptButton.Enabled = true;
            }
            #endregion


        }

        #endregion

        /// <summary>
        /// set focus to the next/previous input field  
        /// </summary>
        /// <param name="tempControl">control to start the search</param>
        /// <param name="forword">if true retrieve next control ,else retrieve previous control</param>
        /// <param name="considerTabStop">if set to <c>true</c> [consider tab stop].</param>
        private void FocusRequiredInputField(Control tempControl, bool forword, bool considerTabStop)
        {
            Control focusingControl = tempControl;
            bool tabStop;

            if (focusingControl != null)
            {
                while (true)
                {
                    focusingControl = this.GetNextControl(focusingControl, forword);

                    if (focusingControl == null)
                    {
                        tempControl.Focus();
                        break;
                    }
                    else
                    {
                        if (considerTabStop)
                        {
                            tabStop = focusingControl.TabStop;
                        }
                        else
                        {
                            tabStop = true;
                        }

                        if (focusingControl.Enabled && tabStop && focusingControl.Tag != null && focusingControl.Tag.Equals(InputValueField))
                        {
                            this.GetContainerControl().ActivateControl(focusingControl);
                            focusingControl.Focus();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// set focus to the previous input field  
        /// </summary>
        private void FocusPreviousInputField()
        {
            if (this.PreviousActiveControl != null)
            {
                if (this.GetContainerControl().ActivateControl(this.PreviousActiveControl))
                {
                    this.ActiveControl.Focus();
                }
                else
                {
                    this.PreviousActiveControl.Focus();
                }
            }
            else
            {
                this.FocusRequiredInputField(this.CurrentActiveControl, true, true);
            }
        }

        /// <summary>
        /// Method will Check the Requirred Fields in the TemplateHeader
        /// </summary>
        /// <returns>Required Control</returns>
        private Control CheckRequiredFields()
        {
            Control requiredField = null;
            if (string.IsNullOrEmpty(this.TemplateIdTextBox.Text.Trim()))
            {
                requiredField = this.TemplateNameButton;
            }
            else if (string.IsNullOrEmpty(this.TemplateNameTextBox.Text.Trim()))
            {
                requiredField = this.TemplateNameButton;
            }
            else if (string.IsNullOrEmpty(this.RecieptDateTextBox.Text.Trim()))
            {
                requiredField = this.ReceiptDateButton;
            }
            else if (string.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                requiredField = this.InterestDateButton;
            }
            //else if (string.IsNullOrEmpty(this.PartialPaymentCheckBox.Text.Trim()))
            //{
            //    requiredField = this.PartialPaymentCheckBox;
            //}
            else if (string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
            {
                requiredField = this.FilePathButton;
            }

            return requiredField;
        }

        #region Import File

        /// <summary>
        /// Handles the Click event of the ImportFileButton control - contains import file functionality
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ImportFileButton_Click(object sender, EventArgs e)
        {
            if (this.mortgageImportFieldsInstance.ImportFileStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
            {
                if (!(MessageBox.Show(SharedFunctions.GetResourceString("ImportFileMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    return;
                }
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                string filePath = this.FilePathTextBox.Text.Trim();
                TextReader textReader = null;
                string tempValue = string.Empty;
                int lineIndex = 1;

                if (File.Exists(filePath))
                {
                    textReader = new StreamReader(File.OpenRead(filePath));
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ImportFileErrorMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.FilePathButton.Focus();
                    this.Cursor = Cursors.Default;
                    return;
                }

                ////fill importFileDetailsDictionary 
                this.GetImportFileInputDetails();
                ////get current field values
                this.ValidateMortgageImportFields();
                ////change status for the import process            
                ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.ProcessStatus, null, null);

                this.importFileDataSet.Clear();
                while (true)
                {
                    tempValue = textReader.ReadLine();

                    if (tempValue == null)
                    {
                        break;
                    }

                    if (!String.IsNullOrEmpty(tempValue.Trim()))
                    {
                        this.GenerateMortgageImportEntryRecord(tempValue, lineIndex);
                    }

                    lineIndex++;
                }

                textReader.Close();

                MortageImportData tempDataSet;
                string errorMessage = string.Empty;

                if (this.importFileDataSet.MortgageImportEntry.Rows.Count > 0)
                {
                    //first half value only
                    if (this.FirstHalfChkBox.Checked)
                    {
                        this.mortgageImportFieldsInstance.FirstHalfPayCode = 1;
                    }
                    else
                    {
                        this.mortgageImportFieldsInstance.FirstHalfPayCode = 0;
                    }

                    ////mortgageImportFieldsInstance.PpaymentId = PaymentEngineUserControl.CreatePayment(0);
                    tempDataSet = F1010WorkItem.SaveMortgageImportEntries(this.mortgageImportFieldsInstance.ImportId, this.mortgageImportFieldsInstance.TemplateId, this.mortgageImportFieldsInstance.TemplateName, this.mortgageImportFieldsInstance.SourceTypeId, this.mortgageImportFieldsInstance.FilePath, this.mortgageImportFieldsInstance.ReceiptDate, this.mortgageImportFieldsInstance.InterestDate, this.mortgageImportFieldsInstance.PayCode, TerraScanCommon.UserId, this.mortgageImportFieldsInstance.RollYear, this.mortgageImportFieldsInstance.PpaymentId,this.mortgageImportFieldsInstance.FirstHalfPayCode, this.importFileDataSet.MortgageImportEntry);

                    if (tempDataSet.Tables.Count > 0 && tempDataSet.SaveMortgageImportEntryError.Count > 0)
                    {
                        errorMessage = tempDataSet.SaveMortgageImportEntryError.Rows[0][tempDataSet.SaveMortgageImportEntryError.ErrorMsgColumn].ToString();
                        tempDataSet.Tables.Remove(tempDataSet.SaveMortgageImportEntryError);
                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            ////updates necessary fields - result of import file process
                            if (tempDataSet.Tables.Count > 0 && tempDataSet.SaveMortgageImportEntry.Rows.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(tempDataSet.SaveMortgageImportEntry.Rows[0][tempDataSet.SaveMortgageImportEntry.ImportedEntriesTotalColumn].ToString()))
                                {
                                    this.EntriesTotalTextBox.Text = Convert.ToDecimal(tempDataSet.SaveMortgageImportEntry.Rows[0][tempDataSet.SaveMortgageImportEntry.ImportedEntriesTotalColumn]).ToString("$#,##0.00");
                                }
                                this.mortgageImportFieldsInstance.ImportedEntries = this.importFileDataSet.MortgageImportEntry.Rows.Count.ToString();
                                this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.AfterStatus;
                                ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.AfterStatus, tempDataSet.SaveMortgageImportEntry.Rows[0][tempDataSet.SaveMortgageImportEntry.ImportRunDtColumn].ToString(), tempDataSet.SaveMortgageImportEntry.Rows[0][tempDataSet.SaveMortgageImportEntry.ImportRunByColumn].ToString());
                                this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                                this.ChangeStatusRelatedFields();
                                this.PageMode = PageModeTypes.View;
                                this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            }
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.FillImportFormDetails(tempDataSet, false);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("ImportFileEmpty"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.BeforeStatus;
                    ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    this.ChangeStatusRelatedFields();
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show("File is being used by another process.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Gets the input file details of the importing record
        /// </summary>
        private void GetImportFileInputDetails()
        {
            DataTable mortgageImportTemplateDataTable = F1010WorkItem.GetMortgageTemplate(this.mortgageImportFieldsInstance.TemplateId).GetMortgageImportTemplate;

            if (mortgageImportTemplateDataTable.Rows.Count > 0)
            {
                this.SourceTypeTextBox.Text = mortgageImportTemplateDataTable.Rows[0]["TypeName"].ToString();
                if (!string.IsNullOrEmpty(mortgageImportTemplateDataTable.Rows[0]["TypeID"].ToString()))
                {
                    this.mortgageImportFieldsInstance.SourceTypeId = Convert.ToInt16(mortgageImportTemplateDataTable.Rows[0]["TypeID"]);
                }

                switch (this.mortgageImportFieldsInstance.SourceTypeId)
                {
                    case 1:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.FixedWidth;
                        break;
                    case 2:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.CommaDelimited;
                        break;
                }

                if (this.importFileSourceType.Equals(TerraScanCommon.ImportFileSourceType.FixedWidth))
                {
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.StatementIDColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["StatementID_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["StatementID_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.StatementNumberColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["StatementNum_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["StatementNum_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.ParcelNumberColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["ParcelNumber_pos"], "~", mortgageImportTemplateDataTable.Rows[0]["ParcelNumber_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.AmountColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["Amount_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["Amount_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.CommentColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["Comment_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["Comment_Wid"]);
                    ////this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.BankCodeColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["BankCode_pos"], "~", mortgageImportTemplateDataTable.Rows[0]["BankCode_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.LoanNumColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["LoanNum_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["LoanNum_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.TaxPayerNameColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["TaxPayName_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["TaxPayName_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.PostTypeColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["PostType_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["PostType_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.PostTypeColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["PostType_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["PostType_Wid"]);
                    ////Coding Added for the co 6498 by malliga
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.OwnerIDColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["OwnerID_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["OwnerID_Wid"]);
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.OwnerIDColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["OwnerID_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["OwnerID_Wid"]);

                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.AmountCharColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["Amount_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["Amount_Wid"]);

                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.CartIDColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["CartID_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["CartID_Wid"]);
                }
                else
                {
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.StatementIDColumn] = mortgageImportTemplateDataTable.Rows[0]["StatementID_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.StatementNumberColumn] = mortgageImportTemplateDataTable.Rows[0]["StatementNum_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.ParcelNumberColumn] = mortgageImportTemplateDataTable.Rows[0]["ParcelNumber_pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.AmountColumn] = mortgageImportTemplateDataTable.Rows[0]["Amount_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.CommentColumn] = mortgageImportTemplateDataTable.Rows[0]["Comment_Pos"];
                    ////this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.BankCodeColumn] = mortgageImportTemplateDataTable.Rows[0]["BankCode_pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.LoanNumColumn] = mortgageImportTemplateDataTable.Rows[0]["LoanNum_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.TaxPayerNameColumn] = mortgageImportTemplateDataTable.Rows[0]["TaxPayName_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.PostTypeColumn] = mortgageImportTemplateDataTable.Rows[0]["PostType_Pos"];

                    ////Coding Added for the co 6498 by malliga
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.OwnerIDColumn] = mortgageImportTemplateDataTable.Rows[0]["OwnerID_Pos"];
                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.AmountCharColumn] = String.Concat(mortgageImportTemplateDataTable.Rows[0]["Amount_Pos"], "~", mortgageImportTemplateDataTable.Rows[0]["Amount_Wid"]);

                    this.importFileDetailsDictionary[this.importFileDataSet.MortgageImportEntry.CartIDColumn] = mortgageImportTemplateDataTable.Rows[0]["CartID_Pos"];
                }
            }
        }

        /// <summary>
        /// Generate the mortgage import empty record for each line in the file
        /// </summary>
        /// <param name="fileLine">The line in the imported file</param>
        /// <param name="lineIndex">The index of the file line</param>
        private void GenerateMortgageImportEntryRecord(string fileLine, int lineIndex)
        {
            //// used to store import file record details
            IDictionary mortgageImportDictionary = new ListDictionary();
            //// used to navigate through the dictionary
            IDictionaryEnumerator fileDetailsEnumerator = this.importFileDetailsDictionary.GetEnumerator();

            ////to store temporary values
            string tempValue = string.Empty;
            ////used to store the values in mortgage import entry table fields
            object fieldValue = null;
            ////temporary value used to store column name of the mortgage import entry table fields
            string columnName = string.Empty;
            ////temporary array
            string[] commaDelimitedArray = null;
            string[] arrValue = new string[2];

            int fieldWidth = -1;
            int position = -1;
            int stringLength = fileLine.Length;

            ////gets the position value for comma delimited files
            if (this.importFileSourceType.Equals(TerraScanCommon.ImportFileSourceType.CommaDelimited))
            {
                string[] tempArr = fileLine.Split(new char[] { ',' });
                Array.Resize(ref commaDelimitedArray, tempArr.Length);
                tempArr.CopyTo(commaDelimitedArray, 0);
            }

            ////navigate to find the position and width of the mortgage import entry table fields
            while (fileDetailsEnumerator.MoveNext())
            {
                ////clear temp values
                tempValue = string.Empty;
                fieldWidth = -1;
                position = -1;

                ////checks for null value 
                if (!Object.Equals(fileDetailsEnumerator.Value, null))
                {
                    columnName = fileDetailsEnumerator.Key.ToString();

                    ////split value - contains both position and width
                    arrValue = fileDetailsEnumerator.Value.ToString().Split(new char[] { '~' });
                    if (!String.IsNullOrEmpty(arrValue[0]))
                    {
                        position = Convert.ToInt16(arrValue[0]);
                    }

                    ////extract field value according to the source type
                    switch (this.importFileSourceType)
                    {
                        case TerraScanCommon.ImportFileSourceType.FixedWidth:
                            position = position - 1;
                            if (position > -1 && position < stringLength)
                            {
                                if (arrValue.Length > 1)
                                {
                                    if (!String.IsNullOrEmpty(arrValue[1]))
                                    {
                                        fieldWidth = Convert.ToInt16(arrValue[1]);
                                    }

                                    if (fieldWidth > -1)
                                    {
                                        if ((fieldWidth + position) > stringLength)
                                        {
                                            fieldWidth = stringLength - position;
                                        }

                                        tempValue = fileLine.Substring(position, fieldWidth);
                                    }
                                }
                            }

                            break;
                        case TerraScanCommon.ImportFileSourceType.CommaDelimited:
                            if (position > 0 && position <= commaDelimitedArray.Length)
                            {
                                tempValue = commaDelimitedArray.GetValue(position - 1).ToString();
                            }

                            break;
                    }

                    ////validate the values for datatype
                    fieldValue = ValidateFieldValue(tempValue.Trim(), this.importFileDataSet.MortgageImportEntry.Columns[columnName].DataType);
                    ////adds columnname and corresponding values to temporary collection
                    mortgageImportDictionary[columnName] = fieldValue;
                }
            }

            ////insert mortgage import entry record to the importfiledataset
            this.importFileDataSet.InsertMortgageImportEntry(mortgageImportDictionary, lineIndex);
        }

        #endregion

        #region New Mortgage Import

        /// <summary>
        /// Handles the Click event of the NewButton control.
        /// </summary>
        private void NewButton_Click()
        {
            try
            {
                this.PageMode = PageModeTypes.New;
                this.CurrentImportId = -1;
                this.isLoaded = true;
                ////set default buttons
                this.SetButtons(TerraScanCommon.ButtonActionMode.NewMode);
                this.ClearMortageImportDetails();
                this.FocusRequiredInputField(this.TemplateHeaderPanel, true, true);
                this.SetAttachmentCommentsCount();
                ////set interest date to today
                this.mortgageImportFieldsInstance.InterestDate = DateTime.Today;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Check For Errors

        /// <summary>
        /// Handles the Click Event for the CheckErrorButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void CheckErrorButton_Click(object sender, EventArgs e)
        {
            try
            {
                MortageImportData tempDataSet = new MortageImportData();
                this.Cursor = Cursors.WaitCursor;
                ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.ProcessStatus, null, null);

                ////save values to database
                this.ValidateMortgageImportFields();

                //priya
                //this.CreateReceiptButton.Enabled=true;

                ////mortgageImportFieldsInstance.PpaymentId = this.PaymentEngineUserControl.CreatePayment(0);

                tempDataSet = F1010WorkItem.MortgageImportCheckErrors(this.CurrentImportId, this.mortgageImportFieldsInstance.TemplateId, this.mortgageImportFieldsInstance.TemplateName, this.mortgageImportFieldsInstance.SourceTypeId, this.mortgageImportFieldsInstance.FilePath, this.mortgageImportFieldsInstance.ReceiptDate, this.mortgageImportFieldsInstance.InterestDate, this.mortgageImportFieldsInstance.PayCode, TerraScanCommon.UserId, this.mortgageImportFieldsInstance.RollYear, this.mortgageImportFieldsInstance.FirstHalfPayCode, this.mortgageImportFieldsInstance.PpaymentId);
                string errorMessage = string.Empty;

                if (tempDataSet.Tables.Count > 0 && tempDataSet.CheckMortgageImportCheckErrors.Rows.Count > 0)
                {
                    errorMessage = tempDataSet.CheckMortgageImportCheckErrors.Rows[0][tempDataSet.CheckMortgageImportCheckErrors.ErrorMsgColumn].ToString();
                    tempDataSet.Tables.Remove(tempDataSet.CheckMortgageImportCheckErrors);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        if (tempDataSet.Tables.Count > 1 && tempDataSet.CheckMortgageImportErrorDetails.Rows.Count > 0)
                        {
                            this.FillCheckErrorDataTable(tempDataSet.GetMortgageImportError);
                            if (tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorTotalColumn] != null)
                            {
                                if (!string.IsNullOrEmpty(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorTotalColumn].ToString()))
                                {
                                    this.ErrorAmountTotalTextBox.Text = Convert.ToDecimal(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorTotalColumn]).ToString();
                                }
                            }
                            if (tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ValidTotalColumn] != null)
                            {
                                if (!string.IsNullOrEmpty(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ValidTotalColumn].ToString()))
                                {
                                    this.ValidEntriesTotalTextBox.Text = Convert.ToDecimal(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ValidTotalColumn]).ToString();
                                }
                            }
                            if (tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.OverUnderAmountColumn] != null)
                            {
                                if (!string.IsNullOrEmpty(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.OverUnderAmountColumn].ToString()))
                                {
                                    this.OverUnderAmountTextBox.Text = Convert.ToDecimal(tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.OverUnderAmountColumn]).ToString();
                                }
                            }
                            this.mortgageImportFieldsInstance.ErrorEntries = tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorEntriesColumn].ToString();
                            this.mortgageImportFieldsInstance.ImportedEntries = tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.TotalEntriesColumn].ToString();
                            this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.AfterStatus;
                            ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.AfterStatus, tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorRunDtColumn].ToString(), tempDataSet.CheckMortgageImportErrorDetails.Rows[0][tempDataSet.CheckMortgageImportErrorDetails.ErrorRunByColumn].ToString());
                            //Removed the tender Type default 'check' TSCO #13410.
                            ///used to load Tender Type "CHECK"
                            //string error=this.mortgageImportFieldsInstance.ErrorEntries.ToString() ;
                            //if (error=="0")
                            //{
                            //    this.PaymentEngineUserControl.LoadDefaultValue(string.Empty, this.BalanceAmountTextBox.DecimalTextBoxValue);
                            //}
                            ////this.PaymentEngineUserControl.LoadPayment(this.mortgageImportFieldsInstance.PpaymentId);
                            this.PageMode = PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        }
                        else
                        {
                            this.FillCheckErrorDataTable(null);
                            this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                            ChangeStatusLabel(this.CheckErrorStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                        }

                        this.ChangeStatusRelatedFields();


                    }
                    else
                    {
                        MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.FillImportFormDetails(tempDataSet, false);
                    }
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
        /// Handles the Column Header Mouse Click Event for the ErrorGridView
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ErrorGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ////if (e.Clicks % 2 != 0)
            ////{
            ////    DataGridViewColumn newSortedColumn = this.ErrorGridView.Columns[e.ColumnIndex];
            ////   DataGridViewColumn oldSortedColumn = this.ErrorGridSortedColumn;
            ////    GridSortOrder gridSortOrder;

            ////    // If oldSortedColumn is null, then the ErrorGridView is not sorted.
            ////   if (oldSortedColumn != null)
            ////    {
            ////        // Sort the same column again, reversing the SortOrder.
            ////        if (oldSortedColumn == newSortedColumn && this.ErrorGridSortOrder.Equals(GridSortOrder.Asc))
            ////        {
            ////            gridSortOrder = GridSortOrder.Desc;
            ////        }
            ////        else
            ////        {
            ////            // Sort a new column and remove the old SortGlyph.
            ////            gridSortOrder = GridSortOrder.Asc;
            ////            oldSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.None;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        gridSortOrder = GridSortOrder.Asc;
            ////    }

            ////    // Sort the selected column.
            ////    this.errorCheckDataTable.DefaultView.Sort = String.Concat("EmptyRecord ASC, ", newSortedColumn.DataPropertyName, " ", gridSortOrder.ToString());
            ////    if (gridSortOrder.Equals(GridSortOrder.Asc))
            ////    {
            ////        newSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            ////    }
            ////    else
            ////    {
            ////        newSortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
            ////    }

            ////    this.ErrorGridSortedColumn = newSortedColumn;
            ////    this.ErrorGridSortOrder = gridSortOrder;
            ////    this.ErrorGridView.Refresh();
            ////}
        }

        /// <summary>
        /// Filldts the Check Errors.
        /// </summary>
        /// <param name="tempErrorCheckDataTable">The dt check error grid.</param>
        private void FillCheckErrorDataTable(MortageImportData.GetMortgageImportErrorDataTable tempErrorCheckDataTable)
        {
            try
            {
                ////reset error grid related values
                this.ErrorGridView.CurrentCell = null;
                ////this.errorCheckDataTable.DefaultView.Sort = "EmptyRecord ASC";
                this.ErrorGridSortedColumn = null;
                this.ErrorGridSortOrder = GridSortOrder.Asc;
                this.ErrorGridView.ClearSorting();

                if (tempErrorCheckDataTable != null && tempErrorCheckDataTable.Rows.Count > 0)
                {
                    this.errorCheckDataTable.Clear();

                    ////Code Commented for the issue 3023 by malliga on 3/3/2010
                    //// this.ErrorGridView.Enabled = true;

                    for (int counter = 0; counter < tempErrorCheckDataTable.Rows.Count; counter++)
                    {
                        MortageImportData.GetMortgageImportErrorRow dr = (MortageImportData.GetMortgageImportErrorRow)this.errorCheckDataTable.NewRow();
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.StatementIDColumn].ToString()))
                        {
                            dr.StatementID = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.StatementIDColumn]);
                        }
                        dr.StatementNumber = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.StatementNumberColumn].ToString().Trim();
                        ////dr.BankCode = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.BankCodeColumn].ToString().Trim();
                        dr.ParcelNumber = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.ParcelNumberColumn].ToString().Trim();
                        dr.ErrorStatus = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.ErrorStatusColumn].ToString().Trim();
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.AmountColumn].ToString()))
                        {
                            dr.Amount = Convert.ToDouble(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.AmountColumn]);
                        }
                        //// dr.Amount = tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.AmountColumn].ToString().Trim();
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.DueAmountColumn].ToString()))
                        {
                            dr.DueAmount = Convert.ToDecimal(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.DueAmountColumn]);
                        }
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.FileLineColumn].ToString()))
                        {
                            dr.FileLine = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.FileLineColumn]);
                        }
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.EntryIDColumn].ToString()))
                        {
                            dr.EntryID = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.EntryIDColumn]);
                        }
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.StatusIDColumn].ToString()))
                        {
                            dr.StatusID = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.StatusIDColumn]);
                        }
                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.PostTypeColumn].ToString()))
                        {
                            dr.PostType = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.PostTypeColumn].ToString().Trim());
                        }

                        if (!string.IsNullOrEmpty(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.OwnerIDColumn].ToString().Trim()))
                        {
                            dr.OwnerID = Convert.ToInt32(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.OwnerIDColumn].ToString().Trim());
                        }

                        ////dr.am = Convert.ToDouble(tempErrorCheckDataTable.Rows[counter][tempErrorCheckDataTable.AmountColumn]);

                        this.errorCheckDataTable.Rows.Add(dr);
                    }
                }
                else
                {
                    DataView tempDataView = new DataView(this.errorCheckDataTable.DefaultView.ToTable());
                    tempDataView.RowFilter = string.Concat(this.errorCheckDataTable.FileLineColumn.ColumnName, "> 0");
                    if (tempDataView.Count > 0)
                    {
                        this.errorCheckDataTable.Clear();
                    }

                    ////Code Commented for the issue 3023 by malliga on 3/3/2010
                    ////this.ErrorGridView.Enabled = false;
                }

                this.ErrorGridView.CreateEmptyRows();

                // this.ErrorGridView.Refresh();
                this.ErrorGridView.CurrentCell = null;

                if (this.errorCheckDataTable.Rows.Count == 5)
                {
                    this.ErrorGridVscrollBar.Visible = true;
                }
                else
                {
                    this.ErrorGridVscrollBar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the  ErrorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ErrorGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column
                if (e.ColumnIndex == this.ErrorGridView.Columns["PayAmt"].Index)
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
                            e.Value = "0.00 ";
                        }

                        ////Check for due amount mismatch
                        if (this.ErrorGridView["StatusID", e.RowIndex].Value != null && string.Equals(this.ErrorGridView["StatusID", e.RowIndex].Value.ToString(), "5") && this.ErrorGridView["DueAmount", e.RowIndex].Value != null)
                        {
                            val = this.ErrorGridView["DueAmount", e.RowIndex].Value.ToString();
                            if (Decimal.TryParse(val, out outDecimal))
                            {
                                this.ErrorGridView["PayAmt", e.RowIndex].ToolTipText = string.Concat(SharedFunctions.GetResourceString("AmountDue"), outDecimal.ToString("#,##0.00"));
                            }
                            else
                            {
                                this.ErrorGridView["PayAmt", e.RowIndex].ToolTipText = string.Empty;
                            }
                        }
                        else
                        {
                            this.ErrorGridView["PayAmt", e.RowIndex].ToolTipText = string.Empty;
                        }
                    }
                    else
                    {
                        e.Value = "";
                    }
                }

                #region Bug #6553: TSCO - 1010 Mortgage Import - Add Statement Number Hyperlink to error grid

                ////Format StatementNumber value based on the availability of both StatementID and StatementNumber
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.ErrorGridView.Columns[e.ColumnIndex].Name.Equals(this.errorCheckDataTable.StatementNumberColumn.ColumnName))
                {
                    if (ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value != null && !string.IsNullOrEmpty(ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value.ToString()) && !ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementIDColumn.ColumnName].Value.Equals(0))
                    {
                        ////Format for StatementNumber Link
                        (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkBehavior = LinkBehavior.SystemDefault;

                        if (this.ErrorGridView.Rows[e.RowIndex].Selected || this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Selected)
                        {
                            ////On row/cell selection set ForeColor as White
                            this.SetForeColor(e.RowIndex);
                        }
                        else
                        {
                            (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.Blue;
                            (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.FromArgb(128, 0, 128);
                        }
                    }
                    else
                    {
                        ////Format for StatementNumber Text
                        (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkBehavior = LinkBehavior.NeverUnderline;

                        if (this.ErrorGridView.Rows[e.RowIndex].Selected || this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Selected)
                        {
                            ////On row/cell selection set ForeColor as White
                            this.SetForeColor(e.RowIndex);
                        }
                        else
                        {
                            (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.FromArgb(102, 102, 102);
                            (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.FromArgb(102, 102, 102);
                        }

                        (this.ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkVisited = false;
                    }
                }

                #endregion Bug #6553: TSCO - 1010 Mortgage Import - Add Statement Number Hyperlink to error grid
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the  ErrorGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ErrorGridView_Leave(object sender, EventArgs e)
        {
            try
            {
                ////deselect grid value
                this.ErrorGridView.CurrentCell = null;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region SetForeColor

        /// <summary>
        /// Sets the color of the fore.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void SetForeColor(int rowIndex)
        {
            ////On row/cell selection set StatmentNumber Link ForeColor as White
            (this.ErrorGridView.Rows[rowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.White;
            (this.ErrorGridView.Rows[rowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
            (this.ErrorGridView.Rows[rowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
        }

        #endregion SetForeColor

        #region Save Mortgage Import

        /// <summary>
        /// Handles the Click event of the SaveButton control - saves mortgage import record
        /// </summary>
        private void SaveButton_Click()
        {
            try
            {
                this.SaveMortgageImport(false);
                this.ParentForm.ActiveControl = this.TemplateNameButton;
                this.ParentForm.ActiveControl.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Saves the mortgage import.
        /// </summary>
        /// <param name="close">if set to <c>true</c> [on close].</param>
        /// <returns>true - successful save</returns>
        private bool SaveMortgageImport(bool close)
        {
            Control requiredField = this.CheckRequiredFields();
            if (requiredField == null)
            {
                MortageImportData tempDataSet = this.SaveMortgageImportDetails(false);
                string errorMsg = string.Empty;

                if (this.PageMode.Equals(PageModeTypes.New))
                {
                    if (tempDataSet.Tables.Count > 0 && tempDataSet.SaveMortgageImport.Rows.Count > 0)
                    {
                        errorMsg = tempDataSet.SaveMortgageImport.Rows[0][tempDataSet.SaveMortgageImport.ErrorMsgColumn].ToString();

                        if (String.IsNullOrEmpty(errorMsg))
                        {
                            // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                            TerraScanCommon.InterestDate = this.InterestDateTextBox.DateTextBoxValue;
                            TerraScanCommon.ReceiptDate = this.RecieptDateTextBox.DateTextBoxValue;

                            if (close)
                            {
                                return true;
                            }

                            this.CurrentImportId = Convert.ToInt32(tempDataSet.SaveMortgageImport.Rows[0]["ImportID"]);

                            if (this.CurrentImportId > 0)
                            {
                                this.FillImportFormDetails(null, false);
                            }
                            else
                            {
                                MessageBox.Show("Error in save", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show(errorMsg, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (close)
                            {
                                return false;
                            }

                            this.ClearHeaderTemplate(true);
                            this.FocusRequiredInputField(this.TemplateHeaderPanel, true, true);
                        }
                    }
                }
                else
                {
                    if (tempDataSet.Tables.Count > 0 && tempDataSet.SaveMortgageImport.Rows.Count > 0)
                    {
                        errorMsg = tempDataSet.SaveMortgageImport.Rows[0][tempDataSet.SaveMortgageImport.ErrorMsgColumn].ToString();
                        tempDataSet.Tables.Remove(tempDataSet.SaveMortgageImport);
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            if (close)
                            {
                                return true;
                            }

                            this.FillImportFormDetails(null, false);
                        }
                        else
                        {
                            MessageBox.Show(errorMsg, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (close)
                            {
                                return false;
                            }

                            this.FillImportFormDetails(tempDataSet, false);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                requiredField.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control - saves mortgage import record
        /// </summary>
        /// <param name="resetErrorCheck">The resetErrorCheck which resets error check</param>
        /// <returns>The Dataset from save</returns>
        private MortageImportData SaveMortgageImportDetails(bool resetErrorCheck)
        {
            this.ValidateMortgageImportFields();

            ////mortgageImportFieldsInstance.PpaymentId = this.PaymentEngineUserControl.CreatePayment(0);

            return F1010WorkItem.SaveMortgageImport(this.CurrentImportId, this.mortgageImportFieldsInstance.TemplateId, this.mortgageImportFieldsInstance.TemplateName, this.mortgageImportFieldsInstance.SourceTypeId, this.mortgageImportFieldsInstance.FilePath, this.mortgageImportFieldsInstance.ReceiptDate, this.mortgageImportFieldsInstance.InterestDate, this.mortgageImportFieldsInstance.PayCode, TerraScanCommon.UserId, this.mortgageImportFieldsInstance.RollYear, this.mortgageImportFieldsInstance.PpaymentId, this.mortgageImportFieldsInstance.FirstHalfPayCode, resetErrorCheck);
        }

        /// <summary>
        /// validate mortgage import fields for saving the import record
        /// </summary>        
        private void ValidateMortgageImportFields()
        {
            DateTime validDate;
            try
            {
                if (this.PageMode.Equals(PageModeTypes.New))
                {
                    this.mortgageImportFieldsInstance.ImportId = -1;
                }
                else
                {
                    this.mortgageImportFieldsInstance.ImportId = this.CurrentImportId;
                }

                ////checks for templateid
                this.mortgageImportFieldsInstance.TemplateId = Convert.ToInt32(this.TemplateIdTextBox.Text.Trim());
                ////historical template name needs no validation
                this.mortgageImportFieldsInstance.TemplateName = this.TemplateNameTextBox.Text.Trim();
                ////checks for valid date
                if (!DateTime.TryParse(this.RecieptDateTextBox.Text.Trim(), out validDate))
                {
                    validDate = DateTime.Today;
                }

                this.mortgageImportFieldsInstance.ReceiptDate = validDate;

                if (!DateTime.TryParse(this.InterestDateTextBox.Text.Trim(), out validDate))
                {
                    validDate = DateTime.Today;
                }

                switch (this.mortgageImportFieldsInstance.SourceTypeId)
                {
                    case 1:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.FixedWidth;
                        break;
                    case 2:
                        this.importFileSourceType = TerraScanCommon.ImportFileSourceType.CommaDelimited;
                        break;
                }

                this.mortgageImportFieldsInstance.InterestDate = validDate;

                ////retrieve paycode value
                // this.mortgageImportFieldsInstance.PayCode = this.PartialPaymentCheckBox.SelectedIndex;

                //Commented and modiifed by purushotham
                if (this.PartialPaymentCheckBox.Checked)
                {
                    this.mortgageImportFieldsInstance.PayCode = true;
                }
                else
                {
                    this.mortgageImportFieldsInstance.PayCode = false;
                }

                ////string value checks for existing
                if (!string.IsNullOrEmpty(this.FilePathTextBox.Text.Trim()))
                {
                    this.mortgageImportFieldsInstance.FilePath = this.FilePathTextBox.Text.Trim();
                }

                this.mortgageImportFieldsInstance.RollYear = DateTime.Now.Year;

                //first half value only
                if (this.FirstHalfChkBox.Checked)
                {
                    this.mortgageImportFieldsInstance.FirstHalfPayCode = 1;
                }
                else
                {
                    this.mortgageImportFieldsInstance.FirstHalfPayCode = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Cancel

        /// <summary>
        /// Handles the Click event of the CancelMortageButton control - cancel current process
        /// </summary>
        private void CancelMortageButton_Click()
        {
            try
            {
                ////todo: this.GetContainerControl().ActivateControl(this.CancelMortageButton);
                this.FillImportFormDetails(null, false);
                this.SetFirstFocus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Handles the Click event of the DeleteMortageButton control - delete existing import
        /// </summary>
        private void DeleteMortageButton_Click()
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteMortgageImport"), "TerraScan T2 - Delete Mortgage Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MortageImportData tempDataSet = F1010WorkItem.DeleteMortgageImport(this.CurrentImportId, TerraScanCommon.UserId);
                        string errorMessage = String.Empty;
                        if (tempDataSet.Tables.Count > 0 && tempDataSet.DeleteMortgageImport.Rows.Count > 0)
                        {
                            errorMessage = tempDataSet.DeleteMortgageImport.Rows[0][tempDataSet.DeleteMortgageImport.ErrorMsgColumn].ToString();
                            tempDataSet.Tables.Remove(tempDataSet.DeleteMortgageImport);
                            if (string.IsNullOrEmpty(errorMessage))
                            {
                                int importIndex = this.RetrieveImportIndex() - 1;
                                this.importIds.Rows.RemoveAt(importIndex);
                                this.CurrentImportId = this.RetrieveImportId(importIndex);

                                this.FillImportFormDetails(null, false);
                            }
                            else
                            {
                                MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.FillImportFormDetails(tempDataSet, false);
                            }

                            this.SetAttachmentCommentsCount();
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
                else
                {
                    this.FocusPreviousInputField();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteFileEntriesButton control - delete existing file entries
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteFileEntriesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("DeleteFileEntries"), ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        MortageImportData tempDataSet = F1010WorkItem.DeleteMortgageImportFileEntries(this.CurrentImportId, TerraScanCommon.UserId);

                        string errorMessage = String.Empty;
                        if (tempDataSet.Tables.Count > 0 && tempDataSet.DeleteMortgageImportEntry.Rows.Count > 0)
                        {
                            errorMessage = tempDataSet.DeleteMortgageImportEntry.Rows[0][tempDataSet.DeleteMortgageImportEntry.ErrorMsgColumn].ToString();
                            tempDataSet.Tables.Remove(tempDataSet.DeleteMortgageImportEntry);
                            if (string.IsNullOrEmpty(errorMessage))
                            {
                                this.mortgageImportFieldsInstance.ImportFileStatus = TerraScanCommon.StatusAction.BeforeStatus;
                                ChangeStatusLabel(this.ImportFileStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                                this.ChangeStatusRelatedFields();
                                this.FocusRequiredInputField(this.TemplateHeaderPanel, true, true);
                            }
                            else
                            {
                                MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationDelete, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.FillImportFormDetails(tempDataSet, false);
                            }
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
                else
                {
                    this.FocusPreviousInputField();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Payment Engine Events

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
        {
            try
            {
                this.PaymentsTotalTextBox.Text = amount.ToString("$#,##0.00");
                this.CalculateBalance();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Form Control Events

        #region Record Navigation

        #endregion

        #region Date Related Calender Controls Events

        /// <summary>
        /// Handles the Click Event for the RecieptDate Picture box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void ReceiptDatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                //// Calls the method to show the calender control.
                this.ShowRecieptDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click Event for the InterestDate Picture Box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void InterestDatePictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                //// Calls the method to show the calender control.
                this.ShowInterestDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating Event for MortgageMonth Calander
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event Arg</param>
        private void MortgageMonthCalendar_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                this.MortgageMonthCalendar.Visible = false;
                e.Cancel = true;

                if (this.MortgageMonthCalendar.Tag.Equals(this.ReceiptDateButton.Tag))
                {
                    this.FocusRequiredInputField(this.ReceiptDateButton, true, true);
                }
                else if (this.MortgageMonthCalendar.Tag.Equals(this.InterestDateButton.Tag))
                {
                    this.FocusRequiredInputField(this.InterestDateButton, true, true);
                }

                e.Cancel = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected Event for MortgageMonthCalender
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">DateRangeEvent Argument</param>
        private void MortgageMonthCalander_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.SetSeletedDate(e.Start.ToString(this.dateFormat));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Sets the seleted date.
        /// </summary>
        /// <param name="dateSelected">The date selected.</param>
        private void SetSeletedDate(string dateSelected)
        {
            //// Assign the selected date to the DateTextbox.
            if (this.MortgageMonthCalendar.Tag.ToString() == this.InterestDateButton.Tag.ToString())
            {
                this.MortgageMonthCalendar.Tag = string.Empty;
                this.InterestDateTextBox.Text = dateSelected;
                this.InterestDateTextBox_Validating(this.InterestDateTextBox, new CancelEventArgs());
                InterestDateTextBox.Focus();
            }
            else if (this.MortgageMonthCalendar.Tag.ToString() == this.ReceiptDateButton.Tag.ToString())
            {
                this.MortgageMonthCalendar.Tag = string.Empty;
                this.RecieptDateTextBox.Text = dateSelected;
                RecieptDateTextBox.Focus();
            }

            this.MortgageMonthCalendar.Visible = false;
        }

        /// <summary>
        /// Handles the RecieptDateButton Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ReceiptDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// Calls the method to show the calender control.
                this.ShowRecieptDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click Event for the InterestDate Butoon
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void InterestDateButton_Click(object sender, EventArgs e)
        {
            try
            {
                //// Calls the method to show the calender control.
                this.ShowInterestDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click Event for the Template View Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TemplateViewButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(1011);
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Mortgage Import Template

        /// <summary>
        /// Handles the Click Event for the TemplateName Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void TemplateNameButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.MortgageImportTemplateId = 0;
                Form mortgageImportTemplateSelect = new Form();
                mortgageImportTemplateSelect = this.form1010control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(1015, null, this.F1010control.WorkItem);
                if (mortgageImportTemplateSelect != null)
                {
                    if (mortgageImportTemplateSelect.ShowDialog() == DialogResult.Ignore)
                    {
                        try
                        {
                            FormInfo formInfo;
                            formInfo = TerraScanCommon.GetFormInfo(1011);
                            this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.ManageException(SharedFunctions.GetResourceString("ErrorLoadingForm"), ex, ExceptionManager.ActionType.Display, this.ParentForm);
                        }
                    }

                    //// Gets The TemplateID From Modal Dialog ,By Passing Form name and Property
                    this.MortgageImportTemplateId = Convert.ToInt32(TerraScanCommon.GetValue(mortgageImportTemplateSelect, "MortgageImportTemplateId"));

                    if (this.MortgageImportTemplateId > 0)
                    {
                        this.ChangeMortgageImportStatus();
                        this.FillTemplateDetails(this.MortgageImportTemplateId);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handle MortgageImportTemplateView Close
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">The Event</param>
        private void MortgageTemplateView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MdiWrapper sourceForm = sender as MdiWrapper;
                int index = -1;
                //// Remove from the collection only if the calling form is a MDI child form

                ////todo: index = ChildCollection.IndexOf(sourceForm.FormID.ToString());
                if (index > -1)
                {
                    ////todo: ChildCollection.RemoveAt(index);
                    ////todo: ChildNames.RemoveAt(index);
                }

                Type formType = TerraScanCommon.mdiparent.GetType();
                MethodInfo methodInfo = formType.GetMethod("ShowActiveForms");
                sourceForm = (MdiWrapper)methodInfo.Invoke(TerraScanCommon.mdiparent, new object[] { false });
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Form Load Events

        /// <summary>
        /// Handles the Load Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void F1010_Load(object sender, EventArgs e)
        {
            try
            {
                this.formLoad = true;
                this.LoadWorkSpaces();
                this.CurrentImportId = -1;
                this.FillImportFormDetails(null, false);
                this.MortgageImportPanel.Size = this.Size;
                this.FocusRequiredInputField(this.TemplateHeaderPanel, true, true);
                this.PaymentEngineUserControl.ParentWorkItem = this.form1010control.WorkItem;
                this.PaymentEngineUserControl.IsAutoPayment = true;
                this.PaymentEngineUserControl.TotalDue = ValidEntriesTotalTextBox.DecimalTextBoxValue;

                if (this.mortgageImportDataSet.GetMortgageImportDetails.Rows.Count > 0)
                {
                    this.ParentForm.ActiveControl = this.TemplateNameButton;
                    this.ParentForm.ActiveControl.Focus();
                }
                else
                {
                    this.SetFirstFocus();
                }
                this.formLoad = false;
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
        /// sets the focus to New,when the form loads with no records
        /// </summary>
        private void SetFirstFocus()
        {
            /*Modified by kuppu for bug fixing - BugID-1686*/
            Control[] slectedcontrols = this.footerSmartPart.Controls.Find("HelpLinkLabel", true);
            if (slectedcontrols[0] is TerraScan.UI.Controls.TerraScanLinkLabel)
            {
                //// (slectedcontrols[0] as TerraScan.UI.Controls.TerraScanButton).Focus();
                this.ParentForm.ActiveControl = slectedcontrols[0];
            }
        }

        /// <summary>
        /// Sets the edit button permission.
        /// </summary>
        private void SetEditButtonPermission()
        {
            this.ImportFileButton.ActualPermission = this.PermissionFiled.editPermission; ////this.PermissionEdit;
        }

        /// <summary>
        /// Handles the Click Event for the Form
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void MortgageImportForm_Click(object sender, EventArgs e)
        {
            try
            {
                this.MortgageMonthCalendar.Visible = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region File path selection

        /// <summary>
        /// Handles the Click Event for FilePathButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void FilePathButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.FilePathOpenFileDialog.Filter = "Text Documents(*.txt)|*.txt|CSV(*.csv)|*.csv|All Files(*.*)|*.*";

                if (!this.FilePathOpenFileDialog.ShowDialog().Equals(DialogResult.Cancel))
                {
                    this.ChangeMortgageImportStatus();
                    this.FilePathTextBox.Text = this.FilePathOpenFileDialog.FileName.ToString();
                    this.FocusRequiredInputField(this.FilePathTextBox, true, true);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #endregion

        #region Create And Print Receipts

        /// <summary>
        /// Handles the Click Event for the CreateReceiptButton
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreateReceiptButton_Click(object sender, EventArgs e)
        {
            #region TSCO # 21806 - Enable create receipt button after check for error
            // Implemented TSCO # 21806 - Enable create receipt button after check for error 
            decimal paymentTotal = this.PaymentsTotalTextBox.DecimalTextBoxValue;
            decimal validEntries = this.ValidEntriesTotalTextBox.DecimalTextBoxValue;

            if (paymentTotal == 0 && validEntries > 0)
            {
                if ((MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), "TerraScan T2 - Unpaid Receipt(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                {
                    //flagmsg = false;
                    return;

                }
            }
            else
            {
                if (MessageBox.Show(SharedFunctions.GetResourceString("CreateReceiptMessage"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            this.Cursor = Cursors.WaitCursor;
            try
            {
                MortageImportData tempDataSet = new MortageImportData();
                string errorMessage = string.Empty;
                string errorCode = string.Empty;
                ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.ProcessStatus, null, null);
                this.ValidateMortgageImportFields();
                tempDataSet = F1010WorkItem.CheckMortgageImportValidReceipt(this.CurrentImportId, this.mortgageImportFieldsInstance.ReceiptDate);

                if (tempDataSet.Tables.Count > 0 && tempDataSet.CheckMortgageImportValidReceiptTest.Rows.Count > 0 && !String.IsNullOrEmpty(tempDataSet.CheckMortgageImportValidReceiptTest.Rows[0][tempDataSet.CheckMortgageImportValidReceiptTest.ErrorMsgColumn].ToString()))
                {
                    errorMessage = tempDataSet.CheckMortgageImportValidReceiptTest.Rows[0][tempDataSet.CheckMortgageImportValidReceiptTest.ErrorMsgColumn].ToString();
                    errorCode = tempDataSet.CheckMortgageImportValidReceiptTest.Rows[0][tempDataSet.CheckMortgageImportValidReceiptTest.ErrorCodeColumn].ToString();

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        if (errorCode == "1")
                        {
                            ////TODO title hardcode - needs some clarification
                            MessageBox.Show(SharedFunctions.GetResourceString("CreateReceiptTimedOutMsg"), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Timed Out"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            ////TODO title hardcode - needs some clarification
                            MessageBox.Show(String.Concat(SharedFunctions.GetResourceString("InvalidReceiptHeader"), errorMessage), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Invalid Receipts"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }


                if (paymentTotal == 0 && validEntries > 0)
                {
                    tempDataSet = F1010WorkItem.CreateReceipt(this.CurrentImportId, this.mortgageImportFieldsInstance.TemplateId, this.mortgageImportFieldsInstance.TemplateName, this.mortgageImportFieldsInstance.FilePath, this.mortgageImportFieldsInstance.SourceTypeId, this.mortgageImportFieldsInstance.ReceiptDate, this.mortgageImportFieldsInstance.InterestDate, this.mortgageImportFieldsInstance.PayCode,this.mortgageImportFieldsInstance.FirstHalfPayCode, TerraScanCommon.UserId, this.mortgageImportFieldsInstance.RollYear, 0, false);
                }
                else
                {
                    this.mortgageImportFieldsInstance.PpaymentId = this.PaymentEngineUserControl.CreatePayment(0);
                    if (this.mortgageImportFieldsInstance.PpaymentId > 0)
                    {
                        tempDataSet = F1010WorkItem.CreateReceipt(this.CurrentImportId, this.mortgageImportFieldsInstance.TemplateId, this.mortgageImportFieldsInstance.TemplateName, this.mortgageImportFieldsInstance.FilePath, this.mortgageImportFieldsInstance.SourceTypeId, this.mortgageImportFieldsInstance.ReceiptDate, this.mortgageImportFieldsInstance.InterestDate, this.mortgageImportFieldsInstance.PayCode, this.mortgageImportFieldsInstance.FirstHalfPayCode, TerraScanCommon.UserId, this.mortgageImportFieldsInstance.RollYear, this.mortgageImportFieldsInstance.PpaymentId, false);
                    }
                    else
                    {
                        ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                    }
                }
            #endregion

                if (tempDataSet.Tables.Count > 0 && tempDataSet.CreateRecieptError.Rows.Count > 0)
                {
                    errorMessage = tempDataSet.CreateRecieptError.Rows[0][tempDataSet.CreateRecieptError.ErrorMsgColumn].ToString();
                    errorCode = tempDataSet.CreateRecieptError.Rows[0][tempDataSet.CreateRecieptError.ErrorCodeColumn].ToString();
                    tempDataSet.Tables.Remove(tempDataSet.CreateRecieptError);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        if (tempDataSet.Tables.Count > 0 && tempDataSet.CreateRecieptDetails.Rows.Count > 0)
                        {
                            if (this.PaymentEngineUserControl.RefundNow && MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), SharedFunctions.GetResourceString("RefundTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ////refund management form
                                FormInfo formInfo = TerraScanCommon.GetFormInfo(1214);
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                            this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.AfterStatus;
                            this.ChangeStatusRelatedFields();
                            //Recalled the Parcelgrid to resolve 21028 issue in Mortage Import not reloaded on create reciept button bu purushotham
                            this.PaymentEngineUserControl.LoadPayment(this.mortgageImportFieldsInstance.PpaymentId);
                            ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.AfterStatus, tempDataSet.CreateRecieptDetails.Rows[0][tempDataSet.CreateRecieptDetails.RcptCreatedDtColumn].ToString(), tempDataSet.CreateRecieptDetails.Rows[0][tempDataSet.CreateRecieptDetails.RcptCreatedByColumn].ToString());
                            this.FocusRequiredInputField(this.CreateReceiptButton, true, true);
                            this.PageMode = PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                        }
                        else
                        {
                            this.mortgageImportFieldsInstance.CreateReceiptStatus = TerraScanCommon.StatusAction.BeforeStatus;
                            ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                            this.ChangeStatusRelatedFields();
                        }
                    }
                    else
                    {
                        if (errorCode == "2")
                        {
                            ////TODO title hardcode - needs some clarification
                            MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ChangeStatusLabel(this.CreateReceiptStatusLabel, TerraScanCommon.StatusAction.BeforeStatus, null, null);
                            this.Cursor = Cursors.Default;
                            return;
                        }
                        else
                        {
                            MessageBox.Show(errorMessage, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.FillImportFormDetails(tempDataSet, false);
                        }
                    }
                    this.CreateReceiptButton.Enabled = false;
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
        /// Handles the Click Event for the Print Receipt Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintReceiptButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.reportOptionalParameter.Clear();
                this.reportOptionalParameter.Add("KeyName", "PPaymentID");
                this.reportOptionalParameter.Add("KeyValue", this.mortgageImportFieldsInstance.PpaymentId);
                TerraScanCommon.ShowReport(10101, Report.ReportType.Print, this.reportOptionalParameter);
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
        /// Handles the Click Event for the View Receipt Button
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewReceiptsButton_Click(object sender, EventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(11001);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.PaymentEngineUserControl.PPaymentId;
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

        #endregion

        #region Mortgage Import Form Value Changed

        /// <summary>
        /// Handles the Value Changed Event for the Mortgage Import Header Fields
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MortgageImportFieldsValueChanged(object sender, EventArgs e)
        {
            Control sourceControl = sender as TextBox;

            if (sourceControl != null && sourceControl.Parent != null && sourceControl.Parent.ContainsFocus)
            {
                this.ChangeMortgageImportStatus();
            }
            TerraScan.UI.Controls.TerraScanCheckBox cc = sender as TerraScan.UI.Controls.TerraScanCheckBox;
            Control source = sender as CheckBox;
            var s = sender.ToString();
            if (s.Contains("System.Windows.Forms.CheckBox"))
            {
                this.ChangeMortgageImportStatus();

            }
            //var array[]=s.Split(",");
        }

        /// <summary>
        /// Change mortgage import form status
        /// </summary>       
        private void ChangeMortgageImportStatus()
        {
            if (this.PageMode.Equals(PageModeTypes.View))
            {
                this.PageMode = PageModeTypes.Edit;
                this.SetButtons(TerraScanCommon.ButtonActionMode.EditMode);
            }
        }

        /// <summary>
        /// Handles the Value Changed Event for the PaymentGrid
        /// </summary>        
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
        {
            ////this.ChangeMortgageImportStatus();
            this.PaymentEngineUserControl.TotalDue = this.ValidEntriesTotalTextBox.DecimalTextBoxValue;
        }

        /// <summary>
        /// adds necessary event handler to handle value changed event
        /// </summary>
        /// <param name="addEventHandler">bool to add the event handler</param>
        private void IncludeNecessaryEventHandler(bool addEventHandler)
        {
            this.RecieptDateTextBox.TextChanged -= new EventHandler(this.MortgageImportFieldsValueChanged);
            this.InterestDateTextBox.TextChanged -= new EventHandler(this.MortgageImportFieldsValueChanged);
            this.FilePathTextBox.TextChanged -= new EventHandler(this.MortgageImportFieldsValueChanged);
            this.PartialPaymentCheckBox.CheckStateChanged -= new EventHandler(this.MortgageImportFieldsValueChanged);
            this.FirstHalfChkBox.CheckStateChanged -= new EventHandler(this.MortgageImportFieldsValueChanged);
            //Commented by purushotham
            // this.PartialPaymentCheckBox.SelectionChangeCommitted -= new EventHandler(this.PayTypeList_SelectionChangeCommitted);

            if (addEventHandler && this.PermissionEdit)
            {
                this.RecieptDateTextBox.TextChanged += new EventHandler(this.MortgageImportFieldsValueChanged);
                this.FilePathTextBox.TextChanged += new EventHandler(this.MortgageImportFieldsValueChanged);
                this.InterestDateTextBox.TextChanged += new EventHandler(this.MortgageImportFieldsValueChanged);
                this.PartialPaymentCheckBox.CheckStateChanged += new EventHandler(this.MortgageImportFieldsValueChanged);
                this.FirstHalfChkBox.CheckStateChanged += new EventHandler(this.MortgageImportFieldsValueChanged);
                //Commented by purushotham
                // this.PartialPaymentCheckBox.SelectionChangeCommitted += new EventHandler(this.PayTypeList_SelectionChangeCommitted);
            }
        }

        /// <summary>
        /// Checks the input value validity.
        /// </summary>
        /// <param name="validValue">The valid value.</param>
        /// <param name="typeCode">The type code.</param>
        /// <returns>bool value</returns>
        private bool CheckInputValueValidity(ref string validValue, TypeCode typeCode)
        {
            bool returnValue = false;
            switch (typeCode)
            {
                case TypeCode.DateTime:
                    DateTime outDatetime;

                    if (DateTime.TryParse(validValue, out outDatetime) && outDatetime <= this.maximumDate && outDatetime >= this.minimunDate)
                    {
                        validValue = outDatetime.ToString(this.dateFormat);
                        returnValue = true;
                    }
                    else
                    {
                        validValue = System.DateTime.Now.ToString(this.dateFormat);
                    }

                    break;
                case TypeCode.Decimal:
                    Decimal outDecimal;

                    if (Decimal.TryParse(validValue, out outDecimal))
                    {
                        returnValue = true;
                    }

                    validValue = outDecimal.ToString("$#,##0.00");

                    break;
                case TypeCode.Int16:
                    Int16 outInteger;

                    if (Int16.TryParse(validValue, out outInteger))
                    {
                        returnValue = true;
                    }

                    validValue = outInteger.ToString();
                    break;
            }

            return returnValue;
        }

        /// <summary>
        /// Handles the Selection Change Committed Event for the PartialPaymentCheckBox
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PayTypeList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.CheckPayCode();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the PartialPaymentCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void PayTypeList_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;

                this.CheckPayCode();

                e.Cancel = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Checks the pay code.
        /// </summary>
        private void CheckPayCode()
        {
            //if (!(this.PartialPaymentCheckBox.SelectedIndex == this.mortgageImportFieldsInstance.PayCode) && this.PartialPaymentCheckBox.Focused)
            //{
            //    if (this.mortgageImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
            //    {
            //        ////disable selection change committed event to avoid looping
            //        this.PartialPaymentCheckBox.SelectionChangeCommitted -= new EventHandler(this.PayTypeList_SelectionChangeCommitted);

            //        if (MessageBox.Show(String.Concat(SharedFunctions.GetResourceString("CheckForErrors1"), "\n", SharedFunctions.GetResourceString("PayCodeChange")), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //        {
            //            this.SaveMortgageImportDetails(true);

            //            this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
            //            this.ChangeStatusRelatedFields();

            //            this.PageMode = PageModeTypes.View;
            //            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
            //        }
            //        else
            //        {
            //            ////revert the changes                          
            //            this.PartialPaymentCheckBox.SelectedIndex = this.mortgageImportFieldsInstance.PayCode;
            //        }

            //        ////enable selection change committed event
            //        this.PartialPaymentCheckBox.SelectionChangeCommitted += new EventHandler(this.PayTypeList_SelectionChangeCommitted);
            //    }
            //    else
            //    {
            //        this.ChangeMortgageImportStatus();
            //    }

            //    this.mortgageImportFieldsInstance.PayCode = this.PartialPaymentCheckBox.SelectedIndex;
            //}
        }

        #endregion

        /// <summary>
        /// Loads the work spaces.
        /// </summary>
        private void LoadWorkSpaces()
        {
            if (this.F1010control.WorkItem.SmartParts.Contains(SmartPartNames.RecordNavigatorSmartPart))
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.RecordNavigatorSmartPart));
            }
            else
            {
                this.RecordNavigatorSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.AddNew<RecordNavigatorSmartPart>(SmartPartNames.RecordNavigatorSmartPart));
            }

            if (this.F1010control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.AdditionalOperationSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.AdditionalOperationSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            if (this.F1010control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.formHeaderSmartPartdeckWorkspace.Show(this.F1010control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }

            if (this.F1010control.WorkItem.SmartParts.Contains(SmartPartNames.OperationSmartPart))
            {
                this.operationSmartPart = (OperationSmartPart)this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }
            else
            {
                this.operationSmartPart = (OperationSmartPart)this.F1010control.WorkItem.SmartParts.AddNew<OperationSmartPart>(SmartPartNames.OperationSmartPart);
                this.operationSmartPartWorkSpace.Show(this.operationSmartPart);
            }

            this.formLabelInfo[0] = Properties.Resources.FormName;
            this.formLabelInfo[1] = string.Empty;
            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));

            ////Testing for Commnts Click Event
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag.ToString());
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag.ToString());
            this.additionalOperationSmartPart.ParentWorkItem = this.F1010control.WorkItem;

            // To Load FooterSmartPart into FooterWorkspace
            if (this.F1010control.WorkItem.SmartParts.Contains(SmartPartNames.FooterSmartPart))
            {
                this.FooterWorkspace.Show(this.F1010control.WorkItem.SmartParts.Get(SmartPartNames.FooterSmartPart));
            }
            else
            {
                this.FooterWorkspace.Show(this.F1010control.WorkItem.SmartParts.AddNew<FooterSmartPart>(SmartPartNames.FooterSmartPart));
            }

            this.footerSmartPart = (FooterSmartPart)this.F1010control.WorkItem.SmartParts[SmartPartNames.FooterSmartPart];
            this.footerSmartPart.ParentWorkItem = this.F1010control.WorkItem;
            this.footerSmartPart.FormId = "1010";
            this.footerSmartPart.AuditLinkText = SharedFunctions.GetResourceString("ImportAuditLink");
            this.footerSmartPart.VisibleHelpButton = false;
            this.footerSmartPart.VisibleHelpLinkButton = true;
            this.footerSmartPart.TabStop = true;
        }

        /// <summary>
        /// Handles the LinkClicked event of the ImportAuditLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void ImportAuditLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowAuditReport("ImportID", this.ImportIdTextBox.Text);
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the MortgageMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MortgageMonthCalendar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.SetSeletedDate(this.MortgageMonthCalendar.SelectionStart.ToString(this.dateFormat));
                }

                this.issShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Reports

        /// <summary>
        /// Handles the Click event of the TaxRollReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TaxRollReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TerraScanCommon.ShowReport(10104, Report.ReportType.Preview);
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
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.reportOptionalParameter.Clear();
                this.reportOptionalParameter.Add("KeyName", "ImportID");
                this.reportOptionalParameter.Add("KeyValue", this.CurrentImportId);
                ////changed the parameter type from string to int
                TerraScanCommon.ShowReport(10101, Report.ReportType.Preview, this.reportOptionalParameter);
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

        /// <summary>
        /// Handles the Click event of the button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                TerraScan.Common.HelpEngine.Show(ParentForm.Text, "1010");
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Validating event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                Control sourceControl = (sender as TextBox);
                string sourceValue = sourceControl.Text.Trim();

                ////allow empty if check for error is not after status 
                if (String.IsNullOrEmpty(sourceValue) && !this.mortgageImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.AfterStatus))
                {
                    return;
                }

                if (!this.CheckInputValueValidity(ref sourceValue, TypeCode.DateTime))
                {
                    ////revert the changes
                    MessageBox.Show(SharedFunctions.GetResourceString("ValidDate") + "\n" + SharedFunctions.GetResourceString("Allowed") + "\n" + SharedFunctions.GetResourceString("MinDate") + this.minimunDate.ToShortDateString() + "\n" + SharedFunctions.GetResourceString("MaxDate") + this.maximumDate.ToShortDateString(), string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("DateValidation")), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    sourceControl.Text = this.mortgageImportFieldsInstance.InterestDate.ToString(this.dateFormat);
                    sourceControl.Focus();
                }
                else
                {
                    if (this.mortgageImportFieldsInstance.CheckErrorStatus.Equals(TerraScanCommon.StatusAction.AfterStatus) && this.mortgageImportFieldsInstance.InterestDate != DateTime.Parse(sourceValue))
                    {
                        if (MessageBox.Show(String.Concat(SharedFunctions.GetResourceString("CheckForErrors1"), "\n", SharedFunctions.GetResourceString("InterestDateChange")), string.Concat(ConfigurationWrapper.ApplicationName, " - ", "Interest Date Change"), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            this.SaveMortgageImportDetails(true);
                            sourceControl.Text = sourceValue;

                            this.mortgageImportFieldsInstance.CheckErrorStatus = TerraScanCommon.StatusAction.BeforeStatus;
                            this.ChangeStatusRelatedFields();

                            this.PageMode = PageModeTypes.View;
                            this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                            ////focus next control
                            ////this.FocusRequiredInputField(sourceControl, true);
                        }
                        else
                        {
                            ////revert the changes
                            ////this.InterestDateTextBox.TextChanged -= new EventHandler(this.InterestDateTextBox_TextChanged);
                            sourceControl.Text = this.mortgageImportFieldsInstance.InterestDate.ToString(this.dateFormat);
                            ////this.InterestDateTextBox.TextChanged += new EventHandler(this.InterestDateTextBox_TextChanged);
                        }
                    }
                    else
                    {
                        sourceControl.Text = sourceValue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the MortgageMonthCalendar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MortgageMonthCalendar_Leave(object sender, EventArgs e)
        {
            try
            {
                MortgageMonthCalendar.Visible = false;
                if (this.MortgageMonthCalendar.Tag.ToString() == "ReceiptDateTextBox")
                {
                    if (this.issShift)
                    {
                        this.RecieptDateTextBox.Focus();
                    }
                    else
                    {
                        this.InterestDateTextBox.Focus();
                    }
                }
                else if (this.MortgageMonthCalendar.Tag.ToString() == "InterestDateTextBox")
                {
                    if (this.issShift)
                    {
                        this.InterestDateTextBox.Focus();
                    }
                    else
                    {
                        this.PartialPaymentCheckBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                InterestDateTextBox.BackColor = Color.White;
                InterestDatePanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PartialPaymentCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PayTypeList_Leave(object sender, EventArgs e)
        {
            try
            {
                PartialPaymentCheckBox.BackColor = Color.White;
                PayPanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the RecieptDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RecieptDateTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                RecieptDateTextBox.BackColor = Color.White;
                RecieptDatePanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #region Bug #6553: TSCO - 1010 Mortgage Import - Add Statement Number Hyperlink to error grid

        /// <summary>
        /// Handles the CellMouseMove event of the ErrorGridView control.
        /// Change cursor type while mouse move on Statement Number link based on StatementID 
        /// </summary>
        /// <param name="sender">ErrorGridView</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void ErrorGridView_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.ErrorGridView.Columns[e.ColumnIndex].Name.Equals(this.errorCheckDataTable.StatementNumberColumn.ColumnName))
                {
                    if (!ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value.Equals(null) && (string.IsNullOrEmpty(ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementIDColumn.ColumnName].Value.ToString()) || string.IsNullOrEmpty(ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value.ToString()) || ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementIDColumn.ColumnName].Value.Equals(0)))
                    {
                        this.ErrorGridView.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the ErrorGridView control.
        /// Show Real Property Statement Form on StatementNumber Link click
        /// </summary>
        /// <param name="sender">ErrorGridView</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void ErrorGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.ErrorGridView.Columns[e.ColumnIndex].Name.Equals(this.errorCheckDataTable.StatementNumberColumn.ColumnName))
                {
                    if (ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value != null && !string.IsNullOrEmpty(ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementNumberColumn.ColumnName].Value.ToString()) && !ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementIDColumn.ColumnName].Value.Equals(0))
                    {
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(11020);
                        formInfo.optionalParameters = new object[1];
                        formInfo.optionalParameters[0] = Convert.ToInt64(ErrorGridView.Rows[e.RowIndex].Cells[this.errorCheckDataTable.StatementIDColumn.ColumnName].Value.ToString());
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Bug #6553: TSCO - 1010 Mortgage Import - Add Statement Number Hyperlink to error grid

        private void PartialPaymentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // var s = sender.ToString();            
        }

        private void PartialPaymentCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            //if (isLoaded)
            //{
            //    this.isLoaded = true;
            //}
            //else
            //{
            //this.ChangeMortgageImportStatus();
            //    this.isLoaded = false;

            //}

            if (this.PartialPaymentCheckBox.CheckState == CheckState.Checked)
            {
                this.mortgageImportFieldsInstance.PayCode = true;
            }
            else
            {
                this.mortgageImportFieldsInstance.PayCode = false;
            }
        }

        private void FirstHalfChkBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FirstHalfChkBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.FirstHalfChkBox.CheckState == CheckState.Checked)
            {
                this.mortgageImportFieldsInstance.FirstHalfPayCode = 1;
            }
            else
            {
                this.mortgageImportFieldsInstance.FirstHalfPayCode = 0;
            }
        }

        private void CommentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter;

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(1010).openPermission))
                {
                    optionalParameter = new object[] { 99999, 0, 99999 };

                    Form commentForm = new Form();
                    commentForm = this.form1010control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
                    commentForm.Tag = this.additionalOperationSmartPart.CurrntFormId; //9999;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        ////AdditionalOperationCountEntity additionalOperationCountEnt;
                        ////this.additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        this.additionalOperationCountEnt.CommentCount = Convert.ToInt32(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        this.additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(this.additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Sets the text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
       
            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentAllButton.Text = "Comment All";
                }
                else
                {
                    this.CommentAllButton.Text = "Comment All" + "(" + additionalOperationCountEntity.CommentCount + ")";
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    this.CommentAllButton.BackColor = this.highPriorityCommentColor;
                    this.CommentAllButton.CommentPriority = true;
                }
                else
                {
                    this.CommentAllButton.BackColor = this.defaultCommentButtonBackColor;
                    this.CommentAllButton.CommentPriority = false;
                }
            }
        }

        

    }
}
