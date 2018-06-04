
// -------------------------------------------------------------------------------------------
// <copyright file="RdlToCodeComp.cs" company="Congruent">
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
    public static class RdlToCodeComp
    {
        #region Get Form Details

        /// <summary>
        /// RDLs to code_ get.
        /// </summary>
        /// <param name="getxmlStringValue">The get XML string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>dataset.</returns>
        public static DataSet RdlToCode_Get(string getxmlStringValue, string formId)
        {
            DataSet rdlDataSet = new DataSet();
            Hashtable ht = new Hashtable();
            string spname = formId + "_Get";
            string inputParameter = "@" + formId + "Data";

            ////for (int rowCount = 0; rowCount < parameterData.Rows.Count; rowCount++)
            ////{
            //    ht.Add("@" + parameterData.Rows[rowCount]["KeyID"].ToString(), parameterData.Rows[rowCount]["KeyName"].ToString());
            ////}
            ht.Add(inputParameter, getxmlStringValue);
            ht.Add("@FormId", formId);
            Utility.LoadDataSet(rdlDataSet, spname, ht);
            return rdlDataSet;
        }

        #endregion Get Form Details

        #region  Get Method for Combo Box

        /// <summary>
        /// RDLs to code_ fill combo.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure.</param>
        /// <returns>dataset</returns>
        public static DataSet RdlToCode_FillCombo(string storedProcedureName)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = DataProxy.FetchDataTable(storedProcedureName);
            ds.Tables.Add(dt);
            return ds;
        }

        #endregion Get Method for Combo Box

        #region Save Method

        /// <summary>
        /// RDLs to code_ save.
        /// </summary>
        /// <param name="savexmlStringValue">The insert XML string.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>Integer value</returns>
        public static int RdlToCode_Save(string savexmlStringValue, string formId)
        {
            Hashtable ht = new Hashtable();
            int errorId;
            string inputParameter = "@" + formId + "Datas";
            string spname = formId + "_Save";
            ht.Add(inputParameter, savexmlStringValue);
            //// ht.Add("@FormId", formId);
            ////Utility.ExecuteSP(spName, ht);
            errorId = Utility.FetchSPExecuteKeyId(spname, ht);
            return errorId;

            ////for (int rowCount = 0; rowCount < parameterData.Rows.Count; rowCount++)
            ////{
            ////    ht.Add("@" + parameterData.Rows[rowCount]["KeyID"].ToString(), parameterData.Rows[rowCount]["KeyName"].ToString());
            ////}
        }

        #endregion Save Method

        #region Delete Method

        /// <summary>
        /// RDLs to code_ delete.
        /// </summary>
        /// <param name="deletexmlStringValue">The delete XML string.</param>
        /// <param name="formId">The form id.</param>
        public static void RdlToCode_Delete(string deletexmlStringValue, string formId)
        {
            Hashtable ht = new Hashtable();
            string inputParameter = "@" + formId + "Data";
            string spname = formId + "_Delete";
            ht.Add(inputParameter, deletexmlStringValue);
            ////ht.Add("@FormId", formId);
            Utility.ImplementProcedure(spname, ht);
        }

        #endregion Delete Method
    }
}
