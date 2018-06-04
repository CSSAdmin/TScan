// -------------------------------------------------------------------------------------------
// <copyright file="F25000ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25000ParcelHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/10/2013       Purushotham.A       Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// 
    /// </summary>
    public static class F2103ExemptionSelectionComp
    {
        /// <summary>
        /// F2103_s the get exemption selection.
        /// </summary>
        /// <param name="exemptionCode">The exemption code.</param>
        /// <param name="description">The description.</param>
        /// <param name="percent">The percent.</param>
        /// <param name="maximum">The maximum.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static F2103ExemptionSelectionData f2103_GetExemptionSelection(string exemptionCode, string description, decimal? percent, decimal? maximum, int? rollYear)
        {
            F2103ExemptionSelectionData exemptionSelectionData = new F2103ExemptionSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExemptionCode", exemptionCode);
            ht.Add("@Description", description);
            ht.Add("@Percent", percent);
            ht.Add("@Maximum", maximum);
            ht.Add("@Rollyear", rollYear);
            Utility.LoadDataSet(exemptionSelectionData.F2103ExemptionSelection, "f2103_pclst_ExemptionSelection", ht);
            return exemptionSelectionData;
        }
    }
}
