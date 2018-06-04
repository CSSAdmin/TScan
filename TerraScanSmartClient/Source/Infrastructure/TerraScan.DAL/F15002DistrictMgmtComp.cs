// -------------------------------------------------------------------------------------------
// <copyright file="F15002DistrictMgmtComp.cs" company="Congruent">
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
    /// F15002 District Management Class
    /// </summary>
    public static class F15002DistrictMgmtComp
    {
        #region Get Distict Fund Details

        /// <summary>
        /// F15002_s the get distirct fund details.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <returns>DataSet Contains the District Fund Deatails</returns>
        public static F15002DistMgmtData F15002_GetDistirctFundDetails(int? districtId)
        {
            F15002DistMgmtData districtMgmtData = new F15002DistMgmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            string[] tableName = new string[] { districtMgmtData.DistrictHeader.TableName, districtMgmtData.ListDistrictFunds.TableName, districtMgmtData.ListAllFunds.TableName };
            Utility.LoadDataSet(districtMgmtData, "f15002_pcget_DistrictFund", ht, tableName);
            return districtMgmtData;
        }

        /// <summary>
        /// F15002_s the list all funds.
        /// </summary>
        /// <param name="fundId">The fund id.</param>
        /// <param name="fund">The fund.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet Contains the All Funds Deatails</returns>
        public static F15002DistMgmtData F15002_ListAllFunds(int? fundId, string fund, int? rollYear)
        {
            F15002DistMgmtData districtMgmtData = new F15002DistMgmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@SubFundID", fundId);
            ht.Add("@SubFund", fund);
            ht.Add("@RollYear", rollYear);
            if (!string.IsNullOrEmpty(fund))
            {
                ////Utility.FillDataSet(districtMgmtData.ListAllFunds, "f15003_pcget_FundDescription", ht);
                //// After Bill Change Request (Fund Changed to Subfund)
                Utility.LoadDataSet(districtMgmtData.ListAllFunds, "f15005_pclst_SubFund", ht);
            }
            else
            {
                Utility.LoadDataSet(districtMgmtData.ListAllFunds, "f15005_pclst_SubFund", ht);
            }

            return districtMgmtData;
        }

        #endregion

        #region Save and Edit District and Fund 

        /// <summary>
        /// F15002_s the check district.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="district">The district.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>the error id or primaryKeyId </returns>
        public static int F15002_CheckDistrict(int? districtId, string district, int rollYear)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            ht.Add("@District", district);
            ht.Add("@RollYear", rollYear);
            errorId = Utility.FetchSPExecuteKeyId("f15002_pcchk_District", ht);
            return errorId;
        }


        /// <summary>
        /// F15002_s the create or edit district MGMT.
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtDetails">The district details.</param>
        /// <param name="districtFundItems">The district fund items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Statement or PrimaryKey Id</returns>
        public static int F15002_CreateOrEditDistrictMgmt(int? districtId, string districtDetails, string districtFundItems, int userId)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            ht.Add("@DistrictDetails", districtDetails);
            ht.Add("@DFItems", districtFundItems);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyId("f15002_pcins_District", ht);
            return errorId;
        }

        #endregion

        #region Get District Details

        /// <summary>
        /// F15002_s the type of the get district.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static F15002DistMgmtData F15002_GetDistrictType(int userId)
        {
            F15002DistMgmtData getDistrictType = new F15002DistMgmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { getDistrictType.F15002_ListDistrictType.TableName, getDistrictType.DistrictVisibility.TableName};
            Utility.LoadDataSet(getDistrictType, "f15002_pclst_DistrictType", ht, tableName);
            return getDistrictType;
        }

        #endregion

    }
}
