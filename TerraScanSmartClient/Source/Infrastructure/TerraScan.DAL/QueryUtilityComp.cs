// -------------------------------------------------------------------------------------------
// <copyright file="QueryUtilityComp.cs" company="Congruent">
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
    /// QueryUtilityComp class file
    /// </summary>
    public static class QueryUtilityComp
    {
        /// <summary>
        /// Gets the query utility list.
        /// </summary>
        /// <param name="formId"> The form id  of the form being used.</param>
        /// <returns>return dataset</returns>
        public static QueryUtilityData GetQueryUtilityList(int formId)
        {
            QueryUtilityData queryUtilityData = new QueryUtilityData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            Utility.LoadDataSet(queryUtilityData.ListQuery, "f9050_pclst_Query", ht);
            return queryUtilityData;
            ////return DataProxy.FetchDataSet("f9050_pclst_Query", ht);
        }

        /// <summary>
        /// Deletes the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="userId">userId</param>
        public static void DeleteQueryUtility(int queryId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@QueryID", queryId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9050_pcdel_Query", ht);
        }

        /// <summary>
        /// Inserts the query utility.
        /// </summary>
        /// <param name="queryId">The query id.</param>
        /// <param name="queryName">Name of the query.</param>
        /// <param name="formId">The form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="whereCondition">The where condition.</param>
        /// <param name="userWhereCondition">The user where condition.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <returns>return integer</returns>
        public static int InsertQueryUtility(int queryId, string queryName, int formId, string description, int userId, string whereCondition, string userWhereCondition, int overrideValue)
        {
            Hashtable ht = new Hashtable();
            if (queryId == 0)
            {
                ht.Add("@QueryID", DBNull.Value);
            }
            else
            {
                ht.Add("@QueryID", queryId);
            }

            ht.Add("@QueryName", queryName);
            ht.Add("@Form", formId);
            ht.Add("@Description", description);
            ht.Add("@UserID", userId);
            ht.Add("@WhereCondn", userWhereCondition);
            ht.Add("@WhereCondnSql", whereCondition);
            ht.Add("@IsOverride", overrideValue);
            ////return DataProxy.FetchSPOutput("f9050_pcins_Query", ht);
            return Utility.FetchSPOutput("f9050_pcins_Query", ht);
        }
    }
}