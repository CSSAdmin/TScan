//// ---------------------------------------------------------------------------------------------------------
//// <copyright file="SQLHelper.cs" company="Congruent">
////     Copyright (c) Congruent Infotech.  All rights reserved.
//// </copyright>
//// <summary>This file contains the implementations of the SqlHelper and SqlHelperParameterCache</summary>
//// <summary>For more information see the Data Access Application Block Implementation Overview. </summary>
//// Release history
//// VERSION	DESCRIPTION



//// -----------------------------------------------------------------------------------------------------------
//namespace TerraScan.DataLayer
//{
//    using System;
//    using System.Data;
//    using System.Xml;
//    using System.Data.SqlClient;
//    using System.Collections;
//    using System.Configuration;
//    using System.Windows.Forms;
//    using TerraScan.WrapperClass;

//    /// <summary>
//    /// The SqlHelper class is intended to encapsulate high performance, scalable best practices for
//    /// common uses of SqlClient
//    /// </summary>
//    public sealed class SqlHelper
//    {
//       // private static readonly string tempString = ConfigurationManager.AppSettings["ConnectionString"];

//        //#region constructor

//        //// Since this class provides only static methods, make the default constructor private to prevent 
//        //// instances from being created with "new SqlHelper()"

//        ///// <summary>
//        ///// Sqlhelper class constructor
//        ///// </summary>
//        //private SqlHelper()
//        //{
//        //}

//        //#endregion constructor

//        //#region enum

//        ///// <summary>
//        ///// This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
//        ///// we can set the appropriate CommandBehavior when calling ExecuteReader()
//        ///// </summary>
//        //private enum SqlConnectionOwnership
//        //{
//        //    /// <summary>
//        //    /// Connection is owned and managed by SqlHelper
//        //    /// </summary>
//        //    Internal,

//        //    /// <summary>
//        //    /// Connection is owned and managed by the caller
//        //    /// </summary>
//        //    External
//        //}
//        //#endregion enum
//        //#region Validation methods
//        ///// <summary>
//        ///// Validates the connection.
//        ///// </summary>
//        ///// <param name="validString">The valid string.</param>
//        ///// <returns></returns>
//        ////private static Boolean validateConnection(string validString)
//        ////{
//        ////    bool isValid = false;
//        ////    if (!string.IsNullOrEmpty(tempString) && !string.IsNullOrEmpty(validString) && (validString.Equals(tempString)))
//        ////    {
//        ////        isValid = true;
//        ////    }
//        ////    return isValid;
//        ////}
//        ///// <summary>
//        ///// Validates for SQL injection.
//        ///// </summary>
//        ///// <param name="userInput">The user input.</param>
//        ///// <returns></returns>
//        //private static Boolean ValidateInputParams(SqlParameter[] userInput)
//        //{
//        //    bool isValid = false;
//        //    string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
//        //                               "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
//        //                               };

//        //    foreach (var item in userInput)
//        //    {
//        //        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
//        //        {
//        //            if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
//        //            {
//        //                if (item.ToString().ToLower().Contains(sqlCheckList[i]))
//        //                {
//        //                    isValid = true;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return isValid;
//        //}

//        ///// <summary>
//        ///// Checks the sclar SQL injection.
//        ///// </summary>
//        ///// <param name="userInput">The user input.</param>
//        ///// <returns></returns>
//        //private static Boolean CheckInputParams(params object[] userInput)
//        //{
//        //    bool isValid = false;
//        //    string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
//        //                               "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
//        //                               };

//        //    foreach (var item in userInput)
//        //    {
//        //        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
//        //        {
//        //            if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
//        //            {
//        //                if (item.ToString().ToLower().Contains(sqlCheckList[i]))
//        //                {
//        //                    isValid = true;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return isValid;
//        //}
//        //#endregion

//        //#region ExecuteNonQuery

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns no resultset and takes no parameters) against the database specified in 
//        ///// the connection string
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders");
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int LocalStringNonQueryMethod(string localStrDetails, string instructionText)
//        ////{
//        ////    //string localStrDetails, CommandType instructionType, string instructionText)
//        ////    // Pass through the call providing null for the set of SqlParameters
//        ////    // return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[])null);
//        ////    return TerraScan.WrapperClass.SqlHelperWraper.NonQueryResultMethod(localStrDetails, instructionText, (SqlParameter[])null);
//        ////}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
//        ///// using the provided parameters
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ///// 
//        //////Commented by purushotham on 03.04.14
//        //////public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connectionString == null || connectionString.Length == 0)
//        //////    {
//        //////        throw new ArgumentNullException("connectionString");
//        //////    }
//        //////    if (validateConnection(connectionString))
//        //////    {
//        //////        // Create & open a SqlConnection, and dispose of it after we are done
//        //////        using (SqlConnection connection = new SqlConnection(connectionString))
//        //////        {
//        //////            connection.Open();

//        //////            // Call the overload that takes a connection in place of the connection string
//        //////            //return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
//        //////        }
//        //////    }
//        //////    else
//        //////    {
//        //////        MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //////        return 0;
//        //////    }
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
//        ///// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(connString, "PublishOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored prcedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int LocalStringNonQueryMethod(string localStrDetails, string localProcDetails, params object[] parameterValues)
//        ////{
//        ////    if (localStrDetails == null || localStrDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("connectionString");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If we receive parameter values, we need to figure out where they go
//        ////    if ((parameterValues != null) && (parameterValues.Length > 0))
//        ////    {
//        ////        if (!CheckInputParams(parameterValues))
//        ////        {
//        ////            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////            SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        ////            // Assign the provided values to these parameters based on parameter order
//        ////            AssignParameterValues(inputParamList, parameterValues);

//        ////            // Call the overload that takes an array of SqlParameters
//        ////            ////return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        ////            return TerraScan.WrapperClass.SqlHelperWraper.NonQueryResultMethod(localStrDetails, localProcDetails, inputParamList);
//        ////        }
//        ////        else
//        ////        {
//        ////            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        ////            return 0;
//        ////        }
//        ////    }
//        ////    else
//        ////    {
//        ////        // Otherwise we can just call the SP without params
//        ////        // return ExecuteNonQuery(localStrDetails,tempType, localProcDetails);
//        ////        return LocalStringNonQueryMethod(localStrDetails, localProcDetails);
//        ////    }
//        ////}

//        ///// <summary>
//        ///// Executes the non query.
//        ///// </summary>
//        ///// <param name="localStrDetails">The local STR details.</param>
//        ///// <param name="instructionType">Type of the instruction.</param>
//        ///// <param name="commandText">The command text.</param>
//        ///// <returns></returns>
//        ////public static int LocalNonQueryMethod(string localStrDetails, string instructionText)
//        ////{
//        ////    // Pass through the call providing null for the set of SqlParameters
//        ////    // return ExecuteNonQuery(connection, commandType, commandText, (SqlParameter[])null);
//        ////    return TerraScan.WrapperClass.SqlHelperWraper.FetchNonQueryResults(localStrDetails, instructionText, (SqlParameter[])null);
//        ////}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(conn, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        //////public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Finally, execute the command
//        //////    int retval = cmd.ExecuteNonQuery();

//        //////    // Detach the SqlParameters from the command object, so they can be used again
//        //////    cmd.Parameters.Clear();

//        //////    if (mustCloseConnection)
//        //////    {
//        //////        connection.Close();
//        //////    }

//        //////    return retval;
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
//        ///// using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(conn, "PublishOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int LocalConnectionNonQueryMethod(string localStrDetails, string localProcDetails, params object[] listParam)
//        ////{
//        ////    if (localStrDetails == null)
//        ////    {
//        ////        throw new ArgumentNullException("connection");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If we receive parameter values, we need to figure out where they go
//        ////    if ((listParam != null) && (listParam.Length > 0))
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        ////        // Assign the provided values to these parameters based on parameter order
//        ////        AssignParameterValues(inputParamList, listParam);

//        ////        // Call the overload that takes an array of SqlParameters
//        ////        //// return ExecuteNonQuery(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        ////        return TerraScan.WrapperClass.SqlHelperWraper.FetchNonQueryResults(localStrDetails, localProcDetails, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        // Otherwise we can just call the SP without params
//        ////        return LocalNonQueryMethod(localStrDetails, localProcDetails);
//        ////    }
//        ////}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders");
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int LocalTransactionNonquery(SqlTransaction transaction, string instructionText)
//        ////{
//        ////    //SqlTransaction transaction, string instructionType, string commandText)
//        ////    // Pass through the call providing null for the set of SqlParameters
//        ////    //// return ExecuteNonQuery(transaction, commandType, commandText, (SqlParameter[])null);
//        ////    return TerraScan.WrapperClass.SqlHelperWraper.PerformNonQueryMethod(transaction, instructionText, (SqlParameter[])null);
//        ////}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        //////public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (transaction == null)
//        //////    {
//        //////        throw new ArgumentNullException("transaction");
//        //////    }

//        //////    if (transaction != null && transaction.Connection == null)
//        //////    {
//        //////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Finally, execute the command
//        //////    int retval = cmd.ExecuteNonQuery();

//        //////    // Detach the SqlParameters from the command object, so they can be used again
//        //////    cmd.Parameters.Clear();
//        //////    return retval;
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified 
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int result = ExecuteNonQuery(conn, trans, "PublishOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int LocalTransactionNonQueryMethod(SqlTransaction transaction, string localProcDetails, params object[] listParam)
//        ////{
//        ////    if (transaction == null)
//        ////    {
//        ////        throw new ArgumentNullException("transaction");
//        ////    }

//        ////    if (transaction != null && transaction.Connection == null)
//        ////    {
//        ////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an proper transaction.", "transaction");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If we receive parameter values, we need to figure out where they go
//        ////    if ((listParam != null) && (listParam.Length > 0))
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        ////        // Assign the provided values to these parameters based on parameter order
//        ////        AssignParameterValues(inputParamList, listParam);

//        ////        // Call the overload that takes an array of SqlParameters
//        ////        ////return ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        ////        return TerraScan.WrapperClass.SqlHelperWraper.PerformNonQueryMethod(transaction, localProcDetails, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        // Otherwise we can just call the SP without params
//        ////        return LocalTransactionNonquery(transaction, localProcDetails);
//        ////    }
//        ////}
//        //#endregion ExecuteNonQuery

//        //#region ExecuteDataset



//        ///// <summary>
//        ///// Resultings the dataset method.
//        ///// </summary>
//        ///// <param name="localStrDetails">The local STR details.</param>
//        ///// <param name="instructionType">Type of the instruction.</param>
//        ///// <param name="instructionText">The instruction text.</param>
//        ///// <returns></returns>
//        //public static DataSet ResultingDatasetMethod(string localStrDetails, string instructionType, string instructionText)
//        //{
//        //    //string localStrDetails, string instructionType, string instructionText)
//        //    if (validateConnection(localStrDetails))
//        //    {
//        //        // Pass through the call providing null for the set of SqlParameters
//        //        ////  return ExecuteDataset(localStrDetails, instructionType, commandText, (SqlParameter[])null);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.ImplementResultSetMethod(localStrDetails, instructionType, instructionText, (SqlParameter[])null);
//        //    }
//        //    else
//        //    {
//        //        MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //        return null;
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <param name="localStrDetails">The local STR details.</param>
//        ///// <param name="localProcDetails">The local proc details.</param>
//        ///// <param name="parameterValues">The parameter values.</param>
//        ///// <returns>
//        ///// A dataset containing the resultset generated by the command
//        ///// </returns>
//        ///// <remarks>
//        ///// e.g.:
//        ///// DataSet ds = ExecuteDataset(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        //////public static DataSet ExecuteDataset(string localStrDetails, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (localStrDetails == null || localStrDetails.Length == 0)
//        //////    {
//        //////        throw new ArgumentNullException("connectionString");
//        //////    }

//        //////    // Create & open a SqlConnection, and dispose of it after we are done
//        //////    using (SqlConnection connection = new SqlConnection(localStrDetails))
//        //////    {
//        //////        connection.Open();

//        //////        // Call the overload that takes a connection in place of the connection string
//        //////        return ExecuteDataset(connection, commandType, commandText, commandParameters);
//        //////        //return TerraScan.WrapperClass.SqlHelperWraper.PerformResultSetData(connection, commandType, commandText, commandParameters);
//        //////    }
//        //////}


//        //public static DataSet PerformLocalExecuteDataSet(string localStrDetails, string localProcDetails, params object[] listObjvalue)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listObjvalue != null) && (listObjvalue.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listObjvalue);

//        //        // Call the overload that takes an array of SqlParameters
//        //        ////return ExecuteDataset(localStrDetails, CommandType.StoredProcedure, storedProcedureName, commandParameters);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.ImplementResultSetMethod(localStrDetails, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return ResultingDatasetMethod(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}


//        ///// <summary>
//        ///// Executes the dataset.
//        ///// </summary>
//        ///// <param name="connection">The connection.</param>
//        ///// <param name="instructionType">Type of the instruction.</param>
//        ///// <param name="instructionText">The instruction text.</param>
//        ///// <returns></returns>
//        //public static DataSet ReturnSampleDataSet(string connection, string instructionType, string instructionText)
//        //{
//        //    // SqlConnection connection, string instructionType, string instructionText)
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    //// return ExecuteDataset(connection, commandType, commandText, (SqlParameter[])null);
//        //    return TerraScan.WrapperClass.SqlHelperWraper.PerformResultSetData(connection, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  DataSet ds = ExecuteDataset(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        ///// 
//        //////Commented by purushotham on .3.04.14
//        //////public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Create the DataAdapter & DataSet
//        //////    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//        //////    {
//        //////        DataSet ds = new DataSet();

//        //////        // Fill the DataSet using default values for DataTable names, etc
//        //////        da.Fill(ds);

//        //////        // Detach the SqlParameters from the command object, so they can be used again
//        //////        cmd.Parameters.Clear();

//        //////        if (mustCloseConnection)
//        //////        {
//        //////            connection.Close();
//        //////        }

//        //////        // Return the dataset
//        //////        return ds;
//        //////    }
//        //////}


//        //public static DataSet PerformConnectionExecuteDataSet(string connection, string localProcDetails, params object[] listObjvalue)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listObjvalue != null) && (listObjvalue.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listObjvalue);

//        //        // Call the overload that takes an array of SqlParameters
//        //        //// return ExecuteDataset(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.PerformResultSetData(connection, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        // return ExecuteDataset(connection,tempType, localProcDetails);
//        //        return ReturnSampleDataSet(connection, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        //public static DataSet RetrnTransactionDataSet(SqlTransaction transaction, string instructionType, string instructionText)
//        //{
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    ////return ExecuteDataset(transaction, instructionType, instructionText, (SqlParameter[])null);

//        //    return TerraScan.WrapperClass.SqlHelperWraper.PerformDatasetMethod(transaction, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        ///// 
//        //////Commented by purushotham on .5.04.14
//        //////public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType instructionType, string instructionText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (transaction == null)
//        //////    {
//        //////        throw new ArgumentNullException("transaction");
//        //////    }

//        //////    if (transaction != null && transaction.Connection == null)
//        //////    {
//        //////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, transaction.Connection, transaction, instructionType, instructionText, commandParameters, out mustCloseConnection);

//        //////    // Create the DataAdapter & DataSet
//        //////    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//        //////    {
//        //////        DataSet ds = new DataSet();

//        //////        // Fill the DataSet using default values for DataTable names, etc
//        //////        da.Fill(ds);

//        //////        // Detach the SqlParameters from the command object, so they can be used again
//        //////        cmd.Parameters.Clear();

//        //////        // Return the dataset
//        //////        return ds;
//        //////    }
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  DataSet ds = ExecuteDataset(trans, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        //public static DataSet PerformTransactionExecuteDataSet(SqlTransaction transaction, string localProcDetails, params object[] listObjvalue)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listObjvalue != null) && (listObjvalue.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listObjvalue);

//        //        // Call the overload that takes an array of SqlParameters
//        //        ////return ExecuteDataset(transaction, CommandType.StoredProcedure, localProcDetails, inputParamList);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.PerformDatasetMethod(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        //return ExecuteDataset(transaction, tempType, localProcDetails);
//        //        return RetrnTransactionDataSet(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion ExecuteDataset

//        // Remove this region for now -  code can be commented
//        //#region ExecuteReader

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
//        ///// the connection string. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader PerformLocalDataReader(string localStrDetails, string instructionType, string instructionText)
//        //{
//        //    if (validateConnection(localStrDetails))
//        //    {
//        //        // Pass through the call providing null for the set of SqlParameters
//        //        return ImplementLocalDataReader(localStrDetails, instructionType, instructionText, (SqlParameter[])null);
//        //    }
//        //    else
//        //    {
//        //        MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //        return null;
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ImplementLocalDataReader(string localStrDetails, string instructionType, string instructionText, params SqlParameter[] inputParamList)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    SqlConnection connection = null;

//        //    try
//        //    {
//        //        //connection = new SqlConnection(localStrDetails);
//        //        //connection.Open();

//        //        // Call the private overload that takes an internally owned connection in place of the connection string
//        //       // return ExecuteReader(connection, null, instructionType, instructionText, inputParamList, SqlConnectionOwnership.Internal);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.OutSideConnectionDataReader(localStrDetails, null, instructionType, instructionText, inputParamList, (TerraScan.WrapperClass.SqlHelperWraper.SqlConnectionOwnership.Internal));
//        //    }
//        //    catch
//        //    {
//        //        // If we fail to return the SqlDatReader, we need to close the connection ourselves
//        //        if (connection != null)
//        //        {
//        //            connection.Close();
//        //        }

//        //        throw;
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
//        ///// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(connString, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader LocalDataReaderMethod(string localStrDetails, string localProcDetails, params object[] listObjvalue)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listObjvalue != null) && (listObjvalue.Length > 0))
//        //    {
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);
//        //        AssignParameterValues(inputParamList, listObjvalue);
//        //        //return ExecuteReader(localStrDetails, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //        return ImplementLocalDataReader(localStrDetails, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //       // return ExecuteReader(localStrDetails, CommandType.StoredProcedure, localProcDetails);
//        //        return PerformLocalDataReader(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader PerformConnectionDataReader(string connection, string instructionType, string instructionText)
//        //{
//        //    // Pass through the call providing null for the set of SqlParameters
//        //   // return ExecuteReader(connection, instructionType, instructionText, (SqlParameter[])null);
//        //    return ImplementConnectionDataReader(connection, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ImplementConnectionDataReader(string connection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
//        //{
//        //    // Pass through the call to the private overload using a null transaction value and an externally owned connection
//        //   // return ExecuteReader(connection, (SqlTransaction)null, instructionType, instructionText, inputParamList, SqlConnectionOwnership.External);
//        //    return TerraScan.WrapperClass.SqlHelperWraper.OutSideConnectionDataReader(connection, (SqlTransaction)null, instructionType, instructionText, inputParamList, (TerraScan.WrapperClass.SqlHelperWraper.SqlConnectionOwnership.External));
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(conn, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ConnectionDataReaderMethod(string connection, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();

//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);
//        //        AssignParameterValues(inputParamList, listParamItems);
//        //        return ImplementConnectionDataReader(connection, tempType, localProcDetails, inputParamList);
//        //        //return ExecuteReader(connection, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return PerformConnectionDataReader(connection,tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader PerformTransactionDataReader(SqlTransaction transaction, string instructionType, string instructionText)
//        //{
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    return ImplementTransactionDataReader(transaction, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////   SqlDataReader dr = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ImplementTransactionDataReader(SqlTransaction transaction, string instructionType, string instructionText, params SqlParameter[] inputParamList)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }
//        //    var localStr = transaction.Connection.ConnectionString;
//        //    // Pass through to private overload, indicating that the connection is owned by the caller
//        //    //return ExecuteReader(transaction.Connection, transaction, instructionType, instructionText, inputParamList, SqlConnectionOwnership.External);
//        //    return TerraScan.WrapperClass.SqlHelperWraper.OutSideConnectionDataReader(localStr, transaction, instructionType, instructionText, inputParamList, (TerraScan.WrapperClass.SqlHelperWraper.SqlConnectionOwnership.External));
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  SqlDataReader dr = ExecuteReader(trans, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader TransactionDataReaderMethod(SqlTransaction transaction, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();

//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);
//        //        AssignParameterValues(inputParamList, listParamItems);
//        //        //return ExecuteReader(transaction, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //        return ImplementTransactionDataReader(transaction,tempType, localProcDetails, inputParamList);
//        //    } 
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //       // return ExecuteReader(transaction, CommandType.StoredProcedure, localProcDetails);
//        //        return PerformTransactionDataReader(transaction,tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion ExecuteReader

//        //#region ExecuteScalar

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in 
//        ///// the connection string. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount");
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object PerformLocalScalarValue(string localStrDetails, string instructionType, string instructionText)
//        //{

//        //    //string localStrDetails, string instructionType, string instructionText)
//        //    if (validateConnection(localStrDetails))
//        //    {
//        //        // Pass through the call providing null for the set of SqlParameters
//        //       // return ExecuteScalar(connectionString, commandType, commandText, (SqlParameter[])null);
//        //        return ImplementLocalScalarValue(localStrDetails, instructionType, instructionText, (SqlParameter[])null);
//        //    }
//        //    else
//        //    {
//        //        MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //        return null;
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object ImplementLocalScalarValue(string localStrDetails, string instructionType, string instructionText, params SqlParameter[] inputParamList)
//        //{
//        //    //string localStrDetails, string instructionType, string instructionText, params SqlParameter[] commandParameters)
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    // Create & open a SqlConnection, and dispose of it after we are done
//        //    //using (SqlConnection connection = new SqlConnection(localStrDetails))
//        //    //{
//        //       // connection.Open();
//        //        if (!CheckInputParams(inputParamList))
//        //        {
//        //            // Call the overload that takes a connection in place of the connection string
//        //           // return ExecuteScalar(connection, commandType, commandText, commandParameters);
//        //            return TerraScan.WrapperClass.SqlHelperWraper.SingleParmOutPutMethod(localStrDetails, instructionType, instructionText, inputParamList);
//        //        }
//        //        else
//        //        {
//        //            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //            return null;
//        //        }
//        //   // }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
//        ///// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(connString, "GetOrderCount", 24, 36);
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object LocalScalarValueMethod(string localStrDetails, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listParamItems);

//        //        // Call the overload that takes an array of SqlParameters
//        //        ////Commented by purushotham on 2nd apr
//        //        //return ExecuteScalar(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return ImplementLocalScalarValue(localStrDetails, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //      // Otherwise we can just call the SP without params
//        //        return PerformLocalScalarValue(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount");
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object PerformConnectionScalarValue(string connection, string instructionType, string instructionText)
//        //{
//        //    //SqlConnection connection, string instructionType, string instructionText)
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    ////Commented by purushotham on 2nd apr
//        //   // return ExecuteScalar(connection, commandType, commandText, (SqlParameter[])null);
//        //    return TerraScan.WrapperClass.SqlHelperWraper.SingleParmOutPutMethod(connection, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        ///// 
//        ///// ////Commented by purushotham on 2nd apr
//        //////public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Execute the command & return the results
//        //////    object retval = cmd.ExecuteScalar();

//        //////    // Detach the SqlParameters from the command object, so they can be used again
//        //////    cmd.Parameters.Clear();

//        //////    if (mustCloseConnection)
//        //////    {
//        //////        connection.Close();
//        //////    }

//        //////    return retval;
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
//        ///// using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(conn, "GetOrderCount", 24, 36);
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object ConnectionScalarValueMethod(string connection, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        if (!CheckInputParams(listParamItems))
//        //        {
//        //            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //            SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //            // Assign the provided values to these parameters based on parameter order
//        //            AssignParameterValues(inputParamList, listParamItems);

//        //            ////Commented by purushotham on 2nd apr
//        //            // Call the overload that takes an array of SqlParameters
//        //           // return ExecuteScalar(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //            return TerraScan.WrapperClass.SqlHelperWraper.SingleParmOutPutMethod(connection, tempType, localProcDetails, inputParamList);
//        //        }
//        //        else
//        //        {
//        //            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //            return null;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return PerformConnectionScalarValue(connection,tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object PerformTransactionScalarValue(SqlTransaction transaction, string instructionType, string instructionText)
//        //{
//        //    //SqlTransaction transaction, string instructionType, string instructionText)
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    ////Commented by Purushotham 02.apr.14
//        //   // return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
//        //    return TerraScan.WrapperClass.SqlHelperWraper.UniqueParamOutPutMethod(transaction, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        ///// 
//        //////Commented by Purushotham on 02.apr.14
//        //////public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (transaction == null)
//        //////    {
//        //////        throw new ArgumentNullException("transaction");
//        //////    }

//        //////    if (transaction != null && transaction.Connection == null)
//        //////    {
//        //////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Execute the command & return the results
//        //////    object retval = cmd.ExecuteScalar();

//        //////    // Detach the SqlParameters from the command object, so they can be used again
//        //////    cmd.Parameters.Clear();
//        //////    return retval;
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  int orderCount = (int)ExecuteScalar(trans, "GetOrderCount", 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object TransactionScalarValueMethod(SqlTransaction transaction, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        if (!CheckInputParams(listParamItems))
//        //        {
//        //            // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //            SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //            // Assign the provided values to these parameters based on parameter order
//        //            AssignParameterValues(inputParamList, listParamItems);

//        //            // Call the overload that takes an array of SqlParameters
//        //            ////Commented by Purushotham on 02.apr.14
//        //           // return ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //            return TerraScan.WrapperClass.SqlHelperWraper.UniqueParamOutPutMethod(transaction, tempType, localProcDetails, inputParamList);
//        //        }
//        //        else
//        //        {
//        //            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //            return null;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return PerformTransactionScalarValue(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion ExecuteScalar

//        //#region ExecuteXmlReader

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //public static XmlReader PerformConnectionXmlReader(string localStrDetails, string instructionType, string instructionText)
//        //{
//        //    //SqlConnection localStrDetails, string instructionType, string instructionText)
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    ////return ExecuteXmlReader(connection, commandType, commandText, (SqlParameter[])null);
//        //    // return TerraScan.WrapperClass.SqlHelperWraper.PerformXMLReaderMethod(localStrDetails, instructionType, instructionText, (SqlParameter[])null);
//        //    return ImplementConnectionXmlDataSet(localStrDetails, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //////public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    bool mustCloseConnection = false;

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();

//        //////    try
//        //////    {
//        //////        if (!ValidateInputParams(commandParameters))
//        //////        {
//        //////            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////            // Create the DataAdapter & DataSet
//        //////            XmlReader retval = cmd.ExecuteXmlReader();

//        //////            // Detach the SqlParameters from the command object, so they can be used again
//        //////            cmd.Parameters.Clear();
//        //////            return retval;
//        //////        }
//        //////        else
//        //////        {
//        //////            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //////            return null;
//        //////        }
//        //////    }
//        //////    catch
//        //////    {
//        //////        if (mustCloseConnection)
//        //////        {
//        //////            connection.Close();
//        //////        }

//        //////        throw;
//        //////    }
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(conn, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure using "FOR XML AUTO"</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //public static XmlReader ImplementConnectionXmlDataSet(string localStrDetails, string localProcDetails, params object[] listParamItems)
//        //{
//        //    //sqlConnection
//        //    if (localStrDetails == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }
//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        if (!CheckInputParams(listParamItems))
//        //        {

//        //            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //            SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //            // Assign the provided values to these parameters based on parameter order
//        //            AssignParameterValues(inputParamList, listParamItems);

//        //            // Call the overload that takes an array of SqlParameters
//        //            ////return ExecuteXmlReader(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //            return TerraScan.WrapperClass.SqlHelperWraper.PerformXMLReaderMethod(localStrDetails, tempType, localProcDetails, inputParamList);
//        //        }
//        //        else
//        //        {
//        //            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //            return null;
//        //        }
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return PerformConnectionXmlReader(localStrDetails, tempType, localProcDetails);
//        //    }

//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders");
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //public static XmlReader PerformTransactionXmlReader(SqlTransaction transaction, string instructionType, string instructionText)
//        //{
//        //    //SqlTransaction transaction, string instructionType, string instructionText)
//        //    // Pass through the call providing null for the set of SqlParameters
//        //    //// return ExecuteXmlReader(transaction, commandType, commandText, (SqlParameter[])null);

//        //    return TerraScan.WrapperClass.SqlHelperWraper.ImplementXMLMethod(transaction, instructionType, instructionText, (SqlParameter[])null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command using "FOR XML AUTO"</param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //////public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
//        //////{
//        //////    if (transaction == null)
//        //////    {
//        //////        throw new ArgumentNullException("transaction");
//        //////    }

//        //////    if (transaction != null && transaction.Connection == null)
//        //////    {
//        //////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////    // Create the DataAdapter & DataSet
//        //////    XmlReader retval = cmd.ExecuteXmlReader();

//        //////    // Detach the SqlParameters from the command object, so they can be used again
//        //////    cmd.Parameters.Clear();
//        //////    return retval;
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  XmlReader r = ExecuteXmlReader(trans, "GetOrders", 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        //public static XmlReader ImplementTransactionXmlDataSet(SqlTransaction transaction, string localProcDetails, params object[] listParamItems)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listParamItems);

//        //        // Call the overload that takes an array of SqlParameters
//        //        //// return ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.ImplementXMLMethod(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        return PerformTransactionXmlReader(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion ExecuteXmlReader

//        //#region FillDataset

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in 
//        ///// the connection string. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)</param>
//        //public static void FillingResultantMethod(string localStrDetails, string instructionText, DataSet dataSet, string[] tableNames)
//        //{
//        //    //string localStrDetails, string commandType, string instructionType, DataSet dataSet, string[] tableNames)
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (dataSet == null)
//        //    {
//        //        throw new ArgumentNullException("dataSet");
//        //    }
            
//        //        PerformConnectionFillResultant(localStrDetails, instructionText, dataSet, tableNames);
//        //        //// Create & open a SqlConnection, and dispose of it after we are done
//        //        //using (SqlConnection connection = new SqlConnection(localStrDetails))
//        //        //{
//        //        //    connection.Open();

//        //        //    // Call the overload that takes a connection in place of the connection string
//        //        //   // PerformConnectionFillResultant(connection, instructionType, instructionText, dataSet, tableNames);
//        //        //}
            
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the database specified in the connection string 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// 
//        //////public static void FillDataset(
//        //////   string connectionString,
//        //////   CommandType commandType,
//        //////   string commandText,
//        //////   DataSet dataSet,
//        //////   string[] tableNames,
//        //////   params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connectionString == null || connectionString.Length == 0)
//        //////    {
//        //////        throw new ArgumentNullException("connectionString");
//        //////    }





//        //////    if (dataSet == null)
//        //////    {
//        //////        throw new ArgumentNullException("dataSet");
//        //////    }
//        //////    if (validateConnection(connectionString))
//        //////    {
//        //////        // Create & open a SqlConnection, and dispose of it after we are done
//        //////        using (SqlConnection connection = new SqlConnection(connectionString))
//        //////        {
//        //////            connection.Open();

//        //////            // Call the overload that takes a connection in place of the connection string
//        //////            FillDataset(connection, commandType, commandText, dataSet, tableNames, commandParameters);
//        //////        }
//        //////    }
//        //////    else
//        //////    {
//        //////        MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //////    }
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
//        ///// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  FillDataset(connString, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, 24);
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>    
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        //public static void ImplementLocalFillResultant(
//        //   string localStrDetails,
//        //   string localProcDetails,
//        //   DataSet dataSet,
//        //   string[] tableNames,
//        //   params object[] listParamItems)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (dataSet == null)
//        //    {
//        //        throw new ArgumentNullException("dataSet");
//        //    }

//        //    ConnectionFillResultantMethod(localStrDetails, localProcDetails, dataSet, tableNames, listParamItems);
//        //    //Create & open a SqlConnection, and dispose of it after we are done
//        //    //using (SqlConnection connection = new SqlConnection(localStrDetails))
//        //    //{
//        //    //    connection.Open();

//        //    //    // Call the overload that takes a connection in place of the connection string
//        //    //   //FillingResultantMethod (connection, localProcDetails, dataSet, tableNames, listParamItems);
//        //    //}
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>    
//        //public static void PerformConnectionFillResultant(
//        //string connection,
//        //    // string instructionType,
//        //string instructionText,
//        //DataSet dataSet,
//        //string[] tableNames)
//        //{
//        //    //SqlConnection connection,CommandType instructionType, string instructionText, DataSet dataSet,string[] tableNames)
//        //    // FillDataset(connection, commandType, commandText, dataSet, tableNames, null);
//        //    TerraScan.WrapperClass.SqlHelperWraper.FillResult(connection, instructionText, dataSet, tableNames, null);


//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(conn, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// 
//        //// Commented by purushotham on 03.04.14
//        //////public static void FillDataset(
//        //////SqlConnection connection,
//        //////CommandType commandType,
//        //////string commandText,
//        //////DataSet dataSet,
//        //////string[] tableNames,
//        //////params SqlParameter[] commandParameters)
//        //////{
//        //////    FillDataset(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
//        //////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  FillDataset(conn, "GetOrders", ds, new string[] {"orders"}, 24, 36);
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        //public static void ConnectionFillResultantMethod(
//        //string connection,
//        //string localProcDetails,
//        //DataSet dataSet,
//        //string[] tableNames,
//        //params object[] listParamItems)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (dataSet == null)
//        //    {
//        //        throw new ArgumentNullException("dataSet");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    // var tempType = CommandType.StoredProcedure.ToString();
//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((listParamItems != null) && (listParamItems.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Assign the provided values to these parameters based on parameter order
//        //        AssignParameterValues(inputParamList, listParamItems);

//        //        // Call the overload that takes an array of SqlParameters
//        //        ////FillDataset(connection, CommandType.StoredProcedure, storedProcedureName, dataSet, tableNames, commandParameters);
//        //        TerraScan.WrapperClass.SqlHelperWraper.FillResult(connection, localProcDetails, dataSet, tableNames, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        // Otherwise we can just call the SP without params
//        //        PerformConnectionFillResultant(connection, localProcDetails, dataSet, tableNames);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"});
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        //public static void PerformTransactionFillResultant(
//        //SqlTransaction transaction,
//        //    // string instructionType,
//        //string instructionText,
//        //DataSet dataSet,
//        //string[] tableNames)
//        //{
//        //    //    SqlTransaction transaction,
//        //    //string instructionType,
//        //    //string instructionText,
//        //    //DataSet dataSet,
//        //    ////string[] tableNames)
//        //    ImplementTransactionFillResultant(transaction, instructionText, dataSet, tableNames, null);
//        //}

//        ///// <summary>
//        ///// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        //public static void ImplementTransactionFillResultant(
//        //    SqlTransaction transaction,
//        //    // string instructionType,
//        //   string instructionText,
//        //   DataSet dataSet,
//        //   string[] tableNames,
//        //   params SqlParameter[] inputParamList)
//        //{
//        //    // SqlTransaction transaction,
//        //    //string instructionType,
//        //    //string instructionText,
//        //    //DataSet dataSet,
//        //    //string[] tableNames,
//        //    //params SqlParameter[] commandParameters)
//        //    //FillDataset(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames, commandParameters); //3nd wrapper
//        //    var tempStr = transaction.Connection.ConnectionString.ToString();
//        //    TerraScan.WrapperClass.SqlHelperWraper.ResultingValueMethod(tempStr, transaction, instructionText, dataSet, tableNames, inputParamList);
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
//        ///// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <remarks>
//        ///// This method provides no access to output parameters or the stored procedure's return value parameter.
//        ///// 
//        ///// e.g.:  
//        /////  FillDataset(trans, "GetOrders", ds, new string[]{"orders"}, 24, 36);
//        ///// </remarks>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="parameterValues">An array of objects to be assigned as the input values of the stored procedure</param>
//        ////public static void TransactionFillingResultantMethod(
//        ////   SqlTransaction transaction,
//        ////   string localProcDetails,
//        ////   DataSet dataSet,
//        ////   string[] tableNames,
//        ////   params object[] listParamItems)
//        ////{
//        ////    if (transaction == null)
//        ////    {
//        ////        throw new ArgumentNullException("transaction");
//        ////    }

//        ////    if (transaction != null && transaction.Connection == null)
//        ////    {
//        ////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        ////    }

//        ////    if (dataSet == null)
//        ////    {
//        ////        throw new ArgumentNullException("dataSet");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If we receive parameter values, we need to figure out where they go
//        ////    if ((listParamItems != null) && (listParamItems.Length > 0))
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        ////        // Assign the provided values to these parameters based on parameter order
//        ////        AssignParameterValues(inputParamList, listParamItems);

//        ////        // Call the overload that takes an array of SqlParameters
//        ////        ImplementTransactionFillResultant(transaction, localProcDetails, dataSet, tableNames, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        // Otherwise we can just call the SP without params
//        ////        PerformTransactionFillResultant(transaction, localProcDetails, dataSet, tableNames);
//        ////    }
//        ////}

//        //#endregion

//        //#region UpdateDataset

//        ///// <summary>
//        ///// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  UpdateDataset(conn, insertCommand, deleteCommand, updateCommand, dataSet, "Order");
//        ///// </remarks>
//        ///// <param name="insertCommand">A valid transact-SQL statement or stored procedure to insert new records into the data source</param>
//        ///// <param name="deleteCommand">A valid transact-SQL statement or stored procedure to delete records from the data source</param>
//        ///// <param name="updateCommand">A valid transact-SQL statement or stored procedure used to update records in the data source</param>
//        ///// <param name="dataSet">The DataSet used to update the data source</param>
//        ///// <param name="tableName">The DataTable used to update the data source.</param>
//        //public static void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
//        //{
//        //    if (insertCommand == null)
//        //    {
//        //        throw new ArgumentNullException("insertCommand");
//        //    }

//        //    if (deleteCommand == null)
//        //    {
//        //        throw new ArgumentNullException("deleteCommand");
//        //    }

//        //    if (updateCommand == null)
//        //    {
//        //        throw new ArgumentNullException("updateCommand");
//        //    }

//        //    if (tableName == null || tableName.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("tableName");
//        //    }

//        //    // Create a SqlDataAdapter, and dispose of it after we are done
//        //    using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
//        //    {
//        //        // Set the data adapter commands
//        //        dataAdapter.UpdateCommand = updateCommand;
//        //        dataAdapter.InsertCommand = insertCommand;
//        //        dataAdapter.DeleteCommand = deleteCommand;

//        //        // Update the dataset changes in the data source
//        //        dataAdapter.Update(dataSet, tableName);

//        //        // Commit all the changes made to the DataSet
//        //        dataSet.AcceptChanges();
//        //    }
//        //}
//        //#endregion

//        //#region CreateCommand

//        ///// <summary>
//        ///// Simplify the creation of a Sql command object by allowing
//        ///// a stored procedure and optional parameters to be provided
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  SqlCommand command = CreateCommand(conn, "AddCustomer", "CustomerID", "CustomerName");
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="sourceColumns">An array of string to be assigned as the source columns of the stored procedure parameters</param>
//        ///// <returns>A valid SqlCommand object</returns>
//        //public static SqlCommand CreateCommand(SqlConnection connection, string localProcDetails, params string[] sourceColumns)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }

//        //    // Create a SqlCommand
//        //    SqlCommand cmd = new SqlCommand(localProcDetails, connection);
//        //    cmd.CommandType = CommandType.StoredProcedure;

//        //    // If we receive parameter values, we need to figure out where they go
//        //    if ((sourceColumns != null) && (sourceColumns.Length > 0))
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Assign the provided source columns to these parameters based on parameter order
//        //        for (int index = 0; index < sourceColumns.Length; index++)
//        //        {
//        //            inputParamList[index].SourceColumn = sourceColumns[index];
//        //        }

//        //        // Attach the discovered parameters to the SqlCommand object
//        //        AttachParameters(cmd, inputParamList);
//        //    }

//        //    return cmd;
//        //}

//        ///// <summary>
//        ///// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
//        ///// to the provided command
//        ///// </summary>
//        ///// <param name="command">The SqlCommand to be prepared</param>
//        ///// <param name="connection">A valid SqlConnection, on which to execute this command</param>
//        ///// <param name="transaction">A valid SqlTransaction, or 'null'</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
//        ///// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
//        //public static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType instructionType, string instructionText, SqlParameter[] inputParamList, out bool mustCloseConnection)
//        //{
//        //    if (command == null)
//        //    {
//        //        throw new ArgumentNullException("command");
//        //    }

//        //    if (instructionText == null || instructionText.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("commandText");
//        //    }

//        //    // If the provided connection is not open, we will open it
//        //    if (connection.State != ConnectionState.Open)
//        //    {
//        //        mustCloseConnection = true;
//        //        connection.Open();
//        //    }
//        //    else
//        //    {
//        //        mustCloseConnection = false;
//        //    }

//        //    // Associate the connection with the command
//        //    command.Connection = connection;

//        //    // Set the command text (stored procedure name or SQL statement)
//        //    command.CommandText = instructionText;
//        //    command.CommandTimeout = 0;

//        //    // If we were provided a transaction, assign it
//        //    if (transaction != null)
//        //    {
//        //        if (transaction.Connection == null)
//        //        {
//        //            throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //        }

//        //        command.Transaction = transaction;
//        //    }

//        //    // Set the command type
//        //    command.CommandType = instructionType;

//        //    // Attach the command parameters if they are provided
//        //    if (inputParamList != null)
//        //    {
//        //        AttachParameters(command, inputParamList);
//        //    }

//        //    return;
//        //}
//        //#endregion

//        //#region ExecuteNonQueryTypedParams

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
//        ///// the connection string using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
//        ///// </summary>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int ExecuteNonQueryTypedParams(String localStrDetails, String localProcDetails, DataRow dataRow)
//        ////{
//        ////    if (localStrDetails == null || localStrDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("connectionString");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If the row has values, the store procedure parameters must be initialized
//        ////    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        ////        // Set the parameters values
//        ////        AssignParameterValues(inputParamList, dataRow);
//        ////        //// return SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);

//        ////        return TerraScan.WrapperClass.SqlHelperWraper.NonQueryResultMethod(localStrDetails, localProcDetails, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        return SqlHelper.LocalStringNonQueryMethod(localStrDetails, localProcDetails);
//        ////    }
//        ////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
//        ///// using the dataRow column values as the stored procedure's parameters values.  
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int ExecuteNonQueryConnectionTypedParams(string connection, String localProcDetails, DataRow dataRow)
//        ////{
//        ////    if (connection == null)
//        ////    {
//        ////        throw new ArgumentNullException("connection");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    //var tempType = CommandType.StoredProcedure.ToString();
//        ////    // If the row has values, the store procedure parameters must be initialized
//        ////    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        ////        // Set the parameters values
//        ////        AssignParameterValues(inputParamList, dataRow);

//        ////        ////return SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        ////        return TerraScan.WrapperClass.SqlHelperWraper.FetchNonQueryResults(connection, localProcDetails, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        return SqlHelper.LocalNonQueryMethod(connection, localProcDetails);
//        ////    }
//        ////}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified
//        ///// SqlTransaction using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
//        ///// </summary>
//        ///// <param name="transaction">A valid SqlTransaction object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An int representing the number of rows affected by the command</returns>
//        ////public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, String localProcDetails, DataRow dataRow)
//        ////{
//        ////    if (transaction == null)
//        ////    {
//        ////        throw new ArgumentNullException("transaction");
//        ////    }

//        ////    if (transaction != null && transaction.Connection == null)
//        ////    {
//        ////        throw new ArgumentException("The transaction was rollbacked or commited, please provide an proper transaction.", "transaction");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
//        ////    // var tempType = CommandType.StoredProcedure.ToString();
//        ////    // Sf the row has values, the store procedure parameters must be initialized
//        ////    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        ////    {
//        ////        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        ////        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        ////        // Set the parameters values
//        ////        AssignParameterValues(inputParamList, dataRow);
//        ////        // return SqlHelper.ExecuteNonQuery(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);

//        ////        return TerraScan.WrapperClass.SqlHelperWraper.PerformNonQueryMethod(transaction, localProcDetails, inputParamList);
//        ////    }
//        ////    else
//        ////    {
//        ////        return SqlHelper.LocalTransactionNonquery(transaction, localProcDetails);
//        ////    }
//        ////}
//        //#endregion

//        //#region ExecuteDatasetTypedParams


//        ///// <summary>
//        ///// Executes the dataset typed params.
//        ///// </summary>
//        ///// <param name="localStrDetails">The local STR details.</param>
//        ///// <param name="localProcDetails">The local proc details.</param>
//        ///// <param name="dataRow">The data row.</param>
//        ///// <returns></returns>
//        //public static DataSet ExecuteDatasetTypedParams(string localStrDetails, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////return SqlHelper.ExecuteDataset(localStrDetails, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.ImplementResultSetMethod(localStrDetails, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.ResultingDatasetMethod(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the dataRow column values as the store procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        //public static DataSet ExecuteDatasetConnectionTypedParams(string connection, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////return SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.PerformResultSetData(connection, tempType, localProcDetails, inputParamList);

//        //    }
//        //    else
//        //    {
//        //        //return SqlHelper.ExecuteDataset(connection,tempType, localProcDetails);
//        //        return SqlHelper.ReturnSampleDataSet(connection, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on row values.
//        ///// </summary>
//        ///// <param name="transaction">A valid SqlTransaction object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>A dataset containing the resultset generated by the command</returns>
//        //public static DataSet ExecuteDatasetTypedParams(SqlTransaction transaction, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        // return SqlHelper.ExecuteDataset(transaction, CommandType.StoredProcedure, localProcDetails, inputParamList);

//        //        return TerraScan.WrapperClass.SqlHelperWraper.PerformDatasetMethod(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        //return SqlHelper.ExecuteDataset(transaction,tempType, localProcDetails);
//        //        return SqlHelper.RetrnTransactionDataSet(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion

//        //Commented by purushotham to code clean up for security on 07.4.14
//        //#region ExecuteReaderTypedParams

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
//        ///// the connection string using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ExecuteReaderTypedParams(String localStrDetails, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////return SqlHelper.ExecuteReader(localStrDetails, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //        return SqlHelper.ImplementLocalDataReader(localStrDetails,tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        ////return SqlHelper.ExecuteReader(localStrDetails, CommandType.StoredProcedure, localProcDetails);
//        //        return SqlHelper.PerformLocalDataReader(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ExecuteReaderConnectionTypedParams(string connection, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        return SqlHelper.ImplementConnectionDataReader(connection, tempType, localProcDetails, inputParamList);
//        //       // return SqlHelper.ExecuteReader(connection, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformConnectionDataReader(connection, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="transaction">A valid SqlTransaction object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>A SqlDataReader containing the resultset generated by the command</returns>
//        //public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        //return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, localProcDetails, inputParamList);
//        //        return SqlHelper.ImplementTransactionDataReader(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //       // return SqlHelper.ExecuteReader(transaction, CommandType.StoredProcedure, localProcDetails);
//        //        return SqlHelper.PerformTransactionDataReader(transaction,tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion


//        //Commented by purushotham to code clean up for security on 07.4.14
//        //#region SingleValueTypedParams

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
//        ///// the connection string using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object SingleValueTypedParams(String localStrDetails, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (localStrDetails == null || localStrDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("connectionString");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(localStrDetails, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        //return SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return SqlHelper.ImplementLocalScalarValue(localStrDetails, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformLocalScalarValue(localStrDetails, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object SingleValueConnectionTypedParams(string connection, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        //return SqlHelper.ExecuteScalar(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.SingleParmOutPutMethod(connection, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformConnectionScalarValue(connection, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="transaction">A valid SqlTransaction object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
//        //public static object SingleValueTypedParams(SqlTransaction transaction, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////Commented by purushotham on 02.04.14
//        //       // return SqlHelper.ExecuteScalar(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.UniqueParamOutPutMethod(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformTransactionScalarValue(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion

//        //#region ExecuteXmlReaderTypedParams

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //public static XmlReader ExecuteXmlReaderTypedParams(string connection, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (connection == null)
//        //    {
//        //        throw new ArgumentNullException("connection");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////return SqlHelper.ExecuteXmlReader(connection, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.PerformXMLReaderMethod(connection, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformConnectionXmlReader(connection, tempType, localProcDetails);
//        //    }
//        //}

//        ///// <summary>
//        ///// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction 
//        ///// using the dataRow column values as the stored procedure's parameters values.
//        ///// This method will query the database to discover the parameters for the 
//        ///// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
//        ///// </summary>
//        ///// <param name="transaction">A valid SqlTransaction object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values.</param>
//        ///// <returns>An XmlReader containing the resultset generated by the command</returns>
//        //public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, String localProcDetails, DataRow dataRow)
//        //{
//        //    if (transaction == null)
//        //    {
//        //        throw new ArgumentNullException("transaction");
//        //    }

//        //    if (transaction != null && transaction.Connection == null)
//        //    {
//        //        throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
//        //    }

//        //    if (localProcDetails == null || localProcDetails.Length == 0)
//        //    {
//        //        throw new ArgumentNullException("spName");
//        //    }
//        //    var tempType = CommandType.StoredProcedure.ToString();
//        //    // If the row has values, the store procedure parameters must be initialized
//        //    if (dataRow != null && dataRow.ItemArray.Length > 0)
//        //    {
//        //        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
//        //        SqlParameter[] inputParamList = SqlHelperParameterCache.GetSpParameterSet(transaction.Connection, localProcDetails);

//        //        // Set the parameters values
//        //        AssignParameterValues(inputParamList, dataRow);
//        //        ////return SqlHelper.ExecuteXmlReader(transaction, CommandType.StoredProcedure, storedProcedureName, commandParameters);
//        //        return TerraScan.WrapperClass.SqlHelperWraper.ImplementXMLMethod(transaction, tempType, localProcDetails, inputParamList);
//        //    }
//        //    else
//        //    {
//        //        return SqlHelper.PerformTransactionXmlReader(transaction, tempType, localProcDetails);
//        //    }
//        //}
//        //#endregion

//        //#region private utility methods & constructors

//        ///// <summary>
//        ///// Private helper method that execute a SqlCommand (that returns a resultset) against the specified SqlTransaction and SqlConnection
//        ///// using the provided parameters.
//        ///// </summary>
//        ///// <remarks>
//        ///// e.g.:  
//        /////  FillDataset(conn, trans, CommandType.StoredProcedure, "GetOrders", ds, new string[] {"orders"}, new SqlParameter("@prodid", 24));
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection</param>
//        ///// <param name="transaction">A valid SqlTransaction</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="dataSet">A dataset wich will contain the resultset generated by the command</param>
//        ///// <param name="tableNames">This array will be used to create table mappings allowing the DataTables to be referenced
//        ///// by a user defined name (probably the actual table name)
//        ///// </param>
//        ///// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
//        ///// 
//        ////// Commented by purushotham on 03..4.14
//        //////private static void FillDataset(
//        //////SqlConnection connection,
//        //////SqlTransaction transaction,
//        //////CommandType commandType,
//        //////string commandText,
//        //////DataSet dataSet,
//        //////string[] tableNames,
//        //////params SqlParameter[] commandParameters)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    if (dataSet == null)
//        //////    {
//        //////        throw new ArgumentNullException("dataSet");
//        //////    }

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand command = new SqlCommand();
//        //////    bool mustCloseConnection = false;
//        //////    if (!ValidateInputParams(commandParameters))
//        //////    {
//        //////        PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

//        //////        // Create the DataAdapter & DataSet
//        //////        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
//        //////        {
//        //////            // Add the table mappings specified by the user
//        //////            if (tableNames != null && tableNames.Length > 0)
//        //////            {
//        //////                string tableName = "Table";

//        //////                for (int index = 0; index < tableNames.Length; index++)
//        //////                {
//        //////                    if (tableNames[index] == null || tableNames[index].Length == 0)
//        //////                    {
//        //////                        throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
//        //////                    }

//        //////                    if (index == 0)
//        //////                    {
//        //////                        dataAdapter.TableMappings.Add(tableName, tableNames[index]);
//        //////                    }
//        //////                    else
//        //////                    {
//        //////                        dataAdapter.TableMappings.Add(tableName + index.ToString(), tableNames[index]);
//        //////                    }
//        //////                }
//        //////            }

//        //////            // Fill the DataSet using default values for DataTable names, etc
//        //////            dataAdapter.Fill(dataSet);

//        //////            // Detach the SqlParameters from the command object, so they can be used again
//        //////            command.Parameters.Clear();
//        //////        }

//        //////        if (mustCloseConnection)
//        //////        {
//        //////            connection.Close();
//        //////        }
//        //////    }
//        //////}

//        ///// <summary>
//        ///// Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
//        ///// </summary>
//        ///// <remarks>
//        ///// If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
//        ///// 
//        ///// If the caller provided the connection, we want to leave it to them to manage.
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection, on which to execute this command</param>
//        ///// <param name="transaction">A valid SqlTransaction, or 'null'</param>
//        ///// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
//        ///// <param name="connectionOwnership">Indicates whether the connection parameter was provided by the caller, or created by SqlHelper</param>
//        ///// <returns>SqlDataReader containing the results of the command</returns>
//        ///// 
//        //////ommented by purushotham on 5.4.14
//        //////private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, string instructionType, string instructionText, SqlParameter[] inputParamList, SqlConnectionOwnership connectionOwnership)
//        //////{
//        //////    if (connection == null)
//        //////    {
//        //////        throw new ArgumentNullException("connection");
//        //////    }

//        //////    bool mustCloseConnection = false;

//        //////    // Create a command and prepare it for execution
//        //////    SqlCommand cmd = new SqlCommand();

//        //////    try
//        //////    {
//        //////        if (!ValidateInputParams(inputParamList))
//        //////        {
//        //////            CommandType tempType = ParseEnum<CommandType>(instructionType);
//        //////            PrepareCommand(cmd, connection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);

//        //////            // Create a reader
//        //////            SqlDataReader dataReader;

//        //////            // Call ExecuteReader with the appropriate CommandBehavior
//        //////            if (connectionOwnership == SqlConnectionOwnership.External)
//        //////            {
//        //////                dataReader = cmd.ExecuteReader();
//        //////            }
//        //////            else
//        //////            {
//        //////                dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
//        //////            }

//        //////            // Detach the SqlParameters from the command object, so they can be used again.
//        //////            // HACK: There is a problem here, the output parameter values are fletched 
//        //////            // when the reader is closed, so if the parameters are detached from the command
//        //////            // then the SqlReader can´t set its values. 
//        //////            // When this happen, the parameters can´t be used again in other command.
//        //////            bool canClear = true;

//        //////            foreach (SqlParameter commandParameter in cmd.Parameters)
//        //////            {
//        //////                if (commandParameter.Direction != ParameterDirection.Input)
//        //////                {
//        //////                    canClear = false;
//        //////                }
//        //////            }

//        //////            if (canClear)
//        //////            {
//        //////                cmd.Parameters.Clear();
//        //////            }

//        //////            return dataReader;
//        //////        }
//        //////        else
//        //////        {
//        //////            MessageBox.Show("An input parameter is invalid ", "TerraScan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        //////            return null;
//        //////        }
//        //////    }
//        //////    catch
//        //////    {
//        //////        if (mustCloseConnection)
//        //////        {
//        //////            connection.Close();
//        //////        }

//        //////        throw;
//        //////    }
//        //////}

//        ///// <summary>
//        ///// This method is used to attach array of SqlParameters to a SqlCommand.
//        ///// 
//        ///// This method will assign a value of DbNull to any parameter with a direction of
//        ///// InputOutput and a value of null.  
//        ///// 
//        ///// This behavior will prevent default values from being used, but
//        ///// this will be the less common case than an intended pure output parameter (derived as InputOutput)
//        ///// where the user provided no input value.
//        ///// </summary>
//        ///// <param name="command">The command to which the parameters will be added</param>
//        ///// <param name="commandParameters">An array of SqlParameters to be added to command</param>
//        ////private static void AttachParameters(SqlCommand instruction, SqlParameter[] inputParamList)
//        ////{
//        ////    if (instruction == null)
//        ////    {
//        ////        throw new ArgumentNullException("command");
//        ////    }

//        ////    if (inputParamList != null)
//        ////    {
//        ////        foreach (SqlParameter p in inputParamList)
//        ////        {
//        ////            if (p != null)
//        ////            {
//        ////                // (v-markdg)Commented out original application block code 
//        ////                // because setting the parameter values to DBNull.Value will
//        ////                // keep any stored procedure parameter defaults from being
//        ////                // assigned.
//        ////                //      // Check for derived output value with no value assigned
//        ////                //      if ((p.Direction == ParameterDirection.InputOutput || 
//        ////                //       p.Direction == ParameterDirection.Input) && 
//        ////                //       (p.Value == null))
//        ////                //      {
//        ////                //       p.Value = DBNull.Value;
//        ////                //      }
//        ////                instruction.Parameters.Add(p);
//        ////            }
//        ////        }
//        ////    }
//        ////}

//        ///// <summary>
//        ///// This method assigns dataRow column values to an array of SqlParameters
//        ///// </summary>
//        ///// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
//        ///// <param name="dataRow">The dataRow used to hold the stored procedure's parameter values</param>
//        ////private static void AssignParameterValues(SqlParameter[] inputParamList, DataRow dataRow)
//        ////{
//        ////    if ((inputParamList == null) || (dataRow == null))
//        ////    {
//        ////        // Do nothing if we get no data
//        ////        return;
//        ////    }

//        ////    int i = 0;

//        ////    // Set the parameters values
//        ////    foreach (SqlParameter tempParameter in inputParamList)
//        ////    {
//        ////        // Check the parameter name
//        ////        if (tempParameter.ParameterName == null ||
//        ////           tempParameter.ParameterName.Length <= 1)
//        ////        {
//        ////            throw new Exception(
//        ////               string.Format(
//        ////               "Please provide a valid parameter name on the parameter #{0}, the ParameterName property has the following value: '{1}'.",
//        ////               i,
//        ////               tempParameter.ParameterName));
//        ////        }

//        ////        if (dataRow.Table.Columns.IndexOf(tempParameter.ParameterName.Substring(1)) != -1)
//        ////        {
//        ////            tempParameter.Value = dataRow[tempParameter.ParameterName.Substring(1)];
//        ////        }

//        ////        i++;
//        ////    }
//        ////}

//        ///// <summary>
//        ///// This method assigns an array of values to an array of SqlParameters
//        ///// </summary>
//        ///// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
//        ///// <param name="parameterValues">Array of objects holding the values to be assigned</param>
//        ////private static void AssignParameterValues(SqlParameter[] inputParamList, object[] listParamItems)
//        ////{
//        ////    if ((inputParamList == null) || (listParamItems == null))
//        ////    {
//        ////        // Do nothing if we get no data
//        ////        return;
//        ////    }

//        ////    // We must have the same number of values as we pave parameters to put them in
//        ////    if (inputParamList.Length != listParamItems.Length)
//        ////    {
//        ////        throw new ArgumentException("Parameter count does not match Parameter Value count.");
//        ////    }

//        ////    // Iterate through the SqlParameters, assigning the values from the corresponding position in the 
//        ////    // value array
//        ////    for (int i = 0, j = inputParamList.Length; i < j; i++)
//        ////    {
//        ////        // If the current array value derives from IDbDataParameter, then assign its Value property
//        ////        if (listParamItems[i] is IDbDataParameter)
//        ////        {
//        ////            IDbDataParameter paramInstance = (IDbDataParameter)listParamItems[i];

//        ////            if (paramInstance.Value == null)
//        ////            {
//        ////                inputParamList[i].Value = DBNull.Value;
//        ////            }
//        ////            else
//        ////            {
//        ////                inputParamList[i].Value = paramInstance.Value;
//        ////            }
//        ////        }
//        ////        else if (listParamItems[i] == null)
//        ////        {
//        ////            inputParamList[i].Value = DBNull.Value;
//        ////        }
//        ////        else
//        ////        {
//        ////            inputParamList[i].Value = listParamItems[i];
//        ////        }
//        ////    }
//        ////}

//        //#endregion private utility methods & constructors
//    }

//    /// <summary>
//    /// SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
//    /// ability to discover parameters for stored procedures at run-time.
//    /// </summary>
//    public sealed class SqlHelperParameterCache
//    {
//       // #region fields

//       // /// <summary>
//       // /// Hash table to cache the parameter
//       // /// </summary>
//       //// private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());
//       //// private static readonly string localString = ConfigurationManager.AppSettings["ConnectionString"];


//       // #endregion fields

//        //#region constructor

//        ///// <summary>
//        ///// SqlHelperParameterCache class constructor
//        ///// </summary>
//        //private SqlHelperParameterCache()
//        //{
//        //}

//        //#endregion constructor

//        //#region Public Methods

//        ///// <summary>
//        ///// Retrieves the set of SqlParameters appropriate for the stored procedure
//        ///// </summary>
//        ///// <remarks>
//        ///// This method will query the database for this information, and then store it in a cache for future requests.
//        ///// </remarks>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
//        ///// <returns>An array of SqlParameters</returns>
//        ////public static SqlParameter[] GetSpParameterSet(string localStrDetails, string localProcDetails, bool includeReturnValueParameter)
//        ////{
//        ////    if (localStrDetails == null || localStrDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("connectionString");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }
            
//        ////        //using (SqlConnection connection = new SqlConnection(localStrDetails))
//        ////        //{
//        ////    return TerraScan.WrapperClass.SqlHelperWraper.GetSpParameterSetInternal(localStrDetails, localProcDetails, includeReturnValueParameter);
//        ////        //}
            
//        ////}
//        ///// <summary>
//        ///// Gets the sp parameter set.
//        ///// </summary>
//        ///// <param name="localStrDetails">The local STR details.</param>
//        ///// <param name="localProcDetails">The local proc details.</param>
//        ///// <returns></returns>
//        ////public static SqlParameter[] GetSpParameterSet(string localStrDetails, string localProcDetails)
//        ////{
//        ////    return GetSpParameterSet(localStrDetails, localProcDetails, false);
//        ////}

//        ///// <summary>
//        ///// Add parameter array to the cache
//        ///// </summary>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <param name="commandParameters">An array of SqlParamters to be cached</param>
//        ////public static void CacheParameterSet(string localStrDetails, string instructionText, params SqlParameter[] inputParamList)
//        ////{
//        ////    if (localStrDetails == null || localStrDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("connectionString");
//        ////    }

//        ////    if (instructionText == null || instructionText.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("commandText");
//        ////    }

//        ////    string hashKey = localStrDetails + ":" + instructionText;
//        ////    paramCache[hashKey] = inputParamList;
//        ////}

//        ///// <summary>
//        ///// Retrieve a parameter array from the cache
//        ///// </summary>
//        ///// <param name="connectionString">A valid connection string for a SqlConnection</param>
//        ///// <param name="commandText">The stored procedure name or T-SQL command</param>
//        ///// <returns>An array of SqlParamters</returns>
//        ////public static SqlParameter[] GetCachedParameterSet(string localStrDetails, string instructionText)
//        ////{
//        ////    if (localStrDetails == null || localStrDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("connectionString");
//        ////    }

//        ////    if (instructionText == null || instructionText.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("commandText");
//        ////    }

//        ////    string hashKey = localStrDetails + ":" + instructionText;
//        ////    SqlParameter[] cachedParameters = paramCache[hashKey] as SqlParameter[];

//        ////    if (cachedParameters == null)
//        ////    {
//        ////        return null;
//        ////    }
//        ////    else
//        ////    {
//        ////        return CloneParameters(cachedParameters);
//        ////    }
//        ////}

//        //#endregion Public Methods

//        //#region Parameter Discovery Functions

//        ///// <summary>
//        ///// Retrieves the set of SqlParameters appropriate for the stored procedure
//        ///// </summary>
//        ///// <remarks>
//        ///// This method will query the database for this information, and then store it in a cache for future requests.
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <returns>An array of SqlParameters</returns>
//        ////internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string localProcDetails)
//        ////{
//        ////    return GetSpParameterSet(connection, localProcDetails, false);
//        ////}

//        ///// <summary>
//        ///// Retrieves the set of SqlParameters appropriate for the stored procedure
//        ///// </summary>
//        ///// <remarks>
//        ///// This method will query the database for this information, and then store it in a cache for future requests.
//        ///// </remarks>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
//        ///// <returns>An array of SqlParameters</returns>
//        ////internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string localProcDetails, bool includeReturnValueParameter)
//        ////{
//        ////    if (connection == null)
//        ////    {
//        ////        throw new ArgumentNullException("connection");
//        ////    }

//        ////    // using (SqlConnection clonedConnection = (SqlConnection)((ICloneable)connection).Clone())
//        ////    //{
//        ////         return GetSpParameterSetInternal(connection, localProcDetails, includeReturnValueParameter);
//        ////    //}
//        ////}

//        //#endregion Parameter Discovery Functions

//        //#region private methods, variables, and constructors

//        //// Since this class provides only static methods, make the default constructor private to prevent 
//        //// instances from being created with "new SqlHelperParameterCache()"

//        ///// <summary>
//        ///// Resolve at run time the appropriate set of SqlParameters for a stored procedure
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
//        ///// <returns>The parameter array discovered.</returns>
//        ////private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string localProcDetails, bool includeReturnValueParameter)
//        ////{
//        ////    if (connection == null)
//        ////    {
//        ////        throw new ArgumentNullException("connection");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }

//        ////    SqlCommand cmd = new SqlCommand(localProcDetails, connection);
//        ////    cmd.CommandType = CommandType.StoredProcedure;
//        ////    connection.Open();
//        ////    SqlCommandBuilder.DeriveParameters(cmd);
//        ////    connection.Close();

//        ////    if (!includeReturnValueParameter)
//        ////    {
//        ////        cmd.Parameters.RemoveAt(0);
//        ////    }

//        ////    SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
//        ////    cmd.Parameters.CopyTo(discoveredParameters, 0);

//        ////    // Init the parameters with a DBNull value
//        ////    foreach (SqlParameter discoveredParameter in discoveredParameters)
//        ////    {
//        ////        discoveredParameter.Value = DBNull.Value;
//        ////    }

//        ////    return discoveredParameters;
//        ////}

//        ///// <summary>
//        ///// Deep copy of cached SqlParameter array
//        ///// </summary>
//        ///// <param name="originalParameters"> The sql parameter array to be cloned..</param>
//        ///// <returns> The cloned sqlparameter array.</returns>
//        ////private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
//        ////{
//        ////    SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

//        ////    for (int i = 0, j = originalParameters.Length; i < j; i++)
//        ////    {
//        ////        clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
//        ////    }

//        ////    return clonedParameters;
//        ////}

//        ///// <summary>
//        ///// Retrieves the set of SqlParameters appropriate for the stored procedure
//        ///// </summary>
//        ///// <param name="connection">A valid SqlConnection object</param>
//        ///// <param name="storedProcedureName">The name of the stored procedure</param>
//        ///// <param name="includeReturnValueParameter">A bool value indicating whether the return value parameter should be included in the results</param>
//        ///// <returns>An array of SqlParameters</returns>
//        ////private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string localProcDetails, bool includeReturnValueParameter)
//        ////{
//        ////    if (connection == null)
//        ////    {
//        ////        throw new ArgumentNullException("connection");
//        ////    }

//        ////    if (localProcDetails == null || localProcDetails.Length == 0)
//        ////    {
//        ////        throw new ArgumentNullException("spName");
//        ////    }

//        ////    string hashKey = connection.ConnectionString + ":" + localProcDetails + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
//        ////    SqlParameter[] cachedParameters;
//        ////    cachedParameters = paramCache[hashKey] as SqlParameter[];

//        ////    if (cachedParameters == null)
//        ////    {
//        ////        SqlParameter[] procedureParameters = TerraScan.WrapperClass.SqlHelperWraper.WrapeerDiscoverSpParameterSet(connection, localProcDetails, includeReturnValueParameter);
//        ////            //DiscoverSpParameterSet(connection, localProcDetails, includeReturnValueParameter);

//        ////        paramCache[hashKey] = procedureParameters;
//        ////        cachedParameters = procedureParameters;
//        ////    }

//        ////    return CloneParameters(cachedParameters);
//        ////}
//        //#endregion private methods, variables, and constructors


//        //#region Validate Methods
        
//        ///// <summary>
//        ///// Validates for SQL injection.
//        ///// </summary>
//        ///// <param name="userInput">The user input.</param>
//        ///// <returns></returns>
//        //private static Boolean ValidateInputParams(SqlParameter[] userInput)
//        //{
//        //    bool isValid = false;
//        //    string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
//        //                               "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
//        //                               };

//        //    foreach (var item in userInput)
//        //    {
//        //        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
//        //        {
//        //            if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
//        //            {
//        //                if (item.ToString().ToLower().Contains(sqlCheckList[i]))
//        //                {
//        //                    isValid = true;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return isValid;
//        //}

//        ///// <summary>
//        ///// Checks the sclar SQL injection.
//        ///// </summary>
//        ///// <param name="userInput">The user input.</param>
//        ///// <returns></returns>
//        //private static Boolean CheckInputParams(params object[] userInput)
//        //{
//        //    bool isValid = false;
//        //    string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
//        //                               "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
//        //                               };

//        //    foreach (var item in userInput)
//        //    {
//        //        for (int i = 0; i <= sqlCheckList.Length - 1; i++)
//        //        {
//        //            if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
//        //            {
//        //                if (item.ToString().ToLower().Contains(sqlCheckList[i]))
//        //                {
//        //                    isValid = true;
//        //                }
//        //            }
//        //        }
//        //    }
//        //    return isValid;
//        //}

//        //#endregion
//    }
//}
