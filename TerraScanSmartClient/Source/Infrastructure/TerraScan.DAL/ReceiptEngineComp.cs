// -------------------------------------------------------------------------------------------
// <copyright file="ReceiptEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access reciept related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    /// Main class for Recipt Engine Component
    /// </summary>
    public static class ReceiptEngineComp
    {
        #region Receipt Engine

        #region ListHistoryGrid

        /// <summary>
        /// Lists the history information of the statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>
        /// The Typed dataset containing the history information of the statementid.
        /// </returns>
        public static ReceiptEngineData ListHistoryGrid(int statementId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(receiptEngineData.ListHistoryGrid, "f1000_pclst_ReceiptEngineHistory", ht);
            return receiptEngineData;
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// Get a cecipt detials.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The Typed dataset containing the details of the reciept.
        /// </returns>
        public static ReceiptEngineData GetReceiptDetails(int receiptId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(receiptEngineData, "f1000_pcget_Receipt", ht, new string[] { "GetReceiptDetails", "PaymentItems" });
            return receiptEngineData;
        }

        #endregion GetReceiptDetails        

        #region ListTenderType

        /// <summary>
        /// Lists the tender type.
        /// </summary>
        /// <param name="allowOverUnder">if set to <c>true</c> [allow over under].</param>
        /// <returns>
        /// The Typed dataset containing the types of tender.
        /// </returns>
        public static ReceiptEngineData ListTenderType(bool allowOverUnder)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@AllowOverUnder", allowOverUnder);
            Utility.LoadDataSet(receiptEngineData.ListTenderType, "f1018_pclst_TenderType", ht);
            return receiptEngineData;
        }

        #endregion ListTenderType

        #region GetValidReceiptTest

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        public static string GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatementId", statementId);
            ht.Add("@ReceiptDate", receiptDate);

            tempvalue = DataProxy.FetchSpObject("f1009_pcget_ValidReceiptTest", ht);

            if (tempvalue != null)
            {
                return tempvalue.ToString();
            }
           
            return string.Empty;
        }

        #endregion GetValidReceiptTest

        #region SaveReceipt

        /// <summary>
        /// Saves the receipt.
        /// </summary>
        /// <param name="receiptItems">The receipt items.</param>
        /// <param name="paymentItems">The payment items.</param>
        /// <param name="userId">userId</param>
        /// <returns>
        /// The return value specifying status of the save action.
        /// </returns>
        public static ReceiptEngineData SaveReceipt(string receiptItems, string paymentItems, int userId)
        {
            ReceiptEngineData receiptEngineData = new ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptItems", receiptItems);
            ht.Add("@PaymentItems", paymentItems);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(receiptEngineData.SaveReceiptResult, "f1000_pcins_Receipt", ht);
            return receiptEngineData;
        }

        #endregion SaveReceipt

        #region Tax CalCulation for Receipt Engine

        #region GetMinTaxDue

        /// <summary>
        /// Gets the minimum tax due amount
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <returns>
        /// The decimal containing minimum tax amount due.
        /// </returns>
        public static decimal GetMinTaxDue(int statmentId, string interestDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statmentId);
            ht.Add("@InterestDate", interestDate);

            tempvalue = DataProxy.FetchSpObject("f1003_pcget_MinTaxDue", ht);

            if (tempvalue != null)
            {
                return (decimal)tempvalue;
            }            

            return 0;
        }

        #endregion GetMinTaxDue

        #region GetInterestAmount        

        /// <summary>
        /// Get the interest amoount.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public static decimal GetInterestAmount(int statmentId, string interestDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statmentId);
            ht.Add("@InterestDate", interestDate);

            tempvalue = DataProxy.FetchSpObject("f1004_pcget_InterestAmount", ht);
           
            if (tempvalue != null)
            {
                return (decimal)tempvalue;
            }

            return 0;       
        }

        #endregion GetInterestAmount

        #endregion Tax CalCulation for Receipt Engine

        #region GetInterestAmount

        /// <summary>
        /// Get the interest amoount.
        /// </summary>
        /// <param name="statmentId">The statment id.</param>
        /// <param name="interestDate">The interest date of the reciept.</param>
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public static decimal GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statmentId);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@TaxAmount", taxDueAmount);
            tempvalue = DataProxy.FetchSpObject("f1004_pcget_InterestAmount", ht);

            if (tempvalue != null)
            {
                return (decimal)tempvalue;
            }

            return 0;  
        }

        #endregion GetInterestAmount

        #endregion Receipt Engine
    }
}