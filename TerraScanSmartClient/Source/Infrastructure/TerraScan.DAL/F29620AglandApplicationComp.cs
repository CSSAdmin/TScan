// -------------------------------------------------------------------------------------------
// <copyright file="F29620.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29620AglandApplicationCompComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 6/8/08          Malliga             Created
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
    /// F29620AglandApplicationComp Class File.
    /// </summary>
    public class F29620AglandApplicationComp
    {
        /// <summary>
        /// F29620_s the get agland application details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>The agland application dataset.</returns>
        public static F29620AglandApplicationData F29620_GetAglandApplicationDetails(int eventId)
        {
            F29620AglandApplicationData aglandApplicationDetails = new F29620AglandApplicationData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(aglandApplicationDetails.f29620GetAglandapplication, "f29620_pcget_Aglandapplication", ht);
            return aglandApplicationDetails;
        }

        /// <summary>
        /// F29620_s the save agland application details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The save record status.</returns>
        public static int F29620_SaveAglandApplicationDetails(int eventId, int ownerId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29620_pcins_Aglandapplication", ht);
        }
    }
}
