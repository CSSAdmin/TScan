// -------------------------------------------------------------------------------------------
// <copyright file="F1405ScheduleSearchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1405ScheduleSearchComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 02/11/10          P.Manoj             Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using TerraScan.DataLayer;
    using TerraScan.BusinessEntities;
    using System.Collections; 

    public static class F1405StateSearchComp
    {
        /// <summary>
        /// F1405_s the type of the get state.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        //public static F1405StateSelectionData F1405_GetStateType(int? stateId)
        //{
        //    F1405StateSelectionData stateSearchDataSet = new F1405StateSelectionData();
        //    Hashtable ht = new Hashtable();
        //    ht.Add("@StateID", stateId);
        //    Utility.FillDataSet(stateSearchDataSet.StateRollYearDataTable, "f1405_pclst_StateAssessed", ht);
        //    return stateSearchDataSet;
        //}

        /// <summary>
        /// F1404_s the list schedule search.
        /// </summary>
        /// <param name="StateConditionXML">The state condition XML.</param>
        /// <returns>Dataset</returns>
        public static F1405StateSelectionData F1405_ListStateSearch(string StateConditionXML)
        {
            F1405StateSelectionData stateSearchEngineDetails = new F1405StateSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@StateConditionXML", StateConditionXML);
            Utility.LoadDataSet(stateSearchEngineDetails.GetStateSelection, "f1405_pclst_StateAssessed", ht);
            return stateSearchEngineDetails;
        }
    }
}
