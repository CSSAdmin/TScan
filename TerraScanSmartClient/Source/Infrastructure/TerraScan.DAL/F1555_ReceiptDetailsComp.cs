// -------------------------------------------------------------------------------------------
// <copyright file="F1555_ReceiptDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1555_ReceiptDetailsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 11/05/07         S. Pradeep         Created
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
    /// F1555_ReceiptDetailsComp Class file
    /// </summary>
    public static class F1555_ReceiptDetailsComp
    {
        #region Receipt Details

        #region Get Receipt Details

        /// <summary>
        /// Gets the Receipt actions details based on the ReceiptId
        /// </summary>
        /// <param name="receiptId">The ReceiptId.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Actions information of the ReceiptId.
        /// </returns>
        public static F1555_ReceiptDetailsData F1555_GetReceiptDetails(int receiptId)
        {
            F1555_ReceiptDetailsData receiptDetailsData = new F1555_ReceiptDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            string[] tableName = new string[] { receiptDetailsData.GetReceiptDetails.TableName, receiptDetailsData.DispositionList.TableName, receiptDetailsData.SharedPaymentList.TableName };
            Utility.LoadDataSet(receiptDetailsData, "f1555_pcget_ReceiptDetails", ht, tableName);
            return receiptDetailsData;
        }

        #endregion Get Receipt Details

        #region Reverse Receipt Details

        /// <summary>
        /// Reverse receipt details
        /// </summary>
        /// <param name="receiptId">Receipt ID</param>
        /// <param name="sharedPaymentId">Shared Payment Id</param>
        /// <param name="userId">User ID</param>
        /// <returns>Reverse Payment Details</returns>
        public static F1555_ReceiptDetailsData F1556_ReverseReceiptDetails(int receiptId, int sharedPaymentId, int userId)
        {
            F1555_ReceiptDetailsData receiptDetailsData = new F1555_ReceiptDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            ht.Add("@SharedPaymentID", sharedPaymentId);
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { receiptDetailsData.ReverseSharedPayment.TableName };
            Utility.LoadDataSet(receiptDetailsData, "f1556_pcget_ReverseSharedPayment", ht, tableName);
            return receiptDetailsData;
        }

        #endregion Reverse Receipt Details

        #endregion Receipt Details
    }
}
