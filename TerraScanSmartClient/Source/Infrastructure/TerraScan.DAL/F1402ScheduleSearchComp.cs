// -------------------------------------------------------------------------------------------
// <copyright file="F1402ScheduleSearchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36041CropComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 14/3/08          Malliga             Created
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
    /// F1402ScheduleSearch Class File.
    /// </summary>
    public static class F1402ScheduleSearchComp
    {

        /// <summary>
        /// F1402_s the list schedule search.
        /// </summary>
        /// <param name="ScheduleConditionXML">The schedule condition XML.</param>
        /// <returns>Dataset</returns>
        public static F1402ScheduleSelectionData F1402_ListScheduleSearch(string ScheduleConditionXML)
        {
            F1402ScheduleSelectionData scheduleSearchEngineDetails = new F1402ScheduleSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ScheduleConditionXML", ScheduleConditionXML);
            Utility.LoadDataSet(scheduleSearchEngineDetails.GetScheduleSelection, "f1402_pclst_Schedule", ht);
            return scheduleSearchEngineDetails;
        }

    }
}
