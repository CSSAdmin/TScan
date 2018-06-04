// -------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxStatementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 July 06		JYOTHI P	            Created
// 21 July 06       JYOTHI P                Added GetExciseTaxStatement method
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
    /// Main class for Excise Tax Statement Component
    /// </summary>
    public static class ExciseTaxStatementComp
    {
        #region Get Excise Tax Statement

        /// <summary>
        /// Gets the Excise Tax Statement
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Excise Tax Statement Details</returns>
        public static ExciseTaxStatementData GetExciseTaxStatement(int statementId)
        {
            ExciseTaxStatementData exciseTaxStatementData = new ExciseTaxStatementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(exciseTaxStatementData, "f1100_pcget_ExciseTaxReceiptDetails", ht, new string[] { "GetExciseTaxStatement", "GetExciseTaxReceipt" });
            return exciseTaxStatementData;
        }

        #endregion

        #region List Excise Tax Statement

        /// <summary>
        /// Gets the Excise Tax Statement ID
        /// </summary>
        /// <returns>Excise Tax Statement ID</returns>
        public static ExciseTaxStatementData ListExciseTaxStatement()
        {
            ExciseTaxStatementData exicseTaxStatementData = new ExciseTaxStatementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(exicseTaxStatementData.ListExciseTaxStatementID, "f1100_pclst_ExciseTaxStatementID", ht);
            return exicseTaxStatementData;
        }

        #endregion List Excise Tax Statement       

        #region SaveExciseTaxReceipt
        
        /// <summary>
        /// Saves the excise tax receipt.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>typed dataset</returns>
        public static ExciseTaxStatementData SaveExciseTaxReceipt(string statementItems, int userId)
        {
            ExciseTaxStatementData exciseTaxStatementData = new ExciseTaxStatementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementItems", statementItems);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(exciseTaxStatementData.ExciseTaxReceiptResultSet, "f1100_pcins_ExciseTaxReceipt", ht);
            return exciseTaxStatementData;
        }

        #endregion SaveExciseTaxReceipt
    }
}
