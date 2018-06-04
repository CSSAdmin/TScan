// -------------------------------------------------------------------------------------------
// <copyright file="F1033SpecialDistrictSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
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
    /// F1033SpecialDistrictSelectionComp class file
    /// </summary>
   public static class F1033SpecialDistrictSelectionComp
    {
        #region ListPostTypes
        /// <summary>
        /// List the PostTypes Based On Form
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>
        /// The typed dataset containing the PostType Items
        /// </returns>
        public static F1033SpecialDistrictSelectionData ListPostTypes(int? form)
        {
            F1033SpecialDistrictSelectionData postTypesData = new F1033SpecialDistrictSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            Utility.LoadDataSet(postTypesData.ListPostType, "f1000_pclst_PostType", ht);
            return postTypesData;
        }
        #endregion  ListPostTypes

        #region ListSpecialDistricts
       /// <summary>
       /// List the Special Districts
       /// </summary>
       /// <param name="district">district</param>
       /// <param name="rollYear">rollyear</param>
       /// <param name="description">description</param>
       /// <param name="postTypeId">postTypeID</param>
       /// <returns>The Typed Data Set Contains Listof SpecialDistricts Items</returns>
        public static F1033SpecialDistrictSelectionData ListSpecialDistricts(int? district, int? rollYear, string description, int? postTypeId)
        {
            F1033SpecialDistrictSelectionData specialDistrictsData = new F1033SpecialDistrictSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@District", district);
            ht.Add("@RollYear", rollYear);
            ht.Add("@Description", description);
            ht.Add("@PostTypeID", postTypeId);
            Utility.LoadDataSet(specialDistrictsData.ListSpecialDistrict, "f1033_pclst_DistrictSelection", ht);
            return specialDistrictsData;
        }
        #endregion
    }
}
