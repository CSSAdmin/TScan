// -------------------------------------------------------------------------------------------
// <copyright file="F9033QueryEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Dec 06		Vinoth             Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// Main class for QueryEngine Component
    /// </summary>
    public static class F9033QueryEngineComp
    {
        #region ListQueryView

        /// <summary>
        /// Lists the query view.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9033QueryEngineData Dataset</returns>
        public static F9033QueryEngineData ListQueryView(int formId)
        {
            F9033QueryEngineData queryEngineData = new F9033QueryEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            Utility.LoadDataSet(queryEngineData.ListQueryView, "f9033_pclst_QueryEngineView", ht);
            return queryEngineData;
        }

        #endregion

        #region GetDefaultLayout

        /// <summary>
        /// Gets the default layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>F9033QueryEngineData dataset</returns>
        public static F9033QueryEngineData GetDefaultLayout(int queryViewId)
        {
            F9033QueryEngineData getDefaultLayoutData = new F9033QueryEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(getDefaultLayoutData.GetDefaultLayoutXML, "f9038_pcget_DefaultLayoutXML", ht);
            return getDefaultLayoutData;
        }

        #endregion

        #region ListQueryEngine

        /// <summary>
        /// Lists the query engine.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static DataSet ListQueryEngine(int queryViewId)
        {
            DataSet queryEngineData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(queryEngineData, "f9033_pclst_QueryEngine", ht);
            return queryEngineData;
        }

        #endregion

        #region ListQuerySnapShot

        /// <summary>
        /// F9033_s the list query snap shot.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>Typed dataset</returns>
        public static F9033QueryEngineData F9033_ListQuerySnapShot(int queryViewId)
        {
            F9033QueryEngineData listQuerySnapShotData = new F9033QueryEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(listQuerySnapShotData.ListQuerySnapShot, "f9033_pclst_QueryEngineSnapshot", ht);
            return listQuerySnapShotData;
        }

        #endregion

        #region GetSnapShotRecordSet

        /// <summary>
        /// F9033_s the get snap shot record set.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">queryViewId</param>
        /// <returns>Dataset</returns>
        public static DataSet F9033_GetSnapShotRecordSet(int snapShotId, int queryViewId)
        {
            DataSet snapShotData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(snapShotData, "f9040_pclst_SnapShotResult", ht);
            return snapShotData;
        }

        #endregion GetSnapShotRecordSet

        #region ListQueryLayout

        /// <summary>
        /// F9033_s the list query layout.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed dataset</returns>
        public static F9033QueryEngineData F9033_ListQueryLayout(int queryViewId, int userId)
        {
            F9033QueryEngineData listQueryLayoutData = new F9033QueryEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(listQueryLayoutData.ListQueryLayout, "f9038_pclst_LayoutManagement", ht);
            return listQueryLayoutData;
        }

        #endregion

        #region GetSystemSnapShotCount

        /// <summary>
        /// F9033_s the Get SystemSnapShotCount.
        /// </summary>
        /// <param name="systemSnapShotId">systemSnapShotId</param>
        /// <returns>F9033QueryEngineData</returns>
        public static F9033QueryEngineData F9033_GetSystemSnapShotCount(int systemSnapShotId)
        {
            F9033QueryEngineData getSystemSnapshotId = new F9033QueryEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@SysSnapshotID", systemSnapShotId);
            Utility.LoadDataSet(getSystemSnapshotId.GetSystemSnapshotCount, "f9033_pcget_SystemSnapshotCount", ht);
            return getSystemSnapshotId;
        }

        #endregion GetSystemSnapShotCount

        #region InsertSnapShotItems

        /// <summary>
        /// F9030_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotxmlValue">The system snap shot XML.</param>
        /// <returns>Integer value</returns>
        public static int F9033_InsertSnapShotItems(int? userId, string systemSnapShotxmlValue)
        {
            int systemShotId;
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@SystemSnapShotXML", systemSnapShotxmlValue);
            systemShotId = Utility.FetchSPExecuteKeyId("f9033_pcins_SystemSnapShot", ht);
            return systemShotId;
        }

        #endregion InsertSnapShotItems        

        #region GetSystemSnapShotRecordSet


        /// <summary>
        /// F9033_s the get system snap shot record set.
        /// </summary>
        /// <param name="systemSnapShotId">The system snap shot id.</param>
        /// <param name="masterFormNO">The master form NO.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">The key id collection.</param>
        /// <param name="isFilter">The is filter.</param>
        /// <returns>DataSet</returns>
        public static DataSet F9033_GetSystemSnapShotRecordSet(int systemSnapShotId, int masterFormNO, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter)
        {
            DataSet systemSnapShotData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@SysSnapshotID", systemSnapShotId);
            ht.Add("@FormID", masterFormNO);
            ht.Add("@FilterXML", filterValue);
            ht.Add("@OrderXML", sortOrder);
            ht.Add("@SummaryXML", summaryValue);
            ht.Add("@ColumnXML", columnValue);
            ht.Add("@KeyXML", keyIdCollection);
            ht.Add("@Fitler", isFilter);
            Utility.LoadDataSet(systemSnapShotData, "f9033_pclst_SystemSnapshotView", ht);
            return systemSnapShotData;
        }

        #endregion GetSystemSnapShotRecordSet

        ////Added By Latha

        #region CustomGridFunctionality

        #region WithoutSanapshot

        /// <summary>
        /// Lists the query engine grid function.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record Cound</param>
        /// <returns>DataSet</returns>
        public static DataSet ListQueryEngineGridFunction(int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, string isFilter, string maxRecord)
        {
            DataSet queryEngineData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            ht.Add("@FilterXML", filterValue);
            ht.Add("@OrderXML", sortOrder);
            ht.Add("@SummaryXML", summaryValue);
            ht.Add("@ColumnXML", columnValue);
            ht.Add("@KeyXML", keyIdCollection);
            ht.Add("@Fitler", isFilter);
            ht.Add("@RecordCount", maxRecord);
            //ht.Add("@StartIndex", startIndex); 
            Utility.LoadDataSet(queryEngineData, "f9033_pclst_QueryEngineFunction", ht);
            return queryEngineData;
        }
        #endregion WithoutSanapshot

        #region WithSanapshot

        /// <summary>
        /// Lists the query engine grid snapshot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="queryViewId">The query view id.</param>
        /// <param name="filterValue">The filter value.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="summaryValue">The summary value.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="keyIdCollection">Newly added KeyIds</param>
        /// <param name="isFilter">Flag for load all records</param>
        /// <param name="maxRecord">Max Record count</param>
        /// <returns>DataSet</returns>
        public static DataSet ListQueryEngineGridSnapshot(int snapShotId, int queryViewId, string filterValue, string sortOrder, string summaryValue, string columnValue, string keyIdCollection, int isFilter, string maxRecord)
        {
            DataSet queryEngineData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@QueryViewID", queryViewId);
            ht.Add("@FilterXML", filterValue);
            ht.Add("@OrderXML", sortOrder);
            ht.Add("@SummaryXML", summaryValue);
            ht.Add("@ColumnXML", columnValue);
            ht.Add("@KeyXML", keyIdCollection);
            ht.Add("@IsFitler", isFilter);
            ht.Add("@RecordCount", maxRecord);
            Utility.LoadDataSet(queryEngineData, "f9033_pclst_SnapShotFunction", ht);
            return queryEngineData;
        }
        #endregion WithSanapshot

        #region Get Columns

        /// <summary>
        /// Lists the columns.
        /// </summary>
        /// <param name="queryViewId">The query view id.</param>
        /// <returns>DataSet</returns>
        public static DataSet ListColumns(int queryViewId)
        {
            DataSet queryEngineData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryViewID", queryViewId);
            Utility.LoadDataSet(queryEngineData, "f9033_pcget_QueryViewColumns", ht);
            return queryEngineData;
        }

        #endregion Get Columns

        #endregion CustomGridFunctionality
    }
}
