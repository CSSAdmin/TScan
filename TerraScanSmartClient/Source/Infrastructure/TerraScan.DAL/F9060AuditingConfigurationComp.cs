// -------------------------------------------------------------------------------------------
// <copyright file="F9060AuditingConfigurationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27006ParcelOwnershipComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 12/04/07         H.Vinayagamurthy       Created
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
    /// F9060AuditingConfigurationComp Class File
    /// </summary>
    public static class F9060AuditingConfigurationComp
    {
        #region F9060 Audit Configuration

        #region List Auditing Tables

        /// <summary>
        /// To List Table Name Details
        /// </summary>
        /// <param name="tableType">Table Type</param>
        /// <returns>Typed DataSet Containing the Table Name Details</returns>
        public static F9060AuditingConfigurationData F9060_ListAuditingTables(string tableType)
        {
            F9060AuditingConfigurationData auditingConfigurationData = new F9060AuditingConfigurationData();
            Hashtable ht = new Hashtable();
            ht.Add("@TableType", tableType);
            Utility.LoadDataSet(auditingConfigurationData.ListAuditingBaseTables, "f9060_pclst_AuditingBaseTables", ht);
            return auditingConfigurationData;
        }

        #endregion List Auditing Tables

        #region List Auditing Columns

        /// <summary>
        /// To List Table Column Details
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <returns>Typed DataSet Containing the Table Column Details</returns>
        public static F9060AuditingConfigurationData F9060_ListAuditingColumns(string tableName)
        {
            F9060AuditingConfigurationData auditingConfigurationData = new F9060AuditingConfigurationData();
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", tableName);
            Utility.LoadDataSet(auditingConfigurationData.ListAuditingColumnsDataTable, "f9060_pclst_AuditingColumns", ht);
            return auditingConfigurationData;
        }

        #endregion List Auditing Columns

        #region Save Audit Column Configuration

        /// <summary>
        /// To Save Audit Table Configuration
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="auditColumns">Auditing Columns</param>
        /// <param name="userId">userId</param>
        public static void F9060_SaveAuditConfiguration(string tableName, string auditColumns, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TableName", tableName);
            ht.Add("@AuditColumn", auditColumns);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9060_pcins_AuditingColumns", ht);
        }

        #endregion Save Audit Column Configuration

        #region Delete Audit Configuration

        /// <summary>
        /// To Delete Audit Table Configuration
        /// </summary>
        /// <param name="auditTableId">Audit Table ID</param>
        /// <param name="userId">userId</param>
        public static void F9060_DeleteAuditConfiguration(int auditTableId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AuditTableID", auditTableId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9060_pcdel_AuditingColumns", ht);
        }

        #endregion Delete Audit Configuration

        #endregion F9060 Audit Configuration
    }
}
