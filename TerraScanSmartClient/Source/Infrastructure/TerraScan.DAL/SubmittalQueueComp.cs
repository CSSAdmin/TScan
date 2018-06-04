// -------------------------------------------------------------------------------------------
// <copyright file="SubmittalQueueComp.cs" company="Congruent">
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
    /// SubmittalQueueComp
    /// </summary>
    public static class SubmittalQueueComp
    {
        /// <summary>
        /// F1108_s the get submit affidavit.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Returns SubmitAffidavit DatatSet</returns>
        public static REETA F1108_GetSubmitAffidavit(string statementId)
        {
            REETA submitDataset = new REETA();            
            Hashtable ht = new Hashtable();
            ht.Add("@StatementIDs", statementId);
            string[] dataTable = new string[] { submitDataset.AFFIDAVIT.TableName, submitDataset.INDIVIDUAL.TableName, submitDataset.PARCEL.TableName, submitDataset.USE_CODES.TableName, submitDataset.SUPPLEMENTAL.TableName };
            Utility.LoadDataSet(submitDataset, "f1108_pclst_ExciseTaxAffidavitStatmentDetails", ht, dataTable);
            return submitDataset;
        }

        /// <summary>
        /// F1108_s the list management queue.
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="name">The name.</param>
        /// <param name="receiptDate">The Receipt date.</param>
        /// <param name="address">The address.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="statementNumber">statementNumber</param>
        /// <returns>Returns SubmittalQueue dataset</returns>
        public static SubmittalQueueData F1108_ListManagementQueue(string parcelNumber, string name, string receiptDate, string address, string taxCode, string receiptNumber, string statementNumber)
        {
            SubmittalQueueData submittalDataSet = new SubmittalQueueData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@Name", name);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@Address", address);
            ht.Add("@TaxCode", taxCode);
            ht.Add("@ReceiptNumber", receiptNumber);
            ht.Add("@StatementNumber", statementNumber);
            ////string[] tableName = new string[] { managementQueueDataSet.ListManagementQueue.TableName, managementQueueDataSet.ListRollYear.TableName };
            Utility.LoadDataSet(submittalDataSet.SubmittalQueueDatatable, "f1108_pclst_ExciseTaxAffidavitSubmittedQueue", ht);
            return submittalDataSet;
        }

        /// <summary>
        /// F1108_s the list configuration detail.
        /// </summary>
        /// <returns>Returns ConfigurationDetail</returns>
        public static SubmittalQueueData F1108_ListConfigurationDetail()
        {
            SubmittalQueueData submitDataset = new SubmittalQueueData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(submitDataset.ListConfigurationDetail, "f1108_pcget_ExciseSubmitConfiguration", ht);
            return submitDataset;
        }

        /// <summary>
        /// F1108_s the get submit affidavit reply.
        /// </summary>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>REETA  Data Explain Status Of Update</returns>
        public static REETA F1108_GetSubmitAffidavitReply(string reetReplyXml, int userId)
        {
            REETA reetReply = new REETA();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@ReetReplay", reetReplyXml);
            string[] dataTable = new string[] { reetReply.ErrorStatusDataTable.TableName, reetReply.AFFIDAVIT.TableName, reetReply.INDIVIDUAL.TableName, reetReply.PARCEL.TableName, reetReply.USE_CODES.TableName, reetReply.SUPPLEMENTAL.TableName };
            Utility.LoadDataSet(reetReply, "f1108_pcins_ExciseTaxAffidavitSubmitted", ht, dataTable);
            return reetReply;
        }

        /// <summary>
        /// F1108_s the save reply reet XML. This is for Testing purpose and it should be removed in later stage.
        /// </summary>
        /// <param name="reetXml">The reet XML.</param>
        /// <param name="reetReplyXml">The reet reply XML.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer Value</returns>
        public static int F1108_SaveReplyReetXml(string reetXml, string reetReplyXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SubmitXML", reetXml);
            ht.Add("@ReplayXML", reetReplyXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1108_pcins_ExciseSubmitReplay", ht);
        }
    }
}
