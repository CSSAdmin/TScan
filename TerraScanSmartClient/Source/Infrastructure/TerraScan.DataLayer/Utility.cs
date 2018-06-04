
//-------------------------------------------------------------------------------------------------
// <copyright file="DataProxy.cs" company="Congruent">
//                      Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//                      A class to hold shared STATIC methods for accessing SAL.
// </summary>
//-------------------------------------------------------------------------------------------------
//**********************************************************************************
// Description:    Sql Utility Class
// Author:                       Thilak Raj
// Date:             20 Oct 2005
//**********************************************************************************
// Change History
//**********************************************************************************
// Date                                      Author                           Description
// ----------                       ---------               ----------------------------------------------------------
// 20 Oct 2005               Thilak Raj      Written the data access methods which connect the SAL
// 16 Nov 2006      Guhan           Moved ExcecuteSPOutput,FetchSpObject  From DataProxy and added new method  ExcecuteSPFetchKey
// 
// 15 April 2014   Purushotham     Modified the existing code
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
//using Microsoft.ApplicationBlocks.Data;
using TerraScan.DataLayer;
using System.Windows.Forms;
using System.Text.RegularExpressions;

[assembly: System.CLSCompliant(false)]
namespace TerraScan.DataLayer
{
    /// <summary>
    /// The utility class.
    /// </summary>
    public static class Utility
    {


        /// <summary>
        /// Fills the DataTable's DataSet with data from the stored procedure.
        /// </summary>
        /// <param name="table">The DataTable to fill.</param>
        /// <param name="storedProcedureName">The stored procedure to execute.</param>
        /// <param name="parameterValues">The values to pass to the stored procedure.</param>
        /// <returns>The SqlParameters from the stored procedure.</returns>
        public static IList LoadDataSet(DataTable table, String localProcDetails, IDictionary listParam)
        {
            if (table == null)
            {
                throw new ArgumentNullException("DataTable");
            }
            if (localProcDetails == null)
            {
                throw new ArgumentNullException("localProcDetails");
            }
            CleanupParameters(listParam);
            SqlParameter[] inputParamList = null;

            try
            {
                if (!DataLayerRepository.CheckInputParams(listParam))
                {
                    // Get the parameters corresponding to this sproc
                    inputParamList = GetSpParameterSet(localProcDetails);
                    //SqlHelperParameterCache.GetSpParameterSet(tempStrDetails, localProcDetails);
                    // Populate the parameters with the values supplied
                    AssignSqlParameterValues(inputParamList, listParam);
                    string[] tableNames = { table.TableName };
                    // var tempType = "StoredProcedure";
                    // SqlHelper.FillDataset(connectionString, CommandType.StoredProcedure, storedProcedureName, table.DataSet, tableNames, commandParameters);
                    TerraScan.UtilityWrapper.UtilityWrapper.FillingResultValue(localProcDetails, table.DataSet, tableNames, inputParamList);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return inputParamList;
        }

        /// <summary>
        /// Static constructor for one time class initializations.
        /// </summary>
        static Utility()
        {
            //InitSqlConnectionInfo();
        }

        /// <summary>
        /// Executes this stored procedure with these parameters.
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure to execute.</param>
        /// <param name="parameterValues">The SqlParameters to use.</param>
        /// <returns>List of parameters</returns>
        /// 
        public static void ImplementProcedure(String localProcDetails, IDictionary listParam)
        {
            if (localProcDetails == null)
            {
                throw new ArgumentNullException("localProcDetails");
            }
            CleanupParameters(listParam);
            SqlParameter[] inputParamList = null;

            try
            {
                if (!DataLayerRepository.CheckInputParams(listParam))
                {
                    // Get the parameters corresponding to this sproc
                    inputParamList = GetSpParameterSet(localProcDetails);
                    ////Commented by purushotham and recalled the method from utility itself
                    //SqlHelperParameterCache.GetSpParameterSet(tempStrDetails, localProcDetails);
                    // Populate the parameters with the values supplied
                    AssignSqlParameterValues(inputParamList, listParam);
                    // var tempType = "StoredProcedure";
                    //  SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
                    TerraScan.UtilityWrapper.UtilityWrapper.NonQueryResultMethod(localProcDetails, inputParamList);
                }
            }
            catch (Exception)
            {
                throw ;
                //HandleException(storedProcedureName, e, commandParameters);
            }
            
        }

        /// <summary>
        /// Method to execute query and fetch value returned by stored procedure
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to be executed.</param>
        /// <param name="paramAndValue">The parameters and value for them stored in Dictionary.</param>
        /// <returns>
        /// The return value(int) from the executed query result.
        /// </returns>

        
        public static int FetchSPOutput(string localProcDetails, IDictionary listParam)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParam))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPOutput(localProcDetails, listParam);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Fetches the sp object.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns>The return value(object) from the executed query result.</returns>
       
        public static object FetchSpObject(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSpObject(localProcDetails, listParamItems);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Method to execute query and fetch value returned by stored procedure
        /// added output parameter and it will retrun new added record.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to be executed.</param>
        /// <param name="paramAndValue">The parameters and value for them stored in Dictionary.</param>
        /// <returns>newly added record keyid</returns>
        public static int FetchSPExecuteKeyId(string localProcDetails, IDictionary listParamItems)
        {
            try
            {               
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {

                     return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPKeyId(localProcDetails, listParamItems);
                }
                
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string CustomFetchSPKeyString(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                string tempStrDetails = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
                SqlConnection connection = null;
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = "@Message";
                connection = new SqlConnection(tempStrDetails);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                    // Add the Parameter
                    command.Parameters.Add(param);
                }
                SqlParameter outputParam = new SqlParameter(outPutParameterName, SqlDbType.NVarChar, 1000);
                outputParam.Direction = ParameterDirection.Output;
                // Add the Parameter
                command.Parameters.Add(outputParam);
                command.ExecuteNonQuery();
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
                if (!string.IsNullOrEmpty(outputParam.Value.ToString()))
                {
                    return outputParam.Value.ToString();
                }
                else
                {
                    outputParam.Value="No Message";
                    return outputParam.Value.ToString();
                }
               
            }
            catch (Exception)
            {
                throw;
            }

            
        }
        public static IList SPParameters(string localProcDetails, DataTable dataTable1, IDictionary listParamItems, DataTable dataTable)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSPParameters(localProcDetails, dataTable1, listParamItems, dataTable);
                }
                else
                { 
                    return null;
                }
            }
            catch (Exception)
            {
                throw ;
            }

        }

        public static IList FetchSPOuputParameters(DataTable dataTable, string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPOuputParameters(dataTable, localProcDetails, listParamItems);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Common method to get single output parameter
        /// </summary>
        /// <param name="storedProcedure">Stored Procedure Name</param>
        /// <param name="paramAndValue">Parameter collection with value</param>
        /// <param name="outputParamName">Ouput parameter name</param>
        /// <returns>Output string returned from SP</returns>
        public static string FetchSingleOuputParameter(string localProcDetails, IDictionary listParamItems, string outputParamName)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSingleOuputParameter(localProcDetails, listParamItems, outputParamName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception )
            {
                throw;
            }

        }
       
        /// <summary>
        /// Fetches the SP executed result.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns></returns>
        public static int FetchSPExecutedReturnValue(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPReturnValue(localProcDetails, listParamItems);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception )
            {
                throw;
            }

        }

        /// <summary>
        /// Method to execute query and fetch value returned by stored procedure
        /// added output parameter and it will retrun new added record.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to be executed.</param>
        /// <param name="paramAndValue">The parameters and value for them stored in Dictionary.</param>
        /// <returns>newly added record keyid</returns>
        public static string FetchSPExecuteKeyString(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPKeyString(localProcDetails, listParamItems);
                }
                else
                { 
                    return null;
                }

            }
            catch (Exception )
            {
                throw;
            }

        }

        /// <summary>
        /// Method to execute query and fetch value returned by stored procedure
        /// added output parameter and it will retrun new added record.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to be executed.</param>
        /// <param name="paramAndValue">The parameters and value for them stored in Dictionary.</param>
        /// <returns>newly added record keyid</returns>
        public static string MessageKeyString(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperMessageKeyString(localProcDetails, listParamItems);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }


        /// <summary>
        /// Method to execute query and fetch value returned by stored procedure
        /// added output parameter and it will retrun new added record.
        /// </summary>
        /// <param name="storedProcedure">The name of the stored procedure to be executed.</param>
        /// <param name="paramAndValue">The parameters and value for them stored in Dictionary.</param>
        /// <returns>retruns the string</returns>
        public static string FetchSPExecuteXmlString(string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPXmlString(localProcDetails, listParamItems);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Returns a readable string for a SqlCommand.
        /// </summary>
        /// <param name="commandText">Command text</param>
        /// <param name="commandParameters">List of parameters</param>
        /// <returns>Sql command</returns>
        private static string CreateExecString(string instructionText, SqlParameter[] inputListValues)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("exec ");
            builder.Append(instructionText);
            builder.Append(" ");
            if (!DataLayerRepository.ValidateInputParams(inputListValues))
            {

                foreach (SqlParameter parameter in inputListValues)
                {
                    if (parameter.Value == DBNull.Value)
                    {
                        builder.AppendFormat(parameter.ParameterName);
                        builder.Append("= NULL,");
                        continue;
                    }
                    if (parameter.Direction != ParameterDirection.ReturnValue && parameter.Value != null)
                    {
                        builder.AppendFormat(parameter.ParameterName);

                        switch (parameter.SqlDbType)
                        {

                            case SqlDbType.VarChar:

                            case SqlDbType.Char:

                            case SqlDbType.NVarChar:

                            case SqlDbType.NChar:

                            case SqlDbType.UniqueIdentifier:

                            case SqlDbType.Text:

                            case SqlDbType.NText:

                            case SqlDbType.SmallDateTime:

                            case SqlDbType.DateTime:
                                builder.Append("= '");
                                builder.Append(parameter.Value);
                                builder.Append("', ");
                                break;

                            case SqlDbType.Int:
                                builder.Append("= ");
                                builder.Append(Convert.ToInt32(parameter.Value));
                                builder.Append(", ");
                                break;
                            default:
                                builder.Append("= ");
                                builder.Append(parameter.Value);
                                builder.Append(", ");
                                break;
                        }
                    }
                }
                return builder.ToString(0, builder.Length - 2);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the associated values in the SqlParameters collection.
        /// </summary>
        /// <param name="sqlParameters">List of parameters</param>
        /// <param name="parameterValues">List of values to assign to associated parameters</param>
        private static void AssignSqlParameterValues(IList listInput, IDictionary listParamItems)
        {
            if (listInput != null && listParamItems != null)
            {

                foreach (SqlParameter sqlParameter in listInput)
                {
                    sqlParameter.Value = listParamItems[sqlParameter.ParameterName];

                    // TypeName is set to "database.schema.typename" rather than "schema.typename" as is expected.
                    // So have to remove database name from the TypeName
                    if (sqlParameter.SqlDbType.Equals(SqlDbType.Structured))
                    {
                        string typeName = sqlParameter.TypeName;

                        // Trim off the database name to get schema.typename
                        typeName = typeName.Substring(typeName.IndexOf(".") + 1);

                        // If Microsoft fix this in a future release and only return
                        // schema.typename, we would end up with just the typename (no dot)
                        // So only change the TypeName if we still have a dot in our text
                        if (typeName.Contains("."))
                        {
                            sqlParameter.TypeName = typeName;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Sets parameter values to null when needed.
        /// </summary>
        /// <param name="dictionary">List of parameters</param>
        public static void CleanupParametersToDBNull(IDictionary dictionary)
        {
            IDictionary changes = new ListDictionary();

            foreach (object key in dictionary.Keys)
            {
                object parameterValue = dictionary[key];
                if (parameterValue != null)
                {
                    if (parameterValue is Int32)
                    {
                        if ((Int32)parameterValue == int.MinValue)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                    else if (parameterValue is DateTime)
                    {
                        if ((DateTime)parameterValue == DateTime.MinValue)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                    else if (parameterValue is Decimal)
                    {
                        if ((Decimal)parameterValue == decimal.MinValue)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                    else if (parameterValue is Boolean)
                    {
                        if (Convert.ToBoolean(parameterValue))
                        {
                            changes.Add(key, 1);
                        }
                        else
                        {
                            changes.Add(key, 0);
                        }
                    }
                    else if (parameterValue is string)
                    {
                        if (parameterValue.ToString().Length == 0)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                    else if (parameterValue is System.Guid)
                    {
                        if ((System.Guid)parameterValue == System.Guid.Empty)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                }
            }

            // make the changes
            foreach (object key in changes.Keys)
            {
                dictionary[key] = changes[key];
            }
        }

        /// <summary>
        /// Sets parameter values to null when needed.
        /// </summary>
        /// <param name="dictionary">parameter values</param>
        private static void CleanupParameters(IDictionary dictionary)
        {
            IDictionary changes = new ListDictionary();

            foreach (object key in dictionary.Keys)
            {
                object parameterValue = dictionary[key];
                if (parameterValue != null)
                {
                    if (parameterValue is Int32)
                    {
                        if ((Int32)parameterValue == int.MinValue || (Int32)parameterValue == -999)
                        {
                            changes.Add(key, null);
                        }
                    }
                    else if (parameterValue is DateTime)
                    {
                        if ((DateTime)parameterValue == DateTime.MinValue)
                        {
                            changes.Add(key, null);
                        }
                    }
                    else if (parameterValue is Decimal)
                    {
                        if ((Decimal)parameterValue == decimal.MinValue || (Decimal)parameterValue == -999)
                        {
                            changes.Add(key, null);
                        }
                    }
                    else if (parameterValue is Boolean)
                    {
                        if (Convert.ToBoolean(parameterValue))
                        {
                            changes.Add(key, 1);
                        }
                        else
                        {
                            changes.Add(key, 0);
                        }
                    }
                    else if (parameterValue is string)
                    {
                        if (parameterValue.ToString().Length == 0)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                    else if (parameterValue is System.Guid)
                    {
                        if ((System.Guid)parameterValue == System.Guid.Empty)
                        {
                            changes.Add(key, System.DBNull.Value);
                        }
                    }
                }
            }

            // make the changes
            foreach (object key in changes.Keys)
            {
                dictionary[key] = changes[key];
            }
        }

        /// <summary>
        /// Fills the DataTable's DataSet with data from the stored procedure.
        /// </summary>
        /// <param name="dataSet">The DataSet to fill.</param>
        /// <param name="storedProcedureName">The stored procedure to execute.</param>
        /// <param name="parameterValues">The values to pass to the stored procedure.</param>
        /// <returns>The SqlParameters from the stored procedure.</returns>
        public static void LoadDataSet(DataSet dataSet, String strProcName, IDictionary listParamItems)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException("DataSet");
            }
            if (strProcName == null)
            {
                throw new ArgumentNullException("localProcDetails");
            }
            CleanupParameters(listParamItems);
            SqlParameter[] inputParamList = null;

            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    // Get the parameters corresponding to this sproc
                    inputParamList = GetSpParameterSet(strProcName);
                    ////Commented the Calling method and recalled the mehod from same class
                    //SqlHelperParameterCache.GetSpParameterSet(tempStrDetails, strProcName);
                    // Populate the parameters with the values supplied
                    AssignSqlParameterValues(inputParamList, listParamItems);
                    // var tempType = "StoredProcedure"; //CommandType.StoredProcedure.ToString();
                    //// SqlHelper.FillDataset(connectionString, CommandType.StoredProcedure, storedProcedureName, dataSet, null, commandParameters);
                    TerraScan.UtilityWrapper.UtilityWrapper.FillingResultValue(strProcName, dataSet, null, inputParamList);
                }
            }
            catch (Exception )
            {
                throw ;
            }
            //return (IList)inputParamList;
        }

        /// <summary>
        /// Fills the data set.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <param name="parameterValues">The parameter values.</param>
        /// <param name="tableNames">The table names.</param>
        /// <returns>The SqlParameters from the stored procedure.</returns>
        public static IList LoadDataSet(DataSet dataSet, String strProcName, IDictionary listParamItems, string[] tableNames)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException("DataSet");
            }
            if (strProcName == null)
            {
                throw new ArgumentNullException("localProcDetails");
            }
            CleanupParameters(listParamItems);
            SqlParameter[] inputParamList = null;

            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    // Get the parameters corresponding to this sproc
                    inputParamList = GetSpParameterSet(strProcName);
                    //SqlHelperParameterCache.GetSpParameterSet(tempStrDetails, strProcName);
                    //commandParameters = SqlHelperParameterCache.GetSpParameterSet(DefaultConnectionInfo.ToString(), storedProcedureName);
                    // Populate the parameters with the values supplied
                    AssignSqlParameterValues(inputParamList, listParamItems);
                    //var tempType = "StoredProcedure"; //CommandType.StoredProcedure.ToString();
                    ////  SqlHelper.FillDataset(connectionString, CommandType.StoredProcedure, storedProcedureName, dataSet, tableNames, commandParameters);
                    TerraScan.UtilityWrapper.UtilityWrapper.FillingResultValue(strProcName, dataSet, tableNames, inputParamList);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception )
            {
                throw;
            }
           return (IList)inputParamList;
        }


        /// <summary>
        /// Fetches the SP execute output value.
        /// </summary>
        /// <param name="dataTable">The data table.</param>
        /// <param name="localProcDetails">The local proc details.</param>
        /// <param name="listParamItems">The list param items.</param>
        /// <returns></returns>
        public static IList FetchSPExecuteOutputValue(DataTable dataTable, string localProcDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperFetchSPOutputValue(dataTable, localProcDetails, listParamItems);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception )
            {
                throw ;
            }

        }
        ////Commented due to un used code
        ///// <summary>
        ///// Creates a connection object using the default connection information.
        ///// </summary>
        ///// <returns>New SqlConnection object.</returns>
        //public static SqlConnection GetConnection()
        //{
        //    return GetConnection(defaultConnectionInfo);
        //}

        ///// <summary>
        ///// The SqlConnectionInfo that we are connected with.
        ///// </summary>
        //public static SqlConnectionInfo DefaultConnectionInfo
        //{
        //    get
        //    {
        //        return defaultConnectionInfo;
        //    }

        //    set
        //    {
        //        defaultConnectionInfo = value;
        //    }
        //}

        ////private static readonly string tempStrDetails = ConfigurationManager.AppSettings["ConnectionString"];
        //// private static readonly string statusTime = ConfigurationManager.AppSettings["CommandTimeout"];
        ////    if(ConfigurationManager.AppSettings["CommandTimeout"] !=null && string.IsNullOREmpty(ConfigurationManager.AppSettings["CommandTimeout"]))
        ////{
        ////}
        //// private static readonly int timeOut = ConfigurationManager.AppSettings["CommandTimeout"];  
        ///// <summary>
        ///// Initializes the default connection info object from app settings
        ///// </summary>
        //private static void InitSqlConnectionInfo()
        //{

        //    defaultConnectionInfo = new SqlConnectionInfo();
        //    string[] connectString = tempStrDetails.Split(';');
        //    defaultConnectionInfo.Server = tempStrDetails[0].ToString();
        //    defaultConnectionInfo.Database = tempStrDetails[1].ToString();
        //    defaultConnectionInfo.UserId = tempStrDetails[2].ToString();
        //    defaultConnectionInfo.Password = tempStrDetails[3].ToString();
        //    if (defaultConnectionInfo.UserId == null)
        //    {
        //        defaultConnectionInfo.TrustedConnection = true;
        //    }
        //    else
        //    {
        //        defaultConnectionInfo.TrustedConnection = false;
        //    }

        //}

        ///// <summary>
        ///// Creates a SqlConnectio with the supplied connection info.
        ///// </summary>
        ///// <param name="connectionInfo">SqlConnectionInfo to use to create connection.</param>
        ///// <returns>New SqlConnection object.</returns>
        //public static SqlConnection GetConnection(SqlConnectionInfo strInfo)
        //{
        //    return GetConnectionFromString(strInfo.ToString());
        //}

        ///// <summary>
        ///// Creates a SqlConnectio with the supplied connection info.
        ///// </summary>
        ///// <param name="connectionInfo">Connection string.</param>
        ///// <returns>New SqlConnection object.</returns>
        //public static SqlConnection GetConnectionFromString(string strInfo)
        //{
        //    SqlConnection connection = new SqlConnection(strInfo);

        //    return connection;
        //}

        //private static SqlConnectionInfo defaultConnectionInfo;

     

        /// <summary>
        /// Gets the sp parameter set.
        /// </summary>
        /// <param name="localProcDetails">The local proc details.</param>
        /// <param name="includeReturnValueParameter">if set to <c>true</c> [include return value parameter].</param>
        /// <returns></returns>
        public static SqlParameter[] GetSpParameterSet(string localProcDetails, bool includeReturnValueParameter)
        {
            if (localProcDetails == null || localProcDetails.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
           return TerraScan.UtilityWrapper.UtilityWrapper.GetSpParameterSetInternal(localProcDetails, includeReturnValueParameter);
        }

        /// <summary>
        /// Gets the sp parameter set.
        /// </summary>
        /// <param name="localProcDetails">The local proc details.</param>
        /// <returns></returns>
        public static SqlParameter[] GetSpParameterSet(string localProcDetails)
        {
            return GetSpParameterSet(localProcDetails, false);
        }
    }
}
