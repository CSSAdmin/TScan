// -------------------------------------------------------------------------------------------
// <copyright file="F15020ReceiptEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Receipts</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 Dec 06		Ranjani	            Created
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
    /// Main class for Receipt Engine Component
    /// </summary>
    public static class F15020ReceiptEngineComp
    {
        #region ListHistoryGrid

        /// <summary>
        /// list history grid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>F15020ReceiptEngineData with receipt history and Detail</returns>
        public static F15020ReceiptEngineData F15020_ListHistoryGrid(int statementId)
        {
            F15020ReceiptEngineData receiptEngineData = new F15020ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(receiptEngineData, "f15020_pclst_ReceiptEngineHistory", ht, new string[] { receiptEngineData.ListHistoryGrid.TableName, receiptEngineData.GetReceiptDetails.TableName, receiptEngineData.PaymentItems.TableName });
            return receiptEngineData;
        }

        #endregion ListHistoryGrid

        #region GetReceiptDetails

        /// <summary>
        /// get receipt details and payment items.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>F15020ReceiptEngineData with receipt detail</returns>
        public static F15020ReceiptEngineData F15020_GetReceiptDetails(int receiptId)
        {
            F15020ReceiptEngineData receiptEngineData = new F15020ReceiptEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(receiptEngineData, "f15020_pcget_Receipt", ht, new string[] { receiptEngineData.GetReceiptDetails.TableName, receiptEngineData.PaymentItems.TableName });
            return receiptEngineData;
        }

        #endregion GetReceiptDetails 
       
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
        public static decimal F1003_GetMinTaxDue(int statmentId, string interestDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statmentId);
            ht.Add("@InterestDate", interestDate);

            tempvalue = Utility.FetchSpObject("f1003_pcget_MinTaxDue", ht);

            if (tempvalue != null && !string.IsNullOrEmpty(tempvalue.ToString()))
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
        /// <param name="taxDueAmount">The tax due amount.</param>
        /// <returns>
        /// The decimal containing the interest information.
        /// </returns>
        public static decimal F1004_GetInterestAmount(int statmentId, string interestDate, decimal taxDueAmount)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatmentID", statmentId);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@TaxAmount", taxDueAmount);
            tempvalue = Utility.FetchSpObject("f1004_pcget_InterestAmount", ht);

            if (tempvalue != null)
            {
                return decimal.Parse(tempvalue.ToString());
            }

            return 0;
        }

        #endregion GetInterestAmount

        #endregion Tax CalCulation for Receipt Engine

        #region GetValidReceiptTest

        /// <summary>
        /// Test for reciept validity
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="receiptDate">The transaction date of the reciept.</param>
        /// <returns>The string containing the recipiet's validity information.</returns>
        public static string F1009_GetValidReceiptTest(int statementId, DateTime receiptDate)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@StatementId", statementId);
            ht.Add("@ReceiptDate", receiptDate);

            tempvalue = Utility.FetchSpObject("f1009_pcget_ValidReceiptTest", ht);

            if (tempvalue != null)
            {
                return tempvalue.ToString();
            }

            return string.Empty;
        }

        #endregion GetValidReceiptTest
    }
}
