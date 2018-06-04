//-------------------------------------------------------------------------------------------------
// <copyright file="DataProxy.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//		A class to hold shared STATIC methods for accessing SAL.
// </summary>
//-------------------------------------------------------------------------------------------------
//**********************************************************************************
// Description:	Sql Utility Class
// Author:		Thilak Raj
// Date:		20 Oct 2005
//**********************************************************************************
// Change History
//**********************************************************************************
// Date				Author			Description
// ----------		---------		----------------------------------------------------------
// 20 Oct 2005		Thilak Raj      Written the data access methods which connect the SAL
// 
// 
// 
//*********************************************************************************/

namespace TerraScan.DataLayer
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration.Assemblies;
    using TerraScan.DataLayer;

    ///<summary>
    /// Clas with methods to perform data access.
    ///</summary>
    public sealed class DataProxy
    {
        /// <summary>
        /// static string for storing the connectionstring
        /// </summary>
        private static readonly string tempString = ConfigurationManager.AppSettings["ConnectionString"];

        /// <summary>
        /// constructor
        /// </summary>
        private DataProxy()
        {
        }

        /// <summary>
        /// Fetches the data set.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns>DataSet</returns>
        public static DataSet FetchDataSet(string storedProcedure, IDictionary paramAndValue)
        {
            return SAL.FetchDataSet(storedProcedure, paramAndValue);
        }

        /// <summary>
        /// Fetches the SP output (returntype - object).
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns>the object</returns>
        public static object FetchSpObject(string storedProcedure, IDictionary paramAndValue)
        {
            return SAL.FetchSpObject(storedProcedure, paramAndValue);
        }

        /// <summary>
        /// Fetches the data set.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>DataSet</returns>
        public static DataSet FetchDataSet(string storedProcedure)
        {
            return SAL.FetchDataSet(storedProcedure);
        }

        /// <summary>
        /// Fetches the data table.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns>DataSet</returns>
        public static DataTable FetchDataTable(string storedProcedure, IDictionary paramAndValue)
        {
            return SAL.FetchDataTable(storedProcedure, paramAndValue);
        }

        /// <summary>
        /// Fetches the data table.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <returns>DataSet</returns>
        public static DataTable FetchDataTable(string storedProcedure)
        {
            return SAL.FetchDataTable(storedProcedure);
        }

        /// <summary>
        /// Executes the SP.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns>Number of Row Affected</returns>
        public static int ExecuteSP(string storedProcedure, IDictionary paramAndValue)
        {
            return SAL.LoadProcedureItem(storedProcedure, paramAndValue);
        }

        /// <summary>
        /// Executes the SP.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns>row affected</returns>
        //public static int ExecuteSP(string sqlQuery)
        //{
        //    return SAL.ExcecuteSP(connectionString, sqlQuery);
        //}
    }
}

