// -------------------------------------------------------------------------------------------
// <copyright file="F29640FrozenValueComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35100NeighborhoodComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F29640FrozenValueComp class
    /// </summary>
    public static class F29640FrozenValueComp
    {
        #region Get Frozen Details

        /// <summary>
        /// Gets the frozen value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Frozen Details</returns>
        public static F29640FrozenValueData GetFrozenValue(int eventId)
        {
            F29640FrozenValueData frozenDetails = new F29640FrozenValueData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(frozenDetails.GetFrozenValues, "f29640_pcget_FrozenValues", ht);
            return frozenDetails;
        }

        #endregion Get Frozen Details

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        /// <param name="frozenElements">The frozen elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveFrozenDetails(string frozenElements, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FrozenItems", frozenElements);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29640_pcins_FrozenValues", ht);
        }

        #endregion Save Frozen Value

        #region Delete Frozen Value

        /// <summary>
        /// Deletes the frozen details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="frozenId">The frozen id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteFrozenDetails(int eventId, int frozenId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@FrozenID", frozenId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29640_pcdel_FrozenValues", ht);
        }

        #endregion Delete Frozen Value
    }
}
