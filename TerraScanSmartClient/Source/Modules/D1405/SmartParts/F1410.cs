//--------------------------------------------------------------------------------------------
// <copyright file="F1410.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14 Dec 06		KARTHIKEYAN V	    Created
// 06 Dec 08        ShanmugaSundaram.A  Modified for Implementing F9025 Validation
// 16 Feb 09        VasanthaMalliga.R   Coding added for the issue 4973
//26 Feb 09         VasanthaMalliga.R   Coding added for the issue 4989
//26 Feb 09         VasanthaMalliga.R   Coding added for the issue 5098
//29 Apr 09         Ramya.D             Coding added for the issue 5381
//24/08/09         Sadha Shivudu        Implemented TSCO # 2803 - Default Interest/Receipt Dates now global
//18 May 10         Biju I.G.           To implement #6598
//22 Sep 10         Biju I.G.           To implement #8507
//26 Nov 10         Biju I.G.           To implement #9326
//28 Jun 11         Manoj Kumar         Change the tender Type default 'check'.    
//25 Aug 11         Manoj Kumar         Change in StatementSearch button and Fix attachment path Copy updation.
//12 SEP 11         Manoj Kumar         Removed the tender Type default 'check' TSCO #13410.
//20120907          Manoj Kumar         To implement #17419
//*********************************************************************************/

namespace D1405
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F1410
    /// </summary>
    [SmartPart]
    public partial class F1410 : BaseSmartPart
    {
        #region Variable

        /// <summary>
        /// F1410Controller
        /// </summary>
        private F1410Controller form1410Control = new F1410Controller();

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private int ownerId;

        /// <summary>
        ///  Used To Store receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        /// Used to Store interestDueTemp
        /// </summary>
        private double interestDueTemp;

        /// <summary>
        /// LoginUserValidation;
        /// </summary>
        private bool loginUserValidation;

        /// <summary>
        ///  Used To Store ownerId
        /// </summary>
        private decimal paymentTotal;

        private bool flagmsg=false;

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color highPriorityCommentColor = Color.FromArgb(255, 0, 0);

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultCommentButtonBackColor = Color.FromArgb(174, 150, 94);


        /// <summary>
        /// additionalOperationSmartPart
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
        /// Created string for store ParcelIDs
        /// </summary>
        private string receiptIdXml = string.Empty;

        /// <summary>
        /// parcelTypeDataset
        /// </summary>
        private F2550TaxRollCorrectionData parcelTypeDataset = new F2550TaxRollCorrectionData();

        /// <summary>
        /// Object for attachment typed dataset
        /// </summary>
        private AttachmentsData attachmentDataSet = new AttachmentsData();

        /// <summary>
        /// Created Integer for Attachment FormID
        /// </summary>
        private int attachmentFormID;

        /// <summary>
        /// Created Integer for Attachment keyID 
        /// </summary>
        private int attachmentKeyID;

        /// <summary>
        /// Created string to Find Extension 
        /// </summary>
        private string browsePathExt = string.Empty;

        /// <summary>
        /// Created string for filePath
        /// </summary>
        private string filePath = string.Empty;

        /// <summary>
        /// Created string for fileID
        /// </summary>
        private string fileID = string.Empty;

        /// <summary>
        /// to check file Exist
        /// </summary>        
        private bool fileExist;

        /// <summary>
        /// F1410OwnerReceiptingData
        /// </summary>
        private F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();

        /// <summary>
        /// ownerXml
        /// </summary>
        private DataTable tempOwnerId = new DataTable(SharedFunctions.GetResourceString("Table"));

        /// <summary>
        /// statementDataTable
        /// </summary>
        private DataTable statementDataTable = new DataTable();

        /// <summary>
        /// tempstatementDataTable
        /// </summary>
        private DataTable tempstatementDataTable = new DataTable();

        /// <summary>
        /// ownerDataTable
        /// </summary>
        private DataTable ownerDataTable = new DataTable();

        /// <summary>
        /// ownerXmlDataset
        /// </summary>
        private DataSet ownerXmlDataset = new DataSet(SharedFunctions.GetResourceString("Root"));

        /// <summary>
        /// getAutoPrintOnValue
        /// </summary>
        private CommentsData getAutoPrintOnValue = new CommentsData();

        /// <summary>
        /// autoprintonoff
        /// </summary>
        private bool autoprintonoff;

        /// <summary>
        /// ownerRowCount
        /// </summary>
        private int ownerRowCount;

        /// <summary>
        /// flag to identify completion of save operation
        /// </summary>
        private bool flagSaveConfirmed;

        /// <summary>
        /// saveBatchOperation
        /// </summary>
        private bool saveBatchOperation;

        /// <summary>
        /// statementRowCount
        /// </summary>
        private int statementRowCount;

        /// <summary>
        /// selectedColumn
        /// </summary>
        private int selectedColumn;

        /// <summary>
        /// ownerLow
        /// </summary>
        private string ownerLow;

        /// <summary>
        /// ownerLowVal
        /// </summary>
        private int ownerLowVal;

        /// <summary>
        /// ownerHigh
        /// </summary>
        private string ownerHigh;

        /// <summary>
        ///  save
        /// </summary>
        private bool saveOption;

        /// <summary>
        /// ownerHighVal
        /// </summary>
        private int ownerHighVal;

        /// <summary>
        /// flagShift
        /// </summary>
        private bool flagShift = true;

        /// <summary>
        /// assigining report Number - default value 0
        /// </summary>
        private int reportNumber = 0;

        /// <summary>
        /// Selectionchangecommited
        /// </summary>
        private bool selectionchangecommited;

        /// <summary>
        ///textchange
        /// </summary>
        private bool textchange;

        /// <summary>
        ///loadstmtgridflag
        /// </summary>
        private bool loadstmtgridflag;

        /// <summary>
        ///checkreadonlyflag
        /// </summary>
        private bool checkreadonlyflag;

        /// <summary>
        ///checkgrid clicked or not
        /// </summary>
        private bool gridclickflag;

        /// <summary>
        ///date changed or not flag checking
        /// </summary>
        private bool datechangeflag;

        /// <summary>
        /// To hold the bool value while clicking RemoveButton
        /// </summary>
        private bool removeButtonClicked;

        /// <summary>
        ///  To hold bool value while appearing the messagebox
        /// </summary>
        private bool messabeBoxFlag;

        /// <summary>
        /// To hold boll value while pressing key
        /// </summary>
        private bool cellEdited;

        /// <summary>
        /// DataSet to hold unchecked statement rows in SaveBatchOperation
        /// </summary>
        private DataSet uncheckedDataSet = new DataSet();

        /// <summary>
        /// To store the edited row index
        /// </summary>
        private int editedRowIndex;

        /// <summary>
        /// Restrict to select the mindue radio button
        /// </summary>
        private bool balanceRadioButton;

        /// <summary>
        /// To restrict the row enter event.
        /// </summary>
        private bool restrictRowEnter;

        /// <summary>
        /// Holds true when from load
        /// /// </summary>
        private bool ownerReceiptingFormLoad;

        /// <summary>
        /// To holds the edited rows
        /// </summary>
        private DataTable modifiedRowCollectionDataTable = new DataTable();

        /// <summary>
        /// allstatementDataTable
        /// </summary>
        private DataTable allstatementDataTable = new DataTable();

        /// <summary>
        /// checkedStatementDataTable
        /// </summary>
        private DataTable checkedStatementDataTable = new DataTable();

        private PaymentEngineData payeeDetails = new PaymentEngineData();

        private SortOrder statmentSortOrder;

        private int sortedColumnIndex;

        //private WorkItem payeeWorkItem;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1410"/> class.
        /// </summary>
        public F1410()
        {
            this.InitializeComponent();
            this.OwnerPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.OwnerPictureBox.Height, this.OwnerPictureBox.Width, SharedFunctions.GetResourceString("Owner"), 0, 51, 0);
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, SharedFunctions.GetResourceString("Statements"), 174, 150, 94);
            this.ReceiptPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ReceiptPictureBox.Height, this.ReceiptPictureBox.Width, SharedFunctions.GetResourceString("Receipt"), 28, 81, 128);
        }


     

        #endregion

        #region Event Publication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// To set the attachment and comment count
        /// </summary>
        ////Added by Manoj
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<AdditionalOperationCountEntity>> SetAttachmentCount;
        
        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the mortgage import template controll.
        /// </summary>
        /// <value>The mortgage import template controll.</value>
        [CreateNew]
        public F1410Controller Form1410Controll
        {
            get { return this.form1410Control as F1410Controller; }
            set { this.form1410Control = value; }
        }


        public PaymentEngineData PayeeDetails
        {
             get { return this.payeeDetails ; }
            set { this.payeeDetails = value; }
        }

        #endregion

        #region Event Scbscription

        /// <summary>
        /// Gets the form status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The source of the event</param>
        [EventSubscription(EventTopics.D9001_ShellForm_GetFormStatus, Thread = ThreadOption.UserInterface)]
        public void GetFormStatus(object sender, DataEventArgs<string> e)
        {
            try
            {
                if (e.Data == "F" + this.Tag.ToString())
                {
                    this.form1410Control.WorkItem.State[SharedFunctions.GetResourceString("FormStatus")] = this.CheckPageStatus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
            ////Coding Added for the Issue 4989 on 26/2/2009(form to be opened to a specific KeyID)
            object[] optionalParams = e.Data;
            if (optionalParams.Length > 0 && optionalParams[optionalParams.Length - 1].Equals(this.ParentFormId))
            {
                string ownerNo = Convert.ToString(optionalParams[0]);
                int.TryParse(ownerNo, out this.ownerId);
                if (!string.IsNullOrEmpty(ownerNo))
                {
                    if (this.ownerId > 0)
                    {
                        this.ownerId = Convert.ToInt32(optionalParams[0]);

                        DataRow[] dr = null;
                        string commandResult = string.Empty;
                        DataTable masterNameSearchDatatable = new DataTable();
                        DataRow masterNameSearchdatarow;

                        if (masterNameSearchDatatable.Columns.Count.Equals(0))
                        {
                            masterNameSearchDatatable.Columns.Add("OwnerId");
                        }

                        masterNameSearchdatarow = masterNameSearchDatatable.NewRow();
                        masterNameSearchdatarow["OwnerId"] = this.ownerId.ToString();
                        masterNameSearchDatatable.Rows.Add(masterNameSearchdatarow);
                        commandResult = TerraScanCommon.GetXmlString(masterNameSearchDatatable);

                        this.ownerRowCount = this.ownerDataTable.Rows.Count;

                        if (this.ownerDataTable != null)
                        {
                            if (this.ownerDataTable.Rows.Count > 0)
                            {
                                dr = this.ownerDataTable.Select(SharedFunctions.GetResourceString("OwnerIdExpression") + this.ownerId.ToString());
                            }
                        }

                        if (this.ownerRowCount > 0)
                        {
                            if (dr.Length.Equals(0))
                            {
                                this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                                this.ownerReceiptingDataSet.FormBackgroundTable.Clear();
                                this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(), commandResult, null));

                                if (this.ownerReceiptingDataSet.ListOwnerReceiptTable.Rows.Count > 0)
                                {
                                    this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                    this.LoadOwnerReceipting();

                                    if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0)
                                    {
                                        Color formBackColor = this.LoadBackGroundColor(this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim());
                                        this.BackColor = formBackColor;
                                    }
                                }
                                else
                                {
                                    DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                    if (DialogResult.OK == dialogResult)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        this.ParentForm.Close();
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("OwnerExixts"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                            this.ownerReceiptingDataSet.FormBackgroundTable.Clear();

                            this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(), commandResult, null));
                            if (this.ownerReceiptingDataSet.ListOwnerReceiptTable.Rows.Count > 0)
                            {
                                this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                this.LoadOwnerReceipting();

                                if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0)
                                {
                                    Color formBackColor = this.LoadBackGroundColor(this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim());
                                    this.BackColor = formBackColor;
                                }
                            }
                            else
                            {
                                DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                if (DialogResult.OK == dialogResult)
                                {
                                    return;
                                }
                                else
                                {
                                    this.ParentForm.Close();
                                    return;
                                }
                            }

                            if (this.OwnerGridView.OriginalRowCount > 0)
                            {
                                this.PaymentEngineUserControl.OwnerName = this.ownerDataTable.Rows[0]["OwnerName"].ToString();
                                //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();    
                            }

                            this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                            this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;
                        }

                        this.OwnerLinkLabel.Focus();
                        ////COding added for the issue 682 on 3/6/209
                       /* if (this.gridclickflag)
                        {
                            this.DisableSorting();
                        }
                        else
                        {
                            this.EnableSorting();
                        }*/
                        ////Ends here for 682
                        this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.Trim());
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show(SharedFunctions.GetResourceString("F9030DefaultAlert"), SharedFunctions.GetResourceString("TerraScanInvalidRecord"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (DialogResult.OK == dialogResult)
                        {
                            return;
                        }
                        else
                        {
                            this.ParentForm.Close();
                            return;
                        }
                    }
                }
            }
        }
        //// Ends Here 4989

        #endregion

        #region Regular Expression

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is integer; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInteger(string value)
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

        #region Events

        /// <summary>
        /// Handles the Load event of the F1410 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1410_Load(object sender, EventArgs e)
        {
            try
            {
                //this.ownerReceiptingDataSet.ListOwnerStatementTable.PrimaryKey = new DataColumn[] { this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn };
                //this.allstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();
                this.LoadWorkSpace();
                this.AttachmentAllButton.Enabled = false;
                this.CommentAllButton.Enabled = false; 
                this.PaymentEngineUserControl.PaymentAmountChangeEvent += new TerraScan.PaymentEngine.PaymentEngineUserControl.PaymentAmountChangeEventHandler(this.PaymentEngineUserControl_PaymentAmountChangeEvent);
                this.CustomizeOwnerGridView();
                this.CustomizeStatementGridView();
                this.ControlStatus(false);
                this.OwnerGridView.Enabled = false;
                this.StatementGridView.Enabled = false;
                this.PreviewButton.Enabled = false;
                this.OwnerGridView.DataSource = null;
                this.StatementGridView.DataSource = null;

                this.RemoveDefaultSelection();
                this.OwnerReceiptingMenu.Visible = false;
                this.SaveMenu.Visible = false;

                if (this.PermissionFiled.newPermission)
                {
                    this.SaveMenu.Click += new EventHandler(this.SaveBatchButton_Click);
                }

                this.PaymentEngineUserControl.LoadPayment();
                this.PaymentEngineUserControl.ParentWorkItem = this.form1410Control.WorkItem;      
                this.ReceivedbyTextBox.Text = TerraScanCommon.UserName;
                ////this.ReceiptDateTextBox.Text = DateTime.Now.ToShortDateString();
                this.ownerReceiptingFormLoad = true;
                ////this.InterestDateTextBox.Text = DateTime.Now.ToShortDateString();

                // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
                this.ReceiptDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
                this.InterestDateTextBox.Text = TerraScanCommon.InterestDate.ToShortDateString();

                this.ownerReceiptingFormLoad = false;
                this.ReciptPanel.Enabled = false;
                this.SaveBatchButton.Enabled = false;
                this.RemoveOwnerButton.Enabled = false;
                this.tempOwnerId.Columns.Add(SharedFunctions.GetResourceString("OwnerID"));
                this.statementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();
                this.modifiedRowCollectionDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();
                this.ownerDataTable = this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clone();
                this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();

                this.PaymentEngineUserControl.IsAutoPayment = true;
                this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                
                this.statementDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.StatementGridView.EmptyRecordColumnName) });
                this.ownerDataTable.Columns.AddRange(new DataColumn[] { new DataColumn(this.OwnerGridView.EmptyRecordColumnName) });
                this.statementDataTable.Columns[this.StatementGridView.EmptyRecordColumnName].DefaultValue = "False";
                this.ownerDataTable.Columns[this.OwnerGridView.EmptyRecordColumnName].DefaultValue = "False";
                OwnerStatusLinkLabel.Visible = false;
                this.ActiveControl = this.OwnerLinkLabel;
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1410Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                OwnerLinkLabel.Focus();

                ////New Form 9025 Validation
                Form newCommentTemplateForm = new Form();
                ////Form UserLoginValidation = new Form();
                newCommentTemplateForm = TerraScanCommon.ShowFormValidation(1410);   ////this.formNo);
                this.loginUserValidation = Convert.ToBoolean(newCommentTemplateForm.Text.ToString());
                double.TryParse(this.InterestDueTextBox.Text.ToString().Trim(), out this.interestDueTemp);
                DeleteAttachmentAndComment();
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
        /// Handles the Click event of the AddOwnerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AddOwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                // To Leave the focus from stmtGrid while clicking the AddOwnerButton after edting the stmtGrid BugId 5381 -- Ramya.D
                this.StatementGridView.CurrentCell = null;

                // To check whether the messagebox is appeared or not BugId 5381 -- Ramya.D
                if (!this.messabeBoxFlag)
                {
                    DataRow[] dr = null;
                    string currentOwnerId = string.Empty;
                    Form parcelF9101 = new Form();
                    object[] optionalParameter = new object[] { 91000 };
                    parcelF9101 = TerraScanCommon.GetForm(9110, null, this.form1410Control.WorkItem);

                    if (parcelF9101 != null)
                    {
                        if (parcelF9101.ShowDialog() == DialogResult.Yes)
                        {
                            this.checkreadonlyflag = false;
                            currentOwnerId = TerraScanCommon.GetValue(parcelF9101, "CommandResult");
                            DataSet currentownerDataTable = new DataSet();
                            currentownerDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentOwnerId));

                            // this.ownerDataTable.Merge(currentownerDataTable.Tables[0], true);

                            if (currentownerDataTable.Tables[0].Rows.Count > 0)
                            {
                                this.ownerId = Convert.ToInt32(currentownerDataTable.Tables[0].Rows[0]["OwnerId"]);
                            }

                            if (this.ownerDataTable != null)
                            {
                                if (this.ownerDataTable.Rows.Count > 0)
                                {
                                    if (currentownerDataTable.Tables[0].Rows.Count > 0)
                                    {
                                        for (int ownerCount = 0; ownerCount < currentownerDataTable.Tables[0].Rows.Count; ownerCount++)
                                        {
                                            this.ownerId = Convert.ToInt32(currentownerDataTable.Tables[0].Rows[ownerCount]["OwnerId"]);
                                            dr = this.ownerDataTable.Select(SharedFunctions.GetResourceString("OwnerIdExpression") + this.ownerId.ToString());
                                        }
                                    }
                                }
                            }

                            if (this.ownerRowCount > 0)
                            {
                                if (dr.Length == 0)
                                {
                                    this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                                    this.ownerReceiptingDataSet.FormBackgroundTable.Clear();
                                    this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(), currentOwnerId, null ));

                                    this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                    this.LoadOwnerReceipting();
                                    
                                    /// used for Owner Receipt Default tender type "Check"
                                    //if (this.PaymentEngineUserControl.PayeeDetails == null)
                                    //{
                                       decimal balance = this.BalanceTextBox.DecimalTextBoxValue;
                                        this.autoTenderType(balance);
                                    //}
                                    //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();
                                    //int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"], out  this.PaymentEngineUserControl.OwnerpayeeID);    
                                                                    
                                }
                                else
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("OwnerExixts"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                                this.ownerReceiptingDataSet.FormBackgroundTable.Clear();
                                this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(), currentOwnerId, null));
                                this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                this.LoadOwnerReceipting();

                                if (this.OwnerGridView.OriginalRowCount > 0)
                                {
                                    /// used for Owner Receipt Default tender type "Check"
                                    this.PaymentEngineUserControl.OwnerName = this.ownerDataTable.Rows[0]["OwnerName"].ToString();
                                    //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();   
                                    //int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"], out  this.PaymentEngineUserControl.OwnerpayeeID);    
                                    int ownerID;
                                    int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"].ToString(), out ownerID);
                                    if (this.PaymentEngineUserControl.PayeeDetails == null)
                                    {
                                        if (ownerID > 0)
                                        {
                                            this.PaymentEngineUserControl.PayeeDetails = this.form1410Control.WorkItem.F1019_GetPayeeDetails(ownerID);
                                        }
                                        else
                                        {
                                            this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                                        }
                                    }
                                    //Removed the tender Type default 'check' TSCO #13410.
                                  //  this.PaymentEngineUserControl.LoadDefultValue(this.PaymentEngineUserControl.OwnerName, this.BalanceTextBox.DecimalTextBoxValue);  
                                }

                                this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                                this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;
                            }

                            if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0)
                            {
                                Color formBackColor = this.LoadBackGroundColor(this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim());
                                this.BackColor = formBackColor;
                            }

                            #region Sorting BugId 5381 -- Ramya.D

                            /*if (this.gridclickflag)
                            {
                                this.DisableSorting();
                            }
                            else
                            {
                                this.EnableSorting();
                            }*/
                            #endregion Sorting
                        }
                    }

                    OwnerLinkLabel.Focus();
                    if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString()))
                    {
                        this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                    }

                    if (this.sortedColumnIndex >= 0)
                    {
                        if (this.statmentSortOrder.Equals(SortOrder.Descending))
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                        }
                        else
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        }
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

                /*if (!this.gridclickflag)
                {
                    this.EnableSorting();
                }
                else
                {
                    this.DisableSorting();
                }*/

                this.messabeBoxFlag = false;

                #region Permission BugId 5381 -- Ramya.D
                ////Coding commented for the issue 698
                ////this.StatementGridView.Enabled = this.PermissionFiled.editPermission;
                ////this.PaymentEngineUserControl.Enabled = this.PermissionFiled.editPermission;
                ////this.ReceiptDateTextBox.LockKeyPress = !this.PermissionFiled.editPermission;
                ////this.InterestDateTextBox.LockKeyPress = !this.PermissionFiled.editPermission;
                ////this.ReceiptDateTextBox.Enabled = this.PermissionFiled.editPermission;
                ////this.InterestDateTextBox.Enabled = this.PermissionFiled.editPermission;
                ////this.OwnerStatusLinkLabel.Enabled = this.PermissionFiled.editPermission;
                ////this.OwnerLinkLabel.Enabled = this.PermissionFiled.editPermission;

                #endregion Permission
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
        /// Handles the DateSelected event of the ReceiptDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.ReceiptDateTextBox.Text = e.Start.ToShortDateString();
                this.ReceiptDateCalender.Visible = false;
                ReceiptDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the DateSelected event of the InterestDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DateRangeEventArgs"/> instance containing the event data.</param>
        private void InterestDateCalender_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                this.InterestDateTextBox.Text = e.Start.ToShortDateString();
                this.InterestDateCalender.Visible = false;
                if (this.datechangeflag)
                {
                    //this.EnableSorting();
                    this.CalculateInterestDate();
                }

                this.datechangeflag = false;
                InterestDateTextBox.Focus();
                this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the ReceiptDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.ReceiptDateTextBox.Text = this.ReceiptDateCalender.SelectionStart.ToShortDateString();
                    this.ReceiptDateCalender.Visible = false;
                    ReceiptDateTextBox.Focus();
                }

                this.flagShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the InterestDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void InterestDateCalender_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    this.InterestDateTextBox.Text = this.InterestDateCalender.SelectionStart.ToShortDateString();
                    this.InterestDateCalender.Visible = false;

                    this.InterestDateCalender.Focus();
                    if (this.datechangeflag)
                    {
                        //this.EnableSorting();
                        this.CalculateInterestDate();
                    }

                    this.datechangeflag = false;
                    this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                }

                this.flagShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the ReceiptDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ReceiptDateCalender.Visible = false;
                if (this.flagShift)
                {
                    this.ReceiptDateTextBox.Focus();
                }
                else
                {
                    this.InterestDateTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the InterestDateCalender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalender_Leave(object sender, EventArgs e)
        {
            try
            {
                this.InterestDateTextBox.Focus();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ReceiptDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowReceiptDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the InterestDateCalenderButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateCalenderButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Calls the method to show the calender control.
                this.ShowInterestDateCalender();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.OwnerGridView.OriginalRowCount > 0 || this.StatementGridView.OriginalRowCount > 0)
                {
                    if (MessageBox.Show(SharedFunctions.GetResourceString("F1410ClearButton"), SharedFunctions.GetResourceString("F1410ClearHeader"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.cellEdited = false;
                        this.ClearForm();
                        this.AttachmentAllButton.Enabled = false;
                        this.CommentAllButton.Enabled = false; 
                        OwnerStatusLinkLabel.Visible = false;

                        OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                        OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                        this.PaymentCaculation();

                        // Change the form background color as default
                        this.BackColor = System.Drawing.Color.White;
                        this.sortedColumnIndex = -1;
                        this.statmentSortOrder = SortOrder.None;
                    }
                    else
                    {
                        this.StatementGridView.CurrentCell = null;
                        return;
                    }
                }

                this.gridclickflag = false;
                this.saveBatchOperation = false;
                this.textchange = false;
                this.loadstmtgridflag = false;
                this.datechangeflag = false;
                this.MinDueRadioButton.Checked = true;
                this.BalanceRadioButton.Checked = false;
                this.StatementGridView.AllowSorting = true;
                this.restrictRowEnter = false;
                DeleteAttachmentAndComment(); 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.ownerRowCount > 0)
                {
                    if (e.RowIndex >= 0)
                    {
                        this.OwnerLinkLabel.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerNameColumn.ColumnName].Value.ToString();
                        this.Address1TextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address1Column.ColumnName].Value.ToString();
                        this.Address2TextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address2Column.ColumnName].Value.ToString();
                        this.CityTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.CityColumn.ColumnName].Value.ToString();
                        this.StateTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.StateColumn.ColumnName].Value.ToString();
                        this.ZipTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.ZipColumn.ColumnName].Value.ToString();
                        ////Added by Biju on 18/May/2010 to implement #6598
                        this.OwnerIDTextbox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerCodeColumn.ColumnName].Value.ToString();
                        ////Added by Biju on 22/Sep/2010 to implement #8507
                        this.OwnershipTypeTextbox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnershipTypeColumn.ColumnName].Value.ToString();
                        
                        this.ownerLow = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName].Value.ToString();

                        this.ownerHigh = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName].Value.ToString();

                        int.TryParse(this.ownerLow, out this.ownerLowVal);
                        int.TryParse(this.ownerHigh, out this.ownerHighVal);
                        if (this.ownerLowVal > 0)
                        {
                            OwnerStatusLinkLabel.Visible = true;
                            OwnerStatusLinkLabel.Text = "Status";
                            OwnerStatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                            OwnerPanel.BackColor = Color.FromArgb(200, 214, 230);
                        }

                        if (this.ownerHighVal > 0)
                        {
                            OwnerStatusLinkLabel.Visible = true;
                            OwnerStatusLinkLabel.Text = "Status";
                            OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                            OwnerPanel.BackColor = Color.FromArgb(237, 205, 203);
                        }

                        if (((this.ownerLowVal == 0) && (this.ownerHighVal == 0)) || ((this.ownerLowVal > 0) && (this.ownerHighVal > 0)))
                        {
                            OwnerStatusLinkLabel.Visible = false;
                            OwnerStatusLinkLabel.Text = "Status";
                            OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                            OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellFormatting event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;
            try
            {
                //// Only paint if desired, formattable column

                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
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

                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
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

                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName.ToString()].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName.ToString()].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val, out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
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

                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName].Index)
                {
                    if (e.RowIndex < 0)
                    {
                        return;
                    }

                    /* Only paint if text provided
                     Only paint if desired text is in cell */
                    if (e.Value != null && !e.Value.Equals(System.DBNull.Value) && !String.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value.ToString().Trim()))
                    {
                        string val = e.Value.ToString();
                        if (Decimal.TryParse(val.Replace(",", "").Replace("(", "").Replace(")", ""), out outDecimal))
                        {
                            if (outDecimal.ToString().Contains("-"))
                            {
                                e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
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

                if (this.StatementGridView.OriginalRowCount > 0)
                {
                    Color rowBackColor = Color.White;
                    if (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.BackgroundColorColumn.ColumnName].Value != null)
                    {
                        rowBackColor = this.LoadBackGroundColor(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.BackgroundColorColumn.ColumnName].Value.ToString().Trim());
                        e.CellStyle.BackColor = rowBackColor;
                    }

                    
                }

                this.ParcelNumberLinkForeColor(e);
                this.StatementNumberLinkForeColor(e);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Statements the color of the number link fore.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void StatementNumberLinkForeColor(DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName].Index)
                {
                    if (this.StatementGridView.Rows[e.RowIndex].Selected || this.StatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.White;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Parcels the color of the number link fore.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void ParcelNumberLinkForeColor(DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].Index)
                {
                    if (this.StatementGridView.Rows[e.RowIndex].Selected || this.StatementGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                    {
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.White;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                    }
                    else
                    {
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.Blue;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                        (this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellParsing event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                Decimal outDecimal;

                // Only paint if desired column
                if (e.RowIndex < 0)
                {
                    return;
                }

                // Only paint if text provided, Only paint if desired text is in cell
                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (tempvalue.EndsWith("."))
                    {
                        tempvalue = string.Concat(tempvalue, "0");
                    }

                    if (Decimal.TryParse(tempvalue, out outDecimal))
                    {
                        // change property for combobox change
                        tempvalue = outDecimal.ToString();
                        tempvalue = tempvalue.Replace("-", "");

                        if (!tempvalue.Contains("."))
                        {
                            tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                        }

                        if (outDecimal.ToString().Contains("-"))
                        {
                            outDecimal = decimal.Parse(tempvalue);
                            outDecimal = decimal.Negate(outDecimal);
                        }
                        else
                        {
                            outDecimal = decimal.Parse(tempvalue);
                        }

                        e.Value = outDecimal;
                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = decimal.Parse("0");
                        e.ParsingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the MinDueRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void MinDueRadioButton_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the Click event of the BalanceRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void BalanceRadioButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.messabeBoxFlag || this.selectedColumn != this.StatementGridView.Columns["TaxDue"].Index)
                {
                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        DataTable expressionDataTable = new DataTable();
                        expressionDataTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked = True,TempBalanceDue,'0.00')";
                        expressionDataTable.Columns["paymentType"].Expression = "IIF(StatementID<>0,'1','-1')";
                        expressionDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked = True,TempInterestDue,'0.00')";
                        this.restrictRowEnter = true;
                        this.statementDataTable.Load(expressionDataTable.CreateDataReader());
                        this.restrictRowEnter = false;

                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;
                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.StatementGridView.CurrentCell = null;
                        this.StatementGridView.Rows[this.editedRowIndex].Selected = true;
                    }

                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    this.BalanceRadioButton.Checked = true;

                    #region BugId 5381

                    //this.EnableSorting();

                    #endregion BugId 5381
                }
                else
                {
                    if (this.balanceRadioButton)
                    {
                        this.BalanceRadioButton.Checked = true;
                    }
                    else
                    {
                        this.MinDueRadioButton.Checked = true;
                    }

                    this.StatementGridView.Focus();
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellContentClick event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
            try
            {
                int rowClicked = 0;
                rowClicked = e.RowIndex + 1;

                if (e.RowIndex >= 0 && this.StatementGridView.OriginalRowCount > 0)
                {
                    if (this.StatementGridView.OriginalRowCount >= rowClicked)
                    {
                        if (e.ColumnIndex == 0)
                        {
                            string statementId = string.Empty;
                            int statementIdIndex = -1;

                            // To find the clicked row in datatable
                            statementId = this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet
                                    .ListOwnerStatementTable.StatementIDColumn.ColumnName].Value.ToString();

                            DataRow[] statementRow = this.statementDataTable.Select("StatementID = " + statementId);
                            if (statementRow.Length > 0)
                            {
                                statementIdIndex = this.statementDataTable.Rows.IndexOf(statementRow[0]);
                            }

                            if (statementIdIndex >= 0)
                            {
                                this.StatementGridView.CurrentCell = StatementGridView[e.ColumnIndex, e.RowIndex];
                                if (this.StatementGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsChecked")].Value.ToString() == "True")
                                {
                                    this.statementDataTable.Rows[statementIdIndex]["IsChecked"] = "False";
                                    this.statementDataTable.Rows[statementIdIndex]["TaxDue"] = "0.00";
                                    this.statementDataTable.Rows[statementIdIndex]["InterestDue"] = "0.00";
                                }
                                else
                                {
                                    //this.DisableSorting();
                                    this.messabeBoxFlag = false;
                                    this.statementDataTable.Rows[statementIdIndex]["IsChecked"] = "True";
                                    if (this.statementDataTable.Rows[statementIdIndex]["PaymentType"].Equals("1"))
                                    {
                                        this.statementDataTable.Rows[statementIdIndex]["TaxDue"]
                                            = this.statementDataTable.Rows[statementIdIndex]["TempBalanceDue"].ToString();
                                    }
                                    else
                                    {
                                        this.statementDataTable.Rows[statementIdIndex]["TaxDue"]
                                            = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet
                                    .ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].ToString();
                                    }

                                    this.statementDataTable.Rows[statementIdIndex]["InterestDue"]
                                        = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet
                                    .ListOwnerStatementTable.TempInterestDueColumn.ColumnName].ToString();
                                    DataRow[] checkedCount = this.statementDataTable.Select("IsChecked=True");
                                    if (checkedCount.Length >= 1)
                                    {
                                        decimal balance = 0.00M;


                                        for (int i = 0; i < checkedCount.Length; i++)
                                        {
                                            decimal bal1, bal2;
                                            decimal.TryParse(checkedCount[i]["TaxDue"].ToString(), out bal1);
                                            decimal.TryParse(checkedCount[i]["InterestDue"].ToString(), out bal2);
                                            balance = balance + bal1 + bal2;
                                        }

                                        this.autoTenderType(balance);
                                    }

                                    
                        }

                                this.statementDataTable.Rows[statementIdIndex].AcceptChanges();
                            }
                          
                            
                            #region Commented
                            /* if (!string.IsNullOrEmpty(this.TaxFeeDueTextBox.Text.Trim()))
                            {
                                decimal.TryParse(this.TaxFeeDueTextBox.Text.Trim(), out taxFeeDue);
                            }

                            if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.Trim()))
                            {
                                decimal.TryParse(this.InterestDueTextBox.Text.Trim(), out interestDue);
                            }

                            if (this.StatementGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsChecked")].Value.ToString() == "True")
                            {
                                this.StatementGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsChecked")].Value = "False";
                              
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].Value = 0;
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].Value = 0;
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName].Value = 0;

                                StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value = "0.00";

                                this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[e.RowIndex]["TaxDue"] = 0.0;

                                
                            }
                            else
                            {
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.InterestDue.Name].Value =
                                    this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[e.RowIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].ToString();
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].Value =
                                    this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].Value;
                                this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName].Value = 
                                    this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].Value;
                                this.StatementGridView.Rows[e.RowIndex].Cells[SharedFunctions.GetResourceString("IsChecked")].Value = "True";
                                // Coding for Issue Id 4973 on 18/2/2009 by Malliga
                                if (this.StatementGridView.Rows[e.RowIndex].Cells["PaymentType"].Value.ToString().Equals("0"))
                                {
                                    StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value = this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].Value;
                                }
                                else
                                {
                                    StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value = this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].Value;
                                }
                                // End Here for 4973

                                this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[e.RowIndex]["TaxDue"] = StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value;

                                if (!string.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].Value.ToString())
                                        || !string.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].Value.ToString()))
                                {
                                    ////taxFeeDue = Convert.ToDecimal(this.StatementGridView.Rows[i].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].Value) + taxFeeDue;
                                    taxFeeDue = taxFeeDue + Convert.ToDecimal(StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].Value.ToString()); ; //  Convert.ToDecimal(this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[e.RowIndex]["TaxDue"].ToString());
                                    interestDue = interestDue + Convert.ToDecimal(StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].Value.ToString());// Convert.ToDecimal(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].Value);
                                }
                            }*/

                            #endregion Commented
                            this.statementDataTable.EndInit();
                            this.StatementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);

                            this.PaymentCaculation();

                            this.StatementGridView.Refresh();
                            ////this.editedRow = true;
                           
                        }
                        else if (e.ColumnIndex == 2)
                        {
                            this.StatementGridView.CurrentCell = StatementGridView[e.ColumnIndex, e.RowIndex];

                            int statementId = 0;
                            int formId = 0;
                            int.TryParse(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].Value.ToString(), out statementId);
                            int.TryParse(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementFormColumn.ColumnName].Value.ToString(), out formId);

                            if (statementId > 0)
                            {
                                FormInfo formInfo;
                                formInfo = TerraScanCommon.GetFormInfo(formId);
                                formInfo.optionalParameters = new object[1];
                                formInfo.optionalParameters[0] = statementId;
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                        }
                        else if (e.ColumnIndex == 4)
                        {
                            this.StatementGridView.CurrentCell = StatementGridView[e.ColumnIndex, e.RowIndex];

                            int parcelId = 0;
                            int.TryParse(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelIDColumn.ColumnName].Value.ToString(), out parcelId);

                            if (parcelId > 0)
                            {
                                FormInfo formInfo;
                                formInfo = TerraScanCommon.GetFormInfo(11006);
                                formInfo.optionalParameters = new object[1];
                                formInfo.optionalParameters[0] = parcelId;
                                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the RemoveOwnerButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RemoveOwnerButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatementGridView.CurrentCell = null;
                this.ownerReceiptingDataSet.ListOwnerStatementTable.Clear();
                string statementXml = string.Empty;
                this.removeButtonClicked = true;
                this.textchange = false;
                this.loadstmtgridflag = false;
                if (!this.messabeBoxFlag)
                {
                    if (this.OwnerGridView.OriginalRowCount == 1)
                    {
                        ////Clear the form
                        this.ClearForm();
                        OwnerStatusLinkLabel.Visible = false;
                        OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                        OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                        this.PaymentCaculation();
                        this.DeleteAttachmentAndComment();
                        this.AttachmentAllButton.Enabled = false;
                        this.CommentAllButton.Enabled = false;   
                        this.removeButtonClicked = false;

                        // Change the form background color as default
                        this.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        int selectedRow = 0;
                        if (this.OwnerGridView.CurrentCell != null)
                        {
                            selectedRow = this.OwnerGridView.CurrentCell.RowIndex;
                        }

                        DataRow[] dr = this.statementDataTable.Select("", "", DataViewRowState.ModifiedOriginal);

                        DataTable dt = new DataTable();
                        dt = this.statementDataTable.GetChanges();
                        statementXml = TerraScanCommon.GetXmlString(this.statementDataTable);

                        string currentFormBackcolor = string.Empty;
                        if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0 && this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName] != null)
                        {
                            currentFormBackcolor = this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim();
                        }

                        this.ownerReceiptingDataSet = this.form1410Control.WorkItem.DeleteOwnerReceipting(Convert.ToInt32(this.OwnerGridView.Rows[selectedRow].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerIDColumn.ColumnName].Value.ToString()), TerraScanCommon.GetXmlString(this.ownerDataTable), statementXml, TerraScanCommon.UserId, currentFormBackcolor);

                        ////for (int count = 0; count < this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows.Count; count++)
                        ////{
                        ////    DataRow[] statementId = this.statementDataTable.Select("StatementID = "
                        ////          + this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].ToString());
                        ////    if (statementId.Length != 0)
                        ////    {
                        ////        this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName] =
                        ////            this.statementDataTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].ToString();

                        ////        this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName] =
                        ////            this.sta    tementDataTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName].ToString();

                        ////        this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName] =
                        ////            this.statementDataTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName].ToString();

                        ////        this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName] =
                        ////            this.statementDataTable.Rows[count][this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName].ToString();
                        ////    }
                        ////}

                        this.statementDataTable.Clear();
                        this.statementDataTable.Merge(this.ownerReceiptingDataSet.ListOwnerStatementTable);
                        this.ownerDataTable.Clear();
                        this.ownerXmlDataset.Clear();
                        this.LoadOwnerReceipting();
                        if (this.OwnerGridView.OriginalRowCount == 0)
                        {
                            this.ClearOwner();
                            this.DeleteAttachmentAndComment();
                            this.AttachmentAllButton.Enabled = false;
                            this.CommentAllButton.Enabled = false;   
                        }
                    }

                    if (this.sortedColumnIndex >= 0)
                    {
                        if (this.statmentSortOrder.Equals(SortOrder.Descending))
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                        }
                        else
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        }
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

                this.messabeBoxFlag = false;
                this.MinDueRadioButton.Checked = true;
                this.BalanceRadioButton.Checked = false;
                this.removeButtonClicked = false;
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
        /// Handles the Click event of the SaveBatchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveBatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatementGridView.CurrentCell = null;
                if (!this.messabeBoxFlag)
                {
                    //int validatedId = this.form1410Control.WorkItem.F9025SaveValidationDetails(1410, TerraScanCommon.ValidationUserId, 1);
                    // Form F9025 Validation for validating the user as Administrator or not
                    if ((this.interestDueTemp != Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim())) && (this.loginUserValidation.Equals(true)) && (TerraScanCommon.Administrator != true))
                    {
                        bool value = TerraScanCommon.AdminUserValidationForm(this.form1410Control.WorkItem);
                        if (value.Equals(true))
                        {
                            if (this.SaveBatchButton.Enabled)
                            {
                                this.PaymentTotalLabel.Focus();
                                if (this.RequiredFields())
                                {
                                    this.SaveBatchOperation();
                                    if (!this.flagmsg)
                                    {
                                        this.CopyAttachment();
                                    }
                                    this.flagmsg = false; 
                                    this.saveOption = true;
                                    int validatedId = this.form1410Control.WorkItem.F9025SaveValidationDetails(1410, TerraScanCommon.ValidationUserId, this.receiptId);
                                    this.SaveBatchButton.Focus();

                                    this.sortedColumnIndex = -1;
                                    this.statmentSortOrder = SortOrder.None;
                                }
                                else
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    if (string.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
                                    {
                                        this.ReceiptDateTextBox.Focus();
                                    }
                                    else
                                    {
                                        this.InterestDateTextBox.Focus();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.SaveBatchButton.Enabled)
                        {
                            this.PaymentTotalLabel.Focus();
                            if (this.RequiredFields())
                            {
                                this.SaveBatchOperation();
                                int validatedId = this.form1410Control.WorkItem.F9025SaveValidationDetails(1410, TerraScanCommon.ValidationUserId, this.receiptId);
                                if (!this.flagmsg)
                                {
                                    this.CopyAttachment();
                                }
                                this.flagmsg = false; 
                                this.saveOption = true;
                                this.SaveBatchButton.Focus();

                                this.sortedColumnIndex = -1;
                                this.statmentSortOrder = SortOrder.None;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                if (string.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()))
                                {
                                    this.ReceiptDateTextBox.Focus();
                                }
                                else
                                {
                                    this.InterestDateTextBox.Focus();
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.StatementGridView.Focus();
                }

                this.gridclickflag = false;
                //this.EnableSorting();
                this.messabeBoxFlag = false;
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
        /// Handles the Click event of the SelectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatementGridView.CurrentCell = null;
                if (!this.messabeBoxFlag)
                {
                    DataRow[] uncheckRows = this.statementDataTable.Select("IsChecked = False and EmptyRecord$ = False");
                    if (uncheckRows.Length > 0 && this.StatementGridView.OriginalRowCount > 0)
                    {
                        DataTable expressionTable = new DataTable();
                        expressionTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionTable.Columns["IsChecked"].Expression = "IIF(EmptyRecord$ = false, 'True', 'False')";
                        expressionTable.Columns["TaxDue"].Expression = "IIF(PaymentType=1,TempBalanceDue,TempMinTaxDue)";
                        expressionTable.Columns["InterestDue"].Expression = "TempInterestDue";
                        this.statementDataTable.Load(expressionTable.CreateDataReader());
                        for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                        {
                            if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                            {
                                StatementGridView.Rows[readonlyCount].ReadOnly = true;
                            }
                            else
                            {
                                StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                            }
                        }
                        this.ReciptPanel.Enabled = true;
                        this.PreviewButton.Enabled = true;
                        this.PaymentEngineUserControl.Enabled = true;
                    }
                 
                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    DataRow[] checkedCount = this.statementDataTable.Select("IsChecked=True");
                    if (checkedCount.Length > 0 && this.tempstatementDataTable.Rows.Count > 0)
                    {
                        decimal balance = this.BalanceTextBox.DecimalTextBoxValue;
                        this.autoTenderType(balance);
                    }
                    if (this.StatementGridView.OriginalRowCount > 0)
                    {
                        this.StatementGridView.Rows[0].Selected = true;
                    }
                    else
                    {
                        this.StatementGridView.Rows[0].Selected = false;
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

                if (this.sortedColumnIndex >= 0)
                {
                    if (this.statmentSortOrder.Equals(SortOrder.Descending))
                    {
                        this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }
                    else
                    {
                        this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                }

                this.messabeBoxFlag = false;
               // this.DisableSorting();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the UnselectAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void UnselectAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatementGridView.CurrentCell = null;
                if (!this.messabeBoxFlag)
                {
                    DataRow[] checkedRow = this.statementDataTable.Select("IsChecked = True and EmptyRecord$ = False");
                    if (this.StatementGridView.OriginalRowCount > 0)
                    {
                        DataTable expressionTable = new DataTable();
                        expressionTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionTable.Columns["IsChecked"].Expression = " 'False' ";
                        expressionTable.Columns["TaxDue"].Expression = "IIF(EmptyRecord$=False,'0.00', '0')";
                        expressionTable.Columns["InterestDue"].Expression = "IIF(EmptyRecord$=False,'0.00', '0' )";
                        this.statementDataTable.Load(expressionTable.CreateDataReader());
                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;
                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.SaveBatchButton.Enabled = false;
                        this.ReciptPanel.Enabled = false;
                        this.PaymentEngineUserControl.LoadPayment();
                        this.PaymentEngineUserControl.Enabled = false;
                        this.PaymentEngineUserControl.BalanceAmount = Decimal.Zero;
                        this.PreviewButton.Enabled = false;
                        this.OwnerLinkLabel.Focus();
                       // this.DisableSorting();
                    }

                    if (this.StatementGridView.OriginalRowCount > 0)
                    {
                        this.StatementGridView.Rows[0].Selected = true;
                    }
                    else
                    {
                        this.StatementGridView.Rows[0].Selected = false;
                    }

                    this.statementDataTable.EndInit();

                    if (this.sortedColumnIndex >= 0)
                    {
                        if (this.statmentSortOrder.Equals(SortOrder.Descending))
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                        }
                        else
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        }
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
                if (this.datechangeflag)
                {
                    this.CalculateInterestDate();
                    this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                    //this.EnableSorting();
                    //coding added for the issue 1363[focus has to be in first feild of payment engine control]
                    this.PaymentEngineUserControl.Focus();
                    //Ends Here
                }

                this.datechangeflag = false;
                InterestDateTextBox.BackColor = Color.White;
                InterestDatePanel.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the InterestDateTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void InterestDateTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Coding added for the issue 5272
                if (!this.ownerReceiptingFormLoad)
                {
                    this.datechangeflag = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the HelplinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelplinkLabel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OwnerStatusLinkLabel_LinkClicked
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OwnerStatusLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Form ownerstatus = new Form();
                if (this.OwnerGridView.CurrentRowIndex != -1)
                {
                    object[] optionalParameters = new object[] { this.ownerDataTable.Rows[this.OwnerGridView.CurrentRowIndex]["OwnerID"], 5, 91000 };
                    ownerstatus = TerraScanCommon.GetForm(9102, optionalParameters, this.form1410Control.WorkItem);
                }

                if (ownerstatus != null)
                {
                    ownerstatus.ShowDialog();
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
        /// Help MenuItem Click Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString().Trim()))
                {
                    HelpEngine.Show(ParentForm.Text, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the OwnerLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void OwnerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int rowId = 0;
                int currentOwnerId = 0;
                if (this.OwnerGridView.CurrentCell != null)
                {
                    rowId = this.OwnerGridView.CurrentCell.RowIndex;
                }

                int.TryParse(this.OwnerGridView.Rows[rowId].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerIDColumn.ColumnName].Value.ToString(), out currentOwnerId);

                this.form1410Control.WorkItem.RootWorkItem.State["91000SystemSnapShotId"] = null;
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(91000);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = currentOwnerId;
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
        /// ReceivedbyTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReceivedbyTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                ReceivedbyTextBox.BackColor = Color.White;
                ReceivedbyPanel.BackColor = Color.White;
                if (this.flagShift)
                {
                    BalanceRadioButton.Focus();
                    this.flagShift = false;
                }
                else
                {
                    ReceiptDateTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ReceiptDateTextBox_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReceiptDateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.flagShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ReceivedbyTextBox_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ReceivedbyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.flagShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// BalanceRadioButton_KeyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void BalanceRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                this.flagShift = e.Shift;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Payments the engine user control_ payment amount change event.
        /// </summary>
        /// <param name="amount">The amount.</param>
        private void PaymentEngineUserControl_PaymentAmountChangeEvent(decimal amount)
            {
            this.paymentTotal = amount;
            this.PaymentTotalTextBox.DecimalTextBoxValue = amount;
            if (this.statementDataTable.Rows.Count > 0 && this.paymentTotal > 0)
            {
                this.PaymentCaculation();
            }
            else if (this.statementDataTable.Rows.Count > 0 && this.paymentTotal < 0)
            {
                this.PaymentCaculation();
            }

            ////COding added for the issue 679 on 2/6/2009
            decimal taxFeeDue = 0;
            decimal interestDue = 0;
            decimal balanceAmount = 0;
            if (this.statementDataTable.Rows.Count > 0 && this.paymentTotal == 0)
            {
                this.TaxFeeDueTextBox.Text = this.statementDataTable.Compute("SUM(TaxDue)", "IsChecked = True").ToString();

                // To add interest values and assign to InterestDue TextBox
                this.InterestDueTextBox.Text = this.statementDataTable.Compute("SUM(InterestDue)", "IsChecked = True").ToString();

                if (!string.IsNullOrEmpty(this.TaxFeeDueTextBox.Text.ToString().Trim()))
                {
                    decimal.TryParse(this.TaxFeeDueTextBox.Text.ToString().Trim(), out taxFeeDue);
                }

                if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString().Trim()))
                {
                    decimal.TryParse(this.InterestDueTextBox.Text.ToString().Trim(), out interestDue);
                }

                this.ReceiptTotalTextBox.Text = (taxFeeDue + interestDue).ToString("$ #,##0.00");
                this.PaymentEngineUserControl.TotalReceiptAmount = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                balanceAmount = (taxFeeDue + interestDue) - this.paymentTotal;
                this.BalanceTextBox.Text = balanceAmount.ToString("$ #,##0.00");
                this.PaymentTotalTextBox.Text = this.paymentTotal.ToString("$ #,##0.00");
                this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;

                if (this.BalanceTextBox.DecimalTextBoxValue == 0)
                {
                    this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                    this.BalanceTextBox.ForeColor = Color.Black;
                }
                else
                {
                    this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                    this.BalanceTextBox.ForeColor = Color.White;
                }
           }
            ////Coding ends here 679
        }

        /// <summary>
        /// Handles the ColumnHeaderMouseClick event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    this.restrictRowEnter = true;
                    this.StatementGridView.AllowSorting = false;
                    this.StatementGridView.ClearSorting();
                    this.StatementGridView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.Programmatic;
                    if (this.StatementGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.None
                        || this.StatementGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
                    {
                        /*DataView gridDataView = (DataView)this.StatementGridView.DataSource;
                        gridDataView.RowFilter = "EmptyRecord$=False";
                        gridDataView.Sort = string.Concat(this.StatementGridView.Columns[e.ColumnIndex].DataPropertyName, " ASC");
                        gridDataView.ApplyDefaultSort = false;
                        this.statementDataTable = gridDataView.ToTable().Copy();
                        this.StatementGridView.DataSource = this.statementDataTable.DefaultView;
                        this.StatementGridView.ClearSorting();
                        this.StatementGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;*/

                        this.sortedColumnIndex = e.ColumnIndex;
                        this.statmentSortOrder = SortOrder.Ascending;
                        this.SortStatementGrid(this.statmentSortOrder, this.sortedColumnIndex);
                    }
                    else
                    {
                        /*DataView gridDataView = (DataView)this.StatementGridView.DataSource;
                        gridDataView.RowFilter = "EmptyRecord$=False";
                        gridDataView.Sort = string.Concat(this.StatementGridView.Columns[e.ColumnIndex].DataPropertyName, " DESC");
                        gridDataView.ApplyDefaultSort = false;
                        this.statementDataTable = gridDataView.ToTable().Copy();
                        this.StatementGridView.DataSource = this.statementDataTable.DefaultView;
                        this.StatementGridView.ClearSorting();
                        this.StatementGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;*/
                        this.sortedColumnIndex = e.ColumnIndex;
                        this.statmentSortOrder = SortOrder.Descending;
                        this.SortStatementGrid(this.statmentSortOrder, this.sortedColumnIndex);
                    }

                    this.StatementGridView.AllowSorting = false;
                   // this.StatementGridView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                {
                    if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                    {
                        StatementGridView.Rows[readonlyCount].ReadOnly = true;
                    }
                    else
                    {
                        StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void SortStatementGrid(SortOrder sortOrder, int ColumnIndex)
        {
            DataView gridDataView = (DataView)this.StatementGridView.DataSource;
            gridDataView.RowFilter = "EmptyRecord$=False";
            if (sortOrder.Equals(SortOrder.Descending))
            {
                gridDataView.Sort = string.Concat(this.StatementGridView.Columns[ColumnIndex].DataPropertyName, " DESC");
            }
            else
            {
                gridDataView.Sort = string.Concat(this.StatementGridView.Columns[ColumnIndex].DataPropertyName, " ASC");
            }
            gridDataView.ApplyDefaultSort = false;
            this.statementDataTable = gridDataView.ToTable().Copy();
            this.StatementGridView.DataSource = this.statementDataTable.DefaultView;
            this.StatementGridView.ClearSorting();
            if (sortOrder.Equals(SortOrder.Descending))
            {
                this.StatementGridView.Columns[ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
            }
            else
            {
                this.StatementGridView.Columns[ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }
        }

        private void StatementGridView_Sorted(object sender, System.EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                this.textchange = true;
                this.loadstmtgridflag = true;

                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    this.selectionchangecommited = false;
                    ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.F1410_StmtGridSelectionChangeCommitted);
                }

                e.Control.TextChanged += new EventHandler(this.Control_TextChanged);
                e.Control.KeyDown += new KeyEventHandler(this.Control_KeyDown);
            }
            catch (ArithmeticException exp)
            {
                ExceptionManager.ManageException(exp, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles text changed event for datatgridview
        /// </summary>
        /// <param name="sender">Statement DataGridView</param>
        /// <param name="e">event args</param>
        private void Control_TextChanged(object sender, EventArgs e)
        {
            if (this.gridclickflag)
            {
                this.DisableSorting();
            }
            ////Coding added for the issue 1166[save and cancel button should get enable when right click is use to paste]. 
            this.cellEdited = true;
        }

        /// <summary>
        /// Handles key down event for datagridview
        /// </summary>
        /// <param name="sender">Statement DataGridView</param>
        /// <param name="e">event args</param>
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 40 || e.KeyValue == 38)
            {
                this.selectionchangecommited = false;
            }
            else
            {
                this.selectionchangecommited = true;
            }

            this.gridclickflag = true;
            this.cellEdited = true;
            ////this.editedRow = true;
            this.StatementGridView.AllowSorting = false;
        }

        /// <summary>
        /// Handles the StmtGridSelectionChangeCommitted event of the F1410 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1410_StmtGridSelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!this.selectionchangecommited && this.StatementGridView.CurrentCell.ColumnIndex.Equals(6))
                {
                    ComboBox combo = (ComboBox)sender;
                    string statementId = string.Empty;
                    int statementIdIndex = -1;

                    if (this.StatementGridView.OriginalRowCount > 0)
                    {
                        int rowIndex = 0;
                        if (this.StatementGridView.CurrentCell != null)
                        {
                            rowIndex = this.StatementGridView.CurrentCell.RowIndex;
                        }

                        statementId = this.StatementGridView.Rows[this.StatementGridView.CurrentCell.RowIndex]
                            .Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].Value.ToString();

                        DataRow[] statementRow = this.statementDataTable.Select("StatementID = " + statementId);

                        if (statementRow.Length > 0)
                        {
                            statementIdIndex = this.statementDataTable.Rows.IndexOf(statementRow[0]);
                        }

                        if (combo.Text == "Min")
                        {
                            if (statementIdIndex >= 0)
                            {
                                if (StatementGridView.Rows[statementIdIndex].Cells["IsChecked"].Value.ToString() != null)
                                {
                                    if (StatementGridView.Rows[statementIdIndex].Cells["IsChecked"].Value.ToString() == "True")
                                    {
                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName] = 0;

                                        // To get the original value of InterestDue Column and TaxDue Column
                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName]
                                            = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].ToString();

                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName]
                                            = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].ToString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (statementIdIndex >= 0)
                            {
                                if (StatementGridView.Rows[statementIdIndex].Cells["IsChecked"].Value.ToString() != null)
                                {

                                    if (StatementGridView.Rows [statementIdIndex].Cells["IsChecked"].Value.ToString() == "True")
                                    {
                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName] = 1;

                                        // To get the original value of InterestDue Column and TaxDue Column
                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName]
                                             = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].ToString();

                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName]
                                            = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].ToString();
                                    }
                                }
                            }
                        }

                        this.statementDataTable.Rows[statementIdIndex].AcceptChanges();
                        this.selectionchangecommited = true;
                        ////this.editedRow = true;
                        this.PaymentCaculation();
                        //this.DisableSorting();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (!string.IsNullOrEmpty(StatementGridView.Rows[e.RowIndex].Cells["StatementID"].Value.ToString()))
                    {
                        this.StatementGridView.Rows[e.RowIndex].Cells["PaymentType"].ReadOnly = false;
                        this.StatementGridView.Rows[e.RowIndex].Cells["IsChecked"].ReadOnly = false;
                        this.StatementGridView.Rows[e.RowIndex].Cells["InterestDue"].ReadOnly = false;
                        this.StatementGridView.Rows[e.RowIndex].Cells["TaxDue"].ReadOnly = false;
                    }
                    else
                    {
                        this.StatementGridView.Rows[e.RowIndex].ReadOnly = true;
                    }

                    if (e.ColumnIndex == this.StatementGridView.Columns["PaymentType"].Index)
                    {
                        this.selectionchangecommited = false;
                    }

                    if (e.ColumnIndex == -1)
                    {
                        this.restrictRowEnter = false;
                    }
                }
                else
                {
                    this.restrictRowEnter = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // To get row index 
                if (e.RowIndex != -1 && e.ColumnIndex != -1 && !this.restrictRowEnter)
                {
                    this.editedRowIndex = e.RowIndex;
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the StatementGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void StatementGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                if (this.loadstmtgridflag && this.textchange && this.checkreadonlyflag && this.cellEdited)
                {
                    int currrentrowidex;
                    int currentColumnIndex;
                    decimal feeAmountValue = 0;
                    decimal taxDueValue = 0;
                    decimal taxDueGridvalue = 0;
                    int taxDueIndex = 0;
                    int paymnetTypeIndex = 0;
                    string statementId = string.Empty;
                    int statementIdIndex = -1;

                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        if (this.StatementGridView.OriginalRowCount > 0)
                        {
                            if (this.StatementGridView.CurrentRow != null)
                            {
                                if (this.StatementGridView.CurrentRow.Index >= 0)
                                {
                                    currrentrowidex = this.StatementGridView.CurrentRow.Index;
                                    currentColumnIndex = this.StatementGridView.CurrentCell.ColumnIndex;
                                    if (StatementGridView.Rows[currrentrowidex].Cells["IsChecked"].Value.ToString() != null)
                                    {
                                        if (StatementGridView.Rows[currrentrowidex].Cells["IsChecked"].Value.ToString() == "True")
                                        {
                                            // To find the clicked row in datatable
                                            statementId = this.StatementGridView.Rows[currrentrowidex].Cells[this.ownerReceiptingDataSet
                                                    .ListOwnerStatementTable.StatementIDColumn.ColumnName].Value.ToString();
                                            DataRow[] statementRow = this.statementDataTable.Select("StatementID = " + statementId);
                                            statementIdIndex = this.statementDataTable.Rows.IndexOf(statementRow[0]);

                                            if (currentColumnIndex == StatementGridView.Columns["TaxDue"].Index)
                                            {
                                                if (statementRow.Length > 0)
                                                {
                                                    // To get FeeAmount column's Index
                                                    int feeAmountIndex = this.statementDataTable.Columns.IndexOf(this.ownerReceiptingDataSet
                                                        .ListOwnerStatementTable.FeeAmountColumn.ColumnName);

                                                    decimal.TryParse(statementRow[0][feeAmountIndex].ToString(), out feeAmountValue);

                                                    // To get PaymentType column's index
                                                    paymnetTypeIndex = this.statementDataTable.Columns.IndexOf(this.ownerReceiptingDataSet
                                                        .ListOwnerStatementTable.PaymentTypeColumn.ColumnName);

                                                    // To get TaxDue depends on PaymentType
                                                    if (this.BalanceRadioButton.Checked ||
                                                        (!string.IsNullOrEmpty(statementRow[0][paymnetTypeIndex].ToString()) &&
                                                        statementRow[0][paymnetTypeIndex].ToString().Equals("1")))
                                                    {
                                                        taxDueIndex = this.statementDataTable.Columns.IndexOf(this.ownerReceiptingDataSet
                                                        .ListOwnerStatementTable.TempBalanceDueColumn.ColumnName);
                                                    }
                                                    else
                                                    {
                                                        taxDueIndex = this.statementDataTable.Columns.IndexOf(this.ownerReceiptingDataSet
                                                        .ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName);
                                                    }

                                                    decimal.TryParse(statementRow[0][taxDueIndex].ToString(), out taxDueValue);

                                                    int taxDueColumnGridIndex = this.statementDataTable.Columns.IndexOf(this.ownerReceiptingDataSet
                                                        .ListOwnerStatementTable.TaxDueColumn.ColumnName);

                                                    decimal.TryParse(statementRow[0][taxDueColumnGridIndex].ToString(), out taxDueGridvalue);

                                                    // To check the edited taxDue value with original taxdue value and feeamount
                                                    if (taxDueGridvalue > taxDueValue || taxDueGridvalue < feeAmountValue)
                                                    {
                                                        MessageBox.Show(SharedFunctions.GetResourceString("Taxes/FeeAmountValidation"), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                       
                                                        this.messabeBoxFlag = true;
                                                        this.cellEdited = false;

                                                        // To get which radio button is checked
                                                        if (this.BalanceRadioButton.Checked)
                                                        {
                                                            this.balanceRadioButton = true;
                                                        }
                                                        else
                                                        {
                                                            this.balanceRadioButton = false;
                                                        }

                                                        if (statementIdIndex >= 0)
                                                        {
                                                            if (statementRow[0][paymnetTypeIndex].ToString() == "0")
                                                            {
                                                                this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName]
                                                                = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].ToString();
                                                            }
                                                            else
                                                            {
                                                                this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName]
                                                                = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].ToString();
                                                                this.BalanceRadioButton.Focus();
                                                            }
                                                        }

                                                        this.PaymentCaculation();
                                                        return;
                                                    }

                                                    this.statementDataTable.Rows[statementIdIndex].AcceptChanges();
                                                }
                                            }
                                            else if (currentColumnIndex == StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].Index)
                                            {
                                                if (statementIdIndex >= 0)
                                                {
                                                    double maxmoney = 922337203685477.5807;
                                                    if (Convert.ToDouble(this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName]) > maxmoney)
                                                    {
                                                        MessageBox.Show(SharedFunctions.GetResourceString("Entered values exceeds max limit."), "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        //// this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName] = "0.00";
                                                        this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName]
                                                        = this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].ToString();
                                                    }
                                                    if (this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName].Equals("True"))
                                                    {
                                                        if (!string.IsNullOrEmpty(this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn].ToString()))
                                                        {
                                                            this.statementDataTable.Rows[statementIdIndex][this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName] = "0.00";
                                                        }
                                                    }
                                                }
                                            }

                                            this.cellEdited = false;
                                            ////this.editedRow = false;
                                            this.PaymentCaculation();
                                        }
                                    }
                                }
                            }
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
        /// Payments the engine user control_ payment item change event.
        /// </summary>
        private void PaymentEngineUserControl_PaymentItemChangeEvent()
            {
            this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
            if (this.ownerDataTable.Rows.Count > 0)
            {
                int ownerID;
                
                this.PaymentEngineUserControl.OwnerName = this.ownerDataTable.Rows[0]["OwnerName"].ToString();
                //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();
                int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"].ToString(), out ownerID);
                if (this.PaymentEngineUserControl.PayeeDetails==null)
                {
                    if (ownerID > 0)
                    {
                        this.PaymentEngineUserControl.PayeeDetails = this.form1410Control.WorkItem.F1019_GetPayeeDetails(ownerID);
                    }
                    else
                    {
                        this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                    }
                }
               // int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"].ToString(), out this.PaymentEngineUserControl.OwnerpayeeID);   
            }
        }

        /// <summary>
        /// PreviewButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatementGridView.CurrentCell = null;
                if (!this.messabeBoxFlag)
                {
                    DataTable statementDetailsDatatable = new DataTable();
                    int resultValue = 0;
                    DataRow[] selectedRow = this.statementDataTable.Select("IsChecked = True");
                    DataSet checkedRecord = new DataSet();
                    checkedRecord.Merge(selectedRow);
                    if (checkedRecord.Tables.Count > 0)
                    {
                        statementDetailsDatatable.Merge(checkedRecord.Tables[0]);
                        if (statementDetailsDatatable.Rows.Count > 0)
                        {
                            for (int columnCount = 0; columnCount < statementDetailsDatatable.Columns.Count; columnCount++)
                            {
                                if (statementDetailsDatatable.Columns[columnCount].ColumnName != "PaymentType" && statementDetailsDatatable.Columns[columnCount].ColumnName != "InterestDue"
                                    && statementDetailsDatatable.Columns[columnCount].ColumnName != "TaxDue" && statementDetailsDatatable.Columns[columnCount].ColumnName != "StatementID"
                                                && statementDetailsDatatable.Columns[columnCount].ColumnName != "OwnerID" && statementDetailsDatatable.Columns[columnCount].ColumnName != "DueLabel")
                                {
                                    statementDetailsDatatable.Columns.RemoveAt(columnCount);
                                    columnCount--;
                                }
                            }

                            statementDetailsDatatable.Columns.AddRange(new DataColumn[] { new DataColumn("ReceiptDate", System.Type.GetType("System.DateTime")), new DataColumn("InterestDate", System.Type.GetType("System.DateTime")) });
                            statementDetailsDatatable.Columns["ReceiptDate"].Expression = " '" + DateTime.Parse(this.ReceiptDateTextBox.Text.Trim()) + "' ";
                            statementDetailsDatatable.Columns["InterestDate"].Expression = " '" + DateTime.Parse(this.InterestDateTextBox.Text.Trim()) + "' ";
                            if (statementDetailsDatatable.Rows.Count > 0)
                            {
                                resultValue = this.form1410Control.WorkItem.F1410_SaveOwnerReceiptPreview(TerraScanCommon.UserId, TerraScanCommon.GetXmlString(statementDetailsDatatable));
                            }

                            if (resultValue > 0)
                            {
                                Hashtable reportOptionalParameter = new Hashtable();
                                reportOptionalParameter.Add("UserID", TerraScanCommon.UserId);
                                TerraScanCommon.ShowReport(141005, Report.ReportType.Preview, reportOptionalParameter);
                            }
                        }
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// AutoPrintOnButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void AutoPrintOnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AutoPrintOnButton.EnableAutoPrint)
                {
                    this.AutoPrintOnButton.EnableAutoPrint = false;
                }
                else
                {
                    this.AutoPrintOnButton.EnableAutoPrint = true;
                }

                this.form1410Control.WorkItem.SaveAutoPrint(this.ParentFormId, TerraScanCommon.UserId, this.AutoPrintOnButton.EnableAutoPrint);
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion

        #region Methods


        //used to locate the file path for attachments

        /// <summary>
        /// Used To upload the image to central location
        /// </summary>
        /// <param name="data"> The data to be uploaded.</param>
        /// <param name="strFileName"> The path of the file name.</param>
        private static void UpLoadImage(byte[] data, string strFileName)
        {
            string uploadFilePath = strFileName;
            if (!System.IO.Directory.Exists(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\"))))
            {
                // Create the directory as per the file path.
                System.IO.Directory.CreateDirectory(uploadFilePath.Substring(0, uploadFilePath.LastIndexOf("\\")));
            }

            // Used to paste the file in the specified directory.
            FileStream fileStream = new FileStream(uploadFilePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(data);
            binaryWriter.Close();
            fileStream.Close();
        }


        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            if (this.form1410Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
            {
                this.FormHeaderWorkSpace.Show(this.form1410Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
            }
            else
            {
                this.FormHeaderWorkSpace.Show(this.form1410Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
            }
            // To Load AdditionalOperationSmartPart into CommentsdeckWorkspace
            if (this.form1410Control.WorkItem.SmartParts.Contains(SmartPartNames.AdditionalOperationSmartPart))
            {
                this.CommentWorkSpace.Show(this.form1410Control.WorkItem.SmartParts.Get(SmartPartNames.AdditionalOperationSmartPart));
            }
            else
            {
                this.CommentWorkSpace.Show(this.form1410Control.WorkItem.SmartParts.AddNew<AdditionalOperationSmartPart>(SmartPartNames.AdditionalOperationSmartPart));
            }

            //////set required variable - attachment and comment
            this.additionalOperationSmartPart = (AdditionalOperationSmartPart)this.form1410Control.WorkItem.SmartParts[SmartPartNames.AdditionalOperationSmartPart];
            this.additionalOperationSmartPart.ParentWorkItem = this.form1410Control.WorkItem;
            this.additionalOperationSmartPart.ParentFormId = Convert.ToInt32(this.Tag);
            this.additionalOperationSmartPart.CurrntFormId = Convert.ToInt32(this.Tag);

            this.formLabelInfo[0] = SharedFunctions.GetResourceString("F1410FormHeader");
            this.formLabelInfo[1] = string.Empty;

            this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
        }

        /// <summary>
        /// Shows the attachment calender.
        /// </summary>
        private void ShowReceiptDateCalender()
        {
            this.ReceiptDateCalender.Visible = true;
            this.flagShift = false;
            this.ReceiptDateCalender.ScrollChange = 1;

            // Display the Calender control near the Calender Picture box.
            this.ReceiptDateCalender.Left = this.ReciptPanel.Left + this.ReceiptDatePanel.Left + this.ReceiptDateCalenderButton.Left + this.ReceiptDateCalenderButton.Width;
            this.ReceiptDateCalender.Top = this.ReciptPanel.Top + this.ReceiptDatePanel.Top + this.ReceiptDateCalenderButton.Top;
            this.ReceiptDateCalender.Tag = this.ReceiptDateCalenderButton.Tag;
            this.ReceiptDateCalender.Focus();

            if (!string.IsNullOrEmpty(this.ReceiptDateTextBox.Text))
            {
                this.ReceiptDateCalender.SetDate(Convert.ToDateTime(this.ReceiptDateTextBox.Text));
            }
        }

        /// <summary>
        /// Shows the attachment calender.
        /// </summary>
        private void ShowInterestDateCalender()
        {
            this.InterestDateCalender.Visible = true;
            this.flagShift = false;
            this.InterestDateCalender.ScrollChange = 1;
            //// Display the Calender control near the Calender Picture box.
            this.InterestDateCalender.Left = this.ReciptPanel.Left + this.InterestDatePanel.Left + this.InterestDateCalenderButton.Left + this.InterestDateCalenderButton.Width;
            this.InterestDateCalender.Top = this.ReciptPanel.Top + this.InterestDatePanel.Top + this.InterestDateCalenderButton.Top;
            this.InterestDateCalender.Tag = this.InterestDateCalenderButton.Tag;
            this.InterestDateCalender.Focus();

            if (!string.IsNullOrEmpty(this.InterestDateTextBox.Text))
            {
                this.InterestDateCalender.SetDate(Convert.ToDateTime(this.InterestDateTextBox.Text));
            }
        }

        /// <summary>
        /// This Method used to  set dataproperty name and column displayindex and paymentsdatatable initialization
        /// CustomizeErrorGridView
        /// </summary>
        private void CustomizeOwnerGridView()
        {
            this.OwnerGridView.AllowUserToResizeColumns = false;
            this.OwnerGridView.AutoGenerateColumns = false;
            this.OwnerGridView.AllowUserToResizeRows = false;
            this.OwnerGridView.StandardTab = true;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerIDColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerIDColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerNameColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerNameColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.CityColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.CityColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.StateColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.StateColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.ZipColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.ZipColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address1Column.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address1Column.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address2Column.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address2Column.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName;
            ////Added by Biju on 17/May/2010 to implement #6598
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerCodeColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerCodeColumn.ColumnName;
            ////Added by Biju on 22/Sep/2010 to implement #8507
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnershipTypeColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnershipTypeColumn.ColumnName;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.CityColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.StateColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.ZipColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address1Column.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address2Column.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            ////Added by Biju on 17/May/2010 to implement #6598
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerCodeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            ////Added by Biju on 22/Sep/2010 to implement #8507
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnershipTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName].Visible = false;
            this.OwnerGridView.Columns[this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName].Visible = false;
        }

        /// <summary>
        /// Customizes the statement grid view.
        /// </summary>
        private void CustomizeStatementGridView()
        {
            this.StatementGridView.AllowUserToResizeColumns = false;
            this.StatementGridView.AutoGenerateColumns = false;
            this.StatementGridView.AllowUserToResizeRows = false;
            this.StatementGridView.StandardTab = false;
            this.StatementGridView.AllowSorting = true;

            this.StatementGridView.PrimaryKeyColumnName = this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName;
            this.StatementGridView.Columns[SharedFunctions.GetResourceString("StatementID")].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.OwnerIDColumn.ColumnName;
            ////this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName;
            ////this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostNameColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.PostNameColumn.ColumnName;
            ////this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;  
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName;
            //// this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName;
            //// this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.MOwnerIDColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.MOwnerIDColumn.ColumnName;
            ////this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.MOwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelIDColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelIDColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostTypeIDColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.PostTypeIDColumn.ColumnName;
            this.StatementGridView.Columns[SharedFunctions.GetResourceString("IsChecked")].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName;
            //// this.StatementGridView.Columns[SharedFunctions.GetResourceString("IsChecked")].SortMode = DataGridViewColumnSortMode.Programmatic;
            ////this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.OutstandingFeesColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.OutstandingFeesColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementFormColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementFormColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName;

            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;  

            //// this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            DataTable pmttypedatatable = new DataTable();
            if (pmttypedatatable.Columns.Count == 0)
            {
                pmttypedatatable.Columns.Add("PaymentTypeid");
                pmttypedatatable.Columns.Add("PaymentType");
            }

            DataRow dr;
            dr = pmttypedatatable.NewRow();
            dr["PaymentTypeid"] = 0;
            dr["PaymentType"] = "Min";
            pmttypedatatable.Rows.Add(dr);

            dr = pmttypedatatable.NewRow();
            dr["PaymentTypeid"] = 1;
            dr["PaymentType"] = "Total";
            pmttypedatatable.Rows.Add(dr);

            (this.PaymentType as DataGridViewComboBoxColumn).DataSource = pmttypedatatable;
            (this.PaymentType as DataGridViewComboBoxColumn).DisplayMember = pmttypedatatable.Columns[1].ColumnName;
            (this.PaymentType as DataGridViewComboBoxColumn).ValueMember = pmttypedatatable.Columns[0].ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName;
            this.StatementGridView.Columns[SharedFunctions.GetResourceString("IsChecked")].ReadOnly = true;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.TaxDueColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelOwnerColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelOwnerColumn.ColumnName;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.BackgroundColorColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.BackgroundColorColumn.ColumnName;

            ////Coding Added for the Co 2804 on 14/8/2008
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.DueLabelColumn.ColumnName].DataPropertyName = this.ownerReceiptingDataSet.ListOwnerStatementTable.DueLabelColumn.ColumnName;
            ////Ends here
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].Resizable = DataGridViewTriState.False;
            this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelOwnerColumn.ColumnName].Resizable = DataGridViewTriState.False;
            this.StatementGridView.Columns["ReceiptReport"].Resizable = DataGridViewTriState.False;
        }

        /// <summary>
        /// Clears the owner.
        /// </summary>
        private void ClearOwner()
        {
            this.OwnerLinkLabel.Text = string.Empty;
            this.Address1TextBox.Text = string.Empty;
            this.Address2TextBox.Text = string.Empty;
            this.CityTextBox.Text = string.Empty;
            this.StateTextBox.Text = string.Empty;
            this.ZipTextBox.Text = string.Empty;
            ////Added by Biju on 18/May/2010 to implement #6598
            this.OwnerIDTextbox.Text = string.Empty;
            ////Added by Biju on 22/Sep/2010 to implement #8507
            this.OwnershipTypeTextbox.Text = string.Empty;
            this.OwnerGridView.DataSource = null;
            this.StatementGridView.DataSource = null;
        }

        /// <summary>
        /// Clears the receipt.
        /// </summary>
        private void ClearReceipt()
        {
            this.TaxFeeDueTextBox.Text = string.Empty;
            this.InterestDueTextBox.Text = string.Empty;
            this.ReceiptTotalTextBox.Text = string.Empty;
            this.PaymentEngineUserControl.TotalReceiptAmount = Decimal.Zero;
            this.PaymentEngineUserControl.BalanceAmount = Decimal.Zero;
            this.BalanceTextBox.Text = string.Empty;
            this.PaymentTotalTextBox.Text = string.Empty;
            this.PaymentEngineUserControl.PayeeDetails = null;  
            this.PaymentEngineUserControl.LoadPayment();
        }

        /// <summary>
        /// Controls the status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void ControlStatus(bool status)
        {
            this.OwnerPanel.Enabled = status;
            this.Address1Panel.Enabled = status;
            this.Address2Panel.Enabled = status;
            this.CityPanel.Enabled = status;
            this.StatePanel.Enabled = status;
            this.ZipPanel.Enabled = status;
            ////Added by Biju on 18/May/2010 to implement #6598
            this.OwnerIDPanel.Enabled = status;
            ////Added by Biju on 22/Sep/2010 to implement #8507
            this.OwnershipTypePanel.Enabled = status;
        }

        /// <summary>
        /// Clears the form.
        /// </summary>
        private void ClearForm()
        {
            this.ClearOwner();
            this.ControlStatus(false);
            this.ClearReceipt();
            this.ownerRowCount = 0;
            this.statementRowCount = 0;
            this.ownerReceiptingDataSet.Clear();
            this.statementDataTable.Clear();
            this.ownerDataTable.Clear();
            
            // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
            this.ReceiptDateTextBox.Text = TerraScanCommon.ReceiptDate.ToShortDateString();
            this.InterestDateTextBox.Text = TerraScanCommon.InterestDate.ToShortDateString();

            this.ReciptPanel.Enabled = false;
            this.SaveBatchButton.Enabled = false;
            this.RemoveOwnerButton.Enabled = false;
            this.StatementGridView.Enabled = false;
            this.PreviewButton.Enabled = false;
            this.OwnerGridView.Enabled = false;
            this.StatementVScrollBar.Visible = true;
            this.OwnerScrollBar.Visible = true;
        }

        /// <summary>
        /// Loads the owner grid.
        /// </summary>
        private void LoadOwnerGrid()
        {
            // Remove Empty Rows for Owner
            if (this.OwnerGridView.OriginalRowCount < this.OwnerGridView.NumRowsVisible)
            {
                DataRow[] emptyRow = this.ownerDataTable.Select(SharedFunctions.GetResourceString("EmptyRecordValidation"));
                foreach (DataRow empty in emptyRow)
                {
                    this.ownerDataTable.Rows.Remove(empty);
                }
            }

            if (this.OwnerGridView.OriginalRowCount > 0)
            {
                // Used to add the statement for current owner. 
                DataRow[] ownerDataRow = this.ownerReceiptingDataSet.ListOwnerReceiptTable.Select();
                
                foreach (DataRow owner in ownerDataRow)
                {
                    ////if condn added by Biju on 28/Sep/2010 to fix multiple owner adding issue
                    if (this.ownerDataTable.Select("OwnerID=" + owner["OwnerID"]).Length.Equals(0))
                    {
                        this.ownerDataTable.ImportRow(owner);
                        this.ownerDataTable.Rows[0][this.OwnerGridView.EmptyRecordColumnName] = "False";
                    }
                }

                if (this.ownerRowCount > 0 || this.OwnerGridView.OriginalRowCount > 0)
                {
                    this.ControlStatus(true);
                    this.OwnerGridView.Enabled = true;
                    this.OwnerGridView.DataSource = this.ownerDataTable.DefaultView;
                    TerraScanCommon.SetDataGridViewPosition(this.OwnerGridView, this.OwnerGridView.OriginalRowCount - 1);
                }
                else
                {
                    this.OwnerGridView.DataSource = null;
                    this.OwnerGridView.Enabled = false;
                }
            }
            else
            {
                this.ControlStatus(true);
                this.OwnerGridView.Enabled = true;
                this.ownerDataTable.Merge(this.ownerReceiptingDataSet.ListOwnerReceiptTable);
                if (this.ownerDataTable.Rows.Count > 0)
                {
                    this.OwnerGridView.DataSource = this.ownerDataTable.DefaultView;

                    TerraScanCommon.SetDataGridViewPosition(this.OwnerGridView, this.OwnerGridView.OriginalRowCount - 1);
                }
                else
                {
                    this.OwnerGridView.DataSource = null;
                    this.OwnerGridView.Enabled = false;
                }
            }

            if (this.OwnerGridView.OriginalRowCount > this.OwnerGridView.NumRowsVisible)
            {
                this.OwnerScrollBar.Visible = false;
            }
            else
            {
                this.OwnerScrollBar.Visible = true;
            }
        }

        /// <summary>
        /// Loads the statement grid.
        /// </summary>
        private void LoadStatementGrid()
        {
            if (!this.removeButtonClicked)
            {
                // Remove Empty Rows for Statement
                if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                {
                    DataRow[] emptyRow = this.statementDataTable.Select(SharedFunctions.GetResourceString("EmptyRecordValidation"));

                    foreach (DataRow empty in emptyRow)
                    {
                        this.statementDataTable.Rows.Remove(empty);
                    }
                }

                // Used to add the statement for current owner. 
                DataRow[] statementDataRow = this.ownerReceiptingDataSet.ListOwnerStatementTable.Select();

                foreach (DataRow statement in statementDataRow)
                {
                    if (!string.IsNullOrEmpty(statement.ItemArray[0].ToString()))
                    {
                        DataRow[] statementId = this.statementDataTable.Select(SharedFunctions.GetResourceString("StatementExpression") + statement.ItemArray[0].ToString());

                        if (statementId.Length == 0)
                        {
                            this.statementDataTable.ImportRow(statement);
                            this.statementDataTable.Rows[0][this.OwnerGridView.EmptyRecordColumnName] = "False";
                        }
                    }
                }
            }

            this.removeButtonClicked = false;

            if (this.saveBatchOperation)
            {
                ////Coding added for the issue 396 on 4/6/2009 by malliga
                IDataReader dataRead = this.checkedStatementDataTable.CreateDataReader();
                allstatementDataTable.Load(dataRead, LoadOption.OverwriteChanges);
                allstatementDataTable.DefaultView.RowFilter = "IsChecked=False";
                allstatementDataTable = allstatementDataTable.DefaultView.ToTable("allstatementDataTable");
                ////Ends here 396
                
                allstatementDataTable.Columns["PaymentType"].Expression = " '0' ";
                allstatementDataTable.Columns["IsChecked"].Expression = "True";
                allstatementDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked = True,IIF(PaymentType=1," + "TempBalanceDue" + "," + "TempMinTaxDue" + "),'0.00')";
                allstatementDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked = True, " + "TempInterestDue" + "," + "'')";


                this.statementDataTable.Clear();
                this.statementDataTable.Load(allstatementDataTable.CreateDataReader());

                if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                {
                    DataSet statementDataSet = new DataSet();
                    DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                    statementDataSet.Merge(statementDataRow);
                    this.statementDataTable.Clear();
                    if (statementDataSet.Tables.Count > 0)
                    {
                        this.statementDataTable.Merge(statementDataSet.Tables[0]);
                    } 
                    
                    int nonEmptyRow = this.statementDataTable.Rows.Count;
                    int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;
                    for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                    {
                        DataRow emptyDataRow = this.statementDataTable.NewRow();
                        emptyDataRow["EmptyRecord$"] = "True";
                        this.statementDataTable.Rows.Add(emptyDataRow);
                    }
                    
                    for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                    {
                        if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                        {
                            StatementGridView.Rows[readonlyCount].ReadOnly = true;
                        }
                        else
                        {
                            StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                            StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                            StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                            StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                        }
                    }
                }
                this.saveBatchOperation = false;
            }

            //// COding added for the issue 594 on 3/7/2009
            if (this.datechangeflag)
            {
                DataTable tempdatechagestmtDataTable = new DataTable();
                tempdatechagestmtDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();
                tempdatechagestmtDataTable.Load(this.statementDataTable.CreateDataReader());
                //Commented the Line for Co #17419 on 03/09/2012
                //Expression Setting Default pmt Type as 'min' instead it will set default value come from SP
                //tempdatechagestmtDataTable.Columns["PaymentType"].Expression = " '0' ";

                ////commented by Biju on 26-Nov-2010 to fix #9326
                ////tempdatechagestmtDataTable.Columns["IsChecked"].Expression = "True";
                ////modified by Biju on 26-Nov-2010 to fix #9326
                //tempdatechagestmtDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked = True, IIF(PaymentType=1," + "TempBalanceDue" + "," + "TempMinTaxDue" + "),0)";
                tempdatechagestmtDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked = True, " + "TempInterestDue" + "," + "'0')";
                ////till here
                this.statementDataTable.Clear();
                this.statementDataTable.Load(tempdatechagestmtDataTable.CreateDataReader());
            }
            ////End here for 594
            

            // Coding Added for Bug Id : 4386 on 16/12/2008
            if (this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows.Count > 0)
            {
                this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows.Clear();
            }

            this.ownerReceiptingDataSet.ListOwnerStatementTable.AcceptChanges();

            if (this.statementRowCount > 0 || this.StatementGridView.OriginalRowCount > 0)
            {
                this.StatementGridView.Enabled = true;
                this.StatementGridView.DataSource = this.statementDataTable.DefaultView;

                if (this.StatementGridView.OriginalRowCount > 0)
                {
                    TerraScanCommon.SetDataGridViewPosition(this.StatementGridView, 0);
                }
                else
                {
                    this.StatementGridView.CurrentCell = null;
                }

                this.PreviewButton.Enabled = true;
                this.ReciptPanel.Enabled = true;
                this.SaveBatchButton.Enabled = true && this.PermissionFiled.newPermission;
                this.RemoveOwnerButton.Enabled = true;
                this.loadstmtgridflag = true;
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1410Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
            }
            else
            {
                this.StatementGridView.DataSource = this.statementDataTable.DefaultView;
                this.StatementGridView.Enabled = false;
                this.PreviewButton.Enabled = false;
                this.ReciptPanel.Enabled = false;
                this.SaveBatchButton.Enabled = false;
                this.saveBatchOperation = false;
                this.PaymentEngineUserControl.LoadPayment();
                if (this.OwnerGridView.OriginalRowCount > 0)
                {
                    this.RemoveOwnerButton.Enabled = true;
                }
                else
                {
                    this.RemoveOwnerButton.Enabled = false;
                    this.AutoPrintOnButton.EnableAutoPrint = false;
                }

                this.loadstmtgridflag = true;
            }

            this.PaymentCaculation();
            this.checkreadonlyflag = true;
            this.CheckedColumn();

            if (this.StatementGridView.OriginalRowCount > this.StatementGridView.NumRowsVisible)
            {
                this.StatementVScrollBar.Visible = false;
            }
            else
            {
                this.StatementVScrollBar.Visible = true;

                for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                {
                    if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                    {
                        StatementGridView.Rows[readonlyCount].ReadOnly = true;
                    }
                    else
                    {
                        StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                        StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                        this.StatementGridView.Rows[readonlyCount].Cells["RollYear"].ToolTipText = this.StatementGridView.Rows[readonlyCount].Cells["StatementNumber"].Value.ToString();
                    }
                }
            }
        }       

        /// <summary>
        /// Loads the owner receipting.
        /// </summary>
        private void LoadOwnerReceipting()
        {
            if (this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows.Count > 0)
            {
                this.AttachmentAllButton.Enabled = true;
                this.CommentAllButton.Enabled = true;
            }
            this.ownerRowCount = this.ownerReceiptingDataSet.ListOwnerReceiptTable.Rows.Count;
            this.statementRowCount = this.ownerReceiptingDataSet.ListOwnerStatementTable.Rows.Count;
            this.LoadOwnerGrid();
            this.LoadStatementGrid();
        }

        /// <summary>
        /// Calculates the interest date.
        /// </summary>
        private void CalculateInterestDate()
        {
            string tempStatementXml = string.Empty;
            tempStatementXml = TerraScanCommon.GetXmlString(this.statementDataTable);
            this.ownerReceiptingDataSet.ListOwnerStatementTable.Clear();

            string currentFormBackcolor = string.Empty;
            if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0 && this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName] != null)
            {
                currentFormBackcolor = this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim();
            }

            this.ownerReceiptingDataSet.FormBackgroundTable.Clear();

            this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.ListOwnerReceipting(this.InterestDateTextBox.Text.Trim(), tempStatementXml, currentFormBackcolor));
            //this.ownerReceiptingDataSet.ListOwnerStatementTable.Merge(this.form1410Control.WorkItem.ListOwnerReceipting(this.InterestDateTextBox.Text.Trim(), tempStatementXml));
            this.statementDataTable.Clear();
            this.LoadStatementGrid();

            if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0)
            {
                Color formBackColor = this.LoadBackGroundColor(this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim());
                this.BackColor = formBackColor;
            }

            if (this.statementDataTable.Rows.Count > 0)
            {
                this.StatementGridView.CurrentCell = null;

                this.StatementGridView.Rows[0].Selected = true;
            }
            if (MinDueRadioButton.Checked)
            {
                this.MinDueRadioButton.Checked = true;
                this.BalanceRadioButton.Checked = false;
            }
            else
            {
                this.MinDueRadioButton.Checked = false;
                this.BalanceRadioButton.Checked = true;
            }
        }

        /// <summary>
        /// Saves the master confirm.
        /// </summary>
        /// <returns>Returns Boolean Value</returns>
        private bool SaveMasterConfirm()
        {
            this.flagSaveConfirmed = false;
            this.SaveBatchOperation();
            return this.flagSaveConfirmed;
        }

        /// <summary>
        /// Check the page Status when the Edit is Cancelled
        /// </summary>
        /// <returns>true if no change in page status</returns>
        private bool CheckPageStatus()
        {
            DialogResult dialogResult;
            if (this.SaveBatchButton.Enabled)
            {
                dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), " ", SharedFunctions.GetResourceString("FormClose1410"), "", "?"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    return this.SaveMasterConfirm();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the batch operation.
        /// </summary>
        private void SaveBatchOperation()
        {
            int ppayamentId = 0;
            decimal balanceAmount = this.BalanceTextBox.DecimalTextBoxValue;

            ////Coding added for the issue 396 on 4/6/2009 by malliga
            // To hold all datarows which is availed in the stmt grid
            this.allstatementDataTable.Clear();
            this.allstatementDataTable = this.statementDataTable.Copy(); 
            allstatementDataTable.DefaultView.RowFilter = "EmptyRecord$=False";
            allstatementDataTable = allstatementDataTable.DefaultView.ToTable("allstatementDataTable");
            this.allstatementDataTable.PrimaryKey = new DataColumn[] { this.allstatementDataTable.Columns[0] };
            ////Ends here for 396
                     
            if (this.BalanceTextBox.DecimalTextBoxValue == 0)
            {
                ppayamentId = this.PaymentEngineUserControl.CreatePayment();
            }
            else
            {
                if ((MessageBox.Show(SharedFunctions.GetResourceString("BatchSave"), "TerraScan T2 - Unpaid Receipt(s)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                {
                    this.flagmsg = true; 
                    return;
                                     
                }
                this.flagmsg = false; 
            }

            this.ownerReceiptingDataSet.ListOwnerStatementTable.Clear();
            DataTable statementDetailsDatatable = new DataTable();
            statementDetailsDatatable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Clone();
            statementDetailsDatatable.Load(this.statementDataTable.CreateDataReader());

            DataRow[] uncheckedRow = this.statementDataTable.Select("IsChecked=False");
            this.uncheckedDataSet.Clear();
            this.uncheckedDataSet.Merge(uncheckedRow);

            for (int columnCount = 0; columnCount < statementDetailsDatatable.Columns.Count; columnCount++)
            {
                if (statementDetailsDatatable.Columns[columnCount].ColumnName != "StatementID" && statementDetailsDatatable.Columns[columnCount].ColumnName != "MOwnerID"
                    && statementDetailsDatatable.Columns[columnCount].ColumnName != "ParcelID" && statementDetailsDatatable.Columns[columnCount].ColumnName != "OwnerID"
                    && statementDetailsDatatable.Columns[columnCount].ColumnName != "InterestDue" && statementDetailsDatatable.Columns[columnCount].ColumnName != "TaxDue"
                    && statementDetailsDatatable.Columns[columnCount].ColumnName != "IsChecked")
                {
                    statementDetailsDatatable.Columns.RemoveAt(columnCount);
                    columnCount--;
                }
            }

            this.statementDataTable.Clear();

            DataRow[] tempStatementDetailsDatatable = statementDetailsDatatable.Select("IsChecked = True");

            DataSet ds = new DataSet();
            ds.Merge(tempStatementDetailsDatatable);

            if (this.MinDueRadioButton.Checked)
            {
                if (this.BalanceTextBox.DecimalTextBoxValue == 0)
                {
                    this.receiptIdXml = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), ppayamentId, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                    //this.ownerReceiptingDataSet = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), ppayamentId, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                    this.saveBatchOperation = true;
                }
                else
                {
                    this.receiptIdXml =this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), 0, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                   // this.ownerReceiptingDataSet = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), 0, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                }

                //this.receiptId = Convert.ToInt32(this.ownerReceiptingDataSet.ReceiptIdTable.Rows[0][this.ownerReceiptingDataSet.ReceiptIdTable.ReceiptIDColumn.ColumnName].ToString());
            }
            else
            {
                if (this.BalanceTextBox.DecimalTextBoxValue == 0)
                {
                    this.receiptIdXml = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), ppayamentId, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                    //this.ownerReceiptingDataSet = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), ppayamentId, 2, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                }
                else
                {
                    this.receiptIdXml =this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), 0, 1, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                    //this.ownerReceiptingDataSet = this.form1410Control.WorkItem.F1410_SaveOwnerReceipting(TerraScanCommon.UserId, this.ReceiptDateTextBox.Text.Trim(), this.InterestDateTextBox.Text.Trim(), 0, 2, TerraScanCommon.GetXmlString(ds.Tables["ListOwnerStatementTable"]));
                }

                //this.receiptId = Convert.ToInt32(this.ownerReceiptingDataSet.ReceiptIdTable.Rows[0][this.ownerReceiptingDataSet.ReceiptIdTable.ReceiptIDColumn.ColumnName].ToString());
            }

            this.BackColor = System.Drawing.Color.White;

            this.saveBatchOperation = true;

            // TSCO 2803  General Receipting - Default Interest/Receipt Dates now global
            TerraScanCommon.InterestDate = this.InterestDateTextBox.DateTextBoxValue;
            TerraScanCommon.ReceiptDate = this.ReceiptDateTextBox.DateTextBoxValue;


            // refund payment
            if (balanceAmount == 0)
            {
                if (ppayamentId > 0 && this.PaymentEngineUserControl.RefundNow && MessageBox.Show(SharedFunctions.GetResourceString("RefundNow"), SharedFunctions.GetResourceString("RefundTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // refund management form
                    FormInfo formInfo = TerraScanCommon.GetFormInfo(1214);
                    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                }
            }

            this.checkedStatementDataTable.Clear();
            this.checkedStatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
            ////checkedStatementDataTable.DefaultView.RowFilter = "EmptyRecord$=False";
            checkedStatementDataTable = checkedStatementDataTable.DefaultView.ToTable("checkedStatementDataTable");
            this.checkedStatementDataTable.PrimaryKey = new DataColumn[] { this.checkedStatementDataTable.Columns[0] };
           
            this.LoadStatementGrid();
            this.PaymentEngineUserControl.LoadPayment();

            if (balanceAmount == 0)
            {
                // Code modified for Bug id-2651 (Report Calling) //Added by Malliga on 5/6/2008
                CommentsData ownerreceipt = new CommentsData();
                ownerreceipt = this.form1410Control.WorkItem.GetConfigDetails("TR_OwnerReceiptReport");
                this.AutoPrintOnButton.EnableAutoPrint = Convert.ToBoolean(this.form1410Control.WorkItem.GetAutoPrintStatus(this.ParentFormId, TerraScanCommon.UserId));
                if (ownerreceipt.GetCommentsConfigDetails.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(ownerreceipt.GetCommentsConfigDetails[0][ownerreceipt.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString()))
                    {
                        this.reportNumber = int.Parse(ownerreceipt.GetCommentsConfigDetails[0][ownerreceipt.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                    }
                }

                if (this.reportNumber > 0 && this.AutoPrintOnButton.EnableAutoPrint)
                {
                    this.getAutoPrintOnValue = this.form1410Control.WorkItem.GetConfigDetails("TR_AutoprintOn");

                    if (this.getAutoPrintOnValue.GetCommentsConfigDetails.Rows.Count > 0)
                    {
                        this.autoprintonoff = bool.Parse(this.getAutoPrintOnValue.GetCommentsConfigDetails[0][this.getAutoPrintOnValue.GetCommentsConfigDetails.ConfigurationValueColumn.ColumnName].ToString());
                    }

                    Hashtable reportOptionalParameter = new Hashtable();
                    reportOptionalParameter.Add("PaymentID", ppayamentId);

                    if (this.autoprintonoff)
                    {
                        try
                        {
                            TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.PrintDefault, reportOptionalParameter);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        TerraScanCommon.ShowReport(this.reportNumber, Report.ReportType.Preview, reportOptionalParameter);
                    }
                }
            }

            this.ClearForm();
            this.ReceiptTotalTextBox.Text = string.Empty;
            this.ReceiptTotalTextBox.Text = "  ";
            this.PaymentTotalTextBox.Text = string.Empty;
            this.PaymentTotalTextBox.Text = "   ";
            this.TaxFeeDueTextBox.Text = string.Empty;
            this.TaxFeeDueTextBox.Text = "   ";
            this.InterestDueTextBox.Text = "   ";
            this.BalanceTextBox.Text = "  ";
            this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
            this.BalanceTextBox.ForeColor = Color.Black;
            this.AttachmentAllButton.Enabled = false;
            this.CommentAllButton.Enabled = false;  
            this.saveBatchOperation = false;
            this.MinDueRadioButton.Checked = true;
            this.BalanceRadioButton.Checked = false;
        }

        /// <summary>
        /// Checkeds the column.
        /// </summary>
        private void CheckedColumn()
        {
            this.StatementGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            DataRow[] checkedRow;
            checkedRow = this.statementDataTable.Select(SharedFunctions.GetResourceString("IsCheckedValidation"));

            if (checkedRow.Length > 0)
            {
                this.SaveBatchButton.Enabled = true && this.PermissionFiled.newPermission;
                this.ReciptPanel.Enabled = true;
                this.PaymentEngineUserControl.Enabled = true;
                this.PreviewButton.Enabled = true;
            }
            else
            {
                this.SaveBatchButton.Enabled = false;
                this.ReciptPanel.Enabled = false;
                this.PaymentEngineUserControl.LoadPayment();
                this.PaymentEngineUserControl.Enabled = false;
                this.TaxFeeDueTextBox.Text = string.Empty;
                this.InterestDueTextBox.Text = string.Empty;
                this.ReceiptTotalTextBox.Text = string.Empty;
                this.BalanceTextBox.Text = string.Empty;
                this.PaymentTotalTextBox.Text = string.Empty;
                this.PreviewButton.Enabled = false;
                this.PaymentEngineUserControl.BalanceAmount = Decimal.Zero;
            }
        }

        /// <summary>
        /// Requireds the fields.
        /// </summary>
        /// <returns>Returns empty or not</returns>
        private bool RequiredFields()
        {
            if (string.IsNullOrEmpty(this.ReceiptDateTextBox.Text.Trim()) || string.IsNullOrEmpty(this.InterestDateTextBox.Text.Trim()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Removes the default selection.
        /// </summary>
        private void RemoveDefaultSelection()
        {
            if (this.OwnerGridView.OriginalRowCount == 0)
            {
                this.OwnerGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.OwnerGridView.RemoveDefaultSelection = false;
            }

            if (this.StatementGridView.OriginalRowCount == 0)
            {
                this.StatementGridView.RemoveDefaultSelection = true;
            }
            else
            {
                this.StatementGridView.RemoveDefaultSelection = false;
            }
        }

        /// <summary>
        ///  To enebling the sorting
        /// </summary>
        private void EnableSorting()
        {
            DataGridViewColumnCollection disableGridSortColumn = this.StatementGridView.Columns;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn["StatementOwnerID"].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn["TaxDue"].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.MOwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostTypeIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.OutstandingFeesColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementFormColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.DueLabelColumn.ColumnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            disableGridSortColumn["ReceiptReport"].SortMode = DataGridViewColumnSortMode.Programmatic;
        }

       /// <summary>
        ///  To enebling the sorting
       /// </summary>
        private void DisableSorting()
        {
            ////if (this.StatementGridView.Columns["InterestDue"].SortMode==DataGridViewColumnSortMode.Programmatic )
            ////{
            ////    this.StatementGridView.Columns["TempInterestDue"].SortMode = DataGridViewColumnSortMode.Programmatic  ;
            ////    this.StatementGridView.Columns["InterestDue"].SortMode = DataGridViewColumnSortMode.NotSortable;
            ////    this.StatementGridView.Sort(this.StatementGridView.Columns["TempInterestDue"], ListSortDirection.Ascending);
            ////}
            
           /* DataGridViewColumnCollection disableGridSortColumn = this.StatementGridView.Columns;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.IsCheckedColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["StatementOwnerID"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostNameColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PaymentTypeColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.InterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["TaxDue"].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.MinTaxDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.BalanceDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.MOwnerIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.ParcelIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.PostTypeIDColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            //disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempInterestDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempMinTaxDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.TempBalanceDueColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.OutstandingFeesColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementFormColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn[this.ownerReceiptingDataSet.ListOwnerStatementTable.DueLabelColumn.ColumnName].SortMode = DataGridViewColumnSortMode.NotSortable;
            disableGridSortColumn["ReceiptReport"].SortMode = DataGridViewColumnSortMode.NotSortable;*/
        }

        /// <summary>
        /// To calculate the payment
        /// </summary>
        private void PaymentCaculation()
        {
            decimal taxFeeDue = 0;
            decimal interestDue = 0;
            decimal balanceAmount = 0;
            #region Calculation

            this.statementDataTable.EndInit();

            // To add TaxDue values and assign to TaxFeeDue TextBox
            this.TaxFeeDueTextBox.Text = string.Empty;
            this.InterestDueTextBox.Text = string.Empty;
            if (this.statementDataTable.Rows.Count > 0)
            {
                this.TaxFeeDueTextBox.Text = this.statementDataTable.Compute("SUM(TaxDue)", "IsChecked = True").ToString();

                // To add interest values and assign to InterestDue TextBox
                    this.InterestDueTextBox.Text = this.statementDataTable.Compute("SUM(InterestDue)", "IsChecked = True").ToString();
            }

            if (!string.IsNullOrEmpty(this.TaxFeeDueTextBox.Text.ToString().Trim()))
            {
                decimal.TryParse(this.TaxFeeDueTextBox.Text.ToString().Trim(), out taxFeeDue);
            }

            if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString().Trim()))
            {
                decimal.TryParse(this.InterestDueTextBox.Text.ToString().Trim(), out interestDue);
            }

            this.ReceiptTotalTextBox.Text = (taxFeeDue + interestDue).ToString("$ #,##0.00");
            this.PaymentEngineUserControl.TotalReceiptAmount = this.ReceiptTotalTextBox.DecimalTextBoxValue;
            balanceAmount = (taxFeeDue + interestDue) - this.paymentTotal;
            this.BalanceTextBox.Text = balanceAmount.ToString("$ #,##0.00");
            this.PaymentTotalTextBox.Text = this.paymentTotal.ToString("$ #,##0.00");
            this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
            this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;

            #endregion Calculation

            if (this.BalanceTextBox.DecimalTextBoxValue == 0)
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(130, 189, 140);
                this.BalanceTextBox.ForeColor = Color.Black;
            }
            else
            {
                this.BalanceTextBox.BackColor = Color.FromArgb(128, 0, 0);
                this.BalanceTextBox.ForeColor = Color.White;
            }

            if (taxFeeDue == 0 && interestDue == 0)
            {
                this.PreviewButton.Enabled = false;
                this.SaveBatchButton.Enabled = false && this.PermissionFiled.newPermission;
                this.ClearReceipt();
                this.ReciptPanel.Enabled = false;
                this.PaymentEngineUserControl.Enabled = false;
            }
            else
            {
                this.PreviewButton.Enabled = true;
                this.SaveBatchButton.Enabled = true && this.PermissionFiled.newPermission;
                this.ReciptPanel.Enabled = true;
                this.PaymentEngineUserControl.Enabled = true;
            }
        }
        /// <summary>
        /// Sets the attachment text.
        /// </summary>
        /// <param name="additionalOperationCount">The additional operation count.</param>
        private void SetAttachmentText(AdditionalOperationCountEntity additionalOperationCount)
        {
            //int currentParcelId = (int)this.CurrentParcelId;
            //AttachmentsData attachmentDataSet = new AttachmentsData();
            //CommentsData commentDataSet = new CommentsData();
            //attachmentDataSet.GetAttachmentItems.Clear();
            //attachmentDataSet.GetAttachmentItems.Merge(this.form1410Control.WorkItem.GetAttachmentItems(2550, currentParcelId, TerraScan.Common.TerraScanCommon.UserId));
            //commentDataSet = this.form1410Control.WorkItem.GetComments(currentParcelId, 2550, TerraScanCommon.UserId);
            //this.additionalOperationCount.AttachmentCount = attachmentDataSet.GetAttachmentItems.Rows.Count;
            //this.additionalOperationCount.CommentCount = commentDataSet.GetComments.Rows.Count;
            //DataView tempDataView = new DataView(commentDataSet.GetComments);
            //tempDataView.RowFilter = string.Concat(commentDataSet.GetComments.IsHighPriorityColumn.ColumnName, "= 'HIGH'");
            //if (tempDataView.Count > 0)
            //{
            //    this.additionalOperationCount.HighPriority = true;
            //}
            //else
            //{
            //    this.additionalOperationCount.HighPriority = false;
            //}

            //this.SetAttachmentCount(this, new DataEventArgs<AdditionalOperationCountEntity>(additionalOperationCount));
            //////Added by Biju on 27/Oct/09 to make the performance better. For 9999 form related attachment/comment
            //////the DB call need not send for each row click
            //////this.SetAttachmentAllCount();
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentAllButton.Text = "Attach All";
                }
                else
                {
                    this.AttachmentAllButton.Text = "Attach All" + "(" + additionalOperationCountEntity.AttachmentCount + ")";
                }
            }

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

        #endregion

        /// <summary>
        /// Handles the CellContentClick event of the OwnerGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void OwnerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (this.ownerRowCount > 0 || this.saveOption)
                    {
                        if (e.RowIndex >= 0)
                        {
                            this.OwnerLinkLabel.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerNameColumn.ColumnName].Value.ToString();
                            this.Address1TextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address1Column.ColumnName].Value.ToString();
                            this.Address2TextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.Address2Column.ColumnName].Value.ToString();
                            this.CityTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.CityColumn.ColumnName].Value.ToString();
                            this.StateTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.StateColumn.ColumnName].Value.ToString();
                            this.ZipTextBox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.ZipColumn.ColumnName].Value.ToString();
                            ////Added by Biju on 18/May/2010 to implement #6598
                            this.OwnerIDTextbox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnerCodeColumn.ColumnName].Value.ToString();
                            ////Added by Biju on 22/Sep/2010 to implement #8507
                            this.OwnershipTypeTextbox.Text = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.OwnershipTypeColumn.ColumnName].Value.ToString();
                            this.ownerLow = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.LowColumn.ColumnName].Value.ToString();

                            this.ownerHigh = this.OwnerGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerReceiptTable.HighColumn.ColumnName].Value.ToString();

                            int.TryParse(this.ownerLow, out this.ownerLowVal);
                            int.TryParse(this.ownerHigh, out this.ownerHighVal);
                            if (this.ownerLowVal > 0)
                            {
                                OwnerStatusLinkLabel.Visible = true;
                                OwnerStatusLinkLabel.Text = "Status";
                                OwnerStatusLinkLabel.LinkColor = Color.FromArgb(0, 0, 255);
                                OwnerPanel.BackColor = Color.FromArgb(200, 214, 230);
                            }

                            if (this.ownerHighVal > 0)
                            {
                                OwnerStatusLinkLabel.Visible = true;
                                OwnerStatusLinkLabel.Text = "Status";
                                OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 0, 0);
                                OwnerPanel.BackColor = Color.FromArgb(237, 205, 203);
                            }

                            if (((this.ownerLowVal == 0) && (this.ownerHighVal == 0)) || ((this.ownerLowVal > 0) && (this.ownerHighVal > 0)))
                            {
                                OwnerStatusLinkLabel.Visible = false;
                                OwnerStatusLinkLabel.Text = "Status";
                                OwnerStatusLinkLabel.LinkColor = Color.FromArgb(255, 255, 255);
                                OwnerPanel.BackColor = Color.FromArgb(255, 255, 255);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the cell leave event for datagridview
        /// </summary>
        /// <param name="sender">statement datagridview</param>
        /// <param name="e">datagridview cell event args</param>
        private void StatementGridView_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string statementId = string.Empty;
                this.selectedColumn = e.ColumnIndex;
                // To hold edited rows.
                this.modifiedRowCollectionDataTable = this.statementDataTable; 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles cell tool tip event for datagridview
        /// </summary>
        /// <param name="sender">statement datagridview</param>
        /// <param name="e">datagridview cell tool tip event args</param>
        private void StatementGridView_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == this.StatementGridView.Columns[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].Index)
                    {
                        if (!string.IsNullOrEmpty(this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementIDColumn.ColumnName].Value.ToString()))
                        {
                            this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.RollYearColumn.ColumnName].ToolTipText
                                = this.StatementGridView.Rows[e.RowIndex].Cells[this.ownerReceiptingDataSet.ListOwnerStatementTable.StatementNumberColumn.ColumnName].Value.ToString();
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Arrow;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the MinDueRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MinDueRadioButton_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!this.messabeBoxFlag || this.selectedColumn != this.StatementGridView.Columns["TaxDue"].Index)
                {
                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        DataTable expressionDataTable = new DataTable();
                        expressionDataTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked=True,TempMinTaxDue,'0.00')";
                        expressionDataTable.Columns["paymentType"].Expression = "IIF(StatementID<>0,'0','-1')";
                        expressionDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked=True,TempInterestDue,'0.00')";
                        this.restrictRowEnter = true;
                        this.statementDataTable.Load(expressionDataTable.CreateDataReader());
                        this.restrictRowEnter = false;

                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;

                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.StatementGridView.CurrentCell = null;
                        this.StatementGridView.Rows[this.editedRowIndex].Selected = true;
                    }

                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    this.MinDueRadioButton.Checked = true;

                    #region Bugid 5381

                    //this.EnableSorting();

                    #endregion Bugid 5381
                }
                else
                {
                    if (this.balanceRadioButton)
                    {
                        this.BalanceRadioButton.Checked = true;
                    }
                    else
                    {
                        this.MinDueRadioButton.Checked = true;
                    }

                    this.StatementGridView.Focus();
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseClick event of the MinDueRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void MinDueRadioButton_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (!this.messabeBoxFlag || this.selectedColumn != this.StatementGridView.Columns["TaxDue"].Index)
                {
                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        DataTable expressionDataTable = new DataTable();
                        expressionDataTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked=True,TempMinTaxDue,'0.00')";
                        expressionDataTable.Columns["paymentType"].Expression = "IIF(StatementID<>0,'0','-1')";
                        expressionDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked=True,TempInterestDue,'0.00')";
                        this.restrictRowEnter = true;
                        this.statementDataTable.Load(expressionDataTable.CreateDataReader());
                        this.restrictRowEnter = false;

                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;

                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.StatementGridView.CurrentCell = null;
                        this.StatementGridView.Rows[this.editedRowIndex].Selected = true;
                    }

                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    this.MinDueRadioButton.Checked = true;

                    #region Bugid 5381

                    //this.EnableSorting();

                    #endregion Bugid 5381
                }
                else
                {
                    if (this.balanceRadioButton)
                    {
                        this.BalanceRadioButton.Checked = true;
                    }
                    else
                    {
                        this.MinDueRadioButton.Checked = true;
                    }

                    this.StatementGridView.Focus();
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Coding added for the issue 693
        /// <summary>
        /// Handles the KeyUp event of the MinDueRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void MinDueRadioButton_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!this.messabeBoxFlag || this.selectedColumn != this.StatementGridView.Columns["TaxDue"].Index)
                    {
                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        DataTable expressionDataTable = new DataTable();
                        expressionDataTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked=True,TempMinTaxDue,'0.00')";
                        expressionDataTable.Columns["paymentType"].Expression = "IIF(StatementID<>0,'0','-1')";
                        expressionDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked=True,TempInterestDue,'0.00')";
                        this.restrictRowEnter = true;
                        this.statementDataTable.Load(expressionDataTable.CreateDataReader());
                        this.restrictRowEnter = false;

                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;

                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.StatementGridView.CurrentCell = null;
                    this.StatementGridView.Rows[this.editedRowIndex].Selected = true;
                    }

                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    this.MinDueRadioButton.Checked = true;

                    #region Bugid 5381

                    //this.EnableSorting();

                    #endregion Bugid 5381
                }
                else
                {
                    if (this.balanceRadioButton)
                    {
                        this.BalanceRadioButton.Checked = true;
                    }
                    else
                    {
                        this.MinDueRadioButton.Checked = true;
                    }

                  ////  this.StatementGridView.Focus();
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the PreviewKeyDown event of the MinDueRadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs"/> instance containing the event data.</param>
        private void MinDueRadioButton_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (!this.messabeBoxFlag || this.selectedColumn != this.StatementGridView.Columns["TaxDue"].Index)
                {
                    if (this.statementDataTable.Rows.Count > 0)
                    {
                        DataTable expressionDataTable = new DataTable();
                        expressionDataTable.Merge(this.statementDataTable);
                        this.statementDataTable.Clear();
                        expressionDataTable.Columns["TaxDue"].Expression = "IIF(IsChecked=True,TempMinTaxDue,'0.00')";
                        expressionDataTable.Columns["paymentType"].Expression = "IIF(StatementID<>0,'0','-1')";
                        expressionDataTable.Columns["InterestDue"].Expression = "IIF(IsChecked=True,TempInterestDue,'0.00')";
                        this.restrictRowEnter = true;
                        this.statementDataTable.Load(expressionDataTable.CreateDataReader());
                        this.restrictRowEnter = false;

                        if (this.StatementGridView.OriginalRowCount < this.StatementGridView.NumRowsVisible)
                        {
                            DataSet statementDataSet = new DataSet();
                            DataRow[] statementDataRow = this.statementDataTable.Select("StatementID<>0");
                            statementDataSet.Merge(statementDataRow);
                            this.statementDataTable.Clear();
                            if (statementDataSet.Tables.Count > 0)
                            {
                                this.statementDataTable.Merge(statementDataSet.Tables[0]);
                            }

                            int nonEmptyRow = this.statementDataTable.Rows.Count;
                            int emptyRow = this.StatementGridView.NumRowsVisible - nonEmptyRow;

                            for (int emptyRowCount = 0; emptyRowCount < emptyRow; emptyRowCount++)
                            {
                                DataRow emptyDataRow = this.statementDataTable.NewRow();
                                emptyDataRow["EmptyRecord$"] = "True";
                                this.statementDataTable.Rows.Add(emptyDataRow);
                            }

                            for (int readonlyCount = 0; readonlyCount < this.StatementGridView.NumRowsVisible; readonlyCount++)
                            {
                                if (string.IsNullOrEmpty(StatementGridView.Rows[readonlyCount].Cells["StatementID"].Value.ToString()))
                                {
                                    StatementGridView.Rows[readonlyCount].ReadOnly = true;
                                }
                                else
                                {
                                    StatementGridView.Rows[readonlyCount].Cells["PaymentType"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["IsChecked"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["InterestDue"].ReadOnly = false;
                                    StatementGridView.Rows[readonlyCount].Cells["TaxDue"].ReadOnly = false;
                                }
                            }
                        }

                        this.StatementGridView.CurrentCell = null;
                     this.StatementGridView.Rows[this.editedRowIndex].Selected = true;
                    }

                    this.statementDataTable.EndInit();
                    this.PaymentCaculation();
                    this.MinDueRadioButton.Checked = true;

                    #region Bugid 5381

                    //this.EnableSorting();

                    #endregion Bugid 5381
                }
                else
                {
                    if (this.balanceRadioButton)
                    {
                        this.BalanceRadioButton.Checked = true;
                    }
                    else
                    {
                        this.MinDueRadioButton.Checked = true;
                    }

                  ////  this.StatementGridView.Focus();
                }

                this.messabeBoxFlag = false;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
       #endregion
        /// <summary>
        /// Added by Biju on 23-Sep-2010 to implement #8507
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddParcelsButton_Click(object sender, EventArgs e)
        {
            try
            {
                // To Leave the focus from stmtGrid while clicking the AddOwnerButton after edting the stmtGrid BugId 5381 -- Ramya.D
                this.StatementGridView.CurrentCell = null;

                // To check whether the messagebox is appeared or not BugId 5381 -- Ramya.D
                if (!this.messabeBoxFlag)
                {
                    DataRow[] dr = null;
                    string currentParcelIds = string.Empty;
                    Form parcelF1403 = new Form();
                    ////object[] optionalParameter = new object[] { 91000 };
                    parcelF1403 = TerraScanCommon.GetForm(1411, null, this.form1410Control.WorkItem);

                    if (parcelF1403 != null)
                    {
                        if (parcelF1403.ShowDialog() == DialogResult.OK )
                        {
                            this.checkreadonlyflag = false;
                            currentParcelIds  = TerraScanCommon.GetValue(parcelF1403, "CommandResult");
                            DataSet currentParcelsDataTable = new DataSet();
                            //25 Aug 11         Manoj Kumar         Change in StatementSearch button and Fix attachment path Copy updation.
                            currentParcelsDataTable.ReadXml(TerraScan.Utilities.SharedFunctions.XmlParser(currentParcelIds));

                            // this.ownerDataTable.Merge(currentownerDataTable.Tables[0], true);
                          
                            if (currentParcelsDataTable.Tables[0].Rows.Count > 0)
                            {
                                this.ownerId = Convert.ToInt32(currentParcelsDataTable.Tables[0].Rows[0]["StatementID"]);
                               
                            }

                            if (this.ownerDataTable != null)
                            {
                                if (this.ownerDataTable.Rows.Count > 0)
                                {
                                    ////commented by Biju on 28/Sep/2010 as it is not required
                                    ////if (currentParcelsDataTable.Tables[0].Rows.Count > 0)
                                    ////{
                                    ////    for (int ownerCount = 0; ownerCount < currentParcelsDataTable.Tables[0].Rows.Count; ownerCount++)
                                    ////    {
                                    ////        this.ownerId = Convert.ToInt32(currentParcelsDataTable.Tables[0].Rows[ownerCount]["StatementID"]);
                                    ////        dr = this.ownerDataTable.Select(SharedFunctions.GetResourceString("OwnerIdExpression") + this.ownerId.ToString());
                                    ////    }
                                    ////}
                                }
                            }

                            if (this.ownerRowCount > 0)
                            {
                                ////commented by Biju on 28/Sep/2010 as it is not required
                                ////if (dr.Length == 0)
                                ////{
                                    this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                                    this.ownerReceiptingDataSet.FormBackgroundTable.Clear();
                                    this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(), null, currentParcelIds));

                                    this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                    this.LoadOwnerReceipting();
                                    DataRow[] checkedCount = this.statementDataTable.Select("IsChecked=True");
                                    if (checkedCount.Length > 0 && this.tempstatementDataTable.Rows.Count > 0)
                                    {
                                        decimal balance = this.BalanceTextBox.DecimalTextBoxValue;
                                        this.autoTenderType(balance);
                                    }
                                       
                                ////commented by Biju on 28/Sep/2010 as it is not required
                                ////}
                                ////else
                                ////{
                                ////    MessageBox.Show(SharedFunctions.GetResourceString("OwnerExixts"), "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ////}
                            }
                            else
                            {
                                this.ownerReceiptingDataSet.ListOwnerReceiptTable.Clear();
                                this.ownerReceiptingDataSet.FormBackgroundTable.Clear();
                                this.ownerReceiptingDataSet.Merge(this.form1410Control.WorkItem.GetOwnerReceipting(this.InterestDateTextBox.Text.Trim(),null, currentParcelIds ));
                                this.tempstatementDataTable = this.ownerReceiptingDataSet.ListOwnerStatementTable.Copy();
                                this.LoadOwnerReceipting();
                                DataRow[] checkedCount = this.statementDataTable.Select("IsChecked=True");
                                if (checkedCount.Length > 0 && this.tempstatementDataTable.Rows.Count > 0)
                                {
                                    decimal balance = this.BalanceTextBox.DecimalTextBoxValue;
                                    this.autoTenderType(balance);
                                }
                                if (this.OwnerGridView.OriginalRowCount > 0)
                                {
                                    this.PaymentEngineUserControl.OwnerName = this.ownerDataTable.Rows[0]["OwnerName"].ToString();
                                    //this.PaymentEngineUserControl.OwnerPayeeID = this.ownerDataTable.Rows[0]["OwnerID"].ToString();    
                                }

                                this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                                this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;
                            }

                            if (this.ownerReceiptingDataSet.FormBackgroundTable.Rows.Count > 0)
                            {
                                Color formBackColor = this.LoadBackGroundColor(this.ownerReceiptingDataSet.FormBackgroundTable.Rows[0][this.ownerReceiptingDataSet.FormBackgroundTable.FormBackgroundColorColumn.ColumnName].ToString().Trim());
                                this.BackColor = formBackColor;
                            }

                            #region Sorting BugId 5381 -- Ramya.D

                           /* if (this.gridclickflag)
                            {
                                this.DisableSorting();
                            }
                            else
                            {
                                this.EnableSorting();
                            }*/
                            #endregion Sorting
                        }
                    }

                    OwnerLinkLabel.Focus();
                    if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString()))
                    {
                        this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                    }

                    if (this.sortedColumnIndex >= 0)
                    {
                        if (this.statmentSortOrder.Equals(SortOrder.Descending))
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                        }
                        else
                        {
                            this.StatementGridView.Columns[this.sortedColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        }
                    }
                }
                else
                {
                    if (this.editedRowIndex >= 0)
                    {
                        this.StatementGridView.CurrentCell = this.StatementGridView[this.StatementGridView.Columns["TaxDue"].Index, this.editedRowIndex];
                    }
                }

               /* if (!this.gridclickflag)
                {
                    this.EnableSorting();
                }
                else
                {
                    this.DisableSorting();
                }*/

                this.messabeBoxFlag = false;

               
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

        private void AttachmentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { 99999, 0, 99999 };

                Form attachmentForm = new Form();
                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.additionalOperationSmartPart.CurrntFormId).openPermission))
                {
                    attachmentForm = this.form1410Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9005, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
                    attachmentForm.Tag = this.additionalOperationSmartPart.CurrntFormId;
                    if (attachmentForm != null)
                    {
                        attachmentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        ////AdditionalOperationCountEntity additionalOperationCountEnt;
                        ////this.additionalOperationCountEnt = new AdditionalOperationCountEntity(-9999, -9999, false);
                        this.additionalOperationCountEnt.AttachmentCount = Convert.ToInt32(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
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

     ///created for file path.
        /// <summary>
        /// Copy all the attached files 
        /// </summary>
        private void CopyAttachment()
        {
           ///25 Aug 11  Manoj Kumar Fix attachment path Copy updation.
            this.ownerReceiptingDataSet  = this.form1410Control.WorkItem.F1410_ListAttachmentDetails(Convert.ToInt32("11001"), this.receiptIdXml, TerraScanCommon.UserId, Convert.ToInt32("1410"));
            if (this.ownerReceiptingDataSet.ListAttachment.Rows.Count > 0)
            {
                for (int i = 0; i < this.ownerReceiptingDataSet.ListAttachment.Rows.Count; i++)
                {
                    string fileTypeId = string.Empty;

                    //this.attachmentKeyID = Convert.ToInt32(this.ownerReceiptingDataSet.ListAttachment.Rows[i][this.ownerReceiptingDataSet.ListAttachment.NewKeyIDColumn].ToString());
                    //this.attachmentFormID = Convert.ToInt32("11001");
                   // this.browsePathExt = this.ownerReceiptingDataSet.ListAttachment.Rows[i][this.ownerReceiptingDataSet.ListAttachment.ExtensionColumn].ToString();
                   // fileTypeId = this.ownerReceiptingDataSet.ListAttachment.Rows[i][this.ownerReceiptingDataSet.ListAttachment.FileTypeIDColumn].ToString();

                   // this.attachmentDataSet.GetFilePath.Clear();
                    //this.attachmentDataSet.GetFilePath.Merge(this.form1410Control.WorkItem.GetFilePath("TSFile", this.attachmentFormID, this.attachmentKeyID, this.browsePathExt));
                   // this.filePath = this.attachmentDataSet.GetFilePath.Rows[0]["FilePath"].ToString();
                   // this.fileID = this.attachmentDataSet.GetFilePath.Rows[0]["FileID"].ToString();
                    this.filePath = this.ownerReceiptingDataSet.ListAttachment.Rows[i][this.ownerReceiptingDataSet.ListAttachment.AurlColumn].ToString();
                    ///25 Aug 11  Manoj Kumar Fix attachment path Copy updation.
                    string tempFilePath = string.Empty;
                    tempFilePath = this.ownerReceiptingDataSet.ListAttachment.Rows[i][this.ownerReceiptingDataSet.ListAttachment.SourceColumn].ToString();

                    try
                    {
                        if (System.IO.File.Exists(tempFilePath))
                        {
                            FileStream fs = new FileStream(tempFilePath, FileMode.Open);
                            BinaryReader bR = new BinaryReader(fs);

                            // Upload the Image to the Central Location.
                            UpLoadImage(bR.ReadBytes((int)fs.Length), this.filePath);
                            this.fileExist = true;
                            bR.Close();
                            fs.Close();
                        }
                    }
                    catch (UnauthorizedAccessException)
                    {
                        MessageBox.Show(String.Concat(new string[] { SharedFunctions.GetResourceString("NoAccessFileLocation"), "\n", tempFilePath, "\n", SharedFunctions.GetResourceString("ContactAdmin") }), SharedFunctions.GetResourceString("TS_File"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                this.DeleteAttachmentAndComment();
            }
            else if (this.additionalOperationCountEnt.CommentCount > 0)
            {
                if (!string.IsNullOrEmpty(this.receiptIdXml))
                {
                  this.DeleteAttachmentAndComment();
                }
            }
        }


        /// <summary>
        /// Deletes the attachment and comment for 9999.
        /// </summary>
        private void DeleteAttachmentAndComment()
        {
            this.form1410Control.WorkItem.F2550_DeleteAttachmentDetails(Convert.ToInt32("99999"));
            this.additionalOperationCountEnt.AttachmentCount = 0;
            this.additionalOperationCountEnt.CommentCount = 0;
            this.additionalOperationCountEnt.HighPriority = false;
            this.SetText(this.additionalOperationCountEnt);
        }

        #region coding Added for the CO:11654
        
        /// <summary>
        /// Loads the color of the back ground.
        /// </summary>
        private Color LoadBackGroundColor(string colorToValidate)
        {
            string backcolor;
            string[] backcolorArr = null;
            int RColor;
            int GColor;
            int BColor;

            // If Background color is not empty
            if (!string.IsNullOrEmpty(colorToValidate.Trim()))
            {
                // Assigning backcolor value
                backcolor = colorToValidate;
                if (!string.IsNullOrEmpty(backcolor))
                {
                    char[] splitchar = { ',' };
                    backcolorArr = backcolor.Split(splitchar);
                    if (backcolorArr.Length.Equals(3))
                    {
                        // Red Color
                        if (string.IsNullOrEmpty(backcolorArr[0]))
                        {
                            RColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[0], out RColor);
                        }

                        // Green Color
                        if (string.IsNullOrEmpty(backcolorArr[1]))
                        {
                            GColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[1], out GColor);
                        }

                        // Blue Color
                        if (string.IsNullOrEmpty(backcolorArr[2]))
                        {
                            BColor = 255;
                        }
                        else
                        {
                            int.TryParse(backcolorArr[2], out BColor);
                        }

                        // Assign RGB value to backcolor
                        if (RColor < 0 || RColor > 255 || GColor < 0 || GColor > 255 || BColor < 0 || BColor > 255)
                        {
                            RColor = 255;
                            GColor = 255;
                            BColor = 255;
                        }

                        return Color.FromArgb(RColor, GColor, BColor);
                    }
                    else
                    {
                        return Color.White;
                    }
                }
                else
                {
                    return Color.White;
                }
            }
            else
            {
                return Color.White;
            }
        }

        #endregion

        /// <summary>
        /// Used to set AutotenderType Check.
        /// </summary>
         private void autoTenderType(decimal balance)
          {
              int ownerID;
              int.TryParse(this.ownerDataTable.Rows[0]["OwnerID"].ToString(), out ownerID);
               if (ownerID > 0)
                  {
                      this.PaymentEngineUserControl.PayeeDetails = this.form1410Control.WorkItem.F1019_GetPayeeDetails(ownerID);
                  }
                  else
                  {
                      this.PaymentEngineUserControl.PayeeDetails = new PaymentEngineData();
                  }
             //Removed the tender Type default 'check' TSCO #13410.
              // this.PaymentEngineUserControl.LoadDefultValue(this.ownerDataTable.Rows[0]["OwnerName"].ToString(), balance);
                   this.PaymentEngineUserControl.TotalDue = this.ReceiptTotalTextBox.DecimalTextBoxValue;
                  this.PaymentEngineUserControl.BalanceAmount = this.BalanceTextBox.DecimalTextBoxValue;
                  if (!string.IsNullOrEmpty(this.InterestDueTextBox.Text.ToString()))
                  {
                      this.interestDueTemp = Convert.ToDouble(this.InterestDueTextBox.Text.ToString().Trim());
                  }
              
                                                  
         }

        private void CommentAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter;

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(1410).openPermission))
                {
                    optionalParameter = new object[] { 99999, 0, 99999 };

                    Form commentForm = new Form();
                    commentForm = this.form1410Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9075, optionalParameter, this.additionalOperationSmartPart.ParentWorkItem);
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
    }
}
