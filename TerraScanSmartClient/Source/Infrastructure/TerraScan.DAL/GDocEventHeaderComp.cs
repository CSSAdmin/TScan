// -------------------------------------------------------------------------------------------
// <copyright file="GDocEventHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Statement</summary>
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
    /// Main class for GDocEventHeaderComp
    /// </summary>
    public static class GDocEventHeaderComp
    {
        #region GDoc Event Header

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public static GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { gdocEventHeaderData.GetGDocEventHeader.TableName, gdocEventHeaderData.GetMasterFormNo.TableName };
            Utility.LoadDataSet(gdocEventHeaderData, "f8030_pcget_EventHeader", ht, tableName);
            return gdocEventHeaderData;
        }   

        #endregion GetGDocEventHeader

        #region ListGDocEventHeaderStatus

        /// <summary>
        /// Lists the GDoc event header status.
        /// </summary>
        /// <param name="eventId">The event Id.</param>
        /// <returns>Typed status containing Event Engine status.</returns>
        public static GDocEventHeaderData ListGDocEventHeaderStatus(int eventId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(gdocEventHeaderData.ListGDocEventHeaderStatus, "f8030_pclst_EventHeader", ht);
            return gdocEventHeaderData;
        }

        #endregion ListGDocEventHeaderStatus

        #region DeleteGDocEventHeader

        /// <summary>
        /// Deletes the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>        
        /// <param name="childFlag">The child flag.</param>
        /// <param name="userId">userId</param>
        public static void DeleteGDocEventHeader(int eventId, int childFlag, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@Flag", childFlag);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8030_pcdel_EventHeader", ht);             
        }

        #endregion DeleteGDocEventHeader

        #region SaveGDocEventHeader

        /// <summary>
        /// Saves the GDoc event header.
        /// </summary>
        /// <param name="eventItems">The event items.</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed dataset</returns>
        public static GDocEventHeaderData SaveGDocEventHeader(string eventItems, int userId)
        {
            GDocEventHeaderData gdocEventHeaderData = new GDocEventHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventItems);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(gdocEventHeaderData.SaveGDocEventHeader, "f8030_pcupd_EventHeader", ht);
            return gdocEventHeaderData;

            // Utility.ExecuteSP("f8030_pcupd_EventEngineSliceHeader", ht);
        }

        #endregion SaveGDocEventHeader

        #endregion GDoc Event Header
    }
}
