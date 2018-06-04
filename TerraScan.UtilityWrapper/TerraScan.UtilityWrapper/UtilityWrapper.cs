using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace TerraScan.UtilityWrapper
{
    public static class UtilityWrapper
    {
        private static readonly string tempStrDetails = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];


        /// <summary>
        /// Checks for SQL injection.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns></returns>
        private static Boolean CheckInputParams(IDictionary userInput)
        {
            bool isValid = false;
            string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
                                       "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
                                       };

            foreach (var item in userInput.Values)
            {
                for (int i = 0; i <= sqlCheckList.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
                    {
                        if (item.ToString().ToLower().Contains(sqlCheckList[i]))
                        {
                            isValid = true;
                        }
                    }
                }
            }
            return isValid;
        }
        public static int WrapperFetchSPOutput(string localProcDetails, IDictionary listParam)
        {
            //SqlConnection connection = new SqlConnection(tempStrDetails)
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParam.GetEnumerator();
                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                    // Add the Parameter
                    command.Parameters.Add(param);
                }

                // Create the DataAdapter & DataSet
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    adapter.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();

                    // Return the int value from dataset
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
            }
        }

        public static object WrapperFetchSpObject(string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();
                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                    // Add the Parameter
                    command.Parameters.Add(param);
                }

                // Create the DataAdapter & DataSet
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    adapter.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();

                    // Return the value from dataset
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0];
                    }

                    return null;

                }
            }
        }


        public static int WrapperFetchSPKeyId(string localProcDetails, IDictionary listParamItems)
        {
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                //SqlConnection connection = null;
                //connection = new SqlConnection(tempStrDetails);
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = "@PrimaryKeyID";


                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputParam = new SqlParameter(outPutParameterName, 0);
                    outputParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return Convert.ToInt32(outputParam.Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public static IList WrapperSPParameters(string localProcDetails, DataTable dataTable1, IDictionary listParamItems, DataTable dataTable)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
               // connection = new SqlConnection(tempStrDetails);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }

                    int paramcount = dataTable1.Rows.Count;
                    //add all the keyfields,value and datatype in sqlparamter list formation
                    for (int invalue = 0; invalue < paramcount; invalue++)
                    {
                        SqlDbType pa = new SqlDbType();
                        int sizelength = 10;
                        if (dataTable1.Rows[invalue]["DataType"].ToString().Equals("int"))
                        {
                            pa = SqlDbType.Int;
                            sizelength = 3000;
                        }
                        else if (dataTable1.Rows[invalue]["DataType"].ToString().Equals("string"))
                        {
                            pa = SqlDbType.VarChar;
                            sizelength = 10000;
                        }
                        else if (dataTable1.Rows[invalue]["DataType"].ToString().Equals("decimal"))
                        {
                            pa = SqlDbType.Decimal;
                            sizelength = 8;
                        }
                        else if (dataTable1.Rows[invalue]["DataType"].ToString().Equals("bool"))
                        {
                            pa = SqlDbType.Bit;
                            sizelength = 2;
                        }
                        else
                        {
                            pa = SqlDbType.Decimal;
                            sizelength = 8;
                        }

                        SqlParameter outputParam = new SqlParameter(dataTable1.Rows[invalue]["Key"].ToString(), pa, sizelength);
                        outputParam.Direction = ParameterDirection.Output;
                        outputParam.SqlDbType = pa;
                        command.Parameters.Add(outputParam);
                    }
                    command.ExecuteNonQuery();
                    DataRow row = dataTable.NewRow();
                    // used to enter the new row for enter value to display in single column
                    for (int outValue = 0; outValue < paramcount; outValue++)
                    {
                        if (command.Parameters[dataTable1.Rows[outValue][0].ToString()] != null)
                        {
                            row[outValue] = command.Parameters[dataTable1.Rows[outValue][0].ToString()].Value.ToString();
                        }
                        else
                        {
                            row[outValue] = string.Empty;
                        }
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    dataTable.Rows.Add(row);
                    return (IList)command.Parameters;
                }
                else
                {
                    return null;
                }
            }
        }

        public static IList WrapperFetchSPOuputParameters(DataTable dataTable, string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string firstOutPutParameter = "@PrimaryKeyID";
                string secondOutputParameter = "@Message";


                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputFirstParam = new SqlParameter(firstOutPutParameter, 0);
                    outputFirstParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputFirstParam);
                    SqlParameter outputSecondParam = new SqlParameter(secondOutputParameter, SqlDbType.VarChar, 3000);
                    outputSecondParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputSecondParam);
                    command.ExecuteNonQuery();

                    DataColumn primaryColumn = new DataColumn(firstOutPutParameter);
                    DataColumn messageColumn = new DataColumn(secondOutputParameter);
                    dataTable.Columns.Add(primaryColumn);
                    dataTable.Columns.Add(messageColumn);
                    DataRow row = dataTable.NewRow();
                    if (command.Parameters[firstOutPutParameter].Value != null)
                    {
                        row[firstOutPutParameter] = command.Parameters[firstOutPutParameter].Value.ToString();
                    }
                    else
                    {
                        row[firstOutPutParameter] = string.Empty;
                    }

                    if (command.Parameters[secondOutputParameter].Value != null)
                    {
                        row[secondOutputParameter] = command.Parameters[secondOutputParameter].Value.ToString();
                    }
                    else
                    {
                        row[secondOutputParameter] = string.Empty;
                    }

                    //dataTable.Rows[0][0] = command.Parameters[0].Value.ToString();

                    dataTable.Rows.Add(row);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return (IList)command.Parameters;
                }
                else
                {
                    return null;
                }
            }
        }

        public static string WrapperFetchSingleOuputParameter(string localProcDetails, IDictionary listParamItems, string outputParamName)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = outputParamName.Trim();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();

                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                //timeOutExcep;
                // To read the Param and Value from Hashtable value

                if (!CheckInputParams(listParamItems))
                {
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputParam = new SqlParameter(outPutParameterName, SqlDbType.NVarChar, 4000);
                    outputParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return outputParam.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static int WrapperFetchSPReturnValue(string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string returnValue = "@RetVal";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                // To read the Param and Value from Hashtable value
                if (!CheckInputParams(listParamItems))
                {
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter retutnValueParam = new SqlParameter(returnValue, SqlDbType.Int);
                    retutnValueParam.Direction = ParameterDirection.ReturnValue;
                    // Add the Parameter
                    command.Parameters.Add(retutnValueParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return Convert.ToInt32(retutnValueParam.Value.ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public static string WrapperFetchSPKeyString(string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = "@PrimaryKeyID";
                //connection = new SqlConnection(tempStrDetails);
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputParam = new SqlParameter(outPutParameterName, SqlDbType.NVarChar, 500);
                    outputParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return outputParam.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
       // [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "User must use Sql Stored procedure or sql parameterized command")]

        public static string WrapperMessageKeyString(string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = "@Message";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputParam = new SqlParameter(outPutParameterName, SqlDbType.NVarChar, 500);
                    outputParam.Direction = ParameterDirection.Output;
                    // Add the Parameter
                    command.Parameters.Add(outputParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return outputParam.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static string WrapperFetchSPXmlString(string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();
                string outPutParameterName = "@OutputXML";

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0; //timeOutExcep; 
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }
                    SqlParameter outputParam = new SqlParameter(outPutParameterName, SqlDbType.Xml, 2147483647);
                    outputParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outputParam);
                    command.ExecuteNonQuery();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return outputParam.Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static IList WrapperFetchSPOutputValue(DataTable dataTable, string localProcDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlParameter PostName = new SqlParameter();


                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                SqlCommand command = new SqlCommand(localProcDetails, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();

                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);
                        if (listEnumParam.Key.ToString() == "@Form" || listEnumParam.Key.ToString() == "@Param1Out" || listEnumParam.Key.ToString() == "@Param2" || listEnumParam.Key.ToString() == "@Param3" || listEnumParam.Key.ToString() == "@Param4")
                        {
                            param.Direction = ParameterDirection.InputOutput;
                            param.Size = 50;
                        }
                        // Add the Parameter
                        command.Parameters.Add(param);
                    }

                    command.ExecuteNonQuery();
                    // return (IList)command.Parameters;
                    DataRow row = dataTable.NewRow();

                    for (int i = 0; i <= dataTable.Columns.Count - 1; i++)
                    {
                        row[dataTable.Columns[i].Caption] = command.Parameters["@" + dataTable.Columns[i].Caption].Value.ToString();
                        //dataTable.Rows[0][0] = command.Parameters[0].Value.ToString();
                    }

                    dataTable.Rows.Add(row);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return (IList)command.Parameters;
                    // return command.Parameters[0].Value.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static int WrapperSALExcecuteSP(string procDetail, IDictionary listParamItems)
        {
            int rowAffected;
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                //SqlConnection connection = null;
                //connection = new SqlConnection(tempStrDetails);
                SqlCommand command = new SqlCommand(procDetail, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (!CheckInputParams(listParamItems))
                {
                    // To read the Param and Value from Hashtable value
                    IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();
                    while (listEnumParam.MoveNext())
                    {
                        // Set the SP Param and its corresponding Value
                        SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                        // Add the Parameter
                        command.Parameters.Add(param);
                    }

                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return rowAffected;
                }

                else
                {
                    return 0;
                }
            }
        }

        public static object WrapperSALFetchSpObject(string procDetail, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlCommand command = new SqlCommand(procDetail, connection);
                command.CommandType = CommandType.StoredProcedure;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();
                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                    // Add the Parameter
                    command.Parameters.Add(param);
                }

                // Create the DataAdapter & DataSet
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    adapter.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();

                    // Return the value from dataset
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0];
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return null;
                }
            }
        }


        public static DataSet WrapperSALFetchDataSet(string procDetail, IDictionary listParamItems)
        {
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlCommand command = new SqlCommand(procDetail, connection);
                command.CommandType = CommandType.StoredProcedure;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();
                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);

                    // Add the Parameter
                    command.Parameters.Add(param);
                }

                // Create the DataAdapter & DataSet
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    adapter.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();

                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    // Return the dataset
                    return ds;
                }
            }
        }


        public static DataSet WrapperSALFetchDataSet(string procDetails)
        {
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(procDetails, connection))
                {
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return ds;
                }
            }
        }

        public static DataTable WrapperSALFetchDataTable(string procDetails, IDictionary listParamItems)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                SqlCommand command = new SqlCommand(procDetails, connection);
                command.CommandType = CommandType.StoredProcedure;

                // To read the Param and Value from Hashtable value
                IDictionaryEnumerator listEnumParam = listParamItems.GetEnumerator();
                while (listEnumParam.MoveNext())
                {
                    // Set the SP Param and its corresponding Value
                    SqlParameter param = new SqlParameter(listEnumParam.Key.ToString(), listEnumParam.Value);
                    command.Parameters.Add(param);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();

                    // Fill the DataSet using default values for DataTable names, etc
                    adapter.Fill(dt);

                    // Detach the SqlParameters from the command object, so they can be used again
                    command.Parameters.Clear();
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    // Return the datatable
                    return dt;
                }
            }
        }

        public static DataTable WrapperSALFetchDataTable(string procDetails)
        {
            //SqlConnection connection = null;
            //connection = new SqlConnection(tempStrDetails);
            using (SqlConnection connection = new SqlConnection(tempStrDetails))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(procDetails, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                    return dt;
                }
            }
        }


        public static SqlParameter[] DiscoverSpParameterSet(string localProcDetails, bool includeReturnValueParameter)
        {
            if (localProcDetails == null || localProcDetails.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }
            using (SqlConnection tempStr = new SqlConnection(tempStrDetails))
            {
                SqlCommand cmd = new SqlCommand(localProcDetails, tempStr);
                cmd.CommandType = CommandType.StoredProcedure;
                tempStr.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                tempStr.Close();

                if (!includeReturnValueParameter)
                {
                    cmd.Parameters.RemoveAt(0);
                }

                SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(discoveredParameters, 0);

                // Init the parameters with a DBNull value
                foreach (SqlParameter discoveredParameter in discoveredParameters)
                {
                    discoveredParameter.Value = DBNull.Value;
                }

                return discoveredParameters;
            }
        }

        // var section = ConfigurationManager.GetSection("appSettings").ToString();
        //NameValueCollection appSettings = ConfigurationManager.AppSettings;
        //var sting = (appSettings.GetKey(0).ToString() + "-" + appSettings[0].ToString());
        //var mine = appSettings[0].ToString();
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        #region public Methods

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
                    throw new ArgumentException("The transaction was rollbacked or commited, please provide an proper transaction.", "transaction");
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

        #endregion

        #region Filling Result
        public static void ResultingValueMethod(
     string connection,
     SqlTransaction transaction,
            //string instructionType,
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
            using (SqlConnection tempConnection = new SqlConnection(connection))
            {
                if (dataSet == null)
                {
                    throw new ArgumentNullException("dataSet");
                }

                // Create a command and prepare it for execution
                SqlCommand command = new SqlCommand();
                command.Connection = tempConnection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                bool mustCloseConnection = false;
                var tempType = CommandType.StoredProcedure; //ParseEnum<CommandType>(instructionType);
                PrepareCommand(command, tempConnection, transaction, tempType, instructionText, inputParamList, out mustCloseConnection);


                // SqlCommand tempCommand = ParseEnum<SqlCommand>(command.ToString());
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
        }

        public static void FillResult(
       string connection,
            //string instructionType,
       string instructionText,
       DataSet dataSet,
       string[] tableNames,
       params SqlParameter[] inputParamList)
        {
            ResultingValueMethod(connection, null, instructionText, dataSet, tableNames, inputParamList);
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
            // string localStrDetails,
            //string instructionType,
           string instructionText,
           DataSet dataSet,
           string[] tableNames,
           params SqlParameter[] inputParamList)
        {
            var localStrDetails = tempStrDetails;
            if (localStrDetails == null || localStrDetails.Length == 0)
            {
                throw new ArgumentNullException("connectionDetails");
            }

            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if (validateConnection(localStrDetails))
            {
                FillResult(localStrDetails, instructionText, dataSet, tableNames, inputParamList);
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
        public static int NonQueryResultMethod(string instructionText, params SqlParameter[] inputParamList)
        {
            var localConnection = tempStrDetails;
            if (localConnection == null || localConnection.Length == 0)
            {
                throw new ArgumentNullException("connectionDetails");
            }

            if (validateConnection(localConnection))
            {
                return FetchNonQueryResults(localConnection, instructionText, inputParamList);
            }
            else
            {
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
        public static int FetchNonQueryResults(string connection, string instructionText, params SqlParameter[] inputParamList)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            using (SqlConnection tempConnection = new SqlConnection(connection))
            {
                //SqlConnection tempConnection = ParseEnum<SqlConnection>(connection);
                // Create a command and prepare it for execution
                SqlCommand cmd = new SqlCommand();
                bool mustCloseConnection = false;
                var tempType = CommandType.StoredProcedure; //= ParseEnum<CommandType>(instructionType);
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
        }


        #endregion


        #region Validation Methods
        private static Boolean validateConnection(string validString)
        {
            bool isValidConnection = false;
            if (!string.IsNullOrEmpty(tempStrDetails) && !string.IsNullOrEmpty(validString) && (validString.Equals(tempStrDetails)))
            {
                isValidConnection = true;
            }
            return isValidConnection;
        }

        #endregion


        /// <summary>
        /// Wrapeers the discover sp parameter set.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="localProcDetails">The local proc details.</param>
        /// <param name="includeReturnValueParameter">if set to <c>true</c> [include return value parameter].</param>
        /// <returns></returns>
        private static SqlParameter[] WrapeerDiscoverSpParameterSet(SqlConnection connection, string localProcDetails, bool includeReturnValueParameter)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            if (localProcDetails == null || localProcDetails.Length == 0)
            {
                throw new ArgumentNullException("spName");
            }

            SqlCommand cmd = new SqlCommand(localProcDetails, connection);
            cmd.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter)
            {
                cmd.Parameters.RemoveAt(0);
            }

            SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (SqlParameter discoveredParameter in discoveredParameters)
            {
                discoveredParameter.Value = DBNull.Value;
            }

            return discoveredParameters;
        }

        /// <summary>
        /// Gets the sp parameter set internal.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="localProcDetails">The local proc details.</param>
        /// <param name="includeReturnValueParameter">if set to <c>true</c> [include return value parameter].</param>
        /// <returns></returns>
        public static SqlParameter[] GetSpParameterSetInternal(string localProcDetails, bool includeReturnValueParameter)
        {
            using (SqlConnection myConn = new SqlConnection(tempStrDetails))
            {
                if (myConn == null)
                {
                    throw new ArgumentNullException("connection");
                }

                if (localProcDetails == null || localProcDetails.Length == 0)
                {
                    throw new ArgumentNullException("spName");
                }

                string hashKey = myConn.ConnectionString + ":" + localProcDetails + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
                SqlParameter[] cachedParameters;
                cachedParameters = paramCache[hashKey] as SqlParameter[];

                if (cachedParameters == null)
                {
                    SqlParameter[] procedureParameters = WrapeerDiscoverSpParameterSet(myConn, localProcDetails, includeReturnValueParameter);
                    paramCache[hashKey] = procedureParameters;
                    cachedParameters = procedureParameters;
                }

                return CloneParameters(cachedParameters);
            }
        }

        /// <summary>
        /// Clones the parameters.
        /// </summary>
        /// <param name="originalParameters">The original parameters.</param>
        /// <returns></returns>
        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] clonedParameters = new SqlParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();
            }

            return clonedParameters;
        }
    }

}

