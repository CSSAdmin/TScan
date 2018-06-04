// -------------------------------------------------------------------------------------------
// <copyright file="F1512DistrictSelectionComp.cs" company="Congruent">
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
    /// F1512DistrictSelectionComp class file
    /// </summary>
    public static class F1512DistrictSelectionComp
    {
        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtId">The district ID.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>returns DistrictSelectionData</returns>
        public static F1512DistrictSelectionData F1512_GetDistrictSelectionData(int districtId, string district, string description, int rollYear)
        {
            F1512DistrictSelectionData districtSelectionData = new F1512DistrictSelectionData();
            Hashtable ht = new Hashtable();

            if (districtId != -999)
            {
                ht.Add("@DistrictID", districtId);
            }

            if (!string.IsNullOrEmpty(district))
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

            Utility.LoadDataSet(districtSelectionData.ListDistrictSelection, "f1512_pclst_District", ht);
            return districtSelectionData;
        }
    }
}
