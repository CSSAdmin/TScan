//--------------------------------------------------------------------------------------------
// <copyright file="PaymentEngineUserControl.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created
// 16 May 06        Thilakraj        Component for Payment Engine.
// 22 June 07       Guhan            Added Property for ToolTip 
// 27 July 07       Jayanthi         Fixed 2 issues in TFS(Bug Id 139 and 141)    
// 13 Aug  07       Guhan            Added Property for readonlyColumn
// 15 Feb 09        Malliga          Auto-fill payment grid in instrument header(CO : 5880)
// 25 Aug 11        Manoj            Amount Value Update in Instrument Payment Engine (bug ID :12320)
//***************************************this.exemptionData.GetSeniorExemptionTypeTable******************************************/
namespace TerraScan.PaymentEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;   
    using TerraScan.Common;
    using TerraScan.BusinessEntities;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using System.Collections;
    using D1018 ;

    /// <summary>
    /// Usercontrol for Payment Engine
    /// </summary>
    public partial class PaymentEngineUserControl : TerraScan.Common.UserControlBasePage
    {
        #region variable

        /// <summary>
        /// used to store all pids
        /// </summary>
        private DataSet pidMainDataSet = new DataSet();

        /// <summary>
        /// used to find Row index in the datatable
        /// </summary>
        private BindingSource pidSource = new BindingSource();

        /// <summary>
        /// used to assign the image
        /// </summary>
        private Image sourceImage;

        /// <summary>
        /// RowHeight 
        /// </summary>
        private int rowHeight = 0;

        /// <summary>
        /// used to Make readonly particular row
        /// </summary>
        private bool rowLock;

        /// <summary>
        /// used to store values of ppaymentid
        /// </summary>
        private int ppaymentId1;

        /// <summary>
        /// used to store values of ppaymentid
        /// </summary>
        private int ppaymentId;

        /// <summary>
        /// It contains the values for number of rows to be displayed in grid
        /// </summary>
        private int rowsVisibleNo = 3;

        /// <summary>
        /// used to identify the payment grid lock value
        /// </summary>
        private bool locked;

        /// <summary>
        /// used to identify the payment grid lock value
        /// </summary>
        private bool instrumentPayment;

        /// <summary>
        /// used to identify the payment grid lock value
        /// </summary>
        private string instrumentBalanceAmount;

        /// <summary>
        /// used to identify the payment grid lock value
        /// </summary>
        private bool amountFiled;

        /// <summary>
        /// used to apply Amount Resolution
        /// </summary>
        private bool applyAmountResolution;

        /// <summary>
        /// It contains total of payment Grid
        /// </summary>
        private decimal amountTotal;

        /// <summary>
        /// It contains total receipt amount
        /// </summary>
        private decimal totalReceiptAmount;

        /// <summary>
        /// It contains the owner name of the current receipt
        /// </summary>
        private string ownerName;

        ///<summary>
        /// IT contains the ownerID of the current receipt
        /// </summary>
        private string Owner    ;

        /// <summary>
        /// object for PaymentEngine typed dataset
        /// </summary>
        private PaymentEngineData paymentEngineDataSet = new PaymentEngineData();

        private F1019 payeeForm = new F1019();  
        /// <summary>
        /// object for PaymentEngine typed dataset
        /// </summary>
        private F49910InstrumentHeaderDataSet instrumentHeaderDataSet = new F49910InstrumentHeaderDataSet();

        /// <summary>
        /// Creating an Instance of Datatable
        /// </summary>
        private DataTable paymentsDataTable = new DataTable();

        /// <summary>
        /// Creating an Instance of Datatable
        /// </summary>
        private DataTable instrumentPaymentsDataTable = new DataTable();

        /// <summary>
        /// tenderTypeDataTable make private because it will be used in Add method to get type ID
        /// </summary>
        private DataTable tenderTypeDataTable = new DataTable();

        /// <summary>
        /// tenderTypeDataTable make private because it will be used in Add method to get type ID
        /// </summary>
        private DataTable instrumentTenderTypeDataTable = new DataTable();

        /// <summary>
        /// Contain OverUnderMaxAmount from configruation
        /// </summary>
        private decimal overUnderMaxAmount;

        /// <summary>
        /// Contain OverUnderMinAmount from configruation
        /// </summary>
        private decimal overUnderMinAmount;

        /// <summary>
        /// Assigning Empty to parentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;

        /// <summary>
        /// indicate the state of loading of value in form.
        /// </summary>
        private bool loadComplete = false;

        /// <summary>
        /// indicate the state to issue refund or not.
        /// </summary>
        private bool refundNow;

        /// <summary>
        /// indicate the state to issue refund or not.
        /// </summary>
        private bool amountTxtBox;

        /// <summary>
        /// allow over under for tendertype
        /// </summary>
        private bool allowOverUnder = true;

        /// <summary>
        /// ppidVisible
        /// </summary>
        private bool pidVisible = true;

        /// <summary>
        /// ppidVisible
        /// </summary>
        private bool ppidVisible = true;

        /// <summary>
        /// orginalWidth
        /// </summary>
        private int orginalWidth = 773;

        /// <summary>
        /// scrollBarPosition
        /// </summary>
        private int scrollBarPosition = 757;

        /// <summary>
        /// autoResizeWithOutppId
        /// </summary>
        private bool autoResizeWithOutppId;

        /// <summary>
        /// autoResizeWithppId
        /// </summary>
        private bool autoResizeWithppId;

        /// <summary>
        /// dataGridViewColumnWidth
        /// </summary>
        private PaymentEngineColumnWidth dataGridViewColumnWidth = new PaymentEngineColumnWidth();

        /// <summary>
        /// autoResizeWithppId
        /// </summary>
        private bool restrictedTenderType = false;

        /// <summary>
        /// autoResizeWithppId
        /// </summary>
        private string[] availableTenderType;

        /// <summary>
        /// autoResizeWithppId
        /// </summary>
        private bool applyToolTip = false;

        /// <summary>
        /// applyReadOnlycolumn
        /// </summary>
        public bool applyReadonlyColumn = false;

        /// <summary>
        /// applyReadOnlycolumn
        /// </summary>
        public bool applyAmountColumn = false;

        /// <summary>
        /// currentCell
        /// </summary>
        public int previousRow = -1;

        private bool Isedit=true;

        private bool IsLeave = false;

        private bool CellLeave = false;

        /// <summary>
        /// boolean for IsDelete
        /// </summary>
        public bool isDelete = true;

        /// <summary>
        /// Flag for UnPaid Receipt form
        /// </summary>
        private bool isAutoPayment = false;

        /// <summary>
        /// Total Balance amount to be paid
        /// </summary>
        private decimal totalBalanceAmount;

        /// <summary>
        /// Total Due amount
        /// </summary>
        private decimal totalDue;

        /// <summary>
        /// variable to hold the flag for edit
        /// </summary>
        private bool edit=true;

        private bool isAutoSelect = true;

        /// <summary>
        /// Variable to hold the Changed Owner Name
        /// </summary>
        private bool changedOwnerName = false;

        /// <summary>
        /// Flag for allow to save payment items with zero.
        /// </summary>
        private bool allowZeroPayment = false;


        private PaymentEngineUserController paymentForm= new PaymentEngineUserController();

        private F1019Controller form1019Control = new F1019Controller();

        private PaymentEngineData payeeDetails;

        private WorkItem payeeWorkItem;

        private bool nonEditable=false;

        private bool IsOverUnder = false;

        private bool IsEditLeave = false;

        /// <summary>
        /// variable Holds the ParentWorkItem
        /// </summary>
        
        #endregion variable

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PaymentEngineUserControl"/> class.
        /// </summary>
        public PaymentEngineUserControl()
        {
            this.loadComplete = false;
            this.InitializeComponent();
            this.loadComplete = true;
            ////Commented By Guhan Since its Access byt the property
            ////this.RowsVisibleNo = 3;            
            this.dataGridViewColumnWidth.PropertyChanged += new PropertyChangedEventHandler(this.DataGridViewColumnWidth_PropertyChanged);
        }

        #endregion Constructors

        #region Delegetes

        /// <summary>
        /// Declare delegate for Payment Amount change.
        /// </summary>
        /// <param name="amount">Payment Amount Total</param>       
        public delegate void PaymentAmountChangeEventHandler(decimal amount);

        /// <summary>
        /// Declare delegate for Payment Item change.
        /// </summary>
        public delegate void PaymentItemChangeEventHandler();

        /// <summary>
        /// Declare delegate for Combobox value changes
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">e</param>
        public delegate void PaymenItemControlShowingEventHandler(object sender, DataGridViewEditingControlShowingEventArgs e);

        #region eventDecalration
        /// <summary>
        /// Declare the event, which is associated with the
        /// delegate PaymentAmountEventHandler(decimal).  
        /// </summary>          
        [Description("Fires when the Payment Amount Changes")]
        public event PaymentAmountChangeEventHandler PaymentAmountChangeEvent;

        /// <summary>
        /// Declare the event, which is associated with the
        /// delegate PaymentItemEventHandler().  
        /// </summary>          
        [Description("Fires when the Payment Item Changes")]
        public event PaymentItemChangeEventHandler PaymentItemChangeEvent;

        /// <summary>
        /// Declare the event for Combobox value changes
        /// </summary>
        public event PaymenItemControlShowingEventHandler PaymentItemsGridEditingControlShowing;

        #endregion

        #endregion

        #region Property

        /// <summary>
        ///  Applying ReadonlyColumn
        /// </summary>
        public bool ApplyReadonlyColumn
        {
            get
            {
                return this.applyReadonlyColumn;
            }

            set
            {
                this.applyReadonlyColumn = value;

                if (!value)
                {
                    this.LockGrid(value);
                }
            }
        }

        /// <summary>
        ///  Applying tool tip for Datagrid
        /// </summary>
        public bool ApplyToolTip
        {
            get { return this.applyToolTip; }
            set { this.applyToolTip = value; }
        }

        /// <summary>
        /// source image
        /// </summary>
        public Image SourceImage
        {
            get
            {
                return this.sourceImage;
            }

            set
            {
                this.sourceImage = value;
                if (this.restrictedTenderType)
                {
                    ////this.PaymentPictureBox.Image = value;
                }
            }
        }

        [CreateNew]
        public PaymentEngineUserController  PaymentForm
        {
            get { return this.paymentForm as PaymentEngineUserController; }

            set { this.paymentForm = value; }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [refund now].
        /// </summary>
        /// <value><c>true</c> if [refund now]; otherwise, <c>false</c>.</value>
        public bool RowLock
        {
            get { return this.rowLock; }
            set { this.rowLock = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [refund now].
        /// </summary>
        /// <value><c>true</c> if [refund now]; otherwise, <c>false</c>.</value>
        public string[] AvailableTenderType
        {
            get { return this.availableTenderType; }
            set { this.availableTenderType = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [refund now].
        /// </summary>
        /// <value><c>true</c> if [refund now]; otherwise, <c>false</c>.</value>
        public bool RestrictedTenderType
        {
            get { return this.restrictedTenderType; }
            set { this.restrictedTenderType = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [refund now].
        /// </summary>
        /// <value><c>true</c> if [refund now]; otherwise, <c>false</c>.</value>
        public bool RefundNow
        {
            get { return this.refundNow; }
            set { this.refundNow = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [apply amount resolution].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [apply amount resolution]; otherwise, <c>false</c>.
        /// </value>
        public bool ApplyAmountResolution
        {
            get { return this.applyAmountResolution; }
            set { this.applyAmountResolution = value; }
        }

        /// <summary>
        /// Gets or sets the total receipt amount - set from the receipt form.
        /// </summary>
        /// <value>The total receipt amount.</value>
        public decimal TotalReceiptAmount
        {
            get { return this.totalReceiptAmount; }
            set { this.totalReceiptAmount = value; }
        }

        /// <summary>
        /// Gets or sets the owner of the current receipt.
        /// </summary>
        /// <value>The name of the owner.</value>
        public string OwnerName
        {
            get { return this.ownerName; }
            set { this.ownerName = value; }
        }

        public bool ChangedOwnerName
        {
            get { return this.changedOwnerName; }
            set { this.changedOwnerName = value; }
        }

        public PaymentEngineData  PayeeDetails
        {
            get { return this.payeeDetails ; }
            set { this.payeeDetails = value; } 
        }

        private WorkItem PayeeWorkItem
        {
            get { return this.payeeWorkItem; }
            set { this.payeeWorkItem = value; }
        }

       
        /// <summary>
        /// Gets or sets the width of the data grid view column.
        /// </summary>
        /// <value>The width of the data grid view column.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PaymentEngineColumnWidth DataGridViewColumnWidth
        {
            get
            {
                return this.dataGridViewColumnWidth;
            }

            set
            {
                if (value != null)
                {
                    this.dataGridViewColumnWidth = value;
                    if (this.autoResizeWithppId || this.autoResizeWithOutppId)
                    {
                        this.CustomizePaymentGridView();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [auto resize].
        /// </summary>
        /// <value><c>true</c> if [auto resize]; otherwise, <c>false</c>.</value>
        public bool AutoResizeWithOutppId
        {
            get
            {
                return this.autoResizeWithOutppId;
            }

            set
            {
                this.autoResizeWithOutppId = value;
                if (this.autoResizeWithOutppId)
                {
                    this.CustomizePaymentGridView();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PaymentEngineUserControl"/> is locked.
        /// </summary>
        /// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
        public bool ApplyInstrumentPayment
        {
            get
            {
                return this.instrumentPayment;
            }

            set
            {
                ////this.LockGrid(value);
                this.instrumentPayment = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PaymentEngineUserControl"/> is locked.
        /// </summary>
        /// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
        public string ApplyInstrumentBalanceAmount
        {
            get
            {
                return this.instrumentBalanceAmount;
            }

            set
            {
                ////this.LockGrid(value);
                this.instrumentBalanceAmount = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PaymentEngineUserControl"/> is locked.
        /// </summary>
        /// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
        public bool ApplyAmountFiled
        {
            get
            {
                return this.amountFiled;
            }

            set
            {
                ////this.LockGrid(value);
                this.amountFiled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [auto resize withpp id].
        /// </summary>
        /// <value><c>true</c> if [auto resize withpp id]; otherwise, <c>false</c>.</value>
        public bool AutoResizeWithppId
        {
            get
            {
                return this.autoResizeWithppId;
            }

            set
            {
                this.autoResizeWithppId = value;
                if (this.autoResizeWithppId)
                {
                    this.CustomizePaymentGridView();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [PPID visible].
        /// </summary>
        /// <value><c>true</c> if [PPID visible]; otherwise, <c>false</c>.</value>
        public bool PPIDVisible
        {
            get
            {
                return this.ppidVisible;
            }

            set
            {
                this.ppidVisible = value;
                ////if (this.pidVisible)
                ////{
                ////    this.orginalWidth = this.PaymentItemsGridView.Width;
                ////    this.PaymentItemsGridView.Width = this.orginalWidth;
                ////}
                ////else
                ////{
                ////    this.PaymentItemsGridView.Width = 624;
                ////}
                ////this.PaymentItemsGridView.Columns["PPID"].Visible = this.ppidVisible;
                ////this.PaymentItemsGridView.Columns["PPID"].Width = 65;
            }
        }

        /// <summary>
        /// Gets or sets the Ppayment id.
        /// </summary>
        /// <value>The P payment id.</value>
        public bool PIDVisible
        {
            get
            {
                return this.pidVisible;
            }

            set
            {
                this.pidVisible = value;
                ////if (this.pidVisible)
                ////{
                ////    this.orginalWidth = this.PaymentItemsGridView.Width;
                ////    this.PaymentItemsGridView.Width = this.orginalWidth;
                ////}
                ////else
                ////{
                ////    this.PaymentItemsGridView.Width = 624; 

                ////}

                ////this.PaymentItemsGridView.Columns["PID"].Visible = this.pidVisible;
                ////this.PaymentItemsGridView.Columns["PID"].Width = 76;               
            }
        }

        /// <summary>
        /// Gets or sets the Ppayment id.
        /// </summary>
        /// <value>The P payment id.</value>
        public int PPaymentId
        {
            get
            {
                return this.ppaymentId;
            }

            set
            {
                this.ppaymentId = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent work item.
        /// </summary>
        /// <value>The parent work item.</value>
        public WorkItem ParentWorkItem
        {
            get { return this.parentWorkItem; }
            set { this.parentWorkItem = value; }
        }

        /// <summary>
        /// Gets or sets the rows visible no.
        /// </summary>
        /// <value>The rows visible no.</value>
        public int RowsVisibleNo
        {
            get
            {
                return this.rowsVisibleNo;
            }

            set
            {
                int controlHeight = 0;
                if (value > 3)
                {
                    controlHeight = this.GetGridHeight(value);
                    this.PaymentItemsGridView.Height = controlHeight;
                    this.Height = controlHeight;
                    //// Added By Guhan
                    this.PaymentEngineVscrollBar.Height = controlHeight;
                    /*this.PaymentPictureBox.Height = controlHeight;
                    //if (this.WillShowLabel)
                    //{
                    //  this.PaymentPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(controlHeight, this.PaymentItemsGridView.Width, "Payments", 192, 192, 192);
                    //}
                     */
                    //// End
                    this.rowsVisibleNo = value;
                }
                else
                {
                    value = 3;
                    controlHeight = this.GetGridHeight(3);
                    this.PaymentItemsGridView.Height = controlHeight;
                    this.Height = controlHeight;
                    this.PaymentEngineVscrollBar.Height = controlHeight;

                    ////this.Height = this.PaymentItemsGridView.Height;
                    ////MessageBox.Show("Number of visible rows should be greater than 0");
                    ////MessageBox.Show(SharedFunctions.GetResourceString("VisibleRowError"),SharedFunctions.GetResourceString("PaymentEngineTitle"),MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PaymentEngineUserControl"/> is locked.
        /// </summary>
        /// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                this.LockGrid(value);
                this.locked = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow over under].
        /// </summary>
        /// <value><c>true</c> if [allow over under]; otherwise, <c>false</c>.</value>
        public bool AllowOverUnder
        {
            get { return this.allowOverUnder; }
            set { this.allowOverUnder = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PaymentEngineUserControl"/> is locked.
        /// </summary>
        /// <value><c>true</c> if locked; otherwise, <c>false</c>.</value>
        public decimal AmountTotal
        {
            get
            {
                return this.amountTotal;
            }

            set
            {
                this.amountTotal = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [will show label].
        /// </summary>
        /// <value><c>true</c> if [will show label]; otherwise, <c>false</c>.</value>
        public bool WillShowLabel
        {
            get
            {
                return this.PaymentPictureBox.Visible;
            }

            set
            {
                // if (value == true)
                // {
                //     this.Width = 797;
                // }
                // else
                // {
                //    this.Width = 773;
                // }

                this.Visible = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [set default selection].
        /// </summary>
        /// <value><c>true</c> if [set default selection]; otherwise, <c>false</c>.</value>
        public bool SetDefaultSelection
        {
            set
            {
                if (this.PaymentItemsGridView.Rows.Count > 0 && value == true)
                {
                    this.PaymentItemsGridView.Rows[0].Cells["TenderType"].Selected = true;
                }
                else
                {
                    try
                    {
                        if (this.PaymentItemsGridView.CurrentCell != null)
                        {
                            this.PaymentItemsGridView.CurrentCell.Selected = false;
                        }
                    }
                    catch ////(InvalidOperationException ex) 
                    {
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unpaid receipt.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is unpaid receipt; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutoPayment
        {
            get
            {
                return this.isAutoPayment;
            }

            set
            {
                this.isAutoPayment = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is F11018MiscReceipt.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is F11018MiscReceipt; otherwise, <c>false</c>.
        /// </value>
        public bool AllowZeroPayment
        {
            get
            {
                return this.allowZeroPayment;
            }
            set
            {
                this.allowZeroPayment = value;
            }
        }

        /// <summary>
        /// Gets or sets the balance amount.
        /// </summary>
        /// <value>The balance amount.</value>
        public decimal BalanceAmount
        {
            get
            {
                return this.totalBalanceAmount;
            }

            set
            {
                this.totalBalanceAmount = value;
            }
        }

        /// <summary>
        /// Gets or sets the total due.
        /// </summary>
        /// <value>The total due.</value>
        public decimal TotalDue
        {
            get
            {
                return this.totalDue;
            }

            set
            {
                this.totalDue = value;
            }
        }

        #endregion Property

        #region Public members

        /// <summary>
        /// Makes a particular columns to read only.
        /// </summary>
        /// <param name="readOnlyColumns">The read only columns.</param>
        public void SetReadOnlycolumn(string[] readOnlyColumns)
        {
            if (this.applyReadonlyColumn)
            {
                DataGridViewRowCollection paymentEngineDataRows = PaymentItemsGridView.Rows;
                foreach (DataGridViewRow dr in paymentEngineDataRows)
                {
                    for (int rowid = 0; rowid <= readOnlyColumns.Length - 1; rowid++)
                    {
                        if (readOnlyColumns[rowid] == "TenderType")
                        {
                            dr.Cells["TenderType"].ReadOnly = true;
                        }

                        dr.Cells[readOnlyColumns[rowid]].ReadOnly = true;
                    }
                }
            }
        }

        /// <summary>
        /// Loads the payment.
        /// </summary>        
        public void LoadPayment()
        {
            this.loadComplete = false;
            this.PopulatePaymentGrid(0);
            this.GetTenderTypeConfiguration();
            this.refundNow = false;
            this.loadComplete = true;
            this.PaymentEngineTabButton.TabStop = false;
        }

        /// <summary>
        /// Loads the payment.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id value.</param>
        public void LoadPayment(int ppaymentIdValue)
        {
            this.loadComplete = false;
            this.PopulatePaymentGrid(ppaymentIdValue);
            this.LockGrid(this.Locked);
            this.GetTenderTypeConfiguration();
            this.refundNow = false;
            this.loadComplete = true;
            this.PaymentEngineTabButton.TabStop = false;
        }

        /// <summary>
        /// This Method added by s.guhan For Fetching Data PPID
        /// </summary>
        /// <param name="ppaymentIdValue">ppaymentIdValue</param>
        public void LoadMultiplePayment(string ppaymentIdValue)
        {
            this.loadComplete = false;
            this.locked = true;
            this.PopulateMultiplePayment(ppaymentIdValue);
            this.LockSuspendedPayments();
            this.GetTenderTypeConfiguration();
            this.refundNow = false;
            this.loadComplete = true;
            this.PaymentEngineTabButton.TabStop = false;
        }

        /// <summary>
        /// The imported suspended payment items will be given as XML
        /// </summary>
        /// <returns>xml value of the imported suspended payments</returns>
        public string GetSuspendedPayments()
        {
            string xmlString = string.Empty;
            if (this.paymentsDataTable.Rows.Count > 0)
            {
                ////DataSet tempDataSet = new DataSet("Root");
                DataView tempDataView = new DataView(this.paymentsDataTable, "TenderType = '7'", "", DataViewRowState.CurrentRows);
                DataTable tempDataTable = tempDataView.ToTable();
                if (tempDataTable.Rows.Count > 0)
                {
                    xmlString = TerraScanCommon.GetXmlString(tempDataTable);
                }
                ////DataColumnCollection data = tempDataTable.Columns;
                ////tempDataSet.Tables.Add(tempDataTable);                
                ////tempDataSet.Tables[0].TableName = "Table";                
                ////return tempDataSet.GetXml();
            }
            ////else
            ////{
            return xmlString;
            ////}
        }

        /// <summary>
        /// Creates payment
        /// </summary>
        /// <returns>CreatePayment</returns>
        public int CreatePayment()
        {
            return this.CreatePayment(0);
        }

        /// <summary>
        /// Creates the payment.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>int ppayment Id</returns>
        public int CreatePayment(int ownerId)
            {
            int savedpPaymentId = 0;
            string paymentItems = null;
            DataSet tempDataSet = new DataSet("Root");

            DataView tempDataView = null;
            if (this.PaymentItemsGridView.CurrentCell != null && this.PaymentItemsGridView.CurrentCell.ColumnIndex.Equals(0))
            {
                this.IsOverUnder = true;
            }
           this.paymentsDataTable.AcceptChanges();
            ////Commented by purushotham to implement 20987

            //for (int i = 0; i < this.paymentsDataTable.Rows.Count; i++)
            //{
            //    if (this.paymentsDataTable.Rows[i]["TenderType"].Equals("2"))
            //    {
            //        if (!string.IsNullOrEmpty(this.paymentsDataTable.Rows[i]["Amount"].ToString()))
            //        {
            //            decimal amttemp = Convert.ToDecimal(this.paymentsDataTable.Rows[i]["Amount"].ToString());
            //            if (!this.paymentsDataTable.Rows[i]["Amount"].ToString().Contains("-"))
            //            {
            //                amttemp = Convert.ToDecimal(amttemp * -1);
            //                this.paymentsDataTable.Rows[i]["Amount"] = amttemp.ToString(".00");

            //                this.paymentsDataTable.AcceptChanges();
            //            }

            //        }
            //    }
            //}
            if (this.allowZeroPayment)
            {
                tempDataView = new DataView(this.paymentsDataTable, "Amount >= '0.00' AND TenderType <> ''", "", DataViewRowState.CurrentRows);
            }
            else
            {
                try
                {
                    tempDataView = new DataView(this.paymentsDataTable, "Amount IS NOT NULL AND TenderType <> ''", "", DataViewRowState.CurrentRows);
                }
                catch (Exception ex)
                {
                }
            }
            
            if (tempDataView.Count > 0)
            {
                DataTable tempDataTable = tempDataView.ToTable();
                tempDataSet.Tables.Add(tempDataTable);
                tempDataSet.Tables[0].TableName = "Table";
                paymentItems = tempDataSet.GetXml();
            }

            //// Code changed to fix bug #4035 --> khaja
            ////if (!(String.IsNullOrEmpty(paymentItems)) && this.PPaymentId == 0)
            if (!(String.IsNullOrEmpty(paymentItems))) ////&& this.PPaymentId == 0)
            {
                savedpPaymentId = WSHelper.SavePayment(this.PPaymentId, paymentItems, TerraScanCommon.UserId, ownerId);
            }
            if (this.GetRefundItemOccurence() >= 1)
            {
                this.refundNow = true;
            }
            else
            {
                this.refundNow = false;
            }

            this.PPaymentId = savedpPaymentId;
            this.LockGrid(this.Locked);
            return savedpPaymentId;
        }

        /// <summary>
        /// Loads the defult value when press New Button. --- Added by Latha
        /// </summary>
        /// <param name="ownerName">Name of the owner.</param>
        /// <param name="amount">The amount.</param>
        public void LoadDefultValue(string ownerName, decimal amount)
        {
            
            DataView paymentView = this.paymentsDataTable.DefaultView;
            paymentView.RowFilter = "TenderType IS NOT null AND TenderType<> ''";
            if(paymentView.Count <=0)
            {
                this.loadComplete = false;
                this.paymentsDataTable.Rows[0]["TenderType"] = "3";
                this.paymentsDataTable.Rows[0]["PaidBy"] = ownerName;
                this.paymentsDataTable.Rows[0]["Amount"] = amount;
                if (payeeDetails != null)
                {
                    if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                    {
                        this.paymentsDataTable.Rows[0]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                        this.paymentsDataTable.Rows[0]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                        this.paymentsDataTable.Rows[0]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                        this.paymentsDataTable.Rows[0]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                        this.paymentsDataTable.Rows[0]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                        this.paymentsDataTable.Rows[0]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                    }
                    this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
                    this.PaymentItemsGridView.Rows[0].Cells["PaidBy"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["TenderType"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["Amount"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["CheckNumber"].ReadOnly = false;

                }
                    for (int counter = this.paymentsDataTable.Rows.Count; counter < this.rowsVisibleNo; counter++)
                    {
                        this.paymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    }

                    if (this.paymentsDataTable.Rows.Count == this.rowsVisibleNo)
                    {
                        this.PaymentEngineVscrollBar.Visible = true;
                        this.PaymentEngineVscrollBar.BringToFront();
                    }
                    else
                    {
                        this.PaymentEngineVscrollBar.Visible = false;
                        this.PaymentItemsGridView.Width = this.orginalWidth;

                    }
                    paymentView.RowFilter = string.Empty;
                    this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
                this.refundNow = false;
                this.loadComplete = true;
                this.CalculatePaymentTotal();
                this.PaymentEngineTabButton.TabStop = false;
            }
            paymentView.RowFilter = string.Empty;
            
        }
        /// <summary>
        /// Loads the defult value when press New Button. --- Added by Latha
        /// </summary>
        /// <param name="ownerName">Name of the owner.</param>
        /// <param name="amount">The amount.</param>
        public void LoadDefaultValue(string ownerName, decimal amount)
        {
            DataView paymentView = this.paymentsDataTable.DefaultView;
            paymentView.RowFilter = "TenderType IS NOT null AND TenderType<> ''";
            if (paymentView.Count <= 0)
            {
                this.loadComplete = false;
                this.paymentsDataTable.Rows[0]["TenderType"] = "3";
                this.paymentsDataTable.Rows[0]["PaidBy"] = ownerName;
                this.paymentsDataTable.Rows[0]["Amount"] = amount;
                if (this.payeeDetails != null)
                {
                    if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                    {
                        this.paymentsDataTable.Rows[0]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                        this.paymentsDataTable.Rows[0]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                        this.paymentsDataTable.Rows[0]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                        this.paymentsDataTable.Rows[0]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                        this.paymentsDataTable.Rows[0]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                        this.paymentsDataTable.Rows[0]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];
                    }
                }
                this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
                this.PaymentItemsGridView.Rows[0].Cells["PaidBy"].ReadOnly = false;
                this.PaymentItemsGridView.Rows[0].Cells["TenderType"].ReadOnly = false;
                this.PaymentItemsGridView.Rows[0].Cells["Amount"].ReadOnly = false;
                this.PaymentItemsGridView.Rows[0].Cells["CheckNumber"].ReadOnly = false;
                for (int counter = this.paymentsDataTable.Rows.Count; counter < this.rowsVisibleNo; counter++)
                {
                    this.paymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                }

                if (this.paymentsDataTable.Rows.Count == this.rowsVisibleNo)
                {
                    this.PaymentEngineVscrollBar.Visible = true;
                    this.PaymentEngineVscrollBar.BringToFront();
                }
                else
                {
                    this.PaymentEngineVscrollBar.Visible = false;
                    this.PaymentItemsGridView.Width = this.orginalWidth;

                }
                paymentView.RowFilter = string.Empty;
                this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
                this.refundNow = false;
                this.loadComplete = true;
               this.CalculatePaymentTotal();
                this.PaymentEngineTabButton.TabStop = false;
            }
            paymentView.RowFilter = string.Empty;
        }


        /// <summary>
        /// Loads the defult value in Instrument Payment Engine
        /// </summary>
        /// <param name="ownerName">Name of the owner.</param>
        /// <param name="amount">The amount.</param>
        public void LoadInstrumentDefaultValue(string ownerName, decimal amount)
        {
            DataView paymentView = this.instrumentPaymentsDataTable.DefaultView;
            paymentView.RowFilter = "TenderType IS NOT null AND TenderType<> ''";
            if (paymentView.Count <= 0)
            {
                this.loadComplete = false;
                this.instrumentPaymentsDataTable.Rows[0]["TenderType"] = "3";
                this.instrumentPaymentsDataTable.Rows[0]["PaidBy"] = ownerName;
                this.instrumentPaymentsDataTable.Rows[0]["Amount"] = amount;
                if (this.payeeDetails != null)
                {
                    if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                    {
                        this.instrumentPaymentsDataTable.Rows[0]["Address1"] = this.payeeDetails.PayeeDetail.Rows[0]["Address1"];
                        this.instrumentPaymentsDataTable.Rows[0]["Address2"] = this.payeeDetails.PayeeDetail.Rows[0]["Address2"];
                        this.instrumentPaymentsDataTable.Rows[0]["City"] = this.payeeDetails.PayeeDetail.Rows[0]["City"];
                        this.instrumentPaymentsDataTable.Rows[0]["State"] = this.payeeDetails.PayeeDetail.Rows[0]["State"];
                        this.instrumentPaymentsDataTable.Rows[0]["Zip"] = this.payeeDetails.PayeeDetail.Rows[0]["Zip"];
                        this.instrumentPaymentsDataTable.Rows[0]["Comment"] = this.payeeDetails.PayeeDetail.Rows[0]["Comment"];

                    }
                }
                    this.PaymentItemsGridView.DataSource = this.instrumentPaymentsDataTable;
                    this.PaymentItemsGridView.Rows[0].Cells["PaidBy"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["TenderType"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["Amount"].ReadOnly = false;
                    this.PaymentItemsGridView.Rows[0].Cells["CheckNumber"].ReadOnly = false;
                    for (int counter = this.instrumentPaymentsDataTable.Rows.Count; counter < this.rowsVisibleNo; counter++)
                    {
                        this.instrumentPaymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
                    }

                    if (this.instrumentPaymentsDataTable.Rows.Count == this.rowsVisibleNo)
                    {
                        this.PaymentEngineVscrollBar.Visible = true;
                        this.PaymentEngineVscrollBar.BringToFront();
                    }
                    else
                    {
                        this.PaymentEngineVscrollBar.Visible = false;
                        this.PaymentItemsGridView.Width = this.orginalWidth;

                    }
                    paymentView.RowFilter = string.Empty;
                    this.PaymentItemsGridView.DataSource = this.instrumentPaymentsDataTable;
                    this.refundNow = false;
                    this.loadComplete = true;
                    this.PaymentEngineTabButton.TabStop = false;
                }
               this.CalculateInsrumentPaymentTotal();
                paymentView.RowFilter = string.Empty;
            }
      
        /// <summary>
        /// Not used as of now
        /// </summary>
        /// <param name="keyID">keyID</param>
        public void DisableRows(string keyID)
        {
            DataSet ppidDataSet = new DataSet();
            ppidDataSet.ReadXml(Utilities.SharedFunctions.XmlParser(keyID));
            foreach (DataRow disableRow in ppidDataSet.Tables[0].Rows)
            {
                DataView tempDataView = new DataView();
                tempDataView = this.paymentsDataTable.DefaultView;
                tempDataView.Sort = "PPID";

                int gridDisableRowId = this.paymentsDataTable.DefaultView.Find(disableRow[0]);
                ////this.PaymentItemsGridView.Rows[2].ReadOnly = true;

                if (gridDisableRowId != -1)
                {
                    ////(PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ReadOnly = true;
                    ////this.PaymentItemsGridView["TenderType", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["PaidBy", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["CheckNumber", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["Amount", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["PaidBy", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["CheckNumber", 1].ReadOnly = true;
                    ////this.PaymentItemsGridView["Amount", 1].ReadOnly = true;
                    this.PaymentItemsGridView.Rows[gridDisableRowId].Cells["TenderType"].ReadOnly = true;
                    this.PaymentItemsGridView.Rows[gridDisableRowId].Cells["PaidBy"].ReadOnly = true;
                    this.PaymentItemsGridView.Rows[gridDisableRowId].Cells["CheckNumber"].ReadOnly = true;
                    this.PaymentItemsGridView.Rows[gridDisableRowId].Cells["Amount"].ReadOnly = true;
                    this.PaymentItemsGridView.Rows[gridDisableRowId].Cells["PaidByImage"].ReadOnly = true;
                }
            }
        }

        #endregion

        #region Protected Members

        /// <summary>
        /// Add a protected method called OnSavedEvent(). 
        /// </summary>
        /// <param name="amount">saved receiptid</param>         
        protected virtual void OnPaymentAmountChangeEvent(decimal amount)
        {
            // If an event has no subscribers registerd, it will 
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subsribers before
            // calling the event itself.
            if (this.PaymentAmountChangeEvent != null)
            {
                this.PaymentAmountChangeEvent(amount);  // Notify Subscribers
            }
        }

        /// <summary>
        /// Called when [payment item change event].
        /// </summary>
        protected virtual void OnPaymentItemChangeEvent()
        {
            // If an event has no subscribers registerd, it will 
            // evaluate to null. The test checks that the value is not
            // null, ensuring that there are subsribers before
            // calling the event itself.
            if (this.PaymentItemChangeEvent != null)
            {
               this.PaymentItemChangeEvent();  // Notify Subscribers
            }
        }

        #endregion

        #region Private User Defined Members

        /// <summary>
        /// Locks the grid.
        /// </summary>
        /// <param name="lockType">if set to <c>true</c> [lock type].</param>
        private void LockGrid(bool lockType)
        {
            for (int rowCount = 0; rowCount < this.PaymentItemsGridView.Rows.Count; rowCount++)
            {
                (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ReadOnly = lockType;

                this.PaymentItemsGridView["TenderType", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["PaidBy", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["CheckNumber", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["Amount", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["PaidBy", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["CheckNumber", rowCount].ReadOnly = lockType;
                this.PaymentItemsGridView["Amount", rowCount].ReadOnly = lockType;
            }
        }

        /// <summary>
        /// If the payments are imported from Suspended payments form , then the records are readonly, 
        /// other payments, which are manually entered by the user are editable
        /// This method is to make specific records in the grid readonly.
        /// </summary>
        private void LockSuspendedPayments()
        {
            for (int rowCount = 0; rowCount < this.PaymentItemsGridView.Rows.Count; rowCount++)
            {
                if (this.PaymentItemsGridView["PID", rowCount].Value.ToString() != "0" && this.PaymentItemsGridView["PID", rowCount].Value.ToString() != "")
                {
                    (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ReadOnly = true;
                    this.PaymentItemsGridView["TenderType", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["PaidBy", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["CheckNumber", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["Amount", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["PID", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["PPID", rowCount].ReadOnly = true;
                    this.PaymentItemsGridView["PaidByImage", rowCount].ReadOnly = true;
                }
            }

            this.locked = false;
        }

        /// <summary>
        /// Calculatings the payment total.
        /// </summary>
        private void CalculatePaymentTotal()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;

            for (int counter = 0; counter < this.paymentsDataTable.Rows.Count; counter++)
            {
                if (Decimal.TryParse(this.paymentsDataTable.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    //// Guhan added 
                    ////  minus for refund
                    if (this.paymentsDataTable.Rows[counter]["TenderType"].ToString() == "2")
                    {
                        if (outDecimalValue > 0 && this.Tag !=null && !this.Tag.Equals("1013") && !this.Tag.Equals("1410") )
                        ////Added the last 3 conditions by Biju on 13/Apr/2010 to implement #6556.
                        {
                            outDecimalValue = decimal.Negate(outDecimalValue);
                        }
                    }

                    paymentsTotal += outDecimalValue;
                    paymentCount++;
                }
            }

            this.AmountTotal = paymentsTotal;
            this.OnPaymentAmountChangeEvent(this.AmountTotal);
            this.ChangeScrollBarStatus();
            if (this.loadComplete)
            {
                /* The below if condition and View is added by Jayanthi to stop adding empty rows when 
                 * any one of the Amount column is found to be "0.0" */
                if (paymentCount == this.paymentsDataTable.Rows.Count)
                {
                    DataView tempDataView = new DataView(this.paymentsDataTable, "Amount = '0.00'", "", DataViewRowState.CurrentRows);
                    if (tempDataView.Count == 0)
                    {
                        this.paymentsDataTable.Rows.Add(new object[] { "", "", "", DBNull.Value, "", "" });
                        this.PaymentEngineVscrollBar.Visible = false;
                    }


                    this.PaymentItemsGridView.Width = this.orginalWidth;
                    if (this.autoResizeWithOutppId)
                    {
                        this.PaymentEngineTabButton.TabStop = true;
                    }

                    ////this.PaymentItemsGridView.Refresh();
                }
                else
                {
                    this.PaymentEngineTabButton.TabStop = false;
                }
            }
        }

        /// <summary>
        /// Gets the refund item occurence.
        /// </summary>
        /// <returns>number of Refund available in grid</returns>
        private int GetRefundItemOccurence()
        {
            int refundCount = 0;
            for (int itemCount = 0; itemCount < this.PaymentItemsGridView.Rows.Count; itemCount++)
            {
                if (this.PaymentItemsGridView["TenderType", itemCount].Value.ToString().Trim() == "2")
                {
                    refundCount = refundCount + 1;
                }
            }

            return refundCount;
        }

        /// <summary>
        /// Checks the type of the tender.
        /// </summary>
        /// <param name="combo">The combo.</param>
        private void CheckTenderType(ComboBox combo)
        {
            ////Added By Ramya.D
            if (combo.SelectedIndex > 0 && combo.SelectedValue != null && this.loadComplete == true && !this.ApplyInstrumentPayment)
            //// if (combo.SelectedIndex > 0 && combo.SelectedValue != null && this.loadComplete == true)
            {
                //if (combo.SelectedValue.ToString() == "2")
                //{
                    //if (this.GetRefundItemOccurence() > 1)
                    //{
                    //    //MessageBox.Show(SharedFunctions.GetResourceString("RefundAllowed"), "TenderTypeError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //combo.SelectedValue = 3;
                    //    //combo.SelectedText = "Check";
                    //    //if (this.PaymentItemsGridView.CurrentCell != null)
                    //    //{
                    //    //    this.PaymentItemsGridView.CurrentCell.Value = 3;

                    //    //    if (!this.ApplyInstrumentPayment)
                    //    //    {
                    //    //        this.paymentsDataTable.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex][this.PaymentItemsGridView.CurrentCell.ColumnIndex] = 3;
                    //    //    }
                    //    //    else
                    //    //    {
                    //    //        this.instrumentPaymentsDataTable.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex][this.PaymentItemsGridView.CurrentCell.ColumnIndex] = 3;
                    //    //    }
                    //    //}
                    //}
                //}
            }
            else if (combo.SelectedIndex > 0 && combo.SelectedValue != null && this.ApplyInstrumentPayment && this.ApplyInstrumentBalanceAmount != null && (this.previousRow != this.PaymentItemsGridView.CurrentCell.RowIndex || string.IsNullOrEmpty(this.PaymentItemsGridView["Amount", this.PaymentItemsGridView.CurrentCell.RowIndex].Value.ToString())))// == "0.00" ))
            {
                ////this.instrumentPaymentsDataTable.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex][this.PaymentItemsGridView.CurrentCell.ColumnIndex] = 3;
               // this.edit = false;
                //// used to place amount value when empty.
                if (string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex].Cells["Amount"].Value.ToString()))
                {
                    this.PaymentItemsGridView["Amount", this.PaymentItemsGridView.CurrentCell.RowIndex].Value = this.ApplyInstrumentBalanceAmount;
                }
                this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;
                this.applyAmountColumn = true;
            }
        }

        /// <summary>
        /// Populates the payment grid with multiple payment ids
        /// </summary>
        /// <param name="multipletIdValue1">multipletIdValue1</param>
        private void PopulateMultiplePayment(string multipletIdValue1)
        {
            ////fill paymentsDatatable with the value from the database
            this.FillMultiplepaymentsDataTable(multipletIdValue1);
            this.PaymentItemsGridView.DataSource = null;
            this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            this.tenderTypeDataTable = WSHelper.ListTenderType(this.allowOverUnder).ListTenderType;
            //// Added By GuhanS To  restrict the tender type
            if (this.RestrictedTenderType)
            {
                StringBuilder stringExp = new StringBuilder();
                DataRow[] selectedTenderRows;
                DataRow tenderTypeNewRow;
                DataTable tenderTempTable = new DataTable();

                tenderTempTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderTypeID", System.Type.GetType("System.Byte")), new DataColumn("TenderType", System.Type.GetType("System.String")) });
                int selectedTTLength = 0;
                if (this.availableTenderType != null)
                {
                    for (int rowid = 0; rowid <= this.availableTenderType.Length - 1; rowid++)
                    {
                        if (rowid != this.availableTenderType.Length - 1)
                        {
                            stringExp.Append(" TenderTypeID <>" + this.availableTenderType[rowid] + " And ");
                        }
                        else
                        {
                            stringExp.Append("TenderTypeID <>" + this.availableTenderType[rowid]);
                        }
                    }
                }

                selectedTenderRows = this.tenderTypeDataTable.Select(stringExp.ToString());
                foreach (DataRow tenderTypeRow in selectedTenderRows)
                {
                    tenderTypeNewRow = tenderTempTable.NewRow();
                    tenderTypeNewRow["TenderTypeID"] = Convert.ToSByte(tenderTypeRow["TenderTypeID"].ToString());
                    tenderTypeNewRow["TenderType"] = tenderTypeRow["TenderType"].ToString();
                    tenderTempTable.Rows.Add(tenderTypeNewRow);
                }

                this.tenderTypeDataTable.Rows.Clear();
                //// PaymentEngineVscrollBar.Height = this.PaymentItemsGridView.Height; 
                try
                {
                    this.tenderTypeDataTable.Merge(tenderTempTable);
                }
                catch (Exception e1)
                {
                }
            }

            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < this.tenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { this.tenderTypeDataTable.Rows[i]["TenderType"].ToString(), this.tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            if (this.restrictedTenderType)
            {
                this.LoadBatchTenderType(tempDataTable, tempDataTable, multipletIdValue1);
            }
            else
            {
                (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable.Copy();
                (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
                (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";
            }

            ////calculating paymenttotal of the payments
            this.CalculatePaymentTotal();
            this.PaymentPictureBox.Visible = this.WillShowLabel;
            this.PaymentPictureBox.Left = this.orginalWidth;
            this.PaymentEngineVscrollBar.Left = this.scrollBarPosition;
        }

        /// <summary>
        /// Populates the payment grid.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id value.</param>
        private void PopulatePaymentGrid(int ppaymentIdValue)
        {
            this.PPaymentId = ppaymentIdValue;
            ////fill paymentsDatatable with the value from the database
            this.FillpaymentsDataTable(ppaymentIdValue);
            this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
            this.pidSource.DataSource = this.paymentsDataTable;
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            this.tenderTypeDataTable = WSHelper.ListTenderType(this.allowOverUnder).ListTenderType;
            //// this.tenderTypeDataTable.Columns[0].DataType  
            //// Added By GuhanS To  restrict the tender type
            if (this.RestrictedTenderType)
            {
                StringBuilder stringExp = new StringBuilder();
                DataRow[] selectedTenderRows;
                DataRow tenderTypeNewRow;
                DataTable tenderTempTable = new DataTable();

                tenderTempTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderTypeID", System.Type.GetType("System.Byte")), new DataColumn("TenderType", System.Type.GetType("System.String")) });
                int selectedTTLength = 0;
                if (this.availableTenderType != null)
                {
                    for (int rowid = 0; rowid <= this.availableTenderType.Length - 1; rowid++)
                    {
                        if (rowid != this.availableTenderType.Length - 1)
                        {
                            //// tempUserId = tempUserId + " userId <>" + userInRows[i][0].ToString() + " And ";
                            stringExp.Append(" TenderTypeID <>" + this.availableTenderType[rowid] + " And ");
                        }
                        else
                        {
                            //// tempUserId = tempUserId + "userId <>" + userInRows[i][0].ToString();
                            stringExp.Append("TenderTypeID <>" + this.availableTenderType[rowid]);
                        }
                    }
                }

                selectedTenderRows = this.tenderTypeDataTable.Select(stringExp.ToString());
                foreach (DataRow tenderTypeRow in selectedTenderRows)
                {
                    tenderTypeNewRow = tenderTempTable.NewRow();
                    tenderTypeNewRow["TenderTypeID"] = Convert.ToSByte(tenderTypeRow["TenderTypeID"].ToString());
                    tenderTypeNewRow["TenderType"] = tenderTypeRow["TenderType"].ToString();
                    tenderTempTable.Rows.Add(tenderTypeNewRow);
                }

                this.tenderTypeDataTable.Rows.Clear();
                //// PaymentEngineVscrollBar.Height = this.PaymentItemsGridView.Height; 
                try
                {
                    this.tenderTypeDataTable.Merge(tenderTempTable);
                }
                catch (Exception e1)
                {
                }
            }

            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < this.tenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { this.tenderTypeDataTable.Rows[i]["TenderType"].ToString(), this.tenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable.Copy();
            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";

            ////calculating paymenttotal of the payments

            this.CalculatePaymentTotal();
            this.PaymentPictureBox.Visible = this.WillShowLabel;
            this.PaymentPictureBox.Left = this.orginalWidth;
            this.PaymentEngineVscrollBar.Left = this.scrollBarPosition;
        }

        /*  /// This Code is specified in the spec that it will be used in future.
         * /// <summary>
         /// Adds the specified tender type.
         /// </summary>
         /// <param name="tenderType">Type of the tender.</param>
         /// <param name="paidBy">The paid by.</param>
         /// <param name="CheckNumber">The check num.</param>
         /// <param name="amount">The amount.</param>
         private void Add(string tenderType, string paidBy, int CheckNumber, decimal amount)
         {
             DataRow[] rowFilter = this.tenderTypeDataTable.Select("TenderType =" + tenderType);
             if (rowFilter.Length >= 1)
             {
                 this.paymentsDataTable.Rows.Add(new object[] { tenderType, paidBy, CheckNumber, amount, "", "" });
             }
             else
             {
                 MessageBox.Show(SharedFunctions.GetResourceString("InvalidTenderType"), ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
             }
         }
         */

        /// <summary>
        /// Fillpaymentses the data table.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id.</param>
        private void FillMultiplepaymentsDataTable(string ppaymentIdValue)
        {
            DataTable tempTable = new DataTable();

            tempTable = this.paymentsDataTable.Clone();

            for (int i = 0; i < this.paymentsDataTable.Rows.Count; i++)
            {
                if (this.paymentsDataTable.Rows[i]["TenderType"].ToString() != string.Empty)
                {
                    DataRow tempRow = tempTable.NewRow();
                    tempRow["TenderType"] = this.paymentsDataTable.Rows[i]["TenderType"].ToString();
                    tempRow["PaidBy"] = this.paymentsDataTable.Rows[i]["PaidBy"].ToString();
                    tempRow["CheckNumber"] = this.paymentsDataTable.Rows[i]["CheckNumber"].ToString();
                    if (this.paymentsDataTable.Rows[i]["Amount"].ToString() != string.Empty)
                    {
                        tempRow["Amount"] = this.paymentsDataTable.Rows[i]["Amount"].ToString();
                    }
                    else
                    {
                        tempRow["Amount"] = "0.00";
                    }

                    tempRow["PID"] = this.paymentsDataTable.Rows[i]["PID"].ToString();
                    tempRow["PPID"] = this.paymentsDataTable.Rows[i]["PPID"].ToString();
                    tempRow["Address1"] = this.paymentsDataTable.Rows[i]["Address1"].ToString();
                    tempRow["Address2"] = this.paymentsDataTable.Rows[i]["Address2"].ToString();
                    tempRow["City"] = this.paymentsDataTable.Rows[i]["City"].ToString();
                    tempRow["State"] = this.paymentsDataTable.Rows[i]["State"].ToString();
                    tempRow["Zip"] = this.paymentsDataTable.Rows[i]["Comment"].ToString();    
                    tempTable.Rows.Add(tempRow);
                }
            }

            if (ppaymentIdValue.Length > 0)
            {
                this.paymentEngineDataSet = WSHelper.GetMultiplePayment(ppaymentIdValue);
            }
            else
            {
                this.paymentEngineDataSet.Clear();
            }

            this.paymentsDataTable.Clear();

            if (this.paymentsDataTable.Columns.Count <= 0)
            {
                this.paymentsDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("PaidBy"), new DataColumn("CheckNumber"), new DataColumn("Amount", typeof(decimal)), new DataColumn("PID"), new DataColumn("PPID"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") });
            }

            bool checkDuplicate = false;

            for (int counter = 0; counter < this.paymentEngineDataSet.GetPayment.Rows.Count; counter++)
            {
                if (tempTable.Rows.Count > 0)
                {
                    for (int tempCounter = 0; tempCounter < tempTable.Rows.Count; tempCounter++)
                    {
                        if (tempTable.Rows[tempCounter]["PID"].ToString() == this.paymentEngineDataSet.GetPayment.Rows[counter]["PaymentID"].ToString())
                        {
                            checkDuplicate = true;
                            break;
                        }
                    }
                }

                if (!checkDuplicate)
                {
                    DataRow dr = this.paymentsDataTable.NewRow();
                    ////The records from the suspended selection form should have the Tender type 
                    ////"Suspended" always. 7 is the tender type id for "suspended"
                    dr["TenderType"] = "7";
                    dr["PaidBy"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PaidBy"].ToString();
                    dr["CheckNumber"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["CheckNumber"].ToString();
                    dr["Amount"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Amount"].ToString();
                    dr["PID"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PaymentID"].ToString();
                    dr["PPID"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PPaymentID"].ToString();
                    dr["Address1"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Address1"].ToString();
                    dr["Address2"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Address2"].ToString();
                    dr["City"]=this.paymentEngineDataSet.GetPayment.Rows[counter]["City"].ToString();
                    dr["State"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["State"].ToString();
                    dr["Zip"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Zip"].ToString();
                    dr["Comment"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Comment"].ToString();
                    this.paymentsDataTable.Rows.Add(dr);
                }
            }

            this.paymentsDataTable.Merge(tempTable);

            int valueToCheckAmount = 0;
            decimal outDecimalValue = 0;
            for (int counter = 0; counter < this.paymentsDataTable.Rows.Count; counter++)
            {
                if (Decimal.TryParse(this.paymentsDataTable.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    valueToCheckAmount++;
                    ////paymentsTotal += outDecimalValue;
                }
            }

            if (valueToCheckAmount == this.paymentsDataTable.Rows.Count)
            {
                this.CreateEmptyRows(false);
            }

            this.ChangeScrollBarStatus();
        }

        /// <summary>
        /// Changes the scroll bar status.
        /// </summary>
        private void ChangeScrollBarStatus()
        {
            if (this.instrumentPayment)
            {
                if (this.instrumentPaymentsDataTable.Rows.Count == this.rowsVisibleNo)
                {
                    this.PaymentEngineVscrollBar.Visible = true;
                    this.PaymentEngineVscrollBar.BringToFront();
                }
                else
                
                {
                    this.PaymentEngineVscrollBar.Visible = false;
                    this.PaymentItemsGridView.Width = this.orginalWidth;
                }
            }
            else
            {
                if (this.paymentsDataTable.Rows.Count == this.rowsVisibleNo)
                {
                    this.PaymentEngineVscrollBar.Visible = true;
                    this.PaymentEngineVscrollBar.BringToFront();
                }
                else
                {
                    this.PaymentEngineVscrollBar.Visible = false;
                    this.PaymentItemsGridView.Width = this.orginalWidth;
                }
            }
        }

        /// <summary>
        /// Fillpaymentses the data table.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id.</param>
        private void FillpaymentsDataTable(int ppaymentIdValue)
        {
            if (ppaymentIdValue > 0)
            {
                this.paymentEngineDataSet = WSHelper.GetPayment(ppaymentIdValue);
            }
            else
            {
                this.paymentEngineDataSet.Clear();
            }

            this.paymentsDataTable.Clear();

            if (this.paymentsDataTable.Columns.Count <= 0)
            {
                this.paymentsDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("PaidBy"), new DataColumn("CheckNumber"), new DataColumn("Amount", typeof(decimal)), new DataColumn("PID"), new DataColumn("PPID"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") });
            }
            ///, new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") 
            for (int counter = 0; counter < this.paymentEngineDataSet.GetPayment.Rows.Count; counter++)
            {
                DataRow dr = this.paymentsDataTable.NewRow();
                dr["TenderType"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["TenderTypeID"].ToString();
                dr["PaidBy"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PaidBy"].ToString();
                dr["CheckNumber"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["CheckNumber"].ToString();
                dr["Amount"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Amount"].ToString();
                dr["PID"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PaymentID"].ToString();
                dr["PPID"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["PPaymentID"].ToString();
                dr["Address1"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Address1"].ToString();
                dr["Address2"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Address2"].ToString();
                dr["City"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["City"].ToString();     
                dr["State"]= this.paymentEngineDataSet.GetPayment.Rows[counter]["State"].ToString();
                dr["Zip"] =  this.paymentEngineDataSet.GetPayment.Rows[counter]["Zip"].ToString();
                dr["Comment"] = this.paymentEngineDataSet.GetPayment.Rows[counter]["Comment"].ToString();
                this.paymentsDataTable.Rows.Add(dr);
            }

            for (int counter = this.paymentsDataTable.Rows.Count; counter < this.rowsVisibleNo; counter++)
            {
                this.paymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty,string.Empty ,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty   });
            }

            if (this.paymentsDataTable.Rows.Count == this.rowsVisibleNo)
            {
                this.PaymentEngineVscrollBar.Visible = true;
                this.PaymentEngineVscrollBar.BringToFront();
            }
            else
            {
                this.PaymentEngineVscrollBar.Visible = false;
                this.PaymentItemsGridView.Width = this.orginalWidth;

            }
        }

        /// <summary>
        /// Gets the tender type configuration.
        /// </summary>
        private void GetTenderTypeConfiguration()
        {
            this.paymentEngineDataSet = WSHelper.GetTenderTypeConfiguration();
            if (this.paymentEngineDataSet.GetTenderTypeConfiguration.Count >= 1)
            {
                if (this.paymentEngineDataSet.GetTenderTypeConfiguration.Rows.Count > 0)
                {
                    //// Changed OverUnderMaxAmount and OverUnderMinAmount datatype -- BugID 5198 -- Ramya.D
                    decimal.TryParse(this.paymentEngineDataSet.GetTenderTypeConfiguration.Rows[0]["OverUnderMaxAmount"].ToString().Trim(), out this.overUnderMaxAmount);
                    decimal.TryParse(this.paymentEngineDataSet.GetTenderTypeConfiguration.Rows[0]["OverUnderMinAmount"].ToString().Trim(), out this.overUnderMinAmount);
                }
            }
        }

        /// <summary>
        /// Gets the height of the grid.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <returns>integer value </returns>
        private int GetGridHeight(int rowCount)
        {
            ////if (this.rowHeight != 0)
            ////{
            ////    return this.PaymentItemsGridView.ColumnHeadersHeight + (this.rowHeight * rowCount);
            ////}
            ////else
            ////{
            ////22 is the row height of the PaymentGridview got from the event Rowheightinfoneeded
            return this.PaymentItemsGridView.ColumnHeadersHeight + (22 * rowCount);
            ////}

            ////return this.PaymentItemsGridView.ColumnHeadersHeight + (this.PaymentItemsGridView.RowHeadersWidth * rowCount);
        }

        /// <summary>
        /// Datas the grid view column width_ property changed.
        /// </summary>
        /// <param name="propertyValue">The property value.</param>
        private void DataGridViewColumnWidth_PropertyChanged(int propertyValue)
        {
            if (this.autoResizeWithppId || this.autoResizeWithOutppId)
            {
                this.CustomizePaymentGridView();
            }
        }

        /// <summary>
        /// This Method used to set dataproperty name
        /// CustomizePaymentGridView
        /// </summary>
        private void CustomizePaymentGridView()
        {
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.PaymentItemsGridView.Columns;

            this.TenderType.DisplayIndex = 0;
            this.PaidBy.DisplayIndex = 1;
            this.PaidByImage.DisplayIndex = 2; 
            this.CheckNumber.DisplayIndex = 3;
            this.Amount.DisplayIndex = 4;
            this.PID.DisplayIndex = 5;
            this.PPID.DisplayIndex = 6;
           this.Address1.DisplayIndex = 7;
           this.Address2.DisplayIndex = 8;
           this.City.DisplayIndex = 9;
           this.State.DisplayIndex = 10;
           this.Zip.DisplayIndex = 11;
           this.Comment.DisplayIndex = 12; 


            if (this.autoResizeWithOutppId)
            {
                this.PPID.Visible = false;
                this.PID.Visible = false;
            }

            this.TenderType.Width = this.dataGridViewColumnWidth.TenderTypeWidth;
            this.CheckNumber.Width = this.dataGridViewColumnWidth.CheckNumberWidth;
            this.Amount.Width = this.dataGridViewColumnWidth.AmountWidth;
            this.PaidBy.Width = this.dataGridViewColumnWidth.PaidByWidth;
            this.PaidByImage.Width = this.dataGridViewColumnWidth.PaidByImageWidth;  
            this.PID.Width = this.dataGridViewColumnWidth.PIDWidth;
            this.PPID.Width = this.dataGridViewColumnWidth.PPIDWidth;

            this.scrollBarPosition = columns.GetColumnsWidth(DataGridViewElementStates.Visible) + this.PaymentItemsGridView.RowHeadersWidth + 1;
            this.orginalWidth = this.scrollBarPosition + this.PaymentEngineVscrollBar.Width;
            this.PaymentItemsGridView.Width = this.orginalWidth;
            this.PaymentEngineVscrollBar.Left = this.scrollBarPosition;

            if (this.ApplyInstrumentPayment)
            {
                this.PaymentEngineVscrollBar.Left = 574;
            }

            this.Width = this.orginalWidth;
        }

        /// <summary>
        /// Handles the Enter event of the PaymentEngineTabButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentEngineTabButton_Enter(object sender, EventArgs e)
        {
            this.PaymentItemsGridView.Focus();
            this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[this.TenderType.Name, this.PaymentItemsGridView.Rows.Count - 1];
            this.PaymentEngineTabButton.TabStop = false;
        }

        /// <summary>
        /// Handles the KeyDown event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_KeyDown(object sender, KeyEventArgs e)
            {
            if (this.RestrictedTenderType)
            {
                int deleteRowIndex;
                deleteRowIndex = ((DataGridView)sender).CurrentRow.Index;
                this.ChangeScrollBarStatus();
                ////Delete key handler
                if (e.KeyValue == 46)
                {
                    if (!this.ApplyInstrumentPayment)
                    {
                        this.paymentsDataTable.Rows[deleteRowIndex].Delete();
                    }
                    ////Added By ramya for instrumentPayment
                    else
                    {
                        this.instrumentPaymentsDataTable.Rows[deleteRowIndex].Delete();
                    }

                    this.CreateEmptyRows(true);
                }

                if (!this.ApplyInstrumentPayment)
                {
                    this.CalculatePaymentTotal();
                }
                ////Added By ramya for instrumentPayment
                else
                {
                    this.CalculateInsrumentPaymentTotal();
                }
            }
        }

        /// <summary>
        /// For creating empty rows in the grid view. 
        /// </summary>
        /// <param name="fromDeleteKeyPress">If this method is called from delete key press
        /// then empty row should not be added in soem cases.
        /// </param>
        private void CreateEmptyRows(bool fromDeleteKeyPress)
        {
            if (!this.ApplyInstrumentPayment)
            {
                if (this.paymentsDataTable.Rows.Count >= this.rowsVisibleNo)
                {
                    if (!fromDeleteKeyPress)
                    {
                        this.paymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty });
                    }
                }
                else
                {
                    int emptyRowsToBeAdded = this.rowsVisibleNo - this.paymentsDataTable.Rows.Count;
                    while (emptyRowsToBeAdded > 0)
                    {
                        this.paymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty, string.Empty });
                        emptyRowsToBeAdded--;
                    }
                }
            }
            ////Added By ramya for instrumentPayment
            else
            {
                if (this.instrumentPaymentsDataTable.Rows.Count >= this.rowsVisibleNo)
                {
                    if (!fromDeleteKeyPress)
                    {
                        this.instrumentPaymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty });
                    }
                }
                else
                {
                    int emptyRowsToBeAdded = this.rowsVisibleNo - this.instrumentPaymentsDataTable.Rows.Count;
                    while (emptyRowsToBeAdded > 0)
                    {
                        this.instrumentPaymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty });
                        emptyRowsToBeAdded--;
                    }
                }
            }
        }

        /// <summary>
        /// This method used to load suspended payement
        /// </summary>
        /// <param name="sourceDataTable">The source data table.</param>
        /// <param name="ppidDetailsTable">The ppid details table.</param>
        /// <param name="pidDetails">The pid details.</param>
        private void LoadBatchTenderType(DataTable sourceDataTable, DataTable ppidDetailsTable, string pidDetails)
        {
            DataTable impTenderTable = new DataTable();
            DataSet pidDataSet = new DataSet();
            pidDataSet.ReadXml(Utilities.SharedFunctions.XmlParser(pidDetails));
            if (pidDataSet != null && pidDataSet.Tables.Count >= 0)
            {
                this.pidMainDataSet.Merge(pidDataSet.Tables[0]);
                if (this.pidMainDataSet != null && this.pidMainDataSet.Tables.Count >= 0)
                {
                    impTenderTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });

                    impTenderTable.Rows.Add(new object[] { "SuspendPayment", 7 });
                    for (int rowID = 0; rowID < this.PaymentItemsGridView.Rows.Count - 1; rowID++)
                    {
                        this.pidMainDataSet.Tables[0].DefaultView.Sort = "PPaymentID";
                        int readonlyRow = this.pidMainDataSet.Tables[0].DefaultView.Find(this.PaymentItemsGridView.Rows[rowID].Cells["PID"].Value.ToString());
                        if (readonlyRow >= 0)
                        {
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).DataSource = impTenderTable;
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).DisplayMember = "TenderType";
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).ValueMember = "TenderTypeID";
                        }
                        else
                        {
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).DataSource = sourceDataTable.Copy();
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).DisplayMember = "TenderType";
                            (PaymentItemsGridView.Rows[rowID].Cells["TenderType"] as DataGridViewComboBoxCell).ValueMember = "TenderTypeID";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Makes the cell to editable on cell Enter or Click depending on the record
        /// </summary>
        /// <param name="rowIndex">Position of the row in the Grid View</param>
        /// <param name="columnIndex">Position of the column in the Grid View</param>
        private void MakeCellsEditable(int rowIndex, int columnIndex)
        {
            if (columnIndex.Equals(this.PaymentItemsGridView.Columns["TenderType"].Index) || !string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[rowIndex].Cells["TenderType"].Value.ToString()))
            {
                if (this.Locked)
                {
                    this.PaymentItemsGridView.Rows[rowIndex].Cells[columnIndex].ReadOnly = true;
                }
                else
                {
                    if (this.RestrictedTenderType)
                    {
                        if (this.PaymentItemsGridView["PID", rowIndex].Value.ToString() != "0" && this.PaymentItemsGridView["PID", rowIndex].Value.ToString() != string.Empty)
                        {
                            this.PaymentItemsGridView.Rows[rowIndex].Cells[columnIndex].ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (!this.ApplyReadonlyColumn)
                        {
                            this.PaymentItemsGridView.Rows[rowIndex].Cells[columnIndex].ReadOnly = false;
                        }
                    }
                }
            }
            else
            {
                this.PaymentItemsGridView.Rows[rowIndex].Cells[columnIndex].ReadOnly = true;
                this.nonEditable = true; 
            }

            this.PaymentItemsGridView.Rows[rowIndex].Cells["PaidByImage"].ReadOnly = true;
        }

        ////Added By Ramya.D For InstrumentHeaderPaymentdetails

        /// <summary>
        /// Loads the payment.
        /// </summary>
        public void LoadPaymentGrid()
        {
            this.loadComplete = false;
            this.PopulateInstrumentPaymentGrid(0);
            ////this.GetTenderTypeConfiguration();
            ////this.refundNow = false;
            this.loadComplete = true;
            this.PaymentEngineTabButton.TabStop = false;
            ////this.s
        }

        /// <summary>
        /// Loads the payment.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id value.</param>
        public void LoadPaymentGrid(int ppaymentIdValue)
        {
            this.loadComplete = false;
            this.PopulateInstrumentPaymentGrid(ppaymentIdValue);
            this.LockGrid(this.Locked);
            this.CalculateInsrumentPaymentTotal();
            //// this.ChangeScrollBarStatus();
            ////this.GetTenderTypeConfiguration();
            ////this.refundNow = false;
            this.loadComplete = true;
            this.PaymentEngineTabButton.TabStop = false;
        }

        /// <summary>
        /// Populates the payment grid.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id value.</param>
        private void PopulateInstrumentPaymentGrid(int ppaymentIdValue)
        {
            this.PPaymentId = ppaymentIdValue;
            ////fill paymentsDatatable with the value from the database
            this.FillpaymentsInstrumentDataTable(ppaymentIdValue);
            this.PaymentItemsGridView.DataSource = this.instrumentPaymentsDataTable;
            this.pidSource.DataSource = this.instrumentPaymentsDataTable;
            this.PaymentItemsGridView.Columns["PaidByImage"].Visible = false;
            this.PaymentItemsGridView.AutoGenerateColumns = false;
            this.instrumentHeaderDataSet = WSHelper.F49910_GetInstrumentTypeDetails();
            this.instrumentTenderTypeDataTable = this.instrumentHeaderDataSet.f49910_pclst_TenderType;   ////WSHelper.ListTenderType(this.allowOverUnder).ListTenderType;
            //// this.tenderTypeDataTable.Columns[0].DataType  
            //// Added By GuhanS To  restrict the tender type
            if (this.RestrictedTenderType)
            {
                StringBuilder stringExp = new StringBuilder();
                DataRow[] selectedTenderRows;
                DataRow tenderTypeNewRow;
                DataTable tenderTempTable = new DataTable();

                this.instrumentTenderTypeDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderTypeID", System.Type.GetType("System.Byte")), new DataColumn("TenderType", System.Type.GetType("System.String")) });
                int selectedTTLength = 0;
                if (this.availableTenderType != null)
                {
                    for (int rowid = 0; rowid <= this.availableTenderType.Length - 1; rowid++)
                    {
                        if (rowid != this.availableTenderType.Length - 1)
                        {
                            //// tempUserId = tempUserId + " userId <>" + userInRows[i][0].ToString() + " And ";
                            stringExp.Append(" TenderTypeID <>" + this.availableTenderType[rowid] + " And ");
                        }
                        else
                        {
                            //// tempUserId = tempUserId + "userId <>" + userInRows[i][0].ToString();
                            stringExp.Append("TenderTypeID <>" + this.availableTenderType[rowid]);
                        }
                    }
                }

                selectedTenderRows = this.instrumentTenderTypeDataTable.Select(stringExp.ToString());
                foreach (DataRow tenderTypeRow in selectedTenderRows)
                {
                    tenderTypeNewRow = tenderTempTable.NewRow();
                    tenderTypeNewRow["TenderTypeID"] = Convert.ToSByte(tenderTypeRow["TenderTypeID"].ToString());
                    tenderTypeNewRow["TenderType"] = tenderTypeRow["TenderType"].ToString();
                    tenderTempTable.Rows.Add(tenderTypeNewRow);
                }

                this.instrumentTenderTypeDataTable.Rows.Clear();
                //// PaymentEngineVscrollBar.Height = this.PaymentItemsGridView.Height; 
                try
                {
                    this.instrumentTenderTypeDataTable.Merge(tenderTempTable);
                }
                catch (Exception e1)
                {
                }
            }

            DataTable tempDataTable = new DataTable();
            tempDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("TenderTypeID") });
            tempDataTable.Rows.Add(new object[] { "", "" });
            for (int i = 0; i < this.instrumentTenderTypeDataTable.Rows.Count; i++)
            {
                tempDataTable.Rows.Add(new object[] { this.instrumentTenderTypeDataTable.Rows[i]["TenderType"].ToString(), this.instrumentTenderTypeDataTable.Rows[i]["TenderTypeID"].ToString() });
            }

            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DataSource = tempDataTable.Copy();
            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).DisplayMember = "TenderType";
            (PaymentItemsGridView.Columns["TenderType"] as DataGridViewComboBoxColumn).ValueMember = "TenderTypeID";

            ////calculating paymenttotal of the payments

            this.CalculateInsrumentPaymentTotal();
            ////this.PaymentPictureBox.Visible = this.WillShowLabel;
            ////this.PaymentPictureBox.Left = this.orginalWidth;
            //this.PaymentEngineVscrollBar.Width=this.PaymentEngineVscrollBar.Width + 1;
            this.orginalWidth = this.scrollBarPosition + this.PaymentEngineVscrollBar.Width-25;
            this.PaymentItemsGridView.Width = this.orginalWidth;
           this.PaymentEngineVscrollBar.Left = this.scrollBarPosition-25;  
            
        }

        /// <summary>
        /// Fillpaymentses the data table.
        /// </summary>
        /// <param name="ppaymentIdValue">The ppayment id.</param>
        private void FillpaymentsInstrumentDataTable(int ppaymentIdValue)
        {
            if (ppaymentIdValue > 0)
            {
                this.instrumentHeaderDataSet = WSHelper.F49910_GetInstrumentHeaderDetails(ppaymentIdValue);
            }
            else
            {
                this.instrumentHeaderDataSet.Clear();
            }

            this.instrumentPaymentsDataTable.Clear();

            if (this.instrumentPaymentsDataTable.Columns.Count <= 0)
            {
                this.instrumentPaymentsDataTable.Columns.AddRange(new DataColumn[] { new DataColumn("TenderType"), new DataColumn("PaidBy"), new DataColumn("CheckNumber"), new DataColumn("Amount", typeof(decimal)), new DataColumn("PID") });
            }

            for (int counter = 0; counter < this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows.Count; counter++)
            {
                DataRow dr = this.instrumentPaymentsDataTable.NewRow();
                dr["TenderType"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["TenderTypeID"].ToString();
                dr["PaidBy"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["PaidBy"].ToString();
                dr["CheckNumber"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["CheckNumber"].ToString();
                dr["Amount"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["Amount"].ToString();
                dr["PID"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["PaymentID"].ToString();
                ////dr["PPID"] = this.instrumentHeaderDataSet.f49901PaymentDetailsDataTable.Rows[counter]["PPaymentID"].ToString();
                this.instrumentPaymentsDataTable.Rows.Add(dr);
            }

            for (int counter = this.instrumentPaymentsDataTable.Rows.Count; counter < this.rowsVisibleNo; counter++)
            {
                this.instrumentPaymentsDataTable.Rows.Add(new object[] { string.Empty, string.Empty, string.Empty, System.DBNull.Value, string.Empty });
            }

            if (this.instrumentPaymentsDataTable.Rows.Count == this.rowsVisibleNo)
            {
                this.PaymentEngineVscrollBar.Visible = true;
                this.PaymentEngineVscrollBar.BringToFront();
            }
            else
            {

                this.PaymentEngineVscrollBar.Visible = false;
                this.PaymentItemsGridView.Width = this.orginalWidth;
            }
        }

        /// <summary>
        /// Calculatings the payment total.
        /// </summary>
        private void CalculateInsrumentPaymentTotal()
        {
            decimal outDecimalValue;
            decimal paymentsTotal = 0;
            int paymentCount = 0;
            ////this.instrumentPaymentsDataTable.AcceptChanges();
            for (int counter = 0; counter < this.instrumentPaymentsDataTable.Rows.Count; counter++)
            {
                if (Decimal.TryParse(this.instrumentPaymentsDataTable.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                {
                    //// Guhan added 
                    ////  minus for refund
                    ////if (this.paymentsDataTable.Rows[counter]["TenderType"].ToString() == "2")
                    ////{
                    ////    if (outDecimalValue > 0)
                    ////    {
                    ////        outDecimalValue = decimal.Negate(outDecimalValue);
                    ////    }
                    ////}

                    paymentsTotal += outDecimalValue;
                    paymentCount++;
                }
            }

            this.AmountTotal = paymentsTotal;
            this.OnPaymentAmountChangeEvent(this.AmountTotal);
            this.ChangeScrollBarStatus();
            if (this.loadComplete)
            {
                /* The below if condition and View is added by Jayanthi to stop adding empty rows when 
                 * any one of the Amount column is found to be "0.0" */
                if (paymentCount == this.instrumentPaymentsDataTable.Rows.Count)
                {
                    ////Ramya
                    ///// DataView tempDataView = new DataView(this.instrumentPaymentsDataTable, "Amount = '0.00'", "", DataViewRowState.CurrentRows);
                    ////if (tempDataView.Count == 0)
                    ////{
                    this.instrumentPaymentsDataTable.Rows.Add(new object[] { "", "", "", DBNull.Value, "" });
                    ////}
                    /////Ramya
                    this.PaymentEngineVscrollBar.Visible = false;
                    this.PaymentItemsGridView.Width = this.orginalWidth;
                    if (this.autoResizeWithOutppId)
                    {
                        this.PaymentEngineTabButton.TabStop = true;
                    }

                    ////this.PaymentItemsGridView.Refresh();
                }
                else
                {
                    this.PaymentEngineTabButton.TabStop = false;
                }
            }
        }

        /// <summary>
        /// SaveInstrumentPaymentDetails
        /// </summary>
        /// <param name="insId">insId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public int SaveInstrumentPaymentDetails(int insId, string instrumentItems, int userId)
        {
            int savedpPaymentId = 0;
            string paymentItems = null;
            DataSet tempDataSet = new DataSet("Root");
            ///Place the pid for the Update operation in Instument Table.
            for (int i = 0; i < this.instrumentPaymentsDataTable.Rows.Count ; i++)
            {
                if (!string.IsNullOrEmpty(this.instrumentPaymentsDataTable.Rows[i]["TenderType"].ToString()) && string.IsNullOrEmpty(this.instrumentPaymentsDataTable.Rows[i]["PID"].ToString()))
                {
                    this.instrumentPaymentsDataTable.Rows[i]["PID"] = "0";
                }
            }
            this.instrumentPaymentsDataTable.AcceptChanges();
            ////DataView tempDataView = new DataView(this.instrumentPaymentsDataTable, "TenderType <> ''", "", DataViewRowState.CurrentRows);
            DataView tempDataView = new DataView(this.instrumentPaymentsDataTable, "Amount <> '0.00' AND TenderType <> ''", "", DataViewRowState.CurrentRows);
            if (tempDataView.Count > 0)
            {
                DataTable tempDataTable = tempDataView.ToTable();
                tempDataSet.Tables.Add(tempDataTable);
                tempDataSet.Tables[0].TableName = "Table";
                paymentItems = tempDataSet.GetXml();
            }
            ////this.instrumentPaymentsDataTable.AcceptChanges();
            ////tempDataSet.Tables.Add(this.instrumentPaymentsDataTable);
            ////tempDataSet.Tables[0].TableName = "Table";
            ////paymentItems = tempDataSet.GetXml();

            ////if (!(String.IsNullOrEmpty(paymentItems)))
            ////{
            savedpPaymentId = WSHelper.F49910_SaveInstrumentHeaderDetails(insId, instrumentItems, paymentItems, userId);
            ////}

            ////if (this.GetRefundItemOccurence() >= 1)
            ////{
            ////    this.refundNow = true;
            ////}
            ////else
            ////{
            ////    this.refundNow = false;
            ////}

            this.PPaymentId = savedpPaymentId;
            this.LockGrid(this.Locked);
            return savedpPaymentId;
        }

        #endregion Private User Defined Members

        #region Event Handlers
        /// <summary>
        /// Handles the CellFormatting event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Decimal outDecimal;

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }
                //// Only paint if text provided, Only paint if desired text is in cell 
                if (e.Value != null && !String.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
                {
                    string val = e.Value.ToString();
                    if (Decimal.TryParse(val, out outDecimal))
                    {
                        if (outDecimal.ToString().Contains("-"))
                        {
                            e.Value = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.00"), ")");
                            e.CellStyle.ForeColor = Color.Green;
                        }
                        else if (this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString() == "2")
                        {
                            ////Added By Ramya.D For Sprint 41
                            if (!this.instrumentPayment && this.Tag != null && !this.Tag.Equals("1013") && !this.Tag.Equals("1410"))
                            ////Added last 2 conditions by Biju on 13/Apr/2010 to implement #6556.
                            {
                                e.Value = String.Concat("(", outDecimal.ToString("#,##0.00"), ")");
                                e.CellStyle.ForeColor = Color.Green;
                            }
                            else
                            {
                                e.Value = outDecimal.ToString("#,##0.00");
                            }
                        }
                        else
                        {
                            e.Value = outDecimal.ToString("#,##0.00");
                            e.FormattingApplied = true;
                        }
                    }
                    else
                    {
                        ////if (!this.ApplyInstrumentPayment)
                        ////{
                        e.Value = "0.00";
                        ////}
                        ////else
                        ////{
                        ////e.Value = this.ApplyInstrumentBalanceAmount;
                        ////this.instrumentPaymentsDataTable.Rows[e.RowIndex][e.ColumnIndex] = this.ApplyInstrumentBalanceAmount;
                        //////this.instrumentPaymentsDataTable.AcceptChanges();
                        //////this.instrumentPaymentsDataTable.AcceptChanges();
                        ////}
                    }
                }
                else
                {
                    e.Value = "";
                }
            }

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.ApplyInstrumentPayment && !String.IsNullOrEmpty(e.Value.ToString()))
                {
                    ///// this.PaymentItemsGridView["Amount", e.RowIndex].Value = this.ApplyInstrumentBalanceAmount;

                    ////this.PaymentItemsGridView["Amount", e.RowIndex].Value=
                }

                if (e.Value != null && String.IsNullOrEmpty(e.Value.ToString()))
                {
                    this.PaymentItemsGridView["PaidBy", e.RowIndex].ReadOnly = true;
                    this.PaymentItemsGridView["CheckNumber", e.RowIndex].ReadOnly = true;
                    this.PaymentItemsGridView["Amount", e.RowIndex].ReadOnly = true;
                    this.PaymentItemsGridView["PaidByImage", e.RowIndex].ReadOnly = true;
                    ////Added  By Ramya.D
                }

                ////this.PaymentItemsGridView["TenderType", e.RowIndex].ReadOnly = true;
                this.PaymentItemsGridView["PID", e.RowIndex].ReadOnly = true;
                this.PaymentItemsGridView["PPID", e.RowIndex].ReadOnly = true;
            }

            ///// Added By guhan on 18-june-07
            if (this.ApplyToolTip && e.RowIndex >= 0)
            {
                if (this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PID"].Index].Value != null && !string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PID"].Index].Value.ToString()) && e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["TenderType"].Index))
                {
                    DataGridViewCell cell = this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    cell.ToolTipText = "PID = " + this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.Columns["PID"].Index].Value.ToString();
                }
            }
            
            ////Tell here          
        }

        /// <summary>
        /// Handles the Load event of the PaymentEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentEngineUserControl_Load(object sender, EventArgs e)
        {
            this.GetTenderTypeConfiguration();
        }

        /// <summary>
        /// Handles the CellParsing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            Decimal outDecimal;
            Int64 outInt;

            // Only paint if desired column

            if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index)
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                //// Only paint if text provided, Only paint if desired text is in cell
                if (e.Value != null)
                {
                    string tempvalue = e.Value.ToString().Trim();
                    if (tempvalue.EndsWith("."))
                    {
                        tempvalue = string.Concat(tempvalue, "0");
                    }

                    if (Decimal.TryParse(tempvalue, System.Globalization.NumberStyles.Currency, null, out outDecimal))
                    {
                        //// check for Over / under based on tender type
                        //// ComboBox combo = (ComboBox)sender;

                        ////change property for combobox change

                        tempvalue = outDecimal.ToString();
                        tempvalue = tempvalue.Replace("-", "");

                        ////Added By ramya
                        ////if (!this.instrumentPayment)
                        ////{
                        if (!tempvalue.Contains("."))
                        {
                            tempvalue = tempvalue.PadLeft(2, '0').Insert(tempvalue.PadLeft(2, '0').Length - 2, ".");
                        }
                        ////}

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
                        

                        int tempTendertype = 0;
                        tempTendertype = Convert.ToInt32(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString());

                        if (tempTendertype > 0 && !this.ApplyInstrumentPayment)
                        {
                            switch (tempTendertype)
                            {
                                case 1:

                                  /*  if (this.totalReceiptAmount < this.overUnderMinAmount)
                                    {
                                        MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, "TenderType Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    ////Modified by Biju on 07/May/2010 to implement #6557
                                    else if (Math.Abs( outDecimal) > this.overUnderMaxAmount)
                                    {
                                        ////Added by Biju on 07/May/2010 to implement #6557
                                        if (outDecimal < 0 && this.Tag !=null && this.Tag.Equals("15018"))
                                            break;
                                        MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, SharedFunctions.GetResourceString("TenderTypeError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }*/
                                    this.IsOverUnder = true; 
                                    break;
                            }
                        }

                        e.ParsingApplied = true;
                    }
                    else
                    {
                        e.Value = decimal.Parse("0");
                        e.ParsingApplied = true;
                    }
                }
                this.IsEditLeave = true;
            }
            else if (e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index)
                {
                if (e.RowIndex < 0)
                {
                    return;
                }
                if (this.paymentsDataTable.Rows.Count > 0)
                {
                    if (this.paymentsDataTable.Rows[e.RowIndex]["TenderType"].ToString().Equals("1"))
                    {
                        this.IsOverUnder = true;
                    }
                }
                this.IsEditLeave = true;
                ////Check - Over/Under
                if (string.Equals(this.PaymentItemsGridView[e.ColumnIndex, e.RowIndex].Value.ToString(), "1"))
                {
                    this.IsOverUnder = true;
                    /*if (decimal.TryParse(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["Amount"].Index, e.RowIndex].Value.ToString(), System.Globalization.NumberStyles.Currency, null, out outDecimal))
                    {
                        if (this.totalReceiptAmount < this.overUnderMinAmount)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, "TenderType Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ////Modified by Biju on 07/May/2010 to implement #6557
                        else if (Math.Abs(outDecimal) > this.overUnderMaxAmount)
                        {
                            ////Added by Biju on 07/May/2010 to implement #6557
                            if (outDecimal < 0 && this.Tag!=null && this.Tag.Equals("15018"))
                                return ;
                            MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, SharedFunctions.GetResourceString("TenderTypeError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }*/
                }
            }
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_EditingControlShowing(object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            ////set color to editing control
            e.CellStyle.BackColor = Color.FromArgb(255, 255, 121);

            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).SelectionChangeCommitted -= new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                ((ComboBox)e.Control).SelectionChangeCommitted += new EventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                ((ComboBox)e.Control).SelectedIndexChanged -= new EventHandler(this.PaymentEngineUserControl_SelectedValueChanged);
                ((ComboBox)e.Control).SelectedIndexChanged += new EventHandler(this.PaymentEngineUserControl_SelectedValueChanged);
                ((ComboBox)e.Control).Leave -= new EventHandler(this.PaymentEngineUserControl_Leave);
                ((ComboBox)e.Control).Leave += new EventHandler(this.PaymentEngineUserControl_Leave);
                ((ComboBox)e.Control).KeyDown += new KeyEventHandler(this.PaymentEngineUserControl_KeyDown);
                //((ComboBox)e.Control).KeyPress -= new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted);
                //((ComboBox)e.Control).KeyPress += new KeyPressEventHandler(this.ReceiptEngine_SelectionChangeCommitted);  
            }
            //if (e.CellStyle is DataGridViewTextBoxEditingControl)
            //{
            //    ((TextBox)e.Control).TextChanged -= new EventHandler(this.PaymentEngineUserControl_SelectedValueChanged);
            //}
            if (this.PaymentItemsGridEditingControlShowing != null)
            {
                this.PaymentItemsGridEditingControlShowing(this, new System.Windows.Forms.DataGridViewEditingControlShowingEventArgs(e.Control, e.CellStyle));
            }
        }

        /// <summary>
        /// When the delete key pressed in the GridCombo all the values has to be cleared in the grid Row.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PaymentEngineUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            ////Delete Key = 46
            if (e.KeyValue == 46)
            {
                this.isDelete = true;  
                this.ReceiptEngine_SelectionChangeCommitted(sender, e);
            }
        }

        /// <summary>
        /// Handles the Leave event of the PaymentEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentEngineUserControl_Leave(object sender, EventArgs e)
        {
            this.previousRow = -1;
            ComboBox combo = (ComboBox)sender;
            //if (!this.IsLeave)
            //{
            //    this.ReceiptEngine_SelectionChangeCommitted(sender, e);
            //    this.IsLeave = true;
            //}
            //else
            //{
            
                if ((this.PaymentItemsGridView.Rows[this.PaymentItemsGridView.CurrentCell.RowIndex].Cells[this.PaymentItemsGridView.CurrentCell.ColumnIndex].Value!=null))
                {
                    this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;
                    if (this.edit)
                    {
                        this.isAutoSelect = false;
                        this.ReceiptEngine_SelectionChangeCommitted(sender, e);
                    }
                    else if (combo.SelectedIndex.Equals(0) && !this.isDelete)
                    {
                        if (!this.CellLeave)
                        {
                            this.isAutoSelect = true;
                            this.ReceiptEngine_SelectionChangeCommitted(sender, e);
                        }
                    }



                    this.CellLeave = true;
                }
                //else
                //    if (combo.SelectedIndex > 0)
                //    {
                //        this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;

                //    }
                else if (combo.SelectedIndex.Equals(0) && !this.isDelete)
                {

                    if (this.PaymentItemsGridView.CurrentCell != null)
                    {
                        this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;
                    }
                    if (this.previousRow >= 0)
                    {
                        if (!this.CellLeave)
                        {
                            this.isAutoSelect = true;
                            this.ReceiptEngine_SelectionChangeCommitted(sender, e);
                        }
                        this.CellLeave = true;
                    }


                }
                else
                {
                    if (combo.SelectedIndex > 0 && this.edit)
                    {
                        this.isAutoSelect = false;
                        this.ReceiptEngine_SelectionChangeCommitted(sender, e);
                    }
                    this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;
                }
            
              //}
            this.IsLeave = false;
                this.CheckTenderType(combo);
                ////Check - Over/Under
                if (this.previousRow>=0)
                {
                    //if (string.Equals(this.PaymentItemsGridView["TenderType", this.previousRow].Value.ToString(), "1"))
                    //{
                    //    decimal outDecimal;
                    //    if (decimal.TryParse(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["Amount"].Index, 0].Value.ToString(), System.Globalization.NumberStyles.Currency, null, out outDecimal))
                    //    {
                    //        if (this.totalReceiptAmount < this.overUnderMinAmount)
                    //        {
                    //            MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, "TenderType Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //        //Modified by Biju on 07/May/2010 to implement #6557
                    //        else if (Math.Abs(outDecimal) > this.overUnderMaxAmount)
                    //        {
                    //            ////Added by Biju on 07/May/2010 to implement #6557
                    //            if (outDecimal < 0 && this.Tag != null && this.Tag.Equals("15018"))
                    //                return;
                    //            MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, SharedFunctions.GetResourceString("TenderTypeError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //    }
                    //}
                }
                this.Isedit = true;
                this.edit = false; 
                this.isDelete = false;
                this.IsEditLeave = false;
                //To Check Amount Field is 0.00
            }
        

        /// <summary>
        /// Handles the SelectedValueChanged event of the PaymentEngineUserControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentEngineUserControl_SelectedValueChanged(object sender, EventArgs e)
            {
            this.IsLeave = true;
            if (this.Isedit && this.edit)
            {
              

                this.previousRow = -1;
                ComboBox combo = (ComboBox)sender;

                if (combo.SelectedIndex > 0)
                {
                    this.previousRow = this.PaymentItemsGridView.CurrentCell.RowIndex;
                    //this.CheckTenderType(combo);


                    // combo.SelectedValueChanged += new EventHandler(this.ReceiptEngine_SelectionChangeCommitted); 

                    this.ReceiptEngine_SelectionChangeCommitted(sender, e);
                    this.CellLeave = true;

                }
                else
                {
                    this.CellLeave = false;
                }

                ////Added bu ramya.d
            }
            else
            {
                //this.CellLeave = true;
                ComboBox combo = (ComboBox)sender;

                if (combo.SelectedIndex > 0)
                {
                    this.CellLeave = true;
                    ////Added by purushotham to solve Bug#19394 on March/12/2013
                    if (((System.Windows.Forms.ComboBox)(sender)).Text == "Over/Under")
                    {
                        SendKeys.Send("{TAB}");

                    }
                }
                else
                {
                    this.CellLeave = false;
                }
            }
           
            }

        //public bool selectionevent(sender, eventarg)
        //{
        //     ComboBox combo = (ComboBox)sender;
        //     this.ReceiptEngine_SelectionChangeCommitted(sender, eventarg);
        //    return true;
            
        //}

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the PaymentItemsGridView combobox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>        
        private void ReceiptEngine_SelectionChangeCommitted(object sender, EventArgs e)
            {
            try
            {
                this.Isedit = false;
                ComboBox combo = (ComboBox)sender;
                int rowIndex = 0;
                if (this.PaymentItemsGridView.CurrentCell != null)
                {
                    rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
                }
                ////code commented on 24-dec-2009 for the issue : 3393
                //Added by Biju on 07/Oct/2009 removing the visible = false code
                ////combo.Enabled = false; //combo.SendToBack();

                ////commented for the issue 3393.because of this flicker will come.
                ////combo.Visible = false;
                combo.SelectAll();
                ////change property for combobox change
                if (combo.SelectedIndex > 0 && combo.SelectedValue != null && this.loadComplete == true)
                {
                    this.CheckTenderType(combo);
                    this.OnPaymentItemChangeEvent();
                    //this.payeeDetails; 
                    if (combo.SelectedIndex > 0 && combo.SelectedValue != null)
                    {
                        
                        this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                            if(string.Equals(this.PaymentItemsGridView["TenderType", rowIndex].Value.ToString(), "1") && this.IsEditLeave)
                        {
                            this.IsOverUnder = true; 
                        }
                        this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["Address1", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["Address2", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["State", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["City", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["Zip", rowIndex].ReadOnly = false;
                        this.PaymentItemsGridView["Comment", rowIndex].ReadOnly = false;

                            if (!this.instrumentPayment)
                        {
                            ////For Unpaid Receipt Form - Added by Latha
                            if (this.isAutoPayment)
                            {
                                if (string.IsNullOrEmpty(this.PaymentItemsGridView["Amount", rowIndex].Value.ToString()))
                                {
                                    if (this.PaymentItemsGridView.Rows.Count > 0 && (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[rowIndex].Cells["TenderType"].Value.ToString())))
                                    {
                                        this.PaymentItemsGridView["Amount", rowIndex].Value = this.totalBalanceAmount;
                                    }
                                    if (rowIndex > 0)
                                    {
                                        this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PaidBy"].ToString();
                                        this.PaymentItemsGridView["Address1", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address1"].ToString();
                                        this.PaymentItemsGridView["Address2", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address2"].ToString();
                                        this.PaymentItemsGridView["City", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["City"].ToString();
                                        this.PaymentItemsGridView["State", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["State"].ToString();
                                        this.PaymentItemsGridView["Zip", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Zip"].ToString();
                                        this.PaymentItemsGridView["Comment", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Comment"].ToString();
                                        //this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PayeeID"].ToString();    
                                    }
                                    else
                                    {
                                        this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.ownerName;
                                        // Row count checked to avoid error throws while table is empty
                                        if (this.payeeDetails != null && this.payeeDetails.PayeeDetail.Rows.Count > 0)
                                        {
                                            this.PaymentItemsGridView["Address1", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address1"].ToString();
                                            this.PaymentItemsGridView["Address2", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address2"].ToString();
                                            this.PaymentItemsGridView["City", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["City"].ToString();
                                            this.PaymentItemsGridView["State", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["State"].ToString();
                                            this.PaymentItemsGridView["Zip", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Zip"].ToString();
                                            this.PaymentItemsGridView["Comment", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Comment"].ToString();
                                        }
                                        // this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.OwnerpayeeID;
                                    }
                                }
                                // this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[0, 0];
                                this.CalculatePaymentTotal();
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(this.PaymentItemsGridView["Amount", rowIndex].Value.ToString()))
                                {
                                    this.PaymentItemsGridView["Amount", rowIndex].Value = this.totalBalanceAmount;
                                    if (rowIndex > 0)
                                    {
                                        this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PaidBy"].ToString();
                                        this.PaymentItemsGridView["Address1", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address1"].ToString();
                                        this.PaymentItemsGridView["Address2", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address2"].ToString();
                                        this.PaymentItemsGridView["City", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["City"].ToString();
                                        this.PaymentItemsGridView["State", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["State"].ToString();
                                        this.PaymentItemsGridView["Zip", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Zip"].ToString();
                                        this.PaymentItemsGridView["Comment", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Comment"].ToString();
                                        //this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PayeeID"].ToString();    
                                    }
                                    else
                                    {
                                        this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.ownerName;
                                        // Row count checked to avoid error throws while table is empty
                                        if (this.payeeDetails != null && this.payeeDetails.PayeeDetail.Rows.Count > 0)
                                        {
                                            this.PaymentItemsGridView["Address1", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address1"].ToString();
                                            this.PaymentItemsGridView["Address2", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address2"].ToString();
                                            this.PaymentItemsGridView["City", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["City"].ToString();
                                            this.PaymentItemsGridView["State", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["State"].ToString();
                                            this.PaymentItemsGridView["Zip", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Zip"].ToString();
                                            this.PaymentItemsGridView["Comment", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Comment"].ToString();
                                        }
                                        // this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.OwnerpayeeID;
                                    }


                                }
                                this.CalculatePaymentTotal();

                            }
                        }

                        ////Coding Added for the co : 5880(Auto-fill payment grid in instrument header)

                        if (this.instrumentPayment)
                        {
                            if (string.IsNullOrEmpty(this.PaymentItemsGridView["PaidBy", rowIndex].Value.ToString()))
                            {
                                if (rowIndex > 0)
                                {
                                    this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.instrumentPaymentsDataTable.Rows[rowIndex - 1]["PaidBy"].ToString();
                                }
                                else
                                {
                                    this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.ownerName;
                                    this.changedOwnerName = false;
                                }
                            }

                            this.CalculateInsrumentPaymentTotal();
                        }
                    }
                    else
                    {

                        this.PaymentItemsGridView["TenderType", rowIndex].Value = "";
                        this.PaymentItemsGridView["PaidBy", rowIndex].Value = "";
                        this.PaymentItemsGridView["CheckNumber", rowIndex].Value = "";
                        this.PaymentItemsGridView["Amount", rowIndex].Value = 0;
                        this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = true;
                        this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = true;
                        this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = true;
                        this.PaymentItemsGridView["PaidByImage", rowIndex].ReadOnly = true;

                        ////For Unpaid Receipt Form - Added by Latha
                        if (this.isAutoPayment)
                        {
                            this.PaymentItemsGridView["Amount", rowIndex].Value = DBNull.Value;
                            this.CalculatePaymentTotal();
                        }

                        ////Coding Added for the co : 5880(Auto-fill payment grid in instrument header)
                        if (this.instrumentPayment)
                        {
                            this.PaymentItemsGridView["Amount", rowIndex].Value = DBNull.Value;
                            this.CalculateInsrumentPaymentTotal();
                        }
                    }
                }
                else
                {
                    if (this.PaymentItemsGridView.CurrentCell != null)
                    {
                        //if (this.payeeDetails.PayeeDetail.Rows.Count > 0)
                        //{
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["PaidBy"].Value = this.payeeDetails.PayeeDetail.Rows[0][0].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["Address1"].Value = this.payeeDetails.PayeeDetail.Rows[0][1].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["Address2"].Value = this.payeeDetails.PayeeDetail.Rows[0][2].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["City"].Value = this.payeeDetails.PayeeDetail.Rows[0][3].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["State"].Value = this.payeeDetails.PayeeDetail.Rows[0][4].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["Zip"].Value = this.payeeDetails.PayeeDetail.Rows[0][5].ToString();
                        //    this.PaymentItemsGridView.Rows[rowIndex].Cells["Comment"].Value = this.payeeDetails.PayeeDetail.Rows[0][6].ToString();

                        //}
                        //else
                        //{
                        if (combo.SelectedValue != null && this.isAutoSelect)
                        {
                            this.PaymentItemsGridView["TenderType", rowIndex].Value = combo.SelectedValue.ToString();
                            if (string.Equals(this.PaymentItemsGridView["TenderType", rowIndex].Value.ToString(), "1") && this.IsEditLeave)
                            {
                                this.IsOverUnder = true;
                            }
                            // this.PaymentItemsGridView.CurrentCell = null;
                            this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = true;
                            this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = true;
                            this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = true;
                            this.PaymentItemsGridView["PaidByImage", rowIndex].ReadOnly = true;
                            this.PaymentItemsGridView["Address1", rowIndex].Value = "";
                            this.PaymentItemsGridView["Address2", rowIndex].Value = "";
                            this.PaymentItemsGridView["City", rowIndex].Value = "";
                            this.PaymentItemsGridView["State", rowIndex].Value = "";
                            this.PaymentItemsGridView["Zip", rowIndex].Value = "";
                            this.PaymentItemsGridView["Comment", rowIndex].Value = "";
                            this.PaymentItemsGridView["PaidBy", rowIndex].Value = "";
                            this.PaymentItemsGridView["CheckNumber", rowIndex].Value = "";
                            this.PaymentItemsGridView["Amount", rowIndex].Value = 0;

                            ////For Unpaid Receipt Form - Added by Latha
                            if (this.isAutoPayment)
                            {
                                this.PaymentItemsGridView["Amount", rowIndex].Value = DBNull.Value;
                                this.CalculatePaymentTotal();
                            }

                            ////Coding Added for the co : 5880(Auto-fill payment grid in instrument header)
                            if (this.instrumentPayment)
                            {
                                this.PaymentItemsGridView["Amount", rowIndex].Value = DBNull.Value;
                                this.CalculateInsrumentPaymentTotal();
                            }
                        }
                        else
                        {
                            this.SelectionValue();
                            this.isAutoSelect = true;  
                        }
                }
                }
                if(this.edit)
                {
                    this.edit = false;
                }
                //Added by Biju on 07/Oct/2009 removing the visible = true code
                combo.Enabled = true; //combo.BringToFront();
                
                combo.Focus();
                combo.SelectedValueChanged -= new EventHandler(this.ReceiptEngine_SelectionChangeCommitted); 
            
            }
            catch (Exception ex)
            {
            }
        }


        private void SelectionValue()
        {
          int   rowIndex = this.PaymentItemsGridView.CurrentCell.RowIndex;
          this.OnPaymentItemChangeEvent();
            this.PaymentItemsGridView["PaidBy", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["Amount", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["CheckNumber", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["Address1", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["Address2", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["State", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["City", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["Zip", rowIndex].ReadOnly = false;
            this.PaymentItemsGridView["Comment", rowIndex].ReadOnly = false;

            if (!this.instrumentPayment)
            {
                ////For Unpaid Receipt Form - Added by Latha
                if (this.isAutoPayment)
                {
                    if (string.IsNullOrEmpty(this.PaymentItemsGridView["Amount", rowIndex].Value.ToString()))
                    {
                        //Added to fix TFS#21281 
                        if (this.PaymentItemsGridView.Rows.Count > 0 && (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[rowIndex].Cells["TenderType"].Value.ToString())))
                        {
                            this.PaymentItemsGridView["Amount", rowIndex].Value = this.totalBalanceAmount;
                        }
                           
                        if (rowIndex > 0)
                        {
                            this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PaidBy"].ToString();
                            this.PaymentItemsGridView["Address1", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address1"].ToString();
                            this.PaymentItemsGridView["Address2", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address2"].ToString();
                            this.PaymentItemsGridView["City", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["City"].ToString();
                            this.PaymentItemsGridView["State", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["State"].ToString();
                            this.PaymentItemsGridView["Zip", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Zip"].ToString();
                            this.PaymentItemsGridView["Comment", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Comment"].ToString();
                            //this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PayeeID"].ToString();    
                        }
                        else
                        {
                            this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.ownerName;
                            // Row count checked to avoid error throws while table is empty
                            if (this.payeeDetails != null && this.payeeDetails.PayeeDetail.Rows.Count > 0)
                            {
                                this.PaymentItemsGridView["Address1", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address1"].ToString();
                                this.PaymentItemsGridView["Address2", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address2"].ToString();
                                this.PaymentItemsGridView["City", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["City"].ToString();
                                this.PaymentItemsGridView["State", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["State"].ToString();
                                this.PaymentItemsGridView["Zip", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Zip"].ToString();
                                this.PaymentItemsGridView["Comment", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Comment"].ToString();
                            }
                            // this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.OwnerpayeeID;
                        }
                    }
                    // this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView[0, 0];
                    this.CalculatePaymentTotal();
                }
                else
                {
                    if (string.IsNullOrEmpty(this.PaymentItemsGridView["Amount", rowIndex].Value.ToString()))
                    {
                        this.PaymentItemsGridView["Amount", rowIndex].Value = this.totalBalanceAmount;
                        if (rowIndex > 0)
                        {
                            this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PaidBy"].ToString();
                            this.PaymentItemsGridView["Address1", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address1"].ToString();
                            this.PaymentItemsGridView["Address2", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Address2"].ToString();
                            this.PaymentItemsGridView["City", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["City"].ToString();
                            this.PaymentItemsGridView["State", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["State"].ToString();
                            this.PaymentItemsGridView["Zip", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Zip"].ToString();
                            this.PaymentItemsGridView["Comment", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["Comment"].ToString();
                            //this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.paymentsDataTable.Rows[rowIndex - 1]["PayeeID"].ToString();    
                        }
                        else
                        {
                            this.PaymentItemsGridView["PaidBy", rowIndex].Value = this.ownerName;
                            // Row count checked to avoid error throws while table is empty
                            if (this.payeeDetails != null && this.payeeDetails.PayeeDetail.Rows.Count > 0)
                            {
                                this.PaymentItemsGridView["Address1", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address1"].ToString();
                                this.PaymentItemsGridView["Address2", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Address2"].ToString();
                                this.PaymentItemsGridView["City", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["City"].ToString();
                                this.PaymentItemsGridView["State", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["State"].ToString();
                                this.PaymentItemsGridView["Zip", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Zip"].ToString();
                                this.PaymentItemsGridView["Comment", rowIndex].Value = this.payeeDetails.PayeeDetail.Rows[0]["Comment"].ToString();
                            }
                            // this.PaymentItemsGridView["PayeeID", rowIndex].Value = this.OwnerpayeeID;
                        }


                    }
                    this.CalculatePaymentTotal();

                }
            }
        }
        /// <summary>
        /// Handles the Leave event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_Leave(object sender, EventArgs e)
                    {
            try
            {
                this.PaymentEngineTabButton.TabStop = false;
               // SendKeys.Send("{TAB}");
               if (this.PaymentItemsGridView.CurrentCell != null)
                {
                    if (PaymentItemsGridView.IsCurrentRowDirty)
                    {
                        this.PaymentItemsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        this.PaymentItemsGridView.CurrentCell.DetachEditingControl();
                        //this.PaymentItemsGridView.Focus();
                        //this.PaymentItemsGridView.CurrentCell = null;
                      
                        //PaymentItemsGridView.CurrentRow.Cells[PaymentItemsGridView.CurrentCell.ColumnIndex].DetachEditingControl();
                        //this.PaymentItemsGridView.CurrentCell.Value = string.Empty;
                            }
                   // this.PaymentItemsGridView.CurrentCell = null;
                    
                }
            }
            catch////(InvalidOperationException ex) 
            {
            }
        }

        /// <summary>
        /// Handles the CellEndEdit event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.PaymentItemsGridView.Columns["Amount"].Index || e.ColumnIndex == this.PaymentItemsGridView.Columns["TenderType"].Index )
            {
                ////calculating related values for new values
                if (this.instrumentPayment)
                {
                    this.CalculateInsrumentPaymentTotal();
                }
                else
                {
                    this.CalculatePaymentTotal();

                    ////For Unpaid Receipt Form - Added by Latha
                    if (this.isAutoPayment)
                    {
                        if (this.PaymentItemsGridView.Rows.Count > 0 && this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value != null)
                        {
                            if (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
                            {
                                //Changes in Cash Back Item automatically for underpayment Manoj P #17427 
                                if (this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString().Equals("4") && this.AmountTotal > this.totalDue)
                                {
                                    int paymentCount = 0;
                                    Decimal outDecimalValue;
                                    for (int counter = 0; counter < this.paymentsDataTable.Rows.Count; counter++)
                                    {
                                        if (Decimal.TryParse(this.paymentsDataTable.Rows[counter]["Amount"].ToString(), out outDecimalValue))
                                        {
                                            paymentCount++;
                                        }
                                    }

                                    if (paymentCount == this.paymentsDataTable.Rows.Count)
                                    {
                                        DataView tempDataView = new DataView(this.paymentsDataTable, "Amount = '0.00'", "", DataViewRowState.CurrentRows);

                                        if (tempDataView.Count == 0)
                                        {
                                            this.paymentsDataTable.Rows.Add(new object[] { "11", "", this.paymentsDataTable.Rows[paymentCount - 1]["PaidBy"].ToString(), this.totalDue - this.AmountTotal, "", "" });
                                        }

                                        this.PaymentEngineVscrollBar.Visible = false;
                                        this.PaymentItemsGridView.Width = this.orginalWidth;
                                        if (this.autoResizeWithOutppId)
                                        {
                                            this.PaymentEngineTabButton.TabStop = true;
                                        }
                                    }
                                    else
                                    {
                                        ////DataView tempDataView = new DataView(this.paymentsDataTable, "Amount = '0.00'", "", DataViewRowState.CurrentRows);
                                        ////if (tempDataView.Count == 0)
                                        ////{
                                        if (paymentCount > 0)
                                        {
                                            this.paymentsDataTable.Rows[paymentCount]["TenderType"] = "11";
                                            this.paymentsDataTable.Rows[paymentCount]["PaidBy"] = this.paymentsDataTable.Rows[paymentCount - 1]["PaidBy"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["Address1"] = this.paymentsDataTable.Rows[paymentCount - 1]["Address1"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["Address2"] = this.paymentsDataTable.Rows[paymentCount - 1]["Address2"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["City"] =  this.paymentsDataTable.Rows[paymentCount - 1]["City"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["State"] =  this.paymentsDataTable.Rows[paymentCount - 1]["State"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["Zip"] = this.paymentsDataTable.Rows[paymentCount - 1]["Zip"].ToString();
                                            this.paymentsDataTable.Rows[paymentCount]["Comment"] = this.paymentsDataTable.Rows[paymentCount - 1]["Comment"].ToString();  
                                            this.paymentsDataTable.Rows[paymentCount]["Amount"] = this.totalDue - this.AmountTotal;
                                        }
                                        ////}
                                        this.PaymentEngineTabButton.TabStop = false;
                                    }

                                    this.CalculatePaymentTotal();
                                }
                            }
                        }
                    }
                    //if (this.PaymentItemsGridView.Rows.Count > 0)
                    //{
                    //   this.paymentsDataTable.Rows[e.RowIndex]["PaidBy"]= this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Value;
                    //}
                }
                if(this.PaymentItemsGridView["TenderType", e.RowIndex].Value != null)
                {
                    if (string.Equals(this.PaymentItemsGridView["TenderType", e.RowIndex].Value.ToString(), "1") && !this.ApplyInstrumentPayment && this.IsOverUnder)
                    {
                        
                        decimal outDecimal;
                        if (decimal.TryParse(this.PaymentItemsGridView[this.PaymentItemsGridView.Columns["Amount"].Index, e.RowIndex].Value.ToString(), System.Globalization.NumberStyles.Currency, null, out outDecimal))
                        {
                            string tempvalue = outDecimal.ToString();
                            if (tempvalue.Contains("-"))
                            {
                                if (this.totalReceiptAmount < this.overUnderMinAmount)
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, "TenderType Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                //Modified by Biju on 07/May/2010 to implement #6557
                                    ////Modified by purushotham to implement #16331
                                else if (System.Math.Abs(outDecimal) > this.overUnderMaxAmount)
                                {
                                    ////Added by Biju on 07/May/2010 to implement #6557
                                    if (outDecimal < 0 && this.Tag != null && this.Tag.Equals("15018"))
                                        return;
                                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, SharedFunctions.GetResourceString("TenderTypeError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (this.totalReceiptAmount < this.overUnderMinAmount)
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderLesser") + this.overUnderMinAmount, "TenderType Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                //Modified by Biju on 07/May/2010 to implement #6557
                                else if (Math.Abs(outDecimal) > this.overUnderMaxAmount)
                                {
                                    ////Added by Biju on 07/May/2010 to implement #6557
                                    if (outDecimal < 0 && this.Tag != null && this.Tag.Equals("15018"))
                                        return;
                                    MessageBox.Show(SharedFunctions.GetResourceString("OverUnderGreater") + this.overUnderMaxAmount, SharedFunctions.GetResourceString("TenderTypeError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            this.IsOverUnder = false;
                        }
                    }
                }
            }
              
        }

        /// <summary>
        /// Handles the CellEnter event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["PID"].Index) || e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["PPID"].Index))
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["TenderType"].Index) || !string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))
            {
                if (this.Locked)
                {
                    this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                }
                else
                {
                    if (this.RestrictedTenderType)
                    {
                        if (this.PaymentItemsGridView["PID", e.RowIndex].Value.ToString() != "0" && this.PaymentItemsGridView["PID", e.RowIndex].Value.ToString() != "")
                        {
                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                        }
                        else
                        {
                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (!this.ApplyReadonlyColumn)
                        {
                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                        }
                    }
                }

                this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidByImage"].ReadOnly = true;
            }
            else
            {
                this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;

            }
        }

        /// <summary>
        /// Handles the CellClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].ColumnIndex.Equals(e.ColumnIndex))
                {
                    this.edit = true;
                }
                
                this.MakeCellsEditable(e.RowIndex, e.ColumnIndex);
                ////if (e.ColumnIndex == 0 || !string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells[0].Value.ToString()))
                ////{
                ////    if (this.Locked)
                ////    {
                ////        this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                ////    }
                ////    else
                ////    {
                ////        if (this.RestrictedTenderType)
                ////        {
                ////            if (this.PaymentItemsGridView["PID", e.RowIndex].Value.ToString() != "0" && this.PaymentItemsGridView["PID", e.RowIndex].Value.ToString() != "")
                ////            {
                ////                this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                ////            }
                ////            else
                ////            {
                ////                this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                ////            }
                ////        }
                ////        else
                ////        {
                ////            this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    this.PaymentItemsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                ////}
            }
        }

        /// <summary>
        /// Handles the DataError event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        /// <summary>
        /// Handles the CellValueChanged event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //ReceiptEngine_SelectionChangeCommitted(sender, e);
           // PaymentItemsGridView.CurrentCellDirtyStateChanged += new EventHandler(this.PaymentItemsGridView_CurrentCellDirtyStateChanged); 
            this.OnPaymentItemChangeEvent();
           // this.paymentEngineDataSet.AcceptChanges();

        }

        /// <summary>
        /// Handles the RowHeaderMouseClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                //// this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.CurrentCell.ColumnIndex].ReadOnly = true;
            }
        }

        /// <summary>
        /// Handles the RowHeaderMouseDoubleClick event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                this.PaymentItemsGridView.Rows[e.RowIndex].Cells[this.PaymentItemsGridView.CurrentCell.ColumnIndex].ReadOnly = true;
            }
        }

        /// <summary>
        /// Handles the Enter event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_Enter(object sender, EventArgs e)
        {
            if (this.PaymentItemsGridView.CurrentCell != null)
            {
                this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView["TenderType", this.PaymentItemsGridView.CurrentRow.Index];
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the PaymentItemsGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void PaymentItemsGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.edit = true;
           // this.ReceiptEngine_SelectionChangeCommitted(sender, e);
            //this.paymentsDataTable.AcceptChanges();
            //this.PaymentItemsGridView.DataSource = this.paymentsDataTable;
        }

        #endregion Event Handlers

        private void PaymentItemsGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
            for (int i = 0; i < this.PaymentItemsGridView.Rows.Count; i++)
            {
                TerraScanTextAndImageCell imgCell = (TerraScanTextAndImageCell)this.PaymentItemsGridView["PaidByImage", i];
                imgCell.ImagexLocation = 1;
                imgCell.ImageyLocation = 1;
                //imgCell.Image = ExtendedGraphics.GenerateImage(1, 1, Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.R), Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.G), Convert.ToInt32(this.PaymentItemsGridView[0, i].InheritedStyle.BackColor.B));
            }
             
        }

        private void PaymentItemsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (!this.nonEditable)
                //{
                    if (e.RowIndex >= 0 && e.ColumnIndex.Equals(this.PaymentItemsGridView.Columns["PaidByImage"].Index))
                    {
                        if (!string.IsNullOrEmpty(this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].Value.ToString()))// if (!this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].ReadOnly && !this.PaymentItemsGridView.Rows[e.RowIndex].Cells["TenderType"].ReadOnly)
                        {
                            if ((e.X >= 2) && (e.X <= (34 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))//((e.X >= 300) && (e.X <= (331 - 10)) && (e.Y >= 1) && (e.Y <= (22 - 1)))
                            {
                                DataTable tempTable = new DataTable();
                                string PayeeItemXml;

                                if (tempTable.Columns.Count <= 0)
                                {
                                    tempTable.Columns.AddRange(new DataColumn[] { new DataColumn("PaidBy"), new DataColumn("Address1"), new DataColumn("Address2"), new DataColumn("City"), new DataColumn("State"), new DataColumn("Zip"), new DataColumn("Comment") });
                                }
                                ///Column information

                                if (tempTable.Rows.Count == 0)
                                {
                                    DataRow tempRow = tempTable.NewRow();
                                    tempRow["PaidBy"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["PaidBy"].ToString();
                                    tempRow["Address1"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address1"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Address1"].ToString();
                                    tempRow["Address2"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address2"].Value.ToString(); //this.ownerDetailDataSet.GetPayment.Rows[0]["Address2"].ToString();
                                    tempRow["City"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["City"].Value.ToString();   //this.ownerDetailDataSet.GetPayment.Rows[0]["City"].ToString();
                                    tempRow["State"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["State"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["State"].ToString();
                                    tempRow["Zip"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Zip"].Value.ToString(); //this.ownerDetailDataSet.GetPayment.Rows[0]["Zip"].ToString();
                                    tempRow["Comment"] = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value.ToString();  //this.ownerDetailDataSet.GetPayment.Rows[0]["Comment"].ToString();
                                    //tempRow["PaymentID"] = 0; 
                                    tempTable.Rows.Add(tempRow);
                                }
                                DataSet tempDataSet = new DataSet("Root");
                                tempDataSet.Tables.Add(tempTable);
                                tempDataSet.Tables[0].TableName = "Table";
                                string payeeIDs = tempDataSet.GetXml();
                                object[] optionalParameter = new object[2];
                                optionalParameter[0] = payeeIDs;
                                optionalParameter[1] = !this.Locked;
                                //this.parentWorkItem = D1018.F1019WorkItem;   
                                //ownerDetailDataSet = this.form1019Control.WorkItem.F1019_GetPayeeDetails(this.OwnerpayeeID); 
                                Form PayeeDetailsForm = TerraScanCommon.GetForm(1019, optionalParameter, this.parentWorkItem);
                                if (PayeeDetailsForm != null) ////&& accountSelectionForm.ShowDialog() == DialogResult.Yes)
                                {
                                    if (PayeeDetailsForm.ShowDialog() == DialogResult.OK)
                                    {
                                        string PayeeInfo = TerraScanCommon.GetValue(PayeeDetailsForm, SharedFunctions.GetResourceString("PayeeID").ToString());
                                        //"<Root><Table><Test1>Test1</Test1><Test2>Test2</Test2></Table></Root>";
                                        System.IO.StringReader stringReader = new System.IO.StringReader(PayeeInfo);
                                        System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(stringReader);
                                        //System.Xml.XmlReader reader = new System.Xml.XmlReader();
                                        DataSet ds = new DataSet();
                                        ds.ReadXml(textReader);

                                        if (ds.Tables.Count > 0)
                                        {
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Value = ds.Tables[0].Rows[0]["PaidBy"].ToString();
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address1"].Value = ds.Tables[0].Rows[0]["Address1"].ToString();
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Address2"].Value = ds.Tables[0].Rows[0]["Address2"].ToString();
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["City"].Value = ds.Tables[0].Rows[0]["City"].ToString();
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["State"].Value = ds.Tables[0].Rows[0]["State"].ToString();
                                            this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Zip"].Value = ds.Tables[0].Rows[0]["Zip"].ToString();
                                            //if (!this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value.Equals(ds.Tables[0].Rows[0]["Comment"].ToString()))
                                            //{
                                                this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value = ds.Tables[0].Rows[0]["Comment"].ToString().Trim();
                                            //}
                                            //this.PaymentItemsGridView.Rows[e.RowIndex].Cells["Comment"].Value = ds.Tables[0].Rows[0]["Comment"].ToString().Trim();
                                        }

                                        //this.paymentsDataTable.AcceptChanges();
                                        this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"].Selected = true;
                                        this.PaymentItemsGridView.CurrentCell = this.PaymentItemsGridView.Rows[e.RowIndex].Cells["PaidBy"];
                                        //this.PaymentItemsGridView.Rows[e.RowIndex].Cells[0].ToString()=      
                                    }
                                }
                            }
                        }
                    }
                //}
               // this.nonEditable = false;
            }
        
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }

        }

        private void PaymentItemsGridView_Paint(object sender, PaintEventArgs e)
        {
              try
            {
                int firstColumn = this.PaymentItemsGridView.Columns["PaidBy"].Index;
                int secondColumn = this.PaymentItemsGridView.Columns["PaidByImage"].Index;
                Rectangle r1 = this.PaymentItemsGridView.GetCellDisplayRectangle(firstColumn, -1, true);
                Rectangle r2 = this.PaymentItemsGridView.GetCellDisplayRectangle(secondColumn, -1, true); 
                r1.X += 1;
                r1.Y += 1; 
                r1.Width += r2.Width - 2; 
                r1.Height -= 2;

                // Draw color
                using (SolidBrush br = new SolidBrush(this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.BackColor))
                {
                    e.Graphics.FillRectangle(br, r1);
                }  
   
                // Draw header text   
                using (SolidBrush br = new SolidBrush(this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.ForeColor))
                {              
                    StringFormat sf = new StringFormat();
                    // Set X, Y position to display header text
                    r1.X += 160;
                    r1.Y += 4;
                    e.Graphics.DrawString("Paid By", this.PaymentItemsGridView.ColumnHeadersDefaultCellStyle.Font, br, r1, sf);   
                } 
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }
        }
    }

