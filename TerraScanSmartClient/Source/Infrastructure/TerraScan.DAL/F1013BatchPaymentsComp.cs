// -------------------------------------------------------------------------------------------
// <copyright file="F1013BatchPaymentsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1013BatchPaymentsComp
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 22/05/07         Sadashivudu        Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// BatchPayments Component Class.
    /// </summary>
    public static class F1013BatchPaymentsComp
    {
        #region F1013BatchPaymentsComp

        #region F1013_ListUnpaidReceipts

        /// <summary>
        /// F1013_s the list unpaid receipts.
        /// </summary>
        /// <param name="userId">userId.</param>
        /// <returns>returns BatchPaymentMgmtDataSet.</returns>
        public static F1013BatchPaymentMgmtData F1013_ListUnpaidReceipts(int? userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            F1013BatchPaymentMgmtData batchPaymentMgmtData = new F1013BatchPaymentMgmtData();
            string[] tableName = new string[] { batchPaymentMgmtData.ListUnpaidReceiptUsers.TableName, batchPaymentMgmtData.ListUnpaidReceipts.TableName, batchPaymentMgmtData.ListDateReciepts.TableName };
            Utility.LoadDataSet(batchPaymentMgmtData, "f1013_pclst_Receipts", ht, tableName);
            return batchPaymentMgmtData;
        }

        #endregion F1013_ListUnpaidReceipts

        #region F1013_SaveBatchPayment

        /// <summary>
        /// F1013_s the save batch payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="paymentItemsXml">The payment items XML.</param>
        /// <param name="receiptItemsXml">The receipt items XML.</param>
        /// <returns>returns the error id.</returns>
        public static int F1013_SaveBatchPayment(int ppaymentId, int userId, string paymentItemsXml, string receiptItemsXml, string receiptDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PPaymentID", ppaymentId);
            ht.Add("@UserID", userId);
            ht.Add("@PaymentItems", paymentItemsXml);
            ht.Add("@ReceiptItems", receiptItemsXml);
            ht.Add("@ReceiptDate", receiptDate);  
            return Utility.FetchSPExecuteKeyId("f1013_pcupd_Payments", ht);
        }

        #endregion F1013_SaveBatchPayment

        #region F1013_ListSnapShotItems

        /// <summary>
        /// To list snap shot items collection.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <returns>Typed dataset containing the snap shot items collection</returns>
        public static F1013BatchPaymentMgmtData F1013_ListSnapShotItems(int snapShotId)
        {
            F1013BatchPaymentMgmtData form1013BatchPaymentMgmtData = new F1013BatchPaymentMgmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            Utility.LoadDataSet(form1013BatchPaymentMgmtData.ListSnapShotItems, "f1013_pclst_SnapShotItems", ht);
            return form1013BatchPaymentMgmtData;
        }

        #endregion F1013_ListSnapShotItems

        #region F1013_DeleteReceiptItems

        /// <summary>
        /// F1013_s the delete receipt items.
        /// </summary>
        /// <param name="paymentId">The payment id.</param>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public static int F1013_DeleteReceiptItems(int paymentId, string receiptItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PPaymentID", paymentId);
            ht.Add("@ReceiptItems", receiptItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1013_pcdel_Receipts", ht);
        }

        #endregion F1013_DeleteReceiptItems

        #endregion F1013BatchPaymentsComp
    }
}
