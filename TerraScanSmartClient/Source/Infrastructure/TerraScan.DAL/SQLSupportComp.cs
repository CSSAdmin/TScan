// -------------------------------------------------------------------------------------------
// <copyright file="SQLSupportComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
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
    /// SQLSupportComp class file
    /// </summary>
    public static class SQLSupportComp
    {
        #region ListSQLDescription

        /// <summary>
        /// Get SQLDescription
        /// </summary>
        /// <param name="categoryId">The SQL category.</param>
        /// <returns>SQLSupportData Dataset</returns>
        public static SQLSupportData GetSQLDescription(int categoryId)
        {
            SQLSupportData sqlSupport = new SQLSupportData();
            Hashtable ht = new Hashtable();
            ht.Add("@CategoryID", categoryId);
            Utility.LoadDataSet(sqlSupport.ListSqlDescription, "f9015_pclst_SqlDescription", ht);
            return sqlSupport;
        }

        #endregion

        #region ListSQLCategory

        /// <summary>
        /// Get SQLCategory
        /// </summary>
        /// <returns>SQLSupportData Dataset</returns>
        public static SQLSupportData GetSQLCategory()
        {
            SQLSupportData sqlSupport = new SQLSupportData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(sqlSupport.ListSqlCategory, "f9015_pclst_SqlCategory", ht);
            return sqlSupport;
        }

        #endregion

        #region GetSqlString

        /// <summary>
        /// Gets SqlString
        /// </summary>
        /// <param name="categoryId">Category</param>
        /// <param name="sqlId">Description</param>
        /// <returns>returns the SqlString</returns>
        public static SQLSupportData GetSqlString(int categoryId, int sqlId)
        {
            SQLSupportData sqlSupport = new SQLSupportData();
            Hashtable ht = new Hashtable();
            ht.Add("@CategoryID", categoryId);
            ht.Add("@SQLID", sqlId);
            Utility.LoadDataSet(sqlSupport.GetSqlString, "f9015_pcget_Sql", ht);
            return sqlSupport;
        }

        #endregion

        #region GetSQLQueryResult

        /// <summary>
        /// Gets the query result.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>returns dataset</returns>
        public static DataSet GetSQLQueryResult(string sqlQuery)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryString", sqlQuery);
            return DataProxy.FetchDataSet("f9015_pcexe_Sql", ht);
        }

        #endregion

        #region SaveSQLQuery

        /// <summary>
        /// Save SQLQuery
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="description">Description</param>
        /// <param name="statement">Statement</param>
        /// <param name="moduleId">ModuleID</param>
        /// <param name="userId">UserID</param>
        /// <param name="sqlId">The SQL id.</param>
        /// <returns>SQLSupportData Dataset</returns>
        public static int SaveSQLQuery(int categoryId, string description, string statement, int moduleId, int userId, int sqlId)
        {
            ////SQLSupportData sqlSupport = new SQLSupportData();
            Hashtable ht = new Hashtable();
            ht.Add("@CategoryID", categoryId);
            ht.Add("@Description", description);
            ht.Add("@Statement", statement);
            ht.Add("@ModuleID", moduleId);
            ht.Add("@UserID", userId);
            ht.Add("@SQLID", sqlId);
            return DataProxy.ExecuteSP("f9015_pcins_Sql", ht);  
        }

        #endregion

        #region Delete Query

        /// <summary>
        /// F9015_s the delete query.
        /// </summary>
        /// <param name="sqlId">The SQL id.</param>
        /// <param name="userId">userId</param>
        /// <returns>The integer Value</returns>
        public static int F9015_DeleteQuery(int sqlId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SQLID", sqlId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f9015_pcdel_Sql", ht);
        }

        #endregion
    }
}
