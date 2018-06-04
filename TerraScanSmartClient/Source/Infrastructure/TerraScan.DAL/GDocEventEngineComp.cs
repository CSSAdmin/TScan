// -------------------------------------------------------------------------------------------
// <copyright file="GDocEventEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update CountyConfiguration</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 Sep 06		GUHAN S	            Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    #endregion

    /// <summary>
    /// GDocEventEngineComp
    /// </summary>
    public static class GDocEventEngineComp
    {
        #region ListGDocEventEngine 

        /// <summary>
        /// Gets the County Configuration
        /// </summary>
        /// <param name="featureClassId">The feature class ID.</param>
        /// <returns>
        /// The dataset containing the County Configuration.
        /// </returns>
        public static GDocEventEngineTypeStatusData ListEventTypeStatusDetails(int featureClassId)
        {
            GDocEventEngineTypeStatusData eventEngineTypeStatusData = new GDocEventEngineTypeStatusData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureClassId);
            string[] tableName = new string[] { eventEngineTypeStatusData.ListEventEngineTypeTable.TableName, eventEngineTypeStatusData.ListEventStatusTypeTable.TableName };
            Utility.LoadDataSet(eventEngineTypeStatusData, "f8001_pclst_EventEngineType", ht, tableName);
            return eventEngineTypeStatusData;
        }
        #endregion

        #region Load Event Engine DataGrid

        /// <summary>
        /// Gets the County Configuration
        /// </summary>
        /// <param name="featureClassId">The feature class ID.</param>
        /// <param name="featureId">The feature ID.</param>
        /// <returns>
        /// returns dataset contains EnvetEngine Datas
        /// </returns>
        public static GDocEventEngineData LoadEventEngineData(int featureClassId, int featureId)
        {
            GDocEventEngineData eventEngineData = new GDocEventEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureId);
            ht.Add("@FeatureID", featureClassId);
            string[] tableName = new string[] { eventEngineData.GDocEventEngineDataTable.TableName, eventEngineData.ListValidKeyID.TableName };
            Utility.LoadDataSet(eventEngineData, "f8001_pclst_EventEngine", ht, tableName);
            return eventEngineData;
        }

        #endregion

        #region Load Event Engine Header

        /// <summary>
        /// Loads the event engine data header.
        /// </summary>
        /// <param name="featureClassId">The feature class ID.</param>
        /// <param name="featureId">The feature ID.</param>
        /// <returns>
        /// returns dataset contains EnvetEngine  Header Datas
        /// </returns>
        public static GDocEventEngineData GetEventEngineDataHeader(int featureClassId, int featureId)
        {
            GDocEventEngineData eventEngineData = new GDocEventEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureClassId);
            ht.Add("@FeatureID", featureId);
            string[] tableName = new string[] { eventEngineData.EventEngineHeaderTable.TableName };
            Utility.LoadDataSet(eventEngineData, "f8001_pcget_EventEngineHeader", ht, tableName);
            return eventEngineData;
        }

        #endregion

        #region Active Work Records

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureClassId">The feature class Id.</param>
        /// <returns>
        /// typed dataset containing the WOID,Date,Type and Comments
        /// </returns>
        public static GDocWorkOrderData GetWorkOrderDetails(int featureClassId)
        {
            GDocWorkOrderData workOrderData = new GDocWorkOrderData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureClassId);
            Utility.LoadDataSet(workOrderData.GetWorkOrderDetails, "f8002_pclst_ActiveWorkOrder", ht);
            return workOrderData;
        }

        #endregion Active Work Records

        #region InsertGdocEventEngine

        /// <summary>
        /// InsertGDocEventEngineData
        /// </summary>
        /// <param name="eventEngineInsertData">eventEngineInsertData</param>
        /// <param name="userId">userID</param>
       /// <returns>integer value</returns>
        public static int InsertGDocEventEngineData(string eventEngineInsertData, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventItems", eventEngineInsertData);
            ht.Add("@UserID", userId);
            //// Utility.ExecuteSP("f8001_pcins_EventEngine", ht);
            ////return DataProxy.FetchSPOutput("f8001_pcins_EventEngine", ht);
            return Utility.FetchSPOutput("f8001_pcins_EventEngine", ht);
        }

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="featureId">The featureId.</param>
        /// <returns>the integer value</returns>
        public static int GetGDocEventEngineFeatureClassId(int featureId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", featureId);
            ////ht.Add("@UserID", userID);
            //// Utility.ExecuteSP("f8001_pcins_EventEngine", ht);
            ////return DataProxy.FetchSPOutput("f8001_pcins_EventEngine", ht);
            return Utility.FetchSPExecuteKeyId("f8001_pcget_FeatureClassID", ht);
        }

        #endregion
    }
}
