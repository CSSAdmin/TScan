// -------------------------------------------------------------------------------------------
// <copyright file="PointEventComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
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
    public static class PointEventComp
    {
        #region Get Point Event Type

        /// <summary>
        /// Gets the Point Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Point Event Properties</returns>
        public static PointEventData GetPointEventType(int eventId)
        {
            PointEventData pointEventData = new PointEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(pointEventData.GetFSLinearEventType, "f8052_pcget_FSLinearEventType", ht);
            return pointEventData;
        }

        #endregion

        #region Save Point Event Type

        /// <summary>
        /// Save the Point Event Type
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        public static void SavePointEventType(string eventItems, int userId)
        {
            ////PointEventData pointEventData = new PointEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8052_pcupd_FSLinearEventType", ht);
        }

        #endregion
    }
}
