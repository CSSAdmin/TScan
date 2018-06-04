// -------------------------------------------------------------------------------------------
// <copyright file="F9503SubFundMgmtComp.cs" company="Congruent">
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
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// F9503 SubFund Management Component File
    /// </summary>
    public static class F9503SubFundMgmtComp
    {
        #region List SubFund Details

        /// <summary>
        /// F9503_s the get sub fund management details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <returns>DataSet F9503SubFungMgmtData </returns>
        public static F9503SubFundManagementData F9503_GetSubFundManagementDetails(int? subFundId)
        {
            F9503SubFundManagementData subFundMgmtData = new F9503SubFundManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@SubFundID", subFundId);
            //Added another type datatable for returning configurwed state by purushotham 
            string[] tableName = new string[] {subFundMgmtData.ConfiguredState.TableName, subFundMgmtData.ListSubFundIds.TableName, subFundMgmtData.ListSubFundType.TableName, subFundMgmtData.SubFundDetails.TableName, subFundMgmtData.ListDisbursementHistory.TableName };
            Utility.LoadDataSet(subFundMgmtData, "f9503_pcget_SubFund", ht, tableName);
            return subFundMgmtData;
        }

        #endregion

        #region Get SubFund Items

        /// <summary>
        /// F19503_s the get sub fund items.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>subfund itmes</returns>
        public static F9503SubFundManagementData F9503_GetSubFundItems(string subFund, short rollYear)
        {
            F9503SubFundManagementData subFundMgmtData = new F9503SubFundManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@SubFund", subFund);
            if (rollYear != -1)
            {
                ht.Add("@RollYear", rollYear);
            }
            else
            {
                ht.Add("@RollYear", DBNull.Value);
            }

            Utility.LoadDataSet(subFundMgmtData.getSubFundItems, "f9503_pcget_SubfundDetail", ht);
            return subFundMgmtData;
        }

        #endregion

        #region Save and Edit SubFund Management Data

        /// <summary>
        /// F15005_s the check sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>error id </returns>
        public static int F15005_CheckSubFund(int? subFundId, string subFund, int rollYear)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@SubFundID", subFundId);
            ht.Add("@SubFund", subFund);
            ht.Add("@RollYear", rollYear);
            errorId = Utility.FetchSPExecuteKeyId("f15005_pcchk_SubFund", ht);
            return errorId;
        }

        /// <summary>
        /// F9503_s the create or edit sub fund.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="subFundElments">The sub fund elments.</param>
        /// <param name="userId">userId</param>
        /// <returns>returns primaryId</returns>
        public static int F9503_CreateOrEditSubFund(int? subFundId, string subFundElments, int userId)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@SubFundID", subFundId);
            ht.Add("@SubFundElments", subFundElments);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyId("f9503_pcins_SubFund", ht);
            return errorId;
        }

        #endregion
    }
}
