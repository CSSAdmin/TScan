// -------------------------------------------------------------------------------------------
// <copyright file="F11020RealPropertyComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to insert Receipts</summary>
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
    /// Main class for Real Property Component
    /// </summary>
    public static class F11020RealPropertyComp
    {
        #region Get Real Property Statement

        /// <summary>
        /// Gets the real Property statement based on the statement id
        /// </summary>
        /// <param name="statementId"> The statement id of the statement to be fetched.</param>
        /// <returns> The typed dataset containing the statement information of the statementid.</returns>
        public static F11020RealPropertyData F11020_GetRealPropertyStatement(int statementId)
        {
            F11020RealPropertyData realPropertyData = new F11020RealPropertyData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(realPropertyData.GetRealPropertyStatement, "f15021_pcget_RealPropertyStatementSummary", ht);
            return realPropertyData;
        }

        #endregion Get Real Property Statement

        #region Update Real Property

        /// <summary>
        /// update the real property statement.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">userId</param>
        public static void F1423_UpdateRealPropertyStatement(int statementId, string statementItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@StatementItems", statementItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1423_pcupd_RealPropertyStatement", ht);
        }

        #endregion Update Real Property

        #region List Mortgage Name

        /// <summary>
        /// list the mortgage name.
        /// </summary>
        /// <returns>F11020RealPropertyData with morgage name list</returns>
        public static F11020RealPropertyData F1423_ListMortgageName()
        {
            F11020RealPropertyData realPropertyData = new F11020RealPropertyData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(realPropertyData.ListMortgageName, "f1423_pclst_MortgageName", ht);
            return realPropertyData;
        }

        #endregion List Mortgage Name

        #region Get Real Property Statements

        /// <summary>
        /// F15030_GetRealPropertyStatements
        /// </summary>
        /// <param name="statementId">statementId</param>
        /// <returns>Typed Dataset</returns>
        public static F11020RealPropertyData F15030_GetRealPropertyStatements(int statementId)
        {
            F11020RealPropertyData realPropertyDatas = new F11020RealPropertyData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(realPropertyDatas.GetRealPropertyStatementSummarys, "f15021_pcget_RealPropertyStatementSummary", ht);
            return realPropertyDatas;
        }

        #endregion
    }
}
