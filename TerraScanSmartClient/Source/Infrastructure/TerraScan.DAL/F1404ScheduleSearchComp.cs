// -------------------------------------------------------------------------------------------
// <copyright file="F1404ScheduleSearchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1404ScheduleSearchComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 09/10/09          Malliga             Created
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
    /// F1404ScheduleSearch Class File.
    /// </summary>
    public static class F1404ScheduleSearchComp
    {

        /// <summary>
        /// F1403_s the type of the get schedule.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns></returns>
        public static F1404ScheduleSelectionData F1404_GetScheduleType(int? scheduleId)
        {
            F1404ScheduleSelectionData scheduleSearchDataSet = new F1404ScheduleSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleID", scheduleId);
            Utility.LoadDataSet(scheduleSearchDataSet.ScheduleRollYearDataTable, "f1404_pclst_ScheduleType", ht);
            return scheduleSearchDataSet;
        }
        
        /// <summary>
        /// F1404_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns>Dataset</returns>
        public static F1404ScheduleSelectionData F1404_ListScheduleSearch(string ScheduleConditionXML)
        {
            F1404ScheduleSelectionData scheduleSearchEngineDetails = new F1404ScheduleSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleConditionXML", ScheduleConditionXML);
            Utility.LoadDataSet(scheduleSearchEngineDetails.GetScheduleSelection, "f1404_pclst_Schedule", ht);
            return scheduleSearchEngineDetails;
        }
        /// <summary>
        /// F1403_s the get Schedule tracking roll year.
        /// </summary>
        /// <param name="eventID">The event ID.</param>
        /// <returns></returns>
        public static F1404ScheduleSelectionData F1404_GetScheduleTrackingRollYear(int eventID)
        {
            F1404ScheduleSelectionData SchedulesearchDataSet = new F1404ScheduleSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventID);
            Utility.LoadDataSet(SchedulesearchDataSet.ScheduleRollYearDataTable, "f29555_pcget_ScheduleSaleTrakingRollYear", ht);
            return SchedulesearchDataSet;
        }
    }
}
