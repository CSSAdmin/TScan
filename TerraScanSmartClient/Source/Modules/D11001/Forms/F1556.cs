//--------------------------------------------------------------------------------------------
// <copyright file="F1556.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1555.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 01/10/2010   D.LathaMaheswari       Created
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;

    /// <summary>
    /// Partial class for F1556
    /// </summary>
    public partial class F1556 : Form
    {
        #region Variables
 
        /// <summary>
        /// Created instance for the Typed Dataset
        /// </summary>
        private F1555_ReceiptDetailsData.ReverseSharedPaymentDataTable loadReceiptDetailsData = new F1555_ReceiptDetailsData.ReverseSharedPaymentDataTable();

        /// <summary>
        /// form1555Control
        /// </summary>
        private F1556Controller form1556Control;

        /// <summary>
        ///  Used To Store receiptId
        /// </summary>
        private int receiptId;

        /// <summary>
        /// Shared Payment ID
        /// </summary>
        private int sharedPaymentId;

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new class.
        /// </summary>
        public F1556()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1556"/> class.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        public F1556(int receiptId, int paymentId)
        {
            InitializeComponent();
            this.receiptId = receiptId;
            this.sharedPaymentId = paymentId;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the reverse receipt controll.
        /// </summary>
        /// <value>The reverse receipt template controll.</value>
        [CreateNew]
        public F1556Controller F1556Controll
        {
            get { return this.form1556Control as F1556Controller; }
            set { this.form1556Control = value; }
        }

        #endregion Properties

        #region Events

        /// <summary>
        /// Handles the Load event of the F1556 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1556_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadReversePaymentDetails();
                this.CancelButton = this.NoButton;
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void NoButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void YesButton_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events

        #region Private Methods

        /// <summary>
        /// Load Reverse Payment Details
        /// </summary>
        private void LoadReversePaymentDetails()
        {
            this.loadReceiptDetailsData = this.form1556Control.WorkItem.F1556_ReverseReceiptDetails(this.receiptId, this.sharedPaymentId, TerraScanCommon.UserId);

            if (this.loadReceiptDetailsData.Rows.Count > 0)
            {
                F1555_ReceiptDetailsData.ReverseSharedPaymentRow receiptDetails = (F1555_ReceiptDetailsData.ReverseSharedPaymentRow)this.loadReceiptDetailsData.Rows[0];

                string sharedReceipts = "0";
                string reversedReceipts = "0";
                string reversalAmount = "0.00";

                if (!receiptDetails.IsSharedReceiptsNull())
                {
                    sharedReceipts = receiptDetails.SharedReceipts.ToString("#,##0");
                }

                if (!receiptDetails.IsReversedReceiptsNull())
                {
                    reversedReceipts = receiptDetails.ReversedReceipts.ToString("#,##0");
                }

                if (!receiptDetails.IsReversalAmountNull())
                {
                    reversalAmount = receiptDetails.ReversalAmount.ToString("#,##0.00");
                }

                //this.FirstLineLabel.Text = "This payment is shared by " + sharedReceipts + " Receipts.";
                //this.SecondLineLabel.Text = reversedReceipts + " of those Recipt will be Reversed by this process.";
                //this.ThirdLineLabel.Text = "This will Reverse a total of $" + reversalAmount + "  in collected funds.";
                //this.FourthLineLabel.Text = "Are you sure you want to continue?";

                this.FirstLineLabel.Text = SharedFunctions.GetResourceString("FirstLineFirstPart") + " " + sharedReceipts + " " + SharedFunctions.GetResourceString("FirstLineLastPart");
                this.SecondLineLabel.Text = reversedReceipts + " " + SharedFunctions.GetResourceString("SecondLineText");
                this.ThirdLineLabel.Text = SharedFunctions.GetResourceString("ThirdLineFirstPart") + reversalAmount + " " + SharedFunctions.GetResourceString("ThirdLineLastPart");
                this.FourthLineLabel.Text = SharedFunctions.GetResourceString("FourthLineText");
            }
        }

        #endregion Private Methods       
    }
}