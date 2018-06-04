// -------------------------------------------------------------------------------------------
// <copyright file="GDocWorkOrderEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Get work order engine methods</summary>
// Release history
// **********************************************************************************
// Date               Author            Description
// ----------        ---------        ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// class for GDocWorkOrderEngineComp
    /// </summary>
    public static class GDocWorkOrderEngineComp
    {
        #region GDoc Work Order Engine

        #region GetSystemID

        /// <summary>
        /// Gets the system id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="formNumber">The form number.</param>
        /// <returns>The System Id.</returns>
        public static int GetSystemId(int userId, int formNumber)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@Form", formNumber);
            return Utility.FetchSPExecuteKeyId("f8901_pcget_SystemID", ht);
        }

        #endregion GetSystemID

        #region GetWorkOrderEngine

        /// <summary>
        /// Gets the work order engine.
        /// </summary>
        /// <param name="systemId">The system id.</param>
        /// <param name="isopen">The is open.</param>
        /// <returns>Typed Dataset containing the Work Order Engine Values</returns>
        public static GDocWorkOrderEngineData F8901_GetWorkOrderEngine(int systemId, int isopen)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@SystemID", systemId);
            ht.Add("@IsOpen", isopen);
            Utility.LoadDataSet(gdocWorkOrderEngineData.GetWorkOrderEngine, "f8901_pclst_WorkOrderEngine", ht);
            return gdocWorkOrderEngineData;
        }

        #endregion GetWorkOrderEngine

        #region GetWorkOrderType

        /// <summary>
        /// Gets the type of the work order.
        /// </summary>
        /// <param name="systemId">The system id.</param>        
        /// <returns>Typed Dataset containing the Work Order Type Values</returns>
        public static GDocWorkOrderEngineData F8901_GetWorkOrderType(int systemId)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@SystemID", systemId);
            Utility.LoadDataSet(gdocWorkOrderEngineData.GetWorkOrderType, "f8901_pclst_WorkOrderType", ht);
            return gdocWorkOrderEngineData;
        }

        #endregion GetWorkOrderType

        #region SaveWorkOrderEngine

        /// <summary>
        /// Saves the work order engine.
        /// </summary>
        /// <param name="workOrderItems">The work order items.</param>
        /// <param name="usetId">The user Id</param>
        /// <returns>Typed Dataset containing the Work Order Engine Values</returns>
        public static GDocWorkOrderEngineData F8901_SaveWorkOrderEngine(string workOrderItems, int usetId)
        {
            GDocWorkOrderEngineData gdocWorkOrderEngineData = new GDocWorkOrderEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@WorkOrderItems", workOrderItems);
            ht.Add("@UserID", usetId);
            Utility.LoadDataSet(gdocWorkOrderEngineData.SaveWorkOrderEngine, "f8901_pcins_WorkOrderEngine", ht);
            return gdocWorkOrderEngineData;

            // Utility.ExecuteSP("f8030_pcupd_EventEngineSliceHeader", ht);
        }

        #endregion SaveWorkOrderEngine

        #endregion GDoc Work Order Engine
    }
}
