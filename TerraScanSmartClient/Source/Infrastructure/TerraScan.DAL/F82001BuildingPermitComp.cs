// -------------------------------------------------------------------------------------------
// <copyright file="F36035LandValueSliceDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36035LandValueSliceDetailsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 19/09/07         Kuppu             Created
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

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
    /// F82001BuildingPermitComp Class File.
    /// </summary>
    public static class F82001BuildingPermitComp
    {
        #region GetF82001BuildingPermitDetails

        /// <summary>
        /// F82001_s the get building permit details.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns>buildingPermitDetails</returns>
        public static F82001BuildingPermitData F82001_GetBuildingPermitDetails(int eventID)
        {
            F82001BuildingPermitData buildingPermitDetails = new F82001BuildingPermitData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventID);
            Utility.LoadDataSet(buildingPermitDetails.F82001GetBuildingPermitDetails, "f82001_pcget_BuildingPermitDetails", ht);
            return buildingPermitDetails;
        }

        #endregion GetF82001BuildingPermitDetails

        #region InsertBuildingPermitDetails

        /// <summary>
        /// F82001_s the insert building permit details.
        /// </summary>
        /// <param name="permitId">The permit id.</param>
        /// <param name="buildingPermitItems">The building permit items.</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public static int F82001_InsertBuildingPermitDetails(int permitId, string buildingPermitItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (permitId != 0)
            {
                ht.Add("@PermitID", permitId);
            }
            ht.Add("@BuildingPermitItems", buildingPermitItems);
            ht.Add("@UserID",userId);
            return Utility.FetchSPExecuteKeyId("f82001_pcins_BuildingPermitDetails", ht);
        }
        #endregion InsertBuildingPermitDetails
    }
}
