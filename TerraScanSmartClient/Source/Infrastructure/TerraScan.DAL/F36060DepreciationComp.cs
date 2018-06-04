// -------------------------------------------------------------------------------------------
// <copyright file="F36060DepreciationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36060DepreciationComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 15/12/07         M.Vijayakumar      Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36060DepreciationComp Class file
    /// </summary>
    public static class F36060DepreciationComp
    {
        #region F36060DepreciationComp

        #region F36060_GetDepreciationTables

        /// <summary>
        /// To get the Depreciation  tables
        /// </summary>
        /// <param name="deprTableId">Deprtable id</param>
        /// <returns>Typed dataset containing the Deprecition and Deprecition items datatable</returns>
        public static F36060DepreciationData F36060_GetDepreciationTables(int deprTableId)
        { 
            F36060DepreciationData form36060DepreciationData = new F36060DepreciationData();
            Hashtable ht = new Hashtable();
            ht.Add("@DeprTableID", deprTableId);
            string[] tableName = new string[] { form36060DepreciationData.GetDepreciationTables.TableName, form36060DepreciationData.ListDepreciationIteamsTables.TableName };
            Utility.LoadDataSet(form36060DepreciationData, "f36060_pcget_DepreciationTables", ht, tableName);
            return form36060DepreciationData;
        }

        #endregion F36060_GetDepreciationTables

        #region F36060_SaveDepreciationTables

        /// <summary>
        /// To save depreciation tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="deprecationItem">The deprecation item.</param>
        /// <param name="otherDeprItem">The other depr item.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Inserted or update key id is returned</returns>
        public static int F36060_SaveDepreciationTables(int deprTableId, string deprecationItem, string otherDeprItem, int userId)
        {
            Hashtable ht = new Hashtable();

            if (deprTableId > 0)
            {
                ht.Add("@DeprTableID", deprTableId);
            }

            ht.Add("@DeprecationItem", deprecationItem);
            ht.Add("@OtherDeprItem", otherDeprItem);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36060_pcins_DepreciationTables", ht);           
        }

        #endregion F36060_SaveDepreciationTables

        #region F36060_DeleteDepreciationTables

        /// <summary>
        /// To delete Depreciation Tables.
        /// </summary>
        /// <param name="deprTableId">The depr table id.</param>
        /// <param name="userId">The user id.</param>
        public static void F36060_DeleteDepreciationTables(int deprTableId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DeprTableID", deprTableId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f36060_pcdel_DepreciationTable", ht);
        }

        #endregion F36060_DeleteDepreciationTables

        #endregion F36060DepreciationComp
    }
}
