// -------------------------------------------------------------------------------------------
// <copyright file="PaymentEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
//  This files provides the various methods to access payment engine related information
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// Thilakraj         26/05/2006         Added ListMethod  
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Payment Engine Component
    /// </summary>
    public static class PaymentEngineComp
    {
        #region List Payment      

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <returns>Complete set of payment details related to current pPaymentId</returns>
        public static PaymentEngineData GetPayment(int ppaymentId)
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@PPaymentID", ppaymentId);
            Utility.LoadDataSet(paymentEngineData.GetPayment, "f1018_pcget_Payment", ht);
            return paymentEngineData;
        }

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id</param>
        /// <returns>Complete set of payment details related to current pPaymentId</returns>
        public static PaymentEngineData GetMultiplePayment(string ppaymentId)
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@PPaymentItems", ppaymentId);
            Utility.LoadDataSet(paymentEngineData.GetPayment, "f1018_pclst_Payment", ht);
            return paymentEngineData;
        }

        #endregion List Payment

        #region Save Payment

        /// <summary>
        /// Saves the payment.
        /// </summary>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Return PPayment ID</returns>
        public static int SavePayment(int ppaymentId, string paymentItems, int userId, int ownerId)
        {
            Hashtable ht = new Hashtable();
            if (ppaymentId > 0)
            {
                ht.Add("@PPaymentID", ppaymentId);
            }

            ht.Add("@PaymentItems", paymentItems);
            ht.Add("@UserID", userId);
            if (ownerId > 0)
            {
                ht.Add("@OwnerID", ownerId);
            }

            ////return DataProxy.FetchSPOutput("f1018_pcins_Payment", ht);
            return Utility.FetchSPOutput("f1018_pcins_Payment", ht);
        }

        #endregion Save Payment

        #region Get Tender Type Configuration

        /// <summary>
        /// Gets the payment.
        /// </summary>
        /// <returns>Tender type configuartion information</returns>
        public static PaymentEngineData GetTenderTypeConfiguration()
        {
            PaymentEngineData paymentEngineData = new PaymentEngineData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(paymentEngineData.GetTenderTypeConfiguration, "f9020_pcget_TenderTypeConfigurationValue", ht);
            return paymentEngineData;            
        }

        #endregion Get Tender Type Configuration


        #region List PayeeDetails

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="payeeId">The payee id.</param>
        /// <returns>Complete set of payee details related to current payeeId</returns>
        public static PaymentEngineData F1019_GetPayeeDetails(int payeeId)
        {
            PaymentEngineData OwnerDetailDataSet = new PaymentEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@PayeeID", payeeId);
            Utility.LoadDataSet(OwnerDetailDataSet.PayeeDetail, "f1019_pcget_PayeeDetails", ht);
            return OwnerDetailDataSet;
        }


        #endregion


    }
}
