// -------------------------------------------------------------------------------------------
// <copyright file="Utility.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the general helper methods.</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Reflection;

    /// <summary>
    /// Terrascan Utility
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// <author>Thilak Raj</author>
        /// <createdOn>20 Oct 2005</createdOn>
        /// <lastUpdated></lastUpdated>
        /// This function is used to generate the XML string from the datatable.
        /// This function is used for the purpose of updating multiple rows from the datagrid
        /// </summary>
        /// <param name="dt">datatable which is to be updated</param>
        /// <returns>xml string passed to the database</returns>
        public static string GetXmlString(DataTable dt)
        {
            DataSet ds = new DataSet("Root");            
            DataTable tempDt = new DataTable();
            if (dt != null)
            {
                tempDt = dt.Copy();
                tempDt.Namespace = string.Empty;
            }

            tempDt.TableName = "Table";
            ds.Tables.Add(tempDt);
            return ds.GetXml();
        }

        /// <summary>
        /// Gets the XML string - added by ranjani.
        /// </summary>
        /// <param name="dt">The datatable array.</param>
        /// <returns></returns>
        public static string GetXmlString(DataTable[] dt)
        {
            DataSet ds = new DataSet("Root");           
            DataTable tempDt = new DataTable();
            for (int i = 0; i < dt.Length; i++)
            {
                if (dt[i] != null)
                {                    
                    tempDt = dt[i].Copy();
                    if (i == 0)
                    {
                        tempDt.TableName = "Table";
                    }
                    else
                    {
                        tempDt.TableName = string.Concat("Table", i);
                    }

                    tempDt.Namespace = string.Empty;
                    ds.Tables.Add(tempDt);
                }
            }
            
            return ds.GetXml();
        }

        /// <summary>
        /// Returns the dllName.FormName
        /// </summary>
        /// <param name="formName">Calling Form Name </param>
        /// <returns>String</returns>
        public static string GetFormNameSpace(string formName)
        {
            Assembly dllName = Assembly.GetCallingAssembly();
            return dllName.ToString().Substring(0, dllName.ToString().IndexOf(',')) + "." + formName;            
        }
    }
}