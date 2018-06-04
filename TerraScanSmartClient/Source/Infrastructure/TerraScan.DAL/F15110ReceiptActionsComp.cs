// -------------------------------------------------------------------------------------------
// <copyright file="F15110ReceiptActionsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15110ReceiptActionsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 08/05/07         S. Pradeep         Created
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
    /// F15110ReceiptActionsComp Class file
    /// </summary>
    public static class F15110ReceiptActionsComp
    {
        #region Receipt Actions

        #region Get Receipt Actions Details

        /// <summary>
        /// Gets the Receipt actions details based on the ReceiptId
        /// </summary>
        /// <param name="receiptId">The ReceiptId.</param>
        /// <returns>
        /// The typed dataset containing the Receipt Actions information of the ReceiptId.
        /// </returns>
        public static F15110ReceiptActionsData F15110_GetReceiptActions(int receiptId)
        {
            F15110ReceiptActionsData receiptActionsData = new F15110ReceiptActionsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(receiptActionsData.GetReceiptAction, "f15110_pcget_ReceiptAction", ht);
            return receiptActionsData;
        }

        /// <summary>
        /// F1557_s the refund interest.
        /// </summary>
        /// <param name="receiptID">The receipt ID.</param>
        /// <param name="userID">The user ID.</param>
        public static void F1557_InsertRefundInterest(int receiptID, int userID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptID);
            ht.Add("@UserID", userID);
            DataProxy.ExecuteSP("f1557_pcins_RefundInterest", ht);
        }
        #endregion Get Receipt Actions Details

        #endregion Receipt Actions
    }
}
