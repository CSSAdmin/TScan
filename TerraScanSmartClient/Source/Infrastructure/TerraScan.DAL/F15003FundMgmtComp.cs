// -------------------------------------------------------------------------------------------
// <copyright file="F15003FundMgmtComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access District Management related information</summary>
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
    /// F15003FundMgmtComp Class
    /// </summary>
    public static class F15003FundMgmtComp
    {
        /// <summary>
        /// F15002_s the get fund sub fund details.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <returns>F15003FundMgmtData with Details</returns>
        public static F15003FundMgmtData F15003_GetFundSubFundDetails(int? fundId)
        {
            F15003FundMgmtData fundMgmtData = new F15003FundMgmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@FundID", fundId);
            string[] tableName = new string[] { fundMgmtData.FundHeader.TableName, fundMgmtData.ListFundAndSubFundItems.TableName, fundMgmtData.ListAvailableSubFundItems.TableName };
            Utility.LoadDataSet(fundMgmtData, "f15003_pcget_FundDetail", ht, tableName);
            return fundMgmtData;
        }

        /// <summary>
        /// F15003_s the list available sub funds.
        /// </summary>
        /// <param name="subFund">The sub fund.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="fundId">The fund id.</param>
        /// <returns>retuns the dataset with AvailableSubFunds Details</returns>
        public static F15003FundMgmtData F15003_ListAvailableSubFunds(string subFund, string description, int? rollYear, int? fundId)
        {
            F15003FundMgmtData fundMgmtData = new F15003FundMgmtData();
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(subFund.Trim()))
            {
                ht.Add("@SubFund", subFund);
            }

            if (!string.IsNullOrEmpty(description.Trim()))
            {
                ht.Add("@Description", description);
            }

            ht.Add("@RollYear", rollYear);
            ht.Add("@FundID", fundId);
            Utility.LoadDataSet(fundMgmtData.ListAvailableSubFundItems, "f15005_pcget_SubfundDescription", ht);
            return fundMgmtData;
        }

        /// <summary>
        /// F15002_s the check district.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns the exist status</returns>
        public static int F15003_CheckFund(int? fundId, string fund, int rollYear)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@FundID", fundId);
            ht.Add("@Fund", fund);
            ht.Add("@RollYear", rollYear);
            errorId = Utility.FetchSPExecuteKeyId("f15003_pcchk_Fund", ht);
            return errorId;
        }

        /// <summary>
        /// F15003_s the create or edit fund MGMT.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="fundGroupId">The fund group id.</param>
        /// <param name="fundItems">The fund items.</param>
        /// <param name="userId">userId</param>
        /// <returns>returns the Insert status</returns>
        public static int F15003_CreateOrEditFundMgmt(int? fundId, string fund, int rollYear, string description, int? fundGroupId, string fundItems, int userId)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@FundID", fundId);
            ht.Add("@Fund", fund);
            ht.Add("@RollYear", rollYear);
            if (!string.IsNullOrEmpty(description.Trim()))
            {
                ht.Add("@Description", description);
            }

            ht.Add("@FundGroupID", fundGroupId);
            ht.Add("@FundItems", fundItems);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyId("f15003_pcins_Fund", ht);
            return errorId;
        }

        /// <summary>
        /// F15003_s the type of the list fund.
        /// </summary>
        /// <returns>dataTable Contains the FundGroup Types</returns>
        public static F15003FundMgmtData.ListFundTypeDataTable F15003_ListFundType()
        {
            F15003FundMgmtData fundMgmtData = new F15003FundMgmtData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(fundMgmtData.ListFundType, "f15003_pclst_FundGroup", ht);
            return fundMgmtData.ListFundType;
        }
    }
}
