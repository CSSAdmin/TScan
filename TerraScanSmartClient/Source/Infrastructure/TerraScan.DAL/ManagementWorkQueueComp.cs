// -------------------------------------------------------------------------------------------
// <copyright file="ManagementWorkQueueComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Affidavit WorkQueue Inspection</summary>
// Release history
//**********************************************************************************
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// AffidavitManagementQueueData
    /// </summary>
    public static class ManagementWorkQueueComp
    {
        /// <summary>
        /// F1109_s the list management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="treasurer">The treasurer.</param>
        /// <param name="assessor">The assessor.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="statementNumber">The statement number.</param>
        /// <returns>Returns ManagementWorkQueue Dataset</returns>
        public static AffidavitManagementQueue F1109_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, int rollYear, string statementNumber)
        {
            AffidavitManagementQueue managementQueueDataSet = new AffidavitManagementQueue();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@Name", name);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@Address", address);
            ht.Add("@TaxCode", taxCode);
            ht.Add("@Treasurer", treasurer);
            ht.Add("@Assessor", assessor);
            ht.Add("@StatementNumber", statementNumber);

            if (rollYear == 0)
            {
                ht.Add("@RollYear", DBNull.Value);
            }
            else
            {
                ht.Add("@RollYear", rollYear);
            }

            ////string[] tableName = new string[] { managementQueueDataSet.ListManagementQueue.TableName, managementQueueDataSet.ListRollYear.TableName };
            Utility.LoadDataSet(managementQueueDataSet.ListManagementQueue, "f1109_pclst_ExciseTaxAffidavitManagementQueue", ht);
            return managementQueueDataSet;
        }

        /// <summary>
        /// F1109_s the management queue filter result.
        /// </summary>
        /// <param name="statusFilterId">The status filter id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="filterFromDate">The filter from date.</param>
        /// <param name="filterToDate">The filter to date.</param>
        /// <returns>
        /// Returns ManagementWorkQueue Filter Result
        /// </returns>
        public static AffidavitManagementQueue F1109_ManagementQueueFilterResult(int statusFilterId, int rollYear, string filterFromDate, string filterToDate)
        {
            AffidavitManagementQueue managementQueueDataSet = new AffidavitManagementQueue();
            Hashtable ht = new Hashtable();

            if (statusFilterId == 0)
            {
                ht.Add("@StatusFilterID", DBNull.Value);
            }
            else
            {
                ht.Add("@StatusFilterID", statusFilterId);
            }

            if (rollYear == 0)
            {
                ht.Add("@RollYear", DBNull.Value);
            }
            else
            {
                ht.Add("@RollYear", rollYear);
            }

            ht.Add("@FromDate", filterFromDate);
            ht.Add("@ToDate", filterToDate);
            Utility.LoadDataSet(managementQueueDataSet.ListManagementQueue, "f1109_pclst_ExciseTaxAffidavitFilter", ht);
            return managementQueueDataSet;
        }

        /// <summary>
        /// F1109_s the list roll year.
        /// </summary>
        /// <returns>Returns Rollyear DataSet</returns>
        public static AffidavitManagementQueue F1109_ListRollYear()
        {
            AffidavitManagementQueue managementQueueDataSet = new AffidavitManagementQueue();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(managementQueueDataSet.ListRollYear, "f1109_pclst_ExciseTaxAffidavitRollYear", ht);
            return managementQueueDataSet;
        }

        /// <summary>
        /// F1109_s the filter search affidavit.
        /// </summary>
        /// <param name="filterXml">The filter XML.</param>
        /// <returns>
        /// Returns ManagementWorkQueue Filter Result
        /// </returns>
        public static AffidavitManagementQueue F1109_FilterSearchAffidavit(string filterXml)
        {
            AffidavitManagementQueue managementQueueDataSet = new AffidavitManagementQueue();
            Hashtable ht = new Hashtable();
            ht.Add("@FilterSearch", filterXml);
            Utility.LoadDataSet(managementQueueDataSet.ListManagementQueue, "f1109_pclst_ExciseTaxAffidavitFilterSearch", ht);
            return managementQueueDataSet;
        }
    }
}
