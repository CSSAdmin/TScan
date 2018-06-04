// -------------------------------------------------------------------------------------------
// <copyright file="F11011ExciseStatementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to get Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Jan 07		Ranjani	            Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;    
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Main class for Excise Statement Component
    /// </summary>
    public static class F11011ExciseStatementComp
    {
        #region Get Excise Receipt

        /// <summary>
        /// Gets the Excise Receipt details 
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Receipt Details</returns>
        public static F11011ExciseStatementData F15012_GetExciseReceipt(int statementId)
        {
            F11011ExciseStatementData exciseStatementData = new F11011ExciseStatementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(exciseStatementData, "f15012_pcget_ExciseTaxReceipt", ht, new string[] { exciseStatementData.GetExciseStatement.TableName, exciseStatementData.GetExciseReceipt.TableName });
            return exciseStatementData;
        }

        #endregion

        #region Get Excise Statement Summary

        /// <summary>
        /// Gets the Excise Statement Summary
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Statement Summary Details</returns>
        public static F11011ExciseStatementData F15011_GetExciseStatement(int statementId)
        {
            F11011ExciseStatementData exciseStatementData = new F11011ExciseStatementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(exciseStatementData, "f15011_pcget_ExciseTaxStatement", ht, new string[] { exciseStatementData.GetExciseStatement.TableName, exciseStatementData.GetExciseReceipt.TableName });
            return exciseStatementData;
        }

        /// <summary>
        /// update the Excise Statement - receipt and interest date
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="userId">userId</param>
        public static void F15011_SaveExciseStatement(int statementId, DateTime interestDate, DateTime receiptDate, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@InterestDate", interestDate);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15011_pcupd_ExciseTaxStatement", ht);           
        }

        #endregion
    }
}
