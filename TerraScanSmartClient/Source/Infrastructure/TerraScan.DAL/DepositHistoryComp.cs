// -------------------------------------------------------------------------------------------
// <copyright file="DepositHistoryComp.cs" company="Congruent">
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
    /// Deposit History Comp 
    /// </summary>
    public static class DepositHistoryComp
    {
        #region List DepositHistroy Details

        /// <summary>
        /// Gets the PaymentItems Details
        /// </summary>
        /// <returns>The dataset containing the comments.</returns>
        public static DepositHistoryData ListDepositHistoryDetails()
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { depositHistoryData.ListDepositHistoryTable.TableName };
            Utility.LoadDataSet(depositHistoryData, "f1213_pclst_DepositHistory", ht, tableName);
            return depositHistoryData;
        }

        #endregion

        #region Get DepositHistory Serach Results

        /// <summary>
        /// Gets the deposit history search result.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="whereCondnSql">The where condn SQL.</param>
        /// <returns>DepositHistoryDataSet contains the resulted Search</returns>
        public static DepositHistoryData GetDepositHistorySearchResult(int form, string whereCondnSql)
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@WhereCondnSql", whereCondnSql);
            Utility.LoadDataSet(depositHistoryData.ListDepositHistoryTable, "f1213_pcexe_DepositHistory", ht);
            return depositHistoryData;
        }

        #endregion

        #region Update Deposit History

        /// <summary>
        /// Updates the deposit history.
        /// </summary>
        /// <param name="clid">The clid.</param>
        /// <param name="userId">The user id.</param>
        public static void UpdateDepositHistory(int clid, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CLID", clid);
            if (userId != 0)
            {
                ht.Add("@UserID", userId);
            }

            Utility.ImplementProcedure("f1213_pcupd_DepositHistory", ht);
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataset containing the AccountNames.</returns>
        public static DepositHistoryData.ListAccountNameDataTable ListAccountNames()
        {
            DepositHistoryData depositHistoryData = new DepositHistoryData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(depositHistoryData.ListAccountName, "f1213_pclst_AccountName", ht);
            return depositHistoryData.ListAccountName;
        }

        #endregion
    }
}
