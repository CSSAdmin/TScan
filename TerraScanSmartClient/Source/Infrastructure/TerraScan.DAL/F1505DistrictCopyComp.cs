// -------------------------------------------------------------------------------------------
// <copyright file="F1505DistrictCopyComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access District COPY FORM related information</summary>
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
    /// F1505 District Copy Form Class
    /// </summary>
    public static class F1505DistrictCopyComp
    {
        
        #region  District Copy Form

        /// <summary>
        /// F1505_s the district Copy Form
        /// </summary>
        /// <param name="districtId">The district id.</param>
        /// <param name="districtText">The district details.</param>
        /// <param name="rollyear">The rollyear.</param>
        /// <param name="description">The description.</param>
        /// <param name="isactive">The isactive.</param>
        /// <param name="districtTypeId">The districtTypeId.</param>
        /// <param name="ExciseId">The ExciseRateID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Message</returns>
        public static string F1505ExecuteCopyDistrict(int districtId, string districtText, int rollyear, string description, bool isactive, int districtTypeId, int ExciseId, int userId)
        {
            string  message;
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictID", districtId);
            ht.Add("@NewDistrict", districtText);
            ht.Add("@Rollyear", rollyear);
            ht.Add("@Description", description);
            ht.Add("@IsActive", isactive);
            ht.Add("@DistrictTypeID", districtTypeId);
            ht.Add("@ExciseRateID", ExciseId);
            ht.Add("@UserID", userId);
            message = Utility.MessageKeyString("f15002_pcexe_CopyDistrict", ht);
            return message;
        }

        #endregion

    }
}
