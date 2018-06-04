// -------------------------------------------------------------------------------------------
// <copyright file="F1224CheckPrintQueueComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to CheckPrintQueueComp related information</summary>
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
    /// Main Class for the Check Print Queue Component
    /// </summary>
    public static class F1224CheckPrintQueueComp
    {
        #region List AccontNames

        /// <summary>
        /// F1224 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public static F1224CheckPrintQueueData.ListAccountNamesDataTable F1224_AccountNames()
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(checkPrintQueueData.ListAccountNames, "f1213_pclst_AccountName", ht);
            return checkPrintQueueData.ListAccountNames;
        }

        #endregion

        #region List Get Check Number

        /// <summary>
        /// F1224_s the get check number.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <returns>Check Numbers</returns>
        public static F1224CheckPrintQueueData F1224_GetCheckNumber(int registerId)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            string[] tableName = new string[] { checkPrintQueueData.CheckNumberTable.TableName };
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            Utility.LoadDataSet(checkPrintQueueData, "f1224_pcget_CheckNumber", ht, tableName);
            return checkPrintQueueData;
        }

        #endregion

        #region List UnPrinted Checks

        /// <summary>
        /// F1224_s the list un printed checks.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <returns>unprinted checks</returns>
        public static F1224CheckPrintQueueData F1224_ListUnPrintedChecks(int registerId)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            string[] tableName = new string[] { checkPrintQueueData.ListUnPrintedChecksTable.TableName };
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            Utility.LoadDataSet(checkPrintQueueData, "f1224_pclst_CheckPrintQueue", ht, tableName);
            return checkPrintQueueData;
        }

        #endregion

        #region Print Checks

        /// <summary>
        /// F1224_s the create checks.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="startingCheckNumber">The starting check number.</param>
        /// <param name="checkItems">The check items.</param>
        /// <returns>printed Check Numbers</returns>
        public static F1224CheckPrintQueueData F1224_CreateChecks(int registerId, int userId, Int64 startingCheckNumber, string checkItems)
        {
            F1224CheckPrintQueueData checkPrintQueueData = new F1224CheckPrintQueueData();
            string[] tableName = new string[] { checkPrintQueueData.ListCreateChecksTable.TableName };
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            ht.Add("@UserID", userId);
            ht.Add("@StartingCheckNumber", startingCheckNumber);
            ht.Add("@CheckItems", checkItems);
            Utility.LoadDataSet(checkPrintQueueData, "f1224_pcins_CreateCheck", ht, tableName);
            return checkPrintQueueData;
        }

        #endregion
    }
}
