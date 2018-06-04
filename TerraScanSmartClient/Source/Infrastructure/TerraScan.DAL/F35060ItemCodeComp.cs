// -------------------------------------------------------------------------------------------
// <copyright file="F35060ItemCodeComp.cs" company="Congruent">
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
    /// F36050 Itemcode
    /// </summary>
    public static class F35060ItemCodeComp
    {
        #region Get Schedule Item Code

        /// <summary>
        /// Gets the schedule item codes.
        /// </summary>
        /// <returns>TypedDataSet contains ScheduleItemCode Details</returns>
        public static F35060ScheduleItemCodeData GetScheduleItemCodes()
        {
            F35060ScheduleItemCodeData scheduleItemCodeTable = new F35060ScheduleItemCodeData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(scheduleItemCodeTable.GetScheduleItemCode, "f35060_pcget_ScheduleItemCode", ht);
            return scheduleItemCodeTable;
        }

        #endregion Get Schedule Item Code

        #region Save Schedule Item Code

        /// <summary>
        /// Saves the schedule item codes.
        /// </summary>
        /// <param name="scheduleCodeElements">The schedule code elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveScheduleItemCodes(string scheduleCodeElements, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleItemCodes", scheduleCodeElements);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35060_pcins_ScheduleItemCode", ht);
        }

        #endregion Save Schedule Item Code

        #region Delete Schedule Item Code

        /// <summary>
        /// Deletes the schedule item codes.
        /// </summary>
        /// <param name="itemCodeId">The item code id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteScheduleItemCodes(string itemCodeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ItemCodeID", itemCodeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35060_pcdel_ScheduleItemCode", ht);
        }

        #endregion Delete Schedule Item Code

    }
}
