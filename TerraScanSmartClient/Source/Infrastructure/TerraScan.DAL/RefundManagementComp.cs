// -------------------------------------------------------------------------------------------
// <copyright file="RefundManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to Refund Management  related information</summary>
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
    /// RefundManagementComp class file
    /// </summary>
    public static class RefundManagementComp
    {
        #region List AccontNames

        /// <summary>
        /// F1214 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static RefundManagementData.ListAccountNamesDataTable F1214_AccountNames()
        {
            RefundManagementData refundManagementData = new RefundManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(refundManagementData.ListAccountNames, "f1213_pclst_AccountName", ht);
            return refundManagementData.ListAccountNames;
        }

        #endregion

        #region ListRefundPayments
        /// <summary>
        /// Lists the refund payments data.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>refundManagementData</returns>
        public static RefundManagementData.ListRefundPaymentsDataTable ListRefundPaymentsData(int form, string whereCondnSql)
        {
            RefundManagementData refundManagementData = new RefundManagementData();
            Hashtable ht = new Hashtable();
            if (form != 0)
            {
                ht.Add("@Form", form);
            }
            else
            {
                ht.Add("@Form", DBNull.Value);
            }

                ht.Add("@WhereCondnSql", whereCondnSql);

                Utility.LoadDataSet(refundManagementData.ListRefundPayments, "f1214_pclst_RefundPayments", ht);
            return refundManagementData.ListRefundPayments;
        }

        #endregion

        #region Prepare Checks

        /// <summary>
        /// F1214_s the prepare checks.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <param name="ownerId">The owner ID.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <returns>ErrorId</returns>
        public static int F1214_PrepareChecks(int registerId, int ownerId, DateTime interestDate, int userId, string paymentItems)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@UserID", userId);
            ht.Add("@PaymentItems", paymentItems);
     
           ////return DataProxy.FetchSPOutput("f1214_pcins_RefundPayments", ht);
            return Utility.FetchSPOutput("f1214_pcins_RefundPayments", ht);
        }

        #endregion
    }
}
