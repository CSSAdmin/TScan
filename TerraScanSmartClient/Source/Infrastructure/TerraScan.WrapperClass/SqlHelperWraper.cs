namespace TerraScan.WrapperClass
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.SqlClient;
    using System.Data;
    using System.Windows.Forms;
    using System.Data.Sql;
    using System.Configuration;
    using System.Xml;


    /// <summary>
    /// 
    /// </summary>
   public static class SqlHelperWraper
    {
       private static readonly string tempString = ConfigurationManager.AppSettings["ConnectionString"];

        #region Scalar Methods
       public static object SingleParmOutPutMethod(string connection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlConnection tempConnection = new SqlConnection(connection);
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, tempConnection, (SqlTransaction)null, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                tempConnection.Close();
            }

            return retval;
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void PrepareCommand(SqlCommand instruction, SqlConnection connection, SqlTransaction transaction, CommandType instructionType, string instructionText, SqlParameter[] inputParamList, out bool mustCloseConnection)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("command");
            }

            if (instructionText == null || instructionText.Length == 0)
            {
                throw new ArgumentNullException("commandText");
            }

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            instruction.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            instruction.CommandText = instructionText;
            instruction.CommandTimeout = 0;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null)
                {
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                }

                instruction.Transaction = transaction;
            }

            // Set the command type
            instruction.CommandType = instructionType;

            // Attach the command parameters if they are provided
            if (inputParamList != null)
            {
                AttachParameters(instruction, inputParamList);
            }

            return;
        }

        private static void AttachParameters(SqlCommand instruction, SqlParameter[] inputList)
        {
            if (instruction == null)
            {
                throw new ArgumentNullException("command");
            }

            if (inputList != null)
            {
                foreach (SqlParameter p in inputList)
                {
                    if (p != null)
                    {
                        instruction.Parameters.Add(p);
                    }
                }
            }
        }

        public static object UniqueParamOutPutMethod(SqlTransaction transaction, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            if (transaction != null && transaction.Connection == null)
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, transaction.Connection, transaction, tempType, instructionType, inputParamList, out mustCloseConnection);

            // Execute the command & return the results
            object retval = cmd.ExecuteScalar();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        } 
        #endregion

        #region Filling Result
        public static void ResultingValueMethod(
     string connection,
     SqlTransaction transaction,
     string instructionType,
     string instructionText,
     DataSet dataSet,
     string[] tableNames,
     params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            SqlConnection tempConnection = new SqlConnection(connection);
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }

            // Create a command and prepare it for execution
            SqlCommand command = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(command, tempConnection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                // Add the table mappings specified by the user
                if (tableNames != null && tableNames.Length > 0)
                {
                    string tableName = "Table";

                    for (int index = 0; index < tableNames.Length; index++)
                    {
                        if (tableNames[index] == null || tableNames[index].Length == 0)
                        {
                            throw new ArgumentException("The tableNames parameter must contain a list of tables, a value was provided as null or empty string.", "tableNames");
                        }

                        if (index == 0)
                        {
                            dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                        }
                        else
                        {
                            dataAdapter.TableMappings.Add(tableName + index.ToString(), tableNames[index]);
                        }
                    }
                }

                // Fill the DataSet using default values for DataTable names, etc
                dataAdapter.Fill(dataSet);

                // Detach the SqlParameters from the command object, so they can be used again
                command.Parameters.Clear();
            }

            if (mustCloseConnection)
            {
                tempConnection.Close();
            }
        }

        public static void FillResult(
       string connection,
       string instructionType,
       string instructionText,
       DataSet dataSet,
       string[] tableNames,
       params SqlParameter[] inputParamList)
        {
            ResultingValueMethod(connection, null, instructionType, instructionText, dataSet, tableNames, inputParamList);
        }



        /// <summary>
        /// Fillings the result value.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="dataSet">The data set.</param>
        /// <param name="tableNames">The table names.</param>
        /// <param name="commandParameters">The command parameters.</param>
        public static void FillingResultValue(
           string localStrDetails,
           string instructionType,
           string instructionText,
           DataSet dataSet,
           string[] tableNames,
           params SqlParameter[] inputParamList)
        {
            if (localStrDetails == null || localStrDetails.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if (validateConnection(localStrDetails))
            {
                FillResult(localStrDetails, instructionType, instructionText, dataSet, tableNames, inputParamList);
             //Create & open a SqlConnection, and dispose of it after we are done
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();

                 //Call the overload that takes a connection in place of the connection string
               // FillResult(connection, instructionType, instructionText, dataSet, tableNames, commandParameters);
            //}
            }
            else
            {
                MessageBox.Show("An input parameter is invalid ", "Taeerascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public static DataSet PerformDatasetMethod(SqlTransaction transaction, string instructionType, string instructionText, params SqlParameter[] inputListValues)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            if (transaction != null && transaction.Connection == null)
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, transaction.Connection, transaction, tempType, instructionText, inputListValues, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                // Return the dataset
                return ds;
            }
        }
        #endregion

        #region Non query Method

        /// <summary>
        /// Nons the query result method.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static int NonQueryResultMethod(string localConnection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (localConnection == null || localConnection.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }
            
            if (validateConnection(localConnection))
            {
                return FetchNonQueryResults(localConnection, instructionType, instructionText, inputParamList);
                //    // Create & open a SqlConnection, and dispose of it after we are done
                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    connection.Open();

                //    // Call the overload that takes a connection in place of the connection string
                //    return FetchNonQueryResults(connection, instructionType, instructionText, commandParameters);
                //}
            }
            else
            {
                MessageBox.Show("An input parameter is invalid ", "Taeerascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        /// <summary>
        /// Fetches the non query results.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static int FetchNonQueryResults(string connection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlConnection tempConnection = new SqlConnection(connection);
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, tempConnection, (SqlTransaction)null, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            if (mustCloseConnection)
            {
                tempConnection.Close();
            }

            return retval;
        }

        /// <summary>
        /// Performs the non query method.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static int PerformNonQueryMethod(SqlTransaction transaction, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            if (transaction != null && transaction.Connection == null)
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, transaction.Connection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Finally, execute the command
            int retval = cmd.ExecuteNonQuery();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        } 
        #endregion

        #region XML Reader Methods
        public static XmlReader PerformXMLReaderMethod(string connection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlConnection tempConnection = new SqlConnection(connection);
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            bool mustCloseConnection = false;

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!ValidateInputParams(inputParamList))
                {
                    
                    CommandType tempType = ParseEnum<CommandType>(instructionType);
                    PrepareCommand(cmd, tempConnection, (SqlTransaction)null, tempType, instructionText, inputParamList, out mustCloseConnection);

                    // Create the DataAdapter & DataSet
                    XmlReader retval = cmd.ExecuteXmlReader();

                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();
                    return retval;
                }
                else
                {
                    MessageBox.Show("An input parameter is invalid ", "Taeerascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            catch
            {
                if (mustCloseConnection)
                {
                    tempConnection.Close();
                }

                throw;
            }
        }


        /// <summary>
        /// Implements the XML method.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static XmlReader ImplementXMLMethod(SqlTransaction transaction, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            if (transaction != null && transaction.Connection == null)
            {
                throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            }

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, transaction.Connection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            XmlReader retval = cmd.ExecuteXmlReader();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return retval;
        } 
        #endregion

        #region Validation Methods
        private static Boolean validateConnection(string validString)
        {
            bool isValidConnection = false;
            if (!string.IsNullOrEmpty(tempString) && !string.IsNullOrEmpty(validString) && (validString.Equals(tempString)))
            {
                isValidConnection = true;
            }
            return isValidConnection;
        }

        private static Boolean ValidateInputParams(SqlParameter[] userInput)
        {
            bool isSQLInjection = false;
            string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
                                       "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
                                       };

            foreach (var item in userInput)
            {
                for (int i = 0; i <= sqlCheckList.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
                    {
                        if (item.ToString().ToLower().Contains(sqlCheckList[i]))
                        {
                            isSQLInjection = true;
                        }
                    }
                }
            }
            return isSQLInjection;
        } 
        #endregion


        public static DataSet ImplementResultSetMethod(string localStrDetails, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (localStrDetails == null || localStrDetails.Length == 0)
            {
                throw new ArgumentNullException("connectionString");
            }
            return PerformResultSetData(localStrDetails, instructionType, instructionText, inputParamList);
            // Create & open a SqlConnection, and dispose of it after we are done
            //using (SqlConnection connection = new SqlConnection(localStrDetails))
            //{
            //    connection.Open();
            //    // Call the overload that takes a connection in place of the connection string
            //    return PerformResultSetData(connection, instructionType, instructionText, commandParameters);
            //}
        }


        public static DataSet PerformResultSetData(string connection, string instructionType, string instructionText, params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlConnection tempConnection = new SqlConnection(connection);
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            bool mustCloseConnection = false;
            CommandType tempType = ParseEnum<CommandType>(instructionType);
            PrepareCommand(cmd, tempConnection, (SqlTransaction)null, tempType, instructionText, inputParamList, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                {
                    tempConnection.Close();
                }

                // Return the dataset
                return ds;
            }
        }

        public static SqlDataReader OutSideConnectionDataReader(string connection, SqlTransaction transaction, string instructionType, string instructionText, SqlParameter[] inputParamList, SqlConnectionOwnership connectionOwnership)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            SqlConnection tempConnection = new SqlConnection(connection);
            //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
            bool mustCloseConnection = false;

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();

            try
            {
                if (!ValidateInputParams(inputParamList))
                {
                    CommandType tempType = ParseEnum<CommandType>(instructionType);
                    PrepareCommand(cmd, tempConnection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);

                    // Create a reader
                    SqlDataReader dataReader;

                    // Call ExecuteReader with the appropriate CommandBehavior
                    if (connectionOwnership == SqlConnectionOwnership.External)
                    {
                        dataReader = cmd.ExecuteReader();
                    }
                    else
                    {
                        dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }

                    // Detach the SqlParameters from the command object, so they can be used again.
                    // HACK: There is a problem here, the output parameter values are fletched 
                    // when the reader is closed, so if the parameters are detached from the command
                    // then the SqlReader can´t set its values. 
                    // When this happen, the parameters can´t be used again in other command.
                    bool canClear = true;

                    foreach (SqlParameter commandParameter in cmd.Parameters)
                    {
                        if (commandParameter.Direction != ParameterDirection.Input)
                        {
                            canClear = false;
                        }
                    }

                    if (canClear)
                    {
                        cmd.Parameters.Clear();
                    }

                    return dataReader;
                }
                else
                {
                    MessageBox.Show("An input parameter is invalid ", "Taeerascan - T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
            }
            catch
            {
                if (mustCloseConnection)
                {
                    tempConnection.Close();
                }

                throw;
            }
        }

        #region enum

        /// <summary>
        /// This enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
        /// we can set the appropriate CommandBehavior when calling ExecuteReader()
        /// </summary>
        public enum SqlConnectionOwnership
        {
            /// <summary>
            /// Connection is owned and managed by SqlHelper
            /// </summary>
            Internal,

            /// <summary>
            /// Connection is owned and managed by the caller
            /// </summary>
            External
        }
        #endregion enum
    }
}
