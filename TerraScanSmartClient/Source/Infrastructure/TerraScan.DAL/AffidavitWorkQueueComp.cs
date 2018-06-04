// -------------------------------------------------------------------------------------------
// <copyright file="AffidavitWorkQueueComp.cs" company="Congruent">
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
    /// AffidavitWorkQueueComp class
    /// </summary>
    public static class AffidavitWorkQueueComp
    {
        /// <summary>
        /// Gets the work queue search result.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="receiptDate">The Receipt date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="treasurer">The treasurer.</param>
        /// <param name="assessor">The assessor.</param>
        /// <param name="statementNumber">The statementNumber</param>
        /// <returns>return AffidavitWorkQueueData dataset</returns>
        public static AffidavitWorkQueueData F1107_ExciseWorkQueue_GetWorkQueueSearchResult(string parcelNumber, string name, string receiptDate, string address, string taxCode, string treasurer, string assessor, string statementNumber)
        {
            AffidavitWorkQueueData affidavitWorkQueue = new AffidavitWorkQueueData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@Name", name);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@Address", address);
            ht.Add("@TaxCode", taxCode);
            ht.Add("@Treasurer", treasurer);
            ht.Add("@Assessor", assessor);
            ht.Add("@StatementNumber", statementNumber);
            Utility.LoadDataSet(affidavitWorkQueue.ListExciseTaxAffidavitWorkQueue, "f1107_pclst_ExciseTaxAffidavitWorkQueue", ht);
            return affidavitWorkQueue;
        }
    }
}
