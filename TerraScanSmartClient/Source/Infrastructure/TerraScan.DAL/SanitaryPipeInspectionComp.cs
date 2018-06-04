// -------------------------------------------------------------------------------------------
// <copyright file="SanitaryPipeInspectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Sanitary Pipe Inspection</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Sep 06		JYOTHI P	            Created
// 12 Sep 06        JYOTHI P                Added GetExciseTaxStatement method
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
    /// Main class for Sanitary Pipe Inspection Component
    /// </summary>
    public static class SanitaryPipeInspectionComp
    {
        #region Get Event Properties

        /// <summary>
        /// Gets the Event Properties
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Event Properties Details</returns>
        public static SanitaryPipeInspectionData GetEventEngineEventProperties(int eventId)
        {
            SanitaryPipeInspectionData sanitaryPipeInspectionData = new SanitaryPipeInspectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(sanitaryPipeInspectionData.GetEventEngineEventProperties, "f8102_pcget_FSSanPipeInspection", ht);
            return sanitaryPipeInspectionData;
        }

        #endregion

        #region Save Event Properties

        /// <summary>
        /// Save the Event Properties
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        public static void SaveEventEngineEventProperties(string eventItems, int userId)
        {
            ////SanitaryPipeInspectionData sanitaryPipeInspectionData = new SanitaryPipeInspectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8102_pcupd_FSSanPipeInspection", ht);
        }

        #endregion
    }
}
