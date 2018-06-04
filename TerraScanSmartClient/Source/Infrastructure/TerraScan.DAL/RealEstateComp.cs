
// -------------------------------------------------------------------------------------------
// <copyright file="RealEstateComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access real estate related information</summary>
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
    /// Real Estate Component
    /// </summary>
    public static class RealEstateComp
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
            ht.Add("@FormID", formId);
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
        /// <returns>Dataset</returns>
        public static DataSet ExecuteQuery(string whereCondition, string orderByCondition, int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@WhereCondnSql", whereCondition);
            ht.Add("@FormID", formId);
            ht.Add("@OrderByCondn", orderByCondition);
            return DataProxy.FetchDataSet("f9050_pcexe_Query", ht);
        }

        /// <summary>
        /// Saves the Query.
        /// </summary>
        /// <param name="savedQueryName">Name of the saved query.</param>
        /// <param name="formId">The form Id.</param>
        /// <param name="savedQueryNote">The saved query note.</param>
        /// <param name="userId">The user Id.</param>
        /// <param name="savedQueryDate">The saved query date.</param>
        /// <param name="savedQuery">The saved query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="canOverride">if set to <c>true</c> [is override].</param>
        /// <returns>DataSet</returns>
        public static DataSet SaveQuery(string savedQueryName, int formId, string savedQueryNote, int userId, DateTime savedQueryDate, string savedQuery, string whereCondn, bool canOverride)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SavedQueryName", savedQueryName);
            ht.Add("@FormID", formId);
            ht.Add("@SavedQueryNote", savedQueryNote);
            ht.Add("@UserID", userId);
            ht.Add("@SavedQueryDate", savedQueryDate);
            ht.Add("@SavedQuery", savedQuery);
            ht.Add("@WhereCondn", whereCondn);
            ht.Add("@IsOverride", canOverride);
            return DataProxy.FetchDataSet("f9050_pcins_Query", ht);
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
        /// Deletes the query.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="userId">userId</param>
        public static void DeleteQuery(int queryId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryID", queryId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9050_pcdel_Query", ht);
        }

        /// <summary>
        /// Gets the snap shot.
        /// </summary>
        /// <param name="queryId">The query Id.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns>All Statement Ids</returns>
        public static DataSet GetQueryResult(int queryId, string orderByCondn)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryID", queryId);
            ht.Add("@OrderByCondn", orderByCondn);
            return DataProxy.FetchDataSet("f9050_pcget_Query", ht);
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
        /// <returns>Dataset</returns>
        public static DataSet ExecuteSnapshot(int snapshotId, string whereCondition, string orderByCondition, int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapshotId);
            ht.Add("@WhereCondn", whereCondition);
            ht.Add("@FormID", formId);
            ht.Add("@OrderByCondn", orderByCondition);
            return DataProxy.FetchDataSet("f9051_pcexe_Snapshot", ht);
        }

        /// <summary>
        /// Get the result of the snapshot.
        /// </summary>
        /// <param name="snapshotId"> The snapshot ID of the snapshot to be exceuted.</param>
        /// <param name="orderByCondn">The orderBy condition.</param>
        /// <returns> The datset containing the snapshot results.</returns>
        public static DataSet GetSnapShotResult(int snapshotId, string orderByCondn)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotID", snapshotId);
            ht.Add("@OrderByCondn", orderByCondn);
            return DataProxy.FetchDataSet("f9051_pcget_Snapshot", ht);
        }

        /// <summary>
        /// Lists the snap shot.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns> The datset containing the list of the snapshots.</returns>
        public static DataSet ListSnapShot(int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            return DataProxy.FetchDataSet("f9051_pclst_Snapshot", ht);
        }

        /// <summary>
        /// Deletes the snap snot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="userId">userId</param>
        public static void DeleteSnapSnot(int snapShotId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotID", snapShotId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9051_pcdel_Snapshot", ht);
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
        /// Saves the snap shot.
        /// </summary>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="formId">The form ID.</param>
        /// <param name="snapshotNote">The snapshot note.</param>
        /// <param name="userId">The user ID.</param>
        /// <param name="snapshotDate">The snapshot date.</param>
        /// <param name="snapshotQuery">The snapshot query.</param>
        /// <param name="whereCondn">The where condn.</param>
        /// <param name="keyIDs">The key I ds.</param>
        /// <param name="canOverride">if set to <c>true</c> [is override].</param>
        /// <returns>first column will return true/false </returns>
        public static DataSet SaveSnapShot(string snapshotName, int formId, string snapshotNote, int userId, DateTime snapshotDate, string snapshotQuery, string whereCondn, string keyIDs, bool canOverride)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotName", snapshotName);
            ht.Add("@FormID", formId);
            ht.Add("@SnapshotNote", snapshotNote);
            ht.Add("@UserID", userId);
            ht.Add("@SnapshotDate", snapshotDate);
            ht.Add("@SnapshotQuery", snapshotQuery);
            ht.Add("@WhereCondn", whereCondn);
            ht.Add("@KeyIDs", keyIDs);
            ht.Add("@IsOverride", canOverride);
            return DataProxy.FetchDataSet("f9051_pcins_Snapshot", ht);
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

        #region Real Estate Statements

       #region Get Real Estate Statement Count

       /// <summary>
       /// Gets the real estate statementcount
       /// </summary>
       /// <returns> The count of statements.</returns>
        public static int GetRealEstateStatementCount()
        {
            Hashtable ht = new Hashtable();
            ////return DataProxy.FetchSPOutput("Pcget_RealEstateStatementCount", ht);
            return Utility.FetchSPOutput("Pcget_RealEstateStatementCount", ht);
        }

        #endregion Get Real Estate Statement Count

        #region Get Real Estate Statement IDs

        /// <summary>
        /// Gets the real estate statement Id's
        /// </summary>
        /// <param name="orderField"> The orderbycolumn name.</param>
        /// <param name="orderBy"> The orderby direction.</param>
        /// <returns> The typed dataset containing the list of real estate statementids.</returns>
        public static RealEstateData GetRealEstateStatementIds(string orderField, string orderBy)
        {
            RealEstateData realEstateData = new RealEstateData();
            Hashtable ht = new Hashtable();
            ht.Add("@OrderField", orderField);
            ht.Add("@Orderby", orderBy);
            Utility.LoadDataSet(realEstateData.ListRealPropertyStatementID, "f1020_pcget_RealPropertyStatementIds", ht);
            return realEstateData;            
        }

        #endregion Get Real Estate Statement IDs

        #region Get Real Estate Statement

        /// <summary>
        /// Gets the real estate statement based on the statement id
        /// </summary>
        /// <param name="statementId"> The statement id of the statement to be fetched.</param>
        /// <returns> The typed dataset containing the statement information of the statementid.</returns>
        public static RealEstateData GetRealEstateStatement(int statementId)
        {
            RealEstateData realEstateData = new RealEstateData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(realEstateData.GetRealPropertyStatementDetails, "f1020_pcget_RealPropertyStatementDetails", ht);
            return realEstateData;           
        }

        #endregion

        #endregion Real Estate Statements
    }
}
