// -------------------------------------------------------------------------------------------
// <copyright file="DistrictSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access DistrictSelection related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    /// DistrictSelectionComp class file
    /// </summary>
    public static class DistrictSelectionComp
    {
        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public static DistrictSelectionData GetDistrictSelectionData(int districtId, int district, string description, int rollYear)
        {
            DistrictSelectionData districtSelectionData = new DistrictSelectionData();
            Hashtable ht = new Hashtable();

            if (districtId != -999)
            {
                ht.Add("@DistrictID", districtId);
            }

            if (district != -999)
            {
                ht.Add("@District", district);
            }

            if (!string.IsNullOrEmpty(description))
            {
                ht.Add("@Description", description);
            }

            if (rollYear != -999)
            {
                ht.Add("@RollYear", rollYear);
            }

            Utility.LoadDataSet(districtSelectionData.ListDistrictSelection, "f9108_pclst_District", ht);
            return districtSelectionData;
        }

        /// <summary>
        /// Gets the district sub fund Items data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public static DistrictSelectionData GetDistrictData(int districtId)
        {
            DistrictSelectionData districtSelectionData = new DistrictSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            Utility.LoadDataSet(districtSelectionData.ListSubFundItems, "f1024_pclst_SubFunds", ht);
            return districtSelectionData;
        }

        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <param name="LevyOptionId">The LevyOptionId.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="userId">The roll year.</param>
        /// <param name="subfundsXML">The roll year.</param>
        /// <param name="isreplace">Theisreplace.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public static DistrictSelectionData GetDistrictDistributionData(int LevyOptionId, int districtId, decimal amount, int userId, string subfundsXML, bool isreplace)
        {
            DistrictSelectionData districtSelectionData = new DistrictSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@LevyOptionID", LevyOptionId);
            ht.Add("@DistrictID", districtId);
            ht.Add("@Amount", amount);
            ht.Add("@UserID", userId);
            ht.Add("@SubFundsXML", subfundsXML);
            ht.Add("@IsReplace", isreplace);
            Utility.LoadDataSet(districtSelectionData.ListReceiptItem, "f1024_pclst_DistrictDistributionItems", ht);
            return districtSelectionData;
        }

    }
}
