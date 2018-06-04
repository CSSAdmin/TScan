// -------------------------------------------------------------------------------------------
// <copyright file="F2205MoveScheduleComp.cs" company="Congruent">
//  Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
// **********************************************************************************
// Date              Author             Description
// ----------       ---------         ---------------------------------------------------------
// 11/09/2008       LathaMaheswari.D    Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.DataLayer;

    /// <summary>
    /// F2205 Move Schedule
    /// </summary>
    public static class F2205MoveScheduleComp
    {
        /// <summary>
        /// F2205s the create schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="isNewSchedule">if set to <c>true</c> [is new schedule].</param>
        /// <param name="scheduleHeaderItems">The schedule header items.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Value for confirmation</returns>
        public static int F2205CreateSchedule(int? scheduleId, bool isNewSchedule, string scheduleHeaderItems, string scheduleItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            ht.Add("@IsNewSchedule", isNewSchedule);
            ht.Add("@ScheduleHeaderItems", scheduleHeaderItems);
            ht.Add("@ScheduleLineItems", scheduleItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f2205_pcexe_MoveScheduleItem", ht);
        }
    }
}
