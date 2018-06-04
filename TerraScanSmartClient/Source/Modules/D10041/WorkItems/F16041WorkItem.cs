// -------------------------------------------------------------------------------------------------
// <copyright file="F16041WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D10041
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using TerraScan.Helper;

    public class F16041WorkItem : WorkItem
    {

        /// <summary>
        /// Get District Parcel Details.
        /// </summary>
        /// <returns></returns>
        public F16041ImprovementDistrictParcels GetDistrictParcels(int districtID)
        {
            return WSHelper.GetDistrictParcels(districtID);
        }

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// List district parcels.
        /// </summary>
        /// <param name="parcelval"></param>
        /// <param name="parcelId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        public F16041ImprovementDistrictParcels ListDistrictParcelsDetails(string parcelval, int? parcelId, int? rollYear)
        {
            return WSHelper.ListDistrictParcelsDetails(parcelval, parcelId, rollYear);
        }

         ///<summary>
         ///Save improvement district definition.
         ///</summary>
        public string F16041_SaveDistrictParcels(string districtProperty, int userId)
        {
            return WSHelper.F16041_SaveDistrictParcels(districtProperty, userId);
        }

        /// <summary>
        /// Check parcel Details.
        /// </summary>
        public string CheckParcelDetails(string districtProperty)
        {
            return WSHelper.CheckParcelDetails(districtProperty);
        }


        /// <summary>
        /// F16041 delete district parcel.
        /// </summary>
        /// <param name="specialDistrictRateItemId">The special district rate item id.</param>
        /// <param name="userID">The user Id.</param>
        public string F16041_DeleteDistrictParcels(int workingFileItemId, int userId)
        {
            return WSHelper.F16041_DeleteDistrictParcels(workingFileItemId, userId);
        }
    }
}
