// -------------------------------------------------------------------------------------------
// <copyright file="DisbursementCheckComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Affidavit WorkQueue Inspection</summary>
// Release history
//**********************************************************************************
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
    /// DisbursementCheckComp
    /// </summary>
    public static class DisbursementCheckComp
    {
        /// <summary>
        /// Gets the disbursement check list.
        /// </summary>
        /// <returns>DisbursementCheckStagingData Dataset</returns>
        public static DisbursementCheckStagingData F1211_GetDisbursementCheckList()
        {
            DisbursementCheckStagingData disbursementCheck = new DisbursementCheckStagingData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { disbursementCheck.ListAgencyTable.TableName, disbursementCheck.ListCheckDetailTable.TableName };
            Utility.LoadDataSet(disbursementCheck, "f1211_pclst_DisbursementCheckStaging", ht, tableName);
            Utility.LoadDataSet(disbursementCheck.ListFromAccountTable, "f1213_pclst_AccountName", ht);
            return disbursementCheck;
        }

        /// <summary>
        /// Updates the check staging.
        /// </summary>
        /// <param name="tclId">The TCL id.</param>
        /// <param name="disbursementCheck">The disbursement check.</param>
        /// <param name="checkItems">The check items.</param>
        /// <param name="userId">The userId.</param>
        public static void F1211_UpdateCheckStaging(int tclId, string disbursementCheck, string checkItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TCLID", tclId);
            ht.Add("@DisbursementCheck", disbursementCheck);
            ht.Add("@CheckItems", checkItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1211_pcupd_DisbursementCheckStaging", ht);
        }

        /// <summary>
        /// Updates the agency valid status.
        /// </summary>
        /// <param name="tclIds">The TCL ids.</param>
        /// <param name="validStatus">The valid status.</param>
        /// <param name="userId">The userId.</param>
        public static void F1211_UpdateAgencyValidStatus(string tclIds, int validStatus, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TCLItems", tclIds);
            ht.Add("@IsValid", validStatus);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1211_pcupd_DisbursementCheckStatus", ht);
        }

        /// <summary>
        /// F1211_s the delete check staging.
        /// </summary>
        /// <param name="tclIdDelete">The TCL id delete.</param>
        /// <param name="userId">The userId.</param>
        public static void F1211_DeleteCheckStaging(string tclIdDelete, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CheckItems", tclIdDelete);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1211_pcdel_DisbursementCheckStaging", ht);
        }

        /// <summary>
        /// F1211_s the create checks.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="createChecksTclId">The create checks TCL id.</param>
        /// <returns>Returns Count</returns>
        public static int F1211_CreateChecks(int userId, string createChecksTclId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@TCLItems", createChecksTclId);
            ////return DataProxy.FetchSPOutput("f1211_pcins_DisbursementCheckStaging", ht);
            return Utility.FetchSPOutput("f1211_pcins_DisbursementCheckStaging", ht);
        }
    }
}
