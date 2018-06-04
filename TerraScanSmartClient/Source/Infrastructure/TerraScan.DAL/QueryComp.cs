// -------------------------------------------------------------------------------------------
// <copyright file="QueryComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Query related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;
    
    /// <summary>
    /// Query Related Component
    /// </summary>
    public static class QueryComp
    {
        #region Query 
        
        /// <summary>
        /// Lists the query.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Dataset</returns>
        public static DataSet ListQuery(int formId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@Form", formId);
            return DataProxy.FetchDataSet("f9050_pclst_Query", ht);
        }

        /// <summary>
        /// Lists the Sorted query.
        /// </summary>
        /// <param name="savedQueryId">The savedquery Id .</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form Id.</param>
        /// <returns> The dataset containing sorted order of the query result.</returns>
        public static DataSet ListSortQuery(int savedQueryId, string orderByCondition, int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SavedQueryID", savedQueryId);
            ht.Add("@OrderByCondition", orderByCondition);
            ht.Add("@FormID", formId);
            return DataProxy.FetchDataSet("pclst_SortSavedQuery", ht);
        }

        /// <summary>
        ///  method to execute query
        /// </summary>
        /// <param name="whereCondition"> The condition used to execute query</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>The Typed Dataset</returns>
        public static QueryData ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            QueryData queryData = new QueryData();
            Hashtable ht = new Hashtable();
            ht.Add("@WhereCondnSql", whereCondition);
            ht.Add("@Form", formId);
            ht.Add("@OrderByCondn", orderByCondition);
            Utility.LoadDataSet(queryData, "f9050_pcexe_Query", ht, new string[] { "ListKeyId", "SearchedCountResult" });
            return queryData;
        }        

        /// <summary>
        /// Checks the snap shot exist.
        /// </summary>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedQueryName">Name of the saved query.</param>
        /// <returns>
        /// True if query name exist and False if not exist.
        /// </returns>
        public static int CheckQueryExist(int formId, string savedQueryName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            ht.Add("@SavedQueryName", savedQueryName);
           ////return DataProxy.FetchSPOutput("pcchk_Query", ht);
            return Utility.FetchSPOutput("pcchk_Query", ht);
        }
       
        /// <summary>
        /// Gets the snap shot.
        /// </summary>
        /// <param name="queryId">The query Id.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns>All Statement Ids</returns>
        public static QueryData GetQueryResult(int queryId, string orderByCondn)
        {
            QueryData queryData = new QueryData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryID", queryId);
            ht.Add("@OrderByCondn", orderByCondn);
            Utility.LoadDataSet(queryData, "f9050_pcget_Query", ht, new string[] { "GetQueryResult", "ListKeyId", "SearchedCountResult" });
            return queryData;            
        }

        #endregion Query

        #region Snapshot

        /// <summary>
        ///  method to execute snapshot
        /// </summary>
        /// <param name="snapshotId"> The id used to retrieve snapshotitems to which filter applied</param>
        /// <param name="whereCondition">wherecondition used to query snapshotitems</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId"> The form Id</param>
        /// <returns>Typed Dataset</returns>
        public static QueryData ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            QueryData queryData = new QueryData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapshotId);
            ht.Add("@WhereCondn", whereCondition);
            ht.Add("@Form", formId);
            ht.Add("@OrderByCondn", orderByCondition);
            Utility.LoadDataSet(queryData, "f9051_pcexe_Snapshot", ht, new string[] { "ListKeyId", "SearchedCountResult" });
            return queryData;
        }

        /// <summary>
        /// Get the result of the snapshot.
        /// </summary>
        /// <param name="snapshotId"> The snapshot ID of the snapshot to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The Typed datset containing the snapshot results.</returns>
        public static QueryData GetSnapShotResult(int snapshotId, string orderByCondn)
        {
            QueryData queryData = new QueryData();
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotID", snapshotId);
            ht.Add("@OrderByCondn", orderByCondn);
            Utility.LoadDataSet(queryData, "f9051_pcget_Snapshot", ht, new string[] { "GetSnapshotResult", "ListKeyId", "SearchedCountResult" });
            return queryData;
        } 

        /// <summary>
        /// Lists the Sorted Sanpshot.
        /// </summary>
        /// <param name="snapShotId">The snapshot id .</param>
        /// <param name="orderByCondition">The order by condition.</param>
        /// <param name="formId">The form Id.</param>
        /// <returns> The dataset containing sorted order of the snapshot result.</returns>
        public static DataSet ListSortSnapShot(int snapShotId, string orderByCondition, int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@OrderByCondition", orderByCondition);
            ht.Add("@FormID", formId);
            return DataProxy.FetchDataSet("pclst_SortSnapshot", ht);
        }        

        /// <summary>
        /// Checks the snap shot exist.
        /// </summary>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedSnapShotName">Name of the saved snap shot.</param>
        /// <returns>True if snapshot name exist and False if not exist.</returns>
        public static int CheckSnapShotExist(int formId, string savedSnapShotName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            ht.Add("@SnapShotName", savedSnapShotName);
            ////return DataProxy.FetchSPOutput("pcchk_Snapshot", ht);
            return Utility.FetchSPOutput("pcchk_Snapshot", ht);
        }

        #endregion Snapshot    
    }
}
