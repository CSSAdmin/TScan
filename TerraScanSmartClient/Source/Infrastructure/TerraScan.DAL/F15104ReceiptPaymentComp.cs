// -------------------------------------------------------------------------------------------
// <copyright file="F15104ReceiptPaymentComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F15104ReceiptPaymentComp
    /// </summary>
    public static class F15104ReceiptPaymentComp
    {
        /// <summary>
        /// F15104_s the get receipt payment.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>ReceiptPayamentDataSet</returns>
        public static F15104ReceiptPayamentData F15104_GetReceiptPayment(int receiptId)
        {
            F15104ReceiptPayamentData receiptPayamentDataSet = new F15104ReceiptPayamentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(receiptPayamentDataSet.ReceiptPaymentTable, "f1000_pclst_ReceiptPaymentItem", ht);
            return receiptPayamentDataSet;
        }

        /// <summary>
        /// F15104_s the update receipt payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">userId</param>
        public static void F15104_UpdateReceiptPayment(string receiptPayment, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptPayListing", receiptPayment);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15104_pcupd_ReceiptPaymentItem", ht);
        }
    }
}
