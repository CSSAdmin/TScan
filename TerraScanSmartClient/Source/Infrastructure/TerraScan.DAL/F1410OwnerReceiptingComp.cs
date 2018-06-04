// -------------------------------------------------------------------------------------------
// <copyright file="F1410OwnerReceiptingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    /// F1410OwnerReceiptingComp
    /// </summary>
    public static class F1410OwnerReceiptingComp
    {
        /// <summary>
        /// F1410_s the get owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns Owner Reeipting DataSet</returns>
        public static F1410OwnerReceiptingData F1410_GetOwnerReceipting(string interestDate, string ownerId, string parcelIds)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            Hashtable ht = new Hashtable();
            ht.Add("@InterestDate", interestDate);
            ht.Add("@OwnerIDs", ownerId);
            ht.Add("@StatementIDs", parcelIds);
            string[] tableName = new string[] { ownerReceiptingDataSet.ListOwnerReceiptTable.TableName, ownerReceiptingDataSet.ListOwnerStatementTable.TableName, ownerReceiptingDataSet.FormBackgroundTable.TableName };
            Utility.LoadDataSet(ownerReceiptingDataSet, "f1410_pcget_OwnerReceipt", ht, tableName);
            return ownerReceiptingDataSet;
        }

        /// <summary>
        /// F1410_s the list owner receipting.
        /// </summary>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <returns>Returns OwnerReceipting Dataset</returns>
        public static F1410OwnerReceiptingData F1410_ListOwnerReceipting(string interestDate, string statementXml, string formBackColor)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            Hashtable ht = new Hashtable();
            ht.Add("@InterestDate", interestDate);
            ht.Add("@Statements", statementXml);
            ht.Add("@FormBackgroundColor", formBackColor);
            string[] tableName = new string[] { ownerReceiptingDataSet.ListOwnerStatementTable.TableName, ownerReceiptingDataSet.FormBackgroundTable.TableName };
            //Utility.FillDataSet(ownerReceiptingDataSet.ListOwnerStatementTable, "f1410_pclst_OwnerReceipt", ht);
            Utility.LoadDataSet(ownerReceiptingDataSet, "f1410_pclst_OwnerReceipt", ht, tableName);
            return ownerReceiptingDataSet;
        }

        /// <summary>
        /// F1410_s the delete owner receipting.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <param name="ownerXml">The owner XML.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <param name="userId">userId</param>
        /// <returns>Returns OwnerReceipting Dataset</returns>
        public static F1410OwnerReceiptingData F1410_DeleteOwnerReceipting(int ownerId, string ownerXml, string statementXml, int userId, string formBackColor)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerID", ownerId);
            ht.Add("@Owners", ownerXml);
            ht.Add("@Statements", statementXml);
            ht.Add("@FormBackgroundColor", formBackColor);
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { ownerReceiptingDataSet.ListOwnerReceiptTable.TableName, ownerReceiptingDataSet.ListOwnerStatementTable.TableName, ownerReceiptingDataSet.FormBackgroundTable.TableName };
            Utility.LoadDataSet(ownerReceiptingDataSet, "f1410_pcdel_OwnerReceipt", ht, tableName);
            return ownerReceiptingDataSet;
        }

        /// <summary>
        /// F1410_s the save owner receipting.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <param name="interestDate">The interest date.</param>
        /// <param name="ppaymentId">The ppayment id.</param>
        /// <param name="paymentOption">The payment option.</param>
        /// <param name="statementXml">The statement XML.</param>
        /// <returns>Returns OwnerReceipting Dataset</returns>
        public static string F1410_SaveOwnerReceipting(int userId, string receiptDate, string interestDate, int ppaymentId, int paymentOption, string statementXml)
        {
            //F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@ReceiptDate", receiptDate);
            ht.Add("@InterestDate", interestDate);

            if (ppaymentId == 0)
            {
                ht.Add("@PPaymentID", DBNull.Value);
            }
            else
            {
                ht.Add("@PPaymentID", ppaymentId);
            }

            ht.Add("@PaymentOption", paymentOption);
            ht.Add("@Statements", statementXml);
            //string[] tableName = new string[] { ownerReceiptingDataSet.ListOwnerStatementTable.TableName, ownerReceiptingDataSet.ReceiptIdTable.TableName };
            //Utility.FillDataSet(ownerReceiptingDataSet, "f1410_pcins_OwnerReceipt", ht, tableName);
            return Utility.FetchSPExecuteXmlString("f1410_pcins_OwnerReceipt", ht);
            //return ownerReceiptingDataSet;
        }


        /// <summary>
        /// F1410_SaveOwnerReceiptPreview
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="statementDetails">statementDetails</param>
        /// <returns>int</returns>
        public static int F1410_SaveOwnerReceiptPreview(int userId, string statementDetails)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@Statements", statementDetails);
            return Utility.FetchSPExecuteKeyId("f1410_pcins_OwnerReceiptPreview", ht);  
        }
        #region ListAttachmentDetails

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public static F1410OwnerReceiptingData F1410_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            F1410OwnerReceiptingData ownerReceiptingDataSet = new F1410OwnerReceiptingData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@KeyIDs", keyIds);
            ht.Add("@UserID", userId);
            ht.Add("@ModuleID", moduleId);
            Utility.LoadDataSet(ownerReceiptingDataSet.ListAttachment, "f1410_pcget_ReceiptAttachment", ht);
            return ownerReceiptingDataSet;
        }

        #endregion ListAttachmentDetails
    }
}
