// -------------------------------------------------------------------------------------------
// <copyright file="DisbursementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Disbursement related information</summary>
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
    /// Disbursement Comp Class
    /// </summary>
    public static class DisbursementComp
    {
        #region List Disbursement Agency/SubFund Details

        /// <summary>
        /// Gets the disbursement details.
        /// </summary>
        /// <param name="postDate">The post date.</param>
        /// <returns>Disbursement DataSet</returns>
        public static DisbursementData F1210_GetDisbursementDetails(DateTime postDate)
        {
            DisbursementData disbursementData = new DisbursementData();
            Hashtable ht = new Hashtable();
            ht.Add("@PostDate", postDate);
            string[] tableName = new string[] { disbursementData.AgencyListing.TableName, disbursementData.SubFundsList.TableName };
            Utility.LoadDataSet(disbursementData, "f1210_pclst_Disbursement", ht, tableName);
            return disbursementData;
        }

        #endregion

        #region List AccontNames

        /// <summary>
        /// Lists the account names.
        /// </summary>
        /// <returns>The dataTable containing the AccountNames.</returns>
        public static DisbursementData.ListAccountNameDataTable F1210_DisbursementAccountNames()
        {
            DisbursementData disbursementData = new DisbursementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(disbursementData.ListAccountName, "f1213_pclst_AccountName", ht);
            return disbursementData.ListAccountName;
        }

        #endregion

        #region Save Disbursement

        /// <summary>
        /// F1210s the save disbursement.
        /// </summary>
        /// <param name="registerId">The register ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="postDate">The post date.</param>
        /// <param name="agencies">The agencies.</param>
        /// <param name="overrideStatus">The override status.</param>
        /// <returns>bit Value to Override the Checks</returns>
        public static int F1210_SaveDisbursement(int registerId, int userId, DateTime postDate, string agencies, int overrideStatus)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            ht.Add("@UserID", userId);
            ht.Add("@PostDate", postDate);
            ht.Add("@Agencies", agencies);
            ht.Add("@IsOverride", overrideStatus);
            ////return DataProxy.FetchSPOutput("f1210_pcins_Disbursement ", ht);
            return Utility.FetchSPOutput("f1210_pcins_Disbursement ", ht);
        } 

        #endregion
    }
}
