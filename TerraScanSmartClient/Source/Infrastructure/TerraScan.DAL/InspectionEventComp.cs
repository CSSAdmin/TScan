// -------------------------------------------------------------------------------------------
// <copyright file="InspectionEventComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Inspection Event Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Oct 06		JYOTHI P	            Created
// 10 Oct 06        JYOTHI P                Added ListInspectionDetails method
// 10 Oct 06        JYOTHI P                Added SaveInspectionDetails method
// 10 Oct 06        JYOTHI P                Added UpdateInspectionDetails method
// 10 Oct 06        JYOTHI P                Added DeleteInspectionDetails method
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
    /// Main class for Inspection Event Details Component
    /// </summary>
    public static class InspectionEventComp
    {
        #region List Inspection Details

        /// <summary>
        /// Lists the Inspection Details
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>returns dataset contains Inspection Details</returns>
        public static InspectionEventData F8056_ListInspectionDetails(int eventId)
        {
            InspectionEventData inspectionEventData = new InspectionEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { inspectionEventData.ListInspectionDetails.TableName, inspectionEventData.ListInspectionAction.TableName, inspectionEventData.ListInspectionComponent.TableName, inspectionEventData.ListInspectionCondition.TableName };
            Utility.LoadDataSet(inspectionEventData, "f8056_pclst_FS_EventTypeInspection", ht, tableName);
            return inspectionEventData;
        }

        #endregion

        #region Save Inspection Details

        /// <summary>
        /// Save the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// /// <param name="userId">userId</param>
        public static void F8056_SaveInspectionDetails(string eventItems, int userId)
        {
            ////InspectionEventData inspectionEventData = new InspectionEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8056_pcins_FS_EventTypeInspection", ht);
        }

        #endregion

        #region Update Inspection Details

        /// <summary>
        /// Updates the Inspection Details
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// /// <param name="userId">userId</param>
        public static void F8056_UpdateInspectionDetails(string eventItems, int userId)
        {
            ////InspectionEventData inspectionEventData = new InspectionEventData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8056_pcupd_FS_EventTypeInspection", ht);
        }

        #endregion

        #region Delete a Inspection Item

        /// <summary>
        /// Deletes the Inspection Item
        /// </summary>
        /// <param name="inspectionId">The inspection id.</param>
        /// <param name="userId">userId</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F8056_DeleteInspectionDetails(int inspectionId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@InspectionID", inspectionId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f8056_pcdel_FS_EventTypeInspection", ht);
        }
        #endregion
    }
}
