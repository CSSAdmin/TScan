// -------------------------------------------------------------------------------------------
// <copyright file="StoppageEventComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Stoppage Event Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11 Oct 06		JAYANTHI	            Created


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
    /// Data Access Layer which talks to the DB directly for F8106 Stoppage Event
    /// </summary>
    public static class StoppageEventComp
    {
        #region List Stoppage Event Details        
        /// <summary>
        /// Gets the Stoppage Event Details as Typed Dataset from DB
        /// </summary>
        /// <param name="eventId">the event id</param>
        /// <returns>Typed Dataset</returns>
        public static StoppageEventData F8016_GetStoppageEventDetails(int eventId)
        {
            StoppageEventData stoppageEventData = new StoppageEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(stoppageEventData.GetEventStoppage, "f8106_pcget_FS_EventTypeStoppage", ht);
            return stoppageEventData;
        }
        #endregion       

        #region Save Stoppage Event Details
        /// <summary>
        /// Saves inot DB the Stoppage Event Details
        /// </summary>
        /// <param name="eventItems">Items to be inserted into the table are passed as an XML of type string</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed DataSet</returns>
        public static StoppageEventData F8016_SaveStoppageEventDetails(string eventItems, int userId)
        {
            StoppageEventData stoppageEventData = new StoppageEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(stoppageEventData.SaveEventStoppage, "f8106_pcupd_FS_EventTypeStoppage", ht);
            return stoppageEventData;            
        }
        #endregion
    }
}
