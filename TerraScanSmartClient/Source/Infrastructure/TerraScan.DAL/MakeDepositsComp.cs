// -------------------------------------------------------------------------------------------
// <copyright file="MakeDepositsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access comment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------;
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
    /// MakeDepositsComp
    /// </summary>
    public static class MakeDepositsComp
    {
        #region GetPaymentItemsDetails

        /// <summary>
        /// Gets the PaymentItems Details
        /// </summary>
        /// <returns>The dataset containing the comments.</returns>
        public static MakeDepositsData GetPaymentItemsDetails()
        {
            MakeDepositsData makeDepositsData = new MakeDepositsData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { makeDepositsData.ListPaymentItemTable.TableName, makeDepositsData.ListAccountsGridTable.TableName };
            Utility.LoadDataSet(makeDepositsData, "f1212_pclst_PaymentItem", ht, tableName);
            return makeDepositsData;
        }

        #endregion

        #region SavePaymentItemsDetails

        /// <summary>
        /// Saves the payment items details.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="paymentItemsDetails">The payment items details.</param>
        public static void SavePaymentItemsDetails(int registerId, decimal amount, int userId, string paymentItemsDetails)
        {
            ////MakeDepositsData exciseTaxRateData = new MakeDepositsData();
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            ht.Add("@Amount", amount);
            ht.Add("@UserID", userId);
            ht.Add("@PaymentItems", paymentItemsDetails);
            Utility.ImplementProcedure("f1212_pcins_CashLedger", ht);
        }

        #endregion
    }
}
