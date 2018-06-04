// -------------------------------------------------------------------------------------------
// <copyright file="SanitaryPipeInspectionDetailsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Sanitary Pipe Inspection Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 Sep 06		JYOTHI P	            Created
// 12 Sep 06        JYOTHI P                Added ListEventEngineTVDetails method
// 12 Sep 06        JYOTHI P                Added ListEventEngineDetailTypes method
// 12 Sep 06        JYOTHI P                Added SaveEventEngineTVDetails method
// 12 Sep 06        JYOTHI P                Added DeleteEventEngineTVDetails method
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
    /// Main class for Sanitary Pipe Inspection Details Component
    /// </summary>
    public static class SanitaryPipeInspectionDetailsComp
    {
        #region List Event Engine TV Details

        /// <summary>
        /// Lists the EventEngineTVDetails
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains EventEngine TVDetails</returns>
        public static SanitaryPipeInspectionDetailsData ListEventEngineTVDetails(int eventId)
        {
            SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(sanitaryPipeInspectionDetailsData.ListEventEngineTVDetails, "f8104_pclst_FSSanPipeInspectionDetails", ht);
            return sanitaryPipeInspectionDetailsData;
        }

        #endregion

        #region List EventEngine Detail Types

        /// <summary>
        /// Lists the EventEngine DetailTypes
        /// </summary>
        /// <returns>returns dataset contains DetailTypes</returns>
        public static SanitaryPipeInspectionDetailsData ListEventEngineDetailTypes()
        {
            SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(sanitaryPipeInspectionDetailsData.ListEventEngineDetailType, "f8104_pclst_FSSanPipeInspectionDetailsType", ht);
            return sanitaryPipeInspectionDetailsData;
        }

        #endregion

        #region Save EventEngine TV Details

        /// <summary>
        /// Save the Event Engine TV Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        public static void SaveEventEngineTVDetails(string eventItems, int userId)
        {
            ////SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8104_pcins_FSSanPipeInspectionDetails", ht);
        }

        #endregion

        #region Update EventEngine TV Details

        /// <summary>
        /// Updates the Event Engine TV Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        public static void UpdateEventEngineTVDetails(string eventItems, int userId)
        {
            ////SanitaryPipeInspectionDetailsData sanitaryPipeInspectionDetailsData = new SanitaryPipeInspectionDetailsData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8104_pcupd_FSSanPipeInspectionDetails", ht);
        }

        #endregion

        #region Delete EventEngine TV Details

        /// <summary>
        /// Deletes the excise tax rate.
        /// </summary>
        /// <param name="detailId">The detail id.</param>
        /// <param name="userId">userId</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int DeleteEventEngineTVDetails(int detailId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DetailID", detailId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f8104_pcdel_FSSanPipeInspectionDetails", ht);
        }
        #endregion
    }
}
