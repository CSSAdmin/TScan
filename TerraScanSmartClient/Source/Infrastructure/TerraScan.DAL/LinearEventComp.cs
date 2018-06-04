// -------------------------------------------------------------------------------------------
// <copyright file="LinearEventComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Linear Event</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Sep 06		JYOTHI P	            Created
// 20 Sep 06        JYOTHI P                Added GetLinearEventType method
// 20 Sep 06        JYOTHI P                Added SaveLinearEventType method
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
    /// Main class for LinearEvent Component
    /// </summary>
    public static class LinearEventComp
    {
        #region Get Linear Event Type

        /// <summary>
        /// Gets the Linear Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Linear Event Properties</returns>
        public static LinearEventData GetLinearEventType(int eventId)
        {
            LinearEventData linearEventData = new LinearEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(linearEventData.GetFSLinearEventType, "f8052_pcget_FSLinearEventType", ht);
            return linearEventData;
        }

        #endregion

        #region Save Linear Event Type

        /// <summary>
        /// Save the Linear Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        public static void SaveLinearEventType(string eventItems, int userId)
        {
           ////LinearEventData linearEventData = new LinearEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8052_pcupd_FSLinearEventType", ht);
        }

        #endregion
    }
}
